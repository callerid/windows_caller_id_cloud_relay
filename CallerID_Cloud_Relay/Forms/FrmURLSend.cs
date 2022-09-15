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
using System.Globalization;
using System.Diagnostics;

namespace CallerID_Cloud_Relay
{
    public partial class FrmURLSend : Form
    {
        //----------------------------------------------------------------------------------Needed variables

        // Database
        CID_Database callLog;

        // URL file
        private string URLDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\CallerID.com\\CloudRelay\\");
        private string URLFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\CallerID.com\\CloudRelay\\cid.dat");

        // URL Format
        private int SL_HIDE = 0;
        private int SL_OVERFLOW = 1;
        private int SL_AUTH = 2;
        private int SL_USER = 3;
        private int SL_PASS = 4;
        private int SL_SUPPLIED = 5;
        private int SL_DELUXE = 6;
        private int SL_SUPPLIED_URL = 7;
        private int SL_BUILT_URL = 8;
        private int SL_SERVER = 9;
        private int SL_LINE = 10;
        private int SL_TIME = 11;
        private int SL_PHONE = 12;
        private int SL_NAME = 13;
        private int SL_IO = 14;
        private int SL_SE = 15;
        private int SL_STATUS = 16;
        private int SL_DUR = 17;
        private int SL_RINGNUM = 18;
        private int SL_RINGTYPE = 19;
        private int SL_SYS_TRAY = 20;

        private int SL_COUNT = 21;

        private bool UseSystemTray = false;
        // ---------------------

        // Names of all Deluxe controls
        private List<string> deluxeNames = new List<string>();

        // Call Record reception <reception_string, seconds_since_it_came_in>
        Dictionary<string, int> previousReceptions = new Dictionary<string, int>();
        
        // Log Column Indexes
        const int logColLine = 0;
        const int logColIO = 1;
        const int logColSE = 2;
        const int logColDur = 3;
        const int logColRing = 4;
        const int logColDateTime = 5;
        const int logColNumber = 6;
        const int logColName = 7;
        const int logColS = 8;
        const int logColLogID = 9;
        const int logColText = 10;

        public bool Kill;

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
            if(InvokeRequired)
            {
                Invoke((MethodInvoker)(GetCall));
            }
            else
            {

                string reception = UdpReceiverClass.ReceivedMessage;
                CallRecord cRecord = new CallRecord(UdpReceiverClass.ReceivedMessage);

                string log_id = GetNewLogID(cRecord);
                AddToCallLog(cRecord, log_id, false);
                PostCallToCloud(cRecord, log_id);

            }

        }

        private void GetCall()
        {

            string reception = UdpReceiverClass.ReceivedMessage;

            // ------------------------------------------------------------------------
                        
            CallRecord cRecord = new CallRecord(UdpReceiverClass.ReceivedMessage);

            if (!cRecord.IsValid) return;
            if (IsDuplicate(cRecord)) return;

            string log_id = GetNewLogID(cRecord);

            // Add to log
            AddToCallLog(cRecord, log_id);
            Common.WriteToCallLog(cRecord.Reception);

            // POST TO CLOUD
            PostCallToCloud(cRecord, log_id);

        }

        private void PostCallToCloud(CallRecord cRecord, string log_id)
        {
            string ln = cRecord.Line.ToString();
            if (ln.Length == 1) ln = "0" + ln;
            string dur = cRecord.Duration.ToString();
            while (dur.Length < 4)
            {
                dur = "0" + dur;
            }

            string url = rbUseSuppliedUrl.Checked ? tbSuppliedURL.Text : tbGeneratedURL.Text;

            int hour = int.Parse(cRecord.DateTime.Hour.ToString());

            if (hour > 12) hour = hour - 12;
            if (hour == 0) hour = 12;
            string formattedTime = cRecord.DateTime.Month.ToString().PadLeft(2, '0') + "/" + cRecord.DateTime.Day.ToString().PadLeft(2, '0') + " " +
                hour.ToString().PadLeft(2, '0') + ":" + cRecord.DateTime.Minute.ToString().PadLeft(2, '0') + " " + cRecord.DateTime.ToString("tt", CultureInfo.InvariantCulture);

            if (rbBasicUnit.Checked)
            {
                if (cRecord.IsStartRecord() && !cRecord.Detailed)
                {
                    PostToUrl(url, ln, formattedTime, cRecord.PhoneNumber, cRecord.Name, cRecord.InboundOrOutboundOrBlock, cRecord.StartOrEnd, cRecord.DetailedType, dur, (cRecord.Detailed ? "" : cRecord.RingType.ToString() + cRecord.RingNumber.ToString()), log_id);
                }
            }
            else
            {
                if (cRecord.Detailed)
                {
                    formattedTime = cRecord.DateTime.Month.ToString().PadLeft(2, '0') + "/" + cRecord.DateTime.Day.ToString().PadLeft(2, '0') + " " +
                    hour.ToString().PadLeft(2, '0') + ":" + cRecord.DateTime.Minute.ToString().PadLeft(2, '0') + ":" + cRecord.DateTime.Second.ToString().PadLeft(2, '0');

                    if (!string.IsNullOrEmpty(tbStatus.Text))
                    {
                        PostToUrl(url, ln, formattedTime, cRecord.PhoneNumber, cRecord.Name, cRecord.InboundOrOutboundOrBlock, cRecord.StartOrEnd, cRecord.DetailedType, dur, (cRecord.Detailed ? "" : cRecord.RingType.ToString() + cRecord.RingNumber.ToString()), log_id);
                    }
                }
                else
                {
                    PostToUrl(url, ln, formattedTime, cRecord.PhoneNumber, cRecord.Name, cRecord.InboundOrOutboundOrBlock, cRecord.StartOrEnd, cRecord.DetailedType, dur, (cRecord.Detailed ? "" : cRecord.RingType.ToString() + cRecord.RingNumber.ToString()), log_id);
                }
            }
        }
        
