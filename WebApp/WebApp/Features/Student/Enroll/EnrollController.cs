using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Features.Student.Enroll;
using WebApp.Infrastructure.Inherits;

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