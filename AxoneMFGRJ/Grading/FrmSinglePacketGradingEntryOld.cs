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
using BusLib.View;
using OfficeOpenXml;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.VisualBasic;

namespace AxoneMFGRJ.Grading
{
    public partial class FrmSinglePacketGradingEntryOld : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOFindRap ObjRap = new BOFindRap();

        DataTable DTabPrdType = new DataTable();
        DataTable DTabRapDate = new DataTable();
        DataTable DTabParameter = new DataTable();
        DataTable DTabGrdDet = new DataTable();
        Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();
        Trn_RapSaveProperty clsFindRapRChk = new Trn_RapSaveProperty();
        DataTable DtabExcelData = new DataTable();
        DataTable DTab = new DataTable();

        DataTable DtabMakable = new DataTable();

        double StrSuratExpBeforeRapaport = 0;

        bool IsDownImage = true;

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

        public FrmSinglePacketGradingEntryOld()
        {
            InitializeComponent();
        }

        public void ShowForm()
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
                if (Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9 || Val.ToInt(DRow["PRDTYPE_ID"]) == 11 || Val.ToInt(DRow["PRDTYPE_ID"]) == 4)
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

            RbtSurat_CheckedChanged(null, null);



            DtabMakable.Columns.Add("BARCODE", typeof(string));
            DtabMakable.Columns.Add("KAPANNAME", typeof(string));
            DtabMakable.Columns.Add("MKBLEMPLOYEECODE", typeof(string));
            DtabMakable.Columns.Add("PACKETNO", typeof(Int32));
            DtabMakable.Columns.Add("TAG", typeof(string));
            DtabMakable.Columns.Add("PKTSERIALNO", typeof(Int64));
            DtabMakable.Columns.Add("MKBLCOLORNAME", typeof(string));
            DtabMakable.Columns.Add("MKBLCLARITYNAME", typeof(string));
            DtabMakable.Columns.Add("MKBLCUTNAME", typeof(string));
            DtabMakable.Columns.Add("MKBLFLNAME", typeof(string));
            DtabMakable.Columns.Add("MKBLSHAPENAME", typeof(string));
            DtabMakable.Columns.Add("MKBLPRDCARAT", typeof(double));
            DtabMakable.Columns.Add("MKBLBALANCECARAT", typeof(double));
            DtabMakable.Columns.Add("MKBLPRDAMOUNT", typeof(double));
            DtabMakable.Columns.Add("SYMMETRY", typeof(string));
            DtabMakable.Columns.Add("POLISH", typeof(string));


            if (MainGrid.RepositoryItems.Count == 0)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdData;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdData.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            GrdDet.EndUpdate();

           GrdData.BestFitColumns();
     
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

            DesignComboBoxForRepairing("SHAPE", CmbRChkShape, "PARACODE");
            DesignComboBoxForRepairing("COLOR", CmbRChkColor, "PARANAME");
            DesignComboBoxForRepairing("CLARITY", CmbRChkClarity, "PARANAME");
            DesignComboBoxForRepairing("CUT", CmbRChkCut, "PARACODE");
            DesignComboBoxForRepairing("POLISH", CmbRChkPol, "PARACODE");
            DesignComboBoxForRepairing("SYMMETRY", CmbRChkSym, "PARACODE");
            DesignComboBoxForRepairing("FLUORESCENCE", CmbRChkFL, "PARANAME");
            DesignComboBoxForRepairing("NATTS", CmbRChkNatts, "PARANAME");
            DesignComboBoxForRepairing("LBLC", CmbRChkLBLC, "PARANAME");
            DesignComboBoxForRepairing("COLORSHADE", CmbRChkColorShade, "PARACODE");
            DesignComboBoxForRepairing("BLACK", CmbRChkBInC, "PARACODE");
            DesignComboBoxForRepairing("WHITE", CmbRChkWInC, "PARACODE");
            DesignComboBoxForRepairing("OPEN", CmbRChkOInC, "PARACODE");
            DesignComboBoxForRepairing("PAVALION", CmbRChkPav, "PARACODE");
            DesignComboBoxForRepairing("MILKY", CmbRChkMilky, "PARACODE");
            DesignComboBoxForRepairing("LUSTER", CmbRChkLuster, "PARACODE");
            DesignComboBoxForRepairing("EYECLEAN", CmbRChkEyeClean, "PARACODE");
            DesignComboBoxForRepairing("HEARTANDARROW", CmbRChkHA, "PARACODE");
            DesignComboBoxForRepairing("LAB", CmbRChkLab, "PARACODE");

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

