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
        SolidBrush snakeBodyBrush = new SolidBrush(Color.Green);
        SolidBrush foodBrush = new SolidBrush(Color.Red);
        Pen snakeBodyBorderPen = new Pen(Color.Black);
        Graphics g,f;

        Snake snake;

        Options options=new Options();

        int snakeBodySize=20;
        int gridWidth, gridHeight, gridX, gridY;
        int direction = 4; /// {1 2 3 4}={up, down, left, right}
        int glavaX = 6, glavaY = 6;

        Keys[] filteredKeys = new Keys[] { Keys.Down, Keys.Up, Keys.Left, Keys.Right};
        Timer timer= new Timer();
        int interval;

        int labelcnt = 0;
        bool playing = false;

        int speedMin = 500, speedMax = 100;

        public SnakeGraphics()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            //options.StartPosition = this.StartPosition;

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
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            resetSnake();
            interval = speedMin - trackSpeed.Value*speedMax;
            launchTimer();
            playing = true;

            btnStart.Enabled = false;
            trackSpeed.Enabled = false;
            tbIme.Enabled = false;
        }
        void launchTimer()
        {
            timer = new Timer();
            timer.Interval=interval;
            timer.Tick += Play;
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
            MessageBox.Show("Game lost!");
            ClearGrid();
        }
        bool makeMove()
        {
            labelcnt++;
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

        void Erase(int x, int y)
        {
            gameGrid.Invalidate(new Rectangle(x, y, snakeBodySize, snakeBodySize));
        }
        void DrawHead(int x, int y)
        {
            g.FillRectangle(snakeBodyBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
            //g.DrawRectangle(snakeBodyBorderPen, new Rectangle(x, y, snakeBodySize - 1, snakeBodySize - 1));
        }
        void DrawFood(int x, int y)
        {
            g.FillRectangle(foodBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData==Keys.P)
            {
                MessageBox.Show("vdsv");
                options.Show();
            }
            if (!playing) return base.ProcessCmdKey(ref msg, keyData);
            bool ret = filteredKeys.Contains(keyData);
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
