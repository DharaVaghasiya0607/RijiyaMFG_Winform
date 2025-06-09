using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Masters
{
    public partial class FormPktGradeUpdate : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();
        DataTable DtabPenalty = new DataTable();

        public FormPktGradeUpdate()
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

        
       

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string IntRes = "";
                if (Global.Confirm("Are You Sure For PacketGrade Update???") == System.Windows.Forms.DialogResult.No)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                IntRes = ObjTrn.UpdatePacketGrade(Val.ToInt64(txtKapan.Tag), Val.ToInt32(txtFromaPacket.Text), Val.ToInt32(txtTopacket.Text), Val.ToString(txtTag.Text), Val.ToInt32(txtPacketGrade.Tag));

                if (IntRes == "SUCCESS")
                {

                    Global.Message("SUCCESSFULLY SAVED RIGHTS");
                    txtKapan.Text = string.Empty;
                    txtKapan.Tag = String.Empty;
                    txtFromaPacket.Text = String.Empty;
                    txtTopacket.Text = String.Empty;
                    txtTag.Text = String.Empty;
                    txtPacketGrade.Text = String.Empty;
                    txtPacketGrade.Tag = String.Empty;

                }
                else
                {
                    Global.Message("OOPS SOMETHING GOES WRONG");
                    txtKapan.Text = string.Empty;
                    txtKapan.Tag = String.Empty;
                    txtFromaPacket.Text = String.Empty;
                    txtTopacket.Text = String.Empty;
                    txtTag.Text = String.Empty;
                    txtPacketGrade.Text = String.Empty;
                    txtPacketGrade.Tag = String.Empty;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }
       
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOTRN_SinglePacketCreate().FindKapan();
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "KAPANNAME";
                    FrmSearch.mStrColumnsToHide = "KAPAN_ID";
                    FrmSearch.ValueMemeter = "KAPAN_ID";
                    FrmSearch.DisplayMemeter = "KAPANNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtKapan.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtKapan.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
        }

        private void txtPacketGrade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PACKETGRADENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PACKETGRADE);

                    FrmSearch.mColumnsToHide = "PACKETGRADE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtPacketGrade.Text = Val.ToString(FrmSearch.mDRow["PACKETGRADENAME"]);
                        txtPacketGrade.Tag = Val.ToString(FrmSearch.mDRow["PACKETGRADE_ID"]);
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtKapan.Text = string.Empty;
            txtKapan.Tag = String.Empty;
            txtFromaPacket.Text = String.Empty;
            txtTopacket.Text = String.Empty;
            txtTag.Text = String.Empty;
            txtPacketGrade.Text = String.Empty;
            txtPacketGrade.Tag = String.Empty;
        }
    }
}
