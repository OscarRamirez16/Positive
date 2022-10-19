using System;
using System.Collections.Generic;
using InventarioItem;
using InventarioDao;

namespace InventarioBusiness
{
    public class tblEmpresaBusiness
    {
        private string cadenaConexion;

        public tblEmpresaBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        public void ActualizarConsecutivo(long IdEmpresa)
        {
            try
            {
                tblEmpresaDao oEmpD = new tblEmpresaDao(cadenaConexion);
                oEmpD.ActualizarConsecutivo(IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tblEmpresaItem> ObtenerEmpresaLista()
        {
            try
            {
                tblEmpresaDao oEmpD = new tblEmpresaDao(cadenaConexion);
                return oEmpD.ObtenerEmpresaLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblEmpresaItem ObtenerEmpresa(long idEmpresa)
        {
            try
            {
                tblEmpresaDao oEmpD = new tblEmpresaDao(cadenaConexion);
                return oEmpD.ObtenerEmpresa(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Guardar(tblEmpresaItem empresa, EmpresaUsuario oEUItem)
        {
            try
            {
                tblEmpresaDao oEmpD = new tblEmpresaDao(cadenaConexion);
                return oEmpD.Guardar(empresa, oEUItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Servidor> ObtenerServidorLista()
        {
            tblEmpresaDao oEmpD = new tblEmpresaDao(cadenaConexion);
            return oEmpD.ObtenerServidorLista();
        }

        public Servidor ObtenerServidorID(int IdServidor)
        {
            tblEmpresaDao oEmpD = new tblEmpresaDao(cadenaConexion);
            return oEmpD.ObtenerServidorID(IdServidor);
        }
    }
}
