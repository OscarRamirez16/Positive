using System;
using System.Collections.Generic;
using InventarioItem;
using InventarioDao;
using System.Data;

namespace InventarioBusiness
{
    public class tblTrasladoMercanciaBusiness
    {
        private string cadenaConexion;

        public tblTrasladoMercanciaBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public bool Guardar(tblTrasladoMercanciaItem Item, List<tblTrasladoMercanciaDetalle> oListTras)
        {
            try
            {
                tblTrasladoMercanciaDao oArtD = new tblTrasladoMercanciaDao(cadenaConexion);
                return oArtD.Guardar(Item, oListTras);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerTrasladoMercancia(DateTime FechaInicial, DateTime FechaFinal, long IdUsuario, long IdEmpresa, bool Agrupado)
        {
            try
            {
                tblTrasladoMercanciaDao oArtD = new tblTrasladoMercanciaDao(cadenaConexion);
                return oArtD.ObtenerTrasladoMercancia(FechaInicial, FechaFinal, IdUsuario, IdEmpresa, Agrupado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
