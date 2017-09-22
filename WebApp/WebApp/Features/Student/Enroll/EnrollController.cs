using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Features.Student.Enroll;

namespace WebApp.Features.Students.Enroll
{
    [Area("Student")]
    public class EnrollController : Controller
    {
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

        [Route("/Student/Enroll/{id}")]
        public IActionResult CourseSelection(int id)
        {
            //For Prototyping: Just create an arbitrary list of Students (Accounts entity)
            List<Course> model = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    number = "CSE 442",
                    name = "Software Engineering",
                    description = "Industry standard software development."

                },
                 new Course
                {
                    Id = 2,
                    number = "CSE 421",
                    name = "Operating Systems",
                    description = "Just resign now."

                }
            };

            ViewData["Title"] = "Fall 2017";
            return View("Courses", model);
        }

        [Route("/Student/Enroll/{SemesterID}/Course/{CourseID}")]
        public IActionResult CourseEnrollInfo(int SemesterID, int CourseID)
        {
            //For Prototyping: Just create an arbitrary student entity
            var model = new CourseEnrollInfoVM()
            {
                course = new Course()
                {
                    name = "Name",
                    number = "1",
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
    }
}