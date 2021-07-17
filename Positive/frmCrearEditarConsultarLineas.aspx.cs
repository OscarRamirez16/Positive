using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioItem;
using InventarioBusiness;
using Idioma;
using HQSFramework.Base;

namespace Inventario
{
    public partial class frmCrearEditarLineas : PaginaBase
    {

        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgLineasColumnsEnum
        {
            idLinea = 0,
            nombreLinea = 1
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarConsultarLineas.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            CargarLineas();
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }
                    else
                    {
                        Response.Redirect("frmMantenimientos.aspx");
                    }
                }
                else
                {
                    Response.Redirect("frmInicioSesion.aspx");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar las lineas. {0}", ex.Message));
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lineas));
            lblTituloGrilla.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lista), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lineas));
            txtNombre.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgLineas.Columns[dgLineasColumnsEnum.nombreLinea.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void dgLineas_EditCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (oRolPagI.Actualizar)
                {
                    txtNombre.Text = e.Item.Cells[dgLineasColumnsEnum.nombreLinea.GetHashCode()].Text;
                    lblID.Text = e.Item.Cells[dgLineasColumnsEnum.idLinea.GetHashCode()].Text;
                }
                else
                {
                    Response.Write("<script>alert('El usuario no posee permisos para esta operación');</script>");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo editar la linea. {0}", ex.Message));
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblLineaBussines oLinB = new tblLineaBussines(cadenaConexion);
                tblLineaItem oLinI = new tblLineaItem();
                CargarDatosLinea(oLinI);
                if (oRolPagI.Insertar && oLinI.IdLinea == 0)
                {
                    if (oLinB.Guardar(oLinI))
                    {
                        Response.Write("<script>alert('La linea se creo con exito');</script>");
                        LimpiarControles();
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al crear la linea');</script>");
                    }
                }
                else
                {
                    if (oRolPagI.Actualizar && oLinI.IdLinea > 0)
                    {
                        if (oLinB.Guardar(oLinI))
                        {
                            Response.Write("<script>alert('La linea se actualizo con exito');</script>");
                            LimpiarControles();
                        }
                        else
                        {
                            Response.Write("<script>alert('Error al actualizar la linea');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('El usuario no posee permisos para esta operación');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo crear la linea. {0}", ex.Message));
            }
        }

        private void LimpiarControles()
        {
            CargarLineas();
            txtNombre.Text = "";
            lblID.Text = "";
        }

        private void CargarDatosLinea(tblLineaItem linea)
        {
            try
            {
                if (lblID.Text != "")
                {
                    linea.IdLinea = long.Parse(lblID.Text);
                }
                linea.Nombre = txtNombre.Text;
                linea.IdEmpresa = oUsuarioI.idEmpresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarLineas()
        {
            try
            {
                tblLineaBussines oLinB = new tblLineaBussines(cadenaConexion);
                dgLineas.DataSource = oLinB.ObtenerLineaLista(oUsuarioI.idEmpresa);
                dgLineas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

    }
}