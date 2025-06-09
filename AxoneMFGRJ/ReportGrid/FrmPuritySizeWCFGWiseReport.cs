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
using BusLib.View;
using OfficeOpenXml;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmPuritySizeWCFGWiseReport : DevExpress.XtraEditors.XtraForm
    {
        BOFindRap ObjRap = new BOFindRap();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjView = new BOTRN_RunninPossition();

        #region Property Settings

        public FrmPuritySizeWCFGWiseReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            //CmbKapan.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            //CmbKapan.DisplayMember = "KAPANNAME";
            //CmbKapan.ValueMember = "KAPANNAME";
            //CmbKapan.Focus();
            //CmbKapan.SelectedIndex = -1;

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            CmbKapan.Focus();


            //RdbGrdType.SelectedIndex = 0;
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
                string StrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems()); //CmbKapan.SelectedValue.ToString();
                if (RbtComboReport.Checked == true)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataSet DS = ObjView.ComboReportGetData(StrKapan);

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowFullKapanAnalysis("RPT_FullKapanAssortment", DS);
                }
                if (RbtPurityReport.Checked == true)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable DTab = ObjView.PurityWiseGetData(StrKapan);

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowForm("Rpt_PurityWise", DTab);
                }

                if (RbtSizeReport.Checked == true)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable DTab = ObjView.SizeWiseGetData(StrKapan);

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowForm("Rpt_SizeWise", DTab);
                }

                if (RbtWCFGReport.Checked == true)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable DTab = ObjView.ShapeWiseGetData(StrKapan);

                    Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();
                    FrmReportViewer.MdiParent = Global.gMainRef;
                    FrmReportViewer.ShowForm("Rpt_ShapeWise", DTab);
                }



                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
                return;
            }


        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        


    }
}
