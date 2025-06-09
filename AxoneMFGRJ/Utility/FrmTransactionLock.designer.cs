namespace AxoneMFGRJ
{
    partial class FrmTransactionLock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTransactionLock));
            this.BtnTransactionUnlock = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTransactionLock = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cLabel18 = new AxonContLib.cLabel(this.components);
            this.lblTransactionLockStatus = new AxonContLib.cLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnTransactionUnlock
            // 
            this.BtnTransactionUnlock.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnTransactionUnlock.Appearance.Options.UseFont = true;
            this.BtnTransactionUnlock.Location = new System.Drawing.Point(258, 83);
            this.BtnTransactionUnlock.Name = "BtnTransactionUnlock";
            this.BtnTransactionUnlock.Size = new System.Drawing.Size(216, 201);
            this.BtnTransactionUnlock.TabIndex = 2;
            this.BtnTransactionUnlock.Text = "UnLock";
            this.BtnTransactionUnlock.Click += new System.EventHandler(this.BtnTransactionUnLock_Click);
            // 
            // BtnTransactionLock
            // 
            this.BtnTransactionLock.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnTransactionLock.Appearance.Options.UseFont = true;
            this.BtnTransactionLock.Location = new System.Drawing.Point(36, 83);
            this.BtnTransactionLock.Name = "BtnTransactionLock";
            this.BtnTransactionLock.Size = new System.Drawing.Size(216, 201);
            this.BtnTransactionLock.TabIndex = 1;
            this.BtnTransactionLock.Text = "Lock";
            this.BtnTransactionLock.Click += new System.EventHandler(this.BtnTransactionLock_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Cambria", 12F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.lblTransactionLockStatus);
            this.groupControl1.Controls.Add(this.cLabel18);
            this.groupControl1.Controls.Add(this.BtnTransactionLock);
            this.groupControl1.Controls.Add(this.BtnTransactionUnlock);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(629, 411);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Transaction Lock/Unlock";
            // 
            // cLabel18
            // 
            this.cLabel18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel18.ForeColor = System.Drawing.Color.Black;
            this.cLabel18.Location = new System.Drawing.Point(33, 47);
            this.cLabel18.Name = "cLabel18";
            this.cLabel18.Size = new System.Drawing.Size(235, 24);
            this.cLabel18.TabIndex = 3;
            this.cLabel18.Text = "Current Transaction Status Is : ";
            this.cLabel18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel18.ToolTips = "";
            // 
            // lblTransactionLockStatus
            // 
            this.lblTransactionLockStatus.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionLockStatus.ForeColor = System.Drawing.Color.Black;
            this.lblTransactionLockStatus.Location = new System.Drawing.Point(270, 47);
            this.lblTransactionLockStatus.Name = "lblTransactionLockStatus";
            this.lblTransactionLockStatus.Size = new System.Drawing.Size(99, 24);
            this.lblTransactionLockStatus.TabIndex = 4;
            this.lblTransactionLockStatus.Text = "Locked";
            this.lblTransactionLockStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTransactionLockStatus.ToolTips = "";
            // 
            // FrmTransactionLock
            // 
            this.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 411);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmTransactionLock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRANSACTION LOCK";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSearch_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnTransactionLock;
        private DevExpress.XtraEditors.SimpleButton BtnTransactionUnlock;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private AxonContLib.cLabel cLabel18;
        private AxonContLib.cLabel lblTransactionLockStatus;




    }
}