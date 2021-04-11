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
    public partial class Form1 : Form
    {
        Snake zmija;
        public Form1()
        {
            InitializeComponent();
            zmija = new Snake();

        }
        public void DrawRectangleFloat(PaintEventArgs e)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create location and size of rectangle.
            float x = 0.0F;
            float y = 0.0F;
            float width = 200.0F;
            float height = 200.0F;

            // Draw rectangle to screen.
            e.Graphics.DrawRectangle(blackPen, x, y, width, height);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush myBrush = new SolidBrush(Color.Red);
            Graphics grafika = this.CreateGraphics();

        }
    }
}
