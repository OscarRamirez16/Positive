using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Configuration;
using Idioma;
using HQSFramework.Base;
using System.Globalization;
using System.Data;

namespace Inventario
{
    public partial class frmPagos : PaginaBase
    {

        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgFormasPagosEnum
        {
            IdFormaPago = 0,
            FormaPago = 1,
            Valor = 2,
            Voucher = 3,
            IdTipoTarjetaCredito = 4,
            TarjetaCredito = 5,
            Eliminar = 6
        }

        private enum TipoPagoEnum
        {
            FacturaVenta = 1,
            FacturaCompra = 2,
            CuentaCobro = 10
        }

        private enum dgSaldosColumnsEnum
        {
            Seleccionar = 0,
            Tipo = 1,
            IdFormaPago = 2,
            Id = 3,
            NumeroDocumento = 4,
            Saldo = 5
        }

        private enum dgDocumentosColumnsEnum
        {
            Seleccionar = 0,
            Tipo = 1,
            idDocumento = 2,
            NumeroDocumento = 3,
            Fecha = 4,
            FechaVencimiento = 5,
            NombreTercero = 6,
            Referencia = 7,
            Observaciones = 8,
            TotalDocumento = 9,
            saldo = 10,
            nuevoSaldo = 11,
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    txtDevuelta.Attributes.Add("readonly", "readonly");
                    txtTotal.Attributes.Add("readonly", "readonly");
                    txtTotalPago.Attributes.Add("readonly", "readonly");
                    txtRestante.Attributes.Add("readonly", "readonly");
                    if (validarCajaAbierta() || oUsuarioI.EsAdmin)
                    {
                        SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                        oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Pagos.GetHashCode().ToString()));
                        if (oRolPagI.Leer)
                        {
                            ConfiguracionIdioma();
                            lblFechaActual.Text = Util.ObtenerFecha(oUsuarioI.idEmpresa).ToString();
                            if (!IsPostBack)
                            {
                                txtTotalPago.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                txtRestante.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                CargarFormasPagos();
                                txtDevuelta.Attributes.Add("class", "form-control");
                            }
                            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                            {
                                string strScript = "$(document).ready(function(){";
                                strScript = string.Format("{0} menu();", strScript);
                                strScript = string.Format("{0} pestañas();", strScript);
                                strScript = string.Format("{0} EstablecerTituloPagina('{1}');", strScript, lblTitulo.Text);
                                if (Request.QueryString["TipoPago"] == TipoPagoEnum.FacturaVenta.GetHashCode().ToString())
                                {
                                    hddTipoDocumento.Value = "1";
                                    strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}','{3}','{4}','{5}');", strScript, txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID, txtTelefono.ClientID, txtDireccion.ClientID);
                                    txtIdentificacion.Attributes.Add("onblur", string.Format("EstablecerAutoCompleteClientePorIdentificacion('{0}','Ashx/Tercero.ashx','{1}','{2}','{3}','{4}')", txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID, txtTelefono.ClientID, txtDireccion.ClientID));
                                }
                                if (Request.QueryString["TipoPago"] == TipoPagoEnum.FacturaCompra.GetHashCode().ToString())
                                {
                                    hddTipoDocumento.Value = "2";
                                    strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}','{3}','{4}','{5}');", strScript, txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID, txtTelefono.ClientID, txtDireccion.ClientID);
                                }
                                if (Request.QueryString["TipoPago"] == TipoPagoEnum.CuentaCobro.GetHashCode().ToString())
                                {
                                    hddTipoDocumento.Value = "10";
                                    strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}','{3}','{4}','{5}');", strScript, txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID, txtTelefono.ClientID, txtDireccion.ClientID);
                                    txtIdentificacion.Attributes.Add("onblur", string.Format("EstablecerAutoCompleteClientePorIdentificacion('{0}','Ashx/Tercero.ashx','{1}','{2}','{3}','{4}')", txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID, txtTelefono.ClientID, txtDireccion.ClientID));
                                }
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
                        divPrincipal.Visible = false;
                        MostrarAlerta(0, "Error", "El usuario no tiene relacionada una caja abierta");
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }   
                }
                else
                {
                    Response.Redirect("frmInicioSesion.aspx");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
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
            if (Request.QueryString["TipoPago"] == TipoPagoEnum.FacturaVenta.GetHashCode().ToString() || Request.QueryString["TipoPago"] == TipoPagoEnum.CuentaCobro.GetHashCode().ToString())
            {
                lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuentasCobrar);
                lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
            }
            if (Request.QueryString["TipoPago"] == TipoPagoEnum.FacturaCompra.GetHashCode().ToString())
            {
                lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CuentasPagar);
                lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Proveedor);
            }
            lblTituloGrilla.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Facturas);
            lblFecha.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Fecha);
            lblObservaciones.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Observaciones);
            lblIdentificacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion);
            lblTelefono.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Telefono);
            lblDireccion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Direccion);
            lblTotal.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Total);
            ConfigurarIdiomaGrillaDocumentos(oCIdioma, Idioma);
        }
        private void ConfigurarIdiomaGrillaDocumentos(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgDocumentos.Columns[dgDocumentosColumnsEnum.NumeroDocumento.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Numero);
                dgDocumentos.Columns[dgDocumentosColumnsEnum.Fecha.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Fecha);
                dgDocumentos.Columns[dgDocumentosColumnsEnum.NombreTercero.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio);
                dgDocumentos.Columns[dgDocumentosColumnsEnum.TotalDocumento.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Total);
                dgDocumentos.Columns[dgDocumentosColumnsEnum.saldo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Saldo);
                dgDocumentos.Columns[dgDocumentosColumnsEnum.nuevoSaldo.GetHashCode()].HeaderText = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nuevo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Saldo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool validarCajaAbierta()
        {
            try
            {
                tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(cadenaConexion);
                tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                oCuaI.idEmpresa = oUsuarioI.idEmpresa;
                oCuaI.idUsuarioCaja = oUsuarioI.idUsuario;
                oCuaI.Estado = true;
                if (oCuaB.ValidarCajaAbierta(oCuaI) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public void CargarFormasPagos()
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(cadenaConexion);
                ddlFormaPago.DataSource = oDocB.ObtenerFormasPagosLista();
                ddlFormaPago.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarFacturasPendientesPagos()
        {
            try
            {
                txtTotal.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(cadenaConexion);
                dgDocumentos.DataSource = oDocB.ObtenerFacturasPendientesPago(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa, short.Parse(hddTipoDocumento.Value));
                dgDocumentos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtTercero_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTercero.Text))
            {
                txtTotal.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoEntero());
                CargarFacturasPendientesPagos();
                CargarSaldosPendientesCliente();
                CalcularTotalPago();
            }
            else
            {
                txtIdentificacion.Text = "";
                txtTelefono.Text = "";
                txtDireccion.Text = "";
            }
        }
        public void CargarSaldosPendientesCliente()
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(cadenaConexion);
                dgSaldos.DataSource = oDocB.ObtenerSaldoDocumentoAFavorCliente(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa);
                dgSaldos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void dgDocumentos_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                txtTotal.Text = (decimal.Parse(txtTotal.Text, NumberStyles.Currency) + decimal.Parse(e.Item.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
            }
        }
        protected void CalcularTotalPago(object sender, EventArgs e)
        {
            txtTotalPago.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
            txtRestante.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
            foreach (DataGridItem Item in dgFormasPagos.Items)
            {
                txtTotalPago.Text = (decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
            }
            if ((decimal.Parse(txtTotal.Text, NumberStyles.Currency) - decimal.Parse(txtTotalPago.Text, NumberStyles.Currency)) < 0)
            {
                txtRestante.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
            }
            else
            {
                txtRestante.Text = (decimal.Parse(txtTotal.Text, NumberStyles.Currency) - decimal.Parse(txtTotalPago.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
            }
            if ((decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) - decimal.Parse(txtTotal.Text, NumberStyles.Currency)) < 0)
            {
                txtDevuelta.Attributes.Remove("class");
                txtDevuelta.Text = "Crédito";
            }
            else
            {
                txtDevuelta.Attributes.Add("class", "BoxValor form-control");
                txtDevuelta.Text = (decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) - decimal.Parse(txtTotal.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
            }
        }
        protected void btnAplicar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                decimal Acumulado = decimal.Parse(txtTotalPago.Text, NumberStyles.Currency);
                foreach (DataGridItem documento in dgDocumentos.Items)
                {
                    if (((CheckBox)(documento.Cells[dgDocumentosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                    {
                        if (documento.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text != "0")
                        {
                            btnAplicar.Visible = false;
                            Acumulado = Acumulado - decimal.Parse(documento.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text, NumberStyles.Currency);
                            if (Acumulado <= 0)
                            {
                                documento.Cells[dgDocumentosColumnsEnum.nuevoSaldo.GetHashCode()].Text = (Acumulado * -1).ToString();
                                break;
                            }
                            else
                            {
                                documento.Cells[dgDocumentosColumnsEnum.nuevoSaldo.GetHashCode()].Text = "0";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("Error al querer aplicar el pago. {0}", ex.Message));
            }
        }
        private bool ValidarSeleccionarDocumento()
        {
            bool Result = false;
            foreach (DataGridItem documento in dgDocumentos.Items)
            {
                if (((CheckBox)(documento.Cells[dgDocumentosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                {
                    Result = true;
                }
            }
            return Result;
        }
        private bool ValidarPagoConTarjetas()
        {
            try
            {
                decimal ValorAPagar = 0;
                decimal ValorPagoConTarjeta = 0;
                foreach (DataGridItem Item in dgDocumentos.Items)
                {
                    if (((CheckBox)(Item.Cells[dgDocumentosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                    {
                        ValorAPagar = Math.Round((ValorAPagar + decimal.Parse(Item.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text, NumberStyles.Currency)), 0);
                    }
                }
                foreach (DataGridItem Item in dgFormasPagos.Items)
                {
                    if (Item.Cells[dgFormasPagosEnum.IdFormaPago.GetHashCode()].Text == tblDocumentoBusiness.FormasPagoEnum.TarjetaCredito.GetHashCode().ToString() || Item.Cells[dgFormasPagosEnum.IdFormaPago.GetHashCode()].Text == tblDocumentoBusiness.FormasPagoEnum.TarjetaDebito.GetHashCode().ToString())
                    {
                        ValorPagoConTarjeta = ValorPagoConTarjeta + decimal.Parse(Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text);
                    }
                }
                if (ValorPagoConTarjeta <= ValorAPagar)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnPagar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Pagos.GetHashCode().ToString()));
                if (oRolPagI.Insertar)
                {
                    if (validarCajaAbierta() && ValidarSeleccionarDocumento())
                    {
                        if (ValidarPagoConTarjetas())
                        {
                            if (hddIdCliente.Value != "0")
                            {
                                tblPagoItem oPagoI = new tblPagoItem();
                                List<tblPagoDetalleItem> oPagDetLis = new List<tblPagoDetalleItem>();
                                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(cadenaConexion);
                                List<tblTipoPagoItem> oTipPagLis = new List<tblTipoPagoItem>();
                                decimal Saldo = 0;
                                decimal TotalAPagar = 0;
                                string PagoDetalle = string.Empty;
                                foreach (DataGridItem Item in dgDocumentos.Items)
                                {
                                    if (((CheckBox)(Item.Cells[dgDocumentosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                                    {
                                        Saldo = Math.Round((Saldo + decimal.Parse(Item.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text, NumberStyles.Currency)), 0);
                                    }
                                }
                                TotalAPagar = Saldo;
                                decimal Acumulado = decimal.Parse(txtTotalPago.Text, NumberStyles.Currency);
                                if (dgSaldos.Items.Count > 0)
                                {
                                    foreach (DataGridItem Item in dgSaldos.Items)
                                    {
                                        if (((CheckBox)(Item.Cells[dgSaldosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                                        {
                                            if (Saldo > 0)
                                            {
                                                tblTipoPagoItem oTipPagI = new tblTipoPagoItem();
                                                oTipPagI.idFormaPago = short.Parse(Item.Cells[dgSaldosColumnsEnum.IdFormaPago.GetHashCode()].Text);
                                                oTipPagI.voucher = Item.Cells[dgSaldosColumnsEnum.Id.GetHashCode()].Text;
                                                if (decimal.Parse(Item.Cells[dgSaldosColumnsEnum.Saldo.GetHashCode()].Text, NumberStyles.Currency) > Saldo)
                                                {
                                                    oTipPagI.ValorPago = Saldo;
                                                    Saldo = Saldo - oTipPagI.ValorPago;
                                                }
                                                else
                                                {
                                                    oTipPagI.ValorPago = decimal.Parse(Item.Cells[dgSaldosColumnsEnum.Saldo.GetHashCode()].Text, NumberStyles.Currency);
                                                    Saldo = Saldo - oTipPagI.ValorPago;
                                                }
                                                oPagoI.totalPago = oPagoI.totalPago + decimal.Parse(Item.Cells[dgSaldosColumnsEnum.Saldo.GetHashCode()].Text, NumberStyles.Currency);
                                                oTipPagLis.Add(oTipPagI);
                                            }
                                        }
                                    }
                                }
                                foreach (DataGridItem Item in dgFormasPagos.Items)
                                {
                                    tblTipoPagoItem oTipPagI = new tblTipoPagoItem();
                                    oTipPagI.idFormaPago = short.Parse(Item.Cells[dgFormasPagosEnum.IdFormaPago.GetHashCode()].Text);
                                    if (oTipPagI.idFormaPago == tblDocumentoBusiness.FormasPagoEnum.TarjetaCredito.GetHashCode())
                                    {
                                        oTipPagI.ValorPago = decimal.Parse(Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text, NumberStyles.Currency);
                                        oTipPagI.voucher = Item.Cells[dgFormasPagosEnum.Voucher.GetHashCode()].Text;
                                        oTipPagI.idTipoTarjetaCredito = short.Parse(Item.Cells[dgFormasPagosEnum.IdTipoTarjetaCredito.GetHashCode()].Text);
                                    }
                                    else if (oTipPagI.idFormaPago != tblDocumentoBusiness.FormasPagoEnum.Efectivo.GetHashCode())
                                    {
                                        oTipPagI.ValorPago = decimal.Parse(Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text, NumberStyles.Currency);
                                        oTipPagI.voucher = Item.Cells[dgFormasPagosEnum.Voucher.GetHashCode()].Text;
                                    }
                                    else
                                    {
                                        if (txtDevuelta.Text != "Crédito" && decimal.Parse(txtDevuelta.Text, NumberStyles.Currency) > 0)
                                        {
                                            oTipPagI.ValorPago = decimal.Parse(Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text, NumberStyles.Currency);
                                            oTipPagI.ValorPago = oTipPagI.ValorPago - decimal.Parse(txtDevuelta.Text, NumberStyles.Currency);
                                        }
                                        else
                                        {
                                            oTipPagI.ValorPago = decimal.Parse(Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text, NumberStyles.Currency);
                                        }
                                    }
                                    oPagoI.totalPago = oPagoI.totalPago + oTipPagI.ValorPago;
                                    oTipPagLis.Add(oTipPagI);
                                }
                                if (oPagoI.totalPago > TotalAPagar)
                                {
                                    oPagoI.totalPago = TotalAPagar;
                                }
                                if (oTipPagLis.Count > 0)
                                {
                                    oPagoI.idTercero = long.Parse(hddIdCliente.Value);
                                    oPagoI.idEmpresa = oUsuarioI.idEmpresa;
                                    oPagoI.fechaPago = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                                    oPagoI.idUsuario = oUsuarioI.idUsuario;
                                    oPagoI.idEstado = short.Parse(tblPagoBusiness.EstadoPago.Definitivo.GetHashCode().ToString());
                                    oPagoI.EnCuadre = false;
                                    oPagoI.Observaciones = txtObser.Text;
                                    oPagoI.IdTipoPago = short.Parse(tblPagoBusiness.TipoPago.PagoNormal.GetHashCode().ToString());
                                    foreach (DataGridItem Item in dgDocumentos.Items)
                                    {
                                        if (((CheckBox)(Item.Cells[dgDocumentosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                                        {
                                            PagoDetalle = PagoDetalle + "* Num. Documento: " + Item.Cells[dgDocumentosColumnsEnum.NumeroDocumento.GetHashCode()].Text + " Valor: " + decimal.Parse(Item.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                            tblPagoDetalleItem oPagDetI = new tblPagoDetalleItem();
                                            oPagDetI.idDocumento = long.Parse(Item.Cells[dgDocumentosColumnsEnum.idDocumento.GetHashCode()].Text);
                                            oPagDetI.NumeroDocumento = Item.Cells[dgDocumentosColumnsEnum.NumeroDocumento.GetHashCode()].Text;
                                            if (Acumulado < decimal.Parse(Item.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text, NumberStyles.Currency))
                                            {
                                                oPagDetI.valorAbono = Acumulado;
                                                Acumulado = 0;
                                            }
                                            else
                                            {
                                                oPagDetI.valorAbono = decimal.Parse(Item.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text, NumberStyles.Currency);
                                                Acumulado = Acumulado - decimal.Parse(Item.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text, NumberStyles.Currency);
                                            }
                                            oPagDetLis.Add(oPagDetI);
                                        }
                                    }
                                }
                                if (oPagoI.totalPago > 0)
                                {
                                    if (oDocB.GuardarPago(oPagoI, oPagDetLis, oTipPagLis, long.Parse(hddTipoDocumento.Value)))
                                    {
                                        if (oPagoI.idPago > 0)
                                        {
                                            Response.Redirect($"frmImprimirCuentasPorCobrarPagar.aspx?TipoPago={Request.QueryString["TipoPago"]}&idPago={oPagoI.idPago}&PagoDetalle={PagoDetalle}&TotalPagar={txtTotal.Text}&Tipo={hddTipoDocumento.Value}");
                                        }
                                        else
                                        {
                                            MostrarAlerta(0, "Error", "El pago no se pudo realizar.");
                                        }
                                    }
                                    else
                                    {
                                        MostrarAlerta(0, "Error", "El pago no se pudo realizar.");
                                    }
                                }
                                else
                                {
                                    MostrarAlerta(0, "Error", "El valor del pago debe ser mayor a 0.");
                                }
                            }
                            else
                            {
                                MostrarAlerta(0, "Error", "Debe seleccionar un cliente.");
                            }
                        }
                        else
                        {
                            MostrarAlerta(0, "Error", "El valor a pagar por medio de tarjetas no puede sobre pasar el valor del pago total de los documentos.");
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", "Debe tener una caja abierta o seleccionar un documento.");
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", "No tiene permiso de crear pagos.");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }

        protected void CalcularPagoTotal(object sender, EventArgs e)
        {
            try
            {
                txtTotal.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                decimal TotalPago = 0;
                foreach (DataGridItem Item in dgDocumentos.Items)
                {
                    if (((CheckBox)(Item.Cells[dgDocumentosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                    {
                        TotalPago = Math.Round((TotalPago + decimal.Parse(Item.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text, NumberStyles.Currency)), 0);
                    }
                }
                txtTotal.Text = TotalPago.ToString(Util.ObtenerFormatoDecimal());
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
        }

        private void LimpiarControles()
        {
            try
            {
                btnAplicar.Visible = true;
                CargarFormasPagos();
                dgDocumentos.DataSource = null;
                dgDocumentos.DataBind();
                txtTercero.Text = "";
                hddIdCliente.Value = "0";
                txtDireccion.Text = "";
                txtTelefono.Text = "";
                txtIdentificacion.Text = "";
                txtTotalPago.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtTotal.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlFormaPago.SelectedValue == tblDocumentoBusiness.FormasPagoEnum.Efectivo.GetHashCode().ToString())
                {
                    divEfectivo.Visible = true;
                    divOtros.Visible = false;
                    divTarjetaCredito.Visible = false;
                }
                else if (ddlFormaPago.SelectedValue == tblDocumentoBusiness.FormasPagoEnum.TarjetaCredito.GetHashCode().ToString())
                {
                    CargarTipoTarjetaCredito();
                    divEfectivo.Visible = false;
                    divOtros.Visible = false;
                    divTarjetaCredito.Visible = true;
                }
                else
                {
                    divEfectivo.Visible = false;
                    divOtros.Visible = true;
                    divTarjetaCredito.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }

        private void CargarColumnasFormaPago(ref DataTable dt)
        {
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IdFormaPago";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "FormaPago";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Valor";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Voucher";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "IdTipoTarjetaCredito";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TarjetaCredito";
            dt.Columns.Add(column);
        }

        protected void btnAdicionarPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSeleccionarDocumento())
                {
                    txtTotalPago.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                    DataTable dt = new DataTable();
                    if (dgFormasPagos.Items.Count > 0)
                    {
                        CargarColumnasFormaPago(ref dt);
                        foreach (DataGridItem Item in dgFormasPagos.Items)
                        {
                            DataRow copia;
                            copia = dt.NewRow();
                            copia["IdFormaPago"] = Item.Cells[dgFormasPagosEnum.IdFormaPago.GetHashCode()].Text;
                            copia["FormaPago"] = Item.Cells[dgFormasPagosEnum.FormaPago.GetHashCode()].Text;
                            copia["Valor"] = Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text;
                            copia["Voucher"] = Item.Cells[dgFormasPagosEnum.Voucher.GetHashCode()].Text;
                            copia["IdTipoTarjetaCredito"] = Item.Cells[dgFormasPagosEnum.IdTipoTarjetaCredito.GetHashCode()].Text;
                            copia["TarjetaCredito"] = Item.Cells[dgFormasPagosEnum.TarjetaCredito.GetHashCode()].Text;
                            dt.Rows.Add(copia);
                        }
                    }
                    else
                    {
                        CargarColumnasFormaPago(ref dt);
                    }
                    DataRow row;
                    row = dt.NewRow();
                    row["IdFormaPago"] = ddlFormaPago.SelectedValue;
                    row["FormaPago"] = ddlFormaPago.SelectedItem.Text;
                    if (ddlFormaPago.SelectedValue == tblDocumentoBusiness.FormasPagoEnum.Efectivo.GetHashCode().ToString())
                    {
                        row["Valor"] = decimal.Parse(txtValorE.Text, NumberStyles.Currency).ToString();
                        row["Voucher"] = "";
                        row["IdTipoTarjetaCredito"] = "";
                        row["TarjetaCredito"] = "";
                    }
                    else if (ddlFormaPago.SelectedValue == tblDocumentoBusiness.FormasPagoEnum.TarjetaCredito.GetHashCode().ToString())
                    {
                        row["Valor"] = decimal.Parse(txtValorTJ.Text, NumberStyles.Currency).ToString();
                        row["Voucher"] = txtVoucherTJ.Text;
                        row["IdTipoTarjetaCredito"] = ddlTipoTarjeta.SelectedValue;
                        row["TarjetaCredito"] = ddlTipoTarjeta.SelectedItem.Text;
                    }
                    else
                    {
                        row["Valor"] = decimal.Parse(txtValorO.Text, NumberStyles.Currency).ToString();
                        row["Voucher"] = txtVoucherO.Text;
                        row["IdTipoTarjetaCredito"] = "";
                        row["TarjetaCredito"] = "";
                    }
                    dt.Rows.Add(row);
                    ddlFormaPago.Focus();
                    dgFormasPagos.DataSource = dt;
                    dgFormasPagos.DataBind();
                    LimpiarControlesFormasPago();
                    CalcularTotalPago();
                }
                else
                {
                    MostrarAlerta(0, "Error", "Primero debe seleccionar un documento.");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }

        protected void CalcularTotalPago()
        {
            txtTotalPago.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
            txtRestante.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
            decimal ValorAPagar = 0;
            foreach (DataGridItem Item in dgFormasPagos.Items)
            {
                txtTotalPago.Text = (decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
            }
            foreach (DataGridItem Item in dgSaldos.Items)
            {
                if (((CheckBox)(Item.Cells[dgSaldosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                {
                    txtTotalPago.Text = Math.Round((decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgSaldosColumnsEnum.Saldo.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoEntero());
                }
            }
            foreach (DataGridItem Item in dgDocumentos.Items)
            {
                if (((CheckBox)(Item.Cells[dgDocumentosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                {
                    ValorAPagar = Math.Round((ValorAPagar + decimal.Parse(Item.Cells[dgDocumentosColumnsEnum.saldo.GetHashCode()].Text, NumberStyles.Currency)), 0);
                }
            }
            if ((ValorAPagar - decimal.Parse(txtTotalPago.Text, NumberStyles.Currency)) < 0)
            {
                txtRestante.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoEntero());
            }
            else
            {
                txtRestante.Text = Math.Round((ValorAPagar - decimal.Parse(txtTotalPago.Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoEntero());
            }
            if ((decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) - ValorAPagar) < 0)
            {
                txtDevuelta.Attributes.Remove("class");
                txtDevuelta.Attributes.Add("class", "form-control");
                txtDevuelta.Text = "Crédito";
            }
            else
            {
                txtDevuelta.Attributes.Add("class", "BoxValor form-control");
                txtDevuelta.Text = Math.Round((decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) - ValorAPagar), 0).ToString(Util.ObtenerFormatoEntero());
            }
        }

        protected void AdicionarPago(object sender, EventArgs e)
        {
            if (ValidarSeleccionarDocumento())
            {
                CalcularTotalPago();
            }
            else
            {
                MostrarAlerta(0, "Error", "Primero debe seleccionar un documento.");
                CargarSaldosPendientesCliente();
            }
        }

        protected void dgFormasPagos_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    txtTotalPago.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                    DataTable dt = new DataTable();
                    CargarColumnasFormaPago(ref dt);
                    foreach (DataGridItem Item in dgFormasPagos.Items)
                    {
                        if (Item.ItemIndex != e.Item.ItemIndex)
                        {
                            DataRow row;
                            row = dt.NewRow();
                            row["IdFormaPago"] = Item.Cells[dgFormasPagosEnum.IdFormaPago.GetHashCode()].Text;
                            row["FormaPago"] = Item.Cells[dgFormasPagosEnum.FormaPago.GetHashCode()].Text;
                            row["Valor"] = Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text;
                            row["Voucher"] = Item.Cells[dgFormasPagosEnum.Voucher.GetHashCode()].Text;
                            row["IdTipoTarjetaCredito"] = Item.Cells[dgFormasPagosEnum.IdTipoTarjetaCredito.GetHashCode()].Text;
                            row["TarjetaCredito"] = Item.Cells[dgFormasPagosEnum.TarjetaCredito.GetHashCode()].Text;
                            dt.Rows.Add(row);
                        }
                    }
                    dgFormasPagos.DataSource = dt;
                    dgFormasPagos.DataBind();
                    CalcularTotalPago();
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }

        public void CargarTipoTarjetaCredito()
        {
            try
            {
                tblPagoBusiness oPBiz = new tblPagoBusiness(cadenaConexion);
                ddlTipoTarjeta.DataSource = oPBiz.ObtenerTipoTarjetaCreditoLista("", oUsuarioI.idEmpresa, true);
                ddlTipoTarjeta.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarControlesFormasPago()
        {
            try
            {
                CargarFormasPagos();
                CargarTipoTarjetaCredito();
                txtValorE.Text = "";
                txtValorTJ.Text = "";
                txtValorO.Text = "";
                txtVoucherO.Text = "";
                txtVoucherTJ.Text = "";
                divEfectivo.Visible = true;
                divTarjetaCredito.Visible = false;
                divOtros.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void chkVerTodos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkVerTodos.Checked)
                {
                    txtRestante.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                    txtTotal.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                    LimpiarControles();
                    LimpiarControlesFormasPago();
                    CargarFacturasPendientesPagos();
                    CalcularTotalPago();
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
    }
}