using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;
using System.Data.SqlClient;

namespace InventarioDao
{
    public class tblListaMaterialesDetalleDao
    {
        private SqlConnection Conexion { get; set; }

        public tblListaMaterialesDetalleDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public List<tblListaMaterialesDetalleItem> ObtenerListaMaterialesDetalleLista(string IdListaMateriales)
        {
            List<tblListaMaterialesDetalleItem> Lista = new List<tblListaMaterialesDetalleItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerListaMaterialesDetalleLista @IdListaMateriales", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdListaMateriales", IdListaMateriales));
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

        private tblListaMaterialesDetalleItem ObtenerItem(SqlDataReader reader)
        {
            tblListaMaterialesDetalleItem Item = new tblListaMaterialesDetalleItem();
            Item.IdListaMaterialesDetalle = reader["IdListaMaterialesDetalle"].ToString();
            Item.IdListaMateriales = reader["IdListaMateriales"].ToString();
            Item.IdArticulo = long.Parse(reader["IdArticulo"].ToString());
            Item.Articulo = reader["Articulo"].ToString();
            Item.Cantidad = decimal.Parse(reader["Cantidad"].ToString());
            return Item;
        }

        public void Insertar(tblListaMaterialesDetalleItem Item, SqlConnection Con, SqlTransaction oTran)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarListaMaterialesDetalle @IdListaMateriales,@IdArticulo,@Cantidad", Con, oTran);
            oSQL.Parameters.Add(new SqlParameter("@IdListaMateriales", Item.IdListaMateriales));
            oSQL.Parameters.Add(new SqlParameter("@IdArticulo", Item.IdArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
            try
            {
                oSQL.ExecuteNonQuery();
            }
            catch(Exception ex)
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

        public bool Eliminar(string IdListaMateriales, SqlConnection Con, SqlTransaction oTran)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spEliminarListaMaterialesDetalle @IdListaMateriales", Con, oTran);
            try
            {
                oSQL.Parameters.Add(new SqlParameter("@IdListaMateriales", IdListaMateriales));
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
