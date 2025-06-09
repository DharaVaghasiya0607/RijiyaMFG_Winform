using BusLib.Transaction;
using BusLib.View;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Salary
{
    public partial class FrmQCPenaltyReport : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();
        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();
        DataTable DTabSummary = new DataTable();
        DataTable DTabDetail = new DataTable();

        string StrFromDate = "";
        string StrToDate = "";
        string mStrReportTitle = "";

        public FrmQCPenaltyReport()
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
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            ChkCmbProcess.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_4PLABOURPROCESS);
            ChkCmbProcess.Properties.DisplayMember = "PROCESSNAME";
            ChkCmbProcess.Properties.ValueMember = "PROCESS_ID";

            //ChkCmbDept.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);
            //ChkCmbDept.Properties.DisplayMember = "DEPARTMENTNAME";
            //ChkCmbDept.Properties.ValueMember = "DEPARTMENT_ID";
            
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
                //if (Val.Trim(ChkCmbProcess.Properties.GetCheckedItems()).ToString().Equals(string.Empty))
                //{
                //    Global.Message("Please Select Process..");
                //    ChkCmbProcess.Focus();
                //    return;
                //}

                StrFromDate = "";
                StrToDate = "";

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Text);
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Text);
                }

                if (!backgroundWorker1.IsBusy)
                {
                    PanelProgress.Visible = true;
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataSet DS = Obj.GetQCLabourWiseData(Val.Trim(CmbKapan.Properties.GetCheckedItems()),
                                Val.Trim(ChkCmbProcess.Properties.GetCheckedItems()),                                
                                StrFromDate, StrToDate);
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        DTabSummary = DS.Tables[0];
                    }
                    //if (DS.Tables[1].Rows.Count > 0)
                    //{
                    //    DTabDetail = DS.Tables[1];

                    //}
                }
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
                MainGrid.DataSource = DTabSummary;
                GrdDet.RefreshData();
                GrdDet.BestFitColumns();

                
             
                PanelProgress.Visible = false;
                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                BtnRefresh.Enabled = true;
                PanelProgress.Visible = false;
                Global.Message(Ex.Message.ToString());
            }
        }

        private void lblLabourExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "QC Labour Report(Employee Wise)";
            CommonExcelExportFuction(MainGrid, GrdDet, "LabourReport");
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
                        PaperKind = System.Drawing.Printing.PaperKind.A4,
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
            StrFilter = StrFilter + ", Process : Polish OK";
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

        private void lblLabourPrint_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "QC Labour Report(Employee Wise)";
            CommonPrintFuction(MainGrid, GrdDet);
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

        private void lblDetailExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "QC Labour Report Detail (" + GrpDetail.Text + ")";
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "ProcessWiseLabourReportDetail.xlsx";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGridDetail,
                        Landscape = false,
                        PaperKind = System.Drawing.Printing.PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [ProcessWiseLabourReportDetail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void lblDetailPrint_Click(object sender, EventArgs e)
        {
            try
            {
                mStrReportTitle = "QC Labour Report Detail (" + GrpDetail.Text + ")";
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

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Clicks == 2)
            {
                DataRow DR = GrdDet.GetDataRow(e.RowHandle);

                DTabDetail = Obj.GetQCWiseData(Val.Trim(CmbKapan.Properties.GetCheckedItems()),
                                Val.Trim(ChkCmbProcess.Properties.GetCheckedItems()),
                                StrFromDate, StrToDate);
                MainGridDetail.DataSource = DTabDetail;
                GrdDetDetail.RefreshData();
                GrdDetDetail.BestFitColumns();


            } 
            
        }

        private void MainGrid_Click(object sender, EventArgs e)
        {

        }

    }
}
