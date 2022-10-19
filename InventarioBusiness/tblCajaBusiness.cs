using System;
using System.Collections.Generic;
using InventarioDao;
using InventarioItem;

namespace InventarioBusiness
{
    public class tblCajaBusiness
    {
        private string cadenaConexion;

        public tblCajaBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        public List<tblCajaItem> ObtenerCajaProximaVencer(long IdEmpresa)
        {
            try
            {
                tblCajaDao oCajaD = new tblCajaDao(cadenaConexion);
                return oCajaD.ObtenerCajaProximaVencer(IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public tblCajaItem ObtenerLinea(long idCaja)
        {
            try
            {
                tblCajaDao oCajaD = new tblCajaDao(cadenaConexion);
                return oCajaD.ObtenerCaja(idCaja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Guardar(tblCajaItem Item)
        {
            try
            {
                tblCajaDao oCajaD = new tblCajaDao(cadenaConexion);
                return oCajaD.Guardar(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblCajaItem> ObtenerCajaLista(long idEmpresa)
        {
            try
            {
                tblCajaDao oCajaD = new tblCajaDao(cadenaConexion);
                return oCajaD.ObtenerCajaListaPorIdEmpresa(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tblCajaItem> ObtenerCajaListaActivas(long idEmpresa)
        {
            try
            {
                tblCajaDao oCajaD = new tblCajaDao(cadenaConexion);
                return oCajaD.ObtenerCajaListaActivas(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblCajaItem> ObtenerCajaListaPorIdEmpresa(long idEmpresa)
        {
            try
            {
                tblCajaDao oCajaD = new tblCajaDao(cadenaConexion);
                return oCajaD.ObtenerCajaListaPorIdEmpresa(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblCajaItem ObtenerCajaPorID(long IdCaja)
        {
            try
            {
                tblCajaDao oCajaD = new tblCajaDao(cadenaConexion);
                return oCajaD.ObtenerCaja(IdCaja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
