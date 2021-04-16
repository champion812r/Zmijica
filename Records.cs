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
        }
        /*public void PutResultsIntoDGV(List<...> lista)
        {
            dgvResults.DataSource = lista.ToList();
        }*/
    }
}
