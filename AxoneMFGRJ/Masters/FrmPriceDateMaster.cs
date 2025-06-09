using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using DevExpress.XtraPrinting;
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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmPriceDateMaster : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_PriceDate ObjMast = new BOMST_PriceDate();
        DataTable DtabPara = new DataTable();


        public FrmPriceDateMaster()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            BtnClear_Click(null, null);
            Fill();
            this.Show();

            CmbParameterType.SelectedIndex = 0;
        }


        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg, string ColName2, string ColValue2)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabPara.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex && Val.ToString(row[ColName2]).ToUpper() == Val.ToString(ColValue2)
                         select row;


            if (Result.Any())
            {
                Global.Message("'" + CmbParameterType.SelectedItem + " " + StrMsg + "' ALREADY EXISTS.");
                return false;
            }
            return false;
        }

        private bool ValSave()
        {
            //if (txtItemGroupCode.Text.Trim().Length == 0)
            //{
            //    Global.Message("Group Code Is Required");
            //    txtItemGroupCode.Focus();
            //    return false;
            //}
            int IntCol = -1, IntRow = -1;
            foreach (DataRow dr in DtabPara.Rows)
            {
                //For Update Validation
                if (Val.ToString(dr["PRICEDATE"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Enter Price Date '");
                    IntCol = 0;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }

            }
            if (IntRow >= 0)
            {
                GrdDet.FocusedRowHandle = IntRow;
                GrdDet.FocusedColumn = GrdDet.VisibleColumns[IntCol];
                GrdDet.Focus();
                return true;
            }
            return false;
        }

        public void Clear()
        {
            Fill();
        }

        public void Fill()
        {
            DtabPara = ObjMast.Fill(Val.ToString(CmbParameterType.SelectedItem));

            DataRow Dr = DtabPara.NewRow();

            DtabPara.Rows.Add(Dr);
            MainGrid.DataSource = DtabPara;
            MainGrid.Refresh();

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport(CmbParameterType.SelectedItem.ToString() + "List", GrdDet);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //if (ValSave())
                //{
                //    return;
                //}

                //if (Global.Confirm("Are Your Sure To Save All Records ?") == System.Windows.Forms.DialogResult.No)
                //    return;

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";
                DataTable DTabChange = DtabPara.GetChanges();
                if (DTabChange != null)
                {

                    foreach (DataRow Dr in DTabChange.Rows)
                    {
                        if (Val.ToString(Dr["PRICEDATE"]) == "")
                        {
                            continue;
                        }
                        PriceDateMasterProperty Property = new PriceDateMasterProperty();

                        Property.PRICETYPE = Val.ToString(CmbParameterType.SelectedItem);

                        Property.PRICE_ID = Val.ToInt(Dr["PRICE_ID"]);
                        Property.PRICEDATE = Val.SqlDate(Val.ToString(Dr["PRICEDATE"]));
                        Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                        Property.REMARK = Val.ToString(Dr["REMARK"]);

                        Property = ObjMast.Save(Property);

                        ReturnMessageDesc = Property.ReturnMessageDesc;
                        ReturnMessageType = Property.ReturnMessageType;

                        Dr["PRICE_ID"] = Property.ReturnValue;

                        Property = null;
                    }
                    DtabPara.AcceptChanges();

                    Global.Message(ReturnMessageDesc);

                    if (ReturnMessageType == "SUCCESS")
                    {

                        Fill();
                        //BtnAdd_Click(null, null);

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
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Clicks == 2)
            {
                DataRow DR = GrdDet.GetDataRow(e.RowHandle);
                //FetchValue(DR);
                DR = null;
            }
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (!Val.ToString(dr["PRICEDATE"]).Equals(string.Empty) && GrdDet.IsLastRow)
                    {
                        DataRow Dr = DtabPara.NewRow();
                        DtabPara.Rows.Add(Dr);
                    }
                    else if (GrdDet.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void CmbParameterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            Fill();
        }

        private void MainGrid_Click(object sender, EventArgs e)
        {

        }

        
    }
}