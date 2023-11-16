using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs
{
    public class CourseStudentDTO : IDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }//Course tablosundan join ile alınacak.
        public int StudentId { get; set; }//user tablosundan alınacak.
        public string StudentFirstName { get; set; }//Kursun öğrencisinin ismi
        public string StudentLastName { get; set;}//Kursun öğrencisinin soyismi
    }
}
