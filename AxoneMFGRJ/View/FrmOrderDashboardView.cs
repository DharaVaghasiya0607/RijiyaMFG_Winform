using BusLib.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using System.Drawing.Printing;
using DevExpress.XtraEditors.Repository;


namespace AxoneMFGRJ.View
{
    public partial class FrmOrderDashboardView : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();
        DataTable DTabSummary = new DataTable();
        BOTRN_OrderMasterDetail ObjRough = new BOTRN_OrderMasterDetail();

        int mIntJob = 0;
        int mIntReset = 0;

        string mStrFromOrderDate = null;
        string mStrToOrderDate = null;
        string mStrFromDueDate = null;
        string mStrToDueDate = null;
        string mStrPartyID = "";
        string mStrStatus = "";
        string mStrOperation = "";

        public FrmOrderDashboardView()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ChkCmbParty.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
            ChkCmbParty.Properties.DisplayMember = "EMPLOYEENAME";
            ChkCmbParty.Properties.ValueMember = "EMPLOYEE_ID";

            ChkCmbStatus.SetEditValue("PENDING,HALFREADY,COMPLETE");
            this.Show();
        }
        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(Obj);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }


        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            GrdDetPacketDetail.BestFitColumns();
            this.Cursor = Cursors.Default;
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

        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //try
            //{
            //    string StrKapanname = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "Kapan"));

            //    if (StrKapanname.ToUpper().Contains("TOTAL") || Val.ToString(e.Column.FieldName).ToUpper().Contains("TOTAL"))
            //    {
            //        e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
            //        e.Appearance.ForeColor = Color.DarkBlue;
            //        e.Appearance.BackColor = Color.LightGray;
            //    }

            //    if (e.Column.FieldName == "Kapan")
            //    {
            //        e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
            //        e.Appearance.BackColor = Color.LightGray;
            //        e.Appearance.BackColor2 = Color.LightGray;
            //    }

            //    //if (e.Column.FieldName.ToUpper().Contains("PLANNING"))
            //    //{
            //    //    e.Appearance.BackColor = Color.FromArgb(192, 255, 255);
            //    //}
            //    //if (e.Column.FieldName.ToUpper().Contains("CHECKING"))
            //    //{
            //    //    e.Appearance.BackColor = Color.FromArgb(255, 230, 255);
            //    //}

            //    //if (e.Column.FieldName.ToUpper().Contains("PLANNING TOTAL"))
            //    //{
            //    //    e.Appearance.BackColor = Color.FromArgb(192, 255, 255);
            //    //    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
            //    //}
            //    //if (e.Column.FieldName.ToUpper().Contains("CHECKING TOTAL"))
            //    //{
            //    //    e.Appearance.BackColor = Color.FromArgb(255, 230, 255);
            //    //    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
            //    //}
            //    //if (e.Column.FieldName == "TOTAL PHYSICAL")
            //    //{
            //    //    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
            //    //    e.Appearance.ForeColor = Color.DarkBlue;
            //    //    e.Appearance.BackColor = Color.LightGray;
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message.ToString());
            //}
        }

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
               
                if (e.Clicks == 2)
                {
                    DataRow Dr = GrdDet.GetFocusedDataRow();
                    DataTable DTabDetail = new DataTable();
                    
                    this.Cursor = Cursors.WaitCursor;
                    string StrParaTitle ="Order No : " +  Val.ToString(Dr["ORDERDETAILNO"]); ;

                   
                    if (e.Column.FieldName == "PLANPCS")
                    {
                        StrParaTitle = StrParaTitle + "    [   PLANNING PCS   ]";
                        DTabDetail = ObjRough.GetDashboardDetail("PLAN", Val.ToString(Dr["ORDERDETAIL_ID"]));
                    }
                    else if (e.Column.FieldName == "MAKPCS")
                    {
                        StrParaTitle = StrParaTitle + "    [   MAKABLE PCS   ]";
                        DTabDetail = ObjRough.GetDashboardDetail("MAK", Val.ToString(Dr["ORDERDETAIL_ID"]));
                    }
                    else if (e.Column.FieldName == "ARTISTPCS")
                    {
                        StrParaTitle = StrParaTitle + "    [   ARTIST PCS   ]";
                        DTabDetail = ObjRough.GetDashboardDetail("ARTIST", Val.ToString(Dr["ORDERDETAIL_ID"]));
                    }
                    else if (e.Column.FieldName == "POLISHPCS")
                    {
                        StrParaTitle = StrParaTitle + "    [   POLISH PCS   ]";
                        DTabDetail = ObjRough.GetDashboardDetail("POLISH", Val.ToString(Dr["ORDERDETAIL_ID"]));
                    }
                    else if (e.Column.FieldName == "BOOKEDPCS")
                    {
                        StrParaTitle = StrParaTitle + "    [   TOTAL PCS   ]";
                        DTabDetail = ObjRough.GetDashboardDetail("BOOKED", Val.ToString(Dr["ORDERDETAIL_ID"]));
                    }
                    
                    GrdDetPacketDetail.BeginUpdate();
                    GrpPacketSearch.Text = StrParaTitle;
                    MainGridPacketDetail.DataSource = DTabDetail;
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

        private void lblExportSummary_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "KapanWiseRollingReport.xlsx";
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

                    if (Global.Confirm("Do You Want To Open [KapanWiseRollingReport.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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
        public void Link_CreateMarginalHeaderAreaSummary(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("KAPANWISE ROLLING SUMMARY", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 250, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        private void lblPktExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "PacketWiseStock";
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
            TextBrick BrickTitleseller = e.Graph.DrawString(GrpPacketSearch.Text, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("Kapan Rolling AsOnDate :- " + DateTime.Now.ToString("dd/MM/yyyy"), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        private void lblPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {

                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                //string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

                link.Component = MainGrd;
                link.Landscape = true;

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

        private void lblPktPrint_Click(object sender, EventArgs e)
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (DTPFromOrderDate.Checked == true && DTPToOrderDate.Checked == true)
                {
                    mStrFromOrderDate = Val.SqlDate(DTPFromOrderDate.Value.ToShortDateString());
                    mStrToOrderDate = Val.SqlDate(DTPToOrderDate.Value.ToShortDateString());
                }
                if (DTPFromOrderDate.Checked == true && DTPToOrderDate.Checked == true)
                {
                    mStrFromDueDate = Val.SqlDate(DTPFromDueDate.Value.ToShortDateString());
                    mStrToDueDate = Val.SqlDate(DTPToDueDate.Value.ToShortDateString());
                }
                mStrPartyID = Val.Trim(ChkCmbParty.Properties.GetCheckedItems().ToString());
                mStrStatus = Val.Trim(ChkCmbStatus.Properties.GetCheckedItems().ToString());
                mStrOperation = "Search";

                progressPanel1.Visible = true;
                BtnSearch.Enabled = false;
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
             
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnResetAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure You Want To Reset All Data ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                
                mStrOperation = "Reset";

                progressPanel1.Visible = true;
                BtnResetAll.Enabled = false;
       
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnCallJob_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure You Want To Update Order Data ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                mStrOperation = "Update";

                progressPanel1.Visible = true;
                BtnCallJob.Enabled = false;
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
           
        }

        private void GrdDet_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string MergeOnStr = "ORDERDATE,MANUALORDERNO,PARTYNAME,ORDERDUEDATE";

            if (MergeOnStr.Contains(e.Column.FieldName))
            {
                string val1 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle1, GrdDet.Columns["MANUALORDERNO"]));
                string val2 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle2, GrdDet.Columns["MANUALORDERNO"]));
                if (val1 == val2)
                    e.Merge = true;
                e.Handled = true;
            }
        }

       

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (mStrOperation == "Search")
            {
                 DTabSummary = ObjRough.GetDashboardSummary(mStrFromOrderDate, mStrToOrderDate, mStrFromDueDate, mStrToDueDate, mStrPartyID, mStrStatus);
            }
            else if (mStrOperation == "Reset")
            {
                mIntReset = ObjRough.RestAllFlag();
            }
            else if (mStrOperation == "Update")
            {
                mIntJob = ObjRough.UpdateAllOrders();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Visible = false;

            if (mStrOperation == "Search")
            {
                BtnSearch.Enabled = true;
                GrdDet.BeginUpdate();
                MainGrd.DataSource = DTabSummary;
                GrdDet.RefreshData();
                GrdDet.EndUpdate();
            }
            else if (mStrOperation == "Update")
            {
                BtnCallJob.Enabled = true;
                Global.Message("SUCCESSFULLY UPDATE ALL THE ORDER DATA");
            }
            else if (mStrOperation == "Reset")
            {
                BtnResetAll.Enabled = true;
                Global.Message("SUCCESSFULLY RESET ALL THE ORDER DATA");
            }
           
        }

        private void BtnExpandMainGrid_Click(object sender, EventArgs e)
        {
            if (BtnExpandMainGrid.Text == "+")
            {
                BtnExpandMainGrid.Text = "-";
                splitContainer1.Panel2Collapsed = true;
            }
            else
            {
                BtnExpandMainGrid.Text = "+";
                splitContainer1.Panel2Collapsed = false;
            
            }
        }

        private void BtnExpandDetailGrid_Click(object sender, EventArgs e)
        {
            if (BtnExpandDetailGrid.Text == "+")
            {
                BtnExpandDetailGrid.Text = "-";
                splitContainer1.Panel1Collapsed = true;
            }
            else
            {
                BtnExpandDetailGrid.Text = "+";
                splitContainer1.Panel1Collapsed = false;

            }
        }


    }
}
