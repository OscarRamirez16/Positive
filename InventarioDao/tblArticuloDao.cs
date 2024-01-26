using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventarioItem;
using System.Data;

namespace InventarioDao
{
    public class tblArticuloDao
    {
        private SqlConnection Conexion { get; set; }

        public tblArticuloDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public decimal ValidarDisponibilidadDV(long IdArticulo, long IdBodega, decimal Cantidad, string Referencia)
        {
            decimal Disponibles = 0;
            SqlCommand oSQL = new SqlCommand("spValidarDisponibilidadDV", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                oSQL.Parameters.Add(new SqlParameter("@IdBodega", IdBodega));
                oSQL.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                oSQL.Parameters.Add(new SqlParameter("@Referencia", Referencia));
                Disponibles = decimal.Parse(oSQL.ExecuteScalar().ToString());
            }
            catch
            {
                return Disponibles;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Disponibles;
        }

        public string ArticuloObtenerUltimoCodigo(long IdEmpresa)
        {
            string result = "";
            SqlCommand oSQL = new SqlCommand("spArticuloObtenerUltimoCodigo", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                result = oSQL.ExecuteScalar().ToString();
            }
            catch
            {
                return result;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return string.IsNullOrEmpty(result) ? "" : result;
        }

        public DataTable ObtenerArticuloListaPorNombreCodigo(string Busqueda, long IdEmpresa)
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerArticuloListaPorNombreCodigo", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@Busqueda", Busqueda));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
            try
            {
                Conexion.Open();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSQL);
                oSqlDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return dt;
        }

        public DataTable ObtenerHistoricoMovimientosArticulo(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa, string CodigoArticulo)
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerHistorialMovimientosArituclo", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
            oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
            if (string.IsNullOrEmpty(CodigoArticulo))
            {
                oSQL.Parameters.Add(new SqlParameter("@CodigoArticulo", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@CodigoArticulo", CodigoArticulo));
            }
            try
            {
                Conexion.Open();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSQL);
                oSqlDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return dt;
        }

        public DataTable ObtenerAuditoriaStock(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa, string CodigoArticulo, long IdBodega)
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerAuditoriaStock", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
            oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
            if (string.IsNullOrEmpty(CodigoArticulo))
            {
                oSQL.Parameters.Add(new SqlParameter("@CodigoArticulo", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@CodigoArticulo", CodigoArticulo));
            }
            if (IdBodega == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@IdBodega", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@IdBodega", IdBodega));
            }
            try
            {
                Conexion.Open();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSQL);
                oSqlDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return dt;
        }

