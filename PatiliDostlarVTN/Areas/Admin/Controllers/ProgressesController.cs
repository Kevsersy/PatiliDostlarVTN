using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProgressesController : Controller
    {
        private readonly PatiDostumContext _context;

        public ProgressesController(PatiDostumContext context)
        {
            _context = context;
        }

        // GET: Admin/Progresses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Progresses.ToListAsync());
        }

        // GET: Admin/Progresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _context.Progresses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (progress == null)
            {
                return NotFound();
            }

            return View(progress);
        }

        // GET: Admin/Progresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Progresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Metric,Percentage,ID")] Progress progress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(progress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(progress);
        }

        // GET: Admin/Progresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _context.Progresses.FindAsync(id);
            if (progress == null)
            {
                return NotFound();
            }
            return View(progress);
        }

        // POST: Admin/Progresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Metric,Percentage,ID")] Progress progress)
        {
            if (id != progress.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(progress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgressExists(progress.ID))
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
            return View(progress);
        }

        // GET: Admin/Progresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _context.Progresses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (progress == null)
            {
                return NotFound();
            }

            return View(progress);
        }

        // POST: Admin/Progresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var progress = await _context.Progresses.FindAsync(id);
            if (progress != null)
            {
                _context.Progresses.Remove(progress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgressExists(int id)
        {
            return _context.Progresses.Any(e => e.ID == id);
        }
    }
}
