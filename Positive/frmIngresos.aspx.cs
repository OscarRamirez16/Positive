using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using Idioma;
using HQSFramework.Base;
using System.Globalization;

namespace Inventario
{
    public partial class frmIngresos : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    if (validarCajaAbierta())
                    {
                        SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                        oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Ingresos.GetHashCode().ToString()));
                        if (oRolPagI.Leer)
                        {
                            ConfiguracionIdioma();
                            txtValor.Attributes.Add("nombreCampo", "Valor");
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
                        tblPrincipal.Visible = false;
                        MostrarMensaje("Error", "El usuario no tiene relacionada una caja abierta.");
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ingresar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Efectivo));
            lblUsuario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
            lblValor.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Valor);
            lblObservaciones.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Observaciones);
            CargarUsuarios(oCIdioma, Idioma);
        }

        private bool validarCajaAbierta()
        {
            try
            {
                tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                oCuaI.idEmpresa = oUsuarioI.idEmpresa;
                oCuaI.idUsuarioCaja = oUsuarioI.idUsuario;
                oCuaI.Estado = true;
                oCuaI = oCuaB.ObtenerCuadreCajaListaPorUsuario(oCuaI);
                if (oCuaI.idCuadreCaja == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        private void CargarUsuarios(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblUsuarioBusiness oUsuB = new tblUsuarioBusiness(CadenaConexion);
                string Opcion = ddlUsuario.SelectedValue;
                ddlUsuario.Items.Clear();
                ddlUsuario.SelectedValue = null;
                ddlUsuario.DataSource = oUsuB.ObtenerUsuarioListaPorIdEmpresa(long.Parse(hddIdEmpresa.Value));
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

        private void CargarDatosGuardar(tblMovimientosDiariosItem Movimiento)
        {
            try
            {
                Movimiento.idTipoMovimiento = 2;
                Movimiento.fechaMovimiento = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                Movimiento.idUsuario = oUsuarioI.idUsuario;
                Movimiento.valor = decimal.Parse(txtValor.Text, NumberStyles.Currency);
                Movimiento.observaciones = txtObser.Value;
                Movimiento.idEmpresa = oUsuarioI.idEmpresa;
                Movimiento.EnCuadre = false;
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
                tblMovimientosDiariosBusiness oMovB = new tblMovimientosDiariosBusiness(CadenaConexion);
                tblMovimientosDiariosItem oMovI = new tblMovimientosDiariosItem();
                CargarDatosGuardar(oMovI);
                if (oMovB.Guardar(oMovI))
                {
                    MostrarMensaje("Ingreso","El ingreso se guardo con exito.");
                    limpiarControles();
                }
                else
                {
                    MostrarMensaje("Error","No se pudo guardar el ingreso.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo guardar el ingreso. {0}", ex.Message));
            }
        }

        private void limpiarControles()
        {
            try
            {
                ConfiguracionIdioma();
                txtValor.Text = "";
                txtObser.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}