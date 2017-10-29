using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Features.Student.MyCourses
{
    public class CourseViewerVM
    {
        /// <summary>
        /// DCourses viewmodel get/set
        /// </summary>
        public Semester semester { get; set; }  
        public ICollection<MyCoursesVM> enrollments { get; set; }
        public bool IsWithinDropPeriod { get; set; }
    }
}