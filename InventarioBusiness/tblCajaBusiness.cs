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
