using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs.ApiDTOs
{
    public class SessionApiDTO : IDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int Queue { get; set; }
        public string SessionName { get; set; }
        public DateTime StartDate { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }
}
