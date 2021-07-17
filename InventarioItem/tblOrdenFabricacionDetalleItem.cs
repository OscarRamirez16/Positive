using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblOrdenFabricacionDetalleItem
    {
        public long IdDetalle { get; set; }
        public long IdOrdenFabricacion { get; set; }
        public long IdArticulo { get; set; }
        public string Articulo { get; set; }
        public decimal Cantidad { get; set; }
        public long IdBodega { get; set; }
    }
}
