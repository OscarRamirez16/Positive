using System;
using System.Collections.Generic;
using InventarioItem;
using System.Data.SqlClient;

namespace InventarioDao
{
    public class tblArticuloCodigoBarraDao
    {
        private SqlConnection Conexion { get; set; }

        public tblArticuloCodigoBarraDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public List<tblArticuloCodigoBarraItem> ObtenerArticuloCodigoBarraLista(long IdArticulo)
        {
            List<tblArticuloCodigoBarraItem> Lista = new List<tblArticuloCodigoBarraItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerArticuloCodigoBarraLista", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
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

        private tblArticuloCodigoBarraItem ObtenerItem(SqlDataReader reader)
        {
            tblArticuloCodigoBarraItem Item = new tblArticuloCodigoBarraItem();
            Item.IdCodigo = long.Parse(reader["IdCodigo"].ToString());
            Item.CodigoBarra = reader["CodigoBarra"].ToString();
            Item.IdArticulo = long.Parse(reader["IdArticulo"].ToString());
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.Descripcion = reader["Descripcion"].ToString();
            Item.IdEmpresa = long.Parse(reader["IdEmpresa"].ToString());
            return Item;
        }

        private bool Insertar(tblArticuloCodigoBarraItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarArticuloCodigoBarra", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@CodigoBarra", Item.CodigoBarra));
            oSQL.Parameters.Add(new SqlParameter("@IdArticulo", Item.IdArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Descripcion));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", Item.IdEmpresa));
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

        private bool Actualizar(tblArticuloCodigoBarraItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarArticuloCodigoBarra", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdCodigo", Item.IdCodigo));
            oSQL.Parameters.Add(new SqlParameter("@CodigoBarra", Item.CodigoBarra));
            oSQL.Parameters.Add(new SqlParameter("@IdArticulo", Item.IdArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Descripcion));
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

        public bool Guardar(tblArticuloCodigoBarraItem Item)
        {
            if (Item.IdCodigo > 0)
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
