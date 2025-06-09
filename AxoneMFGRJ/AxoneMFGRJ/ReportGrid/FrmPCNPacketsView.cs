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
using BusLib.Master;
using AxoneMFGRJ.Transaction;
using BusLib.ReportGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Reflection;
using DevExpress.Data;
using DevExpress.XtraPrintingLinks;
using System.Drawing.Printing;
using BusLib.Rapaport;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmPCNPacketsView : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PredictionView ObjView = new BOTRN_PredictionView();

        DataTable DTabPredictionView = new DataTable();
        DataTable DTabPrdType = new DataTable();

        int mIntFromPacketNo = 0;
        int mIntToPacketNo = 0;        
        string mStrKapan = "";
        string mStrTag = "";
        string mStrParentTag = "";
        Int64 mIntEmployeeID = 0;
        string mStrMainTag = "";
        string mStrPredictionType = "";
        int mIntPacketTag = 0;

        public FORMTYPE mFormType = FORMTYPE.ADMIN;
        
        public enum FORMTYPE
        {
            ADMIN = 0,
            MARKER = 1
        }

        #region Property Settings

        public FrmPCNPacketsView()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType)
        {
            mFormType = pFormType;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            DTabPredictionView.Columns.Add(new DataColumn("KapanName", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("PacketTag", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("LotCarat", typeof(double)));
            DTabPredictionView.Columns.Add(new DataColumn("BalanceCarat", typeof(double)));


            DTabPrdType = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRDTYPE);
            DTabPrdType.DefaultView.Sort = "SEQUENCENo";
            DTabPrdType = DTabPrdType.DefaultView.ToTable();
            
            CmbPrdType.Properties.DataSource = DTabPrdType;
            CmbPrdType.Properties.DisplayMember = "PRDTYPENAME";
            CmbPrdType.Properties.ValueMember = "PRDTYPE_ID";

            CmbBaseCompare.Items.Clear();
            foreach (DataRow DRow in DTabPrdType.Rows)
            {
                if (Val.ToInt(DRow["PRDTYPE_ID"]) == 4 || Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9)
                {
                    CmbBaseCompare.Items.Add(Val.ToString(DRow["PRDTYPENAME"]));
                }
            }

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            foreach (DataRow DRow in DTabPrdType.Rows)
            {
                string Col = "PRDTYPE_" + Val.ToString(DRow["PRDTYPE_ID"]) + "_";

                DTabPredictionView.Columns.Add(new DataColumn(Col + "Tag", typeof(string))); 
                DTabPredictionView.Columns.Add(new DataColumn(Col + "EmpCode", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "EmpName", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Shp", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Col", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "ColSeqNo", typeof(int)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Cla", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "ClaSeqNo", typeof(int)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Cut", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "CutSeqNo", typeof(int)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Pol", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "PolSeqNo", typeof(int)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Sym", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SymSeqNo", typeof(int)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "FL", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "FLSeqNo", typeof(int)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "RoughCarat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "ExpCarat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "ExpPer", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Rapaport", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Discount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "AmountDiscount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "PricePerCarat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Amount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "ColUpAmount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "ColDownAmount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "ClaUpAmount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "ClaDownAmount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Diff", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Remark", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "EntryDate", typeof(DateTime)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "LabProcess", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "LabSelection", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "GiaNGia", typeof(string)));
            }

            DTabPredictionView.Columns.Add(new DataColumn("PolishOKEmpCode", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("PolishOKIssueCarat", typeof(double)));
            DTabPredictionView.Columns.Add(new DataColumn("PolishOKReadyCarat", typeof(double)));

            GrdDet.BeginUpdate();

            MainGrid.DataSource = DTabPredictionView;
            MainGrid.Refresh();
            GrdDet.PopulateColumns();

            GrdDet.Bands.Clear();

            var gridBand = new GridBand();
            gridBand.Name = "BandGeneral";
            gridBand.Caption = "General";
            gridBand.Tag = "General";
            gridBand.RowCount = 1;
            gridBand.VisibleIndex = 0;
            gridBand.Fixed = FixedStyle.Left;
            GrdDet.Bands.Add(gridBand);

            GrdDet.Columns["KapanName"].OwnerBand = gridBand; 
            GrdDet.Columns["PacketTag"].OwnerBand = gridBand;
            GrdDet.Columns["LotCarat"].OwnerBand = gridBand;
            GrdDet.Columns["BalanceCarat"].OwnerBand = gridBand;

            GrdDet.Columns["KapanName"].Caption = "Kapan"; 
            GrdDet.Columns["PacketTag"].Caption = "PktNo";
            GrdDet.Columns["LotCarat"].Caption = "OrgWt";
            GrdDet.Columns["BalanceCarat"].Caption = "Bal";

            GrdDet.Columns["BalanceCarat"].Visible= false;


            GrdDet.Columns["KapanName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center; 
            GrdDet.Columns["PacketTag"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["LotCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            
            GrdDet.Columns["PacketTag"].Summary.Add(SummaryItemType.Custom, "PacketTag", "{0:N0}");
            GrdDet.GroupSummary.Add(SummaryItemType.Custom, "PacketTag", GrdDet.Columns["PacketTag"], "{0:N0}");

            GrdDet.Columns["LotCarat"].Summary.Add(SummaryItemType.Sum, "LotCarat", "{0:N3}");
            GrdDet.GroupSummary.Add(SummaryItemType.Sum, "LotCarat", GrdDet.Columns["LotCarat"], "{0:N3}");


            //GrdDet.Columns["PacketNo"].Visible = false;
            //GrdDet.Columns["Tag"].Visible = false;

            try
            {
                foreach (DataRow DRow in DTabPrdType.Rows)
                {
                    gridBand = new GridBand();
                    gridBand.Name = "PRDTYPE_" + Val.ToString(DRow["PRDTYPE_ID"]);
                    gridBand.Caption = Val.ToString(DRow["SEQUENCENO"]) + ". " + Val.ToString(DRow["PRDTYPENAME"]);
                    gridBand.RowCount = 1;
                    gridBand.Tag = Val.ToString(DRow["PRDTYPE_ID"]);
                    gridBand.VisibleIndex = Val.ToInt(DRow["SEQUENCENO"]);

                    GrdDet.Bands.Add(gridBand);

                    string Col = "PRDTYPE_" + Val.ToString(DRow["PRDTYPE_ID"]) + "_";

                    GrdDet.Columns[Col + "Tag"].OwnerBand = gridBand; 
                    GrdDet.Columns[Col + "EmpCode"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "EmpName"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Shp"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Col"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "ColSeqNo"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Cla"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "ClaSeqNo"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Cut"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "CutSeqNo"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Pol"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "PolSeqNo"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Sym"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "SymSeqNo"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "FL"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "FLSeqNo"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "LabProcess"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "LabSelection"].OwnerBand = gridBand; 
                    GrdDet.Columns[Col + "RoughCarat"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "ExpCarat"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "ExpPer"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Rapaport"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Discount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "AmountDiscount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "PricePerCarat"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Amount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "ColUpAmount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "ColDownAmount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "ClaUpAmount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "ClaDownAmount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Diff"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Remark"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "EntryDate"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "GiaNGia"].OwnerBand = gridBand;
                    
                    
                    GrdDet.Columns[Col + "Tag"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center; 
                    GrdDet.Columns[Col + "EmpCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "EmpName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Shp"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Col"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "ColSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Cla"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "ClaSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Cut"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "CutSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Pol"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "PolSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Sym"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "SymSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "FL"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "FLSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "RoughCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "ExpCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "ExpPer"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "Rapaport"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "Discount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "AmountDiscount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "PricePerCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "Amount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "Diff"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "GiaNGia"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    
                    GrdDet.Columns[Col + "Remark"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "EntryDate"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "LabProcess"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "LabSelection"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    GrdDet.Columns[Col + "EntryDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    GrdDet.Columns[Col + "EntryDate"].DisplayFormat.FormatString = "dd/MM/yy";

                    GrdDet.Columns[Col + "ExpCarat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GrdDet.Columns[Col + "ExpCarat"].DisplayFormat.FormatString = "{0:N3}";

                    GrdDet.Columns[Col + "Discount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Size, FontStyle.Bold); 
                    GrdDet.Columns[Col + "ExpCarat"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Amount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Amount"].AppearanceCell.Font.Size, FontStyle.Bold);
                    GrdDet.Columns[Col + "Amount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Amount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Amount"].AppearanceCell.Font.Size, FontStyle.Bold);
                    GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.Font.Size, FontStyle.Bold);
                    GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.Font.Size, FontStyle.Bold);
                    GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.Font.Size, FontStyle.Bold);
                    GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.Font.Size, FontStyle.Bold);
                    GrdDet.Columns[Col + "Diff"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Diff"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Diff"].AppearanceCell.Font.Size, FontStyle.Bold);

                    GrdDet.Columns[Col + "Discount"].AppearanceCell.ForeColor = Color.Purple; 
                    GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.ForeColor = Color.DarkGreen;
                    GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.ForeColor = Color.FromArgb(192,0,0);
                    GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.ForeColor = Color.DarkGreen;
                    GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.ForeColor = Color.FromArgb(192, 0, 0);

                    GrdDet.Columns[Col + "Tag"].Caption = "Tg"; 
                    GrdDet.Columns[Col + "EmpCode"].Caption = "Code";
                    GrdDet.Columns[Col + "EmpName"].Caption = "Name";
                    GrdDet.Columns[Col + "Shp"].Caption = "Shp";
                    GrdDet.Columns[Col + "Col"].Caption = "Col";
                    GrdDet.Columns[Col + "ColSeqNo"].Caption = "ColSeq";
                    GrdDet.Columns[Col + "Cla"].Caption = "Cla";
                    GrdDet.Columns[Col + "ClaSeqNo"].Caption = "ClaSeq";
                    GrdDet.Columns[Col + "Cut"].Caption = "Cut";
                    GrdDet.Columns[Col + "CutSeqNo"].Caption = "CutSeqNo";
                    GrdDet.Columns[Col + "Pol"].Caption = "Pol";
                    GrdDet.Columns[Col + "PolSeqNo"].Caption = "PolSeqNo";
                    GrdDet.Columns[Col + "Sym"].Caption = "Sym";
                    GrdDet.Columns[Col + "SymSeqNo"].Caption = "SymSeqNo";
                    GrdDet.Columns[Col + "FL"].Caption = "FL";
                    GrdDet.Columns[Col + "FLSeqNo"].Caption = "FLSeqNo";
                    GrdDet.Columns[Col + "RoughCarat"].Caption = "RouCts";
                    GrdDet.Columns[Col + "ExpCarat"].Caption = "Cts";
                    GrdDet.Columns[Col + "ExpPer"].Caption = "Exp %";
                    GrdDet.Columns[Col + "Rapaport"].Caption = "Rap";
                    GrdDet.Columns[Col + "Discount"].Caption = "Dis %";
                    GrdDet.Columns[Col + "AmountDiscount"].Caption = "Amt %";
                    GrdDet.Columns[Col + "PricePerCarat"].Caption = "$/Cts";
                    GrdDet.Columns[Col + "Amount"].Caption = "Amt";
                    GrdDet.Columns[Col + "ColUpAmount"].Caption = "+Col";
                    GrdDet.Columns[Col + "ColDownAmount"].Caption = "-Col";
                    GrdDet.Columns[Col + "ClaUpAmount"].Caption = "+Cla";
                    GrdDet.Columns[Col + "ClaDownAmount"].Caption = "-Cla";
                    GrdDet.Columns[Col + "Diff"].Caption = "Diff";
                    GrdDet.Columns[Col + "Remark"].Caption = "Remark";
                    GrdDet.Columns[Col + "EntryDate"].Caption = "Date";
                    GrdDet.Columns[Col + "LabProcess"].Caption = "Lab Process";
                    GrdDet.Columns[Col + "LabSelection"].Caption = "Lab Select";
                    GrdDet.Columns[Col + "GiaNGia"].Caption = "Mix/Gia";
                    
                    if (Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9)
                    {
                        GrdDet.Columns[Col + "LabProcess"].Visible = true;
                        GrdDet.Columns[Col + "LabSelection"].Visible = true;

                    }
                    else
                    {
                        GrdDet.Columns[Col + "LabProcess"].Visible = false;
                        GrdDet.Columns[Col + "LabSelection"].Visible = false;
                    }

                    GrdDet.Columns[Col + "RoughCarat"].Summary.Add(SummaryItemType.Sum, Col + "RoughCarat", "{0:N3}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "RoughCarat", GrdDet.Columns[Col + "RoughCarat"], "{0:N3}");

                    GrdDet.Columns[Col + "ExpCarat"].Summary.Add(SummaryItemType.Sum, Col + "ExpCarat", "{0:N3}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ExpCarat", GrdDet.Columns[Col + "ExpCarat"], "{0:N0}");

                    GrdDet.Columns[Col + "AmountDiscount"].Summary.Add(SummaryItemType.Custom, Col + "AmountDiscount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "AmountDiscount", GrdDet.Columns[Col + "AmountDiscount"], "{0:N0}");

                    GrdDet.Columns[Col + "PricePerCarat"].Summary.Add(SummaryItemType.Custom, Col + "PricePerCarat", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "PricePerCarat", GrdDet.Columns[Col + "PricePerCarat"], "{0:N0}");

                    GrdDet.Columns[Col + "Amount"].Summary.Add(SummaryItemType.Sum, Col + "Amount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "Amount", GrdDet.Columns[Col + "Amount"], "{0:N0}");

                    GrdDet.Columns[Col + "ColUpAmount"].Summary.Add(SummaryItemType.Sum, Col + "ColUpAmount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ColUpAmount", GrdDet.Columns[Col + "ColUpAmount"], "{0:N0}");

                    GrdDet.Columns[Col + "ColDownAmount"].Summary.Add(SummaryItemType.Sum, Col + "ColDownAmount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ColDownAmount", GrdDet.Columns[Col + "ColDownAmount"], "{0:N0}");

                    GrdDet.Columns[Col + "ClaUpAmount"].Summary.Add(SummaryItemType.Sum, Col + "ClaUpAmount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ClaUpAmount", GrdDet.Columns[Col + "ClaUpAmount"], "{0:N0}");

                    GrdDet.Columns[Col + "ClaDownAmount"].Summary.Add(SummaryItemType.Sum, Col + "ClaDownAmount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ClaDownAmount", GrdDet.Columns[Col + "ClaDownAmount"], "{0:N0}");

                    GrdDet.Columns[Col + "Diff"].Summary.Add(SummaryItemType.Sum, Col + "Diff", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "Diff", GrdDet.Columns[Col + "Diff"], "{0:N0}");

                    GrdDet.Columns[Col + "EmpName"].Visible = false;
                    GrdDet.Columns[Col + "ColSeqNo"].Visible = false;
                    GrdDet.Columns[Col + "ClaSeqNo"].Visible = false;
                    GrdDet.Columns[Col + "CutSeqNo"].Visible = false;
                    GrdDet.Columns[Col + "PolSeqNo"].Visible = false;
                    GrdDet.Columns[Col + "SymSeqNo"].Visible = false;
                    GrdDet.Columns[Col + "FLSeqNo"].Visible = false;
                    GrdDet.Columns[Col + "RoughCarat"].Visible = false;
                    GrdDet.Columns[Col + "ExpPer"].Visible = false;

                    GrdDet.Columns[Col + "ExpCarat"].Caption = "Cts";
                    GrdDet.Columns[Col + "ExpPer"].Caption = "Exp %";

                    if (mFormType == FORMTYPE.ADMIN)
                    {
                        GrdDet.Columns[Col + "Rapaport"].Visible = true;
                        GrdDet.Columns[Col + "Discount"].Visible = true;
                        GrdDet.Columns[Col + "AmountDiscount"].Visible = false;
                        GrdDet.Columns[Col + "PricePerCarat"].Visible = true;
                    }
                    else
                    {
                        GrdDet.Columns[Col + "Rapaport"].Visible = false;
                        GrdDet.Columns[Col + "Discount"].Visible = false;
                        GrdDet.Columns[Col + "AmountDiscount"].Visible = false;
                        GrdDet.Columns[Col + "PricePerCarat"].Visible = false;


                        //Add : Pinali : 30-04-2019
                        GrdDet.Columns["LotCarat"].Visible = false;

                        GrdDet.Columns[Col + "Tag"].Visible = false;
                        GrdDet.Columns[Col + "ColUpAmount"].Visible = false;
                        GrdDet.Columns[Col + "ColDownAmount"].Visible = false;
                        GrdDet.Columns[Col + "ClaUpAmount"].Visible = false;
                        GrdDet.Columns[Col + "ClaDownAmount"].Visible = false;
                        GrdDet.Columns[Col + "Diff"].Visible = false;
                        GrdDet.Columns[Col + "Remark"].Visible = false;

                        if (Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9)
                        {
                            GrdDet.Columns[Col + "LabProcess"].Visible = true;
                            GrdDet.Columns[Col + "LabSelection"].Visible = true;
                            GrdDet.Columns[Col + "EntryDate"].Visible = true;
                   //         GrdDet.Columns[Col + "GiaNGia"].Visible = false;
                        }
                        else
                        {
                            GrdDet.Columns[Col + "LabProcess"].Visible = false;
                            GrdDet.Columns[Col + "LabSelection"].Visible = false;
                            GrdDet.Columns[Col + "EntryDate"].Visible = false;
                        }
                        //End : Pinali : 30-04-2019
                    }
                }

                if (mFormType == FORMTYPE.ADMIN)
                {
                    gridBand = new GridBand();
                    gridBand.Name = "POLISHOK";
                    gridBand.Caption = "Final Polish Ok";
                    gridBand.RowCount = 1;
                    gridBand.Tag = "POLISHOK";
                    gridBand.VisibleIndex = 999;
                    GrdDet.Bands.Add(gridBand);

                    GrdDet.Columns["PolishOKEmpCode"].OwnerBand = gridBand;
                    GrdDet.Columns["PolishOKIssueCarat"].OwnerBand = gridBand;
                    GrdDet.Columns["PolishOKReadyCarat"].OwnerBand = gridBand;

                    GrdDet.Columns["PolishOKEmpCode"].Caption = "EmpCode";
                    GrdDet.Columns["PolishOKIssueCarat"].Caption = "Issue";
                    GrdDet.Columns["PolishOKReadyCarat"].Caption = "Ready";

                    GrdDet.Columns["PolishOKIssueCarat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GrdDet.Columns["PolishOKReadyCarat"].DisplayFormat.FormatString = "{0:N3}";

                    GrdDet.Columns["PolishOKEmpCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns["PolishOKIssueCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns["PolishOKReadyCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    GrdDet.Columns["PolishOKIssueCarat"].Summary.Add(SummaryItemType.Sum, "PolishOKIssueCarat", "{0:N3}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "PolishOKIssueCarat", GrdDet.Columns["PolishOKIssueCarat"], "{0:N3}");

                    GrdDet.Columns["PolishOKReadyCarat"].Summary.Add(SummaryItemType.Sum, "PolishOKReadyCarat", "{0:N3}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "PolishOKReadyCarat", GrdDet.Columns["PolishOKReadyCarat"], "{0:N3}");
                }

                for (int i = 0; i < GrdDet.Columns.Count; i++)
                {
                    GrdDet.Columns[i].OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
                }

                CmbKapan.Focus();
                CmbBaseCompare.SelectedIndex = 0;

                if (mFormType == FORMTYPE.MARKER)
                {
                    PanelHeader.BackColor = Color.FromArgb(255, 192, 192);
                    CmbPrdType.SetEditValue("2,4,8,9,10,11");
                    CmbPrdType.Enabled = false;
                }


                string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);

                if (Str != "")
                {
                    byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                    MemoryStream stream = new MemoryStream(byteArray);
                    GrdDet.RestoreLayoutFromStream(stream);

                }

                GrdDet.EndUpdate();



            }
            catch (Exception EX)
            {
                GrdDet.EndUpdate();
                Global.MessageError(EX.Message);
            }
        }

        //public void ShowForm() //Add : Pinali : 23-10-2019  : Used On Doble Click from Admin(MNGT) Report 'Total PCN Packets'
        //{
        //    mFormType = FORMTYPE.ADMIN;

        //    Val.FormGeneralSetting(this);
        //    AttachFormDefaultEvent();
        //    this.Show();

        //    PanelHeader.Visible = false;
        //    PanelBottom.Visible = false;

        //    DTabPredictionView.Columns.Add(new DataColumn("KapanName", typeof(string)));
        //    DTabPredictionView.Columns.Add(new DataColumn("PacketTag", typeof(string)));
        //    DTabPredictionView.Columns.Add(new DataColumn("LotCarat", typeof(double)));
        //    DTabPredictionView.Columns.Add(new DataColumn("BalanceCarat", typeof(double)));


        //    DTabPrdType = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRDTYPE);
        //    DTabPrdType.DefaultView.Sort = "SEQUENCENo";
        //    DTabPrdType = DTabPrdType.DefaultView.ToTable();

        //    CmbPrdType.Properties.DataSource = DTabPrdType;
        //    CmbPrdType.Properties.DisplayMember = "PRDTYPENAME";
        //    CmbPrdType.Properties.ValueMember = "PRDTYPE_ID";

        //    CmbBaseCompare.Items.Clear();
        //    foreach (DataRow DRow in DTabPrdType.Rows)
        //    {
        //        if (Val.ToInt(DRow["PRDTYPE_ID"]) == 4 || Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9)
        //        {
        //            CmbBaseCompare.Items.Add(Val.ToString(DRow["PRDTYPENAME"]));
        //        }
        //    }

        //    CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
        //    CmbKapan.Properties.DisplayMember = "KAPANNAME";
        //    CmbKapan.Properties.ValueMember = "KAPANNAME";

        //    foreach (DataRow DRow in DTabPrdType.Rows)
        //    {
        //        string Col = "PRDTYPE_" + Val.ToString(DRow["PRDTYPE_ID"]) + "_";

        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Tag", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "EmpCode", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "EmpName", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Shp", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Col", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "ColSeqNo", typeof(int)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Cla", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "ClaSeqNo", typeof(int)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Cut", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "CutSeqNo", typeof(int)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Pol", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "PolSeqNo", typeof(int)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Sym", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "SymSeqNo", typeof(int)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "FL", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "FLSeqNo", typeof(int)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "RoughCarat", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "ExpCarat", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "ExpPer", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Rapaport", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Discount", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "AmountDiscount", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "PricePerCarat", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Amount", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "ColUpAmount", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "ColDownAmount", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "ClaUpAmount", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "ClaDownAmount", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Diff", typeof(double)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "Remark", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "EntryDate", typeof(DateTime)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "LabProcess", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "LabSelection", typeof(string)));
        //        DTabPredictionView.Columns.Add(new DataColumn(Col + "GiaNGia", typeof(string)));
        //    }

        //    DTabPredictionView.Columns.Add(new DataColumn("PolishOKEmpCode", typeof(string)));
        //    DTabPredictionView.Columns.Add(new DataColumn("PolishOKIssueCarat", typeof(double)));
        //    DTabPredictionView.Columns.Add(new DataColumn("PolishOKReadyCarat", typeof(double)));

        //    GrdDet.BeginUpdate();

        //    MainGrid.DataSource = DTabPredictionView;
        //    MainGrid.Refresh();
        //    GrdDet.PopulateColumns();

        //    GrdDet.Bands.Clear();

        //    var gridBand = new GridBand();
        //    gridBand.Name = "BandGeneral";
        //    gridBand.Caption = "General";
        //    gridBand.Tag = "General";
        //    gridBand.RowCount = 1;
        //    gridBand.VisibleIndex = 0;
        //    gridBand.Fixed = FixedStyle.Left;
        //    GrdDet.Bands.Add(gridBand);

        //    GrdDet.Columns["KapanName"].OwnerBand = gridBand;
        //    GrdDet.Columns["PacketTag"].OwnerBand = gridBand;
        //    GrdDet.Columns["LotCarat"].OwnerBand = gridBand;
        //    GrdDet.Columns["BalanceCarat"].OwnerBand = gridBand;

        //    GrdDet.Columns["KapanName"].Caption = "Kapan";
        //    GrdDet.Columns["PacketTag"].Caption = "PktNo";
        //    GrdDet.Columns["LotCarat"].Caption = "OrgWt";
        //    GrdDet.Columns["BalanceCarat"].Caption = "Bal";

        //    GrdDet.Columns["BalanceCarat"].Visible = false;


        //    GrdDet.Columns["KapanName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //    GrdDet.Columns["PacketTag"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //    GrdDet.Columns["LotCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        //    GrdDet.Columns["PacketTag"].Summary.Add(SummaryItemType.Custom, "PacketTag", "{0:N0}");
        //    GrdDet.GroupSummary.Add(SummaryItemType.Custom, "PacketTag", GrdDet.Columns["PacketTag"], "{0:N0}");

        //    GrdDet.Columns["LotCarat"].Summary.Add(SummaryItemType.Sum, "LotCarat", "{0:N3}");
        //    GrdDet.GroupSummary.Add(SummaryItemType.Sum, "LotCarat", GrdDet.Columns["LotCarat"], "{0:N3}");


        //    //GrdDet.Columns["PacketNo"].Visible = false;
        //    //GrdDet.Columns["Tag"].Visible = false;

        //    try
        //    {
        //        foreach (DataRow DRow in DTabPrdType.Rows)
        //        {
        //            gridBand = new GridBand();
        //            gridBand.Name = "PRDTYPE_" + Val.ToString(DRow["PRDTYPE_ID"]);
        //            gridBand.Caption = Val.ToString(DRow["SEQUENCENO"]) + ". " + Val.ToString(DRow["PRDTYPENAME"]);
        //            gridBand.RowCount = 1;
        //            gridBand.Tag = Val.ToString(DRow["PRDTYPE_ID"]);
        //            gridBand.VisibleIndex = Val.ToInt(DRow["SEQUENCENO"]);

        //            GrdDet.Bands.Add(gridBand);

        //            string Col = "PRDTYPE_" + Val.ToString(DRow["PRDTYPE_ID"]) + "_";

        //            GrdDet.Columns[Col + "Tag"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "EmpCode"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "EmpName"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Shp"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Col"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "ColSeqNo"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Cla"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "ClaSeqNo"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Cut"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "CutSeqNo"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Pol"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "PolSeqNo"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Sym"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "SymSeqNo"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "FL"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "FLSeqNo"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "LabProcess"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "LabSelection"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "RoughCarat"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "ExpCarat"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "ExpPer"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Rapaport"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Discount"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "AmountDiscount"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "PricePerCarat"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Amount"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "ColUpAmount"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "ColDownAmount"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "ClaUpAmount"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "ClaDownAmount"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Diff"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "Remark"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "EntryDate"].OwnerBand = gridBand;
        //            GrdDet.Columns[Col + "GiaNGia"].OwnerBand = gridBand;


        //            GrdDet.Columns[Col + "Tag"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "EmpCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "EmpName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "Shp"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "Col"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "ColSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "Cla"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "ClaSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "Cut"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "CutSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "Pol"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "PolSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "Sym"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "SymSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "FL"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "FLSeqNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "RoughCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "ExpCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "ExpPer"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "Rapaport"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "Discount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "AmountDiscount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "PricePerCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "Amount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "Diff"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            GrdDet.Columns[Col + "GiaNGia"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        //            GrdDet.Columns[Col + "Remark"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "EntryDate"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "LabProcess"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns[Col + "LabSelection"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        //            GrdDet.Columns[Col + "EntryDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
        //            GrdDet.Columns[Col + "EntryDate"].DisplayFormat.FormatString = "dd/MM/yy";

        //            GrdDet.Columns[Col + "ExpCarat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //            GrdDet.Columns[Col + "ExpCarat"].DisplayFormat.FormatString = "{0:N3}";

        //            GrdDet.Columns[Col + "Discount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Size, FontStyle.Bold);
        //            GrdDet.Columns[Col + "ExpCarat"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Amount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Amount"].AppearanceCell.Font.Size, FontStyle.Bold);
        //            GrdDet.Columns[Col + "Amount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Amount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Amount"].AppearanceCell.Font.Size, FontStyle.Bold);
        //            GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.Font.Size, FontStyle.Bold);
        //            GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.Font.Size, FontStyle.Bold);
        //            GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.Font.Size, FontStyle.Bold);
        //            GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.Font.Size, FontStyle.Bold);
        //            GrdDet.Columns[Col + "Diff"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Diff"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Diff"].AppearanceCell.Font.Size, FontStyle.Bold);

        //            GrdDet.Columns[Col + "Discount"].AppearanceCell.ForeColor = Color.Purple;
        //            GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.ForeColor = Color.DarkGreen;
        //            GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.ForeColor = Color.FromArgb(192, 0, 0);
        //            GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.ForeColor = Color.DarkGreen;
        //            GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.ForeColor = Color.FromArgb(192, 0, 0);

        //            GrdDet.Columns[Col + "Tag"].Caption = "Tg";
        //            GrdDet.Columns[Col + "EmpCode"].Caption = "Code";
        //            GrdDet.Columns[Col + "EmpName"].Caption = "Name";
        //            GrdDet.Columns[Col + "Shp"].Caption = "Shp";
        //            GrdDet.Columns[Col + "Col"].Caption = "Col";
        //            GrdDet.Columns[Col + "ColSeqNo"].Caption = "ColSeq";
        //            GrdDet.Columns[Col + "Cla"].Caption = "Cla";
        //            GrdDet.Columns[Col + "ClaSeqNo"].Caption = "ClaSeq";
        //            GrdDet.Columns[Col + "Cut"].Caption = "Cut";
        //            GrdDet.Columns[Col + "CutSeqNo"].Caption = "CutSeqNo";
        //            GrdDet.Columns[Col + "Pol"].Caption = "Pol";
        //            GrdDet.Columns[Col + "PolSeqNo"].Caption = "PolSeqNo";
        //            GrdDet.Columns[Col + "Sym"].Caption = "Sym";
        //            GrdDet.Columns[Col + "SymSeqNo"].Caption = "SymSeqNo";
        //            GrdDet.Columns[Col + "FL"].Caption = "FL";
        //            GrdDet.Columns[Col + "FLSeqNo"].Caption = "FLSeqNo";
        //            GrdDet.Columns[Col + "RoughCarat"].Caption = "RouCts";
        //            GrdDet.Columns[Col + "ExpCarat"].Caption = "Cts";
        //            GrdDet.Columns[Col + "ExpPer"].Caption = "Exp %";
        //            GrdDet.Columns[Col + "Rapaport"].Caption = "Rap";
        //            GrdDet.Columns[Col + "Discount"].Caption = "Dis %";
        //            GrdDet.Columns[Col + "AmountDiscount"].Caption = "Amt %";
        //            GrdDet.Columns[Col + "PricePerCarat"].Caption = "$/Cts";
        //            GrdDet.Columns[Col + "Amount"].Caption = "Amt";
        //            GrdDet.Columns[Col + "ColUpAmount"].Caption = "+Col";
        //            GrdDet.Columns[Col + "ColDownAmount"].Caption = "-Col";
        //            GrdDet.Columns[Col + "ClaUpAmount"].Caption = "+Cla";
        //            GrdDet.Columns[Col + "ClaDownAmount"].Caption = "-Cla";
        //            GrdDet.Columns[Col + "Diff"].Caption = "Diff";
        //            GrdDet.Columns[Col + "Remark"].Caption = "Remark";
        //            GrdDet.Columns[Col + "EntryDate"].Caption = "Date";
        //            GrdDet.Columns[Col + "LabProcess"].Caption = "Lab Process";
        //            GrdDet.Columns[Col + "LabSelection"].Caption = "Lab Select";
        //            GrdDet.Columns[Col + "GiaNGia"].Caption = "Mix/Gia";

        //            if (Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9)
        //            {
        //                GrdDet.Columns[Col + "LabProcess"].Visible = true;
        //                GrdDet.Columns[Col + "LabSelection"].Visible = true;

        //            }
        //            else
        //            {
        //                GrdDet.Columns[Col + "LabProcess"].Visible = false;
        //                GrdDet.Columns[Col + "LabSelection"].Visible = false;
        //            }

        //            GrdDet.Columns[Col + "RoughCarat"].Summary.Add(SummaryItemType.Sum, Col + "RoughCarat", "{0:N3}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "RoughCarat", GrdDet.Columns[Col + "RoughCarat"], "{0:N3}");

        //            GrdDet.Columns[Col + "ExpCarat"].Summary.Add(SummaryItemType.Sum, Col + "ExpCarat", "{0:N3}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ExpCarat", GrdDet.Columns[Col + "ExpCarat"], "{0:N0}");

        //            GrdDet.Columns[Col + "AmountDiscount"].Summary.Add(SummaryItemType.Custom, Col + "AmountDiscount", "{0:N0}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "AmountDiscount", GrdDet.Columns[Col + "AmountDiscount"], "{0:N0}");

        //            GrdDet.Columns[Col + "PricePerCarat"].Summary.Add(SummaryItemType.Custom, Col + "PricePerCarat", "{0:N0}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "PricePerCarat", GrdDet.Columns[Col + "PricePerCarat"], "{0:N0}");

        //            GrdDet.Columns[Col + "Amount"].Summary.Add(SummaryItemType.Sum, Col + "Amount", "{0:N0}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "Amount", GrdDet.Columns[Col + "Amount"], "{0:N0}");

        //            GrdDet.Columns[Col + "ColUpAmount"].Summary.Add(SummaryItemType.Sum, Col + "ColUpAmount", "{0:N0}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ColUpAmount", GrdDet.Columns[Col + "ColUpAmount"], "{0:N0}");

        //            GrdDet.Columns[Col + "ColDownAmount"].Summary.Add(SummaryItemType.Sum, Col + "ColDownAmount", "{0:N0}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ColDownAmount", GrdDet.Columns[Col + "ColDownAmount"], "{0:N0}");

        //            GrdDet.Columns[Col + "ClaUpAmount"].Summary.Add(SummaryItemType.Sum, Col + "ClaUpAmount", "{0:N0}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ClaUpAmount", GrdDet.Columns[Col + "ClaUpAmount"], "{0:N0}");

        //            GrdDet.Columns[Col + "ClaDownAmount"].Summary.Add(SummaryItemType.Sum, Col + "ClaDownAmount", "{0:N0}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ClaDownAmount", GrdDet.Columns[Col + "ClaDownAmount"], "{0:N0}");

        //            GrdDet.Columns[Col + "Diff"].Summary.Add(SummaryItemType.Sum, Col + "Diff", "{0:N0}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "Diff", GrdDet.Columns[Col + "Diff"], "{0:N0}");

        //            GrdDet.Columns[Col + "EmpName"].Visible = false;
        //            GrdDet.Columns[Col + "ColSeqNo"].Visible = false;
        //            GrdDet.Columns[Col + "ClaSeqNo"].Visible = false;
        //            GrdDet.Columns[Col + "CutSeqNo"].Visible = false;
        //            GrdDet.Columns[Col + "PolSeqNo"].Visible = false;
        //            GrdDet.Columns[Col + "SymSeqNo"].Visible = false;
        //            GrdDet.Columns[Col + "FLSeqNo"].Visible = false;
        //            GrdDet.Columns[Col + "RoughCarat"].Visible = false;
        //            GrdDet.Columns[Col + "ExpPer"].Visible = false;

        //            GrdDet.Columns[Col + "ExpCarat"].Caption = "Cts";
        //            GrdDet.Columns[Col + "ExpPer"].Caption = "Exp %";

        //            if (mFormType == FORMTYPE.ADMIN)
        //            {
        //                GrdDet.Columns[Col + "Rapaport"].Visible = true;
        //                GrdDet.Columns[Col + "Discount"].Visible = true;
        //                GrdDet.Columns[Col + "AmountDiscount"].Visible = false;
        //                GrdDet.Columns[Col + "PricePerCarat"].Visible = true;
        //            }
        //            else
        //            {
        //                GrdDet.Columns[Col + "Rapaport"].Visible = false;
        //                GrdDet.Columns[Col + "Discount"].Visible = false;
        //                GrdDet.Columns[Col + "AmountDiscount"].Visible = false;
        //                GrdDet.Columns[Col + "PricePerCarat"].Visible = false;


        //                //Add : Pinali : 30-04-2019
        //                GrdDet.Columns["LotCarat"].Visible = false;

        //                GrdDet.Columns[Col + "Tag"].Visible = false;
        //                GrdDet.Columns[Col + "ColUpAmount"].Visible = false;
        //                GrdDet.Columns[Col + "ColDownAmount"].Visible = false;
        //                GrdDet.Columns[Col + "ClaUpAmount"].Visible = false;
        //                GrdDet.Columns[Col + "ClaDownAmount"].Visible = false;
        //                GrdDet.Columns[Col + "Diff"].Visible = false;
        //                GrdDet.Columns[Col + "Remark"].Visible = false;

        //                if (Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9)
        //                {
        //                    GrdDet.Columns[Col + "LabProcess"].Visible = true;
        //                    GrdDet.Columns[Col + "LabSelection"].Visible = true;
        //                    GrdDet.Columns[Col + "EntryDate"].Visible = true;
        //                    //         GrdDet.Columns[Col + "GiaNGia"].Visible = false;
        //                }
        //                else
        //                {
        //                    GrdDet.Columns[Col + "LabProcess"].Visible = false;
        //                    GrdDet.Columns[Col + "LabSelection"].Visible = false;
        //                    GrdDet.Columns[Col + "EntryDate"].Visible = false;
        //                }
        //                //End : Pinali : 30-04-2019
        //            }
        //        }

        //        if (mFormType == FORMTYPE.ADMIN)
        //        {
        //            gridBand = new GridBand();
        //            gridBand.Name = "POLISHOK";
        //            gridBand.Caption = "Final Polish Ok";
        //            gridBand.RowCount = 1;
        //            gridBand.Tag = "POLISHOK";
        //            gridBand.VisibleIndex = 999;
        //            GrdDet.Bands.Add(gridBand);

        //            GrdDet.Columns["PolishOKEmpCode"].OwnerBand = gridBand;
        //            GrdDet.Columns["PolishOKIssueCarat"].OwnerBand = gridBand;
        //            GrdDet.Columns["PolishOKReadyCarat"].OwnerBand = gridBand;

        //            GrdDet.Columns["PolishOKEmpCode"].Caption = "EmpCode";
        //            GrdDet.Columns["PolishOKIssueCarat"].Caption = "Issue";
        //            GrdDet.Columns["PolishOKReadyCarat"].Caption = "Ready";

        //            GrdDet.Columns["PolishOKIssueCarat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //            GrdDet.Columns["PolishOKReadyCarat"].DisplayFormat.FormatString = "{0:N3}";

        //            GrdDet.Columns["PolishOKEmpCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns["PolishOKIssueCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            GrdDet.Columns["PolishOKReadyCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        //            GrdDet.Columns["PolishOKIssueCarat"].Summary.Add(SummaryItemType.Sum, "PolishOKIssueCarat", "{0:N3}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, "PolishOKIssueCarat", GrdDet.Columns["PolishOKIssueCarat"], "{0:N3}");

        //            GrdDet.Columns["PolishOKReadyCarat"].Summary.Add(SummaryItemType.Sum, "PolishOKReadyCarat", "{0:N3}");
        //            GrdDet.GroupSummary.Add(SummaryItemType.Sum, "PolishOKReadyCarat", GrdDet.Columns["PolishOKReadyCarat"], "{0:N3}");
        //        }

        //        for (int i = 0; i < GrdDet.Columns.Count; i++)
        //        {
        //            GrdDet.Columns[i].OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
        //        }

        //        CmbKapan.Focus();
        //        CmbBaseCompare.SelectedIndex = 0;

        //        if (mFormType == FORMTYPE.MARKER)
        //        {
        //            PanelHeader.BackColor = Color.FromArgb(255, 192, 192);
        //            CmbPrdType.SetEditValue("2,4,8,9,10,11");
        //            CmbPrdType.Enabled = false;
        //        }


        //        string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);

        //        if (Str != "")
        //        {
        //            byte[] byteArray = Encoding.ASCII.GetBytes(Str);
        //            MemoryStream stream = new MemoryStream(byteArray);
        //            GrdDet.RestoreLayoutFromStream(stream);

        //        }

        //        GrdDet.EndUpdate();



        //    }
        //    catch (Exception EX)
        //    {
        //        GrdDet.EndUpdate();
        //        Global.MessageError(EX.Message);
        //    }
        //}

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjView);
            ObjFormEvent.ObjToDisposeList.Add(Val);

        }

        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                mStrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());
                if (mStrKapan.Length == 0)
                {
                    lblMessage.Text = "Kapan Name Is Required";
                    CmbKapan.Focus();
                    return;
                }
                if (txtEmployee.Text.Trim().Length == 0)
                {
                    txtEmployee.Tag = "";
                }

                mStrMainTag = "";
                mStrTag = txtTag.Text;
                mStrParentTag = "";
                mIntFromPacketNo = Val.ToInt(txtFromPacketNo.Text);
                mIntToPacketNo = Val.ToInt(txtToPacketNo.Text);
                mStrPredictionType = Val.Trim(CmbPrdType.Properties.GetCheckedItems());
                mIntEmployeeID = Val.ToInt64(txtEmployee.Tag);

                string StrFromDate = null;
                string StrToDate = null;

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                string StrOpe = "";
                if (RbtAll.Checked == true)
                {
                    StrOpe = "ALL";
                }
                else if (RbtPktCreated.Checked == true)
                {
                    StrOpe = "CREATED";
                }
                else if (RbtPktNotCreated.Checked == true)
                {
                    StrOpe = "NOTCREATED";
                }

                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;

                DataSet DS = ObjView.PredictionPCNPAcketsViewGetData(mStrKapan, mIntFromPacketNo, mIntToPacketNo, mStrTag, mStrParentTag, mStrMainTag, mStrPredictionType, StrFromDate, StrToDate, StrOpe, mIntEmployeeID);

                DataTable DTabPacketDetail = DS.Tables[0].Copy();
                DataTable DTabPrdDetail = DS.Tables[1].Copy();
                DataTable DTabPrdSummary = DS.Tables[2].Copy();
                DataTable DTabPolishOkDetail = DS.Tables[3].Copy();

                GrdDet.BeginUpdate();

                MainGridSummary.DataSource = DTabPrdSummary;
                MainGridSummary.Refresh();
                GrdDetSummary.BestFitColumns();


                DTabPredictionView.Rows.Clear();
                foreach (DataRow DRow in DTabPacketDetail.Rows)
                {
                    DataRow DRNew = DTabPredictionView.NewRow();

                    DRNew["KapanName"] = Val.ToString(DRow["KapanName"]);
                    DRNew["PacketTag"] = Val.ToString(DRow["PacketTag"]);
                    DRNew["LotCarat"] = Val.ToString(DRow["LotCarat"]);
                    DRNew["BalanceCarat"] = Val.ToString(DRow["BalanceCarat"]);

                    string StrQueryPol = "KapanName = '" + Val.ToString(DRow["KapanName"]) + "' And PacketNo = '" + Val.ToString(DRow["PacketNo"]) + "' And Tag = '" + Val.ToString(DRow["Tag"]) + "'";
                    DataRow[] UDROWPOL = DTabPolishOkDetail.Select(StrQueryPol);
                    if (UDROWPOL.Length != 0)
                    {
                        DRNew["PolishOKEmpCode"] = Val.ToString(UDROWPOL[0]["WORKERCODE"]);
                        DRNew["PolishOKIssueCarat"] = Val.Val(UDROWPOL[0]["ISSUECARAT"]);
                        DRNew["PolishOKReadyCarat"] = Val.Val(UDROWPOL[0]["READYCARAT"]);
                    }

                    foreach (DataRow DRowPrd in DTabPrdType.Rows)
                    {
                        string Col = "PRDTYPE_" + Val.ToString(DRowPrd["PRDTYPE_ID"]) + "_";

                        string StrKapanName = Val.ToString(DRow["KapanName"]); ;
                        string StrPacketNo = Val.ToString(DRow["PacketNo"]);
                        string StrTag = Val.ToString(DRow["Tag"]);
                        string StrPrdTypeID = Val.ToString(DRowPrd["PrdType_ID"]);

                        string StrQuery = "KapanName = '" + StrKapanName + "' And PacketNo = '" + StrPacketNo + "' And Tag = '" + StrTag + "' And PrdType_ID = '" + StrPrdTypeID + "'";

                        DataRow[] UDROW = DTabPrdDetail.Select(StrQuery);

                        if (UDROW.Length > 1) // Add : Pinali : 02-10/2019 : Coz Display Only One Records between TFlag & MaxAmtFlag
                        {
                            StrQuery = "KapanName = '" + StrKapanName + "' And PacketNo = '" + StrPacketNo + "' And Tag = '" + StrTag + "' And PrdType_ID = '" + StrPrdTypeID + "' AND TFLAG = 1";
                            UDROW = DTabPrdDetail.Select(StrQuery);
                        }

                        if (UDROW != null && UDROW.Length == 1)
                        {
                            foreach (DataRow dddd in UDROW)
                            {
                                DRNew[Col + "Tag"] = Val.ToString(dddd["TAG"]);
                                DRNew[Col + "EmpCode"] = Val.ToString(dddd["EMPCODE"]);
                                DRNew[Col + "EmpName"] = Val.ToString(dddd["EMPNAME"]);
                                DRNew[Col + "Shp"] = Val.ToString(dddd["SHPCODE"]);
                                DRNew[Col + "Col"] = Val.ToString(dddd["COLCODE"]);
                                DRNew[Col + "ColSeqNo"] = Val.ToInt(dddd["COLSEQNO"]);
                                DRNew[Col + "Cla"] = Val.ToString(dddd["CLACODE"]);
                                DRNew[Col + "ClaSeqNo"] = Val.ToInt(dddd["CLASEQNO"]);
                                DRNew[Col + "Cut"] = Val.ToString(dddd["CUTCODE"]);
                                DRNew[Col + "CutSeqNo"] = Val.ToInt(dddd["CUTSEQNO"]);
                                DRNew[Col + "Pol"] = Val.ToString(dddd["POLCODE"]);
                                DRNew[Col + "PolSeqNo"] = Val.ToInt(dddd["POLSEQNO"]);
                                DRNew[Col + "Sym"] = Val.ToString(dddd["SYMCODE"]);
                                DRNew[Col + "SymSeqNo"] = Val.ToInt(dddd["SYMSEQNO"]);
                                DRNew[Col + "FL"] = Val.ToString(dddd["FLCODE"]);
                                DRNew[Col + "FLSeqNo"] = Val.ToInt(dddd["FLSEQNO"]);
                                DRNew[Col + "RoughCarat"] = Val.Val(dddd["BALANCECARAT"]);
                                DRNew[Col + "ExpCarat"] = Val.Val(dddd["CARAT"]);
                                DRNew[Col + "ExpPer"] = Val.Val(dddd["EXPPER"]);
                                DRNew[Col + "Rapaport"] = Val.Val(dddd["RAPAPORT"]);
                                DRNew[Col + "Discount"] = Val.Val(dddd["DISCOUNT"]);
                                DRNew[Col + "AmountDiscount"] = Val.Val(dddd["AMOUNTDISCOUNT"]);
                                DRNew[Col + "PricePerCarat"] = Val.Val(dddd["PRICEPERCARAT"]);
                                DRNew[Col + "Amount"] = Val.Val(dddd["AMOUNT"]);
                                DRNew[Col + "ColUpAmount"] = Val.Val(dddd["UPCOLORAMOUNT"]);
                                DRNew[Col + "ColDownAmount"] = Val.Val(dddd["DOWNCOLORAMOUNT"]);
                                DRNew[Col + "ClaUpAmount"] = Val.Val(dddd["UPCLARITYAMOUNT"]);
                                DRNew[Col + "ClaDownAmount"] = Val.Val(dddd["DOWNCLARITYAMOUNT"]);
                                DRNew[Col + "Remark"] = Val.ToString(dddd["REMARK"]);
                                DRNew[Col + "EntryDate"] = dddd["ENTRYDATE"];
                                DRNew[Col + "LabProcess"] = dddd["LABPROCESS"];
                                DRNew[Col + "LabSelection"] = dddd["LABSELECTION"];
                                DRNew[Col + "GiaNGia"] = dddd["GIANONGIA"];

                                double DoubleAmount = Val.Val(dddd["AMOUNT"]);

                                string StrQueryAmt = "KapanName = '" + StrKapanName + "' And PacketNo = '" + StrPacketNo + "' And Tag = '" + StrTag + "' And PrdType_ID = '" + Val.ToInt(CmbBaseCompare.Tag) + "'";
                                DataRow[] UDROWAmt = DTabPrdDetail.Select(StrQueryAmt);
                                if (UDROWAmt != null && UDROWAmt.Length == 1)
                                {
                                    double DouBaseAmount = Val.Val(UDROWAmt[0]["AMOUNT"]);
                                    DRNew[Col + "Diff"] = Math.Round(DoubleAmount - DouBaseAmount);
                                }
                                else
                                {
                                    DRNew[Col + "Diff"] = 0.00;
                                }

                                UDROWAmt = null;
                            }
                        }
                        UDROW = null;
                    }
                    DTabPredictionView.Rows.Add(DRNew);
                }


                DataTable DTabDistinct = DTabPrdDetail.DefaultView.ToTable(true, "PRDTYPE_ID");
                //string output = string.Empty;
                //for (int i = 0; i < DTabDistinct.Rows.Count; i++)
                //{
                //    output = output + DTabDistinct.Rows[i]["PRDTYPE_ID"].ToString();
                //    output += (i < DTabDistinct.Rows.Count) ? "," : string.Empty;
                //}

                //string[] StrPrdName = Val.Trim(CmbPrdType.Properties.GetCheckedItems()).Split(',');

                if (DTabDistinct.Rows.Count != 0)
                {
                    foreach (GridBand band in GrdDet.Bands)
                    {
                        foreach (DataRow DRPrdID in DTabDistinct.Rows)
                        {
                            if (band.Name == "BandGeneral" || band.Name == "POLISHOK")
                            {
                                continue;
                            }
                            if (Val.ToString(DRPrdID["PRDTYPE_ID"]) != "")
                            {
                                if (band.Tag.ToString() == Val.ToString(DRPrdID["PRDTYPE_ID"]))
                                {
                                    band.Visible = true;
                                    break;
                                }
                                else
                                {
                                    band.Visible = false;
                                }
                            }

                        }
                    }

                }

                DTabDistinct.Dispose();
                DTabDistinct = null;

                GrdDet.EndUpdate();

                GrdDet.RefreshData();
                GrdDet.BestFitColumns();

                this.Cursor = Cursors.Default;
                

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
                return;
            }
          
        }

        

        #region Background Worker
        

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[] 
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }

        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "Prediction";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    //if (tabControl1.SelectedIndex == 0)
                    {
                        PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                        {
                            PrintingSystemBase = new PrintingSystemBase(),
                            Component = MainGrid,
                            Landscape = true,
                            PaperKind = PaperKind.A4,
                            Margins = new System.Drawing.Printing.Margins(20, 20, 200, 20)
                        };

                        link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                        link.ExportToXls(svDialog.FileName);

                        if (Global.Confirm("Do You Want To Open [Prediction.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                        }
                    }
                    //else
                    //{
                    //    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    //    {
                    //        PrintingSystemBase = new PrintingSystemBase(),
                    //        Component = PivotGridData,
                    //        Landscape = true,
                    //        PaperKind = PaperKind.A4,
                    //        Margins = new System.Drawing.Printing.Margins(20, 20, 200, 20)
                    //    };

                    //    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    //    link.ExportToXls(svDialog.FileName);

                    //    if (Global.Confirm("Do You Want To Open [Prediction.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    //    {
                    //        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    //    }
                    //}

                    
                }
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }


        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("Prediction View", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("Kapan Name :- " + Val.Trim(CmbKapan.Properties.GetCheckedItems()), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            GrdDet.BestFitColumns();
        }

        private void BtnExpandAll_Click(object sender, EventArgs e)
        {
            GrdDet.ExpandAllGroups();
        }

        private void BtnCollepsAll_Click(object sender, EventArgs e)
        {
            GrdDet.CollapseAllGroups();
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


        public void SetRowCellColor(RowCellStyleEventArgs e, string BaseType, string Current)
        {
            if (e.Column.FieldName == Current + "_Shp")
            {
                string StrMakShape = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_Shp"));
                string StrShape = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, Current + "_Shp"));
                if (StrMakShape != "" && StrShape != "" && StrMakShape != StrShape)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
            }

            if (e.Column.FieldName == Current + "_Col")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_ColSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_ColSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor; 
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_Cla")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_ClaSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_ClaSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor; 
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_Cut")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_CutSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_CutSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor; 
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_Pol")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_PolSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_PolSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor; 
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_Sym")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_SymSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_SymSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor; 
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_FL")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_FLSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_FLSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor; 
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_ExpCarat")
            {
                double DouMakCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_ExpCarat"));
                double DouCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, Current + "_ExpCarat"));
                if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
                {
                    e.Appearance.BackColor = lblDown.BackColor; 
                }
                else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_Amount")
            {
                double DouMakAmount = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_Amount"));
                double DouAmount = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, Current + "_Amount"));
                if (DouAmount != 0 && DouMakAmount != 0 && DouAmount > DouMakAmount)
                {
                    e.Appearance.BackColor = lblDown.BackColor; 
                }
                else if (DouAmount != 0 && DouMakAmount != 0 && DouAmount < DouMakAmount)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
            }

            if (e.Column.FieldName == Current + "_Diff")
            {
                double DouAmount = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, Current + "_Diff"));
                if (DouAmount > 0)
                {
                    e.Appearance.BackColor = lblDown.BackColor; 
                }
                else if (DouAmount < 0)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
            }
        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle  < 0)
            {
                return;
            }

            if (Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BalanceCarat")) == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 224, 192);                
            }

            string StrBase = "PRDTYPE_" + Val.ToString(CmbBaseCompare.Tag);

            // Set CHIEF ARTIST PREDICTION With Makable Diff

            SetRowCellColor(e, StrBase, "PRDTYPE_4");

            SetRowCellColor(e, StrBase, "PRDTYPE_5");

            // Set ARTIST PREDICTION With Makable Diff
            SetRowCellColor(e, StrBase, "PRDTYPE_6");

            // Set POLISH CHECKER PREDICTION With Makable Diff
            SetRowCellColor(e, StrBase, "PRDTYPE_7");

            // Set GRADING With Makable Diff
            SetRowCellColor(e, StrBase, "PRDTYPE_8");



            // Set Rough With Grading Diff
            SetRowCellColor(e, StrBase, "PRDTYPE_1");

            // Set Final With Grading Diff
            SetRowCellColor(e, StrBase, "PRDTYPE_2");

            // Set Checker Final With Grading Diff
            SetRowCellColor(e, StrBase, "PRDTYPE_10");

            // Set MAKABLE CHECKER PREDICTION Final With Grading Diff
            SetRowCellColor(e, StrBase, "PRDTYPE_3");

            // Set lab grading PREDICTION Final With Grading Diff
            SetRowCellColor(e, StrBase, "PRDTYPE_11");

        }

        private void CmbBaseCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string Name = Val.ToString(CmbBaseCompare.SelectedItem);
                DataRow[] D = DTabPrdType.Select("PRDTYPENAME ='" + Name + "' ");

                if (D.Length != 0)
                {
                    DataRow DRow = D[0];
                    CmbBaseCompare.Tag = Val.ToString(DRow["PrdType_ID"]);

                 
                    foreach (GridBand band in GrdDet.Bands)
                    {
                        band.AppearanceHeader.ForeColor = Color.Black;
                    }

                    if (GrdDet.Bands.Count > 1)
                    {
                        GrdDet.Bands["PRDTYPE_" + Val.ToString(DRow["PrdType_ID"])].AppearanceHeader.ForeColor = Color.FromArgb(192, 0, 0);
                    }
                    

                    DRow = null;
                    GrdDet.Focus();

                    BtnSearch_Click(null, null);

                }  
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }
        }

        private void RbtAll_CheckedChanged(object sender, EventArgs e)
        {
            BtnSearch_Click(null, null);
        }

        private void GrdDet_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle < 0)
            //    {
            //        return;
            //    }

            //    Int64 IntIsColor = Val.ToInt64(GrdDet.GetRowCellValue(e.RowHandle, "ISCOLOR")); //.ToUpper();

            //    if (IntIsColor == 1)
            //    {
            //        //e.Graphics.DrawLine(Pens.Red, e.Bounds.Right, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom);
            //        e.Graphics.DrawLine(Pens.Red, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top, e.Bounds.Right);
            //        e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
            //        e.Handled = true;

            //        Point p1, p2;
            //        p1 = new Point(e.Bounds.Left, e.Bounds.Top + 1);
            //        p2 = new Point(e.Bounds.Right, e.Bounds.Top + 1);

            //        e.Graphics.DrawLine(Pens.Black, p1, p2);

            //        p1 = new Point(e.Bounds.Left, e.Bounds.Top + 3);
            //        p2 = new Point(e.Bounds.Right, e.Bounds.Top + 3);
            //        e.Graphics.DrawLine(Pens.Black, p1, p2);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message);
            //}
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
                Global.Message(ex.Message);
            }
        }

        private void GrdDet_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                mIntToPacketNo = 0;

            }
            else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                if (Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PacketTag")).Contains("A"))
                {
                    mIntToPacketNo = mIntToPacketNo + 1;
                }
                

                //DouFileBack = DouFileBack + (Val.Val
            }

            else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PacketTag") == 0)
                {
                    e.TotalValue = Val.Format(mIntToPacketNo, "######0");                    
                }
               
            }

        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void txtYear_Validated(object sender, EventArgs e)
        {
            if (Val.Val(txtYear.Text) == 0)
            {
                DTPFromDate.Checked = false;
                DTPToDate.Checked = false;
                return;
            }
            else if (txtYear.Text.Length != 6)
            {
                DTPFromDate.Checked = false;
                DTPToDate.Checked = false;
                Global.MessageError("Invalid Form Of Year And Month");
                return;
            }
            int IntYear = Val.ToInt(Val.Left(txtYear.Text, 4));
            int IntMonth = Val.ToInt(Val.Right(txtYear.Text, 2));
            if (IntMonth > 12)
            {
                DTPFromDate.Checked = false;
                DTPToDate.Checked = false;
                
                Global.MessageError("Month Number > 12 Does Not Exists");
                return;
            }

            DateTime startDate = new DateTime(IntYear, IntMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            DTPFromDate.Checked = true;
            DTPToDate.Checked = true;
            DTPFromDate.Value = startDate;
            DTPToDate.Value = endDate;

        }

        private void lblSaveLayout_Click(object sender, EventArgs e)
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

        private void lblDefaultLayout_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDet.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }  
        }

    }
}
