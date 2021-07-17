using InventarioItem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioDao
{
    public class tblCampanaDao
    {
        private SqlConnection Conexion { get; set; }
        public tblCampanaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public tblCampanaItem ObtenerCampana(long Id,long idEmpresa)
        {
            tblCampanaItem Item = new tblCampanaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerCampana", Conexion);
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
        public DataTable ObtenerCampanaLista(string IdCampana, string Nombre, int Estado, string IdArticulo, long idEmpresa)
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerCampanaLista", Conexion);
            try
            {
                if (string.IsNullOrEmpty(IdCampana))
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdCampana", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdCampana", IdCampana));
                }
                oSQL.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                oSQL.Parameters.Add(new SqlParameter("@Estado", Estado));
                if (string.IsNullOrEmpty(IdArticulo))
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdArticulo", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdArticulo", IdArticulo));
                }
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                Conexion.Open();
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(oSQL);
                oSqlDataAdapter.Fill(dt);
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
            return dt;
        }
        private tblCampanaItem ObtenerItem(SqlDataReader reader)
        {
            tblCampanaItem Item = new tblCampanaItem();
            Item.idCampana = long.Parse(reader["idCampana"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            Item.Observacion = reader["Observacion"].ToString();
            Item.FechaInicial = DateTime.Parse(reader["FechaInicial"].ToString());
            Item.FechaFinal = DateTime.Parse(reader["FechaFinal"].ToString());
            if (reader["HoraInicial"] != DBNull.Value)
            {
                Item.HoraInicial = DateTime.Parse(reader["HoraInicial"].ToString());
            }
            if (reader["HoraFinal"] != DBNull.Value)
            {
                Item.HoraFinal = DateTime.Parse(reader["HoraFinal"].ToString());
            }
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            return Item;
        }
        private bool Insertar(tblCampanaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarCampana", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idCampana", DBNull.Value));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Observacion", Item.Observacion));
            oSQL.Parameters.Add(new SqlParameter("@FechaInicial", Item.FechaInicial));
            oSQL.Parameters.Add(new SqlParameter("@FechaFinal", Item.FechaFinal));
            if (Item.HoraInicial == DateTime.MinValue)
            {
                oSQL.Parameters.Add(new SqlParameter("@HoraInicial", DBNull.Value));
            }
            else {
                oSQL.Parameters.Add(new SqlParameter("@HoraInicial", Item.HoraInicial));
            }
            if (Item.HoraFinal == DateTime.MinValue)
            {
                oSQL.Parameters.Add(new SqlParameter("@HoraFinal", DBNull.Value));
            }
            else {
                oSQL.Parameters.Add(new SqlParameter("@HoraFinal", Item.HoraFinal));
            }
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            try
            {
                Conexion.Open();
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                Item.idCampana = long.Parse(((decimal)oSQL.ExecuteScalar()).ToString());
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
        private bool Actualizar(tblCampanaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarCampana", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idCampana", Item.idCampana));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@Observacion", Item.Observacion));
            oSQL.Parameters.Add(new SqlParameter("@FechaInicial", Item.FechaInicial));
            oSQL.Parameters.Add(new SqlParameter("@FechaFinal", Item.FechaFinal));
            oSQL.Parameters.Add(new SqlParameter("@HoraInicial", Item.HoraInicial));
            oSQL.Parameters.Add(new SqlParameter("@HoraFinal", Item.HoraFinal));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
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
        public bool Guardar(tblCampanaItem Item)
        {
            if (Item.idCampana > 0)
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
