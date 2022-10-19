using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventarioItem;
using System.Data;

namespace InventarioDao
{
    public class tblDocumentoDao
    {
        private SqlConnection Conexion { get; set; }

        public enum TipoDocumentoEnum
        {
            Venta = 1,
            Compra = 2,
            Cotizaciones = 3,
            NotaCreditoVenta = 4,
            EntradaMercancia = 5,
            SalidaMercancia = 6,
            NotaCreditoCompra = 7,
            Remision = 8,
            TrasladoMercancia = 9,
            CuentaCobro = 10,
            FacturaElectronica = 11
        }
        public DataTable ObtenerCuentaCobro(DateTime FechaInicial, DateTime FechaFinal, long IdUsuario, long IdTercero, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerCuentasCobro", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                if (IdUsuario == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdUsuario", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                }
                if (IdTercero == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdTercero", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdTercero", IdTercero));
                }
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", IdEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public tblDocumentoDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public DataTable ObtenerFacturasPendientesPorPago(long IdTercero)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerFacturasPendientesPorPago", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdTercero", IdTercero));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public DataTable ObtenerComisionesVentasPorArticulo(DateTime FechaInicial, DateTime FechaFinal, long IdVendedor, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerComisionesVentasPorArticulo", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                if(IdVendedor == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdVendedor", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdVendedor", IdVendedor));
                }
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", IdEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public DataTable ObtenerComisionesVentasPorArticuloAgrupadoPorVendedor(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerComisionesVentasPorArticuloAgrupadoPorVendedor", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", IdEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public DataTable ObtenerFacturasCompra(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerFacturasCompras", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", IdEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public bool ActualizarCotizacion(tblDocumentoItem oDocItem, List<tblDetalleDocumentoItem> oListDet)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                SqlCommand oSQL = new SqlCommand("spActualizarCotizacion", Conexion, oTran);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdDocumento", oDocItem.idDocumento));
                oSQL.Parameters.Add(new SqlParameter("@Telefono", oDocItem.Telefono));
                oSQL.Parameters.Add(new SqlParameter("@Direccion", oDocItem.Direccion));
                oSQL.Parameters.Add(new SqlParameter("@Observaciones", oDocItem.Observaciones));
                oSQL.Parameters.Add(new SqlParameter("@TotalDocumento", oDocItem.TotalDocumento));
                oSQL.Parameters.Add(new SqlParameter("@TotalAntesIVA", oDocItem.TotalAntesIVA));
                oSQL.Parameters.Add(new SqlParameter("@TotalDescuento", oDocItem.TotalDescuento));
                oSQL.Parameters.Add(new SqlParameter("@TotalIVA", oDocItem.TotalIVA));
                oSQL.Parameters.Add(new SqlParameter("@Propina", oDocItem.Propina));
                oSQL.ExecuteNonQuery();
                SqlCommand oSQL1 = new SqlCommand("spEliminarCotizacionDetalles", Conexion, oTran);
                oSQL1.CommandType = CommandType.StoredProcedure;
                oSQL1.Parameters.Add(new SqlParameter("@IdDocumento", oDocItem.idDocumento));
                oSQL1.ExecuteNonQuery();
                foreach (tblDetalleDocumentoItem Detalle in oListDet)
                {
                    SqlCommand oSQL2 = new SqlCommand("spInsertarCotizacionDetalles", Conexion, oTran);
                    oSQL2.CommandType = CommandType.StoredProcedure;
                    oSQL2.Parameters.Add(new SqlParameter("@idDocumento", oDocItem.idDocumento));
                    oSQL2.Parameters.Add(new SqlParameter("@NumeroLinea", Detalle.NumeroLinea));
                    oSQL2.Parameters.Add(new SqlParameter("@idArticulo", Detalle.idArticulo));
                    oSQL2.Parameters.Add(new SqlParameter("@Descripcion", Detalle.Articulo));
                    oSQL2.Parameters.Add(new SqlParameter("@Precio", Detalle.ValorUnitario));
                    oSQL2.Parameters.Add(new SqlParameter("@Impuesto", Detalle.IVA));
                    oSQL2.Parameters.Add(new SqlParameter("@Cantidad", Detalle.Cantidad));
                    oSQL2.Parameters.Add(new SqlParameter("@idBodega", Detalle.idBodega));
                    oSQL2.Parameters.Add(new SqlParameter("@Descuento", Detalle.Descuento));
                    oSQL2.Parameters.Add(new SqlParameter("@CostoPonderado", Detalle.CostoPonderado));
                    oSQL2.ExecuteNonQuery();
                }
                oTran.Commit();
                return true;
            }
            catch
            {
                oTran.Rollback();
                return false;
            }
        }
        public bool EliminarCotizacionDetalle(long idDocumento)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                SqlCommand oSQL1 = new SqlCommand("spEliminarCotizacionDetalles", Conexion, oTran);
                oSQL1.CommandType = CommandType.StoredProcedure;
                oSQL1.Parameters.Add(new SqlParameter("@IdDocumento", idDocumento));
                oSQL1.ExecuteNonQuery();
                oTran.Commit();
                return true;
            }
            catch
            {
                oTran.Rollback();
                return false;
            }
        }
        public tblDocumentoItem ObtenerDevolucionPorReferencia(string NumeroFactura, long IdEmpresa)
        {
            tblDocumentoItem Item = new tblDocumentoItem();
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerDevolucionPorReferencia", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@NumeroFactura", NumeroFactura));
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                Conexion.Open();
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
        public tblDocumentoItem ObtenerFacturaVentaPorNumero(string NumeroFactura, string Nit, long IdEmpresa)
        {
            tblDocumentoItem Item = new tblDocumentoItem();
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerFacturaVentaPorNumero", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@NumeroFactura", NumeroFactura));
                oSQL.Parameters.Add(new SqlParameter("@Nit", Nit));
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                Conexion.Open();
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
        public DataTable ObtenerVentasDevolucionesPorFecha(DateTime FechaInicial, DateTime FechaFinal, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL;
                oSQL = new SqlCommand("spObtenerVentasDevolucionesDetalladas", Conexion);
                oSQL.CommandTimeout = 200000;
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public DataTable ObtenerSaldoDocumentoAFavorClienteTodos(long idTercero, long idEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerSaldoDocumentoAFavorClienteTodos", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@idTercero", idTercero));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public DataTable ObtenerSaldoDocumentoAFavorCliente(long idTercero, long idEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerSaldoDocumentoAFavorCliente", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@idTercero", idTercero));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public DataTable ObtenerPagosDocumentos(DateTime FechaInicial, DateTime FechaFinal, long IdUsuario, string NumeroDocumento, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerPagosConDocumentos", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                if (IdUsuario == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdUsuario", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                }
                if (string.IsNullOrEmpty(NumeroDocumento))
                {
                    oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", NumeroDocumento));
                }
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public DataTable ObtenerDocumentosPagos(DateTime FechaInicial, DateTime FechaFinal, long IdUsuario, string NumeroDocumento, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerDocumentosConPagos", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                if (IdUsuario == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdUsuario", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                }
                if (string.IsNullOrEmpty(NumeroDocumento))
                {
                    oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", NumeroDocumento));
                }
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public bool GuardarTodo(tblDocumentoItem oDocItem, List<tblDetalleDocumentoItem> oListDet, tblPagoItem oPagoI, List<tblTipoPagoItem> oTipPagLis)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                tblTipoDocumentoItem oTDItem;
                tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
                oTDItem = oTDDao.ObtenerTipoDocumentoConTransaccion(oDocItem.IdTipoDocumento, Conexion, oTran);
                SqlCommand oSQL = new SqlCommand("spGuardarDocumento", Conexion, oTran);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@TipoDocumento", oDocItem.IdTipoDocumento));
                oSQL.Parameters.Add(new SqlParameter("@Fecha", oDocItem.Fecha));
                oSQL.Parameters.Add(new SqlParameter("@idTercero", oDocItem.idTercero));
                oSQL.Parameters.Add(new SqlParameter("@Telefono", oDocItem.Telefono));
                oSQL.Parameters.Add(new SqlParameter("@Direccion", oDocItem.Direccion));
                oSQL.Parameters.Add(new SqlParameter("@idCiudad", oDocItem.idCiudad));
                oSQL.Parameters.Add(new SqlParameter("@NombreTercero", oDocItem.NombreTercero));
                oSQL.Parameters.Add(new SqlParameter("@Observaciones", oDocItem.Observaciones));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", oDocItem.idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", oDocItem.idUsuario));
                oSQL.Parameters.Add(new SqlParameter("@TotalDocumento", oDocItem.TotalDocumento));
                oSQL.Parameters.Add(new SqlParameter("@TotalIVA", oDocItem.TotalIVA));
                oSQL.Parameters.Add(new SqlParameter("@saldo", oDocItem.saldo));
                oSQL.Parameters.Add(new SqlParameter("@IdEstado", oDocItem.IdEstado));
                oSQL.Parameters.Add(new SqlParameter("@EnCuadre", oDocItem.EnCuadre));
                oSQL.Parameters.Add(new SqlParameter("@Devuelta", oDocItem.Devuelta));
                oSQL.Parameters.Add(new SqlParameter("@TotalDescuento", oDocItem.TotalDescuento));
                oSQL.Parameters.Add(new SqlParameter("@TotalAntesIVA", oDocItem.TotalAntesIVA));
                if (oDocItem.IdVendedor == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdVendedor", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdVendedor", oDocItem.IdVendedor));
                }
                oSQL.Parameters.Add(new SqlParameter("@FechaVencimiento", oDocItem.FechaVencimiento));
                if (oDocItem.BaseIdTipoDocumento == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@BaseIdTipoDocumento", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@BaseIdTipoDocumento", oDocItem.BaseIdTipoDocumento));
                }
                if (string.IsNullOrEmpty(oDocItem.Referencia))
                {
                    oSQL.Parameters.Add(new SqlParameter("@Referencia", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@Referencia", oDocItem.Referencia));
                }
                oSQL.Parameters.Add(new SqlParameter("@Propina", oDocItem.Propina));
                oSQL.Parameters.Add(new SqlParameter("@Impoconsumo", oDocItem.Impoconsumo));
                oSQL.Parameters.Add(new SqlParameter("@TotalRetenciones", oDocItem.TotalRetenciones));
                oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", oDocItem.NumeroDocumento));
                oSQL.Parameters.Add(new SqlParameter("@ZipKey", oDocItem.ZipKey));
                string Valores = oSQL.ExecuteScalar().ToString();
                oDocItem.idDocumento = long.Parse(Valores.Split(',')[0]);
                oDocItem.NumeroDocumento = Valores.Split(',')[1];
                if(oDocItem.idDocumento > 0)
                {
                    if (oPagoI.totalPago > 0 && (oDocItem.IdTipoDocumento == 1 || oDocItem.IdTipoDocumento == 2 || oDocItem.IdTipoDocumento == 11))
                    {
                        tblPagoDao oPagoDao = new tblPagoDao(Conexion.ConnectionString);
                        List<tblPagoDetalleItem> oPagoDetList = new List<tblPagoDetalleItem>();
                        tblPagoDetalleItem oPagoDetI = new tblPagoDetalleItem();
                        oPagoDetI.idDocumento = oDocItem.idDocumento;
                        if (oPagoI.totalPago > oDocItem.TotalDocumento)
                        {
                            oPagoDetI.valorAbono = oDocItem.TotalDocumento;
                        }
                        else
                        {
                            oPagoDetI.valorAbono = oPagoI.totalPago;
                        }
                        oPagoDetList.Add(oPagoDetI);
                        if (!oPagoDao.GuardarPagoConTransaccion(oPagoI, oPagoDetList, oTipPagLis, oDocItem.IdTipoDocumento, Conexion, oTran))
                        {
                            oTran.Rollback();
                            return false;
                        }
                    }
                    foreach (tblDetalleDocumentoItem Detalle in oListDet)
                    {
                        SqlCommand oSQL1 = new SqlCommand("spGuardarDocumentoDetalle", Conexion, oTran);
                        oSQL1.CommandType = CommandType.StoredProcedure;
                        oSQL1.Parameters.Add(new SqlParameter("@TipoDocumento", oDocItem.IdTipoDocumento));
                        oSQL1.Parameters.Add(new SqlParameter("@idDocumento", oDocItem.idDocumento));
                        oSQL1.Parameters.Add(new SqlParameter("@NumeroLinea", Detalle.NumeroLinea));
                        oSQL1.Parameters.Add(new SqlParameter("@idArticulo", Detalle.idArticulo));
                        oSQL1.Parameters.Add(new SqlParameter("@Descripcion", Detalle.Articulo));
                        oSQL1.Parameters.Add(new SqlParameter("@Precio", Detalle.ValorUnitario));
                        oSQL1.Parameters.Add(new SqlParameter("@Impuesto", Detalle.IVA));
                        oSQL1.Parameters.Add(new SqlParameter("@Cantidad", Detalle.Cantidad));
                        oSQL1.Parameters.Add(new SqlParameter("@idBodega", Detalle.idBodega));
                        oSQL1.Parameters.Add(new SqlParameter("@Descuento", Detalle.Descuento));
                        oSQL1.Parameters.Add(new SqlParameter("@CostoPonderado", Detalle.CostoPonderado));
                        oSQL1.Parameters.Add(new SqlParameter("@PrecioVenta", Detalle.PrecioVenta));
                        Detalle.idDetalleDocumento = long.Parse(oSQL1.ExecuteScalar().ToString());
                        SqlCommand SQLValidarStock = new SqlCommand("spValidarStockBodega", Conexion, oTran);
                        SQLValidarStock.CommandType = CommandType.StoredProcedure;
                        SQLValidarStock.Parameters.Add(new SqlParameter("@idArticulo", Detalle.idArticulo));
                        SQLValidarStock.Parameters.Add(new SqlParameter("@idBodega", Detalle.idBodega));
                        decimal result = decimal.Parse(SQLValidarStock.ExecuteScalar().ToString());
                        if (result > 0)
                        {
                            //oTran.Rollback();
                            //throw new Exception($"El articulo {Detalle.idArticulo} - {Detalle.Articulo} no superó la validación stock.");
                            SqlCommand SQLCorrejirStock = new SqlCommand("spCorrejirStock", Conexion, oTran);
                            SQLCorrejirStock.CommandType = CommandType.StoredProcedure;
                            SQLCorrejirStock.Parameters.Add(new SqlParameter("@IdArticulo", Detalle.idArticulo));
                            SQLCorrejirStock.Parameters.Add(new SqlParameter("@IdBodega", Detalle.idBodega));
                            SQLCorrejirStock.Parameters.Add(new SqlParameter("@IdEmpresa", oDocItem.idEmpresa));
                            SQLCorrejirStock.ExecuteNonQuery();
                        }
                        var SPNombre = ObtenerMovimientoStockNombre((TipoDocumentoEnum)oDocItem.IdTipoDocumento);
                        if (!string.IsNullOrEmpty(SPNombre))
                        {
                            SqlCommand SQLAuditoriaStock = new SqlCommand(SPNombre, Conexion, oTran);
                            SQLAuditoriaStock.CommandType = CommandType.StoredProcedure;
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdArticulo", Detalle.idArticulo));
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdBodega", Detalle.idBodega));
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@Cantidad", Detalle.Cantidad));

                            if (oDocItem.IdTipoDocumento == TipoDocumentoEnum.EntradaMercancia.GetHashCode() || oDocItem.IdTipoDocumento == TipoDocumentoEnum.Compra.GetHashCode()) {
                                SQLAuditoriaStock.Parameters.Add(new SqlParameter("@Costo", Detalle.Cantidad));
                            }

                            if (oDocItem.IdTipoDocumento == TipoDocumentoEnum.FacturaElectronica.GetHashCode() || oDocItem.IdTipoDocumento == TipoDocumentoEnum.Venta.GetHashCode() || oDocItem.IdTipoDocumento == TipoDocumentoEnum.Remision.GetHashCode()) {
                                SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdDocumento", oDocItem.idDocumento));
                            }
                            SQLAuditoriaStock.ExecuteNonQuery();
                        }

                        SPNombre = ObtenerAuditoriaStockNombre((TipoDocumentoEnum)oDocItem.IdTipoDocumento);
                        if (!string.IsNullOrEmpty(SPNombre))
                        {
                            SqlCommand SQLAuditoriaStock = new SqlCommand(SPNombre, Conexion, oTran);
                            SQLAuditoriaStock.CommandType = CommandType.StoredProcedure;
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdArticulo", Detalle.idArticulo));
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdBodega", Detalle.idBodega));
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@Cantidad", Detalle.Cantidad));
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdDocumento", oDocItem.idDocumento));
                            SQLAuditoriaStock.ExecuteNonQuery();
                        }
                    }
                    foreach (tblDocumentoRetencionItem Retencion in oDocItem.Retenciones)
                    {
                        SqlCommand oSQL2 = new SqlCommand("spGuardarDocumentoRetencion", Conexion, oTran);
                        oSQL2.CommandType = CommandType.StoredProcedure;
                        oSQL2.Parameters.Add(new SqlParameter("@IdDocumento", oDocItem.idDocumento));
                        oSQL2.Parameters.Add(new SqlParameter("@IdRetencion", Retencion.IdRetencion));
                        oSQL2.Parameters.Add(new SqlParameter("@TipoDocumento", Retencion.TipoDocumento));
                        oSQL2.Parameters.Add(new SqlParameter("@Porcentaje", Retencion.Porcentaje));
                        oSQL2.Parameters.Add(new SqlParameter("@Base", Retencion.Base));
                        oSQL2.Parameters.Add(new SqlParameter("@Valor", Retencion.Valor));
                        oSQL2.ExecuteNonQuery();
                    }
                    oTran.Commit();
                }
                else
                {
                    oTran.Rollback();
                    return false;
                }
            }
            catch(Exception ex)
            {
                oTran.Rollback();
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
        private string ObtenerMovimientoStockNombre(TipoDocumentoEnum td) {
            string spNombre = "";
            switch (td) {
                case TipoDocumentoEnum.EntradaMercancia:
                    spNombre = "spMovimientoStockArticuloEM";
                    break;
                case TipoDocumentoEnum.Compra:
                    spNombre = "spMovimientoStockArticuloFC";
                    break;
                case TipoDocumentoEnum.FacturaElectronica:
                    spNombre = "spMovimientoStockArticuloFE";
                    break;
                case TipoDocumentoEnum.Venta:
                    spNombre = "spMovimientoStockArticuloFV";
                    break;
                case TipoDocumentoEnum.NotaCreditoCompra:
                    spNombre = "spMovimientoStockArticuloNCC";
                    break;
                case TipoDocumentoEnum.NotaCreditoVenta:
                    spNombre = "spMovimientoStockArticuloNCV";
                    break;
                case TipoDocumentoEnum.Remision:
                    spNombre = "spMovimientoStockArticuloRE";
                    break;
                case TipoDocumentoEnum.SalidaMercancia:
                    spNombre = "spMovimientoStockArticuloSM";
                    break;
            }
            return spNombre;
        }
        private string ObtenerAuditoriaStockNombre(TipoDocumentoEnum td)
        {
            string spNombre = "";
            switch (td)
            {
                case TipoDocumentoEnum.EntradaMercancia:
                    spNombre = "spAuditoriaStockEM";
                    break;
                case TipoDocumentoEnum.Compra:
                    spNombre = "spAuditoriaStockFC";
                    break;
                case TipoDocumentoEnum.FacturaElectronica:
                    spNombre = "spAuditoriaStockFE";
                    break;
                case TipoDocumentoEnum.Venta:
                    spNombre = "spAuditoriaStockFV";
                    break;
                case TipoDocumentoEnum.NotaCreditoCompra:
                    spNombre = "spAuditoriaStockNCC";
                    break;
                case TipoDocumentoEnum.NotaCreditoVenta:
                    spNombre = "spAuditoriaStockNCV";
                    break;
                case TipoDocumentoEnum.Remision:
                    spNombre = "spAuditoriaStockRE";
                    break;
                case TipoDocumentoEnum.SalidaMercancia:
                    spNombre = "spAuditoriaStockSM";
                    break;
            }
            return spNombre;
        }
        public string GuardarSoloDocumento(tblDocumentoItem oDocItem, List<tblDetalleDocumentoItem> oListDet)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                SqlCommand oSQL = new SqlCommand("spGuardarDocumento", Conexion, oTran);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@TipoDocumento", oDocItem.IdTipoDocumento));
                oSQL.Parameters.Add(new SqlParameter("@Fecha", oDocItem.Fecha));
                oSQL.Parameters.Add(new SqlParameter("@idTercero", oDocItem.idTercero));
                oSQL.Parameters.Add(new SqlParameter("@Telefono", oDocItem.Telefono));
                oSQL.Parameters.Add(new SqlParameter("@Direccion", oDocItem.Direccion));
                oSQL.Parameters.Add(new SqlParameter("@idCiudad", oDocItem.idCiudad));
                oSQL.Parameters.Add(new SqlParameter("@NombreTercero", oDocItem.NombreTercero));
                oSQL.Parameters.Add(new SqlParameter("@Observaciones", oDocItem.Observaciones));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", oDocItem.idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", oDocItem.idUsuario));
                oSQL.Parameters.Add(new SqlParameter("@TotalDocumento", oDocItem.TotalDocumento));
                oSQL.Parameters.Add(new SqlParameter("@TotalIVA", oDocItem.TotalIVA));
                oSQL.Parameters.Add(new SqlParameter("@saldo", oDocItem.saldo));
                oSQL.Parameters.Add(new SqlParameter("@IdEstado", oDocItem.IdEstado));
                oSQL.Parameters.Add(new SqlParameter("@EnCuadre", oDocItem.EnCuadre));
                oSQL.Parameters.Add(new SqlParameter("@Devuelta", oDocItem.Devuelta));
                oSQL.Parameters.Add(new SqlParameter("@TotalDescuento", oDocItem.TotalDescuento));
                oSQL.Parameters.Add(new SqlParameter("@TotalAntesIVA", oDocItem.TotalAntesIVA));
                oSQL.Parameters.Add(new SqlParameter("@IdVendedor", DBNull.Value));
                oSQL.Parameters.Add(new SqlParameter("@FechaVencimiento", oDocItem.FechaVencimiento));
                oSQL.Parameters.Add(new SqlParameter("@BaseIdTipoDocumento", DBNull.Value));
                if (string.IsNullOrEmpty(oDocItem.Referencia))
                {
                    oSQL.Parameters.Add(new SqlParameter("@Referencia", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@Referencia", oDocItem.Referencia));
                }
                oSQL.Parameters.Add(new SqlParameter("@Propina", oDocItem.Propina));
                oSQL.Parameters.Add(new SqlParameter("@Impoconsumo", oDocItem.Impoconsumo));
                string Valores = oSQL.ExecuteScalar().ToString();
                oDocItem.idDocumento = long.Parse(Valores.Split(',')[0]);
                oDocItem.NumeroDocumento = Valores.Split(',')[1];
                if (oDocItem.idDocumento > 0)
                {
                    foreach (tblDetalleDocumentoItem Detalle in oListDet)
                    {
                        SqlCommand oSQL1 = new SqlCommand("spGuardarDocumentoDetalle", Conexion, oTran);
                        oSQL1.CommandType = CommandType.StoredProcedure;
                        oSQL1.Parameters.Add(new SqlParameter("@TipoDocumento", oDocItem.IdTipoDocumento));
                        oSQL1.Parameters.Add(new SqlParameter("@idDocumento", oDocItem.idDocumento));
                        oSQL1.Parameters.Add(new SqlParameter("@NumeroLinea", Detalle.NumeroLinea));
                        oSQL1.Parameters.Add(new SqlParameter("@idArticulo", Detalle.idArticulo));
                        oSQL1.Parameters.Add(new SqlParameter("@Descripcion", Detalle.Articulo));
                        oSQL1.Parameters.Add(new SqlParameter("@Precio", Detalle.ValorUnitario));
                        oSQL1.Parameters.Add(new SqlParameter("@Impuesto", Detalle.IVA));
                        oSQL1.Parameters.Add(new SqlParameter("@Cantidad", Detalle.Cantidad));
                        oSQL1.Parameters.Add(new SqlParameter("@idBodega", Detalle.idBodega));
                        oSQL1.Parameters.Add(new SqlParameter("@Descuento", Detalle.Descuento));
                        oSQL1.Parameters.Add(new SqlParameter("@CostoPonderado", Detalle.CostoPonderado));
                        oSQL1.Parameters.Add(new SqlParameter("@PrecioVenta", DBNull.Value));
                        Detalle.idDetalleDocumento = long.Parse(oSQL1.ExecuteScalar().ToString());
                        
                        SqlCommand SQLValidarStock = new SqlCommand("spValidarStockBodega", Conexion, oTran);
                        SQLValidarStock.CommandType = CommandType.StoredProcedure;
                        SQLValidarStock.Parameters.Add(new SqlParameter("@idArticulo", Detalle.idArticulo));
                        SQLValidarStock.Parameters.Add(new SqlParameter("@idBodega", Detalle.idBodega));
                        decimal result = decimal.Parse(SQLValidarStock.ExecuteScalar().ToString());
                        if (result > 0)
                        {
                            //oTran.Rollback();
                            //throw new Exception($"El articulo {Detalle.idArticulo} - {Detalle.Articulo} no superó la validación stock.");
                            SqlCommand SQLCorrejirStock = new SqlCommand("spCorrejirStock", Conexion, oTran);
                            SQLCorrejirStock.CommandType = CommandType.StoredProcedure;
                            SQLCorrejirStock.Parameters.Add(new SqlParameter("@idArticulo", Detalle.idArticulo));
                            SQLCorrejirStock.Parameters.Add(new SqlParameter("@idBodega", Detalle.idBodega));
                            SQLCorrejirStock.ExecuteNonQuery();
                        }
                        var SPNombre = ObtenerMovimientoStockNombre((TipoDocumentoEnum)oDocItem.IdTipoDocumento);
                        if (!string.IsNullOrEmpty(SPNombre))
                        {
                            SqlCommand SQLAuditoriaStock = new SqlCommand(SPNombre, Conexion, oTran);
                            SQLAuditoriaStock.CommandType = CommandType.StoredProcedure;
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdArticulo", Detalle.idArticulo));
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdBodega", Detalle.idBodega));
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@Cantidad", Detalle.Cantidad));
                            if (oDocItem.IdTipoDocumento == TipoDocumentoEnum.EntradaMercancia.GetHashCode() || oDocItem.IdTipoDocumento == TipoDocumentoEnum.Compra.GetHashCode())
                            {
                                SQLAuditoriaStock.Parameters.Add(new SqlParameter("@Costo", Detalle.Cantidad));
                            }
                            if (oDocItem.IdTipoDocumento == TipoDocumentoEnum.FacturaElectronica.GetHashCode() || oDocItem.IdTipoDocumento == TipoDocumentoEnum.Venta.GetHashCode() || oDocItem.IdTipoDocumento == TipoDocumentoEnum.Remision.GetHashCode())
                            {
                                SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdDocumento", oDocItem.idDocumento));
                            }
                            SQLAuditoriaStock.ExecuteNonQuery();
                        }
                        SPNombre = ObtenerAuditoriaStockNombre((TipoDocumentoEnum)oDocItem.IdTipoDocumento);
                        if (!string.IsNullOrEmpty(SPNombre))
                        {
                            SqlCommand SQLAuditoriaStock = new SqlCommand(SPNombre, Conexion, oTran);
                            SQLAuditoriaStock.CommandType = CommandType.StoredProcedure;
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdArticulo", Detalle.idArticulo));
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdBodega", Detalle.idBodega));
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@Cantidad", Detalle.Cantidad));
                            SQLAuditoriaStock.Parameters.Add(new SqlParameter("@IdDocumento", oDocItem.idDocumento));
                            SQLAuditoriaStock.ExecuteNonQuery();
                        }
                        if (oDocItem.IdTipoDocumento == 2 || oDocItem.IdTipoDocumento == 4 || oDocItem.IdTipoDocumento == 5)
                        {
                            SqlCommand oSQL2 = new SqlCommand("spCalcularCostoPonderado", Conexion, oTran);
                            oSQL2.CommandType = CommandType.StoredProcedure;
                            oSQL2.Parameters.Add(new SqlParameter("@idArticulo", Detalle.idArticulo));
                            oSQL2.Parameters.Add(new SqlParameter("@Cantidad", Detalle.Cantidad));
                            oSQL2.Parameters.Add(new SqlParameter("@ValorUnitario", Detalle.ValorUnitario));
                            if (string.IsNullOrEmpty(oDocItem.Referencia))
                            {
                                oSQL2.Parameters.Add(new SqlParameter("@NumeroDocumentoDevolucion", DBNull.Value));
                            }
                            else
                            {
                                oSQL2.Parameters.Add(new SqlParameter("@NumeroDocumentoDevolucion", oDocItem.Referencia));
                            }
                            oSQL2.ExecuteNonQuery();
                        }
                    }
                    if (oDocItem.IdTipoDocumento == 5)
                    {
                        SqlCommand oSQL3 = new SqlCommand("spCalcularTotalesEntradaMercancia", Conexion, oTran);
                        oSQL3.CommandType = CommandType.StoredProcedure;
                        oSQL3.Parameters.Add(new SqlParameter("@IdDocumento", oDocItem.idDocumento));
                        oSQL3.ExecuteNonQuery();
                    }
                    if (oDocItem.IdTipoDocumento == 6)
                    {
                        SqlCommand oSQL3 = new SqlCommand("spCalcularTotalesSalidaMercancia", Conexion, oTran);
                        oSQL3.CommandType = CommandType.StoredProcedure;
                        oSQL3.Parameters.Add(new SqlParameter("@IdDocumento", oDocItem.idDocumento));
                        oSQL3.ExecuteNonQuery();
                    }
                    oTran.Commit();
                    return "Exito";
                }
                else
                {
                    oTran.Commit();
                    return "Documento repetido";
                }
            }
            catch (Exception ex)
            {
                oTran.Rollback();
                return ex.Message;
            }
        }
        public bool CambiarEstadoDocumento(long IdDocumento, int IdEstado, int TipoDocumento)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                SqlCommand oSQL = new SqlCommand("spCambiarEstadoDocumento", Conexion, oTran);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdDocumento", IdDocumento));
                oSQL.Parameters.Add(new SqlParameter("@IdEstado", IdEstado));
                oSQL.Parameters.Add(new SqlParameter("@TipoDocumento", TipoDocumento));
                oSQL.ExecuteNonQuery();
                oTran.Commit();
                return true;
            }
            catch
            {
                oTran.Rollback();
                return false;
            }
        }
        public DataTable ObtenerReporteDocumentoRangoFecha(long idEmpresa, DateTime fechaInicio, DateTime fechaFinal, long tipoDocumento, long idBodega)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("EXEC spReporteDocumentoRangoFecha @idEmpresa,@FechaInicial,@FechaFinal,@TipoDocumento,@idBodega", Conexion);
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", fechaInicio));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", fechaFinal.AddDays(1)));
                oSQL.Parameters.Add(new SqlParameter("@TipoDocumento", tipoDocumento));
                if (idBodega == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@idBodega", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@idBodega", idBodega));
                }
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public DataTable ObtenerInformacionCompra(long idEmpresa, DateTime fechaInicio, DateTime fechaFinal, string NumeroDocumento)
        {
            try
            {
                List<tblMovimientosPorDocumentoItem> Item = new List<tblMovimientosPorDocumentoItem>();
                SqlCommand oSQL = new SqlCommand("spObtenerInformacionCompra", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", fechaInicio));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", fechaFinal));
                if (string.IsNullOrEmpty(NumeroDocumento))
                {
                    oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", NumeroDocumento));
                }
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public DataTable ObtenerMovimientosPorDocumento(long idUsuario, long idEmpresa, DateTime fechaInicio, DateTime fechaFinal, long tipoDocumento, string nombre, short idLinea, long idProveedor, long idBodega)
        {
            try
            {
                List<tblMovimientosPorDocumentoItem> Item = new List<tblMovimientosPorDocumentoItem>();
                SqlCommand oSQL = new SqlCommand("EXEC spReporteMovimientosPorDocumento @idUsuario,@idEmpresa,@FechaInicial,@FechaFinal,@TipoDocumento,@nombre,@idLinea,@idProveedor,@idBodega", Conexion);
                oSQL.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", fechaInicio));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", fechaFinal.AddDays(1)));
                oSQL.Parameters.Add(new SqlParameter("@TipoDocumento", tipoDocumento));
                oSQL.Parameters.Add(new SqlParameter("@nombre", nombre));
                oSQL.Parameters.Add(new SqlParameter("@idLinea", idLinea));
                oSQL.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
                oSQL.Parameters.Add(new SqlParameter("@idBodega", idBodega));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public long ObtenerNumeracionDocumento(long idEmpresa, long tipoDocumento)
        {
            long numero;
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            string strSQL = string.Format("SELECT ISNULL(MAX(CAST(NumeroDocumento AS INT) + 1),1) FROM {0} WHERE idEmpresa = @idEmpresa;", oTDItem.TablaDocumento);
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
            Conexion.Open();
            try
            {
                numero = long.Parse(oSQL.ExecuteScalar().ToString());
            }
            catch
            {
                numero = 0;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return numero;
        }
        public tblDocumentoItem ObtenerDocumento(long Id, long tipoDocumento)
        {
            tblDocumentoItem Item = new tblDocumentoItem();
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            string strSQL = string.Format(@"SELECT T0.idDocumento,
                T0.NumeroDocumento,
                T0.Fecha,
                T0.idTercero,
                T0.Telefono,
                T0.Direccion,
                T0.idCiudad,
                T0.Nombre,
                ('Iden. ' + T2.Identificacion + ' Dir. ' + T0.Direccion + ' Tel. ' + T0.Telefono) [DatosTercero],
                T0.Observaciones,
                T0.idEmpresa,
                T0.idUsuario,
                T0.TotalDocumento,
                T0.TotalIVA,
                T0.saldo,
                ISNULL(T0.IdEstado,1)[IdEstado],
                ISNULL(T1.Estado,'Activo')[Estado],
                ISNULL(T0.Devuelta,0)[Devuelta],
                ISNULL(T0.TotalDescuento,0)[TotalDescuento],
                ISNULL(T0.TotalAntesIVA,0)[TotalAntesIVA],
                ISNULL(T0.Propina,0)[Propina],
                ISNULL(T0.Referencia,'')[Referencia],
                ISNULL(T0.Resolucion,'')[Resolucion],
                ISNULL(T0.Impoconsumo,0)[Impoconsumo],
                ISNULL(T0.IdVendedor,0)[IdVendedor],
                ISNULL(T0.TotalRetencion,0)[TotalRetencion],
                '' AS ZipKey
                FROM {0} T0
                LEFT JOIN tblEstadoFactura T1 ON T1.IdEstado = T0.IdEstado
                INNER JOIN tblTercero T2 ON T2.idTercero = T0.idTercero
                WHERE idDocumento = @id;", oTDItem.TablaDocumento);
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion);
            oSQL.Parameters.Add(new SqlParameter("@id", Id));
            Conexion.Open();
            try
            {
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
        public List<tblDocumentoItem> ObtenerDocumentoLista(long tipoDocumento, DateTime fechaInicial, DateTime fechaFinal, long idEmpresa, string NumeroDocumento, string Cliente, string Identificacion)
        {
            List<tblDocumentoItem> Item = new List<tblDocumentoItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerDocumentoLista", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@FechaInicial", fechaInicial));
            oSQL.Parameters.Add(new SqlParameter("@FechaFinal", fechaFinal));
            oSQL.Parameters.Add(new SqlParameter("@IdTipoDocumento", tipoDocumento));
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
            if (string.IsNullOrEmpty(NumeroDocumento))
            {
                oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", NumeroDocumento));
            }
            if (string.IsNullOrEmpty(Cliente))
            {
                oSQL.Parameters.Add(new SqlParameter("@Cliente", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Cliente", Cliente));
            }
            if (string.IsNullOrEmpty(Identificacion))
            {
                oSQL.Parameters.Add(new SqlParameter("@Identificacion", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Identificacion", Identificacion));
            }
            Conexion.Open();
            try
            {
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Item.Add(ObtenerItem(reader));
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
        public DataTable ObtenerDocumentosACredito(DateTime FechaInicial, DateTime FechaFinal, long IdCliente, string Identificacion, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerDocumentosACredito", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                if (IdCliente == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdCliente", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdCliente", IdCliente));
                }
                if (string.IsNullOrEmpty(Identificacion))
                {
                    oSQL.Parameters.Add(new SqlParameter("@Identificacion", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@Identificacion", Identificacion));
                }
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", IdEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        public DataTable ObtenerFacturasPendientesPago(long idCliente, long idEmpresa, short Tipo)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerFacturasConSaldoPendiente", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                if (idCliente == 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@idTercero", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@idTercero", idCliente));
                }
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@Tipo", Tipo));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
        private tblMovimientosPorDocumentoItem ObtenerMovimiento(SqlDataReader reader)
        {
            tblMovimientosPorDocumentoItem movimiento = new tblMovimientosPorDocumentoItem();
            movimiento.idDocumento = long.Parse(reader["idDocumento"].ToString());
            movimiento.NumeroDocumento = reader["NumeroDocumento"].ToString();
            movimiento.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            movimiento.Nombre = reader["Nombre"].ToString();
            movimiento.NombreCompleto = reader["NombreCompleto"].ToString();
            movimiento.TotalIVA = decimal.Parse(reader["TotalIVA"].ToString());
            movimiento.TotalDocumento = decimal.Parse(reader["TotalDocumento"].ToString());
            movimiento.Descripcion = reader["Descripcion"].ToString();
            movimiento.Cantidad = decimal.Parse(reader["cantidad"].ToString());
            movimiento.Precio = decimal.Parse(reader["precio"].ToString());
            movimiento.ValorTotalLinea = decimal.Parse(reader["ValorTotalLinea"].ToString());
            return movimiento;
        }
        private tblDocumentoItem ObtenerItem(SqlDataReader reader)
        {
            tblDocumentoItem Item = new tblDocumentoItem();
            Item.idDocumento = long.Parse(reader["idDocumento"].ToString());
            Item.NumeroDocumento = reader["NumeroDocumento"].ToString();
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            Item.idTercero = long.Parse(reader["idTercero"].ToString());
            Item.Telefono = reader["Telefono"].ToString();
            Item.Direccion = reader["Direccion"].ToString();
            Item.idCiudad = short.Parse(reader["idCiudad"].ToString());
            Item.NombreTercero = reader["Nombre"].ToString();
            Item.DatosTercero = reader["DatosTercero"].ToString();
            Item.Observaciones = reader["Observaciones"].ToString();
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.TotalDocumento = decimal.Parse(reader["TotalDocumento"].ToString());
            Item.TotalIVA = decimal.Parse(reader["TotalIVA"].ToString());
            if (reader["saldo"].ToString() != "")
            {
                Item.saldo = decimal.Parse(reader["saldo"].ToString());
            }
            Item.IdEstado = long.Parse(reader["IdEstado"].ToString());
            Item.Estado = reader["Estado"].ToString();
            Item.Devuelta = decimal.Parse(reader["Devuelta"].ToString());
            Item.Referencia = reader["Referencia"].ToString();
            Item.TotalDescuento = decimal.Parse(reader["TotalDescuento"].ToString());
            Item.TotalAntesIVA = decimal.Parse(reader["TotalAntesIVA"].ToString());
            Item.Propina = decimal.Parse(reader["Propina"].ToString());
            Item.Resolucion = reader["Resolucion"].ToString();
            Item.Impoconsumo = decimal.Parse(reader["Impoconsumo"].ToString());
            Item.TotalRetenciones = decimal.Parse(reader["TotalRetencion"].ToString());
            Item.IdVendedor = long.Parse(reader["IdVendedor"].ToString());
            if(reader["ZipKey"] != null)
            {
                Item.ZipKey = reader["ZipKey"].ToString();
            }
            return Item;
        }
        private void ActualizarNumeracionDocumento(tblTipoDocumentoItem oTDItem, long IdEmpresa, long IdUsuario, SqlConnection Con, SqlTransaction oTran)
        {
            try
            {
                string strSQL = "";
                if (oTDItem.idTipoDocumento == 1)
                {
                    strSQL = string.Format(@"IF(SELECT ISNULL(FacturacionCaja,0) FROM tblEmpresa WHERE idEmpresa = {1}) = 1
                    BEGIN
                        UPDATE tblCaja SET ProximoValor = ProximoValor + 1 WHERE idCaja = (SELECT T1.idCaja FROM tblCuadreCaja T0
	                                                                            INNER JOIN tblCaja T1 ON T1.IdCaja = T0.IdCaja 
	                                                                            WHERE T1.idEmpresa = {1}
	                                                                            AND T0.IdUsuario = {2}
	                                                                            AND T0.Estado = 1)
                    END
                    ELSE
                    BEGIN
	                    UPDATE {0} SET ProximoValor = ProximoValor + 1 WHERE idEmpresa = {1}
                    END", oTDItem.TablaNumeracion, IdEmpresa, IdUsuario);
                }
                else
                {
                    strSQL = string.Format("UPDATE {0} SET ProximoValor = ProximoValor + 1 WHERE idEmpresa = {1}", oTDItem.TablaNumeracion, IdEmpresa);
                }
                SqlCommand oSQL = new SqlCommand(strSQL, Con, oTran);
                oSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool GuardarVentaRapida(tblDocumentoItem Item, string Articulos)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                if (Item.TipoDocumento == tblTipoDocumentoItem.TipoDocumentoEnum.cotizacion && Item.idDocumento > 0)
                {
                    SqlCommand oSQL = new SqlCommand("spActualizarCotizacion", Conexion, oTran);
                    oSQL.CommandType = CommandType.StoredProcedure;
                    oSQL.Parameters.Add(new SqlParameter("@IdDocumento", Item.idDocumento));
                    oSQL.Parameters.Add(new SqlParameter("@Telefono", Item.Telefono));
                    oSQL.Parameters.Add(new SqlParameter("@Direccion", Item.Direccion));
                    oSQL.Parameters.Add(new SqlParameter("@Observaciones", Item.Observaciones));
                    oSQL.Parameters.Add(new SqlParameter("@TotalDocumento", Item.TotalDocumento));
                    oSQL.Parameters.Add(new SqlParameter("@TotalAntesIVA", Item.TotalAntesIVA));
                    oSQL.Parameters.Add(new SqlParameter("@TotalDescuento", Item.TotalDescuento));
                    oSQL.Parameters.Add(new SqlParameter("@TotalIVA", Item.TotalIVA));
                    oSQL.Parameters.Add(new SqlParameter("@Propina", Item.Propina));
                    oSQL.ExecuteNonQuery();
                    SqlCommand oSQL1 = new SqlCommand("spEliminarCotizacionDetalles", Conexion, oTran);
                    oSQL1.CommandType = CommandType.StoredProcedure;
                    oSQL1.Parameters.Add(new SqlParameter("@IdDocumento", Item.idDocumento));
                    oSQL1.ExecuteNonQuery();
                    SqlCommand oSQL3 = new SqlCommand("spEliminarCotizacionVentaRapida", Conexion, oTran);
                    oSQL3.CommandType = CommandType.StoredProcedure;
                    oSQL3.Parameters.Add(new SqlParameter("@idCotizacion", Item.idDocumento));
                    oSQL3.ExecuteNonQuery();
                }
                else {
                    SqlCommand oSQL = new SqlCommand("spInsertarFacturaVenta", Conexion, oTran);
                    oSQL.CommandType = CommandType.StoredProcedure;
                    oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
                    oSQL.Parameters.Add(new SqlParameter("@IdTercero", Item.idTercero));
                    oSQL.Parameters.Add(new SqlParameter("@Telefono", Item.Telefono));
                    oSQL.Parameters.Add(new SqlParameter("@Direccion", Item.Direccion));
                    oSQL.Parameters.Add(new SqlParameter("@IdCiudad", Item.idCiudad));
                    oSQL.Parameters.Add(new SqlParameter("@NombreTercero", Item.NombreTercero));
                    oSQL.Parameters.Add(new SqlParameter("@Observaciones", Item.Observaciones));
                    oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", Item.idEmpresa));
                    oSQL.Parameters.Add(new SqlParameter("@IdUsuario", Item.idUsuario));
                    oSQL.Parameters.Add(new SqlParameter("@TotalDocumento", Item.TotalDocumento));
                    oSQL.Parameters.Add(new SqlParameter("@TotalIVA", Item.TotalIVA));
                    oSQL.Parameters.Add(new SqlParameter("@Saldo", Item.saldo));
                    oSQL.Parameters.Add(new SqlParameter("@IdEstado", Item.IdEstado));
                    oSQL.Parameters.Add(new SqlParameter("@EnCuadre", Item.EnCuadre));
                    oSQL.Parameters.Add(new SqlParameter("@Devuelta", Item.Devuelta));
                    oSQL.Parameters.Add(new SqlParameter("@IdTipoDocumento", Item.IdTipoDocumento));
                    oSQL.Parameters.Add(new SqlParameter("@FechaVencimiento", Item.FechaVencimiento));
                    oSQL.Parameters.Add(new SqlParameter("@Impoconsumo", Item.Impoconsumo));
                    Item.idDocumento = long.Parse(oSQL.ExecuteScalar().ToString());
                }
                int NumeroLinea = 0;
                foreach (string IdArticulo in Articulos.Split(','))
                {
                    SqlCommand oSQL1 = new SqlCommand("spGuardarDetalleVentaRapida", Conexion, oTran);
                    oSQL1.CommandType = CommandType.StoredProcedure;
                    oSQL1.Parameters.Add(new SqlParameter("@IdVentaRapida", IdArticulo));
                    oSQL1.Parameters.Add(new SqlParameter("@IdDocumento", Item.idDocumento));
                    oSQL1.Parameters.Add(new SqlParameter("@NumeroLinea", NumeroLinea));
                    oSQL1.Parameters.Add(new SqlParameter("@IdTipoDocumento", Item.IdTipoDocumento));
                    oSQL1.ExecuteNonQuery();
                    NumeroLinea++;
                }
                string Procedimiento = string.Empty;
                if (Item.IdTipoDocumento == 1)
                {
                    Procedimiento = "spCalcularTotalesFacturaVenta";
                }
                else if (Item.IdTipoDocumento == 8)
                {
                    Procedimiento = "spCalcularTotalesRemision";
                }
                else {
                    Procedimiento = "spCalcularTotalesCotizacion";
                }
                SqlCommand oSQL2 = new SqlCommand(Procedimiento, Conexion, oTran);
                oSQL2.CommandType = CommandType.StoredProcedure;
                oSQL2.Parameters.Add(new SqlParameter("@IdDocumento", Item.idDocumento));
                oSQL2.ExecuteNonQuery();
                if (Item.IdTipoDocumento == 1)
                {
                    tblPagoItem oPagoI = new tblPagoItem();
                    List<tblPagoDetalleItem> oPagDetList = new List<tblPagoDetalleItem>();
                    List<tblTipoPagoItem> oTipPagList = new List<tblTipoPagoItem>();
                    oPagoI.idTercero = Item.idTercero;
                    oPagoI.totalPago = Item.TotalDocumento;
                    oPagoI.idEmpresa = Item.idEmpresa;
                    oPagoI.fechaPago = Item.Fecha;
                    oPagoI.idUsuario = Item.idUsuario;
                    oPagoI.idEstado = 1;
                    tblPagoDetalleItem oPagDetI = new tblPagoDetalleItem();
                    oPagDetI.valorAbono = Item.TotalDocumento;
                    oPagDetI.idDocumento = Item.idDocumento;
                    oPagDetList.Add(oPagDetI);
                    tblTipoPagoItem oTipPagI = new tblTipoPagoItem();
                    oTipPagI.ValorPago = Item.TotalDocumento;
                    oTipPagI.idFormaPago = 1;
                    oTipPagList.Add(oTipPagI);
                    tblPagoDao oPagoD = new tblPagoDao(Conexion.ConnectionString);
                    if (oPagoD.GuardarPagoConTransaccion(oPagoI, oPagDetList, oTipPagList, 1, Conexion, oTran))
                    {
                        oTran.Commit();
                    }
                    else
                    {
                        oTran.Rollback();
                    }
                }
                else
                {
                    oTran.Commit();
                }
            }
            catch
            {
                oTran.Rollback();
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
        private bool Insertar(tblDocumentoItem documento, long tipoDocumento, long idEmpresa, SqlTransaction transaccion = null)
        {
            bool local = false;
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            tblDetalleDocumentoDao oDDDao = new tblDetalleDocumentoDao(Conexion.ConnectionString);
            Conexion.Open();
            if (transaccion == null)
            {
                transaccion = Conexion.BeginTransaction();
                local = true;
            }
            string strSQL = string.Format(@"DECLARE @PROXIMOVALOR AS NVARCHAR(255);
                IF @TipoDocumento = 1
                BEGIN
	                IF(SELECT ISNULL(FacturacionCaja,0) FROM tblEmpresa WHERE idEmpresa = @idEmpresa) = 1
	                BEGIN
		                SELECT @PROXIMOVALOR = T1.ProximoValor FROM tblCuadreCaja T0
		                INNER JOIN tblCaja T1 ON T1.IdCaja = T0.IdCaja 
		                WHERE T1.idEmpresa = @idEmpresa
		                AND T0.IdUsuario = @idUsuario
		                AND T0.Estado = 1
	                END
	                ELSE
	                BEGIN
		                SELECT @PROXIMOVALOR = ProximoValor FROM {0} WHERE idEmpresa = @idEmpresa;
	                END
                END
                ELSE
                BEGIN
	                SELECT @PROXIMOVALOR = ProximoValor FROM {0} WHERE idEmpresa = @idEmpresa;
                END
                INSERT INTO {1} 
                VALUES (@PROXIMOVALOR,@Fecha,@idTercero,@Telefono,@Direccion,@idCiudad,@NombreTercero,@Observaciones,@idEmpresa,@idUsuario,@TotalDocumento,@TotalIVA,@saldo,1,0,null); 
                SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR(20)) + ',' + @PROXIMOVALOR AS [idDocumento]", oTDItem.TablaNumeracion, oTDItem.TablaDocumento);
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion, transaccion);
            oSQL.Parameters.Add(new SqlParameter("@Fecha", documento.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@idTercero", documento.idTercero));
            oSQL.Parameters.Add(new SqlParameter("@Telefono", documento.Telefono));
            oSQL.Parameters.Add(new SqlParameter("@Direccion", documento.Direccion));
            oSQL.Parameters.Add(new SqlParameter("@idCiudad", documento.idCiudad));
            oSQL.Parameters.Add(new SqlParameter("@NombreTercero", documento.NombreTercero));
            oSQL.Parameters.Add(new SqlParameter("@Observaciones", documento.Observaciones));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", documento.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", documento.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@TotalDocumento", documento.TotalDocumento));
            oSQL.Parameters.Add(new SqlParameter("@TotalIVA", documento.TotalIVA));
            oSQL.Parameters.Add(new SqlParameter("@saldo", documento.saldo));
            oSQL.Parameters.Add(new SqlParameter("@TipoDocumento", documento.IdTipoDocumento));
            try
            {
                string Valores = oSQL.ExecuteScalar().ToString();
                documento.idDocumento = long.Parse(Valores.Split(',')[0]);
                documento.NumeroDocumento = Valores.Split(',')[1];
                ActualizarNumeracionDocumento(oTDItem, documento.idEmpresa, documento.idUsuario, Conexion, transaccion);
                if (local)
                {
                    transaccion.Commit();
                }
            }
            catch
            {
                if(local)
                {
                    transaccion.Rollback();
                }
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
        private bool Actualizar(tblDocumentoItem documento, SqlTransaction transaccion = null)
        {
            bool local = false;
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(documento.TipoDocumento.GetHashCode());
            Conexion.Open();
            if (transaccion == null)
            {
                transaccion = Conexion.BeginTransaction();
                local = true;
            }
            string strSQL = string.Format("UPDATE {0} SET NumeroDocumento = @NumeroDocumento, Fecha = @Fecha, idTercero = @idTercero, Telefono = @Telefono, Direccion = @Direccion, idCiudad = @idCiudad, Nombre = @Nombre, observaciones = @Observaciones, idEmpresa = @idEmpresa, idUsuario = @idUsuario,TotalDocumento = @TotalDocumento, TotalIVA = @TotalIVA, saldo = @saldo WHERE idDocumento = @idDocumento);", oTDItem.TablaDocumento);
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion, transaccion);
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", documento.idDocumento));
            oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", documento.NumeroDocumento));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", documento.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@idCliente", documento.idTercero));
            oSQL.Parameters.Add(new SqlParameter("@Telefono", documento.Telefono));
            oSQL.Parameters.Add(new SqlParameter("@Direccion", documento.Direccion));
            oSQL.Parameters.Add(new SqlParameter("@idCiudad", documento.idCiudad));
            oSQL.Parameters.Add(new SqlParameter("@Nombre", documento.NombreTercero));
            oSQL.Parameters.Add(new SqlParameter("@Observaciones", documento.Observaciones));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", documento.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", documento.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@TotalDocumento", documento.TotalDocumento));
            oSQL.Parameters.Add(new SqlParameter("@TotalIVA", documento.TotalIVA));
            oSQL.Parameters.Add(new SqlParameter("@saldo", documento.saldo));
            try
            {
                oSQL.ExecuteNonQuery();
                if (local)
                {
                    transaccion.Commit();
                }
            }
            catch
            {
                if (local)
                {
                    transaccion.Rollback();
                }
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
        public bool ActualizarFacturaEntrega(long idDocumento)
        {

            string strSQL = "spActualizarFacturaEntrega";
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", idDocumento));
            Conexion.Open();
            try
            {
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
        public bool ActualizarSaldo(tblPagoDetalleItem oPagDetI, long IdTipoDocumento, SqlConnection Con, SqlTransaction Tran)
        {
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumentoConTransaccion(IdTipoDocumento, Con, Tran);
            string strSQL = string.Format(@"UPDATE {0} SET saldo = saldo - @saldo WHERE idDocumento = @idDocumento;
                                    IF (SELECT saldo FROM {0} WHERE idDocumento = @idDocumento) <= 0
                                    BEGIN
	                                    UPDATE {0} SET IdEstado = 5 WHERE idDocumento = @idDocumento;
                                    END;", oTDItem.TablaDocumento);
            SqlCommand oSQL = new SqlCommand(strSQL, Con, Tran);
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", oPagDetI.idDocumento));
            oSQL.Parameters.Add(new SqlParameter("@saldo", oPagDetI.valorAbono));
            try
            {
                oSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex; ;
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
        public bool Guardar(tblDocumentoItem documento, long tipoDocumento, long idEmpresa)
        {
            if (documento.idDocumento > 0)
            {
                return Actualizar(documento);
            }
            else
            {
                return Insertar(documento, tipoDocumento, idEmpresa);
            }
        }
        public bool Eliminar(long idDocumento, long tipoDocumento)
        {
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            string strSQL = string.Format("DELETE FROM {0} WHERE idDocumento = @idDocumento;", oTDItem.TablaDocumento);
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", idDocumento));
            Conexion.Open();
            try
            {
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
        public DataTable FacturasPendienteEntrega(long idEmpresa) {
            try
            {
                SqlCommand oSQL = new SqlCommand("spFacturasPendienteEntrega", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                SqlDataAdapter adapter = new SqlDataAdapter(oSQL);
                DataSet dsReporte = new DataSet();
                adapter.Fill(dsReporte, "Reporte");
                Conexion.Open();
                DataTable dtReporte = dsReporte.Tables[0];
                return dtReporte;
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
        }
    }
}
