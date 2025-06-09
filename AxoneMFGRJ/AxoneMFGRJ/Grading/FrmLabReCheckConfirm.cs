using BusLib;
using BusLib.Configuration;
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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using AxoneMFGRJ.Transaction;

namespace AxoneMFGRJ.Grading
{
    public partial class FrmLabReCheckConfirm : DevExpress.XtraEditors.XtraForm
    {
        // BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_LabRecheckConfirm ObjRecheck = new BOTRN_LabRecheckConfirm();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        IDataObject PasteclipData = Clipboard.GetDataObject();

        #region Property Settings

        public FrmLabReCheckConfirm()
        {
            InitializeComponent();
        }

        public FORMTYPE mFormType = FORMTYPE.LabReCheckConfirm;

        public enum FORMTYPE
        {
            LabReCheckConfirm = 1,
            GradingRepairingConfirm = 2
        }

        public void ShowForm(FORMTYPE pFormType)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();

            mFormType = pFormType;

            if (mFormType == FORMTYPE.GradingRepairingConfirm)
            {
                Text = "GRADING REPAIRING CONFIRM";
                GrdDet.Columns["CURRENTGRDRESULTSTATUS"].Visible = true;
                GrdDet.Columns["CURRENTLABRESULTSTATUS"].Visible = false;
            }
            else
            {
                Text = "LAB RECHECK/REPAIRING CONFIRM";
                GrdDet.Columns["CURRENTGRDRESULTSTATUS"].Visible = false;
                GrdDet.Columns["CURRENTLABRESULTSTATUS"].Visible = false;
            }

            ChkCmbKapanName.Properties.DataSource = ObjPacket.FindKapan();
            ChkCmbKapanName.Properties.DisplayMember = "KAPANNAME";
            ChkCmbKapanName.Properties.ValueMember = "KAPANNAME";
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        private void BtnShow_Click(object sender, EventArgs e)
        {
            Fill();
        }

        public void Fill()
        {
            this.Cursor = Cursors.WaitCursor;

            string StrOpe = "";
            if (mFormType == FORMTYPE.LabReCheckConfirm)
            {
                StrOpe = "LABREPAIRING";
            }
            else if (mFormType == FORMTYPE.GradingRepairingConfirm)
            {
                StrOpe = "GRDREPAIRING";
            }


            DataTable DTabLabRecheck = ObjRecheck.GetLabRecheck(Val.ToString(StrOpe), Val.Trim(ChkCmbKapanName.Properties.GetCheckedItems()),
                                                                Val.ToInt32(txtFromPacketNo.Text), Val.ToInt32(txtToPacketNo.Text),
                                                                Val.ToString(TxtTag.Text), Val.SqlDate(DtpFormDate.Text),
                                                                Val.SqlDate(DtpToDate.Text));
            MainGrid.DataSource = DTabLabRecheck;
            MainGrid.Refresh();
            this.Cursor = Cursors.Default;
        }

        private void repChkIsRecheckConfirm_CheckedChanged(object sender, EventArgs e)
        {
            if (mFormType == FORMTYPE.LabReCheckConfirm)
            {
                string pPacketId;
                string PAxoneId;
                string CurrentLabResultStatus;
                int IsRecheckConfirm;
                string Status;
                GrdDet.PostEditor();
                DataRow DR = GrdDet.GetFocusedDataRow();
                IsRecheckConfirm = Val.ToInt(DR["ISRECHECKCONFIRM"]);
                Status = Val.ToString(DR["CURRENTLABRESULTSTATUS"]);
                if (Status == "RECHECK" && IsRecheckConfirm == 1)
                {
                    CurrentLabResultStatus = "RECHECKCONFIRM";
                }
                else if (Status == "REPAIRING" && IsRecheckConfirm == 1)
                {
                    CurrentLabResultStatus = "REPAIRINGCONFIRM";
                }
                else
                {
                    CurrentLabResultStatus = Status;
                }
                pPacketId = Val.ToString(GrdDet.GetFocusedRowCellValue("PACKET_ID"));
                PAxoneId = Val.ToString(GrdDet.GetFocusedRowCellValue("PRD_ID"));
                int IntRes = new BOTRN_LabRecheckConfirm().SaveRecheckConfirm(pPacketId, CurrentLabResultStatus, PAxoneId);

                if (IntRes != -1)
                {
                    Global.Message("Lab Result Status Saved Successfully.");
                    Fill();
                }
                else
                {
                    Global.Message("Record Not Found");
                    Fill();
                }
            }
            else if (mFormType == FORMTYPE.GradingRepairingConfirm)
            {
                string pPacketId;
                string PAxoneId;
                string CurrentGrdResultStatus;
                int IsRecheckConfirm;
                string Status;
                Int64 IntEmployee_ID = 0;

                GrdDet.PostEditor();
                DataRow DR = GrdDet.GetFocusedDataRow();
                IsRecheckConfirm = Val.ToInt(DR["ISRECHECKCONFIRM"]);
                Status = Val.ToString(DR["CURRENTGRDRESULTSTATUS"]);

                IntEmployee_ID = Val.ToInt64(DR["EMPLOYEE_ID"]);

                if (Status == "REPAIRING" && IsRecheckConfirm == 1)
                {
                    CurrentGrdResultStatus = "REPAIRINGCONFIRM";
                }
                else
                {
                    CurrentGrdResultStatus = Status;
                }
                pPacketId = Val.ToString(GrdDet.GetFocusedRowCellValue("PACKET_ID"));
                PAxoneId = Val.ToString(GrdDet.GetFocusedRowCellValue("PRD_ID"));
                int IntRes = new BOTRN_LabRecheckConfirm().SaveRepairingConfirmForGrading(pPacketId, CurrentGrdResultStatus, PAxoneId, IntEmployee_ID);

                if (IntRes != -1)
                {
                    Global.Message("Grading-Repairing Status Saved Successfully.");
                    Fill();
                }
                else
                {
                    Global.Message("Record Not Found");
                    Fill();
                }
            }
        }

