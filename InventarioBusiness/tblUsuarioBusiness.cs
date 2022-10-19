using System;
using System.Collections.Generic;
using InventarioItem;
using InventarioDao;

namespace InventarioBusiness
{
    public class tblUsuarioBusiness
    {
        private string cadenaConexion;

        public tblUsuarioBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public bool ModificarIdiomaUsuario(long IdUsuario, short IdIdioma)
        {
            try
            {
                tblUsuarioDao oUsuarioD = new tblUsuarioDao(cadenaConexion);
                return oUsuarioD.ModificarIdiomaUsuario(IdUsuario, IdIdioma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CambiarContrasena(string Password, long IdUsuario)
        {
            try
            {
                tblUsuarioDao oUsuarioD = new tblUsuarioDao(cadenaConexion);
                return oUsuarioD.CambiarContrasena(Password, IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblUsuarioItem> ObtenerUsuarioActivoLista(long idEmpresa)
        {
            try
            {
                tblUsuarioDao oUsuarioD = new tblUsuarioDao(cadenaConexion);
                return oUsuarioD.ObtenerUsuarioActivoLista(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblUsuarioItem> ObtenerUsuarioListaPorIdEmpresa(long idEmpresa)
        {
            try
            {
                tblUsuarioDao oUsuarioD = new tblUsuarioDao(cadenaConexion);
                return oUsuarioD.ObtenerUsuarioListaPorIdEmpresa(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblUsuarioItem buscarUsuarioPorLoginPassword(string login, string password)
        {
            try
            {
                tblUsuarioDao oUsuarioD = new tblUsuarioDao(cadenaConexion);
                return oUsuarioD.buscarUsuarioPorLoginPassword(login, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long insertar(tblUsuarioItem usuario)
        {
            try
            {
                tblUsuarioDao oUsuarioD = new tblUsuarioDao(cadenaConexion);
                usuario.NombreCompleto = usuario.PrimerNombre + " " + usuario.SegundoNombre + " " + usuario.PrimerApellido + " " + usuario.SegundoApellido;
                return oUsuarioD.Guardar(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<bsq_ObtenerUsuarioListaPorFiltrosNombreEmpresaItem> ObtenerUsuarioListaPorFiltrosNombreEmpresa(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, long idEmpresa)
        {
            try
            {
                tblUsuarioDao oUsuarioD = new tblUsuarioDao(cadenaConexion);
                return oUsuarioD.ObtenerUsuarioListaPorFiltrosNombreEmpresa(primerNombre, segundoNombre, primerApellido, segundoApellido, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblUsuarioItem ObtenerUsuario(long idUsuario)
        {
            try
            {
                tblUsuarioDao oUsuarioD = new tblUsuarioDao(cadenaConexion);
                return oUsuarioD.ObtenerUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
