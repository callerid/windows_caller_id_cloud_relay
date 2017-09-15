using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cid_cm.Classes;
using System.Threading;
using System.Text.RegularExpressions;
using CallerID_Cloud_Relay.Classes;

namespace CallerID_Cloud_Relay
{
    public partial class FrmURLSend : Form
    {
        //----------------------------------------------------------------------------------Needed variables

        // Database
        CID_Database callLog;

        // Names of all Deluxe controls
        private List<string> deluxeNames = new List<string>();

        // Log Column Indexes
        const int logColLine = 0;
        const int logColIO = 1;
        const int logColSE = 2;
        const int logColDur = 3;
        const int logColRing = 4;
        const int logColDateTime = 5;
        const int logColNumber = 6;
        const int logColName = 7;
        
        // --------------------------------------------------UDP Receiever and Threading - Capturing as well

        public static UdpReceiverClass UdpReceiver = new UdpReceiverClass();
        readonly Thread UdpReceiveThread = new Thread(UdpReceiver.UdpIdleReceive);

        public void Subscribe(UdpReceiverClass u)
        {
            // If UDP event occurs run HeardIt method
            u.DataReceived += HeardIt;
        }

        private void HeardIt(UdpReceiverClass u, EventArgs e)
        {
            // HELP : example how to call method
            // Invoke((MethodInvoker)(() => methodName()));
            Invoke((MethodInvoker)(GetCall));

        }

        private void GetCall()
        {
            var callParser = new CID_Parser.CallRecord(UdpReceiverClass.ReceivedMessage);

            // Reset to correct time, CID_Parser does not always get correct time
            Match matchDateTime = Regex.Match(UdpReceiverClass.ReceivedMessage, @".*(\d\d) ([IO]) ([ESB]) (\d{4}) ([GB]) (.)(\d) (\d\d/\d\d \d\d:\d\d [AP]M) (.{8,15})(.*)");
            string callTime = "01/01 12:00 PM";
            if (matchDateTime.Success)
            {
                callTime = matchDateTime.Groups[8].Value.ToString();
            }

            // Make sure UDP traffic is a CallerID.com call record
            if (callParser.Line < 1) return;

            // ----------------------------------------------------
            //                   Add Call To Log
            // ----------------------------------------------------
            string ln = callParser.Line.ToString();
            if (ln.Length == 1) ln = "0" + ln;
            string dur = callParser.Duration.ToString();
            while (dur.Length < 4)
            {
                dur = "0" + dur;
            }

            if (callParser.IsDetailed())
            {

                AddToLog(ln, callParser.CallTime.ToString(), "", "", callParser.DetailType, "", "", "", "");
            }
            else
            {
                AddToLog(ln, callTime, callParser.Phone, callParser.Name, callParser.IOType, callParser.SEType, callParser.DetailType, dur, callParser.Ring);
            }

        }

        //------------------------------------------------------------------------------------Form Functions

        public void SetMyTitle(string title)
        {
            Text = title;
        }

        public FrmURLSend()
        {
            InitializeComponent();

            // Database Functions
            callLog = new CID_Database();
            LoadLog();

            Text = "CallerID.com Cloud Relay - " + Application.ProductVersion.ToString();

            // Start listener for UDP traffic
            Subscribe(UdpReceiver);
            UdpReceiveThread.IsBackground = true;
            UdpReceiveThread.Start();

            //Fill deluxe checker
            FillDeluxeNames();
            
            // Load old values

            // -- TODO

            // Call toggle functions
            ToggleDeluxe(new object(), new EventArgs());
            ChangeOfUrlType(new object(), new EventArgs());
            AuthRequriedCheckChange(new object(), new EventArgs());
            ToggleDevelopersSection(rbUseBuiltUrl.Checked);

        }

        //------------------------------------------------------------------------------------Toggling of UI

        private void ToggleDevelopersSection(bool isEnabled)
        {
            // Enable/Disable all developers section controls
            foreach (Control ctrl in gbDevSection.Controls)
            {
                if (rbBasicUnit.Checked)
                {
                    if (IsFoundInDeluxeNames(ctrl)) continue;
                }
                ctrl.Enabled = isEnabled;
            }
            gbDevSection.Enabled = isEnabled;

            // Enable/disable all supplied section controls
            foreach (Control ctrl in gbSuppliedUrl.Controls)
            {
                ctrl.Enabled = !isEnabled;
            }
            gbSuppliedUrl.Enabled = !isEnabled;

        }

        private void ChangeOfUrlType(object sender, EventArgs e)
        {
            ToggleDevelopersSection(rbUseBuiltUrl.Checked);
        }

