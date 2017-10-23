using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Student.Enroll
{
    /// <summary>
    /// Course search viewmodel get/set
    /// </summary>
    public class CourseSearchVM
    {
        public int SemesterId { get; set; }
        public Semester semester { get; set; }
        public IEnumerable<WebApp.Models.Major> Majors { get; set; }
    }
}
