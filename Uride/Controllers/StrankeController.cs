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
    public class StrankeController : Controller
    {
        private readonly DataUrideContext _context;

        public StrankeController(DataUrideContext context)
        {
            _context = context;
        }

        // GET: Stranke
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stranka.ToListAsync());
        }

        // GET: Stranke/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stranka = await _context.Stranka
                .FirstOrDefaultAsync(m => m.StrankaId == id);
            if (stranka == null)
            {
                return NotFound();
            }

            return View(stranka);
        }

        // GET: Stranke/Create
        public IActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                ViewData["UpImeIda"] = "";
            } else
            {
                ViewData["UpImeIda"] = User.getUserId();
            }
            return View();
        }

        // POST: Stranke/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StrankaId,Ime,Priimek,Naslov,MobStev,UpImeId")] Stranka stranka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stranka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stranka);
        }

        // GET: Stranke/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stranka = await _context.Stranka.FindAsync(id);
            if (stranka == null)
            {
                return NotFound();
            }
            return View(stranka);
        }

        // POST: Stranke/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StrankaId,Ime,Priimek,Naslov,MobStev,UpImeId")] Stranka stranka)
        {
            if (id != stranka.StrankaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stranka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrankaExists(stranka.StrankaId))
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
            return View(stranka);
        }

        // GET: Stranke/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stranka = await _context.Stranka
                .FirstOrDefaultAsync(m => m.StrankaId == id);
            if (stranka == null)
            {
                return NotFound();
            }

            return View(stranka);
        }

        // POST: Stranke/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stranka = await _context.Stranka.FindAsync(id);
            _context.Stranka.Remove(stranka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StrankaExists(int id)
        {
            return _context.Stranka.Any(e => e.StrankaId == id);
        }
    }
}
