using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblPreciosPorBodegaDao
    {
        private SqlConnection Conexion { get; set; }

        public tblPreciosPorBodegaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public bool EliminarPreciosPorBodega(long IdArticulo, long IdBodega)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spEliminarPreciosPorBodega @IdArticulo,@IdBodega", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                oSQL.Parameters.Add(new SqlParameter("@IdBodega", IdBodega));
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

        public List<tblPreciosPorBodegaItem> ObtenerPreciosPorBodegaVenta(long IdArticulo, long IdBodega)
        {
            List<tblPreciosPorBodegaItem> Lista = new List<tblPreciosPorBodegaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerPreciosPorBodegaVenta", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                oSQL.Parameters.Add(new SqlParameter("@IdBodega", IdBodega));
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

        public List<tblPreciosPorBodegaItem> ObtenerPreciosPorBodega(long IdArticulo, long IdBodega)
        {
            List<tblPreciosPorBodegaItem> Lista = new List<tblPreciosPorBodegaItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPreciosPorBodega @IdArticulo,@IdBodega", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                oSQL.Parameters.Add(new SqlParameter("@IdBodega", IdBodega));
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

        private tblPreciosPorBodegaItem ObtenerItem(SqlDataReader reader)
        {
            tblPreciosPorBodegaItem Item = new tblPreciosPorBodegaItem();
            Item.IdBodega = long.Parse(reader["IdBodega"].ToString());
            Item.IdArticulo = long.Parse(reader["IdArticulo"].ToString());
            Item.Descripcion = reader["Descripcion"].ToString();
            Item.IdTipoManejoPrecio = short.Parse(reader["IdTipoManejoPrecio"].ToString());
            Item.Valor = decimal.Parse(reader["Valor"].ToString());
            return Item;
        }

        public bool Insertar(tblPreciosPorBodegaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarPreciosPorBodega @IdBodega,@IdArticulo,@Descripcion,@IdTipoManejoPrecio,@Valor", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@IdBodega", Item.IdBodega));
            oSQL.Parameters.Add(new SqlParameter("@IdArticulo", Item.IdArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Descripcion));
            oSQL.Parameters.Add(new SqlParameter("@IdTipoManejoPrecio", Item.IdTipoManejoPrecio));
            oSQL.Parameters.Add(new SqlParameter("@Valor", Item.Valor));
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
    }
}
