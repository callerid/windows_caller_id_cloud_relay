using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CallerID_Cloud_Relay.Forms;

namespace CallerID_Cloud_Relay
{
    static class Program
    {
        public static FrmURLSend FUrlSend;
        public static FrmPopup FPopupMessage;
        public static string ErrorLogFile = Application.StartupPath + "\\error_log.txt";

        [STAThread]
        static void Main()
        {
            int numberOfInstances = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Length;
            
            if(numberOfInstances > 1)
            {
                FrmPopup fClosing = new FrmPopup("Already Running", Environment.NewLine + Environment.NewLine + "Cloud Relay already running.", true, 4000);
                fClosing.ShowDialog();
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FUrlSend = new FrmURLSend();
            Application.Run();
        }
    }
}
