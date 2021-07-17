using InventarioItem;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace InventarioDao
{
    public class tblCuentaCobroDao
    {
        private SqlConnection Conexion { get; set; }

        public tblCuentaCobroDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public string ObtenerNumeroCuentaCobro(long IdEmpresa)
        {
            string Numero = string.Empty;
            SqlCommand oSQL = new SqlCommand("spObtenerNumeroCuentaCobro", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                Numero = oSQL.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Numero = ex.Message;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Numero;
        }

        public tblCuentaCobroItem ObtenerCuentaCobro(long Id)
        {
            tblCuentaCobroItem Item = new tblCuentaCobroItem();
            SqlCommand oSQL = new SqlCommand("spObtenerCuentaCobro", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
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

        public List<tblCuentaCobroItem> ObtenerCuentaCobroLista(long IdEmpresa)
        {
            List<tblCuentaCobroItem> Lista = new List<tblCuentaCobroItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerCuentaCobroLista", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
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

        private tblCuentaCobroItem ObtenerItem(SqlDataReader reader)
        {
            tblCuentaCobroItem Item = new tblCuentaCobroItem();
            Item.Id = long.Parse(reader["Id"].ToString());
            Item.Numero = long.Parse(reader["Numero"].ToString());
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            Item.IdTercero = long.Parse(reader["IdTercero"].ToString());
            Item.IdUsuario = long.Parse(reader["IdUsuario"].ToString());
            Item.Concepto = reader["Concepto"].ToString();
            Item.Total = decimal.Parse(reader["Total"].ToString());
            Item.IdEstado = short.Parse(reader["IdEstado"].ToString());
            Item.IdEmpresa = long.Parse(reader["IdEmpresa"].ToString());
            return Item;
        }

        public string Insertar(tblCuentaCobroItem Item)
        {
            string Error = string.Empty;
            SqlCommand oSQL = new SqlCommand("spInsertarCuentaCobro", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@Numero", Item.Numero));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@IdTercero", Item.IdTercero));
            oSQL.Parameters.Add(new SqlParameter("@IdUsuario", Item.IdUsuario));
            oSQL.Parameters.Add(new SqlParameter("@Concepto", Item.Concepto));
            oSQL.Parameters.Add(new SqlParameter("@Total", Item.Total));
            oSQL.Parameters.Add(new SqlParameter("@IdEstado", Item.IdEstado));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", Item.IdEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@Saldo", Item.Saldo));
            try
            {
                Conexion.Open();
                oSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Error;
        }
    }
}
