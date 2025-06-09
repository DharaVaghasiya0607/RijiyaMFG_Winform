using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Utility
{
    public partial class FrmMumbaiTransfer : Form
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOHEL_ColumnMaster ObjMast = new BOHEL_ColumnMaster();

        public FrmMumbaiTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            try
            {
                Val.FormGeneralSetting(this);
                AttachFormDefaultEvent();
                if (MainGrid.RepositoryItems.Count == 2)
                {
                    ObjGridSelection = new BODevGridSelection();
                    ObjGridSelection.View = GrdDet;
                    ObjGridSelection.ClearSelection();
                    ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
                }
                else
                {
                    ObjGridSelection.ClearSelection();
                }
                GrdDet.Columns["COLSELECTCHECKBOX"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                if (ObjGridSelection != null)
                {
                    ObjGridSelection.ClearSelection();
                    ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
                }
                string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);

                if (Str != "")
                {
                    byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                    MemoryStream stream = new MemoryStream(byteArray);
                    GrdDet.RestoreLayoutFromStream(stream);

                }
                this.Show();
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }
        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        private void lblSaveLayout_Click(object sender, EventArgs e)
        {
            try
            {
                Stream str = new System.IO.MemoryStream();
                GrdDet.SaveLayoutToStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
                StreamReader reader = new StreamReader(str);
                string text = reader.ReadToEnd();

                int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDet.Name, text);
                if (IntRes != -1)
                {
                    Global.Message("Layout Successfully Saved");
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }

        }

        private void lblDefaultLayout_Click(object sender, EventArgs e)
        {
            try
            {
                int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDet.Name);
                if (IntRes != -1)
                {
                    Global.Message("Layout Successfully Deleted");
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string Strstatus = "";
                if (RbtAll.Checked == true)
                {
                    Strstatus = "ALL";
                }
                else
                {
                    Strstatus = "Pending";
                }
                DataTable DTab = ObjMast.MumbaiTransferGetData(Strstatus, Val.ToString(txtJangedNo.Text));
                DTab.Columns.Add(new DataColumn("ERROR", typeof(string)));
                foreach (DataRow DRow in DTab.Rows)
                {
                    string StrError = "";

                    if (Val.ToString(DRow["CARAT"]) == "")
                    {
                        StrError = "H_Carat is Missing";
                    }

                    DRow["ERROR"] = StrError;
                }

                DTab.AcceptChanges();
                MainGrid.DataSource = DTab;
                GrdDet.BestFitColumns();
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                
                DataTable DtDetail = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);
                if (DtDetail == null || DtDetail.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                foreach (DataRow DRow in DtDetail.Rows)
                {
                    Int64 pIntPacket_ID = Val.ToInt64(DRow["PACKET_ID"]);
                    if (pIntPacket_ID == 0)
                    {
                        Global.MessageError("You have not Proper detail");
                        return;
                    }

                    string pStrError = "";
                    pStrError = Val.ToString(DRow["ERROR"]);
                    if (Val.Val(DRow["H_CARAT"]) == 0)
                    {
                        if (Global.Confirm("H_Carat blank , Are You Sure For Entry") == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                    
                }

                DtDetail.TableName = "Table";
                string StrXmlForBombayTransfer = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DtDetail.WriteXml(sw);
                    StrXmlForBombayTransfer = sw.ToString();
                }

                if (Global.Confirm("Are You Sure Want To Transfer This Records ? ") == System.Windows.Forms.DialogResult.Yes)
                {
                    HelColumnMasterProperty Property = new HelColumnMasterProperty();

                    Property = ObjMast.MumbaiTransfer(Property, StrXmlForBombayTransfer);

                    Global.Message(Property.ReturnMessageDesc);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        BtnSearch_Click(sender, e);
                        ObjGridSelection.ClearSelection();
                    }
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void deleteSelectedRecordFromTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

                    if (Val.ToBoolean(Drow["ISTRANSFER"]) == true)
                    {
                        if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                        {
                            HelColumnMasterProperty Property = new HelColumnMasterProperty();

                            Property.Helium_ID = Val.ToInt64(Drow["PACKET_ID"]);

                            Property = ObjMast.DeleteformMumbaiTransfer(Property);

                            Global.Message(Property.ReturnMessageDesc);
                        }
                    }
                    else
                    {
                        Global.Message("This Record Can't Delete Because This Record Not Transfer");
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                bool pBoolIstransfer = Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "ISTRANSFER"));

                if (pBoolIstransfer == true)
                {
                    e.Appearance.BackColor = lblTransfer.BackColor;
                    e.Appearance.BackColor2 = lblTransfer.BackColor;
                }

                if (e.Column.FieldName == "CARAT")
                {
                   
                    double pDouCarat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "CARAT"));
                    double pDOuH_Carat = Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "H_CARAT"));
                    if (pDouCarat != pDOuH_Carat)
                    {
                        e.Appearance.BackColor = lblShape.BackColor;
                        e.Appearance.ForeColor = Color.Black;

                    }
                   
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }

        }
    }
}
