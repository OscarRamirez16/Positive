using System;
using System.Collections.Generic;
using System.Drawing;

using iTextSharp.text.pdf.qrcode;

namespace eFacturacionColombia_V2.Pdf
{
    public static class QRHelper
    {
        public static Bitmap Crear(string cadena, int ancho, int alto)
        {
            var hints = new Dictionary<EncodeHintType, object>();
            hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

            var barcode = new CustomQR(cadena, ancho, alto, hints);

            return barcode.GetImage();
        }
    }
}