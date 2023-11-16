using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TurtLearn.Shared.Searching
{
    public interface ISearch<TDto> where TDto : class, IDto, new()
    {
        public IQueryable<TDto> FilterData(IQueryable<TDto> _Query, params Expression<Func<TDto, bool>>[] Exp);
        public int GetPropertyCount();
    }
}
