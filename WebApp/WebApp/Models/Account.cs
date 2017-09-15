using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string dob { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public DateTime lastlogin { get; set; }
        public string usertype { get; set; }
    }
}