using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;

namespace Inventario
{
    public partial class frmListaPrecio : PaginaBase
    {
        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgListaPrecioEnum
        {
            IdListaPrecio = 0,
            Nombre = 1,
            Factor = 2,
            Activo = 3,
            Aumento = 4,
            Editar = 5
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.ListaPrecio.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            CargarListasPrecio();
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
                        Response.Write("<script>alert('El usuario no posee permisos en la pagina');</script>");
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
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagina de listas de precios. {0}", ex.Message));
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
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ListasPrecios);
            lblTituloGrilla.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Listas), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Creadas));
            chkActivo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Activo);
            txtNombre.Attributes.Add("placeholder" , oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            txtFactor.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Factor));
            chkAumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Aumento);
        }

        private void CargarListasPrecio()
        {
            try
            {
                tblListaPrecioBusiness oListB = new tblListaPrecioBusiness(cadenaConexion);
                dgListas.DataSource = oListB.ObtenerListaPrecioLista(oUsuarioI.idEmpresa);
                dgListas.DataBind();
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

        private void CargarDatosGuardar(ref tblListaPrecioBusiness oListB, ref tblListaPrecioItem Item)
        {
            try
            {
                if (lblID.Text != "")
                {
                    Item = oListB.ObtenerListaPrecioPorID(long.Parse(lblID.Text));
                }
                else
                {
                    Item.IdEmpresa = oUsuarioI.idEmpresa;
                }
                Item.Nombre = txtNombre.Text;
                Item.Factor = decimal.Parse(txtFactor.Text);
                Item.Activo = chkActivo.Checked;
                Item.Aumento = chkAumento.Checked;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarFormulario()
        {
            try
            {
                lblID.Text = "";
                chkActivo.Checked = false;
                chkAumento.Checked = false;
                txtNombre.Text = "";
                txtFactor.Text = "";
                CargarListasPrecio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblListaPrecioItem oListPrecioI = new tblListaPrecioItem();
                tblListaPrecioBusiness oListB = new tblListaPrecioBusiness(cadenaConexion);
                CargarDatosGuardar(ref oListB, ref oListPrecioI);
                if (oListB.Guardar(oListPrecioI))
                {
                    MostrarMensaje("Lista Precio","La lista de precio se guardo con exito.");
                    LimpiarFormulario();
                }
                else
                {
                    MostrarMensaje("Error","No se pudo guardar la lista de precio.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo guardar la lista de precio. {0}", ex.Message));
            }
        }

        protected void dgListas_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Editar")
                {
                    if (oRolPagI.Actualizar)
                    {
                        lblID.Text = e.Item.Cells[dgListaPrecioEnum.IdListaPrecio.GetHashCode()].Text;
                        txtNombre.Text = e.Item.Cells[dgListaPrecioEnum.Nombre.GetHashCode()].Text;
                        txtFactor.Text = e.Item.Cells[dgListaPrecioEnum.Factor.GetHashCode()].Text;
                        if (e.Item.Cells[dgListaPrecioEnum.Activo.GetHashCode()].Text == "True")
                        {
                            chkActivo.Checked = true;
                        }
                        else
                        {
                            chkActivo.Checked = false;
                        }
                        if (e.Item.Cells[dgListaPrecioEnum.Aumento.GetHashCode()].Text == "True")
                        {
                            chkAumento.Checked = true;
                        }
                        else
                        {
                            chkAumento.Checked = false;
                        }
                    }
                    else
                    {
                        MostrarMensaje("Permisos","El usuario no tiene permiso para esta operación.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo realizar la operación. {0}", ex.Message));
            }
        }
    }
}