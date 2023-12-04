using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Application.Persistance.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Context;

namespace turtlearnMVP.Persistance.Repositories.EntityFramework
{
    public class EfHomeworkTransferRepository : Repository<HomeworkTransfer>, IHomeworkTransferRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfHomeworkTransferRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<HomeworkTransferDTO> GetAllQueryableRecords()
        {
            var Query = from HT in _Context.HomeworkTransfers.DefaultIfEmpty()
                        join H in _Context.Homeworks.DefaultIfEmpty()
                        on HT.HomeworkId equals H.Id
                        join U in _Context.Users.DefaultIfEmpty()
                        on HT.StudentId equals U.Id
                        select new HomeworkTransferDTO
                        {
                            Id = HT.Id,
                            StudentId = U.Id,
                            StudentFirstName = U.FirstName,
                            StudentLastName = U.LastName,
                            HomeworkInfo = H.Title,
                            Description = H.Description,
                            TransferDate = HT.TransferDate,
                            Attachment = HT.Attachment,
                            Status = HT.Status,
                        };
            return Query;
        }
    }
}
