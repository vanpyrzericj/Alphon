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
            var model = _context.Accounts.Find(id);

            ViewData["Title"] = model.firstname + " " + model.lastname;
            return View("Student", model);
        }

        [HttpPost]
        [Route("/Admin/Students/Create")]
        public IActionResult Create(Account account)
        {
            var salt = GenerateSalt();
            account.password = GenerateHashedPassword(account.password, salt);
            account.salt = GetString(salt);
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return Redirect("/Admin/Students/" + account.Id);
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public string GenerateHashedPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return hashed;
        }

        public static byte[] GetBytes(string str)
        {
            return Convert.FromBase64String(str);
        }

        public static string GetString(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}