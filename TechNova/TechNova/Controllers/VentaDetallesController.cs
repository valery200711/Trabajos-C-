using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TechNova.Models;

namespace TechNova.Controllers
{
    public class VentaDetallesController : Controller
    {
        private readonly TechNovaDbContext _context;

        public VentaDetallesController(TechNovaDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var detalles = _context.VentaDetalle
                .Include(v => v.Producto)
                .Include(v => v.Venta)
                .ThenInclude(v => v.Cliente)
                .ToListAsync();

            return View(await detalles);
        }

        // GET: VentaDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var detalle = await _context.VentaDetalle
                .Include(v => v.Producto)
                .Include(v => v.Venta)
                .ThenInclude(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.VentaDetalleId == id);

            if (detalle == null)
                return NotFound();

            return View(detalle);
        }

        // ❌ SE ELIMINA CREATE — LOS DETALLES SE CREAN AUTOMÁTICAMENTE

        // ❌ SE ELIMINA EDIT — NO SE MODIFICAN DETALLES DE VENTA

        // GET: VentaDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var detalle = await _context.VentaDetalle
                .Include(v => v.Producto)
                .Include(v => v.Venta)
                .ThenInclude(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.VentaDetalleId == id);

            if (detalle == null)
                return NotFound();

            return View(detalle);
        }

        // POST: VentaDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalle = await _context.VentaDetalle.FindAsync(id);
            if (detalle != null)
            {
                _context.VentaDetalle.Remove(detalle);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VentaDetalleExists(int id)
        {
            return _context.VentaDetalle.Any(e => e.VentaDetalleId == id);
        }
    }
}
