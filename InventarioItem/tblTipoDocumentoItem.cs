namespace InventarioItem
{
    public class tblTipoDocumentoItem
    {

        public enum TipoDocumentoEnum
        {
            venta = 1,
            compra = 2,
            cotizacion = 3,
            notaCreditoVenta = 4,
            entradaMercancia = 5,
            salidaMercancia = 6,
            notaCreditoCompra = 7,
            Remision = 8,
            OrdenFabricacion = 9,
            CuentaCobro = 10
        }

        public short idTipoDocumento { get; set; }
        public string Nombre { get; set; }
        public string TablaDocumento { get; set; }
        public string TablaDetalle { get; set; }
        public string idTipoSocioNegocio { get; set; }
        public bool AfectaInventario { get; set; }
        public short SentidoInventario { get; set; }
        public bool ManejaListaPrecios { get; set; }
        public string TablaNumeracion { get; set; }
    }
}