        public void DesignComboBoxForRepairing(string pStrParaType, AxonContLib.cComboBox ComboBox, string pStrDisplayText)
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
                DataSHAPERep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataSHAPERep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkShape.AccessibleDescription = pStrParaType;
                CmbRChkShape.DataSource = DataSHAPERep;
                CmbRChkShape.DisplayMember = pStrDisplayText;
                CmbRChkShape.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "COLOR")
            {
                DataCOLORRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataCOLORRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkColor.AccessibleDescription = pStrParaType;
                CmbRChkColor.DataSource = DataCOLORRep;
                CmbRChkColor.DisplayMember = pStrDisplayText;
                CmbRChkColor.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "CLARITY")
            {
                DataCLARITYRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataCLARITYRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkClarity.AccessibleDescription = pStrParaType;
                CmbRChkClarity.DataSource = DataCLARITYRep;
                CmbRChkClarity.DisplayMember = pStrDisplayText;
                CmbRChkClarity.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "CUT")
            {
                DataCUTRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataCUTRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkCut.AccessibleDescription = pStrParaType;
                CmbRChkCut.DataSource = DataCUTRep;
                CmbRChkCut.DisplayMember = pStrDisplayText;
                CmbRChkCut.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "POLISH")
            {
                DataPOLRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataPOLRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkPol.AccessibleDescription = pStrParaType;
                CmbRChkPol.DataSource = DataPOLRep;
                CmbRChkPol.DisplayMember = pStrDisplayText;
                CmbRChkPol.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "SYMMETRY")
            {
                DataSYMRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataSYMRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkSym.AccessibleDescription = pStrParaType;
                CmbRChkSym.DataSource = DataSYMRep;
                CmbRChkSym.DisplayMember = pStrDisplayText;
                CmbRChkSym.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "FLUORESCENCE")
            {
                DataFLRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataFLRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkFL.AccessibleDescription = pStrParaType;
                CmbRChkFL.DataSource = DataFLRep;
                CmbRChkFL.DisplayMember = pStrDisplayText;
                CmbRChkFL.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "NATTS")
            {
                DataNATTSRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataNATTSRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkNatts.AccessibleDescription = pStrParaType;
                CmbRChkNatts.DataSource = DataNATTSRep;
                CmbRChkNatts.DisplayMember = pStrDisplayText;
                CmbRChkNatts.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "LBLC")
            {
                DataLBLCRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataLBLCRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkLBLC.AccessibleDescription = pStrParaType;
                CmbRChkLBLC.DataSource = DataLBLCRep;
                CmbRChkLBLC.DisplayMember = pStrDisplayText;
                CmbRChkLBLC.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "COLORSHADE")
            {
                DataCSRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataCSRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkColorShade.AccessibleDescription = pStrParaType;
                CmbRChkColorShade.DataSource = DataCSRep;
                CmbRChkColorShade.DisplayMember = pStrDisplayText;
                CmbRChkColorShade.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "BLACK")
            {
                DataBINCRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataBINCRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkBInC.AccessibleDescription = pStrParaType;
                CmbRChkBInC.DataSource = DataBINCRep;
                CmbRChkBInC.DisplayMember = pStrDisplayText;
                CmbRChkBInC.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "WHITE")
            {
                DataWINCRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataWINCRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkWInC.AccessibleDescription = pStrParaType;
                CmbRChkWInC.DataSource = DataWINCRep;
                CmbRChkWInC.DisplayMember = pStrDisplayText;
                CmbRChkWInC.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "OPEN")
            {
                DataOINCRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataOINCRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkOInC.AccessibleDescription = pStrParaType;
                CmbRChkOInC.DataSource = DataOINCRep;
                CmbRChkOInC.DisplayMember = pStrDisplayText;
                CmbRChkOInC.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "PAVALION")
            {
                DataPAVRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataPAVRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkPav.AccessibleDescription = pStrParaType;
                CmbRChkPav.DataSource = DataPAVRep;
                CmbRChkPav.DisplayMember = pStrDisplayText;
                CmbRChkPav.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "MILKY")
            {
                DataMILKYRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataMILKYRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkMilky.AccessibleDescription = pStrParaType;
                CmbRChkMilky.DataSource = DataMILKYRep;
                CmbRChkMilky.DisplayMember = pStrDisplayText;
                CmbRChkMilky.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "LUSTER")
            {
                DataLUSTERRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataLUSTERRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkLuster.AccessibleDescription = pStrParaType;
                CmbRChkLuster.DataSource = DataLUSTERRep;
                CmbRChkLuster.DisplayMember = pStrDisplayText;
                CmbRChkLuster.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "EYECLEAN")
            {
                DataEYECLEANRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataEYECLEANRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkEyeClean.AccessibleDescription = pStrParaType;
                CmbRChkEyeClean.DataSource = DataEYECLEANRep;
                CmbRChkEyeClean.DisplayMember = pStrDisplayText;
                CmbRChkEyeClean.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "HEARTANDARROW")
            {
                DataHARep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataHARep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkHA.AccessibleDescription = pStrParaType;
                CmbRChkHA.DataSource = DataHARep;
                CmbRChkHA.DisplayMember = pStrDisplayText;
                CmbRChkHA.ValueMember = "PARA_ID";
            }
            else if (pStrParaType == "LAB")
            {
                DataLABRep.Add(new DataStructureGradingRepairing() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DataLABRep.Add(new DataStructureGradingRepairing() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbRChkLab.AccessibleDescription = pStrParaType;
                CmbRChkLab.DataSource = DataLABRep;
                CmbRChkLab.DisplayMember = pStrDisplayText;
                CmbRChkLab.ValueMember = "PARA_ID";
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
                if (CmbGrdResultStatus.Text.Trim().Equals(string.Empty) || CmbGrdResultStatus.Text.Trim().ToUpper().Equals("NONE"))
                {
                    Global.Message("Please Select Grd Result.");
                    CmbGrdResultStatus.Focus();
                    return;
                }

                if (((Val.ToInt(CmbPrdType.Tag) == 8) || (Val.ToInt(CmbPrdType.Tag) == 9)) && CmbLabProcess.SelectedIndex == 0)
                {
                    Global.MessageError("You Have To Select Graph/NonGraph  For BY Transfer While Making Grading Entry");
                    CmbLabProcess.Focus();
                    return;
                }

                //if((Val.ToString(CmbCurrGrdResultStatus.Text) == "REPAIRING" || Val.ToString(CmbCurrGrdResultStatus.Text) == "RECHECK") && Val.ToString(CmbGrdResultStatus.Text) == "CONFIRM")
                if ((Val.ToString(CmbCurrGrdResultStatus.Text) != Val.ToString(CmbGrdResultStatus.Text)) &&
                    (Val.ToString(CmbCurrGrdResultStatus.Text) != "REPAIRINGCONFIRM" && Val.ToString(CmbCurrGrdResultStatus.Text) != "RECHECKCONFIRM"
                      && Val.ToString(CmbCurrGrdResultStatus.Text) != "REPAIRINGCANCEL" && Val.ToString(CmbCurrGrdResultStatus.Text) != "RECHECKCANCEL" && Val.ToString(CmbCurrGrdResultStatus.Text) != "NONE")
                   )
                {
                    Global.MessageError("You Can't Update Records With '" + Val.ToString(CmbGrdResultStatus.Text) + "' Status Coz Stone Is In '" + Val.ToString(CmbCurrGrdResultStatus.Text) + "' ");
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

                Property.SHAPE_ID = Val.ToInt32(CmbShape.Tag);
                Property.SHAPENAME = Val.ToString(CmbShape.Text);

                Property.COLOR_ID = Val.ToInt32(CmbColor.Tag);
                Property.COLORNAME = Val.ToString(CmbColor.Text);

                Property.CLARITY_ID = Val.ToInt32(CmbClarity.Tag);
                Property.CLARITYNAME = Val.ToString(CmbClarity.Text);

                Property.CARAT = Val.Val(txtCarat.Text);
                Property.BALANCECARAT = Val.Val(lblBalance.Text);

                Property.LABPROCESS = Val.ToString(CmbLabProcess.SelectedItem);

                Property.DIAMIN = Val.Val(txtDiaMin.Text);
                Property.DIAMAX = Val.Val(txtDiaMax.Text);
                Property.HEIGHT = Val.Val(txtHeight.Text);

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
                FindRap_ForRepairing();

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

                //Add : Pinali : 26-05-2019
                Property.SURATEXPLABCHARGE = Val.Val(txtSuratExpLabCharge.Text);
                Property.SURATEXPBEFORERAPAPORT = Val.Val(txtSuratExpBeforeDiscount.Text) == 0 ? 0 : Val.Val(txtSuratExpRapaport.Text);
                Property.SURATEXPBEFOREDISCOUNT = Val.Val(txtSuratExpBeforeDiscount.Text);
                Property.SURATEXPBEFOREPRICEPERCARAT = Val.Val(txtSuratExpBeforePricePerCarat.Text);
                Property.SURATEXPBEFOREAMOUNT = Val.Val(txtSuratExpBeforeAmount.Text);

                Property.SURATEXPAFTERRAPAPORT = Val.Val(txtSuratExpAfterDiscount.Text) == 0 ? 0 : Val.Val(txtSuratExpRapaport.Text);
                Property.SURATEXPAFTERDISCOUNT = Val.Val(txtSuratExpAfterDiscount.Text);
                Property.SURATEXPAFTERPRICEPERCARAT = Val.Val(txtSuratExpAfterPricePerCarat.Text);
                Property.SURATEXPAFTERAMOUNT = Val.Val(txtSuratExpAfterAmount.Text);


                //Add : #P : 13-01-2021

                double DouMumExpLabCharge = 0;
                double DouMumExpBeforeRapaport = 0;
                double DouMumExpBeforeDiscount = 0;
                double DouMumExpBeforePricePerCarat = 0;
                double DouMumExpBeforeAmount = 0;

                double DouMumExpAfterRapaport = 0;
                double DouMumExpAfterDiscount = 0;
                double DouMumExpAfterPricePerCarat = 0;
                double DouMumExpAfterAmount = 0;

                if (Val.ToInt(CmbPrdType.Tag) == 9)
                {
                    DouMumExpLabCharge = (Val.ToString(CmbLabProcess.Text) == "GRAPH" || Val.ToString(CmbLabProcess.Text) == "ORDER") ? Val.Val(txtSuratExpLabCharge.Text) : 0;
                    DouMumExpBeforeRapaport = (Val.ToString(CmbLabProcess.Text) == "GRAPH" || Val.ToString(CmbLabProcess.Text) == "ORDER") && Val.Val(txtSuratExpBeforeDiscount.Text) != 0 ? Val.Val(txtSuratExpRapaport.Text) : 0;
                    DouMumExpBeforeDiscount = (Val.ToString(CmbLabProcess.Text) == "GRAPH" || Val.ToString(CmbLabProcess.Text) == "ORDER") ? Val.Val(txtSuratExpBeforeDiscount.Text) : 0;
                    DouMumExpBeforePricePerCarat = (Val.ToString(CmbLabProcess.Text) == "GRAPH" || Val.ToString(CmbLabProcess.Text) == "ORDER") ? Val.Val(txtSuratExpBeforePricePerCarat.Text) : 0;
                    DouMumExpBeforeAmount = (Val.ToString(CmbLabProcess.Text) == "GRAPH" || Val.ToString(CmbLabProcess.Text) == "ORDER") ? Val.Val(txtSuratExpBeforeAmount.Text) : 0;

                    DouMumExpAfterRapaport = (Val.ToString(CmbLabProcess.Text) == "GRAPH" || Val.ToString(CmbLabProcess.Text) == "ORDER") && Val.Val(txtSuratExpAfterDiscount.Text) != 0 ? Val.Val(txtSuratExpRapaport.Text) : 0;
                    DouMumExpAfterDiscount = (Val.ToString(CmbLabProcess.Text) == "GRAPH" || Val.ToString(CmbLabProcess.Text) == "ORDER") ? Val.Val(txtSuratExpAfterDiscount.Text) : 0;
                    DouMumExpAfterPricePerCarat = (Val.ToString(CmbLabProcess.Text) == "GRAPH" || Val.ToString(CmbLabProcess.Text) == "ORDER") ? Val.Val(txtSuratExpAfterPricePerCarat.Text) : 0;
                    DouMumExpAfterAmount = (Val.ToString(CmbLabProcess.Text) == "GRAPH" || Val.ToString(CmbLabProcess.Text) == "ORDER") ? Val.Val(txtSuratExpAfterAmount.Text) : 0;
                }

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



                Property.GRDRESULTSTATUS = Val.ToString(CmbGrdResultStatus.Text);
                //Property.CURRENTGRDRESULTSTATUS = Val.ToString(CmbCurrGrdResultStatus.Text) != "REPAIRINGCONFIRM" ? Val.ToString(CmbGrdResultStatus.Text) : Val.ToString(CmbCurrGrdResultStatus.Text);
                Property.CURRENTGRDRESULTSTATUS = Val.ToString(CmbGrdResultStatus.Text);

                Property.HELIUMTABLEPC = Val.ToString(txtTablePC.Text);
                Property.HELIUMRATIO = Val.ToString(txtRatio.Text);
                Property.HELIUMTOTALDEPTH = Val.ToString(txtTotalDepth.Text);
                //End : Pinali : 26-05-2019

                Property.ISNOBGM = ChkNOBGM.Checked;
                Property.ISNOBLACK = ChkNOBlack.Checked;

                Property.ENTRYMODE = lblMode.Text;

                Property.ISPCNGRDBYLABENTRY = Val.ToBoolean(ChkISPcnGrdByLabEntry.Checked);
                Property.PCNGRDBYLAB_ID = Val.ToInt64(ChkISPcnGrdByLabEntry.Tag) == 0 ? 0 : Val.ToInt64(ChkISPcnGrdByLabEntry.Tag);
                Property.REPORTNO = Val.ToString(mStrReportNo);

                Property.ISCONFIRMGRADER = SelectedEmployeeRightsProperty.ISCONFIRMGRADER;

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
                    if (Val.ToInt(CmbPrdType.Tag) == 8 && CmbGrdResultStatus.Text == "REPAIRING")
                    {
                        SaveRChkRepData(19, "GRADING REPAIRING");
                    }
                }

                this.Cursor = Cursors.Default;

                Global.Message("****  " + Val.ToString(CmbPrdType.SelectedItem) + "   *****\n\nSUCCESSFULLY SAVED OF " + txtKapanName.Text + "/" + txtPacketNo.Text + "/" + txtTag.Text);

                DataRow Ds = DtabMakable.NewRow();
                Ds["BARCODE"] = Val.ToString(txtBarcode.Text);
                Ds["KAPANNAME"] = Val.ToString(txtKapanName.Text);
                Ds["MKBLEMPLOYEECODE"] = Val.ToString(txtEmployee.Text);
                Ds["PACKETNO"] = Val.ToInt(txtPacketNo.Text);
                Ds["TAG"] = Val.ToString(txtTag.Text);
                Ds["PKTSERIALNO"] = Val.ToString(txtPktSerialNo.Text);
                Ds["MKBLCOLORNAME"] = Val.ToString(CmbColor.Text);
                Ds["MKBLCLARITYNAME"] = Val.ToString(CmbClarity.Text);
                Ds["MKBLCUTNAME"] = Val.ToString(CmbCut.Text);
                Ds["MKBLFLNAME"] = Val.ToString(CmbFL.Text);
                Ds["MKBLSHAPENAME"] = Val.ToString(CmbShape.Text);
                Ds["MKBLPRDCARAT"] = Val.Val(txtCarat.Text); ;
                Ds["MKBLBALANCECARAT"] = Val.Val(lblBalance.Text);
                Ds["MKBLPRDAMOUNT"] = Math.Round(clsFindRap.AMOUNT, 0); 
                Ds["SYMMETRY"] = Val.ToString(CmbSym.Text);
                Ds["POLISH"] = Val.ToString(CmbPol.Text);
                DtabMakable.Rows.Add(Ds);

                MainGrd.DataSource = DtabMakable;
                MainGrd.Refresh();
                GrdData.BestFitColumns();

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


            Int64 IntRChkPrdID = BOMaximumID.FindMaxID(BOMaximumID.IDTYPE.PRD_ID) ;
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

            PropertyRChk.SHAPE_ID = Val.ToInt(CmbRChkShape.Tag);
            PropertyRChk.CLARITY_ID = Val.ToInt(CmbRChkClarity.Tag);
            PropertyRChk.COLOR_ID = Val.ToInt(CmbRChkColor.Tag);
            PropertyRChk.CUT_ID = Val.ToInt(CmbRChkCut.Tag);
            PropertyRChk.POL_ID = Val.ToInt(CmbRChkPol.Tag);
            PropertyRChk.SYM_ID = Val.ToInt(CmbRChkSym.Tag);
            PropertyRChk.FL_ID = Val.ToInt(CmbRChkFL.Tag);
            PropertyRChk.LBLC_ID = Val.ToInt(CmbRChkLBLC.Tag);
            PropertyRChk.NATTS_ID = Val.ToInt(CmbRChkNatts.Tag);

            PropertyRChk.COLORSHADE_ID = Val.ToInt(CmbRChkColorShade.Tag);

            PropertyRChk.BLACKINC_ID = Val.ToInt(CmbRChkBInC.Tag);
            PropertyRChk.OPENINC_ID = Val.ToInt(CmbRChkOInC.Tag);
            PropertyRChk.WHITEINC_ID = Val.ToInt(CmbRChkWInC.Tag);
            PropertyRChk.PAV_ID = Val.ToInt(CmbRChkPav.Tag);
            PropertyRChk.MILKY_ID = Val.ToInt(CmbRChkMilky.Tag);
            PropertyRChk.LUSTER_ID = Val.ToInt(CmbRChkLuster.Tag);
            PropertyRChk.EYECLEAN_ID = Val.ToInt(CmbRChkEyeClean.Tag);
            PropertyRChk.HA_ID = Val.ToInt(CmbRChkHA.Tag);

            PropertyRChk.NATURAL_ID = Val.ToInt(CmbNatural.SelectedValue);
            PropertyRChk.GRAIN_ID = Val.ToInt(CmbGrain.SelectedValue);
            PropertyRChk.TENSION_ID = Val.ToInt(CmbTension.Tag);

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
            PropertyRChk.SURATEXPBEFORERAPAPORT = Val.Val(txtSuratExpBeforeDiscount.Text) == 0 ? 0 : Val.Val(txtSuratExpRapaport.Text);
            PropertyRChk.SURATEXPBEFOREDISCOUNT = Val.Val(txtSuratExpBeforeDiscount.Text);
            PropertyRChk.SURATEXPBEFOREPRICEPERCARAT = Val.Val(txtSuratExpBeforePricePerCarat.Text);
            PropertyRChk.SURATEXPBEFOREAMOUNT = Val.Val(txtSuratExpBeforeAmount.Text);

            PropertyRChk.SURATEXPAFTERRAPAPORT = Val.Val(txtSuratExpAfterDiscount.Text) == 0 ? 0 : Val.Val(txtSuratExpRapaport.Text);
            PropertyRChk.SURATEXPAFTERDISCOUNT = Val.Val(txtSuratExpAfterDiscount.Text);
            PropertyRChk.SURATEXPAFTERPRICEPERCARAT = Val.Val(txtSuratExpAfterPricePerCarat.Text);
            PropertyRChk.SURATEXPAFTERAMOUNT = Val.Val(txtSuratExpAfterAmount.Text);

            PropertyRChk.GRDRESULTSTATUS = Val.ToString(CmbGrdResultStatus.Text);
            PropertyRChk.CURRENTGRDRESULTSTATUS = Val.ToString(CmbCurrGrdResultStatus.Text) != "REPAIRINGCONFIRM" ? Val.ToString(CmbGrdResultStatus.Text) : Val.ToString(CmbCurrGrdResultStatus.Text);

            PropertyRChk.HELIUMTABLEPC = Val.ToString(txtRChkTablePC.Text);
            PropertyRChk.HELIUMRATIO = Val.ToString(txtRChkRatio.Text);
            PropertyRChk.HELIUMTOTALDEPTH = Val.ToString(txtRChkTotalDepth.Text);
            //End : Pinali : 26-05-2019

            PropertyRChk.ISNOBGM = ChkRChkNoBGM.Checked;
            PropertyRChk.ISNOBLACK = ChkNOBlack.Checked;

            PropertyRChk.ENTRYMODE = lblMode.Text;

            PropertyRChk.ISPCNGRDBYLABENTRY = Val.ToBoolean(ChkISPcnGrdByLabEntry.Checked);
            PropertyRChk.PCNGRDBYLAB_ID = Val.ToInt64(ChkISPcnGrdByLabEntry.Tag) == 0 ? 0 : Val.ToInt64(ChkISPcnGrdByLabEntry.Tag);
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

                txtPktSerialNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;

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

                ChkNOBGM.Checked = false;
                ChkNOBlack.Checked = false;

                CmbLabSelection.SelectedIndex = 0;
                CmbLabProcess.SelectedIndex = 0;
                CmbCurrGrdResultStatus.SelectedIndex = 0;
                CmbGrdResultStatus.SelectedIndex = 0;


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

                txtSuratExpLabCharge.Text = string.Empty;
                txtSuratExpBeforeDiscount.Text = string.Empty;
                txtSuratExpBeforePricePerCarat.Text = string.Empty;
                txtSuratExpBeforeAmount.Text = string.Empty;

                txtSuratExpAfterDiscount.Text = string.Empty;
                txtSuratExpAfterPricePerCarat.Text = string.Empty;
                txtSuratExpAfterAmount.Text = string.Empty;

                txtRemark.Text = string.Empty;
                txtRChkRemark.Text = string.Empty;

                //txtRapnetDiscount.Text = string.Empty;
                //txtRapnetPricePerCarat.Text = string.Empty;
                //txtRapnetAmount.Text = string.Empty;
                //txtRapnetLink.Text = string.Empty;

                MainGrid.DataSource = null;

                lblLot.Text = "0.00";
                lblBalance.Text = "0.00";

                txtSuratExpRapaport.Text = string.Empty;
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
                CmbRChkShape.SelectedIndex = 0;
                CmbRChkColor.SelectedIndex = 0;
                CmbRChkClarity.SelectedIndex = 0;
                CmbRChkCut.SelectedIndex = 0;
                CmbRChkPol.SelectedIndex = 0;
                CmbRChkSym.SelectedIndex = 0;
                CmbRChkFL.SelectedIndex = 0;
                CmbRChkNatts.SelectedIndex = 0;
                CmbRChkLBLC.SelectedIndex = 0;
                CmbRChkColorShade.SelectedIndex = 0;
                CmbRChkBInC.SelectedIndex = 0;
                CmbRChkWInC.SelectedIndex = 0;
                CmbRChkOInC.SelectedIndex = 0;
                CmbRChkPav.SelectedIndex = 0;
                CmbRChkMilky.SelectedIndex = 0;
                CmbRChkLuster.SelectedIndex = 0;
                CmbRChkEyeClean.SelectedIndex = 0;
                CmbRChkHA.SelectedIndex = 0;
                CmbRChkLab.SelectedIndex = 0;

                CmbRChkLabProcess.SelectedIndex = 0;
                CmbRChkLabSelection.SelectedIndex = 0;

                txtRChkRemark.Text = string.Empty;
                txtRChkGiaNonGia.Text = string.Empty;

                txtRChkRapaport.Text = string.Empty;
                txtRChkDiscount.Text = string.Empty;
                txtRChkRate.Text = string.Empty;
                txtRChkAmount.Text = string.Empty;
                ChkRChkNoBGM.Checked = false;

                txtRChkDiaMin.Text = string.Empty;
                txtRChkDiaMax.Text = string.Empty;
                txtRChkHeight.Text = string.Empty;

                txtRChkTablePC.Text = string.Empty;
                txtRChkRatio.Text = string.Empty;
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

            //txtRChkShape.AccessibleDescription = txtShape.AccessibleDescription;
            //txtRChkShape.Text = txtShape.Text;
            //txtRChkShape.Tag = txtShape.Tag;

            //txtRChkColor.AccessibleDescription = txtColor.AccessibleDescription;
            //txtRChkColor.Text = txtColor.Text;
            //txtRChkColor.Tag = txtColor.Tag;

            //txtRChkClarity.AccessibleDescription = txtClarity.AccessibleDescription;
            //txtRChkClarity.Text = txtClarity.Text;
            //txtRChkClarity.Tag = txtClarity.Tag;



            //txtRChkCut.AccessibleDescription = txtCut.AccessibleDescription;
            //txtRChkCut.Text = txtCut.Text;
            //txtRChkCut.Tag = txtCut.Tag;

            //txtRChkPol.AccessibleDescription = txtPol.AccessibleDescription;
            //txtRChkPol.Text = txtPol.Text;
            //txtRChkPol.Tag = txtPol.Tag;

            //txtRChkSym.AccessibleDescription = txtSym.AccessibleDescription;
            //txtRChkSym.Text = txtSym.Text;
            //txtRChkSym.Tag = txtSym.Tag;

            //txtRChkFL.AccessibleDescription = txtFL.AccessibleDescription;
            //txtRChkFL.Text = txtFL.Text;
            //txtRChkFL.Tag = txtFL.Tag;

            //txtRChkDiaMin.Text = txtDiaMin.Text;
            //txtRChkDiaMax.Text = txtDiaMax.Text;
            //txtRChkHeight.Text = txtHeight.Text;


            //txtRChkColorShade.AccessibleDescription = txtColorShade.AccessibleDescription;
            //txtRChkColorShade.Text = txtColorShade.Text;
            //txtRChkColorShade.Tag = txtColorShade.Tag;

            //txtRChkMilky.AccessibleDescription = txtMilky.AccessibleDescription;
            //txtRChkMilky.Text = txtMilky.Text;
            //txtRChkMilky.Tag = txtMilky.Tag;

            //txtRChkBInC.AccessibleDescription = txtBInC.AccessibleDescription;
            //txtRChkBInC.Text = txtBInC.Text;
            //txtRChkBInC.Tag = txtBInC.Tag;

            //txtRChkOInC.AccessibleDescription = txtOInC.AccessibleDescription;
            //txtRChkOInC.Text = txtOInC.Text;
            //txtRChkOInC.Tag = txtOInC.Tag;

            //txtRChkWInC.AccessibleDescription = txtWInC.AccessibleDescription;
            //txtRChkWInC.Text = txtWInC.Text;
            //txtRChkWInC.Tag = txtWInC.Tag;

            //txtRChkPav.AccessibleDescription = txtPav.AccessibleDescription;
            //txtRChkPav.Text = txtPav.Text;
            //txtRChkPav.Tag = txtPav.Tag;

            //txtRChkLuster.AccessibleDescription = txtLuster.AccessibleDescription;
            //txtRChkLuster.Text = txtLuster.Text;
            //txtRChkLuster.Tag = txtLuster.Tag;

            //txtRChkEyeclean.AccessibleDescription = txtEyeclean.AccessibleDescription;
            //txtRChkEyeclean.Text = txtEyeclean.Text;
            //txtRChkEyeclean.Tag = txtEyeclean.Tag;

            //txtRChkHA.AccessibleDescription = txtHA.AccessibleDescription;
            //txtRChkHA.Text = txtHA.Text;
            //txtRChkHA.Tag = txtHA.Tag;

            //txtRChkRapaport.Text = txtRapaport.Text;
            //txtRChkDiscount.Text = txtDiscount.Text;
            //txtRChkRate.Text = txtRate.Text;
            //txtRChkAmount.Text = txtAmount.Text;

            txtRChkCarat.Text = txtCarat.Text;
            txtRChkDiaMin.Text = txtDiaMin.Text;
            txtRChkDiaMax.Text = txtDiaMax.Text;
            txtRChkHeight.Text = txtHeight.Text;
            txtRChkTablePC.Text = txtTablePC.Text;
            txtRChkRatio.Text = txtRatio.Text;
            txtRChkRemark.Text = txtRemark.Text;
            CmbRChkLabProcess.Text = CmbLabProcess.Text;
            CmbRChkLabSelection.Text = CmbLabSelection.Text;
            ChkRChkNoBGM.Checked = ChkNOBGM.Checked;

            Fetch_SetComboBoxRepairing(CmbRChkShape, DataSHAPERep, Val.ToInt(CmbShape.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkColor, DataCOLORRep, Val.ToInt(CmbColor.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkClarity, DataCLARITYRep, Val.ToInt(CmbClarity.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkCut, DataCUTRep, Val.ToInt(CmbCut.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkPol, DataPOLRep, Val.ToInt(CmbPol.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkSym, DataSYMRep, Val.ToInt(CmbSym.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkFL, DataFLRep, Val.ToInt(CmbFL.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkNatts, DataNATTSRep, Val.ToInt(CmbNatts.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkLBLC, DataLBLCRep, Val.ToInt(CmbLBLC.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkLab, DataLABRep, Val.ToInt(CmbLab.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkColorShade, DataCSRep, Val.ToInt(CmbColorShade.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkBInC, DataBINCRep, Val.ToInt(CmbBInC.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkWInC, DataWINCRep, Val.ToInt(CmbWInC.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkOInC, DataOINCRep, Val.ToInt(CmbOInC.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkPav, DataPAVRep, Val.ToInt(CmbPav.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkMilky, DataMILKYRep, Val.ToInt(CmbMilky.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkLuster, DataLUSTERRep, Val.ToInt(CmbLuster.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkEyeClean, DataEYECLEANRep, Val.ToInt(CmbEyeClean.Tag));
            Fetch_SetComboBoxRepairing(CmbRChkHA, DataHARep, Val.ToInt(CmbHA.Tag));

            txtRChkRapaport.Text = txtRapaport.Text;
            txtSuratExpRapaport.Text = txtRapaport.Text;
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

                ConsiderBGMNonBGM(Val.ToInt(CmbMilky.Tag), Val.ToInt(CmbColorShade.Tag), "ORIGINAL");

                clsFindRap = new Trn_RapSaveProperty();

                clsFindRap.SHAPE_ID = Val.ToInt32(CmbShape.Tag);
                clsFindRap.SHAPECODE = Val.ToString(CmbShape.AccessibleName);

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

                if (Val.ToInt(CmbPrdType.Tag) == 9)
                {
                    txtSuratExpRapaport.Text = Convert.ToString((Val.Val(txtSuratExpRapaport.Text) == 0 || Val.ToString(lblMode.Text) == "Add Mode") ? Val.Val(txtRapaport.Text) : Val.Val(txtSuratExpRapaport.Text));
                }
                else
                {
                    txtSuratExpRapaport.Text = txtRapaport.Text;
                }

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

                ConsiderBGMNonBGM(Val.ToInt(CmbRChkMilky.Tag), Val.ToInt(CmbRChkColorShade.Tag), "RECHECKREP");

                clsFindRapRChk = new Trn_RapSaveProperty();

                clsFindRapRChk.SHAPECODE = Val.ToString(CmbRChkShape.AccessibleName);

                clsFindRapRChk.COLOR_ID = Val.ToInt32(CmbRChkColor.Tag);
                clsFindRapRChk.COLORCODE = Val.ToString(CmbRChkColor.AccessibleName);

                clsFindRapRChk.CLARITY_ID = Val.ToInt32(CmbRChkClarity.Tag);
                clsFindRapRChk.CLARITYCODE = Val.ToString(CmbRChkClarity.AccessibleName);

                clsFindRapRChk.CARAT = Val.Val(txtRChkCarat.Text);
                clsFindRapRChk.CUTCODE = Val.ToString(CmbRChkCut.AccessibleName);
                clsFindRapRChk.POLCODE = Val.ToString(CmbRChkPol.AccessibleName);
                clsFindRapRChk.SYMCODE = Val.ToString(CmbRChkSym.AccessibleName);

                clsFindRapRChk.GCARAT = 0;
                clsFindRapRChk.GCUTCODE = "";
                clsFindRapRChk.GPOLCODE = "";
                clsFindRapRChk.GSYMCODE = "";

                clsFindRapRChk.FLCODE = Val.ToString(CmbRChkFL.AccessibleName);
                clsFindRapRChk.MILKYCODE = Val.ToString(CmbRChkMilky.AccessibleName);
                clsFindRapRChk.NATTSCODE = Val.ToString(CmbRChkNatts.AccessibleName);
                clsFindRapRChk.LBLCCODE = Val.ToString(CmbRChkLBLC.AccessibleName);
                clsFindRapRChk.RAPDATE = Val.SqlDate(CmbRapDate.SelectedItem.ToString());

                //clsFindRap.COLORSHADECODE = "";
                clsFindRapRChk.COLORSHADECODE = Val.ToString(CmbRChkColorShade.AccessibleName);

                //clsFindRap.BLACKINCCODE = "";
                //clsFindRap.OPENINCCODE = "";
                //clsFindRap.WHITEINCCODE = "";
                //clsFindRap.PAVCODE = "";
                //clsFindRap.EYECLEANCODE = "";
                //clsFindRap.LUSTERCODE = "";
                //clsFindRap.NATURALCODE = "";
                //clsFindRap.GRAINCODE = "";

                clsFindRapRChk.BLACKINCCODE = Val.ToString(CmbRChkBInC.AccessibleName).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbRChkBInC.AccessibleName);
                clsFindRapRChk.OPENINCCODE = Val.ToString(CmbRChkOInC.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbRChkOInC.AccessibleName);
                clsFindRapRChk.WHITEINCCODE = Val.ToString(CmbRChkWInC.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbRChkWInC.AccessibleName);
                clsFindRapRChk.PAVCODE = Val.ToString(CmbRChkPav.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbRChkPav.AccessibleName);
                clsFindRapRChk.EYECLEANCODE = Val.ToString(CmbRChkEyeClean.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbRChkEyeClean.AccessibleName);
                clsFindRapRChk.LUSTERCODE = Val.ToString(CmbRChkLuster.Text).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbRChkLuster.AccessibleName);
                clsFindRapRChk.NATURALCODE = Val.ToString(CmbNatural.AccessibleName);
                clsFindRapRChk.GRAINCODE = Val.ToString(CmbGrain.AccessibleName);

                //if (Val.ToString(CmbRChkLabSelection.SelectedItem) == "IGI")
                //{
                //    clsFindRapRChk.LABCODE = "IGI";
                //}
                //else
                //{
                //    clsFindRapRChk.LABCODE = "";
                //}
                clsFindRapRChk.LABCODE = Val.ToString(CmbRChkLab.AccessibleName).Trim().Equals(string.Empty) ? "" : Val.ToString(CmbRChkLab.AccessibleName);

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

                txtRChkGiaNonGia.Text = Val.ToString(clsFindRap.GIANONGIA);
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
                if (txtEmployee.Text.Length == 0)
                {
                    Global.MessageError("Employee Is Required");
                    txtEmployee.Focus();
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
                txtPktSerialNo.Text = Val.ToString(DRPkt["PKTSERIALNO"]);
                txtBarcode.Text = Val.ToString(DRPkt["BARCODE"]);

                DRPkt = null;

                Int64 IntEmployeeID = Val.ToInt64(txtEmployee.Tag);
                //if (Val.ToInt(CmbPrdType.Tag) == 8 || Val.ToInt(CmbPrdType.Tag) == 9 || Val.ToInt(CmbPrdType.Tag) == 11)   //Comment : Pinali : 27-09-2019
                //{
                //    IntEmployeeID = 0;
                //}



                DTab = ObjRap.GetSinglePrdGrdPricingData(Val.ToInt32(CmbPrdType.Tag), Val.ToString(txtTag.Tag), IntEmployeeID, 0);
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

                    mStrEntryDate = string.Empty;

                    ////Check Packet Current LabResult Status : #P : 29-05-2020
                    //Trn_RapSaveProperty PropertyStatus = new Trn_RapSaveProperty();
                    //PropertyStatus.PACKET_ID = Guid.Parse(txtTag.Tag.ToString());
                    //PropertyStatus.PRDTYPE_ID = Val.ToInt(CmbPrdType.Tag);
                    //PropertyStatus = ObjRap.CheckLabPricingValdiation(PropertyStatus);
                    //if (PropertyStatus.ReturnMessageType == "FAIL")
                    //{
                    //    BtnSave.Enabled = false;
                    //    this.Cursor = Cursors.Default;
                    //    Global.MessageError(PropertyStatus.ReturnMessageDesc);
                    //    return;
                    //}
                    //PropertyStatus = null;
                    ////End : Check Packet Current LabResult Status : #P : 29-05-2020


                    if (Val.ToString(CmbPrdType.Tag) == "8")
                        lblGrading.Text = "POLISH-CHECKER Data";
                    else if (Val.ToString(CmbPrdType.Tag) == "9")
                        lblGrading.Text = "GRADING Data";
                    else if (Val.ToString(CmbPrdType.Tag) == "11")
                        lblGrading.Text = "MUMBAI-GRADING Data";

                    if (Val.ToInt(CmbPrdType.Tag) == 8) //Grading
                    {
                        DTab = ObjRap.GetSinglePrdGrdPricingData(7, Val.ToString(txtTag.Tag), 0, 0); //PolishChecker
                    }
                    else if (Val.ToInt(CmbPrdType.Tag) == 9) //MumbaiGrading
                    {
                        //DTab = ObjRap.GetSinglePrdGrdPricingData(8, Val.ToString(txtTag.Tag), 0, 0); //Grading
                        DTabGrdDet = ObjRap.GetSinglePrdGrdPricingData(8, Val.ToString(txtTag.Tag), 0, 0);
                        if (DTabGrdDet.Rows.Count > 0)
                        {
                            DataRow[] DRConfGrader = DTabGrdDet.Select("ISConfirmGrader = True", "");
                            //if (DTabGrdDet.Select("ISConfirmGrader = True", "").CopyToDataTable().Rows.Count > 0)
                            if (DRConfGrader.Length > 0)
                            {
                                DTab = DTabGrdDet.Select("ISConfirmGrader = True", "").CopyToDataTable();

                                txtGradingEmployee.Text = Val.ToString(DTab.Rows[0]["EMPLOYEECODE"]);
                                txtGradingEmployee.Tag = Val.ToString(DTab.Rows[0]["EMPLOYEE_ID"]);
                            }
                            else
                            {
                                DTab.Rows.Clear();
                            }


                            txtGradingEmployee.Text = Val.ToString(DTabGrdDet.Rows[0]["EMPLOYEECODE"]);
                            txtGradingEmployee.Tag = Val.ToString(DTabGrdDet.Rows[0]["EMPLOYEE_ID"]);

                            MainGrid.DataSource = DTabGrdDet;
                            GrdDet.RefreshData();
                            GrdDet.BestFitColumns();
                        }
                        else
                        {
                            DTab.Rows.Clear();
                        }
                    }
                    else if (Val.ToInt(CmbPrdType.Tag) == 11) //LabGrading
                    {
                        DTab = ObjRap.GetSinglePrdGrdPricingData(9, Val.ToString(txtTag.Tag), 0, 0); //Grading
                    }
                    else
                        MainGrid.DataSource = null;

                }
                else
                {
                    lblMode.Text = "Edit Mode";
                    lblGrading.Text = Val.ToString(CmbPrdType.SelectedItem) + " : Data";
                    mStrEntryDate = (Val.ToInt(CmbPrdType.Tag) == 8 || Val.ToInt(CmbPrdType.Tag) == 9) ? Val.ToString(DTab.Rows[0]["ENTRYDATE"]) : "";
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

                txtDiaMin.Text = string.Empty;
                txtDiaMax.Text = string.Empty;
                txtHeight.Text = string.Empty;

                txtTablePC.Text = string.Empty;
                txtRatio.Text = string.Empty;
                txtTotalDepth.Text = string.Empty;
                txtRemark.Text = string.Empty;

                CmbShape.SelectedIndex = 0;
                CmbColor.SelectedIndex = 0;
                CmbClarity.SelectedIndex = 0;
                CmbCut.SelectedIndex = 0;
                CmbPol.SelectedIndex = 0;
                CmbSym.SelectedIndex = 0;
                CmbFL.SelectedIndex = 0;
                CmbNatts.SelectedIndex = 0;
                CmbLBLC.SelectedIndex = 0;
                CmbColorShade.SelectedIndex = 0;
                CmbBInC.SelectedIndex = 0;
                CmbWInC.SelectedIndex = 0;
                CmbOInC.SelectedIndex = 0;
                CmbPav.SelectedIndex = 0;
                CmbMilky.SelectedIndex = 0;
                CmbLuster.SelectedIndex = 0;
                CmbEyeClean.SelectedIndex = 0;
                CmbHA.SelectedIndex = 0;
                CmbLab.SelectedIndex = 0;

                CmbLabProcess.SelectedIndex = 0;
                CmbLabSelection.SelectedIndex = 0;

                mISTFlag = false;

                //txtShape.Tag = string.Empty;
                //txtShape.AccessibleDescription = string.Empty;
                //txtShape.Text = string.Empty;

                //txtColor.Tag = string.Empty;
                //txtColor.AccessibleDescription = string.Empty;
                //txtColor.Text = string.Empty;

                //txtClarity.Tag = string.Empty;
                //txtClarity.AccessibleDescription = string.Empty;
                //txtClarity.Text = string.Empty;

                //txtCut.Tag = string.Empty;
                //txtCut.AccessibleDescription = string.Empty;
                //txtCut.Text = string.Empty;

                //txtPol.Tag = string.Empty;
                //txtPol.AccessibleDescription = string.Empty;
                //txtPol.Text = string.Empty;

                //txtSym.Tag = string.Empty;
                //txtSym.AccessibleDescription = string.Empty;
                //txtSym.Text = string.Empty;

                //txtFL.Tag = string.Empty;
                //txtFL.AccessibleDescription = string.Empty;
                //txtFL.Text = string.Empty;

                //txtLBLC.Tag = string.Empty;
                //txtLBLC.AccessibleDescription = string.Empty;
                //txtLBLC.Text = string.Empty;

                //txtNatts.Tag = string.Empty;
                //txtNatts.AccessibleDescription = string.Empty;
                //txtNatts.Text = string.Empty;

                //txtMilky.Tag = string.Empty;
                //txtMilky.AccessibleDescription = string.Empty;
                //txtMilky.Text = string.Empty;

                ////#P : 27-05-2020
                //txtBInC.Tag = string.Empty;
                //txtBInC.AccessibleDescription = string.Empty;
                //txtBInC.Text = string.Empty;

                //txtOInC.Tag = string.Empty;
                //txtOInC.AccessibleDescription = string.Empty;
                //txtOInC.Text = string.Empty;

                //txtWInC.Tag = string.Empty;
                //txtWInC.AccessibleDescription = string.Empty;
                //txtWInC.Text = string.Empty;

                //txtPav.Tag = string.Empty;
                //txtPav.AccessibleDescription = string.Empty;
                //txtPav.Text = string.Empty;

                //txtHA.Tag = string.Empty;
                //txtHA.AccessibleDescription = string.Empty;
                //txtHA.Text = string.Empty;

                //txtLuster.Tag = string.Empty;
                //txtLuster.AccessibleDescription = string.Empty;
                //txtLuster.Text = string.Empty;

                //txtEyeclean.Tag = string.Empty;
                //txtEyeclean.AccessibleDescription = string.Empty;
                //txtEyeclean.Text = string.Empty;

                CmbTension.SelectedIndex = 0;
                CmbNatural.SelectedIndex = 0;
                CmbGrain.SelectedIndex = 0;

                txtSuratExpLabCharge.Text = string.Empty;
                txtSuratExpBeforeDiscount.Text = string.Empty;
                txtSuratExpBeforePricePerCarat.Text = string.Empty;
                txtSuratExpBeforeAmount.Text = string.Empty;
                txtSuratExpAfterDiscount.Text = string.Empty;
                txtSuratExpAfterPricePerCarat.Text = string.Empty;
                txtSuratExpAfterAmount.Text = string.Empty;

                //txtColorShade.Tag = string.Empty;
                //txtColorShade.AccessibleDescription = string.Empty;
                //txtColorShade.Text = string.Empty;

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

                CmbGrdResultStatus.SelectedIndex = 0;
                CmbCurrGrdResultStatus.SelectedIndex = 0;

                ChkNOBlack.Checked = false;
                ChkNOBGM.Checked = false;

                //End : Pinali : 04-11-2019

                ClearReCheckRepControl();

                DTabRChkData.Rows.Clear();

                //#P : 07-09-2020
                if (SelectedEmployeeRightsProperty.ISCONFIRMGRADER)
                {
                    CmbGrdResultStatus.Enabled = true;
                    CmbGrdResultStatus.SelectedIndex = 0;
                }
                else
                {
                    CmbGrdResultStatus.SelectedItem = "CONFIRM";
                    CmbGrdResultStatus.Enabled = false;
                }
                //End : #P : 07-09-2020

               
                if (lblMode.Text == "Edit Mode" && EmployeeRightsProperty.RAPUPDATEPREDICTION == false)
                {
                    BtnSave.Enabled = false;
                }


                if (DTab.Rows.Count != 0)
                {
                    if ((Val.ToString(DTab.Rows[0]["PKTGRDRESULTSTATUS"]) == "REPAIRING" || Val.ToString(DTab.Rows[0]["PKTGRDRESULTSTATUS"]) == "REPAIRINGCONFIRM" || Val.ToString(DTab.Rows[0]["PKTGRDRESULTSTATUS"]) == "REPAIRINGCANCEL"))
                    {
                        DTabRChkData = ObjRap.GetSinglePrdGrdPricingData(19, Val.ToString(txtTag.Tag), 0, 0);
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

                    txtTablePC.Text = Val.ToString(DRow["HELIUMTABLEPC"]);

                    txtTotalDepth.Text = Val.ToString(DRow["HELIUMTOTALDEPTH"]);

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

                    mISTFlag = Val.ToBoolean(DRow["TFLAG"]);

                    //txtShape.Tag = Val.ToString(DRow["SHAPE_ID"]);
                    //txtShape.AccessibleDescription = Val.ToString(DRow["SHAPECODE"]);
                    //txtShape.Text = Val.ToString(DRow["SHAPENAME"]);

                    //txtColor.Tag = Val.ToString(DRow["COLOR_ID"]);
                    //txtColor.AccessibleDescription = Val.ToString(DRow["COLORCODE"]);
                    //txtColor.Text = Val.ToString(DRow["COLORNAME"]);

                    //txtClarity.Tag = Val.ToString(DRow["CLARITY_ID"]);
                    //txtClarity.AccessibleDescription = Val.ToString(DRow["CLARITYCODE"]);
                    //txtClarity.Text = Val.ToString(DRow["CLARITYNAME"]);

                    //txtCut.Tag = Val.ToString(DRow["CUT_ID"]);
                    //txtCut.AccessibleDescription = Val.ToString(DRow["CUTCODE"]);
                    //txtCut.Text = Val.ToString(DRow["CUTCODE"]);

                    //txtPol.Tag = Val.ToString(DRow["POL_ID"]);
                    //txtPol.AccessibleDescription = Val.ToString(DRow["POLCODE"]);
                    //txtPol.Text = Val.ToString(DRow["POLCODE"]);

                    //txtSym.Tag = Val.ToString(DRow["SYM_ID"]);
                    //txtSym.AccessibleDescription = Val.ToString(DRow["SYMCODE"]);
                    //txtSym.Text = Val.ToString(DRow["SYMCODE"]);

                    //txtFL.Tag = Val.ToString(DRow["FL_ID"]);
                    //txtFL.AccessibleDescription = Val.ToString(DRow["FLCODE"]);
                    //txtFL.Text = Val.ToString(DRow["FLNAME"]);

                    //txtLBLC.Tag = Val.ToString(DRow["LBLC_ID"]);
                    //txtLBLC.AccessibleDescription = Val.ToString(DRow["LBLCCODE"]);
                    //txtLBLC.Text = Val.ToString(DRow["LBLCNAME"]);

                    //txtNatts.Tag = Val.ToString(DRow["NATTS_ID"]);
                    //txtNatts.AccessibleDescription = Val.ToString(DRow["NATTSCODE"]);
                    //txtNatts.Text = Val.ToString(DRow["NATTSNAME"]);

                    //txtMilky.Tag = Val.ToString(DRow["MILKY_ID"]);
                    //txtMilky.AccessibleDescription = Val.ToString(DRow["MILKYCODE"]);
                    //txtMilky.Text = Val.ToString(DRow["MILKYNAME"]);

                    //txtColorShade.Tag = Val.ToString(DRow["COLORSHADE_ID"]);
                    //txtColorShade.AccessibleDescription = Val.ToString(DRow["COLORSHADECODE"]);
                    //txtColorShade.Text = Val.ToString(DRow["COLORSHADENAME"]);

                    ////CmbTension.Tag = Val.ToString(DRow["TENSION_ID"]);
                    ////CmbTension.Tag = Val.ToString(DRow["TENSIONCODE"]);
                    ////CmbTension.Text = Val.ToString(DRow["TENSIONNAME"]);

                    ////#P : 27-05-2020
                    //txtBInC.Tag = Val.ToString(DRow["BLACKINC_ID"]);
                    //txtBInC.AccessibleDescription = Val.ToString(DRow["BLACKINCCODE"]);
                    //txtBInC.Text = Val.ToString(DRow["BLACKINCNAME"]);

                    //txtOInC.Tag = Val.ToString(DRow["OPENINC_ID"]);
                    //txtOInC.AccessibleDescription = Val.ToString(DRow["OPENINCCODE"]);
                    //txtOInC.Text = Val.ToString(DRow["OPENINCNAME"]);

                    //txtWInC.Tag = Val.ToString(DRow["WHITEINC_ID"]);
                    //txtWInC.AccessibleDescription = Val.ToString(DRow["WHITEINCCODE"]);
                    //txtWInC.Text = Val.ToString(DRow["WHITEINCNAME"]);

                    //txtPav.Tag = Val.ToString(DRow["PAV_ID"]);
                    //txtPav.AccessibleDescription = Val.ToString(DRow["PAVCODE"]);
                    //txtPav.Text = Val.ToString(DRow["PAVNAME"]);

                    //txtLuster.Tag = Val.ToString(DRow["LUSTER_ID"]);
                    //txtLuster.AccessibleDescription = Val.ToString(DRow["LUSTERCODE"]);
                    //txtLuster.Text = Val.ToString(DRow["LUSTERNAME"]);

                    //txtEyeclean.Tag = Val.ToString(DRow["EYECLEAN_ID"]);
                    //txtEyeclean.AccessibleDescription = Val.ToString(DRow["EYECLEANCODE"]);
                    //txtEyeclean.Text = Val.ToString(DRow["EYECLEANNAME"]);

                    //txtHA.Tag = Val.ToString(DRow["HA_ID"]);
                    //txtHA.AccessibleDescription = Val.ToString(DRow["HACODE"]);
                    //txtHA.Text = Val.ToString(DRow["HANAME"]);

                    Fetch_SetComboBox(CmbTension, DataTENSION, Val.ToInt(DRow["TENSION_ID"]));
                    Fetch_SetComboBox(CmbNatural, DataNATURAL, Val.ToInt(DRow["NATURAL_ID"]));
                    Fetch_SetComboBox(CmbGrain, DataGRAIN, Val.ToInt(DRow["GRAIN_ID"]));

                    txtSuratExpLabCharge.Text = Val.ToString(DRow["SURATEXPLABCHARGE"]);
                    txtSuratExpBeforeDiscount.Text = Val.ToString(DRow["SURATEXPBEFOREDISCOUNT"]);
                    txtSuratExpBeforePricePerCarat.Text = Val.ToString(DRow["SURATEXPBEFOREPRICEPERCARAT"]);
                    txtSuratExpBeforeAmount.Text = Val.ToString(DRow["SURATEXPBEFOREAMOUNT"]);

                    txtSuratExpAfterDiscount.Text = Val.ToString(DRow["SURATEXPAFTERDISCOUNT"]);
                    txtSuratExpAfterPricePerCarat.Text = Val.ToString(DRow["SURATEXPAFTERPRICEPERCARAT"]);
                    txtSuratExpAfterAmount.Text = Val.ToString(DRow["SURATEXPAFTERAMOUNT"]);

                    CmbCurrGrdResultStatus.SelectedItem = Val.ToString(DRow["PKTGRDRESULTSTATUS"]);
                    CmbGrdResultStatus.SelectedItem = Val.ToString(DRow["GRDRESULTSTATUS"]);

                    if (CmbGrdResultStatus.Text == "NONE")
                        CmbGrdResultStatus_SelectedIndexChanged(null, null);


                    if ((Val.ToInt32(CmbPrdType.Tag) == 9 || Val.ToInt32(CmbPrdType.Tag) == 11) && Val.ToString(CmbCurrGrdResultStatus.Text) == "REPAIRINGCANCEL")
                    {
                        CmbGrdResultStatus.SelectedItem = "CONFIRM";
                    }
                    //End : #P : 27-05-2020

                    CmbRapDate.SelectedItem = Val.ToString(DRow["RAPDATE"]);

                    txtCarat.Text = Val.ToString(DRow["CARAT"]);
                    txtDiscount.Text = Val.ToString(DRow["DISCOUNT"]);
                    txtRate.Text = Val.ToString(DRow["PRICEPERCARAT"]);
                    txtAmount.Text = Val.ToString(DRow["AMOUNT"]);

                    txtRapaport.Text = Val.ToString(DRow["RAPAPORT"]);

                    StrSuratExpBeforeRapaport = Val.Val(DRow["SURATEXPBEFORERAPAPORT"]);

                    if (lblMode.Text == "Edit Mode")
                    {
                        txtSuratExpRapaport.Text = Convert.ToString(Val.Val(StrSuratExpBeforeRapaport) == 0 ? Val.Val(DRow["RAPAPORT"]) : Val.Val(StrSuratExpBeforeRapaport));
                    }
                    else
                    {
                        txtSuratExpRapaport.Text = Val.ToString(Val.Val(DRow["RAPAPORT"]));
                    }


                    txtGiaNonGia.Text = Val.ToString(DRow["GIANONGIA"]);

                    ChkNOBGM.Checked = Val.ToBoolean(DRow["ISNOBGM"]);
                    ChkNOBlack.Checked = Val.ToBoolean(DRow["ISNOBLACK"]);

                    //Add : Pinali : 04-11-2019
                    ChkISPcnGrdByLabEntry.Checked = Val.ToBoolean(DRow["ISPCNGRDBYLABENTRY"]);
                    ChkISPcnGrdByLabEntry.Tag = Val.ToString(DRow["PCNGRDBYLAB_ID"]);
                    mStrReportNo = Val.ToString(DRow["REPORTNO"]);
                    //End : Pinali : 04-11-2019


                    if (Val.ToInt(CmbPrdType.Tag) == 8 && lblMode.Text == "Add Mode")
                    {
                        lblLatestHeliumDetail_Click(null, null);
                    }
                    txtPassForDisplayBack_TextChanged(null, null);
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
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

                txtPassForDisplayBack_TextChanged(null, null);

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

                if (Val.ToInt(CmbPrdType.Tag) == 8)
                {
                    panel6.Visible = false;
                    MainGrd.Visible = false;
                    BtnMakablePrint.Visible = false;
                    MainGrid.Visible = false;
                    CmbGrdResultStatus.Enabled = true;
                    PnlUpDown.Visible = false;
                    panel4.Visible = false;
                    panel5.Visible = true;
                    PnlBtn.Visible = true;
                    pnlParameter.Visible = true;
                }

                else if (Val.ToInt(CmbPrdType.Tag) == 9)
                {
                    MainGrd.Visible = false;
                    BtnMakablePrint.Visible = false;
                    panel6.Visible = false;
                    panel6.Dock = DockStyle.Fill;
                    MainGrid.Visible = true;
                    MainGrid.Dock = DockStyle.Fill;
                    CmbGrdResultStatus.Enabled = false;
                    PnlUpDown.Visible = true;
                    PnlUpDown.Dock = DockStyle.Bottom;
                    //panel4.Visible = true;
                }
                
                if (Val.ToInt(CmbPrdType.Tag) == 4)
                {
                    panel6.Visible = true;
                    MainGrd.Visible = true;
                    BtnMakablePrint.Visible = true;
                    MainGrid.Visible = false;
                    CmbShape.Enabled = true;
                    //CmbGrdResultStatus.Enabled = true;
                    PnlUpDown.Visible = false;
                    panel4.Visible = false;
                    panel5.Visible = true;
                    PnlBtn.Visible = true;
                    pnlParameter.Visible = true;
                    //cLabel41.Visible = false;
                    //cLabel47.Visible = false;
                    //cLabel48.Visible = false;
                    //cLabel9.Visible = false;
                    //txtSuratExpBeforeDiscount.Visible = false;
                    //txtSuratExpBeforePricePerCarat.Visible = false;
                    //txtSuratExpBeforeAmount.Visible = false;
                    //txtSuratExpAfterDiscount.Visible = false;
                    //txtSuratExpAfterPricePerCarat.Visible = false;
                    //txtSuratExpAfterAmount.Visible = false;
                    //cLabel36.Visible = false;
                    //CmbGrdResultStatus.Visible = false;
                }
                else
                {
                    panel6.Visible = false;
                    MainGrd.Visible = false;
                    BtnMakablePrint.Visible = false;
                    MainGrid.Visible = false;
                    CmbGrdResultStatus.Enabled = false;
                    CmbShape.Enabled = false;
                    PnlUpDown.Visible = false;
                    panel4.Visible = false;
                    //cLabel41.Visible = true;
                    //cLabel47.Visible = true;
                    //cLabel48.Visible = true;
                    //cLabel9.Visible = true;
                    //txtSuratExpBeforeDiscount.Visible = true;
                    //txtSuratExpBeforePricePerCarat.Visible = true;
                    //txtSuratExpBeforeAmount.Visible = true;
                    //txtSuratExpAfterDiscount.Visible = true;
                    //txtSuratExpAfterPricePerCarat.Visible = true;
                    //txtSuratExpAfterAmount.Visible = true;
                    //cLabel36.Visible = true;
                    //CmbGrdResultStatus.Visible = true;
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
                    MainGrd.Visible = false;
                    BtnMakablePrint.Visible = false;
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
            Calculation();
        }


        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            FindRap();
            FindRap_ForRepairing();
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
                DouMainRapaport = Val.Val(txtSuratExpRapaport.Text);

                double DouSuratEXPBeforePricePerCarat = 0, DouSuratEXPBeforeAmount = 0, DuoSuratExpBeforeDiscount;
                DouSuratEXPBeforePricePerCarat = Val.Val(txtSuratExpBeforeDiscount.Text) == 0 ? 0 : Math.Round((DouMainRapaport - ((DouMainRapaport * Val.Val(txtSuratExpBeforeDiscount.Text)) / 100)), 2);

                DouSuratEXPBeforeAmount = Math.Round((DouSuratEXPBeforePricePerCarat * Val.Val(txtCarat.Text)), 2);

                //double DouRapnetPricePerCarat = 0, DouRapnetAmount = 0;
                //DouRapnetPricePerCarat = Val.Val(txtRapnetDiscount.Text) == 0 ? 0 : Math.Round((DouMainRapaport - ((DouMainRapaport * Val.Val(txtRapnetDiscount.Text)) / 100)), 2);
                //DouRapnetAmount = Math.Round((DouRapnetPricePerCarat * Val.Val(txtCarat.Text))    , 2);

                //txtMKAVPricePerCarat.Text = DouMKAVPricePerCarat.ToString();
                //txtMKAVAmount.Text = Math.Round(DouMKAVAmount, 0).ToString();

                double DouCarat = 0, DouLabCharge = 0, DouSuratExpAfterDisc = 0, DouSuratExpAfterPricePerCarat = 0, DouSuratExpAfterAmount = 0;
                DouCarat = Val.Val(txtCarat.Text);

                if (((Val.ToString(txtPassForDisplayBack.Text) == Val.ToString(txtPassForDisplayBack.Tag) || Val.ToString(lblMode.Text) == "Add Mode")) && Val.ToInt(CmbPrdType.Tag) == 9 || Val.ToInt(CmbPrdType.Tag) != 9)
                {
                    txtSuratExpBeforePricePerCarat.Text = DouSuratEXPBeforePricePerCarat.ToString();
                    txtSuratExpBeforeAmount.Text = Math.Round(DouSuratEXPBeforeAmount, 0).ToString();

                    if (DTabSuratLabExpense.Rows.Count > 0 && Val.Val(txtSuratExpBeforeDiscount.Text) != 0 && (Val.ToString(CmbLabProcess.Text) != "NON GRAPH" && Val.ToString(CmbLabProcess.Text) != "ORDER"))
                    {
                        DataRow[] DrLabExp = DTabSuratLabExpense.Select(DouCarat + " >= FromSize AND " + DouCarat + "<= ToSize");
                        if (DrLabExp.Length != 0 && Val.ToString(CmbLab.Text) == "IGI")
                        {
                            DouLabCharge = Val.Val(DrLabExp[0]["IGIRATE"]);
                        }
                        else if (DrLabExp.Length != 0)
                        {
                            DouLabCharge = Val.Val(DrLabExp[0]["GIARATE"]);
                        }
                    }
                    if (DouLabCharge > 0 && Val.Val(txtSuratExpBeforeDiscount.Text) != 0 && (Val.ToString(CmbLabProcess.Text) != "NON GRAPH" && Val.ToString(CmbLabProcess.Text) != "ORDER"))
                    {
                        DouSuratExpAfterAmount = Val.Val(DouSuratEXPBeforeAmount) == 0 ? 0 : Val.Val(DouSuratEXPBeforeAmount) - DouLabCharge;
                        //DouSuratExpAfterDisc = Math.Abs(Math.Round(((Val.Val(DouSuratExpAfterAmount) / Val.Val(txtRapaport.Text)) * 100) - 100,2));

                        DouSuratExpAfterPricePerCarat = Math.Round(Val.Val(DouSuratExpAfterAmount) / Val.Val(txtCarat.Text), 2);
                        //DouSuratExpAfterDisc = Math.Abs(Math.Round(Val.Val(DouSuratExpAfterPricePerCarat) / Val.Val(txtSuratExpRapaport.Text) * 100 - 100, 2));
                        DouSuratExpAfterDisc = (Math.Round((((Val.Val(txtSuratExpRapaport.Text) - Val.Val(DouSuratExpAfterPricePerCarat)) / Val.Val(txtSuratExpRapaport.Text)) * 100), 2));

                        txtSuratExpAfterPricePerCarat.Text = Val.ToString(DouSuratExpAfterPricePerCarat);
                        txtSuratExpAfterDiscount.Text = Val.ToString(DouSuratExpAfterDisc);
                        txtSuratExpAfterAmount.Text = Val.ToString(DouSuratExpAfterAmount);
                    }
                    else
                    {
                        txtSuratExpAfterDiscount.Text = txtSuratExpBeforeDiscount.Text;
                        txtSuratExpAfterPricePerCarat.Text = txtSuratExpBeforePricePerCarat.Text;
                        txtSuratExpAfterAmount.Text = txtSuratExpBeforeAmount.Text;
                    }
                    txtSuratExpLabCharge.Text = Val.ToString(DouLabCharge);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void CmbGrdResultStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Val.ToString(CmbLabResultStatus.Text) == "REPAIRING" && Val.ToString(CmbCurrLabResultStatus.Text) == "REPAIRINGCONFIRM" && Val.ToString(CmbCurrLabResultStatus.Text) == "REPAIRINGCANCEL")
            if (Val.ToString(CmbGrdResultStatus.Text) == "REPAIRING" || Val.ToString(CmbCurrGrdResultStatus.Text) == "REPAIRINGCONFIRM")
            {
                if (lblMode.Text == "Add Mode" && Val.ToString(CmbCurrGrdResultStatus.Text) != "REPAIRINGCONFIRM" && Val.ToString(CmbCurrGrdResultStatus.Text) != "REPAIRINGCANCEL")
                    CopyPasteRchkRepData();

                lblRecheckRepText.Text = "Repairing Information";
                PnlRecheckRepairing.Visible = true;
            }
            else if ((Val.ToString(CmbCurrGrdResultStatus.Text) == "NONE" || Val.ToString(CmbCurrGrdResultStatus.Text) == "CONFIRM") && Val.ToString(CmbCurrGrdResultStatus.Text) != "REPAIRINGCANCEL")
            {
                lblRecheckRepText.Text = "Recheck/Repairing Information";
                PnlRecheckRepairing.Visible = false;
                ClearReCheckRepControl();
            }
            //if (CmbCurrLabResultStatus.Text == "" || CmbCurrLabResultStatus.Text == "NONE")
            //{
            //    CmbCurrLabResultStatus.Text = CmbLabResultStatus.Text;
            //}
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
                txtRChkTablePC.Text = Val.ToString(DRow["HELIUMTABLEPC"]);
                txtRChkRatio.Text = Val.ToString(DRow["HELIUMRATIO"]);

                txtRChkRemark.Text = Val.ToString(DRow["REMARK"]);
                txtRChkTotalDepth.Text = Val.ToString(DRow["HELIUMTOTALDEPTH"]);

                Fetch_SetComboBoxRepairing(CmbRChkShape, DataSHAPERep, Val.ToInt(DRow["SHAPE_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkColor, DataCOLORRep, Val.ToInt(DRow["COLOR_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkClarity, DataCLARITYRep, Val.ToInt(DRow["CLARITY_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkCut, DataCUTRep, Val.ToInt(DRow["CUT_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkPol, DataPOLRep, Val.ToInt(DRow["POL_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkSym, DataSYMRep, Val.ToInt(DRow["SYM_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkFL, DataFLRep, Val.ToInt(DRow["FL_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkNatts, DataNATTSRep, Val.ToInt(DRow["NATTS_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkLBLC, DataLBLCRep, Val.ToInt(DRow["LBLC_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkLab, DataLABRep, Val.ToInt(DRow["LAB_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkColorShade, DataCSRep, Val.ToInt(DRow["COLORSHADE_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkBInC, DataBINCRep, Val.ToInt(DRow["BLACKINC_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkWInC, DataWINCRep, Val.ToInt(DRow["WHITEINC_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkOInC, DataOINCRep, Val.ToInt(DRow["OPENINC_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkPav, DataPAVRep, Val.ToInt(DRow["PAV_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkMilky, DataMILKYRep, Val.ToInt(DRow["MILKY_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkLuster, DataLUSTERRep, Val.ToInt(DRow["LUSTER_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkEyeClean, DataEYECLEANRep, Val.ToInt(DRow["EYECLEAN_ID"]));
                Fetch_SetComboBoxRepairing(CmbRChkHA, DataHARep, Val.ToInt(DRow["HA_ID"]));

                //txtRChkShape.Tag = Val.ToString(DRow["SHAPE_ID"]);
                //txtRChkShape.AccessibleDescription = Val.ToString(DRow["SHAPECODE"]);
                //txtRChkShape.Text = Val.ToString(DRow["SHAPENAME"]);

                //txtRChkColor.Tag = Val.ToString(DRow["COLOR_ID"]);
                //txtRChkColor.AccessibleDescription = Val.ToString(DRow["COLORCODE"]);
                //txtRChkColor.Text = Val.ToString(DRow["COLORNAME"]);

                //txtRChkClarity.Tag = Val.ToString(DRow["CLARITY_ID"]);
                //txtRChkClarity.AccessibleDescription = Val.ToString(DRow["CLARITYCODE"]);
                //txtRChkClarity.Text = Val.ToString(DRow["CLARITYNAME"]);

                //txtRChkCut.Tag = Val.ToString(DRow["CUT_ID"]);
                //txtRChkCut.AccessibleDescription = Val.ToString(DRow["CUTCODE"]);
                //txtRChkCut.Text = Val.ToString(DRow["CUTCODE"]);

                //txtRChkPol.Tag = Val.ToString(DRow["POL_ID"]);
                //txtRChkPol.AccessibleDescription = Val.ToString(DRow["POLCODE"]);
                //txtRChkPol.Text = Val.ToString(DRow["POLCODE"]);

                //txtRChkSym.Tag = Val.ToString(DRow["SYM_ID"]);
                //txtRChkSym.AccessibleDescription = Val.ToString(DRow["SYMCODE"]);
                //txtRChkSym.Text = Val.ToString(DRow["SYMCODE"]);

                //txtRChkFL.Tag = Val.ToString(DRow["FL_ID"]);
                //txtRChkFL.AccessibleDescription = Val.ToString(DRow["FLCODE"]);
                //txtRChkFL.Text = Val.ToString(DRow["FLNAME"]);

                //txtRChkMilky.Tag = Val.ToString(DRow["MILKY_ID"]);
                //txtRChkMilky.AccessibleDescription = Val.ToString(DRow["MILKYCODE"]);
                //txtRChkMilky.Text = Val.ToString(DRow["MILKYNAME"]);

                //txtRChkColorShade.Tag = Val.ToString(DRow["COLORSHADE_ID"]);
                //txtRChkColorShade.AccessibleDescription = Val.ToString(DRow["COLORSHADECODE"]);
                //txtRChkColorShade.Text = Val.ToString(DRow["COLORSHADENAME"]);


                ////#P : 27-05-2020
                //txtRChkBInC.Tag = Val.ToString(DRow["BLACKINC_ID"]);
                //txtRChkBInC.AccessibleDescription = Val.ToString(DRow["BLACKINCCODE"]);
                //txtRChkBInC.Text = Val.ToString(DRow["BLACKINCNAME"]);

                //txtRChkOInC.Tag = Val.ToString(DRow["OPENINC_ID"]);
                //txtRChkOInC.AccessibleDescription = Val.ToString(DRow["OPENINCCODE"]);
                //txtRChkOInC.Text = Val.ToString(DRow["OPENINCNAME"]);

                //txtRChkWInC.Tag = Val.ToString(DRow["WHITEINC_ID"]);
                //txtRChkWInC.AccessibleDescription = Val.ToString(DRow["WHITEINCCODE"]);
                //txtRChkWInC.Text = Val.ToString(DRow["WHITEINCNAME"]);

                //txtRChkPav.Tag = Val.ToString(DRow["PAV_ID"]);
                //txtRChkPav.AccessibleDescription = Val.ToString(DRow["PAVCODE"]);
                //txtRChkPav.Text = Val.ToString(DRow["PAVNAME"]);

                //txtRChkLuster.Tag = Val.ToString(DRow["LUSTER_ID"]);
                //txtRChkLuster.AccessibleDescription = Val.ToString(DRow["LUSTERCODE"]);
                //txtRChkLuster.Text = Val.ToString(DRow["LUSTERNAME"]);

                //txtRChkEyeclean.Tag = Val.ToString(DRow["EYECLEAN_ID"]);
                //txtRChkEyeclean.AccessibleDescription = Val.ToString(DRow["EYECLEANCODE"]);
                //txtRChkEyeclean.Text = Val.ToString(DRow["EYECLEANNAME"]);

                //txtRChkHA.Tag = Val.ToString(DRow["HA_ID"]);
                //txtRChkHA.AccessibleDescription = Val.ToString(DRow["HACODE"]);
                //txtRChkHA.Text = Val.ToString(DRow["HANAME"]);

                txtRChkCarat.Text = Val.ToString(DRow["CARAT"]);
                txtRChkDiscount.Text = Val.ToString(DRow["DISCOUNT"]);
                txtRChkRate.Text = Val.ToString(DRow["PRICEPERCARAT"]);
                txtRChkAmount.Text = Val.ToString(DRow["AMOUNT"]);
                txtRChkRapaport.Text = Val.ToString(DRow["RAPAPORT"]);
                txtRChkGiaNonGia.Text = Val.ToString(DRow["GIANONGIA"]);

                txtRChkGiaNonGia.Text = Val.ToString(DRow["GIANONGIA"]);
                ChkRChkNoBGM.Checked = Val.ToBoolean(DRow["ISNOBGM"]);
            }

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

        private void CmbRChkBInC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AxonContLib.cComboBox combo = sender as AxonContLib.cComboBox;
                if (combo == null)
                {
                    return;
                }
                DataStructureGradingRepairing selectedDataStructureRep = combo.SelectedItem as DataStructureGradingRepairing;
                if (selectedDataStructureRep == null)
                {
                    Global.MessageError("You didn't select anything at the moment");
                }
                else
                {
                    if (combo.AccessibleDescription == "SHAPE")
                    {
                        CmbRChkShape.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkShape.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkShape.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "COLOR")
                    {
                        CmbRChkColor.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkColor.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkColor.Text = Val.ToString(selectedDataStructureRep.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "CLARITY")
                    {
                        CmbRChkClarity.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkClarity.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkClarity.Text = Val.ToString(selectedDataStructureRep.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "CUT")
                    {
                        CmbRChkCut.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkCut.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkCut.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "POLISH")
                    {
                        CmbRChkPol.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkPol.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkPol.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "SYMMETRY")
                    {
                        CmbRChkSym.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkSym.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkSym.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "FLUORESCENCE")
                    {
                        CmbRChkFL.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkFL.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkFL.Text = Val.ToString(selectedDataStructureRep.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "NATTS")
                    {
                        CmbRChkNatts.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkNatts.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkNatts.Text = Val.ToString(selectedDataStructureRep.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LBLC")
                    {
                        CmbRChkLBLC.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkLBLC.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkLBLC.Text = Val.ToString(selectedDataStructureRep.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LAB")
                    {
                        CmbRChkLab.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkLab.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkLab.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "COLORSHADE")
                    {
                        CmbRChkColorShade.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkColorShade.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkColorShade.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "BLACK")
                    {
                        CmbRChkBInC.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkBInC.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkBInC.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "WHITE")
                    {
                        CmbRChkWInC.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkWInC.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkWInC.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "OPEN")
                    {
                        CmbRChkOInC.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkOInC.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkOInC.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "PAVALION")
                    {
                        CmbRChkPav.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkPav.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkPav.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "MILKY")
                    {
                        CmbRChkMilky.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkMilky.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkMilky.Text = Val.ToString(selectedDataStructureRep.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LUSTER")
                    {
                        CmbRChkLuster.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkLuster.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkLuster.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "EYECLEAN")
                    {
                        CmbRChkEyeClean.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkEyeClean.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkEyeClean.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "HEARTANDARROW")
                    {
                        CmbRChkHA.Tag = Val.ToString(selectedDataStructureRep.PARA_ID);
                        CmbRChkHA.AccessibleName = Val.ToString(selectedDataStructureRep.PARACODE);
                        CmbRChkHA.Text = Val.ToString(selectedDataStructureRep.PARACODE);
                    }
                }
                FindRap_ForRepairing();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
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

                txtRChkDiaMin.Text = Val.ToString(Dr["DIAMIN"]);
                txtRChkDiaMax.Text = Val.ToString(Dr["DIAMAX"]);
                txtRChkHeight.Text = Val.ToString(Dr["HEIGHT"]);
                txtRChkRatio.Text = Val.ToString(Dr["RATIO"]);
                txtRChkTablePC.Text = Val.ToString(Dr["TABLEPC"]);
                txtRChkTotalDepth.Text = Val.ToString(Dr["TOTALDEPTH"]);

                FindRap();
                FindRap_ForRepairing();
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
                    FrmSearch.mSearchField = "EMPLOYEECODE";
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
                    ClearReCheckRepControl();
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

                txtSuratExpLabCharge.Text = Val.ToString(DRow["SURATEXPLABCHARGE"]);
                txtSuratExpBeforeDiscount.Text = Val.ToString(DRow["SURATEXPBEFOREDISCOUNT"]);
                txtSuratExpBeforePricePerCarat.Text = Val.ToString(DRow["SURATEXPBEFOREPRICEPERCARAT"]);
                txtSuratExpBeforeAmount.Text = Val.ToString(DRow["SURATEXPBEFOREAMOUNT"]);

                txtSuratExpAfterDiscount.Text = Val.ToString(DRow["SURATEXPAFTERDISCOUNT"]);
                txtSuratExpAfterPricePerCarat.Text = Val.ToString(DRow["SURATEXPAFTERPRICEPERCARAT"]);
                txtSuratExpAfterAmount.Text = Val.ToString(DRow["SURATEXPAFTERAMOUNT"]);

                CmbCurrGrdResultStatus.SelectedItem = Val.ToString(DRow["PKTGRDRESULTSTATUS"]);
                CmbGrdResultStatus.SelectedItem = Val.ToString(DRow["GRDRESULTSTATUS"]);

                if (CmbGrdResultStatus.Text == "NONE")
                    CmbGrdResultStatus_SelectedIndexChanged(null, null);


                if ((Val.ToInt32(CmbPrdType.Tag) == 9 || Val.ToInt32(CmbPrdType.Tag) == 11) && Val.ToString(CmbCurrGrdResultStatus.Text) == "REPAIRINGCANCEL")
                {
                    CmbGrdResultStatus.SelectedItem = "CONFIRM";
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
                //End : Pinali : 04-11-2019

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
                panel6.Visible = true;
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

        private void txtSuratExpBeforePricePerCarat_Validated(object sender, EventArgs e)
        {
            try
            {

                if ((Val.ToInt(CmbPrdType.Tag) == 9 && (Val.ToString(txtPassForDisplayBack.Text) == Val.ToString(txtPassForDisplayBack.Tag)) || Val.ToString(lblMode.Text) == "Add Mode") || Val.ToInt(CmbPrdType.Tag) != 9)
                {
                    double DouSuratEXPBeforePricePerCarat = 0, DouSuratExpBeforeDisc = 0, DouMainRapaport = 0, DouSuratEXPBeforeAmount = 0;

                    DouSuratEXPBeforePricePerCarat = Val.Val(txtSuratExpBeforePricePerCarat.Text);

                    DouMainRapaport = Val.Val(DouSuratEXPBeforePricePerCarat) == 0 ? 0 : Val.Val(txtSuratExpRapaport.Text);

                    //DouSuratExpBeforeDisc = (Math.Round(Val.Val(DouSuratEXPBeforePricePerCarat) / DouMainRapaport * 100 - 100, 2));
                    DouSuratExpBeforeDisc = (Math.Round((((Val.Val(DouMainRapaport) - Val.Val(DouSuratEXPBeforePricePerCarat)) / DouMainRapaport) * 100), 2));

                    txtSuratExpBeforeDiscount.Text = Val.ToString(DouSuratExpBeforeDisc);

                    DouSuratEXPBeforeAmount = Math.Round((DouSuratEXPBeforePricePerCarat * Val.Val(txtCarat.Text)), 2);

                    double DouCarat = 0, DouLabCharge = 0, DouSuratExpAfterDisc = 0, DouSuratExpAfterPricePerCarat = 0, DouSuratExpAfterAmount = 0;
                    DouCarat = Val.Val(txtCarat.Text);


                    txtSuratExpBeforePricePerCarat.Text = DouSuratEXPBeforePricePerCarat.ToString();
                    txtSuratExpBeforeAmount.Text = Math.Round(DouSuratEXPBeforeAmount, 0).ToString();

                    if (DTabSuratLabExpense.Rows.Count > 0 && Val.Val(txtSuratExpBeforeDiscount.Text) != 0 && (Val.ToString(CmbLabProcess.Text) != "NON GRAPH" && Val.ToString(CmbLabProcess.Text) != "ORDER"))
                    {
                        DataRow[] DrLabExp = DTabSuratLabExpense.Select(DouCarat + " >= FromSize AND " + DouCarat + "<= ToSize");
                        if (DrLabExp.Length != 0 && Val.ToString(CmbLab.Text) == "IGI")
                        {
                            DouLabCharge = Val.Val(DrLabExp[0]["IGIRATE"]);
                        }
                        else if (DrLabExp.Length != 0)
                        {
                            DouLabCharge = Val.Val(DrLabExp[0]["GIARATE"]);
                        }
                    }
                    if (DouLabCharge > 0 && Val.Val(txtSuratExpBeforeDiscount.Text) != 0 && (Val.ToString(CmbLabProcess.Text) != "NON GRAPH" && Val.ToString(CmbLabProcess.Text) != "ORDER"))
                    {
                        DouSuratExpAfterAmount = Val.Val(DouSuratEXPBeforeAmount) == 0 ? 0 : Val.Val(DouSuratEXPBeforeAmount) - DouLabCharge;
                        DouSuratExpAfterPricePerCarat = Math.Round(Val.Val(DouSuratExpAfterAmount) / Val.Val(txtCarat.Text), 2);
                        //DouSuratExpAfterDisc = (Math.Round(Val.Val(DouSuratExpAfterPricePerCarat) / Val.Val(DouMainRapaport) * 100 - 100, 2));
                        DouSuratExpAfterDisc = (Math.Round((((Val.Val(DouMainRapaport) - Val.Val(DouSuratExpAfterPricePerCarat)) / DouMainRapaport) * 100), 2));

                        txtSuratExpAfterPricePerCarat.Text = Val.ToString(DouSuratExpAfterPricePerCarat);
                        txtSuratExpAfterDiscount.Text = Val.ToString(DouSuratExpAfterDisc);
                        txtSuratExpAfterAmount.Text = Val.ToString(DouSuratExpAfterAmount);
                    }
                    else
                    {
                        txtSuratExpAfterDiscount.Text = txtSuratExpBeforeDiscount.Text;
                        txtSuratExpAfterPricePerCarat.Text = txtSuratExpBeforePricePerCarat.Text;
                        txtSuratExpAfterAmount.Text = txtSuratExpBeforeAmount.Text;
                    }
                    txtSuratExpLabCharge.Text = Val.ToString(DouLabCharge);
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
                return;
            }

        }

        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(CmbPrdType.Tag) == 9 && lblMode.Text == "Edit Mode")
                {
                    if ((Val.ToString(txtPassForDisplayBack.Text) == Val.ToString(txtPassForDisplayBack.Tag)))
                    {
                        txtSuratExpBeforeDiscount.ReadOnly = false;
                        txtSuratExpBeforePricePerCarat.ReadOnly = false;

                        ChkLatestRapForSuratExp.Visible = true;
                    }
                    else
                    {
                        txtSuratExpBeforeDiscount.ReadOnly = true;
                        txtSuratExpBeforePricePerCarat.ReadOnly = true;
                        ChkLatestRapForSuratExp.Visible = false;
                    }
                }
                else
                {
                    txtSuratExpBeforeDiscount.ReadOnly = false;
                    txtSuratExpBeforePricePerCarat.ReadOnly = false;
                    ChkLatestRapForSuratExp.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
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
                if (ChkLatestRapForSuratExp.Checked)
                {
                    txtSuratExpRapaport.Text = txtRapaport.Text;
                    
                }
                else
                {
                    txtSuratExpRapaport.Text = Val.ToString(StrSuratExpBeforeRapaport);
                }
                Calculation();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }

        }

        private void BtnMakablePrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdData, true);

                if (DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select at lease One Row For Barcode Print");
                    return;
                }

                if (RbtCitizen.Checked == true)
                {
                    string StrBatchFileName = "";
                    string StrBarcodeTxtFileName = "";

                    StrBarcodeTxtFileName = "\\PrintBarcodeDataMkbl_Citizen.txt";
                    StrBatchFileName = "\\PRINTBARCODE_Citizen.BAT ";

                    string fileLoc = Application.StartupPath + StrBarcodeTxtFileName;
                    if (System.IO.File.Exists(fileLoc) == true)
                    {
                        System.IO.File.Delete(fileLoc);
                    }
                    System.IO.File.Create(fileLoc).Dispose();

                    StreamWriter sw = new StreamWriter(fileLoc);

                    using (sw)
                    {
                        foreach (DataRow DRow in DTab.Rows)
                        {

                            string StrBarcode = Val.ToString(DRow["BARCODE"]);
                            string StrKapanNames = Val.ToString(DRow["KAPANNAME"]);
                            string StrEmployeeCode = Val.ToString(DRow["MKBLEMPLOYEECODE"]);
                            string StrPktNoTag = Val.ToString(DRow["PACKETNO"]) + "" + Val.ToString(DRow["TAG"]);
                            int StrPktSrNo = Val.ToInt(DRow["PKTSERIALNO"]);
                            string StrParameterAmt = DRow["MKBLCOLORNAME"].ToString() + "-" + DRow["MKBLCLARITYNAME"].ToString() + "-" + DRow["MKBLCUTNAME"].ToString() + "-" + DRow["MKBLFLNAME"].ToString() + "-" + DRow["MKBLPRDAMOUNT"].ToString();
                            string StrShpBlnCts = DRow["MKBLSHAPENAME"].ToString() + "-" + DRow["MKBLPRDCARAT"].ToString() + "-" + DRow["MKBLBALANCECARAT"].ToString();
                            string StrDate = DateTime.Now.ToString("dd-MM");
                            Global.BarcodeProntMkblCitizen(sw, StrBarcode, StrKapanNames, StrEmployeeCode, StrPktNoTag, StrPktSrNo.ToString(), StrParameterAmt, StrShpBlnCts);

                        }
                        sw.Close();
                    }

                    sw.Dispose();
                    sw = null;
                    if (File.Exists(Application.StartupPath + StrBatchFileName) && File.Exists(fileLoc))
                    {
                        Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + StrBatchFileName + fileLoc, AppWinStyle.Hide, true, -1);
                    }

                    System.Threading.Thread.Sleep(800);

                }
                else if (RbtTSC.Checked == true)
                {
                    string StrBatchFileName = "";
                    string StrBarcodeTxtFileName = "";

                    StrBarcodeTxtFileName = "\\PrintBarcodeDataMkbl_TSC.txt";
                    StrBatchFileName = "\\PRINTBARCODE_TSC.BAT ";

                    string fileLoc = Application.StartupPath + StrBarcodeTxtFileName;
                    if (System.IO.File.Exists(fileLoc) == true)
                    {
                        System.IO.File.Delete(fileLoc);
                    }
                    System.IO.File.Create(fileLoc).Dispose();

                    StreamWriter sw = new StreamWriter(fileLoc);

                    using (sw)
                    {
                        foreach (DataRow DRow in DTab.Rows)
                        {

                            string StrBarcode = Val.ToString(DRow["BARCODE"]);
                            string StrKapanNames = Val.ToString(DRow["KAPANNAME"]);
                            string StrEmployeeCode = Val.ToString(DRow["MKBLEMPLOYEECODE"]);
                            string StrPktNoTag = Val.ToString(DRow["PACKETNO"]) + "" + Val.ToString(DRow["TAG"]);
                            int StrPktSrNo = Val.ToInt(DRow["PKTSERIALNO"]);
                            string StrParameterAmt = DRow["MKBLCOLORNAME"].ToString() + "-" + DRow["MKBLCLARITYNAME"].ToString() + "-" + DRow["MKBLCUTNAME"].ToString() + "-" + DRow["MKBLFLNAME"].ToString() + "-" + DRow["MKBLPRDAMOUNT"].ToString();
                            string StrShpBlnCts = DRow["MKBLSHAPENAME"].ToString() + "-" + DRow["MKBLPRDCARAT"].ToString() + "-" + DRow["MKBLBALANCECARAT"].ToString();
                            Global.BarcodeProntMkblTSC(sw, StrBarcode, StrKapanNames, StrEmployeeCode, StrPktNoTag, StrPktSrNo.ToString(), StrParameterAmt, StrShpBlnCts);

                        }
                        sw.Close();
                    }

                    sw.Dispose();
                    sw = null;
                    if (File.Exists(Application.StartupPath + StrBatchFileName) && File.Exists(fileLoc))
                    {
                        Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + StrBatchFileName + fileLoc, AppWinStyle.Hide, true, -1);
                    }

                    System.Threading.Thread.Sleep(800);

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

        private void PnlBtn_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RbtTSC_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtCitizen.Checked == true)
            {
                lblBPrintMessage.Text = "Note : Selected Packet Barcode Is Printed In : [" + RbtCitizen.Text + "] Printer";
            }
           
        }

        private void RbtCitizen_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtTSC.Checked == true)
            {
                lblBPrintMessage.Text = "Note : Selected Packet Barcode Is Printed In : [" + RbtTSC.Text + "] Printer";
            }
           
        }

       
    }
    public class DataStructureGradingOld
    {
        public int PARA_ID { get; set; }
        public string PARACODE { get; set; }
        public string PARANAME { get; set; }
    }
    public class DataStructureGradingRepairingOld
    {
        public int PARA_ID { get; set; }
        public string PARACODE { get; set; }
        public string PARANAME { get; set; }
    }
}
