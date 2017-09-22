using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Student.Enroll
{
    public class CourseEnrollInfoVM
    {
        public Course course { get; set; }
        public ICollection<Section> sections { get; set; }
    }
}
