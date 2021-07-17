using System;
using System.Collections.Generic;
using InventarioDao;
using InventarioItem;
using System.Data;

namespace InventarioBusiness
{
    public class tblBodegaBusiness
    {

        private string CadenaConexion;

        public tblBodegaBusiness(string CadenaConexion)
        {
            this.CadenaConexion = CadenaConexion;
        }

        public DataTable ObtenerConfiguracionBodegasPorArticulo(long IdArticulo, long IdEmpresa)
        {
            try
            {
                tblBodegaDao oBodD = new tblBodegaDao(CadenaConexion);
                return oBodD.ObtenerConfiguracionBodegasPorArticulo(IdArticulo, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObtenerOrdenFabricacionDetallePorID(long IdOrden, long IdArticulo)
        {
            try
            {
                tblOrdenFabricacionDao oOrdD = new tblOrdenFabricacionDao(CadenaConexion);
                return oOrdD.ObtenerOrdenFabricacionDetallePorID(IdOrden, IdArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarPreciosPorBodega(long IdArticulo, long IdBodega)
        {
            try
            {
                tblPreciosPorBodegaDao oBodD = new tblPreciosPorBodegaDao(CadenaConexion);
                return oBodD.EliminarPreciosPorBodega(IdArticulo, IdBodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GuardarPreciosPorBodega(tblPreciosPorBodegaItem Item)
        {
            try
            {
                tblPreciosPorBodegaDao oBodD = new tblPreciosPorBodegaDao(CadenaConexion);
                return oBodD.Insertar(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblPreciosPorBodegaItem> ObtenerPreciosPorBodegaVenta(long IdArticulo, long IdBodega)
        {
            try
            {
                tblPreciosPorBodegaDao oBodD = new tblPreciosPorBodegaDao(CadenaConexion);
                return oBodD.ObtenerPreciosPorBodegaVenta(IdArticulo, IdBodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblPreciosPorBodegaItem> ObtenerPreciosPorBodega(long IdArticulo, long IdBodega)
        {
            try
            {
                tblPreciosPorBodegaDao oBodD = new tblPreciosPorBodegaDao(CadenaConexion);
                return oBodD.ObtenerPreciosPorBodega(IdArticulo, IdBodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblTipoManejoPrecioItem> ObtenerTipoManejoPrecioArticulo()
        {
            try
            {
                tblTipoManejoPrecioDao oBodD = new tblTipoManejoPrecioDao(CadenaConexion);
                return oBodD.ObtenerTipoManejoPrecioLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblArticulo_BodegaItem ConsultarArticulosBodegaPorID(long IdArticulo, long IdBodega)
        {
            try
            {
                tblArticulo_BodegaDao oBodD = new tblArticulo_BodegaDao(CadenaConexion);
                return oBodD.ConsultarArticulosBodegaPorID(IdArticulo, IdBodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblArticulo_BodegaItem> ConsultarArticulosPorBodega(long idArticulo)
        {
            try
            {
                tblArticulo_BodegaDao oBodD = new tblArticulo_BodegaDao(CadenaConexion);
                return oBodD.ObtenerArticuloBodegaLista(idArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblBodegaItem ObtenerBodega(long idBodega, long idEmpresa)
        {
            try
            {
                tblBodegaDao oBodD = new tblBodegaDao(CadenaConexion);
                return oBodD.ObtenerBodega(idBodega, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarBodegasArticulos(long idArticulo)
        {
            try
            {
                tblArticulo_BodegaDao oBodD = new tblArticulo_BodegaDao(CadenaConexion);
                oBodD.EliminarBodegasArticulos(idArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerListaBodegaArticuloDisponible(long IdArticulo)
        {
            try
            {
                tblBodegaDao oBodD = new tblBodegaDao(CadenaConexion);
                return oBodD.ObtenerListaBodegaArticuloDisponible(IdArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerBodegaPorDescripcionEmpresa(string Nombre, long IdEmpresa)
        {
            try
            {
                tblBodegaDao oBodD = new tblBodegaDao(CadenaConexion);
                return oBodD.ObtenerBodegaPorDescripcionEmpresa(Nombre, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tblBodegaItem> ObtenerBodegaLista(long idEmpresa)
        {
            try
            {
                tblBodegaDao oBodD = new tblBodegaDao(CadenaConexion);
                return oBodD.ObtenerBodegaLista(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<JSONItem> ObtenerBodegaListaPorNombre(string nombre, long idEmpresa)
        {
            try
            {
                tblBodegaDao oBodD = new tblBodegaDao(CadenaConexion);
                return oBodD.ObtenerBodegaListaPorNombre(nombre, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<JSONItem> ObtenerBodegaListaPorNombreCantidad(string nombre, long idEmpresa, long idArticulo)
        {
            try
            {
                tblBodegaDao oBodD = new tblBodegaDao(CadenaConexion);
                return oBodD.ObtenerBodegaListaPorNombreCantidad(nombre, idEmpresa, idArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Guardar(tblBodegaItem bodega)
        {
            try{
                tblBodegaDao oBodD = new tblBodegaDao(CadenaConexion);
                return oBodD.Guardar(bodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GuardarArticuloPorBodega(tblArticulo_BodegaItem bodega)
        {
            try
            {
                tblArticulo_BodegaDao oBodD = new tblArticulo_BodegaDao(CadenaConexion);
                return oBodD.Insertar(bodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EliminarArticuloBodega(tblArticulo_BodegaItem oArtBodI)
        {
            try
            {
                tblArticulo_BodegaDao oBodD = new tblArticulo_BodegaDao(CadenaConexion);
                return oBodD.EliminarArticuloBodega(oArtBodI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
