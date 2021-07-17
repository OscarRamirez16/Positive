using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using HQSFramework.Base;
using Idioma;

namespace Inventario
{
    public partial class frmVerUsuarios : PaginaBase
    {

        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        enum dgUsuariosColumnsEnum
        {
            idUsuario = 0,
            Identificacion = 1,
            nombreCompleto = 2,
            usuario = 3,
            contrasena = 4,
            Empresa = 5,
            telefonoCelular = 6,
            direccion = 7,
            correo = 8
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI.idUsuario != 0)
                {
                    cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VerUsuarios.GetHashCode().ToString()));
                    if (!oRolPagI.Leer)
                    {
                        Response.Redirect("frmMantenimientos.aspx");
                    }
                    else
                    {
                        ConfiguracionIdioma();
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }
                }
                else
                {
                    Response.Redirect("frmInicioSesion.aspx");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagina. {0}", ex.Message));
            }
        }

        private void ConfiguracionIdioma()
        {
            Traductor oCIdioma = new Traductor();
            short Opcion;
            Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
            if (string.IsNullOrEmpty(Request.Form["ctl00$ddlIdiomas"]))
            {
                Opcion = oUsuarioI.IdIdioma;
            }
            else
            {
                Opcion = short.Parse(Request.Form["ctl00$ddlIdiomas"]);
            }
            if (Opcion == Traductor.IdiomaEnum.Espanol.GetHashCode())
            {
                Idioma = Traductor.IdiomaEnum.Espanol;
            }
            if (Opcion == Traductor.IdiomaEnum.Ingles.GetHashCode())
            {
                Idioma = Traductor.IdiomaEnum.Ingles;
            }
            if (Opcion == Traductor.IdiomaEnum.Aleman.GetHashCode())
            {
                Idioma = Traductor.IdiomaEnum.Aleman;
            }
            lblTitulo.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lista), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuarios));
            lblPrimerNombre.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Primer), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            txtPrimerNombre.Attributes.Add("placeholder", string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Primer), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre)));
            lblSegundoNombre.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Segundo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            txtSegundoNombre.Attributes.Add("placeholder", string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Segundo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre)));
            lblPrimerApellido.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Primer), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Apellido));
            txtPrimerApellido.Attributes.Add("placeholder", string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Primer), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Apellido)));
            lblSegundoApellido.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Segundo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Apellido));
            txtSegundoApellido.Attributes.Add("placeholder", string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Segundo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Apellido)));
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblUsuarioBusiness oUsuarioB = new tblUsuarioBusiness(cadenaConexion);
                dgUsuarios.DataSource = oUsuarioB.ObtenerUsuarioListaPorFiltrosNombreEmpresa(txtPrimerNombre.Text, txtSegundoNombre.Text, txtPrimerApellido.Text, txtSegundoApellido.Text, oUsuarioI.idEmpresa);
                dgUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los usuarios. {0}", ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void dgUsuarios_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (oRolPagI.Actualizar)
            {
                Response.Redirect("frmCrearEditarUsuarios.aspx?idUsuario=" + e.Item.Cells[dgUsuariosColumnsEnum.idUsuario.GetHashCode()].Text);
            }
            else
            {
                Response.Write("<script>alert('El usuario no posee permisos para esta operación');</script>");
            }
        }
    }
}