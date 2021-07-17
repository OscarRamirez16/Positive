using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;
using System.Data.SqlClient;

namespace InventarioDao
{
    public class tblTipoManejoPrecioDao
    {

        private SqlConnection Conexion { get; set; }

        public tblTipoManejoPrecioDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblTipoManejoPrecioItem ObtenerTipoManejoPrecio(long Id)
        {
            tblTipoManejoPrecioItem Item = new tblTipoManejoPrecioItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerTipoManejoPrecio @id", Conexion);
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

        public List<tblTipoManejoPrecioItem> ObtenerTipoManejoPrecioLista()
        {
            List<tblTipoManejoPrecioItem> Lista = new List<tblTipoManejoPrecioItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerTipoManejoPrecioLista", Conexion);
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

        private tblTipoManejoPrecioItem ObtenerItem(SqlDataReader reader)
        {
            tblTipoManejoPrecioItem Item = new tblTipoManejoPrecioItem();
            Item.IdTipoManejoPrecio = short.Parse(reader["IdTipoManejoPrecio"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            return Item;
        }
    }
}
