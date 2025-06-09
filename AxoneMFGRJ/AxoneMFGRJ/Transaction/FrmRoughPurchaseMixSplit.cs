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
using DevExpress.XtraGrid.Views.BandedGrid;

namespace AxoneMFGRJ.Transaction
{
    public partial class 
        FrmRoughPurchaseMixSplit : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchaseMasterDetail ObjRough = new BOTRN_RoughPurchaseMasterDetail();
        
        DataTable DTabFrom = new DataTable();
        DataTable DTabTo = new DataTable();
     
        public FormType mFormType = FormType.Mix;

        public enum FormType
        {
            Mix = 0,
            Split = 1
        }

        #region Property Settings

        public FrmRoughPurchaseMixSplit()
        {
            InitializeComponent();
        }

        public void ShowForm(DataTable pDabFrom, FormType pFormType)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            mFormType = pFormType;
            DTabFrom = pDabFrom.Copy();
            DTabTo = pDabFrom.Clone();
            DTabFrom.Columns.Add(new DataColumn("TRFCARAT", typeof(double)));
            DTabTo.Columns.Add(new DataColumn("GROSSAMOUNTINR", typeof(double)));

            for (int IntI = 0; IntI < DTabFrom.Rows.Count; IntI++)
            {
                DTabFrom.Rows[IntI]["TRFCARAT"] = DTabFrom.Rows[IntI]["BALANCECARAT"];

                if (IntI == 0)
                {
                    txtExcRate.Text = Val.Val(DTabFrom.Rows[IntI]["EXCRATE"]).ToString();

                }
            }
            DTabFrom.AcceptChanges();
            DTabTo.AcceptChanges();
            if (mFormType == FormType.Mix)
            {
                BtnSave.Text = "Mix";
            }
            if (mFormType == FormType.Split)
            {
                BtnSave.Text = "Split";
            }

            GrdDetFrom.BeginUpdate();
            MainGridFrom.DataSource = DTabFrom;
            MainGridFrom.Refresh();
            GrdDetFrom.BestFitColumns();
            GrdDetFrom.EndUpdate();

            GrdDetTo.BeginUpdate();
            MainGridTo.DataSource = DTabTo;
            MainGridTo.Refresh();
            GrdDetTo.BestFitColumns();
            GrdDetTo.EndUpdate();

            
            GrdDetFrom.FocusedRowHandle = 0;
            GrdDetFrom.FocusedColumn = GrdDetFrom.Columns["TRFCARAT"];

