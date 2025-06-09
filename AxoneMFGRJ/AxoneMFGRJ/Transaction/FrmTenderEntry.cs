using BusLib.Configuration;
using BusLib.TableName;
using BusLib.View;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using AxoneMFGRJ.View;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmTenderEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();
        double DouRoughCarat = 0;
        double DouPolishCarat = 0;
        double DouPolishPcsManual = 0;
        double DouPolishPcs = 0;
        double DouImportPrice = 0;
        double DouPolishAmountPerCarat = 0;
        double DouPolishAmount = 0;
        double DouRapAmount = 0;

        int IntRoughPcs = 0;
        Color mSelectedColor = Color.FromArgb(192, 0, 0);
        Color mDeSelectColor = Color.Black;
        Color mSelectedBackColor = Color.FromArgb(255, 224, 192);
        Color mDSelectedBackColor = Color.WhiteSmoke;

        string mStrType = "NOSHOWCLICK";

        public double mStrFinalAmount = 0;
        public double mStrFinalRate = 0;

        public DataTable DTabData = new DataTable("TABLE");
        public DataTable DTabParameter = new DataTable("TABLE");
        public DataTable DTabRapDate = new DataTable("TABLE");
        public DataTable DTabMajuriRate = new DataTable("TABLE");

        bool IsDownImage = true;

        IList<DataStructure> DtabCut = new BindingList<DataStructure>();
        IList<DataStructure> DtabPol = new BindingList<DataStructure>();
        IList<DataStructure> DtabSym = new BindingList<DataStructure>();
        IList<DataStructure> DtabFL = new BindingList<DataStructure>();
        IList<DataStructure> DtabLab = new BindingList<DataStructure>();
        IList<DataStructure> DtabLBLC = new BindingList<DataStructure>();


        #region ShowForm

        public FrmTenderEntry()
        {
            InitializeComponent();
        }

        //public void ShowForm()
        //{
        //    Val.FormGeneralSetting(this);
        //    AttachFormDefaultEvent();
        //    GetData();
        //    this.Show();
        //    DTabParameter = Obj.GetAllParameterTableForTender();

        //    txtCheckBy.Text = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGERNAME);
        //    txtCheckBy.Tag = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID);

        //    SetControl();
        //    txtTenderName.Focus();

        //    BtnBlankNew_Click(null, null);
        //}

        public void ShowForm
            (
            string pStrLotID,
            string pStrSizeID,
            string pStrSizeName,

            string pStrRoughTypeID,
            string pStrRoughTypeName,

            string pStrRoughMinesID,
            string pStrRoughMinesName,

            string pStrCompany,

            string pStrRoughName,
            string pStrRoughCarat,
            string pStrCheckBy,

            string pStrExchangeRate,
            double DouRoughImpExpPer,
            double DouRoughCost,
            DataTable DTabAssortDataFromAssort1
            )
        {
            Val.FormGeneralSettingForPopup(this);
            AttachFormDefaultEvent();

            DTabParameter = Obj.GetAllParameterTableForTender();
            //DTabMajuriRate = Obj.GetMajuriRateTableForTender(); //Cmnt : 16-04-2021

            mStrType = "SHOWCLICK";

            this.Text = pStrCheckBy + " : ASSORTMENT MODULE";

            SetControl();

            txtRoughID.Text = "";
            txtRoughID.Tag = "";

            txtLotID.Text = pStrLotID;
            txtLotID.Tag = pStrLotID;

            txtRoughType.Tag = pStrRoughTypeID;
            txtRoughType.Text = pStrRoughTypeName;

            txtTenderName.Tag = pStrRoughMinesID;
            txtTenderName.Text = pStrRoughMinesName;

            txtSize.Tag = pStrSizeID;
            txtSize.Text = pStrSizeName;

            txtRoughName.Text = pStrRoughName;

            txtCompany.Text = pStrCompany;

            txtMainCarat.Text = pStrRoughCarat;
            txtCheckBy.Text = pStrCheckBy;

            //#P : 18-08-2020
            txtRoughImportPer.Text = Val.ToString(DouRoughImpExpPer);
            txtPurchaseRate.Text = Val.ToString(DouRoughCost);
            //End : #P : 18-08-2020

            lblMode.Text = "Add Mode";

            AddDefaultSetting();

            DataRow DRow = Obj.GetTenderSingleData(pStrLotID, pStrCheckBy);
            if (DRow != null)
            {
                txtRoughID.Text = Val.ToString(DRow["ROUGH_ID"]);
                txtRoughID.Tag = Val.ToString(DRow["ROUGH_ID"]);

                txtImpDollarRate.Text = Val.ToString(DRow["IMPDOLLARRATE"]);
                txtExpDollarRate.Text = Val.ToString(DRow["EXPDOLLARRATE"]);

                txtRoughImportPer.Text = Val.ToString(DRow["ROUGHIMPORTPER"]);
                txtRoughImportAmount.Text = Val.ToString(DRow["ROUGHIMPORTAMOUNT"]);

                txtProfitPer.Text = Val.ToString(DRow["PROFITPER"]);
                txtProfitAmount.Text = Val.ToString(DRow["PROFITAMOUNT"]);

                txtLabourPer.Text = Val.ToString(DRow["LABOURPER"]);
                txtLabourAmount.Text = Val.ToString(DRow["LABOURAMOUNT"]);

                txtMainCarat.Text = Val.ToString(DRow["MAINCARAT"]);
                txtNote.Text = Val.ToString(DRow["NOTE"]);
                DTPEntryDate.Value = DateTime.Parse(Val.ToString(DRow["TENDERDATE"]));
                CmbRapDate.SelectedItem = DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy");
                lblMode.Text = "Edit Mode";

                GetData();
                mStrType = "SHOWCLICK";
                Calculation();

                mStrType = "NOSHOWCLICK";
            }
            else
            {
                GetData();
                Calculation();
            }

            //#D : 03-12-2020
            if (txtCheckBy.Text != "ASSORT1" && DTabData.Rows.Count == 0)
            {
                foreach (DataRow DR in DTabAssortDataFromAssort1.Rows)
                {
                    double DouRoughPer;

                    DataRow DRNew = DTabData.NewRow();
                    DRNew["PACKETNO"] = DTabData.Rows.Count + 1;

                    DRNew["ROUGHCARAT"] = DR["ROUGHCARAT"];
                    DRNew["POLISHPER"] = DR["POLISHPER"];
                    DRNew["ROUGHPCS"] = DR["ROUGHPCS"];
                    DRNew["POLISHCARATMANUAL"] = DR["POLISHCARATMANUAL"];
                    DouRoughPer = Val.Val(txtMainCarat.Text) == 0 ? 0 : Math.Round((Val.Val(DR["ROUGHCARAT"]) / Val.Val(txtMainCarat.Text)) * 100, 2);
                    DRNew["ROUGHPER"] = DouRoughPer;

                    DTabData.Rows.Add(DRNew);
                }
                DTabData.AcceptChanges();
                Calculation();

            }
            //End: #D : 03-12-2020

            BtnBlankNew_Click(null, null);
            GrdDet.Focus();
            GrdDet.FocusedColumn = GrdDet.Columns["ROUGHCARAT"];
            GrdDet.FocusedRowHandle = 0;
            this.ShowDialog();

        }

        private void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(DTabData);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Obj);
        }

        public void SetControl()
        {
            DTabRapDate = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.RAPDATE);
            DTabRapDate.DefaultView.Sort = "RAPDATE DESC";
            DTabRapDate = DTabRapDate.DefaultView.ToTable();
            CmbRapDate.Items.Clear();
            foreach (DataRow DRow in DTabRapDate.Rows)
            {
                CmbRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }
            CmbRapDate.SelectedIndex = 0;

            DesignSystemRadioButtion(PanelShape, "SHAPE", "PARACODE", "SHAPE", false);
            DesignSystemRadioButtion(PanelColor1, "COLOR", "PARANAME", "COLOR1", true);
            DesignSystemRadioButtion(PanelClarity1, "CLARITY", "PARANAME", "CLARITY1", true);
            DesignSystemRadioButtion(PanelColor2, "COLOR", "PARANAME", "COLOR2", true);
            DesignSystemRadioButtion(PanelClarity2, "CLARITY", "PARANAME", "CLARITY2", true);
            //DesignSystemRadioButtion(PanelCut, "CUT", "PARACODE", "CUT", false);
            //DesignSystemRadioButtion(PanelPol, "POLISH", "PARACODE", "POL", false);
            //DesignSystemRadioButtion(PanelSym, "SYMMETRY", "PARACODE", "SYM", false);
            //DesignSystemRadioButtion(PanelFL, "FLUORESCENCE", "PARANAME", "FL", false);
            //DesignSystemRadioButtion(PanelLab, "LAB", "PARANAME", "LAB", true);
            //DesignSystemRadioButtion(PanelLBLC, "LBLC", "SHORTNAME", "LBLC", true);

            DesignComboBox(CmbCut, "CUT", "PARACODE", "CUT");
            DesignComboBox(CmbPol, "POLISH", "PARACODE", "POL");
            DesignComboBox(CmbSym, "SYMMETRY", "PARACODE", "SYM");
            DesignComboBox(CmbFL, "FLUORESCENCE", "PARANAME", "FL");
            DesignComboBox(CmbLab, "LAB", "PARANAME", "LAB");
            DesignComboBox(CmbLBLC, "LBLC", "PARANAME", "LBLC");

            PnlCalculation.Visible = false;
        }


        private void cRadioShapeButton_Click(object sender, EventArgs e)
        {


            AxonContLib.cRadioButton rd = (AxonContLib.cRadioButton)sender;
            if (rd.ToolTips == "SHAPE")
            {
                foreach (AxonContLib.cRadioButton Cont in PanelShape.Controls)
                {
                    Cont.ForeColor = mDeSelectColor;
                    Cont.BackColor = mDSelectedBackColor;
                }

                AxonContLib.cRadioButton rbSelected = PanelShape.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                rbSelected.ForeColor = mSelectedColor;
                rbSelected.BackColor = mSelectedBackColor;

                if (rbSelected.Text == "R" && lblMode.Text == "Add Mode")
                {
                    CmbCut.SelectedIndex = 0;
                    CmbPol.SelectedIndex = 0;
                    CmbSym.SelectedIndex = 0;
                    CmbFL.SelectedIndex = 0;
                    CmbLab.SelectedIndex = 0;
                    CmbLBLC.SelectedIndex = 0;
                    //AxonContLib.cRadioButton rbPol = PanelPol.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault();
                    //rbPol.Checked = true;
                    //rbPol.ForeColor = mSelectedColor;
                    //rbPol.BackColor = mSelectedBackColor;

                    //AxonContLib.cRadioButton rbSym = PanelSym.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault();
                    //rbSym.Checked = true;
                    //rbSym.ForeColor = mSelectedColor;
                    //rbSym.BackColor = mSelectedBackColor;

                }
            }
            else if (rd.ToolTips == "COLOR1")
            {
                foreach (AxonContLib.cRadioButton Cont in PanelColor1.Controls)
                {
                    Cont.ForeColor = mDeSelectColor;
                    Cont.BackColor = mDSelectedBackColor;
                }

                AxonContLib.cRadioButton rbSelected = PanelColor1.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                rbSelected.ForeColor = mSelectedColor;
                rbSelected.BackColor = mSelectedBackColor;
            }
            else if (rd.ToolTips == "CLARITY1")
            {
                foreach (AxonContLib.cRadioButton Cont in PanelClarity1.Controls)
                {
                    Cont.ForeColor = mDeSelectColor;
                    Cont.BackColor = mDSelectedBackColor;
                }

                AxonContLib.cRadioButton rbSelected = PanelClarity1.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                rbSelected.ForeColor = mSelectedColor;
                rbSelected.BackColor = mSelectedBackColor;
            }

            else if (rd.ToolTips == "COLOR2")
            {
                foreach (AxonContLib.cRadioButton Cont in PanelColor2.Controls)
                {
                    Cont.ForeColor = mDeSelectColor;
                    Cont.BackColor = mDSelectedBackColor;
                }

                AxonContLib.cRadioButton rbSelected = PanelColor2.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                rbSelected.ForeColor = mSelectedColor;
                rbSelected.BackColor = mSelectedBackColor;
            }
            else if (rd.ToolTips == "CLARITY2")
            {
                foreach (AxonContLib.cRadioButton Cont in PanelClarity2.Controls)
                {
                    Cont.ForeColor = mDeSelectColor;
                    Cont.BackColor = mDSelectedBackColor;
                }

                AxonContLib.cRadioButton rbSelected = PanelClarity2.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                rbSelected.ForeColor = mSelectedColor;
                rbSelected.BackColor = mSelectedBackColor;
            }

            //else if (rd.ToolTips == "CUT")
            //{
            //    foreach (AxonContLib.cRadioButton Cont in PanelCut.Controls)
            //    {
            //        Cont.ForeColor = mDeSelectColor;
            //        Cont.BackColor = mDSelectedBackColor;
            //    }

            //    AxonContLib.cRadioButton rbSelected = PanelCut.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
            //    rbSelected.ForeColor = mSelectedColor;
            //    rbSelected.BackColor = mSelectedBackColor;
            //}
            //else if (rd.ToolTips == "POL")
            //{
            //    foreach (AxonContLib.cRadioButton Cont in PanelPol.Controls)
            //    {
            //        Cont.ForeColor = mDeSelectColor;
            //        Cont.BackColor = mDSelectedBackColor;
            //    }

            //    AxonContLib.cRadioButton rbSelected = PanelPol.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
            //    rbSelected.ForeColor = mSelectedColor;
            //    rbSelected.BackColor = mSelectedBackColor;
            //}
            //else if (rd.ToolTips == "SYM")
            //{
            //    foreach (AxonContLib.cRadioButton Cont in PanelSym.Controls)
            //    {
            //        Cont.ForeColor = mDeSelectColor;
            //        Cont.BackColor = mDSelectedBackColor;
            //    }

            //    AxonContLib.cRadioButton rbSelected = PanelSym.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
            //    rbSelected.ForeColor = mSelectedColor;
            //    rbSelected.BackColor = mSelectedBackColor;
            //}
            //else if (rd.ToolTips == "FL")
            //{
            //    foreach (AxonContLib.cRadioButton Cont in PanelFL.Controls)
            //    {
            //        Cont.ForeColor = mDeSelectColor;
            //        Cont.BackColor = mDSelectedBackColor;
            //    }

            //    AxonContLib.cRadioButton rbSelected = PanelFL.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
            //    rbSelected.ForeColor = mSelectedColor;
            //    rbSelected.BackColor = mSelectedBackColor;
            //}
            //else if (rd.ToolTips == "LAB")
            //{
            //    foreach (AxonContLib.cRadioButton Cont in PanelLab.Controls)
            //    {
            //        Cont.ForeColor = mDeSelectColor;
            //        Cont.BackColor = mDSelectedBackColor;
            //    }

            //    AxonContLib.cRadioButton rbSelected = PanelLab.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
            //    rbSelected.ForeColor = mSelectedColor;
            //    rbSelected.BackColor = mSelectedBackColor;
            //}
            //else if (rd.ToolTips == "LBLC")
            //{
            //    foreach (AxonContLib.cRadioButton Cont in PanelLBLC.Controls)
            //    {
            //        Cont.ForeColor = mDeSelectColor;
            //        Cont.BackColor = mDSelectedBackColor;
            //    }

            //    AxonContLib.cRadioButton rbSelected = PanelLBLC.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
            //    rbSelected.ForeColor = mSelectedColor;
            //    rbSelected.BackColor = mSelectedBackColor;
            //}
            FindRap();
        }

        public void DesignSystemRadioButtion(Panel PNL, string pStrParaType, string pStrDisplayText, string toolTips, bool ISAddBlank)
        {
            string StrParam = "";
            StrParam = pStrParaType == "COLOR" ? "And ParaCode <= 11" : pStrParaType == "CLARITY" ? "And ParaCode <=  27" : "And 1=1";

            DataRow[] UDRow = DTabParameter.Select("ParaType = '" + pStrParaType + "' " + StrParam);

            if (UDRow.Length == 0)
            {
                return;
            }

            DataTable DTab = UDRow.CopyToDataTable();

            if (ISAddBlank == true)
            {
                DataRow DRNew = DTab.NewRow();
                DRNew["PARA_ID"] = 0;
                DRNew["PARANAME"] = " - ";
                DRNew["PARACODE"] = "";
                DRNew["SHORTNAME"] = " - ";
                DRNew["SEQUENCENO"] = 0;
                DTab.Rows.Add(DRNew);
            }

            DTab.DefaultView.Sort = "SequenceNo";
            DTab = DTab.DefaultView.ToTable();

            PNL.Controls.Clear();

            int IntI = 0;
            foreach (DataRow DRow in DTab.Rows)
            {
                AxonContLib.cRadioButton ValueList = new AxonContLib.cRadioButton();
                ValueList.Text = DRow[pStrDisplayText].ToString();
                ValueList.Tag = DRow["PARA_ID"].ToString();
                ValueList.AccessibleDescription = Val.ToString(DRow["PARACODE"]);
                ValueList.ToolTips = toolTips;
                ValueList.AutoSize = true;
                ValueList.Click += new EventHandler(cRadioShapeButton_Click);
                ValueList.Cursor = Cursors.Hand;
                ValueList.Font = lblLot.Font;
                if (IntI == 0)
                {
                    ValueList.Checked = true;
                    ValueList.ForeColor = mSelectedColor;
                    ValueList.BackColor = mSelectedBackColor;
                }
                else
                {
                    ValueList.Checked = false;
                    ValueList.ForeColor = mDeSelectColor;
                    ValueList.BackColor = mDSelectedBackColor;
                }

                PNL.Controls.Add(ValueList);

                IntI++;
            }
        }
        public void DesignComboBox(AxonContLib.cComboBox ComboBox, string pStrParaType, string pStrDisplayText, string pStrTooltip)
        {
            DataRow[] UDRow = DTabParameter.Select("ParaType = '" + pStrParaType + "'");

            if (UDRow.Length == 0)
            {
                return;
            }

            DataTable DTab = UDRow.CopyToDataTable();
            DTab.DefaultView.Sort = "SequenceNo";
            DTab = DTab.DefaultView.ToTable();


            if (pStrParaType == "CUT")
            {
                //DtabCut.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DtabCut.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbCut.AccessibleDescription = pStrParaType;
                CmbCut.DataSource = DtabCut;
                CmbCut.DisplayMember = pStrDisplayText;
                CmbCut.ValueMember = "PARA_ID";
                CmbCut.ToolTips = pStrTooltip;
            }
            else if (pStrParaType == "POLISH")
            {
                //DtabPol.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DtabPol.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbPol.AccessibleDescription = pStrParaType;
                CmbPol.DataSource = DtabPol;
                CmbPol.DisplayMember = pStrDisplayText;
                CmbPol.ValueMember = "PARA_ID";
                CmbPol.ToolTips = pStrTooltip;
            }
            else if (pStrParaType == "SYMMETRY")
            {
                //DtabSym.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DtabSym.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbSym.AccessibleDescription = pStrParaType;
                CmbSym.DataSource = DtabSym;
                CmbSym.DisplayMember = pStrDisplayText;
                CmbSym.ValueMember = "PARA_ID";
                CmbSym.ToolTips = pStrTooltip;
            }
            else if (pStrParaType == "FLUORESCENCE")
            {
                //DtabFL.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DtabFL.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbFL.AccessibleDescription = pStrParaType;
                CmbFL.DataSource = DtabFL;
                CmbFL.DisplayMember = pStrDisplayText;
                CmbFL.ValueMember = "PARA_ID";
                CmbFL.ToolTips = pStrTooltip;
            }
            else if (pStrParaType == "LAB")
            {
                DtabLab.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DtabLab.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbLab.AccessibleDescription = pStrParaType;
                CmbLab.DataSource = DtabLab;
                CmbLab.DisplayMember = pStrDisplayText;
                CmbLab.ValueMember = "PARA_ID";
                CmbLab.ToolTips = pStrTooltip;
            }
            else if (pStrParaType == "LBLC")
            {
                DtabLBLC.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
                foreach (DataRow DRow in DTab.Rows)
                {
                    DtabLBLC.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
                }
                CmbLBLC.AccessibleDescription = pStrParaType;
                CmbLBLC.DataSource = DtabLBLC;
                CmbLBLC.DisplayMember = pStrDisplayText;
                CmbLBLC.ValueMember = "PARA_ID";
                CmbLBLC.ToolTips = pStrTooltip;
            }

            //else if (pStrParaType == "LBLC")
            //{
            //    DataLBLC.Add(new DataStructure() { PARA_ID = 0, PARACODE = "", PARANAME = "" });
            //    foreach (DataRow DRow in DTab.Rows)
            //    {
            //        DataLBLC.Add(new DataStructure() { PARA_ID = Val.ToInt(DRow["PARA_ID"]), PARACODE = Val.ToString(DRow["PARACODE"]), PARANAME = Val.ToString(DRow["PARANAME"]) });
            //    }
            //    CmbLBLC.AccessibleDescription = pStrParaType;
            //    CmbLBLC.DataSource = DataLBLC;
            //    CmbLBLC.DisplayMember = pStrDisplayText;
            //    CmbLBLC.ValueMember = "PARA_ID";
            //}

        }
        public class DataStructure
        {
            public int PARA_ID { get; set; }
            public string PARACODE { get; set; }
            public string PARANAME { get; set; }
        }

        #endregion

        #region MyRegion

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString("ASSORT - TENDER", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString(GrpPacketSearch.Text, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("[ " + txtRoughType.Text + " ] " + DateTime.Now.ToString("dd/MM/yyyy"), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 400, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
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

        private void lblPrint_Click(object sender, EventArgs e)
        {

        }

        private void lblDeptExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "PacketWiseStock";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrid,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [PacketWiseStock.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        #endregion

        #region KeyPress Events

        private void txtLotNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (Global.OnKeyPressToOpenPopup(e))
            //    {
            //        FrmSearch FrmSearch = new FrmSearch();
            //        FrmSearch.SearchField = "LOTNO";
            //        FrmSearch.SearchText = e.KeyChar.ToString();
            //        this.Cursor = Cursors.WaitCursor;
            //        FrmSearch.DTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_ROUGH);
            //        FrmSearch.ColumnsToHide = "ROUGH_ID";
            //        FrmSearch.ISPostBack = true;
            //        FrmSearch.ISPostBackColumn = "LOTNO";
            //        this.Cursor = Cursors.Default;
            //        FrmSearch.ShowDialog();
            //        e.Handled = true;
            //        if (FrmSearch.DRow != null)
            //        {
            //            if (Val.ToString(FrmSearch.DRow["ROUGH_ID"]).Length == 0)
            //            {
            //                txtLotNo.Tag = "";
            //                txtMainCarat.Text = "";
            //                txtCheckBy.Text = "";
            //                CmbRapDate.SelectedIndex = 0;
            //                txtLotNo.Text = Val.ToString(FrmSearch.DRow["LOTNO"]);

            //                txtImpDollarRate.Text = Val.ToString(System.Configuration.ConfigurationManager.AppSettings["ImpDollarRate"]); ;
            //                txtExpDollarRate.Text = Val.ToString(System.Configuration.ConfigurationManager.AppSettings["ExpDollarRate"]);
            //                txtProfitPer.Text = Val.ToString(System.Configuration.ConfigurationManager.AppSettings["ProfitPer"]);
            //                txtLabourPer.Text = Val.ToString(System.Configuration.ConfigurationManager.AppSettings["LabourPer"]);
            //                txtRoughImportPer.Text = Val.ToString(System.Configuration.ConfigurationManager.AppSettings["RoughImportPer"]);
            //                txtCostPer.Text = Val.ToString(System.Configuration.ConfigurationManager.AppSettings["CostPer"]);

            //                lblMode.Text = "Add Mode";

            //                txtTenderName.Focus();
            //            }
            //            else
            //            {
            //                txtLotNo.Tag = Val.ToString(FrmSearch.DRow["ROUGH_ID"]);
            //                txtLotNo.Text = Val.ToString(FrmSearch.DRow["LOTNO"]);

            //                txtTenderName.Text = Val.ToString(FrmSearch.DRow["TENDERNAME"]);
            //                txtRoughName.Text = Val.ToString(FrmSearch.DRow["ROUGHNAME"]);

            //                txtCheckBy.Text = Val.ToString(FrmSearch.DRow["CHECKBY"]);

            //                txtImpDollarRate.Text = Val.ToString(FrmSearch.DRow["IMPDOLLARRATE"]);
            //                txtExpDollarRate.Text = Val.ToString(FrmSearch.DRow["EXPDOLLARRATE"]);

            //                txtRoughImportPer.Text = Val.ToString(FrmSearch.DRow["ROUGHIMPORTPER"]);
            //                txtRoughImportAmount.Text = Val.ToString(FrmSearch.DRow["ROUGHIMPORTAMOUNT"]);

            //                txtProfitPer.Text = Val.ToString(FrmSearch.DRow["PROFITPER"]);
            //                txtProfitAmount.Text = Val.ToString(FrmSearch.DRow["PROFITAMOUNT"]);

            //                txtLabourPer.Text = Val.ToString(FrmSearch.DRow["LABOURPER"]);
            //                txtLabourAmount.Text = Val.ToString(FrmSearch.DRow["LABOURAMOUNT"]);

            //                txtMainCarat.Text = Val.ToString(FrmSearch.DRow["MAINCARAT"]);
            //                txtNote.Text = Val.ToString(FrmSearch.DRow["NOTE"]);
            //                DTPEntryDate.Value = DateTime.Parse(Val.ToString(FrmSearch.DRow["TENDERDATE"]));
            //                CmbRapDate.SelectedItem = DateTime.Parse(Val.ToString(FrmSearch.DRow["RAPDATE"])).ToString("dd/MM/yyyy");

            //                lblMode.Text = "Edit Mode";

            //                GetData();
            //            }
            //        }
            //        FrmSearch.Hide();
            //        FrmSearch.Dispose();
            //        FrmSearch = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message);
            //}
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
                    FrmSearch.mColumnsToHide = "SHAPE_ID,SHAPECODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "SHAPECODE", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "SHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPENAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "SHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
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

        private void txtCol1_KeyPress(object sender, KeyPressEventArgs e)
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
                    FrmSearch.mColumnsToHide = "COLOR_ID,COLORCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "COLCODE1", Val.ToString(FrmSearch.mDRow["COLORCODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "COLNAME1", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "COL_ID1", Val.ToString(FrmSearch.mDRow["COLOR_ID"]));
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

        private void txtCol2_KeyPress(object sender, KeyPressEventArgs e)
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
                    FrmSearch.mColumnsToHide = "COLOR_ID,COLORCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "COLCODE2", Val.ToString(FrmSearch.mDRow["COLORCODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "COLNAME2", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "COL_ID2", Val.ToString(FrmSearch.mDRow["COLOR_ID"]));
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

        private void txtCla1_KeyPress(object sender, KeyPressEventArgs e)
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
                    FrmSearch.mColumnsToHide = "CLARITY_ID,CLARITYCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "CLACODE1", Val.ToString(FrmSearch.mDRow["CLARITYCODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "CLANAME1", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "CLA_ID1", Val.ToString(FrmSearch.mDRow["CLARITY_ID"]));
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

        private void txtCla2_KeyPress(object sender, KeyPressEventArgs e)
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
                    FrmSearch.mColumnsToHide = "CLARITY_ID,CLARITYCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "CLACODE2", Val.ToString(FrmSearch.mDRow["CLARITYCODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "CLANAME2", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "CLA_ID2", Val.ToString(FrmSearch.mDRow["CLARITY_ID"]));
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
                    FrmSearch.mColumnsToHide = "CUT_ID,CUTCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "CUTCODE", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "CUTNAME", Val.ToString(FrmSearch.mDRow["CUTNAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "CUT_ID", Val.ToString(FrmSearch.mDRow["CUT_ID"]));
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
                    FrmSearch.mColumnsToHide = "POL_ID,POLCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "POLCODE", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "POLNAME", Val.ToString(FrmSearch.mDRow["POLNAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "POL_ID", Val.ToString(FrmSearch.mDRow["POL_ID"]));
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
                    FrmSearch.mColumnsToHide = "SYM_ID,SYMCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "SYMCODE", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "SYMNAME", Val.ToString(FrmSearch.mDRow["SYMNAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "SYM_ID", Val.ToString(FrmSearch.mDRow["SYM_ID"]));
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
                    FrmSearch.mColumnsToHide = "FL_ID,FLCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;

                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "FLCODE", Val.ToString(FrmSearch.mDRow["FLCODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "FLNAME", Val.ToString(FrmSearch.mDRow["FLNAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "FL_ID", Val.ToString(FrmSearch.mDRow["FL_ID"]));
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
                    FrmSearch.mColumnsToHide = "LBLC_ID,LBLCCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "LBLCCODE", Val.ToString(FrmSearch.mDRow["LBLCCODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "LBLCNAME", Val.ToString(FrmSearch.mDRow["LBLCNAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "LBLC_ID", Val.ToString(FrmSearch.mDRow["LBLC_ID"]));
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
                    FrmSearch.mColumnsToHide = "TENSION_ID,TENSIONCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;

                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "TENSIONCODE", Val.ToString(FrmSearch.mDRow["TENSIONCODE"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "TENSIONNAME", Val.ToString(FrmSearch.mDRow["TENSIONNAME"]));
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "TENSION_ID", Val.ToString(FrmSearch.mDRow["TENSION_ID"]));
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



        #endregion

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
            switch (e.Column.FieldName.ToUpper())
            {

                case "ROUGHCARAT":
                case "ROUGHPCS":
                case "POLISHPER":
                case "POLISHCARATMANUAL":
                case "DISCOUNTMANUAL":
                    //case "SHAPENAME":
                    //case "COLNAME1":
                    //case "COLNAME2":
                    //case "CLANAME1":
                    //case "CLANAME2":
                    //case "CUTNAME":
                    //case "POLNAME":
                    //case "SYMNAME":
                    //case "FLNAME":
                    //case "LBLCNAME":
                    //case "LABNAME":
                    //case "TENSIONNAME":

                    double DouRoughCarat = 0;
                    double DouRoughPer = 0;

                    DouRoughCarat = Val.Val(DRow["ROUGHCARAT"]);
                    double DouRoughPcs = Val.Val(DRow["ROUGHPCS"]);
                    double DouRoughSize = DouRoughPcs == 0 ? 0 : Math.Round(DouRoughCarat / DouRoughPcs, 2);

                    DouRoughPer = Val.Val(txtMainCarat.Text) == 0 ? 0 : Math.Round((DouRoughCarat / Val.Val(txtMainCarat.Text)) * 100, 2);

                    DRow["ROUGHPER"] = DouRoughPer;
                    DRow["ROUGHSIZE"] = DouRoughSize;

                    double DouPolPer = Val.Val(DRow["POLISHPER"]);
                    double DouPolishCarat = Math.Round((DouRoughSize * DouPolPer) / 100, 2);

                    DRow["POLISHCARAT"] = DouPolishCarat;

                    DTabData.AcceptChanges();

                    FindRap();

                    break;

                default:
                    break;
            }

            GrdDet.BestFitColumns();

        }

        public void GetData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DTabData.Rows.Clear();
                DTabData = Obj.GetTenderData(Val.ToString(txtRoughID.Text));

                GrdDet.BeginUpdate();

                MainGrid.DataSource = DTabData;
                GrdDet.RefreshData();
                GrdDet.BestFitColumns();
                GrdDet.EndUpdate();
                //Calculation();
                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

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
                if (mStrType == "SHOWCLICK")
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                AxonContLib.cRadioButton rbShp = PanelShape.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                GrdDet.SetFocusedRowCellValue("SHAPE_ID", Val.ToString(rbShp.Tag));
                GrdDet.SetFocusedRowCellValue("SHAPECODE", Val.ToString(rbShp.AccessibleDescription));
                GrdDet.SetFocusedRowCellValue("SHAPENAME", Val.ToString(rbShp.Text));

                AxonContLib.cRadioButton rbCol1 = PanelColor1.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                GrdDet.SetFocusedRowCellValue("COL_ID1", Val.ToString(rbCol1.Tag));
                GrdDet.SetFocusedRowCellValue("COLCODE1", Val.ToString(rbCol1.AccessibleDescription));
                GrdDet.SetFocusedRowCellValue("COLNAME1", Val.ToString(rbCol1.Text));

                AxonContLib.cRadioButton rbCla1 = PanelClarity1.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                GrdDet.SetFocusedRowCellValue("CLA_ID1", Val.ToString(rbCla1.Tag));
                GrdDet.SetFocusedRowCellValue("CLACODE1", Val.ToString(rbCla1.AccessibleDescription));
                GrdDet.SetFocusedRowCellValue("CLANAME1", Val.ToString(rbCla1.Text));

                AxonContLib.cRadioButton rbCol2 = PanelColor2.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                GrdDet.SetFocusedRowCellValue("COL_ID2", Val.ToString(rbCol2.Tag));
                GrdDet.SetFocusedRowCellValue("COLCODE2", Val.ToString(rbCol2.AccessibleDescription));
                GrdDet.SetFocusedRowCellValue("COLNAME2", Val.ToString(rbCol2.Text));

                AxonContLib.cRadioButton rbCla2 = PanelClarity2.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                GrdDet.SetFocusedRowCellValue("CLA_ID2", Val.ToString(rbCla2.Tag));
                GrdDet.SetFocusedRowCellValue("CLACODE2", Val.ToString(rbCla2.AccessibleDescription));
                GrdDet.SetFocusedRowCellValue("CLANAME2", Val.ToString(rbCla2.Text));

                //AxonContLib.cRadioButton rbCut = PanelCut.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                //GrdDet.SetFocusedRowCellValue("CUT_ID", Val.ToString(rbCut.Tag));
                //GrdDet.SetFocusedRowCellValue("CUTCODE", Val.ToString(rbCut.AccessibleDescription));
                //GrdDet.SetFocusedRowCellValue("CUTNAME", Val.ToString(rbCut.Text));

                //AxonContLib.cRadioButton rbPol = PanelPol.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                //GrdDet.SetFocusedRowCellValue("POL_ID", Val.ToString(rbPol.Tag));
                //GrdDet.SetFocusedRowCellValue("POLCODE", Val.ToString(rbPol.AccessibleDescription));
                //GrdDet.SetFocusedRowCellValue("POLNAME", Val.ToString(rbPol.Text));

                //AxonContLib.cRadioButton rbSym = PanelSym.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                //GrdDet.SetFocusedRowCellValue("SYM_ID", Val.ToString(rbSym.Tag));
                //GrdDet.SetFocusedRowCellValue("SYMCODE", Val.ToString(rbSym.AccessibleDescription));
                //GrdDet.SetFocusedRowCellValue("SYMNAME", Val.ToString(rbSym.Text));

                //AxonContLib.cRadioButton rbFL = PanelFL.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                //GrdDet.SetFocusedRowCellValue("FL_ID", Val.ToString(rbFL.Tag));
                //GrdDet.SetFocusedRowCellValue("FLCODE", Val.ToString(rbFL.AccessibleDescription));
                //GrdDet.SetFocusedRowCellValue("FLNAME", Val.ToString(rbFL.Text));

                //AxonContLib.cRadioButton rbLab = PanelLab.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                //GrdDet.SetFocusedRowCellValue("LAB_ID", Val.ToString(rbLab.Tag));
                //GrdDet.SetFocusedRowCellValue("LABCODE", Val.ToString(rbLab.AccessibleDescription));
                //GrdDet.SetFocusedRowCellValue("LABNAME", Val.ToString(rbLab.Text));

                //AxonContLib.cRadioButton rbLBLC = PanelLBLC.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault(r => r.Checked);
                //GrdDet.SetFocusedRowCellValue("LBLC_ID", Val.ToString(rbLBLC.Tag));
                //GrdDet.SetFocusedRowCellValue("LBLCCODE", Val.ToString(rbLBLC.AccessibleDescription));
                //GrdDet.SetFocusedRowCellValue("LBLCNAME", Val.ToString(rbLBLC.Text));

                DataStructure selectedDataStructureCut = CmbCut.SelectedItem as DataStructure;
                GrdDet.SetFocusedRowCellValue("CUT_ID", selectedDataStructureCut.PARA_ID);
                GrdDet.SetFocusedRowCellValue("CUTCODE", selectedDataStructureCut.PARACODE);
                GrdDet.SetFocusedRowCellValue("CUTNAME", selectedDataStructureCut.PARACODE);

                DataStructure selectedDataStructurePol = CmbPol.SelectedItem as DataStructure;
                GrdDet.SetFocusedRowCellValue("POL_ID", selectedDataStructurePol.PARA_ID);
                GrdDet.SetFocusedRowCellValue("POLCODE", selectedDataStructurePol.PARACODE);
                GrdDet.SetFocusedRowCellValue("POLNAME", selectedDataStructurePol.PARACODE);

                DataStructure selectedDataStructureSym = CmbSym.SelectedItem as DataStructure;
                GrdDet.SetFocusedRowCellValue("SYM_ID", selectedDataStructureSym.PARA_ID);
                GrdDet.SetFocusedRowCellValue("SYMCODE", selectedDataStructureSym.PARACODE);
                GrdDet.SetFocusedRowCellValue("SYMNAME", selectedDataStructureSym.PARACODE);

                DataStructure selectedDataStructureFL = CmbFL.SelectedItem as DataStructure;
                GrdDet.SetFocusedRowCellValue("FL_ID", selectedDataStructureFL.PARA_ID);
                GrdDet.SetFocusedRowCellValue("FLCODE", selectedDataStructureFL.PARACODE);
                GrdDet.SetFocusedRowCellValue("FLNAME", selectedDataStructureFL.PARANAME);

                DataStructure selectedDataStructureLab = CmbLab.SelectedItem as DataStructure;
                GrdDet.SetFocusedRowCellValue("LAB_ID", selectedDataStructureLab.PARA_ID);
                GrdDet.SetFocusedRowCellValue("LABCODE", selectedDataStructureLab.PARACODE);
                GrdDet.SetFocusedRowCellValue("LABNAME", selectedDataStructureLab.PARANAME);

                DataStructure selectedDataStructureLBLC = CmbLBLC.SelectedItem as DataStructure;
                GrdDet.SetFocusedRowCellValue("LBLC_ID", selectedDataStructureLBLC.PARA_ID);
                GrdDet.SetFocusedRowCellValue("LBLCCODE", selectedDataStructureLBLC.PARACODE);
                GrdDet.SetFocusedRowCellValue("LBLCNAME", selectedDataStructureLBLC.PARANAME);

                DataRow DRow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

                if (ChkDefaultEXVGVG.Checked == true && Val.Trim(DRow["CUTNAME"]) == "" && Val.Trim(DRow["POLNAME"]) == "" && Val.Trim(DRow["SYMNAME"]) == "")
                {
                    DRow["CUTNAME"] = "EX3";
                    DRow["CUTCODE"] = "EX3";
                    DRow["CUT_ID"] = 113;

                    DRow["POLNAME"] = "VG";
                    DRow["POLCODE"] = "VG";
                    DRow["POL_ID"] = 346;

                    DRow["SYMNAME"] = "VG";
                    DRow["SYMCODE"] = "VG";
                    DRow["SYM_ID"] = 373;
                }

                Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

                if (Val.ToString(DRow["SHAPENAME"]) != "")
                    clsFindRap.SHAPECODE = Val.ToString(DRow["SHAPECODE"]);
                else
                    clsFindRap.SHAPECODE = "";


                if (Val.ToString(DRow["COLNAME1"]) != "")
                {
                    clsFindRap.COLOR_ID1 = Val.ToInt32(DRow["COL_ID1"]);
                    clsFindRap.COLORCODE1 = Val.ToString(DRow["COLCODE1"]);
                }
                else
                {
                    clsFindRap.COLOR_ID1 = 0;
                    clsFindRap.COLORCODE1 = "";
                }

                if (Val.ToString(DRow["CLANAME1"]) != "")
                {
                    clsFindRap.CLARITY_ID1 = Val.ToInt32(DRow["CLA_ID1"]);
                    clsFindRap.CLARITYCODE1 = Val.ToString(DRow["CLACODE1"]);
                }
                else
                {
                    clsFindRap.CLARITY_ID1 = 0;
                    clsFindRap.CLARITYCODE1 = "";
                }

                if (Val.ToString(DRow["COLNAME2"]) != "")
                {
                    clsFindRap.COLOR_ID2 = Val.ToInt32(DRow["COL_ID2"]);
                    clsFindRap.COLORCODE2 = Val.ToString(DRow["COLCODE2"]);
                }
                else
                {
                    clsFindRap.COLOR_ID2 = 0;
                    clsFindRap.COLORCODE2 = "";
                }

                if (Val.ToString(DRow["CLANAME2"]) != "")
                {
                    clsFindRap.CLARITY_ID2 = Val.ToInt32(DRow["CLA_ID2"]);
                    clsFindRap.CLARITYCODE2 = Val.ToString(DRow["CLACODE2"]);
                }
                else
                {
                    clsFindRap.CLARITY_ID2 = 0;
                    clsFindRap.CLARITYCODE2 = "";
                }

                if (Val.Val(DRow["POLISHCARATMANUAL"]) != 0)
                {
                    clsFindRap.CARAT = Val.Val(DRow["POLISHCARATMANUAL"]);
                }
                else
                {
                    clsFindRap.CARAT = Val.Val(DRow["POLISHCARAT"]);
                }

                if (Val.ToString(DRow["CUTNAME"]) != "")
                    clsFindRap.CUTCODE = Val.ToString(DRow["CUTCODE"]);
                else
                    clsFindRap.CUTCODE = "";

                if (Val.ToString(DRow["POLNAME"]) != "")
                    clsFindRap.POLCODE = Val.ToString(DRow["POLCODE"]);
                else
                    clsFindRap.POLCODE = "";

                if (Val.ToString(DRow["SYMNAME"]) != "")
                    clsFindRap.SYMCODE = Val.ToString(DRow["SYMCODE"]);
                else
                    clsFindRap.SYMCODE = "";

                if (Val.ToString(DRow["FLNAME"]) != "")
                    clsFindRap.FLCODE = Val.ToString(DRow["FLCODE"]);
                else
                    clsFindRap.FLCODE = "";


                if (Val.ToString(DRow["TENSIONNAME"]) != "")
                    clsFindRap.TENSIONCODE = Val.ToString(DRow["TENSIONCODE"]);
                else
                    clsFindRap.TENSIONCODE = "";

                if (Val.ToString(DRow["LABNAME"]) != "")
                    clsFindRap.LABCODE = Val.ToString(DRow["LABCODE"]);
                else
                    clsFindRap.LABCODE = "";

                clsFindRap.NATTSCODE = "";


                if (Val.ToString(DRow["LBLCNAME"]) != "")
                    clsFindRap.LBLCCODE = Val.ToString(DRow["LBLCCODE"]);
                else
                    clsFindRap.LBLCCODE = "";

                clsFindRap.DISCOUNTMANUAL = Val.Val(DRow["DISCOUNTMANUAL"]);

                clsFindRap.SHAPENAME = Val.ToString(DRow["SHAPENAME"]);
                clsFindRap.CUTNAME = Val.ToString(DRow["CUTNAME"]);
                clsFindRap.POLNAME = Val.ToString(DRow["POLNAME"]);
                clsFindRap.SYMNAME = Val.ToString(DRow["SYMNAME"]);
                clsFindRap.FLNAME = Val.ToString(DRow["FLNAME"]);
                clsFindRap.LBLCNAME = Val.ToString(DRow["LBLCNAME"]);

                clsFindRap.RAPDATE = Val.SqlDate(Val.ToString(CmbRapDate.SelectedItem));

                if (clsFindRap.SHAPECODE == "" || clsFindRap.COLORCODE1 == "" || clsFindRap.CLARITYCODE1 == "" || clsFindRap.CARAT == 0)
                {
                    this.Cursor = Cursors.Default;
                    DTabData.Rows[GrdDet.FocusedRowHandle]["RAPAPORT"] = 0;
                    DTabData.Rows[GrdDet.FocusedRowHandle]["PRICEPERCART"] = 0;
                    DTabData.Rows[GrdDet.FocusedRowHandle]["POLISHAMOUNT"] = 0;
                    DTabData.Rows[GrdDet.FocusedRowHandle]["DISCOUNT"] = 0;
                    DTabData.Rows[GrdDet.FocusedRowHandle]["AMOUNTDISCOUNT"] = 0;
                    DTabData.Rows[GrdDet.FocusedRowHandle]["XMLDETAIL"] = "";
                    DTabData.Rows[GrdDet.FocusedRowHandle]["RAPDATE"] = DBNull.Value;

                    clsFindRap = null;

                    Calculation();
                    return;
                }

                clsFindRap = Obj.FindTenderRapWithUpDown(clsFindRap);

                DTabData.Rows[GrdDet.FocusedRowHandle]["RAPAPORT"] = clsFindRap.RAPAPORT;
                DTabData.Rows[GrdDet.FocusedRowHandle]["PRICEPERCART"] = clsFindRap.PRICEPERCARAT;
                DTabData.Rows[GrdDet.FocusedRowHandle]["POLISHAMOUNT"] = clsFindRap.AMOUNT;
                DTabData.Rows[GrdDet.FocusedRowHandle]["DISCOUNT"] = clsFindRap.DISCOUNT;
                DTabData.Rows[GrdDet.FocusedRowHandle]["AMOUNTDISCOUNT"] = clsFindRap.AMOUNTDISCOUNT;
                DTabData.Rows[GrdDet.FocusedRowHandle]["XMLDETAIL"] = clsFindRap.XMLDETAIL;
                DTabData.Rows[GrdDet.FocusedRowHandle]["RAPDATE"] = DTPEntryDate.Value.ToShortDateString();

                clsFindRap = null;

                Calculation();

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

        public void Calculation(int Intow = 0)
        {
            try
            {
                double DouTotalRoughCarat = 0;
                int IntTotalRoughPcs = 0;
                double DouTotalPolishCarat = 0;
                double DouTotalPolishAmount = 0;
                double DouTotalRoughAmount = 0;
                double DouTotalPolishPcs = 0;
                double DouTotalManualPolishPcs = 0;

                foreach (DataRow DRow in DTabData.Rows)
                {
                    double DouRoughCarat = Val.Val(DRow["ROUGHCARAT"]);
                    double DouRoughPcs = Val.Val(DRow["ROUGHPCS"]);
                    double DouRoughSize = DouRoughPcs == 0 ? 0 : Math.Round(DouRoughCarat / DouRoughPcs, 2);
                    DRow["ROUGHSIZE"] = DouRoughSize;

                    double DouRoughPer = Val.Val(txtMainCarat.Text) == 0 ? 0 : Math.Round((DouRoughCarat / Val.Val(txtMainCarat.Text)) * 100, 2);
                    DRow["ROUGHSIZE"] = DouRoughSize;


                    double DouPolPer = Val.Val(DRow["POLISHPER"]);
                    double DouPolishCarat = Math.Round((DouRoughSize * DouPolPer) / 100, 2);

                    DRow["POLISHCARAT"] = DouPolishCarat;

                    double DouAmount = 0;

                    if (Val.Val(DRow["DISCOUNTMANUAL"]) != 0)
                    {
                        DouAmount = Val.Val(DRow["POLISHAMOUNT"]);
                        double DouRapaport = Val.Val(DRow["RAPAPORT"]);
                        double DouDiscount = Val.Val(DRow["DISCOUNT"]);
                        double DouAddDisc = Val.Val(DRow["DISCOUNTMANUAL"]);

                        double Crt = Val.Val(DRow["POLISHCARAT"]);
                        if (Val.Val(DRow["POLISHCARATMANUAL"]) != 0)
                        {
                            Crt = Val.Val(DRow["POLISHCARATMANUAL"]);
                        }

                        double pricepercarat = Math.Round(DouRapaport - (DouRapaport * ((DouDiscount + DouAddDisc) / 100)), 2);
                        double Ans = Math.Round(pricepercarat * Crt, 2);

                        DRow["PRICEPERCART"] = pricepercarat;
                        DRow["POLISHAMOUNT"] = Ans;

                    }

                    DouAmount = Val.Val(DRow["POLISHAMOUNT"]);

                    DRow["POLISHAMOUNTROUGHCARAT"] = DouRoughSize == 0 ? 0 : Math.Round(DouAmount / DouRoughSize, 2);

                    DRow["IMPDOLLARPRICEPERCARAT"] = Math.Round(Val.Val(DRow["POLISHAMOUNTROUGHCARAT"]) * Val.Val(txtImpDollarRate.Text), 2);
                    DRow["EXPDOLLARPRICEPERCARAT"] = Val.Val(txtExpDollarRate.Text) == 0 ? 0 : Math.Round(Val.Val(DRow["IMPDOLLARPRICEPERCARAT"]) / Val.Val(txtExpDollarRate.Text), 2);

                    double DouRoughAmount = Math.Round(Val.Val(DRow["EXPDOLLARPRICEPERCARAT"]) * DouRoughCarat, 2);
                    DRow["ROUGHAMOUNT"] = DouRoughAmount;


                    DouTotalPolishPcs = DouTotalPolishPcs + (Val.Val(DRow["POLISHCARAT"]) * Val.Val(DRow["ROUGHPCS"]));
                    DouTotalManualPolishPcs = DouTotalManualPolishPcs + (Val.Val(DRow["POLISHCARATMANUAL"]) * Val.Val(DRow["ROUGHPCS"]));

                    DouTotalRoughCarat = DouTotalRoughCarat + Val.Val(DRow["ROUGHCARAT"]);
                    IntTotalRoughPcs = IntTotalRoughPcs + Val.ToInt(DRow["ROUGHPCS"]);
                    DouTotalPolishCarat = DouTotalPolishCarat + Val.Val(DRow["POLISHCARAT"]);

                    DouTotalPolishAmount = DouTotalPolishAmount + Val.Val(DRow["POLISHAMOUNT"]);
                    DouTotalRoughAmount = DouTotalRoughAmount + Val.Val(DRow["ROUGHAMOUNT"]);

                }

                txtPolishAmount.Text = Val.Format(DouTotalPolishAmount.ToString(), "########0.00");
                txtRoughAmount.Text = Val.Format(DouTotalRoughAmount.ToString(), "########0.00");

                double DouRoughPerCarat = Val.Val(txtMainCarat.Text) == 0 ? 0 : DouTotalRoughAmount / Val.Val(txtMainCarat.Text);
                txtRoughPerCarat.Text = Val.Format(DouRoughPerCarat, "########0.00");

                double DouCostPer = Val.Val(txtCostPer.Text);
                double DouRoughImportPer = Val.Val(txtRoughImportPer.Text);

                double DouLabourPer = Val.Val(txtLabourPer.Text);
                double DouFinalAvg = 0;

                double DouLabourAmount = (DouRoughPerCarat * DouLabourPer / 100);
                DouFinalAvg = DouRoughPerCarat - DouLabourAmount;

                double DouRImpAmount = (DouFinalAvg * DouRoughImportPer / 100);
                DouFinalAvg = DouFinalAvg - DouRImpAmount;

                txtFinalAvg.Text = Val.Format(DouFinalAvg, "########0.00");
                txtFinalAmount.Text = Val.Format(DouFinalAvg * Val.Val(txtMainCarat.Text), "########0.00");

                //Changed: #P : 18-08-2020 RoughCost And RoughAvg Vache na Per Calculate karvana
                double DouProfitPer = Val.Val(txtProfitPer.Text);
                //double DouProfitPer = Val.Val(txtFinalAvg.Text) == 0 ? 0 : Math.Round(((Val.Val(txtFinalAvg.Text) - Val.Val(txtPurchaseRate.Text)) / Val.Val(txtFinalAvg.Text)) * 100, 2);
                //txtProfitPer.Text = Val.ToString(DouProfitPer);
                //Changed: #P : 18-08-2020 FinalAvg pr Profit na Per Kadhine Amt Lavanu
                //double DouProfitAmount = (DouFinalAvg * DouProfitPer / 100);
                //DouFinalAvg = DouFinalAvg - DouProfitAmount;
                double DouProfitAmount = (DouFinalAvg * DouProfitPer / 100);
                DouFinalAvg = DouFinalAvg - DouProfitAmount;
                //End : #P : 18-08-2020


                //txtCostAmount.Text = Val.Format(DouCostAmount.ToString(), "########0.00");
                txtRoughImportAmount.Text = Val.Format(DouRImpAmount.ToString(), "########0.00");
                txtProfitAmount.Text = Val.Format(DouProfitAmount.ToString(), "########0.00");
                txtLabourAmount.Text = Val.Format(DouLabourAmount.ToString(), "########0.00");


                DTabData.AcceptChanges();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCheckBy.Text.Trim().Length == 0)
                {
                    Global.MessageError("Check By  Is Required");
                    txtCheckBy.Focus();
                    return;
                }
                if (DTabData.Rows.Count == 0)
                {
                    Global.MessageError("No Detail Data Found For Save");
                    return;
                }

                Trn_TenderProperty Property = new Trn_TenderProperty();

                Property.Rough_ID = Val.ToString(txtRoughID.Text);
                Property.Lot_ID = Val.ToString(txtLotID.Text);

                Property.LotNo = Val.ToString(txtRoughType.Text);
                Property.TenderName = Val.ToString(txtTenderName.Text);
                Property.RoughName = Val.ToString(txtRoughName.Text);

                Property.Size_ID = Val.ToInt32(txtSize.Tag);

                Property.LotNo = Val.ToString(txtRoughType.Text);
                Property.MainCarat = Val.Val(txtMainCarat.Text);
                Property.IMPDOLLARRATE = Val.Val(txtImpDollarRate.Text);
                Property.EXPDOLLARRATE = Val.Val(txtExpDollarRate.Text);

                Property.ROUGHIMPORTPER = Val.Val(txtRoughImportPer.Text);
                Property.ROUGHIMPORTAMOUNT = Val.Val(txtRoughImportAmount.Text);

                Property.PROFITPER = Val.Val(txtProfitPer.Text);
                Property.PROFITAMOUNT = Val.Val(txtProfitAmount.Text);

                Property.CHECKBY = txtCheckBy.Text;

                Property.LABOURPER = Val.Val(txtLabourPer.Text);
                Property.LABOURAMOUNT = Val.Val(txtLabourAmount.Text);

                Property.FINALAMOUNT = Val.Val(txtFinalAmount.Text);
                Property.FINALAVG = Val.Val(txtFinalAvg.Text);

                Property.RAPDATE = Val.SqlDate(Val.ToString(CmbRapDate.SelectedItem));

                Property.TenderDate = Val.SqlDate(DTPEntryDate.Value.ToShortDateString());
                Property.Note = txtNote.Text;

                string Xml = string.Empty;
                DTabData.TableName = "TABLE";
                using (StringWriter sw = new StringWriter())
                {
                    DTabData.WriteXml(sw);
                    Xml = sw.ToString();
                }

                Property.XmlDetail = Xml;

                Property = Obj.SaveTenderEntry(Property);
                Global.Message(Property.ReturnMessageDesc);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    //if (mStrClear != "Delete")
                    //{
                    //    BtnClear_Click(null, null);
                    //}
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }

        }

        public void AddDefaultSetting()
        {
            DataTable DTabSettings = Obj.GetAllSettings();

            foreach (DataRow DRow in DTabSettings.Rows)
            {
                if (Val.ToString(DRow["SettingKey"]) == "ImpDollarRate")
                {
                    txtImpDollarRate.Text = Val.ToString(DRow["SettingValue"]);
                }
                else if (Val.ToString(DRow["SettingKey"]) == "ExpDollarRate")
                {
                    txtExpDollarRate.Text = Val.ToString(DRow["SettingValue"]);
                }
                //else if (Val.ToString(DRow["SettingKey"]) == "ProfitPer")
                //{
                //    txtProfitPer.Text = Val.ToString(DRow["SettingValue"]);
                //}
                //else if (Val.ToString(DRow["SettingKey"]) == "LabourPer")
                //{
                //    txtLabourPer.Text = Val.ToString(DRow["SettingValue"]);
                //}
                //else if (Val.ToString(DRow["SettingKey"]) == "RoughImportPer")
                //{
                //    txtRoughImportPer.Text = Val.ToString(DRow["SettingValue"]);
                //}
                else if (Val.ToString(DRow["SettingKey"]) == "CostPer")
                {
                    txtCostPer.Text = Val.ToString(DRow["SettingValue"]);
                }
            }
            DTabSettings.Dispose();
            DTabSettings = null;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            lblMode.Text = "Add Mode";
            DTabData.Rows.Clear();
            txtNote.Text = string.Empty;
            txtMainCarat.Text = string.Empty;
            txtRoughType.Text = string.Empty;
            txtRoughType.Tag = string.Empty;

            txtTenderName.Text = string.Empty;
            txtSize.Tag = string.Empty;
            txtSize.Text = string.Empty;
            txtRoughName.Tag = string.Empty;
            txtRoughName.Text = string.Empty;


            txtPolishAmount.Text = string.Empty;
            txtRoughAmount.Text = string.Empty;

            txtCheckBy.Text = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGERNAME);
            txtCheckBy.Tag = Val.ToString(BOConfiguration.gEmployeeProperty.LEDGER_ID);
            PnlCalculation.Visible = false;
            txtPasswordForCalcPnl.Text = string.Empty;

            AddDefaultSetting();

            txtProfitAmount.Text = string.Empty;
            txtLabourAmount.Text = string.Empty;
            txtRoughImportAmount.Text = string.Empty;
            txtCostAmount.Text = string.Empty;

            txtFinalAmount.Text = string.Empty;
            txtFinalAvg.Text = string.Empty;

            txtImpDollarRate.Text = Obj.FindExchangeRate();
            DTPEntryDate.Value = DateTime.Now;
            CmbRapDate.SelectedIndex = 0;

            AxonContLib.cRadioButton rbShp = PanelShape.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault();
            rbShp.Checked = true;
            rbShp.ForeColor = mSelectedColor;
            rbShp.BackColor = mSelectedBackColor;

            AxonContLib.cRadioButton rbCol1 = PanelColor1.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault();
            rbCol1.Checked = true;
            rbCol1.ForeColor = mSelectedColor;
            rbCol1.BackColor = mSelectedBackColor;

            AxonContLib.cRadioButton rbCla1 = PanelClarity1.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault();
            rbCla1.Checked = true;
            rbCla1.ForeColor = mSelectedColor;
            rbCla1.BackColor = mSelectedBackColor;

            AxonContLib.cRadioButton rbCol2 = PanelColor2.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault();
            rbCol2.Checked = true;
            rbCol2.ForeColor = mSelectedColor;
            rbCol2.BackColor = mSelectedBackColor;

            AxonContLib.cRadioButton rbCla2 = PanelClarity2.Controls.OfType<AxonContLib.cRadioButton>().FirstOrDefault();
            rbCla2.Checked = true;
            rbCla2.ForeColor = mSelectedColor;
            rbCla2.BackColor = mSelectedBackColor;

            CmbCut.SelectedIndex = 0;
            CmbPol.SelectedIndex = 0;
            CmbSym.SelectedIndex = 0;
            CmbFL.SelectedIndex = 0;
            CmbLab.SelectedIndex = 0;
            CmbLBLC.SelectedIndex = 0;

            txtTenderName.Focus();
            BtnBlankNew_Click(null, null);

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void GrdDet_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (e.Column.FieldName == "RAPAPORT" || e.Column.FieldName == "DISCOUNT")
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        string StrXml = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "XMLDETAIL"));
                        if (StrXml == string.Empty)
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }

                        StringReader theReader = new StringReader(StrXml);
                        DataSet ds = new DataSet();
                        ds.ReadXml(theReader);

                        DataTable DTab = ds.Tables[0];

                        FrmDiscountBreakup FrmDiscountBreakup = new FrmDiscountBreakup();
                        FrmDiscountBreakup.MdiParent = Global.gMainRef;
                        FrmDiscountBreakup.ShowForm(DTab);

                        ObjFormEvent.ObjToDisposeList.Add(DTab);
                        ObjFormEvent.ObjToDisposeList.Add(ds);
                        this.Cursor = Cursors.Default;

                    }
                    catch (Exception EX)
                    {
                        Global.MessageError(EX.Message);
                    }
                }
            }

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure You Want To Delete  ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                Trn_TenderProperty Property = new Trn_TenderProperty();
                Property.Rough_ID = Val.ToString(txtRoughID.Text);
                Property.Lot_ID = Val.ToString(txtLotID.Text);
                Property.CHECKBY = txtCheckBy.Text;

                Property = Obj.DeleteTenderEntry(Property);
                Global.Message(Property.ReturnMessageDesc);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    mStrFinalAmount = 0;
                    mStrFinalRate = 0;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }
        }

        private void BtnRowDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow DRow = GrdDet.GetFocusedDataRow();

                if (Global.Confirm("Are You Sure You Want To Delete [" + Val.ToString(DRow["PACKETNO"]) + "]  ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                Trn_TenderProperty Property = new Trn_TenderProperty();
                Property.Rough_ID = Val.ToString(txtRoughType.Tag);
                Property.PacketNo = Val.ToInt(DRow["PACKETNO"]);

                Property = Obj.DeleteTenderDetail(Property);
                Global.Message(Property.ReturnMessageDesc);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    GrdDet.DeleteRow(GrdDet.FocusedRowHandle);
                }
                DTabData.AcceptChanges();

                foreach (DataRow DR in DTabData.Rows)
                {
                    double DouRoughCarat = Val.Val(DR["ROUGHCARAT"]);
                    double DouRoughPer = Val.Val(txtMainCarat.Text) == 0 ? 0 : Math.Round((DouRoughCarat / Val.Val(txtMainCarat.Text)) * 100, 2);
                    DRow["ROUGHPER"] = DouRoughPer;
                }
                DTabData.AcceptChanges();
                Calculation();
                BtnSave_Click(null, null);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }
        }

        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouRoughCarat = 0;
                    IntRoughPcs = 0;
                    DouPolishCarat = 0;

                    DouPolishPcsManual = 0;
                    DouPolishPcs = 0;


                    DouImportPrice = 0;
                    DouPolishAmountPerCarat = 0;
                    DouPolishAmount = 0;
                    DouRapAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouRoughCarat = DouRoughCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "ROUGHCARAT"));
                    DouPolishCarat = DouPolishCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLISHCARAT"));
                    IntRoughPcs = IntRoughPcs + Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "ROUGHPCS"));

                    DouPolishPcs = DouPolishPcs + (Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLISHCARAT")) * Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "ROUGHPCS")));
                    DouPolishPcsManual = DouPolishPcsManual + (Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLISHCARATMANUAL")) * Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "ROUGHPCS")));

                    DouPolishAmount = DouPolishAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLISHAMOUNT"));
                    DouRapAmount = DouRapAmount + (Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "RAPAPORT")) * Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLISHCARAT")));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("ROUGHSIZE") == 0)
                    {
                        e.TotalValue = IntRoughPcs == 0 ? 0 : Math.Round(DouRoughCarat / IntRoughPcs, 2);
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLISHPER") == 0)
                    {
                        e.TotalValue = DouRoughCarat == 0 ? 0 : Math.Round((DouPolishCarat / DouRoughCarat) * 100, 2);
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLISHCARAT") == 0)
                    {
                        e.TotalValue = IntRoughPcs == 0 ? 0 : Math.Round((DouPolishPcs / IntRoughPcs), 2);
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLISHCARATMANUAL") == 0)
                    {
                        e.TotalValue = IntRoughPcs == 0 ? 0 : Math.Round((DouPolishPcsManual / IntRoughPcs), 2);
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PRICEPERCART") == 0)
                    {
                        e.TotalValue = DouPolishCarat == 0 ? 0 : Math.Round((DouPolishAmount / DouPolishCarat), 2);
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("DISCOUNT") == 0)
                    {
                        e.TotalValue = DouRapAmount == 0 ? 0 : Math.Round(((DouRapAmount - DouPolishAmount) / DouRapAmount) * 100, 2);
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLISHAMOUNTROUGHCARAT") == 0)
                    {
                        if (IntRoughPcs != 0 && DouRoughCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouPolishAmount / (DouRoughCarat / IntRoughPcs), 2);
                            DouPolishAmountPerCarat = Val.Val(e.TotalValue);
                        }


                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("IMPDOLLARPRICEPERCARAT") == 0)
                    {
                        e.TotalValue = Math.Round(DouPolishAmountPerCarat * Val.Val(txtImpDollarRate.Text), 2);
                        DouImportPrice = Val.Val(e.TotalValue);
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("EXPDOLLARPRICEPERCARAT") == 0)
                    {
                        e.TotalValue = Val.Val(txtImpDollarRate.Text) == 0 ? 0 : Math.Round(DouImportPrice / Val.Val(txtImpDollarRate.Text), 2);
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnBlankNew_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow DRow = DTabData.NewRow();
                DRow["PACKETNO"] = DTabData.Rows.Count + 1;
                DTabData.Rows.Add(DRow);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }

        private void txtImpDollarRate_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void CmbRapDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != null)
            {
                if (this.ActiveControl.Name.ToUpper() == CmbRapDate.Name.ToUpper())
                {
                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        GrdDet.SetRowCellValue(IntI, "RAPDATE", Val.ToString(CmbRapDate.SelectedItem));
                        GrdDet.FocusedRowHandle = IntI;
                        FindRap();
                    }
                }
            }

        }

        private void txtMainCarat_Validated(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //foreach (DataRow DRow in DTabData.Rows)
            //{
            //    double DouPer = Val.Val(DRow["ROUGHPER"]);
            //    double DouRoughCarat = Math.Round((Val.Val(txtMainCarat.Text) * DouPer) / 100, 2);
            //    DRow["ROUGHCARAT"] = DouRoughCarat;

            //    double DouRoughPcs = Val.Val(DRow["ROUGHPCS"]);
            //    double DouRoughSize = DouRoughPcs == 0 ? 0 : Math.Round(DouRoughCarat / DouRoughPcs, 2);

            //    DRow["ROUGHSIZE"] = DouRoughSize;

            //    double DouPolPer = Val.Val(DRow["POLISHPER"]);
            //    double DouPolishCarat = Math.Round((DouRoughSize * DouPolPer) / 100, 2);

            //    DRow["POLISHCARAT"] = DouPolishCarat;
            //}
            //DTabData.AcceptChanges();

            Calculation();
            //this.Cursor = Cursors.Default;
        }

        private void ChkDefaultEXVGVG_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkDefaultEXVGVG.Checked == true)
            {
                for (int IntI = 0; IntI < DTabData.Rows.Count; IntI++)
                {
                    DataRow DRow = DTabData.Rows[IntI];
                    DRow["CUTNAME"] = "EX3";
                    DRow["CUTCODE"] = "EX3";
                    DRow["CUT_ID"] = 113;

                    DRow["POLNAME"] = "VG";
                    DRow["POLCODE"] = "VG";
                    DRow["POL_ID"] = 346;

                    DRow["SYMNAME"] = "VG";
                    DRow["SYMCODE"] = "VG";
                    DRow["SYM_ID"] = 373;

                    GrdDet.FocusedRowHandle = IntI;
                    FindRap();
                }
            }
        }

        private void lblAddNewRow_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow DRow = DTabData.NewRow();
                DRow["PACKETNO"] = DTabData.Rows.Count + 1;
                DTabData.Rows.Add(DRow);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }

        public void Fetch_SetRadioButton(FlowLayoutPanel pn, int Value)
        {
            foreach (AxonContLib.cRadioButton Cont in pn.Controls)
            {
                if (Val.ToInt(Cont.Tag) == Value)
                {
                    Cont.Checked = true;
                    Cont.ForeColor = mSelectedColor;
                    Cont.BackColor = mSelectedBackColor;
                }
                else
                {
                    Cont.ForeColor = mDeSelectColor;
                    Cont.BackColor = mDSelectedBackColor;
                }
            }
        }
        public void Fetch_SetComboBox(AxonContLib.cComboBox Combo, IList<DataStructure> pData, int Value)
        {
            foreach (DataStructure data in pData)
            {
                if (data.PARA_ID == Value)
                {
                    Combo.SelectedItem = data;
                    break;
                }
            }
        }


        private void GrdDet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow DRow = GrdDet.GetDataRow(e.FocusedRowHandle);

            if (DRow == null)
            {
                return;
            }

            mStrType = "SHOWCLICK";

            Fetch_SetRadioButton(PanelShape, Val.ToInt(DRow["SHAPE_ID"]));
            Fetch_SetRadioButton(PanelColor1, Val.ToInt(DRow["COL_ID1"]));
            Fetch_SetRadioButton(PanelClarity1, Val.ToInt(DRow["CLA_ID1"]));
            Fetch_SetRadioButton(PanelColor2, Val.ToInt(DRow["COL_ID2"]));
            Fetch_SetRadioButton(PanelClarity2, Val.ToInt(DRow["CLA_ID2"]));

            //Fetch_SetRadioButton(PanelCut, Val.ToInt(DRow["CUT_ID"]));
            //Fetch_SetRadioButton(PanelPol, Val.ToInt(DRow["POL_ID"]));
            //Fetch_SetRadioButton(PanelSym, Val.ToInt(DRow["SYM_ID"]));
            //Fetch_SetRadioButton(PanelFL, Val.ToInt(DRow["FL_ID"]));
            //Fetch_SetRadioButton(PanelLab, Val.ToInt(DRow["LAB_ID"]));
            //Fetch_SetRadioButton(PanelLBLC, Val.ToInt(DRow["LBLC_ID"]));

            Fetch_SetComboBox(CmbCut, DtabCut, Val.ToInt(DRow["CUT_ID"]));
            Fetch_SetComboBox(CmbPol, DtabPol, Val.ToInt(DRow["POL_ID"]));
            Fetch_SetComboBox(CmbSym, DtabSym, Val.ToInt(DRow["SYM_ID"]));
            Fetch_SetComboBox(CmbFL, DtabFL, Val.ToInt(DRow["FL_ID"]));
            Fetch_SetComboBox(CmbLab, DtabLab, Val.ToInt(DRow["LAB_ID"]));
            Fetch_SetComboBox(CmbLBLC, DtabLBLC, Val.ToInt(DRow["LBLC_ID"]));

            mStrType = "NOSHOWCLICK";

        }


        private void CmbLBLC_SelectedIndexChanged(object sender, EventArgs e)
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
                    if (combo.AccessibleDescription == "CUT")
                    {
                        GrdDet.SetFocusedRowCellValue("CUT_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("CUTCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("CUTNAME", selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "POLISH")
                    {
                        GrdDet.SetFocusedRowCellValue("POL_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("POLCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("POLNAME", selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "SYMMETRY")
                    {
                        GrdDet.SetFocusedRowCellValue("SYM_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("SYMCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("SYMNAME", selectedDataStructure.PARACODE);
                    }
                    else if (combo.AccessibleDescription == "FLUORESCENCE")
                    {
                        GrdDet.SetFocusedRowCellValue("FL_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("FLCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("FLNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LAB")
                    {
                        GrdDet.SetFocusedRowCellValue("LAB_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("LABCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("LABNAME", selectedDataStructure.PARANAME);
                    }
                    else if (combo.AccessibleDescription == "LBLC")
                    {
                        GrdDet.SetFocusedRowCellValue("LBLC_ID", selectedDataStructure.PARA_ID);
                        GrdDet.SetFocusedRowCellValue("LBLCCODE", selectedDataStructure.PARACODE);
                        GrdDet.SetFocusedRowCellValue("LBLCNAME", selectedDataStructure.PARANAME);
                    }
                }
                FindRap();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }

        private void BtnUpDown_Click(object sender, EventArgs e)
        {
            if (IsDownImage)
            {
                BtnUpDown.Image = AxoneMFGRJ.Properties.Resources.A3;
                PnlControl.Visible = false;
                IsDownImage = false;
            }
            else
            {
                BtnUpDown.Image = AxoneMFGRJ.Properties.Resources.A4;
                PnlControl.Visible = true;
                IsDownImage = true;
            }
        }

        private void txtPasswordForCalcPnl_TextChanged(object sender, EventArgs e)
        {
            if (Val.ToString(txtPasswordForCalcPnl.Tag) != "" && Val.ToString(txtPasswordForCalcPnl.Tag).ToUpper() == txtPasswordForCalcPnl.Text.ToUpper())
            {
                PnlCalculation.Visible = true;
            }
            else
            {
                PnlCalculation.Visible = false;
            }

        }

        private void txtFinalAmount_TextChanged(object sender, EventArgs e)
        {
            double DouProfitPer = Val.Val(txtFinalAvg.Text) == 0 ? 0 : Math.Round(((Val.Val(txtFinalAvg.Text) - Val.Val(txtPurchaseRate.Text)) / Val.Val(txtFinalAvg.Text)) * 100, 2);
            txtProfitPer.Text = Val.ToString(DouProfitPer);

            mStrFinalAmount = Val.Val(txtFinalAmount.Text);
            mStrFinalRate = Val.Val(txtFinalAvg.Text);

        }

        private void txtRoughPerCarat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (mStrType == "SHOWCLICK")
                {
                    return;
                }

                double DouRoughPerCarat = Val.Val(txtRoughPerCarat.Text);

                DataRow[] DrLabour = DTabMajuriRate.Select(DouRoughPerCarat + " >= FromAmount AND " + DouRoughPerCarat + "<= TOAMOUNT");
                if (DrLabour.Length != 0)
                {
                    txtLabourPer.Text = DrLabour[0]["RATE"].ToString();
                }
                else
                {
                    txtLabourPer.Text = "0";
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void txtFinalAvg_Validated(object sender, EventArgs e)
        {
            try
            {
                double DouProfitPer = Val.Val(txtFinalAvg.Text) == 0 ? 0 : Math.Round(((Val.Val(txtFinalAvg.Text) - Val.Val(txtPurchaseRate.Text)) / Val.Val(txtFinalAvg.Text)) * 100, 2);
                txtProfitPer.Text = Val.ToString(DouProfitPer);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

    }
}
