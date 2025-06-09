namespace AxoneMFGRJ.Utility
{
    partial class FrmPassword
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.BtnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new AxonContLib.cTextBox(this.components);
            this.BtnCancle = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Cambria", 12F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.BtnLogin);
            this.groupControl1.Controls.Add(this.txtPassword);
            this.groupControl1.Controls.Add(this.BtnCancle);
            this.groupControl1.Location = new System.Drawing.Point(12, 11);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(232, 124);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Enter Password";
            // 
            // BtnLogin
            // 
            this.BtnLogin.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnLogin.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnLogin.Appearance.Options.UseFont = true;
            this.BtnLogin.Appearance.Options.UseForeColor = true;
            this.BtnLogin.Location = new System.Drawing.Point(21, 74);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(87, 35);
            this.BtnLogin.TabIndex = 1;
            this.BtnLogin.Text = "OK";
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.ActivationColor = true;
            this.txtPassword.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtPassword.AllowTabKeyOnEnter = false;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPassword.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Format = "";
            this.txtPassword.IsComplusory = false;
            this.txtPassword.Location = new System.Drawing.Point(21, 34);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.SelectAllTextOnFocus = true;
            this.txtPassword.Size = new System.Drawing.Size(188, 31);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.Tag = "RJ123";
            this.txtPassword.ToolTips = "";
            this.txtPassword.WaterMarkText = null;
            // 
            // BtnCancle
            // 
            this.BtnCancle.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnCancle.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancle.Appearance.Options.UseFont = true;
            this.BtnCancle.Appearance.Options.UseForeColor = true;
            this.BtnCancle.Location = new System.Drawing.Point(122, 74);
            this.BtnCancle.Name = "BtnCancle";
            this.BtnCancle.Size = new System.Drawing.Size(87, 35);
            this.BtnCancle.TabIndex = 2;
            this.BtnCancle.Text = "Close";
            this.BtnCancle.Click += new System.EventHandler(this.BtnCancle_Click);
            // 
            // FrmPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 146);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmPassword";
            this.Text = "PASSWORD";
            this.Load += new System.EventHandler(this.FrmPassword_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPassword_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnLogin;
        private AxonContLib.cTextBox txtPassword;
        private DevExpress.XtraEditors.SimpleButton BtnCancle;

    }
}