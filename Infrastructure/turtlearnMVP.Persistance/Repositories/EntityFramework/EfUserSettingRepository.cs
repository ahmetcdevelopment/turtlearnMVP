using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Application.Persistance.Abstract;
using turtlearnMVP.Application.Persistance.Repositories;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Context;

namespace turtlearnMVP.Persistance.Repositories.EntityFramework
{
    public class EfUserSettingRepository: Repository<UserSetting>, IUserSettingRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfUserSettingRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;
    }
}
