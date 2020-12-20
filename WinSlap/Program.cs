using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace WinSlap
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Get Operating system information.
            OperatingSystem os = Environment.OSVersion;
            //Get version information about the os.
            Version vs = os.Version;

            if (vs.Major != 10)
            {
                MessageBox.Show("WinSlap currently only supports Windows 10.", "Unsupported OS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }

            string releaseid = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString();

            if (releaseid != "2009")
            {
                MessageBox.Show("WinSlap 1.4 is developed for Windows 10 (2009/20H2).\nThis PC is running Windows 10 (" + releaseid + ").\nPlease proceed with caution.", "Untested OS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.Run(new MainForm(args));
        }
    }
}
