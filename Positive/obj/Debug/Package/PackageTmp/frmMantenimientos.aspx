<%@ Page Title="Menu Principal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMantenimientos.aspx.cs" Inherits="Inventario.frmMantenimientos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row" style="display:none; visibility:hidden;"><div class="col-lg-12"><h3><asp:Label ID="lblTitulo" ForeColor="#1D70B7" runat="server"></asp:Label></h3></div></div>
    <div class="row">
        <div class="col-lg-3">
            <div class = "list-group">
                <a href="#" style="text-align:center" class="list-group-item active"><b style="font-size:medium;"><asp:Label ID="lblArticulos" runat="server"></asp:Label></b></a>
                <a href="frmCrearEditarConsultarArticulos.aspx" class="list-group-item"><img src="Images/Menu/Articulo.png" title="Articulos" />&nbsp;<asp:Label ID="lblSubArticulos" runat="server"></asp:Label></a>
                <a href="frmBodegas.aspx" class="list-group-item"><img src="Images/Menu/Bodega.png" title="bodegas" />&nbsp;<asp:Label ID="lblSubBodegas" runat="server"></asp:Label></a>
                <a href="frmGrupoArticulos.aspx" class="list-group-item"><img src="Images/Menu/GrupoArticulos.png" title="Grupo de Articulos" />&nbsp;<asp:Label ID="lblSubLineas" runat="server"></asp:Label></a>
                <a href="frmCreacionMasivaArticulos.aspx" class="list-group-item"><img src="Images/Menu/Masiva.png" title="Creación masiva de articulos" />&nbsp;<asp:Label ID="lblSubCreacionMasiva" runat="server"></asp:Label></a>
                <a href="frmArticuloActualizarMasivo.aspx" class="list-group-item"><img src="Images/Menu/Masiva.png" title="Actualización masiva de articulos" />&nbsp;Actualización Masiva Artículos</a>
                <a href="frmProduccion.aspx" class="list-group-item"><img src="Images/Menu/Produccion.png" title="Producción" />&nbsp<asp:Label ID="lblProduccion" runat="server"></asp:Label></a>
                <a href="frmCampana.aspx" class="list-group-item"><img src="Images/Menu/Campana.png" title="Campaña" />&nbsp;<asp:Label ID="lblCampana" runat="server"></asp:Label></a>
                <a href="frmDocumentos.aspx?opcionDocumento=5" class="list-group-item"><img src="Images/Menu/Entrada.png" title="Entrada de Mercancia" />&nbsp;<asp:Label ID="lblSubEntradaMercancia" runat="server"></asp:Label></a>
                <a href="frmDocumentos.aspx?opcionDocumento=6" class="list-group-item"><img src="Images/Menu/Salida.png" title="Salida de Mercancia" />&nbsp;<asp:Label ID="lblSubSalidaMercancia" runat="server"></asp:Label></a>
                <a href="frmEntradaMasivaMercancia.aspx" class="list-group-item"><img src="Images/Menu/EntradaMasiva.png" title="Entrada Masiva de Mercancia" />&nbsp;<asp:Label ID="lblSubEntradaMasivaMercancia" runat="server"></asp:Label></a>
                <a href="frmListaPrecio.aspx" class="list-group-item"><img src="Images/Menu/ListaPrecios.png" title="Lista de Precios" />&nbsp;<asp:Label ID="lblSubListaPrecio" runat="server"></asp:Label></a>
                <a href="frmTrasladoMercancia.aspx" class="list-group-item"><img src="Images/Menu/Traslado.png" title="Traslado de Mercancia" />&nbsp;<asp:Label ID="lblSubTrasladoMercancia" runat="server"></asp:Label></a>
                <a href="frmReporteTrasladoMercancia.aspx" class="list-group-item"><img src="Images/Menu/Buscar.png" title="Buscar Traslado de Mercancia" />&nbsp;Consultar Traslado de Mercancia</a>
                <a href="frmTrasladoMasivoMercancia.aspx" class="list-group-item"><img src="Images/Menu/Traslado.png" title="Traslado Masivo de Mercancia" />&nbsp;Traslado Masivo de Mercancia</a>
            </div>
        </div>
        <div class="col-lg-3">
            <div class = "list-group">
                <a href="#" style="text-align:center" class="list-group-item active"><b style="font-size:medium;"><asp:Label ID="lblCajas" runat="server"></asp:Label></b></a>
                <a href="frmCrearEditarConsultarCaja.aspx" class="list-group-item"><img src="Images/Menu/Caja.png" title="Cajas" />&nbsp;<asp:Label ID="lblSubCaja" runat="server"></asp:Label></a>
                <a href="frmAbrirCaja.aspx" class="list-group-item"><img src="Images/Menu/IniciarCaja.png" title="Abrir Caja" />&nbsp;<asp:Label ID="lblSubAbrirCaja" runat="server"></asp:Label></a>
                <a href="frmCerrarCaja.aspx" class="list-group-item"><img src="Images/Menu/CerrarCaja.png" title="Cerrar Caja" />&nbsp;<asp:Label ID="lblSubCerrarCaja" runat="server"></asp:Label></a>
                <a href="frmRetiros.aspx" class="list-group-item"><img src="Images/Menu/Retiro.png" title="Retiro de efectivo" />&nbsp;<asp:Label ID="lblRetirar" runat="server"></asp:Label></a>
                <a href="frmIngresos.aspx" class="list-group-item"><img src="Images/Menu/Ingreso.png" title="Ingreso de efectivo" />&nbsp;<asp:Label ID="lblIngresar" runat="server"></asp:Label></a>
            </div>
            <div class = "list-group">
                <a href="#" style="text-align:center" class="list-group-item active"><b style="font-size:medium;"><asp:Label ID="lblTerceros" runat="server"></asp:Label></b></a>
                <a href="frmCrearEditarTerceros.aspx" class="list-group-item"><img src="Images/Menu/SocioNegocio.png" title="Socio de Negocios" />&nbsp;<asp:Label ID="lblSubCrearTerceros" runat="server"></asp:Label></a>
                <a href="frmVerTerceros.aspx" class="list-group-item"><img src="Images/Menu/Buscar.png" title="Buscar Socio de Negocios" />&nbsp;<asp:Label ID="lblSubConsultarTerceros" runat="server"></asp:Label></a>
                <a href="frmGrupoCliente.aspx" class="list-group-item"><img src="Images/Menu/GrupoClientes.png" title="Grupo de Clientes" />&nbsp;<asp:Label ID="lblGrupoCliente" runat="server"></asp:Label></a>
                <a href="frmVendedor.aspx" class="list-group-item"><img src="Images/Menu/Vendedor.png" title="Vendedor" />&nbsp;<asp:Label ID="lblVendedor" runat="server"></asp:Label></a>
            </div>
        </div>
        <div class="col-lg-3">
            <div class = "list-group">
                <a href="#" style="text-align:center" class="list-group-item active"><b style="font-size:medium;"><asp:Label ID="lblVentas" runat="server"></asp:Label></b></a>
                <a href="frmDocumentos.aspx?opcionDocumento=1" class="list-group-item"><img src="Images/Menu/Factura.png" title="Factura Venta" />&nbsp;<asp:Label ID="lblSubVentas" runat="server"></asp:Label></a>
  				<a href="frmVerDocumentos.aspx?opcionDocumento=1" class="list-group-item"><img src="Images/Menu/Buscar.png" title="Buscar Facturas" />&nbsp;<asp:Label ID="lblSubConsultarVentas" runat="server"></asp:Label></a>
                <a href="frmDocumentos.aspx?opcionDocumento=4" class="list-group-item"><img src="Images/Menu/NotaCredito.png" title="Nota credito" />&nbsp;<asp:Label ID="lblNotaCreditoVentas" runat="server"></asp:Label></a>
                <a href="frmVentaRapida.aspx" class="list-group-item"><img src="Images/Menu/VentaRapida.png" title="Venta Rapida" />&nbsp;<asp:Label ID="lblVentaRapida" runat="server"></asp:Label></a>
                <a href="frmFacturaVentaEntregar.aspx" class="list-group-item"><img src="Images/Menu/Entrega.png" title="Entregar Pedidos" />&nbsp;Entregar Pedidos</a>
                <a href="frmFacturacionMasiva.aspx" class="list-group-item"><img src="Images/Menu/Factura.png" title="Facturación Masiva" />&nbsp;Facturación Masiva</a>
                <a href="frmCuentaCobro.aspx" class="list-group-item"><img src="Images/Menu/Factura.png" title="Cuenta de Cobro" />&nbsp;Cuenta de Cobro</a>
                <a href="frmCuentaCobroMasivo.aspx" class="list-group-item"><img src="Images/Menu/Factura.png" title="Cuenta de Cobro Masivo" />&nbsp;Cuenta de Cobro Masivo</a>
                <a href="frmFacturacionTemplateMasiva.aspx" class="list-group-item"><img src="Images/Menu/Factura.png" title="Facturación Masiva Por Template" />&nbsp;Facturación Masiva Por Template</a>
            </div>
            <div class = "list-group">
                <a href="#" style="text-align:center" class="list-group-item active"><b style="font-size:medium;"><asp:Label ID="lblCompras" runat="server"></asp:Label></b></a>
                <a href="frmDocumentos.aspx?opcionDocumento=2" class="list-group-item"><img src="Images/Menu/FacturaCompra.png" title="Factura Compra" />&nbsp;<asp:Label ID="lblSubCompras" runat="server"></asp:Label></a>
  				<a href="frmVerDocumentos.aspx?opcionDocumento=2" class="list-group-item"><img src="Images/Menu/Buscar.png" title="Buscar Facturas Compra" />&nbsp;<asp:Label ID="lblSubConsultarCompras" runat="server"></asp:Label></a>
                <a href="frmDocumentos.aspx?opcionDocumento=7" class="list-group-item"><img src="Images/Menu/NotaCreditoCompra.png" title="Notas Credito Compras" />&nbsp;<asp:Label ID="lblNotaCreditoCompras" runat="server"></asp:Label></a>
            </div>
            <div class = "list-group">
                <a href="#" style="text-align:center" class="list-group-item active"><b style="font-size:medium;"><asp:Label ID="lblCotizaciones" runat="server"></asp:Label></b></a>
                <a href="frmDocumentos.aspx?opcionDocumento=3" class="list-group-item"><img src="Images/Menu/Cotizacion.png" title="Cotización" />&nbsp;<asp:Label ID="lblSubCotizacion" runat="server"></asp:Label></a>
  				<a href="frmVerDocumentos.aspx?opcionDocumento=3" class="list-group-item"><img src="Images/Menu/Buscar.png" title="Buscar Cotizaciones" />&nbsp;<asp:Label ID="lblSubConsultarCotizaciones" runat="server"></asp:Label></a>
                <a href="frmVentaRapida.aspx?IdTipoDocumento=3" class="list-group-item"><img src="Images/Menu/VentaRapida.png" title="Cotización Rápida" />&nbsp;Cotización Rápida</a>
            </div>
        </div>
        <div class="col-lg-3">
            <div class = "list-group">
                <a href="#" style="text-align:center" class="list-group-item active"><b style="font-size:medium;"><asp:Label ID="lblAdministracion" runat="server"></asp:Label></b></a>
                <a href="frmCuentasPorCobrarPagar.aspx?TipoPago=1" class="list-group-item"><img src="Images/Menu/CuentaCobrar.png" title="Cuentas por Cobrar" />&nbsp;<asp:Label ID="lblSubCuentasCobrar" runat="server"></asp:Label></a>
                <a href="frmCuentasPorCobrarPagar.aspx?TipoPago=2" class="list-group-item"><img src="Images/Menu/CuentaPagar.png" title="Cuentas por Pagar" />&nbsp;<asp:Label ID="lblSubCuentasPagar" runat="server"></asp:Label></a>
                <a href="frmTipoTarjetaCredito.aspx" class="list-group-item"><img src="Images/Menu/TarjetaCredito.png" title="Tarjetas de Credito" />&nbsp;<asp:Label ID="lblTipoTarjetaCredito" runat="server"></asp:Label></a>
                <a href="frmCrearEditarConsultarVentaRapida.aspx" class="list-group-item"><img src="Images/Menu/GestionVentaRapida.png" title="Gestión Venta Rapida" />&nbsp;<asp:Label ID="lblGestionVentaRapida" runat="server"></asp:Label></a>
                <a href="frmCrearEditarUsuarios.aspx" class="list-group-item"><img src="Images/Menu/SocioNegocio.png" title="Crear Usuarios" />&nbsp;<asp:Label ID="lblCrearUsuarios" runat="server"></asp:Label></a>
                <a href="frmVerUsuarios.aspx" class="list-group-item"><img src="Images/Menu/SocioNegocio.png" title="Consultar Usuarios" />&nbsp;<asp:Label ID="lblConsultarUsuarios" runat="server"></asp:Label></a>
            </div>
            <div class = "list-group">
                <a href="#" style="text-align:center" class="list-group-item active"><b style="font-size:medium;"><asp:Label ID="lblReportes" runat="server"></asp:Label></b></a>
                <a href="frmReporteMovimientosPorDocumento.aspx" class="list-group-item"><img src="Images/Menu/ReporteVerde.png" title="Movimiento por Documentos" />&nbsp;<asp:Label ID="lblSubMovimientosDocumentos" runat="server"></asp:Label></a>
                <a href="frmReporteVentasDevolucionesPorFecha.aspx" class="list-group-item"><img src="Images/Menu/ReporteNaranja.png" title="FV y DV por Rango de Fechas" />&nbsp;<asp:Label ID="lblSubDocumentosRangoFechas" runat="server"></asp:Label></a>
                <a href="frmReporteArticulosPorBodega.aspx" class="list-group-item"><img src="Images/Menu/ReporteAzulClaro.png" title="Articulos Por Bodega" />&nbsp;<asp:Label ID="lblSubArticuloBodega" runat="server"></asp:Label></a>
                <a href="frmReportePagosPorCliente.aspx" class="list-group-item"><img src="Images/Menu/ReporteRojo.png" title="Pagos Por Cliente" />&nbsp;<asp:Label ID="lblSubPagosCliente" runat="server"></asp:Label></a>
                <a href="frmReportePagosPorProveedor.aspx" class="list-group-item"><img src="Images/Menu/ReporteNaranja.png" title="Pagos Por Proveedor" />&nbsp;<asp:Label ID="lblSubPagosProveedor" runat="server"></asp:Label></a>
                <a href="frmReporteCuadreDiario.aspx" class="list-group-item"><img src="Images/Menu/ReporteFucsia.png" title="Cuadre Diario" />&nbsp;<asp:Label ID="lblSubCuadreDiario" runat="server"></asp:Label></a>
                <a href="frmReporteMovimientosDiarios.aspx" class="list-group-item"><img src="Images/Menu/ReporteVerde.png" title="Movimientos de Retiro e Ingreso" />&nbsp;<asp:Label ID="lblSubMovRetirosIngresos" runat="server"></asp:Label></a>
                <a href="frmImprimirCuadreDiario.aspx" class="list-group-item"><img src="Images/Menu/ReporteFucsia.png" title="Imprimir Cuadre Diario" />&nbsp;<asp:Label ID="lblSubImprimirCuadreDiario" runat="server"></asp:Label></a>
                <a href="frmReporteComisionesVentasPorArticulo.aspx" class="list-group-item"><img src="Images/Menu/ReporteVerde.png" title="Reporte Comisiones Ventas Por Articulo" />&nbsp;<asp:Label ID="lblSubReporteComisionesVentasPorArticulo" runat="server"></asp:Label></a>
                <a href="frmReporteDocumentosAgrupadoPorArticulo.aspx" class="list-group-item"><img src="Images/Menu/ReporteVerde.png" title="Reporte Documentos Agrupado" />&nbsp;Reporte Documentos Agrupado</a>
            </div>
        </div>
    </div>
</asp:Content>
