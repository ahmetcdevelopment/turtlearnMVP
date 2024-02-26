using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TurtLearn.Shared.Searching
{
    public class BaseExpressions<T>
    {
        public class StringContains : Search<T>
        {
            public override Expression<Func<T, bool>> ToExpression(dynamic arguments)
            {
                string propertyName = arguments.GetType().GetProperty("PropAdi").GetValue(arguments, null);
                string value = arguments.GetType().GetProperty("Deger").GetValue(arguments, null);

                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
                ConstantExpression constant = Expression.Constant(value);
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                MethodCallExpression containsMethodExp = Expression.Call(member, method, constant);

                return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameter);
            }
        }
    }
}
