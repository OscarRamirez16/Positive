using System.Collections.Generic;
using System.Web;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;

namespace Inventario.Ashx
{
    /// <summary>
    /// Descripción breve de Articulo
    /// </summary>
    public class Articulo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private enum opciones
        {
            buscarPorNombre = 1,
            buscarPorCodigoOCodigoBarra = 2,
            BuscarSencillo = 3,
            BuscarPorNombreSencilloEnBodegas = 4,
            BuscarArticuloCompuesto = 5,
            BuscarArticuloFinal = 6
        }

        public void ProcessRequest(HttpContext context)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            string ValorBusqueda = context.Request.QueryString["ValorBusqueda"];
            string opcion = context.Request.QueryString["opcion"];
            long idEmpresa = long.Parse(context.Request.QueryString["i_e"]);
            long tipoDocumento = 0;
            long idBodega = 0;
            long idTercero = 0;
            if(!string.IsNullOrEmpty(context.Request.QueryString["tipoFactura"]))
            {
                tipoDocumento = long.Parse(context.Request.QueryString["tipoFactura"]);
            }
            if(!string.IsNullOrEmpty(context.Request.QueryString["i_b"]))
            {
                idBodega = long.Parse(context.Request.QueryString["i_b"]);
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["i_t"]) && context.Request.QueryString["i_t"] != "undefined")
            {
                idTercero = long.Parse(context.Request.QueryString["i_t"]);
            }
            string data;
            tblArticuloBusiness oArticuloB = new tblArticuloBusiness(CadenaConexion);
            List<JSONItem> lista;
            if (opcion == opciones.buscarPorNombre.GetHashCode().ToString())
            {
                lista = oArticuloB.ObtenerArticuloListaPorNombre(ValorBusqueda, idEmpresa, tipoDocumento, idBodega, idTercero);
            }
            else if (opcion == opciones.buscarPorCodigoOCodigoBarra.GetHashCode().ToString())
            {
                lista = oArticuloB.ObtenerArticuloListaPorCodigoOCodigoBarras(ValorBusqueda, idEmpresa, tipoDocumento, idBodega, idTercero);
            }
            else if (opcion == opciones.BuscarArticuloCompuesto.GetHashCode().ToString())
            {
                lista = oArticuloB.ObtenerArticuloListaPorNombreSencillo(ValorBusqueda, idEmpresa, int.Parse(opcion));
            }
            else if (opcion == opciones.BuscarArticuloFinal.GetHashCode().ToString())
            {
                lista = oArticuloB.ObtenerArticuloListaPorNombreSencillo(ValorBusqueda, idEmpresa, int.Parse(opcion));
            }
            else
            {
                lista = oArticuloB.ObtenerArticuloListaPorNombreSencillo(ValorBusqueda, idEmpresa, int.Parse(opcion));
            }
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.ContentType = "application/json";
            data = jss.Serialize(lista);
            context.Response.Write(data);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}