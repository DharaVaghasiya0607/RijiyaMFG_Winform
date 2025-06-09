using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSinglePolishOKTransfer : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOFormPer ObjPer = new BOFormPer();
        BOTRN_SinglePolishOKTransfer ObjMast = new BOTRN_SinglePolishOKTransfer();

        DataTable DTabPolish = new DataTable();
        DataTable DTabDataSum = new DataTable();
        DataTable DTabSummary = new DataTable();
        DataTable DTabPrint = new DataTable();//urvisha add by :10052023
        double balcarat = 0;
        double orgcarat = 0;
        double losscarat = 0;

        Int32 SrNo = 0;
        string KapanName = "";
        Int64 JangedNo = 0;
        Int64 Manager_ID = 0;
        Int64 Employee_ID = 0;

        Int64 Barcode = 0;
        string PktNo = "";
        string Tag = "";
        string FromDate = "";
        string ToDate = "";

        string pStrMainKapan = "";

        #region Property Setting
        public FrmSinglePolishOKTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            //ObjPer.GetFormPermission(Val.ToString(this.Tag));
            //BtnSave.Enabled = ObjPer.ISINSERT;
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            // Fill();

            CmbKapan.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.DisplayMember = "KAPANNAME";
            CmbKapan.ValueMember = "KAPANNAME";

            CmbKapan.SelectedIndex = -1;

            GrdDet.BeginUpdate();
            DTabPolish = ObjMast.PolishOKTransferGetData(0, 0, "-1", "", "", 0, null, null, 0, 0, "");
            MainGridDetail.DataSource = DTabPolish;
            MainGridDetail.Refresh();

            if (MainGridDetail.RepositoryItems.Count == 0)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDet;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdDet.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            GrdDet.EndUpdate();

            DTabSummary = new DataTable();
            DTabSummary.Columns.Add(new DataColumn("KAPAN_ID", typeof(Int32)));
            DTabSummary.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DTabSummary.Columns.Add(new DataColumn("MANAGER_ID", typeof(Int32)));
            DTabSummary.Columns.Add(new DataColumn("MAINMAINMANAGER", typeof(string)));
            DTabSummary.Columns.Add(new DataColumn("ORGPCS", typeof(Int32)));
            DTabSummary.Columns.Add(new DataColumn("ORGCARAT", typeof(double)));
            DTabSummary.Columns.Add(new DataColumn("BALANCEPCS", typeof(Int32)));
            DTabSummary.Columns.Add(new DataColumn("BALANCECARAT", typeof(double)));
            DTabSummary.Columns.Add(new DataColumn("DIFFCARAT", typeof(double)));
            DTabSummary.Columns.Add(new DataColumn("SRNO", typeof(Int32)));
            DTabSummary.Columns.Add(new DataColumn("PACKETNO", typeof(Int32)));

            DTabSummary.Rows.Add(DTabSummary.NewRow());
            MainGrid.DataSource = DTabSummary;
            MainGrid.Refresh();
            CmbKapan.Focus();
            this.Show();
        }

        //private void Fill()
        //{

        //    DTabDataSum.Columns.Add("PARTY_ID", typeof(System.Int32));
        //    DTabDataSum.Columns.Add("PARTYNAME", typeof(System.String));
        //    DTabDataSum.Columns.Add("TOEMPLOYEENAME", typeof(System.String));
        //    DTabDataSum.Columns.Add("TOEMPLOYEECODE", typeof(System.String));
        //    DTabDataSum.Columns.Add("TOEMPLOYEE_ID", typeof(System.Int32));
        //    DTabDataSum.Columns.Add("PCS", typeof(System.Int32));
        //    DTabDataSum.Columns.Add("KAPANNAME", typeof(System.String));
        //    DTabDataSum.Columns.Add("CARAT", typeof(System.Double));
        //    DTabDataSum.Columns.Add("PARTYCODE", typeof(System.String));
        //    DTabDataSum.Columns.Add("TOMANAGER_ID", typeof(System.Int32));
        //    DTabDataSum.Columns.Add("TODEPARTMENT_ID", typeof(System.Int32));
        //    DTabDataSum.Columns.Add("KAPAN_ID", typeof(System.Int32));           
        //    DTabDataSum.Columns.Add("TODEPARTMENTNAME", typeof(System.String));
        //    DTabDataSum.Columns.Add("TOMANAGERNAME", typeof(System.String));

        //    DTabDataSum.Rows.Add(DTabDataSum.NewRow());
        //    MainGrid.DataSource = DTabDataSum;
        //    MainGrid.Refresh();
        //}

        private void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }
        #endregion
        private void TxtManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = ObjMast.PolishOKTransferMainManager(Val.ToString(CmbKapan.Text), BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "MAINMANAGER";
                    FrmSearch.ValueMemeter = "MAINMANAGER_ID";
                    FrmSearch.DisplayMemeter = "MAINMANAGER";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        TxtMainManager.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        TxtMainManager.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void RbtBarcode_CheckedChanged_1(object sender, EventArgs e)
        {
            if (RbtBarcode.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtBarcode.Focus();
            }
            else if (RbtJangedNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtJangedNo.Focus();
            }
            else if (RbtPacketNo.Checked)
            {
                txtJangedNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtKapanName.Focus();
            }
            else if (RbtPktSerialNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Focus();
            }
            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelJangedNo.Visible = RbtJangedNo.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                Barcode = Val.ToInt64(txtBarcode.Text);
                SrNo = Val.ToInt32(txtSrNoSerialNo.Text);
                PktNo = Val.ToString(txtPacketNo.Text).Trim().Equals(string.Empty) ? "0" : Val.ToString(txtPacketNo.Text);
                Tag = Val.ToString(txtTag.Text);
                JangedNo = Val.ToInt64(txtJangedNo.Text);
                FromDate = "";
                ToDate = "";

                // Dhara : 17-04-2023 
                if (Val.ToString(TxtMainManager.Text).Trim().Equals(string.Empty))
                {
                    Manager_ID = 0;
                }
                else
                {
                    Manager_ID = Val.ToInt64(TxtMainManager.Tag);
                }

                Employee_ID = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                // End #Dhara : 17-04-2023

                if (RbtPktSerialNo.Checked == true)
                {
                    KapanName = Val.ToString(txtSrNoKapanName.Text);
                }
                else if (RbtPacketNo.Checked == true)
                {
                    KapanName = Val.ToString(txtKapanName.Text);
                }
                if (DTPFromDate.Checked == true)
                {
                    FromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    ToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                pStrMainKapan = Val.ToString(CmbKapan.Text);

                DTabPolish.Rows.Clear();

                if (ObjGridSelection != null)
                {
                    ObjGridSelection.ClearSelection();
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                BtnShow.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repTxtPartyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdData.SetFocusedRowCellValue("PARTYNAME", Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]));
                        GrdData.SetFocusedRowCellValue("PARTY_ID", Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]));
                        GrdData.SetFocusedRowCellValue("PARTYCODE", Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repTxtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEEDISPLAY);
                    FrmSearch.mColumnsToHide = "LEDGER_ID,DEPARTMENT_ID,MANAGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdData.SetFocusedRowCellValue("TOEMPLOYEENAME", Val.ToString(FrmSearch.mDRow["LEDGERNAME"]));
                        GrdData.SetFocusedRowCellValue("TOEMPLOYEECODE", Val.ToString(FrmSearch.mDRow["LEDGERCODE"]));
                        GrdData.SetFocusedRowCellValue("TOEMPLOYEE_ID", Val.ToString(FrmSearch.mDRow["LEDGER_ID"]));

                        GrdData.SetFocusedRowCellValue("TODEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
                        GrdData.SetFocusedRowCellValue("TODEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));

                        GrdData.SetFocusedRowCellValue("TOMANAGER_ID", Val.ToString(FrmSearch.mDRow["MANAGER_ID"]));
                        GrdData.SetFocusedRowCellValue("TOMANAGERNAME", Val.ToString(FrmSearch.mDRow["MANAGERNAME"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }


        public void FetchSummaty()
        {
            DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);
            DataTable DTabDistinct = DTab.DefaultView.ToTable(true, "KAPAN_ID", "KAPANNAME", "MANAGER_ID", "MAINMAINMANAGER");


            DTabSummary.Rows.Clear();

            foreach (DataRow DROW in DTabDistinct.Rows)
            {
                string StrKapan = Val.ToString(DROW["KAPANNAME"]);
                string StrEmp = Val.ToString(DROW["MAINMAINMANAGER"]);
                int StrKapan_ID = Val.ToInt32(DROW["KAPAN_ID"]);
                int StrManager_ID = Val.ToInt32(DROW["MANAGER_ID"]);

                int IntPcs = Val.ToInt(DTab.Compute("sum(BALANCEPCS)", "KAPANNAME = '" + StrKapan + "' AND MAINMAINMANAGER = '" + StrEmp + "'"));
                double DouCarat = Val.Val(DTab.Compute("sum(BALANCECARAT)", "KAPANNAME = '" + StrKapan + "' AND MAINMAINMANAGER = '" + StrEmp + "'"));

                DataRow DRNew = DTabSummary.NewRow();

                PolishIssueReturnProperty Property = new PolishIssueReturnProperty();

                Property.KAPAN_ID = StrKapan_ID;
                Property = ObjMast.FindNewPacketNoWithKapanForPolishOkTransfer(Property);

                if (Property.ReturnMessageType == "FAIL")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    DRNew["PACKETNO"] = 0;
                    return;
                }

                DRNew["KAPAN_ID"] = StrKapan_ID;
                DRNew["KAPANNAME"] = StrKapan;
                DRNew["MANAGER_ID"] = StrManager_ID;
                DRNew["MAINMAINMANAGER"] = StrEmp;
                DRNew["ORGPCS"] = IntPcs;
                DRNew["ORGCARAT"] = DouCarat;
                DRNew["BALANCEPCS"] = IntPcs;
                DRNew["BALANCECARAT"] = DouCarat;
                DRNew["PACKETNO"] = Property.RETURNVALUEMAXPACKETNO;

                DTabSummary.Rows.Add(DRNew);
                Property = null;


            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //  DTabYear.Rows.Add(DTabYear.NewRow());
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                if (txtTransferTo.Text.Trim().Length == 0)
                {
                    Global.Message("Employee Name Is Required");
                    txtTransferTo.Focus();
                    return;
                }

                if (txtManager.Text.Trim().Length == 0)
                {
                    Global.Message("Manager Name Is Required");
                    txtManager.Focus();
                    return;
                }

                if (txtDepartment.Text.Trim().Length == 0)
                {
                    Global.Message("Department Name Is Required");
                    txtDepartment.Focus();
                    return;
                }

                DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

                if (DTabSummary == null || Val.ToString(DTabSummary.Rows[0]["KAPANNAME"]) == string.Empty)
                {
                    Global.Message("Please Enter Data In Grid..");
                    this.Cursor = Cursors.Default;
                    return;
                }

                int IntCount = 1;
                foreach (DataRow DRow in DTabSummary.Rows)
                {
                    DRow["SRNO"] = IntCount;
                    IntCount = IntCount + 1;
                }
                DTabSummary.AcceptChanges();

                if (Global.Confirm("Are You Sure You Want To Save ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                PolishIssueReturnProperty Property = new PolishIssueReturnProperty();

                DTab.TableName = "Table";
                string StrXMLSelection = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    if (DTab != null)
                    {
                        DTab.WriteXml(sw);
                        StrXMLSelection = sw.ToString();
                    }
                }

                DTabSummary.TableName = "Table1";
                string StrXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    if (DTabSummary != null)
                    {
                        DTabSummary.WriteXml(sw);
                        StrXML = sw.ToString();
                    }
                }

                Property.TOEMPLOYEE_ID = Val.ToInt64(txtTransferTo.Tag);
                Property.TOMANAGER_ID = Val.ToInt64(txtManager.Tag);
                Property.TODEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);

                Property = ObjMast.SaveAndUpdate( StrXML, StrXMLSelection, Property);
                this.Cursor = Cursors.Default;

                ReturnMessageDesc = Property.ReturnMessageDesc;
                ReturnMessageType = Property.ReturnMessageType;

                Property = null;
                if (ReturnMessageType == "SUCCESS")
                {
                    Global.Message(ReturnMessageDesc);

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowForm("SinglePolishOKTransferPrint", DTabSummary);

                    ObjGridSelection.ClearSelection();
                    DTabSummary.Rows.Clear();
                    DTabSummary.Rows.Add(DTabSummary.NewRow());
                    DTabPolish.Rows.Clear();

                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            TxtMainManager.Clear();
            TxtMainManager.Tag = string.Empty;
            TxtEmployee.Clear();
            TxtEmployee.Tag = string.Empty;
            DTPFromDate.Value = DateTime.Now;
            DTPToDate.Value = DateTime.Now;
            DTPFromDate.Checked = false;
            DTPToDate.Checked = false;
            txtBarcode.Clear();
            txtBarcode.Tag = string.Empty;
            txtKapanName.Clear();
            txtKapanName.Tag = string.Empty;
            txtPacketNo.Clear();
            txtPacketNo.Tag = string.Empty;
            txtTag.Clear();
            txtTag.Tag = string.Empty;
            txtSrNoKapanName.Clear();
            txtSrNoKapanName.Tag = string.Empty;
            txtSrNoSerialNo.Clear();
            txtSrNoSerialNo.Tag = string.Empty;
            txtJangedNo.Clear();
            txtJangedNo.Tag = string.Empty;
            CmbKapan.SelectedIndex = -1;
            ObjGridSelection.ClearSelection();
            MainGridDetail.Refresh();
            MainGrid.DataSource = DTabSummary;
            MainGrid.Refresh();
            txtTransferTo.Text = String.Empty;
            txtTransferTo.Tag = String.Empty;
            txtManager.Text = String.Empty;
            txtManager.Tag = String.Empty;
            txtDepartment.Text = String.Empty;
            txtDepartment.Tag = String.Empty;
        }

        private void repTxtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.Transaction.BOTRN_SinglePacketCreate().FindKapan();
                    //  FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdData.SetFocusedRowCellValue("KAPANNAME", Val.ToString(FrmSearch.mDRow["KAPANNAME"]));
                        GrdData.SetFocusedRowCellValue("KAPAN_ID", Val.ToString(FrmSearch.mDRow["KAPAN_ID"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repTxtEmployeeCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO_NONMFG);
                    FrmSearch.mColumnsToHide = "LEDGER_ID,DEPARTMENT_ID,MANAGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdData.SetFocusedRowCellValue("TOEMPLOYEENAME", Val.ToString(FrmSearch.mDRow["LEDGERNAME"]));
                        GrdData.SetFocusedRowCellValue("TOEMPLOYEECODE", Val.ToString(FrmSearch.mDRow["LEDGERCODE"]));
                        GrdData.SetFocusedRowCellValue("TOEMPLOYEE_ID", Val.ToString(FrmSearch.mDRow["LEDGER_ID"]));

                        GrdData.SetFocusedRowCellValue("TODEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
                        GrdData.SetFocusedRowCellValue("TODEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));

                        GrdData.SetFocusedRowCellValue("TOMANAGER_ID", Val.ToString(FrmSearch.mDRow["MANAGER_ID"]));
                        GrdData.SetFocusedRowCellValue("TOMANAGERNAME", Val.ToString(FrmSearch.mDRow["MANAGERNAME"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repTxtCarat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdData.GetFocusedDataRow();
                    if (Val.Val(dr["CARAT"]) > 0)
                    {
                        DTabDataSum.Rows.Add(DTabDataSum.NewRow());
                    }
                    else if (GrdData.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }


        private void txtTag_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtKapanName.Text.Trim().Length == 0)
                {
                    return;
                }
                if (Val.ToInt(txtPacketNo.Text) == 0)
                {
                    txtKapanName.Focus();
                    return;
                }
                if (txtTag.Text.Trim().Length == 0)
                {
                    txtKapanName.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtKapanName.Text.Trim() == Val.ToString(DRow["KAPAN"]).Trim()
                        && txtPacketNo.Text.Trim() == Val.ToString(DRow["PACKETNO"]).Trim()
                        && txtTag.Text.Trim() == Val.ToString(DRow["TAG"]).Trim()
                        )
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();
                        //  CalculateSummary();
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {

                    string KapanName = "";
                    if (RbtPktSerialNo.Checked == true)
                    {
                        KapanName = txtSrNoKapanName.Text;
                    }
                    if (RbtPacketNo.Checked == true)
                    {
                        KapanName = txtKapanName.Text;
                    }


                    //BtnShow_Click(null, null);
                    DataRow DRow = ObjMast.GetDataForFilter(0, 0, KapanName, txtPacketNo.Text, txtTag.Text, 0, Employee_ID);

                    if (DRow == null)
                    {
                        Global.MessageError(" Packet Not In Stock Kindly Check");
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;
                        txtKapanName.Focus();
                        this.Cursor = Cursors.Default;
                        return;

                    }
                    else
                    {
                        IEnumerable<DataRow> rowsNew = DTabPolish.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtKapanName.Text = string.Empty;
                            txtPacketNo.Text = string.Empty;
                            txtTag.Text = string.Empty;
                            txtKapanName.Focus();
                            return;
                        }

                        DataRow DRNew = DTabPolish.NewRow();
                        foreach (DataColumn DCol in DTabPolish.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabPolish.Rows.Add(DRNew);


                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow Drow = GrdDet.GetDataRow(IntI);
                            if (txtKapanName.Text.Trim() == Val.ToString(Drow["KAPAN"]).Trim()
                                && txtPacketNo.Text.Trim() == Val.ToString(Drow["PACKETNO"]).Trim()
                                && txtTag.Text.Trim() == Val.ToString(Drow["TAG"]).Trim()
                                )
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabPolish.AcceptChanges();

                                break;
                            }
                        }
                        GrdDet.FocusedRowHandle = 0;
                    }
                    DRow = null;
                }
                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.FocusedRowHandle = 0;
                GrdDet.RefreshData();
                MainGridDetail.Refresh();
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                //BtnShow.Focus();
                txtKapanName.Focus();
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void txtBarcode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode.Text.Trim().Length == 0)
                {
                    return;
                }
                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtBarcode.Text.Trim() == Val.ToString(DRow["BARCODE"]).Trim())
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        //CalculateSummary();
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }


                if (ISFind == false)
                {
                    DataRow DRow = ObjMast.GetDataForFilter(Val.ToInt64(txtBarcode.Text), 0, "", "", "", 0, BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
                    if (DRow == null)
                    {
                        Global.MessageError(" Packet Not In Stock Kindly Check");
                        txtBarcode.Text = string.Empty;

                        txtBarcode.Focus();
                        this.Cursor = Cursors.Default;
                        return;

                    }
                    else
                    {
                        IEnumerable<DataRow> rowsNew = DTabPolish.Rows.Cast<DataRow>();
                        //if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        //{
                        //    this.Cursor = Cursors.Default;
                        //    Global.Message("This Packet Is Already Selected.");
                        //    txtBarcode.Text = string.Empty;

                        //    txtBarcode.Focus();
                        //    return;
                        //}

                        if (rowsNew.Where(s => Val.ToString(s["KAPANNAME"]) != Val.ToString(DRow["KAPANNAME"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Kapan Is Mismatch With Selected Kapan In Grid.");
                            txtBarcode.Text = string.Empty;

                            txtBarcode.Focus();
                            return;
                        }

                        DataRow DRNew = DTabPolish.NewRow();
                        foreach (DataColumn DCol in DTabPolish.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabPolish.Rows.Add(DRNew);

                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow Drow = GrdDet.GetDataRow(IntI);
                            if (txtBarcode.Text.Trim() == Val.ToString(Drow["BARCODE"]).Trim())
                            {
                                // ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                txtBarcode.Text = string.Empty;

                                //txtBarcode.Focus();
                                // GrdDet.FocusedRowHandle = 0;
                                break;
                            }
                        }
                        GrdDet.FocusedRowHandle = 0;
                    }
                    DRow = null;
                }

                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.FocusedRowHandle = 0;
                GrdDet.RefreshData();
                MainGridDetail.Refresh();
                FetchSummaty();
                // BtnShow.Focus();
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void txtSrNoSerialNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtSrNoKapanName.Text.Trim().Length == 0)
                {
                    return;
                }
                if (txtSrNoSerialNo.Text.Trim().Length == 0)
                {
                    return;
                }
                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtSrNoKapanName.Text.Trim() == Val.ToString(DRow["KAPAN"]).Trim()
                        && txtSrNoSerialNo.Text.Trim() == Val.ToString(DRow["SRNO"]).Trim())
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtSrNoKapanName.Text = string.Empty;
                        txtSrNoSerialNo.Text = string.Empty;

                        txtSrNoKapanName.Focus();
                        //  CalculateSummary();
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {
                    string KapanName = "";
                    if (RbtPktSerialNo.Checked == true)
                    {
                        KapanName = txtSrNoKapanName.Text;
                    }
                    if (RbtPacketNo.Checked == true)
                    {
                        KapanName = txtKapanName.Text;
                    }
                    DataRow DRow = ObjMast.GetDataForFilter(0, Val.ToInt32(txtSrNoSerialNo.Text), KapanName, "", "", 0, Employee_ID);
                    if (DRow == null)
                    {
                        Global.MessageError(" Packet Not In Stock Kindly Check");
                        txtSrNoKapanName.Text = string.Empty;
                        txtSrNoSerialNo.Text = string.Empty;

                        txtSrNoKapanName.Focus();
                        this.Cursor = Cursors.Default;
                        return;

                    }
                    else
                    {
                        IEnumerable<DataRow> rowsNew = DTabPolish.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtSrNoKapanName.Text = string.Empty;
                            txtSrNoSerialNo.Text = string.Empty;

                            txtSrNoKapanName.Focus();
                            return;
                        }

                        DataRow DRNew = DTabPolish.NewRow();
                        foreach (DataColumn DCol in DTabPolish.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabPolish.Rows.Add(DRNew);

                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow Drow = GrdDet.GetDataRow(IntI);
                            if (txtSrNoKapanName.Text.Trim() == Val.ToString(Drow["KAPAN"]).Trim()
                                && txtSrNoSerialNo.Text.Trim() == Val.ToString(Drow["SRNO"]).Trim())
                            {
                                // ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                txtSrNoKapanName.Text = string.Empty;
                                txtSrNoSerialNo.Text = string.Empty;

                                //  txtSrNoKapanName.Focus();

                                break;
                            }
                        }
                        DRow = null;
                    }
                    GrdDet.FocusedRowHandle = 0;
                }

                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.FocusedRowHandle = 0;
                GrdDet.RefreshData();
                MainGridDetail.Refresh();
                BtnShow.Focus();
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                txtSrNoKapanName.Focus();

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void txtJangedNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtJangedNo.Text.Trim().Length == 0)
                {
                    return;
                }
                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtJangedNo.Text.Trim() == Val.ToString(DRow["JANGEDNO"]).Trim())
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        //txtJangedNo.Text = string.Empty;
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();

                        GrdDet.FocusedRowHandle = 0;
                        //break;
                    }
                }

                if (ISFind == false)
                {

                    Int64 JangedNo = Val.ToInt64(txtJangedNo.Text);

                    //BtnShow_Click(null, null);
                    DataRow DRow = ObjMast.GetDataForFilter(0, 0, "", "", "", JangedNo, Employee_ID);

                    if (DRow == null)
                    {
                        Global.MessageError(" Packet Not In Stock Kindly Check");
                        txtJangedNo.Text = string.Empty;

                        txtJangedNo.Focus();
                        this.Cursor = Cursors.Default;
                        return;

                    }
                    else
                    {
                        IEnumerable<DataRow> rowsNew = DTabPolish.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtJangedNo.Text = string.Empty;

                            txtJangedNo.Focus();
                            return;
                        }

                        DataRow DRNew = DTabPolish.NewRow();
                        foreach (DataColumn DCol in DTabPolish.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabPolish.Rows.Add(DRNew);

                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow Drow = GrdDet.GetDataRow(IntI);
                            if (txtJangedNo.Text.Trim() == Val.ToString(Drow["JANGEDNO"]).Trim())
                            {
                                // ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                txtJangedNo.Text = string.Empty;

                                // txtJangedNo.Focus();

                                break;
                            }
                        }
                        DRow = null;
                    }
                    GrdDet.FocusedRowHandle = 0;
                }

                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.FocusedRowHandle = 0;
                GrdDet.RefreshData();
                MainGridDetail.Refresh();
                // BtnShow.Focus();
                txtJangedNo.Text = string.Empty;
                txtJangedNo.Focus();

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO_NONMFG);
                    FrmSearch.mStrColumnsToHide = "LEDGER_ID";
                    FrmSearch.ValueMemeter = "LEDGER_ID";
                    FrmSearch.DisplayMemeter = "LEDGERCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        TxtEmployee.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        TxtEmployee.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainGridDetail_KeyUp(object sender, KeyEventArgs e)
        {
            FetchSummaty();
        }

        private void MainGridDetail_MouseUp(object sender, MouseEventArgs e)
        {
            FetchSummaty();
        }

        private void GrdData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName.ToString().ToUpper())
                {
                    case "DIFFCARAT":
                        orgcarat = Val.Val(GrdData.GetRowCellValue(e.RowHandle, "ORGCARAT"));
                        losscarat = Val.Val(GrdData.GetRowCellValue(e.RowHandle, "DIFFCARAT"));
                        balcarat = Math.Round(orgcarat - losscarat, 3);
                        GrdData.SetRowCellValue(e.RowHandle, "BALANCECARAT", balcarat);

                        break;
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        #region : Other Operation

        private void txtTransferTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE, LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO);

                    FrmSearch.mColumnsToHide = "LEDGER_ID,DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtTransferTo.Text = Val.ToString(FrmSearch.mDRow["LEDGERCODE"]);
                        txtTransferTo.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);

                        txtDepartment.AccessibleDescription = Val.ToString(FrmSearch.mDRow["DEPARTMENTGROUP"]);

                        txtManager.Text = Val.ToString(FrmSearch.mDRow["MANAGERNAME"]);
                        txtManager.Tag = Val.ToString(FrmSearch.mDRow["MANAGER_ID"]);

                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        public void GenerateMaxPacketNoKapanWise()
        {
            foreach (DataRow Dr in DTabSummary.Rows)
            {
                PolishIssueReturnProperty Property = new PolishIssueReturnProperty();

                Property.KAPAN_ID = Val.ToInt64(Dr["KAPAN_ID"]);
                Property = ObjMast.FindNewPacketNoWithKapanForPolishOkTransfer(Property);

                if (Property.ReturnMessageType == "FAIL")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    GrdData.SetFocusedRowCellValue("PACKETNO", 0);
                    return;
                }
                GrdData.SetFocusedRowCellValue("PACKETNO", Property.RETURNVALUEMAXPACKETNO);
                Property = null;
            }

        }
        #endregion  

        #region Backgroud worker

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DTabPolish = ObjMast.PolishOKTransferGetData(Barcode, SrNo, KapanName, PktNo, Tag, JangedNo, FromDate, ToDate, Manager_ID, Employee_ID, pStrMainKapan);
            }
            catch (Exception Ex)
            {
                BtnShow.Enabled = true;
                PanelProgress.Visible = false;
                Global.Message(Ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BtnShow.Enabled = true;
                PanelProgress.Visible = false;
                if (DTabPolish.Rows.Count <= 0)
                {
                    Global.Message("No Data Found..");
                    return;
                }

                GrdDet.BeginUpdate();
                MainGridDetail.DataSource = DTabPolish;
                MainGridDetail.Refresh();
                GrdDet.BestFitColumns();
                GrdDet.EndUpdate();

            }
            catch (Exception Ex)
            {
                BtnShow.Enabled = true;
                PanelProgress.Visible = false;
                Global.Message(Ex.Message.ToString());
            }
        }

        #endregion  

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                if (Global.Confirm("Are You Sure To Slip Print?") == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DTabPrint = ObjMast.PolishOKTransferPrint(Barcode, SrNo, KapanName, PktNo, Tag, JangedNo, FromDate, ToDate, Manager_ID, Employee_ID, pStrMainKapan);

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowForm("SinglePolishOKTransferPrint", DTabPrint);

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }
        }

        private void MainGrid_Click(object sender, EventArgs e)
        {

        }
    }
}
