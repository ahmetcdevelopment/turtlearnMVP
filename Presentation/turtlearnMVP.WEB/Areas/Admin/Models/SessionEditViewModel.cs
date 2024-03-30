using System.ComponentModel.DataAnnotations;

namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class SessionEditViewModel
    {
        public int? Id { get; set; }
        /// <summary>
        /// Ait olduğu kurs
        /// </summary>
        public int CourseId { get; set; }// oturum hangi kursa ait.
        public string Name { get; set; }// Oturumun ismi nedir - 1.oturum vs vs

        /// <summary>
        /// 13:24 23.02.2023
        /// </summary>
        public DateTime StartDate { get; set; }//

        /// <summary>
        /// ders linki
        /// </summary>
        //public string Link { get; set; }// dersin yapılacağı link


        [StringLength(350)]
        public string Description { get; set; } // Ders öncesi işlenecek konular vs açıklama.
    }
}
