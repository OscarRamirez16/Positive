using System;
using System.Collections.Generic;
using InventarioDao;
using InventarioItem;

namespace InventarioBusiness
{
    public class tblCiudadBusiness
    {
        private string cadenaConexion;

        public tblCiudadBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public List<tblCiudadItem> ObtenerCiudadLista()
        {
            try
            {
                tblCiudadDao oCiudadD = new tblCiudadDao(cadenaConexion);
                return oCiudadD.ObtenerCiudadLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblCiudadItem ObtenerCiudad(long idCiudad)
        {
            try
            {
                tblCiudadDao oCiudadD = new tblCiudadDao(cadenaConexion);
                return oCiudadD.ObtenerCiudad(idCiudad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<JSONItem> ObtenerCiudadListaPorNombre(string nombre)
        {
            try
            {
                tblCiudadDao oCiudadD = new tblCiudadDao(cadenaConexion);
                return oCiudadD.ObtenerCiudadListaPorNombre(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
