using HQSFramework.Base;
using InventarioBusiness;
using InventarioItem;
using System;
using System.Configuration;

namespace Positive
{
    public partial class frmRealizarPago : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.PagoMensualidad.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {
                    if (!IsPostBack)
                    {
                        CargarDocumentosPendientesPago();
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

        private void CargarDocumentosPendientesPago()
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Backend"].ConnectionString;
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(Cadena);
                dgFacturas.DataSource = oDocB.ObtenerFacturasPendientesPorPago(oUsuarioI.IdTercero);
                dgFacturas.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}