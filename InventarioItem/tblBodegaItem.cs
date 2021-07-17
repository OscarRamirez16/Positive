namespace InventarioItem
{
    public class tblBodegaItem
    {

        public tblBodegaItem()
        {
            IdBodega = 0;
        }

        public long IdBodega { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public long idEmpresa { get; set; }
        public bool Activo { get; set; }
    }
}
