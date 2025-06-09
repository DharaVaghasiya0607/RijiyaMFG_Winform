namespace AxoneMFGRJ.Utility
{
	partial class FrmExpGoalPopUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExpGoalPopUp));
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.txtGoal = new AxonContLib.cTextBox(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.DTPDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.BtnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cLabel1
            // 
            this.cLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(127)))), ((int)(((byte)(176)))));
            this.cLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.White;
            this.cLabel1.Location = new System.Drawing.Point(0, 0);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(267, 56);
            this.cLabel1.TabIndex = 25;
            this.cLabel1.Text = "Axone Infotech";
            this.cLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel1.ToolTips = "";
            // 
            // txtGoal
            // 
            this.txtGoal.ActivationColor = true;
            this.txtGoal.AllowTabKeyOnEnter = false;
            this.txtGoal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGoal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoal.Format = "######";
            this.txtGoal.IsComplusory = false;
            this.txtGoal.Location = new System.Drawing.Point(99, 75);
            this.txtGoal.Name = "txtGoal";
            this.txtGoal.SelectAllTextOnFocus = true;
            this.txtGoal.Size = new System.Drawing.Size(157, 22);
            this.txtGoal.TabIndex = 17;
            this.txtGoal.Text = "0";
            this.txtGoal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGoal.ToolTips = "";
            this.txtGoal.WaterMarkText = null;
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 10F);
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(22, 78);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(71, 17);
            this.cLabel4.TabIndex = 16;
            this.cLabel4.Text = "Exp.Goal";
            this.cLabel4.ToolTips = "";
            // 
            // DTPDate
            // 
            this.DTPDate.AllowTabKeyOnEnter = false;
            this.DTPDate.Checked = false;
            this.DTPDate.CustomFormat = "dd/MM/yyyy";
            this.DTPDate.Enabled = false;
            this.DTPDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPDate.ForeColor = System.Drawing.Color.Black;
            this.DTPDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPDate.Location = new System.Drawing.Point(99, 38);
            this.DTPDate.Name = "DTPDate";
            this.DTPDate.Size = new System.Drawing.Size(157, 24);
            this.DTPDate.TabIndex = 14;
            this.DTPDate.ToolTips = "";
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 10F);
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(22, 42);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(41, 17);
            this.cLabel5.TabIndex = 15;
            this.cLabel5.Text = "Date";
            this.cLabel5.ToolTips = "";
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Cambria", 12F);
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.BtnOk);
            this.groupControl1.Controls.Add(this.cLabel5);
            this.groupControl1.Controls.Add(this.txtGoal);
            this.groupControl1.Controls.Add(this.DTPDate);
            this.groupControl1.Controls.Add(this.cLabel4);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 56);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(267, 171);
            this.groupControl1.TabIndex = 26;
            this.groupControl1.Text = "Set You Today\'S Goal";
            // 
            // BtnOk
            // 
            this.BtnOk.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnOk.Appearance.Options.UseFont = true;
            this.BtnOk.Image = ((System.Drawing.Image)(resources.GetObject("BtnOk.Image")));
            this.BtnOk.Location = new System.Drawing.Point(25, 112);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(128, 43);
            this.BtnOk.TabIndex = 4;
            this.BtnOk.Text = "&Ok";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // FrmExpGoalPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(267, 227);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.cLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmExpGoalPopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EXPECTED GOAL";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cTextBox txtGoal;
        private AxonContLib.cLabel cLabel4;
        private AxonContLib.cDateTimePicker DTPDate;
        private AxonContLib.cLabel cLabel5;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnOk;

    }
}