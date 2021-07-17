namespace InventarioItem
{
    public class tblLineaItem
    {
        public long IdLinea { get; set; }
        public string Nombre { get; set; }
        public long IdEmpresa { get; set; }
        public bool Activo { get; set; }

        public tblLineaItem()
        {
            IdLinea = 0;
        }
    }
}
