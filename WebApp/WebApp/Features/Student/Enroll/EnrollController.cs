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
using Itenso.TimePeriod;

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

        /// <summary>
        /// Populates the semester selection drop down with available semesters
        /// </summary>
        /// <returns>View("Semester", model)</returns>
        [Route("/Student/Enroll")]
        public IActionResult Enroll()
        {
            var model = new SemesterPickerVM()
            {
                AvailableSemesters = _context.Semesters
                .Where(x => x.enrollopen <= DateTime.Now).Where(x => x.enrollclose >= DateTime.Now)
                .ToList(),
                AllSemesters = _context.Semesters.Where(x => x.enddate >= DateTime.Now).ToList()
            };

            ViewData["Title"] = "Enroll";
            return View("Semester", model);
        }

        /// <summary>
        /// Applying course search filters and returning the resulting courses
        /// </summary>
        /// <param name="SemesterID"></param>
        /// <returns>View("Courses", result)</returns>
        [HttpPost]
        [Route("/Student/Enroll/{SemesterID}/Courses")]
        public IActionResult CourseSelection(int SemesterID, ClassFilterVM filter)
        {
            ViewData["Title"] = "Filtered Results";
            //Apply garunteed filters first and populate new CourseFilterResult
            var result = _context.Sections
                .Where(x => x.offering.semester.Id == SemesterID)
                .Where(x => x.offering.course.major.Id == filter.MajorId)
                .Where(x => x.offering.type == "lecture")
                .Where(x => x.offering.course.career == filter.Career)
                .Where(x => x.offering.course.modeofinstruction == filter.ModeOfInstruction)
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
                    cousedescription = a.offering.course.description,
                    type = Capitalize(a.offering.type),
                    capacity = a.offering.capacity,
                    enrolled = _context.Enrollments.Count(x => x.section.Id == a.Id)
                })
                .ToList();

            //Filter for "show open classes" search option
            if (filter.ShowOpenClasses)
            {
                result = result.Where(x => x.enrolled < x.capacity).ToList();
            }

            //Filter for course number search options
            if (filter.CourseNumber == 0) return View("Courses", result);
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

            return View("Courses", result);
        }

        /// <summary>
        /// Given a string, return that string with the first letter capitalized (for certain ui elements)
        /// </summary>
        /// <param name="original"></param>
        /// <returns>String with capitalized first letter</returns>
        public string Capitalize(string original)
        {
            return char.ToUpper(original[0]) + original.Substring(1);
        }

        /// <summary>
        /// Page for the offering of a course in a particular semester. Get the course info and the associated recitations.
        /// </summary>
        /// <param name="SemesterID"></param>
        /// <param name="OfferingID"></param>
        /// <returns>View("Course", model)</returns>
        [Route("/Student/Enroll/{SemesterID}/Courses/{OfferingID}")]
        public async Task<IActionResult> CourseEnrollInfoAsync(int SemesterID, int OfferingID)
        {
            var sectionId = (await _context.Sections.Where(x => x.offering.Id == OfferingID).FirstAsync()).Id;
            var course = await _context.Offerings.Where(x => x.Id == OfferingID).Include(m => m.course.major).Select(a => a.course).FirstAsync();
            var recitationOfferings = await
                _context.Sections
                .Where(x => x.offering.parentcourse == sectionId)
                .Where(x => x.offering.type == "recitation")
                .Include(x => x.TimeSlots)
                .Include(x => x.professor)
                .Include(x => x.offering.semester).ToListAsync();

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
                semester = await _context.Semesters.FindAsync(SemesterID),
                capacity = (await _context.Offerings.Where(x => x.Id == OfferingID).FirstAsync()).capacity,
                enrolled = await _context.Enrollments.CountAsync(x => x.section.Id == sectionId),
                waitlist = false
            };

            foreach (var offer in recitationOfferings)
            {
                model.recitations.Add(offer);
            }

            ViewData["Title"] = model.course.name;
            //return new JsonResult(model);
            return View("Course", model);
        }

        /// <summary>
        /// For cart page, populate with class information which has been added to the cart
        /// </summary>
        /// <returns>View("Cart", model</returns>
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
                .Include(x => x.section.offering)
                .ToList();

            ViewData["Title"] = "Cart";
            return View("Cart", model);
        }

        /// <summary>
        /// This is the page that displays the course search filter form
        /// </summary>
        /// <param name="SemesterID"></param>
        /// <returns>Class search filter form</returns>
        [Route("/Student/Enroll/{SemesterID}/Search")]
        public IActionResult CourseSearch(int SemesterID)
        {
            ViewData["Title"] = "Course Search";
            return View("CourseSearch", new CourseSearchVM { Majors = _context.Majors.ToList(), SemesterId = SemesterID, semester = _context.Semesters.Find(SemesterID) });
        }

        /// <summary>
        /// Shows the result of the checkout action
        /// </summary>
        /// <returns>View("CheckoutResult")</returns>
        [Route("/Student/Enroll/CheckoutResult")]
        public IActionResult CheckoutResult()
        {
            ViewData["Title"] = "Checkout Result";
            return View("CheckoutResult");
        }

        /// <summary>
        /// Gets enrollment info for the courses and if checks pass, then adds class/recitation to the database
        /// </summary>
        /// <param name="SemesterID"></param>
        /// <param name="courseId"></param>
        /// <param name="sectionForRecitationId"></param>
        /// <returns>JsonResult(new { status = "success" })</returns>
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
                section = _context.Sections.Where(x => x.offering.Id == courseId).First(),
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


            if (CheckCart(SemesterID, account, courseEnrollment.section.Id, sectionForRecitationId))
            {
                _context.Enrollments.Add(courseEnrollment);
                _context.Enrollments.Add(recitationEnrollment);
                _context.SaveChanges();
            }

            else
            {
                return new JsonResult(new { status = "false" });
            }

            return new JsonResult(new { status = "success" });

        }

        /// <summary>
        /// Removes desired course from the cart and updates the database with the changes
        /// </summary>
        /// <param name="enrollmentID"></param>
        /// <returns>Redirect("/Student/Cart")</returns>
        [Route("/Student/Cart/Remove/{enrollmentID}")]
        public IActionResult RemoveFromCart(int enrollmentID)
        {
            var courseEnrollment = _context.Enrollments.Where(x => x.Id == enrollmentID).Include(x => x.section).Include(x => x.section.offering.course).First();
            if (courseEnrollment.section.offering.type == "lecture")
            {
                try
                {
                    _context.Enrollments.Remove(
                        _context.Enrollments
                        //.Where(x => x.section.offering.type == "recitation")
                        .Where(x => x.section.offering.parentcourse == courseEnrollment.section.Id)
                        .First()
                        );
                    _context.Enrollments.Remove(courseEnrollment);
                }
                catch (Exception ex)
                {
                    _context.Enrollments.Remove(courseEnrollment);
                }

            }
            else
            {
                _context.Enrollments.Remove(courseEnrollment);
            }
            _context.SaveChanges();

            return Redirect("/Student/Cart");
        }

        /// <summary>
        /// Removes desired course from the cart and updates the database with the changes. Dashboard drop
        /// </summary>
        /// <param name="enrollmentID"></param>
        /// <returns>Redirect("/Student")</returns>
        [Route("/Student/Cart/Remove/Dashboard/{enrollmentID}")]
        public IActionResult DashboardDropButton(int enrollmentID)
        {
            var course = _context.Enrollments.Where(x => x.Id == enrollmentID).Include(x => x.section).Include(x => x.section.offering.course).First();
            if (course.section.offering.type == "lecture")
            {
                try
                {
                    _context.Enrollments.Remove(_context.Enrollments.Where(x => x.section.offering.parentcourse == course.section.Id).First());
                    _context.Enrollments.Remove(course);
                }
                catch (Exception ex)
                {
                    _context.Enrollments.Remove(course);
                }

            }
            else
            {
                _context.Enrollments.Remove(course);
            }
            _context.SaveChanges();

            return Redirect("/Student");
        }

        /// <summary>
        /// Removes desired course from the cart and updates the database with the changes. MyCourses drop
        /// </summary>
        /// <param name="enrollmentID"></param>
        /// <returns>Redirect("/Student/Courses")</returns>
        [Route("/Student/Cart/Remove/MyCourses/{enrollmentID}")]
        public IActionResult MyCoursesDropButton(int enrollmentID)
        {
            var course = _context.Enrollments.Where(x => x.Id == enrollmentID).Include(x => x.section).Include(x => x.section.offering.course).First();
            if (course.section.offering.type == "lecture")
            {
                try
                {
                    _context.Enrollments.Remove(_context.Enrollments.Where(x => x.section.offering.parentcourse == course.section.Id).First());
                    _context.Enrollments.Remove(course);
                }
                catch (Exception ex)
                {
                    _context.Enrollments.Remove(course);
                }

            }
            else
            {
                _context.Enrollments.Remove(course);
            }
            _context.SaveChanges();

            return Redirect("/Student/Courses");
        }

        /// <summary>
        /// Removes courses from the cart and updates the database with the changes
        /// </summary>
        /// <returns>Redirect("/Student/Cart")</returns>
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

        /// <summary>
        /// Changes status for classes in cart to registered
        /// </summary>
        /// <returns>Redirect("/Student/Enroll/CheckoutResult")</returns>
        [Route("/Student/Enroll/Checkout")]
        public IActionResult CheckOut()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var val = claims.First(x => x.Type == "sub");
            var currentaccount = _context.Accounts.Find(Convert.ToInt32(val.Value));

            foreach (var y in _context.Enrollments.Where(x => x.account.Id == currentaccount.Id).Where(x => x.status == 2).Include(x => x.section.offering.course).Include(x => x.section.offering).ToList())
            {
                y.status = 1;

                if (y.section.offering.type == "lecture")
                {
                    var notification = new Notification
                    {
                        account = currentaccount,
                        date = DateTime.Now,
                        content = "You just enrolled in " + y.section.offering.course.name + "! Don't forget to visit the bookstore's website to see what books are needed.",
                        title = "New Course Added To Your Schedule!",
                        status = 0
                    };
                    _context.Notifications.Add(notification);
                }

                _context.SaveChanges();
            }
            return Redirect("/Student/Enroll/CheckoutResult");
        }

        /// <summary>
        /// Checks if classes are valid to add to cart
        /// </summary>
        /// <param name="semesterID"></param>
        /// <param name="account"></param>
        /// <param name="course"></param>
        /// <param name="rec"></param>
        /// <returns>True if classes can be added. False otherwise</returns>
        public bool CheckCart(int semesterID, Account account, int course, int rec)
        {
            var enrolled = _context.Enrollments
                .Where(x => x.section.offering.semester.Id == semesterID)
                .Where(x => x.account.Id == account.Id)
                .Where(x => x.status < 3)
                .Include(y => y.section.offering.course)
                .Include(t => t.section.TimeSlots)
                .ToList();    

            foreach (var item in enrolled)

            {
                if (item.section.Id == course) return false;
                if (item.section.Id == rec) return false;

                foreach(var itemSlot in item.section.TimeSlots)
                {
                    foreach(var courseSlot in _context.Sections.Where(x => x.Id == course).Include(t => t.TimeSlots).First().TimeSlots)
                    {
                        if (itemSlot.dayofweek == courseSlot.dayofweek)
                        {
                            var splitItemSlotStart = itemSlot.starttime.Split(':');
                            var splitItemSlotEnd = itemSlot.endtime.Split(':');
                            var splitCourseSlotStart = courseSlot.starttime.Split(':');
                            var splitCourseSlotEnd = courseSlot.endtime.Split(':');

                            var itemSlotDate = new TimeRange(
                                new DateTime(2017, 1, 1, Convert.ToInt32(splitItemSlotStart[0]), Convert.ToInt32(splitItemSlotStart[1]), 0),
                                new DateTime(2017,1,1, Convert.ToInt32(splitItemSlotEnd[0]), Convert.ToInt32(splitItemSlotEnd[1]), 0));
                            var itemCourseDate = new TimeRange(
                                new DateTime(2017, 1, 1, Convert.ToInt32(splitCourseSlotStart[0]), Convert.ToInt32(splitCourseSlotStart[1]), 0),
                                new DateTime(2017, 1, 1, Convert.ToInt32(splitCourseSlotEnd[0]), Convert.ToInt32(splitCourseSlotEnd[1]), 0));

                            if (itemSlotDate.IntersectsWith(itemCourseDate))
                            {
                                //We have a conflict on both the day and times.
                                return false;
                            }
                        }
                    }
                }

            }

            //foreach(var i in enrolled)
            //{
            //    if (i.section.offering.course.Id == course)
            //    {
            //        return (i.section.offering.course.Id == rec);
            //    }
            //    else if (i.section.offering.course.Id == rec) return false;
            //}

            return true;

        }

    }
}