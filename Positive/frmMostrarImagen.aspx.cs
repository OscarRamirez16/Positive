using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using System.Configuration;

namespace Inventario
{
    public partial class frmMostrarImagen : PaginaBase
    {
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (Util.EsEntero(Request.QueryString["IdVentaRapida"]) && oUsuarioI != null)
            {
                long IdVentaRapida = long.Parse(Request.QueryString["IdVentaRapida"]);
                string CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
                tblVentaRapidaItem oVRItem = oDBiz.ObtenerVentaRapida(IdVentaRapida, oUsuarioI.idEmpresa);
                if (oVRItem.Imagen != null && oVRItem.Imagen.Length > 0) {
                    System.IO.MemoryStream msImagen = new System.IO.MemoryStream();
                    msImagen.Write(oVRItem.Imagen, 0, oVRItem.Imagen.Length);
                    System.Drawing.Bitmap bmImagen = new System.Drawing.Bitmap(msImagen);
                    Response.ContentType = "image/png";
                    bmImagen.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
}