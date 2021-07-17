using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;

namespace Inventario
{
    public partial class frmGrupoCliente : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private long IdGrupoCliente = 0;

        private enum dgGrupoClienteColumnEnum
        { 
            IdGrupoCliente = 0,
            Nombre = 1,
            CuentaContable = 2,
            lnkEditar = 3
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.GrupoCliente.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {
                    if (Util.EsEntero(Request.QueryString["IdGrupoCliente"]))
                    {
                        IdGrupoCliente = long.Parse(Request.QueryString["IdGrupoCliente"]);
                    }
                    ConfiguracionIdioma();
                    InicializarControles();
                    CargarInformacion();
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
            lblIdGrupoCliente.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Codigo);
            lblNombre.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoCliente);
            lblCuentaContable.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuentaContable);
            liGrupoCliente.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoCliente);
            liBuscarGrupoCliente.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoCliente));
        }

        private void CargarInformacion()
        {
            if (!IsPostBack && IdGrupoCliente > 0)
            {
                tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                tblGrupoClienteItem oGCItem = oTBiz.ObtenerGrupoCliente(IdGrupoCliente, oUsuarioI.idEmpresa);
                if (oGCItem != null && oGCItem.idGrupoCliente > 0)
                {
                    txtIdGrupoCliente.Text = oGCItem.idGrupoCliente.ToString();
                    txtNombre.Text = oGCItem.Nombre;
                    txtCuentaContable.Text = oGCItem.CuentaContable;
                }
            }
        }

        private void InicializarControles()
        {
            if (!IsPostBack) {
                tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                dgGrupoCliente.DataSource = oTBiz.ObtenerGrupoClienteLista(oUsuarioI.idEmpresa, "");
                dgGrupoCliente.DataBind();
            }
        }

        private string ValidarInformacion()
        {
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
            if (!Util.EsEntero(txtIdGrupoCliente.Text))
            {
                MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CodigoValidacion));
            }
            else
            {
                if (IdGrupoCliente <= 0) {
                    tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                    tblGrupoClienteItem oGCItem = oTBiz.ObtenerGrupoCliente(long.Parse(txtIdGrupoCliente.Text), oUsuarioI.idEmpresa);
                    if (oGCItem != null && oGCItem.idGrupoCliente > 0) {
                        MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoClienteExite));
                    }
                }
            }
            if (string.IsNullOrEmpty(txtNombre.Text)) {
                MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.NombreObligatorio));
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
                tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                tblGrupoClienteItem oGCItem;
                if (IdGrupoCliente > 0)
                {
                    oGCItem = oTBiz.ObtenerGrupoCliente(IdGrupoCliente, oUsuarioI.idEmpresa);
                    oGCItem.idGrupoCliente = IdGrupoCliente;
                }
                else
                {
                    oGCItem = new tblGrupoClienteItem();
                    oGCItem.idUsuario = oUsuarioI.idUsuario;
                    oGCItem.idEmpresa = oUsuarioI.idEmpresa;
                    oGCItem.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                    oGCItem.idGrupoCliente = long.Parse(txtIdGrupoCliente.Text);
                }
                oGCItem.Nombre = txtNombre.Text;
                oGCItem.CuentaContable = txtCuentaContable.Text;
                if (oTBiz.Guardar(oGCItem))
                {
                    Response.Redirect("frmGrupoCliente.aspx");
                }
                else
                {
                    MostrarMensaje("Error",oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoClienteErrorGuardar));
                }
            }
            else
            {
                MostrarMensaje(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Validacion),MensajeError);
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmGrupoCliente.aspx");
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
            dgGrupoCliente.DataSource = oTBiz.ObtenerGrupoClienteLista(oUsuarioI.idEmpresa, txtTexto.Text);
            dgGrupoCliente.DataBind();
            ShowTab("aBuscarGrupoCliente", "BuscarGrupoCliente");
        }

        protected void btnCancelarBus_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmGrupoCliente.aspx");
        }

        protected void dgGrupoCliente_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Response.Redirect(string.Format("frmGrupoCliente.aspx?IdGrupoCliente={0}",e.Item.Cells[dgGrupoClienteColumnEnum.IdGrupoCliente.GetHashCode()].Text));
        }
    }
}