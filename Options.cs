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
        bool playing;
        public string name;
        public Options(bool playing)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            this.playing = playing;
            if(playing)
            {
                btnNewGame.Enabled = false;
                tbName.Enabled = false;
                trackSpeed.Enabled = false;
            }
            else
            {
                btnResume.Enabled = false;
            }
        }
        public GameSettings getSettings()
        {
            GameSettings gs = new GameSettings();
            gs.name = tbName.Text;
            gs.speed = trackSpeed.Value;
            return gs;
        }


        private void btnResume_Click(object sender, EventArgs e)
        {
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
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            if(!ValidName(tbName.Text))
            {
                MessageBox.Show("Name is not valid!");
                return;
            }
            name = tbName.Text;
            this.Hide();
        }

        private void btnResults_Click(object sender, EventArgs e)
        {
            Records r = new Records();
            this.Hide();

            List<User> lista = Snake.AllUsersData();
            if (lista.Count == 0)
            {
                MessageBox.Show("There are no records.", "Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Show();
                return;
            }

            r.PutResultsIntoDGV(lista);
            r.ShowDialog();
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            string message="";
            if(playing) message= "Score won't be saved!"; 
            message += "Do you really want to exit the game?";
            string title = "Exit Game";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            //MessageBox.Show()
            if(result==DialogResult.No)
            {
                return;
            }
            Application.Exit();
        }
    }
}
