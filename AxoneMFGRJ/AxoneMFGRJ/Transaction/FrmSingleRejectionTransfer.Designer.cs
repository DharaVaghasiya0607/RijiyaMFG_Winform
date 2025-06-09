namespace AxoneMFGRJ.Transaction
{
    partial class FrmSingleRejectionTransfer
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.PanelHeader = new AxonContLib.cPanel();
            this.BtnApplyAll = new DevExpress.XtraEditors.SimpleButton();
            this.DTPTransferDate = new AxonContLib.cDateTimePicker(this.components);
            this.cLabel7 = new AxonContLib.cLabel(this.components);
            this.lblRate = new AxonContLib.cLabel(this.components);
            this.cLabel18 = new AxonContLib.cLabel(this.components);
            this.txtRate = new AxonContLib.cTextBox(this.components);
            this.txtRejection = new AxonContLib.cTextBox(this.components);
            this.panel2 = new AxonContLib.cPanel();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new AxonContLib.cPanel();
            this.lblAccID = new AxonContLib.cLabel(this.components);
            this.txtTotalCarat = new AxonContLib.cTextBox(this.components);
            this.txtTotalPcs = new AxonContLib.cTextBox(this.components);
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn54 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CmbReturnType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.BtnRejectionTransfer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.PanelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbReturnType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.White;
            this.PanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelHeader.Controls.Add(this.BtnApplyAll);
            this.PanelHeader.Controls.Add(this.DTPTransferDate);
            this.PanelHeader.Controls.Add(this.cLabel7);
            this.PanelHeader.Controls.Add(this.lblRate);
            this.PanelHeader.Controls.Add(this.cLabel18);
            this.PanelHeader.Controls.Add(this.txtRate);
            this.PanelHeader.Controls.Add(this.txtRejection);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1008, 84);
            this.PanelHeader.TabIndex = 0;
            this.PanelHeader.TabStop = true;
            // 
            // BtnApplyAll
            // 
            this.BtnApplyAll.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnApplyAll.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnApplyAll.Appearance.Options.UseFont = true;
            this.BtnApplyAll.Appearance.Options.UseForeColor = true;
            this.BtnApplyAll.Location = new System.Drawing.Point(472, 14);
            this.BtnApplyAll.Name = "BtnApplyAll";
            this.BtnApplyAll.Size = new System.Drawing.Size(84, 51);
            this.BtnApplyAll.TabIndex = 6;
            this.BtnApplyAll.Text = "Apply All";
            this.BtnApplyAll.Click += new System.EventHandler(this.BtnApplyAll_Click);
            // 
            // DTPTransferDate
            // 
            this.DTPTransferDate.AllowTabKeyOnEnter = false;
            this.DTPTransferDate.CustomFormat = "dd/MM/yyyy";
            this.DTPTransferDate.Enabled = false;
            this.DTPTransferDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.DTPTransferDate.ForeColor = System.Drawing.Color.Black;
            this.DTPTransferDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPTransferDate.Location = new System.Drawing.Point(11, 42);
            this.DTPTransferDate.Name = "DTPTransferDate";
            this.DTPTransferDate.Size = new System.Drawing.Size(129, 24);
            this.DTPTransferDate.TabIndex = 1;
            this.DTPTransferDate.ToolTips = "";
            // 
            // cLabel7
            // 
            this.cLabel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cLabel7.Enabled = false;
            this.cLabel7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel7.ForeColor = System.Drawing.Color.Black;
            this.cLabel7.Location = new System.Drawing.Point(11, 14);
            this.cLabel7.Name = "cLabel7";
            this.cLabel7.Size = new System.Drawing.Size(129, 24);
            this.cLabel7.TabIndex = 0;
            this.cLabel7.Text = "Transfer Date";
            this.cLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel7.ToolTips = "";
            // 
            // lblRate
            // 
            this.lblRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate.ForeColor = System.Drawing.Color.Black;
            this.lblRate.Location = new System.Drawing.Point(369, 14);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(97, 24);
            this.lblRate.TabIndex = 4;
            this.lblRate.Text = "Rate";
            this.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRate.ToolTips = "";
            // 
            // cLabel18
            // 
            this.cLabel18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cLabel18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cLabel18.ForeColor = System.Drawing.Color.Black;
            this.cLabel18.Location = new System.Drawing.Point(148, 15);
            this.cLabel18.Name = "cLabel18";
            this.cLabel18.Size = new System.Drawing.Size(215, 24);
            this.cLabel18.TabIndex = 2;
            this.cLabel18.Text = "Rejection Name";
            this.cLabel18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cLabel18.ToolTips = "";
            // 
            // txtRate
            // 
            this.txtRate.ActivationColor = true;
            this.txtRate.AllowTabKeyOnEnter = false;
            this.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRate.ComplusoryMsg = null;
            this.txtRate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRate.Format = "######0";
            this.txtRate.IsComplusory = false;
            this.txtRate.Location = new System.Drawing.Point(369, 42);
            this.txtRate.Name = "txtRate";
            this.txtRate.RequiredChars = "0123456789.";
            this.txtRate.SelectAllTextOnFocus = true;
            this.txtRate.ShowToolTipOnFocus = false;
            this.txtRate.Size = new System.Drawing.Size(97, 23);
            this.txtRate.TabIndex = 5;
            this.txtRate.Text = "0";
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRate.ToolTips = "";
            this.txtRate.WaterMarkText = null;
            // 
            // txtRejection
            // 
            this.txtRejection.ActivationColor = true;
            this.txtRejection.AllowTabKeyOnEnter = false;
            this.txtRejection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRejection.ComplusoryMsg = null;
            this.txtRejection.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRejection.Format = "";
            this.txtRejection.IsComplusory = false;
            this.txtRejection.Location = new System.Drawing.Point(148, 42);
            this.txtRejection.Name = "txtRejection";
            this.txtRejection.RequiredChars = "";
            this.txtRejection.SelectAllTextOnFocus = true;
            this.txtRejection.ShowToolTipOnFocus = false;
            this.txtRejection.Size = new System.Drawing.Size(215, 23);
            this.txtRejection.TabIndex = 3;
            this.txtRejection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRejection.ToolTips = "";
            this.txtRejection.WaterMarkText = null;
            this.txtRejection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRejection_KeyPress);
            this.txtRejection.Validating += new System.ComponentModel.CancelEventHandler(this.txtRejection_Validating);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.BtnSave);
            this.panel2.Controls.Add(this.BtnBack);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 423);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 50);
            this.panel2.TabIndex = 2;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.Location = new System.Drawing.Point(6, 7);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(177, 35);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "&Save / Transfer";
            this.BtnSave.Click += new System.EventHandler(this.BtnTransfer_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BtnBack.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnBack.Appearance.Options.UseFont = true;
            this.BtnBack.Appearance.Options.UseForeColor = true;
            this.BtnBack.Location = new System.Drawing.Point(188, 7);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 35);
            this.BtnBack.TabIndex = 154;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblAccID);
            this.panel1.Controls.Add(this.txtTotalCarat);
            this.panel1.Controls.Add(this.txtTotalPcs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(749, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 50);
            this.panel1.TabIndex = 151;
            // 
            // lblAccID
            // 
            this.lblAccID.AutoSize = true;
            this.lblAccID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblAccID.ForeColor = System.Drawing.Color.Black;
            this.lblAccID.Location = new System.Drawing.Point(25, 17);
            this.lblAccID.Name = "lblAccID";
            this.lblAccID.Size = new System.Drawing.Size(40, 14);
            this.lblAccID.TabIndex = 152;
            this.lblAccID.Text = "Total";
            this.lblAccID.ToolTips = "";
            // 
            // txtTotalCarat
            // 
            this.txtTotalCarat.ActivationColor = false;
            this.txtTotalCarat.AllowTabKeyOnEnter = false;
            this.txtTotalCarat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTotalCarat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalCarat.ComplusoryMsg = null;
            this.txtTotalCarat.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtTotalCarat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalCarat.Format = "";
            this.txtTotalCarat.IsComplusory = false;
            this.txtTotalCarat.Location = new System.Drawing.Point(140, 13);
            this.txtTotalCarat.Name = "txtTotalCarat";
            this.txtTotalCarat.ReadOnly = true;
            this.txtTotalCarat.RequiredChars = "";
            this.txtTotalCarat.SelectAllTextOnFocus = true;
            this.txtTotalCarat.ShowToolTipOnFocus = false;
            this.txtTotalCarat.Size = new System.Drawing.Size(107, 22);
            this.txtTotalCarat.TabIndex = 149;
            this.txtTotalCarat.TabStop = false;
            this.txtTotalCarat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalCarat.ToolTips = "";
            this.txtTotalCarat.WaterMarkText = null;
            // 
            // txtTotalPcs
            // 
            this.txtTotalPcs.ActivationColor = false;
            this.txtTotalPcs.AllowTabKeyOnEnter = false;
            this.txtTotalPcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTotalPcs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalPcs.ComplusoryMsg = null;
            this.txtTotalPcs.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtTotalPcs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalPcs.Format = "";
            this.txtTotalPcs.IsComplusory = false;
            this.txtTotalPcs.Location = new System.Drawing.Point(68, 13);
            this.txtTotalPcs.Name = "txtTotalPcs";
            this.txtTotalPcs.ReadOnly = true;
            this.txtTotalPcs.RequiredChars = "";
            this.txtTotalPcs.SelectAllTextOnFocus = true;
            this.txtTotalPcs.ShowToolTipOnFocus = false;
            this.txtTotalPcs.Size = new System.Drawing.Size(73, 22);
            this.txtTotalPcs.TabIndex = 149;
            this.txtTotalPcs.TabStop = false;
            this.txtTotalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalPcs.ToolTips = "";
            this.txtTotalPcs.WaterMarkText = null;
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 84);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.CmbReturnType});
            this.MainGrid.Size = new System.Drawing.Size(1008, 339);
            this.MainGrid.TabIndex = 1;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(141)))));
            this.GrdDet.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDet.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDet.Appearance.VertLine.BackColor = System.Drawing.Color.Gray;
            this.GrdDet.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDet.ColumnPanelRowHeight = 25;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn54,
            this.gridColumn15,
            this.gridColumn7,
            this.gridColumn13});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            this.GrdDet.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrdDet_CellValueChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.Caption = "PACKET_ID";
            this.gridColumn1.FieldName = "PACKET_ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.Caption = "KAPAN_ID";
            this.gridColumn2.FieldName = "KAPAN_ID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.Caption = "Lot";
            this.gridColumn3.FieldName = "KAPANNAME";
            this.gridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 71;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.Caption = "SL";
            this.gridColumn4.FieldName = "SUBLOT";
            this.gridColumn4.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Width = 64;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.Caption = "SL1";
            this.gridColumn5.FieldName = "SUBLOT1";
            this.gridColumn5.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Width = 67;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn6.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.Caption = "PktNo";
            this.gridColumn6.FieldName = "PACKETNO";
            this.gridColumn6.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.Caption = "Tag";
            this.gridColumn8.FieldName = "TAG";
            this.gridColumn8.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 40;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn9.AppearanceCell.Options.UseFont = true;
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.Caption = "BARCODE";
            this.gridColumn9.FieldName = "BARCODE";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn10.AppearanceCell.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.Caption = "RFIDTAG";
            this.gridColumn10.FieldName = "RFIDTAG";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn11.AppearanceCell.Options.UseFont = true;
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn11.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn11.AppearanceHeader.Options.UseFont = true;
            this.gridColumn11.Caption = "IssPcs";
            this.gridColumn11.FieldName = "ISSUEPCS";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 3;
            this.gridColumn11.Width = 54;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn12.AppearanceCell.Options.UseFont = true;
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn12.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn12.AppearanceHeader.Options.UseFont = true;
            this.gridColumn12.Caption = "IssCts";
            this.gridColumn12.FieldName = "ISSUECARAT";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 4;
            this.gridColumn12.Width = 82;
            // 
            // gridColumn54
            // 
            this.gridColumn54.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn54.AppearanceCell.Options.UseFont = true;
            this.gridColumn54.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn54.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn54.Caption = "Remark";
            this.gridColumn54.FieldName = "REMARK";
            this.gridColumn54.Name = "gridColumn54";
            this.gridColumn54.OptionsColumn.AllowEdit = false;
            this.gridColumn54.Visible = true;
            this.gridColumn54.VisibleIndex = 7;
            this.gridColumn54.Width = 223;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "OLDTRN_ID";
            this.gridColumn15.FieldName = "OLDTRN_ID";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn7.Caption = "Rate";
            this.gridColumn7.DisplayFormat.FormatString = "{0:N3}";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "RATE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridColumn13.AppearanceCell.Options.UseFont = true;
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn13.Caption = "Amount";
            this.gridColumn13.DisplayFormat.FormatString = "{0:N3}";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn13.FieldName = "AMOUNT";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 6;
            // 
            // CmbReturnType
            // 
            this.CmbReturnType.AutoHeight = false;
            this.CmbReturnType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbReturnType.Items.AddRange(new object[] {
            "DONE",
            "NOT DONE"});
            this.CmbReturnType.Name = "CmbReturnType";
            this.CmbReturnType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // BtnRejectionTransfer
            // 
            this.BtnRejectionTransfer.AutoHeight = false;
            serializableAppearanceObject1.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            serializableAppearanceObject1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            serializableAppearanceObject1.Options.UseFont = true;
            serializableAppearanceObject1.Options.UseForeColor = true;
            serializableAppearanceObject1.Options.UseTextOptions = true;
            serializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BtnRejectionTransfer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Rej. x\'fer", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.BtnRejectionTransfer.Name = "BtnRejectionTransfer";
            this.BtnRejectionTransfer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // FrmSingleRejectionTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 473);
            this.Controls.Add(this.MainGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PanelHeader);
            this.Name = "FrmSingleRejectionTransfer";
            this.Text = "REJECTION TRANSFER";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmKapanDashboard_KeyDown);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbReturnType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnRejectionTransfer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel PanelHeader;
        private AxonContLib.cPanel panel2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnRejectionTransfer;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private AxonContLib.cTextBox txtTotalPcs;
        private AxonContLib.cTextBox txtTotalCarat;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn54;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cLabel lblAccID;
        private AxonContLib.cTextBox txtRejection;
        private AxonContLib.cLabel cLabel18;
        private AxonContLib.cLabel cLabel7;
        private AxonContLib.cDateTimePicker DTPTransferDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox CmbReturnType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private AxonContLib.cLabel lblRate;
        private AxonContLib.cTextBox txtRate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.SimpleButton BtnApplyAll;


    }
}