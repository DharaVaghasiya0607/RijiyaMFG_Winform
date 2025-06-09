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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmComparisionViewAdmin : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PredictionView ObjView = new BOTRN_PredictionView();

        DataTable DTabPredictionView = new DataTable();
        DataTable DTabPrdType = new DataTable();
        DataTable DTabPacketDetail = new DataTable();
        DataTable DTabPrdDetail = new DataTable();
        DataTable DTabPrdSummary = new DataTable();
        DataTable DTabPolishOkDetail = new DataTable();
        DataTable DTabDistinct = new DataTable();


        int mIntFromPacketNo = 0;
        int mIntToPacketNo = 0;
        int mIntEmpCode = 0;

        int mIntRefPacketNo = 0;

        string mStrKapan = "";
        string mStrTag = "";
        string mStrParentTag = "";
        Int64 mIntEmployeeID = 0;
        string mStrMainTag = "";
        string mStrPredictionType = "";
        int mIntPacketTag = 0;
        string mStrPredictionTypeOther = "";
        string StrFromDate = null;
        string StrToDate = null;
        string StrOpe = "";
        string StrType = "";

        public FORMTYPE mFormType = FORMTYPE.ADMIN;

        public enum FORMTYPE
        {
            ADMIN = 0,
            MARKER = 1
        }

        #region Property Settings

        public FrmComparisionViewAdmin()
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
            DTabPredictionView.Columns.Add(new DataColumn("ReadyCarat", typeof(double)));
            DTabPredictionView.Columns.Add(new DataColumn("ExpCarat", typeof(double)));
            DTabPredictionView.Columns.Add(new DataColumn("TypeStatus", typeof(string)));
			DTabPredictionView.Columns.Add(new DataColumn("BrekingType", typeof(string))); // add milan

            //Add : Pinali : 26-10-2019
            DTabPredictionView.Columns.Add(new DataColumn("PrevKapanName", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("PrevPacketTag", typeof(string)));
            DTabPredictionView.Columns.Add(new DataColumn("PrevPacketCarat", typeof(double)));
            //End : Pinali : 26-10-2019

            DTabPrdType = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRDTYPE);

            DTabPrdType.Rows.Add(99, "FINLAB", "FINAL LAB", 14);

            DTabPrdType.Rows.Add(999, "FINALCHECKEROWNERSHIP", "FINAL/CHEKER OWNERSHIP", 4);

            //Add: Dhara: 18-06-2020
            DataRow[] DR = null;
            DR = DTabPrdType.Select("PRDTYPE_ID IN(14,15,16,17,18)");
            foreach (DataRow DRow in DR)
            {
                DTabPrdType.Rows.Remove(DRow);
            }
            //End: Dhara: 18-06-2020

            DTabPrdType.DefaultView.Sort = "SEQUENCENO";
            DTabPrdType = DTabPrdType.DefaultView.ToTable();

            CmbPrdType.Properties.DataSource = DTabPrdType;
            CmbPrdType.Properties.DisplayMember = "PRDTYPENAME";
            CmbPrdType.Properties.ValueMember = "PRDTYPE_ID";

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            DataTable DTabPrdTypeOther = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRDTYPE);
            CmbPrdTypeOther.DataSource = DTabPrdTypeOther;
            CmbPrdTypeOther.DisplayMember = "PRDTYPENAME";
            CmbPrdTypeOther.ValueMember = "PRDTYPE_ID";
            DTabPrdTypeOther.Rows.Add(0, "", "", 0);
            DTabPrdTypeOther.DefaultView.Sort = "SEQUENCENO";
            DTabPrdTypeOther = DTabPrdTypeOther.DefaultView.ToTable(true);


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

                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpBeforeDiscount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpBeforePricePerCarat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpAfterAmount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpAfterDiscount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpBefoRepricePerCarat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpBeforeAmount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "TransDate", typeof(DateTime)));

                //#P : 16-01-2021 CMT BY MILAN
              //  DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpBeforeDiscount", typeof(double)));
              //  DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpBeforePricePerCarat", typeof(double)));
              //  DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpAfterAmount", typeof(double)));
              //  DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpAfterDiscount", typeof(double)));
              //  DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpBefoRepricePerCarat", typeof(double)));
              //  DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpBeforeAmount", typeof(double)));
                //End : #P : 16-01-2021

				//Cmt by Milan
                //DTabPredictionView.Columns.Add(new DataColumn(Col + "AdminDiscount", typeof(double)));
                //DTabPredictionView.Columns.Add(new DataColumn(Col + "AdminPricePerCarat", typeof(double)));
                //DTabPredictionView.Columns.Add(new DataColumn(Col + "AdminAmount", typeof(double)));
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
            GrdDet.Columns["TypeStatus"].OwnerBand = gridBand;
			GrdDet.Columns["BrekingType"].OwnerBand = gridBand; // add milan
            GrdDet.Columns["ReadyCarat"].OwnerBand = gridBand;
            GrdDet.Columns["ExpCarat"].OwnerBand = gridBand;

            GrdDet.Columns["KapanName"].Caption = "Kapan";
            GrdDet.Columns["PacketTag"].Caption = "PktNo";
            GrdDet.Columns["LotCarat"].Caption = "OrgWt";
            GrdDet.Columns["BalanceCarat"].Caption = "Bal";
            GrdDet.Columns["TypeStatus"].Caption = "Type";
            GrdDet.Columns["ReadyCarat"].Caption = "RdyCts";
            GrdDet.Columns["ExpCarat"].Caption = "ExpCts";

			GrdDet.Columns["BrekingType"].Visible = false; //add milan
            GrdDet.Columns["BalanceCarat"].Visible = false;

            GrdDet.Columns["KapanName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["PacketTag"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["LotCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["TypeStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["ReadyCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["ExpCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            GrdDet.Columns["PacketTag"].Summary.Add(SummaryItemType.Custom, "PacketTag", "{0:N0}");
            GrdDet.GroupSummary.Add(SummaryItemType.Custom, "PacketTag", GrdDet.Columns["PacketTag"], "{0:N0}");

            GrdDet.Columns["LotCarat"].Summary.Add(SummaryItemType.Sum, "LotCarat", "{0:N3}");
            GrdDet.GroupSummary.Add(SummaryItemType.Sum, "LotCarat", GrdDet.Columns["LotCarat"], "{0:N3}");

            GrdDet.Columns["ReadyCarat"].Summary.Add(SummaryItemType.Sum, "ReadyCarat", "{0:N3}");
            GrdDet.GroupSummary.Add(SummaryItemType.Sum, "ReadyCarat", GrdDet.Columns["ReadyCarat"], "{0:N3}");

            GrdDet.Columns["ExpCarat"].Summary.Add(SummaryItemType.Sum, "ExpCarat", "{0:N3}");
            GrdDet.GroupSummary.Add(SummaryItemType.Sum, "ExpCarat", GrdDet.Columns["ExpCarat"], "{0:N3}");
            
            //Add : Pinali : 26-10-2019
            var gridBandRefDetail = new GridBand();
            gridBandRefDetail.Name = "BandRefDetail";
            gridBandRefDetail.Caption = "Ref Pkts";
            gridBandRefDetail.Tag = "Ref Pkts";
            gridBandRefDetail.RowCount = 1;
            gridBandRefDetail.VisibleIndex = 0;
            gridBandRefDetail.Fixed = FixedStyle.None;
            GrdDet.Bands.Add(gridBandRefDetail);

            GrdDet.Columns["PrevKapanName"].OwnerBand = gridBandRefDetail;
            GrdDet.Columns["PrevPacketTag"].OwnerBand = gridBandRefDetail;
            GrdDet.Columns["PrevPacketCarat"].OwnerBand = gridBandRefDetail;

            GrdDet.Columns["PrevKapanName"].Caption = "Kapan";
            GrdDet.Columns["PrevPacketTag"].Caption = "PktNo";
            GrdDet.Columns["PrevPacketCarat"].Caption = "BalWt";
            GrdDet.Columns["PrevKapanName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["PrevPacketTag"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["PrevPacketCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            GrdDet.Columns["PrevPacketTag"].Summary.Add(SummaryItemType.Custom, "PrevPacketTag", "{0:N0}");
            GrdDet.GroupSummary.Add(SummaryItemType.Custom, "PrevPacketTag", GrdDet.Columns["PrevPacketTag"], "{0:N0}");

            GrdDet.Columns["PrevPacketCarat"].Summary.Add(SummaryItemType.Sum, "PrevPacketCarat", "{0:N3}");
            GrdDet.GroupSummary.Add(SummaryItemType.Sum, "PrevPacketCarat", GrdDet.Columns["PrevPacketCarat"], "{0:N3}");

            GrdDet.Columns["PolishOKEmpCode"].Summary.Add(SummaryItemType.Custom, "PolishOKEmpCode", "{0:N3}");
            GrdDet.GroupSummary.Add(SummaryItemType.Custom, "PolishOKEmpCode", GrdDet.Columns["PolishOKEmpCode"], "{0:N3}");

            //End : Pinali : 26-10-2019

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
                    GrdDet.Columns[Col + "SuratExpBeforeDiscount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "SuratExpBeforePricePerCarat"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "SuratExpBeforeAmount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "SuratExpAfterDiscount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "SuratExpBefoRepricePerCarat"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "SuratExpAfterAmount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "TransDate"].OwnerBand = gridBand;

					//Cmt By Milan
                  //  GrdDet.Columns[Col + "MumbaiExpBeforeDiscount"].OwnerBand = gridBand;
                  //  GrdDet.Columns[Col + "MumbaiExpBeforePricePerCarat"].OwnerBand = gridBand;
                  //  GrdDet.Columns[Col + "MumbaiExpBeforeAmount"].OwnerBand = gridBand;
                  //  GrdDet.Columns[Col + "MumbaiExpAfterDiscount"].OwnerBand = gridBand;
                  //  GrdDet.Columns[Col + "MumbaiExpBefoRepricePerCarat"].OwnerBand = gridBand;
                  //  GrdDet.Columns[Col + "MumbaiExpAfterAmount"].OwnerBand = gridBand;

					//Cmt By Milan
                   // GrdDet.Columns[Col + "AdminDiscount"].OwnerBand = gridBand;
                   // GrdDet.Columns[Col + "AdminPricePerCarat"].OwnerBand = gridBand;
                   // GrdDet.Columns[Col + "AdminAmount"].OwnerBand = gridBand;


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

					//Cmt By Milan 
					//GrdDet.Columns[Col + "AdminDiscount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    //GrdDet.Columns[Col + "AdminPricePerCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    //GrdDet.Columns[Col + "AdminAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    GrdDet.Columns[Col + "Remark"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "EntryDate"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "LabProcess"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "LabSelection"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "TransDate"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    GrdDet.Columns[Col + "EntryDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    GrdDet.Columns[Col + "EntryDate"].DisplayFormat.FormatString = "dd/MM/yy";

                    GrdDet.Columns[Col + "TransDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    GrdDet.Columns[Col + "TransDate"].DisplayFormat.FormatString = "dd/MM/yy";

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

					//Cmt By Milan
                    //GrdDet.Columns[Col + "AdminDiscount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Size, FontStyle.Bold);
                    //GrdDet.Columns[Col + "AdminPricePerCarat"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Size, FontStyle.Bold);
                    //GrdDet.Columns[Col + "AdminAmount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Size, FontStyle.Bold);


                    GrdDet.Columns[Col + "Discount"].AppearanceCell.ForeColor = Color.Purple;
                    GrdDet.Columns[Col + "ColUpAmount"].AppearanceCell.ForeColor = Color.DarkGreen;
                    GrdDet.Columns[Col + "ColDownAmount"].AppearanceCell.ForeColor = Color.FromArgb(192, 0, 0);
                    GrdDet.Columns[Col + "ClaUpAmount"].AppearanceCell.ForeColor = Color.DarkGreen;
                    GrdDet.Columns[Col + "ClaDownAmount"].AppearanceCell.ForeColor = Color.FromArgb(192, 0, 0);

                    GrdDet.Columns[Col + "AmountDiscount"].AppearanceCell.ForeColor = Color.DarkGreen;

                    /*
                    GrdDet.Columns[Col + "Discount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GrdDet.Columns[Col + "Discount"].DisplayFormat.FormatString = "{0:N2}";
                    GrdDet.Columns[Col + "PricePerCarat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GrdDet.Columns[Col + "PricePerCarat"].DisplayFormat.FormatString = "{0:N2}";
                    GrdDet.Columns[Col + "Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GrdDet.Columns[Col + "Amount"].DisplayFormat.FormatString = "{0:N0}";

                    GrdDet.Columns[Col + "AdminDiscount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GrdDet.Columns[Col + "AdminDiscount"].DisplayFormat.FormatString = "{0:N2}";
                    GrdDet.Columns[Col + "AdminPricePerCarat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GrdDet.Columns[Col + "AdminPricePerCarat"].DisplayFormat.FormatString = "{0:N2}";
                    GrdDet.Columns[Col + "AdminAmount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GrdDet.Columns[Col + "AdminAmount"].DisplayFormat.FormatString = "{0:N0}";
                      */

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
                    GrdDet.Columns[Col + "SuratExpBeforeDiscount"].Caption = "Srt Exp Bef Disc";
                    GrdDet.Columns[Col + "SuratExpBeforePricePerCarat"].Caption = "Srt Exp Bef PerCT";
                    GrdDet.Columns[Col + "SuratExpBeforeAmount"].Caption = "Srt Exp Be Amt";
                    GrdDet.Columns[Col + "SuratExpAfterDiscount"].Caption = "Srt Exp Aft Disc";
                    GrdDet.Columns[Col + "SuratExpBefoRepricePerCarat"].Caption = "Srt Exp Aft PerCT";
                    GrdDet.Columns[Col + "SuratExpAfterAmount"].Caption = "Srt Exp Aft Amt";
                    GrdDet.Columns[Col + "TransDate"].Caption = "Trans Date";

					//CMT BY MILAN
                   // GrdDet.Columns[Col + "MumbaiExpBeforeDiscount"].Caption = "MExpBf Disc";
                   // GrdDet.Columns[Col + "MumbaiExpBeforePricePerCarat"].Caption = "MExpBf PerCT";
                   // GrdDet.Columns[Col + "MumbaiExpBeforeAmount"].Caption = "MExpBf Amt";
                   // GrdDet.Columns[Col + "MumbaiExpAfterDiscount"].Caption = "MExpAft Disc";
                   // GrdDet.Columns[Col + "MumbaiExpBefoRepricePerCarat"].Caption = "MExpAft PerCT";
                   // GrdDet.Columns[Col + "MumbaiExpAfterAmount"].Caption = "MExpAft Amt";

					//Cmt By Milan
                    //GrdDet.Columns[Col + "AdminDiscount"].Caption = "Adm Dis %";
                    //GrdDet.Columns[Col + "AdminPricePerCarat"].Caption = "Adm $/Cts";
                    //GrdDet.Columns[Col + "AdminAmount"].Caption = "Adm Amt";

                    if (Val.ToInt(DRow["PRDTYPE_ID"]) == 999)
                    {
                        GrdDet.Columns[Col + "TransDate"].Visible = true;
                    }
                    else
                    {
                        GrdDet.Columns[Col + "TransDate"].Visible = false;
                    }

                    if (Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9)
                    {
                        GrdDet.Columns[Col + "LabProcess"].Visible = true;
                        GrdDet.Columns[Col + "LabSelection"].Visible = true;
                        GrdDet.Columns[Col + "SuratExpBeforeDiscount"].Visible = true;
                        GrdDet.Columns[Col + "SuratExpBeforePricePerCarat"].Visible = true;
                        GrdDet.Columns[Col + "SuratExpBeforeAmount"].Visible = true;
                        GrdDet.Columns[Col + "SuratExpAfterDiscount"].Visible = true;
                        GrdDet.Columns[Col + "SuratExpBefoRepricePerCarat"].Visible = true;
                        GrdDet.Columns[Col + "SuratExpAfterAmount"].Visible = true;

                        if (Val.ToInt(DRow["PRDTYPE_ID"]) == 9)
                        {
							//CMT BY MILAN
                          //  GrdDet.Columns[Col + "MumbaiExpBeforeDiscount"].Visible = true;
                          //  GrdDet.Columns[Col + "MumbaiExpBeforePricePerCarat"].Visible = true;
                          //  GrdDet.Columns[Col + "MumbaiExpBeforeAmount"].Visible = true;
                          //  GrdDet.Columns[Col + "MumbaiExpAfterDiscount"].Visible = true;
                          //  GrdDet.Columns[Col + "MumbaiExpBefoRepricePerCarat"].Visible = true;
                          //  GrdDet.Columns[Col + "MumbaiExpAfterAmount"].Visible = true;
                        }
                        else
                        {
							//CMT BY MILAN
                            //GrdDet.Columns[Col + "MumbaiExpBeforeDiscount"].Visible = false;
                            //GrdDet.Columns[Col + "MumbaiExpBeforePricePerCarat"].Visible = false;
                            //GrdDet.Columns[Col + "MumbaiExpBeforeAmount"].Visible = false;
                            //GrdDet.Columns[Col + "MumbaiExpAfterDiscount"].Visible = false;
                            //GrdDet.Columns[Col + "MumbaiExpBefoRepricePerCarat"].Visible = false;
                            //GrdDet.Columns[Col + "MumbaiExpAfterAmount"].Visible = false;
                        }

                    }
                    else
                    {
                        GrdDet.Columns[Col + "LabProcess"].Visible = false;
                        GrdDet.Columns[Col + "LabSelection"].Visible = false;
                        GrdDet.Columns[Col + "SuratExpBeforeDiscount"].Visible = false;
                        GrdDet.Columns[Col + "SuratExpBeforePricePerCarat"].Visible = false;
                        GrdDet.Columns[Col + "SuratExpBeforeAmount"].Visible = false;
                        GrdDet.Columns[Col + "SuratExpAfterDiscount"].Visible = false;
                        GrdDet.Columns[Col + "SuratExpBefoRepricePerCarat"].Visible = false;
                        GrdDet.Columns[Col + "SuratExpAfterAmount"].Visible = false;

						//CMT BY MILAN
                        //GrdDet.Columns[Col + "MumbaiExpBeforeDiscount"].Visible = false;
                        //GrdDet.Columns[Col + "MumbaiExpBeforePricePerCarat"].Visible = false;
                        //GrdDet.Columns[Col + "MumbaiExpBeforeAmount"].Visible = false;
                        //GrdDet.Columns[Col + "MumbaiExpAfterDiscount"].Visible = false;
                        //GrdDet.Columns[Col + "MumbaiExpBefoRepricePerCarat"].Visible = false;
                        //GrdDet.Columns[Col + "MumbaiExpAfterAmount"].Visible = false;
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
				
					//Cmt by Milan
                //    GrdDet.Columns[Col + "AdminPricePerCarat"].Summary.Add(SummaryItemType.Custom, Col + "AdminPricePerCarat", "{0:N0}");
                //    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "PricePerCarat", GrdDet.Columns[Col + "AdminPricePerCarat"], "{0:N0}");
				//
                //    GrdDet.Columns[Col + "AdminAmount"].Summary.Add(SummaryItemType.Sum, Col + "AdminAmount", "{0:N0}");
                //    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "AdminAmount", GrdDet.Columns[Col + "AdminAmount"], "{0:N0}");

                    GrdDet.Columns[Col + "SuratExpBeforeDiscount"].Summary.Add(SummaryItemType.Sum, Col + "SuratExpBeforeDiscount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "SuratExpBeforeDiscount", GrdDet.Columns[Col + "SuratExpBeforeDiscount"], "{0:N0}");

                    GrdDet.Columns[Col + "SuratExpBeforePricePerCarat"].Summary.Add(SummaryItemType.Sum, Col + "SuratExpBeforePricePerCarat", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "SuratExpBeforePricePerCarat", GrdDet.Columns[Col + "SuratExpBeforePricePerCarat"], "{0:N0}");

                    GrdDet.Columns[Col + "SuratExpBeforeAmount"].Summary.Add(SummaryItemType.Sum, Col + "SuratExpBeforeAmount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "SuratExpBeforeAmount", GrdDet.Columns[Col + "SuratExpBeforeAmount"], "{0:N0}");

                    GrdDet.Columns[Col + "SuratExpAfterDiscount"].Summary.Add(SummaryItemType.Sum, Col + "SuratExpAfterDiscount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "SuratExpAfterDiscount", GrdDet.Columns[Col + "SuratExpAfterDiscount"], "{0:N0}");

                    GrdDet.Columns[Col + "SuratExpBefoRepricePerCarat"].Summary.Add(SummaryItemType.Sum, Col + "SuratExpBefoRepricePerCarat", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "SuratExpBefoRepricePerCarat", GrdDet.Columns[Col + "SuratExpBefoRepricePerCarat"], "{0:N0}");

                    GrdDet.Columns[Col + "SuratExpAfterAmount"].Summary.Add(SummaryItemType.Sum, Col + "SuratExpAfterAmount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "SuratExpAfterAmount", GrdDet.Columns[Col + "SuratExpAfterAmount"], "{0:N0}");


                    //#P : 16-01-2021 CMT BY MILAN
                    //GrdDet.Columns[Col + "MumbaiExpBeforeDiscount"].Summary.Add(SummaryItemType.Sum, Col + "MumbaiExpBeforeDiscount", "{0:N0}");
                    //GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MumbaiExpBeforeDiscount", GrdDet.Columns[Col + "MumbaiExpBeforeDiscount"], "{0:N0}");
					//
                    //GrdDet.Columns[Col + "MumbaiExpBeforePricePerCarat"].Summary.Add(SummaryItemType.Sum, Col + "MumbaiExpBeforePricePerCarat", "{0:N0}");
                    //GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MumbaiExpBeforePricePerCarat", GrdDet.Columns[Col + "MumbaiExpBeforePricePerCarat"], "{0:N0}");
					//
                    //GrdDet.Columns[Col + "MumbaiExpBeforeAmount"].Summary.Add(SummaryItemType.Sum, Col + "MumbaiExpBeforeAmount", "{0:N0}");
                    //GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MumbaiExpBeforeAmount", GrdDet.Columns[Col + "MumbaiExpBeforeAmount"], "{0:N0}");
					//
                    //GrdDet.Columns[Col + "MumbaiExpAfterDiscount"].Summary.Add(SummaryItemType.Sum, Col + "MumbaiExpAfterDiscount", "{0:N0}");
                    //GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MumbaiExpAfterDiscount", GrdDet.Columns[Col + "MumbaiExpAfterDiscount"], "{0:N0}");
					//
                    //GrdDet.Columns[Col + "MumbaiExpBefoRepricePerCarat"].Summary.Add(SummaryItemType.Sum, Col + "MumbaiExpBefoRepricePerCarat", "{0:N0}");
                    //GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MumbaiExpBefoRepricePerCarat", GrdDet.Columns[Col + "MumbaiExpBefoRepricePerCarat"], "{0:N0}");
					//
                    //GrdDet.Columns[Col + "MumbaiExpAfterAmount"].Summary.Add(SummaryItemType.Sum, Col + "MumbaiExpAfterAmount", "{0:N0}");
                    //GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MumbaiExpAfterAmount", GrdDet.Columns[Col + "MumbaiExpAfterAmount"], "{0:N0}");
                    //End : #P : 16-01-2021



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

                        GrdDet.Columns[Col + "ColUpAmount"].Visible = false;
                        GrdDet.Columns[Col + "ColDownAmount"].Visible = false;
                        GrdDet.Columns[Col + "ClaUpAmount"].Visible = false;
                        GrdDet.Columns[Col + "ClaDownAmount"].Visible = false;
                        GrdDet.Columns[Col + "AmountDiscount"].Visible = false;
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
                mStrParentTag = "";
                mIntFromPacketNo = Val.ToInt(txtFromPacketNo.Text);
                mIntToPacketNo = Val.ToInt(txtToPacketNo.Text);
                mStrPredictionType = Val.Trim(CmbPrdType.Properties.GetCheckedItems());
                mIntEmployeeID = Val.ToInt64(txtEmployee.Tag);
                mStrPredictionTypeOther = Val.ToString(CmbPrdTypeOther.SelectedValue);
                mStrTag = txtTag.Text;



                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }


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

                if (ChkWithExtra.Checked == true && ChkWithPCN.Checked == true)
                {
                    StrType = ("EXTRA,PCS,PCN");
                }
                else
                {
                    if (ChkWithExtra.Checked == true)
                    {
                        StrType = ("EXTRA,PCS");
                    }
                    else if (ChkWithPCN.Checked == true)
                    {
                        StrType = ("PCN,PCS");
                    }
                    else
                    {
                        StrType = "PCS";
                    }

                }
                
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                DTabPacketDetail.Rows.Clear();
                DTabPrdDetail.Rows.Clear();
                DTabPrdSummary.Rows.Clear();
                DTabPolishOkDetail.Rows.Clear();
                // DTabDistinct.Rows.Clear();

                BtnSearch.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                // this.Cursor = Cursors.Default;
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
                    // e.Appearance.BackColor = lblUp.BackColor;
                }
            }

            if (e.Column.FieldName == Current + "_Col")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_ColSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_ColSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    // e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    // e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_Cla")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_ClaSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_ClaSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    //  e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    // e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_Cut")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_CutSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_CutSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    //   e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    // e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_Pol")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_PolSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_PolSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    //e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    // e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_Sym")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_SymSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_SymSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    // e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    // e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_FL")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_FLSeqNo"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, Current + "_FLSeqNo"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    // e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    //e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_ExpCarat")
            {
                double DouMakCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_ExpCarat"));
                double DouCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, Current + "_ExpCarat"));
                if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
                {
                    //  e.Appearance.BackColor = lblDown.BackColor;
                }
                else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
                {
                    //e.Appearance.BackColor = lblUp.BackColor;
                }
            }
            if (e.Column.FieldName == Current + "_Amount")
            {
                double DouMakAmount = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, BaseType + "_Amount"));
                double DouAmount = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, Current + "_Amount"));
                if (DouAmount != 0 && DouMakAmount != 0 && DouAmount > DouMakAmount)
                {
                    //e.Appearance.BackColor = lblDown.BackColor;
                }
                else if (DouAmount != 0 && DouMakAmount != 0 && DouAmount < DouMakAmount)
                {
                    //  e.Appearance.BackColor = lblUp.BackColor;
                }
            }

            if (e.Column.FieldName == Current + "_Diff")
            {
                double DouAmount = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, Current + "_Diff"));
                if (DouAmount > 0)
                {
                    //e.Appearance.BackColor = lblDown.BackColor;
                }
                else if (DouAmount < 0)
                {
                    //e.Appearance.BackColor = lblUp.BackColor;
                }
            }
        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BalanceCarat")) == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
            }

            string StrBase = "PRDTYPE_";

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

			//Add Milan (27-03-2021)
			string StrBrekingType = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "BrekingType"));
			if (StrBrekingType == "LS")
			{
				e.Appearance.BackColor = lblls.BackColor;
			}
			else if (StrBrekingType == "BLK" || StrBrekingType == "CONIC")
			{
				e.Appearance.BackColor = lblBlkConic.BackColor;
			}
			else if (StrBrekingType == "MFG" || StrBrekingType == "MFGMISTAKE")
			{
				e.Appearance.BackColor = lblMFG.BackColor;
			}

			//End

        }


        private void RbtAll_CheckedChanged(object sender, EventArgs e)
        {
            BtnSearch_Click(null, null);
        }

        private void GrdDet_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                Int64 IntIsColor = Val.ToInt64(GrdDet.GetRowCellValue(e.RowHandle, "ISCOLOR")); //.ToUpper();

                if (IntIsColor == 1)
                {
                    //e.Graphics.DrawLine(Pens.Red, e.Bounds.Right, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom);
                    e.Graphics.DrawLine(Pens.Red, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top, e.Bounds.Right);
                    e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
                    e.Handled = true;

                    Point p1, p2;
                    p1 = new Point(e.Bounds.Left, e.Bounds.Top + 1);
                    p2 = new Point(e.Bounds.Right, e.Bounds.Top + 1);

                    e.Graphics.DrawLine(Pens.Black, p1, p2);

                    p1 = new Point(e.Bounds.Left, e.Bounds.Top + 3);
                    p2 = new Point(e.Bounds.Right, e.Bounds.Top + 3);
                    e.Graphics.DrawLine(Pens.Black, p1, p2);

                }
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
                mIntRefPacketNo = 0;
                mIntEmpCode = 0;

            }
            else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                if (Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PacketTag")).Contains("A"))
                {
                    mIntToPacketNo = mIntToPacketNo + 1;
                }
                if (!Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PrevPacketTag")).Equals(string.Empty))
                {
                    mIntRefPacketNo = mIntRefPacketNo + 1;
                }
                if (!Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PolishOKEmpCode")).Equals(string.Empty))
                {
                    mIntEmpCode = mIntEmpCode +1;
                }
                //DouFileBack = DouFileBack + (Val.Val
            }

            else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PacketTag") == 0)
                {
                    e.TotalValue = Val.Format(mIntToPacketNo, "######0");
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PrevPacketTag") == 0)
                {
                    e.TotalValue = Val.Format(mIntRefPacketNo, "######0");
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("PolishOKEmpCode") == 0)
                {
                    e.TotalValue = Val.Format(mIntEmpCode, "######0");
                }

            }
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = string.Empty;
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
        private void GrdDetSummary_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;

                DataRow DR = GrdDetSummary.GetDataRow(e.RowHandle);
                if (DR != null && e.Clicks == 2 && Val.ToString(DR["PRDTYPENAME"]).ToUpper().Contains("TOTAL PCN PACKETS"))
                {
                    string StrPCNKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());

                    DataTable DtabPCNPacketsList = new BOFindRap().GetRejectionPacketsWithRoughMakPrd(StrPCNKapan, 0, "");

                    //DataSet DS = ObjView.PredictionPCNPAcketsViewGetData(StrPCNKapan, 0, 0, "", "", "", "3", null, null, "ALL", 0);
                    //DataTable DtabPCNPacketsList = DS.Tables[1].Copy();

                    if (DtabPCNPacketsList.Rows.Count > 0)
                    {
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["AMOUNTDISCOUNT"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["GAMOUNT"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["GAMOUNTDISCOUNT"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["GCUTCODE"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["GPOLCODE"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["GSYMCODE"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["GCARAT"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["GDISCOUNT"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["GRAPAPORT"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["GPRICEPERCARAT"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["MTAG"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["PRICEPERCARAT"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["ROUGHCARAT"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["BALANCECARAT"]);
                        DtabPCNPacketsList.Columns.Remove(DtabPCNPacketsList.Columns["PCNPACKETAMOUNT"]);

                        string StrColumnHide = "ID,PRD_ID,PRDPACKET_ID,PRDTYPE_ID,PRDTYPENAME,PRDTYPECODE,SEQUENCENO,EMPNAME,MANNAME";
                        StrColumnHide = StrColumnHide + "," + "EXPPER,DISCOUNT,RAPAPORT,RAPDATE,LABCODE,LABNAME";
                        StrColumnHide = StrColumnHide + "," + "ISFINAL,PLANNO,MAXAMTFLAG,TFLAG,DIFF,REMARK,LABPROCESS,LABSELECTION,ISMIXRATE,GIANONGIA";

                        FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                        // FrmPopupGrid.DTab = DtData;                   
                        FrmPopupGrid.CountedColumn = "PACKETNO";
                        FrmPopupGrid.SummrisedColumn = "PCNPACKETCARAT,CARAT,AMOUNT";
                        FrmPopupGrid.ColumnsToHide = StrColumnHide;


                        FrmPopupGrid.MainGrid.DataSource = DtabPCNPacketsList;
                        FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                        FrmPopupGrid.Text = "PCN Packets List.";
                        FrmPopupGrid.ISPostBack = true;
                        FrmPopupGrid.LblTitle.Text = "PCN Packets (Rough Makable Info).";
                        //this.Cursor = Cursors.Default;

                        FrmPopupGrid.Width = 1000;
                        FrmPopupGrid.Height = 600;
                        FrmPopupGrid.GrdDet.OptionsBehavior.Editable = false;
                        //FrmPopupGrid.Size = this.Size;

                        FrmPopupGrid.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        FrmPopupGrid.GrdDet.Columns["KAPANNAME"].Caption = "Kapan";
                        FrmPopupGrid.GrdDet.Columns["PACKETNO"].Caption = "PktNo";
                        FrmPopupGrid.GrdDet.Columns["TAG"].Caption = "Tag";
                        FrmPopupGrid.GrdDet.Columns["PCNPACKETCARAT"].Caption = "OrgWt";
                        FrmPopupGrid.GrdDet.Columns["EMPCODE"].Caption = "Code";
                        FrmPopupGrid.GrdDet.Columns["SHPCODE"].Caption = "Shp";
                        FrmPopupGrid.GrdDet.Columns["COLCODE"].Caption = "Col";
                        FrmPopupGrid.GrdDet.Columns["CLACODE"].Caption = "Cla";
                        FrmPopupGrid.GrdDet.Columns["CUTCODE"].Caption = "Cut";
                        FrmPopupGrid.GrdDet.Columns["POLCODE"].Caption = "Pol";
                        FrmPopupGrid.GrdDet.Columns["SYMCODE"].Caption = "Sym";
                        FrmPopupGrid.GrdDet.Columns["FLCODE"].Caption = "FL";
                        FrmPopupGrid.GrdDet.Columns["CARAT"].Caption = "Cts";
                        FrmPopupGrid.GrdDet.Columns["AMOUNT"].Caption = "Amt";

                        FrmPopupGrid.GrdDet.Columns["TAG"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["PACKETNO"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["PCNPACKETCARAT"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["EMPCODE"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["SHPCODE"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["COLCODE"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["CLACODE"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["CUTCODE"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["POLCODE"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["SYMCODE"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["FLCODE"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["CARAT"].Width = 60;
                        FrmPopupGrid.GrdDet.Columns["AMOUNT"].Width = 60;

                        //FrmPopupGrid.GrdDet.Columns["REMARK"].Width = 150;
                        FrmPopupGrid.ShowDialog();
                        FrmPopupGrid.Hide();
                        FrmPopupGrid.Dispose();
                        FrmPopupGrid = null;
                        return;
                    }


                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataSet DS = ObjView.PredictionViewGetDataForAdmin(mStrKapan, mIntFromPacketNo, mIntToPacketNo, mStrTag, mStrParentTag, mStrMainTag, mStrPredictionType, StrFromDate, StrToDate, StrOpe, mIntEmployeeID, mStrPredictionTypeOther, StrType);

                DTabPacketDetail = DS.Tables[0].Copy();
                DTabPrdDetail = DS.Tables[1].Copy();
                DTabPrdSummary = DS.Tables[2].Copy();
                DTabPolishOkDetail = DS.Tables[3].Copy();
            }
            catch (Exception Ex)
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                Global.Message(Ex.Message.ToString());
            }

        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;

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
                    DRNew["TypeStatus"] = Val.ToString(DRow["TypeStatus"]);
					DRNew["BrekingType"] = Val.ToString(DRow["BrekingType"]);
                    DRNew["ExpCarat"] = Val.ToString(DRow["ExpCarat"]);
                    DRNew["ReadyCarat"] = Val.ToString(DRow["ReadyCarat"]);

                    //Add : Pinali : 26-10-2019
                    DRNew["PrevKapanName"] = Val.ToString(DRow["PREVKAPANNAME"]);
                    DRNew["PrevPacketTag"] = Val.ToString(DRow["PREVPACKETTAG"]);
                    DRNew["PrevPacketCarat"] = Val.Val(DRow["PREVPACKETCARAT"]);
                    //End : Pinali : 26-10-2019

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
                                DRNew[Col + "SuratExpBeforeDiscount"] = Val.Val(dddd["SURATEXPBEFOREDISCOUNT"]);
                                DRNew[Col + "SuratExpBeforePricePerCarat"] = Val.Val(dddd["SURATEXPBEFOREPRICEPERCARAT"]);
                                DRNew[Col + "SuratExpBeforeAmount"] = Val.Val(dddd["SURATEXPBEFOREAMOUNT"]);
                                DRNew[Col + "SuratExpAfterDiscount"] = Val.Val(dddd["SURATEXPAFTERDISCOUNT"]);
                                DRNew[Col + "SuratExpBefoRepricePerCarat"] = Val.Val(dddd["SURATEXPAFTERPRICEPERCARAT"]);
                                DRNew[Col + "SuratExpAfterAmount"] = Val.Val(dddd["SURATEXPAFTERAMOUNT"]);
                                DRNew[Col + "TransDate"] = dddd["TRANSDATE"];

								//CMT BY MILAN
                               // DRNew[Col + "MumbaiExpBeforeDiscount"] = Val.Val(dddd["MUMBAIEXPBEFOREDISCOUNT"]);
                               // DRNew[Col + "MumbaiExpBeforePricePerCarat"] = Val.Val(dddd["MUMBAIEXPBEFOREPRICEPERCARAT"]);
                               // DRNew[Col + "MumbaiExpBeforeAmount"] = Val.Val(dddd["MUMBAIEXPBEFOREAMOUNT"]);
                               // DRNew[Col + "MumbaiExpAfterDiscount"] = Val.Val(dddd["MUMBAIEXPAFTERDISCOUNT"]);
                               // DRNew[Col + "MumbaiExpBefoRepricePerCarat"] = Val.Val(dddd["MUMBAIEXPAFTERPRICEPERCARAT"]);
                               // DRNew[Col + "MumbaiExpAfterAmount"] = Val.Val(dddd["MUMBAIEXPAFTERAMOUNT"]);

								//Cmt By Milan
                                //DRNew[Col + "AdminDiscount"] = Val.Val(dddd["ADMINDISCOUNT"]);
                                //DRNew[Col + "AdminPricePerCarat"] = Val.Val(dddd["ADMINPRICEPERCARAT"]);
                                //DRNew[Col + "AdminAmount"] = Val.Val(dddd["ADMINAMOUNT"]);
                                double DoubleAmount = Val.Val(dddd["AMOUNT"]);

                                string StrQueryAmt = "KapanName = '" + StrKapanName + "' And PacketNo = '" + StrPacketNo + "' And Tag = '" + StrTag + "'";
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


                DTabDistinct = DTabPrdDetail.DefaultView.ToTable(true, "PRDTYPE_ID");
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
                            if (band.Name == "BandGeneral" || band.Name == "POLISHOK" || band.Name == "BandRefDetail")
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

            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnSearch.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }
    }
}
