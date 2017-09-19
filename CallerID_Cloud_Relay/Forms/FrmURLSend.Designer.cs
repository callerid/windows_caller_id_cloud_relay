namespace CallerID_Cloud_Relay
{
    partial class FrmURLSend
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmURLSend));
            this.gbAuthenication = new System.Windows.Forms.GroupBox();
            this.ckbHideInSystemTray = new System.Windows.Forms.CheckBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.ckbRequiresAuthenication = new System.Windows.Forms.CheckBox();
            this.gbSuppliedUrl = new System.Windows.Forms.GroupBox();
            this.btnPaste = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSuppliedURL = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbUseBuiltUrl = new System.Windows.Forms.RadioButton();
            this.rbUseSuppliedUrl = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbBasicUnit = new System.Windows.Forms.RadioButton();
            this.rbDeluxeUnit = new System.Windows.Forms.RadioButton();
            this.btnTestSuppliedURL = new System.Windows.Forms.Button();
            this.gbDevSection = new System.Windows.Forms.GroupBox();
            this.lbSuccessfulGen = new System.Windows.Forms.Label();
            this.btnGenerateURL = new System.Windows.Forms.Button();
            this.btnCopyBuiltURL = new System.Windows.Forms.Button();
            this.lbRingTypeD = new System.Windows.Forms.Label();
            this.tbRingType = new System.Windows.Forms.TextBox();
            this.lbRingType = new System.Windows.Forms.Label();
            this.lbRingNumberD = new System.Windows.Forms.Label();
            this.tbRingNumber = new System.Windows.Forms.TextBox();
            this.lbRingNumber = new System.Windows.Forms.Label();
            this.lbDurationD = new System.Windows.Forms.Label();
            this.tbDuration = new System.Windows.Forms.TextBox();
            this.lbDuration = new System.Windows.Forms.Label();
            this.lbStatusD = new System.Windows.Forms.Label();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbSED = new System.Windows.Forms.Label();
            this.tbSE = new System.Windows.Forms.TextBox();
            this.lbSE = new System.Windows.Forms.Label();
            this.lbIOD = new System.Windows.Forms.Label();
            this.tbIO = new System.Windows.Forms.TextBox();
            this.lbIO = new System.Windows.Forms.Label();
            this.lbNameD = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.lbPhoneD = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.lbPhone = new System.Windows.Forms.Label();
            this.lbTimeD = new System.Windows.Forms.Label();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.lbTime = new System.Windows.Forms.Label();
            this.lbLineD = new System.Windows.Forms.Label();
            this.tbLine = new System.Windows.Forms.TextBox();
            this.lbLine = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbGeneratedURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.colLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerShowBoundPort = new System.Windows.Forms.Timer(this.components);
            this.timerHideGenerateSuccess = new System.Windows.Forms.Timer(this.components);
            this.sysTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerSySTrayHide = new System.Windows.Forms.Timer(this.components);
            this.gbAuthenication.SuspendLayout();
            this.gbSuppliedUrl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbDevSection.SuspendLayout();
            this.gbLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.SuspendLayout();
            // 
            // gbAuthenication
            // 
            this.gbAuthenication.Controls.Add(this.ckbHideInSystemTray);
            this.gbAuthenication.Controls.Add(this.tbPassword);
            this.gbAuthenication.Controls.Add(this.lbPassword);
            this.gbAuthenication.Controls.Add(this.tbUsername);
            this.gbAuthenication.Controls.Add(this.lbUsername);
            this.gbAuthenication.Controls.Add(this.ckbRequiresAuthenication);
            this.gbAuthenication.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAuthenication.Location = new System.Drawing.Point(12, 12);
            this.gbAuthenication.Name = "gbAuthenication";
            this.gbAuthenication.Size = new System.Drawing.Size(697, 63);
            this.gbAuthenication.TabIndex = 1;
            this.gbAuthenication.TabStop = false;
            this.gbAuthenication.Text = "Authenication";
            // 
            // ckbHideInSystemTray
            // 
            this.ckbHideInSystemTray.AutoSize = true;
            this.ckbHideInSystemTray.Location = new System.Drawing.Point(409, 0);
            this.ckbHideInSystemTray.Name = "ckbHideInSystemTray";
            this.ckbHideInSystemTray.Size = new System.Drawing.Size(291, 21);
            this.ckbHideInSystemTray.TabIndex = 10;
            this.ckbHideInSystemTray.Text = "Cloud Relay Will Load Up into System Tray";
            this.ckbHideInSystemTray.UseVisualStyleBackColor = true;
            this.ckbHideInSystemTray.CheckedChanged += new System.EventHandler(this.CkbHideInSystemTray_CheckedChanged);
            // 
            // tbPassword
            // 
            this.tbPassword.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(552, 28);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(139, 22);
            this.tbPassword.TabIndex = 1;
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPassword.Location = new System.Drawing.Point(491, 31);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(56, 13);
            this.lbPassword.TabIndex = 4;
            this.lbPassword.Text = "Password";
            // 
            // tbUsername
            // 
            this.tbUsername.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbUsername.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUsername.Location = new System.Drawing.Point(357, 28);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(126, 22);
            this.tbUsername.TabIndex = 0;
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsername.Location = new System.Drawing.Point(296, 31);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(58, 13);
            this.lbUsername.TabIndex = 2;
            this.lbUsername.Text = "Username";
            // 
            // ckbRequiresAuthenication
            // 
            this.ckbRequiresAuthenication.AutoSize = true;
            this.ckbRequiresAuthenication.Checked = true;
            this.ckbRequiresAuthenication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRequiresAuthenication.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckbRequiresAuthenication.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbRequiresAuthenication.Location = new System.Drawing.Point(19, 30);
            this.ckbRequiresAuthenication.Name = "ckbRequiresAuthenication";
            this.ckbRequiresAuthenication.Size = new System.Drawing.Size(266, 17);
            this.ckbRequiresAuthenication.TabIndex = 1;
            this.ckbRequiresAuthenication.Text = "Does your Cloud Server require authenication?";
            this.ckbRequiresAuthenication.UseVisualStyleBackColor = true;
            this.ckbRequiresAuthenication.CheckedChanged += new System.EventHandler(this.AuthRequriedCheckChange);
            // 
            // gbSuppliedUrl
            // 
            this.gbSuppliedUrl.Controls.Add(this.btnPaste);
            this.gbSuppliedUrl.Controls.Add(this.label1);
            this.gbSuppliedUrl.Controls.Add(this.tbSuppliedURL);
            this.gbSuppliedUrl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSuppliedUrl.Location = new System.Drawing.Point(12, 125);
            this.gbSuppliedUrl.Name = "gbSuppliedUrl";
            this.gbSuppliedUrl.Size = new System.Drawing.Size(697, 89);
            this.gbSuppliedUrl.TabIndex = 6;
            this.gbSuppliedUrl.TabStop = false;
            this.gbSuppliedUrl.Text = "Supplied URL";
            // 
            // btnPaste
            // 
            this.btnPaste.BackColor = System.Drawing.SystemColors.Control;
            this.btnPaste.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPaste.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaste.Location = new System.Drawing.Point(552, 60);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(139, 23);
            this.btnPaste.TabIndex = 10;
            this.btnPaste.TabStop = false;
            this.btnPaste.Text = "Paste";
            this.btnPaste.UseVisualStyleBackColor = false;
            this.btnPaste.Click += new System.EventHandler(this.ParseURL);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(519, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Example: http://www.cloudserver.com/upload.php?line=%Line&&number=%Phone&&calleri" +
                "d=%Name";
            // 
            // tbSuppliedURL
            // 
            this.tbSuppliedURL.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbSuppliedURL.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSuppliedURL.Location = new System.Drawing.Point(6, 32);
            this.tbSuppliedURL.Name = "tbSuppliedURL";
            this.tbSuppliedURL.Size = new System.Drawing.Size(685, 22);
            this.tbSuppliedURL.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbUseBuiltUrl);
            this.panel1.Controls.Add(this.rbUseSuppliedUrl);
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(12, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 38);
            this.panel1.TabIndex = 7;
            // 
            // rbUseBuiltUrl
            // 
            this.rbUseBuiltUrl.AutoSize = true;
            this.rbUseBuiltUrl.Location = new System.Drawing.Point(138, 10);
            this.rbUseBuiltUrl.Name = "rbUseBuiltUrl";
            this.rbUseBuiltUrl.Size = new System.Drawing.Size(94, 17);
            this.rbUseBuiltUrl.TabIndex = 1;
            this.rbUseBuiltUrl.Text = "Use Built URL";
            this.rbUseBuiltUrl.UseVisualStyleBackColor = true;
            this.rbUseBuiltUrl.CheckedChanged += new System.EventHandler(this.ChangeOfUrlType);
            // 
            // rbUseSuppliedUrl
            // 
            this.rbUseSuppliedUrl.AutoSize = true;
            this.rbUseSuppliedUrl.Checked = true;
            this.rbUseSuppliedUrl.Location = new System.Drawing.Point(19, 10);
            this.rbUseSuppliedUrl.Name = "rbUseSuppliedUrl";
            this.rbUseSuppliedUrl.Size = new System.Drawing.Size(116, 17);
            this.rbUseSuppliedUrl.TabIndex = 0;
            this.rbUseSuppliedUrl.TabStop = true;
            this.rbUseSuppliedUrl.Text = "Use Supplied URL";
            this.rbUseSuppliedUrl.UseVisualStyleBackColor = true;
            this.rbUseSuppliedUrl.CheckedChanged += new System.EventHandler(this.ChangeOfUrlType);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbBasicUnit);
            this.panel2.Controls.Add(this.rbDeluxeUnit);
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(265, 81);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(202, 38);
            this.panel2.TabIndex = 8;
            // 
            // rbBasicUnit
            // 
            this.rbBasicUnit.AutoSize = true;
            this.rbBasicUnit.Checked = true;
            this.rbBasicUnit.Location = new System.Drawing.Point(23, 10);
            this.rbBasicUnit.Name = "rbBasicUnit";
            this.rbBasicUnit.Size = new System.Drawing.Size(76, 17);
            this.rbBasicUnit.TabIndex = 1;
            this.rbBasicUnit.TabStop = true;
            this.rbBasicUnit.Text = "Basic Unit";
            this.rbBasicUnit.UseVisualStyleBackColor = true;
            this.rbBasicUnit.CheckedChanged += new System.EventHandler(this.ToggleDeluxe);
            // 
            // rbDeluxeUnit
            // 
            this.rbDeluxeUnit.AutoSize = true;
            this.rbDeluxeUnit.Location = new System.Drawing.Point(102, 10);
            this.rbDeluxeUnit.Name = "rbDeluxeUnit";
            this.rbDeluxeUnit.Size = new System.Drawing.Size(85, 17);
            this.rbDeluxeUnit.TabIndex = 0;
            this.rbDeluxeUnit.Text = "Deluxe Unit";
            this.rbDeluxeUnit.UseVisualStyleBackColor = true;
            this.rbDeluxeUnit.CheckedChanged += new System.EventHandler(this.ToggleDeluxe);
            // 
            // btnTestSuppliedURL
            // 
            this.btnTestSuppliedURL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestSuppliedURL.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTestSuppliedURL.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestSuppliedURL.Location = new System.Drawing.Point(476, 88);
            this.btnTestSuppliedURL.Name = "btnTestSuppliedURL";
            this.btnTestSuppliedURL.Size = new System.Drawing.Size(233, 23);
            this.btnTestSuppliedURL.TabIndex = 9;
            this.btnTestSuppliedURL.TabStop = false;
            this.btnTestSuppliedURL.Text = "Test Supplied URL";
            this.btnTestSuppliedURL.UseVisualStyleBackColor = true;
            this.btnTestSuppliedURL.Click += new System.EventHandler(this.BtnTestSuppliedURL_Click);
            // 
            // gbDevSection
            // 
            this.gbDevSection.Controls.Add(this.lbSuccessfulGen);
            this.gbDevSection.Controls.Add(this.btnGenerateURL);
            this.gbDevSection.Controls.Add(this.btnCopyBuiltURL);
            this.gbDevSection.Controls.Add(this.lbRingTypeD);
            this.gbDevSection.Controls.Add(this.tbRingType);
            this.gbDevSection.Controls.Add(this.lbRingType);
            this.gbDevSection.Controls.Add(this.lbRingNumberD);
            this.gbDevSection.Controls.Add(this.tbRingNumber);
            this.gbDevSection.Controls.Add(this.lbRingNumber);
            this.gbDevSection.Controls.Add(this.lbDurationD);
            this.gbDevSection.Controls.Add(this.tbDuration);
            this.gbDevSection.Controls.Add(this.lbDuration);
            this.gbDevSection.Controls.Add(this.lbStatusD);
            this.gbDevSection.Controls.Add(this.tbStatus);
            this.gbDevSection.Controls.Add(this.lbStatus);
            this.gbDevSection.Controls.Add(this.lbSED);
            this.gbDevSection.Controls.Add(this.tbSE);
            this.gbDevSection.Controls.Add(this.lbSE);
            this.gbDevSection.Controls.Add(this.lbIOD);
            this.gbDevSection.Controls.Add(this.tbIO);
            this.gbDevSection.Controls.Add(this.lbIO);
            this.gbDevSection.Controls.Add(this.lbNameD);
            this.gbDevSection.Controls.Add(this.tbName);
            this.gbDevSection.Controls.Add(this.lbName);
            this.gbDevSection.Controls.Add(this.lbPhoneD);
            this.gbDevSection.Controls.Add(this.tbPhone);
            this.gbDevSection.Controls.Add(this.lbPhone);
            this.gbDevSection.Controls.Add(this.lbTimeD);
            this.gbDevSection.Controls.Add(this.tbTime);
            this.gbDevSection.Controls.Add(this.lbTime);
            this.gbDevSection.Controls.Add(this.lbLineD);
            this.gbDevSection.Controls.Add(this.tbLine);
            this.gbDevSection.Controls.Add(this.lbLine);
            this.gbDevSection.Controls.Add(this.label7);
            this.gbDevSection.Controls.Add(this.label6);
            this.gbDevSection.Controls.Add(this.label5);
            this.gbDevSection.Controls.Add(this.tbGeneratedURL);
            this.gbDevSection.Controls.Add(this.label3);
            this.gbDevSection.Controls.Add(this.label2);
            this.gbDevSection.Controls.Add(this.tbServer);
            this.gbDevSection.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDevSection.Location = new System.Drawing.Point(12, 220);
            this.gbDevSection.Name = "gbDevSection";
            this.gbDevSection.Size = new System.Drawing.Size(697, 446);
            this.gbDevSection.TabIndex = 7;
            this.gbDevSection.TabStop = false;
            this.gbDevSection.Text = "Developers Section - Build the relay URL by entering the Cloud Server address and" +
                " mapping data fields";
            // 
            // lbSuccessfulGen
            // 
            this.lbSuccessfulGen.AutoSize = true;
            this.lbSuccessfulGen.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSuccessfulGen.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbSuccessfulGen.Location = new System.Drawing.Point(182, 77);
            this.lbSuccessfulGen.Name = "lbSuccessfulGen";
            this.lbSuccessfulGen.Size = new System.Drawing.Size(109, 13);
            this.lbSuccessfulGen.TabIndex = 10;
            this.lbSuccessfulGen.Text = "Generated Correctly";
            this.lbSuccessfulGen.Visible = false;
            // 
            // btnGenerateURL
            // 
            this.btnGenerateURL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerateURL.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGenerateURL.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateURL.Location = new System.Drawing.Point(75, 72);
            this.btnGenerateURL.Name = "btnGenerateURL";
            this.btnGenerateURL.Size = new System.Drawing.Size(101, 23);
            this.btnGenerateURL.TabIndex = 41;
            this.btnGenerateURL.TabStop = false;
            this.btnGenerateURL.Text = "Generate URL";
            this.btnGenerateURL.UseVisualStyleBackColor = true;
            this.btnGenerateURL.Click += new System.EventHandler(this.BtnGenerateURL_Click);
            // 
            // btnCopyBuiltURL
            // 
            this.btnCopyBuiltURL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyBuiltURL.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCopyBuiltURL.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyBuiltURL.Location = new System.Drawing.Point(590, 72);
            this.btnCopyBuiltURL.Name = "btnCopyBuiltURL";
            this.btnCopyBuiltURL.Size = new System.Drawing.Size(101, 23);
            this.btnCopyBuiltURL.TabIndex = 11;
            this.btnCopyBuiltURL.TabStop = false;
            this.btnCopyBuiltURL.Text = "Copy Built URL";
            this.btnCopyBuiltURL.UseVisualStyleBackColor = true;
            this.btnCopyBuiltURL.Click += new System.EventHandler(this.BtnCopyBuiltURL_Click);
            // 
            // lbRingTypeD
            // 
            this.lbRingTypeD.AutoSize = true;
            this.lbRingTypeD.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRingTypeD.Location = new System.Drawing.Point(384, 410);
            this.lbRingTypeD.Name = "lbRingTypeD";
            this.lbRingTypeD.Size = new System.Drawing.Size(101, 17);
            this.lbRingTypeD.TabIndex = 40;
            this.lbRingTypeD.Text = "Distinctive Ring";
            // 
            // tbRingType
            // 
            this.tbRingType.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbRingType.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRingType.Location = new System.Drawing.Point(192, 410);
            this.tbRingType.Name = "tbRingType";
            this.tbRingType.Size = new System.Drawing.Size(160, 22);
            this.tbRingType.TabIndex = 13;
            this.tbRingType.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // lbRingType
            // 
            this.lbRingType.AutoSize = true;
            this.lbRingType.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRingType.Location = new System.Drawing.Point(49, 410);
            this.lbRingType.Name = "lbRingType";
            this.lbRingType.Size = new System.Drawing.Size(74, 17);
            this.lbRingType.TabIndex = 39;
            this.lbRingType.Text = "%RingType";
            // 
            // lbRingNumberD
            // 
            this.lbRingNumberD.AutoSize = true;
            this.lbRingNumberD.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRingNumberD.Location = new System.Drawing.Point(384, 382);
            this.lbRingNumberD.Name = "lbRingNumberD";
            this.lbRingNumberD.Size = new System.Drawing.Size(270, 17);
            this.lbRingNumberD.TabIndex = 37;
            this.lbRingNumberD.Text = "Number of Rings Before Call was Answered";
            // 
            // tbRingNumber
            // 
            this.tbRingNumber.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbRingNumber.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRingNumber.Location = new System.Drawing.Point(192, 382);
            this.tbRingNumber.Name = "tbRingNumber";
            this.tbRingNumber.Size = new System.Drawing.Size(160, 22);
            this.tbRingNumber.TabIndex = 12;
            this.tbRingNumber.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // lbRingNumber
            // 
            this.lbRingNumber.AutoSize = true;
            this.lbRingNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRingNumber.Location = new System.Drawing.Point(49, 382);
            this.lbRingNumber.Name = "lbRingNumber";
            this.lbRingNumber.Size = new System.Drawing.Size(96, 17);
            this.lbRingNumber.TabIndex = 36;
            this.lbRingNumber.Text = "%RingNumber";
            // 
            // lbDurationD
            // 
            this.lbDurationD.AutoSize = true;
            this.lbDurationD.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDurationD.Location = new System.Drawing.Point(384, 354);
            this.lbDurationD.Name = "lbDurationD";
            this.lbDurationD.Size = new System.Drawing.Size(91, 17);
            this.lbDurationD.TabIndex = 34;
            this.lbDurationD.Text = "Length of Call";
            // 
            // tbDuration
            // 
            this.tbDuration.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbDuration.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDuration.Location = new System.Drawing.Point(192, 354);
            this.tbDuration.Name = "tbDuration";
            this.tbDuration.Size = new System.Drawing.Size(160, 22);
            this.tbDuration.TabIndex = 11;
            this.tbDuration.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // lbDuration
            // 
            this.lbDuration.AutoSize = true;
            this.lbDuration.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDuration.Location = new System.Drawing.Point(49, 354);
            this.lbDuration.Name = "lbDuration";
            this.lbDuration.Size = new System.Drawing.Size(72, 17);
            this.lbDuration.TabIndex = 33;
            this.lbDuration.Text = "%Duration";
            // 
            // lbStatusD
            // 
            this.lbStatusD.AutoSize = true;
            this.lbStatusD.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatusD.Location = new System.Drawing.Point(384, 326);
            this.lbStatusD.Name = "lbStatusD";
            this.lbStatusD.Size = new System.Drawing.Size(142, 17);
            this.lbStatusD.TabIndex = 31;
            this.lbStatusD.Text = "Detailed Phone Status";
            // 
            // tbStatus
            // 
            this.tbStatus.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbStatus.Location = new System.Drawing.Point(192, 326);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Size = new System.Drawing.Size(160, 22);
            this.tbStatus.TabIndex = 10;
            this.tbStatus.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(49, 326);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(57, 17);
            this.lbStatus.TabIndex = 30;
            this.lbStatus.Text = "%Status";
            // 
            // lbSED
            // 
            this.lbSED.AutoSize = true;
            this.lbSED.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSED.Location = new System.Drawing.Point(384, 298);
            this.lbSED.Name = "lbSED";
            this.lbSED.Size = new System.Drawing.Size(122, 17);
            this.lbSED.TabIndex = 28;
            this.lbSED.Text = "Start or End of Call";
            // 
            // tbSE
            // 
            this.tbSE.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbSE.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSE.Location = new System.Drawing.Point(192, 298);
            this.tbSE.Name = "tbSE";
            this.tbSE.Size = new System.Drawing.Size(160, 22);
            this.tbSE.TabIndex = 9;
            this.tbSE.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // lbSE
            // 
            this.lbSE.AutoSize = true;
            this.lbSE.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSE.Location = new System.Drawing.Point(49, 298);
            this.lbSE.Name = "lbSE";
            this.lbSE.Size = new System.Drawing.Size(33, 17);
            this.lbSE.TabIndex = 27;
            this.lbSE.Text = "%SE";
            // 
            // lbIOD
            // 
            this.lbIOD.AutoSize = true;
            this.lbIOD.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIOD.Location = new System.Drawing.Point(384, 270);
            this.lbIOD.Name = "lbIOD";
            this.lbIOD.Size = new System.Drawing.Size(169, 17);
            this.lbIOD.TabIndex = 25;
            this.lbIOD.Text = "Inbound or Outbound Call";
            // 
            // tbIO
            // 
            this.tbIO.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbIO.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIO.Location = new System.Drawing.Point(192, 270);
            this.tbIO.Name = "tbIO";
            this.tbIO.Size = new System.Drawing.Size(160, 22);
            this.tbIO.TabIndex = 8;
            this.tbIO.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // lbIO
            // 
            this.lbIO.AutoSize = true;
            this.lbIO.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIO.Location = new System.Drawing.Point(49, 270);
            this.lbIO.Name = "lbIO";
            this.lbIO.Size = new System.Drawing.Size(33, 17);
            this.lbIO.TabIndex = 24;
            this.lbIO.Text = "%IO";
            // 
            // lbNameD
            // 
            this.lbNameD.AutoSize = true;
            this.lbNameD.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNameD.Location = new System.Drawing.Point(384, 242);
            this.lbNameD.Name = "lbNameD";
            this.lbNameD.Size = new System.Drawing.Size(97, 17);
            this.lbNameD.TabIndex = 22;
            this.lbNameD.Text = "Call Line Name";
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.Location = new System.Drawing.Point(192, 242);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(160, 22);
            this.tbName.TabIndex = 7;
            this.tbName.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(49, 242);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(55, 17);
            this.lbName.TabIndex = 21;
            this.lbName.Text = "%Name";
            // 
            // lbPhoneD
            // 
            this.lbPhoneD.AutoSize = true;
            this.lbPhoneD.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPhoneD.Location = new System.Drawing.Point(384, 214);
            this.lbPhoneD.Name = "lbPhoneD";
            this.lbPhoneD.Size = new System.Drawing.Size(112, 17);
            this.lbPhoneD.TabIndex = 19;
            this.lbPhoneD.Text = "Caller ID Number";
            // 
            // tbPhone
            // 
            this.tbPhone.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbPhone.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPhone.Location = new System.Drawing.Point(192, 214);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(160, 22);
            this.tbPhone.TabIndex = 6;
            this.tbPhone.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // lbPhone
            // 
            this.lbPhone.AutoSize = true;
            this.lbPhone.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPhone.Location = new System.Drawing.Point(49, 214);
            this.lbPhone.Name = "lbPhone";
            this.lbPhone.Size = new System.Drawing.Size(58, 17);
            this.lbPhone.TabIndex = 18;
            this.lbPhone.Text = "%Phone";
            // 
            // lbTimeD
            // 
            this.lbTimeD.AutoSize = true;
            this.lbTimeD.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTimeD.Location = new System.Drawing.Point(384, 186);
            this.lbTimeD.Name = "lbTimeD";
            this.lbTimeD.Size = new System.Drawing.Size(137, 17);
            this.lbTimeD.TabIndex = 16;
            this.lbTimeD.Text = "Time and Date of Call";
            // 
            // tbTime
            // 
            this.tbTime.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbTime.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTime.Location = new System.Drawing.Point(192, 186);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(160, 22);
            this.tbTime.TabIndex = 5;
            this.tbTime.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.Location = new System.Drawing.Point(49, 186);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(48, 17);
            this.lbTime.TabIndex = 15;
            this.lbTime.Text = "%Time";
            // 
            // lbLineD
            // 
            this.lbLineD.AutoSize = true;
            this.lbLineD.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLineD.Location = new System.Drawing.Point(384, 158);
            this.lbLineD.Name = "lbLineD";
            this.lbLineD.Size = new System.Drawing.Size(111, 17);
            this.lbLineD.TabIndex = 13;
            this.lbLineD.Text = "Call Line Number";
            // 
            // tbLine
            // 
            this.tbLine.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbLine.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLine.Location = new System.Drawing.Point(192, 158);
            this.tbLine.Name = "tbLine";
            this.tbLine.Size = new System.Drawing.Size(160, 22);
            this.tbLine.TabIndex = 4;
            this.tbLine.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // lbLine
            // 
            this.lbLine.AutoSize = true;
            this.lbLine.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine.Location = new System.Drawing.Point(49, 158);
            this.lbLine.Name = "lbLine";
            this.lbLine.Size = new System.Drawing.Size(43, 17);
            this.lbLine.TabIndex = 12;
            this.lbLine.Text = "%Line";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(384, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Descriptions";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(189, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Your Parameter Variables";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "CallerID.com Variables";
            // 
            // tbGeneratedURL
            // 
            this.tbGeneratedURL.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbGeneratedURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbGeneratedURL.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbGeneratedURL.ForeColor = System.Drawing.Color.Maroon;
            this.tbGeneratedURL.Location = new System.Drawing.Point(75, 28);
            this.tbGeneratedURL.Multiline = true;
            this.tbGeneratedURL.Name = "tbGeneratedURL";
            this.tbGeneratedURL.ReadOnly = true;
            this.tbGeneratedURL.Size = new System.Drawing.Size(616, 38);
            this.tbGeneratedURL.TabIndex = 8;
            this.tbGeneratedURL.TabStop = false;
            this.tbGeneratedURL.Text = "[ you must first generate your URL]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Built URL:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Server:";
            // 
            // tbServer
            // 
            this.tbServer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbServer.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServer.Location = new System.Drawing.Point(75, 107);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(616, 22);
            this.tbServer.TabIndex = 3;
            this.tbServer.Leave += new System.EventHandler(this.ParamLeaveFocus);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(473, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(239, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sends example call to URL (Start Record Only)";
            // 
            // gbLog
            // 
            this.gbLog.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.gbLog.Controls.Add(this.btnClearLog);
            this.gbLog.Controls.Add(this.dgvLog);
            this.gbLog.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLog.Location = new System.Drawing.Point(15, 672);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(697, 127);
            this.gbLog.TabIndex = 7;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearLog.Location = new System.Drawing.Point(672, 21);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(19, 100);
            this.btnClearLog.TabIndex = 42;
            this.btnClearLog.TabStop = false;
            this.btnClearLog.Text = "CLEAR";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.BtnClearLog_Click);
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLine,
            this.colInOut,
            this.colStartEnd,
            this.colDur,
            this.colRing,
            this.colDate,
            this.colNumber,
            this.colName});
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.Location = new System.Drawing.Point(3, 21);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvLog.Size = new System.Drawing.Size(691, 103);
            this.dgvLog.TabIndex = 0;
            // 
            // colLine
            // 
            this.colLine.HeaderText = "Ln";
            this.colLine.Name = "colLine";
            this.colLine.ReadOnly = true;
            this.colLine.Width = 40;
            // 
            // colInOut
            // 
            this.colInOut.HeaderText = "I/O";
            this.colInOut.Name = "colInOut";
            this.colInOut.ReadOnly = true;
            this.colInOut.Width = 40;
            // 
            // colStartEnd
            // 
            this.colStartEnd.HeaderText = "S/E";
            this.colStartEnd.Name = "colStartEnd";
            this.colStartEnd.ReadOnly = true;
            this.colStartEnd.Width = 40;
            // 
            // colDur
            // 
            this.colDur.HeaderText = "Dur";
            this.colDur.Name = "colDur";
            this.colDur.ReadOnly = true;
            this.colDur.Width = 60;
            // 
            // colRing
            // 
            this.colRing.HeaderText = "Ring";
            this.colRing.Name = "colRing";
            this.colRing.ReadOnly = true;
            this.colRing.Width = 50;
            // 
            // colDate
            // 
            this.colDate.HeaderText = "Date & Time";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.Width = 150;
            // 
            // colNumber
            // 
            this.colNumber.HeaderText = "Number";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Width = 125;
            // 
            // colName
            // 
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 125;
            // 
            // timerShowBoundPort
            // 
            this.timerShowBoundPort.Enabled = true;
            this.timerShowBoundPort.Interval = 1000;
            this.timerShowBoundPort.Tick += new System.EventHandler(this.TimerShowBoundPort_Tick);
            // 
            // timerHideGenerateSuccess
            // 
            this.timerHideGenerateSuccess.Interval = 3000;
            this.timerHideGenerateSuccess.Tick += new System.EventHandler(this.TimerHideGenerateSuccess_Tick);
            // 
            // sysTray
            // 
            this.sysTray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.sysTray.BalloonTipText = "Cloud Relay running in background.";
            this.sysTray.BalloonTipTitle = "Caller ID Cloud Relay";
            this.sysTray.Icon = ((System.Drawing.Icon)(resources.GetObject("sysTray.Icon")));
            this.sysTray.Text = "Caller ID Cloud Relay";
            this.sysTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.BringToForeground);
            // 
            // timerSySTrayHide
            // 
            this.timerSySTrayHide.Enabled = true;
            this.timerSySTrayHide.Tick += new System.EventHandler(this.TimerSySTrayHide_Tick);
            // 
            // FrmURLSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(724, 811);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gbDevSection);
            this.Controls.Add(this.btnTestSuppliedURL);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbSuppliedUrl);
            this.Controls.Add(this.gbAuthenication);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmURLSend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CallerID.com Cloud Relay - v. 0.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmURLSend_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmURLSend_FormClosed);
            this.gbAuthenication.ResumeLayout(false);
            this.gbAuthenication.PerformLayout();
            this.gbSuppliedUrl.ResumeLayout(false);
            this.gbSuppliedUrl.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gbDevSection.ResumeLayout(false);
            this.gbDevSection.PerformLayout();
            this.gbLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAuthenication;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.CheckBox ckbRequiresAuthenication;
        private System.Windows.Forms.GroupBox gbSuppliedUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSuppliedURL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbUseBuiltUrl;
        private System.Windows.Forms.RadioButton rbUseSuppliedUrl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbBasicUnit;
        private System.Windows.Forms.RadioButton rbDeluxeUnit;
        private System.Windows.Forms.Button btnTestSuppliedURL;
        private System.Windows.Forms.GroupBox gbDevSection;
        private System.Windows.Forms.TextBox tbGeneratedURL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.Label lbRingTypeD;
        private System.Windows.Forms.TextBox tbRingType;
        private System.Windows.Forms.Label lbRingType;
        private System.Windows.Forms.Label lbRingNumberD;
        private System.Windows.Forms.TextBox tbRingNumber;
        private System.Windows.Forms.Label lbRingNumber;
        private System.Windows.Forms.Label lbDurationD;
        private System.Windows.Forms.TextBox tbDuration;
        private System.Windows.Forms.Label lbDuration;
        private System.Windows.Forms.Label lbStatusD;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbSED;
        private System.Windows.Forms.TextBox tbSE;
        private System.Windows.Forms.Label lbSE;
        private System.Windows.Forms.Label lbIOD;
        private System.Windows.Forms.TextBox tbIO;
        private System.Windows.Forms.Label lbIO;
        private System.Windows.Forms.Label lbNameD;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbPhoneD;
        private System.Windows.Forms.TextBox tbPhone;
        private System.Windows.Forms.Label lbPhone;
        private System.Windows.Forms.Label lbTimeD;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label lbLineD;
        private System.Windows.Forms.TextBox tbLine;
        private System.Windows.Forms.Label lbLine;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnGenerateURL;
        private System.Windows.Forms.Button btnCopyBuiltURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDur;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRing;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.Timer timerShowBoundPort;
        private System.Windows.Forms.Label lbSuccessfulGen;
        private System.Windows.Forms.Timer timerHideGenerateSuccess;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.NotifyIcon sysTray;
        private System.Windows.Forms.CheckBox ckbHideInSystemTray;
        private System.Windows.Forms.Timer timerSySTrayHide;
    }
}

