using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventarioItem;

namespace InventarioDao
{
    public class tblEmpresaDao
    {
        private SqlConnection Conexion { get; set; }

        public tblEmpresaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblEmpresaItem ObtenerEmpresa(long Id)
        {
            tblEmpresaItem Item = new tblEmpresaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerEmpresa", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
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
        public List<tblEmpresaItem> ObtenerEmpresaLista()
        {
            List<tblEmpresaItem> Lista = new List<tblEmpresaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerEmpresaLista", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
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

        private tblEmpresaItem ObtenerItem(SqlDataReader reader)
        {
            tblEmpresaItem Item = new tblEmpresaItem();
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.Nombre = reader["Nombre"].ToString();
            Item.idTipoIdentificacion = short.Parse(reader["idTipoIdentificacion"].ToString());
            Item.Identificacion = reader["Identificacion"].ToString();
            Item.Telefono = reader["Telefono"].ToString();
            Item.Direccion = reader["Direccion"].ToString();
            Item.idCiudad = short.Parse(reader["idCiudad"].ToString());
            Item.Ciudad = reader["Ciudad"].ToString();
            Item.TextoEncabezadoFactura = reader["TextoEncabezadoFactura"].ToString();
            Item.TextoPieFactura = reader["TextoPieFactura"].ToString();
            Item.InventarioNegativo = bool.Parse(reader["InventarioNegativo"].ToString());
            Item.MargenUtilidad = decimal.Parse(reader["MargenUtilidad"].ToString());
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.NumeroUsuarios = short.Parse(reader["NumeroUsuarios"].ToString());
            Item.ZonaHoraria = reader["ZonaHoraria"].ToString();
            Item.FacturacionCaja = bool.Parse(reader["FacturacionCaja"].ToString());
            Item.FechaInicialEntrega = DateTime.Parse(reader["FechaInicialEntrega"].ToString());
            Item.ManejaPrecioConIVA = bool.Parse(reader["ManejaPrecioConIVA"].ToString());
            Item.ManejaCostoConIVA = bool.Parse(reader["ManejaCostoConIVA"].ToString());
            Item.ManejaDescuentoConIVA = bool.Parse(reader["ManejaDescuentoConIVA"].ToString());
            Item.MultiBodega = bool.Parse(reader["MultiBodega"].ToString());
            Item.MostrarRemisiones = bool.Parse(reader["MostrarRemisiones"].ToString());
            Item.Propina = decimal.Parse(reader["Propina"].ToString());
            if (reader["Logo"] != DBNull.Value) {
                Item.Logo = (byte[])reader["Logo"];
            }
            Item.Impoconsumo = decimal.Parse(reader["Impoconsumo"].ToString());
            return Item;
        }

        private bool Insertar(tblEmpresaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarEmpresa", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@idTipoIdentificacion", Item.idTipoIdentificacion));
            oSQL.Parameters.Add(new SqlParameter("@Identificacion", Item.Identificacion));
            oSQL.Parameters.Add(new SqlParameter("@Telefono", Item.Telefono));
            oSQL.Parameters.Add(new SqlParameter("@Direccion", Item.Direccion));
            oSQL.Parameters.Add(new SqlParameter("@idCiudad", Item.idCiudad));
            oSQL.Parameters.Add(new SqlParameter("@TextoEncabezadoFactura", Item.TextoEncabezadoFactura));
            oSQL.Parameters.Add(new SqlParameter("@TextoPieFactura", Item.TextoPieFactura));
            //oSQL.Parameters.Add(new SqlParameter("@InventarioNegativo", Item.InventarioNegativo));
            oSQL.Parameters.Add(new SqlParameter("@MargenUtilidad", Item.MargenUtilidad));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@NumeroUsuarios", Item.NumeroUsuarios));
            oSQL.Parameters.Add(new SqlParameter("@ZonaHoraria", Item.ZonaHoraria));
            oSQL.Parameters.Add(new SqlParameter("@FacturacionCaja", Item.FacturacionCaja));
            oSQL.Parameters.Add(new SqlParameter("@FechaInicialEntrega", Item.FechaInicialEntrega));
            oSQL.Parameters.Add(new SqlParameter("@ManejaPrecioConIVA", Item.ManejaPrecioConIVA));
            oSQL.Parameters.Add(new SqlParameter("@ManejaCostoConIVA", Item.ManejaCostoConIVA));
            oSQL.Parameters.Add(new SqlParameter("@ManejaDescuentoConIVA", Item.ManejaDescuentoConIVA));
            oSQL.Parameters.Add(new SqlParameter("@MultiBodega", Item.MultiBodega));
            oSQL.Parameters.Add(new SqlParameter("@Impoconsumo", Item.Impoconsumo));
            if (Item.Logo != null)
            {
                oSQL.Parameters.Add(new SqlParameter("@Logo", Item.Logo));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Logo", DBNull.Value));
            }
            
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

        private bool Actualizar(tblEmpresaItem Item)
        {
            SqlCommand oSQL;
            if (Item.Logo != null)
            {
                oSQL = new SqlCommand("spActualizarEmpresaConLogo", Conexion);
                oSQL.Parameters.Add(new SqlParameter("@Logo", Item.Logo));
            }
            else
            {
                oSQL = new SqlCommand("spActualizarEmpresa", Conexion);
            }
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
            oSQL.Parameters.Add(new SqlParameter("@idTipoIdentificacion", Item.idTipoIdentificacion));
            oSQL.Parameters.Add(new SqlParameter("@Identificacion", Item.Identificacion));
            oSQL.Parameters.Add(new SqlParameter("@Telefono", Item.Telefono));
            oSQL.Parameters.Add(new SqlParameter("@Direccion", Item.Direccion));
            oSQL.Parameters.Add(new SqlParameter("@idCiudad", Item.idCiudad));
            oSQL.Parameters.Add(new SqlParameter("@TextoEncabezadoFactura", Item.TextoEncabezadoFactura));
            oSQL.Parameters.Add(new SqlParameter("@TextoPieFactura", Item.TextoPieFactura));
            //oSQL.Parameters.Add(new SqlParameter("@InventarioNegativo", Item.InventarioNegativo));
            oSQL.Parameters.Add(new SqlParameter("@MargenUtilidad", Item.MargenUtilidad));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@NumeroUsuarios", Item.NumeroUsuarios));
            oSQL.Parameters.Add(new SqlParameter("@ZonaHoraria", Item.ZonaHoraria));
            oSQL.Parameters.Add(new SqlParameter("@FacturacionCaja", Item.FacturacionCaja));
            oSQL.Parameters.Add(new SqlParameter("@FechaInicialEntrega", Item.FechaInicialEntrega));
            oSQL.Parameters.Add(new SqlParameter("@ManejaPrecioConIVA", Item.ManejaPrecioConIVA));
            oSQL.Parameters.Add(new SqlParameter("@ManejaCostoConIVA", Item.ManejaCostoConIVA));
            oSQL.Parameters.Add(new SqlParameter("@ManejaDescuentoConIVA", Item.ManejaDescuentoConIVA));
            oSQL.Parameters.Add(new SqlParameter("@MultiBodega", Item.MultiBodega));
            oSQL.Parameters.Add(new SqlParameter("@Impoconsumo", Item.Impoconsumo));
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

        public bool Guardar(tblEmpresaItem Item)
        {
            if (Item.idEmpresa > 0)
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
