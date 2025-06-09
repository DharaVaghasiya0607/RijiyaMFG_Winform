using BusLib.Configuration;
using BusLib.TableName;
using BusLib.View;
using DevExpress.Utils;
using DevExpress.XtraCharts;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.View
{
    public partial class FrmDiscountBreakup : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        
        #region ShowForm

        public FrmDiscountBreakup()
        {
            InitializeComponent();
        }

        public void ShowForm(DataTable pDTab)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            GrdDet.BeginUpdate();
            MainGrid.DataSource = pDTab;
            GrdDet.RefreshData();
            GrdDet.BestFitColumns();
            GrdDet.EndUpdate();

            this.Show();
        }

        private void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
        }
        

        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
