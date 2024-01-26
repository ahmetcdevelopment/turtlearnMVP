using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs
{
    public class ChatDTO : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }//eğer bir grup konuşması ise grubun ismi
        public string Picture { get; set; }
        public int Privacy { get; set; }//Gizlilik public - onlymembers - private
        public string PrivacyTitle { get; set; }//string
    }
}
