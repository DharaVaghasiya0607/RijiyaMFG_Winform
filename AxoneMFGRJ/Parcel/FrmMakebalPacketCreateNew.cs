using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BusLib;
using BusLib.Master;
using BusLib.TableName;
using System.IO;
using BusLib.Rapaport;
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using System.Linq;
using System.Collections;
using BusLib.View;
using OfficeOpenXml;
using DevExpress.XtraGrid.Views.Grid;
using BusLib.Transaction;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmMakebalPacketCreateNew : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        BOFindRap ObjRap = new BOFindRap();

        DataTable DTabPrdType = new DataTable();
        DataTable DTabRapDate = new DataTable();
        DataTable DTabParameter = new DataTable();
        DataTable DTabGrdDet = new DataTable();
        Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();
        Trn_RapSaveProperty clsFindRapRChk = new Trn_RapSaveProperty();
        DataTable DtabExcelData = new DataTable();
        DataTable DTab = new DataTable();

        bool IsDownImage = true;
        Int32 pIntDept_ID = 0;

        string[] StrGrdEmployee;

        string mStrEntryDate = ""; //#P : 12-02-2020

        bool mISTFlag = false; //#p : 17-12-2020 : Kyare Data Update kare tyare.. PrdMarkerWages na table mathi Data Delete karva mate Use thase

        DataTable DtabLabEmp = new DataTable();
        DataTable DTabRChkData = new DataTable();
        DataTable DTabUploadData = new DataTable();

        IList<DataStructureGrading> DataSHAPE = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataCOLOR = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataCLARITY = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataCUT = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataPOL = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataSYM = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataFL = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataNATTS = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataLBLC = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataBLACK = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataCS = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataBINC = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataWINC = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataOINC = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataPAV = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataMILKY = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataLUSTER = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataEYECLEAN = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataHA = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataTENSION = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataNATURAL = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataGRAIN = new BindingList<DataStructureGrading>();
        IList<DataStructureGrading> DataLAB = new BindingList<DataStructureGrading>();


        IList<DataStructureGradingRepairing> DataSHAPERep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataCOLORRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataCLARITYRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataCUTRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataPOLRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataSYMRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataFLRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataNATTSRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataLBLCRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataBLACKRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataCSRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataBINCRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataWINCRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataOINCRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataPAVRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataMILKYRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataLUSTERRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataEYECLEANRep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataHARep = new BindingList<DataStructureGradingRepairing>();
        IList<DataStructureGradingRepairing> DataLABRep = new BindingList<DataStructureGradingRepairing>();

        EmployeeActionRightsProperty EmployeeRightsProperty = new EmployeeActionRightsProperty();

        EmployeeActionRightsProperty SelectedEmployeeRightsProperty = new EmployeeActionRightsProperty();

        DataTable DTabSuratLabExpense = new DataTable();

        string mStrReportNo = "";

        BODevGridSelection ObjGridSelection;

        #region Constructor

        public FrmMakebalPacketCreateNew()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrKapanName, Int64 pIntKapan_ID, string pStrMainPacketNo, Int64 pIntMainPacket_ID, string pStrMarkerCode, Int64 pStrMarker_ID, double pDouBalanceCarat, string pStrEmpCode, Int64 pINtEmp_ID, string StrKapanManager, Int64 StrKapanManager_ID)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            IsDownImage = false;
            BtnUpDown_Click(null, null);

            if (MainGrdExcel.RepositoryItems.Count == 1)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdExcle;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdExcle.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }

            this.Show();

            SetControl();
            BtnClear_Click(null, null);
            DTabSuratLabExpense = new BOTRN_RunninPossition().GetSuratExpectedLabExpenseMaster();

            EmployeeRightsProperty = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
            txtEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;

            DTabPrdType = ObjRap.GetPreditctionType(EmployeeRightsProperty.PRDTYPE_ID);
            CmbPrdType.Items.Clear();
            foreach (DataRow DRow in DTabPrdType.Rows)
            {
                if (Val.ToInt(DRow["PRDTYPE_ID"]) == 4)
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

            txtKapanName.Text = pStrKapanName;
            txtKapanName.Tag = pIntKapan_ID;

            txtMainPacketNo.Text = pStrMainPacketNo;
            txtMainPacketNo.Tag = pIntMainPacket_ID;

            txtEmployee.Text = pStrMarkerCode;
            txtEmployee.Tag = pStrMarker_ID;

            txtMainManager.Text = StrKapanManager;//GUNJAN:28/03/2023
            txtMainManager.Tag = StrKapanManager_ID;

            RbtSurat_CheckedChanged(null, null);
            BtnContinue_Click(null, null);

            txtLotCarat.Focus();

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

            DesignComboBox("SHAPE", CmbShape, "PARACODE");
            DesignComboBox("COLOR", CmbColor, "PARANAME");
            DesignComboBox("CLARITY", CmbClarity, "PARANAME");
            DesignComboBox("CUT", CmbCut, "PARACODE");
            DesignComboBox("POLISH", CmbPol, "PARACODE");
            DesignComboBox("SYMMETRY", CmbSym, "PARACODE");
            DesignComboBox("FLUORESCENCE", CmbFL, "PARANAME");

            DesignComboBox("NATTS", CmbNatts, "PARANAME");
            DesignComboBox("LBLC", CmbLBLC, "PARANAME");
            DesignComboBox("COLORSHADE", CmbColorShade, "PARACODE");
            DesignComboBox("BLACK", CmbBInC, "PARACODE");
            DesignComboBox("WHITE", CmbWInC, "PARACODE");
            DesignComboBox("OPEN", CmbOInC, "PARACODE");
            DesignComboBox("PAVALION", CmbPav, "PARACODE");
            DesignComboBox("MILKY", CmbMilky, "PARACODE");
            DesignComboBox("LUSTER", CmbLuster, "PARACODE");
            DesignComboBox("EYECLEAN", CmbEyeClean, "PARACODE");
            DesignComboBox("HEARTANDARROW", CmbHA, "PARACODE");
            DesignComboBox("TENSION", CmbTension, "PARANAME");
            DesignComboBox("GRAIN", CmbGrain, "PARACODE");
            DesignComboBox("NATURAL", CmbNatural, "PARANAME");
            DesignComboBox("LAB", CmbLab, "PARACODE");

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

            if (pStrParaType == "SHAPE")
            {
                DataSHAPE.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataSHAPE.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbShape.AccessibleDescription = pStrParaType;
                CmbShape.DataSource = DataSHAPE;
                CmbShape.DisplayMember = pStrDisplayText;
                CmbShape.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "COLOR")
            {
                DataCOLOR.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataCOLOR.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbColor.AccessibleDescription = pStrParaType;
                CmbColor.DataSource = DataCOLOR;
                CmbColor.DisplayMember = pStrDisplayText;
                CmbColor.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "CLARITY")
            {
                DataCLARITY.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataCLARITY.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbClarity.AccessibleDescription = pStrParaType;
                CmbClarity.DataSource = DataCLARITY;
                CmbClarity.DisplayMember = pStrDisplayText;
                CmbClarity.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "CUT")
            {
                DataCUT.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataCUT.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbCut.AccessibleDescription = pStrParaType;
                CmbCut.DataSource = DataCUT;
                CmbCut.DisplayMember = pStrDisplayText;
                CmbCut.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "POLISH")
            {
                DataPOL.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataPOL.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbPol.AccessibleDescription = pStrParaType;
                CmbPol.DataSource = DataPOL;
                CmbPol.DisplayMember = pStrDisplayText;
                CmbPol.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "SYMMETRY")
            {
                DataSYM.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataSYM.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbSym.AccessibleDescription = pStrParaType;
                CmbSym.DataSource = DataSYM;
                CmbSym.DisplayMember = pStrDisplayText;
                CmbSym.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "FLUORESCENCE")
            {
                DataFL.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataFL.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbFL.AccessibleDescription = pStrParaType;
                CmbFL.DataSource = DataFL;
                CmbFL.DisplayMember = pStrDisplayText;
                CmbFL.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "NATTS")
            {
                DataNATTS.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataNATTS.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbNatts.AccessibleDescription = pStrParaType;
                CmbNatts.DataSource = DataNATTS;
                CmbNatts.DisplayMember = pStrDisplayText;
                CmbNatts.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "LBLC")
            {
                DataLBLC.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataLBLC.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbLBLC.AccessibleDescription = pStrParaType;
                CmbLBLC.DataSource = DataLBLC;
                CmbLBLC.DisplayMember = pStrDisplayText;
                CmbLBLC.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "COLORSHADE")
            {
                DataCS.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataCS.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbColorShade.AccessibleDescription = pStrParaType;
                CmbColorShade.DataSource = DataCS;
                CmbColorShade.DisplayMember = pStrDisplayText;
                CmbColorShade.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "BLACK")
            {
                DataBINC.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataBINC.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbBInC.AccessibleDescription = pStrParaType;
                CmbBInC.DataSource = DataBINC;
                CmbBInC.DisplayMember = pStrDisplayText;
                CmbBInC.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "WHITE")
            {
                DataWINC.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataWINC.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbWInC.AccessibleDescription = pStrParaType;
                CmbWInC.DataSource = DataWINC;
                CmbWInC.DisplayMember = pStrDisplayText;
                CmbWInC.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "OPEN")
            {
                DataOINC.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataOINC.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbOInC.AccessibleDescription = pStrParaType;
                CmbOInC.DataSource = DataOINC;
                CmbOInC.DisplayMember = pStrDisplayText;
                CmbOInC.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "PAVALION")
            {
                DataPAV.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataPAV.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbPav.AccessibleDescription = pStrParaType;
                CmbPav.DataSource = DataPAV;
                CmbPav.DisplayMember = pStrDisplayText;
                CmbPav.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "MILKY")
            {
                DataMILKY.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataMILKY.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbMilky.AccessibleDescription = pStrParaType;
                CmbMilky.DataSource = DataMILKY;
                CmbMilky.DisplayMember = pStrDisplayText;
                CmbMilky.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "LUSTER")
            {
                DataLUSTER.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataLUSTER.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbLuster.AccessibleDescription = pStrParaType;
                CmbLuster.DataSource = DataLUSTER;
                CmbLuster.DisplayMember = pStrDisplayText;
                CmbLuster.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "EYECLEAN")
            {
                DataEYECLEAN.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataEYECLEAN.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbEyeClean.AccessibleDescription = pStrParaType;
                CmbEyeClean.DataSource = DataEYECLEAN;
                CmbEyeClean.DisplayMember = pStrDisplayText;
                CmbEyeClean.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "HEARTANDARROW")
            {
                DataHA.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataHA.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbHA.AccessibleDescription = pStrParaType;
                CmbHA.DataSource = DataHA;
                CmbHA.DisplayMember = pStrDisplayText;
                CmbHA.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "TENSION")
            {
                DataTENSION.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataTENSION.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbTension.AccessibleDescription = pStrParaType;
                CmbTension.DataSource = DataTENSION;
                CmbTension.DisplayMember = pStrDisplayText;
                CmbTension.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "NATURAL")
            {
                DataNATURAL.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataNATURAL.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbNatural.AccessibleDescription = pStrParaType;
                CmbNatural.DataSource = DataNATURAL;
                CmbNatural.DisplayMember = pStrDisplayText;
                CmbNatural.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "GRAIN")
            {
                DataGRAIN.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataGRAIN.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbGrain.AccessibleDescription = pStrParaType;
                CmbGrain.DataSource = DataGRAIN;
                CmbGrain.DisplayMember = pStrDisplayText;
                CmbGrain.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "LAB")
            {
                DataLAB.Add(new DataStructureGrading() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataLAB.Add(new DataStructureGrading() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbLab.AccessibleDescription = pStrParaType;
                CmbLab.DataSource = DataLAB;
                CmbLab.DisplayMember = pStrDisplayText;
                CmbLab.ValueMember = "PARA_ID";
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
                if(Val.Val(txtCarat.Text) == 0)
                {
                    Global.Message("Exp Carat Is Required");
                    txtCarat.Focus();
                    return;
                }
                if (Val.Val(txtLotCarat.Text) == 0)
                {
                    Global.Message("Carat Is Required");
                    txtLotCarat.Focus();
                    return;
                }

                //if (Val.ToString(txtTag.Tag).Length == 0)
                //{
                //    Global.MessageError("Packet ID Not Found In this PacketNo");
                //    txtTag.Focus();
                //    return;
                //}
                if (Val.ToString(txtEmployee.Tag).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Employee.");
                    txtEmployee.Focus();
                    return;
                }
                if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Employee.");
                    txtEmployee.Focus();
                    return;
                }
                if (((Val.ToInt(CmbPrdType.Tag) == 8) || (Val.ToInt(CmbPrdType.Tag) == 9)) && CmbLabProcess.SelectedIndex == 0)
                {
                    Global.MessageError("You Have To Select Graph/NonGraph  For BY Transfer While Making Grading Entry");
                    CmbLabProcess.Focus();
                    return;
                }

                if (txtMainManager.Text.Trim().Length == 0)//Gunjan:27/03/2023
                {
                    Global.Message("Kapan Main Manager Is Required");
                    txtMainManager.Focus();
                    return ;
                }


                // Validation For Previous Prediction is Exists Or Not
                Trn_RapSaveProperty Property = new Trn_RapSaveProperty();
                Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.KAPAN_ID = Val.ToInt64(txtKapanName.Tag);
                Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                Property.TAG = txtTag.Text;
                Property.MTAG = txtTag.Text;
                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);

                Property.SHAPE_ID = Val.ToInt32(CmbShape.Tag);
                Property.SHAPENAME = Val.ToString(CmbShape.Text);

                Property.COLOR_ID = Val.ToInt32(CmbColor.Tag);
                Property.COLORNAME = Val.ToString(CmbColor.Text);

                Property.CLARITY_ID = Val.ToInt32(CmbClarity.Tag);
                Property.CLARITYNAME = Val.ToString(CmbClarity.Text);

                Property.LOTCARAT = Val.Val(txtLotCarat.Text);
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
                        CmbShape.Focus();
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
                //FindRap_ForRepairing();

                this.Cursor = Cursors.WaitCursor;


                Int64 IntPrdID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.PRD_ID);

                ObjRap.DeleteAll(Val.ToInt(CmbPrdType.Tag), txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, Val.ToInt64(txtEmployee.Tag), Val.ToString(txtTag.Tag), mISTFlag);

                Property = new Trn_RapSaveProperty();

                Property.MAINPACKET_ID = Val.ToInt64(txtMainPacketNo.Tag);
                Property.PACKET_ID = Val.ToInt64(txtTag.Tag);
                Property.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                Property.PRDTYPE = Val.ToString(CmbPrdType.SelectedItem);
                Property.KAPANNAME = Val.ToString(txtKapanName.Text);
                Property.KAPAN_ID = Val.ToInt64(txtKapanName.Tag);
                Property.PACKETNO = Val.ToInt(txtPacketNo.Text);
                Property.MTAG = txtTag.Text;
                Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                Property.MANAGER_ID = 0;

                Property.ID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.ID);
                Property.PRD_ID = IntPrdID;

                Property.TAGSRNO = 1;
                Property.TAG = txtTag.Text;
                Property.PLANNO = 1;

                Property.SHAPE_ID = Val.ToInt(CmbShape.Tag);
                Property.CLARITY_ID = Val.ToInt(CmbClarity.Tag);
                Property.COLOR_ID = Val.ToInt(CmbColor.Tag);
                Property.CUT_ID = Val.ToInt(CmbCut.Tag);
                Property.POL_ID = Val.ToInt(CmbPol.Tag);
                Property.SYM_ID = Val.ToInt(CmbSym.Tag);
                Property.FL_ID = Val.ToInt(CmbFL.Tag);

                Property.LBLC_ID = Val.ToInt(CmbLBLC.Tag);
                Property.NATTS_ID = Val.ToInt(CmbNatts.Tag);

                //Property.COLORSHADE_ID = 0;                   //Changed : 05-02-2020
                Property.COLORSHADE_ID = Val.ToInt(CmbColorShade.Tag);

                Property.BLACKINC_ID = Val.ToInt(CmbBInC.Tag);
                Property.OPENINC_ID = Val.ToInt(CmbOInC.Tag);
                Property.WHITEINC_ID = Val.ToInt(CmbWInC.Tag);
                Property.PAV_ID = Val.ToInt(CmbPav.Tag);
                Property.MILKY_ID = Val.ToInt(CmbMilky.Tag);
                Property.LUSTER_ID = Val.ToInt(CmbLuster.Tag);
                Property.EYECLEAN_ID = Val.ToInt(CmbEyeClean.Tag);
                Property.HA_ID = Val.ToInt(CmbHA.Tag);
                Property.NATURAL_ID = Val.ToInt(CmbNatural.Tag);
                Property.GRAIN_ID = Val.ToInt(CmbGrain.Tag);
                Property.TENSION_ID = Val.ToInt(CmbTension.Tag);

                Property.CARAT = Val.Val(txtCarat.Text);
                Property.PCS = Val.ToInt32(txtPcs.Text);
                Property.LOTCARAT = Val.Val(txtLotCarat.Text);
                Property.BALANCECARAT = Val.Val(txtLotCarat.Text);

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

                Property.ENTRYDATE = Val.ToString(mStrEntryDate) != "" ? DateTime.Parse(Val.ToString(mStrEntryDate)).ToString("yyyy-MM-dd HH:mm:ss") : Val.SqlDate(System.DateTime.Now.ToShortDateString());

                //if (Val.ToString(CmbLabSelection.SelectedItem) == "IGI")
                //{
                //    Property.LAB_ID = 231;
                //}
                //else
                //{
                //    Property.LAB_ID = 0;
                //}

                Property.LAB_ID = Val.ToInt32(CmbLab.Tag);

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

                double DouMumExpLabCharge = 0;
                double DouMumExpBeforeRapaport = 0;
                double DouMumExpBeforeDiscount = 0;
                double DouMumExpBeforePricePerCarat = 0;
                double DouMumExpBeforeAmount = 0;

                double DouMumExpAfterRapaport = 0;
                double DouMumExpAfterDiscount = 0;
                double DouMumExpAfterPricePerCarat = 0;
                double DouMumExpAfterAmount = 0;

                Property.MUMBAIEXPLABCHARGE = DouMumExpLabCharge;
                Property.MUMBAIEXPBEFORERAPAPORT = DouMumExpBeforeRapaport;
                Property.MUMBAIEXPBEFOREDISCOUNT = DouMumExpBeforeDiscount;
                Property.MUMBAIEXPBEFOREPRICEPERCARAT = DouMumExpBeforePricePerCarat;
                Property.MUMBAIEXPBEFOREAMOUNT = DouMumExpBeforeAmount;

                Property.MUMBAIEXPAFTERRAPAPORT = DouMumExpAfterRapaport;
                Property.MUMBAIEXPAFTERDISCOUNT = DouMumExpAfterDiscount;
                Property.MUMBAIEXPAFTERPRICEPERCARAT = DouMumExpAfterPricePerCarat;
                Property.MUMBAIEXPAFTERAMOUNT = DouMumExpAfterAmount;
                //End : #P : 13-01-2021

                Property.HELIUMTABLEPC = Val.ToString(txtTablePC.Text);
                Property.HELIUMRATIO = Val.ToString(txtRatio.Text);
                Property.HELIUMTOTALDEPTH = Val.ToString(txtTotalDepth.Text);
                //End : Pinali : 26-05-2019

                Property.ENTRYMODE = lblMode.Text;

                Property.REPORTNO = Val.ToString(mStrReportNo);

                Property.ISCONFIRMGRADER = SelectedEmployeeRightsProperty.ISCONFIRMGRADER;
                Property.KAPANMAINMANAGER_ID = Val.ToInt64(txtMainManager.Tag);//GUNJAN : 27/03/2023

                Property.Ope = "MAKEABLE";

              
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
                    if (Val.ToInt(CmbPrdType.Tag) == 8)
                    {
                        SaveRChkRepData(19, "GRADING REPAIRING");
                    }
                }
                this.Cursor = Cursors.Default;

                Global.Message("****  " + Val.ToString(CmbPrdType.SelectedItem) + "   *****\n\nSUCCESSFULLY SAVED OF " + txtKapanName.Text + "/" + txtPacketNo.Text + "/" + txtTag.Text);

                lblMode.Text = "Add Mode";
                lblGrading.Text = string.Empty;
                BtnSave.Enabled = true;
                txtLotCarat.Text = string.Empty;
                txtPcs.Text = "1";
                txtPacketNo.Text = string.Empty;
                txtPacketNo.Tag = string.Empty;
                txtTag.Text = string.Empty;
                txtTag.Tag = string.Empty;
                CmbShape.SelectedIndex = 0;
                CmbColor.SelectedIndex = 0;
                CmbClarity.SelectedIndex = 0;
                CmbCut.SelectedIndex = 0;
                CmbPol.SelectedIndex = 0;
                CmbSym.SelectedIndex = 0;
                CmbFL.SelectedIndex = 0;
                CmbLBLC.SelectedIndex = 0;
                CmbNatts.SelectedIndex = 0;
                CmbColorShade.SelectedIndex = 0;
                CmbBInC.SelectedIndex = 0;
                CmbWInC.SelectedIndex = 0;
                CmbOInC.SelectedIndex = 0;
                CmbPav.SelectedIndex = 0;
                CmbMilky.SelectedIndex = 0;
                CmbLuster.SelectedIndex = 0;
                CmbEyeClean.SelectedIndex = 0;
                CmbHA.SelectedIndex = 0;
                CmbTension.SelectedIndex = 0;
                CmbNatural.SelectedIndex = 0;
                CmbGrain.SelectedIndex = 0;
                txtCarat.Text = string.Empty;
                CmbLabSelection.SelectedIndex = 0;
                CmbLabProcess.SelectedIndex = 0;
                txtDiaMax.Text = string.Empty;
                txtDiaMin.Text = string.Empty;
                txtHeight.Text = string.Empty;
                txtTablePC.Text = string.Empty;
                txtRatio.Text = string.Empty;
                txtTotalDepth.Text = string.Empty;
                txtRapaport.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtGiaNonGia.Text = string.Empty;
                txtRemark.Text = string.Empty;
                MainGrid.DataSource = null;

                BtnContinue_Click(null, null);

                txtLotCarat.Focus();
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

            PropertyRChk.NATURAL_ID = Val.ToInt(CmbNatural.SelectedValue);
            PropertyRChk.GRAIN_ID = Val.ToInt(CmbGrain.SelectedValue);
            PropertyRChk.TENSION_ID = Val.ToInt(CmbTension.Tag);

            PropertyRChk.AMOUNTDISCOUNT = 0;

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

            PropertyRChk.ISMIXRATE = Val.ToBoolean(clsFindRapRChk.ISMIXRATE);

            PropertyRChk.MDISCOUNT = Val.Val(clsFindRapRChk.MDISCOUNT);
            PropertyRChk.MPRICEPERCARAT = Val.Val(clsFindRapRChk.MPRICEPERCARAT);
            PropertyRChk.MAMOUNT = Val.Val(clsFindRapRChk.MAMOUNT);
            PropertyRChk.MGDISCOUNT = Val.Val(clsFindRapRChk.MGDISCOUNT);
            PropertyRChk.MGPRICEPERCARAT = Val.Val(clsFindRapRChk.MGPRICEPERCARAT);
            PropertyRChk.MGAMOUNT = Val.Val(clsFindRapRChk.MGAMOUNT);

            PropertyRChk.ENTRYMODE = lblMode.Text;

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

                txtEmployee.Text = string.Empty;
                txtEmployee.Tag = string.Empty;

                txtMainPacketNo.Text = string.Empty;
                txtMainPacketNo.Tag = string.Empty;

                txtLotCarat.Text = string.Empty;
                txtPcs.Text = "1";

                txtPacketNo.Text = string.Empty;
                txtPacketNo.Tag = string.Empty;

                txtTag.Text = string.Empty;
                txtTag.Tag = string.Empty;

                //txtEmployee.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
                //txtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;

                CmbShape.SelectedIndex = 0;
                CmbColor.SelectedIndex = 0;
                CmbClarity.SelectedIndex = 0;
                CmbCut.SelectedIndex = 0;
                CmbPol.SelectedIndex = 0;
                CmbSym.SelectedIndex = 0;
                CmbFL.SelectedIndex = 0;
                CmbLBLC.SelectedIndex = 0;
                CmbNatts.SelectedIndex = 0;
                CmbColorShade.SelectedIndex = 0;
                CmbBInC.SelectedIndex = 0;
                CmbWInC.SelectedIndex = 0;
                CmbOInC.SelectedIndex = 0;

                CmbPav.SelectedIndex = 0;
                CmbMilky.SelectedIndex = 0;
                CmbLuster.SelectedIndex = 0;
                CmbEyeClean.SelectedIndex = 0;
                CmbHA.SelectedIndex = 0;
                CmbTension.SelectedIndex = 0;
                CmbNatural.SelectedIndex = 0;
                CmbGrain.SelectedIndex = 0;

                txtCarat.Text = string.Empty;

                CmbLabSelection.SelectedIndex = 0;
                CmbLabProcess.SelectedIndex = 0;

                txtDiaMax.Text = string.Empty;
                txtDiaMin.Text = string.Empty;
                txtHeight.Text = string.Empty;
                txtTablePC.Text = string.Empty;
                txtRatio.Text = string.Empty;
                txtTotalDepth.Text = string.Empty;

                txtRapaport.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;

                txtGiaNonGia.Text = string.Empty;

                //txtMKAVDisc.Text = string.Empty;
                //txtMKAVPricePerCarat.Text = string.Empty;
                //txtMKAVAmount.Text = string.Empty;


                txtRemark.Text = string.Empty;

                //txtRapnetDiscount.Text = string.Empty;
                //txtRapnetPricePerCarat.Text = string.Empty;
                //txtRapnetAmount.Text = string.Empty;
                //txtRapnetLink.Text = string.Empty;

                MainGrid.DataSource = null;

                lblLot.Text = "0.00";
                lblBalance.Text = "0.00";

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
            txtKapanName.Focus();
            mISTFlag = false;
            this.Cursor = Cursors.Default;
        }

        #endregion

        public void ConsiderBGMNonBGM(Int32 IntMilky_ID, Int32 IntColorShade_ID, string StrEntryType) //Add : Pinali : 01-06-2020
        {
            if (Val.ToString(txtKapanName.Text).Trim().Length == 0 || Val.ToString(txtPacketNo.Text).Trim().Length == 0 || Val.ToString(txtTag.Text).Trim().Length == 0)
            {
                return;
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

                ConsiderBGMNonBGM(Val.ToInt(CmbMilky.Tag), Val.ToInt(CmbColorShade.Tag), "ORIGINAL");

                clsFindRap = new Trn_RapSaveProperty();

                clsFindRap.SHAPECODE = Val.ToString(CmbShape.AccessibleName);
                clsFindRap.SHAPE_ID = Val.ToInt32(CmbShape.Tag);

                clsFindRap.COLOR_ID = Val.ToInt32(CmbColor.Tag);
                clsFindRap.COLORCODE = Val.ToString(CmbColor.AccessibleName);

                clsFindRap.CLARITY_ID = Val.ToInt32(CmbClarity.Tag);
                clsFindRap.CLARITYCODE = Val.ToString(CmbClarity.AccessibleName);

                clsFindRap.CARAT = Val.Val(txtCarat.Text);
                clsFindRap.CUTCODE = Val.ToString(CmbCut.AccessibleName);
                clsFindRap.POLCODE = Val.ToString(CmbPol.AccessibleName);
                clsFindRap.SYMCODE = Val.ToString(CmbSym.AccessibleName);

                clsFindRap.GCARAT = 0;
                clsFindRap.GCUTCODE = "";
                clsFindRap.GPOLCODE = "";
                clsFindRap.GSYMCODE = "";

                clsFindRap.FLCODE = Val.ToString(CmbFL.AccessibleName);
                clsFindRap.MILKYCODE = Val.ToString(CmbMilky.AccessibleName);
                clsFindRap.NATTSCODE = Val.ToString(CmbNatts.AccessibleName);
                clsFindRap.LBLCCODE = Val.ToString(CmbLBLC.AccessibleName);
                clsFindRap.RAPDATE = Val.SqlDate(CmbRapDate.SelectedItem.ToString());

                //clsFindRap.COLORSHADECODE = "";
                clsFindRap.COLORSHADECODE = Val.ToString(CmbColorShade.AccessibleName);

                //clsFindRap.BLACKINCCODE = "";
                //clsFindRap.OPENINCCODE = "";
                //clsFindRap.WHITEINCCODE = "";
                //clsFindRap.PAVCODE = "";
                //clsFindRap.EYECLEANCODE = "";
                //clsFindRap.LUSTERCODE = "";
                //clsFindRap.NATURALCODE = "";
                //clsFindRap.GRAINCODE = "";

                clsFindRap.BLACKINCCODE = Val.ToString(CmbBInC.AccessibleName).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbBInC.AccessibleName);
                clsFindRap.OPENINCCODE = Val.ToString(CmbOInC.AccessibleName).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbOInC.AccessibleName);
                clsFindRap.WHITEINCCODE = Val.ToString(CmbWInC.AccessibleName).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbWInC.AccessibleName);
                clsFindRap.PAVCODE = Val.ToString(CmbPav.AccessibleName).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbPav.AccessibleName);
                clsFindRap.EYECLEANCODE = Val.ToString(CmbEyeClean.AccessibleName).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbEyeClean.AccessibleName);
                clsFindRap.LUSTERCODE = Val.ToString(CmbLuster.AccessibleName).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbLuster.AccessibleName);

                clsFindRap.NATURALCODE = Val.ToString(CmbNatural.AccessibleName);
                clsFindRap.GRAINCODE = Val.ToString(CmbGrain.AccessibleName);


                //if (Val.ToString(CmbLabSelection.SelectedItem) == "IGI")
                //{
                //    clsFindRap.LABCODE = "IGI";
                //}
                //else
                //{
                //    clsFindRap.LABCODE = "";
                //}
                clsFindRap.LABCODE = Val.ToString(CmbLab.AccessibleName).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbLab.AccessibleName);

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

                //if ((Val.Val(txtSuratExpBeforeDiscount.Text) == 0 || Val.Val(txtSuratExpBeforeDiscount.Text) != 0) && lblMode.Text == "Add Mode")
                //{
                //    txtSuratExpRapaport.Text = txtRapaport.Text;
                //}

                //if (Val.Val(StrSuratExpBeforeRapaport) == 0 && lblMode.Text == "Edit Mode")
                //{
                //    txtSuratExpRapaport.Text = txtRapaport.Text;
                //}
                //else
                //{
                //  txtSuratExpRapaport.Text = Convert.ToString(Val.Val(txtSuratExpRapaport.Text) == 0 ? Val.Val(txtRapaport.Text) : Val.Val(txtSuratExpRapaport.Text));
                //}
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

        public void FindRap_ForRepairing()
        {
            try
            {
                if (Val.ToString(txtKapanName.Text).Trim().Length == 0 || Val.ToString(txtPacketNo.Text).Trim().Length == 0 || Val.ToString(txtTag.Text).Trim().Length == 0)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;


                clsFindRapRChk = new Trn_RapSaveProperty();

                clsFindRapRChk.GCARAT = 0;
                clsFindRapRChk.GCUTCODE = "";
                clsFindRapRChk.GPOLCODE = "";
                clsFindRapRChk.GSYMCODE = "";

                if (clsFindRapRChk.SHAPECODE == "" || clsFindRapRChk.COLORCODE == "" || clsFindRapRChk.CLARITYCODE == "")
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                clsFindRapRChk = ObjRap.FindRapWithUpDown(clsFindRapRChk);
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
            if (txtKapanName.Text.Trim() == string.Empty)
            {
                Global.Message("Please Select Kapan First.");
                return;
            }
            //if (txtMainManager.Text.Trim().Length == 0)//Gunjan:27/03/2023
            //{
            //    Global.Message("Kapan Main Manager Is Required");
            //    txtMainManager.Focus();
            //    return;
            //}

            GetXPktBalance();
            GenerateMaxPacketNoKapanWise();
            txtTag.Text = "MA";
        }
        public void Fetch_SetComboBox(AxonContLib.cComboBox Combo, IList<DataStructureGrading> pData, int Value)
        {
            foreach (DataStructureGrading data in pData)
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
        public void Fetch_SetComboBoxRepairing(AxonContLib.cComboBox Combo, IList<DataStructureGradingRepairing> pData, int Value)
        {
            foreach (DataStructureGradingRepairing data in pData)
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
                    FrmSearch.mSearchText = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
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
                        //pIntDept_ID = Val.ToInt32(FrmSearch.mDRow["DEPARTMENT_ID"]);
                        //#P : 07-09-2020
                        SelectedEmployeeRightsProperty = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(Val.ToInt64(txtEmployee.Tag));
                        //if (SelectedEmployeeRightsProperty.ISCONFIRMGRADER)
                        //{
                        //    CmbGrdResultStatus.Enabled = true;
                        //    CmbGrdResultStatus.SelectedIndex = 0;
                        //}
                        //else
                        //{
                        //    CmbGrdResultStatus.SelectedItem = "CONFIRM";
                        //    CmbGrdResultStatus.Enabled = false;
                        //}
                        //End : #P : 07-09-2020
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
                FrmSearch.mSearchText = "EMPLOYEECODE";
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

                txtPassForDisplayBack_TextChanged(null, null);

                if (D.Length != 0)
                {
                    DataRow DRow = D[0];
                    CmbPrdType.Tag = Val.ToString(DRow["PrdType_ID"]);
                    DRow = null;
                }

                D = null;

                //txtEmployee.Enabled = EmployeeRightsProperty.RAPCHANGEEMPLOYEE;

                if (Name == "AP")
                {
                    txtEmployee.Enabled = true;
                }

                if (Val.ToInt(CmbPrdType.Tag) == 8)
                {
                    panel6.Visible = false;
                    MainGrid.Visible = false;
                    PnlUpDown.Visible = false;
                    panel4.Visible = false;
                    panel5.Visible = true;
                    PnlBtn.Visible = true;
                    pnlParameter.Visible = true;
                }

                else if (Val.ToInt(CmbPrdType.Tag) == 9)
                {
                    panel6.Visible = true;
                    panel6.Dock = DockStyle.Fill;
                    MainGrid.Visible = true;
                    MainGrid.Dock = DockStyle.Fill;
                    PnlUpDown.Visible = true;
                    PnlUpDown.Dock = DockStyle.Bottom;
                    //panel4.Visible = true;
                }
                else
                {
                    panel6.Visible = false;
                    MainGrid.Visible = false;
                    PnlUpDown.Visible = false;
                    panel4.Visible = false;
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

                if (Val.ToInt(CmbPrdType.Tag) == 11)
                {
                    //Val.ToInt(CmbPrdType.Tag) == 9 ||  || Val.ToInt(CmbPrdType.Tag) == 12    //Add : Pinali : 27-09-2019
                    lblEmployee.Text = "Lab";
                    PnlUpDown.Visible = false;
                    panel4.Visible = false;
                    panel5.Visible = true;
                    PnlBtn.Visible = true;
                    pnlParameter.Visible = true;
                }
                else
                {
                    lblEmployee.Text = "Emp";
                }

                BtnClear_Click(null, null);

                txtEmployee.Focus();
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
                    FrmSearch.mSearchText = "KAPANNAME";
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
                    FrmSearch.mSearchText = "PACKETNO";
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
                    FrmSearch.mSearchText = "SRNO,TAG";
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
            Calculation();
        }


        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            FindRap();
            //FindRap_ForRepairing();
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

        private void CmbBInC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AxonContLib.cComboBox combo = sender as AxonContLib.cComboBox;
                if (combo == null)
                {
                    return;
                }
                DataStructureGrading selectedDataStructure = combo.SelectedItem as DataStructureGrading;
                if (selectedDataStructure == null)
                {
                    Global.MessageError("You didn't select anything at the moment");
                }
                else
                {
                    if (combo.AccessibleDescription == "SHAPE")
                    {
                        CmbShape.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbShape.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbShape.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "COLOR")
                    {
                        CmbColor.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbColor.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbColor.Text = Val.ToString(selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "CLARITY")
                    {
                        CmbClarity.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbClarity.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbClarity.Text = Val.ToString(selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "CUT")
                    {
                        CmbCut.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbCut.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbCut.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "POLISH")
                    {
                        CmbPol.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbPol.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbPol.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "SYMMETRY")
                    {
                        CmbSym.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbSym.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbSym.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "FLUORESCENCE")
                    {
                        CmbFL.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbFL.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbFL.Text = Val.ToString(selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "NATTS")
                    {
                        CmbNatts.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbNatts.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbNatts.Text = Val.ToString(selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LBLC")
                    {
                        CmbLBLC.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbLBLC.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbLBLC.Text = Val.ToString(selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LAB")
                    {
                        CmbLab.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbLab.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbLab.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "COLORSHADE")
                    {
                        CmbColorShade.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbColorShade.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbColorShade.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "BLACK")
                    {
                        CmbBInC.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbBInC.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbBInC.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "WHITE")
                    {
                        CmbWInC.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbWInC.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbWInC.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "OPEN")
                    {
                        CmbOInC.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbOInC.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbOInC.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "PAVALION")
                    {
                        CmbPav.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbPav.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbPav.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "MILKY")
                    {
                        CmbMilky.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbMilky.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbMilky.Text = Val.ToString(selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LUSTER")
                    {
                        CmbLuster.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbLuster.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbLuster.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "EYECLEAN")
                    {
                        CmbEyeClean.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbEyeClean.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbEyeClean.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "HEARTANDARROW")
                    {
                        CmbHA.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbHA.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbHA.Text = Val.ToString(selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "TENSION")
                    {
                        CmbTension.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbTension.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbTension.Text = Val.ToString(selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "NATURAL")
                    {
                        CmbNatural.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbNatural.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbNatural.Text = Val.ToString(selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "GRAIN")
                    {
                        CmbGrain.Tag = Val.ToString(selectedDataStructure.PARA_ID);
                        CmbGrain.AccessibleName = Val.ToString(selectedDataStructure.PARACODE);
                        CmbGrain.Text = Val.ToString(selectedDataStructure.PARANAME);
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
                double DouCarat = 0, DouLabCharge = 0, DouSuratExpAfterDisc = 0, DouSuratExpAfterPricePerCarat = 0, DouSuratExpAfterAmount = 0;
                DouCarat = Val.Val(txtCarat.Text);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void CmbGrdResultStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void CmbCurrGrdResultStatus_SelectedIndexChanged(object sender, EventArgs e)
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
        private void lblLatestHeliumDetail_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTabHelDetail = new BOFindRap().GetSinglePacketHeliumDetail(Val.ToString(txtKapanName.Text), Val.ToInt32(txtPacketNo.Text), Val.ToString(txtTag.Text));

                if (DTabHelDetail.Rows.Count <= 0)
                {
                    Global.Message("Helium Data Not Found For This Packet..");
                    return;
                }

                DataRow Dr = DTabHelDetail.Rows[0];

                txtDiaMin.Text = Val.ToString(Dr["DIAMIN"]);
                txtDiaMax.Text = Val.ToString(Dr["DIAMAX"]);
                txtHeight.Text = Val.ToString(Dr["HEIGHT"]);
                txtRatio.Text = Val.ToString(Dr["RATIO"]);
                txtTablePC.Text = Val.ToString(Dr["TABLEPC"]);
                txtTotalDepth.Text = Val.ToString(Dr["TOTALDEPTH"]);

                FindRap();
                //FindRap_ForRepairing();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtGradingEmployee_KeyPress(object sender, KeyPressEventArgs e)
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
                    FrmSearch.mSearchText = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();

                    this.Cursor = Cursors.WaitCursor;
                    //FrmSearch.DTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtGradingEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtGradingEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {

                if (e.Clicks <= 1 || e.RowHandle < 0)
                {
                    return;
                }


                DataRow DrGrdDet = GrdDet.GetFocusedDataRow();

                if (Val.ToBoolean(DrGrdDet["ISCONFIRMGRADER"]) == false)
                {
                    Global.MessageError("You Cant't Select " + Val.ToString(DrGrdDet["EMPLOYEECODE"]) + " Employee Detail Because It's Not Confirm Grader..");
                    return;
                }


                txtGradingEmployee.Text = Val.ToString(DrGrdDet["EMPLOYEECODE"]);
                txtGradingEmployee.Tag = Val.ToString(DrGrdDet["EMPLOYEE_ID"]);

                //Check Packet Current LabResult Status : #P : 29-05-2020
                Trn_RapSaveProperty PropertyStatus = new Trn_RapSaveProperty();
                PropertyStatus.PACKET_ID = Val.ToInt64(txtTag.Tag);
                PropertyStatus.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                PropertyStatus.EMPLOYEE_ID = Val.ToInt64(txtGradingEmployee.Tag);
                PropertyStatus = ObjRap.CheckLabPricingValdiation(PropertyStatus);
                if (PropertyStatus.ReturnMessageType == "FAIL")
                {
                    BtnSave.Enabled = false;
                    this.Cursor = Cursors.Default;
                    Global.MessageError(PropertyStatus.ReturnMessageDesc);
                    Clear();
                    return;
                }
                PropertyStatus = null;
                //End : Check Packet Current LabResult Status : #P : 29-05-2020
                BtnSave.Enabled = true;
                DataTable DTab = ObjRap.GetSinglePrdGrdPricingData(8, Val.ToString(txtTag.Tag), Val.ToInt64(txtGradingEmployee.Tag), 0); //Grading
                DataRow DRow = DTab.Rows[0];
                txtRemark.Text = Val.ToString(DRow["REMARK"]);
                CmbLabProcess.SelectedItem = Val.ToString(DRow["LABPROCESS"]);
                CmbLabSelection.SelectedItem = Val.ToString(DRow["LABSELECTION"]);
                txtDiaMin.Text = Val.ToString(DRow["DIAMIN"]);
                txtDiaMax.Text = Val.ToString(DRow["DIAMAX"]);
                txtHeight.Text = Val.ToString(DRow["HEIGHT"]);

                txtTablePC.Text = Val.ToString(DRow["HELIUMTABLEPC"]);
                txtRatio.Text = Val.ToString(DRow["HELIUMRATIO"]);
                Fetch_SetComboBox(CmbShape, DataSHAPE, Val.ToInt(DRow["SHAPE_ID"]));
                Fetch_SetComboBox(CmbColor, DataCOLOR, Val.ToInt(DRow["COLOR_ID"]));
                Fetch_SetComboBox(CmbClarity, DataCLARITY, Val.ToInt(DRow["CLARITY_ID"]));
                Fetch_SetComboBox(CmbCut, DataCUT, Val.ToInt(DRow["CUT_ID"]));
                Fetch_SetComboBox(CmbPol, DataPOL, Val.ToInt(DRow["POL_ID"]));
                Fetch_SetComboBox(CmbSym, DataSYM, Val.ToInt(DRow["SYM_ID"]));
                Fetch_SetComboBox(CmbFL, DataFL, Val.ToInt(DRow["FL_ID"]));
                Fetch_SetComboBox(CmbNatts, DataNATTS, Val.ToInt(DRow["NATTS_ID"]));
                Fetch_SetComboBox(CmbLBLC, DataLBLC, Val.ToInt(DRow["LBLC_ID"]));
                Fetch_SetComboBox(CmbLab, DataLAB, Val.ToInt(DRow["LAB_ID"]));
                Fetch_SetComboBox(CmbColorShade, DataCS, Val.ToInt(DRow["COLORSHADE_ID"]));
                Fetch_SetComboBox(CmbBInC, DataBINC, Val.ToInt(DRow["BLACKINC_ID"]));
                Fetch_SetComboBox(CmbWInC, DataWINC, Val.ToInt(DRow["WHITEINC_ID"]));
                Fetch_SetComboBox(CmbOInC, DataOINC, Val.ToInt(DRow["OPENINC_ID"]));
                Fetch_SetComboBox(CmbPav, DataPAV, Val.ToInt(DRow["PAV_ID"]));
                Fetch_SetComboBox(CmbMilky, DataMILKY, Val.ToInt(DRow["MILKY_ID"]));
                Fetch_SetComboBox(CmbLuster, DataLUSTER, Val.ToInt(DRow["LUSTER_ID"]));
                Fetch_SetComboBox(CmbEyeClean, DataEYECLEAN, Val.ToInt(DRow["EYECLEAN_ID"]));
                Fetch_SetComboBox(CmbHA, DataHA, Val.ToInt(DRow["HA_ID"]));
                Fetch_SetComboBox(CmbTension, DataTENSION, Val.ToInt(DRow["TENSION_ID"]));
                Fetch_SetComboBox(CmbNatural, DataNATURAL, Val.ToInt(DRow["NATURAL_ID"]));
                Fetch_SetComboBox(CmbGrain, DataGRAIN, Val.ToInt(DRow["GRAIN_ID"]));

                CmbGrdResultStatus_SelectedIndexChanged(null, null);
                CmbRapDate.SelectedItem = Val.ToString(DRow["RAPDATE"]);

                txtCarat.Text = Val.ToString(DRow["CARAT"]);
                txtDiscount.Text = Val.ToString(DRow["DISCOUNT"]);
                txtRate.Text = Val.ToString(DRow["PRICEPERCARAT"]);
                txtAmount.Text = Val.ToString(DRow["AMOUNT"]);

                txtRapaport.Text = Val.ToString(DRow["RAPAPORT"]);

                txtGiaNonGia.Text = Val.ToString(DRow["GIANONGIA"]);
                mStrReportNo = Val.ToString(DRow["REPORTNO"]);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtPassForDisplayBack_Validated(object sender, EventArgs e)
        {
            try
            {
                //if (Val.ToString(txtPassForDisplayBack.Tag) == Val.ToString(txtPassForDisplayBack.Text))
                //{
                //    txtDiscount.ReadOnly = false;
                //}
                //else
                //{
                //    txtDiscount.ReadOnly = true;
                //}

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtDiscount_Validated(object sender, EventArgs e)
        {
            double DouPricePerCarat = 0, DouAmount = 0, DouMainRapaport = 0; ;

            DouMainRapaport = Val.Val(txtRapaport.Text);
            DouPricePerCarat = Val.Val(txtDiscount.Text) == 0 ? 0 : Math.Round((DouMainRapaport - ((DouMainRapaport * Val.Val(txtDiscount.Text)) / 100)), 2);
            DouAmount = Math.Round((DouPricePerCarat * Val.Val(txtCarat.Text)), 2);

            txtRate.Text = Val.ToString(DouPricePerCarat);
            txtAmount.Text = Val.ToString(DouAmount);
        }

        private void BtnUpDown_Click(object sender, EventArgs e)
        {
            if (IsDownImage)
            {
                BtnUpDown.Image = AxoneMFGRJ.Properties.Resources.A3;
                pnlParameter.Visible = false;
                panel6.Visible = false;
                MainGrid.Visible = false;
                PnlBtn.Visible = false;
                panel5.Visible = false;
                panel4.Visible = true;
                MainGrdExcel.Visible = true;
                panel7.Visible = true;
                MainGrid.Dock = DockStyle.Fill;
                PnlUpDown.Dock = DockStyle.Top;
                IsDownImage = false;
            }
            else
            {
                BtnUpDown.Image = AxoneMFGRJ.Properties.Resources.A4;
                IsDownImage = true;
                pnlParameter.Visible = true;
                panel6.Visible = true;
                panel6.Dock = DockStyle.Fill;
                MainGrid.Visible = true;
                MainGrid.Dock = DockStyle.Fill;
                panel5.Visible = true;
                PnlBtn.Visible = true;
                MainGrdExcel.Visible = false;
                panel7.Visible = false;
                panel4.Visible = false;
                PnlUpDown.Dock = DockStyle.Bottom;

            }
        }

        private void lblDownload_Click(object sender, EventArgs e)
        {
            string StrFilePathDestination = "";

            if (RbtSurat.Checked)
            {
                StrFilePathDestination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\MumbaiGradingSuratExpFileUploadDetail_" + DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM") + DateTime.Now.Day.ToString() + ".xlsx";
                if (File.Exists(StrFilePathDestination))
                {
                    File.Delete(StrFilePathDestination);
                }
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Format\\MumbaiGradingSuratExpFileUploadDetail.xlsx", StrFilePathDestination);
            }
            else if (RbtMumbai.Checked)
            {
                StrFilePathDestination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\MumbaiGradingMumbaiExpFileUploadDetail_" + DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM") + DateTime.Now.Day.ToString() + ".xlsx";
                if (File.Exists(StrFilePathDestination))
                {
                    File.Delete(StrFilePathDestination);
                }
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Format\\MumbaiGradingMumbaiExpFileUploadDetail.xlsx", StrFilePathDestination);
            }

            System.Diagnostics.Process.Start(StrFilePathDestination, "CMD");
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
            //try
            //{
            this.Cursor = Cursors.WaitCursor;

            DtabExcelData.Rows.Clear();
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

                DtabExcelData = GetDataTableFromExcel(destinationPath, true);

                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }
            }

            if (DtabExcelData.Rows.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                return;
            }
            else
            {
                for (int Intcol = 0; Intcol < DtabExcelData.Columns.Count; Intcol++)
                {
                    if (Val.ToString("Packet No,PacketNo").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PACKETNO");

                    if (Val.ToString("SuratExp_Disc").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SURATEXP_DISC");

                    if (Val.ToString("SURATEXP_PERCTS").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SURATEXP_PERCTS");

                    if (Val.ToString("MumbaiExp_Disc").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("MUMBAIEXP_DISC");

                    if (Val.ToString("MUMBAIEXP_PERCTS").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("MUMBAIEXP_PERCTS");

                    if (Val.ToString("Emp").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("EMP");
                }

                MainGrdExcel.DataSource = DtabExcelData;
                GrdExcle.RefreshData();
                GrdExcle.BestFitColumns();
            }

            this.Cursor = Cursors.Default;

            //}
            //catch (Exception Ex)
            //{
            //    Global.Message(Ex.Message.ToString());
            //    this.Cursor = Cursors.Default;
            //    return;
            //}

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
                OpenFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx;";
                if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtFileName.Text = OpenFileDialog.FileName;

                    GetExcelSheetNames(txtFileName.Text);
                    CmbSheetName.SelectedIndex = 0;

                }
                OpenFileDialog.Dispose();
                OpenFileDialog = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString() + "InValid File Name");
            }
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
                DTabUploadData = GetTableOfSelectedRows(GrdExcle, true);

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

                string UploadDetail = StrXMLValues;
                string StrOpe = "";

                if (RbtSurat.Checked)
                {
                    StrOpe = "SURAT";
                }
                else
                {
                    StrOpe = "MUMBAI";
                }

                Property = ObjRap.UpdateByGrdExpDetailWithExcel(Property, UploadDetail, StrOpe);

                string ReturnMessageDesc = Property.ReturnMessageDesc;
                string ReturnMessageType = Property.ReturnMessageType;


                if (ReturnMessageType == "SUCCESS")
                {
                    Global.Message(ReturnMessageDesc);
                }
                else
                {
                    Global.Message(ReturnMessageDesc);
                    return;
                    txtEmployee.Focus();
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
                return;
            }

        }

        private void BtnClearExcel_Click(object sender, EventArgs e)
        {
            txtFileName.Text = string.Empty;
            DtabExcelData.Rows.Clear();
            MainGrdExcel.DataSource = null;
        }

        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
        }

        private void RbtSurat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RbtSurat.Checked)
                {
                    GrdExcle.Columns["MUMBAIEXP_DISC"].Visible = false;
                    GrdExcle.Columns["MUMBAIEXP_PERCTS"].Visible = false;
                    GrdExcle.Columns["SURATEXP_DISC"].Visible = true;
                    GrdExcle.Columns["SURATEXP_PERCTS"].Visible = true;

                    GrdExcle.Columns["PACKETNO"].VisibleIndex = 1;
                    GrdExcle.Columns["EMP"].VisibleIndex = 2;
                    GrdExcle.Columns["SURATEXP_DISC"].VisibleIndex = 3;
                    GrdExcle.Columns["SURATEXP_PERCTS"].VisibleIndex = 4;
                    GrdExcle.BestFitColumns();

                }
                else
                {
                    GrdExcle.Columns["MUMBAIEXP_DISC"].Visible = true;
                    GrdExcle.Columns["MUMBAIEXP_PERCTS"].Visible = true;
                    GrdExcle.Columns["SURATEXP_DISC"].Visible = false;
                    GrdExcle.Columns["SURATEXP_PERCTS"].Visible = false;

                    GrdExcle.Columns["PACKETNO"].VisibleIndex = 1;
                    GrdExcle.Columns["EMP"].VisibleIndex = 2;
                    GrdExcle.Columns["MUMBAIEXP_DISC"].VisibleIndex = 3;
                    GrdExcle.Columns["MUMBAIEXP_PERCTS"].VisibleIndex = 4;
                    GrdExcle.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }

        private void ChkLatestRapForSuratExp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Calculation();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }

        }

        public void GetXPktBalance()
        {
            try
            {
                DataRow DR = ObjPacket.GetMakPktBalancePcsCarat(Val.ToInt64(txtKapanName.Tag), Val.ToInt64(txtMainPacketNo.Tag));
                if (DR == null)
                {
                    lblBalance.Text = "";
                }
                else
                {
                    lblBalance.Text = Val.ToString(DR["BALANCECARAT"]);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);

            }
        }
        public void GenerateMaxPacketNoKapanWise()
        {
            txtPacketNo.Text = ObjPacket.FindNewMakPacketNo(Val.ToInt64(txtKapanName.Tag)).ToString();
        }

        private void txtKapanName_Validating(object sender, CancelEventArgs e)
        {
            //BtnContinue_Click(null, null);
        }

        private void txtMainManager_KeyPress(object sender, KeyPressEventArgs e)//Gunjan:27/03/2023
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {

        }
    }
    public class DataStructureGrading
    {
        public int PARA_ID { get; set; }
        public string PARACODE { get; set; }
        public string PARANAME { get; set; }
    }
    public class DataStructureGradingRepairing
    {
        public int PARA_ID { get; set; }
        public string PARACODE { get; set; }
        public string PARANAME { get; set; }
    }
}