        private void repChkCancel_CheckedChanged(object sender, EventArgs e)
        {
            if (mFormType == FORMTYPE.LabReCheckConfirm)
            {

                string pPacketId;
                string PAxoneId;
                string CurrentLabResultStatus;
                int IsRecheckCancel;
                string Status;
                GrdDet.PostEditor();
                DataRow DR = GrdDet.GetFocusedDataRow();
                IsRecheckCancel = Val.ToInt(DR["ISRECHECKCANCEL"]);
                Status = Val.ToString(DR["CURRENTLABRESULTSTATUS"]);
                if (Status == "RECHECK" && IsRecheckCancel == 1)
                {
                    CurrentLabResultStatus = "RECHECKCANCEL";
                }
                else if (Status == "REPAIRING" && IsRecheckCancel == 1)
                {
                    CurrentLabResultStatus = "REPAIRINGCANCEL";
                }
                else
                {
                    CurrentLabResultStatus = Status;
                }
                pPacketId = Val.ToString(GrdDet.GetFocusedRowCellValue("PACKET_ID"));
                PAxoneId = Val.ToString(GrdDet.GetFocusedRowCellValue("PRD_ID"));
                int IntRes = new BOTRN_LabRecheckConfirm().SaveRecheckConfirm(pPacketId, CurrentLabResultStatus, PAxoneId);

                if (IntRes != -1)
                {
                    Global.Message("Lab Result Status Saved Successfully.");
                    Fill();
                }
                else
                {
                    Global.Message("Record Not Found");
                    Fill();
                }
            }
            else if (mFormType == FORMTYPE.GradingRepairingConfirm)
            {
                string pPacketId;
                string PAxoneId;
                string CurrentGrdResultStatus;
                int IsRecheckCancel;
                string Status;

                Int64 IntEmployee_ID = 0;

                GrdDet.PostEditor();
                DataRow DR = GrdDet.GetFocusedDataRow();
                IsRecheckCancel = Val.ToInt(DR["ISRECHECKCANCEL"]);

                IntEmployee_ID = Val.ToInt64(DR["EMPLOYEE_ID"]);

                Status = Val.ToString(DR["CURRENTGRDRESULTSTATUS"]);
                if (Status == "REPAIRING" && IsRecheckCancel == 1)
                {
                    CurrentGrdResultStatus = "REPAIRINGCANCEL";
                }
                else
                {
                    CurrentGrdResultStatus = Status;
                }

                pPacketId = Val.ToString(GrdDet.GetFocusedRowCellValue("PACKET_ID"));
                PAxoneId = Val.ToString(GrdDet.GetFocusedRowCellValue("PRD_ID"));
                int IntRes = new BOTRN_LabRecheckConfirm().SaveRepairingConfirmForGrading(pPacketId, CurrentGrdResultStatus, PAxoneId, IntEmployee_ID);

                if (IntRes != -1)
                {
                    Global.Message("Grading Repairing Status Saved Successfully.");
                    Fill();
                }
                else
                {
                    Global.Message("Record Not Found");
                    Fill();
                }
            }
        }
    }
}
