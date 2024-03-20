using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs;

public class OfferDetailDTO : IDto
{
    public int Id { get; set; }
    public int OfferId { get; set; }
    public string OfferName { get; set; }
    public int DiscountRate { get; set; }
    /// <summary>
    /// Hangi kursa tanımlandı?
    /// </summary>
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public DateTime DefinitionDate { get; set; }
    public string DefinitonDateStr => DefinitionDate.ToString("dd/MM/yyyy");
    public DateTime EndDate { get; set; }
    public string EndDateStr => EndDate.ToString("dd/MM/yyyy");
}
