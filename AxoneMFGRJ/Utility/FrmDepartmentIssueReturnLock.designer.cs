namespace AxoneMFGRJ
{
    partial class FrmDepartmentIssueReturnLock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDepartmentIssueReturnLock));
            this.BtnDeptIssueReturnUnLock = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDeptIssueReturnLock = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblDeptTransactionLockStatus = new AxonContLib.cLabel(this.components);
            this.cLabel18 = new AxonContLib.cLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnDeptIssueReturnUnLock
            // 
            this.BtnDeptIssueReturnUnLock.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnDeptIssueReturnUnLock.Appearance.Options.UseFont = true;
            this.BtnDeptIssueReturnUnLock.Location = new System.Drawing.Point(258, 83);
            this.BtnDeptIssueReturnUnLock.Name = "BtnDeptIssueReturnUnLock";
            this.BtnDeptIssueReturnUnLock.Size = new System.Drawing.Size(216, 201);
            this.BtnDeptIssueReturnUnLock.TabIndex = 2;
            this.BtnDeptIssueReturnUnLock.Text = "UnLock";
            this.BtnDeptIssueReturnUnLock.Click += new System.EventHandler(this.BtnDeptIssueReturnUnLock_Click);
            // 
            // BtnDeptIssueReturnLock
            // 
            this.BtnDeptIssueReturnLock.Appearance.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold);
            this.BtnDeptIssueReturnLock.Appearance.Options.UseFont = true;
            this.BtnDeptIssueReturnLock.Location = new System.Drawing.Point(36, 83);
            this.BtnDeptIssueReturnLock.Name = "BtnDeptIssueReturnLock";
            this.BtnDeptIssueReturnLock.Size = new System.Drawing.Size(216, 201);
            this.BtnDeptIssueReturnLock.TabIndex = 1;
            this.BtnDeptIssueReturnLock.Text = "Lock";
            this.BtnDeptIssueReturnLock.Click += new System.EventHandler(this.BtnDeptIssueReturnLock_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Cambria", 12F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.lblDeptTransactionLockStatus);
            this.groupControl1.Controls.Add(this.cLabel18);
            this.groupControl1.Controls.Add(this.BtnDeptIssueReturnLock);
            this.groupControl1.Controls.Add(this.BtnDeptIssueReturnUnLock);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(629, 411);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Department Issue Return Lock/Unlock";
            // 
            // lblDeptTransactionLockStatus
            // 
            this.lblDeptTransactionLockStatus.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeptTransactionLockStatus.ForeColor = System.Drawing.Color.Black;
            this.lblDeptTransactionLockStatus.Location = new System.Drawing.Point(371, 47);
            this.lblDeptTransactionLockStatus.Name = "lblDeptTransactionLockStatus";
            this.lblDeptTransactionLockStatus.Size = new System.Drawing.Size(99, 24);
            this.lblDeptTransactionLockStatus.TabIndex = 4;
            this.lblDeptTransactionLockStatus.Text = "Locked";
            this.lblDeptTransactionLockStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDeptTransactionLockStatus.ToolTips = "";
            // 
            // cLabel18
            // 
            this.cLabel18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel18.ForeColor = System.Drawing.Color.Black;
            this.cLabel18.Location = new System.Drawing.Point(33, 47);
            this.cLabel18.Name = "cLabel18";
            this.cLabel18.Size = new System.Drawing.Size(336, 24);
            this.cLabel18.TabIndex = 3;
            this.cLabel18.Text = "Current Department Lock Status Is : ";
            this.cLabel18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel18.ToolTips = "";
            // 
            // FrmDepartmentIssueReturnLock
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
            this.Name = "FrmDepartmentIssueReturnLock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DEPARTMENT ISSUE RETURN LOCK";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSearch_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnDeptIssueReturnLock;
        private DevExpress.XtraEditors.SimpleButton BtnDeptIssueReturnUnLock;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private AxonContLib.cLabel cLabel18;
        private AxonContLib.cLabel lblDeptTransactionLockStatus;




    }
}