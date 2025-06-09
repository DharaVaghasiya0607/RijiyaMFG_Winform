using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmGIAAdvanceField : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        DataTable DtabDetail = new DataTable();

        public FrmGIAAdvanceField()
        {
            InitializeComponent();
        }

        public void ShowForm(DataTable pDTab)
        {
            Val.FormGeneralSettingForPopup(this);
            AttachFormDefaultEvent();

            DataTable DTab = new DataTable();
            DTab.Columns.Add(new DataColumn("PARAMETER"));
            DTab.Columns.Add(new DataColumn("VALUE"));

            if (pDTab.Rows.Count != 0 )
            {
                   
                foreach (DataColumn DCol in pDTab.Columns)
                {
                    DTab.Rows.Add(Val.ToString(DCol.ColumnName), pDTab.Rows[0][DCol.ColumnName]);
                }
            }
            
            MainGrid.DataSource = DTab;
            MainGrid.Refresh();

            if (pDTab.Rows.Count != 0)
            {
                string StrVideoUrl = Val.ToString(pDTab.Rows[0]["VIDEOURL"]);
                try
                {
                    WebBrowserVideo.Navigate(new Uri(StrVideoUrl));
                }
                catch (System.UriFormatException)
                {
                    return;
                }

                string StrCertUrl = Val.ToString(pDTab.Rows[0]["CERTURL"]);
                try
                {
                    WebBrowserCertificate.Navigate(new Uri(StrCertUrl));
                }
                catch (System.UriFormatException)
                {
                    return;
                }
            }

            this.ShowDialog();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            //ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        private void FrmGIAAdvanceField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}

