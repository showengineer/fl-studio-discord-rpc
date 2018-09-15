using System;
using System.Threading;
using System.Windows.Forms;
using FLRPC;

namespace FLRPC_GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread t = new Thread(FL_RPC.Init);
            t.Start();
            using (NotifyIcon icon = new NotifyIcon())
        {
            icon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            icon.ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Show form", (s, e) => {new Form1().Show();}),
                new MenuItem("Exit", (s, e) => { Application.Exit(); }),
            });
            icon.Visible = true;

            Application.Run();
            icon.Visible = false;
        }
        }
    }
}
