using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;

namespace Inventario
{
    public partial class frmCodigoBarra : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgCodigoEnum
        {
            IdCodigo = 0,
            CodigoBarra = 1,
            Descripcion = 2,
            Activo = 3,
            IdArticulo = 4,
            Editar = 5
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarConsultarArticulos.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} ConfigurarEnter();", strScript);
                        }
                        ConfiguracionIdioma();
                        tblArticuloItem oArtI = new tblArticuloItem();
                        tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                        oArtI = oArtB.ObtenerArticuloPorID(long.Parse(Request.QueryString["IdArticulo"]), oUsuarioI.idEmpresa);
                        if (oArtI.IdArticulo > 0)
                        {
                            lblArticulo.Text = string.Format("{0} - {1}", oArtI.IdArticulo, oArtI.Nombre);
                            CargarCodigos(oArtI.IdArticulo, oArtB);
                        }
                        else
                        {
                            MostrarMensaje("Error", "El artículo no se encuentra en el sistema.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagina. {0}", ex.Message));
            }
        }

        private void CargarCodigos(long IdArticulo, tblArticuloBusiness oArtB)
        {
            try
            {
                dgCodigos.DataSource = oArtB.ObtenerCodigosBarra(IdArticulo);
                dgCodigos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CodigoBarra));
            lblTituloGrilla.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Asignacion), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CodigoBarra));
            txtCodigo.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CodigoBarra));
            txtDescripcion.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Descripcion));
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgCodigos.Columns[dgCodigoEnum.CodigoBarra.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CodigoBarra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmCrearEditarConsultarArticulos.aspx");
        }

        private void LimpiarControles()
        {
            try
            {
                hddIdCodigo.Value = "0";
                txtCodigo.Text = "";
                txtDescripcion.Text = "";
                chkActivo.Checked = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                tblArticuloCodigoBarraItem oCodI = new tblArticuloCodigoBarraItem();
                oCodI.CodigoBarra = txtCodigo.Text.Trim();
                oCodI.Descripcion = txtDescripcion.Text.Trim();
                oCodI.Activo = chkActivo.Checked;
                oCodI.IdArticulo = long.Parse(Request.QueryString["IdArticulo"]);
                oCodI.IdCodigo = long.Parse(hddIdCodigo.Value);
                oCodI.IdEmpresa = oUsuarioI.idEmpresa;
                if(oArtB.GuardarCodigosBarra(oCodI))
                {
                    MostrarMensaje("Exito", "El código de barra se registro con exito.");
                    CargarCodigos(oCodI.IdArticulo, oArtB);
                    LimpiarControles();
                }
                else
                {
                    MostrarMensaje("Error", "No se pudo guardar el registro.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo guardar el registro. {0}", ex.Message));
            }
        }

        protected void dgCodigos_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Editar")
                {
                    hddIdCodigo.Value = e.Item.Cells[dgCodigoEnum.IdCodigo.GetHashCode()].Text;
                    txtCodigo.Text = e.Item.Cells[dgCodigoEnum.CodigoBarra.GetHashCode()].Text;
                    txtDescripcion.Text = e.Item.Cells[dgCodigoEnum.Descripcion.GetHashCode()].Text;
                    if (e.Item.Cells[dgCodigoEnum.Activo.GetHashCode()].Text == "True")
                    {
                        chkActivo.Checked = true;
                    }
                    else
                    {
                        chkActivo.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se puede editar el registro. {0}", ex.Message));
            }
        }
    }
}