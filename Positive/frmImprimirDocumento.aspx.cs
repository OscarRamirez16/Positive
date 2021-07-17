using System;
using System.Collections.Generic;
using InventarioBusiness;
using Microsoft.Reporting.WebForms;
using HQSFramework.Base;
using InventarioItem;
using System.Configuration;

namespace Inventario
{
    public partial class frmImprimirDocumento : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        SeguridadBusiness.paginasEnum PaginaID;
        private long IdDocumento = 0;
        int TipoDocumento = 0;
        string Documento = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
            long.TryParse(Request.QueryString["IdDocumento"], out IdDocumento);
            int.TryParse(Request.QueryString["TipoDocumento"], out TipoDocumento);
            Documento = ObtenerNombreDocumento();
            if (oUsuarioI != null) {
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(PaginaID.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {

                    if (IdDocumento > 0 && TipoDocumento > 0 && !IsPostBack)
                    {
                        try
                        {
                            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                            tblEmpresaBusiness oEBiz = new tblEmpresaBusiness(CadenaConexion);
                            tblDocumentoItem oDocI = new tblDocumentoItem();
                            List<tblDocumentoItem> oDList = new List<tblDocumentoItem>();
                            oDocI = oDocB.traerDocumentoPorId(TipoDocumento, IdDocumento);
                            oDList.Add(oDocI);
                            tblEmpresaItem oEItem = oEBiz.ObtenerEmpresa(oUsuarioI.idEmpresa);
                            List<tblEmpresaItem> oEList = new List<tblEmpresaItem>();
                            oEList.Add(oEItem);
                            tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                            tblTerceroItem oTItem = oTBiz.ObtenerTercero(oDocI.idTercero, oUsuarioI.idEmpresa);
                            List<tblTerceroItem> oTList = new List<tblTerceroItem>();
                            oTList.Add(oTItem);
                            string strRutaReporte = "Reportes/Documento.rdlc";
                            ReportParameter rpDocumentoNombre = new ReportParameter("rpDocumentoNombre", Documento);
                            ReportDataSource rdsEmpresa = new ReportDataSource();
                            rdsEmpresa.Value = oEList;
                            rdsEmpresa.Name = "DataSet1";
                            ReportDataSource rdsDocumento = new ReportDataSource();
                            rdsDocumento.Value = oDList;
                            rdsDocumento.Name = "DataSet2";
                            ReportDataSource rdsTercero = new ReportDataSource();
                            rdsTercero.Value = oTList;
                            rdsTercero.Name = "DataSet3";
                            ReportDataSource rdsDocumentoDetalle = new ReportDataSource();
                            rdsDocumentoDetalle.Value = oDocI.DocumentoLineas;
                            rdsDocumentoDetalle.Name = "DataSet4";
                            ReportDataSource rdsFormaPago = new ReportDataSource();
                            rdsFormaPago.Value = oDocI.FormasPago;
                            rdsFormaPago.Name = "DataSet5";
                            rvPos.LocalReport.ReportEmbeddedResource = strRutaReporte;
                            List<ReportParameter> parameters = new List<ReportParameter>();
                            parameters.Add(rpDocumentoNombre);
                            rvPos.LocalReport.ReportPath = strRutaReporte;
                            rvPos.LocalReport.DataSources.Clear();
                            rvPos.LocalReport.DataSources.Add(rdsEmpresa);
                            rvPos.LocalReport.DataSources.Add(rdsDocumento);
                            rvPos.LocalReport.DataSources.Add(rdsTercero);
                            rvPos.LocalReport.DataSources.Add(rdsDocumentoDetalle);
                            rvPos.LocalReport.DataSources.Add(rdsFormaPago);
                            rvPos.LocalReport.SetParameters(parameters);
                            rvPos.LocalReport.Refresh();
                        }
                        catch (Exception ex)
                        {
                            MostrarMensaje("Error", string.Format("No se pudo cargar los movimientos por documentos. {0}", ex.Message));
                        }
                    }
                }
            }
        }

        public string ObtenerNombreDocumento()
        {
            string Documento = "";
            if (tblTipoDocumentoItem.TipoDocumentoEnum.venta.GetHashCode() == TipoDocumento)
            {
                PaginaID = SeguridadBusiness.paginasEnum.VerVentas;
                Documento = "Factura de venta";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.compra.GetHashCode() == TipoDocumento)
            {
                PaginaID = SeguridadBusiness.paginasEnum.VerCompras;
                Documento = "Factura de compra";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.cotizacion.GetHashCode() == TipoDocumento)
            {
                PaginaID = SeguridadBusiness.paginasEnum.VerCotizaciones;
                Documento = "Cotización";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.notaCreditoVenta.GetHashCode() == TipoDocumento)
            {
                PaginaID = SeguridadBusiness.paginasEnum.NotaCreditoVenta;
                Documento = "Nota Crédito Venta";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.entradaMercancia.GetHashCode() == TipoDocumento)
            {
                PaginaID = SeguridadBusiness.paginasEnum.EntradaMercancia;
                Documento = "Entrada Mercancía";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.salidaMercancia.GetHashCode() == TipoDocumento)
            {
                PaginaID = SeguridadBusiness.paginasEnum.SalidaMercancia;
                Documento = "Salida Mercancía";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.notaCreditoCompra.GetHashCode() == TipoDocumento)
            {
                PaginaID = SeguridadBusiness.paginasEnum.NotaCreditoCompra;
                Documento = "Nota Crédito Compra";
            }
            if (tblTipoDocumentoItem.TipoDocumentoEnum.Remision.GetHashCode() == TipoDocumento)
            {
                PaginaID = SeguridadBusiness.paginasEnum.ConsultarRemisiones;
                Documento = "Remisión";
            }
            return Documento;
        }
    }
}