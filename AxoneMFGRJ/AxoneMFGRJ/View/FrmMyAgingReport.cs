using AxoneMFGRJ.Attendance;
using AxoneMFGRJ.Report;
using BusLib;
using BusLib.Attendance;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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

namespace AxoneMFGRJ.View
{
    public partial class FrmMyAgingReport : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjDashboard = new BOTRN_RunninPossition();

        BOFormPer ObjPer = new BOFormPer();

        DataTable DtabDashboard = new DataTable();
        int Minutes = 0, seconds = 0, hours = 0;

        int TotalMin = 0;

        DataTable DtabSelf = new DataTable();
        DataTable DtabStaff = new DataTable();
        EmployeeActionRightsProperty Property = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
        int count;

        int DueMinutesCounter;

        #region Property Settings

        public FrmMyAgingReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            Clear();
            timer1.Start();

            // #d: 02-09-2020
            GrdDetPacketDetail.Columns["EXTRAPLUSMINUTES"].Visible = Property.ISALLOWEXTRAMIN;
            txtEmployee.Tag = BOConfiguration.gEmployeeProperty.LEDGER_ID;
            txtEmployee.Text = BOConfiguration.gEmployeeProperty.LEDGERNAME;
            // end : 02-09-2020

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            CmbDepartment.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);
            CmbDepartment.Properties.DisplayMember = "DEPARTMENTNAME";
            CmbDepartment.Properties.ValueMember = "DEPARTMENTNAME";

            txtMinutes.Visible = false;
            lblApplyAll.Visible = false;
            lblmin.Visible = false;

            this.Show();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }
        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                Int64 IntLedger_ID = 0;
                string StrOpe = "";
                IntLedger_ID = BOConfiguration.gEmployeeProperty.LEDGER_ID;
                DtabDashboard.Rows.Clear();

                if (RdbAll.Checked)
                    StrOpe = "ALL";
                else if (RdbRunning.Checked)
                    StrOpe = "RUNNING";
                else if (RdbOverDue.Checked)
                    StrOpe = "OVERDUE";

                string StrFromDate = "", StrToDate = "";

                if (DTPSearchFromDate.Checked)
                    StrFromDate = DTPSearchFromDate.Text;

                if (DTPSearchToDate.Checked)
                    StrToDate = DTPSearchToDate.Text;

                DataTable Dt = ObjDashboard.GetDataForMyAgingReport(StrOpe, Val.SqlDate(StrFromDate), Val.SqlDate(StrToDate), Val.Trim(CmbKapan.Properties.GetCheckedItems()), IntLedger_ID, Val.Trim(CmbDepartment.Properties.GetCheckedItems()));
                DtabDashboard = Dt;

                if (DtabDashboard.Rows.Count > 0)
                {
                    lblTotalAgingPackets.Text = "Total : " + Val.ToString(DtabDashboard.Compute("SUM(ISSUEPCS)", ""));

                    DataRow[] DrTotRunning = DtabDashboard.Select("PACKETSAGINGSTATUS = 'RUNNING'");
                    if (DrTotRunning.Length != 0)
                    {
                        lblTotalAgingRunningPkts.Text = "Running : " + DrTotRunning.Length.ToString();
                    }

                    DataRow[] DrTotOverDue = DtabDashboard.Select("PACKETSAGINGSTATUS = 'OVERDUE'");
                    if (DrTotOverDue.Length != 0)
                    {
                        lblTotalAgingOverDuePkts.Text = "OverDue : " + DrTotOverDue.Length.ToString();
                    }

                }

                MainGridPacketDetail.DataSource = DtabDashboard;
                MainGridPacketDetail.Refresh();

                if (ChkUpdateExtramin.Checked == false)
                {
                    timer1.Start();
                }
                else
                {
                    timer1.Stop();
                }


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        public void Clear()
        {
            lblTotalAgingPackets.Text = "Total : 0";
            lblTotalAgingRunningPkts.Text = "Running : 0";
            lblTotalAgingOverDuePkts.Text = "OverDue : 0";
            //lblTotalPendingLeave.Text = "0";
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGridPacketDetail) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGridPacketDetail.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MANAGERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "MANAGERNAME")));
                    return;
                }
            }
            finally
            {
                e.Info = info;
            }
        }

        private void FrmLeaveApproval_FormClosing(object sender, FormClosingEventArgs e)
        {
            BtnSearch.PerformClick();
        }

        private void FrmAdminDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    BtnSearch_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void RdbMyStaff_CheckedChanged(object sender, EventArgs e)
        {

        }

        //public void ConvertToDayHours()
        //{
        //try
        //{
        //    GrdDetPacketDetail.PostEditor();
        //    for (int i = 0; i < GrdDetPacketDetail.RowCount; i++)
        //    {
        //        int rowhandle = GrdDetPacketDetail.GetVisibleRowHandle(i);

        //        if (GrdDetPacketDetail.IsDataRow(rowhandle))
        //        {
        //            DataRow dr = GrdDetPacketDetail.GetDataRow(i);
        //            if (dr != null)
        //            {
        //                if (Val.ToString(dr["PACKETSAGINGSTATUS"]) == "RUNNING")
        //                    GrdDetPacketDetail.SetRowCellValue(rowhandle, "RUNNINGOVERDUEMINUTES", Val.ToString(dr["RUNNINGOVERDUEMINUTES"]));
        //                else if (Val.ToString(dr["PACKETSAGINGSTATUS"]) == "OVERDUE")
        //                    GrdDetPacketDetail.SetRowCellValue(rowhandle, "RUNNINGOVERDUEMINUTES", Val.ToString(dr["RUNNINGOVERDUEMINUTES"]));

        //if (Val.ToString(dr["PACKETSAGINGSTATUS"]) == "RUNNING")
        //    GrdDetPacketDetail.SetRowCellValue(rowhandle, "RUNNINGOVERDUEMINUTES", string.Format("{0:H:mm:ss}", Convert.ToDateTime(dr["RUNNINGOVERDUEMINUTES"].ToString()) + TimeSpan.FromSeconds(-1)));
        //else if (Val.ToString(dr["PACKETSAGINGSTATUS"]) == "OVERDUE")
        //    GrdDetPacketDetail.SetRowCellValue(rowhandle, "RUNNINGOVERDUEMINUTES", string.Format("{0:H:mm:ss}", Convert.ToDateTime(dr["RUNNINGOVERDUEMINUTES"].ToString()) + TimeSpan.FromSeconds(+1)));


        //int pIntTempMin = 0;
        //int test = 0;

        //if (Val.ToString(dr["PACKETSAGINGSTATUS"]) == "RUNNING")
        //{
        //    test = Math.Abs(Val.ToInt32(dr["PACKETSAGINGMINUTES"])) * 60;
        //    pIntTempMin = test-1;
        //    GrdDetPacketDetail.SetRowCellValue(rowhandle, "TEMPMIN", Math.Abs(pIntTempMin));
        //}
        //else if (Val.ToString(dr["PACKETSAGINGSTATUS"]) == "OVERDUE")
        //{
        //    test = Math.Abs(Val.ToInt32(dr["PACKETSAGINGMINUTES"]))*60;
        //    pIntTempMin = test + 1;
        //    GrdDetPacketDetail.SetRowCellValue(rowhandle, "TEMPMIN", pIntTempMin);
        //}

        //string PacketsAgingStatus = Val.ToString(GrdDetPacketDetail.GetRowCellValue(i, "PACKETSAGINGSTATUS"));
        //string RUNNINGOVERDUEMINUTES = Global.ConvertIntoDaysHoursFormate(Val.ToString(GrdDetPacketDetail.GetRowCellValue(i, "TEMPMIN")), PacketsAgingStatus);
        // string RUNNINGOVERDUEMINUTES = Val.ToString(GrdDetPacketDetail.GetRowCellValue(i, "RUNNINGOVERDUEMINUTES"));

        //var time = RUNNINGOVERDUEMINUTES;

        //if (Val.ToString(dr["PACKETSAGINGSTATUS"]) == "RUNNING")
        //{
        //    RUNNINGOVERDUEMINUTES = time + TimeSpan.FromSeconds(-1);
        //}
        //else
        //{
        //    RUNNINGOVERDUEMINUTES = time + TimeSpan.FromSeconds(+1);
        //}

        //GrdDetPacketDetail.SetRowCellValue(rowhandle, "RUNNINGOVERDUEMINUTES", RUNNINGOVERDUEMINUTES);

        // GrdDetPacketDetail.UpdateCurrentRow();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.Message(ex.Message.ToString());
        //    }
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                GrdDetPacketDetail.PostEditor();
                for (int i = 0; i < GrdDetPacketDetail.RowCount; i++)
                {
                    int rowhandle = GrdDetPacketDetail.GetVisibleRowHandle(i);

                    if (GrdDetPacketDetail.IsDataRow(rowhandle))
                    {
                        DataRow dr = GrdDetPacketDetail.GetDataRow(i);
                        if (dr != null)
                        {

                            if (Val.ToString(dr["PACKETSAGINGSTATUS"]) == "RUNNING")
                                GrdDetPacketDetail.SetRowCellValue(rowhandle, "RUNNINGOVERDUEMINUTES", Val.ToString(dr["RUNNINGOVERDUEMINUTES"]));
                            else if (Val.ToString(dr["PACKETSAGINGSTATUS"]) == "OVERDUE")
                                GrdDetPacketDetail.SetRowCellValue(rowhandle, "RUNNINGOVERDUEMINUTES", Val.ToString(dr["RUNNINGOVERDUEMINUTES"]));

                            GrdDetPacketDetail.UpdateCurrentRow();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
            //try
            //{
            //    GrdDetPacketDetail.PostEditor();
            //    for (int i = 0; i < GrdDetPacketDetail.RowCount; i++)
            //    {
            //        int rowhandle = GrdDetPacketDetail.GetVisibleRowHandle(i);

            //        if (GrdDetPacketDetail.IsDataRow(rowhandle))
            //        {
            //            DataRow dr = GrdDetPacketDetail.GetDataRow(i);
            //            if (Val.ToInt32(dr["ISWORKINGENDTIME"]) == 0)
            //            {
            //                timer1.Stop();
            //            }
            //            else
            //            {
            //                timer1.Start();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message.ToString());
            //}
        }

        public string AppendZero(double str)
        {
            if (str <= 9)
                return "0" + str.ToString();
            else return str.ToString();
        }

        void UpdateTime() //Example For Display Timer in lable
        {
            lblTest.Text = string.Format("{0:H:mm:ss}", DateTime.Today + TimeSpan.FromSeconds(++count));
        }

        private void GrdDetPacketDetail_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            string StrAgingStatus = Val.ToString(GrdDetPacketDetail.GetRowCellValue(e.RowHandle, "PACKETSAGINGSTATUS"));

            if (StrAgingStatus == "OVERDUE")
            {
                e.Appearance.BackColor = Color.FromArgb(180, 217, 222);
            }

        }

        private void txtPassForUpdateExtraMints_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnUpdateAgingMinutes_Click(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDetPacketDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                GrdDetPacketDetail.PostEditor();
                if (e.Column.FieldName.ToUpper() == "EXTRAPLUSMINUTES")
                {

                    Double AgingMinWithPer = 0;
                    DataRow Dr = GrdDetPacketDetail.GetDataRow(e.RowHandle);

                    AgingMinWithPer = ((Val.Val(Dr["AGINGMINUTES"]) * Property.EXTRAMINPER) / 100);

                    if (Val.ToString(txtPassForUpdateExtraMints.Tag) != Val.ToString(txtPassForUpdateExtraMints.Text))
                    {
                        if (AgingMinWithPer < Val.Val(Dr["EXTRAPLUSMINUTES"]))
                        {
                            Global.Message("You have no Rights to add extra minute More Then Aging Minutes");
                            return;
                        }
                    }

                    if (Property.ISALLOWEXTRAMIN == true && (Val.ToString(Dr["PACKETSAGINGSTATUS"]) == "OVERDUE"))
                    {
                        TotalMin = Val.ToInt32(Dr["EXTRAPLUSMINUTES"]) + Val.ToInt32(Dr["PACKETSAGINGMINUTES"]) + Val.ToInt32(Dr["TOTALEXTRAMIN"]);
                        GrdDetPacketDetail.SetFocusedRowCellValue("TOTALEXTRAMIN", TotalMin);
                        int IntRes = ObjDashboard.SaveAgingTotalExtraPlusMinutes(Val.ToString(Dr["TRNAGING_ID"]), Val.ToInt32(Dr["TOTALEXTRAMIN"]), Val.ToInt32(Dr["EXTRAPLUSMINUTES"]));
                        if (IntRes == 1)
                        {
                            //Global.Message("Extra Plus Minutes Update SuccessFully.");
                        }
                    }

                    if (Val.ToInt32(Dr["EXTRAPLUSMINUTES"]) > 0 && Property.ISALLOWEXTRAMIN == true && (Val.ToString(Dr["PACKETSAGINGSTATUS"]) == "RUNNING"))
                    {
                        int IntRes = 0;
                       // if (Val.ToInt32(Dr["TOTALEXTRAMIN"]) != 0)
                        {
                            TotalMin = Math.Abs(Val.ToInt32(Dr["EXTRAPLUSMINUTES"])) + Val.ToInt32(Dr["TOTALEXTRAMIN"]);
                            //GrdDetPacketDetail.SetFocusedRowCellValue("TOTALEXTRAMIN", TotalMin);
                            IntRes = ObjDashboard.SaveAgingTotalExtraPlusMinutes(Val.ToString(Dr["TRNAGING_ID"]), TotalMin, Val.ToInt32(Dr["EXTRAPLUSMINUTES"]));
                        }
                        //else
                        //{
                        //    GrdDetPacketDetail.SetFocusedRowCellValue("TOTALEXTRAMIN", Val.ToInt32(Dr["EXTRAPLUSMINUTES"]));
                        //    IntRes = ObjDashboard.SaveAgingTotalExtraPlusMinutes(Val.ToString(Dr["TRNAGING_ID"]), Val.ToInt32(Dr["EXTRAPLUSMINUTES"]), Val.ToInt32(Dr["EXTRAPLUSMINUTES"]));
                        //}
                        if (IntRes == 1)
                        {
                            //Global.Message("Extra Plus Minutes Update SuccessFully.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void lblApplyAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtPassForUpdateExtraMints.Tag) != Val.ToString(txtPassForUpdateExtraMints.Text))
                {
                    Global.Message("Please Enter Password For Add Extra Minute");
                    txtPassForUpdateExtraMints.Focus();
                    return;
                }
                else
                {
                    if (Val.ToInt32(txtMinutes.Text) == 0)
                    {
                        Global.Message("Please Enter Extra Minutes..");
                        txtMinutes.Focus();
                        return;
                    }
                    for (int i = 0; i < GrdDetPacketDetail.DataRowCount; i++)
                    {

                        if (Val.ToString(GrdDetPacketDetail.GetRowCellValue(i, "PACKETSAGINGSTATUS")) == "OVERDUE")
                        {
                            string Str = Val.ToString(GrdDetPacketDetail.GetRowCellValue(i, "TRNAGING_ID"));
                            GrdDetPacketDetail.SetRowCellValue(i, "TOTALEXTRAMIN", TotalMin);
                            GrdDetPacketDetail.SetRowCellValue(i, "EXTRAPLUSMINUTES", txtMinutes.Text);

                        }
                        else
                        {
                            string Str = Val.ToString(GrdDetPacketDetail.GetRowCellValue(i, "TRNAGING_ID"));
                            GrdDetPacketDetail.SetRowCellValue(i, "EXTRAPLUSMINUTES", txtMinutes.Text);
                            GrdDetPacketDetail.SetRowCellValue(i, "TOTALEXTRAMIN", txtMinutes.Text);
                        }
                    }
                    DtabDashboard.AcceptChanges();
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtPassForUpdateExtraMints_Validating(object sender, CancelEventArgs e)
        {
            if (Val.ToString(txtPassForUpdateExtraMints.Tag) != "" && Val.ToString(txtPassForUpdateExtraMints.Tag).ToUpper() == txtPassForUpdateExtraMints.Text.ToUpper())
            {
                timer1.Stop();
                txtMinutes.Visible = true;
                lblApplyAll.Visible = true;
                lblmin.Visible = true;
                GrdDetPacketDetail.Columns["EXTRAPLUSMINUTES"].Visible = true;
            }
            else if ((Val.ToString(txtPassForUpdateExtraMints.Text) == "" && Val.ToString(txtPassForUpdateExtraMints.Tag).ToUpper() != txtPassForUpdateExtraMints.Text.ToUpper()) && Property.ISALLOWEXTRAMIN == true)
            {
                timer1.Stop();
                txtMinutes.Visible = false;
                lblApplyAll.Visible = false;
                lblmin.Visible = false;
                GrdDetPacketDetail.Columns["EXTRAPLUSMINUTES"].Visible = true;
            }
            else if ((Val.ToString(txtPassForUpdateExtraMints.Text) == "" && Val.ToString(txtPassForUpdateExtraMints.Tag).ToUpper() != txtPassForUpdateExtraMints.Text.ToUpper()) && Property.ISALLOWEXTRAMIN == false)
            {
                timer1.Stop();
                txtMinutes.Visible = true;
                lblApplyAll.Visible = true;
                lblmin.Visible = true;
                GrdDetPacketDetail.Columns["EXTRAPLUSMINUTES"].Visible = false;
            }
            else
            {
                timer1.Start();
                txtMinutes.Visible = false;
                lblApplyAll.Visible = false;
                lblmin.Visible = false;
                GrdDetPacketDetail.Columns["EXTRAPLUSMINUTES"].Visible = false;
            }


        }

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERALL);

                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
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

        private void BtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Global.ExcelExport("Aging Report", GrdDetPacketDetail);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDetPacketDetail.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void ChkUpdateExtramin_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkUpdateExtramin.Checked == true)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }

        private void BtnPrint_Click_1(object sender, EventArgs e)
        {
            Clear();
            Int64 IntLedger_ID = 0;
            string StrOpe = "";
            IntLedger_ID = BOConfiguration.gEmployeeProperty.LEDGER_ID;
            DtabDashboard.Rows.Clear();

            if (Val.ToString(CmbDepartment.Text) == "")
            {
                Global.Message("Please Select Department For Print");
                CmbDepartment.Focus();
                return;
            }

            if (RdbAll.Checked)
                StrOpe = "ALL";
            else if (RdbRunning.Checked)
                StrOpe = "RUNNING";
            else if (RdbOverDue.Checked)
                StrOpe = "OVERDUE";

            string StrFromDate = "", StrToDate = "";

            if (DTPSearchFromDate.Checked)
                StrFromDate = DTPSearchFromDate.Text;

            if (DTPSearchToDate.Checked)
                StrToDate = DTPSearchToDate.Text;

            DataTable DTab = ObjDashboard.GetDataForMyAgingReportPrint(StrOpe, Val.SqlDate(StrFromDate), Val.SqlDate(StrToDate), Val.Trim(CmbKapan.Properties.GetCheckedItems()), IntLedger_ID, Val.Trim(CmbDepartment.Properties.GetCheckedItems()));
            if (DTab.Rows.Count == 0)
            {
                Global.MessageError("There Is No Data For Print");
                return;
            }

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.MdiParent = Global.gMainRef;
            FrmReportViewer.ShowWithPrint("AgingPrint", DTab);
        }

    }
}
