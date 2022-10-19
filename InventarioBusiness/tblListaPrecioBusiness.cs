using System;
using System.Collections.Generic;
using InventarioDao;
using InventarioItem;

namespace InventarioBusiness
{
    public class tblListaPrecioBusiness
    {

        private string CadenaConexion;

        public tblListaPrecioBusiness(string CadenaConexion)
        {
            this.CadenaConexion = CadenaConexion;
        }

        public bool Guardar(tblListaPrecioItem Item)
        {
            try
            {
                tblListaPrecioDao oListDao = new tblListaPrecioDao(CadenaConexion);
                return oListDao.Guardar(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblListaPrecioItem ObtenerListaPrecioPorID(long IdListaPrecio)
        {
            try
            {
                tblListaPrecioDao oListDao = new tblListaPrecioDao(CadenaConexion);
                return oListDao.ObtenerListaPrecio(IdListaPrecio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblListaPrecioItem> ObtenerListaPrecioLista(long IdEmpresa)
        {
            try
            {
                tblListaPrecioDao oListDao = new tblListaPrecioDao(CadenaConexion);
                return oListDao.ObtenerListaPrecioLista(IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
