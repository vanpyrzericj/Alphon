using System;

namespace WebApp.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public string season { get; set; }
        public int year { get; set; }
        public DateTime enrollopen { get; set; }
        public DateTime enrollclose { get; set; }
        public DateTime resignclose { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
}