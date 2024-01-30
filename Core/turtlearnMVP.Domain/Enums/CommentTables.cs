using STM.Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Domain.Enums
{
    public enum CommentTables
    {
        [EnumTitle("Kurs")]
        Course=1,
        [EnumTitle("Oturum")]
        Session=2,
    }
}
