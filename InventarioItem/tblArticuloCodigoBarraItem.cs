using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblArticuloCodigoBarraItem
    {
        public long IdCodigo { get; set; }
        public string CodigoBarra { get; set; }
        public long IdArticulo { get; set; }
        public bool Activo { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
