using InventarioItem;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioDao
{
    public class CotizacionCocinaDao
    {
        private SqlConnection Conexion { get; set; }
        public CotizacionCocinaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

		public List<CotizacionCocinaItem> ObtenerCotizacionCocinaLista(long idEmpresa)
		{
			List<CotizacionCocinaItem> Lista = new List<CotizacionCocinaItem>();
			SqlCommand oSQL = new SqlCommand("EXEC spObtenerCotizacionCocinaLista @idEmpresa", Conexion);
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

		private CotizacionCocinaItem ObtenerItem(SqlDataReader reader)
		{
			CotizacionCocinaItem Item = new CotizacionCocinaItem();
			Item.idCotizacion = long.Parse(reader["idDocumento"].ToString());
			Item.idArticulo = long.Parse(reader["idArticulo"].ToString());
			Item.Cantidad = decimal.Parse(reader["Cantidad"].ToString());
			Item.CodigoArticulo = reader["CodigoArticulo"].ToString();
			Item.Nombre = reader["Nombre"].ToString();
			Item.Vendedor = reader["Vendedor"].ToString();
			Item.Observaciones = reader["Observaciones"].ToString();
			Item.NumeroDocumento = reader["NumeroDocumento"].ToString();
			Item.Usuario = reader["Usuario"].ToString();
			return Item;
		}

		private bool Insertar(CotizacionCocinaItem Item)
		{
			SqlCommand oSQL = new SqlCommand("EXEC spInsertarCotizacionCocina @idCotizacionCocina,@idCotizacion,@idArticulo,@Cantidad,@Fecha", Conexion);
			oSQL.Parameters.Add(new SqlParameter("@idCotizacionCocina", Item.idCotizacionCocina));
			oSQL.Parameters.Add(new SqlParameter("@idCotizacion", Item.idCotizacion));
			oSQL.Parameters.Add(new SqlParameter("@idArticulo", Item.idArticulo));
			oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
			oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
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
		public bool Eliminar(long idEmpresa)
		{
			SqlCommand oSQL = new SqlCommand("EXEC spEliminarCotizacionCocina @idEmpresa", Conexion);
			oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
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
		public bool Guardar(CotizacionCocinaItem Item)
		{
			return Insertar(Item);
		}
	}
}
