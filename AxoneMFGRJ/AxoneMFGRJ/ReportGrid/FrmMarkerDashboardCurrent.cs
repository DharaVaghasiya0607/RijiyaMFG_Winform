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
    public partial class FrmMarkerDashboardCurrent : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_ProductionAnalysis ObjView = new BOTRN_ProductionAnalysis();
        BOTRN_RunninPossition ObjRunning = new BOTRN_RunninPossition();

        //DataTable DtabCompDetail = new DataTable();
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


        string mStrFromDate = string.Empty;
        string mStrToDate = string.Empty;

        Int64 mIntEmpID = 0;

        public FORMTYPE mFormType = FORMTYPE.ADMIN;

        public enum FORMTYPE
        {
            MARKER = 0,
            ADMIN = 1
        }


        #region Property Settings

        public FrmMarkerDashboardCurrent()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType)
        {
            mFormType = pFormType;
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
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
            ObjFormEvent.ObjToDisposeList.Add(DTabOSDetail);
            ObjFormEvent.ObjToDisposeList.Add(DRowMain);
            ObjFormEvent.ObjToDisposeList.Add(DTabRoughMakable);          
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
               
        private void GrdDetRolling_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (Val.ToString(GrdDetRolling.GetRowCellValue(e.RowHandle, "PROCESSNAME")).Contains("TOTAL"))
            {
                e.Appearance.BackColor = Color.Silver;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataSet DS = ObjView.GetMarkerDashboardDataCurrent(mIntEmpID.ToString(), mStrFromDate, mStrToDate);

                if (DS.Tables.Count <= 0)
                {
                    return;
                }

                if (DS.Tables[0].Rows.Count != 0)
                {
                    DRowMain = DS.Tables[0].Rows[0];
                }
                             
                DTabRoughMakable = DS.Tables[1];
                DTabOSDetail = DS.Tables[2];
                DTabCurrentRolling = DS.Tables[3];     

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

                    //lblTotalDays.Text = "   Total   " + Val.ToString(DouTotalDays);
                    //lblPresentDays.Text = "  Present   " + Val.ToString(DouPresentDays);
                    //lblAbsentDays.Text = "  Absent   " + Val.ToString(DouAbsentDays);

                    //lblTotalHours.Text = "  Total   " + Val.ToString(DouTotalHours);
                    //lblPresentHours.Text = "  Present   " + Val.ToString(DouPresentHours);
                    //lblAbsentHours.Text = "  Absent   " + Val.ToString(DouAbsentHours);



                }

                //GrdDetTotal.BeginUpdate();
                //MainGrdTotal.DataSource = DtabCompDetail;
                //MainGrdTotal.Refresh();
                //GrdDetTotal.BestFitColumns();
                //GrdDetTotal.EndDataUpdate();

                GrdDetRoughMakable.BeginUpdate();
                MainGridRoughMakable.DataSource = DTabRoughMakable;
                GrdDetRoughMakable.RefreshData();
                GrdDetRoughMakable.BestFitColumns();
                GrdDetRoughMakable.EndUpdate();

                //GrdDetLabour.BeginUpdate();
                //MainGridLabour.DataSource = DTabMarkerLabourDetail;
                //MainGridLabour.Refresh();
                //GrdDetLabour.BestFitColumns();
                //GrdDetLabour.EndUpdate();

                GrdDetOS.BeginUpdate();
                MainGridOS.DataSource = DTabOSDetail;
                MainGridOS.Refresh();
                GrdDetOS.BestFitColumns();
                GrdDetOS.EndUpdate();

                GrdDetRolling.BeginUpdate();
                MainGridRolling.DataSource = DTabCurrentRolling;
                MainGridRolling.Refresh();
                GrdDetRolling.BestFitColumns();
                GrdDetRolling.EndUpdate();


                //int IntTotalPcs = Val.ToInt(DTabMarkerLabourDetail.Compute("COUNT(PACKETNO)", "PRDTYPENAME = 'MAR'"));
                //double DouTotalCarat = Val.Val(DTabMarkerLabourDetail.Compute("SUM(CARAT)", "PRDTYPENAME = 'MAR'"));

                //int IntTotalPcsChk = Val.ToInt(DTabMarkerLabourDetail.Compute("COUNT(PACKETNO)", "PRDTYPENAME = 'CHK'"));
                //double DouTotalCaratChk = Val.Val(DTabMarkerLabourDetail.Compute("SUM(CARAT)", "PRDTYPENAME = 'CHK'"));
                //double DouTotalPlanVariation = Val.Val(DTabPlanVariationDetail.Compute("SUM(DIFF)", ""));

                //double DouPlusDollar = Val.Val(DTabMarkerLabourDetail.Compute("SUM(PLUSDOLLAR)", ""));
                //double DouMinusDollar = Val.Val(DTabMarkerLabourDetail.Compute("SUM(MINUSSDOLLAR)", ""));
                //double DouDFPlus = 0;
                //double DouDFMinus = 0;

                //lblTotalPcsFinalPrd.Text = "Pcs : " + IntTotalPcs.ToString() + "\nCarat : " + DouTotalCarat.ToString();
                //lblTotalPcsChecker.Text = "Pcs : " + IntTotalPcsChk.ToString() + "\nCarat : " + DouTotalCaratChk.ToString();

                //lblDollarPlus.Text = "$ Plus(+)\n" + DouPlusDollar.ToString();
                //lblDollarMinus.Text = "$ Minus(-)\n" + DouMinusDollar.ToString();
                //lblDFPlus.Text = "DF Plus(+)\n" + DouDFPlus.ToString();
                //lblDFMinus.Text = "DF Minus(-)\n" + DouDFMinus.ToString();
                //lblPlanVariation.Text = "Plan Variation $\n" + DouTotalPlanVariation.ToString();


                //lblSalaryViewTotalPcs.Text = "  Pcs \n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["TOTALPCS"]).ToString();
                //lblSalaryViewTotalCarat.Text = "  Carat \n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["TOTALCARAT"]).ToString();
                //lblSalaryViewDollarPlus.Text = "  Plus(+)\n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["BEFOREPER_PLUSDOLLAR"]).ToString();
                //lblSalaryViewDollarMinus.Text = "  Minus(-)\n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["BEFOREPER_MINUSDOLLAR"]).ToString();
                //lblSalaryViewDFPlus.Text = "  DF(+)\n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["BEFOREPER_DFPLUSDOLLAR"]).ToString();
                //lblSalaryViewDFMinus.Text = "  DF(-)\n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["BEFOREPER_DFMINUSDOLLAR"]).ToString();

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

        private void GrdDetRolling_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {

                if (e.Clicks == 2)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string StrReportTitle = "";
                    string StrProcessName = "";

                    int IsWithOw = 1;

                    DataRow DRow = GrdDetRolling.GetDataRow(e.RowHandle);

                    if (Val.ToString(DRow["GRP"]) == "BEFORE FINAL")
                        IsWithOw = 0;

                    if (Val.ToString(DRow["PROCESSNAME"]) == "PLANNING TOTAL" ||
                        Val.ToString(DRow["PROCESSNAME"]) == "CHECKING TOTAL" ||
                        Val.ToString(DRow["PROCESSNAME"]) == "PLANNING PENDING" ||
                         Val.ToString(DRow["PROCESSNAME"]) == "PLANNING DONE" ||
                         Val.ToString(DRow["PROCESSNAME"]) == "CHECKING PENDING" ||
                         Val.ToString(DRow["PROCESSNAME"]) == "CHECKING DONE"
                         )
                    {
                        IsWithOw = 0;
                        StrProcessName = Val.ToString(DRow["PROCESSNAME"]);
                    }

                    if (Val.ToString(DRow["PROCESSNAME"]) != "WITHOUT OWNERSHIP" && Val.ToString(DRow["PROCESSNAME"]) != "WITH OWNERSHIP" && Val.ToString(DRow["PROCESSNAME"]) != "EMPLOYEECODE")
                        StrProcessName = Val.ToString(DRow["PROCESSNAME"]);

                    if (Val.ToString(DRow["PROCESSNAME"]).ToUpper().Equals("SELF STOCK (OWN FINAL)"))
                    {
                        StrProcessName = string.Empty;
                        IsWithOw = 2;
                    }
                    else if (Val.ToString(DRow["PROCESSNAME"]).ToUpper().Equals("SELF STOCK (OTH FINAL)"))
                    {
                        StrProcessName = string.Empty;
                        IsWithOw = 3;
                    }

                    if (Val.ToString(DRow["PROCESSNAME"]).ToUpper().Equals("TOTAL PHYSICAL"))
                    {
                        StrProcessName = string.Empty;
                        IsWithOw = -1;
                    }

                    DataTable DtabRollingDetail = ObjRunning.GetMarkerRollingReport("", Val.ToInt64(txtEmpCode.Tag), "DETAIL", IsWithOw, StrProcessName, "", 0, 0, "");
                    ObjFormEvent.ObjToDisposeList.Add(DtabRollingDetail);

                    if (DtabRollingDetail.Rows.Count == 0)
                    {
                        Global.Message("No Record Found");
                        return;
                    }

                    StrReportTitle = txtEmpCode.Text + " : " + StrProcessName + " Stock";

                    FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                    FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                    FrmRunningPossitionPopupDetail.DTabPacketWiseStock = DtabRollingDetail;
                    FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void GrdDetOS_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {

                if (e.Clicks == 2)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string StrReportTitle = "";

                    DataRow DRow = GrdDetOS.GetDataRow(e.RowHandle);
                    string StrProcessName = Val.ToString(DRow["TYPE"]);

                    DataTable DtabRollingDetail = ObjRunning.GetPopupDetail(Val.ToString(DRow["TYPE"]), Val.ToInt64(txtEmpCode.Tag), null, 0, 0);
                    ObjFormEvent.ObjToDisposeList.Add(DtabRollingDetail);

                    if (DtabRollingDetail.Rows.Count == 0)
                    {
                        Global.Message("No Record Found");
                        return;
                    }

                    StrReportTitle = txtEmpCode.Text + " : " + StrProcessName + " Stock";

                    FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                    FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                    FrmRunningPossitionPopupDetail.DTabPacketWiseStock = DtabRollingDetail;
                    FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
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

        private void GrdDetRoughMakable_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {

                if (e.Clicks == 2)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string StrReportTitle = "";
                    string StrProcessName = "";


                    DataRow DRow = GrdDetRoughMakable.GetDataRow(e.RowHandle);
                    string StrDate = Val.SqlDate(Val.ToString(DRow["ENTRYDATE"]));
                    if (e.Column.FieldName.ToUpper() == "RMAKPCS" || e.Column.FieldName.ToUpper() == "RMAKCARAT")
                    {
                        StrReportTitle = "Rough Makable Done  On " + Val.ToString(DRow["ENTRYDATE"]);
                        StrProcessName = "RMAK";
                    }
                    else if (e.Column.FieldName.ToUpper() == "MAKPCS" || e.Column.FieldName.ToUpper() == "MAKCARAT")
                    {
                        StrReportTitle = "Makable Done On " + Val.ToString(DRow["ENTRYDATE"]);
                        StrProcessName = "MAK";
                    }
                    else if (e.Column.FieldName.ToUpper() == "GRDPCS" || e.Column.FieldName.ToUpper() == "GRDPCS")
                    {
                        StrReportTitle = "Grading Done On " + Val.ToString(DRow["ENTRYDATE"]);
                        StrProcessName = "GRD";
                    }
                    else if (e.Column.FieldName.ToUpper() == "BYPCS" || e.Column.FieldName.ToUpper() == "BYCARAT")
                    {
                        StrReportTitle = "BY Done On " + Val.ToString(DRow["ENTRYDATE"]);
                        StrProcessName = "BY";
                    }
                    else if (e.Column.FieldName.ToUpper() == "LABPCS" || e.Column.FieldName.ToUpper() == "LABCARAT")
                    {
                        StrReportTitle = "Lab Done On " + Val.ToString(DRow["ENTRYDATE"]);
                        StrProcessName = "LAB";
                    }
                    DataTable DtabRollingDetail = ObjRunning.GetPopupDetail(StrProcessName, Val.ToInt64(txtEmpCode.Tag), StrDate, 0, 0);
                    ObjFormEvent.ObjToDisposeList.Add(DtabRollingDetail);

                    if (DtabRollingDetail.Rows.Count == 0)
                    {
                        Global.Message("No Record Found");
                        return;
                    }

                    FrmMarkerGradingComparisionPopupDetail FrmMarkerGradingComparisionPopupDetail = new FrmMarkerGradingComparisionPopupDetail();
                    FrmMarkerGradingComparisionPopupDetail.DTabPacketWiseStock = DtabRollingDetail;
                    FrmMarkerGradingComparisionPopupDetail.MdiParent = Global.gMainRef;
                    ObjFormEvent.ObjToDisposeList.Add(FrmMarkerGradingComparisionPopupDetail);
                    FrmMarkerGradingComparisionPopupDetail.ShowForm(StrReportTitle, StrProcessName);

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

      
    }
}
