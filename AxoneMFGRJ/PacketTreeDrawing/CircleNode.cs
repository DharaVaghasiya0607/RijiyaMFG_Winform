using System;
using System.Drawing;

namespace AxoneMFGRJ.PacketTreeDrawing
{
    class CircleNode : IDrawable
    {
        public static string PacketNo;
        // The string we will draw.
        public string Text;

        public CircleNode(string new_text)
        {
            Text = new_text;
        }

        // Return the size of the string plus a 10 pixel margin.
        public SizeF GetSize(Graphics gr, Font font)
        {
            return gr.MeasureString(Text, font) + new SizeF(10, 10);
        }

        // Draw the object centered at (x, y).
        void IDrawable.Draw(float x, float y, Graphics gr, Pen pen, Brush bg_brush, Brush text_brush, Font font)
        {
            // Fill and draw an ellipse at our location.
            SizeF my_size = GetSize(gr, font);
            /*RectangleF rect = new RectangleF(
                x - my_size.Width / 2,
                y - my_size.Height / 2,
                my_size.Width, my_size.Height);
            gr.FillEllipse(bg_brush, rect);
            gr.DrawEllipse(pen, rect);*/

            // Add : Narendra : 17-Jun-2017
            string[] str = Text.Split('\r');
            string PktNo = str[0].Trim().ToString();
            if (PktNo == PacketNo)
            {
                text_brush = Brushes.Gray;
                bg_brush = Brushes.White;
            }

            // End : narendra : 17-Jun-2017

            Rectangle rect = new Rectangle(Convert.ToInt32(x - (110 / 2)), Convert.ToInt32(y - (my_size.Height / 2)), 110, 55);
            gr.FillRectangle(bg_brush, rect);
            gr.DrawRectangle(pen, rect);


            // Draw the text.
            using (StringFormat string_format = new StringFormat())
            {
                
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Center;
                gr.DrawString(Text, font, text_brush, x, y, string_format);
            }
        }
    }
}
