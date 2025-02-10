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
    public class AppRolesController : Controller
    {
        private readonly PatiDostumContext _context;

        public AppRolesController(PatiDostumContext context)
        {
            _context = context;
        }

        // GET: Admin/AppRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: Admin/AppRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appRole == null)
            {
                return NotFound();
            }

            return View(appRole);
        }

        // GET: Admin/AppRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AppRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedAt,Id,Name,NormalizedName,ConcurrencyStamp")] AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appRole);
        }

        // GET: Admin/AppRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _context.Roles.FindAsync(id);
            if (appRole == null)
            {
                return NotFound();
            }
            return View(appRole);
        }

        // POST: Admin/AppRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CreatedAt,Id,Name,NormalizedName,ConcurrencyStamp")] AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppRoleExists(appRole.Id))
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
            return View(appRole);
        }

        // GET: Admin/AppRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appRole == null)
            {
                return NotFound();
            }

            return View(appRole);
        }

        // POST: Admin/AppRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appRole = await _context.Roles.FindAsync(id);
            if (appRole != null)
            {
                _context.Roles.Remove(appRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppRoleExists(string id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
