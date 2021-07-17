using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblTipoIdentificacionDao
    {
        private SqlConnection Conexion { get; set; }

        public tblTipoIdentificacionDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblTipoIdentificacionItem ObtenerTipoIdentificacion(long Id)
        {
            tblTipoIdentificacionItem Item = new tblTipoIdentificacionItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerTipoIdentificacion @id", Conexion);
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

        public List<tblTipoIdentificacionItem> ObtenerTipoIdentificacionLista()
        {
            List<tblTipoIdentificacionItem> Lista = new List<tblTipoIdentificacionItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerTipoIdentificacionLista", Conexion);
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

        private tblTipoIdentificacionItem ObtenerItem(SqlDataReader reader)
        {
            tblTipoIdentificacionItem Item = new tblTipoIdentificacionItem();
            Item.idTipoIdentificacion = short.Parse(reader["idTipoIdentificacion"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            return Item;
        }

        private bool Insertar(tblTipoIdentificacionItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarTipoIdentificacion @idTipoIdentificacion,@Nombre", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idTipoIdentificacion", Item.idTipoIdentificacion));
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
        private bool Actualizar(tblTipoIdentificacionItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarTipoIdentificacion @idTipoIdentificacion,@Nombre", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idTipoIdentificacion", Item.idTipoIdentificacion));
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
        public bool Guardar(tblTipoIdentificacionItem Item)
        {
            if (Item.idTipoIdentificacion > 0)
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