        public DataTable ObtenerMovimientosAritculos(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa, string CodigoArticulo, string NitTercero)
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerMovimientosArticulos", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
            oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
            if (string.IsNullOrEmpty(CodigoArticulo))
            {
                oSQL.Parameters.Add(new SqlParameter("@CodigoArticulo", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@CodigoArticulo", CodigoArticulo));
            }
            if (string.IsNullOrEmpty(NitTercero))
            {
                oSQL.Parameters.Add(new SqlParameter("@IdProveedor", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@IdProveedor", NitTercero));
            }
            try
            {
                Conexion.Open();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSQL);
                oSqlDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return dt;
        }

        public decimal DisponibilidadArticuloEnBodega(long IdArticulo, long IdBodega)
        {
            decimal Disponibles = 0;
            SqlCommand oSQL = new SqlCommand("spObtenerExistenciasArticuloEnBodega", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                oSQL.Parameters.Add(new SqlParameter("@IdBodega", IdBodega));
                Disponibles = decimal.Parse(oSQL.ExecuteScalar().ToString());
            }
            catch
            {
                return Disponibles;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Disponibles;
        }

        public bool TrasladarMercancia(long IdArticulo, long IdBodegaOrigen, long IdBodegaDestino, decimal Cantidad)
        {
            SqlCommand oSQL = new SqlCommand("spTrasladarMercanciaBodega", Conexion);
            try
            {
                Conexion.Open();
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                oSQL.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                oSQL.Parameters.Add(new SqlParameter("@IdBodegaOrigen", IdBodegaOrigen));
                oSQL.Parameters.Add(new SqlParameter("@IdBodegaDestino", IdBodegaDestino));
                oSQL.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return true;
        }

        public tblArticuloItem ObtenerArticulo(long Id, long tipoDocumento)
        {
            tblArticuloItem Item = new tblArticuloItem();
            SqlCommand oSQL = new SqlCommand("spObtenerArticulo", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", Id));
                oSQL.Parameters.Add(new SqlParameter("@tipoDocumento", tipoDocumento));
                SqlDataReader reader = oSQL.ExecuteReader();
                if (reader.Read())
                {
                    Item = ObtenerItem(reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Item;
        }

        public tblArticuloItem ObtenerArticuloPorCodigo(string Codigo, long idEmpresa)
        {
            tblArticuloItem Item = new tblArticuloItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerArticuloPorCodigoEmpresa @Codigo,@idEmpresa", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                SqlDataReader reader = oSQL.ExecuteReader();
                if (reader.Read())
                {
                    Item = ObtenerItem(reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Item;
        }

        public tblArticuloItem ObtenerArticuloPorID(long Id, long idEmpresa)
        {
            tblArticuloItem Item = new tblArticuloItem();
            SqlCommand oSQL = new SqlCommand("spObtenerArticuloPorID", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", Id));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                SqlDataReader reader = oSQL.ExecuteReader();
                if (reader.Read())
                {
                    Item = ObtenerItem(reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Item;
        }

        public List<tblArticuloItem> ObtenerArticuloListaPorFiltrosNombreLineaProveedorEmpresa(string nombre, short idLinea, long idProveedor,long idEmpresa)
        {
            List<tblArticuloItem> Lista = new List<tblArticuloItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerArticuloListaPorFiltrosNombreLineaProveedorEmpresa @nombre,@idLinea,@Proveedor,@idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@nombre", nombre));
            oSQL.Parameters.Add(new SqlParameter("@idLinea", idLinea));
            oSQL.Parameters.Add(new SqlParameter("@Proveedor", idProveedor));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
            try
            {
                Conexion.Open();
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(ObtenerItem(reader));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Lista;
        }

        public DataTable ObtenerArticulosPorBodega(long idEmpresa, long idBodega, string nombre, short idLinea, long idProveedor)
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerArticuloListaPorBodega", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", idBodega));
            oSQL.Parameters.Add(new SqlParameter("@nombre", nombre));
            oSQL.Parameters.Add(new SqlParameter("@idLinea", idLinea));
            oSQL.Parameters.Add(new SqlParameter("@idProveedor", idProveedor));
            try
            {
                Conexion.Open();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSQL);
                oSqlDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return dt;
        }

        private tblArticuloItem ObtenerItem(SqlDataReader reader)
        {
            tblArticuloItem Item = new tblArticuloItem();
            Item.IdArticulo = long.Parse(reader["idArticulo"].ToString());
            Item.CodigoArticulo = reader["CodigoArticulo"].ToString();
            Item.Nombre = reader["Nombre"].ToString();
            Item.Presentacion = reader["Presentacion"].ToString();
            Item.IdLinea = long.Parse(reader["idLinea"].ToString());
            Item.Linea = reader["Linea"].ToString();
            Item.IVAVenta = decimal.Parse(reader["IVA"].ToString());
            Item.IVACompra = decimal.Parse(reader["IVACompra"].ToString());
            Item.CodigoBarra = reader["CodigoBarra"].ToString();
            Item.IdTercero = long.Parse(reader["idTercero"].ToString());
            Item.Tercero = reader["Tercero"].ToString();
            Item.IdEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.Empresa = reader["Empresa"].ToString();
            Item.IdBodega = long.Parse(reader["idBodega"].ToString());
            Item.Bodega = reader["Bodega"].ToString();
            Item.EsInventario = bool.Parse(reader["EsInventario"].ToString());
            Item.Cantidad = decimal.Parse(reader["Cantidad"].ToString());
            Item.StockMinimo = decimal.Parse(reader["StockMinimo"].ToString());
            Item.PrecioAutomatico = bool.Parse(reader["PrecioAutomatico"].ToString());
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.Ubicacion = reader["Ubicacion"].ToString();
            Item.CostoPonderado = decimal.Parse(reader["CostoPonderado"].ToString());
            Item.EsCompuesto = bool.Parse(reader["EsCompuesto"].ToString());
            Item.EsArticuloFinal = bool.Parse(reader["EsArticuloFinal"].ToString());
            Item.EsHijo = bool.Parse(reader["EsHijo"].ToString());
            Item.IdArticuloPadre = long.Parse(reader["IdArticuloPadre"].ToString());
            Item.NombrePadre = reader["NombrePadre"].ToString();
            Item.CantidadPadre = decimal.Parse(reader["CantidadPadre"].ToString());
            Item.Marca = reader["Marca"].ToString();
            Item.PorcentajeComision = decimal.Parse(reader["PorcentajeComision"].ToString());
            return Item;
        }

        private bool Insertar(tblArticuloItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarArticulo", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@CodigoArticulo", Item.CodigoArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Presentacion", Item.Presentacion));
            oSQL.Parameters.Add(new SqlParameter("@idLinea", Item.IdLinea));
            oSQL.Parameters.Add(new SqlParameter("@IVA", Item.IVAVenta));
            oSQL.Parameters.Add(new SqlParameter("@codigoBarra", Item.CodigoBarra));
            oSQL.Parameters.Add(new SqlParameter("@idProveedor", Item.IdTercero));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.IdEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.IdBodega));
            oSQL.Parameters.Add(new SqlParameter("@EsInventario", Item.EsInventario));
            oSQL.Parameters.Add(new SqlParameter("@StockMinimo", Item.StockMinimo));
            oSQL.Parameters.Add(new SqlParameter("@PrecioAutomatico", Item.PrecioAutomatico));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@IVACompra", Item.IVACompra));
            if (string.IsNullOrEmpty(Item.Ubicacion))
            {
                oSQL.Parameters.Add(new SqlParameter("@Ubicacion", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Ubicacion", Item.Ubicacion));
            }
            oSQL.Parameters.Add(new SqlParameter("@CostoPonderado", Item.CostoPonderado));
            oSQL.Parameters.Add(new SqlParameter("@EsCompuesto", Item.EsCompuesto));
            oSQL.Parameters.Add(new SqlParameter("@EsArticuloFinal", Item.EsArticuloFinal));
            oSQL.Parameters.Add(new SqlParameter("@EsHijo", Item.EsHijo));
            if (Item.IdArticuloPadre == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@IdArticuloPadre", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@IdArticuloPadre", Item.IdArticuloPadre));
            }
            if (Item.CantidadPadre == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@CantidadPadre", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@CantidadPadre", Item.CantidadPadre));
            }
            if (string.IsNullOrEmpty(Item.Marca))
            {
                oSQL.Parameters.Add(new SqlParameter("@Marca", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Marca", Item.Marca));
            }
            if(Item.PorcentajeComision == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@PorcentajeComision", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@PorcentajeComision", Item.PorcentajeComision));
            }
            try
            {
                Conexion.Open();
                Item.IdArticulo = long.Parse(((decimal)oSQL.ExecuteScalar()).ToString());
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return true;
        }

        private bool Actualizar(tblArticuloItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarArticulo", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idArticulo", Item.IdArticulo));
            oSQL.Parameters.Add(new SqlParameter("@CodigoArticulo", Item.CodigoArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Presentacion", Item.Presentacion));
            oSQL.Parameters.Add(new SqlParameter("@idLinea", Item.IdLinea));
            oSQL.Parameters.Add(new SqlParameter("@IVA", Item.IVAVenta));
            oSQL.Parameters.Add(new SqlParameter("@codigoBarra", Item.CodigoBarra));
            oSQL.Parameters.Add(new SqlParameter("@idTercero", Item.IdTercero));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.IdEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.IdBodega));
            oSQL.Parameters.Add(new SqlParameter("@EsInventario", Item.EsInventario));
            oSQL.Parameters.Add(new SqlParameter("@StockMinimo", Item.StockMinimo));
            oSQL.Parameters.Add(new SqlParameter("@PrecioAutomatico", Item.PrecioAutomatico));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@IVACompra", Item.IVACompra));
            if (string.IsNullOrEmpty(Item.Ubicacion))
            {
                oSQL.Parameters.Add(new SqlParameter("@Ubicacion", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Ubicacion", Item.Ubicacion));
            }
            oSQL.Parameters.Add(new SqlParameter("@CostoPonderado", Item.CostoPonderado));
            oSQL.Parameters.Add(new SqlParameter("@EsCompuesto", Item.EsCompuesto));
            oSQL.Parameters.Add(new SqlParameter("@EsArticuloFinal", Item.EsArticuloFinal));
            oSQL.Parameters.Add(new SqlParameter("@EsHijo", Item.EsHijo));
            if (Item.IdArticuloPadre == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@IdArticuloPadre", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@IdArticuloPadre", Item.IdArticuloPadre));
            }
            if (Item.CantidadPadre == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@CantidadPadre", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@CantidadPadre", Item.CantidadPadre));
            }
            if (string.IsNullOrEmpty(Item.Marca))
            {
                oSQL.Parameters.Add(new SqlParameter("@Marca", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Marca", Item.Marca));
            }
            if (Item.PorcentajeComision == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@PorcentajeComision", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@PorcentajeComision", Item.PorcentajeComision));
            }
            try
            {
                Conexion.Open();
                oSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return true;
        }
        public string ActualizarDatosArticulosMasivo(List<tblArticuloItem> oListArt)
        {
            string Error = string.Empty;
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                foreach (tblArticuloItem Item in oListArt)
                {
                    SqlCommand oSQL = new SqlCommand("spActualizarDatosArticulosMasivo", Conexion, oTran);
                    oSQL.CommandType = CommandType.StoredProcedure;
                    oSQL.Parameters.Add(new SqlParameter("@IdArticulo", Item.IdArticulo));
                    oSQL.Parameters.Add(new SqlParameter("@IdBodega", Item.IdBodega));
                    if (string.IsNullOrEmpty(Item.Nombre))
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Nombre", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
                    }
                    if (string.IsNullOrEmpty(Item.Marca))
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Marca", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Marca", Item.Marca));
                    }
                    if (string.IsNullOrEmpty(Item.Ubicacion))
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Ubicacion", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Ubicacion", Item.Ubicacion));
                    }
                    if (Item.Precio == 0)
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Precio", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Precio", Item.Precio));
                    }
                    if (Item.Costo == 0)
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Costo", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Costo", Item.Costo));
                    }
                    oSQL.ExecuteNonQuery();
                }
                oTran.Commit();
            }
            catch (Exception ex)
            {
                oTran.Rollback();
                return ex.Message;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Error;
        }
        public string CreacionMasivaArticulos(List<tblArticuloItem> oListArt)
        {
            string Error = string.Empty;
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                foreach (tblArticuloItem Item in oListArt)
                {
                    SqlCommand oSQL = new SqlCommand("spInsertarArticulo", Conexion, oTran);
                    oSQL.CommandType = CommandType.StoredProcedure;
                    oSQL.Parameters.Add(new SqlParameter("@CodigoArticulo", Item.CodigoArticulo));
                    oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
                    oSQL.Parameters.Add(new SqlParameter("@Presentacion", Item.Presentacion));
                    oSQL.Parameters.Add(new SqlParameter("@idLinea", Item.IdLinea));
                    oSQL.Parameters.Add(new SqlParameter("@IVA", Item.IVAVenta));
                    oSQL.Parameters.Add(new SqlParameter("@codigoBarra", Item.CodigoBarra));
                    oSQL.Parameters.Add(new SqlParameter("@idProveedor", Item.IdTercero));
                    oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.IdEmpresa));
                    oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.IdBodega));
                    oSQL.Parameters.Add(new SqlParameter("@EsInventario", Item.EsInventario));
                    oSQL.Parameters.Add(new SqlParameter("@StockMinimo", Item.StockMinimo));
                    oSQL.Parameters.Add(new SqlParameter("@PrecioAutomatico", Item.PrecioAutomatico));
                    oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
                    oSQL.Parameters.Add(new SqlParameter("@IVACompra", Item.IVACompra));
                    if (string.IsNullOrEmpty(Item.Ubicacion))
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Ubicacion", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Ubicacion", Item.Ubicacion));
                    }
                    oSQL.Parameters.Add(new SqlParameter("@CostoPonderado", Item.CostoPonderado));
                    oSQL.Parameters.Add(new SqlParameter("@EsCompuesto", Item.EsCompuesto));
                    oSQL.Parameters.Add(new SqlParameter("@EsArticuloFinal", Item.EsArticuloFinal));
                    oSQL.Parameters.Add(new SqlParameter("@EsHijo", Item.EsHijo));
                    if (Item.IdArticuloPadre == 0)
                    {
                        oSQL.Parameters.Add(new SqlParameter("@IdArticuloPadre", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@IdArticuloPadre", Item.IdArticuloPadre));
                    }
                    if (Item.CantidadPadre == 0)
                    {
                        oSQL.Parameters.Add(new SqlParameter("@CantidadPadre", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@CantidadPadre", Item.CantidadPadre));
                    }
                    if (string.IsNullOrEmpty(Item.Marca))
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Marca", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Marca", Item.Marca));
                    }
                    if (Item.PorcentajeComision == 0)
                    {
                        oSQL.Parameters.Add(new SqlParameter("@PorcentajeComision", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@PorcentajeComision", Item.PorcentajeComision));
                    }
                    Item.IdArticulo = long.Parse(((decimal)oSQL.ExecuteScalar()).ToString());
                    if(Item.IdArticulo > 0)
                    {
                        SqlCommand oSQL1 = new SqlCommand("spInsertarArticulo_Bodega", Conexion, oTran);
                        oSQL1.CommandType = System.Data.CommandType.StoredProcedure;
                        oSQL1.Parameters.Add(new SqlParameter("@idArticulo", Item.IdArticulo));
                        oSQL1.Parameters.Add(new SqlParameter("@idBodega", Item.IdBodega));
                        oSQL1.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
                        oSQL1.Parameters.Add(new SqlParameter("@Costo", Item.Costo));
                        oSQL1.Parameters.Add(new SqlParameter("@Precio", Item.Precio));
                        oSQL1.Parameters.Add(new SqlParameter("@IdTipoManejoPrecio", 1));
                        oSQL1.ExecuteNonQuery();
                    }
                    else
                    {
                        Error = string.Format("{0} * No se pudo crear el código {1}<br>", Error, Item.CodigoArticulo);
                    }
                }
                oTran.Commit();
            }
            catch(Exception ex)
            {
                oTran.Rollback();
                return ex.Message;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Error;
        }
        public bool Guardar(tblArticuloItem Item)
        {
            if (Item.IdArticulo > 0)
            {
                return Actualizar(Item);
            }
            else
            {
                return Insertar(Item);
            }
        }

        public List<JSONItem> ObtenerArticuloListaPorNombreSencillo(string nombre, long idEmpresa, int Tipo)
        {
            List<JSONItem> Lista = new List<JSONItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerArticuloListaPorNombreSencillo", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@nombre", nombre));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@Tipo", Tipo));
            try
            {
                Conexion.Open();
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(ObtenerDatosArticulo(reader));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Lista;
        }

        public List<JSONItem> ObtenerArticuloListaPorNombre(string nombre, long idEmpresa, long tipoDocumento, long idBodega, long idTercero)
        {
            List<JSONItem> Lista = new List<JSONItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerArticuloListaPorNombre @nombre,@idEmpresa,@tipoDocumento,@idBodega,@idTercero", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@nombre", nombre));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@tipoDocumento", tipoDocumento));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", idBodega));
            oSQL.Parameters.Add(new SqlParameter("@idTercero", idTercero));
            try
            {
                Conexion.Open();
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(ObtenerDatosArticulo(reader));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Lista;
        }

        private JSONItem ObtenerDatosArticulo(SqlDataReader reader)
        {
            return new JSONItem(reader["datosArticulo"].ToString(), reader["Nombre"].ToString());
        }

        public List<JSONItem> ObtenerArticuloListaPorCodigoOCodigoBarras(string codigo, long idEmpresa, long tipoDocumento, long idBodega, long idTercero)
        {
            List<JSONItem> Lista = new List<JSONItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerArticuloListaPorCodigoOCodigoBarras @codigo,@idEmpresa,@tipoDocumento,@idBodega,@idTercero", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@codigo", codigo));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@tipoDocumento", tipoDocumento));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", idBodega));
            oSQL.Parameters.Add(new SqlParameter("@idTercero", idTercero));
            try
            {
                Conexion.Open();
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(ObtenerDatosArticulo(reader));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Lista;
        }

        public bool SumarRestarExistenciasArticulo(decimal cantidad, long idArticulo, long idBodega)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spSumarRestarExistenciasArticulo @Cantidad,@idArticulo,@idBodega", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", cantidad));
            oSQL.Parameters.Add(new SqlParameter("@idArticulo", idArticulo));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", idBodega));
            try
            {
                Conexion.Open();
                oSQL.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return true;
        }

        public bool SumarRestarExistenciasArticuloCosto(decimal cantidad, long idArticulo, long idBodega, decimal costo, long idEmpresa)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spSumarRestarExistenciasArticuloCosto @Cantidad,@idArticulo,@idBodega,@costo,@idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", cantidad));
            oSQL.Parameters.Add(new SqlParameter("@idArticulo", idArticulo));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", idBodega));
            oSQL.Parameters.Add(new SqlParameter("@costo", costo));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
            try
            {
                Conexion.Open();
                oSQL.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return true;
        }

        public tblArticuloItem ObtenerArticuloPorCodigo(long codigo)
        {
            tblArticuloItem Item = new tblArticuloItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerArticuloPorCodigo @id", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", codigo));
                SqlDataReader reader = oSQL.ExecuteReader();
                if (reader.Read())
                {
                    Item = ObtenerItem(reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Item;
        }

    }
}
