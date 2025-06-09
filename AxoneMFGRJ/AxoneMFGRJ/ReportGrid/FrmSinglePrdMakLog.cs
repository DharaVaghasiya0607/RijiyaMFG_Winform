using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
using BusLib.Transaction;
using DevExpress.XtraPrinting;
using Google.API.Translate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusLib.TableName;
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using BusLib.Master;
using AxoneMFGRJ.Transaction;
using BusLib.ReportGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Reflection;
using DevExpress.Data;
using DevExpress.XtraPrintingLinks;
using System.Drawing.Printing;
using BusLib.Rapaport;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AxoneMFGRJ.Masters
{
    public partial class FrmSinglePrdMakLog : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PredictionView ObjView = new BOTRN_PredictionView();

        BOFormPer ObjPer = new BOFormPer();

        DataTable DTabPredictionView = new DataTable();
        DataTable Dtab = new DataTable();

        string StrKapan = "";

        string StrFromDate = null;
        string StrToDate = null;

        double DouFromCarat = 0;
        double DouFromRapaport = 0;
        double DouFromRapaportAmt = 0;
        double DouFromDisc = 0;
        double DouFromPricePerCarat = 0;
        double DouFromAmount = 0;

        double DouToCarat = 0;
        double DouToRapaport = 0;
        double DouToRapaportAmt = 0;
        double DouToDisc = 0;
        double DouToPricePerCarat = 0;
        double DouToAmount = 0;

        #region Property Settings

        public FrmSinglePrdMakLog()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));

            DataTable DTabPrdType = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRDTYPE);
            DTabPrdType.DefaultView.Sort = "SEQUENCENo";
            DTabPrdType = DTabPrdType.DefaultView.ToTable();

            txtKapanName.Focus();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjView);
            ObjFormEvent.ObjToDisposeList.Add(Val);

        }

        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GrdDetRowDetail.BeginUpdate();

                StrKapan = Val.ToString(txtKapanName.Text);

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                else
                {
                    StrFromDate = string.Empty;
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }
                else
                {
                    StrToDate = string.Empty;
                }

                Dtab.Rows.Clear();
                BtnSearch.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

                GrdDetRowDetail.EndUpdate();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
                return;
            }


        }


        #region Background Worker

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

        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "Mak Log Prediction";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGridRow,
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


        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("Prediction View", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            //' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("Packet :- " + Val.ToString(txtKapanName.Text) + "-" + txtFromPacketNo.Text + "-" + txtTag.Text, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGridRow) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGridRow.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "EMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "EMPLOYEENAME")));
                    return;
                }

            }
            finally
            {
                e.Info = info;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Dtab = ObjView.PrdMakLogGetData(StrKapan, Val.ToInt(txtFromPacketNo.Text), txtTag.Text, StrFromDate, StrToDate);

            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;

                if (Dtab.Rows.Count <= 0)
                {
                    Global.Message("No Data Found..");
                }

                GrdDetRowDetail.BeginUpdate();

                MainGridRow.DataSource = Dtab;
                MainGridRow.Refresh();

                GrdDetRowDetail.EndUpdate();

            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOTRN_SinglePacketCreate().FindKapan();
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
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

        private void GrdDetRowDetail_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPSHAPE")) == 1 && (e.Column.FieldName == "FROMSHAPE" || e.Column.FieldName == "TOSHAPE"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPCOL")) == 1 && (e.Column.FieldName == "FROMCOL" || e.Column.FieldName == "TOCOL"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPCLA")) == 1 && (e.Column.FieldName == "FROMCLARITY" || e.Column.FieldName == "TOCLARITY"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPCUT")) == 1 && (e.Column.FieldName == "FROMCUT" || e.Column.FieldName == "TOCUT"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPPOL")) == 1 && (e.Column.FieldName == "FROMPOL" || e.Column.FieldName == "TOPOL"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPFL")) == 1 && (e.Column.FieldName == "FROMFL" || e.Column.FieldName == "TOFL"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPSYM")) == 1 && (e.Column.FieldName == "FROMSYM" || e.Column.FieldName == "TOSYM"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPCARAT")) == 1 && (e.Column.FieldName == "FROMCARAT" || e.Column.FieldName == "TOCARAT"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMDIS")) == 1 && (e.Column.FieldName == "FROMDISCOUNT" || e.Column.FieldName == "TODISCOUNT"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMAMTDIS")) == 1 && (e.Column.FieldName == "FROMAMOUNTDISCOUNT" || e.Column.FieldName == "TOAMOUNTDISCOUNT"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMRAPAPORT")) == 1 && (e.Column.FieldName == "FROMRAPAPORT" || e.Column.FieldName == "TORAPAPORT"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPPRICEPERCARAT")) == 1 && (e.Column.FieldName == "FROMPRICEPERCARAT" || e.Column.FieldName == "TOPRICEPERCARAT"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPAMOUNT")) == 1 && (e.Column.FieldName == "FROMAMOUNT" || e.Column.FieldName == "TOAMOUNT"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
            else if (Val.ToInt32(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "COMPRAPDATE")) == 1 && (e.Column.FieldName == "FROMRAPDATE" || e.Column.FieldName == "TORAPDATE"))
            {
                e.Appearance.BackColor = lblUpdateRecord.BackColor;
            }
        }

        private void GrdDetRowDetail_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouFromCarat = 0;
                    DouFromRapaport = 0;
                    DouFromRapaportAmt = 0;
                    DouFromDisc = 0;
                    DouFromPricePerCarat = 0;
                    DouFromAmount = 0;

                    DouToCarat = 0;
                    DouToRapaport = 0;
                    DouToRapaportAmt = 0;
                    DouToDisc = 0;
                    DouToPricePerCarat = 0;
                    DouToAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouFromCarat = DouFromCarat + Val.Val(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "FROMCARAT"));
                    DouFromAmount = DouFromAmount + Val.Val(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "FROMAMOUNT"));
                    DouFromRapaport = Val.Val(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "FROMRAPAPORT"));
                    DouFromPricePerCarat = DouFromAmount / DouFromCarat;
                    DouFromRapaportAmt = DouFromRapaportAmt + (DouFromRapaport * Val.Val(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "FROMCARAT")));

                    DouToCarat = DouToCarat + Val.Val(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "TOCARAT"));
                    DouToAmount = DouToAmount + Val.Val(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "TOAMOUNT"));
                    DouToRapaport = Val.Val(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "TORAPAPORT"));
                    DouToPricePerCarat = DouToAmount / DouToCarat;
                    DouToRapaportAmt = DouToRapaportAmt + (DouToRapaport * Val.Val(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "TOCARAT")));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FROMPRICEPERCARAT") == 0)
                    {
                        if (Val.Val(DouFromCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouFromAmount) / Val.Val(DouFromCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FROMRAPAPORT") == 0)
                    {
                        if (Val.Val(DouFromAmount) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouFromRapaportAmt) / Val.Val(DouFromCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FROMDISCOUNT") == 0)
                    {
                        DouFromRapaport = Math.Round((DouFromRapaportAmt / DouFromCarat), 2);
                        DouFromDisc = Math.Round(((DouFromRapaport - DouFromPricePerCarat) / DouFromRapaport * 100), 2);
                        e.TotalValue = DouFromDisc;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("TOPRICEPERCARAT") == 0)
                    {
                        if (Val.Val(DouToCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouToAmount) / Val.Val(DouToCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("TORAPAPORT") == 0)
                    {
                        if (Val.Val(DouToCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouToRapaportAmt) / Val.Val(DouToCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("TODISCOUNT") == 0)
                    {
                        DouToRapaport = Math.Round(DouToRapaportAmt / DouToCarat);
                        DouToDisc = Math.Round(((DouToRapaport - DouToPricePerCarat) / DouToRapaport * 100), 2);
                        e.TotalValue = DouToDisc;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }


    }
}
