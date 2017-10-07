using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Features.Student.Enroll;
using WebApp.Infrastructure.Inherits;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Features.Students.Enroll
{
    [Area("Student")]
    public class EnrollController : Controller
    {
        private HubContext _context;
    
        public EnrollController()
        {
            _context = new HubContext();
        }

        [Route("/Student/Enroll")]
        public IActionResult Enroll()
        {
            ViewData["Title"] = "Enroll";
            return View("Semester", _context.Semesters.ToList());
        }

        /// <summary>
        /// Page with filtered results
        /// </summary>
        /// <param name="SemesterID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/Student/Enroll/{SemesterID}/Courses")]
        public IActionResult CourseSelection(int SemesterID, ClassFilterVM filter)
        {
            //For Prototyping: Just create an arbitrary list of Students (Accounts entity)
            List<Course> model = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    number = 442,
                    name = "Software Engineering",
                    description = "Industry standard software development."

                },
                 new Course
                {
                    Id = 2,
                    number = 421,
                    name = "Operating Systems",
                    description = "Just resign now."

                }
            };

            ViewData["Title"] = "Fall 2017";
            return View("Courses", model);
        }

        [Route("/Student/Enroll/{SemesterID}/Courses/{OfferingID}")]
        public async Task<IActionResult> CourseEnrollInfoAsync(int SemesterID, int OfferingID)
        {
            var sectionId = (await _context.Sections.Where(x => x.offering.Id == OfferingID).FirstAsync()).Id;
            var course = await _context.Offerings.Where(x => x.Id == OfferingID).Include(m => m.course.major).Select(a => a.course).FirstAsync();
            var recitationOfferings = await _context.Offerings.Where(x => x.type == "recitation").Where(y => y.course.Id == course.Id).ToListAsync();
            
            var model = new CourseEnrollInfoVM()
            {
                course = course,
                professor = await _context.Sections.Where(x => x.Id == sectionId).Select(a => a.professor).FirstAsync(),
                timeslots = await _context.Timeslots.Where(x => x.section.Id == sectionId).Select(x => new TimeSlot()
                {
                    starttime = x.starttime,
                    endtime = x.endtime,
                    dayofweek = x.dayofweek
                }).ToListAsync(),
                OfferingId = OfferingID,
                semester = await _context.Semesters.FindAsync(SemesterID)
            };

            foreach(var offer in recitationOfferings)
            {
                model.recitations.Add(_context.Sections.Where(x => x.offering.Id == offer.Id).Include(p => p.professor).First());
            }

            ViewData["Title"] = model.course.name;
            //return new JsonResult(model);
            return View("Course", model);
        }

        [Route("/Student/Cart")]
        public IActionResult Cart()
        {
            return View("Cart");
        }
        
        /// <summary>
        /// This is the page that displays the filter form
        /// </summary>
        /// <param name="SemesterID"></param>
        /// <returns></returns>
        [Route("/Student/Enroll/{SemesterID}/Search")]
        public IActionResult CourseSearch(int SemesterID)
        {
            ViewData["Title"] = "Course Search";
            return View("CourseSearch", new CourseSearchVM { Majors = _context.Majors.ToList(), SemesterId = SemesterID});
        }

        [Route("/Student/Enroll/CheckoutResult")]
        public IActionResult CheckoutResult()
        {
            ViewData["Title"] = "Checkout Result";
            return View("CheckoutResult");
        }

        //FOR TESTING PURPOSES
        [Route("/Student/Enroll/{SemesterID}/TestCourses")]
        public IActionResult TestCourseSelection(int SemesterID)
        {
            //TODO: Manipulate the filter object as needed
            var filter = new ClassFilterVM();
            filter.CourseNumber = 425;

            //TODO: Delete this fake model and have your returned results be the model object.
            List<Course> model = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    number = 442,
                    name = "Software Engineering",
                    description = "Industry standard software development."

                },
                 new Course
                {
                    Id = 2,
                    number = 421,
                    name = "Operating Systems",
                    description = "Just resign now."

                }
            };

            ViewData["Title"] = "Fall 2017";
            return View("Courses", model);
        }
    }
}