using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Windows10Brightness
{
    public class HiddenForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public HiddenForm()
        {
            WindowState = FormWindowState.Minimized;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Opacity = 0;
            ShowInTaskbar = false;

            // Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            RegisterHotKey(Handle, GetType().GetHashCode(), 8, (int)Keys.F5);
            RegisterHotKey(Handle, GetType().GetHashCode(), 8, (int)Keys.F6);

            Visible = false;
            Hide();
        }

        ~HiddenForm()
        {
            UnregisterHotKey(Handle, GetType().GetHashCode());
        }

        //{msg=0x312 (WM_HOTKEY) hwnd=0x50a8c wparam=0x31ab3fd lparam=0x740008 result=0x0} // Win+F5
        //{msg=0x312 (WM_HOTKEY) hwnd=0x50a8c wparam=0x31ab3fd lparam=0x750008 result=0x0} // Win+F6

        protected override void WndProc(ref Message m)
        {
            //if (m.Msg == 0x0312)
            //    Activate();
            //Debug.WriteLine(m.LParam);

            // Windows Key
            if (m.Msg == 0x312)
            {
                if (m.LParam == (IntPtr)0x740008) // F5
                {
                    BrightnessControl.DecreaseBrightness();
                }
                else if (m.LParam == (IntPtr)0x750008) // F6
                {
                    BrightnessControl.IncreaseBrightness();
                }
            }
            base.WndProc(ref m);
        }

    }
}
