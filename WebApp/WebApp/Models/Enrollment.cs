using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public Account account { get; set; }
        public Section section { get; set; }
    }
}