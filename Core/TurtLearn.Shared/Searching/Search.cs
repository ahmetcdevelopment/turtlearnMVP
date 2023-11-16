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
            if (Exp.Any() && _Query != null || _Query?.Count() > 0)
            {
                _Query = _Query.Where(Exp.Last());
                Array.Resize(ref Exp, Exp.Length - 1);//dizinin son elemanını çıkarıyoruz
                return Exp != null || Exp?.Length > 0 ? FilterData(_Query, Exp) : _Query;
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
}
