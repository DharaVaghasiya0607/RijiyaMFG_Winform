using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using BusLib.Account;
using BusLib.Configuration;
using BusLib.Transaction;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace AxoneMFGRJ.Account
{
    public partial class FrmAsOnDateStockReport : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_StockReport objStock = new BOTRN_StockReport();

        DataTable DTabReport = new DataTable();
        DataTable DtabStock = new DataTable();

        double DouIssuePcs = 0;
        double DouIssueCarat = 0;
        double DouReturnPcs = 0;
        double DouReturnCarat = 0;
        double DouExpCarat = 0;
     

        #region Constructor

        public FrmAsOnDateStockReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            //GetData();

            DTPFromIssueDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            DTPToIssueDate.Text = DateTime.Now.ToShortDateString();

            ChkCashBank_CheckedChanged(null, null);
            FillControl();
            BtnAdd_Click(null, null);
        }

        private void AttachFormDefaultEvent()
        {
            this.KeyPreview = true;
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = false;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(objStock);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);

        }

        #endregion


        #region Control Events

        #endregion

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DTabReport.Rows.Clear();
                int IntColumn = GrdDet.Bands["BANDEXTRA"].Columns.Count;
                if (IntColumn > 0)
                {
                    GrdDet.Columns.Clear();

                    //for (int i = 0; i < DtabStock.Rows.Count; i++)
                    //{
                    //    GrdDet.Columns[i].GroupIndex = -1;
                    //}

                }

                string StrFromIssueDate = null;
                string StrToIssueDate = null;
                
                if (DTPFromIssueDate.Checked == true && DTPToIssueDate.Checked == true)
                {
                    StrFromIssueDate = Val.SqlDate(DTPFromIssueDate.Text);
                    StrToIssueDate = Val.SqlDate(DTPToIssueDate.Text);
                }
                
                string StrKapan = Val.LRTrim(ChkCmbKapan.Properties.GetCheckedItems());
                string StrProcess = Val.LRTrim(ChkCmbProcess.Properties.GetCheckedItems());
                string StrRough = Val.LRTrim(ChkCmbRough.Properties.GetCheckedItems()); ;

                string Group1 = CmbGroup1.Text;
                string Group2 = Val.ToString(CmbGroup2.SelectedItem);
                string Group3 = Val.ToString(CmbGroup3.SelectedItem);

                string StrGroupBy = string.Empty;

                if (!Val.ToString(Group1).Trim().Equals("NONE"))
                    StrGroupBy = Group1;
                if (!Val.ToString(Group2).Trim().Equals("NONE"))
                    StrGroupBy = StrGroupBy + "," + Group2;
                if (!Val.ToString(Group3).Trim().Equals("NONE"))
                    StrGroupBy = StrGroupBy + "," + Group3;

                StrGroupBy = Val.Trim(StrGroupBy.TrimStart(','));

                string[] Str = StrGroupBy.Split(',');

                DtabStock = objStock.GetStockReportGetData(StrFromIssueDate, StrToIssueDate, StrKapan, StrProcess, StrRough, StrGroupBy);

                MainGrid.DataSource = DtabStock;

                GrdDet.OptionsBehavior.Editable = false;
                GrdDet.ClearSorting();

                GrdDet.Columns["ISSUEPCS"].Caption = "Iss. Pcs";
                GrdDet.Columns["ISSUECARAT"].Caption = "Iss. Cts";
                GrdDet.Columns["RETURNPCS"].Caption = "Ret Pcs";
                GrdDet.Columns["RETURNCARAT"].Caption = "Ret Cts";
                GrdDet.Columns["RETURNPER"].Caption = "Ret %";
                GrdDet.Columns["DIFFPER"].Caption = "Diff %";
                GrdDet.Columns["SIZE"].Caption = "Size";
                GrdDet.Columns["EXPCARAT"].Caption = "Exp Cts";
                GrdDet.Columns["EXPPER"].Caption = "Exp %";
                GrdDet.Columns["LOSSCARAT"].Caption = "Wei(-)";
                GrdDet.Columns["KACHAPCS"].Caption = "Kacha Pcs";
                GrdDet.Columns["KACHACARAT"].Caption = "Kacha Cts";
                GrdDet.Columns["CANCELPCS"].Caption = "Can. Pcs";
                GrdDet.Columns["CANCELCARAT"].Caption = "Can. Crt";
                GrdDet.Columns["LABOURAMOUNT"].Caption = "Labour";
                GrdDet.Columns["OSCARAT"].Caption = "OS Crt";

                GrdDet.Columns["ISSUEPCS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["ISSUECARAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["RETURNPCS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["RETURNCARAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["RETURNPER"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["SIZE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["DIFFPER"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["EXPCARAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["EXPPER"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["LOSSCARAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["KACHAPCS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["KACHACARAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["CANCELPCS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["CANCELCARAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["LABOURAMOUNT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GrdDet.Columns["OSCARAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;


                GrdDet.Columns["ISSUEPCS"].DisplayFormat.FormatString = "{0:N0}";
                GrdDet.Columns["ISSUECARAT"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["RETURNPCS"].DisplayFormat.FormatString = "{0:N0}";
                GrdDet.Columns["RETURNCARAT"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["RETURNPER"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["DIFFPER"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["SIZE"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["EXPCARAT"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["EXPPER"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["LOSSCARAT"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["KACHAPCS"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["KACHACARAT"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["CANCELPCS"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["CANCELCARAT"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["LABOURAMOUNT"].DisplayFormat.FormatString = "{0:N4}";
                GrdDet.Columns["OSCARAT"].DisplayFormat.FormatString = "{0:N4}";

                GrdDet.Columns["ISSUEPCS"].Summary.Add(SummaryItemType.Sum, "ISSUEPCS", "{0:N0}");
                GrdDet.Columns["ISSUECARAT"].Summary.Add(SummaryItemType.Sum, "ISSUECARAT", "{0:N4}");

                GrdDet.Columns["RETURNPCS"].Summary.Add(SummaryItemType.Sum, "RETURNPCS", "{0:N0}");
                GrdDet.Columns["RETURNCARAT"].Summary.Add(SummaryItemType.Sum, "RETURNCARAT", "{0:N4}");
                
                GrdDet.Columns["KACHAPCS"].Summary.Add(SummaryItemType.Sum, "KACHAPCS", "{0:N0}");
                GrdDet.Columns["KACHACARAT"].Summary.Add(SummaryItemType.Sum, "KACHACARAT", "{0:N4}");

                GrdDet.Columns["CANCELPCS"].Summary.Add(SummaryItemType.Sum, "CANCELPCS", "{0:N0}");
                GrdDet.Columns["CANCELCARAT"].Summary.Add(SummaryItemType.Sum, "CANCELCARAT", "{0:N4}");

                GrdDet.Columns["RETURNPER"].Summary.Add(SummaryItemType.Custom, "RETURNPER", "{0:N4}");
                GrdDet.Columns["SIZE"].Summary.Add(SummaryItemType.Custom, "SIZE", "{0:N4}");
                GrdDet.Columns["EXPPER"].Summary.Add(SummaryItemType.Custom, "EXPPER", "{0:N4}");
                GrdDet.Columns["DIFFPER"].Summary.Add(SummaryItemType.Custom, "DIFFPER", "{0:N4}");
                
                GrdDet.Columns["EXPCARAT"].Summary.Add(SummaryItemType.Sum, "EXPCARAT", "{0:N4}");
                GrdDet.Columns["LOSSCARAT"].Summary.Add(SummaryItemType.Sum, "LOSSCARAT", "{0:N4}");
                GrdDet.Columns["LABOURAMOUNT"].Summary.Add(SummaryItemType.Sum, "LABOURAMOUNT", "{0:N4}");
                GrdDet.Columns["OSCARAT"].Summary.Add(SummaryItemType.Sum, "OSCARAT", "{0:N4}");

                if (GrdDet.GroupSummary.Count == 0)
                {
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "ISSUEPCS", GrdDet.Columns["ISSUEPCS"], "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "ISSUECARAT", GrdDet.Columns["ISSUECARAT"], "{0:N4}");

                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "RETURNPCS", GrdDet.Columns["RETURNPCS"], "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "RETURNCARAT", GrdDet.Columns["RETURNCARAT"], "{0:N4}");

                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "KACHAPCS", GrdDet.Columns["KACHAPCS"], "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "KACHACARAT", GrdDet.Columns["KACHACARAT"], "{0:N4}");

                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "CANCELPCS", GrdDet.Columns["CANCELPCS"], "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "CANCELCARAT", GrdDet.Columns["CANCELCARAT"], "{0:N4}");

                    GrdDet.GroupSummary.Add(SummaryItemType.Custom, "RETURNPER", GrdDet.Columns["RETURNPER"], "{0:N4}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Custom, "SIZE", GrdDet.Columns["SIZE"], "{0:N4}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Custom, "EXPPER", GrdDet.Columns["EXPPER"], "{0:N4}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Custom, "DIFFPER", GrdDet.Columns["DIFFPER"], "{0:N4}");
                    
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "EXPCARAT", GrdDet.Columns["EXPCARAT"], "{0:N4}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "LOSSCARAT", GrdDet.Columns["LOSSCARAT"], "{0:N4}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "LABOURAMOUNT", GrdDet.Columns["LABOURAMOUNT"], "{0:N4}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "OSCARAT", GrdDet.Columns["OSCARAT"], "{0:N4}");
                }

                GrdDet.OptionsView.ShowGroupPanel = true;
                int IntCount = 0;
                if (ChkIsPivot.Checked == false)
                {
                    GrdDet.OptionsView.ShowGroupPanel = true;
                    IntCount = Str.Length - 1;
                    for (int IntI = 0; IntI < IntCount; IntI++)
                    {
                        if (Str[IntI] != "")
                        {
                            GrdDet.Columns[Str[IntI]].GroupIndex = IntI;
                            GrdDet.Columns[Str[IntI]].Group();
                            GrdDet.Columns[Str[IntI]].OwnerBand = null;
                        }
                    }
                }
                else
                {
                    GrdDet.OptionsView.ShowBands = false;
                    for (int i = 0; i < DtabStock.Columns.Count; i++)
                    {
                        GrdDet.Columns[i].GroupIndex = -1;
                    }
                }

                int Len = Str.Length;
                if (Len > 0)
                {
                    GrdDet.Columns[Len - 1].SummaryItem.SummaryType = SummaryItemType.Count;
                    GrdDet.Columns[Len - 1].SummaryItem.DisplayFormat = "{0:N0}";
                }


                if (DtabStock.Columns.Contains("PROCESS"))
                    GrdDet.Columns["PROCESS"].Caption = "Process";
                
                if (DtabStock.Columns.Contains("ISSUEDATE"))
                    GrdDet.Columns["ISSUEDATE"].Caption = "Issue Date";
                
                if (DtabStock.Columns.Contains("RETURNDATE"))
                    GrdDet.Columns["RETURNDATE"].Caption = "Return Date";

                if (DtabStock.Columns.Contains("SHAPE"))
                    GrdDet.Columns["SHAPE"].Caption = "Shape";

                if (DtabStock.Columns.Contains("CHARNI"))
                    GrdDet.Columns["CHARNI"].Caption = "Charni";

                if (DtabStock.Columns.Contains("PURITY"))
                    GrdDet.Columns["PURITY"].Caption = "Purity";

                if (DtabStock.Columns.Contains("ROUGHCTS"))
                    GrdDet.Columns["ROUGHCTS"].Caption = "Invoice Cts";

                if (DtabStock.Columns.Contains("ROUGHINVOICE"))
                    GrdDet.Columns["ROUGHINVOICE"].Caption = "Invoice No";

                if (DtabStock.Columns.Contains("JOBWORKPARTY"))
                    GrdDet.Columns["JOBWORKPARTY"].Caption = "JobWrk/Emp";

                if (DtabStock.Columns.Contains("ROUGHPARTY"))
                    GrdDet.Columns["ROUGHPARTY"].Caption = "Rough Party";

                if (DtabStock.Columns.Contains("KAPAN"))
                    GrdDet.Columns["KAPAN"].Caption = "Kapan";

                if (DtabStock.Columns.Contains("PACKETNO"))
                    GrdDet.Columns["PACKETNO"].Caption = "Pkt No";


                for (int IntI = 0; IntI < GrdDet.Columns.Count; IntI++)
                {
                    GrdDet.Columns[IntI].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                }

                GrdDet.BestFitMaxRowCount = 200;
                GrdDet.BestFitColumns();


                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }


        }

        private void SetGridBand(BandedGridView bandedView, GridBand gridBand, DataTable DTabDistinct, string pStrColumnName, DataTable pDTab)
        {
            BandedGridColumn[] bandedColumns = new BandedGridColumn[DTabDistinct.Rows.Count];

            gridBand.Caption = pStrColumnName;

            for (int i = 0; i < pDTab.Rows.Count; i++)
            {
                bandedColumns[i] = (BandedGridColumn)bandedView.Columns.AddField(pStrColumnName);

                bandedColumns[i].OwnerBand = gridBand;
                bandedColumns[i].Visible = true;
            }

            //                pDTab.Columns.Add(Val.ToString(DTabDistinct.Rows[i]["COL"]));

        }


        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString("Stock Report", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Filter


            // ' For Filter
            TextBrick BrickTitlesFilter = e.Graph.DrawString("Date From :- " + DTPFromIssueDate.Text + " To :- " + DTPToIssueDate.Text, System.Drawing.Color.Navy, new RectangleF(0, 65, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesFilter.Font = new Font("Verdana", 9, FontStyle.Bold);
            BrickTitlesFilter.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesFilter.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesFilter.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("Verdana", 8, FontStyle.Bold);
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
            BrickPageNo.Font = new Font("Verdana", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void BtnExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "Stock Report";
                svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;
                    GrdDet.ExportToXlsx(Filepath);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        public void FillControl()
        {
            DataTable DtabKapan = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANMIX);

            ChkCmbKapan.Properties.DataSource = DtabKapan;
            ChkCmbKapan.Properties.DisplayMember = "KAPANNAME";
            ChkCmbKapan.Properties.ValueMember = "KAPAN_ID";

            DataTable DtabProcess = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);

            ChkCmbProcess.Properties.DataSource = DtabProcess;
            ChkCmbProcess.Properties.DisplayMember = "PROCESSNAME";
            ChkCmbProcess.Properties.ValueMember = "PROCESS_ID";

            DataTable DtabImport = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_IMPORT);

            ChkCmbRough.Properties.DataSource = DtabImport;
            ChkCmbRough.Properties.DisplayMember = "INVOICENAME";
            ChkCmbRough.Properties.ValueMember = "LOT_ID";
        }





        private void BtnPrint_Click(object sender, EventArgs e)
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



        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                DouIssuePcs = 0;
                DouIssueCarat = 0;
                DouReturnPcs = 0;
                DouReturnCarat = 0;
                DouExpCarat = 0;
                
            }
            else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                DouIssuePcs = DouIssuePcs + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "ISSUEPCS"));
                DouIssueCarat = DouIssueCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "ISSUECARAT"));
                DouReturnPcs = DouReturnPcs + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "RETURNPCS"));
                DouReturnCarat = DouReturnCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "RETURNCARAT"));
                DouExpCarat = DouExpCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "EXPCARAT"));
                
            }

            else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
               
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("EXPPER") == 0)
                {
                    e.TotalValue = DouIssueCarat == 0 ? 0 : Math.Round((DouExpCarat / DouIssueCarat) * 100, 4);
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RETURNPER") == 0)
                {
                    e.TotalValue = DouIssueCarat == 0 ? 0 : Math.Round((DouReturnCarat / DouIssueCarat) * 100, 4);
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("SIZE") == 0)
                {
                    e.TotalValue = DouIssuePcs == 0 ? 0 : Math.Round((DouIssueCarat / DouIssuePcs) , 4);
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DIFFPER") == 0)
                {
                    e.TotalValue = DouIssuePcs == 0 ? 0 : Math.Round(((DouExpCarat / DouIssueCarat) * 100) - ((DouReturnCarat / DouIssueCarat) * 100), 4) * -1;
                }
            }

        }




        private void FrmLedgerReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.ENGLISH);
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            GrdDet.BestFitColumns();
        }

        private void ChkCashBank_CheckedChanged(object sender, EventArgs e)
        {
            //GrdDet.Columns["LEDGERNAME"].Visible = ChkCashBank.Checked;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            MainGrid.DataSource = null;
            GrdDet.Columns.Clear();
            GrdDet.OptionsView.ShowBands = false;
            GrdDet.OptionsView.ShowGroupPanel = false;
            //GrdDet.Bands.Add();

            CmbGroup1.SelectedIndex = 0;
            CmbGroup2.SelectedIndex = 0;
            CmbGroup3.SelectedIndex = 0;

            ChkCmbKapan.Text = "";
            ChkCmbKapan.EditValue = null;

            ChkCmbProcess.Text = "";
            ChkCmbProcess.EditValue = null;

            ChkCmbRough.Text = "";
            ChkCmbRough.EditValue = null;

            DTPFromIssueDate.Text = string.Empty;
            DTPToIssueDate.Text = string.Empty;
            DTPFromIssueDate.Focus();
            FillControl();
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000" || e.DisplayText == "0.0000")
            {
                e.DisplayText = String.Empty;
            }
        }
       
    }
}