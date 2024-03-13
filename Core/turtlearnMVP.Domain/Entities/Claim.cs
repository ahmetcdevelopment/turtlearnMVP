using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities
{
    [TableInfo("Yetki", 22)]
    /// <summary>
    /// Grup veya kullanıcı sohbetleri
    /// </summary>
    public class Claim : EntityBase<int>, IEntity
    {
        public int ClaimTypeId { get; set; }
        public string ClaimValue { get; set; }
    }
}
