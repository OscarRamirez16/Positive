using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using Idioma;
using HQSFramework.Base;
using System.Collections.Generic;

namespace Inventario
{
    public partial class frmCerrarCaja : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CuadreCaja.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        txtIngresos.Attributes.Add("nombreCampo", "Ingresos");
                        txtRetiros.Attributes.Add("nombreCampo", "Retiros");
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cerrar), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja));
            lblCaja.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja);
            lblSaldoInicial.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Saldo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Inicial));
            lblIngresos.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ingresos);
            lblRetiros.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Retiros);
            lblCompras.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Compra);
            lblPagosCreditos.Text = "Pagos de créditos";
            lblPropina.Text = "Propinas";
            lblRemision.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Remisiones);
            lblCreditos.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Credito);
            lblObservaciones.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Observaciones);
            lblPagoEfectivo.Text = "Pago en efectivo";
            lblOtrasFormasPago.Text = "Otras formas pago";
            CargarCajas(oCIdioma, Idioma);
        }

        private void CargarCajas(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblCajaBusiness oCajB = new tblCajaBusiness(CadenaConexion);
                string Opcion = ddlCaja.SelectedValue;
                ddlCaja.Items.Clear();
                ddlCaja.SelectedValue = null;
                ddlCaja.DataSource = oCajB.ObtenerCajaListaActivas(long.Parse(hddIdEmpresa.Value));
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

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try 
            {
                tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                CargarDatosCaja(oCuaI);
                oCuaI = oCuaB.ObtenerCuadreCajaLista(oCuaI);
                if (oCuaI.idCuadreCaja != 0)
                {
                    if (oUsuarioI.VerCuadreCaja)
                    {
                        //decimal TotalSalidas = 0;
                        List<tblMovimientosDiariosItem> oListMov = new List<tblMovimientosDiariosItem>();
                        List<tblTipoPagoItem> oListPagos = new List<tblTipoPagoItem>();
                        tblMovimientosDiariosBusiness oMovB = new tblMovimientosDiariosBusiness(CadenaConexion);
                        txtSaldoInicial.Text = oCuaI.SaldoInicial.ToString(Util.ObtenerFormatoDecimal());
                        txtObser.Value = oCuaI.Observaciones;
                        oListMov = oMovB.ObtenerMovimientosDiariosCuadre(oUsuarioI.idEmpresa, oCuaI.idUsuarioCaja);
                        txtIngresos.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        txtRetiros.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        if (oListMov.Count > 0)
                        {
                            foreach (tblMovimientosDiariosItem Item in oListMov)
                            {
                                if (Item.idTipoMovimiento == 1)
                                {
                                    txtRetiros.Text = Item.valor.ToString(Util.ObtenerFormatoDecimal());
                                    //TotalSalidas = TotalSalidas + Item.valor;
                                }
                                if (Item.idTipoMovimiento == 2)
                                {
                                    txtIngresos.Text = Item.valor.ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                        }
                        txtCompras.Text = oCuaB.ObtenerTotalComprasCuadre(oCuaI).ToString(Util.ObtenerFormatoDecimal());
                        //TotalSalidas = TotalSalidas + decimal.Parse(txtCompras.Text, System.Globalization.NumberStyles.Currency);
                        oListPagos = oCuaB.ObtenerFormasPagosVentas(oUsuarioI.idEmpresa, oCuaI.idUsuarioCaja);
                        txtEfectivo.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        txtTarjetaCredito.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        txtTarjetaDebito.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        txtCheque.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        txtBonos.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        txtConsignacion.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        txtNomina.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        txtRemision.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        txtPagoEfectivo.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        txtOtrasFormasPago.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                        decimal TotalVentas = 0;
                        decimal OtrasFormasPago = 0;
                        foreach (tblTipoPagoItem Item in oListPagos)
                        {
                            if (Item.idFormaPago == 1)
                            {
                                txtEfectivo.Text = ((Item.ValorPago + decimal.Parse(txtIngresos.Text, System.Globalization.NumberStyles.Currency)) - (decimal.Parse(txtRetiros.Text, System.Globalization.NumberStyles.Currency) + decimal.Parse(txtCompras.Text, System.Globalization.NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                txtPagoEfectivo.Text = Item.ValorPago.ToString(Util.ObtenerFormatoDecimal());
                                TotalVentas = TotalVentas + decimal.Parse(txtEfectivo.Text, System.Globalization.NumberStyles.Currency);
                            }
                            if (Item.idFormaPago == 2)
                            {
                                txtTarjetaCredito.Text = Item.ValorPago.ToString(Util.ObtenerFormatoDecimal());
                                OtrasFormasPago = OtrasFormasPago + Item.ValorPago;
                                TotalVentas = TotalVentas + Item.ValorPago;
                            }
                            if (Item.idFormaPago == 3)
                            {
                                txtTarjetaDebito.Text = Item.ValorPago.ToString(Util.ObtenerFormatoDecimal());
                                OtrasFormasPago = OtrasFormasPago + Item.ValorPago;
                                TotalVentas = TotalVentas + Item.ValorPago;
                            }
                            if (Item.idFormaPago == 4)
                            {
                                txtCheque.Text = Item.ValorPago.ToString(Util.ObtenerFormatoDecimal());
                                OtrasFormasPago = OtrasFormasPago + Item.ValorPago;
                                TotalVentas = TotalVentas + Item.ValorPago;
                            }
                            if (Item.idFormaPago == 5)
                            {
                                txtBonos.Text = Item.ValorPago.ToString(Util.ObtenerFormatoDecimal());
                                OtrasFormasPago = OtrasFormasPago + Item.ValorPago;
                                TotalVentas = TotalVentas + Item.ValorPago;
                            }
                            if (Item.idFormaPago == 6)
                            {
                                txtConsignacion.Text = Item.ValorPago.ToString(Util.ObtenerFormatoDecimal());
                                OtrasFormasPago = OtrasFormasPago + Item.ValorPago;
                                TotalVentas = TotalVentas + Item.ValorPago;
                            }
                            if (Item.idFormaPago == 7)
                            {
                                txtNomina.Text = Item.ValorPago.ToString(Util.ObtenerFormatoDecimal());
                                OtrasFormasPago = OtrasFormasPago + Item.ValorPago;
                                TotalVentas = TotalVentas + Item.ValorPago;
                            }
                        }
                        txtOtrasFormasPago.Text = OtrasFormasPago.ToString(Util.ObtenerFormatoDecimal());
                        txtPagoCreditos.Text = oMovB.ObtenerPagosFacturasACredito(oCuaI.idCuadreCaja, oCuaI.idUsuarioCaja, oUsuarioI.idEmpresa).ToString(Util.ObtenerFormatoDecimal());
                        txtPropina.Text = oMovB.ObtenerPropinaDocumento(oCuaI.idUsuarioCaja, oUsuarioI.idEmpresa).ToString(Util.ObtenerFormatoDecimal());
                        txtRemision.Text = oMovB.ObtenerValorRemisionesUsuario(oCuaI.idUsuarioCaja).ToString(Util.ObtenerFormatoDecimal());
                        txtCreditos.Text = oCuaB.ObtenerCreditos(oCuaI).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalCuadre.Text = (oCuaI.SaldoInicial + TotalVentas).ToString(Util.ObtenerFormatoDecimal());
                    }
                    else
                    {
                        MostrarMensaje("Aviso","No tiene autorización para ver los detalles del cuadre de caja, por favor proceda a cerrar la caja.");
                    }
                }
                else
                {
                    MostrarMensaje("Error", "La caja seleccionada no esta abierta.");
                    limpiarControles();
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }

        private void limpiarControles()
        {
            try
            {
                txtSaldoInicial.Text = "";
                txtIngresos.Text = "";
                txtRetiros.Text = "";
                txtEfectivo.Text = "";
                txtCompras.Text = "";
                txtTarjetaCredito.Text = "";
                txtTarjetaDebito.Text = "";
                txtCheque.Text = "";
                txtBonos.Text = "";
                txtConsignacion.Text = "";
                txtNomina.Text = "";
                txtObser.Value = "";
                txtCreditos.Text = "";
                txtRemision.Text = "";
                ddlCaja.SelectedValue = "0";
                ConfiguracionIdioma();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarDatosCaja(tblCuadreCajaItem Caja)
        {
            try
            {
                Caja.idCaja = long.Parse(ddlCaja.SelectedValue);
                Caja.Estado = true;
                Caja.idEmpresa = long.Parse(hddIdEmpresa.Value);
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

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                CargarDatosCaja(oCuaI);
                oCuaI = oCuaB.ObtenerCuadreCajaLista(oCuaI);
                if (oCuaI.idCuadreCaja != 0)
                {
                    CargarDatosGuardar(oCuaI);
                    if (oCuaB.Guardar(oCuaI))
                    {
                        if (oUsuarioI.VerCuadreCaja)
                        {
                            tblEmpresaItem oEmpI = new tblEmpresaItem();
                            tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                            oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                            string Remisiones = "";
                            if (oEmpI.MostrarRemisiones)
                            {
                                Remisiones = string.Format("<tr><td>Remisiones:</td><td style='text-align: right;'>{0}</td></tr>", oCuaI.TotalRemisiones.ToString(Util.ObtenerFormatoDecimal()));
                            }
                            else
                            {
                                if (oCuaI.TotalRemisiones > 0)
                                {
                                    Remisiones = "<tr><td>Remisiones:</td><td style='text-align: right;'>SI</td></tr>";
                                }
                                else
                                {
                                    Remisiones = "<tr><td>Remisiones:</td><td style='text-align: right;'>NO</td></tr>";
                                }
                            }
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
                            Remisiones +
                            "<tr><td>Propinas:</td><td style='text-align: right;'>{23}</td></tr>" +
                            "<tr><td>Creditos:</td><td style='text-align: right;'>{21}</td></tr>" +
                            "<tr><td>Pagos de Creditos:</td><td style='text-align: right;'>{22}</td></tr></table>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Observaciones: {16}</div>" +
                            "</div>", oEmpI.Nombre, oEmpI.Identificacion, oEmpI.Direccion, oEmpI.Telefono, oCuaI.SaldoInicial.ToString(Util.ObtenerFormatoDecimal()),
                            oCuaI.TotalIngresos.ToString(Util.ObtenerFormatoDecimal()), oCuaI.Efectivo.ToString(Util.ObtenerFormatoDecimal()),
                            oCuaI.TarjetaCredito.ToString(Util.ObtenerFormatoDecimal()), oCuaI.TarjetaDebito.ToString(Util.ObtenerFormatoDecimal()),
                            oCuaI.Cheques.ToString(Util.ObtenerFormatoDecimal()), oCuaI.Bonos.ToString(Util.ObtenerFormatoDecimal()),
                            oCuaI.Consignaciones.ToString(Util.ObtenerFormatoDecimal()), oCuaI.DescuentosNomina.ToString(Util.ObtenerFormatoDecimal()),
                            oCuaI.TotalRetiros.ToString(Util.ObtenerFormatoDecimal()), oCuaI.TotalCompras.ToString(Util.ObtenerFormatoDecimal()),
                            oCuaI.TotalCuadre.ToString(Util.ObtenerFormatoDecimal()), oCuaI.Observaciones.Replace(Environment.NewLine, " "), oCuaI.UsuarioCaja, oUsuarioI.NombreCompleto,
                            oCuaI.Fecha.ToString(), oCuaI.FechaCierre.ToString(), oCuaI.TotalCreditos.ToString(Util.ObtenerFormatoDecimal()),
                            oCuaI.PagoCreditos.ToString(Util.ObtenerFormatoDecimal()), txtPropina.Text, oCuaI.idCuadreCaja);
                            string strScript = string.Format("jQuery(document).ready(function(){{ ImprimirComprobanteCuadreCaja(\"{0}\",'1');}});", Mensaje);
                            if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
                            {
                                Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
                            }
                        }
                        else
                        {
                            MostrarAlerta(1, "Éxito", "La caja se cerro con Éxito.");
                            limpiarControles();
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", "No se pudo cerrar la caja.");
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", "La caja seleccionada no esta abierta.");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }

        private void CargarDatosGuardar(tblCuadreCajaItem Caja)
        {
            try
            {
                tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                CargarDatosCaja(oCuaI);
                oCuaI = oCuaB.ObtenerCuadreCajaLista(oCuaI);
                if (oCuaI.idCuadreCaja != 0)
                {
                    List<tblMovimientosDiariosItem> oListMov = new List<tblMovimientosDiariosItem>();
                    List<tblTipoPagoItem> oListPagos = new List<tblTipoPagoItem>();
                    tblMovimientosDiariosBusiness oMovB = new tblMovimientosDiariosBusiness(CadenaConexion);
                    oListMov = oMovB.ObtenerMovimientosDiariosCuadre(oUsuarioI.idEmpresa, oCuaI.idUsuarioCaja);
                    if (oListMov.Count > 0)
                    {
                        foreach (tblMovimientosDiariosItem Item in oListMov)
                        {
                            if (Item.idTipoMovimiento == 1)
                            {
                                Caja.TotalRetiros = Item.valor;
                            }
                            if (Item.idTipoMovimiento == 2)
                            {
                                Caja.TotalIngresos = Item.valor;
                            }
                        }
                    }
                    Caja.TotalCompras = oCuaB.ObtenerTotalComprasCuadre(oCuaI);
                    oListPagos = oCuaB.ObtenerFormasPagosVentas(oUsuarioI.idEmpresa, oCuaI.idUsuarioCaja);
                    decimal TotalVentas = 0;
                    foreach (tblTipoPagoItem Item in oListPagos)
                    {
                        if (Item.idFormaPago == 1)
                        {
                            Caja.Efectivo = ((Item.ValorPago + Caja.TotalIngresos) - (Caja.TotalRetiros + Caja.TotalCompras));
                            TotalVentas = TotalVentas + Caja.Efectivo;
                        }
                        if (Item.idFormaPago == 2)
                        {
                            Caja.TarjetaCredito = Item.ValorPago;
                            TotalVentas = TotalVentas + Item.ValorPago;
                        }
                        if (Item.idFormaPago == 3)
                        {
                            Caja.TarjetaDebito = Item.ValorPago;
                            TotalVentas = TotalVentas + Item.ValorPago;
                        }
                        if (Item.idFormaPago == 4)
                        {
                            Caja.Cheques = Item.ValorPago;
                            TotalVentas = TotalVentas + Item.ValorPago;
                        }
                        if (Item.idFormaPago == 5)
                        {
                            Caja.Bonos = Item.ValorPago;
                            TotalVentas = TotalVentas + Item.ValorPago;
                        }
                        if (Item.idFormaPago == 6)
                        {
                            Caja.Consignaciones = Item.ValorPago;
                            TotalVentas = TotalVentas + Item.ValorPago;
                        }
                        if (Item.idFormaPago == 7)
                        {
                            Caja.DescuentosNomina = Item.ValorPago;
                            TotalVentas = TotalVentas + Item.ValorPago;
                        }
                    }
                    Caja.TotalRemisiones = oMovB.ObtenerValorRemisionesUsuario(oCuaI.idUsuarioCaja);
                    Caja.TotalCreditos = oCuaB.ObtenerCreditos(oCuaI);
                    Caja.PagoCreditos = oMovB.ObtenerPagosFacturasACredito(oCuaI.idCuadreCaja, oCuaI.idUsuarioCaja, oUsuarioI.idEmpresa);
                    Caja.TotalCuadre = (oCuaI.SaldoInicial + TotalVentas);
                }
                Caja.idCaja = long.Parse(ddlCaja.SelectedValue);
                Caja.Estado = false;
                Caja.Observaciones = txtObser.Value.Replace(Environment.NewLine, " ");
                Caja.idEmpresa = oUsuarioI.idEmpresa;
                Caja.FechaCierre = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                Caja.idUsuarioCierre = oUsuarioI.idUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}