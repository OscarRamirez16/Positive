using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;

namespace Inventario.Ashx
{
    /// <summary>
    /// Descripción breve de Tercero
    /// </summary>
    public class Tercero : IHttpHandler
    {

        long idEmpresa = 0;

        private enum tipoDocumento
        {
            todos = 0,
            venta = 1,
            compra = 2,
            cotizacion = 3,
            notaCreditoVenta = 4,
            entradaMercancia = 5,
            salidaMercancia = 6,
            notaCreditoCompra = 7,
            remision = 8,
            ordenFabricacion = 9,
            CuentaCobro = 10,
            FacturaElectronica = 11
        }

        public void ProcessRequest(HttpContext context)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            string ValorBusqueda = context.Request.QueryString["ValorBusqueda"];
            idEmpresa = long.Parse(context.Request.QueryString["i_e"]);
            string tipoFactura = context.Request.QueryString["tipoFactura"];
            if (tipoFactura == tipoDocumento.venta.GetHashCode().ToString() || tipoFactura == tipoDocumento.cotizacion.GetHashCode().ToString() || tipoFactura == tipoDocumento.notaCreditoVenta.GetHashCode().ToString() || tipoFactura == tipoDocumento.remision.GetHashCode().ToString() || tipoFactura == tipoDocumento.CuentaCobro.GetHashCode().ToString() || tipoFactura == tipoDocumento.FacturaElectronica.GetHashCode().ToString())
            {
                context.Response.Write(obternerClienteListaNombre(ValorBusqueda, context, CadenaConexion));
            }
            if (tipoFactura == tipoDocumento.compra.GetHashCode().ToString() || tipoFactura == tipoDocumento.notaCreditoCompra.GetHashCode().ToString() || tipoFactura == tipoDocumento.entradaMercancia.GetHashCode().ToString() || tipoFactura == tipoDocumento.salidaMercancia.GetHashCode().ToString())
            {
                context.Response.Write(obternerProveedorListaNombre(ValorBusqueda, context, CadenaConexion));
            }
            if (tipoFactura == tipoDocumento.todos.GetHashCode().ToString())
            {
                context.Response.Write(obternerTerceroListaNombre(ValorBusqueda, context, CadenaConexion));
            }
        }

        private string obternerClienteListaNombre(string valorBusqueda, HttpContext context, string cadenaConexion)
        {
            string data;
            tblTerceroBusiness oTerceroB = new tblTerceroBusiness(cadenaConexion);
            List<JSONItem> lista = oTerceroB.ObtenerTerceroListaPorNombre(valorBusqueda, "C", idEmpresa);
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.ContentType = "application/json";
            data = jss.Serialize(lista);
            return data;
        }

        private string obternerProveedorListaNombre(string valorBusqueda, HttpContext context, string cadenaConexion)
        {
            string data;
            tblTerceroBusiness oTerceroB = new tblTerceroBusiness(cadenaConexion);
            List<JSONItem> lista = oTerceroB.ObtenerTerceroListaPorNombre(valorBusqueda, "P", idEmpresa);
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.ContentType = "application/json";
            data = jss.Serialize(lista);
            return data;
        }

        private string obternerTerceroListaNombre(string valorBusqueda, HttpContext context, string cadenaConexion)
        {
            string data;
            tblTerceroBusiness oTerceroB = new tblTerceroBusiness(cadenaConexion);
            List<JSONItem> lista = oTerceroB.ObtenerTerceroListaPorNombre(valorBusqueda, "-1", idEmpresa);
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.ContentType = "application/json";
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