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
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AxoneMFGRJ.Masters
{
    public partial class FrmMarkerProcessPredictionView : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PredictionView ObjView = new BOTRN_PredictionView();

        BOFormPer ObjPer = new BOFormPer();

        DataTable DTabPredictionView = new DataTable();

        #region Property Settings

        public FrmMarkerProcessPredictionView()
        {
            InitializeComponent();
        }

        public FORMTYPE mFormType = FORMTYPE.CLV;

        public enum FORMTYPE
        {
            CLV = 0,
            MFG = 1,
            BLOCKING = 2
        }

        public void ShowForm(FORMTYPE pFormType)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            mFormType = pFormType;

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            txtPassForEditTFlag.Tag = Val.ToString(ObjPer.PASSWORD);

            DataTable DTabPrdType = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRDTYPE);
            DTabPrdType.DefaultView.Sort = "SEQUENCENo";
            DTabPrdType = DTabPrdType.DefaultView.ToTable();

            CmbPrdType.Properties.DataSource = DTabPrdType;
            CmbPrdType.Properties.DisplayMember = "PRDTYPENAME";
            CmbPrdType.Properties.ValueMember = "PRDTYPE_ID";

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
            CmbKapan.Focus();

            if (mFormType == FORMTYPE.MFG)
                this.Text = "WORKER PROCESS PREDICTION VIEW";
            else if (mFormType == FORMTYPE.CLV)
                this.Text = "MARKER PROCESS PREDICTION VIEW";
            else if (mFormType == FORMTYPE.BLOCKING)
                this.Text = "BLOCKING PROCESS PREDICTION VIEW";

        }


        public void ShowForm(FORMTYPE pFormType, string StrKapan, int IntPacketNo, string StrTag)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            mFormType = pFormType;

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            txtPassForEditTFlag.Tag = Val.ToString(ObjPer.PASSWORD);

            DataTable DTabPrdType = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_PRDTYPE);
            DTabPrdType.DefaultView.Sort = "SEQUENCENo";
            DTabPrdType = DTabPrdType.DefaultView.ToTable();

            CmbPrdType.Properties.DataSource = DTabPrdType;
            CmbPrdType.Properties.DisplayMember = "PRDTYPENAME";
            CmbPrdType.Properties.ValueMember = "PRDTYPE_ID";

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
            CmbKapan.Focus();

            if (mFormType == FORMTYPE.MFG)
                this.Text = "WORKER PROCESS PREDICTION VIEW";
            else if (mFormType == FORMTYPE.CLV)
                this.Text = "MARKER PROCESS PREDICTION VIEW";
            else if (mFormType == FORMTYPE.BLOCKING)
                this.Text = "BLOCKING PROCESS PREDICTION VIEW";

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
                GrdDetRowDetail.BeginUpdate();

                string StrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());

                string StrPrdType = Val.Trim(CmbPrdType.Properties.GetCheckedItems());
                string StrFromDate = null;
                string StrToDate = null;
                string StrDeptType = "";

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                if (txtEmployee.Text.Trim().Length == 0)
                {
                    txtEmployee.Tag = "";
                }

                if (mFormType == FORMTYPE.CLV)
                    StrDeptType = "CLV";
                if (mFormType == FORMTYPE.MFG)
                    StrDeptType = "MFG";
                if (mFormType == FORMTYPE.BLOCKING)
                    StrDeptType = "BLOCKING";


                this.Cursor = Cursors.WaitCursor;
                DataSet DS = ObjView.DTabMarkerProcessPredictionDetail(StrDeptType, StrKapan, Val.ToInt(txtFromPacketNo.Text), Val.ToInt(txtToPacketNo.Text), txtTag.Text, Val.ToInt64(txtEmployee.Tag), StrPrdType, StrFromDate, StrToDate);

                //DS.Tables[0].DefaultView.Sort = "KAPANNAME,PACKETNO,TAG,PRDSEQUENCENO,EMPLOYEE_ID,PLANNO";

                MainGridRow.DataSource = DS.Tables[0];
                MainGridRow.Refresh();

                GrdDetRowDetail.Columns["GROUPPACKETTAG"].Visible = false;


                if (ChkUnGroup.Checked == false)
                {
                    GrdDetRowDetail.Columns["GROUPPACKETTAG"].Group();

                    GrdDetRowDetail.ExpandAllGroups();
                    GrdDetRowDetail.BestFitColumns();
                }
                else
                {
                    GrdDetRowDetail.Columns["GROUPPACKETTAG"].UnGroup();
                }


                GrdDetRowDetail.EndUpdate();
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
                    PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                    {
                        PrintingSystemBase = new PrintingSystemBase(),
                        Component = MainGridRow,
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
            TextBrick BrickTitlesParam = e.Graph.DrawString("Packet :- " + CmbKapan.Properties.GetCheckedItems() + "-" + txtFromPacketNo.Text + " To " + txtToPacketNo.Text + "-" + txtTag.Text, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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
                Global.Message(ex.Message);
            }
        }

        private void GrdDetRowDetail_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            bool BoolFinal = Val.ToBoolean(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "ISFINAL"));
            bool BoolTFlag = Val.ToBoolean(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "TFLAG"));
            bool BoolDFlag = Val.ToBoolean(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "DFLAG"));
            bool BoolMaxFlag = Val.ToBoolean(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "MAXAMTFLAG"));

            if (BoolMaxFlag == true)
            {
                e.Appearance.BackColor = lblSMFFlag.BackColor;
            }

            if (BoolTFlag == true)
            {
                e.Appearance.BackColor = lblTFlag.BackColor;
            }
            else if (BoolDFlag == true)
            {
                e.Appearance.BackColor = lblDFlag.BackColor;
            }
            else if (BoolFinal == true)
            {
                e.Appearance.BackColor = lblISFinal.BackColor;
            }


        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGridRow) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGridRow.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "EMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "EMPLOYEENAME")));
                    return;
                }

            }
            finally
            {
                e.Info = info;
            }
        }

        private void GrdDetRowDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)  //Add : Pinali : 26-09-2019
        {
            try
            {
                if (GrdDetRowDetail.FocusedRowHandle < 0)
                {
                    return;
                }
                DataRow DRow = GrdDetRowDetail.GetFocusedDataRow();

                if ((Val.ToString(DRow["PRDTYPE_ID"]) == "2" || Val.ToString(DRow["PRDTYPE_ID"]) == "10") && Val.ToString(txtPassForEditTFlag.Tag).ToUpper() == txtPassForEditTFlag.Text.ToUpper())
                    GrdDetRowDetail.Columns["TFLAG"].OptionsColumn.AllowEdit = true;
                else
                    GrdDetRowDetail.Columns["TFLAG"].OptionsColumn.AllowEdit = false;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDetRowDetail_CellMerge(object sender, CellMergeEventArgs e)
        {
            try
            {
                string MergeOnStr = "MERGEKAPANPKT";
                if (e.Column.FieldName.Contains("SUMAMOUNT"))
                {
                    string val1 = Val.ToString(GrdDetRowDetail.GetRowCellValue(e.RowHandle1, GrdDetRowDetail.Columns[MergeOnStr]));
                    string val2 = Val.ToString(GrdDetRowDetail.GetRowCellValue(e.RowHandle2, GrdDetRowDetail.Columns[MergeOnStr]));
                    if (val1 == val2)
                        e.Merge = true;
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

            }

        }


    }
}
