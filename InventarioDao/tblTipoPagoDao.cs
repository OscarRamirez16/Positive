using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblTipoPagoDao
    {

        private SqlConnection Conexion { get; set; }

        public tblTipoPagoDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblTipoPagoItem ObtenerTipoPago(long Id)
        {
            tblTipoPagoItem Item = new tblTipoPagoItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerTipoPago @id", Conexion);
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

        public List<tblTipoPagoItem> ObtenerTipoPagoListaPorDocumento(long IdDocumento, long TipoDocumento)
        {
            List<tblTipoPagoItem> Lista = new List<tblTipoPagoItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerTipoPagoListaPorDocumento", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdDocumento", IdDocumento));
                oSQL.Parameters.Add(new SqlParameter("@TipoDocumento", TipoDocumento));
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

        public List<tblTipoPagoItem> ObtenerTipoPagoLista()
        {
            List<tblTipoPagoItem> Lista = new List<tblTipoPagoItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerTipoPagoLista", Conexion);
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

        private tblTipoPagoItem ObtenerItem(SqlDataReader reader)
        {
            tblTipoPagoItem Item = new tblTipoPagoItem();
            Item.idTipoPago = long.Parse(reader["idTipoPago"].ToString());
            Item.FormaPago = reader["FormaPago"].ToString();
            Item.idPago = long.Parse(reader["idPago"].ToString());
            Item.ValorPago = decimal.Parse(reader["ValorPago"].ToString());
            Item.idFormaPago = short.Parse(reader["idFormaPago"].ToString());
            Item.voucher = reader["voucher"].ToString();
            return Item;
        }

        private bool Insertar(tblTipoPagoItem Item, SqlConnection Con, SqlTransaction Tran, long TipoDocumento)
        {
            string SQL = "";
            if (TipoDocumento == 1 || TipoDocumento == 10)
            {
                SQL = "spInsertarTipoPago";
            }
            if (TipoDocumento == 2)
            {
                SQL = "spInsertarTipoPagoCompra";
            }
            SqlCommand oSQL = new SqlCommand(SQL, Con, Tran);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idPago", Item.idPago));
            oSQL.Parameters.Add(new SqlParameter("@ValorPago", Item.ValorPago));
            oSQL.Parameters.Add(new SqlParameter("@idFormaPago", Item.idFormaPago));
            if(string.IsNullOrEmpty(Item.voucher))
            {
                oSQL.Parameters.Add(new SqlParameter("@voucher", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@voucher", Item.voucher));
            }
            if (Item.idTipoTarjetaCredito == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@idTipoTarjetaCredito", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@idTipoTarjetaCredito", Item.idTipoTarjetaCredito));
            }
            try
            {
                Item.idTipoPago = long.Parse(oSQL.ExecuteScalar().ToString());
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
            return true;
        }

        public bool InsertarTipoPagoCompra(tblTipoPagoItem Item, SqlConnection Con, SqlTransaction Tran)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarTipoPagoCompra @idPago,@ValorPago,@idFormaPago,@voucher", Con, Tran);
            oSQL.Parameters.Add(new SqlParameter("@idPago", Item.idPago));
            oSQL.Parameters.Add(new SqlParameter("@ValorPago", Item.ValorPago));
            oSQL.Parameters.Add(new SqlParameter("@idFormaPago", Item.idFormaPago));
            oSQL.Parameters.Add(new SqlParameter("@voucher", Item.voucher));
            try
            {
                Item.idTipoPago = long.Parse(oSQL.ExecuteScalar().ToString());
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
            return true;
        }

        private bool Actualizar(tblTipoPagoItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarTipoPago @idTipoPago,@idPago,@ValorPago,@idFormaPago,@voucher", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idTipoPago", Item.idTipoPago));
            oSQL.Parameters.Add(new SqlParameter("@idPago", Item.idPago));
            oSQL.Parameters.Add(new SqlParameter("@ValorPago", Item.ValorPago));
            oSQL.Parameters.Add(new SqlParameter("@idFormaPago", Item.idFormaPago));
            oSQL.Parameters.Add(new SqlParameter("@voucher", Item.voucher));
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

        public bool Guardar(tblTipoPagoItem Item, SqlConnection Con, SqlTransaction Tran, long TipoDocumento)
        {
            if (Item.idTipoPago > 0)
            {
                return Actualizar(Item);
            }
            else
            {
                return Insertar(Item, Con, Tran, TipoDocumento);
            }
        }
    }
}
