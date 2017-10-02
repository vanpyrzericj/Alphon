using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Offering
    {
        public int Id { get; set; }
        public Semester semester { get; set; }
        public Course course { get; set; }
        public string type { get; set; }

    }
}