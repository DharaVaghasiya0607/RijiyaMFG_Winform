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
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using DevExpress.XtraPrinting;
using Google.API.Translate;

namespace AxoneMFGRJ.Masters
{
    public partial class FrmPenultyPoint : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOMST_Penulty ObjPoint = new BOMST_Penulty();

        DataTable DtabSize = new DataTable();


        public FrmPenultyPoint()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            deleteSelectedAmountToolStripMenuItem.Enabled = ObjPer.ISDELETE;  

            Fill();
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
            ObjFormEvent.ObjToDisposeList.Add(ObjPoint);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        public void Clear()
        {
            DtabSize.Rows.Clear();
            DtabSize.Rows.Add(DtabSize.NewRow());
            Fill();
        }

        public void Fill()
        {
            DtabSize = ObjPoint.Fill();
            DtabSize.Rows.Add(DtabSize.NewRow());
            MainGrid.DataSource = DtabSize;
            MainGrid.Refresh();

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                foreach (DataRow Dr in DtabSize.GetChanges().Rows)
                {
                    PenultyMasterProperty Property = new PenultyMasterProperty();

                    if (Val.ToString(Dr["POINTNAME"]).Length == 0)
                        continue;

                    Property.POINT_ID = Val.ToInt32(Dr["POINT_ID"]);
                    Property.POINTNAME = Val.ToString(Dr["POINTNAME"]);
                    Property.NOOFPCS = Val.ToInt32(Dr["NOOFPCS"]);
                    Property.RATE = Val.ToDouble(Dr["RATE"]);

                    Property = ObjPoint.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    DtabSize.Rows.Add(DtabSize.NewRow());
                    
                }
                DtabSize.AcceptChanges();


                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();

                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {                    
                    //txtItemGroupCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Penulty Point List", GrdDet);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        PenultyMasterProperty Property = new PenultyMasterProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.POINT_ID = Val.ToInt32(Drow["POINT_ID"]);
                        Property = ObjPoint.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabSize.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabSize.AcceptChanges();
                            Fill();
                        }
                        else
                        {
                            Global.Message("ERROR IN DELETE ENTRY");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
    }
}
