using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using HQSFramework.Base;
using InventarioItem;
using InventarioBusiness;
using System.Configuration;
using Idioma;
using System.Data;

namespace Inventario
{
    public partial class frmOrdenFabricacion : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgMaterialesEnum
        {
            IdArticulo = 0,
            Articulo = 1,
            Cantidad = 2,
            IdBodega = 3,
            Eliminar = 4
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.OrdenFabricacion.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {
                    ConfiguracionIdioma();
                    if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["IdOrden"]))
                    {
                        tblOrdenFabricacionBiz oOrdB = new tblOrdenFabricacionBiz(CadenaConexion);
                        tblOrdenFabricacionItem oOrdI = new tblOrdenFabricacionItem();
                        oOrdI = oOrdB.ObtenerOrdenFabricacion(long.Parse(Request.QueryString["IdOrden"]));
                        if (oOrdI.IdOrdenFabricacion > 0)
                        {
                            hddIdOrden.Value = oOrdI.IdOrdenFabricacion.ToString();
                            hddIdListaMateriales.Value = oOrdI.IdListaMateriales;
                            txtArticulo.Text = oOrdI.ListaMateriales;
                            txtArticulo.Enabled = false;
                            txtCantidadOrden.Text = oOrdI.Cantidad.ToString();
                            ddlEstado.SelectedValue = oOrdI.IdEstado.ToString();
                            tdBuscar.Visible = false;
                            CargarArticulosOrdenFabricacion(ref oOrdB);
                        }
                    }
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0} EstablecerAutoCompleteListaMateriales('{1}','Ashx/ListaMateriales.ashx','{2}','{3}','3');", strScript, txtArticulo.ClientID, hddIdListaMateriales.ClientID, oUsuarioI.idEmpresa);
                        strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}','{3}','3');", strScript, txtArticuloMat.ClientID, hddIdArticuloMat.ClientID, oUsuarioI.idEmpresa);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                    }
                }
            }
            else
            {
                Response.Redirect("frmInicioSesion.aspx");
            }
        }

        private void CargarArticulosOrdenFabricacion(ref tblOrdenFabricacionBiz oOrdB)
        {
            try
            {
                dgMateriales.DataSource = oOrdB.ObtenerOrdenFabricacionDetallePorID(long.Parse(Request.QueryString["IdOrden"]));
                dgMateriales.DataBind();
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
            lblArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
            lblCantidadOrden.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad);
            lblEstado.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Estado);
            lblCantidad.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad);
            lblArticuloMat.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.MateriaPrima);
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
            CargarEstados(oCIdioma, Idioma);
        }

        private void CargarEstados(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblOrdenFabricacionBiz oOrdB = new tblOrdenFabricacionBiz(CadenaConexion);
                string Opcion = ddlEstado.SelectedValue;
                ddlEstado.Items.Clear();
                ddlEstado.SelectedValue = null;
                ddlEstado.DataSource = oOrdB.ObtenerEstadosOrdenFabricacion();
                ddlEstado.DataBind();
                ddlEstado.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Estado)), "0"));
                if (!IsPostBack)
                {
                    ddlEstado.SelectedValue = "0";
                }
                else
                {
                    ddlEstado.SelectedValue = Opcion;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgMateriales.Columns[dgMaterialesEnum.Articulo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
                dgMateriales.Columns[dgMaterialesEnum.Cantidad.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad);
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
                tblListaMaterialesBusiness oListB = new tblListaMaterialesBusiness(CadenaConexion);
                dgMateriales.DataSource = oListB.ObtenerListaMaterialesDetallesPorID(hddIdListaMateriales.Value);
                dgMateriales.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar la lista de materiales. {0}", ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmProduccion.aspx");
        }

        private void CargarColumnasMateriales(ref DataTable dt)
        {
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IdArticulo";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Articulo";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Cantidad";
            dt.Columns.Add(column);
        }

        protected void dgMateriales_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    DataTable dt = new DataTable();
                    CargarColumnasMateriales(ref dt);
                    foreach (DataGridItem Item in dgMateriales.Items)
                    {
                        if (Item.ItemIndex != e.Item.ItemIndex)
                        {
                            DataRow row;
                            row = dt.NewRow();
                            row["IdArticulo"] = Item.Cells[dgMaterialesEnum.IdArticulo.GetHashCode()].Text;
                            row["Articulo"] = Item.Cells[dgMaterialesEnum.Articulo.GetHashCode()].Text;
                            row["Cantidad"] = Item.Cells[dgMaterialesEnum.Cantidad.GetHashCode()].Text;
                            dt.Rows.Add(row);
                        }
                    }
                    dgMateriales.DataSource = dt;
                    dgMateriales.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo eliminar la metaria prima. {0}", ex.Message));
            }
        }

        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtArticuloMat.Text) && !string.IsNullOrEmpty(txtCantidad.Text))
                {
                    DataTable dt = new DataTable();
                    bool Existe = false;
                    if (dgMateriales.Items.Count > 0)
                    {
                        CargarColumnasMateriales(ref dt);
                        foreach (DataGridItem Item in dgMateriales.Items)
                        {
                            if (Item.Cells[dgMaterialesEnum.IdArticulo.GetHashCode()].Text == hddIdArticuloMat.Value)
                            {
                                Existe = true;
                            }
                            DataRow copia;
                            copia = dt.NewRow();
                            copia["IdArticulo"] = Item.Cells[dgMaterialesEnum.IdArticulo.GetHashCode()].Text;
                            copia["Articulo"] = Item.Cells[dgMaterialesEnum.Articulo.GetHashCode()].Text;
                            copia["Cantidad"] = Item.Cells[dgMaterialesEnum.Cantidad.GetHashCode()].Text;
                            dt.Rows.Add(copia);
                        }
                    }
                    else
                    {
                        CargarColumnasMateriales(ref dt);
                    }
                    if (!Existe)
                    {
                        DataRow row;
                        row = dt.NewRow();
                        row["IdArticulo"] = hddIdArticuloMat.Value;
                        row["Articulo"] = txtArticuloMat.Text;
                        row["Cantidad"] = txtCantidad.Text;
                        dt.Rows.Add(row);
                    }
                    else
                    {
                        MostrarMensaje("Error", "La materia prima ya se encuentra en la lista.");
                    }
                    dgMateriales.DataSource = dt;
                    dgMateriales.DataBind();
                }
                else
                {
                    MostrarMensaje("Error", "Por favor seleccione una materia prima valida o digite una cantidad.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo adicionar la materia prima. {0}", ex.Message));
            }
            hddIdArticuloMat.Value = "0";
            txtArticuloMat.Text = "";
            txtCantidad.Text = "0.00";
        }

        private void CargarDatosGuardar(ref tblOrdenFabricacionItem Item)
        {
            try
            {
                if (!string.IsNullOrEmpty(hddIdOrden.Value))
                {
                    tblOrdenFabricacionBiz oOrdB = new tblOrdenFabricacionBiz(CadenaConexion);
                    Item = oOrdB.ObtenerOrdenFabricacion(long.Parse(hddIdOrden.Value));
                }
                else
                {
                    Item.IdListaMateriales = hddIdListaMateriales.Value;
                    Item.FechaCreacion = DateTime.Now;
                    Item.IdUsuario = oUsuarioI.idUsuario;
                    Item.IdEmpresa = oUsuarioI.idEmpresa;
                    Item.Cantidad = decimal.Parse(txtCantidadOrden.Text, System.Globalization.NumberStyles.Currency);
                }
                Item.IdEstado = int.Parse(ddlEstado.SelectedValue);
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
                tblOrdenFabricacionItem oOrdI = new tblOrdenFabricacionItem();
                tblOrdenFabricacionBiz oOrdB = new tblOrdenFabricacionBiz(CadenaConexion);
                CargarDatosGuardar(ref oOrdI);
                oOrdI.Detalles = new List<tblOrdenFabricacionDetalleItem>();
                foreach (DataGridItem Detalle in dgMateriales.Items)
                {
                    tblOrdenFabricacionDetalleItem oDetI = new tblOrdenFabricacionDetalleItem();
                    oDetI.IdArticulo = long.Parse(Detalle.Cells[dgMaterialesEnum.IdArticulo.GetHashCode()].Text);
                    oDetI.Cantidad = decimal.Parse(Detalle.Cells[dgMaterialesEnum.Cantidad.GetHashCode()].Text, System.Globalization.NumberStyles.Currency);
                    oDetI.IdBodega = long.Parse(((DropDownList)(Detalle.Cells[dgMaterialesEnum.IdBodega.GetHashCode()].FindControl("ddlBodega"))).SelectedValue);
                    oOrdI.Detalles.Add(oDetI);
                }
                if (oOrdB.Guardar(oOrdI))
                {
                    MostrarMensaje("Orden de Fabricación", "La orden de fabricación se guardo con exito.");
                    LimpiarControles();
                }
                else
                {
                    MostrarMensaje("Error", "La orden de fabricación no se pudo guardar.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("La orden de fabricación no se pudo guardar. {0}", ex.Message));
            }
        }

        private void LimpiarControles()
        {
            try
            {
                hddIdListaMateriales.Value = "";
                hddIdArticuloMat.Value = "";
                txtArticulo.Text = "";
                txtCantidadOrden.Text = "";
                txtArticuloMat.Text = "";
                txtCantidad.Text = "";
                dgMateriales.DataSource = null;
                dgMateriales.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo limpiar los controles. {0}", ex.Message));
            }
        }

        protected void dgMateriales_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                ((DropDownList)(e.Item.Cells[dgMaterialesEnum.IdBodega.GetHashCode()].FindControl("ddlBodega"))).DataSource = oBodB.ObtenerListaBodegaArticuloDisponible(long.Parse(e.Item.Cells[dgMaterialesEnum.IdArticulo.GetHashCode()].Text));
                ((DropDownList)(e.Item.Cells[dgMaterialesEnum.IdBodega.GetHashCode()].FindControl("ddlBodega"))).DataBind();
                if (!string.IsNullOrEmpty(hddIdOrden.Value))
                {
                    string IdBodega = oBodB.ObtenerOrdenFabricacionDetallePorID(long.Parse(hddIdOrden.Value), long.Parse(e.Item.Cells[dgMaterialesEnum.IdArticulo.GetHashCode()].Text));
                    if(!string.IsNullOrEmpty(IdBodega) && IdBodega != "0")
                    {
                        ((DropDownList)(e.Item.Cells[dgMaterialesEnum.IdBodega.GetHashCode()].FindControl("ddlBodega"))).SelectedValue = IdBodega;
                    }
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmProduccion.aspx");
        }
    }
}