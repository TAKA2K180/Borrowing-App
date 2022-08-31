﻿using Borrowing_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Borrowing_App.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var Borrowers = new List<Borrower>();
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new Borrower();
                thisViewModel.EmpiId = user.EmpiID;
                thisViewModel.Name = user.FirstName + " " + user.LastName;
                thisViewModel.Department = user.Department;
                Borrowers.Add(thisViewModel);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}