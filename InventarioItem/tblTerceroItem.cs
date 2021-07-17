using System;
using System.Collections.Generic;

namespace InventarioItem
{
    public class tblTerceroItem
    {
        public tblTerceroItem()
        {
            IdTercero = 0;
            Retenciones = new List<tblTerceroRetencionItem>();
        }
        public long IdTercero { get; set; }
        public short idTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Mail { get; set; }
        public string Direccion { get; set; }
        public short idCiudad { get; set; }
        public string Ciudad { get; set; }
        public long idEmpresa { get; set; }
        public string Empresa { get; set; }
        public string TipoTercero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public long IdListaPrecio { get; set; }
        public long idGrupoCliente { get; set; }
        public string GrupoCliente { get; set; }
        public bool Generico { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public List<tblTerceroRetencionItem> Retenciones { get; set; }
    }
}
