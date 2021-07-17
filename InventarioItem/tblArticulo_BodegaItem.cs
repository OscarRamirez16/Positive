namespace InventarioItem
{
    public class tblArticulo_BodegaItem
    {
        public long IdArticulo { get; set; }
        public string Articulo { get; set; }
        public long IdBodega { get; set; }
        public string Bodega { get; set; }
        public decimal Cantidad { get; set; }
        public short IdTipoManejoPrecio { get; set; }
        public decimal Precio { get; set; }
        public decimal Costo { get; set; }
    }
}
