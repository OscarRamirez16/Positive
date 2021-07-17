using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using InventarioBusiness;
using InventarioItem;

namespace Inventario.Ashx
{
    /// <summary>
    /// Descripción breve de Bodega
    /// </summary>
    public class Bodega : IHttpHandler
    {

        private string CadenaConexion = "";

        private enum opcionesBusqueda
        {
            CrearArticulos = 1,
            Documentos = 2,
            PreciosPorBodega = 3
        }

        public void ProcessRequest(HttpContext Context)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            if (Context.Request.QueryString["opcion"] == opcionesBusqueda.CrearArticulos.GetHashCode().ToString())
            {
                Context.Response.Write(ObtenerBodegaListaPorNombre(Context));
            }
            if (Context.Request.QueryString["opcion"] == opcionesBusqueda.Documentos.GetHashCode().ToString())
            {
                Context.Response.Write(ObtenerBodegaListaPorNombreCantidad(Context));
            }
            if (Context.Request.QueryString["opcion"] == opcionesBusqueda.PreciosPorBodega.GetHashCode().ToString())
            {
                Context.Response.Write(ObtenerPreciosPorBodega(Context));
            }
        }

        private string ObtenerBodegaListaPorNombre(HttpContext Context)
        {
            string data;
            string ValorBusqueda = Context.Request.QueryString["ValorBusqueda"];
            long idEmpresa = long.Parse(Context.Request.QueryString["i_e"]);
            tblBodegaBusiness oBodegaB = new tblBodegaBusiness(CadenaConexion);
            List<JSONItem> lista = oBodegaB.ObtenerBodegaListaPorNombre(ValorBusqueda, idEmpresa);
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Context.Response.ContentType = "application/json";
            data = jss.Serialize(lista);
            return data;
        }

        private string ObtenerPreciosPorBodega(HttpContext Context)
        {
            string data = "";
            if (string.IsNullOrEmpty(Context.Request.QueryString["IdArticulo"]))
            {
                data = "No se ha seleccionado un artículo.";
            }
            else
            {
                long IdArticulo = long.Parse(Context.Request.QueryString["IdArticulo"]);
                long IdBodega = long.Parse(Context.Request.QueryString["IdBodega"]);
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                List<tblPreciosPorBodegaItem> oListPreBod = new List<tblPreciosPorBodegaItem>();
                oListPreBod = oBodB.ObtenerPreciosPorBodegaVenta(IdArticulo, IdBodega);
                bool Validar = false;
                data = "<table style='width:100%'><tr><td style='text-align:center; width:35%'><b>Descripción</b></td><td style='text-align:center; width:35%'><b>Precio</b></td><td></td></tr>";
                foreach (tblPreciosPorBodegaItem Item in oListPreBod)
                {
                    if (string.IsNullOrEmpty(Item.Descripcion))
                    {
                        Validar = true;
                    }
                    else 
                    {
                        data = string.Format("{0}<tr><td>{1}</td><td>{2}</td><td style='text-align:center'><a href='#' onclick='SeleccionarPrecio({2});'>Seleccionar</a></td></tr>", data, Item.Descripcion, Item.Valor.ToString().Replace(',', '.'));
                    }
                }
                if (Validar)
                {
                    data = "El artículo no tiene precios configurados";
                }
                else
                {
                    data = string.Format("{0}</table>", data);
                }
            }
            return data;
        }

        private string ObtenerBodegaListaPorNombreCantidad(HttpContext Context)
        {
            string data;
            string ValorBusqueda = Context.Request.QueryString["ValorBusqueda"];
            long idEmpresa = long.Parse(Context.Request.QueryString["i_e"]);
            long idArticulo = 0;
            if (!string.IsNullOrEmpty(Context.Request.QueryString["i_a"]))
            {
                idArticulo = long.Parse(Context.Request.QueryString["i_a"]);
            }
            tblBodegaBusiness oBodegaB = new tblBodegaBusiness(CadenaConexion);
            List<JSONItem> lista = oBodegaB.ObtenerBodegaListaPorNombreCantidad(ValorBusqueda, idEmpresa, idArticulo);
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Context.Response.ContentType = "application/json";
            data = jss.Serialize(lista);
            return data;
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