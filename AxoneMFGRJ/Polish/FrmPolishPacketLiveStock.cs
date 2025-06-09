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
using BusLib.Polish;
using AxoneMFGRJ.Parcel;

namespace AxoneMFGRJ.Polish
{
    public partial class FrmPolishPacketLiveStock : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelectionForKapan;
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_PolishTransaction ObjPolish = new BOTRN_PolishTransaction();
        BOTRN_SinglePolishOKTransfer ObjMast = new BOTRN_SinglePolishOKTransfer();

        DataTable DtabPacket = new DataTable();
        DataTable DTabMainPacketLiveStock = new DataTable();
        DataTable DTabSubPacketLiveStock = new DataTable();
        DataTable DTabFinal = new DataTable();
        BOFormPer ObjPer = new BOFormPer();

        DataTable DTabTransfer = new DataTable();
        string pStrTransferToName = string.Empty;

        System.Diagnostics.Stopwatch watch = null;

        string mStrPassward = "";

        #region Property Settings

        public FrmPolishPacketLiveStock()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();



            DTabSubPacketLiveStock = ObjPolish.GetDataForPolishPacketLiveStock("", TxtManager.Text, TxtKapan.Text, "FULLSTOCK", 0, false, false);
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

            if (MainGridMainPkt.RepositoryItems.Count == 1)
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

            RbtFullStock_CheckedChanged(null, null);

            DTabFinal = new DataTable();
            DTabFinal.Columns.Add(new DataColumn("KAPAN_ID", typeof(Int32)));
            DTabFinal.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DTabFinal.Columns.Add(new DataColumn("MANAGER_ID", typeof(Int32)));
            DTabFinal.Columns.Add(new DataColumn("MANAGERNAME", typeof(string)));
            DTabFinal.Columns.Add(new DataColumn("ORGPCS", typeof(Int32)));
            DTabFinal.Columns.Add(new DataColumn("ORGCARAT", typeof(double)));
            DTabFinal.Columns.Add(new DataColumn("BALANCEPCS", typeof(Int32)));
            DTabFinal.Columns.Add(new DataColumn("BALANCECARAT", typeof(double)));
            DTabFinal.Columns.Add(new DataColumn("DIFFCARAT", typeof(double)));
            DTabFinal.Columns.Add(new DataColumn("SRNO", typeof(Int32)));
            DTabFinal.Columns.Add(new DataColumn("PACKETNO", typeof(Int32)));
            DTabFinal.Columns.Add(new DataColumn("POLISHPACKET_ID", typeof(Int64)));
            DTabFinal.Columns.Add(new DataColumn("EMPLOYEE_ID", typeof(Int64)));
            DTabFinal.Columns.Add(new DataColumn("EMPLOYEENAME", typeof(string)));

            // Action Button Rights And Stock Permission as per User Rights
            EmployeeActionRightsProperty Property = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

            Property = null;

            // END : DHARA : 21-04-22

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            mStrPassward = ObjPer.PASSWORD;
            txtPassForDisplayBack_TextChanged(null, null);

