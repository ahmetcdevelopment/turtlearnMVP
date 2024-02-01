using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities
{
    [TableInfo("Sohbet", 9)]
    /// <summary>
    /// Grup veya kullanıcı sohbetleri
    /// </summary>
    public class Chat : EntityBase<int>, IEntity
    {
        public string Name { get; set; }//eğer bir grup konuşması ise grubun ismi
        public string Picture { get; set; }
        public int Privacy { get; set; }//Gizlilik public - onlymembers - private
    }
}
