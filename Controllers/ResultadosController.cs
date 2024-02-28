using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApiPryBullyng.Models.DB;

namespace ApiPryBullyng.Controllers
{
    public class ResultadosController : Controller
    {
        private readonly AppbullyingContext _context;

        public ResultadosController(AppbullyingContext context)
        {
            _context = context;
        }

        // GET: Resultados
        public async Task<IActionResult> Index()
        {
            var appbullyingContext = _context.Resultados.Include(r => r.IdTestNavigation).Include(r => r.IdUsuarioNavigation);
            return View(await appbullyingContext.ToListAsync());
        }

        // GET: Resultados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados
                .Include(r => r.IdTestNavigation)
                .Include(r => r.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdResultado == id);
            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);
        }

        // GET: Resultados/Create
        public IActionResult Create()
        {
            ViewData["IdTest"] = new SelectList(_context.Tests, "IdTest", "IdTest");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Resultados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdResultado,IdTest,IdUsuario,VecesSi,VecesNo")] Resultado resultado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTest"] = new SelectList(_context.Tests, "IdTest", "IdTest", resultado.IdTest);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", resultado.IdUsuario);
            return View(resultado);
        }

        // GET: Resultados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados.FindAsync(id);
            if (resultado == null)
            {
                return NotFound();
            }
            ViewData["IdTest"] = new SelectList(_context.Tests, "IdTest", "IdTest", resultado.IdTest);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", resultado.IdUsuario);
            return View(resultado);
        }

        // POST: Resultados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdResultado,IdTest,IdUsuario,VecesSi,VecesNo")] Resultado resultado)
        {
            if (id != resultado.IdResultado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultadoExists(resultado.IdResultado))
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
            ViewData["IdTest"] = new SelectList(_context.Tests, "IdTest", "IdTest", resultado.IdTest);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", resultado.IdUsuario);
            return View(resultado);
        }

        // GET: Resultados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados
                .Include(r => r.IdTestNavigation)
                .Include(r => r.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdResultado == id);
            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);
        }

        // POST: Resultados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _context.Resultados.FindAsync(id);
            if (resultado != null)
            {
                _context.Resultados.Remove(resultado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultadoExists(int id)
        {
            return _context.Resultados.Any(e => e.IdResultado == id);
        }
    }
}
