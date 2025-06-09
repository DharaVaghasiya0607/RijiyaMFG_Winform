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
using AxoneMFGRJ.Report;
using BarcodeLib.Barcode;
using BusLib.Master;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleGoodsFinalIssue : DevExpress.XtraEditors.XtraForm
    {

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();

        DataTable DTabPacket = new DataTable();
        string mStrBPrintType = "";

        FORMTYPE mFormType = FORMTYPE.SINGLE;
        public enum FORMTYPE
        {
            SINGLE = 0,
            LOOSE = 1
        }      

        #region Property Settings

        public FrmSingleGoodsFinalIssue()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType)
        {
            mFormType = pFormType;

            if (mFormType == FORMTYPE.SINGLE)
            {
                this.Name = "FinalEmployeeIssue";
                this.Text = "FINAL EMPLOYEE ISSUE (AUTO)";
                lblEmployee.Visible = false;
                txtFinalEmployee.Visible = false;
            }
            else if (mFormType == FORMTYPE.LOOSE)
            {
                this.Name = "FinalEmployeeIssueManual";
                this.Text = "FINAL EMPLOYEE ISSUE (MANUAL)";
                lblEmployee.Visible = true;
                txtFinalEmployee.Visible = true;
            }

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DTPTransferDate.Value = DateTime.Now;

            DTabPacket = new DataTable();
            DTabPacket.Columns.Add(new DataColumn("PACKET_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("KAPAN_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("PACKETNO", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("TAG", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("ISSUEPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("ISSUECARAT", typeof(double)));

            DTabPacket.Columns.Add(new DataColumn("FROMEMPLOYEE_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("FROMEMPLOYEECODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("FROMEMPLOYEENAME", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("FROMMANAGER_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("TOMANAGER_ID", typeof(Int64)));

            DTabPacket.Columns.Add(new DataColumn("TOEMPLOYEE_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("TOEMPLOYEECODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("TOEMPLOYEENAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("TODESIGNATION", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("AUTOCONFIRM", typeof(bool)));

            DTabPacket.Columns.Add(new DataColumn("FROMDEPARTMENT_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("FROMDEPARTMENTNAME", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("TODEPARTMENT_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("TODEPARTMENTNAME", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("FROMPROCESS_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("FROMPROCESSNAME", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("OLDTRN_ID", typeof(Int64)));

            DTabPacket.Columns.Add(new DataColumn("PRDTYPE_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("PRD_ID", typeof(Int64)));

            DTabPacket.Columns.Add(new DataColumn("PKTSERIALNO", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("PARENTTAG", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("PACKETGROUPNAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("PACKETGRADENAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("COLORSHADECODE", typeof(string)));

            MainGrid.DataSource = DTabPacket;
            GrdDet.RefreshData();

            CalculateSummary();

            txtProcessTo.Focus();

            EmployeeActionRightsProperty PropertyEmployeeActionRights = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
            mStrBPrintType = PropertyEmployeeActionRights.BPRINTTYPE;
            lblBPrintType.Text = "(" + mStrBPrintType + ")";

            RbtBarcode_CheckedChanged(null, null);
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
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        public void CalculateSummary()
        {
            int IntPcs = 0;
            double DouCarat = 0;

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntPcs = IntPcs + 1;
                DouCarat = DouCarat + Val.Val(DRow["ISSUECARAT"]);
            }

            txtTotalPcs.Text = IntPcs.ToString();
            txtTotalCarat.Text = DouCarat.ToString();

        }

        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }

        }

        private void txtFinalEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtFinalEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtFinalEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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
        private void txtToProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESSFINAL);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtProcessTo.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcessTo.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        DTabPacket.AcceptChanges();
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

        public bool ValSave()
        {
            if (txtProcessTo.Text.Trim().Length == 0)
            {
                Global.MessageError("For Process Field Is Required");
                txtProcessTo.Focus();
                return false;
            }
            return true;
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                //string EntryType = "EMPISS";
                string EntryType = "";
               
                /*
                if (txtProcessTo.Text.ToString().ToUpper().Contains("MANAGER"))
                {
                    EntryType = "TRANSFER";
                }
                else
                {
                    EntryType = "EMPISS";
                }
                 * */

                if (mFormType == FORMTYPE.LOOSE)
                {
                    EntryType = "TRANSFER";
                }
                else
                {
                    if (txtProcessTo.Text.ToString().ToUpper().Contains("MANAGER"))
                    {
                        EntryType = "TRANSFER";
                    }
                    else
                    {
                        EntryType = "EMPISS";
                    }
                }

                int IntSrNo = 0;
                txtJangedNo.Text = string.Empty;

                if (mFormType == FORMTYPE.SINGLE)
                {
                    //Add : Pinali : 13-08-2019 : Check For Sub Packets Checker/Final Prd Entry is Entered or Not
                    foreach (DataRow DRow in DTabPacket.Rows)
                    {
                        DataTable Dt = ObjTrn.CheckFinalIssueValForPrd(Val.ToString(DRow["KAPANNAME"]), Val.ToInt32(DRow["PACKETNO"]), Val.ToString(DRow["TAG"]), Val.ToString(DRow["PRD_ID"]));
                        if (Dt.Rows.Count <= 0)
                        {
                            string StrKapan = Val.ToString(DRow["KAPANNAME"]) + "/" + Val.ToString(DRow["PACKETNO"]) + "" + Val.ToString(DRow["TAG"]);

                            Global.MessageError("Checker/Final Prd Entry Is Not Exists In Emp Code : '" + Val.ToString(DRow["TOEMPLOYEECODE"]) + "' \n For Packet : '" + StrKapan + "'...Please Check.");
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }
                    //End : Pinali : 13-08-2019
                }
              


                foreach (DataRow DRow in DTabPacket.Rows)
                {
                    if (Val.Val(DRow["ISSUECARAT"]) == 0 && Val.ToString(DRow["FROMEMPLOYEECODE"]).Length == 0)
                    {
                        continue;
                    }

                    IntSrNo++;
                    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                    Property.TRN_ID = 0;
                    Property.OLDTRN_ID = Val.ToInt64(DRow["OLDTRN_ID"].ToString());
                    Property.KAPAN_ID = Val.ToInt64(DRow["KAPAN_ID"].ToString());
                    Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"].ToString());

                    Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"].ToString());
                    Property.PACKETNO = Val.ToInt(DRow["PACKETNO"].ToString());
                    Property.TAG = Val.ToString(DRow["TAG"].ToString());


                    Property.JANGEDNO = Val.ToInt64(txtJangedNo.Text);
                    Property.ENTRYSRNO = IntSrNo;
                    Property.ENTRYTYPE = EntryType;

                    Property.FROMDEPARTMENT_ID = Val.ToInt32(DRow["FROMDEPARTMENT_ID"]);
                    Property.TODEPARTMENT_ID = Val.ToInt32(DRow["TODEPARTMENT_ID"]);

                    Property.FROMEMPLOYEE_ID = Val.ToInt64(DRow["FROMEMPLOYEE_ID"]);
                    Property.TOEMPLOYEE_ID = Val.ToInt64(DRow["TOEMPLOYEE_ID"]);

                    Property.FROMMANAGER_ID = Val.ToInt64(DRow["FROMMANAGER_ID"]);
                    Property.TOMANAGER_ID = Val.ToInt64(DRow["TOMANAGER_ID"]);

                    Property.FROMPROCESS_ID = Val.ToInt32(DRow["FROMPROCESS_ID"]);
                    Property.TOPROCESS_ID = Val.ToInt32(txtProcessTo.Tag);
                    Property.NEXTPROCESS_ID = Val.ToInt32(txtProcessTo.Tag);

                    Property.ISSUEPCS = Val.ToInt32(DRow["ISSUEPCS"]);
                    Property.ISSUECARAT = Val.Val(DRow["ISSUECARAT"]);

                    Property.RETURNTYPE = "DONE";

                    Property.READYPCS = Val.ToInt32(DRow["ISSUEPCS"]);
                    Property.READYCARAT = Val.Val(DRow["ISSUECARAT"]);

                    Property.PRD_ID = Val.ToInt64(Val.ToString(DRow["PRD_ID"]));
                    Property.PRDTYPE_ID = Val.ToInt32(DRow["PRDTYPE_ID"]);

                    Property.RRPCS = 0;
                    Property.RRCARAT = 0;

                    Property.LOSTPCS = 0;
                    Property.LOSTCARAT = 0;
                    Property.LOSSCARAT = 0;
                    Property.MIXINGLESSPLUS = 0;

                    //Property.TRANSDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());
                    Property.TRANSDATE = DTPTransferDate.Value.ToString();
                    Property.TRANSTYPE = EntryType;
                    Property.REMARK = "Final Issue Transfer";
                    Property.AUTOCONFIRM = Val.ToBoolean(DRow["AUTOCONFIRM"]);
                    Property.ISFROMFINALISSUE = true;
                    Property = ObjTrn.TransferGoods(Property);
                    txtJangedNo.Text = Property.JANGEDNO.ToString();
                    if (Property.ReturnMessageType == "FAIL")
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError(Property.ReturnMessageDesc);
                        this.Cursor = Cursors.WaitCursor;
                    }

                    if (ChkPrintBarcode.Checked == true)
                    {
                        //Global.BarcodePrint(Val.ToString(DRow["KAPANNAME"]),
                        //              Val.ToString(DRow["PACKETNO"]),
                        //              Val.ToString(DRow["TAG"]),
                        //              Val.ToString(DTPTransferDate.Value.ToString("dd-MM-yy")),
                        //              Val.ToString(DRow["ISSUECARAT"]),
                        //              Val.ToString(DRow["TOEMPLOYEECODE"]));
                        //Changes : #P : 20-09-2022
                        string StrGroup = Val.ToString(DRow["PACKETGRADENAME"]) + "/" + Val.ToString(DRow["PACKETGROUPNAME"]);
                        string StrParentTag = Val.ToString(DRow["PARENTTAG"]).Trim() == "" ? "" : "(" + Val.ToString(DRow["PARENTTAG"]) + ")";
                        string StrPktSerialNo = Val.ToString(DRow["PKTSERIALNO"]);

                        if (mStrBPrintType == "TSC")
                        {
                            Global.BarcodePrintTSC(Property.KAPANNAME,
                                   Val.ToString(Property.PACKETNO),
                                   Val.ToString(Property.TAG),
                                   Property.TRANSDATE,              //Date
                                   Val.ToString(Property.ISSUECARAT), //Carat
                                   Val.ToString(DRow["TOEMPLOYEECODE"]), //MarkerCode,
                                   Val.ToString(DRow["BARCODE"]),  //BarcodeNo
                                   StrGroup,  //PktGroup
                                   StrParentTag   //ParentTag
                                   );
                        }
                        else if (mStrBPrintType == "CITIZEN")
                        {
                            Global.BarcodePrintCitizen(Property.KAPANNAME, //Coz: Used Only on Temp Marker
                              Val.ToString(Property.PACKETNO),
                              Val.ToString(Property.TAG),
                              Property.TRANSDATE,
                              Val.ToString(Property.ISSUECARAT),
                              Val.ToString(DRow["TOEMPLOYEECODE"]), //MarkerCode,
                              Val.ToString(DRow["BARCODE"]),
                              StrPktSerialNo,        //PktSerialNo
                              StrParentTag
                              );
                        }
                        else if (mStrBPrintType == "TSCGALAXY")
                        {
                            Global.BarcodePrintTSCGalaxy(Property.KAPANNAME, //Coz: Used Only on Temp Marker
                            Val.ToString(Property.PACKETNO),
                            Val.ToString(Property.TAG),
                            Val.ToString(Property.ISSUECARAT),
                                //Property.TRANSDATE,
                            Val.ToString(DRow["TOEMPLOYEECODE"]), //MarkerCode,
                            StrGroup,
                            "",
                            Val.ToString(DRow["BARCODE"])
                            );
                        }
                        //End : #P : 20-09-2022

                    }

                    Property = null;
                }

                this.Cursor = Cursors.Default;
                if (Val.Val(txtJangedNo.Text) != 0)
                {
                    //Global.Message("Your Goods Successfully Transfer To : " + txtTransferTo.Text + "\n\nYour Slip Number : " + txtJangedNo.Text);
                    BtnPrint_Click(null, null);
                    DTabPacket.Rows.Clear();
                    txtProcessTo.Text = "";
                    txtProcessTo.Tag = "";
                    txtFinalEmployee.Text = "";
                    txtFinalEmployee.Tag = "";
                    txtKapanName.Text = "";
                    txtPacketNo.Text = "";
                    txtTag.Text = "";
                    txtJangedNo.Text = "";
                    txtTotalCarat.Text = "";
                    txtTotalPcs.Text = "";
                    ChkPrintBarcode.Checked = false;
                    txtProcessTo.Focus();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }


        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            string pIntJangedNo = "";

            if (txtJangedNo.Text == string.Empty)
            {
                Global.MessageError("Please Enter Slip No");
                txtJangedNo.Focus();
                return;
            }

            var barcode = new Linear();
            barcode.Type = BarcodeType.CODE128;
            barcode.ShowText = false;
            pIntJangedNo = Val.ToString(txtJangedNo.Text);
            barcode.Data = pIntJangedNo;
            string pStrOpe = "SUMMARY";

            DataTable DTab = ObjTrn.PopupJangedForPrint("ROUGH", Val.ToInt64(txtJangedNo.Text), null, pStrOpe);

            if (DTab.Rows.Count == 0)
            {
                Global.MessageError("There Is No Data For Print");
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                DRow["Barcode"] = barcode.drawBarcodeAsBytes();
            }

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.MdiParent = Global.gMainRef;
            FrmReportViewer.ShowWithPrint("JangedPrintSummaryWithDuplicate", DTab);
        }

        private void txtJangedNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "JANGEDNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjTrn.PopupJangedForPrint("", 0);
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtJangedNo.Text = Val.ToString(FrmSearch.mDRow["JANGEDNO"]);
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


        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRequiredProcess_Validated(object sender, EventArgs e)
        {
            BtnSave.Focus();
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtProcessTo.Text.Trim().Length == 0)
                {
                    Global.MessageError("Transfer Process IS Required");
                    txtProcessTo.Focus();
                    return;
                }

                if (mFormType == FORMTYPE.LOOSE)
                {
                    if (txtFinalEmployee.Text.Trim().Length == 0)
                    {
                        Global.MessageError("Final Employee IS Required");
                        txtFinalEmployee.Focus();
                        return;
                    }
                }

                if (RbtPacketNo.Checked)
                {
                    if (txtKapanName.Text.Trim().Length == 0)
                    {
                        return;
                    }
                    if (Val.ToInt32(txtPacketNo.Text.Trim()) == 0)
                    {
                        return;
                    }
                    if (txtTag.Text.Trim().Length == 0)
                    {
                        return;
                    }
                }
                else if (RbtBarcode.Checked)
                {
                    if (txtBarcode.Text.Trim().Length == 0)
                    {
                        return;
                    }
                }
                else if (RbtPktSerialNo.Checked)
                {
                    if (txtSrNoKapanName.Text.Trim().Length == 0)
                    {
                        return;
                    }
                    if (Val.ToInt(txtSrNoSerialNo.Text) == 0)
                    {
                        txtSrNoSerialNo.Focus();
                        return;
                    }
                }

                string Str = txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text;

                //Add : Pinali : 10-08-2019
                //DataRow DRow = ObjTrn.GetFinalEmployeeIssPacketInfo(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, Val.ToInt(txtProcessTo.Tag), txtProcessTo.Text);

                DataTable DtabPktsInfo = new DataTable();
                //DataRow DRow = new DataRow();
                
                if (RbtPacketNo.Checked)
                {
                    DtabPktsInfo = ObjTrn.GetFinalEmployeeIssPacketInfo(Val.ToInt64(txtFinalEmployee.Tag), txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, "", 0, Val.ToInt(txtProcessTo.Tag), txtProcessTo.Text);
                }
                else if (RbtBarcode.Checked)
                {
                    DtabPktsInfo = ObjTrn.GetFinalEmployeeIssPacketInfo(Val.ToInt64(txtFinalEmployee.Tag), "", 0, "", txtBarcode.Text, 0, Val.ToInt(txtProcessTo.Tag), txtProcessTo.Text);
                }
                else if (RbtPktSerialNo.Checked)
                {
                    DtabPktsInfo = ObjTrn.GetFinalEmployeeIssPacketInfo(Val.ToInt64(txtFinalEmployee.Tag), txtSrNoKapanName.Text, 0, "", txtBarcode.Text, Val.ToInt(txtSrNoSerialNo.Text), Val.ToInt(txtProcessTo.Tag), txtProcessTo.Text);
                }
                
                if (DtabPktsInfo.Rows.Count <= 0)
                {
                    Global.MessageError("Oops. " + Str + "  Packet Is Not In Stock. Please Check In Running Stock");
                    txtKapanName.Text = string.Empty;
                    txtPacketNo.Text = string.Empty;
                    txtTag.Text = string.Empty;
                    txtBarcode.Text = string.Empty;
                    txtSrNoKapanName.Text = string.Empty;
                    txtSrNoSerialNo.Text = string.Empty;
                    if (RbtPacketNo.Checked)
                    {
                        txtKapanName.Focus();
                    }
                    else if (RbtBarcode.Checked)
                    {
                        txtBarcode.Focus();
                    }
                    else if (RbtPktSerialNo.Checked)
                    {
                        txtSrNoKapanName.Focus();
                    }
                  
                    return;
                }


                foreach (DataRow DRow in DtabPktsInfo.Rows)
                {
                    if (DRow == null)
                    {
                        Global.MessageError("Oops. " + Str + "  Packet Is Not In Stock. Please Check In Running Stock");

                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;
                        txtBarcode.Text = string.Empty;
                        txtSrNoKapanName.Text = string.Empty;
                        txtSrNoSerialNo.Text = string.Empty;
                        if (RbtPacketNo.Checked)
                        {
                            txtKapanName.Focus();
                        }
                        else if (RbtBarcode.Checked)
                        {
                            txtBarcode.Focus();
                        }
                        else if (RbtPktSerialNo.Checked)
                        {
                            txtSrNoKapanName.Focus();
                        }

                        return;
                    }

                    bool ISExists = false;
                    foreach (DataRow DD in DTabPacket.Rows)
                    {
                        if (Val.ToString(DD["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"]))
                        {
                            ISExists = true;
                            break;
                        }
                    }


                    if (ISExists == false)
                    {
                        DataRow DRNew = DTabPacket.NewRow();

                        DRNew["PACKET_ID"] = DRow["PACKET_ID"];
                        DRNew["KAPAN_ID"] = DRow["KAPAN_ID"];
                        DRNew["KAPANNAME"] = DRow["KAPANNAME"];
                        DRNew["PACKETNO"] = DRow["PACKETNO"];
                        DRNew["TAG"] = DRow["TAG"];
                        DRNew["ISSUEPCS"] = DRow["ISSUEPCS"];
                        DRNew["ISSUECARAT"] = DRow["ISSUECARAT"];
                        DRNew["OLDTRN_ID"] = DRow["OLDTRN_ID"];
                        DRNew["FROMEMPLOYEE_ID"] = DRow["FROMEMPLOYEE_ID"];
                        DRNew["FROMEMPLOYEECODE"] = DRow["FROMEMPLOYEECODE"];
                        DRNew["FROMEMPLOYEENAME"] = DRow["FROMEMPLOYEENAME"];
                        DRNew["TOEMPLOYEE_ID"] = DRow["TOEMPLOYEE_ID"];
                        DRNew["TOEMPLOYEECODE"] = DRow["TOEMPLOYEECODE"];
                        DRNew["TOEMPLOYEENAME"] = DRow["TOEMPLOYEENAME"];
                        DRNew["TODESIGNATION"] = DRow["TODESIGNATION"];
                        DRNew["AUTOCONFIRM"] = DRow["AUTOCONFIRM"];
                        DRNew["FROMDEPARTMENT_ID"] = DRow["FROMDEPARTMENT_ID"];
                        DRNew["FROMDEPARTMENTNAME"] = DRow["FROMDEPARTMENTNAME"];
                        DRNew["TODEPARTMENT_ID"] = DRow["TODEPARTMENT_ID"];
                        DRNew["TODEPARTMENTNAME"] = DRow["TODEPARTMENTNAME"];
                        DRNew["FROMPROCESS_ID"] = DRow["FROMPROCESS_ID"];
                        DRNew["FROMPROCESSNAME"] = DRow["FROMPROCESSNAME"];

                        DRNew["FROMMANAGER_ID"] = DRow["FROMMANAGER_ID"];
                        DRNew["TOMANAGER_ID"] = DRow["TOMANAGER_ID"];

                        DRNew["PRDTYPE_ID"] = DRow["PRDTYPE_ID"];
                        DRNew["PRD_ID"] = DRow["PRD_ID"];

                        DRNew["PKTSERIALNO"] = DRow["PKTSERIALNO"];
                        DRNew["BARCODE"] = DRow["BARCODE"];
                        DRNew["PARENTTAG"] = DRow["PARENTTAG"];
                        DRNew["PACKETGROUPNAME"] = DRow["PACKETGROUPNAME"];
                        DRNew["PACKETGRADENAME"] = DRow["PACKETGRADENAME"];
                        DRNew["COLORSHADECODE"] = DRow["COLORSHADECODE"];

                        DTabPacket.Rows.Add(DRNew);
                        DTabPacket.AcceptChanges();
                    }
                    else
                    {
                        Global.MessageError(Str + " Is Already In Grid Pls Check");
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;
                        txtBarcode.Text = string.Empty;
                        txtSrNoKapanName.Text = string.Empty;
                        txtSrNoSerialNo.Text = string.Empty;
                        if (RbtPacketNo.Checked)
                        {
                            txtKapanName.Focus();
                        }
                        else if (RbtBarcode.Checked)
                        {
                            txtBarcode.Focus();
                        }
                        else if (RbtPktSerialNo.Checked)
                        {
                            txtSrNoKapanName.Focus();
                        }
                        return;
                    }
                }

                if (DtabPktsInfo.Rows.Count > 1)
                {
                    //Global.Message("There Is SubPackets Are Exists Which Has No Ownership");
                    Global.Message("There Is SubPackets Are Exists Which Has No Ownership (Displayed In List).");
                }

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                if (RbtPacketNo.Checked)
                {
                    txtKapanName.Focus();
                }
                else if (RbtBarcode.Checked)
                {
                    txtBarcode.Focus();
                }
                else if (RbtPktSerialNo.Checked)
                {
                    txtSrNoKapanName.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
                this.Cursor = Cursors.Default;
            }
        }

        private void txtFinalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjTrn.GetFinalEmployeeIssEmployee(Val.ToString(DRow["KAPANNAME"]), Val.ToInt(DRow["PACKETNO"]), Val.ToString(DRow["TAG"]), Val.ToInt(txtProcessTo.Tag), txtProcessTo.Text);
                    FrmSearch.mColumnsToHide = "PACKETNO,TAG,PACKET_ID,EMPLOYEE_ID,DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("TOEMPLOYEE_ID", Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]));
                        GrdDet.SetFocusedRowCellValue("TOEMPLOYEECODE", Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]));
                        GrdDet.SetFocusedRowCellValue("TOEMPLOYEENAME", Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]));
                        GrdDet.SetFocusedRowCellValue("TODESIGNATION", Val.ToString(FrmSearch.mDRow["DESIGNATIONNAME"]));
                        GrdDet.SetFocusedRowCellValue("AUTOCONFIRM", Val.ToString(FrmSearch.mDRow["AUTOCONFIRM"]));
                        GrdDet.SetFocusedRowCellValue("TODEPARTMENT_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
                        GrdDet.SetFocusedRowCellValue("TODEPARTMENTNAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));
                        GrdDet.SetFocusedRowCellValue("PRDTYPE_ID", Val.ToString(FrmSearch.mDRow["PRDTYPE_ID"]));
                        GrdDet.SetFocusedRowCellValue("PRD_ID", Val.ToString(FrmSearch.mDRow["PRD_ID"]));
                        GrdDet.SetFocusedRowCellValue("TOMANAGER_ID", Val.ToString(FrmSearch.mDRow["MANAGER_ID"]));

                        DTabPacket.AsEnumerable().Where(r => ((string)r["KAPANNAME"]).Equals(Val.ToString(DRow["KAPANNAME"]))
                                                        && ((int)r["PACKETNO"]) == Val.ToInt32(DRow["PACKETNO"])
                                                        )
                                                        .ToList().ForEach(
                                                        row =>
                                                        {
                                                            row["TOEMPLOYEE_ID"] = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                                                            row["TOEMPLOYEECODE"] = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                                                            row["TOEMPLOYEENAME"] = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                                                            row["TODESIGNATION"] = Val.ToString(FrmSearch.mDRow["DESIGNATIONNAME"]);
                                                            row["AUTOCONFIRM"] = Val.ToString(FrmSearch.mDRow["AUTOCONFIRM"]);
                                                            row["TODEPARTMENT_ID"] = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
                                                            row["TODEPARTMENTNAME"] = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                                                            row["PRDTYPE_ID"] = Val.ToString(FrmSearch.mDRow["PRDTYPE_ID"]);
                                                            row["PRD_ID"] = Val.ToString(FrmSearch.mDRow["PRD_ID"]);
                                                            row["TOMANAGER_ID"] = Val.ToString(FrmSearch.mDRow["MANAGER_ID"]);
                                                        }
                                                        );
                        //.First(); // getting the row to edit , change it as you need
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

        private void GrdDet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle < 0)
                    return;

                DataRow dr = GrdDet.GetFocusedDataRow();

                if (Val.ToInt(txtProcessTo.Tag) == 464) //FinalPrediction
                {
                    if (Val.ToString(dr["TAG"]) != "A")
                        GrdDet.Columns["TOEMPLOYEECODE"].OptionsColumn.AllowEdit = false;
                    else
                        GrdDet.Columns["TOEMPLOYEECODE"].OptionsColumn.AllowEdit = true;
                }
                else if (Val.ToInt(txtProcessTo.Tag) == 503) //FullTop
                {
                    GrdDet.Columns["TOEMPLOYEECODE"].OptionsColumn.AllowEdit = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void RbtBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtBarcode.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                txtBarcode.Focus();
            }
            else if (RbtPacketNo.Checked)
            {
                txtJangedNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                txtKapanName.Focus();
            }
            else if (RbtPktSerialNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Focus();
            }
            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;
        }


    }
}
