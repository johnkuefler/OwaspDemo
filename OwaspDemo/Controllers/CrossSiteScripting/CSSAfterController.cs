using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OwaspDemo.Data;

namespace OwaspDemo.Controllers
{
    public class CSSAfterController : Controller
    {
        public ApplicationDbContext _context;

        public CSSAfterController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name, string email, string password)
        {
            if (!Utilities.IsValidEmail(email))
            {
                ViewBag.Error = "Invalid email";
                return View();
            }

            _context.Logins.Add(new Data.Models.Login
            {
                Name = name,
                Email = email,
                Password = password
            });
            _context.SaveChanges();


            return View("RegistrationSuccess");
        }

        public IActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index2(string name, string email, string password)
        {
            if (!Utilities.IsValidEmail(email))
            {
                ViewBag.Error = "Invalid email";
                return View();
            }

            if (!Utilities.IsValidText(name))
            {
                ViewBag.Error = "Invalid name";
                return View();
            }

            _context.Logins.Add(new Data.Models.Login
            {
                Name = name,
                Email = email,
                Password = password
            });
            _context.SaveChanges();


            return View("RegistrationSuccess");
        }


        public IActionResult ListLogins()
        {
            var logins = _context.Logins.ToList();

            return View(logins);
        }
    }
}