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
    public class Session : EntityBase<int>, IEntity
    {

        /// <summary>
        /// Ait olduğu kurs
        /// </summary>
        public int CourseId { get; set; }// oturum hangi kursa ait.

        public int Queue { get; set; }

        public string Name { get; set; }// Oturumun ismi nedir - 1.oturum vs vs

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
