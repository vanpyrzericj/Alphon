using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Student.MyCourses
{
    public class MyCoursesVM
    {
        public IEnumerable<WebApp.Models.Semester> Semester { get; set; }

        public int year { get; set; }

        public int semesterid { get; set; }

        public Professor professor { get; set; }

        public String room { get; set; }

        public int credithours { get; set; }

        public ICollection<TimeSlot> timeslot { get; set; }

        public int coursenumber { get; set; }

        public int offeringid { get; set; }

        public string coursename { get; set; }

        public string major{ get; set; }
        public int id { get; internal set; }
    }
}
