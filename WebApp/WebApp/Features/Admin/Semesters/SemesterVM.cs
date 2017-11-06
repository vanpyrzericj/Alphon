using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Features.Admin.Semesters
{
    public class SemesterVM
    {
        public Semester semester { get; set; }
        public ICollection<Section> sections { get; set; }
    }
}