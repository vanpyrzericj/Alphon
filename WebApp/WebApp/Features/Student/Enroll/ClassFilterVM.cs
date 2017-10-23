using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Features.Student.Enroll
{
    /// <summary>
    /// Class search filter viewmodel get/set
    /// </summary>
    public class ClassFilterVM
    {
        public int MajorId { get; set; }
        public int CourseNumber { get; set; }
        public int CourseNumberFilterOption { get; set; } // {1} IsExactly {2} Contains {3} >= {4} <=
        public int Career { get; set; }
        public bool ShowOpenClasses { get; set; }
        public string InstructorLastName { get; set; }
        public int InstructorNameFilterOption { get; set; } // {1} IsExactly {2} Contains
        public int ModeOfInstruction { get; set; } // {1} Classroom {2} Online {3} Hybrid
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
    }
} 