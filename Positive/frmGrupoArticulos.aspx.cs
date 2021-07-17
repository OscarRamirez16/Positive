using HQSFramework.Base;
using InventarioBusiness;
using InventarioItem;
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventario
{
    public partial class frmGrupoArticulos : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarConsultarLineas.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
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
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagiona. {0}", ex.Message));
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CargarGrupos();
        }

        private void CargarGrupos()
        {
            try
            {
                tblLineaBussines oLinB = new tblLineaBussines(CadenaConexion);
                gvDatos.DataSource = oLinB.ObtenerLineaPorDescipcion(txtBusqueda.Text.Trim(), oUsuarioI.idEmpresa);
                gvDatos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void gvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDatos.PageIndex = e.NewPageIndex;
            CargarGrupos();
        }

        protected void gvDatos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                long IdGrupo = long.Parse((gvDatos.DataKeys[e.NewSelectedIndex].Value).ToString());
                CargarDatos(IdGrupo);
                mpeEdit.Show();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
            }
        }

        private void CargarDatos(long IdGrupo)
        {
            try
            {
                lblID.Text = IdGrupo.ToString();
                tblLineaBussines oLinB = new tblLineaBussines(CadenaConexion);
                tblLineaItem oLinI = new tblLineaItem();
                oLinI = oLinB.ObtenerLinea(IdGrupo, oUsuarioI.idEmpresa);
                txtDescripcion.Text = oLinI.Nombre;
                chkActivo.Checked = oLinI.Activo;
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
                if (!string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    tblLineaBussines oLinB = new tblLineaBussines(CadenaConexion);
                    tblLineaItem oLinI = new tblLineaItem();
                    CargarDatosGrupo(oLinI);
                    if (oRolPagI.Insertar && oLinI.IdLinea == 0)
                    {
                        if (oLinB.Guardar(oLinI))
                        {
                            mpeEdit.Hide();
                            LimpiarControles();
                            CargarGrupos();
                        }
                        else
                        {
                            MostrarMensaje("Error", "No se pudo crearla bodega.");
                        }
                    }
                    else
                    {
                        if (oRolPagI.Actualizar && oLinI.IdLinea > 0)
                        {
                            if (oLinB.Guardar(oLinI))
                            {
                                mpeEdit.Hide();
                                LimpiarControles();
                                CargarGrupos();
                            }
                            else
                            {
                                MostrarMensaje("Error", "No se pudo actualizar la bodega.");
                            }
                        }
                        else
                        {
                            MostrarMensaje("Error", "El usuario no posee permisos para está operación.");
                        }
                    }
                }
                else
                {
                    MostrarMensaje("Error", "Por favor ingrese un nombre valido a la bodega.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
            }
        }

        protected void btnCerrar_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            mpeEdit.Hide();
        }

        private void CargarDatosGrupo(tblLineaItem Item)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    Item.IdLinea = long.Parse(lblID.Text);
                }
                Item.Nombre = txtDescripcion.Text;
                Item.Activo = chkActivo.Checked;
                Item.IdEmpresa = oUsuarioI.idEmpresa;
            }
            catch
            {
                Response.Write("<script>alert('Error al cargar los datos de la bodega');</script>");
            }
        }

        protected void btnCrear_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            mpeEdit.Show();
        }

        private void LimpiarControles()
        {
            try
            {
                txtDescripcion.Text = "";
                lblID.Text = "";
                chkActivo.Checked = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}