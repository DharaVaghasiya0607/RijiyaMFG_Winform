using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusLib.TableName;
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using AxoneMFGRJ.Report;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using BarcodeLib.Barcode;
using BusLib.Transaction;
using System.IO;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleTransactionUpdate : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BODevGridSelection ObjGridSelection;
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        DataTable DTabPacketLiveStock = new DataTable();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();
        DataTable DtabDateUpdate = new DataTable();

        string employee_ID = "";

        #region Property Setting

        public FrmSingleTransactionUpdate()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();


            GrdDet.BeginUpdate();
            DTabPacketLiveStock = ObjKapan.GetDataForSinglePacketLiveStock("NONE", "NONE", "NONE", 0, "", "", "", false, "", false, 0, "", "");
            MainGrid.DataSource = DTabPacketLiveStock;
            MainGrid.Refresh();

            if (MainGrid.RepositoryItems.Count == 13)
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

            this.Show();
        }

        private void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            //ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }


        private void Clear()
        {
            txtToEmp.Clear();
            txtToEmp.Tag = "";
            txtToDept.Clear();
            txtToDept.Tag = "";
            txtToManagr.Clear();
            txtToManagr.Tag = "";
            txtToProcess.Clear();
            txtToProcess.Tag = "";
            txtBarcode.Clear();
            txtSrNoKapanName.Clear();
            txtSrNoSerialNo.Clear();
            txtKapanName.Clear();
            txtPacketNo.Clear();
            txtTag.Clear();
            txtJangedNoFilter.Clear();
            CmbEntryType.SelectedItem = 0;
            DTabPacketLiveStock = ObjKapan.GetDataForSinglePacketLiveStock("NONE", "NONE", "NONE", 0, "", "", "", false, "", false, 0, "", "");
            MainGrid.DataSource = DTabPacketLiveStock;
            MainGrid.Refresh();
        }


