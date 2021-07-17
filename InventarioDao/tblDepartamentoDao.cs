using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblDepartamentoDao
    {
        private SqlConnection Conexion { get; set; }

        public tblDepartamentoDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblDepartamentoItem ObtenerDepartamento(long Id)
        {
            tblDepartamentoItem Item = new tblDepartamentoItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerDepartamento @id", Conexion);
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

        public List<tblDepartamentoItem> ObtenerDepartamentoLista()
        {
            List<tblDepartamentoItem> Lista = new List<tblDepartamentoItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerDepartamentoLista", Conexion);
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

        private tblDepartamentoItem ObtenerItem(SqlDataReader reader)
        {
            tblDepartamentoItem Item = new tblDepartamentoItem();
            Item.idDepartamento = short.Parse(reader["idDepartamento"].ToString());
            Item.Codigo = reader["Codigo"].ToString();
            Item.Nombre = reader["Nombre"].ToString();
            Item.idPais = short.Parse(reader["idPais"].ToString());
            return Item;
        }

        private bool Insertar(tblDepartamentoItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarDepartamento @idDepartamento,@Codigo,@Nombre,@idPais", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idDepartamento", Item.idDepartamento));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@idPais", Item.idPais));
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

        private bool Actualizar(tblDepartamentoItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarDepartamento @idDepartamento,@Codigo,@Nombre,@idPais", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idDepartamento", Item.idDepartamento));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@idPais", Item.idPais));
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

        public bool Guardar(tblDepartamentoItem Item)
        {
            if (Item.idDepartamento > 0)
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
