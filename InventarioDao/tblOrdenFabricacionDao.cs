using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventarioItem;
using System.Data;

namespace InventarioDao
{
    public class tblOrdenFabricacionDao
    {
        private SqlConnection Conexion { get; set; }

        public tblOrdenFabricacionDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public string CambiarEstadoOF(long IdOF, short IdEstado)
        {
            string Error = string.Empty;
            try
            {
                SqlCommand oSQL = new SqlCommand("spCambiarEstadoOF", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdOF", IdOF));
                oSQL.Parameters.Add(new SqlParameter("@IdEstado", IdEstado));
                Conexion.Open();
                oSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return Error;
        }

        public string ObtenerOrdenFabricacionDetallePorID(long IdOrden, long IdArticulo)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerOrdenFabricacionDetallePorID", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdOrden", IdOrden));
                oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                Conexion.Open();
                return oSQL.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblOrdenFabricacionItem> ObtenerOrdenesFabricacionPorFiltros(DateTime FechaInicial, DateTime FechaFinal, string IdOrden, long IdUsuario)
        {
            List<tblOrdenFabricacionItem> Lista = new List<tblOrdenFabricacionItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerOrdenesFabricacionPorFiltros", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
            oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
            if (string.IsNullOrEmpty(IdOrden))
            {
                oSQL.Parameters.Add(new SqlParameter("@IdOrden", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@IdOrden", IdOrden));
            }
            if (IdUsuario == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@IdUsuario", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
            }
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

        public DataTable ObtenerEstadosOrdenFabricacion()
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerEstadosOrdenFabricacion", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }

        public tblOrdenFabricacionItem ObtenerOrdenFabricacion(long IdOrden)
        {
            tblOrdenFabricacionItem Item = new tblOrdenFabricacionItem();
            SqlCommand oSQL = new SqlCommand("spObtenerOrdenFabricacionPorID", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdOrdenFabricacion", IdOrden));
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

        public List<tblOrdenFabricacionItem> ObtenerOrdenFabricacionLista()
        {
            List<tblOrdenFabricacionItem> Lista = new List<tblOrdenFabricacionItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerOrdenFabricacionLista", Conexion);
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

        public List<tblOrdenFabricacionDetalleItem> ObtenerOrdenFabricacionDetalleLista(long IdOrdenFabricacion)
        {
            List<tblOrdenFabricacionDetalleItem> Lista = new List<tblOrdenFabricacionDetalleItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerOrdenFabricacionDetalleLista", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdOrdenFabricacion", IdOrdenFabricacion));
            try
            {
                Conexion.Open();
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(ObtenerItemDetalle(reader));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }

        private tblOrdenFabricacionDetalleItem ObtenerItemDetalle(SqlDataReader reader)
        {
            tblOrdenFabricacionDetalleItem Item = new tblOrdenFabricacionDetalleItem();
            Item.IdDetalle = long.Parse(reader["IdDetalle"].ToString());
            Item.IdOrdenFabricacion = long.Parse(reader["IdOrdenFabricacion"].ToString());
            Item.IdArticulo = long.Parse(reader["IdArticulo"].ToString());
            Item.Articulo = reader["Articulo"].ToString();
            Item.Cantidad = decimal.Parse(reader["Cantidad"].ToString());
            Item.IdBodega = long.Parse(reader["IdBodega"].ToString());
            return Item;
        }

        private tblOrdenFabricacionItem ObtenerItem(SqlDataReader reader)
        {
            tblOrdenFabricacionItem Item = new tblOrdenFabricacionItem();
            Item.IdOrdenFabricacion = long.Parse(reader["IdOrdenFabricacion"].ToString());
            Item.IdListaMateriales = reader["IdListaMateriales"].ToString();
            Item.ListaMateriales = reader["ListaMateriales"].ToString();
            Item.FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
            Item.IdUsuario = long.Parse(reader["IdUsuario"].ToString());
            Item.Usuario = reader["Usuario"].ToString();
            Item.IdEstado = short.Parse(reader["IdEstado"].ToString());
            Item.Estado = reader["Estado"].ToString();
            Item.IdEmpresa = long.Parse(reader["IdEmpresa"].ToString());
            Item.Cantidad = decimal.Parse(reader["Cantidad"].ToString());
            return Item;
        }

        private bool GuardarDetalle(tblOrdenFabricacionDetalleItem Item, SqlConnection oCon, SqlTransaction oTran)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarOrdenFabricacionDetalle", Conexion, oTran);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdOrdenFabricacion", Item.IdOrdenFabricacion));
            oSQL.Parameters.Add(new SqlParameter("@IdArticulo", Item.IdArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
            oSQL.Parameters.Add(new SqlParameter("@IdBodega", Item.IdBodega));
            try
            {
                oSQL.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Insertar(tblOrdenFabricacionItem Item)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            SqlCommand oSQL = new SqlCommand("spInsertarOrdenFabricacion", Conexion, oTran);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdListaMateriales", Item.IdListaMateriales));
            oSQL.Parameters.Add(new SqlParameter("@FechaCreacion", Item.FechaCreacion));
            oSQL.Parameters.Add(new SqlParameter("@IdUsuario", Item.IdUsuario));
            oSQL.Parameters.Add(new SqlParameter("@IdEstado", Item.IdEstado));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", Item.IdEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
            try
            {
                Item.IdOrdenFabricacion = long.Parse(oSQL.ExecuteScalar().ToString());
                if (Item.IdOrdenFabricacion > 0)
                {
                    foreach (tblOrdenFabricacionDetalleItem Detalle in Item.Detalles)
                    {
                        Detalle.IdOrdenFabricacion = Item.IdOrdenFabricacion;
                        if (!GuardarDetalle(Detalle, Conexion, oTran))
                        {
                            oTran.Rollback();
                            return false;
                        }
                    }
                    oTran.Commit();
                }
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

        private bool Actualizar(tblOrdenFabricacionItem Item)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            SqlCommand oSQL = new SqlCommand("spActualizarOrdenFabricacion", Conexion, oTran);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdOrdenFabricacion", Item.IdOrdenFabricacion));
            oSQL.Parameters.Add(new SqlParameter("@IdEstado", Item.IdEstado));
            try
            {
                if (oSQL.ExecuteNonQuery() > 0)
                {
                    if (EliminarDetallesOrden(Item.IdOrdenFabricacion, Conexion, oTran))
                    {
                        foreach (tblOrdenFabricacionDetalleItem Detalle in Item.Detalles)
                        {
                            Detalle.IdOrdenFabricacion = Item.IdOrdenFabricacion;
                            if (!GuardarDetalle(Detalle, Conexion, oTran))
                            {
                                oTran.Rollback();
                                return false;
                            }
                        }
                        oTran.Commit();
                    }
                    else
                    {
                        oTran.Rollback();
                    }
                }
                else
                {
                    oTran.Rollback();
                    return false;
                }
            }
            catch
            {
                oTran.Rollback();
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

        private bool EliminarDetallesOrden(long IdOrden, SqlConnection oCon, SqlTransaction oTran)
        {
            SqlCommand oSQL = new SqlCommand("spEliminarDetallesOrden", oCon, oTran);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdOrdenFabricacion", IdOrden));
            try
            {
                oSQL.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Guardar(tblOrdenFabricacionItem Item)
        {
            if (Item.IdOrdenFabricacion > 0)
            {
                return Actualizar(Item);
            }
            else
            {
                return Insertar(Item);
            }
        }
    }
}
