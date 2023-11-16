using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Searching;

namespace TurtLearn.Shared.Entities.Abstract
{
    public abstract class QueryDto<TKey, TDto> : IDto where TDto : class, IDto, new()
    {
        protected virtual TKey? Id { get; set; }
        protected virtual ISearch<TDto> _Search { get; set; }
        protected virtual Expression<Func<TDto, bool>>[]? Expressions { get; set; }
        public abstract IQueryable<TDto> GetFilteredData(IQueryable<TDto> _Query, TDto Dto);
    }
}
