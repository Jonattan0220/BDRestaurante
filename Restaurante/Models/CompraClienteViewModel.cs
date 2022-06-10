using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public class CompraClienteViewModel
    {
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        [Display(Name = "Consumo o factura")]
        public int FacturaId { get; set; }
        [Display(Name = "Total (suma) valor facturado")]
        public int SumaValorFactura { get; set; }
    }
}
