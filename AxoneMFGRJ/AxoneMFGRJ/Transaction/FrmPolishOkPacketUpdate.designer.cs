namespace AxoneMFGRJ.Transaction
{
	partial class FrmPolishOkPacketUpdate
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
			this.panel1 = new AxonContLib.cPanel();
			this.cLabel5 = new AxonContLib.cLabel(this.components);
			this.DtpDate = new AxonContLib.cDateTimePicker(this.components);
			this.BtnUpdate = new DevExpress.XtraEditors.SimpleButton();
			this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
			this.MainGrd = new DevExpress.XtraGrid.GridControl();
			this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.RepDtpUpdatedate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.RepDtpOrgDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.BtnExport = new DevExpress.XtraEditors.SimpleButton();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainGrd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RepDtpUpdatedate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RepDtpUpdatedate.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RepDtpOrgDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RepDtpOrgDate.CalendarTimeProperties)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.BtnExport);
			this.panel1.Controls.Add(this.cLabel5);
			this.panel1.Controls.Add(this.DtpDate);
			this.panel1.Controls.Add(this.BtnUpdate);
			this.panel1.Controls.Add(this.BtnSearch);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(971, 60);
			this.panel1.TabIndex = 0;
			// 
			// cLabel5
			// 
			this.cLabel5.AutoSize = true;
			this.cLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
			this.cLabel5.ForeColor = System.Drawing.Color.Black;
			this.cLabel5.Location = new System.Drawing.Point(12, 19);
			this.cLabel5.Name = "cLabel5";
			this.cLabel5.Size = new System.Drawing.Size(38, 14);
			this.cLabel5.TabIndex = 0;
			this.cLabel5.Text = "Date";
			this.cLabel5.ToolTips = "";
			// 
			// DtpDate
			// 
			this.DtpDate.CustomFormat = "dd/MM/yyyy";
			this.DtpDate.Font = new System.Drawing.Font("Verdana", 10F);
			this.DtpDate.ForeColor = System.Drawing.Color.Black;
			this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.DtpDate.Location = new System.Drawing.Point(59, 14);
			this.DtpDate.Name = "DtpDate";
			this.DtpDate.Size = new System.Drawing.Size(129, 24);
			this.DtpDate.TabIndex = 1;
			this.DtpDate.ToolTips = "";
			this.DtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpDate_KeyDown);
			// 
			// BtnUpdate
			// 
			this.BtnUpdate.Appearance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
			this.BtnUpdate.Appearance.Options.UseFont = true;
			this.BtnUpdate.Appearance.Options.UseTextOptions = true;
			this.BtnUpdate.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BtnUpdate.Location = new System.Drawing.Point(316, 8);
			this.BtnUpdate.Name = "BtnUpdate";
			this.BtnUpdate.Size = new System.Drawing.Size(109, 37);
			this.BtnUpdate.TabIndex = 3;
			this.BtnUpdate.Text = "Update";
			this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
			// 
			// BtnSearch
			// 
			this.BtnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
			this.BtnSearch.Appearance.Options.UseFont = true;
			this.BtnSearch.Appearance.Options.UseTextOptions = true;
			this.BtnSearch.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.BtnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BtnSearch.Location = new System.Drawing.Point(201, 8);
			this.BtnSearch.Name = "BtnSearch";
			this.BtnSearch.Size = new System.Drawing.Size(109, 37);
			this.BtnSearch.TabIndex = 2;
			this.BtnSearch.Text = "Search";
			this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
			// 
			// MainGrd
			// 
			this.MainGrd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainGrd.Location = new System.Drawing.Point(0, 60);
			this.MainGrd.MainView = this.GrdDet;
			this.MainGrd.Name = "MainGrd";
			this.MainGrd.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepDtpUpdatedate,
            this.RepDtpOrgDate});
			this.MainGrd.Size = new System.Drawing.Size(971, 486);
			this.MainGrd.TabIndex = 1;
			this.MainGrd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
			// 
			// GrdDet
			// 
			this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
			this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
			this.GrdDet.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
			this.GrdDet.Appearance.FocusedCell.Options.UseFont = true;
			this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
			this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
			this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
			this.GrdDet.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
			this.GrdDet.Appearance.GroupFooter.Options.UseFont = true;
			this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
			this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
			this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
			this.GrdDet.Appearance.HorzLine.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
			this.GrdDet.Appearance.HorzLine.Options.UseFont = true;
			this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.GrdDet.Appearance.Row.Options.UseFont = true;
			this.GrdDet.Appearance.SelectedRow.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.GrdDet.Appearance.SelectedRow.Options.UseFont = true;
			this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
			this.GrdDet.Appearance.VertLine.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
			this.GrdDet.Appearance.VertLine.Options.UseFont = true;
			this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 7.55F, System.Drawing.FontStyle.Bold);
			this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.GrdDet.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
			this.GrdDet.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.85F, System.Drawing.FontStyle.Bold);
			this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.Gray;
			this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
			this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 8F);
			this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
			this.GrdDet.ColumnPanelRowHeight = 25;
			this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn26,
            this.gridColumn14,
            this.gridColumn13,
            this.gridColumn12,
            this.gridColumn11});
			this.GrdDet.GridControl = this.MainGrd;
			this.GrdDet.Name = "GrdDet";
			this.GrdDet.OptionsBehavior.Editable = false;
			this.GrdDet.OptionsNavigation.EnterMoveNextColumn = true;
			this.GrdDet.OptionsPrint.ExpandAllGroups = false;
			this.GrdDet.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.GrdDet.OptionsSelection.EnableAppearanceHideSelection = false;
			this.GrdDet.OptionsSelection.MultiSelect = true;
			this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
			this.GrdDet.OptionsView.ColumnAutoWidth = false;
			this.GrdDet.OptionsView.ShowAutoFilterRow = true;
			this.GrdDet.OptionsView.ShowFooter = true;
			this.GrdDet.OptionsView.ShowGroupPanel = false;
			this.GrdDet.RowHeight = 23;
			// 
			// gridColumn1
			// 
			this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn1.AppearanceCell.Options.UseFont = true;
			this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.Caption = "Update Date";
			this.gridColumn1.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
			this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumn1.FieldName = "FINALTRANSDATE";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 88;
			// 
			// RepDtpUpdatedate
			// 
			this.RepDtpUpdatedate.AutoHeight = false;
			this.RepDtpUpdatedate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.RepDtpUpdatedate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.RepDtpUpdatedate.Name = "RepDtpUpdatedate";
			// 
			// gridColumn2
			// 
			this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn2.AppearanceCell.Options.UseFont = true;
			this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn2.Caption = "First Date";
			this.gridColumn2.FieldName = "FIRSTDATE";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 72;
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn3.AppearanceCell.Options.UseFont = true;
			this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.Caption = "Org Date";
			this.gridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
			this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumn3.FieldName = "ORGDATE";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 2;
			this.gridColumn3.Width = 81;
			// 
			// RepDtpOrgDate
			// 
			this.RepDtpOrgDate.AutoHeight = false;
			this.RepDtpOrgDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.RepDtpOrgDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.RepDtpOrgDate.Name = "RepDtpOrgDate";
			// 
			// gridColumn4
			// 
			this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn4.AppearanceCell.Options.UseFont = true;
			this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.Caption = "Kapan";
			this.gridColumn4.FieldName = "KAPANNAME";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 3;
			this.gridColumn4.Width = 56;
			// 
			// gridColumn5
			// 
			this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn5.AppearanceCell.Options.UseFont = true;
			this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn5.Caption = "Pkt";
			this.gridColumn5.FieldName = "PACKETNO";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 4;
			this.gridColumn5.Width = 40;
			// 
			// gridColumn6
			// 
			this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn6.AppearanceCell.Options.UseFont = true;
			this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn6.Caption = "Tag";
			this.gridColumn6.FieldName = "TAG";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.AllowEdit = false;
			this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 5;
			this.gridColumn6.Width = 40;
			// 
			// gridColumn7
			// 
			this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn7.AppearanceCell.Options.UseFont = true;
			this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn7.Caption = "Janged No";
			this.gridColumn7.FieldName = "JANGEDNO";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.AllowEdit = false;
			this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 6;
			this.gridColumn7.Width = 79;
			// 
			// gridColumn8
			// 
			this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn8.AppearanceCell.Options.UseFont = true;
			this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.Caption = "Entry Type";
			this.gridColumn8.FieldName = "ENTRYTYPE";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.AllowEdit = false;
			this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 7;
			this.gridColumn8.Width = 82;
			// 
			// gridColumn26
			// 
			this.gridColumn26.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn26.AppearanceCell.Options.UseFont = true;
			this.gridColumn26.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn26.Caption = "FromEmp";
			this.gridColumn26.FieldName = "FROMEMPCODE";
			this.gridColumn26.Name = "gridColumn26";
			this.gridColumn26.OptionsColumn.AllowEdit = false;
			this.gridColumn26.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn26.Visible = true;
			this.gridColumn26.VisibleIndex = 8;
			this.gridColumn26.Width = 83;
			// 
			// gridColumn14
			// 
			this.gridColumn14.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn14.AppearanceCell.Options.UseFont = true;
			this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn14.Caption = "ToEmp";
			this.gridColumn14.FieldName = "TOEMPCODE";
			this.gridColumn14.Name = "gridColumn14";
			this.gridColumn14.OptionsColumn.AllowEdit = false;
			this.gridColumn14.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn14.Visible = true;
			this.gridColumn14.VisibleIndex = 9;
			this.gridColumn14.Width = 61;
			// 
			// gridColumn13
			// 
			this.gridColumn13.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn13.AppearanceCell.Options.UseFont = true;
			this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn13.Caption = "From Proc";
			this.gridColumn13.FieldName = "FROMPROCESS";
			this.gridColumn13.Name = "gridColumn13";
			this.gridColumn13.OptionsColumn.AllowEdit = false;
			this.gridColumn13.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn13.Visible = true;
			this.gridColumn13.VisibleIndex = 10;
			this.gridColumn13.Width = 79;
			// 
			// gridColumn12
			// 
			this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn12.AppearanceCell.Options.UseFont = true;
			this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn12.Caption = "To Proc";
			this.gridColumn12.FieldName = "TOPROCESS";
			this.gridColumn12.Name = "gridColumn12";
			this.gridColumn12.OptionsColumn.AllowEdit = false;
			this.gridColumn12.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn12.Visible = true;
			this.gridColumn12.VisibleIndex = 11;
			this.gridColumn12.Width = 63;
			// 
			// gridColumn11
			// 
			this.gridColumn11.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.5F);
			this.gridColumn11.AppearanceCell.Options.UseFont = true;
			this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn11.Caption = "Trn_Id";
			this.gridColumn11.FieldName = "TRN_ID";
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
			this.gridColumn11.Width = 45;
			// 
			// BtnExport
			// 
			this.BtnExport.Appearance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
			this.BtnExport.Appearance.Options.UseFont = true;
			this.BtnExport.Appearance.Options.UseTextOptions = true;
			this.BtnExport.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.BtnExport.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BtnExport.Location = new System.Drawing.Point(496, 8);
			this.BtnExport.Name = "BtnExport";
			this.BtnExport.Size = new System.Drawing.Size(109, 37);
			this.BtnExport.TabIndex = 4;
			this.BtnExport.Text = "Excel Export";
			this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
			// 
			// FrmPolishOkPacketUpdate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(971, 546);
			this.Controls.Add(this.MainGrd);
			this.Controls.Add(this.panel1);
			this.Name = "FrmPolishOkPacketUpdate";
			this.Text = "POLISH OK PACKET UPDATE";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainGrd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RepDtpUpdatedate.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RepDtpUpdatedate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RepDtpOrgDate.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RepDtpOrgDate)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private AxonContLib.cPanel panel1;
		private DevExpress.XtraEditors.SimpleButton BtnUpdate;
		private DevExpress.XtraEditors.SimpleButton BtnSearch;
		private AxonContLib.cDateTimePicker DtpDate;
		private AxonContLib.cLabel cLabel5;
		private DevExpress.XtraGrid.GridControl MainGrd;
		private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit RepDtpUpdatedate;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit RepDtpOrgDate;
		private DevExpress.XtraEditors.SimpleButton BtnExport;
	}
}