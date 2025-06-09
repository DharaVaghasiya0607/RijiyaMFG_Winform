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
    public partial class FrmPolishOKReport : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();
        
        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabDetail = new DataTable();

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        
        string mStrReportTitle = "";
        int mIntProcessID = 531;
        string StrFromDate = "", StrToDate = "";
        
        // #D: 14-09-2021
        Int32 pIntIssuePcs = 0;
        double pDouIssueCts = 0;

        Int32 pIntRdyPcs = 0;
        double pDouRdyCts = 0;

        double pDouKapanCts = 0;
        double pDouExpCts = 0;

        double pDouPrdCts = 0;

        // #D: 14-09-2021
       
        public FrmPolishOKReport()
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

                GrdDet.FocusedRowHandle = 1;
                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                else
                {
                    StrFromDate = null;
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }
                else
                {
                    StrFromDate = null;
                }

                DTabDetail.Rows.Clear();
                DataTable DTab = Obj.GetPolishOKReport("SUMMARY", Val.Trim(CmbKapan.Properties.GetCheckedItems()), mIntProcessID, Val.ToInt64(txtEmployee.Tag), StrFromDate, StrToDate);

                GrdDet.BeginUpdate();
                MainGrid.DataSource = DTab;
                GrdDet.RefreshData();
                GrdDet.BestFitColumns();
                GrdDet.EndUpdate();
                if (DTab.Rows.Count > 0)
                {
                    DataRow DR = DTab.Rows[0];
                    lblTotalRoughCarat.Text = Val.ToString(DR["TOTALROUGHCARAT"]);
                }
                if (GrdDet.FocusedRowHandle == 0)
                {
                    GrdDet_FocusedRowChanged(null, null);     
                }


                DataTable DTabPending = Obj.GetPolishOKReport("PENDING", Val.Trim(CmbKapan.Properties.GetCheckedItems()), mIntProcessID, Val.ToInt64(txtEmployee.Tag), StrFromDate, StrToDate);

               //// DTabDetail.Rows.Clear();
               // DataTable DTabRej = Obj.GetPolishOKReport("REJECTIONDETAIL", Val.Trim(CmbKapan.Properties.GetCheckedItems()), mIntProcessID, Val.ToInt64(txtEmployee.Tag), StrFromDate, StrToDate);

                GrdDetRejection.BeginUpdate();
                MainGrdRejection.DataSource = DTabPending;
                GrdDetRejection.RefreshData();
                GrdDetRejection.BestFitColumns();
                GrdDetRejection.EndUpdate();

               // DataTable DTabTotal = Obj.GetPolishOKReport("TOTAL", Val.Trim(CmbKapan.Properties.GetCheckedItems()), mIntProcessID, Val.ToInt64(txtEmployee.Tag), StrFromDate, StrToDate);
               // if (DTabTotal.Rows.Count > 0)
               // {
               //     DataRow DR = DTabTotal.Rows[0];
               //     lblTotalCts.Text = Val.ToString(DR["TOTALCTS"]);
               //     lblTotalPcs.Text = Val.ToString(DR["TOTALPCS"]);

               // }

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
            TextBrick BrickTitleseller = e.Graph.DrawString(mStrReportTitle, System.Drawing.Color.Navy, new RectangleF(0, 20, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            string StrFilter = "FromDate : " + DTPFromDate.Text + " To " + DTPToDate.Text;
            StrFilter = StrFilter + ", Process : Polish OK";
            //// ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString(StrFilter, System.Drawing.Color.Navy, new RectangleF(0, 50, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;

            string StrKapan = Val.ToString(Val.Trim(CmbKapan.Properties.GetCheckedItems()));
            StrKapan = "Kapan : " + StrKapan + "," + "  " + "RoughCts :" + lblTotalRoughCarat.Text;
            //// ' For Filter 
            TextBrick BrickTitlesKapan = e.Graph.DrawString(StrKapan, System.Drawing.Color.Maroon, new RectangleF(0, 50, e.Graph.ClientPageSize.Width, 50), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesKapan.Font = new Font("verdana", 8, FontStyle.Bold);
            //BrickTitlesKapan.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickTitlesKapan.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesKapan.ForeColor = Color.Black;

            string StrRough = Val.ToString(lblRoughnamevalue.Text);
            StrRough = "Rough : " + lblRoughnamevalue.Text;
            //// ' For Filter 
            TextBrick BrickTitlesRough = e.Graph.DrawString(StrRough, System.Drawing.Color.Maroon, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 50), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesRough.Font = new Font("verdana", 8, FontStyle.Bold);
            //BrickTitlesKapan.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickTitlesKapan.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesRough.ForeColor = Color.Black;

            string StrInv = Val.ToString(lblInv.Text);
            StrInv = "Invoice : " + lblInv.Text;
            //// ' For Filter 
            TextBrick BrickTitlesInv = e.Graph.DrawString(StrInv, System.Drawing.Color.Maroon, new RectangleF(0, 90, e.Graph.ClientPageSize.Width, 100), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesInv.Font = new Font("verdana", 8, FontStyle.Bold);
            //BrickTitlesKapan.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickTitlesKapan.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesInv.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 10, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, -150), DevExpress.XtraPrinting.BorderSide.None);
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
            mStrReportTitle = "PolishOK Report (Employee Wise)";
            CommonPrintFuction(MainGrid, GrdDet);

        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "PolishOK Report (Employee Wise)";
            CommonExcelExportFuction(MainGrid, GrdDet, "PolishOkReport");
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

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 170;

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

                if (e.FocusedRowHandle < 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                DataRow DRow = GrdDet.GetDataRow(e.FocusedRowHandle);

                GrpDetail.Text = Val.ToString(DRow["WORKERCODE"]) + " - " + Val.ToString(DRow["WORKERNAME"]);

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                else
                {
                    StrFromDate = null;
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }
                else
                {
                    StrFromDate = null;
                }
                
                DTabDetail = Obj.GetPolishOKReport("DETAIL", Val.Trim(CmbKapan.Properties.GetCheckedItems()), mIntProcessID, Val.ToInt64(DRow["WORKER_ID"]), StrFromDate, StrToDate);

                MainGridDetail.DataSource = DTabDetail;
                GrdDetDetail.RefreshData();
                GrdDetDetail.BestFitColumns();
                DataRow DR = GrdDet.GetFocusedDataRow();

                if (DTabDetail.Rows.Count > 0)
                {
                    DataRow DRow1 = DTabDetail.Rows[0];
                    lblRoughnamevalue.Text = Val.ToString(DRow1["ROUGHNAME"]);
                    lblInv.Text = Val.ToString(DRow1["INVOICE"]);
                }
				txtEmpCode.Text = Val.ToString(DR["WORKERCODE"]);

				byte[] OFFICELOGO = GrdDet.GetRowCellValue(e.FocusedRowHandle, "IMG") as byte[] ?? null;
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
            mStrReportTitle = "PolishOKReport Detail (" + GrpDetail.Text + ")";
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
                mStrReportTitle = "PolishOk Report Detail (" + GrpDetail.Text + ")";
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

        private void GrdDetDetail_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //try
            //{
            //    GrdDetDetail.Columns["WORKERCODE"].ClearFilter();
            //    GrdDetDetail.Columns["WORKERCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("WORKERCODE='" + Val.ToString(GrdDet.GetFocusedRowCellValue("WORKERCODE")) + "'");
            //}
            //catch (Exception Ex)
            //{
            //    Global.Message(Ex.Message.ToString());
            //}
        }

        private void LblRejectionExcel_Click(object sender, EventArgs e)
        {
            mStrReportTitle = "PolishOKReport Rejection Detail (" + GrpDetail.Text + ")";
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "PolishOkRejectionDetail.xlsx";
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

                    if (Global.Confirm("Do You Want To Open [PolishOkRejectionDetail.xls] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void LblRejectionPrint_Click(object sender, EventArgs e)
        {
            try
            {
                mStrReportTitle = "PolishOk Rejection Detail (" + GrpDetail.Text + ")";
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGrdRejection;
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

        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    pIntIssuePcs = 0;
                    pDouIssueCts = 0;

                    pIntRdyPcs = 0;
                    pDouRdyCts = 0;

                    pDouKapanCts = 0;
                    pDouExpCts = 0;

                    pDouPrdCts = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    pIntIssuePcs = pIntIssuePcs + Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "ISSUEPCS"));
                    pDouIssueCts = pDouIssueCts + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "ISSUECARAT"));

                    pIntRdyPcs = pIntRdyPcs + Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "READYPCS"));
                    pDouRdyCts = pDouRdyCts + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "READYCARAT"));

                    pDouExpCts = pDouExpCts + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "EXPCARAT"));

                    pDouKapanCts = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "KAPANCARAT"));

                    pDouPrdCts = pDouExpCts + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "PRDCTS"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("ISSUESIZE") == 0)
                    {
                        if (Val.Val(pDouIssueCts) > 0)
                            e.TotalValue = Math.Round((Val.ToInt32(pIntIssuePcs) / Val.Val(pDouIssueCts)), 3);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RETURNSIZE") == 0)
                    {
                        if (Val.Val(pDouIssueCts) > 0)
                            e.TotalValue = Math.Round((Val.ToInt32(pIntRdyPcs) / Val.Val(pDouRdyCts)), 3);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("ISSUEPER") == 0)
                    {
                        if (Val.Val(pDouKapanCts) > 0)
                            e.TotalValue = Math.Round((Val.Val(pDouIssueCts) / Val.Val(pDouKapanCts)*100), 3);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("READYPER") == 0)
                    {
                        if (Val.Val(pDouIssueCts) > 0)
                            e.TotalValue = Math.Round((Val.Val(pDouRdyCts) / Val.Val(pDouIssueCts) * 100), 3);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("EXPPER") == 0)
                    {
                        if (Val.Val(pDouIssueCts) > 0)
                            e.TotalValue = Math.Round((Val.Val(pDouExpCts) / Val.Val(pDouIssueCts) * 100), 3);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PRDPER") == 0)
                    {
                        if (Val.Val(pDouIssueCts) > 0)
                            e.TotalValue = Math.Round((Val.Val(pDouPrdCts) / Val.Val(pDouIssueCts) * 100), 3);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

    }
}
