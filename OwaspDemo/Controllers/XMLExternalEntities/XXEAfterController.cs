using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.Extensions.Logging;

namespace OwaspDemo.Controllers.XMLExternalEntities
{
    public class XXEAfterController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly ILogger<XXEAfterController> _logger;

        public XXEAfterController(IHostingEnvironment environment, ILogger<XXEAfterController> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Attack(string xml)
        {
            xml = xml.Trim();
            string output = "";

            XmlTextReader reader = new XmlTextReader(new StringReader(xml));
            reader.DtdProcessing = DtdProcessing.Ignore;
            while (reader.Read())
            {
                output += reader.Value;
            }

            return View("Attack", output);
        }
    }
}