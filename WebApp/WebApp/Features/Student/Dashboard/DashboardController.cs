using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Infrastructure.Inherits;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApp.Features.Student.MyCourses;

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
        [Route("/Student")]
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var acc = _context.Accounts.Find(Convert.ToInt32(val.Value));
            var semester = _context.Semesters.Where(x => x.startdate <= DateTime.Now).Where(x => x.enddate >= DateTime.Now).First();
            var currentSemesterId = semester.Id;
            var enrollments = _context.Enrollments.Where(x => x.account.Id == acc.Id).Where(x => x.section.offering.semester.Id == currentSemesterId).Include(s => s.section.offering.course).ToList();
            
            var model = new DashboardVM();
            model.CurrentCourses = enrollments.Where(c => c.status == 1).Count();
            model.CurrentCreditHours = enrollments.Where(c => c.status == 1).Sum(s => s.section.offering.course.credithours);
            model.AvailableCreditHours = 16 - model.CurrentCreditHours;
            model.ShoppingCartCourses = enrollments.Where(c => c.status == 2).Count();

            //model.CurrentCourses = 1;
            //model.CurrentCreditHours = 1;
            //model.AvailableCreditHours = 1;
            //model.ShoppingCartCourses = 1;

            model.Semester = semester;
            model.Enrollments = _context.Enrollments
                        .Where(x => x.section.offering.semester.Id == currentSemesterId)
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
                            major = x.section.offering.course.major.name
                        })
                        .ToList();

            ViewData["Title"] = "Dashboard";
            return View("Dashboard", model);
        }

        [Route("/Student/AllCourses")]
        public JsonResult AllCourses()
        {
            var model = _context.Sections
                .Include(x => x.offering)
                .Include(x => x.offering.course)
                .Include(x => x.offering.course.major)
                .Include(x => x.TimeSlots)
                .Include(x => x.offering.semester).ToList();
            return new JsonResult(model);
        }
    }
}