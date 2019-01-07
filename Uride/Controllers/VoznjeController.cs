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
    public class VoznjeController : Controller
    {
        private readonly DataUrideContext _context;

        public VoznjeController(DataUrideContext context)
        {
            _context = context;
        }

        // GET: Voznje
        public async Task<IActionResult> Index()
        {
            var dataUrideContext = _context.Voznja.Include(v => v.Uporabnik).Include(v => v.Voznik);
            return View(await dataUrideContext.ToListAsync());
        }

        // GET: Voznje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznja = await _context.Voznja
                .Include(v => v.Uporabnik)
                .Include(v => v.Voznik)
                .FirstOrDefaultAsync(m => m.VoznjaId == id);
            if (voznja == null)
            {
                return NotFound();
            }

            return View(voznja);
        }

        // GET: Voznje/Create
        public IActionResult Create()
        {
            ViewData["UporabnikId"] = new SelectList(_context.Stranka, "StrankaId", "MobStev");
            ViewData["VoznikId"] = new SelectList(_context.Voznik, "StVozniske", "Ime");
            return View();
        }

        // POST: Voznje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoznjaId,UporabnikId,VoznikId,DolzinaPoti")] Voznja voznja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voznja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UporabnikId"] = new SelectList(_context.Stranka, "StrankaId", "MobStev", voznja.UporabnikId);
            ViewData["VoznikId"] = new SelectList(_context.Voznik, "StVozniske", "Ime", voznja.VoznikId);
            return View(voznja);
        }

        // GET: Voznje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznja = await _context.Voznja.FindAsync(id);
            if (voznja == null)
            {
                return NotFound();
            }
            ViewData["UporabnikId"] = new SelectList(_context.Stranka, "StrankaId", "MobStev", voznja.UporabnikId);
            ViewData["VoznikId"] = new SelectList(_context.Voznik, "StVozniske", "Ime", voznja.VoznikId);
            return View(voznja);
        }

        // POST: Voznje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoznjaId,UporabnikId,VoznikId,DolzinaPoti")] Voznja voznja)
        {
            if (id != voznja.VoznjaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voznja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoznjaExists(voznja.VoznjaId))
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
            ViewData["UporabnikId"] = new SelectList(_context.Stranka, "StrankaId", "MobStev", voznja.UporabnikId);
            ViewData["VoznikId"] = new SelectList(_context.Voznik, "StVozniske", "Ime", voznja.VoznikId);
            return View(voznja);
        }

        // GET: Voznje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznja = await _context.Voznja
                .Include(v => v.Uporabnik)
                .Include(v => v.Voznik)
                .FirstOrDefaultAsync(m => m.VoznjaId == id);
            if (voznja == null)
            {
                return NotFound();
            }

            return View(voznja);
        }

        // POST: Voznje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voznja = await _context.Voznja.FindAsync(id);
            _context.Voznja.Remove(voznja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoznjaExists(int id)
        {
            return _context.Voznja.Any(e => e.VoznjaId == id);
        }
    }
}
