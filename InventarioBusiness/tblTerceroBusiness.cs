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
        public string CreacionMasivaTerceros(List<tblTerceroItem> oListTer)
        {
            try
            {
                tblTerceroDao oTerD = new tblTerceroDao(cadenaConexion);
                return oTerD.CreacionMasivaTerceros(oListTer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            TipoIdentificacion = 0,
            Identificacion = 1,
            Nombre = 2,
            Telefono = 3,
            Celular = 4,
            Mail = 5,
            Direccion = 6,
            Ciudad = 7,
            TipoTercero = 8,
            FechaNacimiento = 9,
            ListaPrecio = 10,
            GrupoCliente = 11,
            Observaciones = 12,
            TipoIdentificacionDIAN = 13,
            MatriculaMercantil = 14,
            TipoContribuyente = 15,
            RegimenFiscal = 16,
            ResponsabilidadFiscal = 17,
            CodigoZip = 18
        }

        public string LeerTercerosArchivo(Stream fileStream, List<tblTerceroItem> terceros, char Delimitador)
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
                            if (linea.Split(Delimitador).Length < 19)
                            {
                                blnError = true;
                                Error = "El numero de columnas del archivo no es valido...";
                            }
                            PrimeraLinea = false;
                        }
                        else
                        {
                            if (linea.Split(Delimitador)[PlantillaColumnasEnum.TipoIdentificacion.GetHashCode()] != "" && linea.Split(Delimitador)[PlantillaColumnasEnum.TipoTercero.GetHashCode()] != "" && linea.Split(Delimitador)[PlantillaColumnasEnum.Nombre.GetHashCode()] != "")
                            {
                                tblTerceroItem oTItem = new tblTerceroItem();
                                oTItem.idTipoIdentificacion = short.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.TipoIdentificacion.GetHashCode()] == "NIT" ? "1" : "2");
                                oTItem.Identificacion = linea.Split(Delimitador)[PlantillaColumnasEnum.Identificacion.GetHashCode()];
                                oTItem.Nombre = linea.Split(Delimitador)[PlantillaColumnasEnum.Nombre.GetHashCode()];
                                oTItem.Telefono = linea.Split(Delimitador)[PlantillaColumnasEnum.Telefono.GetHashCode()];
                                oTItem.Celular = linea.Split(Delimitador)[PlantillaColumnasEnum.Celular.GetHashCode()];
                                oTItem.Mail = linea.Split(Delimitador)[PlantillaColumnasEnum.Mail.GetHashCode()];
                                oTItem.Direccion = linea.Split(Delimitador)[PlantillaColumnasEnum.Direccion.GetHashCode()];
                                oTItem.idCiudad = short.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.Ciudad.GetHashCode()]);
                                oTItem.TipoTercero = linea.Split(Delimitador)[PlantillaColumnasEnum.TipoTercero.GetHashCode()];
                                if(linea.Split(Delimitador)[PlantillaColumnasEnum.FechaNacimiento.GetHashCode()] != "")
                                {
                                    oTItem.FechaNacimiento = DateTime.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.FechaNacimiento.GetHashCode()]);
                                }
                                if (linea.Split(Delimitador)[PlantillaColumnasEnum.ListaPrecio.GetHashCode()] != "")
                                {
                                    oTItem.IdListaPrecio = long.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.ListaPrecio.GetHashCode()]);
                                }
                                if (linea.Split(Delimitador)[PlantillaColumnasEnum.GrupoCliente.GetHashCode()] != "")
                                {
                                    oTItem.idGrupoCliente = long.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.GrupoCliente.GetHashCode()]);
                                }
                                oTItem.Observaciones = linea.Split(Delimitador)[PlantillaColumnasEnum.Observaciones.GetHashCode()];
                                oTItem.TipoIdentificacionDIAN = linea.Split(Delimitador)[PlantillaColumnasEnum.TipoIdentificacionDIAN.GetHashCode()];
                                oTItem.MatriculaMercantil = linea.Split(Delimitador)[PlantillaColumnasEnum.MatriculaMercantil.GetHashCode()];
                                oTItem.TipoContribuyente = linea.Split(Delimitador)[PlantillaColumnasEnum.TipoContribuyente.GetHashCode()];
                                oTItem.RegimenFiscal = linea.Split(Delimitador)[PlantillaColumnasEnum.RegimenFiscal.GetHashCode()];
                                oTItem.ResponsabilidadFiscal = linea.Split(Delimitador)[PlantillaColumnasEnum.ResponsabilidadFiscal.GetHashCode()];
                                oTItem.CodigoZip = linea.Split(Delimitador)[PlantillaColumnasEnum.CodigoZip.GetHashCode()];
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
