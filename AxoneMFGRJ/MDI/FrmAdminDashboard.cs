using AxoneMFGRJ.Attendance;
using BusLib;
using BusLib.Attendance;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
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

namespace AxoneMFGRJ.MDI
{
    public partial class FrmAdminDashboard : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Attendance ObjDashboard = new BOMST_Attendance();

        BOFormPer ObjPer = new BOFormPer();

        DataTable DtabDashboard = new DataTable();

        DataTable DtabSelf = new DataTable();
        DataTable DtabStaff = new DataTable();

        #region Property Settings

        public FrmAdminDashboard()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            Clear();

            if(BOConfiguration.gEmployeeProperty.DEPARTMENTNAME == "ADMIN")
                RdbAll.Visible = true;
            else
                RdbAll.Visible = false;

            ChkCmbEmployee.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
            ChkCmbEmployee.Properties.DisplayMember = "EMPLOYEENAME";
            ChkCmbEmployee.Properties.ValueMember = "EMPLOYEE_ID";

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

        private void ChkBtnToday_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBtnToday.Checked == true)
            {
                ChkBtnThisMonth.Checked = false;
                ChkBtnThisWeek.Checked = false;
                ChkBtnYesterDay.Checked = false;

                DTPSearchFromDate.Text = DateTime.Now.ToString();
                DTPSearchToDate.Text = DateTime.Now.ToString();
            }

        }

        private void ChkBtnYesterDay_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBtnYesterDay.Checked == true)
            {
                ChkBtnThisMonth.Checked = false;
                ChkBtnThisWeek.Checked = false;
                ChkBtnToday.Checked = false;

                DTPSearchFromDate.Text = DateTime.Now.AddDays(-1).ToString();
                DTPSearchToDate.Text = DateTime.Now.AddDays(-1).ToString();
            }

        }

        private void ChkBtnThisWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBtnThisWeek.Checked == true)
            {
                ChkBtnThisMonth.Checked = false;
                ChkBtnYesterDay.Checked = false;
                ChkBtnToday.Checked = false;

                DateTime baseDate = DateTime.Today;

                var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
                var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
                var lastWeekStart = thisWeekStart.AddDays(-7);
                var lastWeekEnd = thisWeekStart.AddSeconds(-1);

                DTPSearchFromDate.Text = Val.ToString(baseDate.AddDays(-(int)baseDate.DayOfWeek));
                DTPSearchToDate.Text = Val.ToString(DateTime.Parse(DTPSearchFromDate.Text).AddDays(7).AddSeconds(-1));

                //DTPSearchFromDate.Text = Val.ToString(thisWeekStart.AddDays(-7));
                //DTPSearchFromDate.Text = Val.ToString(DateTime.Parse(DTPSearchFromDate.Text).AddSeconds(-1));

            }

        }

        private void ChkBtnThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBtnThisMonth.Checked == true)
            {
                ChkBtnThisWeek.Checked = false;
                ChkBtnYesterDay.Checked = false;
                ChkBtnToday.Checked = false;

                DateTime baseDate = DateTime.Today;
                DTPSearchFromDate.Text = Val.ToString(baseDate.AddDays(1 - baseDate.Day));
                DTPSearchToDate.Text = Val.ToString(DateTime.Parse(DTPSearchFromDate.Text).AddMonths(1).AddSeconds(-1));
            }

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                Int64 IntLedger_ID = 0;
                string StrOpe = "";
                IntLedger_ID = BOConfiguration.gEmployeeProperty.LEDGER_ID;

                if (RdbMyStaff.Checked)
                    StrOpe = "MTSTAFF";
                else if (RdbSelf.Checked)
                    StrOpe = "SELF";
                else if (RdbAll.Checked)
                    StrOpe = "ALLEMP";

                DtabDashboard.Rows.Clear();

                DataTable Dt = ObjDashboard.GetAdminDashboardData(Val.SqlDate(DTPSearchFromDate.Text), Val.SqlDate(DTPSearchToDate.Text), Val.ToInt64(IntLedger_ID),StrOpe);

                if (RdbSelf.Checked)
                {
                    DataRow[] DrTotEmp = Dt.Select("LEDGER_ID = '" + Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID) + "'");
                    if (DrTotEmp.Length > 0)
                    {
                        DtabDashboard = DrTotEmp.CopyToDataTable();
                    }
                }
                else
                {
                    //DataRow[] DrTotEmp = Dt.Select("LEDGER_ID <> '" + Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID) + "'");
                    //if (DrTotEmp.Length > 0)
                    //{
                    //    DtabDashboard = DrTotEmp.CopyToDataTable();
                    //}
                    DtabDashboard = Dt;
                }


                if (DtabDashboard.Rows.Count > 0)
                {
                    DataRow[] DrTotEmp = DtabDashboard.Select("ATDSTATUS = '' AND LEAVESTATUS = ''");
                    if (DrTotEmp.Length != 0)
                    {
                        lblTotalEmployees.Text = DrTotEmp.Length.ToString();
                    }

                    DataRow[] DrTotAbsent = DtabDashboard.Select("ATDSTATUS = 'A'");
                    if (DrTotAbsent.Length != 0)
                    {
                        lblTotalAbsent.Text = DrTotAbsent.Length.ToString();
                    }

                    DataRow[] DrTotPresent = DtabDashboard.Select("ATDSTATUS IN ('P','H','S')");
                    if (DrTotPresent.Length != 0)
                    {
                        lblTotalPresent.Text = DrTotPresent.Length.ToString();
                    }

                    DataRow[] DtTotLeavePen = DtabDashboard.Select("LEAVESTATUS = 'PENDING'");
                    if (DtTotLeavePen.Length != 0)
                    {
                        lblTotalPendingLeave.Text = DtTotLeavePen.Length.ToString();
                    }
                }

                //MainGrid.DataSource = DtabDashboard;
                //MainGrid.Refresh();

                PnlEmployee_Click(null, null);


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        public void Clear()
        {
            lblTotalEmployees.Text = "0";
            lblTotalAbsent.Text = "0";
            lblTotalPresent.Text = "0";
            lblTotalPendingLeave.Text = "0";
        }

        private void PnlTotPresent_Click(object sender, EventArgs e)
        {
            try
            {
                PnlTotPresent.BackColor = Color.LightCyan;
                PnlTotAbsent.BackColor = Color.White;
                PnlEmployee.BackColor = Color.White;
                PnlPendingLeave.BackColor = Color.White;

                if (DtabDashboard.Rows.Count <= 0)
                    return;

                this.Cursor = Cursors.WaitCursor;

                DataRow[] DrTotPresent = DtabDashboard.Select("ATDSTATUS IN ('P','H','S')");
                if (DrTotPresent.Length != 0)
                {
                    lblTotalPresent.Text = DrTotPresent.Length.ToString();

                    MainGrid.DataSource = DrTotPresent.CopyToDataTable();
                    MainGrid.Refresh();

                    GrdDet.Columns["ATDDATE"].Visible = true;
                    GrdDet.Columns["DETAIL"].Visible = false;
                    GrdDet.Columns["ATDSTATUS"].Visible = true;
                    GrdDet.Columns["LEAVESTATUS"].Visible = false;
                    GrdDet.Columns["WHOURS"].Visible = true;
                    
                    SetGrdColumnsVisibleIndex();
                }
                else
                {
                    MainGrid.DataSource = null;
                    MainGrid.Refresh();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }

        }

        private void PnlTotAbsent_Click(object sender, EventArgs e)
        {
            try
            {
                PnlTotAbsent.BackColor = Color.LightCyan;
                PnlEmployee.BackColor = Color.White;
                PnlTotPresent.BackColor = Color.White;
                PnlPendingLeave.BackColor = Color.White;

                if (DtabDashboard.Rows.Count <= 0)
                    return;

                this.Cursor = Cursors.WaitCursor;

                DataRow[] DrTotAbsent = DtabDashboard.Select("ATDSTATUS = 'A'");
                if (DrTotAbsent.Length != 0)
                {
                    lblTotalAbsent.Text = DrTotAbsent.Length.ToString();

                    MainGrid.DataSource = DrTotAbsent.CopyToDataTable();
                    MainGrid.Refresh();

                    GrdDet.Columns["ATDDATE"].Visible = true;
                    GrdDet.Columns["DETAIL"].Visible = false;
                    GrdDet.Columns["ATDSTATUS"].Visible = true;
                    GrdDet.Columns["LEAVESTATUS"].Visible = false;
                    GrdDet.Columns["WHOURS"].Visible = true;
                    

                    SetGrdColumnsVisibleIndex();
                }
                else
                {
                    MainGrid.DataSource = null;
                    MainGrid.Refresh();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void PnlPendingLeave_Click(object sender, EventArgs e)
        {
            try
            {
                PnlPendingLeave.BackColor = Color.LightCyan;
                PnlEmployee.BackColor = Color.White;
                PnlTotPresent.BackColor = Color.White;
                PnlTotAbsent.BackColor = Color.White;

                if (DtabDashboard.Rows.Count <= 0)
                    return;

                this.Cursor = Cursors.WaitCursor;

                DataRow[] DtTotLeavePen = DtabDashboard.Select("LEAVESTATUS = 'PENDING'");
                if (DtTotLeavePen.Length != 0)
                {
                    lblTotalPendingLeave.Text = DtTotLeavePen.Length.ToString();

                    MainGrid.DataSource = DtTotLeavePen.CopyToDataTable();
                    MainGrid.Refresh();


                    GrdDet.Columns["ATDDATE"].Visible = true;
                    ObjPer.GetFormPermission(Val.ToString("LeaveApproval"));
                    GrdDet.Columns["DETAIL"].Visible = ObjPer.ISVIEW;
                    GrdDet.Columns["ATDSTATUS"].Visible = false;
                    GrdDet.Columns["LEAVESTATUS"].Visible = true;
                    GrdDet.Columns["WHOURS"].Visible = true;

                    
                    ObjPer.GetFormPermission(Val.ToString("LeaveApproval"));
                    GrdDet.Columns["DETAIL"].Visible = ObjPer.ISVIEW;
                    
                    SetGrdColumnsVisibleIndex();
                }
                else
                {
                    MainGrid.DataSource = null;
                    MainGrid.Refresh();
                }
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
                
            }

        }



        private void PnlEmployee_Click(object sender, EventArgs e)
        {

            try
            {
                PnlEmployee.BackColor = Color.LightCyan;
                PnlTotPresent.BackColor = Color.White;
                PnlTotAbsent.BackColor = Color.White;
                PnlPendingLeave.BackColor = Color.White;

                if (DtabDashboard.Rows.Count <= 0)
                    return;

                this.Cursor = Cursors.WaitCursor;

                DataRow[] DrTotEmp = DtabDashboard.Select("ATDSTATUS = '' AND LEAVESTATUS = ''");
                if (DrTotEmp.Length != 0)
                {
                    lblTotalEmployees.Text = DrTotEmp.Length.ToString();

                    MainGrid.DataSource = DrTotEmp.CopyToDataTable();
                    MainGrid.Refresh();

                    GrdDet.Columns["ATDDATE"].Visible = false;
                    GrdDet.Columns["DETAIL"].Visible = false;
                    GrdDet.Columns["ATDSTATUS"].Visible = false;
                    GrdDet.Columns["LEAVESTATUS"].Visible = false;
                    GrdDet.Columns["WHOURS"].Visible = false;
                    

                    SetGrdColumnsVisibleIndex();
                }
                else
                {
                    MainGrid.DataSource = null;
                    MainGrid.Refresh();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        public void SetGrdColumnsVisibleIndex()
        {
            GrdDet.Columns["ATDDATE"].VisibleIndex = GrdDet.Columns["ATDDATE"].Visible == true ? 1 : -1;
            GrdDet.Columns["LEDGERCODE"].VisibleIndex = 2;
            GrdDet.Columns["LEDGERNAME"].VisibleIndex = 3;
            GrdDet.Columns["EMPTYPE"].VisibleIndex = 4;
            GrdDet.Columns["DEPARTMENTNAME"].VisibleIndex = 5;
            GrdDet.Columns["DESIGNATIONNAME"].VisibleIndex = 6;
            GrdDet.Columns["MANAGERCODE"].VisibleIndex = 7;
            
            GrdDet.Columns["ATDSTATUS"].VisibleIndex = GrdDet.Columns["ATDSTATUS"].Visible == true ? 9 : -1;
            GrdDet.Columns["WHOURS"].VisibleIndex = GrdDet.Columns["WHOURS"].Visible == true ? 10 : -1;
            GrdDet.Columns["LEAVESTATUS"].VisibleIndex = GrdDet.Columns["LEAVESTATUS"].Visible == true ? 11 : -1;
                       
            GrdDet.Columns["LEAVE_ID"].Visible = false;
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGrid) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGrid.GetViewAt(e.ControlMousePosition) as GridView;
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

        private void repBtnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                string StrLeave_ID = Val.ToString(GrdDet.GetFocusedRowCellValue("LEAVE_ID"));

                FrmLeaveApproval FrmLeaveApproval = new FrmLeaveApproval();
                //FrmLeaveApproval.MdiParent = Global.gMainRef;
                FrmLeaveApproval.FormClosing += new FormClosingEventHandler(FrmLeaveApproval_FormClosing);
                FrmLeaveApproval.ShowForm(StrLeave_ID);
                PnlPendingLeave_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
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
                    BtnRefreshRestReport_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void RdbMyStaff_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Global.ExcelExport("Idle Employee's List", GrdDet);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnRefreshRestReport_Click(object sender, EventArgs e)
        {
            try
            {
                string StrEmp_ID = "";

                StrEmp_ID = Val.Trim(ChkCmbEmployee.Properties.GetCheckedItems());
                DataTable DtIdle = ObjDashboard.GetEmployeeIdleReport(StrEmp_ID);

                MainGrdIdle.DataSource = DtIdle;
                MainGrdIdle.Refresh();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
    }
}
