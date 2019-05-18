using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace OwaspDemo.Controllers.XMLExternalEntities
{
    public class XXEBeforeController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public XXEBeforeController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Attack(string xml)
        {
            var doc = new XmlDocument();

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(xml)))
            {
                var settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                settings.MaxCharactersFromEntities = 0;

                using (var reader = XmlReader.Create(stream, settings))
                {
                   doc.Load(reader);
                }
            }

            return View("Attack");
        }
    }
}