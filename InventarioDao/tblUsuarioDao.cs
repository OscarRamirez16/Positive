using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventarioItem;

namespace InventarioDao
{
    public class tblUsuarioDao
    {
        private SqlConnection Conexion { get; set; }

        public tblUsuarioDao(string CadenaConexion)
        {
            Conexion = new SqlConnection(CadenaConexion);
        }

        public bool ModificarIdiomaUsuario(long IdUsuario, short IdIdioma)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spModificarIdiomaUsuario @IdUsuario,@IdIdioma", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                oSQL.Parameters.Add(new SqlParameter("@IdIdioma", IdIdioma));
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

        public bool CambiarContrasena(string Password, long IdUsuario)
        {
            SqlCommand oSQL = new SqlCommand("EXEC spCambiarContrasena @Password,@IdUsuario", Conexion);
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@Password", Password));
                oSQL.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
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

        public tblUsuarioItem ObtenerUsuario(long Id)
        {
            tblUsuarioItem Item = new tblUsuarioItem();
            SqlCommand oSQL = new SqlCommand("EXEC spObtenerUsuario @id", Conexion);
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

        public List<tblUsuarioItem> ObtenerUsuarioListaPorIdEmpresa( long idEmpresa)
        {
            List<tblUsuarioItem> Lista = new List<tblUsuarioItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerUsuarioListaPorIdEmpresa", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
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

        public List<tblUsuarioItem> ObtenerUsuarioActivoLista(long IdEmpresa)
        {
            List<tblUsuarioItem> Lista = new List<tblUsuarioItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerUsuarioActivoLista", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", IdEmpresa));
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

        private tblUsuarioItem ObtenerItem(SqlDataReader reader)
        {
            tblUsuarioItem Item = new tblUsuarioItem();
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.idTipoIdentificacion = short.Parse(reader["idTipoIdentificacion"].ToString());
            Item.Identificacion = reader["Identificacion"].ToString();
            Item.Usuario = reader["Usuario"].ToString();
            Item.Contrasena = reader["Contrasena"].ToString();
            Item.NombreCompleto = reader["NombreCompleto"].ToString();
            Item.PrimerNombre = reader["PrimerNombre"].ToString();
            Item.SegundoNombre = reader["SegundoNombre"].ToString();
            Item.PrimerApellido = reader["PrimerApellido"].ToString();
            Item.SegundoApellido = reader["SegundoApellido"].ToString();
            Item.Correo = reader["Correo"].ToString();
            Item.Telefono = reader["Telefono"].ToString();
            Item.Celular = reader["Celular"].ToString();
            Item.Direccion = reader["Direccion"].ToString();
            Item.idCiudad = short.Parse(reader["idCiudad"].ToString());
            Item.idEmpresa = long.Parse(reader["idEmpresa"].ToString());
            Item.idBodega = long.Parse(reader["idBodega"].ToString());
            Item.Activo = bool.Parse(reader["Activo"].ToString());
            Item.IdIdioma = short.Parse(reader["IdIdioma"].ToString());
            Item.ModificaPrecio = bool.Parse(reader["ModificaPrecio"].ToString());
            Item.EsAdmin = bool.Parse(reader["EsAdmin"].ToString());
            Item.PorcentajeDescuento = decimal.Parse(reader["PorcentajeDescuento"].ToString());
            Item.ManejaPrecioConIVA = bool.Parse(reader["ManejaPrecioConIVA"].ToString());
            Item.ManejaCostoConIVA = bool.Parse(reader["ManejaCostoConIVA"].ToString());
            Item.ManejaDescuentoConIVA = bool.Parse(reader["ManejaDescuentoConIVA"].ToString());
            Item.VerCuadreCaja = bool.Parse(reader["VerCuadreCaja"].ToString());
            Item.Impoconsumo = decimal.Parse(reader["Impoconsumo"].ToString());
            Item.PosicionInicialCodigo = int.Parse(reader["PosicionInicialCodigo"].ToString());
            Item.LongitudCodigo = int.Parse(reader["LongitudCodigo"].ToString());
            Item.PosicionInicialCantidad = int.Parse(reader["PosicionInicialCantidad"].ToString());
            Item.LongitudCantidad = int.Parse(reader["LongitudCantidad"].ToString());
            Item.IdTercero = long.Parse(reader["IdTercero"].ToString());
            return Item;
        }

        private long Insertar(tblUsuarioItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spInsertarUsuario", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idTipoIdentificacion", Item.idTipoIdentificacion));
            oSQL.Parameters.Add(new SqlParameter("@Identificacion", Item.Identificacion));
            oSQL.Parameters.Add(new SqlParameter("@Usuario", Item.Usuario));
            oSQL.Parameters.Add(new SqlParameter("@Contrasena", Item.Contrasena));
            oSQL.Parameters.Add(new SqlParameter("@NombreCompleto", Item.NombreCompleto));
            oSQL.Parameters.Add(new SqlParameter("@PrimerNombre", Item.PrimerNombre));
            oSQL.Parameters.Add(new SqlParameter("@SegundoNombre", Item.SegundoNombre));
            oSQL.Parameters.Add(new SqlParameter("@PrimerApellido", Item.PrimerApellido));
            oSQL.Parameters.Add(new SqlParameter("@SegundoApellido", Item.SegundoApellido));
            oSQL.Parameters.Add(new SqlParameter("@Correo", Item.Correo));
            oSQL.Parameters.Add(new SqlParameter("@Telefono", Item.Telefono));
            oSQL.Parameters.Add(new SqlParameter("@Celular", Item.Celular));
            oSQL.Parameters.Add(new SqlParameter("@Direccion", Item.Direccion));
            oSQL.Parameters.Add(new SqlParameter("@idCiudad", Item.idCiudad));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.idBodega));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@ModificaPrecio", Item.ModificaPrecio));
            oSQL.Parameters.Add(new SqlParameter("@EsAdmin", Item.EsAdmin));
            oSQL.Parameters.Add(new SqlParameter("@PorcentajeDescuento", Item.PorcentajeDescuento));
            oSQL.Parameters.Add(new SqlParameter("@VerCuadreCaja", Item.VerCuadreCaja));
            try
            {
                Conexion.Open();
                Item.idUsuario = long.Parse(oSQL.ExecuteScalar().ToString());
            }
            catch
            {
                return -1;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Item.idUsuario;
        }

