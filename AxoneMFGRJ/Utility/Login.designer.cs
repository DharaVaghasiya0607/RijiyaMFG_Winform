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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.BtnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblVersion = new AxonContLib.cLabel(this.components);
            this.txtUserName = new AxonContLib.cTextBox(this.components);
            this.txtPassWord = new AxonContLib.cTextBox(this.components);
            this.PnlRegularDBConnection = new AxonContLib.cPanel(this.components);
            this.txtConnectionString = new AxonContLib.cTextBox(this.components);
            this.BtnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.PnlAxone = new AxonContLib.cPanel(this.components);
            this.cPanel3 = new AxonContLib.cPanel(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.cPanel1 = new AxonContLib.cPanel(this.components);
            this.cPanel2 = new AxonContLib.cPanel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.PnlRegularDBConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.PnlAxone.SuspendLayout();
            this.cPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Appearance.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold);
            this.BtnClose.Appearance.Options.UseFont = true;
            this.BtnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnClose.ImageOptions.Image")));
            this.BtnClose.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.BtnClose.Location = new System.Drawing.Point(591, 48);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(33, 30);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Appearance.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold);
            this.BtnLogin.Appearance.Options.UseFont = true;
            this.BtnLogin.Location = new System.Drawing.Point(484, 48);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(100, 30);
            this.BtnLogin.TabIndex = 3;
            this.BtnLogin.Text = "&Login";
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Cambria", 10F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.lblVersion);
            this.groupControl1.Controls.Add(this.BtnClose);
            this.groupControl1.Controls.Add(this.BtnLogin);
            this.groupControl1.Controls.Add(this.txtUserName);
            this.groupControl1.Controls.Add(this.txtPassWord);
            this.groupControl1.Location = new System.Drawing.Point(19, 271);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(648, 112);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Enter Your Username && Password";
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblVersion.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblVersion.Location = new System.Drawing.Point(2, 93);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(644, 17);
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
            this.txtUserName.Location = new System.Drawing.Point(25, 48);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.SelectAllTextOnFocus = true;
            this.txtUserName.Size = new System.Drawing.Size(225, 31);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.txtPassWord.Location = new System.Drawing.Point(255, 48);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.SelectAllTextOnFocus = true;
            this.txtPassWord.Size = new System.Drawing.Size(225, 31);
            this.txtPassWord.TabIndex = 1;
            this.txtPassWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassWord.ToolTips = "";
            this.txtPassWord.WaterMarkText = null;
            // 
            // PnlRegularDBConnection
            // 
            this.PnlRegularDBConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(78)))), ((int)(((byte)(150)))));
            this.PnlRegularDBConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlRegularDBConnection.Controls.Add(this.txtConnectionString);
            this.PnlRegularDBConnection.Controls.Add(this.BtnUpdate);
            this.PnlRegularDBConnection.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlRegularDBConnection.Location = new System.Drawing.Point(0, 471);
            this.PnlRegularDBConnection.Name = "PnlRegularDBConnection";
            this.PnlRegularDBConnection.Size = new System.Drawing.Size(688, 60);
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
            this.txtConnectionString.Location = new System.Drawing.Point(1, 3);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.SelectAllTextOnFocus = true;
            this.txtConnectionString.Size = new System.Drawing.Size(614, 53);
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
            this.BtnUpdate.Location = new System.Drawing.Point(616, 0);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(70, 58);
            this.BtnUpdate.TabIndex = 3;
            this.BtnUpdate.Text = "Update";
            this.BtnUpdate.Visible = false;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AxoneMFGRJ.Properties.Resources.Background;
            this.pictureBox1.Location = new System.Drawing.Point(19, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(648, 220);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // cLabel1
            // 
            this.cLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(78)))), ((int)(((byte)(150)))));
            this.cLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cLabel1.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.White;
            this.cLabel1.Location = new System.Drawing.Point(0, 0);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(688, 35);
            this.cLabel1.TabIndex = 24;
            this.cLabel1.Text = "Welcome To Diamond Manufacturing Program";
            this.cLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel1.ToolTips = "";
            // 
            // PnlAxone
            // 
            this.PnlAxone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(78)))), ((int)(((byte)(150)))));
            this.PnlAxone.Controls.Add(this.cPanel3);
            this.PnlAxone.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlAxone.Location = new System.Drawing.Point(0, 388);
            this.PnlAxone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PnlAxone.Name = "PnlAxone";
            this.PnlAxone.Size = new System.Drawing.Size(688, 83);
            this.PnlAxone.TabIndex = 77;
            // 
            // cPanel3
            // 
            this.cPanel3.Controls.Add(this.cLabel4);
            this.cPanel3.Controls.Add(this.cLabel2);
            this.cPanel3.Controls.Add(this.cLabel3);
            this.cPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.cPanel3.Location = new System.Drawing.Point(139, 0);
            this.cPanel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cPanel3.Name = "cPanel3";
            this.cPanel3.Size = new System.Drawing.Size(549, 83);
            this.cPanel3.TabIndex = 76;
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(78)))), ((int)(((byte)(150)))));
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.cLabel4.ForeColor = System.Drawing.Color.LightGray;
            this.cLabel4.Location = new System.Drawing.Point(110, 6);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(193, 12);
            this.cLabel4.TabIndex = 75;
            this.cLabel4.Text = "Software Design && Developed By";
            this.cLabel4.ToolTips = "";
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(78)))), ((int)(((byte)(150)))));
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.cLabel2.ForeColor = System.Drawing.Color.White;
            this.cLabel2.Location = new System.Drawing.Point(133, 60);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(148, 12);
            this.cLabel2.TabIndex = 74;
            this.cLabel2.Text = "Committed To The Future";
            this.cLabel2.ToolTips = "";
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(78)))), ((int)(((byte)(150)))));
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.cLabel3.ForeColor = System.Drawing.Color.White;
            this.cLabel3.Location = new System.Drawing.Point(102, 39);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(204, 17);
            this.cLabel3.TabIndex = 73;
            this.cLabel3.Text = "AXONE INFOTECH INDIA";
            this.cLabel3.ToolTips = "";
            // 
            // cPanel1
            // 
            this.cPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(78)))), ((int)(((byte)(150)))));
            this.cPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.cPanel1.Location = new System.Drawing.Point(0, 35);
            this.cPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cPanel1.Name = "cPanel1";
            this.cPanel1.Size = new System.Drawing.Size(9, 353);
            this.cPanel1.TabIndex = 78;
            // 
            // cPanel2
            // 
            this.cPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(78)))), ((int)(((byte)(150)))));
            this.cPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.cPanel2.Location = new System.Drawing.Point(679, 35);
            this.cPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cPanel2.Name = "cPanel2";
            this.cPanel2.Size = new System.Drawing.Size(9, 353);
            this.cPanel2.TabIndex = 79;
            // 
            // FrmLogin
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 531);
            this.Controls.Add(this.cPanel2);
            this.Controls.Add(this.cPanel1);
            this.Controls.Add(this.PnlAxone);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.PnlRegularDBConnection);
            this.Controls.Add(this.cLabel1);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.LookAndFeel.SkinName = "Whiteprint";
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
            this.PnlAxone.ResumeLayout(false);
            this.cPanel3.ResumeLayout(false);
            this.cPanel3.PerformLayout();
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
        private AxonContLib.cTextBox txtConnectionString;
        private DevExpress.XtraEditors.SimpleButton BtnUpdate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AxonContLib.cPanel PnlAxone;
        private AxonContLib.cPanel cPanel3;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cPanel cPanel1;
        private AxonContLib.cPanel cPanel2;

    }
}