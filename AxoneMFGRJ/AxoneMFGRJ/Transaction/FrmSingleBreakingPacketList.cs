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
using AxoneMFGRJ.Transaction;
using DevExpress.XtraPrintingLinks;
using System.Drawing.Printing;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleBreakingPacketList : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        DataTable DTabBrkList = new DataTable();

        BOTRN_SingleBreakingPacketEntry ObjBrk = new BOTRN_SingleBreakingPacketEntry();

        DataTable DtabPacket = new DataTable();
        DataTable DTabPacketLiveStock = new DataTable();

        #region Property Settings

        public FrmSingleBreakingPacketList()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();

            ChkCmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            ChkCmbKapan.Properties.DisplayMember = "KAPANNAME";
            ChkCmbKapan.Properties.ValueMember = "KAPANNAME";
            ChkCmbKapan.Focus();

            ChkCmbEmployee.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
            ChkCmbEmployee.Properties.DisplayMember = "EMPLOYEECODE";
            ChkCmbEmployee.Properties.ValueMember = "EMPLOYEE_ID";

            ChkCmbBrkType.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_BREAKINGTYPE);
            ChkCmbBrkType.Properties.DisplayMember = "BREAKINGTYPECODE";
            ChkCmbBrkType.Properties.ValueMember = "BREAKINGTYPE_ID";

            GrdDetList.BeginUpdate();
            DTabBrkList = ObjBrk.GetBreakingList("", 0, 0, txtTag.Text, "", "", "", "");
            DTabBrkList.Rows.Clear();
            MainGrdList.DataSource = DTabBrkList;
            MainGrdList.Refresh();
            GrdDetList.EndUpdate();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjBrk);
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
            //if (e.KeyCode == Keys.F5)
            //{
            //    BtnSearch_Click(null, null);
            //}
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string StrKapan = "";
                Int32 FromPacketNo = 0;
                Int32 ToPacketNo = 0;
                string StrBreakingType_ID = "";
                string StrEmployee_ID = "";
                string StrFromDate = "";
                string StrToDate = "";


                StrKapan = Val.Trim(ChkCmbKapan.Properties.GetCheckedItems());
                StrBreakingType_ID = Val.Trim(ChkCmbBrkType.Properties.GetCheckedItems());
                StrEmployee_ID = Val.Trim(ChkCmbEmployee.Properties.GetCheckedItems());
                FromPacketNo = Val.ToInt32(txtFromPacketNo.Text);
                ToPacketNo = Val.ToInt32(txtToPacketNo.Text);

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                DTabBrkList = ObjBrk.GetBreakingList(StrKapan, FromPacketNo, ToPacketNo, txtTag.Text, StrEmployee_ID, StrBreakingType_ID, StrFromDate, StrToDate);

                //if (DTabBrkList.Rows.Count <= 0)
                //{
                //    Global.Message("No Data Found.");
                //    this.Cursor = Cursors.Default;
                //    return;
                //}

                MainGrdList.DataSource = DTabBrkList;
                MainGrdList.Refresh();
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }


        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
        }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            BtnSearch_Click(null, null);
        }



        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSingleBreakingPacketEntryDetail FrmSingleBreakingPacketEntryDetail = new FrmSingleBreakingPacketEntryDetail();
                FrmSingleBreakingPacketEntryDetail.MdiParent = Global.gMainRef;
                FrmSingleBreakingPacketEntryDetail.Tag = "BreakingPacketEntry";
                FrmSingleBreakingPacketEntryDetail.ShowForm();
                FrmSingleBreakingPacketEntryDetail.FormClosing += new FormClosingEventHandler(FrmSingleBreakingPacketEntryDetail_FormClosing);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        private void FrmSingleBreakingPacketEntryDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            BtnSearch.PerformClick();
        }

        private void BtnAutoFit_Click(object sender, EventArgs e)
        {
            GrdDetList.BestFitColumns();
        }

        private void repBtnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetList.FocusedRowHandle < 0)
                    return;


                DataRow DR = GrdDetList.GetFocusedDataRow();

                string StrBreakingType_ID = "";
                string StrPacket_ID = "";
                Int64 IntEmployee_ID = 0;
                string StrKapan = "";
                string StrPacketNo = "";
                string StrTag = "";
                //string StrBreakingReason_ID = "";

                StrBreakingType_ID = Val.ToString(DR["BREAKINGTYPE"]);
                StrPacket_ID = Val.ToString(DR["PACKET_ID"]);
                IntEmployee_ID = Val.ToInt64(DR["EMPLOYEE_ID"]);

                StrKapan = Val.ToString(DR["KAPANNAME"]);
                StrPacketNo = Val.ToString(DR["PACKETNO"]);
                StrTag = Val.ToString(DR["MTAG"]);
                
                //StrBreakingReason_ID = Val.ToString(DR["BREAKINGREASON_ID"]);

                FrmSingleBreakingPacketEntryDetail FrmSingleBreakingPacketEntryDetail = new FrmSingleBreakingPacketEntryDetail();
                FrmSingleBreakingPacketEntryDetail.MdiParent = Global.gMainRef;
                FrmSingleBreakingPacketEntryDetail.Tag = "BreakingPacketEntry";
                FrmSingleBreakingPacketEntryDetail.ShowForm(StrPacket_ID, StrBreakingType_ID, IntEmployee_ID, StrKapan, StrPacketNo, StrTag);
                FrmSingleBreakingPacketEntryDetail.FormClosing += new FormClosingEventHandler(FrmSingleBreakingPacketEntryDetail_FormClosing);


            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            CommonExcelExportFuction(MainGrdList, GrdDetList, "BreakingEntryList");
        }

        public void CommonExcelExportFuction(DevExpress.XtraGrid.GridControl pMainGrid,DevExpress.XtraGrid.Views.Grid.GridView pGrdDet,string pStrFileName)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = pStrFileName;
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = pMainGrid,
                        Landscape = false,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [" + pStrFileName + ".xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("Breaking Entry List", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            string StrFilter = "FromDate : " + DTPFromDate.Text + " To " + DTPToDate.Text;
            TextBrick BrickTitlesParam = e.Graph.DrawString(StrFilter, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 250, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

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
                for (int IntI = 0; IntI < GrdDetList.RowCount; IntI++)
                {
                    DataRow DRow = GrdDetList.GetDataRow(IntI);
                    if (txtBarcode.Text.Trim() == Val.ToString(DRow["BARCODE"]).Trim())
                    {
                        ISFind = true;
                        GrdDetList.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();

                        GrdDetList.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {
                    this.Cursor = Cursors.WaitCursor;

                    DataTable DTab = ObjBrk.GetBreakingListPktInfo("", 0, 0, "", "", "", null, null, Val.ToInt64(txtBarcode.Text));
                    if (DTab == null)
                    {
                        this.Cursor = Cursors.Default;

                        Global.MessageError(" Packet Not In Stock Kindly Check\n\n");
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        return;
                    }
                    else
                    {

                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019

                        foreach (DataRow DRow in DTab.Rows)
                        {
                            IEnumerable<DataRow> rowsNew = DTabBrkList.Rows.Cast<DataRow>();
                            if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                            {
                                this.Cursor = Cursors.Default;
                                Global.Message("This Packet Is Already Selected.");
                                txtBarcode.Text = string.Empty;        
                                txtBarcode.Focus();
                                return;
                            }

                            // 07-06-2019

                            DataRow DRNew = DTabBrkList.NewRow();
                            foreach (DataColumn DCol in DTabBrkList.Columns)
                            {
                                DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                            }
                            DTabBrkList.Rows.Add(DRNew);


                            for (int IntI = 0; IntI < GrdDetList.RowCount; IntI++)
                            {
                                DataRow DRowGrid = GrdDetList .GetDataRow(IntI);
                                if (txtBarcode.Text.Trim() == Val.ToString(DRow["BARCODE"]).Trim())
                                {
                                    ISFind = true;
                                    GrdDetList.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                    DTabBrkList.AcceptChanges();
                                    txtBarcode.Text = string.Empty;
                                    txtBarcode.Focus();

                                    GrdDetList.FocusedRowHandle = 0;
                                    break;
                                }
                            }
                            GrdDetList.FocusedRowHandle = 0;
                        }
                    }
                }

                GrdDetList.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDetList.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDetList.FocusedRowHandle = 0;
                GrdDetList.RefreshData();
                MainGrdList.Refresh();

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

    }
}
