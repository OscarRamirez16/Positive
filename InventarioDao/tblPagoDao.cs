using System;
using System.Collections.Generic;
using InventarioItem;
using System.Data.SqlClient;
using System.Data;

namespace InventarioDao
{
    public class tblPagoDao
    {

        private SqlConnection Conexion { get; set; }

        public tblPagoDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public DataTable ObtenerConciliaciones(DateTime FechaInicial, DateTime FechaFinal, string Cliente, string Identificacion, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerConciliaciones", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
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
        public DataTable ObtenerAnticipos(DateTime FechaInicial, DateTime FechaFinal, string Cliente, string Identificacion, string IdAnticipo, long IdEmpresa)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spObtenerAnticipos", Conexion);
                oSQL.CommandType = CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                oSQL.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
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
                if (string.IsNullOrEmpty(IdAnticipo))
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdAnticipo", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdAnticipo", IdAnticipo));
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

        public tblPagoItem ObtenerPago(long Id, int Tipo)
        {
            tblPagoItem Item = new tblPagoItem();
            SqlCommand oSQL = new SqlCommand("spObtenerPago", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", Id));
                oSQL.Parameters.Add(new SqlParameter("@Tipo", Tipo));
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

        public List<tblPagoItem> ObtenerPagosPorCliente(long idCliente, DateTime fechaInical, DateTime fechaFinal)
        {
            List<tblPagoItem> Lista = new List<tblPagoItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPagosPorCliente @id,@fechaInicial,@fechaFinal", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", idCliente));
                oSQL.Parameters.Add(new SqlParameter("@fechaInicial", fechaInical));
                oSQL.Parameters.Add(new SqlParameter("@fechaFinal", fechaFinal));
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

        public List<tblPagoItem> ObtenerPagosPorProveedor(long IdProveedor, DateTime fechaInical, DateTime fechaFinal)
        {
            List<tblPagoItem> Lista = new List<tblPagoItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPagosPorProveedor @id,@fechaInicial,@fechaFinal", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", IdProveedor));
                oSQL.Parameters.Add(new SqlParameter("@fechaInicial", fechaInical));
                oSQL.Parameters.Add(new SqlParameter("@fechaFinal", fechaFinal));
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

