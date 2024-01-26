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
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Inventario
{
    public partial class frmVentaRapida : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private long idDocumento = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                long.TryParse(Request.QueryString["idDocumento"], out idDocumento);
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (!IsPostBack)
                {
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
                                rdbFacturaVenta.Enabled = false;
                                rdbRemision.Enabled = false;
                                rdbCotizacion.Enabled = false;
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
                        if (ValidarCajaAbierta() || rdbCotizacion.Checked)
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
                            strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','1');", strScript, txtBodega.ClientID, hddIdBodega.ClientID, oUsuarioI.idEmpresa);
                            if (rdbCotizacion.Checked && idDocumento > 0)
                            {
                                List<CotizacionVentaRapidaItem> items = (new tblDocumentoBusiness(CadenaConexion)).ObtenerCotizacionVentaRapida(idDocumento, oUsuarioI.idEmpresa);
                                if (items != null && items.Count > 0)
                                {
                                    foreach (CotizacionVentaRapidaItem item in items)
                                    {
                                        strScript = string.Format("{0} AdicionarVentaRapida({1}, {2}, '{3}', '{4}', {5}, {6}, 1000, '{7}', {8}, true);", strScript, item.idVentaRapida, item.Articulo, item.Descripcion, item.Cantidad, item.ValorIVA, item.Precio, hddItems.ClientID, 0);
                                    }
                                }
                            }
                            strScript = string.Format("{0}}});", strScript);
                            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                            {
                                this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                            }
                        }
                        else
                        {
                            MostrarAlerta(0, "Error", "El usuario no tiene una caja abierta.");
                            btnGuardar.Visible = false;
                        }
                    }
                    else
                    {
                        Response.Redirect("frmMantenimientos.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
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
            btnCancelar.Attributes.Add("onclick", string.Format("LimpiarFacturaVentaRapida('{0}');return false;", hddItems.ClientID));
            btnActualizarPrecios.Style.Add("display", "none");
            txtTercero.Attributes.Add("onblur", string.Format("ActualizarPreciosVentaRapida('{0}','{1}')", btnActualizarPrecios.ClientID, hddIdCliente.ClientID));
            hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
            tblTerceroItem oTerI = new tblTerceroItem();
            tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
            oTerI = oTerB.ObtenerTerceroPorIdentificacion("123456789", oUsuarioI.idEmpresa);
            if (!IsPostBack)
            {
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
            txtBodega.Attributes.Add("placeholder", string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega)));
        }
        private void PintarArticulos()
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
            sbVRHtml.AppendLine(string.Format("<img id=\"img_{0}\" style=\"cursor:pointer;\" alt = \"{1}\"/>", oVRItem.idVentaRapida, oVRItem.Nombre));
            //sbVRHtml.AppendLine(string.Format("<img style=\"cursor:pointer;\" src = \"frmMostrarImagen.aspx?IdVentaRapida={0}\" alt = \"{1}\"/>",oVRItem.idVentaRapida,oVRItem.Nombre));
            sbVRHtml.AppendLine("</div>");
            sbVRHtml.AppendLine("<div class = \"caption\">");
            sbVRHtml.AppendLine(string.Format("<input type='hidden' id='hdd{0}Stock' value='{1}'/>", oVRItem.idVentaRapida, oVRItem.Stock));
            sbVRHtml.AppendLine(string.Format("<b>{0}</b>", oVRItem.Nombre));
            sbVRHtml.AppendLine(string.Format("<br/><span>{0}:&nbsp;<b>{1}</b></span>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Disponibles), oVRItem.Stock));
            sbVRHtml.AppendLine(string.Format("<br/><span>{0}:&nbsp;<b id='{2}'>{1:0.00}</b></span><br/>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad), oVRItem.Cantidad, oVRItem.idVentaRapida));
            sbVRHtml.AppendLine(string.Format("<span>{0}:&nbsp;<b>{1:0.00}</b></span>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Precio), oVRItem.Precio));
            sbVRHtml.AppendLine("</div>");
            sbVRHtml.AppendLine(string.Format("<div class = \"vrFacturar\" onclick=\"FaturarVentaRapida({0},'{1}','{2}','{3:0.00}','{4:0.00}','{5:0.00}','{6:0.00}','{7}','{8}');\">", oVRItem.idVentaRapida, oVRItem.idArticulo, oVRItem.Articulo, oVRItem.Cantidad, oVRItem.ValorIVA, oVRItem.Precio, oVRItem.Stock, hddItems.ClientID, oUsuarioI.Impoconsumo));
            sbVRHtml.AppendLine("<p>Facturar</p></div>");
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
            List<ImageItem> imagesList = new List<ImageItem>();
            foreach (tblVentaRapidaItem oVRItem in oVRLista)
            {
                imagesList.Add(new ImageItem()
                {
                    ControlID = $"img_{oVRItem.idVentaRapida}",
                    src = $"frmMostrarImagen.aspx?IdVentaRapida={oVRItem.idVentaRapida}"
                });
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
            hddImageSource.Value = JsonConvert.SerializeObject(imagesList);
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
            sbVRHtml.AppendLine(string.Format("<img id=\"img_{0}\" style=\"cursor:pointer;\" alt = \"{1}\"/>", oVRItem.idVentaRapida, oVRItem.Nombre));
            //sbVRHtml.AppendLine(string.Format("<img style=\"cursor:pointer;\" src = \"frmMostrarImagen.aspx?IdVentaRapida={0}\" alt = \"{1}\"/>", oVRItem.idVentaRapida, oVRItem.Nombre));
            sbVRHtml.AppendLine("<div class = \"containerCard\">");
            sbVRHtml.AppendLine(string.Format("<input type='hidden' id='hdd{0}Stock' value='{1}'/>", oVRItem.idVentaRapida, oVRItem.Stock));
            sbVRHtml.AppendLine(string.Format("<p class=\"title\"><b>{0}</b></p>", oVRItem.Nombre));
            sbVRHtml.AppendLine(string.Format("<p>{0}:&nbsp;<b>{1:N3}</b></p>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Disponibles), oVRItem.Stock));
            sbVRHtml.AppendLine(string.Format("<p>{0}:&nbsp;<b id='{2}'>{1:N0}</b></p>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad), oVRItem.Cantidad, oVRItem.idVentaRapida));
            sbVRHtml.AppendLine(string.Format("<p>{0}:&nbsp;<b>{1:C0}</b></p>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Precio), oVRItem.Precio));
            sbVRHtml.AppendLine("</div>");
            sbVRHtml.AppendLine("</div>");
            sbVRHtml.AppendLine(string.Format("<div class = \"vrFacturar\" onclick=\"FaturarVentaRapida({0},'{1}','{2}','{3:0.00}','{4:0.00}','{5:0.00}','{6:0.00}','{7}','{8}');\">", oVRItem.idVentaRapida, oVRItem.idArticulo, oVRItem.Articulo, oVRItem.Cantidad, oVRItem.ValorIVA, oVRItem.Precio, oVRItem.Stock, hddItems.ClientID, oUsuarioI.Impoconsumo));
            sbVRHtml.AppendLine("<p>Facturar</p></div>");
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
                //string TipoDocumento = "";
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
                oDocI.idDocumento = idDocumento;
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
                oDocI.TotalIVA = decimal.Parse(hddValorIVA.Value.Replace(".", ",").Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                oDocI.Impoconsumo = decimal.Parse(hddValorImpoconsumo.Value.Replace(".", ",").Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                oDocI.saldo = oDocI.TotalDocumento;
                if (rdbRemision.Checked)
                {
                    oDocI.IdTipoDocumento = tblDocumentoBusiness.TipoDocumentoEnum.Remision.GetHashCode();
                    //TipoDocumento = "Remisión";
                }
                else if (rdbFacturaVenta.Checked)
                {
                    oDocI.IdTipoDocumento = tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode();
                    //TipoDocumento = "Factura de venta";
                }
                else
                {
                    oDocI.IdTipoDocumento = tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode();
                    oDocI.TipoDocumento = tblTipoDocumentoItem.TipoDocumentoEnum.cotizacion;
                    //TipoDocumento = "Cotización";
                }
                oDocI.EnCuadre = false;
                oDocI.Devuelta = 0;
                oDocI.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode();
                oDocI.FechaVencimiento = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                if (!oDocB.GuardarVentaRapida(oDocI, hddItems.Value, long.Parse(hddIdBodega.Value)))
                {
                    MostrarMensaje("Error", "No se pudo guardar la venta rapida.");
                }
                else
                {
                    Response.Redirect($"frmImprimirPOS.aspx?idDocumento={oDocI.idDocumento}&IdTipoDocumento={oDocI.IdTipoDocumento}");
                    //Response.Write($"<script>window.open('frmImprimirPOS.aspx?idDocumento={oDocI.idDocumento}&IdTipoDocumento={oDocI.IdTipoDocumento}','_blank');</script>");
                    hddItems.Value = "";
                    hddValorTotal.Value = "0";
                    hddValorIVA.Value = "0";
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo registrar la venta rapida. {0}", ex.Message.Replace("'", "")));
            }
        }
        protected void btnActualizarPrecios_Click(object sender, EventArgs e)
        {

        }
    }
}