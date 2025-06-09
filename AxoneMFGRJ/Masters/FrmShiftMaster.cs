using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Google.API.Translate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AxoneMFGRJ.Masters
{
    public partial class  FrmShiftMaster : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Shift ObjMast = new BOMST_Shift();
        DataTable DTab = new DataTable();

        Int32 TotalHours = 0;
        Int32 TotalMin = 0;

        #region Property Settings

        public FrmShiftMaster()
        {
            InitializeComponent();
        }

      
        public void ShowForm()
        {
           
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            DtpToDate.Value = DateTime.Now;
            DtpFromDate.Value = DateTime.Now;

            DtpToDate.Text = "8:30:00 PM";
            DtpFromDate.Text = "7:30:00 AM";
           // DtpFromDate.Value = DateTime.Now;

            Fill();
            this.Show();

            txtShiftName.Focus();
        }

        private bool ValSave()
        {
            if (txtShiftName.Text.Trim().Length == 0)
            {
                Global.Message("Shift Is Required");
                txtShiftName.Focus();
                return false;
            }

            if (cmbShiftType.Text.Trim().Length == 0)
            {
                Global.Message("Shift Type Is Required");
                cmbShiftType.Focus();
                return false;
            }

            return true;
        }
        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        public void Fill()
        {
            DTab = ObjMast.Fill();
            MainGrid.DataSource = DTab;
            MainGrid.Refresh();
        }

        public void TimeDifferance()
        {
            TimeSpan duration = DateTime.Parse(DtpShiftEnd.Value.ToLongTimeString()).Subtract(DateTime.Parse(DtpShiftStart.Value.ToLongTimeString()));
            TotalHours = Math.Abs(Val.ToInt32(duration.TotalHours));
            TotalMin = Math.Abs(Val.ToInt32(duration.TotalMinutes));
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                TimeDifferance();

                ShiftMasterProperty Property = new ShiftMasterProperty();

                Property.SHIFT_ID = Val.ToInt32(txtShiftName.Tag);
                Property.SHIFTNAME = Val.ToString(txtShiftName.Text);
                Property.SHIFTTYPE = Val.ToString(cmbShiftType.Text);

                Property.FROMDATE = Val.SqlDate(DtpFromDate.Value.ToShortDateString());
                Property.TODATE = "";

                Property.SHIFTSTARTTIME = Val.SqlTime(DtpShiftStart.Value.ToLongTimeString());
                Property.SHIFTENDTIME = Val.SqlTime(DtpShiftEnd.Value.ToLongTimeString());

                Property.PUNCHSTARTTIME = Val.SqlTime(DtpPunchStart.Value.ToLongTimeString());
                Property.PUNCHENDTIME = Val.SqlTime(DtpPunchEnd.Value.ToLongTimeString());

                Property.LUNCHSTARTTIME = Val.SqlTime(DtpLunchStart.Value.ToLongTimeString());
                Property.LUNCHENDTIME = Val.SqlTime(DtpLunchEnd.Value.ToLongTimeString());

                Property.IDLEINTIME = Val.SqlTime(DtpIdleIn.Value.ToLongTimeString());
                Property.IDLEOUTTIME = Val.SqlTime(DtpIdleOut.Value.ToLongTimeString());

                Property.TOTALHOURS = TotalHours;
                Property.TOTALMINUTES = TotalMin;

                Property.OTAPPLICABLEAFTER = Val.ToInt32(txtOtAfter.Text);
                Property.OTAPPLICABLEBEFORE = Val.ToInt32(txtOTBefore.Text);

                Property = ObjMast.Save(Property);

                txtShiftName.Tag = Property.ReturnValue;

                ReturnMessageDesc = Property.ReturnMessageDesc;
                ReturnMessageType = Property.ReturnMessageType;
                
                Property = null;

                Global.Message(ReturnMessageDesc);
                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    BtnAdd_Click(null, null);

                    if (GrdDet.RowCount >= 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {
                    txtShiftName.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        
        private void txtLeaveType_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEAVETYEPE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEAVETYPE);

                    FrmSearch.mColumnsToHide = "LEAVETYPE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        //txtLeaveType.Text = Val.ToString(FrmSearch.DRow["LEAVETYEPE"]);
                        //txtLeaveType.Tag = Val.ToString(FrmSearch.DRow["LEAVETYPE_ID"]);
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ShiftMasterProperty Property = new ShiftMasterProperty();
            try
            {

                if (Global.Confirm("Are Your Sure To Delete The Record ?") == System.Windows.Forms.DialogResult.No)
                    return;


                Property.SHIFT_ID = Val.ToInt32(txtShiftName.Tag);
                Property = ObjMast.Delete(Property);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    BtnAdd_Click(null, null);
                    Global.Message(Property.ReturnMessageDesc);
                }
                else
                {
                    DtpPunchStart.Focus();
                }

            }
            catch (System.Exception ex)
            {
                Global.MessageToster(ex.Message);
            }
            Property = null;
        }

        public void FetchValue(DataRow DR)
        {
            txtShiftName.Tag = Val.ToInt32(DR["SHIFT_ID"]);
            txtShiftName.Text = Val.ToString(DR["SHIFTNAME"]);
            cmbShiftType.Text = Val.ToString(DR["SHIFTTYPE"]);
            DtpFromDate.Text = Val.ToString(DR["FROMDATE"]);
            DtpToDate.Text = Val.ToString(DR["TODATE"]);
            DtpPunchStart.Text = Val.ToString(DR["PUNCHSTARTTIME"]);
            DtpPunchEnd.Text = Val.ToString(DR["PUNCHENDTIME"]);
            DtpShiftStart.Text = Val.ToString(DR["SHIFTSTARTTIME"]);
            DtpShiftEnd.Text = Val.ToString(DR["SHIFTENDTIME"]);
            DtpIdleIn.Text = Val.ToString(DR["IDLEINTIME"]);
            DtpIdleOut.Text = Val.ToString(DR["IDLEOUTTIME"]);
            DtpLunchStart.Text = Val.ToString(DR["LUNCHSTARTTIME"]);
            DtpLunchEnd.Text = Val.ToString(DR["LUNCHENDTIME"]);
            txtOTBefore.Text = Val.ToString(DR["OTAPPLICABLEBEFORE"]);
            txtOtAfter.Text = Val.ToString(DR["OTAPPLICABLEAFTER"]);
        }

        
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                txtShiftName.Tag = string.Empty;
                txtShiftName.Text = string.Empty;
                cmbShiftType.SelectedIndex = 0;
                DtpToDate.Text = "8:30:00 PM";
                DtpFromDate.Text = "7:30:00 AM";
                DtpShiftStart.Value = DateTime.Now;
                DtpShiftEnd.Value = DateTime.Now;
                DtpPunchStart.Value = DateTime.Now;
                DtpPunchEnd.Value = DateTime.Now;
                DtpLunchStart.Value = DateTime.Now;
                DtpLunchEnd.Value = DateTime.Now;
                DtpPunchEnd.Value = DateTime.Now;
                DtpIdleIn.Value = DateTime.Now;
                DtpIdleOut.Value = DateTime.Now;
                txtOTBefore.Text = string.Empty;
                txtOtAfter.Text = string.Empty;
                GrdDet.RefreshData();
                TotalMin = 0;
                TotalHours = 0;
            }
            catch(Exception eX)
            {
                Global.Message(eX.Message);
            }
            

        }

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                if (e.Clicks == 2)
                {
                    DataRow DR = GrdDet.GetDataRow(e.RowHandle);
                    FetchValue(DR);
                    DR = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = ".xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                svDialog.Filter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                //if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                //{
                   

                //    //link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

                    
                //}

                PrintableComponentLinkBase link = new PrintableComponentLinkBase()
                {
                    PrintingSystemBase = new PrintingSystemBase(),
                    Component = MainGrid,
                    Landscape = true,
                    PaperKind = PaperKind.A4,
                    Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20)
                };

                link.ExportToXlsx(svDialog.FileName);

                if (Global.Confirm("Do You Want To Open [ " + svDialog.FileName + "] ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                }
                svDialog.Dispose();
                svDialog = null;

            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }

        }

        private void DtpToDate_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}
