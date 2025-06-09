namespace AxoneMFGRJ.ReportGrid
{
    partial class FrmGIAAdvanceField
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
            this.PanelTop = new AxonContLib.cPanel();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.WebBrowserCertificate = new System.Windows.Forms.WebBrowser();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.WebBrowserVideo = new System.Windows.Forms.WebBrowser();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdBox = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.RepName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.RepCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.lblFormType = new AxonContLib.cLabel(this.components);
            this.PanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCode)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelTop
            // 
            this.PanelTop.AutoScroll = true;
            this.PanelTop.AutoScrollMinSize = new System.Drawing.Size(800, 1000);
            this.PanelTop.BackColor = System.Drawing.Color.White;
            this.PanelTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelTop.Controls.Add(this.groupControl3);
            this.PanelTop.Controls.Add(this.groupControl1);
            this.PanelTop.Controls.Add(this.groupControl2);
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelTop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PanelTop.Location = new System.Drawing.Point(0, 31);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(1008, 588);
            this.PanelTop.TabIndex = 190;
            this.PanelTop.TabStop = true;
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl3.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl3.Controls.Add(this.WebBrowserCertificate);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(340, 550);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(647, 450);
            this.groupControl3.TabIndex = 193;
            this.groupControl3.Text = "Certificate";
            // 
            // WebBrowserCertificate
            // 
            this.WebBrowserCertificate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowserCertificate.Location = new System.Drawing.Point(2, 27);
            this.WebBrowserCertificate.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowserCertificate.Name = "WebBrowserCertificate";
            this.WebBrowserCertificate.Size = new System.Drawing.Size(643, 421);
            this.WebBrowserCertificate.TabIndex = 3;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.WebBrowserVideo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(340, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(647, 550);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Video";
            // 
            // WebBrowserVideo
            // 
            this.WebBrowserVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowserVideo.Location = new System.Drawing.Point(2, 27);
            this.WebBrowserVideo.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowserVideo.Name = "WebBrowserVideo";
            this.WebBrowserVideo.Size = new System.Drawing.Size(643, 521);
            this.WebBrowserVideo.TabIndex = 3;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.Controls.Add(this.MainGrid);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(340, 1000);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Stone Detail";
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(2, 27);
            this.MainGrid.MainView = this.GrdBox;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepRemark,
            this.RepName,
            this.RepCode});
            this.MainGrid.Size = new System.Drawing.Size(336, 971);
            this.MainGrid.TabIndex = 193;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdBox});
            // 
            // GrdBox
            // 
            this.GrdBox.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdBox.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdBox.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdBox.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdBox.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdBox.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdBox.Appearance.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdBox.Appearance.Row.Options.UseFont = true;
            this.GrdBox.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdBox.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdBox.ColumnPanelRowHeight = 25;
            this.GrdBox.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.GrdBox.GridControl = this.MainGrid;
            this.GrdBox.Name = "GrdBox";
            this.GrdBox.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.GrdBox.OptionsSelection.MultiSelect = true;
            this.GrdBox.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdBox.OptionsView.ShowGroupPanel = false;
            this.GrdBox.RowHeight = 25;
            this.GrdBox.RowSeparatorHeight = 2;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Parameter";
            this.gridColumn1.FieldName = "PARAMETER";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 122;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Value";
            this.gridColumn2.FieldName = "VALUE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 184;
            // 
            // RepRemark
            // 
            this.RepRemark.AutoHeight = false;
            this.RepRemark.Name = "RepRemark";
            // 
            // RepName
            // 
            this.RepName.AutoHeight = false;
            this.RepName.Name = "RepName";
            // 
            // RepCode
            // 
            this.RepCode.AutoHeight = false;
            this.RepCode.Name = "RepCode";
            // 
            // lblFormType
            // 
            this.lblFormType.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblFormType.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormType.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormType.ForeColor = System.Drawing.Color.White;
            this.lblFormType.Location = new System.Drawing.Point(0, 0);
            this.lblFormType.Name = "lblFormType";
            this.lblFormType.Size = new System.Drawing.Size(1008, 31);
            this.lblFormType.TabIndex = 192;
            this.lblFormType.Text = "-- : GIA ADVANCE INFORMATION : --";
            this.lblFormType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFormType.ToolTips = "";
            // 
            // FrmGIAAdvanceField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1008, 619);
            this.Controls.Add(this.PanelTop);
            this.Controls.Add(this.lblFormType);
            this.Name = "FrmGIAAdvanceField";
            this.Tag = "";
            this.Text = "GIA Master";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGIAAdvanceField_KeyDown);
            this.PanelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel PanelTop;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdBox;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RepRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RepName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RepCode;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.WebBrowser WebBrowserVideo;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.WebBrowser WebBrowserCertificate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private AxonContLib.cLabel lblFormType;
    }
}