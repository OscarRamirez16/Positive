using InventarioDao;
using InventarioItem;
using System;

namespace InventarioBusiness
{
    public class tblCuentaCobroBusiness
    {
        private string cadenaConexion;

        public tblCuentaCobroBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public enum EstadoCuentaCobroEnum
        {
            Pendiente = 1,
            Anulado = 2,
            Pagado = 3
        }

        public string Guardar(tblCuentaCobroItem Item)
        {
            try
            {
                tblCuentaCobroDao oCueD = new tblCuentaCobroDao(cadenaConexion);
                return oCueD.Insertar(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObtenerNumeroCuentaCobro(long IdEmpresa)
        {
            try
            {
                tblCuentaCobroDao oCueD = new tblCuentaCobroDao(cadenaConexion);
                return oCueD.ObtenerNumeroCuentaCobro(IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
