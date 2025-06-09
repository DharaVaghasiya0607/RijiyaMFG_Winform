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
using DevExpress.Data.Browsing;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmKapanCreation : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjRough = new BOTRN_RoughPurchase();

        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();

        BOTRN_PurchaseLiveStock ObjTrnLiveStock = new BOTRN_PurchaseLiveStock();
        DataTable DtabKapan = new DataTable();


        double TotKapanCarat = 0;

        public FORMTYPE mFormType = FORMTYPE.ORIGINALKAPAN;
        public enum FORMTYPE
        {
            ORIGINALKAPAN = 0,
            REPAIRING = 1,
            PCNKAPAN = 2,
            ORIGINALKAPANUPDATE = 3
        }


        #region Property Settings

        public FrmKapanCreation()
        {
            InitializeComponent();
        }

        public void ShowForm(Int64 pLotID, string pStrPartyName, string pStrPartyInvoiceNo, FORMTYPE pFormType)
        {
            AttachFormDefaultEvent();

            Val.FormGeneralSettingForPopup(this);

            mFormType = pFormType;

            if (mFormType == FORMTYPE.PCNKAPAN)
            {
                this.Text = "PCN KAPAN CREATE";
                CmbKapanCategory.SelectedItem = "PCN";
                PnlOriginalKapanDetail.Visible = false;
            }

            else if (mFormType == FORMTYPE.REPAIRING) //#P : 10-09-2020 
            {
                this.Text = "REPAIRING KAPAN CREATE";
                CmbKapanCategory.SelectedItem = "REPAIRING";
                PnlOriginalKapanDetail.Visible = false;
            }
            else if (mFormType == FORMTYPE.ORIGINALKAPANUPDATE) //#P : 09-09-2020
            {
                this.Text = "KAPAN UPDATE";
                CmbKapanCategory.SelectedItem = "ORIGINAL";
                PnlOriginalKapanDetail.Visible = false;
                PnlDetail.Visible = false;
            }
            else
            {
                this.Text = "ORIGINAL KAPAN CREATE";
                CmbKapanCategory.SelectedItem = "ORIGINAL";
                PnlOriginalKapanDetail.Visible = true;
            }
            CmbKapanCategory.Enabled = false;

            lblLotID.Text = Val.ToString(pLotID);
            lblLotID.Tag = pLotID;

            lblPartyName.Text = pStrPartyName;
            lblInvocieNo.Text = pStrPartyInvoiceNo;
            CmbKapanType.SelectedItem = 0;

            DataRow DR = ObjRough.GetOrgAndBalCarat(Val.ToInt64(lblLotID.Text));

            lblLastKapanName.Text = ObjRough.GetLastCreateKapanName();

            if (DR == null)
            {
                lblBalanceCarat.Text = "";
                lblOrgCarat.Text = "";
                lblRate.Text = "";
                
                lblOrgAmount.Text = "";
            }
            else
            {
                lblBalanceCarat.Text = Val.ToString(DR["BALANCECARAT"]);
                lblOrgCarat.Text = Val.ToString(DR["CARAT"]);
                //lblRate.Text = Val.ToString(DR["RATE"]);
                lblRate.Text = Val.ToString(DR["GROSSBROKRATE"]);
                
                lblOrgAmount.Text = Math.Round((Val.Val(DR["GROSSBROKRATE"]) * Val.Val(DR["CARAT"])), 3).ToString();
            }

            DtabKapan.Columns.Add(new DataColumn("KAPAN_ID", typeof(Int64)));
            DtabKapan.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DtabKapan.Columns.Add(new DataColumn("KAPANGROUP", typeof(string)));
            DtabKapan.Columns.Add(new DataColumn("KAPANCATEGORY", typeof(string)));  //#P : 12-10-2019
            DtabKapan.Columns.Add(new DataColumn("MANAGER_ID", typeof(Int64)));
            DtabKapan.Columns.Add(new DataColumn("MANAGERNAME", typeof(string)));
            DtabKapan.Columns.Add(new DataColumn("KAPANPCS", typeof(int)));
            DtabKapan.Columns.Add(new DataColumn("KAPANCARAT", typeof(double)));

            DtabKapan.Columns.Add(new DataColumn("KAPANRATE", typeof(double)));
            DtabKapan.Columns.Add(new DataColumn("KAPANAMOUNT", typeof(double)));

            DtabKapan.Columns.Add(new DataColumn("EXPMAKPER", typeof(double)));
            DtabKapan.Columns.Add(new DataColumn("EXPMAKCARAT", typeof(double)));
            DtabKapan.Columns.Add(new DataColumn("EXPPOLPER", typeof(double)));
            DtabKapan.Columns.Add(new DataColumn("EXPPOLCARAT", typeof(double)));
            DtabKapan.Columns.Add(new DataColumn("EXPDOLLAR", typeof(double)));
            DtabKapan.Columns.Add(new DataColumn("REMARK", typeof(string)));

            DtabKapan.Columns.Add(new DataColumn("LOTGROUP", typeof(string)));
            DtabKapan.Columns.Add(new DataColumn("DUEDAYS", typeof(Int32)));
            DtabKapan.Columns.Add(new DataColumn("EXPGIAPER", typeof(double)));
            DtabKapan.Columns.Add(new DataColumn("DIAMONDTYPE", typeof(string)));

            //DtabKapan.Rows.Add(DtabKapan.NewRow());

            string strKpType = "";
            if (mFormType == FORMTYPE.PCNKAPAN)
                strKpType = "PCN";
            else if (mFormType == FORMTYPE.REPAIRING)
                strKpType = "REPAIRING";
            else
                strKpType = "ORIGINAL";

            DataRow DrKapan = DtabKapan.NewRow();
            DrKapan["KAPANCATEGORY"] = strKpType;
            DrKapan["DIAMONDTYPE"] = "NATURAL";
            DtabKapan.Rows.Add(DrKapan);
            MainGrid.DataSource = DtabKapan;

            CmbKapanType.SelectedIndex = 0;

            GrdDet.FocusedRowHandle = 0;
            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.Focus();

            MainGrid.RefreshDataSource();

            ChkCmbComparKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            ChkCmbComparKapan.Properties.DisplayMember = "KAPANNAME";
            ChkCmbComparKapan.Properties.ValueMember = "KAPANNAME";
            ChkCmbComparKapan.Focus();
            this.ShowDialog();



        }


        public void ShowForm(Int64 StrLotID, FORMTYPE pFormType, double DouRCostWithDalaliAmt) //#P : 09-09-2020 ;' For Display Saved kapan's
        {
            AttachFormDefaultEvent();
            Val.FormGeneralSettingForPopup(this);
            mFormType = pFormType;

            lblLotID.Text = Val.ToString(StrLotID);

            if (mFormType == FORMTYPE.PCNKAPAN)
            {
                this.Text = "PCN KAPAN CREATE";
                CmbKapanCategory.SelectedItem = "PCN";
                PnlOriginalKapanDetail.Visible = false;
            }
            else if (mFormType == FORMTYPE.ORIGINALKAPANUPDATE) //#P : 09-09-2020
            {
                this.Text = "KAPAN UPDATE";
                CmbKapanCategory.SelectedItem = "ORIGINAL";
                PnlOriginalKapanDetail.Visible = false;
                PnlDetail.Visible = false;

                GrdDet.Columns["KAPANNAME"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["KAPANGROUP"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["REMARK"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["KAPANPCS"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["KAPANCARAT"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["MANAGERNAME"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["MANAGER_ID"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["EXPMAKPER"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["EXPMAKCARAT"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["EXPPOLPER"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["EXPPOLCARAT"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["EXPDOLLAR"].OptionsColumn.AllowEdit = false;
               
                GrdDet.Columns["KAPANRATE"].OptionsColumn.AllowEdit = true;
                GrdDet.Columns["KAPANAMOUNT"].OptionsColumn.AllowEdit = false;
            }
            else
            {
                this.Text = "ORIGINAL KAPAN CREATE";
                CmbKapanCategory.SelectedItem = "ORIGINAL";
                PnlOriginalKapanDetail.Visible = true;
            }

            lblOrgAmount.Text = Val.ToString(DouRCostWithDalaliAmt);

            lblLastKapanName.Text = ObjRough.GetLastCreateKapanName();

            DtabKapan = ObjKapan.GetDataForSingleKapanLiveStock("", Val.ToInt64(StrLotID));

            if (DtabKapan.Rows.Count <= 0)
                return;

            DtabKapan.DefaultView.Sort = "KAPANNAME";

            MainGrid.DataSource = DtabKapan;
            GrdDet.RefreshData();
            this.ShowDialog();
        }


        public void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            //ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);

        }

        #endregion

        #region Validation

        private bool ValSave()
        {
            int IntCol = 0, IntRow = -1;
            foreach (DataRow dr in DtabKapan.Rows)
            {
                //if (Val.ToString(dr["KAPANNAME"]).Trim().Equals(string.Empty) && Val.ToString(dr["ENTRYDATE"]).Trim().Equals(string.Empty) && Val.Val(dr["KAPANCARAT"]) <= 0 && Val.Val(dr["KAPANPCS"]) <= 0)
                //    continue;

                if (Val.ToString(dr["KAPANNAME"]).Trim().Equals(string.Empty))
                {
                    if (DtabKapan.Rows.Count == 1)
                    {
                        Global.Message("Please Enter Kapan Name");
                        IntCol = 1;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }

                if (Val.Val(dr["KAPANPCS"]) <= 0)
                {
                    Global.Message("Kapan Pcs Is Required.");
                    IntCol = 4;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }

                if (Val.Val(dr["KAPANCARAT"]) <= 0)
                {
                    Global.Message("Kapan Carat Is Required.");
                    IntCol = 5;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }

                if (Val.ToString(dr["MANAGERNAME"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Manager Name Is Required.");
                    IntCol = 3;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }

                if (Val.ToString(dr["DIAMONDTYPE"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Diamond Type Required.");
                    IntCol = 4;
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

        #endregion


        public void Clear()
        {
            DtabKapan.Rows.Clear();
            DtabKapan.Rows.Add(DtabKapan.NewRow());

            ChkNotApplyAnyLock.Checked = false;

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (mFormType == FORMTYPE.ORIGINALKAPANUPDATE) //#P : 09-09-2020 : Update KapanRate And Amount
                {
                    if (lblLotID.Text.Trim().Equals(string.Empty))
                    {
                        return;
                    }

                    DtabKapan.TableName = "Table1";
                    string KapanDetailForXML = string.Empty;

                    using (StringWriter sw = new StringWriter())
                    {
                        DtabKapan.WriteXml(sw);
                        KapanDetailForXML = sw.ToString();
                    }

                    TrnKapanCreateProperty Property = new TrnKapanCreateProperty();
                    Property.LOT_ID = Val.ToInt64(lblLotID.Text);
                    Property = ObjKapan.CheckValSaveKapanForRateAndAmount(Property, KapanDetailForXML, "EDIT MODE");

                    if (Property.ReturnMessageType == "FAIL")
                    {
                        Global.MessageError(Property.ReturnMessageDesc);
                        Property = null;
                        return;
                    }

                    string ReturnMessageDescUpdate = "";
                    string ReturnMessageTypeUpdate = "";

                    foreach (DataRow Dr in DtabKapan.Rows)
                    {
                        if (Val.ToString(Dr["KAPANNAME"]).Trim().Equals(string.Empty) || Val.Val(Dr["KAPANCARAT"]) <= 0)
                        {
                            continue;
                        }

                        TrnKapanCreateProperty PropertyRateAmt = new TrnKapanCreateProperty();
                        PropertyRateAmt.Ope = "UPDATE_RATEAMOUNT";
                        PropertyRateAmt.KAPAN_ID = Val.ToInt64(Dr["KAPAN_ID"]);
                        PropertyRateAmt.KAPANRATE = Val.Val(Dr["KAPANRATE"]);
                        PropertyRateAmt.KAPANAMOUNT = Val.Val(Dr["KAPANAMOUNT"]);
                        PropertyRateAmt = ObjKapan.EditKapan(PropertyRateAmt);

                        if (PropertyRateAmt.ReturnMessageType == "FAIL")
                        {
                            Global.Message(PropertyRateAmt.ReturnMessageDesc);
                            return;
                        }
                        ReturnMessageDescUpdate = PropertyRateAmt.ReturnMessageDesc;
                        ReturnMessageTypeUpdate = PropertyRateAmt.ReturnMessageType;
                    }

                    
                    if (ReturnMessageTypeUpdate == "SUCCESS")
                    {
                        Global.Message(ReturnMessageDescUpdate);
                        this.Close();
                    }
                    else
                    {
                        Global.MessageError(ReturnMessageDescUpdate);
                        this.Close();
                    }
                }
                else
                {
                    if (Val.Val(lblBalanceCarat.Text) == 0 && (mFormType == FORMTYPE.ORIGINALKAPAN))
                    {
                        this.Cursor = Cursors.Default;
                        Global.Message("NO BALANCE IS THERE");
                        return;
                    }

                    if (ValSave())
                    {
                        return;
                    }
                    // Kapan Create

                    if (Global.Confirm("Are You Sure You Want To Create Kapan ? ") == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    DtabKapan.AcceptChanges();
                    
                    /*
                    if (mFormType == FORMTYPE.ORIGINALKAPAN || mFormType == FORMTYPE.ORIGINALKAPANUPDATE)
                    {
                        //#P : 10-09-2020
                        DtabKapan.TableName = "Table1";
                        string KapanDetailForXML = string.Empty;
                        using (StringWriter sw = new StringWriter())
                        {
                            DtabKapan.WriteXml(sw);
                            KapanDetailForXML = sw.ToString();

                        }
                        TrnKapanCreateProperty PropertyRateAmt = new TrnKapanCreateProperty();
                        PropertyRateAmt.LOT_ID =Val.ToInt64(lblLotID.Text);
                        PropertyRateAmt = ObjKapan.CheckValSaveKapanForRateAndAmount(PropertyRateAmt, KapanDetailForXML, "ADD MODE");

                        if (PropertyRateAmt.ReturnMessageType == "FAIL")
                        {
                            Global.MessageError(PropertyRateAmt.ReturnMessageDesc);
                            PropertyRateAmt = null;
                            return;
                        }
                    }
                    //End : #P : 10-09-2020
                    */

                    this.Cursor = Cursors.WaitCursor;

                    DtabKapan.DefaultView.Sort = "KAPANNAME";
                    DtabKapan = DtabKapan.DefaultView.ToTable();

                    string ReturnMessageDesc = "";
                    string ReturnMessageType = "";

                    foreach (DataRow Dr in DtabKapan.Rows)
                    {
                        if (Val.ToString(Dr["KAPANNAME"]).Trim().Equals(string.Empty) || Val.Val(Dr["KAPANCARAT"]) <= 0)
                            continue;

                        TrnKapanCreateProperty Property = new TrnKapanCreateProperty();

                        Property.KAPAN_ID = 0;
                        Property.KAPANDATE = Val.SqlDate(DTPReceiveDate.Value.ToShortDateString());
                        Property.KAPANNAME = Val.Trim(Val.ToString(Dr["KAPANNAME"]));
                        Property.KAPANGROUP = Val.Trim(Val.ToString(Dr["KAPANGROUP"]));
                        Property.KAPANCATEGORY = Val.Trim(Val.ToString(Dr["KAPANCATEGORY"]));

                        Property.MANAGER_ID = Val.Bigint(Dr["MANAGER_ID"]);
                       
                        Property.LOT_ID = Val.ToInt64(lblLotID.Text);

                        Property.KAPANPCS = Val.ToInt32(Dr["KAPANPCS"]);
                        Property.KAPANCARAT = Val.Val(Dr["KAPANCARAT"]);

                        Property.KAPANRATE = Val.Val(Dr["KAPANRATE"]);
                        Property.KAPANAMOUNT = Val.Val(Dr["KAPANAMOUNT"]);

                        Property.EXPMAKPER = Val.Val(Dr["EXPMAKPER"]);
                        Property.EXPMAKCARAT = Val.Val(Dr["EXPMAKCARAT"]);
                        Property.EXPPOLPER = Val.Val(Dr["EXPPOLPER"]);
                        Property.EXPPOLCARAT = Val.Val(Dr["EXPPOLCARAT"]);
                        Property.EXPDOLLAR = Val.Val(Dr["EXPDOLLAR"]);

                        Property.LOTGROUP = Val.ToString(Dr["LOTGROUP"]);
                        Property.DUEDAYS = Val.ToInt32(Dr["DUEDAYS"]);
                        Property.EXPGIAPER = Val.Val(Dr["EXPGIAPER"]);

                        Property.REMARK = Val.ToString(Dr["REMARK"]);

                        Property.ISNOTAPPLYANYLOCK = Val.ToBoolean(ChkNotApplyAnyLock.Checked);

                        Property.KAPANTYPE = Val.ToString(CmbKapanType.SelectedItem);

                        Property.COMPARMEMO = Val.ToString(ChkCmbComparKapan.Properties.GetCheckedItems());
                        Property.DIAMONDTYPE = Val.ToString(Dr["DIAMONDTYPE"]);

                        Property = ObjKapan.Save(Property);

                        ReturnMessageDesc = Property.ReturnMessageDesc;
                        ReturnMessageType = Property.ReturnMessageType;
                        Property = null;
                    }
                    this.Cursor = Cursors.Default;

                    Global.Message(ReturnMessageDesc);


                    if (ReturnMessageType == "SUCCESS")
                    {
                        this.Close();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
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


        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {

                //if (e.Column.FieldName.ToUpper() == "SUBLOT1")
                //{
                //    string StrKapanName = Val.ToString(GrdDet.GetFocusedRowCellValue("KAPANNAME"));
                //    string StrSubLot = Val.ToString(GrdDet.GetFocusedRowCellValue("SUBLOT"));
                //    string StrSubLot1 = Val.ToString(GrdDet.GetFocusedRowCellValue("SUBLOT1"));

                //    //DataRow DrKapan = ObjTrnLiveStock.IsKapanNameExists(StrKapanName + StrSubLot + StrSubLot1);
                //    //if (DrKapan != null)
                //    //{
                //    //    GrdDet.SetFocusedRowCellValue("KAPAN_ID", Val.ToString(DrKapan["KAPAN_ID"]));
                //    //}
                //    //else
                //    //{
                //    //    GrdDet.SetFocusedRowCellValue("KAPAN_ID", Guid.Empty);
                //    //}
                //   // DrKapan = null;
                //}

                if (e.Column.FieldName.ToUpper() == "EXPMAKPER")
                {
                    double DouExpPer = Val.Val(GrdDet.GetFocusedRowCellValue("EXPMAKPER"));
                    double DouCarat = Val.Val(GrdDet.GetFocusedRowCellValue("KAPANCARAT"));

                    GrdDet.SetFocusedRowCellValue("EXPMAKCARAT", Math.Round((DouCarat * DouExpPer) / 100, 2));
                }
                if (e.Column.FieldName.ToUpper() == "EXPPOLPER")
                {
                    double DouExpPer = Val.Val(GrdDet.GetFocusedRowCellValue("EXPPOLPER"));
                    double DouCarat = Val.Val(GrdDet.GetFocusedRowCellValue("KAPANCARAT"));

                    GrdDet.SetFocusedRowCellValue("EXPPOLCARAT", Math.Round((DouCarat * DouExpPer) / 100, 2));
                }
                if (e.Column.FieldName.ToUpper() == "KAPANRATE" || e.Column.FieldName.ToUpper() == "KAPANCARAT")
                {

                    string StrKapanName = Val.ToString(GrdDet.GetFocusedRowCellValue("KAPANNAME"));
                    double DouKapanRate = Val.Val(GrdDet.GetFocusedRowCellValue("KAPANRATE"));
                    double DouCarat = Val.Val(GrdDet.GetFocusedRowCellValue("KAPANCARAT"));
                    GrdDet.SetFocusedRowCellValue("KAPANAMOUNT", Math.Round((DouCarat * DouKapanRate), 3));

                    //#P : 10-09-2020
                    DtabKapan.Select("KAPANNAME = '" + StrKapanName + "'")
                    .ToList<DataRow>()
                    .ForEach(r =>
                    {
                        r["KAPANRATE"] = DouKapanRate;
                        r["KAPANAMOUNT"] = DouKapanRate * Val.Val(r["KAPANCARAT"]);
                    });
                    DtabKapan.AcceptChanges();
                    //#P : 10-09-2020


                }
                if (e.Column.FieldName.ToUpper() == "KAPANNAME")
                {
                    GrdDet.PostEditor();
                    DtabKapan.AcceptChanges();
                    string StrKapanName = Val.ToString(GrdDet.GetFocusedRowCellValue("KAPANNAME"));
                    double DouKapanRate = 0;
                    //DataRow[] Dr = DtabKapan.Select("KAPANNAME = '" + StrKapanName + "'");

                    var Result = from row in DtabKapan.AsEnumerable()
                                 where Val.ToString(row["KAPANNAME"]).ToUpper() == Val.ToString(StrKapanName).ToUpper()
                                 && row.Table.Rows.IndexOf(row) != e.RowHandle
                                 select row;

                    if (Result.Any())
                    {
                        DouKapanRate = Val.Val(Result.FirstOrDefault()["KAPANRATE"]);
                    }
                    //if (Dr != null)
                    //{
                    //    DouKapanRate = Val.Val(Dr[0]["KAPANNAME"]);
                    //}

                    //#P : 10-09-2020
                    DtabKapan.Select("KAPANNAME = '" + StrKapanName + "'")
                    .ToList<DataRow>()
                    .ForEach(r =>
                    {
                        r["KAPANRATE"] = DouKapanRate;
                        r["KAPANAMOUNT"] = DouKapanRate * Val.Val(r["KAPANCARAT"]);
                    });
                    DtabKapan.AcceptChanges();
                    //#P : 10-09-2020
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }
        }

        public void Calculatesummary()
        {
            TotKapanCarat = 0;
            DataRow dr = GrdDet.GetFocusedDataRow();
            foreach (DataRow Drow in DtabKapan.Rows)
            {
                TotKapanCarat = TotKapanCarat + Val.Val(Drow["KAPANCARAT"]);
            }
        }

        private void reptxtCarat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {


                //GrdDet.PostEditor();
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDet.PostEditor();
                    Calculatesummary();
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (Val.Val(TotKapanCarat) != 0)
                    {
                        if (TotKapanCarat > Val.Val(lblBalanceCarat.Text) && mFormType == FORMTYPE.ORIGINALKAPAN)
                        {
                            Global.Message("Kapan Carat Is Greate Than Balance Carat.");
                            GrdDet.SetFocusedRowCellValue("KAPANCARAT", 0);
                            GrdDet.FocusedRowHandle = dr.Table.Rows.IndexOf(dr);
                            GrdDet.FocusedColumn = GrdDet.VisibleColumns[6];
                            GrdDet.Focus();
                            e.Handled = true;
                            //e.Cancel = true;

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }


        }

        private void FrmKapanCreation_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnBack_Click(null, null);
            //}
        }

        private void reptxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (!Val.ToString(dr["KAPANNAME"]).Equals(string.Empty) && Val.Val(dr["KAPANCARAT"]) > 0.00 && GrdDet.IsLastRow && mFormType != FORMTYPE.ORIGINALKAPANUPDATE)
                    {

                        string strKpType = "";
                        if (mFormType == FORMTYPE.PCNKAPAN)
                            strKpType = "PCN";
                        else if (mFormType == FORMTYPE.REPAIRING)
                            strKpType = "REPAIRING";
                        else
                            strKpType = "ORIGINAL";

                        DataRow DR = DtabKapan.NewRow();
                        DR["KAPANCATEGORY"] = strKpType;
                        DtabKapan.Rows.Add(DR);
                        DtabKapan.AcceptChanges();
                    }
                    else if (GrdDet.IsLastRow)
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

        private void txtSubLot_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("MANAGER_ID", Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]));
                        GrdDet.SetFocusedRowCellValue("MANAGERNAME", Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]));
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

        private void reptxtKapan_Validating(object sender, CancelEventArgs e)
        {
            if (GrdDet.FocusedRowHandle < 0)
                return;

            try
            {
                GrdDet.PostEditor();
                DataRow Dr = GrdDet.GetFocusedDataRow();
                if (!Val.ToString(GrdDet.EditingValue).Trim().Equals(string.Empty))
                {
                    if (CheckDuplicateKapan("KAPANNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "KAPAN"))
                        e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        public bool CheckDuplicateKapan(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabKapan.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper()
                         && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;

            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }

        private void txtSubLot1_Validating(object sender, CancelEventArgs e)
        {
            //    if (GrdDet.FocusedRowHandle < 0)
            //        return;

            //    try
            //    {
            //        DataRow Dr = GrdDet.GetFocusedDataRow();
            //        if (CheckDuplicateKapan("KAPANNAME", Dr["KAPANNAME"].ToString(), "SUBLOT", Dr["SUBLOT"].ToString(), "SUBLOT1", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "KAPAN"))
            //            e.Cancel = true;
            //        return;
            //    }
            //    catch (Exception ex)
            //    {
            //        Global.Message(ex.Message.ToString());
            //    }
        }

        private void txtSubLot1_Leave(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle < 0)
                return;

            try
            {
                GrdDet.PostEditor();
                DataRow Dr = GrdDet.GetFocusedDataRow();
                if (CheckDuplicateKapan("KAPANNAME", Dr["KAPANNAME"].ToString(), GrdDet.FocusedRowHandle, "KAPAN"))
                {
                    
                    GrdDet.SetFocusedRowCellValue("KAPANNAME", string.Empty);

                    GrdDet.FocusedRowHandle = GrdDet.FocusedRowHandle;
                    GrdDet.FocusedColumn = GrdDet.Columns[0];
                    GrdDet.Focus();
                    GrdDet.ShowEditor();

                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }


    }
}
