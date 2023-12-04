using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.Entities
{
    public class ChatUser : EntityBase<int>, IEntity
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public bool IsManager { get; set; }//Yönetici mi?
    }
}
