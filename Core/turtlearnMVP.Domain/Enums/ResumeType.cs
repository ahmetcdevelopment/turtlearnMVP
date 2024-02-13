using STM.Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Domain.Enums
{
    public enum ResumeType
    {
        [EnumTitle("Mezuniyet")]
        Degree = 1,
        [EnumTitle("Deneyim")]
        Experience = 2,
        [EnumTitle("Sertifika")]
        Certificate = 3,
    }
}
