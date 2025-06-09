using AxoneMFGRJ.Utility;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using BusLib.View;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Utility
{
    public partial class FrmKapanTransfer : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOUTILITY_KapanTransfer Obj = new BOUTILITY_KapanTransfer();

        #region ShowForm

        public FrmKapanTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.ShowDialog();

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
        }

        private void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyPress = false;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Obj);
        }


        #endregion

        private string BulkCopyTransaction(string pStrKapanName) //#P : 09-03-2021
        {
            string Str = string.Empty;
            try
            {
                int pIntTransfer = Obj.KapanTransfer(pStrKapanName,"TRANSFER");
                if (pIntTransfer != 0)
                {
                    lblMessageTransaction.Text = "Kapan Transfer Sucessfully";
                }
            }
            catch (Exception EX)
            {
                Str = "Error !! " + EX.Message;
                lblMessageTransaction.Text = Str;
            }
            return Str;
        }
        private void bgWorkerTransaction_DoWork(object sender, DoWorkEventArgs e)
        {
            BulkCopyTransaction(Val.Trim(CmbKapan.Properties.GetCheckedItems()));
        }

        private void bgWorkerTransaction_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanelTransaction.Visible = false;
            BtnTransferTransaction.Enabled = true;
            BtnDeleteTransaction.Enabled = true;
        }



        private void BtnTransferTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(CmbKapan.Text).Trim().Equals(string.Empty))
                {
                    lblMessageTransaction.Text = "Please Select Kapan That You Wan't To Transfer...";
                    CmbKapan.Focus();
                    return;
                }

                progressPanelTransaction.Visible = true;
                BtnTransferTransaction.Enabled = false;
                BtnDeleteTransaction.Enabled = false;
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                if (!bgWorkerTransaction.IsBusy)
                {
                    bgWorkerTransaction.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                progressPanelTransaction.Visible = false;
                BtnTransferTransaction.Enabled = true;
                BtnDeleteTransaction.Enabled = true;
                Global.Message(ex.Message);
            }
        }


        private void BtnDeleteTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(CmbKapan.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Kapan That You Wan't To Delete...");
                    CmbKapan.Focus();
                    return;
                }

                if (Global.Confirm("Are You Sure To Delete Transaction Of Kapan's From Source Database.. ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                progressPanelTransactionDelete.Visible = true;
                BtnDeleteTransaction.Enabled = false;
                BtnTransferTransaction.Enabled = false;
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                if (!bgWorkerTransactionDelete.IsBusy)
                {
                    bgWorkerTransactionDelete.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                progressPanelTransactionDelete.Visible = false;
                BtnDeleteTransaction.Enabled = true;
                BtnTransferTransaction.Enabled = true;
                Global.Message(ex.Message);
            }
        }

        private void bgWorkerTransactionDelete_DoWork(object sender, DoWorkEventArgs e)
        {
            //BulkCopyTransactionDelete(Val.ToString(CmbKapan.Properties.GetCheckedItems()));
        }

        private void bgWorkerTransactionDelete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanelTransactionDelete.Visible = false;
            BtnDeleteTransaction.Enabled = true;
            BtnTransferTransaction.Enabled = true;
        }
    }
}
