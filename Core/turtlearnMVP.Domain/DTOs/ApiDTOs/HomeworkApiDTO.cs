using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs.ApiDTOs;

public class HomeworkApiDTO : IDto
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public string Title { get; set; }

    public DateTime StartDate { get; set; }//ödevin başlangıç zamanı
    public DateTime EndDate { get; set; }//ödevin bitiş zamanı
    public string Description { get; set; }//ödev açıklaması.
}
