using System;
using System.IO;
using System.Linq;

using eFacturacionColombia_V2.Tipos.Standard;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace eFacturacionColombia_V2.Pdf
{
    public class NotaDebitoPdf : DocumentoPdf
    {
        public DebitNoteType DebitNote { get; private set; }

        public override string Tipo { get; } = TIPO_NOTA_DEBITO;

        public NotaDebitoPdf(DebitNoteType debitNote)
        {
            DebitNote = debitNote;
        }


        public override byte[] Generar()
        {
            var docPdf = new Document(PageSize.A4, 25, 25, 25, 25);
            var streamPdf = new MemoryStream();
            var writer = PdfWriter.GetInstance(docPdf, streamPdf);
            writer.PageEvent = new CustomPageEventHelper();
            docPdf.Open();

            var encabezado = new PdfPTable(3);
            encabezado.HorizontalAlignment = Element.ALIGN_LEFT;
            encabezado.WidthPercentage = 100;
            encabezado.SetWidths(new float[] { 2, 4, 2 });
            encabezado.DefaultCell.Border = Rectangle.NO_BORDER;

            var cEmpty5 = new PdfPCell(new Phrase(" "))
            {
                Border = Rectangle.NO_BORDER,
                FixedHeight = 5
            };
            var cEmpty15 = new PdfPCell(new Phrase(" "))
            {
                Border = Rectangle.NO_BORDER,
                FixedHeight = 15
            };

            if (RutaLogo != null)
            {
                var logo = Image.GetInstance(RutaLogo);
                logo.BackgroundColor = BaseColor.WHITE;
                logo.Alignment = Element.ALIGN_MIDDLE;

                encabezado.AddCell(logo);
            }
            else
            {
                encabezado.AddCell(cEmpty15);
            }

            var tEmisor = new PdfPTable(1);
            tEmisor.WidthPercentage = 100;
            tEmisor.DefaultCell.Border = Rectangle.NO_BORDER;

            var fnt1 = FontFactory.GetFont("Helvetica", 10, Font.BOLD); //f12
            string nombreEmisor = DebitNote.AccountingSupplierParty.Party.PartyName[0].Name.Value;
            var cNombreEmisor = new PdfPCell(new Phrase(nombreEmisor, fnt1));
            cNombreEmisor.Border = Rectangle.NO_BORDER;
            cNombreEmisor.HorizontalAlignment = Element.ALIGN_CENTER;

            var fnt2 = FontFactory.GetFont("Helvetica", 9, Font.BOLD); //f11
            var nitEmisor = DebitNote.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.Value;
            var dvEmisor = DebitNote.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.schemeID;
            var cNitEmisor = new PdfPCell(new Phrase("NIT: " + nitEmisor + "-" + dvEmisor, fnt2));
            cNitEmisor.Border = Rectangle.NO_BORDER;
            cNitEmisor.HorizontalAlignment = Element.ALIGN_CENTER;

            var fnt3 = FontFactory.GetFont("Helvetica", 8, Font.NORMAL); //f9
            var cEncabezado = new PdfPCell(new Phrase(TextoEncabezado, fnt3));
            cEncabezado.Border = Rectangle.NO_BORDER;
            cEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

            var direccionEmisor = DebitNote.AccountingSupplierParty.Party.PhysicalLocation.Address.Country.Name.Value + ", " +
                DebitNote.AccountingSupplierParty.Party.PhysicalLocation.Address.CountrySubentity?.Value + ", " +
                DebitNote.AccountingSupplierParty.Party.PhysicalLocation.Address.CityName?.Value + " \r\n" +
                DebitNote.AccountingSupplierParty.Party.PhysicalLocation.Address.AddressLine[0]?.Line.Value;
            var cDireccionEmisor = new PdfPCell(new Phrase(direccionEmisor.ToUpper().Trim(), fnt3));
            cDireccionEmisor.Border = Rectangle.NO_BORDER;
            cDireccionEmisor.HorizontalAlignment = Element.ALIGN_CENTER;

            var correoEmisor = DebitNote.AccountingSupplierParty.Party.Contact?.ElectronicMail?.Value;
            var cCorreoEmisor = new PdfPCell(new Phrase(correoEmisor, fnt3));
            cCorreoEmisor.Border = Rectangle.NO_BORDER;
            cCorreoEmisor.HorizontalAlignment = Element.ALIGN_CENTER;

            var telefonoEmisor = DebitNote.AccountingSupplierParty.Party.Contact?.Telephone?.Value;
            var cTelefonoEmisor = new PdfPCell(new Phrase(telefonoEmisor, fnt3));
            cTelefonoEmisor.Border = Rectangle.NO_BORDER;
            cTelefonoEmisor.HorizontalAlignment = Element.ALIGN_CENTER;

            tEmisor.AddCell(cEmpty5);
            tEmisor.AddCell(cNombreEmisor);
            tEmisor.AddCell(cNitEmisor);
            tEmisor.AddCell(cEmpty5);
            tEmisor.AddCell(cEncabezado);
            tEmisor.AddCell(cEmpty15);
            tEmisor.AddCell(cDireccionEmisor);
            tEmisor.AddCell(cCorreoEmisor);
            tEmisor.AddCell(cTelefonoEmisor);

            encabezado.AddCell(tEmisor);

            var tInfoGeneral = new PdfPTable(1);
            tInfoGeneral.WidthPercentage = 100;
            tInfoGeneral.DefaultCell.Border = Rectangle.NO_BORDER;

            var fnt4 = FontFactory.GetFont("Helvetica", 7, Font.NORMAL); //f10
            var ciTipoDoc = new PdfPCell(new Phrase(Tipo, fnt4));
            ciTipoDoc.Border = Rectangle.NO_BORDER;
            ciTipoDoc.HorizontalAlignment = Element.ALIGN_CENTER;
            ciTipoDoc.PaddingBottom = 5;

            var fnt4x = FontFactory.GetFont("Helvetica", 8, Font.NORMAL); //f10
            var vNumeroNota = DebitNote.ID.Value;
            var cNumeroNota = new PdfPCell(new Phrase(vNumeroNota, fnt4x));
            cNumeroNota.BackgroundColor = BaseColor.WHITE;
            cNumeroNota.BorderColor = BaseColor.GRAY;
            cNumeroNota.Border = Rectangle.BOX;
            cNumeroNota.HorizontalAlignment = Element.ALIGN_CENTER;
            cNumeroNota.PaddingTop = 5;
            cNumeroNota.PaddingBottom = 7;

            if (string.IsNullOrEmpty(TextoQR)) TextoQR = DebitNote.UUID.Value;
            var bmQr = QRHelper.Crear(TextoQR, 500, 500);
            var imageQr = Image.GetInstance(bmQr, BaseColor.WHITE);
            imageQr.Border = Rectangle.NO_BORDER;
            imageQr.Alignment = Element.ALIGN_CENTER;

            tInfoGeneral.AddCell(ciTipoDoc);
            tInfoGeneral.AddCell(cNumeroNota);
            tInfoGeneral.AddCell(imageQr);

            encabezado.AddCell(tInfoGeneral);

            docPdf.Add(encabezado);

            docPdf.Add(new Phrase("   ", fnt3));

            var encabezado2 = new PdfPTable(3);
            encabezado2.HorizontalAlignment = Element.ALIGN_LEFT;
            encabezado2.WidthPercentage = 100;
            encabezado2.SetWidths(new float[] { 4, 1.25f, 1.25f });
            encabezado2.SpacingAfter = 10;

            var nombreAdquiriente = DebitNote.AccountingCustomerParty[0].Party.PartyName[0].Name.Value;
            var identificacionAdquiriente = DebitNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.Value;
            var direccionAdquiriente = DebitNote.AccountingCustomerParty[0].Party.PhysicalLocation.Address.Country.Name.Value + ", " +
                DebitNote.AccountingCustomerParty[0].Party.PhysicalLocation.Address.CountrySubentity?.Value + ", " +
                DebitNote.AccountingCustomerParty[0].Party.PhysicalLocation.Address.CityName?.Value + ", " +
                DebitNote.AccountingCustomerParty[0].Party.PhysicalLocation.Address.AddressLine[0]?.Line.Value;
            var correoAdquiriente = DebitNote.AccountingCustomerParty[0].Party.Contact?.ElectronicMail?.Value;
            var telefonoAdquiriente = DebitNote.AccountingCustomerParty[0].Party.Contact?.Telephone?.Value;

            var iAdquiriente = "ADQUIRIENTE: " + nombreAdquiriente + "\r\n" +
                "DOCUMENTO DE IDENTIFICACIÓN: " + identificacionAdquiriente + "\r\n" +
                "DIRECCIÓN: " + direccionAdquiriente.ToUpper().Replace("\r\n", " ");
            if (!string.IsNullOrEmpty(correoAdquiriente))
                iAdquiriente += "\r\nCORREO ELECTRÓNICO: " + correoAdquiriente;
            if (!string.IsNullOrEmpty(telefonoAdquiriente))
                iAdquiriente += "\r\nNÚMERO TELEFÓNICO: " + telefonoAdquiriente;

            var fnt5 = FontFactory.GetFont("Helvetica", 8, Font.NORMAL); //f9
            var cAdquiriente = new PdfPCell(new Phrase(iAdquiriente, fnt5));
            cAdquiriente.BorderColor = BaseColor.GRAY;
            cAdquiriente.Border = Rectangle.BOX;
            cAdquiriente.VerticalAlignment = Element.ALIGN_MIDDLE;
            cAdquiriente.ExtraParagraphSpace = 3;
            cAdquiriente.Padding = 4;
            cAdquiriente.PaddingRight = 7;
            cAdquiriente.PaddingLeft = 7;
            cAdquiriente.Rowspan = 2;

            var fechaEmision = DebitNote.IssueDate.Value;
            var horaEmision = DebitNote.IssueTime.Value.Split(new char[] { '+', '-' })[0];
            var iEmisionNota = "FECHA DE EMISIÓN\r\n" + fechaEmision.ToString("yyyy-MM-dd") + "\r\n" + horaEmision;
            var fnt6 = FontFactory.GetFont("Helvetica", 8, Font.NORMAL); //f9
            var cEmisionNota = new PdfPCell(new Phrase(iEmisionNota, fnt6));
            cEmisionNota.BorderColor = BaseColor.GRAY;
            cEmisionNota.Border = Rectangle.BOX;
            cEmisionNota.HorizontalAlignment = Element.ALIGN_CENTER;
            cEmisionNota.VerticalAlignment = Element.ALIGN_MIDDLE;
            cEmisionNota.Padding = 4;

            var facturaReferencia = DebitNote.BillingReference[0]?.InvoiceDocumentReference?.ID.Value;
            var iFacturaReferencia = "FACTURA DE REFERENCIA\r\n" + facturaReferencia;
            var cFacturaReferencia = new PdfPCell(new Phrase(iFacturaReferencia, fnt6));
            cFacturaReferencia.BorderColor = BaseColor.GRAY;
            cFacturaReferencia.Border = Rectangle.BOX;
            cFacturaReferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            cFacturaReferencia.VerticalAlignment = Element.ALIGN_MIDDLE;
            cFacturaReferencia.Padding = 4;

            var cude = DebitNote.UUID.Value;
            var iCude = "CUDE\r\n" + cude;
            var cCude = new PdfPCell(new Phrase(iCude, fnt6));
            cCude.BorderColor = BaseColor.GRAY;
            cCude.Border = Rectangle.BOX;
            cCude.HorizontalAlignment = Element.ALIGN_CENTER;
            cCude.VerticalAlignment = Element.ALIGN_MIDDLE;
            cCude.Padding = 4;
            cCude.Colspan = 2;

            encabezado2.AddCell(cAdquiriente);
            encabezado2.AddCell(cEmisionNota);
            encabezado2.AddCell(cFacturaReferencia);
            encabezado2.AddCell(cCude);

            docPdf.Add(encabezado2);

            // agregar líneas
            var totalLineas = DebitNote.DebitNoteLine.Count();
            if (totalLineas > LINEAS_PRIMERA_PAGINA_CON_TOTALES)
            {
                agregarLineas(docPdf, 0, LINEAS_PRIMERA_PAGINA_SIN_TOTALES);

                if (totalLineas > LINEAS_PRIMERA_PAGINA_SIN_TOTALES)
                {
                    bool masPaginas = true;
                    int inicio = LINEAS_PRIMERA_PAGINA_SIN_TOTALES;

                    do
                    {
                        docPdf.NewPage();

                        agregarLineas(docPdf, inicio, LINEAS_PAGINAS_EXTRA);

                        inicio += LINEAS_PAGINAS_EXTRA;
                        masPaginas = (inicio < totalLineas);
                    }
                    while (masPaginas);

                    var lineasUltimaPagina = (totalLineas - LINEAS_PRIMERA_PAGINA_SIN_TOTALES) % LINEAS_PAGINAS_EXTRA;
                    var difLineasPrimeraPagina = LINEAS_PRIMERA_PAGINA_SIN_TOTALES - LINEAS_PRIMERA_PAGINA_SIN_TOTALES;

                    if (lineasUltimaPagina > LINEAS_PAGINAS_EXTRA - difLineasPrimeraPagina)
                    {
                        docPdf.NewPage();
                    }
                }
                else
                {
                    docPdf.NewPage();
                }
            }
            else
            {
                agregarLineas(docPdf, 0, LINEAS_PRIMERA_PAGINA_CON_TOTALES);
            }

            // tabla de totales
            var tTotales = new PdfPTable(3);
            tTotales.WidthPercentage = 100;
            tTotales.SetWidths(new float[] { 4.5f, 3f, 1.5f });

            var iDatos = (TextoAdicional + "\r\n\r\n" + TextoConstancia + "\r\n\r\n" + TextoResolucion).Trim();
            var fnt7 = FontFactory.GetFont("Helvetica", 8, Font.NORMAL); //f9
            var cDatos = new PdfPCell(new Phrase(iDatos, fnt7));
            cDatos.BorderColor = BaseColor.GRAY;
            cDatos.Border = Rectangle.BOX;
            cDatos.HorizontalAlignment = Element.ALIGN_LEFT;
            cDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
            cDatos.Rowspan = 11;
            cDatos.Padding = 4;
            cDatos.PaddingRight = 7;
            cDatos.PaddingLeft = 7;

            var fnt8 = FontFactory.GetFont("Helvetica", 9, Font.BOLD); //f10
            var ciSubtotalPU = new PdfPCell(new Phrase("Subtotal Precio Unitario (=)", fnt8));
            ciSubtotalPU.BorderColor = BaseColor.GRAY;
            ciSubtotalPU.Border = Rectangle.BOX;
            ciSubtotalPU.BorderWidthBottom = 0f;
            ciSubtotalPU.HorizontalAlignment = Element.ALIGN_CENTER;
            ciSubtotalPU.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciSubtotalPU.Padding = 5;

            var fnt9 = FontFactory.GetFont("Helvetica", 9, Font.NORMAL); //f10
            var descuentosDetalle = DebitNote.DebitNoteLine.Sum(l => l.AllowanceCharge?.Sum(ac => !ac.ChargeIndicator.Value ? ac.Amount.Value : 0M));
            var subtotalPU = DebitNote.RequestedMonetaryTotal.LineExtensionAmount.Value + (descuentosDetalle.HasValue ? descuentosDetalle.Value : 0M);
            var ivSubtotaoPU = subtotalPU.ToString("#,###,##0.##");
            var cvSubtotalPU = new PdfPCell(new Phrase(ivSubtotaoPU, fnt9));
            cvSubtotalPU.BorderColor = BaseColor.GRAY;
            cvSubtotalPU.Border = Rectangle.BOX;
            cvSubtotalPU.BorderWidthBottom = 0f;
            cvSubtotalPU.HorizontalAlignment = Element.ALIGN_CENTER;
            cvSubtotalPU.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvSubtotalPU.Padding = 5;

            var ciDescuentosDetalle = new PdfPCell(new Phrase("Descuentos detalle (-)", fnt8));
            ciDescuentosDetalle.BorderColor = BaseColor.GRAY;
            ciDescuentosDetalle.Border = Rectangle.BOX;
            ciDescuentosDetalle.BorderWidthTop = 0f;
            ciDescuentosDetalle.BorderWidthBottom = 0f;
            ciDescuentosDetalle.HorizontalAlignment = Element.ALIGN_CENTER;
            ciDescuentosDetalle.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciDescuentosDetalle.Padding = 5;

            var ivDescuentosDetalle = descuentosDetalle.HasValue ? descuentosDetalle.Value.ToString("#,###,##0.##") : "0";
            var cvDescuentosDetalle = new PdfPCell(new Phrase(ivDescuentosDetalle, fnt9));
            cvDescuentosDetalle.BorderColor = BaseColor.GRAY;
            cvDescuentosDetalle.Border = Rectangle.BOX;
            cvDescuentosDetalle.BorderWidthTop = 0f;
            cvDescuentosDetalle.BorderWidthBottom = 0f;
            cvDescuentosDetalle.HorizontalAlignment = Element.ALIGN_CENTER;
            cvDescuentosDetalle.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvDescuentosDetalle.Padding = 5;

            var ciRecargosDetalle = new PdfPCell(new Phrase("Recargos detalle (+)", fnt8));
            ciRecargosDetalle.BorderColor = BaseColor.GRAY;
            ciRecargosDetalle.Border = Rectangle.BOX;
            ciRecargosDetalle.BorderWidthTop = 0f;
            ciRecargosDetalle.HorizontalAlignment = Element.ALIGN_CENTER;
            ciRecargosDetalle.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciRecargosDetalle.Padding = 5;

            var recargosDetalle = DebitNote.DebitNoteLine.Sum(l => l.AllowanceCharge?.Sum(ac => ac.ChargeIndicator.Value ? ac.Amount.Value : 0M));
            var iRecargosDetalle = recargosDetalle.HasValue ? recargosDetalle.Value.ToString("#,###,##0.##") : "0";
            var cvRecargosDetalle = new PdfPCell(new Phrase(iRecargosDetalle, fnt9));
            cvRecargosDetalle.BorderColor = BaseColor.GRAY;
            cvRecargosDetalle.Border = Rectangle.BOX;
            cvRecargosDetalle.BorderWidthTop = 0f;
            cvRecargosDetalle.HorizontalAlignment = Element.ALIGN_CENTER;
            cvRecargosDetalle.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvRecargosDetalle.Padding = 5;

            var ciSubtotalNG = new PdfPCell(new Phrase("Subtotal No Gravados (=)", fnt8));
            ciSubtotalNG.BorderColor = BaseColor.GRAY;
            ciSubtotalNG.Border = Rectangle.BOX;
            ciSubtotalNG.BorderWidthBottom = 0f;
            ciSubtotalNG.HorizontalAlignment = Element.ALIGN_CENTER;
            ciSubtotalNG.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciSubtotalNG.Padding = 5;

            var vSubtotalNG = Math.Abs(subtotalPU - DebitNote.RequestedMonetaryTotal.TaxExclusiveAmount.Value);
            var iSubtotalNG = vSubtotalNG.ToString("#,###,##0.##");
            var cvSubtotalNG = new PdfPCell(new Phrase(iSubtotalNG, fnt9));
            cvSubtotalNG.BorderColor = BaseColor.GRAY;
            cvSubtotalNG.Border = Rectangle.BOX;
            cvSubtotalNG.BorderWidthBottom = 0f;
            cvSubtotalNG.HorizontalAlignment = Element.ALIGN_CENTER;
            cvSubtotalNG.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvSubtotalNG.Padding = 5;

            var ciSubtotalBG = new PdfPCell(new Phrase("Subtotal Base Gravable (=)", fnt8));
            ciSubtotalBG.BorderColor = BaseColor.GRAY;
            ciSubtotalBG.Border = Rectangle.BOX;
            ciSubtotalBG.BorderWidthTop = 0f;
            ciSubtotalBG.BorderWidthBottom = 0f;
            ciSubtotalBG.HorizontalAlignment = Element.ALIGN_CENTER;
            ciSubtotalBG.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciSubtotalBG.Padding = 5;

            var iSubtotalBG = DebitNote.RequestedMonetaryTotal.TaxExclusiveAmount.Value.ToString("#,###,##0.##");
            var cvSubtotalBG = new PdfPCell(new Phrase(iSubtotalBG, fnt9));
            cvSubtotalBG.BorderColor = BaseColor.GRAY;
            cvSubtotalBG.Border = Rectangle.BOX;
            cvSubtotalBG.BorderWidthTop = 0f;
            cvSubtotalBG.BorderWidthBottom = 0f;
            cvSubtotalBG.HorizontalAlignment = Element.ALIGN_CENTER;
            cvSubtotalBG.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvSubtotalBG.Padding = 5;

            var ciTotalImpuesto = new PdfPCell(new Phrase("Total impuesto (+)", fnt8));
            ciTotalImpuesto.BorderColor = BaseColor.GRAY;
            ciTotalImpuesto.Border = Rectangle.BOX;
            ciTotalImpuesto.BorderWidthTop = 0f;
            ciTotalImpuesto.BorderWidthBottom = 0f;
            ciTotalImpuesto.HorizontalAlignment = Element.ALIGN_CENTER;
            ciTotalImpuesto.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciTotalImpuesto.Padding = 5;

            var totalImpuesto = DebitNote.TaxTotal?.Sum(t => t.TaxAmount.Value);
            var iTotalImpuesto = totalImpuesto.HasValue ? totalImpuesto.Value.ToString("#,###,##0.##") : "0";
            var cvTotalImpuesto = new PdfPCell(new Phrase(iTotalImpuesto, fnt9));
            cvTotalImpuesto.BorderColor = BaseColor.GRAY;
            cvTotalImpuesto.Border = Rectangle.BOX;
            cvTotalImpuesto.BorderWidthTop = 0f;
            cvTotalImpuesto.BorderWidthBottom = 0f;
            cvTotalImpuesto.HorizontalAlignment = Element.ALIGN_CENTER;
            cvTotalImpuesto.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvTotalImpuesto.Padding = 5;

            var ciTotalMI = new PdfPCell(new Phrase("Total más impuesto (=)", fnt8));
            ciTotalMI.BorderColor = BaseColor.GRAY;
            ciTotalMI.Border = Rectangle.BOX;
            ciTotalMI.BorderWidthTop = 0f;
            ciTotalMI.HorizontalAlignment = Element.ALIGN_CENTER;
            ciTotalMI.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciTotalMI.Padding = 5;

            var totalMI = DebitNote.RequestedMonetaryTotal.TaxInclusiveAmount.Value;
            var iTotalMI = totalMI.ToString("#,###,##0.##");
            var cvTotalMI = new PdfPCell(new Phrase(iTotalMI, fnt9));
            cvTotalMI.BorderColor = BaseColor.GRAY;
            cvTotalMI.Border = Rectangle.BOX;
            cvTotalMI.BorderWidthTop = 0f;
            cvTotalMI.HorizontalAlignment = Element.ALIGN_CENTER;
            cvTotalMI.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvTotalMI.Padding = 5;

            var ciDescuentoGlobal = new PdfPCell(new Phrase("Descuento Global (-)", fnt8));
            ciDescuentoGlobal.BorderColor = BaseColor.GRAY;
            ciDescuentoGlobal.Border = Rectangle.BOX;
            ciDescuentoGlobal.BorderWidthBottom = 0f;
            ciDescuentoGlobal.HorizontalAlignment = Element.ALIGN_CENTER;
            ciDescuentoGlobal.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciDescuentoGlobal.Padding = 5;

            var descuentoGlobal = DebitNote.RequestedMonetaryTotal.AllowanceTotalAmount?.Value;
            var iDescuentoGlobal = descuentoGlobal.HasValue ? descuentoGlobal.Value.ToString("#,###,##0.##") : "0";
            var cvDescuentoGlobal = new PdfPCell(new Phrase(iDescuentoGlobal, fnt9));
            cvDescuentoGlobal.BorderColor = BaseColor.GRAY;
            cvDescuentoGlobal.Border = Rectangle.BOX;
            cvDescuentoGlobal.BorderWidthBottom = 0f;
            cvDescuentoGlobal.HorizontalAlignment = Element.ALIGN_CENTER;
            cvDescuentoGlobal.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvDescuentoGlobal.Padding = 5;

            var ciRecargoGlobal = new PdfPCell(new Phrase("Recargo Global (+)", fnt8));
            ciRecargoGlobal.BorderColor = BaseColor.GRAY;
            ciRecargoGlobal.Border = Rectangle.BOX;
            ciRecargoGlobal.BorderWidthTop = 0f;
            ciRecargoGlobal.HorizontalAlignment = Element.ALIGN_CENTER;
            ciRecargoGlobal.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciRecargoGlobal.Padding = 5;

            var recargoGlobal = DebitNote.RequestedMonetaryTotal.ChargeTotalAmount?.Value;
            var iRecargoGlobal = recargoGlobal.HasValue ? recargoGlobal.Value.ToString("#,###,##0.##") : "0";
            var cvRecargoGlobal = new PdfPCell(new Phrase(iRecargoGlobal, fnt9));
            cvRecargoGlobal.BorderColor = BaseColor.GRAY;
            cvRecargoGlobal.Border = Rectangle.BOX;
            cvRecargoGlobal.BorderWidthTop = 0f;
            cvRecargoGlobal.HorizontalAlignment = Element.ALIGN_CENTER;
            cvRecargoGlobal.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvRecargoGlobal.Padding = 5;

            var ciAnticipo = new PdfPCell(new Phrase("Anticipo (-)", fnt8));
            ciAnticipo.BorderColor = BaseColor.GRAY;
            ciAnticipo.Border = Rectangle.BOX;
            ciAnticipo.HorizontalAlignment = Element.ALIGN_CENTER;
            ciAnticipo.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciAnticipo.Padding = 4;

            var anticipo = DebitNote.RequestedMonetaryTotal.PrepaidAmount?.Value ?? 0;
            var iAnticipo = anticipo.ToString("#,###,##0.##");
            var cvAnticipo = new PdfPCell(new Phrase(iAnticipo, fnt9));
            cvAnticipo.BorderColor = BaseColor.GRAY;
            cvAnticipo.Border = Rectangle.BOX;
            cvAnticipo.HorizontalAlignment = Element.ALIGN_CENTER;
            cvAnticipo.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvAnticipo.Padding = 4;

            var ciTotalNeto = new PdfPCell(new Phrase("Valor Total", fnt8));
            ciTotalNeto.BorderColor = BaseColor.GRAY;
            ciTotalNeto.Border = Rectangle.BOX;
            ciTotalNeto.HorizontalAlignment = Element.ALIGN_CENTER;
            ciTotalNeto.VerticalAlignment = Element.ALIGN_MIDDLE;
            ciTotalNeto.Padding = 5;

            var vTotalAP = DebitNote.RequestedMonetaryTotal.PayableAmount.Value;
            var iTotalNeto = vTotalAP.ToString("#,###,##0.##");
            var cvTotalNeto = new PdfPCell(new Phrase(iTotalNeto, fnt8));
            cvTotalNeto.BorderColor = BaseColor.GRAY;
            cvTotalNeto.Border = Rectangle.BOX;
            cvTotalNeto.HorizontalAlignment = Element.ALIGN_CENTER;
            cvTotalNeto.VerticalAlignment = Element.ALIGN_MIDDLE;
            cvTotalNeto.Padding = 5;

            var moneda = DebitNote.DocumentCurrencyCode.Value;
            var iMontoLetras = montoALetras(vTotalAP, moneda).ToUpper();
            var cMontoLetras = new PdfPCell(new Phrase(iMontoLetras, fnt7));
            cMontoLetras.BorderColor = BaseColor.GRAY;
            cMontoLetras.Border = Rectangle.BOX;
            cMontoLetras.HorizontalAlignment = Element.ALIGN_CENTER;
            cMontoLetras.VerticalAlignment = Element.ALIGN_MIDDLE;
            cMontoLetras.Padding = 7;
            cMontoLetras.Colspan = 3;

            tTotales.AddCell(cDatos);
            tTotales.AddCell(ciSubtotalPU);
            tTotales.AddCell(cvSubtotalPU);
            tTotales.AddCell(ciDescuentosDetalle);
            tTotales.AddCell(cvDescuentosDetalle);
            tTotales.AddCell(ciRecargosDetalle);
            tTotales.AddCell(cvRecargosDetalle);
            tTotales.AddCell(ciSubtotalNG);
            tTotales.AddCell(cvSubtotalNG);
            tTotales.AddCell(ciSubtotalBG);
            tTotales.AddCell(cvSubtotalBG);
            tTotales.AddCell(ciTotalImpuesto);
            tTotales.AddCell(cvTotalImpuesto);
            tTotales.AddCell(ciTotalMI);
            tTotales.AddCell(cvTotalMI);
            tTotales.AddCell(ciDescuentoGlobal);
            tTotales.AddCell(cvDescuentoGlobal);
            tTotales.AddCell(ciRecargoGlobal);
            tTotales.AddCell(cvRecargoGlobal);
            tTotales.AddCell(ciAnticipo);
            tTotales.AddCell(cvAnticipo);
            tTotales.AddCell(ciTotalNeto);
            tTotales.AddCell(cvTotalNeto);
            tTotales.AddCell(cMontoLetras);

            docPdf.Add(tTotales);

            docPdf.AddCreationDate();
            docPdf.AddCreator("eFacturacionColombia_V2.Pdf (v2.3)");
            docPdf.AddTitle(vNumeroNota);

            writer.CloseStream = false;
            docPdf.Close();

            return streamPdf.ToArray();
        }

        private void agregarLineas(Document document, int desdeIndex, int cantidad)
        {
            // tabla de items
            var tItems = new PdfPTable(6);
            tItems.WidthPercentage = 100;
            tItems.SetWidths(new float[] { 0.5f, 3.5f, 0.5f, 1.25f, 1.75f, 1.5f });

            // encabezado de items
            var fnt71 = FontFactory.GetFont("Helvetica", 8, Font.NORMAL, BaseColor.WHITE); //f10
            var cILinea = new PdfPCell(new Phrase("#", fnt71));
            cILinea.BackgroundColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cILinea.BorderColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cILinea.Border = Rectangle.BOX;
            cILinea.HorizontalAlignment = Element.ALIGN_CENTER;
            cILinea.VerticalAlignment = Element.ALIGN_MIDDLE;
            cILinea.Padding = 4;

            var cIDescripcion = new PdfPCell(new Phrase("DESCRIPCIÓN", fnt71));
            cIDescripcion.BackgroundColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cIDescripcion.BorderColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cIDescripcion.Border = Rectangle.BOX;
            cIDescripcion.HorizontalAlignment = Element.ALIGN_CENTER;
            cIDescripcion.VerticalAlignment = Element.ALIGN_MIDDLE;
            cIDescripcion.Padding = 4;

            var cICantidad = new PdfPCell(new Phrase("CTD.", fnt71));
            cICantidad.BackgroundColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cICantidad.BorderColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cICantidad.Border = Rectangle.BOX;
            cICantidad.HorizontalAlignment = Element.ALIGN_CENTER;
            cICantidad.VerticalAlignment = Element.ALIGN_MIDDLE;
            cICantidad.Padding = 4;

            var cIUnitario = new PdfPCell(new Phrase("VALOR\r\nUNITARIO", fnt71));
            cIUnitario.BackgroundColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cIUnitario.BorderColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cIUnitario.Border = Rectangle.BOX;
            cIUnitario.HorizontalAlignment = Element.ALIGN_CENTER;
            cIUnitario.VerticalAlignment = Element.ALIGN_MIDDLE;
            cIUnitario.Padding = 4;

            var cIImpuestos = new PdfPCell(new Phrase("IMPUESTOS", fnt71));
            cIImpuestos.BackgroundColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cIImpuestos.BorderColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cIImpuestos.Border = Rectangle.BOX;
            cIImpuestos.HorizontalAlignment = Element.ALIGN_CENTER;
            cIImpuestos.VerticalAlignment = Element.ALIGN_MIDDLE;
            cIImpuestos.Padding = 4;

            var cITotal = new PdfPCell(new Phrase("SUBTOTAL", fnt71));
            cITotal.BackgroundColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cITotal.BorderColor = new BaseColor(ColorPrimario.R, ColorPrimario.G, ColorPrimario.B);
            cITotal.Border = Rectangle.BOX;
            cITotal.HorizontalAlignment = Element.ALIGN_CENTER;
            cITotal.VerticalAlignment = Element.ALIGN_MIDDLE;
            cITotal.Padding = 4;

            tItems.AddCell(cILinea);
            tItems.AddCell(cIDescripcion);
            tItems.AddCell(cICantidad);
            tItems.AddCell(cIUnitario);
            tItems.AddCell(cIImpuestos);
            tItems.AddCell(cITotal);

            var fnt82 = FontFactory.GetFont("Helvetica", 7, Font.NORMAL); //f9

            // agregar lineas
            var totalLineas = DebitNote.DebitNoteLine.Count();
            for (int i = 0; i < cantidad; i++)
            {
                int x = (desdeIndex + i);

                // verificar y agregar
                if (x < totalLineas)
                {
                    var linea = DebitNote.DebitNoteLine[x];

                    bool lastLinea = (i == cantidad - 1 || x + 1 >= totalLineas);
                    int bordes = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                    if (!lastLinea) bordes = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;

                    var vxLinea = (x + 1).ToString();
                    var cXLinea = new PdfPCell(new Phrase(vxLinea, fnt82));
                    cXLinea.Border = bordes;
                    cXLinea.BorderColor = BaseColor.GRAY;
                    cXLinea.HorizontalAlignment = Element.ALIGN_CENTER;
                    cXLinea.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cXLinea.Padding = 3;

                    var vxDescripcion = linea.Item.Description[0]?.Value;
                    if (linea.Item.StandardItemIdentification != null)
                        vxDescripcion = linea.Item.StandardItemIdentification.ID?.Value + " - " + vxDescripcion;
                    var cXDescripcion = new PdfPCell(new Phrase(vxDescripcion, fnt82));
                    cXDescripcion.FixedHeight = 25f;
                    cXDescripcion.Border = bordes;
                    cXDescripcion.BorderColor = BaseColor.GRAY;
                    cXDescripcion.HorizontalAlignment = Element.ALIGN_CENTER;
                    cXDescripcion.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cXDescripcion.Padding = 3;
                    cXDescripcion.PaddingRight = 7;
                    cXDescripcion.PaddingLeft = 7;

                    var vxCantidad = linea.DebitedQuantity.Value.ToString("0.##");
                    var cXCantidad = new PdfPCell(new Phrase(vxCantidad, fnt82));
                    cXCantidad.Border = bordes;
                    cXCantidad.BorderColor = BaseColor.GRAY;
                    cXCantidad.HorizontalAlignment = Element.ALIGN_CENTER;
                    cXCantidad.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cXCantidad.Padding = 3;

                    var vxPrecio = linea.Price.PriceAmount.Value;
                    var vxDescuento = linea.AllowanceCharge?.Sum(ac => ac.ChargeIndicator.Value ? 0M : ac.Amount.Value) ?? 0M;
                    var vxCargo = linea.AllowanceCharge?.Sum(ac => ac.ChargeIndicator.Value ? ac.Amount.Value : 0M) ?? 0M;

                    var vxUnitario = (vxPrecio).ToString("#,###,##0.##");
                    var cXUnitario = new PdfPCell(new Phrase(vxUnitario, fnt82));
                    cXUnitario.Border = bordes;
                    cXUnitario.BorderColor = BaseColor.GRAY;
                    cXUnitario.HorizontalAlignment = Element.ALIGN_CENTER;
                    cXUnitario.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cXUnitario.Padding = 3;

                    var iva = linea.TaxTotal?.FirstOrDefault(t => t.TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value == "01");
                    var ic = linea.TaxTotal?.FirstOrDefault(t => t.TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value == "02");
                    var inc = linea.TaxTotal?.FirstOrDefault(t => t.TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value == "04");
                    var bolsas = linea.TaxTotal?.FirstOrDefault(t => t.TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value == "22");

                    var vxImpuesto = "";
                    try
                    {
                        if (iva != null)
                        {
                            vxImpuesto = iva.TaxSubtotal[0].TaxAmount.Value.ToString("#,###,##0.##") +
                                " - IVA " + iva.TaxSubtotal[0].TaxCategory.Percent.Value.ToString("0.##") + "%";
                        }
                        else if (ic != null)
                        {
                            vxImpuesto = ic.TaxSubtotal[0].TaxAmount.Value.ToString("#,###,##0.##") +
                                " - IC " + ic.TaxSubtotal[0].TaxCategory.Percent.Value.ToString("0.##") + "%";
                        }
                        else if (inc != null)
                        {
                            vxImpuesto = inc.TaxSubtotal[0].TaxAmount.Value.ToString("#,###,##0.##") +
                                " - INC " + inc.TaxSubtotal[0].TaxCategory.Percent.Value.ToString("0.##") + "%";
                        }
                        else if (bolsas != null)
                        {
                            vxImpuesto = bolsas.TaxSubtotal[0].TaxAmount.Value.ToString("#,###,##0.##") +
                                " - BP " + bolsas.TaxSubtotal[0].PerUnitAmount.Value.ToString("0.##");
                        }
                    }
                    catch
                    {
                        vxImpuesto = "?";
                    }

                    var cXImpuestos = new PdfPCell(new Phrase(vxImpuesto, fnt82));
                    cXImpuestos.Border = bordes;
                    cXImpuestos.BorderColor = BaseColor.GRAY;
                    cXImpuestos.HorizontalAlignment = Element.ALIGN_CENTER;
                    cXImpuestos.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cXImpuestos.Padding = 3;

                    var vxTotal = linea.LineExtensionAmount.Value.ToString("#,###,##0.##");
                    var cXTotal = new PdfPCell(new Phrase(vxTotal, fnt82));
                    cXTotal.Border = bordes;
                    cXTotal.BorderColor = BaseColor.GRAY;
                    cXTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    cXTotal.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cXTotal.Padding = 3;

                    tItems.AddCell(cXLinea);
                    tItems.AddCell(cXDescripcion);
                    tItems.AddCell(cXCantidad);
                    tItems.AddCell(cXUnitario);
                    tItems.AddCell(cXImpuestos);
                    tItems.AddCell(cXTotal);
                }
            }

            document.Add(tItems);
        }
    }
}
