using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinSlap
{
    public partial class Coffee : Form
    {
        public Coffee()
        {
            InitializeComponent();
        }

        private static void RestartNow()
        {
            ProcessStartInfo proc = new ProcessStartInfo
            {
                FileName = "cmd",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/C shutdown -f -r -t 0"
            };
            Process.Start(proc);
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            RestartNow();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            string cmdLine = $"\"{Environment.GetFolderPath(Environment.SpecialFolder.System)}\\cmd.exe\" /C start https://ko-fi.com/svenmauch";
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce");
            key.SetValue("WinSlapOpenKoFi", cmdLine);
            key.Close();
            RestartNow();
        }
    }
}
