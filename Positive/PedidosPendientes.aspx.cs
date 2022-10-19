using HQSFramework.Base;
using InventarioBusiness;
using InventarioItem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Positive
{
    public partial class PedidosPendientes : PaginaBase
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
                            if (!IsPostBack)
                            {
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
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }
        private void InicializarControles()
        {
            btnGuardar.Style.Add("display", "none");
            btnGuardar.Style.Add("visibility", "hidden");
            btnActualizar.Style.Add("display", "none");
            btnActualizar.Style.Add("visibility", "hidden");
        }
        private List<long> ObtenerDocumentos(List<CotizacionCocinaItem> pedidos)
        {
            List<long> Documentos = new List<long>();
            long Documento = 0;
            foreach (CotizacionCocinaItem p in pedidos)
            {
                if (Documento != p.idCotizacion)
                {
                    Documentos.Add(p.idCotizacion);
                    Documento = p.idCotizacion;
                }
            }
            return Documentos;
        }
        private void PintarPedidos()
        {
            string Pedidos = "";
            tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
            List<CotizacionCocinaItem> oCCLista = oDBiz.ObtenerCotizacionCocinaLista(oUsuarioI.idEmpresa);
            int DocumentoActual = 0;
            foreach (int idDocumento in ObtenerDocumentos(oCCLista).Distinct())
            {
                if (DocumentoActual == 0 || DocumentoActual % 4 == 0) {
                    if (string.IsNullOrEmpty(Pedidos))
                    {
                        Pedidos = "<div class='row'>";
                    }
                    else {
                        Pedidos = $"{Pedidos}</div><div class='row'>";
                    }
                }

                bool IsFirstItem = true;
                string footer = "";
                List<CotizacionCocinaDataItem> parameterData = new List<CotizacionCocinaDataItem>();
                foreach (CotizacionCocinaItem p in oCCLista.Where(s => s.idCotizacion == idDocumento)) {
                    if (IsFirstItem) {
                        Pedidos = $"{Pedidos}<div class='col-md-3'><div class='card'><li class='list-group-item liHeader'>Pedido No. {p.NumeroDocumento} - {p.Vendedor}<img src='Images/entregarpedido.png' onclick='EntregarCotizacionCocina(`||parameterData||`);' class='delivery-order' alt='Entregar pedido no. {p.NumeroDocumento}' title='Entregar pedido no. {p.NumeroDocumento}' /> </li><ul class='list-group list-group-flush'>";
                        footer = $"<li class='list-group-item liFooter'><p>{p.Observaciones}</p>By <b>{p.Usuario}</b></li></ul></div></div>";
                        IsFirstItem = false;
                    }
                    List<CotizacionCocinaDataItem> itemdata = new List<CotizacionCocinaDataItem>();
                    parameterData.Add(new CotizacionCocinaDataItem() { idCotizacion = p.idCotizacion, idArticulo = p.idArticulo, Cantidad = p.Cantidad});
                    itemdata.Add(new CotizacionCocinaDataItem() { idCotizacion = p.idCotizacion, idArticulo = p.idArticulo, Cantidad = p.Cantidad});
                    Pedidos = $"{Pedidos}<li class='list-group-item'>{p. Cantidad} - {p.Nombre}<img src='Images/entregarpedido.png' onclick='EntregarCotizacionCocina(`{JsonConvert.SerializeObject(itemdata)}`)' class='delivery-order-item' alt='Entregar {p.Cantidad} - {p.Nombre}' title='Entregar {p.Cantidad} - {p.Nombre}' /></li>";
                }
                Pedidos = $"{Pedidos}{footer}";
                Pedidos = Pedidos.Replace("||parameterData||", JsonConvert.SerializeObject(parameterData));
                DocumentoActual = DocumentoActual + 1;
            }
            Pedidos = $"{Pedidos}</div>";
            divPedidos.InnerHtml = Pedidos;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hddPedido.Value)) {
                List<CotizacionCocinaDataItem> parameterData = JsonConvert.DeserializeObject<List<CotizacionCocinaDataItem>>(hddPedido.Value);
                tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
                foreach (var p in parameterData) {
                    oDBiz.Guardar(new CotizacionCocinaItem() { idCotizacion =  p.idCotizacion, idArticulo = p.idArticulo, Cantidad = p.Cantidad, Fecha = DateTime.Now});
                }
            }
            PintarPedidos();
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            PintarPedidos();
        }
    }
}