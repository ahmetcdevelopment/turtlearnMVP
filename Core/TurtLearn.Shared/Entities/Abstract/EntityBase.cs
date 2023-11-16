using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtLearn.Shared.Entities.Abstract
{
    public abstract class EntityBase<TKey>
    {
        public virtual TKey Id { get; set; }
        public virtual int UpdateUserId { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
