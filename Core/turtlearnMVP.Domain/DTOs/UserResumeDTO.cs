using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs;

public class UserResumeDTO : IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ResumeType { get; set; }//Enumda saklanıyor. -Degree - Experience etc.
    public string ResumeTypeTitle { get; set; }//Özgeçmiş türünün title'ı
    public string Title { get; set; }//Eğer Degree kaydıysa Okul ismi olacak, Experience kaydıysa çalıştığı yerlerin adı olacak
    public string Link { get; set; }//Sertifika kayıtları için sertifika linki
    public DateTime StartDate { get; set; }//Başlangıç zamanı
    public string StartDateStr { get; set; }
    public DateTime EndDate { get; set; }//Bitiş zamanı (Halen devam ediyorsa nasıl yapılacağı düşünülmeli)
    public string EndDateStr { get; set; }
}
