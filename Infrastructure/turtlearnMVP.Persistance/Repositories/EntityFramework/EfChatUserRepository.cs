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
    public class EfChatUserRepository : Repository<ChatUser>, IChatUserRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfChatUserRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<ChatUserDTO> GetAllQueryableRecords()
        {
            var Query = from CU in _Context.ChatUsers.DefaultIfEmpty()
                        join C in _Context.Chats.DefaultIfEmpty()
                        on CU.ChatId equals C.Id
                        join U in _Context.Users.DefaultIfEmpty()
                        on CU.UserId equals U.Id
                        select new ChatUserDTO
                        {
                            Id = CU.Id,
                            ChatId = C.Id,
                            UserId = U.Id,
                            ChatName = C.Name,
                            UserFirstName = U.FirstName,
                            UserLastName = U.LastName,
                            IsManager = CU.IsManager,
                        };
            return Query;
        }
    }
}
