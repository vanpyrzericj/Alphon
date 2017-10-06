using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public ICollection<Section> sections { get; set; }
    }
}