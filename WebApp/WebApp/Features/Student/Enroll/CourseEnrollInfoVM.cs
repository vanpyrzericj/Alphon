using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Student.Enroll
{
    /// <summary>
    /// Course enrollment viewmodel get/set
    /// </summary>
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
        public string type { get; set; }
        public int capacity { get; set; }
        public int enrolled { get; set; }
        public bool waitlist { get; internal set; }
    }
}