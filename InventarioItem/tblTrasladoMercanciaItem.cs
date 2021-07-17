using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblTrasladoMercanciaItem
    {
        public long IdTraslado { get; set; }
        public DateTime Fecha { get; set; }
        public long IdUsuario { get; set; }
        public string Obervaciones { get; set; }
        public long IdEmpresa { get; set; }
    }
}
