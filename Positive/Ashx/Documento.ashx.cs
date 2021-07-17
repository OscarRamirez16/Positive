using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;

namespace Inventario.Ashx
{
    /// <summary>
    /// Descripción breve de Documento
    /// </summary>
    public class Documento : IHttpHandler
    {

        private string CadenaConexion = "";

        private enum opcionGuardarEnum
        {
            encabezado = 1,
            detalles = 2,
            eliminarDocumento = 3,
            pago = 4,
            actualizarDocumento = 5,
            actualizarDetalle = 6,
            eliminarDetalle = 7,
            obternerNumeroFactura = 8,
            obternerNumeroFacturaVenta = 9,
            pagoCompra = 10
        }

        public void ProcessRequest(HttpContext context)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            string opcion = context.Request.QueryString["opcion"];
            if (opcion == opcionGuardarEnum.encabezado.GetHashCode().ToString())
            {
                context.Response.Write(guardarEncabezado(context));
            }
            if (opcion == opcionGuardarEnum.detalles.GetHashCode().ToString())
            {
                context.Response.Write(guardarDetalles(context));
            }
            if (opcion == opcionGuardarEnum.eliminarDocumento.GetHashCode().ToString())
            {
                context.Response.Write(eliminarDocumento(context));
            }
            if (opcion == opcionGuardarEnum.pago.GetHashCode().ToString())
            {
                context.Response.Write(pagarDocumento(context));
            }
            if (opcion == opcionGuardarEnum.actualizarDetalle.GetHashCode().ToString())
            {
                context.Response.Write(ActualizarDetalle(context));
            }
            if (opcion == opcionGuardarEnum.eliminarDetalle.GetHashCode().ToString())
            {
                context.Response.Write(EliminarDetallePorIdDocumentoIdDetalle(context));
            }
            if (opcion == opcionGuardarEnum.actualizarDocumento.GetHashCode().ToString())
            {
                context.Response.Write(actualizarDocumento(context));
            }
            if (opcion == opcionGuardarEnum.obternerNumeroFactura.GetHashCode().ToString())
            {
                context.Response.Write(obternerNumeroFactura(context));
            }
            //if (opcion == opcionGuardarEnum.obternerNumeroFacturaVenta.GetHashCode().ToString())
            //{
            //    context.Response.Write(obternerNumeroFacturaVenta(context));
            //}
            if (opcion == opcionGuardarEnum.pagoCompra.GetHashCode().ToString())
            {
                context.Response.Write(pagarDocumentoCompra(context));
            }
        }

