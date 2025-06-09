using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Newtonsoft.Json;

namespace AxoneMFGRJ.Utility
{
    public partial class FrmClientTicket : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        System.Windows.Forms.DialogResult mDialog;

        string mStrPassword = "";

        public FrmClientTicket()
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
            ObjFormEvent.FormKeyPress = false;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        public void Fill()
        {
            APITicket.WebService A = new APITicket.WebService();
            DataSet DS = A.ClientTicket_Dataset(Val.ToString(txtTicketNo.Text), Val.SqlDate(DtpSearchFromDate.Value.ToShortDateString()), Val.SqlDate(DtpSearchToDate.Value.ToShortDateString()), Val.ToString(cmbTIcketStatus.SelectedItem));
            MainGrid.DataSource = DS.Tables[1];
            GrdDet.RefreshData();
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize the web service
                APITicket.WebService A = new APITicket.WebService();

                // Call the web service and get the response
                DataSet DS = A.ClinetTicket_JSON_V1(Val.ToString(txtTicketNo.Text), 0, 1641127230223, "RIJIYA MFG",
                                                           Val.ToString(txtGeneratedBy.Text),
                                                           Val.SqlDate(DtpTicketDate.Value.ToShortDateString()),
                                                           "", Val.ToString(txtTicketDetail.Text),
                                                           Val.ToString(cmbTicketPriority.SelectedItem),
                                                           Val.ToString(cmbTIcketStatus.SelectedItem), "");
                if (DS.Tables[0].Rows.Count > 0)
                {
                    DataTable DTab = DS.Tables[0];
                    Global.Message(DTab.Rows[0]["RETURNMESSAGE"].ToString());
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }


        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                Fill();
            }
            catch (Exception Ex)
            {

            }
        }

        public void FetchValue(DataRow DR)
        {
            txtTicketNo.Text = Val.ToString(DR["TICKETNOSTR"]);
            txtTicketNo.Tag = Guid.Parse(Val.ToString(DR["ID"]));
            DtpTicketDate.Text = Val.ToString(DR["TICKETDATE"]);
            txtGeneratedBy.Text = Val.ToString(DR["TICKETGENERATEDBY"]);
            cmbTicketPriority.Text = Val.ToString(DR["PRIORITY"]);
            cmbTIcketStatus.Text = Val.ToString(DR["TICKETSTATUS"]);
            txtTicketDetail.Text = Val.ToString(DR["TASKDETAIL"]);
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
    }
}