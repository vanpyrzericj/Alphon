using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Student.Enroll
{
    public class CourseFilterResult
    {
        public int offeringid { get; set; }

        public string coursename { get; set; }

        public Professor professor { get; set; }

        public int credithours { get; set; }

        public ICollection<TimeSlot> timeslot { get; set; }d

    }
}
