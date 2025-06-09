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
using BusLib.ReportGrid;
using BusLib.Transaction;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using DevExpress.XtraPrinting;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmFindRap : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOFormPer ObjPer = new BOFormPer();

        IList<DataStructure> DataMILKY = new BindingList<DataStructure>();
        IList<DataStructure> DataLBLC = new BindingList<DataStructure>();
        IList<DataStructure> DataNATTS = new BindingList<DataStructure>();
        IList<DataStructure> DataBLACK = new BindingList<DataStructure>();
        IList<DataStructure> DataOPEN = new BindingList<DataStructure>();
        IList<DataStructure> DataWHITE = new BindingList<DataStructure>();
        IList<DataStructure> DataHEARTANDARROW = new BindingList<DataStructure>();
        IList<DataStructure> DataPAVALION = new BindingList<DataStructure>();
        IList<DataStructure> DataTENSION = new BindingList<DataStructure>();

        IList<DataStructure> DataSAKHAT = new BindingList<DataStructure>();

        IList<DataStructure> DataCOLORSHADE = new BindingList<DataStructure>();
        IList<DataStructure> DataLUSTER = new BindingList<DataStructure>();
        IList<DataStructure> DataEYECLEAN = new BindingList<DataStructure>();
        IList<DataStructure> DataNATURAL = new BindingList<DataStructure>();
        IList<DataStructure> DataGRAIN = new BindingList<DataStructure>();
        IList<DataStructure> DataLAB = new BindingList<DataStructure>();
        IList<DataStructure> DataTABLEOPEN = new BindingList<DataStructure>();
        IList<DataStructure> DataCROWNOPEN = new BindingList<DataStructure>();
        IList<DataStructure> DataPAVILLIONOPEN = new BindingList<DataStructure>();

        BOFindRap ObjRap = new BOFindRap();

        DataTable DTabParameter = new DataTable();
        DataTable DTabPrdType = new DataTable();
        DataTable DTabPrediction = new DataTable();
        DataTable DTabRapDate = new DataTable();


        DataTable DTabMakLog = new DataTable();

        Color mSelectedColor = Color.FromArgb(192, 0, 0);
        Color mDeSelectColor = Color.Black;
        Color mSelectedBackColor = Color.FromArgb(255, 224, 192);
        Color mDSelectedBackColor = Color.WhiteSmoke;

        DataTable DTabFileTransferType = new DataTable();

        string mStrFileTransferUsername = "";
        string mStrFileTransferPassword = "";

        string pStrPassword = "";


        DataTable DtabFileSharingCredential = new DataTable();
        string mStrSharingFilePath = "";
        string mStrSharingFileUserName = "";
        string mStrSharingFilePassword = "";

        bool ISClickOnShowButton = false;

        string StrPath = "";

        EmployeeActionRightsProperty EmployeeRightsProperty = new EmployeeActionRightsProperty();

        int mIntOldISFinalFlagPlanNo;//#P : 11-10-2019
        Int32 IntOldTFlagPlanNo = 0; //#P : 06-02-2020

        string mStrEntryDate = ""; //#P : 12-02-2020
        string mStrPktFL_ID = ""; //#P : 21-02-2020

        string mStrType = "NOSHOWCLICK";
        string mStrKapanName = "";
        string TAGSER = "", mstrBarcode = "";
        int PKTSRNOSER = 0;
        string StrBPrintType = "";
        string StrBatchFileName = "";
        string StrBarcodeTxtFileName = "";

        int ISCopyRoughMkblPlanIntoFinalTFlagPrd = 0;

        #region Constructor

        public FrmFindRap()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            pStrPassword = ObjPer.PASSWORD;

            this.Show();
            BtnImport.Enabled = false;

            SetControl();

            BtnClear_Click(null, null);

            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);
            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDet.RestoreLayoutFromStream(stream);
            }

            mStrFileTransferUsername = new BOMST_FormPermission().GetFileTransferUsername();
            mStrFileTransferPassword = new BOMST_FormPermission().GetFileTransferPassword();


            EmployeeRightsProperty = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
            txtPassForDisplayBack.Tag = Val.ToString(EmployeeRightsProperty.RAPPASSFORDISPDISC);
            txtEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;
            BtnEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;
            ChkAllowForUpdate.Visible = EmployeeRightsProperty.RAPUPDATEPREDICTION;

            txtPassForDisplayBack_TextChanged(null, null);

            DTabPrdType = ObjRap.GetPreditctionType(EmployeeRightsProperty.PRDTYPE_ID);
            CmbPrdType.Items.Clear();
            foreach (DataRow DRow in DTabPrdType.Rows)
            {
                if (Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9 || Val.ToInt(DRow["PRDTYPE_ID"]) == 11
                    || Val.ToInt(DRow["PRDTYPE_ID"]) == 14 || Val.ToInt(DRow["PRDTYPE_ID"]) == 15 || Val.ToInt(DRow["PRDTYPE_ID"]) == 16
                    || Val.ToInt(DRow["PRDTYPE_ID"]) == 17 || Val.ToInt(DRow["PRDTYPE_ID"]) == 18)
                    continue;

                CmbPrdType.Items.Add(Val.ToString(DRow["PRDTYPENAME"]));
            }
            CmbPrdType.SelectedIndex = 0;

            DTabFileTransferType = new BOComboFill().FillCmb(BOComboFill.TABLE.FILE_TRANSFER_TYPE);

            DtabFileSharingCredential = new BOMST_FormPermission().GetRapCalcSharingFileCredential();
            foreach (DataRow DRow in DtabFileSharingCredential.Rows)
            {
                if (DRow["SETTINGKEY"].ToString() == "RAPCALCFILESHARINGPATH")
                {
                    mStrSharingFilePath = DRow["SETTINGVALUE"].ToString();
                }
                if (DRow["SETTINGKEY"].ToString() == "RAPCALCFILESHARINGUSERNAME")
                {
                    mStrSharingFileUserName = DRow["SETTINGVALUE"].ToString();
                }
                if (DRow["SETTINGKEY"].ToString() == "RAPCALCFILESHARINGPASSWORD")
                {
                    mStrSharingFilePassword = DRow["SETTINGVALUE"].ToString();
                }
            }
            RbtBarcode_CheckedChanged(null, null);
        }
        public void ShowFormWithExistData(int IntPrdType_ID, string StrPrdType, string StrKapanName, int IntPacketNo, string StrTag, Int64 IntPacket_ID, Int64 IntEmployee_ID, string StrEmpCode)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
            BtnImport.Enabled = false;

            SetControl();

            BtnClear_Click(null, null);

            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);
            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDet.RestoreLayoutFromStream(stream);
            }

            mStrFileTransferUsername = new BOMST_FormPermission().GetFileTransferUsername();
            mStrFileTransferPassword = new BOMST_FormPermission().GetFileTransferPassword();


            EmployeeRightsProperty = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
            txtPassForDisplayBack.Tag = Val.ToString(EmployeeRightsProperty.RAPPASSFORDISPDISC);
            txtEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;
            BtnEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;
            ChkAllowForUpdate.Visible = EmployeeRightsProperty.RAPUPDATEPREDICTION;

            txtPassForDisplayBack_TextChanged(null, null);

            DTabPrdType = ObjRap.GetPreditctionType(EmployeeRightsProperty.PRDTYPE_ID);
            CmbPrdType.Items.Clear();
            foreach (DataRow DRow in DTabPrdType.Rows)
            {
                if (Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9 || Val.ToInt(DRow["PRDTYPE_ID"]) == 11
                    || Val.ToInt(DRow["PRDTYPE_ID"]) == 14 || Val.ToInt(DRow["PRDTYPE_ID"]) == 15 || Val.ToInt(DRow["PRDTYPE_ID"]) == 16
                    || Val.ToInt(DRow["PRDTYPE_ID"]) == 17 || Val.ToInt(DRow["PRDTYPE_ID"]) == 18)
                    continue;

                CmbPrdType.Items.Add(Val.ToString(DRow["PRDTYPENAME"]));
            }
            CmbPrdType.SelectedIndex = 0;

            DTabFileTransferType = new BOComboFill().FillCmb(BOComboFill.TABLE.FILE_TRANSFER_TYPE);

            DtabFileSharingCredential = new BOMST_FormPermission().GetRapCalcSharingFileCredential();
            foreach (DataRow DRow in DtabFileSharingCredential.Rows)
            {
                if (DRow["SETTINGKEY"].ToString() == "RAPCALCFILESHARINGPATH")
                {
                    mStrSharingFilePath = DRow["SETTINGVALUE"].ToString();
                }
                if (DRow["SETTINGKEY"].ToString() == "RAPCALCFILESHARINGUSERNAME")
                {
                    mStrSharingFileUserName = DRow["SETTINGVALUE"].ToString();
                }
                if (DRow["SETTINGKEY"].ToString() == "RAPCALCFILESHARINGPASSWORD")
                {
                    mStrSharingFilePassword = DRow["SETTINGVALUE"].ToString();
                }
            }
            RbtBarcode_CheckedChanged(null, null);


            CmbPrdType.Tag = IntPrdType_ID;
            CmbPrdType.Text = StrPrdType;

            RbtPacketNo.Checked = true;
            txtKapanName.Text = StrKapanName;
            txtPacketNo.Text = IntPacketNo.ToString();
            txtPacketNo.Tag = IntPacket_ID.ToString();
            txtTag.Text = StrTag;
            txtEmployee.Text = StrEmpCode;
            txtEmployee.Tag = IntEmployee_ID.ToString();
            BtnContinue_Click(null, null);

        }

        public void AttachFormDefaultEvent()
        {
            //ObjPer.GetPermission(this);
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);

            ObjFormEvent.ObjToDisposeList.Add(ObjRap);
            ObjFormEvent.ObjToDisposeList.Add(DTabParameter);

            ObjFormEvent.ObjToDisposeList.Add(DTabPrdType);
            ObjFormEvent.ObjToDisposeList.Add(DTabPrediction);
            ObjFormEvent.ObjToDisposeList.Add(DTabRapDate);

            ObjFormEvent.ObjToDisposeList.Add(DataMILKY);
            ObjFormEvent.ObjToDisposeList.Add(DataLBLC);
            ObjFormEvent.ObjToDisposeList.Add(DataNATTS);
            ObjFormEvent.ObjToDisposeList.Add(DataBLACK);
            ObjFormEvent.ObjToDisposeList.Add(DataOPEN);
            ObjFormEvent.ObjToDisposeList.Add(DataWHITE);
            ObjFormEvent.ObjToDisposeList.Add(DataHEARTANDARROW);
            ObjFormEvent.ObjToDisposeList.Add(DataPAVALION);
            ObjFormEvent.ObjToDisposeList.Add(DataTENSION);
            ObjFormEvent.ObjToDisposeList.Add(DataSAKHAT);
            ObjFormEvent.ObjToDisposeList.Add(DataCOLORSHADE);
            ObjFormEvent.ObjToDisposeList.Add(DataLUSTER);
            ObjFormEvent.ObjToDisposeList.Add(DataEYECLEAN);
            ObjFormEvent.ObjToDisposeList.Add(DataNATURAL);
            ObjFormEvent.ObjToDisposeList.Add(DataGRAIN);
            ObjFormEvent.ObjToDisposeList.Add(DataLAB);

        }

        private void cButton_Click(object sender, EventArgs e)
        {
            try
            {

                AxonContLib.cButton btn = (AxonContLib.cButton)sender;
                btn.ForeColor = mSelectedColor;
                btn.BackColor = mSelectedBackColor;
                btn.AccessibleName = "true";


                if (MainGrid.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                AxonContLib.cButton rd = (AxonContLib.cButton)sender;
                if (rd.ToolTips == "SHAPE")
                {
                    PanelShape.Controls.OfType<AxonContLib.cButton>().Where(i => Val.ToInt(i.Tag) != Val.ToInt(btn.Tag)).ToList().ForEach(a =>
                    {
                        a.AccessibleName = "false";
                        a.ForeColor = mDeSelectColor;
                        a.BackColor = mDSelectedBackColor;
                    });

                    if (btn.Text == "R")
                    {
                        PanelCut.Controls.OfType<AxonContLib.cButton>().ToList().ForEach(a =>
                        {
                            a.AccessibleName = "false";
                            a.ForeColor = mDeSelectColor;
                            a.BackColor = mDSelectedBackColor;
                        });
                        PanelPol.Controls.OfType<AxonContLib.cButton>().ToList().ForEach(a =>
                        {
                            a.AccessibleName = "false";
                            a.ForeColor = mDeSelectColor;
                            a.BackColor = mDSelectedBackColor;
                        });
                        PanelSym.Controls.OfType<AxonContLib.cButton>().ToList().ForEach(a =>
                        {
                            a.AccessibleName = "false";
                            a.ForeColor = mDeSelectColor;
                            a.BackColor = mDSelectedBackColor;
                        });

                        AxonContLib.cButton rbCut = PanelCut.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                        rbCut.AccessibleName = "true";
                        rbCut.ForeColor = mSelectedColor;
                        rbCut.BackColor = mSelectedBackColor;

                        AxonContLib.cButton rbPol = PanelPol.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                        rbPol.AccessibleName = "true";
                        rbPol.ForeColor = mSelectedColor;
                        rbPol.BackColor = mSelectedBackColor;

                        AxonContLib.cButton rbSym = PanelSym.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                        rbSym.AccessibleName = "true";
                        rbSym.ForeColor = mSelectedColor;
                        rbSym.BackColor = mSelectedBackColor;
                    }
                    else //If Fancy Shape then Select Vg-Vg-Vg in CPS AS per Discuss with client : 17-06-2022
                    {
                        Fetch_SetRadioButton(PanelCut, Val.ToInt(110), true); // 373 = VG
                        Fetch_SetRadioButton(PanelPol, Val.ToInt(346), true); // 373 = VG
                        Fetch_SetRadioButton(PanelSym, Val.ToInt(373), true); // 373 = VG
                    }
                }
                else if (rd.ToolTips == "COLOR")
                {
                    PanelColor.Controls.OfType<AxonContLib.cButton>().Where(i => Val.ToInt(i.Tag) != Val.ToInt(btn.Tag)).ToList().ForEach(a =>
                    {
                        a.AccessibleName = "false";
                        a.ForeColor = mDeSelectColor;
                        a.BackColor = mDSelectedBackColor;
                    });
                }
                else if (rd.ToolTips == "CLARITY")
                {
                    PanelClarity.Controls.OfType<AxonContLib.cButton>().Where(i => Val.ToInt(i.Tag) != Val.ToInt(btn.Tag)).ToList().ForEach(a =>
                    {
                        a.AccessibleName = "false";
                        a.ForeColor = mDeSelectColor;
                        a.BackColor = mDSelectedBackColor;
                    });
                }

                else if (rd.ToolTips == "CUT")
                {
                    PanelCut.Controls.OfType<AxonContLib.cButton>().Where(i => Val.ToInt(i.Tag) != Val.ToInt(btn.Tag)).ToList().ForEach(a =>
                    {
                        a.AccessibleName = "false";
                        a.ForeColor = mDeSelectColor;
                        a.BackColor = mDSelectedBackColor;
                    });


                    ////#P : 11-02-2020 : (1) When Select VG1,VG2,GD1 as Cut then Auto Select VG as Symmetry. (2) When Select G2,FG1 As Cut Then Auto Select G as Symmetry
                    //if (btn.Text == "VG1" || btn.Text == "VG2" || btn.Text == "GD1")  //(1)
                    //    Fetch_SetRadioButton(PanelSym, Val.ToInt(373), true); // 373 = VG
                    //else if (btn.Text == "GD2" || btn.Text == "FG1") //(2)
                    //    Fetch_SetRadioButton(PanelSym, Val.ToInt(374), true); // 374 = VG
                    //else
                    //{
                    //    AxonContLib.cButton rbSym = PanelSym.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                    //    Fetch_SetRadioButton(PanelSym, Val.ToInt(rbSym.Tag), true); // Default
                    //}
                    ////End : #P : 11-02-2020

                }
                else if (rd.ToolTips == "POL")
                {
                    PanelPol.Controls.OfType<AxonContLib.cButton>().Where(i => Val.ToInt(i.Tag) != Val.ToInt(btn.Tag)).ToList().ForEach(a =>
                    {
                        a.AccessibleName = "false";
                        a.ForeColor = mDeSelectColor;
                        a.BackColor = mDSelectedBackColor;
                    });
                }
                else if (rd.ToolTips == "SYM")
                {
                    PanelSym.Controls.OfType<AxonContLib.cButton>().Where(i => Val.ToInt(i.Tag) != Val.ToInt(btn.Tag)).ToList().ForEach(a =>
                    {
                        a.AccessibleName = "false";
                        a.ForeColor = mDeSelectColor;
                        a.BackColor = mDSelectedBackColor;
                    });
                }
                else if (rd.ToolTips == "FL")
                {
                    PanelFL.Controls.OfType<AxonContLib.cButton>().Where(i => Val.ToInt(i.Tag) != Val.ToInt(btn.Tag)).ToList().ForEach(a =>
                    {
                        a.AccessibleName = "false";
                        a.ForeColor = mDeSelectColor;
                        a.BackColor = mDSelectedBackColor;
                    });
                }
                FindRap();
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.WaitCursor;
                Global.MessageError(EX.Message);
                return;
            }

        }

        public void DesignSystemButtion(Panel PNL, string pStrParaType, string pStrDisplayText, string toolTips, int pIntHeight, int pIntWidth)
        {
            DataRow[] UDRow = DTabParameter.Select("ParaType = '" + pStrParaType + "'");

            if (UDRow.Length == 0)
            {
                return;
            }

            DataTable DTab = UDRow.CopyToDataTable();
            DTab.DefaultView.Sort = "SequenceNo";
            DTab = DTab.DefaultView.ToTable();

            PNL.Controls.Clear();

            int IntI = 0;
            foreach (DataRow DRow in DTab.Rows)
            {
                AxonContLib.cButton ValueList = new AxonContLib.cButton();
                ValueList.Text = DRow[pStrDisplayText].ToString();
                ValueList.FlatStyle = FlatStyle.Flat;
                ValueList.Width = pIntWidth;
                ValueList.Height = pIntHeight;
                ValueList.Tag = DRow["PARA_ID"].ToString();
                ValueList.AccessibleDescription = Val.ToString(DRow["PARACODE"]);
                ValueList.ToolTips = toolTips;
                ValueList.AutoSize = true;
                ValueList.Click += new EventHandler(cButton_Click);
                ValueList.Cursor = Cursors.Hand;
                ValueList.Font = new Font("Tahoma", 9, FontStyle.Regular);
                if (pStrParaType == "SHAPE")
                {
                    if (File.Exists(Application.StartupPath + "//Image//" + ValueList.Text + ".png"))
                    {
                        ValueList.Image = Image.FromFile(Application.StartupPath + "//Image//" + ValueList.Text + ".png");
                        ValueList.TextImageRelation = TextImageRelation.ImageAboveText;
                    }
                    else if (File.Exists(Application.StartupPath + "//Image//OTH.png"))
                    {
                        ValueList.Image = Image.FromFile(Application.StartupPath + "//Image//OTH.png");
                        ValueList.TextImageRelation = TextImageRelation.ImageAboveText;
                    }
                }

                if (IntI == 0)
                {
                    ValueList.AccessibleName = "true";
                    ValueList.ForeColor = mSelectedColor;
                    ValueList.BackColor = mSelectedBackColor;
                }
                else
                {
                    ValueList.AccessibleName = "false";
                    ValueList.ForeColor = mDeSelectColor;
                    ValueList.BackColor = mDSelectedBackColor;
                }

                PNL.Controls.Add(ValueList);

                IntI++;
            }
        }

        public void DesignComboBox(string pStrParaType, AxonContLib.cComboBox ComboBox, string pStrDisplayText)
        {
            try
            {
                DataRow[] UDRow = DTabParameter.Select("ParaType = '" + pStrParaType + "'");

                if (UDRow.Length == 0)
                {
                    return;
                }

                DataTable DTab = UDRow.CopyToDataTable();
                DTab.DefaultView.Sort = "SequenceNo";
                DTab = DTab.DefaultView.ToTable();

                if (pStrParaType == "MILKY")
                {
                    //DataMILKY.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataMILKY.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbMilky.AccessibleDescription = pStrParaType;
                    CmbMilky.DataSource = DataMILKY;
                    CmbMilky.DisplayMember = pStrDisplayText;
                    CmbMilky.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "LBLC")
                {
                    //DataLBLC.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataLBLC.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbLBLC.AccessibleDescription = pStrParaType;
                    CmbLBLC.DataSource = DataLBLC;
                    CmbLBLC.DisplayMember = pStrDisplayText;
                    CmbLBLC.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "NATTS")
                {
                    //DataNATTS.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataNATTS.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbNatts.AccessibleDescription = pStrParaType;
                    CmbNatts.DataSource = DataNATTS;
                    CmbNatts.DisplayMember = pStrDisplayText;
                    CmbNatts.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "BLACK")
                {
                    //DataBLACK.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataBLACK.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbBlackInc.AccessibleDescription = pStrParaType;
                    CmbBlackInc.DataSource = DataBLACK;
                    CmbBlackInc.DisplayMember = pStrDisplayText;
                    CmbBlackInc.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "OPEN")
                {
                    //DataOPEN.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataOPEN.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbOpenInc.AccessibleDescription = pStrParaType;
                    CmbOpenInc.DataSource = DataOPEN;
                    CmbOpenInc.DisplayMember = pStrDisplayText;
                    CmbOpenInc.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "WHITE")
                {
                    //DataWHITE.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataWHITE.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbWhiteInc.AccessibleDescription = pStrParaType;
                    CmbWhiteInc.DataSource = DataWHITE;
                    CmbWhiteInc.DisplayMember = pStrDisplayText;
                    CmbWhiteInc.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "HEARTANDARROW")
                {
                    //DataHEARTANDARROW.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataHEARTANDARROW.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbHA.AccessibleDescription = pStrParaType;
                    CmbHA.DataSource = DataHEARTANDARROW;
                    CmbHA.DisplayMember = pStrDisplayText;
                    CmbHA.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "PAVALION")
                {
                    //DataPAVALION.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataPAVALION.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbPavalion.AccessibleDescription = pStrParaType;
                    CmbPavalion.DataSource = DataPAVALION;
                    CmbPavalion.DisplayMember = pStrDisplayText;
                    CmbPavalion.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "TENSION")
                {
                    //DataTENSION.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataTENSION.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbTension.AccessibleDescription = pStrParaType;
                    CmbTension.DataSource = DataTENSION;
                    CmbTension.DisplayMember = pStrDisplayText;
                    CmbTension.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "SAKHAT") //#P : 26-10-2020
                {
                    //DataSAKHAT.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataSAKHAT.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbSakhat.AccessibleDescription = pStrParaType;
                    CmbSakhat.DataSource = DataSAKHAT;
                    CmbSakhat.DisplayMember = pStrDisplayText;
                    CmbSakhat.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "COLORSHADE")
                {
                    //DataCOLORSHADE.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataCOLORSHADE.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbColorShade.AccessibleDescription = pStrParaType;
                    CmbColorShade.DataSource = DataCOLORSHADE;
                    CmbColorShade.DisplayMember = pStrDisplayText;
                    CmbColorShade.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "LUSTER")
                {
                    //DataLUSTER.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataLUSTER.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbLuster.AccessibleDescription = pStrParaType;
                    CmbLuster.DataSource = DataLUSTER;
                    CmbLuster.DisplayMember = pStrDisplayText;
                    CmbLuster.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "EYECLEAN")
                {
                    //DataEYECLEAN.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataEYECLEAN.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbEyeClean.AccessibleDescription = pStrParaType;
                    CmbEyeClean.DataSource = DataEYECLEAN;
                    CmbEyeClean.DisplayMember = pStrDisplayText;
                    CmbEyeClean.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "NATURAL")
                {
                    //DataNATURAL.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataNATURAL.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbNatural.AccessibleDescription = pStrParaType;
                    CmbNatural.DataSource = DataNATURAL;
                    CmbNatural.DisplayMember = pStrDisplayText;
                    CmbNatural.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "GRAIN")
                {
                    //DataGRAIN.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataGRAIN.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbGrain.AccessibleDescription = pStrParaType;
                    CmbGrain.DataSource = DataGRAIN;
                    CmbGrain.DisplayMember = pStrDisplayText;
                    CmbGrain.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "LAB")
                {
                    //DataLAB.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataLAB.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbLab.AccessibleDescription = pStrParaType;
                    CmbLab.DataSource = DataLAB;
                    CmbLab.DisplayMember = pStrDisplayText;
                    CmbLab.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "TABLEOPEN")
                {
                    //DataLAB.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataTABLEOPEN.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbTableOpen.AccessibleDescription = pStrParaType;
                    CmbTableOpen.DataSource = DataTABLEOPEN;
                    CmbTableOpen.DisplayMember = pStrDisplayText;
                    CmbTableOpen.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "CROWNOPEN")
                {
                    //DataLAB.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataCROWNOPEN.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbCrownOpen.AccessibleDescription = pStrParaType;
                    CmbCrownOpen.DataSource = DataCROWNOPEN;
                    CmbCrownOpen.DisplayMember = pStrDisplayText;
                    CmbCrownOpen.ValueMember = "PARA_ID";
                }
                else if (pStrParaType == "PAVILLIONOPEN")
                {
                    //DataLAB.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        DataPAVILLIONOPEN.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                    }
                    CmbPavillionOpen.AccessibleDescription = pStrParaType;
                    CmbPavillionOpen.DataSource = DataPAVILLIONOPEN;
                    CmbPavillionOpen.DisplayMember = pStrDisplayText;
                    CmbPavillionOpen.ValueMember = "PARA_ID";
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        private void SetControl()
        {
            DTabParameter = ObjRap.GetAllParameterTable();

            DTabRapDate = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.RAPDATE);
            DTabRapDate.DefaultView.Sort = "RAPDATE DESC";
            DTabRapDate = DTabRapDate.DefaultView.ToTable();

            DTabPrediction = ObjRap.GetPredictionData(-1, "-1", -1, -1);

            DesignSystemButtion(PanelShape, "SHAPE", "PARACODE", "SHAPE", 15, 50);
            DesignSystemButtion(PanelColor, "COLOR", "PARANAME", "COLOR", 15, 45);
            DesignSystemButtion(PanelClarity, "CLARITY", "PARANAME", "CLARITY", 15, 45);
            DesignSystemButtion(PanelCut, "CUT", "PARACODE", "CUT", 15, 45);
            DesignSystemButtion(PanelPol, "POLISH", "PARACODE", "POL", 15, 45);
            DesignSystemButtion(PanelSym, "SYMMETRY", "PARACODE", "SYM", 15, 45);
            DesignSystemButtion(PanelFL, "FLUORESCENCE", "PARANAME", "FL", 15, 45  );

            DesignComboBox("MILKY", CmbMilky, "PARANAME");
            DesignComboBox("LBLC", CmbLBLC, "PARANAME");
            DesignComboBox("NATTS", CmbNatts, "PARANAME");
            DesignComboBox("TENSION", CmbTension, "PARANAME");
            DesignComboBox("SAKHAT", CmbSakhat, "PARANAME");

            DesignComboBox("NATURAL", CmbNatural, "PARANAME");

            DesignComboBox("BLACK", CmbBlackInc, "PARACODE");
            DesignComboBox("OPEN", CmbOpenInc, "PARACODE");
            DesignComboBox("WHITE", CmbWhiteInc, "PARACODE");
            DesignComboBox("HEARTANDARROW", CmbHA, "PARACODE");
            DesignComboBox("PAVALION", CmbPavalion, "PARACODE");
            DesignComboBox("COLORSHADE", CmbColorShade, "PARACODE");
            DesignComboBox("LUSTER", CmbLuster, "PARACODE");
            DesignComboBox("EYECLEAN", CmbEyeClean, "PARACODE");
            DesignComboBox("GRAIN", CmbGrain, "PARACODE");
            DesignComboBox("LAB", CmbLab, "PARACODE");

            DesignComboBox("TABLEOPEN", CmbTableOpen, "PARACODE");
            DesignComboBox("CROWNOPEN", CmbCrownOpen, "PARACODE");
            DesignComboBox("PAVILLIONOPEN", CmbPavillionOpen, "PARACODE");

            CmbRapDate.Items.Clear();
            foreach (DataRow DRow in DTabRapDate.Rows)
            {
                CmbRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }
            CmbRapDate.SelectedIndex = 0;


            MainGrid.DataSource = DTabPrediction;
            MainGrid.Refresh();

            GrdDet.Columns["PLANNO"].Group();
            GrdDet.Columns["PLANNO"].Visible = true;
            if (GrdDet.GroupSummary.Count == 0)
            {
                GrdDet.GroupSummary.Add(SummaryItemType.Count, "TAG", GrdDet.Columns["TAG"], "{0:N0}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "CARAT", GrdDet.Columns["CARAT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "AMOUNT", GrdDet.Columns["AMOUNT"], "{0:N0}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "AMOUNTDISCOUNT", GrdDet.Columns["AMOUNTDISCOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "DISCOUNT", GrdDet.Columns["DISCOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "RAPAPORT", GrdDet.Columns["RAPAPORT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "PRICEPERCARAT", GrdDet.Columns["PRICEPERCARAT"], "{0:N3}");

                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "GCARAT", GrdDet.Columns["GCARAT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "GAMOUNT", GrdDet.Columns["GAMOUNT"], "{0:N0}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "GAMOUNTDISCOUNT", GrdDet.Columns["GAMOUNTDISCOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "GDISCOUNT", GrdDet.Columns["GDISCOUNT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "GRAPAPORT", GrdDet.Columns["GRAPAPORT"], "{0:N3}");
                GrdDet.GroupSummary.Add(SummaryItemType.Custom, "GPRICEPERCARAT", GrdDet.Columns["GPRICEPERCARAT"], "{0:N3}");

                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "DOWNCOLORAMOUNT", GrdDet.Columns["DOWNCOLORAMOUNT"], "{0:N0}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "UPCOLORAMOUNT", GrdDet.Columns["UPCOLORAMOUNT"], "{0:N0}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "DOWNCLARITYAMOUNT", GrdDet.Columns["DOWNCLARITYAMOUNT"], "{0:N0}");
                GrdDet.GroupSummary.Add(SummaryItemType.Sum, "UPCLARITYAMOUNT", GrdDet.Columns["UPCLARITYAMOUNT"], "{0:N0}");
            }

            //GrdDet.Columns["PLANNO"].Group();
            GrdDet.ExpandAllGroups();

        }

        #endregion

        #region Events

        private void lbl1_Click(object sender, EventArgs e)
        {
            AxonContLib.cButton lbl1 = (AxonContLib.cButton)sender;

            if (lbl1.Tag.ToString() == "C")
            {
                PanelCarat.Text = "";
            }
            else if (lbl1.Tag.ToString() == "BACK")
            {
                if (PanelCarat.Text.Length != 0)
                {
                    PanelCarat.Text = PanelCarat.Text.Substring(0, PanelCarat.Text.Length - 1);
                }
                // return;
            }
            else if (lbl1.Tag.ToString() == ".")
            {
                if (PanelCarat.Text.Contains(".") == true)
                {
                    return;
                }
                PanelCarat.Text = PanelCarat.Text + ".";
            }
            else if (Information.IsNumeric(PanelCarat.Text + lbl1.Tag.ToString()) == true)
            {
                PanelCarat.Text = PanelCarat.Text + lbl1.Tag.ToString();
            }

            if (PanelCarat.Text.Equals("."))
                PanelCarat.Text = "0.";

            if (PanelCarat.Text.StartsWith("."))
            {
                PanelCarat.Text = "0" + PanelCarat.Text;
            }

            if (GrdDet.FocusedRowHandle >= 0 && GrdDet.SelectedRowsCount >= 0)
            {
                if (RbtExp.Checked == true)
                {
                    //DTabPrediction.Rows[GrdDet.FocusedRowHandle]["CARAT"] = Val.Val(PanelCarat.Text);
                    GrdDet.SetFocusedRowCellValue("CARAT", Val.Val(PanelCarat.Text));
                }
                else
                {
                    //DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GCARAT"] = Val.Val(PanelCarat.Text);
                    GrdDet.SetFocusedRowCellValue("GCARAT", Val.Val(PanelCarat.Text));
                }
                FindRap();
            }


            //if (MainGrid.Enabled == false)
            //{
            //    Global.MessageError("Grid Is Unable To Update");
            //    return;
            //}

            //AxonContLib.cLabel lbl = (AxonContLib.cLabel)sender;

            //foreach (AxonContLib.cLabel Cont in PanelCarat.Controls)
            //{
            //    if (Cont.BackColor == Color.FromArgb(255, 224, 192))
            //    {
            //        if (Cont.Name.Contains(Val.Left(lbl.Name, 2)))
            //        {
            //            Cont.BackColor = Color.LightGray;
            //        }
            //    }
            //}

            //if (lbl.BackColor == Color.LightGray)
            //{
            //    lbl.BackColor = Color.FromArgb(255, 224, 192);
            //}

            //string N1 = "", N2 = "", N3 = "", N4 = "", N5 = "";

            //foreach (AxonContLib.cLabel Cont in PanelCarat.Controls)
            //{
            //    if (Cont.BackColor == Color.FromArgb(255, 224, 192))
            //    {
            //        if (Cont.Name.Contains("N1"))
            //        {
            //            N1 = Cont.Tag.ToString();
            //        }
            //        if (Cont.Name.Contains("N2"))
            //        {
            //            N2 = Cont.Tag.ToString();
            //        }
            //        if (Cont.Name.Contains("N3"))
            //        {
            //            N3 = Cont.Tag.ToString();
            //        }
            //        if (Cont.Name.Contains("N4"))
            //        {
            //            N4 = Cont.Tag.ToString();
            //        }
            //        if (Cont.Name.Contains("N5"))
            //        {
            //            N5 = Cont.Tag.ToString();
            //        }
            //    }
            //}

            //string StrNumber = N1 + N2 + "." + N3 + N4 + N5;

            //PanelCarat.Text = Val.Format(Val.Val(StrNumber), "###0.000");
            ////SendKeys.Send("{" + (lbl.Tag.ToString() + "}"));

            //if (GrdDet.FocusedRowHandle >= 0 && GrdDet.SelectedRowsCount >= 0)
            //{
            //    if (RbtExp.Checked == true)
            //    {
            //        GrdDet.SetFocusedRowCellValue("CARAT", Val.Val(PanelCarat.Text));
            //    }
            //    else
            //    {
            //        GrdDet.SetFocusedRowCellValue("GCARAT", Val.Val(PanelCarat.Text));
            //    }
            //    FindRap();
            //}
        }

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
                if (txtTag.Enabled == true && txtTag.Text.Trim().Length == 0)
                {
                    Global.MessageError("Tag Is Required");
                    txtTag.Focus();
                    return;
                }

                if (txtTag.Enabled == true && Val.ToString(txtTag.Tag).Length == 0)
                {
                    Global.MessageError("Packet ID Not Found In this PacketNo");
                    txtTag.Focus();
                    return;
                }

                if (Val.ToInt(CmbPrdType.Tag) == 8 && CmbLabProcess.SelectedIndex == 0)
                {
                    Global.MessageError("You Have To Select Graph/NonGraph  For BY Transfer While Making Grading Entry");
                    CmbLabProcess.Focus();
                    return;
                }

                if (Val.ToInt(CmbPrdType.Tag) == 8 && Val.ToString(CmbLabProcess.SelectedItem) == "GRAPH" && CmbLabSelection.SelectedIndex == 0)
                {
                    Global.MessageError("You Have To Select GIA / IGI / HED Lab Selection For BY Transfer While Makeing Grading Entry");
                    CmbLabSelection.Focus();
                    return;
                }




                // #D : 20-06-2020 : Validation For Check That CalcTypePer Is Less Then RapCalcTypeper
                if ((Val.ToInt32(CmbPrdType.Tag) == 3 || Val.ToInt32(CmbPrdType.Tag) == 4) && Val.ToString(txtPassForDisplayBack.Tag).ToUpper() != txtPassForDisplayBack.Text.ToUpper())
                {
                    Trn_RapSaveProperty ValProperty = new Trn_RapSaveProperty();

                    ValProperty.KAPANNAME = Val.ToString(txtKapanName.Text);
                    ValProperty.PACKETNO = Val.ToInt(txtPacketNo.Text);
                    ValProperty.TAG = Val.ToString(txtTag.Text);

                    ValProperty.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                    ValProperty.BALANCECARAT = Val.Val(lblBalance.Text);
                    ValProperty.CARAT = Val.Val(PanelCarat.Text);

                    ValProperty = ObjRap.ValSaveRapCalcValidationForper(ValProperty);
                    if (ValProperty.ReturnMessageType == "FAIL")
                    {
                        BtnBlankNew.Enabled = false;
                        BtnGeneratePlans.Enabled = false;

                        this.Cursor = Cursors.Default;
                        Global.MessageError(ValProperty.ReturnMessageDesc);
                        return;
                    }
                    ValProperty = null;
                }

                //#P : 28-01-2020 : Check That On Final And Checker Prd Max Amount Pla Is Selected Or Not.
                if (DTabPrediction.Select("ISFINAL = true").Count() > 0 && (Val.ToInt32(CmbPrdType.Tag) == 2 || Val.ToInt32(CmbPrdType.Tag) == 10))
                {
                    double DouMaxAmount = 0, DouSelectedAmount = 0;

                    DataTable DtabAmount = DTabPrediction.AsEnumerable()
                    .OrderBy(r => r.Field<decimal>("AMOUNT"))
                    .GroupBy(r => r.Field<int>("PLANNO"))
                    .Select(g =>
                    {
                        var row = DTabPrediction.NewRow();
                        row["PLANNO"] = g.Key;
                        row["AMOUNT"] = g.Sum(r => r.Field<decimal>("AMOUNT"));
                        return row;
                    }).CopyToDataTable();

                    DouMaxAmount = Val.Val(DtabAmount.Compute("MAX(AMOUNT)", string.Empty));
                    DouSelectedAmount = Val.Val(DTabPrediction.Compute("SUM(AMOUNT)", "ISFINAL = 1"));

                    if (DouSelectedAmount != DouMaxAmount)
                    {
                        Global.MessageError("You Have To Select Maximum Amount Plan No As Final...");
                        return;
                    }
                }
                //End : #P : 28-01-2020

                // Dhara : 14-4-2023 Validation For Plan Amount
                if (Val.ToInt32(CmbPrdType.Tag) == 2)
                {
                    double pDouPlaAmount = Val.Val(DTabPrediction.Compute("SUM(AMOUNT)", "ISFINAL = true"));

                    Trn_RapSaveProperty Property = new Trn_RapSaveProperty();
                    Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                    Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                    Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                    Property.TAG = "A";
                    Property.MTAG = "A";
                    Property.PACKET_ID = Val.ToInt64(txtTag.Tag);
                    Property.AMOUNT = pDouPlaAmount;

                    Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                    Property = ObjRap.ValidationForFinalPalnAmt(Property);
                    if (Property.ReturnMessageType == "FAIL")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        return;
                    }
                    Property = null;
                }
                // End #Dhara : 14-04-2023


                // THIS VALIDATION IS JUST CHECKING TAG SEQUENCE
                string MTag = txtTag.Enabled == false ? "A" : txtTag.Text;

                if (Val.ToString(txtPassForDisplayBack.Tag) != "" && Val.ToString(txtPassForDisplayBack.Tag).ToUpper() == txtPassForDisplayBack.Text.ToUpper())
                {

                }
                else
                {
                    DataTable DTabDistinct = DTabPrediction.DefaultView.ToTable(true, "PLANNO");

                    foreach (DataRow DRPlan in DTabDistinct.Rows)
                    {
                        DataRow[] UDRows = DTabPrediction.Select(" PlanNo = '" + Val.ToString(DRPlan["PLANNO"]) + "'");
                        if (UDRows == null || UDRows.Length == 0)
                        {
                            continue;
                        }
                        DataTable DTabPlan = UDRows.CopyToDataTable();
                        DTabPlan.DefaultView.Sort = "TAGSRNO ASC";
                        DTabPlan = DTabPlan.DefaultView.ToTable();
                        string StrPrevTag = "";
                        string StrCurrTag = "";

                        int PrevTagSrNo = 0;
                        int CurrTagSrNo = 0;

                        for (int IntI = 0; IntI < DTabPlan.Rows.Count; IntI++)
                        {
                            StrCurrTag = Val.ToString(DTabPlan.Rows[IntI]["TAG"]);
                            CurrTagSrNo = Val.ToInt32(DTabPlan.Rows[IntI]["TAGSRNO"]);
                            if (IntI == 0 && MTag != StrCurrTag)
                            {
                                // Check Whethere Packet Is Rejection Out Or Not Other Wise.. Skip 
                                Trn_RapSaveProperty Property = new Trn_RapSaveProperty();
                                Property.KAPANNAME = txtKapanName.Text;
                                Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                                Property.TAG = MTag;
                                Property = ObjRap.CheckRejectionOut(Property);
                                if (Property.ReturnMessageType == "FAIL")
                                {
                                    Property = null;
                                    Global.MessageError("Main Tag Is Not Found In PlanNo : " + Val.ToString(DRPlan["PLANNO"]) + "");
                                    return;
                                }
                                Property = null;
                            }
                            if (IntI > 0)
                            {
                                int IntPrev = 0;
                                int IntCurr = 0;

                                //#p : changed : 07-10-2020
                                //IntPrev = (int)Char.Parse(StrPrevTag);
                                //IntCurr = (int)Char.Parse(StrCurrTag);

                                if (StrPrevTag.Length > 1)
                                {
                                    IntPrev = (int)Char.Parse(StrPrevTag.Substring(0, 1)) + (int)Char.Parse(StrPrevTag.Substring(1, 1));
                                }
                                else
                                {
                                    IntPrev = (int)Char.Parse(StrPrevTag);
                                }

                                if (StrCurrTag.Length > 1)
                                {
                                    string Str1 = StrCurrTag.Substring(0, 1);
                                    string Str2 = StrCurrTag.Substring(1, 1);

                                    IntCurr = (int)Char.Parse(StrCurrTag.Substring(0, 1)) + (int)Char.Parse(StrCurrTag.Substring(1, 1));
                                }
                                else
                                {
                                    IntCurr = (int)Char.Parse(StrCurrTag);
                                }
                                //End : #p : changed : 07-10-2020

                                if (Math.Abs(IntCurr - IntPrev) > 1 && (IntPrev != 90 && IntCurr != 130))
                                {
                                    Trn_RapSaveProperty Property = new Trn_RapSaveProperty();
                                    Property.KAPANNAME = txtKapanName.Text;
                                    Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                                    Property.TAG = StrPrevTag;
                                    Property = ObjRap.CheckRejectionOut(Property);
                                    if (Property.ReturnMessageType == "FAIL")
                                    {
                                        Property = null;
                                        Global.MessageError("Tag Sequence Is Not Matched Please Check In PlanNo : " + Val.ToString(DRPlan["PLANNO"]) + " ( " + StrPrevTag + " & " + StrCurrTag + " )");
                                        return;
                                    }
                                    Property = null;
                                }
                            }

                            StrPrevTag = StrCurrTag;
                            PrevTagSrNo = CurrTagSrNo;
                        }

                        DTabPlan.Dispose();
                        DTabPlan = null;
                        UDRows = null;
                    }

                    DTabDistinct.Dispose();
                    DTabDistinct = null;
                }


                //#P : 12-10-2020
                bool ISSaveWithPassword = false;

                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    try
                    {

                        DataRow DRow = GrdDet.GetDataRow(IntI);

                        if (DRow == null)
                            continue;

                        if (DRow != null && Val.Val(DRow["CARAT"]) != 0 && Val.Val(DRow["AMOUNT"]) <= 0)
                        {
                            Global.Confirm("Amount Is Zero In Row (" + (IntI + 1) + ") Please Change Criteria. Or Contact To System Administrator");
                            return;
                        }


                        //#P : 12-09-2022
                        // Check Lot Carat For Final Prediction else check Balance Carat
                        if (Val.ToInt32(CmbPrdType.Tag) == 2) // Final Prediction
                        {
                            if (DRow != null && Val.Val(DRow["CARAT"]) != 0 && Val.Val(DRow["CARAT"]) > Val.Val(lblLot.Text)) //&& (Val.ToString(txtPassForDisplayBack.Tag).ToUpper() != txtPassForDisplayBack.Text.ToUpper()))
                            {
                                Global.MessageError("Prd Carat Must Be Less Than Lot Carat (" + (IntI + 1) + ") Please Change Criteria. Or Contact To System Administrator");
                                return;
                            }
                        }
                        else
                        {
                            if (DRow != null && Val.Val(DRow["CARAT"]) != 0 && Val.Val(DRow["CARAT"]) > Val.Val(lblBalance.Text)) //&& (Val.ToString(txtPassForDisplayBack.Tag).ToUpper() != txtPassForDisplayBack.Text.ToUpper()))
                            {
                                Global.MessageError("Prd Carat Must Be Less Than Balance Carat (" + (IntI + 1) + ") Please Change Criteria. Or Contact To System Administrator");
                                return;
                            }
                        }
                        if (Val.Val(lblBalance.Text) == 0)
                        {
                            Global.MessageError("Balance Carat Should Not Be Zero Please Check Or Contact To System Administrator");
                            return;
                        }
                        //End : #P : 12-09-2022

                        // #P : 05-09-2020

                        //if (DRow != null && Val.Val(DRow["GCARAT"]) > Val.Val(DRow["CARAT"]))
                        //{
                        //    Global.MessageError("Graph Carat > Expected Carat In Row (" + (IntI + 1) + ") Please Check it. Or Contact To System Administrator");
                        //    return;
                        //}
                        //if (DRow != null && Val.Val(DRow["GAMOUNT"]) > Val.Val(DRow["AMOUNT"]))
                        //{
                        //    Global.MessageError("Graph Amount > Expected Amount In Row (" + (IntI + 1) + ") Please Check it. Or Contact To System Administrator");
                        //    return;
                        //}

                        //End : #P : 05-09-2020

                        // Validation For Check With Makable and planning 
                        Trn_RapSaveProperty Property = new Trn_RapSaveProperty();
                        Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                        Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                        Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                        Property.TAG = txtTag.Text;
                        Property.MTAG = txtTag.Text;
                        Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                        Property.SHAPE_ID = Val.ToInt32(DRow["SHAPE_ID"]);
                        Property.SHAPENAME = Val.ToString(DRow["SHAPENAME"]);

                        Property.COLOR_ID = Val.ToInt32(DRow["COLOR_ID"]);
                        Property.COLORNAME = Val.ToString(DRow["COLORNAME"]);

                        Property.CLARITY_ID = Val.ToInt32(DRow["CLARITY_ID"]);
                        Property.CLARITYNAME = Val.ToString(DRow["CLARITYNAME"]);

                        Property.CARAT = Val.Val(DRow["CARAT"]);
                        Property.BALANCECARAT = Val.Val(lblBalance.Text);

                        Property.LABPROCESS = Val.ToString(CmbLabProcess.SelectedItem);
                        Property.DIAMIN = Val.Val(DRow["DIAMIN"]);
                        Property.DIAMAX = Val.Val(DRow["DIAMAX"]);
                        Property.HEIGHT = Val.Val(DRow["HEIGHT"]);

                        Property.ISMIXRATE = Val.ToBoolean(DRow["ISMIXRATE"]);
                       
                        Property.ISBYPASSVALIDATION = false;
                        if (txtPasswordForByPassValidation.Text.Trim().Length != 0 && pStrPassword == txtPasswordForByPassValidation.Text.ToLower())
                        {
                            Property.ISBYPASSVALIDATION = true;
                        }
                        Property = ObjRap.ValSaveCheckWithMakable(Property);
                        if (Property.ReturnMessageType == "FAIL")
                        {
                            BtnBlankNew.Enabled = false;
                            BtnGeneratePlans.Enabled = false;

                            this.Cursor = Cursors.Default;
                            Global.MessageError(Property.ReturnMessageDesc);
                            return;
                        }
                       
                        if (Val.ToInt(CmbPrdType.Tag) == 2 || Val.ToInt(CmbPrdType.Tag) == 4)
                        {
                            Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                            Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                            Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                            Property.TAG = txtTag.Text;
                            Property.MTAG = txtTag.Text;
                            Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);

                            Property.CARAT = Val.Val(DRow["CARAT"]);
                            Property.BALANCECARAT = Val.Val(lblBalance.Text);


                            Property = ObjRap.ValSaveCheckWithRoughType(Property);
                            if (Property.ReturnMessageType == "FAIL")
                            {
                                BtnBlankNew.Enabled = false;
                                BtnGeneratePlans.Enabled = false;

                                this.Cursor = Cursors.Default;
                                Global.MessageError(Property.ReturnMessageDesc);
                                return;
                            }

                        }

                        Property = null;


                        
                    }
                    catch (Exception EXlOP)
                    {
                        Global.Message(EXlOP.Message.ToString());
                    }
                }

                if (mIntOldISFinalFlagPlanNo != 0 && DTabPrediction.Select("ISFINAL = true").Count() <= 0 && Val.ToInt(CmbPrdType.Tag) != 4)
                {
                    Global.MessageError("You Have To Select Final Plan..");
                    return;
                }

                //#P : 11-10-2019
                bool ISFinalFlagChanged = false;
                //Int32 IntOldTFlagPlanNo = 0;
                Int32 IntNewISFinalFlagPlanNo = 0;
                Int32 IntNewTFlagPlanNo = 0;  //#P : 06-02-2020  
                if (DTabPrediction.Select("ISFINAL = true").Count() > 0)
                {
                    IntNewISFinalFlagPlanNo = Val.ToInt32((DTabPrediction.AsEnumerable().Where(p => p.Field<bool>("ISFINAL") == true).Select(p => p.Field<Int32>("PLANNO"))).FirstOrDefault());
                    if (IntNewISFinalFlagPlanNo != Val.ToInt32(mIntOldISFinalFlagPlanNo))
                    {
                        ISFinalFlagChanged = true;
                    }
                }

                DataRow[] DrowTFlag = DTabPrediction.Select("TFLAG = True");
                if (DrowTFlag != null && DrowTFlag.Length > 0)
                {
                    IntNewTFlagPlanNo = Val.ToInt32((DTabPrediction.AsEnumerable().Where(p => p.Field<bool>("TFLAG") == true).Select(p => p.Field<Int32>("PLANNO"))).FirstOrDefault());
                }

                //#P : 05-03-2021 : Check File Is Exists In Network Path Or Not For FinalPrd/RMkbl/FMkbl
                string StrFileTransferType = "";
                string StrDirectoryPath = "";
                string StrKapanPacketNo = "";

                

                if (Global.Confirm("Are You Sure To Save Prediction Entry ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                int IntIndex = 0;
                this.Cursor = Cursors.WaitCursor;

                Int64 IntPrdID = 0;
                if (Val.ToInt64(lblPrdID.Text) == 0)
                {
                    IntPrdID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.PRD_ID);
                }
                else
                {
                    IntPrdID = Val.ToInt64(lblPrdID.Text);
                }

                //#D : 27-11-2020
                if (Val.ToInt32(CmbPrdType.Tag) == 4)
                {
                    DTabMakLog.TableName = "Table";
                    string StrXMLValues = string.Empty;
                    using (StringWriter sw = new StringWriter())
                    {
                        DTabMakLog.WriteXml(sw);
                        StrXMLValues = sw.ToString();
                    }
                    DataTable DTabResult = ObjRap.SaveMakLOg(StrXMLValues);
                }
                //End : #D : 27-11-2020

                bool mISTFlag = false;
                if (IntNewTFlagPlanNo > 0)
                    mISTFlag = true;
                else
                    mISTFlag = false;

                ObjRap.DeleteAll(Val.ToInt(CmbPrdType.Tag), txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Enabled == false ? "A" : txtTag.Text, Val.ToInt64(txtEmployee.Tag), Val.ToString(txtTag.Tag), mISTFlag);

                //ArrayList AL = new ArrayList();
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);

                    if (DRow == null || (Val.Val(DRow["AMOUNT"]) == 0 && Val.Val(DRow["CARAT"]) == 0))
                    {
                        continue;
                    }

                    IntIndex++;
                    Trn_RapSaveProperty Property = new Trn_RapSaveProperty();

                    Property.PACKET_ID = Val.ToInt64(txtTag.Tag);
                    Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                    Property.PRDTYPE = Val.ToString(CmbPrdType.SelectedItem);
                    Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                    Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                    Property.MTAG = txtTag.Enabled == false ? "A" : txtTag.Text;
                    Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                    Property.MANAGER_ID = Val.ToInt64(txtManager.Tag);

                    Property.ID = Val.ToInt64(DRow["ID"]);
                    if (Val.Val(Property.ID) == 0)
                    {
                        Property.ID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.ID);
                    }
                    Property.PRD_ID = IntPrdID;

                    Property.TAGSRNO = Val.ToInt(DRow["TAGSRNO"]);
                    Property.TAG = Val.ToString(DRow["TAG"]);
                    Property.PLANNO = Val.ToInt(DRow["PLANNO"]);

                    if (Val.ToString(Property.TAG) == "A")
                        mStrEntryDate = Val.ToString(DRow["ENTRYDATE"]);

                    Property.SHAPE_ID = Val.ToInt(DRow["SHAPE_ID"]);
                    Property.CLARITY_ID = Val.ToInt(DRow["CLARITY_ID"]);
                    Property.COLOR_ID = Val.ToInt(DRow["COLOR_ID"]);
                    Property.COLORSHADE_ID = Val.ToInt(DRow["COLORSHADE_ID"]);
                    Property.CUT_ID = Val.ToInt(DRow["CUT_ID"]);
                    Property.POL_ID = Val.ToInt(DRow["POL_ID"]);
                    Property.SYM_ID = Val.ToInt(DRow["SYM_ID"]);
                    Property.FL_ID = Val.ToInt(DRow["FL_ID"]);
                    Property.MILKY_ID = Val.ToInt(DRow["MILKY_ID"]);
                    Property.LBLC_ID = Val.ToInt(DRow["LBLC_ID"]);
                    Property.NATTS_ID = Val.ToInt(DRow["NATTS_ID"]);
                    Property.TENSION_ID = Val.ToInt(DRow["TENSION_ID"]);
                    Property.SAKHAT_ID = Val.ToInt(DRow["SAKHAT_ID"]);
                    Property.BLACKINC_ID = Val.ToInt(DRow["BLACKINC_ID"]);
                    Property.OPENINC_ID = Val.ToInt(DRow["OPENINC_ID"]);
                    Property.WHITEINC_ID = Val.ToInt(DRow["WHITEINC_ID"]);
                    Property.LUSTER_ID = Val.ToInt(DRow["LUSTER_ID"]);
                    Property.HA_ID = Val.ToInt(DRow["HA_ID"]);
                    Property.PAV_ID = Val.ToInt(DRow["PAV_ID"]);
                    Property.EYECLEAN_ID = Val.ToInt(DRow["EYECLEAN_ID"]);
                    Property.NATURAL_ID = Val.ToInt(DRow["NATURAL_ID"]);
                    Property.GRAIN_ID = Val.ToInt(DRow["GRAIN_ID"]);

                    Property.CARAT = Val.Val(DRow["CARAT"]);
                    Property.DISCOUNT = Val.Val(DRow["DISCOUNT"]);
                    Property.AMOUNTDISCOUNT = Val.Val(DRow["AMOUNTDISCOUNT"]);
                    Property.RAPAPORT = Val.Val(DRow["RAPAPORT"]);
                    Property.PRICEPERCARAT = Val.Val(DRow["PRICEPERCARAT"]);
                    Property.AMOUNT = Val.Val(DRow["AMOUNT"]);
                    Property.RAPDATE = Val.SqlDate(Val.ToString(DRow["RAPDATE"]));

                    Property.GCARAT = Val.Val(DRow["GCARAT"]);
                    Property.GCOLOR_ID = Val.ToInt(DRow["GCOLOR_ID"]);
                    Property.GCLARITY_ID = Val.ToInt(DRow["GCLARITY_ID"]);
                    Property.GCUT_ID = Val.ToInt(DRow["GCUT_ID"]);
                    Property.GPOL_ID = Val.ToInt(DRow["GPOL_ID"]);
                    Property.GSYM_ID = Val.ToInt(DRow["GSYM_ID"]);

                    Property.GDISCOUNT = Val.Val(DRow["GDISCOUNT"]);
                    Property.GAMOUNTDISCOUNT = Val.Val(DRow["GAMOUNTDISCOUNT"]);
                    Property.GRAPAPORT = Val.Val(DRow["GRAPAPORT"]);
                    Property.GPRICEPERCARAT = Val.Val(DRow["GPRICEPERCARAT"]);
                    Property.GAMOUNT = Val.Val(DRow["GAMOUNT"]);

                    //Add : Pinali : 07-09-2019
                    Property.MDISCOUNT = Val.Val(DRow["MDISCOUNT"]);
                    Property.MPRICEPERCARAT = Val.Val(DRow["MPRICEPERCARAT"]);
                    Property.MAMOUNT = Val.Val(DRow["MAMOUNT"]);
                    Property.MGDISCOUNT = Val.Val(DRow["MGDISCOUNT"]);
                    Property.MGPRICEPERCARAT = Val.Val(DRow["MGPRICEPERCARAT"]);
                    Property.MGAMOUNT = Val.Val(DRow["MGAMOUNT"]);
                    //End : Pinali : 07-09-2019

                    if (txtTag.Enabled == true)
                    {
                        Property.ISFINAL = true;
                    }
                    else
                    {
                        Property.ISFINAL = Val.ToBoolean(DRow["ISFINAL"]);
                    }

                    Property.TFLAG = Val.ToBoolean(DRow["TFLAG"]);

                    //if (Val.ISDate(Val.ToString(DRow["ENTRYDATE"])))
                    if (Val.ISDate(Val.ToString(DRow["ENTRYDATE"]))) //&& ISFinalFlagChanged == false
                    {
                        //Property.ENTRYDATE = DateTime.Parse(Val.ToString(DRow["ENTRYDATE"])).ToString("yyyy-MM-dd HH:mm:ss");   //Changed : #P : Coz Consider "A" Tag EntryDate For All Their Sub Pcs Plan Wise..When Tag is not enable
                        Property.ENTRYDATE = txtTag.Enabled == false ? DateTime.Parse(Val.ToString(mStrEntryDate)).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Parse(Val.ToString(DRow["ENTRYDATE"])).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        //Property.ENTRYDATE = null;
                        Property.ENTRYDATE = txtTag.Enabled == false && Val.ToString(mStrEntryDate) != "" ? DateTime.Parse(Val.ToString(mStrEntryDate)).ToString("yyyy-MM-dd HH:mm:ss") : null;
                    }

                    //////Add : Pinali : 11-10-2019
                    //if (mIntISFinalFlagPlanNo != Val.ToInt32(DRow["PLANNO"]) && Property.ISFINAL == true) 
                    //{
                    //    Property.ENTRYDATE = null;
                    //}

                    //// End : Pinali : 11-10-2019

                    Property.LAB_ID = Val.ToInt(DRow["LAB_ID"]);

                    Property.UPCOLOR_ID = Val.ToInt(DRow["UPCOLOR_ID"]);
                    Property.UPCOLORDISCOUNT = Val.Val(DRow["UPCOLORDISCOUNT"]);
                    Property.UPCOLORAMOUNTDISCOUNT = Val.Val(DRow["UPCOLORAMOUNTDISCOUNT"]);
                    Property.UPCOLORRAPAPORT = Val.Val(DRow["UPCOLORRAPAPORT"]);
                    Property.UPCOLORPRICEPERCARAT = Val.Val(DRow["UPCOLORPRICEPERCARAT"]);
                    Property.UPCOLORAMOUNT = Val.Val(DRow["UPCOLORAMOUNT"]);

                    Property.DOWNCOLOR_ID = Val.ToInt(DRow["DOWNCOLOR_ID"]);
                    Property.DOWNCOLORDISCOUNT = Val.Val(DRow["DOWNCOLORDISCOUNT"]);
                    Property.DOWNCOLORAMOUNTDISCOUNT = Val.Val(DRow["DOWNCOLORAMOUNTDISCOUNT"]);
                    Property.DOWNCOLORRAPAPORT = Val.Val(DRow["DOWNCOLORRAPAPORT"]);
                    Property.DOWNCOLORPRICEPERCARAT = Val.Val(DRow["DOWNCOLORPRICEPERCARAT"]);
                    Property.DOWNCOLORAMOUNT = Val.Val(DRow["DOWNCOLORAMOUNT"]);

                    Property.UPCLARITY_ID = Val.ToInt(DRow["UPCLARITY_ID"]);
                    Property.UPCLARITYDISCOUNT = Val.Val(DRow["UPCLARITYDISCOUNT"]);
                    Property.UPCLARITYAMOUNTDISCOUNT = Val.Val(DRow["UPCLARITYAMOUNTDISCOUNT"]);
                    Property.UPCLARITYRAPAPORT = Val.Val(DRow["UPCLARITYRAPAPORT"]);
                    Property.UPCLARITYPRICEPERCARAT = Val.Val(DRow["UPCLARITYPRICEPERCARAT"]);
                    Property.UPCLARITYAMOUNT = Val.Val(DRow["UPCLARITYAMOUNT"]);

                    Property.DOWNCLARITY_ID = Val.ToInt(DRow["DOWNCLARITY_ID"]);
                    Property.DOWNCLARITYDISCOUNT = Val.Val(DRow["DOWNCLARITYDISCOUNT"]);
                    Property.DOWNCLARITYAMOUNTDISCOUNT = Val.Val(DRow["DOWNCLARITYAMOUNTDISCOUNT"]);
                    Property.DOWNCLARITYRAPAPORT = Val.Val(DRow["DOWNCLARITYRAPAPORT"]);
                    Property.DOWNCLARITYPRICEPERCARAT = Val.Val(DRow["DOWNCLARITYPRICEPERCARAT"]);
                    Property.DOWNCLARITYAMOUNT = Val.Val(DRow["DOWNCLARITYAMOUNT"]);


                    Property.COPYFROMEMPLOYEE_ID = Val.ToInt64(DRow["COPYFROMEMPLOYEE_ID"]);
                    Property.COPYFROMPRD_ID = Val.ToString(DRow["COPYFROMPRD_ID"]);
                    Property.COPYFROM_ID = Val.ToString(DRow["COPYFROM_ID"]);
                    Property.COPYTOEMPLOYEE_ID = Val.ToInt64(DRow["COPYTOEMPLOYEE_ID"]);
                    Property.COPYTOPRD_ID = Val.ToString(DRow["COPYTOPRD_ID"]);
                    Property.COPYTO_ID = Val.ToString(DRow["COPYTO_ID"]);
                    Property.ISDIFF = Val.ToBoolean(DRow["ISDIFF"]);
                    Property.REMARK = txtRemark.Text;

                    Property.LABPROCESS = Val.ToString(CmbLabProcess.SelectedItem);
                    Property.LABSELECTION = Val.ToString(CmbLabSelection.SelectedItem);
                    Property.DIAMIN = Val.Val(DRow["DIAMIN"]);
                    Property.DIAMAX = Val.Val(DRow["DIAMAX"]);
                    Property.HEIGHT = Val.Val(DRow["HEIGHT"]);
                    Property.ISMIXRATE = Val.ToBoolean(DRow["ISMIXRATE"]);
                    Property.REPORTNO = Val.ToString(DRow["REPORTNO"]);

                    Property.ISSAVEWITHPASSWORD = Val.ToBoolean(ISSaveWithPassword); //#P : 27-01-2020

                    Property.ISPCNGRDBYLABENTRY = Val.ToBoolean(DRow["ISPCNGRDBYLABENTRY"]); //#P : 04-11-2019
                    Property.PCNGRDBYLAB_ID = Val.ToInt64(DRow["PCNGRDBYLAB_ID"]) == 0 ? 0 : Val.ToInt64(DRow["PCNGRDBYLAB_ID"]); //#P : 04-11-2019

                    Property.ISNOBGM = ChkNOBGM.Checked;
                    Property.ISNOBLACK = ChkNOBlack.Checked;

                    Property.SAWINGTYPE = Val.ToString(DRow["SAWINGTYPE"]);
                    Property.ISEXCELIMPORT = Val.ToBoolean(DRow["ISEXCELIMPORT"]);
                    Property.ROUGHWEIGHT = Val.Val(DRow["ROUGHWEIGHT"]);
                    Property.KACHUVAJAN = Val.Val(DRow["KACHUVAJAN"]);
                    Property.WIDTH = Val.Val(DRow["WIDTH"]);
                    Property.LENGTH = Val.Val(DRow["LENGTH"]);
                    Property.RATIO = Val.ToInt32(DRow["RATIO"]);
                    Property.BALANCECARAT = Val.Val(lblBalance.Text);
                    Property.TRN_ID = Val.ToInt64(lblBalance.Tag);

                    Property.ISCOPYROUGHMKBLPLANINTOFINALTFLAGPRD = Val.ToInt(ISCopyRoughMkblPlanIntoFinalTFlagPrd) == 1 ? true : false; //#P : 19-08-2020

                    if (Val.ToInt32(IntOldTFlagPlanNo) != 0 && ((IntOldTFlagPlanNo != IntNewISFinalFlagPlanNo) || IntNewTFlagPlanNo == 0) && ((Val.ToInt32(CmbPrdType.Tag) == 2) || Val.ToInt32(CmbPrdType.Tag) == 10)) //#P : 08-12-2020
                    {
                        Property.ISCHANGETFLAG = true;
                    }
                    else
                    {
                        Property.ISCHANGETFLAG = false;
                    }
                    Property.TO_ID = Val.ToInt(DRow["TO_ID"]);
                    Property.CO_ID = Val.ToInt(DRow["CO_ID"]);
                    Property.PO_ID = Val.ToInt(DRow["PO_ID"]);

                    //AL.Add(Property);
                    Property = ObjRap.Save(Property);

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        GrdDet.SetRowCellValue(IntI, "ID", Property.ReturnValue);
                    }

                    Property = null;
                }

                //string StrRes = ObjRap.SaveLoop(AL);
                //AL.Clear();
                //AL = null;
                // Method Is For Update Max Flag In Table

                Trn_RapSaveProperty PropertySum = new Trn_RapSaveProperty();

                PropertySum.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                PropertySum.KAPANNAME = Val.ToString(txtKapanName.Text);
                PropertySum.PACKETNO = Val.ToInt(txtPacketNo.Text);
                PropertySum.MTAG = txtTag.Enabled == false ? "A" : txtTag.Text;
                PropertySum.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                PropertySum.PRD_ID = IntPrdID;
                PropertySum = ObjRap.SaveMaxAmountFlag(PropertySum);

                PropertySum = null;

                //Add : #P :  02-12-2019 : For TFlag Update :  When Final Issue is done and then Update ISFInal plan
                string str = "";

                //if (Val.ToInt32(IntOldTFlagPlanNo) != 0 && IntOldTFlagPlanNo != IntNewISFinalFlagPlanNo && ((Val.ToInt32(CmbPrdType.Tag) == 2) || Val.ToInt32(CmbPrdType.Tag) == 10))  //Chnged : #P : 06-02-2020 : Manage TFlag When Change IsFinal Plan Or Change Whole Plan With Fetch From "FetchPrevPlan"
                if (Val.ToInt32(IntOldTFlagPlanNo) != 0 && ((IntOldTFlagPlanNo != IntNewISFinalFlagPlanNo) || IntNewTFlagPlanNo == 0) && ((Val.ToInt32(CmbPrdType.Tag) == 2) || Val.ToInt32(CmbPrdType.Tag) == 10))
                {
                    Int64 IntISFinalPrd_ID = IntPrdID;// Val.ToString(DTabPrediction.AsEnumerable().Where(p => p.Field<bool>("ISFINAL") == true).Select(p => p.Field<string>("PRD_ID")).FirstOrDefault());
                    int IntISFinalPlanno = Val.ToInt32((DTabPrediction.AsEnumerable().Where(p => p.Field<bool>("ISFINAL") == true).Select(p => p.Field<int>("PLANNO"))).FirstOrDefault());
                    int IntRes = new BOTRN_PredictionView().UpdatePrdTFlag(IntISFinalPrd_ID, Val.ToInt64(txtEmployee.Tag), IntISFinalPlanno, Val.ToInt32(CmbPrdType.Tag), "RAPCALC");

                    Global.Message("Final Employee Issue Planno Is Change From PlanNo = '" + IntOldTFlagPlanNo + "' To PlanNo = '" + IntNewISFinalFlagPlanNo + "'.");
                    lblTFlagPlanUpdateMessage.Text = "Final Employee PlanNo Successfully Changed From '" + IntOldTFlagPlanNo + "' To '" + IntISFinalPlanno + "'.";
                }
                //End : #P : 02-12-2019


                //#P : 08-12-2020 : Je Prd Update kare 6e Ani Breaking ni Entry thy gy hoy to BrkDetail Update karavani..
                if ((Val.ToInt32(CmbPrdType.Tag) == 2 || Val.ToInt32(CmbPrdType.Tag) == 10 || Val.ToInt32(CmbPrdType.Tag) == 3 || Val.ToInt32(CmbPrdType.Tag) == 4) && IntNewTFlagPlanNo != 0)
                {
                    Trn_RapSaveProperty PropertyForBreaking = new Trn_RapSaveProperty();
                    PropertyForBreaking.KAPANNAME = Val.ToString(txtKapanName.Text);
                    PropertyForBreaking.PACKETNO = Val.ToInt32(txtPacketNo.Text);
                    PropertyForBreaking.MTAG = txtTag.Enabled == false ? "A" : txtTag.Text;
                    PropertyForBreaking.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                    PropertyForBreaking = ObjRap.SaveBrkDetailFromPrdData(PropertyForBreaking);

                    if (PropertyForBreaking.ReturnMessageType == "FAIL")
                    {
                        Global.Message("Selected Stone's Breaking Entry Is Not Updated Properly Please Contact With Your Administrator..");
                    }
                }
                //#End : 08-12-2020


                this.Cursor = Cursors.Default;

                //Global.Message("****  " + Val.ToString(CmbPrdType.SelectedItem) + "   *****\n\nSUCCESSFULLY SAVED OF " + txtKapanName.Text + "/" + txtPacketNo.Text + "/" + txtTag.Text);


                if (Val.ToInt(CmbPrdType.Tag) == 4)
                {
                    if (chkIsSaveBtnPrint.Checked == true)
                    {
                        btnBarcodePrint_Click(sender, e);
                    }
                }

                BtnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }


        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                lblPrdID.Text = string.Empty;

                //#P : 05-02-2020

                if (EmployeeRightsProperty.RAPCHANGEEMPLOYEE)
                {
                    txtEmployee.Enabled = true;
                    BtnEmployee.Enabled = true;
                }
                //End : #P : 05-02-2020


                PanelCarat.Text = "";
                lblTotal.Text = "0";

                mIntOldISFinalFlagPlanNo = 0;

                //#P : 12-10-2020
                PanelCarat.Enabled = false;
                PanelParameter.Enabled = false;
                PanelButtons.Enabled = false;
                //End : #P : 12-10-2020

                PanelCarat.Enabled = true;
                PanelParameter.Enabled = true;
                PanelButtons.Enabled = true;
                IntOldTFlagPlanNo = 0;

                BtnSave.Enabled = false;
                BtnFetchPrevPlan.Enabled = false;
                BtnSave.Text = "&Save";
                lblMessage.Text = string.Empty;

                BtnImport.Enabled = false;

                mStrPktFL_ID = "";

                ISClickOnShowButton = false;

                lblTFlagPlanUpdateMessage.Text = string.Empty;

                DTabPrediction.Rows.Clear();

                DTabMakLog.Rows.Clear();

                AxonContLib.cButton rbShp = PanelShape.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                rbShp.AccessibleName = "true";
                rbShp.ForeColor = mSelectedColor;
                rbShp.BackColor = mSelectedBackColor;

                AxonContLib.cButton rbCol = PanelColor.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                rbCol.AccessibleName = "true";
                rbCol.ForeColor = mSelectedColor;
                rbCol.BackColor = mSelectedBackColor;

                AxonContLib.cButton rbCla = PanelClarity.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                rbCla.AccessibleName = "true";
                rbCla.ForeColor = mSelectedColor;
                rbCla.BackColor = mSelectedBackColor;

                AxonContLib.cButton rbCut = PanelCut.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                rbCut.AccessibleName = "true";
                rbCut.ForeColor = mSelectedColor;
                rbCut.BackColor = mSelectedBackColor;

                AxonContLib.cButton rbPol = PanelPol.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                rbPol.AccessibleName = "true";
                rbPol.ForeColor = mSelectedColor;
                rbPol.BackColor = mSelectedBackColor;

                AxonContLib.cButton rbSym = PanelSym.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                rbSym.AccessibleName = "true";
                rbSym.ForeColor = mSelectedColor;
                rbSym.BackColor = mSelectedBackColor;

                AxonContLib.cButton rbFL = PanelFL.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                rbFL.AccessibleName = "true";
                rbFL.ForeColor = mSelectedColor;
                rbFL.BackColor = mSelectedBackColor;

                txtRemark.Text = string.Empty;

                CmbMilky.SelectedIndex = 0;
                CmbLBLC.SelectedIndex = 0;
                CmbNatts.SelectedIndex = 0;

                CmbBlackInc.SelectedIndex = 0;
                CmbOpenInc.SelectedIndex = 0;
                CmbWhiteInc.SelectedIndex = 0;

                CmbHA.SelectedIndex = 0;
                CmbPavalion.SelectedIndex = 0;
                CmbTension.SelectedIndex = 0;

                CmbSakhat.SelectedIndex = 0;

                CmbColorShade.SelectedIndex = 0;
                CmbLuster.SelectedIndex = 0;

                CmbEyeClean.SelectedIndex = 0;
                CmbNatural.SelectedIndex = 0;
                CmbGrain.SelectedIndex = 0;
                CmbLabProcess.SelectedIndex = 0;
                CmbLabSelection.SelectedIndex = 0;
                txtDiaMax.Text = string.Empty;
                txtDiaMin.Text = string.Empty;
                txtHeight.Text = string.Empty;

                ChkNOBlack.Checked = false;
                ChkNOBGM.Checked = false;

                RbtExp.Checked = true;

                txtKapanName.Text = string.Empty;
                txtKapanName.Tag = string.Empty;

                txtPacketNo.Text = string.Empty;
                txtPacketNo.Tag = string.Empty;

                txtTag.Text = string.Empty;
                txtTag.Tag = string.Empty;

                txtBarcode.Text = string.Empty;

                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;

                //txtEmployee.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
                txtEmployee.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.EMPSHORTNAME;
                txtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;

                txtManager.Text = string.Empty;
                txtManager.Tag = string.Empty;

                lblLot.Text = "0.00";
                lblBalance.Text = "0.00";
                lblBalance.Tag = string.Empty;

                BtnBlankNew.Enabled = true;
                BtnGeneratePlans.Enabled = true;

                CmbRapDate.SelectedIndex = 0;

                ChkDiff.Checked = false;
                ChkAllowForUpdate.Checked = false;

                PanelCarat.Enabled = true;
                PanelParameter.Enabled = true;

                if (GrdDet.RowCount == 0)
                {
                    BtnGeneratePlans.PerformClick();
                }

                //CmbPrdType.Focus();
                if (RbtBarcode.Checked)
                {
                    txtBarcode.Focus();
                }
                else if (RbtPacketNo.Checked)
                {
                    txtKapanName.Focus();
                }
                else if (RbtPktSerialNo.Checked)
                {
                    txtSrNoKapanName.Focus();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        #endregion

        //public void FindRap1()
        //{
        //    try
        //    {
        //        if (GrdDet.RowCount == 0)
        //        {
        //            return;
        //        }
        //        if (GrdDet.FocusedRowHandle < 0)
        //        {
        //            return;
        //        }

        //        this.Cursor = Cursors.WaitCursor;

        //        AxonContLib.cRadioButton rbShp = PanelShape.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
        //        GrdDet.SetFocusedRowCellValue("SHAPE_ID", Val.ToString(rbShp.Tag));
        //        GrdDet.SetFocusedRowCellValue("SHAPECODE", Val.ToString(rbShp.AccessibleDescription));
        //        GrdDet.SetFocusedRowCellValue("SHAPENAME", Val.ToString(rbShp.Text));

        //        AxonContLib.cRadioButton rbCol = PanelColor.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
        //        GrdDet.SetFocusedRowCellValue("COLOR_ID", Val.ToString(rbCol.Tag));
        //        GrdDet.SetFocusedRowCellValue("COLORCODE", Val.ToString(rbCol.AccessibleDescription));
        //        GrdDet.SetFocusedRowCellValue("COLORNAME", Val.ToString(rbCol.Text));

        //        AxonContLib.cRadioButton rbCla = PanelClarity.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
        //        GrdDet.SetFocusedRowCellValue("CLARITY_ID", Val.ToString(rbCla.Tag));
        //        GrdDet.SetFocusedRowCellValue("CLARITYCODE", Val.ToString(rbCla.AccessibleDescription));
        //        GrdDet.SetFocusedRowCellValue("CLARITYNAME", Val.ToString(rbCla.Text));

        //        if (RbtExp.Checked == true)
        //        {
        //            AxonContLib.cRadioButton rbCut = PanelCut.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
        //            GrdDet.SetFocusedRowCellValue("CUT_ID", Val.ToString(rbCut.Tag));
        //            GrdDet.SetFocusedRowCellValue("CUTCODE", Val.ToString(rbCut.AccessibleDescription));
        //            GrdDet.SetFocusedRowCellValue("CUTNAME", Val.ToString(rbCut.Text));

        //            AxonContLib.cRadioButton rbPol = PanelPol.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
        //            GrdDet.SetFocusedRowCellValue("POL_ID", Val.ToString(rbPol.Tag));
        //            GrdDet.SetFocusedRowCellValue("POLCODE", Val.ToString(rbPol.AccessibleDescription));
        //            GrdDet.SetFocusedRowCellValue("POLNAME", Val.ToString(rbPol.Text));

        //            AxonContLib.cRadioButton rbSym = PanelSym.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
        //            GrdDet.SetFocusedRowCellValue("SYM_ID", Val.ToString(rbSym.Tag));
        //            GrdDet.SetFocusedRowCellValue("SYMCODE", Val.ToString(rbSym.AccessibleDescription));
        //            GrdDet.SetFocusedRowCellValue("SYMNAME", Val.ToString(rbSym.Text));    
        //        }
        //        if (RbtGraph.Checked == true)
        //        {
        //            AxonContLib.cRadioButton rbCut = PanelCut.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
        //            GrdDet.SetFocusedRowCellValue("GCUT_ID", Val.ToString(rbCut.Tag));
        //            GrdDet.SetFocusedRowCellValue("GCUTCODE", Val.ToString(rbCut.AccessibleDescription));
        //            GrdDet.SetFocusedRowCellValue("GCUTNAME", Val.ToString(rbCut.Text));

        //            AxonContLib.cRadioButton rbPol = PanelPol.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
        //            GrdDet.SetFocusedRowCellValue("GPOL_ID", Val.ToString(rbPol.Tag));
        //            GrdDet.SetFocusedRowCellValue("GPOLCODE", Val.ToString(rbPol.AccessibleDescription));
        //            GrdDet.SetFocusedRowCellValue("GPOLNAME", Val.ToString(rbPol.Text));

        //            AxonContLib.cRadioButton rbSym = PanelSym.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
        //            GrdDet.SetFocusedRowCellValue("GSYM_ID", Val.ToString(rbSym.Tag));
        //            GrdDet.SetFocusedRowCellValue("GSYMCODE", Val.ToString(rbSym.AccessibleDescription));
        //            GrdDet.SetFocusedRowCellValue("GSYMNAME", Val.ToString(rbSym.Text));    
        //        }

        //        AxonContLib.cRadioButton rbFL = PanelFL.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
        //        GrdDet.SetFocusedRowCellValue("FL_ID", Val.ToString(rbFL.Tag));
        //        GrdDet.SetFocusedRowCellValue("FLCODE", Val.ToString(rbFL.AccessibleDescription));
        //        GrdDet.SetFocusedRowCellValue("FLNAME", Val.ToString(rbFL.Text));

        //        DataRow DRow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

        //        Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

        //        clsFindRap.SHAPECODE = Val.ToString(DRow["SHAPECODE"]);

        //        clsFindRap.COLOR_ID = Val.ToInt32(DRow["COLOR_ID"]);
        //        clsFindRap.COLORCODE = Val.ToString(DRow["COLORCODE"]);

        //        clsFindRap.CLARITY_ID = Val.ToInt32(DRow["CLARITY_ID"]);
        //        clsFindRap.CLARITYCODE = Val.ToString(DRow["CLARITYCODE"]);

        //        if (RbtExp.Checked == true)
        //        {
        //            clsFindRap.CARAT = Val.Val(DRow["CARAT"]);
        //            clsFindRap.CUTCODE = Val.ToString(DRow["CUTCODE"]);
        //            clsFindRap.POLCODE = Val.ToString(DRow["POLCODE"]);
        //            clsFindRap.SYMCODE = Val.ToString(DRow["SYMCODE"]);
        //        }
        //        else if (RbtGraph.Checked == true)
        //        {
        //            clsFindRap.CARAT = Val.Val(DRow["GCARAT"]);
        //            clsFindRap.CUTCODE = Val.ToString(DRow["GCUTCODE"]);
        //            clsFindRap.POLCODE = Val.ToString(DRow["GPOLCODE"]);
        //            clsFindRap.SYMCODE = Val.ToString(DRow["GSYMCODE"]);
        //        }

        //        clsFindRap.FLCODE = Val.ToString(DRow["FLCODE"]);
        //        clsFindRap.MILKYCODE = Val.ToString(DRow["MILKYCODE"]);
        //        clsFindRap.NATTSCODE = Val.ToString(DRow["NATTSCODE"]);
        //        clsFindRap.LBLCCODE = Val.ToString(DRow["LBLCCODE"]);
        //        clsFindRap.RAPDATE = Val.SqlDate(CmbRapDate.SelectedItem.ToString());

        //        clsFindRap.COLORSHADECODE = Val.ToString(DRow["COLORSHADECODE"]);
        //        clsFindRap.OPENINCCODE = Val.ToString(DRow["OPENINCCODE"]);
        //        clsFindRap.BLACKINCCODE = Val.ToString(DRow["BLACKINCCODE"]);
        //        clsFindRap.WHITEINCCODE = Val.ToString(DRow["WHITEINCCODE"]);
        //        clsFindRap.PAVCODE = Val.ToString(DRow["PAVCODE"]);
        //        clsFindRap.EYECLEANCODE = Val.ToString(DRow["EYECLEANCODE"]);
        //        clsFindRap.LUSTERCODE = Val.ToString(DRow["LUSTERCODE"]);
        //        clsFindRap.NATURALCODE = Val.ToString(DRow["NATURALCODE"]);
        //        clsFindRap.GRAINCODE = Val.ToString(DRow["GRAINCODE"]);
        //        clsFindRap.LABCODE = Val.ToString(DRow["LABCODE"]);

        //        if (clsFindRap.SHAPECODE == "" || clsFindRap.COLORCODE == "" || clsFindRap.CLARITYCODE == "" || clsFindRap.CARAT == 0)
        //        {
        //            this.Cursor = Cursors.Default;
        //            return;
        //        }

        //        clsFindRap = ObjRap.FindRap(clsFindRap);

        //        if (RbtExp.Checked == true)
        //        {
        //            GrdDet.SetFocusedRowCellValue("RAPAPORT", clsFindRap.RAPAPORT);
        //            GrdDet.SetFocusedRowCellValue("PRICEPERCARAT", clsFindRap.PRICEPERCARAT);
        //            GrdDet.SetFocusedRowCellValue("AMOUNT", clsFindRap.AMOUNT);
        //            GrdDet.SetFocusedRowCellValue("DISCOUNT", clsFindRap.DISCOUNT);
        //            GrdDet.SetFocusedRowCellValue("AMOUNTDISCOUNT", clsFindRap.AMOUNTDISCOUNT);
        //        }
        //        if (RbtGraph.Checked == true)
        //        {
        //            GrdDet.SetFocusedRowCellValue("GRAPAPORT", clsFindRap.RAPAPORT);
        //            GrdDet.SetFocusedRowCellValue("GPRICEPERCARAT", clsFindRap.PRICEPERCARAT);
        //            GrdDet.SetFocusedRowCellValue("GAMOUNT", clsFindRap.AMOUNT);
        //            GrdDet.SetFocusedRowCellValue("GDISCOUNT", clsFindRap.DISCOUNT);
        //            GrdDet.SetFocusedRowCellValue("GAMOUNTDISCOUNT", clsFindRap.AMOUNTDISCOUNT);
        //        }

        //        DataRow[] UDRow = DTabParameter.Select("ParaType = 'COLOR'");
        //        if (UDRow.Length == 0)
        //        {
        //            return;
        //        }

        //        DataTable DTab = UDRow.CopyToDataTable();
        //        DTab.DefaultView.Sort = "SequenceNo";
        //        DTab = DTab.DefaultView.ToTable();

        //        int pIntMainColorID = clsFindRap.COLOR_ID;
        //        int pIntMainClarityID = clsFindRap.CLARITY_ID;

        //        string pStrMainColorCODE = clsFindRap.COLORCODE;
        //        string pStrMainClarityCODE = clsFindRap.CLARITYCODE;

        //        for (int IntI = 0; IntI < DTab.Rows.Count; IntI++)
        //        {
        //            if (Val.ToInt(DTab.Rows[IntI]["PARA_ID"]) == pIntMainColorID)
        //            {
        //                try
        //                {
        //                    // UpColor
        //                    clsFindRap.COLOR_ID = Val.ToInt(DTab.Rows[IntI - 1]["PARA_ID"]);
        //                    clsFindRap.COLORCODE = Val.ToString(DTab.Rows[IntI - 1]["PARACODE"]);

        //                    clsFindRap.CLARITY_ID = pIntMainClarityID;
        //                    clsFindRap.CLARITYCODE = pStrMainClarityCODE;

        //                    clsFindRap = ObjRap.FindRap(clsFindRap);

        //                    GrdDet.SetFocusedRowCellValue("UPCOLOR_ID", clsFindRap.COLOR_ID);
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORCODE", Val.ToString(DTab.Rows[IntI - 1]["PARACODE"]));
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORNAME", Val.ToString(DTab.Rows[IntI - 1]["PARANAME"]));

        //                    GrdDet.SetFocusedRowCellValue("UPCOLORRAPAPORT", clsFindRap.RAPAPORT);
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORPRICEPERCARAT", clsFindRap.PRICEPERCARAT);
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORAMOUNT", clsFindRap.AMOUNT);
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORDISCOUNT", clsFindRap.DISCOUNT);
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORAMOUNTDISCOUNT", clsFindRap.AMOUNTDISCOUNT);
        //                }
        //                catch
        //                {
        //                    GrdDet.SetFocusedRowCellValue("UPCOLOR_ID", 0);
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORCODE", "");
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORNAME", "");

        //                    GrdDet.SetFocusedRowCellValue("UPCOLORRAPAPORT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORPRICEPERCARAT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORAMOUNT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORDISCOUNT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("UPCOLORAMOUNTDISCOUNT", 0.00);
        //                }

        //                try
        //                {
        //                    // DownColor
        //                    clsFindRap.COLOR_ID = Val.ToInt(DTab.Rows[IntI + 1]["PARA_ID"]);
        //                    clsFindRap.COLORCODE = Val.ToString(DTab.Rows[IntI + 1]["PARACODE"]);

        //                    clsFindRap.CLARITY_ID = pIntMainClarityID;
        //                    clsFindRap.CLARITYCODE = pStrMainClarityCODE;

        //                    clsFindRap = ObjRap.FindRap(clsFindRap);

        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLOR_ID", clsFindRap.COLOR_ID);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORCODE", Val.ToString(DTab.Rows[IntI + 1]["PARACODE"]));
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORNAME", Val.ToString(DTab.Rows[IntI + 1]["PARANAME"]));
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORRAPAPORT", clsFindRap.RAPAPORT);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORPRICEPERCARAT", clsFindRap.PRICEPERCARAT);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORAMOUNT", clsFindRap.AMOUNT);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORDISCOUNT", clsFindRap.DISCOUNT);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORAMOUNTDISCOUNT", clsFindRap.AMOUNTDISCOUNT);

        //                }
        //                catch
        //                {
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLOR_ID", 0);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORCODE", "");
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORNAME", "");
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORRAPAPORT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORPRICEPERCARAT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORAMOUNT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORDISCOUNT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCOLORAMOUNTDISCOUNT", 0.00);
        //                }

        //                break;
        //            }    
        //        }

        //        UDRow = null;
        //        UDRow = DTabParameter.Select("ParaType = 'CLARITY'");
        //        if (UDRow.Length == 0)
        //        {
        //            return;
        //        }

        //        DTab = UDRow.CopyToDataTable();
        //        DTab.DefaultView.Sort = "SequenceNo";
        //        DTab = DTab.DefaultView.ToTable();

        //        for (int IntI = 0; IntI < DTab.Rows.Count; IntI++)
        //        {
        //            if (Val.ToInt(DTab.Rows[IntI]["PARA_ID"]) == pIntMainClarityID)
        //            {

        //                try
        //                {
        //                    // UpClarity
        //                    clsFindRap.COLOR_ID = pIntMainColorID;
        //                    clsFindRap.COLORCODE = pStrMainColorCODE;

        //                    clsFindRap.CLARITY_ID = Val.ToInt(DTab.Rows[IntI - 1]["PARA_ID"]);
        //                    clsFindRap.CLARITYCODE = Val.ToString(DTab.Rows[IntI - 1]["PARACODE"]);

        //                    clsFindRap = ObjRap.FindRap(clsFindRap);

        //                    GrdDet.SetFocusedRowCellValue("UPCLARITY_ID", clsFindRap.CLARITY_ID);
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYCODE", Val.ToString(DTab.Rows[IntI - 1]["PARACODE"]));
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYNAME", Val.ToString(DTab.Rows[IntI - 1]["PARANAME"]));
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYRAPAPORT", clsFindRap.RAPAPORT);
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYPRICEPERCARAT", clsFindRap.PRICEPERCARAT);
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYAMOUNT", clsFindRap.AMOUNT);
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYDISCOUNT", clsFindRap.DISCOUNT);
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYAMOUNTDISCOUNT", clsFindRap.AMOUNTDISCOUNT);

        //                }
        //                catch 
        //                {
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITY_ID", 0);
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYCODE", "");
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYNAME", "");
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYRAPAPORT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYPRICEPERCARAT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYAMOUNT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYDISCOUNT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("UPCLARITYAMOUNTDISCOUNT", 0.00);

        //                }

        //                try
        //                {
        //                    // DownColor
        //                    clsFindRap.COLOR_ID = pIntMainColorID;
        //                    clsFindRap.COLORCODE = pStrMainColorCODE;

        //                    clsFindRap.CLARITY_ID = Val.ToInt(DTab.Rows[IntI + 1]["PARA_ID"]);
        //                    clsFindRap.CLARITYCODE = Val.ToString(DTab.Rows[IntI + 1]["PARACODE"]);

        //                    clsFindRap = ObjRap.FindRap(clsFindRap);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITY_ID", clsFindRap.CLARITY_ID);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYCODE", Val.ToString(DTab.Rows[IntI + 1]["PARACODE"]));
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYNAME", Val.ToString(DTab.Rows[IntI + 1]["PARANAME"]));
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYRAPAPORT", clsFindRap.RAPAPORT);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYPRICEPERCARAT", clsFindRap.PRICEPERCARAT);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYAMOUNT", clsFindRap.AMOUNT);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYDISCOUNT", clsFindRap.DISCOUNT);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYAMOUNTDISCOUNT", clsFindRap.AMOUNTDISCOUNT);

        //                }
        //                catch 
        //                {
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITY_ID", 0);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYCODE", "");
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYNAME", "");
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYRAPAPORT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYPRICEPERCARAT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYAMOUNT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYDISCOUNT", 0.00);
        //                    GrdDet.SetFocusedRowCellValue("DOWNCLARITYAMOUNTDISCOUNT", 0.00);
        //                }

        //                break;
        //            }
        //        }


        //        clsFindRap = null;
        //        DTabPrediction.AcceptChanges();

        //        this.Cursor = Cursors.Default;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Cursor = Cursors.Default;
        //        Global.MessageError(ex.Message);
        //    }
        //    this.Cursor = Cursors.Default;

        //}

        public void ConsiderBGMNonBGM(Int32 IntMilky_ID, Int32 IntColorShade_ID) //Add : Pinali : 01-06-2020
        {
            if (GrdDet.RowCount == 0)
            {
                return;
            }
            if (GrdDet.FocusedRowHandle < 0)
            {
                return;
            }
            if (mStrType == "SHOWCLICK")
            {
                return;
            }
            //Milky -> Blank Or None ANd ColorShade -> Blank,N,White,Yellow,BRN0 Hoy to NoBGM Otherwise BGM
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

        public void FillInclusionDetail()
        {
            DataStructure SelectedMilky = CmbMilky.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("MILKY_ID", SelectedMilky.PARA_ID);
            GrdDet.SetFocusedRowCellValue("MILKYCODE", SelectedMilky.PARACODE);
            GrdDet.SetFocusedRowCellValue("MILKYNAME", SelectedMilky.PARANAME);

            DataStructure SelectedLBLC = CmbLBLC.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("LBLC_ID", SelectedLBLC.PARA_ID);
            GrdDet.SetFocusedRowCellValue("LBLCCODE", SelectedLBLC.PARACODE);
            GrdDet.SetFocusedRowCellValue("LBLCNAME", SelectedLBLC.PARANAME);

            DataStructure SelectedNatts = CmbNatts.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("NATTS_ID", SelectedNatts.PARA_ID);
            GrdDet.SetFocusedRowCellValue("NATTSCODE", SelectedNatts.PARACODE);
            GrdDet.SetFocusedRowCellValue("NATTSNAME", SelectedNatts.PARANAME);

            DataStructure SelectedBlack = CmbBlackInc.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("BLACKINC_ID", SelectedBlack.PARA_ID);
            GrdDet.SetFocusedRowCellValue("BLACKINCCODE", SelectedBlack.PARACODE);
            GrdDet.SetFocusedRowCellValue("BLACKINCNAME", SelectedBlack.PARANAME);

            DataStructure SelectedOpen = CmbOpenInc.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("OPENINC_ID", SelectedOpen.PARA_ID);
            GrdDet.SetFocusedRowCellValue("OPENINCCODE", SelectedOpen.PARACODE);
            GrdDet.SetFocusedRowCellValue("OPENINCNAME", SelectedOpen.PARANAME);

            DataStructure SelectedWhite = CmbWhiteInc.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("WHITEINC_ID", SelectedWhite.PARA_ID);
            GrdDet.SetFocusedRowCellValue("WHITEINCCODE", SelectedWhite.PARACODE);
            GrdDet.SetFocusedRowCellValue("WHITEINCNAME", SelectedWhite.PARANAME);

            DataStructure SelectedHA = CmbHA.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("HA_ID", SelectedHA.PARA_ID);
            GrdDet.SetFocusedRowCellValue("HACODE", SelectedHA.PARACODE);
            GrdDet.SetFocusedRowCellValue("HANAME", SelectedHA.PARANAME);

            DataStructure SelectedPav = CmbPavalion.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("PAV_ID", SelectedPav.PARA_ID);
            GrdDet.SetFocusedRowCellValue("PAVCODE", SelectedPav.PARACODE);
            GrdDet.SetFocusedRowCellValue("PAVNAME", SelectedPav.PARANAME);

            DataStructure SelectedTension = CmbTension.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("TENSION_ID", SelectedTension.PARA_ID);
            GrdDet.SetFocusedRowCellValue("TENSIONCODE", SelectedTension.PARACODE);
            GrdDet.SetFocusedRowCellValue("TENSIONNAME", SelectedTension.PARANAME);

            DataStructure SelectedSakhat = CmbSakhat.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("SAKHAT_ID", SelectedSakhat.PARA_ID);
            GrdDet.SetFocusedRowCellValue("SAKHATCODE", SelectedSakhat.PARACODE);
            GrdDet.SetFocusedRowCellValue("SAKHATNAME", SelectedSakhat.PARANAME);

            DataStructure SelectedCS = CmbColorShade.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("COLORSHADE_ID", SelectedCS.PARA_ID);
            GrdDet.SetFocusedRowCellValue("COLORSHADECODE", SelectedCS.PARACODE);
            GrdDet.SetFocusedRowCellValue("COLORSHADENAME", SelectedCS.PARANAME);

            DataStructure SelectedLuster = CmbLuster.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("LUSTER_ID", SelectedLuster.PARA_ID);
            GrdDet.SetFocusedRowCellValue("LUSTERCODE", SelectedLuster.PARACODE);
            GrdDet.SetFocusedRowCellValue("LUSTERNAME", SelectedLuster.PARANAME);

            DataStructure SelectedEyeClean = CmbEyeClean.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("EYECLEAN_ID", SelectedEyeClean.PARA_ID);
            GrdDet.SetFocusedRowCellValue("EYECLEANCODE", SelectedEyeClean.PARACODE);
            GrdDet.SetFocusedRowCellValue("EYECLEANNAME", SelectedEyeClean.PARANAME);

            DataStructure SelectedNatural = CmbNatural.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("NATURAL_ID", SelectedNatural.PARA_ID);
            GrdDet.SetFocusedRowCellValue("NATURALCODE", SelectedNatural.PARACODE);
            GrdDet.SetFocusedRowCellValue("NATURALNAME", SelectedNatural.PARANAME);

            DataStructure SelectedGrain = CmbGrain.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("GRAIN_ID", SelectedGrain.PARA_ID);
            GrdDet.SetFocusedRowCellValue("GRAINCODE", SelectedGrain.PARACODE);
            GrdDet.SetFocusedRowCellValue("GRAINNAME", SelectedGrain.PARANAME);

            DataStructure SelectedLab = CmbLab.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("LAB_ID", SelectedLab.PARA_ID);
            GrdDet.SetFocusedRowCellValue("LABCODE", SelectedLab.PARACODE);
            GrdDet.SetFocusedRowCellValue("LABNAME", SelectedLab.PARANAME);

            DataStructure SelectedTO = CmbTableOpen.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("TO_ID", SelectedTO.PARA_ID);
            GrdDet.SetFocusedRowCellValue("TOCODE", SelectedTO.PARACODE);
            GrdDet.SetFocusedRowCellValue("TONAME", SelectedTO.PARANAME);

            DataStructure SelectedCO = CmbCrownOpen.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("CO_ID", SelectedCO.PARA_ID);
            GrdDet.SetFocusedRowCellValue("COCODE", SelectedCO.PARACODE);
            GrdDet.SetFocusedRowCellValue("CONAME", SelectedCO.PARANAME);

            DataStructure SelectedPO = CmbPavillionOpen.SelectedItem as DataStructure;
            GrdDet.SetFocusedRowCellValue("PO_ID", SelectedPO.PARA_ID);
            GrdDet.SetFocusedRowCellValue("POCODE", SelectedPO.PARACODE);
            GrdDet.SetFocusedRowCellValue("PONAME", SelectedPO.PARANAME);
        }

        public void FindRap()
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
                if (mStrType == "SHOWCLICK") //Add : Pinali : 11-12-2019
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                GrdDet.PostEditor();

                /*
                AxonContLib.cButton rbShp = PanelShape.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["SHAPE_ID"] =  Val.ToString(rbShp.Tag);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["SHAPECODE"]=  Val.ToString(rbShp.AccessibleDescription);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["SHAPENAME"]=  Val.ToString(rbShp.Text);

                AxonContLib.cButton rbCol = PanelColor.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["COLOR_ID"] =  Val.ToString(rbCol.Tag);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["COLORCODE"] =  Val.ToString(rbCol.AccessibleDescription);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["COLORNAME"] =  Val.ToString(rbCol.Text);

                AxonContLib.cButton rbCla = PanelClarity.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["CLARITY_ID"] =  Val.ToString(rbCla.Tag);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["CLARITYCODE"] =   Val.ToString(rbCla.AccessibleDescription);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["CLARITYNAME"] =  Val.ToString(rbCla.Text);

                if (RbtExp.Checked == true)
                {
                    AxonContLib.cButton rbCut = PanelCut.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["CUT_ID"] =   Val.ToString(rbCut.Tag);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["CUTCODE"] =   Val.ToString(rbCut.AccessibleDescription);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["CUTNAME"] =   Val.ToString(rbCut.Text);

                    AxonContLib.cButton rbPol = PanelPol.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["POL_ID"] =   Val.ToString(rbPol.Tag);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["POLCODE"] =   Val.ToString(rbPol.AccessibleDescription);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["POLNAME"] =   Val.ToString(rbPol.Text);

                    AxonContLib.cButton rbSym = PanelSym.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["SYM_ID"] =   Val.ToString(rbSym.Tag);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["SYMCODE"] =   Val.ToString(rbSym.AccessibleDescription);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["SYMNAME"] =   Val.ToString(rbSym.Text);

                }
                if (RbtGraph.Checked == true)
                {
                    AxonContLib.cButton rbCut = PanelCut.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GCUT_ID"] =   Val.ToString(rbCut.Tag);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GCUTCODE"] =   Val.ToString(rbCut.AccessibleDescription);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GCUTNAME"] =   Val.ToString(rbCut.Text);

                    AxonContLib.cButton rbPol = PanelPol.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GPOL_ID"] =   Val.ToString(rbPol.Tag);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GPOLCODE"] =   Val.ToString(rbPol.AccessibleDescription);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GPOLNAME"] =  Val.ToString(rbPol.Text);

                    AxonContLib.cButton rbSym = PanelSym.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GSYM_ID"] =   Val.ToString(rbSym.Tag);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GSYMCODE"] =   Val.ToString(rbSym.AccessibleDescription);
                    DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GSYMNAME"] =   Val.ToString(rbSym.Text);

                }

                AxonContLib.cButton rbFL = PanelFL.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["FL_ID"] =  Val.ToString(rbFL.Tag);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["FLCODE"] =  Val.ToString(rbFL.AccessibleDescription);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["FLNAME"] =  Val.ToString(rbFL.Text);
                */

                AxonContLib.cButton rbShp = PanelShape.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                GrdDet.SetFocusedRowCellValue("SHAPE_ID", Val.ToString(rbShp.Tag));
                GrdDet.SetFocusedRowCellValue("SHAPECODE", Val.ToString(rbShp.AccessibleDescription));
                GrdDet.SetFocusedRowCellValue("SHAPENAME", Val.ToString(rbShp.Text));

                AxonContLib.cButton rbCol = PanelColor.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                GrdDet.SetFocusedRowCellValue("COLOR_ID", Val.ToString(rbCol.Tag));
                GrdDet.SetFocusedRowCellValue("COLORCODE", Val.ToString(rbCol.AccessibleDescription));
                GrdDet.SetFocusedRowCellValue("COLORNAME", Val.ToString(rbCol.Text));

                AxonContLib.cButton rbCla = PanelClarity.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                GrdDet.SetFocusedRowCellValue("CLARITY_ID", Val.ToString(rbCla.Tag));
                GrdDet.SetFocusedRowCellValue("CLARITYCODE", Val.ToString(rbCla.AccessibleDescription));
                GrdDet.SetFocusedRowCellValue("CLARITYNAME", Val.ToString(rbCla.Text));

                if (RbtExp.Checked == true)
                {
                    AxonContLib.cButton rbCut = PanelCut.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    GrdDet.SetFocusedRowCellValue("CUT_ID", Val.ToString(rbCut.Tag));
                    GrdDet.SetFocusedRowCellValue("CUTCODE", Val.ToString(rbCut.AccessibleDescription));
                    GrdDet.SetFocusedRowCellValue("CUTNAME", Val.ToString(rbCut.Text));

                    AxonContLib.cButton rbPol = PanelPol.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    GrdDet.SetFocusedRowCellValue("POL_ID", Val.ToString(rbPol.Tag));
                    GrdDet.SetFocusedRowCellValue("POLCODE", Val.ToString(rbPol.AccessibleDescription));
                    GrdDet.SetFocusedRowCellValue("POLNAME", Val.ToString(rbPol.Text));

                    AxonContLib.cButton rbSym = PanelSym.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    GrdDet.SetFocusedRowCellValue("SYM_ID", Val.ToString(rbSym.Tag));
                    GrdDet.SetFocusedRowCellValue("SYMCODE", Val.ToString(rbSym.AccessibleDescription));
                    GrdDet.SetFocusedRowCellValue("SYMNAME", Val.ToString(rbSym.Text));
                }
                if (RbtGraph.Checked == true)
                {
                    AxonContLib.cButton rbCut = PanelCut.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    GrdDet.SetFocusedRowCellValue("GCUT_ID", Val.ToString(rbCut.Tag));
                    GrdDet.SetFocusedRowCellValue("GCUTCODE", Val.ToString(rbCut.AccessibleDescription));
                    GrdDet.SetFocusedRowCellValue("GCUTNAME", Val.ToString(rbCut.Text));

                    AxonContLib.cButton rbPol = PanelPol.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    GrdDet.SetFocusedRowCellValue("GPOL_ID", Val.ToString(rbPol.Tag));
                    GrdDet.SetFocusedRowCellValue("GPOLCODE", Val.ToString(rbPol.AccessibleDescription));
                    GrdDet.SetFocusedRowCellValue("GPOLNAME", Val.ToString(rbPol.Text));

                    AxonContLib.cButton rbSym = PanelSym.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                    GrdDet.SetFocusedRowCellValue("GSYM_ID", Val.ToString(rbSym.Tag));
                    GrdDet.SetFocusedRowCellValue("GSYMCODE", Val.ToString(rbSym.AccessibleDescription));
                    GrdDet.SetFocusedRowCellValue("GSYMNAME", Val.ToString(rbSym.Text));
                }

                AxonContLib.cButton rbFL = PanelFL.Controls.OfType<AxonContLib.cButton>().FirstOrDefault(r => r.AccessibleName == "true");
                GrdDet.SetFocusedRowCellValue("FL_ID", Val.ToString(rbFL.Tag));
                GrdDet.SetFocusedRowCellValue("FLCODE", Val.ToString(rbFL.AccessibleDescription));
                GrdDet.SetFocusedRowCellValue("FLNAME", Val.ToString(rbFL.Text));


                FillInclusionDetail();

                DataRow DRow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

                ConsiderBGMNonBGM(Val.ToInt32(DRow["MILKY_ID"]), Val.ToInt32(DRow["COLORSHADE_ID"]));

                Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

                clsFindRap.SHAPE_ID = Val.ToInt32(DRow["SHAPE_ID"]);
                clsFindRap.SHAPECODE = Val.ToString(DRow["SHAPECODE"]);

                clsFindRap.COLOR_ID = Val.ToInt32(DRow["COLOR_ID"]);
                clsFindRap.COLORCODE = Val.ToString(DRow["COLORCODE"]);

                clsFindRap.CLARITY_ID = Val.ToInt32(DRow["CLARITY_ID"]);
                clsFindRap.CLARITYCODE = Val.ToString(DRow["CLARITYCODE"]);

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
                //clsFindRap.GRAINCODE = Val.ToString(DRow["GRAINCODE"]);
                clsFindRap.LABCODE = Val.ToString(DRow["LABCODE"]);
                //clsFindRap.TOCODE = Val.ToString(DRow["TOCODE"]);

                clsFindRap.DIAMIN = Val.Val(txtDiaMin.Text);

                if (clsFindRap.SHAPECODE == "" || clsFindRap.COLORCODE == "" || clsFindRap.CLARITYCODE == "")
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (RbtExp.Checked == true && clsFindRap.CARAT == 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
                else if (RbtGraph.Checked == true && clsFindRap.GCARAT == 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                clsFindRap = ObjRap.FindRapWithUpDown(clsFindRap);


                GrdDet.SetFocusedRowCellValue("RAPAPORT", clsFindRap.RAPAPORT);
                GrdDet.SetFocusedRowCellValue("PRICEPERCARAT", clsFindRap.PRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("AMOUNT", Math.Round(clsFindRap.AMOUNT, 2));
                GrdDet.SetFocusedRowCellValue("DISCOUNT", clsFindRap.DISCOUNT);
                GrdDet.SetFocusedRowCellValue("AMOUNTDISCOUNT", clsFindRap.AMOUNTDISCOUNT);
                GrdDet.SetFocusedRowCellValue("ISMIXRATE", clsFindRap.ISMIXRATE);
                GrdDet.SetFocusedRowCellValue("GIANONGIA", clsFindRap.GIANONGIA);

                GrdDet.SetFocusedRowCellValue("GRAPAPORT", clsFindRap.GRAPAPORT);
                GrdDet.SetFocusedRowCellValue("GPRICEPERCARAT", clsFindRap.GPRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("GAMOUNT", Math.Round(clsFindRap.GAMOUNT, 2));
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

                GrdDet.SetFocusedRowCellValue("UPCOLOR_ID", clsFindRap.UPCOLOR_ID);
                GrdDet.SetFocusedRowCellValue("UPCOLORCODE", clsFindRap.UPCOLORCODE);
                GrdDet.SetFocusedRowCellValue("UPCOLORNAME", clsFindRap.UPCOLORNAME);
                GrdDet.SetFocusedRowCellValue("UPCOLORRAPAPORT", clsFindRap.UPCOLORRAPAPORT);
                GrdDet.SetFocusedRowCellValue("UPCOLORPRICEPERCARAT", clsFindRap.UPCOLORPRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("UPCOLORAMOUNT", Math.Round(clsFindRap.UPCOLORAMOUNT, 2));
                GrdDet.SetFocusedRowCellValue("UPCOLORDISCOUNT", clsFindRap.UPCOLORDISCOUNT);
                GrdDet.SetFocusedRowCellValue("UPCOLORAMOUNTDISCOUNT", clsFindRap.UPCOLORAMOUNTDISCOUNT);

                GrdDet.SetFocusedRowCellValue("DOWNCOLOR_ID", clsFindRap.DOWNCOLOR_ID);
                GrdDet.SetFocusedRowCellValue("DOWNCOLORCODE", clsFindRap.DOWNCOLORCODE);
                GrdDet.SetFocusedRowCellValue("DOWNCOLORNAME", clsFindRap.DOWNCOLORNAME);
                GrdDet.SetFocusedRowCellValue("DOWNCOLORRAPAPORT", clsFindRap.DOWNCOLORRAPAPORT);
                GrdDet.SetFocusedRowCellValue("DOWNCOLORPRICEPERCARAT", clsFindRap.DOWNCOLORPRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("DOWNCOLORAMOUNT", Math.Round(clsFindRap.DOWNCOLORAMOUNT, 2));
                GrdDet.SetFocusedRowCellValue("DOWNCOLORDISCOUNT", clsFindRap.DOWNCOLORDISCOUNT);
                GrdDet.SetFocusedRowCellValue("DOWNCOLORAMOUNTDISCOUNT", clsFindRap.DOWNCOLORAMOUNTDISCOUNT);

                GrdDet.SetFocusedRowCellValue("UPCLARITY_ID", clsFindRap.UPCLARITY_ID);
                GrdDet.SetFocusedRowCellValue("UPCLARITYCODE", clsFindRap.UPCLARITYCODE);
                GrdDet.SetFocusedRowCellValue("UPCLARITYNAME", clsFindRap.UPCLARITYNAME);
                GrdDet.SetFocusedRowCellValue("UPCLARITYRAPAPORT", clsFindRap.UPCLARITYRAPAPORT);
                GrdDet.SetFocusedRowCellValue("UPCLARITYPRICEPERCARAT", clsFindRap.UPCLARITYPRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("UPCLARITYAMOUNT", Math.Round(clsFindRap.UPCLARITYAMOUNT, 2));
                GrdDet.SetFocusedRowCellValue("UPCLARITYDISCOUNT", clsFindRap.UPCLARITYDISCOUNT);
                GrdDet.SetFocusedRowCellValue("UPCLARITYAMOUNTDISCOUNT", clsFindRap.UPCLARITYAMOUNTDISCOUNT);

                GrdDet.SetFocusedRowCellValue("DOWNCLARITY_ID", clsFindRap.DOWNCLARITY_ID);
                GrdDet.SetFocusedRowCellValue("DOWNCLARITYCODE", clsFindRap.DOWNCLARITYCODE);
                GrdDet.SetFocusedRowCellValue("DOWNCLARITYNAME", clsFindRap.DOWNCLARITYNAME);
                GrdDet.SetFocusedRowCellValue("DOWNCLARITYRAPAPORT", clsFindRap.DOWNCLARITYRAPAPORT);
                GrdDet.SetFocusedRowCellValue("DOWNCLARITYPRICEPERCARAT", clsFindRap.DOWNCLARITYPRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("DOWNCLARITYAMOUNT", Math.Round(clsFindRap.DOWNCLARITYAMOUNT, 2));
                GrdDet.SetFocusedRowCellValue("DOWNCLARITYDISCOUNT", clsFindRap.DOWNCLARITYDISCOUNT);
                GrdDet.SetFocusedRowCellValue("DOWNCLARITYAMOUNTDISCOUNT", clsFindRap.DOWNCLARITYAMOUNTDISCOUNT);

                GrdDet.SetFocusedRowCellValue("DROWDISREGULAR", clsFindRap.DRowDisRegularXML);
                /*
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["RAPAPORT"] = clsFindRap.RAPAPORT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["PRICEPERCARAT"] = clsFindRap.PRICEPERCARAT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["AMOUNT"] = Math.Round(clsFindRap.AMOUNT, 0);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DISCOUNT"] = clsFindRap.DISCOUNT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["AMOUNTDISCOUNT"] = clsFindRap.AMOUNTDISCOUNT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["ISMIXRATE"] = clsFindRap.ISMIXRATE;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GIANONGIA"] = clsFindRap.GIANONGIA;

                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GRAPAPORT"] = clsFindRap.GRAPAPORT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GPRICEPERCARAT"] = clsFindRap.GPRICEPERCARAT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GAMOUNT"] = Math.Round(clsFindRap.GAMOUNT, 0);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GDISCOUNT"] = clsFindRap.GDISCOUNT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["GAMOUNTDISCOUNT"] = clsFindRap.GAMOUNTDISCOUNT;

                //Add : Pinali : 07-09-2019
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["MDISCOUNT"] = clsFindRap.MDISCOUNT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["MPRICEPERCARAT"] = clsFindRap.MPRICEPERCARAT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["MAMOUNT"] = clsFindRap.MAMOUNT;

                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["MGDISCOUNT"] = clsFindRap.MGDISCOUNT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["MGPRICEPERCARAT"] = clsFindRap.MGPRICEPERCARAT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["MGAMOUNT"] = clsFindRap.MGAMOUNT;
                //End : Pinali : 07-09-2019

                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCOLOR_ID"] = clsFindRap.UPCOLOR_ID;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCOLORCODE"] = clsFindRap.UPCOLORCODE;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCOLORNAME"] = clsFindRap.UPCOLORNAME;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCOLORRAPAPORT"] = clsFindRap.UPCOLORRAPAPORT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCOLORPRICEPERCARAT"] = clsFindRap.UPCOLORPRICEPERCARAT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCOLORAMOUNT"] = Math.Round(clsFindRap.UPCOLORAMOUNT, 0);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCOLORDISCOUNT"] = clsFindRap.UPCOLORDISCOUNT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCOLORAMOUNTDISCOUNT"] = clsFindRap.UPCOLORAMOUNTDISCOUNT;

                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCOLOR_ID"] = clsFindRap.DOWNCOLOR_ID;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCOLORCODE"] = clsFindRap.DOWNCOLORCODE;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCOLORNAME"] = clsFindRap.DOWNCOLORNAME;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCOLORRAPAPORT"] = clsFindRap.DOWNCOLORRAPAPORT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCOLORPRICEPERCARAT"] = clsFindRap.DOWNCOLORPRICEPERCARAT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCOLORAMOUNT"] = Math.Round(clsFindRap.DOWNCOLORAMOUNT, 0);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCOLORDISCOUNT"] = clsFindRap.DOWNCOLORDISCOUNT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCOLORAMOUNTDISCOUNT"] = clsFindRap.DOWNCOLORAMOUNTDISCOUNT;

                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCLARITY_ID"] = clsFindRap.UPCLARITY_ID;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCLARITYCODE"] = clsFindRap.UPCLARITYCODE;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCLARITYNAME"] = clsFindRap.UPCLARITYNAME;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCLARITYRAPAPORT"] = clsFindRap.UPCLARITYRAPAPORT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCLARITYPRICEPERCARAT"] = clsFindRap.UPCLARITYPRICEPERCARAT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCLARITYAMOUNT"] = Math.Round(clsFindRap.UPCLARITYAMOUNT, 0);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCLARITYDISCOUNT"] = clsFindRap.UPCLARITYDISCOUNT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["UPCLARITYAMOUNTDISCOUNT"] = clsFindRap.UPCLARITYAMOUNTDISCOUNT;

                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCLARITY_ID"] = clsFindRap.DOWNCLARITY_ID;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCLARITYCODE"] = clsFindRap.DOWNCLARITYCODE;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCLARITYNAME"] = clsFindRap.DOWNCLARITYNAME;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCLARITYRAPAPORT"] = clsFindRap.DOWNCLARITYRAPAPORT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCLARITYPRICEPERCARAT"] = clsFindRap.DOWNCLARITYPRICEPERCARAT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCLARITYAMOUNT"] = Math.Round(clsFindRap.DOWNCLARITYAMOUNT, 0);
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCLARITYDISCOUNT"] = clsFindRap.DOWNCLARITYDISCOUNT;
                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DOWNCLARITYAMOUNTDISCOUNT"] = clsFindRap.DOWNCLARITYAMOUNTDISCOUNT;

                DTabPrediction.Rows[GrdDet.FocusedRowHandle]["DROWDISREGULAR"] = clsFindRap.DRowDisRegularXML;
                */

                clsFindRap = null;

                FindDiffAmountWithFirstRow();

                DTabPrediction.AcceptChanges();

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

        public void FindDiffAmountWithFirstRow()
        {
            double DouFirstRowDollar = 0;
            double DouDiffAmount = 0;
            DTabPrediction.AcceptChanges();
            double DouSelectedTotal = 0;
            for (int IntI = 0; IntI < DTabPrediction.Rows.Count; IntI++)
            {
                DataRow DR = DTabPrediction.Rows[IntI];
                if (IntI == 0)
                {
                    DouFirstRowDollar = Val.Val(DR["AMOUNT"]);
                }
                else
                {
                    DouDiffAmount = Math.Round(Val.Val(DR["AMOUNT"]) - DouFirstRowDollar, 2);
                }

                if (DouDiffAmount == 0)
                {
                    DR["DIFFAMOUNT"] = 0;
                }
                else if (DouDiffAmount > 0)
                {
                    DR["DIFFAMOUNT"] = "+" + DouDiffAmount.ToString();
                }
                else if (DouDiffAmount < 0)
                {
                    DR["DIFFAMOUNT"] = DouDiffAmount.ToString();
                }

                if (Val.ToBoolean(DR["CHECKFORSUM"]) == true)
                {
                    DouSelectedTotal += Val.Val(DR["AMOUNT"]);
                }
            }
            lblTotal.Text = Math.Round(DouSelectedTotal, 0).ToString();

        }


        public Int64 SaveDiscussPrintData()
        {
            Int64 IntSlipNo = 0;
            try
            {
                int IntIndex = 0;


                this.Cursor = Cursors.WaitCursor;

                Int64 IntPrdID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.PRD_ID);

                //ArrayList AL = new ArrayList();
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);

                    if (DRow == null || (Val.Val(DRow["AMOUNT"]) == 0 && Val.Val(DRow["CARAT"]) == 0))
                    {
                        continue;
                    }

                    IntIndex++;
                    Trn_RapSaveProperty Property = new Trn_RapSaveProperty();

                    Property.PACKET_ID = 0;
                    Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                    Property.PRDTYPE = Val.ToString(CmbPrdType.SelectedItem);
                    Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                    Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                    Property.MTAG = txtTag.Enabled == false ? "A" : txtTag.Text;
                    Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                    Property.MANAGER_ID = Val.ToInt64(txtManager.Tag);

                    Property.ID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.ID);
                    Property.SLIPNO = IntSlipNo;

                    Property.PRD_ID = IntPrdID;

                    Property.TAGSRNO = Val.ToInt(DRow["TAGSRNO"]);
                    Property.TAG = Val.ToString(DRow["TAG"]);
                    Property.PLANNO = Val.ToInt(DRow["PLANNO"]);

                    Property.SHAPE_ID = Val.ToInt(DRow["SHAPE_ID"]);
                    Property.CLARITY_ID = Val.ToInt(DRow["CLARITY_ID"]);
                    Property.COLOR_ID = Val.ToInt(DRow["COLOR_ID"]);
                    Property.COLORSHADE_ID = Val.ToInt(DRow["COLORSHADE_ID"]);
                    Property.CUT_ID = Val.ToInt(DRow["CUT_ID"]);
                    Property.POL_ID = Val.ToInt(DRow["POL_ID"]);
                    Property.SYM_ID = Val.ToInt(DRow["SYM_ID"]);
                    Property.FL_ID = Val.ToInt(DRow["FL_ID"]);
                    Property.MILKY_ID = Val.ToInt(DRow["MILKY_ID"]);
                    Property.LBLC_ID = Val.ToInt(DRow["LBLC_ID"]);
                    Property.NATTS_ID = Val.ToInt(DRow["NATTS_ID"]);
                    Property.TENSION_ID = Val.ToInt(DRow["TENSION_ID"]);
                    Property.BLACKINC_ID = Val.ToInt(DRow["BLACKINC_ID"]);
                    Property.OPENINC_ID = Val.ToInt(DRow["OPENINC_ID"]);
                    Property.WHITEINC_ID = Val.ToInt(DRow["WHITEINC_ID"]);
                    Property.LUSTER_ID = Val.ToInt(DRow["LUSTER_ID"]);
                    Property.HA_ID = Val.ToInt(DRow["HA_ID"]);
                    Property.PAV_ID = Val.ToInt(DRow["PAV_ID"]);
                    Property.EYECLEAN_ID = Val.ToInt(DRow["EYECLEAN_ID"]);
                    Property.NATURAL_ID = Val.ToInt(DRow["NATURAL_ID"]);
                    Property.GRAIN_ID = Val.ToInt(DRow["GRAIN_ID"]);

                    Property.CARAT = Val.Val(DRow["CARAT"]);
                    Property.DISCOUNT = Val.Val(DRow["DISCOUNT"]);
                    Property.AMOUNTDISCOUNT = Val.Val(DRow["AMOUNTDISCOUNT"]);
                    Property.RAPAPORT = Val.Val(DRow["RAPAPORT"]);
                    Property.PRICEPERCARAT = Val.Val(DRow["PRICEPERCARAT"]);
                    Property.AMOUNT = Val.Val(DRow["AMOUNT"]);
                    Property.RAPDATE = Val.SqlDate(Val.ToString(DRow["RAPDATE"]));

                    Property.GCARAT = Val.Val(DRow["GCARAT"]);
                    Property.GCOLOR_ID = Val.ToInt(DRow["GCOLOR_ID"]);
                    Property.GCLARITY_ID = Val.ToInt(DRow["GCLARITY_ID"]);
                    Property.GCUT_ID = Val.ToInt(DRow["GCUT_ID"]);
                    Property.GPOL_ID = Val.ToInt(DRow["GPOL_ID"]);
                    Property.GSYM_ID = Val.ToInt(DRow["GSYM_ID"]);

                    Property.GDISCOUNT = Val.Val(DRow["GDISCOUNT"]);
                    Property.GAMOUNTDISCOUNT = Val.Val(DRow["GAMOUNTDISCOUNT"]);
                    Property.GRAPAPORT = Val.Val(DRow["GRAPAPORT"]);
                    Property.GPRICEPERCARAT = Val.Val(DRow["GPRICEPERCARAT"]);
                    Property.GAMOUNT = Val.Val(DRow["GAMOUNT"]);


                    if (txtTag.Enabled == true)
                    {
                        Property.ISFINAL = true;
                    }
                    else
                    {
                        Property.ISFINAL = Val.ToBoolean(DRow["ISFINAL"]);
                    }

                    if (Val.ISDate(Val.ToString(DRow["ENTRYDATE"])))
                    {
                        Property.ENTRYDATE = DateTime.Parse(Val.ToString(DRow["ENTRYDATE"])).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        Property.ENTRYDATE = null;
                    }


                    Property.LAB_ID = Val.ToInt(DRow["LAB_ID"]);

                    Property.UPCOLOR_ID = Val.ToInt(DRow["UPCOLOR_ID"]);
                    Property.UPCOLORDISCOUNT = Val.Val(DRow["UPCOLORDISCOUNT"]);
                    Property.UPCOLORAMOUNTDISCOUNT = Val.Val(DRow["UPCOLORAMOUNTDISCOUNT"]);
                    Property.UPCOLORRAPAPORT = Val.Val(DRow["UPCOLORRAPAPORT"]);
                    Property.UPCOLORPRICEPERCARAT = Val.Val(DRow["UPCOLORPRICEPERCARAT"]);
                    Property.UPCOLORAMOUNT = Val.Val(DRow["UPCOLORAMOUNT"]);

                    Property.DOWNCOLOR_ID = Val.ToInt(DRow["DOWNCOLOR_ID"]);
                    Property.DOWNCOLORDISCOUNT = Val.Val(DRow["DOWNCOLORDISCOUNT"]);
                    Property.DOWNCOLORAMOUNTDISCOUNT = Val.Val(DRow["DOWNCOLORAMOUNTDISCOUNT"]);
                    Property.DOWNCOLORRAPAPORT = Val.Val(DRow["DOWNCOLORRAPAPORT"]);
                    Property.DOWNCOLORPRICEPERCARAT = Val.Val(DRow["DOWNCOLORPRICEPERCARAT"]);
                    Property.DOWNCOLORAMOUNT = Val.Val(DRow["DOWNCOLORAMOUNT"]);

                    Property.UPCLARITY_ID = Val.ToInt(DRow["UPCLARITY_ID"]);
                    Property.UPCLARITYDISCOUNT = Val.Val(DRow["UPCLARITYDISCOUNT"]);
                    Property.UPCLARITYAMOUNTDISCOUNT = Val.Val(DRow["UPCLARITYAMOUNTDISCOUNT"]);
                    Property.UPCLARITYRAPAPORT = Val.Val(DRow["UPCLARITYRAPAPORT"]);
                    Property.UPCLARITYPRICEPERCARAT = Val.Val(DRow["UPCLARITYPRICEPERCARAT"]);
                    Property.UPCLARITYAMOUNT = Val.Val(DRow["UPCLARITYAMOUNT"]);

                    Property.DOWNCLARITY_ID = Val.ToInt(DRow["DOWNCLARITY_ID"]);
                    Property.DOWNCLARITYDISCOUNT = Val.Val(DRow["DOWNCLARITYDISCOUNT"]);
                    Property.DOWNCLARITYAMOUNTDISCOUNT = Val.Val(DRow["DOWNCLARITYAMOUNTDISCOUNT"]);
                    Property.DOWNCLARITYRAPAPORT = Val.Val(DRow["DOWNCLARITYRAPAPORT"]);
                    Property.DOWNCLARITYPRICEPERCARAT = Val.Val(DRow["DOWNCLARITYPRICEPERCARAT"]);
                    Property.DOWNCLARITYAMOUNT = Val.Val(DRow["DOWNCLARITYAMOUNT"]);

                    Property.ISDIFF = Val.ToBoolean(DRow["ISDIFF"]);
                    Property.REMARK = txtRemark.Text;

                    Property.LABPROCESS = Val.ToString(CmbLabProcess.SelectedItem);
                    Property.LABSELECTION = Val.ToString(CmbLabSelection.SelectedItem);
                    Property.DIAMIN = Val.Val(DRow["DIAMIN"]);
                    Property.DIAMAX = Val.Val(DRow["DIAMAX"]);
                    Property.HEIGHT = Val.Val(DRow["HEIGHT"]);
                    Property.ISMIXRATE = Val.ToBoolean(DRow["ISMIXRATE"]);

                    //AL.Add(Property);
                    Property = ObjRap.SaveDPrint(Property);

                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        IntSlipNo = Val.ToInt64(Property.ReturnValue);
                    }

                    Property = null;
                }

                this.Cursor = Cursors.Default;

                Global.Message("Discuss Record Saved Successfully, Your Slip No : " + IntSlipNo);
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(EX.Message);
            }

            return IntSlipNo;
        }

        private void BtnDiscussPrint_Click(object sender, EventArgs e)
        {
            try
            {

                Int64 IntSlipNo = SaveDiscussPrintData();

                this.Cursor = Cursors.WaitCursor;

                if (DTabPrediction.Rows.Count != 0)
                {
                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowForm("PredictionPrintGroupDollarPrint", DollarPrint(IntSlipNo));
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    Global.MessageError("No Record Found For Save");
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }

        public void Link_CreateMarginalHeaderAreaSummary(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("PREDICTION PRINT GROUP DOLLAR PRINT", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 250, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 250, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        public void Link_CreateMarginalFooterAreaSummary(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("verdana", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            // this.Cursor = Cursors.WaitCursor;

            //if (Val.ToString(txtTag.Tag) != "")
            //{
            //    DataTable DTab = ObjRap.GetPredictionDataForPrint(Val.ToInt32(CmbPrdType.Tag), Val.ToString(txtTag.Tag), Val.ToInt64(txtEmployee.Tag), Val.ToInt64(txtManager.Tag));

            //    if (DTab.Rows.Count == 0)
            //    {
            //        if (DTabPrediction.Rows.Count != 0)
            //        {
            //            Int64 IntSlipNo = SaveDiscussPrintData();

            //            Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
            //            FrmReportViewer.MdiParent = Global.gMainRef;
            //            FrmReportViewer.ShowForm("PredictionPrintGroupDollarPrint", DollarPrint(IntSlipNo));
            //            this.Cursor = Cursors.Default;
            //        }
            //        else
            //        {
            //            Global.MessageError("No Record Found For Save");
            //        }
            //    }
            //    else
            //    {
            //        Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
            //        FrmReportViewer.MdiParent = Global.gMainRef;
            //        FrmReportViewer.ShowForm("PredictionPrintGroup", DTab);
            //        this.Cursor = Cursors.Default;
            //    }
            //}
            //else
            //{
            //    if (DTabPrediction.Rows.Count != 0)
            //    {
            //        Int64 IntSlipNo = SaveDiscussPrintData();

            //        Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
            //        FrmReportViewer.MdiParent = Global.gMainRef;
            //        FrmReportViewer.ShowForm("PredictionPrintGroupDollarPrint", DollarPrint(IntSlipNo));
            //        this.Cursor = Cursors.Default;
            //    }
            //    else
            //    {
            //        Global.MessageError("No Record Found For Save");
            //    }
            //}

            try
            {
                if (DTabPrediction.Rows.Count != 0)
                {

                    DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                    PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                    string Str = txtEmployee.Text.Trim().Length == 0 ? "Prediction" : txtEmployee.Text;

                    link.Component = MainGrid;
                    link.Landscape = true;

                    link.Margins.Left = 10;
                    link.Margins.Right = 10;
                    link.Margins.Bottom = 40;
                    link.Margins.Top = 130;

                    link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary);
                    link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterAreaSummary);
                    link.CreateDocument();
                    link.ShowPreview();
                    link.PrintDlg();
                }
                else
                {
                    Global.Message("You Have Not Any Data For Print");
                }

            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }



        }


        public DataTable DollarPrint(Int64 pIntSlipNo)
        {
            DataTable DTab = new DataTable();

            DTab.Columns.Add(new DataColumn("SLIPNO", typeof(Int64)));
            DTab.Columns.Add(new DataColumn("USERNAME", typeof(string)));
            DTab.Columns.Add(new DataColumn("ID", typeof(Int64)));
            DTab.Columns.Add(new DataColumn("PRD_ID", typeof(Int64)));
            DTab.Columns.Add(new DataColumn("PACKET_ID", typeof(Int64)));
            DTab.Columns.Add(new DataColumn("PRDTYPE_ID", typeof(int)));
            DTab.Columns.Add(new DataColumn("PRDTYPE", typeof(string)));
            DTab.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DTab.Columns.Add(new DataColumn("PLANNO", typeof(int)));
            DTab.Columns.Add(new DataColumn("PACKETNO", typeof(int)));
            DTab.Columns.Add(new DataColumn("TAGSRNO", typeof(int)));
            DTab.Columns.Add(new DataColumn("TAG", typeof(string)));

            DTab.Columns.Add(new DataColumn("COMPANY_ID", typeof(Int64)));
            DTab.Columns.Add(new DataColumn("COMPANYNAME", typeof(string)));

            DTab.Columns.Add(new DataColumn("MAINKAPAN", typeof(string)));
            DTab.Columns.Add(new DataColumn("MAINPACKETNO", typeof(int)));
            DTab.Columns.Add(new DataColumn("MAINTAG", typeof(string)));

            DTab.Columns.Add(new DataColumn("KAPANPACKETTAG", typeof(string)));
            DTab.Columns.Add(new DataColumn("LOTCARAT", typeof(double)));
            DTab.Columns.Add(new DataColumn("BALANCECARAT", typeof(double)));

            DTab.Columns.Add(new DataColumn("EMPLOYEE_ID", typeof(Int64)));
            DTab.Columns.Add(new DataColumn("EMPLOYEECODE", typeof(string)));
            DTab.Columns.Add(new DataColumn("EMPLOYEENAME", typeof(string)));
            DTab.Columns.Add(new DataColumn("MANAGER_ID", typeof(Int64)));
            DTab.Columns.Add(new DataColumn("MANAGERNAME", typeof(string)));

            DTab.Columns.Add(new DataColumn("Shp", typeof(string)));
            DTab.Columns.Add(new DataColumn("Cla", typeof(string)));
            DTab.Columns.Add(new DataColumn("Col", typeof(string)));
            DTab.Columns.Add(new DataColumn("CS", typeof(string)));
            DTab.Columns.Add(new DataColumn("Cut", typeof(string)));
            DTab.Columns.Add(new DataColumn("Pol", typeof(string)));
            DTab.Columns.Add(new DataColumn("Sym", typeof(string)));
            DTab.Columns.Add(new DataColumn("FL", typeof(string)));
            DTab.Columns.Add(new DataColumn("Milky", typeof(string)));
            DTab.Columns.Add(new DataColumn("LBLC", typeof(string)));
            DTab.Columns.Add(new DataColumn("Natts", typeof(string)));
            DTab.Columns.Add(new DataColumn("Tension", typeof(string)));
            DTab.Columns.Add(new DataColumn("BInc", typeof(string)));
            DTab.Columns.Add(new DataColumn("OInc", typeof(string)));
            DTab.Columns.Add(new DataColumn("WInc", typeof(string)));
            DTab.Columns.Add(new DataColumn("HA", typeof(string)));
            DTab.Columns.Add(new DataColumn("Pav", typeof(string)));
            DTab.Columns.Add(new DataColumn("EC", typeof(string)));
            DTab.Columns.Add(new DataColumn("Natural", typeof(string)));
            DTab.Columns.Add(new DataColumn("Lust", typeof(string)));
            DTab.Columns.Add(new DataColumn("Grain", typeof(string)));

            DTab.Columns.Add(new DataColumn("Cts", typeof(double)));
            DTab.Columns.Add(new DataColumn("Dis", typeof(double)));
            DTab.Columns.Add(new DataColumn("AmtDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("Rap", typeof(double)));
            DTab.Columns.Add(new DataColumn("Rate", typeof(double)));
            DTab.Columns.Add(new DataColumn("Amt", typeof(double)));
            DTab.Columns.Add(new DataColumn("DiffAmt", typeof(double)));
            DTab.Columns.Add(new DataColumn("RapDate", typeof(string)));

            DTab.Columns.Add(new DataColumn("GCts", typeof(double)));

            DTab.Columns.Add(new DataColumn("GCol", typeof(string)));
            DTab.Columns.Add(new DataColumn("GCla", typeof(string)));
            DTab.Columns.Add(new DataColumn("GCut", typeof(string)));
            DTab.Columns.Add(new DataColumn("GPol", typeof(string)));
            DTab.Columns.Add(new DataColumn("GSym", typeof(string)));

            DTab.Columns.Add(new DataColumn("ENTRYDATE", typeof(string)));

            DTab.Columns.Add(new DataColumn("ISFINAL", typeof(string)));

            DTab.Columns.Add(new DataColumn("GDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("GAmtDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("GRap", typeof(double)));
            DTab.Columns.Add(new DataColumn("GRate", typeof(double)));
            DTab.Columns.Add(new DataColumn("GAmt", typeof(double)));

            DTab.Columns.Add(new DataColumn("Lab", typeof(string)));

            DTab.Columns.Add(new DataColumn("UPCol", typeof(string)));
            DTab.Columns.Add(new DataColumn("UPColDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("UPColAmtDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("UPColRap", typeof(double)));
            DTab.Columns.Add(new DataColumn("UPColRate", typeof(double)));
            DTab.Columns.Add(new DataColumn("UPColAmt", typeof(double)));
            DTab.Columns.Add(new DataColumn("UPColAmtDiff", typeof(double)));

            DTab.Columns.Add(new DataColumn("DWCol", typeof(string)));
            DTab.Columns.Add(new DataColumn("DWDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("DWColAmtDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("DWColRap", typeof(double)));
            DTab.Columns.Add(new DataColumn("DWColRate", typeof(double)));
            DTab.Columns.Add(new DataColumn("DWColAmt", typeof(double)));
            DTab.Columns.Add(new DataColumn("DWColAmtDiff", typeof(double)));

            DTab.Columns.Add(new DataColumn("UPCla", typeof(string)));
            DTab.Columns.Add(new DataColumn("UPClaDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("UPClaAmtDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("UPClaRap", typeof(double)));
            DTab.Columns.Add(new DataColumn("UPClaRate", typeof(double)));
            DTab.Columns.Add(new DataColumn("UPClaAmt", typeof(double)));
            DTab.Columns.Add(new DataColumn("UPClaAmtDiff", typeof(double)));

            DTab.Columns.Add(new DataColumn("DWCla", typeof(string)));
            DTab.Columns.Add(new DataColumn("DWClaDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("DWClaAmtDis", typeof(double)));
            DTab.Columns.Add(new DataColumn("DWClaRap", typeof(double)));
            DTab.Columns.Add(new DataColumn("DWClaRate", typeof(double)));
            DTab.Columns.Add(new DataColumn("DWClaAmt", typeof(double)));
            DTab.Columns.Add(new DataColumn("DWClaAmtDiff", typeof(double)));

            int Int = 0;

            double DouAmt = 0;
            double DouColUpAmt = 0;
            double DouColDWAmt = 0;
            double DouClaUpAmt = 0;
            double DouClaDwAmt = 0;


            foreach (DataRow DRow in DTabPrediction.Rows)
            {

                DataRow DRNew = DTab.NewRow();

                DRNew["USERNAME"] = BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME;
                DRNew["ID"] = 0;
                DRNew["PRD_ID"] = 0;
                DRNew["PACKET_ID"] = 0;
                DRNew["SLIPNO"] = pIntSlipNo;
                DRNew["PRDTYPE_ID"] = Val.ToInt(CmbPrdType.Tag);
                DRNew["PRDTYPE"] = Val.ToString(CmbPrdType.SelectedItem);
                DRNew["KAPANNAME"] = txtKapanName.Text;
                DRNew["PLANNO"] = Val.ToInt(DRow["PLANNO"]);
                DRNew["PACKETNO"] = Val.ToInt(txtPacketNo.Text);
                DRNew["TAGSRNO"] = Val.ToInt(DRow["TAGSRNO"]);
                DRNew["TAG"] = Val.ToString(DRow["TAG"]);

                DRNew["COMPANY_ID"] = BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANY_ID;
                DRNew["COMPANYNAME"] = BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME;

                DRNew["MAINKAPAN"] = txtKapanName.Text;
                DRNew["MAINPACKETNO"] = Val.ToInt(txtPacketNo.Text);
                DRNew["MAINTAG"] = txtTag.Text;

                DRNew["KAPANPACKETTAG"] = txtTag.Text;
                DRNew["LOTCARAT"] = lblLot.Text;
                DRNew["BALANCECARAT"] = lblBalance.Text;

                DRNew["EMPLOYEE_ID"] = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                DRNew["EMPLOYEECODE"] = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
                DRNew["EMPLOYEENAME"] = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME;
                DRNew["MANAGER_ID"] = 0;
                DRNew["MANAGERNAME"] = "";

                DRNew["Shp"] = Val.ToString(DRow["SHAPECODE"]);
                DRNew["Cla"] = Val.ToString(DRow["CLARITYNAME"]);
                DRNew["Col"] = Val.ToString(DRow["COLORNAME"]);
                DRNew["CS"] = Val.ToString(DRow["COLORSHADECODE"]);
                DRNew["Cut"] = Val.ToString(DRow["CUTCODE"]);
                DRNew["Pol"] = Val.ToString(DRow["POLCODE"]);
                DRNew["Sym"] = Val.ToString(DRow["SYMCODE"]);
                DRNew["FL"] = Val.ToString(DRow["FLCODE"]);
                DRNew["Milky"] = Val.ToString(DRow["MILKYCODE"]);
                DRNew["LBLC"] = Val.ToString(DRow["LBLCCODE"]);
                DRNew["Natts"] = Val.ToString(DRow["NATTSCODE"]);
                DRNew["Tension"] = Val.ToString(DRow["TENSIONCODE"]);
                DRNew["BInc"] = Val.ToString(DRow["BLACKINCCODE"]);
                DRNew["OInc"] = Val.ToString(DRow["OPENINCCODE"]);
                DRNew["WInc"] = Val.ToString(DRow["WHITEINCCODE"]);
                DRNew["HA"] = Val.ToString(DRow["HACODE"]);
                DRNew["Pav"] = Val.ToString(DRow["PAVCODE"]);
                DRNew["EC"] = Val.ToString(DRow["EYECLEANCODE"]);
                DRNew["Natural"] = Val.ToString(DRow["NATURALCODE"]);
                DRNew["Lust"] = Val.ToString(DRow["LUSTERCODE"]);
                DRNew["Grain"] = Val.ToString(DRow["GRAINCODE"]);

                DRNew["Cts"] = Val.Val(DRow["CARAT"]);
                DRNew["Dis"] = Val.Val(DRow["DISCOUNT"]);
                DRNew["AmtDis"] = Val.Val(DRow["AMOUNTDISCOUNT"]);
                DRNew["Rap"] = Val.Val(DRow["RAPAPORT"]);
                DRNew["Rate"] = Val.Val(DRow["PRICEPERCARAT"]);
                DRNew["Amt"] = Val.Val(DRow["AMOUNT"]);
                if (Int == 0)
                {
                    DouAmt = Val.Val(DRow["AMOUNT"]);
                    DRNew["DiffAmt"] = 0.00;
                }
                else
                {
                    DRNew["DiffAmt"] = Math.Round(DouAmt - Val.Val(DRow["AMOUNT"]), 0);
                }

                DRNew["RapDate"] = Val.ToString(CmbRapDate.SelectedItem);

                DRNew["GCts"] = Val.Val(DRow["GCARAT"]);

                DRNew["GCol"] = Val.ToString(DRow["GCOLORCODE"]);
                DRNew["GCla"] = Val.ToString(DRow["GCLARITYCODE"]);
                DRNew["GCut"] = Val.ToString(DRow["GCUTCODE"]);
                DRNew["GPol"] = Val.ToString(DRow["GPOLCODE"]);
                DRNew["GSym"] = Val.ToString(DRow["GSYMCODE"]);

                DRNew["ENTRYDATE"] = Val.ToString(DateTime.Now.ToString("dd/MM/yyyy"));

                DRNew["ISFINAL"] = Val.ToString(DRow["ISFINAL"]);

                DRNew["GDis"] = Val.Val(DRow["GDISCOUNT"]);
                DRNew["GAmtDis"] = Val.Val(DRow["GAMOUNTDISCOUNT"]);
                DRNew["GRap"] = Val.Val(DRow["GRAPAPORT"]);
                DRNew["GRate"] = Val.Val(DRow["GPRICEPERCARAT"]);
                DRNew["GAmt"] = Val.Val(DRow["GAMOUNT"]);

                DRNew["Lab"] = Val.ToString(DRow["LABCODE"]);

                DRNew["UPCol"] = Val.ToString(DRow["UPCOLORNAME"]);
                DRNew["UPColDis"] = Val.Val(DRow["UPCOLORDISCOUNT"]);
                DRNew["UPColAmtDis"] = Val.Val(DRow["UPCOLORAMOUNTDISCOUNT"]);
                DRNew["UPColRap"] = Val.Val(DRow["UPCOLORRAPAPORT"]);
                DRNew["UPColRate"] = Val.Val(DRow["UPCOLORPRICEPERCARAT"]);
                DRNew["UPColAmt"] = Val.Val(DRow["UPCOLORAMOUNT"]);

                if (Int == 0)
                {
                    DouColUpAmt = Val.Val(DRow["UPCOLORAMOUNT"]);
                    DRNew["UPColAmtDiff"] = 0.00;
                }
                else
                {
                    DRNew["UPColAmtDiff"] = Math.Round(DouColUpAmt - Val.Val(DRow["UPCOLORAMOUNT"]), 0);
                }


                DRNew["DWCol"] = Val.ToString(DRow["DOWNCOLORNAME"]);
                DRNew["DWDis"] = Val.Val(DRow["DOWNCOLORDISCOUNT"]);
                DRNew["DWColAmtDis"] = Val.Val(DRow["DOWNCOLORAMOUNTDISCOUNT"]);
                DRNew["DWColRap"] = Val.Val(DRow["DOWNCOLORRAPAPORT"]);
                DRNew["DWColRate"] = Val.Val(DRow["DOWNCOLORPRICEPERCARAT"]);
                DRNew["DWColAmt"] = Val.Val(DRow["DOWNCOLORAMOUNT"]);

                if (Int == 0)
                {
                    DouColDWAmt = Val.Val(DRow["DOWNCOLORAMOUNT"]);
                    DRNew["DWColAmtDiff"] = 0.00;
                }
                else
                {
                    DRNew["DWColAmtDiff"] = Math.Round(DouColDWAmt - Val.Val(DRow["DOWNCOLORAMOUNT"]), 0);
                }


                DRNew["UPCla"] = Val.ToString(DRow["UPCLARITYNAME"]);
                DRNew["UPClaDis"] = Val.Val(DRow["UPCLARITYDISCOUNT"]);
                DRNew["UPClaAmtDis"] = Val.Val(DRow["UPCLARITYAMOUNTDISCOUNT"]);
                DRNew["UPClaRap"] = Val.Val(DRow["UPCLARITYRAPAPORT"]);
                DRNew["UPClaRate"] = Val.Val(DRow["UPCLARITYPRICEPERCARAT"]);
                DRNew["UPClaAmt"] = Val.Val(DRow["UPCLARITYAMOUNT"]);

                if (Int == 0)
                {
                    DouClaUpAmt = Val.Val(DRow["UPCLARITYAMOUNT"]);
                    DRNew["UPClaAmtDiff"] = 0.00;
                }
                else
                {
                    DRNew["UPClaAmtDiff"] = Math.Round(DouClaUpAmt - Val.Val(DRow["UPCLARITYAMOUNT"]), 0);
                }

                DRNew["DWCla"] = Val.ToString(DRow["DOWNCLARITYNAME"]);
                DRNew["DWClaDis"] = Val.Val(DRow["DOWNCLARITYDISCOUNT"]);
                DRNew["DWClaAmtDis"] = Val.Val(DRow["DOWNCLARITYAMOUNTDISCOUNT"]);
                DRNew["DWClaRap"] = Val.Val(DRow["DOWNCLARITYRAPAPORT"]);
                DRNew["DWClaRate"] = Val.Val(DRow["DOWNCLARITYPRICEPERCARAT"]);
                DRNew["DWClaAmt"] = Val.Val(DRow["DOWNCLARITYAMOUNT"]);

                if (Int == 0)
                {
                    DouClaDwAmt = Val.Val(DRow["DOWNCLARITYAMOUNT"]);
                    DRNew["DWClaAmtDiff"] = 0.00;
                }
                else
                {
                    DRNew["DWClaAmtDiff"] = Math.Round(DouClaDwAmt - Val.Val(DRow["DOWNCLARITYAMOUNT"]), 0);
                }
                DRNew["TONAME"] = Val.ToString(DRow["TOCODE"]);
                DRNew["CONAME"] = Val.ToString(DRow["COCODE"]);
                DRNew["PONAME"] = Val.ToString(DRow["POCODE"]);
                DTab.Rows.Add(DRNew);

                Int++;
            }


            return DTab;
        }


        private void BtnGeneratePlans_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainGrid.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                int PlanNo = Val.ToInt32(DTabPrediction.Compute("MAX(PlanNo)", string.Empty));
                PlanNo = PlanNo + 1;

                lblCurrentPlan.Text = PlanNo.ToString();

                if (txtTag.Enabled == false)
                {
                    DataRow DRow = DTabPrediction.NewRow();
                    DRow["PLANNO"] = PlanNo;
                    DRow["TAGSRNO"] = 1;
                    DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                    DRow["TAG"] = Char.ConvertFromUtf32(64 + Val.ToInt(DRow["TAGSRNO"]));
                    DRow["ID"] = 0;
                    DRow["PRD_ID"] = 0;
                    DTabPrediction.Rows.Add(DRow);
                }

                else if (txtTag.Enabled == true && txtTag.Text.Trim().Length != 0)
                {
                    DataRow DRow = DTabPrediction.NewRow();
                    DRow["PLANNO"] = PlanNo;
                    DRow["TAGSRNO"] = -1;
                    DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                    DRow["TAG"] = txtTag.Text;
                    DRow["ID"] = 0;
                    DRow["PRD_ID"] = 0;
                    DTabPrediction.Rows.Add(DRow);
                }
                else if (txtTag.Enabled == true && txtTag.Text.Trim().Length == 0)
                {
                    DataRow DRow = DTabPrediction.NewRow();
                    DRow["PLANNO"] = PlanNo;
                    DRow["TAGSRNO"] = -1;
                    DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                    DRow["TAG"] = "";
                    DRow["ID"] = 0;
                    DRow["PRD_ID"] = 0;
                    DTabPrediction.Rows.Add(DRow);

                }

                GrdDet.ExpandAllGroups();
                GrdDet.RefreshData();
                //GrdDet.FocusedRowHandle = GrdDet.RowCount;
                GrdDet.MoveLast();
                //GrdDet.SelectRow(GrdDet.FocusedRowHandle);
                //Fetch_SetRadioButton(PanelFL, Val.ToInt(mStrPktFL_ID), false);  //#P : 21-02-2020

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.ToString());
            }


        }

        private void BtnBlankNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainGrid.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                if (GrdDet.RowCount == 0)
                {
                    BtnGeneratePlans.PerformClick();
                }
                else
                {
                    int TagSrNo = Val.ToInt32(DTabPrediction.Compute("MAX(TAGSRNO)", " PlanNo='" + lblCurrentPlan.Text + "'"));
                    TagSrNo = TagSrNo + 1;

                    int TagSrNoRepeat = 0; //#P : 07-10-2020
                    if (TagSrNo > 26)
                        TagSrNoRepeat = TagSrNo - 26;


                    // Add : Pinali : 18-09-2019
                    int IntTFlag = Val.ToBooleanToInt(DTabPrediction.Compute("MAX(TFLAG)", " PlanNo='" + lblCurrentPlan.Text + "'"));
                    int IntISFinal = Val.ToBooleanToInt(DTabPrediction.Compute("MAX(ISFINAL)", " PlanNo='" + lblCurrentPlan.Text + "'"));
                    // End : Pinali : 18-09-2019

                    DataRow DRow = DTabPrediction.NewRow();
                    DRow["PLANNO"] = lblCurrentPlan.Text;
                    DRow["TAGSRNO"] = TagSrNo;
                    DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                    //DRow["TAG"] = Char.ConvertFromUtf32(64 + Val.ToInt(DRow["TAGSRNO"]));
                    DRow["TAG"] = TagSrNo > 26 ? Val.ToString(Char.ConvertFromUtf32(65) + Char.ConvertFromUtf32(64 + TagSrNoRepeat)) : Char.ConvertFromUtf32(64 + Val.ToInt(DRow["TAGSRNO"]));
                    DRow["ID"] = 0;
                    DRow["PRD_ID"] = 0;
                    DRow["TFLAG"] = IntTFlag;  // Add : Pinali : 18-09-2019
                    DRow["ISFINAL"] = IntISFinal;  // Add : Pinali : 18-09-2019

                    DTabPrediction.Rows.Add(DRow);

                    GrdDet.ExpandAllGroups();
                    GrdDet.RefreshData();
                    //GrdDet.SelectRow(GrdDet.RowCount - 1);
                    //GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    //GrdDet.MoveLast();  /// Cmnt : Pinali : 04-10-2019
                }
                //Fetch_SetRadioButton(PanelFL, Val.ToInt(mStrPktFL_ID), false);  //#P : 21-02-2020
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }

        private void BtnCopyPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainGrid.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                if (GrdDet.FocusedRowHandle < 0)
                {
                    Global.MessageError("Kindly Select One Row For Copy Full Plan");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                int TagSrNo = Val.ToInt32(DTabPrediction.Compute("MAX(TAGSRNO)", " PlanNo='" + lblCurrentPlan.Text + "'"));
                TagSrNo = TagSrNo + 1;

                int PlanNo = Val.ToInt32(DTabPrediction.Compute("MAX(PLANNO)", ""));
                PlanNo = PlanNo + 1;


                DataRow[] UDRow = DTabPrediction.Select(" PlanNo='" + lblCurrentPlan.Text + "'");

                foreach (DataRow DR in UDRow)
                {
                    DataRow DRow = DTabPrediction.NewRow();
                    DRow["PLANNO"] = PlanNo;
                    DRow["TAGSRNO"] = DR["TAGSRNO"];
                    DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                    DRow["TAG"] = DR["TAG"];
                    DRow["ID"] = 0;
                    DRow["PRD_ID"] = 0;


                    foreach (DataColumn Col in DR.Table.Columns)
                    {
                        if (Col.ColumnName.ToString().ToUpper() == "PLANNO" ||
                            Col.ColumnName.ToString().ToUpper() == "TAGSRNO" ||
                            Col.ColumnName.ToString().ToUpper() == "TAG" ||
                            Col.ColumnName.ToString().ToUpper() == "RAPDATE" ||
                            Col.ColumnName.ToString().ToUpper() == "ID" ||
                            Col.ColumnName.ToString().ToUpper() == "PRD_ID" ||
                            Col.ColumnName.ToString().ToUpper() == "ISFINAL" ||
                            Col.ColumnName.ToString().ToUpper() == "TFLAG"
                            )
                        {
                            continue;
                        }

                        if (DTabPrediction.Columns.Contains(Col.ColumnName))
                        {
                            try
                            {
                                DRow[Col.ColumnName] = DR[Col.ColumnName];
                            }
                            catch
                            {

                            }
                        }
                    }

                    DTabPrediction.Rows.Add(DRow);

                }


                GrdDet.ExpandAllGroups();
                GrdDet.RefreshData();
                GrdDet.MoveLast();
                //GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                //GrdDet.SelectRow(GrdDet.FocusedRowHandle);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.ToString());
            }
        }

        private void BtnCopyNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainGrid.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                if (GrdDet.FocusedRowHandle < 0)
                {
                    Global.MessageError("Kindly Select One Row For Copy");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                DataRow DRCopy = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

                int TagSrNo = Val.ToInt32(DTabPrediction.Compute("MAX(TAGSRNO)", " PlanNo='" + lblCurrentPlan.Text + "'"));
                TagSrNo = TagSrNo + 1;


                int TagSrNoRepeat = 0; //#P : 07-10-2020
                if (TagSrNo > 26)
                    TagSrNoRepeat = TagSrNo - 26;

                DataRow DRow = DTabPrediction.NewRow();
                DRow["PLANNO"] = lblCurrentPlan.Text;
                DRow["TAGSRNO"] = TagSrNo;
                DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                //DRow["TAG"] = Char.ConvertFromUtf32(64 + Val.ToInt(DRow["TAGSRNO"]));
                DRow["TAG"] = TagSrNo > 26 ? Val.ToString(Char.ConvertFromUtf32(65) + Char.ConvertFromUtf32(64 + TagSrNoRepeat)) : Char.ConvertFromUtf32(64 + Val.ToInt(DRow["TAGSRNO"]));

                DRow["ID"] = 0;
                DRow["PRD_ID"] = 0;
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

                    if (DTabPrediction.Columns.Contains(Col.ColumnName))
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


                DTabPrediction.Rows.Add(DRow);

                GrdDet.ExpandAllGroups();
                GrdDet.RefreshData();
                //GrdDet.MoveLast();  // Cmnt : Pinali : 04-10-2019
                //GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                //GrdDet.SelectRow(GrdDet.FocusedRowHandle);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.ToString());
            }
        }

        #region Control Envets

        private void CmbRapDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                //DTabPrediction.Rows[IntI]["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem); //Cmnt : #P : 17-08-2022 : Thorugh Error
                GrdDet.SetRowCellValue(IntI, "RAPDATE", Val.ToString(CmbRapDate.SelectedItem));
                GrdDet.FocusedRowHandle = IntI;
                FindRap();
            }
        }

        private void CmbMilky_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainGrid.Enabled == false)
                {
                    Global.MessageError("Grid Is Unable To Update");
                    return;
                }

                AxonContLib.cComboBox combo = sender as AxonContLib.cComboBox;
                if (combo == null)
                {
                    return;
                }
                DataStructure selectedDataStructure = combo.SelectedItem as DataStructure;
                if (selectedDataStructure == null)
                {
                    Global.MessageError("You didn't select anything at the moment");
                }
                else
                {
                    if (combo.AccessibleDescription == "MILKY")
                    {
                        GrdDet.SetFocusedRowCellValue("MILKY_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("MILKYCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("MILKYNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LBLC")
                    {
                        GrdDet.SetFocusedRowCellValue("LBLC_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("LBLCCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("LBLCNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "NATTS")
                    {
                        GrdDet.SetFocusedRowCellValue("NATTS_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("NATTSCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("NATTSNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "BLACK")
                    {
                        GrdDet.SetFocusedRowCellValue("BLACKINC_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("BLACKINCCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("BLACKINCNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "OPEN")
                    {
                        GrdDet.SetFocusedRowCellValue("OPENINC_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("OPENINCCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("OPENINCNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "WHITE")
                    {
                        GrdDet.SetFocusedRowCellValue("WHITEINC_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("WHITEINCCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("WHITEINCNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "HEARTANDARROW")
                    {
                        GrdDet.SetFocusedRowCellValue("HA_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("HACODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("HANAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "PAVALION")
                    {
                        GrdDet.SetFocusedRowCellValue("PAV_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("PAVCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("PAVNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "TENSION")
                    {
                        GrdDet.SetFocusedRowCellValue("TENSION_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("TENSIONCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("TENSIONNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "SAKHAT")
                    {
                        GrdDet.SetFocusedRowCellValue("SAKHAT_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("SAKHATCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("SAKHATNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "COLORSHADE")
                    {
                        GrdDet.SetFocusedRowCellValue("COLORSHADE_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("COLORSHADECODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("COLORSHADENAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LUSTER")
                    {
                        GrdDet.SetFocusedRowCellValue("LUSTER_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("LUSTERCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("LUSTERNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "EYECLEAN")
                    {
                        GrdDet.SetFocusedRowCellValue("EYECLEAN_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("EYECLEANCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("EYECLEANNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "NATURAL")
                    {
                        GrdDet.SetFocusedRowCellValue("NATURAL_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("NATURALCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("NATURALNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "GRAIN")
                    {
                        GrdDet.SetFocusedRowCellValue("GRAIN_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("GRAINCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("GRAINNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LAB")
                    {
                        GrdDet.SetFocusedRowCellValue("LAB_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("LABCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("LABNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "TABLEOPEN")
                    {
                        GrdDet.SetFocusedRowCellValue("TO_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("TOCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("TONAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "CROWNOPEN")
                    {
                        GrdDet.SetFocusedRowCellValue("CO_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("COCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("CONAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "PAVILLIONOPEN")
                    {
                        GrdDet.SetFocusedRowCellValue("PO_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("POCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("PONAME", selectedDataStructure.PARANAME);
                    }
                }

                FindRap();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }

        #endregion

        #region Fetch Record


        public void Fetch_SetRadioButton(FlowLayoutPanel pn, int Value, bool ISReset)
        {

            if (Value == 0)
            {
                AxonContLib.cButton rb = pn.Controls.OfType<AxonContLib.cButton>().FirstOrDefault();
                rb.AccessibleName = "true";
                rb.ForeColor = mSelectedColor;
                rb.BackColor = mSelectedBackColor;
            }
            else
            {
                pn.Controls.OfType<AxonContLib.cButton>().Where(i => Val.ToInt(i.Tag) != Value).ToList().ForEach(a =>
                {
                    a.AccessibleName = "false";
                    a.ForeColor = mDeSelectColor;
                    a.BackColor = mDSelectedBackColor;
                });

                pn.Controls.OfType<AxonContLib.cButton>().Where(i => Val.ToInt(i.Tag) == Value).ToList().ForEach(a =>
                {
                    a.AccessibleName = "true";
                    a.ForeColor = mSelectedColor;
                    a.BackColor = mSelectedBackColor;
                });
            }

            //int I = 0;
            //foreach (AxonContLib.cButton Cont in pn.Controls)
            //{
            //    if (ISReset == true)
            //    {
            //        if (Value == 0 && I == 0)
            //        {
            //            Cont.AccessibleName = "true";
            //            Cont.ForeColor = mSelectedColor;
            //            Cont.BackColor = mSelectedBackColor;
            //        }
            //        else if (Val.ToInt(Cont.Tag) == Value)
            //        {
            //            Cont.AccessibleName = "true";
            //            Cont.ForeColor = mSelectedColor;
            //            Cont.BackColor = mSelectedBackColor;
            //        }
            //        else
            //        {
            //            Cont.AccessibleName = "false";
            //            Cont.ForeColor = mDeSelectColor;
            //            Cont.BackColor = mDSelectedBackColor;
            //        }
            //    }
            //    else
            //    {
            //        if (Val.ToInt(Cont.Tag) == Value)
            //        {
            //            Cont.AccessibleName = "true";
            //            Cont.ForeColor = mSelectedColor;
            //            Cont.BackColor = mSelectedBackColor;
            //        }
            //        else
            //        {
            //            Cont.AccessibleName = "false";
            //            Cont.ForeColor = mDeSelectColor;
            //            Cont.BackColor = mDSelectedBackColor;
            //        }
            //    }

            //    I++;
            //}
        }

        public void Fetch_SetComboBox(AxonContLib.cComboBox Combo, IList<DataStructure> pData, int Value)
        {
            foreach (DataStructure data in pData)
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

        public void FetchRecord(int FocusedRowHandle)
        {
            try
            {
                PanelCarat.Text = "";

                //foreach (AxonContLib.cLabel Cont in PanelCarat.Controls)
                //{
                //    Cont.BackColor = Color.LightGray;
                //    if (Cont.Text == "0")
                //    {
                //        Cont.BackColor = Color.FromArgb(255, 224, 192);
                //    }

                //}

                /*
                ListShape[0].Checked = true;
                ListShape[0].ForeColor = mSelectedColor;
                ListShape[0].BackColor = mSelectedBackColor;

                ListColor[0].Checked = true;
                ListColor[0].ForeColor = mSelectedColor;
                ListColor[0].BackColor = mSelectedBackColor;

                ListClarity[0].Checked = true;
                ListClarity[0].ForeColor = mSelectedColor;
                ListClarity[0].BackColor = mSelectedBackColor;

                ListCut[0].Checked = true;
                ListCut[0].ForeColor = mSelectedColor;
                ListCut[0].BackColor = mSelectedBackColor;

                ListPol[0].Checked = true;
                ListPol[0].ForeColor = mSelectedColor;
                ListPol[0].BackColor = mSelectedBackColor;

                ListSym[0].Checked = true;
                ListSym[0].ForeColor = mSelectedColor;
                ListSym[0].BackColor = mSelectedBackColor;

                ListFL[0].Checked = true;
                ListFL[0].ForeColor = mSelectedColor;
                ListFL[0].BackColor = mSelectedBackColor;

                CmbMilky.SelectedIndex = 0;
                CmbLBLC.SelectedIndex = 0;
                CmbNatts.SelectedIndex = 0;

                CmbBlackInc.SelectedIndex = 0;
                CmbOpenInc.SelectedIndex = 0;
                CmbWhiteInc.SelectedIndex = 0;

                CmbHA.SelectedIndex = 0;
                CmbPavalion.SelectedIndex = 0;
                CmbTension.SelectedIndex = 0;

                CmbColorShade.SelectedIndex = 0;
                CmbLuster.SelectedIndex = 0;

                CmbEyeClean.SelectedIndex = 0;
                CmbNatural.SelectedIndex = 0;
                CmbGrain.SelectedIndex = 0;
                CmbLab.SelectedIndex = 0;

                RbtExp.Checked = true;
                */

                if (FocusedRowHandle < 0)
                {
                    return;
                }
                DataRow DRow = GrdDet.GetDataRow(FocusedRowHandle);

                if (DRow == null)
                {
                    return;
                }

                Fetch_SetRadioButton(PanelShape, Val.ToInt(DRow["SHAPE_ID"]), true);
                Fetch_SetRadioButton(PanelColor, Val.ToInt(DRow["COLOR_ID"]), true);
                Fetch_SetRadioButton(PanelClarity, Val.ToInt(DRow["CLARITY_ID"]), true);
                Fetch_SetRadioButton(PanelCut, Val.ToInt(DRow["CUT_ID"]), true);
                Fetch_SetRadioButton(PanelPol, Val.ToInt(DRow["POL_ID"]), true);
                Fetch_SetRadioButton(PanelSym, Val.ToInt(DRow["SYM_ID"]), true);
                Fetch_SetRadioButton(PanelFL, Val.ToInt(DRow["FL_ID"]), true);

                ////Fetch_SetRadioButton(PanelFL, Val.ToInt(DRow["FL_ID"]), false);  //#P : 21-02-202
                //if (Val.ToString(DRow["FL_ID"]).Trim().Equals(string.Empty))
                //    Fetch_SetRadioButton(PanelFL, Val.ToInt(mStrPktFL_ID), false);
                //else
                //    Fetch_SetRadioButton(PanelFL, Val.ToInt(DRow["FL_ID"]), true);


                Fetch_SetComboBox(CmbMilky, DataMILKY, Val.ToInt(DRow["MILKY_ID"]));
                Fetch_SetComboBox(CmbLBLC, DataLBLC, Val.ToInt(DRow["LBLC_ID"]));
                Fetch_SetComboBox(CmbNatts, DataNATTS, Val.ToInt(DRow["NATTS_ID"]));
                Fetch_SetComboBox(CmbBlackInc, DataBLACK, Val.ToInt(DRow["BLACKINC_ID"]));
                Fetch_SetComboBox(CmbOpenInc, DataOPEN, Val.ToInt(DRow["OPENINC_ID"]));
                Fetch_SetComboBox(CmbWhiteInc, DataWHITE, Val.ToInt(DRow["WHITEINC_ID"]));
                Fetch_SetComboBox(CmbHA, DataHEARTANDARROW, Val.ToInt(DRow["HA_ID"]));
                Fetch_SetComboBox(CmbPavalion, DataPAVALION, Val.ToInt(DRow["PAV_ID"]));
                Fetch_SetComboBox(CmbTension, DataTENSION, Val.ToInt(DRow["TENSION_ID"]));
                Fetch_SetComboBox(CmbSakhat, DataSAKHAT, Val.ToInt(DRow["SAKHAT_ID"])); //#P : 26-10-2020    
                Fetch_SetComboBox(CmbColorShade, DataCOLORSHADE, Val.ToInt(DRow["COLORSHADE_ID"]));
                Fetch_SetComboBox(CmbLuster, DataLUSTER, Val.ToInt(DRow["LUSTER_ID"]));
                Fetch_SetComboBox(CmbEyeClean, DataEYECLEAN, Val.ToInt(DRow["EYECLEAN_ID"]));
                Fetch_SetComboBox(CmbNatural, DataNATURAL, Val.ToInt(DRow["NATURAL_ID"]));
                Fetch_SetComboBox(CmbGrain, DataGRAIN, Val.ToInt(DRow["GRAIN_ID"]));
                Fetch_SetComboBox(CmbLab, DataLAB, Val.ToInt(DRow["LAB_ID"]));

                Fetch_SetComboBox(CmbTableOpen, DataTABLEOPEN, Val.ToInt(DRow["TO_ID"]));
                Fetch_SetComboBox(CmbCrownOpen, DataCROWNOPEN, Val.ToInt(DRow["CO_ID"]));
                Fetch_SetComboBox(CmbPavillionOpen, DataPAVILLIONOPEN, Val.ToInt(DRow["PO_ID"]));

                lblCurrentPlan.Text = Val.ToString(DRow["PLANNO"]);

                txtDiaMin.Text = Val.ToString(DRow["DIAMIN"]);
                txtDiaMax.Text = Val.ToString(DRow["DIAMAX"]);
                txtHeight.Text = Val.ToString(DRow["HEIGHT"]);

                if (RbtExp.Checked == true)
                {
                    Fetch_SetRadioButton(PanelCut, Val.ToInt(DRow["CUT_ID"]), true);
                    Fetch_SetRadioButton(PanelPol, Val.ToInt(DRow["POL_ID"]), true);
                    Fetch_SetRadioButton(PanelSym, Val.ToInt(DRow["SYM_ID"]), true);

                    PanelCarat.Text = Val.ToString(DRow["CARAT"]);
                    string Carat = Val.Format(Val.ToString(DRow["CARAT"]), "#00.000");
                    Carat = Carat.Replace(".", "");
                }
                else
                {
                    if (Val.Val(Val.ToString(DRow["GCARAT"])) == 0)
                    {
                        DTabPrediction.Rows[FocusedRowHandle]["GCARAT"] = DRow["CARAT"];
                    }

                    if (Val.ToInt(Val.ToString(DRow["GCUT_ID"])) == 0)
                    {

                        DTabPrediction.Rows[FocusedRowHandle]["GCUT_ID"] = DRow["CUT_ID"];
                        DTabPrediction.Rows[FocusedRowHandle]["GCUTCODE"] = DRow["CUTCODE"];
                        DTabPrediction.Rows[FocusedRowHandle]["GCUTNAME"] = DRow["CUTNAME"];

                    }

                    if (Val.ToInt(Val.ToString(DRow["GPOL_ID"])) == 0)
                    {
                        DTabPrediction.Rows[FocusedRowHandle]["GPOL_ID"] = DRow["POL_ID"];
                        DTabPrediction.Rows[FocusedRowHandle]["GPOLCODE"] = DRow["POLCODE"];
                        DTabPrediction.Rows[FocusedRowHandle]["GPOLNAME"] = DRow["POLNAME"];
                    }

                    if (Val.ToInt(Val.ToString(DRow["GSYM_ID"])) == 0)
                    {
                        DTabPrediction.Rows[FocusedRowHandle]["GSYM_ID"] = DRow["SYM_ID"];
                        DTabPrediction.Rows[FocusedRowHandle]["GSYMCODE"] = DRow["SYMCODE"];
                        DTabPrediction.Rows[FocusedRowHandle]["GSYMNAME"] = DRow["SYMNAME"];
                    }

                    Fetch_SetRadioButton(PanelCut, Val.ToInt(DRow["GCUT_ID"]), true);
                    Fetch_SetRadioButton(PanelPol, Val.ToInt(DRow["GPOL_ID"]), true);
                    Fetch_SetRadioButton(PanelSym, Val.ToInt(DRow["GSYM_ID"]), true);

                    PanelCarat.Text = Val.ToString(DRow["GCARAT"]);
                    string Carat = Val.Format(Val.ToString(DRow["GCARAT"]), "#00.000");
                    Carat = Carat.Replace(".", "");

                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }


        private void RbtExp_CheckedChanged(object sender, EventArgs e)
        {
            FetchRecord(GrdDet.FocusedRowHandle);
        }

        private void RbtGraph_CheckedChanged(object sender, EventArgs e)
        {
            FetchRecord(GrdDet.FocusedRowHandle);
            FindRap();
        }


        #endregion

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                lblPrdID.Text = string.Empty;
                DTabPrediction.Rows.Clear();
                if (RbtPacketNo.Checked)
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
                }
                else if (RbtBarcode.Checked)
                {
                    if (txtBarcode.Text.Length == 0)
                    {
                        Global.MessageError("Barcode Is Required");
                        txtBarcode.Focus();
                        return;
                    }
                }
                else if (RbtPktSerialNo.Checked)
                {
                    if (txtSrNoKapanName.Text.Length == 0)
                    {
                        Global.MessageError("Kapan Name Is Required");
                        txtSrNoKapanName.Focus();
                        return;
                    }
                    if (Val.Val(txtSrNoSerialNo.Text) == 0)
                    {
                        Global.MessageError("Serial No Is Required");
                        txtSrNoSerialNo.Focus();
                        return;
                    }
                }

                string StrTag = "A";

                mStrKapanName = RbtPktSerialNo.Checked ? Val.ToString(txtSrNoKapanName.Text) : Val.ToString(txtKapanName.Text);

                BtnSave.Text = "&Save"; //#p

                if (txtTag.Enabled == false)
                {
                    StrTag = "A";
                    BtnBlankNew.Enabled = true;
                    BtnGeneratePlans.Enabled = true;

                    //Add : Pinali : 31-07-2019
                    if ((Val.ToInt(CmbPrdType.Tag) == 1) || Val.ToInt(CmbPrdType.Tag) == 2 || Val.ToInt(CmbPrdType.Tag) == 10)
                    {
                        //string StrFL_ID = "";

                        string StrPacketNO = Val.ToString(txtPacketNo.Text).Trim().Equals(string.Empty) ? "0" : Val.ToString(txtPacketNo.Text);

                        mStrPktFL_ID = ObjRap.GetPacketFlName(Val.ToString(txtKapanName.Text), Val.ToString(StrPacketNO), StrTag, Val.ToString(txtBarcode.Text), Val.ToString(txtSrNoKapanName.Text), Val.ToInt32(txtSrNoSerialNo.Text));
                        if (mStrPktFL_ID == "FAIL")
                        {
                            Global.Message("Packet Not Exists Please Check..");
                            if (RbtPacketNo.Checked)
                                txtKapanName.Focus();
                            else if (RbtBarcode.Checked)
                                txtBarcode.Focus();
                            else if (RbtPktSerialNo.Checked)
                                txtSrNoKapanName.Focus();
                            return;
                        }
                        Fetch_SetRadioButton(PanelFL, Val.ToInt(mStrPktFL_ID), true);

                        //Comment : Pinali : 07-09-2019 as Per Discuss with pratik bhai
                        //if (Val.ToInt(StrFL_ID) > 0)
                        //    PanelFL.Enabled = false;
                        //else
                        //    PanelFL.Enabled = true;
                    }
                    //End : Pinali : 31-07-2019

                }
                else
                {
                    if (txtTag.Text.Length == 0 && RbtPacketNo.Checked)
                    {
                        Global.MessageError("Tag Is Required");
                        txtTag.Focus();
                        return;
                    }
                    StrTag = txtTag.Text;
                    BtnBlankNew.Enabled = false;
                    BtnGeneratePlans.Enabled = false;

                    PanelFL.Enabled = true;
                }

                //#P : 19-09-2020 : coz Jyare ek record save krine second time bija packet mate direct packet no nakhe 6e.. show pr click na karyu hoy to issue aavse that's why...
                PanelCarat.Enabled = true;
                PanelParameter.Enabled = true;
                PanelButtons.Enabled = true;
                MainGrid.Enabled = true;


                this.Cursor = Cursors.WaitCursor;

                mStrType = "SHOWCLICK";

                BtnSave.Enabled = true;
                BtnFetchPrevPlan.Enabled = true;
                GrdDet.Columns["ISFINAL"].OptionsColumn.AllowEdit = true;
                GrdDet.Columns["DELETE"].Visible = true;


                if (RbtPktSerialNo.Checked || RbtBarcode.Checked)
                {
                    txtKapanName.Text = string.Empty;
                    txtPacketNo.Text = string.Empty;
                    txtTag.Text = string.Empty;
                    txtTag.Tag = string.Empty;
                }


                DataRow DRPkt = ObjRap.GetPacketDataRow(mStrKapanName, Val.ToInt32(txtPacketNo.Text), StrTag, Val.ToString(txtBarcode.Text), Val.ToString(txtSrNoKapanName.Text), Val.ToInt32(txtSrNoSerialNo.Text));
                if (DRPkt == null)
                {
                    Global.MessageError("Ooops.. Packet Is Not Found");
                    this.Cursor = Cursors.Default;
                    return;
                }
                txtTag.Tag = Val.ToString(DRPkt["PACKET_ID"]);
                lblLot.Text = Val.ToString(DRPkt["LOTCARAT"]);
                lblBalance.Text = Val.ToString(DRPkt["BALANCECARAT"]);
                lblBalance.Tag = Val.ToString(DRPkt["TRN_ID"]); //27-09-2022

                txtKapanName.Text = Val.ToString(DRPkt["KAPANNAME"]);
                txtPacketNo.Text = Val.ToString(DRPkt["PACKETNO"]);
                txtTag.Text = txtTag.Enabled ? Val.ToString(DRPkt["TAG"]) : string.Empty;

                StrTag = StrTag == "" ? txtTag.Text : StrTag;

                PKTSRNOSER = Val.ToInt32(DRPkt["PKTSERIALNO"]);
                TAGSER = Val.ToString(DRPkt["TAG"]);
                mstrBarcode = Val.ToString(DRPkt["BARCODE"]);

                DRPkt = null;

                if (txtEmployee.Text.Length != 0)
                {
                    Trn_RapSaveProperty Property = new Trn_RapSaveProperty();

                    Property.PRDTYPE_ID = Val.ToInt32(CmbPrdType.Tag);
                    Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                    Property = ObjRap.CheckValForDesignation(Property);
                    if (Property.ReturnMessageType == "FAIL")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        this.Cursor = Cursors.Default;
                        BtnBlankNew.Enabled = false;
                        BtnGeneratePlans.Enabled = false;
                        BtnSave.Enabled = false;
                        txtEmployee.Focus();
                        return;
                    }

                }


                /*
                if (Val.ToInt32(CmbPrdType.Tag) == 7)
                {
                    DataTable DTabVal = ObjRap.GetVelForRapCal(Val.ToString(txtTag.Tag));

                    if (DTabVal.Rows.Count == 0)
                    {
                        Global.Message("In Packet '" + txtKapanName.Text + "-" + Val.ToString(txtPacketNo.Text) + txtTag.Text + "' Polish Ok Prediction Not Enterd..");
                        this.Cursor = Cursors.Default;
                        txtKapanName.Focus();
                        BtnBlankNew.Enabled = false;
                        BtnGeneratePlans.Enabled = false;
                        BtnSave.Enabled = false;
                        //BtnClear.PerformClick();
                        return;

                    }
                }
                */

                DataTable DTab = ObjRap.GetPredictionData(Val.ToInt32(CmbPrdType.Tag), Val.ToString(txtTag.Tag), Val.ToInt64(txtEmployee.Tag), Val.ToInt64(txtManager.Tag));

                // check PREDICTION THAT ANY FINAL OR NOT 
                bool ISFinalSelect = false;

                foreach (DataRow DRow in DTab.Rows)
                {

                    if (Val.ToBoolean(DRow["ISFINAL"]) == true)
                    {
                        mIntOldISFinalFlagPlanNo = Val.ToInt32(DRow["PLANNO"]); //#P : 11-10-2019
                        ISFinalSelect = true;
                        break;
                    }
                    else
                    {
                        mIntOldISFinalFlagPlanNo = 0;
                    }
                }

                lblMessage.Text = string.Empty;


                if (Val.ToInt32(CmbPrdType.Tag) == 13)
                {
                    if (DTab.Rows.Count == 0)
                    {
                        Global.Message("Marker Or Checker Prediction Not Found For This Entry");
                        return;
                    }
                }

                MainGrid.ContextMenuStrip = contextMenuStrip1;

                GrdDet.Columns["DELETE"].Visible = true;
                // Check Edit Mode
                if (ISFinalSelect == true && DTab.Rows.Count != 0 && Val.ToBoolean(DTab.Rows[0]["ALLOWFORUPDATE"]) == false)
                {
                    BtnSave.Text = "&Update";
                    BtnSave.Enabled = EmployeeRightsProperty.RAPUPDATEPREDICTION;
                    GrdDet.Columns["ISFINAL"].OptionsColumn.AllowEdit = EmployeeRightsProperty.RAPUPDATEPREDICTION;
                    GrdDet.Columns["DELETE"].Visible = EmployeeRightsProperty.RAPUPDATEPREDICTION;

                    if (BtnSave.Enabled == false)
                    {
                        MainGrid.ContextMenuStrip = null;
                    }
                }
                else
                {
                    if (DTab.Rows.Count == 0)
                    {
                        // Validation For Previous Prediction is Exists Or Not
                        Trn_RapSaveProperty Property = new Trn_RapSaveProperty();
                        Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                        Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                        Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                        Property.TAG = StrTag;
                        Property.MTAG = StrTag;
                        Property.PACKET_ID = Val.ToInt64(txtTag.Tag);

                        Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                        Property = ObjRap.ValSave(Property);
                        if (Property.ReturnMessageType == "FAIL")
                        {
                            BtnBlankNew.Enabled = false;
                            BtnGeneratePlans.Enabled = false;

                            this.Cursor = Cursors.Default;

                            //#P : 19-08-2020
                            if (Val.ToString(Property.ReturnValue).Trim().Contains("ROUGHMAKABLE_FAIL"))
                            {
                                if (Global.Confirm("Final/Checker Entry / Any Of One Final Plan Is Required For Save This... Do You Want To Continue Without Final(Tflag) Plan...?") == System.Windows.Forms.DialogResult.Yes)
                                {
                                    ISCopyRoughMkblPlanIntoFinalTFlagPrd = 1;
                                }
                                else
                                {
                                    ISCopyRoughMkblPlanIntoFinalTFlagPrd = 0;
                                    Global.MessageError("You Can''t Save Rough Mkble Prd Without Final(TFlag) Plan..");
                                    return;
                                }
                            }
                            else
                            {
                                ISCopyRoughMkblPlanIntoFinalTFlagPrd = 0;
                                Global.MessageError(Property.ReturnMessageDesc);
                                BtnBlankNew.Enabled = false;
                                BtnGeneratePlans.Enabled = false;
                                BtnSave.Enabled = false;
                                return;
                            }
                            //#P : 19-08-2020
                            //Global.MessageError(Property.ReturnMessageDesc);
                            //return;
                        }
                        Property = null;
                    }
                }

                if (DTab.Rows.Count != 0)
                {
                    lblBalance.Text = Val.ToString(DTab.Rows[0]["BALANCECARAT"]);  //#P : 29-06-2020

                    lblBalance.Tag = Val.ToString(DTab.Rows[0]["TRN_ID"]); //#P : 27-09-2022

                    ChkDiff.Checked = Val.ToBoolean(DTab.Rows[0]["ISDIFF"]);
                    txtRemark.Text = Val.ToString(DTab.Rows[0]["REMARK"]);
                    CmbLabProcess.SelectedItem = Val.ToString(DTab.Rows[0]["LABPROCESS"]);
                    CmbLabSelection.SelectedItem = Val.ToString(DTab.Rows[0]["LABSELECTION"]);
                    txtDiaMin.Text = Val.ToString(DTab.Rows[0]["DIAMIN"]);
                    txtDiaMax.Text = Val.ToString(DTab.Rows[0]["DIAMAX"]);
                    txtHeight.Text = Val.ToString(DTab.Rows[0]["HEIGHT"]);

                    ChkNOBGM.Checked = Val.ToBoolean(DTab.Rows[0]["ISNOBGM"]);
                    ChkNOBlack.Checked = Val.ToBoolean(DTab.Rows[0]["ISNOBLACK"]);

                    CmbRapDate.SelectedItem = DateTime.Parse(Val.ToString(DTab.Rows[0]["RAPDATE"])).ToString("dd/MM/yyyy");

                    lblPrdID.Text = Val.ToString(DTab.Rows[0]["PRD_ID"]);
                    //lblBalance.Text = Val.ToString(DTab.Rows[0]["BALANCECARAT"]); // Add : Pinali : 27-01-2020 : Coz Issue On Update Prd And BalanceCarat Is Chnged


                    int IntISBreak = Val.ToInt(DTab.Rows[0]["ISBREAKINGEXISTS"]);
                    if (IntISBreak != 0)
                    {
                        lblMessage.Text = "BREAKING ENTRY EXISTING IN THIS PACKET";
                    }
                    else
                    {
                        lblMessage.Text = "NO BREAKING";
                    }
                }

                foreach (DataRow DRTab in DTab.Rows)
                {
                    try
                    {
                        DataRow DRNew = DTabPrediction.NewRow();
                        foreach (DataColumn DCol in DTab.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRTab[DCol.ColumnName];
                        }
                        DTabPrediction.Rows.Add(DRNew);
                    }
                    catch (Exception ex)
                    {
                        Global.Message(ex.Message);
                    }
                }
                FindDiffAmountWithFirstRow();
                lblTotal.Text = "0";
                MainGrid.Refresh();
                GrdDet.BestFitColumns();

                //#D : 03-12-2020
                if (Val.ToInt32(CmbPrdType.Tag) == 4)
                {
                    DTabMakLog = DTabPrediction.Copy();
                }
                //End : #D : 03-12-2020

                if (GrdDet.RowCount == 0)
                {
                    BtnGeneratePlans_Click(BtnGeneratePlans, null);
                }

                GrdDet.ExpandAllGroups();

                mStrType = "NOSHOWCLICK";

                //#P : 05-02-2020
                if (Val.ToInt(CmbPrdType.Tag) != 5 && Val.ToInt(CmbPrdType.Tag) != 6) //Cheif And Art Prd time pr editable
                {
                    txtEmployee.Enabled = false;
                    BtnEmployee.Enabled = false;
                }
                //End : #P : 05-02-2020

                IntOldTFlagPlanNo = 0;
                DataRow[] DrowTFlag = DTabPrediction.Select("TFLAG = True");  //#P : 06-02-2020 : when Update Plan From "FetchPrevPlan" their Old Tflag should be manage
                if (DrowTFlag != null && DrowTFlag.Length > 0)
                {
                    IntOldTFlagPlanNo = Val.ToInt32((DTabPrediction.AsEnumerable().Where(p => p.Field<bool>("TFLAG") == true).Select(p => p.Field<Int32>("PLANNO"))).FirstOrDefault());
                }
                else
                {
                    IntOldTFlagPlanNo = 0;
                }
                ISClickOnShowButton = true;

                BtnImport.Enabled = true;

                //#P : 25-02-2022
                GrdDet.FocusedRowHandle = 0;
                GrdDet.FocusedColumn = GrdDet.Columns["SHAPECODE"];
                GrdDet.Focus();

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
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
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME,EMPLOYEESHORTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        //txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEESHORTNAME"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);

                        PanelCarat.Enabled = false;
                        PanelParameter.Enabled = false;
                        PanelButtons.Enabled = false;
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

        private void txtManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO);
                    FrmSearch.mColumnsToHide = "LEDGER_ID,DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtManager.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                        txtManager.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
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

        private void BtnKapan_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "KAPANNAME";
                FrmSearch.mSearchText = "";
                this.Cursor = Cursors.WaitCursor;
                FrmSearch.mDTab = ObjRap.GetKapan();
                //if (EmployeeRightsProperty.RAPCHANGEPACKETS == true)
                //{

                //}
                //else
                //{
                //    FrmSearch.DTab = ObjRap.GetEmployeeOSKapan();
                //}

                FrmSearch.mColumnsToHide = "";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                if (FrmSearch.mDRow != null)
                {
                    txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
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

        private void BtnPacketNo_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "PACKETNO";
                FrmSearch.mSearchText = "";
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
                if (FrmSearch.mDRow != null)
                {
                    txtPacketNo.Text = Val.ToString(FrmSearch.mDRow["PACKETNO"]);
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

        private void BtnTag_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "TAG";
                FrmSearch.mSearchText = "";
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

                FrmSearch.mColumnsToHide = "KAPAN_ID,PACKET_ID";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                if (FrmSearch.mDRow != null)
                {
                    txtTag.Text = Val.ToString(FrmSearch.mDRow["TAG"]);
                    txtTag.Tag = Val.ToString(FrmSearch.mDRow["PACKET_ID"]);
                    txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);

                    lblLot.Text = Val.ToString(FrmSearch.mDRow["LOTCARAT"]);
                    lblBalance.Text = Val.ToString(FrmSearch.mDRow["BALANCECARAT"]);
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

                    PanelCarat.Enabled = false;
                    PanelParameter.Enabled = false;
                    PanelButtons.Enabled = false;
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

        private void BtnManager_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "LEDGERCODE";
                FrmSearch.mSearchText = "";
                this.Cursor = Cursors.WaitCursor;
                FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO);
                FrmSearch.mColumnsToHide = "LEDGER_ID,DEPARTMENT_ID";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                if (FrmSearch.mDRow != null)
                {
                    txtManager.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);
                    txtManager.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
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

        private void txtKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (Global.OnKeyPressToOpenPopup(e))
            //    {
            //        FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
            //        FrmSearch.mSearchField = "KAPANNAME";
            //        FrmSearch.mSearchText = e.KeyChar.ToString();
            //        this.Cursor = Cursors.WaitCursor;
            //        FrmSearch.mDTab = ObjRap.GetKapan();
            //        //if (EmployeeRightsProperty.RAPCHANGEPACKETS == true)
            //        //{
            //        //    FrmSearch.DTab = ObjRap.GetKapan();
            //        //}
            //        //else
            //        //{
            //        //    FrmSearch.DTab = ObjRap.GetEmployeeOSKapan();
            //        //}
            //        FrmSearch.mColumnsToHide = "";
            //        this.Cursor = Cursors.Default;
            //        FrmSearch.ShowDialog();
            //        e.Handled = true;
            //        if (FrmSearch.mDRow != null)
            //        {
            //            txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);

            //            txtPacketNo.Text = string.Empty;
            //            txtPacketNo.Tag = string.Empty;
            //            //PanelCarat.Enabled = false;
            //            //PanelParameter.Enabled = false;
            //            //PanelButtons.Enabled = false;

            //        }
            //        FrmSearch.Hide();
            //        FrmSearch.Dispose();
            //        FrmSearch = null;
            //    }

            //    //if (ISClickOnShowButton == true)
            //    //{
            //    //    txtPacketNo.Text = string.Empty;
            //    //    txtPacketNo.Tag = string.Empty;
            //    //    PanelCarat.Enabled = false;
            //    //    PanelParameter.Enabled = false;
            //    //    PanelButtons.Enabled = false;
            //    //}

            //}
            //catch (Exception ex)
            //{
            //    Global.MessageError(ex.Message);
            //}
        }

        private void txtPacketNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (Global.OnKeyPressToOpenPopup(e))
            //    {
            //        FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
            //        FrmSearch.mSearchField = "PACKETNO";
            //        FrmSearch.mSearchText = e.KeyChar.ToString();
            //        this.Cursor = Cursors.WaitCursor;
            //        FrmSearch.mDTab = ObjRap.GetPacketNo(txtKapanName.Text);
            //        //if (EmployeeRightsProperty.RAPCHANGEPACKETS == true)
            //        //{
            //        //    FrmSearch.DTab = ObjRap.GetPacketNo(txtKapanName.Text);
            //        //}
            //        //else
            //        //{
            //        //    FrmSearch.DTab = ObjRap.GetEmployeeOSPacketNo(txtKapanName.Text);
            //        //}
            //        FrmSearch.mColumnsToHide = "";
            //        this.Cursor = Cursors.Default;
            //        FrmSearch.ShowDialog();
            //        e.Handled = true;
            //        if (FrmSearch.mDRow != null)
            //        {
            //            txtPacketNo.Text = Val.ToString(FrmSearch.mDRow["PACKETNO"]);
            //        }

            //        if (txtTag.Enabled == false)
            //        {
            //            PanelCarat.Enabled = false;
            //            PanelParameter.Enabled = false;
            //            PanelButtons.Enabled = false;
            //        }

            //        FrmSearch.Hide();
            //        FrmSearch.Dispose();
            //        FrmSearch = null;
            //    }

            //    if (ISClickOnShowButton == true)// && (Val.ToInt(CmbPrdType.Tag) == 2 || Val.ToInt(CmbPrdType.Tag) == 10))
            //    {
            //        //ISClickOnShowButton = false;
            //        //BtnContinue_Click(null, null);
            //        PanelCarat.Enabled = false;
            //        PanelParameter.Enabled = false;
            //        PanelButtons.Enabled = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.MessageError(ex.Message);
            //}
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

                        if (txtEmployee.Enabled == true)
                        {
                            txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                            txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);

                            PanelCarat.Enabled = false;
                            PanelParameter.Enabled = false;
                            PanelButtons.Enabled = false;
                        }
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }

                if (ISClickOnShowButton == true)
                {
                    //ISClickOnShowButton = false;
                    //BtnContinue_Click(null, null);
                    PanelCarat.Enabled = false;
                    PanelParameter.Enabled = false;
                    PanelButtons.Enabled = false;
                }
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
                string Name = Val.ToString(CmbPrdType.SelectedItem);
                DataRow[] D = DTabPrdType.Select("PRDTYPENAME ='" + Name + "' ");

                txtKapanName.Enabled = false;
                txtPacketNo.Enabled = false;
                txtTag.Enabled = false;

                txtManager.Enabled = false;
                BtnManager.Enabled = false;

                RbtGraph.Enabled = false;
                RbtExp.Enabled = false;

                if (D.Length != 0)
                {
                    DataRow DRow = D[0];
                    CmbPrdType.Tag = Val.ToString(DRow["PrdType_ID"]);

                    txtKapanName.Enabled = Val.ToBoolean(DRow["ISKapan"]);
                    txtPacketNo.Enabled = Val.ToBoolean(DRow["ISPacketNo"]);
                    txtTag.Enabled = Val.ToBoolean(DRow["ISTag"]);
                    txtManager.Enabled = Val.ToBoolean(DRow["ISManager"]);
                    BtnManager.Enabled = Val.ToBoolean(DRow["ISManager"]);

                    RbtGraph.Enabled = Val.ToBoolean(DRow["ISGraph"]);
                    RbtExp.Enabled = Val.ToBoolean(DRow["ISExp"]);

                    DRow = null;
                }

                D = null;

                txtEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;
                BtnEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;


                if (Name == "AP" || Val.ToInt(CmbPrdType.Tag) == 5 || Val.ToInt(CmbPrdType.Tag) == 6)
                {
                    txtEmployee.Enabled = true;
                    BtnEmployee.Enabled = true;
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

                if (Val.ToInt(CmbPrdType.Tag) == 4)
                {
                    chkIsSaveBtnPrint.Visible = true;
                   // btnBarcodePrint.Visible = true;
                }
                else
                {
                    chkIsSaveBtnPrint.Visible = false;
                  //  btnBarcodePrint.Visible = false;
                }

                BtnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle < 0)
            {
                return;
            }

            if (Global.Confirm("Are You Sure For Delete this entry ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            Int64 IntID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("ID"));
            if (IntID != 0)
            {

                //Add : Pinali : 15-09-2019

                DataTable DTabFurtherPrd = ObjRap.ValCheckFurtherPrdExistsOrNotForDelete(IntID, 0, Val.ToInt32(GrdDet.GetFocusedRowCellValue("PLANNO")), Val.ToInt32(CmbPrdType.Tag));

                if (DTabFurtherPrd.Rows.Count > 0)
                {
                    string commaSeparatedString = String.Join("','", DTabFurtherPrd.AsEnumerable().Select(x => x.Field<string>("FULLPACKETNO").ToString()).ToArray());

                    this.Cursor = Cursors.Default;
                    Global.MessageError("You Can't Delete. Because '" + commaSeparatedString + "' <- This Packets Further Prd Entry Is Exists Please Check.");
                    return;
                }

                //End : Pinali : 15-09-2019


                //string StrRes = ObjRap.DeleteRecord(StrID);   //commented : 20-10-2020 : Coz Delete pr Grid mathi delete thay....Update kare to j DB mathi Pcs delete thay..AS Per discuss with client
                //if (StrRes == "SUCCESS")
                //{
                GrdDet.DeleteRow(GrdDet.FocusedRowHandle);
                //}
            }
            else
            {
                GrdDet.DeleteRow(GrdDet.FocusedRowHandle);
            }

            DTabPrediction.AcceptChanges();
            this.Cursor = Cursors.Default;
        }

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

            //#P : 23-02-2021 : Jo Badha Pcs nu RMkbl Padi gyu hase to j Tag Change karva dese Te Validation
            if (e.Clicks == 2 && e.Column.FieldName.ToUpper() == "TAG" && (Val.ToInt32(CmbPrdType.Tag) == 2 || Val.ToInt32(CmbPrdType.Tag) == 10) &&
                BtnSave.Enabled == false && IntOldTFlagPlanNo != 0
                )
            {
                string StrKapanName = Val.ToString(GrdDet.GetFocusedRowCellValue("KAPANNAME"));
                int IntPacketNo = Val.ToInt32(GrdDet.GetFocusedRowCellValue("PACKETNO"));

                Trn_RapSaveProperty Property = new Trn_RapSaveProperty();
                Property.KAPANNAME = txtKapanName.Text;
                Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                Property = ObjRap.ValSaveForUpdateOnlyTag(Property);
                if (Property.ReturnMessageType == "FAIL")
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    return;
                }

                string StrPlanNo = Val.ToString(GrdDet.GetFocusedRowCellValue("PLANNO"));
                DataTable DtabTagSrnoDetail = new DataTable();
                //DtabTagSrnoDetail = DTabPrediction.Select("PlanNo = " + StrPlanNo).CopyToDataTable();
                int IntMinTagSrNo = Val.ToInt32(DTabPrediction.Compute("Min(TagSrNo)", "PlanNo = " + StrPlanNo));
                int IntMaxTagSrNo = Val.ToInt32(DTabPrediction.Compute("Max(TagSrNo)", "PlanNo = " + StrPlanNo));

                //#P : 24-02-2024 : Added Coz When Update button is not enable and All Pcs RmkblPrd is not exists then.... On Double click on tag...display only plan wise tag not all
                try
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "TAG";
                    FrmSearch.mSearchText = "";
                    this.Cursor = Cursors.WaitCursor;
                    //FrmSearch.DTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_SINGLEFINDTAG);
                    DtabTagSrnoDetail = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_SINGLEFINDTAG);
                    FrmSearch.mDTab = DtabTagSrnoDetail.Select("SrNo >= " + Val.ToString(IntMinTagSrNo) + " And SrNo <=" + Val.ToString(IntMaxTagSrNo) + "").CopyToDataTable();

                    FrmSearch.mColumnsToHide = "SRNO";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        string StrID = Val.ToString(GrdDet.GetFocusedRowCellValue("ID"));
                        string StrTag = Val.ToString(GrdDet.GetFocusedRowCellValue("TAG"));
                        string StrTagSrNo = Val.ToString(GrdDet.GetFocusedRowCellValue("TAGSRNO"));

                        string StrNewTag = Val.ToString(FrmSearch.mDRow["TAG"]);
                        string StrNewSrNo = Val.ToString(FrmSearch.mDRow["SRNO"]);

                        //string StrKapanName = Val.ToString(GrdDet.GetFocusedRowCellValue("KAPANNAME"));
                        //int IntPacketNo = Val.ToInt32(GrdDet.GetFocusedRowCellValue("PACKETNO"));

                        foreach (DataRow DRow in DTabPrediction.Rows)
                        {
                            if (Val.ToInt(DRow["PLANNO"]) == Val.ToInt(StrPlanNo) && Val.ToString(DRow["TAG"]) == Val.ToString(FrmSearch.mDRow["TAG"]))
                            {
                                DRow["TAG"] = StrTag;
                                DRow["TAGSRNO"] = StrTagSrNo;

                                //ObjRap.UpdateTag(Val.ToString(DRow["ID"]), StrTag, Val.ToInt(StrTagSrNo));  
                                ObjRap.UpdateTag(Val.ToString(DRow["ID"]), StrTag, Val.ToInt(StrTagSrNo), StrKapanName, Val.ToString(IntPacketNo), StrTag, true);
                                break;
                            }
                        }

                        GrdDet.SetFocusedRowCellValue("TAG", StrNewTag);
                        GrdDet.SetFocusedRowCellValue("TAGSRNO", StrNewSrNo);
                        //ObjRap.UpdateTag(Val.ToString(DRow["ID"]), StrNewTag, Val.ToInt(StrNewSrNo));  //Change : #P : Coz Tag Change thay tyare Flag 0 karavana SalaryProcess na table ma
                        ObjRap.UpdateTag(StrID, StrNewTag, Val.ToInt(StrNewSrNo), StrKapanName, Val.ToString(IntPacketNo), StrTag, true);

                        //  Global.Message("Your Packet Tag Has Been Update");

                        DTabPrediction.AcceptChanges();
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
            //End : #P : 23-02-2021
            else if (e.Clicks == 2 && e.Column.FieldName.ToUpper() == "TAG" && (Val.ToInt32(CmbPrdType.Tag) == 1 || Val.ToInt32(CmbPrdType.Tag) == 2 || Val.ToInt32(CmbPrdType.Tag) == 10) && BtnSave.Enabled == true)
            {
                try
                {

                    bool IsTflagEntry = IntOldTFlagPlanNo != 0 ? true : false;

                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "TAG";
                    FrmSearch.mSearchText = "";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_SINGLEFINDTAG);
                    FrmSearch.mColumnsToHide = "SRNO";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        string StrID = Val.ToString(GrdDet.GetFocusedRowCellValue("ID"));
                        string StrPlanNo = Val.ToString(GrdDet.GetFocusedRowCellValue("PLANNO"));
                        string StrTag = Val.ToString(GrdDet.GetFocusedRowCellValue("TAG"));
                        string StrTagSrNo = Val.ToString(GrdDet.GetFocusedRowCellValue("TAGSRNO"));

                        string StrNewTag = Val.ToString(FrmSearch.mDRow["TAG"]);
                        string StrNewSrNo = Val.ToString(FrmSearch.mDRow["SRNO"]);

                        string StrKapanName = Val.ToString(GrdDet.GetFocusedRowCellValue("KAPANNAME"));
                        int IntPacketNo = Val.ToInt32(GrdDet.GetFocusedRowCellValue("PACKETNO"));


                        DataRow[] DrNewTagExists = DTabPrediction.Select("PlanNo = " + Val.ToString(StrPlanNo) + " And Tag = '" + StrNewTag + "' And Prd_ID <> 0");
                        if (DrNewTagExists.Length <= 0 && Val.ToString(txtPassForDisplayBack.Tag) != Val.ToString(txtPassForDisplayBack.Text))
                        {
                            Global.MessageError("You Can''t Change Tag Coz Selected Tag [" + StrNewTag + "] plan is not exists Or Not Saved...");
                            return;
                        }

                        foreach (DataRow DRow in DTabPrediction.Rows)
                        {
                            if (Val.ToInt(DRow["PLANNO"]) == Val.ToInt(StrPlanNo) && Val.ToString(DRow["TAG"]) == Val.ToString(FrmSearch.mDRow["TAG"]))
                            {
                                DRow["TAG"] = StrTag;
                                DRow["TAGSRNO"] = StrTagSrNo;

                                //ObjRap.UpdateTag(Val.ToString(DRow["ID"]), StrTag, Val.ToInt(StrTagSrNo));  
                                ObjRap.UpdateTag(Val.ToString(DRow["ID"]), StrTag, Val.ToInt(StrTagSrNo), StrKapanName, Val.ToString(IntPacketNo), StrTag, IsTflagEntry);
                                break;
                            }
                        }

                        GrdDet.SetFocusedRowCellValue("TAG", StrNewTag);
                        GrdDet.SetFocusedRowCellValue("TAGSRNO", StrNewSrNo);
                        //ObjRap.UpdateTag(Val.ToString(DRow["ID"]), StrNewTag, Val.ToInt(StrNewSrNo));  //Change : #P : Coz Tag Change thay tyare Flag 0 karavana SalaryProcess na table ma
                        ObjRap.UpdateTag(StrID, StrNewTag, Val.ToInt(StrNewSrNo), StrKapanName, Val.ToString(IntPacketNo), StrTag, IsTflagEntry);

                        //  Global.Message("Your Packet Tag Has Been Update");

                        DTabPrediction.AcceptChanges();
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

            else if (e.Clicks == 2 && e.Column.FieldName.ToUpper() == "DISCOUNT")
            {
                try
                {

                    //DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
                    //DRow.Table.TableName = "ROW";

                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    DRow.Table.TableName = "ROW";

                    DataTable DTabNew = DTabPrediction.Clone();
                    DTabNew.ImportRow(DRow);

                    string StrParameterDetailXmlString = string.Empty;
                    //using (StringWriter sw = new StringWriter())
                    //{
                    //    DRow.Table.WriteXml(sw);
                    //    StrParameterDetailXmlString = sw.ToString();
                    //}
                    using (StringWriter sw = new StringWriter())
                    {
                        DTabNew.WriteXml(sw);
                        StrParameterDetailXmlString = sw.ToString();
                    }


                    StrParameterDetailXmlString = Regex.Replace(StrParameterDetailXmlString,
                                       @"<RAPDATE>(?<year>\d{4})-(?<month>\d{2})-(?<date>\d{2}).*?</RAPDATE>",
                                       @"<RAPDATE>${month}/${date}/${year}</RAPDATE>",
                                       RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                    //string StrXml = Val.ToString(GrdDet.GetFocusedRowCellValue("DROWDISREGULAR"));
                    string StrXml = Val.ToString(DRow["DROWDISREGULAR"]);
                    if (StrXml == "")
                    {
                        return;
                    }
                    StringReader theReader = new StringReader(StrXml);

                    DataSet ds = new DataSet();
                    ds.ReadXml(theReader);

                    DataTable DTab = ds.Tables[0];

                    DataTable DTabDiscountDetail = new DataTable();

                    DTabDiscountDetail.Columns.Add(new DataColumn("KEY", typeof(string)));
                    DTabDiscountDetail.Columns.Add(new DataColumn("VALUE", typeof(string)));

                    if (DTab != null && DTab.Rows.Count != 0)
                    {
                        foreach (DataColumn Col in DTab.Columns)
                        {
                            if (
                                Val.ToString(Col.ColumnName).ToUpper() == "OPE" ||
                                Val.ToString(Col.ColumnName).ToUpper() == "COLOR_ID" ||
                                Val.ToString(Col.ColumnName).ToUpper() == "COLORCODE" ||
                                Val.ToString(Col.ColumnName).ToUpper() == "CLARITY_ID" ||
                                Val.ToString(Col.ColumnName).ToUpper() == "CLARITYCODE"
                                )
                            {
                                continue;
                            }
                            if (Val.ISNumeric(Val.ToString(DTab.Rows[0][Col.ColumnName])) && Val.Val(Val.ToString(DTab.Rows[0][Col.ColumnName])) != 0)
                            {
                                DTabDiscountDetail.Rows.Add(Val.ToString(Col.ColumnName), Val.ToString(DTab.Rows[0][Col.ColumnName]));
                            }

                        }
                    }

                    FrmSearchBoxForRapCalc FrmSearchBoxForRapCalc = new FrmSearchBoxForRapCalc();
                    FrmSearchBoxForRapCalc.SearchField = "";
                    FrmSearchBoxForRapCalc.SearchText = "";
                    FrmSearchBoxForRapCalc.mDiaMin = 0;

                    FrmSearchBoxForRapCalc.mStrParameterDetailXML = StrParameterDetailXmlString;
                    FrmSearchBoxForRapCalc.mDiaMin = Val.Val(txtDiaMin.Text);

                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchBoxForRapCalc.DTab = DTabDiscountDetail;
                    FrmSearchBoxForRapCalc.ColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearchBoxForRapCalc.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchBoxForRapCalc.DRow != null)
                    {

                    }



                    DTabDiscountDetail.Dispose();
                    DTabDiscountDetail = null;

                    DTab.Dispose();
                    DTab = null;

                    ds.Dispose();
                    ds = null;

                    FrmSearchBoxForRapCalc.Hide();
                    FrmSearchBoxForRapCalc.Dispose();
                    FrmSearchBoxForRapCalc = null;
                }
                catch (Exception ex)
                {
                    Global.MessageError(ex.Message);
                }
            }

        }

        private void GrdDet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            mStrType = "SHOWCLICK";
            FetchRecord(e.FocusedRowHandle);
            mStrType = "NOSHOWCLICK";
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
            if (Val.ToString(txtPassForDisplayBack.Tag) != "" && Val.ToString(txtPassForDisplayBack.Tag).ToUpper() == txtPassForDisplayBack.Text.ToUpper())
            {
                lblMakable.Visible = true;
                lblGrading.Visible = true;
                btnBarcodePrint.Visible = true;

                //lblRapDate.Visible = true;
                //CmbRapDate.Visible = true;

                CmbRapDate.Enabled = true;

                //GrdDet.Columns["GIANONGIA"].Visible = true;

                GrdDet.Columns["DISCOUNT"].Visible = true;
                GrdDet.Columns["AMOUNTDISCOUNT"].Visible = true;
                GrdDet.Columns["RAPAPORT"].Visible = true;

                GrdDet.Columns["GDISCOUNT"].Visible = true;
                GrdDet.Columns["GAMOUNTDISCOUNT"].Visible = true;
                GrdDet.Columns["GRAPAPORT"].Visible = true;

                GrdDet.Columns["UPCOLORDISCOUNT"].Visible = true;
                GrdDet.Columns["UPCOLORAMOUNTDISCOUNT"].Visible = true;
                GrdDet.Columns["UPCOLORRAPAPORT"].Visible = true;

                GrdDet.Columns["DOWNCOLORDISCOUNT"].Visible = true;
                GrdDet.Columns["DOWNCOLORAMOUNTDISCOUNT"].Visible = true;
                GrdDet.Columns["DOWNCOLORRAPAPORT"].Visible = true;

                GrdDet.Columns["UPCLARITYDISCOUNT"].Visible = true;
                GrdDet.Columns["UPCLARITYAMOUNTDISCOUNT"].Visible = true;
                GrdDet.Columns["UPCLARITYRAPAPORT"].Visible = true;

                GrdDet.Columns["DOWNCLARITYDISCOUNT"].Visible = true;
                GrdDet.Columns["DOWNCLARITYAMOUNTDISCOUNT"].Visible = true;
                GrdDet.Columns["DOWNCLARITYRAPAPORT"].Visible = true;
                BtnPlannerView.Visible = true;
            }
            else
            {
                lblMakable.Visible = false;
                lblGrading.Visible = false;
                btnBarcodePrint.Visible = false;
                //lblRapDate.Visible = false;
                //CmbRapDate.Visible = false;

                CmbRapDate.Enabled = false;

                //GrdDet.Columns["GIANONGIA"].Visible = false;

                GrdDet.Columns["DISCOUNT"].Visible = false;
                GrdDet.Columns["AMOUNTDISCOUNT"].Visible = false;
                GrdDet.Columns["RAPAPORT"].Visible = false;

                GrdDet.Columns["GDISCOUNT"].Visible = false;
                GrdDet.Columns["GAMOUNTDISCOUNT"].Visible = false;
                GrdDet.Columns["GRAPAPORT"].Visible = false;

                GrdDet.Columns["UPCOLORDISCOUNT"].Visible = false;
                GrdDet.Columns["UPCOLORAMOUNTDISCOUNT"].Visible = false;
                GrdDet.Columns["UPCOLORRAPAPORT"].Visible = false;

                GrdDet.Columns["DOWNCOLORDISCOUNT"].Visible = false;
                GrdDet.Columns["DOWNCOLORAMOUNTDISCOUNT"].Visible = false;
                GrdDet.Columns["DOWNCOLORRAPAPORT"].Visible = false;

                GrdDet.Columns["UPCLARITYDISCOUNT"].Visible = false;
                GrdDet.Columns["UPCLARITYAMOUNTDISCOUNT"].Visible = false;
                GrdDet.Columns["UPCLARITYRAPAPORT"].Visible = false;

                GrdDet.Columns["DOWNCLARITYDISCOUNT"].Visible = false;
                GrdDet.Columns["DOWNCLARITYAMOUNTDISCOUNT"].Visible = false;
                GrdDet.Columns["DOWNCLARITYRAPAPORT"].Visible = false;
                BtnPlannerView.Visible = false;
            }

        }

        private void lblMakable_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("Are You Sure To Delete Makable Entry ?") == System.Windows.Forms.DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                int IntRes = ObjRap.DeleteMakable(Val.ToString(txtTag.Tag));
                this.Cursor = Cursors.Default;
                if (IntRes != 0)
                {
                    if (Val.ToInt(CmbPrdType.Tag) == 4)
                    {
                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            string StrID = Val.ToString(GrdDet.GetRowCellValue(IntI, "ID"));
                            if (StrID != "")
                            {
                                string StrRes = ObjRap.DeleteRecord(StrID);
                                if (StrRes == "SUCCESS")
                                {
                                    GrdDet.DeleteRow(IntI);
                                }

                            }
                            else
                            {
                                GrdDet.DeleteRow(IntI);
                            }
                        }
                    }

                    DTabPrediction.AcceptChanges();
                    this.Cursor = Cursors.Default;

                    Global.Message("Successfully Delete Makable , Now This Packet Converted Into Rough");
                }
            }
        }

        private void lblGrading_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("Are You Sure To Delete Grading Entry ?") == System.Windows.Forms.DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                int IntRes = ObjRap.DeleteGrading(Val.ToString(txtTag.Tag));
                this.Cursor = Cursors.Default;
                if (IntRes != 0)
                {
                    if (Val.ToInt(CmbPrdType.Tag) == 8)
                    {
                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            string StrID = Val.ToString(GrdDet.GetRowCellValue(IntI, "ID"));
                            if (StrID != "")
                            {
                                string StrRes = ObjRap.DeleteRecord(StrID);
                                if (StrRes == "SUCCESS")
                                {
                                    GrdDet.DeleteRow(IntI);
                                }

                            }
                            else
                            {
                                GrdDet.DeleteRow(IntI);
                            }
                        }
                    }

                    DTabPrediction.AcceptChanges();
                    this.Cursor = Cursors.Default;

                    Global.Message("Successfully Delete Grading , Now This Packet Converted Into Makable");
                }
            }
        }

        private void BtnFetchPrevPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (ISClickOnShowButton == false)
                {
                    Global.Message("Click On Show Button First...");
                    BtnContinue.Focus();
                    return;
                }

                FrmFetchPreviousPlan FrmFetchPreviousPlan = new FrmFetchPreviousPlan();

                //#P
                Int64 IntEmployee_ID = 0;
                IntEmployee_ID = Val.ToInt(CmbPrdType.Tag) == 10 ? Val.ToInt64(txtEmployee.Tag) : 0;


                FrmFetchPreviousPlan.ShowForm(Val.ToInt32(CmbPrdType.Tag), txtKapanName.Text, Val.ToInt32(txtPacketNo.Text), txtTag.Text, Val.ToString(txtTag.Tag), IntEmployee_ID);

                if (FrmFetchPreviousPlan.DTabSelected != null && FrmFetchPreviousPlan.DTabSelected.Rows.Count != 0)
                {
                    DTabPrediction.Rows.Clear();

                    this.Cursor = Cursors.WaitCursor;

                    foreach (DataRow DR in FrmFetchPreviousPlan.DTabSelected.Rows)
                    {
                        DataRow DRow = DTabPrediction.NewRow();
                        DRow["PLANNO"] = DR["PLANNO"];
                        //DRow["TAGSRNO"] = DR["TAGSRNO"];  //Chnged : #P : 13-02-2020
                        DRow["TAGSRNO"] = txtTag.Enabled == true && txtTag.Text.Trim().Length != 0 ? -1 : DR["TAGSRNO"];
                        DRow["RAPDATE"] = Val.ToString(CmbRapDate.SelectedItem);
                        DRow["TAG"] = DR["TAG"];
                        DRow["ID"] = 0;
                        DRow["PRD_ID"] = 0;

                        DRow["COPYFROMEMPLOYEE_ID"] = DR["EMPLOYEE_ID"];
                        DRow["COPYFROMPRD_ID"] = DR["PRD_ID"];
                        DRow["COPYFROM_ID"] = DR["ID"];


                        foreach (DataColumn Col in DR.Table.Columns)
                        {
                            if (Col.ColumnName.ToString().ToUpper() == "PLANNO" ||
                                Col.ColumnName.ToString().ToUpper() == "TAGSRNO" ||
                                Col.ColumnName.ToString().ToUpper() == "TAG" ||
                                Col.ColumnName.ToString().ToUpper() == "RAPDATE" ||
                                Col.ColumnName.ToString().ToUpper() == "ID" ||
                                Col.ColumnName.ToString().ToUpper() == "PRD_ID" ||
                                Col.ColumnName.ToString().ToUpper() == "COPYFROMEMPLOYEE_ID" ||
                                Col.ColumnName.ToString().ToUpper() == "COPYFROMPRD_ID" ||
                                Col.ColumnName.ToString().ToUpper() == "COPYFROM_ID"
                                )
                            {
                                continue;
                            }

                            if (DTabPrediction.Columns.Contains(Col.ColumnName))
                            {
                                try
                                {
                                    if (Col.ColumnName.ToString().ToUpper().Contains("ENTRYDATE"))
                                        continue;
                                    DRow[Col.ColumnName] = DR[Col.ColumnName];
                                }
                                catch
                                {

                                }
                            }
                        }

                        DTabPrediction.Rows.Add(DRow);

                    }

                    if (Val.ToInt(CmbPrdType.Tag) == 3 || Val.ToInt(CmbPrdType.Tag) == 8) //#P : 07-01-2020 : Coz Popup Ownership Prd on RoughMkbl with editable
                    {
                        PanelCarat.Enabled = true;
                        PanelParameter.Enabled = true;
                    }
                    else
                    {
                        PanelCarat.Enabled = false;
                        PanelParameter.Enabled = false;
                    }

                    GrdDet.ExpandAllGroups();
                    GrdDet.RefreshData();
                    GrdDet.MoveLast();
                    //GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    //GrdDet.SelectRow(GrdDet.FocusedRowHandle);


                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.ToString());
            }
        }

        private void ChkAllowForUpdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure TO Give Him One Time Prediction Update = [" + ChkAllowForUpdate.Checked.ToString() + "] Permission ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    int IntRes = 0;
                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        DataRow DRow = GrdDet.GetDataRow(IntI);

                        if (DRow == null || (Val.Val(DRow["AMOUNT"]) == 0 && Val.Val(DRow["CARAT"]) == 0))
                        {
                            continue;
                        }

                        IntRes += ObjRap.AllowForUpdate(Val.ToString(DRow["ID"]), ChkAllowForUpdate.Checked == true ? 1 : 0);
                    }
                    this.Cursor = Cursors.Default;
                    Global.Message("Successfully Allowed For Next One Time Update");
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }

        }

        private void ChkDiff_CheckedChanged(object sender, EventArgs e)
        {
            PanelCarat.Enabled = ChkDiff.Checked;
            PanelParameter.Enabled = ChkDiff.Checked;

        }

        private void BtnFacetCalc_Click(object sender, EventArgs e)
        {
            //AxonFacetWareData.FrmFacetCalculator FrmFacetCalculator = new AxonFacetWareData.FrmFacetCalculator();
            //FrmFacetCalculator.ShowForm(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME, BusLib.Configuration.BOConfiguration.gEmployeeProperty.PASSWORD, BusLib.Configuration.BOConfiguration.ComputerIP);
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow DRow = GrdDet.GetFocusedDataRow();

            Int32 IntPlanNo = Val.ToInt32(DRow["PLANNO"]);

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DD = GrdDet.GetDataRow(IntI);
                if (DD != null && Val.ToString(DD["PLANNO"]) == Val.ToString(DRow["PLANNO"]))
                {
                    GrdDet.SetRowCellValue(IntI, "ISFINAL", true);
                }
                else if (DD != null && Val.ToString(DD["PLANNO"]) == Val.ToString(DRow["PLANNO"]) && Val.ToBoolean(DD["ISFINAL"]) == true)
                {
                    GrdDet.SetRowCellValue(IntI, "ISFINAL", false);
                }
                else
                {
                    GrdDet.SetRowCellValue(IntI, "ISFINAL", false);
                }

            }
        }

        private void GrdDet_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                GrdDet.MakeRowVisible(e.RowHandle, true);
                return;
            }

            int PlanNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "PLANNO"));
            bool BoolFinal = Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "ISFINAL"));
            bool IsImport = Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "ISEXCELIMPORT"));
            //if (BoolFinal == false)
            //{
            //    if (PlanNo % 2 == 0)
            //    {
            //        e.Appearance.BackColor = Color.FromArgb(230, 230, 230);
            //    }
            //    else
            //    {
            //        e.Appearance.BackColor = Color.Transparent;
            //    }  
            //}
            //else
            if (BoolFinal == true)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 224, 220);
            }

            if (IsImport == true)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }

        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                {

                }

                int PlanNo = Val.ToInt(GrdDet.GetFocusedRowCellValue("PLANNO"));

                if (Val.ToInt64(GrdDet.GetFocusedRowCellValue("PRD_ID")) == 0)
                    return;

                if (Global.Confirm("Are You Sure To Delete Full Plan [ " + PlanNo.ToString() + " ] ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;

                    // Add : Pinali : 15-09-2019

                    DataTable DTabFurtherPrd = ObjRap.ValCheckFurtherPrdExistsOrNotForDelete(0, Val.ToInt64(GrdDet.GetFocusedRowCellValue("PRD_ID")), PlanNo, Val.ToInt32(CmbPrdType.Tag));

                    if (DTabFurtherPrd.Rows.Count > 0)
                    {
                        string commaSeparatedString = String.Join("','", DTabFurtherPrd.AsEnumerable().Select(x => x.Field<string>("FULLPACKETNO").ToString()).ToArray());

                        this.Cursor = Cursors.Default;
                        Global.MessageError("You Can't Delete. Because '" + commaSeparatedString + "' <- This Packets Further Prd Entry Is Exists Please Check.");
                        return;
                    }



                    //End  : Pinali : 15-09-2019


                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        DataRow DRow = GrdDet.GetDataRow(IntI);

                        if (DRow != null && (Val.ToInt(DRow["PLANNO"]) == PlanNo))
                        {
                            string s = ObjRap.DeleteRecord(Val.ToString(DRow["ID"]));
                            //   GrdDet.DeleteRow(IntI);
                        }
                    }


                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        DataRow DRow = GrdDet.GetDataRow(IntI);

                        if (DRow != null && (Val.ToInt(DRow["PLANNO"]) == PlanNo))
                        {
                            GrdDet.DeleteRow(IntI);
                        }
                    }

                    DTabPrediction.AcceptChanges();

                    this.Cursor = Cursors.Default;
                    Global.Message("Successfully Deleteed Full Plan [ " + PlanNo.ToString() + " ] ?");

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }

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

        private void txtDiaMin_TextChanged(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle >= 0 && GrdDet.SelectedRowsCount >= 0)
            {
                GrdDet.SetFocusedRowCellValue("DIAMIN", Val.Val(txtDiaMin.Text));
                FindRap();
            }
        }

        private void txtDiaMax_TextChanged(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle >= 0 && GrdDet.SelectedRowsCount >= 0)
            {
                GrdDet.SetFocusedRowCellValue("DIAMAX", Val.Val(txtDiaMax.Text));
                FindRap();
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle >= 0 && GrdDet.SelectedRowsCount >= 0)
            {
                GrdDet.SetFocusedRowCellValue("HEIGHT", Val.Val(txtHeight.Text));
                FindRap();
            }
        }

        private void CmbLabSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Val.ToString(CmbLabSelection.SelectedItem) == "IGI")
            {
                Fetch_SetComboBox(CmbLab, DataLAB, 231);
                FindRap();
            }

        }

        private void GrdDet_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void RepCheckForSum_EditValueChanged(object sender, EventArgs e)
        {
            GrdDet.PostEditor();
            FindDiffAmountWithFirstRow();
        }

        private void BtnPlannerView_Click(object sender, EventArgs e)
        {
            FrmPrdViewMarkerSimple FrmPrdViewMarkerSimple = new FrmPrdViewMarkerSimple();
            FrmPrdViewMarkerSimple.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmPrdViewMarkerSimple);
            FrmPrdViewMarkerSimple.ShowForm(txtKapanName.Text, txtPacketNo.Text, txtTag.Text);
        }

        private void lblSaveLayoutGrd_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdDet.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDet.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblDeleteLayoutGrid_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDet.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void CmbLab_Validating(object sender, CancelEventArgs e)
        {
            /*
            try
            {
                if (Val.ToString(CmbLab.Text) == "ORDER")
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();

                    Trn_RapSaveProperty PropertySaleDemand = new Trn_RapSaveProperty();
                    PropertySaleDemand.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                    PropertySaleDemand.KAPANNAME = Val.ToString(txtKapanName.Text);
                    PropertySaleDemand.PACKETNO = Val.ToInt(txtPacketNo.Text);
                    PropertySaleDemand.TAG = txtTag.Text;
                    PropertySaleDemand.SHAPE_ID = Val.ToInt32(DRow["SHAPE_ID"]);
                    PropertySaleDemand.COLOR_ID = Val.ToInt32(DRow["COLOR_ID"]);
                    PropertySaleDemand.CLARITY_ID = Val.ToInt32(DRow["CLARITY_ID"]);
                    PropertySaleDemand.CUT_ID = Val.ToInt32(DRow["CUT_ID"]);
                    PropertySaleDemand.FL_ID = Val.ToInt32(DRow["FL_ID"]);
                    PropertySaleDemand.LAB_ID = Val.ToInt32(DRow["LAB_ID"]);
                    PropertySaleDemand.CARAT = Val.Val(DRow["CARAT"]);
                    if (Val.ISDate(Val.ToString(DRow["ENTRYDATE"]))) //&& ISFinalFlagChanged == false
                    {
                        PropertySaleDemand.ENTRYDATE = txtTag.Enabled == false ? DateTime.Parse(Val.ToString(mStrEntryDate)).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Parse(Val.ToString(DRow["ENTRYDATE"])).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        PropertySaleDemand.ENTRYDATE = txtTag.Enabled == false && Val.ToString(mStrEntryDate) != "" ? DateTime.Parse(Val.ToString(mStrEntryDate)).ToString("yyyy-MM-dd HH:mm:ss") : null;
                    }
                    DataTable DtabSaleDemandQty = ObjRap.ValSaveRapCalcForSaleDemand(PropertySaleDemand, "FORREMARK");
                    if (DtabSaleDemandQty.Rows.Count > 0)
                    {
                        Global.Message("Note : " + Val.ToString(DtabSaleDemandQty.Rows[0]["REMARK"]) + "");
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
             * */
        }

        private void CmbLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                CmbMilky_SelectedIndexChanged(null, null);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message.ToString());
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable DTABEXCEL = ObjRap.GetDataForExcelExport(Val.ToInt32(CmbPrdType.Tag), Val.ToString(txtTag.Tag), Val.ToInt64(txtEmployee.Tag), Val.ToInt64(txtManager.Tag));
                DataTable DTABEXCEL = new DataTable();
                DTABEXCEL.Columns.Add("Kapan", typeof(string));
                DTABEXCEL.Columns.Add("PlNo", typeof(string));
                DTABEXCEL.Columns.Add("PktNo", typeof(string));
                DTABEXCEL.Columns.Add("Tag", typeof(string));
                DTABEXCEL.Columns.Add("ECode", typeof(string));
                DTABEXCEL.Columns.Add("Carat", typeof(string));
                DTABEXCEL.Columns.Add("Shp", typeof(string));
                DTABEXCEL.Columns.Add("Col", typeof(string));
                DTABEXCEL.Columns.Add("Cla", typeof(string));
                DTABEXCEL.Columns.Add("Cut", typeof(string));
                DTABEXCEL.Columns.Add("Pol", typeof(string));
                DTABEXCEL.Columns.Add("Sym", typeof(string));
                DTABEXCEL.Columns.Add("FL", typeof(string));
                DTABEXCEL.Columns.Add("Lab", typeof(string));
                DTABEXCEL.Columns.Add("Rap", typeof(double));
                DTABEXCEL.Columns.Add("Dis", typeof(double));

                foreach (DataRow DR in DTabPrediction.Rows)
                {
                    DataRow DRNew = DTABEXCEL.NewRow();

                    DRNew["Kapan"] = txtKapanName.Text;
                    DRNew["PlNo"] = DR["PLANNO"];
                    DRNew["PktNo"] = txtPacketNo.Text;
                    DRNew["Tag"] = DR["TAG"];
                    DRNew["ECode"] = txtEmployee.Text;
                    DRNew["Carat"] = DR["CARAT"];
                    DRNew["Shp"] = DR["SHAPECODE"];
                    DRNew["Col"] = DR["COLORNAME"];
                    DRNew["Cla"] = DR["CLARITYNAME"];
                    DRNew["Cut"] = DR["CUTCODE"];
                    DRNew["Pol"] = DR["POLCODE"];
                    DRNew["Sym"] = DR["SYMCODE"];
                    DRNew["FL"] = DR["FLNAME"];
                    DRNew["Lab"] = DR["LABCODE"];
                    DRNew["Rap"] = DR["RAPAPORT"];
                    DRNew["Dis"] = DR["DISCOUNT"];

                    DTABEXCEL.Rows.Add(DRNew);
                }


                object misValue = System.Reflection.Missing.Value;
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "RapCalcData.xlsx";
                svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";





                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string StrFilePath = svDialog.FileName;

                    if (File.Exists(StrFilePath))
                    {
                        File.Delete(StrFilePath);
                    }

                    FileInfo workBook = new FileInfo(StrFilePath);
                    Color BackColor = Color.LightGray;
                    Color FontColor = Color.Black;
                    string FontName = "Verdana";
                    float FontSize = 8;

                    int StartRow = 0;
                    int StartColumn = 0;
                    int EndRow = 0;
                    int EndColumn = 0;


                    int intEndMCts = 0;
                    int intEndMAmt = 0;
                    int intEndMTotal = 0;


                    using (ExcelPackage xlPackage = new ExcelPackage(workBook))
                    {
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Result_" + DateTime.Now.ToString("ddMMyyyy"));

                        StartRow = 1;
                        EndRow = StartRow + DTABEXCEL.Rows.Count;
                        StartColumn = 1;
                        EndColumn = DTABEXCEL.Columns.Count;


                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].LoadFromDataTable(DTABEXCEL, true);
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                        //worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        //worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        //worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        //worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Name = FontName;
                        worksheet.Cells[StartRow, StartColumn, EndRow, EndColumn].Style.Font.Size = FontSize;

                        worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[StartRow, StartColumn, StartRow, EndColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.PatternColor.SetColor(BackColor);
                        worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Fill.BackgroundColor.SetColor(BackColor);
                        worksheet.Cells[StartRow, 1, StartRow, EndColumn].Style.Font.Color.SetColor(FontColor);


                        worksheet.Cells["A:Z"].AutoFitColumns();


                        using (ExcelRange Range = worksheet.Cells[1, StartColumn, 1, EndColumn])
                        {
                            Range.Style.Font.Bold = true;
                        }

                        intEndMCts = DTABEXCEL.Columns.Count + 1;
                        intEndMAmt = DTABEXCEL.Columns.Count + 2;
                        intEndMTotal = DTABEXCEL.Columns.Count + 3;

                        worksheet.Cells[1, intEndMCts].Value = "$/Cts";
                        worksheet.Cells[1, intEndMAmt].Value = "$/Amt";
                        worksheet.Cells[1, intEndMTotal].Value = "Total";

                        using (ExcelRange Range = worksheet.Cells[1, intEndMCts])
                        {
                            Range.Style.Font.Bold = true;
                        }
                        using (ExcelRange Range = worksheet.Cells[1, intEndMAmt])
                        {
                            Range.Style.Font.Bold = true;
                        }
                        using (ExcelRange Range = worksheet.Cells[1, intEndMTotal])
                        {
                            Range.Style.Font.Bold = true;
                        }
                        //CARAT

                        worksheet.Cells[1, intEndMCts].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[1, intEndMCts].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[1, intEndMCts].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


                        // worksheet.Cells[1, intEndMCts].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        // worksheet.Cells[1, intEndMCts].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        // worksheet.Cells[1, intEndMCts].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        // worksheet.Cells[1, intEndMCts].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMCts].Style.Font.Name = FontName;
                        worksheet.Cells[1, intEndMCts].Style.Font.Size = FontSize;

                        worksheet.Cells[1, intEndMCts].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMCts].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMCts].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMCts].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        worksheet.Cells[1, intEndMCts].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[1, intEndMCts].Style.Fill.PatternColor.SetColor(BackColor);
                        worksheet.Cells[1, intEndMCts].Style.Fill.BackgroundColor.SetColor(BackColor);
                        worksheet.Cells[1, intEndMCts].Style.Font.Color.SetColor(FontColor);

                        //AMOUNT
                        worksheet.Cells[1, intEndMAmt].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[1, intEndMAmt].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[1, intEndMAmt].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                        //worksheet.Cells[1, intEndMAmt].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        //worksheet.Cells[1, intEndMAmt].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        //worksheet.Cells[1, intEndMAmt].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        //worksheet.Cells[1, intEndMAmt].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMAmt].Style.Font.Name = FontName;
                        worksheet.Cells[1, intEndMAmt].Style.Font.Size = FontSize;

                        worksheet.Cells[1, intEndMAmt].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMAmt].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMAmt].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMAmt].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        worksheet.Cells[1, intEndMAmt].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[1, intEndMAmt].Style.Fill.PatternColor.SetColor(BackColor);
                        worksheet.Cells[1, intEndMAmt].Style.Fill.BackgroundColor.SetColor(BackColor);
                        worksheet.Cells[1, intEndMAmt].Style.Font.Color.SetColor(FontColor);


                        //MTotal
                        worksheet.Cells[1, intEndMTotal].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[1, intEndMTotal].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[1, intEndMTotal].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[1, intEndMTotal].Style.Font.Name = FontName;
                        worksheet.Cells[1, intEndMTotal].Style.Font.Size = FontSize;
                        worksheet.Cells[1, intEndMTotal].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMTotal].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMTotal].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMTotal].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, intEndMTotal].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[1, intEndMTotal].Style.Fill.PatternColor.SetColor(BackColor);
                        worksheet.Cells[1, intEndMTotal].Style.Fill.BackgroundColor.SetColor(BackColor);
                        worksheet.Cells[1, intEndMTotal].Style.Font.Color.SetColor(FontColor);

                        // worksheet.Cells[1,EndColumn].AutoFitColumns();
                        int nCarat = 0, nRap = 0, nMDisc = 0;
                        string StrRapLetter, StrCaratLetter, StrMDisLetter, StrMDollPerCaratLetter, StrTotalLetter, StrMAmtLetterr;

                        string StrPrevPrdType = "", StrPrevKapan = "", StrPrevPckt = "", StrPrevEcode = "", StrPrevPI = "";
                        string StrCurrPrdType = "", StrCurrKapan = "", StrCurrPckt = "", StrCurrEcode = "", StrCurrPI = "";

                        int IntStart = 2, IntEnd = 2;

                        DataColumnCollection columns = DTABEXCEL.Columns;
                        if (columns.Contains("Rap"))
                            nRap = 15;
                        if (columns.Contains("Carat"))
                            nCarat = 6;
                        if (columns.Contains("Dis"))
                            nMDisc = 16;

                        StrRapLetter = Global.ColumnIndexToColumnLetter(nRap);
                        StrCaratLetter = Global.ColumnIndexToColumnLetter(nCarat);
                        StrMDisLetter = Global.ColumnIndexToColumnLetter(nMDisc);
                        StrMDollPerCaratLetter = Global.ColumnIndexToColumnLetter(intEndMCts);
                        StrTotalLetter = Global.ColumnIndexToColumnLetter(18);
                        StrMAmtLetterr = Global.ColumnIndexToColumnLetter(intEndMAmt);


                        for (int i = 0; i < DTABEXCEL.Rows.Count; i++)
                        {
                            int A = i;

                            A = A + 2;

                            worksheet.Cells[A, intEndMCts].Formula = "=" + StrRapLetter + "" + Val.ToString(A) + "-" + StrRapLetter + "" + Val.ToString(A) + "*" + StrMDisLetter + "" + Val.ToString(A) + "%";
                            worksheet.Cells[A, intEndMAmt].Formula = "=" + StrMDollPerCaratLetter + "" + Val.ToString(A) + "*" + StrCaratLetter + "" + Val.ToString(A) + "";

                            worksheet.Cells[A, intEndMAmt].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            worksheet.Cells[A, intEndMAmt].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                            worksheet.Cells[A, intEndMCts].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            worksheet.Cells[A, intEndMCts].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                            if (i == 0)
                            {

                                StrPrevKapan = Val.ToString(DTABEXCEL.Rows[i]["KAPAN"]);
                                StrCurrKapan = Val.ToString(DTABEXCEL.Rows[i]["KAPAN"]);
                                StrPrevPckt = Val.ToString(DTABEXCEL.Rows[i]["PKTNo"]);
                                StrCurrPckt = Val.ToString(DTABEXCEL.Rows[i]["PKTNo"]);
                                StrPrevEcode = Val.ToString(DTABEXCEL.Rows[i]["ECODE"]);
                                StrCurrEcode = Val.ToString(DTABEXCEL.Rows[i]["ECODE"]);
                                StrPrevPI = Val.ToString(DTABEXCEL.Rows[i]["PLNo"]);
                                StrCurrPI = Val.ToString(DTABEXCEL.Rows[i]["PLNo"]);
                            }
                            else
                            {
                                StrCurrKapan = Val.ToString(DTABEXCEL.Rows[i]["KAPAN"]);
                                StrCurrPckt = Val.ToString(DTABEXCEL.Rows[i]["PKTNo"]);
                                StrCurrEcode = Val.ToString(DTABEXCEL.Rows[i]["ECODE"]);
                                StrCurrPI = Val.ToString(DTABEXCEL.Rows[i]["PLNo"]);
                            }


                            if (StrPrevPrdType != StrCurrPrdType || StrPrevKapan != StrCurrKapan || StrPrevPckt != StrCurrPckt || StrPrevEcode != StrCurrEcode || StrPrevPI != StrCurrPI)
                            {
                                StrPrevKapan = Val.ToString(DTABEXCEL.Rows[i]["KAPAN"]);
                                StrPrevPckt = Val.ToString(DTABEXCEL.Rows[i]["PKTNo"]);
                                StrPrevEcode = Val.ToString(DTABEXCEL.Rows[i]["ECODE"]);
                                StrPrevPI = Val.ToString(DTABEXCEL.Rows[i]["PLNo"]);

                                worksheet.Cells[IntStart, 2, IntEnd, 2].Merge = true;

                                worksheet.Cells[IntStart, 19, IntEnd, 19].Formula = "=SUM(" + StrTotalLetter + "" + Val.ToString(IntStart) + ":" + StrTotalLetter + "" + Val.ToString(IntEnd) + ")";
                                worksheet.Cells[IntStart, 19, IntEnd, 19].Merge = true;

                                worksheet.Cells[IntStart, 19, IntEnd, 19].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells[IntStart, 19, IntEnd, 19].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                                worksheet.Cells[IntStart, 19, IntEnd, 19].Style.Font.Name = FontName;
                                worksheet.Cells[IntStart, 19, IntEnd, 19].Style.Font.Size = FontSize;

                                worksheet.Cells[IntStart, 17, IntEnd, 17].Style.Font.Name = FontName;
                                worksheet.Cells[IntStart, 17, IntEnd, 17].Style.Font.Size = FontSize;

                                worksheet.Cells[IntStart, 18, IntEnd, 18].Style.Font.Name = FontName;
                                worksheet.Cells[IntStart, 18, IntEnd, 18].Style.Font.Size = FontSize;

                                IntStart = i + 2;
                                IntEnd = i + 2;
                            }
                            else
                            {
                                IntEnd = i + 2;
                            }
                        }
                        worksheet.Cells[IntStart, 2, IntEnd, 2].Merge = true;

                        worksheet.Cells[IntStart, 19, IntEnd, 19].Formula = "=SUM(" + StrTotalLetter + "" + Val.ToString(IntStart) + ":" + StrTotalLetter + "" + Val.ToString(IntEnd) + ")";
                        worksheet.Cells[IntStart, 19, IntEnd, 19].Merge = true;

                        worksheet.Cells[IntStart, 19, IntEnd, 19].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[IntStart, 19, IntEnd, 19].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        worksheet.Cells[IntStart, 19, IntEnd, 19].Style.Font.Name = FontName;
                        worksheet.Cells[IntStart, 19, IntEnd, 19].Style.Font.Size = FontSize;

                        worksheet.Cells[IntStart, 17, IntEnd, 17].Style.Font.Name = FontName;
                        worksheet.Cells[IntStart, 17, IntEnd, 17].Style.Font.Size = FontSize;

                        worksheet.Cells[IntStart, 18, IntEnd, 18].Style.Font.Name = FontName;
                        worksheet.Cells[IntStart, 18, IntEnd, 18].Style.Font.Size = FontSize;

                        xlPackage.Save();

                        if (Global.Confirm("Do You Want To Open [RapCalcData.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                        }
                    }

                    svDialog.Dispose();
                    svDialog = null;
                }
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void BtnFileTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                FrmFileTransfer FrmFileTransfer = new FrmFileTransfer();
                //FrmFileTransfer.MdiParent = this;
                FrmFileTransfer.ShowForm();
                //FrmFileTransfer.ShowDialog();

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSave.Text = "&Save";

                DataTable DtabExcelData = new DataTable();
                this.Cursor = Cursors.WaitCursor;
                //DtabPara = new BOMST_Parameter().GetParameterData();
                DtabExcelData.Rows.Clear();


                /*//If Manual Select File : then Uncomment this line : #P : 19-02-2022
               GetPopup();
               string filename = Path.GetFileName(StrPath);
               string extension = Path.GetExtension(StrPath.ToString());
               string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(StrPath);

               if (extension.ToUpper().Contains("XLSX"))
               {
                   destinationPath = destinationPath.Replace(extension, ".xlsx");
                   if (File.Exists(destinationPath))
                   {
                       File.Delete(destinationPath);
                   }
                   File.Copy(StrPath, destinationPath);
               }
               else if (Path.GetExtension(destinationPath).ToUpper().Contains("TXT"))
               {
                   if (File.Exists(destinationPath))
                   {
                       File.Delete(destinationPath);
                   }
                   File.Copy(StrPath, destinationPath);

                   DtabExcelData = Global.GetDataTableFromTxt(destinationPath);
               }
               //DtabExcelData = Global.ImportExcelXLSWithSheetName(destinationPath, true, CmbSheetname.SelectedItem.ToString(), 1);
               //CmbSheetname.Items.Clear();
               */

                //Get Data From Sharing Folder Path : #P : 19-02-2022


                //End : #P : 19-02-2022

                if (RbtBarcode.Checked || RbtPktSerialNo.Checked)
                {
                    mStrKapanName = RbtPktSerialNo.Checked ? Val.ToString(txtSrNoKapanName.Text) : Val.ToString(txtKapanName.Text);
                    DataRow DRPkt = ObjRap.GetPacketDataRow(mStrKapanName, Val.ToInt32(txtPacketNo.Text), Val.ToString(txtTag.Text), Val.ToString(txtBarcode.Text), Val.ToString(txtSrNoKapanName.Text), Val.ToInt32(txtSrNoSerialNo.Text));
                    if (DRPkt == null)
                    {
                        Global.MessageError("Ooops.. Packet Is Not Found");
                        return;
                    }
                    txtTag.Tag = Val.ToString(DRPkt["PACKET_ID"]);
                    lblLot.Text = Val.ToString(DRPkt["LOTCARAT"]);
                    lblBalance.Text = Val.ToString(DRPkt["BALANCECARAT"]);
                    lblBalance.Tag = Val.ToString(DRPkt["TRN_ID"]);

                    txtKapanName.Text = Val.ToString(DRPkt["KAPANNAME"]);
                    txtPacketNo.Text = Val.ToString(DRPkt["PACKETNO"]);
                    txtTag.Text = txtTag.Enabled ? Val.ToString(DRPkt["TAG"]) : string.Empty;
                }

                //#P :  21-02-2022 : Get File From Sharing Path
                string StrKapanPacket = txtKapanName.Text + "-" + txtPacketNo.Text + "-" + "AA";
                string SourcePath = "";
                SourcePath = mStrSharingFilePath + "\\" + StrKapanPacket + ".txt";
                string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(SourcePath);
                //using (new BOSharingLockUnlock(mStrServerPath, mStrServerUserName, mStrServerPassword))

                using (new SharingLockUnlock(mStrSharingFilePath, mStrSharingFileUserName, mStrSharingFilePassword))
                {
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    File.Copy(SourcePath, destinationPath);

                    if (Path.GetExtension(destinationPath).ToUpper().Contains("TXT"))
                    {
                        DtabExcelData = Global.GetDataTableFromTxt(destinationPath);
                    }
                }
                //End : #P : 21-02-2022





                if (DtabExcelData.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                foreach (DataColumn c in DtabExcelData.Columns)
                {
                    c.ColumnName = String.Join("", c.ColumnName.Split());
                    c.ColumnName = c.ColumnName.Replace("(%)", "Per");
                    c.ColumnName = c.ColumnName.Replace("(MM)", "MM");
                }


                DTabPrediction.Rows.Clear();

                string StrXmlExcel = string.Empty;
                DtabExcelData.TableName = "Table";
                using (StringWriter sw = new StringWriter())
                {
                    DtabExcelData.WriteXml(sw);
                    StrXmlExcel = sw.ToString();
                }


                DTabPrediction = ObjRap.ImportExcelForRapCalc(StrXmlExcel);

                for (int i = 0; i < DTabPrediction.Rows.Count; i++)
                {
                    DataRow DR = DTabPrediction.Rows[i];
                    string StrKapanPkt = Val.ToString(txtKapanName.Text);
                    if (StrKapanPkt != Val.ToString(DR["KAPANNAME"]))
                    {
                        Global.Message(StrKapanPkt + "KAPAN NAME MISS-MATCH PLEASE CHECK KAPANNAME..");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }

                //  txtDiaMin.Text = Val.ToString(DTabPrediction.Rows[0]["DIAMIN"]);
                //  txtDiaMax.Text = Val.ToString(DTabPrediction.Rows[0]["DIAMAX"]);
                //  txtHeight.Text = Val.ToString(DTabPrediction.Rows[0]["HEIGHT"]);

                MainGrid.DataSource = DTabPrediction;
                MainGrid.RefreshDataSource();


                BtnSave.Enabled = true;

                GrdDet.ExpandAllGroups();


                string StrPath2 = Val.ToString((Application.StartupPath + "\\Excel-Imported\\")); //@"E:\Milan Work\OM\AxoneMFGRJ\AxoneMFGRJ\bin\Debug\Excel-Imported";
                // DirectoryMove(StrPath,StrPath2); 

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        public static void DirectoryMove(string strSource, string StrDestination)
        {
            try
            {
                string str = strSource;
                string dir = StrDestination;

                File.Move(str, Path.Combine(dir, Path.GetFileName(str)));
            }
            catch (Exception e)
            {
                Global.Message("The process failed: {0}" + e.ToString() + "");
            }
        }

        public void GetPopup()
        {
            try
            {

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = Val.ToString((Application.StartupPath + "\\RapCalcExcel\\"));//"E:\\Milan Work\\OM\\AxoneMFGRJ\\AxoneMFGRJ\\bin\\Debug\\RapCalcExcel";
                dialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                //  dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //MessageBox.Show("You selected: " + dialog.FileName);

                    // OpenFileDialog OpenFileDialog = new OpenFileDialog();
                    //// OpenFileDialog.Filter = "Excel Files (*.xls,*.xlsx)|*.xls;*.xlsx;";
                    // OpenFileDialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                    // if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    // {
                    StrPath = dialog.FileName;

                    string extension = Path.GetExtension(StrPath.ToString());
                    string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(StrPath);

                    if (Path.GetExtension(destinationPath).ToUpper().Contains("XLSX"))
                    {

                        destinationPath = destinationPath.Replace(extension, ".xlsx");
                        if (File.Exists(destinationPath))
                        {
                            File.Delete(destinationPath);
                        }
                        File.Copy(StrPath, destinationPath);


                        //GetExcelSheetNames(destinationPath);
                        //CmbSheetname.SelectedIndex = 0;

                        //if (File.Exists(destinationPath))
                        //{
                        //    File.Delete(destinationPath);
                        //}
                    }
                }
                else
                {
                    StrPath = string.Empty;
                }
                dialog.Dispose();
                dialog = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString() + "InValid File Name");
            }
        }
        private String[] GetExcelSheetNames(string excelFile)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                String connString = "";
                if (Path.GetExtension(excelFile).Equals(".xls"))
                {
                    connString = "Provider=Microsoft.ACE.OLEDB.4.0;" +
                      "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";
                }
                else
                {
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                 "Data Source=" + excelFile + ";Extended Properties=Excel 12.0;";
                }

                objConn = new OleDbConnection(connString);
                objConn.Open();
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                List<string> sheets = new List<string>();
                if (dt == null)
                {
                    return null;
                }
                String[] excelSheets = new String[dt.Rows.Count];
                CmbSheetname.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    string sheetName = (string)row["TABLE_NAME"];
                    sheets.Add(sheetName);
                    CmbSheetname.Items.Add(sheetName);
                }

                return excelSheets;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return null;
            }
            finally
            {
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        private void repTxtPopup_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                DataTable DTab = new DataTable();
                string StrParaNameColName = "";
                string StrParaIDColName = "";
                string StrParaCodeColName = "";

                if (GrdDet.FocusedColumn.FieldName.ToUpper() == "SHAPECODE")
                {
                    DTab = DTabParameter.Select("PARATYPE = 'SHAPE'").CopyToDataTable();
                    StrParaNameColName = "PARACODE";
                    StrParaIDColName = "SHAPE_ID";
                    StrParaCodeColName = "SHAPENAME";
                }
                else if (GrdDet.FocusedColumn.FieldName.ToUpper() == "COLORNAME")
                {
                    DTab = DTabParameter.Select("PARATYPE = 'COLOR'").CopyToDataTable();
                    StrParaNameColName = "PARANAME";
                    StrParaIDColName = "COLOR_ID";
                    StrParaCodeColName = "COLORCODE";
                }
                else if (GrdDet.FocusedColumn.FieldName.ToUpper() == "CLARITYNAME")
                {
                    DTab = DTabParameter.Select("PARATYPE = 'CLARITY'").CopyToDataTable();
                    StrParaNameColName = "PARANAME";
                    StrParaIDColName = "CLARITY_ID";
                    StrParaCodeColName = "CLARITYCODE";
                }
                else if (GrdDet.FocusedColumn.FieldName.ToUpper() == "CUTCODE")
                {
                    DTab = DTabParameter.Select("PARATYPE = 'CUT'").CopyToDataTable();
                    StrParaNameColName = "PARACODE";
                    StrParaIDColName = "CUT_ID";
                    StrParaCodeColName = "CUTNAME";
                }
                else if (GrdDet.FocusedColumn.FieldName.ToUpper() == "POLCODE")
                {
                    DTab = DTabParameter.Select("PARATYPE = 'POLISH'").CopyToDataTable();
                    StrParaNameColName = "PARACODE";
                    StrParaIDColName = "POL_ID";
                    StrParaCodeColName = "POLNAME";
                }
                else if (GrdDet.FocusedColumn.FieldName.ToUpper() == "SYMCODE")
                {
                    DTab = DTabParameter.Select("PARATYPE = 'SYMMETRY'").CopyToDataTable();
                    StrParaNameColName = "PARACODE";
                    StrParaIDColName = "SYM_ID";
                    StrParaCodeColName = "SYMNAME";
                }
                else if (GrdDet.FocusedColumn.FieldName.ToUpper() == "FLNAME")
                {
                    DTab = DTabParameter.Select("PARATYPE = 'FLUORESCENCE'").CopyToDataTable();
                    StrParaNameColName = "PARANAME";
                    StrParaIDColName = "FL_ID";
                    StrParaCodeColName = "FLCODE";

                }
                DTab.DefaultView.Sort = "SequenceNo";
                DTab = DTab.DefaultView.ToTable(false, "Para_ID", "ParaCode", "ParaName", "SequenceNo");

                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                    FrmSearchPopupBox.mSearchField = "ParaCode,ParaName,SequenceNo";
                    FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBox.mDTab = DTab;
                    FrmSearchPopupBox.mColumnsToHide = "Para_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearchPopupBox.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchPopupBox.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue(GrdDet.FocusedColumn.FieldName, (Val.ToString(FrmSearchPopupBox.mDRow[StrParaNameColName])));
                        GrdDet.SetFocusedRowCellValue(GrdDet.Columns[StrParaIDColName], (Val.ToString(FrmSearchPopupBox.mDRow["PARA_ID"])));
                        GrdDet.SetFocusedRowCellValue(GrdDet.Columns[StrParaCodeColName], (Val.ToString(FrmSearchPopupBox.mDRow["PARACODE"])));

                        FetchRecord(GrdDet.FocusedRowHandle);
                        FindRap();

                    }
                    FrmSearchPopupBox.Hide();
                    FrmSearchPopupBox.Dispose();
                    FrmSearchPopupBox = null;
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
                return;

            if (e.Column.FieldName == "CARAT")
            {
                //string str1 = this.ActiveControl.Name.ToString();
                Control c = this.ActiveControl;
                if (c is AxonContLib.cButton)
                {
                    return;
                }

                FetchRecord(GrdDet.FocusedRowHandle); //Cmnt : 02-07-2022
                FindRap(); //Cmnt : 02-07-2022
            }
            if (GrdDet.FocusedColumn.FieldName.ToUpper() == "FLNAME") //#P : 25-02-2022
            {
                GrdDet.FocusedColumn = GrdDet.VisibleColumns[GrdDet.Columns["CARAT"].VisibleIndex - 1];
                GrdDet.Focus();
            }

        }

        private void RbtBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtBarcode.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                //PanelBarcode.TabStop = true;
                //PanelPacketNo.TabStop = false;
                txtBarcode.Focus();
            }
            else if (RbtPacketNo.Checked)
            {
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                //PanelBarcode.TabStop = false;
                //PanelPacketNo.TabStop = true;
                txtKapanName.Focus();
            }
            else if (RbtPktSerialNo.Checked)
            {
                txtBarcode.Text = string.Empty;
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtSrNoKapanName.Focus();
            }
            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;
            BtnSave.Enabled = false;
        }

        private void txtBarcode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(CmbPrdType.Tag) == 3 || Val.ToInt(CmbPrdType.Tag) == 4 || Val.ToInt(CmbPrdType.Tag) == 5 || Val.ToInt(CmbPrdType.Tag) == 6 || Val.ToInt(CmbPrdType.Tag) == 7 || Val.ToInt(CmbPrdType.Tag) == 12)
                {
                    DataTable DTabGetBarcode = ObjRap.GetBarcodeWiseEmployeeCode(txtBarcode.Text);
                    if (DTabGetBarcode == null || DTabGetBarcode.Rows.Count == 0)
                    {
                        txtEmployee.Tag = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID);
                        txtEmployee.Text = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGERCODE);
                    }
                    else
                    {
                        if (txtEmployee.Enabled == true)
                        {
                            txtEmployee.Tag = Val.ToString(DTabGetBarcode.Rows[0]["EMPLOYEE_ID"]);
                            txtEmployee.Text = Val.ToString(DTabGetBarcode.Rows[0]["EMPLOYEECODE"]);
                        }
                        else
                        {
                            txtEmployee.Tag = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID);
                            txtEmployee.Text = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGERCODE);
                        }
                    }
                }
                else
                {
                    txtEmployee.Tag = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID);
                    txtEmployee.Text = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGERCODE);
                }
                if (txtEmployee.Enabled == false && RbtBarcode.Checked)
                    BtnContinue.Focus();
                else if (txtTag.Enabled == false && txtEmployee.Enabled == false && RbtPacketNo.Checked)
                    BtnContinue.Focus();
                else
                    txtEmployee.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtPacketNo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (txtTag.Enabled == false)
            //        txtEmployee.Focus();
            //}
        }
        private void txtPacketNo_Leave(object sender, EventArgs e)
        {
            if (txtTag.Enabled == false)
                txtEmployee.Focus();
            else
                txtTag.Focus();
        }

        private void txtSrNoSerialNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(CmbPrdType.Tag) == 3 || Val.ToInt(CmbPrdType.Tag) == 4 || Val.ToInt(CmbPrdType.Tag) == 5 || Val.ToInt(CmbPrdType.Tag) == 6 || Val.ToInt(CmbPrdType.Tag) == 7 || Val.ToInt(CmbPrdType.Tag) == 12)
                {
                    DataTable DTabGetBarcode = ObjRap.GetBarcodeWiseEmployeeCode("", txtSrNoKapanName.Text, Val.ToInt32(txtSrNoSerialNo.Text));
                    if (DTabGetBarcode == null || DTabGetBarcode.Rows.Count == 0)
                    {
                        txtEmployee.Tag = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID);
                        txtEmployee.Text = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGERCODE);
                    }
                    else
                    {
                        if (txtEmployee.Enabled == true)
                        {
                            txtEmployee.Tag = Val.ToString(DTabGetBarcode.Rows[0]["EMPLOYEE_ID"]);
                            txtEmployee.Text = Val.ToString(DTabGetBarcode.Rows[0]["EMPLOYEECODE"]);
                        }
                        else
                        {
                            txtEmployee.Tag = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID);
                            txtEmployee.Text = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGERCODE);
                        }
                    }
                }
                else
                {
                    txtEmployee.Tag = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID);
                    txtEmployee.Text = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGERCODE);
                }
                if (txtEmployee.Enabled == false && RbtBarcode.Checked)
                    BtnContinue.Focus();
                else if (txtTag.Enabled == false && txtEmployee.Enabled == false && RbtPacketNo.Checked)
                    BtnContinue.Focus();
                else
                    txtEmployee.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtTag_Leave(object sender, EventArgs e)
        {
            if (txtEmployee.Enabled == false && RbtBarcode.Checked)
                BtnContinue.Focus();
            else if (txtTag.Enabled == false && txtEmployee.Enabled == false && RbtPacketNo.Checked)
                BtnContinue.Focus();
            else
                txtEmployee.Focus();
        }

        public void BarcodePrintCode()
        {
            try
            {
                DataRow DRow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

                if (DRow == null)
                {
                    Global.Message("Please Select Packet Prediction Which You Want To Print");
                    return;
                }
                //Global.BarcodeProntMkblTSC(DRow); 

                /*
                EmployeeActionRightsProperty PropertyEmployeeActionRights = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

                StrBPrintType = PropertyEmployeeActionRights.BPRINTTYPE;
                if (StrBPrintType == "TSC")
                {
                    Global.BarcodeProntMkblTSC(DRow);
                }
                else if (StrBPrintType.ToUpper() == "CITIZEN")
                {
                    Global.BarcodeProntMkblCitizen(DRow);
                }
                Global.Message("Print Successfully In :" + StrBPrintType);
                */

                EmployeeActionRightsProperty PropertyEmployeeActionRights = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

                StrBPrintType = PropertyEmployeeActionRights.BPRINTTYPE;
                if (StrBPrintType == "TSC")
                {
                    StrBarcodeTxtFileName = "\\PrintBarcodeDataMkbl_TSC.txt";
                    StrBatchFileName = "\\PRINTBARCODE_TSC.BAT ";
                }
                else if (StrBPrintType.ToUpper() == "CITIZEN")
                {
                    StrBarcodeTxtFileName = "\\PrintBarcodeDataMkbl_Citizen.txt";
                    StrBatchFileName = "\\PRINTBARCODE_Citizen.BAT ";
                }
                //else if (StrBPrintType.ToUpper() == "TSCGALAXY")
                //{
                //    StrBarcodeTxtFileName = "\\PrintBarcodeData_TSCGALAXY.txt";
                //    StrBatchFileName = "\\PRINTBARCODE_TSCGALAXY.BAT ";
                //}
                else if (StrBPrintType == "")
                {
                    StrBarcodeTxtFileName = "\\PrintBarcodeDataMkbl_TSC.txt";
                    StrBatchFileName = "\\PRINTBARCODE_TSC.BAT ";
                    StrBPrintType = "TSC";
                }


                string fileLoc = Application.StartupPath + StrBarcodeTxtFileName;
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }
                System.IO.File.Create(fileLoc).Dispose();

                //DataRow DRow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                string StrBarcode = mstrBarcode;
                string StrKapanNames = txtKapanName.Text;
                string StrEmployeeCode = txtEmployee.Text;
                string StrPktNoTag = txtPacketNo.Text + "" + TAGSER;
                int StrPktSrNo = PKTSRNOSER;
                string StrParameterAmt = DRow["COLORNAME"].ToString() + "-" + DRow["CLARITYNAME"].ToString() + "-" + DRow["CUTCODE"].ToString() + "-" + DRow["FLNAME"].ToString() + "-" + DRow["AMOUNT"].ToString();
                string StrShpBlnCts = DRow["SHAPECODE"].ToString() + "-" + DRow["CARAT"].ToString() + "-" + lblBalance.Text;
                string StrDate = DateTime.Now.ToString("dd-MM");

                StreamWriter sw = new StreamWriter(fileLoc);
                if (StrBPrintType == "CITIZEN")
                {
                    //using (sw)
                    //{
                    //    sw.WriteLine("G0");
                    //    sw.WriteLine("n");
                    //    sw.WriteLine("M0500");
                    //    sw.WriteLine("O0214");
                    //    sw.WriteLine("V0");
                    //    sw.WriteLine("t1");
                    //    sw.WriteLine("Kf0070");
                    //    sw.WriteLine("L");
                    //    sw.WriteLine("D11");
                    //    sw.WriteLine("A2");
                    //    sw.WriteLine("1e6303100150058C" + StrBarcode + "");
                    //    sw.WriteLine("ySPM");
                    //    sw.WriteLine("1911C1200350003" + StrKapanNames + "");
                    //    sw.WriteLine("1911C0800020003" + StrEmployeeCode + "");
                    //    sw.WriteLine("1911C0800450178" + DateTime.Now.ToString("dd-MM") + "");
                    //    sw.WriteLine("1911C0800020176" + StrPktNoTag + "");
                    //    sw.WriteLine("1911C1200190003" + StrPktSrNo + "");
                    //    sw.WriteLine("1911C0800020059" + StrParameterAmt + "");
                    //    sw.WriteLine("1911C0800450063" + StrShpBlnCts + "");
                    //    sw.WriteLine("Q0001");
                    //    sw.WriteLine("E");
                    //}
                    Global.BarcodeProntMkblCitizen(sw, StrBarcode, StrKapanNames, StrEmployeeCode, StrPktNoTag, StrPktSrNo.ToString(), StrParameterAmt, StrShpBlnCts);
                }
                else if (StrBPrintType == "TSC")
                {

                    Global.BarcodeProntMkblTSC(sw, StrBarcode, StrKapanNames, StrEmployeeCode, StrPktNoTag, StrPktSrNo.ToString(), StrParameterAmt, StrShpBlnCts);
                    //sw.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 52.5 mm, 15 mm");
                    //sw.WriteLine("GAP 2.5 mm, 0 mm");
                    //sw.WriteLine("DIRECTION 0,0");
                    //sw.WriteLine("REFERENCE 0,0");
                    //sw.WriteLine("OFFSET 0 mm");
                    //sw.WriteLine("SET PEEL OFF");
                    //sw.WriteLine("SET CUTTER OFF");
                    //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='2' pitch='15.0 mm'></xpml>SET TEAR ON");
                    //sw.WriteLine("CLS");
                    //sw.WriteLine("CODEPAGE 1252");
                    //sw.WriteLine("TEXT 406,75,\"2\",180,1,1,\"" + StrKapanNames + "\"");
                    //sw.WriteLine("BARCODE 304,73,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");
                    //sw.WriteLine("TEXT 406,13,\"1\",180,1,1,\"" + StrEmployeeCode + "\"");
                    //sw.WriteLine("TEXT 69,13,\"1\",180,1,1,\"" + StrPktNoTag + "\"");
                    //sw.WriteLine("TEXT 406,42,\"2\",180,1,1,\"" + StrPktSrNo + "\"");
                    //sw.WriteLine("TEXT 304,95,\"1\",180,1,1,\"" + StrShpBlnCts + "\"");
                    //sw.WriteLine("TEXT 69,95,\"1\",180,1,1,\"" + DateTime.Now.ToString("dd-MM") + "\"");
                    //sw.WriteLine("TEXT 304,13,\"1\",180,1,1,\"" + StrParameterAmt + "\"");
                    //sw.WriteLine("PRINT 1,2");
                    //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + StrBatchFileName) && File.Exists(fileLoc))
                {
                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + StrBatchFileName + fileLoc, AppWinStyle.Hide, true, -1);
                }

                System.Threading.Thread.Sleep(800);



            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void btnBarcodePrint_Click(object sender, EventArgs e)
        {
            BarcodePrintCode();
        }

        private void cLabel8_Click(object sender, EventArgs e)
        {

        }
    }


}