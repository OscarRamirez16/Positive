using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblListaMaterialesDetalleItem
    {
        public string IdListaMaterialesDetalle { get; set; }
        public string IdListaMateriales { get; set; }
        public long IdArticulo { get; set; }
        public string Articulo { get; set; }
        public decimal Cantidad { get; set; }
    }
}
