﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OwaspDemo.Controllers
{
    public class ComponentVulnerabilitiesBeforeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}