using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OwaspDemo.Models;

namespace OwaspDemo.Controllers
{
    public class SensitiveDataExposureAfterController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<SensitiveDataExposureBeforeController> _logger;

        public SensitiveDataExposureAfterController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signinManager,
            ILogger<SensitiveDataExposureBeforeController> logger)
        {
            _userManager = userManager;
            _signInManager = signinManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel registerViewModel)
        {
            IdentityUser user = new IdentityUser
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                _logger.LogError($"Error during registration for user {JsonConvert.SerializeObject(registerViewModel)} - {string.Join(", ", result.Errors.Select(x => x.Description))}");

                ViewBag.Error = string.Join(", ", result.Errors.Select(x => x.Description));

                return View();
            }

            _logger.LogInformation($"User registered successfully! Info: {JsonConvert.SerializeObject(registerViewModel)}");

            return View("RegistrationSuccess", registerViewModel.Email);
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
                _logger.LogInformation($"Login success audit. {JsonConvert.SerializeObject(loginViewModel)}");

                return View("LoginSuccess", loginViewModel.Email);
            }
            else
            {
                ViewBag.Error = "Invalid username or password";

                _logger.LogWarning($"Login failure audit. {JsonConvert.SerializeObject(loginViewModel)}");

                return View();
            }
        }
    }
}