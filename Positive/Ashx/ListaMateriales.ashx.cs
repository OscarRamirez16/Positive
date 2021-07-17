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
    /// Summary description for ListaMateriales
    /// </summary>
    public class ListaMateriales : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            string ValorBusqueda = context.Request.QueryString["ValorBusqueda"];
            long IdEmpresa = long.Parse(context.Request.QueryString["i_e"]);
            string data;
            tblListaMaterialesBusiness oListB = new tblListaMaterialesBusiness(CadenaConexion);
            List<JSONItem> lista = oListB.ObtenerListaMaterialesPorIdEmpresa(ValorBusqueda, IdEmpresa);
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