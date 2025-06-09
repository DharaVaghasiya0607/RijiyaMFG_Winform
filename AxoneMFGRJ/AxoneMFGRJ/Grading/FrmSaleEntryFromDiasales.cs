using BusLib;
using BusLib.Configuration;
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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using AxoneMFGRJ.Transaction;
using AxoneMFGRJ.Utility;
using DevExpress.Data;

namespace AxoneMFGRJ.Grading
{
    public partial class FrmSaleEntryFromDiasales : DevExpress.XtraEditors.XtraForm
    {
        // BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_SaleEntryFromDiasales ObjSales = new BOTRN_SaleEntryFromDiasales();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        IDataObject PasteclipData = Clipboard.GetDataObject();

        DataTable DTabFeatchData = new DataTable();
        DataTable DTabSaveData = new DataTable();

        double DouCarat = 0;
        double DouCostRapaport = 0;
        double DouCostRapaportAmt = 0;
        double DouCostDisc = 0;
        double DouCostPricePerCarat = 0;
        double DouCostAmount = 0;


        BODevGridSelection ObjGridSelection;


        #region Property Settings

        public FrmSaleEntryFromDiasales()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            if (MainGrdDetail.RepositoryItems.Count == 0)
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
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }


            this.Show();

            ChkCmbKapanName.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANSINGLE);
            ChkCmbKapanName.Properties.DisplayMember = "KAPANNAME";
            ChkCmbKapanName.Properties.ValueMember = "KAPANNAME";
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

        private void BtnShow_Click(object sender, EventArgs e)
        {
            ObjGridSelection.ClearSelection();

            if (ChkCmbKapanName.Text.Length == 0)
            {
                Global.Message("Please Select Kapan First");
                ChkCmbKapanName.Focus();
                return;
            }

            DTabFeatchData.Rows.Clear();

            BtnFetch.Enabled = false;
            PanelProgress.Visible = true;
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        public void Fill()
        {
            DTabFeatchData = ObjSales.FetchSaleDataFromDiasales(Val.Trim(ChkCmbKapanName.Properties.GetCheckedItems()));

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Fill();
            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnFetch.Enabled = true;
                Global.Message(ex.Message.ToString());
            }


        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelProgress.Visible = false;
                BtnFetch.Enabled = true;

                GrdDetail.BeginUpdate();

                MainGrdDetail.DataSource = DTabFeatchData;
                MainGrdDetail.Refresh();

                GrdDetail.RefreshData();
                GrdDetail.BestFitColumns();

                GrdDetail.EndUpdate();
            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnFetch.Enabled = true;
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DTabSaveData = GetTableOfSelectedRows(GrdDetail, true);

                if (DTabSaveData.Rows.Count == 0)
                {
                    Global.Message("Please Select at lease One Row For Entry");
                    return;
                }

                if (Global.Confirm("Are you Sure You Want Enter Selected Packets?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                DTabSaveData.AcceptChanges();

                DTabSaveData.TableName = "Table";


                string StrXMLValues = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabSaveData.WriteXml(sw);
                    StrXMLValues = sw.ToString();
                }

                DataTable DTab = new DataTable();

                string UploadDetail = StrXMLValues;

                DTab = ObjSales.SaveSalesDataFromDiasales(UploadDetail);


                if (DTab.Rows.Count > 0)
                {
                    string Str = Val.ToString(DTab.Rows[0]["RETURNDESC"]);
                    Global.Message(Str);
                }
                else
                {
                    Global.Message("Opps...Something Wrong..");
                    return;
                    ChkCmbKapanName.Focus();
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
                return;
            }
        }

        private void GrdDetail_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                DouCarat = 0;
                DouCostRapaport = 0;
                DouCostRapaportAmt = 0;
                DouCostDisc = 0;
                DouCostPricePerCarat = 0;
                DouCostAmount = 0;
            }
            else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                DouCarat = DouCarat + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "CARAT"));
                DouCostAmount = DouCostAmount + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "AMOUNT"));
                DouCostRapaport = Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "RAPAPORT"));
                DouCostPricePerCarat = DouCostAmount / DouCarat;
                DouCostRapaportAmt = DouCostRapaportAmt + (DouCostRapaport * Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "CARAT")));
            }
            else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PRICEPERCARAT") == 0)
                {
                    if (Val.Val(DouCarat) > 0)
                        e.TotalValue = Math.Round(Val.Val(DouCostAmount) / Val.Val(DouCarat), 2);
                    else
                        e.TotalValue = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RAPAPORT") == 0)
                {
                    if (Val.Val(DouCarat) > 0)
                        e.TotalValue = Math.Round(Val.Val(DouCostRapaportAmt) / Val.Val(DouCarat), 2);
                    else
                        e.TotalValue = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DISCOUNT") == 0)
                {
                    DouCostRapaport = Math.Round((DouCostRapaportAmt / DouCarat), 2);
                    //DouCostDisc = Math.Round(((DouCostPricePerCarat - DouCostRapaport) / DouCostRapaport * 100), 2);
                    DouCostDisc = Math.Round(((DouCostRapaport - DouCostPricePerCarat) / DouCostRapaport * 100), 2);
                    e.TotalValue = DouCostDisc;
                }
            }
        }
    }
}
