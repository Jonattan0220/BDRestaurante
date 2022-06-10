using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public class VentaMeseroViewModel
    {
        [Display(Name = "Mesero")]
        public int MeseroId { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        [Display(Name = "Total (Suma) de ventas en pesos colombianos")]
        public int SumaVentas { get; set; }
    }
}
