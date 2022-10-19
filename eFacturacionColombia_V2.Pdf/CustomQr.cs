using System;
using System.Collections.Generic;
using System.Drawing;

using iTextSharp.text.pdf.qrcode;

namespace eFacturacionColombia_V2.Pdf
{
    public class CustomQR
    {
        public Color BackgroundColor { get; set; }
        public Color DataColor { get; set; }
        public Color EmptyColor { get; set; }

        private ByteMatrix byteMatrix;

        public CustomQR(string content, int width, int height, IDictionary<EncodeHintType, Object> hints)
        {
            var codeWriter = new QRCodeWriter();
            byteMatrix = codeWriter.Encode(content, width, height, hints);

            BackgroundColor = Color.White;
            DataColor = Color.Black;
            EmptyColor = Color.White;
        }

        public Bitmap GetImage()
        {
            var bmp = new Bitmap(byteMatrix.GetWidth(), byteMatrix.GetHeight());
            var graphics = Graphics.FromImage(bmp);
            graphics.Clear(this.BackgroundColor);

            var brushData = new SolidBrush(this.DataColor);
            var brushEmpty = new SolidBrush(this.EmptyColor);

            sbyte[][] imgNew = byteMatrix.GetArray();
            for (int i = 0; i <= imgNew.Length - 1; i++)
            {
                for (int j = 0; j <= imgNew[i].Length - 1; j++)
                {
                    if (imgNew[j][i] == 0)
                        graphics.FillRectangle(brushData, i, j, 1, 1);
                    else graphics.FillRectangle(brushEmpty, i, j, 1, 1);
                }
            }

            return bmp;
        }
    }
}