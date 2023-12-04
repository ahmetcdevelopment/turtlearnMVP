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
            builder.HasKey(cs => cs.Id);
            builder.Property(cs => cs.Id).ValueGeneratedOnAdd();

            builder.Property(cs => cs.CourseId).IsRequired();

            builder.Property(cs => cs.StudentId).IsRequired();

            builder.Property(cs => cs.UpdateUserId).IsRequired();
            builder.Property(cs=>cs.UpdateDate).IsRequired();

            builder.ToTable("CourseStudents");
        }
    }
}
