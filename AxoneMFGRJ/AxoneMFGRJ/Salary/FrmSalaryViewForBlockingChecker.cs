using AxoneMFGRJ.Report;
using AxoneMFGRJ.ReportGrid;
using AxoneMFGRJ.View;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Salary
{
    public partial class FrmSalaryViewForBlockingChecker : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabProcess = new DataTable();
        DataTable DTabDetail = new DataTable();
        DataTable DTabSummary = new DataTable();
        DataTable DTabSummaryManagerWise = new DataTable();

        DataTable DTabSummaryYearMonth = new DataTable();

        DataTable DTabPlanVariationDetail = new DataTable();
        DataTable DTabDFPlusMinus = new DataTable();
        DataSet DSDetail = new DataSet();

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

        string mStrKapanName = "";
        Int32 mIntFromPacketNo = 0;
        Int32 mIntToPacketNo = 0;
        Int64 mIntEmployee_ID = 0;
        string mStrFromDate = "";
        string mStrToDate = "";

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        string mStrReportTitle = "";

        public FrmSalaryViewForBlockingChecker()
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


            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdSummary.Name);

            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdSummary.RestoreLayoutFromStream(stream);

            }


            Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDetail.Name);

            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDetail.RestoreLayoutFromStream(stream);
            }

            Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDetYearMonth.Name);
            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDetYearMonth.RestoreLayoutFromStream(stream);
            }

            CmbKapan.Properties.DataSource = ObjPacket.FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            CmbSalaryType.SelectedIndex = 0;
            txtPass_TextChanged(null, null);
        }

        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjPacket);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtEmployee.Text.Trim().Length == 0)
                {
                    txtEmployee.Tag = "";
                }

                string StrSalaryType = "";

                StrSalaryType = Val.ToString(CmbSalaryType.Text);

                PicEmpPhoto.Image = null;
                txtEmpCode.Text = string.Empty;

                //if (RbtAll.Checked == true) StrOpe = "ALL";

                mStrKapanName = Val.Trim(CmbKapan.Properties.GetCheckedItems());
                mIntFromPacketNo = Val.ToInt32(txtFromPacketNo.Text);
                mIntToPacketNo = Val.ToInt32(txtToPacketNo.Text);
                mIntEmployee_ID = Val.ToInt64(txtEmployee.Tag);
                mStrFromDate = DTPFromDate.Value.ToShortDateString();
                mStrToDate = DTPToDate.Value.ToShortDateString();

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                DTabDetail.Rows.Clear();
                DTabSummary.Rows.Clear();
                DTabSummaryYearMonth.Rows.Clear();
                BtnRefresh.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

                //string StrShapeType = "";
                //if (RbtAllShape.Checked)
                //    StrShapeType = "ALL";
                //else if (RbtRoundShape.Checked)
                //    StrShapeType = "ROUND";
                //else if (RbtFancyShape.Checked)
                //    StrShapeType = "FANCY";


                //DataSet DS = Obj.GetSalaryViewSummaryDetailForWorker(Val.Trim(CmbKapan.Properties.GetCheckedItems()), Val.ToInt32(txtFromPacketNo.Text), Val.ToInt32(txtToPacketNo.Text), 531, Val.ToInt64(txtEmployee.Tag), Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), StrShapeType, Val.ToBoolean(ChkISWithRejectedEmp.Checked));

                //DTabSummaryYearMonth = DS.Tables[0];
                //DTabSummary = DS.Tables[1];
                //DTabSummaryManagerWise = DS.Tables[2];
                //DTabDetail = DS.Tables[3];
                ////DTabPlanVariationDetail = DS.Tables[3];
                ////DTabDFPlusMinus = DS.Tables[4];


                //GrdDetail.BeginUpdate();
                //GrdSummary.BeginUpdate();
                //GrdDetYearMonth.BeginUpdate();

                //MainGridSummary.DataSource = DTabSummary;
                //GrdSummary.RefreshData();

                //MainGrdYearMonth.DataSource = DTabSummaryYearMonth;
                //GrdDetYearMonth.RefreshData();

                ////GrdDetSummary.BestFitColumns();

                //MainGridDetail.DataSource = DTabDetail;
                //GrdDetail.RefreshData();
                //GrdDetail.BestFitColumns();
                //GrdDetail.Columns["WORKERCODE"].ClearFilter();


                //GrdDetail.EndUpdate();
                //GrdSummary.EndUpdate();
                //GrdDetYearMonth.EndUpdate();

                //this.Cursor = Cursors.Default;

            }
            catch (Exception EX)
            {
                PanelProgress.Visible = false;
                BtnRefresh.Enabled = true;
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }

        }

        public DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();


            // column names
            PropertyInfo[] oProps = null;


            if (varlist == null) return dtReturn;


            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;


                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }


                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }


                DataRow dr = dtReturn.NewRow();


                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }


                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
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

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtEmployee.Enabled == false)
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
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void FrmFactoryProduction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BtnRefresh_Click(null, null);
            }
        }

        private void lblPacketExport_Click(object sender, EventArgs e)
        {

        }

        private void lblPacketPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Factory Production Labour Detail (" + Str + ")";
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGridDetail;
                link.Landscape = true;

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

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {

            if (e.SelectedControl == MainGridSummary)
            {
                ToolTipControlInfo info = null;
                try
                {
                    GridView view = MainGridSummary.GetViewAt(e.ControlMousePosition) as GridView;
                    if (view == null) return;
                    GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "WORKERCODE")
                    {
                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "WORKERNAME")));
                        return;
                    }
                }
                finally
                {
                    e.Info = info;
                }
            }
            if (e.SelectedControl == MainGrdYearMonth)
            {
                ToolTipControlInfo info = null;
                try
                {
                    GridView view = MainGrdYearMonth.GetViewAt(e.ControlMousePosition) as GridView;
                    if (view == null) return;
                    GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "WORKERCODE")
                    {
                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "WORKERNAME")));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "DOLLARPLUSRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "PLUSDOLLAR"));
                        string StrPlusDollarPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DOLLARPLUSPER"));
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DOLLARPLUSRUPEES"));

                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "DOLLARPLUSRUPEES", ""));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "DOLLARMINUSRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "MINUSDOLLAR"));
                        //string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "ACTUALMINUSDOLLAR"));
                        string StrPlusDollarPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DOLLARMINUSPER"));
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DOLLARMINUSRUPEES"));
                        string StrFinalProcessAmt = Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOTALGRDAMOUNT"));

                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "DOLLARMINUSRUPEES", ""));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "DFPLUSRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFPLUSDOLLAR"));
                        string StrPlusDollarPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFPLUSPER"));
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFPLUSRUPEES"));

                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "DFPLUSRUPEES", ""));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "DFMINUSRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFMINUSDOLLAR"));
                        string StrPlusDollarPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFMINUSPER"));
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFMINUSRUPEES"));

                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "DFMINUSRUPEES", ""));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "PLANVARIATIONRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "PLANVARIATIONDOLLAR"));
                        string StrPlusDollarPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "PLANVARIATIONPER"));
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "PLANVARIATIONRUPEES"));

                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "PLANVARIATION", ""));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "BREAKINGRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "BREAKINGDOLLAR"));
                        string StrPlusDollarPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "BREAKINGPER"));
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "BREAKINGRUPEES"));

                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "BREAKING", ""));
                        return;
                    }
                }

                finally
                {
                    e.Info = info;
                }
            }
            if (e.SelectedControl == MainGridDetail)
            {
                ToolTipControlInfo info = null;
                try
                {
                    GridView view = MainGridDetail.GetViewAt(e.ControlMousePosition) as GridView;
                    if (view == null) return;
                    GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "WORKERCODE")
                    {
                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "WORKERNAME")));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MINUSDOLLAR")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "ACTUALMINUSDOLLAR"));

                        string StrProcessAmount = Val.ToString(view.GetRowCellValue(hi.RowHandle, "GRDAMOUNT"));

                        string StrPlusDollarPer = "1";
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DOLLARMINUSRUPEES"));
                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "MINUSDOLLAR", StrProcessAmount));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "DOLLARPLUSRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "PLUSDOLLAR"));
                        string StrPlusDollarPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DOLLARPLUSPER"));
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DOLLARPLUSRUPEES"));
                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "DOLLARPLUSRUPEES", ""));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "DOLLARMINUSRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        //string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "MINUSDOLLAR"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "ACTUALMINUSDOLLAR"));
                        string StrPlusDollarPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DOLLARMINUSPER"));
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DOLLARMINUSRUPEES"));
                        string StrFinalProcessAmt = Val.ToString(view.GetRowCellValue(hi.RowHandle, "GRDAMOUNT"));
                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "DOLLARMINUSRUPEES", StrFinalProcessAmt));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "DFPLUSRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFPLUSDOLLAR"));
                        string StrPlusDollarPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFPLUSPER"));
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFPLUSRUPEES"));

                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "DFPLUSRUPEES", ""));
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "DFMINUSRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrPlusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFMINUSDOLLAR"));
                        string StrPlusDollarPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFMINUSPER"));
                        string StrPlusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFMINUSRUPEES"));

                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, GetStringForToolTip(StrExcRate, StrPlusDollar, StrPlusDollarPer, StrPlusRupees, "DFMINUSRUPEES", ""));
                        return;
                    }
                }

                finally
                {
                    e.Info = info;
                }
            }
        }

        public string GetStringForToolTip(string pStrExcRate, string pStrDollar, string pStrDollarPer, string pStrRupees, string pStrType, string pFinalProcessAmt)
        {
            string StrToolTips = "";
            return StrToolTips;
        }


        private void MainGridDetail_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            }




        }

        private void lblPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Factory Production Summary (" + Str + ")";
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGridSummary;
                link.Landscape = true;

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

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdSummary.BestFitColumns();
            GrdDetail.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void toolTipController2_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {

        }

        private void GrdDetSummary_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmbWagesBase_EditValueChanged(object sender, EventArgs e)
        {
            if (GrdDetail.FocusedRowHandle >= 0)
            {
                GrdDetail.PostEditor();
                string TrnID = Val.ToString(GrdDetail.GetFocusedRowCellValue("TRN_ID"));
                string WagesBase = Val.ToString(GrdDetail.GetFocusedRowCellValue("WAGESBASE"));

                int IntRes = Obj.UpdateLiveGradingColorFlag(TrnID, WagesBase);
                this.Cursor = Cursors.Default;
                if (IntRes == -1)
                {
                    Global.MessageError("Opps....Something Goes Wrong");
                }
            }

        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Blocking Checker Labour Summary (" + Str + ")";
                try
                {
                    SaveFileDialog svDialog = new SaveFileDialog();
                    svDialog.DefaultExt = ".xlsx";
                    svDialog.Title = "Export to Excel";
                    svDialog.FileName = "LabourSummary.xlsx";
                    svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                    {
                        PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                        {
                            PrintingSystemBase = new PrintingSystemBase(),
                            Component = MainGridSummary,
                            Landscape = true,
                            PaperKind = PaperKind.A4,
                            Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                        };

                        link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                        link.ExportToXlsx(svDialog.FileName);

                        if (Global.Confirm("Do You Want To Open [PolishOkDetail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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
            else
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Factory Production Labour Detail (" + Str + ")";
                try
                {
                    SaveFileDialog svDialog = new SaveFileDialog();
                    svDialog.DefaultExt = ".xlsx";
                    svDialog.Title = "Export to Excel";
                    svDialog.FileName = "LabourDetail.xlsx";
                    svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                    {
                        PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                        {
                            PrintingSystemBase = new PrintingSystemBase(),
                            Component = MainGridDetail,
                            Landscape = true,
                            PaperKind = PaperKind.A4,
                            Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                        };

                        link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                        link.ExportToXlsx(svDialog.FileName);

                        if (Global.Confirm("Do You Want To Open [PolishOkDetail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (DTabDetail == null || DTabDetail.Rows.Count == 0)
            {
                Global.MessageError("There Is No Data For Print");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.MdiParent = Global.gMainRef;
            FrmReportViewer.ShowForm("WorkerLabourReport", DTabDetail);

            this.Cursor = Cursors.Default;
        }

        private void lblSaveLayoutSummary_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdSummary.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();
            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdSummary.Name, text);

            Stream strYM = new System.IO.MemoryStream();
            GrdDetYearMonth.SaveLayoutToStream(strYM);
            strYM.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader readerYM = new StreamReader(strYM);
            string textYM = readerYM.ReadToEnd();
            int IntResYM = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDetYearMonth.Name, textYM);

            if (IntResYM != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblDeleteLayoutSummary_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdSummary.Name);

            int IntResYm = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDetYearMonth.Name);

            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void lblSaveLayoutDetail_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdDetail.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDetail.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblDeleteLayoutDetail_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDetail.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void GrdDetail_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    DataRow DR = GrdDetail.GetDataRow(e.RowHandle);
                    string StrKapan = Val.ToString(DR["KAPANNAME"]);
                    int IntPacketNO = Val.ToInt(DR["PACKETNO"]);
                    string StrTag = Val.ToString(DR["TAG"]);
                    string StrPacketID = Val.ToString(DR["PACKET_ID"]);
                    string StrWorkerName = Val.ToString(DR["WORKERCODE"]);
                    Int64 IntWorkerID = Val.ToInt64(DR["WORKER_ID"]);

                    FrmFactoryLabourDetail FrmFactoryLabourDetail = new FrmFactoryLabourDetail();
                    FrmFactoryLabourDetail.MdiParent = Global.gMainRef;
                    FrmFactoryLabourDetail.ShowForm(StrKapan, IntPacketNO, StrTag, StrPacketID, StrWorkerName, IntWorkerID, "BLOCKINGCHECKER");
                    ObjFormEvent.ObjToDisposeList.Add(FrmFactoryLabourDetail);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdSummary_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0)
            {
                this.Cursor = Cursors.WaitCursor;
                GrdDetail.Columns["WORKERCODE"].ClearFilter();
                GrdDetail.Columns["WORKERCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("WORKERCODE='" + Val.ToString(GrdSummary.GetRowCellValue(e.RowHandle, "WORKERCODE")) + "'");

                byte[] OFFICELOGO = GrdSummary.GetRowCellValue(e.RowHandle, "EMPPHOTO") as byte[] ?? null;
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

                txtEmpCode.Text = Val.ToString(GrdSummary.GetRowCellValue(e.RowHandle, "WORKERCODE"));

                xtraTabControl1.SelectedTabPageIndex = 1;
                this.Cursor = Cursors.Default;
            }
        }

        private void GrdDetail_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
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
                        //DouTotalPlanVarDollar = DouTotalPlanVarDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PLANVARIATIONDOLLAR"));
                        //DouTotalPlanVarRupees = DouTotalPlanVarRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PLANVARIATIONRUPEES"));

                        //DouTotalDFPlusDollar = DouTotalDFPlusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFPLUSDOLLAR"));
                        //DouTotalDFMinusDollar = DouTotalDFMinusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFMINUSDOLLAR"));
                        //DouTotalDFPlusRupees = DouTotalDFPlusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFPLUSRUPEES"));
                        //DouTotalDFMinusRupees = DouTotalDFMinusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFMINUSRUPEES"));
                        //DouTotalPcs = DouTotalPcs + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PCS"));   //#P : 21-01-2020


                        //DouTotalFinalLabourDollar = DouTotalFinalLabourDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "FINAL_LABOURAMOUNTDOLLAR"));
                        //DouTotalFinalLabourRupees = DouTotalFinalLabourRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "FINAL_LABOURAMOUNTRUPEES"));

                        DouTotalDPlusDollar_BeforePer = DouTotalDPlusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_PLUSDOLLAR"));
                        DouTotalDMinusDollar_BeforePer = DouTotalDMinusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_MINUSDOLLAR"));

                        //DouTotalDFPlusDollar_BeforePer = DouTotalDFPlusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_DFPLUSDOLLAR"));
                        //DouTotalDFMinusDollar_BeforePer = DouTotalDFMinusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_DFMINUSDOLLAR"));

                        //DouTotalORGAmount = DouTotalORGAmount + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "TOTALORGAMOUNT"));
                        //DouTotalGRDAmount = DouTotalGRDAmount + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "TOTALGRDAMOUNT"));
                        //DouTotalDPlusDollar = DouTotalDPlusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PLUSDOLLAR"));
                        //DouTotalDMinusDollar = DouTotalDMinusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "MINUSDOLLAR"));
                        //DouTotalDPlusRupees = DouTotalDPlusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DOLLARPLUSRUPEES"));
                        //DouTotalDMinusRupees = DouTotalDMinusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DOLLARMINUSRUPEES"));
                    }
                }

                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_PLUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDPlusDollar_BeforePer, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_MINUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDMinusDollar_BeforePer, "###0.00");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GrdSummary_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    DataRow DRow = GrdSummary.GetDataRow(e.FocusedRowHandle);
                    if (DRow == null)
                        return;

                    this.Cursor = Cursors.WaitCursor;
                    GrdDetYearMonth.Columns["WORKERCODE"].ClearFilter();
                    GrdDetYearMonth.Columns["WORKERCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("WORKERCODE='" + Val.ToString(DRow["WORKERCODE"]) + "'");

                    byte[] OFFICELOGO = GrdSummary.GetRowCellValue(e.FocusedRowHandle, "EMPPHOTO") as byte[] ?? null;
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
                    txtEmpCode.Text = Val.ToString(GrdSummary.GetRowCellValue(e.FocusedRowHandle, "WORKERCODE"));

                    string Str = "Year Month Wise Salary Summary Of Employee : '" + Val.ToString(DRow["WORKERCODE"]) + "'";
                    GrpYearMonthWiseSummary.Text = Str;

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void GrdDetYearMonth_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0)
            {
                this.Cursor = Cursors.WaitCursor;
                GrdDetail.Columns["WORKERCODE"].ClearFilter();
                GrdDetail.Columns["YYMMM"].ClearFilter();
                GrdDetail.Columns["WORKERCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("WORKERCODE='" + Val.ToString(GrdDetYearMonth.GetRowCellValue(e.RowHandle, "WORKERCODE")) + "' AND YYMMM='" + Val.ToString(GrdDetYearMonth.GetRowCellValue(e.RowHandle, "YYMMM")) + "'");



                xtraTabControl1.SelectedTabPageIndex = 1;
                this.Cursor = Cursors.Default;
            }
        }

        private void repChkISConsiderPlusDVariation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetail.FocusedRowHandle < 0)
                    return;


                this.Cursor = Cursors.WaitCursor;
                GrdDetail.PostEditor();

                DataRow Dr = GrdDetail.GetFocusedDataRow();

                int IntRes = Obj.SaveWagesParameterData("PLUSDVARIATION", Val.ToString(Dr["KAPANNAME"]), Val.ToInt32(Dr["PACKETNO"]), Val.ToString(Dr["TAG"]), Val.ToInt64(Dr["WORKER_ID"]), Val.ToBoolean(Dr["ISCONSIDERPLUSDOLLARVARIATION"]), Val.ToBoolean(Dr["ISPROCESSPKTWITHFMKBLCLARITY"]), false, false, "BLOCKING");

                if (IntRes <= 0)
                    Global.Message("Opps.. Something Goes Wrong Please Check.");


                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (txtPass.Text != "" && txtPass.Tag.ToString().ToUpper() == txtPass.Text.ToString().ToUpper())
            {
                //GrdDetail.Columns["GRAPHPLUS"].Visible = true;
                GrdDetail.Columns["DOLLARVARIATIONPLUS"].Visible = true;
                GrdDetail.Columns["ISCONSIDERPLUSDOLLARVARIATION"].Visible = true;
                //GrdDetail.Columns["TOTAL_LABOURPERTEXT"].Visible = true;
                //GrdDetail.Columns["TOTAL_LABOURPER"].Visible = true;
                GrdDetail.Columns["ISPROCESSPKTWITHFMKBLCLARITY"].Visible = true;
                GrdDetail.Columns["BEFOREPER_MINUSDOLLAR_WITHOUTMFG"].Visible = true;

                GrdDetail.Bands["BANDTALLYAMOUNT"].Visible = true;
                GrdDetail.Columns["BTNDATAPROCESS"].Visible = true;
                GrdDetail.Columns["BTNPROCESSPRDDETAIL"].Visible = true;

            }
            else
            {
                //GrdDetail.Columns["GRAPHPLUS"].Visible = false;
                GrdDetail.Columns["DOLLARVARIATIONPLUS"].Visible = false;
                GrdDetail.Columns["ISCONSIDERPLUSDOLLARVARIATION"].Visible = false;
                //GrdDetail.Columns["TOTAL_LABOURPERTEXT"].Visible = false;
                //GrdDetail.Columns["TOTAL_LABOURPER"].Visible = false;
                GrdDetail.Columns["ISPROCESSPKTWITHFMKBLCLARITY"].Visible = false;
                GrdDetail.Columns["BEFOREPER_MINUSDOLLAR_WITHOUTMFG"].Visible = false;
                GrdDetail.Bands["BANDTALLYAMOUNT"].Visible = false;
                GrdDetail.Columns["BTNDATAPROCESS"].Visible = false;
                GrdDetail.Columns["BTNPROCESSPRDDETAIL"].Visible = false;
            }
        }

        private void GrdDetail_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            int IntISRejectEmp = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ISREJECTEDEMP"));
            if (IntISRejectEmp == 1)
            {
                //e.Appearance.BackColor = Color.FromArgb(197, 217, 241);
            }
        }

        private void GrdSummary_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            int IntISRejectEmp = Val.ToInt(GrdSummary.GetRowCellValue(e.RowHandle, "ISREJECTEDEMP"));
            if (IntISRejectEmp == 1)
            {
                e.Appearance.BackColor = Color.FromArgb(197, 217, 241);
            }
        }

        private void GrdDetYearMonth_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            int IntISRejectEmp = Val.ToInt(GrdDetYearMonth.GetRowCellValue(e.RowHandle, "ISREJECTEDEMP"));
            if (IntISRejectEmp == 1)
            {
                e.Appearance.BackColor = Color.FromArgb(197, 217, 241);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DSDetail = Obj.GetBlockingCheckerSalaryViewSummaryDetail(mStrKapanName, mIntFromPacketNo, mIntToPacketNo, 531, mIntEmployee_ID, Val.SqlDate(mStrFromDate), Val.SqlDate(mStrToDate));

                if (DSDetail.Tables.Count > 0)
                {
                    DTabSummaryYearMonth = DSDetail.Tables[0];
                    DTabSummary = DSDetail.Tables[1];
                    DTabDetail = DSDetail.Tables[2];
                    //DTabPlanVariationDetail = DS.Tables[3];
                    //DTabDFPlusMinus = DS.Tables[4];
                }
            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnRefresh.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelProgress.Visible = false;
                BtnRefresh.Enabled = true;

                if (DSDetail.Tables.Count <= 0)
                {
                    Global.Message("No Data Found..");
                }

                GrdDetail.BeginUpdate();
                GrdSummary.BeginUpdate();
                GrdDetYearMonth.BeginUpdate();

                MainGridSummary.DataSource = DTabSummary;
                GrdSummary.RefreshData();

                MainGrdYearMonth.DataSource = DTabSummaryYearMonth;
                GrdDetYearMonth.RefreshData();

                MainGridDetail.DataSource = DTabDetail;
                GrdDetail.RefreshData();
                //GrdDetail.BestFitColumns();
                GrdDetail.Columns["WORKERCODE"].ClearFilter();

                GrdDetail.EndUpdate();
                GrdSummary.EndUpdate();
                GrdDetYearMonth.EndUpdate();
            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnRefresh.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void repChkISProcessPktWithFinalMkblClarity_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetail.FocusedRowHandle < 0)
                    return;


                this.Cursor = Cursors.WaitCursor;
                GrdDetail.PostEditor();

                DataRow Dr = GrdDetail.GetFocusedDataRow();

                int IntRes = Obj.SaveWagesParameterData("FMKBLCLARITY", Val.ToString(Dr["KAPANNAME"]), Val.ToInt32(Dr["PACKETNO"]), Val.ToString(Dr["TAG"]), Val.ToInt64(Dr["WORKER_ID"]), Val.ToBoolean(Dr["ISCONSIDERPLUSDOLLARVARIATION"]), Val.ToBoolean(Dr["ISPROCESSPKTWITHFMKBLCLARITY"]), false, false, "BLOCKING");

                if (IntRes <= 0)
                    Global.Message("Opps.. Something Goes Wrong Please Check.");

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void repBtnDataProcess_Click(object sender, EventArgs e)
        {
            try
            {

                if (GrdDetail.FocusedRowHandle < 0)
                    return;

                DataRow Dr = GrdDetail.GetFocusedDataRow();

                if (Dr == null)
                    return;

                string StrKapan = "";
                int IntPacketNo = 0;
                string StrTag = "";

                StrKapan = Val.ToString(Dr["KAPANNAME"]);
                IntPacketNo = Val.ToInt32(Dr["PACKETNO"]);
                StrTag = Val.ToString(Dr["TAG"]);

                string StrPacketTag = StrKapan + "-" + Val.ToString(IntPacketNo) + Val.ToString(StrTag);

                if (Global.Confirm("Are You Sure To Process Data For Packet : [" + StrPacketTag + "]") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                int IntRes = 0;

                IntRes = Obj.ResetMarkerWorkerPacketDataForProcess(StrKapan, IntPacketNo, IntPacketNo, StrTag, Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), "BLOCKING"); //PolishOk Process
                IntRes = Obj.UpdateBlockingChkrLabourLiveGradingColorStatus(StrKapan, null, null, IntPacketNo, IntPacketNo, StrTag);

                if (IntRes >= 0)
                {
                    lblMessageForDetail.Text = "Data Process Successfully For [" + StrPacketTag + "]";
                }
                else
                {
                    lblMessageForDetail.Text = "Something Goes Wrong When Process Packet : [" + StrPacketTag + "]";
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void repBtnProcessPrdDetail_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow Dr = GrdDetail.GetFocusedDataRow();

                if (Dr == null)
                    return;

                this.Cursor = Cursors.WaitCursor;
                string StrKapan = "";
                int IntPacketNo = 0;
                string StrTag = "";

                StrKapan = Val.ToString(Dr["KAPANNAME"]);
                IntPacketNo = Val.ToInt32(Dr["PACKETNO"]);
                StrTag = Val.ToString(Dr["TAG"]);

                FrmPrdViewForProcessPktDetailSimple FrmPrdViewForProcessPktDetailSimple = new FrmPrdViewForProcessPktDetailSimple();
                FrmPrdViewForProcessPktDetailSimple.MdiParent = Global.gMainRef;
                FrmPrdViewForProcessPktDetailSimple.ShowForm(StrKapan, IntPacketNo, StrTag, "BLOCKING");
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }
    }
}
