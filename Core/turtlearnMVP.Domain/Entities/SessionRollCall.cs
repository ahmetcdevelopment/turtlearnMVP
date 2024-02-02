using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities
{
    [TableInfo("Yoklama", 19)]
    /// <summary>
    /// Her bir oturum için öğrenci yoklaması. - O derse gelen öğrenciler.
    /// </summary>
    public class SessionRollCall : EntityBase<int>, IEntity
    {
        public int CourseId { get; set; }//O kurstaki tüm yoklama verileri getirilirken daha optimize bir sorgu performansı için atıldı.
        public int SessionId { get; set; }//Hangi oturumun yoklaması
        public int UserId { get; set; }//Öğrenci Id'si
        public DateTime TimeToJoin { get; set; }//Kullanıcının katıldığı zaman
    }
}
