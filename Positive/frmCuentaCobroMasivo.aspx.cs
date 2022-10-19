using HQSFramework.Base;
using InventarioBusiness;
using InventarioItem;
using System;
using System.Configuration;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventario
{
    public partial class frmCuentaCobroMasivo : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgClientesEnum
        {
            IdTercero = 0,
            Identificacion = 1,
            Nombre = 2,
            Direccion = 3,
            Telefono = 4,
            Celular = 5,
            IdCiudad = 6,
            Seleccionar = 7
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Ventas.GetHashCode().ToString()));
                if (oRolPagI.Insertar)
                {
                    if (!IsPostBack)
                    {
                        txtTotalFactura.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        CargarGrupoCliente();
                        CargarClientes();
                    }
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0} ConfigurarEnter();", strScript);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                    }
                }
                else
                {
                    Response.Redirect("frmMantenimientos.aspx");
                }
            }
        }

        private void CargarGrupoCliente()
        {
            if (!IsPostBack)
            {
                tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                ddlGrupoCliente.DataTextField = "Nombre";
                ddlGrupoCliente.DataValueField = "idGrupoCliente";
                ddlGrupoCliente.DataSource = oTBiz.ObtenerGrupoClienteLista(oUsuarioI.idEmpresa, "");
                ddlGrupoCliente.DataBind();
                ddlGrupoCliente.Items.Insert(0, new ListItem("Todos", "0"));
                ddlGrupoCliente.SelectedIndex = 0;
            }
        }

        private void CargarClientes()
        {
            try
            {
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                dgClientes.DataSource = oTerB.ObtenerClientesListaActivos(oUsuarioI.idEmpresa, int.Parse(ddlGrupoCliente.SelectedValue));
                dgClientes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlGrupoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarClientes();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if(decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) > 0 && !string.IsNullOrEmpty(txtConcepto.Value))
                {
                    tblCuentaCobroBusiness oCuenB = new tblCuentaCobroBusiness(CadenaConexion);
                    string Errores = string.Empty;
                    foreach (DataGridItem Cliente in dgClientes.Items)
                    {
                        if (((CheckBox)(Cliente.Cells[dgClientesEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                        {
                            tblCuentaCobroItem oCuenI = new tblCuentaCobroItem();
                            oCuenI.Numero = 0;
                            oCuenI.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                            oCuenI.IdTercero = long.Parse(Cliente.Cells[dgClientesEnum.IdTercero.GetHashCode()].Text);
                            oCuenI.IdUsuario = oUsuarioI.idUsuario;
                            oCuenI.Concepto = txtConcepto.Value;
                            oCuenI.Total = decimal.Parse(txtTotalFactura.Text);
                            oCuenI.IdEstado = 1;
                            oCuenI.IdEmpresa = oUsuarioI.idEmpresa;
                            oCuenI.Saldo = oCuenI.Total;
                            string Error = oCuenB.Guardar(oCuenI);
                            if (!string.IsNullOrEmpty(Error))
                            {
                                if (!string.IsNullOrEmpty(Errores))
                                {
                                    Errores = string.Format("*Error: {0}-{1}", Cliente.Cells[dgClientesEnum.Identificacion.GetHashCode()].Text, Error);
                                }
                                else
                                {
                                    Errores = string.Format("{0} *Error: {1}-{2}", Errores, Cliente.Cells[dgClientesEnum.Identificacion.GetHashCode()].Text, Error);
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(Errores))
                    {
                        MostrarMensaje("Errores", Errores);
                    }
                    else
                    {
                        MostrarMensaje("Exito", "Cuentas de cobro realizadas con exito");
                        txtTotalFactura.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        txtConcepto.Value = "";
                    }
                }
                else
                {
                    MostrarMensaje("Error", "El total a pagar no puede ser cero (0) o el concepto no puede estar vacio.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
    }
}