        private string GetNewLogID(CallRecord cRecord)
        {
            return "i" + cRecord.Line + cRecord.PhoneNumber + DateTime.Now.Second;
        }

        private void AddToCallLog(CallRecord cRecord, string log_id, bool show = true)
        {
            string ln = cRecord.Line.ToString();
            if (ln.Length == 1) ln = "0" + ln;
            string dur = cRecord.Duration.ToString();
            while (dur.Length < 4)
            {
                dur = "0" + dur;
            }

            if (cRecord.Detailed)
            {
                AddToLog(ln, cRecord.DateTime.ToString(), "", "", cRecord.DetailedType, "", "", "", "", true, log_id, show);
            }
            else
            {
                AddToLog(ln, cRecord.DateTime.ToString(), cRecord.PhoneNumber, cRecord.Name, cRecord.InboundOrOutboundOrBlock, cRecord.StartOrEnd, cRecord.DetailedType, dur, cRecord.RingType.ToString() + cRecord.RingNumber.ToString(), true, log_id, show);
            }
        }

        private bool IsDuplicate(CallRecord cRecord)
        {
            string reception = cRecord.Reception;

            if (previousReceptions.ContainsKey(reception))
            {
                if (previousReceptions[reception] < 60) return true;
            }
            else
            {

                if (previousReceptions.Count > 30)
                {
                    previousReceptions.Add(reception, 0);

                    string removeKey = "";
                    foreach (string key in previousReceptions.Keys)
                    {
                        removeKey = key;
                        break;
                    }

                    if (!string.IsNullOrEmpty(removeKey))
                    {
                        previousReceptions.Remove(removeKey);
                    }

                }
                else
                {
                    previousReceptions.Add(reception, 0);
                }
            }

            return false;
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
        
        private void CloseProgramFromSysTray(object sender, EventArgs e)
        {
            sysTray.Visible = false;
            Kill = true;
            Close();
            Application.Exit();
            return;
        }

        public FrmURLSend()
        {
            InitializeComponent();

            if(!Directory.Exists(Application.StartupPath + "\\logs\\"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\logs\\");
            }

            ContextMenu sysTrayMenu = new ContextMenu();
            MenuItem itemClose = new MenuItem();
            itemClose.Text = "Exit Cloud Relay";
            itemClose.Click += CloseProgramFromSysTray;
            sysTrayMenu.MenuItems.Add(itemClose);
            sysTray.ContextMenu = sysTrayMenu;

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
            bool readIn = false;
            if (!Directory.Exists(URLDirectory))
            {
                Directory.CreateDirectory(URLDirectory);
            }

            tbGeneratedURL.Text = "[you must first generate your URL]";
            tbGeneratedURL.ForeColor = Color.Maroon;

            if (File.Exists(URLFile))
            {
                string[] lines = File.ReadAllLines(URLFile);

                if(lines.Length == SL_COUNT)
                {
                    UseSystemTray = lines[SL_SYS_TRAY] == "True";
                    ckbHideInSystemTray.Checked = UseSystemTray;

                    ckbRequiresAuthenication.Checked = lines[SL_AUTH] == "True";
                    ckbIgnoreChannelOverflow.Checked = lines[SL_OVERFLOW] == "True";
                    tbUsername.Text = lines[SL_USER];
                    tbPassword.Text = lines[SL_PASS];

                    rbUseSuppliedUrl.Checked = lines[SL_SUPPLIED] == "True";
                    rbUseBuiltUrl.Checked = lines[SL_SUPPLIED] != "True";

                    rbDeluxeUnit.Checked = lines[SL_DELUXE] == "True";
                    rbBasicUnit.Checked =  lines[SL_DELUXE] != "True";

                    tbSuppliedURL.Text = lines[SL_SUPPLIED_URL];
                    tbGeneratedURL.Text = lines[SL_BUILT_URL];

                    if (!(!tbGeneratedURL.Text.Contains("http") && !tbGeneratedURL.Text.Contains("www")))
                    {
                        tbGeneratedURL.ForeColor = Color.Green;
                    }

                    tbServer.Text = lines[SL_SERVER];

                    tbLine.Text = lines[SL_LINE];
                    tbTime.Text = lines[SL_TIME];
                    tbPhone.Text = lines[SL_PHONE];
                    tbName.Text = lines[SL_NAME];
                    tbIO.Text = lines[SL_IO];
                    tbSE.Text = lines[SL_SE];
                    tbStatus.Text = lines[SL_STATUS];
                    tbDuration.Text = lines[SL_DUR];
                    tbRingNumber.Text = lines[SL_RINGNUM];
                    tbRingType.Text = lines[SL_RINGTYPE];

                    readIn = true;
                }
            }
            
            if (!readIn)
            {
                tbSuppliedURL.Text = "";

                if (!tbGeneratedURL.Text.Contains("http") && !tbGeneratedURL.Text.Contains("www"))
                {
                    tbGeneratedURL.Text = "[you must first generate your URL]";
                    tbGeneratedURL.ForeColor = Color.Maroon;
                }
                else
                {
                    tbGeneratedURL.ForeColor = Color.Green;
                }
            }

            //--------------------------------------------------------------------------

            // Call toggle functions
            ToggleDeluxe(new object(), new EventArgs());
            ChangeOfUrlType(new object(), new EventArgs());
            AuthRequriedCheckChange(new object(), new EventArgs());
            ToggleDevelopersSection(rbUseBuiltUrl.Checked);

            if(Screen.FromControl(this).Bounds.Height <= 768)
            {
                panScrollArea.Height = 190;
                gbLog.Location = new Point(gbLog.Location.X, gbLog.Location.Y - 50);
                btnClearLog.Location = new Point(btnClearLog.Location.X, btnClearLog.Location.Y - 50);
            }

            sysTray.Visible = true;
            sysTray.ShowBalloonTip(500);

        }

        private void FrmURLSend_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Directory.Exists(URLDirectory))
            {
                Directory.CreateDirectory(URLDirectory);
            }

            string[] lines = new string[SL_COUNT];

            lines[SL_HIDE] = ckbHideInSystemTray.Checked ? "True" : "False";
            lines[SL_OVERFLOW] = ckbIgnoreChannelOverflow.Checked ? "True" : "False";
            
            lines[SL_AUTH] = ckbRequiresAuthenication.Checked ? "True" : "False";
            lines[SL_USER] = tbUsername.Text;
            lines[SL_PASS] = tbPassword.Text;
            
            lines[SL_SUPPLIED] = rbUseSuppliedUrl.Checked ? "True" : "False";
            lines[SL_DELUXE] = rbDeluxeUnit.Checked ? "True" : "False";
            
            lines[SL_SUPPLIED_URL] = tbSuppliedURL.Text;
            lines[SL_BUILT_URL] = tbGeneratedURL.Text;
            
            lines[SL_SERVER] = tbServer.Text;
            
            lines[SL_LINE] = tbLine.Text;
            lines[SL_TIME] = tbTime.Text;
            lines[SL_PHONE] = tbPhone.Text;
            lines[SL_NAME] = tbName.Text;
            lines[SL_IO] = tbIO.Text;
            lines[SL_SE] = tbSE.Text;
            lines[SL_STATUS] = tbStatus.Text;
            lines[SL_DUR] = tbDuration.Text;
            lines[SL_RINGNUM] = tbRingNumber.Text;
            lines[SL_RINGTYPE] = tbRingType.Text;

            lines[SL_SYS_TRAY] = ckbHideInSystemTray.Checked ? "True" : "False";

            try
            {
                File.WriteAllLines(URLFile, lines.ToArray());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Settings could not be saved. Error: " + ex.ToString());
                return;
            }
            // -------------------------------------------

            if (ckbHideInSystemTray.Checked && !Kill)
            {
                GotoBackground();
                e.Cancel = true;
            }

        }

