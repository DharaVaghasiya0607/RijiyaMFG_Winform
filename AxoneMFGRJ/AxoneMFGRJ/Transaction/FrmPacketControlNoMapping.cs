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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using AxoneMFGRJ.Report;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmPacketControlNoMapping : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_PacketControlNoMapping ObjTrn = new BOTRN_PacketControlNoMapping();

        DataTable DTabFound = new DataTable();
        DataTable DTabNotFound = new DataTable();
        DataTable DTabSaveRec = new DataTable();
        DataTable DTabControlNo = new DataTable();

        #region Property Settings

        public FrmPacketControlNoMapping()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            //DataSet DS = ObjTrn.GetData();
            //DTabNotFound = DS.Tables[0];
            //DTabControlNo = DS.Tables[1];

            //DTabFound = DTabNotFound.Clone();
            //DTabSaveRec = DTabNotFound.Clone();
            //MainGridFound.DataSource = DTabFound;
            //MainGridFound.Refresh();

            //txtKapanName.Text = string.Empty;
            //txtPacketNo.Text = string.Empty;
            //txtTag.Text = string.Empty;
            BtnClear_Click(null, null);


            this.Show();
            txtKapanName.Focus();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion


        private void txtTag_Validated(object sender, EventArgs e)
        {
            try
            {

                if (txtKapanName.Text.Trim().Length == 0)
                {
                    return;
                }
                if (Val.ToInt(txtPacketNo.Text) == 0)
                {
                    txtKapanName.Focus();
                    return;
                }
                if (txtTag.Text.Trim().Length == 0)
                {
                    txtKapanName.Focus();
                    return;
                }

                if (Val.ISNumeric(txtTag.Text) == true)
                {
                    Char c = (Char)(64 + Val.ToInt(txtTag.Text));
                    txtTag.Text = c.ToString();
                }

                this.Cursor = Cursors.WaitCursor;

                DataRow DRowNotFound = null;
                int IntI = 0;
                for (IntI = 0; IntI < DTabNotFound.Rows.Count; IntI++)
                {
                    DataRow DR = DTabNotFound.Rows[IntI];
                    if (txtKapanName.Text.Trim() == Val.ToString(DR["KAPANNAME"]).Trim()
                       && txtPacketNo.Text.Trim() == Val.ToString(DR["PACKETNO"]).Trim()
                       && txtTag.Text.Trim() == Val.ToString(DR["TAG"]).Trim()
                       )
                    {
                        DRowNotFound = DR;
                        break;
                    }
                }

                bool ISExists = false;

                foreach (DataRow DRowFound in DTabFound.Rows)
                {
                    if (txtKapanName.Text.Trim() == Val.ToString(DRowFound["KAPANNAME"]).Trim()
                       && txtPacketNo.Text.Trim() == Val.ToString(DRowFound["PACKETNO"]).Trim()
                       && txtTag.Text.Trim() == Val.ToString(DRowFound["TAG"]).Trim()
                       )
                    {
                        ISExists = true;
                    }
                }

                if (DRowNotFound != null && ISExists == false)
                {

                    DataRow DRNew = DTabFound.NewRow();
                    DRNew["TRN_ID"] = DRowNotFound["TRN_ID"];
                    DRNew["PACKET_ID"] = DRowNotFound["PACKET_ID"];
                    DRNew["KAPANNAME"] = DRowNotFound["KAPANNAME"];
                    DRNew["PACKETNO"] = DRowNotFound["PACKETNO"];
                    DRNew["TAG"] = DRowNotFound["TAG"];
                    DRNew["CARAT"] = DRowNotFound["CARAT"];
                    DRNew["GIACONTROLNUMBER"] = DRowNotFound["GIACONTROLNUMBER"];
                    DRNew["LABCONTROLNUMBER"] = DRowNotFound["LABCONTROLNUMBER"];

                    DTabFound.Rows.Add(DRNew);
                    DTabNotFound.Rows[IntI].Delete();
                    DTabNotFound.AcceptChanges();
                    DTabFound.AcceptChanges();


                }
                //else if (DRowNotFound !=null)
                else if (ISExists)
                {
                    Global.Message(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " : ALREADY SCANNED IN FOUND GRID");
                }


                //if (GrdDetFound.RowCount > 1)
                //{
                //    GrdDetFound.FocusedRowHandle = GrdDetFound.RowCount - 1;
                //}
                //if (GrdDetExtra.RowCount > 1)
                //{
                //    GrdDetExtra.FocusedRowHandle = GrdDetExtra.RowCount - 1;
                //}

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                TxtControlNo.Focus();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTag_Leave(object sender, EventArgs e)
        {
            TxtControlNo.Focus();
        }

        private void TxtControlNo_Validated(object sender, EventArgs e)
        {
            try
            {

                if (TxtControlNo.Text.Trim().Length == 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                //bool contains = Val.ToBoolean( DTabControlNo.AsEnumerable().Any(row => TxtControlNo.Text.Trim() == row.Field<String>("GIACONTROLNUMBER")));

                DataRow[] foundAuthors;

                if(RbtGIAMapping.Checked)
                    foundAuthors = DTabControlNo.Select("GIACONTROLNUMBER = '" + TxtControlNo.Text.Trim() + "'");
                else
                    foundAuthors = DTabControlNo.Select("LABCONTROLNUMBER = '" + TxtControlNo.Text.Trim() + "'");

                //if (!contains)
                if (foundAuthors.Length == 0)
                {

                    bool ISExists = false;
                    int IntControlIndex = -1;
                    int intI = 0;
                    foreach (DataRow DRowFound in DTabFound.Rows)
                    {
                        if (RbtLabMapping.Checked)
                        {
                            if (TxtControlNo.Text.Trim() == Val.ToString(DRowFound["LABCONTROLNUMBER"]).Trim())
                                ISExists = true;

                            if (IntControlIndex == -1)
                            {
                                if (Val.ToInt64(DRowFound["LABCONTROLNUMBER"]) == 0)
                                    IntControlIndex = intI;
                            }
                        }
                        else
                        {
                            if (TxtControlNo.Text.Trim() == Val.ToString(DRowFound["GIAControlNumber"]).Trim())
                                ISExists = true;

                            if (IntControlIndex == -1)
                            {
                                if (Val.ToInt64(DRowFound["GIAControlNumber"]) == 0)
                                    IntControlIndex = intI;
                            }
                        }

                        intI++;
                    }

                    if (ISExists == false && IntControlIndex != -1)
                    {
                        //DTabFound.Rows[IntControlIndex]["GIACONTROLNUMBER"] = TxtControlNo.Text.Trim();
                        if (RbtLabMapping.Checked)
                            DTabFound.Rows[IntControlIndex]["LABCONTROLNUMBER"] = TxtControlNo.Text.Trim();
                        else
                            DTabFound.Rows[IntControlIndex]["GIACONTROLNUMBER"] = TxtControlNo.Text.Trim();

                        DTabFound.AcceptChanges();
                        DTabSaveRec.ImportRow(DTabFound.Rows[IntControlIndex]);
                    }
                    else if (ISExists)
                    {
                        Global.Message(TxtControlNo.Text + " : ALREADY SCANNED ");
                    }
                    else if (IntControlIndex == -1)
                    {
                        Global.Message("PACKET WAS NOT SELECTED FOR NEW CONTROL NO");
                    }
                }
                else
                    Global.Message("CONTROL NO IS ALREADY ALLOCATED FOR SOME OTHER PACKET");

                TxtControlNo.Text = string.Empty;
                txtKapanName.Focus();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void TxtControlNo_Leave(object sender, EventArgs e)
        {
            txtKapanName.Focus();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            DTabFound.Rows.Clear();
            //DTabFound.AcceptChanges();

            txtKapanName.Text = string.Empty;
            txtPacketNo.Text = string.Empty;
            txtTag.Text = string.Empty;

            string StrMappingType = "";
            StrMappingType = RbtSearchGIAMapping.Checked == true ? "GIA" : "LAB";

            DataSet DS = ObjTrn.GetData(StrMappingType);
            DTabNotFound = DS.Tables[0];
            DTabControlNo = DS.Tables[1];

            DTabFound = DTabNotFound.Clone();
            DTabSaveRec = DTabNotFound.Clone();

            DTabFound.AcceptChanges();
            DTabSaveRec.AcceptChanges();

            MainGridFound.DataSource = DTabFound;
            MainGridFound.Refresh();
            txtKapanName.Focus();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                DTabSaveRec.TableName = "Table1";
                string StockPacketControlMappingXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTabSaveRec.WriteXml(sw);
                    StockPacketControlMappingXml = sw.ToString();
                }

                string StrMappingType = "";
                if (RbtGIAMapping.Checked)
                    StrMappingType = "GIA";
                else
                    StrMappingType = "LAB";

                ArrayList AL = ObjTrn.Save(StrMappingType, StockPacketControlMappingXml);
                if (AL[1].ToString() == "SUCCESS")
                {
                    Global.Message(AL[1].ToString());
                    BtnClear.PerformClick();
                    this.Close();
                }
                else
                {
                    Global.MessageError(AL[1].ToString());
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearchKapan.Text.Equals(string.Empty) && txtSearchControlNo.Text.Equals(string.Empty))
            {
                Global.MessageError("Kapan Name Or ControlNo is Required");
                return;
            }

            string StrMappingType = "";
            if (RbtSearchGIAMapping.Checked)
                StrMappingType = "GIA";
            else if(RbtSearchLabMapping.Checked)
                StrMappingType = "LAB";
            else
                StrMappingType = "ALL";

            DataTable DTabSearch = new DataTable();
            DTabSearch = ObjTrn.GetSearchData(StrMappingType, txtSearchKapan.Text, txtSearchPacket.Text, txtSearchTag.Text, txtSearchControlNo.Text);
            MainSummry.DataSource = DTabSearch;
            MainSummry.Refresh();
        }

        private void txtSearchTag_Validated(object sender, EventArgs e)
        {
            if (Val.ISNumeric(txtSearchTag.Text) == true)
            {
                Char c = (Char)(64 + Val.ToInt(txtSearchTag.Text));
                txtSearchTag.Text = c.ToString();
            }
        }

        private void RbtGIAMapping_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string StrMappingType = "";
                StrMappingType = RbtGIAMapping.Checked == true ? "GIA" : "LAB";
                DataSet DS = ObjTrn.GetData(StrMappingType);
                DTabNotFound = DS.Tables[0];
                DTabControlNo = DS.Tables[1];

                if (RbtGIAMapping.Checked)
                {
                    GrdDetFound.Columns["GIACONTROLNUMBER"].Visible = true;
                    GrdDetFound.Columns["LABCONTROLNUMBER"].Visible = false;
                }
                else if (RbtLabMapping.Checked)
                {
                    GrdDetFound.Columns["LABCONTROLNUMBER"].Visible = true;
                    GrdDetFound.Columns["GIACONTROLNUMBER"].Visible = false;
                }
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message.ToString());
            }
        }
    }
}
