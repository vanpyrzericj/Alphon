using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public string description { get; set; }
        public int credithours { get; set; }
        public Major major { get; set; }
    }
}