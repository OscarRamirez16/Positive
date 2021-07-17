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
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {
        private string CadenaConexion;
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                if (!IsPostBack)
                {
                    CargarDatos(oUsuarioI);
                }
            }
            else
            {
                Response.Redirect("frmInicioSesion.aspx");
            }
        }
        public void CargarDatos(tblUsuarioItem usuario)
        {
            tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
            tblEmpresaItem oEmpI = new tblEmpresaItem();
            oEmpI = oEmpB.ObtenerEmpresa(usuario.idEmpresa);
            lblNombreEmpresa.Text = oEmpI.Nombre;
            lblUsuario.InnerText = usuario.Usuario;
        }
    }
}