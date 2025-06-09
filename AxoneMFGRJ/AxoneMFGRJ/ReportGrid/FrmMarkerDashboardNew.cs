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
    public partial class FrmMarkerDashboardNew : DevExpress.XtraEditors.XtraForm
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
        //DataTable DTabSalaryViewYearMonthWise = new DataTable();
        //DataTable DTabSalaryViewPlanVariationDetail = new DataTable();
        //DataTable DTabSalaryViewDFPlusMinusDetail = new DataTable();

        DataTable DTabYearMonthWiseDetail = new DataTable();
        DataTable DTabYearMonthWiseSummary = new DataTable();

        string mStrFromDate = string.Empty;
        string mStrToDate = string.Empty;

        Int64 mIntEmpID = 0;

        double DouTotalPlanVarDollar = 0;
        double DouTotalPlanVarRupees = 0;
        double DouTotalDFPlusDollar = 0;
        double DouTotalDFMinusDollar = 0;
        double DouTotalDFPlusRupees = 0;
        double DouTotalDFMinusRupees = 0;
        double DouTotalPcs = 0;

        double DouTotalDPlusDollar_BeforePer = 0;
        double DouTotalDMinusDollar_BeforePer = 0;
        double DouTotalDFPlusDollar_BeforePer = 0;
        double DouTotalDFMinusDollar_BeforePer = 0;


        public FORMTYPE mFormType = FORMTYPE.ADMIN;

        public enum FORMTYPE
        {
            MARKER = 0,
            ADMIN = 1
        }


        #region Property Settings

        public FrmMarkerDashboardNew()
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

        private void lblTotalDays_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblTotalDays.Width, lblTotalDays.Height);
            this.lblTotalDays.Region = new Region(path);
            //using (var pen = new Pen(Color.FromArgb(184, 197, 190), 6.0f))
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


        private void GrdDetTotal_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;

                DataRow DR = GrdDetTotal.GetDataRow(e.RowHandle);
                if (DR == null || e.Clicks != 2)
                {
                    return;
                }

                string StrClick = string.Empty;
                string StrTitle = string.Empty;
                string StrExp = string.Empty;
                string StrGrd = string.Empty;

               
                string StrControlName = Val.ToString(DR["PARATYPE"]).ToUpper();

                StrClick = "TOTAL";
                //StrTitle = "Sym Comparision Of Exp [" + StrExp + "]  Vs Lab Grd [" + StrGrd + "]";

                string StrTitleType = Val.ToString(GrdDetTotal.FocusedColumn.FieldName).ToUpper() == "TOTALPCS" ? "All" : Val.ProperText(GrdDetTotal.FocusedColumn.FieldName);

                StrTitle = "Marker Grading Comparision Total Stone Detail of : " + Val.ToString(DR["PARATYPE"]) + " (" + StrTitleType + ")";

                StrExp = GrdDetTotal.FocusedColumn.FieldName;
                StrGrd = Val.ToString(DR["PARATYPE"]).ToUpper();

                this.Cursor = Cursors.WaitCursor;

                DataSet DS = new BOTRN_RunninPossition().GerGradingComparisionDetailWithLatestGrdByLabWithPivotBox("", mStrFromDate, mStrToDate, Val.ToInt64(txtEmpCode.Tag), StrClick, StrExp, StrGrd);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                FrmMarkerGradingComparisionPopupDetailWithPivotBox FrmMarkerGradingComparisionPopupDetailWithPivotBox = new FrmMarkerGradingComparisionPopupDetailWithPivotBox();
                FrmMarkerGradingComparisionPopupDetailWithPivotBox.DSPacketWiseStock = DS;
                FrmMarkerGradingComparisionPopupDetailWithPivotBox.MdiParent = Global.gMainRef;
                ObjFormEvent.ObjToDisposeList.Add(FrmMarkerGradingComparisionPopupDetailWithPivotBox);
                FrmMarkerGradingComparisionPopupDetailWithPivotBox.ShowForm(StrTitle, StrGrd, mStrFromDate, mStrToDate, Val.ToInt64(txtEmpCode.Tag));

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataSet DS = ObjView.GetMarkerDashboardDataNew(mIntEmpID.ToString(), mStrFromDate, mStrToDate);

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

                //DTabSalaryViewSummary = DS.Tables[8];
                DtabCompDetail = DS.Tables[1];
                DTabSalaryViewDetail = DS.Tables[2];
                DTabSalaryViewSummary = DS.Tables[3];
                DTabYearMonthWiseDetail = DS.Tables[4];
                DTabYearMonthWiseSummary = DS.Tables[5];
                
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
                }

                GrdDetTotal.BeginUpdate();
                MainGrdTotal.DataSource = DtabCompDetail;
                MainGrdTotal.Refresh();
                GrdDetTotal.BestFitColumns();
                GrdDetTotal.EndDataUpdate();

                GrdDetLabour.BeginUpdate();
                MainGridLabour.DataSource = DTabSalaryViewDetail;
                MainGridLabour.Refresh();
                GrdDetLabour.BestFitColumns();
                GrdDetLabour.EndUpdate();

                GrdDetYearMonthWise.BeginUpdate();
                MainGrdYearMonthDetail.DataSource = DTabYearMonthWiseDetail;
                MainGrdYearMonthDetail.Refresh();
                GrdDetYearMonthWise.EndUpdate();

                GrdDetYearMonthSummary.BeginUpdate();
                MainGrdYearMonthSummary.DataSource = DTabYearMonthWiseSummary;
                MainGrdYearMonthSummary.Refresh();
                GrdDetYearMonthSummary.EndUpdate();

                lblSalaryViewTotalPcs.Text = "  Pcs \n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["TOTALPCS"]).ToString();
                lblSalaryViewTotalCarat.Text = "  Carat \n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["TOTALCARAT"]).ToString();
                lblSalaryViewDollarPlus.Text = "  Plus(+)\n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["BEFOREPER_PLUSDOLLAR"]).ToString();
                lblSalaryViewDollarMinus.Text = "  Minus(-)\n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["BEFOREPER_MINUSDOLLAR"]).ToString();
                lblSalaryViewDFPlus.Text = "  DF(+)\n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["BEFOREPER_DFPLUSDOLLAR"]).ToString();
                lblSalaryViewDFMinus.Text = "  DF(-)\n" + Val.ToString(DTabSalaryViewSummary.Rows[0]["BEFOREPER_DFMINUSDOLLAR"]).ToString();

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
            //string MergeOnStr = "PLANVARIATION";
            //string MergeOn = "KAPANPACKET";

            //if (MergeOnStr.Contains(e.Column.FieldName))
            //{
            //    string val1 = Val.ToString(GrdDetLabour.GetRowCellValue(e.RowHandle1, GrdDetLabour.Columns[MergeOn]));
            //    string val2 = Val.ToString(GrdDetLabour.GetRowCellValue(e.RowHandle2, GrdDetLabour.Columns[MergeOn]));
            //    if (val1 == val2)
            //        e.Merge = true;
            //    e.Handled = true;
            //}
            try
            {
                string MergeOnStr = "DFTYPE,PLANVARIATIONDOLLAR,PLANVARIATIONRUPEES,PLANVARIATIONPER,BEFOREPER_DFPLUSDOLLAR,BEFOREPER_DFMINUSDOLLAR,DFPLUSPER,DFMINUSPER,DFPLUSRUPEES,DFMINUSRUPEES,BEFOREPER_PLUSDOLLAR,BEFOREPER_MINUSDOLLAR";
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
            catch (Exception ex)
            {

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

        private void GrdDetLabour_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
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

                    DouTotalDPlusDollar_BeforePer = 0;
                    DouTotalDMinusDollar_BeforePer = 0;
                    DouTotalDFPlusDollar_BeforePer = 0;
                    DouTotalDFMinusDollar_BeforePer = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    string P1 = Val.ToString(GrdDetLabour.GetRowCellValue(e.RowHandle, "KAPANPACKET"));
                    string P2 = Val.ToString(GrdDetLabour.GetRowCellValue(e.RowHandle - 1, "KAPANPACKET"));
                    if (P1 != P2)
                    {
                        DouTotalPlanVarDollar = DouTotalPlanVarDollar + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "PLANVARIATIONDOLLAR"));
                        DouTotalPlanVarRupees = DouTotalPlanVarRupees + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "PLANVARIATIONRUPEES"));

                        DouTotalDPlusDollar_BeforePer = DouTotalDPlusDollar_BeforePer + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "BEFOREPER_PLUSDOLLAR"));
                        DouTotalDMinusDollar_BeforePer = DouTotalDMinusDollar_BeforePer + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "BEFOREPER_MINUSDOLLAR"));
                        DouTotalDFPlusDollar_BeforePer = DouTotalDFPlusDollar_BeforePer + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "BEFOREPER_DFPLUSDOLLAR"));
                        DouTotalDFMinusDollar_BeforePer = DouTotalDFMinusDollar_BeforePer + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "BEFOREPER_DFMINUSDOLLAR"));

                        DouTotalDFPlusDollar = DouTotalDFPlusDollar + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "DFPLUSDOLLAR"));
                        DouTotalDFMinusDollar = DouTotalDFMinusDollar + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "DFMINUSDOLLAR"));
                        DouTotalDFPlusRupees = DouTotalDFPlusRupees + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "DFPLUSRUPEES"));
                        DouTotalDFMinusRupees = DouTotalDFMinusRupees + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "DFMINUSRUPEES"));
                        DouTotalPcs = DouTotalPcs + Val.Val(GrdDetLabour.GetRowCellValue(e.RowHandle, "PCS"));   //#P : 21-01-2020
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
                        e.TotalValue = DouTotalPlanVarRupees;
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
                        e.TotalValue = DouTotalDFPlusRupees;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFMINUSRUPEES") == 0)
                    {
                        e.TotalValue = DouTotalDFMinusRupees;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFTYPE") == 0)
                    {
                        e.TotalValue = DouTotalPcs;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_PLUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDPlusDollar_BeforePer;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_MINUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDMinusDollar_BeforePer;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_DFPLUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDFPlusDollar_BeforePer;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_DFMINUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDFMinusDollar_BeforePer;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GrdDetLabour_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }

        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "SalaryDetail";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGridLabour,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [PacketWiseStock.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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
            TextBrick BrickTitleseller = e.Graph.DrawString("Salary Detail For : '" + Val.ToString(txtEmpCode.Text) + "'", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("Date :- " + DateTime.Now.ToString("dd/MM/yyyy"), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 400, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        private void GrdDetYearMonthWise_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }
    }
}
