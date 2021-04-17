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
        SolidBrush snakeEye = new SolidBrush(Color.Black);
        Pen snakeBodyBorderPen = new Pen(Color.Black);
        Graphics g,f;

        Snake snake;
        Options o;

        //Options options=new Options();

        int snakeBodySize=20; // dimenzija tela zmije
        int gridWidth, gridHeight, gridX, gridY; // dimenzija i pozicija GameGrid-a
        int direction = 4; /// smer kretanja zmije: {1 2 3 4}={gore, dole, levo, desno}
        int glavaX = 6, glavaY = 6; // startna pozicija zmije
        Point lastHeadPosition = new Point(-1,-1);
        Keys[] controlKeys = new Keys[] { Keys.Down, Keys.Up, Keys.Left, Keys.Right}; // dugmici koji se koriste za upravljanje zmijom
        Timer timer= new Timer(); // tajmer koji ce otkucavati za prikaz novih frejmova
        int interval; // interval nakon kojeg ce tajmer otkucavati - brzina kretanja zmije

        bool playing = false; // oznacava da li je u toku igra

        int speedMin = 500, speedMax = 100; // minimalna i maximalna vrednost intervala, izrazeno u milisekundama

        public SnakeGraphics()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // pozicioniranje forme na centar ekrana




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
            snake = new Snake(gridHeight / snakeBodySize - 1, gridWidth / snakeBodySize - 1, glavaX, glavaY,o.name); // pravim novu zmiju i saljem potrebne podatke
        }

        void launchTimer()
        {
            timer = new Timer() { Interval = interval };
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

            //this.ActiveControl = gameGrid;
        }

        void GameOver()
        {
            Stop();

            int score=snake.CurrentScore(); 
            string message = "Game Over :((\nDo you want to save score: "+score.ToString();
            string title = "Game Over";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            /// salji kristini da li da cuva score
            if (result == DialogResult.Yes) snake.UpisivanjeListe();

            ResetGridBackground();
            StartNewGame();
        }
        bool makeMove()
        {
            List<Instrukcija> instrukckije = snake.lista(direction);
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
            FillGridCell(x, y);
        }
        void DrawBody(int x, int y)
        {
            g.FillRectangle(snakeBodyBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
        }
        void DrawHead(int x, int y)
        {
            DrawBody(x, y);
            Point beforeHead = snake.BeforeHead();
            if(lastHeadPosition.X!=-1)
            {
                DrawBody(lastHeadPosition.X,lastHeadPosition.Y);
            }

            int x1, x2, y1, y2;
            if(direction==1)
            {
                x1 = x; y1 = y;
                x2 = x + 12; y2 = y;
            }
            else if (direction==2)
            {
                x1 = x; y1 = y+12;
                x2 = x + 12; y2 = y+12;
            }
            else if(direction==3)
            {
                x1 = x; y1 = y;
                x2 = x; y2 = y+12;
            }
            else
            {
                x1 = x+12; y1=y;
                x2 = x+12; y2 = y + 12;
            }
            g.FillEllipse(snakeEye, new Rectangle(x1, y1, 8, 8));
            g.FillEllipse(snakeEye, new Rectangle(x2, y2, 8, 8));
            lastHeadPosition = new Point(x, y);

        }
        void DrawFood(int x, int y)
        {
            g.FillEllipse(foodBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
        }

        void Pause()
        {
            timer.Stop();

            Options o = new Options(playing);
            o.ShowDialog();

            timer.Start();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData==Keys.P)
            {
                Pause();
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
            o = new Options(false);
            o.ShowDialog();

            GameSettings gs = o.getSettings();

            resetSnake(); // postavljamo zmiju na pocetne vrednosti
            interval = speedMin - gs.speed * speedMax; // racunamo interval na osnovu odabrane vrednosti na trackSpeed-u
            launchTimer(); // pokrecemo tajmer kako bi igrica zapocela
            playing = true;
        }

        private void RedrawGrid()
        {
            ResetGridBackground();

            if (snake == null) return;

            List<Point>[] data = snake.SnakeAndFoodData();
            foreach(Point p in data[0])
            {
                int X = p.X, Y = p.Y;
                int x = X * snakeBodySize, y = Y * snakeBodySize;
                DrawBody(x, y);
            }
            foreach(Point p in data[1])
            {
                int X = p.X, Y = p.Y;
                int x = X * snakeBodySize, y = Y * snakeBodySize;
                DrawFood(x, y);
            }
        }

        private void gameGrid_Paint(object sender, PaintEventArgs e)
        {
            RedrawGrid();
        }
    }
}
