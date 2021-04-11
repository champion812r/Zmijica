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
    public partial class probnaForma : Form
    {
        public probnaForma()
        {
            InitializeComponent();
        }

        private void probnaForma_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("X");
        }
    }
}
