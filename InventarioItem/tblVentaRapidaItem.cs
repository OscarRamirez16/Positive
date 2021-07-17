using System;

namespace InventarioItem
{
    public class tblVentaRapidaItem
    {
        public long idVentaRapida { get; set; }
        public string Nombre { get; set; }
        public long idArticulo { get; set; }
        public string Articulo { get; set; }
        public byte[] Imagen { get; set; }
        public decimal Cantidad { get; set; }
        public bool Activo { get; set; }
        public DateTime Fecha { get; set; }
        public long idUsuario { get; set; }
        public long idEmpresa { get; set; }
        public string Linea { get; set; }
        public decimal Precio { get; set; }
        public decimal ValorIVA { get; set; }
        public decimal Stock { get; set; }
    }
}
