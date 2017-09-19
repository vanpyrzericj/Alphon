using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/SignIn");
        }

        [Route("/SignIn")]
        public IActionResult SignIn()
        {
            return View("~/Views/Home/SignIn.cshtml");
        }
    }
}