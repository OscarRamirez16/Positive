using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventarioItem;

namespace InventarioDao
{
    public class tblDetalleDocumentoDao
    {

        private SqlConnection Conexion { get; set; }

        public tblDetalleDocumentoDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblDetalleDocumentoItem ObtenerDetalleDocumento(long Id)
        {
            tblDetalleDocumentoItem Item = new tblDetalleDocumentoItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerDetalleDocumento @id", Conexion);
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

        public List<tblDetalleDocumentoItem> ObtenerDetalleDocumentoLista()
        {
            List<tblDetalleDocumentoItem> Lista = new List<tblDetalleDocumentoItem>();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerDetalleDocumentoLista", Conexion);
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

        private tblDetalleDocumentoItem ObtenerItem(SqlDataReader reader)
        {
            tblDetalleDocumentoItem Item = new tblDetalleDocumentoItem();
            Item.idDetalleDocumento = long.Parse(reader["idDetalleDocumento"].ToString());
            Item.idDocumento = long.Parse(reader["idDocumento"].ToString());
            Item.NumeroLinea = short.Parse(reader["NumeroLinea"].ToString());
            Item.idArticulo = long.Parse(reader["idArticulo"].ToString());
            Item.Codigo = reader["CodigoArticulo"].ToString();
            Item.Articulo = reader["Articulo"].ToString();
            Item.ValorUnitario = decimal.Parse(reader["ValorUnitario"].ToString());
            Item.IVA = decimal.Parse(reader["IVA"].ToString());
            Item.Cantidad = decimal.Parse(reader["Cantidad"].ToString());
            Item.idBodega = long.Parse(reader["idBodega"].ToString());
            Item.Bodega = reader["Bodega"].ToString();
            Item.TotalLinea = decimal.Parse(reader["TotalLinea"].ToString());
            Item.Descuento = decimal.Parse(reader["Descuento"].ToString());
            Item.ValorDescuento = decimal.Parse(reader["ValorDescuento"].ToString());
            Item.PrecioCosto = decimal.Parse(reader["PrecioCosto"].ToString());
            Item.CostoPonderado = decimal.Parse(reader["CostoPonderado"].ToString());
            Item.ValorUnitarioConDescuento = decimal.Parse(reader["ValorUnitarioConDescuento"].ToString());
            Item.ValorUnitarioConIVA = decimal.Parse(reader["ValorUnitarioConIVA"].ToString());
            return Item;
        }

        public bool Insertar(tblDetalleDocumentoItem Item, long tipoDocumento, long idEmpresa, SqlTransaction transaccion = null)
        {
            bool local = false;
            long idDetalleDocumento = 0;
            tblArticuloItem oArticuloItem = new tblArticuloItem();
            tblArticuloDao oArticuloD = new tblArticuloDao(Conexion.ConnectionString);
            oArticuloItem = oArticuloD.ObtenerArticuloPorID(Item.idArticulo, idEmpresa);
            Item.idArticulo = oArticuloItem.IdArticulo;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            tblTipoDocumentoItem oTDItem = new tblTipoDocumentoItem();
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            Conexion.Open();
            if (transaccion == null)
            {
                transaccion = Conexion.BeginTransaction();
                local = true;
            }
            string strSQL = string.Format("INSERT INTO {0} VALUES (@idDocumento,@NumeroLinea,@idArticulo,@Descripcion,@Precio,@Impuesto,@Cantidad,@idBodega); SELECT SCOPE_IDENTITY() AS [idDetalleDocumento]", oTDItem.TablaDetalle);
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion, transaccion);
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", Item.idDocumento));
            oSQL.Parameters.Add(new SqlParameter("@NumeroLinea", Item.NumeroLinea));
            oSQL.Parameters.Add(new SqlParameter("@idArticulo", Item.idArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Articulo));
            oSQL.Parameters.Add(new SqlParameter("@Precio", Item.ValorUnitario));
            oSQL.Parameters.Add(new SqlParameter("@Impuesto", Item.IVA));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.idBodega));
            try
            {
                idDetalleDocumento = long.Parse(((decimal)oSQL.ExecuteScalar()).ToString());
                Item.idDetalleDocumento = idDetalleDocumento;
                if (local)
                {
                    if (oArticuloItem.EsInventario)
                    {
                        Item.Cantidad = Item.Cantidad * oTDItem.SentidoInventario;
                        if (tipoDocumento == 2)
                        {
                            oArticuloD.SumarRestarExistenciasArticuloCosto(Item.Cantidad, Item.idArticulo, Item.idBodega, Item.ValorUnitario, idEmpresa);
                        }
                        else
                        {
                            oArticuloD.SumarRestarExistenciasArticulo(Item.Cantidad, Item.idArticulo, Item.idBodega);
                        }
                    }
                    transaccion.Commit();
                }
            }
            catch (Exception ex)
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

