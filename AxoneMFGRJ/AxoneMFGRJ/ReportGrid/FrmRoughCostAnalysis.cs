using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Data;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmRoughCostAnalysis : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughCostAnalysis ObjView = new BOTRN_RoughCostAnalysis();
        BOFormPer ObjPer = new BOFormPer();

        DataTable DTabDetail = new DataTable();
        DataTable DTabSummary = new DataTable();

        DataSet DSMemoDetail = new DataSet();
        RoughCostAnalysisProperty mProperty = new RoughCostAnalysisProperty();

        //double DblWeight = 0, DblRPcs = 0, DblAvgSize = 0, DblPurRate = 0, DblPurAmt = 0, DblByAvg = 0, DblByAmt = 0, DblByPer = 0;
        //double DblFinAvg = 0, DblFinAmt = 0, DblFinPer = 0, DblLabAvg = 0, DblLabAmt = 0, DblLabPer = 0, DblSalAvg = 0, DblSalPer = 0, DblSalAmt = 0, DblLabourPer = 0, DblLabourAmt = 0;


        double DouAmount = 0;

        double DouRejectionCarat = 0;
        double DouCarat = 0;
        double Kapancarat = 0;

        double RJCARAT = 0;
        double RJAMOUNT = 0;

        double MakCart = 0;

        double RejCarat = 0;
        double KapanAmount = 0;

        double MkblPcs = 0;
        double MfgIssueCarat = 0;

        double PolishReadyCarat = 0;

        double DouRejAmount = 0;
        double MFGCostAmount = 0;
        double MumbaiAvg = 0;
        double MumbaiCnvRate = 0;
        double PolAmt = 0;
        double Rate = 0;
        double ExeRate = 0;
        double PadtarAmt = 0;
        double KapanRate = 0;
        double LotConvRate = 0;
        double PolRecvPcs = 0;
        double MfgLabourAmount = 0;
        double PolAmountD = 0;
        double PolAmtRs = 0;

        double PolAvgRate = 0;
        double PolExcRate = 0;

        double pDouPolDiff = 0; // Dhara : 19-05-2023
        double pDouLotCarat = 0; // Dhara : 19-05-2023
        double pDouLotRate = 0; // Dhara : 19-05-2023
        double pDouOtherMFGExp = 0; // Dhara : 19-05-2023
        double pDouRoughPrice = 0; // Dhara : 19-05-2023
        double pDouLotRateRs = 0; // Dhara : 19-05-2023
        double pDouAmoutRs = 0; // Dhara : 19-05-2023
        double pKapanRateAmtRs = 0; // Dhara : 20-05-2023

        double pFinalPolAvgDiff = 0; // Dhara : 10-08-2023
        double pFinalCost = 0; // Dhara : 10-08-2023
        double pPolAvgRate = 0; // Dhara : 10-08-2023

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        string mStrReportTitle = "";

        public FrmRoughCostAnalysis()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            string pStrPassword = ObjPer.PASSWORD;

            FrmPassword FrmPassword = new FrmPassword();
            if (FrmPassword.ShowForm(pStrPassword) == System.Windows.Forms.DialogResult.No)
            {
                this.Close();
                return;
            }

            this.Show();
            GrdDet.Columns["MFGSDATE"].OptionsColumn.AllowEdit = false;
            GrdDet.Columns["POLRDATE"].OptionsColumn.AllowEdit = false;
            GrdDet.Columns["MUMBAIRECVDATE"].OptionsColumn.AllowEdit = false;

            txtPassForDisplayBack_TextChanged(null, null);



            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPAN_ID";

            CmbRoughName.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.TRN_LOT);
            CmbRoughName.Properties.DisplayMember = "LOTNO";
            CmbRoughName.Properties.ValueMember = "LOT_ID";

            CmbRoughTye.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_ROUGHTYPE);
            CmbRoughTye.Properties.DisplayMember = "ROUGHTYPE";
            CmbRoughTye.Properties.ValueMember = "ROUGHTYPE";

            CmbCleaver.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_EMPLOYEE);
            CmbCleaver.Properties.DisplayMember = "EMPLOYEENAME";
            CmbCleaver.Properties.ValueMember = "EMPLOYEE_ID";

            CmbRoughDescription.Properties.DataSource = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_ROUGHDESC);
            CmbRoughDescription.Properties.DisplayMember = "REMARK";
            CmbRoughDescription.Properties.ValueMember = "REMARK";



        }

        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjView);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                mProperty = new RoughCostAnalysisProperty();
                mProperty.ReportType = Val.ToString(CmbReportType.SelectedItem);
                mProperty.KapanName = Val.Trim(CmbKapan.Properties.GetCheckedItems());
                mProperty.RoughName = Val.Trim(CmbRoughName.Properties.GetCheckedItems());
                mProperty.RoughType = Val.Trim(CmbRoughTye.Properties.GetCheckedItems());
                mProperty.CleaverName = Val.Trim(CmbCleaver.Properties.GetCheckedItems());
                mProperty.RoughDescription = Val.Trim(CmbRoughDescription.Properties.GetCheckedItems());

                if (RbtAll.Checked == true)
                {
                    mProperty.RoughStatus = "All";
                }
                else if (RbtCompleteStock.Checked == true)
                {
                    mProperty.RoughStatus = "Complete";
                }
                else if (RbtCurrentStock.Checked == true)
                {
                    mProperty.RoughStatus = "Running";
                }

                mProperty.FromPolDate = null;
                mProperty.ToPolDate = null;
                mProperty.FromClvIssueDate = null;
                mProperty.ToClvIssueDate = null;
                mProperty.SendFromDate = null;
                mProperty.SendToDate = null;

                if (DTPFromPolishDate.Checked == true && DTPToPolishDate.Checked == true)
                {
                    mProperty.FromPolDate = Val.SqlDate(DTPFromPolishDate.Value.ToShortDateString());
                    mProperty.ToPolDate = Val.SqlDate(DTPToPolishDate.Value.ToShortDateString());
                }
                if (DTPFromClvFromDate.Checked == true && DTPFromClvToDate.Checked == true)
                {
                    mProperty.FromClvIssueDate = Val.SqlDate(DTPFromClvFromDate.Value.ToShortDateString());
                    mProperty.ToClvIssueDate = Val.SqlDate(DTPFromClvToDate.Value.ToShortDateString());
                }
                if (DTPFromSendFromDate.Checked == true && DTPFromSendToDate.Checked == true)
                {
                    mProperty.SendFromDate = Val.SqlDate(DTPFromSendFromDate.Value.ToShortDateString());
                    mProperty.SendToDate = Val.SqlDate(DTPFromSendToDate.Value.ToShortDateString());
                }
                mProperty.IsPending = ChkIsPending.Checked;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }


            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
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
            TextBrick BrickTitleseller = e.Graph.DrawString(mStrReportTitle, System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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


        private void GrdDetDept_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
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
        private void FrmFactoryProduction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BtnRefresh_Click(null, null);
            }
        }

        private void MainGrid_Paint(object sender, PaintEventArgs e)
        {
            //GridControl gridC = sender as GridControl;
            //GridView gridView = gridC.FocusedView as GridView;
            //BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            //for (int i = 0; i < info.BandsInfo.BandCount; i++)
            //{
            //    e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            //}
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("RoughCostAnalysis", GrdDet);
        }

        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouCarat = 0;
                    DouAmount = 0;
                    DouRejAmount = 0;
                    RJCARAT = 0;
                    RJAMOUNT = 0;
                    Kapancarat = 0;
                    MakCart = 0;
                    KapanAmount = 0;
                    MkblPcs = 0;
                    DouRejectionCarat = 0;
                    MFGCostAmount = 0;
                    PolishReadyCarat = 0;
                    MfgIssueCarat = 0;
                    MumbaiAvg = 0;
                    MumbaiCnvRate = 0;
                    PolAmt = 0;
                    Rate = 0;
                    ExeRate = 0;
                    PadtarAmt = 0;
                    KapanRate = 0;
                    LotConvRate = 0;
                    PolRecvPcs = 0;
                    MfgLabourAmount = 0;
                    PolAmountD = 0;
                    PolAmtRs = 0;
                    PolAvgRate = 0;
                    PolExcRate = 0;

                    pDouPolDiff = 0; // Dhara : 19-05-2023
                    pDouLotRate = 0; // Dhara : 19-05-2023
                    pDouLotCarat = 0; // Dhara : 19-05-2023
                    pDouOtherMFGExp = 0; // Dhara : 19-05-2023 
                    pDouRoughPrice = 0; // Dhara : 19-05-2023
                    pDouLotRateRs = 0; // Dhara : 19-05-2023
                    pDouAmoutRs = 0; // Dhara : 19-05-2023
                    pKapanRateAmtRs = 0; // Dhara : 20-05-2023

                    pFinalPolAvgDiff = 0; //Dhara : 20-05-2023
                    pFinalCost = 0;// Dhara : 20-05-2023
                    pPolAvgRate = 0;// Dhara : 20-05-2023

                }

                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouCarat = DouCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "CARAT"));
                    DouAmount = Math.Round(DouAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "AMOUNT")), 2);

                    RJCARAT = RJCARAT + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "OUTWEIGHT"));
                    RJAMOUNT = Math.Round(RJAMOUNT + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "RJAMOUNT")), 2);

                    Kapancarat = Kapancarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "LOTCTS"));
                    KapanAmount = Math.Round(KapanAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "KAPANAMOUNT")), 2);

                    MakCart = MakCart + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "MAKCARAT"));
                    MkblPcs = MkblPcs + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "MKBLPCS"));

                    DouRejectionCarat = DouRejectionCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "REJCARAT"));
                    DouRejAmount = Math.Round(DouRejAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "REJAMOUNT")), 2);

                    PolishReadyCarat = PolishReadyCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLRECEIVECARAT"));

                    MfgIssueCarat = MfgIssueCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "MFGISSUECARAT"));

                    MFGCostAmount = MFGCostAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "MFGCOSTAMOUNT"));

                    MumbaiAvg = MumbaiAvg + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLAVGRATERS"));
                    MumbaiCnvRate = MfgIssueCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLEXCRATE"));

                    LotConvRate = LotConvRate + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "LOTCONVRATE"));
                    Rate = Rate + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "RATE"));
                    ExeRate = ExeRate + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "EXCRATE"));
                    PadtarAmt = PadtarAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "MFGCOSTAMOUNT"));

                    KapanRate = KapanRate + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "KAPANRATE"));

                    PolRecvPcs = PolRecvPcs + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLRECEIVEPCS"));
                    MfgLabourAmount = MfgLabourAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "MFGLABOURAMOUNT"));
                    PolAmountD = PolAmountD + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLAMOUNT"));

                    // Calculation As Per Discusion with Hiteshbhai : 19-05-223 : Dhara

                    PolAmt = PolAmt + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLAMOUNT"));
                    PolAmtRs = PolAmtRs + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLAMOUNTRS"));
                    pDouPolDiff = pDouPolDiff + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLDIFF"));
                    pDouLotRate = pDouLotRate + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "LOTRATE"));
                    pDouLotRateRs = pDouLotRateRs + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "LOTRATERS"));
                    pDouLotCarat = pDouLotCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "LOTCTS"));
                    pDouOtherMFGExp = pDouOtherMFGExp + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "OTHERMFGEXP"));
                    pDouRoughPrice = pDouRoughPrice + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "ROUGHPRICE"));
                    pDouAmoutRs = pDouAmoutRs + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "AMOUNTRS"));
                    pKapanRateAmtRs = pKapanRateAmtRs + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "KAPANAMOUNTRS"));
                    PolExcRate = PolExcRate + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLEXCRATE"));
                    // #End : Dhara : 19-5-2023

                }


                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouAmount) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("OUTRATE") == 0)
                    {
                        if (Val.Val(RJCARAT) > 0)
                            e.TotalValue = Math.Round(Val.Val(RJAMOUNT) / Val.Val(RJCARAT), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("KAPANRATE") == 0)
                    {
                        if (Val.Val(Kapancarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(pDouAmoutRs) / Val.Val(Kapancarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("MAKSIZE") == 0)
                    {
                        if (Val.Val(MakCart) > 0)
                            e.TotalValue = Math.Round((Val.Val(MkblPcs) / Val.Val(MakCart)), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("MAKPER") == 0)
                    {
                        if (Val.Val(Kapancarat) > 0)
                            e.TotalValue = Math.Round((Val.Val(MakCart) / Val.Val(Kapancarat) * 100), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("REJPRICE") == 0)
                    {
                        if (Val.Val(RejCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouRejAmount) / Val.Val(RejCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("ROUGHTOPOLPER") == 0)
                    {
                        if (Val.Val(Kapancarat) > 0)
                            e.TotalValue = Math.Round((Val.Val(PolishReadyCarat) / Val.Val(Kapancarat) * 100), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("MAKTOPOLPER") == 0)
                    {
                        if (Val.Val(MfgIssueCarat) > 0)
                            e.TotalValue = Math.Round((Val.Val(PolishReadyCarat) / Val.Val(MfgIssueCarat) * 100), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FINALCOST") == 0)
                    {
                        if (Val.Val(PolishReadyCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(MFGCostAmount) / Val.Val(PolishReadyCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLAVGRATE") == 0)
                    {
                        if (Val.Val(PolExcRate) > 0)
                            e.TotalValue = Math.Round((Math.Round(Val.Val(PolAmtRs) / Val.Val(PolishReadyCarat), 2)) / (Math.Round(Val.Val(PolAmtRs) / Val.Val(PolAmt), 2)), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLAVGRATERS") == 0)
                    {
                        if (Val.Val(PolishReadyCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(PolAmtRs) / Val.Val(PolishReadyCarat), 2);
                        else
                            e.TotalValue = 0;
                    }


                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLAMOUNT") == 0)
                    {
                        if (Val.Val(PolExcRate) > 0)
                            e.TotalValue = Math.Round(Val.Val(PolAmtRs) / Math.Round(Val.Val(PolAmtRs) / Val.Val(PolAmt), 2), 2);
                        else
                            e.TotalValue = 0;
                    }

                    // Change By Dhara : 19-05-2023

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLEXCRATE") == 0)
                    {
                        if (Val.Val(PolAmt) > 0)
                            e.TotalValue = Math.Round(Val.Val(PolAmtRs) / Val.Val(PolAmt), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLDIFF") == 0)
                    {
                        e.TotalValue = Math.Round(Val.Val(MumbaiAvg) - Val.Val(PadtarAmt), 2);
                    }

                    //if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("LOTCONVRATE") == 0)
                    //{
                    //    if (Val.Val(KapanRate) > 0)
                    //        e.TotalValue = Math.Round(Val.Val(pDouLotRateRs) / Val.Val(KapanRate), 2);
                    //    else
                    //        e.TotalValue = 0;
                    //}

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouAmount) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("ROUGHPRICE") == 0)
                    {
                        if (Val.Val(pDouLotCarat) > 0)
                            e.TotalValue = Math.Round((Val.Val(PolAmtRs) + Val.Val(DouRejAmount) - Val.Val(MfgLabourAmount) - Val.Val(pDouOtherMFGExp)) / Val.Val(pDouLotCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("ROUGHDIFF") == 0)
                    {
                        e.TotalValue = Math.Round(Val.Val(pDouRoughPrice) - Val.Val(pDouLotRateRs), 2);
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATERS") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(pDouAmoutRs) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("LOTRATERS") == 0)
                    {
                        if (Val.Val(pDouLotCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(pKapanRateAmtRs) / Val.Val(pDouLotCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    // #End : Dhara : 19-05-2023

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLAMOUNTRS") == 0)
                    {
                        if (Val.Val(PolishReadyCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(MumbaiAvg) / Val.Val(PolishReadyCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("POLAVGRATERS") == 0)
                    {
                        if (Val.Val(PolishReadyCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(PolAmtRs) / Val.Val(PolishReadyCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FULLFINALCOST") == 0)
                    {
                        if (Val.Val(PolishReadyCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(MFGCostAmount) / Val.Val(PolishReadyCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("KAPANLABOURRATE") == 0)
                    {
                        if (Val.Val(MfgLabourAmount) > 0)
                            e.TotalValue = Math.Round(Val.Val(MfgLabourAmount) / Val.Val(PolRecvPcs), 2);
                        else
                            e.TotalValue = 0;
                    }
                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("KAPANLABOURRATE") == 0)
                    {
                        if (Val.Val(MfgLabourAmount) > 0)
                            e.TotalValue = Math.Round(Val.Val(MfgLabourAmount) / Val.Val(PolRecvPcs), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouAmount) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                    }

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("FINALPOLAVGDIFF") == 0)
                    {
                        if (Val.Val(PolishReadyCarat) > 0)
                        {
                            pFinalCost = Math.Round((Val.Val(MFGCostAmount) / Val.Val(PolishReadyCarat)), 2);
                        }
                        if (Val.Val(PolExcRate) > 0)
                        {
                            pPolAvgRate = Math.Round((Math.Round(Val.Val(PolAmtRs) / Val.Val(PolishReadyCarat), 2)) / (Math.Round(Val.Val(PolAmtRs) / Val.Val(PolAmt), 2)), 2);
                        }

                        e.TotalValue = pFinalCost - pPolAvgRate;
                        

                        
                    }

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle < 0)
            //        return;
            //    if (e.Column.FieldName == "TOT_LABEXPENSEPER" || e.Column.FieldName == "SALELABOURPER" || e.Column.FieldName == "SALEPER" || e.Column.FieldName == "LABPER" || e.Column.FieldName == "BYLABOURPER" || e.Column.FieldName == "BYPER" || e.Column.FieldName == "FINALPRDPER")
            //    {
            //        decimal decPer = Val.ToDecimal(GrdDet.GetRowCellValue(e.RowHandle, e.Column.FieldName));
            //        if (decPer > 0)
            //        {
            //            e.Appearance.ForeColor = Color.Green;
            //        }
            //        else if (decPer < 0)
            //        {
            //            e.Appearance.ForeColor = Color.Red;
            //        }
            //        else
            //        {
            //            e.Appearance.ForeColor = Color.Black;
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    Global.Message(Ex.Message);
            //}
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DSMemoDetail = ObjView.GetData(mProperty);
            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelProgress.Visible = false;
                MainGrid.DataSource = DSMemoDetail.Tables[0];
                MainGrid.Refresh();
                GrdDet.BestFitColumns();
                DataTable dtab = DSMemoDetail.Tables[0];
                //for (int i = 0; i < dtab.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dtab.Rows[i].Cells.Count; j++)
                //    {
                //         GrdDet.dtab[i].Cells[j].Style.BackColor =

                //    }
                //}
            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_CellMerge(object sender, CellMergeEventArgs e)
        {
            if ("PARTYINVOICENO".Contains(e.Column.FieldName))
            {
                string val1 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle1, GrdDet.Columns["LOT_ID"]));
                string val2 = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle2, GrdDet.Columns["LOT_ID"]));
                if (val1 == val2)
                    e.Merge = true;
                e.Handled = true;
            }
        }

        private void GrdDet_RowCellStyle_1(object sender, RowCellStyleEventArgs e)
        {
            ////if(e.RowHandle < 0)
            ////{
            ////    return;
            ////}


            //   //for (int i = 0; i < GrdDet.RowCount; i++)
            //   // {
            //if (Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLDIFFPLUS")) != 0.00)
            //       {
            //           e.Appearance.BackColor =  Color.FromArgb(192, 255, 192);

            //       }
            //       //else
            //       //{
            //       //    e.Appearance.BackColor =  Color.Transparent;
            //       //}

            //if (Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "POLDIFFMINUS")) != 0.00)
            //       {
            //           e.Appearance.BackColor =  Color.FromArgb(255, 102, 102);
            //       }
            //       //else
            //       //{
            //       //    e.Appearance.BackColor =  Color.Transparent;
            //       //}

            //    //}
        }

        private void GrdDet_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "POLDIFFPLUS")
            {
                if (Convert.ToInt32(e.CellValue) != 0.00)
                    e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
            }

            if (e.Column.FieldName == "POLDIFFMINUS")
            {
                if (Convert.ToInt32(e.CellValue) != 0.00)
                    e.Appearance.BackColor = Color.FromArgb(255, 102, 102);
            }
        }

        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtPassForDisplayBack.Tag) != "" && Val.ToString(txtPassForDisplayBack.Tag).ToUpper() == txtPassForDisplayBack.Text.ToUpper())
                {

                    GrdDet.Columns["MFGSDATE"].OptionsColumn.AllowEdit = true;
                    GrdDet.Columns["POLRDATE"].OptionsColumn.AllowEdit = true;
                    GrdDet.Columns["MUMBAIRECVDATE"].OptionsColumn.AllowEdit = true;

                    GrdDet.FocusedColumn = GrdDet.Columns["MFGSDATE"];
                    GrdDet.FocusedRowHandle = 0;
                    GrdDet.Focus();
                    GrdDet.ShowEditor();


                }
                else
                {
                    GrdDet.Columns["MFGSDATE"].OptionsColumn.AllowEdit = false;
                    GrdDet.Columns["POLRDATE"].OptionsColumn.AllowEdit = false;
                    GrdDet.Columns["MUMBAIRECVDATE"].OptionsColumn.AllowEdit = false;

                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }

        private void MainGrid_Validating(object sender, CancelEventArgs e)
        {
            //try
            //{
            //    if (GrdDet.FocusedRowHandle < 0)
            //    {
            //        return;
            //    }
            //    if (Global.Confirm("Are You Sure You Want To Update this Entry ?") == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        RoughCostAnalysisProperty Property = new RoughCostAnalysisProperty();

            //        Property.MFGSDATE = Val.ToString(GrdDet.GetFocusedRowCellValue("MFGSDATE"));
            //        Property.POLRDATE = Val.ToString(GrdDet.GetFocusedRowCellValue("POLRDATE"));
            //        Property.MUMBAIRECVDATE = Val.ToString(GrdDet.GetFocusedRowCellValue("MUMBAIRECVDATE"));

            //        this.Cursor = Cursors.WaitCursor;

            //        Property = new BOTRN_RoughCostAnalysis().Update(Property);
            //        this.Cursor = Cursors.Default;
            //        if (Property.ReturnMessageType == "SUCCESS")
            //        {
            //            Global.Message(Property.ReturnMessageDesc);
            //            //GrdDetRejection.DeleteRow(GrdDetRejection.FocusedRowHandle);
            //            BtnRefresh_Click(null, null);
            //        }
            //        else
            //        {
            //            Global.MessageError(Property.ReturnMessageDesc);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message.ToString());
            //}
        }

        private void GrdDet_CustomSummaryCalculate_1(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                {
                    return;
                }
                if (Global.Confirm("Are You Sure You Want To Update this Entry ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    RoughCostAnalysisProperty Property = new RoughCostAnalysisProperty();
                    Property.KAPAN_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("KAPAN_ID"));
                    Property.MFGSDATE = Val.ToString(GrdDet.GetFocusedRowCellValue("MFGSDATE"));
                    Property.POLRDATE = Val.ToString(GrdDet.GetFocusedRowCellValue("POLRDATE"));
                    Property.MUMBAIRECVDATE = Val.ToString(GrdDet.GetFocusedRowCellValue("MUMBAIRECVDATE"));

                    this.Cursor = Cursors.WaitCursor;

                    Property = new BOTRN_RoughCostAnalysis().Update(Property);
                    this.Cursor = Cursors.Default;
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        Global.Message(Property.ReturnMessageDesc);
                        //GrdDetRejection.DeleteRow(GrdDetRejection.FocusedRowHandle);
                        BtnRefresh_Click(null, null);
                    }
                    else
                    {
                        Global.MessageError(Property.ReturnMessageDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void FrmRoughCostAnalysis_Load(object sender, EventArgs e)
        {
            GrdDet.Columns["MFGSDATE"].OptionsColumn.AllowEdit = false;
            GrdDet.Columns["POLRDATE"].OptionsColumn.AllowEdit = false;
            GrdDet.Columns["MUMBAIRECVDATE"].OptionsColumn.AllowEdit = false;
        }

        private void CmbReportType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbReportType.Text == "Mumbai Polish View")
            {
                GrdDet.Columns["PARTYINVOICENO"].Visible = false;
                GrdDet.Columns["PARTYNAME"].Visible = false;
                GrdDet.Columns["ENTRYNO"].Visible = false;
                GrdDet.Columns["CARAT"].Visible = false;
                GrdDet.Columns["RATE"].Visible = false;
                GrdDet.Columns["AMOUNT"].Visible = false;
                GrdDet.Columns["EXCRATE"].Visible = false;
                GrdDet.Columns["RATERS"].Visible = false;
                GrdDet.Columns["AMOUNTRS"].Visible = false;
                GrdDet.Columns["OUTWEIGHT"].Visible = false;
                GrdDet.Columns["OUTRATE"].Visible = false;
                GrdDet.Columns["OUTAMOUNT"].Visible = false;
                GrdDet.Columns["NETRGHWEIGHT"].Visible = false;
                GrdDet.Columns["STKCTS"].Visible = false;
                GrdDet.Columns["MFGSDATE"].Visible = false;
                GrdDet.Columns["MUMBAIRECVDATE"].Visible = false;
                GrdDet.Columns["RGHDESC"].Visible = false;
                GrdDet.Columns["KAPANRATE"].Visible = false;
                GrdDet.Columns["CLEAVERNAME"].Visible = false;
                GrdDet.Columns["CLEAVERGROUP"].Visible = false;
                GrdDet.Columns["LOTCTS"].Visible = false;
                GrdDet.Columns["KAPANAMOUNT"].Visible = false;
                GrdDet.Columns["KAPANAMOUNTRS"].Visible = false;
                GrdDet.Columns["MAKCARAT"].Visible = false;
                GrdDet.Columns["MAKPER"].Visible = false;
                GrdDet.Columns["MAKSIZE"].Visible = false;
                GrdDet.Columns["POLISSUEPCS"].Visible = false;
                GrdDet.Columns["POLISSUECARAT"].Visible = false;
                GrdDet.Columns["REJCARAT"].Visible = false;
                GrdDet.Columns["REJPRICE"].Visible = false;
                GrdDet.Columns["REJAMOUNT"].Visible = false;
                GrdDet.Columns["CLVWEIGHTLOSS"].Visible = false;
                GrdDet.Columns["GALAXYPCS"].Visible = false;
                GrdDet.Columns["GALAXYLABOUR"].Visible = false;
                GrdDet.Columns["QCLABOUR"].Visible = false;
                GrdDet.Columns["SARINLABOUR"].Visible = false;
                GrdDet.Columns["MFGISSUECARAT"].Visible = false;
                GrdDet.Columns["POLRECEIVEPCS"].Visible = false;
                GrdDet.Columns["POLRECEIVECARAT"].Visible = true;
                GrdDet.Columns["KAPANLABOURRATE"].Visible = false;
                GrdDet.Columns["MFGLABOURAMOUNT"].Visible = false;
                GrdDet.Columns["OTHERMFGEXP"].Visible = false;
                GrdDet.Columns["ROUGHTOPOLPER"].Visible = false;
                GrdDet.Columns["MFGCOSTAMOUNT"].Visible = false;
                GrdDet.Columns["MAKTOPOLPER"].Visible = false;
                GrdDet.Columns["FINALCOST"].Visible = false;
                GrdDet.Columns["OTHEREXPAMOUNT"].Visible = false;
                GrdDet.Columns["FULLFINALCOST"].Visible = false;
                GrdDet.Columns["POLAVGRATE"].Visible = false;
                GrdDet.Columns["POLAMOUNT"].Visible = false;
                GrdDet.Columns["POLEXCRATE"].Visible = false;
                GrdDet.Columns["POLDIFF"].Visible = false;
                GrdDet.Columns["POLDIFFPLUS"].Visible = false;
                GrdDet.Columns["POLDIFFMINUS"].Visible = false;
                GrdDet.Columns["LOTCONVRATE"].Visible = false;
                GrdDet.Columns["LOTRATERS"].Visible = false;
                GrdDet.Columns["ROUGHPRICE"].Visible = false;
                GrdDet.Columns["SALEWEIGHT"].Visible = false;
                GrdDet.Columns["SALEPRICE"].Visible = false;
                GrdDet.Columns["ROUGHDIFF"].Visible = false;
                GrdDet.Columns["ROUGHDIFFPLUS"].Visible = false;
                GrdDet.Columns["ADDEXPAMOUNT"].Visible = false;
                GrdDet.Columns["PENALTYAMOUNT"].Visible = false;
                GrdDet.Columns["ROUGHDIFFMINUS"].Visible = false;

            }
            else
            {
                GrdDet.Columns["PARTYINVOICENO"].Visible = true;
                GrdDet.Columns["PARTYNAME"].Visible = true;
                GrdDet.Columns["ENTRYNO"].Visible = true;
                GrdDet.Columns["CARAT"].Visible = true;
                GrdDet.Columns["RATE"].Visible = true;
                GrdDet.Columns["AMOUNT"].Visible = true;
                GrdDet.Columns["EXCRATE"].Visible = true;
                GrdDet.Columns["RATERS"].Visible = true;
                GrdDet.Columns["AMOUNTRS"].Visible = true;
                GrdDet.Columns["OUTWEIGHT"].Visible = true;
                GrdDet.Columns["OUTRATE"].Visible = true;
                GrdDet.Columns["OUTAMOUNT"].Visible = true;
                GrdDet.Columns["NETRGHWEIGHT"].Visible = true;
                GrdDet.Columns["STKCTS"].Visible = true;
                GrdDet.Columns["MFGSDATE"].Visible = true;
                GrdDet.Columns["MUMBAIRECVDATE"].Visible = true;
                GrdDet.Columns["RGHDESC"].Visible = true;
                GrdDet.Columns["KAPANRATE"].Visible = true;
                GrdDet.Columns["CLEAVERNAME"].Visible = true;
                GrdDet.Columns["CLEAVERGROUP"].Visible = true;
                GrdDet.Columns["LOTCTS"].Visible = true;
                GrdDet.Columns["KAPANAMOUNT"].Visible = true;
                GrdDet.Columns["KAPANAMOUNTRS"].Visible = true;
                GrdDet.Columns["MAKCARAT"].Visible = true;
                GrdDet.Columns["MAKPER"].Visible = true;
                GrdDet.Columns["MAKSIZE"].Visible = true;
                GrdDet.Columns["POLISSUEPCS"].Visible = true;
                GrdDet.Columns["POLISSUECARAT"].Visible = true;
                GrdDet.Columns["REJCARAT"].Visible = true;
                GrdDet.Columns["REJPRICE"].Visible = true;
                GrdDet.Columns["REJAMOUNT"].Visible = true;
                GrdDet.Columns["CLVWEIGHTLOSS"].Visible = true;
                GrdDet.Columns["GALAXYPCS"].Visible = true;
                GrdDet.Columns["GALAXYLABOUR"].Visible = true;
                GrdDet.Columns["QCLABOUR"].Visible = true;
                GrdDet.Columns["SARINLABOUR"].Visible = true;
                GrdDet.Columns["MFGISSUECARAT"].Visible = true;
                GrdDet.Columns["POLRECEIVEPCS"].Visible = true;
                GrdDet.Columns["POLRECEIVECARAT"].Visible = false;
                GrdDet.Columns["KAPANLABOURRATE"].Visible = true;
                GrdDet.Columns["MFGLABOURAMOUNT"].Visible = true;
                GrdDet.Columns["OTHERMFGEXP"].Visible = true;
                GrdDet.Columns["ROUGHTOPOLPER"].Visible = true;
                GrdDet.Columns["MFGCOSTAMOUNT"].Visible = true;
                GrdDet.Columns["MAKTOPOLPER"].Visible = true;
                GrdDet.Columns["FINALCOST"].Visible = true;
                GrdDet.Columns["OTHEREXPAMOUNT"].Visible = true;
                GrdDet.Columns["FULLFINALCOST"].Visible = true;
                GrdDet.Columns["POLAVGRATE"].Visible = true;
                GrdDet.Columns["POLAMOUNT"].Visible = true;
                GrdDet.Columns["POLEXCRATE"].Visible = true;
                GrdDet.Columns["POLDIFF"].Visible = true;
                GrdDet.Columns["POLDIFFPLUS"].Visible = true;
                GrdDet.Columns["POLDIFFMINUS"].Visible = true;
                GrdDet.Columns["LOTCONVRATE"].Visible = true;
                GrdDet.Columns["LOTRATERS"].Visible = true;
                GrdDet.Columns["ROUGHPRICE"].Visible = true;
                GrdDet.Columns["SALEWEIGHT"].Visible = true;
                GrdDet.Columns["SALEPRICE"].Visible = true;
                GrdDet.Columns["ROUGHDIFF"].Visible = true;
                GrdDet.Columns["ROUGHDIFFPLUS"].Visible = true;
                GrdDet.Columns["ADDEXPAMOUNT"].Visible = true;
                GrdDet.Columns["PENALTYAMOUNT"].Visible = true;
                GrdDet.Columns["ROUGHDIFFMINUS"].Visible = true;
            }

        }

    }
}
