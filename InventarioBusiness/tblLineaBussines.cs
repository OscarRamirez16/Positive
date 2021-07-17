using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;
using InventarioDao;
using System.Data;

namespace InventarioBusiness
{
    public class tblLineaBussines
    {

        private string cadenaConexion;

        public tblLineaBussines(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public DataTable ObtenerLineaPorDescipcion(string Nombre, long IdEmpresa)
        {
            try
            {
                tblLineaDao oLinD = new tblLineaDao(cadenaConexion);
                return oLinD.ObtenerLineaPorDescipcion(Nombre, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public tblLineaItem ObtenerLinea(long IdLinea, long IdEmpresa)
        {
            try
            {
                tblLineaDao oLinD = new tblLineaDao(cadenaConexion);
                return oLinD.ObtenerLinea(IdLinea, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Guardar(tblLineaItem linea)
        {
            try
            {
                tblLineaDao oBodD = new tblLineaDao(cadenaConexion);
                return oBodD.Guardar(linea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblLineaItem> ObtenerLineaLista(long idEmpresa)
        {
            try
            {
                tblLineaDao oLineaD = new tblLineaDao(cadenaConexion);
                return oLineaD.ObtenerLineaLista(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
