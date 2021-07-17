namespace InventarioItem
{
    public class tblPagoDetalleItem
    {
        public long idPagoDetalle { get; set; }
        public long idPago { get; set; }
        public long idDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal valorAbono { get; set; }
        public string formaPago { get; set; }
    }
}
