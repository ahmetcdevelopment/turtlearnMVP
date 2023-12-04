using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.Entities
{
    /// <summary>
    /// Ders oturumlarındaki mesajlaşmalar veya özel mesajlaşmalar
    /// </summary>
    public class Message : EntityBase<int>, IEntity
    {
        public int ChatId { get; set; }//Mesaj hangi chate gitti
        public int SenderId { get; set; }//Mesajı gönderen
        public string Content { get; set; }//İçerik
        public string Media { get; set; }//Medya eklendiyse medya - fotoğraf, video, ses
    }
}
