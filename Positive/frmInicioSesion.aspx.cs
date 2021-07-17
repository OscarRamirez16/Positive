using System;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;

namespace Inventario
{
    public partial class frmInicioSesion : System.Web.UI.Page
    {
        private string cadenaConexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("Usuario");
                tblUsuarioBusiness oUsuarioB = new tblUsuarioBusiness(cadenaConexion);
                tblUsuarioItem oUsuarioI = new tblUsuarioItem();
                oUsuarioI = oUsuarioB.buscarUsuarioPorLoginPassword(txtLogin.Text, txtPassword.Text);
                if (oUsuarioI != null && oUsuarioI.idUsuario != 0)
                {
                    Session["Usuario"] = oUsuarioI;
                    Response.Redirect("frmMantenimientos.aspx");
                    //Response.Redirect("frmDocumentoResponsive.aspx?opcionDocumento=1");
                }
                else
                {
                    Response.Write("<script>alert('Datos incorrectos');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al intentar conectarse a la BD " + ex.Message + "');</script>");
            }
        }
    }
}