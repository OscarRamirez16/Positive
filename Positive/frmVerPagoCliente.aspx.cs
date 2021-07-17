using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using HQSFramework.Base;

namespace Inventario
{
    public partial class VerPagoCliente : PaginaBase
    {

        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (oUsuarioI != null)
                {
                    cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    oUsuarioI = (tblUsuarioItem)(Session["usuario"]);
                    if (!IsPostBack)
                    {
                        if (Request.QueryString["idPago"] != null)
                        {
                            cargarDetallesPago(long.Parse(Request.QueryString["idPago"]));
                        }
                    }
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                    }
                }
                else
                {
                    Response.Redirect("frmInicioSesion.aspx");
                }
            }
            catch 
            {
                Response.Write("<script>alert('Error al cargar la pagina de ver detalles pago');</script>");
            }
        }

        private void cargarDetallesPago(long idPago)
        {
            try
            {
                tblPagoBusiness oPagoB = new tblPagoBusiness(cadenaConexion);
                dgPagos.DataSource = oPagoB.ObtenerDetallesPagoPorIDPago(idPago);
                dgPagos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los detalles del pago. {0}", ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
    }
}