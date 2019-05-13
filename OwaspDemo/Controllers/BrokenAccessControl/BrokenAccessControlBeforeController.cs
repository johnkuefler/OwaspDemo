using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OwaspDemo.Data;

namespace OwaspDemo.Controllers.BrokenAccessControl
{
    public class BrokenAccessControlBeforeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BrokenAccessControlBeforeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("~/users/me")]
        public IActionResult Profile()
        {
            var user = _context.Logins.First();

            return Ok(new
            {
                user.Name,
                user.Email
            });
        }

        [HttpGet("~/users")]
        public IActionResult Users()
        {
            var users = _context.Logins.ToList();

            return Ok(users);
        }
    }
}