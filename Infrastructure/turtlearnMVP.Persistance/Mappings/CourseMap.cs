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
    public class CourseMap : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.TeacherId).IsRequired();

            builder.Property(c => c.CategoryId).IsRequired();

            builder.Property(c => c.StartDate).IsRequired();

            builder.Property(c => c.EndDate).IsRequired();

            builder.Property(c => c.Quota).IsRequired();

            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(100);

            builder.Property(c => c.PricePerHour).IsRequired();

            builder.Property(c => c.TotalHour).IsRequired();

            builder.Property(c => c.TotalPrice).IsRequired();

            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(500);

            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(c => c.UpdateUserId).IsRequired();

            builder.Property(c => c.UpdateDate).IsRequired();

            builder.ToTable("Courses");
        }
    }
}
