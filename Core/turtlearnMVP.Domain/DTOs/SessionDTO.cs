using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs
{
    public class SessionDTO : IDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Ait olduğu kurs
        /// </summary>
        public string CourseInfo { get; set; }//Kursun ismi veya başlığı olabilir.

        /// <summary>
        /// Sırası
        /// </summary>
        public int Queue { get; set; }

        /// <summary>
        /// Birinci oturum, ikinci oturum, 4.12.2023 tarihli oturum
        /// </summary>
        public string SessionName { get; set; }
        /// <summary>
        /// 13:24 23.02.2023
        /// </summary>
        public DateTime StartDate { get; set; }//
        public string StartDateStr => StartDate.ToString("dd/MM/yyyy");

        /// <summary>
        /// ders linki
        /// </summary>
        public string Link { get; set; }// dersin yapılacağı link

        [StringLength(350)]
        public string Description { get; set; } // Ders öncesi işlenecek konular vs açıklama.
    }
}
