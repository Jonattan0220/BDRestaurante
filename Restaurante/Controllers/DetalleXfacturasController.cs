using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    public class DetalleXfacturasController : Controller
    {
        private readonly BDRestauranteContext _context;

        public DetalleXfacturasController(BDRestauranteContext context)
        {
            _context = context;
        }

        // GET: DetalleXfacturas
        public async Task<IActionResult> Index()
        {
            var bDRestauranteContext = _context.DetalleXfacturas.Include(d => d.Factura).Include(d => d.Supervisor);
            return View(await bDRestauranteContext.ToListAsync());
        }

        // GET: PlatoMasVendido 
        public async Task<IActionResult> PlatoMasVendido()
        {
            DateTime starDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

            List<Factura> facturas = await _context.Facturas
                                                                                    .Where(x => x.FechaHora >= starDate && x.FechaHora <= endDate)
                                                                                    .ToListAsync();

            List<DetalleXfactura> detallesXFacturasEsp = await _context.DetalleXfacturas
                                                                                              .ToListAsync();

            var PlatosVendidos = (from factura in facturas
                                                       join detalle in detallesXFacturasEsp on factura.FacturaId equals detalle.FacturaId
                                                       group new { factura, detalle } by new { detalle.Plato } into g
                                                       select new PlatoMasVendidoViewModel()
                                                       {
                                                           Plato = g.Key.Plato,
                                                           CantidadVendidos = g.Count(),
                                                           MontoFacturado = g.Sum(x => x.detalle.Valor)
                                                       }).ToList();

            var platoMasVendido = PlatosVendidos.OrderByDescending(x => x.MontoFacturado).First();

            ViewData["starDate"] = starDate.ToString("yyyy-MM-ddTHH:mm");
            ViewData["endDate"] = endDate.ToString("yyyy-MM-ddTHH:mm");
            return View(platoMasVendido);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlatoMasVendido(DateTime starDate, DateTime endDate)
        {
            List<Factura> facturas = await _context.Facturas
                                                                                    .Where(x => x.FechaHora >= starDate && x.FechaHora <= endDate)
                                                                                    .ToListAsync();

            List<DetalleXfactura> detallesXFacturasEsp = await _context.DetalleXfacturas
                                                                                              .ToListAsync();

            var PlatosVendidos = (from factura in facturas
                                  join detalle in detallesXFacturasEsp on factura.FacturaId equals detalle.FacturaId
                                  group new { factura, detalle } by new { detalle.Plato } into g
                                  select new PlatoMasVendidoViewModel()
                                  {
                                      Plato = g.Key.Plato,
                                      CantidadVendidos = g.Count(),
                                      MontoFacturado = g.Sum(x => x.detalle.Valor)
                                  }).ToList();

            var platoMasVendido = PlatosVendidos.Where(x => x != null).OrderByDescending(x => x.MontoFacturado).FirstOrDefault();

            ViewData["starDate"] = starDate.ToString("yyyy-MM-ddTHH:mm");
            ViewData["endDate"] = endDate.ToString("yyyy-MM-ddTHH:mm");

            return View(platoMasVendido);
        }

        // GET: DetalleXfacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalleXfacturas == null)
            {
                return NotFound();
            }

            var detalleXfactura = await _context.DetalleXfacturas
                .Include(d => d.Factura)
                .Include(d => d.Supervisor)
                .FirstOrDefaultAsync(m => m.IdDetalleXfactura == id);
            if (detalleXfactura == null)
            {
                return NotFound();
            }

            return View(detalleXfactura);
        }

        // GET: DetalleXfacturas/Create
        public IActionResult Create()
        {
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId");
            ViewData["SupervisorId"] = new SelectList(_context.Supervisors, "SupervirsorId", "SupervirsorId");
            return View();
        }

        // POST: DetalleXfacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleXfactura,FacturaId,SupervisorId,Plato,Valor")] DetalleXfactura detalleXfactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleXfactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", detalleXfactura.FacturaId);
            ViewData["SupervisorId"] = new SelectList(_context.Supervisors, "SupervirsorId", "SupervirsorId", detalleXfactura.SupervisorId);
            return View(detalleXfactura);
        }

        // GET: DetalleXfacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetalleXfacturas == null)
            {
                return NotFound();
            }

            var detalleXfactura = await _context.DetalleXfacturas.FindAsync(id);
            if (detalleXfactura == null)
            {
                return NotFound();
            }
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", detalleXfactura.FacturaId);
            ViewData["SupervisorId"] = new SelectList(_context.Supervisors, "SupervirsorId", "SupervirsorId", detalleXfactura.SupervisorId);
            return View(detalleXfactura);
        }

        // POST: DetalleXfacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleXfactura,FacturaId,SupervisorId,Plato,Valor")] DetalleXfactura detalleXfactura)
        {
            if (id != detalleXfactura.IdDetalleXfactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleXfactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleXfacturaExists(detalleXfactura.IdDetalleXfactura))
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
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", detalleXfactura.FacturaId);
            ViewData["SupervisorId"] = new SelectList(_context.Supervisors, "SupervirsorId", "SupervirsorId", detalleXfactura.SupervisorId);
            return View(detalleXfactura);
        }

        // GET: DetalleXfacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetalleXfacturas == null)
            {
                return NotFound();
            }

            var detalleXfactura = await _context.DetalleXfacturas
                .Include(d => d.Factura)
                .Include(d => d.Supervisor)
                .FirstOrDefaultAsync(m => m.IdDetalleXfactura == id);
            if (detalleXfactura == null)
            {
                return NotFound();
            }

            return View(detalleXfactura);
        }

        // POST: DetalleXfacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetalleXfacturas == null)
            {
                return Problem("Entity set 'BDRestauranteContext.DetalleXfacturas'  is null.");
            }
            var detalleXfactura = await _context.DetalleXfacturas.FindAsync(id);
            if (detalleXfactura != null)
            {
                _context.DetalleXfacturas.Remove(detalleXfactura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleXfacturaExists(int id)
        {
          return (_context.DetalleXfacturas?.Any(e => e.IdDetalleXfactura == id)).GetValueOrDefault();
        }
    }
}
