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
using OfficeOpenXml;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Text.RegularExpressions;

namespace AxoneMFGRJ.Grading
{
    public partial class FrmSinglePacketLabPricingEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BODevGridSelection ObjGridSelection;

        BOFindRap ObjRap = new BOFindRap();

        DataTable DTabPrdType = new DataTable();
        DataTable DTabRapDate = new DataTable();
        DataTable DTabParameter = new DataTable();
        DataTable DTabPrcLvl1Det = new DataTable();
        Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();
        Trn_RapSaveProperty clsFindRapRChk = new Trn_RapSaveProperty();

        DataTable DtabLabEmp = new DataTable();
        DataTable DTabRChkData = new DataTable();
        DataTable DTabExcelData = new DataTable();
        DataTable DTabUploadData = new DataTable();

        IList<DataStructureLabPricing> DataBLACK = new BindingList<DataStructureLabPricing>();
        IList<DataStructureLabPricing> DataOPEN = new BindingList<DataStructureLabPricing>();
        IList<DataStructureLabPricing> DataWHITE = new BindingList<DataStructureLabPricing>();
        IList<DataStructureLabPricing> DataHEARTANDARROW = new BindingList<DataStructureLabPricing>();
        IList<DataStructureLabPricing> DataPAVALION = new BindingList<DataStructureLabPricing>();
        IList<DataStructureLabPricing> DataTENSION = new BindingList<DataStructureLabPricing>();
        IList<DataStructureLabPricing> DataLUSTER = new BindingList<DataStructureLabPricing>();
        IList<DataStructureLabPricing> DataEYECLEAN = new BindingList<DataStructureLabPricing>();
        IList<DataStructureLabPricing> DataNATURAL = new BindingList<DataStructureLabPricing>();
        IList<DataStructureLabPricing> DataGRAIN = new BindingList<DataStructureLabPricing>();

        EmployeeActionRightsProperty EmployeeRightsProperty = new EmployeeActionRightsProperty();
        string mStrReportNo = "";

        bool mISTFlag = false;
        bool IsDownImage = false;

        #region Constructor

        public FrmSinglePacketLabPricingEntry()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            GrdFileUpload.BeginUpdate();
            if (MainGrdExcel.RepositoryItems.Count == 2)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdFileUpload;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 1;

