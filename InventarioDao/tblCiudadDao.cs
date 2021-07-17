using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblCiudadDao
    {
        private SqlConnection Conexion { get; set; }

        public tblCiudadDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblCiudadItem ObtenerCiudad(long Id)
        {
            tblCiudadItem Item = new tblCiudadItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerCiudad @id", Conexion);
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

        public List<tblCiudadItem> ObtenerCiudadLista()
        {
            List<tblCiudadItem> Lista = new List<tblCiudadItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerCiudadLista", Conexion);
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

        private tblCiudadItem ObtenerItem(SqlDataReader reader)
        {
            tblCiudadItem Item = new tblCiudadItem();
            Item.idCiudad = short.Parse(reader["idCiudad"].ToString());
            Item.Codigo = reader["Codigo"].ToString();
            Item.Nombre = reader["Nombre"].ToString();
            Item.idDepartamento = short.Parse(reader["idDepartamento"].ToString());
            return Item;
        }

        private bool Insertar(tblCiudadItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarCiudad @idCiudad,@Codigo,@Nombre,@idDepartamento", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idCiudad", Item.idCiudad));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@idDepartamento", Item.idDepartamento));
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
        private bool Actualizar(tblCiudadItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarCiudad @idCiudad,@Codigo,@Nombre,@idDepartamento", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idCiudad", Item.idCiudad));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@idDepartamento", Item.idDepartamento));
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
        public bool Guardar(tblCiudadItem Item)
        {
            if (Item.idCiudad > 0)
            {
                return Actualizar(Item);
            }
            else
            {
                return Insertar(Item);
            }
        }

        public List<JSONItem> ObtenerCiudadListaPorNombre(string nombre){
            List<JSONItem> Lista = new List<JSONItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerCiudadListaPorNombre @nombre", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add("@nombre",nombre);
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(ObtenerItemFiltro(reader));
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

        private JSONItem ObtenerItemFiltro(SqlDataReader reader)
        {
            return new JSONItem(reader["idCiudad"].ToString(), reader["Nombre"].ToString());
        }
    }
}
