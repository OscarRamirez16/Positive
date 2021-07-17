using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class JSONItem
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public JSONItem(string _id, string _nombre) {
            id = _id;
            nombre = _nombre;
        }
    }
}
