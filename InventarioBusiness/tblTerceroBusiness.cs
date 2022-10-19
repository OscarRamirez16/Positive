using System;
using System.Collections.Generic;
using InventarioItem;
using InventarioDao;
using System.Data;
using System.IO;
using System.Text;

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
        public DataTable ObtenerTipoIdentificacionDIAN()
        {
            try
            {
                tblTerceroDao oTerD = new tblTerceroDao(cadenaConexion);
                return oTerD.ObtenerTipoIdentificacionDIAN();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerTipoContribuyente()
        {
            try
            {
                tblTerceroDao oTerD = new tblTerceroDao(cadenaConexion);
                return oTerD.ObtenerTipoContribuyente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerRegimenFiscal()
        {
            try
            {
                tblTerceroDao oTerD = new tblTerceroDao(cadenaConexion);
                return oTerD.ObtenerRegimenFiscal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerResponsabilidadFiscal()
        {
            try
            {
                tblTerceroDao oTerD = new tblTerceroDao(cadenaConexion);
                return oTerD.ObtenerResponsabilidadFiscal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        private enum PlantillaColumnasEnum { 
            TipoTercero =0,
            TipoIdentificacion = 1,
            Identificacion = 2,
            Grupo = 3,
            Nombre = 4,
            Direccion = 5,
            Telefonos = 6,
            Mail = 7,
            Ciudad = 8
        }

        public string LeerTercerosArchivo(Stream fileStream, List<tblTerceroItem> terceros, long idEmpresa, char Delimitador)
        {
            try
            {
                string Error = "";
                using (StreamReader sr = new StreamReader(fileStream, Encoding.UTF7))
                {
                    string linea = null;
                    bool PrimeraLinea = true;
                    bool blnError = false;
                    int IdLinea = 2;
                    while (((linea = sr.ReadLine()) != null) && !blnError)
                    {
                        if (PrimeraLinea)
                        {
                            if (linea.Split(Delimitador).Length < 9)
                            {
                                blnError = true;
                                Error = "El numero de columnas del archivo no es valido...";
                            }
                            PrimeraLinea = false;
                        }
                        else
                        {
                            if (linea.Split(Delimitador)[PlantillaColumnasEnum.TipoTercero.GetHashCode()] != "" && linea.Split(Delimitador)[PlantillaColumnasEnum.Nombre.GetHashCode()] != "")
                            {
                                tblTerceroItem oTItem = new tblTerceroItem();
                                oTItem.TipoTercero = linea.Split(Delimitador)[PlantillaColumnasEnum.TipoTercero.GetHashCode()];
                                oTItem.idTipoIdentificacion = linea.Split(Delimitador)[PlantillaColumnasEnum.TipoIdentificacion.GetHashCode()];
                                oTItem.Identificacion = linea.Split(Delimitador)[PlantillaColumnasEnum.Identificacion.GetHashCode()];
                                oTItem.idGrupoCliente = long.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.Grupo.GetHashCode()]);
                                oTItem.Nombre = linea.Split(Delimitador)[PlantillaColumnasEnum.Nombre.GetHashCode()];
                                oTItem.Direccion = linea.Split(Delimitador)[PlantillaColumnasEnum.Direccion.GetHashCode()];
                                oTItem.Telefono = linea.Split(Delimitador)[PlantillaColumnasEnum.Telefonos.GetHashCode()];
                                oTItem.Mail = linea.Split(Delimitador)[PlantillaColumnasEnum.Mail.GetHashCode()];
                                oTItem.idCiudad = short.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.Ciudad.GetHashCode()]);
                                oTItem.idEmpresa = idEmpresa;
                                terceros.Add(oTItem);
                                IdLinea++;
                            }
                            else
                            {
                                blnError = true;
                            }
                        }
                    }
                }
                return Error;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
