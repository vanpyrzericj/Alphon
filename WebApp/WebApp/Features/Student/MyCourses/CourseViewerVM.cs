using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Features.Student.MyCourses
{
    public class CourseViewerVM
    {
        public Semester semester { get; set; }  
        public ICollection<MyCoursesVM> enrollments { get; set; }
    }
}