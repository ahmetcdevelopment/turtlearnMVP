namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class CourseEnrollmentConfirmViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }//Başvuranın Adı
        public string LastName { get; set; }// Başvuranın Soyadı
        public string CourseName { get; set; }//Başvurunun yapıldığı kurs
        public bool Approved { get; set; }//
        public string ApprovedStr { get; set; }//Başvurunun durumu
        public DateTime EnrollmentDate { get; set; }
    }
}
