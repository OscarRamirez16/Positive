using System;
using System.Collections.Generic;
using InventarioItem;
using System.Data.SqlClient;

namespace InventarioDao
{
    public class tblMovimientosDiariosDao
    {
        private SqlConnection Conexion { get; set; }

        public tblMovimientosDiariosDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public bool InsertarConTransaccion(tblMovimientosDiariosItem Item, SqlConnection oCon, SqlTransaction oTran)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spInsertarMovimientosDiarios", oCon, oTran);
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@fechaMovimiento", Item.fechaMovimiento));
                oSQL.Parameters.Add(new SqlParameter("@valor", Item.valor));
                oSQL.Parameters.Add(new SqlParameter("@observaciones", Item.observaciones));
                oSQL.Parameters.Add(new SqlParameter("@idTipoMovimiento", Item.idTipoMovimiento));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@EnCuadre", Item.EnCuadre));
                Item.idMovimiento = long.Parse(oSQL.ExecuteScalar().ToString());
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

        public decimal ObtenerPagosFacturasACredito(long IdCuadreCaja, long IdUsuario, long IdEmpresa)
        {
            decimal Valor = 0;
            SqlCommand oSQL = new SqlCommand("spObtenerPagosFacturasACredito", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdCuadreCaja", IdCuadreCaja));
                oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                Valor = decimal.Parse(oSQL.ExecuteScalar().ToString());
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
            return Valor;
        }

        public decimal ObtenerPropinaDocumento(long IdUsuario, long IdEmpresa)
        {
            decimal Valor = 0;
            SqlCommand oSQL = new SqlCommand("spObtenerPropinaDocumento", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                Valor = decimal.Parse(oSQL.ExecuteScalar().ToString());
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
            return Valor;
        }

        public decimal ObtenerValorRemisionesUsuario(long IdUsuario)
        {
            decimal Valor = 0;
            SqlCommand oSQL = new SqlCommand("spObtenerValorRemisionesUsuario", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                Valor = decimal.Parse(oSQL.ExecuteScalar().ToString());
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
            return Valor;
        }

        public List<tblMovimientosDiariosItem> ObtenerMovimientosDiariosCuadre(long IdEmpresa, long IdUsuario)
        {
            List<tblMovimientosDiariosItem> oListMov = new List<tblMovimientosDiariosItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerMovimientosDiariosCuadre", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", IdEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", IdUsuario));
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    tblMovimientosDiariosItem oMovI = new tblMovimientosDiariosItem();
                    oMovI.idTipoMovimiento = short.Parse(reader["idTipoMovimiento"].ToString());
                    oMovI.valor = decimal.Parse(reader["Valor"].ToString());
                    oListMov.Add(oMovI);
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
            return oListMov;
        }

        public decimal ObtenerMovimientosDiariosPorTipoMovimiento(tblMovimientosDiariosItem Item)
        {
            decimal valor;
            SqlCommand oSQL = new SqlCommand("spObtenerMovimientosDiariosPorTipoMovimiento", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idTipoMovimiento", Item.idTipoMovimiento));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
                object DecValue = oSQL.ExecuteScalar();
                if (DecValue != null)
                {
                    valor = decimal.Parse(DecValue.ToString());
                }
                else
                {
                    valor = 0;
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
            return valor;
        }

        public tblMovimientosDiariosItem ObtenerMovimientosDiarios(long Id)
        {
            tblMovimientosDiariosItem Item = new tblMovimientosDiariosItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerMovimientosDiarios @id", Conexion);
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

        public List<tblTipoMovimientoItem> ObtenerTipoMovimientoDiarioLista()
        {
            List<tblTipoMovimientoItem> Lista = new List<tblTipoMovimientoItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerTipoMovimientoDiarioLista", Conexion);
            try
            {
                Conexion.Open();
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(ObtenerItemTipoMovimiento(reader));
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

        public List<tblMovimientosDiariosItem> ObtenerMovimientosDiariosLista(DateTime Desde, DateTime Hasta, long idEmpresa, short TipoMovimiento, long idUsuario)
        {
            List<tblMovimientosDiariosItem> Lista = new List<tblMovimientosDiariosItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerMovimientosDiariosLista @Desde,@Hasta,@idEmpresa,@TipoMovimiento,@idUsuario", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@Desde", Desde));
                oSQL.Parameters.Add(new SqlParameter("@Hasta", Hasta));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@TipoMovimiento", TipoMovimiento));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
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

        private tblTipoMovimientoItem ObtenerItemTipoMovimiento(SqlDataReader reader)
        {
            tblTipoMovimientoItem Item = new tblTipoMovimientoItem();
            Item.idTipoMovimiento = short.Parse(reader["idTipoMovimiento"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            return Item;
        }

        private tblMovimientosDiariosItem ObtenerItem(SqlDataReader reader)
        {
            tblMovimientosDiariosItem Item = new tblMovimientosDiariosItem();
            Item.idMovimiento = long.Parse(reader["idMovimiento"].ToString());
            Item.fechaMovimiento = DateTime.Parse(reader["fechaMovimiento"].ToString());
            Item.valor = decimal.Parse(reader["valor"].ToString());
            Item.observaciones = reader["observaciones"].ToString();
            Item.idTipoMovimiento = short.Parse(reader["idTipoMovimiento"].ToString());
            Item.TipoMovimiento = reader["TipoMovimiento"].ToString();
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.Usuario = reader["Usuario"].ToString();
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            return Item;
        }

        private bool Insertar(tblMovimientosDiariosItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarMovimientosDiarios", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@fechaMovimiento", Item.fechaMovimiento));
            oSQL.Parameters.Add(new SqlParameter("@valor", Item.valor));
            oSQL.Parameters.Add(new SqlParameter("@observaciones", Item.observaciones));
            oSQL.Parameters.Add(new SqlParameter("@idTipoMovimiento", Item.idTipoMovimiento));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@EnCuadre", Item.EnCuadre));
            try
            {
                Conexion.Open();
                oSQL.ExecuteNonQuery();
            }
            catch(Exception ex)
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

        private bool Actualizar(tblMovimientosDiariosItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarMovimientosDiarios @idMovimiento,@fechaMovimiento,@valor,@observaciones,@idTipoMovimiento,@idUsuario,@idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idMovimiento", Item.idMovimiento));
            oSQL.Parameters.Add(new SqlParameter("@fechaMovimiento", Item.fechaMovimiento));
            oSQL.Parameters.Add(new SqlParameter("@valor", Item.valor));
            oSQL.Parameters.Add(new SqlParameter("@observaciones", Item.observaciones));
            oSQL.Parameters.Add(new SqlParameter("@idTipoMovimiento", Item.idTipoMovimiento));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
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

        public bool Guardar(tblMovimientosDiariosItem Item)
        {
            if (Item.idMovimiento > 0)
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
