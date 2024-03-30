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

        public Expression<Func<T, bool>> Equal(string propertyName, string value)
        {
            var arguments = new { PropAdi = propertyName, Deger = value };
            var equal = new BaseExpressions<T>.Equal();
            return equal.ToExpression(arguments);
        }

        public Expression<Func<T, bool>> GreaterThan(string propertyName, string value)
        {
            var arguments = new { PropAdi = propertyName, Deger = value };
            var greaterThan = new BaseExpressions<T>.GreaterThan();
            return greaterThan.ToExpression(arguments);
        }

        public Expression<Func<T, bool>> LessThan(string propertyName, string value)
        {
            var arguments = new { PropAdi = propertyName, Deger = value };
            var lessThan = new BaseExpressions<T>.LessThan();
            return lessThan.ToExpression(arguments);
        }

        public Expression<Func<T, bool>> DateRange(string propertyName, DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            var arguments = new { PropAdi = propertyName, BaslangicTarihi = baslangicTarihi, BitisTarihi = bitisTarihi };
            var dateRange = new BaseExpressions<T>.DateRange();
            return dateRange.ToExpression(arguments);
        }

        public Expression<Func<T, bool>> IsTrue(string propertyName)
        {
            var arguments = new { PropAdi = propertyName };
            var isTrue = new BaseExpressions<T>.IsTrue();
            return isTrue.ToExpression(arguments);
        }

        public Expression<Func<T, bool>> IsFalse(string propertyName)
        {
            var arguments = new { PropAdi = propertyName };
            var isFalse = new BaseExpressions<T>.IsFalse();
            return isFalse.ToExpression(arguments);
        }
    }
}
