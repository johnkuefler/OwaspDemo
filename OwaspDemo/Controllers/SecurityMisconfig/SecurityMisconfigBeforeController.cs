using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OwaspDemo.Data;

namespace OwaspDemo.Controllers
{
    public class SecurityMisconfigBeforeController : Controller
    {
        ApplicationDbContext _context;

        public SecurityMisconfigBeforeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TriggerError()
        {
            _context.Logins.Add(new Data.Models.Login
            {
                Email = "test@waylongerthanthemaxallowed.com",
                Name = "Bob Test",
                Password = "password"
            });

            _context.SaveChanges();

            return View("Index");
        }
    }
}