﻿using BusLib;
using BusLib.Configuration;
using BusLib.Dashboard;
using BusLib.Master;
using BusLib.TableName;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPivotGrid;
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

namespace AxoneMFGRJ.Dashboard
{
    public partial class FrmPlanningDashboard : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        bool resizing = false;
        TableLayoutRowStyleCollection rowStyles;
        TableLayoutColumnStyleCollection columnStyles;
        int colindex = -1;
        int rowindex = -1;
        int nextHeight;

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BODS_PlanningDashboard ObjMast = new BODS_PlanningDashboard();
        DataSet DS = new DataSet();
        string mStrFromDate = string.Empty;
        string mStrToDate = string.Empty;
        string mStrRoughType = string.Empty;
        string mStrXMLFilter = string.Empty;
        string mStrViewType = string.Empty;
        System.Diagnostics.Stopwatch watch = null;
        DataTable DTabFilter = new DataTable();
       

        #region Property Settings

        public FrmPlanningDashboard()
        {
            InitializeComponent();
        }

      
        public void ShowForm()
        {
           
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnLoad.Enabled = ObjPer.ISVIEW;

            this.Show();
            pivotColumnVisible();

        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        private void XtraTabDateControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (Val.ToString(XtraTabDateControl.SelectedTabPage.Tag).ToUpper())
            {
                case "TODAY":
                    DTPFromDate.Value = DateTime.Now;
                    DTPToDate.Value = DateTime.Now;
                    break;
                case "YESTERDAY":
                    DTPFromDate.Value = DateTime.Now.AddDays(-1);
                    DTPToDate.Value = DateTime.Now.AddDays(-1);
                     break;
                case "WEEK":
                     DTPFromDate.Value = DateTime.Now.AddDays(-(int)DateTime.Today.DayOfWeek);
                     DTPToDate.Value = DateTime.Now;
                    break;
                case "MONTH":
                    DTPFromDate.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
                    DTPToDate.Value = DateTime.Now;
                    break;
                case "QUATER":
                    int currentQuater = (DateTime.Now.Date.Month - 1) / 3 + 1;
                    int daysInLastMonthOfQuarter = DateTime.DaysInMonth(DateTime.Now.Year, 3 * currentQuater);


                    DTPFromDate.Value = new DateTime(DateTime.Now.Year, 3 * currentQuater - 2, 1);
                    DTPToDate.Value = new DateTime(DateTime.Now.Year, 3 * currentQuater, daysInLastMonthOfQuarter);
                    break;
                case "6MONTH":
                    DTPFromDate.Value = DateTime.Now.AddMonths(-6);
                    DTPToDate.Value = DateTime.Now;
                    break;
                case "YEAR":
                    DTPFromDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
                    DTPToDate.Value = DateTime.Now;
                    break;
                case "CUSTOME":
                    DTPFromDate.Value = DateTime.Now.AddMonths(-1);
                    DTPToDate.Value = DateTime.Now;
                   break;
            }
            BtnLoad_Click(null, null);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            

            mStrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
            mStrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
            if (rbtRoughStatusAll.Checked == true)
            {
                mStrRoughType = "All";
            }
            else if (rbtRoughStatusCVD.Checked == true)
            {
                mStrRoughType = "CVD";
            }
            else if (rbtRoughStatusHPHT.Checked == true)
            {
                mStrRoughType = "HPHT";
            }
            else if (rbtRoughStatusNatural.Checked == true)
            {
                mStrRoughType = "NATURAL";
            }

            if (RbtYearly.Checked == true)
            {
                mStrViewType = "Yearly";
            }
            else if (RbtMonthly.Checked == true)
            {
                mStrViewType = "Monthly";
            }
            else if (RbtWeekly.Checked == true)
            {
                mStrViewType = "Weekly";
            }
            else if (RbtQuaterly.Checked == true)
            {
                mStrViewType = "Quaterly";
            }
            else if (RbtDaily.Checked == true)
            {
                mStrViewType = "Daily";
            }

            BtnLoad.Enabled = false;
            PnlLoding.Visible = true;

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            watch = System.Diagnostics.Stopwatch.StartNew();
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DS = ObjMast.Fill(mStrFromDate, mStrToDate, mStrRoughType, mStrXMLFilter,mStrViewType);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BtnLoad.Enabled = true;
            PnlLoding.Visible = false;
            watch.Stop();
            lblTime.Text = string.Format("{0:hh\\:mm\\:ss}", watch.Elapsed);

            GrdDetFilter.BeginUpdate();
            PvtMakGrid.BeginUpdate();
            GrdDetMakable.BeginUpdate();
            GrdDet.BeginUpdate();

            MainGridFilter.DataSource = DS.Tables[0];
            MainGridFilter.Refresh();

            PvtMakGrid.DataSource = DS.Tables[1];
            PvtMakGrid.RefreshData();
         
            MainGridMakable.DataSource = DS.Tables[2];
            MainGridMakable.Refresh();


            DispalyChart(ChartControlAreaMakable, ViewType.Line, DS.Tables[2], "PARTICULAR", "PCS");

            MainGrid.DataSource = DS.Tables[3]; 
            MainGrid.Refresh();

            GrdDetMakable.BestFitColumns();
            GrdDetFilter.EndUpdate();
            PvtMakGrid.EndUpdate();
            GrdDet.EndUpdate();
            GrdDetMakable.EndUpdate();
            PvtMakGrid.Fields["PCS"].Width = 50;

            BtnLoad.Enabled = true;
            PnlLoding.Visible = false;
            pivotColumnVisible();
        }
        public void DispalyChart(DevExpress.XtraCharts.ChartControl ChartControl, DevExpress.XtraCharts.ViewType ViewType, DataTable dt, String X, String Y)
        {
            if (ViewType == ViewType.Area)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains(X))
                    {
                        Series series1 = new Series("", ViewType);
                        ChartControl.Series.Clear();

                        double TotalStone = Convert.ToDouble(dt.Copy().Compute("Max(" + Y + ")", ""));
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

            }
        }
        private void ChkSummPcs_CheckedChanged(object sender, EventArgs e)
        {
            pivotColumnVisible();
        }

        private void ChkSummPktCts_CheckedChanged(object sender, EventArgs e)
        {
            pivotColumnVisible();
        }

        private void ChkSummExpCts_CheckedChanged(object sender, EventArgs e)
        {
            pivotColumnVisible();
        }

        private void ChkSummExpPer_CheckedChanged(object sender, EventArgs e)
        {
            pivotColumnVisible();
        }

        private void ChkSummAmt_CheckedChanged(object sender, EventArgs e)
        {
            pivotColumnVisible();
        }

        private void pivotColumnVisible()
        {
            PvtMakGrid.Fields["PCS"].Visible = ChkSummPcs.Checked;
            PvtMakGrid.Fields["PKTCTS"].Visible = ChkSummPktCts.Checked;
            PvtMakGrid.Fields["EXPPER"].Visible = ChkSummExpPer.Checked;
            PvtMakGrid.Fields["EXPCTS"].Visible = ChkSummExpCts.Checked;
            PvtMakGrid.Fields["AMOUNT"].Visible = ChkSummAmt.Checked;

        }


        private void PvtMakGrid_CellClick(object sender, PivotCellEventArgs e)
        {
            try
            {
                PivotCellEventArgs CurrentCell = PvtMakGrid.Cells.GetFocusedCellInfo();
                if (CurrentCell.ColumnValueType == PivotGridValueType.GrandTotal || CurrentCell.RowValueType == PivotGridValueType.GrandTotal)
                {
                    return;
                }

                string shape = Val.ToString(CurrentCell.GetFieldValue(ColMakShape));
                string size = Val.ToString(CurrentCell.GetFieldValue(ColMakSizeName));

                DataSet DSDetail = ObjMast.FillDetail(mStrFromDate, mStrToDate, mStrRoughType, mStrXMLFilter, shape, size, "Mak");

                try
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = DSDetail.Tables[1];
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
                catch (Exception ex)
                {
                    Global.Message(ex.Message);
                }
            }
            catch (Exception ex)
            {
                
            }
            
        }

        private void rbtRoughStatusAll_CheckedChanged(object sender, EventArgs e)
        {
            BtnLoad_Click(null, null);
        }

        private void GrdDet_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                int IntIsColor = Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "ISCOLOR"));

                string StrCol1 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PARAMNAME"));

                string StrCol2 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle - 1, "PARAMNAME"));


                // Draw Horizontal line ---------- //
                GridViewInfo vi2 = GrdDet.GetViewInfo() as GridViewInfo;
                for (int j = 0; j < vi2.RowsInfo.Count; j++)
                {
                    GridRowInfo ri = vi2.RowsInfo[j];
                    if (IntIsColor != 1)
                        e.Graphics.DrawLine(Pens.Black, new Point(ri.Bounds.Left, ri.Bounds.Bottom), new Point(ri.Bounds.Right, ri.Bounds.Bottom));
                }

                //if (!Val.ToString(StrCol1).Trim().Equals(string.Empty))
                if (IntIsColor == 0 || (IntIsColor == 3))
                {
                    GridViewInfo vi = GrdDet.GetViewInfo() as GridViewInfo;
                    {
                        Point p1 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right - 1, vi.ViewRects.Rows.Y);
                        Point p2 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right - 1, vi.ViewRects.Rows.Bottom);

                        Point p3 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right, vi.ViewRects.Rows.Y);
                        Point p4 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right, vi.ViewRects.Rows.Bottom);

                        Pen p = new Pen(Color.Black);
                        //e.Graphics.DrawRectangle(Pens.Black, e.Bounds);
                        e.Graphics.DrawLine(p, p1, p2);

                        //if (IntIsColor == 3)
                        //{
                        //    string str = Val.ToString(e.CellValue);
                        //    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(238, 234, 221)), e.Bounds);
                        //}
                    }

                }

                else if (IntIsColor == 2)  //Draw Line For Makable/Polish/By/Lab Headers
                {
                    if (e.Column.FieldName.ToUpper().Contains("AMOUNTPER") || (IntIsColor == 2 && e.Column.FieldName == "PARAMNAME") || e.Column.FieldName == "MAKAMOUNT")
                    {
                        GridViewInfo vi = GrdDet.GetViewInfo() as GridViewInfo;
                        Point p1 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right - 1, vi.ViewRects.Rows.Y);
                        Point p2 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right - 1, vi.ViewRects.Rows.Bottom);

                        Point p3 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right, vi.ViewRects.Rows.Y);
                        Point p4 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right, vi.ViewRects.Rows.Bottom);

                        Pen p = new Pen(Color.Black);
                        e.Graphics.DrawLine(p, p1, p2);

                    }
                }
                else if (IntIsColor == 1)
                {
                    //GrdDet.OptionsView.ShowHorzLines= false;
                    GrdDet.OptionsView.ShowHorizontalLines = DefaultBoolean.False;
                }


                //int IntPolPcs = Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "POLPCS"));
                //double DouPolPcsPer = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLPCSPER"));
                //if (e.Column.FieldName == "POLPCSPER" && Val.Val(DouPolPcsPer) != 0)
                //    e.CellValue = e.DisplayText = Val.ToString(IntPolPcs) + "/" + Val.ToString(DouPolPcsPer) + "%";

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                int IntIsColor = Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "ISCOLOR"));

                string StrCol1 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PARAMNAME"));

                if (IntIsColor == 3)
                {
                    e.Appearance.Font = new Font("Verdana", 8.25f, FontStyle.Bold);
                    e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                    //if(e.Column.FieldName.Contains("PER"))
                    //e.Appearance.BackColor = Color.FromArgb(238, 234, 221);
                }
                else if (IntIsColor == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(238, 234, 221);
                    e.Appearance.Font = new Font("Verdana", 8.65f, FontStyle.Bold | FontStyle.Italic);
                    e.Appearance.ForeColor = Color.Black;
                }
                if (StrCol1.Trim().Equals("TOTAL"))
                {
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkBlue;
                }


                if (IntIsColor == 2)
                {
                    e.Appearance.Font = new Font("Verdana", 8.25f, FontStyle.Bold);
                    if (e.Column.FieldName.ToUpper().Contains("CARATPER"))
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    else e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                }


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
          /*  try
            {

                int IntIsColor = Val.ToInt32(GrdDet.GetRowCellValue(e., "ISCOLOR"));




                if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
                {
                    e.DisplayText = String.Empty;
                }
                if (Val.ToString(e.Column.FieldName).ToUpper().Contains("PER"))
                {
                    if (IntIsColor == 2)
                    {
                        if (Val.ToString(e.Column.FieldName).ToUpper().Equals("POLCARATPER"))
                            e.DisplayText = "Polish";
                        else if (Val.ToString(e.Column.FieldName).ToUpper().Equals("BYCARATPER"))
                            e.DisplayText = "By";
                        else if (Val.ToString(e.Column.FieldName).ToUpper().Equals("LABCARATPER"))
                            e.DisplayText = "Lab";
                    }



                    if (IntIsColor == 3)
                    {
                        e.DisplayText = "%";
                    }
                    else if ((IntIsColor == 2 || IntIsColor == 1 || IntIsColor == 4))
                    {
                        if (IntIsColor == 2 && (e.Column.FieldName.ToUpper() == "POLCARATPER" || e.Column.FieldName.ToUpper() == "BYCARATPER" || e.Column.FieldName.ToUpper() == "LABCARATPER"))
                        {
                            //e.DisplayText = "";
                        }
                        else
                            e.DisplayText = "";
                    }
                }

                //int IntPolPcs = Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "POLPCS"));
                //double DouPolPcsPer = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLPCSPER"));
                //if (e.Column.FieldName == "POLPCSPER" && Val.Val(DouPolPcsPer) != 0)
                //    e.DisplayText = Val.ToString(IntPolPcs) + "/" + Val.ToString(DouPolPcsPer) + "%";

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }*/
        }
    }
}
