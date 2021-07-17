using InventarioItem;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioDao
{
    public class tblCampanaArticuloDao
    {
        private SqlConnection Conexion { get; set; }
        public tblCampanaArticuloDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public tblCampanaArticuloItem ObtenerCampanaArticulo(long Id, long idEmpresa)
        {
            tblCampanaArticuloItem Item = new tblCampanaArticuloItem();
            SqlCommand oSQL = new SqlCommand("spObtenerCampanaArticulo", Conexion);
            try
            {
                Conexion.Open();
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@id", Id));
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
        public List<tblCampanaArticuloItem> ObtenerCampanaArticuloLista(long idCampana,long idEmpresa)
        {
            List<tblCampanaArticuloItem> Lista = new List<tblCampanaArticuloItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerCampanaArticuloLista", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idCampana", idCampana));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
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
        private tblCampanaArticuloItem ObtenerItem(SqlDataReader reader)
        {
            tblCampanaArticuloItem Item = new tblCampanaArticuloItem();
            Item.idCampanaArticulo = long.Parse(reader["idCampanaArticulo"].ToString());
            Item.idCampana = long.Parse(reader["idCampana"].ToString());
            Item.Excluir = bool.Parse(reader["Excluir"].ToString());
            Item.TipoCampanaArticulo = short.Parse(reader["TipoCampanaArticulo"].ToString());
            Item.TipoCampanaArticuloNombre = reader["TipoCampanaArticuloNombre"].ToString();
            Item.Codigo = reader["Codigo"].ToString();
            Item.Id = reader["Id"].ToString();
            Item.Nombre = reader["Nombre"].ToString();
            Item.TipoDescuento = short.Parse(reader["TipoDescuento"].ToString());
            Item.TipoDescuentoNombre = reader["TipoDescuentoNombre"].ToString();
            Item.ValorDescuento = decimal.Parse(reader["ValorDescuento"].ToString());
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            return Item;
        }
        private bool Insertar(tblCampanaArticuloItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarCampanaArticulo", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idCampanaArticulo", DBNull.Value));
            oSQL.Parameters.Add(new SqlParameter("@idCampana", Item.idCampana));
            oSQL.Parameters.Add(new SqlParameter("@Excluir", Item.Excluir));
            oSQL.Parameters.Add(new SqlParameter("@TipoCampanaArticulo", Item.TipoCampanaArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@TipoDescuento", Item.TipoDescuento));
            oSQL.Parameters.Add(new SqlParameter("@ValorDescuento", Item.ValorDescuento));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            try
            {
                Conexion.Open();
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                Item.idCampanaArticulo = long.Parse(((decimal)oSQL.ExecuteScalar()).ToString());
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
        private bool Actualizar(tblCampanaArticuloItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarCampanaArticulo", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idCampanaArticulo", Item.idCampanaArticulo));
            oSQL.Parameters.Add(new SqlParameter("@idCampana", Item.idCampana));
            oSQL.Parameters.Add(new SqlParameter("@Excluir", Item.Excluir));
            oSQL.Parameters.Add(new SqlParameter("@TipoCampanaArticulo", Item.TipoCampanaArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@TipoDescuento", Item.TipoDescuento));
            oSQL.Parameters.Add(new SqlParameter("@ValorDescuento", Item.ValorDescuento));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
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
        public bool Guardar(tblCampanaArticuloItem Item)
        {
            if (Item.idCampanaArticulo > 0)
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
