using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using Idioma;
using HQSFramework.Base;
using System.Text;
using System.Globalization;

namespace Inventario
{
    public partial class frmVentaRapida : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (!IsPostBack) {
                if (int.TryParse(Request.QueryString["IdTipoDocumento"], out int IdTipoDocumento))
                {
                    switch ((tblDocumentoBusiness.TipoDocumentoEnum)IdTipoDocumento)
                    {
                        case tblDocumentoBusiness.TipoDocumentoEnum.Venta:
                            rdbFacturaVenta.Checked = true;
                            break;
                        case tblDocumentoBusiness.TipoDocumentoEnum.Remision:
                            rdbRemision.Checked = true;
                            break;
                        default:
                            rdbCotizacion.Checked = true;
                            break;
                    }
                }
                else
                {
                    rdbFacturaVenta.Checked = true;
                }
            }
            if (oUsuarioI != null)
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VentaRapida.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {
                    if (ValidarCajaAbierta())
                    {
                        InicializarControles();
                        ConfiguracionIdioma();
                        string strScript = "$(document).ready(function(){";
                        if (!oUsuarioI.ModificaPrecio)
                        {
                            strScript = string.Format("{0} $('#txtPrecioArticulo_0').prop('disabled', true);", strScript);
                        }
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}','','','');", strScript, txtTercero.ClientID, hddIdCliente.ClientID);
                        strScript = string.Format("{0}}});", strScript);
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript")) {
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }
                    else {
                        MostrarMensaje("Error", "El usuario no tiene una caja abierta.");
                        btnGuardar.Visible = false;
                    }
                }
                else {
                    Response.Redirect("frmMantenimientos.aspx");
                }
            }
        }

        private bool ValidarCajaAbierta()
        {
            try
            {
                tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                oCuaI.idEmpresa = oUsuarioI.idEmpresa;
                oCuaI.idUsuarioCaja = oUsuarioI.idUsuario;
                oCuaI.Estado = true;
                oCuaI = oCuaB.ObtenerCuadreCajaListaPorUsuario(oCuaI);
                if (oCuaI.idCuadreCaja == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void InicializarControles()
        {
            btnCancelar.Attributes.Add("onclick", string.Format("LimpiarFacturaVentaRapida('{0}');return false;",hddItems.ClientID));
            btnActualizarPrecios.Style.Add("display", "none");
            txtTercero.Attributes.Add("onblur", string.Format("ActualizarPreciosVentaRapida('{0}','{1}')",btnActualizarPrecios.ClientID,hddIdCliente.ClientID));
            hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
            tblTerceroItem oTerI = new tblTerceroItem();
            tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
            oTerI = oTerB.ObtenerTerceroPorIdentificacion("123456789",oUsuarioI.idEmpresa);
            if(!IsPostBack){
                hddIdCliente.Value = oTerI.IdTercero.ToString();
                txtTercero.Text = oTerI.Nombre;
            }
            PintarArticulosCard();
            
        }

        private void ConfiguracionIdioma()
        {
            Traductor oCIdioma = new Traductor();
            Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
            if (!Util.EsEntero(Request.Form["ctl00$ddlIdiomas"]))
            {
                Idioma = (Idioma.Traductor.IdiomaEnum)oUsuarioI.IdIdioma;
            }
            else
            {
                Idioma = (Traductor.IdiomaEnum)int.Parse(Request.Form["ctl00$ddlIdiomas"]);
            }
            lblCodigo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Codigo);
            lblDescripcion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Descripcion);
            lblCantidad.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad);
            lblValor.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Valor);
            lblValorAntesIVA.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ValorAntesIVA);
            lblIVA.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ValorIVA);
            lblValorTotal.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ValorTotal);
            lblFacturaVenta.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FacturaVenta);
        }
        private void PintarArticulos() {
            tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
            List<tblVentaRapidaItem> oVRLista = oDBiz.ObtenerVentaRapidaLista(oUsuarioI.idEmpresa, 0, false, long.Parse(hddIdCliente.Value), oUsuarioI.idBodega);
            string Linea = "";
            StringBuilder sbHTML = new StringBuilder();
            int numItems = 0;
            string navDiv = "<ul class=\"nav nav-pills\">";
            sbHTML.AppendLine("<br/><div class=\"tab-content\">");
            foreach (tblVentaRapidaItem oVRItem in oVRLista)
            {
                if (oVRItem.Linea != Linea)
                {
                    if (!string.IsNullOrEmpty(Linea))
                    {
                        sbHTML.AppendLine("</div>");
                        if (numItems != 0)
                        {
                            sbHTML.AppendLine("</div>");
                        }
                    }
                    if (string.IsNullOrEmpty(Linea))
                    {
                        navDiv = string.Format("{0}<li class=\"active\"><a data-toggle=\"pill\" href=\"#div_{1}\">{2}</a></li>", navDiv, oVRItem.Linea.Replace(" ", "_"), oVRItem.Linea);
                        sbHTML.AppendLine(string.Format("<div id=\"div_{0}\" class=\"tab-pane fade in active\">", oVRItem.Linea.Replace(" ", "_")));
                    }
                    else
                    {
                        navDiv = string.Format("{0}<li><a data-toggle=\"pill\" href=\"#div_{1}\">{2}</a></li>", navDiv, oVRItem.Linea.Replace(" ", "_"), oVRItem.Linea);
                        sbHTML.AppendLine(string.Format("<div id=\"div_{0}\" class=\"tab-pane fade\">", oVRItem.Linea.Replace(" ", "_")));
                    }
                    Linea = oVRItem.Linea;
                    numItems = 0;
                }
                if (numItems == 0)
                {
                    sbHTML.AppendLine("<div class=\"row\">");
                    sbHTML.AppendLine("<div class=\"col-md-2\">");
                    sbHTML.AppendLine(ObtenerVentaRapidaHTML(oVRItem));
                    sbHTML.AppendLine("</div>");
                    numItems++;
                }
                else if (numItems == 5)
                {
                    sbHTML.AppendLine("<div class=\"col-md-2\">");
                    sbHTML.AppendLine(ObtenerVentaRapidaHTML(oVRItem));
                    sbHTML.AppendLine("</div>");
                    sbHTML.AppendLine("</div>");
                    numItems = 0;
                }
                else
                {
                    //sbHTML.AppendLine(string.Format("<div class=\"col-md-3 col-md-offset-{0}\">",(numItems*3)));
                    sbHTML.AppendLine("<div class=\"col-md-2\">");
                    sbHTML.AppendLine(ObtenerVentaRapidaHTML(oVRItem));
                    sbHTML.AppendLine("</div>");
                    numItems++;
                }
            }
            if (!string.IsNullOrEmpty(Linea) && numItems != 0)
            {
                sbHTML.AppendLine("</div>");
                sbHTML.AppendLine("</div>");
            }
            else if (!string.IsNullOrEmpty(Linea) && numItems == 0)
            {
                sbHTML.AppendLine("</div>");
            }
            sbHTML.AppendLine("</div>");
            navDiv = string.Format("{0}</ul>", navDiv);
            divItems.InnerHtml = string.Format("{0}{1}", navDiv, sbHTML.ToString());
        }

        private string ObtenerVentaRapidaHTML(tblVentaRapidaItem oVRItem)
        {
            Traductor oCIdioma = new Traductor();
            Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
            if (!Util.EsEntero(Request.Form["ctl00$ddlIdiomas"]))
            {
                Idioma = (Idioma.Traductor.IdiomaEnum)oUsuarioI.IdIdioma;
            }
            else
            {
                Idioma = (Traductor.IdiomaEnum)int.Parse(Request.Form["ctl00$ddlIdiomas"]);
            }
            StringBuilder sbVRHtml = new StringBuilder(string.Format("<div class = \"thumbnail\" onclick=\"AdicionarVentaRapida({0},'{1}','{2}','{3:0.00}','{4:0.00}','{5:0.00}','{6:0.00}','{7}','{8}');\">", oVRItem.idVentaRapida, oVRItem.idArticulo, oVRItem.Articulo, oVRItem.Cantidad, oVRItem.ValorIVA, oVRItem.Precio, oVRItem.Stock, hddItems.ClientID, oUsuarioI.Impoconsumo));
            sbVRHtml.AppendLine(string.Format("<img style=\"cursor:pointer;\" src = \"frmMostrarImagen.aspx?IdVentaRapida={0}\" alt = \"{1}\"/>",oVRItem.idVentaRapida,oVRItem.Nombre));
            sbVRHtml.AppendLine("</div>");
            sbVRHtml.AppendLine("<div class = \"caption\">");
            sbVRHtml.AppendLine(string.Format("<input type='hidden' id='hdd{0}Stock' value='{1}'/>",oVRItem.idVentaRapida, oVRItem.Stock));
            sbVRHtml.AppendLine(string.Format("<b>{0}</b>",oVRItem.Nombre));
            sbVRHtml.AppendLine(string.Format("<br/><span>{0}:&nbsp;<b>{1}</b></span>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Disponibles), oVRItem.Stock));
            sbVRHtml.AppendLine(string.Format("<br/><span>{0}:&nbsp;<b id='{2}'>{1:0.00}</b></span><br/>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad), oVRItem.Cantidad, oVRItem.idVentaRapida));
            sbVRHtml.AppendLine(string.Format("<span>{0}:&nbsp;<b>{1:0.00}</b></span>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Precio), oVRItem.Precio));
            sbVRHtml.AppendLine("</div>");
            return sbVRHtml.ToString();
        }


        private void PintarArticulosCard()
        {
            tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
            List<tblVentaRapidaItem> oVRLista = oDBiz.ObtenerVentaRapidaLista(oUsuarioI.idEmpresa, 0, false, long.Parse(hddIdCliente.Value), oUsuarioI.idBodega);
            string Linea = "";
            StringBuilder sbHTML = new StringBuilder();
            int numItems = 0;
            string navDiv = "<ul class=\"nav nav-pills\">";
            sbHTML.AppendLine("<br/><div class=\"tab-content\">");
            foreach (tblVentaRapidaItem oVRItem in oVRLista)
            {
                if (oVRItem.Linea != Linea)
                {
                    if (!string.IsNullOrEmpty(Linea))
                    {
                        sbHTML.AppendLine("</div>");
                        if (numItems != 0)
                        {
                            sbHTML.AppendLine("</div>");
                        }
                    }
                    if (string.IsNullOrEmpty(Linea))
                    {
                        navDiv = string.Format("{0}<li class=\"active\"><a data-toggle=\"pill\" href=\"#div_{1}\">{2}</a></li>", navDiv, oVRItem.Linea.Replace(" ", "_"), oVRItem.Linea);
                        sbHTML.AppendLine(string.Format("<div id=\"div_{0}\" class=\"tab-pane fade in active\">", oVRItem.Linea.Replace(" ", "_")));
                    }
                    else
                    {
                        navDiv = string.Format("{0}<li><a data-toggle=\"pill\" href=\"#div_{1}\">{2}</a></li>", navDiv, oVRItem.Linea.Replace(" ", "_"), oVRItem.Linea);
                        sbHTML.AppendLine(string.Format("<div id=\"div_{0}\" class=\"tab-pane fade\">", oVRItem.Linea.Replace(" ", "_")));
                    }
                    Linea = oVRItem.Linea;
                    numItems = 0;
                }
                if (numItems == 0)
                {
                    sbHTML.AppendLine("<div class=\"row\">");
                    sbHTML.AppendLine("<div class=\"column\">");
                    sbHTML.AppendLine(ObtenerVentaRapidaCardHTML(oVRItem));
                    sbHTML.AppendLine("</div>");
                    numItems++;
                }
                else if (numItems == 7)
                {
                    sbHTML.AppendLine("<div class=\"column\">");
                    sbHTML.AppendLine(ObtenerVentaRapidaCardHTML(oVRItem));
                    sbHTML.AppendLine("</div>");
                    sbHTML.AppendLine("</div>");
                    numItems = 0;
                }
                else
                {
                    sbHTML.AppendLine("<div class=\"column\">");
                    sbHTML.AppendLine(ObtenerVentaRapidaCardHTML(oVRItem));
                    sbHTML.AppendLine("</div>");
                    numItems++;
                }
            }
            if (!string.IsNullOrEmpty(Linea) && numItems != 0)
            {
                sbHTML.AppendLine("</div>");
                sbHTML.AppendLine("</div>");
            }
            else if (!string.IsNullOrEmpty(Linea) && numItems == 0)
            {
                sbHTML.AppendLine("</div>");
            }
            sbHTML.AppendLine("</div>");
            navDiv = string.Format("{0}</ul>", navDiv);
            divItems.InnerHtml = string.Format("{0}{1}", navDiv, sbHTML.ToString());
        }

        private string ObtenerVentaRapidaCardHTML(tblVentaRapidaItem oVRItem)
        {
            Traductor oCIdioma = new Traductor();
            Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
            if (!Util.EsEntero(Request.Form["ctl00$ddlIdiomas"]))
            {
                Idioma = (Idioma.Traductor.IdiomaEnum)oUsuarioI.IdIdioma;
            }
            else
            {
                Idioma = (Traductor.IdiomaEnum)int.Parse(Request.Form["ctl00$ddlIdiomas"]);
            }
            StringBuilder sbVRHtml = new StringBuilder(string.Format("<div class = \"card\" onclick=\"AdicionarVentaRapida({0},'{1}','{2}','{3:0.00}','{4:0.00}','{5:0.00}','{6:0.00}','{7}','{8}');\">", oVRItem.idVentaRapida, oVRItem.idArticulo, oVRItem.Articulo, oVRItem.Cantidad, oVRItem.ValorIVA, oVRItem.Precio, oVRItem.Stock, hddItems.ClientID, oUsuarioI.Impoconsumo));
            sbVRHtml.AppendLine(string.Format("<img style=\"cursor:pointer;\" src = \"frmMostrarImagen.aspx?IdVentaRapida={0}\" alt = \"{1}\"/>", oVRItem.idVentaRapida, oVRItem.Nombre));
            sbVRHtml.AppendLine("<div class = \"containerCard\">");
            sbVRHtml.AppendLine(string.Format("<input type='hidden' id='hdd{0}Stock' value='{1}'/>", oVRItem.idVentaRapida, oVRItem.Stock));
            sbVRHtml.AppendLine(string.Format("<p class=\"title\"><b>{0}</b></p>", oVRItem.Nombre));
            sbVRHtml.AppendLine(string.Format("<p>{0}:&nbsp;<b>{1:N0}</b></p>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Disponibles), oVRItem.Stock));
            sbVRHtml.AppendLine(string.Format("<p>{0}:&nbsp;<b id='{2}'>{1:N0}</b></p>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad), oVRItem.Cantidad, oVRItem.idVentaRapida));
            sbVRHtml.AppendLine(string.Format("<p>{0}:&nbsp;<b>{1:C0}</b></p>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Precio), oVRItem.Precio));
            sbVRHtml.AppendLine("</div>");
            sbVRHtml.AppendLine("</div>");
            return sbVRHtml.ToString();
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmVentaRapida.aspx");
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string TipoDocumento = "";
                tblTerceroItem oTerI = new tblTerceroItem();
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                oTerI = oTerB.ObtenerTercero(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa);
                tblDocumentoItem oDocI = new tblDocumentoItem();
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                oDocI.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                oDocI.idTercero = oTerI.IdTercero;
                oDocI.Telefono = oTerI.Telefono;
                oDocI.Direccion = oTerI.Direccion;
                oDocI.idCiudad = oUsuarioI.idCiudad;
                oDocI.NombreTercero = txtTercero.Text;
                if (string.IsNullOrEmpty(txtObservaciones.Text))
                {
                    oDocI.Observaciones = "Venta rapida";
                }
                else
                {
                    oDocI.Observaciones = txtObservaciones.Text;
                }
                oDocI.idEmpresa = oUsuarioI.idEmpresa;
                oDocI.idUsuario = oUsuarioI.idUsuario;
                oDocI.TotalDocumento = decimal.Parse(hddValorTotal.Value.Replace(".", ",").Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                oDocI.TotalIVA = decimal.Parse(hddValorIVA.Value.Replace(".",",").Replace(",",CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                oDocI.Impoconsumo = decimal.Parse(hddValorImpoconsumo.Value.Replace(".", ",").Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                oDocI.saldo = oDocI.TotalDocumento;
                if (rdbRemision.Checked)
                {
                    oDocI.IdTipoDocumento = tblDocumentoBusiness.TipoDocumentoEnum.Remision.GetHashCode();
                    TipoDocumento = "Remisión";
                }
                else if (rdbFacturaVenta.Checked)
                {
                    oDocI.IdTipoDocumento = tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode();
                    TipoDocumento = "Factura de venta";
                }
                else {
                    oDocI.IdTipoDocumento = tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode();
                    TipoDocumento = "Cotización";
                }
                oDocI.EnCuadre = false;
                oDocI.Devuelta = 0;
                oDocI.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode();
                oDocI.FechaVencimiento = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                if (!oDocB.GuardarVentaRapida(oDocI, hddItems.Value))
                {
                    MostrarMensaje("Error", "No se pudo guardar la venta rapida.");
                }
                else {
                    ImprimirFactura(oDocI.idDocumento, TipoDocumento, oDocI.IdTipoDocumento);
                    hddItems.Value = "";
                    hddValorTotal.Value = "0";
                    hddValorIVA.Value = "0";
                }
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo registrar la venta rapida. {0}", ex.Message.Replace("'","")));
            }
        }

        private void ImprimirFactura(long IdFacturaVenta, string TipoDocumento, long IdTipoDocumento) {
            tblEmpresaItem oEmpI = new tblEmpresaItem();
            tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
            tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
            tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
            tblDocumentoItem oDocI = oDBiz.traerDocumentoPorId(IdTipoDocumento, IdFacturaVenta);
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
            string strScript = string.Format("jQuery(document).ready(function(){{ ImprimirDocumentoVentaRapida(\"{0}\");}});", Mensaje);
            if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
            }
        }

        protected void btnActualizarPrecios_Click(object sender, EventArgs e)
        {

        }
    }
}