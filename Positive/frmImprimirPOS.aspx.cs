using InventarioBusiness;
using InventarioItem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Positive
{
    public partial class frmImprimirPOS : System.Web.UI.Page
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private long idDocumento = 0;
        private int IdTipoDocumento = 0;
        private string TipoDocumento = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Request.QueryString["idDocumento"], out idDocumento);
            int.TryParse(Request.QueryString["IdTipoDocumento"], out IdTipoDocumento);
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VentaRapida.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {
                    switch ((tblDocumentoBusiness.TipoDocumentoEnum)IdTipoDocumento) {
                        case tblDocumentoBusiness.TipoDocumentoEnum.Venta:
                            TipoDocumento = "Factura de venta";
                            break;
                        case tblDocumentoBusiness.TipoDocumentoEnum.Remision:
                            TipoDocumento = "Remisión";
                            break;
                        case tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones:
                            TipoDocumento = "Cotización";
                            break;
                    }
                    ImprimirFactura(idDocumento, TipoDocumento, IdTipoDocumento);
                }
                else
                {
                    Response.Redirect("frmMantenimientos.aspx");
                }
            }
        }
        private void ImprimirFactura(long IdFacturaVenta, string TipoDocumento, long IdTipoDocumento)
        {
            tblEmpresaItem oEmpI = new tblEmpresaItem();
            tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
            tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
            tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
            tblDocumentoItem oDocI = oDBiz.traerDocumentoPorId(IdTipoDocumento, IdFacturaVenta);
            if(oDocI.DocumentoLineas.Count() > 0)
            {
                tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                tblTerceroItem oTItem = oTBiz.ObtenerTercero(oDocI.idTercero, oDocI.idEmpresa);
                oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                string Mensaje = "";
                string Detalles = "";
                Detalles = "<table border='1' style='width:100%'><tr><td align='center'>Cant</td><td align='center'>Descripcion</td><td align='center'>Valor</td></tr>";
                foreach (tblDetalleDocumentoItem Item in oDocI.DocumentoLineas)
                {
                    Detalles = Detalles + "<tr><td align='center'>" + Item.Cantidad + "</td>";
                    Detalles = Detalles + "<td>" + Item.Articulo.Replace("\"", "") + "</td>";
                    Detalles = Detalles + "<td  align='right' style='width: 20%'>" + (Item.ValorUnitario * Item.Cantidad).ToString(Util.ObtenerFormatoDecimal()) + "</td></tr>";
                }
                Detalles = Detalles + "</table>";
                string FormaPago = "";
                FormaPago = "<table border='1' style='width:100%'><tr><td align='center'>Forma</td><td align='center'>Valor</td></tr>";
                foreach (tblTipoPagoItem Item in oDocI.FormasPago)
                {
                    FormaPago = FormaPago + "<tr><td>" + Item.FormaPago + "</td>";
                    FormaPago = FormaPago + "<td align='right'>" + Item.ValorPago.ToString(Util.ObtenerFormatoDecimal()) + "</td></tr>";
                }
                FormaPago = FormaPago + "</table>";
                Mensaje = string.Format("<div style='position:relative;font-family:arial;'>" +
                "<div style='font-size: 18px;font-weight: bold; padding-top: 0px; width: 300px; text-align: center;'>{0}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Nit: {1}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Direcci&oacute;n: {2}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Telefono: {3}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>{4}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{5}</div>" +
                "<div style='font-size: 20px;font-weight: bold; padding-top: 2px; width: 300px;'>Numero Documento: {6}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{7}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Tercero: {8}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Identificaci&oacute;n: {9}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center; width:300px;'>DETALLES</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{10}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Antes de IVA: {11}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Valor IVA: {12}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Impoconsumo: {19}</div>" +
                "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Total a pagar: {13}</div>" +
                "<div style='font-size: 10px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Formas de Pago</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{17}</div>" +
                "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Cambio: {18}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Vende: {14}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Observaciones: {15}</div>" +
                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{16}</div>" +
                "</div>", oEmpI.Nombre, oEmpI.Identificacion, oEmpI.Direccion, oEmpI.Telefono, TipoDocumento, oEmpI.TextoEncabezadoFactura,
                oDocI.NumeroDocumento, oDocI.Fecha, oDocI.NombreTercero, oTItem.Identificacion,
                Detalles, (oDocI.TotalDocumento - oDocI.TotalIVA).ToString(Util.ObtenerFormatoDecimal()),
                oDocI.TotalIVA.ToString(Util.ObtenerFormatoDecimal()), oDocI.TotalDocumento.ToString(Util.ObtenerFormatoDecimal()),
                oUsuarioI.Usuario, oDocI.Observaciones, oEmpI.TextoPieFactura, FormaPago, oDocI.Devuelta.ToString(Util.ObtenerFormatoDecimal()),
                oDocI.Impoconsumo.ToString(Util.ObtenerFormatoDecimal()));
                string strScript = string.Format("jQuery(document).ready(function(){{ setTimeout(function(){{ ImprimirDocumentoVentaRapida(\"{0}\");}},1000);}});", Mensaje);
                if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
                }
            }
            else
            {
                if (IdTipoDocumento == 1)
                {
                    Response.Redirect("frmVentaRapida.aspx");
                }
                else if (IdTipoDocumento == 3)
                {
                    Response.Redirect("frmVentaRapida.aspx?IdTipoDocumento=3");
                }
                else
                {
                    Response.Redirect("frmVentaRapida.aspx?IdTipoDocumento=8");
                }
            }
        }
    }
}