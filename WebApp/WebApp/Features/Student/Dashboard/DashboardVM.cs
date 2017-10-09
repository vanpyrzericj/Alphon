using System.Collections.Generic;
using WebApp.Features.Student.MyCourses;
using WebApp.Models;

namespace WebApp.Features.Student.Dashboard
{
    public class DashboardVM
    {
        public int CurrentCourses { get; set; }
        public int CurrentCreditHours { get; set; }
        public int AvailableCreditHours { get; set; }
        public int ShoppingCartCourses { get; set; }
        public ICollection<MyCoursesVM> Enrollments { get; set; }
        public Semester Semester { get; internal set; }
    }
}