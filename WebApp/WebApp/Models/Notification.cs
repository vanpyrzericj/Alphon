using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        //User Account
        public Account account { get; set; }
        public int status { get; set; } // {0} Unread {1} Cleared
    }
}