using System;
using System.Collections.Generic;
using InventarioDao;
using InventarioItem;

namespace InventarioBusiness
{
    public class tblMovimientosDiariosBusiness
    {

        private string cadenaConexion;

        public tblMovimientosDiariosBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public decimal ObtenerPagosFacturasACredito(long IdCuadreCaja, long IdUsuario, long IdEmpresa)
        {
            try
            {
                tblMovimientosDiariosDao oMovD = new tblMovimientosDiariosDao(cadenaConexion);
                return oMovD.ObtenerPagosFacturasACredito(IdCuadreCaja,IdUsuario, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal ObtenerPropinaDocumento(long IdUsuario, long IdEmpresa)
        {
            try
            {
                tblMovimientosDiariosDao oMovD = new tblMovimientosDiariosDao(cadenaConexion);
                return oMovD.ObtenerPropinaDocumento(IdUsuario, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal ObtenerValorRemisionesUsuario(long IdUsuario)
        {
            try
            {
                tblMovimientosDiariosDao oMovD = new tblMovimientosDiariosDao(cadenaConexion);
                return oMovD.ObtenerValorRemisionesUsuario(IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Guardar(tblMovimientosDiariosItem Item)
        {
            try
            {
                tblMovimientosDiariosDao oMovD = new tblMovimientosDiariosDao(cadenaConexion);
                return oMovD.Guardar(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblMovimientosDiariosItem> ObtenerMovimientosDiariosCuadre(long IdEmpresa, long IdUsuario)
        {
            try
            {
                tblMovimientosDiariosDao oMovD = new tblMovimientosDiariosDao(cadenaConexion);
                return oMovD.ObtenerMovimientosDiariosCuadre(IdEmpresa, IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal ObtenerMovimientosDiariosPorTipoMovimiento(tblMovimientosDiariosItem Item)
        {
            try
            {
                tblMovimientosDiariosDao oMovD = new tblMovimientosDiariosDao(cadenaConexion);
                return oMovD.ObtenerMovimientosDiariosPorTipoMovimiento(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblMovimientosDiariosItem> ObtenerMovimientosDiariosLista(DateTime Desde, DateTime Hasta, long idEmpresa, short TipoMovimiento, long idUsuario)
        {
            try
            {
                tblMovimientosDiariosDao oMovD = new tblMovimientosDiariosDao(cadenaConexion);
                return oMovD.ObtenerMovimientosDiariosLista(Desde, Hasta, idEmpresa, TipoMovimiento, idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblTipoMovimientoItem> ObtenerTipoMovimientoDiarioLista()
        {
            try
            {
                tblMovimientosDiariosDao oMovD = new tblMovimientosDiariosDao(cadenaConexion);
                return oMovD.ObtenerTipoMovimientoDiarioLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
