using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblUsuario_RolDao
    {
        private SqlConnection Conexion { get; set; }

        public tblUsuario_RolDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblUsuario_RolItem ObtenerUsuario_Rol(long Id)
        {
            tblUsuario_RolItem Item = new tblUsuario_RolItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerUsuario_Rol @id", Conexion);
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

        public List<tblUsuario_RolItem> ObtenerUsuario_RolLista(long idUsuario)
        {
            List<tblUsuario_RolItem> Lista = new List<tblUsuario_RolItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerUsuario_RolLista @idUsuario", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
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

        private tblUsuario_RolItem ObtenerItem(SqlDataReader reader)
        {
            tblUsuario_RolItem Item = new tblUsuario_RolItem();
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.idRol = short.Parse(reader["idRol"].ToString());
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            return Item;
        }

        public bool Insertar(tblUsuario_RolItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarUsuario_Rol @idUsuario,@idRol,@idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idRol", Item.idRol));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
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

        public bool Actualizar(tblUsuario_RolItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarUsuario_Rol @idUsuario,@idRol,@idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idRol", Item.idRol));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
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

        public bool eliminarRolesUsuario(long idUsuario)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spEliminarRolesUsuarioPorIDUsuario @idUsuario", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
            Conexion.Open();
            try
            {
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

    }
}
