using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using Idioma;
using HQSFramework.Base;

namespace Inventario
{
    public partial class frmCrearUsuarios : PaginaBase
    {

        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgRolesColumnsEnum
        {
            IdRol = 0,
            Nombre = 1,
            Seleccionar = 2
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarUsuarios.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            if (!string.IsNullOrEmpty(Request.QueryString["idUsuario"]))
                            {
                                hddIdUsuario.Value = Request.QueryString["idUsuario"];
                                tblUsuarioItem oUsuarioItem = new tblUsuarioItem();
                                tblUsuarioBusiness oUsuarioB = new tblUsuarioBusiness(cadenaConexion);
                                oUsuarioItem = oUsuarioB.ObtenerUsuario(long.Parse(hddIdUsuario.Value));
                                CargarDatosUsuario(ref oUsuarioItem);
                            }
                            CargarRoles();
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} pestañas();", strScript);
                            strScript = string.Format("{0} EstablecerAutoCompleteCiudad('{1}','Ashx/Ciudad.ashx','{2}');", strScript, txtCiudad.ClientID, hddIdCiudad.ClientID);
                            strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','1');", strScript, txtBodega.ClientID, hddIdBodega.ClientID, oUsuarioI.idEmpresa);
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
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario));
            lblTituloGrilla.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Asignacion), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Roles));
            lblTipoIdentificacion.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion));
            lblIdentificacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion);
            txtIdentificacion.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion));
            lblLogin.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Login);
            txtLogin.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Login));
            lblPassword.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Contrasena);
            txtContrasena.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Contrasena));
            lblPrimerNombre.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Primer), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            txtPrimerNombre.Attributes.Add("placeholder", string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Primer), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre)));
            lblSegundoNombre.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Segundo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            txtSegundoNombre.Attributes.Add("placeholder", string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Segundo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre)));
            lblPrimerApellido.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Primer), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Apellido));
            txtPrimerApellido.Attributes.Add("placeholder", string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Primer), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Apellido)));
            lblSegundoApellido.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Segundo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Apellido));
            txtSegundoApellido.Attributes.Add("placeholder", string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Segundo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Apellido)));
            lblCorreo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Correo);
            txtCorreo.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Correo));
            lblTelefono.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Telefono);
            txtTelefono.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Telefono));
            lblCelular.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Celular);
            txtCelular.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Celular));
            lblDireccion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Direccion);
            txtDireccion.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Direccion));
            lblCiudad.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ciudad);
            txtCiudad.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ciudad));
            lblBodega.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega);
            txtBodega.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega));
            chkActivo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Activo);
            chkModificaPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ModificaPrecio);
            chkAdmin.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Administrador);
            lblPorcentajeDescuento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.PorcentajeDescuento);
            txtPorcentajeDescuento.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.PorcentajeDescuento));
            CargarTipoIdentificacion(oCIdioma, Idioma);
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgRolesUsuario.Columns[dgRolesColumnsEnum.Nombre.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
                dgRolesUsuario.Columns[dgRolesColumnsEnum.Seleccionar.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarTipoIdentificacion(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                string Opcion = ddlTipoIdentificacion.SelectedValue;
                ddlTipoIdentificacion.Items.Clear();
                ddlTipoIdentificacion.SelectedValue = null;
                ddlTipoIdentificacion.Items.Add(new ListItem(string.Format("{0} {1} {2} {3}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion)), "0"));
                ddlTipoIdentificacion.Items.Add(new ListItem(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nit), tblTerceroBusiness.TipoIdentificacion.Nit.GetHashCode().ToString()));
                ddlTipoIdentificacion.Items.Add(new ListItem(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cedula), tblTerceroBusiness.TipoIdentificacion.Cedula.GetHashCode().ToString()));
                ddlTipoIdentificacion.DataBind();
                if (!IsPostBack)
                {
                    ddlTipoIdentificacion.SelectedValue = "0";
                }
                else
                {
                    ddlTipoIdentificacion.SelectedValue = Opcion;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarRoles()
        {
            tblRolBusiness oRolB = new tblRolBusiness(cadenaConexion);
            dgRolesUsuario.DataSource = oRolB.ObtenerRolLista(oUsuarioI.idEmpresa);
            dgRolesUsuario.DataBind();
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            limpiarControles();
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            tblUsuarioBusiness oUsuarioB = new tblUsuarioBusiness(cadenaConexion);
            tblUsuarioItem oUsuarioI = new tblUsuarioItem();
            try
            {
                if (!string.IsNullOrEmpty(hddIdUsuario.Value) && hddIdUsuario.Value != "0")
                {
                    oUsuarioI = oUsuarioB.ObtenerUsuario(long.Parse(hddIdUsuario.Value));
                    CargarDatosUsuarioEditar(ref oUsuarioI);
                }
                else
                {
                    CargarDatosUsuarioEditar(ref oUsuarioI);
                }
                if (oRolPagI.Insertar && oUsuarioI.idUsuario == 0)
                {
                    if (oUsuarioB.insertar(oUsuarioI) != -1)
                    {
                        SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                        oSegB.eliminarRolesUSuario(oUsuarioI.idUsuario);
                        foreach (DataGridItem item in dgRolesUsuario.Items)
                        {
                            if (((CheckBox)(item.Cells[dgRolesColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkRol"))).Checked == true)
                            {
                                tblUsuario_RolItem oUsuRolI = new tblUsuario_RolItem();
                                oUsuRolI.idRol = short.Parse(item.Cells[dgRolesColumnsEnum.IdRol.GetHashCode()].Text);
                                oUsuRolI.idUsuario = oUsuarioI.idUsuario;
                                oUsuRolI.idEmpresa = oUsuarioI.idEmpresa;
                                if (!oSegB.asignarRolesUsuario(oUsuRolI))
                                {
                                    MostrarMensaje("Error", "Problemas al asignar los roles");
                                }
                            }
                        }
                        MostrarMensaje("Exitoso", "El usuario se guardo con exito.");
                        limpiarControles();
                    }
                    else
                    {
                        MostrarMensaje("Error", "El usuario no se pudo guardar.");
                    }
                }
                else
                {
                    if (oRolPagI.Actualizar && oUsuarioI.idUsuario > 0)
                    {
                        if (oUsuarioB.insertar(oUsuarioI) != -1)
                        {
                            SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                            oSegB.eliminarRolesUSuario(oUsuarioI.idUsuario);
                            foreach (DataGridItem item in dgRolesUsuario.Items)
                            {
                                if (((CheckBox)(item.Cells[dgRolesColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkRol"))).Checked == true)
                                {
                                    tblUsuario_RolItem oUsuRolI = new tblUsuario_RolItem();
                                    oUsuRolI.idRol = short.Parse(item.Cells[dgRolesColumnsEnum.IdRol.GetHashCode()].Text);
                                    oUsuRolI.idUsuario = oUsuarioI.idUsuario;
                                    oUsuRolI.idEmpresa = oUsuarioI.idEmpresa;
                                    if (!oSegB.asignarRolesUsuario(oUsuRolI))
                                    {
                                        MostrarMensaje("Error", "Problemas al asignar los roles");
                                    }
                                }
                            }
                            MostrarMensaje("Exitoso", "El usuario se actualizó con exito.");
                            Response.Redirect("frmVerUsuarios.aspx");
                        }
                        else
                        {
                            MostrarMensaje("Error", "El usuario no se pudo actualizar.");
                        }
                    }
                    else
                    {
                        MostrarMensaje("Error", "El usuario no posee permisos para esta operación.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarDatosUsuario(ref tblUsuarioItem usuario)
        {
            try
            {
                tblCiudadBusiness oCiuB = new tblCiudadBusiness(cadenaConexion);
                tblCiudadItem oCiuI = new tblCiudadItem();
                oCiuI = oCiuB.ObtenerCiudad(usuario.idCiudad);
                tblBodegaBusiness oBodB = new tblBodegaBusiness(cadenaConexion);
                tblBodegaItem oBodI = new tblBodegaItem();
                oBodI = oBodB.ObtenerBodega(usuario.idBodega, usuario.idEmpresa);
                ddlTipoIdentificacion.SelectedValue = usuario.idTipoIdentificacion.ToString();
                txtIdentificacion.Text = usuario.Identificacion;
                txtLogin.Text = usuario.Usuario;
                txtContrasena.Text = usuario.Contrasena;
                txtPrimerNombre.Text = usuario.PrimerNombre;
                txtSegundoNombre.Text = usuario.SegundoNombre;
                txtPrimerApellido.Text = usuario.PrimerApellido;
                txtSegundoApellido.Text = usuario.SegundoApellido;
                txtCorreo.Text = usuario.Correo;
                txtTelefono.Text = usuario.Telefono;
                txtCelular.Text = usuario.Celular;
                txtDireccion.Text = usuario.Direccion;
                txtCiudad.Text = oCiuI.Nombre;
                hddIdCiudad.Value = usuario.idCiudad.ToString();
                txtBodega.Text = oBodI.Descripcion;
                hddIdBodega.Value = usuario.idBodega.ToString();
                chkActivo.Checked = usuario.Activo;
                chkModificaPrecio.Checked = usuario.ModificaPrecio;
                chkAdmin.Checked = usuario.EsAdmin;
                txtPorcentajeDescuento.Text = usuario.PorcentajeDescuento.ToString();
                chkVerCuadreCaja.Checked = usuario.VerCuadreCaja;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarDatosUsuarioEditar(ref tblUsuarioItem usuario)
        {
            try
            {
                usuario.idTipoIdentificacion = short.Parse(ddlTipoIdentificacion.SelectedValue);
                usuario.Identificacion = txtIdentificacion.Text;
                usuario.Usuario = txtLogin.Text;
                if (txtContrasena.Text != "")
                {
                    usuario.Contrasena = txtContrasena.Text;
                }
                usuario.PrimerNombre = txtPrimerNombre.Text.Trim();
                usuario.SegundoNombre = txtSegundoNombre.Text.Trim();
                usuario.PrimerApellido = txtPrimerApellido.Text.Trim();
                usuario.SegundoApellido = txtSegundoApellido.Text.Trim();
                usuario.Correo = txtCorreo.Text;
                usuario.Telefono = txtTelefono.Text;
                usuario.Celular = txtCelular.Text;
                usuario.Direccion = txtDireccion.Text;
                usuario.idCiudad = short.Parse(hddIdCiudad.Value);
                usuario.idBodega = long.Parse(hddIdBodega.Value);
                usuario.idEmpresa = oUsuarioI.idEmpresa;
                usuario.Activo = chkActivo.Checked;
                usuario.ModificaPrecio = chkModificaPrecio.Checked;
                usuario.EsAdmin = chkAdmin.Checked;
                if(!string.IsNullOrEmpty(txtPorcentajeDescuento.Text))
                {
                    usuario.PorcentajeDescuento = decimal.Parse(txtPorcentajeDescuento.Text, System.Globalization.NumberStyles.Currency);
                }
                usuario.VerCuadreCaja = chkVerCuadreCaja.Checked;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void limpiarControles()
        {
            ConfiguracionIdioma();
            ddlTipoIdentificacion.SelectedValue = "0";
            CargarRoles();
            txtIdentificacion.Text = "";
            txtLogin.Text = "";
            txtContrasena.Text = "";
            txtPrimerNombre.Text = "";
            txtSegundoNombre.Text = "";
            txtPrimerApellido.Text = "";
            txtSegundoApellido.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            txtCelular.Text = "";
            txtDireccion.Text = "";
            txtCiudad.Text = "";
            txtBodega.Text = "";
            hddIdCiudad.Value = "";
            hddIdBodega.Value = "";
            hddIdUsuario.Value = "0";
            chkActivo.Checked = false;
            chkModificaPrecio.Checked = false;
            chkAdmin.Checked = false;
            txtPorcentajeDescuento.Text = "";
            chkVerCuadreCaja.Checked = false;
        }

        protected void dgRolesUsuario_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                if (!string.IsNullOrEmpty(hddIdUsuario.Value) && hddIdUsuario.Value != "0")
                {
                    SeguridadBusiness oUsuarioB = new SeguridadBusiness(cadenaConexion);
                    List<tblUsuario_RolItem> oUsuRolI = new List<tblUsuario_RolItem>();
                    oUsuRolI = oUsuarioB.consultarRolesUsuario(long.Parse(hddIdUsuario.Value));
                    foreach (tblUsuario_RolItem rolUsuario in oUsuRolI)
                    {
                        if (short.Parse(e.Item.Cells[dgRolesColumnsEnum.IdRol.GetHashCode()].Text) == rolUsuario.idRol)
                        {
                            ((CheckBox)(e.Item.Cells[dgRolesColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkRol"))).Checked = true;
                        }
                    }
                }
            }
        }
    }
}