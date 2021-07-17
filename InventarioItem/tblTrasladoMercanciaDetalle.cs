using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblTrasladoMercanciaDetalle
    {
        public long IdDetalle { get; set; }
        public long IdTraslado { get; set; }
        public long IdArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public long IdBodegaOrigen { get; set; }
        public long IdBodegaDestino { get; set; }
    }
}
