using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;
using System.Data.SqlClient;

namespace InventarioDao
{
    public class tblTipoDocumentoDao
    {
        private SqlConnection Conexion { get; set; }
        public tblTipoDocumentoDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblTipoDocumentoItem ObtenerTipoDocumento(long Id)
        {
            tblTipoDocumentoItem Item = new tblTipoDocumentoItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerTipoDocumento @id", Conexion);
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

        public tblTipoDocumentoItem ObtenerTipoDocumentoConTransaccion(long Id, SqlConnection Con, SqlTransaction oTran)
        {
            tblTipoDocumentoItem Item = new tblTipoDocumentoItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerTipoDocumento @id", Con, oTran);
            try
            {
                oSQL.Parameters.Add(new SqlParameter("@id", Id));
                SqlDataReader reader = oSQL.ExecuteReader();
                if (reader.Read())
                {
                    Item = ObtenerItem(reader);
                }
                reader.Close();
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

        public List<tblTipoDocumentoItem> ObtenerTipoDocumentoLista()
        {
            List<tblTipoDocumentoItem> Lista = new List<tblTipoDocumentoItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerTipoDocumentoLista", Conexion);
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
        private tblTipoDocumentoItem ObtenerItem(SqlDataReader reader)
        {
            tblTipoDocumentoItem Item = new tblTipoDocumentoItem();
            Item.idTipoDocumento = short.Parse(reader["idTipoDocumento"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            Item.TablaDocumento = reader["TablaDocumento"].ToString();
            Item.TablaDetalle = reader["TablaDetalle"].ToString();
            Item.idTipoSocioNegocio = reader["idTipoSocioNegocio"].ToString();
            Item.AfectaInventario = bool.Parse(reader["AfectaInventario"].ToString());
            Item.SentidoInventario = short.Parse(reader["SentidoInventario"].ToString());
            Item.ManejaListaPrecios = bool.Parse(reader["ManejaListaPrecios"].ToString());
            Item.TablaNumeracion = reader["TablaNumeracion"].ToString();
            return Item;
        }
    }
}