            this.Show();


            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDetSubPkt.Name);

            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDetSubPkt.RestoreLayoutFromStream(stream);

            }
            // BtnSinglePktCreateExit_Click(null, null);
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
                //BtnSubPktAckPending.PerformClick();
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

        private DataTable GetTableOfSelectedSubPktRows(GridView view, Boolean IsSelect)
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


        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PacketLiveStock", GrdDetSubPkt);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            // BtnSearch_Click(null, null);
        }




        private void FormTransfer_Closing(object sender, FormClosingEventArgs e)
        {
            DTabSubPacketLiveStock.Rows.Clear();
            ObjGridSelection.ClearSelection();
            txtSubPktKapanName.Focus();
            //BtnSearch_Click(null, null);
        }


        private void txtTag_Validated(object sender, EventArgs e)
        {

        }

        private void BtnReturnWithSplit_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable DTab = GetTableOfSelectedSubPktRows(GrdDetSubPkt, true);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }


                foreach (DataRow DRow in DTab.Rows)
                {
                    if (Val.ToString(DRow["ENTRYTYPE"]) == "EMPRET")
                    {
                        Global.Message("This Packet is Already Return To " + Val.ToString(DRow["EMPLOYEENAME"]) + ", You Can't Return PacketNo : " + Val.ToString(DRow["KAPANNAME"]) + '-' + Val.ToString(DRow["PACKETNO"]));
                        return;
                    }
                    if (Val.Val(DRow["BALANCECARAT"]) == 0)
                    {
                        Global.Message("You Have Not Enough Balance For This Action ? PacketNo : " + Val.ToString(DRow["JANGEDNO"]).Replace("\n", " - "));
                        return;
                    }
                }

                FrmPolishGoodsTransfer FrmPolishGoodsTransfer = new FrmPolishGoodsTransfer();
                FrmPolishGoodsTransfer.MdiParent = Global.gMainRef;
                FrmPolishGoodsTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmPolishGoodsTransfer.ShowForm(DTab);
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


        private void GrdDet_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle < 0)
            //{
            //    return;
            //}

            //DataRow DR = GrdDetSubPkt.GetDataRow(e.RowHandle);
            //if (Val.ISDate(DR["CONFDATE"]) == true)
            //{
            //    e.Appearance.BackColor = lblConfiredGoods.BackColor;
            //}
            //else if (Val.ISDate(DR["CONFDATE"]) == false)
            //{
            //    e.Appearance.BackColor = lblPendingsGoods.BackColor;
            //}

        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDetSubPkt.BestFitMaxRowCount = 5000000;
            GrdDetSubPkt.BestFitColumns();
            this.Cursor = Cursors.Default;
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


        private void GrdDet_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = string.Empty;
            }
        }

        private void GrdDet_KeyUp(object sender, KeyEventArgs e)
        {
            // CalculateSummary();
        }

        private void GrdDet_MouseUp(object sender, MouseEventArgs e)
        {
            // CalculateSummary();
        }

        private void BtnMainPacketSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string StrManager = "";
                this.Cursor = Cursors.WaitCursor;
                string StrType = string.Empty;
                string pStrDisplayType = string.Empty;

                if (ChkTransaction.Checked == true)
                {
                    BtnTransfer.Visible = false;
                    BtnKapanMerge.Visible = false;
                    BtnMereg.Visible = false;
                }
                else
                {
                    BtnTransfer.Visible = true;
                    BtnKapanMerge.Visible = true;
                    BtnMereg.Visible = true;
                }

                if (RbtFullStock.Checked == true)
                {
                    pStrDisplayType = "FULLSTOCK";
                }
                else
                {
                    pStrDisplayType = "MYSTOCK";
                }


                Int64 pStrEmp_Id = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                DTabMainPacketLiveStock.Rows.Clear();
                ObjGridSelection.ClearSelection();
                StrManager = Val.ToString(TxtManager.Text).Trim().Equals(string.Empty) ? "" : Val.Trim(TxtManager.Tag);

                DTabMainPacketLiveStock = ObjPolish.GetDataForPolishPacketLiveStock("MAINPACKET", StrManager, TxtKapan.Text, pStrDisplayType, pStrEmp_Id, ChkTransaction.Checked, chkIsMerge.Checked);
                MainGridMainPkt.DataSource = DTabMainPacketLiveStock;
                GrdDetMainPkt.BestFitColumns();
                MainGridMainPkt.Refresh();

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
                string StrKapanName = "";
                Int64 StrKapan_ID = 0;
                Int64 StrPolishPacketID = 0;

                string StrManager = "";
                Int64 StrManager_ID = 0;
                Int64 pIntEmployee_ID = 0;
                if (GrdDetMainPkt.FocusedRowHandle < 0)
                {
                    StrKapanName = string.Empty;
                    StrKapan_ID = 0;
                    StrManager = string.Empty;
                    StrManager_ID = 0;
                    StrPolishPacketID = 0;
                }
                else
                {
                    StrPolishPacketID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("POLISHPACKET_ID"));

                    StrKapanName = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("KAPANNAME"));
                    StrKapan_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("KAPAN_ID"));

                    StrManager = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("MANAGERNAME"));
                    StrManager_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("MANAGER_ID"));

                    pIntEmployee_ID = Val.ToInt64(GrdDetMainPkt.GetFocusedRowCellValue("Employee_ID"));

                }
                if (StrPolishPacketID != 0)
                {
                    FrmPolishIssueWithPackets FrmPolishIssueWithPackets = new FrmPolishIssueWithPackets();
                    FrmPolishIssueWithPackets.MdiParent = Global.gMainRef;
                    FrmPolishIssueWithPackets.ShowForm(StrKapanName, StrKapan_ID, StrManager, StrManager_ID, StrPolishPacketID, pIntEmployee_ID);
                    FrmPolishIssueWithPackets.FormClosing += new FormClosingEventHandler(Form_Closing);
                }
            }

            catch (Exception Ex)
            {

            }
        }

        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
            //if (Val.ToString(txtPassForDisplayBack.Text) != "" && Val.ToString(txtPassForDisplayBack.Text).ToUpper() == Val.ToString(mStrPassward).ToUpper())
            //{
            //    GrdDetMainPkt.Columns["BALANCECARAT"].OptionsColumn.AllowEdit = true;
            //}
            //else
            //{
            //    GrdDetMainPkt.Columns["BALANCECARAT"].OptionsColumn.AllowEdit = false;
            //}
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

        private void xtraTabControl2_Click(object sender, EventArgs e)
        {
            txtSubPktKapanName.Focus();


        }

        private void GrdDetMainPkt_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.Column.FieldName == "KAPANNAME")
                {
                    string pStrKapanName = "";

                    pStrKapanName = Val.ToString(GrdDetMainPkt.GetFocusedRowCellValue("KAPANNAME"));

                    FrmPolishPacketHistory FrmPolishPacketHistory = new FrmPolishPacketHistory();
                    FrmPolishPacketHistory.MdiParent = Global.gMainRef;
                    FrmPolishPacketHistory.ShowForm(pStrKapanName);
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }


        private void lblPrintMainPackets_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDetMainPkt, true);

                if (DTab.Rows.Count <= 0)
                {
                    Global.Message("Please Select Records That You Want To Print");
                    return;
                }

                //MainGrdPrint.DataSource = DTab;
                //GrdPrint.RefreshData();

                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                // link.Component = MainGrdPrint;
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

        private void TxtManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "EMPLOYEE_ID";
                    FrmSearch.DisplayMemeter = "EMPLOYEECODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        TxtManager.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        TxtManager.Tag = Val.ToString(FrmSearch.SelectedValuemember);
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

        private void TxtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.Transaction.BOTRN_SinglePacketCreate().FindKapan();

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtKapan.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
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

        private void BtnSubPacketSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string StrManager = "";
                this.Cursor = Cursors.WaitCursor;
                string StrType = string.Empty;

                string pStrDisplayType = string.Empty;

                if (RbtSubFullStock.Checked == true)
                {
                    pStrDisplayType = "FullStock";
                }
                else
                {
                    pStrDisplayType = "MyStock";
                }

                Int64 pIntEMP_ID = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;

                DTabSubPacketLiveStock.Rows.Clear();
                ObjGridSelection.ClearSelection();
                StrManager = Val.ToString(TxtManager.Text).Trim().Equals(string.Empty) ? "" : Val.Trim(TxtManager.Tag);

                DTabSubPacketLiveStock = ObjPolish.GetDataForPolishPacketLiveStock("", StrManager, txtSubPktKapanName.Text, pStrDisplayType, pIntEMP_ID, ChkTransaction.Checked, chkIsMerge.Checked);
                MainGridSubPkt.DataSource = DTabSubPacketLiveStock;
                GrdDetSubPkt.BestFitColumns();
                MainGridSubPkt.Refresh();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {

            }
        }

        private void MainGridMainPkt_Click(object sender, EventArgs e)
        {

        }

        private void BtnKapanMerge_Click(object sender, EventArgs e)
        {
            try
            {

                if (Global.Confirm("Are You Sure You Want To Merge ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                GrpMerge.Visible = true;
                btnMergeToClear_Click(null, null);
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }

        }

        #region Button Event

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DTabTransfer.Rows.Clear();
                DTabTransfer = GetTableOfSelectedRows(GrdDetMainPkt, true);
                if (DTabTransfer.Rows.Count == 0)
                {
                    Global.Message("Please Select At List One Record For Transfer");
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (Global.Confirm("Are You Sure You Want To Transfer This Packet ? ") == System.Windows.Forms.DialogResult.No)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
                GrpTransferTo.Visible = true;
                txtTransferTo.Focus();
            }
            catch (Exception Ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(Ex.Message.ToString());
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PolishIssueReturnProperty Property = new PolishIssueReturnProperty();
                DataTable DTab = DTabTransfer.Copy();

                DTab.Columns.Add("SRNO", typeof(Int32));

                int IntCount = 1;
                foreach (DataRow DRow in DTab.Rows)
                {
                    DRow["SRNO"] = IntCount;
                    IntCount = IntCount + 1;
                }
                DTab.AcceptChanges();

                DTab.TableName = "Table1";
                string StrXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    if (DTab != null)
                    {
                        DTab.WriteXml(sw);
                        StrXML = sw.ToString();
                    }
                }

                Property.TOEMPLOYEE_ID = Val.ToInt64(txtTransferTo.Tag);
                Property.TOMANAGER_ID = Val.ToInt64(txtTransferToManager.Tag);
                Property.TODEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
                

                Property = ObjMast.Update(StrXML, "", Property);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    txtPrintJangedNo.Text = Property.JANGEDNO.ToString();
                    Global.Message(Property.ReturnMessageDesc);
                    DTab.Columns.Add(new DataColumn("TOEMPLOYEENAME", typeof(string)));
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DRow["TOEMPLOYEENAME"] = pStrTransferToName;
                    }
                    DTab.AcceptChanges();
                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowForm("SinglePolishOKTransferPrint", DTab);
                    this.Cursor = Cursors.Default;
                    
                    ObjGridSelectionForKapan.ClearSelection();
                    BtnMainPacketSearch_Click(null, null);
                    BtnGroupBoxClose_Click(null,null);
                }
                Property = null;

                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(Ex.Message.ToString());
            }
        }

        #endregion

        #region Other Operation

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
                        pStrTransferToName = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);

                        txtDepartment.AccessibleDescription = Val.ToString(FrmSearch.mDRow["DEPARTMENTGROUP"]);

                        txtTransferToManager.Text = Val.ToString(FrmSearch.mDRow["MANAGERNAME"]);
                        txtTransferToManager.Tag = Val.ToString(FrmSearch.mDRow["MANAGER_ID"]);

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

        #endregion

        private void BtnGroupBoxClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Default;
                GrpTransferTo.Visible = false;
                BtnClear_Click(null, null);
            }
            catch (Exception Ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(Ex.Message.ToString());
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtTransferToManager.Text = string.Empty;
                txtTransferToManager.Tag = string.Empty;
                txtTransferTo.Text = string.Empty;
                txtTransferTo.Tag = string.Empty;
                txtDepartment.Text = string.Empty;
                txtDepartment.Tag = string.Empty;
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message.ToString());
            }
        }

        private void btnMergeToExit_Click(object sender, EventArgs e)
        {
            GrpMerge.Visible = false;
        }

        private void btnMergeToClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtMeregToManager.Text = string.Empty;
                txtMeregToManager.Tag = string.Empty;
                txtMergeTo.Text = string.Empty;
                txtMergeTo.Tag = string.Empty;
                txtMergeToDepartment.Text = string.Empty;
                txtMergeToDepartment.Tag = string.Empty;
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message.ToString());
            }
        }

        private void BtnMeregToSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                if (txtMergeTo.Text.Trim().Length == 0)
                {
                    Global.Message("Employee Name Is Required");
                    txtMergeTo.Focus();
                    return;
                }

                if (txtMeregToManager.Text.Trim().Length == 0)
                {
                    Global.Message("Manager Name Is Required");
                    txtMeregToManager.Focus();
                    return;
                }

                if (txtMergeToDepartment.Text.Trim().Length == 0)
                {
                    Global.Message("Department Name Is Required");
                    txtMergeToDepartment.Focus();
                    return;
                }

                DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDetMainPkt, true, ObjGridSelectionForKapan);

                DataTable DTabDistinct = DTab.DefaultView.ToTable(true, "KAPAN_ID", "KAPANNAME", "MANAGER_ID", "MANAGERNAME");

                if (DTabDistinct.Rows.Count > 1)
                {
                    Global.Message("Please Check Kapan Or Manager matbe not same");
                    return;
                }


                DTabFinal.Rows.Clear();

                foreach (DataRow DROW in DTabDistinct.Rows)
                {
                    string StrKapan = Val.ToString(DROW["KAPANNAME"]);
                    string StrEmp = Val.ToString(DROW["MANAGERNAME"]);
                    int StrKapan_ID = Val.ToInt32(DROW["KAPAN_ID"]);
                    int StrManager_ID = Val.ToInt32(DROW["MANAGER_ID"]);

                    int IntPcs = Val.ToInt(DTab.Compute("sum(BALANCEPCS)", "KAPANNAME = '" + StrKapan + "' AND MANAGERNAME = '" + StrEmp + "'"));
                    double DouCarat = Val.Val(DTab.Compute("sum(BALANCECARAT)", "KAPANNAME = '" + StrKapan + "' AND MANAGERNAME = '" + StrEmp + "'"));

                    DataRow DRNew = DTabFinal.NewRow();

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
                    DRNew["MANAGERNAME"] = StrEmp;
                    DRNew["ORGPCS"] = IntPcs;
                    DRNew["ORGCARAT"] = DouCarat;
                    DRNew["BALANCEPCS"] = IntPcs;
                    DRNew["BALANCECARAT"] = DouCarat;
                    DRNew["PACKETNO"] = Property.RETURNVALUEMAXPACKETNO;

                    DTabFinal.Rows.Add(DRNew);
                    Property = null;
                }


                this.Cursor = Cursors.WaitCursor;

                PolishIssueReturnProperty PropertySave = new PolishIssueReturnProperty();

                DTab.TableName = "Table1";
                string StrXMLSelection = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    if (DTab != null)
                    {
                        DTab.WriteXml(sw);
                        StrXMLSelection = sw.ToString();
                    }
                }

                DTabFinal.TableName = "Table";
                string StrXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    if (DTabFinal != null)
                    {
                        DTabFinal.WriteXml(sw);
                        StrXML = sw.ToString();
                    }
                }

                PropertySave.TOEMPLOYEE_ID = Val.ToInt64(txtMergeTo.Tag);
                PropertySave.TOMANAGER_ID = Val.ToInt64(txtMeregToManager.Tag);
                PropertySave.TODEPARTMENT_ID = Val.ToInt32(txtMergeToDepartment.Tag);

                PropertySave = ObjMast.SaveAndMergePacket(StrXML, StrXMLSelection, PropertySave);
                this.Cursor = Cursors.Default;

                ReturnMessageDesc = PropertySave.ReturnMessageDesc;
                ReturnMessageType = PropertySave.ReturnMessageType;

                PropertySave = null;

                if (ReturnMessageType == "SUCCESS")
                {
                    this.Cursor = Cursors.Default;
                    Global.Message(ReturnMessageDesc);
                    ObjGridSelectionForKapan.ClearSelection();
                    DTab.Rows.Clear();
                    GrpMerge.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
                this.Cursor = Cursors.Default;
            }

        }

        private void txtMergeTo_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtMergeTo.Text = Val.ToString(FrmSearch.mDRow["LEDGERCODE"]);
                        txtMergeTo.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                        txtMergeToDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtMergeToDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);

                        txtMergeToDepartment.AccessibleDescription = Val.ToString(FrmSearch.mDRow["DEPARTMENTGROUP"]);

                        txtMeregToManager.Text = Val.ToString(FrmSearch.mDRow["MANAGERNAME"]);
                        txtMeregToManager.Tag = Val.ToString(FrmSearch.mDRow["MANAGER_ID"]);

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

        private void RbtFullStock_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtMYStock.Checked == false)
            {
                BtnMereg.Visible = false;
            }
            else
            {
                BtnMereg.Visible = true;
            }
        }

        private void RbtMYStock_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtMYStock.Checked == false)
            {
                BtnMereg.Visible = false;
            }
            else
            {
                BtnMereg.Visible = true;
            }
        }

        private void FormKapanInward_Closing(object sender, FormClosingEventArgs e)
        {
            DTabMainPacketLiveStock.Rows.Clear();
            ObjGridSelection.ClearSelection();
            TxtKapan.Focus();
        }
        

        private void BtnKapanMerge_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDetMainPkt, true, ObjGridSelectionForKapan);

                if(DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select At List One Record");
                    return;
                }

                if (Global.Confirm("Are You Sure You Want To Merge ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                DataTable DTabDistinct = DTab.DefaultView.ToTable(true, "KAPAN_ID", "KAPANNAME", "EMPLOYEE_ID", "EMPLOYEENAME");

                if (DTabDistinct.Rows.Count > 1)
                {
                    Global.Message("Please Check Kapan Or Manager matbe not same");
                    return;
                }

                DTabFinal.Rows.Clear();

                PolishIssueReturnProperty Property = new PolishIssueReturnProperty();

                foreach (DataRow DROW in DTabDistinct.Rows)
                {
                    string StrKapan = Val.ToString(DROW["KAPANNAME"]);
                    string StrEmp = Val.ToString(DROW["EMPLOYEENAME"]);
                    int StrKapan_ID = Val.ToInt32(DROW["KAPAN_ID"]);
                    int StrEmployee_ID = Val.ToInt32(DROW["EMPLOYEE_ID"]);

                    int IntPcs = Val.ToInt(DTab.Compute("sum(BALANCEPCS)", "KAPANNAME = '" + StrKapan + "' AND EMPLOYEENAME = '" + StrEmp + "'"));
                    double DouCarat = Val.Val(DTab.Compute("sum(BALANCECARAT)", "KAPANNAME = '" + StrKapan + "' AND EMPLOYEENAME = '" + StrEmp + "'"));

                    DataRow DRNew = DTabFinal.NewRow();

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
                    DRNew["EMPLOYEE_ID"] = StrEmployee_ID;
                    DRNew["EMPLOYEENAME"] = StrEmp;
                    DRNew["ORGPCS"] = IntPcs;
                    DRNew["ORGCARAT"] = DouCarat;
                    DRNew["BALANCEPCS"] = IntPcs;
                    DRNew["BALANCECARAT"] = DouCarat;
                    DRNew["PACKETNO"] = Property.RETURNVALUEMAXPACKETNO;

                    DTabFinal.Rows.Add(DRNew);
                    Property = null;

                    FrmKapanInward FrmKapanInward = new FrmKapanInward();
                    FrmKapanInward.MdiParent = Global.gMainRef;
                   // FrmKapanInward.FormClosing += new FormClosingEventHandler(FormKapanInward_Closing);
                    FrmKapanInward.ShowForm(DTabFinal, DTab);
                }

                

            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void ChkTransaction_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void txtPrintJangedNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "JANGEDNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_POLISHJANGEDNO);
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPrintJangedNo.Text = Val.ToString(FrmSearch.mDRow["JANGEDNO"]);
                        DataTable DTabPrint = ObjMast.PolishOKTransferPrint(Val.ToInt64(txtPrintJangedNo.Text));

                        Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                        FrmReportViewer.MdiParent = Global.gMainRef;
                        FrmReportViewer.ShowForm("SinglePolishOKTransferPrint", DTabPrint);
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
    }
}
