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
    public partial class SnakeGraphics : Form
    {
        /// <summary>
        /// 
        /// definisanje boja za: telo zmije, hranu i dve vrste polja GameGrid-a
        /// 
        /// </summary>
        SolidBrush snakeBodyBrush = new SolidBrush(Color.FromArgb(46,138,211));
        SolidBrush foodBrush = new SolidBrush(Color.Red);
        SolidBrush gridCellOne = new SolidBrush(Color.FromArgb(134,117,169));
        SolidBrush gridCellTwo = new SolidBrush(Color.FromArgb(195, 174, 214));
        Pen snakeBodyBorderPen = new Pen(Color.Black);
        Graphics g,f;

        Snake snake; 

        Options options=new Options();

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
            options.StartPosition = this.StartPosition;
            //options.currentState = 0;
            options.ShowDialog();
            

            trackSpeed.Minimum = 1;
            trackSpeed.Maximum = 4;

            this.ActiveControl = gameGrid;
            f = this.CreateGraphics();
            g = gameGrid.CreateGraphics();
            

            gridX = gameGrid.Location.X;
            gridY = gameGrid.Location.Y;
            gridWidth = gameGrid.Width;
            gridHeight = gameGrid.Height;

            resetSnake();

            //this.Show();
            //options.Show();
            //this.Enabled = false;

            //SetGridBackground();
        }

        void SetGridBackground()
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

        private void btnStart_Click(object sender, EventArgs e)
        {            
            resetSnake(); // postavljamo zmiju na pocetne vrednosti
            interval = speedMin - trackSpeed.Value*speedMax; // racunamo interval na osnovu odabrane vrednosti na trackSpeed-u
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
        void launchTimer()
        {
            timer = new Timer();
            timer.Interval=interval; 
            timer.Tick += Play; // govorimo tajmeru koju funkciju treba da izvrsi pri svakom otkucaju
            timer.Start();
        }
        private void Play(object sender, EventArgs e)
        {
            makeMove();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
            ClearGrid();
        }
        private void Stop()
        {
            btnStart.Enabled = true;
            trackSpeed.Enabled = true;
            tbIme.Enabled = true;

            timer.Stop();
            playing = false;

            this.ActiveControl = gameGrid;
        }

        void resetSnake()
        {
            direction = 4;
            snake = new Snake(gridHeight / snakeBodySize - 1, gridWidth / snakeBodySize - 1, glavaX, glavaY);
        }
        void GameLost()
        {
            Stop();
            MessageBox.Show("Game Over :( ");
            ClearGrid();
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
                    GameLost();
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

        private void gameGrid_Paint(object sender, PaintEventArgs e)
        {
            //CNT++;
            /*if(CNT==1)*/ SetGridBackground();
        }

        void DrawFood(int x, int y)
        {
            //g.FillRectangle(foodBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
            g.FillEllipse(foodBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData==Keys.P)
            {
                MessageBox.Show("vdsv");
                options.Show();
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

        private void SnakeGraphics_Paint(object sender, PaintEventArgs e)
        {
            //g.DrawRectangle(snakeBodyBorderPen, new Rectangle(0,0,gridWidth-1,gridHeight-1));
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

        void ClearGrid()
        {
            gameGrid.Invalidate(new Rectangle(0, 0, gridWidth, gridHeight));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ClearGrid();
        }
    }
}
