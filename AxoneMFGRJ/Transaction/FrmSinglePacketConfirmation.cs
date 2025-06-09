using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
using BusLib.Transaction;
using DevExpress.XtraPrinting;
using Google.API.Translate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusLib.TableName;
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using AxoneMFGRJ.Report;
using CrystalDecisions.CrystalReports.Engine;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSinglePacketConfirmation : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        BODevGridSelection ObjGridSelSummary;

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        DataTable DTabPacket = new DataTable();

        #region Property Settings

        public FrmSinglePacketConfirmation()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
            DTPConfirmDate.Value = DateTime.Now;
            BtnSearch.PerformClick();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion


        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }
            else if (e.KeyCode == Keys.F5)
            {
                BtnSearch.PerformClick();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Double pDouFromCarat = 0;
                Double pDouToCarat = 0;
                pDouFromCarat = Val.Val(txtFromCarat.Text);
                pDouToCarat = Val.Val(txtToCarat.Text);
                DataSet DS = ObjTrn.GetPendingConfirmationData(pDouFromCarat, pDouToCarat);

                GrdDet.BeginUpdate();
                MainGrid.DataSource = DS.Tables[0];
                GrdDet.RefreshData();

                //MainGridSummary.DataSource = DS.Tables[1];
                //GrdDetSummary.RefreshData();

                if (MainGrid.RepositoryItems.Count == 0)
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

                CalculateSummary();

                GrdDet.EndUpdate();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }

        }

        public void CalculateSummary()
        {
            int IntPcs = 0;
            double DouCarat = 0;
            int IntSelPcs = 0;
            double DouSelCarat = 0;

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntPcs = IntPcs + 1;
                DouCarat = DouCarat + Val.Val(DRow["ISSUECARAT"]);
            }

            DataTable DTab = GetTableOfSelectedRows(GrdDet, true, ObjGridSelection);
            if (DTab == null)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                IntSelPcs = IntSelPcs + 1;
                DouSelCarat = DouSelCarat + Val.Val(DRow["ISSUECARAT"]);
            }

            txtTotalPcs.Text = IntPcs.ToString();
            txtTotalCarat.Text = DouCarat.ToString();
            txtSelectedPcs.Text = IntSelPcs.ToString();
            txtSelectedCarat.Text = DouSelCarat.ToString();

            //txtCarat.Text = Math.Round(DouCarat, 3).ToString();
            //txtTrfCarat.Text = Math.Round(DouCarat, 3).ToString();
            //txtAmount.Text = Math.Round(DouAmount, 3).ToString();
            //double DouRate = DouCarat != 0 ? Math.Round(DouAmount / DouCarat, 3) : 0;
            //txtRate.Text = Math.Round(DouRate, 3).ToString();
            //txtTransferRate.Text = Math.Round(DouRate, 3).ToString();
        }
        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
        }


        private DataTable GetTableOfSelectedRows(GridView pBoolView, Boolean pBoolIsSelect, BODevGridSelection pSelection)
        {
            if (pBoolView.RowCount <= 0)
            {
                return null;
            }
            ArrayList aryLst = new ArrayList();

            DataTable resultTable = new DataTable();
            DataTable sourceTable = null;
            sourceTable = ((DataView)pBoolView.DataSource).Table;

            if (pBoolIsSelect)
            {
                aryLst = pSelection.GetSelectedArrayList();
                resultTable = sourceTable.Clone();
                for (int i = 0; i < aryLst.Count; i++)
                {
                    DataRowView oDataRowView = aryLst[i] as DataRowView;
                    resultTable.Rows.Add(oDataRowView.Row.ItemArray);
                }
            }

            return resultTable;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDet, true, ObjGridSelection);
            if (DTab == null || DTab.Rows.Count == 0)
            {
                Global.Message("Please Select Atleast One Stone For Update");
                return;
            }
            if (Global.Confirm("Are You Sure To Confirm ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            // DataTable DTab = GetTableOfSelectedRows(GrdDet, true, ObjGridSelection);

            this.Cursor = Cursors.WaitCursor;
            int IntRes = 0;
            foreach (DataRow DRow in DTab.Rows)
            {
                TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                Property.CONFDATE = Val.SqlDate(DTPConfirmDate.Value.ToShortDateString());
                Property.PACKET_ID = Val.ToInt64(Val.ToString(DRow["PACKET_ID"]));
                Property.JANGEDNO = Val.ToInt64(DRow["JANGEDNO"]);

                Property = ObjTrn.ConfirmJanged("PACKET", Property);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    IntRes++;
                }
                Property = null;
            }
            this.Cursor = Cursors.Default;

            if (IntRes != 0)
            {
                Global.Message("Successfully Confirmed");
                BtnSearch.PerformClick();
            }
            DTab = null;

        }

        private void BtnConfirmJanged_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("Are You Sure To Confirm ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            DataTable DTab = GetTableOfSelectedRows(GrdDetSummary, true, ObjGridSelSummary);

            this.Cursor = Cursors.WaitCursor;
            int IntRes = 0;
            foreach (DataRow DRow in DTab.Rows)
            {
                TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                Property.CONFDATE = Val.SqlDate(DTPConfirmDate.Value.ToShortDateString());
                Property.JANGEDNO = Val.ToInt64(DRow["JANGEDNO"]);

                Property = ObjTrn.ConfirmJanged("JANGED", Property);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    IntRes++;
                }
                Property = null;
            }
            this.Cursor = Cursors.Default;

            if (IntRes != 0)
            {
                Global.Message("Successfully Confirmed");
                BtnSearch.PerformClick();
            }
            DTab = null;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            bool ISFind = false;
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                if (txtJangedNo.Text.Trim() == Val.ToString(DRow["JANGEDNO"]).Trim()
                    )
                {
                    ISFind = true;
                    GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                    txtJangedNo.Text = string.Empty;

                    txtJangedNo.Focus();
                    CalculateSummary();
                    break;
                }
            }

            if (ISFind == false)
            {
                Global.MessageError(txtJangedNo.Text + " Packet Not In Stock Kindly Check");
                txtJangedNo.Text = string.Empty;

            }
            else
            {
                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.RefreshData();
            }
        }

        private void txtSrNoSerialNo_Validated(object sender, EventArgs e)
        {
            bool ISFind = false;
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                if (txtSrNoKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                    && txtSrNoSerialNo.Text.Trim() == Val.ToString(DRow["PKTSERIALNO"]).Trim()
                    )
                {
                    ISFind = true;
                    GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                    txtSrNoKapanName.Text = string.Empty;
                    txtSrNoSerialNo.Text = string.Empty;

                    txtSrNoKapanName.Focus();
                    CalculateSummary();
                    break;
                }
            }

            if (ISFind == false)
            {
                Global.MessageError(txtSrNoKapanName.Text + "-" + txtSrNoSerialNo.Text + " Packet Not In Stock Kindly Check");
                txtSrNoKapanName.Text = string.Empty;
            }
            else
            {
                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.RefreshData();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt32(TxtLimit.Text) <= 0)
                {
                    Global.Message("Please Enter Limit..");
                    TxtLimit.Focus();
                    return;
                }

                DataTable DTab = GetTableOfSelectedRows(GrdDet, true, ObjGridSelection);
                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                if (Val.ToInt32(TxtLimit.Text) > DTab.Rows.Count)
                {
                    Global.Message("Please Enter Valid Limit..");
                    TxtLimit.Focus();
                    return;
                }
                if (Global.Confirm("Are You Sure To Update ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }


                int limit = Val.ToInt32(TxtLimit.Text);
                int jangedno = 0;

                int MaxJanged = 0;
                int A = 1;

                for (int i = 0; i < DTab.Rows.Count; i++)
                {
                    A = A + i;

                    if (i % limit == 0)
                    {
                        MaxJanged = ObjTrn.GetMaxDeptJangedNoStr();
                        jangedno = MaxJanged;
                        DTab.Rows[i]["DEPTJANGEDNO"] = jangedno;
                        MaxJanged = jangedno;
                    }
                    else
                    {
                        DTab.Rows[i]["DEPTJANGEDNO"] = jangedno;
                    }
                }

                DTab.TableName = "NewJangedNo";

                string NewJangedXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTab.WriteXml(sw);
                    NewJangedXml = sw.ToString();
                }

                string pStrReturnMessageDesc = ObjTrn.SaveNewDeptJangedNoStr(NewJangedXml, Val.SqlDate(DTPConfirmDate.Value.ToShortDateString()));
                if (pStrReturnMessageDesc == "SUCCESS")
                {
                    Global.Message("DeptJanged No Created Sucessfully");
                    BtnSearch_Click(null, null);
                }
                else
                {
                    Global.Message(pStrReturnMessageDesc);
                    //Global.Message("Opps...Something Wrong, Please Check...!");
                    return;
                }

                string StrJanghedNo = "";
                for (int i = 0; i < DTab.Rows.Count; i++)
                {
                    StrJanghedNo = StrJanghedNo + Val.ToString(DTab.Rows[i]["DEPTJANGEDNO"]) + ",";
                }
                if (StrJanghedNo.Length != 0)
                {
                    StrJanghedNo = StrJanghedNo.Substring(0, StrJanghedNo.Length - 1);
                }
                TxtDeptJangedNo.Text = StrJanghedNo;
                btnSlipPrint_Click(null, null);

            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
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


                this.Cursor = Cursors.WaitCursor;

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
                        CalculateSummary();
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataRow DRow = ObjKapan.GetDataForSinglePacketLiveStockPacketInfo("", "", "", 0, "", txtBarcode.Text, 0, "", 0, 0);
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;

                        DataRow DRowOS = ObjKapan.GetDataForSinglePacketLiveStockCurrentOutstanding(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, Val.ToInt64(txtBarcode.Text));
                        string StrMsg = string.Empty;
                        if (DRowOS != null)
                        {
                            StrMsg = StrMsg + "Packet : " + Val.ToString(DRowOS["PACKETTAG"]) + "\n\n";
                            StrMsg = StrMsg + "EntryType : " + Val.ToString(DRowOS["CURR_ENTRYTYPE"]) + "\n\n";
                            StrMsg = StrMsg + "Employee : " + Val.ToString(DRowOS["TOEMPLOYEENAME"]) + "\n\n";
                            StrMsg = StrMsg + "Department : " + Val.ToString(DRowOS["TODEPARTMENTNAME"]) + "\n\n";
                            StrMsg = StrMsg + "For Process : " + Val.ToString(DRowOS["TOPROCESSNAME"]) + "\n\n";
                            StrMsg = StrMsg + "Manager : " + Val.ToString(DRowOS["TOMANAGERNAME"]) + "\n\n";
                        }

                        DRowOS = null;

                        Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check\n\n" + StrMsg);
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();
                        return;
                    }
                    else
                    {

                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        IEnumerable<DataRow> rowsNew = DTabPacket.Rows.Cast<DataRow>();
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
                        // 07-06-2019

                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDet.GetDataRow(IntI);
                            if (txtKapanName.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                                && txtPacketNo.Text.Trim() == Val.ToString(DRowGrid["PACKETNO"]).Trim()
                                && txtTag.Text.Trim() == Val.ToString(DRowGrid["TAG"]).Trim()
                                )
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabPacket.AcceptChanges();
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
                MainGrid.Refresh();

                CalculateSummary();

                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtBarcode.Focus();
            }
        }

        private void RbtBarcode_CheckedChanged(object sender, EventArgs e)
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
                txtBarcode.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtSrNoKapanName.Focus();
            }

            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelJangedNo.Visible = RbtJangedNo.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;
        }

        private void txtJangedNo_Validated(object sender, EventArgs e)
        {
            try
            {

                if (txtJangedNo.Text.Trim().Length == 0)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                
                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtJangedNo.Text.Trim() == Val.ToString(DRow["JANGEDNO"]).Trim())
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();
                        CalculateSummary();
                        GrdDet.FocusedRowHandle = 0;
                        //break;
                    }
                }

                if (ISFind == false)
                {
                    DataTable DTabJangedData = ObjTrn.GetPendingConfirmationDataWithScan(Val.ToInt64(txtJangedNo.Text));
                    if (DTabJangedData == null || DTabJangedData.Rows.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;

                        Global.MessageError("JangedNo : " + Val.ToString(txtJangedNo.Text) + " Is Not In Your Stock Please Check..");
                        txtJangedNo.Text = string.Empty;
                        txtJangedNo.Focus();
                        return;
                    }
                    else
                    {

                        var matched = from table1 in DTabPacket.AsEnumerable()
                                      join table2 in DTabJangedData.AsEnumerable() on table1.Field<Int64>("JANGEDNO") equals table2.Field<Int64>("JANGEDNO")
                                      where table1.Field<Int64>("PACKET_ID") == table2.Field<Int64>("PACKET_ID")
                                      select table1;

                        if (matched.Any())
                        {
                            this.Cursor = Cursors.Default;
                            string Str = matched.FirstOrDefault()["JANGEDNO"].ToString();
                            Global.Message("This JangedNo Is Already Selected." + Str);
                            txtJangedNo.Text = string.Empty;
                            txtJangedNo.Focus();
                            return;
                        }

                        // 07-06-2019

                        foreach (DataRow DR in DTabJangedData.Rows)
                        {
                            DataRow DRNew = DTabPacket.NewRow();
                            foreach (DataColumn DCol in DTabPacket.Columns)
                            {
                                DRNew[DCol.ColumnName] = DR[DCol.ColumnName];
                            }
                            DTabPacket.Rows.Add(DRNew);
                        }


                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDet.GetDataRow(IntI);
                            //if (txtKapanName.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                            //    && txtPacketNo.Text.Trim() == Val.ToString(DRowGrid["PACKETNO"]).Trim()
                            //    && txtTag.Text.Trim() == Val.ToString(DRowGrid["TAG"]).Trim()
                            //    )
                            if (txtJangedNo.Text.Trim() == Val.ToString(DRowGrid["JANGEDNO"]).Trim())
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabPacket.AcceptChanges();
                                //break;
                            }
                        }
                        GrdDet.FocusedRowHandle = 0;
                    }
                    DTabJangedData = null;
                }

                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.FocusedRowHandle = 0;
                GrdDet.RefreshData();
                MainGrid.Refresh();

                CalculateSummary();

                txtJangedNo.Text = string.Empty;
                txtJangedNo.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtBarcode.Focus();
            }
        }

        

      

        private void btnSlipPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string pStrJangedNo = "";

                if (TxtDeptJangedNo.Text == string.Empty)
                {
                    Global.Message("Please Select Slip No First..");
                    TxtDeptJangedNo.Focus();
                    return;
                }
                pStrJangedNo = Val.ToString(TxtDeptJangedNo.Text);
                DataTable DTab = ObjTrn.PopupDeptJangedMultiForPrint(pStrJangedNo, Val.SqlDate(DtpJangedNo.Value.ToShortDateString()), "");

                if (DTab.Rows.Count == 0)
                {
                    Global.MessageError("There Is No Data For Print");
                    return;
                }

                DataTable DTabDistinct = DTab.DefaultView.ToTable(true, "JANGEDNO");
                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowWithPrint("DeptJangedNoDetail", DTab);

                //foreach (DataRow DrDistinct in DTabDistinct.Rows)
                //{
                //    DataRow[] DRFinalData = DTab.Select("JANGEDNO = '" + DrDistinct["JANGEDNO"].ToString() + "'");

                //    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                //    FrmReportViewer.MdiParent = Global.gMainRef;
                //    FrmReportViewer.ShowWithPrint("DeptJangedNoDetail", DRFinalData.CopyToDataTable());
                //}
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        //public void DeptJangedPrint(DataTable DtabDistinctJangedNo)
        //{
        //    List<Crystal> rps = new List<CrystalDecisions>();

        //    ReportDocument RepDoc = new ReportDocument();


        //}

        private void TxtDeptJangedNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    string StrDeptJangedDate = DtpJangedNo.Checked ? DtpJangedNo.Value.ToShortDateString() : null;
                    FrmSearch.mDTab = ObjTrn.PopupDeptJangedMultiForPrint("", Val.SqlDate(StrDeptJangedDate), "POPUP");
                    //FrmSearch.mDTab = ObjTrn.PopupDeptJangedMultiForPrint("", Val.SqlDate(DtpJangedNo.Value.ToShortDateString()), "POPUP");
                    FrmSearch.mStrColumnsToHide = "";
                    FrmSearch.ValueMemeter = "DEPTJANGEDDATE";
                    FrmSearch.DisplayMemeter = "DEPTJANGEDNO";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedValuemember != "")
                    {
                        TxtDeptJangedNo.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        // TxtDeptJangedNo.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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
    }
}
