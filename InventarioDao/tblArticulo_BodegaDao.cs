using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblArticulo_BodegaDao
    {
        private SqlConnection Conexion { get; set; }

        public tblArticulo_BodegaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public bool GuardarArticulosPorBodegaExistencia(List<tblArticulo_BodegaItem> oListArtBod)
        {
            Conexion.Open();
            SqlTransaction oTran = Conexion.BeginTransaction();
            try
            {
                foreach (tblArticulo_BodegaItem Item in oListArtBod)
                {
                    SqlCommand oSQL = new SqlCommand("EXEC spInsertarArticuloBodegaExistencia @idArticulo,@idBodega,@Cantidad,@Costo,@Precio,@IdTipoManejoPrecio", Conexion, oTran);
                    oSQL.Parameters.Add(new SqlParameter("@idArticulo", Item.IdArticulo));
                    oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.IdBodega));
                    oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
                    oSQL.Parameters.Add(new SqlParameter("@Costo", Item.Costo));
                    oSQL.Parameters.Add(new SqlParameter("@Precio", Item.Precio));
                    oSQL.Parameters.Add(new SqlParameter("@IdTipoManejoPrecio", Item.IdTipoManejoPrecio));
                    oSQL.ExecuteNonQuery();
                }
                oTran.Commit();
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

        public tblArticulo_BodegaItem ObtenerArticulo_Bodega(long Id)
        {
            tblArticulo_BodegaItem Item = new tblArticulo_BodegaItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerArticulo_Bodega @id", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", Id));
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

        public tblArticulo_BodegaItem ConsultarArticulosBodegaPorID(long IdArticulo, long IdBodega)
        {
            tblArticulo_BodegaItem Item = new tblArticulo_BodegaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerArticulosBodegaPorID", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
            oSQL.Parameters.Add(new SqlParameter("@IdBodega", IdBodega));
            try
            {
                Conexion.Open();
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
        public List<tblArticulo_BodegaItem> ObtenerArticuloBodegaLista(long idArticulo)
        {
            List<tblArticulo_BodegaItem> Lista = new List<tblArticulo_BodegaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerArticulosBodega", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idArticulo", idArticulo));
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

        private tblArticulo_BodegaItem ObtenerItem(SqlDataReader reader)
        {
            tblArticulo_BodegaItem Item = new tblArticulo_BodegaItem();
            Item.IdArticulo = long.Parse(reader["idArticulo"].ToString());
            Item.Articulo = reader["Articulo"].ToString();
            Item.IdBodega = long.Parse(reader["idBodega"].ToString());
            Item.Bodega = reader["Bodega"].ToString();
            Item.Cantidad = decimal.Parse(reader["Cantidad"].ToString());
            Item.Costo = decimal.Parse(reader["Costo"].ToString());
            Item.Precio = decimal.Parse(reader["Precio"].ToString());
            Item.IdTipoManejoPrecio = short.Parse(reader["IdTipoManejoPrecio"].ToString());
            return Item;
        }

        public string EliminarArticuloBodega(tblArticulo_BodegaItem Item)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spEliminarArticuloBodega", Conexion);
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdArticulo", Item.IdArticulo));
                oSQL.Parameters.Add(new SqlParameter("@IdBodega", Item.IdBodega));
                Conexion.Open();
                oSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return "";
        }

        public string Insertar(tblArticulo_BodegaItem Item)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spInsertarArticulo_Bodega", Conexion);
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@idArticulo", Item.IdArticulo));
                oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.IdBodega));
                oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
                oSQL.Parameters.Add(new SqlParameter("@Costo", Item.Costo));
                oSQL.Parameters.Add(new SqlParameter("@Precio", Item.Precio));
                oSQL.Parameters.Add(new SqlParameter("@IdTipoManejoPrecio", Item.IdTipoManejoPrecio));
                Conexion.Open();
                oSQL.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return "";
        }

        public bool Actualizar(tblArticulo_BodegaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarArticulo_Bodega @idArticulo,@idBodega,@Cantidad,@Costo", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idArticulo", Item.IdArticulo));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.IdBodega));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
            oSQL.Parameters.Add(new SqlParameter("@Costo", Item.Costo));
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

        public void EliminarBodegasArticulos(long idArticulo)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spEliminarBodegasArticulos @idArticulo", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idArticulo", idArticulo));
            try
            {
                Conexion.Open();
                oSQL.ExecuteNonQuery();
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

    }
}
