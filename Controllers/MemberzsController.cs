using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerZ2._0.Data;
using CustomerZ2._0.Models;
using Microsoft.AspNetCore.Authorization;

namespace CustomerZ2._0.Controllers
{
    public class MemberzsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberzsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Memberzs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Memberz.ToListAsync());
        }

        // GET: Memberzs/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // Post: Memberzs/ShowSearchForm
        public async Task<IActionResult> ShowSearchResult(String Search)
        {
            return View("Index", await _context.Memberz.Where(j => j.Name.Contains(Search)).ToListAsync());
        }

        // GET: Memberzs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberz = await _context.Memberz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberz == null)
            {
                return NotFound();
            }

            return View(memberz);
        }

        // GET: Memberzs/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Memberzs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Contact,Question,Answer")] Memberz memberz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberz);
        }

        // GET: Memberzs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberz = await _context.Memberz.FindAsync(id);
            if (memberz == null)
            {
                return NotFound();
            }
            return View(memberz);
        }

        // POST: Memberzs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Contact,Question,Answer")] Memberz memberz)
        {
            if (id != memberz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberzExists(memberz.Id))
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
            return View(memberz);
        }

        // GET: Memberzs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberz = await _context.Memberz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberz == null)
            {
                return NotFound();
            }

            return View(memberz);
        }

        // POST: Memberzs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberz = await _context.Memberz.FindAsync(id);
            _context.Memberz.Remove(memberz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberzExists(int id)
        {
            return _context.Memberz.Any(e => e.Id == id);
        }
    }
}