        private void ToggleDeluxe(object sender, EventArgs e)
        {

            lbIO.Enabled = rbDeluxeUnit.Checked;
            lbIOD.Enabled = rbDeluxeUnit.Checked;
            tbIO.Enabled = rbDeluxeUnit.Checked;

            lbSE.Enabled = rbDeluxeUnit.Checked;
            lbSED.Enabled = rbDeluxeUnit.Checked;
            tbSE.Enabled = rbDeluxeUnit.Checked;

            lbStatus.Enabled = rbDeluxeUnit.Checked;
            lbStatusD.Enabled = rbDeluxeUnit.Checked;
            tbStatus.Enabled = rbDeluxeUnit.Checked;

            lbDuration.Enabled = rbDeluxeUnit.Checked;
            lbDurationD.Enabled = rbDeluxeUnit.Checked;
            tbDuration.Enabled = rbDeluxeUnit.Checked;

            lbRingNumber.Enabled = rbDeluxeUnit.Checked;
            lbRingNumberD.Enabled = rbDeluxeUnit.Checked;
            tbRingNumber.Enabled = rbDeluxeUnit.Checked;

            lbRingType.Enabled = rbDeluxeUnit.Checked;
            lbRingTypeD.Enabled = rbDeluxeUnit.Checked;
            tbRingType.Enabled = rbDeluxeUnit.Checked;

        }

        private void AuthRequriedCheckChange(object sender, EventArgs e)
        {
            tbUsername.Enabled = ckbRequiresAuthenication.Checked;
            tbPassword.Enabled = ckbRequiresAuthenication.Checked;
            lbUsername.Enabled = ckbRequiresAuthenication.Checked;
            lbPassword.Enabled = ckbRequiresAuthenication.Checked;
        }

        //---------------------------------------------------------------------------Deluxe Needed Variables

        private void FillDeluxeNames()
        {
            deluxeNames = new List<string> 
            { lbIO.Name, lbIOD.Name, tbIO.Name,
                lbSE.Name,lbSED.Name,tbSE.Name,
                lbStatus.Name,lbStatusD.Name,tbStatus.Name,
                lbDuration.Name,lbDurationD.Name,tbDuration.Name,
                lbRingNumber.Name,lbRingNumberD.Name,tbRingNumber.Name,
                lbRingType.Name,lbRingTypeD.Name,tbRingType.Name
            };
        }

        private bool IsFoundInDeluxeNames(Control ctrl)
        {
            return deluxeNames.Contains(ctrl.Name);
        }

        //-----------------------------------------------------------------------------------Button Coloring

        private void HoverOnButton(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.PaleGreen;
        }

        private void LeaveHoverOnButton(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.MintCream;
        }

        //-------------------------------------------------------------------------------------Log Functions
        
        private void AddToLog(string line,string dateTime, string number, string name, string io, 
            string se, string status, string duration, string ring, bool addToSQL = true)
        {
            dgvLog.Rows.Add();

            // Insert all values
            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColLine].Value = line;
            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColIO].Value = io;
            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColSE].Value = se;
            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColDur].Value = duration;
            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColRing].Value = ring;
            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColDateTime].Value = dateTime;
            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColNumber].Value = number;
            dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColName].Value = name;

            // Change font to smaller font
            dgvLog.Rows[dgvLog.Rows.Count - 1].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);

            // Auto Scroll
            dgvLog.FirstDisplayedScrollingRowIndex = dgvLog.RowCount - 1;

            // Add to SQL database
            if (addToSQL) callLog.InsertIntoLog(line, dateTime, number, name, io, se, status, duration, ring);

        }

        private void LoadLog()
        {
            DataTable logData = new DataTable();
            logData = callLog.LoadLog();

            if (logData == null)
            {
                Console.WriteLine("No log to load.");
                return;
            }
            
            foreach (DataRow row in logData.Rows)
            {

                string line = row["line"].ToString();
                string io = row["io"].ToString();
                string se = row["se"].ToString();
                string duration = row["duration"].ToString();
                string ring = row["ring"].ToString();
                string theDateTime = row["theDateTime"].ToString();
                string number = row["number"].ToString();
                string name = row["name"].ToString();

                if (io != "I" && io != "O")
                {
                    // Detailed
                    AddToLog(line, theDateTime, "", "", io, "", "", "", "", false);
                }
                else
                {
                    // Call
                    AddToLog(line, theDateTime, number, name, io, se, io, duration, ring, false);
                }

            }
        }


    }
}
