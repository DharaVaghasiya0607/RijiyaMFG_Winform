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
using DevExpress.XtraGrid.Views.BandedGrid;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmRoughPurchaseMixSplitView : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        DataTable DTabMaster = new DataTable();
        DataTable DTabDetail = new DataTable();
        
        bool pBoolIsComplete = true;

        #region Property Settings

        public FrmRoughPurchaseMixSplitView()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            
            BtnSearch_Click(null, null);
            this.Show();

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


        private void BtnExport_Click(object sender, EventArgs e)
        {
            DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

            PrinterSettingsUsing pst = new PrinterSettingsUsing();

            PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);

            //Lesson2 link = new Lesson2(PrintSystem);
            PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

            GrdDet.OptionsPrint.AutoWidth = true;
            GrdDet.OptionsPrint.UsePrintStyles = true;

            link.Component = MainGrid;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;

            link.Margins.Left = 40;
            link.Margins.Right = 40;
            link.Margins.Bottom = 40;
            link.Margins.Top = 130;

            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);

            link.CreateDocument();

            link.ShowPreview();
            link.PrintDlg();
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString("Item Group List", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("Verdana", 11, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;
        }

        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("Verdana", 11, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BtnSearch_Click(null, null);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DTabMaster.Rows.Clear();
            DTabDetail.Rows.Clear();
            
            GrdDet.BeginUpdate();

            DTabMaster = ObjMast.GetMixSplitHistory("SUMMARY", Val.SqlDate(DTPFromInvoiceDate.Value.ToShortDateString()), Val.SqlDate(DTPToInvoiceDate.Value.ToShortDateString()), 0);

            MainGrid.DataSource = DTabMaster;
            MainGrid.Refresh();

            GrdDet.BestFitMaxRowCount = 200;
            GrdDet.BestFitColumns();
          
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
            GrdDet.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAutoFit_Click(object sender, EventArgs e)
        {
            GrdDet.BestFitColumns();
            GrdDetDetail.BestFitColumns();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("MixSplitLiveStock", GrdDet);
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = string.Empty;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

            if (Global.Confirm("Are You Sure You Want To Delete This Entry ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            DataTable DtInvDetail = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

            if (DtInvDetail.Rows.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select AtLeast One Record For This Transaction");
                return;
            }

            if (DtInvDetail.Rows.Count > 1)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select Only One Transaction Record");
                return;
            }


            TrnRoughPurchaseProperty Property = new TrnRoughPurchaseProperty();
            Property.TRN_ID = Val.ToInt64(DtInvDetail.Rows[0]["TRN_ID"]);
            Property = ObjMast.MixSplitHistoryDelete(Property);
            this.Cursor = Cursors.Default;
            if (Property.ReturnMessageType == "SUCCESS")
            {
               Global.Message(Property.ReturnMessageDesc);
                BtnSearch_Click(null, null);
            }
            else if (Property.ReturnMessageType == "FAIL")
            {
                Global.MessageError(Property.ReturnMessageDesc);
            }
        }

        private void GrdDet_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            FetchRow(Val.ToInt64(GrdDet.GetRowCellValue(e.RowHandle, "TRN_ID")));
        }

        public void FetchRow(Int64 pIntTrnID)
        {
            this.Cursor = Cursors.WaitCursor;

            GrdDetDetail.BeginUpdate();

            DTabDetail = ObjMast.GetMixSplitHistory("DETAIL", null, null, pIntTrnID);

            MainGridDetail.DataSource = DTabDetail;
            MainGridDetail.Refresh();

            GrdDetDetail.BestFitMaxRowCount = 200;
            GrdDetDetail.BestFitColumns();

            GrdDetDetail.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void GrdDet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                return;
            }
            FetchRow(Val.ToInt64(GrdDet.GetRowCellValue(e.FocusedRowHandle, "TRN_ID")));
        }


    }
}
