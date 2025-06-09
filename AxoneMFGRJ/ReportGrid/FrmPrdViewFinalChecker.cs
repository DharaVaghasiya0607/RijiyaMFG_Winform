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

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmPrdViewFinalChecker : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PredictionView ObjView = new BOTRN_PredictionView();

        BOFormPer ObjPer = new BOFormPer();

        DataTable DTabPredictionView = new DataTable();

        #region Property Settings

        public FrmPrdViewFinalChecker()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            DataTable DTabRapDate = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.RAPDATE);
            DTabRapDate.DefaultView.Sort = "RAPDATE DESC";
            DTabRapDate = DTabRapDate.DefaultView.ToTable();

            CmbRapDate.Items.Clear();
            foreach (DataRow DRow in DTabRapDate.Rows)
            {
                CmbRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }
            CmbRapDate.SelectedIndex = 0;


        }


        public void ShowForm(string pStrKapanName, string pStrPacketNo, string pStrTag)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            txtKapan.Text = pStrKapanName;
            txtPacketNo.Text = pStrPacketNo;
            txtTag.Text = pStrTag;
            this.Show();
            BtnSearch_Click(null, null);
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
                if (txtKapan.Text.Trim().Length == 0)
                {
                    txtKapan.Focus();
                    return;
                }
                if (Val.Val(txtPacketNo.Text) == 0)
                {
                    txtPacketNo.Focus();
                    return;
                }
                GrdDetRowDetail.BeginUpdate();

                string StrKapan = txtKapan.Text;

                string StrPrdType = "2,10";
                string StrFromDate = null;
                string StrToDate = null;

                this.Cursor = Cursors.WaitCursor;
                DataSet DS = ObjView.DTabPredictionDataForMarker(StrKapan, Val.ToInt(txtPacketNo.Text), Val.ToInt(txtPacketNo.Text), txtTag.Text, 0, StrPrdType, StrFromDate, StrToDate);

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
            TextBrick BrickTitlesParam = e.Graph.DrawString("Packet :- " + txtKapan.Text + "-" + txtPacketNo.Text + " To " + txtKapan.Text + "-" + txtTag.Text, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtKapan.Text.Trim().Length == 0)
            {
                txtKapan.Focus();
                return;
            }
            if (Val.Val(txtPacketNo.Text) == 0)
            {
                txtPacketNo.Focus();
                return;
            }
            GrdDetRowDetail.BeginUpdate();

            string StrKapan = txtKapan.Text;
            string mStrRatDate = Val.ToString(CmbRapDate.SelectedItem);
            Int64 mEmployee_ID = BOConfiguration.gEmployeeProperty.LEDGER_ID;
            int PacketNo = Val.ToInt32(txtPacketNo.Text);
            string Tag = Val.ToString(txtTag.Text);
            string StrFromDate = null;
            string StrToDate = null;
           
            DataTable DTab = ObjView.PriceRevisedForFinalChecker(StrKapan, PacketNo, Tag, Val.SqlDate(mStrRatDate), mEmployee_ID);
            MainGridRow.DataSource = DTab;
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
        }

        private void txtPassForDisplayBack_Validating(object sender, CancelEventArgs e)
        {
            if (Val.ToString(txtPassForDisplayBack.Tag) == Val.ToString(txtPassForDisplayBack.Text))
            {
                CmbRapDate.Enabled = true;
            }
            else
            {
                CmbRapDate.Enabled = false;
            }
        }

    }
}
