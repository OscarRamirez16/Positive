using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;
using System.Globalization;
using System.Data;

namespace Inventario
{
    public partial class frmVerListaMateriales : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgListaMaterialesEnum
        {
            IdListaMateriales = 0,
            IdArticulo = 1,
            Articulo = 2,
            Fecha = 3,
            IdUsuario = 4,
            Usuario = 5,
            IdEmpresa = 6,
            Activo = 7,
            Editar = 8
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VerListaMateriales.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}','{3}','3');", strScript, txtArticulo.ClientID, hddIdArticulo.ClientID, oUsuarioI.idEmpresa);
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
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ListaMateriales);
            lblArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
            lblUsuario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
            CargarUsuarios(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgListaMateriales.Columns[dgListaMaterialesEnum.Articulo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
                dgListaMateriales.Columns[dgListaMaterialesEnum.Fecha.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Fecha);
                dgListaMateriales.Columns[dgListaMaterialesEnum.Usuario.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
                dgListaMateriales.Columns[dgListaMaterialesEnum.Activo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarUsuarios(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblUsuarioBusiness oUsuB = new tblUsuarioBusiness(CadenaConexion);
                string Opcion = ddlUsuario.SelectedValue;
                ddlUsuario.Items.Clear();
                ddlUsuario.SelectedValue = null;
                ddlUsuario.DataSource = oUsuB.ObtenerUsuarioListaPorIdEmpresa(oUsuarioI.idEmpresa);
                ddlUsuario.DataBind();
                ddlUsuario.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario)), "0"));
                if (!IsPostBack)
                {
                    ddlUsuario.SelectedValue = "0";
                }
                else
                {
                    ddlUsuario.SelectedValue = Opcion;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmProduccion.aspx");
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblListaMaterialesBusiness oLisMatB = new tblListaMaterialesBusiness(CadenaConexion);
                dgListaMateriales.DataSource = oLisMatB.ObtenerListaMaterialesPorFiltros(long.Parse(hddIdArticulo.Value), long.Parse(ddlUsuario.SelectedValue), oUsuarioI.idEmpresa);
                dgListaMateriales.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo realizar la busqueda. {0}", ex.Message));
            }
        }

        protected void dgListaMateriales_EditCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("frmListaMateriales.aspx?IdLista={0}", e.Item.Cells[dgListaMaterialesEnum.IdListaMateriales.GetHashCode()].Text));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se puede editar el registro. {0}", ex.Message));
            }
        }
    }
}