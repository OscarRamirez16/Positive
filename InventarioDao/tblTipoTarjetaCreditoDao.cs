using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using InventarioItem;

namespace InventarioDao
{
    public class tblTipoTarjetaCreditoDao
    {
        private SqlConnection Conexion { get; set; }
        public tblTipoTarjetaCreditoDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public tblTipoTarjetaCreditoItem ObtenerTipoTarjetaCredito(long Id, long idEmpresa)
        {
            tblTipoTarjetaCreditoItem Item = new tblTipoTarjetaCreditoItem();
            SqlCommand oSQL = new SqlCommand("spObtenerTipoTarjetaCredito", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", Id));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
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
        public List<tblTipoTarjetaCreditoItem> ObtenerTipoTarjetaCreditoLista(string Texto, long idEmpresa, bool Todos)
        {
            List<tblTipoTarjetaCreditoItem> Lista = new List<tblTipoTarjetaCreditoItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerTipoTarjetaCreditoLista", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@Texto", Texto));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@Todos", Todos));
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
        private tblTipoTarjetaCreditoItem ObtenerItem(SqlDataReader reader)
        {
            tblTipoTarjetaCreditoItem Item = new tblTipoTarjetaCreditoItem();
            Item.idTipoTarjetaCredito = short.Parse(reader["idTipoTarjetaCredito"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            Item.Descripcion = reader["Descripcion"].ToString();
            Item.CuentaContable = reader["CuentaContable"].ToString();
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            return Item;
        }
        private bool Insertar(tblTipoTarjetaCreditoItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarTipoTarjetaCredito", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idTipoTarjetaCredito", DBNull.Value));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Descripcion));
            oSQL.Parameters.Add(new SqlParameter("@CuentaContable", Item.CuentaContable));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            try
            {
                Conexion.Open();
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                Item.idTipoTarjetaCredito = short.Parse(long.Parse(((decimal)oSQL.ExecuteScalar()).ToString()).ToString());
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
        private bool Actualizar(tblTipoTarjetaCreditoItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarTipoTarjetaCredito", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idTipoTarjetaCredito", Item.idTipoTarjetaCredito));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Descripcion));
            oSQL.Parameters.Add(new SqlParameter("@CuentaContable", Item.CuentaContable));
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
        public bool Guardar(tblTipoTarjetaCreditoItem Item)
        {
            if (Item.idTipoTarjetaCredito > 0)
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
