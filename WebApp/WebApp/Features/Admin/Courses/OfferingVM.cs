using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Admin.Courses
{
    public class OfferingVM
    {
        public Section offering { get; set; }
        public ICollection<Section> recitationOfferings { get; set; }
        public List<Professor> professors { get; internal set; }
        public List<Semester> semesters { get; internal set; }
    }
}