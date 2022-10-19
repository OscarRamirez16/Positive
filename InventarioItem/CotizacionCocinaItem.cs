using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class CotizacionCocinaItem
    {
        public long idCotizacionCocina { get; set; }
        public long idCotizacion { get; set; }
        public long idArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string CodigoArticulo { get; set; }
        public string Nombre { get; set; }
        public string Vendedor { get; set; }
        public string Observaciones { get; set; }
        public string Usuario { get; set; }
        public string NumeroDocumento { get; set; }
    }

    public class CotizacionCocinaDataItem
    {
        public long idCotizacion { get; set; }
        public long idArticulo { get; set; }
        public decimal Cantidad { get; set; }
    }
}
