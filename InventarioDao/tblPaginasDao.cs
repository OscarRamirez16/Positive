using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblPaginasDao
    {
        private SqlConnection Conexion { get; set; }
        public tblPaginasDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public tblPaginasItem ObtenerPaginas(long Id)
        {
            tblPaginasItem Item = new tblPaginasItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPaginas @id", Conexion);
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

        public List<tblPaginasItem> ObtenerPaginasLista()
        {
            List<tblPaginasItem> Lista = new List<tblPaginasItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPaginasLista", Conexion);
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

        private tblPaginasItem ObtenerItem(SqlDataReader reader)
        {
            tblPaginasItem Item = new tblPaginasItem();
            Item.idPagina = short.Parse(reader["idPagina"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            Item.Url = reader["Url"].ToString();
            return Item;
        }

        private bool Insertar(tblPaginasItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarPaginas @idPaginas,@Nombre,@Url", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idPaginas", Item.idPagina));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Url", Item.Url));
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

        private bool Actualizar(tblPaginasItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarPaginas @idPaginas,@Nombre,@Url", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idPaginas", Item.idPagina));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Url", Item.Url));
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

        public bool Guardar(tblPaginasItem Item)
        {
            if (Item.idPagina > 0)
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
