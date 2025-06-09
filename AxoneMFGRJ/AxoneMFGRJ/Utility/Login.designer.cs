namespace AxoneMFGRJ.Utility
{
    partial class FrmLogin
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
            this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.BtnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtPassForDisplayTransferTick = new AxonContLib.cTextBox(this.components);
            this.ChkTransferDBConnection = new AxonContLib.cCheckBox(this.components);
            this.lblConn = new AxonContLib.cLabel(this.components);
            this.lblVersion = new AxonContLib.cLabel(this.components);
            this.txtUserName = new AxonContLib.cTextBox(this.components);
            this.txtPassWord = new AxonContLib.cTextBox(this.components);
            this.PnlRegularDBConnection = new AxonContLib.cPanel(this.components);
            this.txtConnectionString = new AxonContLib.cTextBox(this.components);
            this.BtnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCompanyName = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.PnlRegularDBConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnClose.Appearance.Options.UseFont = true;
            this.BtnClose.Location = new System.Drawing.Point(199, 141);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(178, 52);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.Text = "&Close Me";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnLogin.Appearance.Options.UseFont = true;
            this.BtnLogin.Location = new System.Drawing.Point(15, 141);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(178, 52);
            this.BtnLogin.TabIndex = 3;
            this.BtnLogin.Text = "&Login";
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Cambria", 12F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.txtPassForDisplayTransferTick);
            this.groupControl1.Controls.Add(this.ChkTransferDBConnection);
            this.groupControl1.Controls.Add(this.lblConn);
            this.groupControl1.Controls.Add(this.lblVersion);
            this.groupControl1.Controls.Add(this.BtnClose);
            this.groupControl1.Controls.Add(this.BtnLogin);
            this.groupControl1.Controls.Add(this.txtUserName);
            this.groupControl1.Controls.Add(this.txtPassWord);
            this.groupControl1.Location = new System.Drawing.Point(174, 322);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(394, 233);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Enter Your Username && Password";
            // 
            // txtPassForDisplayTransferTick
            // 
            this.txtPassForDisplayTransferTick.ActivationColor = false;
            this.txtPassForDisplayTransferTick.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtPassForDisplayTransferTick.AllowTabKeyOnEnter = false;
            this.txtPassForDisplayTransferTick.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPassForDisplayTransferTick.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassForDisplayTransferTick.Format = "";
            this.txtPassForDisplayTransferTick.IsComplusory = false;
            this.txtPassForDisplayTransferTick.Location = new System.Drawing.Point(331, 196);
            this.txtPassForDisplayTransferTick.Name = "txtPassForDisplayTransferTick";
            this.txtPassForDisplayTransferTick.PasswordChar = '*';
            this.txtPassForDisplayTransferTick.SelectAllTextOnFocus = true;
            this.txtPassForDisplayTransferTick.Size = new System.Drawing.Size(45, 14);
            this.txtPassForDisplayTransferTick.TabIndex = 185;
            this.txtPassForDisplayTransferTick.Tag = "AXONE";
            this.txtPassForDisplayTransferTick.ToolTips = "";
            this.txtPassForDisplayTransferTick.WaterMarkText = null;
            this.txtPassForDisplayTransferTick.TextChanged += new System.EventHandler(this.txtPassForDisplayTransferTick_TextChanged);
            // 
            // ChkTransferDBConnection
            // 
            this.ChkTransferDBConnection.AllowTabKeyOnEnter = false;
            this.ChkTransferDBConnection.AutoSize = true;
            this.ChkTransferDBConnection.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.ChkTransferDBConnection.Location = new System.Drawing.Point(16, 196);
            this.ChkTransferDBConnection.Name = "ChkTransferDBConnection";
            this.ChkTransferDBConnection.Size = new System.Drawing.Size(179, 17);
            this.ChkTransferDBConnection.TabIndex = 184;
            this.ChkTransferDBConnection.TabStop = false;
            this.ChkTransferDBConnection.Text = "Transfer DB Connection";
            this.ChkTransferDBConnection.ToolTips = "If Apply Any Difference";
            this.ChkTransferDBConnection.UseVisualStyleBackColor = true;
            // 
            // lblConn
            // 
            this.lblConn.BackColor = System.Drawing.Color.Transparent;
            this.lblConn.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblConn.Location = new System.Drawing.Point(308, 216);
            this.lblConn.Name = "lblConn";
            this.lblConn.Size = new System.Drawing.Size(84, 14);
            this.lblConn.TabIndex = 26;
            this.lblConn.Text = "Connection";
            this.lblConn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblConn.ToolTips = "";
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblVersion.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblVersion.Location = new System.Drawing.Point(2, 214);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(390, 17);
            this.lblVersion.TabIndex = 25;
            this.lblVersion.Text = "1.1.1.9";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVersion.ToolTips = "";
            // 
            // txtUserName
            // 
            this.txtUserName.ActivationColor = true;
            this.txtUserName.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtUserName.AllowTabKeyOnEnter = false;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Format = "";
            this.txtUserName.IsComplusory = false;
            this.txtUserName.Location = new System.Drawing.Point(15, 44);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.SelectAllTextOnFocus = true;
            this.txtUserName.Size = new System.Drawing.Size(361, 31);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.ToolTips = "";
            this.txtUserName.WaterMarkText = null;
            // 
            // txtPassWord
            // 
            this.txtPassWord.ActivationColor = true;
            this.txtPassWord.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtPassWord.AllowTabKeyOnEnter = false;
            this.txtPassWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassWord.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassWord.Format = "";
            this.txtPassWord.IsComplusory = false;
            this.txtPassWord.Location = new System.Drawing.Point(15, 93);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.SelectAllTextOnFocus = true;
            this.txtPassWord.Size = new System.Drawing.Size(361, 31);
            this.txtPassWord.TabIndex = 1;
            this.txtPassWord.ToolTips = "";
            this.txtPassWord.WaterMarkText = null;
            // 
            // PnlRegularDBConnection
            // 
            this.PnlRegularDBConnection.BackColor = System.Drawing.Color.DimGray;
            this.PnlRegularDBConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlRegularDBConnection.Controls.Add(this.txtConnectionString);
            this.PnlRegularDBConnection.Controls.Add(this.BtnUpdate);
            this.PnlRegularDBConnection.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlRegularDBConnection.Location = new System.Drawing.Point(0, 567);
            this.PnlRegularDBConnection.Name = "PnlRegularDBConnection";
            this.PnlRegularDBConnection.Size = new System.Drawing.Size(736, 60);
            this.PnlRegularDBConnection.TabIndex = 26;
            this.PnlRegularDBConnection.Visible = false;
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.ActivationColor = true;
            this.txtConnectionString.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtConnectionString.AllowTabKeyOnEnter = false;
            this.txtConnectionString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConnectionString.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionString.Format = "";
            this.txtConnectionString.IsComplusory = false;
            this.txtConnectionString.Location = new System.Drawing.Point(3, 3);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.SelectAllTextOnFocus = true;
            this.txtConnectionString.Size = new System.Drawing.Size(660, 52);
            this.txtConnectionString.TabIndex = 0;
            this.txtConnectionString.ToolTips = "";
            this.txtConnectionString.Visible = false;
            this.txtConnectionString.WaterMarkText = null;
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUpdate.Appearance.Options.UseFont = true;
            this.BtnUpdate.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnUpdate.Location = new System.Drawing.Point(664, 0);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(70, 58);
            this.BtnUpdate.TabIndex = 3;
            this.BtnUpdate.Text = "Update";
            this.BtnUpdate.Visible = false;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(174, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(394, 229);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.Navy;
            this.lblCompanyName.Location = new System.Drawing.Point(59, 278);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(624, 34);
            this.lblCompanyName.TabIndex = 27;
            this.lblCompanyName.Text = "Company Name";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCompanyName.ToolTips = "";
            // 
            // cLabel1
            // 
            this.cLabel1.BackColor = System.Drawing.Color.Teal;
            this.cLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cLabel1.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.White;
            this.cLabel1.Location = new System.Drawing.Point(0, 0);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(736, 35);
            this.cLabel1.TabIndex = 24;
            this.cLabel1.Text = "Welcome To Diamond Manufacturing Program";
            this.cLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel1.ToolTips = "";
            // 
            // FrmLogin
            // 
            this.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 627);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.PnlRegularDBConnection);
            this.Controls.Add(this.cLabel1);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.LookAndFeel.SkinName = "Caramel";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLogin_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.PnlRegularDBConnection.ResumeLayout(false);
            this.PnlRegularDBConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cTextBox txtUserName;
        private AxonContLib.cTextBox txtPassWord;
        private AxonContLib.cLabel cLabel1;
        private DevExpress.XtraEditors.SimpleButton BtnClose;
        private DevExpress.XtraEditors.SimpleButton BtnLogin;
        private AxonContLib.cLabel lblVersion;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private AxonContLib.cPanel PnlRegularDBConnection;
        private AxonContLib.cLabel lblCompanyName;
        private AxonContLib.cTextBox txtConnectionString;
        private DevExpress.XtraEditors.SimpleButton BtnUpdate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AxonContLib.cLabel lblConn;
        private AxonContLib.cCheckBox ChkTransferDBConnection;
        private AxonContLib.cTextBox txtPassForDisplayTransferTick;

    }
}