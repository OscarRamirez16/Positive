using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;

namespace Inventario
{
    public partial class frmCrearEditarTerceros : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private enum dgRetencionesEnum
        {
            Id = 0,
            Codigo = 1,
            Descripcion = 2,
            Activo = 3,
            chkSeleccionar = 4
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarTerceros.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            CargarRetenciones();
                            if (Request.QueryString["idTercero"] != null)
                            {
                                tblTerceroItem oTerceroI = new tblTerceroItem();
                                tblTerceroBusiness oTerceroB = new tblTerceroBusiness(CadenaConexion);
                                oTerceroI = oTerceroB.ObtenerTercero(long.Parse(Request.QueryString["idTercero"].ToString()), oUsuarioI.idEmpresa);
                                CargarDatosTercero(oTerceroI);
                            }
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerAutoCompleteCiudad('{1}','Ashx/Ciudad.ashx','{2}');", strScript, txtCiudad.ClientID, hddIdCiudad.ClientID);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaNac.ClientID);
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
        private void CargarRetenciones()
        {
            try
            {
                tblRetencionBusiness oRetB = new tblRetencionBusiness(CadenaConexion);
                dgRetenciones.DataSource = oRetB.ObtenerRetencionesTodas(oUsuarioI.idEmpresa);
                dgRetenciones.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio));
            lblTipoTercero.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio));
            lblTipoIdentificacion.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion));
            lblIdentificacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion);
            txtIdentificacion.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion));
            lblNombre.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            txtNombre.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
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
            lblListaPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ListaPrecio);
            lblFechaNacimiento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaNacimiento);
            txtFechaNac.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaNacimiento));
            lblGrupoCliente.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoCliente);
            //lblObservaciones.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Observaciones);
            CargarTipoIdentificacion(oCIdioma, Idioma);
            CargarTipoTercero(oCIdioma, Idioma);
            CargarListaPrecio(oCIdioma, Idioma);
            CargarGrupoCliente(oCIdioma, Idioma);
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
        public void CargarTipoTercero(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            string Opcion = ddlTipoTercero.SelectedValue;
            ddlTipoTercero.Items.Clear();
            ddlTipoTercero.SelectedValue = null;
            ddlTipoTercero.Items.Add(new ListItem(string.Format("{0} {1} {2} {3}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio)), "0"));
            ddlTipoTercero.Items.Add(new ListItem(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente), "C"));
            ddlTipoTercero.Items.Add(new ListItem(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Proveedor), "P"));
            ddlTipoTercero.DataBind();
            if (!IsPostBack)
            {
                ddlTipoTercero.SelectedValue = "0";
            }
            else
            {
                ddlTipoTercero.SelectedValue = Opcion;
            }
        }
        private void CargarGrupoCliente(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            if (!IsPostBack)
            {
                tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                ddlGrupoCliente.DataTextField = "Nombre";
                ddlGrupoCliente.DataValueField = "idGrupoCliente";
                ddlGrupoCliente.DataSource = oTBiz.ObtenerGrupoClienteLista(oUsuarioI.idEmpresa, "");
                ddlGrupoCliente.DataBind();
                ddlGrupoCliente.Items.Insert(0, new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.GrupoCliente)), "0"));
                ddlGrupoCliente.SelectedIndex = 0;
            }
        }
        public void CargarListaPrecio(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblListaPrecioBusiness oListB = new tblListaPrecioBusiness(CadenaConexion);
                string Opcion = ddlListaPrecio.SelectedValue;
                ddlListaPrecio.Items.Clear();
                ddlListaPrecio.SelectedValue = null;
                ddlListaPrecio.DataSource = oListB.ObtenerListaPrecioLista(oUsuarioI.idEmpresa);
                ddlListaPrecio.DataBind();
                ddlListaPrecio.Items.Add(new ListItem(string.Format("{0} {1} {2} {3}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lista), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Precio)), "0"));
                if (!IsPostBack)
                {
                    ddlListaPrecio.SelectedValue = "0";
                }
                else
                {
                    ddlListaPrecio.SelectedValue = Opcion;
                }
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
        private void CargarDatosTercero(tblTerceroItem tercero)
        {
            try
            {
                if (tercero.IdTercero <= 0)
                {
                    tercero.idTipoIdentificacion = short.Parse(ddlTipoIdentificacion.SelectedValue);
                    tercero.Identificacion = txtIdentificacion.Text;
                    tercero.Nombre = txtNombre.Text;
                    tercero.Mail = txtCorreo.Text;
                    tercero.Telefono = txtTelefono.Text;
                    tercero.Celular = txtCelular.Text;
                    tercero.Direccion = txtDireccion.Text;
                    tercero.idCiudad = short.Parse(hddIdCiudad.Value);
                    tercero.idEmpresa = oUsuarioI.idEmpresa;
                    tercero.TipoTercero = ddlTipoTercero.SelectedValue;
                    if (ddlTipoTercero.SelectedItem.Text == "Cliente" || ddlTipoTercero.SelectedItem.Text == "Customer")
                    {
                        if (txtFechaNac.Text != "")
                        {
                            tercero.FechaNacimiento = DateTime.Parse(txtFechaNac.Text);
                        }
                        if (ddlListaPrecio.SelectedValue != "0")
                        {
                            tercero.IdListaPrecio = long.Parse(ddlListaPrecio.SelectedValue);
                        }
                    }
                    tercero.idGrupoCliente = long.Parse(ddlGrupoCliente.SelectedValue);
                    if (!string.IsNullOrEmpty(txtObservaciones.Text))
                    {
                        tercero.Observaciones = txtObservaciones.Text;
                    }
                    tercero.Activo = chkActivo.Checked;
                    foreach (DataGridItem item in dgRetenciones.Items)
                    {
                        if (((CheckBox)(item.Cells[dgRetencionesEnum.chkSeleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked == true)
                        {
                            tblTerceroRetencionItem oRetI = new tblTerceroRetencionItem();
                            oRetI.IdRetencion = long.Parse(item.Cells[dgRetencionesEnum.Id.GetHashCode()].Text);
                            tercero.Retenciones.Add(oRetI);
                        }
                    }
                }
                else
                {
                    tblCiudadBusiness oCiuB = new tblCiudadBusiness(CadenaConexion);
                    tblCiudadItem oCiuI = new tblCiudadItem();
                    oCiuI = oCiuB.ObtenerCiudad(tercero.idCiudad);
                    ddlTipoIdentificacion.SelectedValue = tercero.idTipoIdentificacion.ToString();
                    txtIdentificacion.Text = tercero.Identificacion;
                    txtNombre.Text = tercero.Nombre;
                    txtCorreo.Text = tercero.Mail;
                    txtTelefono.Text = tercero.Telefono;
                    txtCelular.Text = tercero.Celular;
                    txtDireccion.Text = tercero.Direccion;
                    txtCiudad.Text = oCiuI.Nombre;
                    hddIdCiudad.Value = tercero.idCiudad.ToString();
                    ddlTipoTercero.SelectedValue = tercero.TipoTercero;
                    if (ddlTipoTercero.SelectedItem.Text == "Cliente" || ddlTipoTercero.SelectedItem.Text == "Customer")
                    {
                        txtFechaNac.Text = tercero.FechaNacimiento.ToShortDateString();
                        txtFechaNac.Visible = true;
                        ddlListaPrecio.SelectedValue = tercero.IdListaPrecio.ToString();
                        ddlListaPrecio.Visible = true;
                    }
                    else
                    {
                        txtFechaNac.Visible = false;
                        ddlListaPrecio.Visible = false;
                    }
                    ddlGrupoCliente.SelectedValue = tercero.idGrupoCliente.ToString();
                    txtObservaciones.Text = tercero.Observaciones;
                    chkActivo.Checked = tercero.Activo;
                    foreach(tblTerceroRetencionItem oRetI in tercero.Retenciones)
                    {
                        foreach (DataGridItem item in dgRetenciones.Items)
                        {
                            if (long.Parse(item.Cells[dgRetencionesEnum.Id.GetHashCode()].Text) == oRetI.IdRetencion)
                            {
                                ((CheckBox)(item.Cells[dgRetencionesEnum.chkSeleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked = true;
                            }
                        }
                    }
                }
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
                tblTerceroBusiness oTerceroB = new tblTerceroBusiness(CadenaConexion);
                tblTerceroItem oTerceroI = new tblTerceroItem();
                if (!string.IsNullOrEmpty(Request.QueryString["idTercero"]))
                {
                    CargarDatosTerceroEditar(oTerceroI);
                }
                else
                {
                    CargarDatosTercero(oTerceroI);
                }
                if (oRolPagI.Insertar && oTerceroI.IdTercero == 0)
                {
                    string Error = oTerceroB.insertar(oTerceroI);
                    if (string.IsNullOrEmpty(Error))
                    {
                        MostrarAlerta(1, "Exitoso", "El tercero se guardó con exito.");
                        LimpiarControles();
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", Error);
                    }
                }
                else
                {
                    if (oRolPagI.Actualizar && oTerceroI.IdTercero > 0)
                    {
                        string Error = oTerceroB.insertar(oTerceroI);
                        if (string.IsNullOrEmpty(Error))
                        {
                            MostrarAlerta(1, "Exitoso", "El tercero se actualizó con exito.");
                            LimpiarControles();
                        }
                        else
                        {
                            MostrarAlerta(0, "Error", Error);
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Permisos", "El usuario no posee permisos para esta operación.");
                    }
                }
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }

        private void CargarDatosTerceroEditar(tblTerceroItem tercero)
        {
            try
            {
                tercero.IdTercero = long.Parse(Request.QueryString["idTercero"]);
                tercero.idTipoIdentificacion = short.Parse(ddlTipoIdentificacion.SelectedValue);
                tercero.Identificacion = txtIdentificacion.Text.Trim();
                tercero.Nombre = txtNombre.Text.Trim();
                tercero.Mail = txtCorreo.Text.Trim();
                tercero.Telefono = txtTelefono.Text.Trim();
                tercero.Celular = txtCelular.Text.Trim();
                tercero.Direccion = txtDireccion.Text.Trim();
                tercero.idCiudad = short.Parse(hddIdCiudad.Value);
                tercero.idEmpresa = oUsuarioI.idEmpresa;
                tercero.TipoTercero = ddlTipoTercero.SelectedValue;
                if (ddlTipoTercero.SelectedItem.Text == "Cliente" || ddlTipoTercero.SelectedItem.Text == "Customer")
                {
                    if (txtFechaNac.Text != "" && Util.EsFecha(txtFechaNac.Text))
                    {
                        tercero.FechaNacimiento = DateTime.Parse(txtFechaNac.Text);
                    }
                    if (ddlListaPrecio.SelectedValue != "0")
                    {
                        tercero.IdListaPrecio = long.Parse(ddlListaPrecio.SelectedValue);
                    }
                }
                tercero.idGrupoCliente = long.Parse(ddlGrupoCliente.SelectedValue);
                if (!string.IsNullOrEmpty(txtObservaciones.Text))
                {
                    tercero.Observaciones = txtObservaciones.Text;
                }
                tercero.Activo = chkActivo.Checked;
                foreach (DataGridItem item in dgRetenciones.Items)
                {
                    if (((CheckBox)(item.Cells[dgRetencionesEnum.chkSeleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked == true)
                    {
                        tblTerceroRetencionItem oRetI = new tblTerceroRetencionItem();
                        oRetI.IdRetencion = long.Parse(item.Cells[dgRetencionesEnum.Id.GetHashCode()].Text);
                        tercero.Retenciones.Add(oRetI);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarControles()
        {
            try
            {
                txtIdentificacion.Text = "";
                txtNombre.Text = "";
                txtCorreo.Text = "";
                txtTelefono.Text = "";
                txtCelular.Text = "";
                txtDireccion.Text = "";
                txtCiudad.Text = "";
                hddIdCiudad.Value = "";
                txtFechaNac.Text = "";
                txtObservaciones.Text = "";
                ConfiguracionIdioma();
                ddlGrupoCliente.SelectedIndex = 0;
                chkActivo.Checked = false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlTipoTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoTercero.SelectedItem.Text == "Cliente" || ddlTipoTercero.SelectedItem.Text == "Customer")
                {
                    txtFechaNac.Visible = true;
                    ddlListaPrecio.Visible = true;
                }
                else
                {
                    txtFechaNac.Visible = false;
                    ddlListaPrecio.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }
    }
}