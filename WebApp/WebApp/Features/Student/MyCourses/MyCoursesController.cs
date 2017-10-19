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


        [Route("/Student/Courses/{semesterId?}")]
        public IActionResult Index(int semesterId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var acc = _context.Accounts.Find(Convert.ToInt32(val.Value));

            if (semesterId == 0)
            {
                semesterId = _context.Semesters.Where(x => x.startdate <= DateTime.Now).Where(x => x.enddate >= DateTime.Now).First().Id;
            }

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

            var masterModel = new CourseViewerVM
            {
                enrollments = model,
                semester = _context.Semesters.Find(semesterId)
            };

            ViewData["Title"] = "My Courses";
            return View("MyCourses", masterModel);
        }

        public string Capitalize(string original)
        {
            return char.ToUpper(original[0]) + original.Substring(1);
        }

        [Route("/Student/Courses/AvailbleSemesters/{currentSemesterId?}")]
        public JsonResult AvailableSemesters(int currentSemesterId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var acc = _context.Accounts.Find(Convert.ToInt32(val.Value));

            var model = _context.Semesters
                .Where(x => x.Id >= acc.firstsemester) //Allow account to only see semesters starting from their first semester at the school
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
            if (currentSemesterId == 0) return DateTime.Now >= start && DateTime.Now <= end;
            return (currentSemesterId == semesterId);
        }

        [Route("/Student/Courses/Calendar")]
        public JsonResult Calendar()
        {
            int semesterId = 0;
            Semester sem;
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var acc = _context.Accounts.Find(Convert.ToInt32(val.Value));

            sem = _context.Semesters.Where(x => x.startdate <= DateTime.Now).Where(x => x.enddate >= DateTime.Now).First();
            semesterId = sem.Id;

            var enrollments = _context.Enrollments
                        .Where(x => x.section.offering.semester.Id == semesterId)
                        .Where(x => x.account.Id == acc.Id)
                        .Select(x => new
                        {
                            room = x.section.room,
                            timeslot = x.section.TimeSlots,
                            coursename = x.section.offering.course.name,
                            coursenumber = x.section.offering.course.number
                        })
                        .ToList();

            var model = new List<dynamic>();
            foreach (var course in enrollments)
            {
                var temp = new
                {
                    title = course.coursename,
                    description = course.room,
                    start = course.timeslot.ElementAt(0).starttime,
                    end = course.timeslot.ElementAt(0).endtime,
                    dow = GetDaysOfWeekArray(course.timeslot),
                    ranges = new List<dynamic>()
                };
                temp.ranges.Add(new {
                    start = sem.startdate.ToShortDateString(),
                    end = sem.enddate.ToShortDateString()
                });
                model.Add(temp);
            }

            return new JsonResult(model);
        }

        [Route("/Student/Calendar")]
        public IActionResult MyCalendar()
        {
            return View("Calendar");
        }

        private object GetDaysOfWeekArray(ICollection<TimeSlot> timeslot)
        {
            var dow = new List<int>();
            foreach (var slot in timeslot)
            {
                switch (slot.dayofweek)
                {
                    case "Monday":
                        dow.Add(1);
                        continue;
                    case "Tuesday":
                        dow.Add(2);
                        continue;
                    case "Wednesday":
                        dow.Add(3);
                        continue;
                    case "Thursday":
                        dow.Add(4);
                        continue;
                    case "Friday":
                        dow.Add(5);
                        continue;
                }
            }
            return dow;
        }
    }
}