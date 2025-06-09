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
using AxoneMFGRJ.Masters;
using BusLib.Polish;

namespace AxoneMFGRJ.Polish
{
    public partial class FrmPolishIssueWithPackets : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_PacketCreate ObjPacket = new BOTRN_PacketCreate();
        BOTRN_PolishTransaction ObjPolish = new BOTRN_PolishTransaction();
        DataTable DtabPacketIss = new DataTable();

        bool mBoolAutoConfirm = false;

        Int64 pIntMainPolishPacket_ID = 0;
        Int64 pIntFromEmployee_ID = 0;
        #region Property Settings

        public FrmPolishIssueWithPackets()
        {
            InitializeComponent();
        }
        
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            DTPEntryDate.Text = DateTime.Now.ToShortDateString();

            DtabPacketIss.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("PROCESSNAME", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("PACKETNO", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("PARTYNAME", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("ISSUEPCS", typeof(int)));
            DtabPacketIss.Columns.Add(new DataColumn("ISSUECARAT", typeof(double)));
            DtabPacketIss.Columns.Add(new DataColumn("ENTRYDATE", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("SHAPE", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("CHARNI", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("CLARITY", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("NOTE", typeof(string)));

            MainGrid.DataSource = DtabPacketIss;
            MainGrid.RefreshDataSource();

            txtKapan.Focus();
            CmbLabourType.SelectedIndex = 0;
            this.Show();
        }

        public void ShowForm(string StrKapanName, Int64 StrKapan_ID, string StrManager, Int64 SrtManager_ID,Int64 StrPolishPacket_ID, Int64 pIntEmployee_ID)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            DTPEntryDate.Text = DateTime.Now.ToShortDateString();
            pIntMainPolishPacket_ID = StrPolishPacket_ID;
            txtKapan.Text = StrKapanName;
            txtKapan.Tag = StrKapan_ID;

            txtManager.Text = StrManager;
            txtManager.Tag = SrtManager_ID;

            pIntFromEmployee_ID = pIntEmployee_ID;
          
            GetMainPacketBalance();

            DtabPacketIss.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("PROCESSNAME", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("PACKETNO", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("PARTYNAME", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("ISSUEPCS", typeof(int)));
            DtabPacketIss.Columns.Add(new DataColumn("ISSUECARAT", typeof(double)));
            DtabPacketIss.Columns.Add(new DataColumn("ENTRYDATE", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("SHAPE", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("CHARNI", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("CLARITY", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("NOTE", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("PACKETCATEGORY", typeof(string)));
            DtabPacketIss.Columns.Add(new DataColumn("PACKETTYPE", typeof(string)));

            MainGrid.DataSource = DtabPacketIss;
            MainGrid.RefreshDataSource();
            txtProcess.Focus();           
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
            ObjFormEvent.ObjToDisposeList.Add(ObjPacket);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion



        private void BtnExport_Click(object sender, EventArgs e)
        {
            DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

            PrinterSettingsUsing pst = new PrinterSettingsUsing();

            PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);

            //Lesson2 link = new Lesson2(PrintSystem);
            PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

            GrdDet.OptionsPrint.AutoWidth = true;
            GrdDet.OptionsPrint.UsePrintStyles = true;

            link.Component = MainGrid;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;

            link.Margins.Left = 40;
            link.Margins.Right = 40;
            link.Margins.Bottom = 40;
            link.Margins.Top = 130;

            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);

            link.CreateDocument();

            link.ShowPreview();
            link.PrintDlg();
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString("Item Group List", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("Verdana", 11, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;
        }

        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("Verdana", 11, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }
               
        public void GetMainPacketBalance() 
        {
            try
            {
                DataRow DR = ObjPolish.GetMainPktBalancePcsCarat(Val.ToString(txtKapan.Text), Val.ToInt64(txtKapan.Tag),pIntMainPolishPacket_ID);
                if (DR == null)
                {
                    txtBalanceCarat.Text = "";
                }
                else
                {
                    txtBalanceCarat.Text = Val.ToString(DR["BALANCECARAT"]);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);

            }
        }

        public void Clear()
        {
            txtPcs.Text = string.Empty;
            txtCarat.Text = string.Empty;
            txtShape.Text = string.Empty;
            txtShape.Tag = string.Empty;
            txtClarity.Text = string.Empty;
            txtClarity.Tag = string.Empty;
            txtCharni.Text = string.Empty;
            txtCharni.Tag = string.Empty;

        }

        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnExit_Click(null, null);
            //}
        }



        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    //if (mStrParentFormType == "ROUGH")
                    //{
                    //    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO_NONMFG);
                    //}
                    //else
                    //{
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO);
                    //}

                    FrmSearch.mColumnsToHide = "LEDGER_ID,DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtParty.Text = Val.ToString(FrmSearch.mDRow["LEDGERCODE"]);
                        txtParty.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);

                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
                        //txtManager.Text = Val.ToString(FrmSearch.mDRow["MANAGERNAME"]);
                        //txtManager.Tag = Val.ToString(FrmSearch.mDRow["MANAGER_ID"]);
                        mBoolAutoConfirm = Val.ToBoolean(FrmSearch.mDRow["AUTOCONFIRM"]);
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

        private void txtProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);

                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        txtNextProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtNextProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                    }
                    else
                    {
                        txtProcess.Text = Val.ToString(DBNull.Value);
                        txtProcess.Tag = Val.ToString(DBNull.Value);

                        txtNextProcess.Text = Val.ToString(DBNull.Value);
                        txtNextProcess.Tag = Val.ToString(DBNull.Value);
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

        private void txtShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SHAPECODE,SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);

                    FrmSearch.mColumnsToHide = "SHAPE_ID";

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtShape.Text = Val.ToString(FrmSearch.mDRow["SHAPENAME"]);
                        txtShape.Tag = Val.ToString(FrmSearch.mDRow["SHAPE_ID"]);
                    }
                    else
                    {
                        txtShape.Text = Val.ToString(DBNull.Value);
                        txtShape.Tag = Val.ToString(DBNull.Value);
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

        private void txtClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CLARITYCODE,CLARITYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);

                    FrmSearch.mColumnsToHide = "CLARITY_ID";

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtClarity.Text = Val.ToString(FrmSearch.mDRow["CLARITYNAME"]);
                        txtClarity.Tag = Val.ToString(FrmSearch.mDRow["CLARITY_ID"]);
                    }
                    else
                    {
                        txtClarity.Text = Val.ToString(DBNull.Value);
                        txtClarity.Tag = Val.ToString(DBNull.Value);
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

        private void txtCharni_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CHARNICODE,CHARNINAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CHARNI);

                    FrmSearch.mColumnsToHide = "CHARNI_ID";

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtCharni.Text = Val.ToString(FrmSearch.mDRow["CHARNINAME"]);
                        txtCharni.Tag = Val.ToString(FrmSearch.mDRow["CHARNI_ID"]);


                        //Get Labour Type
                        string LabourType = "";
                        double LabourRate = 0;

                        DataTable DtLabour = ObjPacket.GetLabourRateAndType(Val.ToInt32(txtProcess.Tag), Val.ToInt32(txtCharni.Tag));
                        if (DtLabour.Rows.Count > 0)
                        {
                            LabourType = Val.ToString(DtLabour.Rows[0]["LABOURTYPE"]);
                            LabourRate = Val.Val(DtLabour.Rows[0]["LABOURRATE"]);

                            CmbLabourType.Text = LabourType;
                            txtLabourRate.Text = Val.ToString(LabourRate);
                        }
                        else
                        {
                            CmbLabourType.SelectedIndex = -1;
                            txtLabourRate.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txtCharni.Text = Val.ToString(DBNull.Value);
                        txtCharni.Tag = Val.ToString(DBNull.Value);
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

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (txtKapan.Text.Trim() == string.Empty)
            {
                Global.Message("Please Select Kapan First.");
                return;
            }
            if (txtParty.Text.Trim() == string.Empty)
            {
                Global.Message("Please Enter Party");
                txtParty.Focus();
                return;
            }
            if (txtProcess.Text.Trim() == string.Empty)
            {
                Global.Message("Please Enter Process");
                txtProcess.Focus();
                return;
            }
           // GetRoughBalance();
            GetMainPacketBalance();
            //lblMode.Text = "Add Mode";
            PanelKapan.Enabled = false;

            txtPacketNo.Text = ObjPolish.FindNewPolishOKPacketNo(Val.ToInt64(txtKapan.Tag), pIntMainPolishPacket_ID).ToString();
            txtPcs.Focus();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            lblLots.Text = string.Empty;
            txtPacketNo.Text = string.Empty;
            txtPacketNo.Tag = string.Empty;
            txtPcs.Text = string.Empty;
            txtCarat.Text = string.Empty;
            txtBalanceCarat.Text = string.Empty;
            //txtPacketID.Text = string.Empty;
            //txtTrnID.Text = string.Empty;
            txtJangedNo.Text = string.Empty;
            PanelKapan.Enabled = true;
            txtKapan.Text = string.Empty;
            txtKapan.Tag = string.Empty;
            txtParty.Text = string.Empty;
            txtParty.Tag = string.Empty;
            txtProcess.Text = string.Empty;
            txtProcess.Tag = string.Empty;
            lblMessage.Text = "";
            txtShape.Text = string.Empty;
            txtShape.Tag = string.Empty;
            txtClarity.Text = string.Empty;
            txtClarity.Tag = string.Empty;
            txtCharni.Text = string.Empty;
            txtCharni.Tag = string.Empty;

            txtExpPer.Text = "0";
            txtLabourRate.Text = "";
            txtExpCarat.Text = "0";
            DtabPacketIss.Rows.Clear();

        }



        private void BtnIssue_Click(object sender, EventArgs e)
        {
            try
            {

                if (lblMode.Text == "Add Mode")
                {
                    if (ValSave() == false)
                    {
                        return;
                    }

                    
                    bool pBoolPktno = ObjPolish.CheckMixPacketNoExists(txtKapan.Text, Val.ToInt32(txtPacketNo.Text));
                    {
                        if (pBoolPktno == true)
                        {
                            if (Global.Confirm("This Packet No. Is Already Created \n\n Are You Want To Create Packet ?") == System.Windows.Forms.DialogResult.Yes)
                            {
                                txtPacketNo.Text = ObjPolish.FindNewPolishOKPacketNo(Val.ToInt64(txtPacketNo.Tag), pIntMainPolishPacket_ID).ToString();                                
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    this.Cursor = Cursors.WaitCursor;

                    txtJangedNo.Text = string.Empty; //: Coz One By One packet will issue with Seperate JangedNo

                    TrnPolishPacketIssueReturn Property = new TrnPolishPacketIssueReturn();

                        Property.MAINPOLISHPACKET_ID = pIntMainPolishPacket_ID;
                        Property.KAPAN_ID = Val.ToInt64(txtKapan.Tag);
                        Property.POLISHPACKET_ID = Val.ToInt64(txtPacketNo.Tag);
                        Property.KAPANNAME = txtKapan.Text;

                        Property.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                       
                        Property.LOTPCS = Val.ToInt32(txtPcs.Text);
                        Property.LOTCARAT = Val.Val(txtCarat.Text);                     
                                         

                        Property.JANGEDNO = Val.ToInt64(txtJangedNo.Text);

                        Property.FROMEMPLOYEE_ID = pIntFromEmployee_ID;

                        Property.TOEMPLOYEE_ID = Val.ToInt64(txtParty.Tag);
                        Property.TODEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
                        Property.TOMANAGER_ID = Val.ToInt64(txtManager.Tag);
                        Property.TOPROCESS_ID = Val.ToInt32(txtProcess.Tag);
                        Property.NEXTPROCESS_ID = Val.ToInt32(txtNextProcess.Tag);

                       
                        Property.ENTRYDATE = Val.SqlDate(DTPEntryDate.Text);

                        Property.ENTRYTYPE = "EMPISS";

                        Property.PACKETCATEGORY = "ORIGINAL";
                        Property.PACKETTYPE = "SUB";

                        Property = ObjPolish.SaveProcessIssueForPolish(Property);

                        lblMessage.Text = Property.ReturnMessageDesc;

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            txtJangedNo.Text = Val.ToString(Property.ReturnValueJangedNo);

                            DataRow DRNew = DtabPacketIss.NewRow();
                            DRNew["KAPANNAME"] = txtKapan.Text;
                            DRNew["PROCESSNAME"] = txtProcess.Text;
                            DRNew["PACKETNO"] = txtPacketNo.Text;
                            DRNew["PARTYNAME"] = txtParty.Text;
                            DRNew["ISSUEPCS"] = txtPcs.Text;
                            DRNew["ISSUECARAT"] = txtCarat.Text;                           
                            DRNew["ENTRYDATE"] = DTPEntryDate.Text;

                            lblLots.Text = Val.ToString(DtabPacketIss.Rows.Count);
                            DtabPacketIss.Rows.Add(DRNew);
                            DtabPacketIss.AcceptChanges();
                            txtPacketNo.Text = ObjPolish.FindNewPolishOKPacketNo(Val.ToInt64(txtKapan.Tag), pIntMainPolishPacket_ID).ToString();

                            //GetRoughBalance();
                            GetMainPacketBalance();

                         
                            txtSize.Text = "";

                            txtExpCarat.Text = "";
                            txtExpPer.Text = "";

                            txtExpLossCarat.Text = "";
                            txtExpLossPer.Text = "";

                            txtCharni.Text = "";
                            txtCharni.Tag = "";

                            txtLabourRate.Text = "0";
                            CmbLabourType.SelectedIndex = -1;

                            txtClarity.Text = "";
                            txtClarity.Tag = "";

                            txtPcs.Focus();

                            if (GrdDet.RowCount == Val.ToInt(txtRequiredJanged.Text)) //Cmnt #P : 29-08-2022 : Coz : One By One Packet Transfer And Print JangedDetail
                            {
                                BtnPrint_Click(null, null);
                            }                            
                        }
                        else
                        {
                            txtPcs.Focus();
                        }

                        Property = null;
                    

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);

            }
        }


        public bool ValSave()
        {
            if (PanelKapan.Enabled == true)
            {
                Global.Message("First Press the [Cont.] Button For Further Process");
                BtnContinue.Focus();
                return false;
            }
            if (txtKapan.Text.Trim() == string.Empty)
            {
                Global.Message("Kapan Name Is Required");
                return false;
            }
            if (txtParty.Text.Trim() == string.Empty)
            {
                Global.Message("Party Name Is Required");
                txtParty.Focus();
                return false;
            }
            if (txtProcess.Text.Trim() == string.Empty)
            {
                Global.Message("Process Name Is Required");
                txtProcess.Focus();
                return false;
            }
            if (Val.Val(txtPacketNo.Text) == 0)
            {
                Global.Message("Packet Number Not Generated... Please Check In Program");
                txtPacketNo.Focus();
                return false;
            }
            if (Val.Val(txtPcs.Text) == 0)
            {
                Global.Message("Packet Pcs Is Required");
                txtPcs.Focus();
                return false;
            }
            if (Val.Val(txtCarat.Text) == 0)
            {
                Global.Message("Packet Carat Is Required");
                txtCarat.Focus();
                return false;
            }
            if (Val.Val(txtCarat.Text) < 0)
            {
                Global.Message("Please Enter Proper Packet Carat.");
                txtCarat.Focus();
                return false;
            }



            if (Math.Round(Val.Val(txtCarat.Text), 3) > Math.Round(Val.Val(txtBalanceCarat.Text), 3))
            {
                Global.Message("Your Issue Carat Is Greater Than Balance Carat");
                txtCarat.Focus();
                return false;
            }

            //for Check Process and Charni not empty and Labour Rate is 0
            if (!txtProcess.Text.Trim().Equals(string.Empty) && !txtCharni.Text.Trim().Equals(string.Empty) && Val.Val(txtLabourRate.Text) <= 0)
            {
                Global.Message("Kindly Define A Labour Rate.");
                txtLabourRate.Focus();
                return false;
            }

            return true;
        }


        private void txtExpCarat_Validating(object sender, CancelEventArgs e)
        {
            if (Val.Val(txtCarat.Text) > 0)
            {
                txtExpPer.Text = Val.ToString(Math.Round(Val.Val(txtExpCarat.Text) / Val.Val(txtCarat.Text) * 100, 2));
            }
            else
            {
                txtExpPer.Text = "0.00";
            }
            txtExpPer_Validated(null, null);
        }

        private void txtSize_Validating(object sender, CancelEventArgs e)
        {
            //if (Val.Val(txtPcs.Text) > 0 && Val.Val(txtCarat.Text) > 0)
            //{
            //    txtSize.Text = Val.ToString(Math.Round(Val.Val(txtPcs.Text) / Val.Val(txtCarat.Text), 2));
            //}
            //else
            //{
            //    txtSize.Text = "0.00";
            //}
        }

        private void txtPcs_Validating(object sender, CancelEventArgs e)
        {
            if (Val.Val(txtPcs.Text) > 0 && Val.Val(txtCarat.Text) > 0)
            {
                txtSize.Text = Val.ToString(Math.Round(Val.Val(txtCarat.Text) / Val.Val(txtPcs.Text), 2));
            }
            else
            {
                txtSize.Text = "0.00";
            }
        }      

        private void BtnAddProcess_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmParameter);
            FrmParameter.ShowForm("PROCESS");
        }

        private void BtnEmployee_Click(object sender, EventArgs e)
        {
            FrmLedger FrmLedger = new FrmLedger();
            FrmLedger.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmLedger);
            FrmLedger.ShowForm("LEDGER");
        }

        private void BtnAddShape_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmParameter);
            FrmParameter.ShowForm("SHAPE");
        }

        private void BtnAddClarity_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmParameter);
            FrmParameter.ShowForm("CLARITY");
        }

