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
    public partial class FrmPridictionViewForGrading : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PredictionView ObjView = new BOTRN_PredictionView();

        DataTable DTabPredictionView = new DataTable();
        DataTable DTabPrdType = new DataTable();

        DataSet DS = new DataSet();

        DataTable DTabPacketDetail = new DataTable();
        DataTable DTabPrdDetail = new DataTable();
        DataTable DTabPrdSummary = new DataTable();
        DataTable DTabPolishOkDetail = new DataTable();

        int mIntFromPacketNo = 0;
        int mIntToPacketNo = 0;
        string mStrKapan = "";
        string mStrTag = "";
        string mStrParentTag = "";
        Int64 mIntEmployeeID = 0;
        string mStrMainTag = "";
        string mStrPredictionType = "";
        int mIntPacketTag = 0;

        string StrFromDate = null;
        string StrToDate = null;

        string StrOpe = "";

        string StrType = string.Empty;

        public FORMTYPE mFormType = FORMTYPE.ADMIN;

        public enum FORMTYPE
        {
            ADMIN = 0,
            MARKER = 1
        }

        #region Property Settings

        public FrmPridictionViewForGrading()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
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


            //Add: Dhara: 18-06-2020
            DataRow[] DR = null;
            DR = DTabPrdType.Select("PRDTYPE_ID IN(1,2,3,4,5,6,7,10,11,12,13,14,15,17,18,19)");
            foreach (DataRow DRow in DR)
            {
                DTabPrdType.Rows.Remove(DRow);
            }
            //End: Dhara: 18-06-2020

            CmbPrdType.Properties.DataSource = DTabPrdType;
            CmbPrdType.Properties.DisplayMember = "PRDTYPENAME";
            CmbPrdType.Properties.ValueMember = "PRDTYPE_ID";

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
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Carat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Col", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Cla", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Cut", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Pol", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Sym", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "FL", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "GrdStatus", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "NattsName", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "LblcName", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "LabSelection", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Diamin", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Diamax", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Height", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Heliumtotaldepth", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Heliumtablepc", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Heliumratio", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "LabProcess", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "ColorShadeCode", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "BlackIncCode", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "WhiteIncCode", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "OpenIncCode", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "PavCode", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "MilkyCode", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "LusterName", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "EyeCleanCode", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "HaName", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Rapaport", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Discount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "PricePerCarat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "ClaSeqNo", typeof(int)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "Amount", typeof(double)));
				
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpBeforeDiscount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpBeforePricePerCarat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpAfterAmount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpLabCharge", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpAfterDiscount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpBefoRepricePerCarat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "SuratExpBeforeAmount", typeof(double)));

                DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpBeforeDiscount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpBeforePricePerCarat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpAfterAmount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpLabCharge", typeof(string)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpAfterDiscount", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpBefoRepricePerCarat", typeof(double)));
                DTabPredictionView.Columns.Add(new DataColumn(Col + "MumbaiExpBeforeAmount", typeof(double)));

				//ADD MILAN 13-03-2021:

				DTabPredictionView.Columns.Add(new DataColumn(Col + "MkavDiscount", typeof(double)));
				DTabPredictionView.Columns.Add(new DataColumn(Col + "MkavPricePerCarat", typeof(double)));
				DTabPredictionView.Columns.Add(new DataColumn(Col + "MkavAmount", typeof(double)));
				DTabPredictionView.Columns.Add(new DataColumn(Col + "ExpDiscount", typeof(double)));
				DTabPredictionView.Columns.Add(new DataColumn(Col + "ExpPricePerCarat", typeof(double)));
				DTabPredictionView.Columns.Add(new DataColumn(Col + "ExpAmount", typeof(double)));

				//END MILAN

                DTabPredictionView.Columns.Add(new DataColumn(Col + "Diff", typeof(double)));
            }

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

            GrdDet.Columns["KapanName"].Caption = "Kapan";
            GrdDet.Columns["PacketTag"].Caption = "PktNo";

            GrdDet.Columns["KapanName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GrdDet.Columns["PacketTag"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            GrdDet.Columns["PacketTag"].Summary.Add(SummaryItemType.Custom, "PacketTag", "{0:N0}");
            GrdDet.GroupSummary.Add(SummaryItemType.Custom, "PacketTag", GrdDet.Columns["PacketTag"], "{0:N0}");

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
                    GrdDet.Columns[Col + "Shp"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Carat"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Col"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Cla"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Cut"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Pol"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Sym"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "FL"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "GrdStatus"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "NattsName"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "LblcName"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "LabSelection"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Diamin"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Diamax"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Height"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Heliumtotaldepth"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Heliumtablepc"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Heliumratio"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "LabProcess"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "ColorShadeCode"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "BlackIncCode"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "WhiteIncCode"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "OpenIncCode"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "PavCode"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "MilkyCode"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "LusterName"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "EyeCleanCode"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "HaName"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Rapaport"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Discount"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "PricePerCarat"].OwnerBand = gridBand;
                    GrdDet.Columns[Col + "Amount"].OwnerBand = gridBand;

					//Add Milan(13-03-2021)
					if (Val.ToInt(DRow["PRDTYPE_ID"]) != 16)
					{
						GrdDet.Columns[Col + "SuratExpBeforeDiscount"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "SuratExpBeforePricePerCarat"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "SuratExpBeforeAmount"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "SuratExpLabCharge"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "SuratExpAfterDiscount"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "SuratExpBefoRepricePerCarat"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "SuratExpAfterAmount"].OwnerBand = gridBand;

						GrdDet.Columns[Col + "MumbaiExpBeforeDiscount"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "MumbaiExpBeforePricePerCarat"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "MumbaiExpBeforeAmount"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "MumbaiExpLabCharge"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "MumbaiExpAfterDiscount"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "MumbaiExpBefoRepricePerCarat"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "MumbaiExpAfterAmount"].OwnerBand = gridBand;
					}
					else
					{
						GrdDet.Columns[Col + "MkavDiscount"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "MkavPricePerCarat"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "MkavAmount"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "ExpDiscount"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "ExpPricePerCarat"].OwnerBand = gridBand;
						GrdDet.Columns[Col + "ExpAmount"].OwnerBand = gridBand;
					}
					//End


                    GrdDet.Columns[Col + "Tag"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "EmpCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Shp"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Carat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Col"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Cla"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Cut"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Pol"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Sym"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "FL"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "GrdStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "NattsName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "LblcName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "LabSelection"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Diamin"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Diamax"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Height"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Heliumtotaldepth"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Heliumtablepc"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Heliumratio"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "LabProcess"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "ColorShadeCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "BlackIncCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "WhiteIncCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "OpenIncCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "PavCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "MilkyCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "LusterName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "EyeCleanCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    GrdDet.Columns[Col + "HaName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "Rapaport"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "PricePerCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    GrdDet.Columns[Col + "Amount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GrdDet.Columns[Col + "SuratExpBeforeDiscount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    GrdDet.Columns[Col + "SuratExpBeforePricePerCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "SuratExpLabCharge"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;


                    GrdDet.Columns[Col + "MumbaiExpBeforeDiscount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "MumbaiExpBeforePricePerCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GrdDet.Columns[Col + "MumbaiExpLabCharge"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

					//Add Milan
					GrdDet.Columns[Col + "MkavDiscount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
					GrdDet.Columns[Col + "MkavPricePerCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
					GrdDet.Columns[Col + "MkavAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

					GrdDet.Columns[Col + "ExpDiscount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
					GrdDet.Columns[Col + "ExpPricePerCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
					GrdDet.Columns[Col + "ExpAmount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
					//End Milan


                    GrdDet.Columns[Col + "Discount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Discount"].AppearanceCell.Font.Size, FontStyle.Bold);
                    GrdDet.Columns[Col + "Amount"].AppearanceCell.Font = new Font(GrdDet.Columns[Col + "Amount"].AppearanceCell.Font.Name, GrdDet.Columns[Col + "Amount"].AppearanceCell.Font.Size, FontStyle.Bold);

                    GrdDet.Columns[Col + "Discount"].AppearanceCell.ForeColor = Color.Purple;

                    GrdDet.Columns[Col + "Tag"].Caption = "Tg";
                    GrdDet.Columns[Col + "EmpCode"].Caption = "Code";
                    GrdDet.Columns[Col + "Shp"].Caption = "Shp";
                    GrdDet.Columns[Col + "Carat"].Caption = "Carat";
                    GrdDet.Columns[Col + "Col"].Caption = "Col";
                    GrdDet.Columns[Col + "Cla"].Caption = "Cla";
                    GrdDet.Columns[Col + "Cut"].Caption = "Cut";
                    GrdDet.Columns[Col + "Pol"].Caption = "Pol";
                    GrdDet.Columns[Col + "Sym"].Caption = "Sym";
                    GrdDet.Columns[Col + "FL"].Caption = "FL";
                    GrdDet.Columns[Col + "GrdStatus"].Caption = "Grd Status";
                    GrdDet.Columns[Col + "NattsName"].Caption = "Natts";
                    GrdDet.Columns[Col + "LblcName"].Caption = "Lblc";
                    GrdDet.Columns[Col + "LabSelection"].Caption = "Lab Selection";
                    GrdDet.Columns[Col + "LabProcess"].Caption = "Lab Process";
                    GrdDet.Columns[Col + "Diamin"].Caption = "Dia Min";
                    GrdDet.Columns[Col + "Diamax"].Caption = "Ddia Max";
                    GrdDet.Columns[Col + "Height"].Caption = "Height";
                    GrdDet.Columns[Col + "Heliumtablepc"].Caption = "Table Pc";
                    GrdDet.Columns[Col + "Heliumratio"].Caption = "Ratio";
                    GrdDet.Columns[Col + "Heliumtotaldepth"].Caption = "Total Depth";
                    GrdDet.Columns[Col + "ColorShadeCode"].Caption = "Col Shade";
                    GrdDet.Columns[Col + "BlackIncCode"].Caption = "Binc";
                    GrdDet.Columns[Col + "WhiteIncCode"].Caption = "Winc";
                    GrdDet.Columns[Col + "OpenIncCode"].Caption = "Oinc";
                    GrdDet.Columns[Col + "PavCode"].Caption = "Pav";
                    GrdDet.Columns[Col + "MilkyCode"].Caption = "Milky";
                    GrdDet.Columns[Col + "LusterName"].Caption = "Luster";
                    GrdDet.Columns[Col + "EyeCleanCode"].Caption = "EyeClean";
                    GrdDet.Columns[Col + "HaName"].Caption = "HA";
                    GrdDet.Columns[Col + "Rapaport"].Caption = "Rap";
                    GrdDet.Columns[Col + "Discount"].Caption = "Dis";
                    GrdDet.Columns[Col + "PricePerCarat"].Caption = "PricePerCts";
                    GrdDet.Columns[Col + "Amount"].Caption = "Amount";
                    GrdDet.Columns[Col + "SuratExpBeforeDiscount"].Caption = "Srt Exp Bef Disc";
                    GrdDet.Columns[Col + "SuratExpBeforePricePerCarat"].Caption = "Srt Exp Bef PerCT";
                    GrdDet.Columns[Col + "SuratExpBeforeAmount"].Caption = "Srt Exp Be Amt";
                    GrdDet.Columns[Col + "SuratExpLabCharge"].Caption = "Lab Charge";
                    GrdDet.Columns[Col + "SuratExpAfterDiscount"].Caption = "Srt Exp Aft Disc";
                    GrdDet.Columns[Col + "SuratExpBefoRepricePerCarat"].Caption = "Srt Exp Aft PerCT";
                    GrdDet.Columns[Col + "SuratExpAfterAmount"].Caption = "Srt Exp Aft Amt";

                    GrdDet.Columns[Col + "MumbaiExpBeforeDiscount"].Caption = "Mum Exp Bef Dics";
                    GrdDet.Columns[Col + "MumbaiExpBeforePricePerCarat"].Caption = "Mum Exp Bef PerCT";
                    GrdDet.Columns[Col + "MumbaiExpBeforeAmount"].Caption = "Mum Exp Bef Amt";
                    GrdDet.Columns[Col + "MumbaiExpLabCharge"].Caption = "Mum Exp LabCharge";
                    GrdDet.Columns[Col + "MumbaiExpAfterDiscount"].Caption = "Mum Exp Aft Dics";
                    GrdDet.Columns[Col + "MumbaiExpBefoRepricePerCarat"].Caption = "Mum Exp Aft PerCT";
                    GrdDet.Columns[Col + "MumbaiExpAfterAmount"].Caption = "Mum Exp Aft Amt";

					//ADD MILAN(15-03-2021)
					GrdDet.Columns[Col + "MkavDiscount"].Caption = "Mkav Disc";
					GrdDet.Columns[Col + "MkavPricePerCarat"].Caption = "Mkav $/Cts";
					GrdDet.Columns[Col + "MkavAmount"].Caption = "Mkav Amt";

					GrdDet.Columns[Col + "ExpDiscount"].Caption = "Exp Disc";
					GrdDet.Columns[Col + "ExpPricePerCarat"].Caption = "Exp $/Cts";
					GrdDet.Columns[Col + "ExpAmount"].Caption = "Exp Amt";
					//END

					if (Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9 || Val.ToInt(DRow["PRDTYPE_ID"]) == 16) //add milan(13-03-2021) only 18
                    {
                        GrdDet.Columns[Col + "LabProcess"].Visible = true;
                        GrdDet.Columns[Col + "LabSelection"].Visible = true;

                    }
                    else
                    {
                        GrdDet.Columns[Col + "LabProcess"].Visible = false;
                        GrdDet.Columns[Col + "LabSelection"].Visible = false;
                    }

                    GrdDet.Columns[Col + "Discount"].Summary.Add(SummaryItemType.Sum, Col + "Discount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "Discount", GrdDet.Columns[Col + "Discount"], "{0:N0}");

                    GrdDet.Columns[Col + "Amount"].Summary.Add(SummaryItemType.Sum, Col + "Amount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "Amount", GrdDet.Columns[Col + "Amount"], "{0:N0}");

                    GrdDet.Columns[Col + "PricePerCarat"].Summary.Add(SummaryItemType.Sum, Col + "PricePerCarat", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "PricePerCarat", GrdDet.Columns[Col + "PricePerCarat"], "{0:N0}");

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

                    GrdDet.Columns[Col + "SuratExpLabCharge"].Summary.Add(SummaryItemType.Sum, Col + "SuratExpLabCharge", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "SuratExpLabCharge", GrdDet.Columns[Col + "SuratExpLabCharge"], "{0:N0}");


                    //#P : 16-01-2021

                    GrdDet.Columns[Col + "MumbaiExpBeforeAmount"].Summary.Add(SummaryItemType.Sum, Col + "MumbaiExpBeforeAmount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MumbaiExpBeforeAmount", GrdDet.Columns[Col + "MumbaiExpBeforeAmount"], "{0:N0}");

                    GrdDet.Columns[Col + "MumbaiExpAfterAmount"].Summary.Add(SummaryItemType.Sum, Col + "MumbaiExpAfterAmount", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MumbaiExpAfterAmount", GrdDet.Columns[Col + "MumbaiExpAfterAmount"], "{0:N0}");

                    GrdDet.Columns[Col + "MumbaiExpLabCharge"].Summary.Add(SummaryItemType.Sum, Col + "MumbaiExpLabCharge", "{0:N0}");
                    GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MumbaiExpLabCharge", GrdDet.Columns[Col + "MumbaiExpLabCharge"], "{0:N0}");
                    //End : #P : 16-01-2021

					//Add Milan(13-03-2021)

					if (Val.ToInt(DRow["PRDTYPE_ID"]) == 16)
					{
						//GrdDet.Columns[Col + "MkavPricePerCarat"].Summary.Add(SummaryItemType.Sum, Col + "MkavPricePerCarat", "{0:N0}");
						//GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MkavPricePerCarat", GrdDet.Columns[Col + "MkvPricePerCarat"], "{0:N0}");

						GrdDet.Columns[Col + "MkavAmount"].Summary.Add(SummaryItemType.Sum, Col + "MkavAmount", "{0:N0}");
						GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "MkvAmount", GrdDet.Columns[Col + "MkavAmount"], "{0:N0}");

						//GrdDet.Columns[Col + "ExpPricePerCarat"].Summary.Add(SummaryItemType.Sum, Col + "ExpPricePerCarat", "{0:N0}");
						//GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ExpPricePerCarat", GrdDet.Columns[Col + "ExpPricePerCarat"], "{0:N0}");

						GrdDet.Columns[Col + "ExpAmount"].Summary.Add(SummaryItemType.Sum, Col + "ExpAmount", "{0:N0}");
						GrdDet.GroupSummary.Add(SummaryItemType.Sum, Col + "ExpAmount", GrdDet.Columns[Col + "ExpAmount"], "{0:N0}");
						//End 
					}



                    if (mFormType == FORMTYPE.ADMIN)
                    {
                        GrdDet.Columns[Col + "Rapaport"].Visible = true;
                        GrdDet.Columns[Col + "Discount"].Visible = true;
                        GrdDet.Columns[Col + "PricePerCarat"].Visible = true;
                    }
                    else
                    {
                        GrdDet.Columns[Col + "Rapaport"].Visible = false;
                        GrdDet.Columns[Col + "Discount"].Visible = false;
                        GrdDet.Columns[Col + "PricePerCarat"].Visible = false;


                        //Add : Pinali : 30-04-2019

						if (Val.ToInt(DRow["PRDTYPE_ID"]) == 8 || Val.ToInt(DRow["PRDTYPE_ID"]) == 9 || Val.ToInt(DRow["PRDTYPE_ID"]) == 16) //Add 18 Milan
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
                    //gridBand = new GridBand();
                    //gridBand.Name = "POLISHOK";
                    //gridBand.Caption = "Final Polish Ok";
                    //gridBand.RowCount = 1;
                    //gridBand.Tag = "POLISHOK";
                    //gridBand.VisibleIndex = 999;
                    //GrdDet.Bands.Add(gridBand);

                    //GrdDet.Columns["PolishOKEmpCode"].OwnerBand = gridBand;
                    //GrdDet.Columns["PolishOKIssueCarat"].OwnerBand = gridBand;
                    //GrdDet.Columns["PolishOKReadyCarat"].OwnerBand = gridBand;

                    //GrdDet.Columns["PolishOKEmpCode"].Caption = "EmpCode";
                    //GrdDet.Columns["PolishOKIssueCarat"].Caption = "Issue";
                    //GrdDet.Columns["PolishOKReadyCarat"].Caption = "Ready";

                    //GrdDet.Columns["PolishOKIssueCarat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    //GrdDet.Columns["PolishOKReadyCarat"].DisplayFormat.FormatString = "{0:N3}";

                    //GrdDet.Columns["PolishOKEmpCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //GrdDet.Columns["PolishOKIssueCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //GrdDet.Columns["PolishOKReadyCarat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    //GrdDet.Columns["PolishOKIssueCarat"].Summary.Add(SummaryItemType.Sum, "PolishOKIssueCarat", "{0:N3}");
                    //GrdDet.GroupSummary.Add(SummaryItemType.Sum, "PolishOKIssueCarat", GrdDet.Columns["PolishOKIssueCarat"], "{0:N3}");

                    //GrdDet.Columns["PolishOKReadyCarat"].Summary.Add(SummaryItemType.Sum, "PolishOKReadyCarat", "{0:N3}");
                    //GrdDet.GroupSummary.Add(SummaryItemType.Sum, "PolishOKReadyCarat", GrdDet.Columns["PolishOKReadyCarat"], "{0:N3}");
                }

                for (int i = 0; i < GrdDet.Columns.Count; i++)
                {
                    GrdDet.Columns[i].OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
                }

                CmbKapan.Focus();
                if (mFormType == FORMTYPE.MARKER)
                {
                    //PanelHeader.BackColor = Color.FromArgb(255, 192, 192);
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
                if (mStrKapan.Length == 0 && Val.ToString(txtStoneNo.Text) == "")
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

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                DTabPacketDetail.Rows.Clear();
                DTabPrdDetail.Rows.Clear();
                DTabPrdSummary.Rows.Clear();
                DTabPolishOkDetail.Rows.Clear();

                BtnSearch.Enabled = false;
                PanleProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

                //DataSet DS = ObjView.PredictionViewForGradingGetData(mStrKapan, mIntFromPacketNo, mIntToPacketNo, mStrTag, mStrParentTag, mStrMainTag, mStrPredictionType, StrFromDate, StrToDate, StrOpe, mIntEmployeeID, Val.ToString(txtStoneNo.Text));

                //DataTable DTabPacketDetail = DS.Tables[0].Copy();
                //DataTable DTabPrdDetail = DS.Tables[1].Copy();
                //DataTable DTabPrdSummary = DS.Tables[2].Copy();
                //DataTable DTabPolishOkDetail = DS.Tables[3].Copy();

                //GrdDet.BeginUpdate();

                //DTabPredictionView.Rows.Clear();
                //foreach (DataRow DRow in DTabPacketDetail.Rows)
                //{
                //    DataRow DRNew = DTabPredictionView.NewRow();

                //    DRNew["KapanName"] = Val.ToString(DRow["KapanName"]);
                //    DRNew["PacketTag"] = Val.ToString(DRow["PacketTag"]);
                //    // DRNew["LotCarat"] = Val.ToString(DRow["LotCarat"]);
                //    // DRNew["BalanceCarat"] = Val.ToString(DRow["BalanceCarat"]);

                //    string StrQueryPol = "KapanName = '" + Val.ToString(DRow["KapanName"]) + "' And PacketNo = '" + Val.ToString(DRow["PacketNo"]) + "' And Tag = '" + Val.ToString(DRow["Tag"]) + "'";
                //    DataRow[] UDROWPOL = DTabPolishOkDetail.Select(StrQueryPol);
                //    //if (UDROWPOL.Length != 0)
                //    //{
                //    //    DRNew["PolishOKEmpCode"] = Val.ToString(UDROWPOL[0]["WORKERCODE"]);
                //    //    DRNew["PolishOKIssueCarat"] = Val.Val(UDROWPOL[0]["ISSUECARAT"]);
                //    //    DRNew["PolishOKReadyCarat"] = Val.Val(UDROWPOL[0]["READYCARAT"]);
                //    //}

                //    foreach (DataRow DRowPrd in DTabPrdType.Rows)
                //    {
                //        string Col = "PRDTYPE_" + Val.ToString(DRowPrd["PRDTYPE_ID"]) + "_";

                //        string StrKapanName = Val.ToString(DRow["KapanName"]); ;
                //        string StrPacketNo = Val.ToString(DRow["PacketNo"]);
                //        string StrTag = Val.ToString(DRow["Tag"]);
                //        string StrPrdTypeID = Val.ToString(DRowPrd["PrdType_ID"]);

                //        string StrQuery = "KapanName = '" + StrKapanName + "' And PacketNo = '" + StrPacketNo + "' And Tag = '" + StrTag + "' And PrdType_ID = '" + StrPrdTypeID + "'";

                //        DataRow[] UDROW = DTabPrdDetail.Select(StrQuery);

                //        if (UDROW.Length > 1) // Add : Pinali : 02-10/2019 : Coz Display Only One Records between TFlag & MaxAmtFlag
                //        {
                //            StrQuery = "KapanName = '" + StrKapanName + "' And PacketNo = '" + StrPacketNo + "' And Tag = '" + StrTag + "' And PrdType_ID = '" + StrPrdTypeID + "' AND TFLAG = 1";
                //            UDROW = DTabPrdDetail.Select(StrQuery);
                //        }

                //        if (UDROW != null && UDROW.Length == 1)
                //        {
                //            foreach (DataRow dddd in UDROW)
                //            {
                //                DRNew[Col + "Tag"] = Val.ToString(dddd["TAG"]);
                //                DRNew[Col + "EmpCode"] = Val.ToString(dddd["EMPCODE"]);
                //                DRNew[Col + "Shp"] = Val.ToString(dddd["SHPCODE"]);
                //                DRNew[Col + "Col"] = Val.ToString(dddd["COLCODE"]);
                //                DRNew[Col + "Cla"] = Val.ToString(dddd["CLACODE"]);
                //                DRNew[Col + "Cut"] = Val.ToString(dddd["CUTCODE"]);
                //                DRNew[Col + "Pol"] = Val.ToString(dddd["POLCODE"]);
                //                DRNew[Col + "Sym"] = Val.ToString(dddd["SYMCODE"]);
                //                DRNew[Col + "FL"] = Val.ToString(dddd["FLNAME"]);
                //                DRNew[Col + "ColorShadeCode"] = Val.ToString(dddd["COLORSHADECODE"]);
                //                DRNew[Col + "MilkyCode"] = Val.ToString(dddd["MILKYCODE"]);
                //                DRNew[Col + "LblcName"] = Val.ToString(dddd["LBLCNAME"]);
                //                DRNew[Col + "NattsName"] = Val.ToString(dddd["NATTSNAME"]);
                //                DRNew[Col + "ColorShadeCode"] = Val.ToString(dddd["COLORSHADECODE"]);
                //                DRNew[Col + "Carat"] = Val.Val(dddd["CARAT"]);
                //                DRNew[Col + "Rapaport"] = Val.Val(dddd["RAPAPORT"]);
                //                DRNew[Col + "Discount"] = Val.Val(dddd["DISCOUNT"]);
                //                DRNew[Col + "PricePerCarat"] = Val.Val(dddd["PRICEPERCARAT"]);
                //                DRNew[Col + "Amount"] = Val.Val(dddd["AMOUNT"]);
                //                DRNew[Col + "LabProcess"] = Val.ToString(dddd["LABPROCESS"]);
                //                DRNew[Col + "LabSelection"] = Val.ToString(dddd["LABSELECTION"]);
                //                DRNew[Col + "BlackIncCode"] = Val.ToString(dddd["BLACKINCCODE"]);
                //                DRNew[Col + "WhiteIncCode"] = Val.ToString(dddd["WHITEINCCODE"]);
                //                DRNew[Col + "OpenIncCode"] = Val.ToString(dddd["OPENINCCODE"]);
                //                DRNew[Col + "PavCode"] = Val.ToString(dddd["PAVCODE"]);
                //                DRNew[Col + "MilkyCode"] = Val.ToString(dddd["MILKYCODE"]);
                //                DRNew[Col + "LusterName"] = Val.ToString(dddd["LUSTERNAME"]);
                //                DRNew[Col + "EyeCleanCode"] = Val.ToString(dddd["EYECLEANCODE"]);
                //                DRNew[Col + "HaName"] = Val.ToString(dddd["HANAME"]);
                //                DRNew[Col + "SuratExpBeforeDiscount"] = Val.Val(dddd["SURATEXPBEFOREDISCOUNT"]);
                //                DRNew[Col + "SuratExpBeforePricePerCarat"] = Val.Val(dddd["SURATEXPBEFOREPRICEPERCARAT"]);
                //                DRNew[Col + "SuratExpBeforeAmount"] = Val.Val(dddd["SURATEXPBEFOREAMOUNT"]);
                //                DRNew[Col + "SuratExpLabCharge"] = Val.Val(dddd["SURATEXPLABCHARGE"]);
                //                DRNew[Col + "SuratExpAfterDiscount"] = Val.Val(dddd["SURATEXPAFTERDISCOUNT"]);
                //                DRNew[Col + "SuratExpBefoRepricePerCarat"] = Val.Val(dddd["SURATEXPAFTERPRICEPERCARAT"]);
                //                DRNew[Col + "SuratExpAfterAmount"] = Val.Val(dddd["SURATEXPAFTERAMOUNT"]);
                //                DRNew[Col + "GrdStatus"] = Val.ToString(dddd["GRDSTATUS"]);
                //                DRNew[Col + "Diamin"] = Val.Val(dddd["DIAMIN"]);
                //                DRNew[Col + "Diamax"] = Val.Val(dddd["DIAMAX"]);
                //                DRNew[Col + "Height"] = Val.Val(dddd["Height"]);
                //                DRNew[Col + "Heliumtablepc"] = Val.Val(dddd["HELIUMTABLEPC"]);
                //                DRNew[Col + "Heliumratio"] = Val.Val(dddd["HELIUMRATIO"]);
                //                DRNew[Col + "Heliumtotaldepth"] = Val.Val(dddd["HELIUMTOTALDEPTH"]);


                //                double DoubleAmount = Val.Val(dddd["AMOUNT"]);

                //                string StrQueryAmt = "KapanName = '" + StrKapanName + "' And PacketNo = '" + StrPacketNo + "' And Tag = '" + StrTag + "'";
                //                DataRow[] UDROWAmt = DTabPrdDetail.Select(StrQueryAmt);
                //                if (UDROWAmt != null && UDROWAmt.Length == 1)
                //                {
                //                    double DouBaseAmount = Val.Val(UDROWAmt[0]["AMOUNT"]);
                //                    DRNew[Col + "Diff"] = Math.Round(DoubleAmount - DouBaseAmount);
                //                }
                //                else
                //                {
                //                    DRNew[Col + "Diff"] = 0.00;
                //                }

                //                UDROWAmt = null;
                //            }
                //        }
                //        UDROW = null;
                //    }
                //    DTabPredictionView.Rows.Add(DRNew);
                //}


                //DataTable DTabDistinct = DTabPrdDetail.DefaultView.ToTable(true, "PRDTYPE_ID");
                ////string output = string.Empty;
                ////for (int i = 0; i < DTabDistinct.Rows.Count; i++)
                ////{
                ////    output = output + DTabDistinct.Rows[i]["PRDTYPE_ID"].ToString();
                ////    output += (i < DTabDistinct.Rows.Count) ? "," : string.Empty;
                ////}

                ////string[] StrPrdName = Val.Trim(CmbPrdType.Properties.GetCheckedItems()).Split(',');

                //if (DTabDistinct.Rows.Count != 0)
                //{
                //    foreach (GridBand band in GrdDet.Bands)
                //    {
                //        foreach (DataRow DRPrdID in DTabDistinct.Rows)
                //        {
                //            if (band.Name == "BandGeneral" || band.Name == "POLISHOK")
                //            {
                //                continue;
                //            }
                //            if (Val.ToString(DRPrdID["PRDTYPE_ID"]) != "")
                //            {
                //                if (band.Tag.ToString() == Val.ToString(DRPrdID["PRDTYPE_ID"]))
                //                {
                //                    band.Visible = true;
                //                    break;
                //                }
                //                else
                //                {
                //                    band.Visible = false;
                //                }
                //            }

                //        }
                //    }

                //}

                //DTabDistinct.Dispose();
                //DTabDistinct = null;

                //GrdDet.EndUpdate();

                //GrdDet.RefreshData();
                //GrdDet.BestFitColumns();

                //this.Cursor = Cursors.Default;


            }
            catch (Exception EX)
            {
                PanleProgress.Visible = false;
                BtnSearch.Enabled = true;
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }

        }



        #region Background Worker


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

        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //if (e.RowHandle  < 0)
            //{
            //    return;
            //}

            //if (Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BalanceCarat")) == 0)
            //{
            //    e.Appearance.BackColor = Color.FromArgb(255, 224, 192);                
            //}

            // Set CHIEF ARTIST PREDICTION With Makable Diff

        }

        private void CmbBaseCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string Name = Val.ToString(CmbBaseCompare.SelectedItem);
                //DataRow[] D = DTabPrdType.Select("PRDTYPENAME ='" + Name + "' ");

                //if (D.Length != 0)
                //{
                //    DataRow DRow = D[0];
                //    CmbBaseCompare.Tag = Val.ToString(DRow["PrdType_ID"]);


                //    foreach (GridBand band in GrdDet.Bands)
                //    {
                //        band.AppearanceHeader.ForeColor = Color.Black;
                //    }

                //    if (GrdDet.Bands.Count > 1)
                //    {
                //        GrdDet.Bands["PRDTYPE_" + Val.ToString(DRow["PrdType_ID"])].AppearanceHeader.ForeColor = Color.FromArgb(192, 0, 0);
                //    }


                //    DRow = null;
                //    GrdDet.Focus();

                //    BtnSearch_Click(null, null);

                //}  
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

        private void txtStoneNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String str1 = txtStoneNo.Text.Trim().Replace("\r\n", ",");
                txtStoneNo.Text = str1;
                txtStoneNo.Select(txtStoneNo.Text.Length, 0);
                string[] Str = str1.Split(',');

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
                DS = ObjView.PredictionViewForGradingGetData(mStrKapan, mIntFromPacketNo, mIntToPacketNo, mStrTag, mStrParentTag, mStrMainTag, mStrPredictionType, StrFromDate, StrToDate, StrOpe, mIntEmployeeID, Val.ToString(txtStoneNo.Text));

                DTabPacketDetail = DS.Tables[0].Copy();
                DTabPrdDetail = DS.Tables[1].Copy();
                DTabPrdSummary = DS.Tables[2].Copy();
                DTabPolishOkDetail = DS.Tables[3].Copy();
            }
            catch (Exception EX)
            {
                PanleProgress.Visible = false;
                BtnSearch.Enabled = true;
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }

        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanleProgress.Visible = false;
                BtnSearch.Enabled = true;

                GrdDet.BeginUpdate();

                DTabPredictionView.Rows.Clear();
                foreach (DataRow DRow in DTabPacketDetail.Rows)
                {
                    DataRow DRNew = DTabPredictionView.NewRow();

                    DRNew["KapanName"] = Val.ToString(DRow["KapanName"]);
                    DRNew["PacketTag"] = Val.ToString(DRow["PacketTag"]);
                    // DRNew["LotCarat"] = Val.ToString(DRow["LotCarat"]);
                    // DRNew["BalanceCarat"] = Val.ToString(DRow["BalanceCarat"]);

                    string StrQueryPol = "KapanName = '" + Val.ToString(DRow["KapanName"]) + "' And PacketNo = '" + Val.ToString(DRow["PacketNo"]) + "' And Tag = '" + Val.ToString(DRow["Tag"]) + "'";
                    DataRow[] UDROWPOL = DTabPolishOkDetail.Select(StrQueryPol);
                    //if (UDROWPOL.Length != 0)
                    //{
                    //    DRNew["PolishOKEmpCode"] = Val.ToString(UDROWPOL[0]["WORKERCODE"]);
                    //    DRNew["PolishOKIssueCarat"] = Val.Val(UDROWPOL[0]["ISSUECARAT"]);
                    //    DRNew["PolishOKReadyCarat"] = Val.Val(UDROWPOL[0]["READYCARAT"]);
                    //}

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
                                DRNew[Col + "Shp"] = Val.ToString(dddd["SHPCODE"]);
                                DRNew[Col + "Col"] = Val.ToString(dddd["COLCODE"]);
                                DRNew[Col + "Cla"] = Val.ToString(dddd["CLACODE"]);
                                DRNew[Col + "Cut"] = Val.ToString(dddd["CUTCODE"]);
                                DRNew[Col + "Pol"] = Val.ToString(dddd["POLCODE"]);
                                DRNew[Col + "Sym"] = Val.ToString(dddd["SYMCODE"]);
                                DRNew[Col + "FL"] = Val.ToString(dddd["FLNAME"]);
                                DRNew[Col + "ColorShadeCode"] = Val.ToString(dddd["COLORSHADECODE"]);
                                DRNew[Col + "MilkyCode"] = Val.ToString(dddd["MILKYCODE"]);
                                DRNew[Col + "LblcName"] = Val.ToString(dddd["LBLCNAME"]);
                                DRNew[Col + "NattsName"] = Val.ToString(dddd["NATTSNAME"]);
                                DRNew[Col + "ColorShadeCode"] = Val.ToString(dddd["COLORSHADECODE"]);
                                DRNew[Col + "Carat"] = Val.Val(dddd["CARAT"]);
                                DRNew[Col + "Rapaport"] = Val.Val(dddd["RAPAPORT"]);
                                DRNew[Col + "Discount"] = Val.Val(dddd["DISCOUNT"]);
                                DRNew[Col + "PricePerCarat"] = Val.Val(dddd["PRICEPERCARAT"]);
                                DRNew[Col + "Amount"] = Val.Val(dddd["AMOUNT"]);
                                DRNew[Col + "LabProcess"] = Val.ToString(dddd["LABPROCESS"]);
                                DRNew[Col + "LabSelection"] = Val.ToString(dddd["LABSELECTION"]);
                                DRNew[Col + "BlackIncCode"] = Val.ToString(dddd["BLACKINCCODE"]);
                                DRNew[Col + "WhiteIncCode"] = Val.ToString(dddd["WHITEINCCODE"]);
                                DRNew[Col + "OpenIncCode"] = Val.ToString(dddd["OPENINCCODE"]);
                                DRNew[Col + "PavCode"] = Val.ToString(dddd["PAVCODE"]);
                                DRNew[Col + "MilkyCode"] = Val.ToString(dddd["MILKYCODE"]);
                                DRNew[Col + "LusterName"] = Val.ToString(dddd["LUSTERNAME"]);
                                DRNew[Col + "EyeCleanCode"] = Val.ToString(dddd["EYECLEANCODE"]);
                                DRNew[Col + "HaName"] = Val.ToString(dddd["HANAME"]);
                                DRNew[Col + "SuratExpBeforeDiscount"] = Val.Val(dddd["SURATEXPBEFOREDISCOUNT"]);
                                DRNew[Col + "SuratExpBeforePricePerCarat"] = Val.Val(dddd["SURATEXPBEFOREPRICEPERCARAT"]);
                                DRNew[Col + "SuratExpBeforeAmount"] = Val.Val(dddd["SURATEXPBEFOREAMOUNT"]);
                                DRNew[Col + "SuratExpLabCharge"] = Val.Val(dddd["SURATEXPLABCHARGE"]);
                                DRNew[Col + "SuratExpAfterDiscount"] = Val.Val(dddd["SURATEXPAFTERDISCOUNT"]);
                                DRNew[Col + "SuratExpBefoRepricePerCarat"] = Val.Val(dddd["SURATEXPAFTERPRICEPERCARAT"]);
                                DRNew[Col + "SuratExpAfterAmount"] = Val.Val(dddd["SURATEXPAFTERAMOUNT"]);

                                DRNew[Col + "MumbaiExpBeforeDiscount"] = Val.Val(dddd["MUMBAIEXPBEFOREDISCOUNT"]);
                                DRNew[Col + "MumbaiExpBeforePricePerCarat"] = Val.Val(dddd["MUMBAIEXPBEFOREPRICEPERCARAT"]);
                                DRNew[Col + "MumbaiExpBeforeAmount"] = Val.Val(dddd["MUMBAIEXPBEFOREAMOUNT"]);
                                DRNew[Col + "MumbaiExpLabCharge"] = Val.Val(dddd["MUMBAIEXPLABCHARGE"]);
                                DRNew[Col + "MumbaiExpAfterDiscount"] = Val.Val(dddd["MUMBAIEXPAFTERDISCOUNT"]);
                                DRNew[Col + "MumbaiExpBefoRepricePerCarat"] = Val.Val(dddd["MUMBAIEXPAFTERPRICEPERCARAT"]);
                                DRNew[Col + "MumbaiExpAfterAmount"] = Val.Val(dddd["MUMBAIEXPAFTERAMOUNT"]);

								//Add Milan (13-03-2021)
								DRNew[Col + "MkavDiscount"] = Val.Val(dddd["MKAVDISCOUNT"]);
								DRNew[Col + "MkavPricePerCarat"] = Val.Val(dddd["MKAVPRICEPERCARAT"]);
								DRNew[Col + "MkavAmount"] = Val.Val(dddd["MKAVAMOUNT"]);
								DRNew[Col + "ExpDiscount"] = Val.Val(dddd["EXPDISCOUNT"]);
								DRNew[Col + "ExpPricePerCarat"] = Val.Val(dddd["EXPPRICEPERCARAT"]);
								DRNew[Col + "ExpAmount"] = Val.Val(dddd["EXPAMOUNT"]);
								//End Milan

                                DRNew[Col + "GrdStatus"] = Val.ToString(dddd["GRDSTATUS"]);
                                DRNew[Col + "Diamin"] = Val.Val(dddd["DIAMIN"]);
                                DRNew[Col + "Diamax"] = Val.Val(dddd["DIAMAX"]);
                                DRNew[Col + "Height"] = Val.Val(dddd["Height"]);
                                DRNew[Col + "Heliumtablepc"] = Val.Val(dddd["HELIUMTABLEPC"]);
                                DRNew[Col + "Heliumratio"] = Val.Val(dddd["HELIUMRATIO"]);
                                DRNew[Col + "Heliumtotaldepth"] = Val.Val(dddd["HELIUMTOTALDEPTH"]);


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

            }
            catch (Exception EX)
            {
                PanleProgress.Visible = false;
                BtnSearch.Enabled = true;
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }
        }


    }
}
