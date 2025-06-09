using BusLib.Configuration;
using BusLib.Transaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusLib.TableName;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using BusLib.Master;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using AxoneMFGRJ.Utility;
using AxoneMFGRJ.Transaction;
using DevExpress.XtraPrinting;
using System.Drawing;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmParcelPacketLiveStock : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelectionForKapan;
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();

        DataTable DtabPacket = new DataTable();
        DataTable DTabMainPacketLiveStock = new DataTable();
        DataTable DTabSubPacketLiveStock = new DataTable();
        BOFormPer ObjPer = new BOFormPer();

        System.Diagnostics.Stopwatch watch = null;

        string mStrPassward = "";

        #region Property Settings

        public FrmParcelPacketLiveStock()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();



            DTabSubPacketLiveStock = ObjKapan.GetDataForParcelPacketLiveStock("SUBPACKET", "NONE", "NONE", "NONE", 0, "", false, false, false);
            MainGridSubPkt.DataSource = DTabSubPacketLiveStock;
            MainGridSubPkt.Refresh();

            if (MainGridSubPkt.RepositoryItems.Count == 0)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDetSubPkt;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdDetSubPkt.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }

            if (MainGridMainPkt.RepositoryItems.Count == 4)
            {
                ObjGridSelectionForKapan = new BODevGridSelection();
                ObjGridSelectionForKapan.View = GrdDetMainPkt;
                ObjGridSelectionForKapan.ClearSelection();
                ObjGridSelectionForKapan.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelectionForKapan.ClearSelection();
            }
            GrdDetMainPkt.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelectionForKapan != null)
            {
                ObjGridSelectionForKapan.ClearSelection();
                ObjGridSelectionForKapan.CheckMarkColumn.VisibleIndex = 0;
            }


            // Action Button Rights And Stock Permission as per User Rights
            EmployeeActionRightsProperty Property = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

            RbtSubPktFullStock.Checked = false;
            RbtSubPktDeptStock.Checked = false;
            RbtSubPktMYStock.Checked = false;


            RbtMainPktFullStock.Enabled = Property.ISMAINFULLSTOCK;
            RbtMainPktDeptStock.Enabled = Property.ISMAINDEPTSTOCK;
            RbtMainPktFullStock.Enabled = Property.ISMAINFULLSTOCK;
            RbtMainPktOtherStock.Enabled = Property.ISMAINOTHERSTOCK;

            RbtSubPktFullStock.Enabled = Property.ISSUBFULLSTOCK;

            RbtSubPktMYStock.Enabled = Property.ISSUBMYSTOCK;
            RbtSubPktMYStock.Checked = Property.ISSUBMYSTOCK;

            RbtSubPktDeptStock.Enabled = Property.ISSUBDEPTSTOCK;
            RbtSubPktDeptStock.Checked = Property.ISSUBDEPTSTOCK;

            RbtSubPktOtherStock.Enabled = Property.ISSUBOTHERSTOCK;

            PanelIssue.Visible = Property.ISSUBEMPISSUE;
            PanelReturn.Visible = Property.ISSUBEMPRETURN;
            PanelWithSplit.Visible = Property.ISSUBRETURNWITHSPLIT;
            PanelRejection.Visible = Property.ISSUBREJECTIONTRANSFER;
            PanelWithSplit.Visible = Property.ISSUBRETURNWITHSPLIT;

            Property = null;

            // END : DHARA : 21-04-22

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            mStrPassward = ObjPer.PASSWORD;
            txtPassForDisplayBack_TextChanged(null, null);

            this.Show();

            //BtnSearch.PerformClick();

            /*
            DTabMainPacketLiveStock = ObjKapan.GetDataForParcelPacketLiveStock("MAINPACKET", "NONE", "NONE", "NONE", 0, "", false, false);
            MainGridMainPkt.DataSource = DTabMainPacketLiveStock;
            MainGridMainPkt.Refresh();
            */



            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDetSubPkt.Name);

            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDetSubPkt.RestoreLayoutFromStream(stream);

            }

            RtbWithoutBarcode_CheckedChanged(null, null);
            BtnSinglePktCreateExit_Click(null, null);
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;
                if (RbtSubPktDeptStock.Checked == true)
                {
                    StrType = RbtSubPktDeptStock.Tag.ToString();
                }
                else if (RbtSubPktFullStock.Checked == true)
                {
                    StrType = RbtSubPktFullStock.Tag.ToString();
                }
                else if (RbtSubPktMYStock.Checked == true)
                {
                    StrType = RbtSubPktMYStock.Tag.ToString();
                }
                else if (RbtSubPktOtherStock.Checked == true)
                {
                    StrType = RbtSubPktOtherStock.Tag.ToString();
                }

                ObjGridSelectionForKapan.ClearSelection();
                DTabSubPacketLiveStock.Rows.Clear();
                

                DTabSubPacketLiveStock = ObjKapan.GetDataForParcelPacketLiveStock("SUBPACKET", "ALL", StrType, "", 0, "", false, ChkMainPktDisplayAll.Checked, ChkSubPktDisplayAll.Checked);
                MainGridSubPkt.DataSource = DTabSubPacketLiveStock;
                GrdDetSubPkt.BestFitMaxRowCount = 500;
                //GrdDetSubPkt.BestFitColumns();
                MainGridSubPkt.Refresh();

                BtnSubPktAckPending.Text = "Ack.(0)";
                if (DTabSubPacketLiveStock.Rows.Count != 0)
                {
                    BtnSubPktAckPending.Text = "&Ack.(" + DTabSubPacketLiveStock.Rows[0]["PENDINGJANGED"].ToString() + ")";
                }

                CalculateSummary();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
            //else 
            if (e.KeyCode == Keys.F5)
            {
                BtnSubPacketSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F6)
            {
                BtnSubPktAckPending.PerformClick();
            }
            else if (e.KeyCode == Keys.F7)
            {
                BtnSubPktTransfer.PerformClick();
            }
        }

        private DataTable GetTableOfSelectedRows(GridView view, Boolean IsSelect)
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

        private DataTable GetTableOfSelectedKapanRows(GridView view, Boolean IsSelect)
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
                aryLst = ObjGridSelectionForKapan.GetSelectedArrayList();
                resultTable = sourceTable.Clone();
                for (int i = 0; i < aryLst.Count; i++)
                {
                    DataRowView oDataRowView = aryLst[i] as DataRowView;
                    resultTable.Rows.Add(oDataRowView.Row.ItemArray);
                }
            }

            return resultTable;
        }

        public void CalculateSummary()
        {
            int IntPcs = 0;
            double DouCarat = 0;
            int IntSelPcs = 0;
            double DouSelCarat = 0;

            for (int IntI = 0; IntI < GrdDetSubPkt.RowCount; IntI++)
            {
                DataRow DRow = GrdDetSubPkt.GetDataRow(IntI);
                IntPcs = IntPcs + 1;
                DouCarat = DouCarat + Val.Val(DRow["BALANCECARAT"]);
            }

            DataTable DTab = GetTableOfSelectedRows(GrdDetSubPkt, true);
            if (DTab == null)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                IntSelPcs = IntSelPcs + 1;
                DouSelCarat = DouSelCarat + Val.Val(DRow["BALANCECARAT"]);
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

        private void BtnKapanLiveStockAutoFit_Click(object sender, EventArgs e)
        {
            GrdDetSubPkt.BestFitColumns();
        }

        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PacketLiveStock", GrdDetSubPkt);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            BtnSearch_Click(null, null);
        }

        private void MainGrid_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateSummary();
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDetSubPkt, true);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                foreach (DataRow DRow in DTab.Rows)
                {
                    if (Val.ToString(DRow["ENTRYTYPE"]) == "EMPISS")
                    {
                        Global.Message("This Packet is Already Issue To " + Val.ToString(DRow["EMPLOYEECODE"]) + ", You Can't Transfer PacketNo : " + Val.ToString(DRow["KAPANNAME"]) + '-' + Val.ToString(DRow["PACKETNOSTR"]));
                        return;
                    }
                    if (Val.ToBoolean(DRow["ISMERGE"]))
                    {
                        Global.Message("This Packet Transaction is Already Completed So You Can't Transfer PacketNo : " + Val.ToString(DRow["KAPANNAME"]) + '-' + Val.ToString(DRow["PACKETNOSTR"]));
                        return;
                    }
                    if (Val.Val(DRow["BALANCECARAT"]) == 0)
                    {
                        Global.Message("You Have Not Enough Balance For This Action ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                        return;
                    }
                }

                FrmParcelGoodsTransfer FrmParcelGoodsTransfer = new FrmParcelGoodsTransfer();
                FrmParcelGoodsTransfer.MdiParent = Global.gMainRef;
                FrmParcelGoodsTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmParcelGoodsTransfer.ShowForm(DTab, Parcel.FrmParcelGoodsTransfer.FORMTYPE.TRANSFER);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

        }

        private void FormTransfer_Closing(object sender, FormClosingEventArgs e)
        {
            DTabSubPacketLiveStock.Rows.Clear();
            ObjGridSelection.ClearSelection();
            CalculateSummary();
            txtSubPktKapanName.Focus();
            BtnSearch_Click(null, null);
        }

        private void BtnAckPending_Click(object sender, EventArgs e)
        {
            FrmParcelPacketConfirmation FrmParcelPacketConfirmation = new FrmParcelPacketConfirmation();
            FrmParcelPacketConfirmation.MdiParent = Global.gMainRef;
            FrmParcelPacketConfirmation.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
            FrmParcelPacketConfirmation.ShowForm();
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {

        }

        private void BtnReturnWithSplit_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDetSubPkt, true);

                if (DTab == null)
                {
                    Global.Message("Please Select One Packet For Spliting Operation");
                    return;
                }

                foreach (DataRow DRow in DTab.Rows)
                {
                    if (Val.Val(DRow["BALANCECARAT"]) == 0)
                    {
                        Global.Message("You Have Not Enough Balance For Split, First Confirmed Pending Goods of PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                        return;
                    }
                }

                if (DTab.Rows.Count != 1)
                {
                    Global.Message("Please Select Only One Packet For Spliting Operation");
                    return;
                }

                FrmParcelGoodsReturnWithSplit FrmParcelGoodsReturnWithSplit = new FrmParcelGoodsReturnWithSplit();
                FrmParcelGoodsReturnWithSplit.MdiParent = Global.gMainRef;
                FrmParcelGoodsReturnWithSplit.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmParcelGoodsReturnWithSplit.ShowForm(Val.ToString(DTab.Rows[0]["KAPANNAME"]), Val.ToString(DTab.Rows[0]["PACKETNO"]), Val.ToString(DTab.Rows[0]["TAG"]), Val.ToInt32(DTab.Rows[0]["PROCESS_ID"]), Val.ToString(DTab.Rows[0]["PROCESSNAME"]));

            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }
            */
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDetSubPkt, true);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }


                foreach (DataRow DRow in DTab.Rows)
                {
                    if (Val.ToString(DRow["ENTRYTYPE"]) == "EMPRET")
                    {
                        Global.Message("This Packet is Already Return To " + Val.ToString(DRow["EMPLOYEECODE"]) + ", You Can't Return PacketNo : " + Val.ToString(DRow["KAPANNAME"]) + '-' + Val.ToString(DRow["PACKETNOSTR"]));
                        return;
                    }
                    if (Val.Val(DRow["BALANCECARAT"]) == 0)
                    {
                        Global.Message("You Have Not Enough Balance For This Action ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                        return;
                    }
                }

                FrmParcelGoodsTransfer FrmParcelGoodsTransfer = new FrmParcelGoodsTransfer();
                FrmParcelGoodsTransfer.MdiParent = Global.gMainRef;
                FrmParcelGoodsTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmParcelGoodsTransfer.ShowForm(DTab, Parcel.FrmParcelGoodsTransfer.FORMTYPE.STAFFRETURNWITHMERGE);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnStaffIssue_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDetSubPkt, true);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                foreach (DataRow DRow in DTab.Rows)
                {
                    if (Val.ToString(DRow["ENTRYTYPE"]) == "EMPISS")
                    {
                        Global.Message("This Packet is Already Isuue To " + Val.ToString(DRow["EMPLOYEECODE"]) + ", You Can't Issue PacketNo : " + Val.ToString(DRow["KAPANNAME"]) + '-' + Val.ToString(DRow["PACKETNOSTR"]));
                        return;
                    }
                    if (Val.ToBoolean(DRow["ISMERGE"]))
                    {
                        Global.Message("This Packet Transaction is Already Completed So You Can't Issue PacketNo : " + Val.ToString(DRow["KAPANNAME"]) + '-' + Val.ToString(DRow["PACKETNOSTR"]));
                        return;
                    }
                    if (Val.Val(DRow["BALANCECARAT"]) == 0)
                    {
                        Global.Message("You Have Not Enough Balance For This Action ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                        return;
                    }
                }

                FrmParcelGoodsTransfer FrmParcelGoodsTransfer = new FrmParcelGoodsTransfer();
                FrmParcelGoodsTransfer.MdiParent = Global.gMainRef;
                FrmParcelGoodsTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmParcelGoodsTransfer.ShowForm(DTab, Parcel.FrmParcelGoodsTransfer.FORMTYPE.STAFFISSUE);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

        }

        private void BtnStaffReturn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDetSubPkt, true);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }


                foreach (DataRow DRow in DTab.Rows)
                {
                    if (Val.ToString(DRow["ENTRYTYPE"]) == "EMPRET")
                    {
                        Global.Message("This Packet is Already Return To " + Val.ToString(DRow["EMPLOYEECODE"]) + ", You Can't Return PacketNo : " + Val.ToString(DRow["KAPANNAME"]) + '-' + Val.ToString(DRow["PACKETNOSTR"]));
                        return;
                    }
                    if (Val.ToBoolean(DRow["ISMERGE"]))
                    {
                        Global.Message("This Packet Transaction is Already Completed So You Can't Return PacketNo : " + Val.ToString(DRow["KAPANNAME"]) + '-' + Val.ToString(DRow["PACKETNOSTR"]));
                        return;
                    }
                    if (Val.Val(DRow["BALANCECARAT"]) == 0)
                    {
                        Global.Message("You Have Not Enough Balance For This Action ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                        return;
                    }
                }

                FrmParcelGoodsTransfer FrmParcelGoodsTransfer = new FrmParcelGoodsTransfer();
                FrmParcelGoodsTransfer.MdiParent = Global.gMainRef;
                FrmParcelGoodsTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmParcelGoodsTransfer.ShowForm(DTab, Parcel.FrmParcelGoodsTransfer.FORMTYPE.STAFFRETURN);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

        }

        private void GrdDet_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            DataRow DR = GrdDetSubPkt.GetDataRow(e.RowHandle);
            if (Val.ISDate(DR["CONFDATE"]) == true)
            {
                e.Appearance.BackColor = lblConfiredGoods.BackColor;
            }
            else if (Val.ISDate(DR["CONFDATE"]) == false)
            {
                e.Appearance.BackColor = lblPendingsGoods.BackColor;
            }

        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDetSubPkt.BestFitMaxRowCount = 5000000;
            GrdDetSubPkt.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void RbtFullStock_CheckedChanged(object sender, EventArgs e)
        {
            BtnSubPktAckPending.Enabled = !RbtSubPktFullStock.Checked;
            BtnSubPktTransfer.Enabled = !RbtSubPktFullStock.Checked;
            BtnSubPktReturnWithSplit.Enabled = !RbtSubPktFullStock.Checked;
            BtnSubPktStaffIssue.Enabled = !RbtSubPktFullStock.Checked;
            BtnSubPktStaffReturn.Enabled = !RbtSubPktFullStock.Checked;
            BtnSubPktRejection.Enabled = !RbtSubPktFullStock.Checked;
            BtnSearch_Click(null, null);
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGridSubPkt) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGridSubPkt.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FROMEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FROMEMPLOYEENAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TOEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOEMPLOYEENAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FROMMANAGERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FROMMANAGERNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TOMANAGERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOMANAGERNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MARKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "MARKERNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TRANSBYCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TRANSBYNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "CONFBYCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "CONFBYNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "ISSUECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "ISSUENAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "PREVISSUECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "PREVISSUENAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "WORKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "WORKERNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "POLISHFINALEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "POLISHFINALEMPLOYEENAME")));
                    return;
                }
            }
            finally
            {
                e.Info = info;
            }
        }

        private void BtnRejection_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDetSubPkt, true);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                //FrmSingleRejectionTransfer FrmSingleRejectionTransfer = new FrmSingleRejectionTransfer();
                //FrmSingleRejectionTransfer.MdiParent = Global.gMainRef;
                //FrmSingleRejectionTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                //FrmSingleRejectionTransfer.ShowForm(DTab, Trasaction.FrmSingleRejectionTransfer.FORMTYPE.PARCEL);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

        }

        private void lblSaveLayout_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdDetSubPkt.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDetSubPkt.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblDefaultLayout_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDetSubPkt.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void BtnTransferToPCNRejection_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDetSubPkt, true);

                if (DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                foreach (DataRow DRow in DTab.Rows)
                {
                    if (Val.ISDate(DRow["CONFDATE"]) == false)
                    {
                        Global.Message("First You Confirmed Goods ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                        return;
                    }
                }

                //FrmSingleRejectionTransfer FrmSingleRejectionTransfer = new FrmSingleRejectionTransfer();
                //FrmSingleRejectionTransfer.MdiParent = Global.gMainRef;
                //FrmSingleRejectionTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                //FrmSingleRejectionTransfer.ShowForm(DTab, Trasaction.FrmSingleRejectionTransfer.FORMTYPE.PARCEL);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

        }

        private void txtPacketNo_Validated(object sender, EventArgs e)
        {
            try
            {

                if (txtSubPktKapanName.Text.Trim().Length == 0)
                {
                    return;
                }
                if (Val.ToInt(txtSubPktPacketNo.Text) == 0)
                {
                    txtSubPktKapanName.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;
                if (RbtSubPktDeptStock.Checked == true)
                {
                    StrType = RbtSubPktDeptStock.Tag.ToString();
                }
                else if (RbtSubPktFullStock.Checked == true)
                {
                    StrType = RbtSubPktFullStock.Tag.ToString();
                }
                else if (RbtSubPktMYStock.Checked == true)
                {
                    StrType = RbtSubPktMYStock.Tag.ToString();
                }
                else if (RbtSubPktOtherStock.Checked == true)
                {
                    StrType = RbtSubPktOtherStock.Tag.ToString();
                }

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDetSubPkt.RowCount; IntI++)
                {
                    DataRow DRow = GrdDetSubPkt.GetDataRow(IntI);
                    if (txtSubPktKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                        && txtSubPktPacketNo.Text.Trim() == Val.ToString(DRow["PACKETNO"]).Trim()
                        )
                    {
                        ISFind = true;
                        GrdDetSubPkt.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtSubPktKapanName.Text = string.Empty;
                        txtSubPktPacketNo.Text = string.Empty;

                        txtSubPktKapanName.Focus();
                        CalculateSummary();
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataRow DRow = ObjKapan.GetDataForParcelPacketLiveStockPacketInfo("SUBPACKET", "ALL", StrType, txtSubPktKapanName.Text, Val.ToInt(txtSubPktPacketNo.Text), "", "", false, ChkMainPktDisplayAll.Checked, ChkSubPktDisplayAll.Checked);
                    //ObjKapan.GetDataForParcelPacketLiveStock("SUBPACKET", "ALL", StrType, "", 0, "", false, ChkMainPktDisplayAll.Checked, ChkSubPktDisplayAll.Checked);
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;

                        Global.MessageError(txtSubPktKapanName.Text + "-" + txtSubPktPacketNo.Text + " Packet Not In Stock Kindly Check");
                        txtSubPktKapanName.Text = string.Empty;
                        txtSubPktPacketNo.Text = string.Empty;

                        txtSubPktKapanName.Focus();
                        return;
                    }
                    else
                    {

                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        IEnumerable<DataRow> rowsNew = DTabSubPacketLiveStock.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtSubPktKapanName.Text = string.Empty;
                            txtSubPktPacketNo.Text = string.Empty;
                            txtSubPktKapanName.Focus();
                            return;
                        }
                        // 07-06-2019

                        DataRow DRNew = DTabSubPacketLiveStock.NewRow();
                        foreach (DataColumn DCol in DTabSubPacketLiveStock.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }

                        DTabSubPacketLiveStock.Rows.Add(DRNew);
                        GrdDetSubPkt.SetRowCellValue(DTabSubPacketLiveStock.Rows.Count - 1, "COLSELECTCHECKBOX", true);
                    }
                    DRow = null;
                }

                GrdDetSubPkt.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDetSubPkt.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDetSubPkt.RefreshData();

                GrdDetSubPkt.BestFitMaxRowCount = 500;
                GrdDetSubPkt.BestFitColumns();
                MainGridSubPkt.Refresh();

                CalculateSummary();

                txtSubPktKapanName.Text = string.Empty;
                txtSubPktPacketNo.Text = string.Empty;

                txtSubPktKapanName.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }

        }

        private void GrdDet_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = string.Empty;
            }
        }

        private void GrdDet_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateSummary();
        }

        private void GrdDet_MouseUp(object sender, MouseEventArgs e)
        {
            CalculateSummary();
        }

        private void BtnMainPacketSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string StrType = string.Empty;
                if (RbtMainPktDeptStock.Checked == true)
                {
                    StrType = RbtMainPktDeptStock.Tag.ToString();
                }
                else if (RbtMainPktFullStock.Checked == true)
                {
                    StrType = RbtMainPktFullStock.Tag.ToString();
                }
                else if (RbtMainPktMYStock.Checked == true)
                {
                    StrType = RbtMainPktMYStock.Tag.ToString();
                }
                else if (RbtMainPktOtherStock.Checked == true)
                {
                    StrType = RbtMainPktOtherStock.Tag.ToString();
                }

                DTabMainPacketLiveStock.Rows.Clear();
                ObjGridSelection.ClearSelection();

                DTabMainPacketLiveStock = ObjKapan.GetDataForParcelPacketLiveStock("MAINPACKET", "ALL", StrType, "", 0, "", false, ChkMainPktDisplayAll.Checked, ChkSubPktDisplayAll.Checked);
                MainGridMainPkt.DataSource = DTabMainPacketLiveStock;
                GrdDetMainPkt.BestFitMaxRowCount = 500;
                MainGridMainPkt.Refresh();
                BtnMainPktAckPending.Text = "Ack.(0)";
                if (DTabMainPacketLiveStock.Rows.Count != 0)
                {
                    BtnMainPktAckPending.Text = "&Ack.(" + DTabMainPacketLiveStock.Rows[0]["PENDINGJANGED"].ToString() + ")";
                }
                CalculateSummary();

                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void BtnMainPktAckPending_Click(object sender, EventArgs e)
        {
            FrmParcelPacketConfirmation FrmParcelPacketConfirmation = new FrmParcelPacketConfirmation();
            FrmParcelPacketConfirmation.MdiParent = Global.gMainRef;
            FrmParcelPacketConfirmation.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
            FrmParcelPacketConfirmation.ShowForm();
        }

        private void repBtnProcessIssue_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = new DataTable();
                string StrKapanName = "";
                Int64 StrKapan_ID = 0;

                string StrMainPacketNo = "";
                Int64 StrMainPacket_ID = 0;

                string pStrMarkerCode = "";
                Int64 pStrMarker_ID = 0;

                string StrKapanManager = "";//Gunjan:27/03/2023
                Int64 StrKapanManager_ID = 0;


                if (GrdDetMainPkt.FocusedRowHandle < 0)
                {
                    StrKapanName = string.Empty;
                    StrKapan_ID = 0;
                    StrMainPacketNo = string.Empty;
                    StrMainPacket_ID = 0;
                    pStrMarkerCode = string.Empty;
                    pStrMarker_ID = 0;


                }
                else
                {
                    StrKapanName = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("KAPANNAME"));
                    StrKapan_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("KAPAN_ID"));

                    StrMainPacketNo = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("PACKETNO"));
                    StrMainPacket_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("PACKET_ID"));

                    pStrMarkerCode = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("MARKERCODE"));
                    pStrMarker_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("MARKER_ID"));

                    StrKapanManager = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("KAPANMAINMANAGER"));
                    StrKapanManager_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("KapanMainManager_ID"));

             
                }
                FrmPartyIssueWithPacket FrmPartyIssueWithPacket = new FrmPartyIssueWithPacket();
                FrmPartyIssueWithPacket.MdiParent = Global.gMainRef;
                FrmPartyIssueWithPacket.ShowForm(StrKapanName, StrKapan_ID, StrMainPacketNo, StrMainPacket_ID, pStrMarkerCode, pStrMarker_ID, StrKapanManager, StrKapanManager_ID);
                FrmPartyIssueWithPacket.FormClosing += new FormClosingEventHandler(Form_Closing);
            }

            catch (Exception Ex)
            {

            }
        }

        private void RepMFGProcess_Click(object sender, EventArgs e)
        {
            string StrKapanName = "";
            Guid StrKapan_ID = Guid.Empty;

            string StrMainPacketNo = "";
            Guid StrMainPacket_ID = Guid.Empty;

            string pStrMarkerCode = "";
            Int64 pStrMarker_ID = 0;

            double pDouBalanceCts = 0;

            string pStrEmpCode = "";
            Int64 pIntEmp_ID = 0;


            if (GrdDetMainPkt.FocusedRowHandle < 0)
            {
                StrKapanName = string.Empty;
                StrKapan_ID = Guid.Empty;
                StrMainPacketNo = string.Empty;
                StrMainPacket_ID = Guid.Empty;
                pStrMarkerCode = string.Empty;
                pStrMarker_ID = 0;
            }
            else
            {
                StrKapanName = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("KAPANNAME"));
                StrKapan_ID = Guid.Parse(Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("KAPAN_ID")));

                StrMainPacketNo = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("PACKETNO"));
                StrMainPacket_ID = Guid.Parse(Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("PACKET_ID")));

                pStrMarkerCode = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("MARKERCODE"));
                pStrMarker_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("MARKER_ID"));

                pDouBalanceCts = Val.Val(GrdDetMainPkt.GetFocusedRowCellValue("BALANCECARAT"));

                pStrEmpCode = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("EMPLOYEECODE"));
                pIntEmp_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("EMPLOYEE_ID"));
            }
            //FrmMFGPacketCreate FrmMFGPacketCreate = new FrmMFGPacketCreate();
            //FrmMFGPacketCreate.MdiParent = Global.gMainRef;
            //FrmMFGPacketCreate.ShowForm(StrKapanName, StrKapan_ID, StrMainPacketNo, StrMainPacket_ID, pStrMarkerCode, pStrMarker_ID, pDouBalanceCts, pStrEmpCode, pIntEmp_ID);
            //FrmMFGPacketCreate.FormClosing += new FormClosingEventHandler(Form_Closing);
        }

        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
            if (Val.ToString(txtPassForDisplayBack.Text) != "" && Val.ToString(txtPassForDisplayBack.Text).ToUpper() == Val.ToString(mStrPassward).ToUpper())
            {
                GrdDetMainPkt.Columns["BALANCECARAT"].OptionsColumn.AllowEdit = true;
            }
            else
            {
                GrdDetMainPkt.Columns["BALANCECARAT"].OptionsColumn.AllowEdit = false;
            }
        }



        private void GrdDetMainPkt_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            try
            {
                if (GrdDetMainPkt.FocusedRowHandle < 0)
                    return;

                GrdDetMainPkt.PostEditor();
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message.ToString());
            }
        }

        private void GrdDetMainPkt_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (GrdDetMainPkt.FocusedRowHandle < 0)
                    return;

                GrdDetMainPkt.PostEditor();
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message.ToString());
            }
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RtbWithoutBarcode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RtbWithoutBarcode.Checked == true) //#P
                {
                    txtBarcodeNo.Enabled = false;
                    txtSubPktKapanName.Enabled = true;
                    txtSubPktPacketNo.Enabled = true;
                    txtBarcodeNo.Text = string.Empty;

                    txtSubPktKapanName.Focus();

                    txtSubPktKapanName.TabIndex = 1;
                    txtSubPktPacketNo.TabIndex = 2;
                    txtSubPktJangedNo.Enabled = false;

                    RtbWithoutBarcode.TabStop = false;

                    RbtSubPktDeptStock.TabStop = false;
                    RbtSubPktFullStock.TabStop = false;
                    RbtSubPktMYStock.TabStop = false;
                    RbtSubPktOtherStock.TabStop = false;
                }
                else if (RtbBarcode.Checked == true)
                {
                    txtBarcodeNo.Enabled = true;
                    txtSubPktPacketNo.Enabled = false;

                    txtSubPktKapanName.TabIndex = 1;
                    txtBarcodeNo.TabIndex = 2;
                    RtbBarcode.TabStop = false;
                    txtSubPktJangedNo.Enabled = false;

                    txtSubPktKapanName.Focus();
                }
                else if (RtbJangedNo.Checked == true)
                {
                    txtSubPktJangedNo.Enabled = true;

                    txtBarcodeNo.Text = string.Empty;
                    txtSubPktKapanName.Text = string.Empty;
                    txtSubPktPacketNo.Text = string.Empty;
                    txtBarcodeNo.Enabled = false;
                    txtSubPktKapanName.Enabled = false;
                    txtSubPktPacketNo.Enabled = false;
                    txtSubPktJangedNo.Focus();
                }
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message.ToString());
            }
        }

        private void txtBarcodeNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtSubPktKapanName.Text.Trim().Length == 0)
                {
                    txtSubPktKapanName.Focus();
                    return;
                }

                if (Val.ToInt(txtBarcodeNo.Text) == 0)
                {
                    txtBarcodeNo.Text = "";
                    txtSubPktKapanName.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;

                if (RbtSubPktDeptStock.Checked == true)
                {
                    StrType = RbtSubPktDeptStock.Tag.ToString();
                }
                else if (RbtSubPktFullStock.Checked == true)
                {
                    StrType = RbtSubPktFullStock.Tag.ToString();
                }
                else if (RbtSubPktMYStock.Checked == true)
                {
                    StrType = RbtSubPktMYStock.Tag.ToString();
                }
                else if (RbtSubPktOtherStock.Checked == true)
                {
                    StrType = RbtSubPktOtherStock.Tag.ToString();
                }

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDetSubPkt.RowCount; IntI++)
                {
                    DataRow DRow = GrdDetSubPkt.GetDataRow(IntI);
                    if (txtSubPktKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                        && txtBarcodeNo.Text.Trim() == Val.ToString(DRow["BARCODENO"]).Trim()
                        )
                    {
                        ISFind = true;
                        GrdDetSubPkt.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtSubPktKapanName.Text = string.Empty;
                        txtSubPktPacketNo.Text = string.Empty;
                        txtBarcodeNo.Text = string.Empty;

                        txtSubPktKapanName.Focus();
                        CalculateSummary();
                        break;
                    }
                }
                if (ISFind == false)
                {
                    //DataRow DRow = ObjKapan.GetDataForParcelPacketLiveStockBarcodeInfo("ALL", StrType, txtSubPktKapanName.Text, Val.ToInt(txtSubPktPacketNo.Text), Val.ToInt32(txtBarcodeNo.Text), "");
                    DataRow DRow = ObjKapan.GetDataForParcelPacketLiveStockPacketInfo("SUBPACKET", "ALL", StrType, txtSubPktKapanName.Text, 0, "", Val.ToString(txtBarcodeNo.Text), false, ChkMainPktDisplayAll.Checked, ChkSubPktDisplayAll.Checked);
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;

                        Global.MessageError(txtSubPktKapanName.Text + "-" + txtBarcodeNo.Text + " Packet Not In Stock Kindly Check");
                        txtSubPktKapanName.Text = string.Empty;
                        txtSubPktPacketNo.Text = string.Empty;
                        txtBarcodeNo.Text = string.Empty;
                        txtSubPktKapanName.Focus();
                        return;
                    }
                    else
                    {
                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        IEnumerable<DataRow> rowsNew = DTabSubPacketLiveStock.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtSubPktKapanName.Text = string.Empty;
                            txtSubPktPacketNo.Text = string.Empty;
                            txtSubPktKapanName.Focus();
                            return;
                        }
                        // 07-06-2019

                        DataRow DRNew = DTabSubPacketLiveStock.NewRow();
                        foreach (DataColumn DCol in DTabSubPacketLiveStock.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }

                        DTabSubPacketLiveStock.Rows.Add(DRNew);
                        GrdDetSubPkt.SetRowCellValue(DTabSubPacketLiveStock.Rows.Count - 1, "COLSELECTCHECKBOX", true);
                    }
                    DRow = null;
                }

                GrdDetSubPkt.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDetSubPkt.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDetSubPkt.RefreshData();

                GrdDetSubPkt.BestFitMaxRowCount = 500;
                GrdDetSubPkt.BestFitColumns();
                MainGridSubPkt.Refresh();

                CalculateSummary();

                txtSubPktKapanName.Text = string.Empty;
                txtSubPktPacketNo.Text = string.Empty;
                txtBarcodeNo.Text = string.Empty;
                txtSubPktKapanName.Focus();

                this.Cursor = Cursors.Default;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void xtraTabControl2_Click(object sender, EventArgs e)
        {
            txtSubPktKapanName.Focus();


        }

        private void GrdDetMainPkt_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    string pStrKapanName = "";

                    pStrKapanName = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("KAPANNAME"));

                    FrmPacketHistoryView FrmPacketHistoryView = new FrmPacketHistoryView();
                    FrmPacketHistoryView.MdiParent = Global.gMainRef;
                    FrmPacketHistoryView.ShowForm(pStrKapanName);
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void RepBtnMakbleIssue_Click(object sender, EventArgs e)
        {
            try
            {
                string StrKapanName = "";
                Int64 StrKapan_ID = 0;

                string StrMainPacketNo = "";
                Int64 StrMainPacket_ID = 0;

                string pStrMarkerCode = "";
                Int64 pStrMarker_ID = 0;

                double pDouBalanceCts = 0;

                string pStrEmpCode = "";
                Int64 pIntEmp_ID = 0;

                string StrKapanManager = "";
                Int64 StrKapanManager_ID = 0;

                if (GrdDetMainPkt.FocusedRowHandle < 0)
                {
                    StrKapanName = string.Empty;
                    StrKapan_ID = 0;
                    StrMainPacketNo = string.Empty;
                    StrMainPacket_ID = 0;
                    pStrMarkerCode = string.Empty;
                    pStrMarker_ID = 0;
                    
                }
                else
                {
                    StrKapanName = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("KAPANNAME"));
                    StrKapan_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("KAPAN_ID"));

                    StrMainPacketNo = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("PACKETNO"));
                    StrMainPacket_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("PACKET_ID"));

                    pStrMarkerCode = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("MARKERCODE"));
                    pStrMarker_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("MARKER_ID"));

                    pDouBalanceCts = Val.Val(GrdDetMainPkt.GetFocusedRowCellValue("BALANCECARAT"));

                    pStrEmpCode = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("EMPLOYEECODE"));
                    pIntEmp_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("EMPLOYEE_ID"));

                    StrKapanManager = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("KAPANMAINMANAGER"));
                    StrKapanManager_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("KapanMainManager_ID"));
                 

                }
                FrmMakebalPacketCreateTest FrmMakebalPacketCreateTest = new FrmMakebalPacketCreateTest();
                FrmMakebalPacketCreateTest.MdiParent = Global.gMainRef;
                FrmMakebalPacketCreateTest.ShowForm(StrKapanName, StrKapan_ID, StrMainPacketNo, StrMainPacket_ID, pStrMarkerCode, pStrMarker_ID, pDouBalanceCts, pStrEmpCode, pIntEmp_ID, StrKapanManager, StrKapanManager_ID);
                FrmMakebalPacketCreateTest.FormClosing += new FormClosingEventHandler(Form_Closing);
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void repBtnSinglePacketCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetMainPkt.FocusedRowHandle < 0)
                {
                    return;
                }
                GrpSinglePacketCreate.Visible = true;
               // xtraTabControl2.Enabled = false;
                DataRow Dr = GrdDetMainPkt.GetFocusedDataRow();
                if (Dr != null)
                {
                    txtSnglPktCrtKapan.Text = Val.ToString(Dr["KAPANNAME"]);
                    txtSnglPktCrtKapan.Tag = Val.ToString(Dr["KAPAN_ID"]);
                    txtSnglPktCrtPktNo.Text = Val.ToString(Dr["PACKETNO"]);
                    txtSnglPktCrtPktNo.Tag = Val.ToString(Dr["PACKET_ID"]);
                    txtSnglPktCrtBalCts.Text = Val.ToString(Dr["BALANCECARAT"]);
                    txtSnglPktCrtTransCts.Focus();
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void BtnSinglePktCreateExit_Click(object sender, EventArgs e)
        {
            GrpSinglePacketCreate.Visible = false;

            txtSnglPktCrtKapan.Text = string.Empty;
            txtSnglPktCrtKapan.Tag = string.Empty;
            txtSnglPktCrtPktNo.Text = string.Empty;
            txtSnglPktCrtPktNo.Tag = string.Empty;
            txtSnglPktCrtBalCts.Text = string.Empty;
            txtSnglPktCrtTransCts.Text = string.Empty;
            xtraTabControl2.Enabled = true;
        }

        private void BtnSnglPktCrtClose_Click(object sender, EventArgs e)
        {
            BtnSinglePktCreateExit_Click(null, null);
        }

        private void BtnSinglePktCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.Val(txtSnglPktCrtTransCts.Text) <= 0)
                {
                    Global.MessageError("Please Enter Proper Transfer Carat..!");
                    txtSnglPktCrtTransCts.Focus();
                    return;
                }
                else if (Val.Val(txtSnglPktCrtTransCts.Text) > Val.Val(txtSnglPktCrtBalCts.Text))
                {
                    Global.MessageError("Transfer Carat Should Not Be Greater Than Balance Carat Please Check..!");
                    txtSnglPktCrtTransCts.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;


                TrnSinglePacketCreationProperty Property = new TrnSinglePacketCreationProperty();
                Property.KAPANNAME = txtSnglPktCrtKapan.Text;
                Property.KAPAN_ID = Val.ToInt64(txtSnglPktCrtKapan.Tag);
                Property.PACKETNO = Val.ToInt32(txtSnglPktCrtPktNo.Text);
                Property.PACKET_ID = Val.ToInt64(txtSnglPktCrtPktNo.Tag);
                Property.BALANCECARAT = Val.Val(txtSnglPktCrtBalCts.Text);
                Property.TRANSFERCARAT = Val.Val(txtSnglPktCrtTransCts.Text);
                Property = new BOTRN_PacketCreate().MixToSinglePacketCreate(Property);

                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnSnglPktCrtClose_Click(null, null);
                    BtnMainPacketSearch_Click(null, null);
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtSubPktJangedNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtSubPktJangedNo.Text.Trim().Length == 0)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;
                if (RbtSubPktDeptStock.Checked == true)
                {
                    StrType = RbtSubPktDeptStock.Tag.ToString();
                }
                else if (RbtSubPktFullStock.Checked == true)
                {
                    StrType = RbtSubPktFullStock.Tag.ToString();
                }
                else if (RbtSubPktMYStock.Checked == true)
                {
                    StrType = RbtSubPktMYStock.Tag.ToString();
                }
                else if (RbtSubPktOtherStock.Checked == true)
                {
                    StrType = RbtSubPktOtherStock.Tag.ToString();
                }

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDetSubPkt.RowCount; IntI++)
                {
                    DataRow DRow = GrdDetSubPkt.GetDataRow(IntI);
                    if (txtSubPktJangedNo.Text.Trim() == Val.ToString(DRow["JANGEDNO"]).Trim()
                        )
                    {
                        ISFind = true;
                        GrdDetSubPkt.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtSubPktKapanName.Text = string.Empty;
                        txtSubPktPacketNo.Text = string.Empty;

                        txtSubPktKapanName.Focus();
                        CalculateSummary();
                        //break;
                    }
                }

                if (ISFind == false)
                {
                    DataTable DTabJangedData = ObjKapan.GetDataForParcelPacketLiveStockJangedWiseInfo("SUBPACKET", "ALL", StrType, txtSubPktJangedNo.Text, false, ChkMainPktDisplayAll.Checked, ChkSubPktDisplayAll.Checked);
                    if (DTabJangedData == null || DTabJangedData.Rows.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;

                        Global.MessageError(txtSubPktKapanName.Text + "-" + txtSubPktPacketNo.Text + " Packet Not In Stock Kindly Check");
                        txtSubPktKapanName.Text = string.Empty;
                        txtSubPktPacketNo.Text = string.Empty;

                        txtSubPktKapanName.Focus();
                        return;
                    }
                    else
                    {

                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        var matched = from table1 in DTabSubPacketLiveStock.AsEnumerable()
                                      join table2 in DTabJangedData.AsEnumerable() on table1.Field<Int64>("JANGEDNO") equals table2.Field<Int64>("JANGEDNO")
                                      where table1.Field<Int64>("PACKET_ID") == table2.Field<Int64>("PACKET_ID")
                                      select table1;

                        if (matched.Any())
                        {
                            this.Cursor = Cursors.Default;
                            string Str = matched.FirstOrDefault()["KAPANNAME"].ToString() + "-" + matched.FirstOrDefault()["PACKETNO"].ToString();
                            Global.Message("This PacketNo Is Already Selected." + Str);
                            txtSubPktKapanName.Text = string.Empty;
                            txtSubPktPacketNo.Text = string.Empty;
                            txtSubPktKapanName.Focus();
                            return;
                        }

                        // 07-06-2019



                        //DataRow DRNew = DTabSubPacketLiveStock.NewRow();
                        //foreach (DataColumn DCol in DTabSubPacketLiveStock.Columns)
                        //{
                        //    DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        //}
                        //DTabSubPacketLiveStock.Rows.Add(DRNew);
                        //GrdDetSubPkt.SetRowCellValue(DTabSubPacketLiveStock.Rows.Count - 1, "COLSELECTCHECKBOX", true);
                        foreach (DataRow DR in DTabJangedData.Rows)
                        {
                            DataRow DRNew = DTabSubPacketLiveStock.NewRow();
                            foreach (DataColumn DCol in DTabSubPacketLiveStock.Columns)
                            {
                                DRNew[DCol.ColumnName] = DR[DCol.ColumnName];
                            }
                            DTabSubPacketLiveStock.Rows.Add(DRNew);
                        }
                        for (int IntI = 0; IntI < GrdDetSubPkt.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDetSubPkt.GetDataRow(IntI);
                            //if (txtKapanName.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                            //    && txtPacketNo.Text.Trim() == Val.ToString(DRowGrid["PACKETNO"]).Trim()
                            //    && txtTag.Text.Trim() == Val.ToString(DRowGrid["TAG"]).Trim()
                            //    )
                            if (txtSubPktJangedNo.Text.Trim() == Val.ToString(DRowGrid["JANGEDNO"]).Trim())
                            {
                                ISFind = true;
                                GrdDetSubPkt.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabSubPacketLiveStock.AcceptChanges();
                                //break;
                            }
                        }
                        GrdDetSubPkt.FocusedRowHandle = 0;
                    }
                    DTabJangedData = null;
                }

                GrdDetSubPkt.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDetSubPkt.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDetSubPkt.RefreshData();

                GrdDetSubPkt.BestFitMaxRowCount = 500;
                GrdDetSubPkt.BestFitColumns();
                MainGridSubPkt.Refresh();

                CalculateSummary();

                txtSubPktJangedNo.Text = string.Empty;
                txtSubPktJangedNo.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }

        }

        private void lblPrintMainPackets_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedKapanRows(GrdDetMainPkt, true);

                if (DTab.Rows.Count <= 0)
                {
                    Global.Message("Please Select Records That You Want To Print");
                    return;
                }

                MainGrdPrint.DataSource = DTab;
                GrdPrint.RefreshData();

                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGrdPrint;
                link.Landscape = false;

                link.Margins.Left = 10;
                link.Margins.Right = 10;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;

                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterAreaSummary);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }

        }
        public void Link_CreateMarginalHeaderAreaSummary(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("Parcel Main Packets Summary", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 250, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;
        }
        public void Link_CreateMarginalFooterAreaSummary(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("verdana", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void MainGridMainPkt_Click(object sender, EventArgs e)
        {

        }

    }
}
