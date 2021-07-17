using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using Idioma;
using HQSFramework.Base;

namespace Inventario
{
    public partial class frmCrearEditarRoles : PaginaBase
    {

        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgRolPaginasEnum
        {
            idPagina = 0,
            nombre = 1,
            leer = 2,
            insertar = 3,
            actualizar = 4,
            eliminar = 5
        }

        private enum dgRolesColumnsEnum
        {
            idRol = 0,
            nombre = 1,
            empresa = 2,
            idEmpresa = 3
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarRoles.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            cargarPaginasRoles();
                            cargarRoles();
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Roles));
            lblTituloGrilla.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lista), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Roles));
            txtNombreRol.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Descripcion));
            lblInformacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.InformacionRol);
            lblAsigacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.AsignacionPermisos);
            ConfigurarIdiomaGrillaPaginas(oCIdioma, Idioma);
            ConfigurarIdiomaGrillaRoles(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrillaPaginas(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgRolPaginas.Columns[dgRolPaginasEnum.nombre.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
                dgRolPaginas.Columns[dgRolPaginasEnum.leer.GetHashCode()].HeaderText = string.Format("{0} <input type='checkbox' id='chkSelTodLeer' data-elementos='chkLeer' class='_selecionar' />", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Leer));
                dgRolPaginas.Columns[dgRolPaginasEnum.insertar.GetHashCode()].HeaderText = string.Format("{0} <input type='checkbox' id='chkSelTodGuardar'  data-elementos='chkInsertar' class='_selecionar'/>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Guardar));
                dgRolPaginas.Columns[dgRolPaginasEnum.actualizar.GetHashCode()].HeaderText = string.Format("{0} <input type='checkbox' id='chkSelTodActualizar' data-elementos='chkActulizar' class='_selecionar'/>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Actualizar));
                dgRolPaginas.Columns[dgRolPaginasEnum.eliminar.GetHashCode()].HeaderText = string.Format("{0} <input type='checkbox' id='chkSelTodEliminar' data-elementos='chkEliminar' class='_selecionar'/>", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Eliminar));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigurarIdiomaGrillaRoles(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgRoles.Columns[dgRolesColumnsEnum.nombre.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
                dgRoles.Columns[dgRolesColumnsEnum.empresa.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Empresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            limpiarControles();
        }
        private string ValidarDatos()
        {
            try
            {
                string Errores = string.Empty;
                if (string.IsNullOrEmpty(txtNombreRol.Text))
                {
                    Errores = "Por favor digitar el nombre del rol";
                }
                return Errores;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string Errores = ValidarDatos();
                if (string.IsNullOrEmpty(Errores))
                {
                    tblRolBusiness oRolB = new tblRolBusiness(cadenaConexion);
                    tblRolItem oRolI = new tblRolItem();
                    cargarDatosRol(oRolI);
                    if (oRolPagI.Insertar && oRolI.idRol == 0)
                    {
                        if (oRolB.insertar(oRolI) != -1)
                        {
                            SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                            oSegB.eliminarPaginasRoles(oRolI.idRol);
                            foreach (DataGridItem item in dgRolPaginas.Items)
                            {
                                tblRol_PaginaItem oRolPagItem = new tblRol_PaginaItem();
                                oRolPagItem.idRol = oRolI.idRol;
                                oRolPagItem.idPagina = short.Parse(item.Cells[dgRolPaginasEnum.idPagina.GetHashCode()].Text);
                                oRolPagItem.Leer = ((CheckBox)(item.Cells[dgRolPaginasEnum.leer.GetHashCode()].FindControl("chkLeer"))).Checked;
                                oRolPagItem.Insertar = ((CheckBox)(item.Cells[dgRolPaginasEnum.actualizar.GetHashCode()].FindControl("chkInsertar"))).Checked;
                                oRolPagItem.Actualizar = ((CheckBox)(item.Cells[dgRolPaginasEnum.insertar.GetHashCode()].FindControl("chkActualizar"))).Checked;
                                oRolPagItem.Eliminar = ((CheckBox)(item.Cells[dgRolPaginasEnum.eliminar.GetHashCode()].FindControl("chkEliminar"))).Checked;
                                oRolPagItem.idEmpresa = oRolI.idEmpresa;
                                if (!oSegB.asignarPaginasRoles(oRolPagItem))
                                {
                                    MostrarAlerta(2, "Problemas al asignar las paginas", string.Empty);
                                }
                            }
                            MostrarAlerta(1, "Exitoso", "El rol se guardo con exito");
                            limpiarControles();
                        }
                        else
                        {
                            MostrarAlerta(0, "Exitoso", "El rol no se pudo guardar.");
                        }
                    }
                    else
                    {
                        if (oRolPagI.Actualizar && oRolI.idRol > 0)
                        {
                            if (oRolB.insertar(oRolI) != -1)
                            {
                                SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                                oSegB.eliminarPaginasRoles(oRolI.idRol);
                                foreach (DataGridItem item in dgRolPaginas.Items)
                                {
                                    tblRol_PaginaItem oRolPagItem = new tblRol_PaginaItem();
                                    oRolPagItem.idRol = oRolI.idRol;
                                    oRolPagItem.idPagina = short.Parse(item.Cells[dgRolPaginasEnum.idPagina.GetHashCode()].Text);
                                    oRolPagItem.Leer = ((CheckBox)(item.Cells[dgRolPaginasEnum.leer.GetHashCode()].FindControl("chkLeer"))).Checked;
                                    oRolPagItem.Insertar = ((CheckBox)(item.Cells[dgRolPaginasEnum.actualizar.GetHashCode()].FindControl("chkInsertar"))).Checked;
                                    oRolPagItem.Actualizar = ((CheckBox)(item.Cells[dgRolPaginasEnum.insertar.GetHashCode()].FindControl("chkActualizar"))).Checked;
                                    oRolPagItem.Eliminar = ((CheckBox)(item.Cells[dgRolPaginasEnum.eliminar.GetHashCode()].FindControl("chkEliminar"))).Checked;
                                    oRolPagItem.idEmpresa = oRolI.idEmpresa;
                                    if (!oSegB.asignarPaginasRoles(oRolPagItem))
                                    {
                                        MostrarAlerta(2, "Problemas al asignar las paginas", string.Empty);
                                    }
                                }
                                MostrarAlerta(1, "Exitoso", "El rol se actualizo con exito");
                                limpiarControles();
                            }
                            else
                            {
                                MostrarAlerta(0, "Exitoso", "El rol no se pudo guardar.");
                            }
                        }
                        else
                        {
                            MostrarAlerta(2, "El usuario no posee permisos para esta operación", string.Empty);
                        }
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", Errores);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void cargarDatosRol(tblRolItem rol)
        {
            try
            {
                if (lblIdRol.Text != "")
                {
                    rol.idRol = short.Parse(lblIdRol.Text);
                }
                rol.Nombre = txtNombreRol.Text;
                rol.idEmpresa = oUsuarioI.idEmpresa;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void cargarPaginasRoles()
        {
            try
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                dgRolPaginas.DataSource = oSegB.TraerPaginasRoles();
                dgRolPaginas.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void cargarRoles()
        {
            try
            {
                tblRolBusiness oRolB = new tblRolBusiness(cadenaConexion);
                dgRoles.DataSource = oRolB.ObtenerRolListaNombreEmpresa(oUsuarioI.idEmpresa);
                dgRoles.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void limpiarControles()
        {
            cargarRoles();
            txtNombreRol.Text = "";
            lblIdRol.Text = "";
            cargarPaginasRoles();
        }

        protected void dgRoles_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (oRolPagI.Actualizar)
            {
                txtNombreRol.Text = e.Item.Cells[dgRolesColumnsEnum.nombre.GetHashCode()].Text;
                lblIdRol.Text = e.Item.Cells[dgRolesColumnsEnum.idRol.GetHashCode()].Text;
                cargarPaginasRoles();
            }
            else
            {
                MostrarMensaje("Permisos", "El usuario no posee permisos para esta operación");
            }
        }

        protected void dgRolPaginas_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                if(lblIdRol.Text != ""){
                    SeguridadBusiness oUsuarioB = new SeguridadBusiness(cadenaConexion);
                    List<tblRol_PaginaItem> oUsuRolI = new List<tblRol_PaginaItem>();
                    oUsuRolI = oUsuarioB.consultarPaginasRoles(long.Parse(lblIdRol.Text), oUsuarioI.idEmpresa);
                    foreach (tblRol_PaginaItem item in oUsuRolI)
                    {
                        if(short.Parse(e.Item.Cells[dgRolPaginasEnum.idPagina.GetHashCode()].Text) == item.idPagina){
                            ((CheckBox)(e.Item.Cells[dgRolPaginasEnum.leer.GetHashCode()].FindControl("chkLeer"))).Checked = item.Leer;
                            ((CheckBox)(e.Item.Cells[dgRolPaginasEnum.leer.GetHashCode()].FindControl("chkInsertar"))).Checked = item.Insertar;
                            ((CheckBox)(e.Item.Cells[dgRolPaginasEnum.leer.GetHashCode()].FindControl("chkActualizar"))).Checked = item.Actualizar;
                            ((CheckBox)(e.Item.Cells[dgRolPaginasEnum.leer.GetHashCode()].FindControl("chkEliminar"))).Checked = item.Eliminar;
                        }
                    }
                }
            }
        }
    }
}