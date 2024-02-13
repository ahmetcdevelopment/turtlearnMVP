using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities
{
    [TableInfo("Özgeçmiş", 21)]
    public class UserResume : EntityBase<int>, IEntity
    {
        public int UserId { get; set; }
        public int ResumeType { get; set; }//Enumda saklanıyor. -Degree - Experience etc.
        public string Title { get; set; }//Eğer Degree kaydıysa Okul ismi olacak, Experience kaydıysa çalıştığı yerlerin adı olacak
        public string Link { get; set; }//Sertifika kayıtları için sertifika linki
        public DateTime StartDate { get; set; }//Başlangıç zamanı
        public DateTime EndDate { get; set; }//Bitiş zamanı (Halen devam ediyorsa nasıl yapılacağı düşünülmeli)
    }
}
