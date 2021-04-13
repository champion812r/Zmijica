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
        NewGame newgame = new NewGame();
        public bool playing = false;
        public Options()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            newgame.StartPosition = this.StartPosition;
        }

        private void btnResume_Click(object sender, EventArgs e)
        {

        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            this.Hide();
            newgame.ShowDialog();
            this.Show();
            if(newgame.playable)
            {
                this.Hide();
                
            }
        }

        private void Options_Shown(object sender, EventArgs e)
        {
            
            //if(newgame.playable) 
        }
    }
}
