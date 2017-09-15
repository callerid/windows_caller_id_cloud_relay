using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CallerID_Cloud_Relay
{
    static class Program
    {
        public static FrmURLSend FUrlSend;


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FUrlSend = new FrmURLSend();
            Application.Run(FUrlSend);
        }
    }
}
