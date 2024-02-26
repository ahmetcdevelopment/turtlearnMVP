using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Searching;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface ISearchService<T>
    {
        ISearch<T> AndConcat(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right);
        Expression<Func<T, bool>> StringContains(string propertyName, string value);
    }
}
