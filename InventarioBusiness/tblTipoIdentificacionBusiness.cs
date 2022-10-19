using System;
using System.Collections.Generic;
using InventarioItem;
using InventarioDao;

namespace InventarioBusiness
{
    public class tblTipoIdentificacionBusiness
    {

        private string cadenaConexion;

        public tblTipoIdentificacionBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public List<tblTipoIdentificacionItem> ObtenerTipoIdentificacionLista()
        {
            tblTipoIdentificacionDao oTipoIdD = new tblTipoIdentificacionDao(cadenaConexion);
            return oTipoIdD.ObtenerTipoIdentificacionLista();
        }
    }
}
