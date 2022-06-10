using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public int ClienteId { get; set; }
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        [Display(Name = "Dirección")]
        public string Direccion { get; set; } = null!;
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = null!;

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
