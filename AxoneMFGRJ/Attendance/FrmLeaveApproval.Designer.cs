namespace AxoneMFGRJ.Attendance
{
    partial class FrmLeaveApproval
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
            this.PicEmpPhoto = new System.Windows.Forms.PictureBox();
            this.BtnApproved = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancle = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.ChkISOfficeWork = new AxonContLib.cCheckBox(this.components);
            this.txtReason = new AxonContLib.cTextBox(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.txtRemark = new AxonContLib.cTextBox(this.components);
            this.txtOtherReason = new AxonContLib.cTextBox(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.txtEmployeeName = new AxonContLib.cTextBox(this.components);
            this.txtEmployeeCode = new AxonContLib.cTextBox(this.components);
            this.txtSlipNo = new AxonContLib.cTextBox(this.components);
            this.labelControl8 = new AxonContLib.cLabel(this.components);
            this.DTPSlipDate = new AxonContLib.cDateTimePicker(this.components);
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.panel3 = new AxonContLib.cPanel(this.components);
            this.lblEntryIP = new AxonContLib.cLabel(this.components);
            this.lblEntryDate = new AxonContLib.cLabel(this.components);
            this.cLabel13 = new AxonContLib.cLabel(this.components);
            this.cLabel11 = new AxonContLib.cLabel(this.components);
            this.lblEntryBy = new AxonContLib.cLabel(this.components);
            this.cLabel8 = new AxonContLib.cLabel(this.components);
            this.cLabel7 = new AxonContLib.cLabel(this.components);
            this.txtComment = new AxonContLib.cTextBox(this.components);
            this.lblLeaveType = new AxonContLib.cLabel(this.components);
            this.PnlTop = new AxonContLib.cPanel(this.components);
            this.txtLeaveDate = new AxonContLib.cTextBox(this.components);
            this.cLabel10 = new AxonContLib.cLabel(this.components);
            this.txtLeaveDays = new AxonContLib.cTextBox(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PicEmpPhoto)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.PnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicEmpPhoto
            // 
            this.PicEmpPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicEmpPhoto.Enabled = false;
            this.PicEmpPhoto.Location = new System.Drawing.Point(502, 128);
            this.PicEmpPhoto.Name = "PicEmpPhoto";
            this.PicEmpPhoto.Size = new System.Drawing.Size(155, 166);
            this.PicEmpPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicEmpPhoto.TabIndex = 147;
            this.PicEmpPhoto.TabStop = false;
            // 
            // BtnApproved
            // 
            this.BtnApproved.Appearance.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.BtnApproved.Appearance.ForeColor = System.Drawing.Color.Green;
            this.BtnApproved.Appearance.Options.UseFont = true;
            this.BtnApproved.Appearance.Options.UseForeColor = true;
            this.BtnApproved.Location = new System.Drawing.Point(105, 105);
            this.BtnApproved.Name = "BtnApproved";
            this.BtnApproved.Size = new System.Drawing.Size(148, 44);
            this.BtnApproved.TabIndex = 34;
            this.BtnApproved.Text = "APPROVE";
            this.BtnApproved.Click += new System.EventHandler(this.BtnApproved_Click);
            // 
            // BtnCancle
            // 
            this.BtnCancle.Appearance.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.BtnCancle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnCancle.Appearance.Options.UseFont = true;
            this.BtnCancle.Appearance.Options.UseForeColor = true;
            this.BtnCancle.Location = new System.Drawing.Point(275, 105);
            this.BtnCancle.Name = "BtnCancle";
            this.BtnCancle.Size = new System.Drawing.Size(148, 44);
            this.BtnCancle.TabIndex = 35;
            this.BtnCancle.TabStop = false;
            this.BtnCancle.Text = "CANCEL";
            this.BtnCancle.Click += new System.EventHandler(this.BtnCancle_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Location = new System.Drawing.Point(446, 105);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(148, 44);
            this.BtnExit.TabIndex = 36;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // ChkISOfficeWork
            // 
            this.ChkISOfficeWork.AllowTabKeyOnEnter = false;
            this.ChkISOfficeWork.AutoSize = true;
            this.ChkISOfficeWork.Enabled = false;
            this.ChkISOfficeWork.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.ChkISOfficeWork.ForeColor = System.Drawing.Color.RoyalBlue;
            this.ChkISOfficeWork.Location = new System.Drawing.Point(105, 430);
            this.ChkISOfficeWork.Name = "ChkISOfficeWork";
            this.ChkISOfficeWork.Size = new System.Drawing.Size(125, 18);
            this.ChkISOfficeWork.TabIndex = 140;
            this.ChkISOfficeWork.Text = "IS Office Work";
            this.ChkISOfficeWork.ToolTips = "";
            this.ChkISOfficeWork.UseVisualStyleBackColor = true;
            // 
            // txtReason
            // 
            this.txtReason.ActivationColor = true;
            this.txtReason.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtReason.AllowTabKeyOnEnter = false;
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReason.Enabled = false;
            this.txtReason.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtReason.Format = "";
            this.txtReason.IsComplusory = false;
            this.txtReason.Location = new System.Drawing.Point(105, 167);
            this.txtReason.Name = "txtReason";
            this.txtReason.SelectAllTextOnFocus = true;
            this.txtReason.Size = new System.Drawing.Size(390, 24);
            this.txtReason.TabIndex = 1;
            this.txtReason.ToolTips = "";
            this.txtReason.WaterMarkText = null;
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(11, 247);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(58, 14);
            this.cLabel5.TabIndex = 138;
            this.cLabel5.Text = "Remark";
            this.cLabel5.ToolTips = "";
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(11, 212);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(45, 14);
            this.cLabel4.TabIndex = 138;
            this.cLabel4.Text = "Other";
            this.cLabel4.ToolTips = "";
            // 
            // txtRemark
            // 
            this.txtRemark.ActivationColor = true;
            this.txtRemark.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtRemark.AllowTabKeyOnEnter = false;
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark.Enabled = false;
            this.txtRemark.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtRemark.Format = "";
            this.txtRemark.IsComplusory = false;
            this.txtRemark.Location = new System.Drawing.Point(105, 247);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.SelectAllTextOnFocus = true;
            this.txtRemark.Size = new System.Drawing.Size(390, 78);
            this.txtRemark.TabIndex = 1;
            this.txtRemark.ToolTips = "";
            this.txtRemark.WaterMarkText = null;
            // 
            // txtOtherReason
            // 
            this.txtOtherReason.ActivationColor = true;
            this.txtOtherReason.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtOtherReason.AllowTabKeyOnEnter = false;
            this.txtOtherReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOtherReason.Enabled = false;
            this.txtOtherReason.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtOtherReason.Format = "";
            this.txtOtherReason.IsComplusory = false;
            this.txtOtherReason.Location = new System.Drawing.Point(105, 206);
            this.txtOtherReason.Name = "txtOtherReason";
            this.txtOtherReason.SelectAllTextOnFocus = true;
            this.txtOtherReason.Size = new System.Drawing.Size(390, 24);
            this.txtOtherReason.TabIndex = 1;
            this.txtOtherReason.ToolTips = "";
            this.txtOtherReason.WaterMarkText = null;
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(11, 173);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(55, 14);
            this.cLabel3.TabIndex = 138;
            this.cLabel3.Text = "Reason";
            this.cLabel3.ToolTips = "";
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(11, 134);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(71, 14);
            this.cLabel2.TabIndex = 138;
            this.cLabel2.Text = "Employee";
            this.cLabel2.ToolTips = "";
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(13, 22);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(58, 14);
            this.cLabel1.TabIndex = 138;
            this.cLabel1.Text = "Slip No ";
            this.cLabel1.ToolTips = "";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.ActivationColor = true;
            this.txtEmployeeName.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtEmployeeName.AllowTabKeyOnEnter = false;
            this.txtEmployeeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployeeName.Enabled = false;
            this.txtEmployeeName.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtEmployeeName.Format = "";
            this.txtEmployeeName.IsComplusory = false;
            this.txtEmployeeName.Location = new System.Drawing.Point(204, 128);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.SelectAllTextOnFocus = true;
            this.txtEmployeeName.Size = new System.Drawing.Size(291, 24);
            this.txtEmployeeName.TabIndex = 1;
            this.txtEmployeeName.ToolTips = "";
            this.txtEmployeeName.WaterMarkText = null;
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.ActivationColor = true;
            this.txtEmployeeCode.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtEmployeeCode.AllowTabKeyOnEnter = false;
            this.txtEmployeeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployeeCode.Enabled = false;
            this.txtEmployeeCode.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtEmployeeCode.Format = "";
            this.txtEmployeeCode.IsComplusory = false;
            this.txtEmployeeCode.Location = new System.Drawing.Point(105, 128);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.SelectAllTextOnFocus = true;
            this.txtEmployeeCode.Size = new System.Drawing.Size(94, 24);
            this.txtEmployeeCode.TabIndex = 1;
            this.txtEmployeeCode.ToolTips = "";
            this.txtEmployeeCode.WaterMarkText = null;
            // 
            // txtSlipNo
            // 
            this.txtSlipNo.ActivationColor = true;
            this.txtSlipNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtSlipNo.AllowTabKeyOnEnter = false;
            this.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSlipNo.Enabled = false;
            this.txtSlipNo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlipNo.Format = "";
            this.txtSlipNo.IsComplusory = false;
            this.txtSlipNo.Location = new System.Drawing.Point(100, 18);
            this.txtSlipNo.Name = "txtSlipNo";
            this.txtSlipNo.SelectAllTextOnFocus = true;
            this.txtSlipNo.Size = new System.Drawing.Size(161, 27);
            this.txtSlipNo.TabIndex = 1;
            this.txtSlipNo.ToolTips = "";
            this.txtSlipNo.WaterMarkText = null;
            // 
            // labelControl8
            // 
            this.labelControl8.AutoSize = true;
            this.labelControl8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Location = new System.Drawing.Point(13, 65);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(77, 14);
            this.labelControl8.TabIndex = 30;
            this.labelControl8.Text = "Entry Date";
            this.labelControl8.ToolTips = "";
            // 
            // DTPSlipDate
            // 
            this.DTPSlipDate.AllowTabKeyOnEnter = false;
            this.DTPSlipDate.CustomFormat = "dd/MM/yyyy";
            this.DTPSlipDate.Enabled = false;
            this.DTPSlipDate.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPSlipDate.ForeColor = System.Drawing.Color.Black;
            this.DTPSlipDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPSlipDate.Location = new System.Drawing.Point(100, 61);
            this.DTPSlipDate.Name = "DTPSlipDate";
            this.DTPSlipDate.Size = new System.Drawing.Size(161, 27);
            this.DTPSlipDate.TabIndex = 0;
            this.DTPSlipDate.ToolTips = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.BtnApproved);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.BtnCancle);
            this.panel1.Controls.Add(this.cLabel7);
            this.panel1.Controls.Add(this.txtComment);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 469);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(671, 196);
            this.panel1.TabIndex = 148;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Teal;
            this.panel3.Controls.Add(this.lblEntryIP);
            this.panel3.Controls.Add(this.lblEntryDate);
            this.panel3.Controls.Add(this.cLabel13);
            this.panel3.Controls.Add(this.cLabel11);
            this.panel3.Controls.Add(this.lblEntryBy);
            this.panel3.Controls.Add(this.cLabel8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 161);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(671, 35);
            this.panel3.TabIndex = 139;
            // 
            // lblEntryIP
            // 
            this.lblEntryIP.AutoSize = true;
            this.lblEntryIP.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryIP.ForeColor = System.Drawing.Color.White;
            this.lblEntryIP.Location = new System.Drawing.Point(530, 9);
            this.lblEntryIP.Name = "lblEntryIP";
            this.lblEntryIP.Size = new System.Drawing.Size(61, 14);
            this.lblEntryIP.TabIndex = 138;
            this.lblEntryIP.Text = "Entry IP";
            this.lblEntryIP.ToolTips = "";
            // 
            // lblEntryDate
            // 
            this.lblEntryDate.AutoSize = true;
            this.lblEntryDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryDate.ForeColor = System.Drawing.Color.White;
            this.lblEntryDate.Location = new System.Drawing.Point(281, 9);
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.Size = new System.Drawing.Size(77, 14);
            this.lblEntryDate.TabIndex = 138;
            this.lblEntryDate.Text = "Entry Date";
            this.lblEntryDate.ToolTips = "";
            // 
            // cLabel13
            // 
            this.cLabel13.AutoSize = true;
            this.cLabel13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel13.ForeColor = System.Drawing.Color.White;
            this.cLabel13.Location = new System.Drawing.Point(456, 9);
            this.cLabel13.Name = "cLabel13";
            this.cLabel13.Size = new System.Drawing.Size(74, 14);
            this.cLabel13.TabIndex = 138;
            this.cLabel13.Text = "Entry IP : ";
            this.cLabel13.ToolTips = "";
            // 
            // cLabel11
            // 
            this.cLabel11.AutoSize = true;
            this.cLabel11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel11.ForeColor = System.Drawing.Color.White;
            this.cLabel11.Location = new System.Drawing.Point(196, 9);
            this.cLabel11.Name = "cLabel11";
            this.cLabel11.Size = new System.Drawing.Size(90, 14);
            this.cLabel11.TabIndex = 138;
            this.cLabel11.Text = "Entry Date : ";
            this.cLabel11.ToolTips = "";
            // 
            // lblEntryBy
            // 
            this.lblEntryBy.AutoSize = true;
            this.lblEntryBy.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryBy.ForeColor = System.Drawing.Color.White;
            this.lblEntryBy.Location = new System.Drawing.Point(81, 9);
            this.lblEntryBy.Name = "lblEntryBy";
            this.lblEntryBy.Size = new System.Drawing.Size(63, 14);
            this.lblEntryBy.TabIndex = 138;
            this.lblEntryBy.Text = "Entry By";
            this.lblEntryBy.ToolTips = "";
            // 
            // cLabel8
            // 
            this.cLabel8.AutoSize = true;
            this.cLabel8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel8.ForeColor = System.Drawing.Color.White;
            this.cLabel8.Location = new System.Drawing.Point(8, 9);
            this.cLabel8.Name = "cLabel8";
            this.cLabel8.Size = new System.Drawing.Size(76, 14);
            this.cLabel8.TabIndex = 138;
            this.cLabel8.Text = "Entry By : ";
            this.cLabel8.ToolTips = "";
            // 
            // cLabel7
            // 
            this.cLabel7.AutoSize = true;
            this.cLabel7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel7.ForeColor = System.Drawing.Color.Black;
            this.cLabel7.Location = new System.Drawing.Point(11, 24);
            this.cLabel7.Name = "cLabel7";
            this.cLabel7.Size = new System.Drawing.Size(69, 14);
            this.cLabel7.TabIndex = 138;
            this.cLabel7.Text = "Comment";
            this.cLabel7.ToolTips = "";
            // 
            // txtComment
            // 
            this.txtComment.ActivationColor = true;
            this.txtComment.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtComment.AllowTabKeyOnEnter = false;
            this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComment.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComment.Format = "";
            this.txtComment.IsComplusory = false;
            this.txtComment.Location = new System.Drawing.Point(105, 19);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.SelectAllTextOnFocus = true;
            this.txtComment.Size = new System.Drawing.Size(390, 68);
            this.txtComment.TabIndex = 1;
            this.txtComment.ToolTips = "";
            this.txtComment.WaterMarkText = null;
            // 
            // lblLeaveType
            // 
            this.lblLeaveType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblLeaveType.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLeaveType.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeaveType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLeaveType.Location = new System.Drawing.Point(358, 0);
            this.lblLeaveType.Name = "lblLeaveType";
            this.lblLeaveType.Size = new System.Drawing.Size(313, 102);
            this.lblLeaveType.TabIndex = 138;
            this.lblLeaveType.Text = "Leave Type";
            this.lblLeaveType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeaveType.ToolTips = "";
            // 
            // PnlTop
            // 
            this.PnlTop.BackColor = System.Drawing.Color.White;
            this.PnlTop.Controls.Add(this.cLabel1);
            this.PnlTop.Controls.Add(this.lblLeaveType);
            this.PnlTop.Controls.Add(this.txtSlipNo);
            this.PnlTop.Controls.Add(this.labelControl8);
            this.PnlTop.Controls.Add(this.DTPSlipDate);
            this.PnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTop.Location = new System.Drawing.Point(0, 0);
            this.PnlTop.Name = "PnlTop";
            this.PnlTop.Size = new System.Drawing.Size(671, 102);
            this.PnlTop.TabIndex = 149;
            // 
            // txtLeaveDate
            // 
            this.txtLeaveDate.ActivationColor = true;
            this.txtLeaveDate.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtLeaveDate.AllowTabKeyOnEnter = false;
            this.txtLeaveDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLeaveDate.Enabled = false;
            this.txtLeaveDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtLeaveDate.Format = "";
            this.txtLeaveDate.IsComplusory = false;
            this.txtLeaveDate.Location = new System.Drawing.Point(105, 340);
            this.txtLeaveDate.Multiline = true;
            this.txtLeaveDate.Name = "txtLeaveDate";
            this.txtLeaveDate.SelectAllTextOnFocus = true;
            this.txtLeaveDate.Size = new System.Drawing.Size(390, 42);
            this.txtLeaveDate.TabIndex = 1;
            this.txtLeaveDate.ToolTips = "";
            this.txtLeaveDate.WaterMarkText = null;
            // 
            // cLabel10
            // 
            this.cLabel10.AutoSize = true;
            this.cLabel10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel10.ForeColor = System.Drawing.Color.Black;
            this.cLabel10.Location = new System.Drawing.Point(11, 346);
            this.cLabel10.Name = "cLabel10";
            this.cLabel10.Size = new System.Drawing.Size(88, 14);
            this.cLabel10.TabIndex = 138;
            this.cLabel10.Text = "Date && Time";
            this.cLabel10.ToolTips = "";
            // 
            // txtLeaveDays
            // 
            this.txtLeaveDays.ActivationColor = true;
            this.txtLeaveDays.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtLeaveDays.AllowTabKeyOnEnter = false;
            this.txtLeaveDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLeaveDays.Enabled = false;
            this.txtLeaveDays.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtLeaveDays.Format = "";
            this.txtLeaveDays.IsComplusory = false;
            this.txtLeaveDays.Location = new System.Drawing.Point(105, 394);
            this.txtLeaveDays.Name = "txtLeaveDays";
            this.txtLeaveDays.SelectAllTextOnFocus = true;
            this.txtLeaveDays.Size = new System.Drawing.Size(390, 24);
            this.txtLeaveDays.TabIndex = 1;
            this.txtLeaveDays.ToolTips = "";
            this.txtLeaveDays.WaterMarkText = null;
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(11, 399);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(76, 14);
            this.cLabel6.TabIndex = 138;
            this.cLabel6.Text = "Days / HH";
            this.cLabel6.ToolTips = "";
            // 
            // FrmLeaveApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 665);
            this.Controls.Add(this.PnlTop);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cLabel5);
            this.Controls.Add(this.cLabel6);
            this.Controls.Add(this.cLabel10);
            this.Controls.Add(this.cLabel4);
            this.Controls.Add(this.PicEmpPhoto);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtLeaveDays);
            this.Controls.Add(this.txtLeaveDate);
            this.Controls.Add(this.txtOtherReason);
            this.Controls.Add(this.cLabel3);
            this.Controls.Add(this.txtEmployeeName);
            this.Controls.Add(this.txtEmployeeCode);
            this.Controls.Add(this.cLabel2);
            this.Controls.Add(this.ChkISOfficeWork);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.Name = "FrmLeaveApproval";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "LeaveApproval";
            this.Text = "LEAVE APPROVAL";
            ((System.ComponentModel.ISupportInitialize)(this.PicEmpPhoto)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.PnlTop.ResumeLayout(false);
            this.PnlTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxonContLib.cDateTimePicker DTPSlipDate;
        private AxonContLib.cLabel labelControl8;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cTextBox txtSlipNo;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox txtEmployeeCode;
        private AxonContLib.cTextBox txtEmployeeName;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cTextBox txtReason;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cTextBox txtOtherReason;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cTextBox txtRemark;
        private AxonContLib.cCheckBox ChkISOfficeWork;
        private DevExpress.XtraEditors.SimpleButton BtnApproved;
        private DevExpress.XtraEditors.SimpleButton BtnCancle;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private System.Windows.Forms.PictureBox PicEmpPhoto;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cLabel lblLeaveType;
        private AxonContLib.cPanel PnlTop;
        private AxonContLib.cTextBox txtLeaveDate;
        private AxonContLib.cLabel cLabel10;
        private AxonContLib.cTextBox txtLeaveDays;
        private AxonContLib.cLabel cLabel6;
        private AxonContLib.cLabel cLabel7;
        private AxonContLib.cTextBox txtComment;
        private AxonContLib.cPanel panel3;
        private AxonContLib.cLabel cLabel8;
        private AxonContLib.cLabel lblEntryDate;
        private AxonContLib.cLabel cLabel11;
        private AxonContLib.cLabel lblEntryBy;
        private AxonContLib.cLabel lblEntryIP;
        private AxonContLib.cLabel cLabel13;


    }
}