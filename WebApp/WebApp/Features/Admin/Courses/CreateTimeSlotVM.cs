using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Features.Admin.Courses
{
    public class CreateTimeSlotVM
    {
        public int Id { get; set; }
        public string[] dayofweek { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public int sectionid { get; set; }
    }
}