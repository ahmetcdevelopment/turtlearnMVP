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
    public class Search<T> : ISearch<T> where T : class, IDto, new()
    {
        //İleride recursive hale getirilebilir.
        //public IQueryable<T>? FilterData(Expression<Func<T, bool>> Exp, IQueryable<T> _Query)
        //    => (_Query != null || _Query?.Count() > 0) && Exp != null ? _Query.Where(Exp) : _Query;

        public IQueryable<T>? FilterData(IQueryable<T> _Query, params Expression<Func<T, bool>>[] Exp)
        {
            if ((_Query != null || _Query?.Count() > 0) && Exp.Any())
            {
                _Query = _Query.Where(Exp.Last());
                if (Exp.Length > 1)
                {
                    var remainingExp = Exp.Take(Exp.Length - 1).ToArray();
                    return FilterData(_Query, remainingExp);
                }
            }
            return _Query;
        }

        public int GetPropertyCount()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            return properties.Length;
        }
    }


    //public class Search<TDto> where TDto : class
    //{
    //    public IQueryable<TDto>? FilterData(IQueryable<TDto> query, Dictionary<string, string> filters)
    //    {
    //        foreach (var filter in filters)
    //        {
    //            PropertyInfo property = typeof(TDto).GetProperty(filter.Key);
    //            if (property != null)
    //            {
    //                if (!string.IsNullOrEmpty(filter.Value))
    //                {
    //                    if (property.PropertyType == typeof(string))
    //                        query = query.Where(BuildStringExpression(filter.Key, filter.Value) as Expression<Func<TDto, bool>>);
    //                    else if (property.PropertyType == typeof(int))
    //                        query = query.Where(BuildIntExpression(filter.Key, filter.Value) as Expression<Func<TDto, bool>>);
    //                    else if (property.PropertyType == typeof(decimal))
    //                        query = query.Where(BuildDecimalExpression(filter.Key, filter.Value) as Expression<Func<TDto, bool>>);
    //                }
    //            }
    //        }
    //        return query;
    //    }

    //    private Expression<Func<TDto, bool>> BuildStringExpression(string propertyName, string value)
    //    {
    //        var parameter = Expression.Parameter(typeof(TDto), "x");
    //        var property = Expression.Property(parameter, propertyName);
    //        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
    //        var constant = Expression.Constant(value, typeof(string));
    //        var contains = Expression.Call(property, containsMethod, constant);
    //        return Expression.Lambda<Func<TDto, bool>>(contains, parameter);
    //    }

    //    private Expression<Func<TDto, bool>> BuildIntExpression(string propertyName, string value)
    //    {
    //        var parameter = Expression.Parameter(typeof(TDto), "x");
    //        var property = Expression.Property(parameter, propertyName);
    //        var constant = Expression.Constant(int.Parse(value), typeof(int));
    //        var equal = Expression.Equal(property, constant);
    //        return Expression.Lambda<Func<TDto, bool>>(equal, parameter);
    //    }

    //    private Expression<Func<TDto, bool>> BuildDecimalExpression(string propertyName, string value)
    //    {
    //        var parameter = Expression.Parameter(typeof(TDto), "x");
    //        var property = Expression.Property(parameter, propertyName);
    //        var constant = Expression.Constant(decimal.Parse(value), typeof(decimal));
    //        var equal = Expression.Equal(property, constant);
    //        return Expression.Lambda<Func<TDto, bool>>(equal, parameter);
    //    }
    //}
}
