namespace turtlearnMVP.WEB.Models.Course
{
    public class CourseSliderViewModel
    {
        public int? TeacherId { get; set; }//sliderda görünecek kursların ait olduğu öğretmen
        public int[]? TeacherIds { get; set; }//birden fazla öğretmene ait kurs
        public int? CategoryId { get; set; }//kategoriye göre
    }
}
