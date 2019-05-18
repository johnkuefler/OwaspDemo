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

namespace OwaspDemo.Controllers.XMLExternalEntities
{
    public class XXEAfterController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public XXEAfterController(IHostingEnvironment environment)
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

            try
            {
                using (var stream = new MemoryStream(Encoding.Default.GetBytes(xml)))
                {
                    var settings = new XmlReaderSettings();
                    settings.DtdProcessing = DtdProcessing.Parse;
                    settings.MaxCharactersFromEntities = 2048;

                    using (var reader = XmlReader.Create(stream, settings))
                    {
                        doc.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Attack", "A potentially dangerous XML file was rejected");
            }

            return View("Attack", "XML file parsed successfully");
        }
    }
}