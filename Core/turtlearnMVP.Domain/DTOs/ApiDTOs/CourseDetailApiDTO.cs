using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs.ApiDTOs;

public class CourseDetailApiDTO : IDto
{
    public string TeacherFirstName { get; set; }
    public string TeacherLastName { get; set; }
    public string CategoryName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public int? Quota { get; set; } // kota, kontenjan

    public string Name { get; set; }//kursun ismi

    public decimal? PricePerHour { get; set; }//kurs için saatlik ücret.

    public int? TotalHour { get; set; }

    public decimal? TotalPrice { get; set; }//PricePerHour * TotalHour

    public string Description { get; set; }

    public int? Status { get; set; }
    public IList<SessionDetailApiDTO> Sessions { get; set; }
}
public class SessionDetailApiDTO
{
    public string CourseInfo { get; set; }//Kursun ismi veya başlığı olabilir.

    public int Queue { get; set; }

    public string SessionName { get; set; }
    public string StartDate { get; set; }//

    public string Link { get; set; }// dersin yapılacağı link

    public string Description { get; set; } // Ders öncesi işlenecek konular vs açıklama.
    //Course'un detaylarını çekerken ödevleri çekmiyorum ama  session'un detaylarını çekerken ödevleri de çekiyorum.
    public IList<HomeworkDetailApiDTO> Homeworks { get; set; } = new List<HomeworkDetailApiDTO>();
    public IList<CommentDetailApiDTO> Comments { get; set; } = new List<CommentDetailApiDTO>();
}
public class HomeworkDetailApiDTO
{
    public string Title { get; set; }

    public DateTime StartDate { get; set; }//ödevin başlangıç zamanı
    public string StartDateStr => StartDate.ToString("dd/MM/yyyy");
    public DateTime EndDate { get; set; }//ödevin bitiş zamanı
    public string EndDateStr => EndDate.ToString("dd/MM/yyyy");

    public string Description { get; set; }//ödev açıklaması.
}
public class CommentDetailApiDTO
{
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string Text { get; set; }//yapılan yorumun içeriği
    public short Rating { get; set; }//1 - 2 - 3 - 4 - 5 puanlama
}
