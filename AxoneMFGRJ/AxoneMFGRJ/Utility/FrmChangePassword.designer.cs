namespace AxoneMFGRJ.Utility
{
    partial class FrmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChangePassword));
            this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.BtnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtOldPassWord = new AxonContLib.cTextBox(this.components);
            this.txtnewPassWord = new AxonContLib.cTextBox(this.components);
            this.lblCompanyName = new AxonContLib.cLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnClose.Appearance.Options.UseFont = true;
            this.BtnClose.Location = new System.Drawing.Point(199, 173);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(178, 52);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.Text = "&Close Me";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnUpdate.Appearance.Options.UseFont = true;
            this.BtnUpdate.Location = new System.Drawing.Point(15, 173);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(178, 52);
            this.BtnUpdate.TabIndex = 3;
            this.BtnUpdate.Text = "&Update";
            this.BtnUpdate.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Cambria", 12F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.BtnClose);
            this.groupControl1.Controls.Add(this.BtnUpdate);
            this.groupControl1.Controls.Add(this.txtOldPassWord);
            this.groupControl1.Controls.Add(this.txtnewPassWord);
            this.groupControl1.Location = new System.Drawing.Point(9, 55);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(394, 246);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Change Password";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(15, 104);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(101, 14);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "New PassWord";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(15, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(94, 14);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Old PassWord";
            // 
            // txtOldPassWord
            // 
            this.txtOldPassWord.ActivationColor = true;
            this.txtOldPassWord.AllowTabKeyOnEnter = true;
            this.txtOldPassWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldPassWord.ComplusoryMsg = null;
            this.txtOldPassWord.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOldPassWord.Format = "";
            this.txtOldPassWord.IsComplusory = false;
            this.txtOldPassWord.Location = new System.Drawing.Point(15, 61);
            this.txtOldPassWord.Name = "txtOldPassWord";
            this.txtOldPassWord.PasswordChar = '*';
            this.txtOldPassWord.RequiredChars = "";
            this.txtOldPassWord.SelectAllTextOnFocus = true;
            this.txtOldPassWord.ShowToolTipOnFocus = false;
            this.txtOldPassWord.Size = new System.Drawing.Size(361, 31);
            this.txtOldPassWord.TabIndex = 1;
            this.txtOldPassWord.ToolTips = "";
            this.txtOldPassWord.WaterMarkText = null;
            // 
            // txtnewPassWord
            // 
            this.txtnewPassWord.ActivationColor = true;
            this.txtnewPassWord.AllowTabKeyOnEnter = true;
            this.txtnewPassWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnewPassWord.ComplusoryMsg = null;
            this.txtnewPassWord.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnewPassWord.Format = "";
            this.txtnewPassWord.IsComplusory = false;
            this.txtnewPassWord.Location = new System.Drawing.Point(15, 125);
            this.txtnewPassWord.Name = "txtnewPassWord";
            this.txtnewPassWord.PasswordChar = '*';
            this.txtnewPassWord.RequiredChars = "";
            this.txtnewPassWord.SelectAllTextOnFocus = true;
            this.txtnewPassWord.ShowToolTipOnFocus = false;
            this.txtnewPassWord.Size = new System.Drawing.Size(361, 31);
            this.txtnewPassWord.TabIndex = 2;
            this.txtnewPassWord.ToolTips = "";
            this.txtnewPassWord.WaterMarkText = null;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Font = new System.Drawing.Font("Cambria", 13F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.Navy;
            this.lblCompanyName.Location = new System.Drawing.Point(12, 9);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(394, 24);
            this.lblCompanyName.TabIndex = 27;
            this.lblCompanyName.Text = "Change PassWord";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCompanyName.ToolTips = "";
            // 
            // FrmChangePassword
            // 
            this.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 312);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.groupControl1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Caramel";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cTextBox txtOldPassWord;
        private AxonContLib.cTextBox txtnewPassWord;
        private DevExpress.XtraEditors.SimpleButton BtnClose;
        private DevExpress.XtraEditors.SimpleButton BtnUpdate;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private AxonContLib.cLabel lblCompanyName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;

    }
}