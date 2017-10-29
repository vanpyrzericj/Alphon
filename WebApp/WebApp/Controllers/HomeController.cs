using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using CryptSharp;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private HubContext _context;
        public HomeController()
        {
            _context = new HubContext();
        }

        public IActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        [Route("/Register")]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessRegister(Account account)
        {
            if(!_context.Accounts.Any(x => x.email == account.email)){
                account.password = Crypter.Blowfish.Crypt(account.password, Crypter.Blowfish.GenerateSalt());
                account.firstsemester = 2;
                account.lastlogin = DateTime.MinValue;
                account.usertype = "Student";
                _context.Accounts.Add(account);

                try
                {
                    _context.Enrollments.Add(new Enrollment
                    {
                        account = account,
                        section = _context.Sections.Find(24),
                        status = 1,
                        dateadded = DateTime.Now
                    });
                    _context.Enrollments.Add(new Enrollment
                    {
                        account = account,
                        section = _context.Sections.Find(25),
                        status = 1,
                        dateadded = DateTime.Now
                    });
                    _context.Enrollments.Add(new Enrollment
                    {
                        account = account,
                        section = _context.Sections.Find(28),
                        status = 1,
                        dateadded = DateTime.Now
                    });
                    _context.Enrollments.Add(new Enrollment
                    {
                        account = account,
                        section = _context.Sections.Find(29),
                        status = 1,
                        dateadded = DateTime.Now
                    });
                } catch(Exception ex)
                {
                    //This means the offering doesn't exist. They must not be using demo database. Ignore and continue.
                }
                

                _context.SaveChanges();

                return View("~/Views/Home/RegisterResult.cshtml");
            }
            else
            {
                return View("~/Views/Home/RegisterResult.cshtml");
            }
        }

        [Route("/Register")]
        public IActionResult Register()
        {
            return View("~/Views/Home/Register.cshtml");
        }

        [Route("/Forbidden")]
        public IActionResult Forbidden()
        {
            return View("~/Views/Home/Forbidden.cshtml");
        }

        [Route("/SignIn")]
        public IActionResult SignIn()
        {
            return View("~/Views/Home/SignIn.cshtml");
        }

        [HttpPost]
        [Route("/SignIn")]
        public async Task<IActionResult> ProcessSignInAsync(string emailLogin, string passwordLogin, string ip1, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            try
            {
                if (!string.IsNullOrWhiteSpace(emailLogin))
                {
                    var account = _context.Accounts.Where(x => x.email == emailLogin).First();
                    
                    //Check If Password Exists
                    if (!Crypter.CheckPassword(passwordLogin, account.password))
                        return View("~/Views/Home/SignInError.cshtml", "error");

                    //If Account Exists, Create Principal Object For Authenticated User
                    var claims = new List<Claim>
                {
                    new Claim("sub", account.Id.ToString()),
                    new Claim("given_name", account.firstname + " " + account.lastname),
                    new Claim("role", account.usertype)
                };

                    var id = new ClaimsIdentity(claims, "password");
                    var p = new ClaimsPrincipal(id);

                    await HttpContext.SignInAsync("MyCookieAuthenticationScheme", p);

                    account.lastlogin = DateTime.Now;
                    _context.SaveChanges();

                    if (!string.IsNullOrWhiteSpace(returnUrl)) return LocalRedirect(returnUrl);
                    else if (account.usertype == "Student") return LocalRedirect("/Student");
                    else return LocalRedirect("/Admin");
                }
            }
            catch(Exception ex)
            {
                //A "Sequence contains no elements" exception will be thrown if the email/username provided cannot be found in the database. So we return an error.
                return View("~/Views/Home/SignInError.cshtml", "error");
            }


            return View("~/Views/Home/SignInError.cshtml", "error");
        }

        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
            return Redirect("/?signedout");
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
            prf: KeyDerivationPrf.HMACSHA256,
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