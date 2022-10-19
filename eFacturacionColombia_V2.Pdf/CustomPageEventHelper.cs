
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace eFacturacionColombia_V2.Pdf
{
    internal class CustomPageEventHelper: PdfPageEventHelper
    {
        private PdfContentByte cb;
        private PdfTemplate template;
        private Font font;

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            cb = writer.DirectContent;
            template = cb.CreateTemplate(50, 50);
            font = FontFactory.GetFont("Helvetica", 8, Font.BOLD);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            var pNumber = writer.PageNumber;
            var text = "Página " + pNumber.ToString() + " de ";
            var len = font.BaseFont.GetWidthPoint(text, font.Size);

            var pageSize = document.PageSize;

            cb.SetRGBColorFill(100, 100, 100);

            cb.BeginText();
            cb.SetFontAndSize(font.BaseFont, font.Size);
            cb.SetTextMatrix(document.LeftMargin, pageSize.GetBottom(document.BottomMargin));
            cb.ShowText(text);
            cb.EndText();

            cb.AddTemplate(template, document.LeftMargin + len, pageSize.GetBottom(document.BottomMargin));
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            template.BeginText();
            template.SetFontAndSize(font.BaseFont, font.Size);
            template.SetTextMatrix(0, 0);
            template.ShowText(writer.PageNumber.ToString());
            template.EndText();
        }
    }
}
