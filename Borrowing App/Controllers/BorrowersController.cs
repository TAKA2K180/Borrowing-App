﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Borrowing_App.Data;
using Borrowing_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Borrowing_App.Controllers
{
    [Authorize]
    public class BorrowersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public BorrowersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize]
        // GET: Borrowers
        public async Task<IActionResult> Index()
        {
            if (HttpContext.User.IsInRole("Basic") == true)
            {
                var users = await _userManager.Users.ToListAsync();
                
                var borrowers = new Borrower();
                foreach (ApplicationUser user in users)
                {
                    if (user.Email.ToString() == HttpContext.User.Identity.Name)
                    {
                        var id = user.EmpiID;
                        var result = _context.Borrower.Where(u => u.EmpiId == id);
                        return View(result);
                    }
                    else
                    {
                        string error1 = "Error :(";
                        return View(error1);
                    }
                }
                string error = "Error :(";
                return View(error);
            }
            else if (HttpContext.User.IsInRole("SuperAdmin") == true)
            {
                return _context.Borrower != null ?
                          View(await _context.Borrower.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Borrower'  is null.");
            }
            else
            {
                string error = "Error :(";
                return View(error);
            }
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
        public async Task<IActionResult> Create(string userName)
        {
            //var userEmail = _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var users = await _userManager.Users.ToListAsync();
            var borrowers = new Borrower();
            foreach (ApplicationUser user in users)
            {
                if (user.Email.ToString() == HttpContext.User.Identity.Name)
                {
                    borrowers.EmpiId = user.EmpiID;
                    borrowers.Name = user.FirstName + " " + user.LastName;
                    borrowers.Department = user.Department;
                    borrowers.BorrowDate = DateTime.Now;
                }
            }
            ViewData["Borrower"] = borrowers;
            return View(borrowers);
        }

        // POST: Borrowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,EmpiId,Name,Department,Item,ItemDescription,Reason,BorrowDate,Remarks")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                borrower.id = Guid.NewGuid();
                _context.Add(borrower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(borrower);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("id,EmpiId,Name,Department,Item,ItemDescription,Reason,BorrowDate,Remarks, Status")] Borrower borrower)
        {
            if (id != borrower.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    borrower.Status = "Pending";
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
