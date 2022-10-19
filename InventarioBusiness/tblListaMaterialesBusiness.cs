using System;
using System.Collections.Generic;
using InventarioItem;
using InventarioDao;

namespace InventarioBusiness
{
    public class tblListaMaterialesBusiness
    {
        private string CadenaConexion;

        public tblListaMaterialesBusiness(string CadenaConexion)
        {
            this.CadenaConexion = CadenaConexion;
        }

        public List<JSONItem> ObtenerListaMaterialesPorIdEmpresa(string Nombre, long IdEmpresa)
        {
            try
            {
                tblListaMaterialesDao oListD = new tblListaMaterialesDao(CadenaConexion);
                return oListD.ObtenerListaMaterialesPorIdEmpresa(Nombre, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Guardar(tblListaMaterialesItem Item, List<tblListaMaterialesDetalleItem> Detalles)
        {
            try
            {
                tblListaMaterialesDao oListMatDao = new tblListaMaterialesDao(CadenaConexion);
                return oListMatDao.Guardar(Item, Detalles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblListaMaterialesItem> ObtenerListaMaterialesPorFiltros(long IdArticulo, long IdUsuario, long IdEmpresa)
        {
            try
            {
                tblListaMaterialesDao oListMatDao = new tblListaMaterialesDao(CadenaConexion);
                return oListMatDao.ObtenerListaMaterialesPorFiltros(IdArticulo, IdUsuario, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblListaMaterialesItem ObtenerListaMaterialesPorID(string IdLista)
        {
            try
            {
                tblListaMaterialesDao oListMatDao = new tblListaMaterialesDao(CadenaConexion);
                return oListMatDao.ObtenerListaMateriales(IdLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public tblListaMaterialesItem ObtenerListaMaterialesPorIdArticulo(long IdArticulo)
        {
            try
            {
                tblListaMaterialesDao oListMatDao = new tblListaMaterialesDao(CadenaConexion);
                return oListMatDao.ObtenerListaMaterialesPorIdArticulo(IdArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tblListaMaterialesDetalleItem> ObtenerListaMaterialesDetallesPorID(string IdLista)
        {
            try
            {
                tblListaMaterialesDetalleDao oListMatDao = new tblListaMaterialesDetalleDao(CadenaConexion);
                return oListMatDao.ObtenerListaMaterialesDetalleLista(IdLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
