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
    public class tblOrdenFabricacionBiz
    {
        private string CadenaConexion;

        public enum EstadoOrdenFabricacion
        {
            Abierta = 1,
            Fabricando = 2,
            Terminada = 3,
            Cancelada = 4
        }

        public tblOrdenFabricacionBiz(string CadenaConexion)
        {
            this.CadenaConexion = CadenaConexion;
        }

        public string CambiarEstadoOF(long IdOF, short IdEstado)
        {
            try
            {
                tblOrdenFabricacionDao oOrdD = new tblOrdenFabricacionDao(CadenaConexion);
                return oOrdD.CambiarEstadoOF(IdOF, IdEstado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblOrdenFabricacionDetalleItem> ObtenerOrdenFabricacionDetallePorID(long IdOrden)
        {
            try
            {
                tblOrdenFabricacionDao oOrdD = new tblOrdenFabricacionDao(CadenaConexion);
                return oOrdD.ObtenerOrdenFabricacionDetalleLista(IdOrden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblOrdenFabricacionItem> ObtenerOrdenesFabricacionPorFiltros(DateTime FechaInicial, DateTime FechaFinal, string IdOrden, long IdUsuario)
        {
            try
            {
                tblOrdenFabricacionDao oOrdD = new tblOrdenFabricacionDao(CadenaConexion);
                return oOrdD.ObtenerOrdenesFabricacionPorFiltros(FechaInicial, FechaFinal, IdOrden, IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblOrdenFabricacionItem ObtenerOrdenFabricacion(long IdOrden)
        {
            try
            {
                tblOrdenFabricacionDao oOrdD = new tblOrdenFabricacionDao(CadenaConexion);
                return oOrdD.ObtenerOrdenFabricacion(IdOrden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Guardar(tblOrdenFabricacionItem Item)
        {
            try
            {
                tblOrdenFabricacionDao oOrdD = new tblOrdenFabricacionDao(CadenaConexion);
                return oOrdD.Guardar(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerEstadosOrdenFabricacion()
        {
            try
            {
                tblOrdenFabricacionDao oOrdD = new tblOrdenFabricacionDao(CadenaConexion);
                return oOrdD.ObtenerEstadosOrdenFabricacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
