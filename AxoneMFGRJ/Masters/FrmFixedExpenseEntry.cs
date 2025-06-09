using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmFixedExpenseEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_FixedExpense ObjMast = new BOMST_FixedExpense();
        
        DataTable DtabExpense = new DataTable();


        #region Property Settings

        public FrmFixedExpenseEntry()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            BtnAdd_Click(null, null);
            string currentMonth = DateTime.Now.Month.ToString();
            string currentYear = DateTime.Now.Year.ToString();
            txtYear.Text = currentYear;
            //CmbMonth.SelectedItem = DateTime.Now.AddMonths(-1).ToString("MMM");
            CmbMonth.SelectedItem = DateTime.Now.AddMonths(0).ToString("MMM");

            txtYear.Text = DateTime.Now.Year.ToString();
            BtnShow_Click(null, null);

            CmbMonth.Focus();
            this.Show();

        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabExpense.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }

        #region Validation

        private bool ValSave()
        {
            //if (txtItemGroupCode.Text.Trim().Length == 0)
            //{
            //    Global.Message("Group Code Is Required");
            //    txtItemGroupCode.Focus();
            //    return false;
            //}
            int IntCol = 0, IntRow = -1;
            foreach (DataRow dr in DtabExpense.Rows)
            {
                //For Update Validation
                if (Val.ToString(dr["LEDGERNAME"]).Trim().Equals(string.Empty) && !Val.ToString(dr["EXPENSE_ID"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Enter Expense A/C");
                    IntCol = 0;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                //end as


                if (Val.ToString(dr["LEDGERNAME"]).Trim().Equals(string.Empty))
                {
                    if (DtabExpense.Rows.Count == 1)
                    {
                        Global.Message("Please Enter Expense A/C");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }
                if (Val.Val(dr["AMOUNT"]) <= 0)
                {
                    Global.Message("Please Enter Amount");
                    IntCol = 1;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }

            }
            if (IntRow >= 0)
            {
                GrdDet.FocusedRowHandle = IntRow;
                GrdDet.FocusedColumn = GrdDet.VisibleColumns[IntCol];
                GrdDet.Focus();
                return true;
            }
            return false;
        }

        private bool ValDelete()
        {
            //if (txtItemGroupCode.Text.Trim().Length == 0)
            //{
            //    Global.Message("Group Code Is Required");
            //    txtItemGroupCode.Focus();
            //    return false;
            //}

            return true;
        }

        #endregion

        #region Enter Event

        private void ControlEnterForGujarati_Enter(object sender, EventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.GUJARATI);
        }
        private void ControlEnterForEnglish_Enter(object sender, EventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.ENGLISH);
        }


        #endregion

        public void Clear()
        {
            DtabExpense.Rows.Clear();
            DtabExpense.Rows.Add(DtabExpense.NewRow());

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValSave())
                {
                    return;
                }

                if (Global.Confirm("Are Your Sure To Save All Records ?") == System.Windows.Forms.DialogResult.No)
                    return;



                TRN_FixedExpenseEntryProperty Property = new TRN_FixedExpenseEntryProperty();

                foreach (DataRow Dr in DtabExpense.Rows)
                {
                    if (Val.ToString(Dr["LEDGERNAME"]).Trim().Equals(string.Empty) || Val.Val(Dr["AMOUNT"]) == 0)
                        continue;

                    Property.EXPENSE_ID = Val.ToString(Dr["EXPENSE_ID"]).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["EXPENSE_ID"]));
                    Property.LEDGER_ID = Val.ToInt64(Dr["LEDGER_ID"]);
                    Property.YYYY = Val.ToInt32(txtYear.Text);
                    Property.MM = Val.ToInt32(CmbMonth.SelectedIndex + 1);
                    Property.AMOUNT = Val.Val(Dr["AMOUNT"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property = ObjMast.Save(Property);
                }

                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnShow_Click(null, null);

                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {
                    //txtItemGroupCode.Focus();
                }
                Property = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }


        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLedger_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnBack_Click(null, null);
            //}
        }



        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Expenses List", GrdDet);
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDet.ShowEditor();
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (!Val.ToString(dr["LEDGERNAME"]).Equals(string.Empty) && Val.ToInt64(dr["LEDGER_ID"]) != 0 && Val.Val(dr["AMOUNT"]) != 0 && GrdDet.IsLastRow)
                    {
                        DtabExpense.Rows.Add(DtabExpense.NewRow());
                        DtabExpense.AcceptChanges();

                    }
                    else if (GrdDet.IsLastRow)
                    {

                        e.Handled = true;
                        BtnSave.Focus();

                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (GrdDet.FocusedRowHandle >= 0)
                {
                    DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                    if (Val.ToString(Drow["EXPENSE_ID"]).Trim().Equals(string.Empty))
                    {
                        Global.Message("Please Select Record That You Want To Delete");
                        return;
                    }

                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        TRN_FixedExpenseEntryProperty Property = new TRN_FixedExpenseEntryProperty();


                        Property.EXPENSE_ID = Guid.Parse(Val.ToString(Drow["EXPENSE_ID"]));
                        Property = ObjMast.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabExpense.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabExpense.AcceptChanges();
                            BtnShow_Click(null, null);

                        }
                        else
                        {
                            Global.Message("ERROR IN DELETE ENTRY");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }


        private void repTxtExpenseLedger_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EXPENSETYPE);

                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                   
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("LEDGERNAME", Val.ToString(FrmSearch.mDRow["LEDGERNAME"]));
                        GrdDet.SetFocusedRowCellValue("LEDGER_ID", Val.ToString(FrmSearch.mDRow["LEDGER_ID"]));

                        DataRow Dr = GrdDet.GetFocusedDataRow();
                        if (CheckDuplicate("LEDGERNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "Expense A/C"))
                        {
                            GrdDet.SetFocusedRowCellValue("LEDGERNAME", Val.ToString(DBNull.Value));
                            GrdDet.SetFocusedRowCellValue("LEDGER_ID", 0);
                        }
                        return;
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("LEDGERNAME", Val.ToString(DBNull.Value));
                        GrdDet.SetFocusedRowCellValue("LEDGER_ID", 0);
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

        private void BtnShow_Click(object sender, EventArgs e)
        {
            TRN_FixedExpenseEntryProperty Property = new TRN_FixedExpenseEntryProperty();
            Property.YYYY = Val.ToInt32(txtYear.Text);
            Property.MM = Val.ToInt32(CmbMonth.SelectedIndex + 1);
            DtabExpense = ObjMast.Fill(Property);

          
            DtabExpense.Rows.Add(DtabExpense.NewRow());
            MainGrid.DataSource = DtabExpense;
            MainGrid.Refresh();
            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.FocusedRowHandle = 0;
            GrdDet.Focus();
            GrdDet.ShowEditor();

        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            TRN_FixedExpenseEntryProperty Property = new TRN_FixedExpenseEntryProperty();

            if (GrdDet.FocusedRowHandle >= 0)
            {
                if (Val.ToString(DtabExpense.Rows[0]["EXPENSE_ID"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Record That You Want To Delete");
                    return;
                }



                if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                {

                    foreach (DataRow Dr in DtabExpense.Rows)
                    {
                        if (Val.ToString(Dr["EXPENSE_ID"]).Trim().Equals(string.Empty))
                            continue;

                        Property.EXPENSE_ID = Guid.Parse(Val.ToString(Dr["EXPENSE_ID"]));
                        Property = ObjMast.Delete(Property);

                        if (Property.ReturnMessageType != "SUCCESS")
                            return;

                    }
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        DtabExpense.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                        DtabExpense.AcceptChanges();
                        BtnShow_Click(null, null);
                    }
                    else
                    {
                        Global.Message("ERROR IN DELETE ENTRY");
                    }

                }
            }
        }

        private void CmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnShow_Click(null, null);
            txtYear.Focus();
        }

        private void txtYear_Validating(object sender, CancelEventArgs e)
        {
            BtnShow_Click(null, null);
            
        }

        private void CmbMonth_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.Enter)
            //    SendKeys.Send("{ENTER}");

        }

        private void txtYear_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            SendKeys.Send("{TAB}");

        }

        private void DTPEntryDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }


    }
}
