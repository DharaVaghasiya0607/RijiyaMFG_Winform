using BusLib.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using System.Drawing.Printing;
using DevExpress.Data;

namespace AxoneMFGRJ.View
{
    public partial class FrmRoughStockReport : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();
        DataTable DtabRoughStockSummary = new DataTable();
        DataTable DtabRoughStockDetail = new DataTable();
        DataTable DtabClvEmp = new DataTable();
        string StrDataType = "";

        double DouAmount = 0;
        double DouCarat = 0;
        string pStrFormType = "";


        FORMTYPE mFormType = FORMTYPE.INWARD;

        public enum FORMTYPE
        {
            INWARD = 0,
            JOBWORK = 1
        }

        public FrmRoughStockReport()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();


            mFormType = pFormType;
            if (mFormType == FORMTYPE.INWARD)
            {
                this.Text = "ROUGH STOCK REPORT(INWARD)";
                this.Name = "FrmRoughStockReport(INWARD)";
            }
            else
            {
                this.Text = "ROUGH STOCK REPORT(JOBWORK)";
                this.Name = "FrmRoughStockReport(JOBWORK)";
            }

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            CmbRoughType.SelectedIndex = 0;

            //DtabClvEmp = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLVEMPLOYEE);
            
            this.Show();
        }

        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(Obj);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string StrOpe = "";
                string ViewType = "";
                StrOpe = "SUMMARY";
                MainGrd.DataSource = null;
                MainGrdDetail.DataSource = null;
                GrdDetail.RefreshData();

                string StrFromDate = "";
                string StrToDate = "";

                if (RbtAll.Checked == true)
                {
                    ViewType = Val.ToString(RbtAll.Tag);
                }
                else if (RbtCurrent.Checked == true)
                {
                    ViewType = Val.ToString(RbtCurrent.Tag);
                }
                else if (RbtComplete.Checked == true)
                {
                    ViewType = Val.ToString(RbtComplete.Tag);
                }
                else if (RbtOnHand.Checked == true)
                {
                    ViewType = Val.ToString(RbtOnHand.Tag);
                }
                if (Val.ToBoolean(DTPFromDate.Checked) == true && Val.ToBoolean(DTPToDate.Checked) == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }
                else
                {
                    StrFromDate = null;
                    StrToDate = null;
                }

                

                if (mFormType == FORMTYPE.INWARD)
                {
                    pStrFormType = "PURCHASE";
                }
                else
                {
                    pStrFormType = "JOBWORK";
                }


                DataSet DSet = Obj.GetRoughStockReport(StrOpe,
                                                       ViewType,
                                                       Val.Trim(CmbKapan.Properties.GetCheckedItems()),
                                                       StrFromDate,
                                                       StrToDate,
                                                       Val.ToString(CmbRoughType.Text),"","",pStrFormType 
                                                      );

                //DtabRoughStockSummary = Obj.GetKapanWiseRollingReportDataSummary(StrOpe,
                //                                            ViewType,
                //                                            Val.Trim(CmbKapan.Properties.GetCheckedItems()),
                //                                            Val.ToInt(txtFromPacketNo.Text),
                //                                            Val.ToInt(txtToPacketNo.Text),
                //                                            Val.ToString(txtTag.Text),
                //                                            Val.Trim(ChkCmbDept.Properties.GetCheckedItems()),
                //                                            Val.ToBoolean(ChkWithBombayStock.Checked)
                //                                           );

                if (DSet.Tables.Count > 0)
                {
                    DtabRoughStockSummary = DSet.Tables[0];
                   // DtabRoughStockDetail = DSet.Tables[1];
                }

                if (DtabRoughStockSummary.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    Global.Message("No Data Found");
                    return;
                }


                MainGrd.DataSource = DtabRoughStockSummary;
                GrdDet.RefreshData();
                GrdDet.BestFitColumns();
                //DataTable DTab = DtabRoughStockSummary.AsEnumerable()
                //                    .Where(ra => DtabClvEmp.AsEnumerable()
                //                    .Any(rb => rb.Field<string>("DATATYPE") == ra.Field<string>("DATATYPE")))
                //                    .CopyToDataTable();

                DataTable DTabDistinct = DtabRoughStockSummary.DefaultView.ToTable(true, "DATATYPE", "MANAGERCODE");
                DataTable DTab = DTabDistinct.Select("DATATYPE  like '%Clv%'").CopyToDataTable();

                CmbClvEmpName.Properties.DataSource = DTab;
                CmbClvEmpName.Properties.DisplayMember = "DATATYPE";
                CmbClvEmpName.Properties.ValueMember = "MANAGERCODE";
                //StrDataType = Val.Trim(CmbClvEmpName.Properties.GetCheckedItems());

                //GrdDetail.Columns["CLEAVERNAME"].UnGroup();
                if (CmbClvEmpName.Text.Length != 0)
                {
                    if (mFormType == FORMTYPE.INWARD)
                    {
                        pStrFormType = "PURCHASE";
                    }
                    else
                    {
                        pStrFormType = "JOBWORK";
                    }

                    StrOpe = "DETAIL";   
                      DataSet DSets = Obj.GetRoughStockReport(StrOpe,
                                                      ViewType,
                                                      Val.Trim(CmbKapan.Properties.GetCheckedItems()),
                                                      StrFromDate,
                                                      StrToDate,
                                                      Val.ToString(CmbRoughType.Text),
                                                      "",
                                                      Val.Trim(CmbClvEmpName.Properties.GetCheckedItems()), pStrFormType
                                                     );

                    if (DSet.Tables.Count > 0)
                    {
                        //DtabRoughStockSummary = DSet.Tables[0];
                        DtabRoughStockDetail = DSets.Tables[0];
                    }

                    if (DtabRoughStockDetail.Rows.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;
                        Global.Message("No Data Found");
                        return;
                    }
                    
                    GrdDetail.BeginUpdate();
                    MainGrdDetail.DataSource = DtabRoughStockDetail;
                    GrdDetail.RefreshData();
                    GrdDetail.BestFitColumns();
                    GrdDetail.EndUpdate();

                    //GrdDetail.Columns["CLEAVERNAME"].Group();
                }
               
                ////DtabClvEmp = DtabRoughStockSummary.AsEnumerable()
                ////            .Where(r => r.Field<string>("DATATYPE").Contains("Clv.Stock "))
                ////            .ToList();
                //            //.CopyToDataTable();
                //DataView DV = DtabRoughStockSummary.DefaultView;
                //DV.RowFilter = "DATATYPE LIKE '%Clv.Stock%'";
                //DtabClvEmp = DV.ToTable();




                //cmbClvEmpName.Properties.DataSource = DtabClvEmp;
                //cmbClvEmpName.Properties.DisplayMember = "DATATYPE";
                //cmbClvEmpName.Properties.ValueMember = "DATATYPE";


                 //DtabClvEmp = DtabRoughStockSummary.Select("DATATYPE LIKE '%Clv.Stock%'");
                 //var objlist = DtabRoughStockSummary.AsEnumerable()

                 //                           .Where(r => r.Field<string>("DATATYPE").Contains("Clv.Stock "))
                 //                           .ToList();
                 //DtabClvEmp = objlist.CopyToDataTable();

              

              
                //BtnBestFit_Click(null, null);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000" || e.DisplayText == "0 (0.000)")
                {
                    e.DisplayText = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                string StrParticular = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "DATATYPE"));

                if (StrParticular.ToUpper().Contains("OPENING"))
                {
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkBlue;
                    //e.Appearance.BackColor = Color.LightGray;
                }


                //gridColumn13.Caption = "EntryDate";

                //gridView1.OptionsView.AllowHtmlDrawHeaders = true;
                //gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;

                //string StrEntrydate = Val.ToString(GrdDetail.GetRowCellValue(e.RowHandle, "DATATYPE"));

                //if (GrdDetail.GE)
                //{
                //    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                //    e.Appearance.ForeColor = Color.DarkBlue;
                //    //e.Appearance.BackColor = Color.LightGray;
                //}

                //if (e.Column.FieldName == "Kapan")
                //{
                //    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                //    e.Appearance.BackColor = Color.LightGray;
                //    e.Appearance.BackColor2 = Color.LightGray;
                //}

                //if (e.Column.FieldName.ToUpper().Contains("PLANNING"))
                //{
                //    e.Appearance.BackColor = Color.FromArgb(192, 255, 255);
                //}
                //if (e.Column.FieldName.ToUpper().Contains("CHECKING"))
                //{
                //    e.Appearance.BackColor = Color.FromArgb(255, 230, 255);
                //}

                //if (e.Column.FieldName.ToUpper().Contains("PLANNING TOTAL"))
                //{
                //    e.Appearance.BackColor = Color.FromArgb(192, 255, 255);
                //    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                //}
                //if (e.Column.FieldName.ToUpper().Contains("CHECKING TOTAL"))
                //{
                //    e.Appearance.BackColor = Color.FromArgb(255, 230, 255);
                //    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                //}
                //if (e.Column.FieldName == "TOTAL PHYSICAL")
                //{
                //    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                //    e.Appearance.ForeColor = Color.DarkBlue;
                //    e.Appearance.BackColor = Color.LightGray;
                //}
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                string StrOpe = "";
                string ViewType = "";
                StrOpe = "DETAIL";                

                string StrFromDate = "";
                string StrToDate = "";

                if (RbtAll.Checked == true)
                {
                    ViewType = Val.ToString(RbtAll.Tag);
                }
                else if (RbtCurrent.Checked == true)
                {
                    ViewType = Val.ToString(RbtCurrent.Tag);
                }
                else if (RbtComplete.Checked == true)
                {
                    ViewType = Val.ToString(RbtComplete.Tag);
                }
                else if (RbtOnHand.Checked == true)
                {
                    ViewType = Val.ToString(RbtOnHand.Tag);
                }

                if (Val.ToBoolean(DTPFromDate.Checked) == true && Val.ToBoolean(DTPToDate.Checked) == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }
                else
                {
                    StrFromDate = null;
                    StrToDate = null;
                }

                if (e.RowHandle < 0 || e.Column.FieldName == "KAPANNAME")
                {
                    return;
                }

                if (e.Clicks == 2)
                {
                    this.Cursor = Cursors.WaitCursor;

                    //GrdDetail.Columns["CLEAVERNAME"].UnGroup();
                    //if (StrDataType == "Orig.Rgh.Out")
                    //{
                    //    gridColumn13.Caption = "EntryDate";

                    //    GrdDetail.OptionsView.AllowHtmlDrawHeaders = true;
                    //    GrdDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
                    //}

                    DataRow Dr = GrdDet.GetFocusedDataRow();
                    StrDataType = Val.ToString(Dr["DATATYPE"]).Trim().Equals(string.Empty) ? StrDataType : Val.ToString(Dr["DATATYPE"]);
                    if (StrDataType == "Orig.Rgh.Out")
                    {
                        if (StrDataType == "Orig.Rgh.Out")
                      {
                        gridColumn13.Caption = "Entry Date";
                      }
                        else
                      {
                          gridColumn13.Caption = "Kapan Date";
                      }

                        GrdDetail.Columns["KAPANCREATEDATE"].VisibleIndex = 0;
                        GrdDetail.Columns["LOTNO"].VisibleIndex = 1;
                        GrdDetail.Columns["LOTCARAT"].VisibleIndex = 2;
                        GrdDetail.Columns["RATE"].VisibleIndex = 3;
                        GrdDetail.Columns["AMOUNT"].VisibleIndex = 4;
                        GrdDetail.Columns["KAPANREMARK"].VisibleIndex = 5;

                       // GrdDetail.Columns["LOTCARAT"].Visible = false;
                        GrdDetail.Columns["CLEAVERCODE"].Visible = false;
                        GrdDetail.Columns["CLEAVERNAME"].Visible = false;
                        GrdDetail.Columns["CLVLOTNO"].Visible = false;
                        GrdDetail.Columns["REJECTIONDATE"].Visible = true;
                    }
                    else if (StrDataType == "Orig.Rgh.Sal")
                    {
                        GrdDetail.Columns["KAPANCREATEDATE"].VisibleIndex = 0;
                        GrdDetail.Columns["LOTNO"].VisibleIndex = 1;
                        GrdDetail.Columns["LOTCARAT"].VisibleIndex = 2;
                        GrdDetail.Columns["RATE"].VisibleIndex = 3;
                        GrdDetail.Columns["AMOUNT"].VisibleIndex = 4;
                        GrdDetail.Columns["KAPANREMARK"].VisibleIndex = 5;

                        GrdDetail.Columns["CLEAVERCODE"].Visible = false;
                        GrdDetail.Columns["CLEAVERNAME"].Visible = false;
                        GrdDetail.Columns["CLVLOTNO"].Visible = false;
                        if (StrDataType == "Orig.Rgh.Out")
                        {
                            gridColumn13.Caption = "Entry Date";
                        }
                        else
                        {
                            gridColumn13.Caption = "Kapan Date";
                        }
                    }
                    else if (StrDataType == "On Hand Stock")
                    {
                        GrdDetail.Columns["KAPANCREATEDATE"].VisibleIndex = 0;
                        GrdDetail.Columns["LOTNO"].VisibleIndex = 1;
                        GrdDetail.Columns["LOTCARAT"].VisibleIndex = 2;
                        GrdDetail.Columns["RATE"].VisibleIndex = 3;
                        GrdDetail.Columns["AMOUNT"].VisibleIndex = 4;
                        GrdDetail.Columns["KAPANREMARK"].VisibleIndex = 5;

                        GrdDetail.Columns["CLEAVERCODE"].Visible = false;
                        GrdDetail.Columns["CLEAVERNAME"].Visible = false;
                        GrdDetail.Columns["CLVLOTNO"].Visible = false;
                        if (StrDataType == "Orig.Rgh.Out")
                        {
                            gridColumn13.Caption = "Entry Date";
                        }
                        else
                        {
                            gridColumn13.Caption = "Kapan Date";
                        }
                    }
                    else
                    {
                        GrdDetail.Columns["KAPANCREATEDATE"].VisibleIndex = 0;
                        GrdDetail.Columns["LOTNO"].VisibleIndex = 1;
                        GrdDetail.Columns["CLVLOTNO"].VisibleIndex = 2;
                        GrdDetail.Columns["CLEAVERCODE"].VisibleIndex = 3;
                        GrdDetail.Columns["CLEAVERNAME"].VisibleIndex = 4;
                        GrdDetail.Columns["LOTCARAT"].VisibleIndex = 5;
                        GrdDetail.Columns["RATE"].VisibleIndex = 6;
                        GrdDetail.Columns["AMOUNT"].VisibleIndex = 7;
                        GrdDetail.Columns["KAPANREMARK"].VisibleIndex = 8;

                        GrdDetail.Columns["LOTCARAT"].Visible = true;
                        GrdDetail.Columns["CLEAVERCODE"].Visible = true;
                        GrdDetail.Columns["CLEAVERNAME"].Visible = true;
                        GrdDetail.Columns["CLVLOTNO"].Visible = true;
                        GrdDetail.Columns["REJECTIONDATE"].Visible = false;
                        if (StrDataType == "Orig.Rgh.Out")
                        {
                            gridColumn13.Caption = "Entry Date";
                        }
                        else
                        {
                            gridColumn13.Caption = "Kapan Date";
                        }
                    }

                    if (mFormType == FORMTYPE.INWARD)
                    {
                        pStrFormType = "PURCHASE";
                    }
                    else
                    {
                        pStrFormType = "JOBWORK";
                    }


                    DataSet DSet = Obj.GetRoughStockReport(StrOpe,
                                                      ViewType,
                                                      Val.Trim(CmbKapan.Properties.GetCheckedItems()),
                                                      StrFromDate,
                                                      StrToDate,
                                                      Val.ToString(CmbRoughType.Text), StrDataType,"", pStrFormType
                                                     );

                    if (DSet.Tables.Count > 0)
                    {
                        //DtabRoughStockSummary = DSet.Tables[0];
                        DtabRoughStockDetail = DSet.Tables[0];
                    }

                    if (DtabRoughStockDetail.Rows.Count <= 0)
                    {
                        Global.Message("No Data Found");
                        return;
                    }
                    
                    GrdDetail.BeginUpdate();
                    MainGrdDetail.DataSource = DtabRoughStockDetail;
                    GrdDetail.RefreshData();
                    GrdDetail.BestFitColumns();
                    GrdDetail.EndUpdate();
                    
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void lblExportSummary_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "RoughStockReportSummary";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrd,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [RoughStockReportSummary.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            //A.,
            //A.ISPRINTFIRMADDRESS,
            //A.ISPRINTFILTERCRITERIA,
            //A.ISPRINTHEADINGONEACHPAGE,
            //A.ISPRINTDATETIME
            // ' For Report Title

            int IntHeight = 0;
            TextBrick BrickCompany = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Black, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickCompany.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickCompany.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickCompany.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickCompany.ForeColor = Color.FromArgb(27, 66, 105);
           
            IntHeight = IntHeight + 20;
            TextBrick BrickTitle = e.Graph.DrawString("ROUGH COST REPORT", System.Drawing.Color.Navy, new RectangleF(0, IntHeight, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 9, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitle.ForeColor = Color.FromArgb(27, 66, 105);

            IntHeight = IntHeight + 20;
            string StrFilter = "From Date : " + DTPFromDate.Value.ToShortDateString() + " To " + DTPToDate.Value.ToShortDateString() + "\n";
            StrFilter = StrFilter + "Rough Type : " + Val.ToString(CmbRoughType.SelectedItem);
            TextBrick BrickTitlesParam = e.Graph.DrawString(StrFilter, System.Drawing.Color.Navy, new RectangleF(0, IntHeight, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("Verdana", 8, FontStyle.Regular);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Top;
            BrickTitlesParam.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, IntHeight, 400, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;

        }

        private void lblPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                //GrdDet.BestFitColumns();
               

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGrd;
                link.Landscape = false;
                link.PaperKind = PaperKind.A4;
                GrdDet.OptionsPrint.AutoWidth = false;

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 100;

                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                //link.PrintingSystem.Document.ScaleFactor = 0.1f;

                link.ShowPreview();
                link.PrintDlg();

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
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

        private void LblExportDetail_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "RoughStockReportDetail";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) )
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrdDetail,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [RoughStockReportDetail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void LblPrintDetail_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                GrdDetail.BestFitColumns();
                GrdDetail.OptionsPrint.AutoWidth = true;
                link.Component = MainGrdDetail;
                link.Landscape = false;
                link.PaperKind = PaperKind.A4;

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;

                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        private void GrdDetail_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouCarat = 0;
                    DouAmount = 0;
                }

                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouCarat = DouCarat + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "LOTCARAT"));
                    DouAmount = Math.Round(DouAmount + Val.Val(GrdDetail.GetRowCellValue(e.RowHandle, "AMOUNT")), 2);
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
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
    }
}
