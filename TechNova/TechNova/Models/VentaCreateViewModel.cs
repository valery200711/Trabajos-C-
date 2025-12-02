using System.Collections.Generic;

namespace TechNova.Models
{
    public class VentaCreateViewModel
    {
        public int ClienteId { get; set; }

        public List<VentaItemViewModel> Items { get; set; } = new();
        public List<Cliente> Clientes { get; set; } = new();
        public List<Producto> Productos { get; set; } = new();
    }

    public class VentaItemViewModel
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
