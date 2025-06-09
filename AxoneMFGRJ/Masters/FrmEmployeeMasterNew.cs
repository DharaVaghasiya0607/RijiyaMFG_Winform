using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AxoneMFGRJ.Masters
{
    public partial class FrmEmployeeMasterNew : DevExpress.XtraEditors.XtraForm
    {
        [DllImportAttribute("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Ledger ObjMast = new BOMST_Ledger();
        BOFormPer ObjPer = new BOFormPer();

        DataTable DtabExperience = new DataTable();
        DataTable DtabFamily = new DataTable();
        DataTable DtabReference = new DataTable();
        DataTable DtabAttachment = new DataTable();
        DataTable DtabProcessSetting = new DataTable();
        DataTable DtabItemIssueDetail = new DataTable();


        #region Property Settings

        public FrmEmployeeMasterNew()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            this.Text = "EMPLOYEE MASTER";

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            txtPass.Tag = ObjPer.PASSWORD;
            BtnSave.Enabled = ObjPer.ISINSERT;
            BtnDelete.Enabled = ObjPer.ISDELETE;

            lblAccID.Text = "Emp ID";
            lblAccCode.Text = "Emp Code";

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            txtLedgerCode.Focus();

            CmbShape.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
            CmbShape.Properties.DisplayMember = "SHAPECODE";
            CmbShape.Properties.ValueMember = "SHAPE_ID";

            BtnAdd_Click(null, null);
            txtLedgerCode.Text = Val.ToString(ObjMast.FindMaxLedgerCode());

            Fill();

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
            this.Show();
            txtLedgerCode.Focus();
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
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
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
            if (txtMainManager.Text.Trim().Length == 0)//Gunjan:27/03/2023
            {
                Global.Message("Kapan Main Manager Is Required");
                txtMainManager.Focus();
                return false;
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
            txtLedgerCode.Text = Val.ToString(ObjMast.FindMaxLedgerCode());
            //txtLedgerCode.Text = string.Empty;
            txtLedgerName.Text = string.Empty;
            txtLedgerName.Tag = string.Empty;
            txtLedgerID.Text = string.Empty;
            TxtShortName.Text = string.Empty;
            ChkGoodsAutoConfirm.Checked = false;

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
            ChkISSalaryAcctClear.Checked = false;

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
            //txtUserName.Text = string.Empty;
            //txtPassword.Text = string.Empty;
            txtAdharNo.Text = string.Empty;
            txtStudy.Text = string.Empty;
            txtIDCardNo.Text = string.Empty;
            txtLeaveReason.Text = string.Empty;
            DTPDateOfLeave.Checked = false;

            DTPDateOfJoin.Value = DateTime.Now;
            DTPDateOfBirth.Value = DateTime.Now;

            PicEmpPhoto.Image = null;

            ChkActive_CheckedChanged(null, null);
            txtLedgerCode.Focus();

            txtDomainKnowledge.Text = string.Empty;
            txtStudyPer.Text = "0.00";
            txtCollegeName.Text = string.Empty;

            //txtLanguageKnown.Text = string.Empty;
            ChkCmbLangKnown.SetEditValue(0);

            txtAge.Text = "0";
            txtReligion.Text = string.Empty;
            txtCast.Text = string.Empty;
            txtSubCast.Text = string.Empty;

            CmbGender.SelectedIndex = 0;
            CmbBloodGroup.SelectedIndex = 0;
            CmbMarriedStatus.SelectedIndex = 0;

            txtVillageName.Text = string.Empty;
            txtTaluka.Text = string.Empty;
            txtDistrict.Text = string.Empty;
            txtVillageAddress.Text = string.Empty;

            CmbShape.SetEditValue(0);

            ChkDiamondKnow.Checked = false;
            ChkSarinKnow.Checked = false;
            ChkComputerKnow.Checked = false;
            ChkLaserKnow.Checked = false;

            txtShapeKnown.Text = string.Empty;
            FillLedgerDetailInfo(Val.ToInt64(txtLedgerID.Text));
            txtPartyGroup.Text = string.Empty; // k :22/11/22
            txtPartyGroup.Tag = string.Empty;
            txtTable_ID.Text = string.Empty;
            txtTable_ID.Tag = string.Empty;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                LedgerMasterProperty Property = new LedgerMasterProperty();

                Property.LEDGER_ID = Val.ToInt64(txtLedgerID.Text);
                Property.LEDGERCODE = txtLedgerCode.Text;
                Property.LEDGERNAME = txtLedgerName.Text;
                Property.LEDGERGROUP = "EMPLOYEE";
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
                Property.SALARYTYPE = Val.ToString(CmbSalaryType.SelectedItem);
                Property.ISACTIVE = ChkActive.Checked;
                Property.EMPSHORTNAME = TxtShortName.Text;

                Property.EXPSALARY = Val.Val(txtExpSalary.Text);
                //Property.USERNAME = txtUserName.Text;
                //Property.PASSWORD = txtPassword.Text;
                Property.ADHARNO = txtAdharNo.Text;
                Property.EMPLOYEETYPE = Val.ToString(CmbEmpType.SelectedItem);

                Property.IDCARDNO = txtIDCardNo.Text;

                Property.DATEOFBIRTH = Val.SqlDate(DTPDateOfBirth.Value.ToShortDateString());
                Property.DATEOFJOIN = Val.SqlDate(DTPDateOfJoin.Value.ToShortDateString());

                Property.DATEOFLEAVE = null;
                Property.LEAVEREASON = "";
                if (DTPDateOfLeave.Checked == true && ChkActive.Checked == false)
                {
                    Property.DATEOFLEAVE = Val.SqlDate(DTPDateOfLeave.Value.ToShortDateString());
                    Property.LEAVEREASON = txtLeaveReason.Text;
                }

                
                
                Property.SHAPE_ID = Val.Trim(CmbShape.Properties.GetCheckedItems());
                if(txtPartyGroup.Text == "")//K : 28/11/22
                {
                    Property.PARTYGROUP_ID = 0;
                }
                else
                {
                    Property.PARTYGROUP_ID = Val.ToInt32(txtPartyGroup.Tag);
                }                
                Property.TABLE_ID = Val.ToInt64(txtTable_ID.Tag);
                Property.TABLENAME = Val.ToString(txtTable_ID.Text);

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
                

                ////Compressed Image : 19-08-2019

                ////pictureBox2.Image = ;
                //PicEmpPhoto.Image = (Bitmap)Global.ResizeImage(PicEmpPhoto.Image, new Size(300, 350), false);
                ////pictureBox2.Width = 505;
                //PicEmpPhoto.Refresh();
                ////pictureBox2.Image.Save(StrFilePath + "\\" + FileName1 + "\\vpfVideo\\" + i.ToString() + ".png"); //"\\vpfVideo\\" + 

                //System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                //ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                //EncoderParameters myEncoderParameters = new EncoderParameters(1);
                //EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                //myEncoderParameters.Param[0] = myEncoderParameter;
                //PicEmpPhoto.Image.Save(Application.StartupPath + "\\Test.jpg", jpgEncoder, myEncoderParameters); //"\\vpfVideo\\" +


                ////End : Compressed Image : 19-08-2019

                Property.AUTOCONFIRM = ChkGoodsAutoConfirm.Checked;


                //06-04-2019
                Property.STYDY = txtStudy.Text;
                Property.DOMAINKNOWLEDGE = Val.ToString(txtDomainKnowledge.Text);
                Property.STUDYPER = Val.Val(txtStudyPer.Text);
                Property.STUDYCOLLEGENAME = Val.ToString(txtCollegeName.Text);
                Property.LANGUAGEKNOWN = Val.ToString(ChkCmbLangKnown.Text).Trim();

                Property.AGE = Val.ToInt32(txtAge.Text);
                Property.RELIGION = Val.ToString(txtReligion.Text);
                Property.CAST = Val.ToString(txtCast.Text);
                Property.BLOODGROUP = Val.ToString(CmbBloodGroup.Text);
                Property.MARRIEDSTATUS = Val.ToString(CmbMarriedStatus.Text);

                Property.VILLAGENAME = Val.ToString(txtVillageName.Text);
                Property.VILLAGETALUKA = Val.ToString(txtTaluka.Text);
                Property.VILLAGEDISTRICT = Val.ToString(txtDistrict.Text);
                Property.VILLAGEADDRESS = Val.ToString(txtVillageAddress.Text);

                Property.DIAMONDKNOWLEDGE = ChkDiamondKnow.Checked ? "YES" : "NO";
                Property.SARINKNOWLEDGE = ChkSarinKnow.Checked ? "YES" : "NO";
                Property.COMPUTERKNOWLEDGE = ChkComputerKnow.Checked ? "YES" : "NO";
                Property.LASERKNOWLEDGE = ChkLaserKnow.Checked ? "YES" : "NO";
                Property.SHAPEKNOWN = Val.ToString(txtShapeKnown.Text);

                Property.GENDER = Val.ToString(CmbGender.Text);
                Property.SUBCAST = Val.ToString(txtSubCast.Text);
                //End : On : 06-04-2019

                Property.ISSALARYACCOUNTCLEAR = ChkISSalaryAcctClear.Checked;

                Property.KAPANMAINMANAGER_ID = Val.ToInt64(txtMainManager.Tag);//Gunjan:25/03/2023

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
                    Property = ObjMast.SaveLedgerDetailInfo(Property, DtabExperience, DtabFamily, DtabReference, DtabAttachment, DtabProcessSetting, DtabItemIssueDetail);
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
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }
        private ImageCodecInfo GetEncoder(ImageFormat format) //Add : Pinali : 19-08-2019
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
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

            lblTotal.Text = "Total Record : " + DTab.Rows.Count.ToString();

            MainGrid.Refresh();

            FillLedgerDetailInfo(Val.ToInt64(txtLedgerID.Text));
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

        public void FetchValue(DataRow DR)
        {
            txtLedgerCode.Text = Val.ToString(DR["LEDGERCODE"]);
            txtLedgerName.Text = Val.ToString(DR["LEDGERNAME"]);
            txtLedgerID.Text = Val.ToString(DR["LEDGER_ID"]);
            TxtShortName.Text = Val.ToString(DR["EMPSHORTNAME"]);

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

            txtDesignation.Tag = Val.ToString(DR["DESIGNATION_ID"]);
            txtDesignation.Text = Val.ToString(DR["DESIGNATIONNAME"]);

            txtManager.Tag = Val.ToString(DR["MANAGER_ID"]);
            txtManager.Text = Val.ToString(DR["MANAGERNAME"]);

            txtSalary.Text = Val.ToString(DR["SALARY"]);
            CmbSalaryType.SelectedItem = Val.ToString(DR["SALARYTYPE"]);

            txtExpSalary.Text = Val.ToString(DR["EXPSALARY"]);
            //txtUserName.Text = Val.ToString(DR["USERNAME"]);
            //txtPassword.Text = Val.ToString(DR["PASSWORD"]);
            txtAdharNo.Text = Val.ToString(DR["ADHARNO"]);
            CmbEmpType.SelectedItem = Val.ToString(DR["EMPLOYEETYPE"]);
            txtStudy.Text = Val.ToString(DR["STYDY"]);
            txtIDCardNo.Text = Val.ToString(DR["IDCARDNO"]);

            DTPDateOfBirth.Text = Val.ToString(DR["DATEOFBIRTH"]);
            DTPDateOfJoin.Text = Val.ToString(DR["DATEOFJOIN"]);
            txtPartyGroup.Text = Val.ToString(DR["PARTYGROUP"]); // k: 28/11/22
            txtPartyGroup.Tag = Val.ToInt32(DR["PARTYGROUP_ID"]);
            txtTable_ID.Text = Val.ToString(DR["TABLENAME"]);
            txtTable_ID.Tag = Val.ToInt64(DR["TABLE_ID"]);

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

            ChkGoodsAutoConfirm.Checked = Val.ToBoolean(DR["AUTOCONFIRM"]);

            //byte[] OFFICELOGO = DR["EMPPHOTO"] as byte[] ?? null;
            //if (OFFICELOGO != null)
            //{
            //    using (MemoryStream ms = new MemoryStream(OFFICELOGO))
            //    {
            //        PicEmpPhoto.Image = Image.FromStream(ms);
            //    }
            //}
            //else
            //{
            //    PicEmpPhoto.Image = null;
            //}


            txtDomainKnowledge.Text = Val.ToString(DR["DOMAINKNOWLEDGE"]);
            txtStudyPer.Text = Val.ToString(DR["STUDYPER"]);
            txtCollegeName.Text = Val.ToString(DR["STUDYCOLLEGENAME"]);

            //txtLanguageKnown.Text = Val.ToString(DR["LANGUAGEKNOWN"]);
            ChkCmbLangKnown.Text = Val.ToString(DR["LANGUAGEKNOWN"]);

            txtAge.Text = Val.ToString(DR["AGE"]);
            txtReligion.Text = Val.ToString(DR["RELIGION"]);
            txtCast.Text = Val.ToString(DR["CAST"]);
            CmbBloodGroup.Text = Val.ToString(DR["BLOODGROUP"]);

            CmbMarriedStatus.Text = Val.ToString(DR["MARRIEDSTATUS"]);
            txtVillageName.Text = Val.ToString(DR["VILLAGENAME"]);
            txtTaluka.Text = Val.ToString(DR["VILLAGETALUKA"]);
            txtDistrict.Text = Val.ToString(DR["VILLAGEDISTRICT"]);
            txtVillageAddress.Text = Val.ToString(DR["VILLAGEADDRESS"]);

            ChkDiamondKnow.Checked = Val.ToString(DR["DIAMONDKNOWLEDGE"]) == "YES" ? true : false;
            ChkSarinKnow.Checked = Val.ToString(DR["SARINKNOWLEDGE"]) == "YES" ? true : false;
            ChkComputerKnow.Checked = Val.ToString(DR["COMPUTERKNOWLEDGE"]) == "YES" ? true : false;
            ChkLaserKnow.Checked = Val.ToString(DR["LASERKNOWLEDGE"]) == "YES" ? true : false;

            txtShapeKnown.Text = Val.ToString(DR["SHAPEKNOWN"]);
            CmbGender.Text = Val.ToString(DR["GENDER"]);
            txtSubCast.Text = Val.ToString(DR["SUBCAST"]);

            ChkISSalaryAcctClear.Checked = Val.ToBoolean(DR["ISSALARYACCOUNTCLEAR"]);
            
            FillLedgerDetailInfo(Val.ToInt64(txtLedgerID.Text));  //06-04-2019

            CmbShape.SetEditValue(Val.ToString(DR["SHAPE_ID"]));

            txtMainManager.Tag = Val.ToString(DR["kapanMainManager_ID"]);//Gunjan:27/03/2023
            txtMainManager.Text = Val.ToString(DR["KAPANMAINMANAGER"]);

            txtLedgerName.Focus();

            xtraTabControl1.SelectedTabPageIndex = 0;
        }
        public void FillLedgerDetailInfo(Int64 IntLedger_ID)
        {
            //Ledger Details
            DataSet DsDetailInfo = ObjMast.GetledgerDetailDInfoata(Val.ToInt64(IntLedger_ID));

            //Experience Detail
            DtabExperience = DsDetailInfo.Tables[0];

            if (DtabExperience.Rows.Count > 0)
            {
                int MaxSrNo = 0;
                MaxSrNo = (int)DtabExperience.Compute("Max(SRNO)", "");
                DataRow Dr = DtabExperience.NewRow();
                Dr["SRNO"] = MaxSrNo + 1;
                DtabExperience.Rows.Add(Dr);
                DtabExperience.AcceptChanges();

            }
            else
            {
                DataRow Dr = DtabExperience.NewRow();
                Dr["SRNO"] = 1;
                DtabExperience.Rows.Add(Dr);
                DtabExperience.AcceptChanges();
            }
            MainGrdExperience.DataSource = DtabExperience;
            MainGrdExperience.RefreshDataSource();


            //Family Details
            DtabFamily = DsDetailInfo.Tables[1];

            if (DtabFamily.Rows.Count > 0)
            {
                int MaxSrNo = 0;
                MaxSrNo = (int)DtabFamily.Compute("Max(SRNO)", "");
                DataRow Dr = DtabFamily.NewRow();
                Dr["SRNO"] = MaxSrNo + 1;
                DtabFamily.Rows.Add(Dr);
                DtabFamily.AcceptChanges();

            }
            else
            {
                DataRow Dr = DtabFamily.NewRow();
                Dr["SRNO"] = 1;
                DtabFamily.Rows.Add(Dr);
                DtabFamily.AcceptChanges();
            }
            MainGrdFamily.DataSource = DtabFamily;
            MainGrdFamily.RefreshDataSource();


            //Reference Details
            DtabReference = DsDetailInfo.Tables[2];

            if (DtabReference.Rows.Count > 0)
            {
                int MaxSrNo = 0;
                MaxSrNo = (int)DtabReference.Compute("Max(SRNO)", "");
                DataRow Dr = DtabReference.NewRow();
                Dr["SRNO"] = MaxSrNo + 1;
                DtabReference.Rows.Add(Dr);
                DtabReference.AcceptChanges();

            }
            else
            {
                DataRow Dr = DtabReference.NewRow();
                Dr["SRNO"] = 1;
                DtabReference.Rows.Add(Dr);
                DtabReference.AcceptChanges();
            }
            MainGrdReference.DataSource = DtabReference;
            MainGrdReference.RefreshDataSource();


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

                //Delete Profile Image from DataTable

            }
            else
            {
                DataRow Dr = DtabAttachment.NewRow();
                Dr["SRNO"] = 1;
                DtabAttachment.Rows.Add(Dr);
                DtabAttachment.AcceptChanges();
                PicEmpPhoto.Image = null;
            }
            MainGrdAttachment.DataSource = DtabAttachment;
            MainGrdAttachment.RefreshDataSource();

            //Item Issue Details - 08-06-2019
            DtabItemIssueDetail = DsDetailInfo.Tables[5];

            if (DtabItemIssueDetail.Rows.Count > 0)
            {
                int MaxSrNo = 0;
                MaxSrNo = (int)DtabItemIssueDetail.Compute("Max(SRNO)", "");
                DataRow Dr = DtabItemIssueDetail.NewRow();
                Dr["SRNO"] = MaxSrNo + 1;
                Dr["ISSUEDATE"] = DateTime.Now.ToString("dd-MM-yyyy");
                DtabItemIssueDetail.Rows.Add(Dr);
                DtabItemIssueDetail.AcceptChanges();

            }
            else
            {
                DataRow Dr = DtabItemIssueDetail.NewRow();
                Dr["SRNO"] = 1;
                Dr["ISSUEDATE"] = DateTime.Now.ToString("dd-MM-yyyy");
                DtabItemIssueDetail.Rows.Add(Dr);
                DtabItemIssueDetail.AcceptChanges();
            }
            MainGrdItemIssue.DataSource = DtabItemIssueDetail;
            MainGrdItemIssue.RefreshDataSource();

            //End : Ledger Details : 06-04-2019

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


        private void txtLedgerCode_TextChanged(object sender, EventArgs e)
        {
            //if (txtUserName.Text.Trim().Length == 0 && txtLedgerCode.Text.Length != 0)
            //{
            //    txtUserName.Text = txtLedgerCode.Text;
            //    txtPassword.Text = txtLedgerCode.Text;
            //}
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

        private void repTxtLeaveCompReason_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdExperience.GetFocusedDataRow();
                    if (!Val.ToString(dr["EXPCOMPANYNAME"]).Equals(string.Empty) && !Val.ToString(dr["EXPMANAGERNAME"]).Equals(string.Empty) && GrdExperience.IsLastRow)
                    {
                        int MaxSrNo = 0;
                        MaxSrNo = (int)DtabExperience.Compute("Max(SRNO)", "");
                        DataRow DRE = DtabExperience.NewRow();
                        DRE["SRNO"] = MaxSrNo + 1;
                        DtabExperience.Rows.Add(DRE);
                        //DtabPara.AcceptChanges();
                    }
                    else if (GrdExperience.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repTxtMobileNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdFamily.GetFocusedDataRow();
                    if (!Val.ToString(dr["MEMBERNAME"]).Equals(string.Empty) && GrdFamily.IsLastRow)
                    {
                        int MaxSrNo = 0;
                        MaxSrNo = (int)DtabFamily.Compute("Max(SRNO)", "");
                        DataRow DRE = DtabFamily.NewRow();
                        DRE["SRNO"] = MaxSrNo + 1;
                        DtabFamily.Rows.Add(DRE);
                        //DtabPara.AcceptChanges();
                    }
                    else if (GrdFamily.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdReference.GetFocusedDataRow();
                    if (!Val.ToString(dr["REFNAME"]).Equals(string.Empty) && GrdReference.IsLastRow)
                    {
                        int MaxSrNo = 0;
                        MaxSrNo = (int)DtabReference.Compute("Max(SRNO)", "");
                        DataRow DRE = DtabReference.NewRow();
                        DRE["SRNO"] = MaxSrNo + 1;
                        DtabReference.Rows.Add(DRE);
                        //DtabPara.AcceptChanges();
                    }
                    else if (GrdReference.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void repTxtUploadFilename_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdAttachment.GetFocusedDataRow();
                    if (!Val.ToString(dr["UPLOADFILENAME"]).Equals(string.Empty) && GrdAttachment.IsLastRow)
                    {
                        int MaxSrNo = 0;
                        MaxSrNo = (int)DtabAttachment.Compute("Max(SRNO)", "");
                        DataRow DRE = DtabAttachment.NewRow();
                        DRE["SRNO"] = MaxSrNo + 1;
                        DtabAttachment.Rows.Add(DRE);
                        //DtabPara.AcceptChanges();
                    }
                    else if (GrdAttachment.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDg = new OpenFileDialog();
            if (OpenDg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataRow DRow = DtabAttachment.NewRow();
                //MemoryStream ms = new MemoryStream();
                //Image image = Image.FromStream(ms);
                //Bitmap images = ResetResolution(image as Metafile, 300);
                byte[] ba = File.ReadAllBytes(OpenDg.FileName);
                //Bitmap Bt  = Image.FromStream(OpenDg.FileName);

                //Bitmap bm = new Bitmap(Image.FromStream(new MemoryStream(ba)));

                MemoryStream ms = new MemoryStream();
                ms.Position = 0;
                ms.Read(ba, 0, ba.Length);

                //Bitmap bmp;
                //using (var ms = new MemoryStream(imageData))
                //{
                //    bmp = new Bitmap(ms);
                //}




                DRow["SRNO"] = 0;
                DRow["UPLOADFILENAME"] = OpenDg.SafeFileName;
                DRow["UPLOADIMAGE"] = ms.Read(ba, 0, ba.Length);
                //DRow["FILEPATH"] = OpenDg.FileName;
                //DRow["FILENAME"] = @"";
                DtabAttachment.Rows.Add(DRow);
                MainGrdAttachment.DataSource = DtabAttachment;
                MainGrdAttachment.Refresh();
            }
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {

            if (DtabAttachment.Rows.Count == 0)
            {
                Global.Message("Please add file for upload.");
                return;
            }

            //DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            //System.Threading.Thread Thread = new System.Threading.Thread(UploadFilesAttachment);
            //Thread.Start();

        }

        private void repBtnBrowse_Click(object sender, EventArgs e)
        {
            DataRow DRow = GrdAttachment.GetFocusedDataRow();

            OpenFileDialog OpenDg = new OpenFileDialog();
            if (OpenDg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] ba = File.ReadAllBytes(OpenDg.FileName);
                //MemoryStream ms = new MemoryStream();
                //ms.Position = 0;
                //ms.Read(ba, 0, ba.Length);

                byte[] file;
                using (var stream = new FileStream(OpenDg.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        file = reader.ReadBytes((int)stream.Length);
                    }
                }




                DRow["UPLOADFILENAME"] = OpenDg.SafeFileName;
                DRow["UPLOADIMAGE"] = file;
                //DRow["FILEPATH"] = OpenDg.FileName;
                //DRow["FILENAME"] = @"";
                //DtabAttachment.Rows.Add(DRow);
                DtabAttachment.AcceptChanges();


                //----------  Add New Row --------------//
                if (!Val.ToString(DRow["UPLOADFILENAME"]).Equals(string.Empty) && GrdAttachment.IsLastRow)
                {
                    int MaxSrNo = 0;
                    MaxSrNo = (int)DtabAttachment.Compute("Max(SRNO)", "");
                    DataRow DRE = DtabAttachment.NewRow();
                    DRE["SRNO"] = MaxSrNo + 1;
                    DtabAttachment.Rows.Add(DRE);
                    //DtabPara.AcceptChanges();
                }
                else if (GrdAttachment.IsLastRow)
                {
                    //BtnSave.Focus();
                    //e.Handled = true;
                }

            }
        }

        private void repBtnBrowse_KeyDown(object sender, KeyEventArgs e)
        {
            //DataRow dr = GrdAttachment.GetFocusedDataRow();
            //if (!Val.ToString(dr["UPLOADFILENAME"]).Equals(string.Empty) && !Val.ToString(dr["DOCUMENTTYPE"]).Equals(string.Empty) && GrdAttachment.IsLastRow)
            //{
            //    int MaxSrNo = 0;
            //    MaxSrNo = (int)DtabAttachment.Compute("Max(SRNO)", "");
            //    DataRow DRE = DtabAttachment.NewRow();
            //    DRE["SRNO"] = MaxSrNo + 1;
            //    DtabAttachment.Rows.Add(DRE);
            //    //DtabPara.AcceptChanges();
            //}
            //else if (GrdAttachment.IsLastRow)
            //{
            //    BtnSave.Focus();
            //    e.Handled = true;
            //}
        }

        private void repBtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GrdAttachment.GetFocusedDataRow();

                if (Val.ToString(dr["UPLOADFILENAME"]).Trim().Equals(string.Empty))
                    return;

                byte[] BT = (byte[])dr["UPLOADIMAGE"];
                string LoadPath = "";
                SaveFileDialog Savedlg = new SaveFileDialog();
                Savedlg.Title = "Save File";

                string StrExtension = Path.GetExtension(Val.ToString(dr["UPLOADFILENAME"]));

                Savedlg.FileName = Val.ToString(dr["UPLOADFILENAME"]);
                if (Savedlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (Val.ToString(Path.GetExtension(Savedlg.FileName)).Trim().Equals(string.Empty))
                    {
                        LoadPath = Val.ToString(Savedlg.FileName) + StrExtension;
                    }
                    else
                    {
                        LoadPath = Val.ToString(Savedlg.FileName);//.Replace(Path.GetExtension(Savedlg.FileName), StrExtension);
                    }

                    File.WriteAllBytes(LoadPath, BT);
                    System.Diagnostics.Process.Start(LoadPath, "CMD");
                }
                Savedlg.Dispose();
                Savedlg = null;
                //File.Open(LoadPath, FileMode.Open);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repBtnDeleteExp_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GrdExperience.GetFocusedDataRow();
                if (dr != null)
                {
                    if (!Val.ToString(dr["EXPERIENCE_ID"]).Trim().Equals(string.Empty))
                    {
                        int res = ObjMast.DeleteLedgerDetailInfo("EXPERIENCEDETAIL", Val.ToString(dr["EXPERIENCE_ID"]));
                        if (res > 0)
                        {
                            dr.Delete();
                            DtabExperience.AcceptChanges();
                        }
                    }
                    else
                    {
                        dr.Delete();
                        DtabExperience.AcceptChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repBtnDeleteFamilyDetail_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GrdFamily.GetFocusedDataRow();
                if (dr != null)
                {
                    if (!Val.ToString(dr["FAMILY_ID"]).Trim().Equals(string.Empty))
                    {
                        int res = ObjMast.DeleteLedgerDetailInfo("FAMILYDETAIL", Val.ToString(dr["FAMILY_ID"]));
                        if (res > 0)
                        {
                            dr.Delete();
                            DtabFamily.AcceptChanges();
                        }
                    }
                    else
                    {
                        dr.Delete();
                        DtabFamily.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repBtnDeleteRefDetail_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GrdReference.GetFocusedDataRow();
                if (dr != null)
                {
                    if (!Val.ToString(dr["REFERENCE_ID"]).Trim().Equals(string.Empty))
                    {
                        int res = ObjMast.DeleteLedgerDetailInfo("REFERENCEDETAIL", Val.ToString(dr["REFERENCE_ID"]));
                        if (res > 0)
                        {
                            dr.Delete();
                            DtabReference.AcceptChanges();
                        }
                    }
                    else
                    {
                        dr.Delete();
                        DtabReference.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repBtnDeleteAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GrdAttachment.GetFocusedDataRow();
                if (dr != null)
                {
                    if (!Val.ToString(dr["ATTACHMENT_ID"]).Trim().Equals(string.Empty))
                    {
                        int res = ObjMast.DeleteLedgerDetailInfo("ATTACHMENTDETAIL", Val.ToString(dr["ATTACHMENT_ID"]));
                        if (res > 0)
                        {
                            dr.Delete();
                            DtabAttachment.AcceptChanges();
                        }
                    }
                    else
                    {
                        dr.Delete();
                        DtabAttachment.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void DTPDateOfBirth_Validating(object sender, CancelEventArgs e)
        {
            int age;

            DateTime dateOfBirth = DTPDateOfBirth.Value;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            txtAge.Text = Val.ToString(age);
        }

        private void txtAge_Validated(object sender, EventArgs e)
        {
            //DateTime NewDate;
            //NewDate = DateTime.Now.AddYears(Val.ToInt32(txtAge.Text) * -1);

            //DTPDateOfBirth.Value = NewDate;
        }

        private void repTxtDocumentType_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdAttachment.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PARACODE,PARANAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DOCUMENTTYPE);
                    FrmSearch.mColumnsToHide = "PARA_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdAttachment.SetFocusedRowCellValue("DOCUMENTTYPE", Val.ToString(FrmSearch.mDRow["PARANAME"]));
                        GrdAttachment.SetFocusedRowCellValue("DOCUMENTTYPE_ID", Val.ToString(FrmSearch.mDRow["PARA_ID"]));
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

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                //if (Global.Confirm("Are You Sure To Print The Janged # " + txtJangedNo.Text + " ?") == System.Windows.Forms.DialogResult.Yes)
                //{
                DataTable DtabLedger = ObjMast.GetDataForLedgerPrint(Val.ToInt64(txtLedgerID.Text));
                //}

                Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();

                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowForm("LedgerPrint", DtabLedger);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message("sp : " + ex.Message.ToString());
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

        public bool CheckDuplicate(DataTable Dt, string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            Dt.AcceptChanges();

            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in Dt.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }
        public bool CheckDuplicateItemDetail(DataTable Dt, string ColName, string ColValue, string ColName2, string ColValue2, int IntRowIndex, string StrMsg)
        {
            Dt.AcceptChanges();

            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in Dt.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && Val.ToString(row[ColName2]).ToUpper() == Val.ToString(ColValue2).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }
        public bool CheckDuplicateRefDetail(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            DtabReference.AcceptChanges();
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabReference.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;

            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }

        private void repTxtRefCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (GrdReference.FocusedRowHandle < 0)
                    return;

                GrdReference.PostEditor();
                DataRow DrRef = GrdReference.GetFocusedDataRow();

                if (CheckDuplicateRefDetail("REFCODE", Val.ToString(GrdReference.EditingValue), GrdReference.FocusedRowHandle, "'" + Val.ToString(GrdReference.EditingValue) + "' REFERENCE CODE"))
                {
                    GrdReference.SetFocusedRowCellValue("REFCODE", string.Empty);
                    GrdReference.SetFocusedRowCellValue("REFNAME", string.Empty);
                    GrdReference.SetFocusedRowCellValue("REFADDRESS", string.Empty);
                    GrdReference.SetFocusedRowCellValue("REFMOBILENO", string.Empty);
                    GrdReference.SetFocusedRowCellValue("REFDESIGNATION", string.Empty);

                    //GrdReference.FocusedRowHandle = GrdReference.FocusedRowHandle;
                    //GrdReference.FocusedColumn = GrdReference.Columns["REFCODE"];
                    //GrdReference.ShowEditor();
                    e.Cancel = true;
                    return;
                }

                DataRow DRow = ObjMast.GetLedgerInfoByCode("EMPLOYEE", Val.ToString(GrdReference.EditingValue));

                if (DRow != null)
                {
                    GrdReference.SetFocusedRowCellValue("REFCODE", Val.ToString(DRow["LEDGERCODE"]));
                    GrdReference.SetFocusedRowCellValue("REFNAME", Val.ToString(DRow["LEDGERNAME"]));
                    GrdReference.SetFocusedRowCellValue("REFADDRESS", Val.ToString(DRow["BILLINGADDRESS"]));
                    GrdReference.SetFocusedRowCellValue("REFMOBILENO", Val.ToString(DRow["MOBILENO1"]));
                    GrdReference.SetFocusedRowCellValue("REFDESIGNATION", Val.ToString(DRow["DESIGNATIONNAME"]));

                    if (GrdReference.IsLastRow)
                    {
                        int MaxSrNo = 0;
                        MaxSrNo = (int)DtabReference.Compute("Max(SRNO)", "");
                        DataRow DRE = DtabReference.NewRow();
                        DRE["SRNO"] = MaxSrNo + 1;
                        DtabReference.Rows.Add(DRE);
                    }

                    GrdReference.FocusedRowHandle = GrdReference.FocusedRowHandle + 1;
                    GrdReference.FocusedColumn = GrdReference.Columns["SRNO"];
                    GrdReference.ShowEditor();
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdItemIssue.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "ITEMCODE,ITEMNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_ITEM);
                    FrmSearch.mColumnsToHide = "ITEM_ID";
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "ITEMNAME";

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdItemIssue.PostEditor();
                        DataRow Dr = GrdItemIssue.GetFocusedDataRow();

                        GrdItemIssue.SetFocusedRowCellValue("ITEMNAME", Val.ToString(FrmSearch.mDRow["ITEMNAME"]));
                        GrdItemIssue.SetFocusedRowCellValue("ITEM_ID", Val.ToInt32(FrmSearch.mDRow["ITEM_ID"]));

                        if (CheckDuplicateItemDetail(DtabItemIssueDetail, "ITEMNAME", Val.ToString(GrdItemIssue.EditingValue), "ISSUEDATE", Val.ToString(Dr["ISSUEDATE"]), GrdItemIssue.FocusedRowHandle, "'" + Val.ToString(GrdItemIssue.EditingValue) + "'  ITEM"))
                        {
                            GrdItemIssue.SetFocusedRowCellValue("ITEMNAME", string.Empty);
                            GrdItemIssue.SetFocusedRowCellValue("ITEM_ID", 0);
                            return;
                        }
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

        private void repTxtIssueRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow dr = GrdItemIssue.GetFocusedDataRow();
                    if (!Val.ToString(dr["ITEMNAME"]).Equals(string.Empty) && Val.Val(dr["QUANTITY"]) != 0 && GrdItemIssue.IsLastRow)
                    {
                        int MaxSrNo = 0;
                        MaxSrNo = (int)DtabItemIssueDetail.Compute("Max(SRNO)", "");
                        DataRow DRE = DtabItemIssueDetail.NewRow();
                        DRE["SRNO"] = MaxSrNo + 1;
                        DRE["ISSUEDATE"] = DateTime.Now.ToString("dd-MM-yyyy");
                        DtabItemIssueDetail.Rows.Add(DRE);
                        //DtabPara.AcceptChanges();
                    }
                    else if (GrdItemIssue.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repBtnDeleteIssueDetail_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GrdItemIssue.GetFocusedDataRow();
                if (dr != null)
                {
                    if (!Val.ToString(dr["ITEMISSUE_ID"]).Trim().Equals(string.Empty))
                    {
                        int res = ObjMast.DeleteLedgerDetailInfo("ITEMISSUEDETAIL", Val.ToString(dr["ITEMISSUE_ID"]));
                        if (res > 0)
                        {
                            dr.Delete();
                            DtabItemIssueDetail.AcceptChanges();
                        }
                    }
                    else
                    {
                        dr.Delete();
                        DtabItemIssueDetail.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
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

        private void repDtpIssueDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (GrdItemIssue.FocusedRowHandle < 0)
                    return;

                GrdItemIssue.PostEditor();
                DataRow DrItem = GrdItemIssue.GetFocusedDataRow();

                if (CheckDuplicateItemDetail(DtabItemIssueDetail, "ITEMNAME", Val.ToString(DrItem["ITEMNAME"]), "ISSUEDATE", Val.ToString(GrdItemIssue.EditingValue), GrdItemIssue.FocusedRowHandle, Val.ToString(DrItem["ITEMNAME"]) + " Item On This Date Is"))
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        public void FreezMemory()
        {
            try
            {
                //  iDispose();
                
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (
                    Environment.OSVersion.Platform == PlatformID.Win32NT ||
                    Environment.OSVersion.Platform == PlatformID.Win32S ||
                    Environment.OSVersion.Platform == PlatformID.Win32Windows ||
                    Environment.OSVersion.Platform == PlatformID.WinCE
                    )
                {
                    SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                }
            }
            catch
            {

            }

        }


        private void BtnIDCard_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataTable DTabSelected = GetTableOfSelectedRows(GrdDet, true);
                DTabSelected.TableName = "TABLE";
                string RoundXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabSelected.WriteXml(sw);
                    RoundXml = sw.ToString();
                }
                DataTable DTabReport = ObjMast.PrintIDCard(RoundXml);

                //foreach (DataRow DRow in DTabReport.Rows)
                //{
                //    if (DRow["EMPPHOTO"] != null)
                //    {
                //        DRow["EMPPHOTO"] = Compress((byte[])DRow["EMPPHOTO"]);
                //    }
                //}

                if (DTabReport.Rows.Count == 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    Global.MessageError("No Data For Print");
                    return;
                }

                ////Add : Pinali : 16-11-2019
                //for (int rowNumber = 0; rowNumber < DTabReport.Rows.Count; rowNumber++)
                //{
                //    DisplayImages(DTabReport.Rows[rowNumber], "EMPPHOTO");
                //}

                ////Emd : Pinali : 16-11-2019

                int IntStep = 5;

                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string Str = folderDlg.SelectedPath;
                    if (Directory.Exists(Str) == false)
                    {
                        Directory.CreateDirectory(Str);
                    }

                    for (int IntI = 1; IntI <= DTabSelected.Rows.Count; IntI = IntI + IntStep)
                    {
                        DataRow[] DRow = DTabReport.Select("RNO >= " + IntI + " And RNO <= " + (IntI + IntStep - 1) + "");
                        if (DRow.Length != 0)
                        {
                            FreezMemory(); 
                            System.Threading.Thread.Sleep(1000);
                            DataTable DTabCopy = DRow.CopyToDataTable();
                            Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                            FrmReportViewer.MdiParent = Global.gMainRef;
                            string StrPath = Str + "\\" + IntI + "_" + (IntI + IntStep - 1) + ".pdf";
                            FrmReportViewer.ShowFormPrintExportInPDF("EmployeeCard", DTabCopy, StrPath);
                            
                        }
                    }

                    System.Diagnostics.Process.Start(Str, "cmd");
                }


                

                Global.Message("Export Successfully.");

                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(EX.Message);
            }


        }
        private void DisplayImages(DataRow row, string img) //Add : Pinali : 16-11-2019
        {
            byte[] ImgData = row[img] as byte[] ?? null;
            MemoryStream ms = new MemoryStream(ImgData);
            byte[] Byte = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(Byte, 0, Convert.ToInt32(ms.Length));
            ms.Close();
            row[img] = Byte;
        }

        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionMode.Compress))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray();
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

        private void BtnProfileExport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataTable DTabSelected = GetTableOfSelectedRows(GrdDet, true);

                DTabSelected = DTabSelected.DefaultView.ToTable(true, "LEDGER_ID");

                DTabSelected.TableName = "TABLE";
                string RoundXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabSelected.WriteXml(sw);
                    RoundXml = sw.ToString();
                }
                DataTable DTabReport = ObjMast.PrintIDCard(RoundXml);

                if (DTabReport.Rows.Count == 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    Global.MessageError("No Data For Print");
                    return;
                }

                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string Str = folderDlg.SelectedPath;
                    if (Directory.Exists(Str) == false)
                    {
                        Directory.CreateDirectory(Str);
                    }

                    foreach (DataRow DRow in DTabReport.Rows)
                    {
                        try
                        {
                            string StrPath = Str + "\\" + Val.ToString(DRow["LEDGERCODE"]) + ".jpg";

                            if (DRow["EMPPHOTO"] != null)
                            {
                                byte[] IMAGE = (byte[])DRow["EMPPHOTO"];
                                using (MemoryStream stream = new MemoryStream(IMAGE))
                                {
                                    Image img = Image.FromStream(stream);
                                    img.Save(StrPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    img.Dispose();
                                    img = null;
                                }
                                IMAGE = null;
                            }
                        }
                        catch (Exception)
                        {

                        }

                    }

                    System.Diagnostics.Process.Start(Str, "cmd");
                }

                Global.Message("Export Successfully.");

                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(EX.Message);
            }
        }

        private void txtPartyGroup_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtPartyGroup.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtPartyGroup.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void txtTable_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "TABLENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "TABLENAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_TABLE);

                    FrmSearch.mColumnsToHide = "TABLE_ID";

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtTable_ID.Text = Val.ToString(FrmSearch.mDRow["TABLENAME"]);
                        txtTable_ID.Tag = Val.ToString(FrmSearch.mDRow["TABLE_ID"]);
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

        private void txtMainManager_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtMainManager.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtMainManager.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjPer.PASSWORD != "" && ObjPer.PASSWORD.ToUpper() == txtPass.Text.ToUpper()) 
                {
                    txtMobileNo1.Visible = true;
                    txtMobileNo2.Visible = true;
                    txtBAddress.Visible = true;
                    txtSalary.Visible = true;
                    txtExpSalary.Visible = true;
                }
                else
                {
                    txtMobileNo1.Visible = false;
                    txtMobileNo2.Visible = false;
                    txtBAddress.Visible = false;
                    txtSalary.Visible = false;
                    txtExpSalary.Visible = false;
                }
            }
            catch(Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }
    }
}
