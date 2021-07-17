using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventarioItem;

namespace InventarioDao
{
    public class tblVendedorDao
    {
        private SqlConnection Conexion { get; set; }

        public tblVendedorDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblVendedorItem ObtenerVendedor(long Id)
        {
            tblVendedorItem Item = new tblVendedorItem();
            SqlCommand oSQL = new SqlCommand("spObtenerVendedor", Conexion);
            try
            {
                Conexion.Open();
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
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

        public List<tblVendedorItem> ObtenerVendedorListaActivos(long idEmpresa)
        {
            List<tblVendedorItem> Lista = new List<tblVendedorItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerVendedorListaActivos", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
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

        public List<tblVendedorItem> ObtenerVendedorLista(long idEmpresa)
        {
            List<tblVendedorItem> Lista = new List<tblVendedorItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerVendedorLista", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
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

        private tblVendedorItem ObtenerItem(SqlDataReader reader)
        {
            tblVendedorItem Item = new tblVendedorItem();
            Item.idVendedor = long.Parse(reader["idVendedor"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            Item.Comision = decimal.Parse(reader["Comision"].ToString());
            Item.idBodega = long.Parse(reader["idBodega"].ToString());
            Item.idUsuarioVendedor = long.Parse(reader["idUsuarioVendedor"].ToString());
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            return Item;
        }

        private bool Insertar(tblVendedorItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarVendedor", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idVendedor", DBNull.Value));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Comision", Item.Comision));
            if (Item.idBodega > 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.idBodega));
            }
            else {
                oSQL.Parameters.Add(new SqlParameter("@idBodega", DBNull.Value));
            }
            if (Item.idUsuarioVendedor > 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@idUsuarioVendedor", Item.idUsuarioVendedor));
            }
            else {
                oSQL.Parameters.Add(new SqlParameter("@idUsuarioVendedor", DBNull.Value));
            }
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            try
            {
                Conexion.Open();
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
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

        private bool Actualizar(tblVendedorItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarVendedor", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idVendedor", Item.idVendedor));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Comision", Item.Comision));
            if (Item.idBodega > 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.idBodega));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@idBodega", DBNull.Value));
            }
            if (Item.idUsuarioVendedor > 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@idUsuarioVendedor", Item.idUsuarioVendedor));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@idUsuarioVendedor", DBNull.Value));
            }
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            try
            {
                Conexion.Open();
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
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

        public bool Guardar(tblVendedorItem Item)
        {
            if (Item.idVendedor > 0)
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
