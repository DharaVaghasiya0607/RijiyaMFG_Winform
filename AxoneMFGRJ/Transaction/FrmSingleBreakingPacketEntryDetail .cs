using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleBreakingPacketEntryDetail : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOFormPer ObjPer = new BOFormPer();
        DataSet DSetBreaking = new DataSet();

        DataTable DTabBeforeData = new DataTable();
        DataTable DTabAfterData = new DataTable();
        DataTable DTabParameter = new DataTable();
        DataTable DTabRapDate = new DataTable();
        DataTable DtabBreakingType = new DataTable();

        BOFindRap ObjRap = new BOFindRap();

        Int64 mGroup_ID = 0;

        BOTRN_SingleBreakingPacketEntry ObjBrk = new BOTRN_SingleBreakingPacketEntry();

        #region Property Settings

        public FrmSingleBreakingPacketEntryDetail()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            txtPassForDisplayBack.Tag = ObjPer.PASSWORD;

            txtPassForDisplayBack_TextChanged(null, null);

            BtnDelete.Enabled = ObjPer.ISDELETE;

            this.Show();

            FillControl();
            Clear();

            //DTPFromDate.Focus();
        }
        public void ShowForm(string pStrPacket_ID, string pIntBrkType, Int64 pIntEmployee_ID,string pStrKapanName ,string pStrPacketNo,string pStrTag)
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            txtPassForDisplayBack.Tag = ObjPer.PASSWORD;

            txtPassForDisplayBack_TextChanged(null, null);
            BtnDelete.Enabled = ObjPer.ISDELETE;

            this.Show();
           
            FillControl();
            Clear();

            CmbBreakingType.SelectedItem = Val.ToString(pIntBrkType);
            txtKapanName.Text = pStrKapanName;
            txtPacketNo.Text = pStrPacketNo;
            txtTag.Text = txtTag.Enabled == true ? pStrTag : string.Empty;
            txtTag.Tag = Val.ToString(pStrPacket_ID);
            txtEmployee.Tag = Val.ToString(pIntEmployee_ID);
            BtnShow.PerformClick();
            CmbBreakingType.Focus();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjBrk);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion


        public void FillControl()
        {
            try
            {
                DataTable DtabParam = ObjRap.GetAllParameterTable();
                DTabParameter = new DataView(DtabParam).ToTable(false, "PARA_ID", "PARACODE", "PARANAME", "PARATYPE");

                //DTabParameter.Rows.Add(0, "", "", "MILKY");
                //DTabParameter.Rows.Add(0, "", "", "LBLC");
                //DTabParameter.Rows.Add(0, "", "", "NATTS");
                //DTabParameter.Rows.Add(0, "", "", "TENSION");
                //DTabParameter.Rows.Add(0, "", "", "NATURAL");
                //DTabParameter.Rows.Add(0, "", "", "BLACK");
                //DTabParameter.Rows.Add(0, "", "", "OPEN");
                //DTabParameter.Rows.Add(0, "", "", "WHITE");
                //DTabParameter.Rows.Add(0, "", "", "HEARTANDARROW");
                //DTabParameter.Rows.Add(0, "", "", "PAVALION");
                //DTabParameter.Rows.Add(0, "", "", "COLORSHADE");
                //DTabParameter.Rows.Add(0, "", "", "LUSTER");
                //DTabParameter.Rows.Add(0, "", "", "EYECLEAN");
                //DTabParameter.Rows.Add(0, "", "", "GRAIN");
                //DTabParameter.Rows.Add(0, "", "", "LAB");

                DTabRapDate = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.RAPDATE);
                DTabRapDate.DefaultView.Sort = "RAPDATE DESC";
                DTabRapDate = DTabRapDate.DefaultView.ToTable();

                DSetBreaking = ObjBrk.GetBreakingPacketDetail();
                DTabBeforeData = DSetBreaking.Tables[0];
                DTabAfterData = DSetBreaking.Tables[1];

                CmbRapDate.Items.Clear();
                foreach (DataRow DRow in DTabRapDate.Rows)
                {
                    CmbRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
                }
                CmbRapDate.SelectedIndex = 0;

                MainGrdBefore.DataSource = DTabBeforeData;
                MainGrdBefore.Refresh();

                MainGrdAfter.DataSource = DTabAfterData;
                MainGrdAfter.Refresh();

                DtabBreakingType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_BREAKINGTYPE);
                foreach (DataRow DRow in DtabBreakingType.Rows)
                {
                    CmbBreakingType.Items.Add(Val.ToString(DRow["BREAKINGTYPECODE"]));
                }
                CmbBreakingType.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        #region Validation

        private bool ValSave()
        {

            if (txtBreakingEmployee.Text.Trim().Equals(string.Empty))
            {
                Global.Message("Breaking Employee Required.");
                txtBreakingEmployee.Focus();
                return true;
            }
            if (txtEmployee.Text.Trim().Equals(string.Empty))
            {
                Global.Message("Employee Required.");
                txtEmployee.Focus();
                return true;
            }
            if (Val.ToInt(CmbBreakingType.Tag) == 0)
            {
                Global.MessageError("Breaking Type Is Required");
                return true;
            }

            if (txtKapanName.Text.Trim().Length == 0)
            {
                Global.MessageError("Kapan Name Is Required");
                txtKapanName.Focus();
                return true;
            }
            if (Val.Val(txtPacketNo.Text) == 0)
            {
                Global.MessageError("Packet No Is Required");
                txtPacketNo.Focus();
                return true;
            }
            if (txtTag.Enabled == true && txtTag.Text.Trim().Length == 0)
            {
                Global.MessageError("Tag Is Required");
                txtTag.Focus();
                return true;
            }

            if (txtTag.Enabled == true && Val.ToString(txtTag.Tag).Length == 0)
            {
                Global.MessageError("Packet ID Not Found In this PacketNo");
                txtTag.Focus();
                return true;
            }

            //int IntCol = 0, IntRow = -1;
            //foreach (DataRow dr in DTabBeforeData.Rows)
            //{
            //    //For Update Validation
            //    if (Val.ToString(dr["EMPLOYEECODE"]).Trim().Equals(string.Empty) && !Val.ToString(dr["BREAKING_ID"]).Trim().Equals(string.Empty))
            //    {
            //        Global.Message("Please Select Employee.");
            //        IntCol = 17;
            //        IntRow = dr.Table.Rows.IndexOf(dr);
            //        break;
            //    }
            //    if (Val.ToString(dr["EMPLOYEECODE"]).Trim().Equals(string.Empty))
            //    {
            //        if (DTabBeforeData.Rows.Count == 1)
            //        {
            //            Global.Message("Please Select Employee.");
            //            IntCol = 17;
            //            IntRow = dr.Table.Rows.IndexOf(dr);
            //            break;

            //        }
            //        else
            //            continue;
            //    }

            //    if (Val.ToString(dr["KAPANNAME"]).Trim().Equals(string.Empty))
            //    {
            //        Global.Message("Please Select Kapan.");
            //        IntCol = 2;
            //        IntRow = dr.Table.Rows.IndexOf(dr);
            //        break;
            //    }
            //    else if (Val.ToInt32(dr["PACKETNO"]) <= 0)
            //    {
            //        Global.Message("Please Select PacketNo.");
            //        IntCol = 3;
            //        IntRow = dr.Table.Rows.IndexOf(dr);
            //        break;
            //    }
            //    else if (Val.Val(dr["AMOUNT"]) <= 0)
            //    {
            //        Global.Message("Amount Should Be Greater Than 0.");
            //        IntCol = 15;
            //        IntRow = dr.Table.Rows.IndexOf(dr);
            //        break;
            //    }
            //}
            //if (IntRow > -1)
            //{
            //    GrdDetBefore.FocusedRowHandle = IntRow;
            //    GrdDetBefore.FocusedColumn = GrdDetBefore.VisibleColumns[IntCol];
            //    GrdDetBefore.Focus();
            //    return true;
            //}
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

        public void Clear()
        {
            //DateTime firstDay = new DateTime(DateTime.Now.Year, 1, 1);
            //DTPFromDate.Text = Val.ToString(new DateTime(DateTime.Now.Year, 1, 1));

            BtnSave.Enabled = false;
            BtnSave.Text = "Save";

            lblDifference.Text = "0";

            DTabBeforeData.Rows.Clear();
            DTabAfterData.Rows.Clear();

            DtabBreakingType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_BREAKINGTYPE);

            mGroup_ID = 0;

            DTPBrkEntryDate.Text = Val.ToString(DateTime.Now);

            lblBeforeBlankRow.Enabled = true;
            lblAfterBlankRow.Enabled = true;
            lblBeforeCopyRow.Enabled = true;
            lblAfterCopyRow.Enabled = true;

            CmbRapDate.SelectedIndex = 0;

            txtKapanName.Text = string.Empty;
            txtKapanName.Tag = string.Empty;

            txtPacketNo.Text = string.Empty;
            txtPacketNo.Tag = string.Empty;

            txtTag.Text = string.Empty;
            txtTag.Tag = string.Empty;

            txtEmployee.Text = string.Empty;
            txtEmployee.Tag = string.Empty;

            txtEmployee.Text = string.Empty;
            txtEmployee.Tag = string.Empty;

            TxtReason.Text = string.Empty;
            TxtReason.Tag = string.Empty;

            DTPBrkEntryDate.Text = Val.ToString(DateTime.Now);

            txtBreakingEmployee.Text = string.Empty;
            txtBreakingEmployee .Tag = string.Empty;

            lblBalance.Text = "0.00";

            txtRemark.Text = string.Empty;

            ChkISConsiderOriginal.Checked = false;
            txtDiffAmount.Text = "0.00";

            if (GrdDetBefore.RowCount == 0)
            {
                InsertFreshPlan(MainGrdBefore, DTabBeforeData, lblBeforeCurrentPlan);
            }
            if (GrdDetAfter.RowCount == 0)
            {
                InsertFreshPlan(MainGrdAfter, DTabAfterData, lblAfterCurrentPlan);
            }
            CmbBreakingType.Focus();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();
            CmbBreakingType.SelectedIndex = 0;
            txtPassForDisplayBack.Text = string.Empty;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValSave())
                {
                    return;
                }
                if (DTabBeforeData.Rows.Count <= 0 || Val.Val(DTabBeforeData.Rows[0]["AMOUNT"]) == 0)
                {
                    Global.MessageError("Please Fill Proper  'Before Breaking'   Detail \n (Entry Should Not Be Blank.. OR ..Amount Should be Greater than Zero)");
                    return;
                }

                if (DTabAfterData.Rows.Count <= 0 || Val.Val(DTabAfterData.Rows[0]["AMOUNT"]) == 0)
                {
                    Global.MessageError("Please Fill Proper  'After Breaking'    Detail \n (Entry Should Not Be Blank.. OR ..Amount Should be Greater than Zero)");
                    return;
                }

                //if (Global.Confirm("Are Your Sure To Save All Records ?") == System.Windows.Forms.DialogResult.No)
                //    return;

                string AlreadyExistsPacketNo = "";

                TRN_SingleBreakingPacketEntry Property = new TRN_SingleBreakingPacketEntry();
                ////Property.BREAKING_ID = mBreaking_ID == Guid.Empty ? Guid.NewGuid() : mBreaking_ID;
                //Property.GROUP_ID = mGroup_ID == Guid.Empty ? Guid.NewGuid() : mGroup_ID;

                if (Val.ToInt64(mGroup_ID) == 0)
                {
                    Property.GROUP_ID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.BREAKGROUP_ID);
                }
                else
                {
                    Property.GROUP_ID = Val.ToInt64(mGroup_ID);
                }


                Property.BREAKINGTYPE_ID = Val.ToInt(CmbBreakingType.Tag);
                Property.BREAKINGTYPE = Val.ToString(CmbBreakingType.Text);

                Property.PACKET_ID = Val.ToInt64(txtTag.Tag);
                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                Property.MTAG = txtTag.Enabled == false ? "A" : txtTag.Text;

                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                Property.BREAKINGEMPLOYEE_ID = Val.ToInt64(txtBreakingEmployee.Tag);
                Property.BREAKINGDATE = Val.SqlDate(DTPBrkEntryDate.Text);

                Property.RAPDATE = Val.SqlDate(CmbRapDate.SelectedItem.ToString());

                Property.REMARK = Val.ToString(txtRemark.Text);

                Property.ISCONSIDERORIGINAL = ChkISConsiderOriginal.Checked;
                Property.DIFFAMOUNT = Val.Val(txtDiffAmount.Text);

                Property.BREAKINGREASON_ID = Val.ToInt32(TxtReason.Tag);

                string BeforeBreakingEntryXml = string.Empty;
                string AfterBreakingEntryXml = string.Empty;

                DTabBeforeData.TableName = "BreakingBefore";
                DTabAfterData.TableName = "BreakingAfter";


                //foreach (DataRow DRow in DTabBeforeData.Rows)
                //{
                //    if (Val.Val(DRow["AMOUNT"]) != 0)
                //    {
                //        DRow["BREAKING_ID"] = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.BREAKING_ID);
                //    }
                //}
                //foreach (DataRow DRow in DTabAfterData.Rows)
                //{
                //    if (Val.Val(DRow["AMOUNT"]) != 0)
                //    {
                //        DRow["BREAKING_ID"] = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.BREAKING_ID);
                //    }
                //}

                using (StringWriter sw = new StringWriter())
                {
                    //DataSet DSFinal = new DataSet("DocumentElement");
                    //DSFinal.Tables.Add(DTabBeforeData.Copy());
                    //DSFinal.WriteXml(sw);
                    DTabBeforeData.WriteXml(sw);
                    BeforeBreakingEntryXml = sw.ToString();
                }
                using (StringWriter sw = new StringWriter())
                {
                    DTabAfterData.WriteXml(sw);
                    AfterBreakingEntryXml = sw.ToString();
                }
                Property = ObjBrk.Save(Property, BeforeBreakingEntryXml, AfterBreakingEntryXml);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message("Records " + BtnSave.Text + "d Successfully");
                    this.Close();
                }
                else
                {
                    //txtItemGroupCode.Focus();
                }
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

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }
        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DataRow DR = GrdDet.GetFocusedDataRow();
            //    FetchValue(DR);
            //    DR = null;
            //}
        }


        public void FetchValue(DataRow DR)
        {
            //txtParaType.Text = Val.ToString(DR["ITEMGROUP_ID"]);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Repairing List", GrdDetBefore);
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                DTabBeforeData.Rows.Clear();
                DTabAfterData.Rows.Clear();

                MainGrdBefore.DataSource = null;
                MainGrdAfter.DataSource = null;

                if (txtKapanName.Text.Length == 0)
                {
                    Global.MessageError("Kapan Name Is Required");
                    txtKapanName.Focus();
                    return;
                }
                if (Val.Val(txtPacketNo.Text) == 0)
                {
                    Global.MessageError("PacketNo Is Required");
                    txtPacketNo.Focus();
                    return;
                }
                string StrTag = "A";

                if (txtTag.Enabled == false)
                {
                    StrTag = "A";
                    lblBeforeBlankRow.Enabled = true;
                    lblAfterBlankRow.Enabled = true;
                    lblBeforeCopyRow.Enabled = true;
                    lblAfterCopyRow.Enabled = true;
                }
                else
                {
                    if (txtTag.Text.Length == 0)
                    {
                        Global.MessageError("Tag Is Required");
                        txtTag.Focus();
                        return;
                    }
                    StrTag = txtTag.Text;
                    lblBeforeBlankRow.Enabled = false;
                    lblAfterBlankRow.Enabled = false;
                    lblBeforeCopyRow.Enabled = false;
                    lblAfterCopyRow.Enabled = false;
                }

                if (Val.Val(txtEmployee.Tag) == 0)
                {
                    Global.MessageError("Employee Is Required");
                    txtEmployee.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                //BtnSave.Enabled = true;

                DataRow DRPkt = ObjRap.GetPacketDataRow(txtKapanName.Text, Val.ToInt32(txtPacketNo.Text), StrTag);
                if (DRPkt == null)
                {
                    Global.MessageError("Ooops.. Packet Is Not Found");
                    return;
                }
                lblMode.Text = "Add Mode";

                txtTag.Tag = Val.ToString(DRPkt["PACKET_ID"]);
                lblBalance.Text = Val.ToString(DRPkt["BALANCECARAT"]);

                DRPkt = null;

                BtnSave.Enabled = true;
                if (Val.ToInt32(CmbBreakingType.Tag) == 5208)//LS-B
                {
                    DSetBreaking = ObjBrk.GetBreakingPacketDetail(Val.ToInt32(CmbBreakingType.Tag), Val.ToString(txtKapanName.Text), Val.ToInt32(txtPacketNo.Text), StrTag, 0, Val.ToInt64(txtEmployee.Tag), 0);
                }
                else
                {
                    DSetBreaking = ObjBrk.GetBreakingPacketDetail(Val.ToInt32(CmbBreakingType.Tag),"", 0, "", Val.ToInt64(txtTag.Tag), Val.ToInt64(txtEmployee.Tag), 0);
                }
                    

                if (DSetBreaking.Tables.Count < 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                DTabBeforeData = DSetBreaking.Tables[0];


                if(DSetBreaking.Tables.Count > 1)
                    DTabAfterData = DSetBreaking.Tables[1];

                if (DTabBeforeData.Rows.Count != 0)
                {
                    CmbRapDate.SelectedItem = DateTime.Parse(Val.ToString(DTabBeforeData.Rows[0]["RAPDATE"])).ToString("dd/MM/yyyy");
                    lblBeforeCurrentPlan.Text = Val.ToString(DTabBeforeData.Rows[0]["PLANNO"]);    //#P : 01-02-2020
                    if (DTabAfterData.Rows.Count != 0)
                    {
                        lblAfterCurrentPlan.Text = Val.ToString(DTabAfterData.Rows[0]["PLANNO"]);    
                    }
                }
                else
                {
                    lblBeforeBlankRow.Enabled = false;
                    lblAfterBlankRow.Enabled = false;

                    lblBeforeCopyRow.Enabled = false;
                    lblAfterCopyRow.Enabled = false;
                }

                if (DTabAfterData.Rows.Count != 0 && Val.ToInt64(DTabAfterData.Rows[0]["GROUP_ID"]) != 0)
                {
                    mGroup_ID = Val.ToInt64(DTabAfterData.Rows[0]["GROUP_ID"]);
                    txtBreakingEmployee.Text = Val.ToString(DTabAfterData.Rows[0]["BREAKINGEMPLOYEECODE"]);
                    txtBreakingEmployee.Tag = Val.ToString(DTabAfterData.Rows[0]["BREAKINGEMPLOYEE_ID"]);
                    txtRemark.Text = Val.ToString(DTabAfterData.Rows[0]["REMARK"]);
                    DTPBrkEntryDate.Text = DTabAfterData.Rows[0]["BREAKINGDATE"].ToString();

                    ChkISConsiderOriginal.Checked = Val.ToBoolean(DTabAfterData.Rows[0]["ISCONSIDERORIGINAL"]);
                    txtDiffAmount.Text = Val.ToString(DTabAfterData.Rows[0]["DIFFAMOUNT"]);

                    BtnSave.Text = "Update";

                    lblMode.Text = "Edit Mode";

                    txtKapanName.Text = Val.ToString(DTabAfterData.Rows[0]["KAPANNAME"]);
                    txtPacketNo.Text = Val.ToString(DTabAfterData.Rows[0]["PACKETNO"]);
                    txtEmployee.Text = Val.ToString(DTabAfterData.Rows[0]["EMPLOYEECODE"]);
                    txtEmployee.Tag = Val.ToString(DTabAfterData.Rows[0]["EMPLOYEE_ID"]);
                    TxtReason.Text = Val.ToString(DTabAfterData.Rows[0]["BREAKINGREASONNAME"]);
                    TxtReason.Tag = Val.ToString(DTabAfterData.Rows[0]["BREAKINGREASON_ID"]);                   
                   
                }

               
                //#P : 30-01-2020
                double DouTotalBeforeAmount = Val.Val(DTabBeforeData.Compute("SUM(AMOUNT)", string.Empty));
                double DouTotalAfterAmount = Val.Val(DTabAfterData.Compute("SUM(AMOUNT)", string.Empty));
                lblDifference.Text = Val.ToString(Val.Val(DouTotalBeforeAmount) - Val.Val(DouTotalAfterAmount));

                MainGrdBefore.DataSource = DTabBeforeData;
                GrdDetBefore.BestFitColumns();
                GrdDetBefore.ExpandAllGroups();

                MainGrdAfter.DataSource = DTabAfterData;
                GrdDetAfter.BestFitColumns();
                GrdDetAfter.ExpandAllGroups();

                if (DTabAfterData.Rows.Count == 0)
                {
                    lblAfterBlankRow_Click(null, null);
                }
                this.Cursor = Cursors.Default;




                //this.Cursor = Cursors.WaitCursor;

                //string StrFromDate = "", StrToDate = "";


                //StrToDate = Val.SqlDate(DTPEntryDate.Text);

                //DtabBreaking = ObjBrk.Fill(StrFromDate, StrToDate);

                //DataRow DrNew = DtabBreaking.NewRow();
                //DrNew["BREAKINGDATE"] = Val.ToString(DateTime.Now);
                //DrNew["BFRAPDATE"] = DateTime.Parse(Val.ToString(DTabRapDate.Rows[0]["RAPDATE"])).ToString("dd/MM/yyyy");
                //DrNew["AFRAPDATE"] = DateTime.Parse(Val.ToString(DTabRapDate.Rows[0]["RAPDATE"])).ToString("dd/MM/yyyy");
                //DrNew["BREAKINGTYPE_ID"] = 0;
                //DtabBreaking.Rows.Add(DrNew);

                //MainGrid.DataSource = DtabBreaking;
                //MainGrid.Refresh();

                //GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
                //GrdDet.FocusedRowHandle = DrNew.Table.Rows.IndexOf(DrNew);
                //GrdDet.Focus();
                //GrdDet.ShowEditor();

                //this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
                this.Cursor = Cursors.Default;
            }
        }

        private void repTxtBFShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    AllParameterKeyPress(GrdDetBefore, "SHAPE", "SHAPE_ID", "SHAPECODE", "SHAPENAME", e.KeyChar.ToString());
                    e.Handled = true;
                }

                //    DataRow[] UDRow = DTabParameter.Select("ParaType = 'SHAPE'");
                //    if (UDRow.Length == 0)
                //    {
                //        return;
                //    }
                //    FrmSearch FrmSearch = new FrmSearch();
                //    FrmSearch.SearchField = "SHAPECODE,SHAPENAME";
                //    FrmSearch.SearchText = e.KeyChar.ToString();
                //    this.Cursor = Cursors.WaitCursor;
                //    FrmSearch.DTab = UDRow.CopyToDataTable(); 
                //    FrmSearch.ColumnsToHide = "SHAPE_ID,IMAGE";
                //    this.Cursor = Cursors.Default;
                //    FrmSearch.ShowDialog();
                //    e.Handled = true;
                //    if (FrmSearch.DRow != null)
                //    {
                //        GrdDetBefore.SetFocusedRowCellValue("SHAPE_ID", Val.ToString(FrmSearch.DRow["PARA_ID"]));
                //        GrdDetBefore.SetFocusedRowCellValue("SHAPECODE", Val.ToString(FrmSearch.DRow["PARACODE"]));
                //        GrdDetBefore.SetFocusedRowCellValue("SHAPENAME", Val.ToString(FrmSearch.DRow["PARANAME"]));
                //    }
                //    else
                //    {
                //        GrdDetBefore.SetFocusedRowCellValue("SHAPE_ID", 0);
                //        GrdDetBefore.SetFocusedRowCellValue("SHAPECODE", string.Empty);
                //        GrdDetBefore.SetFocusedRowCellValue("SHAPENAME", string.Empty);
                //    }
                //    FrmSearch.Hide();
                //    FrmSearch.Dispose();
                //    FrmSearch = null;

                //    FindBeforeBrkRap();
                //}
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    AllParameterKeyPress(GrdDetBefore, "COLOR", "COLOR_ID", "COLORCODE", "COLORNAME", e.KeyChar.ToString());
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    AllParameterKeyPress(GrdDetBefore, "CLARITY", "CLARITY_ID", "CLARITYCODE", "CLARITYNAME", e.KeyChar.ToString());
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    AllParameterKeyPress(GrdDetBefore, "CUT", "CUT_ID", "CUTCODE", "CUTNAME", e.KeyChar.ToString());
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFPol_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    AllParameterKeyPress(GrdDetBefore, "POLISH", "POL_ID", "POLCODE", "POLNAME", e.KeyChar.ToString());
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFSym_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    AllParameterKeyPress(GrdDetBefore, "SYMMETRY", "SYM_ID", "SYMCODE", "SYMNAME", e.KeyChar.ToString());
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    AllParameterKeyPress(GrdDetBefore, "FLUORESCENCE", "FL_ID", "FLCODE", "FLNAME", e.KeyChar.ToString());
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFColor_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void repTxtAFClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDetBefore.GetFocusedDataRow();
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
                        GrdDetBefore.SetFocusedRowCellValue("AFCLARITY_ID", Val.ToString(FrmSearch.mDRow["CLARITY_ID"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFCLARITYCODE", Val.ToString(FrmSearch.mDRow["CLARITYCODE"]));
                    }
                    else
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFCLARITY_ID", 0);
                        GrdDetBefore.SetFocusedRowCellValue("AFCLARITYNAME", string.Empty);
                        GrdDetBefore.SetFocusedRowCellValue("AFCLARITYCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDetBefore.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CUTCODE,CUTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CUT);
                    FrmSearch.mColumnsToHide = "CUT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFCUT_ID", Val.ToString(FrmSearch.mDRow["CUT_ID"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFCUTNAME", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFCUTCODE", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                    }
                    else
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFCUT_ID", 0);
                        GrdDetBefore.SetFocusedRowCellValue("AFCUTNAME", string.Empty);
                        GrdDetBefore.SetFocusedRowCellValue("AFCUTCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFPol_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDetBefore.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "POLCODE,POLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_POL);
                    FrmSearch.mColumnsToHide = "POL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFPOL_ID", Val.ToString(FrmSearch.mDRow["POL_ID"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFPOLNAME", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFPOLCODE", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                    }
                    else
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFPOL_ID", 0);
                        GrdDetBefore.SetFocusedRowCellValue("AFPOLNAME", string.Empty);
                        GrdDetBefore.SetFocusedRowCellValue("AFPOLCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFSym_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDetBefore.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SYMCODE,SYMNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SYM);
                    FrmSearch.mColumnsToHide = "SYM_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFSYM_ID", Val.ToString(FrmSearch.mDRow["SYM_ID"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFSYMNAME", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFSYMCODE", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                    }
                    else
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFSYM_ID", 0);
                        GrdDetBefore.SetFocusedRowCellValue("AFSYMNAME", string.Empty);
                        GrdDetBefore.SetFocusedRowCellValue("AFSYMCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDetBefore.GetFocusedDataRow();
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
                        GrdDetBefore.SetFocusedRowCellValue("EMPLOYEE_ID", Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]));
                        GrdDetBefore.SetFocusedRowCellValue("EMPLOYEECODE", Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]));
                    }
                    else
                    {
                        GrdDetBefore.SetFocusedRowCellValue("EMPLOYEE_ID", 0);
                        GrdDetBefore.SetFocusedRowCellValue("EMPLOYEECODE", string.Empty);
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

        public void FindBeforeBrkRap()
        {
            try
            {
                GrdDetBefore.PostEditor();

                DataRow DRow = GrdDetBefore.GetFocusedDataRow();

                Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

                if (Val.ToString(DRow["BFSHAPECODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["BFCOLORCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["BFCLARITYCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["BFCUTCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["BFPOLCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["BFSYMCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["BFFLCODE"]).Trim().Equals(string.Empty)
                  )
                    return;

                this.Cursor = Cursors.WaitCursor;

                clsFindRap.SHAPECODE = Val.ToString(DRow["BFSHAPECODE"]);

                clsFindRap.COLOR_ID = Val.ToInt32(DRow["BFCOLOR_ID"]);
                clsFindRap.COLORCODE = Val.ToString(DRow["BFCOLORCODE"]);

                clsFindRap.CLARITY_ID = Val.ToInt32(DRow["BFCLARITY_ID"]);
                clsFindRap.CLARITYCODE = Val.ToString(DRow["BFCLARITYCODE"]);

                clsFindRap.CARAT = Val.Val(DRow["BFCARAT"]);
                clsFindRap.CUTCODE = Val.ToString(DRow["BFCUTCODE"]);
                clsFindRap.POLCODE = Val.ToString(DRow["BFPOLCODE"]);
                clsFindRap.SYMCODE = Val.ToString(DRow["BFSYMCODE"]);

                //{
                //    clsFindRap.GCARAT = Val.Val(DRow["GCARAT"]);
                //    clsFindRap.GCUTCODE = Val.ToString(DRow["GCUTCODE"]);
                //    clsFindRap.GPOLCODE = Val.ToString(DRow["GPOLCODE"]);
                //    clsFindRap.GSYMCODE = Val.ToString(DRow["GSYMCODE"]);
                //}

                clsFindRap.FLCODE = Val.ToString(DRow["BFFLCODE"]);
                clsFindRap.RAPDATE = Val.SqlDate(DRow["BFRAPDATE"].ToString()); //DateTime.Parse(DRow["BFRAPDATE"].ToString()).ToString("dd-MM-yyyy");


                clsFindRap = new BOFindRap().FindRapWithUpDown(clsFindRap);

                //GrdDet.SetFocusedRowCellValue("BFRAPAPORT", clsFindRap.RAPAPORT);
                //GrdDet.SetFocusedRowCellValue("BFPRICEPERCARAT", clsFindRap.PRICEPERCARAT);
                //GrdDet.SetFocusedRowCellValue("BFAMOUNT", Math.Round(clsFindRap.AMOUNT, 0));
                //GrdDet.SetFocusedRowCellValue("BFDISCOUNT", clsFindRap.DISCOUNT);

                DRow["BFRAPAPORT"] = clsFindRap.RAPAPORT;
                DRow["BFPRICEPERCARAT"] = clsFindRap.PRICEPERCARAT;
                DRow["BFAMOUNT"] = Math.Round(clsFindRap.AMOUNT, 0);
                DRow["BFDISCOUNT"] = clsFindRap.DISCOUNT;
                GrdDetBefore.RefreshData();
                clsFindRap = null;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }

        }
        public void FindAfterBrkRap()
        {
            try
            {
                GrdDetAfter.PostEditor();
                DataRow DRow = GrdDetAfter.GetFocusedDataRow();

                if (Val.ToString(DRow["SHAPECODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["COLORCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["CLARITYCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["CUTCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["POLCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["SYMCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["FLCODE"]).Trim().Equals(string.Empty)
                  )
                    return;

                this.Cursor = Cursors.WaitCursor;
                Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

                clsFindRap.SHAPECODE = Val.ToString(DRow["SHAPECODE"]);

                clsFindRap.COLOR_ID = Val.ToInt32(DRow["COLOR_ID"]);
                clsFindRap.COLORCODE = Val.ToString(DRow["COLORCODE"]);

                clsFindRap.CLARITY_ID = Val.ToInt32(DRow["CLARITY_ID"]);
                clsFindRap.CLARITYCODE = Val.ToString(DRow["CLARITYCODE"]);
                clsFindRap.CLARITYNAME = Val.ToString(DRow["CLARITYNAME"]);

                clsFindRap.CARAT = Val.Val(DRow["CARAT"]);
                clsFindRap.CUTCODE = Val.ToString(DRow["CUTCODE"]);
                clsFindRap.POLCODE = Val.ToString(DRow["POLCODE"]);
                clsFindRap.SYMCODE = Val.ToString(DRow["SYMCODE"]);

                //{
                //    clsFindRap.GCARAT = Val.Val(DRow["GCARAT"]);
                //    clsFindRap.GCUTCODE = Val.ToString(DRow["GCUTCODE"]);
                //    clsFindRap.GPOLCODE = Val.ToString(DRow["GPOLCODE"]);
                //    clsFindRap.GSYMCODE = Val.ToString(DRow["GSYMCODE"]);
                //}

                clsFindRap.FLCODE = Val.ToString(DRow["AFFLCODE"]);
                clsFindRap.RAPDATE = Val.SqlDate(Val.ToString(DRow["AFRAPDATE"]));

                clsFindRap = new BOFindRap().FindRapWithUpDown(clsFindRap);

                GrdDetBefore.SetFocusedRowCellValue("AFRAPAPORT", clsFindRap.RAPAPORT);
                GrdDetBefore.SetFocusedRowCellValue("AFPRICEPERCARAT", clsFindRap.PRICEPERCARAT);
                GrdDetBefore.SetFocusedRowCellValue("AFAMOUNT", Math.Round(clsFindRap.AMOUNT, 0));
                GrdDetBefore.SetFocusedRowCellValue("AFDISCOUNT", clsFindRap.DISCOUNT);

                clsFindRap = null;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }

        }

        private void repTxtLabourRate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                DataRow Drow = GrdDetBefore.GetFocusedDataRow();
                Drow["LABOURAMOUNT"] = Math.Round(Val.Val(GrdDetBefore.EditingValue) * Val.Val(Drow["ISSUECARAT"]), 0);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void MainGrid_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == info.BandsInfo.BandCount - 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            }
        }



        private void repTxtAFFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDetBefore.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "FLCODE,FLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_FL);
                    FrmSearch.mColumnsToHide = "FL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFFL_ID", Val.ToString(FrmSearch.mDRow["FL_ID"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFFLNAME", Val.ToString(FrmSearch.mDRow["FLNAME"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFFLCODE", Val.ToString(FrmSearch.mDRow["FLCODE"]));
                    }
                    else
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFFL_ID", 0);
                        GrdDetBefore.SetFocusedRowCellValue("AFFLNAME", string.Empty);
                        GrdDetBefore.SetFocusedRowCellValue("AFFLCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDetBefore.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SHAPECODE,SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
                    FrmSearch.mColumnsToHide = "SHAPE_ID,IMAGE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFSHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFSHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));
                        GrdDetBefore.SetFocusedRowCellValue("AFSHAPECODE", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));
                    }
                    else
                    {
                        GrdDetBefore.SetFocusedRowCellValue("AFSHAPE_ID", 0);
                        GrdDetBefore.SetFocusedRowCellValue("AFSHAPENAME", string.Empty);
                        GrdDetBefore.SetFocusedRowCellValue("AFSHAPECODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
            if (Val.ToString(txtPassForDisplayBack.Tag) != "" && Val.ToString(txtPassForDisplayBack.Tag).ToUpper() == txtPassForDisplayBack.Text.ToUpper())
            {
                CmbRapDate.Enabled = true;

                GrdDetBefore.Columns["GIANONGIA"].Visible = true;
                GrdDetBefore.Columns["DISCOUNT"].Visible = true;
                GrdDetBefore.Columns["AMOUNTDISCOUNT"].Visible = true;
                GrdDetBefore.Columns["RAPAPORT"].Visible = true;
                GrdDetBefore.Columns["GDISCOUNT"].Visible = true;
                GrdDetBefore.Columns["GAMOUNTDISCOUNT"].Visible = true;
                GrdDetBefore.Columns["GRAPAPORT"].Visible = true;


                GrdDetAfter.Columns["GIANONGIA"].Visible = true;
                GrdDetAfter.Columns["DISCOUNT"].Visible = true;
                GrdDetAfter.Columns["AMOUNTDISCOUNT"].Visible = true;
                GrdDetAfter.Columns["RAPAPORT"].Visible = true;
                GrdDetAfter.Columns["GDISCOUNT"].Visible = true;
                GrdDetAfter.Columns["GAMOUNTDISCOUNT"].Visible = true;
                GrdDetAfter.Columns["GRAPAPORT"].Visible = true;

            }
            else
            {
                GrdDetBefore.Columns["GIANONGIA"].Visible = false;
                GrdDetBefore.Columns["DISCOUNT"].Visible = false;
                GrdDetBefore.Columns["AMOUNTDISCOUNT"].Visible = false;
                GrdDetBefore.Columns["RAPAPORT"].Visible = false;
                GrdDetBefore.Columns["GDISCOUNT"].Visible = false;
                GrdDetBefore.Columns["GAMOUNTDISCOUNT"].Visible = false;
                GrdDetBefore.Columns["GRAPAPORT"].Visible = false;

                GrdDetAfter.Columns["GIANONGIA"].Visible = false;
                GrdDetAfter.Columns["DISCOUNT"].Visible = false;
                GrdDetAfter.Columns["AMOUNTDISCOUNT"].Visible = false;
                GrdDetAfter.Columns["RAPAPORT"].Visible = false;
                GrdDetAfter.Columns["GDISCOUNT"].Visible = false;
                GrdDetAfter.Columns["GAMOUNTDISCOUNT"].Visible = false;
                GrdDetAfter.Columns["GRAPAPORT"].Visible = false;
            }

        }

        private void repCmbAFRapDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                repTxtAfterCarat_Validating(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtBeforeCarat_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                DataRow dr = GrdDetBefore.GetFocusedDataRow();
                dr["BFCARAT"] = Val.Val(GrdDetBefore.EditingValue);

                GrdDetBefore.RefreshData();
                FindBeforeBrkRap();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtAfterCarat_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                DataRow dr = GrdDetBefore.GetFocusedDataRow();
                dr["AFCARAT"] = Val.Val(GrdDetBefore.EditingValue);

                GrdDetBefore.RefreshData();
                FindAfterBrkRap();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void CmbBreakingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string Name = Val.ToString(CmbBreakingType.SelectedItem);
                DataRow[] D = DtabBreakingType.Select("BREAKINGTYPECODE ='" + Name + "' ");

                if (D.Length != 0)
                {
                    DataRow DRow = D[0];
                    CmbBreakingType.Tag = Val.ToString(DRow["BREAKINGTYPE_ID"]);
                    DRow = null;
                }
                //D = null;

                if (Val.ToInt(CmbBreakingType.Tag) == 5208 || Val.ToInt(CmbBreakingType.Tag) == 4969 || Val.ToInt(CmbBreakingType.Tag) == 592) //LS-B//4P-RG//MFG
                {
                    txtKapanName.Enabled = true;
                    txtPacketNo.Enabled = true;
                    txtTag.Enabled = true;
                }
                else if (Val.ToInt(CmbBreakingType.Tag) == 590 || Val.ToInt(CmbBreakingType.Tag) == 591|| Val.ToInt(CmbBreakingType.Tag) == 631 || Val.ToInt(CmbBreakingType.Tag) == 588 || Val.ToInt(CmbBreakingType.Tag) == 4970||Val.ToInt(CmbBreakingType.Tag) == 589 ) // For BLK/CONIC/MFG/MFGMISTAKE// CLV MK // 4P-DN // 
                {
                    txtKapanName.Enabled = true;
                    txtPacketNo.Enabled = true;
                    txtTag.Enabled = false;
                }
                else
                {
                    txtKapanName.Enabled = false;
                    txtPacketNo.Enabled = false;
                    txtTag.Enabled = false;
                }
                Clear();

                /*
                if (Val.ToInt(CmbBreakingType.Tag) == 590 || Val.ToInt(CmbBreakingType.Tag) == 592 || Val.ToInt(CmbBreakingType.Tag) == 631)
                {
                    lblBeforeBlankRow.Enabled = false;
                    lblBeforeCopyRow.Enabled = false;

                    lblAfterBlankRow.Enabled = false;
                    lblAfterCopyRow.Enabled = false;
                }
                else
                {
                    lblBeforeBlankRow.Enabled = true;
                    lblBeforeCopyRow.Enabled = true;

                    lblAfterBlankRow.Enabled = true;
                    lblAfterCopyRow.Enabled = true;
                }
                  */


                //txtKapanName.Enabled = false;
                //txtPacketNo.Enabled = false;
                //txtTag.Enabled = false;

                //txtManager.Enabled = false;
                //BtnManager.Enabled = false;

                //RbtGraph.Enabled = false;
                //RbtExp.Enabled = false;

                //if (D.Length != 0)
                //{
                //    DataRow DRow = D[0];
                //    CmbBreakingTypeOld.Tag = Val.ToString(DRow["BREAKINGTYPE_ID"]);

                //    txtKapanName.Enabled = Val.ToBoolean(DRow["ISKapan"]);
                //    txtPacketNo.Enabled = Val.ToBoolean(DRow["ISPacketNo"]);
                //    txtTag.Enabled = Val.ToBoolean(DRow["ISTag"]);
                //    txtManager.Enabled = Val.ToBoolean(DRow["ISManager"]);
                //    BtnManager.Enabled = Val.ToBoolean(DRow["ISManager"]);

                //    RbtGraph.Enabled = Val.ToBoolean(DRow["ISGraph"]);
                //    RbtExp.Enabled = Val.ToBoolean(DRow["ISExp"]);

                //    DRow = null;
                //}

                //D = null;

                //txtEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;
                //BtnEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;


                //if (Name == "AP")
                //{
                //    txtEmployee.Enabled = true;
                //    BtnEmployee.Enabled = true;
                //}
                //if (Val.ToInt(CmbPrdType.Tag) == 8 || Val.ToInt(CmbPrdType.Tag) == 9 || Val.ToInt(CmbPrdType.Tag) == 11)
                //{
                //    lblLabProcess.Visible = true;
                //    CmbLabProcess.Visible = true;

                //    lblLabSelection.Visible = true;
                //    CmbLabSelection.Visible = true;
                //}
                //else
                //{
                //    lblLabProcess.Visible = false;
                //    CmbLabProcess.Visible = false;

                //    lblLabSelection.Visible = false;
                //    CmbLabSelection.Visible = false;
                //}

                //BtnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
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
                    FrmSearch.mDTab = ObjRap.GetKapan();
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
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void txtPacketNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PACKETNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjRap.GetPacketNo(txtKapanName.Text);
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPacketNo.Text = Val.ToString(FrmSearch.mDRow["PACKETNO"]);
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

        private void txtTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SRNO,TAG";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjRap.GetTag(txtKapanName.Text, Val.ToInt(txtPacketNo.Text));
                    FrmSearch.mColumnsToHide = "KAPAN_ID,PACKET_ID,EMPLOYEE_ID,KAPANNAME,PACKETNO,LOTPCS,BALANCEPCS";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtTag.Text = Val.ToString(FrmSearch.mDRow["TAG"]);
                        txtTag.Tag = Val.ToString(FrmSearch.mDRow["PACKET_ID"]);
                        txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);

                        //if (txtEmployee.Enabled == true)
                        //{
                        //    txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                        //    txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        //}
                            txtBreakingEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                            txtBreakingEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
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

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (txtEmployee.Enabled == false)
                {
                    return;
                }
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
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void lblAfterBlankRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainGrdAfter.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                if (GrdDetAfter.RowCount == 0)
                {
                    InsertFreshPlan(MainGrdAfter, DTabAfterData, lblAfterCurrentPlan);
                }
                else
                {
                    int TagSrNo = Val.ToInt32(DTabAfterData.Compute("MAX(TAGSRNO)", " PlanNo='" + lblAfterCurrentPlan.Text + "'"));
                    TagSrNo = TagSrNo + 1;

                    DataRow DRow = DTabAfterData.NewRow();
                    DRow["PLANNO"] = lblAfterCurrentPlan.Text;
                    DRow["TAGSRNO"] = TagSrNo;
                    DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                    DRow["TAG"] = Char.ConvertFromUtf32(64 + Val.ToInt(DRow["TAGSRNO"]));
                    DRow["ID"] = "";
                    DRow["PRD_ID"] = "";

                    DTabAfterData.Rows.Add(DRow);

                    GrdDetAfter.ExpandAllGroups();
                    GrdDetAfter.RefreshData();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }

        public void InsertFreshPlan(DevExpress.XtraGrid.GridControl MainGrd, DataTable Dtab, Label lblCurrPlan)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView GrdDetBefore = new GridView(MainGrd);


                if (MainGrd.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                int PlanNo = Val.ToInt32(Dtab.Compute("MAX(PlanNo)", string.Empty));
                PlanNo = PlanNo + 1;

                lblCurrPlan.Text = PlanNo.ToString();

                if (txtTag.Enabled == false)
                {
                    DataRow DRow = Dtab.NewRow();
                    DRow["PLANNO"] = PlanNo;
                    DRow["TAGSRNO"] = 1;
                    DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                    DRow["TAG"] = Char.ConvertFromUtf32(64 + Val.ToInt(DRow["TAGSRNO"]));
                    DRow["ID"] = "";
                    DRow["PRD_ID"] = "";
                    Dtab.Rows.Add(DRow);
                }

                else if (txtTag.Enabled == true && txtTag.Text.Trim().Length != 0)
                {
                    DataRow DRow = Dtab.NewRow();
                    DRow["PLANNO"] = PlanNo;
                    DRow["TAGSRNO"] = -1;
                    DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                    DRow["TAG"] = txtTag.Text;
                    DRow["ID"] = "";
                    DRow["PRD_ID"] = "";
                    Dtab.Rows.Add(DRow);
                }
                else if (txtTag.Enabled == true && txtTag.Text.Trim().Length == 0)
                {
                    DataRow DRow = Dtab.NewRow();
                    DRow["PLANNO"] = PlanNo;
                    //DRow["TAGSRNO"] = -1;
                    DRow["TAGSRNO"] = 1;
                    DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                    DRow["TAG"] = "";
                    DRow["ID"] = "";
                    DRow["PRD_ID"] = "";
                    Dtab.Rows.Add(DRow);

                }

                GrdDetBefore.ExpandAllGroups();
                GrdDetBefore.RefreshData();
                //GrdDet.FocusedRowHandle = GrdDet.RowCount;
                GrdDetBefore.MoveLast();
                //GrdDet.SelectRow(GrdDet.FocusedRowHandle);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.ToString());
            }
        }

        private void CmbRapDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int IntI = 0; IntI < GrdDetBefore.RowCount; IntI++)
            {
                GrdDetBefore.SetRowCellValue(IntI, "RAPDATE", Val.ToString(CmbRapDate.SelectedItem));
                GrdDetBefore.FocusedRowHandle = IntI;
                FindRap(GrdDetBefore);
            }
            for (int IntI = 0; IntI < GrdDetAfter.RowCount; IntI++)
            {
                GrdDetAfter.SetRowCellValue(IntI, "RAPDATE", Val.ToString(CmbRapDate.SelectedItem));
                GrdDetAfter.FocusedRowHandle = IntI;
                FindRap(GrdDetAfter);
            }
            DTabBeforeData.AcceptChanges();
            DTabAfterData.AcceptChanges();
        }

        private void lblAfterCopyRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainGrdAfter.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                if (GrdDetAfter.FocusedRowHandle < 0)
                {
                    Global.MessageError("Kindly Select One Row For Copy");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DataRow DRCopy = GrdDetAfter.GetDataRow(GrdDetAfter.FocusedRowHandle);

                int TagSrNo = Val.ToInt32(DTabAfterData.Compute("MAX(TAGSRNO)", " PlanNo='" + lblAfterCurrentPlan.Text + "'"));
                TagSrNo = TagSrNo + 1;

                DataRow DRow = DTabAfterData.NewRow();
                DRow["PLANNO"] = lblAfterCurrentPlan.Text;
                DRow["TAGSRNO"] = TagSrNo;
                DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                DRow["TAG"] = Char.ConvertFromUtf32(64 + Val.ToInt(DRow["TAGSRNO"]));
                DRow["ID"] = "";
                DRow["PRD_ID"] = "";
                foreach (DataColumn Col in DRCopy.Table.Columns)
                {
                    if (Col.ColumnName.ToString().ToUpper() == "PLANNO" ||
                        Col.ColumnName.ToString().ToUpper() == "TAGSRNO" ||
                        Col.ColumnName.ToString().ToUpper() == "TAG" ||
                        Col.ColumnName.ToString().ToUpper() == "RAPDATE" ||
                        Col.ColumnName.ToString().ToUpper() == "ID" ||
                        Col.ColumnName.ToString().ToUpper() == "PRD_ID"
                        )
                    {
                        continue;
                    }

                    if (DTabAfterData.Columns.Contains(Col.ColumnName))
                    {
                        try
                        {
                            DRow[Col.ColumnName] = DRCopy[Col.ColumnName];
                        }
                        catch
                        {

                        }
                    }
                }
                DTabAfterData.Rows.Add(DRow);

                GrdDetAfter.ExpandAllGroups();
                GrdDetAfter.RefreshData();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.ToString());
            }
        }

        private void lblBeforeBlankRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainGrdBefore.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                if (GrdDetBefore.RowCount == 0)
                {
                    InsertFreshPlan(MainGrdBefore, DTabBeforeData, lblBeforeCurrentPlan);
                }
                else
                {
                    int TagSrNo = Val.ToInt32(DTabBeforeData.Compute("MAX(TAGSRNO)", " PlanNo='" + lblBeforeCurrentPlan.Text + "'"));
                    TagSrNo = TagSrNo + 1;

                    DataRow DRow = DTabBeforeData.NewRow();
                    DRow["PLANNO"] = lblBeforeCurrentPlan.Text;
                    DRow["TAGSRNO"] = TagSrNo;
                    DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                    DRow["TAG"] = Char.ConvertFromUtf32(64 + Val.ToInt(DRow["TAGSRNO"]));
                    DRow["ID"] = "";
                    DRow["PRD_ID"] = "";

                    DTabBeforeData.Rows.Add(DRow);

                    GrdDetBefore.ExpandAllGroups();
                    GrdDetBefore.RefreshData();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }

        private void lblBeforeCopyRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainGrdBefore.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                if (GrdDetBefore.FocusedRowHandle < 0)
                {
                    Global.MessageError("Kindly Select One Row For Copy");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DataRow DRCopy = GrdDetBefore.GetDataRow(GrdDetBefore.FocusedRowHandle);

                int TagSrNo = Val.ToInt32(DTabBeforeData.Compute("MAX(TAGSRNO)", " PlanNo='" + lblBeforeCurrentPlan.Text + "'"));
                TagSrNo = TagSrNo + 1;

                DataRow DRow = DTabBeforeData.NewRow();
                DRow["PLANNO"] = lblBeforeCurrentPlan.Text;
                DRow["TAGSRNO"] = TagSrNo;
                DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                DRow["TAG"] = Char.ConvertFromUtf32(64 + Val.ToInt(DRow["TAGSRNO"]));
                DRow["ID"] = "";
                DRow["PRD_ID"] = "";
                foreach (DataColumn Col in DRCopy.Table.Columns)
                {
                    if (Col.ColumnName.ToString().ToUpper() == "PLANNO" ||
                        Col.ColumnName.ToString().ToUpper() == "TAGSRNO" ||
                        Col.ColumnName.ToString().ToUpper() == "TAG" ||
                        Col.ColumnName.ToString().ToUpper() == "RAPDATE" ||
                        Col.ColumnName.ToString().ToUpper() == "ID" ||
                        Col.ColumnName.ToString().ToUpper() == "PRD_ID"
                        )
                    {
                        continue;
                    }

                    if (DTabBeforeData.Columns.Contains(Col.ColumnName))
                    {
                        try
                        {
                            DRow[Col.ColumnName] = DRCopy[Col.ColumnName];
                        }
                        catch
                        {

                        }
                    }
                }
                DTabBeforeData.Rows.Add(DRow);
                GrdDetBefore.ExpandAllGroups();
                GrdDetBefore.RefreshData();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.ToString());
            }
        }

        private void AllParameterKeyPress(DevExpress.XtraGrid.Views.Grid.GridView GrdView, string pStrParaType, string pStrID, string pStrCode, string pStrName, string pStrSearchText)
        {
            try
            {
                DataRow[] UDRow = DTabParameter.Select("ParaType = '" + pStrParaType + "'");
                if (UDRow.Length == 0)
                {
                    return;
                }
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "ParaCode,ParaName";
                FrmSearch.mSearchText = pStrSearchText.ToString();
                this.Cursor = Cursors.WaitCursor;
                FrmSearch.mDTab = UDRow.CopyToDataTable();
                FrmSearch.mColumnsToHide = "Para_ID,ParaType";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                //e.Handled = true;
                if (FrmSearch.mDRow != null)
                {
                    GrdView.SetFocusedRowCellValue(pStrID, Val.ToString(FrmSearch.mDRow["Para_ID"]));
                    GrdView.SetFocusedRowCellValue(pStrCode, Val.ToString(FrmSearch.mDRow["ParaCode"]));
                    GrdView.SetFocusedRowCellValue(pStrName, Val.ToString(FrmSearch.mDRow["ParaName"]));
                }
                else
                {
                    GrdView.SetFocusedRowCellValue(pStrID, 0);
                    GrdView.SetFocusedRowCellValue(pStrCode, "-");
                    GrdView.SetFocusedRowCellValue(pStrName, "-");
                }
                FrmSearch.Hide();
                FrmSearch.Dispose();
                FrmSearch = null;

                //FindBeforeBrkRap();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        public class DataStructure
        {
            public int PARA_ID { get; set; }
            public string PARACODE { get; set; }
            public string PARANAME { get; set; }
            public string PARATYPE { get; set; }
        }

        private void GrdDetBefore_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName.ToUpper())
                {
                    case "CARAT":
                        FindRap(GrdDetBefore);
                        GrdDetBefore.BestFitColumns();
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void GrdDetAfter_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName.ToUpper())
                {
                    case "CARAT" :
                        FindRap(GrdDetAfter);
                        GrdDetAfter.BestFitColumns();
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void GrdDetBefore_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                bool IsColumnExists = false;
                if (GrdDetBefore.FocusedRowHandle < 0)
                    return;

                string Str_ID = "", StrCode = "", StrName = "";
                switch (e.Column.FieldName.ToUpper())
                {
                    case "SHAPECODE":
                        Str_ID = "SHAPE_ID";
                        StrCode = "SHAPECODE";
                        StrName = "SHAPENAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "SHAPE", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "COLORNAME":
                        Str_ID = "COLOR_ID";
                        StrCode = "COLORCODE";
                        StrName = "COLORNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "COLOR", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "CLARITYNAME":
                        Str_ID = "CLARITY_ID";
                        StrCode = "CLARITYCODE";
                        StrName = "CLARITYNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "CLARITY", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "CUTCODE":
                        Str_ID = "CUT_ID";
                        StrCode = "CUTCODE";
                        StrName = "CUTNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "CUT", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "POLCODE":
                        Str_ID = "POL_ID";
                        StrCode = "POLCODE";
                        StrName = "POLNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "POLISH", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "SYMCODE":
                        Str_ID = "SYM_ID";
                        StrCode = "SYMCODE";
                        StrName = "SYMNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "SYMMETRY", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "FLNAME":
                        Str_ID = "FL_ID";
                        StrCode = "FLCODE";
                        StrName = "FLNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "FLUORESCENCE", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "COLORSHADECODE":
                        Str_ID = "COLORSHADE_ID";
                        StrCode = "COLORSHADECODE";
                        StrName = "COLORSHADENAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "COLORSHADE", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "MILKYNAME":
                        Str_ID = "MILKY_ID";
                        StrCode = "MILKYCODE";
                        StrName = "MILKYNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "MILKY", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "LBLCNAME":
                        Str_ID = "LBLC_ID";
                        StrCode = "LBLCCODE";
                        StrName = "LBLCNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "LBLC", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "NATTSNAME":
                        Str_ID = "NATTS_ID";
                        StrCode = "NATTSCODE";
                        StrName = "NATTSNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "NATTS", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "TENSIONNAME":
                        Str_ID = "TENSION_ID";
                        StrCode = "TENSIONCODE";
                        StrName = "TENSIONNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "TENSION", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "BLACKINCCODE":
                        Str_ID = "BLACKINC_ID";
                        StrCode = "BLACKINCCODE";
                        StrName = "BLACKINCNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "BLACK", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "OPENINCCODE":
                        Str_ID = "OPENINC_ID";
                        StrCode = "OPENINCCODE";
                        StrName = "OPENINCNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "OPEN", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "WHITEINCCODE":
                        Str_ID = "WHITEINC_ID";
                        StrCode = "WHITEINCCODE";
                        StrName = "WHITEINCNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "WHITE", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "HACODE":
                        Str_ID = "HA_ID";
                        StrCode = "HACODE";
                        StrName = "HANAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "HEARTANDARROW", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "PAVCODE":
                        Str_ID = "PAV_ID";
                        StrCode = "PAVCODE";
                        StrName = "PAVNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "PAVALION", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "EYECLEANCODE":
                        Str_ID = "EYECLEAN_ID";
                        StrCode = "EYECLEANCODE";
                        StrName = "EYECLEANNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "EYECLEAN", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "NATURALNAME":
                        Str_ID = "NATURAL_ID";
                        StrCode = "NATURALCODE";
                        StrName = "NATURALNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "NATURAL", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "LUSTERCODE":
                        Str_ID = "LUSTER_ID";
                        StrCode = "LUSTERCODE";
                        StrName = "LUSTERNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "LUSTER", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "GRAINCODE":
                        Str_ID = "GRAIN_ID";
                        StrCode = "GRAINCODE";
                        StrName = "GRAINNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "GRAIN", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "LABNAME":
                        Str_ID = "LAB_ID";
                        StrCode = "LABCODE";
                        StrName = "LABNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "LAB", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "GCUTCODE":
                        Str_ID = "GCUT_ID";
                        StrCode = "GCUTCODE";
                        StrName = "GCUTNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "CUT", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "GPOLCODE":
                        Str_ID = "GPOL_ID";
                        StrCode = "GPOLCODE";
                        StrName = "GPOLNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "POLISH", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "GSYMCODE":
                        Str_ID = "GSYM_ID";
                        StrCode = "GSYMCODE";
                        StrName = "GSYMNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetBefore.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetBefore.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetBefore.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetBefore, "SYMMETRY", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;
                }

                if (IsColumnExists)
                {
                    FindRap(GrdDetBefore);
                    GrdDetBefore.BestFitColumns();
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        private void GrdDetAfter_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (GrdDetAfter.FocusedRowHandle < 0)
                    return;

                bool IsColumnExists = false;

                string Str_ID = "", StrCode = "", StrName = "";
                switch (e.Column.FieldName.ToUpper())
                {
                    case "SHAPECODE":
                        Str_ID = "SHAPE_ID";
                        StrCode = "SHAPECODE";
                        StrName = "SHAPENAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "SHAPE", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "COLORNAME":
                        Str_ID = "COLOR_ID";
                        StrCode = "COLORCODE";
                        StrName = "COLORNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "COLOR", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "CLARITYNAME":
                        Str_ID = "CLARITY_ID";
                        StrCode = "CLARITYCODE";
                        StrName = "CLARITYNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "CLARITY", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "CUTCODE":
                        Str_ID = "CUT_ID";
                        StrCode = "CUTCODE";
                        StrName = "CUTNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "CUT", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "POLCODE":
                        Str_ID = "POL_ID";
                        StrCode = "POLCODE";
                        StrName = "POLNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "POLISH", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "SYMCODE":
                        Str_ID = "SYM_ID";
                        StrCode = "SYMCODE";
                        StrName = "SYMNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "SYMMETRY", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "FLNAME":
                        Str_ID = "FL_ID";
                        StrCode = "FLCODE";
                        StrName = "FLNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "FLUORESCENCE", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "COLORSHADECODE":
                        Str_ID = "COLORSHADE_ID";
                        StrCode = "COLORSHADECODE";
                        StrName = "COLORSHADENAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "COLORSHADE", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "MILKYNAME":
                        Str_ID = "MILKY_ID";
                        StrCode = "MILKYCODE";
                        StrName = "MILKYNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "MILKY", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "LBLCNAME":
                        Str_ID = "LBLC_ID";
                        StrCode = "LBLCCODE";
                        StrName = "LBLCNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "LBLC", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "NATTSNAME":
                        Str_ID = "NATTS_ID";
                        StrCode = "NATTSCODE";
                        StrName = "NATTSNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            return;
                        }
                        AllParameterKeyPress(GrdDetAfter, "NATTS", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "TENSIONNAME":
                        Str_ID = "TENSION_ID";
                        StrCode = "TENSIONCODE";
                        StrName = "TENSIONNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "TENSION", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "BLACKINCCODE":
                        Str_ID = "BLACKINC_ID";
                        StrCode = "BLACKINCCODE";
                        StrName = "BLACKINCNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "BLACK", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "OPENINCCODE":
                        Str_ID = "OPENINC_ID";
                        StrCode = "OPENINCCODE";
                        StrName = "OPENINCNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "OPEN", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "WHITEINCCODE":
                        Str_ID = "WHITEINC_ID";
                        StrCode = "WHITEINCCODE";
                        StrName = "WHITEINCNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "WHITE", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "HACODE":
                        Str_ID = "HA_ID";
                        StrCode = "HACODE";
                        StrName = "HANAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "HEARTANDARROW", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "PAVCODE":
                        Str_ID = "PAV_ID";
                        StrCode = "PAVCODE";
                        StrName = "PAVNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "PAVALION", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "EYECLEANCODE":
                        Str_ID = "EYECLEAN_ID";
                        StrCode = "EYECLEANCODE";
                        StrName = "EYECLEANNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "EYECLEAN", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "NATURALNAME":
                        Str_ID = "NATURAL_ID";
                        StrCode = "NATURALCODE";
                        StrName = "NATURALNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "NATURAL", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "LUSTERCODE":
                        Str_ID = "LUSTER_ID";
                        StrCode = "LUSTERCODE";
                        StrName = "LUSTERNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "LUSTER", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "GRAINCODE":
                        Str_ID = "GRAIN_ID";
                        StrCode = "GRAINCODE";
                        StrName = "GRAINNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "GRAIN", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "LABNAME":
                        Str_ID = "LAB_ID";
                        StrCode = "LABCODE";
                        StrName = "LABNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "LAB", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "GCUTCODE":
                        Str_ID = "GCUT_ID";
                        StrCode = "GCUTCODE";
                        StrName = "GCUTNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "CUT", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "GPOLCODE":
                        Str_ID = "GPOL_ID";
                        StrCode = "GPOLCODE";
                        StrName = "GPOLNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "POLISH", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;

                    case "GSYMCODE":
                        Str_ID = "GSYM_ID";
                        StrCode = "GSYMCODE";
                        StrName = "GSYMNAME";
                        IsColumnExists = true;
                        if (Val.ToString(e.Value).Trim().Equals(string.Empty))
                        {
                            GrdDetAfter.SetFocusedRowCellValue(Str_ID, 0);
                            GrdDetAfter.SetFocusedRowCellValue(StrCode, "-");
                            GrdDetAfter.SetFocusedRowCellValue(StrName, "-");
                            break;
                        }
                        AllParameterKeyPress(GrdDetAfter, "SYMMETRY", Str_ID, StrCode, StrName, e.Value.ToString());
                        break;
                }
                if (IsColumnExists)
                {
                    FindRap(GrdDetAfter);
                    GrdDetAfter.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void FindRap(DevExpress.XtraGrid.Views.Grid.GridView GrdDet)
        {
            try
            {
                if (GrdDet.RowCount == 0)
                {
                    return;
                }
                if (GrdDet.FocusedRowHandle < 0)
                {
                    return;
                }
                //if (mStrType == "SHOWCLICK") //Add : Pinali : 11-12-2019
                //{
                //    return;
                //}

                this.Cursor = Cursors.WaitCursor;

                DataRow DRow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

                Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

                clsFindRap.SHAPE_ID = Val.ToInt32(DRow["SHAPE_ID"]);
                clsFindRap.SHAPECODE = Val.ToString(DRow["SHAPECODE"]);

                clsFindRap.COLOR_ID = Val.ToInt32(DRow["COLOR_ID"]);
                clsFindRap.COLORCODE = Val.ToString(DRow["COLORCODE"]);

                clsFindRap.CLARITY_ID = Val.ToInt32(DRow["CLARITY_ID"]);
                clsFindRap.CLARITYCODE = Val.ToString(DRow["CLARITYCODE"]) == "-" ? "" : Val.ToString(DRow["CLARITYCODE"]);

                clsFindRap.CARAT = Val.Val(DRow["CARAT"]);
                clsFindRap.CUTCODE = Val.ToString(DRow["CUTCODE"]);
                clsFindRap.POLCODE = Val.ToString(DRow["POLCODE"]);
                clsFindRap.SYMCODE = Val.ToString(DRow["SYMCODE"]);

                // if (RbtGraph.Checked == true)
                {
                    clsFindRap.GCARAT = Val.Val(DRow["GCARAT"]);
                    clsFindRap.GCUTCODE = Val.ToString(DRow["GCUTCODE"]);
                    clsFindRap.GPOLCODE = Val.ToString(DRow["GPOLCODE"]);
                    clsFindRap.GSYMCODE = Val.ToString(DRow["GSYMCODE"]);
                }

                clsFindRap.FLCODE = Val.ToString(DRow["FLCODE"]);
                clsFindRap.MILKYCODE = Val.ToString(DRow["MILKYCODE"]);
                clsFindRap.NATTSCODE = Val.ToString(DRow["NATTSCODE"]);
                clsFindRap.LBLCCODE = Val.ToString(DRow["LBLCCODE"]);
                clsFindRap.RAPDATE = Val.SqlDate(CmbRapDate.SelectedItem.ToString());

                clsFindRap.COLORSHADECODE = Val.ToString(DRow["COLORSHADECODE"]);
                clsFindRap.OPENINCCODE = Val.ToString(DRow["OPENINCCODE"]);
                clsFindRap.BLACKINCCODE = Val.ToString(DRow["BLACKINCCODE"]);
                clsFindRap.WHITEINCCODE = Val.ToString(DRow["WHITEINCCODE"]);
                clsFindRap.PAVCODE = Val.ToString(DRow["PAVCODE"]);
                clsFindRap.EYECLEANCODE = Val.ToString(DRow["EYECLEANCODE"]);
                clsFindRap.LUSTERCODE = Val.ToString(DRow["LUSTERCODE"]);
                clsFindRap.NATURALCODE = Val.ToString(DRow["NATURALCODE"]);
                clsFindRap.GRAINCODE = Val.ToString(DRow["GRAINCODE"]) == "-" ? "" : Val.ToString(DRow["GRAINCODE"]);
                clsFindRap.LABCODE = Val.ToString(DRow["LABCODE"]) == "-" ? "" : Val.ToString(DRow["LABCODE"]);

                if (clsFindRap.SHAPECODE == "" || clsFindRap.COLORCODE == "" || clsFindRap.CLARITYCODE == "")
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                clsFindRap = ObjRap.FindRapWithUpDown(clsFindRap);

                GrdDet.SetFocusedRowCellValue("RAPAPORT", clsFindRap.RAPAPORT);
                GrdDet.SetFocusedRowCellValue("PRICEPERCARAT", clsFindRap.PRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("AMOUNT", Math.Round(clsFindRap.AMOUNT, 0));
                GrdDet.SetFocusedRowCellValue("DISCOUNT", clsFindRap.DISCOUNT);
                GrdDet.SetFocusedRowCellValue("AMOUNTDISCOUNT", clsFindRap.AMOUNTDISCOUNT);
                GrdDet.SetFocusedRowCellValue("ISMIXRATE", clsFindRap.ISMIXRATE);
                GrdDet.SetFocusedRowCellValue("GIANONGIA", clsFindRap.GIANONGIA);

                GrdDet.SetFocusedRowCellValue("GRAPAPORT", clsFindRap.GRAPAPORT);
                GrdDet.SetFocusedRowCellValue("GPRICEPERCARAT", clsFindRap.GPRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("GAMOUNT", Math.Round(clsFindRap.GAMOUNT, 0));
                GrdDet.SetFocusedRowCellValue("GDISCOUNT", clsFindRap.GDISCOUNT);
                GrdDet.SetFocusedRowCellValue("GAMOUNTDISCOUNT", clsFindRap.GAMOUNTDISCOUNT);

                //Add : Pinali : 07-09-2019
                GrdDet.SetFocusedRowCellValue("MDISCOUNT", clsFindRap.MDISCOUNT);
                GrdDet.SetFocusedRowCellValue("MPRICEPERCARAT", clsFindRap.MPRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("MAMOUNT", clsFindRap.MAMOUNT);

                GrdDet.SetFocusedRowCellValue("MGDISCOUNT", clsFindRap.MGDISCOUNT);
                GrdDet.SetFocusedRowCellValue("MGPRICEPERCARAT", clsFindRap.MGPRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("MGAMOUNT", clsFindRap.MGAMOUNT);
                //End : Pinali : 07-09-2019

                GrdDet.SetFocusedRowCellValue("DROWDISREGULAR", clsFindRap.DRowDisRegularXML);

                clsFindRap = null;

                DTabBeforeData.AcceptChanges();
                DTabAfterData.AcceptChanges();

                //#P : 30-01-2020
                double DouTotalBeforeAmount = Val.Val(DTabBeforeData.Compute("SUM(AMOUNT)", string.Empty));
                double DouTotalAfterAmount = Val.Val(DTabAfterData.Compute("SUM(AMOUNT)", string.Empty));
                lblDifference.Text =  Val.ToString(Val.Val(DouTotalBeforeAmount) - Val.Val(DouTotalAfterAmount));

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                GrdDet.EndUpdate();
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
            this.Cursor = Cursors.Default;

        }

        private void txtBreakingEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtBreakingEmployee.Enabled == false)
                {
                    return;
                }
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
                        txtBreakingEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtBreakingEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void BtnBFDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetBefore.FocusedRowHandle < 0)
                {
                    return;
                }

                if (Global.Confirm("Are You Sure For Delete this entry ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                string StrID = Val.ToString(GrdDetBefore.GetFocusedRowCellValue("BREAKING_ID"));

                /* /// Cmnt : #P : 21-11-2020 : Coz Blank Record save nathi karavano... ek entry compulsory hovi j joiye....
                if (StrID != "")
                {
                    ////Add : Pinali : 15-09-2019
                    //DataTable DTabFurtherPrd = ObjRap.ValCheckFurtherPrdExistsOrNotForDelete(StrID, null, Val.ToInt32(GrdDetBefore.GetFocusedRowCellValue("PLANNO")), Val.ToInt32(CmbPrdType.Tag));
                    //if (DTabFurtherPrd.Rows.Count > 0)
                    //{
                    //    string commaSeparatedString = String.Join("','", DTabFurtherPrd.AsEnumerable().Select(x => x.Field<string>("FULLPACKETNO").ToString()).ToArray());

                    //    this.Cursor = Cursors.Default;
                    //    Global.MessageError("You Can't Delete. Because '" + commaSeparatedString + "' <- This Packets Further Prd Entry Is Exists Please Check.");
                    //    return;
                    //}
                    ////End : Pinali : 15-09-2019

                    TRN_SingleBreakingPacketEntry Property = new TRN_SingleBreakingPacketEntry();
                    Property.BREAKING_ID = Guid.Parse(Val.ToString(StrID));
                    Property.GROUP_ID = Guid.Empty;
                    Property = ObjBrk.Delete(Property);

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        GrdDetBefore.DeleteRow(GrdDetBefore.FocusedRowHandle);
                    }
                }
                else*/
                {
                    GrdDetBefore.DeleteRow(GrdDetBefore.FocusedRowHandle);
                }

                DTabBeforeData.AcceptChanges();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnAFDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetAfter.FocusedRowHandle < 0)
                {
                    return;
                }

                if (Global.Confirm("Are You Sure For Delete this entry ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                string StrID = Val.ToString(GrdDetAfter.GetFocusedRowCellValue("BREAKING_ID"));


                /* /// Cmnt : #P : 21-11-2020 : Coz Blank Record save nathi karavano... ek entry compulsory hovi j joiye....
                if (StrID != "")
                {
                    ////Add : Pinali : 15-09-2019
                    //DataTable DTabFurtherPrd = ObjRap.ValCheckFurtherPrdExistsOrNotForDelete(StrID, null, Val.ToInt32(GrdDetBefore.GetFocusedRowCellValue("PLANNO")), Val.ToInt32(CmbPrdType.Tag));
                    //if (DTabFurtherPrd.Rows.Count > 0)
                    //{
                    //    string commaSeparatedString = String.Join("','", DTabFurtherPrd.AsEnumerable().Select(x => x.Field<string>("FULLPACKETNO").ToString()).ToArray());

                    //    this.Cursor = Cursors.Default;
                    //    Global.MessageError("You Can't Delete. Because '" + commaSeparatedString + "' <- This Packets Further Prd Entry Is Exists Please Check.");
                    //    return;
                    //}
                    ////End : Pinali : 15-09-2019

                    TRN_SingleBreakingPacketEntry Property = new TRN_SingleBreakingPacketEntry();
                    Property.BREAKING_ID = Guid.Parse(Val.ToString(StrID));
                    Property.GROUP_ID = Guid.Empty;
                    Property = ObjBrk.Delete(Property);

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        GrdDetAfter.DeleteRow(GrdDetAfter.FocusedRowHandle);
                    }
                }
                else*/
                {
                    GrdDetAfter.DeleteRow(GrdDetAfter.FocusedRowHandle);
                }

                DTabAfterData.AcceptChanges();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure For Delete Whole Braking Entry?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (mGroup_ID != 0)
                {
                    TRN_SingleBreakingPacketEntry Property = new TRN_SingleBreakingPacketEntry();
                    Property.BREAKING_ID = 0;
                    Property.GROUP_ID = mGroup_ID;
                    Property = ObjBrk.Delete(Property);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                    }
                }
                Clear();
                DTabBeforeData.AcceptChanges();
                DTabAfterData.AcceptChanges();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void TxtReason_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtBreakingEmployee.Enabled == false)
                {
                    return;
                }

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "BREAKINGREASONNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_BREAKINGREASON);
                    FrmSearch.mColumnsToHide = "BREAKINGREASON_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtReason.Text = Val.ToString(FrmSearch.mDRow["BREAKINGREASONNAME"]);
                        TxtReason.Tag = Val.ToString(FrmSearch.mDRow["BREAKINGREASON_ID"]);
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
    }
}
