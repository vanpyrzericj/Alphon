using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Student.Components
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
            var model = new TopHeaderVM
            {
                CartItems = await _context.Enrollments
                .Where(x => x.account.Id == Convert.ToInt32(val.Value))
                .Where(x => x.status != 1)
                .Include(x => x.section)
                .Include(x => x.section.professor)
                .Include(x => x.section.offering.course)
                .Include(x => x.section.TimeSlots)
                .Include(x => x.section.offering)
                .Include(x => x.section.offering.course.major)
                .ToListAsync(),
                Account = await _context.Accounts
                .Where(x => x.Id == Convert.ToInt32(val.Value))
                .FirstAsync()
            };
            return View(model);
        }
    }
}