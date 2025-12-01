using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechNova.Models
{
    public partial class VentaDetalle
    {
        [Key]
        public int VentaDetalleId { get; set; }

        [Required]
        public int VentaId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }

        // Relación con Producto
        public virtual Producto Producto { get; set; }

        // Relación con Venta
        public virtual Venta Venta { get; set; }
    }
}
