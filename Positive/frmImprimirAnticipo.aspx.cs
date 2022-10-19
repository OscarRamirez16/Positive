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
    public partial class frmImprimirAnticipo : System.Web.UI.Page
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        long idPago = 0;
        string FormaPago = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                long.TryParse(Request.QueryString["idPago"], out idPago);
                FormaPago = Request.QueryString["FormaPago"];
                Imprimir();
            }
        }
        private void Imprimir()
        {
            try
            {
                tblEmpresaItem oEmpI = new tblEmpresaItem();
                tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                tblPagoItem oPagI = new tblPagoItem();
                tblPagoBusiness oPagB = new tblPagoBusiness(CadenaConexion);
                oPagI = oPagB.ObtenerPagoPorIDPago(idPago, 1);
                if (string.IsNullOrEmpty(FormaPago))
                {

                    List<tblTipoPagoItem> oListTipoPago = new List<tblTipoPagoItem>();
                    oListTipoPago = oPagB.ObtenerTipoPago(idPago);
                    foreach(tblTipoPagoItem Item in oListTipoPago)
                    {
                        FormaPago = FormaPago + "* Forma: " + Item.FormaPago;
                        FormaPago = FormaPago + " Valor: " + Item.ValorPago.ToString(Util.ObtenerFormatoDecimal());
                    }
                }
                tblTerceroItem oTerI = new tblTerceroItem();
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                oTerI = oTerB.ObtenerTercero(oPagI.idTercero, oUsuarioI.idEmpresa);
                string Mensaje = "";
                Mensaje = string.Format("<div style='position:relative;font-family:arial;'>" +
                    "<div style='font-size: 12px; font-weight: bold; width: 300px; padding-top: 20px; text-align: center;'>{0}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: center;'>Nit: {1}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: center;'>Direcci&oacute;n: {2}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: center;'>Telefono: {3}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>{4}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align:center;'>Anticipo</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Número: {5}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Tercero: {6}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px;'>Identificaci&oacute;n: {7}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; text-align:center; width:300px;'>Formas de Pago</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>{11}</div>" +
                    "<div style='font-size: 14px;font-weight: bold; padding-top: 5px; width: 300px; text-align: right;'>Valor: {8}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Vende: {9}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Observaciones: {10}</div>" +
                    "</div>", oEmpI.Nombre, oEmpI.Identificacion, oEmpI.Direccion, oEmpI.Telefono, oPagI.fechaPago, oPagI.idPago, oTerI.Nombre,
                   oTerI.Identificacion, decimal.Parse(oPagI.totalPago.ToString(), System.Globalization.NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal()),
                   oUsuarioI.Usuario, oPagI.Observaciones, FormaPago);
                string strScript = string.Format("jQuery(document).ready(function(){{ ImprimirComprobanteAnticipo(\"{0}\");}});", Mensaje);
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