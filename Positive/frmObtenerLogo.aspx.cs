using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;

namespace Inventario
{
    public partial class frmObtenerLogo : System.Web.UI.Page
    {
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                string CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                tblEmpresaBusiness oEBiz = new tblEmpresaBusiness(CadenaConexion);
                tblEmpresaItem oEItem = oEBiz.ObtenerEmpresa(oUsuarioI.idEmpresa);
                if (oEItem.Logo != null && oEItem.Logo.Length > 0)
                {
                    System.IO.MemoryStream msImagen = new System.IO.MemoryStream();
                    msImagen.Write(oEItem.Logo, 0, oEItem.Logo.Length);
                    System.Drawing.Bitmap bmImagen = new System.Drawing.Bitmap(msImagen);
                    Response.ContentType = "image/png";
                    bmImagen.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            
        }
    }
}