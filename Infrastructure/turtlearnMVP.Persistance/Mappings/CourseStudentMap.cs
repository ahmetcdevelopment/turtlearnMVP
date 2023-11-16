using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Mappings
{
    public class CourseStudentMap : IEntityTypeConfiguration<CourseStudent>
    {
        public void Configure(EntityTypeBuilder<CourseStudent> builder)
        {
            throw new NotImplementedException();
        }
    }
}
