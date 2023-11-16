using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities
{
    [TableTitle("Oturum")]
    /// <summary>
    /// Kursların her bir oturumu
    /// </summary>
    public class Session : IEntity
    {

        /// <summary>
        /// Ait olduğu kurs
        /// </summary>
        public int CourseId { get; set; }// oturum hangi kursa ait.

        /// <summary>
        /// katılacak öğrenci
        /// </summary>
        public int StudentId { get; set; }//User tablosundan çekeceğiz.

        /// <summary>
        /// 13:24 23.02.2023
        /// </summary>
        public DateTime StartDate { get; set; }//

        /// <summary>
        /// ders linki
        /// </summary>
        public string Link { get; set; }// dersin yapılacağı link


        [StringLength(350)]
        public string Description { get; set; } // Ders öncesi işlenecek konular vs açıklama.

    }
}
