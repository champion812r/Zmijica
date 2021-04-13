﻿using System;
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
        //Snake zmija=new Snake();
        SolidBrush snakeBodyBrush = new SolidBrush(Color.Red);
        Pen snakeBodyBorderPen = new Pen(Color.Black);
        Graphics g,f;

        int snakeBodySize=30;
        int gridWidth, gridHeight, gridX, gridY;
        int direction = 4; /// {1 2 3 4}={up, down, left, right}

        Keys[] filteredKeys = new Keys[] { Keys.Down, Keys.Up, Keys.Left, Keys.Right };
        Timer timer;
        int interval;

        int labelcnt = 0;
        bool playing = false;

        int speedMin = 500, speedMax = 100;

        public SnakeGraphics()
        {

            Snake novaZmija = new Snake(gridHeight/snakeBodySize,gridWidth/snakeBodySize);

            InitializeComponent();
            trackSpeed.Minimum = 1;
            trackSpeed.Maximum = 4;

            this.ActiveControl = gameGrid;
            f = this.CreateGraphics();
            g = gameGrid.CreateGraphics();
            

            gridX = gameGrid.Location.X;
            gridY = gameGrid.Location.Y;
            gridWidth = gameGrid.Width;
            gridHeight = gameGrid.Height;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
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
            btnStart.Enabled = true;
            trackSpeed.Enabled = true;
            tbIme.Enabled = true;
            timer.Stop();
        }


        
        void makeMove()
        {
            labelcnt++;
            label1.Text = labelcnt.ToString();



            ///saljem direction kristini
            ///List<> = novaZmija.Move(direction);
            ///vraca mi listu izmena
            ///primenjujem izmene na gameGrid-u
            ///
            ///
            ///kada se zavrsi gejm, saljem joj ime igraca da ga upise u bazu, a ona racuna score
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
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
                    makeMove();
                    timer.Start();
                }
            }
            return ret;
        }

        private void SnakeGraphics_Paint(object sender, PaintEventArgs e)
        {
            g.DrawRectangle(snakeBodyBorderPen, new Rectangle(0,0,gridWidth-1,gridHeight-1));
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
            gameGrid.Invalidate(new Rectangle(1,1, gridWidth - 2, gridHeight -2));
        }
    }
}
