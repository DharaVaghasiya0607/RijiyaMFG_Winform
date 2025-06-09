namespace AxoneMFGRJ.Masters
{
    partial class FrmHolidayListMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHolidayListMaster));
            this.repTxtParaName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repChkIsActive = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repTxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2 = new AxonContLib.cPanel(this.components);
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.txtHoliday = new AxonContLib.cTextBox(this.components);
            this.txtRemark = new AxonContLib.cTextBox(this.components);
            this.cLabel5 = new AxonContLib.cLabel(this.components);
            this.Dtpholiday = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel4 = new AxonContLib.cLabel(this.components);
            this.txtWdays = new AxonContLib.cTextBox(this.components);
            this.cLabel3 = new AxonContLib.cLabel(this.components);
            this.txtLeaveType = new AxonContLib.cTextBox(this.components);
            this.cLabel2 = new AxonContLib.cLabel(this.components);
            this.DtpHolidayMaster = new System.Windows.Forms.DateTimePicker();
            this.cLabel1 = new AxonContLib.cLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // repTxtParaName
            // 
            this.repTxtParaName.AutoHeight = false;
            this.repTxtParaName.MaxLength = 100;
            this.repTxtParaName.Name = "repTxtParaName";
            // 
            // repChkIsActive
            // 
            this.repChkIsActive.AutoHeight = false;
            this.repChkIsActive.Caption = "Check";
            this.repChkIsActive.Name = "repChkIsActive";
            // 
            // repTxtRemark
            // 
            this.repTxtRemark.AutoHeight = false;
            this.repTxtRemark.Name = "repTxtRemark";
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.MainGrid);
            this.groupControl1.Controls.Add(this.panel2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 84);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1082, 389);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Holiday List";
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(2, 22);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.Size = new System.Drawing.Size(1078, 315);
            this.MainGrid.TabIndex = 1;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn6,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsCustomization.AllowFilter = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsNavigation.EnterMoveNextColumn = true;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 25;
            this.GrdDet.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrdDet_RowCellClick);
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Leave Type";
            this.gridColumn7.FieldName = "LEAVETYPE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 174;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "WDays";
            this.gridColumn1.FieldName = "WDAYS";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 79;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "Date";
            this.gridColumn6.FieldName = "HOLIDAYDATE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 134;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Remark";
            this.gridColumn2.FieldName = "REMARK";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 300;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "HOLIDAY_ID";
            this.gridColumn3.FieldName = "HOLIDAY_ID";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "LEAVETYPE_ID";
            this.gridColumn4.FieldName = "LEAVETYPE_ID";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.BtnDelete);
            this.panel2.Controls.Add(this.BtnExport);
            this.panel2.Controls.Add(this.BtnSave);
            this.panel2.Controls.Add(this.BtnAdd);
            this.panel2.Controls.Add(this.BtnBack);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(2, 337);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1078, 50);
            this.panel2.TabIndex = 0;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDelete.ImageOptions.SvgImage")));
            this.BtnDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnDelete.Location = new System.Drawing.Point(227, 8);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(103, 35);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.TabStop = false;
            this.BtnDelete.Text = "&Delete";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Appearance.Options.UseFont = true;
            this.BtnExport.Appearance.Options.UseForeColor = true;
            this.BtnExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExport.ImageOptions.SvgImage")));
            this.BtnExport.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnExport.Location = new System.Drawing.Point(336, 8);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(103, 35);
            this.BtnExport.TabIndex = 3;
            this.BtnExport.TabStop = false;
            this.BtnExport.Text = "Export";
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSave.ImageOptions.SvgImage")));
            this.BtnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnSave.Location = new System.Drawing.Point(9, 8);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 35);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAdd.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnAdd.Appearance.Options.UseFont = true;
            this.BtnAdd.Appearance.Options.UseForeColor = true;
            this.BtnAdd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnAdd.ImageOptions.SvgImage")));
            this.BtnAdd.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnAdd.Location = new System.Drawing.Point(118, 8);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(103, 35);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.TabStop = false;
            this.BtnAdd.Text = "&Clear";
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click_1);
            // 
            // BtnBack
            // 
            this.BtnBack.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBack.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnBack.Appearance.Options.UseFont = true;
            this.BtnBack.Appearance.Options.UseForeColor = true;
            this.BtnBack.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnBack.ImageOptions.SvgImage")));
            this.BtnBack.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.BtnBack.Location = new System.Drawing.Point(444, 8);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 35);
            this.BtnBack.TabIndex = 4;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtHoliday);
            this.panel1.Controls.Add(this.txtRemark);
            this.panel1.Controls.Add(this.cLabel5);
            this.panel1.Controls.Add(this.Dtpholiday);
            this.panel1.Controls.Add(this.cLabel4);
            this.panel1.Controls.Add(this.txtWdays);
            this.panel1.Controls.Add(this.cLabel3);
            this.panel1.Controls.Add(this.txtLeaveType);
            this.panel1.Controls.Add(this.cLabel2);
            this.panel1.Controls.Add(this.DtpHolidayMaster);
            this.panel1.Controls.Add(this.cLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 84);
            this.panel1.TabIndex = 0;
            // 
            // txtHoliday
            // 
            this.txtHoliday.ActivationColor = false;
            this.txtHoliday.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtHoliday.AllowTabKeyOnEnter = false;
            this.txtHoliday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoliday.Format = "";
            this.txtHoliday.IsComplusory = false;
            this.txtHoliday.Location = new System.Drawing.Point(392, 9);
            this.txtHoliday.Name = "txtHoliday";
            this.txtHoliday.SelectAllTextOnFocus = true;
            this.txtHoliday.Size = new System.Drawing.Size(77, 21);
            this.txtHoliday.TabIndex = 6;
            this.txtHoliday.ToolTips = "";
            this.txtHoliday.Visible = false;
            this.txtHoliday.WaterMarkText = null;
            // 
            // txtRemark
            // 
            this.txtRemark.ActivationColor = false;
            this.txtRemark.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtRemark.AllowTabKeyOnEnter = true;
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark.Format = "";
            this.txtRemark.IsComplusory = false;
            this.txtRemark.Location = new System.Drawing.Point(309, 37);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.SelectAllTextOnFocus = true;
            this.txtRemark.Size = new System.Drawing.Size(323, 41);
            this.txtRemark.TabIndex = 10;
            this.txtRemark.ToolTips = "";
            this.txtRemark.WaterMarkText = null;
            // 
            // cLabel5
            // 
            this.cLabel5.AutoSize = true;
            this.cLabel5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel5.ForeColor = System.Drawing.Color.Black;
            this.cLabel5.Location = new System.Drawing.Point(252, 40);
            this.cLabel5.Name = "cLabel5";
            this.cLabel5.Size = new System.Drawing.Size(57, 13);
            this.cLabel5.TabIndex = 9;
            this.cLabel5.Text = "Remark";
            this.cLabel5.ToolTips = "";
            // 
            // Dtpholiday
            // 
            this.Dtpholiday.AllowTabKeyOnEnter = true;
            this.Dtpholiday.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dtpholiday.ForeColor = System.Drawing.Color.Black;
            this.Dtpholiday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtpholiday.Location = new System.Drawing.Point(109, 7);
            this.Dtpholiday.Name = "Dtpholiday";
            this.Dtpholiday.Size = new System.Drawing.Size(137, 21);
            this.Dtpholiday.TabIndex = 3;
            this.Dtpholiday.ToolTips = "";
            // 
            // cLabel4
            // 
            this.cLabel4.AutoSize = true;
            this.cLabel4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel4.ForeColor = System.Drawing.Color.Black;
            this.cLabel4.Location = new System.Drawing.Point(12, 11);
            this.cLabel4.Name = "cLabel4";
            this.cLabel4.Size = new System.Drawing.Size(90, 13);
            this.cLabel4.TabIndex = 2;
            this.cLabel4.Text = "Holiday Date";
            this.cLabel4.ToolTips = "";
            // 
            // txtWdays
            // 
            this.txtWdays.ActivationColor = false;
            this.txtWdays.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtWdays.AllowTabKeyOnEnter = true;
            this.txtWdays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWdays.Format = "";
            this.txtWdays.IsComplusory = false;
            this.txtWdays.Location = new System.Drawing.Point(309, 9);
            this.txtWdays.Name = "txtWdays";
            this.txtWdays.SelectAllTextOnFocus = true;
            this.txtWdays.Size = new System.Drawing.Size(77, 21);
            this.txtWdays.TabIndex = 5;
            this.txtWdays.ToolTips = "";
            this.txtWdays.WaterMarkText = null;
            // 
            // cLabel3
            // 
            this.cLabel3.AutoSize = true;
            this.cLabel3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel3.ForeColor = System.Drawing.Color.Black;
            this.cLabel3.Location = new System.Drawing.Point(252, 11);
            this.cLabel3.Name = "cLabel3";
            this.cLabel3.Size = new System.Drawing.Size(51, 13);
            this.cLabel3.TabIndex = 4;
            this.cLabel3.Text = "WDays";
            this.cLabel3.ToolTips = "";
            // 
            // txtLeaveType
            // 
            this.txtLeaveType.ActivationColor = false;
            this.txtLeaveType.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtLeaveType.AllowTabKeyOnEnter = true;
            this.txtLeaveType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLeaveType.Format = "";
            this.txtLeaveType.IsComplusory = false;
            this.txtLeaveType.Location = new System.Drawing.Point(109, 36);
            this.txtLeaveType.Name = "txtLeaveType";
            this.txtLeaveType.SelectAllTextOnFocus = true;
            this.txtLeaveType.Size = new System.Drawing.Size(137, 21);
            this.txtLeaveType.TabIndex = 8;
            this.txtLeaveType.ToolTips = "";
            this.txtLeaveType.WaterMarkText = null;
            this.txtLeaveType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLeaveType_KeyPress);
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel2.ForeColor = System.Drawing.Color.Black;
            this.cLabel2.Location = new System.Drawing.Point(12, 40);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(82, 13);
            this.cLabel2.TabIndex = 7;
            this.cLabel2.Text = "Leave Type";
            this.cLabel2.ToolTips = "";
            // 
            // DtpHolidayMaster
            // 
            this.DtpHolidayMaster.CustomFormat = "dd-MM-yyyy";
            this.DtpHolidayMaster.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpHolidayMaster.Location = new System.Drawing.Point(103, -22);
            this.DtpHolidayMaster.Name = "DtpHolidayMaster";
            this.DtpHolidayMaster.Size = new System.Drawing.Size(137, 21);
            this.DtpHolidayMaster.TabIndex = 1;
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel1.ForeColor = System.Drawing.Color.Black;
            this.cLabel1.Location = new System.Drawing.Point(6, -18);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(90, 13);
            this.cLabel1.TabIndex = 0;
            this.cLabel1.Text = "Holiday Date";
            this.cLabel1.ToolTips = "";
            // 
            // FrmHolidayListMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 473);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panel1);
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmHolidayListMaster";
            this.Text = "HOLIDAY MASTER";
            ((System.ComponentModel.ISupportInitialize)(this.repTxtParaName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtParaName;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkIsActive;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtRemark;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private AxonContLib.cPanel panel2;
        private DevExpress.XtraEditors.SimpleButton BtnExport;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnAdd;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cTextBox txtWdays;
        private AxonContLib.cLabel cLabel3;
        private AxonContLib.cTextBox txtLeaveType;
        private AxonContLib.cLabel cLabel2;
        private System.Windows.Forms.DateTimePicker DtpHolidayMaster;
        private AxonContLib.cLabel cLabel1;
        private AxonContLib.cTextBox txtRemark;
        private AxonContLib.cLabel cLabel5;
        private AxonContLib.cDateTimePicker Dtpholiday;
        private AxonContLib.cLabel cLabel4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private AxonContLib.cTextBox txtHoliday;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
    }
}