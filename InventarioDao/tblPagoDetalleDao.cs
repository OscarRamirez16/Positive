using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblPagoDetalleDao
    {

        private SqlConnection Conexion { get; set; }

        public tblPagoDetalleDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public List<tblPagoDetalleItem> ObtenerDetallesPagoPorIDPago(long idPago)
        {
            List<tblPagoDetalleItem> Lista = new List<tblPagoDetalleItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPagoDetallePorIDPago @id", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", idPago));
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

        public List<tblPagoDetalleItem> ObtenerDetallesPagoProveedorPorIDPago(long idPago)
        {
            List<tblPagoDetalleItem> Lista = new List<tblPagoDetalleItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPagoDetallesProveedorPorIDPago @id", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", idPago));
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

        public tblPagoDetalleItem ObtenerPagoDetalle(long Id)
        {
            tblPagoDetalleItem Item = new tblPagoDetalleItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPagoDetalle @id", Conexion);
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

        public List<tblPagoDetalleItem> ObtenerPagoDetalleLista()
        {
            List<tblPagoDetalleItem> Lista = new List<tblPagoDetalleItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPagoDetalleLista", Conexion);
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

        private tblPagoDetalleItem ObtenerItem(SqlDataReader reader)
        {
            tblPagoDetalleItem Item = new tblPagoDetalleItem();
            Item.idPagoDetalle = long.Parse(reader["idPagoDetalle"].ToString());
            Item.idPago = long.Parse(reader["idPago"].ToString());
            Item.idDocumento = long.Parse(reader["idDocumento"].ToString());
            Item.NumeroDocumento = reader["NumeroDocumento"].ToString();
            Item.valorAbono = decimal.Parse(reader["valorAbono"].ToString());
            Item.formaPago = reader["formaPago"].ToString();
            return Item;
        }

        private bool Insertar(tblPagoDetalleItem Item, SqlConnection Con, SqlTransaction Tran, long TipoDocumento)
        {
            string SQL = "";
            if (TipoDocumento == 1 || TipoDocumento == 10)
            {
                SQL = "EXEC spInsertarPagoDetalle @idPago,@idDocumento,@valorAbono,@idTipoDocumento";
            }
            if (TipoDocumento == 2)
            {
                SQL = "EXEC spInsertarPagoCompraDetalle @idPago,@idDocumento,@valorAbono";
            }
            SqlCommand oSQL = new SqlCommand(SQL, Con, Tran);
            oSQL.Parameters.Add(new SqlParameter("@idPago", Item.idPago));
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", Item.idDocumento));
            oSQL.Parameters.Add(new SqlParameter("@valorAbono", Item.valorAbono));
            oSQL.Parameters.Add(new SqlParameter("@idTipoDocumento", TipoDocumento));
            try
            {
                Item.idPagoDetalle = long.Parse(oSQL.ExecuteScalar().ToString());
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

        public bool InsertarPagoCompraDetalle(tblPagoDetalleItem Item, SqlConnection Con, SqlTransaction Tran)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarPagoCompraDetalle @idPago,@idDocumento,@valorAbono", Con, Tran);
            oSQL.Parameters.Add(new SqlParameter("@idPago", Item.idPago));
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", Item.idDocumento));
            oSQL.Parameters.Add(new SqlParameter("@valorAbono", Item.valorAbono));
            try
            {
                Item.idPagoDetalle = long.Parse(oSQL.ExecuteScalar().ToString());
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

        private bool Actualizar(tblPagoDetalleItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarPagoDetalle @idPago,@idDocumento,@valorAbono", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idPago", Item.idPago));
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", Item.idDocumento));
            oSQL.Parameters.Add(new SqlParameter("@valorAbono", Item.valorAbono));
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

        public bool Guardar(tblPagoDetalleItem Item, SqlConnection Con, SqlTransaction Tran, long TipoDocumento)
        {
            if (Item.idPagoDetalle > 0)
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
