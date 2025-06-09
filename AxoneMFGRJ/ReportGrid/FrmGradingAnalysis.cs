using BusLib.Configuration;
using BusLib.ReportGrid;
using BusLib.Transaction;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmGradingAnalysis : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_GradingAnalysis Obj = new BOTRN_GradingAnalysis();
        DataSet DS = new DataSet();

        DataTable DTabColor = new DataTable();
        DataTable DTabClarity = new DataTable();
        DataTable DTabCut = new DataTable();
        DataTable DTabPol = new DataTable();
        DataTable DTabSym = new DataTable();
        DataTable DTabFl = new DataTable();
        DataTable DTabCps = new DataTable();


        string StrFromDate = "";
        string StrToDate = "";
        string StrType = "";
        string StrLab = "";
        string StrMonthly = "";
        string StrGroup = "";
        string StrKapan = "";

        public FrmGradingAnalysis()
        {
            InitializeComponent();
        }

        string mStrReportTitle = "";

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
            CmbKapan.Focus();

            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            DTPToDate.Value = System.DateTime.Now;

            CmbType.SelectedIndex = 0;

            CmbGroup.SelectedIndex = 0;

            CmbLab.SelectedIndex = 0;

            CmbMonthGroup.SelectedIndex = 0;

            CmbKapan.Focus();

        }
        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(Obj);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        private void FrmGridingAnalysis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BtnRefresh.PerformClick();
                BtnBestFit.PerformClick();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DS = Obj.GetDataForGradingAnalysis(StrFromDate, StrToDate, StrType, StrLab, StrMonthly, StrGroup, StrKapan);
            }
            catch (Exception ex)
            {
                progressPanel1.Visible = false;
                BtnRefresh.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                progressPanel1.Visible = true;

                DTabColor = DS.Tables[0].Copy();
                DTabClarity = DS.Tables[1].Copy();
                DTabCut = DS.Tables[2].Copy();
                DTabPol = DS.Tables[3].Copy();
                DTabSym = DS.Tables[4].Copy();
                DTabFl = DS.Tables[5].Copy();
                DTabCps = DS.Tables[5].Copy();


                xtraTabControl1.SelectedTabPageIndex = 0;
                xtraTabControl1_SelectedPageChanged(xtraTabControl1, null);

                progressPanel1.Visible = false;
                BtnRefresh.Enabled = true;

            }
            catch (Exception ex)
            {
                progressPanel1.Visible = false;
                BtnRefresh.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }


        #region EXEL-EXPORT AND PRINT FUNCTIONS

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

            string StrFilter = "FromDate : " + DTPFromDate.Text + " To " + DTPToDate.Text;

            //// ' For Filter 
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

        public void Link_CreateMarginalHeaderAreaSummary(object sender, CreateAreaEventArgs e)
        {
            string StrName = "Griding Analysis ["+xtraTabControl1.SelectedTabPage.Text.Trim()+"]";;

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString(StrName, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "GridingAnalysis("+xtraTabControl1.SelectedTabPage.Text.Trim()+").xlsx";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrdCut,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXlsx(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [Griding Analysis(" + xtraTabControl1.SelectedTabPage.Text.Trim() + ").xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                //string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

                link.Component = MainGrdCut;
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
        #endregion

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            MainGrdCut.BestFit();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            progressPanel1.Visible = true;

            StrFromDate = Val.SqlDate(DTPFromDate.Text);
            StrToDate = Val.SqlDate(DTPToDate.Text);
            StrType = Val.ToString(CmbType.Text);
            StrLab = Val.ToString(CmbLab.Text);
            StrMonthly = Val.ToString(CmbMonthGroup.Text);
            StrGroup = Val.ToString(CmbGroup.Text);
            StrKapan = CmbKapan.Text;


            if (StrGroup == "DON'T GROUP")
            {
                StrGroup = "DONT GROUP";

                pivotGridField22.Visible = false;
            }
            else
            {
                pivotGridField22.Visible = true;
                pivotGridField22.AreaIndex = 0;
            }
            if (StrMonthly == "DON'T GROUP")
            {
                StrMonthly = "DONT GROUP";
            }

            if (CmbMonthGroup.Text == "DON'T GROUP")
            {
                pivotGridField66.Visible = false;
            }
            else if (CmbMonthGroup.Text == "MONTHLY")
            {
                pivotGridField66.Visible = true;
                pivotGridField66.AreaIndex = 0;
            }

            BtnBestFit.PerformClick();

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void MainGrdCut_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            string StrClick = string.Empty;
            string StrTitle = string.Empty;

            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();

            string StrExp = "";
            string StrGrd = "";
            string StrID = "";
            string StrSize = "";
            string StrEmployee = "";

            if (xtraTabControl1.SelectedTabPage.Text.Contains("COLOR"))
            {
                StrExp = Val.ToString(ds.GetValue(0, "LABSEQNO"));
                StrGrd = Val.ToString(ds.GetValue(0, "MUMSEQNO"));
                StrID = Val.ToString(ds.GetValue(0, "GROUPTYPE"));
                StrSize = Val.ToString(ds.GetValue(0, "SIZE"));
                StrEmployee = Val.ToString(ds.GetValue(0, "EMPCODE"));

                StrClick = "COLOR";
                StrTitle = "Color Comparision Of Lab Prd [" + StrExp + "]  Vs Mumbai Prd [" + StrGrd + "]";
            }
            else if (xtraTabControl1.SelectedTabPage.Text.Contains("CLARITY"))
            {
                StrExp = Val.ToString(ds.GetValue(0, "LABSEQNO"));
                StrGrd = Val.ToString(ds.GetValue(0, "MUMSEQNO"));
                StrID = Val.ToString(ds.GetValue(0, "GROUPTYPE"));
                StrSize = Val.ToString(ds.GetValue(0, "SIZE"));

                StrClick = "CLARITY";
                StrTitle = "Clarity Comparision Of Lab Prd [" + StrExp + "]  Vs Mumbai Prd [" + StrGrd + "]";
            }
            else if (xtraTabControl1.SelectedTabPage.Text.Contains("CUT"))
            {
                StrExp = Val.ToString(ds.GetValue(0, "LABSEQNO"));
                StrGrd = Val.ToString(ds.GetValue(0, "MUMSEQNO"));
                StrID = Val.ToString(ds.GetValue(0, "GROUPTYPE"));
                StrSize = Val.ToString(ds.GetValue(0, "SIZE"));

                StrClick = "CUT";
                StrTitle = "Cut Comparision Of Lab Prd [" + StrExp + "]  Vs Mumbai Prd [" + StrGrd + "]";
            }
            else if (xtraTabControl1.SelectedTabPage.Text.Contains("POL"))
            {
                StrExp = Val.ToString(ds.GetValue(0, "LABSEQNO"));
                StrGrd = Val.ToString(ds.GetValue(0, "MUMSEQNO"));
                StrID = Val.ToString(ds.GetValue(0, "GROUPTYPE"));
                StrSize = Val.ToString(ds.GetValue(0, "SIZE"));

                StrClick = "POL";
                StrTitle = "Pol Comparision Of Lab Prd [" + StrExp + "]  Vs Mumbai Prd [" + StrGrd + "]";
            }
            else if (xtraTabControl1.SelectedTabPage.Text.Contains("SYM"))
            {
                StrExp = Val.ToString(ds.GetValue(0, "LABSEQNO"));
                StrGrd = Val.ToString(ds.GetValue(0, "MUMSEQNO"));
                StrID = Val.ToString(ds.GetValue(0, "GROUPTYPE"));
                StrSize = Val.ToString(ds.GetValue(0, "SIZE"));

                StrClick = "SYM";
                StrTitle = "Sym Comparision Of Lab Prd [" + StrExp + "]  Vs Mumbai Prd [" + StrGrd + "]";
            }
            else if (xtraTabControl1.SelectedTabPage.Text.Contains("FL"))
            {
                StrExp = Val.ToString(ds.GetValue(0, "LABSEQNO"));
                StrGrd = Val.ToString(ds.GetValue(0, "MUMSEQNO"));
                StrID = Val.ToString(ds.GetValue(0, "GROUPTYPE"));
                StrSize = Val.ToString(ds.GetValue(0, "SIZE"));

                StrClick = "FL";
                StrTitle = "FL Comparision Of Lab Prd [" + StrExp + "]  Vs Mumbai Prd [" + StrGrd + "]";
            }

            StrGroup = Val.ToString(CmbGroup.Text);
            StrType = Val.ToString(CmbType.Text);
            StrLab = Val.ToString(CmbLab.Text);

            string StrFromDate = null;
            string StrToDate = null;
            int Shape_ID = 0;

            StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());

            StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());

            if (StrID == "ROUND")
            {
                Shape_ID = 1;
            }
            else
            {
                Shape_ID = 0;
            }
            if (StrGroup == "DON'T GROUP")
            {
                StrGroup = "DONT GROUP";
            }
            if (StrMonthly == "DON'T GROUP")
            {
                StrMonthly = "DONT GROUP";
            }

            this.Cursor = Cursors.WaitCursor;

            DataTable DTab = Obj.GerGradingComparisionDetail(StrFromDate, StrToDate, StrClick, StrExp, StrGrd, Shape_ID, StrSize, StrGroup, StrType, StrLab);
            if (DTab.Rows.Count == 0)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            FrmMarkerGradingComparisionPopupDetail FrmMarkerGradingComparisionPopupDetail = new FrmMarkerGradingComparisionPopupDetail();
            FrmMarkerGradingComparisionPopupDetail.DTabPacketWiseStock = DTab;
            FrmMarkerGradingComparisionPopupDetail.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmMarkerGradingComparisionPopupDetail);
            FrmMarkerGradingComparisionPopupDetail.ShowForm(StrTitle, StrClick);

            this.Cursor = Cursors.Default;

        }

        #region DRAWCELL EVENTS

        private void MainGrdCut_CustomDrawCell(object sender, PivotCustomDrawCellEventArgs e)
        {
            try
            {
                if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    e.Appearance.BackColor = Color.LightSkyBlue;
                }
                else if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                {
                    e.Appearance.BackColor = Color.LightGray;
                }

                if (Val.ToString(e.DataField.FieldName) == "PER")
                {
                    PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
                    int Sequence;
                    int Sequence1;

                    Sequence = Val.ToInt(ds.GetValue(0, "LABSEQNO"));
                    Sequence1 = Val.ToInt(ds.GetValue(0, "MUMSEQNO"));

                    if (Sequence1 > Sequence)
                    {
                        e.Appearance.BackColor = lblRed.BackColor;
                        if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000" || e.DisplayText == "")
                        {
                            e.Appearance.ForeColor = lblRed.BackColor;
                        }
                        if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                        {
                            e.Appearance.BackColor = Color.LightSkyBlue;
                        }
                        else if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                        {
                            e.Appearance.BackColor = Color.LightGray;
                        }

                    }
                    else if (Sequence1 < Sequence)
                    {
                        e.Appearance.BackColor = lblGreen.BackColor;
                        if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000" || e.DisplayText == "")
                        {
                            e.Appearance.ForeColor = lblGreen.BackColor;
                        }
                        if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                        {
                            e.Appearance.BackColor = Color.LightSkyBlue;
                        }
                        else if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                        {
                            e.Appearance.BackColor = Color.LightGray;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }
        }

        #endregion

        private void BtnClear_Click(object sender, EventArgs e)
        {

            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            DTPToDate.Value = System.DateTime.Now;

            DTabColor.Rows.Clear();
            DTabClarity.Rows.Clear();
            DTabCut.Rows.Clear();
            DTabPol.Rows.Clear();
            DTabSym.Rows.Clear();
            DTabFl.Rows.Clear();
            DTabCps.Rows.Clear();

            MainGrdCut.DataSource = DTabCut;
            
            CmbKapan.Properties.Items.Clear();
            CmbKapan.Text = "";

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
            CmbKapan.Focus();

            CmbType.SelectedIndex = 0;

            CmbGroup.SelectedIndex = 0;

            CmbLab.SelectedIndex = 0;

            CmbMonthGroup.SelectedIndex = 0;
        }


        private void MainGrdCut_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "LAB")
            {
                if (e.Value1 == null || e.Value2 == null) return;
                e.Handled = true;
                int s1 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "LABSEQNO"));
                int s2 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "LABSEQNO"));
                e.Result = Comparer.Default.Compare(s1, s2);
                e.Handled = true;
            }

            if (e.Field.FieldName == "MUM")
            {
                if (e.Value1 == null || e.Value2 == null) return;
                e.Handled = true;
                int s1 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "MUMSEQNO"));
                int s2 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "MUMSEQNO"));
                e.Result = Comparer.Default.Compare(s1, s2);
                e.Handled = true;
            }
        }

        private void MainGrdCut_CustomCellDisplayText(object sender, PivotCellDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000" || e.DisplayText == "0.00%")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void MainGrdCut_CustomCellValue(object sender, PivotCellValueEventArgs e)
        {
            if (object.ReferenceEquals(e.DataField, pDataFieldPer))
            {
                object summaryValue = 0;
                object[] rowValues;
                if (e.ColumnValueType == PivotGridValueType.GrandTotal)
                {
                    if (e.GetRowFields().Length == 4)
                    {
                        rowValues = new object[] { e.GetFieldValue(e.GetRowFields()[0]).ToString(), e.GetFieldValue(e.GetRowFields()[1]).ToString(), e.GetFieldValue(e.GetRowFields()[2]).ToString() };
                    }
                    else if (e.GetRowFields().Length == 3)
                    {
                        rowValues = new object[] { e.GetFieldValue(e.GetRowFields()[0]).ToString(), e.GetFieldValue(e.GetRowFields()[1]).ToString() };
                    }
                    else if (e.GetRowFields().Length == 2)
                    {
                        rowValues = new object[] { e.GetFieldValue(e.GetRowFields()[0]).ToString() };
                    }
                    else
                    {
                        rowValues = null;
                    }
                }
                else
                {
                    rowValues = e.GetRowFields().Select(f => e.GetFieldValue(f)).ToArray();
                }

                summaryValue = MainGrdCut.GetCellValue(null, rowValues, pDataFieldPcs);
                if (summaryValue is object || Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(summaryValue, 0, false)))
                {
                    if (Val.Val(summaryValue) != 0)
                    {
                        e.Value = Math.Round((Val.Val(e.GetCellValue(pDataFieldPcs)) / Val.Val(summaryValue)) * 100, 2);
                    }
                    else
                    {
                        e.Value = 0;
                    }
                    
                }
                else
                {
                    e.Value = 0;
                }
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Text.Contains("COLOR"))
            {
                MainGrdCut.DataSource = DTabColor;
                MainGrdCut.RefreshData();
            }
            else if (xtraTabControl1.SelectedTabPage.Text.Contains("CLARITY"))
            {
                MainGrdCut.DataSource = DTabClarity;
                MainGrdCut.RefreshData();
            }
            else if (xtraTabControl1.SelectedTabPage.Text.Contains("CUT"))
            {
                MainGrdCut.DataSource = DTabCut;
                MainGrdCut.RefreshData();
            }
            else if (xtraTabControl1.SelectedTabPage.Text.Contains("POL"))
            {
                MainGrdCut.DataSource = DTabPol;
                MainGrdCut.RefreshData();
            }
            else if (xtraTabControl1.SelectedTabPage.Text.Contains("SYM"))
            {
                MainGrdCut.DataSource = DTabSym;
                MainGrdCut.RefreshData();
            }
            else if (xtraTabControl1.SelectedTabPage.Text.Contains("FL"))
            {
                MainGrdCut.DataSource = DTabFl;
                MainGrdCut.RefreshData();
            }
        }


    }
}
