﻿using System;
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
    public partial class NewGameControl : UserControl
    {
        public NewGameControl(ref Options o)
        {
            InitializeComponent();
            o.Fun();
        }
    }
}