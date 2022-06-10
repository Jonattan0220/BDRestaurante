using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public class PlatoMasVendidoViewModel
    {
        public string? Plato { get; set; }
        [Display(Name = "Cantidad de platos vendidos")]
        public int CantidadVendidos { get; set; }
        [Display(Name = "Monto total facturado")]
        public int MontoFacturado { get; set; }
    }
}
