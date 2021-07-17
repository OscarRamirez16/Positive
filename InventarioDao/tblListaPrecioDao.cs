using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioItem;

namespace InventarioDao
{
    public class tblListaPrecioDao 
    { 
        private SqlConnection Conexion { get; set; } 
        
        public tblListaPrecioDao(string CadenaConexion) 
        { 
            Conexion = new SqlConnection(CadenaConexion); 
        } 

        public tblListaPrecioItem ObtenerListaPrecio(long IdListaPrecio) 
        { 
            tblListaPrecioItem Item = new tblListaPrecioItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerListaPrecio @IdListaPrecio", Conexion); 
            try 
            { 
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdListaPrecio", IdListaPrecio)); 
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
        
        public List<tblListaPrecioItem> ObtenerListaPrecioLista(long IdEmpresa) 
        { 
            List<tblListaPrecioItem> Lista = new List<tblListaPrecioItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerListaPrecioLista @IdEmpresa", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa)); 
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
        
        private tblListaPrecioItem ObtenerItem(SqlDataReader reader) 
        { 
            tblListaPrecioItem Item = new tblListaPrecioItem(); 
            Item.IdListaPrecio = long.Parse(reader["IdListaPrecio"].ToString()); 
            Item.Nombre = reader["Nombre"].ToString(); 
            Item.Factor = decimal.Parse(reader["Factor"].ToString()); 
            Item.IdEmpresa = long.Parse(reader["IdEmpresa"].ToString()); 
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.Aumento = bool.Parse(reader["Aumento"].ToString()); 
            return Item; 
        } 
        
        private bool Insertar(tblListaPrecioItem Item) 
        {
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarListaPrecio @Nombre,@Factor,@IdEmpresa,@Activo,@Aumento", Conexion); 
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre)); 
            oSQL.Parameters.Add(new SqlParameter("@Factor", Item.Factor)); 
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", Item.IdEmpresa)); 
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@Aumento", Item.Aumento)); 
            try 
            { 
                Conexion.Open(); 
                oSQL.ExecuteNonQuery(); 
            } 
            catch 
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
        
        private bool Actualizar(tblListaPrecioItem Item) 
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarListaPrecio @IdListaPrecio,@Nombre,@Factor,@IdEmpresa,@Activo,@Aumento", Conexion); 
            oSQL.Parameters.Add(new SqlParameter("@IdListaPrecio", Item.IdListaPrecio)); 
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre)); 
            oSQL.Parameters.Add(new SqlParameter("@Factor", Item.Factor)); 
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", Item.IdEmpresa)); 
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@Aumento", Item.Aumento)); 
            try 
            { 
                Conexion.Open(); 
                oSQL.ExecuteNonQuery(); 
            } 
            catch  
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
        
        public bool Guardar(tblListaPrecioItem Item) 
        { 
            if (Item.IdListaPrecio > 0) 
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
