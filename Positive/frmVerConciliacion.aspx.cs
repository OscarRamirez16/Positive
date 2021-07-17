using System;
using System.Web.UI;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Microsoft.Reporting.WebForms;

namespace Inventario
{
    public partial class frmVerConciliacion : PaginaBase
    {
        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VerAnticipos.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        if (!IsPostBack)
                        {
                            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
                            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerTituloPagina('{1}');", strScript, "Consultar Conciliaciones");
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

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblPagoBusiness oPagB = new tblPagoBusiness(cadenaConexion);
                string strRutaReporte = "Reportes/ReporteConciliaciones.rdlc";
                ReportDataSource rdsDatos = new ReportDataSource();
                rdsDatos.Value = oPagB.ObtenerConciliaciones(DateTime.Parse(txtFechaInicial.Text), DateTime.Parse(txtFechaFinal.Text), txtCliente.Text.Trim(), txtIdentificacion.Text.Trim(), oUsuarioI.idEmpresa);
                rdsDatos.Name = "DataSet1";
                rvPos.LocalReport.ReportEmbeddedResource = strRutaReporte;
                rvPos.LocalReport.ReportPath = strRutaReporte;
                rvPos.LocalReport.DataSources.Clear();
                rvPos.LocalReport.DataSources.Add(rdsDatos);
                rvPos.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los anticipos. {0}", ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
    }
}