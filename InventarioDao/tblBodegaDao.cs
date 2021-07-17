using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;
using System.Data;

namespace InventarioDao
{
    public class tblBodegaDao
    {
        private SqlConnection Conexion { get; set; }

        public tblBodegaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public DataTable ObtenerConfiguracionBodegasPorArticulo(long IdArticulo, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerConfiguracionBodegasPorArticulo", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }

        public tblBodegaItem ObtenerBodega(long idBodega, long idEmpresa)
        {
            tblBodegaItem Item = new tblBodegaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerBodega", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idBodega", idBodega));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
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

        public DataTable ObtenerListaBodegaArticuloDisponible(long IdArticulo)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerListaBodegaArticuloDisponible", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }

        public DataTable ObtenerBodegaPorDescripcionEmpresa(string Nombre, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL;
                oSQL = new SqlCommand("spObtenerBodegaPorDescripcionEmpresa", Conexion);
                oSQL.CommandTimeout = 200000;
                oSQL.CommandType = CommandType.StoredProcedure;
                if (string.IsNullOrEmpty(Nombre))
                {
                    oSQL.Parameters.Add(new SqlParameter("@Nombre", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                }
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }

        public List<tblBodegaItem> ObtenerBodegaLista(long idEmpresa)
        {
            List<tblBodegaItem> Lista = new List<tblBodegaItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerBodegaLista @idEmpresa", Conexion);
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

        private tblBodegaItem ObtenerItem(SqlDataReader reader)
        {
            tblBodegaItem Item = new tblBodegaItem();
            Item.IdBodega = long.Parse(reader["idBodega"].ToString());
            Item.Descripcion = reader["Descripcion"].ToString();
            Item.Direccion = reader["Direccion"].ToString();
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            return Item;
        }

        private bool Insertar(tblBodegaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarBodega @Descripcion,@Direccion,@idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Descripcion));
            oSQL.Parameters.Add(new SqlParameter("@Direccion", Item.Direccion));
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

        private bool Actualizar(tblBodegaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarBodega @idBodega,@Descripcion,@Direccion,@idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.IdBodega));
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Descripcion));
            oSQL.Parameters.Add(new SqlParameter("@Direccion", Item.Direccion));
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

        public bool Guardar(tblBodegaItem Item)
        {
            if (Item.IdBodega > 0)
            {
                return Actualizar(Item);
            }
            else
            {
                return Insertar(Item);
            }
        }

        public List<JSONItem> ObtenerBodegaListaPorNombreCantidad(string nombre, long idEmpresa, long idArticulo)
        {
            List<JSONItem> Lista = new List<JSONItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerBodegaListaPorNombreCantidad @nombre,@idEmpresa,@idArticulo", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add("@nombre", nombre);
                oSQL.Parameters.Add("@idEmpresa", idEmpresa);
                oSQL.Parameters.Add("@idArticulo", idArticulo);
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

        public List<JSONItem> ObtenerBodegaListaPorNombre(string nombre, long idEmpresa)
        {
            List<JSONItem> Lista = new List<JSONItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerBodegaListaPorNombre @nombre,@idEmpresa", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add("@nombre", nombre);
                oSQL.Parameters.Add("@idEmpresa", idEmpresa);
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
            return new JSONItem(reader["idBodega"].ToString(), reader["Descripcion"].ToString());
        }
    }
}
