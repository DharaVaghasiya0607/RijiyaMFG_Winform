using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using DevExpress.XtraPrinting;
using Google.API.Translate;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AxoneMFGRJ.PacketTreeDrawing
{
    public partial class FrmPacketLookup : DevExpress.XtraEditors.XtraForm
    {
        // print
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr Dest, int Xaxes, int Yaxes, int Width, int Height, IntPtr Src, int XSrcaxes, int YSrcaxes, int dwRop);
        private Bitmap myBitmap;
        System.Drawing.Printing.PrintDocument myPrintDocument = new System.Drawing.Printing.PrintDocument();
        // end ; print

        private TreeNode<CircleNode> root = new TreeNode<CircleNode>(new CircleNode("  TEST "));
        DataTable DTabPacket = new DataTable();
        DataTable DTab = new DataTable();
        
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
       
        #region Property Settings

        public FrmPacketLookup()
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
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        #region Paint Function


        void GetPacketTree(string StrPacketID, TreeNode<CircleNode> root_node, string StrInnerSpace)
        {
            var VarQry = from Drow in DTab.AsEnumerable()
                         orderby Convert.ToInt32(Drow["ID"])
                         where Convert.ToString(Drow["PARENTPACKETNO"]) == StrPacketID
                         select Drow;
            if (VarQry.Any())
            {
                foreach (DataRow Dr in VarQry)
                {
                    string StrPacket = Dr["PACKETNO"].ToString();
                    string StrPacketNo = Dr["PACKETNO"].ToString();
                    string StrPacketCarat = Val.Format(Convert.ToDouble(Dr["Carat"]), "########0.000");
                    string StrLossCarat = Val.Format(Convert.ToDouble(Dr["Carat"]), "########0.000");

                    TreeNode<CircleNode> a_node = new TreeNode<CircleNode>(new CircleNode(StrPacketNo + "\r\nCts.  : " + StrPacketCarat + "\r\nLoss : " + StrLossCarat));
                    root_node.AddChild(a_node);

                    DataRow Drow = DTabPacket.NewRow();
                    foreach (DataColumn Dc in DTabPacket.Columns)
                        if (Dc.ColumnName.Equals("PACKETNO"))
                            Drow[Dc.ColumnName] = StrInnerSpace + Dr[Dc.ColumnName];
                        else
                            Drow[Dc.ColumnName] = Dr[Dc.ColumnName];

                    DTabPacket.Rows.Add(Drow);// Add : Narendra : 22-Jun-2017

                    GetPacketTree(StrPacket, a_node, StrInnerSpace + "  ");
                }
            }
        }


        private void ArrangeTree()
        {
            using (Graphics gr = this.CreateGraphics())
            {
                // Arrange the tree once to see how big it is.
                float xmin = 0, ymin = 0;
                if (root != null)
                    root.Arrange(gr, ref xmin, ref ymin);

                xmin = (panel1.Width - xmin) / 2;
                ymin = (panel1.Height - ymin) / 2;
                //xmin = (this.Width - xmin) / 2;
                //ymin = (this.Height - ymin) / 2;
                if (root != null)
                    root.Arrange(gr, ref xmin, ref ymin);

            }

            // Redraw.
            this.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            if (root != null)
                root.DrawTree(e.Graphics);

        }

        #endregion

        private void BtnPacketLookup_Click(object sender, EventArgs e)
        {
            DTab.Columns.Add(new DataColumn("ID", typeof(int)));
            DTab.Columns.Add(new DataColumn("PacketNo", typeof(string)));
            DTab.Columns.Add(new DataColumn("ParentPacketNo", typeof(string)));
            DTab.Columns.Add(new DataColumn("MainPacketNo", typeof(string)));
            DTab.Columns.Add(new DataColumn("Carat", typeof(double)));
            DTab.Columns.Add(new DataColumn("PacketType", typeof(string)));

            DTab.Rows.Add(1, "1A", null, "1A", 10, "Original");
            DTab.Rows.Add(2, "1B", "1A", "1A", 5, "Second");
            DTab.Rows.Add(3, "1C", "1A", "1A", 4, "Second");
            DTab.Rows.Add(4, "1D", "1B", "1A", 3, "Second");
            DTab.Rows.Add(5, "1E", "1B", "1A", 2, "Second");
            DTab.Rows.Add(6, "1F", "1C", "1A", 1, "Second");
            DTab.Rows.Add(7, "1G", "1C", "1A", 10, "Extra");
            DTab.Rows.Add(8, "1H", "1D", "1A", 5, "Second");
            DTab.Rows.Add(9, "1I", "1D", "1A", 2, "Second");
            DTab.Rows.Add(10, "1J", "1E", "1A", 3, "Rejection");
            DTab.Rows.Add(21, "1H", "1E", "1A", 5, "Second");

            DTabPacket = DTab.Clone();

            var Var = from Drow in DTab.AsEnumerable()
                      select Drow;
            panel1.Width = 135 * Convert.ToInt32(Var.Count());
            panel1.Height = 71 * Convert.ToInt32(DTab.DefaultView.ToTable(true, "ParentPacketNo").Rows.Count);
            // for panedl width

            string StrMainPacketID = DTab.Rows[0]["MainPacketNo"].ToString();

            CircleNode.PacketNo = StrMainPacketID;

            var VarQry = from Drow in DTab.AsEnumerable()
                         where Convert.ToString(Drow["PacketNo"]) == StrMainPacketID
                         select Drow;
            string StrPacketID = string.Empty;
            string StrPacketNo = string.Empty;
            string StrPacketCarat = string.Empty;
            string StrLossCarat = string.Empty;

            if (VarQry.Any())
            {
                StrPacketID = VarQry.FirstOrDefault()["PACKETNO"].ToString();
                StrPacketNo = VarQry.FirstOrDefault()["PACKETNO"].ToString();
                StrPacketCarat = Val.Format(VarQry.FirstOrDefault()["Carat"], "########0.000");
                StrLossCarat = Val.Format(VarQry.FirstOrDefault()["Carat"], "########0.000");
                DataRow Drow = DTabPacket.NewRow();
                foreach (DataColumn Dc in DTabPacket.Columns)
                    Drow[Dc.ColumnName] = VarQry.FirstOrDefault()[Dc.ColumnName];
                DTabPacket.Rows.Add(Drow);// Add : Narendra : 22-Jun-2017
            }


            root = new TreeNode<CircleNode>(new CircleNode(StrPacketNo + "\r\nCts.  : " + StrPacketCarat + "\r\nLoss : " + StrLossCarat));

            GetPacketTree(StrPacketID, root, "  ");

            ArrangeTree();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (root == null)
            {
                return;
            }

            SaveFileDialog svDialog = new SaveFileDialog();
            svDialog.DefaultExt = ".pdf";
            svDialog.Title = "PACKET TREE IN PDF";
            svDialog.FileName = "PacketTree_";
            svDialog.Filter = "PDF (*.PDF)|*.PDF"; ;
            if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                string Filepath = svDialog.FileName;

                this.UseWaitCursor = true;
                Graphics myGraphics1 = panel1.CreateGraphics();
                myBitmap = new Bitmap(panel1.Width, panel1.Height, panel1.CreateGraphics());
                Graphics myGraphics2 = Graphics.FromImage(myBitmap);
                IntPtr HandleDeviceContext1 = myGraphics1.GetHdc();
                IntPtr HandleDeviceContext2 = myGraphics2.GetHdc();
                BitBlt(HandleDeviceContext2, 0, 0, panel1.ClientRectangle.Width,
                  panel1.ClientRectangle.Height, HandleDeviceContext1, 0, 0, 13369376);//
                myGraphics1.ReleaseHdc(HandleDeviceContext1);
                myGraphics2.ReleaseHdc(HandleDeviceContext2);
                myPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(myPrintDocument_PrintPage);
                //myPrintDocument.Print();

                // step 1: creation of a document-object
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 10, 10);

                document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                // step 2:
                // we create a writer that listens to the document
                // and directs a PDF-stream to a file

                PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(Filepath, System.IO.FileMode.Create));

                // step 3: we open the document
                document.Open();

                // step 4: 
                //Get the image of the form.
                //System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(form.Width, form.Height);
                //this.DrawToBitmap(form, myBitmap);
                //Add image to the document.
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(myBitmap, iTextSharp.text.BaseColor.WHITE);
                img.ScaleToFit(9450, 2130);//(832, 595);
                document.Add(img);

                //bRet = true;
                // step 5: we close the document
                document.Close();
                this.UseWaitCursor = false;
                // end pdf                
            }


        }

        private void myPrintDocument_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(myBitmap, 0, 0);
        }

    }
}
