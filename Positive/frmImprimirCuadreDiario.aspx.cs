using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using HQSFramework.Base;
using Idioma;

namespace Inventario
{
    public partial class frmImprimirCuadreDiario : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgCuadreDiarioEnum
        {
            IdCuadreCaja = 0,
            UsuarioCaja = 1,
            Fecha = 2,
            FechaCierre = 3,
            SaldoInicial = 4,
            Observaciones = 5,
            Imprimir = 6
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VerFrmReporteCuadreDiario.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
                            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaInicial.ClientID);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaFinal.ClientID);
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
            lblUsuario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
            lblUsuario.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario);
            lblCaja.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja);
            CargarCajas(oCIdioma, Idioma);
            CargarUsuarios(oCIdioma, Idioma);
        }

        private void CargarCajas(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblCajaBusiness oCajB = new tblCajaBusiness(CadenaConexion);
                string Opcion = ddlCaja.SelectedValue;
                ddlCaja.Items.Clear();
                ddlCaja.SelectedValue = null;
                ddlCaja.DataSource = oCajB.ObtenerCajaListaPorIdEmpresa(oUsuarioI.idEmpresa);
                ddlCaja.DataBind();
                ddlCaja.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja)), "0"));
                if (!IsPostBack)
                {
                    tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                    tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                    oCuaI.idEmpresa = oUsuarioI.idEmpresa;
                    oCuaI.idUsuarioCaja = oUsuarioI.idUsuario;
                    oCuaI.Estado = true;
                    oCuaI = oCuaB.ObtenerCuadreCajaListaPorUsuario(oCuaI);
                    if (oCuaI.idCuadreCaja > 0)
                    {
                        ddlCaja.SelectedValue = oCuaI.idCaja.ToString();
                    }
                    else
                    {
                        ddlCaja.SelectedValue = "0";
                    }
                    if (!oUsuarioI.EsAdmin)
                    {
                        ddlCaja.Enabled = false;
                    }
                }
                else
                {
                    ddlCaja.SelectedValue = Opcion;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarUsuarios(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblUsuarioBusiness oUsuB = new tblUsuarioBusiness(CadenaConexion);
                string Opcion = ddlUsuario.SelectedValue;
                ddlUsuario.Items.Clear();
                ddlUsuario.SelectedValue = null;
                ddlUsuario.DataSource = oUsuB.ObtenerUsuarioListaPorIdEmpresa(oUsuarioI.idEmpresa);
                ddlUsuario.DataBind();
                ddlUsuario.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Usuario)), "0"));
                if (!IsPostBack)
                {
                    ddlUsuario.SelectedValue = oUsuarioI.idUsuario.ToString();
                    if (!oUsuarioI.EsAdmin)
                    {
                        ddlUsuario.Enabled = false;
                    }
                }
                else
                {
                    ddlUsuario.SelectedValue = Opcion;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblCuadreCajaBusiness oCuaCajB = new tblCuadreCajaBusiness(CadenaConexion);
                dgCuadreDiario.DataSource = oCuaCajB.ObtenerCuadreCajaListaReporte(DateTime.Parse(txtFechaInicial.Text), DateTime.Parse(txtFechaFinal.Text), oUsuarioI.idEmpresa, short.Parse(ddlCaja.SelectedValue), long.Parse(ddlUsuario.SelectedValue));
                dgCuadreDiario.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los cuadres diarios. {0}", ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void dgCuadreDiario_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if(e.CommandName == "Imprimir")
                {
                    tblCuadreCajaBusiness oCuaCajB = new tblCuadreCajaBusiness(CadenaConexion);
                    tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                    oCuaI = oCuaCajB.ObtenerCuadreCajaPorID(long.Parse(e.Item.Cells[dgCuadreDiarioEnum.IdCuadreCaja.GetHashCode()].Text));
                    tblEmpresaItem oEmpI = new tblEmpresaItem();
                    tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                    oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                    string Mensaje = "";
                    Mensaje = string.Format("<div style='position:relative;font-family:arial;'>" +
                    "<div style='font-size: 18px; font-weight: bold; width: 300px; padding-top: 20px; text-align: center;'>{0}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: center;'>Nit: {1}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: center;'>Direcci&oacute;n: {2}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: center;'>Telefono: {3}</div>" +
                    "<div style='font-size: 20px;font-weight: bold; padding-top: 5px; width: 300px; text-align:center;'>Comprobante cuadre de caja</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>ID: {24}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Usuario caja: {17}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Usuario cierre: {18}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Fecha: {19}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Fecha: {20}</div>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'><table border='1' style='width:100%'>" +
                    "<tr><td colspan='2' style='text-align:center;'>ENTRADAS</td></tr>" +
                    "<tr><td>Saldo inicial:</td><td style='text-align: right;'>{4}</td></tr>" +
                    "<tr><td>Ingresos:</td><td style='text-align: right;'>{5}</td></tr>" +
                    "<tr><td>Efectivo:</td><td style='text-align: right;'>{6}</td></tr>" +
                    "<tr><td>Tarjeta crédito:</td><td style='text-align: right;'>{7}</td></tr>" +
                    "<tr><td>Tarjeta debito:</td><td style='text-align: right;'>{8}</td></tr>" +
                    "<tr><td>Cheques:</td><td style='text-align: right;'>{9}</td></tr>" +
                    "<tr><td>Bonos:</td><td style='text-align: right;'>{10}</td></tr>" +
                    "<tr><td>Consignaciones:</td><td style='text-align: right;'>{11}</td></tr>" +
                    "<tr><td>Descuentos nómina:</td><td style='text-align: right;'>{12}</td></tr>" +
                    "<tr><td colspan='2' style='text-align:center;'>SALIDAS</td></tr>" +
                    "<tr><td>Retiros:</td><td style='text-align: right;'>{13}</td></tr>" +
                    "<tr><td>Compras:</td><td style='text-align: right;'>{14}</td></tr>" +
                    "<tr><td colspan='2' style='text-align:center;'>TOTAL CUADRE</td></tr>" +
                    "<tr><td>Total:</td><td style='text-align: right;'>{15}</td></tr>" +
                    "<tr><td colspan='2' style='text-align:center;'>INFORMATIVO</td></tr>" +
                    "<tr><td>Remisiones:</td><td style='text-align: right;'>{21}</td></tr>" +
                    "<tr><td>Creditos:</td><td style='text-align: right;'>{22}</td></tr>" +
                    "<tr><td>Pagos a Creditos:</td><td style='text-align: right;'>{23}</td></tr></table>" +
                    "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Observaciones: {16}</div>" +
                    "</div>", oEmpI.Nombre, oEmpI.Identificacion, oEmpI.Direccion, oEmpI.Telefono, oCuaI.SaldoInicial.ToString(Util.ObtenerFormatoDecimal()),
                    oCuaI.TotalIngresos.ToString(Util.ObtenerFormatoDecimal()), oCuaI.Efectivo.ToString(Util.ObtenerFormatoDecimal()),
                    oCuaI.TarjetaCredito.ToString(Util.ObtenerFormatoDecimal()), oCuaI.TarjetaDebito.ToString(Util.ObtenerFormatoDecimal()),
                    oCuaI.Cheques.ToString(Util.ObtenerFormatoDecimal()), oCuaI.Bonos.ToString(Util.ObtenerFormatoDecimal()),
                    oCuaI.Consignaciones.ToString(Util.ObtenerFormatoDecimal()), oCuaI.DescuentosNomina.ToString(Util.ObtenerFormatoDecimal()),
                    oCuaI.TotalRetiros.ToString(Util.ObtenerFormatoDecimal()), oCuaI.TotalCompras.ToString(Util.ObtenerFormatoDecimal()),
                    oCuaI.TotalCuadre.ToString(Util.ObtenerFormatoDecimal()), oCuaI.Observaciones.Replace(Environment.NewLine, " "), oCuaI.UsuarioCaja, oUsuarioI.NombreCompleto,
                    oCuaI.Fecha.ToString(), oCuaI.FechaCierre.ToString(), oCuaI.TotalRemisiones.ToString(Util.ObtenerFormatoDecimal()),
                    oCuaI.TotalCreditos.ToString(Util.ObtenerFormatoDecimal()), oCuaI.PagoCreditos.ToString(Util.ObtenerFormatoDecimal()), oCuaI.idCuadreCaja);
                    string strScript = string.Format("jQuery(document).ready(function(){{ ImprimirComprobanteCuadreCaja(\"{0}\",'2');}});", Mensaje);
                    if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
                    }
                }
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo imprimir el cuadre diario. {0}", ex.Message));
            }
        }
    }
}