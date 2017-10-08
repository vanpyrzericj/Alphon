using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Infrastructure.Inherits;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Features.Student.MyCourses
{
    [Area("Student")]
    [Authorize("Student")]
    public class MyCoursesController : Controller
    {
        [Route("/Student/Courses")]
        public IActionResult Index()
        {
            ViewData["Title"] = "My Courses";
            return View("MyCourses");
        }
    }
}