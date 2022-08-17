using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Borrowing_App.Data;
using Borrowing_App.Models;
using Microsoft.AspNetCore.Authorization;

namespace Borrowing_App.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BorrowersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Borrowers
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
              return _context.Borrower != null ? 
                          View(await _context.Borrower.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Borrower'  is null.");
        }

        // GET: Borrowers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Borrower == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrower
                .FirstOrDefaultAsync(m => m.id == id);
            if (borrower == null)
            {
                return NotFound();
            }

            return View(borrower);
        }

        // GET: Borrowers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Borrowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,EmpiId,Name,Item,ItemDescription,BorrowDate,Remarks,Department,Reason")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                borrower.id = Guid.NewGuid();
                borrower.BorrowDate = DateTime.Now;
                _context.Add(borrower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirm));
            }
            return View(Confirm);
        }
        public async Task<IActionResult> Confirm(int empiid)
        {
            if (empiid == null || _context.Borrower == null)
            {
                return NotFound();
            }

            return View();
        }

        // GET: Borrowers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Borrower == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrower.FindAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,EmpiId,Name,Item,ItemDescription,BorrowDate,Remarks")] Borrower borrower)
        {
            if (id != borrower.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrower);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowerExists(borrower.id))
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
            return View(borrower);
        }

        // GET: Borrowers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Borrower == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrower
                .FirstOrDefaultAsync(m => m.id == id);
            if (borrower == null)
            {
                return NotFound();
            }

            return View(borrower);
        }

        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Borrower == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Borrower'  is null.");
            }
            var borrower = await _context.Borrower.FindAsync(id);
            if (borrower != null)
            {
                _context.Borrower.Remove(borrower);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowerExists(Guid id)
        {
          return (_context.Borrower?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
