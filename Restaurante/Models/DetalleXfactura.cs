using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public partial class DetalleXfactura
    {
        public int IdDetalleXfactura { get; set; }
        [Required(ErrorMessage = "Seleccionar una factura")]
        [Display(Name = "Factura")]
        public int FacturaId { get; set; }
        [Required(ErrorMessage = "Seleccionar un supervisor")]
        [Display(Name = "Supervisor")]
        public int SupervisorId { get; set; }
        [Display(Name = "Nombre del plato")]
        public string Plato { get; set; } = null!;
        [Display(Name = "Valor en pesos colombianos")]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor introduce un valor mayor a 0")]
        public int Valor { get; set; }
        [ValidateNever]
        public virtual Factura Factura { get; set; } = null!;
        [ValidateNever]
        public virtual Supervisor Supervisor { get; set; } = null!;
    }
}
