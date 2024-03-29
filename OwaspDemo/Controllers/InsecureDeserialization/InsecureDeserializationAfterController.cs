﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OwaspDemo.Models;

namespace OwaspDemo.Controllers
{
    public class InsecureDeserializationAfterController : Controller
    {
        readonly IConfiguration _configuration;

        public InsecureDeserializationAfterController(IConfiguration configuration)
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
        [HttpPost("~/api/users/after")]
        public ActionResult Index([FromBody]LoginQueryModel param)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string query = "select * from Logins where Email=@email";

            SqlCommand command = new SqlCommand(query, connection);

            // prevent injection
            var emailParam = new SqlParameter("email", SqlDbType.VarChar);
            emailParam.Value = param.Email;
            command.Parameters.Add(emailParam);

            var adapter = new SqlDataAdapter(command);

            var datatable = new DataTable();
            adapter.Fill(datatable);

            List<object> output = new List<object>();

            if (datatable.Rows.Count == 0)
            {
                return NotFound();
            }

            for (int i = 0; i < datatable.Rows.Count; i++)
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