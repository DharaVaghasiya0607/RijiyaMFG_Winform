namespace AxoneMFGRJ.Masters
{
    partial class FrmAttendanceEntry
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
            this.panel4 = new AxonContLib.cPanel(this.components);
            this.txtPassForDisplayBack = new AxonContLib.cTextBox(this.components);
            this.lblParty = new AxonContLib.cLabel(this.components);
            this.txtDepartment = new AxonContLib.cTextBox(this.components);
            this.BtnSearch = new System.Windows.Forms.Button();
            this.labelControl8 = new AxonContLib.cLabel(this.components);
            this.DTPAsOnDate = new AxonContLib.cDateTimePicker(this.components);
            this.panel1 = new AxonContLib.cPanel(this.components);
            this.panel5 = new AxonContLib.cPanel(this.components);
            this.lblTotal = new AxonContLib.cLabel(this.components);
            this.lblAbsent = new AxonContLib.cLabel(this.components);
            this.lblHalfDayBackColor = new AxonContLib.cLabel(this.components);
            this.lblPresent = new AxonContLib.cLabel(this.components);
            this.lblSundayBackColor = new AxonContLib.cLabel(this.components);
            this.lblHalfDay = new AxonContLib.cLabel(this.components);
            this.lblPresentBackColor = new AxonContLib.cLabel(this.components);
            this.lblSunday = new AxonContLib.cLabel(this.components);
            this.lblAbsentBackColor = new AxonContLib.cLabel(this.components);
            this.panel3 = new AxonContLib.cPanel(this.components);
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnBack = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.panel2 = new AxonContLib.cPanel(this.components);
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CmbAP = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbAP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtPassForDisplayBack);
            this.panel4.Controls.Add(this.lblParty);
            this.panel4.Controls.Add(this.txtDepartment);
            this.panel4.Controls.Add(this.BtnSearch);
            this.panel4.Controls.Add(this.labelControl8);
            this.panel4.Controls.Add(this.DTPAsOnDate);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1082, 41);
            this.panel4.TabIndex = 0;
            // 
            // txtPassForDisplayBack
            // 
            this.txtPassForDisplayBack.ActivationColor = false;
            this.txtPassForDisplayBack.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtPassForDisplayBack.AllowTabKeyOnEnter = false;
            this.txtPassForDisplayBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.txtPassForDisplayBack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassForDisplayBack.Format = "";
            this.txtPassForDisplayBack.IsComplusory = false;
            this.txtPassForDisplayBack.Location = new System.Drawing.Point(817, 17);
            this.txtPassForDisplayBack.Name = "txtPassForDisplayBack";
            this.txtPassForDisplayBack.PasswordChar = '*';
            this.txtPassForDisplayBack.SelectAllTextOnFocus = true;
            this.txtPassForDisplayBack.Size = new System.Drawing.Size(45, 14);
            this.txtPassForDisplayBack.TabIndex = 158;
            this.txtPassForDisplayBack.TabStop = false;
            this.txtPassForDisplayBack.Tag = "AXONE";
            this.txtPassForDisplayBack.ToolTips = "";
            this.txtPassForDisplayBack.WaterMarkText = null;
            // 
            // lblParty
            // 
            this.lblParty.AutoSize = true;
            this.lblParty.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParty.ForeColor = System.Drawing.Color.Black;
            this.lblParty.Location = new System.Drawing.Point(233, 13);
            this.lblParty.Name = "lblParty";
            this.lblParty.Size = new System.Drawing.Size(82, 14);
            this.lblParty.TabIndex = 138;
            this.lblParty.Text = "Department";
            this.lblParty.ToolTips = "";
            // 
            // txtDepartment
            // 
            this.txtDepartment.ActivationColor = true;
            this.txtDepartment.ActivationColorCode = System.Drawing.Color.Empty;
            this.txtDepartment.AllowTabKeyOnEnter = false;
            this.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDepartment.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartment.Format = "";
            this.txtDepartment.IsComplusory = false;
            this.txtDepartment.Location = new System.Drawing.Point(318, 9);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.SelectAllTextOnFocus = true;
            this.txtDepartment.Size = new System.Drawing.Size(384, 22);
            this.txtDepartment.TabIndex = 1;
            this.txtDepartment.ToolTips = "";
            this.txtDepartment.WaterMarkText = null;
            this.txtDepartment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDepartment_KeyPress);
            // 
            // BtnSearch
            // 
            this.BtnSearch.BackColor = System.Drawing.Color.Transparent;
            this.BtnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.ForeColor = System.Drawing.Color.Black;
            this.BtnSearch.Location = new System.Drawing.Point(708, 5);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(103, 32);
            this.BtnSearch.TabIndex = 2;
            this.BtnSearch.Text = "Sho&w F5";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.AutoSize = true;
            this.labelControl8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Location = new System.Drawing.Point(9, 13);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(64, 14);
            this.labelControl8.TabIndex = 30;
            this.labelControl8.Text = "ATD Date";
            this.labelControl8.ToolTips = "";
            // 
            // DTPAsOnDate
            // 
            this.DTPAsOnDate.AllowTabKeyOnEnter = false;
            this.DTPAsOnDate.CustomFormat = "dd/MM/yyyy";
            this.DTPAsOnDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPAsOnDate.ForeColor = System.Drawing.Color.Black;
            this.DTPAsOnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPAsOnDate.Location = new System.Drawing.Point(77, 9);
            this.DTPAsOnDate.Name = "DTPAsOnDate";
            this.DTPAsOnDate.Size = new System.Drawing.Size(151, 22);
            this.DTPAsOnDate.TabIndex = 0;
            this.DTPAsOnDate.ToolTips = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 411);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 62);
            this.panel1.TabIndex = 6;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblTotal);
            this.panel5.Controls.Add(this.lblAbsent);
            this.panel5.Controls.Add(this.lblHalfDayBackColor);
            this.panel5.Controls.Add(this.lblPresent);
            this.panel5.Controls.Add(this.lblSundayBackColor);
            this.panel5.Controls.Add(this.lblHalfDay);
            this.panel5.Controls.Add(this.lblPresentBackColor);
            this.panel5.Controls.Add(this.lblSunday);
            this.panel5.Controls.Add(this.lblAbsentBackColor);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(554, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(528, 62);
            this.panel5.TabIndex = 37;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTotal.Font = new System.Drawing.Font("Verdana", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblTotal.ForeColor = System.Drawing.Color.Purple;
            this.lblTotal.Location = new System.Drawing.Point(8, 22);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(47, 17);
            this.lblTotal.TabIndex = 33;
            this.lblTotal.Text = "Total";
            this.lblTotal.ToolTips = "";
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // lblAbsent
            // 
            this.lblAbsent.AutoSize = true;
            this.lblAbsent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAbsent.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbsent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAbsent.Location = new System.Drawing.Point(174, 11);
            this.lblAbsent.Name = "lblAbsent";
            this.lblAbsent.Size = new System.Drawing.Size(73, 13);
            this.lblAbsent.TabIndex = 33;
            this.lblAbsent.Text = "A : Absent";
            this.lblAbsent.ToolTips = "";
            this.lblAbsent.Click += new System.EventHandler(this.lblAbsent_Click);
            // 
            // lblHalfDayBackColor
            // 
            this.lblHalfDayBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblHalfDayBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHalfDayBackColor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHalfDayBackColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblHalfDayBackColor.Location = new System.Drawing.Point(143, 34);
            this.lblHalfDayBackColor.Name = "lblHalfDayBackColor";
            this.lblHalfDayBackColor.Size = new System.Drawing.Size(25, 20);
            this.lblHalfDayBackColor.TabIndex = 35;
            this.lblHalfDayBackColor.ToolTips = "";
            // 
            // lblPresent
            // 
            this.lblPresent.AutoSize = true;
            this.lblPresent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPresent.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresent.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPresent.Location = new System.Drawing.Point(359, 11);
            this.lblPresent.Name = "lblPresent";
            this.lblPresent.Size = new System.Drawing.Size(77, 13);
            this.lblPresent.TabIndex = 33;
            this.lblPresent.Text = "P : Present";
            this.lblPresent.ToolTips = "";
            this.lblPresent.Click += new System.EventHandler(this.lblPresent_Click);
            // 
            // lblSundayBackColor
            // 
            this.lblSundayBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSundayBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSundayBackColor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSundayBackColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblSundayBackColor.Location = new System.Drawing.Point(331, 34);
            this.lblSundayBackColor.Name = "lblSundayBackColor";
            this.lblSundayBackColor.Size = new System.Drawing.Size(25, 20);
            this.lblSundayBackColor.TabIndex = 35;
            this.lblSundayBackColor.ToolTips = "";
            // 
            // lblHalfDay
            // 
            this.lblHalfDay.AutoSize = true;
            this.lblHalfDay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHalfDay.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHalfDay.ForeColor = System.Drawing.Color.Teal;
            this.lblHalfDay.Location = new System.Drawing.Point(174, 38);
            this.lblHalfDay.Name = "lblHalfDay";
            this.lblHalfDay.Size = new System.Drawing.Size(83, 13);
            this.lblHalfDay.TabIndex = 33;
            this.lblHalfDay.Text = "H : Half Day";
            this.lblHalfDay.ToolTips = "";
            this.lblHalfDay.Click += new System.EventHandler(this.lblHalfDay_Click);
            // 
            // lblPresentBackColor
            // 
            this.lblPresentBackColor.BackColor = System.Drawing.Color.White;
            this.lblPresentBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPresentBackColor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentBackColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblPresentBackColor.Location = new System.Drawing.Point(331, 7);
            this.lblPresentBackColor.Name = "lblPresentBackColor";
            this.lblPresentBackColor.Size = new System.Drawing.Size(25, 20);
            this.lblPresentBackColor.TabIndex = 35;
            this.lblPresentBackColor.ToolTips = "";
            // 
            // lblSunday
            // 
            this.lblSunday.AutoSize = true;
            this.lblSunday.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSunday.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSunday.ForeColor = System.Drawing.Color.Black;
            this.lblSunday.Location = new System.Drawing.Point(359, 38);
            this.lblSunday.Name = "lblSunday";
            this.lblSunday.Size = new System.Drawing.Size(105, 13);
            this.lblSunday.TabIndex = 33;
            this.lblSunday.Text = "S : Short Leave";
            this.lblSunday.ToolTips = "";
            this.lblSunday.Click += new System.EventHandler(this.lblSunday_Click);
            // 
            // lblAbsentBackColor
            // 
            this.lblAbsentBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblAbsentBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAbsentBackColor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbsentBackColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblAbsentBackColor.Location = new System.Drawing.Point(143, 7);
            this.lblAbsentBackColor.Name = "lblAbsentBackColor";
            this.lblAbsentBackColor.Size = new System.Drawing.Size(25, 20);
            this.lblAbsentBackColor.TabIndex = 34;
            this.lblAbsentBackColor.ToolTips = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BtnAdd);
            this.panel3.Controls.Add(this.BtnBack);
            this.panel3.Controls.Add(this.BtnSave);
            this.panel3.Controls.Add(this.BtnExport);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(548, 62);
            this.panel3.TabIndex = 36;
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.White;
            this.BtnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.ForeColor = System.Drawing.Color.Black;
            this.BtnAdd.Location = new System.Drawing.Point(3, 15);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(103, 35);
            this.BtnAdd.TabIndex = 31;
            this.BtnAdd.TabStop = false;
            this.BtnAdd.Text = "&Clear";
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.BackColor = System.Drawing.Color.White;
            this.BtnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.ForeColor = System.Drawing.Color.Black;
            this.BtnBack.Location = new System.Drawing.Point(221, 16);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(103, 35);
            this.BtnBack.TabIndex = 30;
            this.BtnBack.TabStop = false;
            this.BtnBack.Text = "E&xit";
            this.BtnBack.UseVisualStyleBackColor = false;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.White;
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Location = new System.Drawing.Point(112, 15);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 35);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.BackColor = System.Drawing.Color.White;
            this.BtnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExport.ForeColor = System.Drawing.Color.Black;
            this.BtnExport.Location = new System.Drawing.Point(330, 16);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(103, 35);
            this.BtnExport.TabIndex = 32;
            this.BtnExport.TabStop = false;
            this.BtnExport.Text = "Print ";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.MainGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1082, 370);
            this.panel2.TabIndex = 19;
            // 
            // MainGrid
            // 
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(0, 0);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repTxtRemark,
            this.CmbAP});
            this.MainGrid.Size = new System.Drawing.Size(1082, 370);
            this.MainGrid.TabIndex = 1;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
            // 
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
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
            this.GrdDet.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.AppearancePrint.EvenRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.GrdDet.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Lines.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GrdDet.AppearancePrint.Lines.Options.UseBackColor = true;
            this.GrdDet.AppearancePrint.OddRow.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.AppearancePrint.OddRow.Options.UseFont = true;
            this.GrdDet.AppearancePrint.Row.Font = new System.Drawing.Font("Verdana", 8F);
            this.GrdDet.AppearancePrint.Row.Options.UseFont = true;
            this.GrdDet.ColumnPanelRowHeight = 23;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn6,
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn3,
            this.gridColumn9,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn10});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsCustomization.AllowSort = false;
            this.GrdDet.OptionsFilter.AllowFilterEditor = false;
            this.GrdDet.OptionsPrint.ExpandAllGroups = false;
            this.GrdDet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.RowHeight = 23;
            this.GrdDet.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GrdDet_RowStyle);
            this.GrdDet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdDet_KeyDown);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Employee";
            this.gridColumn2.FieldName = "EMPLOYEENAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 249;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "Department";
            this.gridColumn6.FieldName = "DEPARTMENTNAME";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 151;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Designation";
            this.gridColumn1.FieldName = "DESIGNATIONNAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Width = 132;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "AP";
            this.gridColumn4.ColumnEdit = this.CmbAP;
            this.gridColumn4.FieldName = "AP";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 64;
            // 
            // CmbAP
            // 
            this.CmbAP.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.CmbAP.Appearance.Options.UseFont = true;
            this.CmbAP.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 10F);
            this.CmbAP.AppearanceDropDown.Options.UseFont = true;
            this.CmbAP.AppearanceFocused.Font = new System.Drawing.Font("Verdana", 10F);
            this.CmbAP.AppearanceFocused.Options.UseFont = true;
            this.CmbAP.AutoHeight = false;
            this.CmbAP.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbAP.Items.AddRange(new object[] {
            "A",
            "P",
            "H",
            "S"});
            this.CmbAP.Name = "CmbAP";
            this.CmbAP.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.CmbAP.SelectedValueChanged += new System.EventHandler(this.CmbAP_SelectedValueChanged);
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "WDays";
            this.gridColumn3.DisplayFormat.FormatString = "{0:N2}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "WDAYS";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 76;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "WHours";
            this.gridColumn9.DisplayFormat.FormatString = "{0:N2}";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "WHOURS";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "OT HH";
            this.gridColumn5.DisplayFormat.FormatString = "{0:N0}";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "OTHH";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "OT MM";
            this.gridColumn7.DisplayFormat.FormatString = "{0:N0}";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "OTMM";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 86;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Remark";
            this.gridColumn8.ColumnEdit = this.repTxtRemark;
            this.gridColumn8.FieldName = "REMARK";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 287;
            // 
            // repTxtRemark
            // 
            this.repTxtRemark.AutoHeight = false;
            this.repTxtRemark.Name = "repTxtRemark";
            this.repTxtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repTxtRemark_KeyDown);
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "Code";
            this.gridColumn10.FieldName = "EMPLOYEECODE";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 95;
            // 
            // FrmAttendanceEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 473);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.LookAndFeel.SkinName = "Stardust";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmAttendanceEntry";
            this.Text = "DAILY ATTENDANCE ENTRY";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLedger_KeyDown);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbAP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtRemark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxonContLib.cPanel panel4;
        private AxonContLib.cPanel panel1;
        private AxonContLib.cPanel panel2;
        private AxonContLib.cDateTimePicker DTPAsOnDate;
        private AxonContLib.cLabel labelControl8;
        private System.Windows.Forms.Button BtnSearch;
        private AxonContLib.cTextBox txtDepartment;
        private AxonContLib.cLabel lblParty;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnBack;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox CmbAP;
        private AxonContLib.cLabel lblAbsent;
        private AxonContLib.cLabel lblPresent;
        private AxonContLib.cLabel lblHalfDay;
        private AxonContLib.cLabel lblSunday;
        private AxonContLib.cLabel lblTotal;
        private AxonContLib.cLabel lblAbsentBackColor;
        private AxonContLib.cLabel lblHalfDayBackColor;
        private AxonContLib.cLabel lblSundayBackColor;
        private AxonContLib.cLabel lblPresentBackColor;
        private AxonContLib.cPanel panel5;
        private AxonContLib.cPanel panel3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private AxonContLib.cTextBox txtPassForDisplayBack;


    }
}