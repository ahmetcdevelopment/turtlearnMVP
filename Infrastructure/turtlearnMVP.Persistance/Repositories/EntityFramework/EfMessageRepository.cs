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
    public class EfMessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfMessageRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<MessageDTO> GetAllQueryableRecords()
        {
            var Query = from M in _Context.Messages.DefaultIfEmpty()
                        join C in _Context.Chats.DefaultIfEmpty()
                        on M.ChatId equals C.Id
                        join U in _Context.Users.DefaultIfEmpty()
                        on M.SenderId equals U.Id
                        select new MessageDTO
                        {
                            Id = M.Id,
                            ChatId = C.Id,
                            SenderId = U.Id,
                            ChatName = C.Name,
                            Content = M.Content,
                            Media = M.Media,
                            SenderLastName = U.LastName,
                            SenderName = U.FirstName,
                        };
            return Query;
        }
    }
}
