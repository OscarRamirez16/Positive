using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;
using InventarioDao;

namespace InventarioBusiness
{
    public class tblEmpresaBusiness
    {
        private string cadenaConexion;

        public tblEmpresaBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public List<tblEmpresaItem> ObtenerEmpresaLista()
        {
            try
            {
                tblEmpresaDao oEmpD = new tblEmpresaDao(cadenaConexion);
                return oEmpD.ObtenerEmpresaLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblEmpresaItem ObtenerEmpresa(long idEmpresa)
        {
            try
            {
                tblEmpresaDao oEmpD = new tblEmpresaDao(cadenaConexion);
                return oEmpD.ObtenerEmpresa(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Guardar(tblEmpresaItem empresa)
        {
            try
            {
                tblEmpresaDao oEmpD = new tblEmpresaDao(cadenaConexion);
                return oEmpD.Guardar(empresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
