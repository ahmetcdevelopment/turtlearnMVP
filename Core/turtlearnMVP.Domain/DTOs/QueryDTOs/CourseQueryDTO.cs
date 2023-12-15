using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Searching;

namespace turtlearnMVP.Domain.DTOs.QueryDTOs
{
    public class CourseQueryDTO : QueryDto<int, CourseDTO>
    {
        public CourseQueryDTO(ISearch<CourseDTO> search)
        {
            _Search = search;
        }
        public override IQueryable<CourseDTO> GetFilteredData(IQueryable<CourseDTO> _Query, CourseDTO Dto)
        {
            if (Expressions == null)
                Expressions = new System.Linq.Expressions.Expression<Func<CourseDTO, bool>>[_Search.GetPropertyCount()];
            if (Dto.Id.HasValue)
                Expressions.Append(c => c.Id == Dto.Id.Value);
            if (!string.IsNullOrEmpty(Dto.TeacherName))
                Expressions.Append(c => c.TeacherName.Trim().ToLower().Contains(Dto.TeacherName.Trim().ToLower()));
            if (!string.IsNullOrEmpty(Dto.TeacherLastName))
                Expressions.Append(c => c.TeacherLastName.Trim().ToLower().Contains(Dto.TeacherLastName.Trim().ToLower()));
            if (!string.IsNullOrEmpty(Dto.CategoryName))
                Expressions.Append(c => c.CategoryName.Trim().ToLower().Contains(Dto.CategoryName.Trim().ToLower()));
            if (Dto.StartDateStr != null && DateTime.TryParse(Dto.StartDateStr, out DateTime _startDate))
                Expressions.Append(c => c.StartDate < _startDate);
            if (Dto.EndDateStr != null && DateTime.TryParse(Dto.EndDateStr, out DateTime _endDate))
                Expressions.Append(c => c.EndDate < _endDate);
            if (Dto.Quota.HasValue)
                Expressions.Append(c => c.Quota.Value == Dto.Quota.Value);
            if (!string.IsNullOrEmpty(Dto.Name))
                Expressions.Append(c => c.Name.Trim().ToLower().Contains(Dto.Name.Trim().ToLower()));
            //Burası tekrar düzenlenecek ISearch yapısında halen çok eksik var.
            if (Dto.PricePerHour.HasValue)
                Expressions.Append(c => c.PricePerHour < Dto.PricePerHour.Value);
            if (Dto.TotalHour.HasValue)
                Expressions.Append(c => c.TotalHour == Dto.TotalHour.Value);
            if (Dto.TotalPrice.HasValue)
                Expressions.Append(c => c.TotalPrice == Dto.TotalPrice.Value);
            if (Dto.Status.HasValue)
                Expressions.Append(c => c.Status == Dto.Status.Value);
            return _Search.FilterData(_Query, Expressions);
        }
    }
}
