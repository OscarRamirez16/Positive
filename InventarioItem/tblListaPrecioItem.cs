using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblListaPrecioItem 
    { 
        public long IdListaPrecio { get; set; } 
        public string Nombre { get; set; } 
        public decimal Factor { get; set; } 
        public long IdEmpresa { get; set; } 
        public bool Activo { get; set; }
        public bool Aumento { get; set; } 
    }
}
