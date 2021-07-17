using InventarioItem;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioDao
{
    public class tblGrupoClienteDao
    {
        private SqlConnection Conexion { get; set; }
        public tblGrupoClienteDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public tblGrupoClienteItem ObtenerGrupoCliente(long Id,long idEmpresa)
        {
            tblGrupoClienteItem Item = new tblGrupoClienteItem();
            SqlCommand oSQL = new SqlCommand("spObtenerGrupoCliente", Conexion);
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
        public List<tblGrupoClienteItem> ObtenerGrupoClienteLista(long idEmpresa,string Texto)
        {
            List<tblGrupoClienteItem> Lista = new List<tblGrupoClienteItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerGrupoClienteLista", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@Texto", Texto));
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
        private tblGrupoClienteItem ObtenerItem(SqlDataReader reader)
        {
            tblGrupoClienteItem Item = new tblGrupoClienteItem();
            Item.idGrupoCliente = long.Parse(reader["idGrupoCliente"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            if (reader["CuentaContable"] != DBNull.Value)
            {
                Item.CuentaContable = reader["CuentaContable"].ToString();
            }
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            return Item;
        }
        private bool Insertar(tblGrupoClienteItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarGrupoCliente", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idGrupoCliente", Item.idGrupoCliente));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@CuentaContable", Item.CuentaContable));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
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
        private bool Actualizar(tblGrupoClienteItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarGrupoCliente", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idGrupoCliente", Item.idGrupoCliente));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@CuentaContable", Item.CuentaContable));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
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
        public bool Guardar(tblGrupoClienteItem Item)
        {
            tblGrupoClienteItem oGCItem = ObtenerGrupoCliente(Item.idGrupoCliente,Item.idEmpresa);
            if (oGCItem != null && oGCItem.idGrupoCliente > 0)
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
