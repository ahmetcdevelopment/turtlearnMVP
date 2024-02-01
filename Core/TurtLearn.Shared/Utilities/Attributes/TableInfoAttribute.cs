using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtLearn.Shared.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TableInfoAttribute : Attribute
    {
        public string Title { get; }
        public int Id { get; }


        public TableInfoAttribute(string Title, int Id)
        {
            this.Title = Title;
            this.Id = Id;
        }
    }
}
