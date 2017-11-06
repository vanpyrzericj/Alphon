using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Features.Admin.Courses
{
    public class CoursesVM
    {
        public ICollection<Course> courses { get; set; }
        public ICollection<Major> majors { get; set; }
    }
}