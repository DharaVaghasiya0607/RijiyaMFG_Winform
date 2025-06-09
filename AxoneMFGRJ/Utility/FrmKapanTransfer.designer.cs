namespace AxoneMFGRJ.Utility
{
    partial class FrmKapanTransfer
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
            this.bgWorkerMaster = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressPanelTransactionDelete = new DevExpress.XtraWaitForm.ProgressPanel();
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.BtnDeleteTransaction = new AxonContLib.cButton(this.components);
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.LblSalesDetail = new AxonContLib.cLabel(this.components);
            this.progressPanelTransaction = new DevExpress.XtraWaitForm.ProgressPanel();
            this.BtnTransferTransaction = new AxonContLib.cButton(this.components);
            this.lblMessageTransaction = new AxonContLib.cLabel(this.components);
            this.txtPassForEnableDelete = new AxonContLib.cTextBox(this.components);
            this.bgWorkerTransaction = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerPricing = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerTransactionDelete = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressPanelTransactionDelete);
            this.panel1.Controls.Add(this.CmbKapan);
            this.panel1.Controls.Add(this.cLabel9);
            this.panel1.Controls.Add(this.BtnDeleteTransaction);
            this.panel1.Controls.Add(this.panelControl3);
            this.panel1.Controls.Add(this.progressPanelTransaction);
            this.panel1.Controls.Add(this.BtnTransferTransaction);
            this.panel1.Controls.Add(this.lblMessageTransaction);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(406, 241);
            this.panel1.TabIndex = 190;
            // 
            // progressPanelTransactionDelete
            // 
            this.progressPanelTransactionDelete.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanelTransactionDelete.Appearance.Options.UseBackColor = true;
            this.progressPanelTransactionDelete.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanelTransactionDelete.AppearanceCaption.Options.UseFont = true;
            this.progressPanelTransactionDelete.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanelTransactionDelete.AppearanceDescription.Options.UseFont = true;
            this.progressPanelTransactionDelete.BarAnimationElementThickness = 2;
            this.progressPanelTransactionDelete.Location = new System.Drawing.Point(203, 155);
            this.progressPanelTransactionDelete.Name = "progressPanelTransactionDelete";
            this.progressPanelTransactionDelete.Size = new System.Drawing.Size(134, 45);
            this.progressPanelTransactionDelete.TabIndex = 193;
            this.progressPanelTransactionDelete.Text = "progressPanel1";
            this.progressPanelTransactionDelete.Visible = false;
            // 
            // CmbKapan
            // 
            this.CmbKapan.Location = new System.Drawing.Point(105, 30);
            this.CmbKapan.Name = "CmbKapan";
            this.CmbKapan.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.CmbKapan.Properties.Appearance.Options.UseFont = true;
            this.CmbKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbKapan.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbKapan.Properties.DropDownRows = 20;
            this.CmbKapan.Properties.IncrementalSearch = true;
            this.CmbKapan.Size = new System.Drawing.Size(254, 22);
            this.CmbKapan.TabIndex = 190;
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(39, 33);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(53, 14);
            this.cLabel9.TabIndex = 189;
            this.cLabel9.Text = "KAPAN";
            this.cLabel9.ToolTips = "";
            // 
            // BtnDeleteTransaction
            // 
            this.BtnDeleteTransaction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteTransaction.ForeColor = System.Drawing.Color.Navy;
            this.BtnDeleteTransaction.Location = new System.Drawing.Point(42, 139);
            this.BtnDeleteTransaction.Name = "BtnDeleteTransaction";
            this.BtnDeleteTransaction.Size = new System.Drawing.Size(155, 75);
            this.BtnDeleteTransaction.TabIndex = 191;
            this.BtnDeleteTransaction.Text = "DELETE";
            this.BtnDeleteTransaction.ToolTips = "";
            this.BtnDeleteTransaction.UseVisualStyleBackColor = true;
            this.BtnDeleteTransaction.Click += new System.EventHandler(this.BtnDeleteTransaction_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.MistyRose;
            this.panelControl3.Appearance.BackColor2 = System.Drawing.Color.PaleTurquoise;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.Controls.Add(this.LblSalesDetail);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.panelControl3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(406, 27);
            this.panelControl3.TabIndex = 246;
            // 
            // LblSalesDetail
            // 
            this.LblSalesDetail.BackColor = System.Drawing.Color.Transparent;
            this.LblSalesDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblSalesDetail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSalesDetail.ForeColor = System.Drawing.Color.MidnightBlue;
            this.LblSalesDetail.Location = new System.Drawing.Point(3, 3);
            this.LblSalesDetail.Name = "LblSalesDetail";
            this.LblSalesDetail.Size = new System.Drawing.Size(400, 21);
            this.LblSalesDetail.TabIndex = 241;
            this.LblSalesDetail.Text = "  --: Kapan Transfer :--";
            this.LblSalesDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblSalesDetail.ToolTips = "";
            // 
            // progressPanelTransaction
            // 
            this.progressPanelTransaction.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanelTransaction.Appearance.Options.UseBackColor = true;
            this.progressPanelTransaction.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanelTransaction.AppearanceCaption.Options.UseFont = true;
            this.progressPanelTransaction.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanelTransaction.AppearanceDescription.Options.UseFont = true;
            this.progressPanelTransaction.BarAnimationElementThickness = 2;
            this.progressPanelTransaction.Location = new System.Drawing.Point(203, 72);
            this.progressPanelTransaction.Name = "progressPanelTransaction";
            this.progressPanelTransaction.Size = new System.Drawing.Size(134, 45);
            this.progressPanelTransaction.TabIndex = 188;
            this.progressPanelTransaction.Text = "progressPanel1";
            this.progressPanelTransaction.Visible = false;
            // 
            // BtnTransferTransaction
            // 
            this.BtnTransferTransaction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTransferTransaction.ForeColor = System.Drawing.Color.Navy;
            this.BtnTransferTransaction.Location = new System.Drawing.Point(42, 58);
            this.BtnTransferTransaction.Name = "BtnTransferTransaction";
            this.BtnTransferTransaction.Size = new System.Drawing.Size(155, 75);
            this.BtnTransferTransaction.TabIndex = 3;
            this.BtnTransferTransaction.Text = "TRANSFER";
            this.BtnTransferTransaction.ToolTips = "";
            this.BtnTransferTransaction.UseVisualStyleBackColor = true;
            this.BtnTransferTransaction.Click += new System.EventHandler(this.BtnTransferTransaction_Click);
            // 
            // lblMessageTransaction
            // 
            this.lblMessageTransaction.AutoSize = true;
            this.lblMessageTransaction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageTransaction.ForeColor = System.Drawing.Color.Maroon;
            this.lblMessageTransaction.Location = new System.Drawing.Point(39, 217);
            this.lblMessageTransaction.Name = "lblMessageTransaction";
            this.lblMessageTransaction.Size = new System.Drawing.Size(63, 13);
            this.lblMessageTransaction.TabIndex = 190;
            this.lblMessageTransaction.Text = "Message";
            this.lblMessageTransaction.ToolTips = "";
            // 
            // txtPassForEnableDelete
            // 
            this.txtPassForEnableDelete.ActivationColor = false;
            this.txtPassForEnableDelete.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtPassForEnableDelete.AllowTabKeyOnEnter = false;
            this.txtPassForEnableDelete.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPassForEnableDelete.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassForEnableDelete.Format = "";
            this.txtPassForEnableDelete.IsComplusory = false;
            this.txtPassForEnableDelete.Location = new System.Drawing.Point(700, 118);
            this.txtPassForEnableDelete.Name = "txtPassForEnableDelete";
            this.txtPassForEnableDelete.PasswordChar = '*';
            this.txtPassForEnableDelete.SelectAllTextOnFocus = true;
            this.txtPassForEnableDelete.Size = new System.Drawing.Size(45, 13);
            this.txtPassForEnableDelete.TabIndex = 247;
            this.txtPassForEnableDelete.TabStop = false;
            this.txtPassForEnableDelete.Tag = "AXONE";
            this.txtPassForEnableDelete.ToolTips = "";
            this.txtPassForEnableDelete.WaterMarkText = null;
            // 
            // bgWorkerTransaction
            // 
            this.bgWorkerTransaction.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerTransaction_DoWork);
            this.bgWorkerTransaction.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerTransaction_RunWorkerCompleted);
            // 
            // bgWorkerTransactionDelete
            // 
            this.bgWorkerTransactionDelete.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerTransactionDelete_DoWork);
            this.bgWorkerTransactionDelete.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerTransactionDelete_RunWorkerCompleted);
            // 
            // FrmKapanTransfer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(406, 241);
            this.Controls.Add(this.panel1);
            this.Name = "FrmKapanTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KAPAN TRANSFER";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorkerMaster;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
        private AxonContLib.cLabel cLabel9;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private AxonContLib.cLabel LblSalesDetail;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanelTransaction;
        private AxonContLib.cButton BtnTransferTransaction;
        private System.ComponentModel.BackgroundWorker bgWorkerTransaction;
        private System.ComponentModel.BackgroundWorker bgWorkerPricing;
        private AxonContLib.cLabel lblMessageTransaction;
        private AxonContLib.cButton BtnDeleteTransaction;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanelTransactionDelete;
        private System.ComponentModel.BackgroundWorker bgWorkerTransactionDelete;
        private AxonContLib.cTextBox txtPassForEnableDelete;
    }
}