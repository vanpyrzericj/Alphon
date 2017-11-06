using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Student.Components
{
    public class TopHeaderVM
    {
        public Account Account { get; set; }
        public ICollection<Enrollment> CartItems { get; set; }
    }
}