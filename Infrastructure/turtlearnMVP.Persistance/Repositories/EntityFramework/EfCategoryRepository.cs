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
    public class EfCategoryRepository : Repository<Category>, ICategoyRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfCategoryRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<CategoryDTO> GetAllQueryableRecords()
        {
            var Query = from C in _Context.Categories
                        where C.IsDeleted == false
                        select new CategoryDTO
                        {
                            Id = C.Id,
                            Name = C.Name,
                            Content = C.Content,
                            SinifDuzeyiId = C.SinifDuzeyiId,
                            Description = C.Description,
                            Picture = C.Picture,
                        };
            return Query;
        }
    }
}
