using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioItem;
using InventarioBusiness;
using System.Configuration;
using HQSFramework.Base;

namespace Inventario
{
    public partial class frmBodega : PaginaBase
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarConsultarBodega.GetHashCode().ToString()));
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
            CargarBodegas();
        }

        private void CargarBodegas()
        {
            try
            {
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                gvDatos.DataSource = oBodB.ObtenerBodegaPorDescripcionEmpresa(txtBusqueda.Text.Trim(), oUsuarioI.idEmpresa);
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

        protected void gvBodegas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDatos.PageIndex = e.NewPageIndex;
            CargarBodegas();
        }

        protected void gvBodegas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                long IdBodega = long.Parse((gvDatos.DataKeys[e.NewSelectedIndex].Value).ToString());
                CargarDatos(IdBodega);
                mpeEdit.Show();
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
            }
        }

        private void CargarDatos(long IdBodega)
        {
            try
            {
                lblID.Text = IdBodega.ToString();
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                tblBodegaItem oBodI = new tblBodegaItem();
                oBodI = oBodB.ObtenerBodega(IdBodega, oUsuarioI.idEmpresa);
                txtDescripcion.Text = oBodI.Descripcion;
                txtDireccion.Text = oBodI.Direccion;
                chkActivo.Checked = oBodI.Activo;
            }
            catch(Exception ex)
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
                    tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                    tblBodegaItem oBodI = new tblBodegaItem();
                    CargarDatosBodega(oBodI);
                    if (oRolPagI.Insertar && oBodI.IdBodega == 0)
                    {
                        if (oBodB.Guardar(oBodI))
                        {
                            mpeEdit.Hide();
                            LimpiarControles();
                            CargarBodegas();
                        }
                        else
                        {
                            MostrarMensaje("Error", "No se pudo crearla bodega.");
                        }
                    }
                    else
                    {
                        if (oRolPagI.Actualizar && oBodI.IdBodega > 0)
                        {
                            if (oBodB.Guardar(oBodI))
                            {
                                mpeEdit.Hide();
                                LimpiarControles();
                                CargarBodegas();
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

        private void CargarDatosBodega(tblBodegaItem bodega)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblID.Text))
                {
                    bodega.IdBodega = long.Parse(lblID.Text);
                }
                bodega.Descripcion = txtDescripcion.Text;
                bodega.Direccion = txtDireccion.Text;
                bodega.Activo = chkActivo.Checked;
                bodega.idEmpresa = oUsuarioI.idEmpresa;
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
                txtDireccion.Text = "";
                chkActivo.Checked = false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}