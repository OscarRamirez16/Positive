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

namespace Inventario
{
    public partial class frmPreciosPorBodega : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgBodegasColumnsEnum
        {
            Descripcion = 0,
            txtDescripcion = 1,
            TipoManejoPrecio = 2,
            Precio = 3,
            Seleccionar = 4
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.FijarPreciosPorBodega.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                            tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                            lblArticulo.Text = string.Format("{0} - {1}", oArtB.ObtenerArticuloPorID(long.Parse(Request.QueryString["IdArticulo"]), oUsuarioI.idEmpresa).Nombre, oBodB.ObtenerBodega(long.Parse(Request.QueryString["IdBodega"]), oUsuarioI.idEmpresa).Descripcion);
                            CargarPreciosPorBodega();
                        }
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
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FijarPreciosBodega);
            ConfigurarIdiomaGrillaBodegas(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrillaBodegas(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgBodegas.Columns[dgBodegasColumnsEnum.txtDescripcion.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Descripcion);
                dgBodegas.Columns[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoManejo);
                dgBodegas.Columns[dgBodegasColumnsEnum.Precio.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Precio);
                dgBodegas.Columns[dgBodegasColumnsEnum.Seleccionar.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarPreciosPorBodega()
        {
            try
            {
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                dgBodegas.DataSource = oBodB.ObtenerPreciosPorBodega(long.Parse(Request.QueryString["IdArticulo"]), long.Parse(Request.QueryString["IdBodega"]));
                dgBodegas.DataBind();
                if (lblArticulo.Text != "")
                {
                    List<tblPreciosPorBodegaItem> oPreBodI = new List<tblPreciosPorBodegaItem>();
                    oPreBodI = oBodB.ObtenerPreciosPorBodega(long.Parse(Request.QueryString["IdArticulo"]), long.Parse(Request.QueryString["IdBodega"]));
                    foreach (tblPreciosPorBodegaItem bodega in oPreBodI)
                    {
                        foreach (DataGridItem Item in dgBodegas.Items)
                        {
                            if (!string.IsNullOrEmpty(bodega.Descripcion) && ((TextBox)(Item.Cells[dgBodegasColumnsEnum.txtDescripcion.GetHashCode()].FindControl("txtDescripcion"))).Text == bodega.Descripcion)
                            {
                                ((TextBox)(Item.Cells[dgBodegasColumnsEnum.txtDescripcion.GetHashCode()].FindControl("txtDescripcion"))).Text = bodega.Descripcion;
                                ((DropDownList)(Item.Cells[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].FindControl("ddlTipoManejo"))).SelectedValue = bodega.IdTipoManejoPrecio.ToString();
                                ((TextBox)(Item.Cells[dgBodegasColumnsEnum.Precio.GetHashCode()].FindControl("txtPrecio"))).Text = bodega.Valor.ToString(Util.ObtenerFormatoDecimal());
                                ((CheckBox)(Item.Cells[dgBodegasColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void dgBodegas_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                ((DropDownList)(e.Item.Cells[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].FindControl("ddlTipoManejo"))).DataSource = oBodB.ObtenerTipoManejoPrecioArticulo();
                ((DropDownList)(e.Item.Cells[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].FindControl("ddlTipoManejo"))).DataBind();
                if (e.Item.Cells[dgBodegasColumnsEnum.Descripcion.GetHashCode()].Text != "&nbsp;")
                {
                    ((TextBox)(e.Item.Cells[dgBodegasColumnsEnum.txtDescripcion.GetHashCode()].FindControl("txtDescripcion"))).Text = e.Item.Cells[dgBodegasColumnsEnum.Descripcion.GetHashCode()].Text;
                }
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                List<tblPreciosPorBodegaItem> oListPreBod = new List<tblPreciosPorBodegaItem>();
                foreach (DataGridItem Item in dgBodegas.Items)
                {
                    if (((CheckBox)(Item.Cells[dgBodegasColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                    {
                        tblPreciosPorBodegaItem oPreBodI = new tblPreciosPorBodegaItem();
                        oPreBodI.IdArticulo = long.Parse(Request.QueryString["IdArticulo"]);
                        oPreBodI.IdBodega = long.Parse(Request.QueryString["IdBodega"]);
                        oPreBodI.Descripcion = ((TextBox)(Item.Cells[dgBodegasColumnsEnum.txtDescripcion.GetHashCode()].FindControl("txtDescripcion"))).Text;
                        oPreBodI.IdTipoManejoPrecio = short.Parse(((DropDownList)(Item.Cells[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].FindControl("ddlTipoManejo"))).SelectedValue);
                        oPreBodI.Valor = decimal.Parse(((TextBox)(Item.Cells[dgBodegasColumnsEnum.Precio.GetHashCode()].FindControl("txtPrecio"))).Text.Replace(".", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator));
                        oListPreBod.Add(oPreBodI);
                    }
                }
                if (oListPreBod.Count > 0)
                {
                    tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                    oBodB.EliminarPreciosPorBodega(long.Parse(Request.QueryString["IdArticulo"]), long.Parse(Request.QueryString["IdBodega"]));
                    string Error = "";
                    foreach (tblPreciosPorBodegaItem Item in oListPreBod)
                    {
                        if (!oBodB.GuardarPreciosPorBodega(Item))
                        {
                            Error = string.Format("{0} * No se pudo guardar el precio {1}.<br>", Error,Item.Descripcion);
                        }
                    }
                    if (!string.IsNullOrEmpty(Error))
                    {
                        MostrarMensaje("Error", Error);
                    }
                    else
                    {
                        MostrarMensaje("Precios Por Bodega", "Los precios se guardaron con exito.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo guardar los precios. {0}", ex.Message));
            }
        }
    }
}