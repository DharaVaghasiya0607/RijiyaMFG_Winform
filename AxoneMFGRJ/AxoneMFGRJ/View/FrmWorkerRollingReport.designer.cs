namespace AxoneMFGRJ.View
{
    partial class FrmWorkerRollingReport
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.BtnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new AxonContLib.cPanel();
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.DTPFromDate = new AxonContLib.cDateTimePicker(this.components);
            this.RdbAfterMak = new AxonContLib.cRadioButton(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.RdbBeforeMak = new AxonContLib.cRadioButton(this.components);
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.txtEmployee = new AxonContLib.cTextBox(this.components);
            this.BtnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBestFit = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new AxonContLib.cPanel();
            this.MainGrd = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.Appearance.Options.UseFont = true;
            this.BtnRefresh.Appearance.Options.UseTextOptions = true;
            this.BtnRefresh.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefresh.Location = new System.Drawing.Point(566, 25);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(102, 34);
            this.BtnRefresh.TabIndex = 5;
            this.BtnRefresh.Text = "Refresh (F5)";
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnPrint);
            this.panel1.Controls.Add(this.DTPFromDate);
            this.panel1.Controls.Add(this.RdbAfterMak);
            this.panel1.Controls.Add(this.cLabel5);
            this.panel1.Controls.Add(this.RdbBeforeMak);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.cLabel1);
            this.panel1.Controls.Add(this.txtEmployee);
            this.panel1.Controls.Add(this.BtnExcel);
            this.panel1.Controls.Add(this.BtnBestFit);
            this.panel1.Controls.Add(this.BtnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1047, 65);
            this.panel1.TabIndex = 0;
            // 
            // BtnPrint
            // 
            this.BtnPrint.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnPrint.Appearance.Options.UseFont = true;
            this.BtnPrint.Appearance.Options.UseTextOptions = true;
            this.BtnPrint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnPrint.Location = new System.Drawing.Point(744, 25);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(71, 34);
            this.BtnPrint.TabIndex = 16;
            this.BtnPrint.TabStop = false;
            this.BtnPrint.Text = "Print";
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.AllowTabKeyOnEnter = false;
            this.DTPFromDate.Checked = false;
            this.DTPFromDate.CustomFormat = "dd/MM/yyyy";
            this.DTPFromDate.Enabled = false;
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPFromDate.ForeColor = System.Drawing.Color.Black;
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(94, 6);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(136, 24);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ToolTips = "";
            this.DTPFromDate.Value = new System.DateTime(2019, 4, 9, 0, 0, 0, 0);
            // 
            // RdbAfterMak
            // 
            this.RdbAfterMak.AutoSize = true;
            this.RdbAfterMak.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RdbAfterMak.ForeColor = System.Drawing.Color.Black;
            this.RdbAfterMak.Location = new System.Drawing.Point(383, 9);
            this.RdbAfterMak.Name = "RdbAfterMak";
            this.RdbAfterMak.Size = new System.Drawing.Size(117, 18);
            this.RdbAfterMak.TabIndex = 1;
            this.RdbAfterMak.Text = "After Makable";
            this.RdbAfterMak.ToolTips = "";
            this.RdbAfterMak.UseVisualStyleBackColor = true;
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(19, 14);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(37, 13);
            this.cLabel5.TabIndex = 6;
            this.cLabel5.Text = "Date";
            this.cLabel5.ToolTips = "";
            // 
            // RdbBeforeMak
            // 
            this.RdbBeforeMak.AutoSize = true;
            this.RdbBeforeMak.Checked = true;
            this.RdbBeforeMak.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RdbBeforeMak.ForeColor = System.Drawing.Color.Black;
            this.RdbBeforeMak.Location = new System.Drawing.Point(249, 9);
            this.RdbBeforeMak.Name = "RdbBeforeMak";
            this.RdbBeforeMak.Size = new System.Drawing.Size(128, 18);
            this.RdbBeforeMak.TabIndex = 1;
            this.RdbBeforeMak.TabStop = true;
            this.RdbBeforeMak.Text = "Before Makable";
            this.RdbBeforeMak.ToolTips = "";
            this.RdbBeforeMak.UseVisualStyleBackColor = true;
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseTextOptions = true;
            this.BtnExit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Location = new System.Drawing.Point(891, 25);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(56, 34);
            this.BtnExit.TabIndex = 15;
            this.BtnExit.TabStop = false;
            this.BtnExit.Text = "Exit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(19, 40);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(71, 13);
            this.cLabel1.TabIndex = 4;
            this.cLabel1.Text = "Employee";
            this.cLabel1.ToolTips = "";
            // 
            // txtEmployee
            // 
            this.txtEmployee.ActivationColor = true;
            this.txtEmployee.AllowTabKeyOnEnter = false;
            this.txtEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployee.ComplusoryMsg = null;
            this.txtEmployee.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployee.Format = "";
            this.txtEmployee.IsComplusory = false;
            this.txtEmployee.Location = new System.Drawing.Point(94, 36);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.RequiredChars = "";
            this.txtEmployee.SelectAllTextOnFocus = true;
            this.txtEmployee.ShowToolTipOnFocus = false;
            this.txtEmployee.Size = new System.Drawing.Size(406, 23);
            this.txtEmployee.TabIndex = 3;
            this.txtEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmployee.ToolTips = "";
            this.txtEmployee.WaterMarkText = null;
            this.txtEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmployee_KeyPress);
            // 
            // BtnExcel
            // 
            this.BtnExcel.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExcel.Appearance.Options.UseFont = true;
            this.BtnExcel.Appearance.Options.UseTextOptions = true;
            this.BtnExcel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExcel.Location = new System.Drawing.Point(670, 25);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(71, 34);
            this.BtnExcel.TabIndex = 12;
            this.BtnExcel.TabStop = false;
            this.BtnExcel.Text = "Excel";
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // BtnBestFit
            // 
            this.BtnBestFit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBestFit.Appearance.Options.UseFont = true;
            this.BtnBestFit.Appearance.Options.UseTextOptions = true;
            this.BtnBestFit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.BtnBestFit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBestFit.Location = new System.Drawing.Point(818, 25);
            this.BtnBestFit.Name = "BtnBestFit";
            this.BtnBestFit.Size = new System.Drawing.Size(71, 34);
            this.BtnBestFit.TabIndex = 14;
            this.BtnBestFit.TabStop = false;
            this.BtnBestFit.Text = "Best Fit";
            this.BtnBestFit.Click += new System.EventHandler(this.BtnBestFit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.MainGrd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1047, 595);
            this.panel2.TabIndex = 180;
            // 
            // MainGrd
            // 
            this.MainGrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrd.Location = new System.Drawing.Point(0, 0);
            this.MainGrd.MainView = this.GrdDet;
            this.MainGrd.Name = "MainGrd";
            this.MainGrd.Size = new System.Drawing.Size(1047, 595);
            this.MainGrd.TabIndex = 178;
            this.MainGrd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FixedLine.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDet.Appearance.FixedLine.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FocusedCell.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.BackColor2 = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDet.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.BackColor2 = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.DarkGray;
            this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent;
            this.GrdDet.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 7F);
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Options.UseTextOptions = true;
            this.GrdDet.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.ColumnPanelRowHeight = 25;
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            styleFormatCondition1.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Black;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.Appearance.Options.UseFont = true;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "[SEL]=1";
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            styleFormatCondition2.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F);
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.Appearance.Options.UseFont = true;
            styleFormatCondition2.ApplyToRow = true;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition2.Expression = "([CONF_DATE]=\'\' OR [CONF_DATE] IS NULL)\r\nAND [SEL] = \'True\'";
            this.GrdDet.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.GrdDet.GridControl = this.MainGrd;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.Editable = false;
            this.GrdDet.OptionsCustomization.AllowQuickHideColumns = false;
            this.GrdDet.OptionsCustomization.AllowRowSizing = true;
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsFilter.AllowMRUFilterList = false;
            this.GrdDet.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = false;
            this.GrdDet.OptionsMenu.EnableColumnMenu = false;
            this.GrdDet.OptionsMenu.EnableFooterMenu = false;
            this.GrdDet.OptionsPrint.EnableAppearanceEvenRow = true;
            this.GrdDet.OptionsPrint.EnableAppearanceOddRow = true;
            this.GrdDet.OptionsSelection.MultiSelect = true;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 20;
            this.GrdDet.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.GrdDet.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDet_RowCellClick);
            this.GrdDet.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GrdDet_RowCellStyle);
            this.GrdDet.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GrdDet_CustomColumnDisplayText);
            // 
            // FrmWorkerRollingReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1047, 660);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmWorkerRollingReport";
            this.Text = "WORKER ROLLING REPORT";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMarkerRollingReport_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnRefresh;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cDateTimePicker DTPFromDate;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cTextBox txtEmployee;
        private DevExpress.XtraEditors.SimpleButton BtnBestFit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private AxonContLib.cRadioButton RdbBeforeMak;
        private AxonContLib.cRadioButton RdbAfterMak;
        private DevExpress.XtraEditors.SimpleButton BtnExcel;
        private DevExpress.XtraEditors.SimpleButton BtnPrint;
        private AxonContLib.cPanel panel2;
        private DevExpress.XtraGrid.GridControl MainGrd;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
    }
}