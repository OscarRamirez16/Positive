using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using HQSFramework.Base;
using System;
using System.Web.UI.WebControls;

namespace Inventario
{
    public partial class frmReporteAuditoriaStock : PaginaBase
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.ReporteAuditoriaStock.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaInicial.ClientID);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaFinal.ClientID);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                        if (!IsPostBack)
                        {
                            CargarBodegas();
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

        private void CargarBodegas()
        {
            try
            {
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                ddlBodegas.DataSource = oBodB.ObtenerBodegaLista(oUsuarioI.idEmpresa);
                ddlBodegas.DataBind();
                ddlBodegas.Items.Add(new ListItem("Todas", "0"));
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar las bodegas. {0}", ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void btnBuscar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItemCode.Text))
                {
                    MostrarMensaje("Error", "Por favor digita un ItemCode.");
                }
                else
                {
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                    string strRutaReporte = "Reportes/ReporteAuditoriaStock.rdlc";
                    ReportDataSource rdsDatos = new ReportDataSource();
                    rdsDatos.Value = oArtB.ObtenerAuditoriaStock(DateTime.Parse(txtFechaInicial.Text), DateTime.Parse(txtFechaFinal.Text), oUsuarioI.idEmpresa, txtItemCode.Text.Trim(), long.Parse(ddlBodegas.SelectedValue));
                    rdsDatos.Name = "DataSet1";
                    rvPos.LocalReport.ReportEmbeddedResource = strRutaReporte;
                    rvPos.LocalReport.ReportPath = strRutaReporte;
                    rvPos.LocalReport.DataSources.Clear();
                    rvPos.LocalReport.DataSources.Add(rdsDatos);
                    rvPos.LocalReport.Refresh();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los artículos por bodega. {0}", ex.Message));
            }
        }
    }
}