using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using Idioma;
using HQSFramework.Base;
using System.Globalization;
using System.Collections.Generic;

namespace Inventario
{
    public partial class frmAbrirCaja : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CuadreCaja.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        txtSaldoInicial.Attributes.Add("nombreCampo","Saldo Inicial");
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                        if (!IsPostBack)
                        {
                            ObtenerNotificaciones();
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
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }
        private void ObtenerNotificaciones()
        {
            try
            {
                string Mensaje = string.Empty;
                List<tblCajaItem> oListCajaI = new List<tblCajaItem>();
                tblCajaBusiness oCajaB = new tblCajaBusiness(CadenaConexion);
                oListCajaI = oCajaB.ObtenerCajaProximaVencer(oUsuarioI.idEmpresa);
                if (oListCajaI.Count > 0)
                {
                    foreach (tblCajaItem Item in oListCajaI)
                    {
                        if (string.IsNullOrEmpty(Mensaje))
                        {
                            Mensaje = string.Format("* La caja {0} esta proxima a vencer, fecha de vencimiento {1}", Item.nombre, Item.FechaVencimiento.ToShortDateString());
                        }
                        else
                        {
                            Mensaje += string.Format(" * La caja {0} esta proxima a vencer, fecha de vencimiento {1}", Item.nombre, Item.FechaVencimiento.ToShortDateString());
                        }
                    }
                }
                if (!string.IsNullOrEmpty(Mensaje))
                {
                    MostrarAlerta(0, "Advertencia", Mensaje);
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message);
            }
        }
        private void ConfiguracionIdioma()
        {
            try
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
                lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Abrir), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja));
                txtSaldoInicial.Attributes.Add("placeholder", string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Saldo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Inicial)));
                lblObservaciones.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Observaciones);
                CargarUsuarios(oCIdioma, Idioma);
                CargarCajas(oCIdioma, Idioma);
            }
            catch(Exception ex)
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
                ddlUsuario.DataSource = oUsuB.ObtenerUsuarioActivoLista(long.Parse(hddIdEmpresa.Value));
                ddlUsuario.DataBind();
                ddlUsuario.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario)), "0"));
                if (!IsPostBack)
                {
                    ddlUsuario.SelectedValue = oUsuarioI.idUsuario.ToString();
                    if (!oUsuarioI.EsAdmin)
                    {
                        ddlUsuario.Enabled = false;
                    }
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
        private void CargarCajas(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblCajaBusiness oCajB = new tblCajaBusiness(CadenaConexion);
                string Opcion = ddlCaja.SelectedValue;
                ddlCaja.Items.Clear();
                ddlCaja.SelectedValue = null;
                ddlCaja.DataSource = oCajB.ObtenerCajaListaActivas(long.Parse(hddIdEmpresa.Value));
                ddlCaja.DataBind();
                ddlCaja.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja)), "0"));
                if (!IsPostBack)
                {
                    ddlCaja.SelectedValue = "0";
                }
                else
                {
                    ddlCaja.SelectedValue = Opcion;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                tblCuadreCajaItem oCuaCajaGuardar = new tblCuadreCajaItem();
                CargarDatosGuardar(oCuaCajaGuardar);
                oCuaI = oCuaB.ObtenerCuadreCajaLista(oCuaCajaGuardar);
                if (oCuaI.idCuadreCaja == 0)
                {
                    oCuaI = oCuaB.ObtenerCuadreCajaListaPorUsuario(oCuaCajaGuardar);
                    if (oCuaI.idCuadreCaja == 0)
                    {
                        if (oCuaB.Guardar(oCuaCajaGuardar))
                        {
                            MostrarAlerta(1, "Exito", "La caja se abrio con exito.");
                            LimpiarControles();
                        }
                        else
                        {
                            MostrarAlerta(0, "Error", "No se pudo abrir la caja.");
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", "El usuario ya esta relacionado a una caja abierta.");
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", "La caja esta abierta, debe realizar el cierre.");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }
        private void CargarDatosGuardar(tblCuadreCajaItem Caja)
        {
            try
            {
                Caja.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                Caja.SaldoInicial = decimal.Parse(txtSaldoInicial.Text, NumberStyles.Currency);
                Caja.Observaciones = txtObser.Value.Replace(Environment.NewLine, " ");
                Caja.idEmpresa = oUsuarioI.idEmpresa;
                Caja.idUsuario = oUsuarioI.idUsuario;
                Caja.idUsuarioCaja = long.Parse(ddlUsuario.SelectedValue);
                Caja.idCaja = long.Parse(ddlCaja.SelectedValue);
                Caja.Estado = true;
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }
        private void LimpiarControles()
        {
            try
            {
                ConfiguracionIdioma();
                txtSaldoInicial.Text = "";
                txtObser.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}