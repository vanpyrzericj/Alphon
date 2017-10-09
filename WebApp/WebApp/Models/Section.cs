using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string room { get; set; }
        public Offering offering { get; set; }
        public Professor professor { get; set; }
        public ICollection<TimeSlot> TimeSlots { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}