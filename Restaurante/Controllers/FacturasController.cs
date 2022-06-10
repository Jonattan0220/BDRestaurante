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
    public class FacturasController : Controller
    {
        private readonly BDRestauranteContext _context;

        public FacturasController(BDRestauranteContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var bDRestauranteContext = _context.Facturas.Include(f => f.Cliente).Include(f => f.Mesa).Include(f => f.Mesero);
            return View(await bDRestauranteContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Mesa)
                .Include(f => f.Mesero)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Identificacion");
            ViewData["MesaId"] = new SelectList(_context.Mesas, "MesaId", "NroMesa");
            ViewData["MeseroId"] = new SelectList(_context.Meseros, "MeseroId", "MeseroId");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturaId,ClienteId,MesaId,MeseroId,FechaHora")] Factura factura)
        {
            if (ModelState.IsValid)
            {
               
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Identificacion");
            ViewData["MesaId"] = new SelectList(_context.Mesas, "MesaId", "NroMesa");
            ViewData["MeseroId"] = new SelectList(_context.Meseros, "MeseroId", "MeseroId");
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", factura.ClienteId);
            ViewData["MesaId"] = new SelectList(_context.Mesas, "MesaId", "MesaId", factura.MesaId);
            ViewData["MeseroId"] = new SelectList(_context.Meseros, "MeseroId", "MeseroId", factura.MeseroId);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacturaId,ClienteId,MesaId,MeseroId,FechaHora")] Factura factura)
        {
            if (id != factura.FacturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.FacturaId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", factura.ClienteId);
            ViewData["MesaId"] = new SelectList(_context.Mesas, "MesaId", "MesaId", factura.MesaId);
            ViewData["MeseroId"] = new SelectList(_context.Meseros, "MeseroId", "MeseroId", factura.MeseroId);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Mesa)
                .Include(f => f.Mesero)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facturas == null)
            {
                return Problem("Entity set 'BDRestauranteContext.Facturas'  is null.");
            }
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
          return (_context.Facturas?.Any(e => e.FacturaId == id)).GetValueOrDefault();
        }
    }
}
