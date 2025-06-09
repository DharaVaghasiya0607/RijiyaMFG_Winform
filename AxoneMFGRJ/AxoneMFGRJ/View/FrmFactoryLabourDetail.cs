using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using System.IO;
using BusLib.Rapaport;
using AxoneMFGRJ.Utility;
using AxonDataLib;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using System.Linq;
using System.Collections;
using AxoneMFGRJ.Masters;
using BusLib.View;

namespace AxoneMFGRJ.View
{
    public partial class FrmFactoryLabourDetail : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOTRN_RunninPossition Obj = new BOTRN_RunninPossition();

        DataTable DTabPrediction = new DataTable();

        #region Constructor

        public FrmFactoryLabourDetail()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrKapanName, int pIntPacketNo, string pStrTag, string pStrPacketID, string pStrEmpoyeeName, Int64 pIntEmployeeID, string StrType)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Text = pStrKapanName + "-" + pIntPacketNo.ToString() + pStrTag;

            txtKapanName.Text = pStrKapanName;
            txtPacketNo.Text = pIntPacketNo.ToString();
            txtTag.Text = pStrTag;
            txtTag.Tag = pStrPacketID;

            txtEmployee.Text = pStrEmpoyeeName;
            txtEmployee.Tag = pIntEmployeeID;

            DTabPrediction = Obj.GetFactoryProductionLabourDetail(pStrKapanName, pIntPacketNo, pStrTag, pStrPacketID, pIntEmployeeID, StrType);

            GrdDet.BeginUpdate();

            MainGrid.DataSource = DTabPrediction;
            GrdDet.RefreshData();
            GrdDet.BestFitColumns();

            GrdDet.EndUpdate();

            if (StrType == "WORKER")
            {
                lblPolChk.Visible = true;
                lblPolishChecker.Visible = true;
            }
            else
            {
                lblPolChk.Visible = false;
                lblPolishChecker.Visible = false;
            }

            this.Show();

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
            ObjFormEvent.ObjToDisposeList.Add(Obj);
            ObjFormEvent.ObjToDisposeList.Add(DTabPrediction);

        }

        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrdDet_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            string Str = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "PRDTYPE")).ToUpper();
            if (Str.Contains("GRADING") || Str.Contains("PCN"))
            {
                e.Appearance.BackColor = lblGrading.BackColor;
            }
            else if (Str.Contains("BOMBAY"))
            {
                e.Appearance.BackColor = lblBombay.BackColor;
            }
            else if (Str.Contains("LAB"))
            {
                e.Appearance.BackColor = lblLab.BackColor;
            }
            else if (Str.Contains("POLCHECKER"))
            {
                e.Appearance.BackColor = lblPolishChecker.BackColor;
            }
        }

    }

}