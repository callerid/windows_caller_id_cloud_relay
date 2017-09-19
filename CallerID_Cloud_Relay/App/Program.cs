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

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FUrlSend = new FrmURLSend();
            Application.Run();
        }
    }
}
