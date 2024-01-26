using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs
{
    public class ChatUserDTO : IDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string ChatName { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public bool IsManager { get; set; }//Yönetici mi?
    }
}
