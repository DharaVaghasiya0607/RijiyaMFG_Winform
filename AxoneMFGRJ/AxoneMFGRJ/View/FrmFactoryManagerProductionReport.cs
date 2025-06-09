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
    public partial class FrmFactoryManagerProductionReport : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabDetail = new DataTable();
        DataTable DTabSummary = new DataTable();
        DataTable DTabProcessWiseDetail = new DataTable();
        DataTable DTabEmpProcessWiseDetail = new DataTable();

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        string mStrReportTitle = "";

        public FORMTYPE mFormType = FORMTYPE.RD3;

        public enum FORMTYPE
        {
            RD3 = 0,
            RD4 = 1,
        }

        public FrmFactoryManagerProductionReport()
        {
            InitializeComponent();
        }
        public void ShowForm(FORMTYPE pFormType)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            mFormType = pFormType;

            CmbKapan.Properties.DataSource = ObjPacket.FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            lblTitle.Text = "Manager Production Report ( " + mFormType + ")";
            this.Text = "FACTORY MANAGER (" + mFormType + ") PRODUCTION REPORT";
            
            //21-01-2020
            if (mFormType == FORMTYPE.RD3)
            {
                txtEmployee.Text = "RD3";
                txtEmployee.Tag = "1573996121000";
                txtEmployee.Enabled = false;
                PanelHeader.BackColor = Color.FromArgb(255, 179, 200);
            }
            else
            {
                txtEmployee.Text = "RD4";
                txtEmployee.Tag = "1576488870463";
                txtEmployee.Enabled = false;
                PanelHeader.BackColor = Color.FromArgb(145, 211, 231);
            }

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

                this.Cursor = Cursors.WaitCursor;

                DTabDetail.Rows.Clear();
                DataSet DS = Obj.GetFactoryManagerProductionReport(Val.Trim(CmbKapan.Properties.GetCheckedItems()), Val.ToInt64(txtEmployee.Tag), Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), Val.ToString(mFormType));

                DTabSummary = DS.Tables[0];
                DTabDetail = DS.Tables[1];
                DTabEmpProcessWiseDetail = DS.Tables[2];

                MainGrid.DataSource = DTabSummary;
                GrdDet.RefreshData();
                GrdDet.BestFitColumns();

                MainGridDetail.DataSource = DTabDetail;
                GrdDetDetail.RefreshData();
                GrdDetDetail.BestFitColumns();
                GrdDetDetail.Columns["WORKERCODE"].ClearFilter();


                DTabProcessWiseDetail = DTabEmpProcessWiseDetail.AsEnumerable()
                .OrderBy(r => r.Field<string>("PROCESS"))
                .GroupBy(r => r.Field<string>("PROCESS"))
                .Select(g =>
                {
                    var row = DTabEmpProcessWiseDetail.NewRow();
                    row["PROCESS"] = g.Key;
                    row["ROUNDPCS"] = g.Sum(r => r.Field<int>("ROUNDPCS"));
                    row["ROUNDCARAT"] = g.Sum(r => r.Field<decimal>("ROUNDCARAT"));
                    row["FANCYPCS"] = g.Sum(r => r.Field<int>("FANCYPCS"));
                    row["FANCYCARAT"] = g.Sum(r => r.Field<decimal>("FANCYCARAT"));
                    return row;
                }).CopyToDataTable();

                MainGrdProcessWise.DataSource = DTabProcessWiseDetail;

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

            string StrFilter = "FromDate : " + DTPFromDate.Text + " To " + DTPToDate.Text;
            //StrFilter = StrFilter + ", Process : Polish OK";
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

        private void lblPrint_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Factory Production Report (Employee Wise)";
            CommonPrintFuction(MainGrid, GrdDet);

        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Factory Production Report (Employee Wise)";
            CommonExcelExportFuction(MainGrid, GrdDet, "ProductionReport");
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

        private void GrdDet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {

                if (e.FocusedRowHandle < 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                GrdDetDetail.Columns["WORKERCODE"].ClearFilter();
                GrdDetDetail.Columns["FROMPROCESSNAME"].ClearFilter();
                GrdDetDetail.Columns["SHAPETYPE"].ClearFilter();
                GrdDetDetail.Columns["WORKERCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("WORKERCODE='" + Val.ToString(GrdDet.GetFocusedRowCellValue("WORKERCODE")) + "'");

                DataTable Dtab = DTabEmpProcessWiseDetail.Select("WORKERCODE = '" + Val.ToString(GrdDet.GetFocusedRowCellValue("WORKERCODE"))+ "'").CopyToDataTable();

                if (Dtab.Rows.Count <= 0)
                {
                    DTabProcessWiseDetail.Rows.Clear();
                    DTabProcessWiseDetail.AcceptChanges();
                    return;
                }

                DTabProcessWiseDetail = Dtab.AsEnumerable()
                .OrderBy(r => r.Field<string>("PROCESS"))
                .GroupBy(r => r.Field<string>("PROCESS"))
                .Select(g =>
                {
                    var row = DTabEmpProcessWiseDetail.NewRow();
                    row["PROCESS"] = g.Key;
                    row["ROUNDPCS"] = g.Sum(r => r.Field<int>("ROUNDPCS"));
                    row["ROUNDCARAT"] = g.Sum(r => r.Field<decimal>("ROUNDCARAT"));
                    row["FANCYPCS"] = g.Sum(r => r.Field<int>("FANCYPCS"));
                    row["FANCYCARAT"] = g.Sum(r => r.Field<decimal>("FANCYCARAT"));
                    return row;
                }).CopyToDataTable();

                MainGrdProcessWise.DataSource = DTabProcessWiseDetail;
                GrdDetProcessWise.RefreshData();

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void lblPacketExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Factory Production Report Detail (" + GrpDetail.Text + ")";
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "PolishOkDetail.xlsx";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGridDetail,
                        Landscape = false,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

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

        private void lblPacketPrint_Click(object sender, EventArgs e)
        {
            try
            {
                mStrReportTitle = "Factory Production Detail (" + GrpDetail.Text + ")";
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGridDetail;
                link.Landscape = false;

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

        private void GrdDetProcessWise_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //try
            //{

            //    if (e.FocusedRowHandle < 0)
            //    {
            //        return;
            //    }
            //    this.Cursor = Cursors.WaitCursor;

            //    string StrShapeType = "";
            //    //StrShapeType =  Val.ToString(GrdDetProcessWise.GetFocusedRowCellValue("PROCESS"));

            //    GrdDetDetail.Columns["FROMPROCESSNAME"].ClearFilter();
            //    GrdDetDetail.Columns["SHAPETYPE"].ClearFilter();
            //    GrdDetDetail.Columns["FROMPROCESSNAME"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("FROMPROCESSNAME='" + Val.ToString(GrdDetProcessWise.GetFocusedRowCellValue("PROCESS")) + "' And SHAPETYPE = '" + Val.ToString(GrdDetProcessWise.GetFocusedRowCellValue("SHAPETYPE")) + "'");
            //    this.Cursor = Cursors.Default;

            //    this.Cursor = Cursors.Default;

            //}
            //catch (Exception ex)
            //{
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void GrdDetProcessWise_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {

                if (e.Clicks == 2)
                {

                    string StrShapeType = "";
                    //StrShapeType =  Val.ToString(GrdDetProcessWise.GetFocusedRowCellValue("PROCESS"));
                    if (e.Column.FieldName.ToUpper() == "ROUNDPCS" || e.Column.FieldName.ToUpper() == "ROUNDCARAT")
                    {
                        StrShapeType = "ROUND";
                    }
                    else if (e.Column.FieldName.ToUpper() == "FANCYPCS" || e.Column.FieldName.ToUpper() == "FANCYCARAT")
                    {
                        StrShapeType = "FANCY";
                    }
                    else
                    {
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    //GrdDetDetail.Columns["WORKERCODE"].ClearFilter();
                    GrdDetDetail.Columns["FROMPROCESSNAME"].ClearFilter();
                    GrdDetDetail.Columns["SHAPETYPE"].ClearFilter();
                    GrdDetDetail.Columns["FROMPROCESSNAME"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("FROMPROCESSNAME='" + Val.ToString(GrdDetProcessWise.GetFocusedRowCellValue("PROCESS")) + "' And SHAPETYPE = '" + StrShapeType + "'");
                    this.Cursor = Cursors.Default;

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void lblProcessWiseExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Factory Manager(RD3) Production Report(Process Wise)";
            CommonExcelExportFuction(MainGrdProcessWise, GrdDetProcessWise, "RD3_ProductionReport");
        }

        private void lblProcessWisePrint_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Factory Manager(RD3) Production Report(Process Wise)";
            CommonPrintFuction(MainGrdProcessWise, GrdDetProcessWise);

        }
    }
}