            CalculateSummary();

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
            ObjFormEvent.ObjToDisposeList.Add(ObjRough);
            ObjFormEvent.ObjToDisposeList.Add(Val);            
        }

        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CalculateSummary()
        {
            double DouFromCarat = 0;
            double DouFromAmount = 0;
            double DouFromAmountINR = 0;


            foreach (DataRow DRow in DTabFrom.Rows)
            {
                DouFromCarat = DouFromCarat + Val.Val(DRow["TRFCARAT"]);
                DouFromAmount = DouFromAmount + (Val.Val(DRow["GROSSBROKRATE"]) * Val.Val(DRow["TRFCARAT"]));
                DouFromAmountINR = DouFromAmountINR + (Val.Val(DRow["GROSSBROKRATE"]) * Val.Val(DRow["TRFCARAT"])) * Val.Val(DRow["EXCRATE"]);
            
            }

            txtTransferCarat.Text = DouFromCarat.ToString();
            txtTransferAmount.Text = DouFromAmount.ToString();
            txtTransferRate.Text = DouFromCarat == 0 ? "0" : (DouFromAmount / DouFromCarat).ToString();
            txtTransferAmountINR.Text = DouFromAmountINR.ToString();

            double DouToCarat = 0;
            double DouToAmount = 0;

            foreach (DataRow DRow in DTabTo.Rows)
            {
                DouToCarat = DouToCarat + Val.Val(DRow["CARAT"]);
                DouToAmount = DouToAmount + (Val.Val(DRow["RATE"]) * Val.Val(DRow["CARAT"]));
                DRow["GROSSAMOUNTINR"] = Val.Val(DRow["GROSSAMOUNT"]) * Val.Val(txtExcRate.Text);
            }

            txtToCarat.Text = DouToCarat.ToString();
            txtToAmountUSD.Text = DouToAmount.ToString();
            txtToRate.Text = DouToCarat == 0 ? "0" : (DouToAmount / DouToCarat).ToString();
            txtToAmountINR.Text = (Val.Val(txtToAmountUSD.Text) * Val.Val(txtExcRate.Text)).ToString();

            double DouDiff = Val.Val(txtTransferCarat.Text) - Val.Val(txtToCarat.Text);
            double DouDiffAmount = Val.Val(txtTransferAmount.Text) - Val.Val(txtToAmountUSD.Text);

            txtDiffCarat.Text = DouDiff.ToString();
            txtDiffAmountUSD.Text = DouDiffAmount.ToString();
            txtDiffAvgRate.Text = DouDiff == 0 ? "0" : (DouDiffAmount / DouDiff).ToString();

            txtDiffAmountINR.Text = (DouDiffAmount * Val.Val(txtExcRate.Text)).ToString();
        }


        private void txtRoughName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDetTo.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "ROUGHNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "ROUGHNAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_ROUGHNAME);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetTo.SetFocusedRowCellValue("ROUGHNAME", Val.ToString(FrmSearch.mDRow["ROUGHNAME"]));
                    }
                   
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtMSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDetTo.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "MSIZENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "MSIZENAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MSIZE);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetTo.SetFocusedRowCellValue("MSIZENAME", Val.ToString(FrmSearch.mDRow["MSIZENAME"]));
                    }
                   
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtMines_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDetTo.PostEditor();
                    
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "MINESNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "MINESNAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MINES);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetTo.SetFocusedRowCellValue("MINESNAME", Val.ToString(FrmSearch.mDRow["MINESNAME"]));
                    }
                    
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtArticle_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDetTo.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "ARTICLENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "ARTICLENAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_ARTICLE);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetTo.SetFocusedRowCellValue("ARTICLENAME", Val.ToString(FrmSearch.mDRow["ARTICLENAME"]));
                    }
                    
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (Val.Val(txtDiffCarat.Text) != 0)
            {
                Global.Message("From Carat And To Carat Is Mismatched, Please Make Correct ");
                return;
            }

            for (int IntI = 0; IntI < DTabTo.Rows.Count; IntI++)
            {
                DataRow DRow = DTabTo.Rows[IntI];
                if (Val.Val(DRow["CARAT"]) != 0 && Val.Val(DRow["GROSSAMOUNT"]) == 0)
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Gross Amount Is Required At this Row");
                    return ;
                }
                if (Val.Val(DRow["CARAT"]) != 0 && Val.ToString(DRow["LOTNO"]).Trim() == "")
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Lot No Is Required At this Row");
                    return ;
                }
                if (Val.Val(DRow["CARAT"]) != 0 && Val.ToString(DRow["ROUGHNAME"]).Trim() == "")
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Rough Name Is Required At this Row");
                    return ;
                }
            }

            if (Global.Confirm("Are you Sure Your Want To Save This Entry ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            TrnRoughPurchaseProperty Property = new TrnRoughPurchaseProperty();
            DTabFrom.TableName = "Table1";
            string StrFromLot = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                DTabFrom.WriteXml(sw);
                StrFromLot = sw.ToString();
            }

            Property.FROMLOTS = StrFromLot;

            DTabTo.TableName = "Table1";
            string StrToLot = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                DTabTo.WriteXml(sw);
                StrToLot = sw.ToString();
            }
            Property.TOLOTS = StrToLot;
            Property.EXCRATE = Val.Val(txtExcRate.Text);
            Property = ObjRough.SaveMixSplit(Property, mFormType.ToString().ToUpper());
            if (Property.ReturnMessageType == "FAIL")
            {
                Global.MessageError(Property.ReturnMessageDesc);
            }
            else
            {
                Global.Message(Property.ReturnMessageDesc);
                this.Close();
            } 
        }

        private void txtTrfCarat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (GrdDetFrom.IsLastRow == true)
                {
                    txtExcRate.Focus();
                }
                else
                {
                    GrdDetFrom.FocusedColumn = GrdDetFrom.Columns["TRFCARAT"];
                }
            }
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            DTabTo.Rows.Clear();

            DataRow DRNew = DTabTo.NewRow();

            DRNew["LOT_ID"] = 0;
            DRNew["SRNO"] = 1;
            DRNew["ROUGHTYPE"] = Val.ToString(DTabFrom.Rows[0]["ROUGHTYPE"]);
            DRNew["LOTNO"] = "";
            DRNew["ARTICLENAME"] = Val.ToString(DTabFrom.Rows[0]["ARTICLENAME"]);
            DRNew["MINESNAME"] = Val.ToString(DTabFrom.Rows[0]["MINESNAME"]);
            DRNew["ROUGHNAME"] = Val.ToString(DTabFrom.Rows[0]["ROUGHNAME"]);
            DRNew["MSIZENAME"] = Val.ToString(DTabFrom.Rows[0]["MSIZENAME"]);
            DRNew["LOTDESCRIPTION"] = Val.ToString(DTabFrom.Rows[0]["LOTDESCRIPTION"]);
            DRNew["PCS"] = 0;
            DRNew["CARAT"] = txtTransferCarat.Text;
            DRNew["RATE"] = txtTransferRate.Text;
            DRNew["GROSSAMOUNT"] = Val.Val(DRNew["CARAT"]) *  Val.Val(DRNew["RATE"]);
            DRNew["GROSSAMOUNTINR"] = Val.Val(DRNew["GROSSAMOUNT"]) * Val.Val(txtExcRate.Text);

            DTabTo.Rows.Add(DRNew);
            GrdDetTo.FocusedRowHandle = GrdDetTo.RowCount - 1;
            GrdDetTo.FocusedColumn = GrdDetTo.Columns["LOTNO"];
            CalculateSummary();
        }

        private void txtExcRate_TextChanged(object sender, EventArgs e)
        {
            CalculateSummary();
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (Val.Val(GrdDetTo.GetFocusedRowCellValue("CARAT")) != 0 && GrdDetTo.IsLastRow == true)
                {
                    if (mFormType == FormType.Split)
                    {
                        if (Val.Val(txtDiffCarat.Text) != 0)
                        {
                            DataRow DRNew = DTabTo.NewRow();

                            DRNew["LOT_ID"] = 0;
                            DRNew["SRNO"] = GrdDetTo.RowCount + 1;
                            DRNew["LOTNO"] = "";
                            DRNew["ARTICLENAME"] = Val.ToString(DTabFrom.Rows[0]["ARTICLENAME"]);
                            DRNew["MINESNAME"] = Val.ToString(DTabFrom.Rows[0]["MINESNAME"]);
                            DRNew["ROUGHNAME"] = Val.ToString(DTabFrom.Rows[0]["ROUGHNAME"]);
                            DRNew["MSIZENAME"] = Val.ToString(DTabFrom.Rows[0]["MSIZENAME"]);
                            DRNew["LOTDESCRIPTION"] = Val.ToString(DTabFrom.Rows[0]["LOTDESCRIPTION"]);
                            DRNew["PCS"] = 0;
                            DRNew["CARAT"] = txtDiffCarat.Text;
                            DRNew["RATE"] = txtDiffAvgRate.Text;
                            DRNew["GROSSAMOUNT"] = txtDiffAmountUSD.Text;
                            DRNew["GROSSAMOUNTINR"] = Val.Val(DRNew["GROSSAMOUNT"]) * Val.Val(txtExcRate.Text);
                            DTabTo.Rows.Add(DRNew);
                            GrdDetTo.FocusedRowHandle = GrdDetTo.RowCount - 1;
                            GrdDetTo.FocusedColumn = GrdDetTo.Columns["SRNO"];
                        }
                       
                    }
                }
                else
                {
                    BtnSave.Focus();
                }
                CalculateSummary();
            }
        }

        private void GrdDetTo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName.ToUpper())
            {

                case "PCS":
                case "CARAT":
                case "RATE" :
                    double DouAmount = Val.Val( GrdDetTo.GetRowCellValue(e.RowHandle, "CARAT")) * Val.Val(GrdDetTo.GetRowCellValue(e.RowHandle, "RATE"));
                    double DouAmountINR = DouAmount * Val.Val(txtExcRate.Text);
                    GrdDetTo.SetRowCellValue(e.RowHandle,"GROSSAMOUNT",DouAmount);
                    GrdDetTo.SetRowCellValue(e.RowHandle, "GROSSAMOUNTINR", DouAmountINR);
                    break;
                case "ROUGHNAME":
                    GrdDetTo.SetRowCellValue(e.RowHandle, "LOTDESCRIPTION", Val.ToString(GrdDetTo.GetRowCellValue(e.RowHandle, "ROUGHNAME")));
                    break;
                default:
                    break;
            }
            GrdDetTo.BestFitColumns();
            CalculateSummary();
        }

        private void GrdDetFrom_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName.ToUpper())
            {
                case "TRFCARAT":
                    CalculateSummary();
                    break;
                default:
                    break;
            }
        }

    }
}
