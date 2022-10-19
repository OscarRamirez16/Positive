using System.Collections.Generic;
using InventarioDao;
using InventarioItem;

namespace InventarioBusiness
{
    public class tblVendedorBusiness
    {
        private string cadenaConexion;

        public tblVendedorBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public tblVendedorItem ObtenerVendedor(long Id)
        {
            tblVendedorDao oVDao = new tblVendedorDao(cadenaConexion);
            return oVDao.ObtenerVendedor(Id);
        }

        public List<tblVendedorItem> ObtenerVendedorListaActivos(long idEmpresa)
        {
            tblVendedorDao oVDao = new tblVendedorDao(cadenaConexion);
            return oVDao.ObtenerVendedorListaActivos(idEmpresa);
        }

        public List<tblVendedorItem> ObtenerVendedorLista(long idEmpresa)
        {
            tblVendedorDao oVDao = new tblVendedorDao(cadenaConexion);
            return oVDao.ObtenerVendedorLista(idEmpresa);
        }

        public bool Guardar(tblVendedorItem Item) {
            tblVendedorDao oVDao = new tblVendedorDao(cadenaConexion);
            return oVDao.Guardar(Item);
        }
    }
}
