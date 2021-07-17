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
    public partial class frmCampana : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private long IdCampana = 0;
        private enum dgCampanasColumnEnum { 
            idCampana = 0,
            Nombre = 1,
            FechaInicial = 2,
            FechaFinal = 3,
            Activo = 4,
            Editar = 5
        }
        private enum dgCampanaArticuloColumnEnum {
            idCampanaArticulo = 0,
            Excluir = 1,
            TipoCampanaArticuloNombre = 2,
            Codigo = 3,
            Nombre = 4,
            TipoDescuentoNombre = 5,
            ValorDescuento = 6,
            lnkEditar = 7
        }
        private enum dgCampanaClienteColumnEnum {
            idCampanaCliente = 0,
            Excluir = 1,
            TipoCampanaClienteNombre = 2,
            Codigo = 3,
            Nombre = 4,
            lnkEditar = 5
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Campana.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        if (Util.EsEntero(Request.QueryString["IdCampana"]))
                        {
                            IdCampana = long.Parse(Request.QueryString["IdCampana"]);
                        }
                        if (!IsPostBack)
                        {
                            CargarEstados();
                        }
                        ConfiguracionIdioma();
                        CargarInformacion();
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} pestañas();", strScript);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaInicial.ClientID);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaFinal.ClientID);
                            strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}','{3}',4);", strScript, txtArticulo.ClientID, hddIdArticulo.ClientID, oUsuarioI.idEmpresa);
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
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message);
            }
        }
        private void CargarEstados()
        {
            try
            {
                ddlEstado.Items.Add(new ListItem("Todos", "-1"));
                ddlEstado.Items.Add(new ListItem("Inactivos", "0"));
                ddlEstado.Items.Add(new ListItem("Activos", "1"));
                ddlEstado.SelectedValue = "-1";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void ConfiguracionIdioma()
        {
            Traductor oCIdioma = new Traductor();
            Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
            if (!Util.EsEntero(Request.Form["ctl00$ddlIdiomas"]))
            {
                Idioma = (Idioma.Traductor.IdiomaEnum)oUsuarioI.IdIdioma;
            }
            else
            {
                Idioma = (Traductor.IdiomaEnum)int.Parse(Request.Form["ctl00$ddlIdiomas"]);
            }
            liCrearCampana.Text = string.Format("{0}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana));
            liCampanaCreadas.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana));
            lblTitulo.Text = string.Format("{0}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana));
            lblNombre.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            txtNombre.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            lblObservacion.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Observaciones);
            txtObservacion.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Observaciones));
            lblFechaInicial.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaInicial);
            lblFechaFinal.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaFinal);
            lblHoraInicial.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.HoraInicial);
            lblHoraFinal.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.HoraFinal);
            //IdCampanaLabel.InnerText = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Numero), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana));
            //lblActivo.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Activo);
            //lblIdCampanaBuscar.InnerText = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Numero), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana));
            lblTituloBuscar.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Buscar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana));
            lblNombreBuscar.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            txtNombreBuscar.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            //lblFecha.InnerText = string.Format("{0}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Fecha));
            lblArticulo.InnerText = string.Format("{0}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo));
            txtArticulo.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo));
            //lblTodos.InnerText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Todos);
            dgCampanas.Columns[dgCampanasColumnEnum.idCampana.GetHashCode()].HeaderText = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Numero), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Campana));
            dgCampanas.Columns[dgCampanasColumnEnum.Nombre.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            dgCampanas.Columns[dgCampanasColumnEnum.FechaInicial.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaInicial);
            dgCampanas.Columns[dgCampanasColumnEnum.FechaFinal.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FechaFinal);
            dgCampanas.Columns[dgCampanasColumnEnum.Activo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Activo);
            lblCampanaArticuloTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulos);
            lblCampanaClienteTitulo.Text = string.Format("{0}s",oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente));
        }

        private void CargarInformacion()
        {
            try
            {
                if (!IsPostBack && IdCampana > 0)
                {
                    tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
                    tblCampanaItem oCItem = oABiz.ObtenerCampana(IdCampana, oUsuarioI.idEmpresa);
                    if (oCItem != null && oCItem.idCampana > 0)
                    {
                        lblIdCampana.Text = oCItem.idCampana.ToString();
                        txtNombre.Text = oCItem.Nombre;
                        txtObservacion.Text = oCItem.Observacion;
                        txtFechaInicial.Text = oCItem.FechaInicial.ToShortDateString();
                        txtFechaFinal.Text = oCItem.FechaFinal.ToShortDateString();
                        if (oCItem.HoraInicial != null && oCItem.HoraInicial != DateTime.MinValue)
                        {
                            txtHoraInicial.Text = Util.ConvertirHoraGUI(oCItem.HoraInicial);
                        }
                        if (oCItem.HoraFinal != null && oCItem.HoraFinal != DateTime.MinValue)
                        {
                            txtHoraFinal.Text = Util.ConvertirHoraGUI(oCItem.HoraFinal);
                        }
                        chkActivo.Checked = oCItem.Activo;
                        dgCampanaArticulo.DataSource = oABiz.ObtenerCampanaArticuloLista(IdCampana, oUsuarioI.idEmpresa);
                        dgCampanaArticulo.DataBind();
                        dgCampanaCliente.DataSource = oABiz.ObtenerCampanaClienteLista(IdCampana, oUsuarioI.idEmpresa);
                        dgCampanaCliente.DataBind();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string ValidarInformacion()
        {
            string MensajeError = "";
            try
            {
                Traductor oCIdioma = new Traductor();
                Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
                if (!Util.EsEntero(Request.Form["ctl00$ddlIdiomas"]))
                {
                    Idioma = (Idioma.Traductor.IdiomaEnum)oUsuarioI.IdIdioma;
                }
                else
                {
                    Idioma = (Traductor.IdiomaEnum)int.Parse(Request.Form["ctl00$ddlIdiomas"]);
                }
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.NombreObligatorio));
                }
                if (!Util.EsFecha(txtFechaInicial.Text))
                {
                    MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaFechaInicial));
                }
                if (!Util.EsFecha(txtFechaFinal.Text))
                {
                    MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaFechaFinal));
                }
                if (Util.EsFecha(txtFechaInicial.Text) && Util.EsFecha(txtFechaFinal.Text))
                {
                    if (DateTime.Parse(txtFechaInicial.Text) > DateTime.Parse(txtFechaFinal.Text))
                    {
                        MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaFechaInicialFinal));
                    }
                }
                if (!string.IsNullOrEmpty(txtHoraInicial.Text) && !string.IsNullOrEmpty(txtHoraFinal.Text))
                {
                    if (!Util.EsHora(txtHoraInicial.Text))
                    {
                        MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaHoraInicial));
                    }
                    if (!Util.EsHora(txtHoraFinal.Text))
                    {
                        MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaHoraFinal));
                    }
                    if (Util.EsHora(txtHoraInicial.Text) && Util.EsHora(txtHoraFinal.Text))
                    {
                        if (Util.ObtenerHora(txtHoraInicial.Text) > Util.ObtenerHora(txtHoraFinal.Text))
                        {
                            MensajeError = string.Format("{0}<br/>{1}...", MensajeError, oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CampanaHoraInicialFinal));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return MensajeError;
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string MensajeError = ValidarInformacion();
                Traductor oCIdioma = new Traductor();
                Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
                if (!Util.EsEntero(Request.Form["ctl00$ddlIdiomas"]))
                {
                    Idioma = (Idioma.Traductor.IdiomaEnum)oUsuarioI.IdIdioma;
                }
                else
                {
                    Idioma = (Traductor.IdiomaEnum)int.Parse(Request.Form["ctl00$ddlIdiomas"]);
                }
                if (string.IsNullOrEmpty(MensajeError))
                {
                    tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
                    tblCampanaItem oCItem;
                    if (IdCampana > 0)
                    {
                        oCItem = oABiz.ObtenerCampana(IdCampana, oUsuarioI.idEmpresa);
                    }
                    else
                    {
                        oCItem = new tblCampanaItem();
                        oCItem.Fecha = DateTime.Now;
                        oCItem.idEmpresa = oUsuarioI.idEmpresa;
                        oCItem.idUsuario = oUsuarioI.idUsuario;
                    }
                    oCItem.Nombre = txtNombre.Text;
                    oCItem.Observacion = txtObservacion.Text;
                    oCItem.FechaInicial = DateTime.Parse(txtFechaInicial.Text);
                    oCItem.FechaFinal = DateTime.Parse(txtFechaFinal.Text);
                    if (!string.IsNullOrEmpty(txtHoraInicial.Text) && !string.IsNullOrEmpty(txtHoraFinal.Text))
                    {
                        oCItem.HoraInicial = Util.ObtenerHora(txtHoraInicial.Text);
                        oCItem.HoraFinal = Util.ObtenerHora(txtHoraFinal.Text);
                    }
                    oCItem.Activo = chkActivo.Checked;
                    if (oABiz.Guardar(oCItem))
                    {
                        Response.Redirect("frmCampana.aspx");
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", "No se pudo guardar la campaña.");
                    }
                }
                else
                {
                    MostrarAlerta(0 , "Error", MensajeError);
                }
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmCampana.aspx");
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblArticuloBusiness oABBiz = new tblArticuloBusiness(CadenaConexion);
                dgCampanas.DataSource = oABBiz.ObtenerCampanaLista(txtIdCampana.Text, txtNombreBuscar.Text, int.Parse(ddlEstado.SelectedValue), hddIdArticulo.Value, oUsuarioI.idEmpresa);
                dgCampanas.DataBind();
                SeleccionarTab("contenido", 1);
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message);
            }
        }

        protected void dgCampanas_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Response.Redirect(string.Format("frmCampana.aspx?IdCampana={0}",e.Item.Cells[dgCampanasColumnEnum.idCampana.GetHashCode()].Text));
        }

        protected void imgAdicionarArticulo_Click(object sender, ImageClickEventArgs e)
        {
            if (IdCampana > 0) {
                Response.Redirect(string.Format("frmCampanaArticulo.aspx?IdCampana={0}", IdCampana));
            }
        }

        protected void dgCampanaArticulo_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (IdCampana > 0)
            {
                Response.Redirect(string.Format("frmCampanaArticulo.aspx?IdCampana={0}&IdCampanaArticulo={1}", IdCampana,e.Item.Cells[dgCampanaArticuloColumnEnum.idCampanaArticulo.GetHashCode()].Text));
            }
        }

        protected void imgAdicionarCliente_Click(object sender, ImageClickEventArgs e)
        {
            if (IdCampana > 0)
            {
                Response.Redirect(string.Format("frmCampanaCliente.aspx?IdCampana={0}", IdCampana));
            }
        }

        protected void dgCampabaCliente_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (IdCampana > 0)
            {
                Response.Redirect(string.Format("frmCampanaCliente.aspx?IdCampana={0}&IdCampanaCliente={1}", IdCampana, e.Item.Cells[dgCampanaClienteColumnEnum.idCampanaCliente.GetHashCode()].Text));
            }
        }
    }
}