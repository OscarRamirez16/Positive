using HQSFramework.Base;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioBusiness;
using InventarioItem;
using System.Globalization;
using System.Configuration;
using System.Collections.Generic;

namespace Inventario
{
    public partial class frmAnticipo : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private enum FormasDePagos
        {
            Efectivo = 1,
            TarjetaCredito = 2,
            TarjetaDebito = 3,
            Cheque = 4
        }

        private enum dgPagosColumnsEnum
        {
            IdFormaPago = 0,
            Nombre = 1,
            ValorPago = 2,
            NumeroCheque = 3
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    //ConfiguracionIdioma();
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Anticipos.GetHashCode().ToString()));
                    if (oRolPagI.Insertar && ValidarCajaAbiertaUsuario())
                    {
                        txtTotalPago.Attributes.Add("readonly", "readonly");
                        txtIdentificacion.Attributes.Add("onblur", string.Format("EstablecerAutoCompleteClientePorIdentificacion('{0}','Ashx/Tercero.ashx','{1}','{2}')", txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID));
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}','{3}');", strScript, txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                        if (!IsPostBack)
                        {
                            txtTotalPago.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoEntero());
                            CargarFormasPagos();
                        }
                    }
                    else
                    {
                        Response.Redirect("frmMantenimientos.aspx?Error=El usuario no tiene permisos o no tiene una caja abierta.");
                    }
                }
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }

        public void CargarFormasPagos()
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                dgPagos.DataSource = oDocB.ObtenerFormasPagosLista();
                dgPagos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidarCajaAbiertaUsuario()
        {
            try
            {
                tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                oCuaI.idEmpresa = oUsuarioI.idEmpresa;
                oCuaI.idUsuarioCaja = oUsuarioI.idUsuario;
                oCuaI.Estado = true;
                oCuaI = oCuaB.ObtenerCuadreCajaListaPorUsuario(oCuaI);
                if (oCuaI.idCuadreCaja == 0)
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

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hddIdCliente.Value) && hddIdCliente.Value != "0")
                {
                    tblTerceroItem oTerI = new tblTerceroItem();
                    tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                    oTerI = oTerB.ObtenerTercero(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa);
                    if (!oTerI.Generico)
                    {
                        tblPagoItem oPagI = new tblPagoItem();
                        List<tblTipoPagoItem> oTipPagLis = new List<tblTipoPagoItem>();
                        foreach (DataGridItem Item in dgPagos.Items)
                        {
                            if (((TextBox)(Item.Cells[dgPagosColumnsEnum.ValorPago.GetHashCode()].FindControl("txtValorPago"))).Text != "" && ((TextBox)(Item.Cells[dgPagosColumnsEnum.ValorPago.GetHashCode()].FindControl("txtValorPago"))).Text != "0")
                            {
                                tblTipoPagoItem oTipPagI = new tblTipoPagoItem();
                                oTipPagI.ValorPago = decimal.Parse(((TextBox)(Item.Cells[dgPagosColumnsEnum.ValorPago.GetHashCode()].FindControl("txtValorPago"))).Text, NumberStyles.Currency);
                                oTipPagI.idFormaPago = short.Parse(Item.Cells[dgPagosColumnsEnum.IdFormaPago.GetHashCode()].Text);
                                if (((TextBox)(Item.Cells[dgPagosColumnsEnum.NumeroCheque.GetHashCode()].FindControl("txtNumeroCheque"))).Text != "" && ((TextBox)(Item.Cells[dgPagosColumnsEnum.ValorPago.GetHashCode()].FindControl("txtNumeroCheque"))).Text != "0")
                                {
                                    oTipPagI.voucher = ((TextBox)(Item.Cells[dgPagosColumnsEnum.NumeroCheque.GetHashCode()].FindControl("txtNumeroCheque"))).Text;
                                }
                                else
                                {
                                    oTipPagI.voucher = "";
                                }
                                oPagI.totalPago = oPagI.totalPago + decimal.Parse(((TextBox)(Item.Cells[dgPagosColumnsEnum.ValorPago.GetHashCode()].FindControl("txtValorPago"))).Text, NumberStyles.Currency);
                                oTipPagLis.Add(oTipPagI);
                            }
                        }
                        if (oTipPagLis.Count > 0)
                        {
                            tblPagoBusiness oPagB = new tblPagoBusiness(CadenaConexion);
                            List<tblPagoDetalleItem> oListDet = new List<tblPagoDetalleItem>();
                            oPagI.fechaPago = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                            oPagI.idEmpresa = oUsuarioI.idEmpresa;
                            oPagI.idEstado = short.Parse(tblPagoBusiness.EstadoPago.Definitivo.GetHashCode().ToString());
                            oPagI.idUsuario = oUsuarioI.idUsuario;
                            oPagI.idTercero = long.Parse(hddIdCliente.Value);
                            oPagI.IdTipoPago = short.Parse(tblPagoBusiness.TipoPago.Anticipo.GetHashCode().ToString());
                            oPagI.Saldo = oPagI.totalPago;
                            if (!string.IsNullOrEmpty(txtObservaciones.Text))
                            {
                                oPagI.Observaciones = txtObservaciones.Text;
                            }
                            if (oPagB.GuardarPago(oPagI, oListDet, oTipPagLis, long.Parse(hddTipoDocumento.Value)))
                            {
                                string FormaPago = "";
                                foreach (DataGridItem Item in dgPagos.Items)
                                {
                                    if (((TextBox)(Item.Cells[dgPagosColumnsEnum.ValorPago.GetHashCode()].FindControl("txtValorPago"))).Text != "" && ((TextBox)(Item.Cells[dgPagosColumnsEnum.ValorPago.GetHashCode()].FindControl("txtValorPago"))).Text != "0")
                                    {
                                        FormaPago = FormaPago + "* Forma: " + Item.Cells[dgPagosColumnsEnum.Nombre.GetHashCode()].Text;
                                        FormaPago = FormaPago + " Valor: " + ((TextBox)(Item.Cells[dgPagosColumnsEnum.ValorPago.GetHashCode()].FindControl("txtValorPago"))).Text;
                                    }
                                }
                                Response.Redirect($"frmImprimirAnticipo.aspx?FormaPago={FormaPago}&idPago={oPagI.idPago}");
                            }
                            else
                            {
                                MostrarAlerta(0, "Error", "No se pudo guardar el anticipo.");
                            }
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", "No se pueden generar anticipos a clientes genericos.");
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", "Por favor seleccione un cliente valido.");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }

        protected void CalcularTotalPago(object sender, EventArgs e)
        {
            txtTotalPago.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
            foreach (DataGridItem Item in dgPagos.Items)
            {
                if (!string.IsNullOrEmpty(((TextBox)(Item.Cells[dgPagosColumnsEnum.ValorPago.GetHashCode()].FindControl("txtValorPago"))).Text))
                {
                    txtTotalPago.Text = (decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) + decimal.Parse(((TextBox)(Item.Cells[dgPagosColumnsEnum.ValorPago.GetHashCode()].FindControl("txtValorPago"))).Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                }
            }
        }

        protected void dgPagos_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                ((TextBox)(e.Item.Cells[dgPagosColumnsEnum.NumeroCheque.GetHashCode()].FindControl("txtValorPago"))).Attributes.Add("onblur", "CalcularTotalPago()");
                if (e.Item.Cells[dgPagosColumnsEnum.IdFormaPago.GetHashCode()].Text == FormasDePagos.Efectivo.GetHashCode().ToString())
                {
                    ((TextBox)(e.Item.Cells[dgPagosColumnsEnum.NumeroCheque.GetHashCode()].FindControl("txtNumeroCheque"))).Enabled = false;
                }
            }
        }
    }
}