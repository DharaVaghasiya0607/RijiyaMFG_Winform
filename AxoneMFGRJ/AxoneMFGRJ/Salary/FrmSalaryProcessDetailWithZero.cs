﻿using AxoneMFGRJ.Report;
using AxoneMFGRJ.Utility;
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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections;
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
    public partial class FrmSalaryProcessDetailWithZero : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabProcess = new DataTable();
        DataTable DTabDetail = new DataTable();
        DataTable DtabSelectedDetail = new DataTable();

        BODevGridSelection ObjGridSelection;

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

        string StrRes = "";

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        string mStrReportTitle = "";

        public FrmSalaryProcessDetailWithZero()
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


            CmbKapan.Properties.DataSource = ObjPacket.FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";


            if (MainGridDetail.RepositoryItems.Count == 0)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDetail;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdDetail.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            //GrdDetail.Columns["COLSELECTCHECKBOX"].OptionsColumn.AllowMerge = DefaultBoolean.False;

            GrdDetail.Bands["BANDGENERAL"].Fixed = FixedStyle.None;
            GridBand band = GrdDetail.Bands.AddBand("..");
            band.Columns.Add(GrdDetail.Columns["COLSELECTCHECKBOX"]);
            band.Fixed = FixedStyle.Left;
            band.VisibleIndex = 0;

            GrdDetail.Bands["BANDGENERAL"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }

            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
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

                ObjGridSelection.ClearSelection();
                if (txtEmployee.Text.Trim().Length == 0)
                {
                    txtEmployee.Tag = "";
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                BtnRefresh.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker2.IsBusy)
                {
                    backgroundWorker2.RunWorkerAsync();
                }


                /*
                selection.ClearSelection();
                if (txtEmployee.Text.Trim().Length == 0)
                {
                    txtEmployee.Tag = "";
                }

                this.Cursor = Cursors.WaitCursor;

                string StrSalaryType = "";
                if (RbtMarker.Checked)
                {
                    StrSalaryType = "MARKER";
                }
                else if (RbtWorker.Checked)
                {
                    StrSalaryType = "WORKER";
                }

                DtabSelectedDetail.Rows.Clear();
                DTabDetail.Rows.Clear();
                DTabDetail = Obj.GetSalaryViewDetailWithZeroAmt(Val.Trim(CmbKapan.Properties.GetCheckedItems()), Val.ToInt32(txtFromPacketNo.Text), Val.ToInt32(txtToPacketNo.Text), Val.ToInt64(txtEmployee.Tag), Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), StrSalaryType);

                if (DTabDetail.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    Global.Message("No Data Found..!");
                    return;
                }

                GrdDetail.BeginUpdate();
                MainGridDetail.DataSource = DTabDetail;
                GrdDetail.RefreshData();
                GrdDetail.BestFitColumns();
                GrdDetail.Columns["WORKERCODE"].ClearFilter();


                GrdDetail.EndUpdate();
                this.Cursor = Cursors.Default;
                */

            }
            catch (Exception EX)
            {
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
                mStrReportTitle = "Salary Detail With Process Amt 0(" + Str + ")";
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
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "BEFOREPER_DFMINUSDOLLAR")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrConfirmEmpCode = Val.ToString(view.GetRowCellValue(hi.RowHandle, "CONFIRMEMPLOYEECODE"));
                        string StrConfirmEmpAmount = Val.ToString(view.GetRowCellValue(hi.RowHandle, "CONFIRMEMPLOYEEAMOUNT"));
                        string StrConfirmGrdAmount = Val.ToString(view.GetRowCellValue(hi.RowHandle, "CONFIRMGRDAMOUNT"));
                        string StrNotConfGrdAmount = Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOTALGRDAMOUNT"));
                        string StrBeforePer_DFMinusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "BEFOREPER_DFMINUSDOLLAR"));
                        string StrToolTips = "";

                        if (Val.Val(StrBeforePer_DFMinusDollar) != 0)
                        {
                            StrToolTips = "\n Processed Grd Amount      :   " + Val.ToString(StrConfirmGrdAmount) + " ($)\n\n";
                            StrToolTips = StrToolTips + "\n Confirm Emp Amount(" + Val.ToString(StrConfirmEmpCode) + ")  :   " + Val.ToString(StrConfirmEmpAmount) + " ($)\n\n";
                            StrToolTips = StrToolTips + "\n Not Confirm Emp Amount  :   " + Val.ToString(StrNotConfGrdAmount) + " ($)\n";
                            StrToolTips = StrToolTips + "---------------------------------------------\n";
                            StrToolTips = StrToolTips + "        DF Minus Dollar          :   " + Val.ToString(StrBeforePer_DFMinusDollar) + " ($)\n";
                        }
                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, StrToolTips);
                        return;
                    }
                    if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "DFMINUSRUPEES")
                    {
                        string StrExcRate = Val.ToString(view.GetRowCellValue(hi.RowHandle, "EXCRATE"));
                        string StrConfirmEmpCode = Val.ToString(view.GetRowCellValue(hi.RowHandle, "CONFIRMEMPLOYEECODE"));
                        string StrConfirmEmpAmount = Val.ToString(view.GetRowCellValue(hi.RowHandle, "CONFIRMEMPLOYEEAMOUNT"));
                        string StrConfirmGrdAmount = Val.ToString(view.GetRowCellValue(hi.RowHandle, "CONFIRMGRDAMOUNT"));
                        string StrNotConfGrdAmount = Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOTALGRDAMOUNT"));
                        string StrBeforePer_DFMinusDollar = Val.ToString(view.GetRowCellValue(hi.RowHandle, "BEFOREPER_DFMINUSDOLLAR"));
                        string StrDFMinusRupees = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFMINUSRUPEES"));
                        string StrDFMinusPer = Val.ToString(view.GetRowCellValue(hi.RowHandle, "DFMINUSPER"));
                        string StrToolTips = "";
                        string StrAfterDeductPer = "";
                        if (Val.Val(StrBeforePer_DFMinusDollar) != 0)
                        {
                            StrAfterDeductPer = Val.ToString(Math.Round(((Val.Val(StrBeforePer_DFMinusDollar) * Val.Val(StrDFMinusPer.Replace("%", ""))) / 100), 2));

                            StrToolTips = "\n Processed Grd Amount      :   " + Val.ToString(StrConfirmGrdAmount) + " ($)\n\n";
                            StrToolTips = StrToolTips + "\n Confirm Emp Amount(" + Val.ToString(StrConfirmEmpCode) + ")  :   " + Val.ToString(StrConfirmEmpAmount) + " ($)\n\n";
                            StrToolTips = StrToolTips + "\n Not Confirm Emp Amount  :   " + Val.ToString(StrNotConfGrdAmount) + " ($)\n";
                            StrToolTips = StrToolTips + "---------------------------------------------\n";
                            StrToolTips = StrToolTips + "        DF Minus Dollar($)     :   " + Val.ToString(StrBeforePer_DFMinusDollar) + " ($)\n\n";
                            StrToolTips = StrToolTips + "        DF Minus Per(%)        :   " + Val.ToString(StrDFMinusPer) + " ($)\n";
                            StrToolTips = StrToolTips + "---------------------------------------------\n";
                            StrToolTips = StrToolTips + "        After Deduct Per        :   " + StrAfterDeductPer + " ($)\n\n";
                            StrToolTips = StrToolTips + "        ExcRate(@)                :   " + Val.ToString(StrExcRate) + "  \n";
                            StrToolTips = StrToolTips + "---------------------------------------------\n";
                            StrToolTips = StrToolTips + "      DF Minus Rupees(₹)     :   " + Val.ToString(StrDFMinusRupees) + " (₹)\n";
                        }
                        info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, StrToolTips);
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
                }

                finally
                {
                    e.Info = info;
                }
            }
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

            //if (Val.ToString(pStrType) == "BEFOREPER_DFMINUSDOLLAR")
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


        private void MainGridDetail_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == info.BandsInfo.BandCount - 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));


                //GridCellInfo cellInfo = info.GetGridCellInfo(0, gridView.Columns["KAPANPACKET"]);
                //GridCellInfo cellInfo1 = info.GetGridCellInfo(1, gridView.Columns["KAPANPACKET"]);
                //if (cellInfo != cellInfo1)
                //{
                //    Point p1 = new Point(info.ColumnsInfo[i].Bounds.Right - 1, info.ViewRects.Rows.Y);
                //    Point p2 = new Point(info.ColumnsInfo[i].Bounds.Right - 1, info.ViewRects.Rows.Bottom);

                //    Pen pen = new Pen(Color.Black);
                //    e.Graphics.DrawLine(pen, p1, p2);
                //}
            }




        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            //if (e.Column.FieldName == "SHAPENAME")
            //{
            //    string StrMakShape = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "ORGSHAPENAME"));
            //    string StrShape = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "SHAPENAME"));
            //    if (StrMakShape != "" && StrShape != "" && StrMakShape != StrShape)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //}

            //if (e.Column.FieldName == "COLORNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGCOLORSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "COLORSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}

            //if (e.Column.FieldName == "CLARITYNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGCLARITYSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "CLARITYSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "CUTNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGCUTSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "CUTSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "POLNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGPOLSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "POLSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "SYMNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGSYMSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "SYMSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "FLNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGFLSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "FLSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}

            //if (e.Column.FieldName == "CARAT")
            //{
            //    double DouMakCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "ORGCARAT"));
            //    double DouCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "CARAT"));
            //    if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //    else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "AMOUNT")
            //{
            //    double DouMakCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "ORGAMOUNT"));
            //    double DouCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "AMOUNT"));
            //    if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //    else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "PRICEPERCARAT")
            //{
            //    double DouMakCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "ORGPRICEPERCARAT"));
            //    double DouCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PRICEPERCARAT"));
            //    if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //    else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //}


            //// PROCESS GRADING PARAMETERS

            //if (e.Column.FieldName == "GRDSHAPENAME")
            //{
            //    string StrMakShape = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "ORGSHAPENAME"));
            //    string StrShape = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "GRDSHAPENAME"));
            //    if (StrMakShape != "" && StrShape != "" && StrMakShape != StrShape)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //}

            //if (e.Column.FieldName == "GRDCOLORNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGCOLORSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "GRDCOLORSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}

            //if (e.Column.FieldName == "GRDCLARITYNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGCLARITYSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "GRDCLARITYSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "GRDCUTNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGCUTSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "GRDCUTSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "GRDPOLNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGPOLSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "GRDPOLSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "GRDSYMNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGSYMSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "GRDSYMSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "GRDFLNAME")
            //{
            //    int IntMakSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "ORGFLSEQNO"));
            //    int IntSeqNo = Val.ToInt(GrdDetail.GetRowCellValue(e.RowHandle, "GRDFLSEQNO"));
            //    if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //    else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //}

            //if (e.Column.FieldName == "GRDCARAT")
            //{
            //    double DouMakCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "ORGCARAT"));
            //    double DouCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "GRDCARAT"));
            //    if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //    else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "GRDAMOUNT")
            //{
            //    double DouMakCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "ORGAMOUNT"));
            //    double DouCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "GRDAMOUNT"));
            //    if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //    else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //}
            //if (e.Column.FieldName == "GRDPRICEPERCARAT")
            //{
            //    double DouMakCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "ORGPRICEPERCARAT"));
            //    double DouCarat = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "GRDPRICEPERCARAT"));
            //    if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblDown.BackColor;
            //    }
            //    else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
            //    {
            //        e.Appearance.BackColor = lblUp.BackColor;
            //    }
            //}
        }

        private void lblPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Factory Production Summary (" + Str + ")";
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

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

        private void GrdDet_RowClick(object sender, RowClickEventArgs e)
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
                FrmFactoryLabourDetail.ShowForm(StrKapan, IntPacketNO, StrTag, StrPacketID, StrWorkerName, IntWorkerID, "MARKER");
                ObjFormEvent.ObjToDisposeList.Add(FrmFactoryLabourDetail);
            }
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
                mStrReportTitle = "Factory Production Labour Summary (" + Str + ")";
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


        private void lblDeleteLayoutSummary_Click(object sender, EventArgs e)
        {

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


        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName.Contains("PANULTYINCENTIVE"))
                {
                    DataRow DRow = GrdDetail.GetDataRow(e.RowHandle);
                    if (Val.ToString(DRow["WORKER_ID"]).Trim().Equals(string.Empty) || Val.Val(DRow["PANULTYINCENTIVE"]) == 0)
                    {
                        lblMessageForDetail.Text = "Message";
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;

                    TrnPenaltyIncentiveProperty Property = new TrnPenaltyIncentiveProperty();
                 //   Property.PENALTY_ID = Guid.NewGuid();
                    Property.PENALTYTYPE = Val.Val(DRow["PANULTYINCENTIVE"]) <= 0 ? "PENALTY" : "INCENTIVE";
                    Property.PENALTYDATE = Val.SqlDate(DTPFromDate.Text);
                    Property.EMPLOYEE_ID = Val.ToInt64(DRow["WORKER_ID"]);
                    Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"]);
                    Property.PACKETNO = Val.ToString(DRow["PACKETNO"]);
                    Property.PACKETTAG = Val.ToString(DRow["TAG"]);
                    Property.REASON = "";//Val.ToInt32(txtReason.Tag);
                    Property.AMOUNT = Math.Abs(Val.Val(DRow["PANULTYINCENTIVE"]));
                    Property.REMARK = Val.ToString("Update From Marker Labour Report(Detail)");
                    Property = new BOTRN_PenaltyIncentive().Save(Property);

                    this.Cursor = Cursors.Default;
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        lblMessageForDetail.Text = "Penalty/Incentive : " + Val.ToString(DRow["PANULTYINCENTIVE"]) + " Added In Kapan [ " + Val.ToString(DRow["KAPANNAME"]) + "/" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + " ] ";
                        BtnRefresh_Click(null, null);
                    }
                    else
                    {
                        lblMessageForDetail.Text = "Error......";
                    }
                    Property = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDetail_CellMerge(object sender, CellMergeEventArgs e)
        {
            string MergeOnStr = "DFTYPE,PLANVARIATIONDOLLAR,PLANVARIATIONRUPEES,PLANVARIATIONPER,DFPLUSDOLLAR,DFMINUSDOLLAR,DFPLUSPER,DFMINUSPER,DFPLUSRUPEES,DFMINUSRUPEES,COLSELECTCHECKBOX";
            MergeOnStr = MergeOnStr + ",PLUSDOLLAR,MINUSDOLLAR,DOLLARPLUSPER,DOLLARMINUSPER,DOLLARPLUSRUPEES,DOLLARMINUSRUPEES,TOTALGRDAMOUNT,BEFOREPER_PLUSDOLLAR,BEFOREPER_MINUSDOLLAR,BEFOREPER_DFPLUSDOLLAR,BEFOREPER_DFMINUSDOLLAR";
            MergeOnStr = MergeOnStr + ",TOTALORGAMOUNT,BEFOREPER_CHECKEROKMINUSDOLLAR,CHECKEROKMINUSPER,CHECKEROKMINUSDOLLAR,CHECKEROKMINUSRUPEES"; //FINAL_LABOURPER,FINAL_LABOURAMOUNTDOLLAR,FINAL_LABOURAMOUNTRUPEES";

            string MergeOn = "KAPANPACKET";

            if (MergeOnStr.Contains(e.Column.FieldName))
            {
                string val1 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle1, GrdDetail.Columns[MergeOn]));
                string val2 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle2, GrdDetail.Columns[MergeOn]));
                if (val1 == val2)
                    e.Merge = true;
                e.Handled = true;
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
                        DouTotalPlanVarDollar = DouTotalPlanVarDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PLANVARIATIONDOLLAR"));
                        DouTotalPlanVarRupees = DouTotalPlanVarRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PLANVARIATIONRUPEES"));

                        DouTotalDFPlusDollar = DouTotalDFPlusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFPLUSDOLLAR"));
                        DouTotalDFMinusDollar = DouTotalDFMinusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFMINUSDOLLAR"));
                        DouTotalDFPlusRupees = DouTotalDFPlusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFPLUSRUPEES"));
                        DouTotalDFMinusRupees = DouTotalDFMinusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DFMINUSRUPEES"));
                        DouTotalPcs = DouTotalPcs + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PCS"));   //#P : 21-01-2020


                        //#P : 06-02-2020
                        DouTotalFinalLabourDollar = DouTotalFinalLabourDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "FINAL_LABOURAMOUNTDOLLAR"));
                        DouTotalFinalLabourRupees = DouTotalFinalLabourRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "FINAL_LABOURAMOUNTRUPEES"));

                        DouTotalDPlusDollar_BeforePer = DouTotalDPlusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_PLUSDOLLAR"));
                        DouTotalDMinusDollar_BeforePer = DouTotalDMinusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_MINUSDOLLAR"));

                        DouTotalDFPlusDollar_BeforePer = DouTotalDFPlusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_DFPLUSDOLLAR"));
                        DouTotalDFMinusDollar_BeforePer = DouTotalDFMinusDollar_BeforePer + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "BEFOREPER_DFMINUSDOLLAR"));

                        DouTotalORGAmount = DouTotalORGAmount + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "TOTALORGAMOUNT"));
                        DouTotalGRDAmount = DouTotalGRDAmount + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "TOTALGRDAMOUNT"));
                        DouTotalDPlusDollar = DouTotalDPlusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PLUSDOLLAR"));
                        DouTotalDMinusDollar = DouTotalDMinusDollar + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "MINUSDOLLAR"));
                        DouTotalDPlusRupees = DouTotalDPlusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DOLLARPLUSRUPEES"));
                        DouTotalDMinusRupees = DouTotalDMinusRupees + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "DOLLARMINUSRUPEES"));
                        //End : #P : 06-02-2020

                    }

                    //string P3 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "PACKETTAG"));
                    //string P4 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle - 1, "PACKETTAG"));
                    //if (P3 != P4)
                    //{
                    //    DouTotalPcs = DouTotalPcs + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "PCS"));   //#P : 21-01-2020
                    //}
                }

                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PLANVARIATIONDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalPlanVarDollar;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PLANVARIATIONRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalPlanVarRupees, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFPLUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDFPlusDollar;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFMINUSDOLLAR") == 0)
                    {
                        e.TotalValue = DouTotalDFMinusDollar;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFPLUSRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDFPlusRupees, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFMINUSRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDFMinusRupees, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DFTYPE") == 0)
                    {
                        e.TotalValue = DouTotalPcs;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("TOTALGRDAMOUNT") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalGRDAmount, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("TOTALORGAMOUNT") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalORGAmount, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PLUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDPlusDollar, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("MINUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDMinusDollar, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DOLLARPLUSRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDPlusRupees, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DOLLARMINUSRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDMinusRupees, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_PLUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDPlusDollar_BeforePer, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_MINUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDMinusDollar_BeforePer, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_DFPLUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDFPlusDollar_BeforePer, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("BEFOREPER_DFMINUSDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalDFMinusDollar_BeforePer, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FINAL_LABOURAMOUNTDOLLAR") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalFinalLabourDollar, "###0.00");
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FINAL_LABOURAMOUNTRUPEES") == 0)
                    {
                        e.TotalValue = Val.Format(DouTotalFinalLabourRupees, "###0.00");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void GrdDetail_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                //string StrCol1 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "KAPANPACKET"));
                //string StrCol2 = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle - 1, "KAPANPACKET"));

                //if (StrCol1 != StrCol2)
                //{
                //    e.Graphics.DrawLine(new Pen(Color.Red) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
                //     new Point(e.Bounds.X, e.Bounds.Bottom), new Point(e.Bounds.Right, e.Bounds.Bottom));
                //    //e.Graphics.DrawLine(new Pen(Color.Red) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
                //    //    new Point(e.Bounds.Right, e.Bounds.Top), new Point(e.Bounds.Right, e.Bounds.Bottom));  

                //    GridViewInfo vi = GrdDetail.GetViewInfo() as GridViewInfo;
                //    Point p5 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right, vi.ViewRects.Rows.Y);
                //    Point p6 = new Point(vi.ColumnsInfo[e.Column].Bounds.Right, vi.ViewRects.Rows.Bottom);
                //    Pen pen = new Pen(Color.Black);
                //    e.Graphics.DrawLine(pen, p5, p6);

                //}
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
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
                e.Appearance.BackColor = Color.FromArgb(197, 217, 241);
            }
        }

        private void BtnProcessPktData_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("Are You Sure You Want Process Selected Packet Detail... ") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            DtabSelectedDetail = GetTableOfSelectedRows(GrdDetail, true);

            if (DtabSelectedDetail == null || DtabSelectedDetail.Rows.Count == 0)
            {
                Global.Message("Please Select Atleast One Stone For Process.");
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            BtnProcessPktData.Enabled = false;
            PanelProgress.Visible = true;
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }

            /*
            try
            {
                
                if (Global.Confirm("Are You Sure You Want Process Selected Packet Detail... ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                DataTable DtabSelectedDetail = GetTableOfSelectedRows(GrdDetail, true);


                if (DtabSelectedDetail == null || DtabSelectedDetail.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Process.");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DtabSelectedDetail.TableName = "Table1";

                string StrPktDetailXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DtabSelectedDetail.WriteXml(sw);
                    StrPktDetailXml = sw.ToString();
                }

                string StrOpe = "";
                if (RbtMarker.Checked)
                {
                    StrOpe = "MARKERUPDATE";
                }
                else if (RbtWorker.Checked)
                {
                    StrOpe = "WORKERUPDATE";
                }

                string StrRes = Obj.ProcessSalaryViewDetailWithZeroAmt(Val.ToInt64(txtEmployee.Tag), Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), StrPktDetailXml, StrOpe);

                if (StrRes == "SUCCESS")
                {
                    Global.Message("Selected Data Processed Successfully..");
                }
                else
                {
                    Global.Message("Somthing Goes Wrong..");
                }

                ///*
                //foreach (DataRow DRow in DtabSelectedDetail.Rows)
                //{
                //    string StrKapanName = "";
                //    Int32 IntPacketNo = 0;

                //    StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                //    IntPacketNo = Val.ToInt32(DRow["PACKETNO"]);

                //    int IntRes = 0;

                //    if (RbtMarker.Checked)
                //    {
                //        IntRes = Obj.ResetMarkerWorkerPacketDataForProcess(StrKapanName, IntPacketNo, IntPacketNo, Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), "WORKER"); //PolishOk Process
                //        IntRes = Obj.UpdateMarkerLabourLiveGradingColorStatus(StrKapanName, Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), Val.ToInt32(txtFromPacketNo.Text), Val.ToInt32(txtToPacketNo.Text));
                //    }
                //    else if (RbtWorker.Checked)
                //    {
                //        IntRes = Obj.ResetMarkerWorkerPacketDataForProcess(StrKapanName, IntPacketNo, IntPacketNo, Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), "MARKER"); //PolishOk Process
                //        IntRes = Obj.UpdateMarkerLabourLiveGradingColorStatus(StrKapanName, Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), IntPacketNo, IntPacketNo);
                //    }

                //    if (IntRes > 0)
                //    {
                //        Global.Message("Selected Data Processed Successfully..");
                //    }
                //    else
                //    {
                //        Global.Message("Somthing Goes Wrong..");
                //    }

                //}
                this.Cursor = Cursors.Default;
            }
            catch(Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
            */
        }
        private DataTable GetTableOfSelectedRows(GridView view, Boolean IsSelect)
        {
            if (view.RowCount <= 0)
            {
                return null;
            }
            ArrayList aryLst = new ArrayList();


            DataTable resultTable = new DataTable();
            DataTable sourceTable = null;
            sourceTable = ((DataView)view.DataSource).Table;

            if (IsSelect)
            {
                aryLst = ObjGridSelection.GetSelectedArrayList();
                resultTable = sourceTable.Clone();
                for (int i = 0; i < aryLst.Count; i++)
                {
                    DataRowView oDataRowView = aryLst[i] as DataRowView;
                    resultTable.Rows.Add(oDataRowView.Row.ItemArray);
                }
            }
            return resultTable;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                StrRes = "";
                //this.Cursor = Cursors.WaitCursor;
                DtabSelectedDetail.TableName = "Table1";

                string StrPktDetailXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DtabSelectedDetail.WriteXml(sw);
                    StrPktDetailXml = sw.ToString();
                }

                string StrOpe = "";
                if (RbtMarker.Checked)
                {
                    StrOpe = "MARKERUPDATE";
                }
                else if (RbtWorker.Checked)
                {
                    StrOpe = "WORKERUPDATE";
                }

                StrRes = Obj.ProcessSalaryViewDetailWithZeroAmt(Val.ToInt64(txtEmployee.Tag), Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), StrPktDetailXml, StrOpe);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BtnProcessPktData.Enabled = true;
            PanelProgress.Visible = false;
            if (StrRes == "SUCCESS")
            {
                Global.Message("Selected Data Processed Successfully..");
            }
            else
            {
                Global.Message("Somthing Goes Wrong..");
            }


        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string StrSalaryType = "";
                if (RbtMarker.Checked)
                {
                    StrSalaryType = "MARKER";
                }
                else if (RbtWorker.Checked)
                {
                    StrSalaryType = "WORKER";
                }

                DtabSelectedDetail.Rows.Clear();
                DTabDetail.Rows.Clear();
                DTabDetail = Obj.GetSalaryViewDetailWithZeroAmt(Val.Trim(CmbKapan.Properties.GetCheckedItems()), Val.ToInt32(txtFromPacketNo.Text), Val.ToInt32(txtToPacketNo.Text), Val.ToInt64(txtEmployee.Tag), Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()), StrSalaryType);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                BtnRefresh.Enabled = true;
                PanelProgress.Visible = false;
                if (DTabDetail.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    Global.Message("No Data Found..!");
                    return;
                }
                GrdDetail.BeginUpdate();
                MainGridDetail.DataSource = DTabDetail;
                GrdDetail.RefreshData();
                GrdDetail.BestFitColumns();
                GrdDetail.Columns["WORKERCODE"].ClearFilter();
                GrdDetail.EndUpdate();

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

    }
}
