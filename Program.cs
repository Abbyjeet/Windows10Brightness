/*/
 *  Windows 10 Brightness using Winkey + F5 (-) / F6 (+)
 *  
 *  Author: Abhijit Jathar
 *
 *  based on work of:
 *  Stephan (Stepe - Codeproject) http://www.codeproject.com/Articles/236898/Screen-Brightness-Control-for-Laptops-and-Tablets
 *  Samuel Lai http://edgylogic.com/projects/display-brightness-vista-gadget/
/*/

using System;
using System.Windows.Forms;

namespace Windows10Brightness
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitialSetup();
        }

        private static void InitialSetup()
        {
            NotifyIcon niMain = new NotifyIcon();
            ContextMenu ctxmnuMain = new ContextMenu();
            MenuItem mnuExit = new MenuItem
            {
                Index = 0,
                Text = "E&xit",
            };

            ctxmnuMain.MenuItems.AddRange(new MenuItem[] { mnuExit });
            mnuExit.Click += new EventHandler(mnuExit_Click);

            niMain.Icon = Properties.Resources.brightness1;
            niMain.Text = "Windows 10 Smooth Brightness";
            niMain.ContextMenu = ctxmnuMain;
            niMain.Visible = true;
            Application.Run(new HiddenForm());
            niMain.Visible = false;
        }

        private static void mnuExit_Click(object Sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
