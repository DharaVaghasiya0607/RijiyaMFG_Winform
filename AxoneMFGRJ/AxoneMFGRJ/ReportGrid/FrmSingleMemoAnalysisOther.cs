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
using AxoneMFGRJ.Masters;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmSingleMemoAnalysisOther : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjView = new BOTRN_RunninPossition();

        BOMST_MajuriRate ObjMajuri = new BOMST_MajuriRate();
        DataTable DTabMajuri = new DataTable();

        DataTable DTabPrdType = new DataTable();

        DataSet DSMemoDetail = new DataSet();

        Int32 mIntPrdType_ID = 0;

        string mStrKapanName = "";
        double mDouMajuri = 0;
        double mDouSaleRate = 0;
        double mDouPurchaseRate = 0;
        double mDouExtraRate = 0;
        double mDouOnOutRate = 0;
        string mStrReportGenerateType = "";

        #region Property Settings

        public FrmSingleMemoAnalysisOther()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            Fill();
            txtPassWord_TextChanged(null, null);
            this.Show();
        }

        public void Fill()
        {
            DTabMajuri = ObjMajuri.Fill(0,0);
            DataRow DR = DTabMajuri.NewRow();
            DTabMajuri.Rows.Add(DR);
            MainGrdMajuri.DataSource = DTabMajuri;
            GrdMajuri.FocusedRowHandle = DR.Table.Rows.IndexOf(DR);
            GrdMajuri.FocusedColumn = GrdMajuri.Columns[0];
            GrdMajuri.Focus();

            DTabPrdType = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRDTYPE);

            DTabPrdType.Rows.Add(0, "", "", 0);

            DTabPrdType.DefaultView.Sort = "SEQUENCENO";

            CmbPrdType.DataSource = DTabPrdType;
            CmbPrdType.DisplayMember = "PRDTYPENAME";
            CmbPrdType.ValueMember = "PRDTYPE_ID";
            //MainGrdMajuri.Refresh();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjView);
            ObjFormEvent.ObjToDisposeList.Add(Val);

        }

        #endregion


        public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DTabMajuri.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }


        public void Clear()
        {
            MainGrdMajuri.Refresh();

        }

        public void SaveOtherRate(string RateType, double RateValue)
        {
            if (txtKapanName.Text.ToString().Trim().Equals(string.Empty))
            {
                Global.Message("Please Select Kapan Name Frist");
                txtKapanName.Text = "";

                txtKapanName.Focus();
                return;
            }

            MajuriRateProperty Property = new MajuriRateProperty();
            Property.KAPANNAME = Val.ToString(txtKapanName.Text);
            Property.RATETYPE = RateType;
            Property.MAJURIRATE = RateValue;

            Property = ObjMajuri.SaveOtherRate(Property);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtKapanName.Text.ToString().Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select KapanName");
                    txtKapanName.Focus();
                    return;
                }
                else if (Val.Val(txtSaleRate.Text) == 0)
                {
                    Global.Message("Please Enter Sale Rate.");
                    txtSaleRate.Focus();
                    return;
                }
                else if (Val.Val(txtPurchaseRate.Text) == 0)
                {
                    Global.Message("Please Enter Purchase Rate.");
                    txtPurchaseRate.Focus();
                    return;
                }

                mIntPrdType_ID = Val.ToInt32(CmbPrdType.SelectedValue);

                mStrKapanName = Val.ToString(txtKapanName.Text);
                mDouMajuri = Val.Val(txtMajuri.Text);
                mDouSaleRate = Val.Val(txtSaleRate.Text);
                mDouPurchaseRate = Val.Val(txtPurchaseRate.Text);
                mDouExtraRate = Val.Val(txtExtraRate.Text);
                mDouOnOutRate = Val.Val(txtOnOutRate.Text);
                mStrReportGenerateType = "GENERATE";


                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                DSMemoDetail.Tables.Clear();
                BtnSearch.Enabled = false;
                BtnReCall.Enabled = false;
                BtnDirectPDFExport.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

                //this.Cursor = Cursors.WaitCursor;
                //DataSet DS = ObjView.GetDataForSinglMemoAnalysis(Val.ToString(txtKapanName.Text), Val.Val(txtMajuri.Text), Val.Val(txtSaleRate.Text), Val.Val(txtPurchaseRate.Text), Val.Val(txtExtraRate.Text), Val.Val(txtOnOutRate.Text));

                //Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                //FrmReportViewer.MdiParent = Global.gMainRef;
                //FrmReportViewer.ShowFormForMemoAnalysis("SingleMemoAnalysis", DS);

                //this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                BtnReCall.Enabled = true;
                BtnDirectPDFExport.Enabled = true;
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
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "pdf";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "SingleMemoAnalysis";
                svDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;

                    if (txtKapanName.Text.ToString().Trim().Equals(string.Empty))
                    {
                        Global.Message("Please Select KapanName");
                        txtKapanName.Focus();
                        return;
                    }
                    else if (Val.Val(txtSaleRate.Text) == 0)
                    {
                        Global.Message("Please Enter Sale Rate.");
                        txtSaleRate.Focus();
                        return;
                    }
                    else if (Val.Val(txtPurchaseRate.Text) == 0)
                    {
                        Global.Message("Please Enter Purchase Rate.");
                        txtPurchaseRate.Focus();
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    DataSet DS = ObjView.GetDataForSinglMemoAnalysisOther(Val.ToString(txtKapanName.Text), Val.Val(txtMajuri.Text), Val.Val(txtSaleRate.Text), Val.Val(txtPurchaseRate.Text), Val.Val(txtExtraRate.Text), Val.Val(txtOnOutRate.Text), Val.ToInt32(CmbPrdType.Tag), "GENERATE");

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ExportPDFForMemoAnalaysis("SingleMemoAnalysis", DS, Filepath);

                    System.Diagnostics.Process.Start(Filepath, "cmd");

                    this.Cursor = Cursors.Default;

                }
            }
            catch (Exception ex)
            {
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
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANSINGLE);
                    FrmSearch.mColumnsToHide = "";
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


                FetchValue();


            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void majuriRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdMajuri.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        MajuriRateProperty Property = new MajuriRateProperty();
                        DataRow Drow = GrdMajuri.GetDataRow(GrdMajuri.FocusedRowHandle);
                        Property.MAJURI_ID = Val.ToInt32(Drow["MAJURI_ID"]);
                        Property = ObjMajuri.Delete(Property);

                        if (Property.ReturnMessageType == "DELETED")
                        {
                            lblMasParamete.Text = "Value Deleted";
                            DTabMajuri.Rows.RemoveAt(GrdMajuri.FocusedRowHandle);
                            DTabMajuri.AcceptChanges();
                            Fill();
                        }
                        else
                        {
                            lblMasParamete.Text = "Error...";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void FrmSingleMemoAnalysis_Load(object sender, EventArgs e)
        {

        }

        private void GrdMajuri_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            GrdMajuri.PostEditor();
            if (e.RowHandle < 0)
            {
                return;
            }

            MajuriRateProperty property = new MajuriRateProperty();


            DataRow DRow = GrdMajuri.GetDataRow(e.RowHandle);

            if (Val.ToString(DRow["RATE"]).Trim().Equals(string.Empty))
                return;

            property.MAJURI_ID = Val.ToInt32(DRow["MAJURI_ID"]);
            property.SIZENAME = Val.ToString(DRow["SIZENAME"]);
            property.FROMAMT = Val.Val(DRow["FROMAMOUNT"]);
            property.TOAMT = Val.Val(DRow["TOAMOUNT"]);
            property.RATE = Val.Val(DRow["RATE"]);


            if (property.SIZENAME == "" || property.FROMAMT == 0 || property.TOAMT == 0 || property.RATE == 0)
            {
                Global.Message("Some Data Has Been Missing");
                return;
            }
            property = ObjMajuri.Save(property);

            if (property.ReturnMessageType == "SUCCESS")
            {

                lblMasParamete.Text = " Value Successfully Inserted";
                Fill();
            }
            else
            {

                lblMasParamete.Text = " Value Updated Successfully";
                Clear();
            }

            GrdMajuri.RefreshData();
            DTabMajuri.AcceptChanges();

            property = null;
        }

        private void repTxtSize_Validating(object sender, CancelEventArgs e)
        {
            DataRow Dr = GrdMajuri.GetFocusedDataRow();
            if (CheckDuplicate("SIZENAME", Val.ToString(GrdMajuri.EditingValue), GrdMajuri.FocusedRowHandle, "SIZENAME"))
            {
                e.Cancel = true;
                return;
            }
            else if (Val.ToString(GrdMajuri.EditingValue).Trim().Equals(String.Empty))
            {
                GrdMajuri.EditingValue = Val.ToString(Dr["FROMAMOUNT"]) + "-" + Val.ToString(Dr["TOAMOUNT"]);
            }
        }

        private void reptxtFAmt_Validating(object sender, CancelEventArgs e)
        {
            GrdMajuri.PostEditor();
            DataRow Dr = GrdMajuri.GetFocusedDataRow();
            if (CheckDuplicate("FROMAMOUNT", Val.ToString(GrdMajuri.EditingValue), GrdMajuri.FocusedRowHandle, "FROMAMOUNT"))
            {
                e.Cancel = true;
                return;
            }
            else
            {
                Dr["SIZENAME"] = Val.ToString(GrdMajuri.EditingValue) + "-" + Val.ToString(Dr["TOAMOUNT"]);
                DTabMajuri.AcceptChanges();

            }
            if (Val.ToDecimal(Dr["TOAMOUNT"]) != 0)
            {
                if (Val.ToDecimal(GrdMajuri.EditingValue) > Val.ToDecimal(Dr["TOAMOUNT"]))
                {
                    Global.Message("From Amount must be Greter Than To Amount");
                    e.Cancel = true;
                    return;
                }
            }

            var dValue = from row in DTabMajuri.AsEnumerable()
                         where Val.Val(row["FROMAMOUNT"]) <= Val.Val(GrdMajuri.EditingValue) && Val.Val(row["TOAMOUNT"]) >= Val.Val(GrdMajuri.EditingValue) && row.Table.Rows.IndexOf(row) != GrdMajuri.FocusedRowHandle
                         select row;

            if (dValue.Any())
            {
                Global.Message("This Value Already Exist Between Some From Amount and To Amount Please Check.!");
                e.Cancel = true;
                return;
            }

        }

        private void reptxtTAmt_Validating(object sender, CancelEventArgs e)
        {
            DataRow Dr = GrdMajuri.GetFocusedDataRow();
            if (CheckDuplicate("TOAMOUNT", Val.ToString(GrdMajuri.EditingValue), GrdMajuri.FocusedRowHandle, "TOAMOUNT"))
            {
                e.Cancel = true;
                return;
            }
            if (Val.ToDecimal(Dr["FROMAMOUNT"]) > Val.ToDecimal(GrdMajuri.EditingValue))
            {
                Global.Message("To Carat must be Greter Than From Amount");
                e.Cancel = true;
                return;
            }
            else
            {
                Dr["SIZENAME"] = Val.ToString(Dr["FROMAMOUNT"]) + "-" + Val.ToString(GrdMajuri.EditingValue);
                DTabMajuri.AcceptChanges();

            }

            var dValue = from row in DTabMajuri.AsEnumerable()
                         where Val.Val(row["FROMAMOUNT"]) <= Val.Val(GrdMajuri.EditingValue) && Val.Val(row["TOAMOUNT"]) >= Val.Val(GrdMajuri.EditingValue) && row.Table.Rows.IndexOf(row) != GrdMajuri.FocusedRowHandle
                         select row;

            if (dValue.Any())
            {
                Global.Message("This Value Already Exist Between Some From Amount and To Amount Please Check.!");
                e.Cancel = true;
                return;
            }

        }

        private void GrdMajuri_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtSaleRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPurchaseRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtExtraRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtOnOutRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSaleRate_Validating(object sender, CancelEventArgs e)
        {
            if (Val.ToString(txtKapanName.Text).Trim().Equals(string.Empty))
            {
                txtSaleRate.Text = "";
            }
            else
            {
                SaveOtherRate("SaleRate", Val.Val(txtSaleRate.Text));
            }

        }

        private void txtExtraRate_Validating(object sender, CancelEventArgs e)
        {
            if (Val.ToString(txtKapanName.Text).Trim().Equals(string.Empty))
            {
                txtExtraRate.Text = "";
            }
            else
            {
                SaveOtherRate("ExtraRate", Val.Val(txtExtraRate.Text));
            }

        }

        private void txtPurchaseRate_Validating(object sender, CancelEventArgs e)
        {
            if (Val.ToString(txtKapanName.Text).Trim().Equals(string.Empty))
            {
                txtPurchaseRate.Text = "";

            }
            else
            {
                SaveOtherRate("PurchaseRate", Val.Val(txtPurchaseRate.Text));

            }

        }

        private void txtOnOutRate_Validating(object sender, CancelEventArgs e)
        {
            if (Val.ToString(txtKapanName.Text).Trim().Equals(string.Empty))
            {
                txtOnOutRate.Text = "";
                return;

            }
            else
            {
                SaveOtherRate("OnOutRate", Val.Val(txtOnOutRate.Text));
            }

        }

        public void FetchValue()
        {
            DataTable DtabRateDetail = ObjMajuri.GetInfoByCodeForReportOther(txtKapanName.Text);
            if (DtabRateDetail.Rows.Count > 0)
            {
                txtSaleRate.Text = String.Empty;
                txtPurchaseRate.Text = String.Empty;
                txtOnOutRate.Text = String.Empty;
                txtExtraRate.Text = String.Empty;

                if (!Val.ToString(DtabRateDetail.Rows[0]["MEMODATASAVEDATE"]).Trim().Equals(string.Empty))
                    lblDetailSaveMessage.Text = "Memo Analysis Detail Saved On '" + Val.ToString(DtabRateDetail.Rows[0]["MEMODATASAVEDATE"]) + "' Date";
                else
                    lblDetailSaveMessage.Text = "Selected Kapan Memo Analysis Details Are Not Saved..";


                foreach (DataRow Dr in DtabRateDetail.Rows)
                {
                    if (Val.ToString(Dr["RATETYPE"]) == "SaleRate")
                        txtSaleRate.Text = Val.ToString(Dr["RATE"]);

                    else if (Val.ToString(Dr["RATETYPE"]) == "PurchaseRate")
                        txtPurchaseRate.Text = Val.ToString(Dr["RATE"]);

                    else if (Val.ToString(Dr["RATETYPE"]) == "OnOutRate")
                        txtOnOutRate.Text = Val.ToString(Dr["RATE"]);

                    else if (Val.ToString(Dr["RATETYPE"]) == "ExtraRate")
                        txtExtraRate.Text = Val.ToString(Dr["RATE"]);
                }
            }
            else
            {
                txtSaleRate.Text = String.Empty;
                txtPurchaseRate.Text = String.Empty;
                txtOnOutRate.Text = String.Empty;
                txtExtraRate.Text = String.Empty;
                lblDetailSaveMessage.Text = "Selected Kapan Memo Analysis Details Are Not Saved..";
            }

        }

        private void txtPassWord_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPassWord.Text != "" && txtPassWord.Tag.ToString().ToUpper() == txtPassWord.Text.ToString().ToUpper())
                {
                    PnlRateDetail.Visible = true;
                    lblMajuriHeader.Visible = true;
                    MainGrdMajuri.Visible = true;
                    lblMasParamete.Visible = true;

                }
                else
                {
                    PnlRateDetail.Visible = false;
                    lblMajuriHeader.Visible = false;
                    MainGrdMajuri.Visible = false;
                    lblMasParamete.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DSMemoDetail = ObjView.GetDataForSinglMemoAnalysisOther(mStrKapanName, mDouMajuri, mDouSaleRate, mDouPurchaseRate, mDouExtraRate, mDouOnOutRate, mIntPrdType_ID, mStrReportGenerateType);
            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                BtnReCall.Enabled = true;
                BtnDirectPDFExport.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                BtnReCall.Enabled = true;
                BtnDirectPDFExport.Enabled = true;

                if (DSMemoDetail.Tables.Count <= 0)
                {
                    Global.Message("No Data Found..!");
                    return;
                }
                Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowFormForMemoAnalysiReportOther("SingleMemoAnalysisReportOther", DSMemoDetail);
            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                BtnReCall.Enabled = true;
                BtnDirectPDFExport.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnReCall_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtKapanName.Text.ToString().Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select KapanName");
                    txtKapanName.Focus();
                    return;
                }
                else if (Val.Val(txtSaleRate.Text) == 0)
                {
                    Global.Message("Please Enter Sale Rate.");
                    txtSaleRate.Focus();
                    return;
                }
                else if (Val.Val(txtPurchaseRate.Text) == 0)
                {
                    Global.Message("Please Enter Purchase Rate.");
                    txtPurchaseRate.Focus();
                    return;
                }

                mIntPrdType_ID = Val.ToInt32(CmbPrdType.SelectedValue);

                mStrKapanName = Val.ToString(txtKapanName.Text);
                mDouMajuri = Val.Val(txtMajuri.Text);
                mDouSaleRate = Val.Val(txtSaleRate.Text);
                mDouPurchaseRate = Val.Val(txtPurchaseRate.Text);
                mDouExtraRate = Val.Val(txtExtraRate.Text);
                mDouOnOutRate = Val.Val(txtOnOutRate.Text);
                mStrReportGenerateType = "RECALL";


                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                DSMemoDetail.Tables.Clear();
                BtnSearch.Enabled = false;
                BtnReCall.Enabled = false;
                BtnDirectPDFExport.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                BtnReCall.Enabled = true;
                BtnDirectPDFExport.Enabled = true;
                Global.Message(ex.Message);
                return;
            }

        }

    }
}


