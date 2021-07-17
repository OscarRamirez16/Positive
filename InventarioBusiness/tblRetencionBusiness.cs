using InventarioDao;
using InventarioItem;
using System;
using System.Data;

namespace InventarioBusiness
{
    public class tblRetencionBusiness
    {
        private string CadenaConexion;
        public tblRetencionBusiness(string cadenaConexion)
        {
            this.CadenaConexion = cadenaConexion;
        }
        public tblRetencionItem ObtenerRetencionPorID(long Id)
        {
            try
            {
                tblRetencionDao oRetD = new tblRetencionDao(CadenaConexion);
                return oRetD.ObtenerRetencionPorID(Id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerRetencionesTodas(long IdEmpresa)
        {
            try
            {
                tblRetencionDao oRetD = new tblRetencionDao(CadenaConexion);
                return oRetD.ObtenerRetencionesTodas(IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerRetencionesActivas(long IdEmpresa)
        {
            try
            {
                tblRetencionDao oRetD = new tblRetencionDao(CadenaConexion);
                return oRetD.ObtenerRetencionesActivas(IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Insertar(tblRetencionItem Item)
        {
            try
            {
                tblRetencionDao oRetD = new tblRetencionDao(CadenaConexion);
                return oRetD.Guardar(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
