namespace AxoneMFGRJ.Rapaport
{
    partial class FrmDollarLabourPriceDetail
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
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new AxonContLib.cPanel();
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.txtMonth = new AxonContLib.cTextBox(this.components);
            this.txtYear = new AxonContLib.cTextBox(this.components);
            this.txtShape = new AxonContLib.cTextBox(this.components);
            this.panel1 = new AxonContLib.cPanel();
            this.BtnQuickUpload = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.PivotGrdDet = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField6 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField4 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField7 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField5 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.panel3 = new AxonContLib.cPanel();
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.cLabel6 = new AxonContLib.cLabel(this.components);
            this.txtCopyToMonth = new AxonContLib.cTextBox(this.components);
            this.txtCopyToYear = new AxonContLib.cTextBox(this.components);
            this.BtnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PivotGrdDet)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.Location = new System.Drawing.Point(632, 5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(103, 32);
            this.BtnExit.TabIndex = 7;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.cLabel3);
            this.panel2.Controls.Add(this.cLabel1);
            this.panel2.Controls.Add(this.cLabel2);
            this.panel2.Controls.Add(this.txtMonth);
            this.panel2.Controls.Add(this.txtYear);
            this.panel2.Controls.Add(this.txtShape);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.BtnExit);
            this.panel2.Controls.Add(this.BtnExport);
            this.panel2.Controls.Add(this.BtnRefresh);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1264, 43);
            this.panel2.TabIndex = 0;
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(247, 14);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(47, 14);
            this.cLabel3.TabIndex = 4;
            this.cLabel3.Text = "Shape";
            this.cLabel3.ToolTips = "";
            this.cLabel3.Visible = false;
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(143, 14);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(46, 14);
            this.cLabel1.TabIndex = 2;
            this.cLabel1.Text = "Month";
            this.cLabel1.ToolTips = "";
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(7, 14);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(34, 14);
            this.cLabel2.TabIndex = 0;
            this.cLabel2.Text = "Year";
            this.cLabel2.ToolTips = "";
            // 
            // txtMonth
            // 
            this.txtMonth.ActivationColor = true;
            this.txtMonth.AllowTabKeyOnEnter = false;
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonth.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtMonth.Format = "######";
            this.txtMonth.IsComplusory = false;
            this.txtMonth.Location = new System.Drawing.Point(195, 10);
            this.txtMonth.MaxLength = 4;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.SelectAllTextOnFocus = true;
            this.txtMonth.Size = new System.Drawing.Size(46, 23);
            this.txtMonth.TabIndex = 3;
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
            this.txtYear.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtYear.Format = "######";
            this.txtYear.IsComplusory = false;
            this.txtYear.Location = new System.Drawing.Point(48, 10);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.SelectAllTextOnFocus = true;
            this.txtYear.Size = new System.Drawing.Size(86, 23);
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.ToolTips = "";
            this.txtYear.WaterMarkText = null;
            // 
            // txtShape
            // 
            this.txtShape.ActivationColor = true;
            this.txtShape.AllowTabKeyOnEnter = false;
            this.txtShape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShape.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShape.Format = "";
            this.txtShape.IsComplusory = false;
            this.txtShape.Location = new System.Drawing.Point(300, 10);
            this.txtShape.Name = "txtShape";
            this.txtShape.SelectAllTextOnFocus = true;
            this.txtShape.Size = new System.Drawing.Size(109, 23);
            this.txtShape.TabIndex = 5;
            this.txtShape.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShape.ToolTips = "";
            this.txtShape.Visible = false;
            this.txtShape.WaterMarkText = null;
            this.txtShape.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtShape_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnQuickUpload);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1144, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 43);
            this.panel1.TabIndex = 36;
            // 
            // BtnQuickUpload
            // 
            this.BtnQuickUpload.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnQuickUpload.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnQuickUpload.Appearance.Options.UseFont = true;
            this.BtnQuickUpload.Appearance.Options.UseForeColor = true;
            this.BtnQuickUpload.Location = new System.Drawing.Point(9, 5);
            this.BtnQuickUpload.Name = "BtnQuickUpload";
            this.BtnQuickUpload.Size = new System.Drawing.Size(103, 32);
            this.BtnQuickUpload.TabIndex = 35;
            this.BtnQuickUpload.TabStop = false;
            this.BtnQuickUpload.Text = "Quick Upload";
            this.BtnQuickUpload.Click += new System.EventHandler(this.BtnQuickUpload_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.Location = new System.Drawing.Point(523, 5);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(103, 32);
            this.BtnExport.TabIndex = 6;
            this.BtnExport.TabStop = false;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnRefresh.Appearance.Options.UseFont = true;
            this.BtnRefresh.Appearance.Options.UseForeColor = true;
            this.BtnRefresh.Location = new System.Drawing.Point(415, 5);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(103, 32);
            this.BtnRefresh.TabIndex = 6;
            this.BtnRefresh.Text = "&Refresh List";
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // PivotGrdDet
            // 
            this.PivotGrdDet.Appearance.FieldValue.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.PivotGrdDet.Appearance.FieldValue.Options.UseFont = true;
            this.PivotGrdDet.Appearance.FilterSeparator.BackColor = System.Drawing.Color.Gray;
            this.PivotGrdDet.Appearance.FilterSeparator.Options.UseBackColor = true;
            this.PivotGrdDet.Appearance.Lines.BackColor = System.Drawing.Color.DimGray;
            this.PivotGrdDet.Appearance.Lines.Options.UseBackColor = true;
            this.PivotGrdDet.Appearance.SelectedCell.ForeColor = System.Drawing.Color.Black;
            this.PivotGrdDet.Appearance.SelectedCell.Options.UseForeColor = true;
            this.PivotGrdDet.AppearancePrint.Cell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.PivotGrdDet.AppearancePrint.Cell.Options.UseFont = true;
            this.PivotGrdDet.AppearancePrint.FieldHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.PivotGrdDet.AppearancePrint.FieldHeader.Options.UseFont = true;
            this.PivotGrdDet.AppearancePrint.FieldValue.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.PivotGrdDet.AppearancePrint.FieldValue.Options.UseFont = true;
            this.PivotGrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.Gray;
            this.PivotGrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
            this.PivotGrdDet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PivotGrdDet.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pivotGridField1,
            this.pivotGridField3,
            this.pivotGridField4,
            this.pivotGridField5,
            this.pivotGridField6,
            this.pivotGridField7});
            this.PivotGrdDet.Location = new System.Drawing.Point(0, 43);
            this.PivotGrdDet.Name = "PivotGrdDet";
            this.PivotGrdDet.OptionsPrint.UsePrintAppearance = true;
            this.PivotGrdDet.OptionsSelection.CellSelection = false;
            this.PivotGrdDet.OptionsSelection.EnableAppearanceFocusedCell = true;
            this.PivotGrdDet.OptionsSelection.MultiSelect = false;
            this.PivotGrdDet.OptionsView.ShowColumnGrandTotalHeader = false;
            this.PivotGrdDet.OptionsView.ShowColumnGrandTotals = false;
            this.PivotGrdDet.OptionsView.ShowColumnTotals = false;
            this.PivotGrdDet.OptionsView.ShowDataHeaders = false;
            this.PivotGrdDet.OptionsView.ShowFilterHeaders = false;
            this.PivotGrdDet.OptionsView.ShowRowGrandTotalHeader = false;
            this.PivotGrdDet.OptionsView.ShowRowGrandTotals = false;
            this.PivotGrdDet.OptionsView.ShowRowTotals = false;
            this.PivotGrdDet.Size = new System.Drawing.Size(1264, 576);
            this.PivotGrdDet.TabIndex = 33;
            this.PivotGrdDet.CustomDrawFieldValue += new DevExpress.XtraPivotGrid.PivotCustomDrawFieldValueEventHandler(this.PivotGrdDet_CustomDrawFieldValue);
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.pivotGridField1.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField1.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.pivotGridField1.Appearance.Header.Options.UseFont = true;
            this.pivotGridField1.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.pivotGridField1.Appearance.Value.Options.UseFont = true;
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField1.AreaIndex = 0;
            this.pivotGridField1.Caption = "Size";
            this.pivotGridField1.FieldName = "SIZE";
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.pivotGridField3.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField3.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.pivotGridField3.Appearance.Header.Options.UseFont = true;
            this.pivotGridField3.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.pivotGridField3.Appearance.Value.Options.UseFont = true;
            this.pivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField3.AreaIndex = 1;
            this.pivotGridField3.Caption = "Col";
            this.pivotGridField3.FieldName = "COLORNAME";
            this.pivotGridField3.Name = "pivotGridField3";
            this.pivotGridField3.SortBySummaryInfo.Field = this.pivotGridField6;
            this.pivotGridField3.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Value;
            // 
            // pivotGridField6
            // 
            this.pivotGridField6.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.pivotGridField6.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField6.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.pivotGridField6.Appearance.Header.Options.UseFont = true;
            this.pivotGridField6.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F);
            this.pivotGridField6.Appearance.Value.Options.UseFont = true;
            this.pivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField6.AreaIndex = 3;
            this.pivotGridField6.Caption = "COLSEQNO";
            this.pivotGridField6.FieldName = "COLSEQNO";
            this.pivotGridField6.Name = "pivotGridField6";
            this.pivotGridField6.Visible = false;
            // 
            // pivotGridField4
            // 
            this.pivotGridField4.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.pivotGridField4.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField4.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.pivotGridField4.Appearance.Header.Options.UseFont = true;
            this.pivotGridField4.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.pivotGridField4.Appearance.Value.Options.UseFont = true;
            this.pivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField4.AreaIndex = 0;
            this.pivotGridField4.Caption = "Cla";
            this.pivotGridField4.FieldName = "CLARITYNAME";
            this.pivotGridField4.Name = "pivotGridField4";
            this.pivotGridField4.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.pivotGridField4.SortBySummaryInfo.Field = this.pivotGridField7;
            this.pivotGridField4.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // pivotGridField7
            // 
            this.pivotGridField7.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.pivotGridField7.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField7.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.pivotGridField7.Appearance.Header.Options.UseFont = true;
            this.pivotGridField7.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F);
            this.pivotGridField7.Appearance.Value.Options.UseFont = true;
            this.pivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField7.AreaIndex = 1;
            this.pivotGridField7.Caption = "CLASEQNO";
            this.pivotGridField7.FieldName = "CLASEQNO";
            this.pivotGridField7.Name = "pivotGridField7";
            this.pivotGridField7.Visible = false;
            // 
            // pivotGridField5
            // 
            this.pivotGridField5.Appearance.Cell.Font = new System.Drawing.Font("Verdana", 8F);
            this.pivotGridField5.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField5.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.pivotGridField5.Appearance.Header.Options.UseFont = true;
            this.pivotGridField5.Appearance.Value.Font = new System.Drawing.Font("Verdana", 8F);
            this.pivotGridField5.Appearance.Value.Options.UseFont = true;
            this.pivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField5.AreaIndex = 0;
            this.pivotGridField5.Caption = "Value";
            this.pivotGridField5.CellFormat.FormatString = "{0:N2}";
            this.pivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField5.FieldName = "NVALUE";
            this.pivotGridField5.Name = "pivotGridField5";
            this.pivotGridField5.Width = 60;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.cLabel5);
            this.panel3.Controls.Add(this.cLabel6);
            this.panel3.Controls.Add(this.txtCopyToMonth);
            this.panel3.Controls.Add(this.txtCopyToYear);
            this.panel3.Controls.Add(this.BtnCopy);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 619);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1264, 43);
            this.panel3.TabIndex = 34;
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(193, 13);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(100, 14);
            this.cLabel5.TabIndex = 2;
            this.cLabel5.Text = "Copy To Month";
            this.cLabel5.ToolTips = "";
            // 
            // cLabel6
            // 
            this.cLabel6.AutoSize = true;
            this.cLabel6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel6.ForeColor = System.Drawing.Color.Black;
            this.cLabel6.Location = new System.Drawing.Point(7, 13);
            this.cLabel6.Name = "cLabel6";
            this.cLabel6.Size = new System.Drawing.Size(88, 14);
            this.cLabel6.TabIndex = 0;
            this.cLabel6.Text = "Copy To Year";
            this.cLabel6.ToolTips = "";
            // 
            // txtCopyToMonth
            // 
            this.txtCopyToMonth.ActivationColor = true;
            this.txtCopyToMonth.AllowTabKeyOnEnter = false;
            this.txtCopyToMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCopyToMonth.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtCopyToMonth.Format = "######";
            this.txtCopyToMonth.IsComplusory = false;
            this.txtCopyToMonth.Location = new System.Drawing.Point(295, 9);
            this.txtCopyToMonth.MaxLength = 4;
            this.txtCopyToMonth.Name = "txtCopyToMonth";
            this.txtCopyToMonth.SelectAllTextOnFocus = true;
            this.txtCopyToMonth.Size = new System.Drawing.Size(46, 23);
            this.txtCopyToMonth.TabIndex = 3;
            this.txtCopyToMonth.Text = "0";
            this.txtCopyToMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCopyToMonth.ToolTips = "";
            this.txtCopyToMonth.WaterMarkText = null;
            // 
            // txtCopyToYear
            // 
            this.txtCopyToYear.ActivationColor = true;
            this.txtCopyToYear.AllowTabKeyOnEnter = false;
            this.txtCopyToYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCopyToYear.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtCopyToYear.Format = "######";
            this.txtCopyToYear.IsComplusory = false;
            this.txtCopyToYear.Location = new System.Drawing.Point(98, 9);
            this.txtCopyToYear.MaxLength = 4;
            this.txtCopyToYear.Name = "txtCopyToYear";
            this.txtCopyToYear.SelectAllTextOnFocus = true;
            this.txtCopyToYear.Size = new System.Drawing.Size(86, 23);
            this.txtCopyToYear.TabIndex = 1;
            this.txtCopyToYear.Text = "0";
            this.txtCopyToYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCopyToYear.ToolTips = "";
            this.txtCopyToYear.WaterMarkText = null;
            // 
            // BtnCopy
            // 
            this.BtnCopy.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnCopy.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCopy.Appearance.Options.UseFont = true;
            this.BtnCopy.Appearance.Options.UseForeColor = true;
            this.BtnCopy.Location = new System.Drawing.Point(357, 6);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(103, 32);
            this.BtnCopy.TabIndex = 6;
            this.BtnCopy.Text = "Copy To";
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // FrmDollarLabourPriceDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 662);
            this.Controls.Add(this.PivotGrdDet);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "FrmDollarLabourPriceDetail";
            this.Text = "DOLLAR LABOUR";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PivotGrdDet)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private AxonContLib.cPanel panel2;
        private AxonContLib.cPanel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnRefresh;
        private DevExpress.XtraEditors.SimpleButton BtnQuickUpload;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cLabel cLabel2;
        private AxonContLib.cTextBox txtMonth;
        private AxonContLib.cTextBox txtYear;
        private AxonContLib.cTextBox txtShape;
        private DevExpress.XtraPivotGrid.PivotGridControl PivotGrdDet;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField4;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField5;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField6;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField7;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private AxonContLib.cPanel panel3;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cLabel cLabel6;
        private AxonContLib.cTextBox txtCopyToMonth;
        private AxonContLib.cTextBox txtCopyToYear;
        private DevExpress.XtraEditors.SimpleButton BtnCopy;


    }
}