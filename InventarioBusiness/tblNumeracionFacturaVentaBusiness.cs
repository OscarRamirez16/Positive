using InventarioDao;
using InventarioItem;

namespace InventarioBusiness
{
    public class tblNumeracionFacturaVentaBusiness
    {

        private string cadenaConexion;

        public tblNumeracionFacturaVentaBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public tblNumeracionFacturaVentaItem ObtenerNumeracionFacturaVenta(long idEmpresa, long idUsuario)
        {
            tblNumeracionFacturaVentaDao oNumDao = new tblNumeracionFacturaVentaDao(cadenaConexion);
            return oNumDao.ObtenerNumeracionFacturaVenta(idEmpresa, idUsuario);
        }

        public bool GuardarNumeracionVenta(tblNumeracionFacturaVentaItem Item)
        {
            tblNumeracionFacturaVentaDao oNumDao = new tblNumeracionFacturaVentaDao(cadenaConexion);
            return oNumDao.Guardar(Item);
        }

        public bool actualizar(long idEmpresa)
        {
            tblNumeracionFacturaVentaDao oNumDao = new tblNumeracionFacturaVentaDao(cadenaConexion);
            return oNumDao.Actualizar(idEmpresa);
        }

    }
}
