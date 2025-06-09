using AxoneMFGRJ.Utility;
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
    public partial class FrmEmployee : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Ledger ObjMast = new BOMST_Ledger();
        
        #region Property Settings

        public FrmEmployee()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            this.Text = "EMPLOYEE MASTER";

            lblAccID.Text = "Emp ID";
            lblAccCode.Text = "Emp Code";

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            txtLedgerCode.Focus();

            BtnAdd_Click(null, null);
            Fill();
            this.Show();
            txtLedgerCode.Focus();

            try
            {
                foreach (BOCaptureDevice device in BOCaptureDevice.GetDevices())
                {
                    cboDevices.Items.Add(device);
                }

                if (cboDevices.Items.Count > 0)
                {
                    cboDevices.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
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
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);            
        }

        #endregion

        #region Validation

        private bool ValSave()
        {

            if (txtLedgerName.Text.Trim().Length == 0)
            {
                Global.Message("Employee Name Is Required");
                txtLedgerName.Focus();
                return false;
            }
            else if (txtLedgerCode.Text.Trim().Length == 0)
            {
                Global.Message("Employee Code Is Required");
                txtLedgerCode.Focus();
                return false;
            }
            else if (txtDepartment.Text.Trim().Length == 0)
            {
                Global.Message("Department Is Required");
                txtDepartment.Focus();
                return false;
            }
            else if (txtDesignation.Text.Trim().Length == 0)
            {
                Global.Message("Designation Is Required");
                txtDesignation.Focus();
                return false;
            }
            if (DTPDateOfLeave.Checked == true)
            {
                if (txtLeaveReason.Text.Trim().Length == 0)
                {
                    Global.Message("Leave Reason Is Required");
                    txtLeaveReason.Focus();
                    return false;
                }
            }
            return true;
        }

        private bool ValDelete()
        {
            if (txtLedgerName.Text.Trim().Length == 0)
            {
                Global.Message("Employee Code Is Required");
                txtLedgerName.Focus();
                return false;
            }

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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            txtLedgerCode.Text = string.Empty;
            txtLedgerName.Text = string.Empty;
            txtLedgerName.Tag = string.Empty;
            txtLedgerID.Text = string.Empty;
            
            ChkGoodsAutoConfirm.Checked = false;

            txtContactPerson.Text = string.Empty;
            txtMobileNo1.Text = string.Empty;
            txtMobileNo2.Text = string.Empty;

            txtPanNo.Text = string.Empty;

            txtBankName.Text = string.Empty;
            txtIFSCCode.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtAccHolderName.Text = string.Empty;

            txtBAddress.Text = string.Empty;
            txtBState.Text = "GUJARAT";
            
            ChkActive.Checked = true;

            txtDepartment.Text = string.Empty;
            txtDepartment.Tag = string.Empty;
            txtDesignation.Text = string.Empty;
            txtDesignation.Tag = string.Empty;
            txtManager.Text = string.Empty;
            txtManager.Tag = string.Empty;
            txtSalary.Text = string.Empty;
            CmbSalaryType.SelectedIndex = 0;

            CmbEmpType.SelectedIndex = 0;
            txtExpSalary.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtAdharNo.Text = string.Empty;
            txtStudy.Text = string.Empty;
            txtIDCardNo.Text = string.Empty;
            txtContactPersonMobile.Text = string.Empty;
            txtContactPersonAddress.Text = string.Empty;
            txtContactPersonCode.Text = string.Empty;
            txtLeaveReason.Text = string.Empty;
            DTPDateOfLeave.Checked = false;

            DTPDateOfJoin.Value = DateTime.Now;
            DTPDateOfBirth.Value = DateTime.Now;

            txtPrevCompany.Text = string.Empty;
            txtPrevDesig.Text = string.Empty;
            txtPrevSalary.Text = string.Empty;
            txtTotalExp.Text = string.Empty;

            PicEmpPhoto.Image = null;

            ChkActive_CheckedChanged(null, null);
            txtLedgerCode.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            LedgerMasterProperty Property = new LedgerMasterProperty();

            Property.LEDGER_ID = Val.ToInt64(txtLedgerID.Text);
            Property.LEDGERCODE = txtLedgerCode.Text;
            Property.LEDGERNAME = txtLedgerName.Text;
            Property.LEDGERGROUP = "EMPLOYEE";
            Property.CONTACTPERSON = txtContactPerson.Text;
            Property.MOBILENO1 = txtMobileNo1.Text;
            Property.MOBILENO2 = txtMobileNo2.Text;

            Property.BANKNAME = txtBankName.Text;
            Property.BANKIFSCCODE = txtIFSCCode.Text;
            Property.BANKACCOUNTNO = txtAccNo.Text;
            Property.BANKACCOUNTNAME = txtAccHolderName.Text;
            Property.PANNO = txtPanNo.Text;
            Property.BILLINGADDRESS = txtBAddress.Text;
            Property.BILLINGSTATE = txtBState.Text;
            Property.SHIPPINGADDRESS = txtBAddress.Text;
            Property.SHIPPINGSTATE = txtBState.Text;

            Property.DEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
            Property.DESIGNATION_ID = Val.ToInt32(txtDesignation.Tag);
            Property.MANAGER_ID = Val.ToInt64(txtManager.Tag);
            Property.SALARY = Val.Val(txtSalary.Text);
            Property.SALARYTYPE= Val.ToString(CmbSalaryType.SelectedItem);
            Property.ISACTIVE = ChkActive.Checked;
            
            Property.EXPSALARY = Val.Val(txtExpSalary.Text);
            Property.USERNAME = txtUserName.Text;
            Property.PASSWORD = txtPassword.Text;
            Property.ADHARNO = txtAdharNo.Text;
            Property.EMPLOYEETYPE = Val.ToString(CmbEmpType.SelectedItem);
            Property.STYDY = txtStudy.Text;
            Property.IDCARDNO = txtIDCardNo.Text;
            Property.CONTACTPERSONMOBILENO = txtContactPersonMobile.Text;
            Property.PREVCOMPANYNAME = txtPrevCompany.Text;
            Property.PREVDESIGNATION = txtPrevDesig.Text;
            Property.PREVSALARY = Val.Val(txtPrevSalary.Text);
            Property.TOTALEXP = Val.Val(txtTotalExp.Text);

            Property.DATEOFBIRTH = Val.SqlDate(DTPDateOfBirth.Value.ToShortDateString());
            Property.DATEOFJOIN =  Val.SqlDate(DTPDateOfJoin.Value.ToShortDateString());
            Property.CONTACTPERSONCODE = txtContactPersonCode.Text;
            Property.CONTACTPERSONADDRESS = txtContactPersonAddress.Text;

            Property.DATEOFLEAVE = null;
            Property.LEAVEREASON = "";
            if (DTPDateOfLeave.Checked == true && ChkActive.Checked == false)
	        {
                Property.DATEOFLEAVE = Val.SqlDate(txtTotalExp.Text);
                Property.LEAVEREASON = txtLeaveReason.Text;
            }
            
            Property.EMPPHOTO = null;
            if (PicEmpPhoto.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                PicEmpPhoto.Image.Save(ms, ImageFormat.Png);
                byte[] Byte = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(Byte, 0, Byte.Length);
                Property.EMPPHOTO = Byte;
            }
            Property.AUTOCONFIRM = ChkGoodsAutoConfirm.Checked;
            Property = ObjMast.Save(Property);

            Global.Message(Property.ReturnMessageDesc);

            if (Property.ReturnMessageType == "SUCCESS")
            {
                Fill();
                BtnAdd_Click(null, null);

                if (GrdDet.RowCount > 1)
                {
                    GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                }
            }
            else
            {
                txtLedgerName.Focus();
            }

        }

        public void Fill()
        {
            string StrLedgerGroup = "";
            StrLedgerGroup = "EMPLOYEE";
            GrdDet.Columns["LEDGERNAME"].Caption = "Employee Name";

            GrdDet.Columns["DEPARTMENTNAME"].Visible = true;
            GrdDet.Columns["DESIGNATIONNAME"].Visible = true;
            GrdDet.Columns["MANAGERNAME"].Visible = true;
            GrdDet.Columns["SALARYTYPE"].Visible = true;
            GrdDet.Columns["SALARY"].Visible = true;
            GrdDet.Columns["LEDGERGROUP"].Visible = false;

            string StrActive = "";
            if (RbtAll.Checked == true)
            {
                StrActive = "ALL";
            }
            else if (RbtActive.Checked == true)
            {
                StrActive = "ACTIVE";
            }
            else if (RbtDeactive.Checked == true)
            {
                StrActive = "DEACTIVE";
            }


            DataTable DTab = ObjMast.Fill(StrLedgerGroup, StrActive);
            MainGrid.DataSource = DTab;

            lblTotal.Text = "Total Record : " + DTab.Rows.Count.ToString();
            
            MainGrid.Refresh();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            LedgerMasterProperty Property = new LedgerMasterProperty();
            try
            {

                if (Global.Confirm("Are Your Sure To Delete The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;


                Property.LEDGER_ID = Val.ToInt64(txtLedgerID.Text);
                Property = ObjMast.Delete(Property);
                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnAdd_Click(null, null);
                    Fill();
                }
                else
                {
                    txtLedgerName.Focus();
                }

            }
            catch (System.Exception ex)
            {
                Global.MessageToster(ex.Message);
            }
            Property = null;
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

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
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
        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow DR = GrdDet.GetFocusedDataRow();
                FetchValue(DR);
                DR = null;
            }
        }

        public void FetchValue(DataRow DR)
        {
            txtLedgerCode.Text = Val.ToString(DR["LEDGERCODE"]);
            txtLedgerName.Text = Val.ToString(DR["LEDGERNAME"]);
            txtLedgerID.Text = Val.ToString(DR["LEDGER_ID"]);

            txtContactPerson.Text = Val.ToString(DR["CONTACTPERSON"]);
            txtMobileNo1.Text = Val.ToString(DR["MOBILENO1"]);
            txtMobileNo2.Text = Val.ToString(DR["MOBILENO2"]);

            txtBankName.Text = Val.ToString(DR["BANKNAME"]);
            txtIFSCCode.Text = Val.ToString(DR["BANKIFSCCODE"]);
            txtAccHolderName.Text = Val.ToString(DR["BANKACCOUNTNAME"]);
            txtAccNo.Text = Val.ToString(DR["BANKACCOUNTNO"]);
            txtBAddress.Text = Val.ToString(DR["BILLINGADDRESS"]);
            txtBState.Text = Val.ToString(DR["BILLINGSTATE"]);

            txtBAddress.Text = Val.ToString(DR["SHIPPINGADDRESS"]);

            txtBState.Text = Val.ToString(DR["SHIPPINGSTATE"]);

            txtPanNo.Text = Val.ToString(DR["PANNO"]);

            ChkActive.Checked = Val.ToBoolean(DR["ISACTIVE"]);
            txtDepartment.Tag = Val.ToString(DR["DEPARTMENT_ID"]);
            txtDepartment.Text = Val.ToString(DR["DEPARTMENTNAME"]);

            txtDesignation.Tag= Val.ToString(DR["DESIGNATION_ID"]);
            txtDesignation.Text = Val.ToString(DR["DESIGNATIONNAME"]);

            txtManager.Tag= Val.ToString(DR["MANAGER_ID"]);
            txtManager.Text = Val.ToString(DR["MANAGERNAME"]);

            txtSalary.Text = Val.ToString(DR["SALARY"]);
            CmbSalaryType.SelectedItem = Val.ToString(DR["SALARYTYPE"]);

            txtExpSalary.Text = Val.ToString(DR["EXPSALARY"]);
            txtUserName.Text = Val.ToString(DR["USERNAME"]);
            txtPassword.Text = Val.ToString(DR["PASSWORD"]);
            txtAdharNo.Text = Val.ToString(DR["ADHARNO"]);
            CmbEmpType.SelectedItem = Val.ToString(DR["EMPLOYEETYPE"]);
            txtStudy.Text = Val.ToString(DR["STYDY"]);
            txtIDCardNo.Text = Val.ToString(DR["IDCARDNO"]);
            txtContactPersonMobile.Text = Val.ToString(DR["CONTACTPERSONMOBILENO"]);
            txtContactPersonCode.Text = Val.ToString(DR["CONTACTPERSONCODE"]);
            txtContactPersonAddress.Text = Val.ToString(DR["CONTACTPERSONADDRESS"]);

            DTPDateOfBirth.Text = Val.ToString(DR["DATEOFBIRTH"]);
            DTPDateOfJoin.Text = Val.ToString(DR["DATEOFJOIN"]);

            if (Val.IsDate(Val.ToString(DR["DATEOFLEAVE"])) == true)
            {
                PanelResign.Enabled = true;
                DTPDateOfLeave.Checked = true;
                DTPDateOfLeave.Text = Val.ToString(DR["DATEOFLEAVE"]);
                txtLeaveReason.Text = Val.ToString(DR["LEAVEREASON"]);
            }
            else
            {
                PanelResign.Enabled = false;
                DTPDateOfLeave.Checked = false;
                txtLeaveReason.Text = "";
            }
            
            txtPrevCompany.Text = Val.ToString(DR["PREVCOMPANYNAME"]);
            txtPrevDesig.Text = Val.ToString(DR["PREVDESIGNATION"]);
            txtPrevSalary.Text = Val.ToString(DR["PREVSALARY"]);
            txtTotalExp.Text = Val.ToString(DR["TOTALEXP"]);
            ChkGoodsAutoConfirm.Checked = Val.ToBoolean(DR["AUTOCONFIRM"]);

            byte[] OFFICELOGO = DR["EMPPHOTO"] as byte[] ?? null;
            if (OFFICELOGO != null)
            {
                using (MemoryStream ms = new MemoryStream(OFFICELOGO))
                {
                    PicEmpPhoto.Image = Image.FromStream(ms);
                }
            }
            else
            {
                PicEmpPhoto.Image = null;
            }

            txtLedgerName.Focus();

            xtraTabControl1.SelectedTabPageIndex = 0;
        }


        private void txtBState_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "STATENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_STATE);

                    FrmSearch.mColumnsToHide = "STATE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtBState.Text = Val.ToString(FrmSearch.mDRow["STATENAME"]);
                        txtBState.Tag = Val.ToString(FrmSearch.mDRow["STATE_ID"]);
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

        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DEPARTMENTCODE,DEPARTMENTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);

                    FrmSearch.mColumnsToHide = "DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
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

        private void txtDesignation_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DESIGNATIONCODE,DESIGNATIONNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DESIGNATION);

                    FrmSearch.mColumnsToHide = "DESIGNATION_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtDesignation.Text = Val.ToString(FrmSearch.mDRow["DESIGNATIONNAME"]);
                        txtDesignation.Tag = Val.ToString(FrmSearch.mDRow["DESIGNATION_ID"]);
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

        private void txtManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtManager.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtManager.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void BtnAddDepartment_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmParameter);
            FrmParameter.ShowForm("DEPARTMENT");
        }

        private void BtnAddManager_Click(object sender, EventArgs e)
        {
            FrmLedger FrmLedger = new FrmLedger();
            FrmLedger.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmLedger);
            FrmLedger.ShowForm("EMPLOYEE");
        }

        private void BtnAddDesignation_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmParameter);
            FrmParameter.ShowForm("DESIGNATION");
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("EmployeeList", GrdDet);
        }

        private void lblBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();
            if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (PicEmpPhoto.Image != null)
                {
                    PicEmpPhoto.Image.Dispose();
                    PicEmpPhoto.Image = null;
                }

                PicEmpPhoto.Image = Image.FromFile(OpenDialog.FileName);
            }
            OpenDialog.Dispose();
            OpenDialog = null;
        }

        private void ChkActive_CheckedChanged(object sender, EventArgs e)
        {
            PanelResign.Enabled = !ChkActive.Checked;
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void txtLedgerCode_Validated(object sender, EventArgs e)
        {
            if (ChkCodeUpdate.Checked == false)
            {
                string Str = txtLedgerCode.Text;

                BtnAdd_Click(null, null);

                txtLedgerCode.Text = Str;
                DataRow DRow = ObjMast.GetLedgerInfoByCode("EMPLOYEE", txtLedgerCode.Text);
                if (DRow != null)
                {
                    FetchValue(DRow);
                }

                txtLedgerName.Focus();
            }
            else
            {
                DataRow DRow = ObjMast.GetLedgerInfoByCode("EMPLOYEE", txtLedgerCode.Text);
                if (DRow != null)
                {
                    if (Global.Confirm("This Employee Code Record Is Already Exists , [YES : For UPdate , NO : Refresh Record] ?") == System.Windows.Forms.DialogResult.No)
                    {
                        BtnAdd_Click(null, null);
                        FetchValue(DRow);
                    }
                }

            }
        }

        private void txtContactPersonCode_Validated(object sender, EventArgs e)
        {
            if (txtContactPersonCode.Text.Trim().Length != 0)
            {
                txtContactPersonMobile.Text = string.Empty;
                txtContactPerson.Text = string.Empty;
                txtContactPersonAddress.Text = string.Empty;

                DataRow DRow = ObjMast.GetLedgerInfoByCode("EMPLOYEE", txtContactPersonCode.Text);
                if (DRow != null)
                {
                    txtContactPerson.Text = Val.ToString(DRow["LEDGERNAME"]);
                    txtContactPersonMobile.Text = Val.ToString(DRow["MOBILENO1"]);
                    txtContactPersonAddress.Text = Val.ToString(DRow["BILLINGADDRESS"]);
                }
            }
            
        }

        private void txtLedgerCode_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim().Length == 0 && txtLedgerCode.Text.Length != 0 )
            {
                txtUserName.Text = txtLedgerCode.Text;
                txtPassword.Text = txtLedgerCode.Text;
            }
        }

        private void RbtAll_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "ISACTIVE")) == true)
            {
                e.Appearance.BackColor = Color.Transparent;
                e.Appearance.BackColor2 = Color.Transparent;
            }
            else if (Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "ISACTIVE")) == false)
            {
                e.Appearance.BackColor = RbtDeactive.BackColor;
                e.Appearance.BackColor2 = RbtDeactive.BackColor;
            }
        }

        private void lblWebCam_Click(object sender, EventArgs e)
        {
            try
            {
                int index = cboDevices.SelectedIndex;
                if (index != -1)
                {

                    ((BOCaptureDevice)cboDevices.SelectedItem).Attach(PicEmpPhoto);
                }

            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }

        private void lblCapture_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboDevices.SelectedItem == null)
                {
                    Global.MessageError("Capture Device Not Found.");
                    return;
                }
                if (((BOCaptureDevice)cboDevices.SelectedItem).Capture() != null)
                {

                    Image image = ((BOCaptureDevice)cboDevices.SelectedItem).Capture();
                    PicEmpPhoto.Image = image;
                    ((BOCaptureDevice)cboDevices.SelectedItem).Detach();
                }
                else
                {
                    Global.MessageError("Image Not Captured.");
                }

            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }

        private void BtnDepartmentUpdate_Click(object sender, EventArgs e)
        {
            LedgerMasterProperty Property = new LedgerMasterProperty();
            try
            {

                if (Global.Confirm("Are Your Sure To Update Department In ALl Transaction ?") == System.Windows.Forms.DialogResult.No)
                    return;


                Property.LEDGER_ID = Val.ToInt64(txtLedgerID.Text);
                Property = ObjMast.UpdateDepartmentInAllTransaction(Property);
                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnAdd_Click(null, null);
                    Fill();
                }
                else
                {
                    txtLedgerName.Focus();
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
