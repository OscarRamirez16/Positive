using HQSFramework.Base;
using InventarioBusiness;
using InventarioItem;
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventario
{
    public partial class frmCrearTercero : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarTerceros.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        if (!IsPostBack)
                        {
                            CargarTipoIdentificacion();
                            CargarTipoTercero();
                            CargarGrupoCliente();
                            CargarListaPrecio();
                            CargarCiudades();
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
                MostrarMensaje("Error", string.Format("Error al cargar la pagina de terceros. {0}", ex.Message));
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblTerceroItem oTerI = new tblTerceroItem();
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                oTerI.idTipoIdentificacion = short.Parse(ddlTipoID.SelectedValue);
                oTerI.Identificacion = txtIdentificacion.Text;
                oTerI.Nombre = txtNombre.Text;
                oTerI.Mail = txtCorreo.Text;
                oTerI.Telefono = txtTelefono.Text;
                oTerI.Celular = txtCelular.Text;
                oTerI.Direccion = txtDireccion.Text;
                oTerI.idCiudad = short.Parse(ddlCiudad.SelectedValue);
                oTerI.idEmpresa = oUsuarioI.idEmpresa;
                oTerI.TipoTercero = ddlTipoTercero.SelectedValue;
                if (ddlTipoTercero.SelectedItem.Text == "Cliente")
                {
                    if (txtFechaNac.Text != "")
                    {
                        oTerI.FechaNacimiento = DateTime.Parse(txtFechaNac.Text);
                    }
                    if (ddlListaPrecio.SelectedValue != "0")
                    {
                        oTerI.IdListaPrecio = long.Parse(ddlListaPrecio.SelectedValue);
                    }
                }
                oTerI.idGrupoCliente = long.Parse(ddlGrupo.SelectedValue);
                if (!string.IsNullOrEmpty(txtObservaciones.Text))
                {
                    oTerI.Observaciones = txtObservaciones.Text;
                }
                oTerI.Activo = true;
                string Error = oTerB.insertar(oTerI);
                if (string.IsNullOrEmpty(Error))
                {
                    Session.Remove("DatosTercero");
                    Session["DatosTercero"] = oTerI;
                    Response.Write("<script>opener.EjecutarCrearTercero();window.close();</script>");
                }
                else
                {
                    MostrarAlerta(0, "Error", Error);
                }
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message);
            }
        }

        public void CargarTipoIdentificacion()
        {
            try
            {
                ddlTipoID.Items.Add(new ListItem("NIT", tblTerceroBusiness.TipoIdentificacion.Nit.GetHashCode().ToString()));
                ddlTipoID.Items.Add(new ListItem("Cedula", tblTerceroBusiness.TipoIdentificacion.Cedula.GetHashCode().ToString()));
                ddlTipoID.DataBind();
                ddlTipoID.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarTipoTercero()
        {
            try
            {
                ddlTipoTercero.Items.Add(new ListItem("Cliente", "C"));
                ddlTipoTercero.Items.Add(new ListItem("Proveedor", "P"));
                ddlTipoTercero.DataBind();
                ddlTipoTercero.SelectedValue = "C";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCiudades()
        {
            try
            {
                tblCiudadBusiness oCiudB = new tblCiudadBusiness(CadenaConexion);
                ddlCiudad.DataTextField = "Nombre";
                ddlCiudad.DataValueField = "idCiudad";
                ddlCiudad.DataSource = oCiudB.ObtenerCiudadLista();
                ddlCiudad.DataBind();
                ddlCiudad.SelectedValue = oUsuarioI.idCiudad.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarGrupoCliente()
        {
            try
            {
                tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                ddlGrupo.DataTextField = "Nombre";
                ddlGrupo.DataValueField = "idGrupoCliente";
                ddlGrupo.DataSource = oTBiz.ObtenerGrupoClienteLista(oUsuarioI.idEmpresa, "");
                ddlGrupo.DataBind();
                ddlGrupo.Items.Add(new ListItem("No aplica", "0"));
                ddlGrupo.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarListaPrecio()
        {
            try
            {
                tblListaPrecioBusiness oListB = new tblListaPrecioBusiness(CadenaConexion);
                ddlListaPrecio.DataTextField = "Nombre";
                ddlListaPrecio.DataValueField = "IdListaPrecio";
                ddlListaPrecio.DataSource = oListB.ObtenerListaPrecioLista(oUsuarioI.idEmpresa);
                ddlListaPrecio.DataBind();
                ddlListaPrecio.Items.Add(new ListItem("No aplica", "0"));
                ddlListaPrecio.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}