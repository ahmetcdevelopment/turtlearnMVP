using STM.Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Domain.Enums
{
    public enum CourseStatus
    {
        [EnumTitle("Özel Ders")]
        Birebir = 1,
        [EnumTitle("Sınıf Ders")]
        Grup,
        [EnumTitle("Koçluk")]
        Kocluk,
    }
}
