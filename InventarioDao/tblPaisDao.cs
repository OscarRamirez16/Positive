using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblPaisDao
    {
        private SqlConnection Conexion { get; set; }
        public tblPaisDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public tblPaisItem ObtenerPais(long Id)
        {
            tblPaisItem Item = new tblPaisItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPais @id", Conexion);
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
        public List<tblPaisItem> ObtenerPaisLista()
        {
            List<tblPaisItem> Lista = new List<tblPaisItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPaisLista", Conexion);
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
        private tblPaisItem ObtenerItem(SqlDataReader reader)
        {
            tblPaisItem Item = new tblPaisItem();
            Item.idPais = short.Parse(reader["idPais"].ToString());
            Item.Codigo = reader["Codigo"].ToString();
            Item.Nombre = reader["Nombre"].ToString();
            return Item;
        }
        private bool Insertar(tblPaisItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarPais @idPais,@Codigo,@Nombre", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idPais", Item.idPais));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
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
        private bool Actualizar(tblPaisItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarPais @idPais,@Codigo,@Nombre", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idPais", Item.idPais));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
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
        public bool Guardar(tblPaisItem Item)
        {
            if (Item.idPais > 0)
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
