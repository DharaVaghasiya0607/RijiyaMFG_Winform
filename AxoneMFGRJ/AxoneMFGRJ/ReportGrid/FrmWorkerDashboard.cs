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
using BusLib.Master;
using AxoneMFGRJ.Transaction;
using BusLib.ReportGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Reflection;
using DevExpress.Data;
using DevExpress.XtraPrintingLinks;
using System.Drawing.Printing;
using BusLib.Rapaport;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using BusLib.View;
using OfficeOpenXml;
using DevExpress.XtraCharts;
using System.Drawing.Drawing2D;
using AxoneMFGRJ.ReportGrid;
using AxoneMFGRJ.View;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using AxoneMFGRJ.Salary;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmWorkerDashboard : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_ProductionAnalysis ObjView = new BOTRN_ProductionAnalysis();
        BOTRN_RunninPossition ObjRunning = new BOTRN_RunninPossition();

        DataTable DtabCompDetail = new DataTable();
        DataTable DTabOSDetail = new DataTable();
        DataRow DRowMain = null;

        DataTable DTabRoughMakable = new DataTable();

        DataTable DTabMarkerLabourDetail = new DataTable();
        DataTable DTabCurrentRolling = new DataTable();
        DataTable DTabPlanVariationDetail = new DataTable();

        DataTable DTabSalaryViewSummary = new DataTable();
        DataTable DTabSalaryViewDetail = new DataTable();
        DataTable DTabSalaryViewYearMonthWise = new DataTable();
        DataTable DTabSalaryViewPlanVariationDetail = new DataTable();
        DataTable DTabSalaryViewDFPlusMinusDetail = new DataTable();
        DataTable DTabManagerDetail = new DataTable();

        double DouTotalPlanVarDollar = 0;
        double DouTotalPlanVarRupees = 0;
        double DouTotalDFPlusDollar = 0;
        double DouTotalDFMinusDollar = 0;
        double DouTotalDFPlusRupees = 0;
        double DouTotalDFMinusRupees = 0;

        double DouTotalDPlusDollar_BeforePer = 0;
        double DouTotalDMinusDollar_BeforePer = 0;
        double DouTotalDFPlusDollar_BeforePer = 0;
        double DouTotalDFMinusDollar_BeforePer = 0;

        double DouTotalORGAmount = 0;
        double DouTotalGRDAmount = 0;
        double DouTotalDPlusDollar = 0;
        double DouTotalDMinusDollar = 0;
        double DouTotalDPlusRupees = 0;
        double DouTotalDMinusRupees = 0;

        double DouTotalFinalLabourDollar = 0;
        double DouTotalFinalLabourRupees = 0;

        double DouTotalPcs = 0;


        string mStrFromDate = string.Empty;
        string mStrToDate = string.Empty;

        Int64 mIntEmpID = 0;
        Int64 Manager_ID = 0;

        public FORMTYPE mFormType = FORMTYPE.ADMIN;

        public enum FORMTYPE
        {
            WORKER = 0,
            ADMIN = 1,
            MANAGER = 2

        }


        #region Property Settings

        public FrmWorkerDashboard()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType)
        {
            mFormType = pFormType;
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            if (mFormType == FORMTYPE.MANAGER)
            {
                xtraTabManSummury.PageVisible = true;
                xtraTabManDetail.PageVisible = true;
                PnlLabourDetail.Visible = false;
                this.Text = "WORKER DASHBOARD(MANAGER)";
            }
            else
            {
                xtraTabManSummury.PageVisible = false;
                xtraTabManDetail.PageVisible = false;
                PnlLabourDetail.Visible = true;
                this.Text = "WORKER DASHBOARD(WORKER)";
            }

            this.Show();

            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            DTPToDate.Value = System.DateTime.Now;

            txtEmpCode.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
            txtEmpCode.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
            txtEmpName.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME;

            if (
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.DESIGNATIONNAME == "ADMIN" ||
                BusLib.Configuration.BOConfiguration.gEmployeeProperty.DESIGNATIONNAME == "CLV HEAD"
                )
            {
                txtEmpCode.Enabled = true;
                txtEmpName.Enabled = true;
            }
            else
            {
                txtEmpCode.Enabled = false;
                txtEmpName.Enabled = false;
            }

            DTPFromDate.Focus();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjView);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjRunning);
            ObjFormEvent.ObjToDisposeList.Add(DtabCompDetail);
            ObjFormEvent.ObjToDisposeList.Add(DTabOSDetail);
            ObjFormEvent.ObjToDisposeList.Add(DRowMain);
            ObjFormEvent.ObjToDisposeList.Add(DTabRoughMakable);
            ObjFormEvent.ObjToDisposeList.Add(DTabMarkerLabourDetail);
            ObjFormEvent.ObjToDisposeList.Add(DTabCurrentRolling);

        }

        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                progressPanel1.Visible = true;
                BtnSearch.Enabled = false;
                mStrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                mStrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                mIntEmpID = Val.ToInt64(txtEmpCode.Tag);
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception E)
            {
                BtnSearch.Enabled = true;
                Global.Message(E.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void DispalyChart(DevExpress.XtraCharts.ChartControl ChartControl, DevExpress.XtraCharts.ViewType ViewType, DataTable dt, String X, String Y)
        {
            if (ViewType == ViewType.Line || ViewType == ViewType.Area || ViewType == ViewType.Bar)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains(X))
                    {
                        Series series1 = new Series("", ViewType);
                        ChartControl.Series.Clear();

                        double TotalStone = Convert.ToDouble(dt.Copy().Compute("Sum(" + Y + ")", ""));
                        DataView view = dt.Copy().DefaultView;
                        DataTable distinctValues = view.ToTable(true, X, Y);

                        series1.DataSource = distinctValues;
                        series1.ArgumentScaleType = ScaleType.Auto;
                        series1.ArgumentDataMember = X;
                        series1.ValueScaleType = ScaleType.Numerical;
                        series1.ValueDataMembers.AddRange(new string[] { Y });
                        ChartControl.Series.Add(series1);

                        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
                        ((DevExpress.XtraCharts.XYDiagram)ChartControl.Diagram).AxisX.Range.SideMarginsEnabled = true;
                        ((DevExpress.XtraCharts.XYDiagram)ChartControl.Diagram).AxisX.Range.Auto = false;


                    }
                }
            }
            else
            {
                DataView view = dt.Copy().DefaultView;
                DataTable distinctValues = view.ToTable(true, X, Y);
                DataTable Xtable = distinctValues.Copy().DefaultView.ToTable(true, X);
                ChartControl.Series.Clear();
                Series Pie3DSeries = new Series("", ViewType);

                var ownerGroups = distinctValues.AsEnumerable()
               .GroupBy(row => row.Field<string>(X));

                var dt2 = distinctValues.Clone();
                dt2.Columns[Y].DataType = typeof(decimal);
                var intColumns = dt2.Columns.Cast<DataColumn>()
                    .Where(c => c.DataType == typeof(decimal)).ToArray();
                foreach (var grp in ownerGroups)
                {
                    if (grp.Key != null && grp.Key != "")
                    {
                        var row = dt2.Rows.Add();
                        row.SetField(X, grp.Key);

                        foreach (DataColumn col in intColumns)
                        {
                            string Exp = X + "='" + grp.Key + "'";
                            double sum = Convert.ToDouble(distinctValues.Compute("sum(" + col + ")", Exp));
                            row.SetField(col, sum);
                        }
                    }
                }


                Pie3DSeries.DataSource = dt2;
                ChartControl.Series.Add(Pie3DSeries);
                Pie3DSeries.ArgumentScaleType = ScaleType.Auto;
                Pie3DSeries.ArgumentDataMember = X;
                Pie3DSeries.ValueScaleType = ScaleType.Numerical;
                Pie3DSeries.ValueDataMembers.AddRange(new string[] { Y });


                Pie3DSeries.Label.PointOptions.PointView = PointView.ArgumentAndValues;
                Pie3DSeries.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                Pie3DSeries.Label.PointOptions.ValueNumericOptions.Precision = 0;
                //((SimpleDiagram3D)ChartControl.Diagram).RuntimeRotation = true;
                //((SimpleDiagram3D)ChartControl.Diagram).RuntimeZooming = true;

                XYDiagram diagram = (XYDiagram)ChartControl.Diagram;
                diagram.AxisX.Title.TextColor = Color.FromArgb(64, 29, 6);
                diagram.AxisX.Title.Text = "Days";
                diagram.AxisY.Title.TextColor = Color.FromArgb(64, 29, 6);
                diagram.AxisY.Title.Text = "Working Hours";

            }
        }


        public void DispalyBARChart(DevExpress.XtraCharts.ChartControl ChartControl, DevExpress.XtraCharts.ViewType ViewType, DataSet dset, String X, String Y)
        {
            if (ViewType == ViewType.Area)
            {
                if (dset.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dset.Tables.Count; i++)
                    {
                        if (dset.Tables[i].Columns.Contains(dset.Tables[i].Columns[1].ColumnName))
                        {
                            Series series1 = new Series("", ViewType);
                            ChartControl.Series.Clear();

                            double TotalStone = Convert.ToDouble(dset.Tables[i].Copy().Compute("Max(" + dset.Tables[i].Columns[0] + ")", ""));
                            DataView view = dset.Tables[i].Copy().DefaultView;
                            DataTable distinctValues = view.ToTable(true, dset.Tables[i].Columns[1].ColumnName, dset.Tables[i].Columns[0].ColumnName);

                            series1.DataSource = distinctValues;
                            series1.ArgumentScaleType = ScaleType.Auto;
                            series1.ArgumentDataMember = dset.Tables[i].Columns[1].ColumnName;
                            series1.ValueScaleType = ScaleType.Numerical;
                            series1.ValueDataMembers.AddRange(new string[] { dset.Tables[i].Columns[0].ColumnName });
                            ChartControl.Series.Add(series1);

                            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
                            ((DevExpress.XtraCharts.XYDiagram)ChartControl.Diagram).AxisX.Range.SideMarginsEnabled = true;
                            ((DevExpress.XtraCharts.XYDiagram)ChartControl.Diagram).AxisX.Range.Auto = false;

                        }
                    }
                }
            }

        }

        private void txtEmpCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (txtEmpCode.Enabled == false)
                {
                    return;
                }
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmpCode.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        txtEmpCode.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmpName.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
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

        private void lblTotalHours_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblTotalHours.Width, lblTotalHours.Height);
            this.lblTotalHours.Region = new Region(path);
            //using (var pen = new Pen(Color.FromArgb(184, 197, 190), 6.0f))
            //    e.Graphics.DrawPath(pen, path);

        }
        private void lblTotalDays_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblTotalDays.Width, lblTotalDays.Height);
            this.lblTotalDays.Region = new Region(path);
            //using (var pen = new Pen(Color.FromArgb(184, 197, 190), 6.0f))
            //    e.Graphics.DrawPath(pen, path);
        }

        private void lblPresentHours_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblPresentHours.Width, lblPresentHours.Height);
            this.lblPresentHours.Region = new Region(path);
            //using (var pen = new Pen(Color.FromArgb(86, 145, 161), 5.0f))
            //    e.Graphics.DrawPath(pen, path);
        }

        private void lblPresentDays_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblPresentDays.Width, lblPresentDays.Height);
            this.lblPresentDays.Region = new Region(path);
            //using (var pen = new Pen(Color.FromArgb(86, 145, 161), 5.0f))
            //    e.Graphics.DrawPath(pen, path);
        }

        private void lblAbsentDays_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblAbsentDays.Width, lblAbsentDays.Height);
            this.lblAbsentDays.Region = new Region(path);
            //using (var pen = new Pen(lblAbsentDays.BackColor, 0.0f))
            //    e.Graphics.DrawPath(pen, path);
        }

        private void lblAbsentHours_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblAbsentHours.Width, lblAbsentHours.Height);
            this.lblAbsentHours.Region = new Region(path);
            //using (var pen = new Pen(Color.FromArgb(86, 145, 161), 5.0f))
            //    e.Graphics.DrawPath(pen, path);
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string Type = "";

                if (mFormType == FORMTYPE.WORKER)
                {
                    Type = "WORKER";
                }
                else
                {
                    Type = "MANAGER";
                }

                DataSet DS = ObjView.GetWorkerDashboardData(mIntEmpID, mStrFromDate, mStrToDate, Type);

                if (DS.Tables.Count <= 0)
                {
                    return;
                }

                if (DS.Tables[0].Rows.Count != 0)
                {
                    DRowMain = DS.Tables[0].Rows[0];
                }

                //DtabCompDetail = DS.Tables[1];
                //DTabRoughMakable = DS.Tables[2];
                //DTabOSDetail = DS.Tables[3];
                //DTabCurrentRolling = DS.Tables[4];
                //DTabMarkerLabourDetail = DS.Tables[5];
                //DTabPlanVariationDetail = DS.Tables[6];
                if (mFormType == FORMTYPE.WORKER)
                {
                    DTabSalaryViewSummary = DS.Tables[2];
                    DTabSalaryViewDetail = DS.Tables[4];
                }
                else
                {
                    DTabManagerDetail = DS.Tables[2];
                    DTabSalaryViewSummary = DS.Tables[3];
                    DTabSalaryViewDetail = DS.Tables[4];
                }
                ObjFormEvent.ObjToDisposeList.Add(DS);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
                return;
            }



        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                progressPanel1.Visible = false;
                BtnSearch.Enabled = true;

                if (DRowMain != null)
                {
                    txtEmpCode.Tag = Val.ToString(DRowMain["LEDGER_ID"]);
                    txtEmpCode.Text = Val.ToString(DRowMain["LEDGERCODE"]);
                    txtEmpName.Text = Val.ToString(DRowMain["LEDGERNAME"]);

                    txtDepartment.Text = Val.ToString(DRowMain["DEPARTMENTNAME"]);
                    txtDesgination.Text = Val.ToString(DRowMain["DESIGNATIONNAME"]);
                    txtMobileNo.Text = Val.ToString(DRowMain["MOBILENO"]);
                    txtManager.Text = Val.ToString(DRowMain["MANAGERCODE"]);

                    txtDOB.Text = Val.ToString(DRowMain["DOB"]);
                    txtDOJ.Text = Val.ToString(DRowMain["DOJ"]);
                    txtEmployeeType.Text = Val.ToString(DRowMain["EMPLOYEETYPE"]);

                    if (Val.ToString(DRowMain["DISPLAYIMAGE"]) == "BIRTHDAY")
                    {
                        PicWelCome.BackgroundImage = Properties.Resources.HappyBirthday;
                    }
                    else if (Val.ToString(DRowMain["DISPLAYIMAGE"]) == "ANNIVERSARY")
                    {
                        PicWelCome.BackgroundImage = Properties.Resources.HappyAnniversary;
                    }
                    else
                    {
                        PicWelCome.BackgroundImage = Properties.Resources.ThinkPositive;
                    }

                    byte[] OFFICELOGO = DRowMain["EMPPHOTO"] as byte[] ?? null;
                    if (OFFICELOGO != null)
                    {
                        using (MemoryStream ms = new MemoryStream(OFFICELOGO))
                        {
                            PicEmpPhoto.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        PicEmpPhoto.Image = null;
                    }

                    double DouTotalDays = Val.Val(DRowMain["TOTALDAYS"]);
                    double DouPresentDays = Val.Val(DRowMain["WDAYS"]);
                    double DouAbsentDays = Val.Val(DouTotalDays - DouPresentDays);

                    double DouTotalHours = Val.Val(DRowMain["TOTALHOURS"]);
                    double DouPresentHours = Val.Val(DRowMain["WHOURS"]);
                    double DouAbsentHours = Val.Val(DouTotalHours - DouPresentHours);

                    lblTotalDays.Text = "   Total   " + Val.ToString(DouTotalDays);
                    lblPresentDays.Text = "  Present   " + Val.ToString(DouPresentDays);
                    lblAbsentDays.Text = "  Absent   " + Val.ToString(DouAbsentDays);

                    lblTotalHours.Text = "  Total   " + Val.ToString(DouTotalHours);
                    lblPresentHours.Text = "  Present   " + Val.ToString(DouPresentHours);
                    lblAbsentHours.Text = "  Absent   " + Val.ToString(DouAbsentHours);
                }

                int IntTotalPcs = Val.ToInt(DTabSalaryViewSummary.Compute("SUM(TOTALPCS)", ""));
                double DouTotalCarat = Val.Val(DTabSalaryViewSummary.Compute("SUM(TOTALISSUECARAT)", ""));

                //int IntTotalPcsChk = Val.ToInt(DTabMarkerLabourDetail.Compute("COUNT(PACKETNO)", "PRDTYPENAME = 'CHK'"));
                //double DouTotalCaratChk = Val.Val(DTabMarkerLabourDetail.Compute("SUM(CARAT)", "PRDTYPENAME = 'CHK'"));
                //double DouTotalPlanVariation = Val.Val(DTabPlanVariationDetail.Compute("SUM(DIFF)", ""));

                double DouPlusDollar = Val.Val(DTabSalaryViewSummary.Compute("SUM(BEFOREPER_PLUSDOLLAR)", ""));
                double DouMinusDollar = Val.Val(DTabSalaryViewSummary.Compute("SUM(BEFOREPER_MINUSDOLLAR)", ""));

                int IntTotalArtistPlusPcs = Val.ToInt(DTabSalaryViewSummary.Compute("SUM(TOTAL_ARTISTPLUSPCS)", ""));
                int IntTotalArtistMinusPcs = Val.ToInt(DTabSalaryViewSummary.Compute("SUM(TOTAL_ARTISTMINUSPCS)", ""));

                int IntTotalBreakingMinusPcs = Val.ToInt(DTabSalaryViewDetail.Compute("COUNT(BEFOREPER_BREAKINGDOLLAR)", "BEFOREPER_BREAKINGDOLLAR <> 0"));

                double DouDFPlus = 0;
                double DouDFMinus = 0;

                double DouArtistPlusRupees = Val.Val(DTabSalaryViewSummary.Compute("SUM(ARTISTPLUSRUPEES)", ""));
                double DouArtistMinusRupees = Val.Val(DTabSalaryViewSummary.Compute("SUM(ARTISTMINUSRUPEES)", ""));

                double DouBreakingDollar = Val.Val(DTabSalaryViewSummary.Compute("SUM(BEFOREPER_BREAKINGDOLLAR)", ""));

                lblTotalPcs.Text = "Pcs\n" + IntTotalPcs.ToString();
                lblTotalCarat.Text = "Carat\n" + DouTotalCarat.ToString();

                lblDollarPlus.Text = "$ Plus(+)\n" + DouPlusDollar.ToString();
                lblDollarMinus.Text = "$ Minus(-)\n" + DouMinusDollar.ToString();

                lblArtistPlusRupees.Text = "$ Plus(+)\n" + DouArtistPlusRupees.ToString() + "(" + IntTotalArtistPlusPcs.ToString() + ")";
                lblArtistMinusRupees.Text = "$ Minus(-)\n" + DouArtistMinusRupees.ToString() + "(" + IntTotalArtistMinusPcs.ToString() + ")";

                lblBreakingMinusDollar.Text = "$ Minus(-)\n" + DouBreakingDollar.ToString() + "(" + IntTotalBreakingMinusPcs.ToString() + ")";

                if (mFormType == FORMTYPE.WORKER)
                {
                    MainGridLabour.DataSource = DTabSalaryViewDetail;
                    GrdDetLabour.RefreshData();
                    GrdDetLabour.BestFitColumns();
                }
                else
                {
                    MainGridDetail.DataSource = DTabSalaryViewDetail;
                    GrdDetail.RefreshData();
                    GrdDetail.BestFitColumns();

                    MainGridManagerSummary.DataSource = DTabManagerDetail;
                    GrdManagerSummary.RefreshData();
                }
                
                //lblDFMinus.Text = "DF Minus(-)\n" + DouDFMinus.ToString();
                //lblPlanVariation.Text = "Plan Variation $\n" + DouTotalPlanVariation.ToString();
                //lblTotalPcsChecker.Text = "Pcs : " + IntTotalPcsChk.ToString() + "\nCarat : " + DouTotalCaratChk.ToString();

            }
            catch (Exception ex)
            {

            }
        }

        private void FrmMarkerDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BtnSearch_Click(null, null);
            }
        }


        private void MainGridRoughMakable_Paint(object sender, PaintEventArgs e)
        {
            //GridControl gridC2 = sender as GridControl;
            //GridView gridView2 = gridC2.FocusedView as GridView;
            //BandedGridViewInfo info2 = (BandedGridViewInfo)gridView2.GetViewInfo();
            //for (int i = 0; i < info2.BandsInfo.BandCount; i++)
            //{
            //    e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info2.BandsInfo[i].Bounds.X + info2.BandsInfo[i].Bounds.Width, info2.BandsInfo[i].Bounds.Top), new Point(info2.BandsInfo[i].Bounds.X + info2.BandsInfo[i].Bounds.Width, info2.RowsInfo[info2.RowsInfo.Count - 1].Bounds.Bottom - 1));
            //    //if (i == 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            //    //if (i == info.BandsInfo.BandCount - 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            //}
        }

        private void GrdDetRoughMakable_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }




        private void GrdDetLabour_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0 && GrdDetLabour.FocusedColumn.FieldName == "PLANVARIATION")
            {
                DataRow DR = GrdDetLabour.GetDataRow(e.RowHandle);


                string StrKapan = Val.ToString(DR["KAPANNAME"]);
                int IntPacketNO = Val.ToInt(DR["PACKETNO"]);

                DataRow[] DRow = DTabPlanVariationDetail.Select("KapanName = '" + StrKapan + "' And PacketNo='" + IntPacketNO + "' ");
                if (DRow.Length != 0)
                {
                    DataTable DTab = DRow.CopyToDataTable();

                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "";
                    FrmSearch.mSearchText = "";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = DTab;
                    FrmSearch.mColumnsToHide = "Employee_ID,RNO";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                    ObjFormEvent.ObjToDisposeList.Add(DTab);
                }
                ObjFormEvent.ObjToDisposeList.Add(DRow);


            }
            else if (e.Clicks == 2 && e.RowHandle >= 0)
            {
                DataRow DR = GrdDetLabour.GetDataRow(e.RowHandle);

                string StrKapan = Val.ToString(DR["KAPANNAME"]);
                int IntPacketNO = Val.ToInt(DR["PACKETNO"]);
                string StrTag = Val.ToString(DR["TAG"]);
                string StrPacketID = Val.ToString(DR["PACKET_ID"]);
                string StrWorkerName = Val.ToString(DR["WORKERCODE"]);
                Int64 IntWorkerID = Val.ToInt64(DR["WORKER_ID"]);

                FrmFactoryLabourDetail FrmFactoryLabourDetail = new FrmFactoryLabourDetail();
                FrmFactoryLabourDetail.MdiParent = Global.gMainRef;
                FrmFactoryLabourDetail.ShowForm(StrKapan, IntPacketNO, StrTag, StrPacketID, StrWorkerName, IntWorkerID, "MARKER");
                ObjFormEvent.ObjToDisposeList.Add(FrmFactoryLabourDetail);
            }
        }

        private void MainGridLabour_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == info.BandsInfo.BandCount - 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            }
        }

        private void lblPlanVariation_Click(object sender, EventArgs e)
        {
            FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
            FrmSearch.mSearchField = "";
            FrmSearch.mSearchText = "";
            this.Cursor = Cursors.WaitCursor;
            FrmSearch.mDTab = DTabPlanVariationDetail;
            FrmSearch.mColumnsToHide = "Employee_ID,RNO";
            this.Cursor = Cursors.Default;
            FrmSearch.ShowDialog();
            if (FrmSearch.mDRow != null)
            {
            }

            FrmSearch.Hide();
            FrmSearch.Dispose();
            FrmSearch = null;
        }

        private void GrdDetLabour_CellMerge(object sender, CellMergeEventArgs e)
        {
            string MergeOnStr = "PLANVARIATION";
            string MergeOn = "KAPANPACKET";

            if (MergeOnStr.Contains(e.Column.FieldName))
            {
                string val1 = Val.ToString(GrdDetLabour.GetRowCellValue(e.RowHandle1, GrdDetLabour.Columns[MergeOn]));
                string val2 = Val.ToString(GrdDetLabour.GetRowCellValue(e.RowHandle2, GrdDetLabour.Columns[MergeOn]));
                if (val1 == val2)
                    e.Merge = true;
                e.Handled = true;
            }
        }

        private void lblSalaryViewTotalPcs_Click(object sender, EventArgs e)
        {
            try
            {

                if (DTabSalaryViewDetail.Rows.Count <= 0)
                    return;


                string StrTitle = string.Empty;
                StrTitle = "Salary Detail Of Employee : " + Val.ToString(txtEmpCode.Text) + "";

                FrmSalaryViewDetailEmployeeWise FrmSalaryViewDetailEmployeeWise = new FrmSalaryViewDetailEmployeeWise();
                FrmSalaryViewDetailEmployeeWise.DTabPacketWiseStock = DTabSalaryViewDetail;
                FrmSalaryViewDetailEmployeeWise.MdiParent = Global.gMainRef;
                ObjFormEvent.ObjToDisposeList.Add(FrmSalaryViewDetailEmployeeWise);
                FrmSalaryViewDetailEmployeeWise.ShowForm(StrTitle);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDetail_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalPlanVarDollar = 0;
                    DouTotalPlanVarRupees = 0;

                    DouTotalDFPlusDollar = 0;
                    DouTotalDFMinusDollar = 0;
                    DouTotalDFPlusRupees = 0;
                    DouTotalDFMinusRupees = 0;
                    DouTotalPcs = 0;

                    DouTotalORGAmount = 0;
                    DouTotalGRDAmount = 0;
                    DouTotalDPlusDollar = 0;
                    DouTotalDMinusDollar = 0;
                    DouTotalDPlusRupees = 0;
                    DouTotalDMinusRupees = 0;

                    DouTotalDPlusDollar_BeforePer = 0;
                    DouTotalDMinusDollar_BeforePer = 0;
                    DouTotalDFPlusDollar_BeforePer = 0;
                    DouTotalDFMinusDollar_BeforePer = 0;

                    DouTotalFinalLabourDollar = 0;
                    DouTotalFinalLabourRupees = 0;

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    string P1 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "KAPANPACKET"));
                    string P2 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle - 1, "KAPANPACKET"));
                    if (P1 != P2)
                    {
                        DouTotalPlanVarDollar = DouTotalPlanVarDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PLANVARIATIONDOLLAR"));
                        DouTotalPlanVarRupees = DouTotalPlanVarRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PLANVARIATIONRUPEES"));

                        DouTotalDFPlusDollar = DouTotalDFPlusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFPLUSDOLLAR"));
                        DouTotalDFMinusDollar = DouTotalDFMinusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFMINUSDOLLAR"));
                        DouTotalDFPlusRupees = DouTotalDFPlusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFPLUSRUPEES"));
                        DouTotalDFMinusRupees = DouTotalDFMinusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFMINUSRUPEES"));
                        DouTotalPcs = DouTotalPcs + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PCS"));   //#P : 21-01-2020


                        //#P : 06-02-2020
                        DouTotalFinalLabourDollar = DouTotalFinalLabourDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "FINAL_LABOURAMOUNTDOLLAR"));
                        DouTotalFinalLabourRupees = DouTotalFinalLabourRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "FINAL_LABOURAMOUNTRUPEES"));

                        DouTotalDPlusDollar_BeforePer = DouTotalDPlusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_PLUSDOLLAR"));
                        DouTotalDMinusDollar_BeforePer = DouTotalDMinusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_MINUSDOLLAR"));

                        DouTotalDFPlusDollar_BeforePer = DouTotalDFPlusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_DFPLUSDOLLAR"));
                        DouTotalDFMinusDollar_BeforePer = DouTotalDFMinusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_DFMINUSDOLLAR"));

                        DouTotalORGAmount = DouTotalORGAmount + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "TOTALORGAMOUNT"));
                        DouTotalGRDAmount = DouTotalGRDAmount + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "TOTALGRDAMOUNT"));
                        DouTotalDPlusDollar = DouTotalDPlusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PLUSDOLLAR"));
                        DouTotalDMinusDollar = DouTotalDMinusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "MINUSDOLLAR"));
                        DouTotalDPlusRupees = DouTotalDPlusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DOLLARPLUSRUPEES"));
                        DouTotalDMinusRupees = DouTotalDMinusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DOLLARMINUSRUPEES"));
                        //End : #P : 06-02-2020

                    }

                    //string P3 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "PACKETTAG"));
                    //string P4 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle - 1, "PACKETTAG"));
                    //if (P3 != P4)
                    //{
                    //    DouTotalPcs = DouTotalPcs + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PCS"));   //#P : 21-01-2020
                    //}
                }

                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PLANVARIATIONDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalPlanVarDollar;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PLANVARIATIONRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalPlanVarRupees, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFPLUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDFPlusDollar;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFMINUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDFMinusDollar;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFPLUSRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDFPlusRupees, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFMINUSRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDFMinusRupees, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFTYPE") == 0)
                    {
                        e.TotalValue = DouTotalPcs;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("TOTALGRDAMOUNT") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalGRDAmount, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("TOTALORGAMOUNT") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalORGAmount, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PLUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDPlusDollar, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("MINUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDMinusDollar, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DOLLARPLUSRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDPlusRupees, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DOLLARMINUSRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDMinusRupees, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_PLUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDPlusDollar_BeforePer, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_MINUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDMinusDollar_BeforePer, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_DFPLUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDFPlusDollar_BeforePer, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_DFMINUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDFMinusDollar_BeforePer, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FINAL_LABOURAMOUNTDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalFinalLabourDollar, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FINAL_LABOURAMOUNTRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalFinalLabourRupees, "###0.00");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GrdManagerSummary_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0)
            {
                this.Cursor = Cursors.WaitCursor;
                GrdDetail.Columns["WORKERCODE"].ClearFilter();
                GrdDetail.Columns["WORKERCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("WORKERCODE='" + Val.ToString(GrdManagerSummary.GetRowCellValue(e.RowHandle, "WORKERCODE")) + "'");
                xtraTabControl1.SelectedTabPageIndex = 1;
                this.Cursor = Cursors.Default;
            }
        }

        private void MainGridManagerSummary_Click(object sender, EventArgs e)
        {

        }

        private void GrdManagerSummary_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }




    }
}
