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
    public class CourseEnrollmentMap : IEntityTypeConfiguration<CourseEnrollment>
    {
        public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
        {
            throw new NotImplementedException();
        }
    }
}