        private void FrmURLSend_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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

            if (rbUseSuppliedUrl.Checked)
            {
                btnTestSuppliedURL.Text = "Test Supplied URL";
                rbBasicUnit.Enabled = false;
                rbDeluxeUnit.Enabled = false;
            }
            else
            {
                btnTestSuppliedURL.Text = "Test Built URL";
                rbBasicUnit.Enabled = true;
                rbDeluxeUnit.Enabled = true;
            }
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
        
        //-------------------------------------------------------------------------------------Log Functions
        
        private void AddToLog(string line,string dateTime, string number, string name, string io, 
            string se, string status, string duration, string ring, bool addToSQL = true, string log_id = "", bool show = true)
        {
            if (show)
            {
                dgvLog.Rows.Add();

                // Insert all values
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColLine].Value = line;
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColIO].Value = io;
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColSE].Value = se;
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColDur].Value = duration;
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColRing].Value = ring;
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColDateTime].Value = dateTime;
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColName].Value = name;
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColNumber].Value = number;
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColS].Value = "L";
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColLogID].Value = log_id;
                dgvLog.Rows[dgvLog.Rows.Count - 1].Cells[logColText].Value = "";

                // Change font to smaller font
                dgvLog.Rows[dgvLog.Rows.Count - 1].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);

                // Auto Scroll
                dgvLog.FirstDisplayedScrollingRowIndex = dgvLog.RowCount - 1;
            }

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

            dgvLog.Rows.Clear();
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
                    AddToLog(line, theDateTime, "", "", io, "", "", "", "", false, "");
                }
                else
                {
                    // Call
                    AddToLog(line, theDateTime, number, name, io, se, io, duration, ring, false, "");
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
            tbServer.BackColor = Color.White;

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
            btnGenerateURL.BackColor = SystemColors.Control;
            lbSuccessfulGen.Visible = true;
            timerHideGenerateSuccess.Enabled = true;
            timerHideGenerateSuccess.Start();

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

        //------------------------------------------------------------------------------Actual Posting of URL

        private void PostToUrl(string urlFull, string line, string dateTime, string number, string name, string io,
            string se, string status, string duration, string ring, string log_id)
        {

            if(!urlFull.Contains("?"))
            {
                Common.MsgBox("Error Parsing URL", Environment.NewLine + Environment.NewLine + "Could not parse URL. There is no separation between URL and Params. Or empty URL");
                Common.WriteToLog("Error parsing URL. No '?' found in Post.");
                return;
            }

            if(number != null && !string.IsNullOrEmpty(number))
            {
                string num = number.Trim();
                num = num.Substring(num.Length - 2);
                if (num == "xx" && ckbIgnoreChannelOverflow.Checked)
                {
                    number = "000-000-0000";
                }
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
            HttpWebRequest request = null;
            bool sent = false;
            if (!ckbRequiresAuthenication.Checked)
            {
                WebClient client = new WebClient();
                string log = "";
                try
                {
                    log = client.DownloadString(url + "?" + postData);
                    UpdateDGVWithLogID(log_id, log);
                    sent = true;
                }
                catch (Exception ex)
                {
                    Common.WriteToLog(ex.ToString());
                    //Common.MsgBox("Invalid URL", Environment.NewLine + Environment.NewLine + "Url is not in valid format.", false, 4000);
                    Common.WriteToLog("Url is not in valid format.");
                }
            }

            if (!sent)
            {
                try
                {
                    request = (HttpWebRequest)WebRequest.Create(url + "?" + postData);
                }
                catch (Exception ex)
                {
                    Common.WriteToLog(ex.ToString());
                    //Common.MsgBox("Invalid URL", Environment.NewLine + Environment.NewLine + "Url is not in valid format.", false, 4000);
                    Common.WriteToLog("Url is not in valid format.");
                    
                }

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                if (ckbRequiresAuthenication.Checked)
                {
                    String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(tbUsername.Text + ":" + tbPassword.Text));
                    request.Headers.Add("Authorization", "Basic " + encoded);
                }

                string response_string = "";
                HttpWebResponse response = null;
                try
                {
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    response = (HttpWebResponse)request.GetResponse();

                    response_string = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    Console.WriteLine(response_string);
                    UpdateDGVWithLogID(log_id, response_string);

                    response.Close();

                }
                catch (Exception ex)
                {
                    Common.MsgBox("URL Error", Environment.NewLine + "Error connecting to server." + Environment.NewLine + Environment.NewLine + ex.ToString(), false, 5000);

                    Common.WriteToLog("URL ERROR: " + ex.ToString());
                }
            }
        }

        private void UpdateDGVWithLogID(string log_id, string text)
        {
            int index = -1;
            foreach(DataGridViewRow r in dgvLog.Rows)
            {
                if(r.Cells[logColLogID].Value.ToString() == log_id)
                {
                    index = r.Index;
                    break;
                }
            }

            if (index == -1) return;

            dgvLog.Rows[index].Cells[logColText].Value = text;

        }

        private void BtnTestSuppliedURL_Click(object sender, EventArgs e)
        {

            string url = rbUseSuppliedUrl.Checked ? tbSuppliedURL.Text : tbGeneratedURL.Text;

            PostToUrl(url, "01", "01/01 12:00 PM", "770-263-7111", "CallerID.com", "I", "S", "n/a", "0000", "A0", "");

            if (rbUseSuppliedUrl.Checked)
            {
                Common.MsgBox("Example Call Sent to Supplied URL", Environment.NewLine + Environment.NewLine + "An example Start of call record was sent to the Supplied URL.", true, 3000);
            }
            else
            {
                Common.MsgBox("Example Call Sent to Built URL", Environment.NewLine + Environment.NewLine + "An example Start of call record was sent to your custom built URL.", true, 3000);
            }

        }

        //-----------------------------------------------------------------------------------------------Misc

        private void TimerHideGenerateSuccess_Tick(object sender, EventArgs e)
        {
            timerHideGenerateSuccess.Enabled = false;
            timerHideGenerateSuccess.Stop();
            lbSuccessfulGen.Visible = false;
        }

        private void ParamLeaveFocus(object sender, EventArgs e)
        {
            btnGenerateURL.BackColor = Color.Pink;
        }

        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to clear the entire Call Log?", "Clear Log?", MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (callLog.ClearLog())
            {
                Common.MsgBox("Call Log Cleared.", Environment.NewLine + Environment.NewLine + "Call Log cleared successfully.", true, 1500);
                dgvLog.Rows.Clear();
            }
            else
            {
                Common.MsgBox("Call Log Failed to Clear.", Environment.NewLine + Environment.NewLine + "Failed to clear Call Log.");
            }
        }

        private void GotoBackground()
        {
            this.Hide();
        }

        private void BringToForeground(object sender, MouseEventArgs e)
        {
            Program.FUrlSend.Show();
            Program.FUrlSend.WindowState = FormWindowState.Normal;
            LoadLog();
        }

        private void TimerSySTrayHide_Tick(object sender, EventArgs e)
        {
            timerSySTrayHide.Enabled = false;
            timerSySTrayHide.Stop();

            // Load system tray setting
            ckbHideInSystemTray.Checked = UseSystemTray;
            if (!ckbHideInSystemTray.Checked)
            {
                BringToForeground(new object(), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0));
            }
            else
            {
                GotoBackground();
            }
        }

        private void CkbHideInSystemTray_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.hideInSystemTray = ckbHideInSystemTray.Checked;
            Properties.Settings.Default.Save();
        }

        private void timerDuplicateHandling_Tick(object sender, EventArgs e)
        {
            // DUPLICATES CODING START
            // This timer is used to increment all seconds
            // of the previous receptions and remove them 
            // after 4 seconds have passed.

            // If there is nothing in the reception buffer then simply exit function
            if (previousReceptions.Count < 1) return;

            // Create needed lists
            List<string> keysToRemove = new List<string>();
            List<string> keysToIncrement = new List<string>();

            // Loop through previously received call records
            // and mark the ones which need to be removed and
            // mark the ones to increment the seconds on
            foreach (string key in previousReceptions.Keys)
            {
                if (previousReceptions[key] > 4) // remove after 4 seconds
                {
                    // This reception will be removed
                    keysToRemove.Add(key);
                }
                else
                {
                    // This reception has no waited another second
                    keysToIncrement.Add(key);
                }
            }

            // Increment the second of all needed receptions in buffer
            foreach (string key in keysToIncrement)
            {
                previousReceptions[key]++;
            }

            // Remove all receptions in buffer that are past the time limit (2 seconds)
            foreach (string key in keysToRemove)
            {
                previousReceptions.Remove(key);
            }
            // DUPLICATES CODING END
        }

        private void dgvLog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (row == -1 || col == -1) return;

            if(col == logColS)
            {
                Common.MsgBox("Response From Server", dgvLog.Rows[row].Cells[logColText].Value.ToString());
            }
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Program.LogDir);
        }
    }
}
