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
    public partial class frmCampanaCliente : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private long IdCampana = 0;
        private long IdCampanaCliente = 0;
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
                    if (Util.EsEntero(Request.QueryString["IdCampanaCliente"]))
                    {
                        IdCampanaCliente = long.Parse(Request.QueryString["IdCampanaCliente"]);
                    }
                    ConfiguracionIdioma();
                    InicializarControles();
                    CargarInformacion();
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0} pestañas();", strScript);
                        strScript = string.Format("{0} EstablecerAutoCompleteTercero('{1}','Ashx/Tercero.ashx','{2}');", strScript, txtTercero.ClientID, hddIdTercero.ClientID);
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo));
            tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
            tblCampanaItem oCItem = oABiz.ObtenerCampana(IdCampana, oUsuarioI.idEmpresa);
            if (oCItem != null && oCItem.idCampana > 0)
            {
                lblCampana.Text = string.Format("<span>{2}:&nbsp;<b>{0} - {1}</b></span>", oCItem.idCampana, oCItem.Nombre, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana));
            }
            else
            {
                MostrarMensaje("Error", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaArticuloErrorId));
                btnGuardar.Visible = false;
            }
            lblExcluir.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Excluir);
            lblTipo.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo);
            lblGrupoCliente.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoCliente);
            lblCliente.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
            liCampanaArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
        }

        private void InicializarControles() {
            if (!IsPostBack) { 
                tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                ddlGrupoCliente.DataSource = oTBiz.ObtenerGrupoClienteLista(oUsuarioI.idEmpresa,"");
                ddlGrupoCliente.DataTextField = "Nombre";
                ddlGrupoCliente.DataValueField = "idGrupoCliente";
                ddlGrupoCliente.DataBind();
            }
            ddlTipo.Attributes.Add("onchange", "MostrarTipoCampanaCliente(this);");
        }
        private void CargarInformacion() {
            if (!IsPostBack && IdCampanaCliente>0) {
                tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
                tblCampanaClienteItem oCCItem = oABiz.ObtenerCampanaCliente(IdCampanaCliente, oUsuarioI.idEmpresa);
                if (oCCItem != null && oCCItem.idCampanaCliente > 0) {
                    lblIdCampanaCliente.Text = oCCItem.idCampanaCliente.ToString();
                    chkExcluir.Checked = oCCItem.Excluir;
                    ddlTipo.SelectedValue = oCCItem.TipoCampanaCliente.ToString();
                    switch ((tblArticuloBusiness.TipoCampanaClienteEnum)oCCItem.TipoCampanaCliente) { 
                        case tblArticuloBusiness.TipoCampanaClienteEnum.Todos:
                            OcultarControlClass("cssGrupoCliente");
                            OcultarControlClass("cssCliente");
                            break;
                        case tblArticuloBusiness.TipoCampanaClienteEnum.GrupoCliente:
                            MostrarControlClass("cssGrupoCliente");
                            OcultarControlClass("cssCliente");
                            ddlGrupoCliente.SelectedValue = oCCItem.Codigo;
                            break;
                        case tblArticuloBusiness.TipoCampanaClienteEnum.Cliente:
                            OcultarControlClass("cssGrupoCliente");
                            MostrarControlClass("cssCliente");
                            tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                            tblTerceroItem oTItem = oTBiz.ObtenerTercero(long.Parse(oCCItem.Codigo), oUsuarioI.idEmpresa);
                            hddIdTercero.Value = oTItem.IdTercero.ToString();
                            txtTercero.Text = oTItem.Nombre;
                            break;
                    }
                }
            }
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
            switch ((tblArticuloBusiness.TipoCampanaClienteEnum)int.Parse(ddlTipo.SelectedValue)) {
                case tblArticuloBusiness.TipoCampanaClienteEnum.GrupoCliente:
                    if (string.IsNullOrEmpty(ddlGrupoCliente.SelectedValue)) {
                        MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaGrupoCliente));
                    }
                    break;
                case tblArticuloBusiness.TipoCampanaClienteEnum.Cliente:
                    if (string.IsNullOrEmpty(hddIdTercero.Value)) {
                        MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaCliente));
                    }
                    break;
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
                tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
                tblCampanaClienteItem oCCItem;
                if (IdCampanaCliente > 0)
                {
                    oCCItem = oABiz.ObtenerCampanaCliente(IdCampanaCliente, oUsuarioI.idEmpresa);
                }
                else {
                    oCCItem = new tblCampanaClienteItem();
                    oCCItem.idUsuario = oUsuarioI.idUsuario;
                    oCCItem.Fecha = DateTime.Now;
                    oCCItem.idCampana = IdCampana;
                }
                oCCItem.Excluir = chkExcluir.Checked;
                oCCItem.TipoCampanaCliente = short.Parse(ddlTipo.SelectedValue);
                switch ((tblArticuloBusiness.TipoCampanaClienteEnum)int.Parse(ddlTipo.SelectedValue))
                {
                    case tblArticuloBusiness.TipoCampanaClienteEnum.GrupoCliente:
                        oCCItem.Codigo = ddlGrupoCliente.SelectedValue;
                        break;
                    case tblArticuloBusiness.TipoCampanaClienteEnum.Cliente:
                        oCCItem.Codigo = hddIdTercero.Value;
                        break;
                    case tblArticuloBusiness.TipoCampanaClienteEnum.Todos:
                        oCCItem.Codigo = "";
                        break;
                }
                if (oABiz.Guardar(oCCItem))
                {
                    Response.Redirect(string.Format("frmCampana.aspx?IdCampana={0}", IdCampana));
                }
                else {
                    MostrarMensaje(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaValidacion), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaClienteErrorGuardar));
                }
            }
            else {
                MostrarMensaje(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaValidacion), MensajeError);
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(string.Format("frmCampana.aspx?IdCampana={0}",IdCampana));
        }
    }
}