        private long Actualizar(tblUsuarioItem Item)
        {
            SqlCommand oSQL = new SqlCommand("spActualizarUsuario", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            oSQL.Parameters.Add(new SqlParameter("@idUsuario", Item.idUsuario));
            oSQL.Parameters.Add(new SqlParameter("@idTipoIdentificacion", Item.idTipoIdentificacion));
            oSQL.Parameters.Add(new SqlParameter("@Identificacion", Item.Identificacion));
            oSQL.Parameters.Add(new SqlParameter("@Usuario", Item.Usuario));
            oSQL.Parameters.Add(new SqlParameter("@Contrasena", Item.Contrasena));
            oSQL.Parameters.Add(new SqlParameter("@NombreCompleto", Item.NombreCompleto));
            oSQL.Parameters.Add(new SqlParameter("@PrimerNombre", Item.PrimerNombre));
            oSQL.Parameters.Add(new SqlParameter("@SegundoNombre", Item.SegundoNombre));
            oSQL.Parameters.Add(new SqlParameter("@PrimerApellido", Item.PrimerApellido));
            oSQL.Parameters.Add(new SqlParameter("@SegundoApellido", Item.SegundoApellido));
            oSQL.Parameters.Add(new SqlParameter("@Correo", Item.Correo));
            oSQL.Parameters.Add(new SqlParameter("@Telefono", Item.Telefono));
            oSQL.Parameters.Add(new SqlParameter("@Celular", Item.Celular));
            oSQL.Parameters.Add(new SqlParameter("@Direccion", Item.Direccion));
            oSQL.Parameters.Add(new SqlParameter("@idCiudad", Item.idCiudad));
            oSQL.Parameters.Add(new SqlParameter("@idEmpresa", Item.idEmpresa));
            oSQL.Parameters.Add(new SqlParameter("@idBodega", Item.idBodega));
            oSQL.Parameters.Add(new SqlParameter("@Activo", Item.Activo));
            oSQL.Parameters.Add(new SqlParameter("@ModificaPrecio", Item.ModificaPrecio));
            oSQL.Parameters.Add(new SqlParameter("@EsAdmin", Item.EsAdmin));
            oSQL.Parameters.Add(new SqlParameter("@PorcentajeDescuento", Item.PorcentajeDescuento));
            oSQL.Parameters.Add(new SqlParameter("@VerCuadreCaja", Item.VerCuadreCaja));
            try
            {
                Conexion.Open();
                oSQL.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            return Item.idUsuario;
        }
        public long Guardar(tblUsuarioItem Item)
        {
            if (Item.idUsuario > 0)
            {
                return Actualizar(Item);
            }
            else
            {
                return Insertar(Item);
            }
        }

        public tblUsuarioItem buscarUsuarioPorLoginPassword(string login, string password)
        {
            tblUsuarioItem usuario = new tblUsuarioItem();
            SqlCommand oSQL = new SqlCommand("spObtenerUsuarioPorLoginPassword", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                oSQL.Parameters.Add(new SqlParameter("@login", login));
                oSQL.Parameters.Add(new SqlParameter("@password", password));
                SqlDataReader reader = oSQL.ExecuteReader();
                if (reader.Read())
                {
                    usuario = ObtenerItem(reader);
                }
                else
                {
                    usuario = null;
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
            return usuario;
        }

        public List<bsq_ObtenerUsuarioListaPorFiltrosNombreEmpresaItem> ObtenerUsuarioListaPorFiltrosNombreEmpresa(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, long idEmpresa)
        {
            List<bsq_ObtenerUsuarioListaPorFiltrosNombreEmpresaItem> Lista = new List<bsq_ObtenerUsuarioListaPorFiltrosNombreEmpresaItem>();
            SqlCommand oSQL = new SqlCommand("spObtenerUsuarioListaPorFiltrosNombreEmpresa", Conexion);
            oSQL.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                Conexion.Open();
                if (string.IsNullOrEmpty(primerNombre))
                {
                    oSQL.Parameters.Add(new SqlParameter("@primerNombre", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@primerNombre", primerNombre));
                }
                if (string.IsNullOrEmpty(segundoNombre))
                {
                    oSQL.Parameters.Add(new SqlParameter("@segundoNombre", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@segundoNombre", segundoNombre));
                }
                if (string.IsNullOrEmpty(primerApellido))
                {
                    oSQL.Parameters.Add(new SqlParameter("@primerApellido", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@primerApellido", primerApellido));
                }
                if (string.IsNullOrEmpty(segundoApellido))
                {
                    oSQL.Parameters.Add(new SqlParameter("@segundoApellido", DBNull.Value));
                }
                else
                {
                    oSQL.Parameters.Add(new SqlParameter("@segundoApellido", segundoApellido));
                }
                oSQL.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                SqlDataReader reader = oSQL.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(ObtenerItemFiltro(reader));
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

        private bsq_ObtenerUsuarioListaPorFiltrosNombreEmpresaItem ObtenerItemFiltro(SqlDataReader reader)
        {
            bsq_ObtenerUsuarioListaPorFiltrosNombreEmpresaItem Item = new bsq_ObtenerUsuarioListaPorFiltrosNombreEmpresaItem();
            Item.idUsuario = long.Parse(reader["idUsuario"].ToString());
            Item.Identificacion = reader["Identificacion"].ToString();
            Item.Usuario = reader["Usuario"].ToString();
            Item.NombreCompleto = reader["NombreCompleto"].ToString();
            Item.Correo = reader["Correo"].ToString();
            Item.Telefonos = reader["Telefonos"].ToString();
            Item.Empresa = reader["Empresa"].ToString();
            Item.Direccion = reader["Direccion"].ToString();
            Item.Contrasena = reader["Contrasena"].ToString();
            return Item;
        }
    }
}
