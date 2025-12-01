using System.Collections.Generic;

namespace TechNova.Models
{
    public class VentaCreateViewModel
    {
        public int ClienteId { get; set; }

        public List<VentaDetalle> Items { get; set; } = new();
        public List<Cliente> Clientes { get; set; } = new();
        public List<Producto> Productos { get; set; } = new();
    }
}