#endregion

        #region Validation

        private void RbtBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtBarcode.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNoFilter.Text = string.Empty;
                txtBarcode.Focus();
            }
            else if (RbtJangedNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtJangedNoFilter.Focus();
            }
            else if (RbtPacketNo.Checked)
            {
                txtJangedNoFilter.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtKapanName.Focus();
            }
            else if (RbtPktSerialNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNoFilter.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Focus();
            }
            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelJangedNo.Visible = RbtJangedNo.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;
        }

        private void CmbEntryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (DataRow DRow in DTabPacketLiveStock.Rows)
            //{
            //    DRow["ENTRYTYPE"] = Val.ToString(CmbEntryType.SelectedItem);                                   
            //}
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

                //string StrType = string.Empty;
                //if (RbtDeptStock.Checked == true)
                //{
                //    StrType = RbtDeptStock.Tag.ToString();
                //}
                //else if (RbtFullStock.Checked == true)
                //{
                //    StrType = RbtFullStock.Tag.ToString();
                //}
                //else if (RbtMYStock.Checked == true)
                //{
                //    StrType = RbtMYStock.Tag.ToString();
                //}
                //else if (RbtOtherStock.Checked == true)
                //{
                //    StrType = RbtOtherStock.Tag.ToString();
                //}

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtBarcode.Text.Trim() == Val.ToString(DRow["BARCODE"]).Trim())
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();
                        CalculateSummary();
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataRow DRow = ObjKapan.GetDataForSinglePacketLiveStockPacketInfo("ALL", "FULLSTOCK", "", 0, "", txtBarcode.Text, 0, "", 0, 0);
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
                        IEnumerable<DataRow> rowsNew = DTabPacketLiveStock.Rows.Cast<DataRow>();
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

                        DataRow DRNew = DTabPacketLiveStock.NewRow();
                        foreach (DataColumn DCol in DTabPacketLiveStock.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabPacketLiveStock.Rows.Add(DRNew);


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
                                DTabPacketLiveStock.AcceptChanges();
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

        private void CalculateSummary()
        {
            //int IntPcs = 0;
            //double DouCarat = 0;
            //int IntSelPcs = 0;
            //double DouSelCarat = 0;

            //for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            //{
            //    DataRow DRow = GrdDet.GetDataRow(IntI);
            //    IntPcs = IntPcs + 1;
            //    DouCarat = DouCarat + Val.Val(DRow["BALANCECARAT"]);
            //}

            if (ObjGridSelection != null)
            {
                DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);
                if (DTab == null)
                {
                    return;

                }
            }
            //    foreach (DataRow DRow in DTab.Rows)
            //    {
            //        IntSelPcs = IntSelPcs + 1;
            //        DouSelCarat = DouSelCarat + Val.Val(DRow["BALANCECARAT"]);
            //    }
            //}

            //txtTotalPcs.Text = IntPcs.ToString();
            //txtTotalCarat.Text = DouCarat.ToString();
            //txtSelectedPcs.Text = IntSelPcs.ToString();
            //txtSelectedCarat.Text = DouSelCarat.ToString();
        }

        private void txtSrNoSerialNo_Validated(object sender, EventArgs e)
        {
            try
            {

                if (txtSrNoKapanName.Text.Trim().Length == 0)
                {
                    return;
                }
                if (Val.ToInt(txtSrNoSerialNo.Text) == 0)
                {
                    txtSrNoSerialNo.Focus();
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                //string StrType = string.Empty;
                //if (RbtDeptStock.Checked == true)
                //{
                //    StrType = RbtDeptStock.Tag.ToString();
                //}
                //else if (RbtFullStock.Checked == true)
                //{
                //    StrType = RbtFullStock.Tag.ToString();
                //}
                //else if (RbtMYStock.Checked == true)
                //{
                //    StrType = RbtMYStock.Tag.ToString();
                //}
                //else if (RbtOtherStock.Checked == true)
                //{
                //    StrType = RbtOtherStock.Tag.ToString();
                //}

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
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataRow DRow = ObjKapan.GetDataForSinglePacketLiveStockPacketInfo("ALL", "FULLSTOCK", txtSrNoKapanName.Text, 0, "", "", Val.ToInt32(txtSrNoSerialNo.Text), "", 0, 0);
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;

                        DataRow DRowOS = ObjKapan.GetDataForSinglePacketLiveStockCurrentOutstanding(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, 0);
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
                        Global.MessageError(txtSrNoKapanName.Text + "-" + txtSrNoSerialNo.Text + " Packet Not In Stock Kindly Check\n\n" + StrMsg);
                        txtSrNoKapanName.Text = string.Empty;
                        txtSrNoSerialNo.Text = string.Empty;

                        txtSrNoKapanName.Focus();
                        return;
                    }
                    else
                    {

                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        IEnumerable<DataRow> rowsNew = DTabPacketLiveStock.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtKapanName.Text = string.Empty;
                            txtPacketNo.Text = string.Empty;
                            txtTag.Text = string.Empty;
                            txtSrNoKapanName.Focus();
                            return;
                        }
                        // 07-06-2019

                        DataRow DRNew = DTabPacketLiveStock.NewRow();
                        foreach (DataColumn DCol in DTabPacketLiveStock.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabPacketLiveStock.Rows.Add(DRNew);


                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDet.GetDataRow(IntI);
                            if (txtSrNoKapanName.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                                && txtSrNoSerialNo.Text.Trim() == Val.ToString(DRowGrid["PKTSERIALNO"]).Trim()
                                )
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabPacketLiveStock.AcceptChanges();
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
                //GrdDet.BestFitMaxRowCount = 500;
                //GrdDet.BestFitColumns();
                MainGrid.Refresh();

                CalculateSummary();

                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;

                txtSrNoKapanName.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtSrNoKapanName.Focus();
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

                if (Val.ISNumeric(txtTag.Text) == true)
                {
                    Char c = (Char)(64 + Val.ToInt(txtTag.Text));
                    txtTag.Text = c.ToString();
                }

                this.Cursor = Cursors.WaitCursor;

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
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
                        CalculateSummary();
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataRow DRow = ObjKapan.GetDataForSinglePacketLiveStockPacketInfo("ALL", "FULLSTOCK", txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, "", 0, "", Val.ToInt(txtPacketNo.Text), 0);
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;

                        DataRow DRowOS = ObjKapan.GetDataForSinglePacketLiveStockCurrentOutstanding(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, 0);
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
                        //Check That Packet Already Exists In Grid then Skip -                                              

                        IEnumerable<DataRow> rowsNew = DTabPacketLiveStock.Rows.Cast<DataRow>();
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

                        DataRow DRNew = DTabPacketLiveStock.NewRow();
                        foreach (DataColumn DCol in DTabPacketLiveStock.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabPacketLiveStock.Rows.Add(DRNew);


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
                                DTabPacketLiveStock.AcceptChanges();
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
                //GrdDet.BestFitMaxRowCount = 500;
                //GrdDet.BestFitColumns();
                MainGrid.Refresh();

                CalculateSummary();

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;

                txtKapanName.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtKapanName.Focus();
            }
        }

        private void txtJangedNoFilter_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtPassForDateUpdate.Text != "")
                {
                    txtPassForDateUpdate_TextChanged(null, null);
                }

                else
                {
                    if (txtJangedNoFilter.Text.Trim().Length == 0)
                    {
                        return;
                    }

                    {
                        this.Cursor = Cursors.WaitCursor;

                        bool ISFind = false;
                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRow = GrdDet.GetDataRow(IntI);
                            if (txtJangedNoFilter.Text.Trim() == Val.ToString(DRow["JANGEDNO"]).Trim())
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                txtKapanName.Text = string.Empty;
                                txtPacketNo.Text = string.Empty;
                                txtTag.Text = string.Empty;

                                txtKapanName.Focus();
                                //CalculateSummary();
                                GrdDet.FocusedRowHandle = 0;
                                //break;
                            }
                        }

                        if (ISFind == false)
                        {
                            DataTable DTabJangedData = ObjKapan.GetDataForSinglePacketLiveStockJangednoWiseInfo("ALL", "FULLSTOCK", "", 0, "", "", 0, txtJangedNoFilter.Text, "");
                            if (DTabJangedData == null || DTabJangedData.Rows.Count <= 0)
                            {
                                this.Cursor = Cursors.Default;

                                DataRow DRowOS = ObjKapan.GetDataForSinglePacketLiveStockCurrentOutstanding(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, 0);
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

                                ////Check That Packet Already Exists In Grid then Skip - 07-06-2019
                                //IEnumerable<DataRow> rowsNew = DTabPacketLiveStock.Rows.Cast<DataRow>();
                                //if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                                //{
                                //    this.Cursor = Cursors.Default;
                                //    Global.Message("This Packet Is Already Selected.");
                                //    txtKapanName.Text = string.Empty;
                                //    txtPacketNo.Text = string.Empty;
                                //    txtTag.Text = string.Empty;
                                //    txtKapanName.Focus();
                                //    return;
                                //}

                                var matched = from table1 in DTabPacketLiveStock.AsEnumerable()
                                              join table2 in DTabJangedData.AsEnumerable() on table1.Field<Int64>("JANGEDNO") equals table2.Field<Int64>("JANGEDNO")
                                              where table1.Field<Int64>("PACKET_ID") == table2.Field<Int64>("PACKET_ID")
                                              select table1;

                                if (matched.Any())
                                {
                                    this.Cursor = Cursors.Default;
                                    string Str = matched.FirstOrDefault()["KAPANNAME"].ToString() + "-" + matched.FirstOrDefault()["PACKETNO"].ToString() + matched.FirstOrDefault()["TAG"].ToString();
                                    Global.Message("This PacketNo Is Already Selected." + Str);
                                    txtKapanName.Text = string.Empty;
                                    txtPacketNo.Text = string.Empty;
                                    txtTag.Text = string.Empty;
                                    txtKapanName.Focus();
                                    return;
                                }

                                // 07-06-2019

                                foreach (DataRow DR in DTabJangedData.Rows)
                                {
                                    DataRow DRNew = DTabPacketLiveStock.NewRow();
                                    foreach (DataColumn DCol in DTabPacketLiveStock.Columns)
                                    {
                                        DRNew[DCol.ColumnName] = DR[DCol.ColumnName];
                                    }
                                    DTabPacketLiveStock.Rows.Add(DRNew);
                                }

                                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                                {
                                    DataRow DRowGrid = GrdDet.GetDataRow(IntI);
                                    //if (txtKapanName.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                                    //    && txtPacketNo.Text.Trim() == Val.ToString(DRowGrid["PACKETNO"]).Trim()
                                    //    && txtTag.Text.Trim() == Val.ToString(DRowGrid["TAG"]).Trim()
                                    //    )
                                    if (txtJangedNoFilter.Text.Trim() == Val.ToString(DRowGrid["JANGEDNO"]).Trim())
                                    {
                                        ISFind = true;
                                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                        DTabPacketLiveStock.AcceptChanges();
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

                        txtJangedNoFilter.Text = string.Empty;
                        txtJangedNoFilter.Focus();

                        this.Cursor = Cursors.Default;
                    }
                }
            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtBarcode.Focus();
            }
        }

        private void txtToEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE, LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    //if (mStrParentFormType == "ROUGH")
                    //{
                    //    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO_NONMFG);
                    //}
                    //else
                    //{
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO);
                    //}
                    FrmSearch.mColumnsToHide = "LEDGER_ID,DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtToEmp.Text = Val.ToString(FrmSearch.mDRow["LEDGERCODE"]);
                        txtToEmp.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                        // txtType.Text = Val.ToString(FrmSearch.mDRow["LEDGERGROUP"]);
                        txtToDept.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtToDept.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);

                        txtToDept.AccessibleDescription = Val.ToString(FrmSearch.mDRow["DEPARTMENTGROUP"]);

                        txtToManagr.Text = Val.ToString(FrmSearch.mDRow["MANAGERNAME"]);
                        txtToManagr.Tag = Val.ToString(FrmSearch.mDRow["MANAGER_ID"]);
                        //mBoolAutoConfirm = Val.ToBoolean(FrmSearch.mDRow["AUTOCONFIRM"]);

                        //#P : 08-10-2019
                        //foreach (DataRow DRow in DTabPacketLiveStock.Rows)
                        //{
                        //    DRow["TOEMPLOYEE_ID"] = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                        //    DRow["TOEMPLOYEECODE"] = Val.ToString(FrmSearch.mDRow["LEDGERCODE"]);

                        //    DRow["TODEPARTMENT_ID"] = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
                        //    DRow["TODEPARTMENTNAME"] = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);

                        //    DRow["TOMANAGER_ID"] = Val.ToString(FrmSearch.mDRow["MANAGER_ID"]);
                        //    DRow["TOMANAGERNAME"] = Val.ToString(FrmSearch.mDRow["MANAGERNAME"]);
                        //}
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

        private void txtToProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID,PROCESSGROUP";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtToProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtToProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                    }

                    //foreach (DataRow DRow in DTabPacketLiveStock.Rows)
                    // {
                    //    if (DRow != null)
                    //    {
                    //        DRow["TOPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                    //        DRow["TOPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                    //    }
                    //    //DRow["NEXTPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                    //    //DRow["NEXTPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                    // }
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

        #endregion 

        #region Control Event

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //string ReturnMessageDesc = "";
                //string ReturnMessageType = "";

                if (Global.Confirm("Are You Sure You Want To Update ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                if ((txtToEmp.Text == "") && (txtToProcess.Text == "") && (CmbEntryType.SelectedItem == null))
                {
                    Global.Message("Please Select Any One Data ...");
                    return;
                }

                foreach (DataRow DRow in DTabPacketLiveStock.Rows)
                {
                    if (CmbEntryType.SelectedItem == "EMPISS" || CmbEntryType.SelectedItem == "TRANSFER")
                    {
                        if (Val.ToString(DRow["ENTRYTYPE"]) == "EMPRET")
                        {
                            Global.Message("Please Select Valid Entry Type...");
                            return;
                        }
                    }
                    if (Val.ToString(DRow["FROMEMPLOYEECODE"]) != "")
                    {
                        if (txtToEmp.Text == Val.ToString(DRow["FROMEMPLOYEECODE"]))
                        {
                            Global.Message("FromEmployee And ToEmployee IS Same In Packet : [" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "] Please Check...");
                            return;
                        }
                    }
                }

                this.Cursor = Cursors.WaitCursor;

                string StrXml;
                DTabPacketLiveStock.TableName = "Table1";
                using (StringWriter sw = new StringWriter())
                {
                    DTabPacketLiveStock.WriteXml(sw);
                    StrXml = sw.ToString();
                }

                string StrRes = ObjTrn.SingleTransactionUpdate(StrXml, Val.ToInt32(txtToEmp.Tag), Val.ToInt32(txtToDept.Tag), Val.ToInt32(txtToManagr.Tag), Val.ToInt32(txtToProcess.Tag), Val.ToString(CmbEntryType.SelectedItem));

                if (StrRes == "SUCCESS")
                {
                    Global.Message("Record Update Success..");
                    Clear();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }
        }

       
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("SingleTransactionUpdate", GrdDet);
        }

        private void deleteSelectedItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
          try
          {
             if (GrdDet.FocusedRowHandle >= 0)
             {
                if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                {
                    DTabPacketLiveStock.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                    DTabPacketLiveStock.AcceptChanges();
                    Global.Message("Deleted Successfully...");
                    //BtnSave_Click(null, null);
                }
             }
          }
          catch (Exception ex)
          {
              Global.Message(ex.Message);
          }
        }

        private void BtnDateUpdate_Click(object sender, EventArgs e)
        {
           try
           {
               if (RbtJangedNo.Checked)
               {
                  // Int64 jangedno = Val.ToInt64(txtJangedNoFilter.Text);
                   DtabDateUpdate = ObjKapan.DateUpate(Val.ToInt64(txtJangedNoFilter.Text), Val.ToString(DTPTransDate.Text));
                   Global.Message("Updated Successfully..");
               }
           }
           catch (Exception ex)
           {
               this.Cursor = Cursors.Default;
               Global.Message(ex.Message);
           }
        }

        private void txtPassForDateUpdate_TextChanged(object sender, EventArgs e)
        {
          try
           {
              if (Val.ToString(txtPassForDateUpdate.Tag) != "" && Val.ToString(txtPassForDateUpdate.Tag).ToUpper() == txtPassForDateUpdate.Text.ToUpper())
              {
                  BtnDateUpdate.Visible = true;
              }
              else
              {
                  BtnDateUpdate.Visible = false;
              }
           }
           catch (Exception ex)
          {
                Global.MessageError(ex.Message.ToString());
          }
        }

        #endregion
    }
 }
