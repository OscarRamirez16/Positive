using System;
using System.Collections.Generic;
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
    public partial class frmListaMateriales : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgMaterialesEnum
        {
            IdArticulo = 0,
            Articulo = 1,
            Cantidad = 2,
            Eliminar= 3
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
                    if(Request.QueryString["ArticuloCompuesto"] == "1")
                    {
                        oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.ConfigurarArticuloCompuesto.GetHashCode().ToString()));
                    }
                    else
                    {
                        oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.ListaMateriales.GetHashCode().ToString()));
                    }
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            if (string.IsNullOrEmpty(Request.QueryString["IdLista"]))
                            {
                                txtCantidad.Text = "0.00";
                            }
                            else
                            {
                                tblListaMaterialesItem oLisMatI = new tblListaMaterialesItem();
                                tblListaMaterialesBusiness oLisMatB = new tblListaMaterialesBusiness(CadenaConexion);
                                oLisMatI = oLisMatB.ObtenerListaMaterialesPorID(Request.QueryString["IdLista"]);
                                CargarDatosListaMateriales(ref oLisMatI, ref oLisMatB);
                            }
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            if (Request.QueryString["ArticuloCompuesto"] == "1")
                            {
                                strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}','{3}','4');", strScript, txtArticulo.ClientID, hddIdArticulo.ClientID, oUsuarioI.idEmpresa);
                            }
                            else
                            {
                                strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}','{3}','5');", strScript, txtArticulo.ClientID, hddIdArticulo.ClientID, oUsuarioI.idEmpresa);
                            }
                            strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}','{3}','3');", strScript, txtArticuloMat.ClientID, hddIdArticuloMat.ClientID, oUsuarioI.idEmpresa);
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

        private void CargarDatosListaMateriales(ref tblListaMaterialesItem Item, ref tblListaMaterialesBusiness oLisMatB)
        {
            try
            {
                hddIdListaMateriales.Value = Item.IdListaMateriales;
                hddIdArticulo.Value = Item.IdArticulo.ToString();
                txtArticulo.Text = Item.Articulo;
                txtCantidadMaestro.Text = Item.Cantidad.ToString(Util.ObtenerFormatoDecimal());
                chkActivo.Checked = Item.Activo;
                dgMateriales.DataSource = oLisMatB.ObtenerListaMaterialesDetallesPorID(Item.IdListaMateriales);
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
            if (Request.QueryString["ArticuloCompuesto"] == "1")
            {
                lblTitulo.Text = "Configurar Artículo Compuesto";
            }
            else
            {
                lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ListaMateriales);
            }
            lblArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
            lblCantidad.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad);
            lblArticuloMat.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.MateriaPrima);
            lblCantidadMaestro.Text = "Cantidad a producir";
            chkActivo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Activo);
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
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

        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo adicionar la materia prima. {0}", ex.Message));
            }
            hddIdArticuloMat.Value = "0";
            txtArticuloMat.Text = "";
            txtCantidad.Text = "0.00";
        }

        protected void dgMateriales_EditCommand(object source, DataGridCommandEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo eliminar la metaria prima. {0}", ex.Message));
            }
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

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmProduccion.aspx");
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblListaMaterialesItem oListMatI = new tblListaMaterialesItem();
                List<tblListaMaterialesDetalleItem> oLisMatDetI = new List<tblListaMaterialesDetalleItem>();
                tblListaMaterialesBusiness oLisMatB = new tblListaMaterialesBusiness(CadenaConexion);
                CargarDatosGuardar(ref oListMatI, ref oLisMatB);
                foreach (DataGridItem Detalle in dgMateriales.Items)
                {
                    tblListaMaterialesDetalleItem oMatDetI = new tblListaMaterialesDetalleItem();
                    oMatDetI.IdArticulo = long.Parse(Detalle.Cells[dgMaterialesEnum.IdArticulo.GetHashCode()].Text);
                    oMatDetI.Cantidad = decimal.Parse(Detalle.Cells[dgMaterialesEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                    oLisMatDetI.Add(oMatDetI);
                }
                if(oLisMatB.Guardar(oListMatI, oLisMatDetI))
                {
                    MostrarMensaje("Lista de Materiales","La lista de materiales se guardo con exito.");
                    hddIdListaMateriales.Value = oListMatI.IdListaMateriales;
                }
                else
                {
                    MostrarMensaje("Error", "La lista de materiales no se pudo guardar.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("La lista de materiales no se pudo guardar. {0}", ex.Message));
            }
        }

        private void CargarDatosGuardar(ref tblListaMaterialesItem Item, ref tblListaMaterialesBusiness oLisMatB)
        {
            try
            {
                if (!string.IsNullOrEmpty(hddIdListaMateriales.Value))
                {
                    Item = oLisMatB.ObtenerListaMaterialesPorID(hddIdListaMateriales.Value);
                }
                Item.IdArticulo = long.Parse(hddIdArticulo.Value);
                Item.Cantidad = decimal.Parse(txtCantidadMaestro.Text, NumberStyles.Currency);
                Item.Activo = chkActivo.Checked;
                Item.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                Item.IdEmpresa = oUsuarioI.idEmpresa;
                Item.IdUsuario = oUsuarioI.idUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}