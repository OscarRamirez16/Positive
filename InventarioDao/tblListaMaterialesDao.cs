using System;
using System.Collections.Generic;
using InventarioItem;
using System.Data.SqlClient;

namespace InventarioDao
{
    public class tblListaMaterialesDao
    {
        private SqlConnection Conexion { get; set; }

        public tblListaMaterialesDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public List<JSONItem> ObtenerListaMaterialesPorIdEmpresa(string Nombre, long IdEmpresa)
        {
            List<JSONItem> Lista = new List<JSONItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerListaMaterialesPorIdEmpresa", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(ObtenerItemFiltro(reader));
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

        private JSONItem ObtenerItemFiltro(SqlDataReader reader)
        {
            return new JSONItem(reader["IdListaMateriales"].ToString(), reader["Nombre"].ToString());
        }

        public tblListaMaterialesItem ObtenerListaMateriales(string Id)
        {
            tblListaMaterialesItem Item = new tblListaMaterialesItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerListaMateriales @id", Conexion);
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

        public List<tblListaMaterialesItem> ObtenerListaMaterialesPorFiltros(long IdArticulo, long IdUsuario, long IdEmpresa)
        {
            List<tblListaMaterialesItem> Lista = new List<tblListaMaterialesItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerListaMaterialesLista @IdArticulo,@IdUsuario,@IdEmpresa", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                if (IdArticulo == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdArticulo", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                }
                if (IdUsuario == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdUsuario", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                }
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

        private tblListaMaterialesItem ObtenerItem(SqlDataReader reader)
        {
            tblListaMaterialesItem Item = new tblListaMaterialesItem();
            Item.IdListaMateriales = reader["IdListaMateriales"].ToString();
            Item.IdArticulo = long.Parse(reader["IdArticulo"].ToString());
            Item.Articulo = reader["Articulo"].ToString();
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            Item.IdUsuario = long.Parse(reader["IdUsuario"].ToString());
            Item.Usuario = reader["Usuario"].ToString();
            Item.IdEmpresa = long.Parse(reader["IdEmpresa"].ToString());
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.Cantidad = decimal.Parse(reader["Cantidad"].ToString());
            return Item;
        }

        private bool Insertar(tblListaMaterialesItem Item, List<tblListaMaterialesDetalleItem> Detalles)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            SqlCommand oSQL = new SqlCommand("spInsertarListaMateriales", Conexion, oTran);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdArticulo", Item.IdArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@IdUsuario", Item.IdUsuario));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", Item.IdEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
            try
            {
                Item.IdListaMateriales = oSQL.ExecuteScalar().ToString();
                tblListaMaterialesDetalleDao oListMatDetD = new tblListaMaterialesDetalleDao(Conexion.ConnectionString);
                foreach (tblListaMaterialesDetalleItem Detalle in Detalles)
                {
                    Detalle.IdListaMateriales = Item.IdListaMateriales;
                    oListMatDetD.Insertar(Detalle, Conexion, oTran);
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

        private bool Actualizar(tblListaMaterialesItem Item, List<tblListaMaterialesDetalleItem> Detalles)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            SqlCommand oSQL = new SqlCommand("spActualizarListaMateriales", Conexion, oTran);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdListaMateriales", Item.IdListaMateriales));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
            try
            {
                oSQL.ExecuteNonQuery();
                tblListaMaterialesDetalleDao oListMatDetD = new tblListaMaterialesDetalleDao(Conexion.ConnectionString);
                oListMatDetD.Eliminar(Item.IdListaMateriales, Conexion, oTran);
                foreach (tblListaMaterialesDetalleItem Detalle in Detalles)
                {
                    Detalle.IdListaMateriales = Item.IdListaMateriales;
                    oListMatDetD.Insertar(Detalle, Conexion, oTran);
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

        public bool Guardar(tblListaMaterialesItem Item, List<tblListaMaterialesDetalleItem> Detalles)
        {
            if (!string.IsNullOrEmpty(Item.IdListaMateriales))
            {
                return Actualizar(Item, Detalles);
            }
            else
            {
                return Insertar(Item, Detalles);
            }
        }
    }
}
