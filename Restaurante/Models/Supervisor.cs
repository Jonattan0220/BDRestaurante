using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public partial class Supervisor
    {
        public Supervisor()
        {
            DetalleXfacturas = new HashSet<DetalleXfactura>();
        }
        [Display(Name = "Supervisor")]
        public int SupervirsorId { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int Edad { get; set; }
        public int Antiguedad { get; set; }

        public virtual ICollection<DetalleXfactura> DetalleXfacturas { get; set; }
    }
}
