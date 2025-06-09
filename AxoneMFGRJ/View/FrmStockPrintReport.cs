using AxoneMFGRJ.Report;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
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

namespace AxoneMFGRJ.View
{
    public partial class FrmStockPrintReport : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        System.Diagnostics.Stopwatch watch = null;

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DtabStockPrint = new DataTable();
        string mStrOpe = "";
        string mStrKapan = "";
        string mStrKapanID = "";
        string mStrTable = "";
        string mStrMarker_ID = "";
        string mStrMainManager_ID = "";
        string mStrOpeViewType = "";
        string mStrPacketCategory = "";
        string mStrPacketGroup = "";

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        public FrmStockPrintReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            FillControl();

            CmbViewType.SelectedIndex = 0;

            this.Show();

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

        public void FillControl()
        {
            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPAN_ID";

            // Dhara : 23-03-2023 start

            chkCmbTable.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_TABLE);
            chkCmbTable.Properties.DisplayMember = "TABLENAME";
            chkCmbTable.Properties.ValueMember = "TABLE_ID";

            // Dhara : 23-03-2023 End

            chkCmbPacketCat.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PACKETCATEGORY);
            chkCmbPacketCat.Properties.DisplayMember = "PACKETCATEGORYNAME";
            chkCmbPacketCat.Properties.ValueMember = "PACKETCATEGORY_ID";

            chkCmbPacketGroup.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PACKETGROUP);
            chkCmbPacketGroup.Properties.DisplayMember = "PACKETGROUPNAME";
            chkCmbPacketGroup.Properties.ValueMember = "PACKETGROUP_ID";
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {

            DtabStockPrint.Rows.Clear();

            mStrKapan = Val.Trim(CmbKapan.Text);
            mStrKapanID = Val.Trim(CmbKapan.Properties.GetCheckedItems());
            mStrTable = Val.Trim(chkCmbTable.Properties.GetCheckedItems());
            mStrOpe = "SUMMARY";

            mStrMarker_ID = Val.ToString(txtMarkerCode.Text).Trim().Equals(string.Empty) ? "" : Val.Trim(txtMarkerCode.Tag);
            mStrMainManager_ID = Val.ToString(txtMultiMainManager.Text).Trim().Equals(string.Empty) ? "" : Val.Trim(txtMultiMainManager.Tag);

            mStrPacketGroup = Val.Trim(chkCmbPacketGroup.Properties.GetCheckedItems());
            mStrPacketCategory = Val.Trim(chkCmbPacketCat.Properties.GetCheckedItems());

            if (CmbViewType.SelectedIndex == 1) //Marker with Kapan Summary
            {
                mStrOpeViewType = "MARKER_KAPAN_SUM";
                GrdDet.Columns["KAPANNAME"].Visible = true;
                GrdDet.Columns["PROCESSNAME"].Visible = false;
            }
            else if (CmbViewType.SelectedIndex == 2) //Marker With Process Summary
            {
                mStrOpeViewType = "MARKER_PROCESS_SUM";
                GrdDet.Columns["KAPANNAME"].Visible = false;
                GrdDet.Columns["PROCESSNAME"].Visible = true;
            }
            else if (CmbViewType.SelectedIndex == 3) //Marker Total Summary
            {
                mStrOpeViewType = "MARKER_SUM";
                GrdDet.Columns["KAPANNAME"].Visible = false;
                GrdDet.Columns["PROCESSNAME"].Visible = false;
            }
            else    //Marker With Kapan + Process Summary                         
            {
                mStrOpeViewType = "MARKER_KAPAN_PROCESS_SUM";
                GrdDet.Columns["KAPANNAME"].Visible = true;
                GrdDet.Columns["PROCESSNAME"].Visible = true;
            }
            watch = System.Diagnostics.Stopwatch.StartNew();

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            BtnRefresh.Enabled = false;
            PanelProgress.Visible = true;
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }

            /*
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string StrOpe = "";

                if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                    txtEmployee.Tag = string.Empty;


                DtabStockPrint = Obj.GetStockPrintReport(StrOpe, Val.Trim(CmbKapan.Properties.GetCheckedItems()));

                //GrdDet.BeginUpdate();

                if (DtabStockPrint.Rows.Count <= 0)
                {
                    Global.Message("No Data Found");
                    return;
                }

                MainGrd.DataSource = DtabStockPrint;
                GrdDet.RefreshData();

                //foreach (DevExpress.XtraGrid.Columns.GridColumn Col in GrdDet.Columns)
                //{
                //    Col.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                //    Col.Width = 85;
                //}

                //GrdDet.BestFitColumns();

                //GrdDet.EndUpdate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }
            */
        }

        public void Link_CreateMarginalHeaderAreaSummary(object sender, CreateAreaEventArgs e)
        {
            int IntHeight = 0;
            TextBrick BrickCompany = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Black, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickCompany.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickCompany.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickCompany.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickCompany.ForeColor = Color.FromArgb(27, 66, 105);

            IntHeight = IntHeight + 20;
            TextBrick BrickTitle = e.Graph.DrawString("STOCK PRINT SUMMARY (MARKER WISE)", System.Drawing.Color.Navy, new RectangleF(0, IntHeight, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 9, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitle.ForeColor = Color.FromArgb(27, 66, 105);

            IntHeight = IntHeight + 20;
            string StrFilter = "Stock AS On Date : " + DateTime.Now.ToShortDateString() +"\n";
            StrFilter = StrFilter + "Marker : " + txtMarkerCode.Text + " | Main Magager : " + txtMultiMainManager.Text;
            TextBrick BrickTitlesParam = e.Graph.DrawString(StrFilter, System.Drawing.Color.Navy, new RectangleF(0, IntHeight, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("Verdana", 8, FontStyle.Regular);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            BrickTitlesParam.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, IntHeight, 400, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrdDet_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0 || e.Column.FieldName == "KAPANNAME")
                {
                    return;
                }


                if (e.Clicks == 2)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string StrReportTitle = "";

                    DataRow Dr = GrdDet.GetFocusedDataRow();
                    string StrMarker_ID = "";
                    string StrKapanName = "";
                    string StrProcessName = "";
                    string StrMainManager_ID = "";

                    //StrKapanName = Val.Trim(Dr["KAPANNAME"]);
                    StrKapanName = Val.Trim(Dr["KAPANNAME"]).Equals(string.Empty) ? mStrKapan : Val.Trim(Dr["KAPANNAME"]);
                    StrMarker_ID = Val.ToString(Dr["MARKER_ID"]).Trim().Equals(string.Empty) ? mStrMarker_ID : Val.ToString(Dr["MARKER_ID"]); 
                    StrProcessName = Val.ToString(Dr["PROCESSNAME"]);

                    StrMainManager_ID = Val.ToString(txtMultiMainManager.Text).Trim().Equals(string.Empty) ? "" : Val.Trim(txtMultiMainManager.Tag);


                    if (Val.ToString(Dr["MARKERNAME"]).ToUpper() == "TOTAL")
                    {
                        StrProcessName = "ALL_TOTAL";
                    }

                    StrMarker_ID = StrMarker_ID == "0" ? "" : StrMarker_ID;

                    //if (Val.ToString(Dr["EMPLOYEECODE"]).Trim().ToUpper().Equals("TOTAL"))
                    //    return;

                    DataTable DtabStockPrintDetail = Obj.GetStockPrintReport("DETAIL", StrKapanName, mStrKapanID, mStrTable, StrMarker_ID, StrProcessName, StrMainManager_ID, mStrOpeViewType, mStrPacketCategory, mStrPacketGroup);

                    StrReportTitle = "[" + Val.ProperText(Val.ToString(Dr["MARKERNAME"])) + " : " + Val.ToString(Dr["KAPANNAME"]) + " : " + Val.ProperText(Val.ToString(Dr["PROCESSNAME"])) + "] Detail";
                    GrpPacketSearch.Text = StrReportTitle;
                    Val.FormGeneralSetting(this);
                    AttachFormDefaultEvent();

                    GrdDetPacketDetail.BeginUpdate();
                    MainGridPacketDetail.DataSource = DtabStockPrintDetail;

                    GrdDetPacketDetail.RefreshData();
                    GrdDetPacketDetail.BestFitColumns();
                    GrdDetPacketDetail.EndUpdate();

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

        private void FrmMarkerRollingReport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    BtnRefresh.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000" || e.DisplayText == "0 (0.000)")
                {
                    e.DisplayText = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "Stock Print Detail";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGridPacketDetail,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [StockPrintDetail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void lblDeptPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGridPacketDetail;
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

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGridPacketDetail) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGridPacketDetail.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FROMEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FROMEMPLOYEENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TOEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOEMPLOYEENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "ISSUECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "ISSUENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "PREVISSUECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "PREVISSUENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MARKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "MARKERNAME")));
                    return;
                }
            }
            finally
            {
                e.Info = info;
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
            TextBrick BrickTitleseller = e.Graph.DrawString(GrpPacketSearch.Text, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("Stock Print Detail :- " + DateTime.Now.ToString("dd/MM/yyyy"), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        private void lblExportSummary_Click(object sender, EventArgs e)
        {

            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "StockPrintReportSummary.xlsx";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    GrdDet.Appearance.Row.Font = new Font("Verdana", 8.25f);
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrd,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary);

                    link.ExportToXlsx(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [StockPrintReportSummary.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                GrdDet.Appearance.Row.Font = new Font("Verdana", 8);
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }

        }

        private void lblPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {

                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

                link.Component = MainGrd;
                link.Landscape = false;
                link.PaperKind = PaperKind.A4;
                GrdDet.OptionsPrint.AutoWidth = false;

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
        private void GrdDet_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                string StrProcessname = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PROCESSNAME"));
                string StrMarkerName = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "MARKERNAME"));


                if (StrProcessname.ToUpper() == "TOTAL")
                {
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkBlue;
                    e.Appearance.BackColor = Color.LightGray;
                }
                if (StrMarkerName.ToUpper() == "TOTAL")
                {
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkBlue;
                    e.Appearance.BackColor = Color.DarkGray;
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                string StrKapanName = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "KAPANNAME"));

                if (StrKapanName.ToUpper() == "TOTAL")
                {
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkBlue;
                    e.Appearance.BackColor = Color.LightGray;
                    e.Appearance.BackColor2 = Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //DtabStockPrint = Obj.GetStockPrintReport_25082023(mStrOpe, mStrKapan,mStrKapanID, mStrTable, mStrMarker_ID, "", mStrMainManager_ID, mStrOpeViewType, mStrPacketCategory, mStrPacketGroup);
                DtabStockPrint = Obj.GetStockPrintReport(mStrOpe, mStrKapan,mStrKapanID, mStrTable, mStrMarker_ID, "", mStrMainManager_ID, mStrOpeViewType, mStrPacketCategory, mStrPacketGroup);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BtnRefresh.Enabled = true;
                PanelProgress.Visible = false;

                MainGrd.DataSource = DtabStockPrint;
                MainGrd.RefreshDataSource();
                GrdDet.BestFitColumns();
                watch.Stop();
                lblTime.Text = string.Format("{0:hh\\:mm\\:ss}", watch.Elapsed);

            }
            catch (Exception Ex)
            {
                BtnRefresh.Enabled = true;
                PanelProgress.Visible = false;
                Global.Message(Ex.Message.ToString());
            }
        }

        private void GrdDet_CellMerge(object sender, CellMergeEventArgs e)
        {
            string MergeOnStr = "KAPANNAME,ASSORTER";
            string MergeOn = "GROUPNAME";

            if (MergeOnStr.Contains(e.Column.FieldName))
            {
                string val1 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle1, GrdDet.Columns[MergeOn]));
                string val2 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle2, GrdDet.Columns[MergeOn]));
                if (val1 == val2)
                    e.Merge = true;
                e.Handled = true;
            }
        }

        private void txtMarkerCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (txtMarkerCode.Enabled == false)
                {
                    return;
                }
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtMarkerCode.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        txtMarkerCode.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
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

        private void txtMultiMainManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_MAINMANAGER);
                    FrmSearch.mStrColumnsToHide = "LEDGER_ID,AUTOCONFIRM";
                    FrmSearch.ValueMemeter = "LEDGER_ID";
                    FrmSearch.DisplayMemeter = "LEDGERCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtMultiMainManager.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtMultiMainManager.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainGridPacketDetail_Click(object sender, EventArgs e)
        {

        }
    }
}
