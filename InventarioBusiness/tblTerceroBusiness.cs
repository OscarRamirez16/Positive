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

    public class tblTerceroBusiness
    {

        private string cadenaConexion;

        public enum TipoIdentificacion
        {
            Nit = 1,
            Cedula = 2
        }

        public tblTerceroBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public tblTerceroItem ObtenerClienteProveedorGenerico(long IdEmpresa, string Tipo)
        {
            try
            {
                tblTerceroDao oTerceroD = new tblTerceroDao(cadenaConexion);
                return oTerceroD.ObtenerClienteProveedorGenerico(IdEmpresa, Tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerClientesListaActivos(long IdEmpresa, int GrupoCliente)
        {
            try
            {
                tblTerceroDao oTerceroD = new tblTerceroDao(cadenaConexion);
                return oTerceroD.ObtenerClientesListaActivos(IdEmpresa, GrupoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblTerceroItem> ObtenerTerceroListaPorTipo(long IdEmpresa, string Tipo)
        {
            try
            {
                tblTerceroDao oTerceroD = new tblTerceroDao(cadenaConexion);
                return oTerceroD.ObtenerTerceroListaPorTipo(IdEmpresa, Tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblTerceroItem ObtenerTerceroPorId(long IdTercero, long IdEmpresa)
        {
            try
            {
                tblTerceroDao oTerceroD = new tblTerceroDao(cadenaConexion);
                return oTerceroD.ObtenerTerceroPorId(IdTercero, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblTerceroItem> ObtenerTerceroLista(long IdEmpresa, string TipoTercero, int GrupoCliente)
        {
            try
            {
                tblTerceroDao oTerceroD = new tblTerceroDao(cadenaConexion);
                return oTerceroD.ObtenerTerceroLista(IdEmpresa, TipoTercero, GrupoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string insertar(tblTerceroItem cliente)
        {
            try
            {
                tblTerceroDao oTerceroD = new tblTerceroDao(cadenaConexion);
                return oTerceroD.Guardar(cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblTerceroItem> ObtenerTerceroListaPorFiltrosNombreCiudadEmpresa(long idTercero, string cedula, long idEmpresa, string tipoTercero)
        {
            try
            {
                tblTerceroDao oTerceroD = new tblTerceroDao(cadenaConexion);
                return oTerceroD.ObtenerTerceroListaPorFiltrosNombreCiudadEmpresa(idTercero, cedula, idEmpresa, tipoTercero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblTerceroItem ObtenerTercero(long IdTercero, long IdEmpresa)
        {
            try
            {
                tblTerceroDao oTerceroD = new tblTerceroDao(cadenaConexion);
                tblTerceroItem Item = new tblTerceroItem();
                Item = oTerceroD.ObtenerTercero(IdTercero, IdEmpresa);
                Item.Retenciones = oTerceroD.ObtenerRetencionesPorIdTercero(Item.IdTercero);
                return Item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblTerceroItem ObtenerTerceroPorIdentificacion(string identificacion, long idEmpresa)
        {
            try
            {
                tblTerceroDao oTerceroD = new tblTerceroDao(cadenaConexion);
                return oTerceroD.ObtenerTerceroPorIdentificacion(identificacion, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<JSONItem> ObtenerTerceroListaPorNombre(string nombre, string tipoTercero, long idEmpresa)
        {
            try
            {
                tblTerceroDao oTerceroD = new tblTerceroDao(cadenaConexion);
                return oTerceroD.ObtenerTerceroListaPorNombre(nombre, tipoTercero, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblGrupoClienteItem ObtenerGrupoCliente(long Id, long idEmpresa)
        {
            tblGrupoClienteDao oGCDao = new tblGrupoClienteDao(cadenaConexion);
            return oGCDao.ObtenerGrupoCliente(Id, idEmpresa);
        }

        public List<tblGrupoClienteItem> ObtenerGrupoClienteLista(long idEmpresa, string Texto)
        {
            tblGrupoClienteDao oGCDao = new tblGrupoClienteDao(cadenaConexion);
            return oGCDao.ObtenerGrupoClienteLista(idEmpresa,Texto);
        }

        public bool Guardar(tblGrupoClienteItem Item)
        {
            tblGrupoClienteDao oGCDao = new tblGrupoClienteDao(cadenaConexion);
            return oGCDao.Guardar(Item);
        }
    }
}
