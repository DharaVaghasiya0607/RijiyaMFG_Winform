using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using System.IO;
using BusLib.Rapaport;
using AxoneMFGRJ.Utility;
using AxonDataLib;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using System.Linq;
using System.Collections;
using AxoneMFGRJ.Masters;

namespace AxoneMFGRJ.Grading
{
    public partial class FrmSinglePacketPolishChkGradingEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOFindRap ObjRap = new BOFindRap();

        DataTable DTabPrdType = new DataTable();
        DataTable DTabRapDate = new DataTable();
        Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

        DataTable DtabLabEmp = new DataTable();

        EmployeeActionRightsProperty EmployeeRightsProperty = new EmployeeActionRightsProperty();
        string mStrReportNo = "";
        //#K : 24-11-2020
        IList<DataStructureGrading> DataLAB = new BindingList<DataStructureGrading>();

        bool mISTFlag = false;

        #region Constructor

        public FrmSinglePacketPolishChkGradingEntry()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();

            BtnClear_Click(null, null);

            EmployeeRightsProperty = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
            txtEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;

            DTabPrdType = ObjRap.GetPreditctionType(EmployeeRightsProperty.PRDTYPE_ID);
            CmbPrdType.Items.Clear();
            foreach (DataRow DRow in DTabPrdType.Rows)
            {
                if (Val.ToInt(DRow["PRDTYPE_ID"]) == 6 || Val.ToInt(DRow["PRDTYPE_ID"]) == 7)
                // || Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9 || Val.ToInt(DRow["PRDTYPE_ID"]) == 11)
                {
                    CmbPrdType.Items.Add(Val.ToString(DRow["PRDTYPENAME"]));
                }

            }
            CmbPrdType.SelectedIndex = 0;

