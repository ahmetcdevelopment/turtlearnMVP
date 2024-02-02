using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities
{
    [TableInfo("Kurs", 12)]
    /// <summary>
    /// Kurs aslen kararlaştırılmış oturumlar bütünüdür. Bir nevi ilandır.
    /// </summary>
    public class Course : EntityBase<int>, IEntity // ahmet çiftçi'nin matematik 1 kursu
    {

        public int TeacherId { get; set; } //UserId'den. her kursun bir öğretmeni vardır.

        public int CategoryId { get; set; } //mat türkçe cart curt

        /// <summary>
        /// kursun başlayacağı tarih
        /// 17 haziran 2023 de başlıyor.
        /// course'un içinde section section olarak açılacak.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// kursun biteceği tarih
        /// 17 ağustos 2023 de bitiyor.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Kursun kotası
        /// </summary>
        public int Quota { get; set; } // kota, kontenjan

        /// <summary>
        /// Kursun Adı
        /// </summary>
        public string Name { get; set; }//kursun ismi

        /// <summary>
        /// Kursun saatlik ücreti
        /// </summary>
        public decimal PricePerHour { get; set; }//kurs için saatlik ücret.

        /// <summary>
        /// Kursun saat bazında süresi
        /// </summary>
        public int TotalHour { get; set; }

        /// <summary>
        /// Kursun saatle birlikte toplam ücreti
        /// </summary>
        public decimal TotalPrice { get; set; }//PricePerHour * TotalHour

        public string Description { get; set; }

        /// <summary>
        /// Kurs statüsü. Örneğin Eğitim Koçluğu. Özel ders, yabancı dil egzersizi vb
        /// </summary>
        public int Status { get; set; }

    }
}
