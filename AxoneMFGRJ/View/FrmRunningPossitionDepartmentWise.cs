using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.XtraCharts;
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

namespace AxoneMFGRJ.View
{
    public partial class FrmRunningPossitionDepartmentWise : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();
        
        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabDeptStock = new DataTable();
        DataTable DTabManStock = new DataTable();
        DataTable DTabEmpStock = new DataTable();
        DataTable DTabStockDetail = new DataTable();
        DataTable DTabLossDetail = new DataTable();

        string mStrKapan = string.Empty;
        int mIntPacketNo = 0;
        string mStrTag = string.Empty;

        string mStrStockCategory = string.Empty;
        string mStrStockType = string.Empty;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        FORMTYPE mFormType = FORMTYPE.FACTORY;

        public enum FORMTYPE
        {
            ALL = 0,
            FACTORY = 1,
            BOMBAY = 2
        }
        
        string mStrReportTitle = "";

        public FrmRunningPossitionDepartmentWise()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType)
        {
            mFormType = pFormType;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            this.MouseWheel += new MouseEventHandler(Panel1_MouseWheel);

            // Action Button Rights
            EmployeeActionRightsProperty Property = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

            RbtFullStock.Checked = false;
            RbtDeptStock.Checked = false;
            RbtMYStock.Checked = false;
            RbtOtherStock.Checked = false;

            RbtFullStock.Enabled = Property.ISFULLSTOCK;

            RbtMYStock.Enabled = Property.ISMYSTOCK;
            RbtMYStock.Checked = Property.ISMYSTOCK;

            RbtDeptStock.Enabled = Property.ISDEPTSTOCK;
            RbtDeptStock.Checked = Property.ISDEPTSTOCK;

            RbtOtherStock.Enabled = Property.ISOTHERSTOCK;
            
            CmbKapan.Properties.DataSource = ObjPacket.FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            if (mFormType == FORMTYPE.BOMBAY)
            {
                this.Text = "GRADING/BOMBAY RUNNING STOCK (" + Val.ToString(BOConfiguration.gEmployeeProperty.DEPARTMENTNAME) + ")";
                GrdDetDept.Bands["BandDeptRough"].Visible = false;
                GrdDetDept.Bands["BandDeptMak"].Visible = false;                
                GrdDetDept.Bands["BandDeptToday"].Visible = false;

                GrdDetEmpSummary.Bands["BandEmpRough"].Visible = false;
                GrdDetEmpSummary.Bands["BandEmpMak"].Visible = false;                
                GrdDetEmpSummary.Bands["BandEmpToday"].Visible = false;
            }
            else if (mFormType == FORMTYPE.FACTORY)
            {
                this.Text = "FACTORY RUNNING STOCK (" + Val.ToString(BOConfiguration.gEmployeeProperty.DEPARTMENTNAME) + ")";                
            }
            else if (mFormType == FORMTYPE.ALL)
            {
                this.Text = "FACTORY + BOMBAY COMBINE RUNNING STOCK (" + Val.ToString(BOConfiguration.gEmployeeProperty.DEPARTMENTNAME) + ")";
            }
        }

        private void Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            PanelMain.Focus();
        }

        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(DTabDeptStock);
            ObjFormEvent.ObjToDisposeList.Add(DTabEmpStock);
            ObjFormEvent.ObjToDisposeList.Add(DTabStockDetail);
            ObjFormEvent.ObjToDisposeList.Add(DTabLossDetail);
            ObjFormEvent.ObjToDisposeList.Add(ObjPacket);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (RbtAll.Checked == true)
                {
                    mStrStockCategory = RbtAll.Tag.ToString();
                }
                else if (RbtRough.Checked == true)
                {
                    mStrStockCategory = RbtRough.Tag.ToString();
                }
                else if (RbtMak.Checked == true)
                {
                    mStrStockCategory = RbtMak.Tag.ToString();
                }
                else if (RbtGrd.Checked == true)
                {
                    mStrStockCategory = RbtGrd.Tag.ToString();
                }

                if (RbtFullStock.Checked == true)
                {
                    mStrStockType = RbtFullStock.Tag.ToString();
                }
                else if (RbtDeptStock.Checked == true)
                {
                    mStrStockType = RbtDeptStock.Tag.ToString();
                }
                else if (RbtMYStock.Checked == true)
                {
                    mStrStockType = RbtMYStock.Tag.ToString();
                }
                else if (RbtOtherStock.Checked == true)
                {
                    mStrStockType = RbtOtherStock.Tag.ToString();
                }

                mStrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());

                SetControlPropertyValue(lblMessage, "Text", "");

                this.Cursor = Cursors.WaitCursor;
                DataSet DS = Obj.RunningPosstionDataDepartmentWise(mFormType.ToString(), mStrStockCategory, mStrStockType, mStrKapan, mIntPacketNo, mStrTag,Val.ToBoolean(ChkWithExtraStock.Checked));

                DTabDeptStock = DS.Tables[0];
                DTabManStock = DS.Tables[1];
                DTabEmpStock = DS.Tables[2];
                DTabStockDetail = DS.Tables[3];
                DTabLossDetail = DS.Tables[4];

                GrdDetDept.BeginUpdate();
                GrdDetManager.BeginUpdate();
                GrdDetEmpSummary.BeginUpdate();

                MainGridDeptStock.DataSource = DTabDeptStock;
                GrdDetDept.RefreshData();
               // GrdDetDept.BestFitColumns();
                
                MainGridManager.DataSource = DTabManStock;
                GrdDetManager.RefreshData();
                //GrdDetManager.BestFitColumns();

                MainGridEmpSummary.DataSource = DTabEmpStock;
                GrdDetEmpSummary.RefreshData();
               // GrdDetEmpSummary.BestFitColumns();

                GrdDetDept.EndUpdate();
                GrdDetManager.EndUpdate();
                GrdDetEmpSummary.EndUpdate();

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

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("[ " + mStrStockCategory + " & " + mStrStockType + "  ] STOCK Of AsOnDate :- " + DateTime.Now.ToString("dd/MM/yyyy"), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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
            mStrReportTitle = "Running Possition (Department Wise)";
            CommonPrintFuction(MainGridDeptStock, GrdDetDept);

        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Running Possition (Department Wise)";
            CommonExcelExportFuction(MainGridDeptStock, GrdDetDept, "DeptWiseStockSummary");
        }

        private void lblEmployeePrint_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Running Possition (Employee Wise)";
            CommonPrintFuction(MainGridEmpSummary, GrdDetEmpSummary);

        }

        private void lblEmployeeExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Running Possition (Employee Wise)";
            CommonExcelExportFuction(MainGridEmpSummary, GrdDetEmpSummary, "EmpWiseStockSummary");  
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
                link.Landscape = true;

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
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open ["+pStrFileName+".xlsx] ?") == System.Windows.Forms.DialogResult.Yes )
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


        private void timer1_Tick(object sender, EventArgs e)
        {
            BtnRefresh_Click(null, null);
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

        /*

        #region Chart Preparation


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
                Pie3DSeries.Label.PointOptions.ValueNumericOptions.Precision = rbtPcsBase.Checked == true ? 0 : 3;
                if (ViewType == DevExpress.XtraCharts.ViewType.Pie3D)
                {
                    ((SimpleDiagram3D)ChartControl.Diagram).RuntimeRotation = true;
                    ((SimpleDiagram3D)ChartControl.Diagram).RuntimeZooming = true;    
                }
                
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
        

        #endregion 
        */

        private void GrdDetDept_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {

                if (e.RowHandle < 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                DataRow DRow = GrdDetDept.GetDataRow(e.RowHandle);

                if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "ROUGHPCS" || e.Column.FieldName.ToUpper() == "ROUGHCARAT"))
                {
                    DataRow[] UDRow = DTabStockDetail.Select("TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And CONFDATE IS NOT NULL And StockType ='ROUGH'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ]  Confired Rough Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "MAKPCS" || e.Column.FieldName.ToUpper() == "MAKCARAT"))
                {
                    DataRow[] UDRow = DTabStockDetail.Select("TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And CONFDATE IS NOT NULL And StockType ='MAK'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ]  Confired Makable Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "POLPCS" || e.Column.FieldName.ToUpper() == "POLCARAT"))
                {
                    DataRow[] UDRow = DTabStockDetail.Select("TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And CONFDATE IS NOT NULL And StockType ='POL'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ]  Confired Polish Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "PENDINGPCS" || e.Column.FieldName.ToUpper() == "PENDINGCARAT"))
                {

                    DataRow[] UDRow = DTabStockDetail.Select("TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And CONFDATE IS NULL");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ]  In Transit Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "TOTALPCS" || e.Column.FieldName.ToUpper() == "TOTALCARAT" || e.Column.FieldName.ToUpper() == "AGE"))
                {

                    DataRow[] UDRow = DTabStockDetail.Select("TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ]  Total Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "SECONDPCS" || e.Column.FieldName.ToUpper() == "SECONDCARAT"))
                {

                    DataRow[] UDRow = DTabLossDetail.Select("TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And ISNULL(SECONDCARAT,0) <> 0");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ]  In Second Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "EXTRAPCS" || e.Column.FieldName.ToUpper() == "EXTRACARAT"))
                {

                    DataRow[] UDRow = DTabLossDetail.Select("TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And ISNULL(EXTRACARAT,0) <> 0");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ]  In Extra Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "LOSSCARAT"))
                {

                    DataRow[] UDRow = DTabLossDetail.Select("TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And ISNULL(LOSSCARAT,0) <> 0");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ]  In Loss Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void GrdDetEmpSummary_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                DataRow DRow = GrdDetEmpSummary.GetDataRow(e.RowHandle);
                if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "ROUGHPCS" || e.Column.FieldName.ToUpper() == "ROUGHCARAT"))
                {
                    DataRow[] UDRow = DTabStockDetail.Select("TOEMPLOYEENAME= '" + Val.ToString(DRow["EMPLOYEENAME"]) + "' And CONFDATE IS NOT NULL And StockType='ROUGH'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Employee [ " + Val.ToString(DRow["EMPLOYEENAME"]) + " ]  Confired Rough Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "MAKPCS" || e.Column.FieldName.ToUpper() == "MAKCARAT"))
                {
                    DataRow[] UDRow = DTabStockDetail.Select("TOEMPLOYEENAME= '" + Val.ToString(DRow["EMPLOYEENAME"]) + "' And CONFDATE IS NOT NULL And StockType='MAK'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Employee [ " + Val.ToString(DRow["EMPLOYEENAME"]) + " ]  Confired Makable Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "POLPCS" || e.Column.FieldName.ToUpper() == "POLCARAT"))
                {
                    DataRow[] UDRow = DTabStockDetail.Select("TOEMPLOYEENAME= '" + Val.ToString(DRow["EMPLOYEENAME"]) + "' And CONFDATE IS NOT NULL And StockType='POL'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Employee [ " + Val.ToString(DRow["EMPLOYEENAME"]) + " ]  Confired Polish Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "PENDINGPCS" || e.Column.FieldName.ToUpper() == "PENDINGCARAT"))
                {
                    DataRow[] UDRow = DTabStockDetail.Select("TOEMPLOYEENAME = '" + Val.ToString(DRow["EMPLOYEENAME"]) + "' And CONFDATE IS NULL");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Employee [ " + Val.ToString(DRow["EMPLOYEENAME"]) + " ]  In Transit Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "TOTALPCS" || e.Column.FieldName.ToUpper() == "TOTALCARAT"|| e.Column.FieldName.ToUpper() == "AGE"))
                {

                    DataRow[] UDRow = DTabStockDetail.Select("TOEMPLOYEENAME = '" + Val.ToString(DRow["EMPLOYEENAME"]) + "'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Employee [ " + Val.ToString(DRow["EMPLOYEENAME"]) + " ]  Total Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "SECONDPCS" || e.Column.FieldName.ToUpper() == "SECONDCARAT"))
                {

                    DataRow[] UDRow = DTabLossDetail.Select("TOEMPLOYEENAME = '" + Val.ToString(DRow["EMPLOYEENAME"]) + "' And ISNULL(SECONDCARAT,0) <> 0");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Employee [ " + Val.ToString(DRow["EMPLOYEENAME"]) + " ]  In Second Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "EXTRAPCS" || e.Column.FieldName.ToUpper() == "EXTRACARAT"))
                {

                    DataRow[] UDRow = DTabLossDetail.Select("TOEMPLOYEENAME = '" + Val.ToString(DRow["EMPLOYEENAME"]) + "' And ISNULL(EXTRACARAT,0) <> 0");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Employee [ " + Val.ToString(DRow["EMPLOYEENAME"]) + " ]  In Extra Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "LOSSCARAT"))
                {
                    DataRow[] UDRow = DTabLossDetail.Select("TOEMPLOYEENAME = '" + Val.ToString(DRow["EMPLOYEENAME"]) + "' And ISNULL(LOSSCARAT,0) <> 0");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Employee [ " + Val.ToString(DRow["EMPLOYEENAME"]) + " ]  In Loss Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void BtnWIPPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (RbtAll.Checked == true)
                {
                    mStrStockCategory = RbtAll.Tag.ToString();
                }
                else if (RbtRough.Checked == true)
                {
                    mStrStockCategory = RbtRough.Tag.ToString();
                }
                else if (RbtMak.Checked == true)
                {
                    mStrStockCategory = RbtMak.Tag.ToString();
                }
                else if (RbtGrd.Checked == true)
                {
                    mStrStockCategory = RbtGrd.Tag.ToString();
                }

                if (RbtFullStock.Checked == true)
                {
                    mStrStockType = RbtFullStock.Tag.ToString();
                }
                else if (RbtDeptStock.Checked == true)
                {
                    mStrStockType = RbtDeptStock.Tag.ToString();
                }
                else if (RbtMYStock.Checked == true)
                {
                    mStrStockType = RbtMYStock.Tag.ToString();
                }
                else if (RbtOtherStock.Checked == true)
                {
                    mStrStockType = RbtOtherStock.Tag.ToString();
                }

                mStrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());

                SetControlPropertyValue(lblMessage, "Text", "");

                this.Cursor = Cursors.WaitCursor;
                DataTable DTab = Obj.RunningPosstionDataWIPPivotReport(mStrStockCategory, mStrStockType, mStrKapan, mIntPacketNo, mStrTag);


                Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowForm("RunningPossitionProcessPivotWIP", DTab);

                this.Cursor = Cursors.Default;

            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }
        }

        private void GrdDetManager_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {

                if (e.RowHandle < 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                DataRow DRow = GrdDetManager.GetDataRow(e.RowHandle);

                if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "ROUGHPCS" || e.Column.FieldName.ToUpper() == "ROUGHCARAT"))
                {
                    DataRow[] UDRow = DTabStockDetail.Select("TOMANAGERNAME = '" + Val.ToString(DRow["MANAGERNAME"]) + "' And TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And CONFDATE IS NOT NULL And StockType ='ROUGH'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Manager [ " + Val.ToString(DRow["MANAGERNAME"]) + " ] Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ] Confired Rough Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "MAKPCS" || e.Column.FieldName.ToUpper() == "MAKCARAT"))
                {
                    DataRow[] UDRow = DTabStockDetail.Select("TOMANAGERNAME = '" + Val.ToString(DRow["MANAGERNAME"]) + "' And TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And CONFDATE IS NOT NULL And StockType ='MAK'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Manager [ " + Val.ToString(DRow["MANAGERNAME"]) + " ] Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ] Confired Makable Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "POLPCS" || e.Column.FieldName.ToUpper() == "POLCARAT"))
                {
                    DataRow[] UDRow = DTabStockDetail.Select("TOMANAGERNAME = '" + Val.ToString(DRow["MANAGERNAME"]) + "' And TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And CONFDATE IS NOT NULL And StockType ='POL'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Manager [ " + Val.ToString(DRow["MANAGERNAME"]) + " ] Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ] Confired Polish Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "PENDINGPCS" || e.Column.FieldName.ToUpper() == "PENDINGCARAT"))
                {

                    DataRow[] UDRow = DTabStockDetail.Select("TOMANAGERNAME = '" + Val.ToString(DRow["MANAGERNAME"]) + "' And TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And CONFDATE IS NULL");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Manager [ " + Val.ToString(DRow["MANAGERNAME"]) + " ] Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ] In Transit Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }
                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "TOTALPCS" || e.Column.FieldName.ToUpper() == "TOTALCARAT" || e.Column.FieldName.ToUpper() == "AGE"))
                {

                    DataRow[] UDRow = DTabStockDetail.Select("TOMANAGERNAME = '" + Val.ToString(DRow["MANAGERNAME"]) + "' And TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "'");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Manager [ " + Val.ToString(DRow["MANAGERNAME"]) + " ] Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ] Total Stock Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "SECONDPCS" || e.Column.FieldName.ToUpper() == "SECONDCARAT"))
                {

                    DataRow[] UDRow = DTabLossDetail.Select("TOMANAGERNAME = '" + Val.ToString(DRow["MANAGERNAME"]) + "' And TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And ISNULL(SECONDCARAT,0) <> 0");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Manager [ " + Val.ToString(DRow["MANAGERNAME"]) + " ] Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ] In Second Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "EXTRAPCS" || e.Column.FieldName.ToUpper() == "EXTRACARAT"))
                {

                    DataRow[] UDRow = DTabLossDetail.Select("TOMANAGERNAME = '" + Val.ToString(DRow["MANAGERNAME"]) + "' And TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And ISNULL(EXTRACARAT,0) <> 0");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Manager [ " + Val.ToString(DRow["MANAGERNAME"]) + " ] Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ] In Extra Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                else if (e.Clicks == 2 && (e.Column.FieldName.ToUpper() == "LOSSCARAT"))
                {

                    DataRow[] UDRow = DTabLossDetail.Select("TOMANAGERNAME = '" + Val.ToString(DRow["MANAGERNAME"]) + "' And TODEPARTMENTNAME = '" + Val.ToString(DRow["DEPARTMENTNAME"]) + "' And ISNULL(LOSSCARAT,0) <> 0");
                    if (UDRow != null)
                    {
                        string StrReportTitle = "Manager [ " + Val.ToString(DRow["MANAGERNAME"]) + " ] Department [ " + Val.ToString(DRow["DEPARTMENTNAME"]) + " ] In Loss Packet Wise";
                        FrmRunningPossitionPopupDetail FrmRunningPossitionPopupDetail = new FrmRunningPossitionPopupDetail();
                        FrmRunningPossitionPopupDetail.MdiParent = Global.gMainRef;
                        FrmRunningPossitionPopupDetail.mStrOpe = mStrStockCategory;
                        FrmRunningPossitionPopupDetail.DTabPacketWiseStock = UDRow.CopyToDataTable();
                        FrmRunningPossitionPopupDetail.ShowForm(StrReportTitle);
                    }
                }

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }


    }
}
