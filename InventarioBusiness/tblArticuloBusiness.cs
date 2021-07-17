using System;
using System.Collections.Generic;
using System.Text;
using InventarioItem;
using InventarioDao;
using System.IO;
using System.Data;

namespace InventarioBusiness
{
    public class tblArticuloBusiness
    {

        private string cadenaConexion;

        public tblArticuloBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        enum dgArticulosColumnsEnum
        {
            CodigoArticulo = 0,
            IdBodega = 1,
            Nombre = 2,
            Precio = 3,
            Costo = 4,
            Marca = 5,
            Ubicacion = 6
        }
        public enum TipoCampanaArticuloEnum { 
            Todos = 0,
            GrupoArticulo = 1,
            Articulo = 2
        }
        public enum PlantillaEntradaSalidaMasivaMercanciaEnum
        {
            CodigoArticulo = 0,
            Cantidad = 1,
            IdBodega = 2
        }
        public enum TipoCampanaClienteEnum
        {
            Todos = 0,
            GrupoCliente = 1,
            Cliente = 2
        }
        public enum TipoManejoPrecio
        {
            Valor = 1,
            Porcentaje = 2
        }
        public enum PlantillaEntradaMasivaMercanciaEnum
        {
            CodigoArticulo = 0,
            IdBodega = 1,
            Costo = 2,
            TipoManejoPrecio = 3,
            Precio = 4,
            Existencias = 5,
        }

