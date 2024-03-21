using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TurtLearn.Shared.Searching
{
    public class AndSearch<T> : Search<T>
    {
        private readonly Expression<Func<T, bool>> _leftSpecification;
        private readonly Expression<Func<T, bool>> _rightSpecification;

        public AndSearch(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            _rightSpecification = right;
            _leftSpecification = left;
        }

        public override Expression<Func<T, bool>> ToExpression(dynamic arguments)
        {
            Expression<Func<T, bool>> leftExpression = _leftSpecification;
            Expression<Func<T, bool>> rightExpression = _rightSpecification;

            // Sol ve sağ ifadelerin parametrelerini alın
            var leftParameter = leftExpression.Parameters[0];
            var rightParameter = rightExpression.Parameters[0];

            // Sol ve sağ ifadelerin gövdelerini birbiriyle AND işlemine geçirin
            var andExpression = Expression.AndAlso(
                Expression.Invoke(leftExpression, rightParameter),
                Expression.Invoke(rightExpression, rightParameter)
            );

            // Lambda ifadesini oluşturun ve döndürün
            return Expression.Lambda<Func<T, bool>>(andExpression, rightParameter);
        }
    }
}
