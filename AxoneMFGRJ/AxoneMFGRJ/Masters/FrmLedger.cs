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
    public partial class FrmLedger : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Ledger ObjMast = new BOMST_Ledger();
		DataTable DtabAttachment = new DataTable();
        BOFormPer ObjPer = new BOFormPer();

        #region Property Settings

        public FrmLedger()
        {
            InitializeComponent();
        }
        public void ShowForm(string pStrFormType)
        {
            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            BtnDelete.Enabled = ObjPer.ISDELETE;

            if (pStrFormType == "COMPANY")
            {
                this.Text = "COMPANY MASTER";
                CmbLedgerGroup.SelectedItem = "COMPANY";
                CmbLedgerGroup.Enabled = false;

                lblAccID.Text = "Comp ID";
                lblAccGrp.Text = "Comp Grp";
                lblAccCode.Text = "Comp Code";
            }
            else if (pStrFormType == "EMPLOYEE")
            {
                this.Text = "EMPLOYEE MASTER";
                CmbLedgerGroup.SelectedItem = "EMPLOYEE";
                CmbLedgerGroup.Enabled = false;

                groupBox1.Text = "Address";
                groupBox3.Visible = true;
                groupBox2.Visible = false;

                lblAccID.Text = "Emp ID";
                lblAccGrp.Text = "Emp Grp";
                lblAccCode.Text = "Emp Code";
            }
            else if (pStrFormType == "LEDGER")
            {
                this.Text = "ACCOUNT MASTER";

                CmbLedgerGroup.Items.Remove("COMPANAY");
                CmbLedgerGroup.Items.Remove("EMPLOYEE");
                CmbLedgerGroup.Items.Remove("LOCATION");
                CmbLedgerGroup.Enabled = true;

                lblAccID.Text = "Acct ID";
                lblAccGrp.Text = "Acct Grp";
                lblAccCode.Text = "Acct Code";
            }
            else if (pStrFormType == "LOCATION")
            {
                this.Text = "LOCATION MASTER";
                CmbLedgerGroup.SelectedItem = "LOCATION";
                CmbLedgerGroup.Enabled = false;
                lblAccID.Text = "Loc ID";
                lblAccGrp.Text = "Loc Grp";
                lblAccCode.Text = "Loc Code";
            }

            CmbShape.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
            CmbShape.Properties.DisplayMember = "SHAPECODE";
            CmbShape.Properties.ValueMember = "SHAPE_ID";

            txtLedgerCode.Text = Val.ToString(ObjMast.FindMaxLedgerCode());

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();


            string[] StrTrnType = System.Configuration.ConfigurationManager.AppSettings["TrnType"].ToString().Split(',');
            CmbTrnTypeDebit.Items.Clear();
            CmbTrnTypeCredit.Items.Clear();

            foreach (string Str in StrTrnType)
            {
                CmbTrnTypeDebit.Items.Add(Str);
                CmbTrnTypeCredit.Items.Add(Str);
            }
            CmbTrnTypeDebit.SelectedIndex = 0;
            CmbTrnTypeCredit.SelectedIndex = 0;
            CmbLedgerGroup.Focus();

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

            BtnAdd_Click(null, null);
            Fill();
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
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        #region Validation

        private bool ValSave()
        {

            if (txtLedgerName.Text.Trim().Length == 0)
            {
                Global.Message("Ledger Name Is Required");
                txtLedgerName.Focus();
                return false;
            }
            else if (txtLedgerCode.Text.Trim().Length == 0)
            {
                Global.Message("Ledger Code Is Required");
                txtLedgerCode.Focus();
                return false;
            }

            else if (txtDepartment.Text.Trim().Length == 0)
            {
                Global.Message("Department Code Is Required");
                txtDepartment.Focus();
                return false;
            }

            else if (Val.ToString(CmbLedgerGroup.SelectedItem) == "PURCHASE" ||
                Val.ToString(CmbLedgerGroup.SelectedItem) == "SALE" ||
                Val.ToString(CmbLedgerGroup.SelectedItem) == "COMPANY"
                )
            {

                if (txtBState.Text.Trim().Length == 0)
                {
                    Global.Message("Billing State Is Required");
                    txtBState.Focus();
                    return false;
                }

                if (txtSState.Text.Trim().Length == 0)
                {
                    Global.Message("Shipping State Is Required");
                    txtSState.Focus();
                    return false;
                }

            }

            return true;
        }

        private bool ValDelete()
        {
            if (txtLedgerName.Text.Trim().Length == 0)
            {
                Global.Message("Ledger Code Is Required");
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


        public void Clear()
        {
            txtLedgerCode.Text = Val.ToString(ObjMast.FindMaxLedgerCode());
            txtLedgerName.Text = string.Empty;
            txtLedgerName.Tag = string.Empty;
            txtLedgerID.Text = string.Empty;
            txtLedgerID.Tag = string.Empty;
            txtLedgerNameGujarati.Text = string.Empty;

            txtOpeningCredit.Text = string.Empty;
            txtOpeningDebit.Text = string.Empty;

            txtContactPerson.Text = string.Empty;
            txtMobileNo1.Text = string.Empty;
            txtMobileNo2.Text = string.Empty;

            txtGSTNo.Text = string.Empty;
            txtCSTNo.Text = string.Empty;
            txtPanNo.Text = string.Empty;

            txtBankName.Text = string.Empty;
            txtIFSCCode.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtAccHolderName.Text = string.Empty;

            txtBAddress.Text = string.Empty;
            txtBState.Text = "GUJARAT";

            txtSAddress.Text = string.Empty;
            txtSState.Text = "GUJARAT";

            ChkActive.Checked = true;
            ChkGoodsAutoConfirm.Checked = true;

            txtDepartment.Text = string.Empty;
            txtDepartment.Tag = string.Empty;
            txtDesignation.Text = string.Empty;
            txtDesignation.Tag = string.Empty;
            txtManager.Text = string.Empty;
            txtManager.Tag = string.Empty;
            txtSalary.Text = string.Empty;
            CmbSalaryType.SelectedIndex = 0;

            CmbShape.SetEditValue(0);

			PicEmpPhoto.Image = null;

            CmbTrnTypeCredit.SelectedIndex = 0;
            CmbTrnTypeDebit.SelectedItem = 0;

            if (this.Text == "COMPANY MASTER")
            {
                txtLedgerCode.Focus();
            }
            else if (this.Text == "EMPLOYEE MASTER")
            {
                txtLedgerCode.Focus();
            }
            else if (this.Text == "LOCATION MASTER")
            {
                txtLedgerCode.Focus();
            }
            else
            {
                CmbLedgerGroup.SelectedIndex = 1;
                CmbLedgerGroup.Focus();
            }
			FillLedgerDetailInfo(Val.ToInt64(txtLedgerID.Text));
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();
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
			Property.LEDGERNAMEGUJARATI = txtLedgerNameGujarati.Text;
			Property.LEDGERGROUP = Val.ToString(CmbLedgerGroup.SelectedItem);
			Property.CONTACTPERSON = txtContactPerson.Text;
			Property.MOBILENO1 = txtMobileNo1.Text;
			Property.MOBILENO2 = txtMobileNo2.Text;

            Property.SHAPE_ID = Val.Trim(CmbShape.Properties.GetCheckedItems());

			Property.CREDITAMOUNT = Val.Val(txtOpeningCredit.Text);
			Property.TRNTYPECREDIT = Val.ToString(CmbTrnTypeCredit.SelectedItem);

			Property.DEBITAMOUNT = Val.Val(txtOpeningDebit.Text);
			Property.TRNTYPEDEBIT = Val.ToString(CmbTrnTypeDebit.SelectedItem);

			Property.BANKNAME = txtBankName.Text;
			Property.BANKIFSCCODE = txtIFSCCode.Text;
			Property.BANKACCOUNTNO = txtAccNo.Text;
			Property.BANKACCOUNTNAME = txtAccHolderName.Text;
			Property.GSTNO = txtGSTNo.Text;
			Property.CSTNO = txtCSTNo.Text;
			Property.PANNO = txtPanNo.Text;
			Property.BILLINGADDRESS = txtBAddress.Text;
			Property.BILLINGSTATE = txtBState.Text;
			Property.SHIPPINGADDRESS = txtSAddress.Text;
			Property.SHIPPINGSTATE = txtSState.Text;

			Property.DEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
			Property.DESIGNATION_ID = Val.ToInt32(txtDesignation.Tag);
			Property.MANAGER_ID = Val.ToInt64(txtManager.Tag);
			Property.SALARY = Val.Val(txtSalary.Text);
			Property.SALARYTYPE = Val.ToString(CmbSalaryType.SelectedItem);

			Property.ISACTIVE = ChkActive.Checked;
			Property.AUTOCONFIRM = ChkGoodsAutoConfirm.Checked;
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

			Property = ObjMast.Save(Property);

			string StrReturnDesc = Property.ReturnMessageDesc;

			
				if (Property.ReturnMessageType == "SUCCESS")
				{
					//int MaxSrNo = 0;
					//MaxSrNo = (int)DtabAttachment.Compute("Max(SRNO)", "");
					DataRow[] dr = DtabAttachment.Select("DOCUMENTTYPE = 'PROFILE'");
					if (dr.Length > 0)
					{
						dr[0]["SRNO"] = 0;
						dr[0]["UPLOADIMAGE"] = Property.EMPPHOTO;
						dr[0]["DOCUMENTTYPE"] = "PROFILE";
					}
					else
					{
						string Str = PicEmpPhoto.ImageLocation;
						DataRow DRA = DtabAttachment.NewRow();
						DRA["SRNO"] = 0;
						DRA["DOCUMENTTYPE"] = "PROFILE";
						DRA["UPLOADIMAGE"] = Property.EMPPHOTO;
						DRA["UPLOADFILENAME"] = "EmployeeProfileImage";
						DtabAttachment.Rows.Add(DRA);
					}


					Property.LEDGER_ID = Val.ToInt64(Property.ReturnValue);
					Property = ObjMast.SaveLedgerDetailInfo(Property, null, null, null, DtabAttachment, null, null);
				}

				this.Cursor = Cursors.Default;
				Global.Message(StrReturnDesc);

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
            if (this.Text == "COMPANY MASTER")
            {
                StrLedgerGroup = "COMPANY";
                GrdDet.Columns["LEDGERNAME"].Caption = "Company Name";

                GrdDet.Columns["DEPARTMENTNAME"].Visible = false;
                GrdDet.Columns["DESIGNATIONNAME"].Visible = false;
                GrdDet.Columns["MANAGERNAME"].Visible = false;
                GrdDet.Columns["SALARYTYPE"].Visible = false;
                GrdDet.Columns["SALARY"].Visible = false;
            }
            else if (this.Text == "EMPLOYEE MASTER")
            {
                StrLedgerGroup = "EMPLOYEE";
                GrdDet.Columns["LEDGERNAME"].Caption = "Employee Name";

                GrdDet.Columns["DEPARTMENTNAME"].Visible = true;
                GrdDet.Columns["DESIGNATIONNAME"].Visible = true;
                GrdDet.Columns["MANAGERNAME"].Visible = true;
                GrdDet.Columns["SALARYTYPE"].Visible = true;
                GrdDet.Columns["SALARY"].Visible = true;
            }
            else if (this.Text == "LOCATION MASTER")
            {
                StrLedgerGroup = "LOCATION";
                GrdDet.Columns["LEDGERNAME"].Caption = "Location Name";

                GrdDet.Columns["DEPARTMENTNAME"].Visible = false;
                GrdDet.Columns["DESIGNATIONNAME"].Visible = false;
                GrdDet.Columns["MANAGERNAME"].Visible = false;
                GrdDet.Columns["SALARYTYPE"].Visible = false;
                GrdDet.Columns["SALARY"].Visible = false;
            }
            else
            {
                StrLedgerGroup = "OTHER";
                GrdDet.Columns["DEPARTMENTNAME"].Visible = false;
                GrdDet.Columns["DESIGNATIONNAME"].Visible = false;
                GrdDet.Columns["MANAGERNAME"].Visible = false;
                GrdDet.Columns["SALARYTYPE"].Visible = false;
                GrdDet.Columns["SALARY"].Visible = false;
            }
            

            DataTable DTab = ObjMast.Fill(StrLedgerGroup, "ALL");
            MainGrid.DataSource = DTab;

            lblTotal.Text = "Total Record : " + DTab.Rows.Count.ToString();

			FillLedgerDetailInfo(Val.ToInt64(txtLedgerID.Text));

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
            CmbLedgerGroup.SelectedItem = Val.ToString(DR["LEDGERGROUP"]);
            txtLedgerCode.Text = Val.ToString(DR["LEDGERCODE"]);
            txtLedgerName.Text = Val.ToString(DR["LEDGERNAME"]);
            txtLedgerID.Text = Val.ToString(DR["LEDGER_ID"]);

            txtLedgerNameGujarati.Text = Val.ToString(DR["LEDGERNAMEGUJARATI"]);

            txtContactPerson.Text = Val.ToString(DR["CONTACTPERSON"]);
            txtMobileNo1.Text = Val.ToString(DR["MOBILENO1"]);
            txtMobileNo2.Text = Val.ToString(DR["MOBILENO2"]);

            txtOpeningCredit.Text = Val.ToString(DR["CREDITAMOUNT"]);
            CmbTrnTypeCredit.SelectedItem = Val.ToString(DR["TRNTYPECREDIT"]);

            txtOpeningDebit.Text = Val.ToString(DR["DEBITAMOUNT"]);
            CmbTrnTypeDebit.SelectedItem = Val.ToString(DR["TRNTYPEDEBIT"]);

            txtBankName.Text = Val.ToString(DR["BANKNAME"]);
            txtIFSCCode.Text = Val.ToString(DR["BANKIFSCCODE"]);
            txtAccHolderName.Text = Val.ToString(DR["BANKACCOUNTNAME"]);
            txtAccNo.Text = Val.ToString(DR["BANKACCOUNTNO"]);
            txtBAddress.Text = Val.ToString(DR["BILLINGADDRESS"]);
            txtBState.Text = Val.ToString(DR["BILLINGSTATE"]);

            txtSAddress.Text = Val.ToString(DR["SHIPPINGADDRESS"]);

            txtSState.Text = Val.ToString(DR["SHIPPINGSTATE"]);

            txtGSTNo.Text = Val.ToString(DR["GSTNO"]);
            txtCSTNo.Text = Val.ToString(DR["CSTNO"]);
            txtPanNo.Text = Val.ToString(DR["PANNO"]);

            ChkActive.Checked = Val.ToBoolean(DR["ISACTIVE"]);
            txtDepartment.Tag = Val.ToString(DR["DEPARTMENT_ID"]);
            txtDepartment.Text = Val.ToString(DR["DEPARTMENTNAME"]);

            txtDesignation.Tag= Val.ToString(DR["DESIGNATION_ID"]);
            txtDesignation.Text = Val.ToString(DR["DESIGNATIONNAME"]);

            txtManager.Tag= Val.ToString(DR["MANAGER_ID"]);
            txtManager.Text = Val.ToString(DR["MANAGERNAME"]);

            CmbShape.SetEditValue(Val.ToString(DR["SHAPE_ID"]));

            txtSalary.Text = Val.ToString(DR["SALARY"]);
            CmbSalaryType.SelectedItem = Val.ToString(DR["SALARYTYPE"]);
            ChkGoodsAutoConfirm.Checked = Val.ToBoolean(DR["AUTOCONFIRM"]);

            FillLedgerDetailInfo(Val.ToInt64(txtLedgerID.Text));  //06-04-2019

            txtLedgerName.Focus();

            xtraTabControl1.SelectedTabPageIndex = 0;
        }
		public void FillLedgerDetailInfo(Int64 IntLedger_ID)
        {
            //Ledger Details
            DataSet DsDetailInfo = ObjMast.GetledgerDetailDInfoata(Val.ToInt64(IntLedger_ID));

            //Attachment Details
            DtabAttachment = DsDetailInfo.Tables[3];

			if (DtabAttachment.Rows.Count > 0)
			{
				//Display Profile Image
				DataRow[] DR = DtabAttachment.Select("DOCUMENTTYPE ='PROFILE'");
				if (DR.Length > 0)
				{
					byte[] OFFICELOGO = DR[0]["UPLOADIMAGE"] as byte[] ?? null;
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

				}
				else
				{
					PicEmpPhoto.Image = null;
				}


				DataView dv = DtabAttachment.DefaultView;
				dv.RowFilter = "DOCUMENTTYPE <> 'PROFILE'";
				DtabAttachment = dv.ToTable();

				if (DtabAttachment.Rows.Count > 0)
				{
					int MaxSrNo = 0;
					MaxSrNo = (int)DtabAttachment.Compute("Max(SRNO)", "");
					DataRow Dr = DtabAttachment.NewRow();
					Dr["SRNO"] = MaxSrNo + 1;
					DtabAttachment.Rows.Add(Dr);
					DtabAttachment.AcceptChanges();
				}
				else
				{
					DataRow Dr = DtabAttachment.NewRow();
					Dr["SRNO"] = 1;
					DtabAttachment.Rows.Add(Dr);
					DtabAttachment.AcceptChanges();
				}

			}
			else
			{
				PicEmpPhoto.Image = null;
			}
		}


        private void txtLedgerCode_Validated(object sender, EventArgs e)
        {
            string StrGroup = string.Empty;
            if (this.Text == "COMPANY MASTER")
            {
                StrGroup = "COMPANY";
            }
            else if (this.Text == "ACCOUNT MASTER")
            {
                StrGroup = "OTHER";
            }

            string Str = txtLedgerCode.Text;

//            BtnAdd_Click(null, null);

            txtLedgerCode.Text = Str;
            DataRow DRow = ObjMast.GetLedgerInfoByCode(StrGroup, txtLedgerCode.Text);
            if (DRow != null)
            {
                FetchValue(DRow);
            }

            txtLedgerName.Focus();
        }

        public string Transliterate(string latinCharacters)
        {
            StringBuilder gujarati = new StringBuilder(latinCharacters.Length);
            for (int i = 0; i < latinCharacters.Length; i++)
            {
                switch (char.ToLower(latinCharacters[i]))
                {
                    case 'a':
                        gujarati.Append('\u0abe');
                        break;
                    case 'h':
                        gujarati.Append('\u0ab9');
                        break;
                    case 'j':
                        gujarati.Append('\u0a9c');
                        break;
                    case 'l':
                        gujarati.Append('\u0ab2');
                        break;
                    case 'm':
                        gujarati.Append('\u0aae');
                        break;
                    case 't':
                        gujarati.Append('\u0aa4');
                        break;
                }
            }
            return gujarati.ToString();
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

        private void txtSState_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtSState.Text = Val.ToString(FrmSearch.mDRow["STATENAME"]);
                        txtSState.Tag = Val.ToString(FrmSearch.mDRow["STATE_ID"]);
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

        private void lblSameAsBilling_Click(object sender, EventArgs e)
        {
            txtSAddress.Text = txtBAddress.Text;
            txtSState.Text = txtBState.Text;
            txtSState.Tag = txtBState.Tag;
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
                    else
                    {
                        txtDesignation.Text = Val.ToString(DBNull.Value);
                        txtDesignation.Tag = Val.ToString(DBNull.Value);
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
                    else
                    {
                        txtManager.Text = Val.ToString(DBNull.Value);
                        txtManager.Tag = Val.ToString(DBNull.Value);
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
            Global.ExcelExport("LedgerList", GrdDet);
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
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

    }
}
