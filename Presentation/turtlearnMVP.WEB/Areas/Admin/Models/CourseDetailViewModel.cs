using Microsoft.AspNetCore.Mvc.Rendering;

namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class CourseDetailViewModel
    {
        public CommentSendEditViewModel? CommentSendModel { get; set; }
        public int? Id { get; set; }
        public int TeacherId { get; set; } //UserId'den. her kursun bir öğretmeni vardır.
        public string TeacherName { get; set; } //UserId'den. her kursun bir öğretmeni vardır.

        public int CategoryId { get; set; } //mat türkçe cart curt
        public string CategoryStr { get; set; } //mat türkçe cart curt

        /// <summary>
        /// kursun başlayacağı tarih
        /// 17 haziran 2023 de başlıyor.
        /// course'un içinde section section olarak açılacak.
        /// </summary>
        public string StartDateStr { get; set; }

        /// <summary>
        /// kursun biteceği tarih
        /// 17 ağustos 2023 de bitiyor.
        /// </summary>
        public string EndDateStr { get; set; }

        ///// <summary>
        ///// Kursun kotası
        ///// </summary>
        //public int Quota { get; set; } // kota, kontenjan

        /// <summary>
        /// Kursun Adı
        /// </summary>
        public string Name { get; set; }//kursun ismi

        ///// <summary>
        ///// Kursun saatlik ücreti
        ///// </summary>
        //public decimal PricePerHour { get; set; }//kurs için saatlik ücret.

        /// <summary>
        /// Kursun saat bazında süresi
        /// </summary>
        public string TotalHour { get; set; }

        ///// <summary>
        ///// Kursun saatle birlikte toplam ücreti
        ///// </summary>
        //public decimal? TotalPrice { get; set; }//PricePerHour * TotalHour

        public string Description { get; set; }

        /// <summary>
        /// Kurs statüsü. Örneğin Eğitim Koçluğu. Özel ders, yabancı dil egzersizi vb
        /// </summary>
        public string StatusStr { get; set; }
    }
}