                GrdFileUpload.Bands["BANDGENERAL"].Fixed = FixedStyle.None;
                GridBand band = GrdFileUpload.Bands.AddBand("..");
                band.Columns.Add(GrdFileUpload.Columns["COLSELECTCHECKBOX"]);
                band.Fixed = FixedStyle.Left;
                band.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }


            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 1;
            }
            GrdFileUpload.EndUpdate();
            this.Show();


            SetControl();
            BtnClear_Click(null, null);
            BtnUpDown_Click(null, null);

            EmployeeRightsProperty = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
            txtEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;

            DTabPrdType = ObjRap.GetPreditctionType(EmployeeRightsProperty.PRDTYPE_ID);
            CmbPrdType.Items.Clear();
            foreach (DataRow DRow in DTabPrdType.Rows)
            {
                if (Val.ToInt(DRow["PRDTYPE_ID"]) == 14 || Val.ToInt(DRow["PRDTYPE_ID"]) == 15 || Val.ToInt(DRow["PRDTYPE_ID"]) == 16)
                {
                    CmbPrdType.Items.Add(Val.ToString(DRow["PRDTYPENAME"]));
                }

            }
            CmbPrdType.SelectedIndex = 0;


            DtabLabEmp.Columns.Add("EMPLOYEE_ID", typeof(Int64));
            DtabLabEmp.Columns.Add("EMPLOYEENAME", typeof(string));
            DtabLabEmp.Columns.Add("EMPLOYEECODE", typeof(string));

            DataRow DrLabEmp = DtabLabEmp.NewRow();
            DtabLabEmp.Rows.Add("-2", "GIA", "GIA");
            DtabLabEmp.Rows.Add("-3", "IGI", "IGI");
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

            ObjFormEvent.ObjToDisposeList.Add(DataBLACK);
            ObjFormEvent.ObjToDisposeList.Add(DataOPEN);
            ObjFormEvent.ObjToDisposeList.Add(DataWHITE);
            ObjFormEvent.ObjToDisposeList.Add(DataHEARTANDARROW);
            ObjFormEvent.ObjToDisposeList.Add(DataPAVALION);
            ObjFormEvent.ObjToDisposeList.Add(DataTENSION);
            ObjFormEvent.ObjToDisposeList.Add(DataNATURAL);
            ObjFormEvent.ObjToDisposeList.Add(DataLUSTER);
            ObjFormEvent.ObjToDisposeList.Add(DataGRAIN);
            ObjFormEvent.ObjToDisposeList.Add(DataEYECLEAN);

            ObjFormEvent.ObjToDisposeList.Add(DTabRapDate);

            ObjFormEvent.ObjToDisposeList.Add(clsFindRap);
        }

        #endregion

        #region Functions

        public void SetControl()
        {
            DTabRapDate = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.RAPDATE);
            DTabRapDate.DefaultView.Sort = "RAPDATE DESC";
            DTabRapDate = DTabRapDate.DefaultView.ToTable();

            DTabParameter = ObjRap.GetAllParameterTable();
            DesignComboBox("TENSION", CmbTension, "PARANAME");
            DesignComboBox("GRAIN", CmbGrain, "PARACODE");
            DesignComboBox("NATURAL", CmbNatural, "PARANAME");

            //DesignComboBox("BLACK", CmbBlackInc, "PARACODE");
            //DesignComboBox("OPEN", CmbOpenInc, "PARACODE");
            //DesignComboBox("WHITE", CmbWhiteInc, "PARACODE");
            //DesignComboBox("HEARTANDARROW", CmbHA, "PARACODE");
            //DesignComboBox("PAVALION", CmbPavalion, "PARACODE");
            //DesignComboBox("LUSTER", CmbLuster, "PARACODE");
            //DesignComboBox("EYECLEAN", CmbEyeClean, "PARACODE");

            //ReCheck-Repairing Cmb Control
            //DesignComboBox("BLACK", CmbRChkBlackInc, "PARACODE");
            //DesignComboBox("OPEN", CmbRChkOpenInc, "PARACODE");
            //DesignComboBox("WHITE", CmbRChkWhiteInc, "PARACODE");
            //DesignComboBox("HEARTANDARROW", CmbRChkHA, "PARACODE");
            //DesignComboBox("PAVALION", CmbRChkPavalion, "PARACODE");
            //DesignComboBox("LUSTER", CmbRChkLuster, "PARACODE");
            //DesignComboBox("EYECLEAN", CmbRChkEyeClean, "PARACODE");

            CmbRapDate.Items.Clear();
            foreach (DataRow DRow in DTabRapDate.Rows)
            {
                CmbRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }
            CmbRapDate.SelectedIndex = 0;
        }

        public void DesignComboBox(string pStrParaType, AxonContLib.cComboBox ComboBox, string pStrDisplayText)
        {
            DataRow[] UDRow = DTabParameter.Select("ParaType = '" + pStrParaType + "'");

            if (UDRow.Length == 0)
            {
                return;
            }

            DataTable DTab = UDRow.CopyToDataTable();
            DTab.DefaultView.Sort = "SequenceNo";
            DTab = DTab.DefaultView.ToTable();

            if (pStrParaType == "TENSION")
            {
                DataTENSION.Add(new DataStructureLabPricing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataTENSION.Add(new DataStructureLabPricing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbTension.AccessibleDescription = pStrParaType;
                CmbTension.DataSource = DataTENSION;
                CmbTension.DisplayMember = pStrDisplayText;
                CmbTension.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "NATURAL")
            {
                DataNATURAL.Add(new DataStructureLabPricing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataNATURAL.Add(new DataStructureLabPricing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbNatural.AccessibleDescription = pStrParaType;
                CmbNatural.DataSource = DataNATURAL;
                CmbNatural.DisplayMember = pStrDisplayText;
                CmbNatural.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "GRAIN")
            {
                DataGRAIN.Add(new DataStructureLabPricing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataGRAIN.Add(new DataStructureLabPricing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbGrain.AccessibleDescription = pStrParaType;
                CmbGrain.DataSource = DataGRAIN;
                CmbGrain.DisplayMember = pStrDisplayText;
                CmbGrain.ValueMember = "PARA_ID";
            }
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

                if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Employee.");
                    txtEmployee.Focus();
                    return;
                }
                if (CmbLabResultStatus.Text.Trim().Equals(string.Empty) || CmbLabResultStatus.Text.Trim().ToUpper().Equals("NONE"))
                {
                    Global.Message("Please Select Lab Result.");
                    CmbLabResultStatus.Focus();
                    return;
                }
                if ((Val.ToString(CmbCurrLabResultStatus.Text) != Val.ToString(CmbLabResultStatus.Text)) &&
                   (Val.ToString(CmbCurrLabResultStatus.Text) != "REPAIRINGCONFIRM" && Val.ToString(CmbCurrLabResultStatus.Text) != "RECHECKCONFIRM"
                     && Val.ToString(CmbCurrLabResultStatus.Text) != "REPAIRINGCANCEL" && Val.ToString(CmbCurrLabResultStatus.Text) != "RECHECKCANCEL" && Val.ToString(CmbCurrLabResultStatus.Text) != "NONE")
                  )
                {
                    Global.MessageError("You Can't Update Records With '" + Val.ToString(CmbLabResultStatus.Text) + "' Status Coz Stone Is In '" + Val.ToString(CmbCurrLabResultStatus.Text) + "' ");
                    return;
                }


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

                if (Val.Val(txtRate.Text) == 0 && Val.ToString(txtPassForDisplayBack.Tag).ToUpper() != txtPassForDisplayBack.Text.ToUpper())
                {
                    Global.MessageError("Rate Is Required");
                    txtRate.Focus();
                    return;
                }

                if (Val.Val(txtAmount.Text) <= 0 && Val.ToString(txtPassForDisplayBack.Tag).ToUpper() != txtPassForDisplayBack.Text.ToUpper())
                {
                    Global.MessageError("Amount Is Required");
                    txtAmount.Focus();
                    return;
                }

                if (Global.Confirm("Are You Sure To Save Prediction Entry ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                FindRap();
                FindRap_ForRChkRep();

                this.Cursor = Cursors.WaitCursor;


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

                Property.TENSION_ID = Val.ToInt(CmbTension.Tag);
                Property.BLACKINC_ID = Val.ToString(txtBInC.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtBInC.Tag);
                Property.OPENINC_ID = Val.ToString(txtOInC.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtOInC.Tag);
                Property.WHITEINC_ID = Val.ToString(txtWInC.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtWInC.Tag);
                Property.LUSTER_ID = Val.ToString(txtLuster.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtLuster.Tag);
                Property.HA_ID = Val.ToString(txtHA.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtHA.Tag);
                Property.PAV_ID = Val.ToString(txtPav.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtPav.Tag);
                Property.EYECLEAN_ID = Val.ToString(txtEyeclean.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtEyeclean.Tag);
                Property.NATURAL_ID = Val.ToInt(CmbNatural.SelectedValue);
                Property.GRAIN_ID = Val.ToInt(CmbGrain.SelectedValue);

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

                if (Val.ToString(CmbLabSelection.SelectedItem) == "IGI")
                {
                    Property.LAB_ID = 231;
                }
                else
                {
                    Property.LAB_ID = 0;
                }


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

                Property.MDISCOUNT = Val.Val(clsFindRap.MDISCOUNT);
                Property.MPRICEPERCARAT = Val.Val(clsFindRap.MPRICEPERCARAT);
                Property.MAMOUNT = Val.Val(clsFindRap.MAMOUNT);
                Property.MGDISCOUNT = Val.Val(clsFindRap.MGDISCOUNT);
                Property.MGPRICEPERCARAT = Val.Val(clsFindRap.MGPRICEPERCARAT);
                Property.MGAMOUNT = Val.Val(clsFindRap.MGAMOUNT);

                //Add : Pinali : 26-05-2019
                Property.MKAVRAPAPORT = Val.Val(txtMKAVDisc.Text) == 0 ? 0 : Val.Val(clsFindRap.RAPAPORT);
                Property.MKAVDISCOUNT = Val.Val(txtMKAVDisc.Text);
                Property.MKAVPRICEPERCARAT = Val.Val(txtMKAVPricePerCarat.Text);
                Property.MKAVAMOUNT = Val.Val(txtMKAVAmount.Text);

                Property.EXPRAPAPORT = Val.Val(txtExpDiscount.Text) == 0 ? 0 : Val.Val(clsFindRap.RAPAPORT);
                Property.EXPDISCOUNT = Val.Val(txtExpDiscount.Text);
                Property.EXPPRICEPERCARAT = Val.Val(txtExpPricePerCarat.Text);
                Property.EXPAMOUNT = Val.Val(txtExpAmount.Text);

                Property.RAPNETRAPAPORT = Val.Val(txtRapnetDiscount.Text) == 0 ? 0 : Val.Val(clsFindRap.RAPAPORT);
                Property.RAPNETDISCOUNT = Val.Val(txtRapnetDiscount.Text);
                Property.RAPNETPRICEPERCARAT = Val.Val(txtRapnetPricePerCarat.Text);
                Property.RAPNETAMOUNT = Val.Val(txtRapnetAmount.Text);

                Property.RAPNETLINK = Val.ToString(txtRapnetLink.Text);
                Property.LABRESULTSTATUS = Val.ToString(CmbLabResultStatus.Text);
                Property.CURRENTLABRESULTSTATUS = Val.ToString(CmbCurrLabResultStatus.Text) != "RECHECKCONFIRM" || Val.ToString(CmbCurrLabResultStatus.Text) != "REPAIRINGCONFIRM" ? Val.ToString(CmbLabResultStatus.Text) : Val.ToString(CmbCurrLabResultStatus.Text);
                //End : Pinali : 26-05-2019

                Property.ISNOBGM = ChkNOBGM.Checked;
                Property.ISNOBLACK = ChkNOBlack.Checked;

                Property.ENTRYMODE = lblMode.Text;

                Property.ISPCNGRDBYLABENTRY = Val.ToBoolean(ChkISPcnGrdByLabEntry.Checked);
                Property.PCNGRDBYLAB_ID = Val.ToInt64(ChkISPcnGrdByLabEntry.Tag) == 0 ? 0 : Val.ToInt64(ChkISPcnGrdByLabEntry.Tag);
                Property.REPORTNO = Val.ToString(mStrReportNo);

                Property = ObjRap.Save(Property);

                string StrReturnType = Property.ReturnMessageType;
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

                if (StrReturnType == "SUCCESS")
                {
                    if (Val.ToInt(CmbPrdType.Tag) == 14 && CmbLabResultStatus.Text == "RECHECK")
                    {
                        SaveRChkRepData(17, "LAB INCLUSION-RECHECK");
                    }
                    else if (Val.ToInt(CmbPrdType.Tag) == 14 && CmbLabResultStatus.Text == "REPAIRING")
                    {
                        SaveRChkRepData(18, "LAB INCLUSION-REPAIRING");
                    }
                }
                this.Cursor = Cursors.Default;

                Global.Message("****  " + Val.ToString(CmbPrdType.SelectedItem) + "   *****\n\nSUCCESSFULLY SAVED OF " + txtKapanName.Text + "/" + txtPacketNo.Text + "/" + txtTag.Text);

                BtnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        public void SaveRChkRepData(int IntRChkPrdType_ID, string pStrPrdType)
        {
            if (lblMode.Text == "Edit Mode")
            {
                ObjRap.DeleteAll(IntRChkPrdType_ID, txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, Val.ToInt64(txtEmployee.Tag), Val.ToString(txtTag.Tag), mISTFlag);
            }


            Int64 IntRChkPrdID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.PRD_ID);

            Trn_RapSaveProperty PropertyRChk = new Trn_RapSaveProperty();
            PropertyRChk.PACKET_ID = Val.ToInt64(txtTag.Tag);
            PropertyRChk.PRDTYPE_ID = Val.ToInt(IntRChkPrdType_ID);
            PropertyRChk.PRDTYPE = Val.ToString(pStrPrdType);
            PropertyRChk.KAPANNAME = Val.ToString(txtKapanName.Text);
            PropertyRChk.PACKETNO = Val.ToInt(txtPacketNo.Text);
            PropertyRChk.MTAG = txtTag.Text;
            PropertyRChk.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
            PropertyRChk.MANAGER_ID = 0;

            PropertyRChk.ID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.ID); 
            PropertyRChk.PRD_ID = IntRChkPrdID;

            PropertyRChk.TAGSRNO = 1;
            PropertyRChk.TAG = txtTag.Text;
            PropertyRChk.PLANNO = 1;

            PropertyRChk.SHAPE_ID = Val.ToInt(txtRChkShape.Tag);
            PropertyRChk.CLARITY_ID = Val.ToInt(txtRChkClarity.Tag);
            PropertyRChk.COLOR_ID = Val.ToInt(txtRChkColor.Tag);
            //Property.COLORSHADE_ID = 0;                   //Changed : 05-02-2020
            PropertyRChk.COLORSHADE_ID = Val.ToInt(txtRChkColorShade.Tag);
            PropertyRChk.CUT_ID = Val.ToInt(txtRChkCut.Tag);
            PropertyRChk.POL_ID = Val.ToInt(txtRChkPol.Tag);
            PropertyRChk.SYM_ID = Val.ToInt(txtRChkSym.Tag);
            PropertyRChk.FL_ID = Val.ToInt(txtRChkFL.Tag);
            PropertyRChk.MILKY_ID = Val.ToInt(txtRChkMilky.Tag);
            PropertyRChk.LBLC_ID = Val.ToInt(txtLBLC.Tag);
            PropertyRChk.NATTS_ID = Val.ToInt(txtNatts.Tag);

            PropertyRChk.TENSION_ID = Val.ToInt(CmbTension.Tag);
            PropertyRChk.BLACKINC_ID = Val.ToString(txtRChkBInC.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtRChkBInC.Tag);
            PropertyRChk.OPENINC_ID = Val.ToString(txtRChkOInC.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtRChkOInC.Tag);
            PropertyRChk.WHITEINC_ID = Val.ToString(txtRChkWInC.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtRChkWInC.Tag);
            PropertyRChk.LUSTER_ID = Val.ToString(txtRChkLuster.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtRChkLuster.Tag);
            PropertyRChk.HA_ID = Val.ToString(txtRChkHA.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtRChkHA.Tag);
            PropertyRChk.PAV_ID = Val.ToString(txtRChkPav.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtRChkPav.Tag);
            PropertyRChk.EYECLEAN_ID = Val.ToString(txtRChkEyeclean.Text).Trim().Equals(string.Empty) ? 0 : Val.ToInt(txtRChkEyeclean.Tag);
            PropertyRChk.NATURAL_ID = Val.ToInt(CmbNatural.SelectedValue);
            PropertyRChk.GRAIN_ID = Val.ToInt(CmbGrain.SelectedValue);

            PropertyRChk.CARAT = Val.Val(txtRChkCarat.Text);
            PropertyRChk.DISCOUNT = Val.Val(txtRChkDiscount.Text);
            PropertyRChk.AMOUNTDISCOUNT = 0;
            PropertyRChk.RAPAPORT = Val.Val(txtRChkRapaport.Text);
            PropertyRChk.PRICEPERCARAT = Val.Val(txtRChkRate.Text);
            PropertyRChk.AMOUNT = Val.Val(txtRChkAmount.Text);
            PropertyRChk.RAPDATE = Val.SqlDate(Val.ToString(CmbRapDate.SelectedItem));

            PropertyRChk.GCARAT = 0;
            PropertyRChk.GCOLOR_ID = 0;
            PropertyRChk.GCLARITY_ID = 0;
            PropertyRChk.GCUT_ID = 0;
            PropertyRChk.GPOL_ID = 0;
            PropertyRChk.GSYM_ID = 0;

            PropertyRChk.GDISCOUNT = clsFindRapRChk.GDISCOUNT;
            PropertyRChk.GAMOUNTDISCOUNT = clsFindRapRChk.GAMOUNTDISCOUNT;
            PropertyRChk.GRAPAPORT = clsFindRapRChk.GRAPAPORT;
            PropertyRChk.GPRICEPERCARAT = clsFindRapRChk.GPRICEPERCARAT;
            PropertyRChk.GAMOUNT = Math.Round(clsFindRapRChk.GAMOUNT, 0);
            PropertyRChk.ISFINAL = true;

            PropertyRChk.ENTRYDATE = Val.SqlDate(System.DateTime.Now.ToShortDateString());

            if (Val.ToString(CmbLabSelection.SelectedItem) == "IGI")
            {
                PropertyRChk.LAB_ID = 231;
            }
            else
            {
                PropertyRChk.LAB_ID = 0;
            }
            PropertyRChk.UPCOLOR_ID = clsFindRapRChk.UPCOLOR_ID;
            PropertyRChk.UPCOLORDISCOUNT = clsFindRapRChk.UPCOLORDISCOUNT;
            PropertyRChk.UPCOLORAMOUNTDISCOUNT = clsFindRapRChk.UPCOLORAMOUNTDISCOUNT;
            PropertyRChk.UPCOLORRAPAPORT = clsFindRapRChk.UPCOLORRAPAPORT;
            PropertyRChk.UPCOLORPRICEPERCARAT = clsFindRapRChk.UPCOLORPRICEPERCARAT;
            PropertyRChk.UPCOLORAMOUNT = Math.Round(clsFindRapRChk.UPCOLORAMOUNT, 0);

            PropertyRChk.DOWNCOLOR_ID = clsFindRapRChk.DOWNCOLOR_ID;
            PropertyRChk.DOWNCOLORDISCOUNT = clsFindRapRChk.DOWNCOLORDISCOUNT;
            PropertyRChk.DOWNCOLORAMOUNTDISCOUNT = clsFindRapRChk.DOWNCOLORAMOUNTDISCOUNT;
            PropertyRChk.DOWNCOLORRAPAPORT = clsFindRapRChk.DOWNCOLORRAPAPORT;
            PropertyRChk.DOWNCOLORPRICEPERCARAT = clsFindRapRChk.DOWNCOLORPRICEPERCARAT;
            PropertyRChk.DOWNCOLORAMOUNT = Math.Round(clsFindRapRChk.DOWNCOLORAMOUNT, 0);

            PropertyRChk.UPCLARITY_ID = clsFindRapRChk.UPCLARITY_ID;
            PropertyRChk.UPCLARITYDISCOUNT = clsFindRapRChk.UPCLARITYDISCOUNT;
            PropertyRChk.UPCLARITYAMOUNTDISCOUNT = clsFindRapRChk.UPCLARITYAMOUNTDISCOUNT;
            PropertyRChk.UPCLARITYRAPAPORT = clsFindRapRChk.UPCLARITYRAPAPORT;
            PropertyRChk.UPCLARITYPRICEPERCARAT = clsFindRapRChk.UPCLARITYPRICEPERCARAT;
            PropertyRChk.UPCLARITYAMOUNT = Math.Round(clsFindRap.UPCLARITYAMOUNT, 0);

            PropertyRChk.DOWNCLARITY_ID = clsFindRapRChk.DOWNCLARITY_ID;
            PropertyRChk.DOWNCLARITYDISCOUNT = clsFindRapRChk.DOWNCLARITYDISCOUNT;
            PropertyRChk.DOWNCLARITYAMOUNTDISCOUNT = clsFindRapRChk.DOWNCLARITYAMOUNTDISCOUNT;
            PropertyRChk.DOWNCLARITYRAPAPORT = clsFindRapRChk.DOWNCLARITYRAPAPORT;
            PropertyRChk.DOWNCLARITYPRICEPERCARAT = clsFindRapRChk.DOWNCLARITYPRICEPERCARAT;
            PropertyRChk.DOWNCLARITYAMOUNT = Math.Round(clsFindRapRChk.DOWNCLARITYAMOUNT, 0);


            PropertyRChk.COPYFROMEMPLOYEE_ID = 0;
            PropertyRChk.COPYFROMPRD_ID = null;
            PropertyRChk.COPYFROM_ID = null;
            PropertyRChk.COPYTOEMPLOYEE_ID = 0;
            PropertyRChk.COPYTOPRD_ID = null;
            PropertyRChk.COPYTO_ID = null;
            PropertyRChk.ISDIFF = false;
            PropertyRChk.REMARK = txtRemark.Text;

            PropertyRChk.LABPROCESS = Val.ToString(CmbRChkLabProcess.SelectedItem);
            PropertyRChk.LABSELECTION = Val.ToString(CmbRChkLabSelection.SelectedItem);
            PropertyRChk.DIAMIN = Val.Val(txtRChkDiaMin.Text);
            PropertyRChk.DIAMAX = Val.Val(txtRChkDiaMax.Text);
            PropertyRChk.HEIGHT = Val.Val(txtRChkHeight.Text);

            PropertyRChk.ISMIXRATE = Val.ToBoolean(clsFindRapRChk.ISMIXRATE);

            PropertyRChk.MDISCOUNT = Val.Val(clsFindRapRChk.MDISCOUNT);
            PropertyRChk.MPRICEPERCARAT = Val.Val(clsFindRapRChk.MPRICEPERCARAT);
            PropertyRChk.MAMOUNT = Val.Val(clsFindRapRChk.MAMOUNT);
            PropertyRChk.MGDISCOUNT = Val.Val(clsFindRapRChk.MGDISCOUNT);
            PropertyRChk.MGPRICEPERCARAT = Val.Val(clsFindRapRChk.MGPRICEPERCARAT);
            PropertyRChk.MGAMOUNT = Val.Val(clsFindRapRChk.MGAMOUNT);

            //Add : Pinali : 26-05-2019
            PropertyRChk.MKAVRAPAPORT = Val.Val(txtMKAVDisc.Text) == 0 ? 0 : Val.Val(clsFindRapRChk.RAPAPORT);
            PropertyRChk.MKAVDISCOUNT = Val.Val(txtMKAVDisc.Text);
            PropertyRChk.MKAVPRICEPERCARAT = Val.Val(txtMKAVPricePerCarat.Text);
            PropertyRChk.MKAVAMOUNT = Val.Val(txtMKAVAmount.Text);

            PropertyRChk.EXPRAPAPORT = Val.Val(txtExpDiscount.Text) == 0 ? 0 : Val.Val(clsFindRapRChk.RAPAPORT);
            PropertyRChk.EXPDISCOUNT = Val.Val(txtExpDiscount.Text);
            PropertyRChk.EXPPRICEPERCARAT = Val.Val(txtExpPricePerCarat.Text);
            PropertyRChk.EXPAMOUNT = Val.Val(txtExpAmount.Text);

            PropertyRChk.RAPNETRAPAPORT = Val.Val(txtRapnetDiscount.Text) == 0 ? 0 : Val.Val(clsFindRapRChk.RAPAPORT);
            PropertyRChk.RAPNETDISCOUNT = Val.Val(txtRapnetDiscount.Text);
            PropertyRChk.RAPNETPRICEPERCARAT = Val.Val(txtRapnetPricePerCarat.Text);
            PropertyRChk.RAPNETAMOUNT = Val.Val(txtRapnetAmount.Text);

            PropertyRChk.RAPNETLINK = Val.ToString(txtRapnetLink.Text);
            PropertyRChk.LABRESULTSTATUS = Val.ToString(CmbLabResultStatus.Text);
            PropertyRChk.CURRENTLABRESULTSTATUS = Val.ToString(CmbCurrLabResultStatus.Text) != "RECHECKCONFIRM" || Val.ToString(CmbCurrLabResultStatus.Text) != "REPAIRINGCONFIRM" ? Val.ToString(CmbLabResultStatus.Text) : Val.ToString(CmbCurrLabResultStatus.Text);
            PropertyRChk.RCHKREPDIFFAMOUNT = Val.Val(txtRChkDiffAmount.Text);
            PropertyRChk.RCHKREPDIFFPER = Val.Val(txtRChkDiffPer.Text);
            PropertyRChk.RCHKREPCOMMENT = Val.ToString(txtRchkRepComment.Text);
            //End : Pinali : 26-05-2019

            PropertyRChk.ISNOBGM = ChkRChkNoBGM.Checked;
            PropertyRChk.ISNOBLACK = ChkNOBlack.Checked;

            PropertyRChk.ENTRYMODE = lblMode.Text;

            PropertyRChk.ISPCNGRDBYLABENTRY = Val.ToBoolean(ChkISPcnGrdByLabEntry.Checked);
            PropertyRChk.PCNGRDBYLAB_ID = Val.ToInt64(ChkISPcnGrdByLabEntry.Tag)== 0 ? 0 : Val.ToInt64(ChkISPcnGrdByLabEntry.Tag);
            PropertyRChk.REPORTNO = Val.ToString(mStrReportNo);

            PropertyRChk = ObjRap.Save(PropertyRChk);

            PropertyRChk = null;


            // Method Is For Update Max Flag In Table

            Trn_RapSaveProperty PropertySum = new Trn_RapSaveProperty();

            PropertySum.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
            PropertySum.KAPANNAME = Val.ToString(txtKapanName.Text);
            PropertySum.PACKETNO = Val.ToInt(txtPacketNo.Text);
            PropertySum.MTAG = txtTag.Text;
            PropertySum.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
            PropertySum.PRD_ID = IntRChkPrdID;
            PropertySum = ObjRap.SaveMaxAmountFlag(PropertySum);

            PropertySum = null;
        }

        public void Clear() //Add : Pinali : 04-11-2019
        {
            try
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

                CmbTension.SelectedIndex = 0;
                CmbNatural.SelectedIndex = 0;
                CmbGrain.SelectedIndex = 0;

                txtBInC.Tag = string.Empty;
                txtBInC.Text = string.Empty;
                txtBInC.AccessibleDescription = string.Empty;

                txtOInC.Tag = string.Empty;
                txtOInC.Text = string.Empty;
                txtOInC.AccessibleDescription = string.Empty;

                txtWInC.Tag = string.Empty;
                txtWInC.Text = string.Empty;
                txtWInC.AccessibleDescription = string.Empty;

                txtPav.Tag = string.Empty;
                txtPav.Text = string.Empty;
                txtPav.AccessibleDescription = string.Empty;

                txtLuster.Tag = string.Empty;
                txtLuster.Text = string.Empty;
                txtLuster.AccessibleDescription = string.Empty;

                txtEyeclean.Tag = string.Empty;
                txtEyeclean.Text = string.Empty;
                txtEyeclean.AccessibleDescription = string.Empty;

                txtHA.Tag = string.Empty;
                txtHA.Text = string.Empty;
                txtHA.AccessibleDescription = string.Empty;

                txtColorShade.Text = string.Empty;
                txtColorShade.Tag = string.Empty;
                txtColorShade.AccessibleDescription = string.Empty;

                txtCarat.Text = string.Empty;

                ChkNOBGM.Checked = false;
                ChkNOBlack.Checked = false;

                CmbLabSelection.SelectedIndex = 0;
                CmbLabProcess.SelectedIndex = 0;
                CmbCurrLabResultStatus.SelectedIndex = 0;
                CmbLabResultStatus.SelectedIndex = 0;


                txtDiaMax.Text = string.Empty;
                txtDiaMin.Text = string.Empty;
                txtHeight.Text = string.Empty;

                txtRapaport.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;

                txtGiaNonGia.Text = string.Empty;

                txtMKAVDisc.Text = string.Empty;
                txtMKAVPricePerCarat.Text = string.Empty;
                txtMKAVAmount.Text = string.Empty;

                txtExpDiscount.Text = string.Empty;
                txtExpPricePerCarat.Text = string.Empty;
                txtExpAmount.Text = string.Empty;

                txtRapnetDiscount.Text = string.Empty;
                txtRapnetPricePerCarat.Text = string.Empty;
                txtRapnetAmount.Text = string.Empty;
                txtRapnetLink.Text = string.Empty;

                MainGrid.DataSource = null;

                lblLot.Text = "0.00";
                lblBalance.Text = "0.00";

                txtShape.Enabled = false;
                txtColor.Enabled = false;
                txtClarity.Enabled = false;
                txtCut.Enabled = false;
                txtPol.Enabled = false;
                txtSym.Enabled = false;
                txtFL.Enabled = false;
                txtCarat.Enabled = false;
                CmbSheetName.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        public void ClearReCheckRepControl()
        {
            try
            {
                txtRChkShape.Text = string.Empty;
                txtRChkShape.Tag = string.Empty;
                txtRChkShape.AccessibleDescription = string.Empty;

                txtRChkColor.Text = string.Empty;
                txtRChkColor.Tag = string.Empty;
                txtRChkColor.AccessibleDescription = string.Empty;

                txtRChkClarity.Text = string.Empty;
                txtRChkClarity.Tag = string.Empty;
                txtRChkClarity.AccessibleDescription = string.Empty;

                txtRChkCut.Text = string.Empty;
                txtRChkCut.Tag = string.Empty;
                txtRChkCut.AccessibleDescription = string.Empty;

                txtRChkPol.Text = string.Empty;
                txtRChkPol.Tag = string.Empty;
                txtRChkPol.AccessibleDescription = string.Empty;

                txtRChkSym.Text = string.Empty;
                txtRChkSym.Tag = string.Empty;
                txtRChkSym.AccessibleDescription = string.Empty;

                txtRChkFL.Text = string.Empty;
                txtRChkFL.Tag = string.Empty;
                txtRChkFL.AccessibleDescription = string.Empty;

                txtRChkMilky.Text = string.Empty;
                txtRChkMilky.Tag = string.Empty;
                txtRChkMilky.AccessibleDescription = string.Empty;

                txtRChkBInC.Text = string.Empty;
                txtRChkBInC.Tag = string.Empty;
                txtRChkBInC.AccessibleDescription = string.Empty;

                txtRChkOInC.Text = string.Empty;
                txtRChkOInC.Tag = string.Empty;
                txtRChkOInC.AccessibleDescription = string.Empty;

                txtRChkWInC.Text = string.Empty;
                txtRChkWInC.Tag = string.Empty;
                txtRChkWInC.AccessibleDescription = string.Empty;

                txtRChkPav.Text = string.Empty;
                txtRChkPav.Tag = string.Empty;
                txtRChkPav.AccessibleDescription = string.Empty;

                txtRChkLuster.Text = string.Empty;
                txtRChkLuster.Tag = string.Empty;
                txtRChkLuster.AccessibleDescription = string.Empty;

                txtRChkEyeclean.Text = string.Empty;
                txtRChkEyeclean.Tag = string.Empty;
                txtRChkEyeclean.AccessibleDescription = string.Empty;

                txtRChkHA.Text = string.Empty;
                txtRChkHA.Tag = string.Empty;
                txtRChkHA.AccessibleDescription = string.Empty;

                txtRChkColorShade.Text = string.Empty;
                txtRChkColorShade.Tag = string.Empty;
                txtRChkColorShade.AccessibleDescription = string.Empty;

                txtRChkCarat.Text = string.Empty;

                CmbRChkLabProcess.SelectedIndex = 0;
                txtRChkDiaMax.Text = string.Empty;
                txtRChkDiaMin.Text = string.Empty;
                txtRChkHeight.Text = string.Empty;

                txtRChkRapaport.Text = string.Empty;
                txtRChkDiscount.Text = string.Empty;
                txtRChkRate.Text = string.Empty;
                txtRChkAmount.Text = string.Empty;

                txtRChkDiffAmount.Text = string.Empty;
                txtRChkDiffPer.Text = string.Empty;
                txtRchkRepComment.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Clear();
            ClearReCheckRepControl();
            txtKapanName.Focus();
            mISTFlag = false;
            this.Cursor = Cursors.Default;
        }

        #endregion

        public void CopyPasteRchkRepData()
        {
            ClearReCheckRepControl();
            txtRChkShape.AccessibleDescription = txtShape.AccessibleDescription;
            txtRChkShape.Text = txtShape.Text;
            txtRChkShape.Tag = txtShape.Tag;

            txtRChkColor.AccessibleDescription = txtColor.AccessibleDescription;
            txtRChkColor.Text = txtColor.Text;
            txtRChkColor.Tag = txtColor.Tag;

            txtRChkClarity.AccessibleDescription = txtClarity.AccessibleDescription;
            txtRChkClarity.Text = txtClarity.Text;
            txtRChkClarity.Tag = txtClarity.Tag;

            txtRChkCarat.Text = txtCarat.Text;

            txtRChkCut.AccessibleDescription = txtCut.AccessibleDescription;
            txtRChkCut.Text = txtCut.Text;
            txtRChkCut.Tag = txtCut.Tag;

            txtRChkPol.AccessibleDescription = txtPol.AccessibleDescription;
            txtRChkPol.Text = txtPol.Text;
            txtRChkPol.Tag = txtPol.Tag;

            txtRChkSym.AccessibleDescription = txtSym.AccessibleDescription;
            txtRChkSym.Text = txtSym.Text;
            txtRChkSym.Tag = txtSym.Tag;

            txtRChkFL.AccessibleDescription = txtFL.AccessibleDescription;
            txtRChkFL.Text = txtFL.Text;
            txtRChkFL.Tag = txtFL.Tag;

            txtRChkDiaMin.Text = txtDiaMin.Text;
            txtRChkDiaMax.Text = txtDiaMax.Text;
            txtRChkHeight.Text = txtHeight.Text;

            CmbRChkLabProcess.Text = CmbLabProcess.Text;
            CmbRChkLabSelection.Text = CmbLabSelection.Text;

            txtRChkColorShade.AccessibleDescription = txtColorShade.AccessibleDescription;
            txtRChkColorShade.Text = txtColorShade.Text;
            txtRChkColorShade.Tag = txtColorShade.Tag;

            txtRChkMilky.AccessibleDescription = txtMilky.AccessibleDescription;
            txtRChkMilky.Text = txtMilky.Text;
            txtRChkMilky.Tag = txtMilky.Tag;

            txtRChkBInC.AccessibleDescription = txtBInC.AccessibleDescription;
            txtRChkBInC.Text = txtBInC.Text;
            txtRChkBInC.Tag = txtBInC.Tag;

            txtRChkOInC.AccessibleDescription = txtOInC.AccessibleDescription;
            txtRChkOInC.Text = txtOInC.Text;
            txtRChkOInC.Tag = txtOInC.Tag;

            txtRChkWInC.AccessibleDescription = txtWInC.AccessibleDescription;
            txtRChkWInC.Text = txtWInC.Text;
            txtRChkWInC.Tag = txtWInC.Tag;

            txtRChkPav.AccessibleDescription = txtPav.AccessibleDescription;
            txtRChkPav.Text = txtPav.Text;
            txtRChkPav.Tag = txtPav.Tag;

            txtRChkLuster.AccessibleDescription = txtLuster.AccessibleDescription;
            txtRChkLuster.Text = txtLuster.Text;
            txtRChkLuster.Tag = txtLuster.Tag;

            txtRChkEyeclean.AccessibleDescription = txtEyeclean.AccessibleDescription;
            txtRChkEyeclean.Text = txtEyeclean.Text;
            txtRChkEyeclean.Tag = txtEyeclean.Tag;

            txtRChkHA.AccessibleDescription = txtHA.AccessibleDescription;
            txtRChkHA.Text = txtHA.Text;
            txtRChkHA.Tag = txtHA.Tag;

            txtRChkRapaport.Text = txtRapaport.Text;
            txtRChkDiscount.Text = txtDiscount.Text;
            txtRChkRate.Text = txtRate.Text;
            txtRChkAmount.Text = txtAmount.Text;

            //CmbRChkBlackInc.AccessibleDescription = CmbBlackInc.AccessibleDescription;
            //CmbRChkBlackInc.AccessibleName = CmbBlackInc.AccessibleName;
            //CmbRChkBlackInc.Text = CmbBlackInc.Text;
            //CmbRChkBlackInc.Tag = CmbBlackInc.Tag;

        }

        public void ConsiderBGMNonBGM(Int32 IntMilky_ID, Int32 IntColorShade_ID, string StrEntryType) //Add : Pinali : 01-06-2020
        {
            if (Val.ToString(txtKapanName.Text).Trim().Length == 0 || Val.ToString(txtPacketNo.Text).Trim().Length == 0 || Val.ToString(txtTag.Text).Trim().Length == 0)
            {
                return;
            }


            if (StrEntryType == "RECHECKREP")
            {
                if ((Val.ToInt32(IntMilky_ID) == 0 || Val.ToInt32(IntMilky_ID) == 276)
                &&
                (Val.ToInt32(IntColorShade_ID) == 0 || Val.ToInt32(IntColorShade_ID) == 98 || Val.ToInt32(IntColorShade_ID) == 104 || Val.ToInt32(IntColorShade_ID) == 106 || Val.ToInt32(IntColorShade_ID) == 1650))
                {
                    ChkRChkNoBGM.Checked = true;
                }
                else
                {
                    ChkRChkNoBGM.Checked = false;
                }
            }
            else
            {
                //Milky -> Blank Or None ANd ColorShade -> Blank,N,White,Yellow,BRN0 Hoy to NoBGM Otherwise BGM
                if ((Val.ToInt32(IntMilky_ID) == 0 || Val.ToInt32(IntMilky_ID) == 276)
                    &&
                    (Val.ToInt32(IntColorShade_ID) == 0 || Val.ToInt32(IntColorShade_ID) == 98 || Val.ToInt32(IntColorShade_ID) == 104 || Val.ToInt32(IntColorShade_ID) == 106 || Val.ToInt32(IntColorShade_ID) == 1650))
                {
                    ChkNOBGM.Checked = true;
                }
                else
                {
                    ChkNOBGM.Checked = false;
                }
            }
        }

        public void FindRap()
        {
            try
            {
                if (Val.ToString(txtKapanName.Text).Trim().Length == 0 || Val.ToString(txtPacketNo.Text).Trim().Length == 0 || Val.ToString(txtTag.Text).Trim().Length == 0)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                ConsiderBGMNonBGM(Val.ToInt(txtMilky.Tag), Val.ToInt(txtColorShade.Tag), "ORIGINAL");

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

                //clsFindRap.BLACKINCCODE = "";
                //clsFindRap.OPENINCCODE = "";
                //clsFindRap.WHITEINCCODE = "";
                //clsFindRap.PAVCODE = "";
                //clsFindRap.EYECLEANCODE = "";
                //clsFindRap.LUSTERCODE = "";
                //clsFindRap.NATURALCODE = "";
                //clsFindRap.GRAINCODE = "";

                clsFindRap.BLACKINCCODE = Val.ToString(txtBInC.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtBInC.AccessibleDescription);
                clsFindRap.OPENINCCODE = Val.ToString(txtOInC.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtOInC.AccessibleDescription);
                clsFindRap.WHITEINCCODE = Val.ToString(txtWInC.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtWInC.AccessibleDescription);
                clsFindRap.PAVCODE = Val.ToString(txtPav.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtPav.AccessibleDescription);
                clsFindRap.EYECLEANCODE = Val.ToString(txtEyeclean.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtEyeclean.AccessibleDescription);
                clsFindRap.LUSTERCODE = Val.ToString(txtLuster.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtLuster.AccessibleDescription);

                clsFindRap.NATURALCODE = Val.ToString(CmbNatural.AccessibleName);
                clsFindRap.GRAINCODE = Val.ToString(CmbGrain.AccessibleName);


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

                txtDiscount.Text = clsFindRap.DISCOUNT.ToString();
                txtRate.Text = clsFindRap.PRICEPERCARAT.ToString();
                txtAmount.Text = Math.Round(clsFindRap.AMOUNT, 0).ToString();

                txtRapaport.Text = clsFindRap.RAPAPORT.ToString();

                txtGiaNonGia.Text = Val.ToString(clsFindRap.GIANONGIA);

                Calculation();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
            this.Cursor = Cursors.Default;

        }

        public void FindRap_ForRChkRep()
        {
            try
            {
                if (Val.ToString(txtKapanName.Text).Trim().Length == 0 || Val.ToString(txtPacketNo.Text).Trim().Length == 0 || Val.ToString(txtTag.Text).Trim().Length == 0)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                ConsiderBGMNonBGM(Val.ToInt(txtRChkMilky.Tag), Val.ToInt(txtRChkColorShade.Tag), "RECHECKREP");

                clsFindRapRChk = new Trn_RapSaveProperty();

                clsFindRapRChk.SHAPECODE = Val.ToString(txtRChkShape.AccessibleDescription);

                clsFindRapRChk.COLOR_ID = Val.ToInt32(txtRChkColor.Tag);
                clsFindRapRChk.COLORCODE = Val.ToString(txtRChkColor.AccessibleDescription);

                clsFindRapRChk.CLARITY_ID = Val.ToInt32(txtRChkClarity.Tag);
                clsFindRapRChk.CLARITYCODE = Val.ToString(txtRChkClarity.AccessibleDescription);

                clsFindRapRChk.CARAT = Val.Val(txtRChkCarat.Text);
                clsFindRapRChk.CUTCODE = Val.ToString(txtRChkCut.AccessibleDescription);
                clsFindRapRChk.POLCODE = Val.ToString(txtRChkPol.AccessibleDescription);
                clsFindRapRChk.SYMCODE = Val.ToString(txtRChkSym.AccessibleDescription);

                clsFindRapRChk.GCARAT = 0;
                clsFindRapRChk.GCUTCODE = "";
                clsFindRapRChk.GPOLCODE = "";
                clsFindRapRChk.GSYMCODE = "";

                clsFindRapRChk.FLCODE = Val.ToString(txtRChkFL.AccessibleDescription);
                clsFindRapRChk.MILKYCODE = Val.ToString(txtRChkMilky.AccessibleDescription);
                clsFindRapRChk.NATTSCODE = Val.ToString(txtNatts.AccessibleDescription);
                clsFindRapRChk.LBLCCODE = Val.ToString(txtLBLC.AccessibleDescription);
                clsFindRapRChk.RAPDATE = Val.SqlDate(CmbRapDate.SelectedItem.ToString());

                //clsFindRap.COLORSHADECODE = "";
                clsFindRapRChk.COLORSHADECODE = Val.ToString(txtRChkColorShade.AccessibleDescription);

                //clsFindRap.BLACKINCCODE = "";
                //clsFindRap.OPENINCCODE = "";
                //clsFindRap.WHITEINCCODE = "";
                //clsFindRap.PAVCODE = "";
                //clsFindRap.EYECLEANCODE = "";
                //clsFindRap.LUSTERCODE = "";
                //clsFindRap.NATURALCODE = "";
                //clsFindRap.GRAINCODE = "";

                clsFindRapRChk.BLACKINCCODE = Val.ToString(txtRChkBInC.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtRChkBInC.AccessibleDescription);
                clsFindRapRChk.OPENINCCODE = Val.ToString(txtRChkOInC.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtRChkOInC.AccessibleDescription);
                clsFindRapRChk.WHITEINCCODE = Val.ToString(txtRChkWInC.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtRChkWInC.AccessibleDescription);
                clsFindRapRChk.PAVCODE = Val.ToString(txtRChkPav.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtRChkPav.AccessibleDescription);
                clsFindRapRChk.EYECLEANCODE = Val.ToString(txtRChkEyeclean.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtRChkEyeclean.AccessibleDescription);
                clsFindRapRChk.LUSTERCODE = Val.ToString(txtRChkLuster.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(txtRChkLuster.AccessibleDescription);
                clsFindRapRChk.NATURALCODE = Val.ToString(CmbNatural.AccessibleName);
                clsFindRapRChk.GRAINCODE = Val.ToString(CmbGrain.AccessibleName);

                if (Val.ToString(CmbRChkLabSelection.SelectedItem) == "IGI")
                {
                    clsFindRapRChk.LABCODE = "IGI";
                }
                else
                {
                    clsFindRapRChk.LABCODE = "";
                }

                if (clsFindRapRChk.SHAPECODE == "" || clsFindRapRChk.COLORCODE == "" || clsFindRapRChk.CLARITYCODE == "")
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                clsFindRapRChk = ObjRap.FindRapWithUpDown(clsFindRapRChk);

                txtRChkRapaport.Text = clsFindRapRChk.RAPAPORT.ToString();
                txtRChkDiscount.Text = clsFindRapRChk.DISCOUNT.ToString();
                txtRChkRate.Text = clsFindRapRChk.PRICEPERCARAT.ToString();
                txtRChkAmount.Text = Math.Round(clsFindRapRChk.AMOUNT, 0).ToString();

                txtRChkGIANonGIA.Text = Val.ToString(clsFindRap.GIANONGIA);
                txtRChkAmount_Validated(null, null);
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
            try
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

                if ((Val.ToInt(CmbPrdType.Tag) == 14 || Val.ToInt(CmbPrdType.Tag) == 15 || Val.ToInt(CmbPrdType.Tag) == 16) && Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
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



                DataTable DTab = ObjRap.GetSinglePrdLabPricingData(Val.ToInt32(CmbPrdType.Tag), Val.ToString(txtTag.Tag), IntEmployeeID, 0);
                lblMode.Text = "Add Mode";
                if (DTab.Rows.Count == 0 || Val.ToString(DTab.Rows[0]["ENTRYTYPE"]) == "FRESH")
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

                    //Check Packet Current LabResult Status : #P : 29-05-2020
                    Trn_RapSaveProperty PropertyStatus = new Trn_RapSaveProperty();
                    PropertyStatus.PACKET_ID = Val.ToInt64(txtTag.Tag);
                    PropertyStatus.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                    PropertyStatus = ObjRap.CheckLabPricingValdiation(PropertyStatus);
                    if (PropertyStatus.ReturnMessageType == "FAIL")
                    {
                        BtnSave.Enabled = false;
                        this.Cursor = Cursors.Default;
                        Global.MessageError(PropertyStatus.ReturnMessageDesc);
                        return;
                    }
                    PropertyStatus = null;
                    //End : Check Packet Current LabResult Status : #P : 29-05-2020


                    if (Val.ToString(CmbPrdType.Tag) == "14")
                        lblGrading.Text = "LAB GRADING Data";
                    else if (Val.ToString(CmbPrdType.Tag) == "15")
                        lblGrading.Text = "LAB INCLUSION Data";
                    else if (Val.ToString(CmbPrdType.Tag) == "16")
                        lblGrading.Text = "LAB FINAL PRICING Data";

                    //// FETCH GRADING FIELDS
                    //DTab = ObjRap.GetSinglePrdLabPricingData(8, Val.ToString(txtTag.Tag), Val.ToInt64(txtEmployee.Tag), 0);

                    if (Val.ToInt(CmbPrdType.Tag) == 14)
                    {
                        DTab = ObjRap.GetSinglePrdLabPricingData(11, Val.ToString(txtTag.Tag), 0, 0);
                    }
                    else if (Val.ToInt(CmbPrdType.Tag) == 15)
                    {
                        DTab = ObjRap.GetSinglePrdLabPricingData(14, Val.ToString(txtTag.Tag), 0, 0);
                    }
                    else if (Val.ToInt(CmbPrdType.Tag) == 16)
                    {
                        DTabPrcLvl1Det = ObjRap.GetSinglePrdLabPricingData(15, Val.ToString(txtTag.Tag), 0, 0);
                        if (DTabPrcLvl1Det.Rows.Count > 0)
                        {
                            DTab = DTabPrcLvl1Det.Select("RNo = 1", "").CopyToDataTable();
                            MainGrid.DataSource = DTabPrcLvl1Det;
                            GrdDet.RefreshData();
                            GrdDet.BestFitColumns();

                        }
                        else
                        {
                            DTabPrcLvl1Det = ObjRap.GetSinglePrdLabPricingData(14, Val.ToString(txtTag.Tag), 0, 0);
                            if (DTabPrcLvl1Det.Rows.Count > 0)
                            {
                                DTab = DTabPrcLvl1Det.Copy();
                                MainGrid.DataSource = DTabPrcLvl1Det;
                                GrdDet.RefreshData();
                                GrdDet.BestFitColumns();
                            }
                            else
                                MainGrid.DataSource = null;
                        }

                    }
                    else
                        MainGrid.DataSource = null;

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

                //#P : 27-05-2020
                txtBInC.Tag = string.Empty;
                txtBInC.AccessibleDescription = string.Empty;
                txtBInC.Text = string.Empty;

                txtOInC.Tag = string.Empty;
                txtOInC.AccessibleDescription = string.Empty;
                txtOInC.Text = string.Empty;

                txtWInC.Tag = string.Empty;
                txtWInC.AccessibleDescription = string.Empty;
                txtWInC.Text = string.Empty;

                txtPav.Tag = string.Empty;
                txtPav.AccessibleDescription = string.Empty;
                txtPav.Text = string.Empty;

                txtHA.Tag = string.Empty;
                txtHA.AccessibleDescription = string.Empty;
                txtHA.Text = string.Empty;

                txtLuster.Tag = string.Empty;
                txtLuster.AccessibleDescription = string.Empty;
                txtLuster.Text = string.Empty;

                txtEyeclean.Tag = string.Empty;
                txtEyeclean.AccessibleDescription = string.Empty;
                txtEyeclean.Text = string.Empty;

                CmbTension.SelectedIndex = 0;
                CmbNatural.SelectedIndex = 0;
                CmbGrain.SelectedIndex = 0;

                txtMKAVDisc.Text = string.Empty;
                txtMKAVPricePerCarat.Text = string.Empty;
                txtMKAVAmount.Text = string.Empty;

                txtExpDiscount.Text = string.Empty;
                txtExpPricePerCarat.Text = string.Empty;
                txtExpAmount.Text = string.Empty;

                txtRapnetDiscount.Text = string.Empty;
                txtRapnetPricePerCarat.Text = string.Empty;
                txtRapnetAmount.Text = string.Empty;

                txtRapnetLink.Text = string.Empty;
                CmbLabResultStatus.SelectedIndex = 0;
                //End : #P : 27-05-2020

                txtColorShade.Tag = string.Empty;
                txtColorShade.AccessibleDescription = string.Empty;
                txtColorShade.Text = string.Empty;

                CmbRapDate.SelectedItem = string.Empty;

                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtRapaport.Text = string.Empty;

                txtGiaNonGia.Text = string.Empty; //Add : Pinali : 19-09-2019

                BtnSave.Enabled = true;

                //Add : Pinali : 04-11-2019
                ChkISPcnGrdByLabEntry.Checked = false;
                ChkISPcnGrdByLabEntry.Tag = string.Empty;
                mStrReportNo = string.Empty;

                CmbLabResultStatus.SelectedIndex = 0;
                CmbCurrLabResultStatus.SelectedIndex = 0;

                mISTFlag = false;

                //End : Pinali : 04-11-2019

                ClearReCheckRepControl();

                DTabRChkData.Rows.Clear();

             
                if (lblMode.Text == "Edit Mode" && EmployeeRightsProperty.RAPUPDATEPREDICTION == false)
                {
                    BtnSave.Enabled = false;
                }


                if (DTab.Rows.Count != 0)
                {

                    //if (Val.ToInt(CmbPrdType.Tag) == 14 && (Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "RECHECK" || Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "RECHECKCONFIRM" || Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "RECHECKCANCEL")) //Coz LabFinal Price time pr pn recheck ni detail display karvani 
                    if ((Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "RECHECK" || Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "RECHECKCONFIRM" || Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "RECHECKCANCEL"))
                    {

                        DTabRChkData = ObjRap.GetSinglePrdLabPricingData(17, Val.ToString(txtTag.Tag), 0, 0);
                        FetchDataForRChkRep(DTabRChkData);
                    }
                    //else if (Val.ToInt(CmbPrdType.Tag) == 14 && (Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "REPAIRING" || Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "REPAIRINGCONFIRM" || Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "REPAIRINGCANCEL"))
                    else if ((Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "REPAIRING" || Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "REPAIRINGCONFIRM" || Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "REPAIRINGCANCEL"))
                    {
                        DTabRChkData = ObjRap.GetSinglePrdLabPricingData(18, Val.ToString(txtTag.Tag), 0, 0);
                        FetchDataForRChkRep(DTabRChkData);
                    }

                    ////Coz When we select lab pricing and stone is in recheck/Repairing cancel then save labstatus as confirm coz it is non-editable
                    //if ((Val.ToInt(CmbPrdType.Tag) == 15 || Val.ToInt(CmbPrdType.Tag) == 16) && (Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "RECHECKCANCEL"||Val.ToString(DTab.Rows[0]["PKTLABRESULTSTATUS"]) == "REPAIRINGCANCEL")) 
                    //{
                    //    CmbLabResultStatus.SelectedItem = "CONFIRM";
                    //}


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

                    txtColorShade.Tag = Val.ToString(DRow["COLORSHADE_ID"]);
                    txtColorShade.AccessibleDescription = Val.ToString(DRow["COLORSHADECODE"]);
                    txtColorShade.Text = Val.ToString(DRow["COLORSHADENAME"]);

                    //CmbTension.Tag = Val.ToString(DRow["TENSION_ID"]);
                    //CmbTension.Tag = Val.ToString(DRow["TENSIONCODE"]);
                    //CmbTension.Text = Val.ToString(DRow["TENSIONNAME"]);

                    //#P : 27-05-2020
                    txtBInC.Tag = Val.ToString(DRow["BLACKINC_ID"]);
                    txtBInC.AccessibleDescription = Val.ToString(DRow["BLACKINCCODE"]);
                    txtBInC.Text = Val.ToString(DRow["BLACKINCNAME"]);

                    txtOInC.Tag = Val.ToString(DRow["OPENINC_ID"]);
                    txtOInC.AccessibleDescription = Val.ToString(DRow["OPENINCCODE"]);
                    txtOInC.Text = Val.ToString(DRow["OPENINCNAME"]);

                    txtWInC.Tag = Val.ToString(DRow["WHITEINC_ID"]);
                    txtWInC.AccessibleDescription = Val.ToString(DRow["WHITEINCCODE"]);
                    txtWInC.Text = Val.ToString(DRow["WHITEINCNAME"]);

                    txtPav.Tag = Val.ToString(DRow["PAV_ID"]);
                    txtPav.AccessibleDescription = Val.ToString(DRow["PAVCODE"]);
                    txtPav.Text = Val.ToString(DRow["PAVNAME"]);

                    txtLuster.Tag = Val.ToString(DRow["LUSTER_ID"]);
                    txtLuster.AccessibleDescription = Val.ToString(DRow["LUSTERCODE"]);
                    txtLuster.Text = Val.ToString(DRow["LUSTERNAME"]);

                    txtEyeclean.Tag = Val.ToString(DRow["EYECLEAN_ID"]);
                    txtEyeclean.AccessibleDescription = Val.ToString(DRow["EYECLEANCODE"]);
                    txtEyeclean.Text = Val.ToString(DRow["EYECLEANNAME"]);

                    txtHA.Tag = Val.ToString(DRow["HA_ID"]);
                    txtHA.AccessibleDescription = Val.ToString(DRow["HACODE"]);
                    txtHA.Text = Val.ToString(DRow["HANAME"]);

                    Fetch_SetComboBox(CmbTension, DataTENSION, Val.ToInt(DRow["TENSION_ID"]));
                    Fetch_SetComboBox(CmbNatural, DataNATURAL, Val.ToInt(DRow["NATURAL_ID"]));
                    Fetch_SetComboBox(CmbGrain, DataGRAIN, Val.ToInt(DRow["GRAIN_ID"]));

                    txtMKAVDisc.Text = Val.ToString(DRow["MKAVDISCOUNT"]);
                    txtMKAVPricePerCarat.Text = Val.ToString(DRow["MKAVPRICEPERCARAT"]);
                    txtMKAVAmount.Text = Val.ToString(DRow["MKAVAMOUNT"]);

                    txtExpDiscount.Text = Val.ToString(DRow["EXPDISCOUNT"]);
                    txtExpPricePerCarat.Text = Val.ToString(DRow["EXPPRICEPERCARAT"]);
                    txtExpAmount.Text = Val.ToString(DRow["EXPAMOUNT"]);

                    txtRapnetDiscount.Text = Val.ToString(DRow["RAPNETDISCOUNT"]);
                    txtRapnetPricePerCarat.Text = Val.ToString(DRow["RAPNETPRICEPERCARAT"]);
                    txtRapnetAmount.Text = Val.ToString(DRow["RAPNETAMOUNT"]);
                    txtRapnetLink.Text = Val.ToString(DRow["RAPNETLINK"]);
                    //CmbCurrLabResultStatus.SelectedItem = Val.ToString(DRow["CURRENTLABRESULTSTATUS"]);
                    CmbCurrLabResultStatus.SelectedItem = Val.ToString(DRow["PKTLABRESULTSTATUS"]);
                    CmbLabResultStatus.SelectedItem = Val.ToString(DRow["LABRESULTSTATUS"]);

                    mISTFlag = Val.ToBoolean(DRow["TFLAG"]);

                    if (CmbLabResultStatus.Text == "NONE")
                        CmbLabResultStatus_SelectedIndexChanged(null, null);


                    if ((Val.ToInt32(CmbPrdType.Tag) == 15 || Val.ToInt32(CmbPrdType.Tag) == 16) && (Val.ToString(CmbCurrLabResultStatus.Text) == "RECHECKCANCEL" || Val.ToString(CmbCurrLabResultStatus.Text) == "REPAIRINGCANCEL"))
                    {
                        CmbLabResultStatus.SelectedItem = "CONFIRM";
                    }
                    //End : #P : 27-05-2020

                    CmbRapDate.SelectedItem = Val.ToString(DRow["RAPDATE"]);

                    txtCarat.Text = Val.ToString(DRow["CARAT"]);
                    txtDiscount.Text = Val.ToString(DRow["DISCOUNT"]);
                    txtRate.Text = Val.ToString(DRow["PRICEPERCARAT"]);
                    txtAmount.Text = Val.ToString(DRow["AMOUNT"]);

                    txtRapaport.Text = Val.ToString(DRow["RAPAPORT"]);

                    txtGiaNonGia.Text = Val.ToString(DRow["GIANONGIA"]);

                    ChkNOBGM.Checked = Val.ToBoolean(DRow["ISNOBGM"]);
                    ChkNOBlack.Checked = Val.ToBoolean(DRow["ISNOBLACK"]);

                    //Add : Pinali : 04-11-2019
                    ChkISPcnGrdByLabEntry.Checked = Val.ToBoolean(DRow["ISPCNGRDBYLABENTRY"]);
                    ChkISPcnGrdByLabEntry.Tag = Val.ToString(DRow["PCNGRDBYLAB_ID"]);
                    mStrReportNo = Val.ToString(DRow["REPORTNO"]);
                    txtRchkRepComment.Text = Val.ToString(DRow["RCHKREPCOMMENT"]);
                    //End : Pinali : 04-11-2019
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }
        public void Fetch_SetComboBox(AxonContLib.cComboBox Combo, IList<DataStructureLabPricing> pData, int Value)
        {
            foreach (DataStructureLabPricing data in pData)
            {
                if (Value == 0)
                {
                    Combo.SelectedItem = data;
                    break;
                }
                if (data.PARA_ID == Value)
                {
                    Combo.SelectedItem = data;
                    break;
                }
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

                if (Val.ToInt(CmbPrdType.Tag) == 14)
                {
                    MainGrid.Visible = false;
                    CmbLabResultStatus.Enabled = true;
                }
                else if (Val.ToInt(CmbPrdType.Tag) == 16)
                {
                    MainGrid.Visible = true;
                    CmbLabResultStatus.Enabled = false;
                }
                else
                {
                    MainGrid.Visible = false;
                    CmbLabResultStatus.Enabled = false;
                }

                //else
                //{
                //    lblLabProcess.Visible = false;
                //    CmbLabProcess.Visible = false;

                //    lblLabSelection.Visible = false;
                //    CmbLabSelection.Visible = false;

                //    ChkNOBGM.Checked = false;
                //    ChkNOBlack.Checked = false;
                //    ChkNOBGM.Visible = false;
                //    ChkNOBlack.Visible = false;
                //}

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
            FindRap_ForRChkRep();
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

        private void CmbBlackInc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AxonContLib.cComboBox combo = sender as AxonContLib.cComboBox;
                if (combo == null)
                {
                    return;
                }
                DataStructureLabPricing selectedDataStructure = combo.SelectedItem as DataStructureLabPricing;
                if (selectedDataStructure == null)
                {
                    Global.MessageError("You didn't select anything at the moment");
                }
                else
                {
                    if (combo.AccessibleDescription == "TENSION")
                    {
                        CmbTension.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbTension.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbTension.Text = Val.ToString(selectedDataStructure.PARANAME);
                        //GrdDet.SetFocusedRowCellValue("TENSION_ID", selectedDataStructure.PARA_ID);
                        //GrdDet.SetFocusedRowCellValue("TENSIONCODE", selectedDataStructure.PARACODE);
                        //GrdDet.SetFocusedRowCellValue("TENSIONNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "NATURAL")
                    {
                        CmbNatural.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbNatural.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbNatural.Text = Val.ToString(selectedDataStructure.PARANAME);
                        //GrdDet.SetFocusedRowCellValue("NATURAL_ID", selectedDataStructure.PARA_ID);
                        //GrdDet.SetFocusedRowCellValue("NATURALCODE", selectedDataStructure.PARACODE);
                        //GrdDet.SetFocusedRowCellValue("NATURALNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "GRAIN")
                    {
                        CmbGrain.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbGrain.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbGrain.Text = Val.ToString(selectedDataStructure.PARANAME);
                        //GrdDet.SetFocusedRowCellValue("GRAIN_ID", selectedDataStructure.PARA_ID);
                        //GrdDet.SetFocusedRowCellValue("GRAINCODE", selectedDataStructure.PARACODE);
                        //GrdDet.SetFocusedRowCellValue("GRAINNAME", selectedDataStructure.PARANAME);
                    }
                }

                FindRap();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }

        private void txtMKAVDisc_Validating(object sender, CancelEventArgs e)
        {
            Calculation();
        }

        public void Calculation()
        {
            try
            {
                double DouMainRapaport = 0;
                DouMainRapaport = Val.Val(txtRapaport.Text);

                double DouMKAVPricePerCarat = 0, DouMKAVAmount = 0;
                DouMKAVPricePerCarat = Val.Val(txtMKAVDisc.Text) == 0 ? 0 : Math.Round((DouMainRapaport - ((DouMainRapaport * Val.Val(txtMKAVDisc.Text)) / 100)), 2);
                DouMKAVAmount = Math.Round((DouMKAVPricePerCarat * Val.Val(txtCarat.Text)), 2);

                double DouEXPPricePerCarat = 0, DouEXPAmount = 0;
                DouEXPPricePerCarat = Val.Val(txtExpDiscount.Text) == 0 ? 0 : Math.Round((DouMainRapaport - ((DouMainRapaport * Val.Val(txtExpDiscount.Text)) / 100)), 2);
                DouEXPAmount = Math.Round((DouEXPPricePerCarat * Val.Val(txtCarat.Text)), 2);

                double DouRapnetPricePerCarat = 0, DouRapnetAmount = 0;
                DouRapnetPricePerCarat = Val.Val(txtRapnetDiscount.Text) == 0 ? 0 : Math.Round((DouMainRapaport - ((DouMainRapaport * Val.Val(txtRapnetDiscount.Text)) / 100)), 2);
                DouRapnetAmount = Math.Round((DouRapnetPricePerCarat * Val.Val(txtCarat.Text)), 2);

                txtMKAVPricePerCarat.Text = DouMKAVPricePerCarat.ToString();
                txtMKAVAmount.Text = Math.Round(DouMKAVAmount, 0).ToString();

                txtExpPricePerCarat.Text = DouEXPPricePerCarat.ToString();
                txtExpAmount.Text = Math.Round(DouEXPAmount, 0).ToString();

                txtRapnetPricePerCarat.Text = DouRapnetPricePerCarat.ToString();
                txtRapnetAmount.Text = Math.Round(DouRapnetAmount, 0).ToString();

            }
            catch (Exception ex)
            {
            }
        }

        private void CmbLabResultStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Val.ToString(CmbLabResultStatus.Text) == "RECHECK" || Val.ToString(CmbCurrLabResultStatus.Text) == "RECHECKCONFIRM")
            {
                if (lblMode.Text == "Add Mode" && Val.ToString(CmbCurrLabResultStatus.Text) != "RECHECKCONFIRM" && Val.ToString(CmbCurrLabResultStatus.Text) != "RECHECKCANCEL") //Coz Rchk Confrm chk na karaviye to second time labgrading aave tyare detail proper nthi aavti(as it is labGrd na data Rchk ma consider kari ley 6e).
                    CopyPasteRchkRepData();

                lblRecheckRepText.Text = "ReCheck Information";
                PnlRecheckRepairing.Visible = true;
            }
            //else if (Val.ToString(CmbLabResultStatus.Text) == "REPAIRING" && Val.ToString(CmbCurrLabResultStatus.Text) == "REPAIRINGCONFIRM" && Val.ToString(CmbCurrLabResultStatus.Text) == "REPAIRINGCANCEL")
            else if (Val.ToString(CmbLabResultStatus.Text) == "REPAIRING" || Val.ToString(CmbCurrLabResultStatus.Text) == "REPAIRINGCONFIRM")
            {
                if (lblMode.Text == "Add Mode" && Val.ToString(CmbCurrLabResultStatus.Text) != "REPAIRINGCONFIRM" && Val.ToString(CmbCurrLabResultStatus.Text) != "REPAIRINGCANCEL")
                    CopyPasteRchkRepData();

                lblRecheckRepText.Text = "Repairing Information";
                PnlRecheckRepairing.Visible = true;
            }
            else if ((Val.ToString(CmbCurrLabResultStatus.Text) == "NONE" || Val.ToString(CmbCurrLabResultStatus.Text) == "CONFIRM") && (Val.ToString(CmbCurrLabResultStatus.Text) != "RECHECKCANCEL" && Val.ToString(CmbCurrLabResultStatus.Text) != "REPAIRINGCANCEL"))
            {
                lblRecheckRepText.Text = "Recheck/Repairing Information";
                PnlRecheckRepairing.Visible = false;
                ClearReCheckRepControl();
            }
            //if (CmbCurrLabResultStatus.Text == "" || CmbCurrLabResultStatus.Text == "NONE")
            //{
            //    CmbCurrLabResultStatus.Text = CmbLabResultStatus.Text;
            //}
            EditableRChkRepControl();
        }


        private void txtRChkShape_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtRChkShape.AccessibleDescription = Val.ToString(FrmSearch.mDRow["SHAPECODE"]);
                        txtRChkShape.Text = Val.ToString(FrmSearch.mDRow["SHAPENAME"]);
                        txtRChkShape.Tag = Val.ToString(FrmSearch.mDRow["SHAPE_ID"]);

                        FindRap_ForRChkRep();
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

        private void txtRChkColor_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtRChkColor.AccessibleDescription = Val.ToString(FrmSearch.mDRow["COLORCODE"]);
                        txtRChkColor.Text = Val.ToString(FrmSearch.mDRow["COLORNAME"]);
                        txtRChkColor.Tag = Val.ToString(FrmSearch.mDRow["COLOR_ID"]);

                        FindRap_ForRChkRep();
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

        private void txtRChkClarity_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtRChkClarity.AccessibleDescription = Val.ToString(FrmSearch.mDRow["CLARITYCODE"]);
                        txtRChkClarity.Text = Val.ToString(FrmSearch.mDRow["CLARITYNAME"]);
                        txtRChkClarity.Tag = Val.ToString(FrmSearch.mDRow["CLARITY_ID"]);
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

        private void txtRChkCut_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtRChkCut.AccessibleDescription = Val.ToString(FrmSearch.mDRow["CUTCODE"]);
                        txtRChkCut.Text = Val.ToString(FrmSearch.mDRow["CUTCODE"]);
                        txtRChkCut.Tag = Val.ToString(FrmSearch.mDRow["CUT_ID"]);
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

        private void txtRChkPol_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtRChkPol.AccessibleDescription = Val.ToString(FrmSearch.mDRow["POLCODE"]);
                        txtRChkPol.Text = Val.ToString(FrmSearch.mDRow["POLCODE"]);
                        txtRChkPol.Tag = Val.ToString(FrmSearch.mDRow["POL_ID"]);
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

        private void txtRChkSym_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtRChkSym.AccessibleDescription = Val.ToString(FrmSearch.mDRow["SYMCODE"]);
                        txtRChkSym.Text = Val.ToString(FrmSearch.mDRow["SYMCODE"]);
                        txtRChkSym.Tag = Val.ToString(FrmSearch.mDRow["SYM_ID"]);
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

        private void txtRChkFL_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtRChkFL.AccessibleDescription = Val.ToString(FrmSearch.mDRow["FLCODE"]);
                        txtRChkFL.Text = Val.ToString(FrmSearch.mDRow["FLNAME"]);
                        txtRChkFL.Tag = Val.ToString(FrmSearch.mDRow["FL_ID"]);
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

        private void txtRChkAmount_Validated(object sender, EventArgs e)
        {
            try
            {
                if (Val.Val(txtRChkAmount.Text) != 0)
                {
                    txtRChkDiffAmount.Text = Val.ToString(Val.Val(txtRChkAmount.Text) - Val.Val(txtAmount.Text));
                    txtRChkDiffPer.Text = Math.Round(Val.Val(txtRChkDiffAmount.Text) / Val.Val(txtAmount.Text), 2).ToString();
                }
                else
                {
                    txtRChkDiffAmount.Text = string.Empty;
                    txtRChkDiffPer.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtBInC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,BLACKCODE,BLACKNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_BLACK);
                    FrmSearch.mColumnsToHide = "BLACK_ID,SEQUENCENO,BLACKNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtBInC.AccessibleDescription = Val.ToString(FrmSearch.mDRow["BLACKCODE"]);
                        txtBInC.Text = Val.ToString(FrmSearch.mDRow["BLACKCODE"]);
                        txtBInC.Tag = Val.ToString(FrmSearch.mDRow["BLACK_ID"]);
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

        public void FetchDataForRChkRep(DataTable DT)
        {
            if (DT.Rows.Count != 0)
            {

                DataRow DRow = DT.Rows[0];
                CmbRChkLabProcess.SelectedItem = Val.ToString(DRow["LABPROCESS"]);
                CmbRChkLabSelection.SelectedItem = Val.ToString(DRow["LABSELECTION"]);
                txtRChkDiaMin.Text = Val.ToString(DRow["DIAMIN"]);
                txtRChkDiaMax.Text = Val.ToString(DRow["DIAMAX"]);
                txtRChkHeight.Text = Val.ToString(DRow["HEIGHT"]);

                txtRChkShape.Tag = Val.ToString(DRow["SHAPE_ID"]);
                txtRChkShape.AccessibleDescription = Val.ToString(DRow["SHAPECODE"]);
                txtRChkShape.Text = Val.ToString(DRow["SHAPENAME"]);

                txtRChkColor.Tag = Val.ToString(DRow["COLOR_ID"]);
                txtRChkColor.AccessibleDescription = Val.ToString(DRow["COLORCODE"]);
                txtRChkColor.Text = Val.ToString(DRow["COLORNAME"]);

                txtRChkClarity.Tag = Val.ToString(DRow["CLARITY_ID"]);
                txtRChkClarity.AccessibleDescription = Val.ToString(DRow["CLARITYCODE"]);
                txtRChkClarity.Text = Val.ToString(DRow["CLARITYNAME"]);

                txtRChkCut.Tag = Val.ToString(DRow["CUT_ID"]);
                txtRChkCut.AccessibleDescription = Val.ToString(DRow["CUTCODE"]);
                txtRChkCut.Text = Val.ToString(DRow["CUTCODE"]);

                txtRChkPol.Tag = Val.ToString(DRow["POL_ID"]);
                txtRChkPol.AccessibleDescription = Val.ToString(DRow["POLCODE"]);
                txtRChkPol.Text = Val.ToString(DRow["POLCODE"]);

                txtRChkSym.Tag = Val.ToString(DRow["SYM_ID"]);
                txtRChkSym.AccessibleDescription = Val.ToString(DRow["SYMCODE"]);
                txtRChkSym.Text = Val.ToString(DRow["SYMCODE"]);

                txtRChkFL.Tag = Val.ToString(DRow["FL_ID"]);
                txtRChkFL.AccessibleDescription = Val.ToString(DRow["FLCODE"]);
                txtRChkFL.Text = Val.ToString(DRow["FLNAME"]);

                txtRChkMilky.Tag = Val.ToString(DRow["MILKY_ID"]);
                txtRChkMilky.AccessibleDescription = Val.ToString(DRow["MILKYCODE"]);
                txtRChkMilky.Text = Val.ToString(DRow["MILKYNAME"]);

                txtRChkColorShade.Tag = Val.ToString(DRow["COLORSHADE_ID"]);
                txtRChkColorShade.AccessibleDescription = Val.ToString(DRow["COLORSHADECODE"]);
                txtRChkColorShade.Text = Val.ToString(DRow["COLORSHADENAME"]);


                //#P : 27-05-2020
                txtRChkBInC.Tag = Val.ToString(DRow["BLACKINC_ID"]);
                txtRChkBInC.AccessibleDescription = Val.ToString(DRow["BLACKINCCODE"]);
                txtRChkBInC.Text = Val.ToString(DRow["BLACKINCNAME"]);

                txtRChkOInC.Tag = Val.ToString(DRow["OPENINC_ID"]);
                txtRChkOInC.AccessibleDescription = Val.ToString(DRow["OPENINCCODE"]);
                txtRChkOInC.Text = Val.ToString(DRow["OPENINCNAME"]);

                txtRChkWInC.Tag = Val.ToString(DRow["WHITEINC_ID"]);
                txtRChkWInC.AccessibleDescription = Val.ToString(DRow["WHITEINCCODE"]);
                txtRChkWInC.Text = Val.ToString(DRow["WHITEINCNAME"]);

                txtRChkPav.Tag = Val.ToString(DRow["PAV_ID"]);
                txtRChkPav.AccessibleDescription = Val.ToString(DRow["PAVCODE"]);
                txtRChkPav.Text = Val.ToString(DRow["PAVNAME"]);

                txtRChkLuster.Tag = Val.ToString(DRow["LUSTER_ID"]);
                txtRChkLuster.AccessibleDescription = Val.ToString(DRow["LUSTERCODE"]);
                txtRChkLuster.Text = Val.ToString(DRow["LUSTERNAME"]);

                txtRChkEyeclean.Tag = Val.ToString(DRow["EYECLEAN_ID"]);
                txtRChkEyeclean.AccessibleDescription = Val.ToString(DRow["EYECLEANCODE"]);
                txtRChkEyeclean.Text = Val.ToString(DRow["EYECLEANNAME"]);

                txtRChkHA.Tag = Val.ToString(DRow["HA_ID"]);
                txtRChkHA.AccessibleDescription = Val.ToString(DRow["HACODE"]);
                txtRChkHA.Text = Val.ToString(DRow["HANAME"]);

                txtRChkCarat.Text = Val.ToString(DRow["CARAT"]);
                txtRChkDiscount.Text = Val.ToString(DRow["DISCOUNT"]);
                txtRChkRate.Text = Val.ToString(DRow["PRICEPERCARAT"]);
                txtRChkAmount.Text = Val.ToString(DRow["AMOUNT"]);
                txtRChkRapaport.Text = Val.ToString(DRow["RAPAPORT"]);
                txtRChkGIANonGIA.Text = Val.ToString(DRow["GIANONGIA"]);

                txtRChkDiffAmount.Text = Val.ToString(DRow["RCHKREPDIFFAMOUNT"]);
                txtRChkDiffPer.Text = Val.ToString(DRow["RCHKREPDIFFPER"]);
                txtRchkRepComment.Text = Val.ToString(DRow["RCHKREPCOMMENT"]);
            }

        }

        private void CmbCurrLabResultStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Val.ToString(CmbCurrLabResultStatus.Text) == "RECHECK" || Val.ToString(CmbCurrLabResultStatus.Text) == "RECHECKCONFIRM")
            //{
            //    if (lblMode.Text == "Add Mode")
            //        CopyPasteRchkRepData();

            //    lblRecheckRepText.Text = "ReCheck Information";
            //    PnlRecheckRepairing.Visible = true;
            //}
            //else if (Val.ToString(CmbCurrLabResultStatus.Text) == "REPAIRING" || Val.ToString(CmbCurrLabResultStatus.Text) == "REPAIRINGCONFIRM")
            //{
            //    if (lblMode.Text == "Add Mode")
            //        CopyPasteRchkRepData();

            //    lblRecheckRepText.Text = "Repairing Information";
            //    PnlRecheckRepairing.Visible = true;
            //}
            //else
            //{
            //    lblRecheckRepText.Text = "Recheck/Repairing Information";
            //    PnlRecheckRepairing.Visible = false;
            //    ClearReCheckRepControl();
            //}
            //if (CmbCurrLabResultStatus.Text == "" || CmbCurrLabResultStatus.Text == "NONE")
            //{
            //    CmbCurrLabResultStatus.Text = CmbLabResultStatus.Text;
            //}
            //EditableRChkRepControl();
        }

        private void txtRChkColorShade_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtRChkColorShade.AccessibleDescription = Val.ToString(FrmSearch.mDRow["COLORSHADECODE"]);
                        txtRChkColorShade.Text = Val.ToString(FrmSearch.mDRow["COLORSHADENAME"]);
                        txtRChkColorShade.Tag = Val.ToString(FrmSearch.mDRow["COLORSHADE_ID"]);
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

        private void txtOInC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,OPENCODE,OPENNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_OPEN);
                    FrmSearch.mColumnsToHide = "OPEN_ID,OPENNAME,SEQUENCENO";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtOInC.AccessibleDescription = Val.ToString(FrmSearch.mDRow["OPENCODE"]);
                        txtOInC.Text = Val.ToString(FrmSearch.mDRow["OPENCODE"]);
                        txtOInC.Tag = Val.ToString(FrmSearch.mDRow["OPEN_ID"]);
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

        private void txtWInC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,WHITECODE,WHITENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_WHITE);
                    FrmSearch.mColumnsToHide = "WHITE_ID,SEQUENCENO,WHITENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtWInC.AccessibleDescription = Val.ToString(FrmSearch.mDRow["WHITECODE"]);
                        txtWInC.Text = Val.ToString(FrmSearch.mDRow["WHITECODE"]);
                        txtWInC.Tag = Val.ToString(FrmSearch.mDRow["WHITE_ID"]);
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

        private void txtPav_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,PAVCODE,PAVNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PAVALION);
                    FrmSearch.mColumnsToHide = "PAV_ID,SEQUENCENO,PAVNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPav.AccessibleDescription = Val.ToString(FrmSearch.mDRow["PAVCODE"]);
                        txtPav.Text = Val.ToString(FrmSearch.mDRow["PAVCODE"]);
                        txtPav.Tag = Val.ToString(FrmSearch.mDRow["PAV_ID"]);
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

        private void txtLuster_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,LUSTERCODE,LUSTERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LUSTER);
                    FrmSearch.mColumnsToHide = "LUSTER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtLuster.AccessibleDescription = Val.ToString(FrmSearch.mDRow["LUSTERCODE"]);
                        txtLuster.Text = Val.ToString(FrmSearch.mDRow["LUSTERNAME"]);
                        txtLuster.Tag = Val.ToString(FrmSearch.mDRow["LUSTER_ID"]);
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

        private void txtEyeclean_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,EYECLEANCODE,EYECLEANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EYECLEAN);
                    FrmSearch.mColumnsToHide = "EYECLEAN_ID,SEQUENCENO";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEyeclean.AccessibleDescription = Val.ToString(FrmSearch.mDRow["EYECLEANCODE"]);
                        txtEyeclean.Text = Val.ToString(FrmSearch.mDRow["EYECLEANCODE"]);
                        txtEyeclean.Tag = Val.ToString(FrmSearch.mDRow["EYECLEAN_ID"]);
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

        private void txtHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,HACODE,HANAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_HA);
                    FrmSearch.mColumnsToHide = "HA_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtHA.AccessibleDescription = Val.ToString(FrmSearch.mDRow["HACODE"]);
                        txtHA.Text = Val.ToString(FrmSearch.mDRow["HANAME"]);
                        txtHA.Tag = Val.ToString(FrmSearch.mDRow["HA_ID"]);
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

        private void txtRChkBInC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,BLACKCODE,BLACKNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_BLACK);
                    FrmSearch.mColumnsToHide = "BLACK_ID,SEQUENCENO,BLACKNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRChkBInC.AccessibleDescription = Val.ToString(FrmSearch.mDRow["BLACKCODE"]);
                        txtRChkBInC.Text = Val.ToString(FrmSearch.mDRow["BLACKCODE"]);
                        txtRChkBInC.Tag = Val.ToString(FrmSearch.mDRow["BLACK_ID"]);
                        FindRap_ForRChkRep();
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

        private void txtRChkOInC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,OPENCODE,OPENNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_OPEN);
                    FrmSearch.mColumnsToHide = "OPEN_ID,SEQUENCENO,OPENNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRChkOInC.AccessibleDescription = Val.ToString(FrmSearch.mDRow["OPENCODE"]);
                        txtRChkOInC.Text = Val.ToString(FrmSearch.mDRow["OPENCODE"]);
                        txtRChkOInC.Tag = Val.ToString(FrmSearch.mDRow["OPEN_ID"]);
                        FindRap_ForRChkRep();
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

        private void txtRChkWInC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,WHITECODE,WHITENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_WHITE);
                    FrmSearch.mColumnsToHide = "WHITE_ID,SEQUENCENO,WHITENAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRChkWInC.AccessibleDescription = Val.ToString(FrmSearch.mDRow["WHITECODE"]);
                        txtRChkWInC.Text = Val.ToString(FrmSearch.mDRow["WHITECODE"]);
                        txtRChkWInC.Tag = Val.ToString(FrmSearch.mDRow["WHITE_ID"]);
                        FindRap_ForRChkRep();
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

        private void txtRChkPav_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,PAVCODE,PAVNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PAVALION);
                    FrmSearch.mColumnsToHide = "PAV_ID,SEQUENCENO,PAVNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRChkPav.AccessibleDescription = Val.ToString(FrmSearch.mDRow["PAVCODE"]);
                        txtRChkPav.Text = Val.ToString(FrmSearch.mDRow["PAVCODE"]);
                        txtRChkPav.Tag = Val.ToString(FrmSearch.mDRow["PAV_ID"]);
                        FindRap_ForRChkRep();
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

        private void txtRChkMilky_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtRChkMilky.AccessibleDescription = Val.ToString(FrmSearch.mDRow["MILKYCODE"]);
                        txtRChkMilky.Text = Val.ToString(FrmSearch.mDRow["MILKYNAME"]);
                        txtRChkMilky.Tag = Val.ToString(FrmSearch.mDRow["MILKY_ID"]);
                        FindRap_ForRChkRep();
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

        private void txtRChkLuster_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,LUSTERCODE,LUSTERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LUSTER);
                    FrmSearch.mColumnsToHide = "LUSTER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRChkLuster.AccessibleDescription = Val.ToString(FrmSearch.mDRow["LUSTERCODE"]);
                        txtRChkLuster.Text = Val.ToString(FrmSearch.mDRow["LUSTERNAME"]);
                        txtRChkLuster.Tag = Val.ToString(FrmSearch.mDRow["LUSTER_ID"]);
                        FindRap_ForRChkRep();
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

        private void txtRChkEyeclean_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,EYECLEANCODE,EYECLEANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EYECLEAN);
                    FrmSearch.mColumnsToHide = "EYECLEAN_ID,SEQUENCENO";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRChkEyeclean.AccessibleDescription = Val.ToString(FrmSearch.mDRow["EYECLEANCODE"]);
                        txtRChkEyeclean.Text = Val.ToString(FrmSearch.mDRow["EYECLEANCODE"]);
                        txtRChkEyeclean.Tag = Val.ToString(FrmSearch.mDRow["EYECLEAN_ID"]);
                        FindRap_ForRChkRep();
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

        private void txtRChkHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,HACODE,HANAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_HA);
                    FrmSearch.mColumnsToHide = "HA_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRChkHA.AccessibleDescription = Val.ToString(FrmSearch.mDRow["HACODE"]);
                        txtRChkHA.Text = Val.ToString(FrmSearch.mDRow["HANAME"]);
                        txtRChkHA.Tag = Val.ToString(FrmSearch.mDRow["HA_ID"]);
                        FindRap_ForRChkRep();
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
        public void EditableRChkRepControl()
        {
            if (Val.ToString(CmbLabResultStatus.Text) == "RECHECK"
                || Val.ToString(CmbCurrLabResultStatus.Text) == "RECHECKCONFIRM"
                //|| Val.ToInt32(CmbPrdType.Tag) == 15 || Val.ToInt32(CmbPrdType.Tag) == 15
               )
            {
                txtRChkShape.Enabled = false;
                txtRChkCarat.Enabled = false;
                txtRChkDiaMin.Enabled = false;
                txtRChkDiaMax.Enabled = false;
                txtRChkHeight.Enabled = false;
                txtRChkColorShade.Enabled = false;
                txtRChkBInC.Enabled = false;
                txtRChkOInC.Enabled = false;
                txtRChkWInC.Enabled = false;
                txtRChkPav.Enabled = false;
                txtRChkMilky.Enabled = false;
                txtRChkLuster.Enabled = false;
                txtRChkEyeclean.Enabled = false;
                txtRChkHA.Enabled = false;
            }
            else
            {
                txtRChkShape.Enabled = true;
                txtRChkCarat.Enabled = true;
                txtRChkDiaMin.Enabled = true;
                txtRChkDiaMax.Enabled = true;
                txtRChkHeight.Enabled = true;
                txtRChkColorShade.Enabled = true;
                txtRChkBInC.Enabled = true;
                txtRChkOInC.Enabled = true;
                txtRChkWInC.Enabled = true;
                txtRChkPav.Enabled = true;
                txtRChkMilky.Enabled = true;
                txtRChkLuster.Enabled = true;
                txtRChkEyeclean.Enabled = true;
                txtRChkHA.Enabled = true;
            }

        }

        private void lblLatestLabGrdDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if(Val.ToString(txtTag.Tag).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Packe First..");
                    txtKapanName.Focus();
                    return;
                }


                DataTable DTabLabGrd = ObjRap.GetSinglePrdLabPricingData(11, Val.ToString(txtTag.Tag), 0, 0);

                if (DTabLabGrd.Rows.Count <= 0)
                {
                    Global.Message("Lab Grading Data Not Found..");
                    return;
                }

                DataRow DRow = DTabLabGrd.Rows[0];
                //txtRemark.Text = Val.ToString(DRow["REMARK"]);
                CmbLabProcess.SelectedItem = Val.ToString(DRow["LABPROCESS"]);
                CmbLabSelection.SelectedItem = Val.ToString(DRow["LABSELECTION"]);

                txtCarat.Text = Val.ToString(DRow["CARAT"]);

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

                if (CmbLabResultStatus.Text == "REPAIRING" || CmbLabResultStatus.Text == "RECHECK")
                {
                    txtRChkCarat.Text = Val.ToString(DRow["CARAT"]);

                    CmbRChkLabProcess.SelectedItem = Val.ToString(DRow["LABPROCESS"]);
                    CmbRChkLabSelection.SelectedItem = Val.ToString(DRow["LABSELECTION"]);

                    txtRChkShape.Tag = Val.ToString(DRow["SHAPE_ID"]);
                    txtRChkShape.AccessibleDescription = Val.ToString(DRow["SHAPECODE"]);
                    txtRChkShape.Text = Val.ToString(DRow["SHAPENAME"]);

                    txtRChkColor.Tag = Val.ToString(DRow["COLOR_ID"]);
                    txtRChkColor.AccessibleDescription = Val.ToString(DRow["COLORCODE"]);
                    txtRChkColor.Text = Val.ToString(DRow["COLORNAME"]);

                    txtRChkClarity.Tag = Val.ToString(DRow["CLARITY_ID"]);
                    txtRChkClarity.AccessibleDescription = Val.ToString(DRow["CLARITYCODE"]);
                    txtRChkClarity.Text = Val.ToString(DRow["CLARITYNAME"]);

                    txtRChkCut.Tag = Val.ToString(DRow["CUT_ID"]);
                    txtRChkCut.AccessibleDescription = Val.ToString(DRow["CUTCODE"]);
                    txtRChkCut.Text = Val.ToString(DRow["CUTCODE"]);

                    txtRChkPol.Tag = Val.ToString(DRow["POL_ID"]);
                    txtRChkPol.AccessibleDescription = Val.ToString(DRow["POLCODE"]);
                    txtRChkPol.Text = Val.ToString(DRow["POLCODE"]);

                    txtRChkSym.Tag = Val.ToString(DRow["SYM_ID"]);
                    txtRChkSym.AccessibleDescription = Val.ToString(DRow["SYMCODE"]);
                    txtRChkSym.Text = Val.ToString(DRow["SYMCODE"]);

                    txtRChkFL.Tag = Val.ToString(DRow["FL_ID"]);
                    txtRChkFL.AccessibleDescription = Val.ToString(DRow["FLCODE"]);
                    txtRChkFL.Text = Val.ToString(DRow["FLNAME"]);
                }
                FindRap();
                FindRap_ForRChkRep();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }

        private void BtnUpDown_Click(object sender, EventArgs e)
        {
            if (IsDownImage == true)
            {
                BtnUpDown.Image = AxoneMFGRJ.Properties.Resources.A3;
                pnlParameter.Visible = false;
                panel6.Visible = false;
                MainGrid.Visible = false;
                PnlBtn.Visible = false;
                panel5.Visible = false;
                PnlBtn.Visible = false;
                MainGrdExcel.Visible = true;
                panel8.Visible = true;
                MainGrid.Dock = DockStyle.Fill;
                PnlUpdown.Dock = DockStyle.Top;
                IsDownImage = false;
            }
            else
            {
                BtnUpDown.Image = AxoneMFGRJ.Properties.Resources.A4;
                IsDownImage = true;
                pnlParameter.Visible = true;
                panel6.Visible = true;
                panel6.Dock = DockStyle.Fill;
                MainGrid.Visible = false;
                //  MainGrid.Dock = DockStyle.Fill;
                panel5.Visible = true;
                PnlBtn.Visible = true;
                MainGrdExcel.Visible = false;
                panel8.Visible = false;
                PnlUpdown.Dock = DockStyle.Bottom;

            }
        }

        public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    if (Convert.ToString(firstRowCell.Text).Equals(string.Empty))
                        continue;

                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        if (Convert.ToString(cell.Text).Equals(string.Empty))
                            continue;

                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DTabExcelData.Rows.Clear();
                ObjGridSelection.ClearSelection();
                if (Path.GetExtension(txtFileName.Text.ToString()).ToUpper().Contains("XLSX") || Path.GetExtension(txtFileName.Text.ToString()).ToUpper().Contains("XLS"))
                {
                    string extension = Path.GetExtension(txtFileName.Text.ToString());
                    string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(txtFileName.Text);
                    destinationPath = destinationPath.Replace(extension, ".xlsx");
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    File.Copy(txtFileName.Text, destinationPath);

                    DTabExcelData = GetDataTableFromExcel(destinationPath, true);

                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }

                    DTabExcelData.TableName = "Table";

                    string StrXMLValues = string.Empty;
                    using (StringWriter sw = new StringWriter())
                    {
                        DTabExcelData.WriteXml(sw);
                        StrXMLValues = sw.ToString();
                    }

                    //StrXMLValues = Regex.Replace(StrXMLValues,
                    //    @"<RAPDATE>(?<year>\d{4})-(?<month>\d{2})-(?<date>\d{2}).*?</RAPDATE>",
                    //    @"<RAPDATE>${month}/${date}/${year}</RAPDATE>",
                    //    RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                    DataTable DTab = ObjRap.GetdataForLabInclusion(StrXMLValues);

                    if (DTab.Rows[0]["RETURNMESSAGETYPE"].ToString() == "FAIL")
                    {
                        string StrReturnMas = DTab.Rows[0]["RETURNMESSAGEDESC"].ToString();
                        Global.Message(StrReturnMas);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        MainGrdExcel.DataSource = DTab;
                        GrdFileUpload.RefreshData();
                        this.Cursor = Cursors.Default;
                    }

                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return;
                }


                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(Ex.Message);
            }
        }

        private void lblDownload_Click(object sender, EventArgs e)
        {
            string StrFilePathDestination = "";

            StrFilePathDestination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\LabInclusionFileUpload" + DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM") + DateTime.Now.Day.ToString() + ".xlsx";
            if (File.Exists(StrFilePathDestination))
            {
                File.Delete(StrFilePathDestination);
            }
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Format\\LabInclusionFileUpload.xlsx", StrFilePathDestination);
            System.Diagnostics.Process.Start(StrFilePathDestination, "CMD");
        }

        private void GetExcelSheetNames(string excelFile)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(excelFile))
                {
                    pck.Load(stream);
                }

                CmbSheetName.Items.Clear(); //ADD:KULDEEP[24/05/18]
                foreach (ExcelWorksheet row in pck.Workbook.Worksheets)
                {
                    CmbSheetName.Items.Add(row.Name);
                }
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenFileDialog = new OpenFileDialog();
                OpenFileDialog.Filter = "Excel Files (*.xls,*.xlsx)|*.xls;*.xlsx;";
                //OpenFileDialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtFileName.Text = OpenFileDialog.FileName;

                    string extension = Path.GetExtension(txtFileName.Text.ToString());
                    string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(txtFileName.Text);
                    destinationPath = destinationPath.Replace(extension, ".xlsx");
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    File.Copy(txtFileName.Text, destinationPath);


                    GetExcelSheetNames(destinationPath);
                    CmbSheetName.SelectedIndex = 0;

                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }


                }
                OpenFileDialog.Dispose();
                OpenFileDialog = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString() + "InValid File Name");
            }
        }

        private void BtnClearExcel_Click(object sender, EventArgs e)
        {
            txtFileName.Text = string.Empty;
            DTabExcelData.Rows.Clear();
            MainGrdExcel.DataSource = null;
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

        private void BtnVerified_Click(object sender, EventArgs e)
        {
            try
            {
                DTabUploadData = GetTableOfSelectedRows(GrdFileUpload, true);

                if (DTabUploadData.Rows.Count == 0)
                {
                    Global.Message("Please Select at lease One Row For Update");
                    return;
                }

                if (Global.Confirm("Are you Sure You Want Update Selected Packets?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                DTabUploadData.AcceptChanges();

                DTabUploadData.TableName = "Table";


                string StrXMLValues = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabUploadData.WriteXml(sw);
                    StrXMLValues = sw.ToString();
                }

                Trn_RapSaveProperty Property = new Trn_RapSaveProperty();

                Property.PRDTYPE_ID = Val.ToInt32(CmbPrdType.Tag);
                Property.PRDTYPE = Val.ToString(CmbPrdType.Text);

                string UploadDetail = StrXMLValues;

                UploadDetail = Regex.Replace(UploadDetail,
                    @"<RAPDATE>(?<year>\d{4})-(?<month>\d{2})-(?<date>\d{2}).*?</RAPDATE>",
                    @"<RAPDATE>${month}/${date}/${year}</RAPDATE>",
                    RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                Property = ObjRap.UpdateByLabIncclusionExpDetailWithExcel(Property, UploadDetail);

                string ReturnMessageDesc = Property.ReturnMessageDesc;
                string ReturnMessageType = Property.ReturnMessageType;


                if (ReturnMessageType == "SUCCESS")
                {
                    Global.Message(ReturnMessageDesc);
                    Clear();
                }
                else
                {
                    Global.Message(ReturnMessageDesc);
                    return;
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
                return;
            }
        }
    }
    public class DataStructureLabPricing
    {
        public int PARA_ID { get; set; }
        public string PARACODE { get; set; }
        public string PARANAME { get; set; }
    }
}