using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Application.Persistance.Repositories;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Context;

namespace turtlearnMVP.Persistance.Repositories.EntityFramework
{
    public class EfChatRepository : Repository<Chat>, IChatRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfChatRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<ChatDTO> GetAllQueryableRecords()
        {
            var Query = from c in _Context.Chats.DefaultIfEmpty()
                        select new ChatDTO
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Picture = c.Picture,
                            Privacy = c.Privacy,
                        };
            return Query;
        }
    }
}
