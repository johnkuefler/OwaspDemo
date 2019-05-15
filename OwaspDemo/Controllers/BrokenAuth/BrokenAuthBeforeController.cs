using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OwaspDemo.Data;
using OwaspDemo.Models;

namespace OwaspDemo.Controllers.BrokenAuth
{
    public class BrokenAuthBeforeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrokenAuthBeforeController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var user = _context.Logins.Where(x => x.Email == loginViewModel.Email && x.Password == loginViewModel.Password).FirstOrDefault();

            if (user != null)
            {
                HttpContext.Session.SetInt32("User", user.Id);
                return RedirectToAction("AuthenticatedPage");
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View();
            }
        }

        public IActionResult AuthenticatedPage()
        {
            int? userId = HttpContext.Session.GetInt32("User");

            if (userId == null)
            {
                return View("Index");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("User");

            return View();
        }
    }
}