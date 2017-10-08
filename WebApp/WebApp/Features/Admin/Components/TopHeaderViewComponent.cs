using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Admin.Components
{
    public class TopHeaderViewComponent : ViewComponent
    {
        private readonly HubContext _context;

        public TopHeaderViewComponent()
        {
            _context = new HubContext();
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            return View(await _context.Accounts
                .Where(x => x.Id == Convert.ToInt32(val.Value))
                .FirstAsync());
        }
    }
}