using System;
using System.Web.UI;
using InventarioItem;
using InventarioBusiness;
using HQSFramework.Base;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using Idioma;
using System.Web.UI.WebControls;

namespace Positive
{
    public partial class frmReporteDocumentosAgrupadoPorArticulo : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VerVentas.GetHashCode().ToString()));
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
                            strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','1');", strScript, txtBodega.ClientID, hddIdBodega.ClientID, oUsuarioI.idEmpresa);
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
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
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
            lblBodega.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega);
            CargarUsuarios(oCIdioma, Idioma);
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
        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtFechaInicial.Text) || string.IsNullOrEmpty(txtFechaFinal.Text) || string.IsNullOrEmpty(txtFechaFinal.Text) || hddIdBodega.Value == "0")
                {
                    MostrarAlerta(0, "Error", "Las fechas y la bodega son obligatorios.");
                }
                else
                {
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                    string strRutaReporte = "Reportes/ReporteDocumentosAgrupadoPorArticulo.rdlc";
                    ReportDataSource rdsDatos = new ReportDataSource();
                    rdsDatos.Value = oDocB.ObtenerDocumentosAgrupadoPorArticulo(DateTime.Parse(txtFechaInicial.Text), DateTime.Parse(txtFechaFinal.Text), long.Parse(ddlUsuario.SelectedValue), oUsuarioI.idEmpresa, tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode(), long.Parse(hddIdBodega.Value));
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
                MostrarAlerta(0, "Error", string.Format("No se pudo cargar el reporte. {0}", ex.Message));
            }
        }
    }
}