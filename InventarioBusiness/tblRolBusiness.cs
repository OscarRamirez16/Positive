using System;
using System.Collections.Generic;
using InventarioDao;
using InventarioItem;

namespace InventarioBusiness
{
    public class tblRolBusiness
    {

        private string cadenaConexion;

        public tblRolBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public List<tblRolItem> ObtenerRolLista(long idEmpresa)
        {
            tblRolDao oRolD = new tblRolDao(cadenaConexion);
            return oRolD.ObtenerRolLista(idEmpresa);
        }

        public long insertar(tblRolItem rol)
        {
            try
            {
                tblRolDao oRolD = new tblRolDao(cadenaConexion);
                return oRolD.Guardar(rol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<bsq_ObtenerRolLista> ObtenerRolListaNombreEmpresa(long idEmpresa)
        {
            try
            {
                tblRolDao oRolD = new tblRolDao(cadenaConexion);
                return oRolD.ObtenerRolListaNombreEmpresa(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblRolItem ObtenerRol(long idRol)
        {
            try
            {
                tblRolDao oRolD = new tblRolDao(cadenaConexion);
                return oRolD.ObtenerRol(idRol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
