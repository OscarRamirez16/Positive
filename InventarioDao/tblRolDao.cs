using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblRolDao
    {
        private SqlConnection Conexion { get; set; }
        public tblRolDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public tblRolItem ObtenerRol(long Id)
        {
            tblRolItem Item = new tblRolItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerRol @id", Conexion);
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
        public List<tblRolItem> ObtenerRolLista(long idEmpresa)
        {
            List<tblRolItem> Lista = new List<tblRolItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerRolLista @idEmpresa", Conexion);
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

        private tblRolItem ObtenerItem(SqlDataReader reader)
        {
            tblRolItem Item = new tblRolItem();
            Item.idRol = short.Parse(reader["idRol"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            return Item;
        }

        private long Insertar(tblRolItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarRol @Nombre,@idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            try
            {
                Conexion.Open();
                Item.idRol = short.Parse(oSQL.ExecuteScalar().ToString());
            }
            catch
            {
                return -1;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Item.idRol;
        }
        private long Actualizar(tblRolItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarRol @idRol,@Nombre,@idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idRol", Item.idRol));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            try
            {
                Conexion.Open();
                oSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Item.idRol;
        }

        public long Guardar(tblRolItem Item)
        {
            if (Item.idRol > 0)
            {
                return Actualizar(Item);
            }
            else
            {
                return Insertar(Item);
            }
        }

        public List<bsq_ObtenerRolLista> ObtenerRolListaNombreEmpresa(long idEmpresa)
        {
            List<bsq_ObtenerRolLista> Lista = new List<bsq_ObtenerRolLista>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerRolListaNombreEmpresa @idEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
            try
            {
                Conexion.Open();
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

        private bsq_ObtenerRolLista ObtenerItemFiltro(SqlDataReader reader)
        {
            bsq_ObtenerRolLista Item = new bsq_ObtenerRolLista();
            Item.idRol = short.Parse(reader["idRol"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            Item.Empresa = reader["Empresa"].ToString();
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            return Item;
        }
    }
}
