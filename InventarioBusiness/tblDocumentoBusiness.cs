using System;
using System.Collections.Generic;
using InventarioItem;
using InventarioDao;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InventarioBusiness
{
    public class tblDocumentoBusiness
    {

        private string cadenaConexion;

        public enum FormasPagoEnum
        {
            Efectivo = 1,
            TarjetaCredito = 2,
            TarjetaDebito = 3,
            Cheque = 4,
            Anticipo = 5,
            NotaCreditoCliente = 6,
            Bonos = 7,
            Consignacion = 8,
            DescuentoNomina = 9
        }

        public enum EstadoDocumentoEnum
        {
            Abierto = 1,
            Anular = 2,
            Pendiente = 3,
            Cerrado = 4,
            Pagado = 5
        }

        public enum TipoDocumentoEnum
        {
            Venta = 1,
            Compra = 2,
            Cotizaciones = 3,
            NotaCreditoVenta = 4,
            EntradaMercancia = 5,
            SalidaMercancia = 6,
            NotaCreditoCompra = 7,
            Remision = 8,
            TrasladoMercancia = 9
        }

        public tblDocumentoBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        public DataTable ObtenerCuentaCobro(DateTime FechaInicial, DateTime FechaFinal, long IdUsuario, long IdTercero, long IdEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerCuentaCobro(FechaInicial, FechaFinal, IdUsuario, IdTercero, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerComisionesVentasPorArticulo(DateTime FechaInicial, DateTime FechaFinal, long IdVendedor, long IdEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerComisionesVentasPorArticulo(FechaInicial, FechaFinal, IdVendedor, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerComisionesVentasPorArticuloAgrupadoPorVendedor(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerComisionesVentasPorArticuloAgrupadoPorVendedor(FechaInicial, FechaFinal, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerFacturasPendientesPorPago(long IdTercero)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerFacturasPendientesPorPago(IdTercero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerFacturasCompra(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerFacturasCompra(FechaInicial, FechaFinal, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarCotizacion(tblDocumentoItem oDocItem, List<tblDetalleDocumentoItem> oListDet)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ActualizarCotizacion(oDocItem, oListDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblDocumentoItem ObtenerDevolucionPorReferencia(string NumeroFactura, long IdEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerDevolucionPorReferencia(NumeroFactura, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblDocumentoItem ObtenerFacturaVentaPorNumero(string NumeroDocumento, string Identificacion, long IdEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerFacturaVentaPorNumero(NumeroDocumento, Identificacion, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerDocumentosACredito(DateTime FechaInicial, DateTime FechaFinal, long IdCliente, string Identificacion, long IdEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerDocumentosACredito(FechaInicial, FechaFinal, IdCliente, Identificacion, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerVentasDevolucionesPorFecha(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerVentasDevolucionesPorFecha(FechaInicial, FechaFinal, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerSaldoDocumentoAFavorClienteTodos(long idTercero, long idEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerSaldoDocumentoAFavorClienteTodos(idTercero, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerSaldoDocumentoAFavorCliente(long idTercero, long idEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerSaldoDocumentoAFavorCliente(idTercero, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerDocumentosPagos(DateTime FechaInicial, DateTime FechaFinal, long IdUsuario, string NumeroDocumento, long IdEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerDocumentosPagos(FechaInicial, FechaFinal, IdUsuario, NumeroDocumento, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerPagosDocumentos(DateTime FechaInicial, DateTime FechaFinal, long IdUsuario, string NumeroDocumento, long IdEmpresa)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerPagosDocumentos(FechaInicial, FechaFinal, IdUsuario, NumeroDocumento, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GuardarTodo(tblDocumentoItem oDocItem, List<tblDetalleDocumentoItem>oListDet, tblPagoItem oPagoI, List<tblTipoPagoItem> oTipPagLis)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.GuardarTodo(oDocItem, oListDet, oPagoI, oTipPagLis);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GuardarSoloDocumento(tblDocumentoItem oDocItem, List<tblDetalleDocumentoItem> oListDet)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.GuardarSoloDocumento(oDocItem, oListDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CambiarEstadoDocumento(long IdDocumento, int IdEstado, int TipoDocumento)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.CambiarEstadoDocumento(IdDocumento, IdEstado, TipoDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblFormaPagoItem> ObtenerFormasPagosLista()
        {
            try
            {
                tblFormaPagoDao oForPagD = new tblFormaPagoDao(cadenaConexion);
                return oForPagD.ObtenerFormaPagoLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerInformacionCompra(long idEmpresa, DateTime fechaInicio, DateTime fechaFinal, string NumeroDocumento)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerInformacionCompra(idEmpresa, fechaInicio, fechaFinal, NumeroDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerMovimientosPorDocumento(long idUsuario, long idEmpresa, DateTime fechaInicio, DateTime fechaFinal, long tipoDocumento, string nombre, short idLinea, long idProveedor, long idBodega)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerMovimientosPorDocumento(idUsuario, idEmpresa, fechaInicio, fechaFinal, tipoDocumento, nombre, idLinea, idProveedor, idBodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerReporteDocumentoRangoFecha(long idEmpresa, DateTime fechaInicio, DateTime fechaFinal, long tipoDocumento, long idBodega)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerReporteDocumentoRangoFecha(idEmpresa, fechaInicio, fechaFinal, tipoDocumento, idBodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblTipoDocumentoItem> ObtenerTipoDocumentoLista()
        {
            try
            {
                tblTipoDocumentoDao oTipoDocD = new tblTipoDocumentoDao(cadenaConexion);
                return oTipoDocD.ObtenerTipoDocumentoLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long obtenerNumeracionDocumento(long idEmpresa, long tipoDocumento)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerNumeracionDocumento(idEmpresa,tipoDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblDocumentoItem> traerDocumentosLista(long tipoDocumento, DateTime fechaInicial, DateTime fechaFinal, long idEmpresa, string NumeroDocumento, string Cliente, string Identificacion)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerDocumentoLista(tipoDocumento, fechaInicial, fechaFinal, idEmpresa, NumeroDocumento, Cliente, Identificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerFacturasPendientesPago(long idCliente, long idEmpresa, short Tipo)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                return oDocD.ObtenerFacturasPendientesPago(idCliente, idEmpresa, Tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GuardarVentaRapida(tblDocumentoItem Item, string Articulos)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                if (oDocD.GuardarVentaRapida(Item, Articulos))
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

        public bool guardarDocumento(tblDocumentoItem documento, long tipoDocumento, long idEmpresa)
        {
            try
            {
                tblDocumentoDao oDocumentoD = new tblDocumentoDao(cadenaConexion);
                if (oDocumentoD.Guardar(documento, tipoDocumento, idEmpresa))
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

        public bool guardarDetalleDocumento(tblDetalleDocumentoItem detalle, long tipoDocumento, long idEmpresa)
        {
            try
            {
                tblDetalleDocumentoDao oDetalleD = new tblDetalleDocumentoDao(cadenaConexion);
                return oDetalleD.Insertar(detalle, tipoDocumento, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool eliminarDocumento(long idDocumento, long tipoDocumento)
        {
            try
            {
                tblDocumentoDao oDocD = new tblDocumentoDao(cadenaConexion);
                tblDetalleDocumentoDao oDetalleD = new tblDetalleDocumentoDao(cadenaConexion);
                if (oDetalleD.Eliminar(idDocumento, tipoDocumento))
                {
                    if(oDocD.Eliminar(idDocumento, tipoDocumento)){
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

        public bool pagarDocumentoCompra(tblPagoItem pago, tblPagoDetalleItem detallePago, tblTipoPagoItem tipoPago, long tipoDocumento)
        {
            try
            {
                tblPagoDao oPagoD = new tblPagoDao(cadenaConexion);
                if (oPagoD.InsertarPagoCompra(pago, detallePago, tipoPago, tipoDocumento))
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

        public bool pagarDocumento(tblPagoItem pago, tblPagoDetalleItem detallePago, tblTipoPagoItem tipoPago, long tipoDocumento)
        {
            try
            {
                tblPagoDao oPagoD = new tblPagoDao(cadenaConexion);
                if (oPagoD.Guardar(pago, detallePago, tipoPago, tipoDocumento))
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

        public bool GuardarPago(tblPagoItem pago, List<tblPagoDetalleItem> detallePago, List<tblTipoPagoItem> tipoPago, long tipoDocumento)
        {
            try
            {
                tblPagoDao oPagoD = new tblPagoDao(cadenaConexion);
                return oPagoD.GuardarPago(pago, detallePago, tipoPago, tipoDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblDocumentoItem traerDocumentoPorId(long tipoDocumento, long idDocumento)
        {
            try
            {
                tblDocumentoDao oDocumentoD = new tblDocumentoDao(cadenaConexion);
                tblDocumentoItem oDocI = new tblDocumentoItem();
                oDocI = oDocumentoD.ObtenerDocumento(idDocumento, tipoDocumento);
                tblDetalleDocumentoDao oDetD = new tblDetalleDocumentoDao(cadenaConexion);
                tblTipoDocumentoItem oTDItem;
                tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(cadenaConexion);
                oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
                oDocI.DocumentoLineas = oDetD.ObtenerDetalleDocumentoListaPorIdDocumento(oDocI.idDocumento, oTDItem.TablaDetalle);
                tblTipoPagoDao oTipPagoDao = new tblTipoPagoDao(cadenaConexion);
                oDocI.FormasPago = oTipPagoDao.ObtenerTipoPagoListaPorDocumento(oDocI.idDocumento, tipoDocumento);
                return oDocI;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarDetallePorIdDocumentoIdDetalle(long idDocumento, long idDetalle, long tipoDocumento)
        {
            try
            {
                tblDetalleDocumentoDao oDetalleD = new tblDetalleDocumentoDao(cadenaConexion);
                return oDetalleD.EliminarDetallePorIdDocumentoIdDetalle(idDocumento, idDetalle, tipoDocumento);
            }
            catch
            {
                return false;
            }
        }

        public bool ActualizarDetalle(tblDetalleDocumentoItem detalle, long tipoDocumento)
        {
            try
            {
                tblDetalleDocumentoDao oDetalleD = new tblDetalleDocumentoDao(cadenaConexion);
                return oDetalleD.Actualizar(detalle, tipoDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Venta rapida
        public tblVentaRapidaItem ObtenerVentaRapida(long Id, long idEmpresa) {
            tblVentaRapidaDao oVRDao = new tblVentaRapidaDao(cadenaConexion);
            return oVRDao.ObtenerVentaRapida(Id, idEmpresa);
        }
        public List<tblVentaRapidaItem> ObtenerVentaRapidaLista(long idEmpresa, long idArticulo, bool Todos, long idTercero,long idBodega)
        {
            tblVentaRapidaDao oVRDao = new tblVentaRapidaDao(cadenaConexion);
            return oVRDao.ObtenerVentaRapidaLista(idEmpresa, idArticulo, Todos, idTercero, idBodega);
        }
        public List<tblVentaRapidaItem> ObtenerVentaRapidaBusqueda(long idEmpresa, long idArticulo, string Texto)
        {
            tblVentaRapidaDao oVRDao = new tblVentaRapidaDao(cadenaConexion);
            return oVRDao.ObtenerVentaRapidaBusqueda(idEmpresa, idArticulo, Texto);
        }
        public bool Guardar(tblVentaRapidaItem Item) {
            tblVentaRapidaDao oVRDao = new tblVentaRapidaDao(cadenaConexion);
            return oVRDao.Guardar(Item);
        }
        public Bitmap CambiarTamanoImagen(Image pImagen, int pAncho, int pAlto)
        {
            //creamos un bitmap con el nuevo tamaño
            Bitmap vBitmap = new Bitmap(pAncho, pAlto);
            //creamos un graphics tomando como base el nuevo Bitmap
            using (Graphics vGraphics = Graphics.FromImage((Image)vBitmap))
            {
                //especificamos el tipo de transformación, se escoge esta para no perder calidad.
                vGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //Se dibuja la nueva imagen
                vGraphics.DrawImage(pImagen, 0, 0, pAncho, pAlto);
            }
            //retornamos la nueva imagen
            return vBitmap;
        }
        public List<CotizacionVentaRapidaItem> ObtenerCotizacionVentaRapida(long idCotizacion, long idEmpresa) {
            CotizacionVentaRapidaDao oCPVR = new CotizacionVentaRapidaDao(cadenaConexion);
            return oCPVR.ObtenerCotizacionVentaRapida(idCotizacion, idEmpresa);
        }

        public List<PedidoAbierto> ObtenerCotizacionVentaRapidaLista(long idUsuario) {
            CotizacionVentaRapidaDao oCPVR = new CotizacionVentaRapidaDao(cadenaConexion);
            return oCPVR.ObtenerCotizacionVentaRapidaLista(idUsuario);
        }

        public bool Eliminar(CotizacionVentaRapidaItem Item) {
            CotizacionVentaRapidaDao oCPVR = new CotizacionVentaRapidaDao(cadenaConexion);
            return oCPVR.Eliminar(Item);
        }
        public bool Guardar(CotizacionVentaRapidaItem Item)
        {
            CotizacionVentaRapidaDao oCPVR = new CotizacionVentaRapidaDao(cadenaConexion);
            return oCPVR.Guardar(Item);
        }
        #endregion
        #region  Entrega de Facturas
        public DataTable FacturasPendienteEntrega(long idEmpresa)
        {
            tblDocumentoDao oDocumentoD = new tblDocumentoDao(cadenaConexion);
            return oDocumentoD.FacturasPendienteEntrega(idEmpresa);
        }
        public bool ActualizarFacturaEntrega(long idDocumento) {
            tblDocumentoDao oDocumentoD = new tblDocumentoDao(cadenaConexion);
            return oDocumentoD.ActualizarFacturaEntrega(idDocumento);
        }
        #endregion
        #region entrega pedidos cocina
        public List<CotizacionCocinaItem> ObtenerCotizacionCocinaLista(long idEmpresa) {
            CotizacionCocinaDao oCCDao = new CotizacionCocinaDao(cadenaConexion);
            return oCCDao.ObtenerCotizacionCocinaLista(idEmpresa);
        }
        public bool EliminarCotizacionCocina(long idEmpresa) {
            CotizacionCocinaDao oCCDao = new CotizacionCocinaDao(cadenaConexion);
            return oCCDao.Eliminar(idEmpresa);
        }
        public bool Guardar(CotizacionCocinaItem Item) {
            CotizacionCocinaDao oCCDao = new CotizacionCocinaDao(cadenaConexion);
            return oCCDao.Guardar(Item);
        }
        #endregion
    }
}