        public List<tblPagoItem> ObtenerPagoLista()
        {
            List<tblPagoItem> Lista = new List<tblPagoItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerPagoLista", Conexion);
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

        private tblPagoItem ObtenerItem(SqlDataReader reader)
        {
            tblPagoItem Item = new tblPagoItem();
            Item.idPago = long.Parse(reader["idPago"].ToString());
            Item.idTercero = long.Parse(reader["idTercero"].ToString());
            Item.Tercero = reader["Tercero"].ToString();
            Item.totalPago = decimal.Parse(reader["totalPago"].ToString());
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.fechaPago = DateTime.Parse(reader["fechaPago"].ToString());
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.idEstado = short.Parse(reader["idEstado"].ToString());
            Item.Observaciones = reader["Observaciones"].ToString();
            return Item;
        }

        public bool GuardarPago(tblPagoItem pago, List<tblPagoDetalleItem> detallePago, List<tblTipoPagoItem> tipoPago, long tipoDocumento)
        {
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            string SQL = "";
            if (tipoDocumento == 1 || tipoDocumento == 10 || tipoDocumento == 11)
            {
                SQL = "spInsertarPago";
            }
            if (tipoDocumento == 2)
            {
                SQL = "spInsertarPagoCompra";
            }
            SqlCommand oSQL = new SqlCommand(SQL, Conexion, oTran);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idTercero", pago.idTercero));
            oSQL.Parameters.Add(new SqlParameter("@totalPago", pago.totalPago));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", pago.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@fechaPago", pago.fechaPago));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", pago.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEstado", pago.idEstado));
            if (!string.IsNullOrEmpty(pago.Observaciones))
            {
                oSQL.Parameters.Add(new SqlParameter("@Observaciones", pago.Observaciones));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Observaciones", DBNull.Value));
            }
            oSQL.Parameters.Add(new SqlParameter("@EnCuadre", pago.EnCuadre));
            oSQL.Parameters.Add(new SqlParameter("@IdTipoPago", pago.IdTipoPago));
            if (pago.Saldo != 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@Saldo", pago.Saldo));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Saldo", DBNull.Value));
            }
            try
            {
                pago.idPago = long.Parse(oSQL.ExecuteScalar().ToString());
                if (pago.idPago > 0)
                {
                    tblPagoDetalleDao oPagDetD = new tblPagoDetalleDao(Conexion.ConnectionString);
                    tblTipoPagoDao oPagTipD = new tblTipoPagoDao(Conexion.ConnectionString);
                    tblDocumentoDao oDocD = new tblDocumentoDao(Conexion.ConnectionString);
                    foreach (tblPagoDetalleItem oPagDetI in detallePago)
                    {
                        if (oPagDetI.valorAbono > 0)
                        {
                            oPagDetI.idPago = pago.idPago;
                            oPagDetD.Guardar(oPagDetI, Conexion, oTran, tipoDocumento);
                        }
                        else
                        {
                            oTran.Rollback();
                            return false;
                        }
                    }
                    foreach (tblTipoPagoItem oTipPagI in tipoPago)
                    {
                        if (oTipPagI.ValorPago > 0)
                        {
                            oTipPagI.idPago = pago.idPago;
                            oPagTipD.Guardar(oTipPagI, Conexion, oTran, tipoDocumento);
                        }
                        else
                        {
                            oTran.Rollback();
                            return false;
                        }
                    }
                }
                oTran.Commit();
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

        public bool GuardarPagoConTransaccion(tblPagoItem Item, List<tblPagoDetalleItem> detallePago, List<tblTipoPagoItem> tipoPago, long tipoDocumento, SqlConnection oCon, SqlTransaction oTran)
        {
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(oCon.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumentoConTransaccion(tipoDocumento, oCon, oTran);
            string SQL = "";
            if (tipoDocumento == 1 || tipoDocumento == 11)
            {
                SQL = "spInsertarPago";
            }
            if (tipoDocumento == 2)
            {
                SQL = "spInsertarPagoCompra";
            }
            SqlCommand oSQL = new SqlCommand(SQL, oCon, oTran);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idTercero", Item.idTercero));
            oSQL.Parameters.Add(new SqlParameter("@totalPago", Item.totalPago));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@fechaPago", Item.fechaPago));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEstado", Item.idEstado));
            if (!string.IsNullOrEmpty(Item.Observaciones))
            {
                oSQL.Parameters.Add(new SqlParameter("@Observaciones", Item.Observaciones));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Observaciones", DBNull.Value));
            }
            oSQL.Parameters.Add(new SqlParameter("@EnCuadre", Item.EnCuadre));
            oSQL.Parameters.Add(new SqlParameter("@IdTipoPago", Item.IdTipoPago));
            if (Item.Saldo != 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@Saldo", Item.Saldo));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@Saldo", DBNull.Value));
            }
            try
            {
                Item.idPago = long.Parse(oSQL.ExecuteScalar().ToString());
                if (Item.idPago > 0)
                {
                    tblPagoDetalleDao oPagDetD = new tblPagoDetalleDao(oCon.ConnectionString);
                    tblTipoPagoDao oPagTipD = new tblTipoPagoDao(oCon.ConnectionString);
                    tblDocumentoDao oDocD = new tblDocumentoDao(oCon.ConnectionString);
                    foreach (tblPagoDetalleItem oPagDetI in detallePago)
                    {
                        if (oPagDetI.valorAbono > 0)
                        {
                            oPagDetI.idPago = Item.idPago;
                            oPagDetD.Guardar(oPagDetI, oCon, oTran, tipoDocumento);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    foreach (tblTipoPagoItem oTipPagI in tipoPago)
                    {
                        if (oTipPagI.ValorPago > 0)
                        {
                            oTipPagI.idPago = Item.idPago;
                            oPagTipD.Guardar(oTipPagI, oCon, oTran, tipoDocumento);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
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

        private bool Insertar(tblPagoItem Item, tblPagoDetalleItem detallePago, tblTipoPagoItem tipoPago, long tipoDocumento)
        {
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarPago @idTercero,@totalPago,@idEmpresa,@fechaPago,@idUsuario,@idEstado", Conexion, oTran);
            oSQL.Parameters.Add(new SqlParameter("@idTercero", Item.idTercero));
            oSQL.Parameters.Add(new SqlParameter("@totalPago", Item.totalPago));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@fechaPago", Item.fechaPago));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEstado", Item.idEstado));
            try
            {
                Item.idPago = long.Parse(oSQL.ExecuteScalar().ToString());
                tblPagoDetalleDao oDetPagD = new tblPagoDetalleDao(Conexion.ConnectionString);
                tblTipoPagoDao oTipPagD = new tblTipoPagoDao(Conexion.ConnectionString);
                detallePago.idPago = Item.idPago;
                tipoPago.idPago = Item.idPago;
                if (oDetPagD.Guardar(detallePago, Conexion, oTran, tipoDocumento))
                {
                    if (oTipPagD.Guardar(tipoPago, Conexion, oTran, tipoDocumento))
                    {
                        decimal saldo = Item.totalPago - detallePago.valorAbono;
                        string strSQL = string.Format("UPDATE {0} SET saldo = {1} WHERE idDocumento = @idDocumento;", oTDItem.TablaDocumento, saldo);
                        SqlCommand oSQLCom = new SqlCommand(strSQL, Conexion, oTran);
                        oSQLCom.Parameters.Add(new SqlParameter("@idDocumento", detallePago.idDocumento));
                        oSQLCom.ExecuteNonQuery();
                        oTran.Commit();
                    }
                }
            }
            catch (Exception ex)
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

        public bool InsertarPagoCompra(tblPagoItem Item, tblPagoDetalleItem detallePago, tblTipoPagoItem tipoPago, long tipoDocumento)
        {
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            SqlCommand oSQL = new SqlCommand("EXEC spInsertarPagoCompra @idTercero,@totalPago,@idEmpresa,@fechaPago,@idUsuario,@idEstado", Conexion, oTran);
            oSQL.Parameters.Add(new SqlParameter("@idTercero", Item.idTercero));
            oSQL.Parameters.Add(new SqlParameter("@totalPago", Item.totalPago));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@fechaPago", Item.fechaPago));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEstado", Item.idEstado));
            try
            {
                Item.idPago = long.Parse(oSQL.ExecuteScalar().ToString());
                tblPagoDetalleDao oDetPagD = new tblPagoDetalleDao(Conexion.ConnectionString);
                tblTipoPagoDao oTipPagD = new tblTipoPagoDao(Conexion.ConnectionString);
                detallePago.idPago = Item.idPago;
                tipoPago.idPago = Item.idPago;
                if (oDetPagD.InsertarPagoCompraDetalle(detallePago, Conexion, oTran))
                {
                    if (oTipPagD.InsertarTipoPagoCompra(tipoPago, Conexion, oTran))
                    {
                        decimal saldo = Item.totalPago - detallePago.valorAbono;
                        string strSQL = string.Format("UPDATE {0} SET saldo = {1} WHERE idDocumento = @idDocumento;", oTDItem.TablaDocumento, saldo);
                        SqlCommand oSQLCom = new SqlCommand(strSQL, Conexion, oTran);
                        oSQLCom.Parameters.Add(new SqlParameter("@idDocumento", detallePago.idDocumento));
                        oSQLCom.ExecuteNonQuery();
                        oTran.Commit();
                    }
                }
            }
            catch (Exception ex)
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

        private bool Actualizar(tblPagoItem Item)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spActualizarPago @idPago,@idTercero,@totalPago,@idEmpresa,@fechaPago,@idUsuario,@idEstado", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idPago", Item.idPago));
            oSQL.Parameters.Add(new SqlParameter("@idTercero", Item.idTercero));
            oSQL.Parameters.Add(new SqlParameter("@totalPago", Item.totalPago));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@fechaPago", Item.fechaPago));
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idEstado", Item.idEstado));
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

        public bool Guardar(tblPagoItem Item, tblPagoDetalleItem detallePago, tblTipoPagoItem tipoPago, long tipoDocumento)
        {
            return Insertar(Item, detallePago, tipoPago, tipoDocumento);
        }

        public bool GuardarDetallesPago(tblPagoItem Item, tblPagoDetalleItem detallePago, tblTipoPagoItem tipoPago, long tipoDocumento)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                tblPagoDetalleDao oDetPagD = new tblPagoDetalleDao(Conexion.ConnectionString);
                tblTipoPagoDao oTipPagD = new tblTipoPagoDao(Conexion.ConnectionString);
                detallePago.idPago = Item.idPago;
                tipoPago.idPago = Item.idPago;
                if (oDetPagD.Guardar(detallePago, Conexion, oTran, tipoDocumento))
                {
                    if (oTipPagD.Guardar(tipoPago, Conexion, oTran, tipoDocumento))
                    {
                        tblTipoDocumentoItem oTDItem;
                        tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
                        oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
                        decimal saldo = Item.totalPago - detallePago.valorAbono;
                        string strSQL = string.Format("UPDATE {0} SET saldo = {1} WHERE idDocumento = @idDocumento;", oTDItem.TablaDocumento, saldo);
                        SqlCommand oSQLCom = new SqlCommand(strSQL, Conexion, oTran);
                        oSQLCom.Parameters.Add(new SqlParameter("@idDocumento", detallePago.idDocumento));
                        oSQLCom.ExecuteNonQuery();
                        oTran.Commit();
                    }
                }
            }
            catch (Exception ex)
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
    }
}
