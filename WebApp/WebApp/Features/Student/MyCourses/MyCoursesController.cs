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

        /// <summary>
        /// For the student courses page given a semester id to populate the table 
        /// </summary>
        /// <param name="semesterId"></param>
        /// <returns>View("MyCourses", masterModel)</returns>
        [Route("/Student/Courses/{semesterId?}")]
        public IActionResult Index(int semesterId)
        {
            //Get info about the current account
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var acc = _context.Accounts.Find(Convert.ToInt32(val.Value));

            //If 0 semester id, get the id based on current time frame
            if(semesterId == 0)
            {
                semesterId = _context.Semesters.Where(x => x.startdate <= DateTime.Now).Where(x => x.enddate >= DateTime.Now).First().Id;
            }

            //Populate the model with the current student's enrollment info for the given semester
            var model = _context.Enrollments
                        .Where(x => x.section.offering.semester.Id == semesterId)
                        .Where(x => x.account.Id == acc.Id)
                        .Select(x => new MyCoursesVM
                        {
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

            //Populate new CourseViewerVM with filtered results model
            var masterModel = new CourseViewerVM
            {
                enrollments = model,
                semester = _context.Semesters.Find(semesterId)
            };

            ViewData["Title"] = "My Courses";
            return View("MyCourses", masterModel);
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

        /// <summary>
        /// Calculates the earliest semester available for the student - the first semester the student was enrolled in
        /// </summary>
        /// <param name="currentSemesterId"></param>
        /// <returns>JSON result of semesters</returns>
        [Route("/Student/Courses/AvailbleSemesters/{currentSemesterId?}")]
        public JsonResult AvailableSemesters(int currentSemesterId)
        {
            //Get info about the current account
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var acc = _context.Accounts.Find(Convert.ToInt32(val.Value));

            //Allow account to only see semesters starting from their first semester at the school
            var model = _context.Semesters
                .Where(x => x.Id >= acc.firstsemester) 
                .OrderBy(x => x.startdate)
                .Select(x => new
                {
                    x.Id,
                    x.season,
                    x.year,
                    current = IsCurrentSemester(x.startdate, x.enddate, x.Id, currentSemesterId)
                })
                .ToList();

            return new JsonResult(model);
        }

        /// <summary>
        /// Determine if the given semester is currently happening (selected by default in the drop-down) *or* selected course item.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Whether or not the given semester is currently happening.</returns>
        public bool IsCurrentSemester(DateTime start, DateTime end, int semesterId, int currentSemesterId)
        {
            if(currentSemesterId == 0) return DateTime.Now >= start && DateTime.Now <= end;
            return (currentSemesterId == semesterId);
        }
    }
}