using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OwaspDemo.Data;

namespace OwaspDemo.Controllers.BrokenAccessControl
{
    public class BrokenAccessControlAfterController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BrokenAccessControlAfterController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("~/v2/users/me")]
        public IActionResult Profile()
        {
            var user = _context.Logins.First();

            return Ok(new
            {
                user.Name,
                user.Email
            });
        }

        [HttpGet("~/v2/users/")]
        public IActionResult Users()
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            var users = _context.Logins.ToList();

            return Ok(users);
        }
    }
}