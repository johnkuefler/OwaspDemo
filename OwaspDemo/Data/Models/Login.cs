using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OwaspDemo.Data.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(20)]
        public string Email { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }
    }
}
