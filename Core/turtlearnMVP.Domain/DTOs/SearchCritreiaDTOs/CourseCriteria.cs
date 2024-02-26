using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Domain.DTOs.SearchCritreiaDTOs
{
    public class CourseCriteria
    {
        public string? Name { get; set; }
        public int? TeacherId { get; set; }
        public int? CategoryId { get; set; }
        public string? TeacherName { get; set; }
        public int? Quota { get; set; }
        public decimal? PricePerHourMin { get; set; }//kurs için saatlik ücret.
        public decimal? PricePerHourMax { get; set; }//kurs için saatlik ücret.
        public decimal? TotalPriceMin { get; set; }//PricePerHour * TotalHour
        public decimal? TotalPriceMax { get; set; }//PricePerHour * TotalHour
        public int? Status { get; set; }
    }
}
