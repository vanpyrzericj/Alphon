using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Student.Enroll
{
    public class SemesterPickerVM
    {
        public ICollection<Semester> AvailableSemesters { get; set; }
        public ICollection<Semester> AllSemesters { get; set; }
    }
}