using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Features.Admin.Semesters
{
    [Area("Admin")]
    public class SemestersController : Controller
    {
        [Route("/Admin/Semesters")]
        public IActionResult Semesters()
        {
            //For Prototyping: Just create an arbitrary list of Students (Accounts entity)
            List<Semester> model = new List<Semester>
            {
                new Semester
                {
                    season = "Fall",
                    year = "2017",
                    Id = 1,
                }
            };

            ViewData["Title"] = "Semesters";
            return View("Semesters", model);
        }

        [Route("/Admin/Semesters/{id}")]
        public IActionResult Semester(int id)
        {
            //For Prototyping: Just create an arbitrary student entity
            var model = new Semester
            {
                season = "Fall",
                year = "2017",
                Id = id,
            };

            ViewData["Title"] = model.season + " " + model.year;
            return View("Semester", model);
        }

        [HttpPost]
        [Route("/Admin/Semesters/Create")]
        public IActionResult Create(Semester semester) => Redirect("/Admin/Semesters");
    }
}