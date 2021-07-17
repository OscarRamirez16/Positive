using System;

namespace InventarioItem
{
    public class tblConciliacionItem
    {
        public long IdConciliacion { get; set; }
        public DateTime Fecha { get; set; }
        public long IdTercero { get; set; }
        public long IdUsuario { get; set; }
        public long IdRetiro { get; set; }
        public string Observaciones { get; set; }
        public decimal Total { get; set; }
        public long IdEmpresa { get; set; }
    }
}
