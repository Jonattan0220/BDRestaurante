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
    public class ClientesController : Controller
    {
        private readonly BDRestauranteContext _context;

        public ClientesController(BDRestauranteContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
              return _context.Clientes != null ? 
                          View(await _context.Clientes.ToListAsync()) :
                          Problem("Entity set 'BDRestauranteContext.Clientes'  is null.");
        }

        // GET: Ventas 
        public async Task<IActionResult> Compras()
        {
            int valor = 0;
            DateTime starDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

            List<Cliente> clientes = await _context.Clientes
                                                                                 .ToListAsync();

            List<Factura> facturas = await _context.Facturas
                                                                                    .Where(x => x.FechaHora >= starDate && x.FechaHora <= endDate)
                                                                                    .ToListAsync();

            List<DetalleXfactura> detallesXFacturasEsp = await _context.DetalleXfacturas
                                                                                              .Where(detalle => detalle.Valor >= valor)
                                                                                              .ToListAsync();

            var ClientesCompras = (from cliente in clientes
                                   join factura in facturas on cliente.ClienteId equals factura.ClienteId
                                   join detalle in detallesXFacturasEsp on factura.FacturaId equals detalle.FacturaId
                                   group new { cliente, factura, detalle } by new { cliente.Identificacion, cliente.Nombres, cliente.Apellidos, factura.FacturaId } into g
                                   select new CompraClienteViewModel()
                                   {
                                       Identificacion = g.Key.Identificacion,
                                       Nombres = g.Key.Nombres,
                                       Apellidos = g.Key.Apellidos,
                                       FacturaId = g.Key.FacturaId,
                                       SumaValorFactura = g.Sum(x => x.detalle.Valor)
                                   }).ToList();

            ViewData["valor"] = valor;
            ViewData["starDate"] = starDate.ToString("yyyy-MM-ddTHH:mm");
            ViewData["endDate"] = endDate.ToString("yyyy-MM-ddTHH:mm");
            return View(ClientesCompras);
        }

        // POST: Meseros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Compras(int valor, DateTime starDate, DateTime endDate)
        {
            List<Cliente> clientes = await _context.Clientes
                                                                                 .ToListAsync();

            List<Factura> facturas = await _context.Facturas
                                                                                    .Where(x => x.FechaHora >= starDate && x.FechaHora <= endDate)
                                                                                    .ToListAsync();

            List<DetalleXfactura> detallesXFacturasEsp = await _context.DetalleXfacturas
                                                                                              .Where(detalle => detalle.Valor >= valor)
                                                                                              .ToListAsync();

            var ClientesCompras = (from cliente in clientes
                                                    join factura in facturas on cliente.ClienteId equals factura.ClienteId
                                                    join detalle in detallesXFacturasEsp on factura.FacturaId equals detalle.FacturaId
                                                    group new { cliente, factura, detalle} by new {cliente.Identificacion, cliente.Nombres, cliente.Apellidos, factura.FacturaId} into g
                                                    select new CompraClienteViewModel()
                                                    {
                                                        Identificacion = g.Key.Identificacion,
                                                        Nombres = g.Key.Nombres,
                                                        Apellidos = g.Key.Apellidos,
                                                        FacturaId = g.Key.FacturaId,
                                                        SumaValorFactura = g.Sum(x=>x.detalle.Valor)
                                                    }).ToList();

            ViewData["valor"] = valor;
            ViewData["starDate"] = starDate.ToString("yyyy-MM-ddTHH:mm");
            ViewData["endDate"] = endDate.ToString("yyyy-MM-ddTHH:mm");

            return View(ClientesCompras);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Identificacion,Nombres,Apellidos,Direccion,Telefono")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Identificacion,Nombres,Apellidos,Direccion,Telefono")] Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteId))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'BDRestauranteContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.ClienteId == id)).GetValueOrDefault();
        }
    }
}
