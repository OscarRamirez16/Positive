using System;
using System.Collections.Generic;
using InventarioItem;
using System.Data.SqlClient;
using System.Data;

namespace InventarioDao
{
    public class tblTerceroDao
    {
        private SqlConnection Conexion { get; set; }

        public tblTerceroDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }
        public DataTable ObtenerTipoIdentificacionDIAN()
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerTipoIdentificacionDIAN", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
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
        public DataTable ObtenerTipoContribuyente()
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerTipoContribuyente", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
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
        public DataTable ObtenerRegimenFiscal()
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerRegimenFiscal", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
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
        public DataTable ObtenerResponsabilidadFiscal()
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerResponsabilidadFiscal", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
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
        public List<tblTerceroRetencionItem> ObtenerRetencionesPorIdTercero(long IdTercero)
        {
            List<tblTerceroRetencionItem> Lista = new List<tblTerceroRetencionItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerRetencionesPorIdTercero", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdTercero", IdTercero));
            try
            {
                Conexion.Open();
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    tblTerceroRetencionItem oRetI = new tblTerceroRetencionItem();
                    oRetI.IdTercero = long.Parse(reader["IdTercero"].ToString());
                    oRetI.IdRetencion = long.Parse(reader["IdRetencion"].ToString());
                    Lista.Add(oRetI);
                }
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
            return Lista;
        }
        public DataTable ObtenerClientesListaActivos(long IdEmpresa, int GrupoCliente)
        {
            DataTable dt = new DataTable();
            SqlCommand oSQL = new SqlCommand("spObtenerClientesListaActivos", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
            if(GrupoCliente == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@GrupoCliente", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@GrupoCliente", GrupoCliente));
            }
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
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return dt;
        }

        public List<tblTerceroItem> ObtenerTerceroListaPorTipo(long IdEmpresa, string Tipo)
        {
            List<tblTerceroItem> Lista = new List<tblTerceroItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerTerceroListaPorTipo", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@Tipo", Tipo));
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

        public tblTerceroItem ObtenerClienteProveedorGenerico(long IdEmpresa, string Tipo)
        {
            tblTerceroItem Item = new tblTerceroItem();
            SqlCommand oSQL = new SqlCommand("spObtenerClienteProveedorGenerico", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@TipoTercero", Tipo));
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

        public tblTerceroItem ObtenerTerceroPorId(long IdTercero, long IdEmpresa)
        {
            tblTerceroItem Item = new tblTerceroItem();
            SqlCommand oSQL = new SqlCommand("spObtenerTerceroPorId", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdTercero", IdTercero));
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
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

        public tblTerceroItem ObtenerTercero(long Id, long IdEmpresa)
        {
            tblTerceroItem Item = new tblTerceroItem();
            SqlCommand oSQL = new SqlCommand("spObtenerTercero", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@id", Id));
                oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
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

        public tblTerceroItem ObtenerTerceroPorIdentificacion(string identificacion, long idEmpresa)
        {
            tblTerceroItem Item = new tblTerceroItem();
            SqlCommand oSQL = new SqlCommand("spObtenerTerceroPorIdentificacion", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@identificacion", identificacion));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
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

        public List<tblTerceroItem> ObtenerTerceroLista(long IdEmpresa, string TipoTercero, int GrupoCliente)
        {
            List<tblTerceroItem> Lista = new List<tblTerceroItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerTerceroLista", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@TipoTercero", TipoTercero));
            if(GrupoCliente == 0)
            {
                oSQL.Parameters.Add(new SqlParameter("@GrupoCliente", DBNull.Value));
            }
            else
            {
                oSQL.Parameters.Add(new SqlParameter("@GrupoCliente", GrupoCliente));
            }
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

        private tblTerceroItem ObtenerItem(SqlDataReader reader)
        {
            tblTerceroItem Item = new tblTerceroItem();
            Item.IdTercero = long.Parse(reader["idTercero"].ToString());
            Item.idTipoIdentificacion = reader["idTipoIdentificacion"].ToString();
            Item.Identificacion = reader["Identificacion"].ToString();
            Item.Nombre = reader["Nombre"].ToString();
            Item.Telefono = reader["Telefono"].ToString();
            Item.Celular = reader["Celular"].ToString();
            Item.Mail = reader["Mail"].ToString();
            Item.Direccion = reader["Direccion"].ToString();
            Item.idCiudad = short.Parse(reader["idCiudad"].ToString());
            Item.Ciudad = reader["Ciudad"].ToString();
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.Empresa = reader["Empresa"].ToString();
            Item.TipoTercero = reader["TipoTercero"].ToString();
            if (reader["FechaNacimiento"].ToString() != "" )
            {
                Item.FechaNacimiento = DateTime.Parse(reader["FechaNacimiento"].ToString());
            }
            if (reader["IdListaPrecio"].ToString() != "")
            {
                Item.IdListaPrecio = long.Parse(reader["IdListaPrecio"].ToString());
            }
            Item.idGrupoCliente = long.Parse(reader["idGrupoCliente"].ToString());
            Item.GrupoCliente = reader["GrupoCliente"].ToString();
            Item.Generico = bool.Parse(reader["Generico"].ToString());
            Item.Observaciones = reader["Observaciones"].ToString();
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.TipoIdentificacionDIAN = reader["TipoIdentificacionDIAN"].ToString();
            Item.CodigoDepartamento = reader["CodigoDepartamento"].ToString();
            Item.Departamento = reader["Departamento"].ToString();
            Item.CodigoCiudad = reader["CodigoCiudad"].ToString();
            Item.MatriculaMercantil = reader["MatriculaMercantil"].ToString();
            Item.TipoContribuyente = reader["TipoContribuyente"].ToString();
            Item.RegimenFiscal = reader["RegimenFiscal"].ToString();
            Item.CodigoResponsabilidadFiscal = reader["CodigoResponsabilidadFiscal"].ToString();
            Item.ResponsabilidadFiscal = reader["ResponsabilidadFiscal"].ToString();
            Item.CodigoZip = reader["CodigoZip"].ToString();
            return Item;
        }
        private string Insertar(tblTerceroItem Item)
        {
            string Error = string.Empty;
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                SqlCommand oSQL = new SqlCommand("spInsertarTercero", Conexion, oTran);
                oSQL.Parameters.Add(new SqlParameter("@idTipoIdentificacion", Item.idTipoIdentificacion));
                oSQL.Parameters.Add(new SqlParameter("@Identificacion", Item.Identificacion));
                oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
                if (string.IsNullOrEmpty(Item.Telefono))
                {
                    oSQL.Parameters.Add(new SqlParameter("@Telefono", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@Telefono", Item.Telefono));
                }
                if (string.IsNullOrEmpty(Item.Celular))
                {
                    oSQL.Parameters.Add(new SqlParameter("@Celular", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@Celular", Item.Celular));
                }
                if (string.IsNullOrEmpty(Item.Mail))
                {
                    oSQL.Parameters.Add(new SqlParameter("@Mail", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@Mail", Item.Mail));
                }
                oSQL.Parameters.Add(new SqlParameter("@Direccion", Item.Direccion));
                oSQL.Parameters.Add(new SqlParameter("@idCiudad", Item.idCiudad));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@TipoTercero", Item.TipoTercero));
                if (Item.FechaNacimiento != null && Item.FechaNacimiento != DateTime.MinValue)
                {
                    oSQL.Parameters.Add(new SqlParameter("@FechaNacimiento", Item.FechaNacimiento));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@FechaNacimiento", DBNull.Value));
                }
                if (Item.IdListaPrecio != 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdListaPrecio", Item.IdListaPrecio));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdListaPrecio", DBNull.Value));
                }
                if (Item.idGrupoCliente != 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@idGrupoCliente", Item.idGrupoCliente));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@idGrupoCliente", DBNull.Value));
                }
                if (string.IsNullOrEmpty(Item.Observaciones))
                {
                    oSQL.Parameters.Add(new SqlParameter("@Observaciones", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@Observaciones", Item.Observaciones));
                }
                oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
                oSQL.Parameters.Add(new SqlParameter("@TipoIdentificacionDIAN", Item.TipoIdentificacionDIAN));
                oSQL.Parameters.Add(new SqlParameter("@MatriculaMercantil", Item.MatriculaMercantil));
                oSQL.Parameters.Add(new SqlParameter("@TipoContribuyente", Item.TipoContribuyente));
                oSQL.Parameters.Add(new SqlParameter("@RegimenFiscal", Item.RegimenFiscal));
                oSQL.Parameters.Add(new SqlParameter("@ResponsabilidadFiscal", Item.ResponsabilidadFiscal));
                if (string.IsNullOrEmpty(Item.CodigoZip))
                {
                    oSQL.Parameters.Add(new SqlParameter("@CodigoZip", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@CodigoZip", Item.CodigoZip));
                }
                oSQL.CommandType = CommandType.StoredProcedure;
                Item.IdTercero = long.Parse(oSQL.ExecuteScalar().ToString());
                if(Item.IdTercero > 0)
                {
                    SqlCommand oSQL1 = new SqlCommand("spEliminarTerceroRetencion", Conexion, oTran);
                    oSQL1.CommandType = CommandType.StoredProcedure;
                    oSQL1.Parameters.Add(new SqlParameter("@IdTercero", Item.IdTercero));
                    oSQL1.ExecuteNonQuery();
                    foreach (tblTerceroRetencionItem oRetI in Item.Retenciones)
                    {
                        SqlCommand oSQL2 = new SqlCommand("spInsertarTerceroRetencion", Conexion, oTran);
                        oSQL2.CommandType = CommandType.StoredProcedure;
                        oSQL2.Parameters.Add(new SqlParameter("@IdTercero", Item.IdTercero));
                        oSQL2.Parameters.Add(new SqlParameter("@IdRetencion", oRetI.IdRetencion));
                        oSQL2.ExecuteNonQuery();
                    }
                    oTran.Commit();
                }
                else
                {
                    Error = "No se pudo guardar el tercero";
                }
            }
            catch(Exception ex)
            {
                oTran.Rollback();
                return ex.Message;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Error;
        }
        private string Actualizar(tblTerceroItem Item)
        {
            string Error = string.Empty;
            Conexion.Open();
            SqlTransaction oTran;
            oTran = Conexion.BeginTransaction();
            try
            {
                SqlCommand oSQL = new SqlCommand("spActualizarTercero", Conexion, oTran);
                oSQL.Parameters.Add(new SqlParameter("@idTercero", Item.IdTercero));
                oSQL.Parameters.Add(new SqlParameter("@idTipoIdentificacion", Item.idTipoIdentificacion));
                oSQL.Parameters.Add(new SqlParameter("@Identificacion", Item.Identificacion));
                oSQL.Parameters.Add(new SqlParameter("@Nombre", Item.Nombre));
                oSQL.Parameters.Add(new SqlParameter("@Telefono", Item.Telefono));
                oSQL.Parameters.Add(new SqlParameter("@Celular", Item.Celular));
                oSQL.Parameters.Add(new SqlParameter("@Mail", Item.Mail));
                oSQL.Parameters.Add(new SqlParameter("@Direccion", Item.Direccion));
                oSQL.Parameters.Add(new SqlParameter("@idCiudad", Item.idCiudad));
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
                oSQL.Parameters.Add(new SqlParameter("@TipoTercero", Item.TipoTercero));
                if (Item.FechaNacimiento != null && Item.FechaNacimiento != DateTime.MinValue)
                {
                    oSQL.Parameters.Add(new SqlParameter("@FechaNacimiento", Item.FechaNacimiento));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@FechaNacimiento", DBNull.Value));
                }
                if (Item.IdListaPrecio != 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdListaPrecio", Item.IdListaPrecio));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@IdListaPrecio", DBNull.Value));
                }
                if (Item.idGrupoCliente != 0)
                {
                    oSQL.Parameters.Add(new SqlParameter("@idGrupoCliente", Item.idGrupoCliente));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@idGrupoCliente", DBNull.Value));
                }
                if (string.IsNullOrEmpty(Item.Observaciones))
                {
                    oSQL.Parameters.Add(new SqlParameter("@Observaciones", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@Observaciones", Item.Observaciones));
                }
                oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
                oSQL.Parameters.Add(new SqlParameter("@TipoIdentificacionDIAN", Item.TipoIdentificacionDIAN));
                oSQL.Parameters.Add(new SqlParameter("@MatriculaMercantil", Item.MatriculaMercantil));
                oSQL.Parameters.Add(new SqlParameter("@TipoContribuyente", Item.TipoContribuyente));
                oSQL.Parameters.Add(new SqlParameter("@RegimenFiscal", Item.RegimenFiscal));
                oSQL.Parameters.Add(new SqlParameter("@ResponsabilidadFiscal", Item.ResponsabilidadFiscal));
                if (string.IsNullOrEmpty(Item.CodigoZip))
                {
                    oSQL.Parameters.Add(new SqlParameter("@CodigoZip", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@CodigoZip", Item.CodigoZip));
                }
                oSQL.CommandType = CommandType.StoredProcedure;
                if (oSQL.ExecuteNonQuery() > 0)
                {
                    SqlCommand oSQL1 = new SqlCommand("spEliminarTerceroRetencion", Conexion, oTran);
                    oSQL1.CommandType = CommandType.StoredProcedure;
                    oSQL1.Parameters.Add(new SqlParameter("@IdTercero", Item.IdTercero));
                    oSQL1.ExecuteNonQuery();
                    foreach (tblTerceroRetencionItem oRetI in Item.Retenciones)
                    {
                        SqlCommand oSQL2 = new SqlCommand("spInsertarTerceroRetencion", Conexion, oTran);
                        oSQL2.CommandType = CommandType.StoredProcedure;
                        oSQL2.Parameters.Add(new SqlParameter("@IdTercero", Item.IdTercero));
                        oSQL2.Parameters.Add(new SqlParameter("@IdRetencion", oRetI.IdRetencion));
                        oSQL2.ExecuteNonQuery();
                    }
                    oTran.Commit();
                }
                else
                {
                    Error = "No se pudo actualizar el tercero";
                }
            }
            catch(Exception ex)
            {
                oTran.Rollback();
                return ex.Message;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Error;
        }
        public string Guardar(tblTerceroItem Item)
        {
            if (Item.IdTercero > 0)
            {
                return Actualizar(Item);
            }
            else
            {
                return Insertar(Item);
            }
        }
        public List<tblTerceroItem> ObtenerTerceroListaPorFiltrosNombreCiudadEmpresa(long idTercero, string cedula, long idEmpresa, string tipoTercero)
        {
            List<tblTerceroItem> Lista = new List<tblTerceroItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerTerceroListaPorFiltrosIDCedulaEmpresaTipoTercero", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                if(idTercero == -1)
                {
                    oSQL.Parameters.Add(new SqlParameter("@idTercero", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@idTercero", idTercero));
                }
                if (string.IsNullOrEmpty(cedula))
                {
                    oSQL.Parameters.Add(new SqlParameter("@cedula", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@cedula", cedula));
                }
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                if (tipoTercero == "-1")
                {
                    oSQL.Parameters.Add(new SqlParameter("@tipoTercero", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@tipoTercero", tipoTercero));
                }
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

        private bsq_ObtenerTerceroListaPorFiltrosNombreCiudadEmpresaItem ObtenerItemFiltro(SqlDataReader reader)
        {
            bsq_ObtenerTerceroListaPorFiltrosNombreCiudadEmpresaItem Item = new bsq_ObtenerTerceroListaPorFiltrosNombreCiudadEmpresaItem();
            Item.idTercero = long.Parse(reader["idTercero"].ToString());
            Item.Identificacion = reader["Identificacion"].ToString();
            Item.Nombre = reader["Nombre"].ToString();
            Item.Telefono = reader["Telefono"].ToString();
            Item.Mail = reader["Mail"].ToString();
            Item.Direccion = reader["Direccion"].ToString();
            Item.Ciudad = reader["Ciudad"].ToString();
            Item.Empresa = reader["Empresa"].ToString();
            Item.TipoTercero = reader["TipoTercero"].ToString();
            return Item;
        }

        public List<JSONItem> ObtenerTerceroListaPorNombre(string nombre, string tipoTercero, long idEmpresa)
        {
            List<JSONItem> Lista = new List<JSONItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerTerceroListaPorNombre", Conexion);
            oSQL.CommandType = CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@nombre", nombre));
            oSQL.Parameters.Add(new SqlParameter("@tipoTercero", tipoTercero));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
            try
            {
                Conexion.Open();
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(ObtenerDatosTercero(reader));
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

        private JSONItem ObtenerDatosTercero(SqlDataReader reader)
        {
            return new JSONItem(reader["datosCliente"].ToString(), reader["Nombre"].ToString());
        }
    }
}
