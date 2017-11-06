using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Features.Admin.Courses
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class CoursesController : Controller
    {
        public HubContext _context;
        public CoursesController()
        {
            _context = new HubContext();
        }

        [Route("/Admin/Courses")]
        public async Task<IActionResult> CoursesAsync()
        {
            var model = new CoursesVM
            {
                courses = await _context.Courses.ToListAsync(),
                majors = await _context.Majors.ToListAsync()
            };
            ViewData["Title"] = "Courses";
            return View("Courses", model);
        }

        [Route("/Admin/Courses/{id}")]
        public async Task<IActionResult> CourseAsync(int id)
        {
            var model = new CourseVM
            {
                course = await _context.Courses.FindAsync(id),
                offerings = await _context.Sections.Where(x => x.offering.course.Id == id).Where(x => x.offering.type == "lecture").Include(x => x.TimeSlots).Include(x => x.professor).Include(x => x.offering.semester).ToListAsync(),
                professors = await _context.Professors.ToListAsync(),
                semesters = await _context.Semesters.ToListAsync()
            };

            ViewData["Title"] = model.course.name;
            return View("Course", model);
        }

        [Route("/Admin/Courses/{id}/Offerings/{sectionId}")]
        public async Task<IActionResult> CourseOfferingAsync(int id, int sectionId)
        {
            var model = new OfferingVM
            {
                offering = await _context.Sections
                .Where(x => x.Id == sectionId)
                .Include(x => x.offering)
                .Include(x => x.offering.course)
                .Include(x => x.offering.course.major)
                .Include(x => x.offering.semester)
                .Include(x => x.professor)
                .Include(x => x.TimeSlots)
                .Include(x => x.Enrollments)
                .FirstAsync(),
                recitationOfferings = await _context.Sections
                .Where(x => x.offering.parentcourse == sectionId)
                .Where(x => x.offering.type == "recitation")
                .Include(x => x.TimeSlots)
                .Include(x => x.professor)
                .Include(x => x.offering.semester).ToListAsync(),
                professors = await _context.Professors.ToListAsync(),
                semesters = await _context.Semesters.ToListAsync()
            };
            ViewData["Title"] = model.offering.offering.course.name + " (Offer #" + model.offering.Id + ")";
            return View("Offering", model);
        }

        [HttpPost]
        [Route("/Admin/Courses/Create")]
        public async Task<IActionResult> CreateAsync(CreateCourseVM coursevm)
        {
            var course = new Course
            {
                name = coursevm.name,
                number = coursevm.number,
                description = coursevm.description,
                credithours = coursevm.credithours,
                major = await _context.Majors.FindAsync(coursevm.majorid),
                career = coursevm.career,
                modeofinstruction = coursevm.modeofinstruction
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return Redirect("/Admin/Courses/" + course.Id);
        }

        [HttpPost]
        [Route("/Admin/Courses/{courseId}/Offerings/Create")]
        public async Task<IActionResult> CreateOfferingAsync(int courseId, CreateOfferingVM offeringvm)
        {
            var offering = new Offering
            {
                semester = await _context.Semesters.FindAsync(offeringvm.semesterid),
                course = await _context.Courses.FindAsync(courseId),
                capacity = offeringvm.capacity,
                type = offeringvm.type
            };
            if(offeringvm.type == "recitation")
            {
                //offeringvm.offeringid is the parent LECTURE offering
                offering.parentcourse = offeringvm.offeringid;
            }
            else
            {
                offering.parentcourse = 0;
            }

            var section = new Section
            {
                room = offeringvm.room,
                professor = await _context.Professors.FindAsync(offeringvm.professorid),
                offering = offering
            };

            _context.Offerings.Add(offering);
            _context.Sections.Add(section);
            await _context.SaveChangesAsync();
            return Redirect("/Admin/Courses/" + courseId + "/Offerings/" + section.Id);
        }

        [HttpPost]
        [Route("/Admin/Courses/{courseId}/Offerings/{sectionId}/Timeslots/Create")]
        public async Task<IActionResult> CreateTimeslotAsync(int courseId, int sectionId, CreateTimeSlotVM timeslotvm)
        {
            foreach(var day in timeslotvm.dayofweek)
            {
                var timeslot = new TimeSlot
                {
                    dayofweek = day,
                    starttime = timeslotvm.starttime,
                    endtime = timeslotvm.endtime,
                    section = await _context.Sections.FindAsync(sectionId)
                };
                _context.Timeslots.Add(timeslot);
            }
            
            await _context.SaveChangesAsync();
            return Redirect("/Admin/Courses/" + courseId + "/Offerings/" + sectionId);
        }
    }
}