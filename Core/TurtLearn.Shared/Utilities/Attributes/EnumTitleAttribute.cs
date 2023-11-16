using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM.Core.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumTitleAttribute : Attribute
    {
        public string Title { get; set; }

        public EnumTitleAttribute(string title)
        {
            Title = title;
        }
    }
}
