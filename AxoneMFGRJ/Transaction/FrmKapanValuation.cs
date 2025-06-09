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
using DevExpress.XtraGrid.Views.BandedGrid;
using AxoneMFGRJ.Transaction;
using DevExpress.Data;

namespace AxoneMFGRJ.Masters
{
    public partial class FrmKapanValuation : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOFormPermission ObjPer = new BOFormPermission();

        System.Diagnostics.Stopwatch watch = null;
        BOTRN_SinglePacketCreate ObjKapan = new BOTRN_SinglePacketCreate();
        BOTRN_Rejection ObjRejection = new BOTRN_Rejection();
        BOTRN_KapanValuation ObjRough = new BOTRN_KapanValuation();
        DataTable Dtab = new DataTable();

        DataTable DtabRejection = new DataTable();
        string mStrRejectionFrom = string.Empty;
        
        double TotKapanCarat = 0;
        double DouCts = 0;
        double DouRejectionAmount = 0;
        double DouRejectionCarat = 0;
        double DouCarat = 0;
        double DouRate = 0;
        double DouAmt = 0;

        #region Property Settings

        public FrmKapanValuation()
        {
            InitializeComponent();
        }

        public void ShowForm(DataRow Dr)
        {
            if (Dr == null)
            {
                this.Show();
            }
            else
            {

                if (Val.IsDate( Val.ToString(Dr["POLISHRECVDATE"])) == true)
                {
                    DTPPolishRcvDate.Checked = true;
                    DTPPolishRcvDate.Value = DateTime.Parse(Dr["POLISHRECVDATE"].ToString());
                }
                else
                {
                    DTPPolishRcvDate.Checked = false;
                }

                if (Val.IsDate(Val.ToString(Dr["CLVCOMPLETEDATE"])) == true)
                {
                    DTPClvCompleteDate.Checked = true;
                    DTPClvCompleteDate.Value = DateTime.Parse(Dr["CLVCOMPLETEDATE"].ToString());
                }
                else
                {
                    DTPClvCompleteDate.Checked = false;
                }

                if (Val.IsDate(Val.ToString(Dr["MUMBAIRECVDATE"])) == true)
                {
                    DTPMunbaiRecvDate.Checked = true;
                    DTPMunbaiRecvDate.Value = DateTime.Parse(Dr["MUMBAIRECVDATE"].ToString());
                }
                else
                {
                    DTPMunbaiRecvDate.Checked = false;
                }
                if (Val.IsDate(Val.ToString(Dr["COMPLETEDATE"])) == true)
                {
                    DTPCompleteDate.Checked = true;
                    DTPCompleteDate.Value = DateTime.Parse(Dr["COMPLETEDATE"].ToString());
                }
                else
                {
                    DTPCompleteDate.Checked = false;
                }
                if (Val.IsDate(Val.ToString(Dr["MFGISSUEDATE"])) == true)
                {
                    DTPMfgIssueDate.Checked = true;
                    DTPMfgIssueDate.Value = DateTime.Parse(Dr["MFGISSUEDATE"].ToString());
                }
                else
                {
                    DTPMfgIssueDate.Checked = false;
                }
                if (Val.IsDate(Val.ToString(Dr["GHATCOMPLETEDATE"])) == true)
                {
                    DTPGhatCompleteDate.Checked = true;
                    DTPGhatCompleteDate.Value = DateTime.Parse(Dr["GHATCOMPLETEDATE"].ToString());
                }
                else
                {
                    DTPGhatCompleteDate.Checked = false;
                }

                txtLabRate.Text = Val.ToString(Dr["LABRATE"]);
                txtLotConversionRate.Text = Val.ToString(Dr["LOTCONVRATE"]);
                txtOtherExpenseAmt.Text = Val.ToString(Dr["OTHEREXPENSEAMT"]);
                txtOtherMFGExp.Text = Val.ToString(Dr["OTHERMFGEXPENSE"]);
                //txtPolishAvg.Text = Val.ToString(Dr["POLISHAVG"]);
                //txtPolishConversionRate.Text = Val.ToString(Dr["POLISHCONVRATE"]);
                txtLotConversionRate.Text = Val.ToString(Dr["LOTCONVRATE"]);

                DtabRejection.Columns.Add(new DataColumn("ENTRYDATE", typeof(DateTime)));
                DtabRejection.Columns.Add(new DataColumn("REJECTION_ID", typeof(int)));
                DtabRejection.Columns.Add(new DataColumn("REJECTIONTRN_ID", typeof(double)));
                DtabRejection.Columns.Add(new DataColumn("REJECTIONNAME", typeof(string)));
                DtabRejection.Columns.Add(new DataColumn("PCS", typeof(int)));
                DtabRejection.Columns.Add(new DataColumn("CARAT", typeof(double)));
                DtabRejection.Columns.Add(new DataColumn("RATE", typeof(double)));
                DtabRejection.Columns.Add(new DataColumn("AMOUNT", typeof(double)));
                DtabRejection.Columns.Add(new DataColumn("REMARK", typeof(string)));

                DtabRejection.Rows.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                // DtabKapan.Rows.Add(DtabKapan.NewRow());

                MainGrid.DataSource = DtabRejection;

                GrdDet.FocusedRowHandle = 0;
                GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
                GrdDet.Focus();

                MainGrid.RefreshDataSource();

                txtKapanName.Text = Dr["KAPANNAME"].ToString();
                txtKapanName.Tag = Dr["KAPAN_ID"].ToString();
                //lblKapanID.Text = Dr["KAPAN_ID"].ToString();
                //TxtRoughName.Text = kapanvalation.GetRoughName(Convert.ToInt32(pDabFrom.Rows[0]["LOT_ID"].ToString()));
                txtRoughName.Text = null;
                txtLabRate.Focus();
                this.Show();
            }
            
            
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            txtKapanName.Focus();
          

        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjKapan);
            ObjFormEvent.ObjToDisposeList.Add(ObjRough);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjPer.GetPermission(this);
        }

        #endregion

        public void ResetControl()
        {
            DTPPolishRcvDate.ResetText();
            DTPClvCompleteDate.ResetText(); 
            DTPMunbaiRecvDate.ResetText();
            DTPCompleteDate.ResetText();
            DTPMfgIssueDate.ResetText();
            DTPGhatCompleteDate.ResetText();
            DtpClvIssueDate.ResetText();
            txtLabRate.ResetText();
            txtLotConversionRate.ResetText();
            txtOtherExpenseAmt.ResetText();
            txtOtherMFGExp.ResetText();
            //txtPolishAvg.ResetText();
            //txtPolishConversionRate.ResetText();
            txtPolishReadyPcs.ResetText();
            txtPolishReadyCarat.ResetText();
            txtPolishIssuePcs.ResetText();
            txtPolishissueCarat.ResetText();
            txtQClabour.ResetText();
            txtSarinLabour.ResetText();
            txtGalaxyLabour.ResetText();
            txtGalaxyIssuePcs.ResetText();
            txtMKBLSize.ResetText();
            txtMKBLPer.ResetText();
            txtMKBLPcs.ResetText();
            txtMKBLCarat.ResetText();
            txtCLVWeightLoss.ResetText();
            txtRoughName.ResetText();
            txtRghCts.ResetText();
            txtKapanCarat.ResetText();
            txtKapanRate.ResetText();
            txtKapanAmtDollar.ResetText();
            txtKapanAmtRs.ResetText();
            txtExpPol.ResetText();
            txtExpMak.ResetText();
            txtExpDollar.ResetText();
            txtLotRemark.ResetText();
            txtExpMakCarat.ResetText();
            txtExpMakCarat.ResetText();
            //txtPolishAvgRs.ResetText();
            //txtSize.ResetText();
            txtKachaPcs.ResetText();
            txtGhatReceiveCrt.ResetText();
            txtKachaCarat.ResetText();
            txtPadtar.ResetText();
            txtPadtarAmt.ResetText();
            txtRejectionAmt.ResetText();
            txtRejectionAvg.ResetText();
            txtRejectionRateCts.ResetText();
            DtabRejection.Rows.Clear();
            txtKapantTotAmt.ResetText();
            txtPolishamt.ResetText();
            txtGalaxyAmt.ResetText();

            txtMumbaiAvg.ResetText();
            txtMumbaiCnv.ResetText();
            txtMumbaiAvgDollar.ResetText();
            txtPadtarAvg.ResetText();
            txtRejectionPcs.ResetText();
            txtRejectionRateCts.ResetText();
        }

        #region Button Event

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                KapanValuationProperty Property = new KapanValuationProperty();

                if (DTPGhatCompleteDate.Checked == true)
                {
                    Property.GHATCOMPLETEDATE = Val.SqlDate(DTPGhatCompleteDate.Value.ToShortDateString());
                }
                else
                {
                    Property.GHATCOMPLETEDATE = null;
                }

                if (DTPClvCompleteDate.Checked == true)
                {
                    Property.CLVCOMPLETEDATE = Val.SqlDate(DTPClvCompleteDate.Value.ToShortDateString());
                }
                else
                {
                    Property.CLVCOMPLETEDATE = null;
                }

                if (DTPCompleteDate.Checked == true)
                {
                    Property.COMPLETEDATE = Val.SqlDate(DTPCompleteDate.Value.ToShortDateString());
                }
                else
                {
                    Property.COMPLETEDATE = null;
                }
                if (DTPMfgIssueDate.Checked == true)
                {
                    Property.MFGISSUEDATE = Val.SqlDate(DTPMfgIssueDate.Value.ToShortDateString());
                }
                else
                {
                    Property.MFGISSUEDATE = null;
                }
                if (DTPMunbaiRecvDate.Checked == true)
                {
                    Property.MUMBAIRECVDATE = Val.SqlDate(DTPMunbaiRecvDate.Value.ToShortDateString());
                }
                else
                {
                    Property.MUMBAIRECVDATE = null;
                }
                if (DTPPolishRcvDate.Checked == true)
                {
                    Property.POLISHRECVDATE = Val.SqlDate(DTPPolishRcvDate.Value.ToShortDateString());
                }
                else
                {
                    Property.POLISHRECVDATE = null;
                }
                if (DtpClvIssueDate.Checked == true)
                {
                    Property.CLVISSUEDATE = Val.SqlDate(DtpClvIssueDate.Value.ToShortDateString());
                }
                else
                {
                    Property.CLVISSUEDATE = null;
                }

                Property.POLISHISSUEPCS = Val.ToInt(txtPolishIssuePcs.Text);
                Property.POLISHISSUECARAT = Val.Val(txtPolishissueCarat.Text);
                Property.POLISHREADYPCS = Val.ToInt(txtPolishReadyPcs.Text);
                Property.POLISHREADYCARAT = Val.Val(txtPolishReadyCarat.Text);
                Property.MKBLCARAT = Val.Val(txtMKBLCarat.Text);
                Property.MKBLPC = Val.ToInt(txtMKBLPcs.Text);
                Property.MKBLPER = Val.Val(txtMKBLPer.Text);
                Property.MKBLSIZE = Val.Val(txtMKBLSize.Text);
                Property.GALAXYISSUEPC = Val.ToInt(txtGalaxyIssuePcs.Text);
                Property.GALAXYLABOUR = Val.Val(txtGalaxyLabour.Text);
                Property.QCLABOUR = Val.Val(txtQClabour.Text);
                Property.SARINLABOUR = Val.Val(txtSarinLabour.Text);

                Property.LABRATE = Val.Val(txtLabRate.Text);
                Property.LOSTCONVERSIONRATE = Val.Val(txtLotConversionRate.Text);
                Property.KAPAN_ID = Val.ToInt64(txtKapanName.Tag);
                Property.OTHEREXPENSEAMT = Val.Val(txtOtherExpenseAmt.Text);
                Property.OTHERMFGEXP = Val.Val(txtOtherMFGExp.Text);
                //Property.POLISHAVG = Val.Val(txtPolishAvg.Text);
                //Property.POLISHCONVERSIONRATE = Val.Val(txtPolishConversionRate.Text);
                Property.CLVWEIGHTLOSS = Val.Val(txtCLVWeightLoss.Text);

                //Mayank Start
                Property.KAPANCARAT = Val.Val(txtKapanCarat.Text);
                Property.KAPANRATE = Val.Val(txtKapanRate.Text);
                Property.KAPANAMTRS = Val.Val(txtKapanAmtRs.Text);
                Property.KAPANAMTDOLLAR = Val.Val(txtKapanAmtDollar.Text);
                Property.EXPPOLPER = Val.Val(txtExpPol.Text);
                Property.EXPPOLCARAT = Val.Val(txtExpPolCarat.Text);
                Property.EXPMAKPER = Val.Val(txtExpMak.Text);
                Property.EXPMAKCARAT = Val.Val(txtExpMakCarat.Text);
                Property.EXPDOLLAR = Val.Val(txtExpDollar.Text);
                Property.REMARK = Val.ToString(txtLotRemark.Text);
                //Property.POLISHAVGRS = Val.Val(txtPolishAvgRs.Text);
                //Property.SIZE = Val.Val(txtSize.Text);
                Property.KACHAPCS = Val.ToInt(txtKachaPcs.Text);
                Property.GHATRECIEVECARAT = Val.Val(txtGhatReceiveCrt.Text);
                Property.KACHACARAT = Val.Val(txtKachaCarat.Text);
                Property.PADTAR = Val.Val(txtPadtar.Text);
                Property.PADTARAMT = Val.Val(txtPadtarAmt.Text);
                Property.OUTAMOUNT = Val.Val(txtRejectionAmt.Text);
                Property.OUTAVG = Val.Val(txtRejectionAvg.Text);
                Property.OUTCARAT = Val.Val(txtRejectionRateCts.Text);
                Property.OUTPCS = Val.ToInt(txtRejectionPcs.Text);
                Property.OUTREJECTIONCARAT = Val.Val(txtRejectionCts.Text);
                //TOTALAMOUNTS
                Property.KAPANTOTALAMOUNT = Val.Val(txtKapantTotAmt.Text);
                Property.POLISHAMOUNT = Val.Val(txtPolishamt.Text);
                Property.GALAXYAMOUNT = Val.Val(txtGalaxyAmt.Text);
                
                //MUMBAI PADTAR DETAIL
                Property.MUMBAIAVG = Val.Val(txtMumbaiAvg.Text);
                Property.MUMBAICNVRATE = Val.Val(txtMumbaiCnv.Text);
                Property.MUMBAIAMOUNT = Val.Val(txtMumbaiAvgDollar.Text);
                Property.PADTARAVG = Val.Val(txtPadtarAvg.Text);
                

                Property = ObjRough.UpdateKapan(Property);

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                if (DtabRejection.Rows.Count > 0)
                {
                    foreach (DataRow Dr in DtabRejection.Rows)
                    {

                        TRN_RejectionProperty Property1 = new TRN_RejectionProperty();
                        if (Val.ToString(Dr["REJECTIONNAME"]).Trim().Equals(string.Empty) || Val.Val(Dr["CARAT"]) <= 0)
                            continue;
                        Property1.LOT_ID = 0;
                        Property1.PARTYNAME = "";
                        Property1.KAPAN_ID = Val.ToInt64(txtKapanName.Tag);
                        Property1.KAPANNAME = Val.ToString(txtKapanName.Text);
                        Property1.PACKET_ID = 0;
                        Property1.PACKETNO = 0;
                        Property1.TAG = "";

                        Property1.PCS = Val.ToInt32(Dr["PCS"]);
                        Property1.REJECTION_ID = Val.ToInt32(Dr["REJECTION_ID"]);
                        Property1.REJECTIONTRN_ID = Val.ToInt64(Dr["REJECTIONTRN_ID"]);

                        Property1.CARAT = Val.Val(Dr["CARAT"]);
                        Property1.RATE = Val.Val(Dr["RATE"]);
                        Property1.AMOUNT = Math.Round(Property1.CARAT * Property1.RATE, 2);
                        Property1.REMARK = "";
                        Property1.REJECTIONDATE = Val.SqlDate(DateTime.Now.ToShortDateString());
                        Property1.REJECTIONFROM = "KAPAN";

                        Property1 = ObjRejection.KapanSave(Property1);
                        ReturnMessageDesc = Property1.ReturnMessageDesc;
                        ReturnMessageType = Property1.ReturnMessageType;
                        if (ReturnMessageType != "SUCCESS")
                        {
                            Global.MessageError(ReturnMessageDesc);
                            return;
                        }
                        Property = null;
                    }
                }

                Global.Message("Kapan Data Updated Successfully");

                ResetControl();
                txtKapanName.Focus();

            }
            catch (System.Exception ex)
            {
                //Global.Message(ex);

            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                watch = System.Diagnostics.Stopwatch.StartNew();
                btnRefresh.Enabled = false;

                PanelProgress.Visible = true;
                lblTime.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Dtab = ObjRough.GetKapanPolishData(Val.ToString(txtKapanName.Tag));
            }
            catch (Exception ex)
            {

                btnRefresh.Enabled = true;
                PanelProgress.Visible = false;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                btnRefresh.Enabled = true;
                PanelProgress.Visible = false;

                txtLabRate.Text = Val.ToString(Dtab.Rows[0]["LABRATE"]);
                txtLotConversionRate.Text = Val.ToString(Dtab.Rows[0]["LOTCONVRATE"]);
                txtOtherExpenseAmt.Text = Val.ToString(Dtab.Rows[0]["OTHEREXPENSEAMT"]);
                txtOtherMFGExp.Text = Val.ToString(Dtab.Rows[0]["OTHERMFGEXPENSE"]);
                //txtPolishAvg.Text = Val.ToString(Dtab.Rows[0]["POLISHAVG"]);
                //txtPolishConversionRate.`Text = Val.ToString(Dtab.Rows[0]["POLISHCONVRATE"]);
                txtLotConversionRate.Text = Val.ToString(Dtab.Rows[0]["LOTCONVRATE"]);

                txtPolishissueCarat.Text = Val.ToString(Dtab.Rows[0]["POLISSUECARAT"]);
                txtPolishIssuePcs.Text = Val.ToString(Dtab.Rows[0]["POLISSUEPCS"]);
                txtPolishReadyCarat.Text = Val.ToString(Dtab.Rows[0]["POLRECEIVECARAT"]);
                txtPolishReadyPcs.Text = Val.ToString(Dtab.Rows[0]["POLRECEIVEPCS"]);
                txtMKBLPcs.Text = Val.ToString(Dtab.Rows[0]["MAKPCS"]);
                txtMKBLCarat.Text = Val.ToString(Dtab.Rows[0]["MAKCARAT"]);
                txtMKBLPer.Text = Val.ToString(Dtab.Rows[0]["MAKPER"]);
                txtMKBLSize.Text = Val.ToString(Dtab.Rows[0]["MAKSIZE"]);
                txtGalaxyIssuePcs.Text = Val.ToString(Dtab.Rows[0]["GALAXYPCS"]);
                txtGalaxyLabour.Text = Val.ToString(Dtab.Rows[0]["GALAXYLABOUR"]);
                txtQClabour.Text = Val.ToString(Dtab.Rows[0]["QCLABOUR"]);
                txtSarinLabour.Text = Val.ToString(Dtab.Rows[0]["SARINLABOUR"]);
                txtCLVWeightLoss.Text = Val.ToString(Dtab.Rows[0]["CLVWEIGHTLOSS"]);
                txtRoughName.Text = Val.ToString(Dtab.Rows[0]["ROUGHNAME"]);
                txtRghCts.Text = Val.ToString(Dtab.Rows[0]["TOTALCARAT"]);
                txtRejectionRateCts.Text = Val.ToString(Dtab.Rows[0]["REJECTIONCARAT"]);
                txtKachaPcs.Text = Val.ToString(Dtab.Rows[0]["KACHAPCS"]);


                if (Val.IsDate(Val.ToString(Dtab.Rows[0]["POLISHRECVDATE"])) == true)
                {
                    DTPPolishRcvDate.Checked = true;
                    DTPPolishRcvDate.Value = DateTime.Parse(Dtab.Rows[0]["POLISHRECVDATE"].ToString());
                }
                else
                {
                    DTPPolishRcvDate.Checked = false;
                }

                if (Val.IsDate(Val.ToString(Dtab.Rows[0]["CLVCOMPLETEDATE"])) == true)
                {
                    DTPClvCompleteDate.Checked = true;
                    DTPClvCompleteDate.Value = DateTime.Parse(Dtab.Rows[0]["CLVCOMPLETEDATE"].ToString());
                }
                else
                {
                    DTPClvCompleteDate.Checked = false;
                }

                if (Val.IsDate(Val.ToString(Dtab.Rows[0]["MUMBAIRECVDATE"])) == true)
                {
                    DTPMunbaiRecvDate.Checked = true;
                    DTPMunbaiRecvDate.Value = DateTime.Parse(Dtab.Rows[0]["MUMBAIRECVDATE"].ToString());
                }
                else
                {
                    DTPMunbaiRecvDate.Checked = false;
                }

                if (Val.IsDate(Val.ToString(Dtab.Rows[0]["COMPLETEDATE"])) == true)
                {
                    DTPCompleteDate.Checked = true;
                    DTPCompleteDate.Value = DateTime.Parse(Dtab.Rows[0]["COMPLETEDATE"].ToString());
                }
                else
                {
                    DTPCompleteDate.Checked = false;
                }
                if (Val.IsDate(Val.ToString(Dtab.Rows[0]["MFGISSUEDATE"])) == true)
                {
                    DTPMfgIssueDate.Checked = true;
                    DTPMfgIssueDate.Value = DateTime.Parse(Dtab.Rows[0]["MFGISSUEDATE"].ToString());
                }
                else
                {
                    DTPMfgIssueDate.Checked = false;
                }
                if (Val.IsDate(Val.ToString(Dtab.Rows[0]["GHATCOMPLETEDATE"])) == true)
                {
                    DTPGhatCompleteDate.Checked = true;
                    DTPGhatCompleteDate.Value = DateTime.Parse(Dtab.Rows[0]["GHATCOMPLETEDATE"].ToString());
                }
                else
                {
                    DTPGhatCompleteDate.Checked = false;
                }
                //Calculate();
                txtClvrCode.Focus();
                watch.Stop();
                lblTime.Text = string.Format("{0:hh\\:mm\\:ss}", watch.Elapsed);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet DS = ObjRough.GetKapanValudationExistingData(Val.ToString(txtKapanName.Tag));

                DataRow DRow = DS.Tables[0].Rows[0];

                // Text Box Value Assign
                txtClvrCode.Text = DRow["CLEVERCODE"].ToString();
                txtClvrName.Text = DRow["CLEVERNAME"].ToString();

                txtKapanName.Text = DRow["KAPANNAME"].ToString();
                txtKapanCarat.Text = DRow["KAPANCARAT"].ToString();
                txtExpDollar.Text = DRow["EXPDOLLAR"].ToString();
                txtExpMak.Text = DRow["EXPMAKPER"].ToString();
                txtCLVWeightLoss.Text = DRow["CLVWEIGHTLOSS"].ToString();
                txtExpMakCarat.Text = DRow["EXPMAKCARAT"].ToString();
                txtExpPol.Text = DRow["EXPPOLPER"].ToString();
                txtExpPolCarat.Text = DRow["EXPPOLCARAT"].ToString();
                txtGalaxyIssuePcs.Text = DRow["GALAXYISSUEPCS"].ToString();
                txtGalaxyLabour.Text = DRow["GALAXYLABOUR"].ToString();
                txtGhatReceiveCrt.Text = DRow["GHATRECEIVECARAT"].ToString();
                txtKachaCarat.Text = DRow["KACHACARAT"].ToString();
                txtKachaPcs.Text = DRow["KACHAPCS"].ToString();
                txtKapanAmtDollar.Text = DRow["KAPANAMOUNT"].ToString();
                txtKapanAmtRs.Text = DRow["KAPANAMOUNTRS"].ToString();
                txtKapanRate.Text = DRow["KAPANRATE"].ToString();
                txtLabRate.Text = DRow["LABRATE"].ToString();
                txtLotConversionRate.Text = DRow["LOTCONVRATE"].ToString();
                txtLotRemark.Text = DRow["REMARK"].ToString();
                txtMKBLCarat.Text = DRow["MKBLCARAT"].ToString();
                txtMKBLPcs.Text = DRow["MKBLPCS"].ToString();
                txtMKBLPer.Text = DRow["MKBLPER"].ToString();
                txtMKBLSize.Text = DRow["MKBLSIZE"].ToString();
                txtOtherExpenseAmt.Text = DRow["OTHEREXPENSEAMT"].ToString();
                txtOtherMFGExp.Text = DRow["OTHERMFGEXPENSE"].ToString();
                txtPadtar.Text = DRow["PADTAR"].ToString();
                txtPadtarAmt.Text = DRow["PADTARAMOUNT"].ToString();
                //txtPolishAvg.Text = DRow["POLISHAVG"].ToString();
                //txtPolishAvgRs.Text = DRow["POLISHAVGRS"].ToString();
                //txtPolishConversionRate.Text = DRow["POLISHCONVRATE"].ToString();
                txtPolishissueCarat.Text = DRow["POLISHISSUECARAT"].ToString();
                txtPolishIssuePcs.Text = DRow["POLISHISSUEPCS"].ToString();
                txtPolishReadyCarat.Text = DRow["POLISHREADYCARAT"].ToString();
                txtPolishReadyPcs.Text = DRow["POLISHREADYPCS"].ToString();
                txtQClabour.Text = DRow["QCLABOUR"].ToString();
                txtKapantTotAmt.Text = DRow["KAPANTOTALAMOUNT"].ToString();
                txtPolishamt.Text = DRow["POLISHAMOUNT"].ToString();
                txtGalaxyAmt.Text = DRow["GALAXYAMOUNT"].ToString();

                txtMumbaiAvg.Text = DRow["MUMBAIAVG"].ToString();
                txtMumbaiCnv.Text = DRow["MUMBAICNVRATE"].ToString();
                txtMumbaiAvgDollar.Text = DRow["MUMBAIAMOUNT"].ToString();
                txtPadtarAvg.Text = DRow["PADTARAVG"].ToString();
                
                txtSarinLabour.Text = DRow["SARINLABOUR"].ToString();
                if (Val.IsDate(Val.ToString(DRow["POLISHRECVDATE"])) == true)
                {
                    DTPPolishRcvDate.Checked = true;
                    DTPPolishRcvDate.Value = DateTime.Parse(DRow["POLISHRECVDATE"].ToString());
                }
                else
                {
                    DTPPolishRcvDate.Checked = false;
                }
                if (Val.IsDate(Val.ToString(DRow["CLVISSUEDATE"])) == true)
                {
                    DtpClvIssueDate.Checked = true;
                    DtpClvIssueDate.Value = DateTime.Parse(DRow["CLVISSUEDATE"].ToString());
                }
                else
                {
                    DtpClvIssueDate.Checked = false;
                }
                if (Val.IsDate(Val.ToString(DRow["CLVCOMPLETEDATE"])) == true)
                {
                    DTPClvCompleteDate.Checked = true;
                    DTPClvCompleteDate.Value = DateTime.Parse(DRow["CLVCOMPLETEDATE"].ToString());
                }
                else
                {
                    DTPClvCompleteDate.Checked = false;
                }

                if (Val.IsDate(Val.ToString(DRow["MUMBAIRECVDATE"])) == true)
                {
                    DTPMunbaiRecvDate.Checked = true;
                    DTPMunbaiRecvDate.Value = DateTime.Parse(DRow["MUMBAIRECVDATE"].ToString());
                }
                else
                {
                    DTPMunbaiRecvDate.Checked = false;
                }

                if (Val.IsDate(Val.ToString(DRow["COMPLETEDATE"])) == true)
                {
                    DTPCompleteDate.Checked = true;
                    DTPCompleteDate.Value = DateTime.Parse(DRow["COMPLETEDATE"].ToString());
                }
                else
                {
                    DTPCompleteDate.Checked = false;
                }
                if (Val.IsDate(Val.ToString(DRow["MFGISSUEDATE"])) == true)
                {
                    DTPMfgIssueDate.Checked = true;
                    DTPMfgIssueDate.Value = DateTime.Parse(DRow["MFGISSUEDATE"].ToString());
                }
                else
                {
                    DTPMfgIssueDate.Checked = false;
                }
                if (Val.IsDate(Val.ToString(DRow["GHATCOMPLETEDATE"])) == true)
                {
                    DTPGhatCompleteDate.Checked = true;
                    DTPGhatCompleteDate.Value = DateTime.Parse(DRow["GHATCOMPLETEDATE"].ToString());
                }
                else
                {
                    DTPGhatCompleteDate.Checked = false;
                }

                
                DtabRejection = DS.Tables[1].Copy();
                MainGrid.DataSource = DtabRejection;
                MainGrid.Refresh();
                Calculatesummary();
                //txtClvrCode.Focus();
                //btnRefresh.Focus();
                //txtKapanCarat.Focus();
                //Calculate();


            }
            catch (Exception ex)
            {

                btnRefresh.Enabled = true;
                PanelProgress.Visible = false;
                Global.Message(ex.Message.ToString());
            }
        }

        private void reptxtKapan_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("REJECTION_ID", Val.ToString(FrmSearch.mDRow["REJECTION_ID"]));
                        GrdDet.SetFocusedRowCellValue("REJECTIONNAME", Val.ToString(FrmSearch.mDRow["REJECTIONNAME"]));
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

        private void reptxtCarat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GrdDet.PostEditor();
                    TotKapanCarat = 0;
                    DataRow dr = GrdDet.GetFocusedDataRow();
                    foreach (DataRow Drow in DtabRejection.Rows)
                    {
                        TotKapanCarat = TotKapanCarat + Val.Val(Drow["CARAT"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void reptxtRate_Leave(object sender, EventArgs e)
        {
            //DtabRejection.Rows.Add(DtabRejection.NewRow());
            DtabRejection.AcceptChanges();
            Calculatesummary();
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
                    FrmSearch.mDTab = ObjKapan.FindKapan();
                    FrmSearch.mColumnsToHide = "KAPAN_ID,MANAGER_ID,MANAGERNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);
                    }
                    else
                    {
                        txtKapanName.Text = Val.ToString(DBNull.Value);
                        txtKapanName.Tag = Val.ToString(DBNull.Value);
                    }
                    
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                    //txtKapanCarat.Focus();
                }
                BtnShow_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        #endregion
       
        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjPer.Password != "" && ObjPer.Password.ToUpper() == txtPassForDisplayBack.Text.ToUpper())
                {
                    txtPolishIssuePcs.Enabled = true;
                    txtPolishissueCarat.Enabled = true;
                    txtPolishReadyPcs.Enabled = true;
                    txtPolishReadyCarat.Enabled = true;
                    txtMKBLPcs.Enabled = true;
                    txtMKBLCarat.Enabled = true;
                    txtMKBLPer.Enabled = true;
                    txtMKBLSize.Enabled = true;
                    txtCLVWeightLoss.Enabled = true;
                    txtGalaxyIssuePcs.Enabled = true;
                    txtGalaxyLabour.Enabled = true;
                    txtQClabour.Enabled = true;
                    txtSarinLabour.Enabled = true;
                    txtRejectionRateCts.Enabled = true;
                }
                else
                {
                    txtPolishIssuePcs.Enabled = false;
                    txtPolishissueCarat.Enabled = false;
                    txtPolishReadyPcs.Enabled = false;
                    txtPolishReadyCarat.Enabled = false;
                    txtMKBLPcs.Enabled = false;
                    txtMKBLCarat.Enabled = false;
                    txtMKBLPer.Enabled = false;
                    txtMKBLSize.Enabled = false;
                    txtCLVWeightLoss.Enabled = false;
                    txtGalaxyIssuePcs.Enabled = false;
                    txtGalaxyLabour.Enabled = false;
                    txtQClabour.Enabled = false;
                    txtSarinLabour.Enabled = false;
                    txtRejectionRateCts.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            } 
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName.ToUpper() == "RATE" || e.Column.FieldName.ToUpper() == "CARAT")
                {
                    DouCts = Val.Val(GrdDet.GetFocusedRowCellValue("CARAT"));
                    DouRate = Val.Val(GrdDet.GetFocusedRowCellValue("RATE"));
                    DouAmt = Math.Round(DouCts * DouRate, 0);
                    GrdDet.SetFocusedRowCellValue("AMOUNT", DouAmt);

                    Calculatesummary();
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }
        }

        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try 
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouCarat = 0;
                    DouRejectionAmount = 0;
                    DouRejectionCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouCarat = DouCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "CARAT"));

                    DouRejectionAmount = Math.Round(DouRejectionAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "AMOUNT")),2);
                    DouRejectionCarat = DouRejectionAmount / DouCarat;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouRejectionAmount) / Val.Val(DouCarat), 2);
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

        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                GrdDet.FocusedRowHandle = GrdDet.FocusedRowHandle - 1;
                //GrdDet.FocusedRowHandle = GrdDet.FocusedRowHandle - 1;
            }
            if (e.KeyCode == Keys.Escape)
            {
                txtRejectionPcs.Focus();
            }
        }
       
        #region Calculation Validated 

        private void txtExpMak_Validated(object sender, EventArgs e)
        {
            txtExpMakCarat.Text = ((Val.Val(txtKapanCarat.Text) * Val.Val(txtExpMak.Text)) / 100).ToString();
            //Calculate();
        }

        private void txtExpPol_Validated(object sender, EventArgs e)
        {
            txtExpPolCarat.Text = ((Val.Val(txtKapanCarat.Text) * Val.Val(txtExpPol.Text)) / 100).ToString();
            Calculate();
        }

        private void txtLotConversionRate_Validated(object sender, EventArgs e)
        {
            txtKapanAmtRs.Text = Val.ToString(Math.Round((Val.Val(txtKapanRate.Text) * Val.Val(txtLotConversionRate.Text))));
            txtKapantTotAmt.Text = Val.ToString(Math.Round((Val.Val(txtKapanAmtRs.Text) * Val.Val(txtKapanCarat.Text))));
            Calculate();
            //txtGrossAmt.Text = 
        }

        private void txtPolishConversionRate_Validated(object sender, EventArgs e)
        {
            //txtPolishAvgRs.Text = Val.ToString(Math.Round((Val.Val(txtPolishConversionRate.Text) * Val.Val(txtPolishAvg.Text))));
            Calculate();
        }

        private void txtKapanCarat_Validated(object sender, EventArgs e)
        {
            txtExpMakCarat.Text = Val.ToString(Math.Round(((Val.Val(txtKapanRate.Text) * Val.Val(txtExpMak.Text)) / 100), 2));
            txtExpPolCarat.Text = Val.ToString(Math.Round(((Val.Val(txtKapanRate.Text) * Val.Val(txtExpPol.Text)) / 100), 2));
            txtKapanAmtRs.Text = Val.ToString(Math.Round((Val.Val(txtKapanRate.Text) * Val.Val(txtLotConversionRate.Text))));
            Calculate();
        }

        private void txtKapanRate_Validated(object sender, EventArgs e)
        {
            txtKapanAmtRs.Text = Val.ToString(Math.Round((Val.Val(txtKapanRate.Text) * Val.Val(txtLotConversionRate.Text))));
            Calculate();
        }

        private void txtLabRate_Validated(object sender, EventArgs e)
        {
            txtPolishamt.Text = Val.ToString(Math.Round((Val.Val(txtPolishReadyPcs.Text) * Val.Val(txtLabRate.Text)))); 
            Calculate();
        }

        private void txtGalaxyLabour_Validated(object sender, EventArgs e)
        {
            txtGalaxyAmt.Text = Val.ToString(Math.Round((Val.Val(txtGalaxyIssuePcs.Text) * Val.Val(txtGalaxyLabour.Text))));
            //double pDouGalaxyLabour = 0;
            //pDouGalaxyLabour = 22;
            //if (Val.Val(txtGalaxyLabour.Text) != 22)
            //{
            //    txtGalaxyAmt.Text = Val.ToString(Math.Round((Val.Val(txtGalaxyIssuePcs.Text) * Val.Val(txtGalaxyLabour.Text))));
            //}
            //else
            //{
            //    txtGalaxyAmt.Text = Val.ToString(Math.Round((Val.Val(txtGalaxyIssuePcs.Text) * pDouGalaxyLabour)));
            //}
        }

        public void Calculatesummary()
        {
            double TotOutAmount = 0;
            double TotWitoutOutCarat = 0;
            double TotOutAvg = 0;
            double TotOutCarat = 0;
            double TotOutPcs = 0;
            //double TotOutRate = 0;

            GrdDet.PostEditor();

            foreach (DataRow Drow in DtabRejection.Rows)
            { 
                 
                if (Val.Val(Drow["RATE"].ToString()) == 0)
                {
                    TotWitoutOutCarat = TotWitoutOutCarat + Val.Val(Drow["CARAT"].ToString());
                    TotOutPcs = TotOutPcs + Val.ToInt(Drow["PCS"].ToString());
                }
                else
                {
                    TotOutCarat = TotOutCarat + Val.Val(Drow["CARAT"].ToString());
                    TotOutAmount = Math.Round(TotOutAmount + Val.Val(Drow["AMOUNT"].ToString()));
                }
            }
            TotOutAvg = TotOutCarat == 0 ? 0 : (TotOutAmount / TotOutCarat);
            txtRejectionAmt.Text = TotOutAmount.ToString();
            txtRejectionAvg.Text = TotOutAvg.ToString();
            txtRejectionCts.Text = TotWitoutOutCarat.ToString();
            txtRejectionRateCts.Text = TotOutCarat.ToString();
            txtRejectionPcs.Text = TotOutPcs.ToString();
            Calculate();
        }

        public void Calculate()
        {
            double TotKapanAmt = 0;
            double TotalPolishAmt = 0;
            double TotalRejectionAmt = 0;
            double TotalGalaxyAmt = 0;
            //double TotOutPcs = 0;
            //double TotOutRate = 0;

            TotKapanAmt = Val.Val(txtKapantTotAmt.Text);
            TotalPolishAmt = Val.Val(txtPolishamt.Text);
            TotalGalaxyAmt = Val.Val(txtGalaxyAmt.Text);
            TotalRejectionAmt = Val.Val(txtRejectionAmt.Text);

            txtPadtarAmt.Text = Val.ToString(Math.Round(TotKapanAmt + TotalPolishAmt + TotalGalaxyAmt - TotalRejectionAmt));

            //TotKapanAmt = Val.Val(txtKapanAmtRs.Text);    
      
            txtPadtar.Text = Val.ToString(Math.Round(Val.Val(txtPadtarAmt.Text) / Val.Val(txtPolishReadyCarat.Text)));
            double DouTotalRejection = Val.Val(DtabRejection.Compute("SUM(CARAT)", ""));

            if (Val.Val(txtCLVWeightLoss.Text) != 0)
            {
                //common on 27/01/2023 by vipul
               // txtMKBLCarat.Text = Val.ToString(Val.Val(txtKapanCarat.Text) - Val.Val(txtRejectionRateCts.Text) - Val.Val(txtCLVWeightLoss.Text));

                // discuss with hiteshbhai consider total rejection weight with n without rate
                txtMKBLCarat.Text = Val.ToString(Val.Val(txtKapanCarat.Text) - DouTotalRejection - Val.Val(txtCLVWeightLoss.Text));
               
                txtMKBLSize.Text = Val.ToString(Val.Val(txtMKBLPcs.Text) / Val.Val(txtMKBLCarat.Text));
                txtMKBLPer.Text = Val.ToString((Val.Val(txtMKBLCarat.Text) / Val.Val(txtKapanCarat.Text)) * 100);
                txtKachaPcs.Text = Val.ToString(Val.Val(txtMKBLPcs.Text) - Val.Val(txtPolishReadyPcs.Text));
            }
            //Calculatesummary();
        }
        
        #endregion
        
        private void txtMumbaiCnv_Validated(object sender, EventArgs e)
        {
            txtMumbaiAvgDollar.Text = Val.ToString(Math.Round(Val.Val(txtMumbaiAvg.Text) / Val.Val(txtMumbaiCnv.Text)));
            btnUpdate.Focus();
        }

        private void txtMumbaiAvg_Validated(object sender, EventArgs e)
        {
            txtPadtarAvg.Text = Val.ToString(Math.Round(Val.Val(txtMumbaiAvg.Text) - Val.Val(txtPadtar.Text)));

        }

        private void txtCLVWeightLoss_Validated(object sender, EventArgs e)
        {
            Calculate();
            txtMKBLCarat.Focus();
        }

        private void txtMKBLPcs_Validated(object sender, EventArgs e)
        {
            Calculate();
        }
                       
        private void txtLotRemark_Validated(object sender, EventArgs e)
        {
            GrdDet.Focus();
            GrdDet.FocusedRowHandle = 0;
            GrdDet.FocusedColumn = GrdDet.Columns["CARAT"];
        }

    }
}
