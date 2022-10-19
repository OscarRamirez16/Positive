using System;
using System.Configuration;
using System.Web.UI;
using InventarioItem;
using Idioma;
using HQSFramework.Base;
using InventarioBusiness;
using System.Data;

namespace Inventario
{
    public partial class frmMantenimientos : PaginaBase
    {

        private string cadenaConexion;
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum FacturasPendientesEnum
        {
            Fecha = 0,
            NumeroDocumento = 1,
            Nombre = 2,
            Direccion = 3,
            Telefono = 4,
            Observaciones = 5,
            TotalDocumento = 6,
            saldo = 7,
            TotalAntesIVA = 8
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI!= null && oUsuarioI.idUsuario != 0)
                {
                    cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    if (!string.IsNullOrEmpty(Request.QueryString["Error"]))
                    {
                        MostrarMensaje("Error", Request.QueryString["Error"]);
                    }
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                    }
                    ConfiguracionIdioma();
                    if (!IsPostBack)
                    {
                        ObtenerNotificaciones();
                    }
                }
                else
                {
                    Response.Redirect("frmInicioSesion.aspx");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", string.Format("No se pudo cargar la pagina. {0}", ex.Message));
            }
        }
        private void ObtenerNotificaciones()
        {
            try
            {
                string Mensaje = string.Empty;
                string Cadena = ConfigurationManager.ConnectionStrings["Backend"].ConnectionString;
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(Cadena);
                DataTable FacturasPendientes = oDocB.ObtenerFacturasPendientesPorPago(oUsuarioI.IdTercero);
                if(FacturasPendientes.Rows.Count > 0)
                {
                    foreach (DataRow row in FacturasPendientes.Rows)
                    {
                        if (string.IsNullOrEmpty(Mensaje))
                        {
                            Mensaje = string.Format("* Tiene pendiente por pago la factura No {0} por un valor de {1}, observaciones: {2}", row[FacturasPendientesEnum.NumeroDocumento.GetHashCode()], decimal.Parse(row[FacturasPendientesEnum.TotalDocumento.GetHashCode()].ToString()).ToString(Util.ObtenerFormatoDecimal()), row[FacturasPendientesEnum.Observaciones.GetHashCode()]);
                        }
                        else
                        {
                            Mensaje += string.Format(" * Tiene pendiente por pago la factura No {0} por un valor de {1}, observaciones: {2}", row[FacturasPendientesEnum.NumeroDocumento.GetHashCode()], decimal.Parse(row[FacturasPendientesEnum.TotalDocumento.GetHashCode()].ToString()).ToString(Util.ObtenerFormatoDecimal()), row[FacturasPendientesEnum.Observaciones.GetHashCode()]);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(Mensaje))
                {
                    MostrarAlerta(0, "Advertencia", Mensaje);
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message);
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
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.MenuRapido);
            //Menu cajas
            lblCajas.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cajas);
            lblSubCaja.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cajas);
            lblSubAbrirCaja.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IniciarCaja);
            lblSubCerrarCaja.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CerrarCaja);
            lblRetirar.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Retirar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Efectivo));
            lblIngresar.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IngresarEfectivo);
            //Menu artículos
            lblArticulos.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Inventario);
            lblSubArticulos.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulos);
            lblSubBodegas.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodegas);
            lblSubLineas.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lineas);
            lblSubCreacionMasiva.Text = string.Format("{0} {1} {2} {3}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Creacion), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Masiva), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulos));
            lblProduccion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Produccion);
            lblCampana.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana);
            lblGrupoCliente.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoCliente);
            lblSubEntradaMercancia.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.EntradaMercacia);
            lblSubSalidaMercancia.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SalidaMercancia);
            lblSubEntradaMasivaMercancia.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.EntradaMercacia), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Masiva));
            lblSubListaPrecio.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Listas), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Precios));
            lblSubTrasladoMercancia.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TrasladoMercancia);
            //Menu terceros
            lblTerceros.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio);
            lblSubCrearTerceros.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SociosNegocio);
            lblSubConsultarTerceros.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SociosNegocio));
            lblVendedor.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Vendedor);
            //Menu ventas
            lblVentas.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ventas);
            lblSubVentas.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Factura);
            lblSubConsultarVentas.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Factura));
            lblNotaCreditoVentas.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Devolucion);
            lblVentaRapida.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.VentaRapida);
            //Menu compras
            lblCompras.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Compra);
            lblSubCompras.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Factura);
            lblSubConsultarCompras.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Factura));
            lblNotaCreditoCompras.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Devolucion);
            //Menu Cotizaciones
            lblCotizaciones.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cotizaciones);
            lblSubCotizacion.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cotizacion));
            lblSubConsultarCotizaciones.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Consultar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cotizacion));
            //Menu Administracion
            lblAdministracion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Administracion);
            lblSubCuentasCobrar.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuentasCobrar);
            lblSubCuentasPagar.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuentasPagar);
            lblTipoTarjetaCredito.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoTarjetaCredito);
            lblGestionVentaRapida.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GestionVentaRapida);
            lblCrearUsuarios.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario));
            lblConsultarUsuarios.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Consultar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario));
            //Menu Reportes
            lblReportes.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Reportes);
            lblSubMovimientosDocumentos.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.MovimientosDocumentos);
            lblSubDocumentosRangoFechas.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.DocumentosRangoFechas);
            lblSubArticuloBodega.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ArticulosBodega);
            lblSubPagosCliente.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.PagosCliente);
            lblSubPagosProveedor.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.PagosProveedor);
            lblSubCuadreDiario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuadreDiario);
            lblSubMovRetirosIngresos.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.MovimientosRetiroIngreso);
            lblSubImprimirCuadreDiario.Text = string.Format("Imprimir {0}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuadreDiario));
            lblSubReporteComisionesVentasPorArticulo.Text = "Reporte Comisiones Ventas Por Articulo";
        }

        protected void imbSalir_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove("Usuario");
            Response.Redirect("frmInicioSesion.aspx");
        }
    }
}