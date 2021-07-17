namespace InventarioItem
{
    public class tblDocumentoRetencionItem
    {
        public long IdDocumento { get; set; }
        public long IdRetencion { get; set; }
        public int TipoDocumento { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Base { get; set; }
        public decimal Valor { get; set; }
    }
}
