using BusLib.Configuration;
using BusLib.Transaction;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmProductionReport : Form
    {
        public FrmProductionReport()
        {
            InitializeComponent();
        }
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        DataTable Dtab = new DataTable();
        BOTRN_ProductionReport ObjProd = new BOTRN_ProductionReport();

        string StrFromDate = null;
        string StrToDate = null;
        double NewCarat = 0.00;
        int Newkapan = 0;

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GrdReportSum.BeginUpdate();

                StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());

                Dtab.Rows.Clear();
                BtnSearch.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

                GrdReportSum.EndUpdate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
                return;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Dtab = ObjProd.ProductionRptGetData(StrFromDate, StrToDate);

            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;

                if (Dtab.Rows.Count <= 0)
                {
                    Global.Message("No Data Found..");
                }

                GrdReportSum.BeginUpdate();

                MainGridRow.DataSource = Dtab;
                MainGridRow.Refresh();

                GrdReportSum.EndUpdate();

            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTPFromDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void DTPToDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void ChkPcs_CheckedChanged(object sender, EventArgs e)
        {
            foreach (BandedGridColumn Col in GrdReportSum.Columns)
            {
                if (Col.FieldName.ToUpper().Contains("PCS"))
                {
                    Col.Visible = ChkPcs.Checked;
                }
                if (Col.FieldName.ToUpper().Contains("CARAT"))
                {
                    Col.Visible = ChkCarat.Checked;
                }
            }
        }

        private void FrmProductionReport_Load(object sender, EventArgs e)
        {
            Val.FormGeneralSetting(this);
            this.Show();
            ChkPcs_CheckedChanged(null, null);

            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            DTPToDate.Value = System.DateTime.Now;


        }

        private void lblExportSummary_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Production Summary Report", GrdReportSum);
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LblExportDetail_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Production Detail Report", GrdDetail);
        }

        private void lblPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {

                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                //string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

                link.Component = MainGridRow;
                link.Landscape = true;

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
                Global.Message(ex.Message.ToString());
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
            TextBrick BrickTitleseller = e.Graph.DrawString("PRODUCTION SUMMARY REPORT", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

        private void GrdReportSum_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0 || e.Column.FieldName == "Date")
                {
                    return;
                }

                if (e.Clicks == 2)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string StrDeptName = "";
                    string StrOpe = "";

                    if (e.Column.FieldName != "Date")
                    {
                        StrDeptName = e.Column.FieldName;
                    }

                    #region Get Operation Name
                    if (StrDeptName == "FINPCS" || StrDeptName == "FINCARAT")
                    {
                        StrOpe = "Final";
                    }
                    else if (StrDeptName == "CHKPCS" || StrDeptName == "CHKCARAT")
                    {
                        StrOpe = "Check";
                    }
                    else if (StrDeptName == "RMKPCS" || StrDeptName == "RMKCARAT")
                    {
                        StrOpe = "Rmk";
                    }
                    else if (StrDeptName == "FMKPCS" || StrDeptName == "FMKCARAT")
                    {
                        StrOpe = "Fmk";
                    }
                    else if (StrDeptName == "POKPCS" || StrDeptName == "POKCARAT")
                    {
                        StrOpe = "Pook";
                    }
                    else if (StrDeptName == "LSPCS" || StrDeptName == "LSCARAT")
                    {
                        StrOpe = "Ls";
                    }
                    #endregion

                    DataRow Dr = GrdReportSum.GetFocusedDataRow();

                    //DateTime StrDate = new DateTime();

                    //if (Val.ToString(Dr["ENTDATE"]).ToUpper() != "TOTAL")
                    //{
                    //    StrDate = Convert.ToDateTime(Dr["ENTDATE"]);
                    //}
                    //else
                    //{
                    //    StrDate = null;
                    //}

                    string StrDate = "";

                    if (Val.ToString(Dr["ENTDATE"]).ToUpper() != "TOTAL")
                    {
                        StrDate = Val.SqlDate(Val.ToString(Dr["ENTDATE"]));
                    }
                    else
                    {
                        StrDate = null;
                    }


                    #region Grid Format

                    if (StrOpe == "Fmk" || StrOpe == "Rmk")
                    {
                        KAPANBEND.Visible = true;
                        CARATFBAND.Visible = true;
                        PKTBAND.Visible = true;
                        PARABAND.Visible = true;

                        KapanBend2.Visible = false;
                        CARATRBEND.Visible = false;
                        PRDCTSBAND.Visible = false;
                        LSBANS.Visible = false;
                        ISSRETCTSBAND.Visible = false;
                    }

                    if (StrOpe == "Final" || StrOpe == "Check")
                    {
                        KAPANBEND.Visible = true;
                        CARATRBEND.Visible = true;
                        PRDCTSBAND.Visible = true;
                        PKTBAND.Visible = true;
                        PARABAND.Visible = true;

                        KapanBend2.Visible = false;
                        CARATFBAND.Visible = false;
                        LSBANS.Visible = false;
                        ISSRETCTSBAND.Visible = false;
                    }

                    if (StrOpe == "Ls")
                    {
                        KapanBend2.Visible = true;

                        ISSRETCTSBAND.Visible = true;
                        LSBANS.Visible = true;

                        KAPANBEND.Visible = false;
                        CARATFBAND.Visible = false;
                        PRDCTSBAND.Visible = false;
                        PKTBAND.Visible = false;
                        PARABAND.Visible = false;
                        CARATRBEND.Visible = false;
                    }

                    if (StrOpe == "Pook")
                    {
                        KAPANBEND.Visible = true;
                        ISSRETCTSBAND.Visible = true;
                        PARABAND.Visible = true;

                        KapanBend2.Visible = false;
                        LSBANS.Visible = false;
                        CARATFBAND.Visible = false;
                        CARATRBEND.Visible = false;
                        PRDCTSBAND.Visible = false;
                        PKTBAND.Visible = false;
                    }
                    #endregion

                    DataTable DtabDetail = ObjProd.GetDetailOfReport(StrDate, StrOpe, Val.SqlDate(Val.ToString(DTPFromDate.Text)), Val.SqlDate(Val.ToString(DTPToDate.Text)));

                    MainGrdDetail.DataSource = DtabDetail;
                    MainGrdDetail.RefreshDataSource();
                    this.Cursor = Cursors.Default;

                }

            }
            catch (Exception ex)
            {
                //return ex;
            }
        }

        public void GrdDetail_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    NewCarat = 0.00;
                    Newkapan = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    double P1 = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "CARAT2"));
                    double P2 = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle - 1, "CARAT2"));

                    string M1 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "KapanName"));
                    string M2 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle - 1, "KapanName"));

                    if (M1 != M2)
                    {
                        Newkapan = Newkapan + 1;
                    }

                    if (P1 != P2)
                    {
                        NewCarat = NewCarat + Val.Val(P1);
                    }
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("CARAT2") == 0)
                    {
                        e.TotalValue = NewCarat;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("KapanName") == 0)
                    {
                        e.TotalValue = Newkapan;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void LblDetailPrint_Click(object sender, EventArgs e)
        {
            try
            {

                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                //string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

                link.Component = MainGrdDetail;
                link.Landscape = true;

                link.Margins.Left = 10;
                link.Margins.Right = 10;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;

                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary2);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterAreaSummary2);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        public void Link_CreateMarginalHeaderAreaSummary2(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("PRODUCTION DETAIL REPORT", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

        public void Link_CreateMarginalFooterAreaSummary2(object sender, CreateAreaEventArgs e)
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

        private void GrdReportSum_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                string StrType = Val.ToString(GrdReportSum.GetRowCellValue(e.RowHandle, "ENTDATE"));

                if (StrType.ToUpper() == "TOTAL")
                {
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkBlue;
                    e.Appearance.BackColor = Color.LightGray;
                    e.Appearance.BackColor2 = Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

		private void FrmProductionReport_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
			{
				BtnSearch.PerformClick();
			}
		}

    }
}
