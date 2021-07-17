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
    /// Descripción breve de Ciudad
    /// </summary>
    public class Ciudad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            string ValorBusqueda = context.Request.QueryString["ValorBusqueda"];
            string data;
            tblCiudadBusiness oCiudadB = new tblCiudadBusiness(CadenaConexion);
            List<JSONItem> lista = oCiudadB.ObtenerCiudadListaPorNombre(ValorBusqueda);
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