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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmProductionAnalysis : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_ProductionAnalysis ObjView = new BOTRN_ProductionAnalysis();

        DataTable DTabMabable = new DataTable();
        DataTable DTabPolish = new DataTable();


        #region Property Settings

        public FrmProductionAnalysis()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {   
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            DTPToDate.Value = System.DateTime.Now;
            RbtWeekly.Checked = true;
            RbtYearly_CheckedChanged(null, null);
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
              
                string StrFromDate = null;
                string StrToDate = null;
                string StrOpe = "";
                if (RbtYearly.Checked == true)
                {
                    StrOpe = "YEARLY";
                }
                else if (RbtMonthly.Checked == true)
                {
                    StrOpe = "MONTHLY";                    
                }
                else if (RbtWeekly.Checked == true)
                {
                    StrOpe = "WEEKLY";
                }
                else if (RbtQuater.Checked == true)
                {
                    StrOpe = "QUATERLY";
                }
                else if (RbtDaily.Checked == true)
                {
                    StrOpe = "DAILY";                    
                }


                StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());

               this.Cursor = Cursors.WaitCursor;
               DataSet DS = ObjView.GetProductionAnalysisData(StrOpe, StrFromDate, StrToDate);

               DTabMabable = new DataTable();
               DTabMabable = DS.Tables[0];
               DTabPolish = DS.Tables[1];
               
               MainGridMakable.DataSource = DTabMabable;
               MainGridMakable.Refresh();
               DispalyChart(ChartControlAreaMakable, ViewType.Line, DTabMabable, "PARTICULAR", "PCS");

                
               MainGridPolish.DataSource = DTabPolish;
               MainGridPolish.Refresh();
               DispalyChart(ChartControlAreaPolish, ViewType.Line, DTabPolish, "PARTICULAR", "PCS");
               
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

        private void RbtYearly_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtYearly.Checked == true)
            {
                lblMakable.Text = "Makable Production (YEARLY)";
                lblPolish.Text = "Polish Production (YEARLY)";
            }
            else if (RbtMonthly.Checked == true)
            {
                lblMakable.Text = "Makable Production (MONTHLY)";
                lblPolish.Text = "Polish Production (MONTHLY)";

            }
            else if (RbtWeekly.Checked == true)
            {
                lblMakable.Text = "Makable Production (WEEKLY)";
                lblPolish.Text = "Polish Production (WEEKLY)";
            }
            else if (RbtQuater.Checked == true)
            {
                lblMakable.Text = "Makable Production (QUATERLY)";
                lblPolish.Text = "Polish Production (QUATERLY)"; 
            }
            else if (RbtDaily.Checked == true)
            {
                lblMakable.Text = "Makable Production (DAILY)";
                lblPolish.Text = "Polish Production (DAILY)";
            }
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
        public void DispalyBARChart(DevExpress.XtraCharts.ChartControl ChartControl, DevExpress.XtraCharts.ViewType ViewType, DataSet dset)
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
                DispalyChart(ChartControlAreaMakable, ViewType.Line, DTabMabable, "PARTICULAR", "PCS");
            }
            else if (e.Column.FieldName.ToUpper().Contains("CARAT"))
            {
                DispalyChart(ChartControlAreaMakable, ViewType.Line, DTabMabable, "PARTICULAR", "CARAT");
            }
            else if (e.Column.FieldName.ToUpper().Contains("AMOUNT"))
            {
                DispalyChart(ChartControlAreaMakable, ViewType.Line, DTabMabable, "PARTICULAR", "AMOUNT");
            }
        }

        private void GrdDetPolish_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName.ToUpper().Contains("PCS"))
            {
                DispalyChart(ChartControlAreaPolish, ViewType.Line, DTabPolish, "PARTICULAR", "PCS");
            }
            else if (e.Column.FieldName.ToUpper().Contains("CARAT"))
            {
                DispalyChart(ChartControlAreaPolish, ViewType.Line, DTabPolish, "PARTICULAR", "CARAT");
            }
            else if (e.Column.FieldName.ToUpper().Contains("AMOUNT"))
            {
                DispalyChart(ChartControlAreaPolish, ViewType.Line, DTabPolish, "PARTICULAR", "AMOUNT");
            }
        }




    }
}
