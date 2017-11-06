using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebApp.Features.Student.MyCourses;
using WebApp.Models;

namespace WebApp.Features.Student.Dashboard
{
    [Area("Student")]
    [Authorize("Student")]
    public class DashboardController : Controller
    {
        public HubContext _context;
        public DashboardController()
        {
            _context = new HubContext();
        }

        /// <summary>
        /// /Student page (the student's personal dashboard) 
        /// </summary>
        /// <returns>View("Dashboard", model)</returns>
        [Route("/Student")]
        public IActionResult Index()
        {
            //Get information about current user (user account, registration info) and semester
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var acc = _context.Accounts.Find(Convert.ToInt32(val.Value));
            var semester = _context.Semesters.Where(x => x.startdate <= DateTime.Now).Where(x => x.enddate >= DateTime.Now).First();
            var currentSemesterId = semester.Id;
            var enrollments = _context.Enrollments.Where(x => x.account.Id == acc.Id).Where(x => x.section.offering.semester.Id == currentSemesterId).Include(s => s.section.offering.course).ToList();

            //Create new dashboard viewmodel and populate using filtered results
            var model = new DashboardVM();
            model.CurrentCourses = enrollments.Where(c => c.section.offering.type == "lecture").Where(c => c.status == 1).Count();
            model.CurrentCreditHours = enrollments.Where(c => c.status == 1).Where(x => x.section.offering.type == "lecture").Sum(s => s.section.offering.course.credithours);
            model.AvailableCreditHours = 16 - model.CurrentCreditHours;
            model.ShoppingCartCourses = enrollments.Where(c => c.section.offering.type == "lecture").Where(c => c.status == 2).Count();

            model.IsWithinDropPeriod = (semester.enrollopen <= DateTime.Now && DateTime.Now <= semester.resignclose);

            model.Notifications = new List<Notification>();

            //User Notifications
            model.Notifications = _context.Notifications.Where(x => x.account.Id == acc.Id).Where(x => x.status != 1).ToList();

            //Populate with enrollment info based on the current semester and current student
            model.Semester = semester;
            model.Enrollments = _context.Enrollments
                        .Where(x => x.section.offering.semester.Id == currentSemesterId)
                        .Where(x => x.status == 1)
                        .Where(x => x.account.Id == acc.Id)
                        .Select(x => new MyCoursesVM
                        {
                            id = x.Id,
                            professor = x.section.professor,
                            room = x.section.room,
                            timeslot = x.section.TimeSlots,
                            credithours = x.section.offering.course.credithours,
                            coursename = x.section.offering.course.name,
                            coursenumber = x.section.offering.course.number,
                            major = x.section.offering.course.major.name,
                            type = Capitalize(x.section.offering.type)
                        })
                        .ToList();

            ViewData["Title"] = "Dashboard";
            return View("Dashboard", model);
        }

        /// <summary>
        /// Given a string, return that string with the first letter capitalized (for certain ui elements)
        /// </summary>
        /// <param name="original"></param>
        /// <returns>String with capitalized first letter</returns>
        public string Capitalize(string original)
        {
            return char.ToUpper(original[0]) + original.Substring(1);
        }

        //[Route("/Student/AllCourses")]
        //public JsonResult AllCourses()
        //{
        //    var model = _context.Sections
        //        .Include(x => x.offering)
        //        .Include(x => x.offering.course)
        //        .Include(x => x.offering.course.major)
        //        .Include(x => x.TimeSlots)
        //        .Include(x => x.offering.semester).ToList();
        //    return new JsonResult(model);
        //}

        [HttpPost]
        [Route("/Student/ClearNotification/{notificationId}")]
        public IActionResult ClearNotification(int notificationId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");

            var notification = _context.Notifications.Where(x => x.Id == notificationId).Include(a => a.account).First();

            //Ensure The Authenticated User Is Doing the Clearing
            if (Convert.ToInt32(val.Value) != notification.account.Id) return Unauthorized();
            notification.status = 1;
            _context.SaveChanges();
            return Ok();
        }
    }
}