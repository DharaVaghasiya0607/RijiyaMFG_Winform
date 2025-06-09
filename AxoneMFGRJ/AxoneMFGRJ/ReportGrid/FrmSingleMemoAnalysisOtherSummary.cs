using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmSingleMemoAnalysisOtherSummary : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjView = new BOTRN_RunninPossition();

        DataTable DTabDetail = new DataTable();
        DataTable DTabSummary = new DataTable();

        double DblWeight = 0, DblRPcs = 0, DblAvgSize = 0, DblPurRate = 0, DblPurAmt = 0, DblByAvg = 0, DblByAmt = 0, DblByPer = 0;
        double DblFinAvg = 0, DblFinAmt = 0, DblFinPer = 0, DblLabAvg = 0, DblLabAmt = 0, DblLabPer = 0, DblSalAvg = 0, DblSalPer = 0, DblSalAmt = 0, DblLabourPer = 0, DblLabourAmt = 0;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        string mStrReportTitle = "";

        public FrmSingleMemoAnalysisOtherSummary()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
        }

        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjView);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbKapan.Text.Trim().Length == 0)
                {
                    Global.Message("Please Select Kapan...");
                    CmbKapan.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                MainGrid.DataSource = null;
                DTabSummary.Rows.Clear();

                DTabSummary = ObjView.GetDataForSinglMemoAnalysisForOtherSummary(Val.Trim(CmbKapan.Properties.GetCheckedItems()));

                MainGrid.DataSource = DTabSummary;
                MainGrid.RefreshDataSource();
                GrdDet.BestFitColumns();
                this.Cursor = Cursors.Default;

            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
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
            TextBrick BrickTitleseller = e.Graph.DrawString(mStrReportTitle, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
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

        private void lblPrint_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Factory Production Report (Employee Wise)";
            CommonPrintFuction(MainGrid, GrdDet);

        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Factory Production Report (Employee Wise)";
            CommonExcelExportFuction(MainGrid, GrdDet, "ProductionReport");
        }

        private void GrdDetDept_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }


        public void CommonPrintFuction(
           DevExpress.XtraGrid.GridControl pMainGrid,
           DevExpress.XtraGrid.Views.BandedGrid.BandedGridView pGrdDet

           )
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = pMainGrid;
                link.Landscape = false;

                //GrdDetDept.BestFitColumns();
                //GrdDetDept.OptionsPrint.AutoWidth = true;

                //foreach (System.Drawing.Printing.PaperKind foo in Enum.GetValues(typeof(System.Drawing.Printing.PaperKind)))
                //{
                //    if (Val.ToString(CmbPageKind.SelectedItem) == foo.ToString())
                //    {
                //        link.PaperKind = foo;
                //        link.PaperName = foo.ToString();

                //    }
                //}

                //}
                //if (Val.ToString(cmbExpand.SelectedItem) == "Yes")
                //{
                //    GridView1.OptionsPrint.ExpandAllGroups = true;
                //}
                //else
                //{
                //    GridView1.OptionsPrint.ExpandAllGroups = false;
                //}

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
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        public void CommonExcelExportFuction(
           DevExpress.XtraGrid.GridControl pMainGrid,
           DevExpress.XtraGrid.Views.BandedGrid.BandedGridView pGrdDet,
            string pStrFileName

           )
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




        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[] 
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }
        private void FrmFactoryProduction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BtnRefresh_Click(null, null);
            }
        }

        private void MainGrid_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            }
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Memo Analysis Summary(Kapan Wise)";
            CommonExcelExportFuction(MainGrid, GrdDet, "MemoAnalysisSummaryReport");
        }

        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

            if (e.IsTotalSummary)
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    DblWeight = 0; DblRPcs = 0; DblAvgSize = 0; DblPurRate = 0; DblPurAmt = 0; DblByAvg = 0; DblByAmt = 0; DblByPer = 0;
                    DblFinAvg = 0; DblFinAmt = 0; DblFinPer = 0; DblLabAvg = 0; DblLabAmt = 0; DblLabPer = 0; DblSalAvg = 0; DblSalPer = 0; DblSalAmt = 0; DblLabourPer = 0; DblLabourAmt = 0;
                }
                else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    DblWeight = DblWeight + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "KAPANCARAT"));
                    DblRPcs = DblRPcs + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "KAPANPCS"));
                    DblPurAmt = DblPurAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "ROUGHAMOUNT"));
                    DblByAmt = DblByAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BYAMT"));
                    DblLabourAmt = DblLabourAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BYLABOURAMOUNT"));
                    DblSalAmt = DblSalAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "SALEAMT"));
                    DblFinAmt = DblFinAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "FINALPRDAMT"));
                    DblLabAmt = DblLabAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "LABAMT"));
                }
                else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("KAPANAVG") == 0)
                    {
                        if (DblRPcs > 0)
                        {
                            DblAvgSize = Math.Round((DblWeight / DblRPcs), 2);
                            e.TotalValue = DblAvgSize;
                        }
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("ROUGHRATE") == 0)
                    {
                        if (DblWeight > 0)
                        {
                            DblPurRate = Math.Round((DblPurAmt / DblWeight), 2);
                            e.TotalValue = DblPurRate;
                        }
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BYAVG") == 0)
                    {
                        if (DblWeight > 0)
                        {
                            DblByAvg = Math.Round((DblByAmt / DblWeight), 2);
                            e.TotalValue = DblByAvg;
                        }
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BYPER") == 0)
                    {
                        if (Math.Round((DblByAmt / DblWeight), 2) > 0)
                        {
                            DblByPer = Math.Round(((Math.Round((DblByAmt / DblWeight), 2) - Math.Round((DblPurAmt / DblWeight), 2)) / Math.Round((DblByAmt / DblWeight), 2)) * 100, 2);
                            e.TotalValue = DblByPer;
                        }
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FINALPRDAVG") == 0)
                    {
                        if (DblWeight > 0)
                        {
                            DblFinAvg = Math.Round((DblFinAmt / DblWeight), 2);
                            e.TotalValue = DblFinAvg;
                        }
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FINALPRDPER") == 0)
                    {
                        if (Math.Round((DblFinAmt / DblWeight), 2) > 0)
                        {
                            DblFinPer = Math.Round(((Math.Round((DblFinAmt / DblWeight), 2) - Math.Round((DblPurAmt / DblWeight), 2)) / Math.Round((DblFinAmt / DblWeight), 2)) * 100, 2);
                            e.TotalValue = DblFinPer;
                        }
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("LABAVG") == 0)
                    {
                        if (DblWeight > 0)
                        {
                            DblLabAvg = Math.Round((DblLabAmt / DblWeight), 2);
                            e.TotalValue = DblLabAvg;
                        }
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("LABPER") == 0)
                    {
                        if (Math.Round((DblLabAmt / DblWeight), 2) > 0)
                        {
                            DblLabPer = Math.Round(((Math.Round((DblLabAmt / DblWeight), 2) - Math.Round((DblPurAmt / DblWeight), 2)) / Math.Round((DblLabAmt / DblWeight), 2)) * 100, 2);
                            e.TotalValue = DblLabPer;
                        }
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("SALEAVG") == 0)
                    {
                        if (DblWeight > 0)
                        {
                            DblSalAvg = Math.Round((DblSalAmt / DblWeight), 2);
                            e.TotalValue = DblSalAvg;
                        }
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("SALEPER") == 0)
                    {
                        if (Math.Round((DblSalAmt / DblWeight), 2) > 0)
                        {
                            DblSalPer = Math.Round(((Math.Round((DblSalAmt / DblWeight), 2) - Math.Round((DblPurAmt / DblWeight), 2)) / Math.Round((DblSalAmt / DblWeight), 2)) * 100, 2);
                            e.TotalValue = DblSalPer;
                        }
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BYLABOURPER") == 0)
                    {
                        if (DblPurAmt > 0)
                        {
                            DblLabourPer = Math.Round(((DblLabourAmt / DblPurAmt) * 100), 2);
                            e.TotalValue = DblLabourPer;
                        }
                    }


                }
            }
        }
        /// <summary>
        /// Kuldeep For Color To Cell Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                if (e.Column.FieldName == "TOT_LABEXPENSEPER" || e.Column.FieldName == "SALELABOURPER" || e.Column.FieldName == "SALEPER" || e.Column.FieldName == "LABPER" || e.Column.FieldName == "BYLABOURPER" || e.Column.FieldName == "BYPER" || e.Column.FieldName == "FINALPRDPER")
                {
                    decimal decPer = Val.ToDecimal(GrdDet.GetRowCellValue(e.RowHandle, e.Column.FieldName));
                    if (decPer > 0 )
                    {
                        e.Appearance.ForeColor = Color.Green;
                    }
                    else if (decPer < 0)
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }
        }

    }
}
