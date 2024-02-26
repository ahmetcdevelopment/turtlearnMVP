using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Searching;
using turtlearnMVP.Application.Persistance.Services;

namespace turtlearnMVP.Persistance.Services
{
    public class SearchManager<T> : ISearchService<T>
    {
        private readonly ISearch<T> _search;
        //BaseExpressions<T> _baseExpressions;
        public SearchManager(ISearch<T> search)
        {
            _search = search;
            var _baseExpressions = new BaseExpressions<T>();
        }

        public ISearch<T> AndConcat(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return _search.And(left, right); 
        }

        public Expression<Func<T, bool>> StringContains(string propertyName, string value)
        {
            var arguments = new { PropAdi = propertyName, Deger = value };
            var stringContains = new BaseExpressions<T>.StringContains();
            return stringContains.ToExpression(arguments);
        }
    }
}