        public bool Actualizar(tblDetalleDocumentoItem Item, long tipoDocumento, SqlTransaction transaccion = null)
        {
            bool local = false;
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            Conexion.Open();
            if (transaccion == null)
            {
                transaccion = Conexion.BeginTransaction();
                local = true;
            }
            string strSQL = string.Format("UPDATE {0} SET idArticulo = @idArticulo, Descripcion = @Descripcion, Precio = @Precio, Impuesto = @Impuesto, Cantidad = @Cantidad WHERE idDocumento = @idDocumento AND idDetalleDocumento = @idDetalleDocumento;", oTDItem.TablaDetalle);
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion, transaccion);
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", Item.idDocumento));
            oSQL.Parameters.Add(new SqlParameter("@idDetalleDocumento", Item.idDetalleDocumento));
            oSQL.Parameters.Add(new SqlParameter("@idArticulo", Item.idArticulo));
            oSQL.Parameters.Add(new SqlParameter("@Descripcion", Item.Articulo));
            oSQL.Parameters.Add(new SqlParameter("@Precio", Item.ValorUnitario));
            oSQL.Parameters.Add(new SqlParameter("@Impuesto", Item.IVA));
            oSQL.Parameters.Add(new SqlParameter("@Cantidad", Item.Cantidad));
            try
            {
                oSQL.ExecuteNonQuery();
                if (local)
                {
                    transaccion.Commit();
                }
            }
            catch (Exception ex)
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

        public List<tblDetalleDocumentoItem> ObtenerDetalleDocumentoListaPorIdDocumento(long idDocumento, string tablaDetalle)
        {
            List<tblDetalleDocumentoItem> Lista = new List<tblDetalleDocumentoItem>();
            string strSQL = string.Format(@"SELECT idDetalleDocumento, 
	                                        idDocumento,
	                                        NumeroLinea,
	                                        {0}.idArticulo,
	                                        CodigoArticulo,
	                                        {0}.Descripcion [Articulo],
	                                        {0}.Precio [ValorUnitario],
	                                        FLOOR(Impuesto) [IVA],
	                                        {0}.Cantidad[Cantidad],
	                                        tblBodega.idBodega,
	                                        tblBodega.Descripcion [Bodega],
	                                        CAST(({0}.Precio * {0}.Cantidad) AS Decimal(18,2)) [TotalLinea],
	                                        FLOOR(ISNULL(Descuento,0))[Descuento],
	                                        0 [ValorDescuento],
                                            tblArticulo_Bodega.Precio [PrecioCosto],
                                            ISNULL({0}.CostoPonderado,0)[CostoPonderado],
	                                        CAST(({0}.Precio + ({0}.Precio * ({0}.Impuesto / 100))) AS Decimal(18,2)) as ValorUnitarioConDescuento,
	                                        CAST((({0}.Precio + ({0}.Precio * ({0}.Impuesto / 100)))/(1-({0}.Descuento / 100))) AS Decimal(18,2)) as ValorUnitarioConIVA
                                        FROM {0}
                                        INNER JOIN tblBodega ON tblBodega.idBodega = {0}.idbodega
                                        INNER JOIN tblArticulo ON tblArticulo.idArticulo = {0}.idArticulo
                                        INNER JOIN tblArticulo_Bodega ON tblArticulo_Bodega.idArticulo = {0}.idArticulo AND tblArticulo_Bodega.idBodega = {0}.idBodega
                                        WHERE idDocumento = @idDocumento
                                        ORDER BY NumeroLinea;", tablaDetalle);
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", idDocumento));
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

        public void DevolverMovimientosFacturaVenta(long idDocumento)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spDevolverMovimientosFacturaVenta @idDocumento ", Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", idDocumento));
            try
            {
                Conexion.Open();
                oSQL.ExecuteNonQuery();
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

        public bool Eliminar(long idDocumento, long tipoDocumento)
        {
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            DevolverMovimientosFacturaVenta(idDocumento);
            string strSQL = string.Format("DELETE FROM {0} WHERE idDocumento = @idDocumento;", oTDItem.TablaDetalle);
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", idDocumento));
            Conexion.Open();
            try
            {
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

        public bool EliminarDetallePorIdDocumentoIdDetalle(long idDocumento, long idDetalle, long tipoDocumento)
        {
            tblTipoDocumentoItem oTDItem;
            tblTipoDocumentoDao oTDDao = new tblTipoDocumentoDao(Conexion.ConnectionString);
            oTDItem = oTDDao.ObtenerTipoDocumento(tipoDocumento);
            string strSQL = string.Format("DELETE FROM {0} WHERE idDocumento = @idDocumento AND idDetalleDocumento = @idDetalleDocumento;", oTDItem.TablaDetalle);
            SqlCommand oSQL = new SqlCommand(strSQL, Conexion);
            oSQL.Parameters.Add(new SqlParameter("@idDocumento", idDocumento));
            oSQL.Parameters.Add(new SqlParameter("@idDetalleDocumento", idDetalle));
            Conexion.Open();
            try
            {
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

    }
}
