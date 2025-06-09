using AxoneMFGRJ.Report;
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
    public partial class FrmGradingReportView : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabDetail = new DataTable();

        string mStrFromDate = string.Empty;
        string mStrToDate = string.Empty;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        EmployeeActionRightsProperty Property = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

        string mStrReportTitle = "";

        public FrmGradingReportView()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            //MainGridDetail.DefaultCellStyle.SelectionBackColor = Color.Orange

            txtEmployee.Tag = BOConfiguration.gEmployeeProperty.LEDGER_ID;
            txtEmployee.Text = BOConfiguration.gEmployeeProperty.LEDGERCODE;

            if (BOConfiguration.gEmployeeProperty.DEPARTMENT_ID == 436)
            {
                txtEmployee.Enabled = true;
            }
            else
            {
                txtEmployee.Enabled = false;
            }

            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            DTPToDate.Value = System.DateTime.Now;

            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);
            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDet.RestoreLayoutFromStream(stream);
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

                mStrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                mStrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                DTabDetail.Rows.Clear();
                BtnRefresh.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

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

                link.Component = MainGrid;
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

        }

        public string GetStringForToolTip(string pStrExcRate, string pStrDollar, string pStrDollarPer, string pStrRupees, string pStrType, string pFinalProcessAmt)
        {

            //string StrPlusMinusSign = Val.ToString(pStrType).Contains("DOLLARPLUS") ? "Dollar (+)" : Val.ToString(pStrType).Contains("DOLLARPLUS") ? "Dollar (+)" : "";

            //double LessMinusDollarAmt = Math.Round((Val.Val(pFinalProcessAmt) * 1 / 100), 2);

            //double DouDollarPerAmt = Math.Round(((Val.Val(LessMinusDollarAmt) + Val.Val(pStrDollar)) * Val.Val(pStrDollarPer)) / 100, 2);

            //if (Val.ToString(pStrType).Contains("DOLLARPLUSRUPEES"))
            //    StrPlusMinusSign = " Dollar (+) ";
            //else if (Val.ToString(pStrType).Contains("DOLLARMINUSRUPEES") || Val.ToString(pStrType).Contains("MINUSDOLLAR"))
            //    StrPlusMinusSign = " Dollar (-) ";
            //else if (Val.ToString(pStrType).Contains("DFPLUSRUPEES"))
            //    StrPlusMinusSign = "  DF (+)  ";
            //else if (Val.ToString(pStrType).Contains("DFMINUSRUPEES"))
            //    StrPlusMinusSign = "  DF (-)  ";
            //else if (Val.ToString(pStrType).Contains("PLANVARIATIONRUPEES"))
            //    StrPlusMinusSign = "Plan Varitn";
            //else if (Val.ToString(pStrType).Contains("BREAKING"))
            //    StrPlusMinusSign = " Breaking  ";

            ////string StrToolTips = "\n  " + StrPlusMinusSign + "   :   " + Val.ToString(pStrDollar) + "           ($)      \n\n";
            string StrToolTips = "";

            //if (Val.ToString(pStrType) == "MINUSDOLLAR")
            //{
            //    StrToolTips = "\n  Final Amt   :   " + Val.ToString(pFinalProcessAmt) + "           ($)      \n\n";
            //    StrToolTips = StrToolTips + "  Less Per     :    1            (%)      \n";
            //    StrToolTips = StrToolTips + "  ----------------------------------\n";
            //    StrToolTips = StrToolTips + "       Amt       :   " + Val.ToString(LessMinusDollarAmt) + "           ($)      \n\n";
            //    StrToolTips = StrToolTips + "                    :   " + Val.ToString(pStrDollar) + "           ($)      \n";
            //    StrToolTips = StrToolTips + "  ----------------------------------\n";
            //    StrToolTips = StrToolTips + "" + StrPlusMinusSign + "     :   " + Val.ToString(Val.Val(LessMinusDollarAmt) + Val.Val(pStrDollar)) + "           ($)      \n\n";
            //    return StrToolTips;
            //}

            //if (Val.Val(pFinalProcessAmt) != 0)
            //{
            //    StrToolTips = "\n  Final Amt   :   " + Val.ToString(pFinalProcessAmt) + "           ($)      \n\n";
            //    StrToolTips = StrToolTips + "  Less Per     :    1            (%)      \n";
            //    StrToolTips = StrToolTips + "  ----------------------------------\n";
            //    StrToolTips = StrToolTips + "" + StrPlusMinusSign + "   :   " + Val.ToString(LessMinusDollarAmt) + "           ($)      \n\n";
            //    StrToolTips = StrToolTips + "                    :   " + Val.ToString(pStrDollar) + "           ($)      \n";
            //    StrToolTips = StrToolTips + "  ----------------------------------\n";
            //    StrToolTips = StrToolTips + "                    :  " + Val.ToString(Val.Val(LessMinusDollarAmt) + Val.Val(pStrDollar)) + "           ($)      \n\n";
            //}
            //else
            //{
            //    StrToolTips = "\n  " + StrPlusMinusSign + "   :   " + Val.ToString(pStrDollar) + "           ($)      \n\n";
            //}
            //StrToolTips = StrToolTips + "  Dollar Per   :   " + Val.ToString(pStrDollarPer) + "            (%)      \n";
            //StrToolTips = StrToolTips + "  ----------------------------------\n";
            //StrToolTips = StrToolTips + "     Amt          :   " + Val.ToString(DouDollarPerAmt) + "         ($)      \n\n";
            //StrToolTips = StrToolTips + "    ExcRate     :   " + Val.ToString(pStrExcRate) + "              (@)      \n";
            //StrToolTips = StrToolTips + "  ----------------------------------\n";
            //StrToolTips = StrToolTips + "    Rupees.     :   " + Val.ToString(pStrRupees) + "        (₹)      \n\n";
            return StrToolTips;
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DTabDetail = Obj.GetGradingReportGetData(mStrFromDate, mStrToDate, Val.ToInt64(txtEmployee.Tag));

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

                if (DTabDetail.Rows.Count <= 0)
                {
                    Global.Message("No Data Found..");
                }

                GrdDet.BeginUpdate();

                MainGrid.DataSource = DTabDetail;
                GrdDet.RefreshData();

                MainGrid.DataSource = DTabDetail;
                GrdDet.RefreshData();

                GrdDet.EndUpdate();

            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnRefresh.Enabled = true;
                Global.Message(ex.Message.ToString());
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

        private void lblSaveLayoutDetail_Click(object sender, EventArgs e)
        {
            Stream strYM = new System.IO.MemoryStream();
            GrdDet.SaveLayoutToStream(strYM);
            strYM.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader readerYM = new StreamReader(strYM);
            string textYM = readerYM.ReadToEnd();
            int IntResYM = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDet.Name, textYM);

            if (IntResYM != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblDeleteLayoutDetail_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDet.Name);

            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "Prediction";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrid,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 200, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [Prediction.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            string StrBrekingType = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "BREKINGTYPE"));
            string StrPacket = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PRD_KAPANNAME"));
            int StrGrdTodate = Val.ToInt32(GrdDet.GetRowCellValue(e.RowHandle, "GRD_TODATE"));

            if (StrBrekingType == "LS")
            {
                e.Appearance.BackColor = lblls.BackColor;
            }
            else if (StrBrekingType == "BLK" || StrBrekingType == "CONIC")
            {
                e.Appearance.BackColor = lblBlkConic.BackColor;
            }
            else if (StrBrekingType == "MFG" || StrBrekingType == "MFGMISTAKE")
            {
                e.Appearance.BackColor = lblMFG.BackColor;
            }

            if (StrGrdTodate == 1)
            {
                e.Appearance.BackColor = lblLatest.BackColor;
            }

        }

        private void MainGrid_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            }
        }

    }
}
