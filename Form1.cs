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
        Snake zmija=new Snake();
        SolidBrush snakeBodyBrush = new SolidBrush(Color.Red);
        Pen snakeBodyBorderPen = new Pen(Color.Black);
        Graphics g,f;

        int snakeBodySize=30;
        int gridWidth, gridHeight, gridX, gridY;

        private void SnakeGraphics_Paint(object sender, PaintEventArgs e)
        {
            //g.FillRectangle(snakeBodyBrush, new Rectangle(0, 0, 30, 30));
            //g.DrawRectangle(snakeBodyBorderPen, new Rectangle(gridX-1,gridY-1,gridWidth+1,gridHeight+1));
            g.DrawRectangle(snakeBodyBorderPen, new Rectangle(0,0,gridWidth-1,gridHeight-1));

        }

        public SnakeGraphics()
        {
            InitializeComponent();
            f = this.CreateGraphics();
            g = gameGrid.CreateGraphics();

            gridX = gameGrid.Location.X;
            gridY = gameGrid.Location.Y;
            gridWidth = gameGrid.Width;
            gridHeight = gameGrid.Height;

            //string msg = String.Format("{0} {1} {2} {3}", gridX - 1, gridY - 1, gridWidth + 1, gridHeight + 1);
            //MessageBox.Show(msg);

            //g = gameGrid.CreateGraphics();

            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Rectangle rec = new Rectangle(0, 0, 50, 50);
            //g.FillRectangle(snakeBodyBrush, new Rectangle(0, 0, 300, 300));
            //g.DrawRectangle(snakeBodyBorderPen,new Rectangle(0,0,300,300));
            //snakeBodyBrush.Dispose(); g.Dispose();

            for(int y=0; y<=gridHeight-snakeBodySize; y+=snakeBodySize)
            {
                for(int x=0; x<=gridWidth-snakeBodySize; x+=snakeBodySize)
                {
                    g.FillRectangle(snakeBodyBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
                    g.DrawRectangle(snakeBodyBorderPen, new Rectangle(x, y, snakeBodySize, snakeBodySize));
                }
            }

            /*for (int y = 0; y <= gridHeight - snakeBodySize; y += snakeBodySize)
            {
                for (int x = 0; x <= gridWidth - snakeBodySize; x += snakeBodySize)
                {
                    Label p = new Label();
                    p.Location = new Point(x, y);
                    p.Size = new Size(snakeBodySize, snakeBodySize);
                    p.BackColor = Color.Red;
                    p.Enabled=false;
                    gameGrid.Controls.Add(p);
                }
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gameGrid.Invalidate(new Rectangle(1,1, gridWidth - 2, gridHeight -2));
        }
    }
}
