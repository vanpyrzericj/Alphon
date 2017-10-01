using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Infrastructure.Inherits
{
    public class BaseController : Controller
    {
        public HubContext _context;
        public BaseController(HubContext context)
        {
            _context = context;
        }
    }
}