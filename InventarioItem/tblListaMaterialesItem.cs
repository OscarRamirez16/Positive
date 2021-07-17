using System;

namespace InventarioItem
{
    public class tblListaMaterialesItem
    {
        public string IdListaMateriales { get; set; }
        public long IdArticulo { get; set; }
        public string Articulo { get; set; }
        public DateTime Fecha { get; set; }
        public long IdUsuario { get; set; }
        public string Usuario { get; set; }
        public long IdEmpresa { get; set; }
        public bool Activo { get; set; }
        public decimal Cantidad { get; set; }
    }
}
