using InventarioDao;
using InventarioItem;
using System;
using System.Collections.Generic;

namespace InventarioBusiness
{
    public class tblConciliacionBusiness
    {
        private string CadenaConexion;

        public tblConciliacionBusiness(string cadenaConexion)
        {
            this.CadenaConexion = cadenaConexion;
        }

        public bool Guardar(tblConciliacionItem Item, List<tblConciliacionDetalleItem> oListDet, tblMovimientosDiariosItem oMovI)
        {
            try
            {
                tblConciliacionDao oArtD = new tblConciliacionDao(CadenaConexion);
                return oArtD.Guardar(Item, oListDet, oMovI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
