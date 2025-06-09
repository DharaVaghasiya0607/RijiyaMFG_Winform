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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmPrdViewManagement : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PredictionView ObjView = new BOTRN_PredictionView();

        DataTable DTabPredictionView = new DataTable();

        #region Property Settings

        public FrmPrdViewManagement()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {   
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            txtKapanName.Focus();
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
                if (txtKapanName.Text.Trim().Length == 0)
                {
                    Global.Message("Kapan Name Is Requrired");
                    txtKapanName.Focus();
                    return;
                }
                if (Val.ToInt(txtPacketNo.Text) == 0)
                {
                    Global.Message("PacketNo Is Requrired");
                    txtPacketNo.Focus();
                    return;
                }
                if (txtTag.Text.Trim().Length == 0)
                {
                    Global.Message("Tag Is Requrired");
                    txtTag.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DTabPredictionView = ObjView.DTabPredictionDataForManagement(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text);
                MainGridRow.DataSource = DTabPredictionView;
                MainGridRow.Refresh();
                GrdDetRowDetail.BestFitColumns();

                double DouGIAGrading = 0;
                double DouSuratGrading = 0;
                double DouBombayGrading = 0;

                foreach (DataRow Drow in DTabPredictionView.Rows)
                {
                    if (Val.ToString(Drow["PRDTYPENAME"]).ToUpper().Contains("GIA") || Val.ToString(Drow["PRDTYPENAME"]).ToUpper().Contains("LAB"))
                    {
                        DouGIAGrading = Val.Val(Drow["AMOUNT"]);
                    }
                    if (Val.ToString(Drow["PRDTYPENAME"]).ToUpper().Contains("MUMBAI") || Val.ToString(Drow["PRDTYPENAME"]).ToUpper().Contains("BOMBAY"))
                    {
                        DouBombayGrading= Val.Val(Drow["AMOUNT"]);
                    }
                    if (Val.ToString(Drow["PRDTYPENAME"]).ToUpper().Contains("SURAT") || Val.ToString(Drow["PRDTYPENAME"]).ToUpper().Contains("GRADING"))
                    {
                        DouSuratGrading = Val.Val(Drow["AMOUNT"]);
                    }
                }


                txtBYAmount.Text = DouBombayGrading.ToString();
                txtGIAAmount.Text = DouGIAGrading.ToString();
                txtGradingAmount.Text = DouSuratGrading.ToString();


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
            TextBrick BrickTitlesParam = e.Graph.DrawString("Packet :- " + txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
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

        private void GrdDetRowDetail_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            string StrPrdType = Val.ToString(GrdDetRowDetail.GetRowCellValue(e.RowHandle, "PRDTYPENAME")).ToUpper();
            
            if (StrPrdType == "MUMBAI GRADING")
            {
                e.Appearance.BackColor = Color.FromArgb(201, 182, 237);
            }
            else if (StrPrdType == "GIA GRADING" || StrPrdType.Contains("LAB"))
            {
                e.Appearance.BackColor = Color.FromArgb(218, 196, 209);
            }
            else if (StrPrdType.Contains("POLISH") || StrPrdType.Contains("GRADING"))
            {
                e.Appearance.BackColor = Color.FromArgb(200, 226, 199);
            }
            else if (StrPrdType.Contains("ARTIST"))
            {
                e.Appearance.BackColor = Color.FromArgb(240, 241, 210);
            }
            else if (StrPrdType.Contains("CHIEF"))
            {
                e.Appearance.BackColor = Color.FromArgb(255, 205, 253);
            }
            else if (StrPrdType.Contains("MAKABLE"))
            {
                e.Appearance.BackColor = Color.FromArgb(254, 203, 199);
            }
            else if (StrPrdType.Contains("CHECKER"))
            {
                e.Appearance.BackColor = Color.FromArgb(185, 181, 231);
            }
            else if (StrPrdType.Contains("FINAL PREDICTION"))
            {
                e.Appearance.BackColor = Color.FromArgb(200, 255, 253);
            }
            else if (StrPrdType.Contains("ROUGH PREDICTION"))
            {
                e.Appearance.BackColor = Color.FromArgb(255,255,255);
            }
        }

        private void GrdDetRowDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow DRow = new BOMST_Ledger().GetLedgerInfoByCode("EMPLOYEE", Val.ToString(GrdDetRowDetail.GetFocusedRowCellValue("EMPCODE")));

            lblEmployeeName.Text = "Employee : ";
            lblDepartmentName.Text = "Department : ";
            lblDesignation.Text = "Designation : ";
            lblMobileNo.Text = "MobileNo : ";
            PicEmpPhoto.Image = null;
            if (DRow != null)
            {
                lblEmployeeName.Text = "Employee : " + Val.ToString(DRow["LEDGERNAME"]);
                lblDepartmentName.Text = "Department : " + Val.ToString(DRow["DEPARTMENTNAME"]);
                lblDesignation.Text = "Designation : " + Val.ToString(DRow["DESIGNATIONNAME"]);
                lblMobileNo.Text = "MobileNo : " + Val.ToString(DRow["MOBILENO1"]);
                PicEmpPhoto.Image = null;

                byte[] OFFICELOGO = DRow["EMPPHOTO"] as byte[] ?? null;
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
            }


        }



        private void BtnKapan_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "KAPANNAME";
                FrmSearch.mSearchText = "";
                this.Cursor = Cursors.WaitCursor;

                FrmSearch.mDTab = ObjRap.GetKapan();

                FrmSearch.mColumnsToHide = "";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                if (FrmSearch.mDRow != null)
                {
                    txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
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

        private void BtnPacketNo_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "PACKETNO";
                FrmSearch.mSearchText = "";
                this.Cursor = Cursors.WaitCursor;

                FrmSearch.mDTab = ObjRap.GetPacketNo(txtKapanName.Text);

                FrmSearch.mColumnsToHide = "";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                if (FrmSearch.mDRow != null)
                {
                    txtPacketNo.Text = Val.ToString(FrmSearch.mDRow["PACKETNO"]);
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

        private void BtnTag_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "TAG";
                FrmSearch.mSearchText = "";
                this.Cursor = Cursors.WaitCursor;
                FrmSearch.mDTab = ObjRap.GetTag(txtKapanName.Text, Val.ToInt(txtPacketNo.Text));

                FrmSearch.mColumnsToHide = "KAPAN_ID,PACKET_ID";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                if (FrmSearch.mDRow != null)
                {
                    txtTag.Text = Val.ToString(FrmSearch.mDRow["TAG"]);
                    txtTag.Tag = Val.ToString(FrmSearch.mDRow["PACKET_ID"]);
                    txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);

                    lblLot.Text = "Lot : " + Val.ToString(FrmSearch.mDRow["LOTCARAT"]);
                    lblBalance.Text = "Bal : " + Val.ToString(FrmSearch.mDRow["BALANCECARAT"]);
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

        private void txtKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjRap.GetKapan();
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
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

        private void txtPacketNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PACKETNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjRap.GetPacketNo(txtKapanName.Text);
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPacketNo.Text = Val.ToString(FrmSearch.mDRow["PACKETNO"]);
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

        private void txtTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "TAG";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjRap.GetTag(txtKapanName.Text, Val.ToInt(txtPacketNo.Text));
                    FrmSearch.mColumnsToHide = "KAPAN_ID,PACKET_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtTag.Text = Val.ToString(FrmSearch.mDRow["TAG"]);
                        txtTag.Tag = Val.ToString(FrmSearch.mDRow["PACKET_ID"]);
                        txtKapanName.Tag = Val.ToString(FrmSearch.mDRow["KAPAN_ID"]);
                        lblLot.Text = "Lot : " + Val.ToString(FrmSearch.mDRow["LOTCARAT"]);
                        lblBalance.Text = "Bal : " + Val.ToString(FrmSearch.mDRow["BALANCECARAT"]);

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


    }
}
