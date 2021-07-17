using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using HQSFramework.Base;
using Idioma;
using Microsoft.Reporting.WebForms;

namespace Inventario
{
    public partial class frmReporteCuadreDiario : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgCuadresDiariosEnum
        {
            idCuadreCaja = 0,
            Fecha = 1,
            FechaCierre = 2,
            SaldoInicial = 3,
            TotalRetiros = 4,
            TotalIngresos = 5,
            TotalVentas = 6,
            TotalCompras = 7,
            TotalCuadre = 8,
            idUsuario = 9,
            Usuario = 10,
            idEmpresa = 11,
            Observaciones = 12,
            idCaja = 13,
            Caja = 14,
            idUsuarioCaja = 15,
            Estado = 16
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VerFrmReporteCuadreDiario.GetHashCode().ToString()));
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
            lblUsuario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
            lblUsuario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
            lblCaja.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja);
            CargarCajas(oCIdioma, Idioma);
            CargarUsuarios(oCIdioma, Idioma);
        }

        private void CargarCajas(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblCajaBusiness oCajB = new tblCajaBusiness(CadenaConexion);
                string Opcion = ddlCaja.SelectedValue;
                ddlCaja.Items.Clear();
                ddlCaja.SelectedValue = null;
                ddlCaja.DataSource = oCajB.ObtenerCajaListaPorIdEmpresa(oUsuarioI.idEmpresa);
                ddlCaja.DataBind();
                ddlCaja.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja)), "0"));
                if (!IsPostBack)
                {
                    tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                    tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                    oCuaI.idEmpresa = oUsuarioI.idEmpresa;
                    oCuaI.idUsuarioCaja = oUsuarioI.idUsuario;
                    oCuaI.Estado = true;
                    oCuaI = oCuaB.ObtenerCuadreCajaListaPorUsuario(oCuaI);
                    if (oCuaI.idCuadreCaja > 0)
                    {
                        ddlCaja.SelectedValue = oCuaI.idCaja.ToString();
                    }
                    else
                    {
                        ddlCaja.SelectedValue = "0";
                    }
                    if (!oUsuarioI.EsAdmin)
                    {
                        ddlCaja.Enabled = false;
                    }
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

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblCuadreCajaBusiness oCuaCajB = new tblCuadreCajaBusiness(CadenaConexion);
                string strRutaReporte = "Reportes/ReporteCuadreDiario.rdlc";
                ReportDataSource rdsDatos = new ReportDataSource();
                rdsDatos.Value = oCuaCajB.ObtenerCuadreCajaListaReporte(DateTime.Parse(txtFechaInicial.Text), DateTime.Parse(txtFechaFinal.Text), oUsuarioI.idEmpresa, short.Parse(ddlCaja.SelectedValue), long.Parse(ddlUsuario.SelectedValue));
                rdsDatos.Name = "DataSet1";
                rvPos.LocalReport.ReportEmbeddedResource = strRutaReporte;
                rvPos.LocalReport.ReportPath = strRutaReporte;
                rvPos.LocalReport.DataSources.Clear();
                rvPos.LocalReport.DataSources.Add(rdsDatos);
                rvPos.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los cuadres diarios. {0}", ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
    }
}