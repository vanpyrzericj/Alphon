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
            //For Prototyping: Just create an arbitrary list of Students (Accounts entity)
            List<Semester> model = new List<Semester>
            {
                new Semester
                {
                    Id = 1,
                    season = "Fall",
                    year = 2017

                },
                new Semester
                {
                    Id = 2,
                    season = "Winter",
                    year = 2017

                },
                new Semester
                {
                    Id = 3,
                    season = "Spring",
                    year = 2017

                },
                new Semester
                {
                    Id = 4,
                    season = "Summer",
                    year = 2017

                },
            };

            ViewData["Title"] = "Enroll";
            return View("Semester", model);
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
            //TODO: Manipulate the filter object as needed
            var result = _context.Sections
                .Where(x => x.offering.semester.Id == SemesterID)
                .Where(x => x.offering.course.major.Id == filter.MajorId)
                //.Where(x => x.offering.type == "lecture")
                //.Where(x => x.offering.course.number == filter.CourseNumber)
                .Include(y => y.offering)
                .Include(y => y.offering.semester)
                .Include(y => y.offering.course.major)
                .Select(a => new CourseFilterResult
                {
                    offeringid = a.offering.Id,
                    coursename = a.offering.course.name,
                    professor = a.professor,
                    credithours = a.offering.course.credithours,
                    timeslot = a.TimeSlots,
                    coursenumber = a.offering.course.number,
                    cousedescription = a.offering.course.description

                })
                .ToList();

            switch (filter.CourseNumberFilterOption)
            {
                case 1:
                    result = result.Where(x => x.coursenumber == filter.CourseNumber).ToList();
                    break;
                case 3:
                    result = result.Where(x => x.coursenumber >= filter.CourseNumber).ToList();
                    break;
                case 4:
                    result = result.Where(x => x.coursenumber <= filter.CourseNumber).ToList();
                    break;
                default:
                    break;
            }

            ViewData["Title"] = "Fall 2017";
            return View("Courses", result);
        }

        [Route("/Student/Enroll/{SemesterID}/Courses/{CourseID}")]
        public IActionResult CourseEnrollInfo(int SemesterID, int CourseID)
        {
            //For Prototyping: Just create an arbitrary student entity
            var model = new CourseEnrollInfoVM()
            {
                course = new Course()
                {
                    name = "Name",
                    number = 1,
                    description = "Something"       
                
                },

                sections = new List<Section>()
                {
                    new Section()
                    {
                        room = "BuildingA 40"
                    },
                    new Section()
                    {
                        room = "BuildingB 60"
                    },
                    new Section()
                    {
                        room = "BuildingC 80"
                    }
                }
            };

            ViewData["Title"] = model.course.name;
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
            //For Prototyping: Just create an arbitrary list of Students (Accounts entity)
            ViewData["Title"] = "Course Search";
            return View("CourseSearch", new CourseSearchVM { Majors = _context.Majors.ToList(), SemesterId = SemesterID});
        }

        [Route("/Student/Enroll/CheckoutResult")]
        public IActionResult CheckoutResult()
        {
            //For Prototyping: Just create an arbitrary list of Students (Accounts entity)
            ViewData["Title"] = "Checkout Result";
            return View("CheckoutResult");
        }

        //FOR TESTING PURPOSES
        [Route("/Student/Enroll/{SemesterID}/TestCourses")]
        public JsonResult TestCourseSelection(int SemesterID)
        {
            //TODO: Manipulate the filter object as needed
            var filter = new ClassFilterVM();
            filter.Career = 1;
            filter.CourseNumber = 442;
            filter.CourseNumberFilterOption = 1;
            filter.MajorId = 1;
            SemesterID = 1;

            var result = _context.Sections
                .Where(x => x.offering.semester.Id == SemesterID)
                .Where(x => x.offering.course.major.Id == filter.MajorId)
                //.Where(x => x.offering.type == "lecture")
                .Where(x => x.offering.course.number == filter.CourseNumber)
                .Include(y => y.offering)
                .Include(y => y.offering.semester)
                .Include(y => y.offering.course.major)
                .ToList();

            return new JsonResult(result);
        }


    }

}