using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    //Information about a major
    public class Major
    {
        public int Id { get; set; }
        //Three letter name (ie CSE)
        public string name { get; set; } 
        //Full name (ie - Computer Science)
        public string fullname { get; set; }
        //Lecture or recitation
        public string type { get; set; }

    }
}