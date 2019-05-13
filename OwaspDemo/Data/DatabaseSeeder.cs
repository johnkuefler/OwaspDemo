using Microsoft.EntityFrameworkCore;
using OwaspDemo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwaspDemo.Data
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;

        public DatabaseSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            ClearData();
            SeedLogins();
        }

        public void ClearData()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM [Logins]");

            _context.Database.ExecuteSqlCommand("DELETE FROM [AspNetUsers]");

            _context.Database.ExecuteSqlCommand("DELETE FROM [AuditLogs]");
        }

        public void SeedLogins()
        {
            Login login1 = new Login
            {
                Email = "bobj@gmail.com",
                Name = "Bob Jones",
                Password = "password1"
            };

            Login login2 = new Login
            {
                Email = "robk@gmail.com",
                Name = "Rob Knox",
                Password = "password2"
            };

            Login login3 = new Login
            {
                Email = "jessicab@gmail.com",
                Name = "Jessica Brown",
                Password = "password3"
            };

            Login login4 = new Login
            {
                Email = "dawnr@gmail.com",
                Name = "Dawn Randell",
                Password = "password4"
            };

            _context.Logins.Add(login1);
            _context.Logins.Add(login2);
            _context.Logins.Add(login3);
            _context.Logins.Add(login4);

            _context.SaveChanges();
        }
    }
}