        private string guardarEncabezado(HttpContext context)
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                tblDocumentoItem oDocItem = new tblDocumentoItem();
                oDocItem.Fecha = Util.ObtenerFecha(long.Parse(context.Request.QueryString["_i_e"]));
                oDocItem.idTercero = long.Parse(context.Request.QueryString["idTercero"]);
                oDocItem.Telefono = context.Request.QueryString["telefono"];
                oDocItem.Direccion = context.Request.QueryString["direccion"];
                oDocItem.idCiudad = short.Parse(context.Request.QueryString["idCiudad"]);
                oDocItem.NombreTercero = context.Request.QueryString["nombreTercero"];
                oDocItem.Observaciones = context.Request.QueryString["observaciones"];
                oDocItem.idEmpresa = long.Parse(context.Request.QueryString["_i_e"]);
                oDocItem.idUsuario = long.Parse(context.Request.QueryString["_i_u"]);
                oDocItem.TotalDocumento = decimal.Parse(context.Request.QueryString["TotalDocumento"]);
                oDocItem.TotalIVA = decimal.Parse(context.Request.QueryString["TotalIVA"]);
                oDocItem.saldo = oDocItem.TotalDocumento;
                if (oDocB.guardarDocumento(oDocItem, long.Parse(context.Request.QueryString["tipoDocumento"]), long.Parse(context.Request.QueryString["_i_e"])))
                {
                    return string.Format("{0},{1}",oDocItem.idDocumento, oDocItem.NumeroDocumento);
                }
                else
                {
                    return "-1";
                }
            }
            catch
            {
                return "-1";
            }
        }

        private long actualizarDocumento(HttpContext context)
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                tblDocumentoItem oDocItem = new tblDocumentoItem();
                oDocItem.idDocumento = long.Parse(context.Request.QueryString["idDocumento"]);
                oDocItem.NumeroDocumento = context.Request.QueryString["numeroDocumento"];
                oDocItem.Fecha = DateTime.Parse(context.Request.QueryString["fecha"]);
                oDocItem.idTercero = long.Parse(context.Request.QueryString["idTercero"]);
                oDocItem.Telefono = context.Request.QueryString["telefono"];
                oDocItem.Direccion = context.Request.QueryString["direccion"];
                oDocItem.idCiudad = short.Parse(context.Request.QueryString["idCiudad"]);
                oDocItem.NombreTercero = context.Request.QueryString["nombreTercero"];
                oDocItem.Observaciones = context.Request.QueryString["observaciones"];
                oDocItem.idEmpresa = long.Parse(context.Request.QueryString["_i_e"]);
                oDocItem.idUsuario = long.Parse(context.Request.QueryString["_i_u"]);
                oDocItem.TotalDocumento = decimal.Parse(context.Request.QueryString["TotalDocumento"]);
                oDocItem.TotalIVA = decimal.Parse(context.Request.QueryString["TotalIVA"]);
                oDocB.guardarDocumento(oDocItem, long.Parse(context.Request.QueryString["tipoDocumento"]), long.Parse(context.Request.QueryString["_i_e"]));
                return oDocItem.idDocumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private long guardarDetalles(HttpContext context)
        {
            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
            tblDetalleDocumentoItem oDocItem = new tblDetalleDocumentoItem();
            try
            {
                oDocItem.idDocumento = long.Parse(context.Request.QueryString["idDocumento"]);
                oDocItem.NumeroLinea = short.Parse(context.Request.QueryString["numeroLinea"]);
                oDocItem.idArticulo = long.Parse(context.Request.QueryString["idArticulo"]);
                oDocItem.Articulo = context.Request.QueryString["descripcion"];
                oDocItem.ValorUnitario = decimal.Parse(context.Request.QueryString["precio"].Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                oDocItem.IVA = decimal.Parse(context.Request.QueryString["impuesto"]);
                oDocItem.Cantidad = decimal.Parse(context.Request.QueryString["cantidad"]);
                oDocItem.idBodega = long.Parse(context.Request.QueryString["_i_b"]);
                if (oDocB.guardarDetalleDocumento(oDocItem, long.Parse(context.Request.QueryString["tipoDocumento"]), long.Parse(context.Request.QueryString["_i_e"])))
                {
                    return oDocItem.idDetalleDocumento;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                oDocB.eliminarDocumento(oDocItem.idDocumento, long.Parse(context.Request.QueryString["tipoDocumento"]));
                throw ex;
            }
        }

        public long eliminarDocumento(HttpContext context)
        {
            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
            if (oDocB.eliminarDocumento(long.Parse(context.Request.QueryString["idDocumento"]), long.Parse(context.Request.QueryString["tipoDocumento"])))
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public long pagarDocumento(HttpContext context)
        {
            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
            tblPagoItem oPagoI = new tblPagoItem();
            tblPagoDetalleItem oPagDetI = new tblPagoDetalleItem();
            tblTipoPagoItem oTipPagI = new tblTipoPagoItem();
            oPagoI.idTercero = long.Parse(context.Request.QueryString["idTercero"]);
            oPagoI.totalPago = decimal.Parse(context.Request.QueryString["TotalPago"]);
            oPagoI.idEmpresa = long.Parse(context.Request.QueryString["idEmpresa"]);
            oPagoI.fechaPago = Util.ObtenerFecha(long.Parse(context.Request.QueryString["idEmpresa"]));
            oPagoI.idUsuario = long.Parse(context.Request.QueryString["idUsuario"]);
            oPagoI.idEstado = short.Parse(context.Request.QueryString["idEstado"]);
            oPagDetI.valorAbono = decimal.Parse(context.Request.QueryString["valorPago"]);
            if (context.Request.QueryString["idDocumento"] != "")
            {
                oPagDetI.idDocumento = long.Parse(context.Request.QueryString["idDocumento"]);
                oTipPagI.ValorPago = decimal.Parse(context.Request.QueryString["valorPago"]);
                oTipPagI.idFormaPago = short.Parse(context.Request.QueryString["formaPago"]);
                oTipPagI.voucher = context.Request.QueryString["voucher"].ToString();
                if (oDocB.pagarDocumento(oPagoI, oPagDetI, oTipPagI, long.Parse(context.Request.QueryString["tipoDocumento"])))
                {
                    return oPagoI.idPago;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public long pagarDocumentoCompra(HttpContext context)
        {
            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
            tblPagoItem oPagoI = new tblPagoItem();
            tblPagoDetalleItem oPagDetI = new tblPagoDetalleItem();
            tblTipoPagoItem oTipPagI = new tblTipoPagoItem();
            oPagoI.idTercero = long.Parse(context.Request.QueryString["idTercero"]);
            oPagoI.totalPago = decimal.Parse(context.Request.QueryString["TotalPago"]);
            oPagoI.idEmpresa = long.Parse(context.Request.QueryString["idEmpresa"]);
            oPagoI.fechaPago = Util.ObtenerFecha(long.Parse(context.Request.QueryString["idEmpresa"]));
            oPagoI.idUsuario = long.Parse(context.Request.QueryString["idUsuario"]);
            oPagoI.idEstado = short.Parse(context.Request.QueryString["idEstado"]);
            oPagDetI.valorAbono = decimal.Parse(context.Request.QueryString["valorPago"]);
            oPagDetI.idDocumento = long.Parse(context.Request.QueryString["idDocumento"]);
            oTipPagI.ValorPago = decimal.Parse(context.Request.QueryString["valorPago"]);
            oTipPagI.idFormaPago = short.Parse(context.Request.QueryString["formaPago"]);
            oTipPagI.voucher = context.Request.QueryString["voucher"].ToString();
            if (oDocB.pagarDocumentoCompra(oPagoI, oPagDetI, oTipPagI, long.Parse(context.Request.QueryString["tipoDocumento"])))
            {
                return oPagoI.idPago;
            }
            else
            {
                return -1;
            }
        }

        private long ActualizarDetalle(HttpContext context)
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                tblDetalleDocumentoItem oDocItem = new tblDetalleDocumentoItem();
                oDocItem.idDocumento = long.Parse(context.Request.QueryString["idDocumento"]);
                oDocItem.idDetalleDocumento = long.Parse(context.Request.QueryString["idDetalleDocumento"]);
                oDocItem.NumeroLinea = short.Parse(context.Request.QueryString["numeroLinea"]);
                oDocItem.idArticulo = long.Parse(context.Request.QueryString["idArticulo"]);
                oDocItem.Articulo = context.Request.QueryString["descripcion"];
                oDocItem.ValorUnitario = decimal.Parse(context.Request.QueryString["precio"]);
                oDocItem.IVA = decimal.Parse(context.Request.QueryString["impuesto"]);
                oDocItem.Cantidad = decimal.Parse(context.Request.QueryString["cantidad"]);
                if (oDocB.ActualizarDetalle(oDocItem, long.Parse(context.Request.QueryString["tipoDocumento"])))
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long EliminarDetallePorIdDocumentoIdDetalle(HttpContext context)
        {
            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
            if (oDocB.EliminarDetallePorIdDocumentoIdDetalle(long.Parse(context.Request.QueryString["idDocumento"]), long.Parse(context.Request.QueryString["idDetalleDocumento"]), long.Parse(context.Request.QueryString["tipoDocumento"])))
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public long obternerNumeroFactura(HttpContext context)
        {
            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
            return oDocB.obtenerNumeracionDocumento(long.Parse(context.Request.QueryString["ValorBusqueda"]), long.Parse(context.Request.QueryString["tipoFactura"]));
        }

        //public long obternerNumeroFacturaVenta(HttpContext context)
        //{
        //    tblNumeracionFacturaVentaBusiness oNumBiz = new tblNumeracionFacturaVentaBusiness(CadenaConexion);
        //    tblNumeracionFacturaVentaItem oNumItem = oNumBiz.ObtenerNumeracionFacturaVenta(long.Parse(context.Request.QueryString["ValorBusqueda"]),);
        //    return long.Parse(oNumItem.ProximoValor);
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}