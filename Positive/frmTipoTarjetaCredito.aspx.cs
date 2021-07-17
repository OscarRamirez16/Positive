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
    public partial class frmTipoTarjetaCredito : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private long IdTipoTarjetaCredito = 0;
        private enum dgTipoTarjetaCreditoColumnEnum {
            IdTipoTarjetaCredito = 0,
            Nombre = 1,
            CuentaContable = 2,
            Activo = 3,
            lnkEditar = 4
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.TipoTarjetaCredito.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {
                    if (Util.EsEntero(Request.QueryString["IdTipoTarjetaCredito"]))
                    {
                        IdTipoTarjetaCredito = long.Parse(Request.QueryString["IdTipoTarjetaCredito"]);
                    }
                    ConfiguracionIdioma();
                    InicializarControles();
                    CargarInformacion();
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0} pestañas();", strScript);
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
            lblIdTipoTarjetaCredito.InnerText = "Id";
            lblNombre.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoTarjetaCredito);
            lblCuentaContable.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuentaContable);
            lblDescripcion.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Descripcion);
            liTipoTarjetaCredito.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoTarjetaCredito);
            liBuscarTipoTarjetaCredito.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoTarjetaCredito));
            lblActivo.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Activo);
            lblTituloBuscar.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoTarjetaCredito));
            lblTexto.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Texto);
        }
        private void InicializarControles() {
            txtIdTipoTarjetaCredito.Enabled = false;
            if (!IsPostBack) { 
                tblPagoBusiness oPBiz = new tblPagoBusiness(CadenaConexion);
                dgTipoTarjetaCredito.DataSource = oPBiz.ObtenerTipoTarjetaCreditoLista("", oUsuarioI.idEmpresa,true);
                dgTipoTarjetaCredito.DataBind();
            }
        }
        private void CargarInformacion(){
            if (!IsPostBack && IdTipoTarjetaCredito>0) {
                tblPagoBusiness oPBiz = new tblPagoBusiness(CadenaConexion);
                tblTipoTarjetaCreditoItem oTTCItem = oPBiz.ObtenerTipoTarjetaCredito(IdTipoTarjetaCredito, oUsuarioI.idEmpresa);
                if (oTTCItem != null && oTTCItem.idTipoTarjetaCredito > 0) {
                    txtIdTipoTarjetaCredito.Text = oTTCItem.idTipoTarjetaCredito.ToString();
                    txtNombre.Text = oTTCItem.Nombre;
                    txtCuentaContable.Text = oTTCItem.CuentaContable;
                    txtDescripcion.Text = oTTCItem.Descripcion;
                    chkActivo.Checked = oTTCItem.Activo;
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
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.NombreObligatorio));
            }
            if (string.IsNullOrEmpty(txtCuentaContable.Text))
            {
                MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuentaContableObligatorio));
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
                tblPagoBusiness oPBiz = new tblPagoBusiness(CadenaConexion);
                tblTipoTarjetaCreditoItem oTTCItem;
                if (IdTipoTarjetaCredito > 0)
                {
                    oTTCItem = oPBiz.ObtenerTipoTarjetaCredito(IdTipoTarjetaCredito, oUsuarioI.idEmpresa);
                }
                else
                {
                    oTTCItem = new tblTipoTarjetaCreditoItem();
                    oTTCItem.idUsuario = oUsuarioI.idUsuario;
                    oTTCItem.idEmpresa = oUsuarioI.idEmpresa;
                    oTTCItem.Fecha = DateTime.Now;
                }
                oTTCItem.Nombre = txtNombre.Text;
                oTTCItem.CuentaContable = txtCuentaContable.Text;
                oTTCItem.Descripcion = txtDescripcion.Text;
                oTTCItem.Activo = chkActivo.Checked;
                if (oPBiz.Guardar(oTTCItem))
                {
                    Response.Redirect("frmTipoTarjetaCredito.aspx");
                }
                else{
                    MostrarMensaje("Error", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoTarjetaCreditoErrorGuardar));
                }
            }
            else {
                MostrarMensaje(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Validacion), MensajeError);
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmTipoTarjetaCredito.aspx");
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            tblPagoBusiness oPBiz = new tblPagoBusiness(CadenaConexion);
            dgTipoTarjetaCredito.DataSource = oPBiz.ObtenerTipoTarjetaCreditoLista(txtTexto.Text, oUsuarioI.idEmpresa, true);
            dgTipoTarjetaCredito.DataBind();
            SeleccionarTab("contenido", 1);
        }

        protected void btnCancelarBus_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmTipoTarjetaCredito.aspx");
        }

        protected void dgTipoTarjetaCredito_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Response.Redirect(string.Format("frmTipoTarjetaCredito.aspx?IdTipoTarjetaCredito={0}",e.Item.Cells[dgTipoTarjetaCreditoColumnEnum.IdTipoTarjetaCredito.GetHashCode()].Text));
        }
    }
}