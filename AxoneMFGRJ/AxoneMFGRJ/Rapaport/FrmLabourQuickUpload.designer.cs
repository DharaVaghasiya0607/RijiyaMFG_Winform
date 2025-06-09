namespace AxoneMFGRJ.Rapaport
{
    partial class FrmLabourQuickUpload
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
            this.GrpPivot = new DevExpress.XtraEditors.GroupControl();
            this.MainGridDDet = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPaste = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new AxonContLib.cPanel();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.txtMonth = new AxonContLib.cTextBox(this.components);
            this.txtYear = new AxonContLib.cTextBox(this.components);
            this.lblDownloadSample = new AxonContLib.cLabel(this.components);
            this.txtShape = new AxonContLib.cTextBox(this.components);
            this.panel1 = new AxonContLib.cPanel();
            this.BtnTruncateAll = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.GrpPivot)).BeginInit();
            this.GrpPivot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridDDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpPivot
            // 
            this.GrpPivot.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrpPivot.AppearanceCaption.Options.UseFont = true;
            this.GrpPivot.AppearanceCaption.Options.UseTextOptions = true;
            this.GrpPivot.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrpPivot.Controls.Add(this.MainGridDDet);
            this.GrpPivot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpPivot.Location = new System.Drawing.Point(0, 76);
            this.GrpPivot.Name = "GrpPivot";
            this.GrpPivot.Size = new System.Drawing.Size(1008, 486);
            this.GrpPivot.TabIndex = 50;
            this.GrpPivot.Text = "Color Clarity Wise Matrix";
            // 
            // MainGridDDet
            // 
            this.MainGridDDet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGridDDet.Location = new System.Drawing.Point(2, 21);
            this.MainGridDDet.MainView = this.GrdDet;
            this.MainGridDDet.Name = "MainGridDDet";
            this.MainGridDDet.Size = new System.Drawing.Size(1004, 463);
            this.MainGridDDet.TabIndex = 0;
            this.MainGridDDet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet,
            this.gridView1});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.GridControl = this.MainGridDDet;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsCustomization.AllowFilter = false;
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.MainGridDDet;
            this.gridView1.Name = "gridView1";
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Appearance.Options.UseForeColor = true;
            this.BtnEdit.Location = new System.Drawing.Point(523, 20);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(103, 32);
            this.BtnEdit.TabIndex = 0;
            this.BtnEdit.TabStop = false;
            this.BtnEdit.Text = "&Save";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnPaste
            // 
            this.BtnPaste.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnPaste.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnPaste.Appearance.Options.UseFont = true;
            this.BtnPaste.Appearance.Options.UseForeColor = true;
            this.BtnPaste.Location = new System.Drawing.Point(412, 20);
            this.BtnPaste.Name = "BtnPaste";
            this.BtnPaste.Size = new System.Drawing.Size(105, 32);
            this.BtnPaste.TabIndex = 35;
            this.BtnPaste.TabStop = false;
            this.BtnPaste.Text = "Paste";
            this.BtnPaste.Click += new System.EventHandler(this.BtnPaste_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.cLabel3);
            this.panel2.Controls.Add(this.cLabel1);
            this.panel2.Controls.Add(this.cLabel2);
            this.panel2.Controls.Add(this.txtMonth);
            this.panel2.Controls.Add(this.txtYear);
            this.panel2.Controls.Add(this.lblDownloadSample);
            this.panel2.Controls.Add(this.txtShape);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.BtnClear);
            this.panel2.Controls.Add(this.BtnPaste);
            this.panel2.Controls.Add(this.BtnEdit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 76);
            this.panel2.TabIndex = 32;
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(153, 6);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(47, 14);
            this.cLabel3.TabIndex = 141;
            this.cLabel3.Text = "Shape";
            this.cLabel3.ToolTips = "";
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(104, 6);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(46, 14);
            this.cLabel1.TabIndex = 141;
            this.cLabel1.Text = "Month";
            this.cLabel1.ToolTips = "";
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(13, 6);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(35, 14);
            this.cLabel2.TabIndex = 141;
            this.cLabel2.Text = "Year";
            this.cLabel2.ToolTips = "";
            // 
            // txtMonth
            // 
            this.txtMonth.ActivationColor = true;
            this.txtMonth.AllowTabKeyOnEnter = false;
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonth.ComplusoryMsg = null;
            this.txtMonth.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtMonth.Format = "######";
            this.txtMonth.IsComplusory = false;
            this.txtMonth.Location = new System.Drawing.Point(104, 29);
            this.txtMonth.MaxLength = 4;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.RequiredChars = "0123456789.";
            this.txtMonth.SelectAllTextOnFocus = true;
            this.txtMonth.ShowToolTipOnFocus = false;
            this.txtMonth.Size = new System.Drawing.Size(46, 23);
            this.txtMonth.TabIndex = 140;
            this.txtMonth.Text = "0";
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMonth.ToolTips = "";
            this.txtMonth.WaterMarkText = null;
            // 
            // txtYear
            // 
            this.txtYear.ActivationColor = true;
            this.txtYear.AllowTabKeyOnEnter = false;
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.ComplusoryMsg = null;
            this.txtYear.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtYear.Format = "######";
            this.txtYear.IsComplusory = false;
            this.txtYear.Location = new System.Drawing.Point(13, 29);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.RequiredChars = "0123456789.";
            this.txtYear.SelectAllTextOnFocus = true;
            this.txtYear.ShowToolTipOnFocus = false;
            this.txtYear.Size = new System.Drawing.Size(86, 23);
            this.txtYear.TabIndex = 140;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.ToolTips = "";
            this.txtYear.WaterMarkText = null;
            // 
            // lblDownloadSample
            // 
            this.lblDownloadSample.AutoSize = true;
            this.lblDownloadSample.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownloadSample.ForeColor = System.Drawing.Color.Navy;
            this.lblDownloadSample.Location = new System.Drawing.Point(173, 55);
            this.lblDownloadSample.Name = "lblDownloadSample";
            this.lblDownloadSample.Size = new System.Drawing.Size(150, 13);
            this.lblDownloadSample.TabIndex = 39;
            this.lblDownloadSample.Text = "Download Sample File";
            this.lblDownloadSample.ToolTips = "";
            this.lblDownloadSample.Click += new System.EventHandler(this.lblDownloadSample_Click);
            // 
            // txtShape
            // 
            this.txtShape.ActivationColor = true;
            this.txtShape.AllowTabKeyOnEnter = false;
            this.txtShape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShape.ComplusoryMsg = null;
            this.txtShape.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShape.Format = "";
            this.txtShape.IsComplusory = false;
            this.txtShape.Location = new System.Drawing.Point(156, 29);
            this.txtShape.Name = "txtShape";
            this.txtShape.RequiredChars = "";
            this.txtShape.SelectAllTextOnFocus = true;
            this.txtShape.ShowToolTipOnFocus = false;
            this.txtShape.Size = new System.Drawing.Size(167, 23);
            this.txtShape.TabIndex = 38;
            this.txtShape.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShape.ToolTips = "";
            this.txtShape.WaterMarkText = null;
            this.txtShape.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtShape_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnTruncateAll);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(743, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 76);
            this.panel1.TabIndex = 36;
            // 
            // BtnTruncateAll
            // 
            this.BtnTruncateAll.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnTruncateAll.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnTruncateAll.Appearance.Options.UseFont = true;
            this.BtnTruncateAll.Appearance.Options.UseForeColor = true;
            this.BtnTruncateAll.Location = new System.Drawing.Point(3, 20);
            this.BtnTruncateAll.Name = "BtnTruncateAll";
            this.BtnTruncateAll.Size = new System.Drawing.Size(136, 32);
            this.BtnTruncateAll.TabIndex = 0;
            this.BtnTruncateAll.TabStop = false;
            this.BtnTruncateAll.Text = "Delete Labour";
            this.BtnTruncateAll.Click += new System.EventHandler(this.BtnTruncateAll_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Location = new System.Drawing.Point(145, 20);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(105, 32);
            this.BtnExit.TabIndex = 35;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "Exit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnClear.Appearance.Options.UseFont = true;
            this.BtnClear.Appearance.Options.UseForeColor = true;
            this.BtnClear.Location = new System.Drawing.Point(632, 20);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(105, 32);
            this.BtnClear.TabIndex = 35;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "Clear";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // FrmLabourQuickUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.GrpPivot);
            this.Controls.Add(this.panel2);
            this.Name = "FrmLabourQuickUpload";
            this.Text = "QUICK UPLOAD";
            ((System.ComponentModel.ISupportInitialize)(this.GrpPivot)).EndInit();
            this.GrpPivot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridDDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl GrpPivot;
        private DevExpress.XtraGrid.GridControl MainGridDDet;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnPaste;
        private AxonContLib.cPanel panel2;
        private DevExpress.XtraEditors.SimpleButton BtnClear;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnTruncateAll;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cTextBox txtShape;
        private AxonContLib.cLabel lblDownloadSample;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox txtMonth;
        private AxonContLib.cTextBox txtYear;


    }
}