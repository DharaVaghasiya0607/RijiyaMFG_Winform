namespace AxoneMFGRJ.PacketTreeDrawing
{
    partial class FrmPacketLookup
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
            this.panel4 = new AxonContLib.cPanel(this.components);
            this.BtnTag = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPacketNo = new DevExpress.XtraEditors.SimpleButton();
            this.BtnKapan = new DevExpress.XtraEditors.SimpleButton();
            this.aLabel2 = new AxonContLib.cLabel(this.components);
            this.txtTag = new AxonContLib.cTextBox(this.components);
            this.txtKapanName = new AxonContLib.cTextBox(this.components);
            this.txtPacketNo = new AxonContLib.cTextBox(this.components);
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPacketLookup = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.Controls.Add(this.BtnTag);
            this.panel4.Controls.Add(this.BtnPacketNo);
            this.panel4.Controls.Add(this.BtnKapan);
            this.panel4.Controls.Add(this.aLabel2);
            this.panel4.Controls.Add(this.txtTag);
            this.panel4.Controls.Add(this.txtKapanName);
            this.panel4.Controls.Add(this.txtPacketNo);
            this.panel4.Controls.Add(this.BtnExport);
            this.panel4.Controls.Add(this.BtnPacketLookup);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1082, 41);
            this.panel4.TabIndex = 0;
            // 
            // BtnTag
            // 
            this.BtnTag.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTag.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnTag.Appearance.Options.UseFont = true;
            this.BtnTag.Appearance.Options.UseForeColor = true;
            this.BtnTag.Location = new System.Drawing.Point(280, 8);
            this.BtnTag.Name = "BtnTag";
            this.BtnTag.Size = new System.Drawing.Size(25, 25);
            this.BtnTag.TabIndex = 101;
            this.BtnTag.TabStop = false;
            this.BtnTag.Text = "..";
            // 
            // BtnPacketNo
            // 
            this.BtnPacketNo.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPacketNo.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnPacketNo.Appearance.Options.UseFont = true;
            this.BtnPacketNo.Appearance.Options.UseForeColor = true;
            this.BtnPacketNo.Location = new System.Drawing.Point(206, 8);
            this.BtnPacketNo.Name = "BtnPacketNo";
            this.BtnPacketNo.Size = new System.Drawing.Size(25, 25);
            this.BtnPacketNo.TabIndex = 102;
            this.BtnPacketNo.TabStop = false;
            this.BtnPacketNo.Text = "..";
            // 
            // BtnKapan
            // 
            this.BtnKapan.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnKapan.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnKapan.Appearance.Options.UseFont = true;
            this.BtnKapan.Appearance.Options.UseForeColor = true;
            this.BtnKapan.Location = new System.Drawing.Point(119, 8);
            this.BtnKapan.Name = "BtnKapan";
            this.BtnKapan.Size = new System.Drawing.Size(25, 25);
            this.BtnKapan.TabIndex = 100;
            this.BtnKapan.TabStop = false;
            this.BtnKapan.Text = "..";
            // 
            // aLabel2
            // 
            this.aLabel2.AutoSize = true;
            this.aLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aLabel2.ForeColor = System.Drawing.Color.Black;
            this.aLabel2.Location = new System.Drawing.Point(12, 14);
            this.aLabel2.Name = "aLabel2";
            this.aLabel2.Size = new System.Drawing.Size(28, 13);
            this.aLabel2.TabIndex = 96;
            this.aLabel2.Text = "Pkt";
            this.aLabel2.ToolTips = "";
            // 
            // txtTag
            // 
            this.txtTag.ActivationColor = false;
            this.txtTag.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtTag.AllowTabKeyOnEnter = false;
            this.txtTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTag.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTag.Format = "";
            this.txtTag.IsComplusory = false;
            this.txtTag.Location = new System.Drawing.Point(238, 9);
            this.txtTag.Name = "txtTag";
            this.txtTag.SelectAllTextOnFocus = true;
            this.txtTag.Size = new System.Drawing.Size(42, 23);
            this.txtTag.TabIndex = 99;
            this.txtTag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTag.ToolTips = "";
            this.txtTag.WaterMarkText = null;
            // 
            // txtKapanName
            // 
            this.txtKapanName.ActivationColor = false;
            this.txtKapanName.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtKapanName.AllowTabKeyOnEnter = false;
            this.txtKapanName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKapanName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKapanName.Format = "";
            this.txtKapanName.IsComplusory = false;
            this.txtKapanName.Location = new System.Drawing.Point(40, 9);
            this.txtKapanName.Name = "txtKapanName";
            this.txtKapanName.SelectAllTextOnFocus = true;
            this.txtKapanName.Size = new System.Drawing.Size(79, 23);
            this.txtKapanName.TabIndex = 97;
            this.txtKapanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKapanName.ToolTips = "";
            this.txtKapanName.WaterMarkText = null;
            // 
            // txtPacketNo
            // 
            this.txtPacketNo.ActivationColor = false;
            this.txtPacketNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtPacketNo.AllowTabKeyOnEnter = false;
            this.txtPacketNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPacketNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPacketNo.Format = "";
            this.txtPacketNo.IsComplusory = false;
            this.txtPacketNo.Location = new System.Drawing.Point(152, 9);
            this.txtPacketNo.Name = "txtPacketNo";
            this.txtPacketNo.SelectAllTextOnFocus = true;
            this.txtPacketNo.Size = new System.Drawing.Size(54, 23);
            this.txtPacketNo.TabIndex = 98;
            this.txtPacketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPacketNo.ToolTips = "";
            this.txtPacketNo.WaterMarkText = null;
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.Location = new System.Drawing.Point(442, 4);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(125, 33);
            this.BtnExport.TabIndex = 30;
            this.BtnExport.Text = ".PDF Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnPacketLookup
            // 
            this.BtnPacketLookup.Appearance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnPacketLookup.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnPacketLookup.Appearance.Options.UseFont = true;
            this.BtnPacketLookup.Appearance.Options.UseForeColor = true;
            this.BtnPacketLookup.Location = new System.Drawing.Point(311, 4);
            this.BtnPacketLookup.Name = "BtnPacketLookup";
            this.BtnPacketLookup.Size = new System.Drawing.Size(125, 33);
            this.BtnPacketLookup.TabIndex = 30;
            this.BtnPacketLookup.Text = "Packet LookUp";
            this.BtnPacketLookup.Click += new System.EventHandler(this.BtnPacketLookup_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1068, 395);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1082, 432);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1074, 401);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "     CHART VIEW     ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1074, 401);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "     TRANSACTIONS     ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FrmPacketLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 473);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmPacketLookup";
            this.Text = "REASON MASTER";
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel panel4;
        private DevExpress.XtraEditors.SimpleButton BtnPacketLookup;
        private DevExpress.XtraEditors.SimpleButton BtnTag;
        private DevExpress.XtraEditors.SimpleButton BtnPacketNo;
        private DevExpress.XtraEditors.SimpleButton BtnKapan;
        private AxonContLib.cLabel aLabel2;
        private AxonContLib.cTextBox txtTag;
        private AxonContLib.cTextBox txtKapanName;
        private AxonContLib.cTextBox txtPacketNo;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private AxonContLib.cPanel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;


    }
}