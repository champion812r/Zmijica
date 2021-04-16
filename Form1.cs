using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmijica
{
    public struct GameSettings
    {
        /// <summary>
        /// 
        /// Struktura koja čuva podatke o brzini zmije i imenu igrača
        /// koji se unose pre početka gejma
        /// 
        /// </summary>
        public int speed;
        public string name;
    }
    public partial class SnakeGraphics : Form
    {
        /// <summary>
        /// 
        /// definisanje boja za: telo zmije, hranu i dve vrste polja GameGrid-a
        /// 
        /// </summary>
        SolidBrush snakeBodyBrush = new SolidBrush(Color.FromArgb(46,138,211));
        SolidBrush foodBrush = new SolidBrush(Color.OrangeRed);
        SolidBrush gridCellOne = new SolidBrush(Color.FromArgb(134,117,169));
        SolidBrush gridCellTwo = new SolidBrush(Color.FromArgb(195, 174, 214));
        Pen snakeBodyBorderPen = new Pen(Color.Black);
        Graphics g,f;

        Snake snake; 

        //Options options=new Options();

        int snakeBodySize=20; // dimenzija tela zmije
        int gridWidth, gridHeight, gridX, gridY; // dimenzija i pozicija GameGrid-a
        int direction = 4; /// smer kretanja zmije: {1 2 3 4}={gore, dole, levo, desno}
        int glavaX = 6, glavaY = 6; // startna pozicija zmije

        Keys[] controlKeys = new Keys[] { Keys.Down, Keys.Up, Keys.Left, Keys.Right}; // dugmici koji se koriste za upravljanje zmijom
        Timer timer= new Timer(); // tajmer koji ce otkucavati za prikaz novih frejmova
        int interval; // interval nakon kojeg ce tajmer otkucavati - brzina kretanja zmije

        int labelcnt = 0;
        bool playing = false; // oznacava da li je u toku igra

        int speedMin = 500, speedMax = 100; // minimalna i maximalna vrednost intervala, izrazeno u milisekundama

        public SnakeGraphics()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // pozicioniranje forme na centar ekrana


            /// <summary>
            /// 
            /// Setovanje maksimalnog i minimalnog stepena za brzinu zmije
            /// 
            /// </summary>
            trackSpeed.Minimum = 1;
            trackSpeed.Maximum = 4;

            this.Enabled = false;
            g = gameGrid.CreateGraphics(); //pomocu 'g' se iscrtavaju objekti po GameGrid-u
            

            gridWidth = gameGrid.Width;
            gridHeight = gameGrid.Height;


            this.Show(); //rucno prikazujem osnovnu formu da se ne bi Options forma prikazala prva
            MessageBox.Show("Control snake using arrow keys. Pause game by pressing 'P'", "Welcome to game Zmijica!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            StartNewGame();
        }

        void ResetGridBackground()
        {
            /// <summary>
            /// 
            /// Prikazivanje pozadine na GameGrid-u. 
            /// x i y predstavljaju piksele u GameGrid-u
            /// 
            /// </summary>
            for (int y = 0; y <= gridHeight - snakeBodySize; y += snakeBodySize)
            {
                for (int x = 0; x <= gridWidth - snakeBodySize; x += snakeBodySize)
                {
                    FillGridCell(x, y); // popunjavanje pravougaonog polja ciji se gornji levi ugao nalazi na poziciji (x,y) 
                }
            }
        }
        void resetSnake()
        {
            /// <summary>
            /// 
            /// Postavljanje zmije na pocetne vrednosti
            /// Startni smer je desno (4)
            /// 
            /// </summary>
            direction = 4;
            snake = new Snake(gridHeight / snakeBodySize - 1, gridWidth / snakeBodySize - 1, glavaX, glavaY); // pravim novu zmiju i saljem potrebne podatke
        }

        void launchTimer()
        {
            timer = new Timer();
            timer.Interval=interval; 
            timer.Tick += Frame; // govorimo tajmeru koju funkciju treba da izvrsi pri svakom otkucaju odnosno za svaki frejm
            timer.Start();
        }
        private void Frame(object sender, EventArgs e)
        {
            makeMove(); // u svakom frejmu trazicamo da zmija nacini sledeci korak
        }


        private void Stop()
        {
            /// <summary>
            /// 
            /// Funkcija koja zaustavlja igru, odnosno zaustavlja timer koji je sam po sebi pokretac igre
            /// 
            /// </summary>

            timer.Stop();
            playing = false;

            this.ActiveControl = gameGrid;
        }

        void GameOver()
        {
            Stop();

            int score=0; //pokupim score od kristine
            string message = "Game Over :((\nDo you want to save score: "+score.ToString();
            string title = "Game Over";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            /// salji kristini da li da cuva score

            ResetGridBackground();
            StartNewGame();

            /*Options o = new Options(false);
            o.ShowDialog();*/
        }
        bool makeMove()
        {
            labelcnt++;
            //labelScore.Text = CNT.ToString();
            //label1.Text = labelcnt.ToString();

            List<Instrukcija> instrukckije = snake.lista(direction);
            label1.Text = instrukckije.Count().ToString();
            foreach(Instrukcija i in instrukckije)
            {
                int X = i.xy.X, Y = i.xy.Y;
                if(X==-1)
                {
                    GameOver();
                    return false;
                }
                int x = X * snakeBodySize, y = Y * snakeBodySize;
                if (i.oboj)
                {
                    if (i.telo)
                    {
                        DrawHead(x, y);
                    }
                    else
                    {
                        DrawFood(x, y);
                    }
                    //DrawHead(x, y);
                }
                else
                {
                    Erase(x, y);
                }
            }
            return true;
        }

        void FillGridCell(int x, int y)
        {
            int i = x / snakeBodySize, j = y / snakeBodySize;
            if ((i + j) % 2 == 0)
            {
                g.FillRectangle(gridCellOne, new Rectangle(x, y, snakeBodySize, snakeBodySize));
            }
            else g.FillRectangle(gridCellTwo, new Rectangle(x, y, snakeBodySize, snakeBodySize));
        }
        void Erase(int x, int y)
        {
            //gameGrid.Invalidate(new Rectangle(x, y, snakeBodySize, snakeBodySize));
            FillGridCell(x, y);
        }
        void DrawHead(int x, int y)
        {
            g.FillRectangle(snakeBodyBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
            //g.DrawRectangle(snakeBodyBorderPen, new Rectangle(x, y, snakeBodySize - 1, snakeBodySize - 1));
        }
        void DrawFood(int x, int y)
        {
            //g.FillRectangle(foodBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
            g.FillEllipse(foodBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
        }


        private void btnRecords_Click(object sender, EventArgs e)
        {
            // preuzmem listu korisnika
            /*if(listaRezultata.Count()==0)
            {
                MessageBox.Show("Jos uvek nema rezultata");
                return;
            }
            Records r = new Records();
            r.PutResultsIntoDGV(listaRezultata);
            r.ShowDialog();*/
        }


        void Pause()
        {
            timer.Stop();

            Options o = new Options(playing);
            o.ShowDialog();

            timer.Start();
            //int state=o
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData==Keys.P)
            {
                Pause();
                //MessageBox.Show("vdsv");
                //options.Show();
            }
            if (!playing) return base.ProcessCmdKey(ref msg, keyData);
            bool ret = controlKeys.Contains(keyData);
            if(ret)
            {
                bool directionChanged = false;
                if (keyData==Keys.Down && direction!=1 && direction!=2)
                {
                    direction = 2;
                    directionChanged = true;
                }
                if (keyData == Keys.Up && direction != 2 && direction != 1)
                {
                    direction = 1;
                    directionChanged = true;
                }
                if (keyData == Keys.Left && direction != 4 && direction != 3)
                {
                    direction = 3;
                    directionChanged = true;
                }
                if (keyData == Keys.Right && direction != 3 && direction != 4)
                {
                    direction = 4;
                    directionChanged = true;
                }
                if(directionChanged) 
                {
                    timer.Stop();
                    if(makeMove()) timer.Start();
                }
            }
            return ret;
        }

        void StartNewGame()
        {
            Options o = new Options(false);
            o.ShowDialog();

            GameSettings gs = o.getSettings();

            resetSnake(); // postavljamo zmiju na pocetne vrednosti
            interval = speedMin - gs.speed * speedMax; // racunamo interval na osnovu odabrane vrednosti na trackSpeed-u
            launchTimer(); // pokrecemo tajmer kako bi igrica zapocela
            playing = true;

            /// <summary>
            /// 
            /// Onemogucujemo button Start, birac brzine i polje za ime kada otpocne igra
            /// 
            /// </summary>
            btnStart.Enabled = false;
            trackSpeed.Enabled = false;
            tbIme.Enabled = false;

        }
        private void SnakeGraphics_Paint(object sender, PaintEventArgs e)
        {
            /// POZOVI KRISTININU FUNKCIJU STO VRACA POZICIJE ZMIJE I HRANE
            /// STAMPAJ TO
        }

        

        

        int CNT = 0;
        private void gameGrid_Paint(object sender, PaintEventArgs e)
        {
            CNT++;
            if(CNT==1) ResetGridBackground();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int y = 0; y <= gridHeight - snakeBodySize; y += snakeBodySize)
            {
                for (int x = 0; x <= gridWidth - snakeBodySize; x += snakeBodySize)
                {
                    g.FillRectangle(snakeBodyBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
                    g.DrawRectangle(snakeBodyBorderPen, new Rectangle(x, y, snakeBodySize, snakeBodySize));
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ResetGridBackground();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
            ResetGridBackground();
        }
    }
}
