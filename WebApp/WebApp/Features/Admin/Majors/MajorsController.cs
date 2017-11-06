using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace WebApp.Features.Admin.Majors
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class MajorsController : Controller
    {
        public HubContext _context;
        public MajorsController()
        {
            _context = new HubContext();
        }

        [Route("/Admin/Majors")]
        public async Task<IActionResult> MajorsAsync()
        {
            ViewData["Title"] = "Majors";
            return View("Majors", await _context.Majors.ToListAsync());
        }

        [Route("/Admin/Majors/{id}")]
        public async Task<IActionResult> MajorAsync(int id)
        {
            var model = new MajorVM
            {
                major = await _context.Majors.FindAsync(id),
                courses = await _context.Courses.Where(x => x.major.Id == id).ToListAsync()
            };
            ViewData["Title"] = model.major.fullname;
            return View("Major", model);
        }

        [HttpPost]
        [Route("/Admin/Majors/Create")]
        public async Task<IActionResult> CreateAsync(Major major)
        {
            _context.Majors.Add(major);
            await _context.SaveChangesAsync();
            return Redirect("/Admin/Majors/" + major.Id);
        }
    }
}