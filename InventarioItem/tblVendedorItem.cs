using System;

namespace InventarioItem
{
    public class tblVendedorItem
    {
        public long idVendedor { get; set; }
        public string Nombre { get; set; }
        public decimal Comision { get; set; }
        public long idBodega { get; set; }
        public long idUsuarioVendedor { get; set; }
        public bool Activo { get; set; }
        public DateTime Fecha { get; set; }
        public long idUsuario { get; set; }
        public long idEmpresa { get; set; }
    }
}
