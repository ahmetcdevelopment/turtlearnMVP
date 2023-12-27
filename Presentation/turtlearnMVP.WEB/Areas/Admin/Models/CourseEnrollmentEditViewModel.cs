using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class CourseEnrollmentEditViewModel
    {
        public int? Id { get; set; }

        public int? CourseId { get; set; }

        public int? StudentId { get; set; }

        public bool Approved { get; set; }

        public SelectList? SelCourse { get; set; }

        public SelectList? SelStudent { get; set; }
    }
}