        public enum PlantillaColumnasEnum
        {
            CodigoArticulo = 0,
            Descripcion = 1,
            Presentacion = 2,
            IdLinea = 3,
            IVAVenta = 4,
            CodigoBarra = 5,
            IdTercero = 6,
            IdBodega = 7,
            EsInventario = 8,
            StockMinimo = 9,
            IVACompra = 10,
            Ubicacion = 11,
            EsCompuesto = 12,
            EsArticuloFinal = 13,
            EsHijo = 14,
            IdPadre = 15,
            CantidadPadre = 16,
            Costo = 17,
            Precio = 18
        }
        public string ActualizarDatosArticulosMasivo(List<tblArticuloItem> oList)
        {
            try
            {
                tblArticuloDao oArtD = new tblArticuloDao(cadenaConexion);
                return oArtD.ActualizarDatosArticulosMasivo(oList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CreacionMasivaArticulos(List<tblArticuloItem> oListArt)
        {
            try
            {
                tblArticuloDao oArtD = new tblArticuloDao(cadenaConexion);
                return oArtD.CreacionMasivaArticulos(oListArt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal ValidarDisponibilidadDV(long IdArticulo, long IdBodega, decimal Cantidad, string Referencia)
        {
            try
            {
                tblArticuloDao oArtD = new tblArticuloDao(cadenaConexion);
                return oArtD.ValidarDisponibilidadDV(IdArticulo, IdBodega, Cantidad, Referencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerArticuloListaPorNombreCodigo(string Busqueda, long IdEmpresa)
        {
            try
            {
                tblArticuloDao oArtD = new tblArticuloDao(cadenaConexion);
                return oArtD.ObtenerArticuloListaPorNombreCodigo(Busqueda, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ArticuloObtenerUltimoCodigo(long IdEmpresa) {
            tblArticuloDao oArtD = new tblArticuloDao(cadenaConexion);
            return oArtD.ArticuloObtenerUltimoCodigo(IdEmpresa);
        }

        public DataTable ObtenerHistoricoMovimientosArticulo(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa, string CodigoArticulo)
        {
            try
            {
                tblArticuloDao oArtD = new tblArticuloDao(cadenaConexion);
                return oArtD.ObtenerHistoricoMovimientosArticulo(FechaInicial, FechaFinal, IdEmpresa, CodigoArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerAuditoriaStock(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa, string CodigoArticulo, long IdBodega)
        {
            try
            {
                tblArticuloDao oArtD = new tblArticuloDao(cadenaConexion);
                return oArtD.ObtenerAuditoriaStock(FechaInicial, FechaFinal, IdEmpresa, CodigoArticulo, IdBodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerMovimientosAritculos(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa, string CodigoArticulo, string NitTercero)
        {
            try
            {
                tblArticuloDao oArtD = new tblArticuloDao(cadenaConexion);
                return oArtD.ObtenerMovimientosAritculos(FechaInicial, FechaFinal, IdEmpresa, CodigoArticulo, NitTercero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal DisponibilidadArticuloEnBodega(long IdArticulo, long IdBodega)
        {
            try
            {
                tblArticuloDao oArtD = new tblArticuloDao(cadenaConexion);
                return oArtD.DisponibilidadArticuloEnBodega(IdArticulo, IdBodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GuardarCodigosBarra(tblArticuloCodigoBarraItem Item)
        {
            try
            {
                tblArticuloCodigoBarraDao oArtD = new tblArticuloCodigoBarraDao(cadenaConexion);
                return oArtD.Guardar(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblArticuloCodigoBarraItem> ObtenerCodigosBarra(long IdArticulo)
        {
            try
            {
                tblArticuloCodigoBarraDao oArtD = new tblArticuloCodigoBarraDao(cadenaConexion);
                return oArtD.ObtenerArticuloCodigoBarraLista(IdArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool TrasladarMercancia(long IdArticulo, long IdBodegaOrigen, long IdBodegaDestino, decimal Cantidad)
        {
            try
            {
                tblArticuloDao oArtD = new tblArticuloDao(cadenaConexion);
                return oArtD.TrasladarMercancia(IdArticulo, IdBodegaOrigen, IdBodegaDestino, Cantidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GuardarArticulosPorBodegaExistencia(List<tblArticulo_BodegaItem> oListArtBod)
        {
            try
            {
                tblArticulo_BodegaDao oArtBodD = new tblArticulo_BodegaDao(cadenaConexion);
                return oArtBodD.GuardarArticulosPorBodegaExistencia(oListArtBod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LeerArticulosArchivo(Stream fileStream, List<tblArticuloItem> Articulos, long idEmpresa, char Delimitador)
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
                            if (linea.Split(Delimitador)[PlantillaColumnasEnum.CodigoArticulo.GetHashCode()] != "" && linea.Split(Delimitador)[PlantillaColumnasEnum.Descripcion.GetHashCode()] != "")
                            {
                                tblArticuloItem oAItem = new tblArticuloItem();
                                oAItem.CodigoArticulo = linea.Split(Delimitador)[PlantillaColumnasEnum.CodigoArticulo.GetHashCode()];
                                oAItem.Nombre = linea.Split(Delimitador)[PlantillaColumnasEnum.Descripcion.GetHashCode()];
                                oAItem.Presentacion = linea.Split(Delimitador)[PlantillaColumnasEnum.Presentacion.GetHashCode()];
                                oAItem.IdLinea = long.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.IdLinea.GetHashCode()]);
                                oAItem.IVAVenta = decimal.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.IVAVenta.GetHashCode()], System.Globalization.NumberStyles.Currency);
                                oAItem.CodigoBarra = linea.Split(Delimitador)[PlantillaColumnasEnum.CodigoBarra.GetHashCode()];
                                oAItem.IdTercero = long.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.IdTercero.GetHashCode()]);
                                oAItem.IdEmpresa = idEmpresa;
                                oAItem.IdBodega = long.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.IdBodega.GetHashCode()]);
                                oAItem.EsInventario = linea.Split(Delimitador)[PlantillaColumnasEnum.EsInventario.GetHashCode()] == "1" ? true : false;
                                oAItem.StockMinimo = decimal.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.StockMinimo.GetHashCode()], System.Globalization.NumberStyles.Currency);
                                oAItem.Activo = true;
                                oAItem.IVACompra = decimal.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.IVACompra.GetHashCode()], System.Globalization.NumberStyles.Currency);
                                oAItem.Ubicacion = linea.Split(Delimitador)[PlantillaColumnasEnum.Ubicacion.GetHashCode()];
                                oAItem.EsCompuesto = linea.Split(Delimitador)[PlantillaColumnasEnum.EsCompuesto.GetHashCode()] == "1" ? true : false;
                                oAItem.EsArticuloFinal = linea.Split(Delimitador)[PlantillaColumnasEnum.EsArticuloFinal.GetHashCode()] == "1" ? true : false;
                                oAItem.EsHijo = linea.Split(Delimitador)[PlantillaColumnasEnum.EsHijo.GetHashCode()] == "1" ? true : false;
                                oAItem.IdArticuloPadre = long.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.IdPadre.GetHashCode()]);
                                oAItem.CantidadPadre = decimal.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.CantidadPadre.GetHashCode()], System.Globalization.NumberStyles.Currency);
                                oAItem.Costo = decimal.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.Costo.GetHashCode()], System.Globalization.NumberStyles.Currency);
                                oAItem.Precio = decimal.Parse(linea.Split(Delimitador)[PlantillaColumnasEnum.Precio.GetHashCode()], System.Globalization.NumberStyles.Currency);
                                Articulos.Add(oAItem);
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
        public string LeerArticulosParaEditarMasivo(Stream fileStream, List<tblArticuloItem> Articulos, char Delimitador, long IdEmpresa)
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
                            if (linea.Split(Delimitador).Length != 7)
                            {
                                blnError = true;
                                Error = "El numero de columnas del archivo no es valido...";
                            }
                            PrimeraLinea = false;
                        }
                        else
                        {
                            if (linea.Split(Delimitador)[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()] != "" && linea.Split(Delimitador)[PlantillaEntradaSalidaMasivaMercanciaEnum.CodigoArticulo.GetHashCode()] != "")
                            {
                                tblArticuloBusiness oArtB = new tblArticuloBusiness(cadenaConexion);
                                tblArticuloItem oArtI = new tblArticuloItem();
                                tblBodegaBusiness oBodB = new tblBodegaBusiness(cadenaConexion);
                                tblBodegaItem oBodI = new tblBodegaItem();
                                oArtI = oArtB.ObtenerArticuloPorCodigo(linea.Split(Delimitador)[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()], IdEmpresa);
                                if (oArtI.IdArticulo > 0)
                                {
                                    oBodI = oBodB.ObtenerBodega(long.Parse(linea.Split(Delimitador)[dgArticulosColumnsEnum.IdBodega.GetHashCode()]), IdEmpresa);
                                    if (oBodI.IdBodega > 0)
                                    {
                                        oArtI.IdBodega = oBodI.IdBodega;
                                        oArtI.Bodega = oBodI.Descripcion;
                                    }
                                    else
                                    {
                                        Error = string.Format("La bodega {0} no existe en el sistema. <br>", linea.Split(Delimitador)[dgArticulosColumnsEnum.IdBodega.GetHashCode()]);
                                    }
                                    if(!string.IsNullOrEmpty(linea.Split(Delimitador)[dgArticulosColumnsEnum.Nombre.GetHashCode()]))
                                    {
                                        oArtI.Nombre = linea.Split(Delimitador)[dgArticulosColumnsEnum.Nombre.GetHashCode()];
                                    }
                                    if (!string.IsNullOrEmpty(linea.Split(Delimitador)[dgArticulosColumnsEnum.Precio.GetHashCode()]))
                                    {
                                        oArtI.Precio = decimal.Parse(linea.Split(Delimitador)[dgArticulosColumnsEnum.Precio.GetHashCode()], System.Globalization.NumberStyles.Currency);
                                    }
                                    if (!string.IsNullOrEmpty(linea.Split(Delimitador)[dgArticulosColumnsEnum.Costo.GetHashCode()]))
                                    {
                                        oArtI.Costo = decimal.Parse(linea.Split(Delimitador)[dgArticulosColumnsEnum.Costo.GetHashCode()], System.Globalization.NumberStyles.Currency);
                                    }
                                    if (!string.IsNullOrEmpty(linea.Split(Delimitador)[dgArticulosColumnsEnum.Ubicacion.GetHashCode()]))
                                    {
                                        oArtI.Ubicacion = linea.Split(Delimitador)[dgArticulosColumnsEnum.Ubicacion.GetHashCode()];
                                    }
                                    if (!string.IsNullOrEmpty(linea.Split(Delimitador)[dgArticulosColumnsEnum.Marca.GetHashCode()]))
                                    {
                                        oArtI.Marca = linea.Split(Delimitador)[dgArticulosColumnsEnum.Marca.GetHashCode()];
                                    }
                                    if (string.IsNullOrEmpty(Error))
                                    {
                                        Articulos.Add(oArtI);
                                    }
                                }
                                else
                                {
                                    Error = string.Format("El código {0} no existe en el sistema. <br>", linea.Split(Delimitador)[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()]);
                                }
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
        public string LeerArticulosParaEntradaSalidaMasiva(Stream fileStream, List<tblArticuloItem> Articulos, char Delimitador, long IdEmpresa)
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
                            if (linea.Split(Delimitador).Length != 3)
                            {
                                blnError = true;
                                Error = "El numero de columnas del archivo no es valido...";
                            }
                            PrimeraLinea = false;
                        }
                        else
                        {
                            if (linea.Split(Delimitador)[PlantillaEntradaSalidaMasivaMercanciaEnum.CodigoArticulo.GetHashCode()] != "" && linea.Split(Delimitador)[PlantillaEntradaSalidaMasivaMercanciaEnum.CodigoArticulo.GetHashCode()] != "")
                            {
                                tblArticuloBusiness oArtB = new tblArticuloBusiness(cadenaConexion);
                                tblArticuloItem oArtI = new tblArticuloItem();
                                tblBodegaBusiness oBodB = new tblBodegaBusiness(cadenaConexion);
                                tblBodegaItem oBodI = new tblBodegaItem();
                                oArtI = oArtB.ObtenerArticuloPorCodigo(linea.Split(Delimitador)[PlantillaEntradaSalidaMasivaMercanciaEnum.CodigoArticulo.GetHashCode()], IdEmpresa);
                                if (oArtI.IdArticulo > 0)
                                {
                                    oBodI = oBodB.ObtenerBodega(long.Parse(linea.Split(Delimitador)[PlantillaEntradaSalidaMasivaMercanciaEnum.IdBodega.GetHashCode()]), IdEmpresa);
                                    if (oBodI.IdBodega > 0)
                                    {
                                        oArtI.IdBodega = oBodI.IdBodega;
                                        oArtI.Bodega = oBodI.Descripcion;
                                    }
                                    else
                                    {
                                        Error = string.Format("La bodega {0} no existe en el sistema. <br>", linea.Split(Delimitador)[PlantillaEntradaSalidaMasivaMercanciaEnum.IdBodega.GetHashCode()]);
                                    }
                                    oArtI.Cantidad = decimal.Parse(linea.Split(Delimitador)[PlantillaEntradaSalidaMasivaMercanciaEnum.Cantidad.GetHashCode()]);
                                    oArtI.Costo = oBodB.ConsultarArticulosBodegaPorID(oArtI.IdArticulo, oArtI.IdBodega).Costo;
                                    if (string.IsNullOrEmpty(Error))
                                    {
                                        Articulos.Add(oArtI);
                                    }
                                }
                                else
                                {
                                    Error = string.Format("El código {0} no existe en el sistema. <br>", linea.Split(Delimitador)[PlantillaEntradaSalidaMasivaMercanciaEnum.CodigoArticulo.GetHashCode()]);
                                }
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
        public string LeerArticulosParaEntradaMasiva(Stream fileStream, List<tblArticuloItem> articulos, long idEmpresa, char Delimitador)
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
                            if (linea.Split(Delimitador).Length < 6)
                            {
                                blnError = true;
                                Error = "El numero de columnas del archivo no es valido...";
                            }
                            PrimeraLinea = false;
                        }
                        else
                        {
                            if (linea.Split(Delimitador)[PlantillaColumnasEnum.CodigoArticulo.GetHashCode()] != "" && linea.Split(Delimitador)[PlantillaColumnasEnum.Descripcion.GetHashCode()] != "")
                            {
                                tblArticuloItem oAItem = new tblArticuloItem();
                                oAItem.CodigoArticulo = linea.Split(Delimitador)[PlantillaEntradaMasivaMercanciaEnum.CodigoArticulo.GetHashCode()];
                                oAItem.IdBodega = long.Parse(linea.Split(Delimitador)[PlantillaEntradaMasivaMercanciaEnum.IdBodega.GetHashCode()]);
                                oAItem.Costo = decimal.Parse(linea.Split(Delimitador)[PlantillaEntradaMasivaMercanciaEnum.Costo.GetHashCode()]);
                                if (linea.Split(Delimitador)[PlantillaEntradaMasivaMercanciaEnum.TipoManejoPrecio.GetHashCode()] == "Porcentaje")
                                {
                                    oAItem.IdTipoManejoPrecio = short.Parse(TipoManejoPrecio.Porcentaje.GetHashCode().ToString());
                                }
                                else if (linea.Split(Delimitador)[PlantillaEntradaMasivaMercanciaEnum.TipoManejoPrecio.GetHashCode()] == "Valor")
                                {
                                    oAItem.IdTipoManejoPrecio = short.Parse(TipoManejoPrecio.Valor.GetHashCode().ToString());
                                }
                                else
                                {
                                    Error = string.Format("{0} Tipo manejo precio no valido, por favor digitir una opción valida.<br>");
                                }
                                oAItem.Precio = decimal.Parse(linea.Split(Delimitador)[PlantillaEntradaMasivaMercanciaEnum.Precio.GetHashCode()]);
                                oAItem.Cantidad = decimal.Parse(linea.Split(Delimitador)[PlantillaEntradaMasivaMercanciaEnum.Existencias.GetHashCode()]);
                                if (string.IsNullOrEmpty(Error))
                                {
                                    articulos.Add(oAItem);
                                }
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

        public bool insertar(tblArticuloItem articulo)
        {
            try
            {
                tblArticuloDao oArticuloD = new tblArticuloDao(cadenaConexion);
                return oArticuloD.Guardar(articulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblArticuloItem> ObtenerArticuloListaPorFiltrosNombreLineaProveedorEmpresa(string nombre, short idLinea, long idProveedor, long idEmpresa)
        {
            try
            {
                tblArticuloDao oArticuloD = new tblArticuloDao(cadenaConexion);
                return oArticuloD.ObtenerArticuloListaPorFiltrosNombreLineaProveedorEmpresa(nombre, idLinea, idProveedor, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerArticulosPorBodega(long idEmpresa, long idBodega, string nombre, short idLinea, long idProveedor)
        {
            try
            {
                tblArticuloDao oArticuloD = new tblArticuloDao(cadenaConexion);
                return oArticuloD.ObtenerArticulosPorBodega(idEmpresa, idBodega, nombre, idLinea, idProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblArticuloItem ObtenerArticuloPorID(long idArticulo, long idEmpresa)
        {
            try
            {
                tblArticuloDao oArticuloD = new tblArticuloDao(cadenaConexion);
                return oArticuloD.ObtenerArticuloPorID(idArticulo, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblArticuloItem ObtenerArticuloPorCodigo(string Codigo, long idEmpresa)
        {
            try
            {
                tblArticuloDao oArticuloD = new tblArticuloDao(cadenaConexion);
                return oArticuloD.ObtenerArticuloPorCodigo(Codigo, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblArticuloItem ObtenerArticulo(long idArticulo, long tipoDocumento)
        {
            try
            {
                tblArticuloDao oArticuloD = new tblArticuloDao(cadenaConexion);
                return oArticuloD.ObtenerArticulo(idArticulo, tipoDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<JSONItem> ObtenerArticuloListaPorNombreSencillo(string nombre, long idEmpresa, int Tipo)
        {
            try
            {
                tblArticuloDao oArticuloD = new tblArticuloDao(cadenaConexion);
                return oArticuloD.ObtenerArticuloListaPorNombreSencillo(nombre, idEmpresa, Tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<JSONItem> ObtenerArticuloListaPorNombre(string nombre, long idEmpresa, long tipoDocumento, long idBodega, long idTercero)
        {
            try
            {
                tblArticuloDao oArticuloD = new tblArticuloDao(cadenaConexion);
                return oArticuloD.ObtenerArticuloListaPorNombre(nombre, idEmpresa, tipoDocumento, idBodega, idTercero);
            }
            catch(Exception ex)
            {
                throw ex;
            }   
        }

        public List<JSONItem> ObtenerArticuloListaPorCodigoOCodigoBarras(string codigo, long idEmpresa, long tipoDocumento, long idBodega, long idTercero)
        {
            try
            {
                tblArticuloDao oArticuloD = new tblArticuloDao(cadenaConexion);
                return oArticuloD.ObtenerArticuloListaPorCodigoOCodigoBarras(codigo, idEmpresa, tipoDocumento, idBodega, idTercero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region "Campañas"
        public tblCampanaItem ObtenerCampana(long Id,long idEmpresa) {
            tblCampanaDao oCDao = new tblCampanaDao(cadenaConexion);
            return oCDao.ObtenerCampana(Id,idEmpresa);
        }
        public DataTable ObtenerCampanaLista(string IdCampana, string Nombre, int Estado, string IdArticulo, long idEmpresa)
        {
            tblCampanaDao oCDao = new tblCampanaDao(cadenaConexion);
            return oCDao.ObtenerCampanaLista(IdCampana, Nombre, Estado, IdArticulo, idEmpresa);
        }
        public bool Guardar(tblCampanaItem Item) {
            tblCampanaDao oCDao = new tblCampanaDao(cadenaConexion);
            return oCDao.Guardar(Item);
        }
        public tblCampanaClienteItem ObtenerCampanaCliente(long Id, long idEmpresa) {
            tblCampanaClienteDao oCCDao = new tblCampanaClienteDao(cadenaConexion);
            return oCCDao.ObtenerCampanaCliente(Id,idEmpresa);
        }
        public List<tblCampanaClienteItem> ObtenerCampanaClienteLista(long idCampana, long idEmpresa) {
            tblCampanaClienteDao oCCDao = new tblCampanaClienteDao(cadenaConexion);
            return oCCDao.ObtenerCampanaClienteLista(idCampana,idEmpresa);
        }
        public bool Guardar(tblCampanaClienteItem Item) {
            tblCampanaClienteDao oCCDao = new tblCampanaClienteDao(cadenaConexion);
            return oCCDao.Guardar(Item);
        }
        public tblCampanaArticuloItem ObtenerCampanaArticulo(long Id, long idEmpresa)
        {
            tblCampanaArticuloDao oCADao = new tblCampanaArticuloDao(cadenaConexion);
            return oCADao.ObtenerCampanaArticulo(Id, idEmpresa);
        }
        public List<tblCampanaArticuloItem> ObtenerCampanaArticuloLista(long idCampana, long idEmpresa)
        {
            tblCampanaArticuloDao oCADao = new tblCampanaArticuloDao(cadenaConexion);
            return oCADao.ObtenerCampanaArticuloLista(idCampana,idEmpresa);
        }
        public bool Guardar(tblCampanaArticuloItem Item) {
            tblCampanaArticuloDao oCADao = new tblCampanaArticuloDao(cadenaConexion);
            return oCADao.Guardar(Item);
        }
        #endregion

    }
}
