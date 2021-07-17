using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblUsuarioItem
    {

        public tblUsuarioItem()
        {
            idUsuario = 0;
        }

        public long idUsuario { get; set; }
        public short idTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string NombreCompleto { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public short idCiudad { get; set; }
        public long idEmpresa { get; set; }
        public long idBodega { get; set; }
        public bool Activo { get; set; }
        public short IdIdioma { get; set; }
        public bool ModificaPrecio { get; set; }
        public bool EsAdmin { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public bool ManejaPrecioConIVA { get; set; }
        public bool ManejaCostoConIVA { get; set; }
        public bool ManejaDescuentoConIVA { get; set; }
        public bool VerCuadreCaja { get; set; }
        public decimal Impoconsumo { get; set; }
        public int PosicionInicialCodigo { get; set; }
        public int LongitudCodigo { get; set; }
        public int PosicionInicialCantidad { get; set; }
        public int LongitudCantidad { get; set; }
        public long IdTercero { get; set; }
    }
}
