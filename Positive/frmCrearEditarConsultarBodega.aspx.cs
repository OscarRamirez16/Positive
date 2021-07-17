using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioItem;
using InventarioBusiness;
using System.Configuration;
using Idioma;
using HQSFramework.Base;

namespace Inventario
{
    public partial class frmCrearEditarBodega : PaginaBase
    {

        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgBodegasColumnsEnum
        {
            idBodega = 0,
            nombre = 1,
            direccion = 2
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarConsultarBodega.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            CargarBodegas();
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
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagiona. {0}", ex.Message));
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega));
            lblTituloGrilla.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lista), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodegas));
            txtNombre.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Descripcion));
            txtDireccion.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Direccion));
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgBodegas.Columns[dgBodegasColumnsEnum.nombre.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
                dgBodegas.Columns[dgBodegasColumnsEnum.direccion.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Direccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarBodegas()
        {
            try
            {
                tblBodegaBusiness oBodB = new tblBodegaBusiness(cadenaConexion);
                dgBodegas.DataSource = oBodB.ObtenerBodegaLista(oUsuarioI.idEmpresa);
                dgBodegas.DataBind();
            }
            catch 
            {
                Response.Write("<script>alert('Error al cargar las bodegas');</script>");
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombre.Text))
                {
                    tblBodegaBusiness oBodB = new tblBodegaBusiness(cadenaConexion);
                    tblBodegaItem oBodI = new tblBodegaItem();
                    cargarDatosBodega(oBodI);
                    if (oRolPagI.Insertar && oBodI.IdBodega == 0)
                    {
                        if (oBodB.Guardar(oBodI))
                        {
                            Response.Write("<script>alert('La bodega se creo con exito');</script>");
                            limpiarControles();
                        }
                        else
                        {
                            Response.Write("<script>alert('Error al crear la bodega');</script>");
                        }
                    }
                    else
                    {
                        if (oRolPagI.Actualizar && oBodI.IdBodega > 0)
                        {
                            if (oBodB.Guardar(oBodI))
                            {
                                Response.Write("<script>alert('La bodega se actualizo con exito');</script>");
                                limpiarControles();
                            }
                            else
                            {
                                Response.Write("<script>alert('Error al actualizar la bodega');</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('El usuario no posee permisos para esta operación');</script>");
                        }
                    }
                }
                else
                {
                    MostrarMensaje("Error", "Por favor ingrese un nombre valido a la bodega.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo crear la bodega. {0}", ex.Message));
            }
        }

        private void limpiarControles()
        {
            CargarBodegas();
            txtNombre.Text = "";
            txtDireccion.Text = "";
            hddIdBodega.Value = "";
        }

        private void cargarDatosBodega(tblBodegaItem bodega)
        {
            try
            {
                if (hddIdBodega.Value != "")
                {
                    bodega.IdBodega = long.Parse(hddIdBodega.Value);
                }
                bodega.Descripcion = txtNombre.Text;
                bodega.Direccion = txtDireccion.Text;
                bodega.idEmpresa = oUsuarioI.idEmpresa;
            }
            catch
            {
                Response.Write("<script>alert('Error al cargar los datos de la bodega');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void dgBodegas_EditCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (oRolPagI.Actualizar)
                {
                    txtNombre.Text = e.Item.Cells[dgBodegasColumnsEnum.nombre.GetHashCode()].Text;
                    txtDireccion.Text = e.Item.Cells[dgBodegasColumnsEnum.direccion.GetHashCode()].Text;
                    hddIdBodega.Value = e.Item.Cells[dgBodegasColumnsEnum.idBodega.GetHashCode()].Text;
                }
                else
                {
                    Response.Write("<script>alert('El usuario no posee permisos para esta operación');</script>");
                }
            }
            catch(Exception ex){
                Response.Write("<script>alert('Error al querer editar la bodega " + ex.ToString() +"');</script>");
            }
        }
                       
    }
}