using System;
using System.Configuration;
using System.Web.UI;
using InventarioItem;
using Idioma;
using HQSFramework.Base;

namespace Inventario
{
    public partial class frmMantenimientos : PaginaBase
    {

        private string cadenaConexion;
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

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
                }
                else
                {
                    Response.Redirect("frmInicioSesion.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar la pagina de menu. " + ex.ToString() + "');</script>");
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
        }

        protected void imbSalir_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove("Usuario");
            Response.Redirect("frmInicioSesion.aspx");
        }
    }
}