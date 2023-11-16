using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Application.Persistance.Abstract;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Repositories.EntityFramework
{
    public class EfCommentRepository : Repository<Comment>, ICommentRepository
    {
        public EfCommentRepository(DbContext context) : base(context)
        {
        }
    }
}
