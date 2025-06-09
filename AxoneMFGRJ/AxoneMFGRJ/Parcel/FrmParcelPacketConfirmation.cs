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
using AxoneMFGRJ.Utility;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmParcelPacketConfirmation : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        BODevGridSelection selectionSummary;

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();
      
        #region Property Settings

        public FrmParcelPacketConfirmation()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
            DTPConfirmDate.Value = DateTime.Now;
            BtnSearch.PerformClick();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
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
            else if (e.KeyCode == Keys.F5)
            {
                BtnSearch.PerformClick();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataSet DS = ObjTrn.GetPendingConfirmationDataParcel();

                MainGrid.DataSource = DS.Tables[0];
                GrdDet.RefreshData();

                //MainGridSummary.DataSource = DS.Tables[1];
                //GrdDetSummary.RefreshData();

                if (MainGrid.RepositoryItems.Count == 0)
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

                CalculateSummary();

                //if (MainGridSummary.RepositoryItems.Count == 0)
                //{
                //    selectionSummary = new DevExpressGrid();
                //    selectionSummary.View = GrdDetSummary;
                //    selectionSummary.ClearSelection();
                //    selectionSummary.CheckMarkColumn.VisibleIndex = 0;
                //}
                //else
                //{
                //    selectionSummary.ClearSelection();
                //}
                //GrdDetSummary.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
                //if (selectionSummary != null)
                //{
                //    selectionSummary.ClearSelection();
                //    selectionSummary.CheckMarkColumn.VisibleIndex = 0;
                //}
               
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
           
        }

        public void CalculateSummary()
        {
            int IntPcs = 0;
            double DouCarat = 0;
            int IntSelPcs = 0;
            double DouSelCarat = 0;

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntPcs = IntPcs + 1;
                DouCarat = DouCarat + Val.Val(DRow["ISSUECARAT"]);
            }

            DataTable DTab = GetTableOfSelectedRows(GrdDet, true, ObjGridSelection);
            if (DTab == null)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                IntSelPcs = IntSelPcs + 1;
                DouSelCarat = DouSelCarat + Val.Val(DRow["ISSUECARAT"]);
            }

            txtTotalPcs.Text = IntPcs.ToString();
            txtTotalCarat.Text = DouCarat.ToString();
            txtSelectedPcs.Text = IntSelPcs.ToString();
            txtSelectedCarat.Text = DouSelCarat.ToString();

            //txtCarat.Text = Math.Round(DouCarat, 3).ToString();
            //txtTrfCarat.Text = Math.Round(DouCarat, 3).ToString();
            //txtAmount.Text = Math.Round(DouAmount, 3).ToString();
            //double DouRate = DouCarat != 0 ? Math.Round(DouAmount / DouCarat, 3) : 0;
            //txtRate.Text = Math.Round(DouRate, 3).ToString();
            //txtTransferRate.Text = Math.Round(DouRate, 3).ToString();
        }
        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
        }


        private DataTable GetTableOfSelectedRows(GridView view, Boolean IsSelect, BODevGridSelection ObjGridSelection)
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

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("Are You Sure To Confirm ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            DataTable DTab = GetTableOfSelectedRows(GrdDet, true,ObjGridSelection);

            if (DTab.Rows.Count < 0)
            {
                Global.Message("Please Select At Least One Stone");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            int IntRes = 0;
            foreach (DataRow DRow in DTab.Rows)
            {
                TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                Property.CONFDATE = DTPConfirmDate.Value.ToString();
                Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"]);
                Property.JANGEDNO = Val.ToInt64(DRow["JANGEDNO"]);
                Property.TRN_ID = Val.ToInt64(DRow["TRN_ID"]);

                Property = ObjTrn.ConfirmJangedForParcel("PACKET", Property);
               if (Property.ReturnMessageType == "SUCCESS")
               {
                   IntRes++;
               }
               Property = null;
            }
            this.Cursor = Cursors.Default;
            
            if (IntRes != 0)
            {
                Global.Message("Successfully Confirmed");
                BtnSearch.PerformClick();
            }
            DTab = null;

        }

        private void BtnConfirmJanged_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("Are You Sure To Confirm ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            DataTable DTab = GetTableOfSelectedRows(GrdDetSummary, true, selectionSummary);

            this.Cursor = Cursors.WaitCursor;
            int IntRes = 0;
            foreach (DataRow DRow in DTab.Rows)
            {
                TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                Property.CONFDATE = Val.SqlDate(DTPConfirmDate.Value.ToShortDateString());
                Property.JANGEDNO = Val.ToInt64(DRow["JANGEDNO"]);

                Property = ObjTrn.ConfirmJanged("JANGED", Property);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    IntRes++;
                }
                Property = null;
            }
            this.Cursor = Cursors.Default;

            if (IntRes != 0)
            {
                Global.Message("Successfully Confirmed");
                BtnSearch.PerformClick();
            }
            DTab = null;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            bool ISFind = false;
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                if (txtKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                    && txtPacketNo.Text.Trim() == Val.ToString(DRow["PACKETNO"]).Trim()
                    && txtTag.Text.Trim() == Val.ToString(DRow["TAG"]).Trim()
                    )
                {
                    ISFind = true;
                    GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                    txtKapanName.Text = string.Empty;
                    txtPacketNo.Text = string.Empty;
                    txtTag.Text = string.Empty;

                    txtKapanName.Focus();
                    CalculateSummary();
                    break;
                }
            }

            if (ISFind == false)
            {
                Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check");
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;

                txtKapanName.Focus();
            }
            else
            {
                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.RefreshData();
            }
        }
      
    }
}
