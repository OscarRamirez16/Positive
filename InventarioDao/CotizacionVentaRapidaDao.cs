using InventarioItem;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioDao
{
    public class CotizacionVentaRapidaDao { 
        private SqlConnection Conexion { get; set; } 
        public CotizacionVentaRapidaDao(string CadenaConexion) { 
            Conexion = new SqlConnection(CadenaConexion); 
        } 
        public List<CotizacionVentaRapidaItem> ObtenerCotizacionVentaRapida(long idCotizacion, long idEmpresa) {
            List<CotizacionVentaRapidaItem> lista = new List<CotizacionVentaRapidaItem>(); 
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerCotizacionVentaRapida @idCotizacion, @idEmpresa", Conexion); 
            try { Conexion.Open(); 
                oSQL.Parameters.Add(new SqlParameter("@idCotizacion", idCotizacion)); 
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                SqlDataReader reader = oSQL.ExecuteReader(); 
                while (reader.Read()) {
                    lista.Add(ObtenerItem(reader)); 
                } 
            } 
            catch (Exception ex) { 
                throw ex; 
            } 
            finally { 
                if (Conexion.State == System.Data.ConnectionState.Open) { 
                    Conexion.Close(); 
                } 
            } 
            return lista; 
        } 
        public List<PedidoAbierto> ObtenerCotizacionVentaRapidaLista(long idUsuario) { 
            List<PedidoAbierto> Lista = new List<PedidoAbierto>(); 
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerCotizacionVentaRapidaLista @idUsuario", Conexion); 
            try { 
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                SqlDataReader reader = oSQL.ExecuteReader(); 
                while (reader.Read()) {
                    PedidoAbierto item = new PedidoAbierto() {
                        Fecha = DateTime.Parse(reader["Fecha"].ToString()),
                        idDocumento = long.Parse(reader["idDocumento"].ToString()),
                        Nombre = reader["Nombre"].ToString(),
                        IdVendedor = reader["IdVendedor"]==DBNull.Value ? 0 :  long.Parse(reader["IdVendedor"].ToString()),
                        NumeroDocumento = reader["NumeroDocumento"].ToString(),
                        Observaciones = reader["Observaciones"].ToString()
                    };
                    Lista.Add(item); 
                } 
            } 
            catch (Exception ex) { 
                throw ex; 
            } 
            finally { 
                if (Conexion.State == System.Data.ConnectionState.Open) { 
                    Conexion.Close(); 
                } 
            } 
            return Lista; 
        } 
        private CotizacionVentaRapidaItem ObtenerItem(SqlDataReader reader) { 
            CotizacionVentaRapidaItem Item = new CotizacionVentaRapidaItem(); 
            Item.idCotizacionVentaRapida = long.Parse(reader["idCotizacionVentaRapida"].ToString()); 
            Item.idCotizacion = long.Parse(reader["idCotizacion"].ToString()); 
            Item.idVentaRapida = long.Parse(reader["idVentaRapida"].ToString());
            Item.Articulo = long.Parse(reader["Articulo"].ToString()); 
            Item.Descripcion = reader["Descripcion"].ToString(); 
            Item.Cantidad = decimal.Parse(reader["Cantidad"].ToString()); 
            Item.ValorIVA = decimal.Parse(reader["ValorIVA"].ToString()); 
            Item.Precio = decimal.Parse(reader["Precio"].ToString()); 
            Item.Impoconsumo = decimal.Parse(reader["Impoconsumo"].ToString()); 
            return Item; 
        } 
        private bool Insertar(CotizacionVentaRapidaItem Item) { 
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarCotizacionVentaRapida @idCotizacionVentaRapida,@idCotizacion,@Articulo,@Descripcion,@Cantidad,@ValorIVA,@Precio,@Impoconsumo", Conexion); 
            oSQL.Parameters.Add(new SqlParameter("@idCotizacionVentaRapida", Item.idCotizacionVentaRapida)); 
            oSQL.Parameters.Add(new SqlParameter("@idCotizacion", Item.idCotizacion)); 
            oSQL.Parameters.Add(new SqlParameter("@Articulo", Item.Articulo)); 
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Descripcion)); 
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad)); 
            oSQL.Parameters.Add(new SqlParameter("@ValorIVA", Item.ValorIVA)); 
            oSQL.Parameters.Add(new SqlParameter("@Precio", Item.Precio)); 
            oSQL.Parameters.Add(new SqlParameter("@Impoconsumo", Item.Impoconsumo)); 
            try { 
                Conexion.Open(); 
                oSQL.ExecuteNonQuery(); 
            } 
            catch (Exception ex) { 
                return false; 
            } 
            finally { 
                if (Conexion.State == System.Data.ConnectionState.Open) { 
                    Conexion.Close(); 
                } 
            } 
            return true; 
        }
        public bool Eliminar(CotizacionVentaRapidaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spEliminarCotizacionVentaRapida @idCotizacion", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idCotizacion", Item.idCotizacion));
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
        public bool Guardar(CotizacionVentaRapidaItem Item) {
            return Insertar(Item);
        }
    }
}
