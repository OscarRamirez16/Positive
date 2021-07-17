namespace InventarioItem
{
    public class tblCajaItem
    {
        public long idCaja { get; set; }
        public string nombre { get; set; }
        public long idBodega { get; set; }
        public string Bodega { get; set; }
        public long idEmpresa { get; set; }
        public string ProximoValor { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }
        public string Resolucion { get; set; }
        public bool Activo { get; set; }
    }
}