            DTabRapDate = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.RAPDATE);
            DTabRapDate.DefaultView.Sort = "RAPDATE DESC";
            DTabRapDate = DTabRapDate.DefaultView.ToTable();

            CmbRapDate.Items.Clear();
            foreach (DataRow DRow in DTabRapDate.Rows)
            {
                CmbRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }
            CmbRapDate.SelectedIndex = 0;

            DtabLabEmp.Columns.Add("EMPLOYEE_ID", typeof(Int64));
            DtabLabEmp.Columns.Add("EMPLOYEENAME", typeof(string));
            DtabLabEmp.Columns.Add("EMPLOYEECODE", typeof(string));

            DataRow DrLabEmp = DtabLabEmp.NewRow();
            DtabLabEmp.Rows.Add("-2", "GIA", "GIA");
            DtabLabEmp.Rows.Add("-3", "IGI", "IGI");

            //#K: 24-11-2020
            DataTable DTabParameter = ObjRap.GetAllParameterTable();
            DataRow[] UDRow = DTabParameter.Select("ParaType = 'LAB'");
            DataTable DTab = UDRow.CopyToDataTable();
            DTab.DefaultView.Sort = "SequenceNo";
            DTab = DTab.DefaultView.ToTable();
            DataLAB.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
            foreach (DataRow DRow in DTab.Rows)
            {
                DataLAB.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
            }
            CmbLab.AccessibleDescription = "LAB";
            CmbLab.DataSource = DataLAB;
            CmbLab.DisplayMember = "PARACODE";
            CmbLab.ValueMember = "PARA_ID";
            //#K : 24-11-2020

            //DtabLabEmp.Rows.Add("-1", "BOMBAY", "BOMBAY");
            //DtabLabEmp.Rows.Add("-4", "HRD", "HRD");
            //DtabLabEmp.Rows.Add("-5", "SALES", "SALES");

        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);

            ObjFormEvent.ObjToDisposeList.Add(ObjRap);
            ObjFormEvent.ObjToDisposeList.Add(DTabPrdType);
            ObjFormEvent.ObjToDisposeList.Add(DTabRapDate);

            ObjFormEvent.ObjToDisposeList.Add(clsFindRap);
        }

        #endregion

        #region Events

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(CmbPrdType.Tag) == 0)
                {
                    Global.MessageError("Prediction Type Is Required");
                    return;
                }

                if (txtKapanName.Text.Trim().Length == 0)
                {
                    Global.MessageError("Kapan Name Is Required");
                    txtKapanName.Focus();
                    return;
                }
                if (Val.Val(txtPacketNo.Text) == 0)
                {
                    Global.MessageError("Packet No Is Required");
                    txtPacketNo.Focus();
                    return;
                }
                if (txtTag.Text.Trim().Length == 0)
                {
                    Global.MessageError("Tag Is Required");
                    txtTag.Focus();
                    return;
                }

                if (Val.ToString(txtTag.Tag).Length == 0)
                {
                    Global.MessageError("Packet ID Not Found In this PacketNo");
                    txtTag.Focus();
                    return;
                }

                if (Val.ToInt(CmbPrdType.Tag) == 8 && CmbLabProcess.SelectedIndex == 0)
                {
                    Global.MessageError("You Have To Select Graph/NonGraph  For BY Transfer While Makeing Grading Entry");
                    CmbLabProcess.Focus();
                    return;
                }

                if (Val.ToInt(CmbPrdType.Tag) == 8 && Val.ToString(CmbLabProcess.SelectedItem) == "GRAPH" && CmbLabSelection.SelectedIndex == 0)
                {
                    Global.MessageError("You Have To Select GIA / IGI / HED Lab Selection For BY Transfer While Makeing Grading Entry");
                    CmbLabSelection.Focus();
                    return;
                }
                if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Employee.");
                    txtEmployee.Focus();
                    return;
                }


                ////Add : Pinali : 27-09-2019 : Comment : Pinali : 04-11-2019 Coz For PCN RefPacketDetail Grd Entry is Already Copy from PCN Packets SO There Is posibility to not eixsts ArtistPrd Entry. 
                //Trn_RapSaveProperty  PropertyChk = new Trn_RapSaveProperty();
                //PropertyChk.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                //PropertyChk.KAPANNAME = Val.ToString(txtKapanName.Text);
                //PropertyChk.PACKETNO = Val.ToInt(txtPacketNo.Text);
                //PropertyChk.TAG = txtTag.Text;
                //PropertyChk.MTAG = txtTag.Text;
                //PropertyChk.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                //PropertyChk.PACKET_ID = Guid.Parse(txtTag.Tag.ToString());
                //PropertyChk = ObjRap.ValSave(PropertyChk);
                //if (PropertyChk.ReturnMessageType == "FAIL")
                //{
                //    BtnSave.Enabled = false;
                //    this.Cursor = Cursors.Default;
                //    Global.MessageError(PropertyChk.ReturnMessageDesc);
                //    return;
                //}
                //// End : Pinali : 27-09-2019

                // Validation For Previous Prediction is Exists Or Not
                Trn_RapSaveProperty Property = new Trn_RapSaveProperty();
                Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                Property.TAG = txtTag.Text;
                Property.MTAG = txtTag.Text;
                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);

                Property.SHAPE_ID = Val.ToInt32(txtShape.Tag);
                Property.SHAPENAME = Val.ToString(txtShape.Text);

                Property.COLOR_ID = Val.ToInt32(txtColor.Tag);
                Property.COLORNAME = Val.ToString(txtColor.Text);

                Property.CLARITY_ID = Val.ToInt32(txtClarity.Tag);
                Property.CLARITYNAME = Val.ToString(txtClarity.Text);

                Property.CARAT = Val.Val(txtCarat.Text);
                Property.BALANCECARAT = Val.Val(lblBalance.Text);

                Property.LABPROCESS = Val.ToString(CmbLabProcess.SelectedItem);

                Property.DIAMIN = Val.Val(txtDiaMin.Text);
                Property.DIAMAX = Val.Val(txtDiaMax.Text);
                Property.HEIGHT = Val.Val(txtHeight.Text);


                Property = ObjRap.ValSaveCheckWithMakable(Property);
                if (Property.ReturnMessageType == "FAIL")
                {
                    if (Property.ReturnMessageDesc.Contains("Shape"))
                    {
                        txtShape.Focus();
                    }
                    else if (Property.ReturnMessageDesc.Contains("Carat"))
                    {
                        txtCarat.Focus();
                    }
                    else if (Property.ReturnMessageDesc.Contains("Dia Min"))
                    {
                        txtDiaMin.Focus();
                    }
                    else if (Property.ReturnMessageDesc.Contains("Dia Max"))
                    {
                        txtDiaMax.Focus();
                    }
                    else if (Property.ReturnMessageDesc.Contains("Height"))
                    {
                        txtHeight.Focus();
                    }

                    Global.MessageError(Property.ReturnMessageDesc);
                    return;
                }
                Property = null;

                if (Global.Confirm("Are You Sure To Save Prediction Entry ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                FindRap();

                this.Cursor = Cursors.WaitCursor;

                if (Val.Val(txtRate.Text) == 0)
                {
                    Global.MessageError("Rate Is Required");
                    txtRate.Focus();
                    return;
                }

                if (Val.Val(txtAmount.Text) <= 0)
                {
                    Global.MessageError("Amount Is Required");
                    txtAmount.Focus();
                    return;
                }

                Int64 IntPrdID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.PRD_ID);

                ObjRap.DeleteAll(Val.ToInt(CmbPrdType.Tag), txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, Val.ToInt64(txtEmployee.Tag), Val.ToString(txtTag.Tag), mISTFlag);

                Property = new Trn_RapSaveProperty();

                Property.PACKET_ID = Val.ToInt64(txtTag.Tag);
                Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                Property.PRDTYPE = Val.ToString(CmbPrdType.SelectedItem);
                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                Property.MTAG = txtTag.Text;
                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                Property.MANAGER_ID = 0;

                Property.ID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.ID);
                Property.PRD_ID = IntPrdID;

                Property.TAGSRNO = 1;
                Property.TAG = txtTag.Text;
                Property.PLANNO = 1;

                Property.SHAPE_ID = Val.ToInt(txtShape.Tag);
                Property.CLARITY_ID = Val.ToInt(txtClarity.Tag);
                Property.COLOR_ID = Val.ToInt(txtColor.Tag);
                //Property.COLORSHADE_ID = 0;                   //Changed : 05-02-2020
                Property.COLORSHADE_ID = Val.ToInt(txtColorShade.Tag);
                Property.CUT_ID = Val.ToInt(txtCut.Tag);
                Property.POL_ID = Val.ToInt(txtPol.Tag);
                Property.SYM_ID = Val.ToInt(txtSym.Tag);
                Property.FL_ID = Val.ToInt(txtFL.Tag);
                Property.MILKY_ID = Val.ToInt(txtMilky.Tag);
                Property.LBLC_ID = Val.ToInt(txtLBLC.Tag);
                Property.NATTS_ID = Val.ToInt(txtNatts.Tag);
                Property.TENSION_ID = Val.ToInt(txtTension.Tag);
                Property.BLACKINC_ID = 0;
                Property.OPENINC_ID = 0;
                Property.WHITEINC_ID = 0;
                Property.LUSTER_ID = 0;
                Property.HA_ID = 0;
                Property.PAV_ID = 0;
                Property.EYECLEAN_ID = 0;
                Property.NATURAL_ID = 0;
                Property.GRAIN_ID = 0;

                Property.CARAT = Val.Val(txtCarat.Text);
                Property.DISCOUNT = clsFindRap.DISCOUNT;
                Property.AMOUNTDISCOUNT = clsFindRap.AMOUNTDISCOUNT;
                Property.RAPAPORT = clsFindRap.RAPAPORT;
                Property.PRICEPERCARAT = clsFindRap.PRICEPERCARAT;
                Property.AMOUNT = Math.Round(clsFindRap.AMOUNT, 0);
                Property.RAPDATE = Val.SqlDate(Val.ToString(CmbRapDate.SelectedItem));

                Property.GCARAT = 0;
                Property.GCOLOR_ID = 0;
                Property.GCLARITY_ID = 0;
                Property.GCUT_ID = 0;
                Property.GPOL_ID = 0;
                Property.GSYM_ID = 0;

                Property.GDISCOUNT = clsFindRap.GDISCOUNT;
                Property.GAMOUNTDISCOUNT = clsFindRap.GAMOUNTDISCOUNT;
                Property.GRAPAPORT = clsFindRap.GRAPAPORT;
                Property.GPRICEPERCARAT = clsFindRap.GPRICEPERCARAT;
                Property.GAMOUNT = Math.Round(clsFindRap.GAMOUNT, 0);
                Property.ISFINAL = true;

                Property.ENTRYDATE = Val.SqlDate(System.DateTime.Now.ToShortDateString());

                //#K : 24-11-2020
                //if (Val.ToString(CmbLabSelection.SelectedItem) == "IGI")
                //{
                //    Property.LAB_ID = 231;
                //}
                //else
                //{
                //    Property.LAB_ID = 0;
                //}
                //Property.LAB_ID = Val.ToInt32(CmbLab.Tag);
                Property.LAB_ID = Val.ToInt32(CmbLab.SelectedValue);
                Property.UPCOLOR_ID = clsFindRap.UPCOLOR_ID;
                Property.UPCOLORDISCOUNT = clsFindRap.UPCOLORDISCOUNT;
                Property.UPCOLORAMOUNTDISCOUNT = clsFindRap.UPCOLORAMOUNTDISCOUNT;
                Property.UPCOLORRAPAPORT = clsFindRap.UPCOLORRAPAPORT;
                Property.UPCOLORPRICEPERCARAT = clsFindRap.UPCOLORPRICEPERCARAT;
                Property.UPCOLORAMOUNT = Math.Round(clsFindRap.UPCOLORAMOUNT, 0);

                Property.DOWNCOLOR_ID = clsFindRap.DOWNCOLOR_ID;
                Property.DOWNCOLORDISCOUNT = clsFindRap.DOWNCOLORDISCOUNT;
                Property.DOWNCOLORAMOUNTDISCOUNT = clsFindRap.DOWNCOLORAMOUNTDISCOUNT;
                Property.DOWNCOLORRAPAPORT = clsFindRap.DOWNCOLORRAPAPORT;
                Property.DOWNCOLORPRICEPERCARAT = clsFindRap.DOWNCOLORPRICEPERCARAT;
                Property.DOWNCOLORAMOUNT = Math.Round(clsFindRap.DOWNCOLORAMOUNT, 0);

                Property.UPCLARITY_ID = clsFindRap.UPCLARITY_ID;
                Property.UPCLARITYDISCOUNT = clsFindRap.UPCLARITYDISCOUNT;
                Property.UPCLARITYAMOUNTDISCOUNT = clsFindRap.UPCLARITYAMOUNTDISCOUNT;
                Property.UPCLARITYRAPAPORT = clsFindRap.UPCLARITYRAPAPORT;
                Property.UPCLARITYPRICEPERCARAT = clsFindRap.UPCLARITYPRICEPERCARAT;
                Property.UPCLARITYAMOUNT = Math.Round(clsFindRap.UPCLARITYAMOUNT, 0);

                Property.DOWNCLARITY_ID = clsFindRap.DOWNCLARITY_ID;
                Property.DOWNCLARITYDISCOUNT = clsFindRap.DOWNCLARITYDISCOUNT;
                Property.DOWNCLARITYAMOUNTDISCOUNT = clsFindRap.DOWNCLARITYAMOUNTDISCOUNT;
                Property.DOWNCLARITYRAPAPORT = clsFindRap.DOWNCLARITYRAPAPORT;
                Property.DOWNCLARITYPRICEPERCARAT = clsFindRap.DOWNCLARITYPRICEPERCARAT;
                Property.DOWNCLARITYAMOUNT = Math.Round(clsFindRap.DOWNCLARITYAMOUNT, 0);


                Property.COPYFROMEMPLOYEE_ID = 0;
                Property.COPYFROMPRD_ID = null;
                Property.COPYFROM_ID = null;
                Property.COPYTOEMPLOYEE_ID = 0;
                Property.COPYTOPRD_ID = null;
                Property.COPYTO_ID = null;
                Property.ISDIFF = false;
                Property.REMARK = txtRemark.Text;

                Property.LABPROCESS = Val.ToString(CmbLabProcess.SelectedItem);
                Property.LABSELECTION = Val.ToString(CmbLabSelection.SelectedItem);
                Property.DIAMIN = Val.Val(txtDiaMin.Text);
                Property.DIAMAX = Val.Val(txtDiaMax.Text);
                Property.HEIGHT = Val.Val(txtHeight.Text);

                Property.ISMIXRATE = Val.ToBoolean(clsFindRap.ISMIXRATE);

                //Add : Pinali : 07-09-2019
                Property.MDISCOUNT = Val.Val(clsFindRap.MDISCOUNT);
                Property.MPRICEPERCARAT = Val.Val(clsFindRap.MPRICEPERCARAT);
                Property.MAMOUNT = Val.Val(clsFindRap.MAMOUNT);
                Property.MGDISCOUNT = Val.Val(clsFindRap.MGDISCOUNT);
                Property.MGPRICEPERCARAT = Val.Val(clsFindRap.MGPRICEPERCARAT);
                Property.MGAMOUNT = Val.Val(clsFindRap.MGAMOUNT);
                //End : Pinali : 07-09-2019

                Property.ISNOBGM = ChkNOBGM.Checked;
                Property.ISNOBLACK = ChkNOBlack.Checked;

                Property.ISPCNGRDBYLABENTRY = Val.ToBoolean(ChkISPcnGrdByLabEntry.Checked);
                Property.PCNGRDBYLAB_ID = Val.ToInt64(ChkISPcnGrdByLabEntry.Tag) == 0 ? 0 : Val.ToInt64(ChkISPcnGrdByLabEntry.Tag);
                Property.REPORTNO = Val.ToString(mStrReportNo);

                Property = ObjRap.Save(Property);

                Property = null;


                // Method Is For Update Max Flag In Table

                Trn_RapSaveProperty PropertySum = new Trn_RapSaveProperty();

                PropertySum.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                PropertySum.KAPANNAME = Val.ToString(txtKapanName.Text);
                PropertySum.PACKETNO = Val.ToInt(txtPacketNo.Text);
                PropertySum.MTAG = txtTag.Text;
                PropertySum.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                PropertySum.PRD_ID = IntPrdID;
                PropertySum = ObjRap.SaveMaxAmountFlag(PropertySum);

                PropertySum = null;

                this.Cursor = Cursors.Default;

                Global.Message("****  " + Val.ToString(CmbPrdType.SelectedItem) + "   *****\n\nSUCCESSFULLY SAVED OF " + txtKapanName.Text + "/" + txtPacketNo.Text + "/" + txtTag.Text);

                BtnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

        public void Clear() //Add : Pinali : 04-11-2019
        {
            lblMode.Text = "Add Mode";

            lblGrading.Text = string.Empty;
            BtnSave.Enabled = true;
            txtKapanName.Text = string.Empty;
            txtKapanName.Tag = string.Empty;

            txtPacketNo.Text = string.Empty;
            txtPacketNo.Tag = string.Empty;

            txtTag.Text = string.Empty;
            txtTag.Tag = string.Empty;

            //txtEmployee.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
            //txtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;

            txtShape.Text = string.Empty;
            txtShape.Tag = string.Empty;
            txtShape.AccessibleDescription = string.Empty;

            txtColor.Text = string.Empty;
            txtColor.Tag = string.Empty;
            txtColor.AccessibleDescription = string.Empty;

            txtClarity.Text = string.Empty;
            txtClarity.Tag = string.Empty;
            txtClarity.AccessibleDescription = string.Empty;

            txtCut.Text = string.Empty;
            txtCut.Tag = string.Empty;
            txtCut.AccessibleDescription = string.Empty;

            txtPol.Text = string.Empty;
            txtPol.Tag = string.Empty;
            txtPol.AccessibleDescription = string.Empty;

            txtSym.Text = string.Empty;
            txtSym.Tag = string.Empty;
            txtSym.AccessibleDescription = string.Empty;

            txtFL.Text = string.Empty;
            txtFL.Tag = string.Empty;
            txtFL.AccessibleDescription = string.Empty;

            txtLBLC.Text = string.Empty;
            txtLBLC.Tag = string.Empty;
            txtLBLC.AccessibleDescription = string.Empty;

            txtNatts.Text = string.Empty;
            txtNatts.Tag = string.Empty;
            txtNatts.AccessibleDescription = string.Empty;

            txtMilky.Text = string.Empty;
            txtMilky.Tag = string.Empty;
            txtMilky.AccessibleDescription = string.Empty;

            txtTension.Text = string.Empty;
            txtTension.Tag = string.Empty;
            txtTension.AccessibleDescription = string.Empty;

            txtColorShade.Text = string.Empty;
            txtColorShade.Tag = string.Empty;
            txtColorShade.AccessibleDescription = string.Empty;

            txtCarat.Text = string.Empty;

            ChkNOBGM.Checked = false;
            ChkNOBlack.Checked = false;

            CmbLabSelection.SelectedIndex = 0;
            CmbLabProcess.SelectedIndex = 0;
            txtDiaMax.Text = string.Empty;
            txtDiaMin.Text = string.Empty;
            txtHeight.Text = string.Empty;

            txtRate.Text = string.Empty;
            txtAmount.Text = string.Empty;

            lblLot.Text = "0.00";
            lblBalance.Text = "0.00";
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Clear();
            txtKapanName.Focus();
            mISTFlag = false;
            this.Cursor = Cursors.Default;
        }

        #endregion
        public void ConsiderBGMNonBGM(Int32 IntMilky_ID, Int32 IntColorShade_ID) //Add : Pinali : 01-06-2020
        {
            //if (Val.ToString(txtKapanName.Text).Trim().Length == 0 || Val.ToString(txtPacketNo.Text).Trim().Length == 0 || Val.ToString(txtTag.Text).Trim().Length == 0)
            //{
            //    return;
            //}

            if ((Val.ToInt32(IntMilky_ID) == 0 || Val.ToInt32(IntMilky_ID) == 276)
                   &&
                   (Val.ToInt32(IntColorShade_ID) == 0 || Val.ToInt32(IntColorShade_ID) == 98 || Val.ToInt32(IntColorShade_ID) == 104 || Val.ToInt32(IntColorShade_ID) == 106 || Val.ToInt32(IntColorShade_ID) == 1650)
                   &&
                   (Val.ToInt(CmbPrdType.Tag) == 8 || Val.ToInt(CmbPrdType.Tag) == 9 || Val.ToInt(CmbPrdType.Tag) == 11))
            {
                ChkNOBGM.Checked = true;
            }
            else
            {
                ChkNOBGM.Checked = false;
            }
        }
        public void FindRap()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                ConsiderBGMNonBGM(Val.ToInt32(txtMilky.Tag), Val.ToInt32(txtColorShade.Tag));

                clsFindRap = new Trn_RapSaveProperty();

                clsFindRap.SHAPECODE = Val.ToString(txtShape.AccessibleDescription);

                clsFindRap.COLOR_ID = Val.ToInt32(txtColor.Tag);
                clsFindRap.COLORCODE = Val.ToString(txtColor.AccessibleDescription);

                clsFindRap.CLARITY_ID = Val.ToInt32(txtClarity.Tag);
                clsFindRap.CLARITYCODE = Val.ToString(txtClarity.AccessibleDescription);

                clsFindRap.CARAT = Val.Val(txtCarat.Text);
                clsFindRap.CUTCODE = Val.ToString(txtCut.AccessibleDescription);
                clsFindRap.POLCODE = Val.ToString(txtPol.AccessibleDescription);
                clsFindRap.SYMCODE = Val.ToString(txtSym.AccessibleDescription);

                clsFindRap.GCARAT = 0;
                clsFindRap.GCUTCODE = "";
                clsFindRap.GPOLCODE = "";
                clsFindRap.GSYMCODE = "";

                clsFindRap.FLCODE = Val.ToString(txtFL.AccessibleDescription);
                clsFindRap.MILKYCODE = Val.ToString(txtMilky.AccessibleDescription);
                clsFindRap.NATTSCODE = Val.ToString(txtNatts.AccessibleDescription);
                clsFindRap.LBLCCODE = Val.ToString(txtLBLC.AccessibleDescription);
                clsFindRap.RAPDATE = Val.SqlDate(CmbRapDate.SelectedItem.ToString());

                //clsFindRap.COLORSHADECODE = "";
                clsFindRap.COLORSHADECODE = Val.ToString(txtColorShade.AccessibleDescription);
                clsFindRap.OPENINCCODE = "";
                clsFindRap.BLACKINCCODE = "";
                clsFindRap.WHITEINCCODE = "";
                clsFindRap.PAVCODE = "";
                clsFindRap.EYECLEANCODE = "";
                clsFindRap.LUSTERCODE = "";
                clsFindRap.NATURALCODE = "";
                clsFindRap.GRAINCODE = "";
                if (Val.ToString(CmbLabSelection.SelectedItem) == "IGI")
                {
                    clsFindRap.LABCODE = "IGI";
                }
                else
                {
                    clsFindRap.LABCODE = "";
                }


                if (clsFindRap.SHAPECODE == "" || clsFindRap.COLORCODE == "" || clsFindRap.CLARITYCODE == "")
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                clsFindRap = ObjRap.FindRapWithUpDown(clsFindRap);

                txtRate.Text = clsFindRap.PRICEPERCARAT.ToString();
                txtAmount.Text = Math.Round(clsFindRap.AMOUNT, 0).ToString();

                txtGiaNonGia.Text = Val.ToString(clsFindRap.GIANONGIA);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
            this.Cursor = Cursors.Default;

        }


        #region Control Envets

        private void CmbRapDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindRap();
        }

        #endregion


        private void BtnContinue_Click(object sender, EventArgs e)
        {
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
            if (txtTag.Text.Length == 0)
            {
                Global.MessageError("Tag Is Required");
                txtTag.Focus();
                return;
            }

            if (Val.ToInt(CmbPrdType.Tag) == 11 && Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
            {
                Global.MessageError("Lab Is Required.");
                txtEmployee.Focus();
                return;
            }


            this.Cursor = Cursors.WaitCursor;

            DataRow DRPkt = ObjRap.GetPacketDataRow(txtKapanName.Text, Val.ToInt32(txtPacketNo.Text), txtTag.Text);
            if (DRPkt == null)
            {
                BtnSave.Enabled = false;
                Global.MessageError("Ooops.. Packet Is Not Found");
                return;
            }

            txtTag.Tag = Val.ToString(DRPkt["PACKET_ID"]);
            lblLot.Text = Val.ToString(DRPkt["LOTCARAT"]);
            lblBalance.Text = Val.ToString(DRPkt["BALANCECARAT"]);

            DRPkt = null;

            Int64 IntEmployeeID = Val.ToInt64(txtEmployee.Tag);
            //if (Val.ToInt(CmbPrdType.Tag) == 8 || Val.ToInt(CmbPrdType.Tag) == 9 || Val.ToInt(CmbPrdType.Tag) == 11)   //Comment : Pinali : 27-09-2019
            //{
            //    IntEmployeeID = 0;
            //}

            DataTable DTab = ObjRap.GetPredictionData(Val.ToInt32(CmbPrdType.Tag), Val.ToString(txtTag.Tag), IntEmployeeID, 0);
            lblMode.Text = "Add Mode";
            if (DTab.Rows.Count == 0)
            {
                lblMode.Text = "Add Mode";
                // Validation For Previous Prediction is Exists Or Not
                Trn_RapSaveProperty Property = new Trn_RapSaveProperty();
                Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                Property.TAG = txtTag.Text;
                Property.MTAG = txtTag.Text;
                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                Property.PACKET_ID = Val.ToInt64(txtTag.Tag);
                Property = ObjRap.ValSave(Property);
                if (Property.ReturnMessageType == "FAIL")
                {
                    BtnSave.Enabled = false;
                    this.Cursor = Cursors.Default;
                    Global.MessageError(Property.ReturnMessageDesc);
                    return;
                }
                Property = null;

                mISTFlag = false;

                lblGrading.Text = "GRADING Data";
                // FETCH GRADING FIELDS

                DTab = ObjRap.GetPredictionData(8, Val.ToString(txtTag.Tag), 0, 0);
            }
            else
            {
                lblMode.Text = "Edit Mode";
                lblGrading.Text = Val.ToString(CmbPrdType.SelectedItem) + " : Data";
            }

            /*     if (DTab.Rows.Count != 0 && Val.ToBoolean(DTab.Rows[0]["ALLOWFORUPDATE"]) == false)
                 {                
                     BtnSave.Enabled = EmployeeRightsProperty.RAPUPDATEPREDICTION;
                
                 }
                 else
                 {
                     if (DTab.Rows.Count  == 0)
                     {
                         // Validation For Previous Prediction is Exists Or Not
                         Trn_RapSaveProperty Property = new Trn_RapSaveProperty();
                         Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                         Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                         Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                         Property.TAG = txtTag.Text;
                         Property.MTAG = txtTag.Text;
                         Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                         Property = ObjRap.ValSave(Property);
                         if (Property.ReturnMessageType == "FAIL")
                         {
                             BtnSave.Enabled = false;
                             this.Cursor = Cursors.Default;
                             Global.MessageError(Property.ReturnMessageDesc);
                             return;
                         }
                         Property = null;
                     }                
                 }*/



            txtRemark.Text = string.Empty;
            CmbLabProcess.SelectedItem = "";
            CmbLabSelection.SelectedItem = "";
            txtDiaMin.Text = string.Empty;
            txtDiaMax.Text = string.Empty;
            txtHeight.Text = string.Empty;

            txtShape.Tag = string.Empty;
            txtShape.AccessibleDescription = string.Empty;
            txtShape.Text = string.Empty;

            txtColor.Tag = string.Empty;
            txtColor.AccessibleDescription = string.Empty;
            txtColor.Text = string.Empty;

            txtClarity.Tag = string.Empty;
            txtClarity.AccessibleDescription = string.Empty;
            txtClarity.Text = string.Empty;

            txtCut.Tag = string.Empty;
            txtCut.AccessibleDescription = string.Empty;
            txtCut.Text = string.Empty;

            txtPol.Tag = string.Empty;
            txtPol.AccessibleDescription = string.Empty;
            txtPol.Text = string.Empty;

            txtSym.Tag = string.Empty;
            txtSym.AccessibleDescription = string.Empty;
            txtSym.Text = string.Empty;

            txtFL.Tag = string.Empty;
            txtFL.AccessibleDescription = string.Empty;
            txtFL.Text = string.Empty;

            txtLBLC.Tag = string.Empty;
            txtLBLC.AccessibleDescription = string.Empty;
            txtLBLC.Text = string.Empty;

            txtNatts.Tag = string.Empty;
            txtNatts.AccessibleDescription = string.Empty;
            txtNatts.Text = string.Empty;

            txtMilky.Tag = string.Empty;
            txtMilky.AccessibleDescription = string.Empty;
            txtMilky.Text = string.Empty;

            txtTension.Tag = string.Empty;
            txtTension.AccessibleDescription = string.Empty;
            txtTension.Text = string.Empty;

            txtColorShade.Tag = string.Empty;
            txtColorShade.AccessibleDescription = string.Empty;
            txtColorShade.Text = string.Empty;

            CmbRapDate.SelectedItem = string.Empty;

            txtCarat.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtAmount.Text = string.Empty;

            txtGiaNonGia.Text = string.Empty; //Add : Pinali : 19-09-2019

            BtnSave.Enabled = true;

            //Add : Pinali : 04-11-2019
            ChkISPcnGrdByLabEntry.Checked = false;
            ChkISPcnGrdByLabEntry.Tag = string.Empty;
            mStrReportNo = string.Empty;
            //End : Pinali : 04-11-2019

            mISTFlag = false;

          
            if (lblMode.Text == "Edit Mode" && EmployeeRightsProperty.RAPUPDATEPREDICTION == false)
            {
                BtnSave.Enabled = false;
            }

            if (DTab.Rows.Count != 0)
            {

                DataRow DRow = DTab.Rows[0];
                txtRemark.Text = Val.ToString(DRow["REMARK"]);
                CmbLabProcess.SelectedItem = Val.ToString(DRow["LABPROCESS"]);
                CmbLabSelection.SelectedItem = Val.ToString(DRow["LABSELECTION"]);
                txtDiaMin.Text = Val.ToString(DRow["DIAMIN"]);
                txtDiaMax.Text = Val.ToString(DRow["DIAMAX"]);
                txtHeight.Text = Val.ToString(DRow["HEIGHT"]);

                txtShape.Tag = Val.ToString(DRow["SHAPE_ID"]);
                txtShape.AccessibleDescription = Val.ToString(DRow["SHAPECODE"]);
                txtShape.Text = Val.ToString(DRow["SHAPENAME"]);

                txtColor.Tag = Val.ToString(DRow["COLOR_ID"]);
                txtColor.AccessibleDescription = Val.ToString(DRow["COLORCODE"]);
                txtColor.Text = Val.ToString(DRow["COLORNAME"]);

                txtClarity.Tag = Val.ToString(DRow["CLARITY_ID"]);
                txtClarity.AccessibleDescription = Val.ToString(DRow["CLARITYCODE"]);
                txtClarity.Text = Val.ToString(DRow["CLARITYNAME"]);

                txtCut.Tag = Val.ToString(DRow["CUT_ID"]);
                txtCut.AccessibleDescription = Val.ToString(DRow["CUTCODE"]);
                txtCut.Text = Val.ToString(DRow["CUTCODE"]);

                txtPol.Tag = Val.ToString(DRow["POL_ID"]);
                txtPol.AccessibleDescription = Val.ToString(DRow["POLCODE"]);
                txtPol.Text = Val.ToString(DRow["POLCODE"]);

                txtSym.Tag = Val.ToString(DRow["SYM_ID"]);
                txtSym.AccessibleDescription = Val.ToString(DRow["SYMCODE"]);
                txtSym.Text = Val.ToString(DRow["SYMCODE"]);

                txtFL.Tag = Val.ToString(DRow["FL_ID"]);
                txtFL.AccessibleDescription = Val.ToString(DRow["FLCODE"]);
                txtFL.Text = Val.ToString(DRow["FLNAME"]);

                txtLBLC.Tag = Val.ToString(DRow["LBLC_ID"]);
                txtLBLC.AccessibleDescription = Val.ToString(DRow["LBLCCODE"]);
                txtLBLC.Text = Val.ToString(DRow["LBLCNAME"]);

                txtNatts.Tag = Val.ToString(DRow["NATTS_ID"]);
                txtNatts.AccessibleDescription = Val.ToString(DRow["NATTSCODE"]);
                txtNatts.Text = Val.ToString(DRow["NATTSNAME"]);

                txtMilky.Tag = Val.ToString(DRow["MILKY_ID"]);
                txtMilky.AccessibleDescription = Val.ToString(DRow["MILKYCODE"]);
                txtMilky.Text = Val.ToString(DRow["MILKYNAME"]);

                txtTension.Tag = Val.ToString(DRow["TENSION_ID"]);
                txtTension.AccessibleDescription = Val.ToString(DRow["TENSIONCODE"]);
                txtTension.Text = Val.ToString(DRow["TENSIONNAME"]);

                txtColorShade.Tag = Val.ToString(DRow["COLORSHADE_ID"]);
                txtColorShade.AccessibleDescription = Val.ToString(DRow["COLORSHADECODE"]);
                txtColorShade.Text = Val.ToString(DRow["COLORSHADENAME"]);

                CmbRapDate.SelectedItem = Val.ToString(DRow["RAPDATE"]);

                txtCarat.Text = Val.ToString(DRow["CARAT"]);
                txtRate.Text = Val.ToString(DRow["PRICEPERCARAT"]);
                txtAmount.Text = Val.ToString(DRow["AMOUNT"]);

                txtGiaNonGia.Text = Val.ToString(DRow["GIANONGIA"]);

                ChkNOBGM.Checked = Val.ToBoolean(DRow["ISNOBGM"]);
                ChkNOBlack.Checked = Val.ToBoolean(DRow["ISNOBLACK"]);

                //Add : Pinali : 04-11-2019
                ChkISPcnGrdByLabEntry.Checked = Val.ToBoolean(DRow["ISPCNGRDBYLABENTRY"]);
                ChkISPcnGrdByLabEntry.Tag = Val.ToString(DRow["PCNGRDBYLAB_ID"]);
                mStrReportNo = Val.ToString(DRow["REPORTNO"]);
                //End : Pinali : 04-11-2019

                mISTFlag = Val.ToBoolean(DRow["TFLAG"]);

            }

            this.Cursor = Cursors.Default;

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
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();

                    this.Cursor = Cursors.WaitCursor;
                    //FrmSearch.DTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    if (Val.ToString(CmbPrdType.Tag) == "11")
                    {
                        FrmSearch.mColumnsToHide = "EMPLOYEE_ID,EMPLOYEECODE";
                        FrmSearch.mColumnHeaderCaptions = "EMPLOYEENAME=Lab";
                        FrmSearch.mDTab = DtabLabEmp;
                    }
                    else
                    {
                        FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    }



                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);

                        Clear();
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

        private void BtnEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "EMPLOYEECODE";
                FrmSearch.mSearchText = "";
                this.Cursor = Cursors.WaitCursor;
                FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                if (FrmSearch.mDRow != null)
                {
                    txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                    txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                }

                FrmSearch.Hide();
                FrmSearch.Dispose();
                FrmSearch = null;
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void CmbPrdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtEmployee.Text = string.Empty;
                txtEmployee.Tag = string.Empty;

                string Name = Val.ToString(CmbPrdType.SelectedItem);
                DataRow[] D = DTabPrdType.Select("PRDTYPENAME ='" + Name + "' ");

                //txtKapanName.Enabled = false;
                //txtPacketNo.Enabled = false;
                //txtTag.Enabled = false;

                if (D.Length != 0)
                {
                    DataRow DRow = D[0];
                    CmbPrdType.Tag = Val.ToString(DRow["PrdType_ID"]);

                    //txtKapanName.Enabled = Val.ToBoolean(DRow["ISKapan"]);
                    //txtPacketNo.Enabled = Val.ToBoolean(DRow["ISPacketNo"]);
                    //txtTag.Enabled = Val.ToBoolean(DRow["ISTag"]);

                    DRow = null;
                }

                D = null;

                //txtEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;

                if (Name == "AP")
                {
                    txtEmployee.Enabled = true;
                }
                if (Val.ToInt(CmbPrdType.Tag) == 8 || Val.ToInt(CmbPrdType.Tag) == 9 || Val.ToInt(CmbPrdType.Tag) == 11)
                {
                    lblLabProcess.Visible = true;
                    CmbLabProcess.Visible = true;

                    lblLabSelection.Visible = true;
                    CmbLabSelection.Visible = true;

                    ChkNOBGM.Visible = true;
                    ChkNOBlack.Visible = true;
                }
                else
                {
                    lblLabProcess.Visible = false;
                    CmbLabProcess.Visible = false;

                    lblLabSelection.Visible = false;
                    CmbLabSelection.Visible = false;

                    ChkNOBGM.Checked = false;
                    ChkNOBlack.Checked = false;
                    ChkNOBGM.Visible = false;
                    ChkNOBlack.Visible = false;
                }

                if (Val.ToInt(CmbPrdType.Tag) == 11)  //Val.ToInt(CmbPrdType.Tag) == 9 ||  || Val.ToInt(CmbPrdType.Tag) == 12    //Add : Pinali : 27-09-2019
                    lblEmployee.Text = "Lab";
                else
                    lblEmployee.Text = "Emp";

                BtnClear_Click(null, null);

                txtEmployee.Focus();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }

        private void txtShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,SHAPECODE,SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
                    FrmSearch.mColumnsToHide = "SHAPE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtShape.AccessibleDescription = Val.ToString(FrmSearch.mDRow["SHAPECODE"]);
                        txtShape.Text = Val.ToString(FrmSearch.mDRow["SHAPENAME"]);
                        txtShape.Tag = Val.ToString(FrmSearch.mDRow["SHAPE_ID"]);
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

        private void txtColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,COLORCODE,COLORNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);
                    FrmSearch.mColumnsToHide = "COLOR_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtColor.AccessibleDescription = Val.ToString(FrmSearch.mDRow["COLORCODE"]);
                        txtColor.Text = Val.ToString(FrmSearch.mDRow["COLORNAME"]);
                        txtColor.Tag = Val.ToString(FrmSearch.mDRow["COLOR_ID"]);
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
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,CLARITYCODE,CLARITYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);
                    FrmSearch.mColumnsToHide = "CLARITY_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtClarity.AccessibleDescription = Val.ToString(FrmSearch.mDRow["CLARITYCODE"]);
                        txtClarity.Text = Val.ToString(FrmSearch.mDRow["CLARITYNAME"]);
                        txtClarity.Tag = Val.ToString(FrmSearch.mDRow["CLARITY_ID"]);
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

        private void txtCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,CUTCODE,CUTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CUT);
                    FrmSearch.mColumnsToHide = "CUT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtCut.AccessibleDescription = Val.ToString(FrmSearch.mDRow["CUTCODE"]);
                        txtCut.Text = Val.ToString(FrmSearch.mDRow["CUTCODE"]);
                        txtCut.Tag = Val.ToString(FrmSearch.mDRow["CUT_ID"]);
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

        private void txtPol_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,POLCODE,POLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_POL);
                    FrmSearch.mColumnsToHide = "POL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPol.AccessibleDescription = Val.ToString(FrmSearch.mDRow["POLCODE"]);
                        txtPol.Text = Val.ToString(FrmSearch.mDRow["POLCODE"]);
                        txtPol.Tag = Val.ToString(FrmSearch.mDRow["POL_ID"]);
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

        private void txtSym_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,SYMCODE,SYMNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SYM);
                    FrmSearch.mColumnsToHide = "SYM_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtSym.AccessibleDescription = Val.ToString(FrmSearch.mDRow["SYMCODE"]);
                        txtSym.Text = Val.ToString(FrmSearch.mDRow["SYMCODE"]);
                        txtSym.Tag = Val.ToString(FrmSearch.mDRow["SYM_ID"]);
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

        private void txtFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,FLCODE,FLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_FL);
                    FrmSearch.mColumnsToHide = "FL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;

                    if (FrmSearch.mDRow != null)
                    {
                        txtFL.AccessibleDescription = Val.ToString(FrmSearch.mDRow["FLCODE"]);
                        txtFL.Text = Val.ToString(FrmSearch.mDRow["FLNAME"]);
                        txtFL.Tag = Val.ToString(FrmSearch.mDRow["FL_ID"]);
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

        private void txtLBLC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,LBLCCODE,LBLCNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LBLC);
                    FrmSearch.mColumnsToHide = "LBLC_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtLBLC.AccessibleDescription = Val.ToString(FrmSearch.mDRow["LBLCCODE"]);
                        txtLBLC.Text = Val.ToString(FrmSearch.mDRow["LBLCNAME"]);
                        txtLBLC.Tag = Val.ToString(FrmSearch.mDRow["LBLC_ID"]);
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

        private void txtNatts_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,NATTSCODE,NATTSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_NATTS);
                    FrmSearch.mColumnsToHide = "NATTS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtNatts.AccessibleDescription = Val.ToString(FrmSearch.mDRow["NATTSCODE"]);
                        txtNatts.Text = Val.ToString(FrmSearch.mDRow["NATTSNAME"]);
                        txtNatts.Tag = Val.ToString(FrmSearch.mDRow["NATTS_ID"]);
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

        private void txtMilky_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,MILKYCODE,MILKYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MILKY);
                    FrmSearch.mColumnsToHide = "MILKY_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtMilky.AccessibleDescription = Val.ToString(FrmSearch.mDRow["MILKYCODE"]);
                        txtMilky.Text = Val.ToString(FrmSearch.mDRow["MILKYNAME"]);
                        txtMilky.Tag = Val.ToString(FrmSearch.mDRow["MILKY_ID"]);
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

        private void txtTension_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,TENSIONCODE,TENSIONNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_TENSION);
                    FrmSearch.mColumnsToHide = "TENSION_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtTension.AccessibleDescription = Val.ToString(FrmSearch.mDRow["TENSIONCODE"]);
                        txtTension.Text = Val.ToString(FrmSearch.mDRow["TENSIONNAME"]);
                        txtTension.Tag = Val.ToString(FrmSearch.mDRow["TENSION_ID"]);
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
                    //if (EmployeeRightsProperty.RAPCHANGEPACKETS == true)
                    //{
                    //    FrmSearch.DTab = ObjRap.GetKapan();
                    //}
                    //else
                    //{
                    //    FrmSearch.DTab = ObjRap.GetEmployeeOSKapan();
                    //}
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
                    //if (EmployeeRightsProperty.RAPCHANGEPACKETS == true)
                    //{
                    //    FrmSearch.DTab = ObjRap.GetPacketNo(txtKapanName.Text);
                    //}
                    //else
                    //{
                    //    FrmSearch.DTab = ObjRap.GetEmployeeOSPacketNo(txtKapanName.Text);
                    //}
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
                    //if (EmployeeRightsProperty.RAPCHANGEPACKETS == true)
                    //{
                    //    FrmSearch.DTab = ObjRap.GetTag(txtKapanName.Text, Val.ToInt(txtPacketNo.Text));
                    //}
                    //else
                    //{
                    //    FrmSearch.DTab = ObjRap.GetEmployeeOSTag(txtKapanName.Text, Val.ToInt(txtPacketNo.Text));
                    //}

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
                        //    txtEmployee.Tag = Val.ToString(FrmSearch.DRow["EMPLOYEE_ID"]);
                        //    txtEmployee.Text = Val.ToString(FrmSearch.DRow["EMPLOYEECODE"]);
                        //}
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

        private void txtTag_Validated(object sender, EventArgs e)
        {
            BtnContinue_Click(null, null);
        }

        private void CmbLabProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Val.ToString(CmbLabProcess.SelectedItem) == "GRAPH")
            {
                CmbLabSelection.Enabled = true;
                lblLabSelection.Enabled = true;
            }
            else
            {
                CmbLabSelection.Text = string.Empty;
                CmbLabSelection.SelectedIndex = 0;
                CmbLabSelection.Enabled = false;
                lblLabSelection.Enabled = false;
            }
        }


        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            FindRap();
        }

        private void txtEmployee_Validated(object sender, EventArgs e)
        {
            try
            {
                //BtnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtColorShade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,COLORSHADECODE,COLORSHADENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLORSHADE);
                    FrmSearch.mColumnsToHide = "COLORSHADE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtColorShade.AccessibleDescription = Val.ToString(FrmSearch.mDRow["COLORSHADECODE"]);
                        txtColorShade.Text = Val.ToString(FrmSearch.mDRow["COLORSHADENAME"]);
                        txtColorShade.Tag = Val.ToString(FrmSearch.mDRow["COLORSHADE_ID"]);
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
    public class DataStructure
    {
        public int PARA_ID { get; set; }
        public string PARACODE { get; set; }
        public string PARANAME { get; set; }
    }

}