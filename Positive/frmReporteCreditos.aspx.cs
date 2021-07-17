using HQSFramework.Base;
using InventarioBusiness;
using InventarioItem;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Web.UI;

namespace Inventario
{
    public partial class frmReporteCreditos : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.DocumentosACredito.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
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
                            strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}');", strScript, txtCliente.ClientID, hddIdCliente.ClientID);
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
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                string strRutaReporte = "";
                strRutaReporte = "Reportes/ReporteDocumentosACredito.rdlc";
                ReportDataSource rdsDatos = new ReportDataSource();
                rdsDatos.Value = oDocB.ObtenerDocumentosACredito(DateTime.Parse(txtFechaInicial.Text), DateTime.Parse(txtFechaFinal.Text), long.Parse(hddIdCliente.Value), txtIdentificacion.Text, oUsuarioI.idEmpresa);
                rdsDatos.Name = "DataSet1";
                rvPos.LocalReport.ReportEmbeddedResource = strRutaReporte;
                rvPos.LocalReport.ReportPath = strRutaReporte;
                rvPos.LocalReport.DataSources.Clear();
                rvPos.LocalReport.DataSources.Add(rdsDatos);
                rvPos.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los movimientos por documentos. {0}", ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
    }
}