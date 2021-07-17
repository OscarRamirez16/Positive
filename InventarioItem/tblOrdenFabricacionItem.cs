using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblOrdenFabricacionItem
    {
        public long IdOrdenFabricacion { get; set; }
        public string IdListaMateriales { get; set; }
        public string ListaMateriales { get; set; }
        public DateTime FechaCreacion { get; set; }
        public long IdUsuario { get; set; }
        public string Usuario { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public long IdEmpresa { get; set; }
        public decimal Cantidad { get; set; }
        public List<tblOrdenFabricacionDetalleItem> Detalles { get; set; }
    }
}
