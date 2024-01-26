using System;
using System.Collections.Generic;
using InventarioDao;
using InventarioItem;

namespace InventarioBusiness
{
    public class SeguridadBusiness
    {

        private string cadenaConexion;

        public enum paginasEnum
        {
            CrearEditarUsuarios = 1,
            VerUsuarios = 2,
            CrearEditarConsultarArticulos = 3,
            VerArticulos = 4,
            CrearEditarRoles = 5,
            CrearEditarTerceros = 6,
            VerTerceros = 7,
            Ventas = 8,
            Compras = 9,
            Cotizaciones = 10,
            VerVentas = 11,
            VerCompras = 12,
            VerCotizaciones = 13,
            VerFrmMovimientosPorDocumento = 14,
            CrearEditarConsultarBodega = 15,
            CrearEditarConsultarLineas = 16,
            NotaCreditoVenta = 17,
            VerFrmReporteArticulosPorBodega = 18,
            Pagos = 19,
            Empresa = 20,
            VerFrmReportePagosPorCliente = 21,
            CreacionMasivaArticulos = 22,
            CuadreCaja = 23,
            CrearEditarConsultarCajas = 24,
            Retiros = 25,
            Ingresos = 26,
            VerFrmReporteMovimientosDiarios = 27,
            VerFrmReporteCuadreDiario = 28,
            EntradaMercancia = 29,
            SalidaMercancia = 30,
            NotaCreditoCompra = 31,
            EntradaMasivaMercancia = 32,
            ListaPrecio = 33,
            ReporteDocumentosRangoFecha = 34,
            TrasladarMercancia = 35,
            VerFrmReportePagosPorProveedor = 36,
            FijarPreciosPorBodega = 37,
            ListaMateriales = 38,
            Produccion = 39,
            OrdenFabricacion = 40,
            VerListaMateriales = 41,
            Campana = 42,
            GrupoCliente = 43,
            TipoTarjetaCredito = 44,
            CrearEditarConsultarVentaRapida = 45,
            VentaRapida = 46,
            ImprimirFacturaVenta = 47,
            AnularFacturaVenta = 48,
            AnularFacturaCompra = 49,
            Vendedor = 50,
            EliminarDetalles = 51,
            Descuentos = 52,
            ReporteDocumentosPagos = 53,
            ReporteMovimientosArticulos = 54,
            ReporteAuditoriaStock = 55,
            ReporteHistorialMovimientosArticulo = 56,
            frmFacturaVentaEntregar = 57,
            ConfigurarArticuloCompuesto = 58,
            Remisiones = 59,
            ConsultarRemisiones = 60,
            Conciliacion = 61,
            ReporteVentasDevolucionesPorFecha = 62,
            DocumentosACredito = 63,
            Anticipos = 64,
            CuentaCobro = 65,
            VerAnticipos = 66,
            InformacionCompra = 67,
            PagoMensualidad = 68,
            Retenciones = 69,
            ReporteComisionPorArticulo = 70,
            ReporteCuentaCobro = 71,
            FacturacionMasivaTemplate = 72,
            TrasladoMasivoTemplate = 73
        }

        public SeguridadBusiness(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public bool asignarRolesUsuario(tblUsuario_RolItem oUsuRolI)
        {
            try
            {
                tblUsuario_RolDao oUsuRolD = new tblUsuario_RolDao(cadenaConexion);
                return oUsuRolD.Insertar(oUsuRolI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public List<tblUsuario_RolItem> consultarRolesUsuario(long idUsuario)
        {
            try
            {
                tblUsuario_RolDao oUsuRolD = new tblUsuario_RolDao(cadenaConexion);
                return oUsuRolD.ObtenerUsuario_RolLista(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<tblRol_PaginaItem> consultarPaginasRoles(long idRol, long idEmpresa)
        {
            try
            {
                tblRol_PaginaDao oUsuRolD = new tblRol_PaginaDao(cadenaConexion);
                return oUsuRolD.ObtenerRol_PaginaLista(idRol, idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool eliminarRolesUSuario(long idUsuario)
        {
            try
            {
                tblUsuario_RolDao oUsuRolD = new tblUsuario_RolDao(cadenaConexion);
                return oUsuRolD.eliminarRolesUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool eliminarPaginasRoles(long idRol)
        {
            try
            {
                tblRol_PaginaDao oUsuRolD = new tblRol_PaginaDao(cadenaConexion);
                return oUsuRolD.eliminarPaginasRoles(idRol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool asignarPaginasRoles(tblRol_PaginaItem oRolPagI)
        {
            try
            {
                tblRol_PaginaDao oRolPagD = new tblRol_PaginaDao(cadenaConexion);
                return oRolPagD.Insertar(oRolPagI);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<tblPaginasItem> TraerPaginasRoles()
        {
            try
            {
                tblPaginasDao oPagD = new tblPaginasDao(cadenaConexion);
                return oPagD.ObtenerPaginasLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tblRol_PaginaItem TraerPermisosPaginasPorUsuario(long idUsuario, long idEmpresa, int idPagina)
        {
            try
            {
                tblRol_PaginaDao oRolPagD = new tblRol_PaginaDao(cadenaConexion);
                return oRolPagD.TraerPermisosPaginasPorUsuario(idUsuario, idEmpresa, idPagina);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
