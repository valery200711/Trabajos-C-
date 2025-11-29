using System;
using System.Collections.Generic;

namespace TechNova.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
}
