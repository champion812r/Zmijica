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
    public partial class NewGame : Form
    {
        int speed;
        bool controlMod;
        public bool playable = false;
        string name;
        public NewGame()
        {
            InitializeComponent();
            trackSpeed.Minimum = 1; trackSpeed.Maximum = 4;
            rbWASD.Checked = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            playable = false;
            speed = trackSpeed.Value;
            controlMod = rbWASD.Checked;
            name = tbName.Text;

            if(!ValidName(name))
            {
                MessageBox.Show("Invalid name!");
                return;
            }
            playable = true;
            this.Hide();
        }
        private bool ValidName(string name)
        {
            for(int i=0; i<name.Length; ++i)
            {
                if (name[i] != ' ') return true;
            }
            return false;
        }
    }
}
