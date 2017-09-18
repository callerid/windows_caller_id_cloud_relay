using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CallerID_Cloud_Relay.Forms
{
    public partial class FrmPopup : Form
    {
        public FrmPopup(string title, string message, bool autoClose = false, int autoCloseMiliSeconds = 0)
        {
            InitializeComponent();

            Text = title;

            tbMessage.Text = message;

            if (!autoClose) return;

            timerAutoClose.Interval = autoCloseMiliSeconds;
            timerAutoClose.Enabled = true;
            timerAutoClose.Start();

            btnOkay.Focus();

        }

        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            timerAutoClose.Stop();
            timerAutoClose.Enabled = false;
            Close();
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
