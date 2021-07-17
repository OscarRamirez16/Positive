using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioItem;
using InventarioBusiness;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using Idioma;
using HQSFramework.Base;

namespace Inventario.Reportes
{
    public partial class frmReporteMovimientosPorDocumento : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgMovimientosEnum
        {
            numeroDocumento = 1,
            fecha = 2,
            tercero = 3,
            usuario = 4,
            IVA = 5,
            totalPago = 6,
            articulo = 7,
            cantidad = 8,
            valorUnitario = 9,
            totalLinea = 10
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VerFrmMovimientosPorDocumento.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
                            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
                            CargarTipoDocumentos();
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
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.MovimientosDocumentos);
            lblUsuario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
            lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoDocumento);
            lblFechaInical.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaInicial);
            lblFechaFinal.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaFinal);
            lblLinea.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Linea);
            lblArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
            lblBodega.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega);
            CargarLineas(oCIdioma, Idioma);
            CargarUsuarios(oCIdioma, Idioma);
        }

        public void CargarLineas(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblLineaBussines oLineaB = new tblLineaBussines(CadenaConexion);
                string OpcionBus = ddlLineaBus.SelectedValue;
                ddlLineaBus.Items.Clear();
                ddlLineaBus.SelectedValue = null;
                ddlLineaBus.DataSource = oLineaB.ObtenerLineaLista(oUsuarioI.idEmpresa);
                ddlLineaBus.DataBind();
                ddlLineaBus.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Linea)), "0"));
                if (!IsPostBack)
                {
                    ddlLineaBus.SelectedValue = "0";
                }
                else
                {
                    ddlLineaBus.SelectedValue = OpcionBus;
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

        public void CargarTipoDocumentos()
        {
            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
            string Opcion = ddlTipoDocumento.SelectedValue;
            ddlTipoDocumento.Items.Clear();
            ddlTipoDocumento.SelectedValue = null;
            ddlTipoDocumento.DataSource = oDocB.ObtenerTipoDocumentoLista();
            ddlTipoDocumento.DataBind();
            if (!IsPostBack)
            {
                ddlTipoDocumento.SelectedValue = "0";
            }
            else
            {
                ddlTipoDocumento.SelectedValue = Opcion;
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                string strRutaReporte = "Reportes/MovimientosPorDocumento.rdlc";
                ReportDataSource rdsDatos = new ReportDataSource();
                rdsDatos.Value = oDocB.ObtenerMovimientosPorDocumento(long.Parse(ddlUsuario.SelectedValue), oUsuarioI.idEmpresa, DateTime.Parse(txtFechaInicial.Text), DateTime.Parse(txtFechaFinal.Text), long.Parse(ddlTipoDocumento.SelectedValue), txtNombreBus.Text, short.Parse(ddlLineaBus.SelectedValue), long.Parse("0"), long.Parse(hddIdBodega.Value)); ;
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