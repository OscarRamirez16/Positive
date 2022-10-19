using System;
using InventarioBusiness;
using InventarioItem;
using Idioma;
using HQSFramework.Base;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Globalization;
using eFacturacionColombia_V2.Documentos;
using eFacturacionColombia_V2.ServiciosWeb;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using eFacturacionColombia_V2.Util;
using eFacturacionColombia_V2.ServiciosWeb.DianServices;
using eFacturacionColombia_V2.Tipos.Standard;
using eFacturacionColombia_V2.Firma;
using System.Net;
using System.Threading.Tasks;
using System.Net.Mail;
using Newtonsoft.Json;
using System.IO;
using System.Net.Mime;
using System.Linq;
using eFacturacionColombia_V2.Pdf;

namespace Inventario
{
    public partial class frmDocumentos : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        SeguridadBusiness.paginasEnum PaginaID;
        private string Documento = "";
        private enum dgRetencionesEnum
        {
            Id = 0,
            Codigo = 1,
            Descripcion = 2,
            Porcentaje = 3,
            Base = 4,
            Seleccionar = 5
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
        private enum dgFacturaEnum
        {
            IdArticulo = 0,
            Codigo = 1,
            Articulo = 2,
            IdBodega = 3,
            Bodega = 4,
            Cantidad = 5,
            ValorUnitario = 6,
            PrecioVenta = 7,
            Descuento = 8,
            ValorUnitarioConDescuento = 9,
            IVA = 10,
            ValorUnitarioConIVA = 11,
            TotalLinea = 12,
            Eliminar = 13,
            CostoPonderado = 14,
            ValorDescuento = 15,
            PrecioCosto = 16
        }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    if (oUsuarioI.Impoconsumo > 0)
                    {
                        MostrarControl(trImpoconsumo.ClientID);
                    }
                    else
                    {
                        OcultarControl(trImpoconsumo.ClientID);
                    }
                    if (oUsuarioI.ManejaDescuentoConIVA)
                    {
                        dgFactura.Columns[dgFacturaEnum.Descuento.GetHashCode()].Visible = false;
                        dgFactura.Columns[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Visible = false;
                    }
                    if (Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Compra.GetHashCode().ToString() || Request.QueryString["TipoDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString())
                    {
                        MostrarControl(tblFormasPagos.ClientID);
                        MostrarControl(lblDevuelta.ClientID);
                        MostrarControl(txtDevuelta.ClientID);
                        if (!IsPostBack)
                        {
                            CargarRetenciones();
                        }
                        if (Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString())
                        {
                            MostrarControl(imgPrecios.ClientID);
                            MostrarControl(trPropina.ClientID);
                            txtCodigo.Focus();
                        }
                        else
                        {
                            OcultarControl(imgPrecios.ClientID);
                            OcultarControl(trPropina.ClientID);
                            txtTercero.Focus();
                        }
                        if (Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Compra.GetHashCode().ToString())
                        {
                            dgFactura.Columns[dgFacturaEnum.PrecioVenta.GetHashCode()].Visible = true;
                        }
                        else
                        {
                            dgFactura.Columns[dgFacturaEnum.PrecioVenta.GetHashCode()].Visible = false;
                        }
                    }
                    else if (Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                    {
                        MostrarControl(imgPrecios.ClientID);
                        MostrarControl(trPropina.ClientID);
                        dgFactura.Columns[dgFacturaEnum.PrecioVenta.GetHashCode()].Visible = false;
                        txtCodigo.Focus();
                    }
                    else
                    {
                        dgFactura.Columns[dgFacturaEnum.PrecioVenta.GetHashCode()].Visible = false;
                        OcultarControl(tblFormasPagos.ClientID);
                        OcultarControl(lblDevuelta.ClientID);
                        OcultarControl(txtDevuelta.ClientID);
                        OcultarControl(trPropina.ClientID);
                        OcultarControl(imgPrecios.ClientID);
                    }
                    if (!oUsuarioI.ModificaPrecio)
                    {
                        txtPrecio.Attributes.Add("readonly", "readonly");
                    }
                    txtDevuelta.Attributes.Add("readonly", "readonly");
                    txtAntesIVA.Attributes.Add("readonly", "readonly");
                    txtTotalRetencion.Attributes.Add("readonly", "readonly");
                    txtTotalIVA.Attributes.Add("readonly", "readonly");
                    txtTotalFactura.Attributes.Add("readonly", "readonly");
                    txtTotalPago.Attributes.Add("readonly", "readonly");
                    txtRestante.Attributes.Add("readonly", "readonly");
                    txtTotalDescuento.Attributes.Add("readonly", "readonly");
                    txtPropina.Attributes.Add("readonly", "readonly");
                    txtImpoconsumo.Attributes.Add("readonly", "readonly");
                    hddIdUsuario.Value = oUsuarioI.idUsuario.ToString();
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    hddBodegaPorDefectoUsuario.Value = oUsuarioI.idBodega.ToString();
                    if (ValidarCajaAbierta() || Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.EntradaMercancia.GetHashCode().ToString() || Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.SalidaMercancia.GetHashCode().ToString() || Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Compra.GetHashCode().ToString() || Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString() || Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoCompra.GetHashCode().ToString())
                    {
                        ConfiguracionIdioma();
                        SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                        oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(PaginaID.GetHashCode().ToString()));
                        if (oRolPagI.Insertar)
                        {
                            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                            {
                                string strScript = "$(document).ready(function(){";
                                strScript = string.Format("{0} menu();", strScript);
                                strScript = string.Format("{0} ConfigurarEnter();", strScript);
                                strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaVen.ClientID);
                                strScript = string.Format("{0} EstablecerTituloPagina('{1}');", strScript, lblTipoDocumento.Text);
                                if (oRolPagI.Leer && Request.QueryString["consulta"] == "1" && !string.IsNullOrEmpty(Request.QueryString["opcionDocumento"]))
                                {
                                    OcultarControl(divValidador.ClientID);
                                    OcultarControl(divDescuento.ClientID);
                                    OcultarControl(btnAdicionar.ClientID);
                                    OcultarControl(btnLogin.ClientID);
                                    OcultarControl(btnDescuento.ClientID);
                                    OcultarControl(btnCalcularDescuento.ClientID);
                                    if (!IsPostBack)
                                    {
                                        CargarVendedores();
                                        CargarRetenciones();
                                        tblDocumentoItem oDocI = new tblDocumentoItem();
                                        tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                                        oDocI = oDocB.traerDocumentoPorId(long.Parse(Request.QueryString["opcionDocumento"]), long.Parse(Request.QueryString["idDocumento"]));
                                        CargarEncabezadoDocumento(oDocI);
                                        dgFactura.DataSource = oDocI.DocumentoLineas;
                                        dgFactura.DataBind();
                                    }
                                    if (!string.IsNullOrEmpty(Request.QueryString["TipoDocumento"]))
                                    {
                                        btnGuardar.Visible = true;
                                        btnFacturar.Visible = false;
                                        btnPasar.Visible = false;
                                        txtFechaVen.Text = DateTime.Now.ToShortDateString();
                                        txtDescuento.Text = "0";
                                        if (!IsPostBack)
                                        {
                                            CargarFormasPagos();
                                            CargarTipoTarjetaCredito();
                                        }
                                        if (Request.QueryString["TipoDocumento"] == tblTipoDocumentoItem.TipoDocumentoEnum.venta.GetHashCode().ToString())
                                        {
                                            MostrarControl(trPropina.ClientID);
                                            if (string.IsNullOrEmpty(txtPropina.Text))
                                            {
                                                txtPropina.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                            }
                                            else
                                            {
                                                chkPropina.Checked = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Request.QueryString["opcionDocumento"] == tblTipoDocumentoItem.TipoDocumentoEnum.cotizacion.GetHashCode().ToString())
                                        {
                                            btnFacturar.Visible = true;
                                            btnFacturar.ToolTip = "Pasar a Remisión";
                                            btnPasar.Visible = true;
                                            btnGuardar.Visible = true;
                                            txtDescuento.Text = "0";
                                            txtCodigo.Attributes.Add("onblur", string.Format("traerArticuloPorCodigoOCodigoBarra('{0}','Ashx/Articulo.ashx','{1}','{2}','{3}','{4}',2,'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')", txtCodigo.ClientID, hddIdArticulo.ClientID, txtArticulo.ClientID, txtPrecio.ClientID, hddIVA.ClientID, hddCantidad.ClientID, hddIdBodega.ClientID, txtBodega.ClientID, hddEsInventario.ClientID, hddCostoPonderado.ClientID, hddPrecioCosto.ClientID, oUsuarioI.PosicionInicialCodigo, oUsuarioI.LongitudCodigo, oUsuarioI.PosicionInicialCantidad, oUsuarioI.LongitudCantidad, txtCantidad.ClientID, txtDescuento.ClientID));
                                            txtPrecio.Attributes.Add("onblur", "AdicionarArticulo()");
                                            strScript = string.Format("{0} EstablecerAutoCompleteArticulo('{1}','Ashx/Articulo.ashx','{2}','{3}','{4}','{5}',1,'{6}','{7}','{8}','{9}','{10}','{11}');", strScript, txtArticulo.ClientID, hddIdArticulo.ClientID, txtCodigo.ClientID, txtPrecio.ClientID, hddIVA.ClientID, hddCantidad.ClientID, hddIdBodega.ClientID, txtBodega.ClientID, hddEsInventario.ClientID, hddCostoPonderado.ClientID, hddPrecioCosto.ClientID);
                                            strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','2','{4}','{5}');", strScript, txtBodega.ClientID, hddIdBodega.ClientID, oUsuarioI.idEmpresa, hddIdArticulo.ClientID, hddCantidad.ClientID);
                                        }
                                        else if (Request.QueryString["opcionDocumento"] == tblTipoDocumentoItem.TipoDocumentoEnum.Remision.GetHashCode().ToString())
                                        {
                                            btnFacturar.Visible = true;
                                            btnFacturar.ToolTip = "Pasar a Factura";
                                            btnPasar.Visible = false;
                                            btnGuardar.Visible = true;
                                            txtDescuento.Text = "0";
                                            txtCodigo.Attributes.Add("onblur", string.Format("traerArticuloPorCodigoOCodigoBarra('{0}','Ashx/Articulo.ashx','{1}','{2}','{3}','{4}',2,'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')", txtCodigo.ClientID, hddIdArticulo.ClientID, txtArticulo.ClientID, txtPrecio.ClientID, hddIVA.ClientID, hddCantidad.ClientID, hddIdBodega.ClientID, txtBodega.ClientID, hddEsInventario.ClientID, hddCostoPonderado.ClientID, hddPrecioCosto.ClientID, oUsuarioI.PosicionInicialCodigo, oUsuarioI.LongitudCodigo, oUsuarioI.PosicionInicialCantidad, oUsuarioI.LongitudCantidad, txtCantidad.ClientID, txtDescuento.ClientID));
                                            txtPrecio.Attributes.Add("onblur", "AdicionarArticulo()");
                                            strScript = string.Format("{0} EstablecerAutoCompleteArticulo('{1}','Ashx/Articulo.ashx','{2}','{3}','{4}','{5}',1,'{6}','{7}','{8}','{9}','{10}','{11}');", strScript, txtArticulo.ClientID, hddIdArticulo.ClientID, txtCodigo.ClientID, txtPrecio.ClientID, hddIVA.ClientID, hddCantidad.ClientID, hddIdBodega.ClientID, txtBodega.ClientID, hddEsInventario.ClientID, hddCostoPonderado.ClientID, hddPrecioCosto.ClientID);
                                            strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','2','{4}','{5}');", strScript, txtBodega.ClientID, hddIdBodega.ClientID, oUsuarioI.idEmpresa, hddIdArticulo.ClientID, hddCantidad.ClientID);
                                        }
                                        else
                                        {
                                            btnFacturar.Visible = false;
                                            btnGuardar.Visible = false;
                                        }
                                        CargarFormasPagos();
                                        CargarTipoTarjetaCredito();
                                    }
                                }
                                else
                                {
                                    OcultarControl(btnAdicionar.ClientID);
                                    OcultarControl(btnLogin.ClientID);
                                    OcultarControl(btnDescuento.ClientID);
                                    OcultarControl(btnCalcularDescuento.ClientID);
                                    OcultarControl(divDescuento.ClientID);
                                    if (string.IsNullOrEmpty(hddIdEliminar.Value))
                                    {
                                        OcultarControl(divValidador.ClientID);
                                    }
                                    if (!IsPostBack)
                                    {
                                        txtFechaVen.Text = DateTime.Now.ToShortDateString();
                                        txtDescuento.Text = "0";
                                        txtTotalFactura.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        txtTotalIVA.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        txtAntesIVA.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        txtRestante.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        txtTotalPago.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        txtTotalDescuento.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        txtPropina.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        txtImpoconsumo.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        txtTotalRetencion.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        CargarFormasPagos();
                                        CargarTipoTarjetaCredito();
                                        CargarVendedores();
                                        txtDevuelta.Attributes.Add("class", "form-control");
                                    }
                                    txtCodigo.Attributes.Add("onblur", string.Format("traerArticuloPorCodigoOCodigoBarra('{0}','Ashx/Articulo.ashx','{1}','{2}','{3}','{4}',2,'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')", txtCodigo.ClientID, hddIdArticulo.ClientID, txtArticulo.ClientID, txtPrecio.ClientID, hddIVA.ClientID, hddCantidad.ClientID, hddIdBodega.ClientID, txtBodega.ClientID, hddEsInventario.ClientID, hddCostoPonderado.ClientID, hddPrecioCosto.ClientID, oUsuarioI.PosicionInicialCodigo, oUsuarioI.LongitudCodigo, oUsuarioI.PosicionInicialCantidad, oUsuarioI.LongitudCantidad, txtCantidad.ClientID, txtDescuento.ClientID));
                                    txtIdentificacion.Attributes.Add("onblur", string.Format("EstablecerAutoCompleteClientePorIdentificacionDocumentos('{0}','Ashx/Tercero.ashx','{1}','{2}','{3}','{4}','{5}','{6}')", txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID, txtTelefono.ClientID, txtDireccion.ClientID, txtCiudad.ClientID, hddIdCiudad.ClientID));
                                    txtPrecio.Attributes.Add("onblur", "AdicionarArticulo()");
                                    strScript = string.Format("{0} EstablecerAutoCompleteCiudad('{1}','Ashx/Ciudad.ashx','{2}');", strScript, txtCiudad.ClientID, hddIdCiudad.ClientID);
                                    strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}','{3}','{4}','{5}','{6}','{7}');", strScript, txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID, txtTelefono.ClientID, txtDireccion.ClientID, txtCiudad.ClientID, hddIdCiudad.ClientID);
                                    strScript = string.Format("{0} EstablecerAutoCompleteArticulo('{1}','Ashx/Articulo.ashx','{2}','{3}','{4}','{5}',1,'{6}','{7}','{8}','{9}','{10}','{11}');", strScript, txtArticulo.ClientID, hddIdArticulo.ClientID, txtCodigo.ClientID, txtPrecio.ClientID, hddIVA.ClientID, hddCantidad.ClientID, hddIdBodega.ClientID, txtBodega.ClientID, hddEsInventario.ClientID, hddCostoPonderado.ClientID, hddPrecioCosto.ClientID);
                                    strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','2','{4}','{5}');", strScript, txtBodega.ClientID, hddIdBodega.ClientID, oUsuarioI.idEmpresa, hddIdArticulo.ClientID, hddCantidad.ClientID);
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
                        MostrarAlerta(0, "Error", "El usuario no tiene una caja abierta, la caja venció su resolución o el tipo de documento no es correcto.");
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} pestañas();", strScript);
                            strScript = string.Format("{0} ConfigurarEnter();", strScript);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                        btnGuardar.Visible = false;
                    }
                }
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
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
        protected void btnCrearTercero_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                tblTerceroItem oTerI = new tblTerceroItem();
                oTerI = (tblTerceroItem)(Session["DatosTercero"]);
                hddIdCliente.Value = oTerI.IdTercero.ToString();
                txtTercero.Text = oTerI.Nombre;
                txtIdentificacion.Text = oTerI.Identificacion;
                txtDireccion.Text = oTerI.Direccion;
                txtTelefono.Text = oTerI.Telefono + " " + oTerI.Celular;
                txtCodigo.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarVendedores()
        {
            try
            {
                tblVendedorBusiness oVenB = new tblVendedorBusiness(CadenaConexion);
                ddlVendedor.DataSource = oVenB.ObtenerVendedorListaActivos(oUsuarioI.idEmpresa);
                ddlVendedor.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarTipoTarjetaCredito()
        {
            try
            {
                tblPagoBusiness oPBiz = new tblPagoBusiness(CadenaConexion);
                ddlTipoTarjeta.DataSource = oPBiz.ObtenerTipoTarjetaCreditoLista("", oUsuarioI.idEmpresa, true);
                ddlTipoTarjeta.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarFormasPagos()
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                ddlFormaPago.DataSource = oDocB.ObtenerFormasPagosLista();
                ddlFormaPago.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarEncabezadoDocumento(tblDocumentoItem documento)
        {
            try
            {
                if (documento.idDocumento != 0)
                {
                    hddIdDocumento.Value = documento.idDocumento.ToString();
                    lblEstado.Text = string.Format("Estado: {0}", documento.Estado);
                    lblEstado.Visible = true;
                    tblCiudadBusiness oCiuB = new tblCiudadBusiness(CadenaConexion);
                    tblCiudadItem oCiuI = new tblCiudadItem();
                    oCiuI = oCiuB.ObtenerCiudad(documento.idCiudad);
                    tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                    tblTerceroItem oTerI = new tblTerceroItem();
                    oTerI = oTerB.ObtenerTercero(documento.idTercero, oUsuarioI.idEmpresa);
                    hddIdCliente.Value = documento.idTercero.ToString();
                    hddIdCiudad.Value = documento.idCiudad.ToString();
                    txtCiudad.Text = oCiuI.Nombre;
                    txtCiudad.Enabled = false;
                    txtTercero.Text = documento.NombreTercero;
                    txtTercero.Enabled = false;
                    txtIdentificacion.Text = oTerI.Identificacion.ToString();
                    txtIdentificacion.Enabled = false;
                    txtTelefono.Text = documento.Telefono;
                    txtDireccion.Text = documento.Direccion;
                    lblNumeroFactura.Text = documento.NumeroDocumento;
                    txtTotalFactura.Text = documento.TotalDocumento.ToString(Util.ObtenerFormatoDecimal());
                    txtTotalIVA.Text = documento.TotalIVA.ToString(Util.ObtenerFormatoDecimal());
                    txtAntesIVA.Text = documento.TotalAntesIVA.ToString(Util.ObtenerFormatoDecimal());
                    txtTotalRetencion.Text = documento.TotalRetenciones.ToString(Util.ObtenerFormatoDecimal());
                    txtPropina.Text = documento.Propina.ToString(Util.ObtenerFormatoDecimal());
                    txtTotalDescuento.Text = documento.TotalDescuento.ToString(Util.ObtenerFormatoDecimal());
                    txtObservaciones.Text = documento.Observaciones;
                    txtImpoconsumo.Text = documento.Impoconsumo.ToString(Util.ObtenerFormatoDecimal());
                    ddlVendedor.SelectedValue = documento.IdVendedor.ToString();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidarCajaAbierta()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["consulta"]) || Request.QueryString["TipoDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString())
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
                        tblCajaItem oCajaI = new tblCajaItem();
                        tblCajaBusiness oCajaB = new tblCajaBusiness(CadenaConexion);
                        oCajaI = oCajaB.ObtenerCajaPorID(oCuaI.idCaja);
                        hddResolucion.Value = oCajaI.Resolucion;
                        List<tblCajaItem> oListCajaI = new List<tblCajaItem>();
                        oListCajaI = oCajaB.ObtenerCajaProximaVencer(oUsuarioI.idEmpresa);
                        if (oListCajaI.Count > 0)
                        {
                            if(oListCajaI.Where(x => x.idCaja == oCajaI.idCaja && ((x.FechaVencimiento - DateTime.Now).Days <= 0)).ToList().Count() > 0)
                            {
                                Response.Redirect(string.Format("frmMantenimientos.aspx?Error=La caja {0} venció, fecha de vencimiento {1}", oCajaI.nombre, oCajaI.FechaVencimiento.ToShortDateString()));
                            }
                        }
                        return true;
                    }
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
        private bool ValidarCajaAbiertaSinConsulta()
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
                    tblCajaItem oCajaI = new tblCajaItem();
                    tblCajaBusiness oCajaB = new tblCajaBusiness(CadenaConexion);
                    oCajaI = oCajaB.ObtenerCajaPorID(oCuaI.idCaja);
                    hddResolucion.Value = oCajaI.Resolucion;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private void ConfiguracionIdioma()
        {
            try
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
                lblCiudad.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ciudad);
                lblIdentificacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion);
                lblTelefono.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Telefono);
                lblDireccion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Direccion);
                lblArticulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
                lblObservaciones.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Observaciones);
                lblBodega.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega);
                lblDevuelta.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Devuelta);
                CargarOpcionFactura(oUsuarioI, oCIdioma, Idioma);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void AdicionarPago(object sender, EventArgs e)
        {
            CalcularTotalPago();
        }
        protected void CalcularTotalPago()
        {
            try
            {
                txtTotalPago.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                txtRestante.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                foreach (DataGridItem Item in dgFormasPagos.Items)
                {
                    txtTotalPago.Text = Math.Round((decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text, NumberStyles.Currency)), 2).ToString(Util.ObtenerFormatoDecimal());
                }
                foreach (DataGridItem Item in dgSaldos.Items)
                {
                    if (((CheckBox)(Item.Cells[dgSaldosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                    {
                        txtTotalPago.Text = Math.Round((decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgSaldosColumnsEnum.Saldo.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                    }
                }
                if ((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) - decimal.Parse(txtTotalPago.Text, NumberStyles.Currency)) < 0)
                {
                    txtRestante.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                }
                else
                {
                    txtRestante.Text = Math.Round((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) - decimal.Parse(txtTotalPago.Text, NumberStyles.Currency)), 2).ToString(Util.ObtenerFormatoDecimal());
                }
                if ((decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) - decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency)) < 0)
                {
                    txtDevuelta.Attributes.Remove("class");
                    txtDevuelta.Attributes.Add("class", "form-control");
                    txtDevuelta.Text = "Crédito";
                }
                else
                {
                    txtDevuelta.Attributes.Add("class", "BoxValor form-control");
                    txtDevuelta.Text = Math.Round((decimal.Parse(txtTotalPago.Text, NumberStyles.Currency) - decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency)), 2).ToString(Util.ObtenerFormatoDecimal());
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void CargarOpcionFactura(tblUsuarioItem usuario, Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                lblDetalles.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo);
                lblNumero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Numero);
                if (tblTipoDocumentoItem.TipoDocumentoEnum.venta.GetHashCode().ToString() == Request.QueryString["opcionDocumento"] || Request.QueryString["TipoDocumento"] == tblTipoDocumentoItem.TipoDocumentoEnum.venta.GetHashCode().ToString())
                {
                    Documento = "Factura de venta POS";
                    lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FacturaVenta);
                    lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
                    //lblPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ValorUnitario);
                    hddTipoDocumento.Value = "1";
                    PaginaID = SeguridadBusiness.paginasEnum.Ventas;
                    tblNumeracionFacturaVentaBusiness oNumBiz = new tblNumeracionFacturaVentaBusiness(CadenaConexion);
                    tblNumeracionFacturaVentaItem oNumItem = oNumBiz.ObtenerNumeracionFacturaVenta(usuario.idEmpresa, usuario.idUsuario);
                    if (oNumItem.idNumeracionFacturaVenta > 0 || Request.QueryString["consulta"] == "1")
                    {
                        tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                        tblEmpresaItem oEmpI = new tblEmpresaItem();
                        oEmpI = oEmpB.ObtenerEmpresa(usuario.idEmpresa);
                        lblNumeroFactura.Text = oNumItem.ProximoValor;
                        tblCiudadItem oCiuI = new tblCiudadItem();
                        tblCiudadBusiness oCiuB = new tblCiudadBusiness(CadenaConexion);
                        oCiuI = oCiuB.ObtenerCiudad(usuario.idCiudad);
                        txtCiudad.Text = oCiuI.Nombre;
                        hddIdCiudad.Value = oCiuI.idCiudad.ToString();
                        if (!IsPostBack)
                        {
                            tblTerceroItem oTerI = new tblTerceroItem();
                            tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                            oTerI = oTerB.ObtenerTerceroPorIdentificacion("123456789", long.Parse(hddIdEmpresa.Value));
                            hddIdCliente.Value = oTerI.IdTercero.ToString();
                            txtTercero.Text = oTerI.Nombre;
                            txtIdentificacion.Text = oTerI.Identificacion;
                            txtDireccion.Text = oTerI.Direccion;
                            txtTelefono.Text = oTerI.Telefono + " " + oTerI.Celular;
                        }
                    }
                    else
                    {
                        divPrincipal.Visible = false;
                        MostrarMensaje("Error", "Excedio la numeración de la factura. Por favor amplie el rango de numeración");
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }
                }
                if (tblTipoDocumentoItem.TipoDocumentoEnum.cotizacion.GetHashCode().ToString() == Request.QueryString["opcionDocumento"] && Request.QueryString["TipoDocumento"] == null)
                {
                    Documento = "Cotización";
                    lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cotizacion);
                    lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
                    //lblPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ValorUnitario);
                    hddTipoDocumento.Value = "3";
                    PaginaID = SeguridadBusiness.paginasEnum.Cotizaciones;
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                    lblNumeroFactura.Text = oDocB.obtenerNumeracionDocumento(usuario.idEmpresa, long.Parse(hddTipoDocumento.Value)).ToString();
                    tblCiudadItem oCiuI = new tblCiudadItem();
                    tblCiudadBusiness oCiuB = new tblCiudadBusiness(CadenaConexion);
                    oCiuI = oCiuB.ObtenerCiudad(usuario.idCiudad);
                    txtCiudad.Text = oCiuI.Nombre;
                    hddIdCiudad.Value = oCiuI.idCiudad.ToString();
                    if (!IsPostBack)
                    {
                        tblTerceroItem oTerI = new tblTerceroItem();
                        tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                        oTerI = oTerB.ObtenerTerceroPorIdentificacion("123456789", long.Parse(hddIdEmpresa.Value));
                        hddIdCliente.Value = oTerI.IdTercero.ToString();
                        txtTercero.Text = oTerI.Nombre;
                        txtIdentificacion.Text = oTerI.Identificacion;
                        txtDireccion.Text = oTerI.Direccion;
                        txtTelefono.Text = oTerI.Telefono + " " + oTerI.Celular;
                    }
                }
                if (tblTipoDocumentoItem.TipoDocumentoEnum.compra.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
                {
                    Documento = "Factura de compra";
                    lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.FacturaCompra);
                    lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Proveedor);
                    //lblPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Costo);
                    hddTipoDocumento.Value = "2";
                    PaginaID = SeguridadBusiness.paginasEnum.Compras;
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                    lblNumeroFactura.Text = oDocB.obtenerNumeracionDocumento(usuario.idEmpresa, long.Parse(hddTipoDocumento.Value)).ToString();
                }
                if (tblTipoDocumentoItem.TipoDocumentoEnum.notaCreditoVenta.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
                {
                    Documento = "Nota credito venta";
                    lblTipoDocumento.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nota), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Credito), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ventas));
                    lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
                    //lblPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Precio);
                    hddTipoDocumento.Value = "4";
                    PaginaID = SeguridadBusiness.paginasEnum.NotaCreditoVenta;
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                    lblNumeroFactura.Text = oDocB.obtenerNumeracionDocumento(usuario.idEmpresa, long.Parse(hddTipoDocumento.Value)).ToString();
                }
                if (tblTipoDocumentoItem.TipoDocumentoEnum.entradaMercancia.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
                {
                    Documento = "Entrada de mercancía";
                    lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.EntradaMercacia);
                    lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Proveedor);
                    //lblPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Costo);
                    hddTipoDocumento.Value = "5";
                    PaginaID = SeguridadBusiness.paginasEnum.EntradaMercancia;
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                    lblNumeroFactura.Text = oDocB.obtenerNumeracionDocumento(usuario.idEmpresa, long.Parse(hddTipoDocumento.Value)).ToString();
                }
                if (tblTipoDocumentoItem.TipoDocumentoEnum.salidaMercancia.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
                {
                    Documento = "Salida de mercancía";
                    lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SalidaMercancia);
                    lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Proveedor);
                    //lblPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Costo);
                    hddTipoDocumento.Value = "6";
                    PaginaID = SeguridadBusiness.paginasEnum.SalidaMercancia;
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                    lblNumeroFactura.Text = oDocB.obtenerNumeracionDocumento(usuario.idEmpresa, long.Parse(hddTipoDocumento.Value)).ToString();
                }
                if (tblTipoDocumentoItem.TipoDocumentoEnum.notaCreditoCompra.GetHashCode().ToString() == Request.QueryString["opcionDocumento"])
                {
                    Documento = "Nota credito compra";
                    lblTipoDocumento.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nota), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Credito), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Compra));
                    lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Proveedor);
                    //lblPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Costo);
                    hddTipoDocumento.Value = "7";
                    PaginaID = SeguridadBusiness.paginasEnum.NotaCreditoCompra;
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                    lblNumeroFactura.Text = oDocB.obtenerNumeracionDocumento(usuario.idEmpresa, long.Parse(hddTipoDocumento.Value)).ToString();
                }
                if ((tblTipoDocumentoItem.TipoDocumentoEnum.Remision.GetHashCode().ToString() == Request.QueryString["opcionDocumento"] && string.IsNullOrEmpty(Request.QueryString["TipoDocumento"])) || Request.QueryString["TipoDocumento"] == tblTipoDocumentoItem.TipoDocumentoEnum.Remision.GetHashCode().ToString())
                {
                    Documento = "Remisión";
                    lblTipoDocumento.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Remision);
                    lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
                    //lblPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ValorUnitario);
                    hddTipoDocumento.Value = "8";
                    PaginaID = SeguridadBusiness.paginasEnum.Remisiones;
                    tblNumeracionFacturaVentaBusiness oNumBiz = new tblNumeracionFacturaVentaBusiness(CadenaConexion);
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                    long NumeroDocumento = oDocB.obtenerNumeracionDocumento(usuario.idEmpresa, long.Parse(hddTipoDocumento.Value));
                    if (NumeroDocumento > 0)
                    {
                        tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                        tblEmpresaItem oEmpI = new tblEmpresaItem();
                        oEmpI = oEmpB.ObtenerEmpresa(usuario.idEmpresa);
                        lblNumeroFactura.Text = NumeroDocumento.ToString();
                        tblCiudadItem oCiuI = new tblCiudadItem();
                        tblCiudadBusiness oCiuB = new tblCiudadBusiness(CadenaConexion);
                        oCiuI = oCiuB.ObtenerCiudad(usuario.idCiudad);
                        txtCiudad.Text = oCiuI.Nombre;
                        hddIdCiudad.Value = oCiuI.idCiudad.ToString();
                        if (!IsPostBack)
                        {
                            tblTerceroItem oTerI = new tblTerceroItem();
                            tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                            oTerI = oTerB.ObtenerTerceroPorIdentificacion("123456789", long.Parse(hddIdEmpresa.Value));
                            hddIdCliente.Value = oTerI.IdTercero.ToString();
                            txtTercero.Text = oTerI.Nombre;
                            txtIdentificacion.Text = oTerI.Identificacion;
                            txtDireccion.Text = oTerI.Direccion;
                            txtTelefono.Text = oTerI.Telefono + " " + oTerI.Celular;
                        }
                    }
                    else
                    {
                        divPrincipal.Visible = false;
                        MostrarMensaje("Error", "Excedio la numeración de la factura. Por favor amplie el rango de numeración");
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }
                }
                if (tblTipoDocumentoItem.TipoDocumentoEnum.DocumentoElectronico.GetHashCode().ToString() == Request.QueryString["opcionDocumento"] || Request.QueryString["TipoDocumento"] == tblTipoDocumentoItem.TipoDocumentoEnum.DocumentoElectronico.GetHashCode().ToString())
                {
                    Documento = "Documento Electronico";
                    lblTipoDocumento.Text = "Documento Electronico";
                    lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
                    //lblPrecio.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.ValorUnitario);
                    hddTipoDocumento.Value = "1";
                    PaginaID = SeguridadBusiness.paginasEnum.Ventas;
                    tblNumeracionFacturaVentaBusiness oNumBiz = new tblNumeracionFacturaVentaBusiness(CadenaConexion);
                    tblNumeracionFacturaVentaItem oNumItem = oNumBiz.ObtenerNumeracionFacturaVenta(usuario.idEmpresa, usuario.idUsuario);
                    if (oNumItem.idNumeracionFacturaVenta > 0 || Request.QueryString["consulta"] == "1")
                    {
                        tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                        tblEmpresaItem oEmpI = new tblEmpresaItem();
                        oEmpI = oEmpB.ObtenerEmpresa(usuario.idEmpresa);
                        lblNumeroFactura.Text = oNumItem.ProximoValor;
                        tblCiudadItem oCiuI = new tblCiudadItem();
                        tblCiudadBusiness oCiuB = new tblCiudadBusiness(CadenaConexion);
                        oCiuI = oCiuB.ObtenerCiudad(usuario.idCiudad);
                        txtCiudad.Text = oCiuI.Nombre;
                        hddIdCiudad.Value = oCiuI.idCiudad.ToString();
                        if (!IsPostBack)
                        {
                            tblTerceroItem oTerI = new tblTerceroItem();
                            tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                            oTerI = oTerB.ObtenerTerceroPorIdentificacion("123456789", long.Parse(hddIdEmpresa.Value));
                            hddIdCliente.Value = oTerI.IdTercero.ToString();
                            txtTercero.Text = oTerI.Nombre;
                            txtIdentificacion.Text = oTerI.Identificacion;
                            txtDireccion.Text = oTerI.Direccion;
                            txtTelefono.Text = oTerI.Telefono + " " + oTerI.Celular;
                        }
                    }
                    else
                    {
                        divPrincipal.Visible = false;
                        MostrarMensaje("Error", "Excedio la numeración de la factura. Por favor amplie el rango de numeración");
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void CargarSaldosPendientesCliente()
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                dgSaldos.DataSource = oDocB.ObtenerSaldoDocumentoAFavorCliente(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa);
                dgSaldos.DataBind();
                tblListaPrecioBusiness oListP = new tblListaPrecioBusiness(CadenaConexion);
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                tblListaPrecioItem oListPre = new tblListaPrecioItem();
                oListPre = oListP.ObtenerListaPrecioPorID(oTerB.ObtenerTercero(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa).IdListaPrecio);
                lblListaPrecio.Text = oListPre.Nombre;
                dgFactura.DataSource = null;
                dgFactura.DataBind();
                if (oListPre.Factor > 0)
                {
                    hddDescuento.Value = oListPre.Factor.ToString(Util.ObtenerFormatoEntero());
                    txtDescuento.Text = oListPre.Factor.ToString(Util.ObtenerFormatoEntero());
                }
                txtAntesIVA.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtTotalIVA.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtTotalFactura.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                LimpiarControlesFormasPago();
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
                CargarSaldosPendientesCliente();
            }
        }
        protected void btnAdicionar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCantidad.Text) && decimal.Parse(txtCantidad.Text, NumberStyles.Currency) > 0)
                {
                    if (decimal.Parse(txtDescuento.Text, NumberStyles.Currency) > 0)
                    {
                        SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                        oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Descuentos.GetHashCode().ToString()));
                        if (oRolPagI.Insertar)
                        {
                            if (decimal.Parse(txtDescuento.Text, NumberStyles.Currency) > oUsuarioI.PorcentajeDescuento)
                            {
                                MostrarAlerta(0, "Error", "El descuento otorgado sobre pasa el valor permitido por el usuario");
                            }
                            else
                            {
                                OcultarControl(divDescuento.ClientID);
                                decimal Descuento = (decimal.Parse(txtDescuento.Text, NumberStyles.Currency) / 100);
                                if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                                {
                                    if (oUsuarioI.ManejaPrecioConIVA)
                                    {
                                        if (oUsuarioI.ManejaDescuentoConIVA)
                                        {
                                            hddValorDescuento.Value = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) * Descuento).ToString(Util.ObtenerFormatoDecimal());
                                        }
                                        else
                                        {
                                            hddValorDescuento.Value = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))) * Descuento).ToString(Util.ObtenerFormatoDecimal());
                                        }
                                    }
                                    else
                                    {
                                        hddValorDescuento.Value = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) * Descuento)).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                }
                                else
                                {
                                    if (oUsuarioI.ManejaCostoConIVA)
                                    {
                                        hddValorDescuento.Value = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) * (1 - (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))) * Descuento).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                    else
                                    {
                                        hddValorDescuento.Value = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) * Descuento)).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                }
                                txtCodigo.Focus();
                                AdicionarDetalle();
                            }
                        }
                        else
                        {
                            MostrarControl(divDescuento.ClientID);
                            string Titulo = "Requiere Autorización.";
                            if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarValidador"))
                            {
                                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarValidador", string.Format("$(document).ready(function(){{MostrarDescuento('{0}', 600);}});", Titulo), true);
                            }
                        }
                    }
                    else
                    {
                        hddValorDescuento.Value = "0";
                        AdicionarDetalle();
                    }
                }
                else
                {
                    txtCantidad.Text = "";
                    txtBodega.Text = "";
                    txtArticulo.Text = "";
                    txtCodigo.Text = "";
                    txtPrecio.Text = "";
                    txtCodigo.Focus();
                    MostrarAlerta(0, "Error", "Debe digitar una cantidad valida");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        private void AdicionarDetalle()
        {
            try
            {
                hddIdEliminar.Value = string.Empty;
                OcultarControl(divValidador.ClientID);
                bool Validador = true;
                decimal Cantidad = 0;
                if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.SalidaMercancia.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoCompra.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Remision.GetHashCode().ToString())
                {
                    if (hddEsInventario.Value == "1" && (decimal.Parse(hddCantidad.Value, NumberStyles.Currency) < decimal.Parse(txtCantidad.Text, NumberStyles.Currency)))
                    {
                        Validador = false;
                        MostrarMensaje("Error", "No hay existencias suficientes del artículo para la venta.");
                        txtCantidad.Text = "";
                        txtBodega.Text = "";
                        txtArticulo.Text = "";
                        txtCodigo.Text = "";
                        txtPrecio.Text = "";
                        txtCodigo.Focus();
                    }
                }
                if (Validador)
                {
                    DataTable dt = new DataTable();
                    bool Existe = false;
                    if (dgFactura.Items.Count > 0)
                    {
                        CargarColumnasFactura(ref dt);
                        foreach (DataGridItem Item in dgFactura.Items)
                        {
                            if (Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text == hddIdArticulo.Value && hddIdBodega.Value == Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text)
                            {
                                Existe = true;
                                Cantidad = Cantidad + decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                            }
                            DataRow copia;
                            copia = dt.NewRow();
                            copia["IdArticulo"] = Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text;
                            copia["Codigo"] = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text;
                            copia["Articulo"] = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text;
                            copia["IdBodega"] = Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text;
                            copia["Bodega"] = Item.Cells[dgFacturaEnum.Bodega.GetHashCode()].Text;
                            copia["Cantidad"] = Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text;
                            copia["ValorUnitario"] = Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text;
                            copia["Descuento"] = Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text;
                            copia["ValorUnitarioConDescuento"] = Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text;
                            copia["IVA"] = Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text;
                            copia["ValorUnitarioConIVA"] = Item.Cells[dgFacturaEnum.ValorUnitarioConIVA.GetHashCode()].Text;
                            copia["TotalLinea"] = Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text;
                            copia["CostoPonderado"] = Item.Cells[dgFacturaEnum.CostoPonderado.GetHashCode()].Text;
                            copia["ValorDescuento"] = Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text;
                            copia["PrecioCosto"] = Item.Cells[dgFacturaEnum.PrecioCosto.GetHashCode()].Text;
                            dt.Rows.Add(copia);
                        }
                    }
                    else
                    {
                        CargarColumnasFactura(ref dt);
                    }
                    if (!Existe)
                    {
                        DataRow row;
                        row = dt.NewRow();
                        row["IdArticulo"] = hddIdArticulo.Value;
                        row["Codigo"] = txtCodigo.Text;
                        row["Articulo"] = txtArticulo.Text;
                        row["IdBodega"] = hddIdBodega.Value;
                        row["Bodega"] = txtBodega.Text;
                        row["Cantidad"] = decimal.Parse(txtCantidad.Text, NumberStyles.Currency).ToString();
                        row["Descuento"] = decimal.Parse(txtDescuento.Text, NumberStyles.Currency).ToString();
                        if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                        {
                            if (oUsuarioI.ManejaPrecioConIVA)
                            {
                                if (oUsuarioI.ManejaDescuentoConIVA)
                                {
                                    row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                }
                                else
                                {
                                    row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = (decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                            else
                            {
                                row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConIVA"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                            }
                        }
                        else
                        {
                            if (oUsuarioI.ManejaCostoConIVA)
                            {
                                if (oUsuarioI.ManejaDescuentoConIVA)
                                {
                                    row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                }
                                else
                                {
                                    row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = (decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                            else
                            {
                                row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConIVA"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                            }
                        }
                        row["IVA"] = decimal.Parse(hddIVA.Value, NumberStyles.Currency).ToString();
                        row["CostoPonderado"] = decimal.Parse(hddCostoPonderado.Value, NumberStyles.Currency).ToString();
                        row["ValorDescuento"] = decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency).ToString();
                        row["PrecioCosto"] = decimal.Parse(hddPrecioCosto.Value, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        dt.Rows.Add(row);
                        txtCodigo.Focus();
                    }
                    else
                    {
                        if ((hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.SalidaMercancia.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoCompra.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Remision.GetHashCode().ToString()))
                        {
                            if (hddEsInventario.Value == "1" && (decimal.Parse(hddCantidad.Value, NumberStyles.Currency) < (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) + Cantidad)))
                            {
                                MostrarMensaje("Error", "No hay existencias suficientes del artículo para la venta.");
                            }
                            else
                            {
                                DataRow row;
                                row = dt.NewRow();
                                row["IdArticulo"] = hddIdArticulo.Value;
                                row["Codigo"] = txtCodigo.Text;
                                row["Articulo"] = txtArticulo.Text;
                                row["IdBodega"] = hddIdBodega.Value;
                                row["Bodega"] = txtBodega.Text;
                                row["Cantidad"] = decimal.Parse(txtCantidad.Text, NumberStyles.Currency).ToString();
                                row["Descuento"] = decimal.Parse(txtDescuento.Text, NumberStyles.Currency).ToString();
                                if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                                {
                                    if (oUsuarioI.ManejaPrecioConIVA)
                                    {
                                        if (oUsuarioI.ManejaDescuentoConIVA)
                                        {
                                            row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                            row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                            row["ValorUnitarioConIVA"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                            row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                        }
                                        else
                                        {
                                            row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                            row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                            row["ValorUnitarioConIVA"] = (decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                            row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                        }
                                    }
                                    else
                                    {
                                        row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConIVA"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                }
                                else
                                {
                                    if (oUsuarioI.ManejaCostoConIVA)
                                    {
                                        if (oUsuarioI.ManejaDescuentoConIVA)
                                        {
                                            row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                            row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                            row["ValorUnitarioConIVA"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                            row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                        }
                                        else
                                        {
                                            row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                            row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                            row["ValorUnitarioConIVA"] = (decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                            row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                        }
                                    }
                                    else
                                    {
                                        row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConIVA"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                }
                                row["IVA"] = decimal.Parse(hddIVA.Value, NumberStyles.Currency).ToString();
                                row["CostoPonderado"] = decimal.Parse(hddCostoPonderado.Value, NumberStyles.Currency).ToString();
                                row["ValorDescuento"] = decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency).ToString();
                                row["PrecioCosto"] = decimal.Parse(hddPrecioCosto.Value, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                dt.Rows.Add(row);
                                txtCodigo.Focus();
                            }
                        }
                        else
                        {
                            DataRow row;
                            row = dt.NewRow();
                            row["IdArticulo"] = hddIdArticulo.Value;
                            row["Codigo"] = txtCodigo.Text;
                            row["Articulo"] = txtArticulo.Text;
                            row["IdBodega"] = hddIdBodega.Value;
                            row["Bodega"] = txtBodega.Text;
                            row["Cantidad"] = decimal.Parse(txtCantidad.Text, NumberStyles.Currency).ToString();
                            row["Descuento"] = decimal.Parse(txtDescuento.Text, NumberStyles.Currency).ToString();
                            if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                            {
                                if (oUsuarioI.ManejaPrecioConIVA)
                                {
                                    if (oUsuarioI.ManejaDescuentoConIVA)
                                    {
                                        row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConIVA"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                    else
                                    {
                                        row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConIVA"] = (decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                }
                                else
                                {
                                    row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                            else
                            {
                                if (oUsuarioI.ManejaCostoConIVA)
                                {
                                    if (oUsuarioI.ManejaDescuentoConIVA)
                                    {
                                        row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConIVA"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                    else
                                    {
                                        row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConIVA"] = (decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                }
                                else
                                {
                                    row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))).ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                            row["IVA"] = decimal.Parse(hddIVA.Value, NumberStyles.Currency).ToString();
                            row["CostoPonderado"] = decimal.Parse(hddCostoPonderado.Value, NumberStyles.Currency).ToString();
                            row["ValorDescuento"] = decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency).ToString();
                            row["PrecioCosto"] = decimal.Parse(hddPrecioCosto.Value, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                            dt.Rows.Add(row);
                            txtCodigo.Focus();
                        }
                    }
                    dgFactura.DataSource = dt;
                    dgFactura.DataBind();
                    hddIdArticulo.Value = "0";
                    txtArticulo.Text = "";
                    hddIdBodega.Value = "0";
                    txtBodega.Text = "";
                    txtCantidad.Text = "";
                    if (string.IsNullOrEmpty(hddDescuento.Value))
                    {
                        txtDescuento.Text = "0";
                    }
                    else
                    {
                        txtDescuento.Text = hddDescuento.Value;
                    }
                    txtDescuento.Text = "0";
                    txtCodigo.Text = "";
                    txtPrecio.Text = "";
                    hddValorDescuento.Value = "0";
                    CalcularTotalesDocumento();
                    CalcularTotalPago();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void CalcularTotalesDocumento()
        {
            try
            {
                txtAntesIVA.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtTotalDescuento.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtTotalIVA.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtImpoconsumo.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtPropina.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtTotalFactura.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtTotalRetencion.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                foreach (DataGridItem Item in dgFactura.Items)
                {
                    if (oUsuarioI.ManejaPrecioConIVA)
                    {
                        if (oUsuarioI.ManejaDescuentoConIVA)
                        {
                            txtAntesIVA.Text = Math.Round((decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalDescuento.Text = Math.Round((decimal.Parse(txtTotalDescuento.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalIVA.Text = Math.Round((decimal.Parse(txtTotalIVA.Text, NumberStyles.Currency) + ((decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100))) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalFactura.Text = Math.Round((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                        }
                        else
                        {
                            txtAntesIVA.Text = Math.Round((decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalDescuento.Text = Math.Round((decimal.Parse(txtTotalDescuento.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalIVA.Text = Math.Round((decimal.Parse(txtTotalIVA.Text, NumberStyles.Currency) + ((decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100))) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalFactura.Text = Math.Round((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                        }
                    }
                    else
                    {
                        txtAntesIVA.Text = Math.Round((decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalDescuento.Text = Math.Round((decimal.Parse(txtTotalDescuento.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalIVA.Text = Math.Round((decimal.Parse(txtTotalIVA.Text, NumberStyles.Currency) + ((decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100))) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalFactura.Text = Math.Round((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                    }
                }
                if (chkPropina.Checked)
                {
                    tblEmpresaItem oEmpI = new tblEmpresaItem();
                    tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                    oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                    if (oEmpI.Propina > 0)
                    {
                        txtPropina.Text = Math.Round((decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency) * (oEmpI.Propina / 100)), 0).ToString(Util.ObtenerFormatoDecimal());
                    }
                }
                if (oUsuarioI.Impoconsumo > 0 && (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Remision.GetHashCode().ToString()))
                {
                    txtImpoconsumo.Text = Math.Round((decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency) * (oUsuarioI.Impoconsumo / 100)), 0).ToString(Util.ObtenerFormatoDecimal());
                }
                //Calculo de retenciones
                decimal TotalRetenciones = 0;
                decimal AntesIVA = decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency);
                foreach (DataGridItem Row in dgRetenciones.Items)
                {
                    if (((CheckBox)(Row.Cells[dgRetencionesEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked == true)
                    {
                        if (AntesIVA > decimal.Parse(Row.Cells[dgRetencionesEnum.Base.GetHashCode()].Text, NumberStyles.Currency))
                        {
                            TotalRetenciones = TotalRetenciones + (AntesIVA * (decimal.Parse(Row.Cells[dgRetencionesEnum.Porcentaje.GetHashCode()].Text, NumberStyles.Currency) / 100));
                        }
                    }
                }
                txtTotalRetencion.Text = TotalRetenciones.ToString(Util.ObtenerFormatoDecimal());
                txtTotalFactura.Text = Math.Round(((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) + decimal.Parse(txtPropina.Text, NumberStyles.Currency) + decimal.Parse(txtImpoconsumo.Text, NumberStyles.Currency)) - decimal.Parse(txtTotalRetencion.Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                txtRestante.Text = txtTotalFactura.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarColumnasFactura(ref DataTable dt)
        {
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IdArticulo";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Codigo";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Articulo";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IdBodega";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Bodega";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Cantidad";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Descuento";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IVA";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ValorUnitario";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ValorUnitarioConDescuento";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ValorUnitarioConIVA";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TotalLinea";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CostoPonderado";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ValorDescuento";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PrecioCosto";
            dt.Columns.Add(column);
        }
        protected void dgFactura_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.EliminarDetalles.GetHashCode().ToString()));
                    if (oRolPagI.Eliminar)
                    {
                        DataTable dt = new DataTable();
                        CargarColumnasFactura(ref dt);
                        txtAntesIVA.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalIVA.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalFactura.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalDescuento.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        foreach (DataGridItem Item in dgFactura.Items)
                        {
                            if (Item.ItemIndex != e.Item.ItemIndex)
                            {
                                DataRow row;
                                row = dt.NewRow();
                                row["IdArticulo"] = Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text;
                                row["Codigo"] = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text;
                                row["Articulo"] = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text;
                                row["IdBodega"] = Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text;
                                row["Bodega"] = Item.Cells[dgFacturaEnum.Bodega.GetHashCode()].Text;
                                row["Cantidad"] = Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text;
                                row["Descuento"] = Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text;
                                row["ValorUnitario"] = Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text;
                                row["Descuento"] = Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text;
                                row["ValorUnitarioConDescuento"] = Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text;
                                row["IVA"] = Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text;
                                row["ValorUnitarioConIVA"] = Item.Cells[dgFacturaEnum.ValorUnitarioConIVA.GetHashCode()].Text;
                                row["TotalLinea"] = Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text;
                                row["CostoPonderado"] = Item.Cells[dgFacturaEnum.CostoPonderado.GetHashCode()].Text;
                                row["ValorDescuento"] = Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text;
                                row["PrecioCosto"] = Item.Cells[dgFacturaEnum.PrecioCosto.GetHashCode()].Text;
                                dt.Rows.Add(row);
                            }
                        }
                        dgFactura.DataSource = dt;
                        dgFactura.DataBind();
                        CalcularTotalesDocumento();
                        CalcularTotalPago();
                    }
                    else
                    {
                        MostrarControl(divValidador.ClientID);
                        hddIdEliminar.Value = e.Item.ItemIndex.ToString();
                        string Titulo = "Requiere Autorización.";
                        if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarValidador"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarValidador", string.Format("$(document).ready(function(){{MostrarValidador('{0}', 600);}});", Titulo), true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        private string ValidarInventarioSuficiente()
        {
            string Errores = "";
            try
            {
                if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.SalidaMercancia.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoCompra.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Remision.GetHashCode().ToString())
                {
                    if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() && Request.QueryString["TipoDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString())
                    {

                    }
                    else
                    {
                        tblArticuloItem oArtI = new tblArticuloItem();
                        tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                        foreach (DataGridItem Item in dgFactura.Items)
                        {
                            long IdArticulo = long.Parse(Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text);
                            long IdBodega = long.Parse(Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text);
                            decimal Cantidad = 0;
                            oArtI = oArtB.ObtenerArticuloPorID(IdArticulo, oUsuarioI.idEmpresa);
                            if (oArtI.EsCompuesto)
                            {
                                foreach (DataGridItem Item1 in dgFactura.Items)
                                {
                                    if (IdArticulo.ToString() == Item1.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text)
                                    {
                                        Cantidad = Cantidad + (decimal.Parse(Item1.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency));
                                    }
                                }
                                tblListaMaterialesItem oLisMatI = new tblListaMaterialesItem();
                                List<tblListaMaterialesDetalleItem> oListMat = new List<tblListaMaterialesDetalleItem>();
                                tblListaMaterialesBusiness oLisMatB = new tblListaMaterialesBusiness(CadenaConexion);
                                oLisMatI = oLisMatB.ObtenerListaMaterialesPorIdArticulo(IdArticulo);
                                if (string.IsNullOrEmpty(oLisMatI.IdListaMateriales))
                                {
                                    Errores = string.Format("El artículo compuesto {0} no se encuentra o esta inactivo cómo lista de material.", oArtI.CodigoArticulo);
                                }
                                else
                                {
                                    oListMat = oLisMatB.ObtenerListaMaterialesDetallesPorID(oLisMatI.IdListaMateriales);
                                    foreach (tblListaMaterialesDetalleItem Detalle in oListMat)
                                    {
                                        decimal CantidadDetalle = 0;
                                        tblArticuloItem oArtCompuesto = new tblArticuloItem();
                                        oArtCompuesto = oArtB.ObtenerArticuloPorID(Detalle.IdArticulo, oUsuarioI.idEmpresa);
                                        CantidadDetalle = Cantidad * Detalle.Cantidad;
                                        decimal Disponibles = oArtB.DisponibilidadArticuloEnBodega(Detalle.IdArticulo, IdBodega);
                                        if (CantidadDetalle > Disponibles)
                                        {
                                            if (string.IsNullOrEmpty(Errores))
                                            {
                                                Errores = string.Format("El artículo compuesto {0} no tiene suficientes existencias para la MP {2}. Disponibilidad {1}", oArtI.CodigoArticulo, Disponibles.ToString(Util.ObtenerFormatoDecimal()).Replace("$", ""), oArtCompuesto.CodigoArticulo);
                                            }
                                            else
                                            {
                                                Errores = string.Format("{0}. El artículo compuesto {1} no tiene suficientes existencias para la MP {3}. Disponibilidad {2}", Errores, oArtI.CodigoArticulo, Disponibles.ToString(Util.ObtenerFormatoDecimal()).Replace("$", ""), oArtCompuesto.CodigoArticulo);
                                            }
                                        }
                                    }
                                }           
                            }
                            else if (oArtI.EsHijo)
                            {
                                tblArticuloItem oArtPadreI = new tblArticuloItem();
                                oArtPadreI = oArtB.ObtenerArticuloPorID(oArtI.IdArticuloPadre, oUsuarioI.idEmpresa);
                                if (oArtPadreI.EsInventario)
                                {
                                    foreach (DataGridItem Item1 in dgFactura.Items)
                                    {
                                        if (IdArticulo.ToString() == Item1.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text)
                                        {
                                            Cantidad = Cantidad + (decimal.Parse(Item1.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency) * oArtI.CantidadPadre);
                                        }
                                    }
                                    decimal Disponibles = oArtB.DisponibilidadArticuloEnBodega(oArtPadreI.IdArticulo, IdBodega);
                                    if (Cantidad > Disponibles)
                                    {
                                        if (string.IsNullOrEmpty(Errores))
                                        {
                                            Errores = string.Format("El artículo {0} no tiene suficientes existencias. Disponibilidad {1}", oArtPadreI.CodigoArticulo, Disponibles.ToString(Util.ObtenerFormatoDecimal()).Replace("$", ""));
                                        }
                                        else
                                        {
                                            Errores = string.Format("{0}. El artículo {1} no tiene suficientes existencias. Disponibilidad {2}", Errores, oArtPadreI.CodigoArticulo, Disponibles.ToString(Util.ObtenerFormatoDecimal()).Replace("$", ""));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (oArtI.EsInventario)
                                {
                                    foreach (DataGridItem Item1 in dgFactura.Items)
                                    {
                                        if (IdArticulo.ToString() == Item1.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text)
                                        {
                                            Cantidad = Cantidad + decimal.Parse(Item1.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                                        }
                                    }
                                    decimal Disponibles = oArtB.DisponibilidadArticuloEnBodega(IdArticulo, IdBodega);
                                    if (Cantidad > Disponibles)
                                    {
                                        if (string.IsNullOrEmpty(Errores))
                                        {
                                            Errores = string.Format("El artículo {0} no tiene suficientes existencias. Disponibilidad {1}", Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text, Disponibles.ToString(Util.ObtenerFormatoDecimal()).Replace("$", ""));
                                        }
                                        else
                                        {
                                            Errores = string.Format("{0}. El artículo {1} no tiene suficientes existencias. Disponibilidad {2}", Errores, Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text, Disponibles.ToString(Util.ObtenerFormatoDecimal()).Replace("$", ""));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode().ToString())
                {
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                    foreach (DataGridItem Item in dgFactura.Items)
                    {
                        long IdArticulo = long.Parse(Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text);
                        long IdBodega = long.Parse(Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text);
                        decimal Cantidad = decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                        decimal Disponibles = oArtB.ValidarDisponibilidadDV(IdArticulo, IdBodega, Cantidad, txtReferencia.Text.Trim());
                        if (Cantidad > Disponibles)
                        {
                            if (string.IsNullOrEmpty(Errores))
                            {
                                Errores = string.Format("El artículo {0} no tiene suficientes existencias. Disponibilidad {1}", Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text, Disponibles.ToString(Util.ObtenerFormatoEntero()).Replace("$", ""));
                            }
                            else
                            {
                                Errores = string.Format("{0}. El artículo {1} no tiene suficientes existencias. Disponibilidad {2}", Errores, Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text, Disponibles.ToString(Util.ObtenerFormatoEntero()).Replace("$", ""));
                            }
                        }
                    }
                }
                return Errores;
            }
            catch (Exception ex)
            {
                Errores = ex.Message;
                return Errores;
            }
        }
        private RangoNumeracion ObtenerRangoNumeracion(string EMISOR_NIT, string SOFTWARE_IDENTIFICADOR, string CLAVE_CERTIFICADO, byte[] CertificadoFE)
        {
            try
            {
                RangoNumeracion Rango = new RangoNumeracion();
                var cliente = new ClienteServicioDian
                {
                    Ambiente = AmbienteServicio.PRODUCCION,
                    Certificado = new X509Certificate2(CertificadoFE, CLAVE_CERTIFICADO)
                };
                var response = cliente.ObtenerRangosNumeracion(EMISOR_NIT, SOFTWARE_IDENTIFICADOR);
                if (response.ResponseList != null)
                {
                    foreach (var range in response.ResponseList)
                    {
                        Rango.NumeroResolucion = long.Parse(range.ResolutionNumber);
                        Rango.FechaResolucion = DateTime.Parse(range.ResolutionDate);
                        Rango.Prefijo = range.Prefix;
                        Rango.Desde = range.FromNumber;
                        Rango.Hasta = range.ToNumber;
                        Rango.ClaveTecnica = range.TechnicalKey;
                        Rango.VigenciaDesde = DateTime.Parse(range.ValidDateFrom);
                        Rango.VigenciaHasta = DateTime.Parse(range.ValidDateTo);
                    }
                }
                else
                {
                    Rango.NumeroResolucion = 18760000001;
                    Rango.FechaResolucion = new DateTime(0001, 01, 01);
                    Rango.Prefijo = "SETP";
                    Rango.Desde = 990000000;
                    Rango.Hasta = 995000000;
                    Rango.ClaveTecnica = "fc8eac422eba16e22ffd8c6f94b3f40a6e38162c";
                    Rango.VigenciaDesde = new DateTime(2019, 01, 19);
                    Rango.VigenciaHasta = new DateTime(2030, 01, 19);
                }
                return Rango;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private string ValidarInformacionParaFE(tblEmpresaItem oEmpI, tblTerceroItem oTerI)
        {
            try
            {
                string Errores = string.Empty;
                if (string.IsNullOrEmpty(oEmpI.SoftwareID))
                {
                    Errores = "El campo Software ID es requerido.";
                }
                if (string.IsNullOrEmpty(oEmpI.SoftwarePIN))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Software PIN es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Software PIN es requerido.", Errores);
                    }
                }
                if (string.IsNullOrEmpty(oEmpI.TipoContribuyente))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Tipo Contribuyente Empresa es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Tipo Contribuyente Empresa es requerido.", Errores);
                    }
                }
                if (string.IsNullOrEmpty(oEmpI.RegimenFiscal))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Regimen Fiscal Empresa es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Regimen Fiscal Empresa es requerido.", Errores);
                    }
                }
                if (string.IsNullOrEmpty(oEmpI.CodigoResponsabilidadFiscal))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Codigo Responsabilidad Fiscal Empresa es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Codigo Responsabilidad Fiscal Empresa es requerido.", Errores);
                    }
                }
                //if (string.IsNullOrEmpty(oEmpI.TestSetId))
                //{
                //    if (string.IsNullOrEmpty(Errores))
                //    {
                //        Errores = "El campo TestSetId es requerido.";
                //    }
                //    else
                //    {
                //        Errores = string.Format("{0}, El campo TestSetId es requerido.", Errores);
                //    }
                //}
                if (string.IsNullOrEmpty(oEmpI.ClaveTecnica))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Clave Tecnica es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Clave Tecnica es requerido.", Errores);
                    }
                }
                if (string.IsNullOrEmpty(oEmpI.ClaveCertificado))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Clave Certificado Empresa es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0} El campo Clave Certificado Empresa es requerido.", Errores);
                    }
                }
                if (string.IsNullOrEmpty(oEmpI.Correo))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Correo Empresa es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Correo Empresa es requerido.", Errores);
                    }
                }
                if (string.IsNullOrEmpty(oTerI.TipoIdentificacionDIAN))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Tipo Identificacion DIAN Cliente es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Tipo Identificacion DIAN Cliente es requerido.", Errores);
                    }
                }
                if (string.IsNullOrEmpty(oTerI.TipoContribuyente))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Tipo Contribuyente Cliente es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Tipo Contribuyente Cliente es requerido.", Errores);
                    }
                }
                if (string.IsNullOrEmpty(oTerI.RegimenFiscal))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Regimen Fiscal Cliente es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Regimen Fiscal Cliente es requerido.", Errores);
                    }
                }
                if (string.IsNullOrEmpty(oTerI.CodigoResponsabilidadFiscal))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Codigo Responsabilidad Fiscal Cliente es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Codigo Responsabilidad Fiscal Cliente es requerido.", Errores);
                    }
                }
                if (string.IsNullOrEmpty(oTerI.Mail))
                {
                    if (string.IsNullOrEmpty(Errores))
                    {
                        Errores = "El campo Correo Cliente es requerido.";
                    }
                    else
                    {
                        Errores = string.Format("{0}, El campo Correo Cliente es requerido.", Errores);
                    }
                }
                return Errores;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void btnGuardar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (dgFactura.Items.Count > 0)
                {
                    string Errores = ValidarInventarioSuficiente();
                    if (string.IsNullOrEmpty(Errores))
                    {
                        if (chkFE.Checked)
                        {
                            tblEmpresaItem oEmpI = new tblEmpresaItem();
                            tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                            oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                            tblTerceroItem oTerI = new tblTerceroItem();
                            tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                            oTerI = oTerB.ObtenerTercero(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa);
                            Errores = ValidarInformacionParaFE(oEmpI, oTerI);
                            if (string.IsNullOrEmpty(Errores))
                            {
                                bool NotaCredito = false;
                                if (Request.QueryString["opcionDocumento"] == tblTipoDocumentoItem.TipoDocumentoEnum.notaCreditoVenta.GetHashCode().ToString() || Request.QueryString["Tipoocumento"] == tblTipoDocumentoItem.TipoDocumentoEnum.notaCreditoVenta.GetHashCode().ToString())
                                {
                                    NotaCredito = true;
                                }
                                RangoNumeracion Rango = ObtenerRangoNumeracion(oEmpI.Identificacion, oEmpI.SoftwareID, oEmpI.ClaveCertificado, oEmpI.CertificadoFE);
                                var consecutivo = new Positive.ArchivoConsecutivo(oEmpI.Consecutivo);
                                string numero = Rango.Prefijo + consecutivo.ObtenerSiguiente();
                                var fecha = DateTimeHelper.GetColombianDate();
                                var vencimiento = fecha.AddMonths(1);
                                var extensionDian = new ExtensionDian
                                {
                                    RangoNumeracion = Rango,
                                    PaisOrigen = Pais.COLOMBIA,
                                    SoftwareProveedorNit = oEmpI.Identificacion,
                                    SoftwareIdentificador = oEmpI.SoftwareID,
                                    SoftwarePin = oEmpI.SoftwarePIN
                                };
                                DireccionFisica EMISOR_DIRECCION_FISICA = new DireccionFisica
                                {
                                    Departamento = new Departamento
                                    {
                                        Codigo = oEmpI.CodigoDepartamento,
                                        Nombre = oEmpI.Departamento
                                    },
                                    Municipio = new Municipio
                                    {
                                        Codigo = oEmpI.CodigoCiudad,
                                        Nombre = oEmpI.Ciudad
                                    },
                                    Linea = oEmpI.Direccion,
                                    Pais = Pais.COLOMBIA
                                };
                                TipoContribuyente EMISOR_TIPO_CONTRIBUYENTE = new TipoContribuyente
                                {
                                    Codigo = oEmpI.TipoContribuyente
                                };
                                RegimenFiscal EMISOR_REGIMEN_FISCAL = new RegimenFiscal
                                {
                                    Codigo = oEmpI.RegimenFiscal
                                };
                                ResponsabilidadFiscal EMISOR_RESPONSABILIDAD_FISCAL = new ResponsabilidadFiscal
                                {
                                    Codigo = oEmpI.CodigoResponsabilidadFiscal,
                                    Descripcion = oEmpI.ResponsabilidadFiscal
                                };
                                var emisor = new Emisor
                                {
                                    Nit = oEmpI.Identificacion,
                                    Nombre = oEmpI.Nombre,
                                    DireccionFisica = EMISOR_DIRECCION_FISICA,
                                    TipoContribuyente = EMISOR_TIPO_CONTRIBUYENTE,
                                    RegimenFiscal = EMISOR_REGIMEN_FISCAL,
                                    ResponsabilidadFiscal = EMISOR_RESPONSABILIDAD_FISCAL,
                                    PrefijoRangoNumeracion = Rango.Prefijo,
                                    MatriculaMercantil = oEmpI.MatriculaMercantil,
                                    CorreoElectronico = oEmpI.Correo,
                                    NumeroTelefonico = oEmpI.Telefono
                                };
                                TipoIdentificacion CEDULA_CIUDADANIA = new TipoIdentificacion
                                {
                                    Codigo = oTerI.TipoIdentificacionDIAN
                                };
                                DireccionFisica ADQUIRIENTE_DIRECCION_FISICA = new DireccionFisica
                                {
                                    Departamento = new Departamento
                                    {
                                        Codigo = oTerI.CodigoDepartamento,
                                        Nombre = oTerI.Departamento
                                    },
                                    Municipio = new Municipio
                                    {
                                        Codigo = oTerI.CodigoCiudad,
                                        Nombre = oTerI.Ciudad
                                    },
                                    Linea = oTerI.Direccion,
                                    Pais = Pais.COLOMBIA
                                };
                                TipoContribuyente ADQUIRIENTE_TIPO_CONTRIBUYENTE = new TipoContribuyente
                                {
                                    Codigo = oTerI.TipoContribuyente
                                };
                                RegimenFiscal ADQUIRIENTE_REGIMEN_FISCAL = new RegimenFiscal
                                {
                                    Codigo = oTerI.RegimenFiscal
                                };
                                ResponsabilidadFiscal ADQUIRIENTE_RESPONSABILIDAD_FISCAL = new ResponsabilidadFiscal
                                {
                                    Codigo = oTerI.CodigoResponsabilidadFiscal,
                                    Descripcion = oTerI.ResponsabilidadFiscal
                                };
                                var adquiriente = new Adquiriente
                                {
                                    TipoIdentificacion = TipoIdentificacion.CEDULA_CIUDADANIA,
                                    Identificacion = oTerI.Identificacion,
                                    Nombre = oTerI.Nombre,
                                    DireccionFisica = ADQUIRIENTE_DIRECCION_FISICA,
                                    TipoContribuyente = ADQUIRIENTE_TIPO_CONTRIBUYENTE,
                                    RegimenFiscal = ADQUIRIENTE_REGIMEN_FISCAL,
                                    ResponsabilidadFiscal = ADQUIRIENTE_RESPONSABILIDAD_FISCAL,
                                    MatriculaMercantil = string.IsNullOrEmpty(oTerI.MatriculaMercantil) ? "00000-00" : oTerI.MatriculaMercantil,
                                    CorreoElectronico = oTerI.Mail,
                                    NumeroTelefonico = oTerI.Celular
                                };
                                List<Linea> Lineas = new List<Linea>();
                                decimal Importe5 = 0;
                                decimal Importe19 = 0;
                                decimal BaseImponible5 = 0;
                                decimal BaseImponible19 = 0;
                                decimal ValorBruto = 0;
                                decimal ValorBrutoConImpuestos = 0;
                                foreach (DataGridItem Item in dgFactura.Items)
                                {
                                    if (decimal.Parse(Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text, NumberStyles.Currency) == 0 && decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) == 0)
                                    {
                                        Linea linea = new Linea
                                        {
                                            Cantidad = decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                            Descripcion = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text,
                                            CodigoProducto = new CodigoProducto { Valor = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text },
                                            PrecioUnitario = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency),
                                            CostoTotal = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)
                                        };
                                        Lineas.Add(linea);
                                    }
                                    else if (decimal.Parse(Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text, NumberStyles.Currency) > 0 && decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) == 0)
                                    {
                                        Linea linea = new Linea
                                        {
                                            Cantidad = decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                            Descripcion = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text,
                                            CodigoProducto = new CodigoProducto { Valor = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text },
                                            PrecioUnitario = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency),
                                            CostoTotal = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                            Descuentos =
                                        {
                                            new Descuento
                                            {
                                                Tipo = TipoDescuento.OTROS,
                                                Razon = "Discount",
                                                Porcentaje = decimal.Parse(Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text, NumberStyles.Currency),
                                                Monto = decimal.Parse(Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                                Base = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)
                                            }
                                        }
                                        };
                                        Lineas.Add(linea);
                                    }
                                    else if (decimal.Parse(Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text, NumberStyles.Currency) == 0 && decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) > 0)
                                    {
                                        if (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) == 5)
                                        {
                                            Linea linea = new Linea
                                            {
                                                Cantidad = decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                                Descripcion = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text,
                                                CodigoProducto = new CodigoProducto { Valor = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text },
                                                PrecioUnitario = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency),
                                                CostoTotal = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                                Impuestos =
                                            {
                                                decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) == 0 ? null : new Impuesto
                                                {
                                                    Tipo = TipoImpuesto.IVA,
                                                    //Porcentaje = Math.Round((decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency)),2),
                                                    Porcentaje = 5.00M,
                                                    Importe = Math.Round(((decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100)),2),
                                                    BaseImponible = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)
                                                }
                                            }
                                            };
                                            BaseImponible5 = BaseImponible5 + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency));
                                            Importe5 = Math.Round((Importe5 + ((decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100))), 2);
                                            Lineas.Add(linea);
                                        }
                                        else
                                        {
                                            Linea linea = new Linea
                                            {
                                                Cantidad = decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                                Descripcion = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text,
                                                CodigoProducto = new CodigoProducto { Valor = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text },
                                                PrecioUnitario = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency),
                                                CostoTotal = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                                Impuestos =
                                                {
                                                    decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) == 0 ? null : new Impuesto
                                                    {
                                                        Tipo = TipoImpuesto.IVA,
                                                        //Porcentaje = Math.Round((decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency)),2),
                                                        Porcentaje = 19.00M,
                                                        Importe = Math.Round(((decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100)),2),
                                                        BaseImponible = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)
                                                    }
                                                }
                                            };
                                            BaseImponible19 = BaseImponible19 + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency));
                                            Importe19 = Math.Round((Importe19 + ((decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100))), 2);
                                            Lineas.Add(linea);
                                        }
                                    }
                                    else
                                    {
                                        Linea linea = new Linea
                                        {
                                            Cantidad = decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                            Descripcion = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text,
                                            CodigoProducto = new CodigoProducto { Valor = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text },
                                            PrecioUnitario = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency),
                                            CostoTotal = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                            Descuentos =
                                            {
                                                decimal.Parse(Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text, NumberStyles.Currency) == 0 ? null : new Descuento
                                                {
                                                    Tipo = TipoDescuento.OTROS,
                                                    Razon = "Discount",
                                                    Porcentaje = decimal.Parse(Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text, NumberStyles.Currency),
                                                    Monto = decimal.Parse(Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency),
                                                    Base = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)
                                                }
                                            },
                                            Impuestos =
                                            {
                                                decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) == 0 ? null : new Impuesto
                                                {
                                                    Tipo = TipoImpuesto.IVA,
                                                    Porcentaje = decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency),
                                                    Importe = (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100),
                                                    BaseImponible = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)
                                                }
                                            }
                                        };
                                        Lineas.Add(linea);
                                    }
                                    ValorBruto = ValorBruto + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency));
                                    ValorBrutoConImpuestos = ValorBrutoConImpuestos + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConIVA.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency));
                                }
                                var impuesto = new ResumenImpuesto
                                {
                                    Tipo = TipoImpuesto.IVA,
                                    Importe = Importe5 + Importe19,
                                    Detalles =
                                    {
                                        BaseImponible5 == 0 ? null : new Impuesto
                                        {
                                            Tipo = TipoImpuesto.IVA,
                                            Porcentaje = 5.00M,
                                            Importe = Importe5,
                                            BaseImponible = BaseImponible5
                                        },
                                        BaseImponible19 == 0 ? null : new Impuesto
                                        {
                                            Tipo = TipoImpuesto.IVA,
                                            Porcentaje = 19.00M,
                                            Importe = Importe19,
                                            BaseImponible = BaseImponible19
                                        }
                                    }
                                };
                                var detallePago = chkCredito.Checked ? new DetallePago
                                {
                                    Forma = FormaPago.CREDITO,
                                    Metodo = MetodoPago.NO_DEFINIDO,
                                    Fecha = vencimiento
                                } : new DetallePago
                                {
                                    Forma = FormaPago.CONTADO,
                                    Metodo = MetodoPago.EFECTIVO,
                                    Fecha = vencimiento
                                };
                                if(ValorBruto == (BaseImponible5 + BaseImponible19) && ((ValorBrutoConImpuestos - (BaseImponible5 + BaseImponible19 + Importe5 + Importe19)) > 2))
                                {
                                    ValorBrutoConImpuestos = BaseImponible5 + BaseImponible19 + Importe5 + Importe19;
                                }
                                var totales = new Totales
                                {
                                    ValorBruto = ValorBruto,
                                    BaseImponible = BaseImponible5 + BaseImponible19,
                                    ValorBrutoConImpuestos = ValorBrutoConImpuestos,
                                    Descuentos = 0M,
                                    Cargos = 0M,
                                    TotalPagable = ValorBrutoConImpuestos,
                                };
                                bool AMBIENTE_PRUEBAS = true;
                                bool USAR_SET_PUEBAS = true;
                                if (string.IsNullOrEmpty(oEmpI.TestSetId))
                                {
                                    AMBIENTE_PRUEBAS = false;
                                    USAR_SET_PUEBAS = false;
                                }
                                AmbienteServicio AMBIENTE_SERVICIO = AMBIENTE_PRUEBAS ? AmbienteServicio.PRUEBAS : AmbienteServicio.PRODUCCION;
                                AmbienteDestino AMBIENTE_DESTINO = AMBIENTE_PRUEBAS ? AmbienteDestino.PRUEBAS : AmbienteDestino.PRODUCCION;
                                string IDENTIFICADOR_SET_PRUEBAS = oEmpI.TestSetId; //(TestSetId)
                                if (NotaCredito)
                                {
                                    var concepto = ConceptoNotaCredito.ANULACION_TOTAL;
                                    ReferenciaDocumento referencia = JsonConvert.DeserializeObject<ReferenciaDocumento>(hhdZipKey.Value);
                                    var generador = new GeneradorNotaCredito(AMBIENTE_DESTINO, concepto, referencia)
                                        .ConNumero(referencia.Numero)
                                        .ConFecha(fecha)
                                        .ConNota(txtObservaciones.Text.Trim())
                                        .ConMoneda(Moneda.PESO_COLOMBIANO)
                                        .ConExtensionDian(extensionDian)
                                        .ConEmisor(emisor)
                                        .ConAdquiriente(adquiriente)
                                        .AgregarLineas(Lineas)
                                        .AgregarResumenImpuesto(impuesto)
                                        //.AgregarDescuento(descuento1)
                                        //.AgregarCargo(cargo1)
                                        .ConTotales(totales)
                                        .ConDetallePago(detallePago)
                                        .AsignarCUDE(oEmpI.SoftwarePIN);
                                    var creditNote = generador.Obtener();
                                    var xmlCreditNote = creditNote.SerializeXmlDocument();
                                    var stringCreditNote = xmlCreditNote.ToXmlString();
                                    var firmaElectronica = new FirmaElectronica
                                    {
                                        RolFirmante = RolFirmante.EMISOR,
                                        CertificadoFE = oEmpI.CertificadoFE,
                                        ClaveCertificado = oEmpI.ClaveCertificado
                                    };
                                    fecha = DateTimeHelper.GetColombianDate();
                                    var signedBytes = firmaElectronica.FirmarNotaCredito(stringCreditNote, fecha);
                                    var signedXml = Encoding.UTF8.GetString(signedBytes);
                                    var clienteDian = new ClienteServicioDian
                                    {
                                        Ambiente = AMBIENTE_SERVICIO,
                                        CertificadoFE = oEmpI.CertificadoFE,
                                        ClaveCertificado = oEmpI.ClaveCertificado
                                    };
                                    DianResponse dianResponse = null;
                                    UploadDocumentResponse uploadResponse = null;
                                    if (AMBIENTE_PRUEBAS && USAR_SET_PUEBAS)
                                    {
                                        var documentos = new List<byte[]>() { signedBytes };
                                        uploadResponse = clienteDian.EnviarSetPruebas(documentos, IDENTIFICADOR_SET_PRUEBAS);
                                        if (uploadResponse.ErrorMessageList != null)
                                        {
                                            foreach (var itemList in uploadResponse.ErrorMessageList)
                                            {
                                                Console.WriteLine("[{0}] {1}", itemList.DocumentKey, itemList.ProcessedMessage);
                                            }
                                        }
                                        Task.Delay(10000).Wait();
                                        dianResponse = clienteDian.ConsultarDocumentos(uploadResponse.ZipKey)[0];
                                        string registro = string.Format("\r\nFACTURA_VENTA\t\t{0}\t\t{1}\t\t{2}\t\t{3}: {4}", creditNote.IssueDate.Value,
                                            creditNote.ID.Value, creditNote.UUID.Value, dianResponse.StatusCode, dianResponse.StatusMessage);
                                        if (dianResponse.StatusCode != RespuestaDian.PROCESADO_CORRECTAMENTE)
                                        {
                                            if (dianResponse.ErrorMessage != null)
                                            {
                                                MostrarAlerta(0, "Error", string.Join("* ", dianResponse.ErrorMessage));
                                            }
                                            else
                                            {
                                                MostrarAlerta(0, "Error", string.Format("{0}: {1}", dianResponse.StatusCode, dianResponse.StatusDescription));
                                            }
                                        }
                                        if (dianResponse.XmlBase64Bytes != null)
                                        {
                                            var appResponse = (dianResponse.XmlBase64Bytes == null) ? "null" : Encoding.UTF8.GetString(dianResponse.XmlBase64Bytes);
                                            // Suponiendo que cuando appresponse no es null significa que la dian aceptó el documento
                                            if (appResponse != "null")
                                            {
                                                oEmpB.ActualizarConsecutivo(oUsuarioI.idEmpresa);
                                                MostrarAlerta(1, string.Format("{0} - {1}", dianResponse.StatusCode, dianResponse.StatusMessage), dianResponse.StatusDescription);
                                            }
                                            else
                                            {
                                                MostrarAlerta(0, "Error", string.Join("* ", dianResponse.ErrorMessage));
                                            }
                                        }
                                        else
                                        {
                                            MostrarAlerta(0, "Error", string.Format("FACTURA_VENTA: {0} {1} {2} {3}: {4}", creditNote.IssueDate.Value,
                                            creditNote.ID.Value, creditNote.UUID.Value, dianResponse.StatusCode, dianResponse.StatusMessage));
                                        }
                                    }
                                    else if (AMBIENTE_PRUEBAS)
                                    {
                                        dianResponse = clienteDian.EnviarDocumento(signedBytes);
                                        string registro = string.Format("\r\nFACTURA_VENTA\t\t{0}\t\t{1}\t\t{2}\t\t{3}: {4}", creditNote.IssueDate.Value,
                                            creditNote.ID.Value, creditNote.UUID.Value, dianResponse.StatusCode, dianResponse.StatusMessage);
                                        Console.WriteLine("{0} - {1} - {2}", dianResponse.StatusCode, dianResponse.StatusMessage, dianResponse.StatusDescription);
                                        if (dianResponse.StatusCode != RespuestaDian.PROCESADO_CORRECTAMENTE)
                                        {
                                            if (dianResponse.ErrorMessage != null)
                                            {
                                                MostrarAlerta(0, "Error", string.Join("* ", dianResponse.ErrorMessage));
                                            }
                                        }
                                        var appResponse = (dianResponse.XmlBase64Bytes == null) ? "null" : Encoding.UTF8.GetString(dianResponse.XmlBase64Bytes);
                                    }
                                    else
                                    {
                                        dianResponse = clienteDian.EnviarDocumento(signedBytes);
                                        if (dianResponse.StatusCode != RespuestaDian.PROCESADO_CORRECTAMENTE)
                                        {
                                            if (dianResponse.ErrorMessage != null)
                                            {
                                                MostrarAlerta(0, "Error", string.Join("* ", dianResponse.ErrorMessage));
                                            }
                                        }
                                        else
                                        {
                                            if (dianResponse.XmlBase64Bytes != null)
                                            {
                                                var appResponse = (dianResponse.XmlBase64Bytes == null) ? "null" : Encoding.UTF8.GetString(dianResponse.XmlBase64Bytes);
                                                // Suponiendo que cuando appresponse no es null significa que la dian aceptó el documento
                                                if (appResponse != "null")
                                                {
                                                    //oEmpB.ActualizarConsecutivo(oUsuarioI.idEmpresa);
                                                    var applicationResponse = ApplicationResponseType.DeserializeXmlString(appResponse);
                                                    var generadorContenedor = new GeneradorContenedor(AMBIENTE_DESTINO)
                                                        .ConDocumento(creditNote) // Se pasa invoicetype, creditnotetype o debitnotetype según corresponda
                                                        .ConRespuesta(applicationResponse);
                                                    var attachedDocument = generadorContenedor.Obtener();
                                                    var xmlAttachedDocument = WebUtility.HtmlDecode(attachedDocument.SerializeXmlDocument().ToXmlString());
                                                    var bytesAttachedDocument = Encoding.UTF8.GetBytes(xmlAttachedDocument);
                                                    var zipAttachedDocument = ZipHelper.Compress(bytesAttachedDocument, "attached-document.xml");
                                                    // Este es el zip que se adjunta en el correo, que se envia al adquiriente, junto al pdf
                                                    Errores = CrearDocumento(referencia.Numero, applicationResponse.UUID.Value);
                                                    if (string.IsNullOrEmpty(Errores))
                                                    {
                                                        var notaCreditoPdf = new NotaCreditoPdf(creditNote)
                                                        {
                                                            RutaLogo = "logo.png",
                                                            TextoEncabezado = "Persona Jurídica \r\nRégimen Ordinario",
                                                            TextoQR = creditNote.UUID.Value,
                                                            TextoResolucion = DocumentoPdf.CrearTextoResolucion(Rango.NumeroResolucion,
                                                            Rango.FechaResolucion, Rango.Prefijo, Rango.Desde, Rango.Hasta, Rango.VigenciaHasta)
                                                        };
                                                        var bytesPdf = notaCreditoPdf.Generar();
                                                        MostrarAlerta(1, string.Format("{0} - {1}", dianResponse.StatusCode, dianResponse.StatusMessage), dianResponse.StatusDescription);
                                                        EnviarCorreo(oEmpI, oTerI, zipAttachedDocument, bytesPdf);
                                                    }
                                                    else
                                                    {
                                                        MostrarAlerta(0, "Error", Errores);
                                                    }
                                                }
                                                else
                                                {
                                                    MostrarAlerta(0, "Error", string.Join("* ", dianResponse.ErrorMessage));
                                                }
                                            }
                                            else
                                            {
                                                MostrarAlerta(0, "Error", string.Format("FACTURA_VENTA: {0} {1} {2} {3}: {4}", creditNote.IssueDate.Value,
                                                creditNote.ID.Value, creditNote.UUID.Value, dianResponse.StatusCode, dianResponse.StatusMessage));
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    var generador = new GeneradorFactura(AMBIENTE_DESTINO, TipoFactura.VENTA)
                                        .ConNumero(numero)
                                        .ConFecha(fecha)
                                        .ConVencimiento(vencimiento)
                                        .ConNota(txtObservaciones.Text.Trim())
                                        .ConMoneda(Moneda.PESO_COLOMBIANO)
                                        .ConExtensionDian(extensionDian)
                                        .ConEmisor(emisor)
                                        .ConAdquiriente(adquiriente)
                                        .AgregarLineas(Lineas)
                                        .AgregarResumenImpuesto(impuesto)
                                        //.AgregarDescuento(descuento1)
                                        //.AgregarCargo(cargo1)
                                        .ConTotales(totales)
                                        .ConDetallePago(detallePago)
                                        .AsignarCUFE(Rango.ClaveTecnica, oEmpI.SoftwarePIN);
                                    var qrFactura = generador.GenerarTextoQR();
                                    InvoiceType invoice = generador.Obtener();
                                    var xmlInvoice = invoice.SerializeXmlDocument();
                                    var stringInvoice = xmlInvoice.ToXmlString();
                                    var firmaElectronica = new FirmaElectronica
                                    {
                                        RolFirmante = RolFirmante.EMISOR,
                                        CertificadoFE = oEmpI.CertificadoFE,
                                        ClaveCertificado = oEmpI.ClaveCertificado
                                    };
                                    var signedBytes = firmaElectronica.FirmarFactura(stringInvoice, fecha);
                                    var clienteDian = new ClienteServicioDian
                                    {
                                        Ambiente = AMBIENTE_SERVICIO,
                                        CertificadoFE = oEmpI.CertificadoFE,
                                        ClaveCertificado = oEmpI.ClaveCertificado
                                    };
                                    DianResponse dianResponse = null;
                                    UploadDocumentResponse uploadResponse = null;
                                    if (AMBIENTE_PRUEBAS && USAR_SET_PUEBAS)
                                    {
                                        var documentos = new List<byte[]>() { signedBytes };
                                        uploadResponse = clienteDian.EnviarSetPruebas(documentos, IDENTIFICADOR_SET_PRUEBAS);
                                        if (uploadResponse.ErrorMessageList != null)
                                        {
                                            foreach (var itemList in uploadResponse.ErrorMessageList)
                                            {
                                                Console.WriteLine("[{0}] {1}", itemList.DocumentKey, itemList.ProcessedMessage);
                                            }
                                        }
                                        Task.Delay(10000).Wait();
                                        dianResponse = clienteDian.ConsultarDocumentos(uploadResponse.ZipKey)[0];
                                        string registro = string.Format("\r\nFACTURA_VENTA\t\t{0}\t\t{1}\t\t{2}\t\t{3}: {4}", invoice.IssueDate.Value,
                                            invoice.ID.Value, invoice.UUID.Value, dianResponse.StatusCode, dianResponse.StatusMessage);
                                        if (dianResponse.StatusCode != RespuestaDian.PROCESADO_CORRECTAMENTE)
                                        {
                                            if (dianResponse.ErrorMessage != null)
                                            {
                                                MostrarAlerta(0, "Error", string.Join("* ", dianResponse.ErrorMessage));
                                            }
                                            else
                                            {
                                                MostrarAlerta(0, "Error", string.Format("{0}: {1}", dianResponse.StatusCode, dianResponse.StatusDescription));
                                            }
                                        }
                                        if (dianResponse.XmlBase64Bytes != null)
                                        {
                                            var appResponse = (dianResponse.XmlBase64Bytes == null) ? "null" : Encoding.UTF8.GetString(dianResponse.XmlBase64Bytes);
                                            // Suponiendo que cuando appresponse no es null significa que la dian aceptó el documento
                                            if (appResponse != "null")
                                            {
                                                oEmpB.ActualizarConsecutivo(oUsuarioI.idEmpresa);
                                                MostrarAlerta(1, string.Format("{0} - {1}", dianResponse.StatusCode, dianResponse.StatusMessage), dianResponse.StatusDescription);
                                            }
                                            else
                                            {
                                                MostrarAlerta(0, "Error", string.Join("* ", dianResponse.ErrorMessage));
                                            }
                                        }
                                        else
                                        {
                                            MostrarAlerta(0, "Error", string.Format("FACTURA_VENTA: {0} {1} {2} {3}: {4}", invoice.IssueDate.Value,
                                            invoice.ID.Value, invoice.UUID.Value, dianResponse.StatusCode, dianResponse.StatusMessage));
                                        }
                                    }
                                    else if (AMBIENTE_PRUEBAS)
                                    {
                                        dianResponse = clienteDian.EnviarDocumento(signedBytes);
                                        string registro = string.Format("\r\nFACTURA_VENTA\t\t{0}\t\t{1}\t\t{2}\t\t{3}: {4}", invoice.IssueDate.Value,
                                            invoice.ID.Value, invoice.UUID.Value, dianResponse.StatusCode, dianResponse.StatusMessage);
                                        Console.WriteLine("{0} - {1} - {2}", dianResponse.StatusCode, dianResponse.StatusMessage, dianResponse.StatusDescription);
                                        if (dianResponse.StatusCode != RespuestaDian.PROCESADO_CORRECTAMENTE)
                                        {
                                            if (dianResponse.ErrorMessage != null)
                                            {
                                                MostrarAlerta(0, "Error", string.Join("* ", dianResponse.ErrorMessage));
                                            }
                                        }
                                        var appResponse = (dianResponse.XmlBase64Bytes == null) ? "null" : Encoding.UTF8.GetString(dianResponse.XmlBase64Bytes);
                                    }
                                    else
                                    {
                                        dianResponse = clienteDian.EnviarDocumento(signedBytes);
                                        if (dianResponse.StatusCode != RespuestaDian.PROCESADO_CORRECTAMENTE)
                                        {
                                            if (dianResponse.ErrorMessage != null)
                                            {
                                                MostrarAlerta(0, "Error", string.Join("* ", dianResponse.ErrorMessage));
                                            }
                                        }
                                        else
                                        {
                                            if (dianResponse.XmlBase64Bytes != null)
                                            {
                                                var appResponse = (dianResponse.XmlBase64Bytes == null) ? "null" : Encoding.UTF8.GetString(dianResponse.XmlBase64Bytes);
                                                // Suponiendo que cuando appresponse no es null significa que la dian aceptó el documento
                                                if (appResponse != "null")
                                                {
                                                    oEmpB.ActualizarConsecutivo(oUsuarioI.idEmpresa);
                                                    var applicationResponse = ApplicationResponseType.DeserializeXmlString(appResponse);
                                                    var generadorContenedor = new GeneradorContenedor(AMBIENTE_DESTINO)
                                                        .ConDocumento(invoice) // Se pasa invoicetype, creditnotetype o debitnotetype según corresponda
                                                        .ConRespuesta(applicationResponse);
                                                    var attachedDocument = generadorContenedor.Obtener();
                                                    var xmlAttachedDocument = WebUtility.HtmlDecode(attachedDocument.SerializeXmlDocument().ToXmlString());
                                                    var bytesAttachedDocument = Encoding.UTF8.GetBytes(xmlAttachedDocument);
                                                    var zipAttachedDocument = ZipHelper.Compress(bytesAttachedDocument, "attached-document.xml");
                                                    // Este es el zip que se adjunta en el correo, que se envia al adquiriente, junto al pdf
                                                    var referencia = new ReferenciaDocumento
                                                    {
                                                        Numero = invoice.ID.Value,
                                                        Cufe = invoice.UUID.Value,
                                                        AlgoritmoCufe = new AlgoritmoSeguridadUUID { Codigo = invoice.UUID.schemeName },
                                                        TipoDocumento = new TipoDocumento { Codigo = invoice.InvoiceTypeCode.Value },
                                                        Fecha = invoice.IssueDate.Value
                                                    };
                                                    string JsonFE = JsonConvert.SerializeObject(referencia);
                                                    Errores = CrearDocumento(numero, JsonFE);
                                                    if (string.IsNullOrEmpty(Errores))
                                                    {
                                                        var facturaPdf = new FacturaPdf(invoice, DocumentoPdf.TIPO_FACTURA_VENTA)
                                                        {
                                                            RutaLogo = "logo.png",
                                                            TextoEncabezado = "Persona Jurídica \r\nRégimen Ordinario",
                                                            TextoConstancia = DocumentoPdf.TEXTO_CONSTANCIA_FACTURA_DEFAULT,
                                                            TextoQR = generador.GenerarTextoQR(),
                                                            TextoResolucion = DocumentoPdf.CrearTextoResolucion(Rango.NumeroResolucion,
                                                            Rango.FechaResolucion, Rango.Prefijo, Rango.Desde, Rango.Hasta, Rango.VigenciaHasta)
                                                        };
                                                        var bytesPdf = facturaPdf.Generar();
                                                        MostrarAlerta(1, string.Format("{0} - {1}", dianResponse.StatusCode, dianResponse.StatusMessage), dianResponse.StatusDescription);
                                                        EnviarCorreo(oEmpI, oTerI, zipAttachedDocument, bytesPdf);
                                                    }
                                                    else
                                                    {
                                                        MostrarAlerta(0, "Error", Errores);
                                                    }
                                                }
                                                else
                                                {
                                                    MostrarAlerta(0, "Error", string.Join("* ", dianResponse.ErrorMessage));
                                                }
                                            }
                                            else
                                            {
                                                MostrarAlerta(0, "Error", string.Format("FACTURA_VENTA: {0} {1} {2} {3}: {4}", invoice.IssueDate.Value,
                                                invoice.ID.Value, invoice.UUID.Value, dianResponse.StatusCode, dianResponse.StatusMessage));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MostrarAlerta(0, "Error", Errores);
                            }
                        }
                        else
                        {
                            Errores = CrearDocumento();
                            if (!string.IsNullOrEmpty(Errores))
                            {
                                MostrarAlerta(0, "Error", Errores);
                            }
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", Errores);
                        txtCantidad.Text = "";
                        txtBodega.Text = "";
                        txtArticulo.Text = "";
                        txtCodigo.Text = "";
                        txtPrecio.Text = "";
                        txtCodigo.Focus();
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", "No hay artículos para realizar el documento.");
                }
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        private void EnviarCorreo(tblEmpresaItem oEmpI, tblTerceroItem oTerI, byte[] zipAttachedDocument, byte[] bytesPdf)
        {
            try
            {
                MailMessage email = new MailMessage();
                SmtpClient protocol = new SmtpClient();
                email.To.Add(oTerI.Mail);
                string message = string.Format("<h1>Sr(a). {0}</h1><p><h2>Cordial saludo,</h2></p><p>La empresa {1} generó un documento electrónico", oTerI.Nombre, oEmpI.Nombre);
                email.From = new MailAddress("positive@hqs-positive.com", oEmpI.Nombre, System.Text.Encoding.UTF8);
                email.Subject = "Se ha generado un Documento Electrónico.";
                email.SubjectEncoding = System.Text.Encoding.UTF8;
                email.Body = message;
                email.BodyEncoding = System.Text.Encoding.UTF8;
                email.IsBodyHtml = true;
                Attachment attZip = new Attachment(new MemoryStream(zipAttachedDocument), "Documento Electronico.zip", MediaTypeNames.Application.Zip);
                email.Attachments.Add(attZip);
                Attachment attPdf = new Attachment(new MemoryStream(bytesPdf), "Documento Electronico.pdf", MediaTypeNames.Application.Pdf);
                email.Attachments.Add(attPdf);
                email.Priority = MailPriority.High;
                protocol.Credentials = new System.Net.NetworkCredential("positive@hqs-positive.com", "Positive@01");
                protocol.Port = 8889;
                protocol.Host = "mail.hqs-positive.com";
                protocol.EnableSsl = false;
                protocol.DeliveryMethod = SmtpDeliveryMethod.Network;
                protocol.Send(email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void EnviarCorreoPrueba()
        {
            try
            {
                MailMessage email = new MailMessage();
                SmtpClient protocol = new SmtpClient();
                //email.To.Add(oTerI.Mail);
                email.To.Add("osragu90@gmail.com");
                //string message = string.Format("<h1>Sr(a). {0}</h1><p><h2>Cordial saludo,</h2></p><h1></p>", oTerI.Nombre);
                string message = string.Format("<h1>Sr(a). {0}</h1><p><h2>Cordial saludo,</h2></p><h1></p>", "Cliente de prueba");
                //email.From = new MailAddress("Hqspositive@gmail.com", "Oscar Ramirez", System.Text.Encoding.UTF8);
                email.From = new MailAddress("positive@hqs-positive.com", "Positive", System.Text.Encoding.UTF8);
                //email.From = new MailAddress(oEmpI.Correo, oEmpI.Nombre, System.Text.Encoding.UTF8);
                email.Subject = "Se ha generado un Documento Electrónico.";
                email.SubjectEncoding = System.Text.Encoding.UTF8;
                email.Body = message;
                email.BodyEncoding = System.Text.Encoding.UTF8;
                email.IsBodyHtml = true;
                //Attachment att = new Attachment(new MemoryStream(zipAttachedDocument), "Documento Electronico.zip", MediaTypeNames.Application.Zip);
                //email.Attachments.Add(att);
                protocol.Credentials = new System.Net.NetworkCredential("positive@hqs-positive.com", "Positive@01");
                //protocol.Credentials = new System.Net.NetworkCredential(oEmpI.Correo, oEmpI.TestSetId);
                protocol.Port = 8889;
                protocol.Host = "mail.hqs-positive.com";
                protocol.EnableSsl = false;
                protocol.DeliveryMethod = SmtpDeliveryMethod.Network;
                protocol.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string CrearDocumento(string NumeroDocumento = "", string ZipKey = "")
        {
            try
            {
                string Errores = string.Empty;
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                tblDocumentoItem oDocItem = new tblDocumentoItem();
                List<tblDetalleDocumentoItem> oListDet = new List<tblDetalleDocumentoItem>();
                if (!string.IsNullOrEmpty(Request.QueryString["consulta"]) && (Request.QueryString["opcionDocumento"] == tblTipoDocumentoItem.TipoDocumentoEnum.cotizacion.GetHashCode().ToString() || Request.QueryString["opcionDocumento"] == tblTipoDocumentoItem.TipoDocumentoEnum.Remision.GetHashCode().ToString()))
                {
                    oDocItem.idDocumento = long.Parse(hddIdDocumento.Value);
                    oDocItem.BaseIdTipoDocumento = short.Parse(Request.QueryString["opcionDocumento"]);
                    oDocItem.Referencia = Request.QueryString["idDocumento"];
                }
                oDocItem.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                oDocItem.idTercero = long.Parse(hddIdCliente.Value);
                oDocItem.Telefono = txtTelefono.Text;
                oDocItem.Direccion = txtDireccion.Text;
                oDocItem.idCiudad = short.Parse(hddIdCiudad.Value);
                oDocItem.NombreTercero = txtTercero.Text;
                oDocItem.Observaciones = txtObservaciones.Text.Replace(Environment.NewLine, " ");
                oDocItem.idEmpresa = oUsuarioI.idEmpresa;
                oDocItem.idUsuario = oUsuarioI.idUsuario;
                oDocItem.TotalDocumento = decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency);
                oDocItem.TotalIVA = decimal.Parse(txtTotalIVA.Text, NumberStyles.Currency);
                oDocItem.saldo = oDocItem.TotalDocumento;
                if (chkFE.Checked)
                {
                    oDocItem.IdTipoDocumento = 11;
                    oDocItem.NumeroDocumento = NumeroDocumento;
                    oDocItem.ZipKey = ZipKey;
                }
                else
                {
                    oDocItem.IdTipoDocumento = int.Parse(hddTipoDocumento.Value);
                }
                oDocItem.EnCuadre = false;
                oDocItem.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode();
                if (txtDevuelta.Text != "Crédito")
                {
                    oDocItem.Devuelta = decimal.Parse(txtDevuelta.Text, NumberStyles.Currency);
                }
                else if (txtDevuelta1.Value != "0")
                {
                    oDocItem.Devuelta = decimal.Parse(txtDevuelta1.Value, NumberStyles.Currency);
                }
                if (!string.IsNullOrEmpty(ddlVendedor.SelectedValue))
                {
                    oDocItem.IdVendedor = long.Parse(ddlVendedor.SelectedValue);
                }
                oDocItem.TotalDescuento = decimal.Parse(txtTotalDescuento.Text, NumberStyles.Currency);
                oDocItem.TotalAntesIVA = decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency);
                if (!string.IsNullOrEmpty(txtFechaVen.Text))
                {
                    oDocItem.FechaVencimiento = DateTime.Parse(txtFechaVen.Text);
                }
                oDocItem.Propina = decimal.Parse(txtPropina.Text, NumberStyles.Currency);
                if (!string.IsNullOrEmpty(txtReferencia.Text))
                {
                    oDocItem.Referencia = txtReferencia.Text.Trim();
                }
                oDocItem.Impoconsumo = decimal.Parse(txtImpoconsumo.Text, NumberStyles.Currency);
                oDocItem.Resolucion = hddResolucion.Value;
                oDocItem.TotalRetenciones = decimal.Parse(txtTotalRetencion.Text, NumberStyles.Currency);
                short NumeroLinea = 1;
                foreach (DataGridItem Item in dgFactura.Items)
                {
                    tblDetalleDocumentoItem oDetI = new tblDetalleDocumentoItem();
                    oDetI.NumeroLinea = NumeroLinea;
                    oDetI.idArticulo = long.Parse(Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text);
                    oDetI.Articulo = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text;
                    oDetI.ValorUnitario = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency);
                    if (oDocItem.IdTipoDocumento == int.Parse(tblDocumentoBusiness.TipoDocumentoEnum.Compra.GetHashCode().ToString()))
                    {
                        if (!string.IsNullOrEmpty(((TextBox)(Item.Cells[dgFacturaEnum.PrecioVenta.GetHashCode()].FindControl("txtPrecioVenta"))).Text))
                        {
                            oDetI.PrecioVenta = decimal.Parse(((TextBox)(Item.Cells[dgFacturaEnum.PrecioVenta.GetHashCode()].FindControl("txtPrecioVenta"))).Text, NumberStyles.Currency);
                        }
                    }
                    oDetI.IVA = decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.Cantidad = decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.Descuento = decimal.Parse(Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.CostoPonderado = decimal.Parse(Item.Cells[dgFacturaEnum.CostoPonderado.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.ValorDescuento = decimal.Parse(Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.idBodega = long.Parse(Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text);
                    oListDet.Add(oDetI);
                    NumeroLinea++;
                }
                //Calculo de retenciones
                oDocItem.Retenciones = new List<tblDocumentoRetencionItem>();
                foreach (DataGridItem Row in dgRetenciones.Items)
                {
                    if (((CheckBox)(Row.Cells[dgRetencionesEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked == true)
                    {
                        if (oDocItem.TotalAntesIVA > decimal.Parse(Row.Cells[dgRetencionesEnum.Base.GetHashCode()].Text, NumberStyles.Currency))
                        {
                            tblDocumentoRetencionItem Item = new tblDocumentoRetencionItem();
                            Item.IdRetencion = long.Parse(Row.Cells[dgRetencionesEnum.Id.GetHashCode()].Text);
                            Item.TipoDocumento = int.Parse(hddTipoDocumento.Value);
                            Item.Porcentaje = decimal.Parse(Row.Cells[dgRetencionesEnum.Porcentaje.GetHashCode()].Text, NumberStyles.Currency);
                            Item.Base = oDocItem.TotalAntesIVA;
                            Item.Valor = (oDocItem.TotalAntesIVA * (decimal.Parse(Row.Cells[dgRetencionesEnum.Porcentaje.GetHashCode()].Text, NumberStyles.Currency) / 100));
                            oDocItem.Retenciones.Add(Item);
                        }
                    }
                }
                //Cargar datos pagos
                List<tblTipoPagoItem> oTipPagLis = new List<tblTipoPagoItem>();
                tblPagoItem oPagoI = new tblPagoItem();
                if (!chkCredito.Checked)
                {
                    decimal Saldo = oDocItem.TotalDocumento + oDocItem.Propina;
                    if (dgSaldos.Items.Count > 0)
                    {
                        foreach (DataGridItem Item in dgSaldos.Items)
                        {
                            if (((CheckBox)(Item.Cells[dgSaldosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                            {
                                tblTipoPagoItem oTipPagI = new tblTipoPagoItem();
                                oTipPagI.idFormaPago = short.Parse(Item.Cells[dgSaldosColumnsEnum.IdFormaPago.GetHashCode()].Text);
                                oTipPagI.voucher = Item.Cells[dgSaldosColumnsEnum.Id.GetHashCode()].Text;
                                if (decimal.Parse(Item.Cells[dgSaldosColumnsEnum.Saldo.GetHashCode()].Text, NumberStyles.Currency) > Saldo)
                                {
                                    oTipPagI.ValorPago = Saldo;
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
                    decimal PagoEfectivo = 0;
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
                                oTipPagI.voucher = Item.Cells[dgFormasPagosEnum.Voucher.GetHashCode()].Text;
                                PagoEfectivo = PagoEfectivo + oTipPagI.ValorPago;
                                if ((oPagoI.totalPago + PagoEfectivo) > (oDocItem.TotalDocumento + oDocItem.Propina))
                                {
                                    oTipPagI.ValorPago = oTipPagI.ValorPago - decimal.Parse(txtDevuelta.Text, NumberStyles.Currency);
                                }
                            }
                            else
                            {
                                oTipPagI.ValorPago = decimal.Parse(Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text, NumberStyles.Currency);
                                if (!string.IsNullOrEmpty(Item.Cells[dgFormasPagosEnum.Voucher.GetHashCode()].Text) && Item.Cells[dgFormasPagosEnum.Voucher.GetHashCode()].Text != "&nbsp;")
                                {
                                    oTipPagI.voucher = Item.Cells[dgFormasPagosEnum.Voucher.GetHashCode()].Text;
                                }
                            }
                        }
                        oPagoI.totalPago = oPagoI.totalPago + oTipPagI.ValorPago;
                        oTipPagLis.Add(oTipPagI);
                    }
                    if (oTipPagLis.Count > 0)
                    {
                        oPagoI.idTercero = long.Parse(hddIdCliente.Value);
                        oPagoI.idEmpresa = oUsuarioI.idEmpresa;
                        oPagoI.fechaPago = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                        oPagoI.idUsuario = oUsuarioI.idUsuario;
                        oPagoI.idEstado = short.Parse(tblPagoBusiness.EstadoPago.Definitivo.GetHashCode().ToString());
                        oPagoI.EnCuadre = false;
                        if (oDocItem.TotalDocumento > oPagoI.totalPago)
                        {
                            oDocItem.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode();
                        }
                        else
                        {
                            oDocItem.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Pagado.GetHashCode();
                        }
                    }
                    else
                    {
                        if (Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Compra.GetHashCode().ToString() || Request.QueryString["TipoDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString())
                        {
                            oPagoI.idTercero = long.Parse(hddIdCliente.Value);
                            oPagoI.idEmpresa = oUsuarioI.idEmpresa;
                            oPagoI.fechaPago = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                            oPagoI.idUsuario = oUsuarioI.idUsuario;
                            oPagoI.idEstado = short.Parse(tblPagoBusiness.EstadoPago.Definitivo.GetHashCode().ToString());
                            oPagoI.EnCuadre = false;
                            tblTipoPagoItem oTipPagI = new tblTipoPagoItem();
                            oTipPagI.idFormaPago = short.Parse(tblPagoBusiness.FormaPagoEnum.Efectivo.GetHashCode().ToString());
                            oTipPagI.ValorPago = decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency);
                            oPagoI.totalPago = oPagoI.totalPago + oTipPagI.ValorPago;
                            oDocItem.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Pagado.GetHashCode();
                            oTipPagLis.Add(oTipPagI);
                        }
                    }
                    oPagoI.IdTipoPago = short.Parse(tblPagoBusiness.TipoPago.PagoNormal.GetHashCode().ToString());
                }
                else
                {
                    tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                    if (oTerB.ObtenerTerceroPorId(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa).Generico)
                    {
                        Errores = "A los clientes genericos no se les puede generar facturas a crédito.";
                    }
                }
                if (string.IsNullOrEmpty(Errores))
                {
                    if (oDocItem.idDocumento > 0 && oDocItem.IdTipoDocumento == 3)
                    {
                        if (oDocB.traerDocumentoPorId(oDocItem.IdTipoDocumento, oDocItem.idDocumento).IdEstado == tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode())
                        {
                            if (oDocB.ActualizarCotizacion(oDocItem, oListDet))
                            {
                                MostrarAlerta(1, "Exito", "La cotizacion se actualizó con exito.");
                            }
                            else
                            {
                                MostrarAlerta(0, "Error", "La cotización no se pudo actualizar");
                            }
                        }
                        else
                        {
                            MostrarAlerta(0, "Error", "La cotización debe estar en estado activo para poder modificarla");
                        }
                    }
                    else if (oDocB.GuardarTodo(oDocItem, oListDet, oPagoI, oTipPagLis))
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["consulta"]))
                        {
                            if ((Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString() || Request.QueryString["opcionDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Remision.GetHashCode().ToString()))
                            {
                                oDocB.CambiarEstadoDocumento(long.Parse(Request.QueryString["idDocumento"]), tblDocumentoBusiness.EstadoDocumentoEnum.Cerrado.GetHashCode(), int.Parse(Request.QueryString["opcionDocumento"]));
                            }
                        }
                        tblEmpresaItem oEmpI = new tblEmpresaItem();
                        tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                        oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                        string Mensaje = "";
                        string Detalles = "";
                        Detalles = "<table border='1' style='width:100%'><tr><td align='center'>Cant</td><td align='center'>Descripcion</td><td align='center'>Valor</td></tr>";
                        foreach (DataGridItem Item in dgFactura.Items)
                        {
                            Detalles = Detalles + "<tr><td align='center'>" + Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text + "</td>";
                            if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Compra.GetHashCode().ToString())
                            {
                                if (oEmpI.idEmpresa == 29 || oEmpI.idEmpresa == 31 || oEmpI.idEmpresa == 58)
                                {
                                    Detalles = string.Format("{0}<td>{1}- {2}. {3}</td>", Detalles, Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text, Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text.Replace("\"", ""), Item.Cells[dgFacturaEnum.PrecioCosto.GetHashCode()].Text);
                                }
                                else
                                {
                                    Detalles = Detalles + "<td>" + Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text + "</td>";
                                }
                            }
                            else
                            {
                                Detalles = Detalles + "<td>" + Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text + "</td>";
                            }
                            Detalles = Detalles + "<td align='right' style='width: 20%'>" + Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text + "</td></tr>";
                        }
                        Detalles = Detalles + "</table>";
                        if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Compra.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                        {
                            string FormaPago = "";
                            decimal Devuelta = 0;
                            if (hddTipoDocumento.Value != tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                            {
                                FormaPago = "<table border='1' style='width:100%'><tr><td align='center'>Forma</td><td align='center'>Valor</td></tr>";
                                if (!chkCredito.Checked)
                                {
                                    bool ValidadorPago = false;
                                    foreach (DataGridItem Item in dgSaldos.Items)
                                    {
                                        if (((CheckBox)(Item.Cells[dgSaldosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                                        {
                                            ValidadorPago = true;
                                            FormaPago = FormaPago + "<tr><td>" + Item.Cells[dgSaldosColumnsEnum.Tipo.GetHashCode()].Text + "</td>";
                                            FormaPago = FormaPago + "<td align='right'>" + Math.Round(decimal.Parse(Item.Cells[dgSaldosColumnsEnum.Saldo.GetHashCode()].Text, NumberStyles.Currency), 0).ToString(Util.ObtenerFormatoDecimal()) + "</td></tr>";
                                        }
                                    }
                                    foreach (DataGridItem Item in dgFormasPagos.Items)
                                    {
                                        ValidadorPago = true;
                                        FormaPago = FormaPago + "<tr><td>" + Item.Cells[dgFormasPagosEnum.FormaPago.GetHashCode()].Text + "</td>";
                                        FormaPago = FormaPago + "<td align='right'>" + Item.Cells[dgFormasPagosEnum.Valor.GetHashCode()].Text + "</td></tr>";
                                        if (txtDevuelta.Text != "Crédito" && decimal.Parse(txtDevuelta.Text, NumberStyles.Currency) > 0)
                                        {
                                            Devuelta = decimal.Parse(txtDevuelta.Text, NumberStyles.Currency);
                                        }
                                    }
                                    if (!ValidadorPago)
                                    {
                                        string ValorPago = txtValorPago.Value;
                                        FormaPago = FormaPago + "<tr><td>Efectivo</td>";
                                        if (decimal.Parse(ValorPago, NumberStyles.Currency) <= 0)
                                        {
                                            FormaPago = FormaPago + "<td align='right'>" + oPagoI.totalPago.ToString(Util.ObtenerFormatoDecimal()) + "</td></tr>";
                                        }
                                        else
                                        {
                                            FormaPago = FormaPago + "<td align='right'>" + decimal.Parse(ValorPago, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal()) + "</td></tr>";
                                        }
                                        Devuelta = decimal.Parse(txtDevuelta1.Value, NumberStyles.Currency);
                                    }
                                }
                                else
                                {
                                    FormaPago = FormaPago + "<tr><td colspan='2'>Documento a crédito.</td></tr>";
                                }
                                FormaPago = FormaPago + "</table>";
                            }
                            if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                            {
                                Mensaje = string.Format("<div style='position:relative;font-family:arial;'>" +
                                "<div style='font-size: 18px;font-weight: bold; padding-top: 0px; width: 300px; text-align: center;'>{0}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Nit: {1}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Direcci&oacute;n: {2}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Telefono: {3}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center;'>{4}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{5}</div>" +
                                "<div style='font-size: 20px;font-weight: bold; padding-top: 2px; width: 300px;'>Numero Documento: {6}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{7}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Tercero: {8}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Identificaci&oacute;n: {9}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Telefono: {20}</div>" +
                                "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px;'>Direcci&oacute;n: {21}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center;'>DETALLES</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{10}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Antes de IVA: {11}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Descuento: {19}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Valor IVA: {12}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Impoconsumo: {23}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Propina: {22}</div>" +
                                "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Total a pagar: {13}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center;'>Formas de Pago</div>" +
                                "<div style='font-size: 10px;font-weight: bold; padding-top: 2px; width: 300px;'>{17}</div>" +
                                "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Cambio: {18}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Vende: {14}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Vendedor: {24}</div>" +
                                "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px;'>Observaciones: {15}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{16}</div>" +
                                "</div>", oEmpI.Nombre.Replace("\"", ""), oEmpI.Identificacion, oEmpI.Direccion, oEmpI.Telefono, Documento,
                                string.Format("{0} - {1}", oEmpI.TextoEncabezadoFactura.Replace(Environment.NewLine, " "), hddResolucion.Value.Replace(Environment.NewLine, " ")),
                                oDocItem.NumeroDocumento, oDocItem.Fecha, txtTercero.Text.Replace("\"", ""), txtIdentificacion.Text, Detalles, txtAntesIVA.Text,
                                txtTotalIVA.Text, txtTotalFactura.Text, oUsuarioI.Usuario, oDocItem.Observaciones.Replace("\"", ""), oEmpI.TextoPieFactura.Replace(Environment.NewLine, " "), FormaPago, Devuelta.ToString(Util.ObtenerFormatoDecimal()),
                                txtTotalDescuento.Text, txtTelefono.Text, txtDireccion.Text.Replace("\"", ""), txtPropina.Text, txtImpoconsumo.Text, ddlVendedor.SelectedItem == null ? "" : ddlVendedor.SelectedItem.Text);
                                string strScript = string.Format("jQuery(document).ready(function(){{ ImprimirDocumentoServidor(\"{0}\");}});", Mensaje);
                                if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
                                {
                                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
                                }
                            }
                            else
                            {
                                Mensaje = string.Format("<div style='position:relative;font-family:arial;'>" +
                                "<div style='font-size: 18px;font-weight: bold; padding-top: 0px; width: 300px; text-align: center;'>{0}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Nit: {1}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Direcci&oacute;n: {2}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Telefono: {3}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center;'>{4}</div>" +
                                "<div style='font-size: 20px;font-weight: bold; padding-top: 2px; width: 300px;'>Numero Documento: {5}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{6}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Tercero: {7}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Identificaci&oacute;n: {8}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Telefono: {18}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Direcci&oacute;n: {19}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center;'>DETALLES</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{9}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Antes de IVA: {10}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Descuento: {17}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Valor IVA: {11}</div>" +
                                "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Total a pagar: {12}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center;'>Formas de Pago</div>" +
                                "<div style='font-size: 10px;font-weight: bold; padding-top: 2px; width: 300px;'>{15}</div>" +
                                "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Cambio: {16}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Usuario: {13}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Observaciones: {14}</div>" +
                                "</div>", oEmpI.Nombre.Replace("\"", ""), oEmpI.Identificacion, oEmpI.Direccion, oEmpI.Telefono, Documento,
                                oDocItem.NumeroDocumento, oDocItem.Fecha, txtTercero.Text.Replace("\"", ""), txtIdentificacion.Text, Detalles, txtAntesIVA.Text,
                                txtTotalIVA.Text, txtTotalFactura.Text, oUsuarioI.Usuario, oDocItem.Observaciones.Replace("\"", ""), FormaPago, Devuelta.ToString(Util.ObtenerFormatoDecimal()),
                                txtTotalDescuento.Text, txtTelefono.Text, txtDireccion.Text.Replace("\"", ""));
                                string strScript = string.Format("jQuery(document).ready(function(){{ ImprimirDocumentoServidor(\"{0}\");}});", Mensaje);
                                if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
                                {
                                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
                                }
                            }
                        }
                        else
                        {
                            Mensaje = string.Format("<div style='position:relative;font-family:arial;'>" +
                            "<div style='font-size: 18px;font-weight: bold; padding-top: 0px; width: 300px; text-align: center;'>{0}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Nit: {1}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Direcci&oacute;n: {2}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: center;'>Telefono: {3}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center;'>{4}</div>" +
                            "<div style='font-size: 20px;font-weight: bold; padding-top: 2px; width: 300px;'>Numero Documento: {5}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{6}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Tercero: {7}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Identificaci&oacute;n: {8}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Telefono: {15}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Direcci&oacute;n: {16}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align:center;'>DETALLES</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>{9}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Antes de IVA: {10}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Descuento: {14}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Valor IVA: {11}</div>" +
                            "<div style='font-size: 14px;font-weight: bold; padding-top: 2px; width: 300px; text-align: right;'>Total: {12}</div>" +
                            "<div style='font-size: 12px;font-weight: bold; padding-top: 2px; width: 300px;'>Observaciones: {13}</div>" +
                            "</div>", oEmpI.Nombre.Replace("\"", ""), oEmpI.Identificacion, oEmpI.Direccion, oEmpI.Telefono, Documento,
                            oDocItem.NumeroDocumento, oDocItem.Fecha, txtTercero.Text.Replace("\"", ""), txtIdentificacion.Text, Detalles, txtAntesIVA.Text,
                            txtTotalIVA.Text, txtTotalFactura.Text, oDocItem.Observaciones.Replace("\"", ""), txtTotalDescuento.Text, txtTelefono.Text, txtDireccion.Text.Replace("\"", ""));
                            string strScript = string.Format("jQuery(document).ready(function(){{ ImprimirDocumentoServidor(\"{0}\");}});", Mensaje);
                            if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
                            {
                                Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
                            }
                        }
                    }
                    else
                    {
                        Errores = "El documento no se pudo guardar";
                    }
                }
                return Errores;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        protected void btnFacturar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Request.QueryString["opcionDocumento"] == "3")
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                if (oDocB.traerDocumentoPorId(long.Parse(Request.QueryString["opcionDocumento"]), long.Parse(Request.QueryString["idDocumento"])).IdEstado != tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode())
                {
                    MostrarAlerta(0, "Error", "La cotización debe estar en estado abierto para pasar a remisión");
                }
                else
                {
                    Response.Redirect("frmDocumentos.aspx?idDocumento=" + Request.QueryString["idDocumento"] + "&opcionDocumento=" + hddTipoDocumento.Value + "&consulta=1" + "&TipoDocumento=8");
                }
            }
            else if (Request.QueryString["opcionDocumento"] == "8")
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                if (oDocB.traerDocumentoPorId(long.Parse(Request.QueryString["opcionDocumento"]), long.Parse(Request.QueryString["idDocumento"])).IdEstado != tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode())
                {
                    MostrarAlerta(0, "Error", "La remisión debe estar en estado abierto para pasar a factura.");
                }
                else
                {
                    Response.Redirect("frmDocumentos.aspx?idDocumento=" + Request.QueryString["idDocumento"] + "&opcionDocumento=" + hddTipoDocumento.Value + "&consulta=1" + "&TipoDocumento=1");
                }
            }
            else
            {
                Response.Redirect("frmDocumentos.aspx?idDocumento=" + Request.QueryString["idDocumento"] + "&opcionDocumento=" + hddTipoDocumento.Value + "&consulta=1" + "&TipoDocumento=1");
            }
        }
        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTab("aFormaPago", "divFormaPago");
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
        protected void btnAdicionarPago_Click(object sender, EventArgs e)
        {
            ShowTab("aFormaPago", "divFormaPago");
            try
            {
                txtTotalPago.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                DataTable dt = new DataTable();
                if (dgFormasPagos.Items.Count > 0)
                {
                    CargarColumnasFormaPago(ref dt);
                    foreach (DataGridItem Item in dgFormasPagos.Items)
                    {
                        DataRow copia;
                        copia = dt.NewRow();
                        copia["IdFormaPago"] = Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text;
                        copia["FormaPago"] = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text;
                        copia["Valor"] = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text;
                        copia["Voucher"] = Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text;
                        copia["IdTipoTarjetaCredito"] = Item.Cells[dgFacturaEnum.Bodega.GetHashCode()].Text;
                        copia["TarjetaCredito"] = Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text;
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
                    row["Valor"] = decimal.Parse(txtValorE.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                    row["Voucher"] = "";
                    row["IdTipoTarjetaCredito"] = "";
                    row["TarjetaCredito"] = "";
                }
                else if (ddlFormaPago.SelectedValue == tblDocumentoBusiness.FormasPagoEnum.TarjetaCredito.GetHashCode().ToString())
                {
                    row["Valor"] = decimal.Parse(txtValorTJ.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                    row["Voucher"] = txtVoucherTJ.Text;
                    row["IdTipoTarjetaCredito"] = ddlTipoTarjeta.SelectedValue;
                    row["TarjetaCredito"] = ddlTipoTarjeta.SelectedItem.Text;
                }
                else
                {
                    row["Valor"] = decimal.Parse(txtValorO.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
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
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
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
        protected void dgFormasPagos_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    txtTotalPago.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                    DataTable dt = new DataTable();
                    CargarColumnasFormaPago(ref dt);
                    foreach (DataGridItem Item in dgFormasPagos.Items)
                    {
                        if (Item.ItemIndex != e.Item.ItemIndex)
                        {
                            DataRow row;
                            row = dt.NewRow();
                            row["IdFormaPago"] = Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text;
                            row["FormaPago"] = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text;
                            row["Valor"] = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text;
                            row["Voucher"] = Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text;
                            row["IdTipoTarjetaCredito"] = Item.Cells[dgFacturaEnum.Bodega.GetHashCode()].Text;
                            row["TarjetaCredito"] = Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text;
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
        protected void btnIngresar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                tblUsuarioItem oUsuI = new tblUsuarioItem();
                tblUsuarioBusiness oUsuB = new tblUsuarioBusiness(CadenaConexion);
                oUsuI = oUsuB.buscarUsuarioPorLoginPassword(hddLogin.Value.Trim(), hddPassword.Value.Trim());
                if (oUsuI != null && oUsuI.idUsuario > 0)
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.EliminarDetalles.GetHashCode().ToString()));
                    if (oRolPagI.Eliminar)
                    {
                        DataTable dt = new DataTable();
                        CargarColumnasFactura(ref dt);
                        txtAntesIVA.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalIVA.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalFactura.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalDescuento.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        foreach (DataGridItem Item in dgFactura.Items)
                        {
                            if (Item.ItemIndex != int.Parse(hddIdEliminar.Value))
                            {
                                DataRow row;
                                row = dt.NewRow();
                                row["IdArticulo"] = Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text;
                                row["Codigo"] = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text;
                                row["Articulo"] = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text;
                                row["IdBodega"] = Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text;
                                row["Bodega"] = Item.Cells[dgFacturaEnum.Bodega.GetHashCode()].Text;
                                row["Cantidad"] = Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text;
                                row["Descuento"] = Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text;
                                row["ValorUnitario"] = Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text;
                                row["IVA"] = Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text;
                                row["TotalLinea"] = Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text;
                                row["CostoPonderado"] = Item.Cells[dgFacturaEnum.CostoPonderado.GetHashCode()].Text;
                                row["ValorDescuento"] = Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text;
                                dt.Rows.Add(row);
                                if (decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) == 0)
                                {
                                    txtTotalFactura.Text = Math.Round((decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency)), 2).ToString(Util.ObtenerFormatoDecimal());
                                }
                                else
                                {
                                    txtTotalFactura.Text = Math.Round((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency))), 2).ToString(Util.ObtenerFormatoDecimal());
                                }
                                if (decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency) == 0)
                                {
                                    decimal IVA = 1 + (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100);
                                    decimal Cantidad = decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                                    decimal ValorUnitario = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency);
                                    txtAntesIVA.Text = Math.Round(((Cantidad * ValorUnitario) / IVA), 2).ToString(Util.ObtenerFormatoDecimal());
                                }
                                else
                                {
                                    decimal IVA = 1 + (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100);
                                    decimal Cantidad = decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                                    decimal ValorUnitario = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency);
                                    txtAntesIVA.Text = Math.Round((decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency) + ((Cantidad * ValorUnitario) / IVA)), 2).ToString(Util.ObtenerFormatoDecimal());
                                }
                                txtTotalIVA.Text = Math.Round((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) - decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency)), 2).ToString(Util.ObtenerFormatoDecimal());
                            }
                        }
                        dgFactura.DataSource = dt;
                        dgFactura.DataBind();
                        CalcularTotalPago();
                        hddIdEliminar.Value = string.Empty;
                        OcultarControl(divValidador.ClientID);
                    }
                    else
                    {
                        string Titulo = "Requiere Autorización.";
                        if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarValidador"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarValidador", string.Format("$(document).ready(function(){{MostrarValidador('{0}', 600);}});", Titulo), true);
                        }
                    }
                }
                else
                {
                    string Titulo = "Requiere Autorización.";
                    if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarValidador"))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarValidador", string.Format("$(document).ready(function(){{MostrarValidador('{0}', 600);}});", Titulo), true);
                    }
                    MostrarAlerta(0, "Error", "Usuario o contraseña invalidos, por favor ingrese datos validos.");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        protected void btnDescuento_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                tblUsuarioItem oUsuI = new tblUsuarioItem();
                tblUsuarioBusiness oUsuB = new tblUsuarioBusiness(CadenaConexion);
                oUsuI = oUsuB.buscarUsuarioPorLoginPassword(hddLogin.Value.Trim(), hddPassword.Value.Trim());
                if (oUsuI != null && oUsuI.idUsuario > 0)
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Descuentos.GetHashCode().ToString()));
                    if (oRolPagI.Insertar)
                    {
                        if (decimal.Parse(txtDescuento.Text, NumberStyles.Currency) > oUsuarioI.PorcentajeDescuento)
                        {
                            MostrarMensaje("Error", "El descuento otorgado sobre pasa el valor permitido por el usuario.");
                        }
                        else
                        {
                            OcultarControl(divDescuento.ClientID);
                            decimal Descuento = (decimal.Parse(txtDescuento.Text, NumberStyles.Currency) / 100);
                            decimal IVA = 1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100);
                            hddPrecioSinIVA.Value = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / IVA).ToString();
                            hddValorDescuento.Value = (decimal.Parse(hddPrecioSinIVA.Value, NumberStyles.Currency) * Descuento).ToString(Util.ObtenerFormatoDecimal());
                            decimal PrecioSinDescuento = (decimal.Parse(hddPrecioSinIVA.Value, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency));
                            txtPrecio.Text = (PrecioSinDescuento * IVA).ToString(Util.ObtenerFormatoDecimal());
                            txtCodigo.Focus();
                            AdicionarDetalle();
                        }
                    }
                    else
                    {
                        string Titulo = "Requiere Autorización.";
                        if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarValidador"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarValidador", string.Format("$(document).ready(function(){{MostrarDescuento('{0}', 600);}});", Titulo), true);
                        }
                    }
                }
                else
                {
                    string Titulo = "Requiere Autorización.";
                    if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarValidador"))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarValidador", string.Format("$(document).ready(function(){{MostrarDescuento('{0}', 600);}});", Titulo), true);
                    }
                    MostrarMensaje("Error", "Usuario o contraseña invalidos, Por favor ingrese datos validos.");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        protected void btnCalcularDescuento_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Descuentos.GetHashCode().ToString()));
                if (oRolPagI.Insertar)
                {
                    OcultarControl(divDescuento.ClientID);
                    decimal Descuento = 1 - (decimal.Parse(txtDescuento.Text, NumberStyles.Currency) / 100);
                    txtPrecio.Text = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) * Descuento).ToString(Util.ObtenerFormatoDecimal());
                    txtPrecio.Focus();
                }
                else
                {
                    MostrarControl(divDescuento.ClientID);
                    string Titulo = "Requiere Autorización.";
                    if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarValidador"))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarValidador", string.Format("$(document).ready(function(){{MostrarDescuento('{0}', 600);}});", Titulo), true);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        protected void chkPropina_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CalcularTotalesDocumento();
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        protected void txtReferencia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtReferencia.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(hddIdCliente.Value))
                    {
                        if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode().ToString())
                        {
                            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                            tblDocumentoItem oDocI = new tblDocumentoItem();
                            oDocI = oDocB.ObtenerFacturaVentaPorNumero(txtReferencia.Text.Trim(), txtIdentificacion.Text.Trim(), oUsuarioI.idEmpresa);
                            if (long.Parse(hddIdCliente.Value) == oDocI.idTercero)
                            {
                                hhdZipKey.Value = oDocI.ZipKey;
                                txtCodigo.Focus();
                            }
                            else
                            {
                                MostrarAlerta(0, "Error", "El documento no registra a nombre del cliente");
                                txtReferencia.Text = "";
                                txtReferencia.Focus();
                            }
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", "Debe seleccionar un cliente valido.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        protected void GuardarPrecioVenta(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridItem Row in dgFactura.Items)
                {
                    Row.Cells[dgFacturaEnum.PrecioCosto.GetHashCode()].Text = decimal.Parse(((TextBox)(Row.Cells[dgFacturaEnum.PrecioVenta.GetHashCode()].FindControl("txtPrecioVenta"))).Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        protected void dgFactura_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                ((TextBox)(e.Item.Cells[dgFacturaEnum.PrecioVenta.GetHashCode()].FindControl("txtPrecioVenta"))).Text = decimal.Parse(e.Item.Cells[dgFacturaEnum.PrecioCosto.GetHashCode()].Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
            }
        }
        protected void btnPasar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (Request.QueryString["opcionDocumento"] == "3")
                {
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                    if (oDocB.traerDocumentoPorId(long.Parse(Request.QueryString["opcionDocumento"]), long.Parse(Request.QueryString["idDocumento"])).IdEstado != tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode())
                    {
                        MostrarAlerta(0, "Error", "La cotización debe estar en estado abierto para pasar a factura.");
                    }
                    else
                    {
                        if (ValidarCajaAbiertaSinConsulta())
                        {
                            Response.Redirect("frmDocumentos.aspx?idDocumento=" + Request.QueryString["idDocumento"] + "&opcionDocumento=" + hddTipoDocumento.Value + "&consulta=1" + "&TipoDocumento=1");
                        }
                        else
                        {
                            MostrarAlerta(0, "Error", "El usuario no tiene una caja abierta o el tipo de documento no es correcto.");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        protected void CalcularRetenciones(object sender, EventArgs e)
        {
            try
            {
                CalcularTotalesDocumento();
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        //protected void btnPrueba_Click(object sender, EventArgs e)
        //{
        //    EnviarCorreoPrueba();
        //}
    }
}