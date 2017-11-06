using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Features.Admin.Courses
{
    public class CourseVM
    {
        public Course course { get; set; }
        public ICollection<Section> offerings { get; set; }
        public ICollection<Semester> semesters { get; set; }
        public ICollection<Professor> professors { get; set; }
    }
}