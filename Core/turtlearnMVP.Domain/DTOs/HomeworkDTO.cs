using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs;

public class HomeworkDTO : IDto
{
    public int Id { get; set; }
    /// <summary>
    /// Ödevin ait olduğu oturum.
    /// </summary>

    public string SessionInfo { get; set; }//Oturum bilgisi

    /// <summary>
    /// Ödev başlığı
    /// </summary>
    [StringLength(200)]
    public string Title { get; set; }

    //ödevin dosyalarnı içeren bir property gerekli
    public DateTime StartDate { get; set; }//ödevin başlangıç zamanı
    public string StartDateStr => StartDate.ToString("dd/MM/yyyy");
    public DateTime EndDate { get; set; }//ödevin bitiş zamanı
    public string EndDateStr => EndDate.ToString("dd/MM/yyyy");

    [StringLength(350)]
    public string Description { get; set; }//ödev açıklaması.
}
