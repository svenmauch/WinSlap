using System;
using System.Windows.Forms;

namespace WinSlap
{
    public partial class SlapForm : Form
    {
        public int PercentFinished
        {
            get => progressBar1.Value;
            set => progressBar1.Value = value;
        }

        public string CurrentJobText
        {
            get => currentOp.Text;
            set => currentOp.Text = value;
        }

        public SlapForm()
        {
            InitializeComponent();
        }

        private void Slapping_Load(object sender, EventArgs e)
        {

        }
    }
}
