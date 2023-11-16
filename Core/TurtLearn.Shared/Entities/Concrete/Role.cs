using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace TurtLearn.Shared.Entities.Concrete
{
    public class Role : IdentityRole<int>, IEntity
    {
    }
}
