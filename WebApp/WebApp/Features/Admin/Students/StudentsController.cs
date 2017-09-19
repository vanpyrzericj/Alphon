using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Features.Admin.Students
{
    [Area("Admin")]
    public class StudentsController : Controller
    {
        [Route("/Admin/Students")]
        public IActionResult Students()
        {
            //For Prototyping: Just create an arbitrary list of Students (Accounts entity)
            List<Account> model = new List<Account>
            {
                new Account
                {
                    firstname = "John",
                    lastname = "Doe",
                    Id = 1,
                    usertype = "student",
                    lastlogin = DateTime.Now
                },
                new Account
                {
                    firstname = "Jane",
                    lastname = "Doe",
                    Id = 2,
                    usertype = "student",
                    lastlogin = DateTime.Now
                },
                new Account
                {
                    firstname = "Jesse",
                    lastname = "Hartloff",
                    Id = 3,
                    usertype = "student",
                    lastlogin = DateTime.Now
                }
            };

            ViewData["Title"] = "Students";
            return View("Students", model);
        }

        [Route("/Admin/Students/{id}")]
        public IActionResult Student(int id)
        {
            //For Prototyping: Just create an arbitrary student entity
            var model = new Account
            {
                firstname = "John",
                lastname = "Doe",
                Id = id,
                usertype = "student",
                lastlogin = DateTime.Now
            };

            ViewData["Title"] = model.firstname + " " + model.lastname;
            return View("Student", model);
        }
    }
}