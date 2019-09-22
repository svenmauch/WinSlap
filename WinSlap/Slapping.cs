using System;
using System.Windows.Forms;

namespace WinSlap
{
    public partial class Slapping : Form
    {
        public Slapping()
        {
            InitializeComponent();
        }

        private void Slapping_Load(object sender, EventArgs e)
        {

        }

        public void SetCurrentOp(string text)
        {
            // ThreadHelperClass.SetText(this, currentOp, text);
            this.currentOp.Text = text;
        }
    }
}
