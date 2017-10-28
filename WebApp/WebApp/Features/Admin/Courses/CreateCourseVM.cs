using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Features.Admin.Courses
{
    public class CreateCourseVM
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public string description { get; set; }
        public int credithours { get; set; }
        public int majorid { get; set; }
        // {1} Undergrad {2} Grad
        public int career { get; set; }
        // {1} Classroom {2} Online {3} Hybrid
        public int modeofinstruction { get; set; }
    }
}