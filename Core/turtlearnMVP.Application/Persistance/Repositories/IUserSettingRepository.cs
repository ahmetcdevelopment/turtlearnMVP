﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Repositories
{
    public interface IUserSettingRepository:IRepository<UserSetting>
    {
    }
}
