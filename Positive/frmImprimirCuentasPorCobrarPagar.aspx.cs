using InventarioBusiness;
using InventarioItem;
using System;
using System.Configuration;
using System.Globalization;
using System.Web.UI;

namespace Positive
{
    public partial class frmImprimirCuentasPorCobrarPagar : System.Web.UI.Page
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        long idPago = 0;
        string PagoDetalle = string.Empty;
        string TotalPagar = string.Empty;
        int Tipo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                long.TryParse(Request.QueryString["idPago"], out idPago);
                int.TryParse(Request.QueryString["Tipo"], out Tipo);
                PagoDetalle = Request.QueryString["PagoDetalle"];
                TotalPagar = Request.QueryString["TotalPagar"];
                Imprimir();
            }
        }
        private void Imprimir()
        {
            try
            {
                tblPagoItem oPagoI = new tblPagoItem();
                tblPagoBusiness oPagB = new tblPagoBusiness(CadenaConexion);
                oPagoI = oPagB.ObtenerPagoPorIDPago(idPago, Tipo);
                tblTerceroItem oTerI = new tblTerceroItem();
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                oTerI = oTerB.ObtenerTercero(oPagoI.idTercero, oUsuarioI.idEmpresa);
                tblEmpresaItem oEmpI = new tblEmpresaItem();
                tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                string Mensaje = "";
                Mensaje = string.Format("<div style='position:relative;font-family:arial;'>" +
                    "<div style='font-size: 12px; font-weight: bold; width: 300px; padding-top: 20px; text-align: left;'>{0}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: left;'>Nit: {1}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: left;'>Direcci&oacute;n: {2}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: left;'>Telefono: {3}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>{4}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align:center;'>Comprobante de pago</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>ID Pago: {5}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Tercero: {6}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px;'>Identificaci&oacute;n: {7}</div>" +
                    "<div style='font-size: 14px;font-weight: bold; padding-top: 5px; width: 300px; text-align: right;'>Total a pagar: {8}</div>" +
                    "<div style='font-size: 14px;font-weight: bold; padding-top: 5px; width: 300px; text-align: right;'>Valor del pago: {9}</div>" +
                    "<div style='font-size: 14px;font-weight: bold; padding-top: 5px; width: 300px; text-align: right;'>Saldo: {10}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Vende: {11}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Observaciones: {12}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align:center;'>Documentos Relacionados</div>" +
                    "<div style='font-size: 14px;font-weight: bold; padding-top: 5px; width: 300px; text-align: left;'>{13}</div>" +
                    "</div>", oEmpI.Nombre, oEmpI.Identificacion, oEmpI.Direccion, oEmpI.Telefono, oPagoI.fechaPago, oPagoI.idPago, oTerI.Nombre,
                    oTerI.Identificacion, TotalPagar, oPagoI.totalPago, (decimal.Parse(TotalPagar, NumberStyles.Currency) - oPagoI.totalPago).ToString(Util.ObtenerFormatoDecimal()),
                    oUsuarioI.Usuario, oPagoI.Observaciones, PagoDetalle);
                string strScript = string.Format("jQuery(document).ready(function(){{ ImprimirComprobantePago(\"{0}\");}});", Mensaje);
                if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}