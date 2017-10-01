using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Infrastructure.Inherits;

namespace WebApp.Features.Student.Dashboard
{
    [Area("Student")]
    public class DashboardController : Controller
    {
        [Route("/Student")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard";
            return View("Dashboard");
        }
    }
}