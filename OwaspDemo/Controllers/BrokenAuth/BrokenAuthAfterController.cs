using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OwaspDemo.Models;

namespace OwaspDemo.Controllers.BrokenAuth
{
    public class BrokenAuthAfterController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public BrokenAuthAfterController(
            SignInManager<IdentityUser> signinManager)
        {
            _signInManager = signinManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("AuthenticatedPage");
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View();
            }
        }

        [Authorize]
        public IActionResult AuthenticatedPage()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View();
        }
    }
}