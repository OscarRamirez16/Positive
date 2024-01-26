using System;
using System.Collections.Generic;
using InventarioItem;
using System.Data.SqlClient;
using System.Data;

namespace InventarioDao
{
    public class tblCuadreCajaDao
    {
        private SqlConnection Conexion { get; set; }

        public tblCuadreCajaDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public DataTable ObtenerValoresImpuestosAgrupados(long IdUsuario, long IdEmpresa)
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerValoresImpuestosAgrupados", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
            try
            {
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
                if (Conexion.State == ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return dt;
        }

        public int ObtenerNumeroFacturaDesde(long idEmpresa, long idUsuario)
        {
            int valor;
            SqlCommand oSQL = new SqlCommand("spObtenerNumeroFacturaDesde", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                object DecValue = oSQL.ExecuteScalar();
                if (DecValue != null)
                {
                    valor = int.Parse(DecValue.ToString());
                }
                else
                {
                    valor = 0;
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
            return valor;
        }

        public decimal ObtenerCreditos(tblCuadreCajaItem Item)
        {
            decimal valor;
            SqlCommand oSQL = new SqlCommand("spObtenerCreditos", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuarioCaja));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                object DecValue = oSQL.ExecuteScalar();
                if (DecValue != null)
                {
                    valor = decimal.Parse(DecValue.ToString());
                }
                else
                {
                    valor = 0;
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
            return valor;
        }

        public List<CuadreCajaItem> ObtenerTotalVentasCuadreConResumen(tblCuadreCajaItem Item)
        {
            List<CuadreCajaItem> lista = new List<CuadreCajaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerTotalVentasCuadreConResumen", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuarioCaja));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                SqlDataReader reader= oSQL.ExecuteReader();
                while (reader.Read())
                {
                    CuadreCajaItem oCCItem = new CuadreCajaItem();
                    oCCItem.Total = decimal.Parse(reader["Total"].ToString());
                    oCCItem.Devoluciones = decimal.Parse(reader["Devoluciones"].ToString());
                    oCCItem.idFormaPago = short.Parse(reader["idFormaPago"].ToString());
                    oCCItem.Nombre = reader["nombre"].ToString();
                    oCCItem.Valor = decimal.Parse(reader["Valor"].ToString());
                    lista.Add(oCCItem);
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
            return lista;
        }

        public List<tblTipoPagoItem> ObtenerFormasPagosVentas(long IdEmpresa, long IdUsuario)
        {
            List<tblTipoPagoItem> oListTipoPago = new List<tblTipoPagoItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerFormasPagosVentas", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", IdEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", IdUsuario));
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    tblTipoPagoItem oTipoI = new tblTipoPagoItem();
                    oTipoI.idFormaPago = short.Parse(reader["idFormaPago"].ToString());
                    oTipoI.ValorPago = decimal.Parse(reader["ValorPago"].ToString());
                    oListTipoPago.Add(oTipoI);
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
            return oListTipoPago;
        }

        public tblCuadreCajaItem ObtenerTotalVentasCuadre(tblCuadreCajaItem Item)
        {
            tblCuadreCajaItem oDatos = new tblCuadreCajaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerTotalVentasCuadre", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuarioCaja));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                SqlDataReader reader = oSQL.ExecuteReader();
                if (reader.Read())
                {
                    oDatos.Efectivo = decimal.Parse(reader["Efectivo"].ToString());
                    oDatos.TarjetaCredito = decimal.Parse(reader["TarjetaCredito"].ToString());
                    oDatos.TarjetaDebito = decimal.Parse(reader["TarjetaDebito"].ToString());
                    oDatos.Cheques = decimal.Parse(reader["Cheques"].ToString());
                    oDatos.Bonos = decimal.Parse(reader["Bonos"].ToString());
                    oDatos.Consignaciones = decimal.Parse(reader["Consignaciones"].ToString());
                    oDatos.DescuentosNomina = decimal.Parse(reader["DescuentoNomina"].ToString());
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
            return oDatos;
        }

        public decimal ObtenerTotalComprasCuadre(tblCuadreCajaItem Item)
        {
            decimal valor;
            SqlCommand oSQL = new SqlCommand("spObtenerTotalComprasCuadre", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuarioCaja));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                object DecValue = oSQL.ExecuteScalar();
                if (DecValue != null)
                {
                    valor = decimal.Parse(DecValue.ToString());
                }
                else
                {
                    valor = 0;
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
            return valor;
        }

        public tblCuadreCajaItem ObtenerCuadreCajaPorID(long Id)
        {
            tblCuadreCajaItem Item = new tblCuadreCajaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerCuadreCaja", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@Id", Id));
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

        public List<tblCuadreCajaItem> ObtenerCuadreCajaListaReporte(DateTime Desde, DateTime Hasta, long idEmpresa, short idCaja, long idUsuario)
        {
            List<tblCuadreCajaItem> Cuadre = new List<tblCuadreCajaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerCuadreCajaListaReporte", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@Desde", Desde));
                oSQL.Parameters.Add(new SqlParameter("@Hasta", Hasta));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                if (idCaja == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@idCaja", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@idCaja", idCaja));
                }
                if (idUsuario == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@idUsuario", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                }
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Cuadre.Add(ObtenerItem(reader));
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
            return Cuadre;
        }

        public tblCuadreCajaItem ObtenerCuadreCajaLista(tblCuadreCajaItem Item)
        {
            tblCuadreCajaItem Cuadre = new tblCuadreCajaItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerCuadreCajaLista @idEmpresa,@idCaja,@Estado", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idCaja", Item.idCaja));
                oSQL.Parameters.Add(new SqlParameter("@Estado", Item.Estado));
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Cuadre = ObtenerItem(reader);
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
            return Cuadre;
        }

