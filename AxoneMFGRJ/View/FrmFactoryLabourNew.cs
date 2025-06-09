using AxoneMFGRJ.Report;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.View
{
    public partial class FrmFactoryLabourNew : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabProcess = new DataTable();
        DataTable DTabDetail = new DataTable();
        DataTable DTabSummary = new DataTable();
        DataTable DTabManagerWise = new DataTable();

        DataTable DTabFactoryPoductionDetail = new DataTable();

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        string mStrReportTitle = "";

        public FrmFactoryLabourNew()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            CmbKapan.Properties.DataSource = ObjPacket.FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            DTabProcess = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);

            foreach (DataRow DRow in DTabProcess.Rows)
            {
                if (Val.ToInt(DRow["PROCESS_ID"]) == 531)
                {
                    txtProcess.Text = Val.ToString(DRow["PROCESSNAME"]);
                    txtProcess.Tag = Val.ToString(DRow["PROCESS_ID"]);
                    break;
                }
            }

            DTPFromDate.Value = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            DTPToDate.Value = System.DateTime.Now;


            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDetSummary.Name);

            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDetSummary.RestoreLayoutFromStream(stream);

            }

            Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);

            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDet.RestoreLayoutFromStream(stream);
            }
        }

        private void AttachFormDefaultEvent()
        {
            Val.FormGeneralSetting(this);
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjPacket);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtProcess.Text.Trim().Length == 0)
                {
                    Global.MessageError("Process Name Is Required");
                    txtProcess.Focus();
                    return;
                }

                if (txtEmployee.Text.Trim().Length == 0)
                {
                    txtEmployee.Tag = "";
                }

                this.Cursor = Cursors.WaitCursor;

                string StrOpe = "";
                if (RbtAll.Checked == true) StrOpe = "ALL";
                else if (RbtJobWork.Checked == true) StrOpe = "JOBWORK";
                else if (RbtWorker.Checked == true) StrOpe = "EMPLOYEE";

                if (DTabDetail.Rows.Count > 0)
                {
                    DTabDetail.Rows.Clear();
                    DTabFactoryPoductionDetail.Rows.Clear();
                }

                if (DTabSummary.Rows.Count > 0)
                DTabSummary.Rows.Clear();

                PicEmpPhoto.Image = null;
                txtEmpCode.Text = string.Empty;

                DataSet DS = Obj.GetFactoryProductionLabour(StrOpe, Val.Trim(CmbKapan.Properties.GetCheckedItems()), Val.ToInt(txtProcess.Tag), Val.ToInt64(txtEmployee.Tag), Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()));


                DTabDetail = DS.Tables[0];
                DTabSummary = DS.Tables[1];

                DTabManagerWise = DS.Tables[2];





                DTabFactoryPoductionDetail = DS.Tables[0]; //Used For Save Factory Labour Production Data

                GrdDet.BeginUpdate();
                GrdDetSummary.BeginUpdate();

                MainGridDetail.DataSource = DTabDetail;
                GrdDet.RefreshData();
                GrdDet.BestFitColumns();
                GrdDet.Columns["WORKERCODE"].ClearFilter();

                MainGrdManagerWise.DataSource = DTabManagerWise;
                GrdDetManagerWise.RefreshData();
                GrdDetManagerWise.BestFitColumns();


                MainGridSummary.DataSource = DTabSummary;
                GrdDetSummary.RefreshData();
                GrdDetSummary.BestFitColumns();

                txtPass_TextChanged(null, null);

                GrdDet.EndUpdate();
                GrdDetSummary.EndUpdate();

                this.Cursor = Cursors.Default;

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

            string StrFilter = "FromDate : " + DTPFromDate.Text + " To " + DTPToDate.Text;
            StrFilter = StrFilter + ", Process : " + txtProcess.Text;
            //// ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString(StrFilter, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;


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


        public void CommonPrintFuction(
           DevExpress.XtraGrid.GridControl pMainGrid,
           DevExpress.XtraGrid.Views.BandedGrid.BandedGridView pGrdDet

           )
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = pMainGrid;
                link.Landscape = false;

                //GrdDetDept.BestFitColumns();
                //GrdDetDept.OptionsPrint.AutoWidth = true;

                //foreach (System.Drawing.Printing.PaperKind foo in Enum.GetValues(typeof(System.Drawing.Printing.PaperKind)))
                //{
                //    if (Val.ToString(CmbPageKind.SelectedItem) == foo.ToString())
                //    {
                //        link.PaperKind = foo;
                //        link.PaperName = foo.ToString();

                //    }
                //}

                //}
                //if (Val.ToString(cmbExpand.SelectedItem) == "Yes")
                //{
                //    GridView1.OptionsPrint.ExpandAllGroups = true;
                //}
                //else
                //{
                //    GridView1.OptionsPrint.ExpandAllGroups = false;
                //}

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

        public void CommonExcelExportFuction(
           DevExpress.XtraGrid.GridControl pMainGrid,
           DevExpress.XtraGrid.Views.BandedGrid.BandedGridView pGrdDet,
            string pStrFileName

           )
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = pStrFileName;
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = pMainGrid,
                        Landscape = false,
                        PaperKind = PaperKind.A4,
                        Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                    };

                    link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    link.ExportToXls(svDialog.FileName);

                    if (Global.Confirm("Do You Want To Open [" + pStrFileName + ".xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void txtProcess_KeyPress(object sender, KeyPressEventArgs e)
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
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = DTabProcess;
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
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

        private void FrmFactoryProduction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BtnRefresh_Click(null, null);
            }
        }

        private void lblPacketExport_Click(object sender, EventArgs e)
        {

        }

        private void lblPacketPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Factory Production Labour Detail (" + Str + ")";
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGridDetail;
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

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGridDetail) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGridDetail.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "WORKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "WORKERNAME")));
                    return;
                }

                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "WLABOURAMOUNT")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "WLABOURREMARK")));
                    return;
                }

                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "GRDLABOURAMOUNT" || hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "GRDLABOURRATE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "GRDLABOURREMARK")));
                    return;
                }

                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "BYLABOURAMOUNT" || hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "BYLABOURRATE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "BYLABOURREMARK")));
                    return;
                }

                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "LABLABOURAMOUNT" || hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "LABLABOURRATE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "LABLABOURREMARK")));
                    return;
                }

                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FINAL_LABOURAMOUNT" || hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FINAL_LABOURRATE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FINAL_LABOURREMARK")));
                    return;
                }
            }
            finally
            {
                e.Info = info;
            }
        }

        private void BtnLiveUpdate_Click(object sender, EventArgs e)
        {

            try
            {

                if (txtProcess.Text.Trim().Length == 0)
                {
                    Global.MessageError("Process Name Is Required");
                    txtProcess.Focus();
                    return;
                }

                if (txtEmployee.Text.Trim().Length == 0)
                {
                    txtEmployee.Tag = "";
                }

                if (Global.Confirm("Are You Sure To Update Live Status Becuase It Takes Some Time") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                int IntRes = Obj.UpdateLiveGradingColorStatus(Val.Trim(CmbKapan.Properties.GetCheckedItems()), Val.ToInt(txtProcess.Tag), Val.ToInt64(txtEmployee.Tag), Val.SqlDate(DTPFromDate.Value.ToShortDateString()), Val.SqlDate(DTPToDate.Value.ToShortDateString()));

                this.Cursor = Cursors.Default;
                if (IntRes != -1)
                {
                    Global.Message("Running Grading Color Is Updated To Status Is Update Successfully");
                }
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }

        }

        private void MainGridDetail_Paint(object sender, PaintEventArgs e)
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

        private void GrdDet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            int IsMinusLossCaratInIssueCarat = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "ISMINUSLOSSCARATINISSUECARAT"));

            if (e.Column.FieldName == "WSHPCODE")
            {
                string StrMakShape = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "BSHPCODE"));
                string StrShape = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "WSHPCODE"));
                if (StrMakShape != "" && StrShape != "" && StrMakShape != StrShape)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
            }

            if (e.Column.FieldName == "WCLACODE")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "BCLASEQNO"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "WCLASEQNO"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == "WCUTCODE")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "BCUTSEQNO"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "WCUTSEQNO"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == "WPOLCODE")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "BPOLSEQNO"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "WPOLSEQNO"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == "WSYMCODE")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "BSYMSEQNO"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "WSYMSEQNO"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }


            if (e.Column.FieldName == "WGCUTCODE")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "BCUTSEQNO"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "WGCUTSEQNO"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == "WGPOLCODE")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "BPOLSEQNO"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "WGPOLSEQNO"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }
            if (e.Column.FieldName == "WGSYMCODE")
            {
                int IntMakSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "BSYMSEQNO"));
                int IntSeqNo = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "WGSYMSEQNO"));
                if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo > IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
                else if (IntSeqNo != 0 && IntMakSeqNo != 0 && IntSeqNo < IntMakSeqNo)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
            }

            if (e.Column.FieldName == "WCARAT")
            {
                double DouMakCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BCARAT"));
                double DouCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "WCARAT"));
                if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
                else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
            }
            if (e.Column.FieldName == "WAMOUNT")
            {
                double DouMakCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BAMOUNT"));
                double DouCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "WAMOUNT"));
                if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
                else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
            }
            if (e.Column.FieldName == "WGCARAT")
            {
                double DouMakCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BCARAT"));
                double DouCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "WGCARAT"));
                if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
                else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
            }
            if (e.Column.FieldName == "WGAMOUNT")
            {
                double DouMakCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "BAMOUNT"));
                double DouCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "WGAMOUNT"));
                if (DouCarat != 0 && DouMakCarat != 0 && DouCarat > DouMakCarat)
                {
                    e.Appearance.BackColor = lblDown.BackColor;
                }
                else if (DouCarat != 0 && DouMakCarat != 0 && DouCarat < DouMakCarat)
                {
                    e.Appearance.BackColor = lblUp.BackColor;
                }
            }

            if (e.Column.FieldName == "GRDLABOURAMOUNT")
            {
                string StrRemark = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "GRDLABOURREMARK"));
                if (StrRemark != "")
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LemonChiffon;
                }
            }
            if (e.Column.FieldName == "GRDLABOURRATE")
            {
                string StrRemark = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "GRDLABOURREMARK"));
                if (StrRemark != "")
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LemonChiffon;
                }
            }
            if (e.Column.FieldName == "BYLABOURAMOUNT")
            {
                string StrRemark = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "BYLABOURREMARK"));
                if (StrRemark != "")
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LemonChiffon;
                }
            }
            if (e.Column.FieldName == "BYLABOURRATE")
            {
                string StrRemark = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "BYLABOURREMARK"));
                if (StrRemark != "")
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LemonChiffon;
                }
            }
            if (e.Column.FieldName == "LABLABOURAMOUNT")
            {
                string StrRemark = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "LABLABOURREMARK"));
                if (StrRemark != "")
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LemonChiffon;
                }
            }
            if (e.Column.FieldName == "LABLABOURRATE")
            {
                string StrRemark = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "LABLABOURREMARK"));
                if (StrRemark != "")
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LemonChiffon;
                }
            }

            if (e.Column.FieldName == "FINAL_LABOURAMOUNT")
            {
                string StrRemark = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "FINAL_LABOURREMARK"));
                if (StrRemark != "")
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LemonChiffon;
                }
            }
            if (e.Column.FieldName == "FINAL_LABOURRATE")
            {
                string StrRemark = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "FINAL_LABOURREMARK"));
                if (StrRemark != "")
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LemonChiffon;
                }
            }


            if (Val.ToString(e.Column.FieldName).Trim().ToUpper() == "ISSUECARAT" && Val.ToInt(IsMinusLossCaratInIssueCarat) == 1)
            {
                //e.Appearance.ForeColor = Color.FromArgb(195,51,185);
                e.Appearance.ForeColor = Color.FromArgb(204, 42, 169);
            }
        }

        private void lblExportSummary_Click(object sender, EventArgs e)
        {

        }

        private void lblPrintSummary_Click(object sender, EventArgs e)
        {
            try
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Factory Production Summary (" + Str + ")";
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = MainGridSummary;
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

        private void GrdDetSummary_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;

            }
            if (e.Clicks == 2)
            {
                this.Cursor = Cursors.WaitCursor;
                GrdDet.Columns["WORKERCODE"].ClearFilter();
                GrdDet.Columns["WORKERCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("WORKERCODE='" + Val.ToString(GrdDetSummary.GetRowCellValue(e.RowHandle, "WORKERCODE")) + "'");
                
                byte[] OFFICELOGO = GrdDetSummary.GetRowCellValue(e.RowHandle, "EMPPHOTO") as byte[] ?? null;
                if (OFFICELOGO != null)
                {
                    using (MemoryStream ms = new MemoryStream(OFFICELOGO))
                    {
                        PicEmpPhoto.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    PicEmpPhoto.Image = null;
                }

                txtEmpCode.Text = Val.ToString(GrdDetSummary.GetRowCellValue(e.RowHandle, "WORKERCODE"));
                xtraTabControl1.SelectedTabPageIndex = 1;
                this.Cursor = Cursors.Default;
            }

        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDetSummary.BestFitColumns();
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void toolTipController2_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {

        }

        private void GrdDetSummary_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrdDet_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0)
            {
                DataRow DR = GrdDet.GetDataRow(e.RowHandle);

                if(Val.ToString(DR["WAGESBASE"]) == "REP")
                {
                    return;
                }

                string StrKapan = Val.ToString(DR["KAPANNAME"]);
                int IntPacketNO = Val.ToInt(DR["PACKETNO"]);
                string StrTag = Val.ToString(DR["TAG"]);
                string StrPacketID = Val.ToString(DR["PACKET_ID"]);
                string StrWorkerName = Val.ToString(DR["WORKERCODE"]);
                Int64 IntWorkerID = Val.ToInt64(DR["WORKER_ID"]);

                FrmFactoryLabourDetail FrmFactoryLabourDetail = new FrmFactoryLabourDetail();
                FrmFactoryLabourDetail.MdiParent = Global.gMainRef;
                FrmFactoryLabourDetail.ShowForm(StrKapan, IntPacketNO, StrTag, StrPacketID, StrWorkerName, IntWorkerID, "FACTORY");
                ObjFormEvent.ObjToDisposeList.Add(FrmFactoryLabourDetail);
            }
        }

        private void CmbWagesBase_EditValueChanged(object sender, EventArgs e)
        {
            if (GrdDet.FocusedRowHandle >= 0)
            {
                GrdDet.PostEditor();
                string TrnID = Val.ToString(GrdDet.GetFocusedRowCellValue("TRN_ID"));
                string WagesBase = Val.ToString(GrdDet.GetFocusedRowCellValue("WAGESBASE"));

                int IntRes = Obj.UpdateLiveGradingColorFlag(TrnID, WagesBase);
                this.Cursor = Cursors.Default;
                if (IntRes == -1)
                {
                    Global.MessageError("Opps....Something Goes Wrong");
                }
            }

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (txtPass.Text != "" && txtPass.Tag.ToString().ToUpper() == txtPass.Text.ToString().ToUpper())
            {
                GrdDet.Columns["WAGESBASE"].OptionsColumn.AllowEdit = true;
                CmbWagesBase.ReadOnly = false;
                GrdDet.Columns["WAGESBASE"].ColumnEdit = CmbWagesBase;

                //Add : #P : 24-01-2019
                GrdDetSummary.Columns["GRDPLUSDOLLAR"].Visible = true;
                GrdDetSummary.Columns["GRDMINUSDOLLAR"].Visible = true;
                GrdDetSummary.Columns["BYPLUSDOLLAR"].Visible = true;
                GrdDetSummary.Columns["BYMINUSDOLLAR"].Visible = true;
                GrdDetSummary.Columns["LABPLUSDOLLAR"].Visible = true;
                GrdDetSummary.Columns["LABMINUSDOLLAR"].Visible = true;
                GrdDetSummary.Columns["POLCHKPLUSDOLLAR"].Visible = true;
                GrdDetSummary.Columns["POLCHKMINUSDOLLAR"].Visible = true;
                GrdDetSummary.Columns["FINAL_PLUSDOLLAR"].Visible = true;
                GrdDetSummary.Columns["FINAL_MINUSDOLLAR"].Visible = true;

                GrdDet.Columns["GRDPLUSDOLLAR"].Visible = true;
                GrdDet.Columns["GRDMINUSDOLLAR"].Visible = true;
                GrdDet.Columns["BYPLUSDOLLAR"].Visible = true;
                GrdDet.Columns["BYMINUSDOLLAR"].Visible = true;
                GrdDet.Columns["LABPLUSDOLLAR"].Visible = true;
                GrdDet.Columns["LABMINUSDOLLAR"].Visible = true;
                GrdDet.Columns["POLCHKPLUSDOLLAR"].Visible = true;
                GrdDet.Columns["POLCHKMINUSDOLLAR"].Visible = true;
                GrdDet.Columns["FINAL_PLUSDOLLAR"].Visible = true;
                GrdDet.Columns["FINAL_MINUSDOLLAR"].Visible = true;
                //End : #P : 24-01-2019
            }
            else
            {
                GrdDet.Columns["WAGESBASE"].OptionsColumn.AllowEdit = false;
                CmbWagesBase.ReadOnly = true;
                GrdDet.Columns["WAGESBASE"].ColumnEdit = null;

                //Add : #P : 24-01-2019
                GrdDetSummary.Columns["GRDPLUSDOLLAR"].Visible = false;
                GrdDetSummary.Columns["GRDMINUSDOLLAR"].Visible = false;
                GrdDetSummary.Columns["BYPLUSDOLLAR"].Visible = false;
                GrdDetSummary.Columns["BYMINUSDOLLAR"].Visible = false;
                GrdDetSummary.Columns["LABPLUSDOLLAR"].Visible = false;
                GrdDetSummary.Columns["LABMINUSDOLLAR"].Visible = false;
                GrdDetSummary.Columns["POLCHKPLUSDOLLAR"].Visible = false;
                GrdDetSummary.Columns["POLCHKMINUSDOLLAR"].Visible = false;
                GrdDetSummary.Columns["FINAL_PLUSDOLLAR"].Visible = false;
                GrdDetSummary.Columns["FINAL_MINUSDOLLAR"].Visible = false;

                GrdDet.Columns["GRDPLUSDOLLAR"].Visible = false;
                GrdDet.Columns["GRDMINUSDOLLAR"].Visible = false;
                GrdDet.Columns["BYPLUSDOLLAR"].Visible = false;
                GrdDet.Columns["BYMINUSDOLLAR"].Visible = false;
                GrdDet.Columns["LABPLUSDOLLAR"].Visible = false;
                GrdDet.Columns["LABMINUSDOLLAR"].Visible = false;
                GrdDet.Columns["POLCHKPLUSDOLLAR"].Visible = false;
                GrdDet.Columns["POLCHKMINUSDOLLAR"].Visible = false;
                GrdDet.Columns["FINAL_PLUSDOLLAR"].Visible = false;
                GrdDet.Columns["FINAL_MINUSDOLLAR"].Visible = false;
                //End : #P : 24-01-2019
            }
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Factory Production Labour Summary (" + Str + ")";
                try
                {
                    SaveFileDialog svDialog = new SaveFileDialog();
                    svDialog.DefaultExt = ".xlsx";
                    svDialog.Title = "Export to Excel";
                    svDialog.FileName = "LabourSummary.xlsx";
                    svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                    {
                        PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                        {
                            PrintingSystemBase = new PrintingSystemBase(),
                            Component = MainGridSummary,
                            Landscape = true,
                            PaperKind = PaperKind.A4,
                            Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                        };

                        link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                        link.ExportToXlsx(svDialog.FileName);

                        if (Global.Confirm("Do You Want To Open [PolishOkDetail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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
            else
            {
                string Str = txtEmployee.Text.Trim().Length == 0 ? "All Worker" : txtEmployee.Text;
                mStrReportTitle = "Factory Production Labour Detail (" + Str + ")";
                try
                {
                    SaveFileDialog svDialog = new SaveFileDialog();
                    svDialog.DefaultExt = ".xlsx";
                    svDialog.Title = "Export to Excel";
                    svDialog.FileName = "LabourDetail.xlsx";
                    svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                    {
                        PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                        {
                            PrintingSystemBase = new PrintingSystemBase(),
                            Component = MainGridDetail,
                            Landscape = true,
                            PaperKind = PaperKind.A4,
                            Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                        };

                        link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                        link.ExportToXlsx(svDialog.FileName);

                        if (Global.Confirm("Do You Want To Open [PolishOkDetail.xlsx] ?") == System.Windows.Forms.DialogResult.Yes)
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

        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (DTabDetail == null || DTabDetail.Rows.Count == 0)
            {
                Global.MessageError("There Is No Data For Print");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.MdiParent = Global.gMainRef;
            FrmReportViewer.ShowForm("WorkerLabourReport", DTabDetail);

            this.Cursor = Cursors.Default;
        }

        private void lblSaveLayoutSummary_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdDetSummary.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDetSummary.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblDeleteLayoutSummary_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDetSummary.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void lblSaveLayoutDetail_Click(object sender, EventArgs e)
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

        private void lblDeleteLayoutDetail_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDet.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void BtnSaveFLabourData_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTabFactoryPoductionDetail.Rows.Count <= 0)
                    return;


                if (Global.Confirm("Are You Sure You Want To Save Factory Labour Data...?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DataTable DTFinal = new DataTable();
                DTFinal = DTabFactoryPoductionDetail.Clone();

                for (int ICount = 0; ICount < DTabFactoryPoductionDetail.Rows.Count; ICount++)
                {
                    DataRow DRow = DTFinal.NewRow();
                    DataRow DrCopy = DTabFactoryPoductionDetail.Rows[ICount];

                    foreach (DataColumn Col in DTabFactoryPoductionDetail.Columns)
                    {
                        if (DTFinal.Columns.Contains(Col.ColumnName))
                        {
                            try
                            {
                                DRow[Col.ColumnName] = DrCopy[Col.ColumnName];
                            }
                            catch
                            {

                            }
                        }
                    }
                    DTFinal.Rows.Add(DRow);
                }

                DataTable DtabProduction = Obj.StoreLabourProductionData("DISPLAY", null);

                string[] columnNames = (from dc in DtabProduction.Columns.Cast<DataColumn>()
                                        select dc.ColumnName).ToArray();
                string[] columnNames2 = (from dc in DTFinal.Columns.Cast<DataColumn>()
                                         select dc.ColumnName).ToArray();

                for (int i = 0; i < columnNames2.Length; i++)
                {
                    if (!columnNames.Contains(Val.ToString(columnNames2[i])))
                    {
                        string str = Val.ToString(columnNames2[i]);
                        DTFinal.Columns.Remove(columnNames2[i]);
                    }
                }
                int IntC = DTFinal.Columns.Count;

                DataTable DT = Obj.StoreLabourProductionData("STORE", DTFinal);
                if (DT.Rows.Count > 0)
                {
                    Global.Message("Factory Labour Production Data Store Successfully");
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }

        }

        private void GrdDetSummary_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                int IsMinusLossCaratInIssueCarat = Val.ToInt(GrdDetSummary.GetRowCellValue(e.RowHandle, "ISMINUSLOSSCARATINISSUECARAT"));

                int IntWorkerCode = Val.ToInt(GrdDetSummary.GetRowCellValue(e.RowHandle, "WORKERCODE"));

                if (Val.ToString(e.Column.FieldName).Trim().ToUpper() == "ISSUECARAT" && Val.ToInt(IsMinusLossCaratInIssueCarat) == 1)
                {
                    e.Appearance.ForeColor = Color.FromArgb(204, 42, 169);
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDetManagerWise_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (e.Clicks == 2)
            {
                this.Cursor = Cursors.WaitCursor;
                GrdDetSummary.Columns["MANAGERCODE"].ClearFilter();
                GrdDetSummary.Columns["MANAGERCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("MANAGERCODE='" + Val.ToString(GrdDetManagerWise.GetRowCellValue(e.RowHandle, "MANAGERCODE")) + "'");

                PicEmpPhoto.Image = null;
                txtEmpCode.Text = string.Empty;

                xtraTabControl1.SelectedTabPageIndex = 0;
                this.Cursor = Cursors.Default;


            }
        }

        private void MainGrdManagerWise_Paint(object sender, PaintEventArgs e)
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

        private void GrdDetManagerWise_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
            }
        }
    }
}
