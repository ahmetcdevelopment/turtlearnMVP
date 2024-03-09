using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs.ApiDTOs
{
    public class MessageApiDTO : IDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }//Mesaj hangi chate gitti
        public string ChatName { get; set; }
        public int SenderId { get; set; }//Mesajı gönderen
        public string SenderName { get; set; }
        public string SenderLastName { get; set; }
        public string Content { get; set; }//İçerik
        public string Media { get; set; }//Medya eklendiyse medya - fotoğraf, video, ses
    }
}
