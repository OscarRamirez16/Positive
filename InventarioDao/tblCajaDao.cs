using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventarioItem;

namespace InventarioDao
{
    public class tblCajaDao
    {
        private SqlConnection Conexion { get; set; }

        public tblCajaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public List<tblCajaItem> ObtenerCajaProximaVencer(long IdEmpresa)
        {
            List<tblCajaItem> Lista = new List<tblCajaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerCajaProximaVencer", Conexion);
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
        public tblCajaItem ObtenerCaja(long Id)
        {
            tblCajaItem Item = new tblCajaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerCaja", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdCaja", Id));
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

        public List<tblCajaItem> ObtenerCajaListaActivas(long IdEmpresa)
        {
            List<tblCajaItem> Lista = new List<tblCajaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerCajaListaActivas", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", IdEmpresa));
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

        public List<tblCajaItem> ObtenerCajaListaPorIdEmpresa(long IdEmpresa)
        {
            List<tblCajaItem> Lista = new List<tblCajaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerCajaLista", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", IdEmpresa));
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

        private tblCajaItem ObtenerItem(SqlDataReader reader)
        {
            tblCajaItem Item = new tblCajaItem();
            Item.idCaja = long.Parse(reader["idCaja"].ToString());
            Item.nombre = reader["nombre"].ToString();
            Item.idBodega = long.Parse(reader["idBodega"].ToString());
            Item.Bodega = reader["Bodega"].ToString();
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.ValorInicial = reader["ValorInicial"].ToString();
            Item.ValorFinal = reader["ValorFinal"].ToString();
            Item.ProximoValor = reader["ProximoValor"].ToString();
            Item.Resolucion = reader["Resolucion"].ToString();
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            if (reader["FechaVencimiento"].ToString() != "")
            {
                Item.FechaVencimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());
            }
            return Item;
        }

        private bool Insertar(tblCajaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarCaja", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idCaja", Item.idCaja));
            oSQL.Parameters.Add(new SqlParameter("@nombre", Item.nombre));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.idBodega));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@ValorInicial", Item.ValorInicial));
            oSQL.Parameters.Add(new SqlParameter("@ValorFinal", Item.ValorFinal));
            oSQL.Parameters.Add(new SqlParameter("@ProximoValor", Item.ProximoValor));
            oSQL.Parameters.Add(new SqlParameter("@Resolucion", Item.Resolucion));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            if(Item.FechaVencimiento == DateTime.MinValue)
            {
                oSQL.Parameters.Add(new SqlParameter("@FechaVencimiento", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@FechaVencimiento", Item.FechaVencimiento));
            }
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

        private bool Actualizar(tblCajaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarCaja", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idCaja", Item.idCaja));
            oSQL.Parameters.Add(new SqlParameter("@nombre", Item.nombre));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.idBodega));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@ValorInicial", Item.ValorInicial));
            oSQL.Parameters.Add(new SqlParameter("@ValorFinal", Item.ValorFinal));
            oSQL.Parameters.Add(new SqlParameter("@ProximoValor", Item.ProximoValor));
            oSQL.Parameters.Add(new SqlParameter("@Resolucion", Item.Resolucion));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            if (Item.FechaVencimiento == DateTime.MinValue)
            {
                oSQL.Parameters.Add(new SqlParameter("@FechaVencimiento", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@FechaVencimiento", Item.FechaVencimiento));
            }
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

        public bool Guardar(tblCajaItem Item)
        {
            if (Item.idCaja > 0)
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
