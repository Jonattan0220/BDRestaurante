using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleXfacturas = new HashSet<DetalleXfactura>();
        }
        [Display(Name = "Factura")]
        public int FacturaId { get; set; }
        [Required(ErrorMessage = "Seleccionar un cliente")]
        [Display(Name = "Identificación del cliente")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Seleccionar un número de mesa")]
        [Display(Name = "Número de mesa")]
        public int MesaId { get; set; }
        [Required(ErrorMessage = "Seleccionar un mesero")]
        [Display(Name = "Mesero")]
        public int MeseroId { get; set; }
        [Required(ErrorMessage = "Seleccionar un fecha hora (puede seleccionar y teclear Enter)")]
        [Display(Name = "Fecha y Hora")]
        public DateTime FechaHora { get; set; }
        [ValidateNever]
        public virtual Cliente Cliente { get; set; } = null!;
        [ValidateNever]
        public virtual Mesa Mesa { get; set; } = null!;
        [ValidateNever]
        public virtual Mesero Mesero { get; set; } = null!;
        public virtual ICollection<DetalleXfactura> DetalleXfacturas { get; set; }
    }
}
