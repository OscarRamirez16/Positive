using System;
using System.Web.UI;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;

namespace Inventario
{
    public partial class frmCampanaArticulo : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private long IdCampana = 0;
        private long IdCampanaArticulo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Campana.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {
                    if (Util.EsEntero(Request.QueryString["IdCampana"]))
                    {
                        IdCampana = long.Parse(Request.QueryString["IdCampana"]);
                    }
                    if (Util.EsEntero(Request.QueryString["IdCampanaArticulo"]))
                    {
                        IdCampanaArticulo = long.Parse(Request.QueryString["IdCampanaArticulo"]);
                    }
                    ConfiguracionIdioma();
                    InicializarControles();
                    CargarInformacion();
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0} pestañas();", strScript);
                        strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}','{3}',3);", strScript, txtArticulo.ClientID, hddIdArticulo.ClientID, oUsuarioI.idEmpresa);
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
        private void ConfiguracionIdioma() {
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo));
            tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
            tblCampanaItem oCItem = oABiz.ObtenerCampana(IdCampana, oUsuarioI.idEmpresa);
            if (oCItem != null && oCItem.idCampana > 0) {
                lblCampana.Text = string.Format("<span>{2}:&nbsp;<b>{0} - {1}</b></span>", oCItem.idCampana, oCItem.Nombre, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana));
            }
            else{
                MostrarMensaje("Error", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaArticuloErrorId));
                btnGuardar.Visible = false;
            }
            lblExcluir.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Excluir);
            lblTipo.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo);
            lblGrupoArticulo.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lineas);
            lblArticulo.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
            lblTipoDescuento.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoDescuento);
            lblValorDescuento.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Valor);
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
            liCampanaArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
        }
        private void CargarInformacion(){
            if (!IsPostBack) {
                if (IdCampanaArticulo > 0) { 
                    tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
                    tblCampanaArticuloItem oCAItem = oABiz.ObtenerCampanaArticulo(IdCampanaArticulo, oUsuarioI.idEmpresa);
                    if (oCAItem != null && oCAItem.idCampanaArticulo > 0) {
                        lblIdCampanaArticulo.Text = oCAItem.idCampanaArticulo.ToString();
                        chkExcluir.Checked = oCAItem.Excluir;
                        ddlTipo.SelectedValue = oCAItem.TipoCampanaArticulo.ToString();
                        switch((tblArticuloBusiness.TipoCampanaArticuloEnum)oCAItem.TipoCampanaArticulo){
                            case tblArticuloBusiness.TipoCampanaArticuloEnum.Todos:
                                OcultarControlClass("cssGrupoArticulo");
                                OcultarControlClass("cssArticulo");
                                break;
                            case tblArticuloBusiness.TipoCampanaArticuloEnum.GrupoArticulo:
                                OcultarControlClass("cssArticulo");
                                MostrarControlClass("cssGrupoArticulo");
                                ddlGrupoArticulo.SelectedValue = oCAItem.Codigo;
                                break;
                            case tblArticuloBusiness.TipoCampanaArticuloEnum.Articulo:
                                OcultarControlClass("cssGrupoArticulo");
                                MostrarControlClass("cssArticulo");
                                tblArticuloItem oAItem = oABiz.ObtenerArticuloPorID(long.Parse(oCAItem.Codigo),oUsuarioI.idEmpresa);
                                hddIdArticulo.Value = oAItem.IdArticulo.ToString();
                                txtArticulo.Text = oAItem.Nombre;
                                break;
                        }
                        ddlTipoDescuento.SelectedValue = oCAItem.TipoDescuento.ToString();
                        txtValorDescuento.Text = oCAItem.ValorDescuento.ToString();
                    }
                }
            }
        }
        private void InicializarControles() {
            if (!IsPostBack) {
                tblLineaBussines oLBiz = new tblLineaBussines(CadenaConexion);
                ddlGrupoArticulo.DataSource = oLBiz.ObtenerLineaLista(oUsuarioI.idEmpresa);
                ddlGrupoArticulo.DataTextField = "Nombre";
                ddlGrupoArticulo.DataValueField = "IdLinea";
                ddlGrupoArticulo.DataBind();
            }
            ddlTipo.Attributes.Add("onchange", "MostrarTipoCampanaArticulo(this);");
        }
        private string ValidarInformacion() {
            string MensajeError = "";
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
            switch ((tblArticuloBusiness.TipoCampanaArticuloEnum)int.Parse(ddlTipo.SelectedValue))
            {
                case tblArticuloBusiness.TipoCampanaArticuloEnum.Articulo:
                    if(string.IsNullOrEmpty(hddIdArticulo.Value)){
                        MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaArticulo));
                    }
                    break;
                case tblArticuloBusiness.TipoCampanaArticuloEnum.GrupoArticulo:
                    if(string.IsNullOrEmpty(ddlGrupoArticulo.SelectedValue)){
                        MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaGrupoArticulo));
                    }
                    break;
            }
            if (!Util.EsDecimal(txtValorDescuento.Text)) {
                MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaValorDescuento));
            }
            return MensajeError;
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            string MensajeError = ValidarInformacion();
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
            if (string.IsNullOrEmpty(MensajeError))
            {
                tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
                tblCampanaArticuloItem oCAItem;
                if (IdCampanaArticulo > 0)
                {
                    oCAItem = oABiz.ObtenerCampanaArticulo(IdCampanaArticulo, oUsuarioI.idEmpresa);
                }
                else {
                    oCAItem = new tblCampanaArticuloItem();
                    oCAItem.idCampana = IdCampana;
                    oCAItem.idUsuario = oUsuarioI.idUsuario;
                    oCAItem.Fecha = DateTime.Now;
                }
                oCAItem.Excluir = chkExcluir.Checked;
                oCAItem.TipoCampanaArticulo = short.Parse(ddlTipo.SelectedValue);
                switch ((tblArticuloBusiness.TipoCampanaArticuloEnum)int.Parse(ddlTipo.SelectedValue))
                {
                    case tblArticuloBusiness.TipoCampanaArticuloEnum.Articulo:
                        oCAItem.Codigo = hddIdArticulo.Value;
                        break;
                    case tblArticuloBusiness.TipoCampanaArticuloEnum.GrupoArticulo:
                        oCAItem.Codigo = ddlGrupoArticulo.SelectedValue;
                        break;
                }
                oCAItem.TipoDescuento = short.Parse(ddlTipoDescuento.SelectedValue);
                oCAItem.ValorDescuento = decimal.Parse(txtValorDescuento.Text);
                if (oABiz.Guardar(oCAItem))
                {
                    Response.Redirect(string.Format("frmCampana.aspx?IdCampana={0}", IdCampana));
                }
                else {
                    MostrarMensaje("Error", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaArticuloErrorGuardar));
                }
            }
            else {
                MostrarMensaje(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaValidacion), MensajeError);
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(string.Format("frmCampana.aspx?IdCampana={0}", IdCampana));
        }
    }
}