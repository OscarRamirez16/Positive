using System;
using System.Data.SqlClient;
using InventarioItem;
using System.Data;

namespace InventarioDao
{
    public class tblRetencionDao
    {
        private SqlConnection Conexion { get; set; }

        public tblRetencionDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public tblRetencionItem ObtenerRetencionPorID(long Id)
        {
            tblRetencionItem Item = new tblRetencionItem();
            SqlCommand oSQL = new SqlCommand("spObtenerRetencionPorID", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@Id", Id));
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
        public DataTable ObtenerRetencionesTodas(long IdEmpresa)
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerRetencionesTodas", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
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
        public DataTable ObtenerRetencionesActivas(long IdEmpresa)
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerRetencionesActivas", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
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
        private tblRetencionItem ObtenerItem(SqlDataReader reader)
        {
            tblRetencionItem Item = new tblRetencionItem();
            Item.Id = long.Parse(reader["Id"].ToString());
            Item.Codigo = reader["Codigo"].ToString();
            Item.Descripcion = reader["Descripcion"].ToString();
            Item.Porcentaje = decimal.Parse(reader["Porcentaje"].ToString());
            Item.Base = decimal.Parse(reader["Base"].ToString());
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.IdEmpresa = long.Parse(reader["idEmpresa"].ToString());
            return Item;
        }
        private string Insertar(tblRetencionItem Item)
        {
            string Mensaje = string.Empty;
            SqlCommand oSQL = new SqlCommand("spInsertarRetencion", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Descripcion));
            oSQL.Parameters.Add(new SqlParameter("@Porcentaje", Item.Porcentaje));
            oSQL.Parameters.Add(new SqlParameter("@Base", Item.Base));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.IdEmpresa));
            try
            {
                Conexion.Open();
                Item.Id = long.Parse(((decimal)oSQL.ExecuteScalar()).ToString());
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
            return Mensaje;
        }
        private string Actualizar(tblRetencionItem Item)
        {
            string Mensaje = string.Empty;
            SqlCommand oSQL = new SqlCommand("spActualizarRetencion", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@Id", Item.Id));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Descripcion));
            oSQL.Parameters.Add(new SqlParameter("@Porcentaje", Item.Porcentaje));
            oSQL.Parameters.Add(new SqlParameter("@Base", Item.Base));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            try
            {
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
            return Mensaje;
        }
        public string Guardar(tblRetencionItem Item)
        {
            if (Item.Id > 0)
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
