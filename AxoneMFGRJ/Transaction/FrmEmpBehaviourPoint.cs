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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using BusLib.Master;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmEmpBehaviourPoint : DevExpress.XtraEditors.XtraForm
    {
        
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_EmployeeBehaviourPoint ObjMast = new BOTRN_EmployeeBehaviourPoint();

        #region Property Settings

        public FrmEmpBehaviourPoint()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DataTable DtabEmp = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGEREMPLOYEE);
            ChkCmbEmp.Properties.DataSource = DtabEmp;
            ChkCmbEmp.Properties.DisplayMember = "LEDGERNAME";
            ChkCmbEmp.Properties.ValueMember = "LEDGER_ID";

            this.Show();

            Clear();

            DtpFromDate.Value = DateTime.Now.AddMonths(-1);
            DtpToDate.Value = DateTime.Now;

            Fill();
            
            
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

        #region Validation

        private bool ValSave()
        {

            if (txtEmpName.Text.Trim().Length == 0)
            {
                Global.Message("Employee Name Is Required");
                txtEmpName.Focus();
                return false;
            }
            else if (txtCode.Text.Trim().Length == 0)
            {
                Global.Message("Employee Code Is Required");
                txtCode.Focus();
                return false;
            }
            else if (txtPlusPoint.Text.Trim().Length == 0)
            {
                Global.Message("Plus Point Is Required");
                txtPlusPoint.Focus();
                return false;
            }
            else if (txtMinusPoint.Text.Trim().Length == 0)
            {
                Global.Message("Minus Point Is Required");
                txtMinusPoint.Focus();
                return false;
            }

            return true;
        }

        #endregion

        public void Clear()
        {
            txtBehaviouID.Tag = string.Empty;
            txtEmpName.Tag = string.Empty;
            txtEmpName.Text = string.Empty;
            txtCode.Tag = string.Empty;
            txtCode.Text = string.Empty;
            txtPlusPoint.Text = string.Empty;
            txtMinusPoint.Text = string.Empty;
            txtRemark.Text = string.Empty;
            DtpDate.Value = DateTime.Now;
            MainGrid.Refresh();
        }
          
        public void Fill()
        {
            string StrEmp_ID = Val.Trim(ChkCmbEmp.Properties.GetCheckedItems());
            string StrFromDate = null;
            string StrToDate = null;

            StrFromDate = Val.SqlDate(DtpFromDate.Text);
            StrToDate = Val.SqlDate(DtpToDate.Text);

            DataTable Dtab = ObjMast.Fill(StrFromDate, StrToDate, StrEmp_ID);
            MainGrid.DataSource = Dtab;
            Dtab.AcceptChanges();
            MainGrid.Refresh();

        }

        public void FetchValue(DataRow Dr)
        {
            txtBehaviouID.Tag = Guid.Parse(Val.ToString(Dr["BEHAVIOUR_ID"]));
            txtEmpName.Text = Val.ToString(Dr["EMPLOYEE"]);
            txtEmpName.Tag = Val.ToString(Dr["EMPLOYEE_ID"]);
            txtCode.Text = Val.ToString(Dr["EMPLOYEECODE"]);
            txtPlusPoint.Text = Val.ToString(Dr["PLUSPOINT"]);
            txtMinusPoint.Text = Val.ToString(Dr["MINUSPOINT"]);
            txtRemark.Text = Val.ToString(Dr["REMARK"]);
            DtpDate.Text = Val.SqlDate(Val.ToString(Dr["ENTRYDATE"]));

        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Fill();
        }


        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                TrnEmployeeBehaviourPointProperty Property = new TrnEmployeeBehaviourPointProperty();
                Property.BEHAVIOUR_ID = Val.ToString(txtBehaviouID.Tag).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(txtBehaviouID.Tag));
                Property.EMPLOYEE_ID = Val.ToInt64(txtEmpName.Tag);
                Property.PLUSPOINT = Val.Val(txtPlusPoint.Text);
                Property.MINUSPOINT = Val.Val(txtMinusPoint.Text);
                Property.REMARK = Val.ToString(txtRemark.Text);
                Property.DATE = Val.SqlDate(DtpDate.Value.ToShortDateString());

                Property = ObjMast.Save(Property);
                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    
                    Fill();
                    Clear();

                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {
                    txtCode.Focus();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtEmpName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERNAME, LEDGERCODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGEREMPLOYEE);

                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmpName.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtEmpName.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                        txtCode.Text = Val.ToString(FrmSearch.mDRow["LEDGERCODE"]);
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

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERNAME, LEDGERCODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGEREMPLOYEE);

                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmpName.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtEmpName.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                        txtCode.Text = Val.ToString(FrmSearch.mDRow["LEDGERCODE"]);
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void GrdDet_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                if (e.Clicks == 2)
                {
                    DataRow DR = GrdDet.GetDataRow(e.RowHandle);
                    FetchValue(DR);
                    DR = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow DR = GrdDet.GetFocusedDataRow();
                FetchValue(DR);
                DR = null;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            TrnEmployeeBehaviourPointProperty Property = new TrnEmployeeBehaviourPointProperty();
            try
            {

                if (Global.Confirm("Are Your Sure To Delete The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;


                Property.BEHAVIOUR_ID = Guid.Parse(Val.ToString(txtBehaviouID.Tag));
                Property = ObjMast.Delete(Property);
                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Clear();
                    Fill();
                }
                else
                {
                    txtCode.Focus();
                }

            }
            catch (System.Exception ex)
            {
                Global.MessageToster(ex.Message);
            }
            Property = null;
        }

    }
}
