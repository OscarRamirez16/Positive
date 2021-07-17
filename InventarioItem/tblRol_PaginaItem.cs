using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblRol_PaginaItem
    {
        public short idRol { get; set; }
        public short idPagina { get; set; }
        public bool Leer { get; set; }
        public bool Insertar { get; set; }
        public bool Actualizar { get; set; }
        public bool Eliminar { get; set; }
        public long idEmpresa { get; set; }
    }
}
