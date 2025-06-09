namespace AxoneMFGRJ.Utility
{
    partial class FrmJangedUpdate
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
            this.DtpTransdate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.txtJangedNo = new AxonContLib.cTextBox(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.BtnToProcessUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.txtToProcess = new AxonContLib.cTextBox(this.components);
            this.BtnToProcessClear = new DevExpress.XtraEditors.SimpleButton();
            this.BtnUpdateToEmployee = new DevExpress.XtraEditors.SimpleButton();
            this.txtToEmployee = new AxonContLib.cTextBox(this.components);
            this.BtnToEmployeeClear = new DevExpress.XtraEditors.SimpleButton();
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
            this.groupControl1.Controls.Add(this.DtpTransdate);
            this.groupControl1.Controls.Add(this.cLabel2);
            this.groupControl1.Controls.Add(this.txtJangedNo);
            this.groupControl1.Controls.Add(this.cLabel1);
            this.groupControl1.Controls.Add(this.cLabel3);
            this.groupControl1.Controls.Add(this.BtnToProcessUpdate);
            this.groupControl1.Controls.Add(this.txtToProcess);
            this.groupControl1.Controls.Add(this.BtnToProcessClear);
            this.groupControl1.Controls.Add(this.BtnUpdateToEmployee);
            this.groupControl1.Controls.Add(this.txtToEmployee);
            this.groupControl1.Controls.Add(this.BtnToEmployeeClear);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(597, 159);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Janged Wise Update";
            // 
            // DtpTransdate
            // 
            this.DtpTransdate.AllowTabKeyOnEnter = false;
            this.DtpTransdate.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            this.DtpTransdate.Enabled = false;
            this.DtpTransdate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DtpTransdate.ForeColor = System.Drawing.Color.Black;
            this.DtpTransdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpTransdate.Location = new System.Drawing.Point(278, 33);
            this.DtpTransdate.Name = "DtpTransdate";
            this.DtpTransdate.Size = new System.Drawing.Size(226, 24);
            this.DtpTransdate.TabIndex = 18;
            this.DtpTransdate.TabStop = false;
            this.DtpTransdate.ToolTips = "";
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(11, 39);
            this.cLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(74, 13);
            this.cLabel2.TabIndex = 9;
            this.cLabel2.Text = "Janged No";
            this.cLabel2.ToolTips = "";
            // 
            // txtJangedNo
            // 
            this.txtJangedNo.ActivationColor = true;
            this.txtJangedNo.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtJangedNo.AllowTabKeyOnEnter = false;
            this.txtJangedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJangedNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJangedNo.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJangedNo.Format = "";
            this.txtJangedNo.IsComplusory = false;
            this.txtJangedNo.Location = new System.Drawing.Point(108, 30);
            this.txtJangedNo.Name = "txtJangedNo";
            this.txtJangedNo.PasswordChar = '*';
            this.txtJangedNo.SelectAllTextOnFocus = true;
            this.txtJangedNo.Size = new System.Drawing.Size(164, 31);
            this.txtJangedNo.TabIndex = 8;
            this.txtJangedNo.Tag = "RJ123";
            this.txtJangedNo.ToolTips = "";
            this.txtJangedNo.WaterMarkText = null;
            this.txtJangedNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJangedNo_KeyPress);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(11, 113);
            this.cLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(78, 13);
            this.cLabel1.TabIndex = 7;
            this.cLabel1.Text = "To Process";
            this.cLabel1.ToolTips = "";
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(11, 76);
            this.cLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(91, 13);
            this.cLabel3.TabIndex = 6;
            this.cLabel3.Text = "To Employee";
            this.cLabel3.ToolTips = "";
            // 
            // BtnToProcessUpdate
            // 
            this.BtnToProcessUpdate.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnToProcessUpdate.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnToProcessUpdate.Appearance.Options.UseFont = true;
            this.BtnToProcessUpdate.Appearance.Options.UseForeColor = true;
            this.BtnToProcessUpdate.Location = new System.Drawing.Point(417, 104);
            this.BtnToProcessUpdate.Name = "BtnToProcessUpdate";
            this.BtnToProcessUpdate.Size = new System.Drawing.Size(87, 35);
            this.BtnToProcessUpdate.TabIndex = 4;
            this.BtnToProcessUpdate.Text = "Update";
            // 
            // txtToProcess
            // 
            this.txtToProcess.ActivationColor = true;
            this.txtToProcess.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtToProcess.AllowTabKeyOnEnter = false;
            this.txtToProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToProcess.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtToProcess.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToProcess.Format = "";
            this.txtToProcess.IsComplusory = false;
            this.txtToProcess.Location = new System.Drawing.Point(108, 104);
            this.txtToProcess.Name = "txtToProcess";
            this.txtToProcess.PasswordChar = '*';
            this.txtToProcess.SelectAllTextOnFocus = true;
            this.txtToProcess.Size = new System.Drawing.Size(299, 31);
            this.txtToProcess.TabIndex = 3;
            this.txtToProcess.Tag = "RJ123";
            this.txtToProcess.ToolTips = "";
            this.txtToProcess.WaterMarkText = null;
            this.txtToProcess.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToProcess_KeyPress);
            // 
            // BtnToProcessClear
            // 
            this.BtnToProcessClear.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnToProcessClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnToProcessClear.Appearance.Options.UseFont = true;
            this.BtnToProcessClear.Appearance.Options.UseForeColor = true;
            this.BtnToProcessClear.Location = new System.Drawing.Point(506, 104);
            this.BtnToProcessClear.Name = "BtnToProcessClear";
            this.BtnToProcessClear.Size = new System.Drawing.Size(87, 35);
            this.BtnToProcessClear.TabIndex = 5;
            this.BtnToProcessClear.Text = "Clear";
            // 
            // BtnUpdateToEmployee
            // 
            this.BtnUpdateToEmployee.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUpdateToEmployee.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnUpdateToEmployee.Appearance.Options.UseFont = true;
            this.BtnUpdateToEmployee.Appearance.Options.UseForeColor = true;
            this.BtnUpdateToEmployee.Location = new System.Drawing.Point(417, 67);
            this.BtnUpdateToEmployee.Name = "BtnUpdateToEmployee";
            this.BtnUpdateToEmployee.Size = new System.Drawing.Size(87, 35);
            this.BtnUpdateToEmployee.TabIndex = 1;
            this.BtnUpdateToEmployee.Text = "Update";
            this.BtnUpdateToEmployee.Click += new System.EventHandler(this.BtnUpdateToEmployee_Click);
            // 
            // txtToEmployee
            // 
            this.txtToEmployee.ActivationColor = true;
            this.txtToEmployee.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtToEmployee.AllowTabKeyOnEnter = false;
            this.txtToEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtToEmployee.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToEmployee.Format = "";
            this.txtToEmployee.IsComplusory = false;
            this.txtToEmployee.Location = new System.Drawing.Point(108, 67);
            this.txtToEmployee.Name = "txtToEmployee";
            this.txtToEmployee.PasswordChar = '*';
            this.txtToEmployee.SelectAllTextOnFocus = true;
            this.txtToEmployee.Size = new System.Drawing.Size(299, 31);
            this.txtToEmployee.TabIndex = 0;
            this.txtToEmployee.Tag = "RJ123";
            this.txtToEmployee.ToolTips = "";
            this.txtToEmployee.WaterMarkText = null;
            this.txtToEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToEmployee_KeyPress);
            // 
            // BtnToEmployeeClear
            // 
            this.BtnToEmployeeClear.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnToEmployeeClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnToEmployeeClear.Appearance.Options.UseFont = true;
            this.BtnToEmployeeClear.Appearance.Options.UseForeColor = true;
            this.BtnToEmployeeClear.Location = new System.Drawing.Point(506, 67);
            this.BtnToEmployeeClear.Name = "BtnToEmployeeClear";
            this.BtnToEmployeeClear.Size = new System.Drawing.Size(87, 35);
            this.BtnToEmployeeClear.TabIndex = 2;
            this.BtnToEmployeeClear.Text = "Clear";
            // 
            // FrmJangedUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 159);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmJangedUpdate";
            this.Text = "JANGED UPDATE";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnUpdateToEmployee;
        private AxonContLib.cTextBox txtToEmployee;
        private DevExpress.XtraEditors.SimpleButton BtnToEmployeeClear;
        private DevExpress.XtraEditors.SimpleButton BtnToProcessUpdate;
        private AxonContLib.cTextBox txtToProcess;
        private DevExpress.XtraEditors.SimpleButton BtnToProcessClear;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox txtJangedNo;
        private AxonContLib.cDateTimePicker DtpTransdate;
    }
}