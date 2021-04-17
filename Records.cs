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
    public partial class Records : Form
    {
        public Records()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent; ///pozicioniramo Records prozor centralno u odnosu na Options prozor

            dgvResults.EditMode = DataGridViewEditMode.EditProgrammatically; /// onemocuavamo korisniku da sam menja sadrzaj tabele
        }
        public void PutResultsIntoDGV(List<User> lista)
        {
            dgvResults.DataSource = lista.ToList(); /// prenosimmo sadrzaj liste User-a u tabelu rezultata

            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; /// podesavamo tabelu da njene kolone popune svu njenu sirinu
            dgvResults.RowHeadersVisible = false; /// sakrivamo praznu kolonu na pocetku
            dgvResults.Columns[0].HeaderText = "Name"; /// imenujemo prvu kolonu
            dgvResults.Columns[1].HeaderText = "Max Score"; /// imenujemo drugu kolonu
        }
    }
}
