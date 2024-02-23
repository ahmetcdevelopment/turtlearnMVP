using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace TurtLearn.Shared.Entities.Dtos
{
    public class DropdownDTO<TKey, TValue> : IDto
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
