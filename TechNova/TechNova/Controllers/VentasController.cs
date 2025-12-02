using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechNova.Models;

namespace TechNova.Controllers
{
    public class VentasController : Controller
    {
        private readonly TechNovaDbContext _context;

        public VentasController(TechNovaDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Administrador")]

        // GET: Ventas

        public async Task<IActionResult> Index()
        {
            var ventas = _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.VentaDetalles)
                    .ThenInclude(d => d.Producto);

            return View(await ventas.ToListAsync());
        }
        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.VentaDetalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(m => m.VentaId == id);

            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            var vm = new VentaCreateViewModel
            {
                Clientes = _context.Clientes.ToList(),
                Productos = _context.Productos.ToList()
            };

            return View(vm);
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VentaCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Clientes = _context.Clientes.ToList();
                vm.Productos = _context.Productos.ToList();
                return View(vm);
            }

            if (vm.Items == null || !vm.Items.Any())
            {
                ModelState.AddModelError(string.Empty, "La venta debe contener al menos un producto.");
                vm.Clientes = _context.Clientes.ToList();
                vm.Productos = _context.Productos.ToList();
                return View(vm);
            }

            // Crear la venta
            var venta = new Venta
            {
                ClienteId = vm.ClienteId,
                Fecha = DateTime.Now,
                Total = 0m // calcularemos después
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            decimal totalVenta = 0m;

            // Crear los detalles
            foreach (var item in vm.Items)
            {
                // Asegurarse de que el producto exista y tomar precio real
                var producto = await _context.Productos.FindAsync(item.ProductoId);
                if (producto == null)
                {
                    ModelState.AddModelError(string.Empty, "Producto no encontrado.");
                    vm.Clientes = _context.Clientes.ToList();
                    vm.Productos = _context.Productos.ToList();
                    return View(vm);
                }

                if (producto.Stock < item.Cantidad)
                {
                    ModelState.AddModelError(string.Empty, $"No hay stock suficiente para {producto.Nombre}");
                    vm.Clientes = _context.Clientes.ToList();
                    vm.Productos = _context.Productos.ToList();
                    return View(vm);
                }

                producto.Stock -= item.Cantidad;

                var detalle = new VentaDetalle
                {
                    VentaId = venta.VentaId,
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = producto.PrecioUnitario,
                    Subtotal = producto.PrecioUnitario * item.Cantidad
                };

                totalVenta += detalle.Subtotal;

                _context.VentaDetalle.Add(detalle);
            }

            // Actualizar total de la venta
            venta.Total = totalVenta;
            _context.Ventas.Update(venta);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", venta.ClienteId);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VentaId,ClienteId,Fecha,Total")] Venta venta)
        {
            if (id != venta.VentaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.VentaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", venta.ClienteId);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.VentaId == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.VentaId == id);
        }
    }
}
