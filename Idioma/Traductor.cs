using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idioma
{
    public class Traductor
    {
        public enum IdiomaEnum
        {
            Espanol = 0,
            Ingles = 1,
            Aleman = 2
        }

        public enum IdiomaPalabraEnum
        {
            Nombre = 0,
            Descripcion = 1,
            Identificacion = 2,
            Grande = 3,
            Mediana = 4,
            Pequena = 5,
            Seleccione = 6,
            un = 7,
            Tamano = 8,
            Tipo = 9,
            de = 10,
            Ciudad = 11,
            Pais = 12,
            Direccion = 13,
            Telefono = 14,
            Celular = 15,
            o = 16,
            SitioWeb = 17,
            Contacto = 18,
            Correo = 19,
            Empresario = 20,
            Crear = 21,
            Usuario = 22,
            Nit = 23,
            Primer = 24,
            Segundo = 25,
            Contrasena = 26,
            Login = 27,
            Apellido = 28,
            Bodega = 29,
            Activo = 30,
            Asignacion = 31,
            Roles = 32,
            Menu = 33,
            Rapido = 34,
            Lista = 35,
            Usuarios = 36,
            Bodegas = 37,
            Cajas = 38,
            Articulos = 39,
            Caja = 40,
            Abrir = 41,
            Cerrar = 42,
            Retirar = 43,
            Efectivo = 44,
            Ingresar = 45,
            Lineas = 46,
            Creacion = 47,
            Masiva = 48,
            SocioNegocio = 49,
            Consultar = 50,
            SociosNegocio = 51,
            Ventas = 52,
            Factura = 53,
            Compra = 54,
            Nota = 55,
            Credito = 56,
            Saldo = 57,
            Inicial = 58,
            Observaciones = 60,
            Ingresos = 61,
            Retiros = 62,
            Total = 63,
            Valor = 64,
            Registrar = 65,
            Articulo = 66,
            PrecioAutomatico = 67,
            Inventario = 68,
            Es = 69,
            Linea = 70,
            Presentacion = 71,
            Codigo = 72,
            Precio = 73,
            CodigoBarra = 74,
            IVA = 75,
            StockMinimo = 76,
            Cajeros = 78,
            Delimitador = 79,
            Archivo = 80,
            Cliente = 81,
            Proveedor = 82,
            FechaNacimiento = 83,
            Cotizaciones = 84,
            Administracion = 85,
            CuentasCobrar = 86,
            CuentasPagar = 87,
            Reportes = 88,
            Cotizacion = 89,
            Listas = 90,
            Precios = 91,
            MovimientosDocumentos = 92,
            EntradaMercacia = 93,
            SalidaMercancia = 94,
            TrasladoMercancia = 95,
            DocumentosRangoFechas = 96,
            Costo = 97,
            ValorUnitario = 98,
            Detalles = 99,
            Numero = 100,
            MenuRapido = 101,
            IniciarCaja = 102,
            CerrarCaja = 103,
            IngresarEfectivo = 104,
            Cedula = 105,
            Buscar = 106,
            Devolucion = 107,
            InventarioNegativo = 108,
            MargenUtilidad = 109,
            Encabezado = 110,
            PiePagina = 111,
            Empresa = 112,
            InformacionRol = 113,
            AsignacionPermisos = 114,
            Facturas = 115,
            Cantidad = 116,
            FechaInicial = 117,
            FechaFinal = 118,
            FacturasVentas = 119,
            FacturasCompras = 120,
            FacturaVenta = 121,
            FacturaCompra = 122,
            ListasPrecios = 123,
            Aumento = 124,
            Factor = 125,
            Creadas = 126,
            TipoDocumento = 127,
            PagosCliente = 128,
            PagosProveedor = 129,
            ArticulosBodega = 130,
            MovimientosRetiroIngreso = 131,
            CuadreDiario = 132,
            BodegaOrigen = 133,
            BodegaDestino = 134,
            TipoMovimiento = 135,
            CambiarContrasena = 136,
            NuevaContrasena = 137,
            ConfirmarContrasena = 138,
            ContrasenaActual = 139,
            IVACompra = 140,
            IVAVenta = 141,
            Errores = 142,
            TipoManejo = 143,
            FijarPreciosBodega = 144,
            ListaPrecio = 145,
            Leer = 146,
            Guardar = 147,
            Actualizar = 148,
            Eliminar = 149,
            NumeroCheque = 150,
            Fecha = 151,
            Nuevo = 152,
            InicioNumeracion = 153,
            FinalNumeracion = 154,
            NumeracionVentas = 155,
            ProximoValor = 156,
            TotalPago = 157,
            ListaMateriales = 158,
            MateriaPrima = 159,
            Produccion = 160,
            OrdenFabricacion = 161,
            Estado = 162,
            ModificaPrecio = 163,
            Campana = 164,
            HoraInicial = 165,
            HoraFinal = 166,
            NombreObligatorio = 167,
            CampanaFechaInicial = 168,
            CampanaFechaFinal = 169,
            CampanaFechaInicialFinal = 170,
            CampanaHoraInicial = 171,
            CampanaHoraFinal = 172,
            CampanaHoraInicialFinal = 173,
            CampanaErrorGuardar = 174,
            CampanaValidacion = 175,
            Todos = 176,
            Excluir = 177,
            TipoDescuento = 178,
            CampanaArticulo = 179,
            CampanaGrupoArticulo = 180,
            CampanaValorDescuento = 181,
            CampanaArticuloErrorGuardar = 182,
            CampanaArticuloErrorId = 183,
            CampanaCliente = 184,
            CampanaGrupoCliente = 185,
            CampanaClienteErrorGuardar = 186,
            GrupoCliente = 187,
            CuentaContable = 188,
            CodigoValidacion = 189,
            Validacion = 190,
            GrupoClienteExite = 191,
            GrupoClienteErrorGuardar = 192,
            TipoTarjetaCredito = 193,
            CuentaContableObligatorio = 194,
            TipoTarjetaCreditoErrorGuardar = 195,
            Texto = 196,
            VentaRapida = 197,
            Imagen = 198,
            ArticuloObligatorio = 199,
            CantidadFormato = 200,
            ImagenObligatorio = 201,
            VentaRapidaErrorGuardar = 202,
            GestionVentaRapida = 203,
            ValorAntesIVA = 204,
            ValorIVA = 205,
            ValorTotal = 206,
            Comision = 207,
            Vendedor = 208,
            ComisionDecimal = 209,
            VendedorGuardar = 210,
            VendedorError = 211,
            VendedoresCreados = 212,
            Ubicacion = 213,
            Disponibles = 214,
            Devuelta = 215,
            Administrador = 216,
            PorcentajeDescuento = 217,
            Remision = 218,
            Remisiones = 219,
            ZipCode = 220,
            Rapida = 221,
            CuentaCobro = 222,
            Anticipo = 223,
            Conciliacion = 224,
            RetirarEfectivo = 225,
            GrupoArticulo = 226,
            CreacionMasivaArticulo = 227,
            ActualizacionMasivaArticulo = 228,
            EntradaSalidaMasiva = 229,
            ArticuloCompuesto = 230,
            GrupoSocioNegocio = 231
        }

        public string TraducirPalabra(IdiomaEnum Idioma, IdiomaPalabraEnum Palabra) {
            string strPalabra = "";
            switch (Palabra)
            {
                case IdiomaPalabraEnum.GrupoSocioNegocio:
                    strPalabra = "Grupo Socio Negocio|Business Partner Group";
                    break;
                case IdiomaPalabraEnum.ArticuloCompuesto:
                    strPalabra = "Articulo Compuesto|Composite Item";
                    break;
                case IdiomaPalabraEnum.EntradaSalidaMasiva:
                    strPalabra = "Entrada/Salida Masiva|Massive Input/Output";
                    break;
                case IdiomaPalabraEnum.ActualizacionMasivaArticulo:
                    strPalabra = "Actualizacion Masiva Articulo|Massive Update Articles";
                    break;
                case IdiomaPalabraEnum.CreacionMasivaArticulo:
                    strPalabra = "Creacion Masiva Articulo|Massive Creation Articles";
                    break;
                case IdiomaPalabraEnum.GrupoArticulo:
                    strPalabra = "Grupo Articulo|Items Group";
                    break;
                case IdiomaPalabraEnum.RetirarEfectivo:
                    strPalabra = "Retirar Efectivo|Remove Cash";
                    break;
                case IdiomaPalabraEnum.Conciliacion:
                    strPalabra = "Conciliacion|Conciliation";
                    break;
                case IdiomaPalabraEnum.Anticipo:
                    strPalabra = "Anticipo|Advance Payment";
                    break;
                case IdiomaPalabraEnum.CuentaCobro:
                    strPalabra = "Cuenta Cobro|Account Receivable";
                    break;
                case IdiomaPalabraEnum.Remisiones:
                    strPalabra = "Remisiones|Deliveries";
                    break;
                case IdiomaPalabraEnum.Remision:
                    strPalabra = "Remision|Delivery";
                    break;
                case IdiomaPalabraEnum.PorcentajeDescuento:
                    strPalabra = "Porcentaje Descuento|Discount Rate";
                    break;
                case IdiomaPalabraEnum.Administrador:
                    strPalabra = "Administrador|Administrator";
                    break;
                case IdiomaPalabraEnum.Devuelta:
                    strPalabra = "Devuelta|Change";
                    break;
                case IdiomaPalabraEnum.Disponibles:
                    strPalabra = "Disponibles|Available";
                    break;
                case IdiomaPalabraEnum.Ubicacion:
                    strPalabra = "Ubicacion|Location";
                    break;
                case IdiomaPalabraEnum.Estado:
                    strPalabra = "Estado|State";
                    break;
                case IdiomaPalabraEnum.OrdenFabricacion:
                    strPalabra = "Orden de Fabricación|Production Order";
                    break;
                case IdiomaPalabraEnum.Produccion:
                    strPalabra = "Producción|Production";
                    break;
                case IdiomaPalabraEnum.MateriaPrima:
                    strPalabra = "Materia Prima|Raw Materia";
                    break;
                case IdiomaPalabraEnum.ListaMateriales:
                    strPalabra = "Lista de Materiales|Material's List";
                    break;
                case IdiomaPalabraEnum.TotalPago:
                    strPalabra = "Total Pago|Full Payment";
                    break;
                case IdiomaPalabraEnum.ProximoValor:
                    strPalabra = "Proximo Valor|Next Value";
                    break;
                case IdiomaPalabraEnum.NumeracionVentas:
                    strPalabra = "Numeración Facturas Ventas|Sales Invoice Numbering";
                    break;
                case IdiomaPalabraEnum.FinalNumeracion:
                    strPalabra = "Final Numeracion|Final Numbering";
                    break;
                case IdiomaPalabraEnum.InicioNumeracion:
                    strPalabra = "Inicio Numeracion|Start Numbering";
                    break;
                case IdiomaPalabraEnum.Nuevo:
                    strPalabra = "Nuevo|New";
                    break;
                case IdiomaPalabraEnum.Fecha:
                    strPalabra = "Fecha|Date";
                    break;
                case IdiomaPalabraEnum.NumeroCheque:
                    strPalabra = "Numero Cheque|Check Number";
                    break;
                case IdiomaPalabraEnum.Eliminar:
                    strPalabra = "Eliminar|Delete";
                    break;
                case IdiomaPalabraEnum.Guardar:
                    strPalabra = "Guardar|Save";
                    break;
                case IdiomaPalabraEnum.Actualizar:
                    strPalabra = "Actualizar|Update";
                    break;
                case IdiomaPalabraEnum.Leer:
                    strPalabra = "Leer|Read";
                    break;
                case IdiomaPalabraEnum.ListaPrecio:
                    strPalabra = "Lista de Precio|Price List";
                    break;
                case IdiomaPalabraEnum.FijarPreciosBodega:
                    strPalabra = "Fijar Precios Por Bodega|Set Prices For Warehouse";
                    break;
                case IdiomaPalabraEnum.TipoManejo:
                    strPalabra = "Tipo Manejo|Type Management";
                    break;
                case IdiomaPalabraEnum.Errores:
                    strPalabra = "Errores|Error";
                    break;
                case IdiomaPalabraEnum.IVACompra:
                    strPalabra = "IVA Compra|Purchase Tax";
                    break;
                case IdiomaPalabraEnum.IVAVenta:
                    strPalabra = "IVA Venta|Sales Tax";
                    break;
                case IdiomaPalabraEnum.ContrasenaActual:
                    strPalabra = "Contraseña Actual|Current Password";
                    break;
                case IdiomaPalabraEnum.NuevaContrasena:
                    strPalabra = "Nueva Contraseña|New Password";
                    break;
                case IdiomaPalabraEnum.ConfirmarContrasena:
                    strPalabra = "Confirmar Contraseña|Confirm Password";
                    break;
                case IdiomaPalabraEnum.CambiarContrasena:
                    strPalabra = "Cambiar Contraseña|Change Password";
                    break;
                case IdiomaPalabraEnum.TipoMovimiento:
                    strPalabra = "Tipo Movimiento|Movement Type";
                    break;
                case IdiomaPalabraEnum.BodegaOrigen:
                    strPalabra = "Bodega Origen|Warehouse Origin";
                    break;
                case IdiomaPalabraEnum.BodegaDestino:
                    strPalabra = "Bodega Destino|Warehouse Destiny";
                    break;
                case IdiomaPalabraEnum.CuadreDiario:
                    strPalabra = "Cuadre Diario|Cuadre Daily";
                    break;
                case IdiomaPalabraEnum.MovimientosRetiroIngreso:
                    strPalabra = "Movimientos de Retiro e Ingreso|Movements of Remove and Deposits";
                    break;
                case IdiomaPalabraEnum.ArticulosBodega:
                    strPalabra = "Artículos Por Bodega|Items by Warehouse";
                    break;
                case IdiomaPalabraEnum.PagosProveedor:
                    strPalabra = "Pagos Por Proveedor|Provider Payments";
                    break;
                case IdiomaPalabraEnum.PagosCliente:
                    strPalabra = "Pagos Por Cliente|Customer Payments";
                    break;
                case IdiomaPalabraEnum.TipoDocumento:
                    strPalabra = "Tipo Documento|Document Type";
                    break;
                case IdiomaPalabraEnum.Creadas:
                    strPalabra = "Creadas|Created";
                    break;
                case IdiomaPalabraEnum.Factor:
                    strPalabra = "Factor|Factor";
                    break;
                case IdiomaPalabraEnum.Aumento:
                    strPalabra = "Aumento|Increase";
                    break;
                case IdiomaPalabraEnum.ListasPrecios:
                    strPalabra = "Listas de Precios|Price Lists";
                    break;
                case IdiomaPalabraEnum.FacturaVenta:
                    strPalabra = "Factura de Venta|Sale Invoice";
                    break;
                case IdiomaPalabraEnum.FacturaCompra:
                    strPalabra = "Factura de Compra|Purchase Invoice";
                    break;
                case IdiomaPalabraEnum.FacturasVentas:
                    strPalabra = "Facturas de Ventas|Sales Invoices";
                    break;
                case IdiomaPalabraEnum.FacturasCompras:
                    strPalabra = "Facturas de Compras|Purchases Invoices";
                    break;
                case IdiomaPalabraEnum.FechaInicial:
                    strPalabra = "Fecha Inicial|Initial Date";
                    break;
                case IdiomaPalabraEnum.FechaFinal:
                    strPalabra = "Fecha Final|Final Date";
                    break;
                case IdiomaPalabraEnum.Cantidad:
                    strPalabra = "Cantidad|Quantity";
                    break;
                case IdiomaPalabraEnum.Facturas:
                    strPalabra = "Facturas|Invoices";
                    break;
                case IdiomaPalabraEnum.InformacionRol:
                    strPalabra = "Informacion del Rol|Information Role";
                    break;
                case IdiomaPalabraEnum.AsignacionPermisos:
                    strPalabra = "Asignación de Permisos|Assigning Permissions";
                    break;
                case IdiomaPalabraEnum.Empresa:
                    strPalabra = "Empresa|Company";
                    break;
                case IdiomaPalabraEnum.Encabezado:
                    strPalabra = "Encabezado|Header";
                    break;
                case IdiomaPalabraEnum.PiePagina:
                    strPalabra = "Pie de Pagina|Footnote";
                    break;
                case IdiomaPalabraEnum.MargenUtilidad:
                    strPalabra = "Margen Utilidad|Profit Margin";
                    break;
                case IdiomaPalabraEnum.InventarioNegativo:
                    strPalabra = "Inventario Negativo|Negative Inventory";
                    break;
                case IdiomaPalabraEnum.Devolucion:
                    strPalabra = "Devolución|Return";
                    break;
                case IdiomaPalabraEnum.Buscar:
                    strPalabra = "Buscar|Search";
                    break;
                case IdiomaPalabraEnum.Cedula:
                    strPalabra = "Cedula|Cell Phone Number";
                    break;
                case IdiomaPalabraEnum.IngresarEfectivo:
                    strPalabra = "Ingresar Efectivo|Cash Deposits";
                    break;
                case IdiomaPalabraEnum.CerrarCaja:
                    strPalabra = "Cerrar Caja|Close Cash Register";
                    break;
                case IdiomaPalabraEnum.IniciarCaja:
                    strPalabra = "Iniciar Caja|Start Cash Register";
                    break;
                case IdiomaPalabraEnum.MenuRapido:
                    strPalabra = "Menu Rapido|Fast Menu";
                    break;
                case IdiomaPalabraEnum.Numero:
                    strPalabra = "Número|Number";
                    break;
                case IdiomaPalabraEnum.Detalles:
                    strPalabra = "Detalles|Details";
                    break;
                case IdiomaPalabraEnum.ValorUnitario:
                    strPalabra = "Precio|Price";
                    break;
                case IdiomaPalabraEnum.Costo:
                    strPalabra = "Costo|Cost";
                    break;
                case IdiomaPalabraEnum.DocumentosRangoFechas:
                    strPalabra = "FV, FC y DV por rango de fechas|FV, FC and DV by date range";
                    break;
                case IdiomaPalabraEnum.TrasladoMercancia:
                    strPalabra = "Traslado de Mercacia|Transfer Item";
                    break;
                case IdiomaPalabraEnum.SalidaMercancia:
                    strPalabra = "Salida de Mercacia|Item Out";
                    break;
                case IdiomaPalabraEnum.EntradaMercacia:
                    strPalabra = "Entrada de Mercacia|Item In";
                    break;
                case IdiomaPalabraEnum.MovimientosDocumentos:
                    strPalabra = "Movimientos por Documentos|Movements Documents";
                    break;
                case IdiomaPalabraEnum.Listas:
                    strPalabra = "Listas|Lists";
                    break;
                case IdiomaPalabraEnum.Precios:
                    strPalabra = "Precios|Prices";
                    break;
                case IdiomaPalabraEnum.Reportes:
                    strPalabra = "Reportes|Reports";
                    break;
                case IdiomaPalabraEnum.CuentasCobrar:
                    strPalabra = "Cuentas por Cobrar|Accounts Receivable";
                    break;
                case IdiomaPalabraEnum.CuentasPagar:
                    strPalabra = "Cuentas por Pagar|Debts to Pay";
                    break;
                case IdiomaPalabraEnum.Administracion:
                    strPalabra = "Administración|Administration";
                    break;
                case IdiomaPalabraEnum.Cotizacion:
                    strPalabra = "Cotización|Quotation";
                    break;
                case IdiomaPalabraEnum.Cotizaciones:
                    strPalabra = "Cotizaciones|Quotes";
                    break;
                case IdiomaPalabraEnum.FechaNacimiento:
                    strPalabra = "Fecha Nacimiento|Birthdate";
                    break;
                case IdiomaPalabraEnum.Cliente:
                    strPalabra = "Cliente|Customer";
                    break;
                case IdiomaPalabraEnum.Proveedor:
                    strPalabra = "Proveedor|Provider";
                    break;
                case IdiomaPalabraEnum.Archivo:
                    strPalabra = "Archivo|Archive";
                    break;
                case IdiomaPalabraEnum.Delimitador:
                    strPalabra = "Delimitador|Delimiter";
                    break;
                case IdiomaPalabraEnum.Cajeros:
                    strPalabra = "Cajeros|Cashiers";
                    break;
                case IdiomaPalabraEnum.StockMinimo:
                    strPalabra = "Stock Minimo|Low Stock";
                    break;
                case IdiomaPalabraEnum.IVA:
                    strPalabra = "IVA|Tax";
                    break;
                case IdiomaPalabraEnum.CodigoBarra:
                    strPalabra = "Código Barra|Barcode";
                    break;
                case IdiomaPalabraEnum.Precio:
                    strPalabra = "Precio|Price";
                    break;
                case IdiomaPalabraEnum.Codigo:
                    strPalabra = "Codigo|Code";
                    break;
                case IdiomaPalabraEnum.Presentacion:
                    strPalabra = "Presentacion|Presentation";
                    break;
                case IdiomaPalabraEnum.Linea:
                    strPalabra = "Grupo|Group";
                    break;
                case IdiomaPalabraEnum.Es:
                    strPalabra = "Es|Is";
                    break;
                case IdiomaPalabraEnum.Inventario:
                    strPalabra = "Inventario|Inventory";
                    break;
                case IdiomaPalabraEnum.PrecioAutomatico:
                    strPalabra = "Precio Automatico|Automatic Price";
                    break;
                case IdiomaPalabraEnum.Articulo:
                    strPalabra = "Articulo|Item";
                    break;
                case IdiomaPalabraEnum.Registrar:
                    strPalabra = "Registrar|Register";
                    break;
                case IdiomaPalabraEnum.Saldo:
                    strPalabra = "Saldo|Balance";
                    break;
                case IdiomaPalabraEnum.Valor:
                    strPalabra = "Valor|Value";
                    break;
                case IdiomaPalabraEnum.Total:
                    strPalabra = "Total|Total";
                    break;
                case IdiomaPalabraEnum.Retiros:
                    strPalabra = "Retiros|Removes";
                    break;
                case IdiomaPalabraEnum.Ingresos:
                    strPalabra = "Ingresos|Income";
                    break;
                case IdiomaPalabraEnum.Observaciones:
                    strPalabra = "Observaciones|Observations";
                    break;
                case IdiomaPalabraEnum.Inicial:
                    strPalabra = "Inicial|Initial";
                    break;
                case IdiomaPalabraEnum.Nota:
                    strPalabra = "Nota|Note";
                    break;
                case IdiomaPalabraEnum.Credito:
                    strPalabra = "Crédito|Credit";
                    break;
                case IdiomaPalabraEnum.Compra:
                    strPalabra = "Compra|Purchase";
                    break;
                case IdiomaPalabraEnum.SocioNegocio:
                    strPalabra = "Socio Negocio|Business Partner";
                    break;
                case IdiomaPalabraEnum.Factura:
                    strPalabra = "Factura|Invoice";
                    break;
                case IdiomaPalabraEnum.Ventas:
                    strPalabra = "Ventas|Sales";
                    break;
                case IdiomaPalabraEnum.SociosNegocio:
                    strPalabra = "Socios Negocio|Business Partners";
                    break;
                case IdiomaPalabraEnum.Consultar:
                    strPalabra = "Consultar|Consult";
                    break;
                case IdiomaPalabraEnum.Lineas:
                    strPalabra = "Grupo de Articulos|Items Groups";
                    break;
                case IdiomaPalabraEnum.Masiva:
                    strPalabra = "Masiva|Mass";
                    break;
                case IdiomaPalabraEnum.Creacion:
                    strPalabra = "Creacion|Creation";
                    break;
                case IdiomaPalabraEnum.Menu:
                    strPalabra = "Menu|Menu";
                    break;
                case IdiomaPalabraEnum.Abrir:
                    strPalabra = "Abrir|Open";
                    break;
                case IdiomaPalabraEnum.Cerrar:
                    strPalabra = "Cerrar|Close";
                    break;
                case IdiomaPalabraEnum.Retirar:
                    strPalabra = "Retirar|Remove";
                    break;
                case IdiomaPalabraEnum.Efectivo:
                    strPalabra = "Efectivo|Cash";
                    break;
                case IdiomaPalabraEnum.Ingresar:
                    strPalabra = "Ingresar|Deposit";
                    break;
                case IdiomaPalabraEnum.Caja:
                    strPalabra = "Caja|Cash Register";
                    break;
                case IdiomaPalabraEnum.Articulos:
                    strPalabra = "Articulos|Items";
                    break;
                case IdiomaPalabraEnum.Cajas:
                    strPalabra = "Cajas|Cash Registers";
                    break;
                case IdiomaPalabraEnum.Bodegas:
                    strPalabra = "Bodegas|Warehouses";
                    break;
                case IdiomaPalabraEnum.Rapido:
                    strPalabra = "Rapido|Fast";
                    break;
                case IdiomaPalabraEnum.Lista:
                    strPalabra = "Lista|List";
                    break;
                case IdiomaPalabraEnum.Usuarios:
                    strPalabra = "Usuarios|Users";
                    break;
                case IdiomaPalabraEnum.Roles:
                    strPalabra = "Roles|Roles";
                    break;
                case IdiomaPalabraEnum.Asignacion:
                    strPalabra = "Asignación|Assignment";
                    break;
                case IdiomaPalabraEnum.Primer:
                    strPalabra = "Primer|First";
                    break;
                case IdiomaPalabraEnum.Bodega:
                    strPalabra = "Bodega|Warehouse";
                    break;
                case IdiomaPalabraEnum.Activo:
                    strPalabra = "Activo|Active";
                    break;
                case IdiomaPalabraEnum.Apellido:
                    strPalabra = "Apellido|Last name";
                    break;
                case IdiomaPalabraEnum.Login:
                    strPalabra = "Login|Login";
                    break;
                case IdiomaPalabraEnum.Segundo:
                    strPalabra = "Segundo|Second";
                    break;
                case IdiomaPalabraEnum.Contrasena:
                    strPalabra = "Contraseña|Password";
                    break;
                case IdiomaPalabraEnum.Nombre:
                    strPalabra = "Nombre|Name";
                    break;
                case IdiomaPalabraEnum.Descripcion:
                    strPalabra = "Descripción|Description";
                    break;
                case IdiomaPalabraEnum.Identificacion:
                    strPalabra = "Identificación|Id";
                    break;
                case IdiomaPalabraEnum.Grande:
                    strPalabra = "Grande|Big";
                    break;
                case IdiomaPalabraEnum.Mediana:
                    strPalabra = "Mediana|Median";
                    break;
                case IdiomaPalabraEnum.Pequena:
                    strPalabra = "Pequeña|Small";
                    break;
                case IdiomaPalabraEnum.Seleccione:
                    strPalabra = "Seleccione|Select";
                    break;
                case IdiomaPalabraEnum.un:
                    strPalabra = "un|a";
                    break;
                case IdiomaPalabraEnum.Tamano:
                    strPalabra = "Tamaño|Size";
                    break;
                case IdiomaPalabraEnum.Tipo:
                    strPalabra = "Tipo|Type";
                    break;
                case IdiomaPalabraEnum.de:
                    strPalabra = "de|of";
                    break;
                case IdiomaPalabraEnum.Ciudad:
                    strPalabra = "Ciudad|City";
                    break;
                case IdiomaPalabraEnum.Pais:
                    strPalabra = "Pais|Country";
                    break;
                case IdiomaPalabraEnum.Direccion:
                    strPalabra = "Dirección|Address";
                    break;
                case IdiomaPalabraEnum.Telefono:
                    strPalabra = "Telefono|Phone";
                    break;
                case IdiomaPalabraEnum.Celular:
                    strPalabra = "Celular|Cell Phone";
                    break;
                case IdiomaPalabraEnum.o:
                    strPalabra = "o|or";
                    break;
                case IdiomaPalabraEnum.SitioWeb:
                    strPalabra = "Sitio Web|Website";
                    break;
                case IdiomaPalabraEnum.Contacto:
                    strPalabra = "Contacto|Contact";
                    break;
                case IdiomaPalabraEnum.Correo:
                    strPalabra = "Correo|Email";
                    break;
                case IdiomaPalabraEnum.Empresario:
                    strPalabra = "Empresario|Businessman";
                    break;
                case IdiomaPalabraEnum.Crear:
                    strPalabra = "Crear|Create";
                    break;
                case IdiomaPalabraEnum.Usuario:
                    strPalabra = "Usuario|User";
                    break;
                case IdiomaPalabraEnum.Nit:
                    strPalabra = "Nit|Tax id";
                    break;
                case IdiomaPalabraEnum.ModificaPrecio:
                    strPalabra = "Modifica Precio|Change Price";
                    break;
                case IdiomaPalabraEnum.Campana:
                    strPalabra = "Campaña|Discount Campaign";
                    break;
                case IdiomaPalabraEnum.HoraInicial:
                    strPalabra = "Hora Inicial|Start Time";
                    break;
                case IdiomaPalabraEnum.HoraFinal:
                    strPalabra = "Hora Final|End Time";
                    break;
                case IdiomaPalabraEnum.NombreObligatorio:
                    strPalabra = "El Nombre es obligatorio|Name is required";
                    break;
                case IdiomaPalabraEnum.CampanaFechaInicial:
                    strPalabra = "La Fecha Inicial no tiene el formato de fecha correcto|The Start Date does not have the correct date format";
                    break;
                case IdiomaPalabraEnum.CampanaFechaFinal:
                    strPalabra = "La Fecha Final no tiene el formato de fecha correcto|The End Date does not have the correct date format";
                    break;
                case IdiomaPalabraEnum.CampanaFechaInicialFinal:
                    strPalabra = "La Fecha Inicial debe ser menor o igual a Fecha Final|Start Date must be less than or equal to End Date";
                    break;
                case IdiomaPalabraEnum.CampanaHoraInicial:
                    strPalabra = "La Hora Inicial no tiene el formato de hora correcto|Start Time does not have the correct time format";
                    break;
                case IdiomaPalabraEnum.CampanaHoraFinal:
                    strPalabra = "La Hora Final no tiene el formato de hora correcto|End Time does not have the correct time format";
                    break;
                case IdiomaPalabraEnum.CampanaHoraInicialFinal:
                    strPalabra = "La Hora Inicial debe ser menor o igual a Hora Final|Start Time must be less than or equal to End Time";
                    break;
                case IdiomaPalabraEnum.CampanaErrorGuardar:
                    strPalabra = "Error al guardar la campaña, contacte el administrador del sistema|Failed to save the campaign, contact your system administrator";
                    break;
                case IdiomaPalabraEnum.CampanaValidacion:
                    strPalabra = "Validación Campaña|Validation Campaign Discounts";
                    break;
                case IdiomaPalabraEnum.Todos:
                    strPalabra = "Todos|Alls";
                    break;
                case IdiomaPalabraEnum.Excluir:
                    strPalabra = "Excluir|Exclude";
                    break;
                case IdiomaPalabraEnum.TipoDescuento:
                    strPalabra = "Tipo Descuento|Type Discount";
                    break;
                case IdiomaPalabraEnum.CampanaArticulo:
                    strPalabra = "El campo articulo es obligatorio|The item field is required";
                    break;
                case IdiomaPalabraEnum.CampanaGrupoArticulo:
                    strPalabra = "El campo grupo de articulo es obligatorio|The group item field is required";
                    break;
                case IdiomaPalabraEnum.CampanaValorDescuento:
                    strPalabra = "El campo valor descuento no tiene el formato correcto|The discount value field is not in the correct format";
                    break;
                case IdiomaPalabraEnum.CampanaArticuloErrorGuardar:
                    strPalabra = "Error al guardar articulo en campaña de descuento|Error saving item in discount campaign";
                    break;
                case IdiomaPalabraEnum.CampanaArticuloErrorId:
                    strPalabra = "Identificador de campaña no valido|Invalid campaign identifier";
                    break;
                case IdiomaPalabraEnum.CampanaCliente:
                    strPalabra = "El campo cliente es obligatorio|The customer field is required";
                    break;
                case IdiomaPalabraEnum.CampanaGrupoCliente:
                    strPalabra = "El campo grupo cliente es obligatorio|The customer group field is required";
                    break;
                case IdiomaPalabraEnum.CampanaClienteErrorGuardar:
                    strPalabra = "Error al guardar cliente en campaña de descuento|Error saving customer in discount campaign";
                    break;
                case IdiomaPalabraEnum.GrupoCliente:
                    strPalabra = "Grupo Cliente|Customer Group";
                    break;
                case IdiomaPalabraEnum.CuentaContable:
                    strPalabra = "Cuenta Contable|Account";
                    break;
                case IdiomaPalabraEnum.CodigoValidacion:
                    strPalabra = "El campo codigo no tiene el formato correcto|The code field is not in the correct format";
                    break;
                case IdiomaPalabraEnum.Validacion:
                    strPalabra = "Validación|Validation";
                    break;
                case IdiomaPalabraEnum.GrupoClienteExite:
                    strPalabra = "El código del Grupo de cliente ya existe|the group customer code exists";
                    break;
                case IdiomaPalabraEnum.GrupoClienteErrorGuardar:
                    strPalabra = "Error al guardar grupo de cliente|Error to save group customer";
                    break;
                case IdiomaPalabraEnum.TipoTarjetaCredito:
                    strPalabra = "Tipo Tarjeta de Credito|Credit Card Type";
                    break;
                case IdiomaPalabraEnum.CuentaContableObligatorio:
                    strPalabra = "Cuenta contable es obligatorio|Account is mandatory";
                    break;
                case IdiomaPalabraEnum.TipoTarjetaCreditoErrorGuardar:
                    strPalabra = "Error al guardar tipo tarjeta credito|Error to save credit card type";
                    break;
                case IdiomaPalabraEnum.Texto:
                    strPalabra = "Texto|Text";
                    break;
                case IdiomaPalabraEnum.VentaRapida:
                    strPalabra = "Venta Rapida|Quick Sale";
                    break;
                case IdiomaPalabraEnum.Imagen:
                    strPalabra = "Imagen|Image";
                    break;
                case IdiomaPalabraEnum.ArticuloObligatorio:
                    strPalabra = "El Articulo es obligatorio|Item field is required";
                    break;
                case IdiomaPalabraEnum.CantidadFormato:
                    strPalabra = "El campo cantidad no tiene el formato correcto|The quantity field is not in the correct format";
                    break;
                case IdiomaPalabraEnum.ImagenObligatorio:
                    strPalabra = "La imagen es obligatoria|Image is required";
                    break;
                case IdiomaPalabraEnum.VentaRapidaErrorGuardar:
                    strPalabra = "Error al guardar venta rapida|Error to save quick sale";
                    break;
                case IdiomaPalabraEnum.GestionVentaRapida:
                    strPalabra = "Gestión venta rapida|Management quick sale";
                    break;
                case IdiomaPalabraEnum.ValorAntesIVA:
                    strPalabra = "Valor antes IVA|Amount before TAX";
                    break;
                case IdiomaPalabraEnum.ValorIVA:
                    strPalabra = "Valor IVA|Amount TAX";
                    break;
                case IdiomaPalabraEnum.ValorTotal:
                    strPalabra = "Valor total|Total amount";
                    break;
                case IdiomaPalabraEnum.Comision:
                    strPalabra = "% Comisión|% Commission";
                    break;
                case IdiomaPalabraEnum.Vendedor:
                    strPalabra = "Vendedor|Vendor";
                    break;
                case IdiomaPalabraEnum.ComisionDecimal:
                    strPalabra = "Campo comisión debe ser decimal|Commission field must be decimal";
                    break;
                case IdiomaPalabraEnum.VendedorGuardar:
                    strPalabra = "Vendedor guardado con exito|Salesman is saved";
                    break;
                case IdiomaPalabraEnum.VendedorError:
                    strPalabra = "Error al guardar el vendedor|Error Salesman is not saved";
                    break;
                case IdiomaPalabraEnum.VendedoresCreados:
                    strPalabra = "Vendedores creados|Salesmans list";
                    break;
                case IdiomaPalabraEnum.ZipCode:
                    strPalabra = "Código Zip|Zip Code";
                    break;
                case IdiomaPalabraEnum.Rapida:
                    strPalabra = "Rapida|Quick";
                    break;
            }
            return TraducirPalabra(Idioma,strPalabra);
        }

        private string TraducirPalabra(IdiomaEnum Idioma, string Palabra)
        {
            return Palabra.Split('|')[Idioma.GetHashCode()];
        }
    }
}
