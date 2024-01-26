using STM.Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Domain.Enums
{
    public enum ChatPrivacy
    {
        [EnumTitle("Herkese Açık")]
        Public=1,
        [EnumTitle("Gizli")]
        Private=2,
        [EnumTitle("Grup Sohbet")]
        Group=3,
    }
}
