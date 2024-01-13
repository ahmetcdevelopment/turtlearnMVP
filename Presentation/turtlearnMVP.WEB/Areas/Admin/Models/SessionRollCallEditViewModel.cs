namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class SessionRollCallEditViewModel
    {
        public int? Id { get; set; }
        public int CourseId { get; set; }//O kurstaki tüm yoklama verileri getirilirken daha optimize bir sorgu performansı için atıldı.
        public int SessionId { get; set; }//Hangi oturumun yoklaması
        public int UserId { get; set; }//Öğrenci Id'si
        public DateTime TimeToJoin { get; set; }//Kullanıcının katıldığı zaman
    }
}
