using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs
{
    public class SessionRollCallDTO : IDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }//O kurstaki tüm yoklama verileri getirilirken daha optimize bir sorgu performansı için atıldı.
        public string CourseName { get; set; }//İlgili kursun ismi
        public int SessionId { get; set; }//Hangi oturumun yoklaması
        public string SessionTitle { get; set; }//İlgili oturumun başlığı.
        public string StudentFirstName { get; set; }//Yoklamadaki öğrencinin ismi
        public string StudentLastName { get; set; }//Yoklamadaki öğrencinin soyismi
        public DateTime TimeToJoin { get; set; }//Kullanıcının katıldığı zaman
        public string TimeToJoinStr => TimeToJoin.ToString("dd/MM/yyyy");
    }
}
