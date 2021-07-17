using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;
using InventarioDao;

namespace InventarioBusiness
{
    public class tblCajaBussines
    {

        private string cadenaConexion;

        public tblCajaBussines(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
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
    }
}
