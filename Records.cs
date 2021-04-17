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
            this.StartPosition = FormStartPosition.CenterParent;

            dgvResults.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        public void PutResultsIntoDGV(List<User> lista)
        {
            dgvResults.DataSource = lista.ToList();

            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.RowHeadersVisible = false;
            dgvResults.Columns[0].HeaderText = "Name";
            dgvResults.Columns[1].HeaderText = "Max Score";
        }
    }
}
