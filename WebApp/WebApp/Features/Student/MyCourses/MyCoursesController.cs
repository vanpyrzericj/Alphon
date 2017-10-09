using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Infrastructure.Inherits;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;
using System.Security.Claims;

namespace WebApp.Features.Student.MyCourses
{
    [Area("Student")]
    [Authorize("Student")]
    public class MyCoursesController : Controller
    {

        private HubContext _context;

        public MyCoursesController()
        {
            _context = new HubContext();
        }


        [Route("/Student/Courses")]
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var acc = _context.Accounts.Find(Convert.ToInt32(val.Value));

            var model = _context.Enrollments
                        .Where(x => x.section.offering.semester.Id == 1)
                        .Where(x => x.account.Id == acc.Id)
                        .Select(x => new MyCoursesVM
                        {
                            professor = x.section.professor,
                            room = x.section.room,
                            timeslot = x.section.TimeSlots,
                            credithours = x.section.offering.course.credithours,
                            coursename = x.section.offering.course.name,
                            coursenumber = x.section.offering.course.number,
                            major = x.section.offering.course.major.name
                        })
                        .ToList();
         
            ViewData["Title"] = "My Courses";
            return View("MyCourses", model);
        }
    }
}