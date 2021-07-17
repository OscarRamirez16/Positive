using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblRol_PaginaDao
    {
        private SqlConnection Conexion { get; set; }

        public tblRol_PaginaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblRol_PaginaItem ObtenerRol_Pagina(long Id)
        {
            tblRol_PaginaItem Item = new tblRol_PaginaItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerRol_Pagina @id", Conexion);
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
        public List<tblRol_PaginaItem> ObtenerRol_PaginaLista(long idRol, long idEmpresa)
        {
            List<tblRol_PaginaItem> Lista = new List<tblRol_PaginaItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerRol_PaginaLista @idRol, @idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idRol", idRol));
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
        private tblRol_PaginaItem ObtenerItem(SqlDataReader reader)
        {
            tblRol_PaginaItem Item = new tblRol_PaginaItem();
            Item.idRol = short.Parse(reader["idRol"].ToString());
            Item.idPagina = short.Parse(reader["idPagina"].ToString());
            Item.Leer = bool.Parse(reader["Leer"].ToString());
            Item.Insertar = bool.Parse(reader["Insertar"].ToString());
            Item.Actualizar = bool.Parse(reader["Actualizar"].ToString());
            Item.Eliminar = bool.Parse(reader["Eliminar"].ToString());
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            return Item;
        }

        public bool Insertar(tblRol_PaginaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarRol_Pagina @idRol,@idPagina,@Leer,@Insertar,@Actualizar,@Eliminar,@idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idRol", Item.idRol));
            oSQL.Parameters.Add(new SqlParameter("@idPagina", Item.idPagina));
            oSQL.Parameters.Add(new SqlParameter("@Leer", Item.Leer));
            oSQL.Parameters.Add(new SqlParameter("@Insertar", Item.Insertar));
            oSQL.Parameters.Add(new SqlParameter("@Actualizar", Item.Actualizar));
            oSQL.Parameters.Add(new SqlParameter("@Eliminar", Item.Eliminar));
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

        public bool eliminarPaginasRoles(long iRol)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spEliminarPaginasRolesPorIDRol @idRol", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idRol", iRol));
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

        public tblRol_PaginaItem TraerPermisosPaginasPorUsuario(long idUsuario, long idEmpresa, int idPagina)
        {
            tblRol_PaginaItem Item = new tblRol_PaginaItem();
            SqlCommand oSQL = new SqlCommand("EXEC spPermisosPaginaPorUsuario @idUsuario,@idEmpresa,@idPagina", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idPagina", idPagina));
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
