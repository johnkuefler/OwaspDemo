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
            xml = xml.Trim();
            string output = "";

            XmlReaderSettings rs = new XmlReaderSettings();
            rs.DtdProcessing = DtdProcessing.Parse;
            rs.XmlResolver = new XmlUrlResolver();
            rs.MaxCharactersFromEntities = 0;

            XmlReader myReader = XmlReader.Create(new StringReader(xml), rs);

            while (myReader.Read())
            {
                output += myReader.Value;
            }

            return View("Attack", output);
        }
    }
}