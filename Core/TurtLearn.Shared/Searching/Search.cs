using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace TurtLearn.Shared.Searching
{
    public abstract class Search<T> : ISearch<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression(null).Compile();

            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression(dynamic arguments);

        public ISearch<T> And(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return new AndSearch<T>(left, right);
        }
    }


}
