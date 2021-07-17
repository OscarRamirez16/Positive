using InventarioItem;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioDao
{
    public class tblCampanaClienteDao
    {
        private SqlConnection Conexion { get; set; }
        public tblCampanaClienteDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public tblCampanaClienteItem ObtenerCampanaCliente(long Id,long idEmpresa)
        {
            tblCampanaClienteItem Item = new tblCampanaClienteItem();
            SqlCommand oSQL = new SqlCommand("spObtenerCampanaCliente", Conexion);
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
        public List<tblCampanaClienteItem> ObtenerCampanaClienteLista(long idCampana, long idEmpresa)
        {
            List<tblCampanaClienteItem> Lista = new List<tblCampanaClienteItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerCampanaClienteLista", Conexion);
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
        private tblCampanaClienteItem ObtenerItem(SqlDataReader reader)
        {
            tblCampanaClienteItem Item = new tblCampanaClienteItem();
            Item.idCampanaCliente = long.Parse(reader["idCampanaCliente"].ToString());
            Item.idCampana = long.Parse(reader["idCampana"].ToString());
            Item.Excluir = bool.Parse(reader["Excluir"].ToString());
            Item.TipoCampanaCliente = short.Parse(reader["TipoCampanaCliente"].ToString());
            Item.TipoCampanaClienteNombre = reader["TipoCampanaClienteNombre"].ToString();
            Item.Codigo = reader["Codigo"].ToString();
            Item.Nombre = reader["Nombre"].ToString();
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            return Item;
        }
        private bool Insertar(tblCampanaClienteItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarCampanaCliente", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idCampanaCliente", DBNull.Value));
            oSQL.Parameters.Add(new SqlParameter("@idCampana", Item.idCampana));
            oSQL.Parameters.Add(new SqlParameter("@Excluir", Item.Excluir));
            oSQL.Parameters.Add(new SqlParameter("@TipoCampanaCliente", Item.TipoCampanaCliente));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            try
            {
                Conexion.Open();
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                Item.idCampanaCliente = long.Parse(((decimal)oSQL.ExecuteScalar()).ToString());
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
        private bool Actualizar(tblCampanaClienteItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarCampanaCliente", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idCampanaCliente", Item.idCampanaCliente));
            oSQL.Parameters.Add(new SqlParameter("@idCampana", Item.idCampana));
            oSQL.Parameters.Add(new SqlParameter("@Excluir", Item.Excluir));
            oSQL.Parameters.Add(new SqlParameter("@TipoCampanaCliente", Item.TipoCampanaCliente));
            oSQL.Parameters.Add(new SqlParameter("@Codigo", Item.Codigo));
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
        public bool Guardar(tblCampanaClienteItem Item)
        {
            if (Item.idCampanaCliente > 0)
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
