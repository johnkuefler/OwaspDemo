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
    public class BrokenAuthAfterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrokenAuthAfterController(
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
                string authToken = Guid.NewGuid().ToString();
                user.AuthToken = authToken;
                _context.SaveChanges();

                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(20);
                option.HttpOnly = true;
                option.Secure = true;
                option.SameSite = SameSiteMode.Strict;
                Response.Cookies.Append("AuthCookie", user.Email + "|" + authToken, option);

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
            string authCookie = Request.Cookies["AuthCookie"];


            if (string.IsNullOrEmpty(authCookie))
            {
                return View("Index");
            }

            string email = authCookie.Split('|')[0];
            string authToken = authCookie.Split('|')[1];

            var user = _context.Logins.Where(x => x.Email == email && x.AuthToken == authToken).FirstOrDefault();
            if (user != null)
            {
                return View("AuthenticatedPage", user.Email);
            }
            else
            {
                Response.Cookies.Delete("AuthCookie");
                return View("Index");
            }
        }

        public async Task<IActionResult> Logout()
        {
            string authCookie = Request.Cookies["AuthCookie"];


            if (string.IsNullOrEmpty(authCookie))
            {
                return View("Index");
            }

            string email = authCookie.Split('|')[0];
            string authToken = authCookie.Split('|')[1];

            var user = _context.Logins.Where(x => x.Email == email && x.AuthToken == authToken).FirstOrDefault();
            if (user != null)
            {
                user.AuthToken = null;
                _context.SaveChanges();
            }

            Response.Cookies.Delete("AuthCookie");

            return View();
        }
    }
}