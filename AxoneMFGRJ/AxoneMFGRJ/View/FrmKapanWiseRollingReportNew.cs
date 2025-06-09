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
using DevExpress.XtraEditors.Repository;


namespace AxoneMFGRJ.View
{
    public partial class FrmKapanWiseRollingReportNew : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();
        DataTable DtabKapanRollingSummary = new DataTable();
        DataTable DtabKapanRollingDetail = new DataTable();


        public FrmKapanWiseRollingReportNew()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            ChkCmbDept.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);
            ChkCmbDept.Properties.DisplayMember = "DEPARTMENTNAME";
            ChkCmbDept.Properties.ValueMember = "DEPARTMENT_ID";

            ChkCmbParty.Properties.DataSource = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
            ChkCmbParty.Properties.DisplayMember = "EMPLOYEENAME";
            ChkCmbParty.Properties.ValueMember = "EMPLOYEE_ID";

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
                string StrStone = "";
                string ViewType = "";
                MainGrd.DataSource = null;
                GrdDet.Columns.Clear();


                if (RbtAll.Checked == true)
                {
                    StrStone = "ALL";
                }
                else if (RbtOriginal.Checked == true)
                {
                    StrStone = "ORI";
                }
                else if (RbtRepairing.Checked == true)
                {
                    StrStone = "REP";
                }


                if (RbtPcs.Checked == true)
                {
                    StrOpe = "PCS";
                }
                else if (RdbCts.Checked == true)
                {
                    StrOpe = "CARAT";
                }                
                else if (RbtBoth.Checked == true)
                {
                    StrOpe = "BOTH";
                }

                if (RbtnDeptWise.Checked == true)
                {
                    ViewType = "DEPTWISE";
                }
                else if (RbtnProcessWise.Checked == true)
                {
                    ViewType = "PROCESSWISE";
                }
                else if (RbtnMngrWise.Checked == true)
                {
                    ViewType = "MANAGERWISE";
                }

                else if (RbtEmployee.Checked == true)//GUNJAN :20-02-2023
                {
                    ViewType = "EMPLOYEE";
                }


                DataSet DSet = Obj.GetKapanWiseRollingReportDataSummary(StrOpe,
                                                                            ViewType,
                                                                            Val.Trim(CmbKapan.Properties.GetCheckedItems()),
                                                                            Val.ToInt(txtFromPacketNo.Text),
                                                                            Val.ToInt(txtToPacketNo.Text),
                                                                            Val.ToString(txtTag.Text),
                                                                            Val.Trim(ChkCmbDept.Properties.GetCheckedItems()),
                                                                            Val.ToBoolean(ChkWithBombayStock.Checked),
                                                                            StrStone,
                                                                            Val.Trim(ChkCmbParty.Properties.GetCheckedItems())
                                                                           );

                //DtabKapanRollingSummary = Obj.GetKapanWiseRollingReportDataSummary(StrOpe,
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
                    DtabKapanRollingSummary = DSet.Tables[0];
                    DtabKapanRollingDetail = DSet.Tables[1];
                }

                if (DtabKapanRollingSummary.Rows.Count <= 0)
                {
                    Global.Message("No Data Found");
                    return;
                }
                GrdDet.BeginUpdate();
                MainGrd.DataSource = DtabKapanRollingSummary;
                GrdDet.RefreshData();

                MainGrd.RepositoryItems.Clear();
                for (int IntI = 0; IntI < GrdDet.Columns.Count; IntI++)
                {
                    RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();

                    memoEdit.ReadOnly = true;
                    memoEdit.AutoHeight = true;
                    memoEdit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap ;
                    memoEdit.WordWrap = true;
                    GrdDet.GridControl.RepositoryItems.Add(memoEdit);

                    GrdDet.Columns[IntI].ColumnEdit = memoEdit;
                }

               

                GrdDet.EndUpdate();
                GrdDet.Columns["KAPAN_ID"].Visible = false;

                //GrdDet.Columns["Kapan"].AppearanceCell.BackColor = Color.LightGray;
                //GrdDet.Columns["Kapan"].AppearanceCell.BackColor2 = Color.LightGray;
                //GrdDet.Columns["Kapan"].AppearanceCell.Font= Font("Verdana", 8, FontStyle.Bold);
                //GrdDet.Columns["Kapan"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                foreach (DevExpress.XtraGrid.Columns.GridColumn Col in GrdDet.Columns)
                {
                    Col.Width = 85;
                }

                //BtnBestFit_Click(null, null);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
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
                string StrKapanname = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "Kapan"));

                if (StrKapanname.ToUpper().Contains("TOTAL") || Val.ToString(e.Column.FieldName).ToUpper().Contains("TOTAL"))
                {
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkBlue;
                    e.Appearance.BackColor = Color.LightGray;
                }

                if (e.Column.FieldName == "Kapan")
                {
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LightGray;
                    e.Appearance.BackColor2 = Color.LightGray;
                }

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
                if (e.RowHandle < 0 || e.Column.FieldName == "EMPLOYEECODE")
                {
                    return;
                }


                if (e.Clicks == 2)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string StrReportTitle = "";

                    DataRow Dr = GrdDet.GetFocusedDataRow();

                    Int64 IntKapan_ID = 0;

                    if (!Val.ToString(Dr["Kapan"]).ToUpper().Contains("TOTAL"))
                    {
                        IntKapan_ID = Val.ToInt64(Dr["KAPAN_ID"]);
                    }

                    //string StrOpe = "";
                    //string ViewType = "";
                    //if (RbtPcs.Checked == true)
                    //{
                    //    StrOpe = "PCS";
                    //}
                    //else if (RdbCts.Checked == true)
                    //{
                    //    StrOpe = "CARAT";
                    //}
                    //else if (RbtBoth.Checked == true)
                    //{
                    //    StrOpe = "BOTH";
                    //}

                    //if (RbtnDeptWise.Checked == true)
                    //{
                    //    ViewType = "DEPTWISE";
                    //}
                    //else if (RbtnProcessWise.Checked == true)
                    //{
                    //    ViewType = "PROCESSWISE";
                    //}
                    //else if (RbtnMngrWise.Checked == true)
                    //{
                    //    ViewType = "MANAGERWISE";
                    //}

                    //string StrKapan = Val.ToString(Val.ToString(Dr["Kapan"]));


                    //DataTable DtabRollingDetail = Obj.GetKapanWiseRollingReportDataSummary(StrOpe,
                    //                                                                           ViewType,
                    //                                                                           StrKapan,
                    //                                                                           Val.ToInt(txtFromPacketNo.Text),
                    //                                                                           Val.ToInt(txtToPacketNo.Text),
                    //                                                                           Val.ToString(txtTag.Text),
                    //                                                                           Val.Trim(ChkCmbDept.Properties.GetCheckedItems()),
                    //                                                                           Val.ToBoolean(ChkWithBombayStock.Checked)
                    //                                                                          );




                    string StrQuery = "";
                    string StrColName = e.Column.FieldName;
                    string StrKapan = Val.ToString(Dr["Kapan"]).ToUpper().Contains("TOTAL") ? "" : Val.ToString(Dr["KAPAN_ID"]);

                    StrQuery = StrKapan == "" ? "1=1" : "KAPAN_ID = '" + StrKapan + "' ";

                    if (RbtnDeptWise.Checked && !StrColName.ToUpper().Contains("TOTAL"))
                    {
                        StrQuery = StrQuery + "AND ToDepartment = '" + StrColName + "'";
                    }
                    else if (RbtnProcessWise.Checked && !StrColName.ToUpper().Contains("TOTAL"))
                    {
                        StrQuery = StrQuery + "AND ToProcess = '" + StrColName + "'";
                    }
                    else if (!StrColName.ToUpper().Contains("TOTAL"))
                    {
                        StrQuery = StrQuery + "AND MANAGERCODE = '" + StrColName + "'";
                    }

                    DataRow[] UDRow = DtabKapanRollingDetail.Select(StrQuery);
                    if (UDRow != null)
                    {
                        GrdDetPacketDetail.BeginUpdate();
                        StrReportTitle = Val.ToString(Dr["Kapan"]) + " : " + StrColName; //+ " Stock";
                        GrpPacketSearch.Text = StrReportTitle;
                        MainGridPacketDetail.DataSource = UDRow.CopyToDataTable();
                        GrdDetPacketDetail.RefreshData();
                        GrdDetPacketDetail.BestFitColumns();
                        GrdDetPacketDetail.EndUpdate();
                    }

                    //StrReportTitle = Val.ToString(Dr["Kapan"]) + " : " + StrProcessName; //+ " Stock";
                    //GrpPacketSearch.Text = StrReportTitle;
                    //MainGridPacketDetail.DataSource = DtabRollingDetail;
                    //GrdDetPacketDetail.RefreshData();
                    //GrdDetPacketDetail.BestFitColumns();
                    //GrdDetPacketDetail.EndUpdate();

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void lblExportSummary_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "KapanWiseRollingReport.xlsx";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    GrdDet.Appearance.Row.Font = new Font("Verdana", 8.25f);
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGrd,
                        Landscape = true,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderAreaSummary);

                    link.ExportToXlsx(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [KapanWiseRollingReport.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }
                }
                GrdDet.Appearance.Row.Font = new Font("Verdana", 8);
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
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
            TextBrick BrickTitleseller = e.Graph.DrawString("KAPANWISE ROLLING SUMMARY", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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

        private void lblPktExport_Click(object sender, EventArgs e)
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
                        Component = MainGridPacketDetail,
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
        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
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
            TextBrick BrickTitlesParam = e.Graph.DrawString("Kapan Rolling AsOnDate :- " + DateTime.Now.ToString("dd/MM/yyyy"), System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        private void lblPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {

                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                //string Str = txtEmployee.Text.Trim().Length == 0 ? "All Kapan's" : txtEmployee.Text;

                link.Component = MainGrd;
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
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
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

        private void lblPktPrint_Click(object sender, EventArgs e)
        {
             try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGridPacketDetail;
                link.Landscape = true;


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
        

    }
}
