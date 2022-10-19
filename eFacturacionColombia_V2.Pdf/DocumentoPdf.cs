using System;
using System.Drawing;

namespace eFacturacionColombia_V2.Pdf
{
    public abstract class DocumentoPdf
    {
        public const int LINEAS_PRIMERA_PAGINA_CON_TOTALES = 7;

        public const int LINEAS_PRIMERA_PAGINA_SIN_TOTALES = 16;

        public const int LINEAS_PAGINAS_EXTRA = 28;

        public const string TIPO_FACTURA_VENTA = "FACTURA ELECTRÓNICA DE VENTA";

        public const string TIPO_FACTURA_EXPORTACION = "FACTURA ELECTRÓNICA DE EXPORTACIÓN";

        public const string TIPO_FACTURA_CONTINGENCIA = "FACTURA ELECTRÓNICA DE CONTINGENCIA";

        public const string TIPO_NOTA_CREDITO = "NOTA DE CRÉDITO ELECTRÓNICA";

        public const string TIPO_NOTA_DEBITO = "NOTA DE DÉBITO ELECTRÓNICA";

        public const string TEXTO_CONSTANCIA_FACTURA_DEFAULT = "Se hace constar que las mercancías o servicios fueron entregados real y materialmente y en todo caso, la factura será considerada irrevocablemente aceptada por el comprador si no reclamare en los tres (3) días hábiles siguientes a su recepción.";

        public const string TEXTO_CONSTANCIA_NOTA_DEBITO_DEFAULT = "Se hace constar que las mercancías o servicios fueron entregados real y materialmente y en todo caso, la nota de débito será considerada irrevocablemente aceptada por el comprador si no reclamare en los tres (3) días hábiles siguientes a su recepción.";


        public virtual string Tipo { get; }

        public virtual string RutaLogo { get; set; }

        public virtual string TextoEncabezado { get; set; }

        public virtual string TextoQR { get; set; }

        public virtual string TextoAdicional { get; set; }

        public virtual string TextoConstancia { get; set; }

        public virtual string TextoResolucion { get; set; }

        public virtual Color ColorPrimario { get; set; } = Color.FromArgb(73, 79, 255);


        public abstract byte[] Generar();


        public static string CrearTextoResolucion(long numero, DateTime fecha, string prefijo, long desde, long hasta, DateTime? vigencia = null)
        {
            var ret = "Resolución número " + numero + " autorizada el " + fecha.ToString("yyyy-MM-dd") + " desde " + prefijo + desde + " hasta " + prefijo + hasta;
            if (vigencia.HasValue)
                ret += ", vigente hasta el " + vigencia.Value.ToString("yyyy-MM-dd") + ".";
            else ret += ".";

            return ret;
        }


        protected string montoALetras(decimal valor, string moneda)
        {
            var entero = Convert.ToInt64(Math.Truncate(valor));
            var decimales = Convert.ToInt32(Math.Round((valor - entero) * 100, 2));

            var ret = numeroALetras(entero);

            switch (moneda)
            {
                case "COP":
                    moneda = "peso(s) colombiano(s)";
                    break;

                case "USD":
                    moneda = "dólar(es) estadounidense(s)";
                    break;

                case "EUR":
                    moneda = "euro(s)";
                    break;

                case "GBP":
                    moneda = "libra(s) esterlina(s)";
                    break;
            }

            if (decimales == 0)
                ret += " " + moneda + " exactos";
            else if (decimales == 1)
                ret += " " + moneda + " con un centavo";
            else ret += " " + moneda + " con " + numeroALetras(decimales) + " centavos";

            return ret;
        }

        protected string numeroALetras(double valor)
        {
            string ret = null;
            valor = Math.Truncate(valor);

            if (valor == 0) ret = "cero";
            else if (valor == 1) ret = "uno";
            else if (valor == 2) ret = "dos";
            else if (valor == 3) ret = "tres";
            else if (valor == 4) ret = "cuatro";
            else if (valor == 5) ret = "cinco";
            else if (valor == 6) ret = "seis";
            else if (valor == 7) ret = "siete";
            else if (valor == 8) ret = "ocho";
            else if (valor == 9) ret = "nueve";
            else if (valor == 10) ret = "diez";
            else if (valor == 11) ret = "once";
            else if (valor == 12) ret = "doce";
            else if (valor == 13) ret = "trece";
            else if (valor == 14) ret = "catorce";
            else if (valor == 15) ret = "quince";
            else if (valor < 20) ret = "dieci" + numeroALetras(valor - 10);
            else if (valor == 20) ret = "veinte";
            else if (valor < 30) ret = "veinti" + numeroALetras(valor - 20);
            else if (valor == 30) ret = "treinta";
            else if (valor == 40) ret = "cuarenta";
            else if (valor == 50) ret = "cincuenta";
            else if (valor == 60) ret = "sesenta";
            else if (valor == 70) ret = "setenta";
            else if (valor == 80) ret = "ochenta";
            else if (valor == 90) ret = "noventa";
            else if (valor < 100) ret = numeroALetras(Math.Truncate(valor / 10) * 10) + " Y " + numeroALetras(valor % 10);
            else if (valor == 100) ret = "cien";
            else if (valor < 200) ret = "ciento " + numeroALetras(valor - 100);
            else if ((valor == 200) || (valor == 300) || (valor == 400) || (valor == 600) || (valor == 800)) ret = numeroALetras(Math.Truncate(valor / 100)) + "cientos";
            else if (valor == 500) ret = "quinientos";
            else if (valor == 700) ret = "setecientos";
            else if (valor == 900) ret = "novecientos";
            else if (valor < 1000) ret = numeroALetras(Math.Truncate(valor / 100) * 100) + " " + numeroALetras(valor % 100);
            else if (valor == 1000) ret = "mil";
            else if (valor < 2000) ret = "mil " + numeroALetras(valor % 1000);
            else if (valor < 1000000)
            {
                ret = numeroALetras(Math.Truncate(valor / 1000)) + " mil";
                if ((valor % 1000) > 0)
                {
                    ret += " " + numeroALetras(valor % 1000);
                }
            }
            else if (valor == 1000000)
            {
                ret = "un millón";
            }
            else if (valor < 2000000)
            {
                ret = "un millón " + numeroALetras(valor % 1000000);
            }
            else if (valor < 1000000000000)
            {
                ret = numeroALetras(Math.Truncate(valor / 1000000)) + " millones ";
                if ((valor - Math.Truncate(valor / 1000000) * 1000000) > 0)
                {
                    ret += " " + numeroALetras(valor - Math.Truncate(valor / 1000000) * 1000000);
                }
            }
            else if (valor == 1000000000000) ret = "un billón";
            else if (valor < 2000000000000) ret = "un billón " + numeroALetras(valor - Math.Truncate(valor / 1000000000000) * 1000000000000);
            else
            {
                ret = numeroALetras(Math.Truncate(valor / 1000000000000)) + " billones";
                if ((valor - Math.Truncate(valor / 1000000000000) * 1000000000000) > 0)
                {
                    ret += " " + numeroALetras(valor - Math.Truncate(valor / 1000000000000) * 1000000000000);
                }
            }

            return ret;
        }
    }
}