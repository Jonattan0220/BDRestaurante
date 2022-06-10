using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public partial class Mesa
    {
        public Mesa()
        {
            Facturas = new HashSet<Factura>();
        }

        public int MesaId { get; set; }
        [Display(Name = "Número de mesa")]
        public int NroMesa { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Reservada { get; set; }
        public int Puestos { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
