using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs
{
    public class CourseDTO : IDto
    {
        public int? Id { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherLastName { get; set; }
        public string CategoryName { get; set; }

        /// <summary>
        /// kursun başlayacağı tarih
        /// 17 haziran 2023 de başlıyor.
        /// course'un içinde section section olarak açılacak.
        /// </summary>
        public DateTime? StartDate { get; set; }
        public string StartDateStr => StartDate?.ToString("dd/MM/yyyy");

        /// <summary>
        /// kursun biteceği tarih
        /// 17 ağustos 2023 de bitiyor.
        /// </summary>
        public DateTime? EndDate { get; set; }
        public string EndDateStr => EndDate?.ToString("dd/MM/yyyy");
        /// <summary>
        /// Kursun kotası
        /// </summary>
        public int? Quota { get; set; } // kota, kontenjan

        /// <summary>
        /// Kursun Adı
        /// </summary>
        public string Name { get; set; }//kursun ismi

        /// <summary>
        /// Kursun saatlik ücreti
        /// </summary>
        public decimal? PricePerHour { get; set; }//kurs için saatlik ücret.

        /// <summary>
        /// Kursun saat bazında süresi
        /// </summary>
        public int? TotalHour { get; set; }

        /// <summary>
        /// Kursun saatle birlikte toplam ücreti
        /// </summary>
        public decimal? TotalPrice { get; set; }//PricePerHour * TotalHour

        public string Description { get; set; }

        /// <summary>
        /// Kurs statüsü. Örneğin Eğitim Koçluğu. Özel ders, yabancı dil egzersizi vb
        /// </summary>
        public int? Status { get; set; }
        public string StatusTitle { get; set; }

    }
}
