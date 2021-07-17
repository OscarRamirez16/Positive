using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using HQSFramework.Base;
using Idioma;

namespace Inventario
{
    public partial class frmReporteArticulosPorBodega : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgArticulosColumnsEnum
        {
            codigo = 1,
            nombre = 2,
            presentacion = 3,
            cantidad = 4,
            bodega = 5
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VerFrmReporteArticulosPorBodega.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','1');", strScript, txtBodega.ClientID, hddIdBodega.ClientID, oUsuarioI.idEmpresa);
                            strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}');", strScript, txtProveedorBus.ClientID, hddIdProBus.ClientID);
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
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ArticulosBodega);
            lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Proveedor);
            lblLinea.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Linea);
            lblArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
            lblBodega.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega);
            CargarLineas(oCIdioma, Idioma);
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

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                string strRutaReporte = "Reportes/ArticulosPorBodega.rdlc";
                ReportDataSource rdsDatos = new ReportDataSource();
                rdsDatos.Value = oArtB.ObtenerArticulosPorBodega(oUsuarioI.idEmpresa, long.Parse(hddIdBodega.Value), txtNombreBus.Text, short.Parse(ddlLineaBus.SelectedValue), long.Parse(hddIdProBus.Value));
                rdsDatos.Name = "DataSet1";
                rvPos.LocalReport.ReportEmbeddedResource = strRutaReporte;
                rvPos.LocalReport.ReportPath = strRutaReporte;
                rvPos.LocalReport.DataSources.Clear();
                rvPos.LocalReport.DataSources.Add(rdsDatos);
                rvPos.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los artículos por bodega. {0}", ex.Message));
            }
        }
    }
}