using BusLib.Configuration;
using BusLib.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmQCTransfer : DevExpress.XtraEditors.XtraForm
    {

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RunninPossition ObjMast = new BOTRN_RunninPossition();
        BOFormPer ObjPer = new BOFormPer();
        DataTable DtabQC = new DataTable();

        string StrFromDate = null;
        string StrToDate = null;

        #region Property Settings
        public FrmQCTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

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
        }
        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; 
            if (DTPFromDate.Checked == true)
            {
                StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
            }
            if (DTPToDate.Checked == true)
            {
                StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
            }
                 if (!backgroundWorker1.IsBusy)
                {
                  
                    backgroundWorker1.RunWorkerAsync();
                }

            }
         
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Global.Message(EX.ToString());
            }
              this.Cursor = Cursors.Default; 
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Clear()
        {
            this.Cursor = Cursors.WaitCursor;

            DTPFromDate.Text = Val.ToString(DateTime.Now.AddDays(-7));
            DTPToDate.Text = Val.ToString(DateTime.Now);
           
            this.Cursor = Cursors.Default;

        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Clear();
            this.Cursor = Cursors.Default;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataSet DS = ObjMast.GetQCTransferData( StrFromDate, StrToDate);
                 DtabQC = DS.Tables[0];
                
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
             try
            {
                this.Cursor = Cursors.WaitCursor;

                MainGrid.DataSource = DtabQC;
                GrdDet.RefreshData();
                GrdDet.BestFitColumns();

                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
              
                Global.Message(Ex.Message.ToString());
            }
        
        }
    
    
    }


}

