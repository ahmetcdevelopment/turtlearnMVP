using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs
{
    public class CourseEnrollmentDTO : IDto
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public int StudentId { get; set; } // userdan alınacak.
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

        /// <summary>
        /// Kursa kabul edilme durumu
        /// (Enum ya da bool hakkında düşünmek gerek)
        /// </summary>
        public bool Approved { get; set; } // öğretmen onayı.
    }
}
