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
using DevExpress.Data;
using AxoneMFGRJ.Masters;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmRoughPurchaseView : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        DataTable DtabSalary = new DataTable();
        DataTable DTabPurchase = new DataTable();
        BOFormPer ObjPer = new BOFormPer();

        bool pBoolIsComplete = true;

        double DouCarat = 0;//urvisha
        double DouRejectionAmount = 0;//urvisha
        double DouSaleRapaport = 0;//urvisha
        double DouSaleRapaportAmt = 0;//urvisha
        double DouRejectionCarat = 0;//urvisha

        double DouSaleAmount = 0;//urvisha
        double DouSaleCarat = 0;//urvisha

        double DouKapanAmount = 0;//urvisha
        double DouKapanCarat = 0;//urvisha

        double DouRateDalaliAvg = 0;
        double DouGrossAmt = 0;
        double DouRghCarat = 0;


        #region Property Settings

        public FrmRoughPurchaseView()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));

            string pStrPassword = ObjPer.PASSWORD;

            FrmPassword FrmPassword = new FrmPassword();
            if (FrmPassword.ShowForm(pStrPassword) == System.Windows.Forms.DialogResult.No)
            {
                this.Close();
                return;
            }

            CmbRoughType.SelectedIndex = 0;
            BtnSearch_Click(null, null);

            this.Show();

            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);

            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDet.RestoreLayoutFromStream(stream);

            }

            Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDetMixSplit.Name);
            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDetMixSplit.RestoreLayoutFromStream(stream);

            }

            Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDetKapan.Name);
            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDetKapan.RestoreLayoutFromStream(stream);

            }
            Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDetRejection.Name);
            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDetRejection.RestoreLayoutFromStream(stream);

            }
            Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDetSale.Name);
            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDetSale.RestoreLayoutFromStream(stream);
            }
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

            TrnRoughPurchaseProperty Property = new TrnRoughPurchaseProperty();

            GrdDet.BeginUpdate();
            DTabPurchase = ObjMast.GetDataForPurchaseLiveStock("PURCHASE", ChkDisplayAllLot.Checked, CmbRoughType.Text, txtKapan.Text);
            MainGrid.DataSource = DTabPurchase;
            MainGrid.Refresh();

            GrdDet.BestFitMaxRowCount = 200;
            GrdDet.BestFitColumns();
          

            if (MainGrid.RepositoryItems.Count == 4)
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
            GrdDet.LeftCoord = 0;
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

                if (Val.Val(DRow["BALANCECARAT"]) == 0)
                {
                    Global.MessageError("There Is Not Balance Found In This Lot You Can't Create Kapan");
                    return;
                }

                FrmKapanCreation FrmKapanCreation = new FrmKapanCreation();
                //FrmKapanCreation.ShowForm(Val.ToInt64(DRow["LOT_ID"]), Val.ToString(DRow["PARTYNAME"]), Val.ToString(DRow["PARTYINVOICENO"]), Transaction.FrmKapanCreation.FORMTYPE.ORIGINALKAPAN);
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
            if (e.Clicks == 1)
            {
                xtraTabControl1.SelectedTabPageIndex = 1;

                Int64 LotID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("LOT_ID"));
                string LotNo = Val.ToString(GrdDet.GetFocusedRowCellValue("LOTNO"));
                string SystemInvoiceNo = Val.ToString(GrdDet.GetFocusedRowCellValue("SYSTEMINVOICENO"));
                string ManualInvoiceNo = Val.ToString(GrdDet.GetFocusedRowCellValue("MANUALINVOICENO"));

                GrpCaption.Text = "TRACKING OF INVOICE : [ " + ManualInvoiceNo + " ] && LOT NO : [ " + LotNo + " ]";

                xtraTabPageMixSplit.Text = "     MIX/SPLIT ( 0 )     ";
                xtraTabPageKapan.Text = "     KAPAN ( 0 )     ";
                xtraTabPageRejection.Text = "     REJECTION ( 0 )     ";

                if (LotID != 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    GrdDetMixSplit.BeginUpdate();
                    GrdDetKapan.BeginUpdate();
                    GrdDetRejection.BeginUpdate();
                    GrdDetSale.BeginUpdate();

                    DataSet DS = ObjMast.GetLotIDTracking(LotID);

                    MainGridMixSplit.DataSource = DS.Tables[0];
                    MainGridMixSplit.Refresh();

                    MainGridKapan.DataSource = DS.Tables[1];
                    MainGridKapan.Refresh();

                    MainGridRejection.DataSource = DS.Tables[2];
                    MainGridRejection.Refresh();

                    MainGridSale.DataSource = DS.Tables[3];
                    MainGridSale.Refresh();

                    GrdDetMixSplit.BestFitColumns();
                    GrdDetKapan.BestFitColumns();
                    GrdDetRejection.BestFitColumns();

                    GrdDetMixSplit.EndUpdate();
                    GrdDetKapan.EndUpdate();
                    GrdDetRejection.EndUpdate();
                    GrdDetSale.EndUpdate();

                    xtraTabPageMixSplit.Text = "     MIX/SPLIT ( " + DS.Tables[0].Rows.Count.ToString() + " )     ";
                    xtraTabPageKapan.Text = "     KAPAN ( " + DS.Tables[1].Rows.Count.ToString() + " )     ";
                    xtraTabPageRejection.Text = "     REJECTION ( " + DS.Tables[2].Rows.Count.ToString() + " )     ";
                    xtraTabPageSale.Text = "     SALE ( " + DS.Tables[3].Rows.Count.ToString() + " )     ";

                    this.Cursor = Cursors.Default;

                }
            }

            if (e.Clicks == 2)
            {
                Int64 LOT_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("LOT_ID"));

                if (e.Column.FieldName == "KAPANCARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("KAPAN", LOT_ID,txtKapan.Text);

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
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("REJECTION", LOT_ID, txtKapan.Text);

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
                else if (e.Column.FieldName == "SPLITCARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("SPLIT", LOT_ID, txtKapan.Text);

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Split List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;
                    FrmPopupGrid.Width = 1000;

                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
                else if (e.Column.FieldName == "MIXCARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("MIX", LOT_ID, txtKapan.Text);

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Mix List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;
                    FrmPopupGrid.Width = 1000;
                    
                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
                else if (e.Column.FieldName == "SALECARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("SALE", LOT_ID, txtKapan.Text);

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Sale List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;
                    FrmPopupGrid.Width = 1000;

                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
                else if(e.Column.FieldName == "KAPANNAME")
                {
                    Int64 StrLot_ID = 0; double DouRCostWithDalaliAmt = 0;

                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    StrLot_ID = Val.ToInt64(DRow["LOT_ID"]);
                    DouRCostWithDalaliAmt = Math.Round((Val.Val(DRow["ROUGHCOSTWITHDALALI"]) * Val.Val(DRow["CARAT"])),3);

                    if (StrLot_ID == 0)
                        return;

                    this.Cursor = Cursors.WaitCursor;
                    FrmKapanCreation FrmKapanCreation = new FrmKapanCreation();
                    //FrmKapanCreation.MdiParent = Global.gMainRef;
                    //FrmKapanCreation.ShowForm(StrLot_ID, FrmKapanCreation.FORMTYPE.ORIGINALKAPANUPDATE, DouRCostWithDalaliAmt);
                    this.Cursor = Cursors.Default;
                }

                else if (e.Column.FieldName == "SYSTEMINVOICENO")
                {
                    
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    Int64 StrInvoiceID = Val.ToInt64(DRow["INVOICE_ID"]);
                    
                    if (StrInvoiceID == 0)
                        return;

                    this.Cursor = Cursors.WaitCursor;

                    FrmRoughPurchaseMasterDetail FrmRoughPurchaseMasterDetail = new FrmRoughPurchaseMasterDetail();
                    FrmRoughPurchaseMasterDetail.MdiParent = Global.gMainRef;
                    FrmRoughPurchaseMasterDetail.ShowForm(StrInvoiceID);

                    this.Cursor = Cursors.Default;
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
                if (Val.Val(DRow["BALANCECARAT"]) == 0)
                {
                    Global.MessageError("There Is Not Balance Found In This Lot You Can't Create Kapan");
                    return;
                }

                FrmRejectionTransfer FrmRejectionTransfer = new FrmRejectionTransfer();
                FrmRejectionTransfer.ShowForm(Val.ToString(DRow["LOT_ID"]), Val.ToString(DRow["PARTYNAME"]), Val.ToString(DRow["PARTYINVOICENO"]), "INVOICE");
                FrmRejectionTransfer.MdiParent = Global.gMainRef;
                FrmRejectionTransfer.FormClosing += new FormClosingEventHandler(Form_Closing);

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

        private void BtnREPKapanCreate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                FrmKapanCreation FrmKapanCreation = new FrmKapanCreation();
                //FrmKapanCreation.MdiParent = this;
                //FrmKapanCreation.Tag = ((ToolStripMenuItem)sender).Tag;
                //FrmKapanCreation.ShowForm(0, "", "", Transaction.FrmKapanCreation.FORMTYPE.REPAIRING);

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message.ToString());
            }
        }

        private void GrdDet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                xtraTabControl1.SelectedTabPageIndex = 1;

                Int64 LotID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("LOT_ID"));
                string LotNo = Val.ToString(GrdDet.GetFocusedRowCellValue("LOTNO"));
                string SystemInvoiceNo = Val.ToString(GrdDet.GetFocusedRowCellValue("SYSTEMINVOICENO"));
                string ManualInvoiceNo = Val.ToString(GrdDet.GetFocusedRowCellValue("MANUALINVOICENO"));

                GrpCaption.Text = "TRACKING OF INVOICE : [ " + ManualInvoiceNo + " ] && LOT NO : [ " + LotNo + " ]";
                
                xtraTabPageMixSplit.Text = "     MIX/SPLIT ( 0 )     ";
                xtraTabPageKapan.Text = "     KAPAN ( 0 )     ";
                xtraTabPageRejection.Text = "     REJECTION ( 0 )     ";

                if (LotID != 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    GrdDetMixSplit.BeginUpdate();
                    GrdDetKapan.BeginUpdate();
                    GrdDetRejection.BeginUpdate();
                    GrdDetSale.BeginUpdate();

                    DataSet DS =  ObjMast.GetLotIDTracking(LotID);
                    
                    MainGridMixSplit.DataSource = DS.Tables[0];
                    MainGridMixSplit.Refresh();

                    MainGridKapan.DataSource = DS.Tables[1];
                    MainGridKapan.Refresh();

                    MainGridRejection.DataSource = DS.Tables[2];
                    MainGridRejection.Refresh();

                    MainGridSale.DataSource = DS.Tables[3];
                    MainGridSale.Refresh();

                    GrdDetMixSplit.BestFitColumns();
                    GrdDetKapan.BestFitColumns();
                    GrdDetRejection.BestFitColumns();

                    GrdDetMixSplit.EndUpdate();
                    GrdDetKapan.EndUpdate();
                    GrdDetRejection.EndUpdate();
                    GrdDetSale.EndUpdate();

                    xtraTabPageMixSplit.Text = "     MIX/SPLIT ( " + DS.Tables[0].Rows.Count.ToString() + " )     ";
                    xtraTabPageKapan.Text = "     KAPAN ( " + DS.Tables[1].Rows.Count.ToString() + " )     ";
                    xtraTabPageRejection.Text = "     REJECTION ( " + DS.Tables[2].Rows.Count.ToString() + " )     ";
                    xtraTabPageSale.Text = "     SALE ( " + DS.Tables[3].Rows.Count.ToString() + " )     ";

                    this.Cursor = Cursors.Default;
                
                }
                
            }
            catch (Exception Ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(Ex.Message.ToString());
            }
        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "ISCOMPLETE")) == true)
            {
                e.Appearance.BackColor = Color.LightGray;
                e.Appearance.BackColor2 = Color.LightGray;
            }
            else 
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.BackColor2 = Color.White;
            }
        }

        private void BtnParcelSplit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable DtInvDetail = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

            if (DtInvDetail.Rows.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select AtLeast One Record For This Transaction");
                return;
            }

            DataTable DTabDistinct =  DtInvDetail.DefaultView.ToTable(true, "INVOICE_ID");
            if (DTabDistinct.Rows.Count > 1)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select Only One Invoice Lot For This Operation");
                return;
            }
            if (DtInvDetail.Rows.Count > 1)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select Only One Lot For This Operation");
                return;
            } 
            DTabDistinct.Dispose();
            DTabDistinct = null;
            this.Cursor = Cursors.Default;
            FrmRoughPurchaseMixSplit FrmRoughPurchaseMixSplit = new FrmRoughPurchaseMixSplit();
            FrmRoughPurchaseMixSplit.MdiParent = Global.gMainRef;
            FrmRoughPurchaseMixSplit.ShowForm(DtInvDetail, Transaction.FrmRoughPurchaseMixSplit.FormType.Split);
            FrmRoughPurchaseMixSplit.FormClosing += new FormClosingEventHandler(FrmRoughPurchaseMixSplit_FormClosing);


        }

        private void BtnSale_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable DtInvDetail = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

            if (DtInvDetail.Rows.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select AtLeast One Record For This Transaction");
                return;
            }

            DataTable DTabDistinct = DtInvDetail.DefaultView.ToTable(true, "INVOICE_ID");
            if (DTabDistinct.Rows.Count > 1)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select Only One Invoice Lot For This Operation");
                return;
            }
            //if (DtInvDetail.Rows.Count == 1)
            //{
            //    this.Cursor = Cursors.Default;
            //    Global.Message("Please Select Multiple Lots For This Operation");
            //    return;
            //}

            DTabDistinct.Dispose();
            DTabDistinct = null;
            this.Cursor = Cursors.Default;
            FrmRoughSaleMasterDetail FrmRoughSaleMasterDetail = new FrmRoughSaleMasterDetail();
            FrmRoughSaleMasterDetail.MdiParent = Global.gMainRef;
            FrmRoughSaleMasterDetail.ShowForm(DtInvDetail, Transaction.FrmRoughSaleMasterDetail.FormType.Lot);
            FrmRoughSaleMasterDetail.FormClosing += new FormClosingEventHandler(FrmRoughPurchaseMixSplit_FormClosing);

        }

        private void ParcelMix_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable DtInvDetail = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

            if (DtInvDetail.Rows.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select AtLeast One Record For This Transaction");
                return;
            }

            DataTable DTabDistinct = DtInvDetail.DefaultView.ToTable(true, "INVOICE_ID");
            if (DTabDistinct.Rows.Count > 1)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select Only One Invoice Lot For This Operation");
                return;
            }
            if (DtInvDetail.Rows.Count == 1)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select Multiple Lots For This Operation");
                return;
            }

            DTabDistinct.Dispose();
            DTabDistinct = null;
            this.Cursor = Cursors.Default;
            FrmRoughPurchaseMixSplit FrmRoughPurchaseMixSplit = new FrmRoughPurchaseMixSplit();
            FrmRoughPurchaseMixSplit.MdiParent = Global.gMainRef;
            FrmRoughPurchaseMixSplit.ShowForm(DtInvDetail, Transaction.FrmRoughPurchaseMixSplit.FormType.Mix);
            FrmRoughPurchaseMixSplit.FormClosing += new FormClosingEventHandler(FrmRoughPurchaseMixSplit_FormClosing);

        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = string.Empty;
            }
        }

        private void FrmRoughPurchaseMixSplit_FormClosing(object sender, FormClosingEventArgs e)
        {
            BtnSearch_Click(null, null);
        }

        private void BtnRejectionDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetRejection.FocusedRowHandle < 0)
                {
                    return;
                }
                if (Global.Confirm("Are You Sure You Want To Delete this Entry ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;

                    TRN_RejectionProperty Property = new TRN_RejectionProperty();
                    Property.REJECTIONTRN_ID = Val.ToInt64(GrdDetRejection.GetFocusedRowCellValue("REJECTIONTRN_ID"));

                    Property = new BOTRN_Rejection().DeleteRejectionID(Property);
                    this.Cursor = Cursors.Default;
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        GrdDetRejection.DeleteRow(GrdDetRejection.FocusedRowHandle);
                        BtnSearch_Click(null, null);
                    }
                    else
                    {
                        Global.MessageError(Property.ReturnMessageDesc);
                    }
                }
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(EX.Message);
            }
        }

        private void BtnRejectionUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetRejection.FocusedRowHandle < 0)
                {
                    return;
                }
                if (Global.Confirm("Are You Sure You Want To Delete this Entry ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    TRN_RejectionProperty Property = new TRN_RejectionProperty();

                    Property.REJECTIONTRN_ID = Val.ToInt64(GrdDetRejection.GetFocusedRowCellValue("REJECTIONTRN_ID"));
                    Property.LOT_ID = Val.ToInt64(GrdDetRejection.GetFocusedRowCellValue("LOT_ID"));
                    Property.PCS = Val.ToInt32(GrdDetRejection.GetFocusedRowCellValue("PCS"));
                    Property.CARAT = Val.Val(GrdDetRejection.GetFocusedRowCellValue("CARAT"));
                    Property.RATE = Val.Val(GrdDetRejection.GetFocusedRowCellValue("RATE"));
                    Property.AMOUNT = Val.Val(GrdDetRejection.GetFocusedRowCellValue("AMOUNT"));
                    Property.REJECTION_ID = Val.ToInt32(GrdDetRejection.GetFocusedRowCellValue("REJECTION_ID")); 
                    Property.REMARK = Val.ToString(GrdDetRejection.GetFocusedRowCellValue("REMARK"));
                    Property.REJECTIONDATE = Val.SqlDate(Val.ToString( GrdDetRejection.GetFocusedRowCellValue("REJECTIONDATE")));

                    if (Val.ToString(GrdDetRejection.GetFocusedRowCellValue("REJECTIONNAME")).Trim().Length == 0)
                    {
                        Global.MessageError("Rejection Name Is Required");
                        return;
                    }
                    if (Val.Val(GrdDetRejection.GetFocusedRowCellValue("CARAT")) == 0)
                    {
                        Global.MessageError("Rejection Carat Is Required");
                        return;
                    }
                    if (Val.Val(GrdDetRejection.GetFocusedRowCellValue("RATE")) == 0)
                    {
                        Global.MessageError("Rejection Rate Is Required");
                        return;
                    }
                    this.Cursor = Cursors.WaitCursor;

                    Property = new BOTRN_Rejection().UpdateRejectionID(Property);
                    this.Cursor = Cursors.Default;
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        GrdDetRejection.DeleteRow(GrdDetRejection.FocusedRowHandle);
                        BtnSearch_Click(null, null);
                    }
                    else
                    {
                        Global.MessageError(Property.ReturnMessageDesc);
                    }
                }
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(EX.Message);
            }
        }

        private void BtnKapanDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetKapan.FocusedRowHandle < 0)
                {
                    return;
                }
                else
                {
                    if (Global.Confirm("Are You Sure To Delete this Kapan ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnKapanCreateProperty Property = new TrnKapanCreateProperty();
                    Property.Ope = "DELETE";
                    Property.KAPAN_ID = Val.ToInt64(GrdDetKapan.GetFocusedRowCellValue("KAPAN_ID"));
                    
                    Property = ObjKapan.EditKapan(Property);
                    this.Cursor = Cursors.Default;

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        GrdDetKapan.DeleteRow(GrdDetKapan.FocusedRowHandle);
                        BtnSearch_Click(null, null);
                    }
                    else
                    {
                        Global.MessageError(Property.ReturnMessageDesc);
                    }
                }

            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(EX.Message);
            }
        }

        private void BtnKapanUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetKapan.FocusedRowHandle < 0)
                {
                    return;
                }
                else
                {
                    if (Global.Confirm("Are You Sure To Update Status Of this Kapan ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    TrnKapanCreateProperty Property = new TrnKapanCreateProperty();
                    Property.Ope = "UPDATE";
                    Property.KAPAN_ID = Val.ToInt64(GrdDetKapan.GetFocusedRowCellValue("KAPAN_ID"));
                    Property.STATUS = Val.ToString(GrdDetKapan.GetFocusedRowCellValue("STATUS"));
                    Property.KAPANPCS = Val.ToInt32(GrdDetKapan.GetFocusedRowCellValue("KAPANPCS"));
                    Property.KAPANCARAT = Val.Val(GrdDetKapan.GetFocusedRowCellValue("KAPANCARAT"));
                    Property.KAPANNAME = Val.ToString(GrdDetKapan.GetFocusedRowCellValue("KAPANNAME"));
                    Property.KAPANGROUP = Val.ToString(GrdDetKapan.GetFocusedRowCellValue("KAPANGROUP"));
                    if (Val.ToString(GrdDetKapan.GetFocusedRowCellValue("MANAGERNAME")) != "")
                    {
                        Property.MANAGER_ID = Val.Bigint(GrdDetKapan.GetFocusedRowCellValue("MANAGER_ID"));
                    }
                    else
                    {
                        Property.MANAGER_ID = 0;
                    }
                    Property.ISHIDE = Val.ToBoolean(GrdDetKapan.GetFocusedRowCellValue("ISHIDE"));
                    Property.ISNOTAPPLYANYLOCK = Val.ToBoolean(GrdDetKapan.GetFocusedRowCellValue("ISNOTAPPLYANYLOCK"));
                    Property.LABOURAMOUNT = Val.Val(GrdDetKapan.GetFocusedRowCellValue("LABOURAMOUNT"));


                    if (Val.IsDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("COMPLETEDATE"))) == true)
                    {
                        Property.COMPLETEDATE = Val.SqlDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("COMPLETEDATE")));
                    }
                    else
                    {
                        Property.COMPLETEDATE = null;
                    }
                    if (Val.IsDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("CLVCOMPLETEDATE"))) == true)//urvisha24/11/22
                    {
                        Property.CLVCOMPLETEDATE = Val.SqlDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("CLVCOMPLETEDATE")));
                    }
                    else
                    {
                        Property.CLVCOMPLETEDATE = null;
                    }
                    if (Val.IsDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("MFGISSUEDATE"))) == true)//urvisha24/11/22
                    {
                        Property.MFGISSUEDATE = Val.SqlDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("MFGISSUEDATE")));
                    }
                    else
                    {
                        Property.MFGISSUEDATE = null;
                    }
                    if (Val.IsDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("GHATCOMPLETEDATE"))) == true)//urvisha24/11/22
                    {
                        Property.GHATCOMPLETEDATE = Val.SqlDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("GHATCOMPLETEDATE")));
                    }
                    else
                    {
                        Property.GHATCOMPLETEDATE = null;
                    }
                    if (Val.IsDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("POLISHRECVDATE"))) == true)//urvisha24/11/22
                    {
                        Property.POLISHRECVDATE = Val.SqlDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("POLISHRECVDATE")));
                    }
                    else
                    {
                        Property.POLISHRECVDATE = null;
                    } if (Val.IsDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("MUMBAIRECVDATE"))) == true)//urvisha24/11/22
                    {
                        Property.MUMBAIRECVDATE = Val.SqlDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("MUMBAIRECVDATE")));
                    }
                    else
                    {
                        Property.MUMBAIRECVDATE = null;
                    }
                    Property.KAPANDATE = Val.SqlDate(Val.ToString(GrdDetKapan.GetFocusedRowCellValue("KAPANDATE"))); // K : 06/12/2022
                    Property.KAPANAMOUNT = Val.Val(GrdDetKapan.GetFocusedRowCellValue("KAPANAMOUNT"));
                    Property.KAPANRATE = Val.Val(GrdDetKapan.GetFocusedRowCellValue("KAPANRATE"));
                    Property = ObjKapan.EditKapan(Property);
                    this.Cursor = Cursors.Default;
                    
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        GrdDetKapan.RefreshData();
                        BtnSearch_Click(null, null);
                    }
                    else
                    {
                        Global.MessageError(Property.ReturnMessageDesc);
                    }
                }

            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(EX.Message);
            }
        }

        private void BtnMixSplitDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetMixSplit.FocusedRowHandle < 0)
                {
                    return;
                }
                if (Global.Confirm("Are You Sure You Want To Delete this Entry ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;

                    TrnRoughPurchaseProperty Property = new TrnRoughPurchaseProperty();

                    Property.TRN_ID = Val.ToInt64(GrdDetMixSplit.GetFocusedRowCellValue("TRN_ID"));
                    Property.OPERATION = Val.ToString(GrdDetMixSplit.GetFocusedRowCellValue("OPERATION"));
                    Property.LOT_ID = Val.ToInt64(GrdDetMixSplit.GetFocusedRowCellValue("LOT_ID"));
                    Property.LOTNO = Val.ToString(GrdDetMixSplit.GetFocusedRowCellValue("LOTNO"));
                    Property.REFLOT_ID = Val.ToInt64(GrdDetMixSplit.GetFocusedRowCellValue("REFLOT_ID"));
                    Property.REFLOTNO = Val.ToString(GrdDetMixSplit.GetFocusedRowCellValue("REFLOTNO"));
                    Property.SRNO = Val.ToInt32(GrdDetMixSplit.GetFocusedRowCellValue("SRNO"));
            
                    Property = new BOTRN_RoughPurchase().MixSplitHistoryDeleteSingle(Property);
                    this.Cursor = Cursors.Default;
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        GrdDetRejection.DeleteRow(GrdDetRejection.FocusedRowHandle);
                        BtnSearch_Click(null, null);
                    }
                    else
                    {
                        Global.MessageError(Property.ReturnMessageDesc);
                    }
                }
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(EX.Message);
            }
        }

        private void txtManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetKapan.SetFocusedRowCellValue("MANAGER_ID", Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]));
                        GrdDetKapan.SetFocusedRowCellValue("MANAGERNAME", Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]));
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

        private void txtRejectionName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "REJECTIONCODE,REJECTIONNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_REJECTION);
                    FrmSearch.mColumnsToHide = "REJECTION_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetRejection.SetFocusedRowCellValue("REJECTION_ID", Val.ToString(FrmSearch.mDRow["REJECTION_ID"]));
                        GrdDetRejection.SetFocusedRowCellValue("REJECTIONNAME", Val.ToString(FrmSearch.mDRow["REJECTIONNAME"]));
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

        private void GrdDetRejection_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName.ToUpper())
            {
                case "CARAT":
                case "RATE":
                    double DouAmount = Math.Round(Val.Val(GrdDetRejection.GetFocusedRowCellValue("CARAT")) * Val.Val(GrdDetRejection.GetFocusedRowCellValue("RATE")), 2);
                    GrdDetRejection.SetFocusedRowCellValue("AMOUNT", DouAmount);
                    break;
                default:
                    break;
            }
        }

        private void GrdDetRejection_CustomSummaryCalculate(object sender,CustomSummaryEventArgs e)
        {
            try

            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouCarat = 0;
                    DouRejectionAmount = 0;
                    DouRejectionCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouCarat = DouCarat + Val.Val(GrdDetRejection.GetRowCellValue(e.RowHandle, "CARAT"));

                    DouRejectionAmount = DouRejectionAmount + Val.Val(GrdDetRejection.GetRowCellValue(e.RowHandle, "AMOUNT"));
                    DouRejectionCarat = DouRejectionAmount / DouCarat;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouRejectionAmount) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void GrdDetKapan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName.ToUpper() == "KAPANRATE" || e.Column.FieldName.ToUpper() == "KAPANCARAT")
                {
                    string StrKapanName = Val.ToString(GrdDetKapan.GetFocusedRowCellValue("KAPANNAME"));
                    double DouKapanRate = Val.Val(GrdDetKapan.GetFocusedRowCellValue("KAPANRATE"));
                    double DouCarat = Val.Val(GrdDetKapan.GetFocusedRowCellValue("KAPANCARAT"));
                    GrdDetKapan.SetFocusedRowCellValue("KAPANAMOUNT", Math.Round((DouCarat * DouKapanRate), 3));
                }

                switch (e.Column.FieldName.ToUpper())
                {
                    case "KAPANCARAT":
                    case "KAPANRATE":
                        double DouAmount = Math.Round(Val.Val(GrdDetKapan.GetFocusedRowCellValue("KAPANAMOUNT")) / Val.Val(GrdDetKapan.GetFocusedRowCellValue("KAPANCARAT")), 2);
                        GrdDetRejection.SetFocusedRowCellValue("KAPANRATE", DouAmount);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }

        private void btnKapanValuation_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //DataTable DtKapanDetail = Global.GetSelectedRecordOfGrid(GrdDetKapan, false, ObjGridSelection);
           // DataTable DtKapanDetail = new DataTable();
            //if (GrdDetKapan.DataSource != null)
              //  DtKapanDetail = ((DataView)GrdDetKapan.DataSource).ToTable();
            /*if (DtKapanDetail.Rows.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                Global.Message("Please Select AtLeast One Record For This Transaction");
                return;
            }*/
            if (GrdDetKapan.FocusedRowHandle < 0)
                return;

            DataRow Dr = GrdDetKapan.GetFocusedDataRow();
            

            this.Cursor = Cursors.Default;
            FrmKapanValuation FrmKapanValuation = new FrmKapanValuation();
            //FrmRoughPurchaseMixSplit FrmRoughPurchaseMixSplit = new FrmRoughPurchaseMixSplit();
            FrmKapanValuation.MdiParent = Global.gMainRef;
            FrmKapanValuation.ShowForm(Dr);
            FrmKapanValuation.FormClosing += new FormClosingEventHandler(FrmRoughPurchaseMixSplit_FormClosing);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                {
                    return;
                }
                else
                {
                    if (Global.Confirm("Are You Sure To Update Status Of this Kapan ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    RoughUpdateProperty Property = new RoughUpdateProperty();
                    Property.COMPLETEDATE = Val.SqlDate(Val.ToString(GrdDet.GetFocusedRowCellValue("COMPLETEDATE")));
                    Property.ISCOMPLETE = Val.ToBoolean(GrdDet.GetFocusedRowCellValue("ISCOMPLETE"));
                    Property.LOT_ID = Val.ToInt(GrdDet.GetFocusedRowCellValue("LOT_ID"));

                    Property = ObjMast.UpdateRough(Property);
                    this.Cursor = Cursors.Default;

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        GrdDetKapan.RefreshData();
                        BtnSearch_Click(null, null);
                    }
                    else
                    {
                        Global.MessageError(Property.ReturnMessageDesc);
                    }
                    
                }
            }
            catch(Exception ex)
            {
            
            }
        }

        private void txtKapan_Validated(object sender, EventArgs e)
        {
            BtnSearch_Click(null, null);

            GrdDet.Focus();
            GrdDet.FocusedRowHandle = 0;
            GrdDet.FocusedColumn = GrdDet.Columns["SYSTEMINVOICENO"];
            GrdDet_FocusedRowChanged(null, null);

            xtraTabControl1.SelectedTabPageIndex = 1;
      
        }

        private void GrdDet_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                DouRateDalaliAvg = 0;
                DouGrossAmt = 0;
                DouRghCarat = 0;

            }
            else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                DouGrossAmt = DouGrossAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "GROSSAMOUNT"));
                DouRghCarat = DouRghCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "CARAT"));
            }
            //GROSSBROKRATE
            else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("GROSSBROKRATE") == 0)
                {
                    if (Val.Val(DouRghCarat) > 0)
                        e.TotalValue = Math.Round(Val.Val(DouGrossAmt) / Val.Val(DouRghCarat), 2);
                    else
                        e.TotalValue = 0;
                }
            }
        }

        private void lblSaveLayout_Click(object sender, EventArgs e)
        {

            Stream str = new System.IO.MemoryStream();
            GrdDet.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDet.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }

        }

        private void lblDefaultLayout_Click(object sender, EventArgs e)
        {

            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDet.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void lblMixSplitSaveLayout_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdDetMixSplit.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDetMixSplit.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblMixSplitDeleteLayout_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDetMixSplit.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void lblKapanSaveLayout_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdDetKapan.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDetKapan.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblKapanDeleteLayout_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDetKapan.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void lblRejectionSaveLayout_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdDetRejection.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDetRejection.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblRejectionDeleteLayout_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDetRejection.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void lblSaleEntrySaveLayout_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdDetSale.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDetSale.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblSaleEntryDeletLayout_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDetSale.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void GrdDetSale_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouSaleCarat = 0;
                    DouSaleAmount = 0;                    
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouSaleCarat = DouSaleCarat + Val.Val(GrdDetSale.GetRowCellValue(e.RowHandle, "CARAT"));
                    DouSaleAmount = DouSaleAmount + Val.Val(GrdDetSale.GetRowCellValue(e.RowHandle, "GROSSAMOUNT"));                    
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouSaleAmount) / Val.Val(DouSaleCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void GrdDetKapan_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouKapanCarat = 0;
                    DouKapanAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouKapanCarat = DouKapanCarat + Val.Val(GrdDetKapan.GetRowCellValue(e.RowHandle, "KAPANCARAT"));
                    DouKapanAmount = DouKapanAmount + Val.Val(GrdDetKapan.GetRowCellValue(e.RowHandle, "KAPANAMOUNT"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("KAPANRATE") == 0)
                    {
                        if (Val.Val(DouKapanCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouKapanAmount) / Val.Val(DouKapanCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void RepTxtKapnPcs_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDetKapan.PostEditor();
                    if (GrdDetKapan.FocusedRowHandle < 0)
                    {
                        return;
                    }
                    if (Global.Confirm("Are You Sure To Update Pcs this Kapan ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnKapanCreateProperty Property = new TrnKapanCreateProperty();

                    Property.KAPAN_ID = Val.ToInt64(GrdDetKapan.GetFocusedRowCellValue("KAPAN_ID"));
                    Property.STATUS = Val.ToString(GrdDetKapan.GetFocusedRowCellValue("STATUS"));
                    Property.KAPANPCS = Val.ToInt32(GrdDetKapan.GetFocusedRowCellValue("KAPANPCS"));

                    Property = ObjKapan.EditKapanpcs(Property);

                    Global.Message(Property.ReturnMessageDesc);

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        GrdDet.RefreshData();
                    }

                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void ReptxtKapanRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDetKapan.PostEditor();
                    if (GrdDetKapan.FocusedRowHandle < 0)
                    {
                        return;
                    }
                    if (Global.Confirm("Are You Sure To Update Rate this Kapan ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnKapanCreateProperty Property = new TrnKapanCreateProperty();

                    Property.KAPAN_ID = Val.ToInt64(GrdDetKapan.GetFocusedRowCellValue("KAPAN_ID"));
                    Property.STATUS = Val.ToString(GrdDetKapan.GetFocusedRowCellValue("STATUS"));
                    Property.KAPANRATE = Val.Val(GrdDetKapan.GetFocusedRowCellValue("KAPANRATE"));
                    Property.KAPANAMOUNT = Val.ToInt32(GrdDetKapan.GetFocusedRowCellValue("KAPANAMOUNT"));

                    Property = ObjKapan.EditKapanRate(Property);

                    Global.Message(Property.ReturnMessageDesc);

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        GrdDet.RefreshData();
                    }

                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void ReptxtKapanCarat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDetKapan.PostEditor();
                    if (GrdDetKapan.FocusedRowHandle < 0)
                    {
                        return;
                    }
                    if (Global.Confirm("Are You Sure To Update Carat this Kapan ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnKapanCreateProperty Property = new TrnKapanCreateProperty();

                    Property.KAPAN_ID = Val.ToInt64(GrdDetKapan.GetFocusedRowCellValue("KAPAN_ID"));
                    Property.STATUS = Val.ToString(GrdDetKapan.GetFocusedRowCellValue("STATUS"));
                    Property.KAPANCARAT = Val.Val(GrdDetKapan.GetFocusedRowCellValue("KAPANCARAT"));

                    Property = ObjKapan.EditKapanCarat(Property);

                    Global.Message(Property.ReturnMessageDesc);

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        GrdDet.RefreshData();
                    }

                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }
    }
}
