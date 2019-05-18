using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OwaspDemo.Data.Models;
using OwaspDemo.Models;

namespace OwaspDemo.Controllers
{
    public class InsecureDeserializationBeforeController : Controller
    {
        IConfiguration _configuration;

        public InsecureDeserializationBeforeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <remarks>
        /// Try this
        /// {
        ///      "email": " 'or'1'='1 "
        /// }
        /// </remarks>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("~/api/users/before")]
        public ActionResult Index([FromBody]LoginQueryModel param)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string query = "select * from Logins where Email='" + param.Email + "' ";

            var adapter = new SqlDataAdapter(query, connection);

            var datatable = new DataTable();
            adapter.Fill(datatable);

            List<object> output = new List<object>();

            if (datatable.Rows.Count ==0)
            {
                return NotFound();
            }

            for(int i=0; i<datatable.Rows.Count; i++)
            {
                output.Add(new
                {
                    Email = datatable.Rows[i]["Email"],
                    Name = datatable.Rows[i]["Name"]
                });
            }

            return Ok(output);
        }
    }
}