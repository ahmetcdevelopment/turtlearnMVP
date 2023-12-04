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
            builder.HasKey(ce => ce.Id);
            builder.Property(ce => ce.Id).ValueGeneratedOnAdd();

            builder.Property(ce => ce.CourseId).IsRequired();

            builder.Property(ce => ce.StudentId).IsRequired();

            builder.Property(ce => ce.Approved).IsRequired();

            builder.Property(ce => ce.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(ce => ce.UpdateUserId).IsRequired();
            builder.Property(ce => ce.UpdateDate).IsRequired();

            builder.ToTable("CourseEnrollments");
        }
    }
}
