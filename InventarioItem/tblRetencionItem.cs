namespace InventarioItem
{
    public class tblRetencionItem
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Base { get; set; }
        public bool Activo { get; set; }
        public long IdEmpresa { get; set; }
    }
}
