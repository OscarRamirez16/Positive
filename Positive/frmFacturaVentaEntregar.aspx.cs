using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using Idioma;
using HQSFramework.Base;
using System.Globalization;
using System.Data;

namespace Inventario
{
    public partial class frmFacturaVentaEntregar : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.frmFacturaVentaEntregar.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                            if (!IsPostBack) {
                                PintarPedidos();
                            }
                            InicializarControles();
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
        private void InicializarControles() {
            btnGuardar.Style.Add("display", "none");
            btnGuardar.Style.Add("visibility", "hidden");
            btnActualizar.Style.Add("display", "none");
            btnActualizar.Style.Add("visibility", "hidden");
        }
        private List<int> ObtenerDocumentos(DataTable dtPedidos) {
            List<int> Documentos = new List<int>();
            int Documento = 0;
            foreach (DataRow drItem in dtPedidos.Rows)
            {
                if (Documento.ToString() != drItem["idDocumento"].ToString()) {
                    Documentos.Add(int.Parse(drItem["idDocumento"].ToString()));
                    Documento = int.Parse(drItem["idDocumento"].ToString());
                }
            }
            return Documentos;
        }
        private void PintarPedidos() {
            int NumPedidos = 4;
            int Ancho = 100 / NumPedidos;
            string Pedidos = "<table style='width:100%'><tr>";
            string PedidoEncabezadoPlantilla = "<td style='width:{1}%;padding:10px;vertical-align:top'><div class = \"list-group\"><a href=\"#\" onclick=\"EntregarPedido('{2}','{3}','{4}');\" style=\"text-align:center;\" class=\"list-group-item active\"><b style=\"font-size:medium;\">{0}</a>";
            string PedidoDetallePlantilla = "<a href=\"#\" class=\"list-group-item\">&nbsp;{0},&nbsp;&nbsp;Cantidad:&nbsp;{1}</a>";
            tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
            DataTable dtPedidos = oDBiz.FacturasPendienteEntrega(oUsuarioI.idEmpresa);
            int DocumentoActual = 0;
            int PedidosPorLinea = 0;
            foreach (int idDocumento in ObtenerDocumentos(dtPedidos)) {
                bool Encabezado = true;
                PedidosPorLinea = PedidosPorLinea + 1;
                if (DocumentoActual != 0)
                {
                    Pedidos = string.Format("{0}</div></td>", Pedidos);
                }
                DocumentoActual = idDocumento;
                if (PedidosPorLinea > 4)
                {
                    PedidosPorLinea = 1;
                    Pedidos = string.Format("{0}</tr><tr>", Pedidos);
                }
                foreach (DataRow item in dtPedidos.Select(string.Format("idDocumento = {0}", idDocumento)))
                {
                    if (Encabezado)
                    {
                        Pedidos = string.Format("{0}{1}", Pedidos, string.Format(PedidoEncabezadoPlantilla, item["NumeroDocumento"],Ancho,btnGuardar.ClientID,hddPedido.ClientID, item["idDocumento"]));
                        Encabezado = false;
                    }
                    Pedidos = string.Format("{0}{1}", Pedidos, string.Format(PedidoDetallePlantilla, item["Descripcion"], item["Cantidad"]));
                }
            }
            if (Pedidos != "<tr>") {
                Pedidos = string.Format("{0}</div></td></tr></table>", Pedidos);
                tdFacturas.InnerHtml = Pedidos;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hddPedido.Value) && hddPedido.Value != "0") {
                tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
                if (!oDBiz.ActualizarFacturaEntrega(long.Parse(hddPedido.Value)))
                {
                    MostrarMensaje("Entregar Pedido", string.Format("Error al Entregar el Pedido {0}", hddPedido.Value));
                }
                else {
                    PintarPedidos();
                }
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            PintarPedidos();
        }
    }
}