using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;
using System.Data.SqlClient;

namespace InventarioDao
{
    public class tblNumeracionFacturaVentaDao
    {
        private SqlConnection Conexion { get; set; }

        public tblNumeracionFacturaVentaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblNumeracionFacturaVentaItem ObtenerNumeracionFacturaVenta(long idEmpresa, long idUsuario)
        {
            tblNumeracionFacturaVentaItem Item = new tblNumeracionFacturaVentaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerNumeracionFacturaVenta", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
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

        private tblNumeracionFacturaVentaItem ObtenerItem(SqlDataReader reader)
        {
            tblNumeracionFacturaVentaItem Item = new tblNumeracionFacturaVentaItem();
            Item.idNumeracionFacturaVenta = long.Parse(reader["idNumeracionFacturaVenta"].ToString());
            Item.ValorInicial = reader["ValorInicial"].ToString();
            Item.ValorFinal = reader["ValorFinal"].ToString();
            Item.ProximoValor = reader["ProximoValor"].ToString();
            return Item;
        }

        public bool Guardar(tblNumeracionFacturaVentaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarNumeracionFacturaVenta @ValorInicial,@ValorFinal,@ProximoValor,@IdEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@ValorInicial", Item.ValorInicial));
            oSQL.Parameters.Add(new SqlParameter("@ValorFinal", Item.ValorFinal));
            oSQL.Parameters.Add(new SqlParameter("@ProximoValor", Item.ProximoValor));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", Item.idEmpresa));
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

        public bool Actualizar(long idEmpresa)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarNumeracionFacturaVenta @idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
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
    }
}
