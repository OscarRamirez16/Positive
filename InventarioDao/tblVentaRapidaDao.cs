using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;
using System.Data.SqlClient;

namespace InventarioDao
{
    public class tblVentaRapidaDao
    {
        private SqlConnection Conexion { get; set; }
        public tblVentaRapidaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public tblVentaRapidaItem ObtenerVentaRapida(long Id, long idEmpresa)
        {
            tblVentaRapidaItem Item = new tblVentaRapidaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerVentaRapida", Conexion);
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
        public List<tblVentaRapidaItem> ObtenerVentaRapidaLista(long idEmpresa,long idArticulo,bool Todos,long idTercero,long idBodega)
        {
            List<tblVentaRapidaItem> Lista = new List<tblVentaRapidaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerVentaRapidaLista", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idArticulo", idArticulo));
                oSQL.Parameters.Add(new SqlParameter("@Todos", Todos));
                oSQL.Parameters.Add(new SqlParameter("@idTercero", idTercero));
                oSQL.Parameters.Add(new SqlParameter("@idBodega", idBodega));
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
        public List<tblVentaRapidaItem> ObtenerVentaRapidaBusqueda(long idEmpresa, long idArticulo, string Texto)
        {
            List<tblVentaRapidaItem> Lista = new List<tblVentaRapidaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerVentaRapidaBusqueda", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idArticulo", idArticulo));
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
        private tblVentaRapidaItem ObtenerItem(SqlDataReader reader)
        {
            tblVentaRapidaItem Item = new tblVentaRapidaItem();
            Item.idVentaRapida = long.Parse(reader["idVentaRapida"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            Item.idArticulo = long.Parse(reader["idArticulo"].ToString());
            Item.Articulo = reader["Articulo"].ToString();
            if (reader["Imagen"] != DBNull.Value) {
                Item.Imagen = (byte[])reader["Imagen"];
            }
            Item.Cantidad = decimal.Parse(reader["Cantidad"].ToString());
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.Linea = reader["Linea"].ToString();
            Item.Precio = decimal.Parse(reader["Precio"].ToString());
            Item.ValorIVA = decimal.Parse(reader["ValorIVA"].ToString());
            Item.Stock = decimal.Parse(reader["Stock"].ToString());
            return Item;
        }

        private bool Insertar(tblVentaRapidaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarVentaRapida", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idVentaRapida", DBNull.Value));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@idArticulo", Item.idArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Imagen", Item.Imagen));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                Item.idVentaRapida = long.Parse(((decimal)oSQL.ExecuteScalar()).ToString());
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
        private bool Actualizar(tblVentaRapidaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarVentaRapida", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idVentaRapida", Item.idVentaRapida));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@idArticulo", Item.idArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Imagen", Item.Imagen));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
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
        public bool Guardar(tblVentaRapidaItem Item)
        {
            if (Item.idVentaRapida > 0)
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
