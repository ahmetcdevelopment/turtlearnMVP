using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities;

[TableInfo("Kampanya Detay", 24)]
public class OfferDetail : EntityBase<int>, IEntity
{
    /// <summary>
    /// Hangi kampanya tanımlandı
    /// </summary>
    public int OfferId { get; set; }
    /// <summary>
    /// Hangi kursa tanımlandı?
    /// </summary>
    public int CourseId { get; set; }
    public DateTime DefinitionDate { get; set; }
    public DateTime EndDate { get; set; }
}
