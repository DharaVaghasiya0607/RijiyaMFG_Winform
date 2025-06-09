using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
using BusLib.Transaction;
using DevExpress.XtraPrinting;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
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

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmRoughPurchaseRolling : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        DataTable DtabSalary = new DataTable();
        DataTable DTabPurchase = new DataTable();



        #region Property Settings

        public FrmRoughPurchaseRolling()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();



            //MainGrid.DataSource = DtabSalary;
            //MainGrid.RefreshDataSource();
            string currentMonth = DateTime.Now.Month.ToString();
            string currentYear = DateTime.Now.Year.ToString();

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
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnExit_Click(null, null);
            //}
            if (e.KeyCode == Keys.F5)
            {
                BtnSearch_Click(null, null);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            TrnRoughPurchaseProperty Property = new TrnRoughPurchaseProperty();

            DTabPurchase = ObjMast.GetDataForPurchaseLiveStock("",ChkDisplayAllLot.Checked, null, "");
            MainGrid.DataSource = DTabPurchase;

            GrdDet.Columns["STATUS"].Group();

            if (GrdDet.GroupSummary.Count == 0)
            {
                GrdDet.Columns["PCS"].Summary.Add(SummaryItemType.Sum, "PCS", "{0:N0}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "PCS", GrdDet.Columns["PCS"], "{0:N0}");

                GrdDet.Columns["CARAT"].Summary.Add(SummaryItemType.Sum, "CARAT", "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "CARAT", GrdDet.Columns["CARAT"], "{0:N3}");

                GrdDet.Columns["DUEDAYS"].Summary.Add(SummaryItemType.Sum, "DUEDAYS");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "DUEDAYS", GrdDet.Columns["DUEDAYS"]);

                GrdDet.Columns["ROUGHSTATUSDATE"].Summary.Add(SummaryItemType.Count, "ROUGHSTATUSDATE");
                GrdDet.GroupSummary.Add(SummaryItemType.Count, "ROUGHSTATUSDATE", GrdDet.Columns["ROUGHSTATUSDATE"]);

                GrdDet.Columns["SIZEAVG"].Summary.Add(SummaryItemType.Sum, "SIZEAVG");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "SIZEAVG", GrdDet.Columns["SIZEAVG"]);

                GrdDet.Columns["NETAMOUNT"].Summary.Add(SummaryItemType.Sum, "NETAMOUNT", "{0:N2}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "NETAMOUNT", GrdDet.Columns["NETAMOUNT"], "{0:N2}");

                GrdDet.Columns["ROUGHCOST"].Summary.Add(SummaryItemType.Sum, "ROUGHCOST", "{0:N2}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "ROUGHCOST", GrdDet.Columns["ROUGHCOST"], "{0:N2}");
            }

            GrdDet.ExpandAllGroups();

            GrdDet.FocusedRowHandle = 0;
            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.BestFitMaxRowCount = 200;
            GrdDet.BestFitColumns();
            GrdDet.Focus();
            MainGrid.Refresh();
            this.Cursor = Cursors.Default;

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCreateKapan_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure To Create New Kapan ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                DataRow DRow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);


                FrmKapanCreation FrmKapanCreation = new FrmKapanCreation();
                //FrmKapanCreation.ShowForm(Val.ToInt64(DRow["LOT_ID"]), FrmKapanCreation.FORMTYPE.KAPAN);
                //FrmKapanCreation.ShowForm(Val.ToInt64(DRow["LOT_ID"]), Val.ToString(DRow["PARTYNAME"]), Val.ToString(DRow["PARTYINVOICENO"]), Transaction.FrmKapanCreation.FORMTYPE.ORIGINALKAPAN);

                //  DRow = null;
                BtnSearch_Click(null, null);
            }
            catch (System.Exception ex)
            {
                Global.Message(ex.Message);

            }
        }

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (e.Clicks == 2)
            {
                Int64 LOT_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("LOT_ID"));

                if (e.Column.FieldName == "KAPANCARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("KAPAN", LOT_ID,"");

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Kapan List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;

                    FrmPopupGrid.Width = 1000;
                    
                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
                else if (e.Column.FieldName == "REJECTIONCARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("IMPORT", LOT_ID, "");

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                  
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Rejection List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;

                    FrmPopupGrid.Width = 1000;
                   
                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }


            }
        }

        private void BtnAutoFit_Click(object sender, EventArgs e)
        {
            GrdDet.BestFitColumns();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PurchaseLiveStock", GrdDet);
        }

        private void BtnRejectionTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure To Rejection Transfer ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                DataRow DRow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);


                FrmRejectionTransfer FrmRejectionTransfer = new FrmRejectionTransfer();
                //FrmKapanCreation.ShowForm(Val.ToInt64(DRow["LOT_ID"]), FrmKapanCreation.FORMTYPE.KAPAN);
                FrmRejectionTransfer.ShowForm(Val.ToString(DRow["LOT_ID"]), Val.ToString(DRow["PARTYNAME"]), Val.ToString(DRow["PARTYINVOICENO"]), "INVOICE");
                FrmRejectionTransfer.MdiParent = Global.gMainRef;
                FrmRejectionTransfer.FormClosing += new FormClosingEventHandler(Form_Closing);

                //  DRow = null;
                
            }
            catch (System.Exception ex)
            {
                Global.Message(ex.Message);

            }
        }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            BtnSearch_Click(null, null);
        }

        private void FrmPurchaseLiveStock_Load(object sender, EventArgs e)
        {
           
        }

        private void ChkDisplayMakPol_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void BtnPCNKapanCreate_Click(object sender, EventArgs e) // Add : Pinali : 12-10-2019
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                FrmKapanCreation FrmKapanCreation = new FrmKapanCreation();
                //FrmKapanCreation.MdiParent = this;
                //FrmKapanCreation.Tag = ((ToolStripMenuItem)sender).Tag;
                //FrmKapanCreation.ShowForm(0, "", "", Transaction.FrmKapanCreation.FORMTYPE.PCNKAPAN);  

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

    }
}
