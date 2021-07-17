using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using System.Text;
using System.IO;
using HQSFramework.Base;
using Idioma;

namespace Inventario
{
    public partial class frmReporteMovimientosDiarios : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgMovimientosEnum
        {
            idMovimiento = 0,
            fechaMovimiento = 1,
            observaciones = 2,
            idTipoMovimiento = 3,
            TipoMovimiento = 4,
            idUsuario = 5,
            Usuario = 6,
            valor = 7,
            idEmpresa = 8
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VerFrmReporteMovimientosDiarios.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
                            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
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
                        Response.Write("<script>alert('El usuario no posee permisos en la pagina');</script>");
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
                Response.Write("<script>alert('Error al cargar la pagina de documentos. " + ex.ToString() + "');</script>");
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
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.MovimientosDocumentos);
            lblUsuario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
            lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoMovimiento);
            lblFechaInical.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaInicial);
            lblFechaFinal.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaFinal);
            lbltotal.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Total);
            CargarTipoMovimiento(oCIdioma, Idioma);
            CargarUsuarios(oCIdioma, Idioma);
        }

        public void CargarTipoMovimiento(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblMovimientosDiariosBusiness oTipMovB = new tblMovimientosDiariosBusiness(CadenaConexion);
                string Opcion = ddlTipoMovimiento.SelectedValue;
                ddlTipoMovimiento.Items.Clear();
                ddlTipoMovimiento.SelectedValue = null;
                ddlTipoMovimiento.DataSource = oTipMovB.ObtenerTipoMovimientoDiarioLista();
                ddlTipoMovimiento.DataBind();
                ddlTipoMovimiento.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoMovimiento)), "0"));
                if (!IsPostBack)
                {
                    ddlTipoMovimiento.SelectedValue = "0";
                }
                else
                {
                    ddlTipoMovimiento.SelectedValue = Opcion;
                }
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

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtTotal.Text = "0";
                tblMovimientosDiariosBusiness oMovDiaB = new tblMovimientosDiariosBusiness(CadenaConexion);
                dgMovimientosDiarios.DataSource = oMovDiaB.ObtenerMovimientosDiariosLista(DateTime.Parse(txtFechaInicial.Text), DateTime.Parse(txtFechaFinal.Text), oUsuarioI.idEmpresa, short.Parse(ddlTipoMovimiento.SelectedValue), long.Parse(ddlUsuario.SelectedValue));
                dgMovimientosDiarios.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los movimientos diarios. {0}", ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void dgMovimientosDiarios_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                txtTotal.Text = (double.Parse(txtTotal.Text) + double.Parse(e.Item.Cells[dgMovimientosEnum.valor.GetHashCode()].Text)).ToString();
            }
        }

        private void ExportGrid()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgMovimientosDiarios.EnableViewState = false;
            dgMovimientosDiarios.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=MovimientosDiarios {0}.xls", DateTime.Now));
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void btnExportarExcel_Click(object sender, ImageClickEventArgs e)
        {
            ExportGrid();
        }
    }
}