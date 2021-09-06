using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwaspDemo.Models;

namespace OwaspDemo.Controllers.InsufficientLogging
{
    public class AuditLoggingController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<SensitiveDataExposureBeforeController> _logger;

        public AuditLoggingController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signinManager,
            ILogger<SensitiveDataExposureBeforeController> logger)
        {
            _userManager = userManager;
            _signInManager = signinManager;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);

            if (result.Succeeded)
            {
                return View("LoginSuccess", loginViewModel.Email);
            }
            else
            {
                ViewBag.Error = "Invalid username or password";

                _logger.LogWarning($"Login failure audit for {loginViewModel.Email}");

                return View();
            }
        }
    }
}