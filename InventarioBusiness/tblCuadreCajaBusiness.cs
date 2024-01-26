using System;
using System.Collections.Generic;
using InventarioItem;
using InventarioDao;
using System.Data;

namespace InventarioBusiness
{
    public class tblCuadreCajaBusiness
    {

        private string cadenaConexion;

        public enum tipoDetalleCuadreCajaEnum
        {
            Ingreso = 1,
            Retiro = 2,
            Pago = 3,
            Compra = 4
        }

        public tblCuadreCajaBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public DataTable ObtenerValoresImpuestosAgrupados(long IdUsuario, long IdEmpresa)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ObtenerValoresImpuestosAgrupados(IdUsuario, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblCuadreCajaItem ObtenerCuadreCajaPorID(long IdCuadreCaja)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ObtenerCuadreCajaPorID(IdCuadreCaja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ObtenerNumeroFacturaDesde(long idEmpresa, long idUsuario)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ObtenerNumeroFacturaDesde(idEmpresa, idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal ObtenerCreditos(tblCuadreCajaItem Item)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ObtenerCreditos(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblTipoPagoItem> ObtenerFormasPagosVentas(long IdEmpresa, long IdUsuario)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ObtenerFormasPagosVentas(IdEmpresa, IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblCuadreCajaItem ObtenerTotalVentasCuadre(tblCuadreCajaItem Item)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ObtenerTotalVentasCuadre(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CuadreCajaItem> ObtenerTotalVentasCuadreConResumen(tblCuadreCajaItem Item)
        {
            tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
            return oCuaD.ObtenerTotalVentasCuadreConResumen(Item);
        }

        public decimal ObtenerTotalComprasCuadre(tblCuadreCajaItem Item)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ObtenerTotalComprasCuadre(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Guardar(tblCuadreCajaItem Item)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.Guardar(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblCuadreCajaItem ObtenerCuadreCajaLista(tblCuadreCajaItem Item)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ObtenerCuadreCajaLista(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblCuadreCajaItem> ObtenerCuadreCajaListaReporte(DateTime Desde, DateTime Hasta, long idEmpresa, short idCaja, long idUsuario)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ObtenerCuadreCajaListaReporte(Desde, Hasta, idEmpresa, idCaja, idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblCuadreCajaItem ObtenerCuadreCajaListaPorUsuario(tblCuadreCajaItem Item)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ObtenerCuadreCajaListaPorUsuario(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long ValidarCajaAbierta(tblCuadreCajaItem Item)
        {
            try
            {
                tblCuadreCajaDao oCuaD = new tblCuadreCajaDao(cadenaConexion);
                return oCuaD.ValidarCajaAbierta(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
