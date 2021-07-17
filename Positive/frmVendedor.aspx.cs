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
using System.IO;

namespace Inventario
{
    public partial class frmVendedor : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private long IdVendedor = 0;
        private enum dgVendedoresColumnEnum {
            idVendedor = 0
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Vendedor.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {
                    if (Util.EsEntero(Request.QueryString["IdVendedor"]))
                    {
                        IdVendedor = long.Parse(Request.QueryString["IdVendedor"]);
                    }
                    ConfiguracionIdioma();
                    CargarInformacion();
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0} EstablecerAutoCompleteBodegaSimple('{1}','Ashx/Bodega.ashx','{2}','{3}',1);", strScript, txtBodega.ClientID, hddIdBodega.ClientID, oUsuarioI.idEmpresa);
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
        private void ConfiguracionIdioma()
        {
            Traductor oCIdioma = new Traductor();
            Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
            if (!Util.EsEntero(Request.Form["ctl00$ddlIdiomas"]))
            {
                Idioma = (Idioma.Traductor.IdiomaEnum)oUsuarioI.IdIdioma;
            }
            else
            {
                Idioma = (Traductor.IdiomaEnum)int.Parse(Request.Form["ctl00$ddlIdiomas"]);
            }
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Vendedor);
            lblNombre.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            lblComision.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Comision);
            lblBodega.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega);
            lblActivo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Activo);
        }
        private void CargarInformacion() {
            tblVendedorBusiness oVBiz = new tblVendedorBusiness(Util.ObtenerCadenaConexion());
            dgVendedores.DataSource = oVBiz.ObtenerVendedorLista(oUsuarioI.idEmpresa);
            dgVendedores.DataBind();
            if (IdVendedor > 0 & !IsPostBack) {
                tblVendedorItem oVItem = oVBiz.ObtenerVendedor(IdVendedor);
                if (oVItem != null && oVItem.idVendedor > 0) {
                    txtidVendedor.Text = oVItem.idVendedor.ToString();
                    txtNombre.Text = oVItem.Nombre;
                    txtComision.Text = oVItem.Comision.ToString();
                    chkActivo.Checked = oVItem.Activo;
                    if (oVItem.idBodega > 0) {
                        tblBodegaBusiness oBBiz = new tblBodegaBusiness(Util.ObtenerCadenaConexion());
                        tblBodegaItem oBItem = oBBiz.ObtenerBodega(oVItem.idBodega, oUsuarioI.idEmpresa);
                        if (oBItem != null && oBItem.IdBodega > 0) {
                            hddIdBodega.Value = oBItem.IdBodega.ToString();
                            txtBodega.Text = oBItem.Descripcion;
                        }
                    }
                }
            }
        }
        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmVendedor.aspx");
        }
        private string ValidarInformacion() {
            Traductor oCIdioma = new Traductor();
            Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
            if (!Util.EsEntero(Request.Form["ctl00$ddlIdiomas"]))
            {
                Idioma = (Idioma.Traductor.IdiomaEnum)oUsuarioI.IdIdioma;
            }
            else
            {
                Idioma = (Traductor.IdiomaEnum)int.Parse(Request.Form["ctl00$ddlIdiomas"]);
            }
            string MensajeError = "";
            if (string.IsNullOrEmpty(txtNombre.Text)) {
                MensajeError = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.NombreObligatorio);
            }
            if (!Util.EsDecimal(txtComision.Text)) {
                MensajeError = string.Format("{0}<br>{1}",MensajeError,oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ComisionDecimal));
            }
            return MensajeError;
        }
        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            Traductor oCIdioma = new Traductor();
            Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
            if (!Util.EsEntero(Request.Form["ctl00$ddlIdiomas"]))
            {
                Idioma = (Idioma.Traductor.IdiomaEnum)oUsuarioI.IdIdioma;
            }
            else
            {
                Idioma = (Traductor.IdiomaEnum)int.Parse(Request.Form["ctl00$ddlIdiomas"]);
            }
            string MensajeError = ValidarInformacion();
            if (string.IsNullOrEmpty(MensajeError))
            {
                tblVendedorBusiness oVBiz = new tblVendedorBusiness(Util.ObtenerCadenaConexion());
                tblVendedorItem oVItem;
                if (IdVendedor > 0)
                {
                    oVItem = oVBiz.ObtenerVendedor(IdVendedor);
                }
                else {
                    oVItem = new tblVendedorItem();
                    oVItem.Fecha = DateTime.Now;
                    oVItem.idUsuario = oUsuarioI.idUsuario;
                    oVItem.idEmpresa = oUsuarioI.idEmpresa;
                }
                oVItem.Nombre = txtNombre.Text;
                if (Util.EsEntero(hddIdBodega.Value)) {
                    oVItem.idBodega = long.Parse(hddIdBodega.Value);
                }
                oVItem.Comision = decimal.Parse(txtComision.Text);
                oVItem.Activo = chkActivo.Checked;
                if (oVBiz.Guardar(oVItem))
                {
                    Response.Redirect("frmVendedor.aspx");
                }
                else {
                    MostrarMensaje("Error", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.VendedorError));
                }
            }
            else {
                MostrarMensaje(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Validacion), MensajeError);
            }
        }
        private void LimpiarFormulario() {
            txtidVendedor.Text = "";
            txtNombre.Text = "";
            txtComision.Text = "";
            txtBodega.Text = "";
            hddIdBodega.Value = "";
            tblVendedorBusiness oVBiz = new tblVendedorBusiness(Util.ObtenerCadenaConexion());
            dgVendedores.DataSource = oVBiz.ObtenerVendedorLista(oUsuarioI.idEmpresa);
            dgVendedores.DataBind();
        }

        protected void dgVendedores_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Response.Redirect($"frmVendedor.aspx?idVendedor={e.Item.Cells[dgVendedoresColumnEnum.idVendedor.GetHashCode()].Text}");
        }
    }
}