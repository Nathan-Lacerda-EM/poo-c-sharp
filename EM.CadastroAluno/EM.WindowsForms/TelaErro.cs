using System;
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
