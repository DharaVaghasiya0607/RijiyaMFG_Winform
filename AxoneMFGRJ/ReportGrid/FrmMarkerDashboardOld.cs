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

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmMarkerDashboardOld : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_ProductionAnalysis ObjView = new BOTRN_ProductionAnalysis();

        DataTable DtabEmpATDDetail = new DataTable();
        DataTable DtabATDDetail = new DataTable();
        DataTable DtabCompDetail = new DataTable();

        DataTable DTabMakable = new DataTable();
        DataTable DTabGrading = new DataTable();

        DataTable DTabFinalPcs = new DataTable();
        DataTable DTabOnHandStock = new DataTable();

        DataTable DTabMarkerLabourSummary = new DataTable();
        DataTable DTabMarkerLabourDetail = new DataTable();

        #region Property Settings

        public FrmMarkerDashboardOld()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();

            txtEmpCode.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
            txtEmpCode.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
            txtEmpName.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME;
            txtYear.Focus();

            PnlLabourSummary.HorizontalScroll.Visible = true;
            //var path = new System.Drawing.Drawing2D.GraphicsPath();
            //path.AddEllipse(0, 0, lblTest.Width, lblTest.Height);
            //this.lblTest.Region = new Region(path);




            //byte[] OFFICELOGO = BusLib.Configuration.BOConfiguration.gEmployeeProperty.EMPPHOTO as byte[] ?? null;
            //if (OFFICELOGO != null)
            //{
            //    using (MemoryStream ms = new MemoryStream(OFFICELOGO))
            //    {
            //        PicEmpPhoto.Image = Image.FromStream(ms);
            //    }
            //}
            //else
            //{
            //    PicEmpPhoto.Image = null;
            //}
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

        }

        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                double DouTotalDays = 0;
                double DouPresentDays = 0;
                double DouAbsentDays = 0;

                double DouTotalHours = 0;
                double DouPresentHours = 0;
                double DouAbsentHours = 0;

                DataSet DS = new DataSet(); // ObjView.GetMarkerDashboardData(Val.ToString(txtEmpCode.Tag), Val.ToInt32(txtYear.Text), Val.ToInt32(txtMonth.Text));

                if (DS.Tables.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                DtabEmpATDDetail = DS.Tables[0];
                txtEmpCode.Tag = Val.ToString(DtabEmpATDDetail.Rows[0]["LEDGER_ID"]);
                txtEmpCode.Text = Val.ToString(DtabEmpATDDetail.Rows[0]["LEDGERCODE"]);
                txtEmpName.Text = Val.ToString(DtabEmpATDDetail.Rows[0]["LEDGERNAME"]);

                txtDepartment.Text = Val.ToString(DtabEmpATDDetail.Rows[0]["DEPARTMENTNAME"]);
                txtDesgination.Text = Val.ToString(DtabEmpATDDetail.Rows[0]["DESIGNATIONNAME"]);
                txtMobileNo.Text = Val.ToString(DtabEmpATDDetail.Rows[0]["MOBILENO"]);
                txtManager.Text = Val.ToString(DtabEmpATDDetail.Rows[0]["MANAGERCODE"]);

                byte[] OFFICELOGO = DtabEmpATDDetail.Rows[0]["EMPPHOTO"] as byte[] ?? null;
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

                DouTotalDays = Val.Val(DtabEmpATDDetail.Compute("Max(TOTALDAYS)", ""));
                DouPresentDays = Val.Val(DtabEmpATDDetail.Compute("Sum(WDAYS)", ""));
                DouAbsentDays = Val.Val(DouTotalDays - DouPresentDays);

                DouTotalHours = Val.Val(DtabEmpATDDetail.Compute("Sum(TOTALHOURS)", ""));
                DouPresentHours = Val.Val(DtabEmpATDDetail.Compute("Sum(WHOURS)", ""));
                DouAbsentHours = Val.Val(DouTotalHours - DouPresentHours);

                lblTotalDays.Text = "   Total   " + Val.ToString(DouTotalDays);
                lblPresentDays.Text = "  Present   " + Val.ToString(DouPresentDays);
                lblAbsentDays.Text = "  Absent   " + Val.ToString(DouAbsentDays);

                lblTotalHours.Text = "  Total   " + Val.ToString(DouTotalHours);
                lblPresentHours.Text = "  Present   " + Val.ToString(DouPresentHours);
                lblAbsentHours.Text = "  Absent   " + Val.ToString(DouAbsentHours);

                // Makable/Grading Data
                if (DS.Tables[1].Rows.Count > 0)
                {
                    MainGrdTotal.DataSource = DS.Tables[1];
                    MainGrdTotal.Refresh();

                    DTabMakable = DS.Tables[2];
                    DTabGrading = DS.Tables[3];

                    lblTotalMakable.Text = Val.ToString(Val.Val(DTabMakable.Compute("Sum(PCS)", ""))) + "/" + Val.ToString(Val.Val(DTabMakable.Compute("Sum(CARAT)", ""))) + " Cts";
                    lblTotalPolish.Text = Val.ToString(Val.Val(DTabGrading.Compute("Sum(PCS)", ""))) + "/" + Val.ToString(Val.Val(DTabGrading.Compute("Sum(CARAT)", ""))) + " Cts";

                    MainGridMakable.DataSource = DTabMakable;
                    GrdDetMakable.RefreshData();
                    GrdDetMakable.BestFitColumns();

                    RbtMakPcs_CheckedChanged(null, null);

                    MainGridGrading.DataSource = DS.Tables[3];
                    GrdDetGrading.RefreshData();
                    GrdDetGrading.BestFitColumns();

                    RbtGrdPcs_CheckedChanged(null, null);
                }

                // Final Pcs & On Hand Stock
                DTabFinalPcs = DS.Tables[4];
                DTabOnHandStock = DS.Tables[5];
                lblTotalFinalPcs.Text = Val.ToString(Val.Val(DTabFinalPcs.Compute("Sum(PCS)", ""))) + "/" + Val.ToString(Val.Val(DTabFinalPcs.Compute("Sum(CARAT)", ""))) + " Cts";
                lblOnHandStock.Text = Val.ToString(Val.Val(DTabOnHandStock.Compute("Sum(PCS)", ""))) + "/" + Val.ToString(Val.Val(DTabOnHandStock.Compute("Sum(CARAT)", ""))) + " Cts";

                //Marker Labour Detail
                DTabMarkerLabourDetail = DS.Tables[6];
                DTabMarkerLabourSummary = DS.Tables[7];

                MainGrdLabour.DataSource = DTabMarkerLabourDetail;
                MainGrdLabour.Refresh();



                //MainGridMakable.DataSource = DTabMabable;
                //MainGridMakable.Refresh();
                DispalyChart(ChartControlAttendance, ViewType.Line, DtabEmpATDDetail, "DD", "WHOURS");

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
                return;
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

        private void GrdDetMakable_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName.ToUpper().Contains("PCS"))
            {
                DispalyChart(ChartControlAttendance, ViewType.Line, DtabEmpATDDetail, "PARTICULAR", "PCS");
            }
            else if (e.Column.FieldName.ToUpper().Contains("CARAT"))
            {
                DispalyChart(ChartControlAttendance, ViewType.Line, DtabEmpATDDetail, "PARTICULAR", "CARAT");
            }
            else if (e.Column.FieldName.ToUpper().Contains("AMOUNT"))
            {
                DispalyChart(ChartControlAttendance, ViewType.Line, DtabEmpATDDetail, "PARTICULAR", "AMOUNT");
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
            using (var pen = new Pen(Color.FromArgb(86, 145, 161), 5.0f))
                e.Graphics.DrawPath(pen, path);

        }
        private void lblTotalDays_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblTotalDays.Width, lblTotalDays.Height);
            this.lblTotalDays.Region = new Region(path);
            using (var pen = new Pen(Color.FromArgb(184, 197, 190), 6.0f))
                e.Graphics.DrawPath(pen, path);
        }

        private void lblPresentHours_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblPresentHours.Width, lblPresentHours.Height);
            this.lblPresentHours.Region = new Region(path);
            using (var pen = new Pen(Color.FromArgb(86, 145, 161), 5.0f))
                e.Graphics.DrawPath(pen, path);
        }

        private void lblPresentDays_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblPresentDays.Width, lblPresentDays.Height);
            this.lblPresentDays.Region = new Region(path);
            using (var pen = new Pen(Color.FromArgb(86, 145, 161), 5.0f))
                e.Graphics.DrawPath(pen, path);
        }

        private void lblAbsentDays_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblAbsentDays.Width, lblAbsentDays.Height);
            this.lblAbsentDays.Region = new Region(path);
            using (var pen = new Pen(Color.FromArgb(86, 145, 161), 5.0f))
                e.Graphics.DrawPath(pen, path);
        }

        private void lblAbsentHours_Paint(object sender, PaintEventArgs e)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblAbsentHours.Width, lblAbsentHours.Height);
            this.lblAbsentHours.Region = new Region(path);
            using (var pen = new Pen(Color.FromArgb(86, 145, 161), 5.0f))
                e.Graphics.DrawPath(pen, path);
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

                DateTime firstDayOfMonth = new DateTime(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text), 1);
                DateTime LastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                string StrControlName = Val.ToString(DR["PARATYPE"]).ToUpper();

                StrClick = "TOTAL";
                //StrTitle = "Sym Comparision Of Exp [" + StrExp + "]  Vs Lab Grd [" + StrGrd + "]";

                string StrTitleType = Val.ToString(GrdDetTotal.FocusedColumn.FieldName).ToUpper() == "TOTALPCS" ? "All" : Val.ProperText(GrdDetTotal.FocusedColumn.FieldName);

                StrTitle = "Marker Grading Comparision Total Stone Detail of : " + Val.ToString(DR["PARATYPE"]) + " (" + StrTitleType + ")";

                StrExp = GrdDetTotal.FocusedColumn.FieldName;
                StrGrd = Val.ToString(DR["PARATYPE"]).ToUpper();

                string StrFromDate = null;
                string StrToDate = null;

                StrFromDate = Val.SqlDate(firstDayOfMonth.ToShortDateString());
                StrToDate = Val.SqlDate(LastDayOfMonth.ToShortDateString());

                this.Cursor = Cursors.WaitCursor;

                DataSet DS = new BOTRN_RunninPossition().GerGradingComparisionDetailWithLatestGrdByLabWithPivotBox("", StrFromDate, StrToDate, Val.ToInt64(txtEmpCode.Tag), StrClick, StrExp, StrGrd);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                FrmMarkerGradingComparisionPopupDetailWithPivotBox FrmMarkerGradingComparisionPopupDetailWithPivotBox = new FrmMarkerGradingComparisionPopupDetailWithPivotBox();
                FrmMarkerGradingComparisionPopupDetailWithPivotBox.DSPacketWiseStock = DS;
                FrmMarkerGradingComparisionPopupDetailWithPivotBox.MdiParent = Global.gMainRef;
                ObjFormEvent.ObjToDisposeList.Add(FrmMarkerGradingComparisionPopupDetailWithPivotBox);
                FrmMarkerGradingComparisionPopupDetailWithPivotBox.ShowForm(StrTitle, StrGrd, StrFromDate, StrToDate, Val.ToInt64(txtEmpCode.Tag));

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }


        private void RbtMakPcs_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtMakPcs.Checked == true)
            {
                DispalyChart(ChartControlMakable, ViewType.Line, DTabMakable, "DD", "PCS");
            }
            else if (RbtMakCarat.Checked == true)
            {
                DispalyChart(ChartControlMakable, ViewType.Line, DTabMakable, "DD", "CARAT");
            }
            else if (RbtMakAmount.Checked == true)
            {
                DispalyChart(ChartControlMakable, ViewType.Line, DTabMakable, "DD", "AMOUNT");
            }
        }

        private void RbtGrdPcs_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtMakPcs.Checked == true)
            {
                DispalyChart(ChartControlGrading, ViewType.Line, DTabGrading, "DD", "PCS");
            }
            else if (RbtMakCarat.Checked == true)
            {
                DispalyChart(ChartControlGrading, ViewType.Line, DTabGrading, "DD", "CARAT");
            }
            else if (RbtMakAmount.Checked == true)
            {
                DispalyChart(ChartControlGrading, ViewType.Line, DTabGrading, "DD", "AMOUNT");
            }
        }

        private void lblTotalPcs_Paint(object sender, PaintEventArgs e)
        {

        }
       private void lblDollarPlus_Paint(object sender, PaintEventArgs e)
        {
            //using (var pen = new Pen(Color.FromArgb(86, 145, 161), 5.0f))
            //    e.Graphics.DrawRectangle(pen,0,0,this.ClientSize.Width+1,this.ClientSize.Height+1);
            //ControlPaint.DrawBorde(e.Graphics, lblDollarPlus.DisplayRectangle, Color.FromArgb(32, 165, 223), ButtonBorderStyle.Solid);
        }
    }
}
