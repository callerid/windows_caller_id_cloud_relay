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
using System.Net;
using System.IO;

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

        // Patterns for parsing pasted URL
        string linePattern = "([&]?([A-Za-z0-9_-]+)=%Line)";
        string ioPattern = "([&]?([A-Za-z0-9_-]+)=%IO)";
        string sePattern = "([&]?([A-Za-z0-9_-]+)=%SE)";
        string durationPattern = "([&]?([A-Za-z0-9_-]+)=%Duration)";
        string ringTypePattern = "([&]?([A-Za-z0-9_-]+)=%RingType)";
        string ringNumberPattern = "([&]?([A-Za-z0-9_-]+)=%RingNumber)";
        string timePattern = "([&]?([A-Za-z0-9_-]+)=%Time)";
        string phonePattern = "([&]?([A-Za-z0-9_-]+)=%Phone)";
        string namePattern = "([&]?([A-Za-z0-9_-]+)=%Name)";
        string statusPattern = "([&]?([A-Za-z0-9_-]+)=%Status)";
        
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

            // POST TO CLOUD
            string url = rbUseSuppliedUrl.Checked ? tbSuppliedURL.Text : tbGeneratedURL.Text;
            if (rbBasicUnit.Checked)
            {
                if (callParser.IsStartRecord() && !callParser.IsDetailed())
                {
                    PostToUrl(url, ln, callTime, callParser.Phone, callParser.Name, callParser.IOType, callParser.SEType, callParser.DetailType, dur, (callParser.IsDetailed() ? "" : callParser.Ring));
                }
            }
            else
            {
                if (callParser.IsDetailed())
                {
                    if (!string.IsNullOrEmpty(tbStatus.Text))
                    {
                        PostToUrl(url, ln, callParser.CallTime.ToString(), callParser.Phone, callParser.Name, callParser.IOType, callParser.SEType, callParser.DetailType, dur, (callParser.IsDetailed() ? "" : callParser.Ring));
                    }
                }
                else
                {
                    PostToUrl(url, ln, callTime, callParser.Phone, callParser.Name, callParser.IOType, callParser.SEType, callParser.DetailType, dur, (callParser.IsDetailed() ? "" : callParser.Ring));
                }
            }

        }

        //------------------------------------------------------------------------------------Form Functions

        delegate void SetMyTitleCallback(string title);
        public void SetMyTitle(string title)
        {
            if (this.InvokeRequired)
            {
                SetMyTitleCallback d = new SetMyTitleCallback(SetMyTitle);
                this.Invoke(d, new object[] { title });
            }
            else
            {
                Text = title;
            }
        }
        
        public FrmURLSend()
        {
            InitializeComponent();

            // Database Functions
            callLog = new CID_Database();
            LoadLog();

            // Start listener for UDP traffic
            Subscribe(UdpReceiver);
            UdpReceiveThread.IsBackground = true;
            UdpReceiveThread.Start();

            //Fill deluxe checker
            FillDeluxeNames();
            
            // Load old values -----------------------------------------------------------
            ckbRequiresAuthenication.Checked = Properties.Settings.Default.usesAuth;
            tbUsername.Text = Properties.Settings.Default.username;
            tbPassword.Text = Properties.Settings.Default.password;

            rbUseSuppliedUrl.Checked = Properties.Settings.Default.useSupplied;
            rbUseBuiltUrl.Checked = !Properties.Settings.Default.useSupplied;

            rbDeluxeUnit.Checked = Properties.Settings.Default.useDeluxe;
            rbBasicUnit.Checked = !Properties.Settings.Default.useDeluxe;

            tbSuppliedURL.Text = Properties.Settings.Default.suppliedUrl;

            if (!Properties.Settings.Default.builtUrl.Contains("http:") && !Properties.Settings.Default.builtUrl.Contains("www"))
            {
                tbGeneratedURL.Text = "[you must first generate your URL]";
                tbGeneratedURL.ForeColor = Color.Maroon;
            }
            else
            {
                tbGeneratedURL.Text = Properties.Settings.Default.builtUrl;
                tbGeneratedURL.ForeColor = Color.Green;
            }

            tbServer.Text = Properties.Settings.Default.server;

            tbLine.Text = Properties.Settings.Default.line;
            tbTime.Text = Properties.Settings.Default.time;
            tbPhone.Text = Properties.Settings.Default.phone;
            tbName.Text = Properties.Settings.Default.name;
            tbIO.Text = Properties.Settings.Default.io;
            tbSE.Text = Properties.Settings.Default.se;
            tbStatus.Text = Properties.Settings.Default.status;
            tbDuration.Text = Properties.Settings.Default.duration;
            tbRingNumber.Text = Properties.Settings.Default.ringNumber;
            tbRingType.Text = Properties.Settings.Default.ringType;
            //--------------------------------------------------------------------------

            // Call toggle functions
            ToggleDeluxe(new object(), new EventArgs());
            ChangeOfUrlType(new object(), new EventArgs());
            AuthRequriedCheckChange(new object(), new EventArgs());
            ToggleDevelopersSection(rbUseBuiltUrl.Checked);

        }

        private void FrmURLSend_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save all settings
            Properties.Settings.Default.usesAuth = ckbRequiresAuthenication.Checked;
            Properties.Settings.Default.username = tbUsername.Text;
            Properties.Settings.Default.password = tbPassword.Text;

            Properties.Settings.Default.useSupplied = rbUseSuppliedUrl.Checked;
            Properties.Settings.Default.useDeluxe = rbDeluxeUnit.Checked;

            Properties.Settings.Default.suppliedUrl = tbSuppliedURL.Text;

            Properties.Settings.Default.builtUrl = tbGeneratedURL.Text;
            Properties.Settings.Default.server = tbServer.Text;

            Properties.Settings.Default.line = tbLine.Text;
            Properties.Settings.Default.time = tbTime.Text;
            Properties.Settings.Default.phone = tbPhone.Text;
            Properties.Settings.Default.name = tbName.Text;
            Properties.Settings.Default.io = tbIO.Text;
            Properties.Settings.Default.se = tbSE.Text;
            Properties.Settings.Default.status = tbStatus.Text;
            Properties.Settings.Default.duration = tbDuration.Text;
            Properties.Settings.Default.ringNumber = tbRingNumber.Text;
            Properties.Settings.Default.ringType = tbRingType.Text;

            Properties.Settings.Default.Save();

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

        //-----------------------------------------------------------------------------Parsing of Pasted URL

        private void ParseURL(object sender, EventArgs e)
        {
            string fullURL = Clipboard.GetText();

            if (!fullURL.Contains("?"))
            {
                Common.MsgBox("Incorrect format.", Environment.NewLine + Environment.NewLine + "The text on clipboard does not contain a '?' which is required.");
                return;
            }

            var urlParts = fullURL.Split('?');

            string allParams = urlParts[1];

            if (string.IsNullOrEmpty(allParams))
            {
                Common.MsgBox("No Parameters Found.", Environment.NewLine + Environment.NewLine + "The text on clipboard does not contain text after '?'.");
                return;
            }

            int parameters = 0;

            Match m = Regex.Match(allParams, linePattern);
            if (m.Success)
            {
                tbLine.Text = m.Groups[2].Value.ToString();
                parameters++;
            }

            m = Regex.Match(allParams, timePattern);
            if (m.Success)
            {
                tbTime.Text = m.Groups[2].Value.ToString();
                parameters++;
            }

            m = Regex.Match(allParams, phonePattern);
            if (m.Success)
            {
                tbPhone.Text = m.Groups[2].Value.ToString();
                parameters++;
            }

            m = Regex.Match(allParams, namePattern);
            if (m.Success)
            {
                tbName.Text = m.Groups[2].Value.ToString();
                parameters++;
            }

            m = Regex.Match(allParams, ioPattern);
            if (m.Success)
            {
                tbIO.Text = m.Groups[2].Value.ToString();
                parameters++;
            }

            m = Regex.Match(allParams, sePattern);
            if (m.Success)
            {
                tbSE.Text = m.Groups[2].Value.ToString();
                parameters++;
            }

            m = Regex.Match(allParams, statusPattern);
            if (m.Success)
            {
                tbStatus.Text = m.Groups[2].Value.ToString();
                parameters++;
            }

            m = Regex.Match(allParams, durationPattern);
            if (m.Success)
            {
                tbDuration.Text = m.Groups[2].Value.ToString();
                parameters++;
            }

            m = Regex.Match(allParams, ringNumberPattern);
            if (m.Success)
            {
                tbRingNumber.Text = m.Groups[2].Value.ToString();
                parameters++;
            }

            m = Regex.Match(allParams, ringTypePattern);
            if (m.Success)
            {
                tbRingType.Text = m.Groups[2].Value.ToString();
                parameters++;
            }

            if (parameters < 1)
            {
                Common.MsgBox("No Parameters Parsed.", Environment.NewLine + Environment.NewLine + "There were no parameters that could be parsed.");
            }
            else
            {
                Common.MsgBox("Paste Complete.", Environment.NewLine + Environment.NewLine + "Clipboard text succesfully parsed into your Developer Section.", true, 1500);
                tbSuppliedURL.Text = fullURL;
            }

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

        //-------------------------------------------------------------------------------Generate URL Section

        private void BtnGenerateURL_Click(object sender, EventArgs e)
        {
            StringBuilder genUrl = new StringBuilder();

            if (string.IsNullOrEmpty(tbServer.Text))
            {

                tbGeneratedURL.Text = "[previous generation failed - fill out server]";
                tbGeneratedURL.ForeColor = Color.Maroon;
                tbServer.BackColor = Color.Pink;

                Common.MsgBox("Server Cannot be blank.", Environment.NewLine + Environment.NewLine + "Please input your Cloud Server.");
                return;
            }

            genUrl.Append(tbServer.Text + "?");
            tbServer.BackColor = Color.Honeydew;

            int parameters = 0;
            
            // Line
            if (!string.IsNullOrEmpty(tbLine.Text))
            {
                parameters++;
                genUrl.Append(tbLine.Text + "=%Line&");
            }

            // Time
            if (!string.IsNullOrEmpty(tbTime.Text))
            {
                parameters++;
                genUrl.Append(tbTime.Text + "=%Time&");
            }

            // Phone
            if (!string.IsNullOrEmpty(tbPhone.Text))
            {
                parameters++;
                genUrl.Append(tbPhone.Text + "=%Phone&");
            }

            // Name
            if (!string.IsNullOrEmpty(tbName.Text))
            {
                parameters++;
                genUrl.Append(tbName.Text + "=%Name&");
            }

            // IO
            if (!string.IsNullOrEmpty(tbIO.Text))
            {
                parameters++;
                genUrl.Append(tbIO.Text + "=%IO&");
            }

            // SE
            if (!string.IsNullOrEmpty(tbSE.Text))
            {
                parameters++;
                genUrl.Append(tbSE.Text + "=%SE&");
            }

            // Status
            if (!string.IsNullOrEmpty(tbStatus.Text))
            {
                parameters++;
                genUrl.Append(tbStatus.Text + "=%Status&");
            }

            // Duration
            if (!string.IsNullOrEmpty(tbDuration.Text))
            {
                parameters++;
                genUrl.Append(tbDuration.Text + "=%Duration&");
            }

            // RingNumber
            if (!string.IsNullOrEmpty(tbRingNumber.Text))
            {
                parameters++;
                genUrl.Append(tbRingNumber.Text + "=%RingNumber&");
            }

            // RingType
            if (!string.IsNullOrEmpty(tbRingType.Text))
            {
                parameters++;
                genUrl.Append(tbRingType.Text + "=%RingType&");
            }

            if (parameters == 0)
            {
                tbGeneratedURL.Text = "[previous generation failed - use at least one parameter]";
                tbGeneratedURL.ForeColor = Color.Maroon;

                Common.MsgBox("No Parameters Set", Environment.NewLine + Environment.NewLine + "You must use at least one parameter.");
                return;
            }

            tbGeneratedURL.Text = genUrl.ToString().Substring(0, genUrl.ToString().Length - 1);
            tbGeneratedURL.ForeColor = Color.Green;

        }

        private void BtnCopyBuiltURL_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbGeneratedURL.Text);
        }

        private void TimerShowBoundPort_Tick(object sender, EventArgs e)
        {
            timerShowBoundPort.Stop();
            timerShowBoundPort.Enabled = false;
            SetMyTitle("Caller ID Cloud Relay - " + Application.ProductVersion.ToString() + " - Listening on Port: " + UdpReceiverClass.BoundTo);
        }

        // -----------------------------------------------------------------------------Actual Posting of URL

        private void PostToUrl(string urlFull, string line, string dateTime, string number, string name, string io,
            string se, string status, string duration, string ring)
        {

            if(!urlFull.Contains("?"))
            {
                Common.MsgBox("Error Parsing URL", Environment.NewLine + Environment.NewLine + "Could not parse URL. There is no separation between URL and Params.");
                return;
            }
            
            var parts = urlFull.Split('?');

            string url = parts[0];
            string postData = parts[1];

            // Replace all params
            postData = postData.Replace("%Line", line);
            postData = postData.Replace("%Time", dateTime);
            postData = postData.Replace("%Phone", number);
            postData = postData.Replace("%Name", name);
            postData = postData.Replace("%IO", io);
            postData = postData.Replace("%SE", se);
            postData = postData.Replace("%Status", status);
            postData = postData.Replace("%Duration", duration);

            string ringType = "";
            string ringNumber = "";
            if (ring.Length == 2)
            {
                ringType = ring.Substring(0, 1);
                ringNumber = ring.Substring(1, 1);
            }            

            postData = postData.Replace("%RingNumber", ringNumber);
            postData = postData.Replace("%RingType", ringType);

            var data = Encoding.ASCII.GetBytes(postData);

            var request = (HttpWebRequest)WebRequest.Create(url + "?" + postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            if (ckbRequiresAuthenication.Checked)
            {
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tbUsername.Text + ":" + tbPassword.Text));
                request.Headers.Add("Authorization", "Basic " + encoded);
            }
            
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Console.WriteLine(responseString);
        }

        private void BtnTestSuppliedURL_Click(object sender, EventArgs e)
        {

            string url = rbUseSuppliedUrl.Checked ? tbSuppliedURL.Text : tbGeneratedURL.Text;

            PostToUrl(url, "01", "01/01 12:00 PM", "770-263-7111", "CallerID.com", "I", "S", "n/a", "0000", "A0");

        }

    }
}
