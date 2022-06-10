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
    public class SupervisoresController : Controller
    {
        private readonly BDRestauranteContext _context;

        public SupervisoresController(BDRestauranteContext context)
        {
            _context = context;
        }

        // GET: Supervisores
        public async Task<IActionResult> Index()
        {
              return _context.Supervisors != null ? 
                          View(await _context.Supervisors.ToListAsync()) :
                          Problem("Entity set 'BDRestauranteContext.Supervisors'  is null.");
        }

        // GET: Supervisores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Supervisors == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisors
                .FirstOrDefaultAsync(m => m.SupervirsorId == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        // GET: Supervisores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supervisores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupervirsorId,Nombres,Apellidos,Edad,Antiguedad")] Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supervisor);
        }

        // GET: Supervisores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Supervisors == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisors.FindAsync(id);
            if (supervisor == null)
            {
                return NotFound();
            }
            return View(supervisor);
        }

        // POST: Supervisores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupervirsorId,Nombres,Apellidos,Edad,Antiguedad")] Supervisor supervisor)
        {
            if (id != supervisor.SupervirsorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supervisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupervisorExists(supervisor.SupervirsorId))
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
            return View(supervisor);
        }

        // GET: Supervisores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Supervisors == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisors
                .FirstOrDefaultAsync(m => m.SupervirsorId == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        // POST: Supervisores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Supervisors == null)
            {
                return Problem("Entity set 'BDRestauranteContext.Supervisors'  is null.");
            }
            var supervisor = await _context.Supervisors.FindAsync(id);
            if (supervisor != null)
            {
                _context.Supervisors.Remove(supervisor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupervisorExists(int id)
        {
          return (_context.Supervisors?.Any(e => e.SupervirsorId == id)).GetValueOrDefault();
        }
    }
}
