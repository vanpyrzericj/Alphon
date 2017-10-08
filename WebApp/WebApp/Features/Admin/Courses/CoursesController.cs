using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Infrastructure.Inherits;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Features.Admin.Courses
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class CoursesController : Controller
    {
        [Route("/Admin/Courses")]
        public IActionResult Courses()
        {
            //For Prototyping: Just create an arbitrary list of Students (Accounts entity)
            List<Course> model = new List<Course>
            {
                new Course
                {
                    name = "Software Engineering",
                    number = 442,
                    Id = 1,
                    description = "github101"
                }
            };

            ViewData["Title"] = "Courses";
            return View("Courses", model);
        }

        [Route("/Admin/Courses/{id}")]
        public IActionResult Course(int id)
        {
            //For Prototyping: Just create an arbitrary student entity
            var model = new Course
            {
				name = "Software Engineering",
				number = 442,
				Id = 1,
				description = "github101"
            };

            ViewData["Title"] = model.name;
            return View("Course", model);
        }

        [HttpPost]
        [Route("/Admin/Courses/Create")]
        public IActionResult Create(Course course) => Redirect("/Admin/Courses");
    }
}
