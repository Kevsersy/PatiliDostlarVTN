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
    public class RecentWorksController : Controller
    {
        private readonly PatiDostumContext _context;

        public RecentWorksController(PatiDostumContext context)
        {
            _context = context;
        }

        // GET: Admin/RecentWorks
        public async Task<IActionResult> Index()
        {
            return View(await _context.RecentWork.ToListAsync());
        }

        // GET: Admin/RecentWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recentWork = await _context.RecentWork
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recentWork == null)
            {
                return NotFound();
            }

            return View(recentWork);
        }

        // GET: Admin/RecentWorks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/RecentWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Category,ID")] RecentWork recentWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recentWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recentWork);
        }

        // GET: Admin/RecentWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recentWork = await _context.RecentWork.FindAsync(id);
            if (recentWork == null)
            {
                return NotFound();
            }
            return View(recentWork);
        }

        // POST: Admin/RecentWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Category,ID")] RecentWork recentWork)
        {
            if (id != recentWork.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recentWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecentWorkExists(recentWork.ID))
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
            return View(recentWork);
        }

        // GET: Admin/RecentWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recentWork = await _context.RecentWork
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recentWork == null)
            {
                return NotFound();
            }

            return View(recentWork);
        }

        // POST: Admin/RecentWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recentWork = await _context.RecentWork.FindAsync(id);
            if (recentWork != null)
            {
                _context.RecentWork.Remove(recentWork);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecentWorkExists(int id)
        {
            return _context.RecentWork.Any(e => e.ID == id);
        }
    }
}
