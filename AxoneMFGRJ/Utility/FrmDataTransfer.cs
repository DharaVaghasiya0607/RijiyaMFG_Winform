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
    public partial class FrmDataTransfer : Form
    {
        BODevGridSelection ObjSelMaster;
        BODevGridSelection ObjSelTransaction;
        
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOMST_KapanTransfer ObjKapan = new BOMST_KapanTransfer();
        KapanTransferProperty property = new KapanTransferProperty();

        #region ShowForm

        public FrmDataTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";

            this.Show();
        }


        private void BtnRefresh_Click(object sender, EventArgs e)
        {
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
            ObjFormEvent.ObjToDisposeList.Add(ObjKapan);
        }


        #endregion
        private void BtnTransferMaster_Click(object sender, EventArgs e)
        {
            try
            {
                progressPanelDelete.Visible = false;
                

                progressPanelTransaction.Visible = true;
                BtnTransferTransaction.Enabled = false;
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
                Global.Message(ex.Message);
            }

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bgWorkerTransaction_DoWork(object sender, DoWorkEventArgs e)
        {
            property.OPE = "TRANSFER";
            property.KAPANNAME = Val.Trim(CmbKapan.Properties.GetCheckedItems());
            property = ObjKapan.Transfer(property);
        }

        private void bgWorkerTransaction_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanelTransaction.Visible = false;
            BtnTransferTransaction.Enabled = true;

            progressPanelDelete.Visible = false;
            BtnDeleteTransaction.Enabled = false;

            if (property.ReturnMessageType == "FAIL")
            {
                lblMessageTransaction.ForeColor = Color.Maroon;
                lblMessageTransaction.Text = "TRANSACTION DATA TRANSFER GETS SOME ERROR PLS CHECK...!!!!";
            }
            else
            {
                lblMessageTransaction.ForeColor = Color.DarkGreen;
                lblMessageTransaction.Text = "TRANSACTION DATA TRANSFER DONE...!!!!";
            }
        }

        private void BtnTransferTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(CmbKapan.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Kapan That You Wan't To Transfer...");
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
                BtnDeleteTransaction.Enabled = true;
                BtnTransferTransaction.Enabled = true;
                Global.Message(ex.Message);
            }
        }

        private void bgWorkerTransactionDelete_DoWork(object sender, DoWorkEventArgs e)
        {
            property.OPE = "DELETE";
            property.KAPANNAME = Val.ToString(CmbKapan.Properties.GetCheckedItems());
            property = ObjKapan.DELETE(property);
        }

        private void bgWorkerTransactionDelete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanelTransaction.Visible = false;
            BtnTransferTransaction.Enabled = false;

            progressPanelDelete.Visible = false;
            BtnDeleteTransaction.Enabled = true;

            if (property.ReturnMessageType == "FAIL")
            {
                lblMessageTransaction.ForeColor = Color.Maroon;
                lblMessageTransaction.Text = "TRANSACTION DATA TRANSFER GETS SOME ERROR PLS CHECK...!!!!";
            }
            else
            {
                lblMessageTransaction.ForeColor = Color.DarkGreen;
                lblMessageTransaction.Text = "TRANSACTION DATA TRANSFER DONE...!!!!";
            }
        }
    }
}
