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
using BusLib.View;
using OfficeOpenXml;
using DevExpress.XtraPivotGrid;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmMarkerGradingComparisionWithLatestGrd : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjView = new BOTRN_RunninPossition();

        #region Property Settings

        public FrmMarkerGradingComparisionWithLatestGrd()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
            CmbKapan.Focus();

            DTPFromDate.Value = DateTime.Now.AddMonths(-1);
            DTPToDate.Value = DateTime.Now;

            txtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
            txtEmployee.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;

            if (BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME == "ADMIN")
            {
                txtEmployee.Enabled = true;
            }
            else
            {
                txtEmployee.Enabled = false;
            }
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
                string StrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());

                string StrFromDate = null;
                string StrToDate = null;

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                this.Cursor = Cursors.WaitCursor;
                DataSet DGrd = ObjView.GerGradingComparisionWithLatestGrdByLab(StrKapan, "GRD", StrFromDate, StrToDate, Val.ToInt64(txtEmployee.Tag));

                PvtGrdColor.DataSource = DGrd.Tables[0];
                PvtGrdColor.Refresh();

                PvtGrdClarity.DataSource = DGrd.Tables[1];
                PvtGrdClarity.Refresh();

                PvtGrdCut.DataSource = DGrd.Tables[2];
                PvtGrdCut.Refresh();

                PvtGrdPol.DataSource = DGrd.Tables[3];
                PvtGrdPol.Refresh();

                PvtGrdSym.DataSource = DGrd.Tables[4];
                PvtGrdSym.Refresh();

                MainGrdTotal.DataSource = DGrd.Tables[6];
                MainGrdTotal.Refresh();
                GrdDetTotal.BestFitColumns();

                //DataSet DSMum = ObjView.GerGradingComparisionWithLatestGrdByLab(StrKapan, "BY", StrFromDate, StrToDate, Val.ToInt64(txtEmployee.Tag));

                //PvtMumColor.DataSource = DSMum.Tables[0];
                //PvtMumColor.Refresh();

                //PvtMumClarity.DataSource = DSMum.Tables[1];
                //PvtMumClarity.Refresh();

                //PvtMumCut.DataSource = DSMum.Tables[2];
                //PvtMumCut.Refresh();

                //PvtMumPol.DataSource = DSMum.Tables[3];
                //PvtMumPol.Refresh();

                //PvtMumSym.DataSource = DSMum.Tables[4];
                //PvtMumSym.Refresh();

                //DataSet DSLab = ObjView.GerGradingComparisionWithLatestGrdByLab(StrKapan, "LAB", StrFromDate, StrToDate, Val.ToInt64(txtEmployee.Tag));

                //PvtLabColor.DataSource = DSLab.Tables[0];
                //PvtLabColor.Refresh();

                //PvtLabClarity.DataSource = DSLab.Tables[1];
                //PvtLabClarity.Refresh();

                //PvtLabCut.DataSource = DSLab.Tables[2];
                //PvtLabCut.Refresh();

                //PvtLabPol.DataSource = DSLab.Tables[3];
                //PvtLabPol.Refresh();

                //PvtLabSym.DataSource = DSLab.Tables[4];
                //PvtLabSym.Refresh();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
                return;
            }


        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnDirectPDFExport_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
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

        private void PvtGrdColor_CustomDrawCell(object sender, DevExpress.XtraPivotGrid.PivotCustomDrawCellEventArgs e)
        {
            try
            {

                if (Val.ToString(e.DataField.FieldName) == "PCS")
                {
                    PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
                    int Sequence;
                    int Sequence1;
                    Sequence = Val.ToInt(ds.GetValue(0, "EXPSEQNO"));
                    Sequence1 = Val.ToInt(ds.GetValue(0, "GRDSEQNO"));

                    if (Sequence1 > Sequence)
                    {
                        e.Appearance.BackColor = lblRed.BackColor;
                        if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000" || e.DisplayText == "")
                        {
                            e.Appearance.ForeColor = lblRed.BackColor;
                        }
                    }
                    else if (Sequence1 < Sequence)
                    {
                        e.Appearance.BackColor = lblGreen.BackColor;
                        if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000" || e.DisplayText == "")
                        {
                            e.Appearance.ForeColor = lblGreen.BackColor;
                        }
                    }

                }


            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }
        }

        private void PvtGrdColor_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "EXPNAME")
            {
                if (e.Value1 == null || e.Value2 == null) return;
                e.Handled = true;
                int s1 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "EXPSEQNO"));
                int s2 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "EXPSEQNO"));
                e.Result = Comparer.Default.Compare(s1, s2);
                e.Handled = true;
            }

            if (e.Field.FieldName == "GRDNAME")
            {
                if (e.Value1 == null || e.Value2 == null) return;
                e.Handled = true;
                int s1 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "GRDSEQNO"));
                int s2 = Val.ToInt(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "GRDSEQNO"));
                e.Result = Comparer.Default.Compare(s1, s2);
                e.Handled = true;
            }
        }

        private void PvtGrdColor_CustomDrawFieldValue(object sender, PivotCustomDrawFieldValueEventArgs e)
        {
            DevExpress.XtraPivotGrid.PivotGridControl pPivotGrid = (DevExpress.XtraPivotGrid.PivotGridControl)sender;
            PropertyInfo pi = typeof(PivotCustomDrawFieldValueEventArgs).GetProperty("FieldCellViewInfo", (BindingFlags.NonPublic | BindingFlags.Instance));
            DevExpress.XtraPivotGrid.ViewInfo.PivotFieldsAreaCellViewInfo viewInfo = ((DevExpress.XtraPivotGrid.ViewInfo.PivotFieldsAreaCellViewInfo)(pi.GetValue(e, null)));
            if (
                (
                    (
                        (viewInfo.Item.Area == PivotArea.RowArea)
                        &&
                            (
                                (viewInfo.MinLastLevelIndex <= pPivotGrid.Cells.FocusedCell.Y)
                                &&
                                (viewInfo.MaxLastLevelIndex >= pPivotGrid.Cells.FocusedCell.Y)
                            )
                    )
                    ||
                    (
                        (viewInfo.Item.Area == PivotArea.ColumnArea)
                        &&
                            (
                                (viewInfo.MinLastLevelIndex <= pPivotGrid.Cells.FocusedCell.X)
                                &&
                                (viewInfo.MaxLastLevelIndex >= pPivotGrid.Cells.FocusedCell.X)
                            )
                    )
                  )
                )
            {
                // e.Appearance.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
                e.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void PvtGrdColor_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            string StrClick = string.Empty;
            string StrTitle = string.Empty;

            string StrControlName = ((PivotGridControl)sender).Name;


            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            string StrExp = Val.ToString(ds.GetValue(0, "EXPNAME"));
            string StrGrd = Val.ToString(ds.GetValue(0, "GRDNAME"));
            string StrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());

            if (StrControlName == "PvtGrdColor")
            {
                StrClick = "COLOR";
                StrTitle = "Color Comparision Of Exp [" + StrExp + "]  Vs Factory Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtGrdClarity")
            {
                StrClick = "CLARITY";
                StrTitle = "Clarity Comparision Of Exp [" + StrExp + "]  Vs Factory Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtGrdCut")
            {
                StrClick = "CUT";
                StrTitle = "Cut Comparision Of Exp [" + StrExp + "]  Vs Factory Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtGrdPol")
            {
                StrClick = "POL";
                StrTitle = "Pol Comparision Of Exp [" + StrExp + "]  Vs Factory Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtGrdSym")
            {
                StrClick = "SYM";
                StrTitle = "Sym Comparision Of Exp [" + StrExp + "]  Vs Factory Grd [" + StrGrd + "]";
            }

            if (StrControlName == "PvtMumColor")
            {
                StrClick = "COLOR";
                StrTitle = "Color Comparision Of Exp [" + StrExp + "]  Vs Mumbai Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtMumClarity")
            {
                StrClick = "CLARITY";
                StrTitle = "Clarity Comparision Of Exp [" + StrExp + "]  Vs Mumbai Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtMumCut")
            {
                StrClick = "CUT";
                StrTitle = "Cut Comparision Of Exp [" + StrExp + "]  Vs Mumbai Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtMumPol")
            {
                StrClick = "POL";
                StrTitle = "Pol Comparision Of Exp [" + StrExp + "]  Vs Mumbai Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtMumSym")
            {
                StrClick = "SYM";
                StrTitle = "Sym Comparision Of Exp [" + StrExp + "]  Vs Mumbai Grd [" + StrGrd + "]";
            }

            if (StrControlName == "PvtLabColor")
            {
                StrClick = "COLOR";
                StrTitle = "Color Comparision Of Exp [" + StrExp + "]  Vs Lab Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtLabClarity")
            {
                StrClick = "CLARITY";
                StrTitle = "Clarity Comparision Of Exp [" + StrExp + "]  Vs Lab Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtLabCut")
            {
                StrClick = "CUT";
                StrTitle = "Cut Comparision Of Exp [" + StrExp + "]  Vs Lab Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtLabPol")
            {
                StrClick = "POL";
                StrTitle = "Pol Comparision Of Exp [" + StrExp + "]  Vs Lab Grd [" + StrGrd + "]";
            }
            else if (StrControlName == "PvtLabSym")
            {
                StrClick = "SYM";
                StrTitle = "Sym Comparision Of Exp [" + StrExp + "]  Vs Lab Grd [" + StrGrd + "]";
            }
            string StrFromDate = null;
            string StrToDate = null;

            if (DTPFromDate.Checked == true)
            {
                StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
            }
            if (DTPToDate.Checked == true)
            {
                StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
            }

            this.Cursor = Cursors.WaitCursor;

            DataTable DTab = ObjView.GerGradingComparisionDetailWithLatestGrdByLab(StrKapan, StrFromDate, StrToDate, Val.ToInt64(txtEmployee.Tag), StrClick, StrExp, StrGrd);
            if (DTab.Rows.Count == 0)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            FrmMarkerGradingComparisionPopupDetail FrmMarkerGradingComparisionPopupDetail = new FrmMarkerGradingComparisionPopupDetail();
            FrmMarkerGradingComparisionPopupDetail.DTabPacketWiseStock = DTab;
            FrmMarkerGradingComparisionPopupDetail.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmMarkerGradingComparisionPopupDetail);
            FrmMarkerGradingComparisionPopupDetail.ShowForm(StrTitle, StrClick);

            this.Cursor = Cursors.Default;

        }

        private void GrdDetTotal_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;

                DataRow DR = GrdDetTotal.GetDataRow(e.RowHandle);
                if (DR == null || e.Clicks != 2)
                {
                    return;
                }

                string StrClick = string.Empty;
                string StrTitle = string.Empty;
                string StrExp = string.Empty;
                string StrGrd = string.Empty;

                string StrControlName = Val.ToString(DR["PARATYPE"]).ToUpper();
                string StrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());


                StrClick = "TOTAL";
                //StrTitle = "Sym Comparision Of Exp [" + StrExp + "]  Vs Lab Grd [" + StrGrd + "]";

                string StrTitleType = Val.ToString(GrdDetTotal.FocusedColumn.FieldName).ToUpper() == "TOTALPCS" ? "All" : Val.ProperText(GrdDetTotal.FocusedColumn.FieldName);

                StrTitle = "Marker Grading Comparision Total Stone Detail of : " + Val.ToString(DR["PARATYPE"]) + " (" + StrTitleType + ")";

                StrExp = GrdDetTotal.FocusedColumn.FieldName;
                StrGrd = Val.ToString(DR["PARATYPE"]).ToUpper();

                string StrFromDate = null;
                string StrToDate = null;

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                this.Cursor = Cursors.WaitCursor;

                DataTable DTab = ObjView.GerGradingComparisionDetailWithLatestGrdByLab(StrKapan, StrFromDate, StrToDate, Val.ToInt64(txtEmployee.Tag), StrClick, StrExp, StrGrd);
                if (DTab.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                FrmMarkerGradingComparisionPopupDetail FrmMarkerGradingComparisionPopupDetail = new FrmMarkerGradingComparisionPopupDetail();
                FrmMarkerGradingComparisionPopupDetail.DTabPacketWiseStock = DTab;
                FrmMarkerGradingComparisionPopupDetail.MdiParent = Global.gMainRef;
                ObjFormEvent.ObjToDisposeList.Add(FrmMarkerGradingComparisionPopupDetail);
                FrmMarkerGradingComparisionPopupDetail.ShowForm(StrTitle, StrClick);

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
