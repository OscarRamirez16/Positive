using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using Idioma;
using HQSFramework.Base;

namespace Inventario
{
    public partial class frmVerDocumentos : PaginaBase
    {

        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();        
        SeguridadBusiness.paginasEnum PaginaID;
        private string Documento = "";

        public enum dgDocumentosEnum
        {
            idDocumento = 0,
            numDocumento = 1,
            fecha = 2,
            tercero = 3,
            datosTercero = 4,
            idEstado = 5,
            Estado = 6,
            TotalDocumento = 7,
            Observaciones = 8,
            txtObservaciones = 9,
            seleccionar = 10,
            //Anular = 11,
            Imprimir = 11,
            ImprimirFormal = 12
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarMensaje();
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    ConfiguracionIdioma();
                    SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(PaginaID.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        if (!IsPostBack)
                        {
                            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
                            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
                            CargarDocumentos();
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerTituloPagina('{1}');", strScript, lblTipoDocumento.Text);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaInicial.ClientID);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaFinal.ClientID);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }
                    else
                    {
                        Response.Redirect("frmMantenimientos.aspx");
                    }
                }
                else
                {
                    Response.Redirect("frmInicioSesion.aspx");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagina. {0}", ex.Message));
            }
            
        }
        public void CargarOpcionDocumento(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            if (tblTipoDocumentoItem.TipoDocumentoEnum.venta.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
            {
                lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FacturasVentas);
                hddTipoDocumento.Value = "1";
                PaginaID = SeguridadBusiness.paginasEnum.VerVentas;
                Documento = "Factura de venta POS";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.compra.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
            {
                lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FacturasCompras);
                hddTipoDocumento.Value = "2";
                PaginaID = SeguridadBusiness.paginasEnum.VerCompras;
                Documento = "Factura de compra";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.cotizacion.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
            {
                lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cotizacion);
                hddTipoDocumento.Value = "3";
                PaginaID = SeguridadBusiness.paginasEnum.VerCotizaciones;
                Documento = "Cotización";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.notaCreditoVenta.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
            {
                lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Devolucion);
                hddTipoDocumento.Value = "4";
                PaginaID = SeguridadBusiness.paginasEnum.NotaCreditoVenta;
                Documento = "Nota Crédito Venta";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.entradaMercancia.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
            {
                lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.EntradaMercacia);
                hddTipoDocumento.Value = "5";
                PaginaID = SeguridadBusiness.paginasEnum.EntradaMercancia;
                Documento = "Entrada Mercancía";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.salidaMercancia.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
            {
                lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SalidaMercancia);
                hddTipoDocumento.Value = "6";
                PaginaID = SeguridadBusiness.paginasEnum.SalidaMercancia;
                Documento = "Salida Mercancía";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.notaCreditoCompra.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
            {
                lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Devolucion);
                hddTipoDocumento.Value = "7";
                PaginaID = SeguridadBusiness.paginasEnum.NotaCreditoCompra;
                Documento = "Nota Crédito Compra";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.Remision.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
            {
                lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Remision);
                hddTipoDocumento.Value = "8";
                PaginaID = SeguridadBusiness.paginasEnum.ConsultarRemisiones;
                Documento = "Remisión";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.CuentaCobro.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
            {
                lblTipoDocumento.Text = "Cuenta de Cobro";
                hddTipoDocumento.Value = "10";
                PaginaID = SeguridadBusiness.paginasEnum.CuentaCobro;
                Documento = "Cuenta de Cobro";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.DocumentoElectronico.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
            {
                lblTipoDocumento.Text = "Documento Electrónico";
                hddTipoDocumento.Value = "11";
                PaginaID = SeguridadBusiness.paginasEnum.VerVentas;
                Documento = "Documento Electrónico";
            }
        }
        private void ConfiguracionIdioma()
        {
            Traductor oCIdioma = new Traductor();
            short Opcion;
            Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
            if (string.IsNullOrEmpty(Request.Form["ctl00$ddlIdiomas"]))
            {
                Opcion = oUsuarioI.IdIdioma;
            }
            else
            {
                Opcion = short.Parse(Request.Form["ctl00$ddlIdiomas"]);
            }
            if (Opcion == Traductor.IdiomaEnum.Espanol.GetHashCode())
            {
                Idioma = Traductor.IdiomaEnum.Espanol;
            }
            if (Opcion == Traductor.IdiomaEnum.Ingles.GetHashCode())
            {
                Idioma = Traductor.IdiomaEnum.Ingles;
            }
            if (Opcion == Traductor.IdiomaEnum.Aleman.GetHashCode())
            {
                Idioma = Traductor.IdiomaEnum.Aleman;
            }
            lblFechaInical.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaInicial);
            lblFechaFinal.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaFinal);
            lblDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Numero);
            lblCliente.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
            lblIdentificacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion);
            CargarOpcionDocumento(oCIdioma, Idioma);
        }
        public void CargarDocumentos()
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(cadenaConexion);
                dgDocumentos.DataSource = oDocB.traerDocumentosLista(long.Parse(hddTipoDocumento.Value), DateTime.Parse(txtFechaInicial.Text), DateTime.Parse(txtFechaFinal.Text), oUsuarioI.idEmpresa, txtDocumento.Text.Trim(), txtCliente.Text.Trim(), txtIdentificacion.Text.Trim());
                dgDocumentos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void dgDocumentos_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (oRolPagI.Leer)
            {
                Response.Redirect("frmDocumentos.aspx?idDocumento=" + e.Item.Cells[dgDocumentosEnum.idDocumento.GetHashCode()].Text + "&opcionDocumento=" + hddTipoDocumento.Value + "&consulta=1");
            }
            else
            {
                MostrarMensaje("Error", "El usuario no posee permisos pata esta operación");
            }
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CargarDocumentos();
        }
        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
        protected void dgDocumentos_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Imprimir")
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                if (oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, SeguridadBusiness.paginasEnum.ImprimirFacturaVenta.GetHashCode()).Leer)
                {
                    tblDocumentoItem oDocI = new tblDocumentoItem();
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(cadenaConexion);
                    oDocI = oDocB.traerDocumentoPorId(long.Parse(hddTipoDocumento.Value), long.Parse(e.Item.Cells[dgDocumentosEnum.idDocumento.GetHashCode()].Text));
                    if(oDocI.idDocumento > 0)
                    {
                        tblEmpresaItem oEmpI = new tblEmpresaItem();
                        tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(cadenaConexion);
                        tblTerceroBusiness oTerB = new tblTerceroBusiness(cadenaConexion);
                        oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                        string Mensaje = "";
                        string Detalles = "";
                        Detalles = "<table border='1' style='width:100%'><tr><td align='center'>Cant</td><td align='center'>Descripcion</td><td align='center'>Valor</td></tr>";
                        foreach (tblDetalleDocumentoItem Item in oDocI.DocumentoLineas)
                        {
                            Detalles = Detalles + "<tr><td align='center'>" + Item.Cantidad + "</td>";
                            if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Compra.GetHashCode().ToString())
                            {
                                if (oEmpI.idEmpresa == 29 || oEmpI.idEmpresa == 31 || oEmpI.idEmpresa == 58)
                                {
                                    Detalles = string.Format("{0}<td>{1}- {2}. {3}</td>", Detalles, Item.Codigo, Item.Articulo.Replace("\"", ""), Item.PrecioCosto);
                                }
                                else
                                {
                                    Detalles = Detalles + "<td>" + Item.Articulo.Replace("\"", "") + "</td>";
                                }
                            }
                            else
                            {
                                Detalles = Detalles + "<td>" + Item.Articulo.Replace("\"", "") + "</td>";
                            }
                            Detalles = Detalles + "<td  align='right' style='width: 20%'>" + Item.TotalLinea.ToString(Util.ObtenerFormatoDecimal()) + "</td></tr>";
                        }
                        Detalles = Detalles + "</table>";
                        string FormaPago = "";
                        FormaPago = "<table border='1' style='width:100%'><tr><td align='center'>Forma</td><td align='center'>Valor</td></tr>";
                        if(oDocI.FormasPago.Count > 0)
                        {
                            foreach (tblTipoPagoItem Item in oDocI.FormasPago)
                            {
                                FormaPago = FormaPago + "<tr><td>" + Item.FormaPago + "</td>";
                                FormaPago = FormaPago + "<td align='right'>" + Item.ValorPago.ToString(Util.ObtenerFormatoDecimal()) + "</td></tr>";
                            }
                        }
                        else
                        {
                            FormaPago = FormaPago + "<tr><td colspan='2'>Documento a crédito.</td></tr>";
                        }
                        FormaPago = FormaPago + "</table>";
                        Mensaje = string.Format("<div style='position:relative;font-family:arial;'>" +
                        "<div style='font-size: 18px;font-weight: bold; padding-top: 0px; width: 300px; text-align: center;'>{0}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Nit: {1}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Direcci&oacute;n: {2}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Telefono: {3}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center;'>{4}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{5}</div>" +
                        "<div style='font-size: 20px;font-weight: bold; padding-top: 2px; width: 300px;'>Numero Documento: {6}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{7}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Tercero: {8}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Identificaci&oacute;n: {9}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center; width:300px;'>DETALLES</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{10}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Antes de IVA: {11}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Descuento: {19}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Valor IVA: {12}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Impoconsumo: {20}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Propina: {21}</div>" +
                        "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Total a pagar: {13}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center;'>Formas de Pago</div>" +
                        "<div style='font-size: 10px;font-weight: bold; padding-top: 2px; width: 300px;'>{17}</div>" +
                        "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Cambio: {18}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Vende: {14}</div>" +
                        "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px;'>Observaciones: {15}</div>" +
                        "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{16}</div>" +
                        "</div>", oEmpI.Nombre.Replace("\"", ""), oEmpI.Identificacion, oEmpI.Direccion.Replace("\"", ""), oEmpI.Telefono, Documento,
                        string.Format("{0} - {1}", oEmpI.TextoEncabezadoFactura.Replace(Environment.NewLine, " ").Replace("\"", ""), oDocI.Resolucion.Replace(Environment.NewLine, " ")),
                        oDocI.NumeroDocumento, oDocI.Fecha, oDocI.NombreTercero.Replace("\"", ""), oTerB.ObtenerTerceroPorId(oDocI.idTercero, oUsuarioI.idEmpresa).Identificacion,
                        Detalles, oDocI.TotalAntesIVA.ToString(Util.ObtenerFormatoDecimal()),
                        oDocI.TotalIVA.ToString(Util.ObtenerFormatoDecimal()), oDocI.TotalDocumento.ToString(Util.ObtenerFormatoDecimal()),
                        oUsuarioI.Usuario, oDocI.Observaciones.Replace("\"", ""), oEmpI.TextoPieFactura.Replace("\"", ""), FormaPago, oDocI.Devuelta.ToString(Util.ObtenerFormatoDecimal()),
                        oDocI.TotalDescuento.ToString(Util.ObtenerFormatoDecimal()), oDocI.Impoconsumo.ToString(Util.ObtenerFormatoDecimal()), oDocI.Propina.ToString(Util.ObtenerFormatoDecimal()));
                        string strScript = string.Format("jQuery(document).ready(function(){{ ReImprimirDocumentoServidor(\"{0}\");}});", Mensaje);
                        if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
                        }
                    }
                }
            }
            if (e.CommandName == "ImprimirFormal")
            {
                Response.Redirect(string.Format("frmImprimirDocumento.aspx?IdDocumento={0}&TipoDocumento={1}", long.Parse(e.Item.Cells[dgDocumentosEnum.idDocumento.GetHashCode()].Text), long.Parse(hddTipoDocumento.Value)));
            }
            else if (e.CommandName == "AnularFactura" && hddTipoDocumento.Value == "1")
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.AnularFacturaVenta.GetHashCode().ToString()));
                if (oRolPagI.Insertar && !ValidarFacturaConDevolucion(e.Item.Cells[dgDocumentosEnum.numDocumento.GetHashCode()].Text))
                {
                    if (!this.ClientScript.IsClientScriptBlockRegistered("AnularFactura"))
                    {
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "AnularFactura", $"AnularFacturaVenta({e.Item.Cells[dgDocumentosEnum.idDocumento.GetHashCode()].Text});", true);
                    }
                }
                else
                {
                    if (ValidarCajaAbierta() && !ValidarFacturaConDevolucion(e.Item.Cells[dgDocumentosEnum.numDocumento.GetHashCode()].Text))
                    {
                        if (!this.ClientScript.IsClientScriptBlockRegistered("AnularFactura"))
                        {
                            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "AnularFactura", $"AnularFacturaVenta({e.Item.Cells[dgDocumentosEnum.idDocumento.GetHashCode()].Text});", true);
                        }
                    }
                    else
                    {
                        MostrarMensaje("Anular Factura", "El usuario no tiene caja abierta o la factura ya tiene una DV");
                    }
                }
            }
        }
        private bool ValidarFacturaConDevolucion(string NumeroFactuta)
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(cadenaConexion);
                if (oDocB.ObtenerDevolucionPorReferencia(NumeroFactuta, oUsuarioI.idEmpresa).idDocumento > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidarCajaAbierta()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["consulta"]) || Request.QueryString["TipoDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString())
                {
                    tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(cadenaConexion);
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
                        tblCajaItem oCajaI = new tblCajaItem();
                        tblCajaBusiness oCajaB = new tblCajaBusiness(cadenaConexion);
                        oCajaI = oCajaB.ObtenerCajaPorID(oCuaI.idCaja);
                        return true;
                    }
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
    }
}