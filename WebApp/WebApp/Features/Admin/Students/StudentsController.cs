using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Infrastructure.Inherits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using CryptSharp;

namespace WebApp.Features.Admin.Students
{
    [Area("Admin")]
    [Authorize("Admin")]
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
            ViewData["Title"] = "Students";
            return View("Students", _context.Accounts.Where(x => x.usertype == "Student").ToList());
        }

        [Route("/Admin/Students/{id}")]
        public IActionResult Student(int id)
        {
            var model = _context.Accounts.Find(id);
            ViewData["Title"] = model.firstname + " " + model.lastname;
            return View("Student", model);
        }

        [HttpPost]
        [Route("/Admin/Students/Create")]
        public async Task<IActionResult> CreateAsync(Account account)
        {
            account.password = Crypter.Blowfish.Crypt(account.password, Crypter.Blowfish.GenerateSalt());
            account.firstsemester = 1;
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return Redirect("/Admin/Students/" + account.Id);
        }
    }
}