        private void BtnAddCharni_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmParameter);
            FrmParameter.ShowForm("CHARNI");
        }

        private void txtExpPer_TextChanged(object sender, EventArgs e)
        {
            txtExpCarat.Text = Val.ToString(Math.Round(Val.Val(txtCarat.Text) * Val.Val(txtExpPer.Text) / 100, 4));
        }

        private void txtExpLossPer_TextChanged(object sender, EventArgs e)
        {
            txtExpLossCarat.Text = Val.ToString(Math.Round(Val.Val(txtCarat.Text) * Val.Val(txtExpLossPer.Text) / 100, 4));


        }

        private void txtExpPer_Validated(object sender, EventArgs e)
        {
            if (Val.Val(txtExpPer.Text) != 0)
            {
                txtExpLossPer.Text = Math.Round(100 - Val.Val(txtExpPer.Text), 4).ToString();
            }
            else
            {
                txtExpLossPer.Text = string.Empty;
            }
        }

        private void txtCarat_TextChanged(object sender, EventArgs e)
        {
            if (Val.Val(txtPcs.Text) > 0 && Val.Val(txtCarat.Text) > 0)
            {
                txtSize.Text = Val.ToString(Math.Round(Val.Val(txtCarat.Text) / Val.Val(txtPcs.Text), 4));
            }
            else
            {
                txtSize.Text = "0.00";
            }
            txtExpPer_Validated(null, null);
            txtExpLossPer_Validated(null, null);
        }

        private void txtExpLossCarat_Validating(object sender, CancelEventArgs e)
        {
            if (Val.Val(txtCarat.Text) > 0)
            {
                txtExpLossPer.Text = Val.ToString(Math.Round(Val.Val(txtExpLossCarat.Text) / Val.Val(txtCarat.Text) * 100, 2));
            }
            else
            {
                txtExpLossPer.Text = "0.00";
            }
            txtExpLossPer_Validated(null, null);
        }

        private void txtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANMIX);

                    FrmSearch.mColumnsToHide = "KAPAN_ID,PACKET_ID,MARKER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapan.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtKapan.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);

                        //GetRoughBalance();
                        //txtMainPktNo.Text = Val.ToString(FrmSearch.mDRow["PACKETNO"]);
                        //txtMainPktNo.Tag = Val.ToString(FrmSearch.mDRow["PACKET_ID"]);
                        txtMarker.Text = Val.ToString(FrmSearch.mDRow["MARKERCODE"]);
                        txtMarker.Tag = Val.ToString(FrmSearch.mDRow["MARKER_ID"]);

                        GetMainPacketBalance();
                    }
                    else
                    {
                        txtKapan.Text = Val.ToString(DBNull.Value);
                        txtKapan.Tag = Val.ToString(DBNull.Value);
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

        private void txtExpLossPer_Validated(object sender, EventArgs e)
        {
            if (Val.Val(txtExpLossPer.Text) != 0)
            {
                txtExpPer.Text = Math.Round(100 - Val.Val(txtExpLossPer.Text), 4).ToString();
            }
            else
            {
                txtExpPer.Text = string.Empty;
            }
        }

        private void txtCharni_Validating(object sender, CancelEventArgs e)
        {
            string LabourType = string.Empty;
            double LabourRate = 0;
            if (txtCharni.Text.Trim().Equals(string.Empty))
                txtCharni.Tag = string.Empty;
            DataTable DtLabour = ObjPacket.GetLabourRateAndType(Val.ToInt32(txtProcess.Tag), Val.ToInt32(txtCharni.Tag));
            if (DtLabour.Rows.Count > 0)
            {
                LabourType = Val.ToString(DtLabour.Rows[0]["LABOURTYPE"]);
                LabourRate = Val.Val(DtLabour.Rows[0]["LABOURRATE"]);

                CmbLabourType.Text = LabourType;
                txtLabourRate.Text = Val.ToString(LabourRate);
            }
            else
            {
                CmbLabourType.SelectedIndex = -1;
                txtLabourRate.Text = string.Empty;
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

                    BusLib.BOComboFill ObjCmb = new BOComboFill();
                    FrmSearch.mDTab = ObjCmb.FillCombo(BusLib.BOComboFill.TABLE.MST_REQUIREDPROCESS, Val.ToInt32(txtProcess.Tag));
                    //FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);

                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtNextProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtNextProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                PanelKapan.Enabled = true;

                txtKapan.Text = string.Empty;
                txtKapan.Tag = string.Empty;               
                txtProcess.Text = string.Empty;
                txtProcess.Tag = string.Empty;

                txtPacketNo.Text = string.Empty;

                txtNextProcess.Text = string.Empty;
                txtNextProcess.Tag = string.Empty;

                txtParty.Text = string.Empty;
                txtParty.Tag = string.Empty;
                txtDepartment.Text = string.Empty;
                txtDepartment.Tag = string.Empty;
                txtManager.Text = string.Empty;
                txtManager.Tag = string.Empty;
                mBoolAutoConfirm = false;
                txtBalanceCarat.Text = string.Empty;

                txtMarker.Text = string.Empty;
                txtMarker.Tag = string.Empty;

                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;

                txtKapan.Focus();

            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtPcs.Focus();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }       

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                if (Global.Confirm("Are You Sure To Print The Janged # " + txtJangedNo.Text + " ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string pStrOpe = "";                  

                    DataTable DTabPrint = ObjPolish.PopupJangedForPolishPrint(Val.ToInt64(txtJangedNo.Text), "SUMMARY");

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;                    
                    FrmReportViewer.ShowForm("JangedPrintSummaryWithDuplicatePolish", DTabPrint);
                    
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                Global.Message(ex.Message);

            }
        }

        
    }
}
