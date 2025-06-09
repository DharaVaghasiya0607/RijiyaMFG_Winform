namespace AxoneMFGRJ.Utility
{
    partial class FrmDataTransfer
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
            this.BtnTransferTransaction = new AxonContLib.cButton(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressPanelDelete = new DevExpress.XtraWaitForm.ProgressPanel();
            this.BtnRefresh = new AxonContLib.cButton(this.components);
            this.CmbKapan = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.BtnDeleteTransaction = new AxonContLib.cButton(this.components);
            this.progressPanelTransaction = new DevExpress.XtraWaitForm.ProgressPanel();
            this.lblMessageTransaction = new AxonContLib.cLabel(this.components);
            this.cLabel9 = new AxonContLib.cLabel(this.components);
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.LblSalesDetail = new AxonContLib.cLabel(this.components);
            this.bgWorkerTransaction = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerTransactionDelete = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnTransferTransaction
            // 
            this.BtnTransferTransaction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTransferTransaction.ForeColor = System.Drawing.Color.Navy;
            this.BtnTransferTransaction.Location = new System.Drawing.Point(76, 58);
            this.BtnTransferTransaction.Name = "BtnTransferTransaction";
            this.BtnTransferTransaction.Size = new System.Drawing.Size(155, 75);
            this.BtnTransferTransaction.TabIndex = 3;
            this.BtnTransferTransaction.Text = "TRANSFER";
            this.BtnTransferTransaction.ToolTips = "";
            this.BtnTransferTransaction.UseVisualStyleBackColor = true;
            this.BtnTransferTransaction.Click += new System.EventHandler(this.BtnTransferMaster_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressPanelDelete);
            this.panel1.Controls.Add(this.BtnRefresh);
            this.panel1.Controls.Add(this.CmbKapan);
            this.panel1.Controls.Add(this.BtnDeleteTransaction);
            this.panel1.Controls.Add(this.progressPanelTransaction);
            this.panel1.Controls.Add(this.BtnTransferTransaction);
            this.panel1.Controls.Add(this.lblMessageTransaction);
            this.panel1.Controls.Add(this.cLabel9);
            this.panel1.Controls.Add(this.panelControl3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 148);
            this.panel1.TabIndex = 190;
            // 
            // progressPanelDelete
            // 
            this.progressPanelDelete.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanelDelete.Appearance.Options.UseBackColor = true;
            this.progressPanelDelete.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanelDelete.AppearanceCaption.Options.UseFont = true;
            this.progressPanelDelete.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanelDelete.AppearanceDescription.Options.UseFont = true;
            this.progressPanelDelete.BarAnimationElementThickness = 2;
            this.progressPanelDelete.Location = new System.Drawing.Point(587, 86);
            this.progressPanelDelete.Name = "progressPanelDelete";
            this.progressPanelDelete.Size = new System.Drawing.Size(181, 45);
            this.progressPanelDelete.TabIndex = 248;
            this.progressPanelDelete.Text = "progressPanel1";
            this.progressPanelDelete.Visible = false;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRefresh.ForeColor = System.Drawing.Color.Navy;
            this.BtnRefresh.Location = new System.Drawing.Point(671, 29);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(132, 32);
            this.BtnRefresh.TabIndex = 247;
            this.BtnRefresh.Text = "REFRESH";
            this.BtnRefresh.ToolTips = "";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // CmbKapan
            // 
            this.CmbKapan.Location = new System.Drawing.Point(76, 31);
            this.CmbKapan.Name = "CmbKapan";
            this.CmbKapan.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.CmbKapan.Properties.Appearance.Options.UseFont = true;
            this.CmbKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 9F);
            this.CmbKapan.Properties.AppearanceDropDown.Options.UseFont = true;
            this.CmbKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbKapan.Properties.DropDownRows = 20;
            this.CmbKapan.Properties.IncrementalSearch = true;
            this.CmbKapan.Size = new System.Drawing.Size(589, 22);
            this.CmbKapan.TabIndex = 190;
            // 
            // BtnDeleteTransaction
            // 
            this.BtnDeleteTransaction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteTransaction.ForeColor = System.Drawing.Color.Navy;
            this.BtnDeleteTransaction.Location = new System.Drawing.Point(237, 58);
            this.BtnDeleteTransaction.Name = "BtnDeleteTransaction";
            this.BtnDeleteTransaction.Size = new System.Drawing.Size(155, 75);
            this.BtnDeleteTransaction.TabIndex = 191;
            this.BtnDeleteTransaction.Text = "DELETE";
            this.BtnDeleteTransaction.ToolTips = "";
            this.BtnDeleteTransaction.UseVisualStyleBackColor = true;
            this.BtnDeleteTransaction.Click += new System.EventHandler(this.BtnDeleteTransaction_Click);
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
            this.progressPanelTransaction.Location = new System.Drawing.Point(400, 86);
            this.progressPanelTransaction.Name = "progressPanelTransaction";
            this.progressPanelTransaction.Size = new System.Drawing.Size(181, 45);
            this.progressPanelTransaction.TabIndex = 188;
            this.progressPanelTransaction.Text = "progressPanel1";
            this.progressPanelTransaction.Visible = false;
            // 
            // lblMessageTransaction
            // 
            this.lblMessageTransaction.AutoSize = true;
            this.lblMessageTransaction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageTransaction.ForeColor = System.Drawing.Color.Maroon;
            this.lblMessageTransaction.Location = new System.Drawing.Point(398, 70);
            this.lblMessageTransaction.Name = "lblMessageTransaction";
            this.lblMessageTransaction.Size = new System.Drawing.Size(63, 13);
            this.lblMessageTransaction.TabIndex = 190;
            this.lblMessageTransaction.Text = "Message";
            this.lblMessageTransaction.ToolTips = "";
            // 
            // cLabel9
            // 
            this.cLabel9.AutoSize = true;
            this.cLabel9.Font = new System.Drawing.Font("Verdana", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.cLabel9.ForeColor = System.Drawing.Color.Black;
            this.cLabel9.Location = new System.Drawing.Point(10, 36);
            this.cLabel9.Name = "cLabel9";
            this.cLabel9.Size = new System.Drawing.Size(53, 14);
            this.cLabel9.TabIndex = 189;
            this.cLabel9.Text = "KAPAN";
            this.cLabel9.ToolTips = "";
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
            this.panelControl3.Size = new System.Drawing.Size(812, 27);
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
            this.LblSalesDetail.Size = new System.Drawing.Size(806, 21);
            this.LblSalesDetail.TabIndex = 241;
            this.LblSalesDetail.Text = "  --: Kapan Transfer :--";
            this.LblSalesDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblSalesDetail.ToolTips = "";
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
            // FrmDataTransfer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(812, 156);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmDataTransfer";
            this.Text = "KAPAN TRANSFER";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cButton BtnTransferTransaction;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CmbKapan;
        private AxonContLib.cLabel cLabel9;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private AxonContLib.cLabel LblSalesDetail;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanelTransaction;
        private System.ComponentModel.BackgroundWorker bgWorkerTransaction;
        private AxonContLib.cLabel lblMessageTransaction;
        private AxonContLib.cButton BtnDeleteTransaction;
        private System.ComponentModel.BackgroundWorker bgWorkerTransactionDelete;
        private AxonContLib.cButton BtnRefresh;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanelDelete;
    }
}