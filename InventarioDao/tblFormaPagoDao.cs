using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblFormaPagoDao
    {

        private SqlConnection Conexion { get; set; }

        public tblFormaPagoDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblFormaPagoItem ObtenerFormaPago(long Id)
        {
            tblFormaPagoItem Item = new tblFormaPagoItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerFormaPago @id", Conexion);
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

        public List<tblFormaPagoItem> ObtenerFormaPagoLista()
        {
            List<tblFormaPagoItem> Lista = new List<tblFormaPagoItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerFormaPagoLista", Conexion);
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

        private tblFormaPagoItem ObtenerItem(SqlDataReader reader)
        {
            tblFormaPagoItem Item = new tblFormaPagoItem();
            Item.idFormaPago = short.Parse(reader["idFormaPago"].ToString());
            Item.nombre = reader["nombre"].ToString();
            return Item;
        }
    }
}
