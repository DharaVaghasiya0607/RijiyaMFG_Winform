using AxoneMFGRJ.Masters;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Sale;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.Data;
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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmRoughAllotmentEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_RoughAllotment ObjRough = new BOTRN_RoughAllotment();
        BOFormPer ObjPer = new BOFormPer();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        BOTRN_Rejection ObjRejection = new BOTRN_Rejection();

        DataTable DTabDetail = new DataTable();
        DataTable DTabInvoice = new DataTable();
        DataTable DTabOut = new DataTable();
        DataTable DTabAssort = new DataTable();
        DataTable DTabVal = new DataTable();
        DataTable DtabKapan = new DataTable();
        DataTable DtabRejection = new DataTable();
     
        double TotKapanCarat = 0;
        // double TotKapanCarat = 0;
        double DouRJCarat = 0;  // K : 06/12/2022
        double DouRejectionAmount = 0;
        double DouRejectionCarat = 0;

        double DouCarat = 0;
        double DouAmt = 0;
        double FDouAmt = 0;

        public FORMTYPE mFormType = FORMTYPE.ORIGINALKAPAN;
        public enum FORMTYPE
        {
            ORIGINALKAPAN = 0,
            REPAIRING = 1,
            PCNKAPAN = 2,
            ORIGINALKAPANUPDATE = 3
        }

        #region Property Settings

        public FrmRoughAllotmentEntry()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            //CmbRoughType.SelectedIndex = 0;
            BtnSearch_Click(null, null);

            this.Show();

            DTPFromDate.Value = DateTime.Now.AddMonths(-1);
            DTPToDate.Value = DateTime.Now;
            FetchInvoiceData(-1, -1);

            //DataRow DrKapan = DtabKapan.NewRow();
            //DtabKapan.Rows.Add(DrKapan);
            //MainKapanGrd.DataSource = DtabKapan;

            DtabKapan.Columns.Add(new DataColumn("KAPAN_ID", typeof(Int64)));
            DtabKapan.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DtabKapan.Columns.Add(new DataColumn("KAPANGROUP", typeof(string)));
            DtabKapan.Columns.Add(new DataColumn("KAPANCATEGORY", typeof(string)));
            DtabKapan.Columns.Add(new DataColumn("MANAGER_ID", typeof(Int64)));
            DtabKapan.Columns.Add(new DataColumn("MANAGERNAME", typeof(string)));
            DtabKapan.Columns.Add(new DataColumn("KAPANPCS", typeof(int)));
            DtabKapan.Columns.Add(new DataColumn("KAPANCARAT", typeof(double)));
            DtabKapan.Columns.Add(new DataColumn("ENTRYDATE", typeof(DateTime)));

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
            DtabKapan.Columns.Add(new DataColumn("COMPARMEMO", typeof(string)));
            DtabKapan.Columns.Add(new DataColumn("KAPANTYPE", typeof(string)));
            //DtabKapan.Columns.Add(new DataColumn("LOT_ID", typeof(string)));
            //DtabKapan.Columns.Add(new DataColumn("KAPANCATEGORY", typeof(Int32)));


            //DtabKapan.Rows.Add(DtabKapan.NewRow());

            DtabKapan.Rows.Add(DtabKapan.NewRow());
            MainKapanGrd.DataSource = DtabKapan;

            GrdKapan.FocusedRowHandle = 0;
            GrdKapan.FocusedColumn = GrdKapan.VisibleColumns[0];
            GrdKapan.Focus();

            MainKapanGrd.RefreshDataSource();

            CmbCompareMemo.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbCompareMemo.DisplayMember = "KAPANNAME";
            CmbCompareMemo.ValueMember = "KAPANNAME";


            DtabRejection.Columns.Add(new DataColumn("ENTRYDATE", typeof(DateTime)));
            DtabRejection.Columns.Add(new DataColumn("REJECTION_ID", typeof(int)));
            DtabRejection.Columns.Add(new DataColumn("REJECTIONNAME", typeof(string)));
            DtabRejection.Columns.Add(new DataColumn("PCS", typeof(int)));
            DtabRejection.Columns.Add(new DataColumn("CARAT", typeof(double)));
            DtabRejection.Columns.Add(new DataColumn("RATE", typeof(double)));
            DtabRejection.Columns.Add(new DataColumn("AMOUNT", typeof(double)));
            DtabRejection.Columns.Add(new DataColumn("REMARK", typeof(string)));

            DtabRejection.Rows.Add(DateTime.Now.ToString("dd/MM/yyyy"));
            // DtabKapan.Rows.Add(DtabKapan.NewRow());

            MainGrdRej.DataSource = DtabRejection;

            GrdRej.FocusedRowHandle = 0;
            GrdRej.FocusedColumn = GrdRej.VisibleColumns[0];
            GrdRej.Focus();

            MainGrdRej.RefreshDataSource();
            xtraTabControl1.SelectedTabPageIndex = 0;
            txtRoughName.Focus();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjRough);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        #region Validation

        private bool ValSave()
        {
            for (int IntI = 0; IntI < DTabDetail.Rows.Count; IntI++)
            {
                DataRow DRow = DTabDetail.Rows[IntI];
                if (Val.Val(DRow["WEIGHT"]) != 0 && Val.Val(DRow["RATE"]) == 0)
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Rate Is Required At this Row");
                    return false;
                }
                if (Val.Val(DRow["WEIGHT"]) != 0 && Val.Val(DRow["AMOUNT"]) == 0)
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Amount Is Required At this Row");
                    return false;
                }

                if (Val.Val(DRow["WEIGHT"]) == 0 && Val.Val(DRow["RATE"]) != 0)
                {
                    Global.MessageError("Row No :[ " + IntI.ToString() + " ] Weight Is Required At this Row");
                    return false;
                }

            }

            //if (Val.Val(txtAssortmentCts.Text) > Val.Val(txtAssortmentBalance.Text))
            //{
            //    Global.MessageError("You can't Allot Carat Not More Then Assortment Balance");
            //    txtAssortmentBalance.Focus();
            //    return false;
            //}

            return true;
        }

        #endregion

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            DTabDetail.Rows.Clear();
            DtabKapan.Rows.Clear();
            DtabRejection.Rows.Clear();

            txtAllotmentID.Text = string.Empty;
            txtAllotmentID.Tag = string.Empty;

            txtRoughName.Text = String.Empty;
            txtRoughName.Tag = String.Empty;
            txtRoughNo.Text = string.Empty;

            txtPartyName.Text = string.Empty;
            txtPartyName.Tag = string.Empty;
            txtPartyCode.Text = string.Empty;

            txtBrokerName.Tag = string.Empty;
            txtBrokerName.Text = string.Empty;
            txtBrokerCode.Text = string.Empty;
            txtBrokeragePer.Text = string.Empty;

            txtExtraExp.Text = string.Empty;

            txtExcRate.Text = string.Empty;

            txtRoughCts.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtRateFE.Text = string.Empty;

            txtNetRate.Text = string.Empty;
            txtNetRateFE.Text = string.Empty;

            txtOutCts.Text = string.Empty;
            txtOutRate.Text = string.Empty;
            txtOutRateFE.Text = string.Empty;

            txtAssortmentCts.Text = string.Empty;
            txtAssortmentRate.Text = string.Empty;
            txtAssortmentRateFE.Text = string.Empty;
            txtAssortmentBalance.Text = string.Empty;

            txtMFGCts.Text = string.Empty;
            txtMFGRate.Text = string.Empty;
            txtMFGRateFE.Text = string.Empty;

            txtInvoiceRemark.Text = string.Empty;

            CalculateSummary();

            lblMode.Text = "Add Mode";
            DTPAllotmentDate.Focus();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string StrAllotmentID = "";

                if (ValSave() == false)
                {
                    return;
                }
                if (Global.Confirm("Are you Sure Your Want To Save This Entry ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                RoughAllotmentProperty Property = new RoughAllotmentProperty();

                Property.ALLOTMENT_ID = Val.ToInt64(txtAllotmentID.Text);
                Property.ALLOTMENTDATE = Val.SqlDate(DTPAllotmentDate.Text);

                Property.INVOICE_ID = Val.ToInt64(lblInvoice_ID.Text);
                Property.LOT_ID = Val.ToInt64(txtRoughName.Tag);

                Property.PARTY_ID = Val.ToInt64(txtPartyName.Tag);

                Property.BROKER_ID = Val.ToInt64(txtBrokerName.Tag);
                Property.BORKERAGEPER = Val.Val(txtBrokeragePer.Text);

                Property.EXTRAEXP = Val.Val(txtExtraExp.Text);

                Property.EXCRATE = Val.Val(txtExcRate.Text);

                Property.ROUGHWEIGHT = Val.Val(txtRoughCts.Text);
                Property.ROUGHOUTRATE = Val.Val(txtRate.Text);
                Property.FROUGHOUTRATE = Val.Val(txtRateFE.Text);

                Property.NETAMOUNT = Val.Val(txtNetRate.Text);
                Property.FNETAMOUNT = Val.Val(txtNetRateFE.Text);

                Property.OUTROUGHCTS = Val.Val(txtOutCts.Text);
                Property.OUTRATE = Val.Val(txtOutRate.Text);
                Property.FOUTRATE = Val.Val(txtOutRateFE.Text);

                Property.ASSORTMENTCTS = Val.Val(txtAssortmentCts.Text);
                Property.ASSORTMENTRATE = Val.Val(txtAssortmentRate.Text);
                Property.FASSORTMENTRATE = Val.Val(txtAssortmentRateFE.Text);
                Property.ASSORTMENTBALANCECTS = Val.Val(txtAssortmentBalance.Text);

                Property.MFGCTS = Val.Val(txtMFGCts.Text);
                Property.MFGRATE = Val.Val(txtMFGRate.Text);
                Property.FMFGRATE = Val.Val(txtMFGRateFE.Text);

                Property.REMARK = Val.ToString(txtInvoiceRemark.Text);

                DTabDetail.TableName = "Table1";
                string RoughDetailXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabDetail.WriteXml(sw);
                    RoughDetailXML = sw.ToString();
                }

                RoughDetailXML = Regex.Replace(RoughDetailXML,
                                 @"<ASSORTMENTDATE>(?<year>\d{4})-(?<month>\d{2})-(?<date>\d{2}).*?</ASSORTMENTDATE>",
                                 @"<ASSORTMENTDATE>${month}/${date}/${year}</ASSORTMENTDATE>",
                                 RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                Property.XMLFORROUGHDETAIL = RoughDetailXML;

                Property = ObjRough.Save(Property);
                StrAllotmentID = Property.ReturnValue;


                //DtabKapan.DefaultView.Sort = "KAPANNAME";
                //DtabKapan = DtabKapan.DefaultView.ToTable();

                //string ReturnMessageDesc = "";
                //string ReturnMessageType = "";
                DtabKapan.AcceptChanges();

                foreach (DataRow Dr in DtabKapan.Rows)
                {
                    if (Val.ToString(Dr["KAPANNAME"]).Trim().Equals(string.Empty) || Val.Val(Dr["KAPANCARAT"]) <= 0)
                    {
                        continue;
                    }

                    TrnKapanCreateProperty PClsKapan = new TrnKapanCreateProperty();

                    PClsKapan.KAPAN_ID = Val.ToInt64(Dr["KAPAN_ID"]); 

                    PClsKapan.KAPANNAME = Val.Trim(Val.ToString(Dr["KAPANNAME"]));
                    PClsKapan.KAPANGROUP = Val.Trim(Val.ToString(Dr["KAPANGROUP"]));
                    PClsKapan.KAPANCATEGORY = Val.Trim(Val.ToString(Dr["KAPANCATEGORY"]));

                    PClsKapan.MANAGER_ID = Val.Bigint(Dr["MANAGER_ID"]);

                   // PClsKapan.LOT_ID = Val.ToInt64(Dr["LOT_ID"]);
                    PClsKapan.LOT_ID = Val.ToInt64(txtRoughName.Tag);
                    PClsKapan.KAPANPCS = Val.ToInt32(Dr["KAPANPCS"]);
                    PClsKapan.KAPANCARAT = Val.Val(Dr["KAPANCARAT"]);

                    PClsKapan.KAPANRATE = Val.Val(Dr["KAPANRATE"]);
                    PClsKapan.KAPANAMOUNT = Val.Val(Dr["KAPANAMOUNT"]);

                    PClsKapan.EXPMAKPER = Val.Val(Dr["EXPMAKPER"]);
                    PClsKapan.EXPMAKCARAT = Val.Val(Dr["EXPMAKCARAT"]);
                    PClsKapan.EXPPOLPER = Val.Val(Dr["EXPPOLPER"]);
                    PClsKapan.EXPPOLCARAT = Val.Val(Dr["EXPPOLCARAT"]);
                    PClsKapan.EXPDOLLAR = Val.Val(Dr["EXPDOLLAR"]);

                    PClsKapan.LOTGROUP = Val.ToString(Dr["LOTGROUP"]);
                    PClsKapan.DUEDAYS = Val.ToInt32(Dr["DUEDAYS"]);
                    PClsKapan.EXPGIAPER = Val.Val(Dr["EXPGIAPER"]);

                    PClsKapan.REMARK = Val.ToString(Dr["REMARK"]);

                    PClsKapan.KAPANTYPE = Val.ToString(Dr["KAPANTYPE"]);

                    PClsKapan.COMPARMEMO = Val.ToString(Dr["COMPARMEMO"]);
                    PClsKapan.DIAMONDTYPE = Val.ToString(Dr["DIAMONDTYPE"]);

                    PClsKapan = ObjKapan.Save(PClsKapan);

                    PClsKapan = null;

                }

                foreach (DataRow Dr in DtabRejection.Rows)
                {
                    //string ReturnMessageDesc = "";
                    //string ReturnMessageType = "";
                    TRN_RejectionProperty PClsRejection = new TRN_RejectionProperty();

                    if (Val.ToString(Dr["REJECTIONNAME"]).Trim().Equals(string.Empty) || Val.ToString(Dr["ENTRYDATE"]).Trim().Equals(string.Empty) || Val.Val(Dr["CARAT"]) <= 0)
                        continue;


                    PClsRejection.REJECTIONTRN_ID = Val.ToInt64(Dr["REJECTIONTRN_ID"]); 
                    PClsRejection.REJECTIONDATE = Val.ToString(Val.SqlDate(Val.ToString(Dr["ENTRYDATE"])));
                    PClsRejection.REJECTIONFROM = "INVOICE";

                    PClsRejection.REJECTION_ID = Val.ToInt32(Dr["REJECTION_ID"]);

                    PClsRejection.PCS = Val.ToInt32(Dr["PCS"]);
                    PClsRejection.CARAT = Val.Val(Dr["CARAT"]);
                    PClsRejection.RATE = Val.Val(Dr["RATE"]);
                    PClsRejection.AMOUNT = Math.Round(PClsRejection.CARAT * PClsRejection.RATE, 2);
                    PClsRejection.REMARK = Val.ToString(Dr["REMARK"]);

                    PClsRejection = ObjRejection.Save(PClsRejection);
                    //ReturnMessageDesc = Property.ReturnMessageDesc;
                    //ReturnMessageType = Property.ReturnMessageType;
                    PClsRejection = null;


                    //if (ReturnMessageType == "SUCCESS")
                    //{
                    //    Global.Message(ReturnMessageDesc);
                    //    // this.Close();
                    //}
                    //else
                    //{
                    //   // BtnAdd_Click(null, null);
                    //    Global.MessageError(ReturnMessageDesc);main
                    //}
                }


                Global.Message("Succefully Saved");
                txtAllotmentID.Text = StrAllotmentID;
                
                BtnAdd_Click(null, null);


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

        public void FetchValue(DataRow DR)
        {
            lblMode.Text = "Edit Mode";
            txtAllotmentID.Text = Val.ToString(DR["ALLOTMENT_ID"]);
            DTPAllotmentDate.Value = DateTime.Parse(Val.ToString(DR["ALLOTMENTDATE"]));

            txtRoughName.Text = Val.ToString(DR["MANUALINVOICENO"]);

            //Global.Message("FetchValue"); //14-12-2022

            txtRoughNo.Text = Val.ToString(DR["LOTNO"]);  //K : 30/11/2022
            txtRoughName.Tag = Val.ToString(DR["LOT_ID"]);

            txtPartyName.Tag = Val.ToString(DR["PARTY_ID"]);
            txtPartyName.Text = Val.ToString(DR["PARTYNAME"]);
            txtPartyCode.Text = Val.ToString(DR["PARTYCODE"]);

            txtBrokerName.Tag = Val.ToString(DR["BROKER_ID"]);
            txtBrokerName.Text = Val.ToString(DR["BROKERNAME"]);
            txtBrokerCode.Text = Val.ToString(DR["BROKERCODE"]);

            txtBrokeragePer.Text = Val.ToString(DR["BORKERAGEPER"]);

            txtExtraExp.Text = Val.ToString(DR["EXTRAEXP"]);

            txtExcRate.Text = Val.ToString(DR["EXCRATE"]);

            txtRoughCts.Text = Val.ToString(DR["ROUGHWEIGHT"]);
            txtRate.Text = Val.ToString(DR["ROUGHOUTRATE"]);
            txtRateFE.Text = Val.ToString(DR["FROUGHOUTRATE"]);

            txtNetRate.Text = Val.ToString(DR["NETAMOUNT"]);
            txtNetRateFE.Text = Val.ToString(DR["FNETAMOUNT"]);

            txtOutCts.Text = Val.ToString(DR["OUTROUGHCTS"]);
            txtOutRate.Text = Val.ToString(DR["OUTRATE"]);
            txtOutRateFE.Text = Val.ToString(DR["FOUTRATE"]);

            txtAssortmentCts.Text = Val.ToString(DR["ASSORTMENTCTS"]);
            txtAssortmentRate.Text = Val.ToString(DR["ASSORTMENTRATE"]);
            txtAssortmentRateFE.Text = Val.ToString(DR["FASSORTMENTRATE"]);
            txtAssortmentBalance.Text = Val.ToString(DR["ASSORTMENTBALANCECTS"]);

            txtMFGCts.Text = Val.ToString(DR["MFGCTS"]);
            txtMFGRate.Text = Val.ToString(DR["MFGRATE"]);
            txtMFGRateFE.Text = Val.ToString(DR["FMFGRATE"]);

            txtInvoiceRemark.Text = Val.ToString(DR["REMARK"]);

            // CalculateSummary();
            DTPAllotmentDate.Focus();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("List", GrdDet);
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        RoughAllotmentProperty Property = new RoughAllotmentProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.ALLOTMENTDETAIL_ID = Val.ToInt64(Drow["ALLOTMENTDETAIL_ID"]);
                        double pDouAssortmenBalance = Val.Val(Drow["WEIGHT"]);
                        double pDouMFGRate = Val.Val(Drow["RATE"]);
                        double pDouMFGRateFE = Val.Val(Drow["FRATE"]);
                        Property.ALLOTMENT_ID = 0;
                        Property = ObjRough.Delete(Property, pDouAssortmenBalance, pDouMFGRate, pDouMFGRateFE);
                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message(Property.ReturnMessageDesc);
                            DTabDetail.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DTabDetail.AcceptChanges();
                            BtnSearch_Click(null, null);
                            CalculateSummary();
                        }
                        else
                        {
                            Global.MessageError(Property.ReturnMessageDesc);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }
        public void CalculateSummary()
        {
            double DouCarat = 0;
            double DouRate = 0;
            double DouAmount = 0;


            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                DouCarat = DouCarat + Val.Val(DRow["WEIGHT"]);
                DouRate = DouRate + Val.Val(DRow["RATE"]);
                DouAmount = DouAmount + Val.Val(DRow["AMOUNT"]);
            }

            txtMFGCts.Text = DouCarat.ToString();
            txtMFGRate.Text = Math.Round(DouAmount / Val.Val(txtMFGCts.Text), 2).ToString();
            txtMFGRateFE.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtMFGRate.Text), 2).ToString();

            txtAssortmentCts.Text = DouCarat.ToString();
            txtAssortmentRate.Text = Math.Round(DouAmount / Val.Val(txtAssortmentCts.Text), 2).ToString();
            txtAssortmentRateFE.Text = Math.Round(Val.Val(txtExcRate.Text) * Val.Val(txtAssortmentRate.Text), 2).ToString();

            //if (Val.Val(txtAssortmentBalance.Text) != 0)
            //{
            txtAssortmentBalance.Text = Math.Round(Val.Val(txtRoughCts.Text) - Val.Val(txtOutCts.Text) - Val.Val(txtMFGCts.Text), 2).ToString();
            //}

        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string StrFromDate = null;
                string StrToDate = null;

                if (DTPFromDate.Checked == true && DTPToDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                this.Cursor = Cursors.WaitCursor;
                GrdDetList.BeginUpdate();
                DataSet DS = ObjRough.GetAllotmentDetail(0, 0, StrFromDate, StrToDate);
                DataTable DTab = DS.Tables[0];
                MainGridList.DataSource = DTab;
                GrdDetList.RefreshData();
                GrdDetList.EndUpdate();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }


        public void FetchInvoiceData(Int64 pIntAllotment_ID, Int64 pIntInvoice_ID)
        {
            this.Cursor = Cursors.WaitCursor;
            DataSet DS = ObjRough.GetAllotmentDetail(pIntAllotment_ID, Val.ToInt64(txtRoughName.Tag));

            if (DS.Tables.Count != 0)
            {
                DTabDetail = DS.Tables[1];

                GrdDet.BeginUpdate();
                DataRow DRow = DTabDetail.NewRow();
                DTabDetail.Rows.Add(DRow);
                DtabKapan.Rows.Add(DtabKapan.NewRow());
                DtabRejection.Rows.Add(DtabRejection.NewRow());
                MainGrid.DataSource = DTabDetail;
                GrdDet.RefreshData();
                GrdDet.EndUpdate();
            }
            if (DS.Tables[0].Rows.Count != 0)
            {
                DataRow DrDetail = DS.Tables[0].Rows[0];
                FetchValue(DrDetail);
            }
            else
            {
                BtnAdd_Click(null, null);
            }



            this.Cursor = Cursors.Default;
        }

        private void GrdDetList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
           
            if (e.Clicks == 1)
            {

                FetchInvoiceData(Val.ToInt64(GrdDetList.GetFocusedRowCellValue("ALLOTMENT_ID")), 0);
                xtraTabControl1.SelectedTabPageIndex = 0;

                // xtraTabControl1.SelectedTabPageIndex = 1;

                Int64 LotID = Val.ToInt64(GrdDetList.GetFocusedRowCellValue("LOT_ID"));
                string LotNo = Val.ToString(GrdDetList.GetFocusedRowCellValue("LOTNO"));
                string SystemInvoiceNo = Val.ToString(GrdDetList.GetFocusedRowCellValue("SYSTEMINVOICENO"));
                string ManualInvoiceNo = Val.ToString(GrdDetList.GetFocusedRowCellValue("MANUALINVOICENO"));

                GrpCaption.Text = "TRACKING OF INVOICE : [ " + ManualInvoiceNo + " ] && LOT NO : [ " + LotNo + " ]";


                xtraTabKapan.Text = "     Kapan ( 0 )     ";
                xtraTabRejection.Text = "     REJECTION ( 0 )     ";

                if (LotID != 0)
                {
                    this.Cursor = Cursors.WaitCursor;


                    GrdKapan.BeginUpdate();
                    GrdRej.BeginUpdate();


                    DataSet DS = ObjMast.GetLotIDTracking(LotID);
                    

                    MainKapanGrd.DataSource = DS.Tables[1];
                    MainKapanGrd.Refresh();
                    DtabKapan.Rows.Add(DtabKapan.NewRow());



                    MainGrdRej.DataSource = DS.Tables[2];
                    MainGrdRej.Refresh();

                    GrdKapan.BestFitColumns();
                    GrdRej.BestFitColumns();

                    GrdKapan.EndUpdate();
                    GrdRej.EndUpdate();

                    xtraTabKapan.Text = "     KAPAN ( " + DS.Tables[1].Rows.Count.ToString() + " )     ";
                    xtraTabRejection.Text = "     REJECTION ( " + DS.Tables[2].Rows.Count.ToString() + " )     ";

                    this.Cursor = Cursors.Default;

                }
            }

            if (e.Clicks == 2)
            {
                Int64 LOT_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("LOT_ID"));

                if (e.Column.FieldName == "KAPANCARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("KAPAN", LOT_ID, txtRoughName.Text);

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Kapan List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;
                    FrmPopupGrid.Width = 1000;
                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
                else if (e.Column.FieldName == "REJECTIONCARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("REJECTION", LOT_ID, txtRoughName.Text);

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Rejection List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;
                    FrmPopupGrid.Width = 1000;

                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
                else if (e.Column.FieldName == "SPLITCARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("SPLIT", LOT_ID, txtRoughName.Text);

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Split List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;
                    FrmPopupGrid.Width = 1000;

                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
                else if (e.Column.FieldName == "MIXCARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("MIX", LOT_ID, txtRoughName.Text);

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Mix List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;
                    FrmPopupGrid.Width = 1000;

                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
                else if (e.Column.FieldName == "SALECARAT")
                {
                    DataTable DtData = ObjKapan.GetLotUsedCaratData("SALE", LOT_ID, txtRoughName.Text);

                    FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                    FrmPopupGrid.MainGrid.DataSource = DtData;
                    FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                    FrmPopupGrid.Text = "Sale List";
                    FrmPopupGrid.ISPostBack = true;
                    this.Cursor = Cursors.Default;
                    FrmPopupGrid.Width = 1000;

                    FrmPopupGrid.ShowDialog();
                    FrmPopupGrid.Hide();
                    FrmPopupGrid.Dispose();
                    FrmPopupGrid = null;
                }
                else if (e.Column.FieldName == "KAPANNAME")
                {
                    Int64 StrLot_ID = 0; double DouRCostWithDalaliAmt = 0;

                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    StrLot_ID = Val.ToInt64(DRow["LOT_ID"]);
                    DouRCostWithDalaliAmt = Math.Round((Val.Val(DRow["ROUGHCOSTWITHDALALI"]) * Val.Val(DRow["CARAT"])), 3);

                    if (StrLot_ID == 0)
                        return;

                    this.Cursor = Cursors.WaitCursor;
                    FrmKapanCreation FrmKapanCreation = new FrmKapanCreation();
                    //FrmKapanCreation.MdiParent = Global.gMainRef;
                    //FrmKapanCreation.ShowForm(StrLot_ID, FrmKapanCreation.FORMTYPE.ORIGINALKAPANUPDATE, DouRCostWithDalaliAmt);
                    this.Cursor = Cursors.Default;
                }

                else if (e.Column.FieldName == "SYSTEMINVOICENO")
                {

                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    Int64 StrInvoiceID = Val.ToInt64(DRow["INVOICE_ID"]);

                    if (StrInvoiceID == 0)
                        return;

                    this.Cursor = Cursors.WaitCursor;

                    FrmRoughPurchaseMasterDetail FrmRoughPurchaseMasterDetail = new FrmRoughPurchaseMasterDetail();
                    FrmRoughPurchaseMasterDetail.MdiParent = Global.gMainRef;
                    FrmRoughPurchaseMasterDetail.ShowForm(StrInvoiceID);

                    this.Cursor = Cursors.Default;
                }

            }
            //}
            //}
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtAllotmentID.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please First Select Record That You Want To Delete");
                    return;
                }

                if (Global.Confirm("Are Your Sure To Delete The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;
                RoughAllotmentProperty Property = new RoughAllotmentProperty();
                Property.ALLOTMENT_ID = Val.ToInt64(txtAllotmentID.Text);
                Property.ALLOTMENTDETAIL_ID = 0;
                Property = ObjRough.Delete(Property, Val.Val(txtMFGCts.Text), 0, 0);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    BtnAdd_Click(null, null);
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    txtAllotmentID.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        private void txtRoughNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    BtnAdd_Click(null, null);
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LOTNO,MANUALNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_INVOICE);
                    FrmSearch.mColumnsToHide = "INVOICE_ID,PARTYINVOICENO,LOT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRoughName.Text = Val.ToString(FrmSearch.mDRow["MANUALNO"]);
                        txtRoughNo.Text = Val.ToString(FrmSearch.mDRow["LOTNO"]);  //K :28/11/22
                        txtRoughName.Tag = Val.ToString(FrmSearch.mDRow["LOT_ID"]);
                        lblInvoice_ID.Text = Val.ToString(FrmSearch.mDRow["INVOICE_ID"]);

                        //Global.Message(Val.ToString(FrmSearch.mDRow["LOTNO"])); //14-12-2022

                        DataSet Ds = ObjRough.GetInvoiceDetail(Val.ToInt64(txtRoughName.Tag));

                        if (Ds.Tables.Count == 4)
                        {
                            FetchInvoiceData(0, Val.ToInt64(txtRoughName.Tag));
                        }
                        else
                        {
                            if (Ds.Tables.Count == 3)
                            {
                                DTabInvoice.Rows.Clear();
                                DTabOut.Rows.Clear();
                                DTabAssort.Rows.Clear();

                                DTabInvoice = Ds.Tables[0];
                                DTabOut = Ds.Tables[1];
                                DTabAssort = Ds.Tables[2];

                                if (DTabInvoice.Rows.Count != 0)
                                {
                                    txtRoughCts.Text = Val.ToString(DTabInvoice.Rows[0]["TOTALCARAT"]);
                                    txtExcRate.Text = Val.ToString(Math.Round(Val.Val(DTabInvoice.Rows[0]["EXCRATE"]), 2));
                                    txtRate.Text = Val.ToString(Math.Round(Val.Val(DTabInvoice.Rows[0]["RATE"]), 2));
                                    txtNetRate.Text = Val.ToString(Math.Round(Val.Val(DTabInvoice.Rows[0]["NETAMOUNT"]), 2));
                                    txtRateFE.Text = Val.ToString(Math.Round(Val.Val(txtExcRate.Text) * Val.Val(DTabInvoice.Rows[0]["RATE"]), 2));
                                    txtNetRateFE.Text = Val.ToString(Val.Val(txtExcRate.Text) * Val.Val(DTabInvoice.Rows[0]["NETAMOUNT"]));
                                    txtPartyName.Text = Val.ToString(DTabInvoice.Rows[0]["PARTYNAME"]);
                                    txtPartyName.Tag = Val.ToString(DTabInvoice.Rows[0]["Party_ID"]);
                                    txtPartyCode.Text = Val.ToString(DTabInvoice.Rows[0]["PARTYCODE"]);
                                    txtBrokerName.Text = Val.ToString(DTabInvoice.Rows[0]["BROKERNAME"]);
                                    txtBrokerName.Tag = Val.ToString(DTabInvoice.Rows[0]["BROKER_ID"]);
                                    txtBrokerCode.Text = Val.ToString(DTabInvoice.Rows[0]["BROKERCODE"]);
                                    txtBrokeragePer.Text = Val.ToString(DTabInvoice.Rows[0]["BROKRAGEPER"]);
                                }

                                if (DTabOut.Rows.Count != 0)
                                {
                                    txtOutCts.Text = Val.ToString(DTabOut.Rows[0]["OUTCARAT"]);
                                    txtOutRate.Text = Val.ToString(Math.Round(Val.Val(DTabOut.Rows[0]["OUTRATE"]), 2));
                                    txtOutRateFE.Text = Val.ToString(Val.Val(DTabOut.Rows[0]["OUTRATE"]) * Val.Val(txtExcRate.Text));
                                }

                                if (DTabAssort.Rows.Count != 0)
                                {
                                    //txtAssortmentCts.Text = Val.ToString(DTabAssort.Rows[0]["ASSORTMENTCARAT"]);
                                    txtAssortmentBalance.Text = Math.Round(Val.Val(DTabInvoice.Rows[0]["TOTALCARAT"]) - Val.Val(DTabOut.Rows[0]["OUTCARAT"]), 2).ToString();
                                    //txtAssortmentRate.Text = Val.ToString(DTabAssort.Rows[0]["ASSORTMENTRATE"]);
                                    //txtAssortmentRateFE.Text = Val.ToString(Val.Val(DTabAssort.Rows[0]["ASSORTMENTRATE"]) * Val.Val(txtExcRate.Text));
                                }

                                if (Val.Val(txtAssortmentBalance.Text) == 0)
                                {
                                    Global.Message("You can't Add Assortment for this Rough");
                                    BtnAdd_Click(null, null);
                                    return;
                                }
                                DataRow DRow = DTabDetail.NewRow();
                                DTabDetail.Rows.Add(DRow);
                                MainGrid.DataSource = DTabDetail;
                                GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                                GrdDet.FocusedColumn = GrdDet.Columns["ASSORTMENTDATE"];
                                GrdDet.RefreshData();
                                MainGrid.Refresh();
                                DataRow DrKapan = DtabKapan.NewRow();
                                DtabKapan.Rows.Add(DtabKapan.NewRow());
                                GrdKapan.FocusedRowHandle = GrdKapan.RowCount - 1;
                                GrdKapan.FocusedColumn = GrdKapan.Columns["KAPANNAME"];
                                GrdDet.RefreshData();
                                MainGrid.Refresh();

                                DtabRejection.Rows.Add(DtabRejection.NewRow());
                               
                            }
                        }

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

        private void reptxtAssortment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "ASSORTMENTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_ASSORTMENT);
                    FrmSearch.mColumnsToHide = "ASSORTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("ASSORTMENTNAME", Val.ToString(FrmSearch.mDRow["ASSORTMENTNAME"]));
                        GrdDet.SetFocusedRowCellValue("ASSORTMENT_ID", Val.ToString(FrmSearch.mDRow["ASSORTMENT_ID"]));
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

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            GrdDet.PostEditor();

            DataRow DRow = GrdDet.GetDataRow(e.RowHandle);

            double pDouWeight = 0;
            double pDouRate = 0;
            double pDouAmt = 0;
            double PDouRateFE = 0;
            double pDouAmtFE = 0;

            pDouWeight = Val.Val(DRow["WEIGHT"]);
            pDouRate = Val.Val(DRow["RATE"]);
            pDouAmt = Val.Val(DRow["AMOUNT"]);

            if (e.Column.FieldName == "WEIGHT")
            {
                if (pDouWeight > Val.Val(txtAssortmentBalance.Text) && Val.Val(txtAssortmentBalance.Text) != 0)
                {
                    Global.MessageError("Assortment Carat Not More Then Assortment Balance At ASSORTMENT : " + DRow["ASSORTMENTNAME"].ToString());
                    GrdDet.SetRowCellValue(e.RowHandle, "WEIGHT", 0);
                    //return;
                }
                else if (pDouWeight > Val.Val(txtAssortmentBalance.Text) && Val.Val(txtAssortmentBalance.Text) == 0)
                {
                    Global.MessageError("You have not enough carat for Rough Assortment");
                    GrdDet.SetRowCellValue(e.RowHandle, "WEIGHT", 0);
                    return;
                }
            }

            if (e.Column.FieldName == "RATE")
            {
                pDouAmt = Math.Round(pDouWeight * pDouRate, 3);
                PDouRateFE = Math.Round(Val.Val(txtExcRate.Text) * pDouRate, 2);
                pDouAmtFE = Math.Round(Val.Val(txtExcRate.Text) * pDouAmt, 2);
                GrdDet.SetRowCellValue(e.RowHandle, "AMOUNT", pDouAmt);
                GrdDet.SetRowCellValue(e.RowHandle, "FRATE", PDouRateFE);
                GrdDet.SetRowCellValue(e.RowHandle, "FAMOUNT", pDouAmtFE);
                //txtAssortmentCts.Text = Math.Round(Val.Val(txtRoughCts.Text) - pDouWeight, 2).ToString();

            }
            CalculateSummary();
        }

        private void txtExcRate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                CalculateSummary();
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void ReptxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (Val.Val(GrdDet.GetFocusedRowCellValue("WEIGHT")) != 0 && Val.Val(GrdDet.GetFocusedRowCellValue("RATE")) != 0 && GrdDet.IsLastRow == true)
                {
                    DTabDetail.Rows.Add(DTabDetail.NewRow());
                    GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    GrdDet.FocusedColumn = GrdDet.Columns["ASSORTMENTDATE"];
                }
                else
                {
                    BtnSave.Focus();
                    e.Handled = true;
                }
            }
        }

        private void RepTxtAmount_Validating(object sender, CancelEventArgs e)
        {
            try
            {

                //double pDouWeight = 0;
                //double pDouRate = 0;
                //double pDouAmt = 0;

                //GrdDet.PostEditor();

                //for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                //{
                //    DataRow DRow = GrdDet.GetDataRow(IntI);
                //    pDouWeight = Val.Val(DRow["WEIGHT"]);
                //    pDouRate = Val.Val(DRow["RATE"]);
                //    pDouAmt = Val.Val(DRow["AMOUNT"]);
                //}

                //pDouRate = Math.Round(pDouAmt / pDouWeight, 3);
                //double PDouRateFE = Math.Round(Val.Val(txtExcRate.Text) * pDouRate, 2);
                //double pDouAmtFE = Math.Round(Val.Val(txtExcRate.Text) * pDouAmt, 2);

                //GrdDet.SetFocusedRowCellValue("RATE", pDouRate);
                //GrdDet.SetFocusedRowCellValue("FRATE", PDouRateFE);
                //GrdDet.SetFocusedRowCellValue("FAMOUNT", pDouAmtFE);

                //CalculateSummary();


            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void ReptxtWeight_Validating(object sender, CancelEventArgs e)
        {
            try
            {

            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouCarat = 0;
                    DouAmt = 0;
                    FDouAmt = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouCarat = DouCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "WEIGHT"));
                    DouAmt = DouAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "AMOUNT"));
                    FDouAmt = FDouAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "FAMOUNT"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouAmt) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FRATE") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(FDouAmt) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void kapanButSave_Click(object sender, EventArgs e)
        {
            //try
            //{
            //        this.Cursor = Cursors.WaitCursor;
            //        DtabKapan.DefaultView.Sort = "KAPANNAME";
            //        DtabKapan = DtabKapan.DefaultView.ToTable();

            //        string ReturnMessageDesc = "";
            //        string ReturnMessageType = "";

            //        foreach (DataRow Dr in DtabKapan.Rows)
            //        {

            //            if (Val.ToString(Dr["KAPANNAME"]).Trim().Equals(string.Empty) || Val.ToString(Dr["ENTRYDATE"]).Trim().Equals(string.Empty) || Val.Val(Dr["KAPANCARAT"]) <= 0)
            //                continue;

            //            TrnKapanCreateProperty Property = new TrnKapanCreateProperty();

            //            Property.KAPAN_ID = 0;

            //            Property.KAPANNAME = Val.Trim(Val.ToString(Dr["KAPANNAME"]));
            //            Property.KAPANGROUP = Val.Trim(Val.ToString(Dr["KAPANGROUP"]));
            //            Property.KAPANCATEGORY = Val.Trim(Val.ToString(Dr["KAPANCATEGORY"]));

            //            Property.MANAGER_ID = Val.Bigint(Dr["MANAGER_ID"]);

            //            //Property.LOT_ID = Val.ToInt64(Dr["LOT_ID"]);

            //            Property.KAPANPCS = Val.ToInt32(Dr["KAPANPCS"]);
            //            Property.KAPANCARAT = Val.Val(Dr["KAPANCARAT"]);

            //            Property.KAPANRATE = Val.Val(Dr["KAPANRATE"]);
            //            Property.KAPANAMOUNT = Val.Val(Dr["KAPANAMOUNT"]);

            //            Property.EXPMAKPER = Val.Val(Dr["EXPMAKPER"]);
            //            Property.EXPMAKCARAT = Val.Val(Dr["EXPMAKCARAT"]);
            //            Property.EXPPOLPER = Val.Val(Dr["EXPPOLPER"]);
            //            Property.EXPPOLCARAT = Val.Val(Dr["EXPPOLCARAT"]);
            //            Property.EXPDOLLAR = Val.Val(Dr["EXPDOLLAR"]);

            //            Property.LOTGROUP = Val.ToString(Dr["LOTGROUP"]);
            //            Property.DUEDAYS = Val.ToInt32(Dr["DUEDAYS"]);
            //            Property.EXPGIAPER = Val.Val(Dr["EXPGIAPER"]);

            //            Property.REMARK = Val.ToString(Dr["REMARK"]);

            //            Property.KAPANTYPE = Val.ToString(Dr["KAPANTYPE"]);

            //            Property.COMPARMEMO = Val.ToString(Dr["COMPARMEMO"]);
            //            Property.DIAMONDTYPE = Val.ToString(Dr["DIAMONDTYPE"]);

            //            Property = ObjKapan.Save(Property);

            //            ReturnMessageDesc = Property.ReturnMessageDesc;
            //            ReturnMessageType = Property.ReturnMessageType;


            //            Property = null;

            //            this.Cursor = Cursors.Default;

            //            Global.Message(ReturnMessageDesc);


            //            if (ReturnMessageType == "SUCCESS")
            //            {
            //                this.Close();
            //            }
            //            else
            //            {
            //                return;
            //            }
            //        }
            //        this.Cursor = Cursors.Default;
            //        //this.Cursor = Cursors.Default;

            //        //Global.Message(ReturnMessageDesc);


            //        //if (ReturnMessageType == "SUCCESS")
            //        //{
            //        //    this.Close();
            //        //}
            //        //else
            //        //{
            //        //    return;
            //        //}
            //    }

            //catch (Exception Ex)
            //{
            //    Global.Message(Ex.Message.ToString());
            //}
        }

        private void xtraTabControl2_Click(object sender, EventArgs e)
        {
            //DataRow DrKapan = DtabKapan.NewRow();
            //DtabKapan.Rows.Add(DrKapan);
            //MainKapanGrd.DataSource = DtabKapan;
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


        private void reptxtKapan_Validating(object sender, CancelEventArgs e)
        {

            if (GrdKapan.FocusedRowHandle < 0)
                return;

            try
            {
                GrdKapan.PostEditor();
                DataRow Dr = GrdKapan.GetFocusedDataRow();
                if (!Val.ToString(GrdKapan.EditingValue).Trim().Equals(string.Empty))
                {
                    if (CheckDuplicateKapan("KAPANNAME", Val.ToString(GrdKapan.EditingValue), GrdKapan.FocusedRowHandle, "KAPAN"))
                        e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reptxtRemarkKapan_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    DataRow dr = GrdKapan.GetFocusedDataRow();
                    if (!Val.ToString(dr["KAPANNAME"]).Equals(string.Empty))
                    {
                        DataRow DR = DtabKapan.NewRow();
                        // DtabKapan.Rows.Add(DR);
                        DtabKapan.Rows.Add(DtabKapan.NewRow());
                        DtabKapan.AcceptChanges();
                    }
                    else if (GrdKapan.IsLastRow)
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
                if (e.KeyCode == Keys.Enter)
                {
                    GrdKapan.PostEditor();
                    Calculatesummary();
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    //if (Val.Val(TotKapanCarat) != 0)
                    //{
                    //    //if (mFormType == FORMTYPE.ORIGINALKAPAN)
                    //    //{
                    //    //    Global.Message("Kapan Carat Is Greate Than Balance Carat.");
                    //    //    GrdKapan.SetFocusedRowCellValue("KAPANCARAT", 0);
                    //    //    GrdKapan.FocusedRowHandle = dr.Table.Rows.IndexOf(dr);
                    //    //    GrdKapan.FocusedColumn = GrdKapan.VisibleColumns[6];
                    //    //    GrdKapan.Focus();
                    //    //    e.Handled = true;

                    //    //}

                    //}

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
                        GrdKapan.SetFocusedRowCellValue("MANAGER_ID", Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]));
                        GrdKapan.SetFocusedRowCellValue("MANAGERNAME", Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]));
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

        private void BtnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnRejection_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //if (Val.Val(lblBalanceCarat.Text) == 0)
            //    //{
            //    //    this.Cursor = Cursors.Default;
            //    //    Global.MessageToster("NO BALANCE IS THERE");
            //    //    return;
            //    //}

            //    if (Global.Confirm("Are You Sure You Want To Transfer Rejection ? ") == System.Windows.Forms.DialogResult.No)
            //    {
            //        return;
            //    }

            //    this.Cursor = Cursors.WaitCursor;


            //    string ReturnMessageDesc = "";
            //    string ReturnMessageType = "";

            //    foreach (DataRow Dr in DtabRejection.Rows)
            //    {
            //        TRN_RejectionProperty Property = new TRN_RejectionProperty();
            //        if (Val.ToString(Dr["REJECTIONNAME"]).Trim().Equals(string.Empty) || Val.ToString(Dr["ENTRYDATE"]).Trim().Equals(string.Empty) || Val.Val(Dr["CARAT"]) <= 0)
            //            continue;

            //        Property.REJECTIONTRN_ID = 0;
            //        Property.REJECTIONDATE = Val.ToString(Val.SqlDate(Val.ToString(Dr["ENTRYDATE"])));
            //        Property.REJECTIONFROM = mStrRejectionFrom;

            //        Property.REJECTION_ID = Val.ToInt32(Dr["REJECTION_ID"]);

            //        //if (Property.REJECTIONFROM == "INVOICE")
            //        //{
            //        //    Property.LOT_ID = 0;
            //        //    Property.PARTYNAME = "";

            //        //    Property.KAPAN_ID = 0;
            //        //    Property.KAPANNAME = "";
            //        //    Property.PACKET_ID = 0;
            //        //    Property.PACKETNO = 0;
            //        //    Property.TAG = "";
            //        //}
            //        //else if (Property.REJECTIONFROM == "KAPAN")
            //        //{
            //        //    Property.LOT_ID = 0;
            //        //    Property.PARTYNAME = "";
            //        //    Property.KAPAN_ID =0;
            //        //    Property.KAPANNAME = "";
            //        //    Property.PACKET_ID = 0;
            //        //    Property.PACKETNO = 0;
            //        //    Property.TAG = "";
            //        //}

            //        Property.PCS = Val.ToInt32(Dr["PCS"]);
            //        Property.CARAT = Val.Val(Dr["CARAT"]);
            //        Property.RATE = Val.Val(Dr["RATE"]);
            //        Property.AMOUNT = Math.Round(Property.CARAT * Property.RATE, 2);
            //        Property.REMARK = Val.ToString(Dr["REMARK"]);

            //        Property = ObjRejection.Save(Property);
            //        ReturnMessageDesc = Property.ReturnMessageDesc;
            //        ReturnMessageType = Property.ReturnMessageType;
            //        Property = null;
            //    }

            //    if (ReturnMessageType == "SUCCESS")
            //    {
            //        Global.Message(ReturnMessageDesc);
            //       // this.Close();
            //    }
            //    else
            //    {
            //        Global.MessageError(ReturnMessageDesc);
            //    }
            //    this.Cursor = Cursors.Default;

            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message);
            //}
        }

        private void repRjKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "REJECTIONCODE,REJECTIONNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_REJECTION);

                    FrmSearch.mColumnsToHide = "REJECTION_ID";

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdRej.SetFocusedRowCellValue("REJECTION_ID", Val.ToString(FrmSearch.mDRow["REJECTION_ID"]));
                        GrdRej.SetFocusedRowCellValue("REJECTIONNAME", Val.ToString(FrmSearch.mDRow["REJECTIONNAME"]));
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

        private void RepRJRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    DataRow dr = GrdRej.GetFocusedDataRow();
                    if (!Val.ToString(dr["REJECTIONNAME"]).Equals(string.Empty) && Val.Val(dr["CARAT"]) > 0.00 && GrdDet.IsLastRow)
                    {
                        DtabRejection.Rows.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                        DtabRejection.Rows.Add(DtabRejection.NewRow());
                        DtabRejection.AcceptChanges();

                    }
                    else if (GrdRej.IsLastRow)
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

        private void GrdRej_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName.ToUpper() == "RATE" || e.Column.FieldName.ToUpper() == "CARAT")
                {
                    double DouCarat = Val.Val(GrdRej.GetFocusedRowCellValue("CARAT"));
                    double DouRate = Val.Val(GrdRej.GetFocusedRowCellValue("RATE"));

                    GrdRej.SetFocusedRowCellValue("AMOUNT", Math.Round(DouCarat * DouRate, 0));
                }



            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }
        }

        private void GrdRej_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try // K : 06/12/2022
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouRJCarat = 0;
                    DouRejectionAmount = 0;
                    DouRejectionCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouRJCarat = DouRJCarat + Val.Val(GrdRej.GetRowCellValue(e.RowHandle, "CARAT"));

                    DouRejectionAmount = DouRejectionAmount + Val.Val(GrdRej.GetRowCellValue(e.RowHandle, "AMOUNT"));
                    DouRejectionCarat = DouRejectionAmount / DouRJCarat;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(DouRJCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouRejectionAmount) / Val.Val(DouRJCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void GrdDetList_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = string.Empty;
            }
        }

        private void GrdDetList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                xtraTabControl1.SelectedTabPageIndex = 1;

                Int64 LotID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("LOT_ID"));
                string LotNo = Val.ToString(GrdDet.GetFocusedRowCellValue("LOTNO"));
                string SystemInvoiceNo = Val.ToString(GrdDet.GetFocusedRowCellValue("SYSTEMINVOICENO"));
                string ManualInvoiceNo = Val.ToString(GrdDet.GetFocusedRowCellValue("MANUALINVOICENO"));

                GrpCaption.Text = "TRACKING OF INVOICE : [ " + ManualInvoiceNo + " ] && LOT NO : [ " + LotNo + " ]";


                xtraTabKapan.Text = "     KAPAN ( 0 )     ";
                xtraTabRejection.Text = "     REJECTION ( 0 )     ";

                if (LotID != 0)
                {
                    this.Cursor = Cursors.WaitCursor;


                    GrdKapan.BeginUpdate();
                    GrdRej.BeginUpdate();
                    //GrdDetSale.BeginUpdate();

                    DataSet DS = ObjMast.GetLotIDTracking(LotID);

                    //MainGridMixSplit.DataSource = DS.Tables[0];
                    //MainGridMixSplit.Refresh();

                    MainKapanGrd.DataSource = DS.Tables[1];
                    MainKapanGrd.Refresh();

                    MainGrdRej.DataSource = DS.Tables[2];
                    MainGrdRej.Refresh();

                    //MainGridSale.DataSource = DS.Tables[3];
                    //MainGridSale.Refresh();

                    //GrdDetMixSplit.BestFitColumns();
                    GrdKapan.BestFitColumns();
                    GrdRej.BestFitColumns();

                    //GrdDetMixSplit.EndUpdate();
                    GrdKapan.EndUpdate();
                    GrdRej.EndUpdate();
                    //GrdDetSale.EndUpdate()

                    xtraTabKapan.Text = "     KAPAN ( " + DS.Tables[1].Rows.Count.ToString() + " )     ";
                    xtraTabRejection.Text = "     REJECTION ( " + DS.Tables[2].Rows.Count.ToString() + " )     ";

                    this.Cursor = Cursors.Default;

                }

            }
            catch (Exception Ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(Ex.Message.ToString());
            }
        }




    }
}
