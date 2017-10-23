using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    /// <summary>
    /// Holds information about a class offering for a particular semester 
    /// </summary>
    public class Offering
    {
        public int Id { get; set; }
        public Semester semester { get; set; }
        public Course course { get; set; }
        public string type { get; set; }
        public int capacity { get; set; }
        public int parentcourse { get; set; }

    }
}