using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    public class MeserosController : Controller
    {
        private readonly BDRestauranteContext _context;

        public MeserosController(BDRestauranteContext context)
        {
            _context = context;
        }

        // GET: Meseros
        public async Task<IActionResult> Index()
        {
            return _context.Meseros != null ? 
                          View(await _context.Meseros.ToListAsync()) :
                          Problem("Entity set 'BDRestauranteContext.Meseros'  is null.");
        }

        // GET: Ventas 
        public async Task<IActionResult> Ventas()
        {
            DateTime starDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0,0);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute,0);

            List<Mesero> MeserosFacturacion = await _context.Meseros
                                                                            .Include(mesero => mesero.Facturas.Where(x=>x.FechaHora >= starDate && x.FechaHora <= endDate))
                                                                            .ThenInclude(factura => factura.DetalleXfacturas)
                                                                            .ToListAsync();

            var MeserosVentas = (from meseroFacturacion in MeserosFacturacion
                                                     select new VentaMeseroViewModel()
                                                         {
                                                            MeseroId = meseroFacturacion.MeseroId,
                                                             Nombres = meseroFacturacion.Nombres,
                                                             Apellidos = meseroFacturacion.Apellidos,
                                                             SumaVentas = meseroFacturacion.Facturas.Sum(x => x.DetalleXfacturas.Sum(x => x.Valor)),
                                                         }).ToList();

            ViewData["starDate"] = starDate.ToString("yyyy-MM-ddTHH:mm");
            ViewData["endDate"] = endDate.ToString("yyyy-MM-ddTHH:mm");
            return View(MeserosVentas);
        }
            
        // POST: Meseros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ventas(DateTime starDate, DateTime endDate)
        {
            List<Mesero> MeserosFacturacion = await _context.Meseros
                                                                            .Include(mesero => mesero.Facturas.Where(x => x.FechaHora >= starDate && x.FechaHora <= endDate))
                                                                            .ThenInclude(factura => factura.DetalleXfacturas)
                                                                            .ToListAsync();

            var MeserosVentas = (from meseroFacturacion in MeserosFacturacion select new VentaMeseroViewModel()
            {
                MeseroId = meseroFacturacion.MeseroId,
                Nombres = meseroFacturacion.Nombres,
                Apellidos = meseroFacturacion.Apellidos,
                SumaVentas = meseroFacturacion.Facturas.Sum(x => x.DetalleXfacturas.Sum(x => x.Valor)), 

            }).ToList();

            ViewData["starDate"] = starDate.ToString("yyyy-MM-ddTHH:mm");
            ViewData["endDate"] = endDate.ToString("yyyy-MM-ddTHH:mm");

            return View(MeserosVentas);
        }

        // GET: Meseros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Meseros == null)
            {
                return NotFound();
            }

            var mesero = await _context.Meseros
                .FirstOrDefaultAsync(m => m.MeseroId == id);
            if (mesero == null)
            {
                return NotFound();
            }

            return View(mesero);
        }

        // GET: Meseros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meseros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeseroId,Nombres,Apellidos,Edad,Antiguedad")] Mesero mesero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mesero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mesero);
        }

        // GET: Meseros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Meseros == null)
            {
                return NotFound();
            }

            var mesero = await _context.Meseros.FindAsync(id);
            if (mesero == null)
            {
                return NotFound();
            }
            return View(mesero);
        }

        // POST: Meseros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeseroId,Nombres,Apellidos,Edad,Antiguedad")] Mesero mesero)
        {
            if (id != mesero.MeseroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mesero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeseroExists(mesero.MeseroId))
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
            return View(mesero);
        }

        // GET: Meseros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Meseros == null)
            {
                return NotFound();
            }

            var mesero = await _context.Meseros
                .FirstOrDefaultAsync(m => m.MeseroId == id);
            if (mesero == null)
            {
                return NotFound();
            }

            return View(mesero);
        }

        // POST: Meseros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Meseros == null)
            {
                return Problem("Entity set 'BDRestauranteContext.Meseros'  is null.");
            }
            var mesero = await _context.Meseros.FindAsync(id);
            if (mesero != null)
            {
                _context.Meseros.Remove(mesero);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeseroExists(int id)
        {
          return (_context.Meseros?.Any(e => e.MeseroId == id)).GetValueOrDefault();
        }
    }
}
