using STM.Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Domain.Enums;

public enum Currency
{
    [EnumTitle("Türk Lirası")]
    TL=1,
    [EnumTitle("Euro")]
    EURO=2,
    [EnumTitle("Dolar")]
    DOLLAR=3
}
