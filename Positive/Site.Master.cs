using HQSFramework.Base;
using Idioma;
using InventarioBusiness;
using InventarioItem;
using System;
using System.Configuration;
using System.Web.UI;

namespace Positive
{
    public partial class SiteMaster : MasterPage
    {
        private string CadenaConexion;
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
        protected void Page_Load(object sender, EventArgs e)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                if (!IsPostBack)
                {
                    CargarDatos(oUsuarioI);
                    if(oUsuarioI.IdIdioma.ToString() == Traductor.IdiomaEnum.Espanol.GetHashCode().ToString())
                    {
                        Idioma = Traductor.IdiomaEnum.Espanol;
                    }
                    if (oUsuarioI.IdIdioma.ToString() == Traductor.IdiomaEnum.Ingles.GetHashCode().ToString())
                    {
                        Idioma = Traductor.IdiomaEnum.Ingles;
                    }
                }
                ConfiguracionIdioma();
            }
            else
            {
                Response.Redirect("frmInicioSesion.aspx");
            }
        }
        public void CargarDatos(tblUsuarioItem usuario)
        {
            tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
            tblEmpresaItem oEmpI = new tblEmpresaItem();
            oEmpI = oEmpB.ObtenerEmpresa(usuario.idEmpresa);
            lblNombreEmpresa.Text = oEmpI.Nombre;
            lblUsuario.InnerText = usuario.Usuario;
        }
        protected void lnkEspanol_Click(object sender, EventArgs e)
        {
            try
            {
                tblUsuarioBusiness oUsuB = new tblUsuarioBusiness(CadenaConexion);
                if (oUsuB.ModificarIdiomaUsuario(oUsuarioI.idUsuario, short.Parse(Traductor.IdiomaEnum.Espanol.GetHashCode().ToString())))
                {
                    Session.Remove("Usuario");
                    Session["Usuario"] = oUsuB.ObtenerUsuario(oUsuarioI.idUsuario);
                    Idioma = Traductor.IdiomaEnum.Espanol;
                    ConfiguracionIdioma();
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void lnkIngles_Click(object sender, EventArgs e)
        {
            try
            {
                tblUsuarioBusiness oUsuB = new tblUsuarioBusiness(CadenaConexion);
                if (oUsuB.ModificarIdiomaUsuario(oUsuarioI.idUsuario, short.Parse(Traductor.IdiomaEnum.Ingles.GetHashCode().ToString())))
                {
                    Session.Remove("Usuario");
                    Session["Usuario"] = oUsuB.ObtenerUsuario(oUsuarioI.idUsuario);
                    Idioma = Traductor.IdiomaEnum.Ingles;
                    ConfiguracionIdioma();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ConfiguracionIdioma()
        {
            Traductor oCIdioma = new Traductor();
            lblMenu.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Menu);
            lblVentas.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ventas);
            lblFacturaVenta.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FacturaVenta);
            lblConsultarFacturaVenta.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FacturaVenta));
            lblDevolucion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Devolucion);
            lblConsultarDevolucion.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Devolucion));
            lblVentaRapida.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Rapida), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FacturaVenta));
            lblRemision.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Remision);
            lblConsultarRemision.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Remision));
            lblRemisionRapida.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Rapida), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Remision));
            lblCuentaCobro.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuentaCobro);
            lblConsultarCuentaCobro.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuentaCobro));
            lblAnticipo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Anticipo);
            lblConsultarAnticipo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Anticipo));
            lblConciliacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Conciliacion);
            lblConsultarConciliacion.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Conciliacion));
            lblCajas.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cajas);
            lblSubCajas.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja);
            lblAbrirCaja.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Abrir), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja));
            lblCerrarCaja.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CerrarCaja);
            lblRetiro.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.RetirarEfectivo);
            lblIngreso.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IngresarEfectivo);
            lblInventario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Inventario);
            lblArticulos.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulos);
            lblBodega.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodegas);
            lblGrupoArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoArticulo);
            lblCreacionMasivaArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CreacionMasivaArticulo);
            lblActualizacionMasivaArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ActualizacionMasivaArticulo);
            lblProduccion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Produccion);
            lblCamapna.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana);
            lblEntradaMercancia.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.EntradaMercacia);
            lblConsultarEntradaMercancia.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.EntradaMercacia));
            lblSalidaMercancia.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SalidaMercancia);
            lblConsultarSalidaMercancia.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SalidaMercancia));
            lblEntradaSalidaMasiva.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.EntradaSalidaMasiva);
            lblListaPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ListasPrecios);
            lblTrasladoMercancia.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TrasladoMercancia);
            lblArticuloCompuesto.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ArticuloCompuesto);
            lblCompras.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Compra);
            lblFacturaCompra.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FacturaCompra);
            lblConsultarFacturaCompras.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FacturasCompras));
            lblDevolucionCompra.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Devolucion);
            lblConsultarDevolucionCompra.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Devolucion));
            lblSocioNegocio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SociosNegocio);
            lblSubSocioNegocio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio);
            lblConsultarSocioNegocio.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio));
            lblGrupoSocioNegocio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoSocioNegocio);
            lblVendedor.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Vendedor);
            lblConsultarDocumentoElectronico.Text = "Documento Electronico";
        }
    }
}