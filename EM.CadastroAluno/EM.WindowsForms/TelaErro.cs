using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace EM.WindowsForms
{
    public partial class TelaErro : Form
    {

        public TelaErro(Exception exc)
        {
            InitializeComponent();

            rtxtErro.Text = exc.ToString();
        }
    }
}
