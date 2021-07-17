using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblCampanaArticuloItem
    {
        public long idCampanaArticulo { get; set; }
        public long idCampana { get; set; }
        public bool Excluir { get; set; }
        public short TipoCampanaArticulo { get; set; }
        public string TipoCampanaArticuloNombre { get; set; }
        public string Codigo { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
        public short TipoDescuento { get; set; }
        public string TipoDescuentoNombre { get; set; }
        public decimal ValorDescuento { get; set; }
        public long idUsuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
