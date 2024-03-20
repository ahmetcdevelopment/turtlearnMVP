using STM.Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Domain.Enums;

public enum OrderStatus
{
    [EnumTitle("Onaylandı")]
    Accepted = 1,
    [EnumTitle("Reddedildi")]
    Denied = 2,
}
