using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Features.Student.Enroll
{
    public class CourseSearchVM
    {
        public int SemesterId { get; set; }
        public IEnumerable<WebApp.Models.Major> Majors { get; set; }
    }
}
