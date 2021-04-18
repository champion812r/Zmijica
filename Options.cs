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
        bool playing; /// indikator da li je prozor options otvoren u toku igre ili ne
        public string name; /// ime igraca
        public Options(bool playing) 
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent; ///pozicioniramo Options prozor centralno u odnosu na osnovnu formu

            rbArrows.Checked = true;

            this.playing = playing;
            if(playing)
            {
                ///
                /// Ukoliko se igra, odnosno Options prozor je otvoren preko dugmeta 'P',
                /// u toku je pauza i treba onemoguciti button za novu igru, polje za ime i birac brzine
                ///

                btnNewGame.Enabled = false;
                tbName.Enabled = false;
                trackSpeed.Enabled = false;
                rbArrows.Enabled = rbWASD.Enabled = false;
            }
            else
            {
                ///
                /// Ukoliko se ne igra, odnosno Options prozor je otvoren pred pocetak igre,
                /// treba onemoguciti samo button Resume jer igra jos uvek nije zapoceta.
                ///
                btnResume.Enabled = false;
            }
        }
        public GameSettings getSettings()
        {
            ///
            /// Funkcija koja pokuplja korisnicki odabrane vrednosti
            ///

            GameSettings gs = new GameSettings();
            gs.name = tbName.Text;
            gs.speed = trackSpeed.Value;
            gs.controls = rbArrows.Checked;
            return gs;
        }


        private void btnResume_Click(object sender, EventArgs e)
        {
            ///
            /// Igra se nastavlja jednostavnim sakrivanjem prozora Options.
            ///
            this.Hide();
        }

        private bool ValidName(string name)
        {
            ///
            /// Funkcija koja proverava da li je uneseno ime validno,
            /// odnosno da li sadrzi ista osim razmaka
            ///
            for (int i=0; i<name.Length; ++i)
            {
                if (name[i] != ' ') return true; ///ukoliko ime sadrzi nesto sto nije razmak, ime je validno
            }
            return false;
        }
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            ///
            /// Button pomocu kojeg se zapocinje nova igra
            ///
            if (!ValidName(tbName.Text))
            {
                ///
                /// Ukoliko ime nije validno, igrac ce biti obavesten i nista se nece dogoditi
                ///
                MessageBox.Show("Name is not valid!");
                return;
            }

            ///
            /// Ukoliko je ima validno, sacuvacemo ga u stringu name
            /// i zatvoriti Options formu cime ce otpoceti igra
            ///
            name = tbName.Text;
            this.Hide();
        }

        private void btnResults_Click(object sender, EventArgs e)
        {
            ///
            /// Button koji otvara formu Records u kojoj su prikazani prethodni rezultati
            ///

            Records r = new Records();

            //List<User> lista = Snake.AllUsersData(); /// prikupljam listu User-a iz klase Snake
            Snake.UcitavanjePodataka();
            List<User> lista = Snake.AllUsersData();
            if (lista.Count == 0)
            {
                ///
                /// Ukoliko jos uvek nema rezultata, igrac ce o tome biti obavesten
                ///
                MessageBox.Show("There are no records.", "Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            ///
            /// Ukoliko ima rezultata, pozivam funkciju koja unosi rezultate u tabelu
            /// i prikazujem formu sa rezultatima
            ///
            r.PutResultsIntoDGV(lista);
            r.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ///
            /// Kada igrac pritisne button Exit, bice upitan da li
            /// zaista zeli da napusti igru, i ukoliko pokusa da izadje u toku igre,
            /// bice jos i obavesten da se rezultat nece sacuvati
            ///
            string message ="";
            if(playing) message= "Score won't be saved!"; 
            message += "Do you really want to exit the game?";
            string title = "Exit Game";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            //MessageBox.Show()
            if(result==DialogResult.No)
            {
                ///
                /// Ukoliko ipak ne zeli da napusti, nista se nece dogoditi
                ///
                return;
            }

            Application.Exit(); /// Ukoliko zeli da napusti, aplikacija se gasi
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
