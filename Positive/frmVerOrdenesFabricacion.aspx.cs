using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioItem;
using InventarioBusiness;
using HQSFramework.Base;
using System.Configuration;
using Idioma;

namespace Inventario
{
    public partial class frmVerOrdenesFabricacion : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgOrdenesFabricacionEnum
        {
            IdOrdenFabricacion = 0,
            IdListaMateriales = 1,
            FechaCreacion = 2,
            IdUsuario = 3,
            Usuario = 4,
            IdEstado = 5,
            Estado = 6,
            CambiarEstado = 7,
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
                            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
                            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
                            CargarOrdenesFabricacion();
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
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

        private void CargarOrdenesFabricacion()
        {
            try
            {
                tblOrdenFabricacionBiz oOrdB = new tblOrdenFabricacionBiz(CadenaConexion);
                dgOrdenes.DataSource = oOrdB.ObtenerOrdenesFabricacionPorFiltros(DateTime.Parse(txtFechaInicial.Text), DateTime.Parse(txtFechaFinal.Text), txtIdOrden.Text, long.Parse(ddlUsuario.SelectedValue), oUsuarioI.idEmpresa);
                dgOrdenes.DataBind();
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
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.OrdenFabricacion);
            lblFechaInical.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaInicial);
            lblFechaFinal.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaFinal);
            lblUsuario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
            CargarUsuarios(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgOrdenes.Columns[dgOrdenesFabricacionEnum.FechaCreacion.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Fecha);
                dgOrdenes.Columns[dgOrdenesFabricacionEnum.Usuario.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
                dgOrdenes.Columns[dgOrdenesFabricacionEnum.Estado.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Estado);
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

        protected void dgOrdenes_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Editar")
                {
                    Response.Redirect(string.Format("frmOrdenFabricacion.aspx?IdOrden={0}", e.Item.Cells[dgOrdenesFabricacionEnum.IdOrdenFabricacion.GetHashCode()].Text));
                }
                if(e.CommandName == "CambiarEstado")
                {
                    if(int.Parse(e.Item.Cells[dgOrdenesFabricacionEnum.IdEstado.GetHashCode()].Text) < int.Parse(((DropDownList)(e.Item.Cells[dgOrdenesFabricacionEnum.CambiarEstado.GetHashCode()].FindControl("ddlEstado"))).SelectedValue))
                    {
                        tblOrdenFabricacionBiz oOrdB = new tblOrdenFabricacionBiz(CadenaConexion);
                        string Error = oOrdB.CambiarEstadoOF(long.Parse(e.Item.Cells[dgOrdenesFabricacionEnum.IdOrdenFabricacion.GetHashCode()].Text), short.Parse(((DropDownList)(e.Item.Cells[dgOrdenesFabricacionEnum.CambiarEstado.GetHashCode()].FindControl("ddlEstado"))).SelectedValue));
                        if (string.IsNullOrEmpty(Error))
                        {
                            MostrarMensaje("Exito","El estado de la orden de fabricación se actualizó con exito.");
                        }
                        else
                        {
                            MostrarMensaje("Error", Error);
                        }
                    }
                    else
                    {
                        MostrarMensaje("Error", "No se puede cambiar a un estado menor.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo editar la orden de fabricación. {0}", ex.Message));
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CargarOrdenesFabricacion();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo realizar la busqueda de las ordenes de fabricación. {0}", ex.Message));
            }
        }

        protected void dgOrdenes_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                tblOrdenFabricacionBiz oOrdB = new tblOrdenFabricacionBiz(CadenaConexion);
                ((DropDownList)(e.Item.Cells[dgOrdenesFabricacionEnum.CambiarEstado.GetHashCode()].FindControl("ddlEstado"))).DataSource = oOrdB.ObtenerEstadosOrdenFabricacion();
                ((DropDownList)(e.Item.Cells[dgOrdenesFabricacionEnum.CambiarEstado.GetHashCode()].FindControl("ddlEstado"))).DataBind();
                ((DropDownList)(e.Item.Cells[dgOrdenesFabricacionEnum.CambiarEstado.GetHashCode()].FindControl("ddlEstado"))).SelectedValue = e.Item.Cells[dgOrdenesFabricacionEnum.IdEstado.GetHashCode()].Text;
            }
        }
    }
}