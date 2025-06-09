using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.View;
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

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmAtdCaptureDetail : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        string mStrLogDateTime = "";
        string mStrEmpCode = "";
        string mStrPunch = "";

        #region Property Settings

        public FrmAtdCaptureDetail()
        {
            InitializeComponent();
        }

        public void ShowForm(DataTable DtabDetail, string StrPassword)
        {
            //Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            MainGrid.DataSource = DtabDetail;
            GrdDet.RefreshData();

            if (StrPassword == Val.ToString(txtPassword.Tag))
            {
                GrdDet.Columns["DELETE"].Visible = true;
            }
            else
            {
                GrdDet.Columns["DELETE"].Visible = false;
            }
            

          
            this.ShowDialog();

        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Obj);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        private void RepBtnDelete_Click(object sender, EventArgs e)
        {
            DataRow DRow = GrdDet.GetFocusedDataRow();
            this.Cursor = Cursors.WaitCursor;
            AttendanceEntryProperty Property = new AttendanceEntryProperty();

            if (Global.Confirm("Are You Sure You Want To Delete [" + Val.ToString(DRow["EMPCODE"]) + "]  ?") == System.Windows.Forms.DialogResult.No)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            if (Val.ToString(DRow["EMPCODE"]) != "")
            {

                mStrPunch = Val.ToString(DRow["PUCHDERACTION"]);
                mStrEmpCode = Val.ToString(DRow["EMPCODE"]);
                mStrLogDateTime = Val.ToString(DRow["LOGDATETIME"]);
                Property = Obj.Delete(mStrEmpCode, mStrLogDateTime, mStrPunch, Property);
                Global.Message(Property.ReturnMessageDesc);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    GrdDet.DeleteRow(GrdDet.FocusedRowHandle);
                    MainGrid.Refresh();
                }
            }
            else
            {
                GrdDet.DeleteRow(GrdDet.FocusedRowHandle);
                MainGrid.Refresh();
            }

            this.Cursor = Cursors.Default;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.ToUpper() == Val.ToString(txtPassword.Tag).ToUpper())
            {
                GrdDet.Columns["DELETE"].Visible = true;
            }
            else
            {
                GrdDet.Columns["DELETE"].Visible = false;
            }
        }

    }
}
