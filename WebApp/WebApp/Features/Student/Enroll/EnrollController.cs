using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Features.Student.Enroll;
using WebApp.Infrastructure.Inherits;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApp.Features.Students.Enroll
{
    [Area("Student")]
    [Authorize("Student")]
    public class EnrollController : Controller
    {
        private HubContext _context;

        public EnrollController()
        {
            _context = new HubContext();
        }

        [Route("/Student/Enroll")]
        public IActionResult Enroll()
        {
            ViewData["Title"] = "Enroll";
            return View("Semester", _context.Semesters.ToList());
        }

        /// <summary>
        /// Page with filtered results
        /// </summary>
        /// <param name="SemesterID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/Student/Enroll/{SemesterID}/Courses")]
        public IActionResult CourseSelection(int SemesterID, ClassFilterVM filter)
        {
            //TODO: Manipulate the filter object as needed
            var result = _context.Sections
                .Where(x => x.offering.semester.Id == SemesterID)
                .Where(x => x.offering.course.major.Id == filter.MajorId)
                .Where(x => x.offering.type == "lecture")
                //.Where(x => x.offering.course.number == filter.CourseNumber)
                .Include(y => y.offering)
                .Include(y => y.offering.semester)
                .Include(y => y.offering.course.major)
                .Select(a => new CourseFilterResult
                {
                    offeringid = a.offering.Id,
                    coursename = a.offering.course.name,
                    professor = a.professor,
                    credithours = a.offering.course.credithours,
                    timeslot = a.TimeSlots,
                    coursenumber = a.offering.course.number,
                    cousedescription = a.offering.course.description

                })
                .ToList();

            switch (filter.CourseNumberFilterOption)
            {
                case 1:
                    result = result.Where(x => x.coursenumber == filter.CourseNumber).ToList();
                    break;
                case 3:
                    result = result.Where(x => x.coursenumber >= filter.CourseNumber).ToList();
                    break;
                case 4:
                    result = result.Where(x => x.coursenumber <= filter.CourseNumber).ToList();
                    break;
                default:
                    break;
            }

            ViewData["Title"] = "Filtered Results";
            return View("Courses", result);
        }

        [Route("/Student/Enroll/{SemesterID}/Courses/{OfferingID}")]
        public async Task<IActionResult> CourseEnrollInfoAsync(int SemesterID, int OfferingID)
        {
            var sectionId = (await _context.Sections.Where(x => x.offering.Id == OfferingID).FirstAsync()).Id;
            var course = await _context.Offerings.Where(x => x.Id == OfferingID).Include(m => m.course.major).Select(a => a.course).FirstAsync();
            var recitationOfferings = await _context.Offerings.Where(x => x.type == "recitation").Where(y => y.course.Id == course.Id).ToListAsync();

            var model = new CourseEnrollInfoVM()
            {
                course = course,
                professor = await _context.Sections.Where(x => x.Id == sectionId).Select(a => a.professor).FirstAsync(),
                timeslots = await _context.Timeslots.Where(x => x.section.Id == sectionId).Select(x => new TimeSlot()
                {
                    starttime = x.starttime,
                    endtime = x.endtime,
                    dayofweek = x.dayofweek
                }).ToListAsync(),
                OfferingId = OfferingID,
                semester = await _context.Semesters.FindAsync(SemesterID)
            };

            foreach (var offer in recitationOfferings)
            {
                model.recitations.Add(_context.Sections.Where(x => x.offering.Id == offer.Id).Include(p => p.professor).First());
            }

            ViewData["Title"] = model.course.name;
            //return new JsonResult(model);
            return View("Course", model);
        }

        [Route("/Student/Cart")]
        public IActionResult Cart()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var model = _context.Enrollments
                .Where(x => x.account.Id == Convert.ToInt32(val.Value))
                .Where(x => x.status != 1)
                .Include(x => x.section)
                .Include(x => x.section.professor)
                .Include(x => x.section.offering.course)
                .Include(x => x.section.TimeSlots)
                .ToList();

            ViewData["Title"] = "Cart";
            return View("Cart", model);
        }

        /// <summary>
        /// This is the page that displays the filter form
        /// </summary>
        /// <param name="SemesterID"></param>
        /// <returns></returns>
        [Route("/Student/Enroll/{SemesterID}/Search")]
        public IActionResult CourseSearch(int SemesterID)
        {
            ViewData["Title"] = "Course Search";
            return View("CourseSearch", new CourseSearchVM { Majors = _context.Majors.ToList(), SemesterId = SemesterID });
        }

        [Route("/Student/Enroll/CheckoutResult")]
        public IActionResult CheckoutResult()
        {
            ViewData["Title"] = "Checkout Result";
            return View("CheckoutResult");
        }

        //FOR TESTING PURPOSES
        [Route("/Student/Enroll/{SemesterID}/TestCourses")]
        public JsonResult TestCourseSelection(int SemesterID)
        {
            //TODO: Manipulate the filter object as needed
            var filter = new ClassFilterVM();
            filter.Career = 1;
            filter.CourseNumber = 442;
            filter.CourseNumberFilterOption = 1;
            filter.MajorId = 1;
            SemesterID = 1;

            var result = _context.Sections
                .Where(x => x.offering.semester.Id == SemesterID)
                .Where(x => x.offering.course.major.Id == filter.MajorId)
                //.Where(x => x.offering.type == "lecture")
                .Where(x => x.offering.course.number == filter.CourseNumber)
                .Include(y => y.offering)
                .Include(y => y.offering.semester)
                .Include(y => y.offering.course.major)
                .ToList();

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("/Student/Enroll/{SemesterID}/AddToCart")]
        public JsonResult AddToCart(int SemesterID, int courseId, int sectionForRecitationId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");

            var account = _context.Accounts.Find(Convert.ToInt32(val.Value));

            var courseEnrollment = new Enrollment
            {
                account = account,
                section = _context.Sections.Find(courseId),
                status = 2,
                dateadded = DateTime.Now
            };

            var recitationEnrollment = new Enrollment
            {
                account = account,
                section = _context.Sections.Find(sectionForRecitationId),
                status = 2,
                dateadded = DateTime.Now
            };

            _context.Enrollments.Add(courseEnrollment);
            _context.Enrollments.Add(recitationEnrollment);

            _context.SaveChanges();


            return new JsonResult(new { status = "success" });

        }

        [Route("/Student/Cart/Remove/{enrollmentID}")]
        public IActionResult RemoveFromCart(int enrollmentID)
        {
            _context.Enrollments.Remove(_context.Enrollments.Find(enrollmentID));
            _context.SaveChanges();

            return Redirect("/Student/Cart");
        }

        [Route("/Student/Cart/Clear")]
        public IActionResult ClearCart()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var account = _context.Accounts.Find(Convert.ToInt32(val.Value));

            var enrollments = _context.Enrollments.Where(x => x.account.Id == account.Id).Where(x => x.status == 2).ToList();

            foreach (var enrollment in enrollments)
            {
                _context.Enrollments.Remove(enrollment);
            }
            _context.SaveChanges();

            return Redirect("/Student/Cart");
        }
    }
}