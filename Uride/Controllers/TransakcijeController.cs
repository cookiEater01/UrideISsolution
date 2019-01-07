using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uride.Models;

namespace Uride.Controllers
{
    public class TransakcijeController : Controller
    {
        private readonly DataUrideContext _context;

        public TransakcijeController(DataUrideContext context)
        {
            _context = context;
        }

        // GET: Transakcije
        public async Task<IActionResult> Index()
        {
            var dataUrideContext = _context.Transakcija.Include(t => t.IdvoznjeNavigation);
            return View(await dataUrideContext.ToListAsync());
        }

        // GET: Transakcije/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transakcija = await _context.Transakcija
                .Include(t => t.IdvoznjeNavigation)
                .FirstOrDefaultAsync(m => m.StRacuna == id);
            if (transakcija == null)
            {
                return NotFound();
            }

            return View(transakcija);
        }

        // GET: Transakcije/Create
        public IActionResult Create()
        {
            ViewData["Idvoznje"] = new SelectList(_context.Voznja, "VoznjaId", "VoznjaId");
            return View();
        }

        // POST: Transakcije/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StRacuna,Znesek,Idvoznje")] Transakcija transakcija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transakcija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idvoznje"] = new SelectList(_context.Voznja, "VoznjaId", "VoznjaId", transakcija.Idvoznje);
            return View(transakcija);
        }

        // GET: Transakcije/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transakcija = await _context.Transakcija.FindAsync(id);
            if (transakcija == null)
            {
                return NotFound();
            }
            ViewData["Idvoznje"] = new SelectList(_context.Voznja, "VoznjaId", "VoznjaId", transakcija.Idvoznje);
            return View(transakcija);
        }

        // POST: Transakcije/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StRacuna,Znesek,Idvoznje")] Transakcija transakcija)
        {
            if (id != transakcija.StRacuna)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transakcija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransakcijaExists(transakcija.StRacuna))
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
            ViewData["Idvoznje"] = new SelectList(_context.Voznja, "VoznjaId", "VoznjaId", transakcija.Idvoznje);
            return View(transakcija);
        }

        // GET: Transakcije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transakcija = await _context.Transakcija
                .Include(t => t.IdvoznjeNavigation)
                .FirstOrDefaultAsync(m => m.StRacuna == id);
            if (transakcija == null)
            {
                return NotFound();
            }

            return View(transakcija);
        }

        // POST: Transakcije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transakcija = await _context.Transakcija.FindAsync(id);
            _context.Transakcija.Remove(transakcija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransakcijaExists(int id)
        {
            return _context.Transakcija.Any(e => e.StRacuna == id);
        }
    }
}
