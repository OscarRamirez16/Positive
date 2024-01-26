using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblTrasladoMercanciaDao
    {
        private SqlConnection Conexion { get; set; }

        public tblTrasladoMercanciaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public DataTable ObtenerTrasladoMercancia(DateTime FechaInicial, DateTime FechaFinal, long IdUsuario, long IdEmpresa, bool Agrupado)
        {
            DataTable dt = new DataTable();
            string sp = "spObtenerTrasladoMercancia";
            if (Agrupado)
            {
                sp = "spObtenerTrasladoMercanciaAgrupado";
            }
            SqlCommand oSQL = new SqlCommand(sp, Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
            oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
            if(IdUsuario == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@IdUsuario", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
            }
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
            try
            {
                Conexion.Open();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSQL);
                oSqlDataAdapter.Fill(dt);
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
            return dt;
        }
        public bool Guardar(tblTrasladoMercanciaItem Item, List<tblTrasladoMercanciaDetalle> oListTras)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            SqlCommand oSQL = new SqlCommand("spInsertarTrasladoMercancia", Conexion, oTran);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", Item.IdEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@IdUsuario", Item.IdUsuario));
            if (!string.IsNullOrEmpty(Item.Obervaciones))
            {
                oSQL.Parameters.Add(new SqlParameter("@Observaciones", Item.Obervaciones));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Observaciones", DBNull.Value));
            }
            try
            {
                Item.IdTraslado = long.Parse(oSQL.ExecuteScalar().ToString());
                foreach (tblTrasladoMercanciaDetalle Detalle in oListTras)
                {
                    Detalle.IdTraslado = Item.IdTraslado;
                    InsertarDetalleTraslado(Detalle, Conexion, oTran);
                }
                oTran.Commit();
            }
            catch
            {
                oTran.Rollback();
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

        public void InsertarDetalleTraslado(tblTrasladoMercanciaDetalle Item, SqlConnection oCon, SqlTransaction oTran)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarTrasladoMercanciaDetalle", oCon, oTran);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdTraslado", Item.IdTraslado));
            oSQL.Parameters.Add(new SqlParameter("@IdArticulo", Item.IdArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
            oSQL.Parameters.Add(new SqlParameter("@IdBodegaOrigen", Item.IdBodegaOrigen));
            oSQL.Parameters.Add(new SqlParameter("@IdBodegaDestino", Item.IdBodegaDestino));
            try
            {
                oSQL.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
