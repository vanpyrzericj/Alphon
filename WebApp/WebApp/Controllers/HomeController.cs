using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        //public HomeController(HubContext context)
        //{
        //    _context = context;
        //}
        //private HubContext _context;


        // GET: /<controller>/
        public JsonResult Index()
        {
            HubContext _context = new HubContext();

            var account = new Account();
            account.firstname = "Joe";
            account.lastname = "Shmoe";
            account.lastlogin = DateTime.Now;
            account.password = "Hello There!";

            _context.Accounts.Add(account);

            //var accountToRemove = _context.Accounts.Find(1);
            //accountToRemove.firstname = "Change";

            //_context.Accounts.Remove(accountToRemove);

            //_context.SaveChanges();


            var result = _context.Accounts.ToList();

            return new JsonResult(result);
        }
    }
}
