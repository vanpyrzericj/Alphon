using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Student.Enroll
{
    public class CourseEnrollInfoVM
    {
        public CourseEnrollInfoVM()
        {
            recitations = new List<Section>();
        }
        public Course course { get; set; }
        public Professor professor { get; set; }
        public Semester semester { get; set; }
        public int OfferingId { get; set; }
        public ICollection<TimeSlot> timeslots { get; set; }
        public ICollection<Section> recitations { get; set; }
    }
}