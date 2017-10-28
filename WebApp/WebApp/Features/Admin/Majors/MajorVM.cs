using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Admin.Majors
{
    public class MajorVM
    {
        public Major major { get; set; }
        public ICollection<Course> courses { get; set; }
    }
}