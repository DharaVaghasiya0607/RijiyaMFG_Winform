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
using AxoneMFGRJ.Parcel;
using AxoneMFGRJ.Report;
using AxoneMFGRJ.Masters;
using AxoneMFGRJ.Utility;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleKapanDashboard : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        BODevGridSelection ObjGridSelection;

        DataTable DtabPacket = new DataTable();
        DataTable DTabPacketLiveStock = new DataTable();

        #region Property Settings

        public FrmSingleKapanDashboard()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
            //  BtnSearch_Click(null, null);

            CmbKapanStatus.Properties.Items["PENDING"].CheckState = CheckState.Checked;
            CmbKapanStatus.Properties.Items["RUNNING"].CheckState = CheckState.Checked;
            
            BtnSearch.PerformClick();
        }

        public void ShowForm(string pStr)
        {
            this.Text = "PROCESS RETURN";
            this.Name = "FrmProcessReturn";
            panel4.Visible = false;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
            //  BtnSearch_Click(null, null);

            CmbKapanStatus.Properties.Items["PENDING"].CheckState = CheckState.Checked;
            CmbKapanStatus.Properties.Items["RUNNING"].CheckState = CheckState.Checked;

        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion


        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }
            //if (e.KeyCode == Keys.F5)
            //{
            //    BtnSearch_Click(null, null);
            //}
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDetKapanLive.BeginUpdate();
            DataTable DTabKapanLiveStock = ObjKapan.GetDataForSingleKapanLiveStock(Val.Trim(CmbKapanStatus.Properties.GetCheckedItems()),0);
            MainGridKapanLive.DataSource = DTabKapanLiveStock;
            MainGridKapanLive.Refresh();
            GrdDetKapanLive.BestFitColumns();
            GrdDetKapanLive.EndUpdate();
            this.Cursor = Cursors.Default;
        }


        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
        }

        private void repBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetKapanLive.FocusedRowHandle < 0)
                {
                    return;
                }
                else
                {
                    if (Global.Confirm("Are You Sure To Delete this Kapan ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnKapanCreateProperty Property = new TrnKapanCreateProperty();
                    Property.Ope = "DELETE";
                    Property.KAPAN_ID = Val.ToInt64(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID"));

                    Property = ObjKapan.EditKapan(Property);

                    Global.Message(Property.ReturnMessageDesc);

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        GrdDetKapanLive.DeleteRow(GrdDetKapanLive.FocusedRowHandle);
                    }

                }

            }
            catch
            {

            }
        }

        private void repBtnKapanProcIssue_Click(object sender, EventArgs e)
        {
            try
            {
                string StrKapanName = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANNAME"));
                string StrOwner = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("MANAGERNAME"));
                Int64 IntKapan_ID = Val.ToInt64(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID"));
                Int64 IntManager_ID = Val.ToInt64(GrdDetKapanLive.GetFocusedRowCellValue("MANAGER_ID"));
                string StrManager = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("MANAGERNAME"));
                string StrKapanCategory = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANCATEGORY"));
                
                FrmSinglePacketCreate FrmSinglePacketCreate = new FrmSinglePacketCreate();
                FrmSinglePacketCreate.MdiParent = Global.gMainRef;
                FrmSinglePacketCreate.Tag = "PacketCreate";
                FrmSinglePacketCreate.ShowForm(StrKapanName, StrManager,IntManager_ID, IntKapan_ID, StrKapanCategory);
                FrmSinglePacketCreate.FormClosing += new FormClosingEventHandler(Form_Closing);
            }
            catch
            {

            }
        }

        private void repBtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetKapanLive.FocusedRowHandle < 0)
                {
                    return;
                }
                else
                {
                    if (Global.Confirm("Are You Sure To Update Status Of this Kapan ?") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    //// Check Validation : For Kapan Create with Sublot Sequence Or Not : 18-07-2019
                    //TrnKapanCreateProperty PropertyChk = new TrnKapanCreateProperty();
                    //PropertyChk.KAPAN_ID = Guid.Parse(Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID")));
                    //PropertyChk.KAPANNAME = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANNAME"));
                    //PropertyChk.SUBLOT = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("SUBLOT"));
                    //PropertyChk.SUBLOT1 = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("SUBLOT1"));
                    //PropertyChk = ObjKapan.CheckValSaveKapanWithSublot(PropertyChk);
                    //if (PropertyChk.ReturnMessageType == "FAIL")
                    //{
                    //    Global.Message(PropertyChk.ReturnMessageDesc);
                    //    PropertyChk = null;
                    //    return;
                    //}
                    //PropertyChk = null;
                    ////End : 18-07-2019


                    TrnKapanCreateProperty Property = new TrnKapanCreateProperty();
                    Property.Ope = "UPDATE";
                    Property.KAPAN_ID = Val.ToInt64(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID"));
                    Property.STATUS = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("STATUS"));
                    Property.KAPANPCS = Val.ToInt32(GrdDetKapanLive.GetFocusedRowCellValue("KAPANPCS"));
                    Property.KAPANCARAT = Val.Val(GrdDetKapanLive.GetFocusedRowCellValue("KAPANCARAT"));
                    Property.KAPANNAME = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANNAME"));
                    Property.KAPANGROUP = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANGROUP"));
                    Property.MANAGER_ID = Val.Bigint(GrdDetKapanLive.GetFocusedRowCellValue("MANAGER_ID"));
                    Property.ISHIDE = Val.ToBoolean(GrdDetKapanLive.GetFocusedRowCellValue("ISHIDE"));
                    Property.ISNOTAPPLYANYLOCK = Val.ToBoolean(GrdDetKapanLive.GetFocusedRowCellValue("ISNOTAPPLYANYLOCK"));
                    Property.LABOURAMOUNT = Val.Val(GrdDetKapanLive.GetFocusedRowCellValue("LABOURAMOUNT"));
                    Property.DIAMONDTYPE = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("DIAMONDTYPE"));

                    if (Val.IsDate(Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("COMPLETEDATE"))) == true)
                    {
                        Property.COMPLETEDATE = Val.SqlDate(Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("COMPLETEDATE")));
                    }
                    else
                    {
                        Property.COMPLETEDATE = null;
                    }

                    Property = ObjKapan.EditKapan(Property);

                    Global.Message(Property.ReturnMessageDesc);

                    GrdDetKapanLive.RefreshData();

                    BtnSearch_Click(null, null);

                }

            }
            catch
            {

            }
        }

        private void BtnKapanLiveStockAutoFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDetKapanLive.BestFitColumns();
            this.Cursor = Cursors.Default;
        }


        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("KapanLiveStock", GrdDetKapanLive);
        }

        private void BtnRejection_Click(object sender, EventArgs e)
        {
            try
            {
                string StrKapanName = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANNAME"));
                string KapanCarat = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANCARAT"));

                string StrKapan_ID = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID"));

                FrmRejectionTransfer FrmRejectionTransfer = new FrmRejectionTransfer();
                FrmRejectionTransfer.MdiParent = Global.gMainRef;
                FrmRejectionTransfer.ShowForm(StrKapan_ID, StrKapanName, KapanCarat, "KAPAN");
                FrmRejectionTransfer.FormClosing += new FormClosingEventHandler(Form_Closing);
            }
            catch
            {

            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            BtnSearch_Click(null, null);
        }

        private void GrdDetKapanLive_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (e.Clicks == 2)
            {
                TrnKapanCreateProperty KapanProperty = new TrnKapanCreateProperty();
                KapanProperty.KAPAN_ID = Val.ToInt64(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID"));

                if (e.Column.FieldName == "REJECTIONCARAT")
                {
                    DataTable DtData = ObjKapan.GetRejectionData("KAPAN", KapanProperty);

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    // FrmPopupGrid.DTab = DtData;                   
                    FrmPopupGrid.CountedColumn = "REJECTIONNAME";
                    FrmPopupGrid.SummrisedColumn = "PCS,CARAT,AMOUNT";
                    FrmPopupGrid.ColumnsToHide = "REJECTION_ID";
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Rejection List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;

                    FrmPopupGrid.Width = 1000;
                    //FrmPopupGrid.Size = this.Size;

                    FrmPopupGrid.GrdDet.Columns["REJECTIONNAME"].Caption = "Rejection";
                    FrmPopupGrid.GrdDet.Columns["PCS"].Caption = "Pcs";
                    FrmPopupGrid.GrdDet.Columns["CARAT"].Caption = "Carat";
                    FrmPopupGrid.GrdDet.Columns["REJECTIONDATE"].Caption = "Rej. Date";
                    FrmPopupGrid.GrdDet.Columns["RATE"].Caption = "Rate";
                    FrmPopupGrid.GrdDet.Columns["AMOUNT"].Caption = "Amount";
                    FrmPopupGrid.GrdDet.Columns["REMARK"].Caption = "Remark";

                    FrmPopupGrid.GrdDet.Columns["REJECTIONNAME"].Width = 100;
                    FrmPopupGrid.GrdDet.Columns["REJECTIONDATE"].Width = 100;
                    FrmPopupGrid.GrdDet.Columns["REMARK"].Width = 150;
                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
            }
        }


        private void GrdDetKapanLive_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            string Status = GrdDetKapanLive.GetRowCellValue(e.RowHandle, "STATUS").ToString();
            if (Status == "PENDING")
            {
                e.Appearance.BackColor = Color.Transparent;
            }
            else if (Status == "RUNNING")
            {
                e.Appearance.BackColor = lblRunning.BackColor;
            }
            else if (Status == "COMPLETE")
            {
                e.Appearance.BackColor = lblComplete.BackColor;
            }
        }

        private void txtSubLot_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "SUBLOTCODE,SUBLOTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SUBLOT);
                    FrmSearch.mColumnsToHide = "SUBLOT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetKapanLive.SetFocusedRowCellValue("SUBLOT", Val.ToString(FrmSearch.mDRow["SUBLOTNAME"]));
                    }
                    else
                    {
                        GrdDetKapanLive.SetFocusedRowCellValue("SUBLOT", "");
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

        private void txtSubLot1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetKapanLive.SetFocusedRowCellValue("MANAGER_ID", Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]));
                        GrdDetKapanLive.SetFocusedRowCellValue("MANAGERNAME", Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]));
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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrdDetKapanLive_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e) //Add : Pinali : 30-04-2019 For Repairing Kapan(Couldn't update Kapan Name)
        {
            try
            {
                if (GrdDetKapanLive.FocusedRowHandle < 0)
                    return;

                DataRow Dr = GrdDetKapanLive.GetFocusedDataRow();

                //Add : Pinali : 24-04-2019
                if (Val.ToString(Dr["KAPANNAME"]).Trim().Equals("REP"))
                {
                    GrdDetKapanLive.Columns["KAPANNAME"].OptionsColumn.AllowEdit = false;
                    GrdDetKapanLive.Columns["DELETE"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    GrdDetKapanLive.Columns["KAPANNAME"].OptionsColumn.AllowEdit = false;
                    //GrdDetKapanLive.Columns["KAPANNAME"].OptionsColumn.AllowEdit = true;
                    GrdDetKapanLive.Columns["DELETE"].OptionsColumn.AllowEdit = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDetKapanLive_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (GrdDetKapanLive.FocusedRowHandle < 0)
                    return;

                DataRow Dr = GrdDetKapanLive.GetFocusedDataRow();

                //Add : Pinali : 24-04-2019
                if (Val.ToString(Dr["KAPANNAME"]).Trim().Equals("REP"))
                {
                    GrdDetKapanLive.Columns["KAPANNAME"].OptionsColumn.AllowEdit = false;
                    GrdDetKapanLive.Columns["DELETE"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    GrdDetKapanLive.Columns["KAPANNAME"].OptionsColumn.AllowEdit = false;
                    //GrdDetKapanLive.Columns["KAPANNAME"].OptionsColumn.AllowEdit = true;
                    GrdDetKapanLive.Columns["DELETE"].OptionsColumn.AllowEdit = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void ReptxtMixPktCreate_Click(object sender, EventArgs e) // Add By Dhara : 18-04-22
        {
            try
            {
                string StrKapanName = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANNAME"));
                string StrOwner = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("MANAGERNAME"));
                Int64 IntKapan_ID = Val.ToInt64(GrdDetKapanLive.GetFocusedRowCellValue("KAPAN_ID"));
                Int64 IntManager_ID = Val.ToInt64(GrdDetKapanLive.GetFocusedRowCellValue("MANAGER_ID"));
                string StrManager = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("MANAGERNAME"));
                string StrKapanCategory = Val.ToString(GrdDetKapanLive.GetFocusedRowCellValue("KAPANCATEGORY"));
                double pDouPktCts = Val.Val(GrdDetKapanLive.GetFocusedRowCellValue("MIXCARAT"));
                Int32 pIntPktPcs = Val.ToInt32(GrdDetKapanLive.GetFocusedRowCellValue("MIXPCS"));

                FrmMixPacketCreate FrmMixPacketCreate = new FrmMixPacketCreate();
                FrmMixPacketCreate.MdiParent = Global.gMainRef;
                FrmMixPacketCreate.Tag = "PacketCreate";
                FrmMixPacketCreate.ShowForm(StrKapanName, StrManager, IntManager_ID, IntKapan_ID, StrKapanCategory, pDouPktCts, pIntPktPcs);
                FrmMixPacketCreate.FormClosing += new FormClosingEventHandler(Form_Closing);
            }
            catch
            {

            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = ObjKapan.GetDataForSingleKapanLiveStock(Val.Trim(CmbKapanStatus.Properties.GetCheckedItems()), 0);
                if (DTab.Rows.Count == 0)
                {
                    Global.MessageError("There Is No Data For Print");
                    return;
                }
                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowWithPrint("KapanDashboardPrint", DTab);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void btnKapanValuation_Click(object sender, EventArgs e)
        {
            //ObjGridSelection = new BODevGridSelection();
            //ObjGridSelection.View = GrdDetKapanLive;
            
            //this.Cursor = Cursors.WaitCursor;
            //DataTable DtKapanDetail = new DataTable();
            
            //    DtKapanDetail = Global.GetSelectedRecordOfGrid(GrdDetKapanLive, true, ObjGridSelection);
            //    if (DtKapanDetail == null)
            //    {
            //        return;
            //    }
            if (GrdDetKapanLive.FocusedRowHandle < 0)
                return;

            DataRow Dr = GrdDetKapanLive.GetFocusedDataRow();
           /* this.Cursor = Cursors.WaitCursor;
            //DataTable DtKapanDetail = Global.GetSelectedRecordOfGrid(GrdDetKapan, false, ObjGridSelection);
            DataTable DtKapanDetail = new DataTable();
            if (GrdDetKapanLive.DataSource != null)
                DtKapanDetail = ((DataView)GrdDetKapanLive.DataSource).ToTable();*/

            this.Cursor = Cursors.Default;
            FrmKapanValuation FrmKapanValuation = new FrmKapanValuation();
            FrmKapanValuation.MdiParent = Global.gMainRef;
            FrmKapanValuation.ShowForm(Dr);
           // FrmKapanValuation.FormClosing += new FormClosingEventHandler(FrmRoughPurchaseMixSplit_FormClosing);
        }
    }
}
