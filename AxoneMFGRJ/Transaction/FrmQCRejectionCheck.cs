using AxoneMFGRJ.Utility;
using BusLib.Configuration;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections;
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
    public partial class FrmQCRejectionCheck : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        BODevGridSelection ObjGridSelection;
        string StrFromDate = "", StrToDate = "";
        string StrStatus = "";
        DataTable DTabDetail = new DataTable();
        DataTable DTab = new DataTable();
        string StrOpe = "";
        DataTable DTabQC = new DataTable();

        public FrmQCRejectionCheck()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            GrdDetDetail.BeginUpdate();
            DTabDetail = Obj.GetRejectionData("", "", "OK");
            MainGridDetail.DataSource = DTabDetail;
            MainGridDetail.Refresh();

            if (MainGridDetail.RepositoryItems.Count == 0)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDetDetail;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdDetDetail.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            GrdDetDetail.EndUpdate();

            this.Show();

        }

        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            // ObjFormEvent.ObjToDisposeList.Add(ObjPacket);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPacketNo.Text.Trim().Length == 0)
                {
                    return;
                }
                StrFromDate = "";
                StrToDate = "";

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                if (RbtAll.Checked == true)
                {
                    StrStatus = Val.ToString(RbtAll.Tag);
                }
                else if (RbtApproved.Checked == true)
                {
                    StrStatus = Val.ToString(RbtApproved.Tag);
                }
                else if (RbtPending.Checked == true)
                {
                    StrStatus = Val.ToString(RbtPending.Tag);
                }
                else if (RbtReject.Checked == true)
                {
                    StrStatus = Val.ToString(RbtReject.Tag);
                }

                DTabDetail = Obj.GetRejectionData(StrFromDate, StrToDate, StrStatus);
                MainGridDetail.DataSource = DTabDetail;
                GrdDetDetail.RefreshData();
                GrdDetDetail.BestFitColumns();

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
                StrFromDate = "";
                StrToDate = "";

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                if (RbtAll.Checked == true)
                {
                    StrStatus = Val.ToString(RbtAll.Tag);
                }
                else if (RbtApproved.Checked == true)
                {
                    StrStatus = Val.ToString(RbtApproved.Tag);
                }
                else if (RbtPending.Checked == true)
                {
                    StrStatus = Val.ToString(RbtPending.Tag);
                }
                else if (RbtReject.Checked == true)
                {
                    StrStatus = Val.ToString(RbtReject.Tag);
                }
                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDetDetail.RowCount; IntI++)
                {
                    DataRow DRow = GrdDetDetail.GetDataRow(IntI);
                    if (txtKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                        && txtPacketNo.Text.Trim() == Val.ToString(DRow["PACKETNO"]).Trim()
                        && txtTag.Text.Trim() == Val.ToString(DRow["TAG"]).Trim()
                        )
                    {
                        ISFind = true;
                        GrdDetDetail.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();
                        //CalculateSummary();
                        GrdDetDetail.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataRow DRow = ObjKapan.GetDataForQCLiveStock(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, StrFromDate, StrToDate, StrStatus);
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;

                        DataRow DRowOS = ObjKapan.GetDataForQCLiveStock(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, StrFromDate, StrToDate, StrStatus);
                        string StrMsg = string.Empty;
                        if (DRowOS != null)
                        {
                            StrMsg = StrMsg + "Packet : " + Val.ToString(DRowOS["PACKETNO"]) + "\n\n";
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

                        IEnumerable<DataRow> rowsNew = DTabDetail.Rows.Cast<DataRow>();
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

                        DataRow DRNew = DTabDetail.NewRow();
                        foreach (DataColumn DCol in DTabDetail.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabDetail.Rows.Add(DRNew);


                        for (int IntI = 0; IntI < GrdDetDetail.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDetDetail.GetDataRow(IntI);
                            if (txtKapanName.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                                && txtPacketNo.Text.Trim() == Val.ToString(DRowGrid["PACKETNO"]).Trim()
                                && txtTag.Text.Trim() == Val.ToString(DRowGrid["TAG"]).Trim()
                                )
                            {
                                ISFind = true;
                                GrdDetDetail.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabDetail.AcceptChanges();
                                break;
                            }
                        }
                        GrdDetDetail.FocusedRowHandle = 0;
                    }
                    DRow = null;
                }

                GrdDetDetail.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDetDetail.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDetDetail.FocusedRowHandle = 0;
                GrdDetDetail.RefreshData();
                //GrdDet.BestFitMaxRowCount = 500;
                //GrdDet.BestFitColumns();
                MainGridDetail.Refresh();

                //CalculateSummary();

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;

                txtKapanName.Focus();
                GrdDetDetail.BestFitColumns();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtKapanName.Focus();
            }

        }

        private void BtnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";
                string StrOpe = "";

                DTab = GetTableOfSelectedRows(GrdDetDetail, true);
                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Record For Approve");
                    return;
                }

                if (Global.Confirm("Do You Want To Approve This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                // StrOpe = "APPROVED";
                foreach (DataRow Dr in DTab.Rows)
                {
                    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                    Property.TRN_ID = Val.ToInt64(Val.ToString(Dr["TRN_ID"]));
                    Property.PACKET_ID = Val.ToInt64(Val.ToString(Dr["PACKET_ID"]));
                    Property = Obj.ApprovedOrRejectTransaction(Property, "APPROVED");
                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DTabDetail.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    BtnRefresh_Click(null, null);
                    if (GrdDetDetail.RowCount > 1)
                    {
                        GrdDetDetail.FocusedRowHandle = GrdDetDetail.RowCount - 1;
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
                DTab.Rows.Clear();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private DataTable GetTableOfSelectedRows(DevExpress.XtraGrid.Views.Grid.GridView view, Boolean IsSelect)
        {
            if (view.RowCount <= 0)
            {
                return null;
            }

            ArrayList aryLst = new ArrayList();
            DataTable resultTable = new DataTable();
            DataTable sourceTable = null;
            sourceTable = ((DataView)view.DataSource).Table;

            if (IsSelect)
            {
                aryLst = ObjGridSelection.GetSelectedArrayList();
                resultTable = sourceTable.Clone();
                for (int i = 0; i < aryLst.Count; i++)
                {
                    DataRowView oDataRowView = aryLst[i] as DataRowView;
                    resultTable.Rows.Add(oDataRowView.Row.ItemArray);
                }
            }

            return resultTable;
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                string StrPacketNo = "";
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";
                string StrOpe = "";

                DTab = GetTableOfSelectedRows(GrdDetDetail, true);
                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Record For Reject");
                    return;
                }


                if (Global.Confirm("Do You Want To Reject This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }


                this.Cursor = Cursors.WaitCursor;
                int IntI = 0;
                for (int j = 0; j < GrdDetDetail.RowCount; j++)
                {
                    if (Val.ToBoolean(GrdDetDetail.GetRowCellValue(j, "COLSELECTCHECKBOX")) == true)
                    {
                        DataRow DRow = GrdDetDetail.GetDataRow(j);
                        IntI++;
                        StrPacketNo = Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + "-" + Val.ToString(DRow["TAG"]);

                        string StrQCSourceServer = "";
                        string StrQCSourceServerPath = "";
                        string StrQCSourceServerUserName = "";
                        string StrQCSourceServerPassword = "";
                        DataTable DTabPath = new DataTable();
                        int Employee_ID = 0;

                        TxtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                        Employee_ID = Val.ToInt32(TxtEmployee.Tag);
                        DTabPath = ObjKapan.GetDataForPath(Employee_ID);
                        if (DTabPath.Columns.Count == 0)
                        {
                            DataRow DRowQCServerUpload = ObjKapan.GetMainServerPath(); //Defult GalaxyOperator Process na Credential Consider karavya 6e..
                            StrQCSourceServer = Val.ToString(DRowQCServerUpload["UPLOADSERVERPATH"]);
                            StrQCSourceServerPath = Val.ToString(DRowQCServerUpload["UPLOADSERVERPATH"]);
                            StrQCSourceServerUserName = Val.ToString(DRowQCServerUpload["UPLOADSERVERUSERNAME"]);
                            StrQCSourceServerPassword = Val.ToString(DRowQCServerUpload["UPLOADSERVERPASSWORD"]);
                        }
                        else
                        {
                            StrQCSourceServer = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                            StrQCSourceServerPath = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                            StrQCSourceServerUserName = Val.ToString(DTabPath.Rows[0]["QCUSERWISEUSERNAME"]);
                            StrQCSourceServerPassword = Val.ToString(DTabPath.Rows[0]["QCUSERWISEPASSWARD"]);
                        }
                        var lastFolder = Path.GetDirectoryName(StrQCSourceServer);
                        StrQCSourceServer = lastFolder;
                        StrQCSourceServerPath = lastFolder;

                        string StrQCDestinationServerPath = "";
                        string StrQCDestinationServer = "";


                        string pStrQCDoneByCode = Val.ToString(DRow["QCDONEBYCODE"]);

                        string pStrQCSourceServerPath = StrQCSourceServerPath + "\\QCFail\\" + pStrQCDoneByCode + "\\" + StrPacketNo + "(GAL)" + ".cap";
                        string pStrQCDestinationServer = StrQCSourceServer + "\\QCPending\\" + pStrQCDoneByCode;
                        string pStrQCDestinationServerPath = pStrQCDestinationServer + "\\" + StrPacketNo + "(GAL)" + ".cap";

                        if (File.Exists(StrQCSourceServerPath) == false)
                        {
                            pStrQCSourceServerPath = StrQCSourceServerPath + "\\QCFail\\" + pStrQCDoneByCode + "\\" + StrPacketNo + "(GAL)" + ".adv";
                            pStrQCDestinationServer = StrQCSourceServer + "\\QCPending\\" + pStrQCDoneByCode;
                            pStrQCDestinationServerPath = pStrQCDestinationServer + "\\" + StrPacketNo + "(GAL)" + ".adv";
                        }


                        using (new AxonDataLib.BONetworkConnect(pStrQCDestinationServer, StrQCSourceServerUserName, StrQCSourceServerPassword))
                        {
                            if (Directory.Exists(pStrQCDestinationServer) == false)
                            {
                                Directory.CreateDirectory(pStrQCDestinationServer);
                            }
                            File.Move(pStrQCSourceServerPath, pStrQCDestinationServerPath);
                        }

                        StrOpe = "REJECT";

                        if (Val.ToString(DRow["REJECTSTATUS"]) == "REJECT")
                        {
                            Global.Message("This Packet Is Already Rejected");
                            return;
                        }
                        TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                        Property.TRN_ID = Val.ToInt64(Val.ToString(DRow["TRN_ID"]));
                        Property.PACKET_ID = Val.ToInt64(Val.ToString(DRow["PACKET_ID"]));
                        Property = Obj.ApprovedOrRejectTransaction(Property, StrOpe);
                        ReturnMessageDesc = Property.ReturnMessageDesc;
                        ReturnMessageType = Property.ReturnMessageType;

                        Property = null;
                        DTabDetail.AcceptChanges();
                        this.Cursor = Cursors.Default;

                        Global.Message(ReturnMessageDesc);
                        BtnRefresh_Click(null, null);

                    }
                }


                DTab.Rows.Clear();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";
                string StrOpe = "";
                string StrPacketNo = "";
                string StrUserCode = "";
                if (GrdDetDetail.RowCount > 1)
                {
                    GrdDetDetail.FocusedRowHandle = GrdDetDetail.RowCount - 1;
                }

                DTab = GetTableOfSelectedRows(GrdDetDetail, true);
                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Record For Return");
                    return;
                }
                if (Global.Confirm("Do You Want To Return This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;


                int IntI = 0;
                for (int j = 0; j < GrdDetDetail.RowCount; j++)
                {
                    if (Val.ToBoolean(GrdDetDetail.GetRowCellValue(j, "COLSELECTCHECKBOX")) == true)
                    {
                        DataRow DRow = GrdDetDetail.GetDataRow(j);
                        IntI++;
                        StrPacketNo = Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + "-" + Val.ToString(DRow["TAG"]);
                        StrUserCode = Val.ToString(DRow["USERCODE"]);

                        string StrQCSourceServer = "";
                        string StrQCSourceServerPath = "";
                        string StrQCSourceServerUserName = "";
                        string StrQCSourceServerPassword = "";
                        DataTable DTabPath = new DataTable();
                        int Employee_ID = 0;

                        TxtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                        Employee_ID = Val.ToInt32(TxtEmployee.Tag);
                        DTabPath = ObjKapan.GetDataForPath(Employee_ID);
                        if (DTabPath.Columns.Count == 0)
                        {
                            DataRow DR = ObjKapan.GetMainServerPath(); //Defult GalaxyOperator Process na Credential Consider karavya 6e..
                            StrQCSourceServer = Val.ToString(DR["UPLOADSERVERPATH"]);
                            StrQCSourceServerPath = Val.ToString(DR["UPLOADSERVERPATH"]);
                            StrQCSourceServerUserName = Val.ToString(DR["UPLOADSERVERUSERNAME"]);
                            StrQCSourceServerPassword = Val.ToString(DR["UPLOADSERVERPASSWORD"]);
                        }
                        else
                        {
                            StrQCSourceServer = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                            StrQCSourceServerPath = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                            StrQCSourceServerUserName = Val.ToString(DTabPath.Rows[0]["QCUSERWISEUSERNAME"]);
                            StrQCSourceServerPassword = Val.ToString(DTabPath.Rows[0]["QCUSERWISEPASSWARD"]);
                        }
                        var lastFolder = Path.GetDirectoryName(StrQCSourceServerPath);
                        StrQCSourceServerPath = lastFolder;

                        string StrQCDestinationServerPath = "";
                        string StrQCDestinationServer = "";
                        
                        string pStrQCSourceServerPath = StrQCSourceServerPath + "\\QCPending\\" + StrUserCode + "\\" + StrPacketNo + "(GAL)" + ".cap";
                        string pStrQCDestinationServer = StrQCSourceServer;
                        string pStrQCDestinationServerPath = pStrQCDestinationServer + "\\" + StrPacketNo + "(GAL)" + ".cap";


                        if (File.Exists(pStrQCSourceServerPath) == false)
                        {
                            pStrQCSourceServerPath = StrQCSourceServerPath + "\\QCPending\\" + StrUserCode + "\\" + StrPacketNo + "(GAL)" + ".adv";
                            pStrQCDestinationServer = StrQCSourceServer;
                            pStrQCDestinationServerPath = pStrQCDestinationServer + "\\" + StrPacketNo + "(GAL)" + ".adv";

                        }

                        using (new AxonDataLib.BONetworkConnect(StrQCSourceServer, StrQCSourceServerUserName, StrQCSourceServerPassword))
                        {
                            if (Directory.Exists(pStrQCDestinationServer) == false)
                            {
                                Directory.CreateDirectory(pStrQCDestinationServer);
                            }
                            File.Move(pStrQCSourceServerPath, pStrQCDestinationServerPath);
                        }

                        StrOpe = "RETURN";
                        foreach (DataRow Dr in DTab.Rows)
                        {
                            if (Val.ToString(Dr["STATUS"]) != "RUNNING" && Val.ToString(Dr["STATUS"]) != "NEXTPROCESS")
                            {
                                Global.Message("Oops..You can't return this entry");
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                            Property.TRN_ID = Val.ToInt64(Val.ToString(Dr["TRN_ID"]));
                            Property.PACKET_ID = Val.ToInt64(Val.ToString(Dr["PACKET_ID"]));
                            Property = Obj.ApprovedOrRejectTransaction(Property, StrOpe);
                            ReturnMessageDesc = Property.ReturnMessageDesc;
                            ReturnMessageType = Property.ReturnMessageType;

                            Property = null;

                        }
                        DTabDetail.AcceptChanges();
                        this.Cursor = Cursors.Default;

                        Global.Message(ReturnMessageDesc);
                        BtnRefresh_Click(null, null);


                    }
                }
                DTab.Rows.Clear();
                this.Cursor = Cursors.Default;

                /*StrOpe = "RETURN";
               foreach (DataRow Dr in DTab.Rows)
               {
                   if (Val.ToString(Dr["STATUS"]) != "RUNNING" && Val.ToString(Dr["STATUS"]) != "NEXTPROCESS")
                   {
                       Global.Message("Oops..You can't return this entry");
                       return;
                   }
                   TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                   Property.TRN_ID = Val.ToInt64(Val.ToString(Dr["TRN_ID"]));
                   Property.PACKET_ID = Val.ToInt64(Val.ToString(Dr["PACKET_ID"]));
                   Property = Obj.ApprovedOrRejectTransaction(Property, StrOpe);
                   ReturnMessageDesc = Property.ReturnMessageDesc;
                   ReturnMessageType = Property.ReturnMessageType;

                   Property = null;

               }
               DTabDetail.AcceptChanges();

               Global.Message(ReturnMessageDesc);*/

                /*foreach (DataRow dr in DTab.Rows)
                {
                    StrPacketNo = Val.ToString(dr["KAPANNAME"]) + "-" + Val.ToString(dr["PACKETNO"]) + "-" + Val.ToString(dr["TAG"]);
                    DataTable DTabPath = new DataTable();
                    int Employee_ID = 0;
                    TxtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                    Employee_ID = Val.ToInt32(TxtEmployee.Tag);
                    DTabPath = ObjKapan.GetDataForPath(Employee_ID);
                    if (DTabPath.Columns.Count == 0)
                    {
                        DataRow DRow = ObjKapan.GetMainServerPath(); //Defult GalaxyOperator Process na Credential Consider karavya 6e..
                        StrQCSourceServer = Val.ToString(DRow["UPLOADSERVERPATH"]);
                        StrQCSourceServerPath = Val.ToString(DRow["UPLOADSERVERPATH"]);
                        StrQCSourceServerUserName = Val.ToString(DRow["UPLOADSERVERUSERNAME"]);
                        StrQCSourceServerPassword = Val.ToString(DRow["UPLOADSERVERPASSWORD"]);
                    }
                    else
                    {
                        StrQCSourceServer = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                        StrQCSourceServerPath = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                        StrQCSourceServerUserName = Val.ToString(DTabPath.Rows[0]["QCUSERWISEUSERNAME"]);
                        StrQCSourceServerPassword = Val.ToString(DTabPath.Rows[0]["QCUSERWISEPASSWARD"]);
                    }

                    string StrQCDestinationServerPath = "";
                    string StrQCDestinationServer = "";
                    string StrPath = GetHierarchyOfFolder(StrQCSourceServer, "", ".cap");
                    var lastFolder = Path.GetDirectoryName(StrPath);

                    StrQCSourceServerPath = StrQCSourceServerPath + "\\QCPending\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE + "\\" + StrPacketNo + "(GAL)" + ".cap";
                    StrQCDestinationServer = lastFolder;
                    StrQCDestinationServerPath = StrQCDestinationServer + "\\" + StrPacketNo + "(GAL)" + ".cap";

                    using (new AxonDataLib.BONetworkConnect(StrQCSourceServer, StrQCSourceServerUserName, StrQCSourceServerPassword))
                    {
                        if (Directory.Exists(StrQCDestinationServer) == false)
                        {
                            Directory.CreateDirectory(StrQCDestinationServer);
                        }
                        File.Move(StrQCSourceServerPath, StrQCDestinationServerPath);
                    }

                }

                    
              
            DTab.Rows.Clear();*/

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }


        public string GetHierarchyOfFolder(string StrRootPath, string StrFolderName, string StrExtension)
        {
            Stack<string> dirs = new Stack<string>(20);

            string StrDirectoryPath = "";

            string root = StrRootPath;

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();

                DirectoryInfo di = new DirectoryInfo(@currentDir);

                DirectoryInfo[] subDirs1 = di.GetDirectories();



                string[] files = null;
                try
                {
                    //  files = System.IO.Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }



                //Path ni under je sub Folder's hoy eni under File Check karse and Path aapse
                foreach (DirectoryInfo Str in subDirs1)
                {
                    //if (Val.ToString(Str.Name).ToUpper() == Val.ToString(StrFolderName).ToUpper())
                    if (Val.ToString(Str.Name).ToUpper() != "QCPENDING" && Val.ToString(Str.Name).ToUpper() != "QCCOMPLETE") //coz Server na Main path ma j QCPending And QCComplete nu folder hase..
                    {
                        FileInfo[] Files = Str.GetFiles("*" + StrExtension.ToLower()); //Getting Text files
                        string str = "";
                        if (Files.Length > 0)
                        {
                            foreach (FileInfo file in Files)
                            {
                                dirs.Push(file.Name);
                                StrDirectoryPath = file.FullName;
                                break;
                            }
                        }

                    }
                }

                if (StrDirectoryPath == "")
                {
                    //Path ni under Files Check karse and path aapse...SubFolder mathi nahi
                    FileInfo[] Files = di.GetFiles("*" + StrExtension.ToLower()); //Getting Text files
                    if (Files.Length > 0)
                    {
                        foreach (FileInfo file in Files)
                        {
                            dirs.Push(file.Name);
                            StrDirectoryPath = file.FullName;
                            break;
                        }
                    }
                }

                if (StrDirectoryPath != "")
                    break;

            }

            return StrDirectoryPath;
        }
    }
}


