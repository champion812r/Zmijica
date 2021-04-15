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
    public partial class Options : Form
    {
        public NewGameControl ngc;
        public optionsControl oc = new optionsControl();
        public int currentState = 0; // 0-options 1-newgame
        public bool playing = false;
        public Options()
        {
            InitializeComponent();
            //ngc = new NewGameControl(ref this);
            this.StartPosition = FormStartPosition.CenterParent;

            panel1.Controls.Add(ngc);
        }
        
        public void Fun()
        {
            MessageBox.Show("ddfdsf");
        }

        
    }
}
