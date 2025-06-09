using AxoneMFGRJ.Attendance;
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
    public partial class FrmHeliumPacketPrintDetail : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjDashboard = new BOTRN_RunninPossition();

        BOFormPer ObjPer = new BOFormPer();

        DataTable DtabDashboard = new DataTable();
        int Minutes = 0, seconds = 0, hours = 0;

        DataTable DtabSelf = new DataTable();
        DataTable DtabStaff = new DataTable();
        int count;

        int DueMinutesCounter;

        #region Property Settings

        public FrmHeliumPacketPrintDetail()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            Clear();
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
                string StrFromDate = "", StrToDate = "";

                if (RdbAll.Checked)
                    StrOpe = "ALL";
                else if (RdbRunning.Checked)
                    StrOpe = "RUNNING";
                else if (RdbOverDue.Checked)
                    StrOpe = "OVERDUE";

                if (DTPSearchFromDate.Checked)
                    StrFromDate = DTPSearchFromDate.Text;

                if (DTPSearchToDate.Checked)
                    StrToDate = DTPSearchToDate.Text;

                DataTable Dt = ObjDashboard.GetDataForHeliumPacketPrintLimitDetail(Val.Trim(CmbKapan.Properties.GetCheckedItems()), Val.SqlDate(StrFromDate), Val.SqlDate(StrToDate), StrOpe);
                DtabDashboard = Dt;

                if (DtabDashboard.Rows.Count > 0)
                {
                    lblTotalAgingPackets.Text = "Total : " + Val.ToString(DtabDashboard.Compute("SUM(ISSUEPCS)", ""));

                    DataRow[] DrTotRunning = DtabDashboard.Select("PACKETPRINTSTATUS = 'RUNNING'");
                    if (DrTotRunning.Length != 0)
                    {
                        lblTotalAgingRunningPkts.Text = "Running : " + DrTotRunning.Length.ToString();
                    }

                    DataRow[] DrTotOverDue = DtabDashboard.Select("PACKETPRINTSTATUS = 'DUE'");
                    if (DrTotOverDue.Length != 0)
                    {
                        lblTotalAgingOverDuePkts.Text = "OverDue : " + DrTotOverDue.Length.ToString();
                    }
                }

                MainGridPacketDetail.DataSource = DtabDashboard;
                MainGridPacketDetail.Refresh();

                //PnlEmployee_Click(null, null);
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
            CmbKapan.SetEditValue(0);
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

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Global.ExcelExport("Idle Employee's List", GrdDetPacketDetail);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

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
                                GrdDetPacketDetail.SetRowCellValue(rowhandle, "RUNNINGOVERDUEMINUTES", string.Format("{0:H:mm:ss}", Convert.ToDateTime(dr["RUNNINGOVERDUEMINUTES"].ToString()) + TimeSpan.FromSeconds(-1)));
                            else if (Val.ToString(dr["PACKETSAGINGSTATUS"]) == "OVERDUE")
                                GrdDetPacketDetail.SetRowCellValue(rowhandle, "RUNNINGOVERDUEMINUTES", string.Format("{0:H:mm:ss}", Convert.ToDateTime(dr["RUNNINGOVERDUEMINUTES"].ToString()) + TimeSpan.FromSeconds(+1)));

                            GrdDetPacketDetail.UpdateCurrentRow();

                            if (Val.ToInt32(dr["EXTRAPLUSMINUTES"]) != 0)
                            {
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Global.Message(ex.Message.ToString());
            }
        }
        public string AppendZero(double str)
        {
            if (str <= 9)
                return "0" + str.ToString();
            else return str.ToString();
        }

        private void GrdDetPacketDetail_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            string StrAgingStatus = Val.ToString(GrdDetPacketDetail.GetRowCellValue(e.RowHandle, "PACKETPRINTSTATUS"));

            if (StrAgingStatus == "DUE")
            {
                //e.Appearance.BackColor = Color.FromArgb(213, 229, 239);
                e.Appearance.BackColor = Color.FromArgb(180, 217, 222);
            }

        }

        private void txtPassForUpdateExtraMints_TextChanged(object sender, EventArgs e)
        {
            if (Val.ToString(txtPassForUpdateExtraMints.Tag) != "" && Val.ToString(txtPassForUpdateExtraMints.Tag).ToUpper() == txtPassForUpdateExtraMints.Text.ToUpper())
            {
                GrdDetPacketDetail.Columns["EXTRAPRINTLIMIT"].OptionsColumn.AllowEdit = true;
            }
            else
            {
                GrdDetPacketDetail.Columns["EXTRAPRINTLIMIT"].OptionsColumn.AllowEdit = false;
            }
        }

        private void GrdDetPacketDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;

                if (e.Column.FieldName.ToUpper() == "EXTRAPRINTLIMIT")
                {
                    DataRow Dr = GrdDetPacketDetail.GetDataRow(e.RowHandle);
                    if (Val.ToInt32(Dr["EXTRAPRINTLIMIT"]) > 0)
                    {
                        int IntRes = ObjDashboard.SaveHeliumExtraPacketPrintLimit(Val.ToString(Dr["HELLIMIT_ID"]), Val.ToInt32(Dr["EXTRAPRINTLIMIT"]));
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
                if (Val.ToInt32(txtPrintLimits.Text) == 0)
                {
                    Global.Message("Please Enter Extra Minutes..");
                    txtPrintLimits.Focus();
                    return;
                }

                for (int i = 0; i < GrdDetPacketDetail.DataRowCount; i++)
                {
                    //if (Val.ToString(GrdDetPacketDetail.GetRowCellValue(i, "EXTRAPRINTLIMIT")) == "OVERDUE")
                    if (Val.ToString(GrdDetPacketDetail.GetRowCellValue(i, "PACKETPRINTSTATUS")) == "DUE")
                    {
                        string Str = Val.ToString(GrdDetPacketDetail.GetRowCellValue(i, "HELLIMIT_ID"));
                        GrdDetPacketDetail.SetRowCellValue(i, "EXTRAPRINTLIMIT", txtPrintLimits.Text);
                    }
                    //else
                    //{
                    //    string Str = Val.ToString(GrdDetPacketDetail.GetRowCellValue(i, "HELLIMIT_ID"));
                    //    GrdDetPacketDetail.SetRowCellValue(i, "EXTRAPRINTLIMIT", txtPrintLimits.Text);
                    //}
                }
                DtabDashboard.AcceptChanges();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void lblTotalAgingPackets_Click(object sender, EventArgs e)
        {

        }
    }
}
