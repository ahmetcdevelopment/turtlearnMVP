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

        public class Equal : Search<T>
        {
            public override Expression<Func<T, bool>> ToExpression(dynamic arguments)
            {
                string propertyName = arguments.GetType().GetProperty("PropAdi").GetValue(arguments, null);
                string value = arguments.GetType().GetProperty("Deger").GetValue(arguments, null);

                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
                ConstantExpression constant = Expression.Constant(value);

                // Eşitlik için == operatörünü kullanıyoruz
                BinaryExpression equalExp = Expression.Equal(member, constant);

                return Expression.Lambda<Func<T, bool>>(equalExp, parameter);
            }
        }

        public class GreaterThan : Search<T>
        {
            public override Expression<Func<T, bool>> ToExpression(dynamic arguments)
            {
                string propertyName = arguments.GetType().GetProperty("PropAdi").GetValue(arguments, null);
                string value = arguments.GetType().GetProperty("Deger").GetValue(arguments, null);

                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
                ConstantExpression constant = Expression.Constant(value);

                // Büyükse > operatörünü kullanıyoruz
                BinaryExpression greaterThanExp = Expression.GreaterThan(member, constant);

                return Expression.Lambda<Func<T, bool>>(greaterThanExp, parameter);
            }
        }

        public class LessThan : Search<T>
        {
            public override Expression<Func<T, bool>> ToExpression(dynamic arguments)
            {
                string propertyName = arguments.GetType().GetProperty("PropAdi").GetValue(arguments, null);
                string value = arguments.GetType().GetProperty("Deger").GetValue(arguments, null);

                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
                ConstantExpression constant = Expression.Constant(value);

                // Küçükse < operatörünü kullanıyoruz
                BinaryExpression lessThanExp = Expression.LessThan(member, constant);

                return Expression.Lambda<Func<T, bool>>(lessThanExp, parameter);
            }
        }

        public class DateRange : Search<T>
        {
            public override Expression<Func<T, bool>> ToExpression(dynamic arguments)
            {
                string propertyName = arguments.GetType().GetProperty("PropAdi").GetValue(arguments, null);
                DateTime baslangicTarihi = arguments.GetType().GetProperty("BaslangicTarihi").GetValue(arguments, null);
                DateTime bitisTarihi = arguments.GetType().GetProperty("BitisTarihi").GetValue(arguments, null);

                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
                ConstantExpression baslangicTarihiExp = Expression.Constant(baslangicTarihi);
                ConstantExpression bitisTarihiExp = Expression.Constant(bitisTarihi);

                // Tarih aralığı için && operatörünü ve DateTime.CompareTo metodunu kullanıyoruz
                BinaryExpression tarihAraligiExp = Expression.AndAlso(
                    Expression.GreaterThanOrEqual(member, baslangicTarihiExp),
                    Expression.LessThanOrEqual(member, bitisTarihiExp)
                );

                return Expression.Lambda<Func<T, bool>>(tarihAraligiExp, parameter);
            }
        }

        public class IsTrue : Search<T>
        {
            public override Expression<Func<T, bool>> ToExpression(dynamic arguments)
            {
                string propertyName = arguments.GetType().GetProperty("PropAdi").GetValue(arguments, null);

                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.PropertyOrField(parameter, propertyName);

                // True değer için == operatörünü ve bool.True değerini kullanıyoruz
                BinaryExpression isTrueExp = Expression.Equal(member, Expression.Constant(true));

                return Expression.Lambda<Func<T, bool>>(isTrueExp, parameter);
            }
        }

        public class IsFalse : Search<T>
        {
            public override Expression<Func<T, bool>> ToExpression(dynamic arguments)
            {
                string propertyName = arguments.GetType().GetProperty("PropAdi").GetValue(arguments, null);

                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.PropertyOrField(parameter, propertyName);

                // False değer için == operatörünü ve bool.False değerini kullanıyoruz
                BinaryExpression isFalseExp = Expression.Equal(member, Expression.Constant(false));

                return Expression.Lambda<Func<T, bool>>(isFalseExp, parameter);
            }
        }
    }
}
