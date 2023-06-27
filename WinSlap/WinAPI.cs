using System;
using System.Runtime.InteropServices;

namespace WinSlap
{
    public class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static uint GetShellWindowProcessId()
        {
            var shellWindow = GetShellWindow();
            GetWindowThreadProcessId(shellWindow, out var shellProcessId);
            return shellProcessId;
        }
    }
}
