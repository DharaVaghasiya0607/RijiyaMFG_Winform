namespace AxoneMFGRJ.Utility
{
    partial class FrmUserConfig
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
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtLocalDownloadPath = new AxonContLib.cTextBox(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.txtLocalOutputPath = new AxonContLib.cTextBox(this.components);
            this.SuspendLayout();
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Location = new System.Drawing.Point(88, 157);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(118, 48);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnClose.Appearance.Options.UseFont = true;
            this.BtnClose.Location = new System.Drawing.Point(222, 157);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(118, 48);
            this.BtnClose.TabIndex = 10;
            this.BtnClose.TabStop = false;
            this.BtnClose.Text = "&Close Me";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // txtLocalDownloadPath
            // 
            this.txtLocalDownloadPath.ActivationColor = true;
            this.txtLocalDownloadPath.AllowTabKeyOnEnter = false;
            this.txtLocalDownloadPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocalDownloadPath.ComplusoryMsg = null;
            this.txtLocalDownloadPath.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtLocalDownloadPath.Format = "";
            this.txtLocalDownloadPath.IsComplusory = false;
            this.txtLocalDownloadPath.Location = new System.Drawing.Point(21, 46);
            this.txtLocalDownloadPath.Name = "txtLocalDownloadPath";
            this.txtLocalDownloadPath.RequiredChars = "";
            this.txtLocalDownloadPath.SelectAllTextOnFocus = true;
            this.txtLocalDownloadPath.ShowToolTipOnFocus = false;
            this.txtLocalDownloadPath.Size = new System.Drawing.Size(449, 22);
            this.txtLocalDownloadPath.TabIndex = 7;
            this.txtLocalDownloadPath.ToolTips = "";
            this.txtLocalDownloadPath.WaterMarkText = null;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.BackColor = System.Drawing.Color.Transparent;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(18, 23);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(139, 14);
            this.cLabel2.TabIndex = 6;
            this.cLabel2.Text = "Local Download Path";
            this.cLabel2.ToolTips = "";
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.BackColor = System.Drawing.Color.Transparent;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(18, 86);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(120, 14);
            this.cLabel5.TabIndex = 8;
            this.cLabel5.Text = "Local Output Path";
            this.cLabel5.ToolTips = "";
            // 
            // txtLocalOutputPath
            // 
            this.txtLocalOutputPath.ActivationColor = true;
            this.txtLocalOutputPath.AllowTabKeyOnEnter = false;
            this.txtLocalOutputPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocalOutputPath.ComplusoryMsg = null;
            this.txtLocalOutputPath.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtLocalOutputPath.Format = "";
            this.txtLocalOutputPath.IsComplusory = false;
            this.txtLocalOutputPath.Location = new System.Drawing.Point(21, 108);
            this.txtLocalOutputPath.Name = "txtLocalOutputPath";
            this.txtLocalOutputPath.RequiredChars = "";
            this.txtLocalOutputPath.SelectAllTextOnFocus = true;
            this.txtLocalOutputPath.ShowToolTipOnFocus = false;
            this.txtLocalOutputPath.Size = new System.Drawing.Size(449, 22);
            this.txtLocalOutputPath.TabIndex = 9;
            this.txtLocalOutputPath.ToolTips = "";
            this.txtLocalOutputPath.WaterMarkText = null;
            // 
            // FrmUserConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 229);
            this.Controls.Add(this.txtLocalOutputPath);
            this.Controls.Add(this.txtLocalDownloadPath);
            this.Controls.Add(this.cLabel5);
            this.Controls.Add(this.cLabel2);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnClose);
            this.Name = "FrmUserConfig";
            this.Text = "USER CONNECTION MASTER";
            this.Load += new System.EventHandler(this.FrmUserConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnClose;
        private AxonContLib.cTextBox txtLocalDownloadPath;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cTextBox txtLocalOutputPath;

    }
}