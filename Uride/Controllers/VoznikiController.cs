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
    public class VoznikiController : Controller
    {
        private readonly DataUrideContext _context;

        public VoznikiController(DataUrideContext context)
        {
            _context = context;
        }

        // GET: Vozniki
        public async Task<IActionResult> Index()
        {
            var dataUrideContext = _context.Voznik.Include(v => v.Avto);
            return View(await dataUrideContext.ToListAsync());
        }

        // GET: Vozniki/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznik = await _context.Voznik
                .Include(v => v.Avto)
                .FirstOrDefaultAsync(m => m.StVozniske == id);
            if (voznik == null)
            {
                return NotFound();
            }

            return View(voznik);
        }

        // GET: Vozniki/Create
        public IActionResult Create()
        {
            ViewData["AvtoId"] = new SelectList(_context.Vozilo, "AvtoId", "Model");
            return View();
        }

        // POST: Vozniki/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StVozniske,Ime,Priimek,Naslov,AvtoId,MobStev,Upokojen,UpImeId")] Voznik voznik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voznik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AvtoId"] = new SelectList(_context.Vozilo, "AvtoId", "Model", voznik.AvtoId);
            return View(voznik);
        }

        // GET: Vozniki/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznik = await _context.Voznik.FindAsync(id);
            if (voznik == null)
            {
                return NotFound();
            }
            ViewData["AvtoId"] = new SelectList(_context.Vozilo, "AvtoId", "Model", voznik.AvtoId);
            return View(voznik);
        }

        // POST: Vozniki/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StVozniske,Ime,Priimek,Naslov,AvtoId,MobStev,Upokojen,UpImeId")] Voznik voznik)
        {
            if (id != voznik.StVozniske)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voznik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoznikExists(voznik.StVozniske))
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
            ViewData["AvtoId"] = new SelectList(_context.Vozilo, "AvtoId", "Model", voznik.AvtoId);
            return View(voznik);
        }

        // GET: Vozniki/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznik = await _context.Voznik
                .Include(v => v.Avto)
                .FirstOrDefaultAsync(m => m.StVozniske == id);
            if (voznik == null)
            {
                return NotFound();
            }

            return View(voznik);
        }

        // POST: Vozniki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voznik = await _context.Voznik.FindAsync(id);
            _context.Voznik.Remove(voznik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoznikExists(int id)
        {
            return _context.Voznik.Any(e => e.StVozniske == id);
        }
    }
}
