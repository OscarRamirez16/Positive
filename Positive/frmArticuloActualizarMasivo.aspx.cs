using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using System.Globalization;

namespace Inventario
{
    public partial class frmArticuloActualizarMasivo : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        enum dgArticulosColumnsEnum
        {
            IdArticulo = 0,
            CodigoArticulo = 1,
            Nombre = 2,
            IdBodega = 3,
            Bodega = 4,
            Precio = 5,
            Costo = 6,
            Marca = 7,
            Ubicacion = 8
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
                        if (!IsPostBack)
                        {
                            OcultarControl(divBotones.ClientID);
                            OcultarControl(divGrilla.ClientID);
                            CargarDelimitadores();
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
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagina. {0}", ex.Message));
            }
        }

        private void CargarDelimitadores()
        {
            try
            {
                ddlDelimitador.Items.Add(new ListItem("Coma (,)", ","));
                ddlDelimitador.Items.Add(new ListItem("Punto y coma (;)", ";"));
                ddlDelimitador.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los delimitadores. {0}", ex.Message));
            }
        }

        protected void btnCargar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (fulArticulos.HasFile)
                {
                    btnGuardarMasivo.Visible = true;
                    List<tblArticuloItem> Articulos = new List<tblArticuloItem>();
                    tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
                    string error = oABiz.LeerArticulosParaEditarMasivo(fulArticulos.PostedFile.InputStream, Articulos, char.Parse(ddlDelimitador.SelectedValue), oUsuarioI.idEmpresa);
                    if (error != "")
                    {
                        MostrarMensaje("Error", error);
                    }
                    else
                    {
                        dgArticulosMasivo.DataSource = Articulos;
                        dgArticulosMasivo.DataBind();
                    }
                }
                else
                {
                    Response.Write("<script>alert('No hay un archivo seleccionado');</script>");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo realizar la operación. {0}", ex.Message));
            }
        }

        protected void btnGuardarMasivo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (oRolPagI.Actualizar)
                {
                    List<tblArticuloItem> oListArt = new List<tblArticuloItem>();
                    foreach (DataGridItem Item in dgArticulosMasivo.Items)
                    {
                        tblArticuloItem oArtI = new tblArticuloItem();
                        oArtI.IdArticulo = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdArticulo.GetHashCode()].Text);
                        if(Item.Cells[dgArticulosColumnsEnum.Nombre.GetHashCode()].Text != "&nbsp;")
                        {
                            oArtI.Nombre = Item.Cells[dgArticulosColumnsEnum.Nombre.GetHashCode()].Text;
                        }
                        if (Item.Cells[dgArticulosColumnsEnum.Marca.GetHashCode()].Text != "&nbsp;")
                        {
                            oArtI.Marca = Item.Cells[dgArticulosColumnsEnum.Marca.GetHashCode()].Text;
                        }
                        if (Item.Cells[dgArticulosColumnsEnum.Ubicacion.GetHashCode()].Text != "&nbsp;")
                        {
                            oArtI.Ubicacion = Item.Cells[dgArticulosColumnsEnum.Ubicacion.GetHashCode()].Text;
                        }
                        oArtI.IdBodega = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdBodega.GetHashCode()].Text, NumberStyles.Currency);
                        oArtI.Precio = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.Precio.GetHashCode()].Text, NumberStyles.Currency);
                        oArtI.Costo = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.Costo.GetHashCode()].Text, NumberStyles.Currency);
                        oListArt.Add(oArtI);
                    }
                    if (oListArt.Count > 0)
                    {
                        tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                        string Error = oArtB.ActualizarDatosArticulosMasivo(oListArt);
                        if (string.IsNullOrEmpty(Error))
                        {
                            MostrarMensaje("Exito", "La actualización masiva se realizó con exito.");
                            LimpiarControles();
                        }
                        else
                        {
                            MostrarMensaje("Error", Error);
                        }
                    }
                    else
                    {
                        MostrarMensaje("Error", "No hay artículos para realizar la operación.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo realizar la operación. {0}", ex.Message));
            }
        }
        private void LimpiarControles()
        {
            CargarDelimitadores();
            OcultarControl(divBotones.ClientID);
            OcultarControl(divGrilla.ClientID);
            dgArticulosMasivo.DataSource = null;
            dgArticulosMasivo.DataBind();
        }
        protected void btnCancelarMasivo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
    }
}