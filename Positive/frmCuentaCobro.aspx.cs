using HQSFramework.Base;
using InventarioBusiness;
using InventarioItem;
using System;
using System.Configuration;
using System.Web.UI;

namespace Inventario
{
    public partial class frmCuentaCobro : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CuentaCobro.GetHashCode().ToString()));
                if (oRolPagI.Leer)
                {
                    hddIdUsuario.Value = oUsuarioI.idUsuario.ToString();
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    tblCuentaCobroBusiness oCuenB = new tblCuentaCobroBusiness(CadenaConexion);
                    txtNumero.Text = oCuenB.ObtenerNumeroCuentaCobro(oUsuarioI.idEmpresa);
                    if (!IsPostBack)
                    {
                        txtFecha.Text = DateTime.Now.ToShortDateString();
                    }
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0} ConfigurarEnter();", strScript);
                        strScript = string.Format("{0} EstablecerTituloPagina('{1}');", strScript, "Cuenta de Cobro");
                        strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFecha.ClientID);
                        strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}','{3}','{4}','{5}','{6}','{7}');", strScript, txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID, txtTelefono.ClientID, txtDireccion.ClientID, txtCiudad.ClientID, hddIdCiudad.ClientID);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        txtIdentificacion.Attributes.Add("onblur", string.Format("EstablecerAutoCompleteClientePorIdentificacionDocumentos('{0}','Ashx/Tercero.ashx','{1}','{2}','{3}','{4}','{5}','{6}')", txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID, txtTelefono.ClientID, txtDireccion.ClientID, txtCiudad.ClientID, hddIdCiudad.ClientID));
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (oRolPagI.Insertar)
                {
                    tblCuentaCobroItem oCuenI = new tblCuentaCobroItem();
                    tblCuentaCobroBusiness oCuenB = new tblCuentaCobroBusiness(CadenaConexion);
                    oCuenI.Numero = long.Parse(txtNumero.Text);
                    oCuenI.Fecha = DateTime.Parse(txtFecha.Text);
                    oCuenI.IdTercero = long.Parse(hddIdCliente.Value);
                    oCuenI.IdUsuario = oUsuarioI.idUsuario;
                    oCuenI.Concepto = txtConcepto.Value;
                    oCuenI.Total = decimal.Parse(txtTotal.Text);
                    oCuenI.IdEstado = 1;
                    oCuenI.IdEmpresa = oUsuarioI.idEmpresa;
                    oCuenI.Saldo = oCuenI.Total;
                    string Error = oCuenB.Guardar(oCuenI);
                    if (string.IsNullOrEmpty(Error))
                    {
                        MostrarMensaje("Exito", "La cuenta de cobro se generó con éxito.");
                        txtNumero.Text = oCuenB.ObtenerNumeroCuentaCobro(oUsuarioI.idEmpresa);
                        txtFecha.Text = DateTime.Now.ToShortDateString();
                        hddIdCiudad.Value = "";
                        hddIdCliente.Value = "";
                        hddIdCiudad.Value = "";
                        txtTercero.Text = "";
                        txtTelefono.Text = "";
                        txtDireccion.Text = "";
                        txtConcepto.Value = "";
                        txtTotal.Text = "";
                        txtIdentificacion.Text = "";
                        txtCiudad.Text = "";
                    }
                    else
                    {
                        MostrarMensaje("Error", Error);
                    }
                }
                else
                {
                    MostrarMensaje("Error","No tiene permisos para guardar cuentas de cobro");
                } 
            }
            catch(Exception ex)
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