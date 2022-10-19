using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioItem;
using InventarioBusiness;
using System.Configuration;
using HQSFramework.Base;
using System;
using System.Globalization;

namespace Inventario
{
    public partial class frmArticulos : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum gvBodegasEnum
        {
            IdBodega = 0,
            Descripcion = 1,
            Cantidad = 2,
            Costo = 3,
            Precio = 4
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarConsultarArticulos.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        if (!IsPostBack)
                        {
                            CargarGruposArticulos();
                            CargarBodegas();
                            CargarTerceros();
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} ConfigurarEnter();", strScript);
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
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }

        private void CargarGruposArticulos()
        {
            try
            {
                tblLineaBussines oLineaB = new tblLineaBussines(CadenaConexion);
                ddlGrupo.DataSource = oLineaB.ObtenerLineaLista(oUsuarioI.idEmpresa);
                ddlGrupo.DataBind();
                ddlGrupo.Items.Add(new ListItem("Seleccione Grupo", "0"));
                ddlGrupo.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarBodegas()
        {
            try
            {
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                ddlBodega.DataSource = oBodB.ObtenerBodegaLista(oUsuarioI.idEmpresa);
                ddlBodega.DataBind();
                ddlBodega.Items.Add(new ListItem("Seleccione Bodega", "0"));
                ddlBodega.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarControles()
        {
            try
            {
                lblErrores.Text = string.Empty;
                txtCodigo.Text = "";
                lblID.Text = "";
                txtNombre.Text = "";
                txtPresentacion.Text = "";
                chkActivo.Checked = false;
                ddlGrupo.SelectedValue = "0";
                txtIVAVenta.Text = "";
                txtCodigoBarra.Text = "";
                ddlTercero.SelectedValue = "0";
                ddlBodega.SelectedValue = "0";
                chkEsInventario.Checked = false;
                txtStockMinimo.Text = "";
                txtIVACompra.Text = "";
                txtUbicacion.Text = "";
                txtCostoPonderado.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CargarArticulos();
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }

        private void CargarArticulos()
        {
            try
            {
                tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                gvDatos.DataSource = oArtB.ObtenerArticuloListaPorNombreCodigo(txtBusqueda.Text.Trim(), oUsuarioI.idEmpresa);
                gvDatos.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void CargarTerceros()
        {
            try
            {
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                ddlTercero.DataSource = oTerB.ObtenerTerceroListaPorTipo(oUsuarioI.idEmpresa, "P");
                ddlTercero.DataBind();
                ddlTercero.Items.Add(new ListItem("Seleccione Proveedor", "0"));
                ddlTercero.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvBodegas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDatos.PageIndex = e.NewPageIndex;
            CargarArticulos();
        }

        private void CargarDatos(long IdArticulo)
        {
            try
            {
                lblID.Text = IdArticulo.ToString();
                tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                tblArticuloItem oArtI = new tblArticuloItem();
                oArtI = oArtB.ObtenerArticuloPorID(IdArticulo, oUsuarioI.idEmpresa);
                txtNombre.Text = oArtI.Nombre;
                txtCodigo.Text = oArtI.CodigoArticulo;
                txtPresentacion.Text = oArtI.Presentacion;
                ddlGrupo.SelectedValue = oArtI.IdLinea.ToString();
                txtIVAVenta.Text = oArtI.IVAVenta.ToString(Util.ObtenerFormatoEntero());
                txtIVACompra.Text = oArtI.IVACompra.ToString(Util.ObtenerFormatoEntero());
                txtCodigoBarra.Text = oArtI.CodigoBarra;
                ddlTercero.SelectedValue = oArtI.IdTercero.ToString();
                ddlBodega.SelectedValue = oArtI.IdBodega.ToString();
                chkEsInventario.Checked = oArtI.EsInventario;
                txtStockMinimo.Text = oArtI.StockMinimo.ToString(Util.ObtenerFormatoDecimal());
                txtUbicacion.Text = oArtI.Ubicacion;
                txtCostoPonderado.Text = oArtI.CostoPonderado.ToString(Util.ObtenerFormatoDecimal());
                chkActivo.Checked = oArtI.Activo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ValidarGuardar()
        {
            string Errores = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    Errores = string.Format("{0} * Debe digitar un código valido.<br>", Errores);
                }
                if(ddlGrupo.SelectedValue == "0")
                {
                    Errores = string.Format("{0} * Debe seleccionar un grupo valido.<br>", Errores);
                }
                if (ddlTercero.SelectedValue == "0")
                {
                    Errores = string.Format("{0} * Debe seleccionar un proveedor valido.<br>", Errores);
                }
                if (ddlBodega.SelectedValue == "0")
                {
                    Errores = string.Format("{0} * Debe seleccionar una bodega valida.", Errores);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Errores;
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblErrores.Text = string.Empty;
                string Errores = string.Empty;
                Errores = ValidarGuardar();
                if (string.IsNullOrEmpty(Errores))
                {
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                    tblArticuloItem oArtI = new tblArticuloItem();
                    CargarDatosArticulo(ref oArtI);
                    if (oRolPagI.Insertar && oArtI.IdArticulo == 0)
                    {
                        if (oArtB.insertar(oArtI))
                        {
                            mpeEdit.Hide();
                            LimpiarControles();
                            CargarArticulos();
                        }
                        else
                        {
                            MostrarAlerta(0,"Error", "El Articulo no se pudo crear");
                        }
                    }
                    else
                    {
                        if (oRolPagI.Actualizar && oArtI.IdArticulo > 0)
                        {
                            if (oArtB.insertar(oArtI))
                            {
                                mpeEdit.Hide();
                                LimpiarControles();
                                CargarArticulos();
                            }
                            else
                            {
                                MostrarAlerta(0, "Error", "El Articulo no se pudo actualizar");
                            }
                        }
                        else
                        {
                            MostrarAlerta(0, "Error", "El usuario no posee permisos para esta operación.");
                        }
                    }
                }
                else
                {
                    lblErrores.Text = Errores;
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }

        private void CargarDatosArticulo(ref tblArticuloItem Item)
        {
            try
            {
                if (lblID.Text != "")
                {
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                    Item = oArtB.ObtenerArticuloPorID(long.Parse(lblID.Text), oUsuarioI.idEmpresa);
                }
                else
                {
                    Item.IdEmpresa = oUsuarioI.idEmpresa;
                }
                Item.IdTercero = long.Parse(ddlTercero.SelectedValue);
                Item.CodigoArticulo = txtCodigo.Text;
                Item.Nombre = txtNombre.Text;
                Item.Presentacion = txtPresentacion.Text;
                Item.IdLinea = long.Parse(ddlGrupo.SelectedValue);
                if (txtIVACompra.Text != "")
                {
                    Item.IVACompra = decimal.Parse(txtIVACompra.Text, NumberStyles.Currency);
                }
                if (txtIVAVenta.Text != "")
                {
                    Item.IVAVenta = decimal.Parse(txtIVAVenta.Text, NumberStyles.Currency);
                }
                Item.CodigoBarra = txtCodigoBarra.Text;
                Item.IdBodega = long.Parse(ddlBodega.SelectedValue);
                if (txtStockMinimo.Text != "")
                {
                    Item.StockMinimo = decimal.Parse(txtStockMinimo.Text, NumberStyles.Currency);
                }
                Item.Activo = chkActivo.Checked;
                Item.Ubicacion = txtUbicacion.Text;
                if (txtCostoPonderado.Text != "")
                {
                    Item.CostoPonderado = decimal.Parse(txtCostoPonderado.Text, NumberStyles.Currency);
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }

        protected void btnCrear_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            mpeEdit.Show();
        }

        protected void btnCerrar_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            mpeEdit.Hide();
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void gvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Editar")
                {
                    long IdArticulo = long.Parse((gvDatos.DataKeys[int.Parse(e.CommandArgument.ToString())].Value).ToString());
                    CargarDatos(IdArticulo);
                    mpeEdit.Show();
                }
                if (e.CommandName == "Bodegas")
                {
                    long IdArticulo = long.Parse((gvDatos.DataKeys[int.Parse(e.CommandArgument.ToString())].Value).ToString());
                    hddIdArticulo.Value = IdArticulo.ToString();
                    CargarConfiguracionBodegas(IdArticulo);
                    mpeBodega.Show();
                }
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }

        private void CargarConfiguracionBodegas(long IdArticulo)
        {
            try
            {
                lblErroresBodegas.Text = string.Empty;
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                gvBodegas.DataSource = oBodB.ObtenerConfiguracionBodegasPorArticulo(IdArticulo, oUsuarioI.idEmpresa);
                gvBodegas.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelarBodega_Click(object sender, ImageClickEventArgs e)
        {
            lblErroresBodegas.Text = string.Empty;
            mpeBodega.Hide();
        }

        protected void gvBodegas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = gvBodegas.Rows[e.RowIndex];
                tblArticulo_BodegaItem oArtBodI = new tblArticulo_BodegaItem();
                oArtBodI.IdArticulo = long.Parse(hddIdArticulo.Value);
                oArtBodI.IdBodega = long.Parse(row.Cells[gvBodegasEnum.IdBodega.GetHashCode()].Text);
                oArtBodI.Cantidad = decimal.Parse(row.Cells[gvBodegasEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                oArtBodI.Costo = decimal.Parse(((TextBox)(row.Cells[gvBodegasEnum.Costo.GetHashCode()].Controls[0])).Text, NumberStyles.Currency);
                oArtBodI.Precio = decimal.Parse(((TextBox)(row.Cells[gvBodegasEnum.Precio.GetHashCode()].Controls[0])).Text, NumberStyles.Currency);
                oArtBodI.IdTipoManejoPrecio = 1;
                if (oArtBodI.Costo > 0 && oArtBodI.Precio > 0)
                {
                    tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                    string Error = oBodB.GuardarArticuloPorBodega(oArtBodI);
                    if (!string.IsNullOrEmpty(Error))
                    {
                        lblErroresBodegas.Text = Error;
                    }
                    else
                    {
                        gvBodegas.EditIndex = -1;
                        CargarConfiguracionBodegas(long.Parse(hddIdArticulo.Value));
                    }
                }
                else
                {
                    lblErroresBodegas.Text = "El costo y el precio por bodega deben ser mayor a cero (0).";
                }
            }
            catch(Exception ex)
            {
                lblErroresBodegas.Text = ex.Message;
            }
        }

        protected void gvBodegas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBodegas.EditIndex = e.NewEditIndex;
            CargarConfiguracionBodegas(long.Parse(hddIdArticulo.Value));
        }

        protected void gvBodegas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBodegas.EditIndex = -1;
            CargarConfiguracionBodegas(long.Parse(hddIdArticulo.Value));
        }

        protected void gvBodegas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvBodegas.Rows[e.RowIndex];
                tblArticulo_BodegaItem oArtBodI = new tblArticulo_BodegaItem();
                oArtBodI.IdArticulo = long.Parse(hddIdArticulo.Value);
                oArtBodI.IdBodega = long.Parse(row.Cells[gvBodegasEnum.IdBodega.GetHashCode()].Text);
                oArtBodI.Cantidad = decimal.Parse(row.Cells[gvBodegasEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                if(oArtBodI.Cantidad == 0)
                {
                    tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                    string Error = oBodB.EliminarArticuloBodega(oArtBodI);
                    if (!string.IsNullOrEmpty(Error))
                    {
                        lblErroresBodegas.Text = Error;
                    }
                    else
                    {
                        CargarConfiguracionBodegas(long.Parse(hddIdArticulo.Value));
                    }
                }
                else
                {
                    lblErroresBodegas.Text = "No se puede eliminar un artículo de una bodega si la cantidad no es igual a cero (0).";
                }
            }
            catch(Exception ex)
            {
                lblErroresBodegas.Text = ex.Message;
            }
        }
    }
}