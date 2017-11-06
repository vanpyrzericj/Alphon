using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApp.Features.Admin.Professors
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class ProfessorsController : Controller
    {
        public HubContext _context;
        public ProfessorsController()
        {
            _context = new HubContext();
        }
        [Route("/Admin/Professors")]
        public async Task<IActionResult> ProfessorsAsync()
        {
            ViewData["Title"] = "Professors";
            return View("Professors", await _context.Professors.ToListAsync());
        }

        [Route("/Admin/Professors/{id}")]
        public async Task<IActionResult> ProfessorAsync(int id)
        {
            var model = new ProfessorVM
            {
                professor = await _context.Professors.FindAsync(id),
                sections = await _context.Sections.Where(x => x.professor.Id == id).Include(x => x.offering.course).Include(x => x.offering.semester).ToListAsync()
            };
            ViewData["Title"] = model.professor.firstname + " " + model.professor.lastname;
            return View("Professor", model);
        }

        [HttpPost]
        [Route("/Admin/Professors/Create")]
        public async Task<IActionResult> CreateAsync(Professor professor)
        {
            _context.Professors.Add(professor);
            await _context.SaveChangesAsync();
            return Redirect("/Admin/Professors/" + professor.Id);
        }
    }
}