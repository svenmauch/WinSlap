using System.Windows.Forms;

namespace WinSlap
{
    public static class ThreadHelperClass
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);

        public static void SetText(Form form, Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = SetText;
                form.Invoke(d, form, ctrl, text);
            }
            else
            {
                ctrl.Text = text;
            }
        }
    }
}
