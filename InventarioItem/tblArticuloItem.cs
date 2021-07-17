namespace InventarioItem
{
    public class tblArticuloItem
    {

        public tblArticuloItem()
        {
            IdArticulo = 0;
        }

        public long IdArticulo { get; set; }
        public string CodigoArticulo { get; set; }
        public string Nombre { get; set; }
        public string Presentacion { get; set; }
        public long IdLinea { get; set; }
        public string Linea { get; set; }
        public decimal IVACompra { get; set; }
        public decimal IVAVenta { get; set; }
        public string CodigoBarra { get; set; }
        public long IdTercero { get; set; }
        public string Tercero { get; set; }
        public long IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public long IdBodega { get; set; }
        public string Bodega { get; set; }
        public bool EsInventario { get; set; }
        public decimal StockMinimo { get; set; }
        public bool Activo { get; set; }
        public bool PrecioAutomatico { get; set; }
        public string Ubicacion { get; set; }
        public string Marca { get; set; }
        public decimal CostoPonderado { get; set; }
        public bool EsCompuesto { get; set; }
        public bool EsArticuloFinal { get; set; }
        public bool EsHijo { get; set; }
        public long IdArticuloPadre { get; set; }
        public string NombrePadre { get; set; }
        public decimal CantidadPadre { get; set; }

        //Para entrada masiva de artículos
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public short IdTipoManejoPrecio { get; set; }
    }
}
