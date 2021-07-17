namespace InventarioItem
{
    public class tblConciliacionDetalleItem
    {
        public long IdConciliacionDetalle { get; set; }
        public long IdConciliacion { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal Valor { get; set; }
    }
}
