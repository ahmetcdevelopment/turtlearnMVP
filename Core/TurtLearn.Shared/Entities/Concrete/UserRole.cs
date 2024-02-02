using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace TurtLearn.Shared.Entities.Concrete
{
    [TableInfo("Kullanıcı Rol", 4)]
    public class UserRole : IdentityUserRole<int>, IEntity
    {
    }
}
