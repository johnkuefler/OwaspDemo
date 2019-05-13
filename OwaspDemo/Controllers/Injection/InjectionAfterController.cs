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
    public class InjectionAfterController : Controller
    {
        IConfiguration _configuration;

        public InjectionAfterController(IConfiguration configuration)
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
            string query = "select * from Logins where Email=@email and Password=@password";
            SqlCommand command = new SqlCommand(query, connection);

            // prevent injection
            var emailParam = new SqlParameter("email", SqlDbType.VarChar);
            emailParam.Value = email;
            command.Parameters.Add(emailParam);

            var passwordParam = new SqlParameter("password", SqlDbType.VarChar);
            passwordParam.Value = password;
            command.Parameters.Add(passwordParam);

            var adapter = new SqlDataAdapter(command);

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