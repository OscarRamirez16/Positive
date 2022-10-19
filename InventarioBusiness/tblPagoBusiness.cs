using System;
using System.Collections.Generic;
using InventarioItem;
using InventarioDao;
using System.Data;

namespace InventarioBusiness
{

    public class tblPagoBusiness
    {

        private string cadenaConexion;

        public enum TipoPago
        {
            PagoNormal = 0,
            Anticipo = 1
        }

        public enum EstadoPago
        {
            Definitivo = 1,
            Anulado = 2
        }

        public enum FormaPagoEnum
        {
            Efectivo = 1,
            TarjetaCredito = 2,
            TarjetaDebito = 3,
            Cheque = 4,
            Bonos = 5,
            Consignacion = 6,
            DescuentoNomina = 7
        }

        public tblPagoBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        public tblPagoItem ObtenerPagoPorIDPago(long idPago, int Tipo)
        {
            try
            {
                tblPagoDao oPagoD = new tblPagoDao(cadenaConexion);
                return oPagoD.ObtenerPago(idPago, Tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerConciliaciones(DateTime FechaInicial, DateTime FechaFinal, string Tercero, string Identificacion, long IdEmpresa)
        {
            try
            {
                tblPagoDao oPagoD = new tblPagoDao(cadenaConexion);
                return oPagoD.ObtenerConciliaciones(FechaInicial, FechaFinal, Tercero, Identificacion, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerAnticipos(DateTime FechaInicial, DateTime FechaFinal, string Tercero, string Identificacion, string IdAnticipo, long IdEmpresa)
        {
            try
            {
                tblPagoDao oPagoD = new tblPagoDao(cadenaConexion);
                return oPagoD.ObtenerAnticipos(FechaInicial, FechaFinal, Tercero, Identificacion, IdAnticipo, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool GuardarPago(tblPagoItem Item, List<tblPagoDetalleItem> oListDet, List<tblTipoPagoItem> oListTip, long TipoDocumento)
        {
            try
            {
                tblPagoDao oPagoD = new tblPagoDao(cadenaConexion);
                return oPagoD.GuardarPago(Item, oListDet, oListTip, TipoDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblPagoItem> ObtenerPagosPorProveedor(long IdProveedor, DateTime fechaInical, DateTime fechaFinal)
        {
            try
            {
                tblPagoDao oPagoD = new tblPagoDao(cadenaConexion);
                return oPagoD.ObtenerPagosPorProveedor(IdProveedor, fechaInical, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblPagoItem> ObtenerPagosPorCliente(long idCliente, DateTime fechaInical, DateTime fechaFinal)
        {
            try
            {
                tblPagoDao oPagoD = new tblPagoDao(cadenaConexion);
                return oPagoD.ObtenerPagosPorCliente(idCliente, fechaInical, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblPagoDetalleItem> ObtenerDetallesPagoProveedorPorIDPago(long idPago)
        {
            try
            {
                tblPagoDetalleDao oPagoD = new tblPagoDetalleDao(cadenaConexion);
                return oPagoD.ObtenerDetallesPagoProveedorPorIDPago(idPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblPagoDetalleItem> ObtenerDetallesPagoPorIDPago(long idPago)
        {
            try
            {
                tblPagoDetalleDao oPagoD = new tblPagoDetalleDao(cadenaConexion);
                return oPagoD.ObtenerDetallesPagoPorIDPago(idPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public tblTipoTarjetaCreditoItem ObtenerTipoTarjetaCredito(long Id, long idEmpresa) {
            tblTipoTarjetaCreditoDao oTTCDao = new tblTipoTarjetaCreditoDao(cadenaConexion);
            return oTTCDao.ObtenerTipoTarjetaCredito(Id, idEmpresa);
        }
        public List<tblTipoTarjetaCreditoItem> ObtenerTipoTarjetaCreditoLista(string Texto, long idEmpresa,bool Todos) {
            tblTipoTarjetaCreditoDao oTTCDao = new tblTipoTarjetaCreditoDao(cadenaConexion);
            return oTTCDao.ObtenerTipoTarjetaCreditoLista(Texto, idEmpresa,Todos);
        }
        public bool Guardar(tblTipoTarjetaCreditoItem Item) {
            tblTipoTarjetaCreditoDao oTTCDao = new tblTipoTarjetaCreditoDao(cadenaConexion);
            return oTTCDao.Guardar(Item);
        }

        public List<tblTipoPagoItem> ObtenerTipoPago(long idPago)
        {
            try
            {
                tblTipoPagoDao oTipoPagoD = new tblTipoPagoDao(cadenaConexion);
                return oTipoPagoD.ObtenerTipoPagoLista(idPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
