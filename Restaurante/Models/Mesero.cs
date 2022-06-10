using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public partial class Mesero
    {
        public Mesero()
        {
            Facturas = new HashSet<Factura>();
        }
        [Display(Name = "Mesero")]
        public int MeseroId { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int Edad { get; set; }
        public int Antiguedad { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
