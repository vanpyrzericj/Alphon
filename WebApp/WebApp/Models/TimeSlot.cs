using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public string dayofweek { get; set; }
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }

    }
}