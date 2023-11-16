using STM.Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace TurtLearn.Shared.Utilities.Extensions
{
    public static class TableExtensions
    {
        public static string GetTableTitle<TTable>() where TTable : class, IEntity, new()
        {
            Type entityType = typeof(TTable);
            var titleAttribute = entityType.GetCustomAttribute<TableTitleAttribute>().Title;
            return !string.IsNullOrEmpty(titleAttribute) ? titleAttribute : string.Empty;
        }
    }
}
