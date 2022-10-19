using System;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using System.Data;

namespace Inventario
{
    public partial class frmInicioSesion : System.Web.UI.Page
    {
        private string cadenaConexion;
        private enum FacturasPendientesEnum
        {
            Fecha = 0,
            NumeroDocumento = 1,
            Nombre = 2,
            Direccion = 3,
            Telefono = 4,
            Observaciones = 5,
            TotalDocumento = 6,
            saldo = 7,
            TotalAntesIVA = 8
        }

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
                    string Cadena = ConfigurationManager.ConnectionStrings["Backend"].ConnectionString;
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(Cadena);
                    DataTable FacturasPendientes = oDocB.ObtenerFacturasPendientesPorPago(oUsuarioI.IdTercero);
                    if (FacturasPendientes.Rows.Count > 0)
                    {
                        if(FacturasPendientes.Rows.Count >= 3)
                        {
                            Response.Write("<script>alert('Usted tiene pendientes 3 o más facturas, por favor contacte con el administrador - 3147131717');</script>");
                        }
                        else
                        {
                            Session["Usuario"] = oUsuarioI;
                            Response.Redirect("frmMantenimientos.aspx");
                        }
                    }
                    else
                    {
                        Session["Usuario"] = oUsuarioI;
                        Response.Redirect("frmMantenimientos.aspx");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Datos incorrectos');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al intentar conectarse a la BD " + ex.Message.Replace("'", "").Replace(Environment.NewLine, " ") + "');</script>");
            }
        }
    }
}