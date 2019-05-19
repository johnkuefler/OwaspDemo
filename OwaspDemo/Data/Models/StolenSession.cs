using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwaspDemo.Data.Models
{
    public class StolenSession
    {
        public int Id { get; set; }
        public string Site { get; set; }
        public string SessionCookieValue { get; set; }
    }
}
