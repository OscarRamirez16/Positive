using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventarioItem;

namespace InventarioDao
{
    public class tblConciliacionDao
    {
        private SqlConnection Conexion { get; set; }

        public tblConciliacionDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public tblConciliacionItem ObtenerConciliacion(long Id)
        {
            tblConciliacionItem Item = new tblConciliacionItem();
            SqlCommand oSQL = new SqlCommand("spObtenerConciliacion", Conexion);
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

        public List<tblConciliacionItem> ObtenerConciliacionLista()
        {
            List<tblConciliacionItem> Lista = new List<tblConciliacionItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerConciliacionLista", Conexion);
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

        private tblConciliacionItem ObtenerItem(SqlDataReader reader)
        {
            tblConciliacionItem Item = new tblConciliacionItem();
            Item.IdConciliacion = long.Parse(reader["IdConciliacion"].ToString());
            Item.Fecha = DateTime.Parse(reader["Fecha"].ToString());
            Item.IdTercero = long.Parse(reader["IdTercero"].ToString());
            Item.IdUsuario = long.Parse(reader["IdUsuario"].ToString());
            Item.IdRetiro = long.Parse(reader["IdRetiro"].ToString());
            Item.Observaciones = reader["Observaciones"].ToString();
            return Item;
        }

        private void InsertarDetalle(tblConciliacionDetalleItem Item, SqlConnection oCon, SqlTransaction oTran)
        {
            try
            {
                SqlCommand oSQL = new SqlCommand("spInsertarConciliacionDetalle", oCon, oTran);
                oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                oSQL.Parameters.Add(new SqlParameter("@IdConciliacion", Item.IdConciliacion));
                oSQL.Parameters.Add(new SqlParameter("@TipoDocumento", Item.TipoDocumento));
                oSQL.Parameters.Add(new SqlParameter("@NumeroDocumento", Item.NumeroDocumento));
                oSQL.Parameters.Add(new SqlParameter("@Valor", Item.Valor));
                oSQL.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private bool Insertar(tblConciliacionItem Item, List<tblConciliacionDetalleItem> oListDet, tblMovimientosDiariosItem oMovI)
        {
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                tblMovimientosDiariosDao oMovD = new tblMovimientosDiariosDao(Conexion.ConnectionString);
                if(oMovD.InsertarConTransaccion(oMovI, Conexion, oTran))
                {
                    SqlCommand oSQL = new SqlCommand("spInsertarConciliacion", Conexion, oTran);
                    oSQL.CommandType = System.Data.CommandType.StoredProcedure;
                    oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
                    oSQL.Parameters.Add(new SqlParameter("@IdTercero", Item.IdTercero));
                    oSQL.Parameters.Add(new SqlParameter("@IdUsuario", Item.IdUsuario));
                    oSQL.Parameters.Add(new SqlParameter("@IdRetiro", oMovI.idMovimiento));
                    if (string.IsNullOrEmpty(Item.Observaciones))
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Observaciones", DBNull.Value));
                    }
                    else
                    {
                        oSQL.Parameters.Add(new SqlParameter("@Observaciones", Item.Observaciones));
                    }
                    oSQL.Parameters.Add(new SqlParameter("@Total", Item.Total));
                    oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", Item.IdEmpresa));
                    Item.IdConciliacion = long.Parse(oSQL.ExecuteScalar().ToString());
                    foreach(tblConciliacionDetalleItem Detalle in oListDet)
                    {
                        Detalle.IdConciliacion = Item.IdConciliacion;
                        InsertarDetalle(Detalle, Conexion, oTran);
                    }
                    oTran.Commit();
                }
                else
                {
                    oTran.Rollback();
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

        private bool Actualizar(tblConciliacionItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarConciliacion", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdConciliacion", Item.IdConciliacion));
            oSQL.Parameters.Add(new SqlParameter("@Fecha", Item.Fecha));
            oSQL.Parameters.Add(new SqlParameter("@IdTercero", Item.IdTercero));
            oSQL.Parameters.Add(new SqlParameter("@IdUsuario", Item.IdUsuario));
            oSQL.Parameters.Add(new SqlParameter("@IdRetiro", Item.IdRetiro));
            oSQL.Parameters.Add(new SqlParameter("@Observaciones", Item.Observaciones));
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

        public bool Guardar(tblConciliacionItem Item, List<tblConciliacionDetalleItem> oListDet, tblMovimientosDiariosItem oMovI)
        {
            if (Item.IdConciliacion > 0)
            {
                return Actualizar(Item);
            }
            else
            {
                return Insertar(Item, oListDet, oMovI);
            }
        }
    }
}
