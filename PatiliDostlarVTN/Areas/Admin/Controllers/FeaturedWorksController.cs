using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class FeaturedWorksController : Controller
    {
        private readonly PatiDostumContext _context;

        public FeaturedWorksController(PatiDostumContext context)
        {
            _context = context;
        }

        // GET: Admin/FeaturedWorks
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeaturedWork.ToListAsync());
        }

        // GET: Admin/FeaturedWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featuredWork = await _context.FeaturedWork
                .FirstOrDefaultAsync(m => m.ID == id);
            if (featuredWork == null)
            {
                return NotFound();
            }

            return View(featuredWork);
        }

        // GET: Admin/FeaturedWorks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/FeaturedWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Subtitle,Description,Benefits,Footer,ImageUrls,ID")] FeaturedWork featuredWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(featuredWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(featuredWork);
        }

        // GET: Admin/FeaturedWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featuredWork = await _context.FeaturedWork.FindAsync(id);
            if (featuredWork == null)
            {
                return NotFound();
            }
            return View(featuredWork);
        }

        // POST: Admin/FeaturedWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Subtitle,Description,Benefits,Footer,ImageUrls,ID")] FeaturedWork featuredWork)
        {
            if (id != featuredWork.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(featuredWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeaturedWorkExists(featuredWork.ID))
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
            return View(featuredWork);
        }

        // GET: Admin/FeaturedWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featuredWork = await _context.FeaturedWork
                .FirstOrDefaultAsync(m => m.ID == id);
            if (featuredWork == null)
            {
                return NotFound();
            }

            return View(featuredWork);
        }

        // POST: Admin/FeaturedWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var featuredWork = await _context.FeaturedWork.FindAsync(id);
            if (featuredWork != null)
            {
                _context.FeaturedWork.Remove(featuredWork);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeaturedWorkExists(int id)
        {
            return _context.FeaturedWork.Any(e => e.ID == id);
        }
    }
}
