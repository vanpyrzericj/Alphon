using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Infrastructure.Inherits;

namespace WebApp.Features.Admin.Students
{
    [Area("Admin")]
    public class StudentsController : Controller
    {
        public HubContext _context;
        public StudentsController()
        {
            _context = new HubContext();
        }

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
            return View("Students", _context.Accounts.ToList());
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

        [HttpPost]
        [Route("/Admin/Students/Create")]
        public IActionResult Create(Account account) => Redirect("/Admin/Students");
    }
}