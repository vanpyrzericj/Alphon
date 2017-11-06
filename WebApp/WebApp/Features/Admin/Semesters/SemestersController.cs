using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Infrastructure.Inherits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Features.Admin.Semesters
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class SemestersController : Controller
    {
        public HubContext _context;
        public SemestersController()
        {
            _context = new HubContext();
        }
        [Route("/Admin/Semesters")]
        public async Task<IActionResult> SemestersAsync()
        {
            ViewData["Title"] = "Semesters";
            return View("Semesters", await _context.Semesters.ToListAsync());
        }

        [Route("/Admin/Semesters/{id}")]
        public async Task<IActionResult> SemesterAsync(int id)
        {
            var model = new SemesterVM
            {
                semester = await _context.Semesters.FindAsync(id),
                sections = await _context.Sections.Where(x => x.offering.semester.Id == id).Include(x => x.offering.course).Include(x => x.professor).ToListAsync()
            };
            ViewData["Title"] = model.semester.season + " " + model.semester.year;
            return View("Semester", model);
        }

        [HttpPost]
        [Route("/Admin/Semesters/Create")]
        public async Task<IActionResult> CreateAsync(Semester semester)
        {
            _context.Semesters.Add(semester);
            await _context.SaveChangesAsync();
            return Redirect("/Admin/Semesters/" + semester.Id);
        }
    }
}