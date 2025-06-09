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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using AxoneMFGRJ.Utility;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmParcelTransactionView : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjIssRet = new BOTRN_SingleIssueReturn();
        BOFormPer ObjPer = new BOFormPer();
        BODevGridSelection ObjGridSelection;
        DataTable DtabPacket = new DataTable();
        DataTable  DTabPacketLiveStock = new DataTable();
        DataTable DTab = new DataTable();

        System.Diagnostics.Stopwatch watch = null;
        string StrFromDate = null;
        string StrToDate = null;

        string mStrPassward = "";

        #region Property Settings

        public FrmParcelTransactionView()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            //mStrPassward = ObjPer.PASSWORD;
            txtPassForDisplayBack_TextChanged(null, null);

            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);

            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDet.RestoreLayoutFromStream(stream);
            }

            if (MainGrid.RepositoryItems.Count == 6)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDet;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdDet.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
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
             ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTPFromDate.Checked == true && DTPToDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }
                if (backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.CancelAsync();
                }
                watch = System.Diagnostics.Stopwatch.StartNew();
                PanelProgress.Visible = true;
                backgroundWorker1.RunWorkerAsync();
                BtnSearch.Enabled = false;
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
                PanelProgress.Visible = true;
                backgroundWorker1.CancelAsync();
            }
            
        }


        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
            //else
            if (e.KeyCode == Keys.F5)
            {
                BtnSearch.PerformClick();
            }
         
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (Val.ToString(GrdDet.GetRowCellValue(e.RowHandle,"LIVE")) == "LIVE")
            {
                e.Appearance.BackColor = Color.LightGray;
                e.Appearance.BackColor2 = Color.LightGray;
            }
            else
            {
               e.Appearance.BackColor = Color.White;
                e.Appearance.BackColor2 = Color.White;
            }
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void txtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.Transaction.BOTRN_SinglePacketCreate().FindKapan();

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapan.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtKapan.Text = string.Empty;
            txtFromPacketNo.Text = string.Empty;
            txtToPacketNo.Text = string.Empty;
            txtTag.Text = string.Empty;
            DTPFromDate.Checked = false;
            DTPToDate.Checked = false;
            ChkCurrentTransaction.Checked = false;
            txtKapan.Focus();    
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("TransactionView.xlsx", GrdDet);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle < 0)
            {
                return;
            }
            if (Global.Confirm("Do You Want To Delete This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //if (Val.ToString(GrdDet.GetFocusedRowCellValue("PACKETTYPE")) == "MIX")
            //{
            //    Global.Message("You Can't Delete This Transaction");
            //    return;
            //}

            TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

            Property.TRN_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("TRN_ID"));
            Property.PACKET_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("PACKET_ID"));
            //string pStrPacketType = Val.ToString(GrdDet.GetFocusedRowCellValue("PACKETTYPE"));

            Property = ObjIssRet.DeleteParcelTransaction(Property);

            Global.Message(Property.ReturnMessageDesc);

            if (Property.ReturnMessageType == "SUCCESS")
            {
                GrdDet.DeleteRow(GrdDet.FocusedRowHandle);
                GrdDet.RefreshData();
            }
            
        }

        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
            if (Val.ToString(txtPassForDisplayBack.Tag) != "" && Val.ToString(txtPassForDisplayBack.Tag).ToUpper() == txtPassForDisplayBack.Text.ToUpper())
            {
                ChkCurrentTransaction.Visible = true;

                GrdDet.Columns["FROMPROCESSNAME"].OptionsColumn.AllowEdit = true;
                GrdDet.Columns["TOPROCESSNAME"].OptionsColumn.AllowEdit = true;
                GrdDet.Columns["UPDATE"].OptionsColumn.AllowEdit = true;

                //GrdDet.Columns["ISSUECARAT"].OptionsColumn.AllowEdit = true;
                //GrdDet.Columns["READYCARAT"].OptionsColumn.AllowEdit = true;
                BtnUpdate.AllowFocused = true;
            }
            else
            {
                ChkCurrentTransaction.Visible = false;
                ChkCurrentTransaction.Checked = false;

                GrdDet.Columns["FROMPROCESSNAME"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["TOPROCESSNAME"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["UPDATE"].OptionsColumn.AllowEdit = false;

                GrdDet.Columns["ISSUECARAT"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["READYCARAT"].OptionsColumn.AllowEdit = false;

                BtnUpdate.AllowFocused = false;
            }
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
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FROMEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FROMEMPLOYEENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TOEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOEMPLOYEENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FROMMANAGERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FROMMANAGERNAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TOMANAGERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOMANAGERNAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MARKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "MARKERNAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TRANSBYCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TRANSBYNAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "CONFBYCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "CONFBYNAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "ISSUECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "ISSUENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "PREVISSUECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "PREVISSUENAME")));
                    return;
                }
            }
            finally
            {
                e.Info = info;
            }
        }

        private void lblSaveLayout_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdDet.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDet.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblDefaultLayout_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDet.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }           
        }

        private void txtFromProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("FROMPROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                        GrdDet.SetFocusedRowCellValue("FROMPROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
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

        private void txtToProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("TOPROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                        GrdDet.SetFocusedRowCellValue("TOPROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));

                        GrdDet.SetFocusedRowCellValue("NEXTPROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                        GrdDet.SetFocusedRowCellValue("NEXTPROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
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
        
        private void txtNextProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("NEXTPROCESSNAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                        GrdDet.SetFocusedRowCellValue("NEXTPROCESS_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
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

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            //if (GrdDet.FocusedRowHandle < 0 || Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID) != "1520162494440")
            //{
            //    return;
            //}
            //if (Global.Confirm("Do You Want To Update Process This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
            //{
            //    return;
            //}

            //TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

            //Property.TRN_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("TRN_ID"));
            //Property.PACKET_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("PACKET_ID"));

            //Property.FROMPROCESS_ID = Val.ToInt(GrdDet.GetFocusedRowCellValue("FROMPROCESS_ID"));
            //Property.TOPROCESS_ID = Val.ToInt(GrdDet.GetFocusedRowCellValue("TOPROCESS_ID"));
            //Property.NEXTPROCESS_ID = Val.ToInt(GrdDet.GetFocusedRowCellValue("NEXTPROCESS_ID"));

            //Property.ISSUECARAT = Val.Val(GrdDet.GetFocusedRowCellValue("ISSUECARAT"));
            //Property.READYCARAT = Val.Val(GrdDet.GetFocusedRowCellValue("READYCARAT"));
            //Property.LOSSCARAT = Val.Val(GrdDet.GetFocusedRowCellValue("LOSSCARAT"));

            //Property = ObjIssRet.UpdateTransaction(Property);

            //Global.Message(Property.ReturnMessageDesc);

            //if (Property.ReturnMessageType == "SUCCESS")
            //{
            //    GrdDet.RefreshData();
            //}
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName.ToString().ToUpper())
            {
                case "ISSUECARAT":
                case "READYCARAT":
                    double DouIssueCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "ISSUECARAT"));
                    double DouReadyCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "READYCARAT"));
                    double DouSecondCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "SECONDCARAT"));
                    double DouExtraCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "EXTRACARAT"));
                    double DouLoss = Math.Round(DouIssueCarat - DouReadyCarat - DouSecondCarat - DouExtraCarat, 3);
                    GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", DouLoss);
                    break;
                default:
                    break;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DTabPacketLiveStock = ObjIssRet.GetTransactionViewDataForParcel(txtKapan.Text,
                    "",
                    "",
                    Val.ToInt(txtFromPacketNo.Text),
                    Val.ToInt(txtToPacketNo.Text),
                    txtTag.Text,
                    StrFromDate,
                    StrToDate,
                    ChkCurrentTransaction.Checked,
                    Val.Trim(ChkCmbDepartment.Properties.GetCheckedItems())
                    );
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MainGrid.DataSource = DTabPacketLiveStock;
                MainGrid.Refresh();

                GrdDet.BestFitMaxRowCount = 500;
                GrdDet.BestFitColumns();

                GrdDet.Columns["DELETE"].Visible = ChkCurrentTransaction.Checked;
                if (ChkCurrentTransaction.Checked)
                {
                    BtnDlt.Visible = true;
                }
                else
                {
                    BtnDlt.Visible = false;
                }
                ObjGridSelection.ClearSelection();

                watch.Stop();
                lblTime.Text = string.Format("{0:hh\\:mm\\:ss}", watch.Elapsed);

                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
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

        private void BtnDlt_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                DTab = GetTableOfSelectedRows(GrdDet, true);
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (DTab == null || DTab.Rows.Count == 0)
                    {
                        Global.Message("Please Select Atleast One Record For Delete");
                        return;
                    }

                    if (DTab.Rows.Count > 1)
                    {
                        DataRow[] Dr = DTab.Select("SECONDPCS <> 0 OR EXTRAPCS <> 0");
                        if (Dr.Length > 0)
                        {
                            Global.MessageError("Please Select Single-Single Row For Return Transaction Which Has Second And Extra Detail");
                            return;
                        }
                    }

                    if (Global.Confirm("Do You Want To Delete This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }                    

                    this.Cursor = Cursors.WaitCursor;

                    foreach (DataRow Dr in DTab.Rows)
                    {
                        if (Val.ToString(Dr["LIVE"]) == "LIVE")
                        {
                            TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

                            Property.TRN_ID = Val.ToInt64(Dr["TRN_ID"]);
                            Property.PACKET_ID = Val.ToInt64(Val.ToString(Dr["PACKET_ID"]));
                            //string pStrPacketType = Val.ToString(Dr["PACKETTYPE"]);

                            Property = ObjIssRet.DeleteParcelTransaction(Property);
                            ReturnMessageDesc = Property.ReturnMessageDesc;
                            ReturnMessageType = Property.ReturnMessageType;
                            Property = null;
                        }
                    }
                    DTabPacketLiveStock.AcceptChanges();
                    Global.Message(ReturnMessageDesc);

                    if (ReturnMessageType == "SUCCESS")
                    {
                        BtnSearch_Click(null, null);
                        if (GrdDet.RowCount > 1)
                        {
                            GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                    DTab.Rows.Clear();
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }
        }

        private void ChkCurrentTransaction_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCurrentTransaction.Checked == true)
            {
                GrdDet.Columns["LOSSCARAT"].OptionsColumn.AllowEdit = true;
            }
            else
            {
                GrdDet.Columns["LOSSCARAT"].OptionsColumn.AllowEdit = false;
            }
        }

        private void RepTxtLossCarat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDet.PostEditor();
                    if (GrdDet.FocusedRowHandle < 0)
                    {
                        return;
                    }
                    if (Global.Confirm("Do You Want To Update Loss Carat For This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

                    Property.PACKET_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("PACKET_ID"));
                    Property.TRN_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("TRN_ID"));
                    Property.LOSSCARAT = Val.Val(GrdDet.GetFocusedRowCellValue("LOSSCARAT"));

                    Property = ObjIssRet.UpdateLossCarat(Property);
                    Global.Message(Property.ReturnMessageDesc);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        GrdDet.RefreshData();
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
