using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Data;
using DevExpress.XtraCharts;
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
    public partial class FrmProcessWiseReport : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();
        
        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();
        DataSet DS = new DataSet();

        DataTable DTabDetail = new DataTable();
        DataTable DTabSummary = new DataTable();
        DataTable DTabJanged = new DataTable();

        System.Diagnostics.Stopwatch watch = null;

        double pDouMainCts = 0;
        string mMainManager_ID = string.Empty;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        
        string mStrReportTitle = "";
        string mStrProcess = "";
        string mStrRequiredProcess = "";
        string mStrDepartment = "";
        string mStrKapanname = "";
        string mStrKapanID = "";

        int IntIssueWise = 0;
        
        string StrFromDate = "", StrToDate = "";
        bool BoolNotInProcess = false;

        public FrmProcessWiseReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

           
            this.Show();            

            CmbKapan.Properties.DataSource = ObjPacket.FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPAN_ID";


            DataTable DTabProcess = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PROCESS);
            DataTable DTabDepartment = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_DEPARTMENT);

            CmbProcess.Properties.DataSource = DTabProcess;
            CmbProcess.Properties.DisplayMember = "PROCESSNAME";
            CmbProcess.Properties.ValueMember = "PROCESS_ID";

            CmbRequiredProcess.Properties.DataSource = DTabProcess;
            CmbRequiredProcess.Properties.DisplayMember = "PROCESSNAME";
            CmbRequiredProcess.Properties.ValueMember = "PROCESS_ID";

            CmbDepartment.Properties.DataSource = DTabDepartment;
            CmbDepartment.Properties.DisplayMember = "DEPARTMENTNAME";
            CmbDepartment.Properties.ValueMember = "DEPARTMENT_ID";
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
                
                if (CmbProcess.Text.Trim().Length == 0)
                {
                    Global.Message("Plese Select Process");
                    CmbProcess.Focus();
                    return;
                }

                GrdSummary.FocusedRowHandle = 1;

                StrFromDate = "";
                StrToDate = "";

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                mStrProcess = Val.Trim(CmbProcess.Properties.GetCheckedItems());
                mStrRequiredProcess = Val.Trim(CmbRequiredProcess.Properties.GetCheckedItems());
                mStrDepartment = Val.Trim(CmbDepartment.Properties.GetCheckedItems());

                mStrKapanID =  Val.Trim(CmbKapan.Properties.GetCheckedItems());
                mStrKapanname   = Val.Trim(CmbKapan.Text);

                if (txtMultiMainManager.Text.Trim().Equals(string.Empty))
                {
                    mMainManager_ID = string.Empty;
                }
                else
                {
                    mMainManager_ID = Val.Trim(txtMultiMainManager.Tag.ToString());
                }

                if (RbtIssue.Checked)
                {
                    IntIssueWise = Val.ToInt(RbtIssue.Tag);
                }
                else if (RbtTransfer.Checked)
                {
                    IntIssueWise = Val.ToInt(RbtTransfer.Tag);
                }
                else if (RbtReturn.Checked)
                {
                    IntIssueWise = Val.ToInt(RbtReturn.Tag);
                }
                else if (RbtPending.Checked)
                {
                    IntIssueWise = Val.ToInt(RbtPending.Tag);
                }

                BoolNotInProcess = ChkNotInProcess.Checked;

                DTabDetail.Rows.Clear();
                DTabSummary.Rows.Clear();
                DTabJanged.Rows.Clear();
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                watch = System.Diagnostics.Stopwatch.StartNew();
                BtnRefresh.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
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


            int IntHeight = 0;
            TextBrick BrickCompany = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Black, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickCompany.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickCompany.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickCompany.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickCompany.ForeColor = Color.FromArgb(27, 66, 105);

            IntHeight = IntHeight + 20;
            TextBrick BrickTitle = e.Graph.DrawString(mStrReportTitle, System.Drawing.Color.Navy, new RectangleF(0, IntHeight, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 9, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitle.ForeColor = Color.FromArgb(27, 66, 105);

            IntHeight = IntHeight + 20;
            string StrFilter = "From Date : " + DTPFromDate.Value.ToShortDateString() + " To " + DTPToDate.Value.ToShortDateString() + "\n";
            StrFilter = StrFilter + "Process : " + Val.ToString(CmbProcess.Text) + " Kapan : " + Val.ToString(CmbKapan.Text);
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
            mStrReportTitle = "Process Wise Report (Employee Wise)";
            CommonPrintFuction(MainGrdSummary, GrdSummary);

        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Process Wise Report (Employee Wise)";
            CommonExcelExportFuction(MainGrdSummary, GrdSummary, "ProcessWiseReport");
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

                pGrdDet.BestFitColumns();
                link.Component = pMainGrid;
                link.Landscape = false;
                pGrdDet.OptionsPrint.AutoWidth = false;

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

        private void FrmPolishOKReport_KeyDown(object sender, KeyEventArgs e)
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
                if (e == null)
                {
                    return;
                }
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                //DataRow DRow = GrdSummary.GetDataRow(e.FocusedRowHandle);

                //GrpDetail.Text = Val.ToString(DRow["WORKERCODE"]) + " - " + Val.ToString(DRow["WORKERNAME"]);

                //DTabDetail = Obj.GetPolishOKReport("DETAIL", Val.Trim(CmbKapan.Properties.GetCheckedItems()), mIntProcessID, Val.ToInt64(DRow["WORKER_ID"]), Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()));

                //MainGridDetail.DataSource = DTabDetail;
                //GrdDetDetail.RefreshData();
                //GrdDetDetail.BestFitColumns();

                DataRow DR = GrdSummary.GetFocusedDataRow();
                txtEmpCode.Text = Val.ToString(DR["TOWORKERCODE"]);

                byte[] OFFICELOGO = GrdSummary.GetRowCellValue(e.FocusedRowHandle, "IMG") as byte[] ?? null;
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

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void lblPacketExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "ProcessWiseReportDetail (" + GrpDetail.Text + ")";
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "ProcessWiseReportDeatail.xlsx";
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

                    if (Global.Confirm("Do You Want To Open [ProcessWiseReportDetail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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
                mStrReportTitle = "Process Wise Report Detail (" + GrpDetail.Text + ")";
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


        private void GrdSummary_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GrdDetDetail.Columns["TOWORKERCODE"].ClearFilter();
            GrdDetDetail.Columns["TOWORKERCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("TOWORKERCODE='" + Val.ToString(GrdSummary.GetFocusedRowCellValue("WORKERCODE")) + "'");

            GrdJanged.Columns["WORKERCODE"].ClearFilter();
            GrdJanged.Columns["WORKERCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("WORKERCODE='" + Val.ToString(GrdSummary.GetFocusedRowCellValue("WORKERCODE")) + "'");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DS = Obj.GetProcessWiseReport(mStrKapanname,mStrKapanID, mStrProcess, Val.ToInt64(txtEmployee.Tag), StrFromDate, StrToDate, IntIssueWise, BoolNotInProcess, mMainManager_ID, mStrDepartment,mStrRequiredProcess);
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BtnRefresh.Enabled = true;
                PanelProgress.Visible = false;

                DTabSummary = DS.Tables[0];
                DTabJanged = DS.Tables[1];
                DTabDetail = DS.Tables[2];

                GrdSummary.BeginUpdate();
                GrdJanged.BeginUpdate();
                GrdDetDetail.BeginUpdate();

                MainGrdSummary.DataSource = DTabSummary;
                GrdSummary.RefreshData();

                MainGrdJanged.DataSource = DTabJanged;
                GrdJanged.RefreshData();

                MainGridDetail.DataSource = DTabDetail;
                GrdDetDetail.RefreshData();

                GrdSummary.BestFitColumns();
                GrdJanged.BestFitColumns();
                GrdDetDetail.BestFitColumns();

                GrdSummary.EndUpdate();
                GrdJanged.EndUpdate();
                GrdDetDetail.EndUpdate();

                watch.Stop();
                lblTime.Text = string.Format("{0:hh\\:mm\\:ss}", watch.Elapsed);

                if (GrdSummary.FocusedRowHandle == 0)
                {
                    GrdDet_FocusedRowChanged(null, null);
                }
            }
            catch (Exception Ex)
            {
                BtnRefresh.Enabled = true;
                PanelProgress.Visible = false;
                Global.Message(Ex.Message.ToString());
            }
        }

        private void GrdDetDetail_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string MergeOnStr = "MAINCTS,KAPAN,PACKET,TAG";

            string MergeOn = "PACKET_ID";

            if (MergeOnStr.Contains(e.Column.FieldName))
            {
                string val1 = Val.ToString(GrdDetDetail.GetRowCellValue(e.RowHandle1, GrdDetDetail.Columns[MergeOn]));
                string val2 = Val.ToString(GrdDetDetail.GetRowCellValue(e.RowHandle2, GrdDetDetail.Columns[MergeOn]));
                if (val1 == val2)
                    e.Merge = true;
                else
                {
                }
                e.Handled = true;
            }
        }

        private void GrdDetDetail_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    pDouMainCts = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    string P1 = Val.ToString(GrdDetDetail.GetRowCellValue(e.RowHandle, "PACKET_ID"));
                    string P2 = Val.ToString(GrdDetDetail.GetRowCellValue(e.RowHandle - 1, "PACKET_ID"));
                    if (P1 != P2)
                    {
                        pDouMainCts = pDouMainCts + Val.Val(GrdDetDetail.GetRowCellValue(e.RowHandle, "MAINCTS"));
                    }
                }

                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("MAINCTS") == 0)
                    {
                        e.TotalValue = pDouMainCts;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }


        private void lblJangedExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Process Wise Report (Janged Wise)";
            CommonExcelExportFuction(MainGrdJanged, GrdJanged, "ProcessWiseReport");
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

        private void lblJangedPrint_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "Process Wise Report (Janged Wise)";
            CommonPrintFuction(MainGrdJanged, GrdJanged);
        }

        private void RbtTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtIssue.Checked == true)
            {
                GrpJanged.Visible = true;
                GrpSummary.Dock = DockStyle.Top;
                GrpJanged.Dock = DockStyle.Fill;
            }
            else
            {
                GrpJanged.Visible = false;
                GrpSummary.Dock = DockStyle.Fill;
            }
        }


    }
}
