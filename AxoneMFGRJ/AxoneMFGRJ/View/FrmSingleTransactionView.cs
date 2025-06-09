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
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AxoneMFGRJ.View
{
    public partial class FrmSingleTransactionView : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjIssRet = new BOTRN_SingleIssueReturn();

        BOFormPermission ObjPer = new BOFormPermission();

        DataTable DtabPacket = new DataTable();
        DataTable DTabPacketLiveStock = new DataTable();
        DataTable DTab = new DataTable();
        BODevGridSelection ObjGridSelection;

        string StrFromDate = null;
        string StrToDate = null;
        string pStrTransaction = null;
        string StrBarcode = null;
        string StrKapanName = null;
        string StrFromPacketNo = null;
        string StrToPacketNo = null;
        string StrJangedNo = null;
        string StrTag = null;
        bool BlnCurrentTrn = false;

        #region Property Settings

        public FrmSingleTransactionView()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
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
            RbtBarcode_CheckedChanged(null, null);

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

            ObjPer.GetPermission(this);
        }

        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                StrFromDate = null;
                StrToDate = null;
                if (Val.ToBoolean(DTPFromDate.Checked) == true && Val.ToBoolean(DTPToDate.Checked) == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                StrBarcode = txtBarcode.Text;
                StrKapanName = txtKapan.Text;
                StrFromPacketNo = txtFromPacketNo.Text;
                StrToPacketNo = txtToPacketNo.Text;
                StrTag = txtTag.Text;
                StrJangedNo = txtJangedNo.Text;
                BlnCurrentTrn = ChkCurrentTransaction.Checked;
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                BtnSearch.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

            //this.Cursor = Cursors.WaitCursor;

            //string StrFromDate = null;
            //string StrToDate = null;
            //if (DTPFromDate.Checked == true && DTPToDate.Checked == true)
            //{
            //    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
            //    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
            //}

            //DTabPacketLiveStock = ObjIssRet.GetTransactionViewData(txtKapan.Text,
            //    "",
            //    "",
            //    Val.ToInt(txtFromPacketNo.Text),
            //    Val.ToInt(txtToPacketNo.Text),
            //    txtTag.Text,
            //    StrFromDate,
            //    StrToDate,
            //    ChkCurrentTransaction.Checked
            //    );
            //MainGrid.DataSource = DTabPacketLiveStock;
            //MainGrid.Refresh();

            //GrdDet.BestFitMaxRowCount = 500;
            //GrdDet.BestFitColumns();

            //GrdDet.Columns["DELETE"].Visible = ChkCurrentTransaction.Checked;

            //this.Cursor = Cursors.Default;
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
            //if (e.RowHandle < 0)
            //{
            //    return;
            //}

            //if (Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "LIVE")) == "LIVE")
            //{
            //    e.Appearance.BackColor = Color.LightGray;
            //    e.Appearance.BackColor2 = Color.LightGray;
            //}
            //else
            //{
            //    e.Appearance.BackColor = Color.White;
            //    e.Appearance.BackColor2 = Color.White;
            //}
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
                if (Global.OnKeyPressToOpenPopup(e))
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
            txtJangedNo.Text = string.Empty;
            txtKapan.Focus();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("TransactionView.xlsx", GrdDet);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle < 0 || Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID) != "1520162494440")
            {
                return;
            }

            DataRow DRow = GrdDet.GetFocusedDataRow();
            if (Val.ToInt(DRow["TODEPARTMENT_ID"]) != BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID)
            {
                Global.Message("You Have Not Authorise Department For Delete This Trasaction");
                return;
            }

            if (GrdDet.FocusedRowHandle < 0 || Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID) != "1520162494440")
            {
                return;
            }

            if (Global.Confirm("Do You Want To Delete This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }


            TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

            Property.TRN_ID = Val.ToInt64(Val.ToString(GrdDet.GetFocusedRowCellValue("TRN_ID")));
            Property.PACKET_ID = Val.ToInt64(Val.ToString(GrdDet.GetFocusedRowCellValue("PACKET_ID")));

            Property = ObjIssRet.DeleteTransaction(Property);

            Global.Message(Property.ReturnMessageDesc);

            if (Property.ReturnMessageType == "SUCCESS")
            {
                GrdDet.DeleteRow(GrdDet.FocusedRowHandle);
                GrdDet.RefreshData();
            }

        }

        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
            if (ObjPer.Password != "" && ObjPer.Password.ToUpper() == txtPassForDisplayBack.Text.ToUpper())
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
                if (Global.OnKeyPressToOpenPopup(e))
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
                if (Global.OnKeyPressToOpenPopup(e))
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
                if (Global.OnKeyPressToOpenPopup(e))
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

            if (Val.Val(GrdDet.GetFocusedRowCellValue("LOSSCARAT")) < 0)
            {
                Global.MessageError("Loss Carat Is In Minus Figure Please Check..");
                return;
            }
            if (Val.Val(GrdDet.GetFocusedRowCellValue("ISSUECARAT")) > Val.Val(GrdDet.GetFocusedRowCellValue("LOTCARAT")))
            {
                Global.MessageError("Issue Carat Can't Be Greater than LotCarat..Please Check.");
                return;
            }
            if (Global.Confirm("Do You Want To Update Process This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

            Property.TRN_ID = Val.ToInt64(Val.ToString(GrdDet.GetFocusedRowCellValue("TRN_ID")));
            Property.PACKET_ID = Val.ToInt64(Val.ToString(GrdDet.GetFocusedRowCellValue("PACKET_ID")));

            Property.FROMPROCESS_ID = Val.ToInt(GrdDet.GetFocusedRowCellValue("FROMPROCESS_ID"));
            Property.TOPROCESS_ID = Val.ToInt(GrdDet.GetFocusedRowCellValue("TOPROCESS_ID"));
            Property.NEXTPROCESS_ID = Val.ToInt(GrdDet.GetFocusedRowCellValue("NEXTPROCESS_ID"));

            Property.ISSUECARAT = Val.Val(GrdDet.GetFocusedRowCellValue("ISSUECARAT"));
            Property.READYCARAT = Val.Val(GrdDet.GetFocusedRowCellValue("READYCARAT"));
            Property.LOSSCARAT = Val.Val(GrdDet.GetFocusedRowCellValue("LOSSCARAT"));
            Property.LOSTCARAT = Val.Val(GrdDet.GetFocusedRowCellValue("LOSTCARAT")); //#p : 04-10-2022
            Property.EXPCARAT = Val.Val(GrdDet.GetFocusedRowCellValue("EXPCARAT"));
           
            Property = ObjIssRet.UpdateTransaction(Property);

            Global.Message(Property.ReturnMessageDesc);

            if (Property.ReturnMessageType == "SUCCESS")
            {
                GrdDet.RefreshData();
            }
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
                //string StrFromDate = null;
                //string StrToDate = null;

                //if (Val.ToBoolean(DTPFromDate.Checked) == true && Val.ToBoolean(DTPToDate.Checked) == true)
                //{
                //    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                //    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                //}

                DTabPacketLiveStock = ObjIssRet.GetTransactionViewData(StrKapanName,
                    "",
                    "",
                    Val.ToInt(StrFromPacketNo),
                    Val.ToInt(StrToPacketNo),
                    StrTag,
                    StrFromDate,
                    StrToDate,
                    BlnCurrentTrn,
                    StrBarcode,
                    Val.ToInt64(StrJangedNo)
                    );

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BtnSearch.Enabled = true;
                PanelProgress.Visible = false;
                if (DTabPacketLiveStock.Rows.Count <= 0)
                {
                    Global.Message("No Data Found..");
                    return;
                }

                GrdDet.BeginUpdate();

                MainGrid.DataSource = DTabPacketLiveStock;
                MainGrid.Refresh();

                GrdDet.BestFitMaxRowCount = 500;
                GrdDet.BestFitColumns();

                GrdDet.Columns["DELETE"].Visible = ChkCurrentTransaction.Checked;
                GrdDet.EndUpdate();

                if (ChkCurrentTransaction.Checked)
                {
                    BtnAllDelete.Visible = true;
                }
                else
                {
                    BtnAllDelete.Visible = false;
                }
                ObjGridSelection.ClearSelection();
                //txtBarcode.Text = string.Empty;
                //txtJangedNo.Text = string.Empty;
                //txtKapan.Text = string.Empty;
                //txtToPacketNo.Text = string.Empty;
                //txtFromPacketNo.Text = string.Empty;
                //txtTag.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                pStrTransaction = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "LIVE"));
                if (pStrTransaction == "LIVE")
                {
                    e.Appearance.BackColor = Color.LightGray;
                    e.Appearance.BackColor2 = Color.LightGray;
                }
                //else
                //{
                //    e.Appearance.BackColor = Color.White;
                //    e.Appearance.BackColor2 = Color.White;
                //}
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


        private void BtnAllDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                DTab = GetTableOfSelectedRows(GrdDet, true);

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

                if (DTab.Rows.Count > 0)
                {
                    foreach (DataRow Dr in DTab.Rows)
                    {

                        if (Val.ToInt(Dr["FROMDEPARTMENT_ID"]) != BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID)
                        {

                            Global.Message("You Have Not Authorise Department For Delete This Trasaction");
                            return;

                        }
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
                        Property.TRN_ID = Val.ToInt64(Val.ToString(Dr["TRN_ID"]));
                        Property.PACKET_ID = Val.ToInt64(Val.ToString(Dr["PACKET_ID"]));
                        Property = ObjIssRet.DeleteTransaction(Property);
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
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }
        }

        private void ChkCurrentTransaction_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCurrentTransaction.Checked == true)
            {
               // BtnAllDelete.Visible = true;
                GrdDet.Columns["EXPCARAT"].Visible = true;
            }
            else
            {
                //BtnAllDelete.Visible = false;
                GrdDet.Columns["EXPCARAT"].Visible = false;
            }
        }

        private void ReptxtExpcarat_Validating(object sender, CancelEventArgs e)
        {
            //try
            //{
            //    if (GrdDet.FocusedRowHandle < 0 || Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID) != "1520162494440")
            //    {
            //        return;
            //    }
            //    if (Global.Confirm("Do You Want To Update Process This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
            //    {
            //        return;
            //    }

            //    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

            //    Property.TRN_ID = Guid.Parse(Val.ToString(GrdDet.GetFocusedRowCellValue("TRN_ID")));
            //    Property.PACKET_ID = Guid.Parse(Val.ToString(GrdDet.GetFocusedRowCellValue("PACKET_ID")));

            //    Property.EXPCARAT = Val.Val(GrdDet.GetFocusedRowCellValue("EXPCARAT"));

            //    Property = ObjIssRet.UpdateExpTransaction(Property);

            //    Global.Message(Property.ReturnMessageDesc);

            //    if (Property.ReturnMessageType == "SUCCESS")
            //    {
            //        GrdDet.RefreshData();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    Global.Message(Ex.Message.ToString());
            //}
        }

        private void ReptxtExpcarat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDet.PostEditor();
                    if (GrdDet.FocusedRowHandle < 0 || Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID) != "1520162494440")
                    {
                        return;
                    }
                    if (Global.Confirm("Do You Want To Update Process This Transaction Entry ? ") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

                    Property.TRN_ID = Val.ToInt64(Val.ToString(GrdDet.GetFocusedRowCellValue("TRN_ID")));
                    Property.PACKET_ID = Val.ToInt64(Val.ToString(GrdDet.GetFocusedRowCellValue("PACKET_ID")));

                    Property.EXPCARAT = Val.Val(GrdDet.GetFocusedRowCellValue("EXPCARAT"));

                    Property = ObjIssRet.UpdateExpTransaction(Property);

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

        private void txtPassForCaratUpdate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtPassForCaratUpdate.Tag) != "" && Val.ToString(txtPassForCaratUpdate.Tag).ToUpper() == txtPassForCaratUpdate.Text.ToUpper())
                {
                    GrdDet.Columns["ISSUECARAT"].OptionsColumn.AllowEdit = true;
                    GrdDet.Columns["READYCARAT"].OptionsColumn.AllowEdit = true;
                }
                else
                {
                    GrdDet.Columns["ISSUECARAT"].OptionsColumn.AllowEdit = false;
                    GrdDet.Columns["READYCARAT"].OptionsColumn.AllowEdit = false;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }

        private void RbtBarcode_CheckedChanged(object sender, EventArgs e) // K : 06/12/2022
        {
            if(RbtBarcode.Checked)
            {
                txtBarcode.Text = string.Empty;
                txtKapan.Text = string.Empty;
                txtToPacketNo.Text = string.Empty;
                txtFromPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtBarcode.Focus();
            }
            else if(RbtnRange.Checked)
            {
                txtBarcode.Text = string.Empty;
                txtKapan.Text = string.Empty;
                txtToPacketNo.Text = string.Empty;
                txtFromPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtKapan.Focus();
            }
            else if (RbtJangedNo.Checked)
            {
                txtBarcode.Text = string.Empty;
                txtKapan.Text = string.Empty;
                txtToPacketNo.Text = string.Empty;
                txtFromPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtKapan.Focus();
            }
            PanelBarcode.Visible = RbtBarcode.Checked;
            PnlKapanPkt.Visible = RbtnRange.Checked;
            PnlJangedno.Visible = RbtJangedNo.Checked;
        }
        private void txtBarcode_Validated(object sender, EventArgs e)
        {
            if(txtBarcode.Text != "")
            {
                BtnSearch_Click(null, null);
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
            }
            
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            if(txtTag.Text != "")
            {
                BtnSearch_Click(null, null); 
            }            
        }

        private void txtJangedNo_Validated(object sender, EventArgs e)
        {
            if (txtJangedNo.Text != "")
            {
                BtnSearch_Click(null, null);
            }  
        }
    }
}
