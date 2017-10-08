using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Infrastructure.Inherits;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Features.Student.Dashboard
{
    [Area("Student")]
    [Authorize("Student")]
    public class DashboardController : Controller
    {
        public HubContext _context;
        public DashboardController()
        {
            _context = new HubContext();
        }
        [Route("/Student")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard";
            return View("Dashboard");
        }

        [Route("/Student/AllCourses")]
        public JsonResult AllCourses()
        {
            var model = _context.Sections
                .Include(x => x.offering)
                .Include(x => x.offering.course)
                .Include(x => x.offering.course.major)
                .Include(x => x.TimeSlots)
                .Include(x => x.offering.semester).ToList();
            return new JsonResult(model);
        }
    }
}