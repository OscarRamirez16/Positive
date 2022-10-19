using HQSFramework.Base;
using InventarioBusiness;
using InventarioItem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Positive
{
    public partial class MisPedidos : PaginaBase
    {
        private string cadenaConexion;
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        public enum dgDocumentosEnum
        {
            idDocumento = 0,
            numDocumento = 1,
            fecha = 2,
            nombre = 3,
            Observaciones = 4,
            seleccionar = 5
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    CargarDocumentos();
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
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagina. {0}", ex.Message));
            }
        }

        public void CargarDocumentos()
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(cadenaConexion);
                dgDocumentos.DataSource = oDocB.ObtenerCotizacionVentaRapidaLista(oUsuarioI.idUsuario);
                dgDocumentos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void dgDocumentos_EditCommand(object source, DataGridCommandEventArgs e)
        {
            Response.Redirect("frmVentaRapida.aspx?IdTipoDocumento=3&idDocumento=" + e.Item.Cells[dgDocumentosEnum.idDocumento.GetHashCode()].Text);
        }
    }
}