namespace WebApp.Features.Admin.Courses
{
    public class CreateOfferingVM
    {
        public string room { get; set; }
        public int offeringid { get; set; }
        public int professorid { get; set; }
        public int semesterid { get; set; }
        public string type { get; set; }
        public int capacity { get; set; }
        public int parentcourse { get; set; }
    }
}