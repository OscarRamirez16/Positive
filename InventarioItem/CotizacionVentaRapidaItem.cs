using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class CotizacionVentaRapidaItem { 
        public long idCotizacionVentaRapida { get; set; } 
        public long idCotizacion { get; set; } 
        public long Articulo { get; set; } 
        public string Descripcion { get; set; } 
        public decimal Cantidad { get; set; } 
        public decimal ValorIVA { get; set; } 
        public decimal Precio { get; set; } 
        public decimal Impoconsumo { get; set; }
        public long idVentaRapida { get; set; }
    }

    public class PedidoAbierto {
        public long idDocumento { get; set; }
		public string NumeroDocumento { get; set; }
        public DateTime Fecha { get; set; }
        public long IdVendedor { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
    }
}
