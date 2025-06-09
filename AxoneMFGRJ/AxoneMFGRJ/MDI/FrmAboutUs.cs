using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusLib.TableName;
using BusLib.Configuration;
using AxoneMFGRJ.MDI;
using BusLib.Master;
namespace AxoneMFGRJ.Utility
{
    public partial class FrmAboutUs : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();     
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        #region Constructor

        public FrmAboutUs()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSettingForPopup(this);
            AttachFormDefaultEvent();            
            this.ShowDialog();
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

        #region Form Validation

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
                this.Close();
            }
        }


        #endregion

    }
}