        public long ValidarCajaAbierta(tblCuadreCajaItem Item)
        {
            long IdCuadreCaja = 0;
            tblCuadreCajaItem Cuadre = new tblCuadreCajaItem();
            SqlCommand oSQL = new SqlCommand("spValidarCajaAbierta", Conexion);
            try
            {
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                oSQL.CommandTimeout = 1000000;
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuarioCaja));
                oSQL.Parameters.Add(new SqlParameter("@Estado", Item.Estado));
                IdCuadreCaja = long.Parse(oSQL.ExecuteScalar().ToString());
            }
            catch
            {
                return IdCuadreCaja;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return IdCuadreCaja;
        }

        public tblCuadreCajaItem ObtenerCuadreCajaListaPorUsuario(tblCuadreCajaItem Item)
        {
            tblCuadreCajaItem Cuadre = new tblCuadreCajaItem();
            SqlCommand oSQL = new SqlCommand("spObtenerCuadreCajaListaPorUsuario", Conexion);
            try
            {
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                oSQL.CommandTimeout = 1000000;
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuarioCaja));
                oSQL.Parameters.Add(new SqlParameter("@Estado", Item.Estado));
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Cuadre = ObtenerItem(reader);
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
            return Cuadre;
        }

        private tblCuadreCajaItem ObtenerItem(SqlDataReader reader)
        {
            tblCuadreCajaItem Item = new tblCuadreCajaItem();
            Item.idCuadreCaja = long.Parse(reader["idCuadreCaja"].ToString());
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            Item.SaldoInicial = decimal.Parse(reader["SaldoInicial"].ToString());
            Item.TotalRetiros = decimal.Parse(reader["TotalRetiros"].ToString());
            Item.TotalIngresos = decimal.Parse(reader["TotalIngresos"].ToString());
            Item.Efectivo = decimal.Parse(reader["Efectivo"].ToString());
            Item.TarjetaCredito = decimal.Parse(reader["TarjetaCredito"].ToString());
            Item.TarjetaDebito = decimal.Parse(reader["TarjetaDebito"].ToString());
            Item.Cheques = decimal.Parse(reader["Cheques"].ToString());
            Item.Bonos = decimal.Parse(reader["Bonos"].ToString());
            Item.Consignaciones = decimal.Parse(reader["Consignaciones"].ToString());
            Item.DescuentosNomina = decimal.Parse(reader["DescuentoNomina"].ToString());
            Item.TotalVentas = decimal.Parse(reader["TotalVentas"].ToString());
            Item.TotalCompras = decimal.Parse(reader["TotalCompras"].ToString());
            Item.TotalCuadre = decimal.Parse(reader["TotalCuadre"].ToString());
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.Usuario = reader["Usuario"].ToString();
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.Observaciones = reader["Observaciones"].ToString();
            Item.idCaja = long.Parse(reader["idCaja"].ToString());
            Item.Caja = reader["Caja"].ToString();
            Item.idUsuarioCaja = long.Parse(reader["idUsuarioCaja"].ToString());
            Item.UsuarioCaja = reader["UsuarioCaja"].ToString();
            Item.idUsuarioCierre = long.Parse(reader["idUsuarioCierre"].ToString());
            Item.UsuarioCierre = reader["UsuarioCierre"].ToString();
            Item.Estado = bool.Parse(reader["Estado"].ToString());
            if (!string.IsNullOrEmpty(reader["FechaCierre"].ToString()))
            {
                Item.FechaCierre = DateTime.Parse(reader["FechaCierre"].ToString());
            }
            Item.TotalRemisiones = decimal.Parse(reader["TotalRemisiones"].ToString());
            Item.TotalCreditos = decimal.Parse(reader["TotalCreditos"].ToString());
            Item.PagoCreditos = decimal.Parse(reader["PagoCreditos"].ToString());
            return Item;
        }

        private bool Insertar(tblCuadreCajaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarCuadreCaja", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idCuadreCaja", Item.idCuadreCaja));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@SaldoInicial", Item.SaldoInicial));
            oSQL.Parameters.Add(new SqlParameter("@TotalRetiros", Item.TotalRetiros));
            oSQL.Parameters.Add(new SqlParameter("@TotalIngresos", Item.TotalIngresos));
            oSQL.Parameters.Add(new SqlParameter("@Efectivo", Item.Efectivo));
            oSQL.Parameters.Add(new SqlParameter("@TarjetaCredito", Item.TarjetaCredito));
            oSQL.Parameters.Add(new SqlParameter("@TarjetaDebito", Item.TarjetaDebito));
            oSQL.Parameters.Add(new SqlParameter("@Cheques", Item.Cheques));
            oSQL.Parameters.Add(new SqlParameter("@Bonos", Item.Bonos));
            oSQL.Parameters.Add(new SqlParameter("@Consignaciones", Item.Consignaciones));
            oSQL.Parameters.Add(new SqlParameter("@DescuentosNomina", Item.DescuentosNomina));
            oSQL.Parameters.Add(new SqlParameter("@TotalVentas", Item.TotalVentas));
            oSQL.Parameters.Add(new SqlParameter("@TotalCompras", Item.TotalCompras));
            oSQL.Parameters.Add(new SqlParameter("@TotalCuadre", Item.TotalCuadre));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@Observaciones", Item.Observaciones));
            oSQL.Parameters.Add(new SqlParameter("@idCaja", Item.idCaja));
            oSQL.Parameters.Add(new SqlParameter("@idUsuarioCaja", Item.idUsuarioCaja));
            oSQL.Parameters.Add(new SqlParameter("@Estado", Item.Estado));
            if (Item.FechaCierre == DateTime.MinValue)
            {
                oSQL.Parameters.Add(new SqlParameter("@FechaCierre", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@FechaCierre", Item.FechaCierre));
            }
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

        private bool Actualizar(tblCuadreCajaItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarCuadreCaja", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.CommandTimeout = 10000000;
            oSQL.Parameters.Add(new SqlParameter("@idCuadreCaja", Item.idCuadreCaja));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@SaldoInicial", Item.SaldoInicial));
            oSQL.Parameters.Add(new SqlParameter("@TotalRetiros", Item.TotalRetiros));
            oSQL.Parameters.Add(new SqlParameter("@TotalIngresos", Item.TotalIngresos));
            oSQL.Parameters.Add(new SqlParameter("@Efectivo", Item.Efectivo));
            oSQL.Parameters.Add(new SqlParameter("@TarjetaCredito", Item.TarjetaCredito));
            oSQL.Parameters.Add(new SqlParameter("@TarjetaDebito", Item.TarjetaDebito));
            oSQL.Parameters.Add(new SqlParameter("@Cheques", Item.Cheques));
            oSQL.Parameters.Add(new SqlParameter("@Bonos", Item.Bonos));
            oSQL.Parameters.Add(new SqlParameter("@Consignaciones", Item.Consignaciones));
            oSQL.Parameters.Add(new SqlParameter("@DescuentosNomina", Item.DescuentosNomina));
            oSQL.Parameters.Add(new SqlParameter("@TotalVentas", Item.TotalVentas));
            oSQL.Parameters.Add(new SqlParameter("@TotalCompras", Item.TotalCompras));
            oSQL.Parameters.Add(new SqlParameter("@TotalCuadre", Item.TotalCuadre));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@Observaciones", Item.Observaciones));
            oSQL.Parameters.Add(new SqlParameter("@idCaja", Item.idCaja));
            oSQL.Parameters.Add(new SqlParameter("@idUsuarioCaja", Item.idUsuarioCaja));
            oSQL.Parameters.Add(new SqlParameter("@Estado", Item.Estado));
            oSQL.Parameters.Add(new SqlParameter("@IdUsuarioCierre", Item.idUsuarioCierre));
            if (Item.FechaCierre == DateTime.MinValue)
            {
                oSQL.Parameters.Add(new SqlParameter("@FechaCierre", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@FechaCierre", Item.FechaCierre));
            }
            oSQL.Parameters.Add(new SqlParameter("@TotalRemisiones", Item.TotalRemisiones));
            oSQL.Parameters.Add(new SqlParameter("@TotalCreditos", Item.TotalCreditos));
            oSQL.Parameters.Add(new SqlParameter("@PagoCreditos", Item.PagoCreditos));
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

        public bool Guardar(tblCuadreCajaItem Item)
        {
            if (Item.idCuadreCaja > 0)
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
