namespace InventarioItem
{
    public class tblTipoPagoItem
    {
        public long idTipoPago { get; set; }
        public long idPago { get; set; }
        public decimal ValorPago { get; set; }
        public short idFormaPago { get; set; }
        public string FormaPago { get; set; }
        public string voucher { get; set; }
        public short idTipoTarjetaCredito { get; set; }
    }
}
