﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CallerID_Cloud_Relay.Forms;

namespace CallerID_Cloud_Relay.Classes
{
    class Common
    {

        public static void MsgBox(string title, string message, bool autoClose = false, int autoCloseMiliSeconds = 0)
        {
            if (Program.FPopupMessage == null)
            {
                Program.FPopupMessage = new FrmPopup(title, message, autoClose, autoCloseMiliSeconds);
                Program.FPopupMessage.Show();
                return;
            }

            if (Program.FPopupMessage.Visible) return;

            Program.FPopupMessage = new FrmPopup(title, message, autoClose, autoCloseMiliSeconds);
            Program.FPopupMessage.Show();

        }

    }
}
