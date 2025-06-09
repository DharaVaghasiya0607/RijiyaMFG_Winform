using AxoneMFGRJ.Report;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Polish
{
    public partial class FrmIsAdditionalAssortment : Form
    {

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_KapanInward ObjADDKapan = new BOTRN_KapanInward();
        //BOFormPer ObjPer = new BOFormPer();
        BOTRN_SinglePacketCreate ObjKapan = new BOTRN_SinglePacketCreate();
        
        DataTable  DtabAdditional = new DataTable ();
        System.Windows.Forms.DialogResult mDialog;

        Int32 pIntShape_ID = 0;
        Int64 Kapan_ID = 0;
        string Kapanname = "";
        Guid pGuidInward_ID = Guid.Empty;
        Guid pGuidSizeAssort_ID = Guid.Empty;
      
        
        #region Property Settings

        public FrmIsAdditionalAssortment()
        {
            InitializeComponent();
        }

        public void ShowForm(Int32 SHAPE_ID, Int64 pIntKapan_ID, string pStrKapanName, Guid InwardID, Guid SIZEASSORT_ID)
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            pIntShape_ID = SHAPE_ID;
            Kapan_ID = pIntKapan_ID;
            Kapanname = pStrKapanName;
            pGuidInward_ID = InwardID;
            pGuidSizeAssort_ID = SIZEASSORT_ID;
            
            Fill();

            this.ShowDialog();
        }

        public void Fill()
        {
            DtabAdditional = ObjADDKapan.IsAdditionalGet(Kapanname);
            DataRow Dr = DtabAdditional.NewRow();
            DtabAdditional.Rows.Add(Dr);
            DtabAdditional.AcceptChanges();
            MainGridSize.DataSource = DtabAdditional;
            MainGridSize.Refresh();
        }

        public void AttachFormDefaultEvent()
        {

            ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjADDKapan);
            ObjFormEvent.ObjToDisposeList.Add(Val);
         
        }

        #endregion

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = ObjADDKapan.IsAdditionalGet(Kapanname);
                if (DTab.Rows.Count == 0)
                {
                    Global.MessageError("There Is No Data For Print");
                    return;
                }
                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowWithPrint("KapanDashboardPrint", DTab);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";


                foreach (DataRow Dr in DtabAdditional.Rows)
                {
                    TrnFancyRateProperty Property = new TrnFancyRateProperty();

                    
                    
                    if ( (Val.ToString(Dr["SIZENAME"]).Trim().Equals(string.Empty)) &&  (Val.ToString(Dr["CARAT"]).Trim().Equals(string.Empty)) && (Val.ToString(Dr["RATE"]).Trim().Equals(string.Empty)))
                        continue;
                    Property.DETAIL_ID = Val.ToInt64(Dr["DETAIL_ID"]);
                    Property.KAPAN_ID = Kapan_ID;
                    Property.KAPANNAME = Kapanname;
                    Property.SHAPE_ID = pIntShape_ID;
                    Property.INWARD_ID = pGuidInward_ID;
                    Property.SIZEASSROT_ID = pGuidSizeAssort_ID;
                    Property.SIZE_ID = Val.ToInt32(Dr["SIZE_ID"]);
                    Property.SIZENAME = Val.ToString(Dr["SIZENAME"]);
                    Property.RATE = Val.ToDouble(Dr["RATE"]);
                    Property.CARAT = Val.Val(Dr["CARAT"]);
                    Property.AMOUNT = Val.Val(Dr["AMOUNT"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property = ObjADDKapan.IsAdditionalSave(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Dr["DETAIL_ID"] = Property.ReturnValue;
                    Property = null;
                }
                DtabAdditional.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    if (GrdDetAs.RowCount > 1)
                    {
                        GrdDetAs.FocusedRowHandle = GrdDetAs.RowCount - 1;
                    }
                    this.Close();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void reptxtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjKapan.FindKapan();
                    FrmSearch.mColumnsToHide = "KAPAN_ID,MANAGER_ID,MANAGERNAME";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetAs.SetFocusedRowCellValue("KAPAN_ID", Val.ToString(FrmSearch.mDRow["KAPAN_ID"]));
                        GrdDetAs.SetFocusedRowCellValue("KAPANNAME", Val.ToString(FrmSearch.mDRow["KAPANNAME"]));
                        
                    }
                    
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                    //txtKapanCarat.Focus();
                }
               
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void reptxtxSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {

                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SIZECODE,SIZENAME,SIZE_ID";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "SIZENAME";
                    //FrmSearch.mColumnsToHide = "SIZE_ID";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.PARCEL_MIXSIZE);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetAs.SetFocusedRowCellValue("SIZENAME", Val.ToString(FrmSearch.mDRow["MIXSIZENAME"]));
                        GrdDetAs.SetFocusedRowCellValue("SIZE_ID", Val.ToString(FrmSearch.mDRow["MIXSIZE_ID"]));
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void reptxtShepe_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SEQUENCENO,SHAPECODE,SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
                    FrmSearch.mColumnsToHide = "SHAPE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDetAs.SetFocusedRowCellValue("SHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
                        GrdDetAs.SetFocusedRowCellValue("SHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPENAME"]));
                        
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

        private void reptxtRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GrdDetAs.GetFocusedDataRow();
                    if (Val.ToString(dr["KAPANNAME"]) != "" && GrdDetAs.IsLastRow)
                    {
                        DtabAdditional.Rows.Add(DtabAdditional.NewRow());
                        DtabAdditional.AcceptChanges();

                    }
                    else if (GrdDetAs.IsLastRow)
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

        private void GrdDetAs_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                if (e.Column.FieldName == "RATE")
                {
                    DataRow DRow = GrdDetAs.GetDataRow(e.RowHandle);
                    DtabAdditional.Rows[e.RowHandle]["AMOUNT"] = Math.Round(Val.Val(DRow["CARAT"]) * Val.Val(DRow["RATE"]), 2);
                    DtabAdditional.AcceptChanges();
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }

        }

        private void ReptxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GrdDetAs.GetFocusedDataRow();
                    if (Val.ToString(dr["SIZENAME"]) != "" && Val.Val(dr["CARAT"]) != 0 && Val.Val(dr["RATE"]) != 0 && GrdDetAs.IsLastRow)
                    {
                        DtabAdditional.Rows.Add(DtabAdditional.NewRow());
                        //DtabPara.AcceptChanges();

                    }
                    else if (GrdDetAs.IsLastRow)
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

    }

}
   
