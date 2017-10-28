using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Features.Admin.Professors
{
    public class ProfessorVM
    {
        public Professor professor { get; set; }
        public ICollection<Section> sections { get; set; }
    }
}