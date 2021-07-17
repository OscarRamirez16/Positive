using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblLineaDao
    {
        private SqlConnection Conexion { get; set; }

        public tblLineaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public DataTable ObtenerLineaPorDescipcion(string Nombre, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL;
                oSQL = new SqlCommand("spObtenerLineaPorDescipcion", Conexion);
                oSQL.CommandTimeout = 200000;
                oSQL.CommandType = CommandType.StoredProcedure;
                if (string.IsNullOrEmpty(Nombre))
                {
                    oSQL.Parameters.Add(new SqlParameter("@Nombre", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                }
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
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

        public tblLineaItem ObtenerLinea(long Id, long IdEmpresa)
        {
            tblLineaItem Item = new tblLineaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerLinea", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", Id));
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
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
        public List<tblLineaItem> ObtenerLineaLista(long idEmpresa)
        {
            List<tblLineaItem> Lista = new List<tblLineaItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerLineaLista @idEmpresa", Conexion);
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

        private tblLineaItem ObtenerItem(SqlDataReader reader)
        {
            tblLineaItem Item = new tblLineaItem();
            Item.IdLinea = long.Parse(reader["idLinea"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.IdEmpresa = long.Parse(reader["idEmpresa"].ToString());
            return Item;
        }

        private bool Insertar(tblLineaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarLinea", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idLinea", Item.IdLinea));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.IdEmpresa));
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
        private bool Actualizar(tblLineaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarLinea", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idLinea", Item.IdLinea));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.IdEmpresa));
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
        public bool Guardar(tblLineaItem Item)
        {
            if (Item.IdLinea > 0)
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
