using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace OwaspDemo.Controllers
{
    public class InjectionBeforeController : Controller
    {
        IConfiguration _configuration;

        public InjectionBeforeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string query = "select * from Logins where Email='" + email + "'and Password='" + password + "' ";

            var adapter = new SqlDataAdapter(query, connection);

            var datatable = new DataTable();
            adapter.Fill(datatable);

            if (datatable.Rows.Count >= 1)
            {
                return View("LoggedIn", datatable.Rows[0]["Email"]);
            }
            else
            {
                ViewBag.Error = "Invalid login";
                return View();
            }
        }
    }
}