using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;
using System.IO;

namespace Inventario
{
    public partial class frmCrearEditarConsultarVentaRapida : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private long IdVentaRapida = 0;

        private enum dgVentaRapidaColumnEnum
        {
            idVentaRapida = 0,
            Nombre = 1,
            idArticulo = 2,
            Articulo = 3,
            Linea = 4,
            Cantidad = 5,
            Activo = 6,
            lnkEditar = 7
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarConsultarVentaRapida.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {
                    if (Util.EsEntero(Request.QueryString["IdVentaRapida"]))
                    {
                        IdVentaRapida = long.Parse(Request.QueryString["IdVentaRapida"]);
                    }
                    if (Request.QueryString["VentaRapidaActualizada"] == "true")
                    {
                        setlocalStorage("VentaRapidaActualizada", "false");
                    }
                    ConfiguracionIdioma();
                    CargarInformacion();
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}','{3}','3');", strScript, txtArticulo.ClientID, hddIdArticulo.ClientID, oUsuarioI.idEmpresa);
                        strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}','{3}','3');", strScript, txtArticuloBusqeuda.ClientID, hddIdArticuloBusqueda.ClientID, oUsuarioI.idEmpresa);
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
            lblidVentaRapidaLabel.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Codigo);
            lblNombre.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.VentaRapida);
            lblArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
            lblCantidad.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad);
            lblActivo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Activo);
            lblImagen.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Imagen);
            liVentaRapida.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.VentaRapida);
            liBuscarVentaRapida.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.VentaRapida));
            lblTituloBusqueda.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.VentaRapida));
            lblNombreBusqueda.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            lblArticuloBusqueda.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
        }
        private void CargarInformacion()
        {
            if (IdVentaRapida > 0 && !IsPostBack)
            {
                tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
                tblVentaRapidaItem oVRItem = oDBiz.ObtenerVentaRapida(IdVentaRapida, oUsuarioI.idEmpresa);
                if (oVRItem != null && oVRItem.idVentaRapida > 0)
                {
                    lblidVentaRapida.Text = oVRItem.idVentaRapida.ToString();
                    txtNombre.Text = oVRItem.Nombre;
                    hddIdArticulo.Value = oVRItem.idArticulo.ToString();
                    txtArticulo.Text = oVRItem.Articulo;
                    txtCantidad.Text = oVRItem.Cantidad.ToString();
                    chkActivo.Checked = oVRItem.Activo;
                    imgOutput.Src = string.Format("frmMostrarImagen.aspx?IdVentaRapida={0}", IdVentaRapida);
                }
            }
        }
        private string ValidarInformacion()
        {
            string MensajeError = "";
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
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MensajeError = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.NombreObligatorio);
            }
            if (string.IsNullOrEmpty(hddIdArticulo.Value))
            {
                MensajeError = string.Format("{0}<br/>{1}", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ArticuloObligatorio));
            }
            if (!Util.EsDecimal(txtCantidad.Text))
            {
                MensajeError = string.Format("{0}<br/>{1}", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CantidadFormato));
            }
            if (string.IsNullOrEmpty(fluImagen.Value) && IdVentaRapida == 0)
            {
                MensajeError = string.Format("{0}<br/>{1}", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ImagenObligatorio));
            }
            return MensajeError;
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
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
            string MensajeError = ValidarInformacion();
            if (string.IsNullOrEmpty(MensajeError))
            {
                tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
                tblVentaRapidaItem oVRItem;
                if (IdVentaRapida > 0)
                {
                    oVRItem = oDBiz.ObtenerVentaRapida(IdVentaRapida, oUsuarioI.idEmpresa);
                }
                else
                {
                    oVRItem = new tblVentaRapidaItem();
                    oVRItem.Fecha = DateTime.Now;
                    oVRItem.idUsuario = oUsuarioI.idUsuario;
                    oVRItem.idEmpresa = oUsuarioI.idEmpresa;
                }
                if (!string.IsNullOrEmpty(fluImagen.Value))
                {
                    System.Drawing.Bitmap oBitmap = new System.Drawing.Bitmap(fluImagen.PostedFile.InputStream);
                    System.Drawing.Bitmap ImagenPequeña = oDBiz.CambiarTamanoImagen((System.Drawing.Image)oBitmap, Util.ImagenAncho(), Util.ImagenAlto()); ;
                    using (var stream = new MemoryStream())
                    {
                        ImagenPequeña.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        oVRItem.Imagen = stream.ToArray();
                    }
                }
                oVRItem.Nombre = txtNombre.Text;
                oVRItem.idArticulo = long.Parse(hddIdArticulo.Value);
                oVRItem.Cantidad = decimal.Parse(txtCantidad.Text);
                oVRItem.Activo = chkActivo.Checked;
                if (oDBiz.Guardar(oVRItem))
                {
                    Response.Redirect("frmCrearEditarConsultarVentaRapida.aspx?VentaRapidaActualizada=true");
                }
                else
                {
                    MostrarMensaje("Error", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.VentaRapidaErrorGuardar));
                }
            }
            else
            {
                MostrarMensaje(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Validacion), MensajeError);
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmCrearEditarConsultarVentaRapida.aspx");
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
            dgVentaRapida.DataSource = oDBiz.ObtenerVentaRapidaBusqueda(oUsuarioI.idEmpresa, long.Parse(hddIdArticuloBusqueda.Value), txtNombreBusqueda.Text);
            dgVentaRapida.DataBind();
            ShowTab("aBuscarVentaRapida", "divBuscarVentaRapida");
        }

        protected void btnCancelarBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmCrearEditarConsultarVentaRapida.aspx");
        }

        protected void dgVentaRapida_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Response.Redirect(string.Format("frmCrearEditarConsultarVentaRapida.aspx?idVentaRapida={0}", e.Item.Cells[dgVentaRapidaColumnEnum.idVentaRapida.GetHashCode()].Text));
        }
    }
}